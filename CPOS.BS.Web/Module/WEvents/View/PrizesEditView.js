function InitView() {


    /*修改*/
    Ext.create('Jit.form.field.ComboBox', {
        id: "txtPrizeType",
        renderTo: "txtPrizeType",
        valueField: "Id",
        displayField: "Name",
        store: Ext.getStore("PrizeTypeStore"),
        width: 100,
        listeners: {
            change: fnselectPrizeType
        }

    });

    Ext.create('Jit.form.field.ComboBox', {
        id: "txtCouponType",
        renderTo: "txtCouponType",
        valueField: "CouponTypeID",
        displayField: "CouponTypeName",
        store: Ext.getStore("CouponTypeStore"),
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtPoint",
        text: "",
        renderTo: "txtPoint",
        width: 100,
        value: "0"
    });

    Ext.create('Ext.form.field.Number', {
        id: "txtMoney",
        renderTo: "txtMoney",
        width: 100,
        minValue: 0,
        hideTrigger: true,
        keyNavEnabled: false,
        mouseWheelEnabled: false,
        value:'0'



    });

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtPrizeName",
        text: "",
        renderTo: "txtPrizeName",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtPrizeShortDesc",
        text: "",
        renderTo: "txtPrizeShortDesc",
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtPrizeDesc",
        text: "",
        renderTo: "txtPrizeDesc",
        margin: "0 0 10 10",
        width: 330
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtLogoURL",
        text: "",
        renderTo: "txtLogoURL",
        width: 330
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        width: 330
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtContentUrl",
        text: "",
        renderTo: "txtContentUrl",
        width: 330
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtContentText",
        text: "",
        renderTo: "txtContentText",
        margin: "0 0 10 10",
        width: 330
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtPrice",
        value: "0",
        renderTo: "txtPrice",
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        value: "1",
        renderTo: "txtDisplayIndex",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtCountTotal",
        value: "0",
        renderTo: "txtCountTotal",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtCountLeft",
        value: "0",
        renderTo: "txtCountLeft",
        width: 100
    });


    Ext.create('Jit.form.field.Checkbox', {
        id: "IsAutoPrizes",
        text: "",
        renderTo: "txtIsAutoPrizes",
        width: 10,
        value: false
    });

    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            text: "保存",
            formBind: true,
            disabled: true,
            hidden: false,
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}