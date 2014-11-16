function InitView() {

    var tabs = Ext.widget('tabpanel', {
        renderTo: 'tabsMain',
        width: '100%',
        height: 451,
        plain: true,
        activeTab: 0,
        defaults :{
            bodyPadding: 0
        },
        items: [{
            contentEl:'tabInfo', 
            title: '基本信息'
        }
        ]
    });
    
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWQRCodeTypeCode",
        text: "",
        renderTo: "txtWQRCodeTypeCode",
        width: 100
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtWQRCodeTypeName",
        text: "",
        renderTo: "txtWQRCodeTypeName",
        width: 100
    });
    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 100
            ,listeners: {
                select: function (store) {
                    Ext.getCmp("txtWModel").setDefaultValue("");
                }
                //,'load': function (store, record, opts) {
                //    var firstValue = record[0].data.LawsAndRegulationsID;
                //    Ext.getCmp("txtApplicationId").setValue(firstValue);
                //}
            }
    });
    Ext.create('jit.biz.WModel', {
        id: "txtWModel",
        text: "",
        renderTo: "txtWModel",
        width: 230,
        c: true,
        multiSelect: true,
        parent_id: "txtApplicationId"
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
        ,buttonAlign: "left" 
        ,buttons: [
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