function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtName",
        text: "",
        renderTo: "txtName",
        width: 100
    });
    Ext.create('jit.biz.EEnterpriseCustomerType', {
        id: "txtTypeId",
        text: "",
        renderTo: "txtTypeId",
        width: 100
    });
    Ext.create('jit.biz.EIndustry', {
        id: "txtIndustryId",
        text: "",
        renderTo: "txtIndustryId",
        width: 100
    });
    Ext.create('Jit.Biz.CitySelectTree', {
        id: "txtCity",
        text: "",
        renderTo: "txtCity",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtAddress",
        text: "",
        renderTo: "txtAddress",
        width: 320
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtTel",
        text: "",
        renderTo: "txtTel",
        width: 100
    });
    Ext.create('jit.biz.EEnterpriseCustomerSource', {
        id: "txtECSourceId",
        text: "",
        renderTo: "txtECSourceId",
        width: 100
    });
    Ext.create('jit.biz.EScale', {
        id: "txtScaleId",
        text: "",
        renderTo: "txtScaleId",
        width: 100
    });
    Ext.create('jit.biz.EEnterpriseCustomerStatus', {
        id: "txtStatus",
        text: "",
        renderTo: "txtStatus",
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 480,
        height: 200,
        margin: "0 0 0 10"
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