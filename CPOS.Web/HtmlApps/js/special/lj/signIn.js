Jit.AM.defindPage({

	name:'FCsignIn',
	
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入signIn.....');
		
		this.eventId = this.getUrlParam("eventId")?this.getUrlParam("eventId"):"45486dd1c7a1de1231199eb41b04a669";
		if(!this.eventId){
			Jit.UI.Dialog({
				type : "Alert",
				content : "未取到eventId,取到的eventId为"+this.eventId,
				CallBackOk : function() {
					Jit.AM.toPage('FCsignIn');
				}
			});
		}
		this.initEvent();
	},
	
	initEvent:function(){
		var self = this;
		this.pass = false;
		this.ajaxSending = false;
		
		$("#phoneNo").bind("input",function(e){
			//if(e.target.value.length>11){
			//	e.target.value = e.target.value.substr(0,11);
			//}
			//if(self.validate(e.target.value)){
			//	Jit.log('&&&&通过验证');
			//	self.pass = true;
			//}else{
			//	if(e.target.value.length==11){
			//		Jit.log('&&&&验证不通过');
			//	}
			//	self.pass = false;
		    //}
		    if (e.target.value.length > 1) {
		        self.pass = true;
		    }
		});
		
		$("#submit").click(function(){
			if(self.pass){
				Jit.log("通过正则验证，号码为："+$("#phoneNo").val());
				self.submit($("#phoneNo").val());
			}else{
				Jit.UI.Dialog({
					type:"Alert",
					content:"手机号码格式不正确",
					CallBackOk:function(){
						Jit.UI.Dialog("CLOSE");
					}
				});
			}
		});
		
		this.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action': 'checkSign',
				'eventId':this.eventId
			},
			success:function(data){
				if(!data){
					Jit.log("服务器返回数据错误");
				}else{
					Jit.log("客户端已接收到数据");
					if(data.code==200){
						if(data.content.isSigned==1){
							Jit.UI.Dialog({
								type:"Alert",
								content:"您已签到！",
								CallBackOk:function(data){
									Jit.AM.toPage('FCindex');		
								}
							});
						}
					}
				}
			}
		});
	},
	submit:function(phoneNo){
		var self = this;
		
		if(!this.ajaxSending){
			this.ajax({
				url:'/module/eventcheckin.ashx',
				data:{
					'action': 'setSignUp',
					'phone':phoneNo,
					'eventId':this.eventId
				},
				beforeSend:function(){
					self.ajaxSending = true;
				},
				success:function(data){
					if(!data){
						//Jit.log("服务器返回数据错误");
						Jit.UI.Dialog({
							type:"Alert",
							content:"服务器返回数据错误！",
							CallBackOk:function(data){
								Jit.AM.toPage('FCindex');		
							}
						});
					}else{
						Jit.log("客户端已接收到数据");
						Jit.UI.Dialog({
							type:"Alert",
							content:data.description,
							CallBackOk:function(){
								if(data.code==200){
									Jit.AM.toPage('FCindex');		
								}else{
									Jit.UI.Dialog("CLOSE");
								}				
							}
						});
					}
				},
				error:function(){
					Jit.UI.Dialog({
						type:"Alert",
						content:"签到失败,请检查网络后重新签到",
						CallBackOk:function(data){
							Jit.UI.Dialog("CLOSE");
						}
					});
				},
				complete:function(){
					self.ajaxSending = false;
				}
			});
		}
		
	},
	validate:function(phoneNo){
		var str = phoneNo||"";
		var reg = /^0?(13[0-9]|15[012356789]|18[02356789]|14[57])[0-9]{8}$/;
		return reg.test(str);
	}
});