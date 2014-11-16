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
            fieldLabel: "类型编码",
            id: "txtItemCategoryCode",
            name: "item_category_code",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "类型名称",
            id: "txtItemCategoryName",
            name: "item_category_name",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "拼音助记码",
            id: "txtPyzjm",
            name: "pyzjm",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizstatus",
            fieldLabel: "状态",
            id: "txtItemCategoryStatus",
            name: "item_category_status",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizitemcategoryselecttree",
            fieldLabel: "商品分类",
            id: "txtItemCategory",
            name: "item_category_id",
            jitSize: 'small',
            hidden: true
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
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "查询",
            margin: '0 0 10 14',
            handler: fnSearch
        },
        {
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
        handler: fnCreate
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("itemCategoryStore"),
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
            store: Ext.getStore("itemCategoryStore"),
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
            width: 110,
            sortable: true,
            dataIndex: 'Item_Category_Id',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.Parent_Id != "-99") {
                    if (d.Status != "-1") {
                        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '-1')\">停用</a>";
                    } else {
                        str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '1')\">启用</a>";
                    }
                }
                return str;
            }
        },
        {
            text: '类型编码',
            width: 150,
            sortable: true,
            dataIndex: 'Item_Category_Code',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.Item_Category_Id + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '类型名称',
            width: 150,
            sortable: true,
            dataIndex: 'Item_Category_Name',
            align: 'left'
        }
        , {
            text: '助记码',
            width: 110,
            sortable: true,
            dataIndex: 'Pyzjm',
            align: 'left'
        }
        , {
            text: '上级商品名称',
            width: 110,
            sortable: true,
            dataIndex: 'Parent_Name',
            align: 'left'
        }
        , {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'Status_desc',
            align: 'left',
            renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '排序',
            width: 110,
            sortable: true,
            dataIndex: 'DisplayIndex',
            align: 'left'
        }
        ]
    });
}