function InitVE() {
    Ext.define("EventsViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'VipShowId', type: 'string' },
            { name: 'VipId', type: 'string' },
            { name: 'VipName', type: 'string' },
            { name: 'UnitId', type: 'string' },
            { name: 'Experience', type: 'string' },
            { name: 'PraiseCount', type: 'string' },
            { name: 'HairStylistId', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' },
            { name: 'ImageCount', type: 'string' },
            { name: 'ItemName', type: 'string' },
            { name: 'IsCheck', type: 'string' }
        ]
    });
    
    Ext.define("ImageViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ImageId', type: 'string' },
            { name: 'ObjectId', type: 'string' },
            { name: 'ImageURL', type: 'string' },
            { name: 'DispalyIndex', type: 'string' },
            { name: 'DisplayIndexLast', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' },
            { name: 'ImageCount', type: 'string' }
        ]
    });
}