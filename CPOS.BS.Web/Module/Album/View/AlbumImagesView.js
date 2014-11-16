function InitView() {

    //editPanel area
    Ext.create('Jit.button.Button', {
        text: "添加相片",
        renderTo: "btnCreate",
        handler: fnCreate
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("AlbumImagesStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 445,
        width: "100%",
        stripeRows: true,
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("AlbumImagesStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '',
            width: 0
        },
        {
            text: '操作',
            width: 120,
            sortable: true,
            dataIndex: 'PhotoId',
            align: 'center',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnView('" + value + "')\">编辑</a>";
                str += "&nbsp;&nbsp;<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '序号',
            width: 80,
            sortable: true,
            dataIndex: 'SortOrder',
            align: 'center'
        },
        {
            text: '标题',
            width: 150,
            sortable: true,
            dataIndex: 'Title',
            align: 'center'
        },
        {
            text: '阅读数',
            width: 80,
            sortable: true,
            dataIndex: 'ReaderCount',
            align: 'center'
        },
        {
            text: '图片',
            width: 200,
            sortable: true,
            dataIndex: '',
            align: 'center',
            flex: true,
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a href=\"" + d.LinkUrl + "\" target=\"_blank\"><img style=\"width:134px; height:100px; border:0px;\" src=\"" + d.LinkUrl + "\"/></a>";
                return str;
            }
        }
        ]
    });
}