function InitVE() {
    Ext.define("RoleViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Role_Id', type: 'string' }, 
            { name: 'Role_Code', type: 'string' }, 
            { name: 'Role_Name', type: 'string' }, 
            { name: 'Role_Eng_Name', type: 'string' }, 
            { name: 'Is_Sys', type: 'string' }, 
            { name: 'Role_Email', type: 'string' }, 
            { name: 'Role_Cellphone', type: 'string' }, 
            { name: 'Role_Tel', type: 'string' }, 
            { name: 'Role_Status', type: 'string' }, 
            { name: 'Role_Status_Desc', type: 'string' }, 
            { name: 'Role_Remark', type: 'string' },
            { name: 'Def_App_Id', type: 'string' }, 
            { name: 'Def_App_Name', type: 'string' }, 
            { name: "Create_User_Id", type: 'string' }, 
            { name: "Create_User_Name", type: 'string' }, 
            { name: "Create_Time", type: 'string' }, 
            { name: "Modify_User_Id", type: 'string' }, 
            { name: "Modify_User_Name", type: 'string' }, 
            { name: "Modify_Time", type: 'string' }
            ]
    });
    
    Ext.define("MarketWaveBandViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'WaveBandID', type: 'string' }, 
            { name: 'MarketEventID', type: 'string' }, 
            { name: 'BeginTime', type: 'string' }, 
            { name: 'EndTime', type: 'string' }, 
            { name: 'FactBeginTime', type: 'string' }, 
            { name: 'FactEndTime', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }
            ]
    });
    
    Ext.define("MarketStoreViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MarketStoreID', type: 'string' }, 
            { name: 'MarketEventID', type: 'string' }, 
            { name: 'StoreID', type: 'string' }, 
            { name: 'StoreCode', type: 'string' }, 
            { name: 'StoreName', type: 'string' }, 
            { name: 'BusinessDistrict', type: 'string' }, 
            { name: 'Address', type: 'string' }, 
            { name: 'MembersCount', type: 'string' }, 
            { name: 'SalesYear', type: 'string' }, 
            { name: 'Opened', type: 'string' }, 
            { name: 'Longitude', type: 'string' }, 
            { name: 'Latitude', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }
            ]
    });

    Ext.define("MarketPersonViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MarketPersonID', type: 'string' }, 
            { name: 'MarketEventID', type: 'string' }, 
            { name: 'VIPID', type: 'string' }, 
            { name: 'VipName', type: 'string' }, 
            { name: 'VipLevel', type: 'string' }, 
            { name: 'VipCode', type: 'string' }, 
            { name: 'WeiXin', type: 'string' }, 
            { name: 'Phone', type: 'string' }, 
            { name: 'Integration', type: 'string' },
            { name: 'PurchaseAmount', type: 'string' }, 
            { name: 'PurchaseCount', type: 'string' },
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }, 
            { name: 'LastUpdateTime', type: 'string' }
            ]
    });
    
    Ext.define("VipViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "VIPID", type: 'string' }, 
            { name: "VipName", type: 'string' }, 
            { name: "VipLevel", type: 'string' }, 
            { name: "VipLevelDesc", type: 'string' }, 
            { name: "VipCode", type: 'string' }, 
            { name: "WeiXin", type: 'string' }, 
            { name: "WeiXinUserId", type: 'string' }, 
            { name: "Gender", type: 'string' }, 
            { name: "Age", type: 'string' }, 
            { name: "Phone", type: 'string' }, 
            { name: "SinaMBlog", type: 'string' }, 
            { name: "TencentMBlog", type: 'string' }, 
            { name: "Birthday", type: 'string' }, 
            { name: "Qq", type: 'string' }, 
            { name: "Email", type: 'string' }, 
            { name: "Status", type: 'string' }, 
            { name: "StatusDesc", type: 'string' }, 
            { name: "VipSourceId", type: 'string' }, 
            { name: "VipSourceName", type: 'string' }, 
            { name: "Integration", type: 'string' }, 
            { name: "ClientID", type: 'string' }, 
            { name: "RecentlySalesTime", type: 'string' }, 
            { name: "RegistrationTime", type: 'string' }, 
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
            { name: 'IntegralForHightUser', type: 'string' }, 
            { name: 'PurchaseAmount', type: 'string' }, 
            { name: 'PurchaseCount', type: 'string' }, 
            { name: 'UserName', type: 'string' }, 
            { name: 'Enterprice', type: 'string' }, 
            { name: 'IsChainStores', type: 'string' }, 
            { name: 'IsWeiXinMarketing', type: 'string' }, 
            { name: 'GenderInfo', type: 'string' }
            ]
    });
}