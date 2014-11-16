function InitView() {
    new Ext.form.Radio({
        boxLabel: '<font class="rbclass">不使用</font>',
        inputValue: 'user',
        name: 'radiogroup',
        renderTo: 'rbwapNo',
        id: "rbwapNo",
        listeners: {
            'change': function () {
                debugger;
                var bl = Ext.getCmp("rbwapNo").getValue();
                if (bl) {
                    fnwapHiden();
                }

            }
        }
    });
    new Ext.form.Radio({
        boxLabel: '<font class="rbclass">使用O2OMarkting支付方式</font>',
        inputValue: 'user',
        name: 'radiogroup',
        renderTo: 'rbwapMarketing',
        id: "rbwapMarketing",
        listeners: {
            'change': function () {
                debugger;
                var bl = Ext.getCmp("rbwapMarketing").getValue();
                if (bl) {
                    fnwapHiden();
                }
            }

        }
    });
    new Ext.form.Radio({
        boxLabel: '<font class="rbclass">自定义支付方式</font>',
        inputValue: 'user',
        name: 'radiogroup',
        renderTo: 'rbwapCustom',
        id: "rbwapCustom",
        checked: true,
        listeners: {
            'change': function () {
                debugger;
                var bl = Ext.getCmp("rbwapCustom").getValue();
                if (bl) {
                    fnwapShow();
                }
            }
        }
    });
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
    Ext.create('Jit.form.field.Text', {
        id: "txtwapPrivate",
        text: "",
        renderTo: "txtwapPrivate",
        width: 240
    });
    new Ext.form.Radio({
        boxLabel: '不使用',
        inputValue: 'user',
        name: 'lineradiogroup',
        renderTo: 'rblineNo',
        id: "rblineNo",
        listeners: {
            'change': function () {
                debugger;
                var bl = Ext.getCmp("rblineNo").getValue();
                if (bl) {
                    fnlineHiden();
                }
            }
        }
    });
    new Ext.form.Radio({
        boxLabel: '使用O2OMarkting支付方式',
        inputValue: 'user',
        name: 'lineradiogroup',
        renderTo: 'rblineMarketing',
        id: "rblineMarketing",
        listeners: {
            'change': function () {
                debugger;
                var bl = Ext.getCmp("rblineMarketing").getValue();
                if (bl) {
                    fnlineHiden();
                }
            }

        }
    });
    new Ext.form.Radio({
        boxLabel: '自定义支付方式',
        inputValue: 'user',
        name: 'lineradiogroup',
        renderTo: 'rblineCustom',
        id: "rblineCustom",
        checked: true,
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rblineCustom").getValue();
                if (bl) {
                    fnlineShow();
                }
            }
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtlineBank",
        text: "",
        renderTo: "txtlineBank",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtlinePrivate",
        text: "",
        renderTo: "txtlinePrivate",
        width: 140
    });

    new Ext.form.Radio({
        boxLabel: '不使用',
        inputValue: 'user',
        name: 'Cupradiogroup',
        renderTo: 'rbCupNo',
        id: "rbCupNo",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbCupNo").getValue();
                if (bl) {
                    fnCupHiden();
                }
            }
        }
    });
    new Ext.form.Radio({
        boxLabel: '使用O2OMarkting支付方式',
        inputValue: 'user',
        name: 'Cupradiogroup',
        renderTo: 'rbCupMarketing',
        id: "rbCupMarketing",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbCupMarketing").getValue();
                if (bl) {
                    fnCupHiden();
                }
            }

        }
    });
    new Ext.form.Radio({
        boxLabel: '自定义支付方式',
        inputValue: 'user',
        name: 'Cupradiogroup',
        renderTo: 'rbCupCustom',
        id: "rbCupCustom",
        checked: true,
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbCupCustom").getValue();
                if (bl) {
                    fnCupShow();
                }
            }
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtCupBackID",
        text: "",
        renderTo: "txtCupBackID",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupEnCryption",
        text: "",
        renderTo: "txtCupEnCryption",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupPassword",
        text: "",
        renderTo: "txtCupPassword",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupDecryption",
        text: "",
        renderTo: "txtCupDecryption",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCupDecryptionPassword",
        text: "",
        renderTo: "txtCupDecryptionPassword",
        width: 140
    });


    new Ext.form.Radio({
        boxLabel: '不使用',
        inputValue: 'user',
        name: 'webCupradiogroup',
        renderTo: 'rbwebCupNo',
        id: "rbwebCupNo",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbwebCupNo").getValue();
                if (bl) {
                    fnwebCupHiden();
                }
            }
        }
    });
    new Ext.form.Radio({
        boxLabel: '使用O2OMarkting支付方式',
        inputValue: 'user',
        name: 'webCupradiogroup',
        renderTo: 'rbwebCupMarketing',
        id: "rbwebCupMarketing",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbwebCupMarketing").getValue();
                if (bl) {
                    fnwebCupHiden();
                }
            }

        }
    });
    new Ext.form.Radio({
        boxLabel: '自定义支付方式',
        inputValue: 'user',
        name: 'webCupradiogroup',
        renderTo: 'rbwebCupCustom',
        id: "rbwebCupCustom",
        checked: true,
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbwebCupCustom").getValue();
                if (bl) {
                    fnwebCupShow();
                }
            }
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupBackID",
        text: "",
        renderTo: "txtwebCupBackID",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupEnCryption",
        text: "",
        renderTo: "txtwebCupEnCryption",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupPassword",
        text: "",
        renderTo: "txtwebCupPassword",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupDecryption",
        text: "",
        renderTo: "txtwebCupDecryption",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtwebCupDecryptionPassword",
        text: "",
        renderTo: "txtwebCupDecryptionPassword",
        width: 140
    });

    new Ext.form.Radio({
        boxLabel: '不使用',
        inputValue: 'user',
        name: 'MicroCupradiogroup',
        renderTo: 'rbMicroCupNo',
        id: "rbMicroCupNo",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbMicroCupNo").getValue();
                if (bl) {
                    fnMicroHiden();
                }
            }
        }
    });
    new Ext.form.Radio({
        boxLabel: '使用O2OMarkting支付方式',
        inputValue: 'user',
        name: 'MicroCupradiogroup',
        renderTo: 'rbMicroCupMarketing',
        id: "rbMicroCupMarketing",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbMicroCupMarketing").getValue();
                if (bl) {
                    fnMicroHiden();
                }
            }

        }
    });
    new Ext.form.Radio({
        boxLabel: '自定义支付方式',
        inputValue: 'user',
        checked: true,
        name: 'MicroCupradiogroup',
        renderTo: 'rbMricroCupCustom',
        id: "rbMricroCupCustom",
        listeners: {
            'change': function () {
                var bl = Ext.getCmp("rbMricroCupCustom").getValue();
                if (bl) {
                    fnMicroShow();
                }
            }
        }
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtMicroLendtity",
        text: "",
        renderTo: "txtMicroLendtity",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroPublic",
        text: "",
        renderTo: "txtMricroPublic",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroStoreLendtity",
        text: "",
        renderTo: "txtMricroStoreLendtity",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroStoreCompotence",
        text: "",
        renderTo: "txtMricroStoreCompotence",
        width: 140
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMricroParsword",
        text: "",
        renderTo: "txtMricroParsword",
        width: 140
    });
    Ext.create('Ext.Button', {
        text: '保存',
        width: '100',
        height: '50',
        renderTo: "btnSave",
        cls: 'transparentBut',
        handler: fnSave
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

                $("#absolute1")[0].style.top = "650px";
                $("#absolute2")[0].style.top = "840px";
                // $("#absolute3")[0].style.top = "900px";
                if ($("#spanOpenUpload2")[0].style.display == "none") {
                    $("#absolute3")[0].style.top = "900px";
                }
                else {
                    $("#absolute3")[0].style.top = "955px";
                }
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
                fnUploadFile("fileSpan1", "txt_Attach1", "txtCupDecryption");
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