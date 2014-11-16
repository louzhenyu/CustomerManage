function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtOptionDesc",
        text: "",
        renderTo: "txtOptionDesc",
        width: 100
    });
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsSelect",
        text: "",
        renderTo: "txtIsSelect",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
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