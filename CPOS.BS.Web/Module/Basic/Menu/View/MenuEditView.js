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

    Ext.create('jit.biz.AppSys', {
        id: "txtAppSys",
        text: "",
        renderTo: "txtAppSys",
        width: 100,
        selectFn: function () {
            fnLoadMenus(Ext.getCmp("txtAppSys").jitGetValue(), getUrlParam("menu_id"));
        }
    });

//    Ext.create('Jit.Biz.MenuSelectTree', {
//        id: "txtParentMenuId",
//        text: "",
//        renderTo: "txtParentMenuId",
//        width: 100

//    });

    Ext.create('Jit.form.field.Text', {
        id: "txtMenuCode",
        text: "",
        renderTo: "txtMenuCode",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtMenuName",
        text: "",
        renderTo: "txtMenuName",
        width: 100
    });

    Ext.create('Jit.form.field.Text', {
        id: "txtClass",
        text: "",
        renderTo: "txtClass",
        width: 100
    });



    Ext.create('Jit.form.field.Text', {
        id: "txtUrl",
        text: "",
        renderTo: "txtUrl",
        width: 312
    });

    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayIndex",
        value: "0",
        renderTo: "txtDisplayIndex",
        width: 100
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