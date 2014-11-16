function InitView() {

    Ext.create('Jit.form.field.ComboBox', {
        id: "txtNewsType",
        text: "",
        renderTo: "txtNewsType",
        displayField: 'ModuleName',
        emptyText: '--请选择--',
        valueField: 'ModuleType',
        name: 'ModuleType',
        store: Ext.getStore("lNewsTypeStore"),
        width: 112
    });
    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtNewsTitle",
        text: "",
        renderTo: "txtNewsTitle",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtVideoUrl",
        text: "",
        renderTo: "txtVideoUrl",
        width: 405
    });


    Ext.create('Jit.form.field.Number', {
        id: "txtSortOrder",
        text: "",
        labelPad: 0
        , labelWidth: 0,
        renderTo: "txtSortOrder",
        width: 112, margin: '0 0 10 0 '
    });

    Ext.create('Jit.form.field.TextArea', {
        id: 'txt_Intro',
        name: 'Intro',
        renderTo: 'divIntro'
        , width: 400
        , height: 100
        , labelPad: 0
        , labelWidth: 0
        , margin: '0'
    });


    var content = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent',
        renderTo: "txtContent",
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

    Ext.create('Ext.form.Panel', {
        id: 'fileForm',
        width: 326,
        height: 22,
        margin: '0 0 0 10',
        columnWidth: 1,
        items: [
        {
            xtype: 'filefield',
            name: 'file-path',
            renderTo: 'txtfilepath',
            width: 405,
            labelWidth: 0,
            height: 22,
            id: 'file-pathID',
            labelSeparator: '',
            labelPad: 0,
            labelAlign: 'right',
            buttonText: '选择文件...',
            fieldLabel: '',
            margin: '0 0 10 10 '
        },
    {
        xtype: "jitbutton",
        text: "上传视频",
        handler: fnUpdateVideo,
        renderTo: 'btnVideo',
        jitIsHighlight: false,
        jitIsDefaultCSS: false,
        margin: '0 0 0 0 '
    }],
        layout: 'column',
        border: 0
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