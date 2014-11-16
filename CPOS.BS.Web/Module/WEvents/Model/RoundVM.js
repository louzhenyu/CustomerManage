function InitVE() {
    Ext.define("RoundViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'RoundId', type: 'string' },
            { name: 'EventId', type: 'string' },
            { name: 'Round', type: 'string' },
            { name: 'RoundDesc', type: 'string' },
            { name: 'RoundStatus', type: 'string' },
            { name: 'RoundStatusName', type: 'string' },
            { name: 'PrizesCount', type: 'string' },
            { name: 'WinnerCount', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' }
        ]
    });
    Ext.define("RoundPrizesViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'RoundId', type: 'string' },
            { name: 'EventId', type: 'string' },
            { name: 'PrizesID', type: 'string' },
            { name: 'PrizesCount', type: 'string' },
            { name: 'WinnerCount', type: 'string' },
            { name: 'RoundStatus', type: 'string' },
            { name: 'PrizeName', type: 'string' },
            { name: 'RoundStatusName', type: 'string' },
            { name: 'PrizesCount', type: 'string' },
            { name: 'WinnerCount', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' }
        ]
    });
    Ext.define("StatusEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });
}