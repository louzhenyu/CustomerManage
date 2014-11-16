function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizoptions",
            fieldLabel: "参数类型",
            OptionName: 'ParameterType',
            name: "ParameterType",
            isDefault: true
        }, {
            xtype: "jittextfield",
            id: "txtParameterName",
            name: "ParameterName",
            fieldLabel: "参数名称",
            jitSize: 'small'
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
                isImgFirst:true
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
        store: Ext.getStore("list_parameterStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'VisitingParameterID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '数据名称',
            width: 110,
            sortable: true,
            dataIndex: 'ParameterName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '参数类型',
            width: 110,
            sortable: true,
            dataIndex: 'ParameterTypeText',
            align: 'left'
        }, {
            text: '控件类型',
            width: 110,
            sortable: true,
            dataIndex: 'ControlTypeText',
            align: 'left'
        }, {
            text: '是否必填',
            width: 110,
            sortable: true,
            dataIndex: 'IsMustDo',
            align: 'left',
            renderer: fnColumnMustDo
        }, {
            text: '最大值',
            width: 110,
            sortable: true,
            dataIndex: 'MaxValue',
            align: 'left'
        }, {
            text: '最小值',
            width: 110,
            sortable: true,
            dataIndex: 'MinValue',
            align: 'left'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("list_parameterStore"),
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