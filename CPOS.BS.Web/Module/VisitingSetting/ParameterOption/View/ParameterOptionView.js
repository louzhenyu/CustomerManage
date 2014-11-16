function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jittextfield",
            name: "OptionName",
            fieldLabel: "名称",
            jitSize: 'small'
        }
           ],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: 'span_panel2',
        items: [{
            xtype: "jitbutton",
            imgName: 'search',
            hidden: __getHidden("search"),
            handler: fnSearch,
            isImgFirst: true
        }, {
            xtype: "jitbutton",
            imgName: 'reset',
            hidden: __getHidden("search"),
            handler: fnReset
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });
    //operator area
    Ext.create('Jit.button.Button', {
        isImgFirst: true,
        imgName: "create",
        renderTo: "span_create",
        hidden: __getHidden("create"),
        handler: fnCreate
    });
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("optionsListStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'OptionName',
            align: 'left',
            hideable: false,
            hidden: __getHidden("delete"),
            renderer: fnColumnDelete
        }, {
            text: '名称',
            width: 110,
            sortable: true,
            dataIndex: 'OptionName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '选项个数',
            width: 110,
            sortable: true,
            dataIndex: 'OptionCount',
            align: 'left'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("optionsListStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView",
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });

    //edit window
    Ext.grid.RowEditor.prototype.saveBtnText = "保&nbsp;&nbsp;存";
    Ext.grid.RowEditor.prototype.cancelBtnText = '取&nbsp;&nbsp;消';
   
    var rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
        clicksToMoveEditor: 1,
        errorSummary: false,
        autoCancel: false
    });
    Ext.create('Ext.grid.Panel', {
        layout: 'fit',
        height: 240,
        width: "100%",
        id: "optionGrid",
        store: Ext.getStore("optionsEditStore"),
        columns: [{
            text: "排序",
            sortable: false,
            flex: 1,
            hideable: false,
            dataIndex: "Sequence",
            editor: {
                allowBlank: true
            }
        }, {
            text: "文本",
            sortable: false,
            flex: 1,
            hideable: false,
            dataIndex: "OptionText",
            editor: {
                allowBlank: false
            }
        }, {
            text: "值",
            sortable: false,
            flex: 1,
            hideable: false,
            dataIndex: "OptionValue",
            editor: {
                allowBlank: false,
                vtype: "positiveInteger"
            }
        }],
        tbar: [
            {
                xtype: "jitbutton",
                text: "添加",
                name: "create",
                handler: function () {
                    rowEditing.cancelEdit();

                    // Create a model instance
                    var r = Ext.create('OptionsEntity', {
                        OptionText: '',
                        OptionValue: valuei,
                        Sequence: valuej
                    });

                    Ext.getStore("optionsEditStore").insert(Ext.getStore("optionsEditStore").getCount(), r);
                    rowEditing.startEdit(Ext.getStore("optionsEditStore").getCount() - 1, 0);

                    valuei++;
                    valuej++;
                }
            },
            {
                xtype: "jitbutton",
                text: "修改",
                name: "update",
                handler: function () {
                    //rowEditing.cancelEdit();
                    var records = Ext.getCmp("optionGrid").getSelectionModel().getSelection();
                    if (records.length == 1) {
                        rowEditing.startEdit(Ext.getStore("optionsEditStore").indexOf(records[0]), 0);
                    }
                }
            },
           {
               xtype: "jitbutton",
               text: "删除",
               name: "delete",
               handler: function () {
                   var sm = Ext.getCmp("optionGrid").getSelectionModel();
                   rowEditing.cancelEdit();
                   Ext.getStore("optionsEditStore").remove(sm.getSelection());
                   if (Ext.getStore("optionsEditStore").getCount() > 0) {
                       sm.select(Ext.getStore("optionsEditStore").getCount() - 1);
                   }

                   valuei--;
                   valuej--;
               }
           }
        ],
        plugins: [rowEditing],
        listeners: {
            'selectionchange': function (view, records) {
                //w_gridView.down('#removeEmployee').setDisabled(!records.length);
            }
        }
    });


    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        autoScroll: true,
        columnWidth: 200,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'anchor',
        items: [{
            columnWidth: 1,
            layout: "column",
            border: 0,
            items: [{
                xtype: "jittextfield",
                fieldLabel: "名称",
                id: "OptionName",
                name: "OptionName",
                allowBlank: false
            }]
        },
            Ext.getCmp("optionGrid")
        ]
    });

    Ext.create('Ext.window.Window', {
        height: 350,
        id: "editWin",
        title: '选项编辑',
        width: 400,
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("editPanel")],
        border: 0,
        buttons: ['->', {
            xtype: "jitbutton",
            id: "btnSave",
            handler: fnSubmit,
            isImgFirst: true,
            imgName: "save"
        }, {
            xtype: "jitbutton",
            handler: fnCancel,
            imgName: "cancel"
        }],
        closeAction: 'hide'
    });

}