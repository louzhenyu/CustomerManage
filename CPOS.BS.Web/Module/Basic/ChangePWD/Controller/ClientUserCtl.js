var pUserID, pOldPwd, pNewPwd, btncode;
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/ClientUserHandler.ashx?mid=");
    fnSearch();
});

fnSearch = function () {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=search",
        params: { id: "" },
        method: 'post',
        success: function (response) {
            var jdata = eval(response.responseText);
            //赋值
            Ext.getCmp("Username").jitSetValue(jdata[0].User_Name);
            pUserID = jdata[0].User_Id;
            pOldPwd = jdata[0].User_Password;
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

fnSubmit = function () {
    if (Ext.getCmp("UserPWD").value == "") {
        Ext.Msg.show({
            title: '警告',
            msg: '"旧密码"不能为空',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    if (Ext.getCmp("NewUserPWD").value == "") {
        Ext.Msg.show({
            title: '警告',
            msg: '"新密码"不能为空',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    if (Ext.getCmp("pNewUserPWD").value == "") {
        Ext.Msg.show({
            title: '警告',
            msg: '"新密码确认"不能为空',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    if (Ext.getCmp("pNewUserPWD").value != Ext.getCmp("NewUserPWD").value) {
        Ext.Msg.show({
            title: '警告',
            msg: '"新密码确认与新密码不同"',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=setpwd",
        params: {
            id: pUserID,
            OldPwd: Ext.getCmp("UserPWD").jitGetValue(),
            UserPWD: Ext.getCmp("NewUserPWD").jitGetValue()
        },
        method: 'post',
        success: function (response) {
            var jdata = eval('(' + response.responseText + ')');
            //赋值
            if (!jdata.success) {
                Ext.Msg.alert("提示", "旧密码不正确");
                return false;
            } else {
                Ext.Msg.alert("提示", "密码修改成功");
                fnSearch();
                Ext.getCmp("editPanel").getForm().reset();
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}