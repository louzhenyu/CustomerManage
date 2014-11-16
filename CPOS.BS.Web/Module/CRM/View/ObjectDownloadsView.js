function InitView() {

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("ObjectDownloadsStore"),
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
            store: Ext.getStore("ObjectDownloadsStore"),
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
            text: '附件名',
            flex:true,
            sortable: true,
            dataIndex: 'DownloadName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"" + d.DownloadUrl + "\" target=\"_blank\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '排序',
            width: 100,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        ]
    });
    
    //Ext.create('Jit.button.Button', {
    //    text: "关闭",
    //    renderTo: "btnClose",
    //    width: 70,
    //    handler: fnECSearchCustomerClear
    //});

}