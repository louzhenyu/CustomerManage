function InitView() {
    //searchpanel area
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        items: [
        {
            xtype: "jitbizoptions",
            fieldLabel: "品牌等级",
            OptionName: 'BrandLevel',
            name: "BrandLevel",
            isDefault: true
        }, {
            xtype: "jittextfield",
            id: "txtBrandName",
            name: "BrandName",
            fieldLabel: "品牌名称",
            jitSize: 'small'
        }, {
            xtype: "jitbutton",
            text: __getText("search"),
            hidden: __getHidden("search"),
            handler: fnSearch
        }],
        renderTo: 'span_panel',
        margin: '10 0 0 0',
        layout: 'column',
        border: 0
    });
    //operator area
    Ext.create('Jit.button.Button', {
        text: __getText("create"),
        renderTo: "span_create",
        hidden: __getHidden("create"),
        handler: fnCreate
    });

    //list area（）
    Ext.create('Ext.grid.Panel', {
        store: Ext.getStore("brandStore"),
        id: "gridView",
        columnLines: true,
        columns: [{
            text: '操作',
            width: 110,
            sortable: true,
            dataIndex: 'BrandID',
            align: 'left',
            hidden: __getHidden("delete"),
            renderer: fnColumnDelete
        }, {
            text: '品牌编号',
            width: 110,
            sortable: true,
            dataIndex: 'BrandNo',
            align: 'left'
        }, {
            text: '品牌名称',
            width: 110,
            sortable: true,
            dataIndex: 'BrandName',
            align: 'left',
            renderer: fnColumnUpdate
        }, {
            text: '品牌等级',
            width: 110,
            sortable: true,
            dataIndex: 'BrandLevel',
            align: 'left',
            renderer: fnValidateBrandLevel
        }, {
            text: '上级品牌',
            width: 110,
            sortable: true,
            dataIndex: 'ParentBrandName',
            align: 'left'
        }, {
            text: '自有品牌',
            width: 110,
            sortable: true,
            dataIndex: 'IsOwner',
            align: 'left',
            renderer: fnCheckIsOwnerValue
        }, {
            text: '所属公司',
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
            text: '添加时间',
            width: 110,
            sortable: true,
            dataIndex: 'CreateTime',
            align: 'left'
        }],
        height: 450,
        stripeRows: true,
        width: "100%",
        selModel: Ext.create('Ext.selection.CheckboxModel', {
            mode: 'MULTI'
        }),
        bbar: new Ext.PagingToolbar({
            displayInfo: true,
            id: "pageBar",
            defaultType: 'button',
            store: Ext.getStore("brandStore"),
            pageSize: JITPage.PageSize.getValue()
        }),
        renderTo: "DivGridView"
    });
}