define(['newJquery', 'tools', 'template','kkpager'], function () {
    Date.prototype.format = function(format)
    {
        var o =
        {
            "M+" : this.getMonth()+1, //month
            "d+" : this.getDate(),    //day
            "h+" : this.getHours(),   //hour
            "m+" : this.getMinutes(), //minute
            "s+" : this.getSeconds(), //second
            "q+" : Math.floor((this.getMonth()+3)/3),  //quarter
            "S" : this.getMilliseconds() //millisecond
        }
        if(/(y+)/.test(format))
        format=format.replace(RegExp.$1,(this.getFullYear()+"").substr(4 - RegExp.$1.length));
        for(var k in o)
        if(new RegExp("("+ k +")").test(format))
        format = format.replace(RegExp.$1,RegExp.$1.length==1 ? o[k] : ("00"+ o[k]).substr((""+ o[k]).length));
        return format;
    }
    var page =
        {
            pageSize: 10,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx",
            //关联到的类别
            elems:
            {
                searchBtn: $("#searchBtn"),                //搜索按钮
                serialNo: $("#serialNo"),               //流水号
                dateTime: $("#dateTime"),       //日期
                beginDate:$("#date-begin"),                 //开始时间
                endDate:$("#date-end"),                     //结束时间
                tabQueryResult:$("#queryResult"),       //表格结果
                popTableResult:$("#popTableResult"),
                status:$("#status"),                    //状态
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            clearInput: function () {

            },
            //初始化提现日期内容
            initTime:function(){
                var date = new Date();
                //当前日期
                var currentDate=date.format("yyyy-MM-dd");
                var list=[];
                list[0]={
                    date:currentDate+"/"+currentDate,
                    dateName:"今天"
                };

                var startDate=new Date();// 开始日期
                //昨天
                startDate.setDate(date.getDate()-1);
                var yesterday=startDate.format("yyyy-MM-dd");
                list[1]={
                    date:yesterday+"/"+yesterday,
                    dateName:"昨天"
                }
          
                var startDate2=new Date();// 开始日期
                var endDate2=new Date();// 结束日期
                var day=date.getDate(); // 取当日
                var week=date.getDay();// 取当日是周几
                startDate2.setDate(day-week);
                endDate2.setDate(day+(6-week));
                list[2]={
                    date:startDate2.format("yyyy-MM-dd")+"/"+endDate2.format("yyyy-MM-dd"),
                    dateName:"本周"
                }
               
                var startDate3=new Date();// 开始日期
                var endDate3=new Date();// 结束日期
                var year=date.getFullYear();
                var month=date.getMonth()+1;
                endDate3=new Date(year+"-"+(month+1)+"-01");
                startDate3.setDate(1);
                endDate3.setDate(0);
                list[3]={
                    date:startDate3.format("yyyy-MM-dd")+"/"+endDate3.format("yyyy-MM-dd"),
                    dateName:"本月"
                }
                var html=bd.template("tpl_date",{list:list});
                this.elems.dateTime.html(html);
            },
            init: function () {
                var that=this;
                this.initTime();
                this.initStatus();
                this.initTables();
               
                var picker = new Pikaday(
                {
                    field: that.elems.beginDate[0],
                    format:"yyyy-MM-dd",
                    firstDay: 1,
                    minDate: new Date('2000-01-01'),
                    maxDate: new Date('2020-12-31'),
                    yearRange: [1900,2050]
                });
                var picker2 = new Pikaday(
                {
                    field: that.elems.endDate[0],
                    format:"yyyy-MM-dd",
                    firstDay: 1,
                    minDate: new Date('2000-01-01'),
                    maxDate: new Date('2020-12-31'),
                    yearRange: [1900,2050]
                });
                this.initEvent();
            },
            //初始化状态
            initStatus:function(){
                var that=this;
                this.loadData.getStatus(function(data){
                debugger;
                    var list=data.Data.GetCustomerOrderPayStatusList;
                    var html=bd.template("tpl_status",{list:list});
                    that.elems.status.html(html);
                });
            },
            initTables:function(){
                debugger;
                var that=this;
                $("#loading").show();
                //订单号
                var serialNo=this.elems.serialNo.val();
                this.loadData.args.SerialNo=serialNo;
                //时间
                var date=$("#dateTimeText").data("day");
                //获取阶段值
                if(date==0||date==-1){
                    this.loadData.args.BeginDate=this.elems.beginDate.val()?this.elems.beginDate.val():this.loadData.args.BeginDate;
                    this.loadData.args.EndDate=this.elems.endDate.val()?this.elems.endDate.val():this.loadData.args.EndDate;
                }else{
                    this.loadData.args.BeginDate=date.split("/")[0];
                    this.loadData.args.EndDate=date.split("/")[1];
                }
                //状态
                var status=$("#statusText").data("status");
                this.loadData.args.Status=status;
                //初始化当前页为0
                this.loadData.args.PageIndex=0;
                //请求结果
                this.loadData.getList(function(data){
                        var list=data.Data.CustomerWithdrawalList;
                        list=list?list:[];
                    var html=bd.template("tpl_content",{list:list});
                    if(list.length){
                        $("#footer").hide();
                        $("#content").html(html);
                        $("#loading").hide();
                    }else{
                         $("#content").html("");
                         $("#footer").show();
                    }
                    if(data.Data.TotalPageCount>1){
                         that.selector="#list1";
                         kkpager.generPageHtml({
	                        pno : 1,
	                        mode : 'click', //设置为click模式
	                        //总页码  
	                        total :data.Data.TotalPageCount,  
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
	
                        },true);
                   }
               
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
            //显示弹层
            showElements:function(selector){
                this.elems.uiMask.show();
                $(selector).show();
            },
            hideElements:function(selector){
                this.elems.uiMask.fadeIn(500);
                $(selector).fadeIn(500);
            },
            //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex=currentPage-1;
                this.loadData.getList(function(data){
                    var list=data.Data.CustomerWithdrawalList;
                    list=list?list:[];
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                });
            },
            //获得更多的入账记录
            getMoreData:function(currentPage){
                that.loadData.args.PageIndex1=currentPage-1;
                that.loadData.getEntryList(function(data){
                    var list=data.Data.CustomerOrderList;
                    list=list?list:[];
                    if(list.length){
                        var html=bd.template("tpl_popContent",{list:list})
                        $("#popContent").html(html);
                    }else{
                         $("#popContent").html("<p style='text-align:center'>暂无数据</p>");
                    }
                });
            },
            initPagination: function (currentPage, allPage, callback) {
                    $('.pagination').remove();
                    var html = bd.template("pageTmpl", {});
                    $(this.selector).append(html);
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
            initEvent: function () {
                //初始化事件集
                var that = this;
                //流水号 关键字查询事件  按下enter
                this.elems.serialNo.keydown(function (e) {
                    if (e.keyCode == 13) {
                        that.initTables();
                    }
                });
                //关键字查询事件
                this.elems.searchBtn.click(function () {
                    that.initTables();
                });
                 //选择提现时间事件
                this.elems.dateTime.delegate("span","click",function(){
                    var $this=$(this);
                    $("#dateTimeText").html($this.html());
                    var day=$this.data("day");
                    //设置日期
                    $("#dateTimeText").data("day",day);
                    //让选择日期的空间不能再选择
                    if(day!=0&&day!=-1){
                        that.elems.beginDate.attr("disabled","disabled");
                        that.elems.endDate.attr("disabled","disabled");
                    }else{
                        that.elems.beginDate.removeAttr("disabled");
                        that.elems.endDate.removeAttr("disabled");
                    }
                });
                //获得点击每行的事件
                this.elems.tabQueryResult.on('click', '.handle', function () {
                    //获得提现的id
                    var id = $(this).data("id");
                    //根据id请求入账列表数据
                    //让层显示
                    that.showElements("#listDiv");
                    that.loadData.args.PageIndex1=0;
                    that.loadData.args.WithdrawalId=id;
                    that.loadData.getEntryList(function(data){
                        var list=data.Data.CustomerOrderList;
                        list=list?list:[];
                        if(list.length){
                            $("#popFooter").hide();
                            var html=bd.template("tpl_popContent",{list:list})
                            $("#popContent").html(html);
                        }else{
                            $("#popContent").html("");
                            $("#popFooter").show();
                        }
                        if(data.Data.PageCount>1){
                            that.selector="#list2";
                            kkpager.generPageHtml({
                                pagerid:'kkpager2',
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
                                    that.getMoreData(n);
	                            },
	                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
	                            getHref : function(n){
		                            return '#';
	                            }
	
                            });
                        }else{
                            //$("#popContent").html("<p style='text-align:center'>暂无数据</p>");
                        }
                       
                    },true);
                });
                //关闭弹出层
               $(".hintClose").bind("click",function(){
                      that.elems.uiMask.slideUp();
                      $(this).parent().parent().fadeOut();
               });
                //选择状态事件
                this.elems.status.delegate("span","click",function(){
                    var $this=$(this);
                    $("#statusText").html($this.html());
                    $("#statusText").data("status",$this.data("status"));
                });
            }
        };

    page.loadData =
    {
        args:{
            PageIndex:0,
            PageSize:10,
            Status:"-1"
        },
        //获得提现列表
        getList: function (callback) {
             $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetCustomerWithdrawalList',
                    'SerialNo':this.args.SerialNo,   //流水号
                    'BeginDate':this.args.BeginDate,     //支付状态
                    'EndDate':this.args.EndDate,
                    'Status':this.args.Status,
                    'PageIndex': this.args.PageIndex,
                    'PageSize': this.args.PageSize
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
        getStatus:function(callback){
            $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'GetCustomerOrderPayStatus'
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
         //根据提现ID获得入现列表
        getEntryList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'getCustomerOrderPayPage',
                    'PageSize':10,
                    'PageIndex':this.args.PageIndex1,
                    'WithdrawalId':this.args.WithdrawalId   //提现ID
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