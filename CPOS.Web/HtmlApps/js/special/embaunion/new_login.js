Jit.AM.defindPage({

    name: 'NewLogin',
    elements: {
        txtUserName: '',
        txtPassword: '',
        submitLogin: '',
        submitRegister: ''
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
        self.elements.txtUserName = $('#txtUserName');
        self.elements.txtPassword = $('#txtPassword');
        self.elements.submitRegister = $('#submitRegister');
        self.elements.submitLogin = $('#submitLogin');

    }, //绑定事件
    initEvent: function() {
        var self = this;
        //提交登陆
        self.elements.submitLogin.bind('click', function() {
            self.HandleSubmit(1);
        });
        //提交注册
        self.elements.submitRegister.bind('click', function() {
            self.HandleSubmit(0);

        });



    },
    HandleSubmit: function(flag) {
        var self = this,
            loginName = self.elements.txtUserName.val(),
            passWord = self.elements.txtPassword.val();
        if (!loginName) {
            return Jit.UI.Dialog({
                'content': '请输入帐号',
                'type': 'Alert',
                'CallBackOk': function() {
                    Jit.UI.Dialog('CLOSE');
                }
            });
        }
        if (!passWord) {
            return Jit.UI.Dialog({
                'content': '请输入密码',
                'type': 'Alert',
                'CallBackOk': function() {
                    Jit.UI.Dialog('CLOSE');
                }
            });
        }
        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getUserByLogin',
                'LoginName': loginName,
                'PassWord': passWord,
                'RoleID': 'CE1FF5BE7B6A4B0E8ACF5E2501B43D78',
                'IsLogin': flag == 1
            },
            beforeSend: function() {
                UIBase.loading.show();
            },
            success: function(data) {
  
                UIBase.loading.hide();
                if (data && data.code == 200) {

                    var baseInfo = self.getBaseInfo();
                    baseInfo.userId = data.content.userId;
                    Jit.AM.setBaseAjaxParam(baseInfo);

            
                    if (flag) {
                        if (Jit.AM.hasHistory()) { //验证是否有浏览历史，如果没有跳转到指定页面
                            self.pageBack();
                        } else {

                            self.toPage('Home')
                        }

                    } else {
                        self.toPage('AddUserInfo');
                    }

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
    }

});