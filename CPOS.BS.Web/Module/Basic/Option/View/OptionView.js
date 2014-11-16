function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jittextfield",
            id: "Txt_Title",
            name: "Title",
            fieldLabel: "标题",
            jitSize: 'small'
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        hidden: __getHidden("search"),
        items: [{
            xtype: "jitbutton",
            text: "",
            imgName: 'search',
            hidden: __getHidden("search"),
            isImgFirst: true,
            handler: fnSearch
        }, {
            xtype: "jitbutton",
            text: "",
            imgName: 'reset',
            hidden: __getHidden("search"),
            handler: fnReset
        }],
        renderTo: 'span_panel2',
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

   //create
    Ext.create('Jit.button.Button', {
        text: "添&nbsp&nbsp;加",
        id:'btn_create',
        renderTo: "span_create",
        jitIsHighlight: true,
        jitIsDefaultCSS: true,
        hidden: __getHidden("create"),
        handler: fnCreate
    });



    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("optionsDefinedStore"),
        id: "gridView",
        columnLines: true,
        columns: [
           { xtype: 'jitcolumn',
               jitDataType: 'String',
               text: '操作',
               width: JITPage.Layout.OperateWidth,
               sortable: false,
               dataIndex: 'DefinedID',
               align: 'left',
               renderer: fnColumnDelete,
               hideable: false
           }, {
               xtype: 'jitcolumn',
               jitDataType: 'String',
               text: '标题',
               width: 130,
               sortable: false,
               dataIndex: 'Title',
               align: 'left',
               renderer: fnColumnUpdate
           }, {
               xtype: 'jitcolumn',
               jitDataType: 'String',
               text: '名称',
               width: 130,
               sortable: false,
               dataIndex: 'OptionName',
               align: 'left'
           }],
        height: 450,

        stripeRows: true,
        width: '100%',
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("optionsDefinedStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }

    });


//list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("fixedoptionsDefinedStore"),
        id: "fixedgridView",
        columnLines: true,
        columns: [
      {
          xtype: 'jitcolumn',
          text: '标题',
          jitDataType: 'String',
          width: 130,
          sortable: false,
          dataIndex: 'Title',
          align: 'left',
          renderer: fnColumnUpdate
      }, {
          xtype: 'jitcolumn',
          jitDataType: 'String',
          text: '名称',
          width: 130,
          sortable: false,
          dataIndex: 'OptionName',
          align: 'left'
      }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "fixedpageBar",
            defaultType: 'button',
            store: Ext.getStore("fixedoptionsDefinedStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }

    });


    Ext.create("Ext.tab.Panel", {
        id: "tabpanel",
        name: "tabpanel",
        renderTo: "DivGridView",
        minTabWidth: 80,
        activeTab: 0,
        width: "100%",
        height: 480,
        plain: false,
        bodyPadding: 2,
        autoScroll: false,
        items: [{
            id: "fixedPanel",
            title: "基础固定数据",
            items: [Ext.getCmp("fixedgridView")],
            border: 0,
            listeners: {
                activate: function (tab) {
                    pOptionType = 2;
                    Ext.getCmp("btn_create").setDisabled(true);
                }
            },
            width: "100%",
            height: 480
        }, {
            id: "activePanel",
            title: "基础动态数据",
            items: [Ext.getCmp("gridView")],
            border: 0,
            listeners: {
                activate: function (tab) {
                    pOptionType = 3;
                    Ext.getCmp("btn_create").setDisabled(false);
                }
            },
            width: "100%",
            height: 480
        }]
    });
    //edit window
    var rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
        clicksToMoveEditor: 1,
        autoCancel: false
    });
    var cellEditing = Ext.create('Ext.grid.plugin.CellEditing', {
        clicksToEdit: 1,
        listeners: {
            "beforeedit": function (a, b, c) {
                if (b.record.data.OptionText != null && b.record.data.OptionText != "") {
                    return true;
                }
                return false;           
            }
        }
    });
    Ext.create('Ext.grid.Panel', {
        layout: 'fit',
        height: 240,
        width: 435,
        id: "optionGrid",
        store: Ext.getStore("optionsEditStore"),
        columns: [{

            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: false,
            dataIndex: 'OptionText',
            align: 'left',
            hidden:(__getHidden("update")&& __getHidden("create")),
            renderer: fnColumnOptionDel,
            hideable: false
        }, {
            xtype: 'jitcolumn',
            jitDataType: 'String',
            text: "文本",
            sortable: false,
            flex: 1,
            hideable: false,
            dataIndex: "OptionText",
            editor: {
                allowBlank: false
            }
        }, {
            xtype: 'jitcolumn',
            jitDataType: 'Int',
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
        plugins: [cellEditing],
        listeners: {
            'selectionchange': function (view, records) {
            }
        }
    });


    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        columnWidth: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px;',
        layout: 'column',
        items: [{
            columnWidth: 1,
            layout: "column",
            border: 0,
            items: [{
                columnWidth: 0.5,
                layout: 'column',
                border: 0,
                items: [{
                    xtype: "jittextfield",
                    fieldLabel: "标题",
                    id: "Title",
                    name: "Title",
                    maxLength:50,
                    allowBlank: false
                }]
            }, {
                columnWidth: 0.5,
                layout: 'column',
                border: 0,
                items: [{
                    xtype: "jittextfield",
                    fieldLabel: "名称",
                    id: "OptionName",
                    name: "OptionName",
                    maxLength: 50,
                    allowBlank: false
                }]
            },{
                columnWidth: 1,
                layout: 'column',
                border: 0,
                items: [Ext.getCmp("optionGrid")]
            }]
        }]
    });

    Ext.create('Ext.window.Window', {
        height: 350,
        id: "editWin",
        title: '选项编辑',
        width: 450,
        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("editPanel")],
        border: 0,
        buttons: ['->', {
            xtype: "jitbutton",
            text: '',
            id: "btnSave",
            imgName: 'save',
            isImgFirst:true,
            handler: fnSubmit
        }, {
            xtype: "jitbutton",
            text: '',
            imgName: 'cancel',
            handler: fnCancel
        }],
        closeAction: 'hide'
    });
     
}