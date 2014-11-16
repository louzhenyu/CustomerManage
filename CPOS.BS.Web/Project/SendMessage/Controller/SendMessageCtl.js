var addr, name, vipChecked = new Array(), K, htmlEditor;
Ext.onReady(function () {
    __InitSendMessageVE();
    __InitSendMessageStore();
    __InitMessageSendView();

    JITPage.HandlerUrl.setValue("Handler/SendMessageHandler.ashx?mid=");

    setTimeout(function () {
        $(".ke-edit").css({ height: "200px" });
        $(".ke-edit-iframe").css({ height: "200px" });
        $(".ke-edit-textarea").css({ height: "200px" });
        htmlEditor.html('<p>Dear #会员#</p><p>	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;已经收到您的众筹报名表，为了进一步确认您对本众筹项目的理解，本邮件附上相关项目介绍以及法律协议，如果你阅读后没有问题，请汇款5万元到：xxx，账号。</p><p>	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;汇款前请务必电话确认账号是否正确，联系人：王子赫秘书EMBA联盟秘书处。</p>');
    }, 1000);

    addr = JITMethod.getUrlParam('params').split('|')[0];
    name = decodeURI(JITMethod.getUrlParam('params').split('|')[1]);

    Ext.getCmp("__MessageAddress").jitSetValue(addr);
    Ext.getCmp("txtUserCount").jitSetValue("共" + addr.split(',').length + "人");

});

/*发送测试邮件*/
function fnSendTestMessage() {
    Ext.getCmp("__sendTestMessagePanel").getForm().reset();
    Ext.getCmp("__sendTestMessageWin").show();
}

/*发送测试邮件*/
function fnSubmitTestMessage() {
    var form = Ext.getCmp("__sendTestMessagePanel").getForm();

    if (Ext.getCmp("__TestMessageAddress").jitGetValue() == null || Ext.getCmp("__TestMessageAddress").jitGetValue() == "") {
        Ext.Msg.show({
            title: '提示',
            msg: "'邮箱地址'不能为空",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }

    if (!form.isValid()) {
        return false;
    }
    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&method=sendTestMsg",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            emailAddress: Ext.getCmp("__TestMessageAddress").jitGetValue()
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: '发送成功',
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("__sendTestMessageWin").hide();
                    }
                });
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

/*发送消息*/
function fnSubmitMessage() {
    var form = Ext.getCmp("__sendMessagePanel").getForm();

    if (Ext.getCmp("__MessageAddress").jitGetValue() == null || Ext.getCmp("__MessageAddress").jitGetValue() == "") {
        Ext.Msg.show({
            title: '提示',
            msg: "'收箱人'不能为空",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    if (Ext.getCmp("__MessageTitle").jitGetValue() == null || Ext.getCmp("__MessageTitle").jitGetValue() == "") {
        Ext.Msg.show({
            title: '提示',
            msg: "'邮箱标题'不能为空",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }

    if (!form.isValid()) {
        return false;
    }
    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&method=sendMsg",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            emailAddress: Ext.getCmp("__MessageAddress").jitGetValue(),
            tempEmailTitle: Ext.getCmp("__MessageTitle").jitGetValue(),
            userName: name,
            emailContent: htmlEditor.html()
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: '发送成功',
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {

                    }
                });
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

/*插入认证链接*/
function fnAuthLink() {
    htmlEditor.focus();
    htmlEditor.appendHtml("<br/><a href=\"http://112.124.68.147:9004/HtmlApps/auth.html?pageName=NewAddUserInfo\">点击认证</a>");
    var editor;
    htmlEditor.ready(function (K) {
        editor = K.create('textarea[name="content"]', {});
        editor.focus();
        editor.appendHtml('<strong>@Tony</strong>');
    });
}

/*插入缴费链接*/
function fnFeesLink() {
    htmlEditor.focus();
    htmlEditor.appendHtml("<br/><a href=\"http://112.124.68.147:9004/HtmlApps/html/special/europe/account.html?rootPage=true&customerId=a2573925f3b94a32aca8cac77baf6d33&ver=1396606355601\">点击交会费</a>");
}