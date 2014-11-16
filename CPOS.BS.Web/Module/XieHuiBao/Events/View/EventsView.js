function InitEditView() {
    /*列表查询面板*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    Ext.getCmp("searchPanel").add({
        xtype: "jittextfield",
        fieldLabel: "姓名",
        name: "VipName",
        id: "txt_VipName"
    });


    /*列表按钮面板*/
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: 'span_panel2',
        items: [{
            xtype: "jitbutton",
            imgName: 'search',
            hidden: __getHidden("search"),
            handler: fnSearch,
            isImgFirst: true
        }, {
            xtype: "jitbutton",
            imgName: 'reset',
            hidden: __getHidden("search"),
            handler: function () {
                Ext.getCmp("searchPanel").getForm().reset();
            }
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    Ext.create('Jit.button.Button', {
        xtype: "jitbutton",
        text: "导出数据",
        jitIsHighlight: true,
        jitIsDefaultCSS: true,
        handler: fnExportData,
        renderTo: 'span_create'
    });



    /*列表分页*/
    new Ext.PagingToolbar({
        id: "pageBar",
        displayInfo: true,
        defaultType: 'button',
        store: Ext.getStore("vipStore"),
        pageSize: JITPage.HandlerUrl.getValue()
    });


    /*明细面板*/
    Ext.create('Ext.panel.Panel', {
        id: 'showPanel',
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'column',
        border: 0,
        autoScroll: true
    });



    /*列表*/
    Ext.create('Ext.grid.Panel', {
        id: "gridlist",
        store: Ext.getStore("vipStore"),
        columns: eval("[]"),
        height: 390,
        stripeRows: true,
        columnLines: true,
        width: "100%",
        bbar: Ext.getCmp("pageBar"),
        renderTo: 'DivGridView',
        viewConfig: {
            loadMask: true
        }
    });
}