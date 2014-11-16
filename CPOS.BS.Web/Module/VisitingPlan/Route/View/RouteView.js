function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "txtRouteName",
            name: "RouteName",
            fieldLabel: "路线名称",
            jitSize: 'small'
        }, {
            xtype: "jitbizclientstructure",
            fieldLabel: "部门",
            id: "ClientStructureID",
            isDefault: true
        }, {
            xtype: "JITStoreSelectPannel",
            id: "ClientUserID",
            margin: '0 0 0 0',

            fieldLabel: '执行人员',
            layout: 'column',
            border: 0,
            CheckMode: 'SINGLE',
            CorrelationValue: 0,//所有人员
            KeyName: "ClientUserID", 
            KeyText: "Name",
            ajaxPath: '/Module/BasicData/ClientUser/Handler/ClientUserPositionHandler.ashx'
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
    //operator area
    Ext.create('Jit.button.Button', {
        imgName: 'create',
        renderTo: "span_create",
        hidden: __getHidden("create"),
        isImgFirst: true,
        handler: fnCreate
    });
    //list area

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("routeStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'RouteID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '路线名称',
            width: 110,
            sortable: true,
            dataIndex: 'RouteName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '执行人员',
            width: 110,
            sortable: true,
            dataIndex: 'UserName',
            align: 'left'
        }, {
            text: '职位',
            width: 110,
            sortable: true,
            dataIndex: 'PositionName',
            align: 'left'
        }, {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'Status',
            align: 'left',
            renderer: fnRenderStatus
        }, {
            text: '开始时间',
            width: 110,
            sortable: true,
            dataIndex: 'StartDate',
            align: 'left',
            xtype: "jitcolumn",
            jitDataType: 'date'
        }, {
            text: '结束时间',
            width: 110,
            sortable: true,
            dataIndex: 'EndDate',
            align: 'left',
            xtype: "jitcolumn",
            jitDataType: 'date'
        }, {
            text: '备注',
            width: 310,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("routeStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
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