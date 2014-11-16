function InitView() {

    //editPanel area
    Ext.create('Jit.form.field.Text', {
        id: "txtTitle",
        text: "",
        renderTo: "txtTitle",
        maxLength: 100,
        enforceMaxLength: true,
        width: 515
    });
    Ext.create('jit.biz.AlbumTypeLink', {
        id: "txtAlbumType",
        text: "",
        renderTo: "txtAlbumType",
        enforceMaxLength: true,
        width: 100
    });

    Ext.create('Jit.form.field.ComboBox', {
        id: "txtPicType",
        store: Ext.getStore("AlbumITypeStore"),
        displayField: 'ModuleName',
        valueField: 'ModuleType',
        renderTo: "txtPicType",
        width: 100,
        isDefault: true
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtSortOrder",
        renderTo: "txtSortOrder",
        maxLength: 4,
        enforceMaxLength: true,
        value: 1,
        maxValue: 9999,
        minValue: 1,
        width: 100
    });
    //    Ext.create('Jit.form.field.Text', {
    //        id: "txtImageURL",
    //        text: "",
    //        renderTo: "txtImageURL",
    //        maxLength: 200,
    //        enforceMaxLength: true,
    //        width: 515
    //    });
    Ext.create('Jit.form.field.Text', {
        id: "txtModuleName",
        text: "",
        renderTo: "txtModuleName",
        maxLength: 100,
        enforceMaxLength: true,
        readOnly: true,
        width: 515
    });
    Ext.create('Jit.button.Button', {
        text: "选择模块",
        renderTo: "btnCreateLink",
        handler: fnCreateLink
    });

    var content = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent',
        renderTo: "txtContent",
        anchor: '80%'
        , listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#txtContent', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 290,
                    width: "100%"
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
        border: 0,
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
            handler: fnSave,
            jitIsHighlight: true,
            jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}