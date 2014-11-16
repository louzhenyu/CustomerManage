define(['newJquery', 'tools', 'template',,'kkpager'], function () {
    var page =
        {
            pageSize: 2,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/Card/CardEntry.ashx",
            //关联到的类别
            elems:
            {
                keyword:$("#keyword"),                                 //查询关键字
                btnQueryCard:$("#queryVip"),                           //查询按钮
                popDiv:$("#popDiv"),                                   //弹出层
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            clearInput: function () {

            },
            init: function () {
                var that=this;
                this.initTables();
                this.initEvent();
            },
            stopBubble: function (e) {
                if (e && e.stopPropagation) {
                    //因此它支持W3C的stopPropagation()方法 
                    e.stopPropagation();
                }
                else {
                    //否则，我们需要使用IE的方式来取消事件冒泡 
                    window.event.cancelBubble = true;
                }
                e.preventDefault();
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
            //显示弹层
            showElements:function(selector){
                this.elems.uiMask.show();
                $(selector).show();
            },
            hideElements:function(selector){
                this.elems.uiMask.fadeIn(500);
                $(selector).fadeIn(500);
            },
            initTables:function(){
                var that=this;
                $("#loading").show();
                //初始化当前页为0
                this.loadData.args.PageIndex=0;
                //请求结果
                this.loadData.getCardSummary(function(data){
                    var list=data.Data.CardSummaryArray;
                    list=list?list:[];
                    debugger;
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                    if(data.Data.TotalPage<1){
                      $("#content").html("<p style='text-align:center'>暂无数据</p>");
                   }
                    kkpager.generPageHtml({
	                        pno : 1,
	                        mode : 'click', //设置为click模式
	                        //总页码  
	                        total :data.Data.TotalPage,  
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
               
                });
            },
             //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex=currentPage-1;
                this.loadData.getCardSummary(function(data){
                   var list=data.Data.CardSummaryList;
                    list=list?list:[];
                    
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                });
            },
            initEvent: function () {
            }
        };

    page.loadData =
    {
        args:{
            PageIndex:0,
            PageSize:10,
            Status:"-1"
        },
         //消费信息查询
        getCardSummary  : function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'CardSummary',
                    'PageSize':this.args.PageSize,
                    'PageIndex':this.args.PageIndex
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