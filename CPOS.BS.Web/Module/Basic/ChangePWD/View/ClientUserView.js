function InitView() {
    Ext.create('Ext.form.Panel', {
        title: '密码管理',
        id: "editPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:10px',
        layout: "anchor",
        items: [{
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>用户名",
            name: "UserName",
            id: "Username",
            disabled: true,
            allowBlank: false
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>旧密码",
            name: "UserPWD",
            id: "UserPWD",
            inputType: "password",
            allowBlank: false
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>新密码",
            name: "NewPWD",
            id: "NewUserPWD",
            inputType: "password",
            allowBlank: false
        }, {
            xtype: "jittextfield",
            fieldLabel: "<font color='red'>*</font>新密码确认",
            name: "PNewPWD",
            id: "pNewUserPWD",
            inputType: "password",
            allowBlank: false
        }],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            imgName: 'save',
            id: 'btnSave',
            isImgFirst: true,
            //formBind: true,
            //disabled: true,
            //hidden: __getHidden("setpwd"),
            handler: fnSubmit
        }],
        renderTo: "DivGridView"
    });
}