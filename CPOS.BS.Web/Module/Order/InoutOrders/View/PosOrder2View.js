/*Jermyn 2013-04-01
POS小票
*/

function InitView() {
    //    debugger;
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
            fieldLabel: "单据号码",
            id: "txtOrderNo",
            name: "order_no",
            jitSize: 'small'
        },

        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "门店",
            id: "txtSalesUnit",
            name: "sales_unit_id",
            jitSize: 'small'
        }
//        ,{
//            xtype: "jitbizorderstatus",
//            fieldLabel: "单据状态",
//            colspan: 2,
//            id: "txtOrderStatus",
//            name: "status",
//            jitSize: 'small',
//            orderType: 'order_status_ro'
//            ,hidden: true
//        }
         , {
             xtype: "jitbizpospaytype",
             fieldLabel: "支付方式",
             colspan: 2,
             id: "txtPosPayType",
             name: "DefrayTypeId",
//             hidden: true,
             jitSize: 'small'
         }
        ,{
            xtype: 'panel',
            colspan: 2,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            id: 'txtOrderDate',
            hidden: true,
            bodyStyle: 'background:#F1F2F5;',
            items: [
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
            }, {
                xtype: "jitbizpossendtype",
                fieldLabel: "配送方式",
                colspan: 2,
                id: "txtPosSendType",
                name: "DeliveryId",
                hidden: true,
                jitSize: 'small'
            }
       
        ,{
            xtype: 'panel',
            colspan: 4,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            id: 'txtField9',
            hidden: true,
            bodyStyle: 'background:#F1F2F5;',
            items: [
                {
                    xtype: "jitdatefield",
                    fieldLabel: "发货日期",
                    id: "txtField9Begin",
                    name: "Field9_begin",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtField9End",
                    name: "Field9_end",
                    jitSize: 'small',
                    width: 100
                }
            ]
            }
//            ,
//            {
//                xtype: "label",
//                text: "xxx"
//                ,width:100
//            },
        

        ,{
            xtype: 'panel',
            colspan: 4,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            id: 'txtModifyTime',
            hidden: true,
            bodyStyle: 'background:#F1F2F5;',
            items: [
                {
                    xtype: "jitdatefield",
                    fieldLabel: "取消日期",
                    id: "txtModifyTimeBegin",
                    name: "ModifyTime_begin",
                    jitSize: 'small'
                },
                {
                    xtype: "label",
                    text: "至"
                },
                {
                    xtype: "jitdatefield",
                    fieldLabel: "",
                    id: "txtModifyTimeEnd",
                    name: "ModifyTime_end",
                    jitSize: 'small',
                    width: 100
                }
            ]
        }
       
        
        ]

    });
