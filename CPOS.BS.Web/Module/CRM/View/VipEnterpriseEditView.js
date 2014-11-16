function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtName",
        text: "",
        renderTo: "txtName",
        width: 100
    });
    Ext.create('jit.biz.UserGender', {
        id: "txtGender",
        text: "",
        renderTo: "txtGender",
        width: 100
    });
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtEnterpriseCustomerId",
    //    text: "",
    //    renderTo: "txtEnterpriseCustomerId",
    //    readOnly: true,
    //    width: 100
    //});
    Ext.create('jit.biz.ECCustomerSelect', {
        id: "txtEnterpriseCustomerId",
        text: "",
        renderTo: "txtEnterpriseCustomerId",
        fnCallback: function(d) {
            get("hECCustomerId").value = d.id;
        },
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtDepartment",
        text: "",
        renderTo: "txtDepartment",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtPosition",
        text: "",
        renderTo: "txtPosition",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtPhone",
        text: "",
        renderTo: "txtPhone",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtFax",
        text: "",
        renderTo: "txtFax",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEmail",
        text: "",
        renderTo: "txtEmail",
        width: 100
    });
    Ext.create('jit.biz.VipEnterpriseExpandStatus', {
        id: "txtStatus",
        text: "",
        renderTo: "txtStatus",
        width: 100
    });
    Ext.create('jit.biz.EPolicyDecisionRole', {
        id: "txtPDRoleId",
        text: "",
        renderTo: "txtPDRoleId",
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 380,
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

    
    Ext.create('Jit.form.field.Text', {
        id: "tbECSearchCustomerName",
        text: "",
        renderTo: "tbECSearchCustomerName",
        width: 180
    });
    Ext.create('Jit.button.Button', {
        text: "搜索",
        renderTo: "tbECSearchCustomerGo",
        width: 70,
        handler: fnECSearchCustomerGo
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    Ext.create('Jit.button.Button', {
        text: "清除",
        renderTo: "tbECSearchCustomerClear",
        width: 70,
        handler: fnECSearchCustomerClear
    });

}