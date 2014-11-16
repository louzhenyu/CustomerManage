function InitView() {


    Ext.define("Post", {
        extend: 'Ext.data.Model',
        proxy: {
            type: 'ajax',
            url: '/Framework/Javascript/Biz/Handler/VipSourceHandler.ashx?method=GetVipByPhone',
            reader: {
                type: 'json',
                root: 'data',
                totalProperty: 'totalCount'
            }
        },
        fields: [
            { name: 'VIPID', mapping: 'VIPID' },
            { name: 'VipName', mapping: 'VipName' },
            { name: 'VipCode', mapping: 'VipCode' }
        ]
    });

    var vipDs = Ext.create('Ext.data.Store', {
        model: 'Post',
    });


    
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divMain",
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;padding-bottom:0px;border-bottom:0px;',
        layout: {
            type: 'table'
            , columns: 4
            , align: 'right'
        },
        defaults: {},

        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>单据号码",
            name: "order_no",
            id: "txtOrderNo",
            readOnly: true,
            colspan:4,
            allowBlank: false
        },
        {
            name: "vipNo",
            xtype: "hidden",
            id: "vipNo"
        },
        {
            xtype: 'combo',
            fieldLabel: "<font color='red'>*</font>用户",
            store: vipDs,//Ext.getStore("vipStore"),
            displayField: 'VipName',
            hideTrigger: true,
            emptyText: "请输入用户手机号"
            , labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            //, queryMode: 'local'
            , margin: '0 10 10 10'
            , width: 183
            , labelWidth: 73
            , height: 22,
            //, matchFieldWidth: false,
            listConfig: {
                loadingText: 'Searching...',
                emptyText: '没有查询结果',
                getInnerTpl: function () {
                    return '<div>{VipName}/{VipCode}</div>';
                }
            },
            listeners: {
                select: function (combo, selection) {
                    var post = selection[0];
                    Ext.getCmp("vipNo").setValue(post.get("VIPID"));

                }
            }
        },
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "<font color='red'>*</font>销售单位",
            name: "sales_unit_id",
            id: "txtSalesUnitId",
            isDefault: false,
            allowBlank: false
            , jitSize: 'small'
        },
        {
            xtype: "jitdatefield",
            fieldLabel: "<font color='red'>*</font>单据日期",
            name: "order_date",
            id: "txtOrderDate",
            format: 'Y-m-d',
            isDefault: false,
            allowBlank: false,
            colspan:2
        }
        //,
        //{
        //    xtype: "jitdatefield",
        //    fieldLabel: "<font color='red'>*</font>销售日期",
        //    name: "complete_date",
        //    id: "txtCompleteDate",
        //    format: 'Y-m-d',
        //    isDefault: false,
        //    allowBlank: false
        //}
        //,
        //{
        //    xtype: "jitbizwarehouse"
        //    , fieldLabel: "<font color='red'>*</font>仓库"
        //    , name: "sales_warehouse_id"
        //    , id: "txtSalesWarehuouse"
        //    , parent_id: "txtSalesUnitId"
        //    , isDefault: false
        //    , allowBlank: false
        //}
        ,{
            xtype: "jittextfield",
            fieldLabel: "备注",
            name: "remark",
            id: "txtRemark",
            isDefault: false,
            allowBlank: true,
            colspan: 2,
            width: 387
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "总金额",
            name: "total_amount",
            id: "txtTotalAmount",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;color:rgb(169,169,169);",
            labelStyle: "color:rgb(130,130,130)"

        },
        {
            xtype: "jittextfield",
            fieldLabel: "总数量",
            name: "total_num",
            id: "txtTotalNum",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;color:rgb(169,169,169);",
            labelStyle: "color:rgb(130,130,130)"
        },   

        {
            xtype: "jittextfield",
            fieldLabel: "创建人",
            name: "create_user_name",
            id: "txtCreateUserName",
            allowBlank: true,
            readOnly: true,            
            hidden: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "创建日期",
            name: "create_time",
            id: "txtCreateTime",
            allowBlank: true,
            colspan: 2,
            hidden: true,
            //width: 220,
            readOnly: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "最后修改人",
            name: "modify_user_name",
            id: "txtModifyUserName",
            allowBlank: true,
            hidden: true,
            readOnly: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "最后修改时间",
            name: "modify_time",
            id: "txtModifyTime",
            hidden: true,
            allowBlank: true,
            colspan: 2,
            readOnly: true
        }
        ]
    });

    Ext.create('Jit.button.Button', {
        id: "btnAddItem" + "_ext",
        text: "添加商品",
        renderTo: "btnAddItem",
        hidden: false,
        margin: '0 0 0 3',
        handler: function () {
            eval("fnAddItem()");
        }
    });

    var z_sku_prop_cfg = Ext.create('jit.biz.SkuPropCfg', {});

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderEditItemStore"),
        id: "gridView",
        renderTo: "grid",
        columnLines: true,
        stripeRows: true,
        //    height: 284,
        //    width: "100%",
        height: DetailGridHeight,
        width: DetailGridWidth,
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("salesOutOrderEditItemStore"),
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
                header: "操作",
                id: 'ListOp', width: 90, sortable: false, dataIndex: 'order_detail_id', menuDisabled: true,
                //hidden: (op == "edit" || op == "new" ? false : true),
                renderer: function (value, p, record) {
                    var str = "";
                    var d = record.data;
                    if (("myOrderStatus" in window)) {
                        if (parseInt(myOrderStatus) >= 10) return "";
                    }
                    var param = "";
                    param += "&index=" + getStr(record.index);
                    param += "&order_detail_id=" + getStr(d.order_detail_id);
                    param += "&sku_id=" + getStr(d.sku_id);
                    param += "&item_code=" + getStr(d.item_code);
                    param += "&enter_qty=" + getStr(d.enter_qty);
                    param += "&order_qty=" + getStr(d.order_qty);
                    param += "&enter_price=" + getStr(d.enter_price);
                    param += "&retail_amount=" + getStr(d.retail_amount);
                    param += "&enter_amount=" + getStr(d.enter_amount);
                    //                str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnEditItem('" +
                    //                    value + "', 'edit', '" + param + "')\">修改</a>";
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnDeleteItem('" +
                        value + "', 'delete', '" + param + "')\">删除</a>";
                    return str;
                }
            },
            {
                text: '商品代码', width: 110, sortable: true, dataIndex: 'item_code', align: 'left'
                , renderer: function (value, p, record) {
                    var str = "";
                    var d = record.data;
                    if (("myOrderStatus" in window)) {
                        if (parseInt(myOrderStatus) >= 10) return value;
                    }
                    var param = "";
                    param += "&index=" + getStr(record.index);
                    param += "&order_detail_id=" + getStr(d.order_detail_id);
                    param += "&sku_id=" + getStr(d.sku_id);
                    param += "&item_code=" + getStr(d.item_code);
                    param += "&enter_qty=" + getStr(d.enter_qty);
                    param += "&order_qty=" + getStr(d.order_qty);
                    param += "&enter_price=" + getStr(d.enter_price);
                    param += "&retail_amount=" + getStr(d.retail_amount);
                    param += "&enter_amount=" + getStr(d.enter_amount);
                    str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnAddItem('" +
                        value + "', 'edit', '" + param + "')\">" + value + "</a>";
                    return str;
                }
            },
            {
                text: '商品名称', width: 110, sortable: true, dataIndex: 'item_name', align: 'left'
            },
            {
                id: "g_sp_1", dataIndex: 'prop_1_detail_name', width: 90, menuDisabled: true, flex: 1,
                text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_1_name : "规格",
                hidden: z_sku_prop_cfg.sku_prop_1 == "1" ? false : true
            },
            {
                id: "g_sp_2", dataIndex: 'prop_2_detail_name', width: 90, menuDisabled: true, flex: 1,
                text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_2_name : "规格",
                hidden: z_sku_prop_cfg.sku_prop_2 == "1" ? false : true
            },
            {
                id: "g_sp_3", dataIndex: 'prop_3_detail_name', width: 90, menuDisabled: true, flex: 1,
                text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_3_name : "规格",
                hidden: z_sku_prop_cfg.sku_prop_3 == "1" ? false : true
            },
            {
                id: "g_sp_4", dataIndex: 'prop_4_detail_name', width: 90, menuDisabled: true, flex: 1,
                text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_4_name : "规格",
                hidden: z_sku_prop_cfg.sku_prop_4 == "1" ? false : true
            },
            {
                id: "g_sp_5", dataIndex: 'prop_5_detail_name', width: 90, menuDisabled: true, flex: 1,
                text: z_sku_prop_cfg.sku_prop_1 == "1" ? z_sku_prop_cfg.sku_prop_5_name : "规格",
                hidden: z_sku_prop_cfg.sku_prop_5 == "1" ? false : true
            },
            //{ text: "预订数量", dataIndex: 'enter_qty', width: 90, menuDisabled: true, align: "right", flex: 1 },
            { text: "数量", dataIndex: 'order_qty', width: 90, menuDisabled: true, align: "right", flex: 1 },
            { text: "单价", dataIndex: 'enter_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
            { text: "金额", dataIndex: 'retail_amount', width: 90, menuDisabled: true, align: "right", flex: 1 }

        ]
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
                hidden: false,
                handler: fnSave
                , id: "btnSave"
            },
            {
                xtype: "jitbutton",
                text: "关闭",
                handler: fnClose
            }
            ]
    });


}