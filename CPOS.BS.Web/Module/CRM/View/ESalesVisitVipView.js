function InitView() {

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("ESalesVisitVipStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 367,
        width: "100%",
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("ESalesVisitVipStore"),
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
            text: '联系人',
            flex:true,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"" + value + "\" target=\"_blank\">" + value + "</a>";
                return value;
            }
        },
        {
            text: '部门',
            width: 150,
            sortable: true,
            dataIndex: 'Department',
            align: 'left'
        },
        {
            text: '职位',
            width: 150,
            sortable: true,
            dataIndex: 'Position',
            align: 'left'
        },
        {
            text: '决策作用',
            width: 150,
            sortable: true,
            dataIndex: 'PDRoleName',
            align: 'left'
        }
        ]
    });
}