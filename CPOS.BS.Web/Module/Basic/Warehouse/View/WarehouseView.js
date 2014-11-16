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
            xtype: "jitbizunitselecttree",
            fieldLabel: "所属单位",
            id: "txtUnitName",
            name: "unit_name",
            jitSize: 'small'
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "仓库编码",
            id: "txtWarehouseCode",
            name: "warehouse_code",
            jitSize: 'small'
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "仓库名称",
            id: "txtWarehouseName",
            name: "warehouse_name",
            jitSize: 'small'
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "联系人",
            id: "txtContacter",
            name: "warehouse_contacter",
            jitSize: 'small'
        }
        ,{
            xtype: "jittextfield",
            fieldLabel: "电话",
            id: "txtTel",
            name: "warehouse_tel",
            hidden: true,
            jitSize: 'small'
        }
        ,{
            xtype: "jitbizstatus",
            fieldLabel: "状态",
            id: "txtStatus",
            name: "warehouse_status",
            hidden: true,
            jitSize: 'small'
        }
//        ,{
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
        store: Ext.getStore("warehouseStore"),
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
            store: Ext.getStore("warehouseStore"),
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
            dataIndex: 'warehouse_id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.wh_status != "-1") {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '-1')\">停用</a>";
                } else {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '1')\">启用</a>";
                }
                return str;
            }
        }
        ,{
            text: '所属单位',
            width: 150,
            sortable: true,
            dataIndex: 'unit_name',
            align: 'left'
        }
        ,{
            text: '仓库编码',
            width: 150,
            sortable: true,
            dataIndex: 'wh_code',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.warehouse_id + "')\">" + value + "</a>";
                return str;
            }
        }
        ,{
            text: '仓库名称',
            width: 150,
            sortable: true,
            dataIndex: 'wh_name',
            align: 'left'
        }
        ,{
            text: '联系人',
            width: 110,
            sortable: true,
            dataIndex: 'wh_contacter',
            align: 'left'
        }
        ,{
            text: '电话',
            width: 110,
            sortable: true,
            dataIndex: 'wh_tel',
            align: 'left'
        }
        ,{
            text: '默认仓库',
            width: 110,
            sortable: true,
            dataIndex: 'is_default_desc',
            align: 'left'
        }
        ,{
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'wh_status_desc',
            align: 'left',
            renderer: function (value, p, record) {
                if (value == "-1") return "停用";
                return "正常";
            }
        }
        ]
    });
}