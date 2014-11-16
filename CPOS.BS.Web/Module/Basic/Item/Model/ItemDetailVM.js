function InitVE() {

    Ext.define("ItemPriceViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'item_price_id', type: 'string' }, 
            { name: 'item_price_type_id', type: 'string' }, 
            { name: 'item_price_type_name', type: 'string' }, 
            { name: 'item_price', type: 'string' }
            ]
    });
    //图片实体
    Ext.define("ItemImageViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ImageId', type: 'string' },
            { name: 'ObjectId', type: 'string' },
            { name: 'ImageURL', type: 'string' },
            { name: 'Title', type: 'string' },
            { name: 'Description', type: 'string' },
            { name: 'DisplayIndex', type: 'string' }
            ]
    });
    
    Ext.define("ItemUnitViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MappingId', type: 'string' }, 
            { name: 'ItemId', type: 'string' }, 
            { name: 'UnitId', type: 'string' }, 
            { name: 'UnitName', type: 'string' }
            ]
    });

    Ext.define("ItemCategoryViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "Item_Category_Id", type: 'string' },
            { name: "Item_Category_Name", type: 'string' },
            { name: "IsFirstVisit", type: 'int' }
            ]
    });

    Ext.define("ItemTagViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ItemTagID", type: 'string' },
            { name: "ItemTagName", type: 'string' },
            { name: "IsFirstVisit", type: 'int' }
        ]
    });
}