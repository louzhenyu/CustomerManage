function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtLicensePlateNo",
        text: "",
        renderTo: "txtLicensePlateNo",
        width: 150,
        allowBlank: false
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtChassisNumber",
        text: "",
        renderTo: "txtChassisNumber",
        width: 150
    });
    Ext.create('jit.biz.CarBrand', {
        id: "txtCarBrand",
        text: "",
        renderTo: "txtCarBrand",
        width: 150,
        allowBlank: false
    });
    Ext.create('jit.biz.CarModels', {
        id: "txtCarModels",
        text: "",
        renderTo: "txtCarModels",
        width: 150,
        parent_id: "txtCarBrand",
        allowBlank: false
    });
    Ext.create('jit.biz.CompartmentsForm', {
        id: "txtCompartmentsForm",
        text: "",
        renderTo: "txtCompartmentsForm",
        width: 150
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtPurchaseTime",
        text: "",
        renderTo: "txtPurchaseTime",
        width: 150
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 472,
        height: 180
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