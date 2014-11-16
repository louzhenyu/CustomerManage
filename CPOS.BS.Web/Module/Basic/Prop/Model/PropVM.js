function InitVE() {
    Ext.define("PropViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ID", type: 'string' }, 
            { name: "Name", type: 'string' }, 
            { name: "ParentId", type: 'string' }, 
            { name: "prop_code", type: 'string' }, 
            { name: "prop_id", type: 'string' }, 
            { name: "prop_code", type: 'string' }, 
            { name: "prop_name", type: 'string' }, 
            { name: "prop_eng_name", type: 'string' }, 
            { name: "prop_type", type: 'string' }, 
            { name: "parent_prop_id", type: 'string' }, 
            { name: "prop_level", type: 'string' }, 
            { name: "prop_domain", type: 'string' }, 
            { name: "prop_input_flag", type: 'string' }, 
            { name: "prop_max_length", type: 'string' }, 
            { name: "prop_default_value", type: 'string' }, 
            { name: "prop_status", type: 'string' }, 
            { name: "display_index", type: 'string' }, 
            { name: "create_user_id", type: 'string' }, 
            { name: "create_time", type: 'string' }, 
            { name: "modify_user_id", type: 'string' }, 
            { name: "modify_time", type: 'string' }
            ]
    });

    Ext.define("PropViewEntity2", {
        extend: "Ext.data.Model",
        fields: [
            { name: "Prop_Id", type: 'string' }, 
            { name: "Prop_Code", type: 'string' }, 
            { name: "Prop_Name", type: 'string' }, 
            { name: "Prop_Eng_Name", type: 'string' }, 
            { name: "Prop_Type", type: 'string' }, 
            { name: "Prop_Type_Name", type: 'string' }, 
            { name: "Parent_Prop_id", type: 'string' }, 
            { name: "Prop_Level", type: 'string' }, 
            { name: "Prop_Domain", type: 'string' }, 
            { name: "Prop_Input_Flag", type: 'string' }, 
            { name: "Prop_Max_Length", type: 'string' }, 
            { name: "Prop_Default_Value", type: 'string' }, 
            { name: "Prop_Status", type: 'string' }, 
            { name: "Display_Index", type: 'string' }, 
            { name: "Create_User_Id", type: 'string' }, 
            { name: "Create_Time", type: 'string' }, 
            { name: "Modify_User_Id", type: 'string' }, 
            { name: "Modify_Time", type: 'string' }, 
            { name: "Prop_Status_Desc", type: 'string' }, 
            { name: "Create_User_Name", type: 'string' }, 
            { name: "Modify_User_Name", type: 'string' }, 
            { name: "Parent_Prop_Name", type: 'string' }, 
            { name: "CreateByName", type: 'string' }
            ]
    });
    
    Ext.define("BrandDetailViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "BrandId", type: 'string' }, 
            { name: "BrandName", type: 'string' }, 
            { name: "BrandCode", type: 'string' }, 
            { name: "BrandDesc", type: 'string' }, 
            { name: "BrandEngName", type: 'string' }, 
            { name: "BrandLogoURL", type: 'string' }, 
            { name: "Tel", type: 'string' }, 
            { name: "DisplayIndex", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }, 
            { name: "CreateByName", type: 'string' }
            ]
    });
    
    Ext.define("ItemImageViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ImageId', type: 'string' },
            { name: 'ObjectId', type: 'string' },
            { name: 'ImageURL', type: 'string' },
            { name: 'DisplayIndex', type: 'string' }
            ]
    });
}