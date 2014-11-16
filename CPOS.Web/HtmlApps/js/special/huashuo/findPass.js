Jit.AM.defindPage({
    onPageLoad: function () {
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        //登录事件
        $("#find").bind("tap", function () {
            var email = $("#email").val();  //大使编号
            if (!/^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/.test(email)) {
                Jit.UI.Dialog({
                    'content': '电子邮件格式不对!',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return false;
            }
            me.findPass(email);
        });

    },
    //找回密碼接口
    findPass: function (email) {
        var me = this;
        this.ajax({
            url: "/Project/Asus/AsusHandler.ashx",
            data: {
                "action": "ForgetPassword",
                "email": email
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                Jit.UI.Masklayer.hide();
                if (data.code == "200") {
                    var obj = data.content.vipList[0];
                    Jit.UI.Dialog({
                        'content': "您好，新密码已发送 <br>请登录邮箱查看！",
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.AM.toPage("Login");
                        }
                    });
                } else {
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
    initPage: function () {

    }
});