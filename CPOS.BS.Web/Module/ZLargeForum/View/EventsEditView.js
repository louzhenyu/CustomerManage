function InitView() {

    //editPanel area
    Ext.create('jit.biz.ZForumType', {
        id: "txtForumType",
        text: "",
        renderTo: "txtForumType",
        multiSelect: true,
        width: 100
    });
    Ext.create('jit.biz.ZCourse', {
        id: "txtCourse",
        text: "",
        renderTo: "txtCourse",
        multiSelect: true,
        width: 100
    });

    Ext.create('Jit.form.field.Date', {
        id: "txtStartDate",
        text: "",
        renderTo: "txtStartDate",
        width: 100
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtEndDate",
        text: "",
        renderTo: "txtEndDate",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtStartTime",
        text: "",
        value: "00:00",
        renderTo: "txtStartTime",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEndTime",
        text: "",
        value: "00:00",
        renderTo: "txtEndTime",
        width: 100
    });


    Ext.create('Jit.form.field.Text', {
        id: "txtTitle",
        text: "",
        renderTo: "txtTitle",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtCity",
        text: "",
        renderTo: "txtCity",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtEmail",
        text: "",
        renderTo: "txtEmail",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEmailTitle",
        text: "",
        renderTo: "txtEmailTitle",
        width: 405
    });
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtPhoneNumber",
    //    text: "",
    //    renderTo: "txtPhoneNumber",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtEmail",
    //    text: "",
    //    renderTo: "txtEmail",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtAddress",
    //    text: "",
    //    renderTo: "txtAddress",
    //    width: 405
    //});
    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        //readOnly: true,
        width: 405
    });
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsSignUp",
        text: "",
        renderTo: "txtIsSignUp",
        dataType: "yn",
        width: 100
    });

    //Ext.create('jit.biz.EventCheckinType', {
    //    id: "txtCheckinType",
    //    text: "",
    //    renderTo: "txtCheckinType",
    //    width: 100
    //});
    
    //Ext.create('jit.biz.EventRange', {
    //    id: "txtCheckinRange",
    //    text: "",
    //    renderTo: "txtCheckinRange",
    //    width: 100
    //});
    
    //Ext.create('jit.biz.QuestionnaireType', {
    //    id: "txtApplyQues",
    //    text: "",
    //    renderTo: "txtApplyQues",
    //    width: 100
    //});

    //Ext.create('jit.biz.QuestionnaireType', {
    //    id: "txtPollQues",
    //    text: "",
    //    renderTo: "txtPollQues",
    //    width: 100
    //});
    
    //Ext.create('jit.biz.WeiXinPublic', {
    //    id: "txtWeiXinPublic",
    //    text: "",
    //    renderTo: "txtWeiXinPublic",
    //    width: 100
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtWeiXinPublic",
    //    text: "",
    //    renderTo: "txtWeiXinPublic",
    //    width: 100
    //});
    
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
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content2 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent2',
        renderTo: "txtContent2",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor2 = K.create('#txtContent2', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content3 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent3',
        renderTo: "txtContent3",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor3 = K.create('#txtContent3', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content4 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent4',
        renderTo: "txtContent4",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor4 = K.create('#txtContent4', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content5 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent5',
        renderTo: "txtContent5",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor5 = K.create('#txtContent5', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content6 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent6',
        renderTo: "txtContent6",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor6 = K.create('#txtContent6', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content7 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent7',
        renderTo: "txtContent7",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor7 = K.create('#txtContent7', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content8 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent8',
        renderTo: "txtContent8",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor8 = K.create('#txtContent8', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content9 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent9',
        renderTo: "txtContent9",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor9 = K.create('#txtContent9', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });
    
    var content10 = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent10',
        renderTo: "txtContent10",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor10 = K.create('#txtContent10', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
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
        }]
    });
}