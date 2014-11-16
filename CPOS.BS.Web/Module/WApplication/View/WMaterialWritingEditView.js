function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 401,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '文本素材信息'
        }
        ]
    });
    
    Ext.create('Jit.form.field.TextArea', {
        id: "txtContent",
        text: "",
        renderTo: "txtContent",
        margin: '0 0 0 10',
        width: 405,
        height: 260
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
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "返回",
            handler: fnClose
        }
        ]
    });

}