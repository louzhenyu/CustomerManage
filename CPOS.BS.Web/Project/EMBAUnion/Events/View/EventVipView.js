function InitEditView() {
    /*列表查询面板*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            fieldLabel: "姓名",
            name: "VipName",
            id:"txt_VipName"
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
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

    /*列表*/
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'SignUpID',
            align: 'left',
            renderer: fnColumnDelete
        }, {
            xtype: "jitcolumn",
            jitDataType: 'Tips',
            text: '姓名',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'Tips',
            text: '手机',
            width: 110,
            dataIndex: 'Phone',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'Tips',
            text: '邮箱',
            width: 110,
            dataIndex: 'Email',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'Tips',
            text: '公司',
            width: 110,
            dataIndex: 'Col5',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'Tips',
            text: '职位',
            width: 110,
            dataIndex: 'Col6',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'Tips',
            text: '学校',
            width: 110,
            dataIndex: 'VipSchool',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'string',
            text: '期/班级',
            width: 110,
            dataIndex: 'Col2',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'string',
            text: '课程',
            width: 110,
            dataIndex: 'VipClass',
            align: 'left'
        }],
        forceFit: true,
        height: 367,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("vipStore"),
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