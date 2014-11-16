function InitVE() {
    Ext.define("customeTypeEntit", {
        extend: "Ext.data.Model",
        url: JITPage.HandlerUrl.getValue() + "&method=GetCousCustomerType",
        fields: [{
            name: "UnitSortId",
            type: "string"
        }, {
            name: "SortName",
            type: "string"
        }]
    })
}