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
//        {
//            xtype: "jitbizoptions",
//            fieldLabel: "参数类型",
//            OptionName: 'ParameterType',
//            name: "ParameterType",
//            isDefault: true
//        }, 
        {
            xtype: "jittextfield",
            fieldLabel: "单据号码",
            id: "txtOrderNo",
            name: "order_no",
            jitSize: 'small'
        }, 
//        {
//            xtype: "jitbizoptions",
//            fieldLabel: "供应商",
//            OptionName: 'SupplierUnitType',
//            name: "sales_unit_id",
//            isDefault: true
//        }, 
        {
            xtype: "jitbizsupplierunit",
            fieldLabel: "供应商",
            id: "txtSupplierUnitType",
            name: "sales_unit_id",
            jitSize: 'small'
        }, 
        {
            xtype: "jitbizorderstatus",
            fieldLabel: "单据状态",
            id: "txtOrderStatus",
            name: "order_status",
            jitSize: 'small',
            orderType: 'order_status_po'
        }, 
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "采购单位",
            id: "txtPurchaseUnit",
            name: "purchase_unit_id",
            jitSize: 'small'
        },
        {
            xtype:'panel',
            colspan:2,
            layout:'hbox',
            border: 0,
            bodyBorder: false,
            bodyStyle: 'background:#F1F2F5;',
            hidden: true,
            id: 'txtOrderDate',
            items:[
                {
                    xtype: "jitdatefield",
                    fieldLabel: "单据日期",
                    id: "txtOrderDateBegin",
                    name: "order_date_begin",
                    jitSize: 'small'
                },  
                {
                    xtype: "label",
                    text: "至"
                },   
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtOrderDateEnd",
                    name: "order_date_end",
                    jitSize: 'small',
                    width: 100
                }
            ]
        },
        {
            xtype:'panel',
            colspan:2,
            layout:'hbox',
            border: 0,
            bodyBorder: false,
            bodyStyle: 'background:#F1F2F5;',
            hidden: true,
            id: 'txtRequestDate',
            items:[
                {
                    xtype: "jitdatefield",
                    fieldLabel: "预计日期",
                    id: "txtRequestDateBegin",
                    name: "request_date_begin",
                    jitSize: 'small'
                },  
                {
                    xtype: "label",
                    text: "至"
                },   
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtRequestDateEnd",
                    name: "request_date_end",
                    jitSize: 'small',
                    width: 100
                }
            ]
        }
//       ,{
//            xtype: "jitbutton",
//            text: "查询",
//            //hidden: __getHidden("search"),
//            margin: '0 0 10 14',
//            handler: fnSearch
//        }
        ]

    });

    // btn_panel
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
        //width: 200,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
        }
        , {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView
        }
        ]

    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
    });
    //list area

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("purchaseOrderStore"),
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
            store: Ext.getStore("purchaseOrderStore"),
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
            width: 110,
            sortable: true,
            dataIndex: 'order_id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                if (d.order_status == "1") {
                    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
                    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
                }
                return str;
            }
        }, {
            text: '单据号码',
            width: 110,
            sortable: true,
            dataIndex: 'order_no',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "')\">" + value + "</a>";
                return str;
            }
        }, {
            text: '供应商',
            width: 110,
            sortable: true,
            dataIndex: 'sales_unit_name',
            align: 'left'
        }, {
            text: '采购单位',
            width: 110,
            sortable: true,
            dataIndex: 'purchase_unit_name',
            align: 'left'
        }, {
            text: '单据日期',
            width: 110,
            sortable: true,
            dataIndex: 'order_date',
            align: 'left'
        }, {
            text: '预定日期',
            width: 110,
            sortable: true,
            dataIndex: 'request_date',
            align: 'left'
        }, {
            text: '金额',
            width: 110,
            sortable: true,
            dataIndex: 'total_amount',
            align: 'right'
        }, {
            text: '数量',
            width: 110,
            sortable: true,
            dataIndex: 'total_qty',
            align: 'right'
        }, {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'order_status_desc',
            align: 'left'
        }]
    });
}