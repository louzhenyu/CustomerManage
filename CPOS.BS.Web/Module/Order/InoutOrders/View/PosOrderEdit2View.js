function InitView() {

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
            fieldLabel: "单据号码",
            name: "order_no",
            id: "txtOrderNo",
            readOnly: true,
            allowBlank: false
        },
        {
            xtype: "jitdatefield",
            fieldLabel: "单据日期",
            name: "order_date",
            id: "txtOrderDate",
            format: 'Y-m-d',
            isDefault: false,
            readOnly: true,
            allowBlank: false
        },
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "消费门店",
            name: "sales_unit_id",
            id: "txtSalesUnitId",
            isDefault: false,
            readOnly: true,
            allowBlank: false
            , jitSize: 'small'
        },
        {
            xtype: "jittextfield",
            fieldLabel: "当前状态",
            name: "Field10",
            id: "txtField10",
            value: 0,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "会员",
            name: "vip_name",
            id: "txtVipName",
            value: 0,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "会员手机",
            name: "vip_phone",
            id: "txtVipPhone",
            value: 0,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "支付方式",
            name: "DefrayTypeName",
            id: "txtDefrayTypeName",
            value: 0,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "配送方式",
            name: "DeliveryName",
            id: "txtDeliveryName",
            value: 0,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "来源",
            name: "data_from_name",
            id: "txtDataFromName",
            value: 0,
            colspan: 1,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "处理门店",
            name: "purchase_unit_name",
            id: "txtPurchaseUnitName",
            colspan: 3,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "消费金额",
            name: "total_amount",
            id: "txtTotalAmount",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "购买数量",
            name: "total_num",
            id: "txtTotalNum",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "折扣",
            name: "discount_rate",
            id: "tbDiscountRate",
            value: 0,
            readOnly: true,
            allowBlank: true,
            colspan: 2,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "实付金额",
            name: "actual_amount",
            id: "txtActualAmount",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "找零",
            name: "keep_the_change",
            id: "tbKeepTheChange",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "抹零",
            name: "wiping_zero",
            id: "tbWipingZero",
            value: 0,
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "实收金额",
            name: "total_retail",
            id: "txtTotalRetail",
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "交易时间",
            name: "create_time",
            id: "tbSYCreateTime",
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "发货时间",
            name: "send_time",
            id: "txtSendTime",
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "取消时间",
            name: "modify_time",
            id: "txtCancelTime",
            colspan: 2,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "创建日期",
            name: "create_time",
            id: "txtCreateTime",
            allowBlank: true,
            readOnly: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "创建人",
            name: "create_user_name",
            id: "txtCreateUserName",
            allowBlank: true,
            readOnly: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "最后修改时间",
            name: "modify_time",
            id: "txtModifyTime",
            allowBlank: true,
            readOnly: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "最后修改人",
            name: "modify_user_name",
            id: "txtModifyUserName",
            allowBlank: true,
            readOnly: true
        }
        ]
    });

    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divDetail",
        id: "editPanel2",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;padding-bottom:0px;border-bottom:0px;',
        layout: {
            type: 'table'
            , columns: 3
            , align: 'right'
        },
        defaults: {},

        items: [{
            xtype: "jittextfield",
            fieldLabel: "收件人",
            name: "Field14",
            id: "txtField14",
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "联系电话",
            name: "Field6",
            id: "txtField6",
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "邮编",
            name: "Field5",
            id: "txtField5",
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "收货地址",
            name: "Field4",
            id: "txtField4",
            colspan: 3,
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "配送商",
            name: "carrier_name",
            id: "txtCarrierName",
            readOnly: true,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "配送单号",
            name: "Field2",
            id: "txtField2",
            readOnly: true,
            allowBlank: true
        }
        ]
    });

    var z_sku_prop_cfg = Ext.create('jit.biz.SkuPropCfg', {});

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderEditItemStore"),
        id: "gridView",
        renderTo: "grid",
        columnLines: true,
        stripeRows: true,
        height: 284,
        width: "100%",
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
        { header: "操作",
            id: 'ListOp', width: 0, sortable: false, dataIndex: 'order_detail_id', menuDisabled: true,
            //hidden: (op == "edit" || op == "new" ? false : true),
            renderer: function (value, p, record) {
                var str = "";
                var d = record.data;
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
                //str += "<a class=\"z_op_link\" href=\"#\" onclick=\"fnAddItem('" +
                //    value + "', 'edit', '" + param + "')\">" + value + "</a>";
                return value;
            }
        },
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
        { text: "数量", dataIndex: 'order_qty', width: 90, menuDisabled: true, align: "right", flex: 1 },
        { text: "折扣", dataIndex: 'discount_rate', width: 90, menuDisabled: true, align: "right", flex: 1 },
        { text: "标准价", dataIndex: 'enter_price', width: 90, menuDisabled: true, align: "right", flex: 1 },
        { text: "总金额", dataIndex: 'retail_amount', width: 90, menuDisabled: true, align: "right", flex: 1 }
        , { text: "备注", dataIndex: 'remark', width: 90, menuDisabled: true, align: "right", flex: 1 }
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
        ,
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });


}