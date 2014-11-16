function InitVE() {
    Ext.define("VipCardViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "VIPID", type: 'string' }, 
            { name: "VipName", type: 'string' }, 
            { name: "VipLevel", type: 'string' }, 
            { name: "VipLevelDesc", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "APPID", type: 'string' }, 
            { name: "HigherVipID", type: 'string' }, 
            { name: "QRVipCode", type: 'string' }, 
            { name: "City", type: 'string' }, 
            { name: "LastUnit", type: 'string' }, 
            { name: "NextLevelIntegralAmount", type: 'string' }, 
            { name: "IntegralAmount", type: 'string' }, 
            { name: 'IntegralForHightUser', type: 'string' }
            ]
    });

    
    Ext.define("VipCardSalesViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'SalesID', type: 'string' }, 
            { name: 'VipCardID', type: 'string' }, 
            { name: 'SalesAmount', type: 'string' }, 
            { name: 'SalesBeforeAmount', type: 'string' }, 
            { name: 'SalesAfterAmount', type: 'string' }, 
            { name: 'OrderNo', type: 'string' }, 
            { name: 'UnitID', type: 'string' }, 
            { name: 'UnitName', type: 'string' }, 
            { name: 'SalesTime', type: 'string' }, 
            { name: 'OperationUserID', type: 'string' }, 
            { name: 'OperationUserName', type: 'string' }, 
            { name: 'CustomerID', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });

    Ext.define("VipCardRechargeRecordViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'RechargeRecordID', type: 'string' }, 
            { name: 'VipCardID', type: 'string' }, 
            { name: 'RechargeAmount', type: 'string' }, 
            { name: 'BalanceBeforeAmount', type: 'string' }, 
            { name: 'BalanceAfterAmount', type: 'string' }, 
            { name: 'RechargeNo', type: 'string' }, 
            { name: 'PaymentTypeID', type: 'string' }, 
            { name: 'PaymentTypeName', type: 'string' }, 
            { name: 'RechargeTime', type: 'string' }, 
            { name: 'RechargeUserID', type: 'string' }, 
            { name: 'RechargeUserName', type: 'string' }, 
            { name: 'UnitID', type: 'string' }, 
            { name: 'UnitName', type: 'string' }, 
            { name: 'CustomerID', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });
    
    Ext.define("VipCardGradeChangeLogViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ChangeLogID', type: 'string' }, 
            { name: 'VipCardID', type: 'string' }, 
            { name: 'ChangeBeforeGradeID', type: 'string' }, 
            { name: 'ChangeBeforeGradeName', type: 'string' }, 
            { name: 'NowGradeID', type: 'string' }, 
            { name: 'NowGradeName', type: 'string' }, 
            { name: 'ChangeReason', type: 'string' }, 
            { name: 'OperationType', type: 'string' }, 
            { name: 'OperationTypeName', type: 'string' }, 
            { name: 'ChangeTime', type: 'string' }, 
            { name: 'UnitID', type: 'string' }, 
            { name: 'UnitName', type: 'string' }, 
            { name: 'OperationUserID', type: 'string' }, 
            { name: 'OperationUserName', type: 'string' }, 
            { name: 'CustomerID', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });

    Ext.define("VipCardStatusChangeLogViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'LogID', type: 'string' }, 
            { name: 'VipCardID', type: 'string' }, 
            { name: 'VipCardStatusID', type: 'string' }, 
            { name: 'VipCardStatusName', type: 'string' }, 
            { name: 'OldStatusName', type: 'string' }, 
            { name: 'UnitName', type: 'string' }, 
            { name: 'OperationUserName', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });

    Ext.define("VipExpandViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'VipExpandID', type: 'string' }, 
            { name: 'VipCardID', type: 'string' }, 
            { name: 'VipID', type: 'string' }, 
            { name: 'LicensePlateNo', type: 'string' }, 
            { name: 'CarBrandID', type: 'string' }, 
            { name: 'CarBarndName', type: 'string' }, 
            { name: 'CarModelsID', type: 'string' }, 
            { name: 'CarModelsName', type: 'string' }, 
            { name: 'ChassisNumber', type: 'string' }, 
            { name: 'CompartmentsForm', type: 'string' }, 
            { name: 'PurchaseTime', type: 'string' }, 
            { name: 'Remark', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });

}