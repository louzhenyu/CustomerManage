function InitVE() {
    Ext.define("EventsTypeEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'EventTypeID', type: 'string' },
            { name: 'Title', type: 'string' },
             { name: 'GroupNo', type: 'string' },
            { name: 'Remark', type: 'string' },
            { name: 'ClientID', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' }

        ]
    });
}