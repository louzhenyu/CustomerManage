function InitVE() {
    Ext.define("RoleTableNameEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "role_name",
            type: "string"
        }, {
            name: "table_name",
            type: "string"
        }]
    });

}