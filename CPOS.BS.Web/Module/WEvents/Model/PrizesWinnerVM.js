function InitVE() {
    Ext.define("PrizesWinnerViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'PrizeWinnerID', type: 'string' },
            { name: 'VipID', type: 'string' },
            { name: 'VipName', type: 'string' },
            { name: 'PrizeID', type: 'string' },
            { name: 'PrizeName', type: 'string' },
            { name: 'EventId', type: 'string' },
            { name: 'RoundId', type: 'string' },
            { name: 'Round', type: 'string' },
            { name: 'RoundDesc', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' }
        ]
    });
}