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
            fieldLabel: "姓名",
            id: "txtVipName",
            name: "VipName",
            jitSize: 'small'
        },
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
        {
            xtype: "jitbizvipenterpriseexpandstatus",
            fieldLabel: "状态",
            id: "txtStatus",
            name: "Status",
            jitSize: 'small'
        }
        //{
        //    xtype: "jittextfield",
        //    fieldLabel: "责任人",
        //    id: "txtECSourceId",
        //    name: "ECSourceId",
        //    jitSize: 'small'
        //}
        ]
    });
    
    //Ext.create('Ext.form.Panel', {
    //    id: 'btn_panel2',
    //    layout: {
    //        type: 'table',
    //        columns: 4
    //    },
    //    renderTo: 'btn_panel2',
    //    padding: '10 0 0 0',
    //    bodyStyle: 'background:#F1F2F5;',
    //    border: 0,
    //    height: 42,
    //    items: [
    //    {
    //        xtype: "jitbutton",
    //        id: "btnMoreSearchView",
    //        text: "高级查询",
    //        margin: '0 0 10 14',
    //        handler: fnMoreSearchView
    //    }
    //    ]
    //});

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
        store: Ext.getStore("VipEnterpriseStore"),
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
            store: Ext.getStore("VipEnterpriseStore"),
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
            dataIndex: 'VipId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.IsDelete == 0)
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', 1)\">停用</a>";
                else
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', 0)\">启用</a>";
                return str;
            }
        },
        {
            text: '名称',
            width: 250,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.VipId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '所属客户',
            width: 100,
            sortable: true,
            dataIndex: 'EnterpriseCustomerName',
            align: 'left'
        },
        {
            text: '性别',
            width: 100,
            sortable: true,
            dataIndex: 'GenderName',
            align: 'left'
        },
        {
            text: '联系电话',
            width: 100,
            sortable: true,
            dataIndex: 'Phone',
            align: 'left'
        },
        {
            text: '部门',
            width: 100,
            sortable: true,
            dataIndex: 'Department',
            align: 'left'
        },
        {
            text: '职务',
            width: 100,
            sortable: true,
            dataIndex: 'Position',
            align: 'left'
        },
        {
            text: '状态',
            width: 100,
            sortable: true,
            dataIndex: 'StatusName',
            align: 'left'
        },
        {
            text: '决策力',
            width: 100,
            sortable: true,
            dataIndex: 'PDRoleName',
            align: 'left'
        }
        //{
        //    text: '责任人',
        //    width: 100,
        //    sortable: true,
        //    dataIndex: 'CreateByName',
        //    align: 'left'
        //}
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