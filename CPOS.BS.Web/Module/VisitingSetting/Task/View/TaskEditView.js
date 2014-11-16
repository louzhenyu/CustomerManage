function InitView() {

    Ext.create('Ext.form.Panel', {
        //title: '拜访任务编辑',
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: "anchor",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>任务名称",
            name: "VisitingTaskName",
            allowBlank: false
        }, {
            xtype: "jitbizroleposition",
            id: "ClientPositionID",
            fieldLabel: "<font color='red'>*</font>人员职位",
            isDefault: false,
            allowBlank: false,
            isClientDistributor: true,
            isSelectLeafOnly: true
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>终端类型",
            OptionName: 'POPType',
            name: "POPType",
            id: "POPType",
            isDefault: false,
            allowBlank: false
        }, {
            xtype: "jitdatefield",
            fieldLabel: "<font color='red'>*</font>开始时间",
            id: "txtStartDate",
            name: "StartDate",
            allowBlank: false
        }, {
            xtype: "jitdatefield",
            fieldLabel: "结束时间",
            id: "txtEndDate",
            name: "EndDate",
            vtype:"enddate",
            begindateField: "txtStartDate"
        }, {
            columnWidth: 1,
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jitbizoptions",
                fieldLabel: "开始需执行",
                OptionName: 'StartGPSType',
                name: "__StartGPSType",
                id: "StartGPSType",
                isDefault: true,
                allowBlank: true
            },
            {
                xtype: "jitcheckbox",
                boxLabel: "拍照",
                name: "StartPic",
                inputValue: "1"
            }]
        }, {
            columnWidth: 1,
            layout: 'column',
            border: 0,
            items: [{
                xtype: "jitbizoptions",
                fieldLabel: "结束需执行",
                OptionName: 'EndGPSType',
                name: "__EndGPSType",
                id: "EndGPSType",
                isDefault: true,
                allowBlank: true
            }, {
                xtype: "jitcheckbox",
                boxLabel: "拍照",
                name: "EndPic",
                inputValue: "1"
            }]
        }, {
            xtype: "jittextfield",
            fieldLabel: "任务优先级",
            name: "TaskPriority",
            vtype: "number"
        }, {
            xtype: "jitcombobox",
            fieldLabel: "<font color='red'>*</font>合并操作",
            name: "IsCombin",
            allowBlank: false,
            store: Ext.getStore("isCombinStore"),
            valueField: "value",
            displayField: "name"
        }, {
            xtype: "jittextarea",
            fieldLabel: "备注",
            name: "Remark",
            maxLength:300
        }],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            imgName: 'save',
            isImgFirst: true,
            id: "btnSave",
            handler: fnSubmit
        }, {
            xtype: "jitbutton",
            handler: fnCancel,
            imgName: 'back'
        }],
        renderTo: "DivGridView"
    });
}