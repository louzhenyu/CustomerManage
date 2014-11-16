function InitVE() {
    Ext.define("TagsViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "TagsId", type: 'string' }, 
            { name: "TagsName", type: 'string' }, 
            { name: "TagsDesc", type: 'string' }, 
            { name: "TagsFormula", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "CreateByName", type: 'string' }, 
            { name: "TypeName", type: 'string' }, 
            { name: "StatusName", type: 'string' }, 
            { name: "VipCount", type: 'string' }
            ]
    });

}