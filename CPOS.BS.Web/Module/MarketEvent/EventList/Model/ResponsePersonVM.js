function InitVE() { //活动响应对象
    Ext.define("ResponsePersonEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'ReponseID', type: 'string'
        } //主标识
        , { name: 'MarketEventID', type: 'string'} //活动标识
        , { name: 'VIPID', type: 'string'} //vip标识
        , { name: 'CustomerPrice', type: 'string'} //客单价
        , { name: 'UnitPrice', type: 'string'} //件单价
        , { name: 'PurchaseNumber', type: 'int'} //件数
        , { name: 'SalesIntegral', type: 'string'} //消费积分
        , { name: 'PurchaseAmount', type: 'string'} //购买金额
        , { name: 'PurchaseCount', type: 'string'} //购买次数
        , { name: 'VipCode', type: 'string'} //卡号
        , { name: 'VipLevel', type: 'string'} //等级
        , { name: 'VipName', type: 'string'} //会员名称
        , { name: 'StatisticsTime', type: 'string'} //统计时间
        , { name: 'CreateTime', type: 'string'} //创建时间
        , { name: 'CreateBy', type: 'string'} //创建人标识
        , { name: 'LastUpdateBy', type: 'string'} //最后更改时间
        , { name: 'LastUpdateTime', type: 'string'} //最后更改人
        , { name: 'IsDelete', type: 'string'} //是否删除
        , { name: 'ICount', type: 'int'} //总数量
        , { name: 'DisplayIndex', type: 'int'} //排序
        ]
    });
}