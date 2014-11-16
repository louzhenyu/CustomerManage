var OrdersViewColumns =[{
    text: '操作',
    width: JITPage.Layout.OperateWidth,
    sortable: true,
    dataIndex: 'Status',
    align: 'left',
    renderer: fnColumnUpdate
}, {
    text: '订单号',
    width: 80,
    sortable: true,
    dataIndex: 'OrdersNo',
    align: 'left'
}, {
    text: '下单日期',
    width: 80,
    sortable: true,
    xtype: "jitcolumn",
    jitDataType: "date",
    dataIndex: 'OrdersDate'
}, {
    text: '付款方式',
    width: 110,
    sortable: true,
    dataIndex: 'Payment',
    align: 'left',
    renderer: fnShowPayment
}, {
    text: '状态',
    width: 80,
    sortable: true,
    dataIndex: 'OrdersStatusText',
    align: 'left'
}, {
    text: '门店',
    width: 135,
    sortable: true,
    dataIndex: 'StoreName',
    xtype: "jitcolumn",
    jitDataType: 'Tips'
}, {
    text: '房型名称',
    width: 138,
    sortable: true,
    dataIndex: 'RoomTypeName',
    xtype: "jitcolumn",
    jitDataType: 'Tips'
}, {
    text: '客人名称',
    width: 110,
    sortable: true,
    dataIndex: 'GuestName',
    align: 'left'
}, {
    text: '入住日期',
    width: 80,
    sortable: true,
    xtype: "jitcolumn",
    jitDataType: "date",
    dataIndex: 'StartDate'
}, {
    text: '离店日期',
    width: 80,
    sortable: true,
    xtype: "jitcolumn",
    jitDataType: "date",
    dataIndex: 'EndDate'
}, {
    text: '备注',
    width: 110,
    sortable: true,
    dataIndex: 'Remark',
    renderer: fnColumnRemark
}, {
    text: '房间数',
    width: 60,
    sortable: true,
    xtype: "jitcolumn",
    jitDataType: "int",
    dataIndex: 'RoomCount'
}]

