function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizclientstructure",
            fieldLabel: "部门",
            id: "ClientStructureID",
            isDefault: true
        }, {
            xtype: "jitbizclientposition",
            fieldLabel: "职位",
            id: "ClientPositionID",
            isDefault: true
        }, {
            xtype: "JITStoreSelectPannel",
            id: "ClientUserID",
            margin: '0 0 0 0',

            fieldLabel: '执行人员',
            layout: 'column',
            border: 0,
            CheckMode: 'SINGLE',
            CorrelationValue: 0,
            KeyName: "ClientUserID", //主健ID
            KeyText: "Name", //显示健值
            ajaxPath: '/Module/BasicData/ClientUser/Handler/ClientUserPositionHandler.ashx'
        }, {
            xtype: "jitmonthfield",
            id: "CallDate",
            fieldLabel: "月度",
            format: 'Y-m',
            jitSize: 'small',
            name: "CallDate"
        }
           ],
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
//    Ext.create('Jit.button.Button', {
//        text: __getText("create"),
//        renderTo: "span_create",
//        hidden: __getHidden("create"),
//        handler: fnCreate
//    });
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("userStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '人员名称',
            width: 110,
            sortable: true,
            dataIndex: 'UserName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '职位',
            width: 110,
            sortable: true,
            dataIndex: 'PositionName',
            align: 'left'
        }, {
            text: '部门名称',
            width: 110,
            sortable: true,
            dataIndex: 'StructureName',
            align: 'left'
        }, {
            text: '计划拜访次数',
            width: 110,
            sortable: true,
            dataIndex: 'POPCount',
            align: 'left'
        }, {
            text: '月拜访指标',
            width: 110,
            sortable: true,
            dataIndex: 'IsMustDo',
            align: 'left',
            hidden:true
        }, {
            text: '已计划任务',
            width: 110,
            sortable: true,
            dataIndex: 'MaxValue',
            align: 'left',
            hidden: true
        }, {
            text: '计划达标率',
            width: 110,
            sortable: true,
            dataIndex: 'MinValue',
            align: 'left',
            hidden: true
        }, {
            text: '应增加拜访',
            width: 110,
            sortable: true,
            dataIndex: 'MinValue',
            align: 'left',
            hidden: true
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("userStore"),
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