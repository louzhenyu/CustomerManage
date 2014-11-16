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
    

    Ext.create('Jit.Biz.WMenuSelectTree', {
        id: "txtParentId",
        renderTo: "txtParentId",
        width: 405
        //fn: function(e) {
        //    Ext.Ajax.request({
        //        url: JITPage.HandlerUrl.getValue() + "&method=get_wmenu_by_id",
        //        params: { WMenuId: e.data.id },
        //        method: 'POST',
        //        success: function (response) {
        //            var d = Ext.decode(response.responseText).data;
        //            if (d != null) {
        //                Ext.getCmp("txtLevel").jitSetValue(parseInt(d.Level)+1);
        //            }
        //        }
        //    });
        //}
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtName",
        text: "",
        renderTo: "txtName",
        width: 405
    });
    Ext.create('Jit.form.field.Text', {
        id: "txtMenuURL",
        text: "",
        renderTo: "txtMenuURL",
        width: 405
    });
    
    //Ext.create('jit.biz.WApplicationInterface', {
    //    id: "txtWeiXinID",
    //    text: "",
    //    renderTo: "txtWeiXinID",
    //    width: 405
    //});
    
    //Ext.create('Jit.form.field.Text', {
    //    id: "txtKey",
    //    text: "",
    //    renderTo: "txtKey",
    //    width: 405
    //});
    Ext.create('jit.biz.WMenuType', {
        id: "txtType",
        text: "",
        renderTo: "txtType",
        width: 405
    });
    //Ext.create('Jit.form.field.Number', {
    //    id: "txtLevel",
    //    text: "",
    //    renderTo: "txtLevel",
    //    value: 1,
    //    //minValue: 1,
    //    //maxValue: 3,
    //    //readOnly: true,
    //    width: 405
    //});
    Ext.create('Jit.form.field.Number', {
        id: "txtDisplayColumn",
        text: "",
        renderTo: "txtDisplayColumn",
        width: 405
    });
    Ext.create('jit.biz.WModel', {
        id: "txtModelId",
        text: "",
        renderTo: "txtModelId",
        width: 405,
        c: false
    });
    //Ext.create('Jit.form.field.TextArea', {
    //    id: "txtMaterialTypeName",
    //    text: "",
    //    renderTo: "txtMaterialTypeName",
    //    readOnly: true,
    //    margin: "0 0 0 10",
    //    width: 405,
    //    height: 100
    //});
    
    Ext.create('Jit.button.Button', {
        id: "txtReset" + "_ext",
        text: "重置",
        renderTo: "txtReset",
        margin: '0 0 0 10',
        width: 50,
        hidden: false,
        handler: function () {
            Ext.getCmp("txtParentId").jitSetValue([{ "id": "", "text": "" }]);
            //Ext.getCmp("txtLevel").jitSetValue(1);
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