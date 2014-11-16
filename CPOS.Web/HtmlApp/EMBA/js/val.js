Jit.AM.defindPage({
    name: 'Val',
    onPageLoad: function() {
        //如果已经注册直接跳转到首页
        var me = Jit.AM,
            datas = {
                eventId: (me.getBaseAjaxParam()).eventId
            };
		
        FxLoading.AutoHide();
		
        this.initEvent();

        this.initData();
    },
    initData: function() {


    },
	getCode:function(){
		var me = this;
		var datas = {
			'mobile': $('#txtPhone').val(),
			'Action':'sendCode',
		}, regPhone = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/;
		if (!datas.mobile) {
			Tips({
				msg: '请输入您的手机号码.'
			});
			return false;
		} else if (!regPhone.test(datas.mobile)) {
			Tips({
				msg: '请输入正确的手机号码.'
			});
			return false;
		}
		
		me.ajax({
			url: '/OnlineShopping/data/Data.aspx?Action=setSignUpFosun',
			data: datas,
			success: function(data) {
				Tips({
					msg: data.description
				});
				if (data.code == 200) {
					me.toPage('HomePage');
				}
			},
			error: function() {
				Tips({
					msg: '服务器繁忙，请稍后.'
				});
			}

		});
	},
    initEvent: function() {
        var toVal = $('#toVal'),
            txtPhone = $('#txtPhone'),
            me = this;

        //手机验证
        toVal.bind('click', function() {
            var datas = {
                'phone': txtPhone.val(),
				'vipRealName':$('#username').val(),
				'school':$('#xx').val(),
				'className':$('#bj').val(),
				'company':$('#gs').val(),
				'position':$('#zw').val(),
				'email':$('#yx').val(),
				'hobby':$('#xqah').val(),
				'myValue':$('#tgjz').val(),
				'needValue':$('#hdjz').val(),
				'sinaMBlog':$('#wbzh').val(),
				'weixin':$('#wxzh').val(),
            }, regPhone = /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/;
            if (!datas.phone) {
                Tips({
                    msg: '请输入您的手机号码.'
                });
                return false;
            } else if (!regPhone.test(datas.phone)) {
                Tips({
                    msg: '请输入正确的手机号码.'
                });
                return false;
            }
			
            me.ajax({
                url: '/onlineshopping/data/emba.aspx?Action=updateVip',
                data: datas,
                success: function(data) {
                    Tips({
                        msg: data.description
                    });
                    if (data.code == 200) {
						
						var cfg = Jit.AM.getBaseAjaxParam();
						
						cfg.userId = data.content.VipId;
						
						Jit.AM.setBaseAjaxParam(cfg);
						
						me.setParams('IsRegister','1');
						
                        me.toPage('HomePage');
                    }
                },
                error: function() {
                    Tips({
                        msg: '服务器繁忙，请稍后.'
                    });
                }

            });

        });


    }
});