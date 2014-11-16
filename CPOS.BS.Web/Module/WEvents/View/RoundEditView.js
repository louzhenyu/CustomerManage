function InitView() {

    //editPanel area
    Ext.create('jit.biz.Round', {
        id: "txtRound",
        text: "",
        renderTo: "txtRound",
        width: 150
    });
    //    Ext.create('jit.biz.YesNoStatus', {
    //        id: "txtStatus",
    //        text: "",
    //        renderTo: "txtStatus",
    //        width: 100
    //    });
    Ext.create('Jit.form.field.ComboBox', {
        id: "txtStatus",
        renderTo: "txtStatus",
        store: Ext.getStore("StatusStore"),
        valueField: "Id",
        displayField: "Name",
        width: 100,
        isDefault: true
    });


    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("RoundPrizesStore"),
        id: "grid",
        renderTo: "grid",
        columnLines: true,
        height: 316,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("RoundPrizesStore"),
            pageSize: 1000
        }),
        plugins: [Ext.create('Ext.grid.plugin.CellEditing', { clicksToEdit: 1 })],
        columns: [
        {
            text: '奖品',
            width: 240,
            sortable: true,
            dataIndex: 'PrizeName',
            align: 'left',
            flex: true
        }
        , {
            xtype: 'numbercolumn',
            header: '奖品数量（点击修改）',
            dataIndex: 'PrizesCount',
            format: '0,0',
            width: 150,
            editor: {
                xtype: 'numberfield',
                allowBlank: false,
                minValue: 0,
                maxValue: 100000
            }
        }
        ,
        {
            text: '已中奖数量',
            width: 150,
            sortable: true,
            dataIndex: 'WinnerCount',
            align: 'left'
        }
        ]
    });

    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            text: "保存",
            formBind: true,
            disabled: true,
            hidden: false,
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}