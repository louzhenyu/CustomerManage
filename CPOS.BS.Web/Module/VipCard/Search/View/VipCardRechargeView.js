function InitView() {

    Ext.create('Jit.form.field.Number', {
        id: "txtCashAmount",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 2,
        renderTo: "txtCashAmount"
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtCardAmount",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 2,
        renderTo: "txtCardAmount"
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtOrderNo",
        text: "",
        renderTo: "txtOrderNo",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtRechargeAmount",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 2,
        renderTo: "txtRechargeAmount"
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 400
    });
    
    Ext.create('Jit.form.field.Radio', {
        id: "chkAmount1",
        text: "",
        renderTo: "chkAmount1",
        name: "chkAmount",
        width: 100,
        checked: true,
        margin: '-3 0 0 10',
        handler: fnCheckAmount
    });
    Ext.create('Jit.form.field.Radio', {
        id: "chkAmount2",
        text: "",
        renderTo: "chkAmount2",
        name: "chkAmount",
        width: 100,
        checked: false,
        margin: '-3 0 0 10',
        handler: fnCheckAmount
    });
    
    Ext.create('Jit.button.Button', {
        text: "快速充值",
        renderTo: "btnSave",
        handler: fnSave
    });
    Ext.create('Jit.button.Button', {
        text: "关闭",
        renderTo: "btnClose",
        handler: fnClose2
    });

}