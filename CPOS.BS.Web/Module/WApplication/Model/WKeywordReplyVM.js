function InitVE() {
    Ext.define("WKeywordReplyViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ReplyId", type: 'string' }, 
            { name: "Keyword", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "WeiXinName", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "CreateByName", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "ModelId", type: 'string' }, 
            { name: "ModelName", type: 'string' }, 
            { name: "MaterialTypeName", type: 'string' }
            ]
    });

}