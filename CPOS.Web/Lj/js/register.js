function trim(s){
    if (!s || s == null || typeof(s) == "undefined") 
        return "";
    return ("" + s).replace(/^\s*|\s*$/g, "");
}

function isEmpty(s){
    return trim(s).length == 0
}

function isInt(s){
    if (isEmpty(s)) 
        return false;
    for (var i = 0; i < s.length; i++) {
        if ('1234567890'.indexOf(s.charAt(i)) < 0) {
            return false;
        }
    }
    return true;
}

function isPhoneNumber(str){

	var regAee  = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/;
	
	if(regAee.test(str)){
	
		return true;
		
	}else{
		
		return false;
	}
}

var Register = {
    getCodeOnOff: false,
    url: "/lj/Interface/RegisterData.aspx",
    init: function () {
        //S("openId", "oUcanjlz0pGMW57Xm50-uiCqkPIc");
        //S("userId", "8e64ec412d224c46a8ad80857c26e2eb");
        Lzlj.getIsRegistered(function () {
            if (Lzlj.isRegistered != "2") {
                Register.getCodeOnOff = true;
                $('.codebtn').bind('click', Register.getVerificationCode);
                $('.btn_register').bind('click', Register.doRegister);
            }
            else {
                //alert("您已注册，无需再次注册。");
                LinkAddCustomerId("RegisterDone.html");
            }
        });
    },
	getVerificationCode:function(){
		
		if(!Register.getCodeOnOff){
		
			return ;
		}
		
		var phoneNumber = $('#user_phone').val();

		if (!phoneNumber) {
			alert('请输入手机号码');
			
			return;
		}else if(!isPhoneNumber(phoneNumber)){
		
			alert('请输入正确的手机号');
			
			return;
		}


		
		var jsonarr = { 'action': "sendCode", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "mobile": phoneNumber } }) };

		$.ajax({
	        type: "get",
	        dataType: "json",
	        url: Register.url,
	        data: jsonarr,
	        beforeSend: function () {
	            Win.Loading();
	        },
	        success: function (data) {
			
				Win.Loading("CLOSE");
				
                if (data.code == "200") {
                    
					Register.countDown();
					
                } else {
				
                    alert(data.description);
                }
	        },
			
	        error: function (XMLHttpRequest, textStatus, errorThrown){
			
				Win.Loading("CLOSE");
				
	            alert(errorThrown);
	        }
	    });
	},
	countDown:function(){
	
		var btncode = $('.codebtn');
		
		btncode.addClass('unenable');
		
		var me = this;
		
		me.timeNum = 60;
		
		me.getCodeOnOff = false;
		
		me.timer = setInterval(function(){
		
			if(me.timeNum>0){
			
				btncode.html(me.timeNum+'秒后重新获取');
				
				me.timeNum--;
				
			}else{
				
				me.getCodeOnOff = true;
				
				btncode.html('获取验证码');
				
				btncode.removeClass('unenable');
				
				clearTimeout(me.timer);
				
				me.timer = null;
			}
			
		},1000);
		
	},
	doRegister:function(){
		
		var name = $('#user_name').val(),
			mobile = $('#user_phone').val(),
			code = $('#vcode').val();
			
		if(name == ""){
		
			alert("请填写名称");
			
		}else if(!isPhoneNumber(mobile)){
		
			alert('请输入正确的手机号');
			
		}else if(code == ""){
			
			alert('请输入验证码');
			
		}else{
		
			var jsonarr = { 
				'action': "register", 
				'ReqContent': JSON.stringify({ 
					'common': Base.All(), 
					'special': {	
						"name": name, 
						"mobile": mobile, 
						"code": code
					}
				})
			};
			
			$.ajax({
				type: "post",
				dataType: "json",
				url: Register.url,
				data: jsonarr,
				beforeSend: function () {
					Win.Loading();
				},
				success: function (data) {
				
					if(data.code == 200){
						
						var tourl = getParam("source");
						
						LinkAddCustomerId(tourl);
						
					}else{
						alert(data.description);
					}
				},
				error: function (XMLHttpRequest, textStatus, errorThrown) {
					alert(errorThrown);
				}
			});
		
		}
	}
}

$(document).ready(function () {
    if (location.pathname.indexOf("register.html") > 0)
        Register.init();
});