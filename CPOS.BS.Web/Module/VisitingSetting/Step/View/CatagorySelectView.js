var CheckBoxModel = Ext.create('Jit.selection.CheckboxModel', {
    mode: 'MULTI',
    idProperty: 'Target1ID',   //这个值是添加到数据库中数据
    idSelect: 'ObjectID'  //这个是判断是否选中的数据
});
var gridCloumn = [{
            text: '品类名称',
            width: 110,
            sortable: true,
            dataIndex: 'ParentCategoryName',
            align: 'left'
        }, {
            text: '子品类名称',
            width: 110,
            sortable: true,
            dataIndex: 'CategoryName',
            align: 'left'
        }, {
            text: '备注',
            width: 110,
            sortable: true,
            dataIndex: 'Remark',
            align: 'left'
        }];

function InitView() {

    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitcombobox",
            fieldLabel: "品类等级",
            name: "comboType",
            id: "comboType",
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
        id: "btnSave",
        renderTo: "span_save",
        isImgFirst: true,
        handler: fnSave
    });

    //list area
    Ext.getStore("categoryStore").on("load", function () {
        CheckBoxModel.jitSetValue();
    });
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("categoryStore"),
        id: "gridView",
        columnLines: true,
        selModel: CheckBoxModel,
        columns: gridCloumn,
        height: 380,
        stripeRows: true,
        
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("categoryStore"),
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