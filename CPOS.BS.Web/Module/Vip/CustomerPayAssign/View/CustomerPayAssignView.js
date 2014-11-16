var tagsData = [], tagsStr = "";
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
            xtype: "jitbizpaymenttype",
            fieldLabel: "支付方式",
            id: "txtPaymentType",
            name: "PaymentTypeId",
            jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "客户帐号",
            id: "txtCustomerAccountNumber",
            name: "CustomerAccountNumber",
            jitSize: 'small'
        }
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
        ]

    });

    //Ext.create('Jit.button.Button', {
    //    text: "清 除",
    //    renderTo: "btnCancel"
    //    , handler: fnCancel
    //});
    //operator area
    //Ext.create('Jit.button.Button', {
    //    text: "添加",
    //    renderTo: "span_create",
    //    //hidden: __getHidden("create"),
    //    handler: fnCreate
    //});

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("CustomerPayAssignStore"),
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
            store: Ext.getStore("CustomerPayAssignStore"),
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
            dataIndex: 'AssignId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                //if (d.order_status == "1") {
                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
                //}
                return str;
            }
        }
        ,{
            text: '客户帐号',
            width: 210,
            sortable: true,
            dataIndex: 'CustomerAccountNumber',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.AssignId + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '支付方式',
            width: 150,
            sortable: true,
            dataIndex: 'PaymentTypeName',
            align: 'left'
        }, {
            text: '客户分成比例',
            width: 150,
            sortable: true,
            dataIndex: 'CustomerProportion',
            align: 'left'
        }
        ,{
            text: '杰亦特截留比例',
            width: 150,
            sortable: true,
            dataIndex: 'JITProportion',
            align: 'left'
        }
        ,{
            text: '备注',
            width: 210,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }
        , {
            text: '创建时间',
            width: 130,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += getDate(value);
                return str;
            }
        }
        
        ]
    });

    
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnAdd",
        handler: fnCreate
    });
}