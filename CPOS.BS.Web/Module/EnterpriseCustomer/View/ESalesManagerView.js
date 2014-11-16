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
            fieldLabel: "销售线索",
            id: "txtSalesName",
            name: "SalesName",
            jitSize: 'small'
        },
        //{
        //    xtype: "jittextfield",
        //    fieldLabel: "所属客户",
        //    id: "txtEnterpriseCustomerId",
        //    name: "EnterpriseCustomerId",
        //    jitSize: 'small'
        //}
        {
            xtype: "jitbizeccustomerselect",
            fieldLabel: "所属客户",
            id: "txtEnterpriseCustomerId",
            name: "EnterpriseCustomerId",
            fnCallback: function(d) {
                get("hECCustomerId").value = d.id;
            },
            jitSize: 'small'
        },
        ,
        {
            xtype: "jitbizESalesProduct",
            fieldLabel: "销售产品",
            id: "txtSalesProduct",
            name: "SalesProductId",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbizESalesStage",
            fieldLabel: "阶段",
            id: "txtSalesStage",
            name: "SalesStage",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbizESalesChargeVip",
            fieldLabel: "销售负责人",
            id: "txtSalesVipId",
            name: "SalesChargeVipId",
            jitSize: 'small',
            hidden: true
        }
        ]
    });

    Ext.create('Ext.form.Panel', {
        id: 'btn_panel2',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel2',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView
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
        , {
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
        store: Ext.getStore("salesStore"),
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
            store: Ext.getStore("salesStore"),
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
            text: '序号',
            width: 50,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        },
        {
            text: '操作',
            width: 80,
            sortable: true,
            dataIndex: 'SalesId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">停用</a>";
                return str;
            }
        },
        {
            text: '销售线索名称',
            width: 200,
            sortable: true,
            dataIndex: 'SalesName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.SalesId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '所属客户',
            width: 150,
            sortable: true,
            dataIndex: 'EnterpriseCustomerName',
            align: 'left'
        },
        {
            text: '可能性(%)',
            width: 100,
            sortable: true,
            dataIndex: 'Possibility',
            align: 'left'

        },
        {
            text: '销售产品',
            width: 100,
            sortable: true,
            dataIndex: 'SalesProductName',
            align: 'left'
        }
        ,{
            text: '金额',
            width: 100,
            sortable: true,
            dataIndex: 'ForecastAmount',
            align: 'left'
        }
        , {
            text: '结束日期',
            width: 100,
            sortable: true,
            dataIndex: 'EndDate',
            align: 'left'
        }
        , {
            text: '来源',
            width: 100,
            sortable: true,
            dataIndex: 'ECSourceName',
            align: 'left'
        }
        , {
            text: '阶段',
            width: 100,
            sortable: true,
            dataIndex: 'StageName',
            align: 'left'
        }
        , {
            text: '销售负责人',
            width: 100,
            sortable: true,
            dataIndex: 'SalesVipName',
            align: 'left'
        }
        , {
            text: '创建人',
            width: 100,
            sortable: true,
            dataIndex: 'CreateUserName',
            align: 'left'
        }
        , {
            text: '创建时间',
            width: 100,
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

    
    Ext.create('Jit.form.field.Text', {
        id: "tbECSearchCustomerName",
        text: "",
        renderTo: "tbECSearchCustomerName",
        width: 180
    });
    Ext.create('Jit.button.Button', {
        text: "搜索",
        renderTo: "tbECSearchCustomerGo",
        width: 70,
        handler: fnECSearchCustomerGo
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "清除",
        renderTo: "tbECSearchCustomerClear",
        width: 70,
        handler: fnECSearchCustomerClear
    });
}