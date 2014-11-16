function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jitbizleventselecttree",
            fieldLabel: "活动标题",
            id: "EventID",
            name: "EventID",
            dataType: "ParentEvent",
            jitSize: 'small'
        } ,
        {
            xtype: "jittextfield",
            fieldLabel: "票务名称",
            id: "TicketName",
            name: "TicketName",
            jitSize: 'small'
        }
        ]
    });

    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: fnSearch
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        }
        ,{
            xtype: "jitbutton",
            text: "重置",
            margin: '0 0 10 14',
            handler: fnReset
        }
        ]
    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("ticketStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: 367,
        width: "100%",
        stripeRows: true,
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("ticketStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        },
        columns: [{
            text: '操作',
            width: 50,
            sortable: true,
            dataIndex: 'TicketID',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        },
        {
            text: '票务名称',
            width: 200,
            sortable: true,
            dataIndex: 'TicketName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.TicketID + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '活动标题',
            width: 200,
            sortable: true,
            dataIndex: 'Title',
            align: 'left'
        },
        {
            text: '票务备注',
            width: 240,
            sortable: true,
            dataIndex: 'TicketRemark',
            align: 'left'
        },
        {
            text: '票务价格',
            width: 80,
            sortable: true,
            dataIndex: 'TicketPrice',
            align: 'left'
        },
        {
            text: '票务数量',
            width: 80,
            sortable: true,
            dataIndex: 'TicketNum',
            align: 'left'
        }
        ]
    });

}