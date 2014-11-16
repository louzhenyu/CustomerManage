function InitView() {

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventsRoundListStore"),
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
            store: Ext.getStore("eventsRoundListStore"),
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
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'RoundId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '轮次',
            width: 140,
            sortable: true,
            dataIndex: 'RoundDesc',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.RoundId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '启用',
            width: 140,
            sortable: true,
            dataIndex: 'RoundStatusName',
            align: 'left'
        },
        {
            text: '奖品数量',
            width: 100,
            sortable: true,
            dataIndex: 'PrizesCount',
            align: 'left',
            flex:true
        },
        {
            text: '中奖数量',
            width: 100,
            sortable: true,
            dataIndex: 'WinnerCount',
            align: 'left',
            flex:true
        },
        {
            text: '创建时间',
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

    
    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "生成奖品池",
        renderTo: "span_pub",
        //hidden: __getHidden("create"),
        handler: fnPool
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
}