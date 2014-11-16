function InitView() {
    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "部门",
            id: "ClientStructureID"
        },
            {
                xtype: "jitbizrole",
                fieldLabel: "职位",
                //isDefaultLoad: true,
                id: "ClientPositionID"
            }, {
                xtype: "jittextfield",
                id: "ClientUserName",
                fieldLabel: "人员"
            }, {
                xtype: "jitdatefield",
                id: "ExecutionTime",
                fieldLabel: "执行日期",
                jitSize: 'small'
            }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });

    //operator area
    Ext.create('Ext.form.Panel', {
        width: '100%',
        cls: 'panel_search',
        renderTo: 'span_panel2',
        items: [{
            xtype: "jitbutton",
            imgName: 'search',
            text: "查询",
            hidden: __getHidden("search"),
            handler: fnSearch,
            isImgFirst: true
        }, {
            xtype: "jitbutton",
            imgName: 'reset',
            text: "重置",
            hidden: __getHidden("search"),
            handler: fnReset
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("taskDataStore"),
        id: "gridView",
        columnLines: true,
        forceFit: true,
        features: [{
            ftype: 'summary'
        }],
        columns: [{
            text: '人员名称',
            width: 55,
            sortable: true,
            dataIndex: 'ClientUserName',
            align: 'left',
            renderer: fnColumnUpdate,
            summaryRenderer: function () { return "合计"; }
        }, {
            text: '人员职位',
            width: 70,
            sortable: true,
            dataIndex: 'UserPositionName',
            align: 'left'
        }, {
            text: '所属部门',
            width: 95,
            sortable: true,
            dataIndex: 'DepartmentName',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'int',
            text: '计划拜访',
            width: 55,
            sortable: true,
            dataIndex: 'VisitingTaskPlanCount',
            align: 'left',
            summaryRenderer: function () {
                return Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).VisitingTaskPlanCountSummary;
            }
        }, {
            xtype: "jitcolumn",
            jitDataType: 'int',
            text: '实际拜访',
            width: 55,
            sortable: true,
            dataIndex: 'VisitingTaskExecutionCount',
            align: 'left',
            summaryRenderer: function () {
                return Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).VisitingTaskExecutionCountSummary;
            }
        }, {
            text: '拜访达成',
            width: 65,
            sortable: true,
            dataIndex: 'VisitingTaskExecutionEfficiency',
            align: 'right',
            summaryRenderer: function () {
                return Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).VisitingTaskExecutionEfficiencySummary;
            }
        }, {
            xtype: "jitcolumn",
            //jitDataType: 'monthdayhourminute',
            text: '首次进店',
            width: 75,
            sortable: true,
            dataIndex: 'FirstInTime',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            //jitDataType: 'monthdayhourminute',
            text: '最后出店',
            width: 75,
            sortable: true,
            dataIndex: 'LastOutTime',
            align: 'left'
        }, {
            text: '工作时间',
            width: 85,
            sortable: true,
            dataIndex: 'WorkingHoursTotal',
            align: 'right',
            renderer: fnColumnTime,
            summaryRenderer: function () {
                return fnColumnTime(Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).WorkingHoursTotalSummary);
            }
        }, {
            text: '店内时间',
            width: 85,
            sortable: true,
            dataIndex: 'WorkingHoursIndoor',
            align: 'right',
            renderer: fnColumnTime,
            summaryRenderer: function () {
                return fnColumnTime(Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).WorkingHoursIndoorSummary);
            }
        }, {
            text: '路途时间',
            width: 85,
            sortable: true,
            dataIndex: 'WorkingHoursJourneyTime',
            align: 'right',
            renderer: fnColumnTime,
            summaryRenderer: function () {
                return fnColumnTime(Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).WorkingHoursJourneyTimeSummary);
            }
        }, {
            text: '有效百分比',
            width: 65,
            sortable: true,
            dataIndex: 'WorkingHoursEfficiency',
            align: 'right',
            renderer: fnColumnColor,
            summaryRenderer: function () {
                return Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).WorkingHoursEfficiencySummary;
            }
        }, {
            xtype: "jitcolumn",
            jitDataType: 'int',
            text: '有效拜访',
            width: 50,
            sortable: true,
            dataIndex: 'EffectiveVisit',
            align: 'right',
            summaryRenderer: function (value, summaryData, dataIndex) {
                return Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).EffectiveVisitSummary;

            }
        }, {
            text: '订单达成',
            width: 65,
            sortable: true,
            dataIndex: 'OrderSuccessRate',
            align: 'right',
            summaryRenderer: function () {
                return Ext.JSON.decode(Ext.getStore("taskDataStore").first().data.summary).OrderSuccessRateSummary;
            }
        }],
        height: 400,
        stripeRows: true,
        width: "100%",
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("taskDataStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView",
        listeners: {
            render: function (p) {
                p.setLoading({
                    //msg: JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });
}