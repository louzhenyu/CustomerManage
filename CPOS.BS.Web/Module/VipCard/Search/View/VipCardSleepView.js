function InitView() {
    
    Ext.create('Jit.form.field.Date', {
        text: "",
        renderTo: "txtDormancyTime",
        id: "txtDormancyTime",
        jitSize: 'small',
        width: 100,
        format: 'Y-m-d'
    });
    
    Ext.create('Jit.button.Button', {
        text: "确认",
        renderTo: "btnSave",
        handler: fnSave
    });
    Ext.create('Jit.button.Button', {
        text: "关闭",
        renderTo: "btnClose",
        handler: fnClose
    });

}