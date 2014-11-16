Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

var eventTypeID;
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=" + __mid);

    fnSearch();
});
function fnSearch() {

    Ext.getStore("EventsTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=getEventstypeList";
    Ext.getStore("EventsTypeStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("EventsTypeStore").proxy.extraParams = {
        title: Ext.getCmp("eventsType").jitGetValue()
    };

    Ext.getStore("EventsTypeStore").load();
}
function fnAddEditView() {
    eventTypeID = "";
    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("editWin").height = 280;
    Ext.getCmp("editWin").width = 380;
    Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
    Ext.getCmp("editWin").show();

}
function fnSave() {
    debugger;
    var eventstype = {};
    eventstype.EventTypeID = eventTypeID;
    eventstype.Title = Ext.getCmp("txtTitle").jitGetValue();
    eventstype.Remark = Ext.getCmp("txtRemark").jitGetValue();
    eventstype.GroupNo = Ext.getCmp("txtGroupNo").jitGetValue();
    if (eventstype.Title == null || eventstype.Title == "") {
        Ext.Msg.alert("提示", "活动费类型不能为空");
    }
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventsHandler.ashx?method=eventsType_save',
        params: {
            "eventTypeID": Ext.encode(eventstype)
        },
        success: function (result, request) {

            var d = Ext.decode(result.responseText);
            if (d.success == true) {
                Ext.Msg.alert("提示", "保存成功");
                parent.fnSearch();
                Ext.getCmp("editWin").hide();
            } else {
                Ext.Msg.alert("提示", "保存失败!" + d.msg);

            }
        },
        failure: function (result) {
            Ext.Msg.alert("提示", "保存失败!");
        }
    });

}
//查看订单详细数据
function fnView(id) {
    eventTypeID = id;
    Ext.getCmp("editPanel").getForm().reset();
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetEventsTypeById",
        params: { eventTypeID: id },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d.success == true) {
                id = d.data.EventTypeID;
                Ext.getCmp("txtTitle").jitSetValue(d.data.Title);
                Ext.getCmp("txtRemark").jitSetValue(d.data.Remark);
                if (d.data.GroupNo == null || d.data.GroupNo=='') {
                    Ext.getCmp("txtGroupNo").jitSetValue('1');
                }
                else {
                    Ext.getCmp("txtGroupNo").jitSetValue(d.data.GroupNo);
                } 
               
     
            } else {
                Ext.Msg.alert("提示", "获取数据失败");
            }

        },
        failure: function () {
            Ext.Msg.alert("提示", "获取数据失败");
            myMask.hide();
        }
    });
    myMask.hide();
    Ext.getCmp("editWin").height = 280;
    Ext.getCmp("editWin").width = 380;
    Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
    Ext.getCmp("editWin").show();
}
//删除操作
function fnDelete(id) {
    Ext.Msg.confirm("请确认", "是否要删除数据？", function (button) {
        if (button == "yes") {
            url: JITPage.HandlerUrl.getValue() + "&method=EventsTypeRemove",
            Ext.Ajax.request({
                url: JITPage.HandlerUrl.getValue() + "&method=EventsTypeRemove",
                params: {
                    eventsTypeId: id
                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '删除成功',
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: fnSearch()
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
            });
        }
    });
}