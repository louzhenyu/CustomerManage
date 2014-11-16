Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
    },
    initEvent:function(){
        var me=this;
    	//登录事件
    	$("#login").bind("tap",function(){
    		var number=$("#idNumber").val(),  //大使编号
    			pass=$("#password").val();
    		if(number.length==0){
    			 Jit.UI.Dialog({
	                'content': '大使编号不能为空!！',
	                'type': 'Alert',
	                'CallBackOk': function () {
	                    Jit.UI.Dialog('CLOSE');
	                }
	            });
	            return;
    		}
    		if(pass.length==0){
    			 Jit.UI.Dialog({
	                'content': '密码不能为空!！',
	                'type': 'Alert',
	                'CallBackOk': function () {
	                    Jit.UI.Dialog('CLOSE');
	                }
	            });
	            return;
    		}
            me.loginSubmit(number,pass);
    	});
       
    	
    },
    //获得二维码
    getErweima:function(){
        var href=location.href;
        var str=location.href.substring(0,href.indexOf("/HtmlApps"));
        var customerId=Jit.AM.getBaseAjaxParam().customerId;
        console.log(str+"/HtmlApps/html/special/huashuo/Register.html?customerId="+customerId+"&vipName="+this.getParams("vipName")+"&code="+code);
    	var vipName=this.getParams("vipName"),
            code=this.getParams("code");
        this.ajax({
            url:"/Project/Asus/AsusHandler.ashx",
            data:{
                "action":"GenerateQR",
                 generater:false,    //是否生成
                 url:str+"/HtmlApps/html/special/huashuo/Register.html?customerId="+customerId+"&vipName="+vipName+"&code="+code
            },
            success:function(data){
                console.log(data);
                if(data.code=="200"){
                    if(data.content&&data.content.vipList){
                        $("#second").attr("src",data.content.vipList[0].QRCode);
                    }
                }else{
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });
    },
    initPage:function(){
        this.getErweima();
    }
});