Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/VipTransferHandler.ashx?mid=");

    fnSearch();
});

function fnSearch() {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=data_query",
        method: 'get',
        success: function (response) {
            var d = Ext.decode(response.responseText).topics;

            document.getElementById("storeTotal").innerText = Ext.util.Format.number(d.Table[0].StoreTotal, '0,000');
            document.getElementById("weixinTotal").innerText = Ext.util.Format.number(d.Table[0].WeixinTotal, '0,000');
            document.getElementById("storeActive").innerText = Ext.util.Format.number(d.Table[0].StoreTotal, '0,000');
            document.getElementById("weixinActive").innerText = Ext.util.Format.number(d.Table[0].WeixinTotal, '0,000');
            document.getElementById("storeConsumer").innerText = Ext.util.Format.number(d.Table[0].StoreConsumer, '0,000');
            document.getElementById("weixinConsumer").innerText = Ext.util.Format.number(d.Table[0].WeixinConsumer, '0,000');
            document.getElementById("storeMConsumer").innerText = Ext.util.Format.number(d.Table[0].StoreMConsumer, '0,000');
            document.getElementById("weixinMConsumer").innerText = Ext.util.Format.number(d.Table[0].WeixinMConsumer, '0,000');
            document.getElementById("storeLoyalConsumer").innerText = Ext.util.Format.number(d.Table[0].StoreLoyalConsumer, '0,000');
            document.getElementById("weixinLoyalConsumer").innerText = Ext.util.Format.number(d.Table[0].WeixinLoyalConsumer, '0,000');
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    })
}