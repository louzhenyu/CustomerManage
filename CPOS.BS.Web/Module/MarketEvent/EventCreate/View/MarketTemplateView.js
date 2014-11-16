function InitView() {
    
    //operator area
    Ext.create('Jit.button.Button', {
        text: "清空",
        renderTo: "btnReset",
        handler: fnReset
    });
    Ext.create('Jit.button.Button', {
        text: "保存",
        renderTo: "btnNext",
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        id: "btnStart",
        text: "立即启动发送",
        renderTo: "btnStart",
        width: 150,
        handler: fnSendMsg
    });
    Ext.create('Jit.button.Button', {
        text: "上一步",
        renderTo: "btnPre",
        handler: fnPre
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "tbTemplateContent",
        text: "",
        renderTo: "tbTemplateContent",
        width: '98%',
        //readOnly: true,
        disabled: true,
        height: 70
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "tbTemplateContentSMS",
        text: "",
        renderTo: "tbTemplateContentSMS",
        width: '98%',
        //readOnly: true,
        disabled: true,
        height: 70
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "tbTemplateContentAPP",
        text: "",
        renderTo: "tbTemplateContentAPP",
        width: '98%',
        //readOnly: true,
        disabled: true,
        height: 70
    });
    Ext.create('jit.biz.MarketSendType', {
        id: "txtSendType",
        renderTo: "txtSendType",
        width: 150
    });

}