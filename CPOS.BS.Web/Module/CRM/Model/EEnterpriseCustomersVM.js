function InitVE() {
    Ext.define("EEnterpriseCustomersViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'EnterpriseCustomerId', type: 'string' },
            { name: 'EnterpriseCustomerName', type: 'string' },
            { name: 'TypeId', type: 'string' },
            { name: 'IndustryId', type: 'string' },
            { name: 'ScaleId', type: 'string' },
            { name: 'CityId', type: 'string' },
            { name: 'Tel', type: 'string' },
            { name: 'Address', type: 'string' },
            { name: 'ECSourceId', type: 'string' },
            { name: 'Remark', type: 'string' },
            { name: 'Status', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' },
            { name: 'CustomerId', type: 'string' },
            { name: 'TypeName', type: 'string' },
            { name: 'IndustryName', type: 'string' },
            { name: 'ScaleName', type: 'string' },
            { name: 'ECSourceName', type: 'string' },
            { name: 'CreateByName', type: 'string' },
            { name: 'CityName', type: 'string' },
            { name: 'DisplayIndex', type: 'string' }
        ]
    });
    
    Ext.define("VipEnterpriseViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'VipId', type: 'string' },
            { name: 'EnterpriseCustomerId', type: 'string' },
            { name: 'Position', type: 'string' },
            { name: 'Department', type: 'string' },
            { name: 'Fax', type: 'string' },
            { name: 'PersonDesc', type: 'string' },
            { name: 'Status', type: 'string' },
            { name: 'PDRoleId', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' },
            { name: 'VipName', type: 'string' },
            { name: 'GenderName', type: 'string' },
            { name: 'Phone', type: 'string' },
            { name: 'PDRoleName', type: 'string' },
            { name: 'EnterpriseCustomerName', type: 'string' },
            { name: 'StatusName', type: 'string' },
            { name: 'CustomerId', type: 'string' }
        ]
    });
    
    Ext.define("ItemImageViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'DownloadId', type: 'string' },
            { name: 'ObjectId', type: 'string' },
            { name: 'DownloadName', type: 'string' },
            { name: 'DownloadUrl', type: 'string' },
            { name: 'DisplayIndex', type: 'string' }
            ]
    });
    
    Ext.define("ESalesVisitVipViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MappingId', type: 'string' },
            { name: 'VipId', type: 'string' },
            { name: 'VipName', type: 'string' },
            { name: 'SalesVisitId', type: 'string' },
            { name: 'Department', type: 'string' },
            { name: 'Position', type: 'string' },
            { name: 'PDRoleId', type: 'string' },
            { name: 'PDRoleName', type: 'string' }
            ]
    });
}