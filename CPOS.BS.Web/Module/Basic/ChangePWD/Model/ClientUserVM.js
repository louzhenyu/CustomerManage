function InitVE() {
    Ext.define("ClientUserEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "User_Id",
            type: "string"
        }, {
            name: "User_Code",
            type: "string"
        }, {
            name: "User_Name",
            type: "string"
        }, {
            name: "User_Password",
            type: "string"
        }]
    });
}