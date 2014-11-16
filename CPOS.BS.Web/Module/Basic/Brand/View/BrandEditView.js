function InitView() {
    Ext.create('Ext.form.Panel', {
        title: '品牌管理编辑',
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: "anchor",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "品牌名称",
            name: "BrandName",
            allowBlank: false
        }, {
            xtype: "jittextfield",
            fieldLabel: "品牌编号",
            name: "BrandNo",
            allowBlank: false
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "是否自有品牌",
            OptionName: 'IsOwner',
            name: "IsOwner",
            isDefault: false,
            listeners: {
                "change": fnControlTypeChange
            },
            allowBlank: false
        }, {
            xtype: "jitbizoptions",
            fieldLabel: "品牌等级",
            OptionName: 'BrandLevel',
            name: "BrandLevel",
            isDefault: false,
            allowBlank: false
        }, {
            xtype: "jitbizbrand",
            fieldLabel: "上级品牌",
            OptionName: 'ParentID',
            name: "ParentID1",
            isDefault: true,
            id: "cmbSingleSelect"
        }, {
            xtype: "jittextarea",
            fieldLabel: "备注",
            name: "Remark"
        }],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            text: "保存",
            formBind: true,
            disabled: true,
            handler: fnSubmit
        }, {
            xtype: "jitbutton",
            text: "返回",
            handler: fnCancel
        }],
        renderTo: "DivGridView"
    });
}