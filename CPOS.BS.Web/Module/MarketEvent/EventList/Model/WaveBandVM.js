function InitVE() { //活动波段对象
    Ext.define("WaveBandEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'WaveBandID', type: 'string'} //主标识
        , { name: 'MarketEventID', type: 'string'} //活动主标识
        , { name: 'BeginTime', type: 'string'} //开始时间
        , { name: 'EndTime', type: 'string'} //结束时间
        , { name: 'FactBeginTime', type: 'string'} //实际开始时间
        , { name: 'FactEndTime', type: 'string'} //实际结束时间
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