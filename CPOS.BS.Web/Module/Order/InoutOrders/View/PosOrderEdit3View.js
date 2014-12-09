function InitView() {

    //操作按钮
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        id: "TotalPanel",
        renderTo: 'divOperation',
        items: ary_items,
        //        items: [{
        //            xtype: "jitbutton",
        //            text: "审&nbsp;&nbsp;核",
        //            id: 'btn_1',
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnbtn_1
        //        }, {
        //            xtype: "jitbutton",
        //            text: "收&nbsp;&nbsp;款",
        //            id: 'btn_2',
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnbtn_2
        //        }, {
        //            xtype: "jitbutton",
        //            text: "发&nbsp;&nbsp;货",
        //            id: 'btn_3',
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnbtn_3
        //        }, {
        //            xtype: "jitbutton",
        //            text: "取&nbsp;&nbsp;消",
        //            id: 'btn_4',
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnbtn_4
        //        }, {
        //            xtype: "jitbutton",
        //            text: "完&nbsp;&nbsp;成",
        //            id: 'btn_5',
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnbtn_5
        //        }, {
        //            xtype: "jitbutton",
        //            text: "日&nbsp;&nbsp;志",
        //            id: 'btn_6',
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnShowLog
        //        },
        //        {
        //            xtype: "jitbutton",
        //            text: "关&nbsp;&nbsp;闭",
        //            jitIsHighlight: false,
        //            jitIsDefaultCSS: true,
        //            handler: fnClose
        //        }],
        margin: '10 0 10 0',
        layout: 'column',
        border: 0
    });

    //订单主信息
    Ext.create('Ext.form.Panel', {
        //title: "订单信息",
        collapsible: true,
        collapsed: true,
        header: {
            xtype: "header",
            //title: "订单信息",
            //titleAlign: "left",
            headerAsText: false,
            items: [{
                id: "lab_Status",
                labelPad: 5,
                lableWidth: 10,
                fieldStyle: "color:red;font-weight:bold;",
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>订单信息</b>',
                width: '200',
                value: ''
              
            }],
            //style: "cursor:pointer;",
            cls:"headerOver",
            listeners: {  //折叠展开事件
                "click": function () {
                    if(!Ext.getCmp("editPanel").collapsed)
                       Ext.getCmp("editPanel").collapse(Ext.Component.DIRECTION_TOP, true);
                    else
                        Ext.getCmp("editPanel").expand(true);
                }
            },
            layout: "hbox"
        },
        renderTo: "divMain",
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;padding-bottom:0px;border-bottom:0px;',
        layout: "column",
        defaults: {},

        items: [{ xtype: "container",
            layout: {
                type: 'table'
                , columns: 4
                , align: 'right'
            },
            items: [{
                xtype: "jittextfield",
                fieldLabel: "订单编号",
                name: "order_no",
                id: "txtOrderNo",
                readOnly: true,
                allowBlank: false,                     
                fieldCls: "txtDisable"
                // fieldStyle: 'background:red;',  
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
                ,fieldCls: "txtDisable"
            },
            {
                xtype: "jittextfield",
                fieldLabel: "来源",
                name: "data_from_name",
                id: "txtDataFromName",
                value: 0,
                //colspan: 1,
                readOnly: true,
                allowBlank: true
                    , fieldCls: "txtDisable"
            },
            {
                xtype: "jittextfield",
                fieldLabel: "交易时间",
                name: "create_time",
                id: "tbSYCreateTime",
                readOnly: true,
                width:250,
                allowBlank: true
                , fieldCls: "txtDisable"
            },
            {
                xtype: "jittextarea",//新增加的备注
                fieldLabel: "备注",
                name: "remark",
                id: "txtRemark",
                width: 385,
                height:60,
                colspan: 2,
                readOnly: true,
                //hidden: true,
                allowBlank: true
                ,fieldCls: "txtDisable"
               //, fieldStyle: "background:Transparent; border:0px; border-bottom:dotted 1px #000;"
            },
            {
                xtype: "jittextfield",
                fieldLabel: "取消时间",
                name: "modify_time",
                id: "txtCancelTime",
                colspan: 2,
                readOnly: true,
                allowBlank: true
                    , fieldCls: "txtDisable"
            }]
        },
        { xtype: "container",
            layout: {
                type: 'table'
                , columns: 4
                , align: 'right'
            },
            items: [
            {
                xtype: "jittextfield",
                fieldLabel: "创建日期",
                name: "create_time",
                id: "txtCreateTime",
                allowBlank: true,
                hidden: true,
                readOnly: true
                    , fieldCls: "txtDisable"
            },
            {
                xtype: "jittextfield",
                fieldLabel: "创建人",
                name: "create_user_name",
                id: "txtCreateUserName",
                allowBlank: true,
                hidden: true,
                readOnly: true
                    , fieldCls: "txtDisable"
            },
            {
                xtype: "jittextfield",
                fieldLabel: "最后修改时间",
                name: "modify_time",
                id: "txtModifyTime",
                allowBlank: true,
                hidden: true,
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
                    , fieldCls: "txtDisable"
            }]
        }
        ]
    });

    //配送信息
    Ext.create('Ext.panel.Panel', {
        //title: null,
        collapsible: true,//设置为可折叠
        collapsed: true,//目前是折叠状态
        id: "deliveryPanel",
        header: {
            xtype: "header",
            headerAsText: false,
            margin: "-1",
            items: [{
                id: "lab_Delivery",
                labelPad: 5,
                lableWidth: 10,
                fieldStyle: "color:red;font-weight:bold;",
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>配送信息</b>',
                value: ''
            }],
            cls: "headerOver",
            listeners: {  //折叠展开事件
                "click": function () {
                    if (!Ext.getCmp("deliveryPanel").collapsed)//不是折叠状态
                        Ext.getCmp("deliveryPanel").collapse(Ext.Component.DIRECTION_TOP, true);//向上折叠
                    else
                        Ext.getCmp("deliveryPanel").expand(true);
                }
            },
            layout: "hbox"
        },
        renderTo: "divDetail",//渲染在
        //id: "editPanel2",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;padding-bottom:10px;border-bottom:0px;',
        layout: {
            type: 'table'
            , columns: 4
            , align: 'right'
        },
        defaults: {},

        items: [{
            xtype: "jittextfield",
            fieldLabel: "收件人",
            name: "Field14",
            id: "txtField14",
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "联系电话",
            name: "Field6",
            id: "txtField6",
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "邮编",
            name: "Field5",
            hidden: true,
            id: "txtField5",
            allowBlank: true
        }, {
            xtype: "jitbizdelivery",
            fieldLabel: "配送方式",
            name: "DeliveyType",
            id: "cmbDeliveyType",
            listeners: {
                "change": fnDeliveryChanged
            },
            allowBlank: true
        }, {
            xtype: "jitbiztunit",
            fieldLabel: "配送商",
            readOnly: true,
            name: "carrier_name",
            id: "txtCarrierName",
            allowBlank: true
                , fieldCls: "txtDisable"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "收货地址",
            name: "Field4",
            id: "txtField4",
            width: 600,
            colspan: 3,
            allowBlank: true
        },
        {
            xtype: "jittextfield",
            fieldLabel: "配送时间",
            name: "Field9",
            readOnly: true,
            id: "txtField9",
            width:250,
            //readOnly: true,
            allowBlank: true
                , fieldCls: "txtDisable"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "配送单号",
            name: "Field2",
            readOnly: true,
            id: "txtField2",
         //   colspan: 3,
            hidden: false,
            allowBlank: true
             , fieldCls: "txtDisable"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "配送费",
            name: "FieldDeliveryAmount",
            readOnly: true,
            id: "txtFieldDeliveryAmount",
            colspan: 2,
            hidden: false,
            allowBlank: true
             , fieldCls: "txtDisable"

        },

        {
            xtype: "jittextfield",//修改成文本框（原jitdatefield）update by Henry 2014-11-11 
            fieldLabel: "提货时间",
            name: "SendTime",
            id: "txtSendTime",
            readOnly: true,
            width: 250,
            allowBlank: true,
            fieldCls: "txtDisable"
        },
        {
            xtype: "jittextfield",
            fieldLabel: "处理门店",
            name: "purchase_unit_name",
            id: "txtPurchaseUnitName",
            //colspan: 3,
            readOnly: true,
            allowBlank: true
             , fieldCls: "txtDisable"
        },
        {
            xtype: "jitbutton",
            text: "修&nbsp;&nbsp;改",
            id: 'btn_update1',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnSaveDeliveryInfo
        }
        ]
    });

    var z_sku_prop_cfg = Ext.create('jit.biz.SkuPropCfg', {});


    //支付信息
    Ext.create('Ext.form.Panel', {
        collapsible: true,
        collapsed: true,
        header: {
            xtype: "header",
            headerAsText: false,
            items: [{
                id: "lab_Pay",
                labelPad: 5,
                lableWidth: 10,
                xtype: 'jitdisplayfield',
                fieldStyle: "color:red;font-weight:bold;",
                width: '200',
                fieldLabel: '<b>支付信息</b>',
                value: ''
            }],
            cls: "headerOver",
            listeners: {  //折叠展开事件
                "click": function () {
                    if (!Ext.getCmp("payPanel").collapsed)//不是折叠状态
                        Ext.getCmp("payPanel").collapse(Ext.Component.DIRECTION_TOP, true);//向上折叠
                    else
                        Ext.getCmp("payPanel").expand(true);
                }
            },
            layout: "hbox"
        },
        renderTo: "divMain",
        id: "payPanel",
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
            fieldLabel: "订单金额",
            name: "total_amount",
            id: "txtTotalAmount",
            value: 0,
            readOnly: true,
            allowBlank: true,
            width: 200
            //,
            //fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }, {
            xtype: "jittextfield",
            fieldLabel: "购买数量",
            name: "total_num",
            id: "txtTotalNum",
            value: 0,
            readOnly: true,
            allowBlank: true
            //,
            //fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }
        , {
            xtype: "jittextfield",
            fieldLabel: "支付方式",
            width: 200,
            id: "txtPayment_name",
            readOnly: true,
            allowBlank: true
            //,
            //fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }, {
            xtype: "jittextfield",
            fieldLabel: "发票抬头",
            id: 'txtinvoice',
          //  fieldStyle: "text-align:right;",
            readOnly: true,
            allowBlank: true
                , fieldCls: "txtDisable"
        }
      

        , {
            xtype: "jitbizdefraytype",
            fieldLabel: "支付方式",
            hidden: true,   //隐藏的
            name: "DefrayType",
            id: "cmbDefrayType",
            readOnly: true,
            fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }, {
            xtype: "jittextfield",
            fieldLabel: "实付金额",
            name: "actual_amount",
            id: "txtActualAmount",
            value: 0,
            hidden: true, //隐藏
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }, {
            xtype: "jittextfield",
            fieldLabel: "找零",
            name: "keep_the_change",
            id: "tbKeepTheChange",
            value: 0,
            hidden: true,//隐藏
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }, {
            xtype: "jittextfield",
            fieldLabel: "抹零",   
            name: "wiping_zero",
            id: "tbWipingZero",
            value: 0,
            hidden: true,//隐藏
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
                , fieldCls: "txtDisable"
        }
         //第二行
          , {
              columnWidth: 1,
              layout: 'column',
              hidden:true,
              border: 0,
              items:
                  [{
                      xtype: "jittextfield",
                      fieldLabel: "折扣",
                      name: "discount_rate",
                      id: "tbDiscountRate",
                      value: 0,
                      margin: "0 0 10 10",
                      readOnly: true,
                      allowBlank: true,
                      fieldStyle: "text-align:right;"
                  }, {
                      xtype: 'label',
                      forId: 'lblRate',
                      text: "%"
                  }]
          }//折扣这一块
        , {
            hidden: true,
            xtype: "jittextfield",
            fieldLabel: "应付金额",
            name: "total_retail",
            id: "txtTotalRetail",
            readOnly: true,
            allowBlank: true,
            fieldStyle: "text-align:right;"
        }
       
        
        , {
            name: "wholeOrder",
            id: "lab_wholeOrder",
    labelPad: 5,
    lableWidth: 10,
    colspan:4,  //合并四列
    xtype: 'jittextfield',
    fieldStyle: "color:red;",//font-weight:bold;
    width: 600,
    fieldLabel: ' ',//<b>整体：</b>
    readOnly: true,
    allowBlank: true, fieldCls: "txtDisable"
   // value: ''
}


        , {
            xtype: "jitbutton",
            text: "修&nbsp;&nbsp;改",
            id: 'btn_updatePay',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: true,
            handler: fnSaveDefrayType
        }
        ]
    });
    //会员信息   
    //    var msgGrid = Ext.create('Ext.grid.Panel', {
    //        store: Ext.getStore("WUserMessageEntity"),
    //        id: "gridViewMessage",
    //        //renderTo: "divvipMessage",
    //        columnLines: true,
    //        stripeRows: true,
    //        height: 200,
    //        width: "100%",
    //        selModel: Ext.create('Ext.selection.CheckboxModel', {
    //            mode: 'MULTI'
    //        }),
    //        bbar: new Ext.PagingToolbar({
    //            displayInfo: true,
    //            id: "pageBar2",
    //            defaultType: 'button',
    //            store: Ext.getStore("WUserMessageEntity"),
    //            pageSize:5
    //        }),
    //        listeners: {
    //            render: function (p) {
    //                p.setLoading({
    //                    store: p.getStore()
    //                }).hide();
    //            }
    //        },
    //        columns: [
    //        { 
    //            xtype: 'jitcolumn',
    //            jitDataType: 'Time',
    //            text: "提交时间",
    //            dataIndex: 'CreateTime',
    //            width: 90,
    //            menuDisabled: true,

    //            flex: 1
    //        },
    //        {
    //            xtype: 'jitcolumn',
    //            jitDataType: 'String',
    //            text: "留言内容",
    //            dataIndex: 'MessageText',
    //            width: 390,
    //            menuDisabled: true,
    //            flex: 1
    //        }
    //        ]
    //    });
    Ext.create('Ext.panel.Panel', {
        //title: null,
        collapsible: true,
        collapsed: true,
        id: "vipPanel",
        renderTo: "divvipMessage",
        header: {
            xtype: "header",
            headerAsText: false,
            margin: "-1",
            items: [{
                id: "lab_vip",
                labelPad: 5,
                lableWidth: 10,
                fieldStyle: "color:red;font-weight:bold;",
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>会员信息</b>',
                value: ''
            }],
            cls: "headerOver",
            listeners: {  //折叠展开事件
                "click": function () {
                    if (!Ext.getCmp("vipPanel").collapsed)//不是折叠状态
                        Ext.getCmp("vipPanel").collapse(Ext.Component.DIRECTION_TOP, true);//向上折叠
                    else
                        Ext.getCmp("vipPanel").expand(true);
                }
            },
            layout: "hbox"
        },
        renderTo: "divvipMessage",
        //id: "editPanel2",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;padding-bottom:10px;border-bottom:0px;',

        defaults: {},

        items: [{
            xtype: "container",
            width: "100%",
            layout: {
                type: 'table'
            , columns: 4
            , align: 'right'
            },
            items: [{
                xtype: "jittextfield",
                fieldLabel: "会员号",
                name: "vip_code",
                id: "txtVipCode",
                readOnly: true,
                allowBlank: true
                 , fieldCls: "txtDisable"
            }, {
                xtype: "jittextfield",
                fieldLabel: "会员",
                name: "vip_name",
                id: "txtVipName",
                value: 0,
                readOnly: true,
                allowBlank: true
                    , fieldCls: "txtDisable"
            },
                {
                    xtype: "jittextfield",
                    fieldLabel: "会员手机",
                    name: "vip_phone",
                    id: "txtVipPhone",
                    value: 0,
                    readOnly: true,
                    allowBlank: true
                        , fieldCls: "txtDisable"
                }
                //{
                //    xtype: "jittextfield",
                //    fieldLabel: "微信",
                //    name: "vip_weixin",
                //    id: "txtVipweixin",
                //    value: 0,
                //    readOnly: true,
                //    allowBlank: true
                //        , fieldCls: "txtDisable"
                //},
                //{
                //    xtype: "jittextfield",
                //    fieldLabel: "微博",
                //    name: "vip_wb",
                //    id: "txtVipwb",
                //    value: 0,
                //    readOnly: true,
                //    allowBlank: true
                //        , fieldCls: "txtDisable"
                //},
                //{
                //    xtype: "jittextfield",
                //    fieldLabel: "积分",
                //    name: "vip_integration",
                //    id: "txtVipintegration",
                //    value: 0,
                //    readOnly: true,
                //    allowBlank: true
                //        , fieldCls: "txtDisable"
                //},
                //{
                //    xtype: "jittextfield",
                //    fieldLabel: "消费总额",
                //    name: "vip_TotalSum",
                //    id: "txtTotalSum",
                //    colspan: 3,
                //    value: 0,
                //    readOnly: true,
                //    allowBlank: true
                //        , fieldCls: "txtDisable"
                //},
                //{
                //    xtype: "jitdisplayfield",
                //    fieldLabel: "标签",
                //    name: "vip_Tags",
                //    id: "labTags",
                //    colspan: 4,
                //    value: 0,
                //    readOnly: true,
                //    allowBlank: true
                //        , fieldCls: "txtDisable"
                //}
        ]
        }
        //        ,
        //        {
        //            xtype: "container",
        //            colspan: 4,
        //            width:"100%",
        //            items: [msgGrid]
        //        }
        ]
    });

    //商品明细
    var orderItemGrid = Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("salesOutOrderEditItemStore"),
        id: "gridView",
        //renderTo: "grid",
        columnLines: true,
        stripeRows: true,
        height: 284,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'   //选取多个
        }),
        bbar: new Ext.PagingToolbar({  //分页控件
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

    Ext.create('Ext.panel.Panel', {
        //title: null,
        collapsible: true,
        collapsed: false,
      //  renderTo: "grid",
        id:"ItemDetail",
        header: {
            xtype: "header",
            headerAsText: false,
            margin: "-1",
            items: [{
                id: "lab_OrderItem",
                labelPad: 5,
                lableWidth: 10,
                fieldStyle: "color:red;font-weight:bold;",
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>商品信息</b>',
                value: ''
            }],
            cls: "headerOver",
            listeners: {  //折叠展开事件
                "click": function () {
                    if (!Ext.getCmp("ItemDetail").collapsed)//不是折叠状态
                        Ext.getCmp("ItemDetail").collapse(Ext.Component.DIRECTION_TOP, true);//向上折叠
                    else
                        Ext.getCmp("ItemDetail").expand(true);
                }
            },
            layout: "hbox"
        },
        renderTo: "divDetail1",
        //id: "editPanel2",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;padding-bottom:10px;border-bottom:0px;',
        items: [
        {
            xtype: "container",
            colspan: 3,
            items: [orderItemGrid]
        }]
    });

    // jifeng.cao begin
    Ext.create('Ext.form.Panel', {
        id: "operationPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jitbiztunit",
            fieldLabel: "配送公司",
            id: "DeliverCompany",
            name: "DeliverCompany",
            hidden: true
        }, {
            xtype: "jittextfield",
            fieldLabel: "配送单号",
            id: "DeliverOrder",
            name: "DeliverOrder",
            hidden: true
             , fieldCls: "txtDisable"
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "付款方式",
            id: "PayMethod",
            name: "PayMethod",
            OptionName: 'PayMethod',
            hidden: true
        },
        //        {
        //            xtype: "jitbizoptions",
        //            fieldLabel: "审核状态",
        //            id: "CheckStatus",
        //            name: "CheckStatus",
        //            OptionName: 'CheckStatus',
        //            listeners: {
        //                "change": fnCheckStatusChange
        //            },
        //            hidden: true
        //        }, 
        {
        xtype: "jitbizoptions",
        fieldLabel: "未通过理由",
        OptionName: 'CheckResult',
        name: "CheckResult",
        id: "CheckResult",
        hidden: true
    }, {
        xtype: 'filefield',
        name: 'PicUrl',
        width: 258,
        labelWidth: 82,
        height: 22,
        labelSeparator: '',
        id: 'PicUrl',
        labelPad: 11,
        labelAlign: 'right',
        buttonText: '选择图片',
        fieldLabel: '图片链接',
        hidden: true
    }, {
        xtype: "jittextarea",
        fieldLabel: "备注",
        name: "Remark",
        id: "Remark",
        maxLength: 300
    }]

});

