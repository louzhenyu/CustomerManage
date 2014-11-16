function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        maxLength: 4,
        enforceMaxLength: true,
        value: 1,
        maxValue: 9999,
        minValue: 1,
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtTitle",
        text: "",
        renderTo: "txtTitle",
        maxLength: 100,
        enforceMaxLength: true,
        width: 375
    });
    //    Ext.create('Jit.form.field.Text', {
    //        id: "txtImageURL",
    //        text: "",
    //        renderTo: "txtImageURL",
    //        maxLength: 200,
    //        enforceMaxLength: true,
    //        width: 375
    //  });

    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 0,
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
            handler: fnSave,
            jitIsHighlight: true,
            jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}