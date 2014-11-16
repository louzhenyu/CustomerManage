function InitVE() {
    Ext.define("UserRoleViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' }, 
            { name: 'UserId', type: 'string' }, 
            { name: 'RoleId', type: 'string' }, 
            { name: 'RoleName', type: 'string' }, 
            { name: 'UnitId', type: 'string' }, 
            { name: 'UnitName', type: 'string' }, 
            { name: 'DefaultFlag', type: 'string' }, 
            { name: 'DefaultFlagDesc', type: 'string' }
            ]
    });

}