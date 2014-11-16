function InitView() {

    //editPanel area

    //jifeng.cao begin
    Ext.create('Jit.biz.Options', {
        id: "cmbIsDefault",
      OptionName: 'YES/NO',
        //emptyText: '--请选择--',
        //store: Ext.getStore("titleStore"),
        renderTo: "cmbIsDefault",
        width: 183
    });
  //Ext.getCmp("cmbIsDefault").load();

    Ext.create('Jit.biz.Options', {
        id: "cmbIsTop",
        OptionName: 'YES/NO',
        renderTo: "cmbIsTop",
        width: 183
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtAuthor",
        text: "",
        renderTo: "txtAuthor",
        width: 183
    });

    //jifeng.cao end
    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        value: "1",
        width: 100
    });

    //    Ext.create('jit.biz.NewsType', {
    //        id: "txtNewsType",
    //        text: "",
    //        renderTo: "txtNewsType",
    //        width: 100
    //    });
    Ext.create('Jit.Biz.LnewsTypeSelectTree', {
        id: "txtNewsType",
        text: "",
        renderTo: "txtNewsType",
        width: 100
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtPublishTime",
        text: "",
        renderTo: "txtPublishTime",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtNewsTitle",
        text: "",
        renderTo: "txtNewsTitle",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtNewsSubTitle",
        text: "",
        renderTo: "txtNewsSubTitle",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtContentUrl",
        text: "",
        renderTo: "txtContentUrl",
        width: 405
    });

//    Ext.create('Jit.form.field.Text', {
//        id: "txtImageUrl",
//        text: "",
//        renderTo: "txtImageUrl",
//        readOnly: true,
//        width: 405
//    });

    Ext.create('Jit.form.field.Text', {
        id: "txtThumbnailImageUrl",
        text: "",
        renderTo: "txtThumbnailImageUrl",
        readOnly: true,
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtAPPId",
        text: "",
        renderTo: "txtAPPId",
        width: 405
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

    Ext.create('Jit.biz.CheckboxList', {
        renderTo: 'divTags'
        , width: 400
        , height: 100
                , items: objTags
    });





}