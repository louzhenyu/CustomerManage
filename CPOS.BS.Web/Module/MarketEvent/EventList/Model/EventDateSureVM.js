function InitVE() {
    Ext.define("EventDateSureViewEntity", {
        extend: "Ext.data.Model",
        fields: [
        {
            name: 'DisplayIndex', //序号
            type: 'string'
        }, {
            name: 'MarketEventID', type: 'string'} //主标识
            , { name: 'WaveBandID', type: 'string'} //活动号码
            ,{name: 'EventType',type: 'string'} //活动类型
        ,
        {
            name: 'BeginTime',
            type: 'string'
        } //计划开始时间
        ,
        {
            name: 'FactBeginTime', //实际开始时间
            type: 'date',
            dateFormat: 'Y-m-d'
        },
        {
            name: 'EndTime', //计划结束时间
            type: 'string'
        },
        {
            name: 'FactEndTime', //实际结束时间
            type: 'date',
            dateFormat: 'Y-m-d'
        }
        ]
    });
  
    
    
}