function InitView() {

    Ext.create('jit.biz.SysIntegralSource', {
        id: "txtIntegralSourceID",
        text: "",
        renderTo: "txtIntegralSourceID",
        width: 300
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtIntegral",
        text: "",
        renderTo: "txtIntegral",
        width: 400
    });
    Ext.create('Jit.form.field.Date', {
        text: "",
        renderTo: "txtBeginDate",
        id: "txtBeginDate",
        jitSize: 'small',
        width: 100,
        format: 'Y-m-d'
    });
    Ext.create('Jit.form.field.Date', {
        text: "",
        renderTo: "txtEndDate",
        id: "txtEndDate",
        jitSize: 'small',
        width: 100,
        format: 'Y-m-d'
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtIntegralDesc",
        text: "",
        renderTo: "txtIntegralDesc",
        width: 400,
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