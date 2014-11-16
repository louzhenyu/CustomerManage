function InitView() {

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