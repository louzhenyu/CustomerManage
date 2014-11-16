function InitView() {
    Ext.create('Jit.form.field.Text', {
        id: "txtCupBackID",
        text: "",
        renderTo: "txtCupBackID",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupEnCryption",
        text: "",
        renderTo: "txtCupEnCryption",
        width: 240,
        readOnly: true
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupPassword",
        text: "",
        renderTo: "txtCupPassword",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupDecryption",
        text: "",
        renderTo: "txtCupDecryption",
        width: 240,
        readOnly: true
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupDecryptionPassword",
        text: "",
        renderTo: "txtCupDecryptionPassword",
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
        renderTo: "btnOpenUpload"
        , handler: function () {
            debugger;
            var div = $("#spanOpenUpload");
            if (div[0].style.display == "none") {
                div[0].style.display = "";
                $("#Div1").height(300);
                $("#absolute1")[0].style.top = "205px";
            }
            else {
                div[0].style.display = "none";
                $("#absolute1")[0].style.top = "150px"
                if ($("#spanOpenUpload")[0].style.display == "none" && $("#spanOpenUpload1")[0].style.display == "none") {
                    $("#Div1").height(200);
                }

            }
        }
    });

    Ext.create('Ext.form.Panel', {
        renderTo: "spanUpload",
        fileUpload: true,
        layout: 'column',
        //width: "400",
        margin: '10 0 10 10',
        border: 0,
        id: "fileSpan",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "文件",
            name: "file",
            id: "txt_Attach",
            inputType: 'file',
            allowBlank: false,
            //acceptMimes: ['doc', 'xls', 'xlsx', 'pdf', 'zip', 'rar'],
            //maxFileSize: 1 * 1024 * 1024,
            acceptSize: 2,
            width: 300
        }, {
            xtype: 'displayfield',
            id: "filehidden",
            name: 'AttacnmentFileName'
        }]
    });

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: "spanUploadButton",
        layout: 'column',
        margin: '0 30 7 0',
        name: "upload",
        border: 0,
        items: [{
            xtype: "jitbutton",
            text: '开始上传',
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: function () {
                fnUploadFile("fileSpan", "txt_Attach", "txtCupEnCryption");
            }
        }]
    });

    Ext.create('Jit.button.Button', {
        text: "上传文件",
        renderTo: "btnOpenUpload1"
         , handler: function () {
             var div = $("#spanOpenUpload1");
             if (div[0].style.display == "none") {
                 div[0].style.display = "";
                 $("#Div1").height(300);

             }
             else {
                 div[0].style.display = "none";
                 if ($("#spanOpenUpload")[0].style.display == "none" && $("#spanOpenUpload1")[0].style.display == "none") {
                     $("#Div1").height(200);
                 }
             }
         }
    });

    Ext.create('Ext.form.Panel', {
        renderTo: "spanUpload1",
        fileUpload: true,
        layout: 'column',
        //width: "400",
        margin: '10 0 10 10',
        border: 0,
        id: "fileSpan1",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "文件",
            name: "file",
            id: "txt_Attach1",
            inputType: 'file',
            allowBlank: false,
            //acceptMimes: ['doc', 'xls', 'xlsx', 'pdf', 'zip', 'rar'],
            //maxFileSize: 1 * 1024 * 1024,
            acceptSize: 2,
            width: 300
        }, {
            xtype: 'displayfield',
            id: "filehidden1",
            name: 'AttacnmentFileName'
        }]
    });

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: "spanUploadButton1",
        layout: 'column',
        margin: '0 30 7 0',
        name: "upload",
        border: 0,
        items: [{
            xtype: "jitbutton",
            text: '开始上传',
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: function () {
                fnUploadFile("fileSpan1", "txt_Attach1", "txtCupDecryption");
            }
        }]
    });


}