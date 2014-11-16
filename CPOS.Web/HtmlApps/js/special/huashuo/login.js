Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        //登录事件
        $("#login").bind("tap", function () {
            var number = $("#idNumber").val(),  //专家编号
    			pass = $("#password").val();
            if (number.length == 0) {
                Jit.UI.Dialog({
                    'content': '专家编号不能为空!！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return;
            }
            if (pass.length == 0) {
                Jit.UI.Dialog({
                    'content': '密码不能为空!！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return;
            }
            me.loginSubmit(number, pass);
        });

    },
    //登录接口
    loginSubmit: function (idNumber, pass) {
        var me = this;
        this.ajax({
            url: "/Project/Asus/AsusHandler.ashx",
            data: {
                "action": "ambassadorLoginIn",
                "code": idNumber,
                "pass": pass
            },
            success: function (data) {
                if (data.code == "200") {
                    Jit.AM.setBaseAjaxParam({ "userId": data.content.vipList[0].VipID, "openId": data.content.vipList[0].VipId, "customerId": me.getUrlParam('customerId'), "isAmbassador": "true" })
                    //存储专家名称
                    me.setParams("vipName", encodeURIComponent(data.content.vipList[0].VipName));
                    me.setParams("code", data.content.vipList[0].Code);
                    Jit.AM.toPage("Members");
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
        var res = Jit.AM.getBaseAjaxParam();
        if (Jit.AM.getBaseAjaxParam() == null || Jit.AM.getBaseAjaxParam().isAmbassador == null) {
            //如果userId不为空，则表示缓存已有基础数据，如果无，则需要给值
            if (this.getUrlParam('customerId') != null && this.getUrlParam('customerId') != "") {
                var userId = Jit.AM.getBaseAjaxParam().userId == null ? "" : Jit.AM.getBaseAjaxParam().userId;
                var openId = Jit.AM.getBaseAjaxParam().openId == null ? "" : Jit.AM.getBaseAjaxParam().openId;

                Jit.AM.setBaseAjaxParam({ "customerId": this.getUrlParam('customerId'), "userId": userId, "openId": openId });
            }
        }
        else {
            var baseInfo = Jit.AM.getBaseAjaxParam();
            if (baseInfo.userId) {//已经登录
                Jit.AM.toPage("Members");
            }
        }
    }
});