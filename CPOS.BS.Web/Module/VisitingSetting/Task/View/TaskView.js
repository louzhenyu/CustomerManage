function InitView() {
    Ext.create('Jit.button.Button', {
        renderTo: "span_create",
        hidden: __getHidden("create"),
        handler: fnCreate,
        imgName: 'create',
        isImgFirst: true
    });

    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitbizroleposition",
            id: "ClientPositionID",
            fieldLabel: '职位信息',
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
        store: Ext.getStore("taskStore"),
        pageSize: 15
    });
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("taskStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'VisitingTaskID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '任务名称',
            width: 110,
            sortable: true,
            dataIndex: 'VisitingTaskName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '职位',
            width: 110,
            sortable: true,
            dataIndex: 'ClientPositionText',
            align: 'left'
        }, {
            text: '终端类型',
            width: 110,
            sortable: true,
            dataIndex: 'POPTypeText',
            align: 'left'
        }, {
            xtype: 'jitcolumn',
            jitDataType: 'int',
            text: '终端数量',
            width: 110,
            sortable: true,
            dataIndex: 'POPCount',
            align: 'left'
        }, {
            text: '开始时间',
            width: 110,
            sortable: true,
            xtype: "jitcolumn",
            dataIndex: 'StartDate',
            jitDataType: 'date',
            align: 'left'
        }, {
            text: '结束时间',
            width: 110,
            sortable: true,
            xtype: "jitcolumn",
            dataIndex: 'EndDate',
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