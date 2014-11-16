function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtVipCardCode",
        text: "",
        renderTo: "txtVipCardCode",
        readOnly: true,
        hidden: true,
        width: 150
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtVipCardName",
        text: "",
        renderTo: "txtVipCardName",
        width: 150
    });
    Ext.create('jit.biz.VipCardType', {
        id: "txtVipCardType",
        text: "",
        renderTo: "txtVipCardType",
        width: 150
    });
    Ext.create('jit.biz.VipCardStatus', {
        id: "txtVipCardStatus",
        text: "",
        renderTo: "txtVipCardStatus",
        width: 150
    });
    Ext.create('jit.biz.VipCardGrade', {
        id: "txtVipCardGrade",
        text: "",
        renderTo: "txtVipCardGrade",
        width: 150
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtBalanceAmount",
        value: "0",
        renderTo: "txtBalanceAmount",
        width: 150,
        allowDecimals: true,
        decimalPrecision: 2
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtBeginDate",
        text: "",
        renderTo: "txtBeginDate",
        width: 150
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtEndDate",
        text: "",
        renderTo: "txtEndDate",
        width: 150
    });
    Ext.create('Jit.Biz.UnitSelectTree', {
        id: "txtUnitName",
        text: "",
        renderTo: "txtUnitName",
        width: 150
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