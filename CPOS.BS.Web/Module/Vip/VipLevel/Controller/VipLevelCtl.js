Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/VipHandler.ashx?mid=");



    fnSearch();

});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "vipEdit",
        title: "会员",
        url: "vipEdit.aspx?mid=" + __mid
    });
    win.show();
}

fnSearch = function () {
   
    var store = Ext.getStore("vipLevelStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_vip";
    store.pageSize = JITPage.PageSize.getValue();
//    store.proxy.extraParams = {
//        form: 
//    };
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width()) });

}


function fnView(id, tabsIndex) {
    //alert(id);
    location.href = "/module/Vip/VipSearchNew/Vip.aspx?mid=" + getUrlParam("mid") + "&vipLevel=" + id;
}


