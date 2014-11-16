function InitVE() {
    Ext.define("VipLevelEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "VipCardGradeID", type: 'string' },
            { name: "VipCardGradeName", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "IsDelete", type: 'string' },
            { name: "CustomerID", type: 'string' },
            { name: "AddUpAmount", type: 'string' },
            { name: "IsExpandVip", type: 'string' },
            { name: "PreferentialAmount", type: 'string' },
            { name: "SalesPreferentiaAmount", type: 'string' },
            { name: "BeVip", type: 'string' },
            { name: "Remark", type: 'string' },
            { name: "IntegralMultiples", type: 'string' }
            ,{ name: "VipLevelCount", type: 'string' }
            ]
    });

}