Ext.create('Jit.window.Window', {
    height: 350,
    id: "operationWin",
    title: '订单操作',
    jitSize: 'big',
    layout: 'fit',
    draggable: true,
    items: [Ext.getCmp("operationPanel")],
    border: 0,
    modal: false,
    buttons: ['->', {
        xtype: "jitbutton",
        id: "btnSave",
        handler: fnOperationSubmit,
        isImgFirst: true,
        imgName: "save"
    }, {
        xtype: "jitbutton",
        handler: fnOperationCancel,
        imgName: "cancel"
    }],
    closeAction: 'hide'
});



var logGrid = Ext.create("Ext.grid.Panel", {
    id: "logGrid",
    store: Ext.getStore("inoutStatusStore"),
    columnLines: true,
    height: 320,
    stripeRows: true,
    width: "100%",
    forceFit: true,
    columns: [{
        width: 25,
        text: "日期",
        sortable: true,
        dataIndex: "LastUpdateTimeFormat",
        align: 'left'
    }, {
        //            width: 50,
        text: "描述",
        sortable: true,
        dataIndex: "StatusRemark",
        renderer: fnColumnStatusRemark,
        align: 'left'
    }
    //        , {
    //            width: 50,
    //            text: "订单状态",
    //            sortable: true,
    //            dataIndex: "OrderStatusName",
    //            align: 'left'
    //        }, {
    //            width: 50,
    //            text: "未审核理由",
    //            sortable: true,
    //            dataIndex: "CheckResultName",
    //            align: 'left'
    //        }, {
    //            width: 50,
    //            text: "支付方式",
    //            sortable: true,
    //            dataIndex: "PayMethodName",
    //            align: 'left'
    //        }, {
    //            width: 50,
    //            text: "配送公司",
    //            sortable: true,
    //            dataIndex: "unit_name",
    //            align: 'left'
    //        }, {
    //            width: 50,
    //            text: "配送单号",
    //            sortable: true,
    //            dataIndex: "DeliverOrder",
    //            align: 'left'
    //        }, {
    //            width: 50,
    //            text: "图片",
    //            sortable: true,
    //            dataIndex: "PicUrl",
    //            renderer: fnColumnPhoto,
    //            align: 'center'
    //        }, {
    //            xtype: "jitcolumn",
    //            jitDataType: 'tips',
    //            width: 50,
    //            text: "备注",
    //            sortable: true,
    //            dataIndex: "Remark",
    //            align: 'left'
    //        }
        ],
    bbar: new Ext.PagingToolbar({
        displayInfo: true,
        id: "pageBar1",
        defaultType: 'button',
        store: Ext.getStore("inoutStatusStore"),
        pageSize: JITPage.PageSize.getValue()
    }),
    listeners: {
        render: function (p) {
            p.setLoading({
                msg: JITPage.Msg.GetData,
                store: p.getStore()
            }).hide();
        },
        'afterlayout': function () {
            setImg(this);
        }
    }
});

Ext.create('Ext.form.Panel', {
    id: "logPanel",
    width: "100%",
    height: "100%",
    border: 1,
    bodyStyle: 'background:#F1F2F5;',
    layout: 'anchor',
    defaults: {},
    items: [logGrid]
});

Ext.create('Jit.window.Window', {
    height: 350,
    id: "logWin",
    title: '日志详情',
    jitSize: 'big',
    layout: 'fit',
    draggable: true,
    items: [Ext.getCmp("logPanel")],
    border: 0,
    modal: false,
    closeAction: 'hide'
});

// jifeng.cao end


}