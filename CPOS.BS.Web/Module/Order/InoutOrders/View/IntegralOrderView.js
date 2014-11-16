
function InitView() {
    //   查询控件
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "商品编号",
            id: "txtItemCode",
            name: "item_code",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "商品名称",
            id: "txtItemName",
            name: "item_name",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "会员",
            id: "txtVipName",
            name: "VipName",
            jitSize: 'small'
        }]
    });

    // 导出按钮
    Ext.create('Jit.button.Button', {
        text: "导出",
        renderTo: "btn_excel",
        handler: fnSearchExcel
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //查询按钮
    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        height: 42,
        items: [{
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: function () {
                fnSearch();
            }
        }]
    });

    // 查询的数据集
    var grid = Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("orderIntegralStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar0",
            defaultType: 'button',
            store: Ext.getStore("orderIntegralStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
//        selModel: Ext.create('Ext.selection.CheckboxModel', {
//            mode: 'MULTI'
//        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [{
            text: '订单编号',
            width: 260,
            sortable: true,
            dataIndex: 'OrderIntegralID',
            align: 'left'
        }, {
            text: '下单时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTimeFormat',
            align: 'left'
        }, {
            text: '商品名称',
            width: 110,
            sortable: true,
            dataIndex: 'item_name',
            align: 'left'
        }, {
            text: '商品编号',
            width: 110,
            sortable: true,
            dataIndex: 'item_code',
            align: 'left'
        }, {
            text: '商品积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integral',
            align: 'right'
        }, {
            text: '兑换数量',
            width: 110,
            sortable: true,
            dataIndex: 'Quantity',
            align: 'right'
        }, {
            text: '总积分',
            width: 110,
            sortable: true,
            dataIndex: 'IntegralAmmount',
            align: 'right'
        }, {
            text: '会员',
            width: 110,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        }, {
            text: '会员编号',
            width: 110,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
        }, {
            text: '收货人',
            width: 110,
            sortable: true,
            dataIndex: 'LinkMan',
            align: 'left'
        }, {
            text: '收货电话',
            width: 110,
            sortable: true,
            dataIndex: 'LinkTel',
            align: 'left'
        }, {
            text: '收货地址',
            width: 150,
            sortable: true,
            dataIndex: 'Address',
            align: 'left'
        }]
    });

}