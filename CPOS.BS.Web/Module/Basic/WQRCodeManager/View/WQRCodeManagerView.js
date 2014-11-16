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
            xtype: "jittextfield",
            fieldLabel: "二维码号码",
            id: "txtQRCode",
            name: "QRCode",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbizwqrcodetype",
            fieldLabel: "类型标识",
            id: "txtQRCodeTypeId",
            name: "QRCodeTypeId",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
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

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("wQRCodeManagerStore"),
        id: "gridView",
        renderTo: "DivGridView",
        columnLines: true,
        height: DefaultGridHeight,
        width: DefaultGridWidth,
        stripeRows: true,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("wQRCodeManagerStore"),
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
            width: 60,
            sortable: true,
            dataIndex: 'QRCodeId',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                return str;
            }
        }
        , {
            text: '二维码号码',
            width: 200,
            sortable: true,
            dataIndex: 'QRCode',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.QRCodeId + "')\">" + value + "</a>";
                return str;
            }
        }, {
            text: '类型名称',
            width: 200,
            sortable: true,
            dataIndex: 'QRCodeTypeName',
            align: 'left'
        }, {
            text: '使用状态',
            width: 200,
            sortable: true,
            dataIndex: 'IsUse',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                if (value == "1") str = "是";
                else if (value == "0") str = "否";
                return str;
            }
        },
        {
            text: '创建时间',
            width: 130,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        },
        ]
    });
}