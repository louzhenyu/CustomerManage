function __InitMessageSendView() {
    //查询面板生成区域
    btnSend = Ext.create('Jit.button.Button', {
        xtype: "jitbutton",
        text: "发&nbsp;&nbsp;送",
        jitIsHighlight: true,
        jitIsDefaultCSS: true,
        handler: fnSubmitMessage
    });

    btnTestSend = Ext.create('Jit.button.Button', {
        xtype: "jitbutton",
        text: "测试发送",
        jitIsHighlight: false,
        jitIsDefaultCSS: true,
        handler: fnSendTestMessage
    });

    btnAuthLink = Ext.create('Jit.button.Button', {
        xtype: "jitbutton",
        text: "插入认证资料链接",
        width: 120,
        jitIsHighlight: false,
        jitIsDefaultCSS: false,
        handler: fnAuthLink
    });

    btnFeesLink = Ext.create('Jit.button.Button', {
        xtype: "jitbutton",
        text: "插入交会费链接",
        width: 110,
        jitIsHighlight: false,
        jitIsDefaultCSS: false,
        handler: fnFeesLink
    });

    //操作区域
    pnlWork = Ext.create('Ext.panel.Panel', {
        renderTo: 'dvWork',
        items: [btnSend, btnTestSend],
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    /**
    * 信息内容
    */
    var content = new Jit.form.field.TextArea({
        fieldLabel: "<font color='red'>*</font>邮箱内容",
        id: '__MessageContent',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#__MessageContent-inputEl', {
                    resizeType: 1,
                    uploadJson: "/Framework/Javascript/Other/editor/EditorFileHandler.ashx?mid=&btncode=&method=EditorFile&FileUrl=SendMessage",
                    allowFileManager: true
                });
            }
        }
    });

    Ext.create('Ext.form.Panel', {
        id: "__sendMessagePanel",
        width: "100%",
        height: 390,
        border: 1,
        margin: '0 0 10 0',
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>发送类型",
            id: "__TemplateType",
            name: "TemplateType",
            hidden: true,
            OptionName: 'TemplateType'
        }, {
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jittextfield",
                fieldLabel: "<font color='red'>*</font>收箱人",
                name: "MessageAddress",
                id: "__MessageAddress",
                readOnly: false,
                maxLength: 300
            }, {
                xtype: "jitdisplayfield",
                id: 'txtUserCount'
            }]
        }, {
            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>邮箱模版",
            store: Ext.getStore("typeStore"),
            displayField: 'name',
            valueField: 'value',
            hidden: true,
            name: "MessageTemplete",
            id: "__MessageTemplete",
            emptyText: "--请选择--"
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>邮箱标题",
            name: "MessageTitle",
            id: "__MessageTitle",
            maxLength: 300,
            value: '注册通知'
        }, {
            layout: 'column',
            border: 0,
            height: 350,
            defaults: {},
            bodyStyle: 'background:#F1F2F5;',
            items: [content, {
                layout: 'anchor',
                border: 0,
                bodyStyle: 'background:#F1F2F5;margin-left:83px',
                defaults: {},
                items: [btnAuthLink, btnFeesLink]
            }]
        }],
        renderTo: 'basic_panel'
    });

    Ext.create('Ext.form.Panel', {
        id: "__sendTestMessagePanel",
        width: "100%",
        height: 390,
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>邮箱地址",
            name: "TestMessageAddress",
            id: "__TestMessageAddress",
            maxLength: 300,
            vtype: "email"
        }]
    });

    Ext.create('Jit.window.Window', {
        height: 110,
        width: 300,
        id: "__sendTestMessageWin",
        title: '测试邮箱发送',
        jitSize: 'small',
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("__sendTestMessagePanel")],
        border: 0,
        modal: false,
        buttons: ['->', {
            xtype: "jitbutton",
            text: "发&nbsp;&nbsp;送",
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: fnSubmitTestMessage
        }],
        closeAction: 'hide'
    });
}