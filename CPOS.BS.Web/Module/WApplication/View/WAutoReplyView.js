function InitView() {
    
    Ext.create('Ext.form.Panel', {
        id: 'searchPanel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'span_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        items: [
        {
            xtype: "jitbizwapplicationinterface",
            fieldLabel: "微信账号",
            id: "txtApplicationId",
            name: "ApplicationId",
            jitSize: 'small',
            fn: function() {
                Ext.Ajax.request({
                    url: JITPage.HandlerUrl.getValue() + "&method=get_WAutoReply_by_id",
                    params: { WAutoReplyId: "1" },
                    method: 'POST',
                    success: function (response) {
                        var d = Ext.decode(response.responseText).data;
                        if (d != null) {
                            Ext.getCmp("txtModelId").fnLoad(function(){
                                    if (Ext.getCmp("txtApplicationId").jitGetValue() != "") 
                                        Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
                                    else
                                        Ext.getCmp("txtModelId").jitSetValue("");
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
                    Ext.Ajax.request({
                        url: JITPage.HandlerUrl.getValue() + "&method=get_WAutoReply_by_id",
                        params: { WAutoReplyId: "1" },
                        method: 'POST',
                        success: function (response) {
                            var d = Ext.decode(response.responseText).data;
                            if (d != null) {
                                Ext.getCmp("txtModelId").fnLoad(function(){
                                    if (Ext.getCmp("txtApplicationId").jitGetValue() == Ext.getCmp("txtApplicationId").ApplicationId) 
                                        Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
                                    else
                                        Ext.getCmp("txtModelId").jitSetValue("");
                                });
                            }
                        }, 
                        failure: function () {
                            Ext.Msg.alert("提示", "获取参数数据失败");
                        }
                    });

                }
            }
        }
        ,{
            xtype: "jitbizwmodel",
            fieldLabel: "模板",
            id: "txtModelId",
            name: "ModelId",
            jitSize: 'small',
            c: true,
            parent_id: "txtApplicationId"
        }
        ]
    });

    Ext.create('Ext.form.Panel', {
        id: 'btn_panel',
        layout: {
            type: 'table',
            columns: 4
        },
        renderTo: 'btn_panel',
        padding: '10 0 0 0',
        bodyStyle: 'background:#F1F2F5;',
        border: 0,
        height: 42,
        items: [
        {
            xtype: "jitbutton",
            text: "保存",
            margin: '0 0 10 14',
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        }
        ]
    });

}