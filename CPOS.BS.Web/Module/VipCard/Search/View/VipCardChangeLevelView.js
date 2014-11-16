function InitView() {

    //Ext.create('Jit.form.field.Text', {
    //    id: "txtReturnCash",
    //    text: "",
    //    renderTo: "txtReturnCash",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtReturnCard",
    //    text: "",
    //    renderTo: "txtReturnCard",
    //    width: 100
    //});
    Ext.create('jit.biz.VipCardGrade', {
        id: "txtExtensionYear",
        text: "",
        renderTo: "txtExtensionYear",
        fnCallback: fnExtensionYear,
        width: 100
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