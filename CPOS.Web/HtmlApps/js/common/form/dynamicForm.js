Jit.AM.defindPage({
    elems:{
    	contact:$("#contact"),
        phone:$("#phone"),
        btnGetCode : $("#btnGetCode"),
        authCode : $("#authCode"),
        submit:$("#submit"),
        
        
        registerFormArea : $("#registerFormArea"),
        registerForm : $("#registerForm")
        
    },
    hideMask:function(){
        $('#masklayer').hide();
    },
    initWithParam:function(param){
    	this.param=param;
    },
    onPageLoad: function () {
        var that=this;
        
        this.objectId = JitPage.getUrlParam('objectId');
        if(!this.objectId){
        	this.alert("未获取到objectId");
        }
        
        this.loadData();
        this.initEvent();
        
    },
    loadData:function(){
    	this.getRegisterForm();
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
				theme : 'android-ics light', //皮肤样式
				display : 'modal', //显示方式
				mode : 'scroller', //日期选择模式
				lang : 'zh',
				startYear : 1900, //开始年份
				endYear : currYear + 10, //结束年份,
				CallBack : function() {
	
				}
			};
		$("#txtDate").mobiscroll().date($.extend(opt['date'], opt['default']),opt['default']);
	},
	showContact:function(){
		var self = this;
		$("#mask").show();
		//this.hideForm();
		this.elems.contact.show().addClass("pullDownState");
		this.elems.contact.bind("webkitAnimationEnd", function(){ //动画结束时事件
			self.elems.contact.removeClass("pullDownState");
		});
		
	},
	hideContact:function(){
		var self =this;
		this.elems.contact.attr('class','container pullUpState');
		this.elems.contact.bind("webkitAnimationEnd", function(){ //动画结束时事件
			self.elems.contact.hide().removeClass("pullUpState");
			$("#mask").hide();
			self.showForm();
		});
	},
	showForm:function(){
		var self =this;
		this.elems.registerFormArea.show().addClass("pullDown");
		this.elems.registerFormArea.bind("webkitAnimationEnd", function(){ //动画结束时事件
			self.elems.registerFormArea.removeClass("pullDown");
		});
	},
	hideForm:function(callback){
		var self =this;
		this.elems.registerFormArea.addClass("pullUp");
		this.elems.registerFormArea.bind("webkitAnimationEnd", function(){ //动画结束时事件
			self.elems.registerFormArea.hide().removeClass("pullUp");
			if(callback){
				callback();
			}
		});
	},
    initEvent:function(){
        var that=this;
        //取消领取
		$("#close").bind(this.eventType,function(){
            var $this=$(this);
			that.hideContact();
        });
        
        $("#closeBtn").bind(this.eventType,function(){
           	that.hideForm();
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
 		
		//手机号验证码登陆
        this.elems.submit.bind(this.eventType,function(){
        	
            var $this=$(this);
            if(that.elems.phone.val()==""){
            	that.alert("请输入手机号");
                return;
            }
            if(that.elems.authCode.val()==""){
            	that.alert("请输入验证码");
                return;
            }
            $this.val("提交中..");
            that.LoginUseMobile();
        });
    },
    LoginUseMobile:function(){
        
        var self = this;
        this.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.AuthCodeLogin',
                'Mobile': this.elems.phone.val(),
                'AuthCode':this.elems.authCode.val(),
                'VipSource':3
            },
            beforeSend:function(){
            	Jit.UI.Loading(true);
            },
            success: function (data) {
                if(data.IsSuccess){
                    var baseInfo = Jit.AM.getBaseAjaxParam();
                    baseInfo.userId = data.Data.MemberInfo.VipID;
                    Jit.AM.setBaseAjaxParam(baseInfo);
                    
                                    	
            		self.hideContact();

                }else{
                	self.alert(data.Message);
                }
            },
            complete:function(){
            	Jit.UI.Loading(false);
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
    			$(id).html("<font size='3'>"+count+"秒</font>后发送");
    		}else{
    			clearInterval(that.timerId);
    			$(id).html("发送验证码");
    			//表示已经发送   1分钟后可以再次发送
            	that.authCode=true;
            	$(id).css({"box-shadow":"0 2px 0 #f60","background": "#ff8331"});
    		}
    	},1000);
    },
    //获取验证码
    getAuthCode:function(){
        var self = this;
		if(this.elems.phone.val()==""){
        	this.alert("请输入手机号");
            return;
        }else if(!Jit.valid.isPhoneNumber(this.elems.phone.val())){
        	this.alert("请输正确的入手机号");
            return;
        }
        this.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.GetAuthCode',
                'Mobile': self.elems.phone.val(),
                'VipSource':3
            },
            beforeSend:function(){
        		Jit.UI.Loading(true);
            },
            success: function (data) {
            	if(data.IsSuccess){
            		//表示已经发送   1分钟后可以再次发送
                	self.authCode=false;
                	$("#btnGetCode").css({"box-shadow":"0 2px 0 gray","background": "gray"});
                	//需要添加60秒倒计时代码
        			self.showTimer("#btnGetCode");
            	}else{
            		self.alert(data.Message);
            	}
                
            },
            complete:function(){
            	Jit.UI.Loading(false);
            }
        });
    },

    //动态用户注册表单项
    PageFormData:null,
    //获取注册表单项
    getRegisterForm:function(){

        var self = this;

        if(self.PageFormData){
			
            self.buildRegisterForm(self.PageFormData);

            return;
        }

        this.ajax({
            url: '/ApplicationInterface/Vip/VipGateway.ashx',
            data: {
                'action': 'GetEvevtFormItems',
                'ObjectId':this.objectId
            },
            success: function (data) {
                Jit.UI.Loading(false);

                if(data.ResultCode == 0 && data.Data.Pages[0]){

                    self.PageFormData = data.Data.Pages[0].Blocks[0].PropertyDefineInfos;

                    self.buildRegisterForm(self.PageFormData);
                    
                    if(!!data.Data.Pages[0].ValidFlag){
                    	self.showContact();
                    }else{
                    	self.showForm();
                    }
                }else{
                	self.alert("未获得注册表单!");
                	return false;
                }
            },
            complete:function(){
            	self.hideMask();
            }
        });
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
			if(item.ControlInfo.ControlType==4){
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
    //提示内容
    alert:function(content){
    	Jit.UI.Dialog({
            'content':content+"",
            'type': 'Alert',
            'CallBackOk': function () {
                Jit.UI.Dialog('CLOSE');
            }
        });
    	return false;
    },
    //保存信息
    saveVipInfo:function(){
		
        var self = this;

        var vipinfo = [];
        var inputDom=$('[name=vipinfo]');
        inputDom.each(function(i,dom){
        	var $dom=$(dom);
        	var dataText=$dom.attr("data-text");
        	if($dom.val()==""){
        		self.alert(dataText+"不能为空!");
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
	        self.ajax({
	            url: '/ApplicationInterface/Vip/VipGateway.ashx',
	            data: {
	                'action': 'SetEventFormItems',
	                'ItemList':vipinfo,
                	'ObjectId':this.objectId,
	                'VipSource':3
	            },
	            beforeSend:function(){
	            	Jit.UI.Loading(true);
	            },
	            success: function (data) {
	                if(data.ResultCode == 0){
	                	self.hideForm(function(){
	                		
	                		//若带 跳转页面，跳转到响应页面，带上之前的参数，若没有，返回上级页面；
	                		var toPage = JitPage.getUrlParam('toPage');
	                		debugger;
	                		if(toPage){
	                			self.toPageWithParam(toPage);
	                		}else{
	                			self.pageBack();
	                		}
	                	});
	                }else{
	                	self.alert(data.Message);
	                }
	            },
	            complete:function(){
	                Jit.UI.Loading(false);
	            }
	        });
	   }
    }
});