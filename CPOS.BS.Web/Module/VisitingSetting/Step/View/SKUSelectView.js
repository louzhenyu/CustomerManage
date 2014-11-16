function InitView() {

    btnAdd = Ext.create('Jit.button.Button', {
        imgName: 'save',
        id: "btnSave",
        renderTo: 'dvWork',
        isImgFirst: true,
        handler: fnSubmit
    });

    // 数据操作

    gridStoreList = Ext.create('Ext.JITDynamicGrid.Panel',
                            {
                                renderTo: 'dvGrid',
                                height: 375,
                                pageSize: 15,
                                pageIndex: 1,
                                CheckMode: 'MULTI',
                                isSelectKeyName: 'ObjectID', //这个是判断是否选中的数据
                                KeyName: "Target1ID", //这个值是添加到数据库中数据
                                BtnCode: btncode,
                                CorrelationValue: id,
                                ajaxPath: JITPage.HandlerUrl.getValue()
                            });

    gridStoreList.selModel.jitSetValue();
    pnlSearch = Ext.create('Jit.panel.JITStoreSearchPannel',
     {
         margin: '10 0 0 0',
         layout: 'column',
         border: 0,
         width: 910,
         grid: gridStoreList,
         BtnCode: btncode,
         renderTo: 'dvSearch',
         ajaxPath: JITPage.HandlerUrl.getValue()
     });
}