function InitView() {

    var tabs = Ext.widget('tabpanel', {
        id: "tabs3",
        renderTo: 'tabsMain',
        width: '100%',
        height: 451,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '基本信息'
        }
        ,{
            contentEl:'tabProp', 
            title: '积分变更记录',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabProp");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        ,{
            contentEl:'tabProp2', 
            title: '门店消费记录',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabProp2");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        ,{
            contentEl:'tabProp3', 
            title: '下线清单及积分',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabProp3");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
         //, {
         //    contentEl: 'tabProp4',
         //    title: '采集到属性列表',
         //    listeners: {
         //        activate: function (tab) {
         //            var tmp = get("tabProp4");
         //            tmp.style.display = "";
         //            tmp.style.height = "451px";
         //            tmp.style.overflow = "";
         //            tmp.style.background = "rgb(241, 242, 245)";
         //        }
         //    }
         //}
        ,{
            contentEl:'tabProp5', 
            title: '会员具备的标签',
            listeners: {
                activate: function(tab) {
                    var tmp = get("tabProp5");
                    tmp.style.display = "";
                    tmp.style.height = "451px";
                    tmp.style.overflow = "";
                    tmp.style.background = "rgb(241, 242, 245)";
                }
            }
        }
        ]
    });
    
    Ext.create('jit.biz.Tags', {
        id: "txtTags",
        text: "",
        renderTo: "txtTags",
        width: 100
    });
    Ext.create('Jit.button.Button', {
        text: "添加",
        renderTo: "txtTagsAdd",
        handler: fnTagsAdd
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtVipCode",
        text: "",
        renderTo: "txtVipCode",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtVipName",
        text: "",
        renderTo: "txtVipName",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtPhone",
        text: "",
        renderTo: "txtPhone",
        readOnly: true,
        width: 100
    });
    
    Ext.create('jit.biz.VipSource', {
        id: "txtVipSource",
        text: "",
        renderTo: "txtVipSource",
        readOnly: true,
        width: 100
    });
    
    Ext.create('jit.biz.VipLevel', {
        id: "txtVipLevel",
        text: "",
        renderTo: "txtVipLevel",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtIntegration",
        text: "",
        renderTo: "txtIntegration",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtRegistrationTime",
        text: "",
        renderTo: "txtRegistrationTime",
        dataType: "yn",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtRecentlySalesTime",
        text: "",
        renderTo: "txtRecentlySalesTime",
        readOnly: true,
        width: 100
    });

    Ext.create('jit.biz.Status', {
        id: "txtVipStatus",
        text: "",
        renderTo: "txtVipStatus",
        readOnly: true,
        dataType: "vip_status",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtChangeIntegral",
        value: "0",
        renderTo: "txtChangeIntegral",
        decimalPrecision : 2, 
        allowDecimals : true,
        width: 100,
        listeners: {
            blur: function (p) {
                Ext.getCmp("txtChangeIntegral").setValue(parseFloat(Ext.getCmp("txtChangeIntegral").getValue()));
            }
        }
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtChangeIntegralRemark",
        value: "",
        renderTo: "txtChangeIntegralRemark",
        width: 300
    });
    Ext.create('Jit.button.Button', {
        text: "确定",
        renderTo: "txtChangeIntegralAdd",
        handler: fnChangeIntegralAdd
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
        ,buttonAlign: "left"
        ,buttons: [
        //{
        //    xtype: "jitbutton",
        //    text: "保存",
        //    formBind: true,
        //    disabled: true,
        //    handler: fnSave
        //},
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });

    
    // gridVipIntegralDetailView list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipIntegralDetailStore"),
        id: "gridVipIntegralDetail",
        renderTo: "gridVipIntegralDetailView",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBarVipIntegral",
            defaultType: 'button',
            store: Ext.getStore("vipIntegralDetailStore"),
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
            width: 50,
            sortable: true,
            dataIndex: 'Id',
            align: 'left',
            //hidden: __getHidden("delete"),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteRole('" + value + "')\">删除</a>";
                //if (d.order_status == "1") {
                //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
                //    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
                //}
                return str;
            }
        }
        ,{
            text: '消费金额',
            width: 110,
            sortable: true,
            dataIndex: 'SalesAmount',
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '产生积分',
            width: 110,
            sortable: true,
            dataIndex: 'Integral',
            align: 'left'
        }
        ,{
            text: '积分来源',
            width: 230,
            sortable: true,
            dataIndex: 'IntegralSourceName',
            align: 'left'
        }
        ,{
            text: '创建时间',
            width: 150,
            sortable: true,
            dataIndex: 'CreateTime',
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
    
    var z_sku_prop_cfg = Ext.create('jit.biz.SkuPropCfg', {});
    
    // gridPosOrderListView list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("PosOrderListStore"),
        id: "gridPosOrderList",
        renderTo: "gridPosOrderListView",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: "100%",
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBarPosOrderList",
            defaultType: 'button',
            store: Ext.getStore("PosOrderListStore"),
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
        //    {
        //    text: '操作',
        //    width: 50,
        //    sortable: true,
        //    dataIndex: 'Id',
        //    align: 'left',
        //    //hidden: __getHidden("delete"),
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteRole('" + value + "')\">删除</a>";
        //        //if (d.order_status == "1") {
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        //        //    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        //        //}
        //        return str;
        //    }
        //}
        {
            text: '商品名称', width: 110, sortable: true, dataIndex: 'item_name', align: 'left'
        },
        { id: "g_sp_1", dataIndex: 'prop_1_detail_name', width: 90, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_1_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_1 == "1" ? false : true
        },
        { id: "g_sp_2", dataIndex: 'prop_2_detail_name', width: 90, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_2_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_2 == "1" ? false : true
        },
        { id: "g_sp_3", dataIndex: 'prop_3_detail_name', width: 90, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_3_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_3 == "1" ? false : true
        },
        { id: "g_sp_4", dataIndex: 'prop_4_detail_name', width: 90, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_4_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_4 == "1" ? false : true
        },
        { id: "g_sp_5", dataIndex: 'prop_5_detail_name', width: 90, menuDisabled: true, flex: 1,
            text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_5_name : "规格",
            hidden: z_sku_prop_cfg.sku_prop_5 == "1" ? false : true
        },
        { text: "单价", dataIndex: 'std_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
        { text: "折扣", dataIndex: 'discount_rate', width: 90, menuDisabled: true, align: "right", flex: 1 },
        { text: "标准价", dataIndex: 'enter_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
        { text: "总金额", dataIndex: 'retail_amount', width: 90, menuDisabled: true, align: "right", flex: 1 }
        , { text: "备注", dataIndex: 'remark', width: 90, menuDisabled: true, align: "right", flex: 1 }
        ]
    });

    
    // gridNextLevelUserListView list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("NextLevelUserListStore"),
        id: "gridNextLevelUserList",
        renderTo: "gridNextLevelUserListView",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: "100%",
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBarNextLevelUser",
            defaultType: 'button',
            store: Ext.getStore("NextLevelUserListStore"),
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
        //    {
        //    text: '操作',
        //    width: 50,
        //    sortable: true,
        //    dataIndex: 'Id',
        //    align: 'left',
        //    //hidden: __getHidden("delete"),
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteRole('" + value + "')\">删除</a>";
        //        //if (d.order_status == "1") {
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        //        //    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        //        //}
        //        return str;
        //    }
        //}
        {
            text: '会员名',
            width: 150,
            sortable: true,
            dataIndex: 'VipName',
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '积分',
            width: 150,
            sortable: true,
            dataIndex: 'Integration',
            align: 'left'
        }
        ]
    });

    // gridCollectionPropertyView list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("CollectionPropertyStore"),
        id: "gridCollectionPropertyList",
        renderTo: "gridCollectionPropertyView",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: "100%",
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBarCollectionProperty",
            defaultType: 'button',
            store: Ext.getStore("NextLevelUserListStore"),
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
        //    {
        //    text: '操作',
        //    width: 50,
        //    sortable: true,
        //    dataIndex: 'Id',
        //    align: 'left',
        //    //hidden: __getHidden("delete"),
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteRole('" + value + "')\">删除</a>";
        //        //if (d.order_status == "1") {
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        //        //    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        //        //}
        //        return str;
        //    }
        //}
        {
            text: '属性名',
            width: 150,
            sortable: true,
            dataIndex: 'ParameterName',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '属性值',
            width: 150,
            sortable: true,
            dataIndex: 'ParameterValue',
            align: 'left'
        }
        ]
    });
    
    
    // gridVipTagsView list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipTagsStore"),
        id: "gridVipTags",
        renderTo: "gridVipTagsView",
        columnLines: true,
        height: 366,
        stripeRows: true,
        width: "100%",
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBarVipTags",
            defaultType: 'button',
            store: Ext.getStore("vipTagsStore"),
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
        //    {
        //    text: '操作',
        //    width: 50,
        //    sortable: true,
        //    dataIndex: 'Id',
        //    align: 'left',
        //    //hidden: __getHidden("delete"),
        //    renderer: function (value, p, record) {
        //        var str = "";
        //        var d = record.data;
        //        //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteVipTags('" + d.MappingId + "')\">删除</a>";
        //        //if (d.order_status == "1") {
        //        //    //str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnEdit('" + value + "')\">修改</a>";
        //        //    str += "<a class=\"z_op_link2\" href=\"#\" onclick=\"fnPass('" + value + "')\">审核</a>";
        //        //}
        //        return str;
        //    }
        //}
        {
            text: '标签名称',
            width: 250,
            sortable: true,
            dataIndex: 'TagsName',
            align: 'left'
            ,renderer: function (value, p, record) {
                return value;
            }
        }
        ,{
            text: '标签内容',
            width: 410,
            sortable: true,
            dataIndex: 'TagsDesc',
            align: 'left'
        }
        ]
    });

}