function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Date', {
        id: "txtWorkDate",
        text: "",
        renderTo: "txtWorkDate",
        width: 100
    });


    Ext.create('Jit.form.field.Text', {
        id: "txtWorkTitle",
        text: "",
        renderTo: "txtWorkTitle",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtCreative",
        text: "",
        renderTo: "txtCreative",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtPhone",
        text: "",
        renderTo: "txtPhone",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCreativeAddress",
        text: "",
        renderTo: "txtCreativeAddress",
        width: 405
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        value: 0,
        renderTo: "txtDisplayIndex",
        width: 100
    }); 

    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        //readOnly: true,
        width: 405
    });


    var content = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent',
        //renderTo: "txtContent",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#txtContent', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 300,
                    width: '100%'
                });
            }
        }
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
            , jitIsHighlight: false
            , jitIsDefaultCSS: true
        }]
    });
}