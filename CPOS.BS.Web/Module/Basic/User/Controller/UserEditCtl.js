Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/UserHandler.ashx?mid=");

    var user_id = new String(JITMethod.getUrlParam("user_id"));
    if (user_id != "null" && user_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_user_by_id",
            params: { user_id: user_id },
            method: 'POST',
            success: function (response) {
                var storeId = "userEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;
                debugger;

                Ext.getCmp("txtUserCode").setValue(getStr(d.User_Code));
                Ext.getCmp("txtUserName").setValue(getStr(d.User_Name));
                Ext.getCmp("txtUserEnglish").setValue(getStr(d.User_Name_En));
                Ext.getCmp("txtUserGender").setDefaultValue(getStr(d.User_Gender));
                Ext.getCmp("txtUserIdentity").setValue(getStr(d.User_Identity));
                Ext.getCmp("txtUserBirthday").jitSetValue(getStr(d.User_Birthday));
                Ext.getCmp("txtUserPwd").jitSetValue(getStr("888888"));
                //                Ext.getCmp("txtUserPwd").disabled = true;
                //                Ext.getCmp("txtUserPwd").readonly = true;

                $("table #txtUserPwd").addClass("x-item-disabled");
                $("#txtUserPwd-inputEl").attr("readonly", "readonly");

                Ext.getCmp("txtFailDate").jitSetValue(getStr(d.Fail_Date));
                Ext.getCmp("txtCellPhone").jitSetValue(getStr(d.User_Cellphone));
                Ext.getCmp("txtTelPhone").jitSetValue(getStr(d.User_Telephone));
                Ext.getCmp("txtEmail").jitSetValue(getStr(d.User_Email));
                Ext.getCmp("txtQQ").jitSetValue(getStr(d.QQ));
                Ext.getCmp("txtMSN").jitSetValue(getStr(d.MSN));
                Ext.getCmp("txtBlog").jitSetValue(getStr(d.Blog));
                Ext.getCmp("txtAddress").jitSetValue(getStr(d.User_Address));
                Ext.getCmp("txtPostcode").jitSetValue(getStr(d.User_Postcode));

                Ext.getCmp("txtRemark").jitSetValue(getStr(d.User_Remark));
                Ext.getCmp("txtCreateUserName").setValue(getStr(d.Create_User_Name));
                Ext.getCmp("txtCreateTime").setValue(getStr(d.Create_Time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.Modify_User_Name));
                Ext.getCmp("txtModifyTime").setValue(getStr(d.Modify_Time));

                // Load role list
                fnLoadRole();

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        myMask.hide();
    }

});

fnLoadRole = function () {
    var store = Ext.getStore("userEditRoleStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_user_role_info_by_user_id&user_id=" +
            getUrlParam("user_id"),
        params: { start: 0, limit: 0 }
    });
}

function fnClose() {
    CloseWin('UserEdit');
}

function fnSave() {
    var flag;
    var grid = Ext.getStore("userEditRoleStore");
    var user = {};

    var tbUserCodeCtrl = Ext.getCmp("txtUserCode");
    var tbUserNameCtrl = Ext.getCmp("txtUserName");
    var tbUserEnglishCtrl = Ext.getCmp("txtUserEnglish");
    var hdUserGenderCtrl = Ext.getCmp("txtUserGender");
    var tbUserIdentityCtrl = Ext.getCmp("txtUserIdentity");
    var tbUserCellphoneCtrl = Ext.getCmp("txtCellPhone");
    var tbUserBirthdayCtrl = Ext.getCmp("txtUserBirthday");
    var tbFailDateCtrl = Ext.getCmp("txtFailDate");
    var tbAddressCtrl = Ext.getCmp("txtAddress");
    var tbRemarkCtrl = Ext.getCmp("txtRemark");
    var tbPostcodeCtrl = Ext.getCmp("txtPostcode");
    var tbQQCtrl = Ext.getCmp("txtQQ");
    var tbMSNCtrl = Ext.getCmp("txtMSN");
    var tbBlogCtrl = Ext.getCmp("txtBlog");
    var tbTelPhoneCtrl = Ext.getCmp("txtTelPhone");
    var tbEmailCtrl = Ext.getCmp("txtEmail");
    var tbUserPwdCtrl = Ext.getCmp("txtUserPwd");

    var user_id = getUrlParam("user_id");
    user.User_Id = user_id;
    user.User_Code = getStr(tbUserCodeCtrl.getValue());
    user.User_Name = getStr(tbUserNameCtrl.getValue());
    user.User_Name_En = getStr(tbUserEnglishCtrl.getValue());
    user.User_Gender = getStr(hdUserGenderCtrl.getValue());
    user.User_Birthday = getStr(tbUserBirthdayCtrl.jitGetValueText());
    user.Fail_Date = getStr(tbFailDateCtrl.jitGetValueText());
    user.User_Identity = getStr(tbUserIdentityCtrl.getValue());
    user.User_Cellphone = getStr(tbUserCellphoneCtrl.getValue());
    user.User_Remark = getStr(tbRemarkCtrl.getValue());
    user.User_Address = getStr(tbAddressCtrl.getValue());
    user.User_Postcode = getStr(tbPostcodeCtrl.getValue());
    user.QQ = getStr(tbQQCtrl.getValue());
    user.MSN = getStr(tbMSNCtrl.getValue());
    user.Blog = getStr(tbBlogCtrl.getValue());
    user.User_Telephone = getStr(tbTelPhoneCtrl.getValue());
    user.User_Email = getStr(tbEmailCtrl.getValue());
    user.User_Password = getStr(tbUserPwdCtrl.getValue());

    user.userRoleInfoList = [];
    if (grid.data.map != null) {
        for (var tmpItem in grid.data.map) {
            var objData = grid.data.map[tmpItem].data;
            var objItem = {};
            objItem.Id = objData.Id;
            objItem.UserId = user.User_Id;
            objItem.RoleId = objData.RoleId;
            objItem.UnitId = objData.UnitId;
            objItem.DefaultFlag = objData.DefaultFlag;
            user.userRoleInfoList.push(objItem);
        }
    }

    if (user.User_Name == null || user.User_Name == "") {
        showError("请填写用户名称");
        return;
    }
    if (user.User_Code == null || user.User_Code == "") {
        showError("请填写用户工号");
        return;
    }
    if (user.User_Password == null || user.User_Password == "") {
        showError("请填写密码");
        return;
    }
    if (user.Fail_Date == null || user.Fail_Date == "") {
        showError("请选择有效日期");
        return;
    }
    if (user.User_Telephone == null || user.User_Telephone == "") {
        showError("请填写手机");
        return;
    }
    if (user.User_Email == null || user.User_Email == "") {
        showError("请填写邮箱");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/User/Handler/UserHandler.ashx?method=user_save&user_id=' + user_id,
        params: { "user": Ext.encode(user) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}

function fnDeleteRole() {
    var grid = Ext.getCmp("gridRole");
    var ids = JITPage.getSelected({ gridView: grid, id: "Id" });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择职务");
        return;
    };

    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("Id", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
}

function fnAddRole(id, op, param) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "UserEditRole",
        title: "职务",
        url: "UserEditRole.aspx?op=" + op + "&id=" + id + getStr(param)
    });
    win.show();
}

function fnCloseWin() {
    CloseWin('UserEdit');
}


