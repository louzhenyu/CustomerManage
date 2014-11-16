function InitView() {
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("EventVipTicketStore"),
        id: "wgridView",
        renderTo: "wdivBtn",
        columnLines: true,
        height: 367,
        width: "100%",
        stripeRows: true,
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "wpageBar",
            defaultType: 'button',
            store: Ext.getStore("EventVipTicketStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [
        {
            text: '姓名',
            width: 100,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        },
                {
                    text: '电话',
                    width: 100,
                    sortable: true,
                    dataIndex: 'Phone',
                    align: 'left'
                },
                {
                    text: '邮箱',
                    width: 100,
                    sortable: true,
                    dataIndex: 'Email',
                    align: 'left'
                },
                {
                    text: '公司',
                    width: 120,
                    sortable: true,
                    dataIndex: 'Col5',
                    align: 'left'
                },
                {
                    text: '职位',
                    width: 120,
                    sortable: true,
                    dataIndex: 'Col6',
                    align: 'left'
                },
                {
                    text: '票名',
                    width: 120,
                    sortable: true,
                    dataIndex: 'TicketName',
                    align: 'left'
                },
                {
                    text: '票价',
                    width: 120,
                    sortable: true,
                    dataIndex: 'TicketPrice',
                    align: 'left'
                }
        ]
    });
}