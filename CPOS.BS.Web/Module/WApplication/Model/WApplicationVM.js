function InitVE() {
    Ext.define("WApplicationViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ApplicationId", type: 'string' }, 
            { name: "WeiXinName", type: 'string' }, 
            { name: "WeiXinID", type: 'string' }, 
            { name: "URL", type: 'string' }, 
            { name: "Token", type: 'string' }, 
            { name: "AppID", type: 'string' }, 
            { name: "AppSecret", type: 'string' }, 
            { name: "ServerIP", type: 'string' }, 
            { name: "FileAddress", type: 'string' }, 
            { name: "IsHeight", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "CreateByName", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "LoginUser", type: 'string' }, 
            { name: "LoginPass", type: 'string' },
            { name: "CustomerId", type: 'string' }
            , { name: "WeiXinTypeId", type: 'string' }
            , { name: "AuthUrl", type: 'string' }
            //加解密字段,2014-10-21 zoukun
            , { name: "PrevEncodingAESKey", type: 'string' }
            , { name: "CurrentEncodingAESKey", type: 'string' }
            , { name: "EncryptType", type: 'string' }
            ]
    });

}