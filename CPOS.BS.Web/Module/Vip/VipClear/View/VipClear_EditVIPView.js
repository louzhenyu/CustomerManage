function InitView() {

    // 数据操作
    gridStoreList = Ext.create('Ext.JITStoreGrid.Panel', {
        renderTo: 'dvGrid',
        height: 405,
        pageSize: 15,
        pageIndex: 1,
        isHaveCheckMode: false,
        KeyName: "VIPID", //这个值是添加到数据库中数据
        BtnCode: 'search',
        CorrelationValue: id + "|" + type,
        editKeyName: 'VipName',
        ajaxPath: JITPage.HandlerUrl.getValue()
    });

    //gridStoreList.selModel.jitSetValue();
    pnlSearch = Ext.create('Jit.panel.JITStoreSearchPannel', {
        margin: '10 0 0 0',
        layout: 'column',
        border: 0,
        width: 910,
        grid: gridStoreList,
        renderTo: 'dvSearch',
        BtnCode: 'search',
        ajaxPath: JITPage.HandlerUrl.getValue()
    });

    pnlEditView = Ext.create('Jit.window.JITVipFrmWindow', {
        jitSize: "large",
        height: 520,
        ajaxPath: '/Module/Vip/VipSearchNew/Handler/VipHandler.ashx?mid=1=',
        grid: gridStoreList,
        BtnCode: 'search'
    });
}