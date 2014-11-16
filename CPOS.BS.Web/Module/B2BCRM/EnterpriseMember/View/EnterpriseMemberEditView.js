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
            name: 'MemberName',
            allowBlank: false

        },{
            xtype: "jittextfield",
            fieldLabel: '名称(En)',
            name: 'MemberNameEn'
        }, {
            xtype: "jitbizcityselecttree",
            fieldLabel: '城市',
            name: 'CityCode'
        }, {
            xtype: "jittextfield",
            fieldLabel: '地址',
            name: 'Address'
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