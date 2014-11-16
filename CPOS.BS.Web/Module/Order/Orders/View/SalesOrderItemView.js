function InitView() {

    Ext.create('jit.biz.SkuSelect', {
        id: "txtItemCode",
        width: 380,
        renderTo: "txtItemCode",
        nameId: 'txtItemName',
        skuPropId: {
            rowSkuProp1: 'rowSkuProp1',
            rowSkuProp2: 'rowSkuProp2',
            rowSkuProp3: 'rowSkuProp3',

            lblSkuProp1: 'lblSkuProp1',
            lblSkuProp2: 'lblSkuProp2',
            lblSkuProp3: 'lblSkuProp3',
            lblSkuProp4: 'lblSkuProp4',
            lblSkuProp5: 'lblSkuProp5',

            txtSkuProp1: 'txtSkuProp1',
            txtSkuProp2: 'txtSkuProp2',
            txtSkuProp3: 'txtSkuProp3',
            txtSkuProp4: 'txtSkuProp4',
            txtSkuProp5: 'txtSkuProp5'
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtSkuProp1",
        text: "",
        width: 100,
        readOnly: true,
        renderTo: "txtSkuProp1"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtSkuProp2",
        text: "",
        width: 100,
        readOnly: true,
        renderTo: "txtSkuProp2"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtSkuProp3",
        text: "",
        width: 100,
        readOnly: true,
        renderTo: "txtSkuProp3"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtSkuProp4",
        text: "",
        width: 100,
        readOnly: true,
        renderTo: "txtSkuProp4"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtSkuProp5",
        text: "",
        width: 100,
        readOnly: true,
        renderTo: "txtSkuProp5"
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtItemName",
        text: "",
        width: 380,
        readOnly: true,
        renderTo: "txtItemName"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtEnterQty",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtEnterQty"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtOrderQty",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtOrderQty"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtStdPrice",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtStdPrice"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtOrderDiscountRate",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtOrderDiscountRate"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtEnterPrice",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtEnterPrice"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtDiscountRate",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtDiscountRate"
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtRetailPrice",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtRetailPrice"
    });

    // op
    Ext.create('Jit.button.Button', {
        id: "btnSave",
        text: "确定",
        renderTo: "btnSave",
        handler: function () {
            eval("fnSave()");
        }
    });

    Ext.create('Jit.button.Button', {
        id: "btnClose",
        text: "关闭",
        renderTo: "btnClose",
        handler: function () {
            eval("fnClose()");
        }
    });
}