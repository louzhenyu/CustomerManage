function InitView() {
    /*创建查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            fieldLabel: "支付类型",
            id: "paymentTypeName",
            name: "Payment_Type_Name",
            jitSize: 'small',
            isDefault: true
        }, {
            xtype: "jittextfield",
            fieldLabel: "支付类型编码",
            id: "paymentTypeCode",
            name: "Payment_Type_Code",
            jitSize: 'small',
            isDefault: true
        }],
        renderTo: 'search_form_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    /*查询按钮区域*/
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        items: [{
            xtype: "jitbutton",
            height: 22,
            isImgFirst: true,
            text: "查询",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: __getHidden("search"),
            handler: function () { fnSearch(); }
        }, {
            xtype: "jitbutton",
            height: 22,
            text: "重置",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: function () {
                Ext.getCmp("searchPanel").getForm().reset();
            }
        }],
        renderTo: 'search_button_panel',
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore('paymentStore'),
        id: 'gridView',
        renderTo: 'DivGridView',
        columnLines: true,
        height: 420,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("paymentStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        columns: [
        {
            text: '用户账号操作',
            width: 180,
            sortable: true,
            dataIndex: 'PaymentTypeID',
            align: 'left',
            renderer: function (value, p, record) {
                var code = record.data.PaymentTypeCode;
                var ChannelId = record.data.ChannelId;
                var str = "";
                var name = record.data.PaymentTypeName;
                if (record.data.IsCustom == "false") {
                    str += "<div><div style=float:left><a >" + name + "</a></div>";
                }
                else if (record.data.IsCustom == "true") {
                    str += "<div ><a class=\"z_op_link\" href=\"#\" onclick=\"fnSetView('" + value + "','" + code + "',false)\">" + name + "</a></div>";
                }
                return str;
            }
        }
        , {
            text: '支付编码',
            width: 120,
            sortable: true,
            dataIndex: 'PaymentTypeCode',
            align: 'left'

        }
        , {
            text: '操作',
            columns: [
            {
                width: 180
           , sortable: true
           , dataIndex: 'IsDefault'
           , align: 'center'
           , renderer: function (value, p, record) {
               var code = record.data.PaymentTypeCode;
               var ChannelId = record.data.ChannelId;
               var PaymentTypeID = record.data.PaymentTypeID;
               var str = "";
               if (value == "true") {

                   str = "<div   class= CloseBut><a   href=\"#\">启用阿拉丁支付</a> </div>";
               }
               else {
                   str = "<div class=OpenBut><a  href=\"#\" onclick=\"fnDefault('" + PaymentTypeID + "','" + code + "','" + ChannelId + "')\">启用阿拉丁支付</a> </div>";

               }
               if (record.data.PaymentTypeCode == "WXJS") {//因为这个功能没有，所以特殊对待
                   str = "<div class= WXJSBut ><a   href=\"#\">启用阿拉丁支付</a> </div>";
               }
               return str;
           }
            }
           , {
               width: 180,
               sortable: true,
               dataIndex: 'IsCustom',
               align: 'center'
               , renderer: function (value, p, record) {
                   var code = record.data.PaymentTypeCode;
                   var ChannelId = record.data.ChannelId;
                   var PaymentTypeID = record.data.PaymentTypeID;
                   var str = "";
                   if (value == "true") {
                       str = "<div class= CloseBut><a   href=\"#\">启用商家支付</a> </div>";
                   }
                   else {
                       str = "<div class=OpenBut><a  href=\"#\" onclick=\"fnSetView('" + PaymentTypeID + "','" + code + "',true)\">启用商家支付</a> </div>";
                   }
                   return str;
               }
           }
           , {
               width: 180
            , align: 'center'
            , dataIndex: 'IsOpen'
            , renderer: function (value, p, record) {
                var PaymentTypeID = record.data.PaymentTypeID;
                var str = "";
                if (value == "true") {
                    str = "<div class=OpenBut><a  href=\"#\" onclick=\"fnDisble('" + PaymentTypeID + "')\">停用</a> </div>";
                }
                else {
                    str = "<div class= CloseBut><a   href=\"#\">停用</a> </div>";
                }
                return str;
            }
           }
            ]


        }
        ],
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });
}