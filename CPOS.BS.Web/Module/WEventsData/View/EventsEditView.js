function InitView() {

    //editPanel area

    //jifeng.cao begin
    Ext.create('Jit.biz.Options', {
        id: "cmbIsDefault",
        OptionName: 'YES/NO',
        renderTo: "cmbIsDefault",
        width: 183
    });
    Ext.create('Jit.biz.Options', {
        id: "cmbIsTop",
        OptionName: 'YES/NO',
        renderTo: "cmbIsTop",
        width: 183
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtOrganizer",
        text: "",
        renderTo: "txtOrganizer",
        width: 405
    });   
 
    //jifeng.cao end


    Ext.create('Jit.Biz.LEventSelectTree', {
        id: "txtParentEvent",
        text: "",
        renderTo: "txtParentEvent",
        width: 100
    });

    Ext.create('Jit.form.field.Date', {
        id: "txtStartDate",
        text: "",
        renderTo: "txtStartDate",
        width: 100
    });
    Ext.create('Jit.form.field.Date', {
        id: "txtEndDate",
        text: "",
        renderTo: "txtEndDate",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtStartTime",
        text: "",
        value: "00:00",
        renderTo: "txtStartTime",
        maxLength:5,
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEndTime",
        text: "",
        value: "00:00",
        renderTo: "txtEndTime",
        maxLength:5,
        width: 100
    });


    Ext.create('Jit.form.field.Text', {
        id: "txtTitle",
        text: "",
        renderTo: "txtTitle",
        width: 405
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtCityId",
        text: "",
        renderTo: "txtCityId",
        width: 100
    });
    //Ext.create('Jit.Biz.CitySelectTree', {
    //    id: "txtCityId",
    //    text: "",
    //    renderTo: "txtCityId",
    //    width: 100
    //});

    Ext.create('Jit.form.field.Text', {
        id: "txtContact",
        text: "",
        renderTo: "txtContact",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtPhoneNumber",
        text: "",
        renderTo: "txtPhoneNumber",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtEmail",
        text: "",
        renderTo: "txtEmail",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtAddress",
        text: "",
        renderTo: "txtAddress",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtImageUrl",
        text: "",
        renderTo: "txtImageUrl",
        //readOnly: true,
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtUrl",
        text: "",
        renderTo: "txtUrl",
        //readOnly: true,
        width: 405
    });

    //Ext.create('jit.biz.EventCheckinType', {
    //    id: "txtCheckinType",
    //    text: "",
    //    renderTo: "txtCheckinType",
    //    width: 100
    //});
    
    //Ext.create('jit.biz.EventRange', {
    //    id: "txtCheckinRange",
    //    text: "",
    //    renderTo: "txtCheckinRange",
    //    width: 100
    //});
    
    Ext.create('jit.biz.QuestionnaireType', {
        id: "txtApplyQues",
        text: "",
        renderTo: "txtApplyQues",
        width: 100
    });

    Ext.create('jit.biz.QuestionnaireType', {
        id: "txtPollQues",
        text: "",
        renderTo: "txtPollQues",
        width: 100
    });
    
    Ext.create('jit.biz.WEventAdmin', {
        id: "txtWEventAdmin",
        text: "",
        renderTo: "txtWEventAdmin",
        width: 100
    });
    
    Ext.create('jit.biz.WApplicationInterface2', {
        id: "txtWeiXinPublic",
        text: "",
        renderTo: "txtWeiXinPublic",
        width: 100
            ,fn: function() {
                Ext.getCmp("txtModelId").setDefaultValue("");
                Ext.Ajax.request({
                    url: "/Module/WEventsData/Handler/EventsHandler.ashx?method=get_events_by_id",
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
                                    if (Ext.getCmp("txtWeiXinPublic").jitGetValue() != "") 
                                        Ext.getCmp("txtModelId").jitSetValue(getStr(d.ModelId));
                                    else
                                        Ext.getCmp("txtModelId").jitSetValue("");
                                });
                            Ext.getCmp("txtWXCode").fnLoad(function(){
                                    if (Ext.getCmp("txtWeiXinPublic").jitGetValue() != "") 
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
                    Ext.getCmp("txtModelId").setDefaultValue("");
                    Ext.getCmp("txtWXCode").setDefaultValue("");
                }
            }
    });
    Ext.create('jit.biz.WModel', {
        id: "txtModelId",
        text: "",
        renderTo: "txtModelId",
        parent_id: "txtWeiXinPublic",
        type: "2",
        width: 100,
        c: true,
        parent_id: "txtWeiXinPublic"
    });
    
    Ext.create('Jit.form.field.Text', {
        id: "txtLongitude",
        text: "",
        renderTo: "txtLongitude",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtLatitude",
        text: "",
        renderTo: "txtLatitude",
        width: 100
    });
    Ext.create('jit.biz.EventStatus', {
        id: "txtEventStatus",
        text: "",
        renderTo: "txtEventStatus",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtDisplayIndex",
        text: "",
        renderTo: "txtDisplayIndex",
        width: 100
    });
    Ext.create('Jit.form.field.Number', {
        id: "txtPersonCount",
        value: "0",
        renderTo: "txtPersonCount",
        width: 100
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtWXCode2",
        text: "",
        renderTo: "txtWXCode2",
        readOnly: true,
        width: 100
    });
    Ext.create('Jit.button.Button', {
        text: "获取二维码",
        renderTo: "btnWXImage",
        handler: function () {
            fnGetWXCode();
        }
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtDimensionalCodeURL",
        text: "",
        renderTo: "txtDimensionalCodeURL",
        readOnly: true,
        width: 330
    });
    Ext.create('jit.biz.WQRCodeType', {
        id: "txtWXCode",
        text: "",
        renderTo: "txtWXCode",
        width: 100,
        c: true,
        parent_id: "txtWeiXinPublic"
    });

    var content = new Ext.form.TextArea({
        height: 10,
        id: 'txtContent',
        renderTo: "txtContent",
        anchor: '80%',
        listeners: {
            "render": function (f) {
                K = KindEditor;
                htmlEditor = K.create('#txtContent', {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    height: 270,
                    width: '100%'
                });
            }
        }
    });


    //operator area
    Ext.create('Ext.form.Panel', {
        title: null,
        renderTo: "divBtn",
        id: "editBtnPanel",
        width: "100%",
        height: "100%",
        border: 1,
        layout: {
            type: 'table',
            columns: 3,
            align: 'right'
        },
        defaults: {},
        items: [],
        buttonAlign: "left",
        buttons: [{
            xtype: "jitbutton",
            id: "btnSave",
            text: "保存",
            formBind: true,
            disabled: true,
            hidden: false,
            handler: fnSave
            , jitIsHighlight: true
            , jitIsDefaultCSS: true
        },
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}