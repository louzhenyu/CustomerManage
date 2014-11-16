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
            pageSize: 2,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/Card/CardEntry.ashx",
            //关联到的类别
            elems:
            {
                searchBtn: $("#searchBtn"),                //搜索按钮
                dateTime: $("#dateTime"),       //日期
                beginDate:$("#date-begin"),                 //开始时间
                endDate:$("#date-end"),                     //结束时间
                source:$("#source"),                        //渠道
                sourceText:$("#sourceText"),                //渠道内容
                useStatus:$("#useStatus"),                  //使用状态
                useStatusText:$("#useStatusText"),          //使用状态的内容
                cardStatus:$("#cardStatus"),                //卡状态
                cardStatusText:$("#cardStatusText"),        //卡内容
                tabQueryResult:$("#queryResult"),       //表格结果
                popTableResult:$("#popTableResult"),
                status:$("#status"),                    //状态
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            clearInput: function () {

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
                //初始化渠道
                
                this.initSource();
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
             //初始化渠道
            initSource: function () {
                var that = this;
                this.loadData.getSource(function (data) {
                
                    var list = data.Data.ChannelList;
                    var html = bd.template("tpl_source", { list: list })
                    that.elems.source.html(html);
                });

            },
            initTables:function(){
                var that=this;
                $("#loading").show();
                //渠道ID
                var channelId=this.elems.sourceText.data("status");
                this.loadData.args.ChannelID=channelId;
                //idfrom
                var idFrom=$("#idFrom").val();
                this.loadData.args.CardNoStart=idFrom;
                var idTo=$("#idTo").val();
                this.loadData.args.CardNoEnd=idTo;
                //使用状态
                var useStatus=that.elems.useStatusText.data("status");
                this.loadData.args.UseStatus=useStatus;
                //卡状态
                var cardStatus=that.elems.cardStatus.data("status");
                this.loadData.args.CardStatus=cardStatus;
                //自定义类型
                this.loadData.args.DateRange=5;
                //金额
                var intoMoney=$("#intoMoney").val();
                this.loadData.args.Amount=intoMoney;
                //制卡日期
                var date=$("#dateTimeText").data("day");

                //获取阶段值
                if(date==999){
                    
                     this.loadData.args.CreateTimeStart=this.elems.beginDate.val()?this.elems.beginDate.val():this.loadData.args.BeginDate;
                    this.loadData.args.CreateTimeEnd=this.elems.endDate.val()?this.elems.endDate.val():this.loadData.args.EndDate;
                }else{
                    this.loadData.args.CreateTimeStart=date.split("/")[0];
                    this.loadData.args.CreateTimeEnd=date.split("/")[1];
                }
                //初始化当前页为0
                this.loadData.args.PageIndex=0;
                
                //请求结果
                this.loadData.getCardList(function(data){
                    
                    var list=data.Data.CardList;
                    list=list?list:[];
                    
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                    //$("#appendPage").append("<div id='kkpager'></div>");
                    var total=data.Data.TotalPage;
                    if(data.Data.TotalPage>=1){
                         that.selector="#list1";
                         kkpager.generPageHtml({
	                        pno : 1,
	                        mode : 'click', //设置为click模式
	                        //总页码  
	                        total :total,  
                            isShowTotalPage:true,
                            isShowTotalRecords:false,
	                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
	                        //适用于不刷新页面，比如ajax
	                        click : function(n){
		                        //这里可以做自已的处理
                                this.init({total:total});
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
                   }else{
                      $("#content").html("<p style='text-align:center'>暂无数据</p>");
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
                this.loadData.getCardList(function(data){
                   var list=data.Data.CardList;
                    list=list?list:[];
                    
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                     //触发全选按钮事件
                    if($("#selAll").hasClass("on")){
                        $("#selAll").removeClass("on");
                        $("#selAll").trigger("click");
                    }
                });
            },

            initEvent: function () {
                //初始化事件集
                var that = this;
                //流水号 关键字查询事件  按下enter
                $("#intoMoney").keydown(function (e) {
                    if (e.keyCode == 13) {
                        that.initTables();
                    }
                });
                //关键字查询事件
                this.elems.searchBtn.click(function () {
                    //验证所有的输入的内容是否合法
                    var idFrom=$("#idFrom");   //开始卡号
                    var idTo=$("#idTo");       //结束卡号
                    var intoMoney=$("#intoMoney");   //充值金额
                    if(isNaN(idFrom.val())){
                        alert("开始卡序号请输入数字!");
                        return;
                    }
                    if(isNaN(idTo.val())){
                        alert("结束卡序号请输入数字!");
                        return;
                    }
                    if(isNaN(intoMoney.val())){
                        alert("充值金额请输入数字!");
                        return;
                    }
                    //删除分页
                    that.initTables();
                });
                //选择渠道
                this.elems.source.delegate("span","click",function(){
                    var $this=$(this);
                    
                    that.elems.sourceText.html($this.html());
                    that.elems.sourceText.data("status",$this.data("status"))
                });

                //选择使用状态
                this.elems.useStatus.delegate("span","click",function(){
                    var $this=$(this);
                    that.elems.useStatusText.html($this.html());
                    that.elems.useStatusText.data("status",$this.data("status"))
                });
                //选中每行
                $("tbody").delegate(".toSel","click",function(e){
                    var $this=$(this);
                    if($this.hasClass("on")){
                        $this.removeClass("on");
                    }else{
                        $this.addClass("on");
                    }
                    that.stopBubble(e);
                });
                //全选
                $("#selAll").click(function(e){
                    var $this=$(this);
                    if($this.hasClass("on")){
                        $this.removeClass("on");
                        $(".toSel").removeClass("on");
                    }else{
                        $this.addClass("on");
                        $(".toSel").addClass("on");
                    }

                });
                //选择卡状态
                this.elems.cardStatus.delegate("span","click",function(){
                    var $this=$(this);
                    that.elems.cardStatusText.html($this.html());
                    that.elems.cardStatus.data("status",$this.data("status"))
                });
                 //选择提现时间事件
                this.elems.dateTime.delegate("span","click",function(){
                    var $this=$(this);
                    $("#dateTimeText").html($this.html());
                    var day=$this.data("day");
                    //设置日期
                    $("#dateTimeText").data("day",day);
                    //让选择日期的空间不能再选择
                    if(day!=""&&day==999){
                       $("#timeBetwwen").show();
                    }else{
                     $("#timeBetwwen").hide();
                        //时间段显示
                        
                    }
                });
                //可以弹出单个设置的层
                this.elems.tabQueryResult.on("mouseover",".last",function(){
                    that.elems.tabQueryResult.find(".set-layer").hide();
                    $(this).find(".set-layer").show();
                }).on("mouseout",".last",function(){
                    $(this).find(".set-layer").hide();
                });
                //状态更新
                $("tbody").delegate(".set-layer span","click",function(e){
                    var $this=$(this);
                    
                     //获得id
                     var itemId=$this.parent().parent().data("itemid");
                     var newStatus=$this.data("status");
                     var text=$this.data("text");
                     that.loadData.setCard.CardIds=[itemId];
                     that.loadData.setCard.CardStatus=newStatus;
                    //有on点击则进行取消
                    if(!$this.hasClass("on")){
                        //更新卡状态
                        that.loadData.setCardStatus(function(data){
                            //状态修改
                            $this.parent().parent().find("span").removeClass("on");
                            $this.addClass("on");
                            $this.parent().parent().parent().find("a").html(text);
                        });

                        
                    }
                     that.stopBubble(e);

                });
                //关闭弹出层
               $(".hintClose").bind("click",function(e){
                      that.elems.uiMask.slideUp();
                      $(this).parent().parent().fadeOut();
                      that.stopBubble(e);
               });
                //选择状态事件
                this.elems.status.delegate("span","click",function(e){
                    var $this=$(this);
                    $("#statusText").html($this.html());
                    $("#statusText").data("status",$this.data("status"));
                    that.stopBubble(e);
                });
                //批量更新卡
                $("#btnUpdateCards").click(function(e){
                    var cardIds=[];
                    $("tbody").find(".toSel").each(function(i,o){
                        var $this=$(this);
                        if($this.hasClass("on")){
                            var cardId=$this.data("cardid");
                            cardIds.push(cardId);
                        }
                    });
                    if(cardIds.length==0){
                        alert("请至少选中一行内容再进行批量操作!!");
                    }else{
                        that.loadData.setCard.CardIds=cardIds;
                        $("#cardCount").html(cardIds.length);
                        //弹出更新层
                        that.showElements("#popUpdateDiv");
                   }
                   that.stopBubble(e);
                });
                //确定批量更新卡
                $("#sureUpdate").click(function(e){
                    var status=$("#setCardStatusText").data("status");
                    that.loadData.setCard.CardStatus=status;
                    that.loadData.setCardStatus(function(data){
                        alert("成功修改"+that.loadData.setCard.CardIds.length+"条数据!");
                        //重新请求一次数据
                        that.initTables();
                         $(".hintClose").trigger("click");
                    });
                    that.stopBubble(e);
                });
                //批量选择卡状态
                $("#setCardStatus").delegate("span","click",function(e){
                    var $this=$(this);
                    $("#setCardStatusText").html($this.html());
                    $("#setCardStatusText").data("status",$this.data("status"));
                    that.stopBubble(e);
                });
                 //导出数据
                $("#exportAll").click(function(e){
//                    that.loadData.exportCard(function(data){
//                    
//                    });


                    var cardIds=[];
                    $("tbody").find(".toSel").each(function(i,o){
                        var $this=$(this);
                        if($this.hasClass("on")){
                            var cardId=$this.data("cardid");
                            cardIds.push(cardId);
                        }
                    });
                    var url="";
                    if(cardIds.length){
                        
                        url=that.url+"?type=Product&action=ExportCard"+'&req={"Parameters":{"CardIDs":{array}},'+'"random":'+Math.random()+'}';
                        url=url.replace("{array}",JSON.stringify(cardIds));
                    }else{
                        url=that.url+"?type=Product&action=ExportCard"+'&req={"Parameters":{"CardIDs":null},'+'"random":'+Math.random()+'}';
                    }
                    location.href=url;
                    that.stopBubble(e);
                });
            }
        };

    page.loadData =
    {
        args:{
            PageIndex:0,
            PageSize:10,
            ChannelID:"",   //流水号
            CardNoStart:"",     //支付状态
            CardNoEnd:"",
            CardStatus:"",
            UseStatus: "",
            Amount: "",
            DateRange: "",
            CreateTimeStart:"",
            CreateTimeEnd: "",
        },
        setCard:{
            CardIds:[],
            CardStatus:""
        },
        //获取渠道
        getSource: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetChannel'
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
        //获得充值卡列表
        getCardList: function (callback) {
             $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetCard',
                    'ChannelID':this.args.ChannelID,   //流水号
                    'CardNoStart':this.args.CardNoStart,     //支付状态
                    'CardNoEnd':this.args.CardNoEnd,
                    'CardStatus':this.args.CardStatus,
                    'UseStatus': this.args.UseStatus,
                    'Amount': this.args.Amount,
                    'DateRange': this.args.DateRange,
                    'CreateTimeStart': this.args.CreateTimeStart,
                    'CreateTimeEnd': this.args.CreateTimeEnd,
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
         //导出excel接口
        exportCard:function(){
            var that=this;
            
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'ExportCard',
                    'CardIDs':null,
                    'ChannelID':this.args.ChannelID,   //流水号
                    'CardNoStart':this.args.CardNoStart,     //支付状态
                    'CardNoEnd':this.args.CardNoEnd,
                    'CardStatus':this.args.CardStatus,
                    'UseStatus': this.args.UseStatus,
                    'Amount': this.args.Amount,
                    'DateRange': this.args.DateRange,
                    'CreateTimeStart': this.args.CreateTimeStart,
                    'CreateTimeEnd': this.args.CreateTimeEnd,
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
        //更新卡状态
        setCardStatus:function(callback){
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'SetCardStatus',
                    'CardIDs':this.setCard.CardIds,
                    'CardStatus':this.setCard.CardStatus
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