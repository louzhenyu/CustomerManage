var btncode = JITMethod.getUrlParam("btncode");
var id = JITMethod.getUrlParam("id") == null ? "" : JITMethod.getUrlParam("id");

Ext.onReady(function () {

    //加载需要的文件
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    InitVE();
    InitStore();
    InitView();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/VisitingObjectHandler.ashx?mid=" + __mid);
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    //设置初始值
    Ext.getCmp("eidt_State").setValue(1);

    Ext.getStore("objectParentStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetParentObject&id=" + id;
    Ext.getStore("objectParentStore").load();

    if (id != null && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetByID",
            params: { id: id },
            method: 'post',
            success: function (response) {
                //try {
                var jdata = Ext.JSON.decode(response.responseText);
                //                if (!JITPage.checkAjaxPermission(jdata)) {
                //                    return;
                //                }
                //加载form
                Ext.getStore("objectStore").add(jdata);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("objectStore").first());
                Ext.getCmp("ObjectGroup").jitSetValue(Ext.getStore("objectStore").first().data.ObjectGroup);
                Ext.getCmp("LayoutType").jitSetValue(Ext.getStore("objectStore").first().data.LayoutType);
                myMask.hide();

                //tab设置
                var focus = null;
                if (JITMethod.getUrlParam("focus") != null && JITMethod.getUrlParam("focus") != "") {
                    focus = parseInt(JITMethod.getUrlParam("focus"));
                }
                fnLoadTab({
                    id: jdata.VisitingObjectID,
                    focus: focus
                });

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

function fnSubmit() {
    var form = this.up('form').getForm();
    if (!form.isValid()) {
        return false;
    }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditObject",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id
        },
        success: function (form, action) {
            var focus = 0;
            parent.Ext.getStore("list_objectStore").load();

            parent.document.getElementById("tab1").setAttribute("src", "VisitingObjectEdit.aspx" + "?mid=" + __mid + "&id=" + action.result.id + "&focus=" + focus + "&btncode=" + btncode);
        },
        failure: JITPage.submitFailure
    });
}

function fnStepChange() {
    //alert(this.getValue());
    if (parseInt(this.getValue()) == 7) {
        Ext.getCmp("ObjectGroup").show();
    }
    else {
        Ext.getCmp("ObjectGroup").hide();
    }
}

/*
@id 
@focus
*/
function fnLoadTab(obj) {

    parent.tab2State = false;

    var parameters = "?mid=" + __mid +  "&id=" + obj.id + "&btncode=" + btncode + "&focus="+obj.focus+"&r=" + Math.random();
    parent.document.getElementById("tab2").setAttribute("src", "VisitingObjectEdit_ParameterSelect.aspx" + parameters);
    parent.Ext.getCmp("tabs").items.items[1].setDisabled(false);

    //编辑完成后
    if (obj.focus != null && obj.focus != 0) {
        parent.Ext.getCmp("tabs").setActiveTab(obj.focus);
        return;
    }
}