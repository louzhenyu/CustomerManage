function InitView() {

    //operator area
    Ext.create('Jit.button.Button', {
        text: "清空",
        renderTo: "btnReset",
        handler: fnReset
    });
    Ext.create('Jit.button.Button', {
        text: "下一步",
        renderTo: "btnNext",
        handler: fnSave
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });

    
    Ext.create('jit.biz.MarketBrand', {
        id: "txtBrand",
        text: "",
        renderTo: "txtBrand",
        width: 100
    });
    //Ext.create('jit.biz.EventType', {
    //    id: "txtEventType",
    //    text: "",
    //    renderTo: "txtEventType",
    //    width: 100
    //});
    Ext.create('jit.biz.EventMode', {
        id: "txtEventMode",
        text: "",
        renderTo: "txtEventMode",
        width: 100
    });
    
    Ext.create('Jit.form.field.Number', {
        id: "txtAmount1",
        value: "0",
        renderTo: "txtAmount1",
        width: 100,
        allowDecimals: true,
        decimalPrecision: 2
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtAmount2",
        value: "0",
        renderTo: "txtAmount2",
        width: 100,
        disabled: true,
        allowDecimals: true,
        decimalPrecision: 2
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtEventDesc",
        text: "",
        renderTo: "txtEventDesc",
        width: 500,
        height: 180
    });
    
    Ext.create('Jit.form.field.Radio', {
        id: "chkAmount1",
        text: "",
        renderTo: "chkAmount1",
        name: "chkAmount",
        width: 100,
        checked: true,
        handler: fnCheckAmount
    });
    Ext.create('Jit.form.field.Radio', {
        id: "chkAmount2",
        text: "",
        renderTo: "chkAmount2",
        name: "chkAmount",
        width: 100,
        handler: fnCheckAmount
    });

}