//    Ext.create('Jit.button.Button', {
//        text: "添加",
//        renderTo: "span_create",
//        //hidden: __getHidden("create"),
//        handler: fnCreate
//    });
        //operator area
        Ext.create('Jit.button.Button', {
            text: "导出",
            renderTo: "btn_excel",
            handler: fnSearchExcel
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
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
            //width: 200,
            height: 42,
            items: [
        {
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: function() {
                fnSearch();
            }
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

    // grid1
    var grid1 = Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderStore1"),
        id: "gridView1",
        renderTo: "DivGridView1",
        columnLines: true,
        height: 450,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("salesOutOrderStore1"),
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
            width: 140,
            sortable: true,
            dataIndex: 'order_id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                //if (d.status == "1") {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnPosOrderDeliveryUpdate('" + value + "', '0')\">取消</a>";
                //}
                str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPosOrderDeliveryUpdate('" + value + "', '2', '" + d.total_amount + "')\">收款</a>";
                str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnViewUnit('" + value + "')\">配送订单</a>";
                return str;
            }
        }
        , {
            text: '单据号码',
            width: 150,
            sortable: true,
            dataIndex: 'order_no',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "','" + d.status + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '单据日期',
            width: 110,
            sortable: true,
            dataIndex: 'order_date',
            align: 'left'
        }
     , {
        text: '消费金额',
        width: 110,
        sortable: true,
        dataIndex: 'total_amount',
        align: 'left'
    }
        ,{
            text: '下单门店',
            width: 110,
            sortable: true,
            dataIndex: 'sales_unit_name',
            align: 'left'
        }
       , {
           text: '预约门店',
           width: 110,
           sortable: true,
           dataIndex: 'purchase_unit_name',
           align: 'left'
       }
    , {
        text: '会员',
        width: 110,
        sortable: true,
        dataIndex: 'vip_name',
        align: 'left'
    }
    , {
        text: '交易时间',
        width: 150,
        sortable: true,
        dataIndex: 'create_time',
        align: 'left'
        ,renderer: function (value, p, record) {
            return getDate(value);
        }
    }
    , {
        text: '支付方式',
        width: 100,
        sortable: true,
        dataIndex: 'DefrayTypeName',
        align: 'left'
    }
    , {
        text: '配送方式',
        width: 100,
        sortable: true,
        dataIndex: 'DeliveryName',
        align: 'left'
    }
    , {
        text: '来源',
        width: 100,
        sortable: true,
        dataIndex: 'data_from_name',
        align: 'left'
    }
    , {
        text: '操作人',
        width: 80,
        sortable: true,
        dataIndex: 'create_user_name',
        align: 'left'
    }
    ]
    });

    
    // grid2
    var grid2 = Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderStore2"),
        id: "gridView2",
        renderTo: "DivGridView2",
        columnLines: true,
        height: 450,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar2",
            defaultType: 'button',
            store: Ext.getStore("salesOutOrderStore2"),
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
            width: 80,
            sortable: true,
            dataIndex: 'order_id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.DeliveryId == "1") {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnPosOrderDeliveryUpdate('" + value + "', '3')\">提货</a>";
                } else {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnPosOrderDeliveryView('" + value + "', '3')\">配送</a>";
                }
                return str;
            }
        },
        {
            text: '单据号码',
            width: 150,
            sortable: true,
            dataIndex: 'order_no',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "','" + d.status + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '单据日期',
            width: 110,
            sortable: true,
            dataIndex: 'order_date',
            align: 'left'
        }
    , {
        text: '消费金额',
        width: 110,
        sortable: true,
        dataIndex: 'total_amount',
        align: 'left'
    }
        ,{
            text: '消费门店',
            width: 110,
            sortable: true,
            dataIndex: 'sales_unit_name',
            align: 'left'
        }, {
            text: '配送门店',
            width: 110,
            sortable: true,
            dataIndex: 'purchase_unit_name',
            align: 'left'
        }
    , {
        text: '会员',
        width: 110,
        sortable: true,
        dataIndex: 'vip_name',
        align: 'left'
    }
    , {
        text: '交易时间',
        width: 150,
        sortable: true,
        dataIndex: 'create_time',
        align: 'left'
        ,renderer: function (value, p, record) {
            return getDate(value);
        }
    }
    , {
        text: '支付方式',
        width: 100,
        sortable: true,
        dataIndex: 'DefrayTypeName',
        align: 'left'
    }
    , {
        text: '配送方式',
        width: 100,
        sortable: true,
        dataIndex: 'DeliveryName',
        align: 'left'
    }
    , {
        text: '来源',
        width: 100,
        sortable: true,
        dataIndex: 'data_from_name',
        align: 'left'
    }
    , {
        text: '操作人',
        width: 80,
        sortable: true,
        dataIndex: 'create_user_name',
        align: 'left'
    }
    ]
    });

    
    // grid3
    var grid3 = Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderStore3"),
        id: "gridView3",
        renderTo: "DivGridView3",
        columnLines: true,
        height: 450,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar3",
            defaultType: 'button',
            store: Ext.getStore("salesOutOrderStore3"),
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
            width: 80,
            sortable: true,
            dataIndex: 'order_id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                //if (d.status == "1") {
                    //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnPosOrderDeliveryUpdate('" + value + "', '4')\">完成</a>";
                //}
                return str;
            }
        }, {
            text: '单据号码',
            width: 150,
            sortable: true,
            dataIndex: 'order_no',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "','" + d.status + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '单据日期',
            width: 110,
            sortable: true,
            dataIndex: 'order_date',
            align: 'left'
        }
    , {
        text: '消费金额',
        width: 110,
        sortable: true,
        dataIndex: 'total_amount',
        align: 'left'
    }
        , {
            text: '消费门店',
            width: 110,
            sortable: true,
            dataIndex: 'sales_unit_name',
            align: 'left'
        }
    , {
        text: '会员',
        width: 110,
        sortable: true,
        dataIndex: 'vip_name',
        align: 'left'
    }
    , {
        text: '交易时间',
        width: 150,
        sortable: true,
        dataIndex: 'create_time',
        align: 'left'
        ,renderer: function (value, p, record) {
            return getDate(value);
        }
    }
    , {
        text: '支付方式',
        width: 100,
        sortable: true,
        dataIndex: 'DefrayTypeName',
        align: 'left'
    }
    , {
        text: '配送方式',
        width: 100,
        sortable: true,
        dataIndex: 'DeliveryName',
        align: 'left'
    }
    , {
        text: '发货时间',
        width: 150,
        sortable: true,
        dataIndex: 'send_time',
        align: 'left'
        ,renderer: function (value, p, record) {
            return getDate(value);
        }
    }
    , {
        text: '配送商',
        width: 100,
        sortable: true,
        dataIndex: 'carrier_name',
        align: 'left'
    }
    , {
        text: '配送单号',
        width: 100,
        sortable: true,
        dataIndex: 'Field2',
        align: 'left'
    }
    , {
        text: '来源',
        width: 100,
        sortable: true,
        dataIndex: 'data_from_name',
        align: 'left'
    }
    , {
        text: '操作人',
        width: 80,
        sortable: true,
        dataIndex: 'create_user_name',
        align: 'left'
    }
    ]
    });

    
    // grid4
    var grid4 = Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderStore4"),
        id: "gridView4",
        renderTo: "DivGridView4",
        columnLines: true,
        height: 450,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar4",
            defaultType: 'button',
            store: Ext.getStore("salesOutOrderStore4"),
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
            width: 80,
            sortable: true,
            dataIndex: 'order_id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                //if (d.status == "1") {
                    //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnCancel('" + value + "')\">取消</a>";
                //}
                //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPay('" + value + "')\">收款</a>";
                return str;
            }
        }
        , {
            text: '单据号码',
            width: 150,
            sortable: true,
            dataIndex: 'order_no',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "','" + d.status + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '单据日期',
            width: 110,
            sortable: true,
            dataIndex: 'order_date',
            align: 'left'
        }
     , {
        text: '消费金额',
        width: 110,
        sortable: true,
        dataIndex: 'total_amount',
        align: 'left'
    }
        ,{
            text: '消费门店',
            width: 110,
            sortable: true,
            dataIndex: 'sales_unit_name',
            align: 'left'
        }
    , {
        text: '会员',
        width: 110,
        sortable: true,
        dataIndex: 'vip_name',
        align: 'left'
    }
    , {
        text: '交易时间',
        width: 150,
        sortable: true,
        dataIndex: 'create_time',
        align: 'left'
        ,renderer: function (value, p, record) {
            return getDate(value);
        }
    }
    , {
        text: '取消时间',
        width: 150,
        sortable: true,
        dataIndex: 'modify_time',
        align: 'left'
        ,renderer: function (value, p, record) {
            return getDate(value);
        }
    }
    , {
        text: '支付方式',
        width: 100,
        sortable: true,
        dataIndex: 'DefrayTypeName',
        align: 'left'
    }
    , {
        text: '配送方式',
        width: 100,
        sortable: true,
        dataIndex: 'DeliveryName',
        align: 'left'
    }
    , {
        text: '来源',
        width: 100,
        sortable: true,
        dataIndex: 'data_from_name',
        align: 'left'
    }
    , {
        text: '操作人',
        width: 80,
        sortable: true,
        dataIndex: 'create_user_name',
        align: 'left'
    }
    ]
    });
    
    Ext.create('Jit.button.Button', {
        text: "取消",
        id: "btnOp1",
        renderTo: "btnOp1",
        //disabled: true,
        hidden: true,
        handler: function() {
            fnPosOrderDeliveryUpdateByBatch('0');
        }
    });
    Ext.create('Jit.button.Button', {
        text: "配送",
        id: "btnOp2",
        renderTo: "btnOp2",
        //disabled: true,
        hidden: true,
        handler: function() {
            fnPosOrderDeliveryUpdateByBatch('3');
        }
    });
    Ext.create('Jit.button.Button', {
        text: "完成",
        id: "btnOp3",
        renderTo: "btnOp3",
        //disabled: true,
        hidden: true,
        handler: function() {
            fnPosOrderDeliveryUpdateByBatch('4');
        }
    });
}
