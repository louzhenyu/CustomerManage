function InitVE() {
    Ext.define("PrintPickingEntity", {
        extend: "Ext.data.Model",
        fields: [
            {
            name: "itemcode",
            type: "string"
        }, {
            name: "itemname",
            type: "string"
        }, {
            name: "price",
            type: "decimal"
        }, {
            name: "orderqty",
            type: "int"
        }, {
            name: "money",
            type: "decimal"
        }, {
            name: "remark",
            type: "string"
        }, {
            name: "ItemType",
            type: "string"
        }
        ]
    });
}