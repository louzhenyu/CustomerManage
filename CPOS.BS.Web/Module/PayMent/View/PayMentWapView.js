function InitView() {
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupBackID",
        text: "",
        renderTo: "txtwebCupBackID",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupEnCryption",
        text: "",
        renderTo: "txtwebCupEnCryption",
        width: 240,
        readOnly: true
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupPassword",
        text: "",
        renderTo: "txtwebCupPassword",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupDecryption",
        text: "",
        renderTo: "txtwebCupDecryption",
        width: 240,
        readOnly: true
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupDecryptionPassword",
        text: "",
        renderTo: "txtwebCupDecryptionPassword",
        width: 240
    });
    Ext.create('Ext.Button', {
        text: '保存',
        width: '100',
        height: '50',
        renderTo: "btnSave",
        cls: 'transparentBut',
        handler: fnSave
    });
    Ext.create('Ext.Button', {
        text: '关闭',
        width: '100',
        height: '50',
        renderTo: "btnClose",
        cls: 'CloseBut',
        handler: fnClose
    });

    Ext.create('Jit.button.Button', {
        text: "上传文件",
        renderTo: "btnOpenUpload2"
        , handler: function () {
            var div = $("#spanOpenUpload2");

            if (div[0].style.display == "none") {
                div[0].style.display = "";
                $("#Div2").height(300);
                $("#absolute3")[0].style.top = "205px";
            }
            else {
                div[0].style.display = "none";
                $("#absolute3")[0].style.top = "150px"
                if ($("#spanOpenUpload2")[0].style.display == "none" && $("#spanOpenUpload3")[0].style.display == "none") {
                    $("#Div2").height(200);
                }

            }
        }
    });


    Ext.create('Ext.form.Panel', {
        renderTo: "spanUpload2",
        fileUpload: true,
        layout: 'column',
        //width: "400",
        margin: '10 0 10 10',
        border: 0,
        id: "fileSpan2",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "文件",
            name: "file",
            id: "txt_Attach2",
            inputType: 'file',
            allowBlank: false,
            //acceptMimes: ['doc', 'xls', 'xlsx', 'pdf', 'zip', 'rar'],
            //maxFileSize: 1 * 1024 * 1024,
            acceptSize: 2,
            width: 300
        }, {
            xtype: 'displayfield',
            id: "filehidden2",
            name: 'AttacnmentFileName'
        }]
    });

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: "spanUploadButton2",
        layout: 'column',
        margin: '0 30 7 0',
        name: "upload",
        border: 0,
        items: [{
            xtype: "jitbutton",
            text: '开始上传',
            jitIsHighlight: true,
            jitIsDefaultCSS: true,

            handler: function () { fnUploadFile("fileSpan2", "txt_Attach2", "txtwebCupEnCryption"); }
        }]
    });



    Ext.create('Jit.button.Button', {
        text: "上传文件",
        renderTo: "btnOpenUpload3"
        , handler: function () {
            var div = $("#spanOpenUpload3");
            if (div[0].style.display == "none") {
                div[0].style.display = "";
                $("#Div2").height(300);

            }
            else {
                div[0].style.display = "none";
                if ($("#spanOpenUpload2")[0].style.display == "none" && $("#spanOpenUpload3")[0].style.display == "none") {
                    $("#Div2").height(200);
                }
            }
        }
    });

    Ext.create('Ext.form.Panel', {
        renderTo: "spanUpload3",
        fileUpload: true,
        layout: 'column',
        //width: "400",
        margin: '10 0 10 10',
        border: 0,
        id: "fileSpan3",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "文件",
            name: "file",
            id: "txt_Attach3",
            inputType: 'file',
            allowBlank: false,
            //acceptMimes: ['doc', 'xls', 'xlsx', 'pdf', 'zip', 'rar'],
            //maxFileSize: 1 * 1024 * 1024,
            acceptSize: 2,
            width: 300
        }, {
            xtype: 'displayfield',
            id: "filehidden3",
            name: 'AttacnmentFileName'
        }]
    });

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: "spanUploadButton3",
        layout: 'column',
        margin: '0 30 7 0',
        name: "upload",
        border: 0,
        items: [{
            xtype: "jitbutton",
            text: '开始上传',
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: function () { fnUploadFile("fileSpan3", "txt_Attach3", "txtwebCupDecryption"); }
        }]
    });


}