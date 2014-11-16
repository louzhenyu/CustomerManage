var id, btncode;
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/EventStatsHander.ashx?mid=" + __mid);

    fnSearch();

//    Ext.getStore("titleStore").addListener({
//        'load': function () {
//            if (Ext.getStore("titleStore").first() != undefined) {
//                Ext.getCmp("title").jitSetValue(Ext.getStore("titleStore").first().data.NewsID);
//            }
//        }
//    });

    //    Ext.getStore("serchStore").addListener({
    //        'load': function () {
    //            if (Ext.getStore("serchStore").first() != undefined) {
    //                Ext.getCmp("serchTitle").jitSetValue(Ext.getStore("serchStore").first().data.NewsID);
    //            }
    //        }
    //    });
});
function fnSearch() {

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=EventStatsPageData";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        objectType: Ext.getCmp("serchObjectType").jitGetValue(),
        title: Ext.getCmp("serchTitle").jitGetValue()
    };
    Ext.getCmp("pageBar").moveFirst();
}
//添加窗体
function fnAddEditView() {
    id = null;
    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("title").jitSetValue("");

    Ext.getCmp("editWin").height = 280;
    Ext.getCmp("editWin").width = 380;
    Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
    Ext.getCmp("editWin").show();
}
//查看订单详细数据
function fnView(eventStatsid) {

    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("title").jitSetValue("");
    id = eventStatsid;


    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetEventStatsDetail",
        params: { eventStatid: id },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText).eventsDetail[0];
            Ext.getStore("titleStore").removeAll();
            Ext.getStore("titleStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetOptionID&objecttype=" + d.ObjectType;
            Ext.getStore("titleStore").load();
            debugger;
            Ext.getCmp("ObjectType").jitSetValue(getStr(d.ObjectType));
            Ext.getCmp("title").jitSetValue(getStr(d.ObjectID));
            Ext.getCmp("txt_sequence").jitSetValue(d.Sequence);
        }
    });



    Ext.getCmp("editWin").height = 280;
    Ext.getCmp("editWin").width = 380;
    Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
    Ext.getCmp("editWin").show();
}

//删除操作
function fnDelete(id) {
    if (!confirm("确认删除?")) return;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=EventStatsRemove",
        params: { eventStatid: id },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
                return;
            }
            fnSearch();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}

function fnSave() {
    var ObjectType = Ext.getCmp("ObjectType").jitGetValue();
    if (ObjectType == null || ObjectType == "") {
        showError("请选择类型");
        return;
    }
    var title = Ext.getCmp("title").jitGetValue();
    if (title == null || title == "") {
        showError("请选择标题");
        return;
    }
    var numsequence = Ext.getCmp("txt_sequence").jitGetValue();

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=EventStatsSave",
        params: {
            eventStatsId: id,
            objectType: Ext.getCmp("ObjectType").jitGetValue(),
            title: Ext.getCmp("title").jitGetValue(),
            sequence: numsequence
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            if (jdata.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("editWin").hide();

                        fnSearch();
                    }
                });
            }
            else {
                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
        }
    });

}



