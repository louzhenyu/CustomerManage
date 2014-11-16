function InitVE() {
    Ext.define("NewsViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: 'AlbumId',
            type: 'string'
        },
        {
            name: 'ImageUrl',
            type: 'string'
        },
        {
            name: 'Title',
            type: 'string'
        },
        {
            name: 'Intro',
            type: 'string'
        },
        {
            name: 'Description',
            type: 'string'
        },
        {
            name: 'SortOrder',
            type: 'int'
        },
        {
            name: 'ContentUrl',
            type: 'string'
        },
        {
            name: 'BrowseNum',
            type: 'int'
        },
        {
            name: 'BookmarkNum',
            type: 'int'
        },
        {
            name: 'PraiseNum',
            type: 'int'
        },
         {
             name: 'ShareNum',
             type: 'int'
         }
        ]
    });
    Ext.define("LNewsTypeEntit", {
        extend: "Ext.data.Model",
        fields: [{
            name: "ModuleType",
            type: "string"
        }, {
            name: "ModuleName",
            type: "string"
        }]
    });
}