function InitView() {
    
    Ext.create('jit.biz.AppSys', {
        id: "txtAppSys",
        text: "",
        renderTo: "txtAppSys",
        width: 100,
        selectFn: function() {
            if (Ext.getCmp("txtRole") != undefined && Ext.getCmp("txtRole") != null) {
                Ext.getCmp("txtRole").jitSetValue("");
            }
        }
    });

    Ext.create('jit.biz.Role', {
        id: "txtRole",
        text: "",
        width: 100,
        renderTo: "txtRole",
        parentId: "txtAppSys"
    });

    Ext.create('Jit.Biz.UnitSelectTree', {
        id: "txtUnit",
        text: "",
        renderTo: "txtUnit",
        width: 100,
        labelWidth: 300,
        matchFieldWidth: false,
        labelHeight: 100,
        matchFieldHeight: false
    });

    Ext.create('jit.biz.YesNoStatus', {
        id: "txtDefaultFlag",
        text: "",
        renderTo: "txtDefaultFlag",
        dataType: "yn",
	value:'1',//初始值
        width: 100
    });

//    
//    Ext.create('Jit.form.field.Text', {
//        id: "txtSkuProp2",
//        text: "",
//        width: 100,
//        readOnly: true,
//        renderTo: "txtSkuProp2"
//    });
//    
//    Ext.create('Jit.form.field.Text', {
//        id: "txtSkuProp3",
//        text: "",
//        width: 100,
//        readOnly: true,
//        renderTo: "txtSkuProp3"
//    });

//    Ext.create('Jit.form.field.Text', {
//        id: "txtSkuProp4",
//        text: "",
//        width: 100,
//        readOnly: true,
//        renderTo: "txtSkuProp4"
//    });

//    Ext.create('Jit.form.field.Text', {
//        id: "txtSkuProp5",
//        text: "",
//        width: 100,
//        readOnly: true,
//        renderTo: "txtSkuProp5"
//    });

//    Ext.create('Jit.form.field.Text', {
//        id: "txtItemName",
//        text: "",
//        width: 380,
//        readOnly: true,
//        renderTo: "txtItemName"
//    });

//    Ext.create('Jit.form.field.Number', {
//        id: "txtEnterQty",
//        text: "0",
//        width: 100,
//        readOnly: false,
//        allowDecimals: true,
//        decimalPrecision: 4,
//        renderTo: "txtEnterQty"
//    });
//    
//    Ext.create('Jit.form.field.Number', {
//        id: "txtRetailPrice",
//        text: "0",
//        width: 100,
//        readOnly: false,
//        allowDecimals: true,
//        decimalPrecision: 4,
//        renderTo: "txtRetailPrice"
//    });
//    
//    Ext.create('Jit.form.field.Number', {
//        id: "txtEnterPrice",
//        text: "0",
//        width: 100,
//        readOnly: false,
//        allowDecimals: true,
//        decimalPrecision: 4,
//        renderTo: "txtEnterPrice"
//    });
    
    // op
    Ext.create('Jit.button.Button', {
        id: "btnSave",
        text: "确定",
        renderTo: "btnSave",
        //magin: '0 0 0 0',
        handler: function() {
            eval("fnSave()");
        }
    });
    
    Ext.create('Jit.button.Button', {
        id: "btnClose",
        text: "关闭",
        renderTo: "btnClose",
        //magin: '0 0 0 0',
        handler: function() {
            eval("fnClose()");
        }
    });


}