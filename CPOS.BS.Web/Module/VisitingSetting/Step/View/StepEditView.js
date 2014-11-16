function InitView() {
    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        width: "920px",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: "anchor",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>步骤名称",
            name: "StepName",
            allowBlank: false
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>对象类型",
            OptionName: 'StepType',
            name: "StepType",
            id: "StepType",
            isDefault: false,
            allowBlank: false,
            listeners: {
                change: fnStepChange
            }
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>对象分组",
            OptionName: 'ObjectGroup',
            name: "ObjectGroup",
            id: "ObjectGroup",
            isDefault: false,
            allowBlank: true,
            hidden: true
        }, {
            xtype: "jitcombobox",
            fieldLabel: "必做",
            name: "IsMustDo",
            store: Ext.getStore("tfStore"),
            valueField: "value",
            displayField: "name",
            id:"MustDo"
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>显示顺序",
            name: "StepPriority",
            allowBlank: false,
            vtype: "number",
            value: "1"
        }, {
            columnWidth: 1,
            layout: 'column',
            border: 0,
            items: [
            {
                xtype: "jitcombobox",
                fieldLabel: "<font color='red'>*</font>一页操作",
                name: "IsOnePage",
                allowBlank: false,
                store: Ext.getStore("tfStore"),
                valueField: "value",
                displayField: "name",
                id:"OnePage"
            },
            {
                xtype: 'label',
                text: "说明：将拜访对象与采集数据以二维表格一页，通常采集数据不适合超过三个以上",
                margin: '0 0 0 10'
            }]
        }, {
            xtype: "jittextarea",
            fieldLabel: "备注",
            name: "Remark"
        }],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            imgName: 'save',
            isImgFirst: true,
            handler: fnSubmit
        }],
        renderTo: "DivGridView"
    });
}