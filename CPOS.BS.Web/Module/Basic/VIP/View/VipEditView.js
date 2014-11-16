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
    

    Ext.create('Jit.form.field.Text', {
        id: "txtTagsName",
        text: "",
        renderTo: "txtTagsName",
        width: 200
    });
    
    Ext.create('Jit.form.field.TextArea', {
        id: "txtTagsDesc",
        text: "",
        renderTo: "txtTagsDesc",
        margin: "0 0 0 10",
        height: 130,
        width: 405
    });
    
    Ext.create('Jit.form.field.TextArea', {
        id: "txtTagsFormula",
        text: "",
        renderTo: "txtTagsFormula",
        height: 130,
        width: 405
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