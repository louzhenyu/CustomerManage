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
    
    Ext.create('Jit.Biz.UnitSelectTree', {
        id: "txtParentUnit",
        text: "",
        renderTo: "txtParentUnit",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtWarehouseCode",
        text: "",
        renderTo: "txtWarehouseCode",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWarehouseName",
        text: "",
        renderTo: "txtWarehouseName",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWarehouseEnglish",
        text: "",
        renderTo: "txtWarehouseEnglish",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWarehouseContacter",
        text: "",
        renderTo: "txtWarehouseContacter",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWarehouseTel",
        text: "",
        renderTo: "txtWarehouseTel",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWarehouseFax",
        text: "",
        renderTo: "txtWarehouseFax",
        width: 100
    });
    
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsDefaultWarehouse",
        text: "",
        renderTo: "txtIsDefaultWarehouse",
        dataType: "yn",
        width: 100
    });

    Ext.create('jit.biz.Status', {
        id: "txtWarehouseStatus",
        text: "",
        renderTo: "txtWarehouseStatus",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtAddress",
        text: "",
        renderTo: "txtAddress",
        width: 315
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 315,
        height: 70,
        margin: '0 0 10 10'
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtCreateUserName",
        text: "",
        renderTo: "txtCreateUserName",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtCreateTime",
        text: "",
        renderTo: "txtCreateTime",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtModifyUserName",
        text: "",
        renderTo: "txtModifyUserName",
        readOnly: true,
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtModifyTime",
        text: "",
        renderTo: "txtModifyTime",
        readOnly: true,
        width: 100
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
            formBind: true,
            disabled: true,
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