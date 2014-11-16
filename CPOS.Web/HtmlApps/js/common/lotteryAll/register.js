var register = {
	elems:{
		
        contact : $("#contact"),

        phone : $("#phone"),

        authCode : $("#authCode"),

        btnGetCode : $("#btnGetCode"),

        submit	:$("#submit"),

        registerForm : $("#registerForm"),

        registerFormArea : $("#registerFormArea")

	},
	init:function(){
		this.elems.contact.show().addClass("pullDownState");
		this.loadData();
		this.initEvent();
		
	},
	loadData:function(){
		
		this.getRegisterForm();
	},
	initEvent:function(){
    	var me  = this;
    	me.elems.btnGetCode.bind(JitPage.eventType,function(){

            if(me.authCode==undefined||me.authCode){

                me.getAuthCode();
            }
        });

        //手机号验证码注册登录
        me.elems.submit.bind(JitPage.eventType,function(){
            
            var $this=$(this);
            if(me.elems.phone.val()==""){
                Jit.UI.Dialog({
                    'content':"请输入手机号",
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return;
            }
            if(me.elems.authCode.val()==""){
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

            me.LoginUseMobile();
            
        });
    },
	buildRegisterForm:function(items){

        var htmlstr = '';
        
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
            
            this.DateTimeEvent();//初始化
        }
   },
   
    DateTimeEvent : function() {

        var self = this, currYear = (new Date()).getFullYear(), opt = {};

        opt.date = {

            preset : 'date'
        };

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
    
    LoginUseMobile:function(){
        
        var me = this;

        Jit.UI.Loading(true);

        JitPage.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Login.AuthCodeLogin',
                'Mobile': me.elems.phone.val(),
                'AuthCode':me.elems.authCode.val(),
                'VipSource':3
            },
            success: function (data) {

                Jit.UI.Loading(false);

                if(data.ResultCode == 0){

                    var baseInfo = Jit.AM.getBaseAjaxParam();

                    baseInfo.userId = data.Data.MemberInfo.VipID;

                    Jit.AM.setBaseAjaxParam(baseInfo);

                    //显示注册页面
                    $("#contact").attr('class','container letterScale pullUpState');

                    setTimeout(function(){

                        $("#contact").hide().attr('class','container');

                        me.elems.registerFormArea.show();

                    },1000);

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

    //保存信息
    saveVipInfo:function(){
        
        var me = this;

        var vipinfo = [];

        var inputDom=$('[name=vipinfo]');

        inputDom.each(function(i,dom){
            var $dom=$(dom);
            var dataText=$dom.attr("data-text");
            if($dom.val()==""){
                me.alert(dataText+"不能为空!");
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
                        
                        Jit.UI.Dialog({
                            'content': '您已成功注册成为会员！',
                            'type': 'Alert',
                            'LabelOk': '马上抽奖',
                            'CallBackOk': function () {

                                location.reload();

                                Jit.UI.Dialog('CLOSE');
                            }
                        });

                    }else{

                        me.alert(data.Message);
                    }
                }
            });
       }
    },
    
    //获取验证码
    getAuthCode:function(){

        var me = this;
		if(this.elems.phone.val()==""){
        	this.alert("请输入手机号");
            return;
        }else if(!Jit.valid.isPhoneNumber(this.elems.phone.val())){
        	this.alert("请输正确的入手机号");
            return;
        }
        Jit.UI.Loading(true);
        JitPage.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            type:"get",
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

                    $("#btnGetCode").css({"box-shadow":"0 2px 0 gray","background": "gray"});

                    me.showTimer("#btnGetCode");
                }
                //需要添加60秒倒计时代码
            }
        });
    },
    //倒计时显示
    showTimer:function(id){
        var count=60;
        var me=this;
        this.timerId=setInterval(function(){
            if(count>0){
                --count;
                $(id).html("<font size='3'>"+count+"秒</font>后发送");
            }else{
                clearInterval(me.timerId);
                $(id).html("发送验证码");
                //表示已经发送   1分钟后可以再次发送
                me.authCode=true;
                $(id).css({"box-shadow":"0 2px 0 #f60","background": "#ff8331"});
            }
        },1000);
    },
    getRegisterForm:function(){

        var me = this;

        Jit.UI.Loading(true);

        JitPage.ajax({
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

                    me.alert("未获得注册表单!");

                    return false;
                }
            }
        });
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
    }
    
};
