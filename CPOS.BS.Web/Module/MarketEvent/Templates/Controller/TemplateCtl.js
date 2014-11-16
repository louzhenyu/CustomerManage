Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });


    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/TemplateHandler.ashx?mid=");

    var user_id = new String(JITMethod.getUrlParam("user_id"));
    if (user_id != "null" && user_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_user_by_id",
            params: { user_id: user_id },
            method: 'POST',
            success: function (response) {
                var storeId = "TemplateStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;

//                Ext.getCmp("txtUserCode").setValue(getStr(d.User_Code));
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        //myMask.hide();
    }

});


function fuGetTemplate(TemplateType) {
    //alert("Handler/TemplateHandler.ashx?mid=&method=GetTemplateListByType&TemplateType=" + TemplateType);
    var store = Ext.getStore("TemplateStore");
    store.load({
        url: "Handler/TemplateHandler.ashx?mid=&method=GetTemplateListByType&TemplateType=" + encodeURIComponent(TemplateType),
        params: { start: 0, limit: 0 }
    });
}

function fnModify(id) {//, TemplateType, TemplateContent, TemplateDesc
    //debugger;
    //alert(id)
    var TemplateId = id;
    if (TemplateId != "null" && TemplateId != "") {
        Ext.Ajax.request({
            url: "Handler/TemplateHandler.ashx?mid=&method=GetEventById&TemplateId=" + TemplateId + "",
            method: 'GET',
            success: function (response) {
                var storeId = "TemplateStore";
                //alert(response.responseText)
                //alert(Ext.decode(response.responseText).topics)
                var d = Ext.decode(response.responseText).topics;
                //alert(getStr(d.TemplateType));
                Ext.getCmp("txtTemplateDesc").setValue(getStr(d.TemplateContent));
                Ext.getCmp("txtTemplateDesc1").setValue(getStr(d.TemplateDesc));
                Ext.getCmp("txtTemplateId").setValue(getStr(id));
                Ext.getCmp("txtTemplateType").setValue(getStr(d.TemplateType));
                Ext.getCmp("txtTemplateDescSMS").setValue(getStr(d.TemplateContentSMS));
                Ext.getCmp("txtTemplateDescAPP").setValue(getStr(d.TemplateContentAPP));
                
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    document.getElementById("divModify").style.height = "451px";
//    Ext.getCmp("txtTemplateDesc").setValue(getStr(TemplateContent));
//    Ext.getCmp("txtTemplateDesc1").setValue(getStr(TemplateDesc));
//    Ext.getCmp("txtTemplateId").setValue(getStr(id));
//    Ext.getCmp("txtTemplateType").setValue(getStr(TemplateType));
//    document.getElementById("divModify").style.height = "451px";
}

function fnDelete(id, TemplateType) {
    //alert(TemplateType);
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridRole"), id: "TemplateID" }),
        url: "Handler/TemplateHandler.ashx?mid=&method=template_delete&id=" + id,
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridRole"), id: "TemplateID" })
        },
        handler: function () {
            fuGetTemplate(TemplateType);// Ext.getStore("userStore").load();
        }
    });
}
//清空
function fnEmpty() {
    //alert('Empty');
    Ext.getCmp("txtTemplateDesc").setValue("");
    Ext.getCmp("txtTemplateDescSMS").setValue("");
    Ext.getCmp("txtTemplateDescAPP").setValue("");
}

function fnSave() {
    var template = {};
    //alert('Save');
    template.TemplateID = Ext.getCmp("txtTemplateId").getValue();
    template.TemplateContent = Ext.getCmp("txtTemplateDesc").getValue();
    template.TemplateContentSMS = Ext.getCmp("txtTemplateDescSMS").getValue();
    template.TemplateContentAPP = Ext.getCmp("txtTemplateDescAPP").getValue();
    template.TemplateType = Ext.getCmp("txtTemplateType").getValue();
    template.TemplateDesc = Ext.getCmp("txtTemplateDesc1").getValue();
//    alert(template.TemplateID)
    //    alert(template.TemplateContent)
    if (template.TemplateID == null || template.TemplateID == "") {
        showError("必须选择编辑模板");
        return;
    }
    if (template.TemplateContent == null || template.TemplateContent == "") {
        showError("必须输入编辑模板信息");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/TemplateHandler.ashx?method=templae_save&TemplateID=' + template.TemplateID,
        params: {
            "template": Ext.encode(template)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                fuGetTemplate(template.TemplateType);
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}








