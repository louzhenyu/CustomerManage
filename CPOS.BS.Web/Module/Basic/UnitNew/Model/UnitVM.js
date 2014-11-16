function InitVE() {
    Ext.define("UnitViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "Id", type: 'string' }, 
            { name: "Parent_Unit_Id", type: 'string' }, 
            { name: "Parent_Unit_Name", type: 'string' }, 
            { name: "Code", type: 'string' }, 
            { name: "Name", type: 'string' }, 
            { name: "EnglishName", type: 'string' }, 
            { name: "ShortName", type: 'string' }, 
            { name: "Status", type: 'string' }, 
            { name: "Status_Desc", type: 'string' }, 
            { name: "Remark", type: 'string' }, 
            { name: "CityName", type: 'string' }, 
            { name: "Contact", type: 'string' }, 
            { name: "Telephone", type: 'string' }, 
            { name: "TypeName", type: 'string' }, 
            { name: "Email", type: 'string' }, 
            { name: "longitude", type: 'string' }, 
            { name: "dimension", type: 'string' },
            { name: "Create_User_Id", type: 'string' }, 
            { name: "Create_User_Name", type: 'string' }, 
            { name: "Create_Time", type: 'string' }, 
            { name: "Modify_User_Id", type: 'string' }, 
            { name: "Modify_User_Name", type: 'string' },
            { name: "Modify_Time", type: 'string' },
            { name: "imageURL", type: 'string' },
            { name: "ftpImagerURL", type: 'string' },
            { name: "webserversURL", type: 'string' },
            { name: "weiXinId", type: 'string' },
            { name: "dimensionalCodeURL", type: 'string' }
            ]
    });

}