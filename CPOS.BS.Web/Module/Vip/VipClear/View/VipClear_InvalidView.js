function InitView() {
    // 数据操作
    gridStoreList = Ext.create('Ext.JITStoreGrid.Panel', {
        renderTo: 'dvGrid',
        height: 405,
        pageSize: 15,
        pageIndex: 1,
        isHaveCheckMode: false,
        KeyName: "StoreID", //这个值是添加到数据库中数据
        CorrelationValue: id + "|" + type,
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
        ajaxPath: JITPage.HandlerUrl.getValue()
    });
}