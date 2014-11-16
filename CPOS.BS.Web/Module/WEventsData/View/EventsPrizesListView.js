function InitView() {

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("eventsPrizesListStore"),
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
            store: Ext.getStore("eventsPrizesListStore"),
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
            dataIndex: 'PrizesID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '奖品名称',
            width: 140,
            sortable: true,
            dataIndex: 'PrizeName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.PrizesID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '奖品缩写',
            width: 140,
            sortable: true,
            dataIndex: 'PrizeShortDesc',
            align: 'left'
        },
        {
            text: '价格',
            width: 140,
            sortable: true,
            dataIndex: 'Price',
            align: 'left'
        },
        {
            text: '总数量',
            width: 100,
            sortable: true,
            dataIndex: 'CountTotal',
            align: 'left'
        },
        {
            text: '没中奖品数量',
            width: 100,
            sortable: true,
            dataIndex: 'CountLeft',
            align: 'left'
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
}