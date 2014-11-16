function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 451,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo',
            title: '基本信息'
        }
        , {
            contentEl: 'tabRole',
            title: '职务信息',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tabRole");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        ]
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUserCode",
        text: "",
        renderTo: "txtUserCode",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUserName",
        text: "",
        renderTo: "txtUserName",
        width: 100
    });

    Ext.create('jit.biz.UserGender', {
        id: "txtUserGender",
        text: "",
        renderTo: "txtUserGender",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUserEnglish",
        text: "",
        renderTo: "txtUserEnglish",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUserIdentity",
        text: "",
        renderTo: "txtUserIdentity",
        width: 100
    });

    Ext.create('Jit.form.field.Date', {
        id: "txtUserBirthday",
        text: "",
        renderTo: "txtUserBirthday",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtUserPwd",
        text: "",
        renderTo: "txtUserPwd",
        inputType: "password",
        value: "888888",
        width: 100
    });

    Ext.create('Jit.form.field.Date', {
        id: "txtFailDate",
        text: "",
        renderTo: "txtFailDate",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCellPhone",
        text: "",
        renderTo: "txtCellPhone",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtTelPhone",
        text: "",
        renderTo: "txtTelPhone",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtEmail",
        text: "",
        renderTo: "txtEmail",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtQQ",
        text: "",
        renderTo: "txtQQ",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtMSN",
        text: "",
        renderTo: "txtMSN",
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtBlog",
        text: "",
        renderTo: "txtBlog",
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtAddress",
        text: "",
        renderTo: "txtAddress",
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtPostcode",
        text: "",
        renderTo: "txtPostcode",
        width: 100
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 330,
        height: 70,
        margin: '0 0 10 10'

    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCreateUserName",
        text: "",
        renderTo: "txtCreateUserName",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCreateTime",
        text: "",
        renderTo: "txtCreateTime",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtModifyUserName",
        text: "",
        renderTo: "txtModifyUserName",
        readOnly: true,
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtModifyTime",
        text: "",
        renderTo: "txtModifyTime",
        readOnly: true,
        width: 100
    });


    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:0px;padding-bottom:0px;border:0px;',
        //layout: 'anchor',
        layout: {
            type: 'table'
            , columns: 3
            , align: 'right'
        },
        defaults: {},

        items: [
        ]
        , buttonAlign: "left"
        , buttons: [
        {
            xtype: "jitbutton",
            text: "保存",
            formBind: true,
            disabled: true,
            handler: fnSave
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });


    // role list
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "btnRoleCreate",
        //hidden: __getHidden("create"),
        handler: fnAddRole
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("userEditRoleStore"),
        id: "gridRole",
        renderTo: "gridRoleView",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("userEditRoleStore"),
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
            dataIndex: 'Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteRole('" + value + "')\">删除</a>";
                if (d.order_status == "1") {
                    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
                    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
                }
                return str;
            }
        }
        , {
            text: '角色',
            width: 110,
            sortable: true,
            dataIndex: 'RoleName',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '单位',
            width: 330,
            sortable: true,
            dataIndex: 'UnitName',
            align: 'left'
        }
        , {
            text: '缺省标识',
            width: 110,
            sortable: true,
            dataIndex: 'DefaultFlag',
            align: 'left',
            renderer: function (value, p, record) {
                var str = "否";
                var d = record.data;
                if (getStr(d.DefaultFlag) == "1") str = "是";
                return str;
            }
        }
        ]
    });

}