function InitView() {
    /*创建查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "txtOrdersNo",
            name: "OrdersNo",
            fieldLabel: "订单号",
            jitSize: 'small'
        }, {
            xtype: "jitdatefield",
            fieldLabel: "入住日期",
            id: "dtStartDate",
            name: "StartDate",
            jitSize: 'small'
        }, {
            xtype: "jitdatefield",
            fieldLabel: "离店日期",
            id: "dtEndDate",
            name: "EndDate",
            jitSize: 'small',
            vtype: "enddate",
            begindateField: "dtStartDate"
        }, {
            xtype: "jittextfield",
            id: "txtStoreName",
            name: "StoreName",
            fieldLabel: "门店",
            jitSize: 'small'
        }, {
            xtype: "jittextfield",
            id: "txtRoomType",
            name: "RoomType",
            fieldLabel: "房型",
            jitSize: 'small'
        }, {
            xtype: "jittextfield",
            id: "txtGuestName",
            name: "GuestName",
            fieldLabel: "客人名称",
            jitSize: 'small'
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
            handler: fnSearch
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

    Ext.create('Jit.button.Button', {
        text: "导出",
        renderTo: "btn_export",
        handler: fnExport
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
    });

    /*查询列表区域*/
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("ordersStore"),
        id: "gridView",
        columnLines: true,
        columns: OrdersViewColumns,
        height: 400,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("ordersStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView",
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });

    /*备注编辑表单面板*/
    Ext.create('Ext.form.Panel', {
        id: "editRemarkPanel",
        autoScroll: true,
        columnWidth: 200,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'anchor',
        items: [{
            columnWidth: 1,
            layout: "column",
            border: 0,
            items: [{
                xtype: "jittextarea",
                fieldLabel: "<font color='red'>*</font>备注",
                width: 340,
                height: 140,
                id: "txtRemark",
                name: "Remark",
                maxLength: 500,
                maxLengthText: "不能大于500个字符"
            }]
        }]
    });

    /*备注Window*/
    Ext.create('Ext.window.Window', {
        height: 300,
        id: "editRemarkWin",
        title: '编辑备注',
        width: 400,
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("editRemarkPanel")],
        border: 0,
        buttons: ['->', {
            xtype: "jitbutton",
            imgName: 'save',
            isImgFirst: true,
            id: "btnAuditSave",
            handler: fnSubmitRemark
        }, {
            xtype: "jitbutton",
            imgName: 'cancel',
            isImgFirst: false,
            handler: function () { Ext.getCmp("editRemarkWin").hide(); }
        }],
        closeAction: 'hide'
    });

    /*订单审核表单面板*/
    Ext.create('Ext.form.Panel', {
        id: "approvePanel",
        autoScroll: true,
        columnWidth: 200,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'anchor',
        items: [{
            xtype: "jitdisplayfield",
            fieldLabel: "订单号",
            name: "OrdersNo",
            id: "txt_OrdersNo",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "客人名称",
            name: "GuestName",
            id: "txt_GuestName",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "联系电话",
            name: "LinkTel",
            id: "txt_LinkTel",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "住宿日期",
            name: "OrdersDate",
            id: "txt_OrdersDate",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "房间数",
            name: "RoomCount",
            id: "txt_RoomCount",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "预定客房",
            name: "RoomTypeName",
            id: "txt_RoomTypeName",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "优惠劵抵扣金额",
            name: "couponAmount",
            id: "txt_couponAmount"
            , width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "积分抵扣金额",
            name: "integral",
            id: "txt_integral"
            , width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "余额抵扣金额",
            name: "vipEndAmount",
            id: "txt_vipEndAmount"
            , width: 450

        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "总计金额",
            name: "totalamount",
            id: "txt_totalamount"
            , width: 450

        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "付款方式",
            name: "Payment",
            id: "txt_Payment",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "备注",
            name: "Remark",
            id: "txt_hdRemark",
            width: 450
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "操作明细",
            name: "OperationDetail",
            id: "txt_OperationDetail",
            width: 460
        }]
    });

    /*订单操作Window*/
    Ext.create('Jit.window.Window', {
        height: 450,
        width: 500,
        title: '',
        jitSize: "large",
        id: "approveWin",
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("approvePanel")],
        buttons: ['->', {
            xtype: "jitbutton",
            id: "btnSave",
            text: '通过',
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: fnAuditOrders
        }, {
            xtype: "jitbutton",
            id: "btnCancel",
            text: '不通过',
            jitIsHighlight: false,
            jitIsDefaultCSS: false,
            handler: fnShowNotAuditWin
        }, {
            xtype: "jitbutton",
            id: "btnComplete",
            text: '完成',
            hidden: true,
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: fnComplete
        }, {
            xtype: "jitbutton",
            id: "btnCancelOrder",
            text: '取消订单',
            jitIsHighlight: false,
            jitIsDefaultCSS: false,
            handler: fnCancelOrders
        }, {
            xtype: "jitbutton",
            text: '打印',
            isImgFirst: false,
            handler: fnPrint
        }, {
            xtype: "jitbutton",
            id: "btnClose",
            text: '关闭',
            jitIsHighlight: false,
            jitIsDefaultCSS: false,
            handler: function () { Ext.getCmp("approveWin").hide(); }
        }],
        border: 0,
        closeAction: 'hide'
    });

    /*审批不通过表单面板*/
    Ext.create('Ext.form.Panel', {
        id: 'notAuditPanel',
        autoScroll: true,
        columnWidth: 200,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        items: [{
            xtype: 'radiogroup',
            fieldLabel: '不通过原因',
            columns: 1,
            id: 'rdReason',
            labelStyle: 'padding-left:10px',
            labelWidth: 80,
            vertical: true,
            items: [{
                boxLabel: '满房封订单首日（例：订单预订日期为4/8-4/10号，封首日则代表仅4/8号一天满房。 ）',
                name: 'rb',
                id: 'rb1',
                inputValue: '1'
            }, {
                boxLabel: '满房封订单入住日（例：订单预订日期为4/8-4/10号，封入住日则代表4/8号-4/10三天有一天或均满房。）',
                name: 'rb',
                id: 'rb2',
                inputValue: '2'
            }, {
                boxLabel: '其他',
                name: 'rb',
                id: 'rb3',
                inputValue: '3'
            }],
            onChange: function (a, b) {
                if (a.rb == "3") {
                    Ext.getCmp("txt_Remark").show();
                } else {
                    Ext.getCmp("txt_Remark").hide();
                    Ext.getCmp("txt_Remark").jitSetValue('');
                }
            }
        }, {
            xtype: "jittextarea",
            fieldLabel: "<font color='red'>*</font>备注",
            width: 460,
            height: 100,
            id: "txt_Remark",
            name: "Remark",
            maxLength: 500,
            hidden: true,
            maxLengthText: "不能大于500个字符"
        }]
    });

    /*审批不通过Window*/
    Ext.create('Jit.window.Window', {
        height: 300,
        width: 500,
        title: '不通过审核',
        jitSize: 'large',
        id: 'notAuditWin',
        layout: 'fit',
        items: [Ext.getCmp("notAuditPanel")],
        buttons: ['->', {
            xtype: "jitbutton",
            imgName: 'save',
            isImgFirst: true,
            id: "btnNotAuditSave",
            handler: fnNotAudit
        }, {
            xtype: "jitbutton",
            imgName: 'cancel',
            isImgFirst: false,
            handler: function () { Ext.getCmp("notAuditWin").hide(); }
        }],
        border: 0,
        closeAction: 'hide'
    });
}