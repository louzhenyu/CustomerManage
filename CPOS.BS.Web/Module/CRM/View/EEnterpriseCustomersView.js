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
            fieldLabel: "客户名称",
            id: "txtName",
            name: "Name",
            jitSize: 'small'
        },
        {
            xtype: "jitbizeenterprisecustomertype",
            fieldLabel: "客户类型",
            id: "txtTypeId",
            name: "TypeId",
            jitSize: 'small'
        },
        {
            xtype: "jitbizeindustry",
            fieldLabel: "行业",
            id: "txtIndustryId",
            name: "IndustryId",
            jitSize: 'small'
        },
        {
            xtype: "jitbizeenterprisecustomersource",
            fieldLabel: "来源",
            id: "txtECSourceId",
            name: "ECSourceId",
            jitSize: 'small'
        },
        {
            xtype: "jitbizcityselecttree",
            fieldLabel: "所在省市",
            id: "txtCityId",
            name: "CityId",
            jitSize: 'small',
            hidden: true
        },
        {
            xtype: "jitbizescale",
            fieldLabel: "规模",
            id: "txtScaleId",
            name: "ScaleId",
            jitSize: 'small',
            hidden: true
        },
        {
            xtype: "jitbizeenterprisecustomerstatus",
            fieldLabel: "状态",
            id: "txtStatus",
            name: "Status",
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
        store: Ext.getStore("EEnterpriseCustomersStore"),
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
            store: Ext.getStore("EEnterpriseCustomersStore"),
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
            dataIndex: 'EnterpriseCustomerId',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.Status == 1)
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', 0)\">停用</a>";
                else
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', 1)\">启用</a>";
                return str;
            }
        },
        {
            text: '客户名称',
            width: 250,
            sortable: true,
            dataIndex: 'EnterpriseCustomerName',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.EnterpriseCustomerId + "')\">" + value + "</a>";
                return str;
            }
        },
        {
            text: '客户类型',
            width: 100,
            sortable: true,
            dataIndex: 'TypeName',
            align: 'left'
        },
        {
            text: '所属行业',
            width: 100,
            sortable: true,
            dataIndex: 'IndustryName',
            align: 'left'
        },
        {
            text: '联系电话',
            width: 100,
            sortable: true,
            dataIndex: 'Tel',
            align: 'left'
        },
        {
            text: '规模',
            width: 100,
            sortable: true,
            dataIndex: 'ScaleName',
            align: 'left'
        },
        {
            text: '来源',
            width: 100,
            sortable: true,
            dataIndex: 'ECSourceName',
            align: 'left'
        },
        {
            text: '创建人',
            width: 100,
            sortable: true,
            dataIndex: 'CreateByName',
            align: 'left'
        },
        {
            text: '创建日期',
            width: 130,
            sortable: true,
            dataIndex: 'CreateTime',
            //format: 'Y-m-d',
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
}