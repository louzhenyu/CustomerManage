function InitVE() { //活动波段对象
    Ext.define("EventAnalysisEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'MarketEventID', type: 'string'
        } //主标识
        , { name: 'BeginDate', type: 'string'} //执行活动开始时间
        , { name: 'EndDate', type: 'string'} //执行活动结束时间
        , { name: 'StoreCount', type: 'string'} //参与门店
        , { name: 'ResponseStoreCount', type: 'string'} //响应门店
        , { name: 'ResponseStoreRate', type: 'string'} //门店响应率
        , { name: 'PersonCount', type: 'string'} //邀约人数
        , { name: 'ResponsePersonCount', type: 'string'} //响应人数
        , { name: 'ResponsePersonRate', type: 'string'} //会员响应率
        , { name: 'BudgetTotal', type: 'string'} //预算总费用
        , { name: 'CurrentSales', type: 'string'} //当前消费额
        , { name: 'EventMaori', type: 'string'} //活动毛利
        , { name: 'EventNetProfit', type: 'int'} //活动净利润
        ]
    });
}