function InitView() {

    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 200
        //            , listeners: {
        //                select: function (store) {
        //                    //  Ext.getCmp("txtWModel").setDefaultValue("");
        //                }
        //            }
    });
    //    Ext.create('jit.biz.WModel', {
    //        id: "txtWModel",
    //        text: "",
    //        renderTo: "txtWModel",
    //        width: 230,
    //        c: true,
    //        parent_id: "txtApplicationId"
    //        , listeners: {
    //            select: function (sender, value, event) {
    //                fnGetWMaterialTextById(value[0].data.id);
    //            }
    //        }
    //    });

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 66,
        plain: true,
        activeTab: 0,
        defaults: {
            bodyPadding: 0
        },
        items: [{
            contentEl: 'tabInfo',
            title: '模板选择'
        }
        ]
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtContent",
        text: "",
        renderTo: "txtContent",
        margin: '0 0 0 10',
        width: 405,
        height: 260
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
            text: "发送",
            formBind: true,
            disabled: true
            , handler: fnPushWeixin
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭"
            , handler: fnClose
        }
        ]
    });
}