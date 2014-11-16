function InitView() {


    //tab1
    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: 'anchor',
        defaults: {},
        renderTo: "DivGridView",
        items: [
        {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>路线名称",
            name: "RouteName",
            allowBlank: false
        }, {
            xtype: "jitcombobox",
            id: "eidt_State",
            fieldLabel: "<font color='red'>*</font>路线状态",
            store: Ext.getStore("stateStore"),
            allowBlank: false,
            displayField: 'name',
            valueField: 'value',
            name: "Status"
        }, {
            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>周期类型",
            store: Ext.getStore("cycleStore"),
            allowBlank: false,
            displayField: 'CycleName',
            valueField: 'CycleID',
            listeners: {
                change: fnCycleChange
            },
            name: "CycleID",
            id: "CycleID"
        }, {
            xtype: "jitcombobox",
            id: "eidt_CycleDetail",
            fieldLabel: "<font color='red'>*</font>执行日期",
            store: Ext.getStore("cycleDetailStore"),
            allowBlank: false,
            multiSelect: true,
            jitAllText :'全选',
            displayField: 'DayOfCycle',
            valueField: 'CycleDetailID',
            name: "CycleDetailID"
        }, {
            xtype: "JITStoreSelectPannel",
            id: "ClientUserID",

            fieldLabel: '<font color="red">*</font>执行人员',
            layout: 'column',
            border: 0,
            CheckMode: 'SINGLE',
            CorrelationValue: 0, //所有人员
            KeyName: "ClientUserID", //主健ID
            KeyText: "Name", //显示健值
            ajaxPath: '/Module/BasicData/ClientUser/Handler/ClientUserPositionHandler.ashx'
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "出行方式",
            OptionName: 'TripMode',
            id: "TripMode",
            isDefault: false
        }, {
            xtype: "jitdatefield",
            fieldLabel: "<font color='red'>*</font>开始时间",
            id: "StartDate",
            name: "StartDate",
            allowBlank: false
        }, {
            xtype: "jitdatefield",
            fieldLabel: "结束时间",
            id: "EndDate",
            name: "EndDate",
            vtype: "enddate",
            begindateField: "StartDate"
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>终端类型",
            OptionName: 'POPType',
            name: "POPType",
            id: "POPType",
            isDefault: false,
            allowBlank: false
        }, {
            xtype: "jittextarea",
            fieldLabel: "备注",
            name: "Remark"
        }],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            handler: fnSubmit,
            isImgFirst: true,
            imgName: 'save'
        }, {
            xtype: "jitbutton",
            handler: fnCancel,
            imgName: 'back'
        }]
    });
}