function InitVE() {
    Ext.define("AlbumViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'AlbumId', type: 'string' },
            { name: 'ModuleId', type: 'string' },
            { name: 'ModuleType', type: 'string' },
            { name: 'ModuleName', type: 'string' },
            { name: 'Type', type: 'string' },
            { name: 'ImageUrl', type: 'string' },
            { name: 'Title', type: 'string' },
            { name: 'Description', type: 'string' },
            { name: 'SortOrder', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' },
            { name: 'TypeName', type: 'string' },
            { name: 'ModuleTypeName', type: 'string' }
        ]
    });

    Ext.define("AlbumModuleViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ID', type: 'string' },
            { name: 'Title', type: 'string' },
            { name: 'CreateTime', type: 'string' }
        ]
    });

    Ext.define("AlbumImageViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'PhotoId', type: 'string' },
            { name: 'AlbumId', type: 'string' },
            { name: 'LinkUrl', type: 'string' },
            { name: 'Title', type: 'string' },
            { name: 'Description', type: 'string' },
            { name: 'SortOrder', type: 'string' },
            { name: 'ReaderCount', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' }
        ]
    });
    Ext.define("AlbumTypeEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ModuleType', type: 'string' },
            { name: 'ModuleName', type: 'string' },
        ]
    });
}