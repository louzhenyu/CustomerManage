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
            title: '图片素材信息'
        }
        ]
    });
    

    Ext.create('Jit.form.field.Text', {
        id: "txtImageName",
        text: "",
        renderTo: "txtImageName",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtImageSize",
        text: "",
        renderTo: "txtImageSize",
        width: 110
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtImageFormat",
        text: "",
        renderTo: "txtImageFormat",
        width: 110
    });
    
    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 110
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