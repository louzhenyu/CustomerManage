function InitVE() {
    Ext.define("EventListViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'MarketEventID', type: 'string'
        } //主标识
            , { name: 'EventCode', type: 'string'} //活动号码
            , { name: 'EventType', type: 'string'} //活动类型
        ,
        {
            name: 'EventMode',
            type: 'string'
        } //活动方式
        ,
        {
            name: 'EventStatus', //活动状态
            type: 'string'
        },
        {
            name: 'StatusDesc', //活动状态描述
            type: 'string'
        },
        {
            name: 'EventStatusDesc', //活动状态描述
            type: 'string'
        },
        {
            name: 'BudgetTotal', //预算总金额
            type: 'string'
        },
        {
            name: 'PerCapita', //人均金额
            type: 'string'
        },
        {
            name: 'BeginTime', //开始时间
            type: 'string'
        },
        {
            name: 'EndTime', //结束时间
            type: 'string'
        },
        {
            name: 'EventDesc', //活动描述
            type: 'string'
        },
        {
            name: 'IsWaveBand', //是否有波段
            type: 'string'
        },
        {
            name: 'StoreCount', //门店数量
            type: 'string'
        },
        {
            name: 'PersonCount', //人群数量
            type: 'string'
        },
        {
            name: 'TemplateID',
            type: 'string'
        },
        {
            name: 'BrandID',
            type: 'string'
        },
        {
            name: 'BrandName', //品牌名称
            type: 'string'
        },
        {
            name: 'StatisticsID', //统计标识
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
        }
        , { name: 'TemplateContent', type: 'string'} //邀约内容
        , { name: 'EventModeDesc',type:'string' }
        ]
    });
}