function InitView() {
    /*创建查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitdatefield",
            id: "pStartDate",
            fieldLabel: "开始日期",
            jitSize: 'small',
            value: Ext.Date.add(new Date, Ext.Date.DAY, -3)
        }, {
            xtype: "jitdatefield",
            id: "pEndDate",
            fieldLabel: "结束日期",
            jitSize: 'small',
            value: new Date()
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    /*查询按钮区域*/
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        items: [{
            xtype: "jitbutton",
            height: 22,
            isImgFirst: true,
            text: "查询",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: __getHidden("search"),
            handler: fnSearch
        }, {
            xtype: "jitbutton",
            height: 22,
            text: "重置",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: function () {
                Ext.getCmp("searchPanel").getForm().reset();
            }
        }],
        renderTo: 'span_panel2',
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    /*查询列表区域*/
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipClearStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("vipClearStore"),
            pageSize: 15
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [{
            text: '清洗日期',
            width: 130,
            sortable: true,
            xtype: 'jitcolumn',
            jitDataType: 'Date',
            dataIndex: 'CreateTime'
        }, {
            text: '无效数据',
            width: 130,
            sortable: true,
            dataIndex: 'InvalidNum',
            align: 'right',
            renderer: function (value, meta, r) {
                return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnShowWindow('" + r.data.VIPClearID + "',1);\">" + value + "</a>";
            }
        }, {
            text: '重复数据',
            width: 130,
            sortable: true,
            dataIndex: 'DuplicateNum',
            align: 'right',
            renderer: function (value, meta, r) {
                return html = "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnShowWindow('" + r.data.VIPClearID + "',2);\">" + value + "</a>";
            }
        }, {
            text: '缺憾数据',
            width: 130,
            sortable: true,
            dataIndex: 'DrawbackNum',
            align: 'right',
            renderer: function (value, meta, r) {
                return html = "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnShowWindow('" + r.data.VIPClearID + "',3);\">" + value + "</a>";
            }
        }]
    });

    /*iframe弹出框*/
    Ext.create('Jit.window.Window', {
        title: '',
        height: 560,
        width: 1100,
        jitSize: "large",
        id: "showVipWin",
        layout: 'fit',
        draggable: true,
        border: 1,
        html: '<iframe id="showVipFrame" frameborder="no" height="100%" width="100%" marginheight="0" marginwidth="0" scrolling="auto"  src=""></iframe>',
        closeAction: 'hide'
    });
}