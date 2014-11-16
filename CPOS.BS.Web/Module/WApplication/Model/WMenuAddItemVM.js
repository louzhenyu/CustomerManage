function InitVE() {
    Ext.define("WMenuAddItem1ViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "WritingId", type: 'string' }, 
            { name: "Content", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

    Ext.define("WMenuAddItem2ViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ImageId", type: 'string' }, 
            { name: "ImageName", type: 'string' }, 
            { name: "ImageUrl", type: 'string' }, 
            { name: "ImageSize", type: 'string' }, 
            { name: "ImageFormat", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

    Ext.define("WMenuAddItem3ViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "TextId", type: 'string' }, 
            { name: "ParentTextId", type: 'string' }, 
            { name: "Title", type: 'string' }, 
            { name: "Author", type: 'string' }, 
            { name: "CoverImageUrl", type: 'string' }, 
            { name: "Text", type: 'string' }, 
            { name: "OriginalUrl", type: 'string' }, 
            { name: "DisplayIndex", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "TypeId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

    Ext.define("WMenuAddItem4ViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "VoiceId", type: 'string' }, 
            { name: "VoiceName", type: 'string' }, 
            { name: "VoiceUrl", type: 'string' }, 
            { name: "VoiceSize", type: 'string' }, 
            { name: "VoiceFormat", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

    Ext.define("WMenuAddItem5ViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "TextId", type: 'string' }, 
            { name: "ParentTextId", type: 'string' }, 
            { name: "Title", type: 'string' }, 
            { name: "Author", type: 'string' }, 
            { name: "CoverImageUrl", type: 'string' }, 
            { name: "Text", type: 'string' }, 
            { name: "OriginalUrl", type: 'string' }, 
            { name: "DisplayIndex", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "TypeId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });
}