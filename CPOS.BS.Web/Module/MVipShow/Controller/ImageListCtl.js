
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");
    
    fnSearch();

    myMask.hide();
});

function fnClose() {
    CloseWin('ImageList');
}

function fnSearch() {
    var eventId = getUrlParam("EventID");
    Ext.getStore("imageStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_images";
    Ext.getStore("imageStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("imageStore").proxy.extraParams = {
        EventId: eventId
    };
    Ext.getStore("imageStore").load();
}

function fnDelete(id, val) {
    if (!confirm("确认删除?")) return;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=image_delete",
        params: { ids: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d.success) {
                alert("操作成功");
                fnSearch();
                return;
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}

