function InitView() {

    Ext.create('jit.biz.SkuSelect', {
        id: "txtItemCode",
        //text: "",
        width: 380,
        renderTo: "txtItemCode",
        nameId: 'txtItemName'
        ,
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
        id: "txtPrice",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtPrice"
    });

    //Ext.create('Jit.form.field.Number', {
    //    id: "txtRequestNum",
    //    text: "0",
    //    width: 100,
    //    readOnly: false,
    //    allowDecimals: true,
    //    decimalPrecision: 4,
    //    renderTo: "txtRequestNum"
    //});

    Ext.create('Jit.form.field.Number', {
        id: "txtInNum",
        text: "0",
        width: 100,
        readOnly: false,
        allowDecimals: true,
        decimalPrecision: 4,
        renderTo: "txtInNum"
    });

    // op
    Ext.create('Jit.button.Button', {
        id: "btnSave",
        text: "确定",
        renderTo: "btnSave",
        //magin: '0 0 0 0',
        handler: function () {
            eval("fnSave()");
        }
    });

    Ext.create('Jit.button.Button', {
        id: "btnClose",
        text: "关闭",
        renderTo: "btnClose",
        //magin: '0 0 0 0',
        handler: function () {
            eval("fnClose()");
        }
    });


}