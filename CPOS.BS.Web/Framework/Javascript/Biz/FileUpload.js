
Ext.define('Jit.biz.FileUpload', {
    alias: 'widget.jitbizfileupload',
    constructor: function (args) {

        if (args.width == null) { args.width = 183; }
        var me = this;
        Jit.form.field.Text.override({
            extractFileInput: function () {
                //提交数据时 需要传入的上传控件的对象 所以这里给的是me
                var mes = this, fileInput = mes.isFileUpload() ? mes : null;
                return me.fnFileInput(fileInput);
            }
        });

        me.fnFileInput = function (fileInput) {
            var me = fileInput,
            fileInput = me.isFileUpload() ? me.inputEl.dom : null,
            clone;
            if (fileInput) {
                clone = fileInput.cloneNode(true);
                try {
                    //fileInput.parentNode.replaceChild(clone, fileInput);
                    //因为parentNode有时候找不到 这里用jquery强制替换
                    $("#file-pathID-inputEl").replaceWith(clone);
                }
                catch (e) {
                    $("#file-pathID-inputEl").replaceWith(clone);
                }
                me.inputEl = Ext.get(clone);
            }
            return fileInput;
        }

        var defaultConfig = {
            id: '__FileUploadID'   //panel 的id 默认为mapSelect
            , fieldLabel: '文件上传'  //textField 的fieldLabel  
            , renderTo: null //panel的renderTo
            , width: args.width
            , uploadTitle: '上传数据'
            , ajaxPath: ''
            , photoType: 1
        }
        args = Ext.applyIf(args, defaultConfig);
        //下载按钮
        me.buttonDown = Ext.create('Jit.button.Button', {
            text: '下载模板'
            , margin: '10 0 10 10'
            , height: 22
            , width: 80
            , jitIsHighlight: false
            , jitIsDefaultCSS: true
            , border: 0
            , handler: function () {
                window.open(args.ajaxPath + "&btncode=import&method=GetImportFile");
            }
        });

        //上传文件文本框
        me.textFieldUpdate = Ext.create('Jit.form.field.Text', {
            id: "file-pathID",
            fieldLabel: args.fieldLabel
            , jitSize: 'small'
            , name: 'file-path'
            , margin: '10 0 20 10'
            , inputType: 'file'
            , width: 250
        });


        //上传的方法
        me.fnFileUpdate = function () {
            var pFileName = "Store";
            if (args.photoType == 1) {
                pFileName = "Store";
            } else if (args.photoType == 2) {
                pFileName = "Dis";
            } else if (args.photoType == 3) {
                pFileName = "Sku";
            } else if (args.photoType == 4) {
                pFileName = "User";
            } else if (args.photoType == 5) {
                pFileName = "Dep";
            }
            var myMask_info = "上传中...";
            var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
            myMask.show();
            me.fileUpdatePanel.getForm().submit({
                url: args.ajaxPath + "&btncode=import&method=import&pFileName=" + pFileName, // 后台处理的页面             
                success: function (fp, o) {
                    myMask.hide();
                    try {
                        if (o.result.oimport) {
                            if (o.result.o) {
                                Ext.Msg.show({
                                    title: "提示",
                                    msg: "导入成功,请刷新页面查看",
                                    minWidth: 200,
                                    modal: true,
                                    icon: Ext.Msg.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function () {
                                        //me.photoWindow.hide();
                                        document.getElementById("__fileUpdateID").innerHTML = "处理结果：导入成功;";

                                    }
                                });
                            }
                            else {
                                Ext.Msg.show({
                                    title: "提示",
                                    msg: "导入失败",
                                    minWidth: 200,
                                    modal: true,
                                    icon: Ext.Msg.INFO,
                                    buttons: Ext.Msg.OK,
                                    fn: function () {
                                        document.getElementById("__fileUpdateID").innerHTML = "处理结果：导入失败<br/><a   color='blue'  href='" + o.result.msg + "'><font color='blue'>下载导入结果</font></a>"
                                    }
                                });
                                me.textFieldUpdate.jitSetValue("");
                            }
                        } else {
                            Ext.Msg.show({
                                title: "提示",
                                msg: "导入失败",
                                minWidth: 200,
                                modal: true,
                                icon: Ext.Msg.INFO,
                                buttons: Ext.Msg.OK,
                                fn: function () {
                                    document.getElementById("__fileUpdateID").innerHTML = "处理结果：导入失败;请查看导入模板<br/>" + o.result.msg
                                }
                            });
                            me.textFieldUpdate.jitSetValue("");
                        }
                    } catch (e) {
                        myMask.hide();
                    }
                },
                failure: function (fp, o) {
                    myMask.hide();
                    Ext.Msg.show({
                        title: "提示",
                        msg: "上传失败",
                        minWidth: 200,
                        modal: true,
                        icon: Ext.Msg.INFO,
                        buttons: Ext.Msg.OK
                    });
                }
            });
        }
        //下载模板的panel
        me.dwonPanel = Ext.create('Ext.form.Panel', {
            width: 326,
            height: 45,
            margin: '0 0 0 0',
            columnWidth: 1,
            items: [me.buttonDown],
            layout: 'column',
            border: 0
        });
        //上传Excel的panel
        me.filePanel = Ext.create('Ext.form.Panel', {
            width: 326,
            height: 50,
            margin: '0 0 0 0',
            columnWidth: 1,
            items: [me.textFieldUpdate],
            layout: 'column',
            border: 0
        });

        //返回的处理结果
        me.filePanelDiv = Ext.create('Ext.form.Panel', {
            width: 326,
            height: 60,
            margin: '0 0 0 0',
            columnWidth: 1,
            html: '<div id="__fileUpdateID" style="width:326px;height:100px;padding:0 10px">处理结果:</div>',
            layout: 'column',
            border: 0
        });

        //上传的formPanel
        me.fileUpdatePanel = Ext.create('Ext.form.Panel', {
            width: 326,
            height: 210,
            margin: '0 0 0 0',
            columnWidth: 1,
            items: [me.dwonPanel, me.filePanel, me.filePanelDiv],
            layout: 'column',
            border: 0
        });

        //上传图片的window
        var instance = Ext.create('Jit.window.Window', {
            id: args.id,
            title: args.uploadTitle,
            items: [me.fileUpdatePanel],
            width: 336,
            height: 220,
            buttons: ['->', {
                xtype: "jitbutton",
                imgName: 'save',
                isImgFirst: true,
                id: "btnSave",
                handler: function () {
                    var fileName = me.textFieldUpdate.jitGetValue();
                    if (fileName == null || fileName == "") {
                        Ext.Msg.show({
                            title: "提示",
                            msg: "请选择上传文件",
                            minWidth: 200,
                            modal: true,
                            icon: Ext.Msg.INFO,
                            buttons: Ext.Msg.OK,
                            fn: function () {
                                return;
                            }
                        });
                    }
                    else {
                        var filesuffix = fileName.substring(fileName.lastIndexOf("."));
                        if (filesuffix.toLowerCase() == ".xls" || filesuffix.toLowerCase() == ".xlsx") {
                            Ext.MessageBox.buttonText.yes = "是";
                            Ext.MessageBox.buttonText.no = "否";
                            Ext.MessageBox.confirm('提示信息', '是否确定导入数据?',
                                                     function deldbconfig(btn) {
                                                         if (btn == 'yes') {
                                                             me.fnFileUpdate();
                                                         }
                                                     });
                        } else {
                            Ext.Msg.show({
                                title: "提示",
                                msg: "请选择上传Excel文件",
                                minWidth: 200,
                                modal: true,
                                icon: Ext.Msg.INFO,
                                buttons: Ext.Msg.OK,
                                fn: function () {
                                    return;
                                }
                            });
                        }
                    }
                }
            }, {
                xtype: "jitbutton",
                imgName: 'cancel',
                handler: function () {
                    Ext.getCmp(args.id).hide();
                }
            }],
            jitSize: "custom",
            constrain: true,
            modal: true
        });
        instance.jitClreaValue = function () {
            if (document.getElementById("__fileUpdateID") != null) {
                document.getElementById("__fileUpdateID").innerHTML = "处理结果";
            }
            me.textFieldUpdate.jitSetValue("");
        }
        return instance;
    }
});