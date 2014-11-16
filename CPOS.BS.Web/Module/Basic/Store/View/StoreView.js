function InitView() {
    //查询面板生成区域
    btnAdd = Ext.create('Jit.button.Button', {
        // componentCls: 'create',
        imgName: 'create',
        isImgFirst: true,
        renderTo: 'dvWork',
        //		margin:'0 0 0 0',
        hidden: __getHidden("create"),
        handler: function () {
            fnAddEditView();

        }
    });
 //操作区域
//    pnlWork = Ext.create('Ext.panel.Panel', {
//        width: '100%',
//        height:25,
//        id: 'pnlWork',
//        renderTo: 'dvWork', 
//		items: [btnAdd],
//        cls:'panel_create',       
//        margin: '10 0 0 0',
//        layout: 'column',
//        border: 0
//    });

    //导出按钮
    btnExport = Ext.create('Jit.button.Button', {
        // componentCls: 'create',
        text: '导&nbsp;&nbsp;出',
        jitIsHighlight: false,
        jitIsDefaultCSS: true,
        renderTo: 'dvExport',
        hidden: __getHidden("export"),
        handler: function () {
            fnExport();
        }
    });

    //数据操作
    var otherColumns = new Array();
    if (!__getHidden("delete")) {
        otherColumns.push({ text: '操作', width: 50, sortable: false, renderer: fnRenderDelete });
    }

    pnlSearch = Ext.create('Jit.panel.JITStoreSearchPannel',
     { id: 'search',
         margin: '10 0 0 0',
         layout: 'column',
         border: 0,
         BtnCode: "search",
         renderTo: 'dvSearch',
         ajaxPath: '/Module/BasicData/Store/Handler/StoreHandler.ashx?mid=' + __mid
     });
    gridStoreList = Ext.create('Ext.JITStoreGrid.Panel',
                            {
                                jitSize: "big",
                                renderTo: 'dvGrid',
                                height: 430,
                                pageSize: 15,
                                pageIndex: 1,
                                CheckMode: 'MULTI',
                                KeyName: "StoreID",
                                BtnCode: 'search',
                                ajaxPath: '/Module/BasicData/Store/Handler/StoreHandler.ashx?mid=' + __mid,
                                isHaveCheckMode: false,
                                editKeyName: 'StoreName',
                                otherGridColumns: otherColumns

                            });
    pnlSearch.grid = gridStoreList;
    pnlEditView = Ext.create('Jit.window.JITStoreFrmWindow', {
        jitSize: "large",
        ajaxPath: '/Module/BasicData/Store/Handler/StoreHandler.ashx?mid=' + __mid,
        grid: gridStoreList,
        BtnCode: 'search',
        clientDistributorID: pClientDistributorID
    });
 
}