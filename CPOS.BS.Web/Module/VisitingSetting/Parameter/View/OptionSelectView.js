function InitOptionWindowView() {

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("optionsListStore"),
        id: "optionSelectGridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'OptionName',
            align: 'left',
            hidden: __getHidden("delete"),
            renderer: fnColumnSelect
        }, {
            text: '名称',
            width: 110,
            sortable: true,
            dataIndex: 'OptionName',
            align: 'left'
        }, {
            text: '选项个数',
            width: 110,
            sortable: true,
            dataIndex: 'OptionCount',
            align: 'left'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "optionSelectPageBar",
            defaultType: 'button',
            store: Ext.getStore("optionsListStore"),
            pageSize: JITPage.PageSize.getValue()
        })
    });

    Ext.create('Ext.window.Window', {
        height: 350,
        id: "optionSelectWin",
        title: '数据选项选择',
        width: 400,
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("optionSelectGridView")],
        border: 0,
        closeAction: 'hide'
    });
}