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
    
    
    Ext.create('Jit.form.field.Number', {
        id: "txtQRCode",
        text: "",
        renderTo: "txtQRCode",
        width: 100
    });
    
    
    Ext.create('jit.biz.YesNoStatus', {
        id: "txtIsUse",
        text: "",
        renderTo: "txtIsUse",
        width: 100
    });

    Ext.create('jit.biz.WApplicationInterface', {
        id: "txtApplicationId",
        text: "",
        renderTo: "txtApplicationId",
        width: 100
            ,fn: function() {
                Ext.getCmp("txtQRCodeTypeId").setDefaultValue("");
                Ext.Ajax.request({
                    url: "/Module/WEvents/Handler/EventsHandler.ashx?method=get_events_by_id",
                    params: { EventID: getUrlParam("EventID") },
                    method: 'POST',sync: true, async: false,
                    success: function (response) {
                        //var d = Ext.decode(response.responseText).topics;
                        //if (d != null) {
                        //    Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
                        //}
                        var d = Ext.decode(response.responseText).topics;
                        if (d != null) {
                            //Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                            Ext.getCmp("txtModelId").fnLoad(function(){
                                    if (Ext.getCmp("txtApplicationId").jitGetValue() != "") 
                                        Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
                                    else
                                        Ext.getCmp("txtModelId").jitSetValue("");
                                });
                            Ext.getCmp("txtWXCode").fnLoad(function(){
                                    if (Ext.getCmp("txtApplicationId").jitGetValue() != "") 
                                        Ext.getCmp("txtWXCode").jitSetValue(getStr(d.QRCodeTypeId));
                                    else
                                        Ext.getCmp("txtWXCode").jitSetValue("");
                                });
                        }
                    },
                    failure: function () {
                        Ext.Msg.alert("提示", "获取参数数据失败");
                    }
                });
            }
            ,listeners: {
                select: function (store) {
                    //Ext.Ajax.request({
                    //    url: "/Module/WApplication/Handler/WApplicationHandler.ashx?method=search_wapplication",
                    //    params: { form: "{ \"WeiXinID\":\"" + Ext.getCmp("txtWeiXinPublic").jitGetValue() + "\" }" },
                    //    method: 'POST',
                    //    sync: true, async: false,
                    //    success: function (response) {
                    //        var d = Ext.decode(response.responseText).data;
                    //        if (d != null) {
                    //            //Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                    //        }
                    //    },
                    //    failure: function () {
                    //        Ext.Msg.alert("提示", "获取参数数据失败");
                    //    }
                    //});
                    //Ext.getCmp("txtModelId").setDefaultValue("");
                    Ext.getCmp("txtQRCodeTypeId").setDefaultValue("");
                }
            }
    });
    Ext.create('jit.biz.WQRCodeType', {
        id: "txtQRCodeTypeId",
        text: "",
        renderTo: "txtQRCodeTypeId",
        width: 100,
        c: true,type: "2",
        parent_id: "txtApplicationId"
    });

    Ext.create('Jit.form.field.TextArea', {
        id: "txtRemark",
        text: "",
        renderTo: "txtRemark",
        width: 400,
        height: 150,
        margin: '0 0 0 10'
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        width: 400
    });
    
    Ext.create('Jit.button.Button', {
        text: "获取二维码",
        renderTo: "btnWXImage",
       
        //hidden: __getHidden("create"),
        handler: function () {
            fnGetWXCode();
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