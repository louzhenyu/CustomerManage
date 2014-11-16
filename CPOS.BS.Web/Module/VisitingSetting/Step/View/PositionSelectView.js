var CheckBoxModel = Ext.create('Jit.selection.CheckboxModel', {
    mode: 'MULTI',
    idProperty: 'Target1ID',   //这个值是添加到数据库中数据
    idSelect: 'ObjectID'  //这个是判断是否选中的数据
});


function InitView() {

    //searchpanel area
    /*Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [{
            xtype: "jitcombobox",
            fieldLabel: "品类等级",
            name: "ParameterType",
            store: null,
            valueField: "OptionsID",
            displayField: "OptionText"
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });*/
    
    //operator area
    Ext.create('Jit.button.Button', {
        imgName: 'save',
        id: "btnSave",
        renderTo: "span_save",
        isImgFirst: true,
        handler: fnSave
    });

    
    //list area
    Ext.getStore("positionStore").on("load", function () {
        CheckBoxModel.jitSetValue();
    });

    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("positionStore"),
        id: "gridView",
        columnLines: true,
        selModel: CheckBoxModel,
        columns: [{
            text: '职位名称',
            width: 110,
            sortable: true,
            dataIndex: 'PositionName',
            align: 'left'
        }],
        height: 430,
        stripeRows: true,
        
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id:"pageBar",
            defaultType: 'button',
            store: Ext.getStore("positionStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView"
    });
}