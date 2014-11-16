function InitVE() {
    Ext.define("LotteryLogViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'LogId', type: 'string' },
            { name: 'VipId', type: 'string' },
            { name: 'VipName', type: 'string' },
            { name: 'EventId', type: 'string' },
            { name: 'LotteryCount', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' }
        ]
    });
}