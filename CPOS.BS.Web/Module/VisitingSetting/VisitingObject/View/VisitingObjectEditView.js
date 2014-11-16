function InitView() {
    Ext.create('Ext.form.Panel', {
        id: "editPanel",
        width: "880px",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: "anchor",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>对象名称",
            name: "ObjectName",
            allowBlank: false
        },{
            xtype: "jitcombobox",
            fieldLabel: "状态",
            id: "eidt_State",
            store: Ext.getStore("stateStore"),
            allowBlank: false,
            displayField: 'name',
            valueField: 'value',
            name: "Status"
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>对象分组",
            OptionName: 'ObjectGroup',
            name: "ObjectGroup",
            id: "ObjectGroup",
            isDefault: false,
            allowBlank: false
        }, {
            xtype: "jitcombobox",
            fieldLabel: "上级对象",
            store: Ext.getStore("objectParentStore"),
            displayField: 'ObjectName',
            valueField: 'VisitingObjectID',
            name: "ParentID"
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>执行顺序",
            name: "Sequence",
            allowBlank: false//,
           // vtype: "number"
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "<font color='red'>*</font>排版方式",
            OptionName: 'LayoutType',
            name: "LayoutType",
            id: "LayoutType",
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
            id:"btnSave",
            imgName: 'save',
            isImgFirst: true,
            handler: fnSubmit
        }],
        renderTo: "DivGridView"
    });
}