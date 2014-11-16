function InitView() {

    Ext.create('Jit.button.Button', {
        imgName: 'create',
        isImgFirst: true,
        margin: '0 0 0 10',
        renderTo: 'dvWork',
        handler: function () {
            fnAddEditView();
        }
    });

//    Ext.create('Jit.button.Button', {
//        text: "导入",
//        jitIsHighlight: false,
//        jitIsDefaultCSS: true,
//        renderTo: 'dvWork',
//        handler: function () {
//            fnImport();
//        }
//    });

    Ext.create("Jit.biz.FileUpload", {
        id: 'FileUploadID'
                , fieldLabel: '会员上传'  //上传空间Label
                , uploadTitle: '导入会员数据'
                , ajaxPath: '/Module/Vip/VipSearchNew/Handler/VipHandler.ashx?mid=11'
                , photoType: 1 // 1 为会员           
                , margin: '10'
    });

    Ext.create('Jit.button.Button', {
        text: "导出",
        jitIsHighlight: false,
        jitIsDefaultCSS: true,
        renderTo: 'dvWork',
        handler: function () {
            fnExport();
        }
    });

    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitcombobox",
            fieldLabel: "角色",
            id: "RoleTableName",
            name: "RoleTableName",
            store: Ext.getStore("RoleTableNameStore"),
            displayField: 'role_name',
            valueField: 'table_name'
        }],
        renderTo: 'dvRole',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    pnlSearch = Ext.create('Jit.panel.JITStoreSearchPannel', {//查询内容的控件
        id: 'search', 
        margin: '10 0 0 0',
        layout: 'column',
        border: 0,
        BtnCode: "search",
        renderTo: 'dvSearch',//查询内容的位置
        ajaxPath: '/Module/Vip/VipSearchNew/Handler/VipHandler.ashx?mid=1&tablename=' + tableName
    });

    var otherColumns = new Array();
    otherColumns.push({ text: '操作', width: 50, sortable: false, renderer: fnRenderDelete });
    
    gridStoreList = Ext.create('Ext.JITStoreGrid.Panel', {//数据表格
        jitSize: "big",
        renderTo: 'dvGrid',
        height: 430,
        pageSize: 15,
        pageIndex: 1,
        CheckMode: 'MULTI',
        KeyName: "VIPID",
        BtnCode: 'search',
        ajaxPath: '/Module/Vip/VipSearchNew/Handler/VipHandler.ashx?mid=1&tablename=' + tableName,
        isHaveCheckMode: false,
        editKeyName: 'VipName',
        CorrelationValue: getUrlParam("type") + "&" + getUrlParam("paramValue"),
        otherGridColumns: otherColumns
    });
    pnlSearch.grid = gridStoreList;

    pnlEditView = Ext.create('Jit.window.JITVipFrmWindow', {
        jitSize: "large",
        ajaxPath: '/Module/Vip/VipSearchNew/Handler/VipHandler.ashx?mid=1&tablename=' + tableName,
        grid: gridStoreList,
        BtnCode: 'search'
    });
}