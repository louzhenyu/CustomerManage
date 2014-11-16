function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizoptions",
            fieldLabel: "对象分组",
            OptionName: 'ObjectGroup',
            name: "ObjectGroup",
            isDefault: true
        }, {
            xtype: "jittextfield",
            id: "txtObjectName",
            name: "ObjectName",
            fieldLabel: "对象名称",
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
                isImgFirst:true
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
        imgName: 'create',
        renderTo: "span_create",
        hidden: __getHidden("create"),
        isImgFirst: true,
        handler: fnCreate
    });
    //list area

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("list_objectStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'VisitingParameterID',
            align: 'left',
            hidden: __getHidden("delete"),
            hideable: false,
            renderer: fnColumnDelete
        }, {
            text: '对象名称',
            width: 110,
            sortable: true,
            dataIndex: 'ObjectName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '对象分组',
            width: 110,
            sortable: true,
            dataIndex: 'ObjectGroupText',
            align: 'left'
        }, {
            text: '顺序',
            width: 110,
            sortable: true,
            dataIndex: 'Sequence',
            align: 'left'
        }, {
            text: '状态',
            width: 110,
            sortable: true,
            dataIndex: 'Status',
            align: 'left',
            renderer: fnRenderStatus
        }, {
            text: '备注',
            width: 110,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("list_objectStore"),
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


    //window
    Ext.widget('tabpanel', {
        width: 450,
        id: "tabs",
        activeTab: 0,
        items: [{
            title: '基本信息',
            contentEl: "tab1"
        }, {
            title: '采集参数',
            contentEl: "tab2",
            listeners:
            {
                activate: function () {
                    if (!tab2State) {
                        tab2State = true;
                        document.getElementById("tab2").src = document.getElementById("tab2").src;
                    }
                }
            }
        }]
    });

    Ext.create('Jit.window.Window', {
        height: 550,
        width: 910,
        jitSize: "large",
        id: "objectEditWin",

        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("tabs")],
        border: 0,
        closeAction: 'hide'
    });
}