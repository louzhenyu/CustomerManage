function InitView() {

    Ext.create('Jit.form.field.Text', {
        id: "txtItemCategoryCode",
        text: "",
        renderTo: "txtItemCategoryCode",
        width: 150
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtItemCategoryName",
        text: "",
        renderTo: "txtItemCategoryName",
        width: 150
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtPyzjm",
        text: "",
        renderTo: "txtPyzjm",
        width: 150
    });

    Ext.create('jit.biz.Status', {
        id: "txtStatus",
        text: "",
        renderTo: "txtStatus",
        width: 150
    });

    Ext.create('Jit.Biz.ItemCategorySelectTree', {
        id: "txtParent",
        text: "",
        renderTo: "txtParent",
        setRootAsDefault: true,
        width: 150
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        value: "0",
        renderTo: "txtDisplayIndex",
        width: 150,
        allowDecimals: true,
        decimalPrecision: 0
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        readOnly: true,
        width: 405
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
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });

}