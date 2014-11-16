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
    
    Ext.create('jit.biz.AppSys', {
        id: "txtAppSys",
        text: "",
        renderTo: "txtAppSys",
        width: 100,
        selectFn: function() {
            fnLoadMenus(Ext.getCmp("txtAppSys").jitGetValue(), getUrlParam("role_id"));
        }
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtRoleCode",
        text: "",
        renderTo: "txtRoleCode",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtRoleName",
        text: "",
        renderTo: "txtRoleName",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtRoleEnglish",
        text: "",
        renderTo: "txtRoleEnglish",
        width: 100
    });
    
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsSys",
        text: "",
        renderTo: "txtIsSys",
        dataType: "yn",
        width: 100
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