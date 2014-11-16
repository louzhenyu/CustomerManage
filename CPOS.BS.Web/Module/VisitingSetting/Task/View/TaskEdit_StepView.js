function InitView() {
    Ext.create('Jit.button.Button', {
        renderTo: "span_create",
        handler: fnCreate,
        id: "btnCreate",
        imgName: 'create',
        isImgFirst: true
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("stepStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: JITPage.Layout.OperateWidth,
            sortable: true,
            dataIndex: 'VisitingTaskStepID',
            align: 'left',
            hideable: false,
            hidden: __getHidden("update"),
            renderer: renderer.Delete
        }, {
            text: '任务名称',
            width: 110,
            sortable: true,
            dataIndex: 'VisitingTaskName',
            align: 'left'
        }, {
            text: '步骤名称',
            width: 110,
            sortable: true,
            dataIndex: 'StepName',
            align: 'left',
            renderer: renderer.Update
        }, {
            xtype: 'jitcolumn',
            jitDataType: 'int',
            text: '显示顺序',
            width: 110,
            sortable: true,
            dataIndex: 'StepPriority',
            align: 'left'
        }, {
            text: '必做',
            width: 110,
            sortable: true,
            dataIndex: 'IsMustDo',
            align: 'left',
            renderer: fnColumnMust
        }, {
            text: '对象类型',
            width: 110,
            sortable: true,
            dataIndex: 'StepTypeText',
            align: 'left'
        }, {
            text: '备注',
            width: 310,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }],
        height: 500,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("stepStore"),
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
            contentEl: "tab1",
            //2013.5.16 by zhongbao.xiao 设置弹出窗体的Title
            listeners: {
                activate: function () {
                    handler: fnSetWinTitle("0");
                }
            }
        }, {
            title: '拜访对象',
            contentEl: "tab2",
            listeners:
            {
                activate: function () {
                    if (!tab2State) {
                        tab2State = true;
                        document.getElementById("tab2").src = document.getElementById("tab2").src;
                        //2013.5.16 by zhongbao.xiao 设置弹出窗体的Title
                        handler: fnSetWinTitle("1");
                    }
                }
            }
        }, {

            title: '采集参数',
            contentEl: "tab3",
            listeners:
            {
                activate: function () {
                    if (!tab3State) {
                        tab3State = true;
                        document.getElementById("tab3").src = document.getElementById("tab3").src;
                        //2013.5.16 by zhongbao.xiao 设置弹出窗体的Title
                        handler: fnSetWinTitle("2");
                    }
                }
            }
        }]
    });

    Ext.create('Jit.window.Window', {
        height: 550,
        width: 960,
        jitSize: "large",
        id: "stepEditWin",

        layout: 'fit',
        draggable: true,
        items: [Ext.getCmp("tabs")],
        border: 0,
        closeAction: 'hide'
    });
}