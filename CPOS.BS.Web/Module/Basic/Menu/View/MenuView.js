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
            jitSize: 'small',
            selectFn: function () {
                debugger;
                if (Ext.getCmp("txtAppSys").jitGetValue() != null && Ext.getCmp("txtAppSys").jitGetValue() != "") {
                    var menuStore = Ext.getStore('meauTreeStore');
                    menuStore.proxy.url = "Handler/MeauTreeHandler.ashx?reg_app_id=" + Ext.getCmp("txtAppSys").jitGetValue();
                    menuStore.load();
                }


            }
        }
        //        , {
        //            xtype: "jitbizmenuselecttree",
        //            fieldLabel: "父菜单",
        //            id: "txtParentMenuId",
        //            name: "ParentMenuId",
        //            jitSize: 'small'
        //        }
        //        , {
        //            xtype: "jittextfield",
        //            fieldLabel: "菜单编码",
        //            id: "txtMenuCode",
        //            name: "menu_code",
        //            jitSize: 'small'
        //        }
        //        , {
        //            xtype: "jittextfield",
        //            fieldLabel: "菜单名称",
        //            id: "txtMenuName",
        //            name: "menu_name",
        //            jitSize: 'small'
        //        }
        //        , {
        //            xtype: "jitbutton",
        //            text: "查询",
        //            //hidden: __getHidden("search"),
        //            margin: '0 0 10 14',
        //            handler: fnSearch
        //        , jitIsHighlight: true
        //        , jitIsDefaultCSS: true
        //        }
]

    });
    //operator area
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "span_create",
        //hidden: __getHidden("create"),
        handler: fnCreate
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });


    Ext.create('Ext.menu.Menu', {
        id: 'ctnMenu', items: [{
            id: 'ctnMenuItemAdd',
            text: '删除菜单'
        }]
    });

    //    Ext.create('Ext.tree.Panel', {
    //        id: "meauTree",
    //        title: '菜单信息',
    //        width: 400,
    //        height: DefaultGridHeight,
    //        rootVisible: false,
    //        renderTo: DivGridView,
    //        store: Ext.getStore("meauTreeStore")
    //    })
    Ext.create('Ext.panel.Panel', {
        id: 'pnlContainer',
        renderTo: 'DivGridView',
        layout: {
            type: 'hbox',
            align: 'stretch'
        },
        border: 0,
        margin: '5 0 5 5',
        items: [{
            xtype: 'treepanel',
            width: 400,
            height: 550,
            margin: '0 2 0 0',
            id: 'meauTree',
            title: '菜单信息',
            store: Ext.getStore('meauTreeStore'),
            rootVisible: false
        }]
    });
    //list area
    //    Ext.create('Ext.grid.Panel', {
    //        store: Ext.getStore("menuStore"),
    //        id: "gridView",
    //        renderTo: "DivGridView",
    //        columnLines: true,
    //        height: DefaultGridHeight,
    //        width: DefaultGridWidth,
    //        stripeRows: true,
    //        selModel: Ext.create('Ext.selection.CheckboxModel', {
    //            mode: 'MULTI'
    //        }),
    //        bbar: new Ext.PagingToolbar({
    //            displayInfo: true,
    //            id: "pageBar",
    //            defaultType: 'button',
    //            store: Ext.getStore("menuStore"),
    //            pageSize: JITPage.PageSize.getValue()
    //        }),
    //        listeners: {
    //            render: function (p) {
    //                p.setLoading({
    //                    store: p.getStore()
    //                }).hide();
    //            }
    //        },
    //        columns: [{
    //            text: '操作',
    //            width: 60,
    //            sortable: true,
    //            dataIndex: 'Menu_Id',
    //            align: 'left',
    //            //hidden: __getHidden("delete"),
    //            renderer: function (value, p, record) {
    //                var str = "";
    //                var d = record.data;
    //                if (d.Menu_Status != "-1") {
    //                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDelete('" + value + "')\">删除</a>";
    //                }
    //                return str;
    //            }
    //        }
    //        , {
    //            text: '应用系统',
    //            width: 150,
    //            sortable: true,
    //            dataIndex: 'Reg_App_Name',
    //            align: 'left'
    //        }
    //        , {
    //            text: '菜单编码',
    //            width: 200,
    //            sortable: true,
    //            dataIndex: 'Menu_Code',
    //            align: 'left'
    //            ,renderer: function (value, p, record) {
    //                var str = "";
    //                var d = record.data;
    //                str += "<a class=\"pointer z_col_light_text\" onclick=\"fnView('" + d.Menu_Id + "')\">" + value + "</a>";
    //                return str;
    //            }
    //        }, {
    //            text: '菜单名称',
    //            width: 200,
    //            sortable: true,
    //            dataIndex: 'Menu_Name',
    //            align: 'left'
    //        }, {
    //            text: '显示序号',
    //            width: 110,
    //            sortable: true,
    //            dataIndex: 'Display_Index',
    //            align: 'left'
    //        }
    //        ]
    //    });



}