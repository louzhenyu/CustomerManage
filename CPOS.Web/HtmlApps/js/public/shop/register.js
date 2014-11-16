Jit.AM.defindPage({

	name: 'Register',

	onPageLoad: function() {
		var me = this;
		me.loadPageData();
	},
	loadPageData: function() {
	
		var me = this;
		
		me.getIsRegistered();
		
		me.initEvent();
	},
	initEvent: function() {
		var me = this,
			REGMOBILE = /1[3-8]+\d{9}/,
			name,
			mobile,
			code,
			$codebtn = $('.codebtn'),
			$btn_register = $('.btn_register');
		
		$('.codebtn').bind('click', function() {

			if(me.lock){

				Jit.UI.Dialog({
					'content': '不能重复获取验证码！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});

				return;
			}

			var mobile = $('#user_phone').val();

			if (!mobile) {
				return Jit.UI.Dialog({
					'content': '请输入手机号码！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			} else if (!REGMOBILE.test(mobile)) {
				return Jit.UI.Dialog({
					'content': '手机号码有误！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			}
			me.getVcode(mobile);
		});

		$('.btn_register').bind('click', function() {
			var mobile = $('#user_phone').val(),
				name = Jit.trim($('#user_name').val()),
				code = $('#vcode').val();
			if (!name) {
				Jit.UI.Dialog({
					'content': '请输入名字！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			} else if (name.length < 2) {
				Jit.UI.Dialog({
					'content': '输入的名字过短！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			if (!mobile) {
				return Jit.UI.Dialog({
					'content': '请输入手机号码！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			} else if (!REGMOBILE.test(mobile)) {
				return Jit.UI.Dialog({
					'content': '手机号码有误！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			}
			if (!code || code == "0") {
				return Jit.UI.Dialog({
					'content': '请输入验证码！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			}

			//发送注册请求
			me.submitRequest(name, mobile, code);


		});
	},
	getVcode: function(mobile) {
		var me = this;
		me.lock = true;
		me.ajax({
			url: '/lj/Interface/RegisterData.aspx',
			data: {
				'action': 'sendCode',
				'mobile': mobile,
			},
			success: function(data) {
				if (data.code == 200) {
					//console.log(data);
					me.countDown();
				}
			}
		});
	},
	countDown: function() {

		var btncode = $('.codebtn');

		btncode.addClass('unenable');

		var me = this;

		me.timeNum = 60;

		me.getCodeOnOff = false;

		me.timer = setInterval(function() {

			if (me.timeNum > 0) {

				btncode.html(me.timeNum + '秒后重新获取');

				me.timeNum--;

			} else {

				me.lock = false;
				
				me.getCodeOnOff = true;

				btncode.html('获取验证码');

				btncode.removeClass('unenable');

				clearTimeout(me.timer);

				me.timer = null;
			}

		}, 1000);

	},
	submitRequest: function(name, mobile, code) {
		var me = this;
		me.ajax({
			url: '/lj/Interface/RegisterData.aspx',
			data: {
				'action': 'register',
				'name': name,
				'mobile': mobile,
				'code': code
			},
			success: function(data) {

				if (data.code == 200) {

					return Jit.UI.Dialog({
						'content': '您已注册成为"泸州老窖VIP俱乐部"会员',
						'type': 'Alert',
						'CallBackOk': function() {
							Jit.UI.Dialog('CLOSE');
							/*
							var isGive=me.getUrlParam('give');
							
							if (isGive) {//跳转至兑换积分列表页面
								me.toPage('GoodsJiFenDetail');//JiFenShop
							}else{
								me.toPage('JiFenShop');//GoodsDetailJifen //GoodsList 
							}
							*/
							me.pageBack();
							
							//location.href = location.host + Jit.AM.getUrlParam('source');
						}
					});


				} else {

					return Jit.UI.Dialog({
						'content': data.description,
						'type': 'Alert',
						'CallBackOk': function() {
							Jit.UI.Dialog('CLOSE');
						}
					});

				}
			}
		});
	},
	getIsRegistered : function(){
	
		var me = this;
		
		me.ajax({
			url: '/lj/Interface/RegisterData.aspx',
			data: {
				'action': 'getIsRegistered',
			},
			success: function(data) {
			
				if (data.code == "200") {
				
                    if (data.content.IsRegistered == "2") {
                        
						var info = Jit.AM.getBaseAjaxParam();
						
						Jit.AM.setAppSession('FirstAuth','',null);
						
						location.href = '/lj/RegisterDone.html?' 
									  + 'openId=' + info.openId 
									  + '&customerId=' + info.customerId
									  + '&userId=' + info.userId;
						
                    }
					
                } else {
				
                    //alert(data.description);
                }
			}
		});
	}
});