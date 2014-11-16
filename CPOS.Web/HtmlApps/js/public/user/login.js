Jit.AM.defindPage({

    name: 'Login',
    elements: {
        txtMobile: '',
        tipValCode: 'tipValCode',
        txtValCode: '',
        submitLogin: ''
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
        self.elements.tipValCode = $('#tipValCode');
        self.elements.txtValCode = $('#txtValCode');
        self.elements.submitLogin = $('#submitLogin');


        //如果用户注册或者已经登陆，不需要再次登陆
        // var baseInfo = self.getBaseInfo();
        // if (baseInfo && baseInfo.userId) {
        //     self.toPage('Home');
        //     return false;
        // };

        // var userId = localStorage.getItem(KeyList.userId);
        // if (userId) {
        //     baseInfo.userId = userId;
        //     Jit.AM.setBaseAjaxParam(baseInfo);
        //     localStorage.setItem(KeyList.userId, baseInfo.userId); //设置缓存
        //     self.toPage('Home');
        //     return false;
        // };



    }, //绑定事件
    initEvent: function() {
        var self = this;
        //提交注册
        self.elements.submitLogin.bind('click', function() {
            var mobile = self.elements.txtMobile.val(),
                valCode = self.elements.txtValCode.val();
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

            self.ajax({
                url: '/dynamicinterface/data/data.aspx',
                data: {
                    'action': 'getUserByPhoneAndCode',
                    'name': name,
                    'Photo': mobile,
                    'code': valCode,
                    'roleid':'CE1FF5BE7B6A4B0E8ACF5E2501B43D78'
                },
                beforeSend:function(){
                    UIBase.loading.show();

                },
                success: function(data) {
                                        UIBase.loading.hide();
                    if (data && data.code == 200) {

                        //设置基础用户id
                        var baseInfo = self.getBaseInfo();
                        baseInfo.userId = data.content.userId;
                        Jit.AM.setBaseAjaxParam(baseInfo);
                        localStorage.setItem(KeyList.userId, baseInfo.userId); //设置缓存
                        //  Jit.UI.Dialog({
                        //     'content': '注册成功',
                        //     'type': 'Alert',
                        //     'CallBackOk': function() {
                        //         Jit.UI.Dialog('CLOSE');
                        //         self.toPage('AddUserInfo');
                        //     }
                        // });
                        self.toPage('AddUserInfo');

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

        self.elements.tipValCode.bind('click', function() {
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
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getCodeByPhone',
                'Photo': mobile,
                'roleid':'CE1FF5BE7B6A4B0E8ACF5E2501B43D78',
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
            btncode = me.elements.tipValCode;

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