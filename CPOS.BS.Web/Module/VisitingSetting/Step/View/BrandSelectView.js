var CheckBoxModel = Ext.create('Jit.selection.CheckboxModel', {
    mode: 'MULTI',
    idProperty: 'Target1ID',   //这个值是添加到数据库中数据
    idSelect: 'ObjectID'  //这个是判断是否选中的数据
});
var gridCloumn = [{
    text: '品牌名称',
    width: 110,
    sortable: true,
    dataIndex: 'BrandName',
    align: 'left'
}, {
    text: '子品牌名称',
    width: 110,
    sortable: true,
    dataIndex: 'ParameterTypeText',
    align: 'left'
}, {
    text: '自有品牌',
    width: 110,
    sortable: true,
    dataIndex: 'IsOwner',
    align: 'left'
}, {
    text: '客户名称',
    width: 110,
    sortable: true,
    dataIndex: 'BrandCompany',
    align: 'left'
}, {
    text: '备注',
    width: 110,
    sortable: true,
    dataIndex: 'Remark',
    align: 'left'
}, {
    text: '备注',
    width: 110,
    sortable: true,
    dataIndex: 'Target1ID',
    align: 'left'
}, {
    text: '备注',
    width: 110,
    sortable: true,
    dataIndex: 'ObjectID',
    align: 'left'
}];

function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitcombobox",
            fieldLabel: "品牌等级",
            name: "comboType",
            id:"comboType",
            store: Ext.getStore("typeStore"),
            valueField: "value",
            displayField: "name",
            listeners: {
                change: fnTypeChange
            }
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });
    
    //operator area
    Ext.create('Jit.button.Button', {
        imgName: 'save',
        id:"btnSave",
        renderTo: "span_save",
        isImgFirst: true,
        handler: fnSave
    });

    //list area
    Ext.getStore("brandStore").on("load", function () {
        CheckBoxModel.jitSetValue();
    });
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("brandStore"),
        id: "gridView",
        columnLines: true,
        selModel: CheckBoxModel,
        columns: gridCloumn,
        height: 375,
        stripeRows: true,
        
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id:"pageBar",
            defaultType: 'button',
            store: Ext.getStore("brandStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView",
        listeners: {
            render: function (p) {
                p.setLoading({
                    store: p.getStore()
                }).hide();
            }
        }
    });
}