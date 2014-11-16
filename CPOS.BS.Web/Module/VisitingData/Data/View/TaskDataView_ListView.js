function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizunitselecttree",
            fieldLabel: "部门",
            id: "ClientStructureID",
            isDefault: true
        }, {
            xtype: "jitbizrole",
            fieldLabel: "职位",
            hidden: true,
            id: "ClientPositionID",
            isDefault: true
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
        cls: 'panel_search',
        renderTo: 'span_panel2',
        items: [{
            xtype: "jitbutton",
            imgName: 'search',
            hidden: __getHidden("search"),
            handler: fnSearch,
            text: "查询",
            isImgFirst: true
        }, {
            xtype: "jitbutton",
            imgName: 'reset',
            hidden: __getHidden("search"),
            text: "重置",
            handler: fnReset
        }, {
            xtype: "jitbutton",
            text: "返回",
            imgName: 'back',
            handler: fnBack
        }, {
            xtype: "jitcheckbox",
            width: 130,
            id: "map_ckplan",
            boxLabel: '计划线路(蓝色线条)',
            checked: true,
            listeners: {
                'change': fnMapPlanLine
            }
        }, {
            xtype: "jitcheckbox",
            id: "map_ckfact",
            width: 430,
            boxLabel: '实际线路(绿色线条)，未定位或定位偏离计划位置超过1km的是红色点',
            checked: true,
            listeners: {
            'change':fnMapFactLine
            }
        }],
        margin: '0 0 10 0',
        layout: 'column',
        border: 0
    });

    Ext.widget('tabpanel', {
        width: "100%",
        height: "100%",
        id: "tabs",
        activeTab: 0,
        items: [{
            title: '走店明细',
            id: "tabs1",
            contentEl: "tab1",
            height: 500,
            border: 0,
            listeners: {
                activate: function () {
                    fnSetMapTool();
                    if (Ext.getCmp("gridView")) {
                        Ext.getCmp("gridView").reconfigure(Ext.getStore("taskDataStore"));
                    }
                }
            }
        }],
        renderTo: "DivGridView"
    });

    //tab1 
    //list area
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("taskDataStore"),
        id: "gridView",
        columnLines: true,
        forceFit: true,
        border: 0,
//        features: [{
//            ftype: 'summary'
//        }],
        columns: [{
            text: '人员',
            sortable: true,
            width: 60,
            align:"left",
            dataIndex: 'ClientUserName',
            summaryRenderer: function () { return "合计"; }
        }, {
            text: '门店名称',
            width: 250,
            sortable: true,
            dataIndex: 'POPName',
            align: 'left',
            renderer: fnColumnView
        }, {
            text: '走店日期',
            xtype: "jitcolumn",
            jitDataType: 'date',
            sortable: true,
            dataIndex: 'ExecutionTime',
            align: 'left'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'monthdayhourminute',
            text: '进店时间',
            width: 100,
            sortable: true,
            dataIndex: 'InTime'
        }, {
            xtype: "jitcolumn",
            jitDataType: 'monthdayhourminute',
            text: '出店时间',
            width: 100,
            sortable: true,
            dataIndex: 'OutTime'
        }, {
            text: '店内时间',
            width: 90,
            sortable: true,
            dataIndex: 'WorkingHoursIndoor',
            align: 'right',
            renderer: fnColumnTime,
            summaryRenderer: function (value, summaryData, dataIndex) {
                return fnColumnTime(oWorkingHoursIndoor);
            }
        }, {
            text: '定位',
            width: 90,
            sortable: true,
            dataIndex: 'InCoordinate',
            getMapTitle: function (val, r) { if (r.data.POPName != null && r.data.POPName != "undefined" && r.data.POPName != "") { return "地图-" + r.data.POPName; } },
            align: 'left',
            renderer: fnCoordinate
        }],
        height: 500,
        stripeRows: true,
        width: "100%",
        renderTo: "tab1",
        listeners: {
            render: function (p) {
                p.setLoading({
                    msg:"",// JITPage.Msg.GetData,
                    store: p.getStore()
                }).hide();
            }
        }
    });
   
}