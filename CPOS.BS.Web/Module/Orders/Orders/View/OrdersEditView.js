function InitEditView() {
    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        autoScroll: true,
        columnWidth: 200,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'anchor',
        items: [{
            xtype: "jitdisplayfield",
            fieldLabel: "订单号",
            name: "OrdersNo",
            id: "lblOrdersNo"
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "客人名称",
            name: "GuestName",
            id: "lblGuestName"
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "联系电话",
            name: "LinkTel",
            id: "lblLinkTel"
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "住宿日期",
            name: "OrdersDate",
            id: "lblOrdersDate"
        }, {
            xtype: "jitdisplayfield",
            fieldLabel: "预定客房",
            name: "RoomTypeName",
            id: "lblRoomTypeName"
        },
        {
            xtype: "jitdisplayfield",
            fieldLabel: "付款方式",
            name: "Payment",
            id: "lblPayment"
        }]
    });
}