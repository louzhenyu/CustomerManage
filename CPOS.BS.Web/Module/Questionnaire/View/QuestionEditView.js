function InitView() {

    //editPanel area
    Ext.create('jit.biz.QuestionType', {
        id: "txtQuestionType",
        text: "",
        renderTo: "txtQuestionType",
        width: 100
    });


    Ext.create('Jit.form.field.Text', {
        id: "txtQuestionDesc",
        text: "",
        renderTo: "txtQuestionDesc",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtQuestionValue",
        text: "",
        renderTo: "txtQuestionValue",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMinSelected",
        text: "",
        value: '0',
        renderTo: "txtMinSelected",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMaxSelected",
        text: "",
        value: '0',
        renderTo: "txtMaxSelected",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtQuestionValueCount",
        text: "",
        value: '0',
        renderTo: "txtQuestionValueCount",
        width: 100
    });
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsRequired",
        text: "",
        renderTo: "txtIsRequired",
        width: 100
    });
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsOpen",
        text: "",
        renderTo: "txtIsOpen",
        width: 100
    });
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsSaveOutEvent",
        text: "",
        renderTo: "txtIsSaveOutEvent",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCookieName",
        text: "",
        renderTo: "txtCookieName",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        value: "0",
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