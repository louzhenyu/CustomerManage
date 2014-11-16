define(['newJquery', 'tools', 'template', 'kkpager'], function () {
    var page =
        {
            pageSize: 2,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx",
            //关联到的类别
            elems:
            {
                searchBtn: $("#search"),                //搜索按钮
                inOrderNo: $("#orderNo"),               //输入框订单号
                inOrderSource: $("#orderSource"),       //订单来源
                inOrderStatus:$("#statusText"),         //订单状态
                divStatus:$("#status"),                 //订单状态选择
                divSource:$("#source"),                 //订单来源
                tabQueryResult:$("#queryResult"),       //表格结果
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            clearInput: function () {

            },
            init: function () {
                var that=this;
                this.initSource();
                this.initStatus();
                this.searchResult();

                this.initEvent();
            },
            //初始化来源
            initSource:function(){
                var that=this;
                this.loadData.getSource(function(data){
                    var list=data.Data.listSource;
                    var html=bd.template("tpl_source",{list:list})
                    that.elems.divSource.html(html);
                });
                
            },
            //初始化来源
            initStatus:function(){
                var that=this;
                this.loadData.getStatus(function(data){
                    var list=data.Data.listOptionStatus;
                    var html=bd.template("tpl_status",{list:list});
                    that.elems.divStatus.html(html);
                });
               
            },
            //显示遮罩层
            showMask: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.chooseEventsDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopUp(type);
                    this.elems.chooseEventsDiv.show();
                }
            },
            //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex=currentPage-1;
                this.loadData.getList(function(data){
                    var list=data.Data.CustomerOrderList;
                    list=list?list:[];
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                });
            },
            initPagination: function (currentPage, allPage, callback) {
                    $('.pagination').remove();
                    var html = bd.template("pageTmpl", {});
                    $(".dataTableShow").append(html);
                    $('.pagination').jqPagination({
                        link_string: '/?page={page_number}',
                        current_page: currentPage,
                        //设置当前页 默认为1
                        max_page: allPage,
                        //设置最大页 默认为1
                        page_string: '当前第{current_page}页,共{max_page}页',
                        paged: function (page) {
                            //回发事件。。。
                            if (callback) {
                                callback(page);
                            }
                        }
                    });
            },
            //搜索结果
            searchResult:function(){
                var that=this;
                $("#footer").show();
                //订单号
                var orderNo=this.elems.inOrderNo.val();
                //订单来源
                var source=this.elems.inOrderSource.data("status");
                //订单支付状态
                var status=$("#statusText").data("status");
                //初始化当前页为0
                this.loadData.args.PageIndex=0;
                this.loadData.args.OrderNo=orderNo;
                this.loadData.args.OrderSource=source;
                this.loadData.args.PayStatus=status;
                //请求结果
                this.loadData.getList(function(data){
                    //判断是否是datatable了   不能够重复初始化
                    var $searchResult = that.elems.tabQueryResult;  
                    var list=data.Data.CustomerOrderList;
                    list=list?list:[];
                    if(list.length){
                        $("#footer").hide();
                        var html=bd.template("tpl_content",{list:list})
                        $("#content").html(html);
                    }else{
                        $("#content").html("");
                        $("#footer").show().find("td").html("未检索到相关数据内容");
                    }
                    if(data.Data.PageCount>1){
                        kkpager.generPageHtml({
	                        pno : 1,
	                        mode : 'click', //设置为click模式
	                        //总页码  
	                        total :data.Data.PageCount,  
                            isShowTotalPage:false,
                            isShowTotalRecords:false,
	                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
	                        //适用于不刷新页面，比如ajax
	                        click : function(n){
		                        //这里可以做自已的处理
		                        //...
		                        //处理完后可以手动条用selectPage进行页码选中切换
		                        this.selectPage(n);
                                that.loadMoreData(n);
	                        },
	                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
	                        getHref : function(n){
		                        return '#';
	                        }
	
                        });
                    }
                });
            },
            initEvent: function () {
                //初始化事件集
                var that = this;
                //订单号 关键字查询事件  按下enter
                this.elems.inOrderNo.keydown(function (e) {
                    if (e.keyCode == 13) {
                        that.searchResult();
                    }
                });
                //订单来源 查询事件  按下enter
                this.elems.inOrderSource.keydown(function (e) {
                    if (e.keyCode == 13) {
                        that.searchResult();
                    }
                });
                //关键字查询事件
                this.elems.searchBtn.click(function () {
                    that.searchResult();
                });
                //选择状态事件
                this.elems.divStatus.delegate("span","click",function(){
                    var $this=$(this);
                    $("#statusText").html($this.html());
                    $("#statusText").data("status",$this.data("status"));
                });
                //选择来源事件
                this.elems.divSource.delegate("span","click",function(){
                    var $this=$(this);
                    that.elems.inOrderSource.html($this.html());
                    that.elems.inOrderSource.data("status",$this.data("status"));
                });
                
                //分页事件
                this.elems.tabQueryResult.on( 'page.dt',   function (page) { 
                    console.log(page);
                
                } )
            }


        };

    page.loadData =
    {
        //请求参数
        args:{
            PageIndex:0,
            PageSize:10
        },
         getStatus:function(callback){
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetOptionsStatus',
                    'statusName':'CustomerOrderPayStatus'
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        getSource:function(callback){
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetOrderSource'
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得入账列表
        getList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'getCustomerOrderPayPage',
                    'OrderNo':this.args.OrderNo,   //订单号
                    'OrderPayStatus':this.args.PayStatus,     //支付状态
                    'OrderSource':this.args.OrderSource,
                    'PageSize': this.args.PageSize,
                    'PageIndex': this.args.PageIndex
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        }
    }
    //初始化
    page.init();
});