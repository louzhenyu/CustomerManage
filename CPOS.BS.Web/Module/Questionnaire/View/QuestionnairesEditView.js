function InitView() {

    //editPanel area
    //Ext.create('jit.biz.EventType', {
    //    id: "txtEventType",
    //    text: "",
    //    renderTo: "txtEventType",
    //    width: 100
    //});

    //Ext.create('Jit.form.field.Date', {
    //    id: "txtStartDate",
    //    text: "",
    //    renderTo: "txtStartDate",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Date', {
    //    id: "txtEndDate",
    //    text: "",
    //    renderTo: "txtEndDate",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtStartTime",
    //    text: "",
    //    value: "08:00",
    //    renderTo: "txtStartTime",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtEndTime",
    //    text: "",
    //    value: "16:00",
    //    renderTo: "txtEndTime",
    //    width: 100
    //});


    Ext.create('Jit.form.field.Text', {
        id: "txtQuestionnaireName",
        text: "",
        renderTo: "txtQuestionnaireName",
        width: 220
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtQuestionnaireDesc",
        text: "",
        renderTo: "txtQuestionnaireDesc",
        width: 450,
        height: 200
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