function InitView() {

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

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtVipCode",
    //    text: "",
    //    renderTo: "txtVipCode",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtVipName",
    //    text: "",
    //    renderTo: "txtVipName",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtPhone",
    //    text: "",
    //    renderTo: "txtPhone",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('jit.biz.VipSource', {
    //    id: "txtVipSource",
    //    text: "",
    //    renderTo: "txtVipSource",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('jit.biz.VipLevel', {
    //    id: "txtVipLevel",
    //    text: "",
    //    renderTo: "txtVipLevel",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtIntegration",
    //    text: "",
    //    renderTo: "txtIntegration",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtRegistrationTime",
    //    text: "",
    //    renderTo: "txtRegistrationTime",
    //    dataType: "yn",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtRecentlySalesTime",
    //    text: "",
    //    renderTo: "txtRecentlySalesTime",
    //    readOnly: true,
    //    width: 100
    //});

    //Ext.create('jit.biz.Status', {
    //    id: "txtVipStatus",
    //    text: "",
    //    renderTo: "txtVipStatus",
    //    readOnly: true,
    //    dataType: "vip_status",
    //    width: 100
    //});

    Ext.create('Jit.form.field.Number', {
        id: "txtChangeIntegral",
        value: "0",
        renderTo: "txtChangeIntegral",
        decimalPrecision: 2,
        allowDecimals: true,
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




    // gridVipIntegralDetailView list
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("vipIntegralDetailStore"),
        id: "gridVipIntegralDetail",
        renderTo: "gridVipIntegralDetailView",
        columnLines: true,
        height: 306,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
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
        , {
            text: '消费金额',
            width: 110,
            sortable: true,
            dataIndex: 'SalesAmount',
            align: 'left'
            , renderer: function (value, p, record) {
                return value;
            }
        }
        , {
            text: '产生积分',
            width: 60,
            sortable: true,
            dataIndex: 'Integral',
            align: 'left'
        }
        , {
            text: '积分来源',
            width: 120,
            sortable: true,
            dataIndex: 'IntegralSourceName',
            align: 'left'
        }
        , {
            text: '积分来源会员',
            width: 180,
            sortable: true,
            dataIndex: 'FromVipName',
            align: 'left'
        }
        , {
            text: '备注',
            width: 100,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }
        , {
            text: '创建人',
            width: 80,
            sortable: true,
            dataIndex: 'CreateBy',
            align: 'left'
        }
        , {
            text: '产生时间',
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
        height: 306,
        stripeRows: true,
        width: "100%",
        //selModel: Ext.create('Ext.selection.CheckboxModel', {
        //    mode: 'MULTI'
        //}),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar2",
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
        { text: "实际金额", dataIndex: 'retail_amount', width: 90, menuDisabled: true, align: "right", flex: 1 }
        , { text: "备注", dataIndex: 'remark', width: 90, menuDisabled: true, align: "right", flex: 1 }
        ]
});


// gridNextLevelUserListView list
Ext.create('Ext.grid.Panel', {
    store: Ext.getStore("NextLevelUserListStore"),
    id: "gridNextLevelUserList",
    renderTo: "gridNextLevelUserListView",
    columnLines: true,
    height: 306,
    stripeRows: true,
    width: "100%",
    //selModel: Ext.create('Ext.selection.CheckboxModel', {
    //    mode: 'MULTI'
    //}),
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBar3",
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
            , renderer: function (value, p, record) {
                return value;
            }
    }
        , {
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
        id: "pageBar3",
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
    height: 306,
    stripeRows: true,
    width: "100%",
    //selModel: Ext.create('Ext.selection.CheckboxModel', {
    //    mode: 'MULTI'
    //}),
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBar4",
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
            , renderer: function (value, p, record) {
                return value;
            }
    }
        , {
            text: '标签内容',
            width: 200,
            sortable: true,
            dataIndex: 'TagsDesc',
            align: 'left'
        }
        , {
            text: '标签类型',
            width: 210,
            sortable: true,
            dataIndex: 'TypeName',
            align: 'left'
        }
        ]
});

}