function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 451,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo',
            title: '基本信息'
        }
        ]
    });
    Ext.create('Jit.form.field.Checkbox', {
        id: "ckIsMoreCS",
        renderTo: "ckIsMoreCS",
        value: true
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtWeiXinName",
        text: "",
        renderTo: "txtWeiXinName",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtWeiXinID",
        text: "",
        renderTo: "txtWeiXinID",
        width: 405
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtURL",
        text: "",
        renderTo: "txtURL",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtToken",
        text: "",
        renderTo: "txtToken",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtAppID",
        text: "",
        renderTo: "txtAppID",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtAppSecret",
        text: "",
        renderTo: "txtAppSecret",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtServerIP",
        text: "",
        renderTo: "txtServerIP",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtFileAddress",
        text: "",
        renderTo: "txtFileAddress",
        width: 405
    });
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsHeight",
        text: "",
        renderTo: "txtIsHeight",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtLoginUser",
        text: "",
        renderTo: "txtLoginUser",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtLoginPass",
        text: "",
        renderTo: "txtLoginPass",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtCustomerId",
        text: "",
        renderTo: "txtCustomerId",
        width: 405
    });

    Ext.create('jit.biz.WeiXinType', {
        id: "txtWeiXinType",
        text: "",
        renderTo: "txtWeiXinType",
        width: 100
    });
    //加解密字段，2014-10-21
    //Ext.create('Jit.form.field.ComboBox', {
    //    id: "txtEncryptType",
    //    renderTo: "txtEncryptType",
    //    width: 100,
    //    store: Ext.getStore("EncryptTypeStore"),
    //    valueField: "Id",
    //    displayField: "Name",
    //    isDefault: true
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtCurrentAESKey",
    //    text: "",
    //    renderTo: "txtCurrentAESKey",
    //    width:405
    //});
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtPrevAESKey",
    //    text: "",
    //    renderTo: "txtPrevAESKey",
    //    width:405
    //});

    Ext.create('Jit.form.field.Text', {
        id: "txtAuthUrl",
        text: "",
        renderTo: "txtAuthUrl",
        width: 405
    });

    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        bodyStyle: 'background:#F1F2F5;padding-top:0px;padding-bottom:0px;border:0px;',
        //layout: 'anchor',
        layout: {
            type: 'table'
            , columns: 3
            , align: 'right'
        },
        defaults: {},

        items: [
        ]
        , buttonAlign: "left"
        , buttons: [
        {
            xtype: "jitbutton",
            text: "保存",
            formBind: true,
            disabled: true,
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });

}