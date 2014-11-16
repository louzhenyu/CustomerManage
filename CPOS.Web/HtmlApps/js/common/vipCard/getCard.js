Jit.AM.defindPage({
    elems:{},
    onPageLoad: function () {
        var that=this;
        this.initPage();
        this.initEvent();
    },
    initWithParam:function(param){
    	this.param=param;
        if(!!!param.vipCardImg){
            param.vipCardImg="../../../images/common/vipCard/defaultCard.png";
        }
        //根据config中参数设置客户的会员卡图片
        $('[name=vipCardImg]').attr('src',param.vipCardImg);
        var title=document.title;
        var shareLink=location.href.replace("&userId","&temp").replace("&openId","&temp2");
		Jit.WX.shareTimeline(title,"会员卡",shareLink,param.vipCardImg);
        Jit.WX.shareFriends(title,"会员卡",shareLink,param.vipCardImg);
        if (param.infoColor) {$('.vipClipBanner .infoWrap .card').css('color',param.infoColor);};
        var htmlstr="";
        var links=[
			{
				'title':'会员权益',
				'toUrl':"javascript:Jit.AM.toPage('VipBenefits');"
			},
			{
				'title':'我的订单',
				'toUrl':"javascript:Jit.AM.toPage('MyOrder');"
			},
			{
				'title':'门店列表',
				'toUrl':"javascript:Jit.AM.toPage('StoreList');"
			},
			{
				'title':'账号绑定',
				'toUrl':"javascript:JitPage.showBindAccountPanel()",
				'text':"未绑定"
			}
		];

		var links2=[
				{
					'title':'门店列表',
					'toUrl':"javascript:Jit.AM.toPage('StoreList');"
				},
				{
					'title':'会员权益',
					'toUrl':"javascript:Jit.AM.toPage('VipBenefits');"
				}
			];
		if(!!!param.links){
			param.links=links;  //则用默认的
		}
		if(!!!param.links2){
			param.links2=links2;  //则用默认的
		}
        //动态把内容加载出来
        for(var i in param.links){
			var item=param.links[i];
            htmlstr += template.render('tpl_toItem',item);
        }
        $("#toItems").html(htmlstr);
        var htmlstr2="";
        //动态把内容加载出来
        for(var i in param.links2){
			var item=param.links2[i];
            htmlstr2 += template.render('tpl_item',item);
        }
        $("#itemList").html(htmlstr2);
    },
    hideMask:function(){

        $('#masklayer').hide();
    },
    initPage: function () {
        //确认领取
        this.elems.sureGet=$("#getCard");
        this.elems.openLetter=$("#open");
        this.elems.closeLetter=$("#close");
        this.elems.submit=$("#submit");
        //this.elems.name=$("#name");
		this.elems.hytq=$("#hytq");
		this.elems.mdlb=$("#mdlb");
        this.elems.phone=$("#phone");
        this.elems.authCode = $("#authCode");

        this.elems.contact=$("#contact");
		//this.elems.inputBox= $("#contact .userinfo input");
        this.elems.letter=$("#letter");
        this.elems.content=$("#content");
        this.elems.registerForm = $("#registerForm");
        this.elems.registerFormArea = $("#registerFormArea");

        this.elems.btnGetCode = $("#btnGetCode");
        this.elems.vipCardPage = $('#vipCardPage');
        this.elems.getVipCardPage = $('#getVipCardPage');
        this.elems.coupon=$("#coupon");//我的券包
        this.elems.myScore=$("#myScore");//个人积分
        this.elems.balance=$("#balance");//账户余额
        this.elems.closeBind=$("#closeBind");
        //关闭注册表单
        this.elems.closeReg=$("#closeBtn");

        this.checkIsRegister();
    },
    DateTimeEvent : function() {
		var self = this, currYear = (new Date()).getFullYear(), opt = {};
			opt.date = {
				preset : 'date'
			};
	
			//opt.datetime = { preset : 'datetime', minDate: new Date(2012,3,10,9,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
			opt.datetime = {
				preset : 'datetime'
			};
			opt.time = {
				preset : 'time'
			};
			opt["default"] = {
				theme : "android-ics light"/*'android-ics light'*/, //皮肤样式
				display : 'modal', //显示方式
				mode : 'scroller', //日期选择模式
				lang : 'zh',
				startYear : 1900, //开始年份
				endYear : currYear + 10, //结束年份,
				CallBack : function() {
	
				}
			};

		$("#txtDate").scroller('destroy').scroller($.extend(opt['date'], opt['default']),opt['default']);
	},
    initEvent:function(){
    	
        var that=this;
        //跳转页面事件
        $("#gotoPage").delegate("div",this.eventType,function(){
        	var $this=$(this);
        	var num=$this.find(".num").html();
        	var money=$("[info=vip_balance]").html();
        	var href=$this.attr("data-href");
        	var args="";
        	if(href=="MyScore"){
        		args+="num="+num;
        	}
        	if(href=="Balance"){
        		args+="money="+money;
        	}
        	Jit.AM.toPage(href,args);
        });
        //点击领取事件
        this.elems.sureGet.bind(this.eventType,function(){
            var $this=$(this);
            $(this).css("background","rgba(0,0,0,0)");
            $(this).find("span").removeClass("receiveBtnShow").addClass("receiveBtnHide");
			$("#mask").show();
			$("#contact").show().addClass("pullDownState popup-modify");
			$("#contact").bind("webkitAnimationEnd", function(){ //动画结束时事件
				$("#contact").removeClass("pullDownState");
			});
            //设置可以使用
            that.canUse();
			
        });
        //开始领取
        this.elems.openLetter.bind(this.eventType,function(){
            var $this=$(this);
			//wlong
			$("div#content").addClass('open');
            //location.href=$this.attr("data-href");
        });
        //开始领取
        this.elems.closeBind.bind(this.eventType,function(){
            $("#mask").hide();
            
            $('#bindAccountPage').hide();
        });
        //取消领取
		this.elems.closeLetter.bind(this.eventType,function(){
            var $this=$(this);
            $("#getCard").css({"background-color":"",opacity:1});
           $("#getCard").css("background-image","-webkit-gradient(linear, left top, left bottom, color-stop(0, #666), color-stop(1, #333))");
            $("#getCard").find("span").removeClass("receiveBtnHide").addClass("receiveBtnShow");
            that.elems.contact.removeClass("letterScale").addClass("letterScale2");
			//wlong
			$("#contact").attr('class','container popup-modify letterScale pullUpState');
			setTimeout(function(){
				$("#mask").hide();
				$("#contact").attr('class','container popup-modify');
				$("#contact").hide();
				//让其他注册内容显示
				$(".lastShow").hide();
				//重新渲染内容
				that.buildRegisterForm(that.PageFormData);
			},500);
        });
        //获取验证码
        this.elems.btnGetCode.bind(this.eventType,function(){
        	if(that.authCode==undefined||that.authCode){
            	that.getAuthCode();
            }
        });
         //获取验证码
        $("body").delegate(".focus_in",this.eventType,function(){
        	var tdom=$(this);
        	setTimeout(function(){
        		tdom.focus();
        	},500);
        });
        $("body").delegate('.togo',this.eventType,function(){  //添加点击效果
        	var $this=$(this);
        	var parent=$this.parent();
        	parent.addClass("on");  //添加点击效果
        	that.timerID=setTimeout(function(){
        		parent.removeClass("on");
        		clearTimeout(that.timerID);
        		location.href=$this.attr("$data-href");
        		
        	},200);
        });
 		//获取验证码
        this.elems.closeReg.bind(this.eventType,function(){
            //隐藏注册会员界面
            that.elems.registerFormArea.hide();
            //关闭信封动画
            that.closeLetterAnimate();
        });
		//手机号验证码登陆
        this.elems.submit.bind(this.eventType,function(){
        	
            var $this=$(this);

            if(that.elems.phone.val()==""){
                Jit.UI.Dialog({
	                'content':"请输入手机号",
	                'type': 'Alert',
	                'CallBackOk': function () {
	                    Jit.UI.Dialog('CLOSE');
	                }
	            });
                return;
            }
            if(that.elems.phone.val().length!=11){
                that.showTips("请输入正确的手机号!");
                return;
            }
            if(isNaN(that.elems.phone.val())){
                that.showTips("请输入正确的手机号!");
                return;
            }
            var value=that.elems.phone.val();
            if(!(/^13\d{9}$/g.test(value)||(/^15\d{9}$/g.test(value))||(/^18[0-9]\d{8}$/g.test(value)))){
                that.showTips("手机号格式不正确!");
                return false;
            };
            if(that.elems.authCode.val()==""){
                 Jit.UI.Dialog({
	                'content':"请输入验证码",
	                'type': 'Alert',
	                'CallBackOk': function () {
	                    Jit.UI.Dialog('CLOSE');
	                }
	            });
                return;
            }
            $this.val("提交中..");
            that.LoginUseMobile();
            
        });
    },
    //关闭信封动画
    closeLetterAnimate:function(){
    	var that=this;
    	//location.href=this.elems.closeLetter.attr("data-href");
    	setTimeout(function(){
        	$("#mask").hide();
        	that.elems.sureGet.find("span").removeClass("receiveBtnHide").addClass("receiveBtnShow");
        	that.elems.sureGet.css({"background-image":"-webkit-gradient(linear, left top, left bottom, color-stop(0, #666), color-stop(1, #333))",
        		"background-color":""
        	});
        },2000);
    },
    showBlockPage:function(name){

        $('[blockPage=true]').hide();

        $('#'+name).show();
    },
    //判断是否是注册会员
    checkIsRegister:function(){
        var me = this;
		//this.getVipBenefits();
        //Jit.UI.Loading(true);
        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.GetMemberInfo',
                'VipSource':3
            },
            success: function (data) {
                me.hideMask();
                ////用户不存在,显示领卡界面
                if(data.ResultCode == 330 || (data.Data && data.Data.MemberInfo && data.Data.MemberInfo.Status==1)){
                    me.elems.getVipCardPage.show();
					$("#getCard").css({"background-color":"",opacity:1});
		            $("#getCard").css("background-image","-webkit-gradient(linear, left top, left bottom, color-stop(0, #666), color-stop(1, #333))");
		            $("#getCard").find("span").removeClass("receiveBtnHide").addClass("receiveBtnShow");
		            me.elems.contact.removeClass("letterScale").addClass("letterScale2");
					//wlong
					$("#contact").attr('class','container letterScale pullUpState');
					$("#mask").hide();
					$("#contact").hide();
					$("#contact").attr('class','container');
                    //加载注册表单项数据
                    me.getRegisterForm();
                    var param=me.param;
                    if(param.visible&&param.visible.length){
                    	$("#menuItems").show();
			    		for(var i=0;i<(param.visible.length||0);i++){
				    		var item=param.visible[i];
				    		$("#"+item).show();
				    	}
			    	}else{  //默认没配置则都显示出来
			    		$("#menuItems").show();
			    		$("#menuItems li").show();
			    	}

                }else if(data.ResultCode == 0 && data.Data && data.Data.MemberInfo.Status==2){
                      me.elems.getVipCardPage.hide();
                      $("#menuItems").hide();
                    //用户已注册,进入会员卡中心
                    me.elems.vipCardPage.show();
					if(data.Data.MemberInfo.IsActivate){  //表示已绑定
						$("#status").html("已绑定");
					}
                    //更新会员卡中心用户信息
                    me.updateVipCardPageInfo(data.Data.MemberInfo);
                }
            }
        });
    },
    //倒计时显示
    showTimer:function(id){
    	var count=60;
    	var that=this;
    	this.timerId=setInterval(function(){
    		if(count>0){
    			--count;
    			$(id).html("<font size='1'>"+count+"秒</font>后发送");
    		}else{
    			clearInterval(that.timerId);
    			$(id).html("发送验证码");
    			//表示已经发送   1分钟后可以再次发送
            	that.authCode=true;
            	$(id).css({"color": "#fe7c23"});
    		}
    	},1000);
    },
    //获取验证码
    getAuthCode:function(){
        if(this.canGetAuthCode=="no"){
            return;
        }
		if(this.elems.phone.val().length!=11){
			this.showTips("请输入正确的手机号!");
			return;
		}
        var me = this;

        Jit.UI.Loading(true);

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.GetAuthCode',
                'Mobile': me.elems.phone.val(),
                'VipSource':3
            },
            success: function (data) {
                Jit.UI.Loading(false);
                if(data.ResultCode==310){
                	 Jit.UI.Dialog({
		                'content': data.Message,
		                'type': 'Alert',
		                'CallBackOk': function () {
		                    Jit.UI.Dialog('CLOSE');
		                }
		            });
                }else{
                	//表示已经发送   1分钟后可以再次发送
                	me.authCode=false;
                	$("#btnGetCode").css({"color": "gray"});
        			me.showTimer("#btnGetCode");
                }
                //需要添加60秒倒计时代码
                
            }
        });
    },
    //手机号验证码登录
    LoginUseMobile:function(){
        
        var me = this;
         if(!me.PageFormData){
            me.showTips("未获得注册表单,请联系管理员!");
            return;
        }
        Jit.UI.Loading(true);

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.AuthCodeLogin',
                'Mobile': me.elems.phone.val(),
                'AuthCode':me.elems.authCode.val(),
                'VipSource':3
            },
            success: function (data) {

                Jit.UI.Loading(false);
                me.elems.submit.val("确定");  //还原确定按钮的文字

                if(data.ResultCode == 0){
                    if(data.Data.MemberInfo.Status == 2){
                        //已填写过注册表单，直接进入会员卡中心
                        me.elems.vipCardPage.show();
                        //更新会员卡中心用户信息
                        me.updateVipCardPageInfo(data.Data.MemberInfo);
                    }else{
                        //设置不可点击
                        me.notCanUse();
                        

                        var baseInfo = Jit.AM.getBaseAjaxParam();

                        baseInfo.userId = data.Data.MemberInfo.VipID;

                        Jit.AM.setBaseAjaxParam(baseInfo);
                        //显示注册内容
                        me.showRegister(); 
                    }
               // });
                }else{
                	Jit.UI.Dialog({
		                'content': data.Message,
		                'type': 'Alert',
		                'CallBackOk': function () {
		                    Jit.UI.Dialog('CLOSE');
		                }
		            });
                }
            }
        });
    },
    //显示注册内容
    showRegister:function(){
        //显示注册内容
        $("#contact .popup-modify-wrap").css({  
           "overflow-y": "scroll",
           "height": "400px"
        });
        //让其他注册内容显示
        $(".lastShow").show();
    },
    //正常出现输入内容的时候   设置不可用
	notCanUse:function(){
        this.elems.phone.attr("disabled","disabled");
        this.elems.authCode.attr("disabled","disabled");
        this.canGetAuthCode="no";
        this.elems.submit.attr("disabled","disabled");
        //默认背景色 fe7c23
        this.elems.submit.css("background-color","gray");
    },
    //设置可以正常使用
    canUse:function(){
        this.elems.phone.removeAttr("disabled");
        this.elems.authCode.removeAttr("disabled");
        this.canGetAuthCode="yes";
        this.elems.submit.removeAttr("disabled");
         //默认背景色 fe7c23
        this.elems.submit.css("background-color","#fe7c23");
    },
    playLoginSuccessAnimal:function(callback){
        
        var me = this;

		me.elems.submit.val("成功领取");
		me.elems.contact.hide();
		$(".myCard").show().addClass("moveCenter");
		setTimeout(function(){
			$(".myCard").addClass("moveTop");
			setTimeout(function(){
				//$(".vipClipBanner").find("img").remove();
				//$(".vipClipBanner") .append($(".myCard").find("img").clone());
				$("#mask").hide();
				$(".myCard").hide();
				if(typeof callback == 'function'){
					callback();
				}
			},1000);

		},1000);
    },


    //动态用户注册表单项
    PageFormData:null,
    //获取注册表单项
    getRegisterForm:function(){
        var me = this;
        if(me.PageFormData){
            me.buildRegisterForm(me.PageFormData);
            return;
        }

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Register.GetRegisterFormItems',
                'EventCode':'OnLine005'
            },
            success: function (data) {
                Jit.UI.Loading(false);
                if(data.ResultCode == 0 && data.Data.Pages[0]){
                    me.PageFormData = data.Data.Pages[0].Blocks[0].PropertyDefineInfos;
                    me.buildRegisterForm(me.PageFormData);
                }else{
                	me.showTips("未获得注册表单!");
                	return false;
                }
            }
        });
    },

    updateVipCardPageInfo: function(info){

        $('[info=vip_name]').html(info.VipRealName||info.VipName);
        var vipNo=info.VipNo;
        if(vipNo){
        	vipNo=vipNo.replace("Vip","");
        }
        $('[info=vip_phone]').html(vipNo||'暂无卡号');
        $('[info=vip_balance]').html(info.Balance);
        $('[info=vip_coupon]').html(info.CouponsCount);
        $('[info=vip_integration]').html(info.Integration);
        $('[info=vip_level]').html(info.VipLevelName?info.VipLevelName:"注册会员");
    },

    buildRegisterForm:function(items){

        var htmlstr = '';
		/*
        items = items.sort(function(A,B){
            if(A.DisplayIndex>B.A){
                return 1;
            }else{
                return -1;
            }
        });
		*/
		var isDate=false;
        for(var i in items){
			var item=items[i];
			//表示的是日期类型
			if(item.ControlInfo.DisplayType==6){
				isDate=true;
			}
            htmlstr += template.render('tpl_block_item',items[i]);
        }

        this.elems.registerForm.html(htmlstr);
        if(isDate){
        	//日期类型
	        this.elems.txtDate = $('#txtDate');
			//this.elements.txtTime = $('#txtTime');
			this.DateTimeEvent();//初始化
        }

    },
    //信息验证
    validate:function(name){
    	var flag=false;
    },
    //提示内容
    showTips:function(content){
    	Jit.UI.Dialog({
            'content':content+"",
            'type': 'Alert',
            'CallBackOk': function () {
                Jit.UI.Dialog('CLOSE');
            }
        });
    	return false;
    },
    //验证每个表单项
    myValidate:function(value,dataType,dataText){
        var that=this;
        if(value==""&&(dataType!=5)){
            that.showTips(dataText+"不能为空!");
            return true;
        }
        //数值类型验证
        if(dataType==2){
            if(!/^(\-?)(\d+)$/.test(value)){
                that.showTips(dataText+"只能输入数字!");
                return true;
            };
        }
        //手机号验证
        if(dataType==3){
            if(!(/^13\d{9}$/g.test(value)||(/^15\d{9}$/g.test(value))||(/^18[8,9]\d{8}$/g.test(value)))){
                that.showTips("手机号格式不正确!");
                return true;
            };
        }
        //邮件验证
        if(dataType==4){
            if(!/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/.test(value)){
                that.showTips("邮箱格式不正确!");
                return true;
            };
        }
        return false;
    },
    //提交的内容验证
    contentVal:function($dom){
    	var that=this;
    	var value=$dom.val();
    	//内容  如  姓名      
    	var dataText=$dom.attr("data-text"),
    		dataType=$dom.attr("data-type"),      
    		isMustDo=$dom.attr("data-isMustDo");  //是否必填
        if(value!=""){   //不为空则进行验证
        	return that.myValidate(value,dataType,dataText);
        }else{
            if(isMustDo==1){  //为空的时候必填的必须要判断
                return that.myValidate(value,dataType,dataText);
            }else{
                return false;
            }
        }
    },
    //保存信息
    saveVipInfo:function(){
		
        var me = this;

        var vipinfo = [];
        var inputDom=$('[name=vipinfo]');
        inputDom.each(function(i,dom){
        	var $dom=$(dom);
        	var dataText=$dom.attr("data-text");
        	if(me.contentVal($dom)){  //进行每个验证
        		return false;
        	}else{
        		vipinfo.push({
	                'ID':$dom.attr('wid'),
	                'IsMustDo':false,
	                'Value':$dom.val()
            	});
        	}
            
        });
		if(inputDom&&(inputDom.length==vipinfo.length)){
	        Jit.UI.Loading(true);
	
	        me.ajax({
	            url: '/ApplicationInterface/Gateway.ashx',
	            data: {
	                'action': 'VIP.Register.SetRegisterFormItems',
	                'ItemList':vipinfo,
	                'VipSource':3
	            },
	            success: function (data) {
	                
	                Jit.UI.Loading(false);
					
	                if(data.ResultCode == 0){
	                	$("#menuItems").hide();
	                	me.elems.getVipCardPage.hide();
	                	//me.elems.registerFormArea.hide();
	                		//让其他注册内容显示
						$(".lastShow").hide();
						//让上面的部分不能点击
						$("#divMask").hide();
						//重新渲染内容
						me.buildRegisterForm(me.PageFormData);
						me.playLoginSuccessAnimal();
	                    //me.showBlockPage('vipCardPage');
	                    me.checkIsRegister();
	                }else{
	                	me.showTips(data.Message);
	                }
	            }
	        });
	   }
    },
    //显示绑定的界面
    showBindAccountPanel:function(){
    	if($("#status").html()!="已绑定"){
			$("#mask").show();
	 		$('#bindAccountPage').css("display","block");
	 	}
    },
    //获得绑定已有卡的验证码
    getBindAuthCode:function(){
		if(!$("#bindPhone").val().length){
            this.showTips("请输入手机号！");
            return false;
		}
		if(this.authCode==undefined||this.authCode){
            	
	        var me = this;
	
	        Jit.UI.Loading(true);
	
	        me.ajax({
	            url: '/ApplicationInterface/Gateway.ashx',
	            data: {
	                'action': 'VIP.Login.GetAuthCode',
	                'Mobile': $('#bindPhone').val()
	            },
	            success: function (data) {
	                Jit.UI.Loading(false);
	                me.authCode=false;
	                $("#getBindAuthCode").css({"background": "gray"});
	        		me.showTimer("#btnGetCode");
	                me.showTimer("#getBindAuthCode");
	            }
	        });
	    }
    },
    bindAccount:function(){

        var me = this;

        Jit.UI.Loading(true);

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Register.MergeVipInfo',
                'Mobile': $('#bindPhone').val(),
                'AuthCode':$('#bindAuthCode').val(),
                'VipSource':3
            },
            success: function (data) {
                
                Jit.UI.Loading(false);

                if(data.ResultCode == 0){
                    //绑定成功
                    me.checkIsRegister();
                     $("#mask").hide();
            		$('#bindAccountPage').hide();
            		$("#status").html("已绑定");
                }else{
                	 me.showTips(data.Message);
                }
            }
        });
    }
});