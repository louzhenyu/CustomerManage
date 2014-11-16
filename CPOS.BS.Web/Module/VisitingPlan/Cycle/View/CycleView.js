function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "txtCycleName",
            name: "CycleName",
            fieldLabel: "周期名称",
            jitSize: 'small'
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
    Ext.create('Jit.button.Button', {
        imgName: 'create',
        renderTo: "span_create",
        hidden: __getHidden("create"),
        isImgFirst: true,
        handler: fnCreate
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("listStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'CycleID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '周期名称',
            width: 110,
            sortable: true,
            dataIndex: 'CycleName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '周期类型',
            width: 110,
            sortable: true,
            dataIndex: 'CycleType',
            renderer: fnRenderType
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
            text: '周期天数',
            width: 110,
            sortable: true,
            dataIndex: 'CycleDay'
        }, {
            text: '备注',
            width: 310,
            sortable: true,
            dataIndex: 'Remark'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("listStore"),
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


    //window
    Ext.create('Ext.form.Panel', {
        id: "cycleEditPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>周期名称",
            name: "CycleName",
            allowBlank: false
        }, {
            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>周期类型",
            id: "CycleType",
            name: "CycleType",
            store: Ext.getStore("cycleTypeStore"),
            displayField: 'name',
            disabled:true,
            valueField: 'value',
            listeners: {
                "change": fnCycleTypeChange
            }
        }, {
            xtype: "jitdatefield",
            fieldLabel: "<font color='red'>*</font>开始日期",
            id: "StartDate",
            name: "StartDate",
            allowBlank: false
        }, {
            xtype: "jitdatefield",
            fieldLabel: "结束日期",
            id: "EndDate",
            name: "EndDate",
            vtype: "enddate",
            begindateField: "StartDate"
        }, {
            xtype: "jitnumberfield",
            fieldLabel: "周期天数",
            id:"CycleDay",
            name: "CycleDay",
            vtype: "positiveInteger",
            minValue:1,
            maxValue:31
        }, {
            columnWidth: 1,
            layout: 'column',
            id: "columnCycleDetail",
            hidden: false,
            style:"margin-left:30px;",
            border: 0,
            items: []
        }]
    });

    Ext.create('Jit.window.Window', {
        height: 450,
        id: "cycleEditWin",
        title: '选项选择',
        jitSize: 'big',
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("cycleEditPanel")],
        border: 0,
        closeAction: 'hide',
        buttons: [{
            xtype: "jitbutton",
            imgName: 'save',
            id: "btnSave",
            isImgFirst: true,
            handler: fnSubmit
        }, {
            xtype: "jitbutton",
            imgName: 'cancel',
            handler: fnCancel
        }]
    });
}