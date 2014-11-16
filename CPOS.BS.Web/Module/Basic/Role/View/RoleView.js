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
            xtype: "jitbizappsys",
            fieldLabel: "应用系统",
            id: "txtAppSys",
            name: "app_sys_id",
            dataType: "get_app_sys_list",
            jitSize: 'small'
        }
        ,{
            xtype: "jitbutton",
            text: "查询",
            //hidden: __getHidden("search"),
            margin: '0 0 10 14',
            handler: fnSearch
        }]

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
        store: Ext.getStore("roleStore"),
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
            store: Ext.getStore("roleStore"),
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
            dataIndex: 'Role_Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.Role_Status != "-1") {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
                }
                return str;
            }
        }
        , {
            text: '应用系统',
            width: 110,
            sortable: true,
            dataIndex: 'Def_App_Name',
            align: 'left'
        }
        , {
            text: '角色编码',
            width: 150,
            sortable: true,
            dataIndex: 'Role_Code',
            align: 'left'
            ,renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.Role_Id + "')\">" + value + "</a>";
                return str;
            }
        }, {
            text: '角色名称',
            width: 150,
            sortable: true,
            dataIndex: 'Role_Name',
            align: 'left'
        }, {
            text: '英文名',
            width: 110,
            sortable: true,
            dataIndex: 'Role_Eng_Name',
            align: 'left'
        }, {
            text: '系统保留',
            width: 110,
            sortable: true,
            dataIndex: 'Is_Sys',
            align: 'left',
            renderer: function (value, p, record) {
                if (value == "0") return "否";
                return "是";
            }
        }
        ]
    });
}