function InitView() {

    
    
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventsLotteryLogListStore"),
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
            store: Ext.getStore("eventsLotteryLogListStore"),
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
            text: '会员名称',
            width: 140,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
        },
        {
            text: '抽奖次数',
            width: 140,
            sortable: true,
            dataIndex: 'LotteryCount',
            align: 'left'
        },
        {
            text: '时间',
            width: 140,
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
        buttons: [
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}