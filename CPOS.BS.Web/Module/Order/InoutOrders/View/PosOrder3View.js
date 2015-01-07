/*Jermyn 2013-04-01
POS小票
*/

function InitView() {
    //   查询控件
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
               xtype: "jittextfield",
               fieldLabel: "会员",
               id: "txtVipName",
               name: "vip_no",
               jitSize: 'small'
           },
        {
            xtype: "jitcombobox",
            fieldLabel: "付款状态",
            id: "txtField1",
            name: "Field1",
            store: Ext.getStore("payStatusStore"),
            displayField: 'name',
            valueField: 'value',
            emptyText: "--请选择--"
        }
         , {
             xtype: "jitbizunitselecttree",
             fieldLabel: "门店",
             colspan: 2,
             id: "txtSalesUnit",
             name: "sales_unit_id",
             jitSize: 'small'
         },
        //         {
        //             xtype: "jitbizpospaytype",
        //             fieldLabel: "支付方式",
        //             colspan: 2,
        //             id: "txtPosPayType",
        //             name: "DefrayTypeId",
        //             hidden: true,
        //             jitSize: 'small'
        //         }
        //        , 
        {
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
                    fieldLabel: "成交日期",
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
        xtype: "jitbizvipsource",
        fieldLabel: "来源",
        colspan: 2,
        id: "txtDataFromID",
        name: "data_from_id",
        hidden: true,
        jitSize: 'small'
    },
             {
                 xtype: "jitbizpossendtype",
                 fieldLabel: "配送方式",
                 colspan: 2,
                 id: "txtPosSendType",
                 name: "DeliveryId",
                 hidden: true,
                 jitSize: 'small'
             }
        , {
            xtype: 'panel',
            colspan: 4,
            layout: 'hbox',
            border: 0,
            bodyBorder: false,
            id: 'txtField9',
            hidden: true,
            bodyStyle: 'background:#F1F2F5;',
            items: [{
                xtype: 'jitbizoptions'
                , id: 'InoutSort'
                , name: 'InoutSort'
                , fieldLabel: '排序'
                , OptionName: 'InoutSort'
                , isDefault: true
            }]
        }
        , {
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
        }]
});

// 新建按钮
Ext.create('Jit.button.Button', {
    text: "新建",
    renderTo: "btn_Create",
    handler: function () {
        var win = Ext.create('jit.biz.Window', {
            jitSize: "large",
            height: 500,
            id: "salesOrderCreate",
            title: "新建订单",
            url: "SalesOrderCreate.aspx?mid=" + __mid + "&op=new"
        });
        win.show();
    }
    , jitIsHighlight: true
    , jitIsDefaultCSS: true
});

// 导出按钮
Ext.create('Jit.button.Button', {
    text: "导出",
    renderTo: "btn_excel",
    handler: fnSearchExcel
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
});

    // 导出按钮
Ext.create('Jit.button.Button', {
    text: "配发货门店",
    renderTo: "btn_SetUnit",
    handler: fnSetUnit
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
});

//查询按钮
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
            handler: function () {
                fnSearch();
            }
        },
        {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView
        }]
});

    // jifeng.cao begin
    //付款操作**
Ext.create('Ext.form.Panel', {
    id: "operationPanel",
    width: "100%",
    height: "100%",
    border: 1,
    bodyStyle: 'background:#F1F2F5;padding-top:10px',
    layout: 'anchor',
    defaults: {},
    items: [{
        xtype: "jitbizoptions",
        fieldLabel: "付款方式",
        id: "PayMethod",
        name: "PayMethod",
        OptionName: 'PayMethod',
        allowBlank: false
    }, {
        xtype: 'filefield',
        name: 'PicUrl',
        width: 258,
        labelWidth: 82,
        height: 22,
        labelSeparator: '',
        id: 'PicUrl',
        labelPad: 11,
        labelAlign: 'right',
        buttonText: '选择图片',
        fieldLabel: '上传凭证'
    }, {
        xtype: "jittextarea",
        fieldLabel: "备注",
        name: "Remark",
        id: "Remark",
        maxLength: 300
    }]

});

Ext.create('Jit.window.Window', {
    height: 350,
    id: "operationWin",
    title: '订单操作',
    jitSize: 'big',
    layout: 'fit',
    draggable: true,
    items: [Ext.getCmp("operationPanel")],//把收款的部分给包含进去了
    border: 0,
    modal: false,
    buttons: ['->', {
        xtype: "jitbutton",
        id: "btnSave",
        handler: fnOperationSubmit,     //按钮
        isImgFirst: true,
        imgName: "save"
    }, {
        xtype: "jitbutton",
        handler: fnOperationCancel,
        imgName: "cancel"
    }],
    closeAction: 'hide'
});
// jifeng.cao end


