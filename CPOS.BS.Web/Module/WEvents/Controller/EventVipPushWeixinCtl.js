Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

var myMask;
var ids;
var eventId;

Ext.onReady(function () {
    ids = this.parent.ids;
    eventId = this.parent.eventId;

    myMask = new Ext.LoadMask(Ext.getBody(), { msg: JITPage.Msg });
    //myMask.show();
    //加载需要的文件
    //InitVE();
    //InitStore();
    InitView();
    fnload();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");

});


function fnload() {
    Ext.Ajax.request({
        url: "/Framework/Javascript/Biz/Handler/WApplicationInterfaceHandler.ashx?method=get_app_sys_list",
        method: 'post',
        success: function (response) {
            debugger;
            var jdata = Ext.decode(response.responseText);
            if (jdata.data.length > 0) {
                Ext.getCmp("txtApplicationId").setValue(jdata.data[0].ApplicationId);
            }
        }
    });

}
function fnClose() {
    CloseWin("EventVipPushMessage");
}

function fnGetWMaterialTextById(Id) {
    myMask.show();
    if (Id != "null" && Id != "") {
        Ext.Ajax.request({
            url: "../WApplication/Handler/WApplicationHandler.ashx?mid=&method=get_items1",
            params: { Id: Id },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;
                Ext.getCmp("txtContent").jitSetValue(getStr(d[0].Content));

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
                myMask.hide();
            }
        });
    }
}

function fnPushWeixin() {
    myMask.show();

    Ext.Ajax.request({
        method: 'POST',
        async: true,
        url: JITPage.HandlerUrl.getValue() + '&method=PushWeixin',
        timeout: 900000,  //推送人数很多时会发生超时，增加超时时间，default 30000 milliseconds    
        params: {
            "template": Ext.getCmp("txtContent").getValue()
            , ids: ids
            , eventId: eventId
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("推送失败：" + d.msg);
            } else {
                if (d.status == null || d.status == "")
                    showSuccess("推送成功");
                else
                    showSuccess(d.status + ":" + d.msg);
            }
            myMask.hide();
        },
        failure: function (result) {
            showError("推送失败：" + result.responseText);
            myMask.hide();
        }
    });
}