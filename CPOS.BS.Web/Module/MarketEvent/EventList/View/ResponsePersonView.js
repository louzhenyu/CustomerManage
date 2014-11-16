function InitView() {
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("responsePersonStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 450,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("responsePersonStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [{
            text: '操作',
            width: 1,
            sortable: true,
            dataIndex: 'ReponseID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
            }
        }
        , {
            text: '卡号',
            width: 100,
            sortable: true,
            dataIndex: 'VipCode',
            align: 'left'
        },
        {
            text: '等级',
            width: 100,
            sortable: true,
            dataIndex: 'VipLevel',
            align: 'left'
        },
        {
            text: '姓名',
            width: 150,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        },
        {
            text: '客单价',
            width: 100,
            sortable: true,
            dataIndex: 'CustomerPrice',
            align: 'left'
        }, {
            text: '件单价',
            width: 100,
            sortable: true,
            dataIndex: 'UnitPrice',
            align: 'left'
        }, {
            text: '购买件数',
            width: 100,
            sortable: true,
            dataIndex: 'PurchaseNumber',
            align: 'left'
        }, {
            text: '消费积分',
            width: 100,
            sortable: true,
            dataIndex: 'SalesIntegral',
            align: 'left'
        }, {
            text: '购买金额',
            width: 100,
            sortable: true,
            dataIndex: 'PurchaseAmount',
            align: 'left'
        }, {
            text: '购买频次',
            width: 100,
            sortable: true,
            dataIndex: 'PurchaseCount',
            align: 'left'
        }, {
            text: '统计时间',
            width: 150,
            sortable: true,
            dataIndex: 'StatisticsTime',
            align: 'left'
        }

        ]
    });
}
