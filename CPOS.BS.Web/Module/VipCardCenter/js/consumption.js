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
                btnQueryVip:$("#queryVip"),                           //查询按钮
                popDiv:$("#popDiv"),                                   //弹出层
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            authCode:true,
            clearInput: function () {

            },
            init: function () {
                var that=this;
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
                this.loadData.getCardVip(function(data){
                    var list=data.Data.VipList;
                    list=list?list:[];
                    
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
	
                        },true);
               
                });
            },
             //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex=currentPage-1;
                this.loadData.getCardVip(function(data){
                   var list=data.Data.VipList;
                    list=list?list:[];
                    
                    var html=bd.template("tpl_content",{list:list})
                    $("#content").html(html);
                    $("#loading").hide();
                });
            },
            //倒计时显示
            showTimer:function(id){
    	        var count=60;
    	        var that=this;
    	        this.timerId=setInterval(function(){
    		        if(count>0){
    			        --count;
    			        $(id).val(count+"秒后发送");
                        $(id).css({"width":"80px","background":"gray"});
    		        }else{
    			        clearInterval(that.timerId);
    			        $(id).val("发送");
    			        //表示已经发送   1分钟后可以再次发送
            	        that.authCode=true;
            	        $(id).css({"background": "#5FAFE4","width":"70px"});
    		        }
    	        },1000);
            },
            initEvent: function () {
                //初始化事件集
                var that = this;
                //查询会员
                this.elems.btnQueryVip.click(function(e){
                    that.initTables();
                    that.stopBubble(e);
                });
                $("tbody").delegate(".btn-pay","click",function(e){
                    that.showElements(that.elems.popDiv);
                    debugger;
                    //保存每个会员信息
                    that.vipInfo=$(this).parent().parent().data("item");
                    that.stopBubble(e);
                });
                $("#btnSend").click(function(e){
                    var vipId=that.vipInfo.VIPID;
                    var amount=$("#amount").val();
                    if(isNaN(parseFloat(amount))){
                        alert("请输入数字金额");
                        return;
                    }
                    debugger;
                    that.loadData.args.VipID=vipId;
                    that.loadData.args.Amount=amount;
                    that.loadData.args.SMSCode=null;
                    //判断是否已经发送在时间内
                    if(that.authCode){
                        //发送短信
                        that.loadData.vipConsume(function(data){
                            that.authCode=false;
                            that.showTimer("#btnSend");
                        });
                        $("#btnSend").val("发送中..");
                    }
                    
                    that.stopBubble(e);
                });
                //确定消费
                  $("#btnSure").click(function(e){
                    var vipId=that.vipInfo.VIPID;
                    var amount=$("#amount").val();
                    if(isNaN(parseFloat(amount))){
                        alert("请输入数字金额");
                        return;
                    }
                    that.loadData.args.VipID=vipId;
                    that.loadData.args.Amount=amount;
                    var code=$("#code").val();
                    if(code.length==0){
                        alert("验证码不能为空!");
                        return;
                    }
                    that.loadData.args.SMSCode=code;
                    //确认消费
                    that.loadData.vipConsume(function(data){
                        if(data.IsSuccess){
                            alert("消费成功!");
                            that.initTables();
                        }
                        $(".hintClose").trigger("click");
                    });
                    
                    that.stopBubble(e);
                });
                //关闭弹出层
               $(".hintClose").bind("click",function(){
                      that.elems.uiMask.slideUp();
                      $(this).parent().parent().fadeOut();
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
         getCardVip: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetCardVip',
                    'PageSize':this.args.PageSize,
                    'PageIndex':this.args.PageIndex,
                    'Criterion':$("#name").val()   //手机号/身份证/会员名
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
        //消费
        vipConsume: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'VipConsume',
                    'VipID':this.args.VipID,                //会员ID
                    'Amount':this.args.Amount,              //消费金额
                    'SMSCode':this.args.SMSCode,            //验证码
                    'DocumentCode':this.args.DocumentCode   //原始凭证
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