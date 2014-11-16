function InitVE() {
    Ext.define("TransferEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "WithdrawalId",
            type: "string"
        }, {
            name: "SerialNo",
            type: "string"
        }, {
            name: "WithdrawalTime",
            type: "string"
        }, {
            name: "WithdrawalAmount",
            type: "string"
        },
        {
            name: "BankAccount",
            type: "string"
        }, {

            name: "CustomerName",
            type: "string"
        },
        {
            name: "ReceivingBank",
            type: "string"
        }]
    });
}