// 查询的数据集
var grid = Ext.create('Ext.grid.Panel', {
    store: Ext.getStore("salesOutOrderStore1"),
    id: "gridView",
    renderTo: "DivGridView",
    columnLines: true,
    height: 450,
    stripeRows: true,
    width: "100%",
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBar0",
        defaultType: 'button',
        store: Ext.getStore("salesOutOrderStore1"),
        pageSize: JITPage.PageSize.getValue()
    }),
    selModel: Ext.create('Ext.selection.CheckboxModel', {
        mode: 'MULTI'
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
        text: '单据号码',
        width: 150,
        sortable: true,//可排序，其实不是真排序，只是本页的排序
        dataIndex: 'order_no',
        align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (record.data.customer_id == "a6c351d17bf5482a807f1780a83b8239" || record.data.customer_id == "4638b8b9d8c1435e8618f892d44a17a1") {
                    str += "<a class=\"pointer z_col_light_text\" onclick=\"fnHSView('" + d.order_id + "','" + d.status + "','" + d.Field7 + "','" + d.Field1 + "')\">" + value + "</a>";
                } else {
                    if (d.Field1 == "1" || d.Field7 == "800" || d.Field7 == "900") {
                        str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "','" + d.status + "','" + d.Field7 + "','" + d.Field1 + "')\">" + value + "</a>";
                    } else {
                        str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.order_id + "','" + d.status + "','" + d.Field7 + "','" + d.Field1 + "')\">" + value + "</a>&nbsp;&nbsp;&nbsp;<a class=\"pointer z_col_light_text\" onclick=\"fnbtn(10000,'" + d.order_id + "');\">收款</a>";
                    }
                }
                return str;
            }
    }
        , {
            text: '单据日期',
            width: 110,
            sortable: true,
            dataIndex: 'order_date',
            align: 'left'
        }, {
            text: '修改时间',
            width: 150,
            sortable: true,
            dataIndex: 'modify_time',
            hidden: true,
            align: 'left'
        }, {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'status_desc',
            align: 'left'
        }, {
            text: '付款状态',
            width: 110,
            sortable: true,
            dataIndex: 'Field1',
            align: 'left',
            renderer: fnPayStatus
        }, {
            text: '消费金额',
            width: 110,
            sortable: true,
            dataIndex: 'total_amount',
            align: 'left'
        }, {
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
        }, {
            text: '会员',
            width: 110,
            sortable: true,
            dataIndex: 'vip_name',
            align: 'left'
        }, {
            text: '交易时间',
            width: 150,
            sortable: true,
            dataIndex: 'create_time',
            align: 'left'
        , renderer: function (value, p, record) {
            return getDate(value);
        }
        },
    //        {
    //            text: '支付方式',
    //            width: 100,
    //            sortable: true,
    //            dataIndex: 'DefrayTypeName',
    //            align: 'left'
    //        }, 
        {
        text: '配送方式',
        width: 100,
        sortable: true,
        dataIndex: 'DeliveryName',
        align: 'left'
    }, {
        xtype: 'datecolumn',
        text: '发货时间',
        format: 'Y-m-d',
        width: 100,
        sortable: true,
        dataIndex: 'Field9',
        align: 'left'
    }, {
        text: '配送地址',
        width: 100,
        sortable: true,
        dataIndex: 'address',
        align: 'left'
    }, {
        text: '收货人',
        width: 100,
        sortable: true,
        dataIndex: 'linkMan',
        align: 'left'
    }, {
        text: '电话',
        width: 100,
        sortable: true,
        dataIndex: 'linkTel',
        align: 'left'
    }, {
        text: '发票抬头',
        width: 100,
        sortable: true,
        dataIndex: 'Field19',
        align: 'left'
    }, {
        text: '来源',
        width: 100,
        sortable: true,
        dataIndex: 'data_from_name',
        align: 'left'
    }, {
        text: '奖励返现金额',
        width: 100,
        sortable: true,
        dataIndex: 'AmountBack',
        align: 'left'
    }, {
        text: '赠送积分',
        width: 100,
        sortable: true,
        dataIndex: 'IntegralBack',
        align: 'left'
    }
    
    //        , {
    //            text: '操作人',
    //            width: 80,
    //            sortable: true,
    //            dataIndex: 'create_user_name',
    //            align: 'left'
    //        }
        ]
});
}