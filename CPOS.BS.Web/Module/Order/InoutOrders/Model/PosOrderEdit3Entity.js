function fnInitVE() {
    Ext.define("WUserMessageEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MessageText', type: 'string' },
            { name: 'CreateTime', type: 'string' }
            ]
    });

    Ext.define("TInoutStatusEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "InoutStatusID",
            type: "string"
        }, {
            name: "OrderID",
            type: "string"
        }, {
            name: "OrderStatus",
            type: "int"
        }, {
            name: "CheckResult",
            type: "int"
        }, {
            name: "PayMethod",
            type: "int"
        }, {
            name: "DeliverCompanyID",
            type: "string"
        }, {
            name: "DeliverOrder",
            type: "string"
        }, {
            name: "PicUrl",
            type: "string"
        }, {
            name: "StatusRemark",
            type: "string"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "CustomerID",
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
            name: "OrderStatusName",
            type: "string"
        }, {
            name: "CheckResultName",
            type: "string"
        }, {
            name: "PayMethodName",
            type: "string"
        }, {
            name: "unit_name",
            type: "string"
        }, {
            name: "LastUpdateTimeFormat",
            type: "string"
        }],
        idProperty: 'OrderStatus'
    });

}