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
        id: "txtModelCode",
        text: "",
        renderTo: "txtModelCode",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtModelName",
        text: "",
        renderTo: "txtModelName",
        width: 405
    });
    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 405,
            fn: function() {
                //Ext.getCmp("txtModelId").setDefaultValue("");
                //Ext.Ajax.request({
                //    url: JITPage.HandlerUrl.getValue() + "&method=get_WModelList_by_id",
                //    params: { ModelId: getUrlParam("ModelId") },
                //    method: 'POST',
                //    success: function (response) {
                //        //var d = Ext.decode(response.responseText).data;
                //        //if (d != null) {
                //        //    Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                //        //}
                //    },
                //    failure: function () {
                //        Ext.Msg.alert("提示", "获取参数数据失败");
                //    }
                //});
            }
            ,listeners: {
                select: function (store) {
                    //Ext.getCmp("txtModelId").setDefaultValue("");
                    //Ext.getCmp("txtModelId").jitSetValue("");
                }
            }
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