function InitVE() {
    Ext.define("PaymentEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "PaymentTypeID",
            type: "string"
        }, {
            name: "PaymentTypeName",
            type: "string"
        }, {
            name: "PaymentTypeCode",
            type: "string"
        }, {
            name: "ChannelId",
            type: "string"

        }, {
            name: "IsDefault",
            type: "string"
        }, {
            name: "IsOpen",
            type: "string"

        }, {
            name: "IsCustom",
            type: "string"

        }]

    });
}