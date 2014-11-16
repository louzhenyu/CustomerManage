function InitView() {

    //editPanel area

    Ext.create('Jit.form.field.Text', {
        id: "TicketName",
        text: "",
        renderTo: "txtTicketName",
        width: 405,
        allowBlank: false
    });
    Ext.create('Jit.Biz.LEventSelectTree', {
        id: "EventID",
        text: "",
        renderTo: "txtEventID",
        width: 405
    });
    Ext.create('Jit.form.field.Number', {
        id: "TicketPrice",
        value: "0",
        renderTo: "txtTicketPrice",
        width: 100,
        allowBlank: false
    });
    Ext.create('Jit.form.field.Number', {
        id: "TicketNum",
        value: "0",
        renderTo: "txtTicketNum",
        width: 100,
        allowBlank: false
    });
    Ext.create('Jit.form.field.Number', {
        id: "TicketSort",
        value: "0",
        renderTo: "txtTicketSort",
        width: 100,
        allowBlank: false
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "TicketRemark",
        text: "",
        renderTo: "txtTicketRemark",
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