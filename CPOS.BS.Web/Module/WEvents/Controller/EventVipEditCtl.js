var myMask;
var eventId;
Ext.onReady(function () {
    eventId = this.parent.eventId;
    //加载需要的文件
    var myMask_info = "loading...";
    myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });

    //InitVE();
    //InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");
    debugger;
    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetEventVipById",
            params: {
                id: id
            },
            method: 'post',
            success: function (response) {
                debugger;
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtName").setValue(d.UserName);
                Ext.getCmp("txtPhone").jitSetValue(d.Phone);
                Ext.getCmp("txtCompany").jitSetValue(d.VipCompany);
                Ext.getCmp("txtEmail").jitSetValue(d.Email);
                Ext.getCmp("txtPost").jitSetValue(d.VipPost);

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        myMask.hide();
    }

});

function fnClose() {
    var win = getUrlParam("win");
    if (win == undefined || win == null || win == "") win = "EventVipEdit";
    CloseWin(win);
}

function fnSave() {
    var vip = {};

    vip.SignUpID = getUrlParam("id");

    vip.UserName = Ext.getCmp("txtName").jitGetValue();
    vip.Phone = Ext.getCmp("txtPhone").jitGetValue();
    vip.VipCompany = Ext.getCmp("txtCompany").jitGetValue();
    vip.VipPost = Ext.getCmp("txtPost").jitGetValue();
    vip.Email = Ext.getCmp("txtEmail").jitGetValue();
    vip.EventId = eventId;

    if (vip.UserName == null || vip.UserName == "") {
        showError("必须输入姓名");
        return;
    }
    if (vip.Phone == null || vip.Phone == "") {
        showError("必须输入联系电话");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: JITPage.HandlerUrl.getValue() + '&method=SaveEventVip',
        params: {
            "vip": Ext.encode(vip)
        },
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

    if (flag) fnClose();
}