function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 521,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '图文素材信息'
        }
        ]
    });
    

    Ext.create('Jit.form.field.Text', {
        id: "txtTitle",
        text: "",
        renderTo: "txtTitle",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtAuthor",
        text: "",
        renderTo: "txtAuthor",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtCoverImageUrl",
        text: "",
        renderTo: "txtCoverImageUrl",
        width: 405
    });
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtText",
    //    text: "",
    //    renderTo: "txtText",
    //    width: 405
    //});
    var content = new Ext.form.TextArea({
        height: 10,
        //margin: '0 0 0 10',
        id: 'txtText',
        renderTo: "txtText",
        anchor: '100%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#txtText', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 300,
                    width: '100%'
                });
            }
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtOriginalUrl",
        text: "",
        renderTo: "txtOriginalUrl",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        width: 110
    });
    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 110
    });
    Ext.create('jit.biz.WMaterialTextType', {
        id: "txtTypeId",
        text: "",
        renderTo: "txtTypeId",
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
            text: "关闭",
            handler: fnClose
        }
        ]
    });

}