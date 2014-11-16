function InitView() {
    
    //Ext.create('Jit.Biz.UnitSelectTree', {
    //    id: "txtUnit",
    //    text: "",
    //    renderTo: "txtUnit",
    //    width: 100
    //});


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
        , buttons: [{
            xtype: "jitbutton",
            text: "保存",
            handler: fnSave
            , jitIsHighlight: true
        , jitIsDefaultCSS: true
        }
        ,
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }
        ]
    });


}