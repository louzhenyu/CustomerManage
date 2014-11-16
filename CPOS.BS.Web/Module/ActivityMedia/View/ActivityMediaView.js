function InitView() {
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "MediaTitle",
            fieldLabel: '标题',
            isDefault: true
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

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
            handler: fnReset
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    new Ext.PagingToolbar({
        id: "pageBar",
        displayInfo: true,
        defaultType: 'button',
        store: Ext.getStore("ActivityMediaStore"),
        pageSize: 15
    });
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("ActivityMediaStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'ActivityMediaID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '活动标题',
            width: 110,
            sortable: true,
            dataIndex: 'ActivityTitle',
            align: 'left'
        }, {
            text: '媒体标题',
            width: 110,
            sortable: true,
            dataIndex: 'MediaTitle',
            align: 'left'
        }, {
            text: '媒体类型',
            width: 110,
            sortable: true,
            dataIndex: 'MediaTypeText',
            align: 'left'
        }, {
            text: '文件名',
            width: 110,
            sortable: true,
            dataIndex: 'FileName',
            align: 'left'
        }, {
            text: '创建时间',
            width: 110,
            sortable: true,
            xtype: "jitcolumn",
            dataIndex: 'CreateTime',
            jitDataType: 'date',
            align: 'left'
        }, {
            text: '备注',
            width: 310,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }],
        height: 500,
        stripeRows: true,
        width: "100%",
        bbar: Ext.getCmp("pageBar"),
        renderTo: "DivGridView",
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });
}