function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 30,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tab1',
            title: '促销',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tab1");
                    tmp.style.display = "";
                    tmp.style.height = "30px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fuGetTemplate('促销');
                }
            }

        }
        , {
            contentEl: 'tab2',
            title: '兑礼',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tab2");
                    tmp.style.display = "";
                    tmp.style.height = "30px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fuGetTemplate('兑礼');
                }
            }
        }, {
            contentEl: 'tab3',
            title: '升保级',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tab3");
                    tmp.style.display = "";
                    tmp.style.height = "30px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fuGetTemplate('升保级');
                }
            }
        }, {
            contentEl: 'tab4',
            title: '到期提醒',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tab4");
                    tmp.style.display = "";
                    tmp.style.height = "30px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fuGetTemplate('到期提醒');
                }
            }
        }, {
            contentEl: 'tab5',
            title: '其他',
            listeners: {
                activate: function (tab) {
                    var tmp = get("tab5");
                    tmp.style.display = "";
                    tmp.style.height = "30px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                    fuGetTemplate('其他');
                }
            }
        }
        ]
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtTemplateId",
        text: "",
        renderTo: "txtTemplateId",
        width: 100
        , hidden: true

    });

    Ext.create('Jit.form.field.Text', {
        id: "txtTemplateType",
        text: "",
        renderTo: "txtTemplateType",
        width: 100
        , hidden: true

    });

    Ext.create('Jit.form.field.Text', {
        id: "txtTemplateDesc1",
        text: "",
        renderTo: "txtTemplateDesc1",
        width: 200

    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtTemplateDesc",
        text: "",
        renderTo: "txtTemplateDesc",
        width: 500,
        height: 70
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtTemplateDescSMS",
        text: "",
        renderTo: "txtTemplateDescSMS",
        width: 500,
        height: 70
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtTemplateDescAPP",
        text: "",
        renderTo: "txtTemplateDescAPP",
        width: 500,
        height: 70
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("TemplateStore"),
        id: "gridRole",
        renderTo: "gridTab1",
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
            store: Ext.getStore("TemplateStore"),
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
            dataIndex: 'TemplateID',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "','" + d.TemplateType + "')\">删除</a>";
                str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnModify('" + value + "')\">编辑</a>"; //,'" + d.TemplateType + "','" + d.TemplateContent + "','" + d.TemplateDesc + "'

                return str;
            }
        }, {
            text: '简要描述',
            width: 150,
            sortable: true,
            dataIndex: 'TemplateDesc',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '创建时间',
            width: 250,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '更新时间',
            width: 250,
            sortable: true,
            dataIndex: 'LastUpdateTime',
            align: 'left'
        }
        , {
            text: '话术内容',
            width: 430,
            sortable: true,
            dataIndex: 'TemplateContent',
            align: 'left'
        }
        ]
    });

    Ext.create('Jit.button.Button', {
        text: "清 空",
        renderTo: "btnEmpty"
        //hidden: __getHidden("create"),
        , handler: fnEmpty
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    Ext.create('Jit.button.Button', {
        text: "保 存",
        renderTo: "btnSave"
        //hidden: __getHidden("create"),
        ,handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

}