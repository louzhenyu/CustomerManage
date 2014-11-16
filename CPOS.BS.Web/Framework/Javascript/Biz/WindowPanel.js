
//WindowPanel业务控件
Ext.define('Jit.biz.WindowPanel', {
    alias: 'widget.jitbizwindowpanel',
    constructor: function (args) {
        //默认值
        var argsConfig = {
            width: 900, fieldItems: null, title: '基本信息', buttonSave: function () { alert("请传入buttonSave方法") },
            fieldCount: 4, id: '__WindowPanelID', buttonHidden: true
        };
        var me = this;

        //操作区域的Panel
        me.FieldPanel = Ext.create("Ext.form.Panel", {
            items: args.fieldItems,
            width: args.width,
            margin: '10 0 0 0',
            style: '',
            layout: 'column',
            border: 0
        });

        //操作按钮的panel
        me.ButtonPanel = Ext.create('Ext.form.Panel', {
            width: '100%',
            items: [{
                xtype: "jitbutton",
                id: "__wp_btn_1",
                text: "编&nbsp;&nbsp;辑",
                jitIsHighlight: true,
                jitIsDefaultCSS: true,
                hidden: args.buttonHidden,
                margin: '0 0 10 10',
                handler: function () {
                    if (me.FieldPanel.items.items != null && me.FieldPanel.items.items.length > 0) {
                        for (var i = 0; i < me.FieldPanel.items.items.length; i++) {
                            me.FieldPanel.items.items[i].setVisible(true);
                            try {
                                me.FieldPanel.items.items[i].setReadOnly(false);
                            } catch (e) { }
                        }
                    }
                    Ext.getCmp("__wp_btn_1").setVisible(false);
                    Ext.getCmp("__wp_btn_2").setVisible(true);
                    Ext.getCmp("__wp_btn_3").setVisible(true);
                    Ext.getCmp("__wp_btn_4").setVisible(false);
                    Ext.getCmp("__wp_btn_5").setVisible(false);
                }
            }, {
                xtype: "jitbutton",
                id: "__wp_btn_2",
                text: "保&nbsp;&nbsp;存",
                hidden: true,
                jitIsHighlight: true,
                jitIsDefaultCSS: true,
                margin: '0 0 10 10',
                handler: function () {
                    args.buttonSave();
                }
            }, {
                xtype: "jitbutton",
                id: "__wp_btn_3",
                text: "取&nbsp;&nbsp;消",
                hidden: true,
                jitIsHighlight: false,
                jitIsDefaultCSS: true,
                margin: '0 10 10 10',
                handler: function () {
                    if (me.FieldPanel.items.items != null && me.FieldPanel.items.items.length > 0) {
                        for (var i = 0; i < me.FieldPanel.items.items.length; i++) {
                            if (i > (args.fieldCount - 1)) {
                                me.FieldPanel.items.items[i].setVisible(false);
                            }
                            try {
                                me.FieldPanel.items.items[i].setReadOnly(true);
                            } catch (e) { }
                        }
                    }
                    Ext.getCmp("__wp_btn_1").setVisible(true);
                    Ext.getCmp("__wp_btn_2").setVisible(false);
                    Ext.getCmp("__wp_btn_3").setVisible(false);
                    Ext.getCmp("__wp_btn_4").setVisible(true);
                    Ext.getCmp("__wp_btn_5").setVisible(false);
                }
            }, {
                xtype: "jitbutton",
                id: "__wp_btn_4",
                text: "更&nbsp;&nbsp;多",
                jitIsHighlight: false,
                jitIsDefaultCSS: true,
                margin: '0 10 10 10',
                handler: function () {
                    Ext.getCmp("__wp_btn_5").setVisible(true);
                    Ext.getCmp("__wp_btn_4").setVisible(false);
                    if (me.FieldPanel.items.items != null && me.FieldPanel.items.items.length > 0) {
                        for (var i = 0; i < me.FieldPanel.items.items.length; i++) {
                            me.FieldPanel.items.items[i].setVisible(true);
                            try {
                                me.FieldPanel.items.items[i].setReadOnly(true);
                            } catch (e) { }
                        }
                    }
                }
            }, {
                xtype: "jitbutton",
                id: "__wp_btn_5",
                text: "收&nbsp;&nbsp;起",
                hidden: true,
                jitIsHighlight: false,
                jitIsDefaultCSS: true,
                margin: '0 10 10 10',
                handler: function () {
                    Ext.getCmp("__wp_btn_4").setVisible(true);
                    Ext.getCmp("__wp_btn_5").setVisible(false);
                    if (me.FieldPanel.items.items != null && me.FieldPanel.items.items.length > 0) {
                        for (var i = 0; i < me.FieldPanel.items.items.length; i++) {
                            if (i > (args.fieldCount - 1)) {
                                me.FieldPanel.items.items[i].setVisible(false);
                            }
                            try {
                                me.FieldPanel.items.items[i].setReadOnly(true);
                            } catch (e) { }
                        }
                    }
                }
            }],

            margin: '10 0 0 0',
            height: 34,
            width: args.width,
            style: 'border-bottom:1px solid #C2C3C8;background:#ffffff',
            layout: 'column',
            border: 0
        });

        //定义的Panel
        var instance = Ext.create("Ext.form.Panel", {
            id: args.id,
            items: [me.FieldPanel, me.ButtonPanel],
            title: args.title,
            width: args.width,
            margin: '0 0 0 0',
            style: '',
            layout: 'column',
            border: 0
        });

        //获取FormPanel
        instance.jitGetForm = function () {
            return me.FieldPanel;
        }

        //初始化的时候处理
        if (me.FieldPanel.items.items != null && me.FieldPanel.items.items.length > 0) {
            for (var i = 0; i < me.FieldPanel.items.items.length; i++) {
                if (i > (args.fieldCount - 1)) {
                    me.FieldPanel.items.items[i].setVisible(false);
                }
                try {
                    me.FieldPanel.items.items[i].setReadOnly(true);
                } catch (e) { }
            }
        }
        return instance;
    }
})
