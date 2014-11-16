function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: 'column',
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        width: 815,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "门店编码",
            id: "txtUnitCode",
            name: "unit_code",
            //hidden: true,
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "门店名称",
            id: "txtUnitName",
            name: "unit_name",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizunittype",
            fieldLabel: "类型",
            id: "txtUnitType",
            name: "unit_type_id",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "电话",
            id: "txtTel",
            name: "unit_tel",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizcityselecttree",
            fieldLabel: "城市",
            id: "txtCity",
            name: "city_id",
            hidden: true,
            jitSize: 'small'
        }
        , {
            xtype: "jitbizstatus",
            fieldLabel: "状态",
            id: "txtStatus",
            name: "unit_status",
            hidden: true,
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
            handler: fnSearch   //查询操作
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
        }
        , {
            xtype: "jitbutton",
            id: "btnMoreSearchView",
            text: "高级查询",
            margin: '0 0 10 14',
            handler: fnMoreSearchView   //高级查询
        }
        ]

    });

    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate    //添加操作
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("unitStore"),
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
            store: Ext.getStore("unitStore"),   //分页的数据对象
            pageSize:JITPage.PageSize.getValue()   //一页的数量,这句话没用
        }),
        listeners: {
            render: function (p) {
               // alert("as");
                p.setLoading({
                    store: p.getStore()   //分页
                }).hide();
            }
        },
        columns: [{
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.Status != "-1") {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '-1')\">停用</a>";
                } else {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '1')\">启用</a>";
                }
                return str;
            }
        }
        , {
            text: '门店编码',
            width: 110,
            sortable: true,
            dataIndex: 'Code',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.Id + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '门店名称',
            width: 190,
            sortable: true,
            dataIndex: 'Name',
            align: 'left'
        }
        , {
            text: '城市',
            width: 150,
            sortable: true,
            dataIndex: 'CityName',
            align: 'left'
        }
        , {
            text: '联系人',
            width: 110,
            sortable: true,
            dataIndex: 'Contact',
            align: 'left'
        }
        , {
            text: '电话',
            width: 110,
            sortable: true,
            dataIndex: 'Telephone',
            align: 'left'
        }
        , {
            text: '类型',
            width: 110,
            sortable: true,
            dataIndex: 'TypeName',
            align: 'left'
        }
        , {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'Status',
            align: 'left',
            renderer: function (value, p, record) {
                if (value == "-1") return "停用";
                else if (value == "1") return "正常";
                return "";
            }
        }
        ]
    });
}