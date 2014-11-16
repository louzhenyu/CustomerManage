function InitView() {
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "MemberName",
            fieldLabel: '会员名称',
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

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: 'span_panel3',
        items: [{
            xtype: "jitbutton",
            imgName: 'create',
            hidden: __getHidden("create"),
            handler: fnCreate,
            isImgFirst: true
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    new Ext.PagingToolbar({
        id: "pageBar",
        displayInfo: true,
        defaultType: 'button',
        store: Ext.getStore("EnterpriseMemberStore"),
        pageSize: 15
    });
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("EnterpriseMemberStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'EnterpriseMemberID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '名称',
            width: 110,
            sortable: true,
            dataIndex: 'MemberName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '地址',
            width: 110,
            sortable: true,
            dataIndex: 'Address',
            align: 'left',
            renderer: fnColumnUpdate
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