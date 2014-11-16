function InitVE() {
    Ext.define("SalesViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'SalesId',
            type: 'string'
        },
        {
            name: 'SalesName',
            type: 'string'
        },
        {
            name: 'EnterpriseCustomerId',
            type: 'string'
        },
        {
            name: 'SalesProductId',
            type: 'string'
        },
        {
            name: 'SalesVipId',
            type: 'string'
        },
        {
            name: 'EndDate',
            type: 'string'
        },
        {
            name: 'ECSourceId',
            type: 'string'
        },
        {
            name: 'StageId',
            type: 'string'
        },
        {
            name: 'Possibility',
            type: 'string'
        },
        {
            name: 'ForecastAmount',
            type: 'string'
        },
        {
            name: 'CreateTime',
            type: 'string'
        },
        {
            name: 'CreateBy',
            type: 'string'
        },
        {
            name: 'LastUpdateTime',
            type: 'string'
        },
        {
            name: 'LastUpdateBy',
            type: 'string'
        },
        {
            name: 'IsDelete',
            type: 'string'
        },
        {
            name: 'Remark',
            type: 'string'
        },
        {
            name: 'StrPublishTime',
            type: 'string'
        },
        {
            name: 'StrTags',
            type: 'string'
        },
        {
            name: 'EnterpriseCustomerName',
            type: 'string'
        },
        {
            name: 'SalesProductName',
            type: 'string'
        },
        {
            name: 'ECSourceName',
            type: 'string'
        },
        {
            name: 'StageName',
            type: 'string'
        },
        {
            name: 'SalesVipName',
            type: 'string'
        },
        {
            name: 'CreateUserName',
            type: 'string'
        },
        {
            name: 'DisplayIndex',
            type: 'string'
        }
        
        ]
    });
}