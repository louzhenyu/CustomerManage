function InitVE() {
    Ext.define("VipCardEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'VipCardID', type: 'string' }
            , { name: 'VipCardTypeID', type: 'string' }
            , { name: 'VipCardCode', type: 'string' }
            , { name: 'VipCardName', type: 'string' }
            , { name: 'VipCardStatusId', type: 'string' }
            , { name: 'MembershipTime', type: 'string' }
            , { name: 'BeginDate', type: 'string' }
            , { name: 'EndDate', type: 'string' }
            , { name: 'TotalAmount', type: 'string' }
            , { name: 'BalanceAmount', type: 'string' }
            , { name: 'VipCardGradeID', type: 'string' }
            , { name: 'PurchaseTotalAmount', type: 'string' }
            , { name: 'PurchaseTotalCount', type: 'string' }
            , { name: 'LastSalesTime', type: 'string' }
            , { name: 'CustomerID', type: 'string' }
            , { name: 'VipName', type: 'string' }
            , { name: 'UnitName', type: 'string' }
            , { name: 'VipCardGradeName', type: 'string' }
            , { name: 'VipStatusName', type: 'string' }
//            , { name: 'PurchaseTotalCount', type: 'string' }
            , { name: 'CreateBy', type: 'string' }
            , { name: 'LastUpdateBy', type: 'string' }
            , { name: 'LastUpdateTime', type: 'string' }
            , { name: 'TemplateDesc', type: 'string' }
            ]
    });

}