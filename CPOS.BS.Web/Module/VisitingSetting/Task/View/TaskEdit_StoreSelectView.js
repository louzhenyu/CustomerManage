function InitView() {
    btnAdd = Ext.create('Jit.button.Button', {
        renderTo: 'dvWork',
        imgName: 'save',
        id: "btnSave",
        style: 'float:left',
        isImgFirst: true,
        handler: fnSubmit
    });

    Ext.create('Ext.form.field.Checkbox', {
        renderTo: 'dvAutoFill',
        id: 'ckAutoFill',
        style: 'float:left;margin-left:10px;',
        boxLabel: '按条件匹配(自动补充符合条件的终端信息)',
        listeners: {
            'change': fnAotoFillSetChange
        }
    });
    // 数据操作
    gridStoreList = Ext.create('Ext.JITDynamicGrid.Panel',
                            {
                                renderTo: 'dvGrid',
                                height: 440,
                                pageSize: 15,
                                pageIndex: 1,
                                CheckMode: 'MULTI',
                                isSelectKeyName: 'MappingID', //这个是判断是否选中的数据
                                KeyName: "StoreID", //这个值是添加到数据库中数据
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
         grid: gridStoreList,
         BtnCode: btncode,
         renderTo: 'dvSearch',
         ajaxPath: JITPage.HandlerUrl.getValue()
     });

     Ext.create('Jit.window.Window', {
         title:'自动补充设置',
         height: 550,
         width: 1054,
         jitSize: "large",
         id: "storeAutoFillSetWin",
         layout: 'fit',
         draggable: true,
         contentEl: 'tab1',
         border: 0,
         closeAction: 'hide'
     });
}