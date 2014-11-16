function InitEditView() {
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: 'divOperation',
        items: [{
            xtype: "jitbutton",
            text: "审&nbsp;&nbsp;核",
            id: 'btn_1',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnbtn_1
        }, {
            xtype: "jitbutton",
            text: "关&nbsp;&nbsp;闭",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: function () {
                CloseWin('Asus_OrdersDetail');
            }
        }],
        margin: '10 0 10 0',
        layout: 'column',
        border: 0
    });

    //订单主信息
    Ext.create('Ext.form.Panel', {
        collapsible: true,
        header: {
            xtype: "header",
            headerAsText: false,
            items: [{
                id: "lab_Status",
                labelPad: 5,
                lableWidth: 10,
                xtype: 'jitdisplayfield',
                fieldLabel: '<b>订单信息</b>',
                value: ''
            }],
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
                type: 'table',
                columns: 4,
                align: 'right'
            },
            items: [{
                xtype: "jittextfield",
                fieldLabel: "单据号码",
                name: "order_no",
                id: "txtOrderNo",
                readOnly: true,
                allowBlank: false
            }, {
                xtype: "jittextfield",
                fieldLabel: "单据日期",
                name: "order_date",
                id: "txt_order_date",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "产品型号",
                name: "model",
                id: "txt_model",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "产品序列号",
                name: "SerCode",
                id: "txt_SerCode",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "产品价格",
                name: "Price",
                id: "txt_Price",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "购买方式",
                name: "BuyWay",
                id: "txt_BuyWay",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "购买日期",
                name: "BuyDate",
                id: "txt_BuyDate",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "客户姓名",
                name: "UserName",
                id: "txt_UserName",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "客户电话",
                name: "Phone",
                id: "txt_Phone",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "客户邮箱",
                name: "Email",
                id: "txt_Email",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "获取方式",
                name: "GetWay",
                id: "txt_GetWay",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "订单状态",
                name: "orderStatus",
                id: "txt_orderStatus",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "校园大使",
                name: "vipName",
                id: "txt_vipName",
                readOnly: true,
                allowBlank: true
            }, {    
                xtype: "jittextfield",
                fieldLabel: "学校",
                name: "school",
                id: "txt_school",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "专业",
                name: "specialt",
                id: "txt_specialt",
                readOnly: true,
                allowBlank: true
            }, {
                xtype: "jittextfield",
                fieldLabel: "购买前意向",
                name: "intent",
                id: "txt_intent",
                readOnly: true,
                allowBlank: true
            }]
        }]
    });

    Ext.create('Ext.form.Panel', {
        id: "operationPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jitbizoptions",
            fieldLabel: "审核状态",
            id: "CheckStatus",
            name: "CheckStatus",
            OptionName: 'CheckStatus',
            listeners: {
                "change": fnCheckStatusChange
            }
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "未通过理由",
            OptionName: 'CheckResult',
            name: "CheckResult",
            id: "CheckResult",
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
            handler: function () {
                Ext.getCmp("operationWin").hide();
            },
            imgName: "cancel"
        }],
        closeAction: 'hide'
    });
}