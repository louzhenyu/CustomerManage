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
            fieldLabel: '名称',
            name: 'StructureTitle',
            allowBlank: false

        },{
            xtype: "jitbizenterprisememberstructureselecttree",
            fieldLabel: '上级部门',
            name: 'ParentID'
        }, {
            xtype: "jittextfield",
            fieldLabel: '备注',
            name: 'Remark'
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