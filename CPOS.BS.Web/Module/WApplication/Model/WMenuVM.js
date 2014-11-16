function InitVE() {
    Ext.define("WMenuViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ID", type: 'string' }, 
            { name: "WeiXinID", type: 'string' }, 
            { name: "ParentId", type: 'string' }, 
            { name: "ParentName", type: 'string' }, 
            { name: "Name", type: 'string' }, 
            { name: "Key", type: 'string' }, 
            { name: "Type", type: 'string' }, 
            { name: "Level", type: 'string' }, 
            { name: "DisplayColumn", type: 'string' }, 
            { name: "MaterialTypeId", type: 'string' }, 
            { name: "MaterialTypeName", type: 'string' }, 
            { name: "Text", type: 'string' }, 
            { name: "ImageId", type: 'string' }, 
            { name: "TextId", type: 'string' }, 
            { name: "VoiceId", type: 'string' }, 
            { name: "VideoId", type: 'string' }, 
            { name: "ModelId", type: 'string' }, 
            { name: "ModelName", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

}