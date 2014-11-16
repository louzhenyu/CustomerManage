function InitView() {

    Ext.create('Jit.form.field.Text', {
        id: "txtField2",
        text: "",
        renderTo: "txtField2",
        width: 100
    });
    Ext.create('jit.biz.DeliveryUnit', {
        id: "txtCarrierId",
        text: "",
        renderTo: "txtCarrierId",
        width: 100
    });
    
    Ext.create('Jit.button.Button', {
        text: "发货",
        renderTo: "btnSave",
        handler: fnSave
    });
    Ext.create('Jit.button.Button', {
        text: "关闭",
        renderTo: "btnClose",
        handler: fnClose
    });

}