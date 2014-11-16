function InitVE() {
    Ext.define("TicketViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "TicketID",
            type: "string"
        }, {
            name: "TicketName",
            type: "string"
        }, {
            name: "TicketRemark",
            type: "string"
        }, {
            name: "TicketPrice",
            type: "string"
        }, {
            name: "TicketNum",
            type: "int"
        }, {
            name: "TicketSort",
            type: "int"
        }, {
            name: "EventID",
            type: "string"
        }, {
            name: "CreateBy",
            type: "string"
        }, {
            name: "CreateTime",
            type: "datetime"
        }, {
            name: "LastUpdateBy",
            type: "string"
        }, {
            name: "LastUpdateTime",
            type: "datetime"
        }, {
            name: "IsDelete",
            type: "int"
        }, {
            name: "CustomerID",
            type: "string"
        }, {
            name: "Title",
            type: "string"
        }]
    });

}