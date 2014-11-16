function InitVE() {
    Ext.define("WAutoReplyViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ReplyId", type: 'string' }, 
            { name: "NewsType", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "CreateByName", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "ModelId", type: 'string' }, 
            { name: "ModelName", type: 'string' }
            ]
    });

}