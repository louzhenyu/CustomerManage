function InitView() {

    //operator area 
    Ext.create('Jit.button.Button', {
        text: "查询",
        renderTo: "btnSearch",
        handler: fnSearch,
        jitIsHighlight: true,
        jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "确定",
        renderTo: "btnConfirm",
        handler: fnConfirm,
        jitIsHighlight: true,
        jitIsDefaultCSS: false
    });
    Ext.create('jit.biz.AlbumModuleType', {
        id: "txtAlbumModuleType",
        text: "",
        renderTo: "txtAlbumModuleType",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtModuleName",
        text: "",
        renderTo: "txtModuleName",
        width: 100
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("AlbumModuleStore"),
        id: "gridView",
        renderTo: "gridView",
        columnLines: true,
        height: 389,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'SINGLE'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("AlbumModuleStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '标题',
            width: 300,
            sortable: true,
            dataIndex: 'Title',
            align: 'left'
        },
        {
            text: '发布时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        ]
    });
}