function InitView() {
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventListStore"),
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
            store: Ext.getStore("eventListStore"),
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
            width: 150,
            sortable: true,
            dataIndex: 'NewsId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.EventStatus == '1') {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnModify('" + d.MarketEventID + "')\">修改&nbsp;&nbsp; &nbsp;</a>";

                } else {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnModify('" + d.MarketEventID + "')\">查看&nbsp;&nbsp; &nbsp;</a>";
                }
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnRun('" + d.MarketEventID + "')\">执行 &nbsp;&nbsp; </a>";
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnAnalysis('" + d.MarketEventID + "')\">分析</a>";
                return str;
            }
        }
        , {
            text: '活动代码',
            width: 100,
            sortable: true,
            dataIndex: 'EventCode',
            align: 'left'
        },
        {
            text: '品牌',
            width: 200,
            sortable: true,
            dataIndex: 'BrandName',
            align: 'left'
        },
        {
            text: '状态',
            width: 150,
            sortable: true,
            dataIndex: 'StatusDesc',
            align: 'left'
        },
        {
            text: '类型',
            width: 120,
            sortable: true,
            dataIndex: 'EventType',
            align: 'left'
        }, {
            text: '方式',
            width: 150,
            sortable: true,
            dataIndex: 'EventModeDesc',
            align: 'left'
        }, {
            text: '参与门店',
            width: 150,
            sortable: true,
            dataIndex: 'StoreCount',
            align: 'left'
        }, {
            text: '邀约人数',
            width: 150,
            sortable: true,
            dataIndex: 'PersonCount',
            align: 'left'
        }, {
            text: '预算总费用',
            width: 150,
            sortable: true,
            dataIndex: 'BudgetTotal',
            align: 'left'
        }, {
            text: '实际开始',
            width: 150,
            sortable: true,
            dataIndex: 'BeginTime',
            align: 'left'
        }, {
            text: '实际结束',
            width: 150,
            sortable: true,
            dataIndex: 'EndTime',
            align: 'left'
        }

        ]
    });
}
