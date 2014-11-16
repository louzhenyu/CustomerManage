function InitView() {

    Ext.create('Jit.form.field.Text', {
        id: "txtwapBank",
        text: "",
        renderTo: "txtwapBank",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwaptbBack",
        text: "",
        renderTo: "txtwaptbBack",
        width: 240
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtwapPublic",
        text: "",
        renderTo: "txtwapPublic",
        width: 240
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtwapPrivate",
        text: "",
        renderTo: "txtwapPrivate",
        width: 240
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtlineBank",
        text: "",
        renderTo: "txtlineBank",
        width: 240
    });
    Ext.create('Jit.form.field.TextArea', {
        id: "txtlinePrivate",
        text: "",
        renderTo: "txtlinePrivate",
        width: 240
    });



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
        width: 240
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
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupDecryptionPassword",
        text: "",
        renderTo: "txtCupDecryptionPassword",
        width: 240
    });




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
        width: 240
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
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupDecryptionPassword",
        text: "",
        renderTo: "txtwebCupDecryptionPassword",
        width: 240
    });



    Ext.create('Jit.form.field.Text', {
        id: "txtMicroLendtity",
        text: "",
        renderTo: "txtMicroLendtity",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroPublic",
        text: "",
        renderTo: "txtMricroPublic",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroStoreLendtity",
        text: "",
        renderTo: "txtMricroStoreLendtity",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroStoreCompotence",
        text: "",
        renderTo: "txtMricroStoreCompotence",
        width: 240
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroParsword",
        text: "",
        renderTo: "txtMricroParsword",
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

                //                $("#absolute1")[0].style.top = "650px";
                //                $("#absolute2")[0].style.top = "840px";
                //                // $("#absolute3")[0].style.top = "900px";
                //                if ($("#spanOpenUpload2")[0].style.display == "none") {
                //                    $("#absolute3")[0].style.top = "900px";
                //                }
                //                else {
                //                    $("#absolute3")[0].style.top = "955px";
                //                }
            }
            else {
                div[0].style.display = "none";
                $("#absolute1")[0].style.top = "600px"
                $("#absolute2")[0].style.top = "840px";
                //$("#absolute3")[0].style.top = "950px";
                if ($("#spanOpenUpload2")[0].style.display == "none") {
                    $("#absolute3")[0].style.top = "910px";
                }
                else {
                    $("#absolute3")[0].style.top = "960px";
                }
                if ($("#spanOpenUpload")[0].style.display == "none" && $("#spanOpenUpload1")[0].style.display == "none") {
                    $("#absolute2")[0].style.top = "740px";
                    $("#Div1").height(200);
                    if ($("#spanOpenUpload2")[0].style.display == "none") {
                        $("#absolute3")[0].style.top = "810px";
                    }
                    else {
                        $("#absolute3")[0].style.top = "860px";
                    }
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
                //   fnUploadFile("fileSpan", "txt_Attach", "txtCupEnCryption");
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
                 $("#absolute2")[0].style.top = "840px";
                 if ($("#spanOpenUpload2")[0].style.display == "none") {
                     $("#absolute3")[0].style.top = "900px";
                 }
                 else {
                     $("#absolute3")[0].style.top = "955px";
                 }

             }
             else {
                 div[0].style.display = "none";
                 if ($("#spanOpenUpload")[0].style.display == "none" && $("#spanOpenUpload1")[0].style.display == "none") {
                     $("#Div1").height(200);
                 }
                 $("#absolute2")[0].style.top = "740px";
                 if ($("#spanOpenUpload2")[0].style.display == "none") {
                     $("#absolute3")[0].style.top = "810px";
                 }
                 else {
                     $("#absolute3")[0].style.top = "857px";
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
                // fnUploadFile("fileSpan1", "txt_Attach1", "txtCupDecryption");
            }
        }]
    });

    Ext.create('Jit.button.Button', {
        text: "上传文件",
        renderTo: "btnOpenUpload2"
        , handler: function () {
            var div = $("#spanOpenUpload2");
            if (div[0].style.display == "none") {
                div[0].style.display = "";
                $("#Div2").height(300);
                if ($("#Div1").height() == 200) {

                    $("#absolute3")[0].style.top = "860px";
                }
                else {
                    $("#absolute3")[0].style.top = "950px";
                }
            }
            else {
                div[0].style.display = "none";
                if ($("#spanOpenUpload2")[0].style.display == "none" && $("#spanOpenUpload3")[0].style.display == "none") {
                    $("#Div2").height(200);
                }
                if ($("#Div1").height() == 200) {

                    $("#absolute3")[0].style.top = "810px";
                }
                else {
                    $("#absolute3")[0].style.top = "900px";
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