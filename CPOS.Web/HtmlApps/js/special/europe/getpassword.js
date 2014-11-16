Jit.AM.defindPage({

    name: 'GetPassword',
    elements: {
        txtMobile: '',
        btValCode: '',
        txtValCode: '',
        btSubmit: '',
        txtNewPassword: '',
        txtOnNewPassword: ''
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;

        self.elements.txtMobile = $('#txtMobile');
        self.elements.btValCode = $('#btValCode');
        self.elements.txtValCode = $('#txtValCode');
        self.elements.btSubmit = $('#btSubmit');
        self.elements.txtNewPassword = $('#txtNewPassword');
        self.elements.txtOnNewPassword = $('#txtOnNewPassword');



    }, //绑定事件
    initEvent: function() {
        var self = this;
        //提交找回密码
        self.elements.btSubmit.bind('click', function() {
            var mobile = self.elements.txtMobile.val(),
                valCode = self.elements.txtValCode.val(),
                newPassWord = self.elements.txtNewPassword.val(),
                onNewPassword = self.elements.txtOnNewPassword.val();
            if (!mobile) {
                return Jit.UI.Dialog({
                    'content': '请输入手机号码！',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            } else if (!IsMobileNumber(mobile)) {
                return Jit.UI.Dialog({
                    'content': '手机号码有误！',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            }
            if (!valCode) {
                return Jit.UI.Dialog({
                    'content': '请输入验证码！',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            }
            if (!newPassWord) {
                return Jit.UI.Dialog({
                    'content': '请输入您的新密码',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });


            };
            if (!onNewPassword) {
                return Jit.UI.Dialog({
                    'content': '请再次输入您的新密码',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            };

            if (newPassWord != onNewPassword) {
                return Jit.UI.Dialog({
                    'content': '两次密码不一致，请重新输入。',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            };

            self.ajax({
                url: '/Project/CEIBS/CEIBSHandler.ashx',
                data: {
                    'action': 'ResetPassword',
                    'name': name,
                    'mobile': mobile,
                    'code': valCode,
                    'roleid': 'CE1FF5BE7B6A4B0E8ACF5E2501B43D78',
                    'newPassWord': newPassWord
                },
                beforeSend: function() {
                    UIBase.loading.show();

                },
                success: function(data) {
                    UIBase.loading.hide();
                    if (data && data.code == 200) {
                      Jit.UI.Dialog({
                            'content':'重置成功',
                            'type': 'Alert',
                            'CallBackOk': function() {
                                Jit.UI.Dialog('CLOSE');
                                self.pageBack();
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



        });

        self.elements.btValCode.bind('click', function() {
            var mobile = self.elements.txtMobile.val();
            if (!mobile) {
                return Jit.UI.Dialog({
                    'content': '请输入手机号码！',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            } else if (!IsMobileNumber(mobile)) {
                return Jit.UI.Dialog({
                    'content': '手机号码有误！',
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
            }
            self.getVcode(mobile);
        });


    },
    getVcode: function(mobile) {
        var me = this;
        me.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: {
                'action': 'sendCode',
                'mobile': mobile,
                'roleid': 'CE1FF5BE7B6A4B0E8ACF5E2501B43D78',
            },
            success: function(data) {
                me.countDown();
                if (data && data.code == 200) {
                    //console.log(data);
                    me.countDown();
                }
            }
        });
    },
    countDown: function() {

        var me = this,
            btncode = me.elements.btValCode;

        btncode.addClass('unenable');

        me.timeNum = 60;

        me.getCodeOnOff = false;

        me.timer = setInterval(function() {

            if (me.timeNum > 0) {

                btncode.html(me.timeNum + '秒后重新获取');

                me.timeNum--;

            } else {

                me.getCodeOnOff = true;

                btncode.html('获取验证码');

                btncode.removeClass('unenable');

                clearTimeout(me.timer);

                me.timer = null;
            }

        }, 1000);

    },

});