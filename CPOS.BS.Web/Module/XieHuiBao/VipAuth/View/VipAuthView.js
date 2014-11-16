var checkBoxModel = Ext.create('Ext.selection.CheckboxModel', {
    mode: 'MULTI',
    checkOnly: true,
    onRowMouseDown: function (view, record, item, index, e) {
        view.el.focus();
        var me = this,
                checker = e.getTarget('.' + Ext.baseCSSPrefix + 'grid-row-checker'),
                mode;
        if (!me.allowRightMouseSelection(e)) {
            return;
        }
        if (me.checkOnly && !checker) {
            return;
        }
        //checker 为true的情况为选择checkBox按钮选择的结果，不然为选择行的结果
        checker = true;
        if (checker || this.rowSelect) {
            mode = me.getSelectionMode();
            if (mode !== 'SINGLE') {
                me.setSelectionMode('SIMPLE');
            }
            me.selectWithEvent(record, e);
            me.setSelectionMode(mode);
        }
    },
    listeners: {
        'deselect': function (a, b, c) {
            for (var i = 0; i < vipChecked.length; i++) {
                if (b.data.VIPID == vipChecked[i].VIPID) {
                    vipChecked.splice(i, 1);
                }
            }
        },
        "select": function (a, b, c) {
            var hasId = false;
            for (var i = 0; i < vipChecked.length; i++) {
                if (b.data.VIPID == vipChecked[i].VIPID) {
                    hasId = true;
                }
            }
            if (!hasId) {
                vipChecked.push(b.data);
            }
        }
    }
});
function InitView() {
    /*创建查询区域*/
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jittextfield",
            id: "txtVipName",
            name: "VipName",
            fieldLabel: "姓名",
            jitSize: 'small'
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "状态",
            OptionName: 'VipStatus',
            name: 'serchObjectType',
            jitSize: 'StatusType',
            isDefault: true,
            id: 'cmbStatusType'
        }],
        renderTo: 'search_form_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    /*查询按钮区域*/
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        items: [{
            xtype: "jitbutton",
            height: 22,
            isImgFirst: true,
            text: "查询",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            hidden: __getHidden("search"),
            handler: fnSearch
        }, {
            xtype: "jitbutton",
            height: 22,
            text: "重置",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: function () {
                Ext.getCmp("searchPanel").getForm().reset();
            }
        }, {
            xtype: "jitbutton",
            height: 22,
            text: "群发消息",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnShowSendMessageWin
        }, {
            xtype: "jitbutton",
            height: 22,
            text: "导出",
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnSearchExcel
        }],
        renderTo: 'search_button_panel',
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    /*列表分页*/
    new Ext.PagingToolbar({
        id: "pageBar",
        displayInfo: true,
        defaultType: 'button',
        store: Ext.getStore("vipList"),
        pageSize: JITPage.HandlerUrl.getValue()
    });

    /*列表*/
    Ext.create('Ext.grid.Panel', {
        id: "gridlist",

        store: Ext.getStore("vipList"),
        columns: eval("[]"),
        height: 500,
        stripeRows: true,
        columnLines: true,
        selModel: checkBoxModel,
        width: "100%",
        bbar: Ext.getCmp("pageBar"),
        renderTo: 'dvGrid',
        listeners: {
            'afterlayout': function () {
                $('a[rel=fancybox_group]').fancybox({
                    openEffect: 'none',
                    closeEffect: 'none',
                    prevEffect: 'none',
                    nextEffect: 'none',
                    closeBtn: true
                });
            }
        },
        viewConfig: {
            loadMask: true
        }
    });

    /*明细面板*/
    Ext.create('Ext.panel.Panel', {
        id: 'showPanel',
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'column',
        border: 0,
        autoScroll: true
    });

    /*创建弹出窗体*/
    Ext.create('Jit.window.Window', {
        jitSize: "large",
        title: '会员明细',
        id: "editWin",
        items: [Ext.getCmp("showPanel")],
        border: 1,
        buttons: ['->', {
            xtype: "jitbutton",
            text: "保 存",
            jitIsHighlight: true,
            jitIsDefaultCSS: true,
            handler: fnSubmit
        }, {
            xtype: "jitbutton",
            text: "通&nbsp;&nbsp;过",
            id: 'btnApprove',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnApprove
        }, {
            xtype: "jitbutton",
            text: "不&nbsp;&nbsp;通&nbsp;&nbsp;过",
            id: 'btnNoApprove',
            jitIsHighlight: false,
            jitIsDefaultCSS: true,
            handler: fnNoApprove
        }],
        closeAction: 'hide'
    });

    Ext.create('Ext.form.Panel', {
        id: "operationPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        items: [{
            xtype: "jittextarea",
            fieldLabel: "<font color='red'>*</font>备注",
            name: "Remark",
            id: "remark",
            maxLength: 300
        }]
    });

    Ext.create('Jit.window.Window', {
        height: 250,
        width: 550,
        id: "operationWin",
        title: '审核操作',
        jitSize: 'big',
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("operationPanel")],
        border: 0,
        modal: false,
        buttons: ['->', {
            xtype: "jitbutton",
            id: "btnSave",
            handler: fnOperationSubmit,
            isImgFirst: true,
            imgName: "save"
        }, {
            xtype: "jitbutton",
            handler: function () {
                Ext.getCmp("operationWin").hide();
            },
            imgName: "cancel"
        }],
        closeAction: 'hide'
    });

    /*群发消息窗口*/
    Ext.create('Jit.window.Window', {
        id: 'sendMessageWin',
        title: '群发消息',
        width: 600,
        height: 500,
        modal: true,
        border: 1,
        html: '<iframe id="framemessage" name="frame" frameborder="no" height="100%" width="100%" marginheight="0" marginwidth="0" scrolling="auto"  src=""/>'
    });
}