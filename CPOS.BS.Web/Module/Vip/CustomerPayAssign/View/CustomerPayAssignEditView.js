function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 451,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '基本信息'
        }
        ]
    });
    
    Ext.create('jit.biz.PaymentType', {
        id: "txtPaymentTypeId",
        text: "",
        renderTo: "txtPaymentTypeId",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCustomerAccountNumber",
        text: "",
        renderTo: "txtCustomerAccountNumber",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtCustomerProportion",
        value: "0",
        maxValue: 100,
        renderTo: "txtCustomerProportion",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtJITProportion",
        value: "0",
        maxValue: 100,
        renderTo: "txtJITProportion",
        width: 100
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 400,
        height: 200,
        margin: '0 0 0 10'
    });

    
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:0px;padding-bottom:0px;border:0px;',
        //layout: 'anchor',
        layout: {
            type: 'table'
            , columns: 3
            , align: 'right'
        },
        defaults: {},

        items: [
        ]
        ,buttonAlign: "left"
        ,buttons: [
        {
            xtype: "jitbutton",
            text: "保存",
            handler: fnSave
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });

    

}