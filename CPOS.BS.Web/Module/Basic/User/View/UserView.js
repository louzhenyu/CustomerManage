function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 5
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "工号",
            id: "txtUserCode",
            name: "user_code",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "姓名",
            id: "txtUserName",
            name: "user_name",
            jitSize: 'small'
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "手机",
            id: "txtTel",
            name: "user_tel",
            jitSize: 'small'
        }
        , {
            xtype: "jitbizstatus",
            fieldLabel: "状态",
            id: "txtStatus",
            name: "user_status",
            dataType: "user_status",
            jitSize: 'small'
        }
        , {
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
        store: Ext.getStore("userStore"),
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
            store: Ext.getStore("userStore"),
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
            dataIndex: 'User_Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                if (d.User_Status != "-1") {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '-1')\">停用</a>";
                } else {
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "', '1')\">启用</a>";
                }
                return str;
            }
        }, {
            text: '密码重置',
            width: 110,
            sortable: true,
            dataIndex: 'User_Id',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnRevert('" + value + "')\">重置密码</a>";
                return str;
            }
        }, {
            text: '工号/登录名',
            width: 150,
            sortable: true,
            dataIndex: 'User_Code',
            align: 'left'
            , renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.User_Id + "')\">" + value + "</a>";
                return str;
            }
        }
        , {
            text: '姓名',
            width: 150,
            sortable: true,
            dataIndex: 'User_Name',
            align: 'left'
        }
        , {
            text: '性别',
            width: 110,
            sortable: true,
            dataIndex: 'user_genderText',
            align: 'left'
        }
        , {
            text: '手机',
            width: 110,
            sortable: true,
            dataIndex: 'User_Telephone',
            align: 'left'
        }
        , {
            text: '门店',
            width: 110,
            sortable: true,
            dataIndex: 'UnitName',
            align: 'left'
        }
        , {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'User_Status',
            align: 'left',
            renderer: function (value, p, record) {
                if (value == "-1") return "停用";
                return "正常";
            }
        }
        ]
    });
}