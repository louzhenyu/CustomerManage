function InitView() {
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventAnalysisStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 70,
        width: "100%",
        stripeRows: true,
//        selModel: Ext.create('Ext.selection.CheckboxModel', {
//            mode: 'MULTI'
//        })
//        ,
//        bbar: new Ext.PagingToolbar({
//            displayInfo: true,
//            id: "pageBar",
//            defaultType: 'button',
//            store: Ext.getStore("eventAnalysisStore"),
//            pageSize: JITPage.PageSize.getValue()
//        }),
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
            dataIndex: 'MarketEventID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
            }
        }
        , {
            text: '执行活动时间',
            width: 200,
            sortable: true,
            dataIndex: 'BeginDate',
            align: 'left'
        },
        {
            text: '参与门店',
            width: 80,
            sortable: true,
            dataIndex: 'StoreCount',
            align: 'left'
        },
        {
            text: '响应门店',
            width: 80,
            sortable: true,
            dataIndex: 'ResponseStoreCount',
            align: 'left'
        },
        {
            text: '门店响应率',
            width: 80,
            sortable: true,
            dataIndex: 'ResponseStoreRate',
            align: 'left'
        }, {
            text: '邀约人数',
            width: 80,
            sortable: true,
            dataIndex: 'PersonCount',
            align: 'left'
        }, {
            text: '响应人数',
            width: 80,
            sortable: true,
            dataIndex: 'ResponsePersonCount',
            align: 'left'
        }, {
            text: '会员响应率',
            width: 80,
            sortable: true,
            dataIndex: 'ResponsePersonRate',
            align: 'left'
        }, {
            text: '预算总费用',
            width: 80,
            sortable: true,
            dataIndex: 'BudgetTotal',
            align: 'left'
        }, {
            text: '当前消费额',
            width: 80,
            sortable: true,
            dataIndex: 'CurrentSales',
            align: 'left'
        }, {
            text: '活动毛利',
            width: 80,
            sortable: true,
            dataIndex: 'EventMaori',
            align: 'left'
        }, {
            text: '活动净利润',
            width: 80,
            sortable: true,
            dataIndex: 'EventNetProfit',
            align: 'left'
        }

        ]
    });




}
