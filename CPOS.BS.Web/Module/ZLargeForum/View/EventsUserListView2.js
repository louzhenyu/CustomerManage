function InitView() {

    
    Ext.create('Jit.button.Button', {
        text: "查询",
        renderTo: "btnSearch",
        handler: fnSearch
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
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
        buttons: [
        {
            xtype: "jitbutton",
            text: "关闭",
            handler: fnClose
        }]
    });
}