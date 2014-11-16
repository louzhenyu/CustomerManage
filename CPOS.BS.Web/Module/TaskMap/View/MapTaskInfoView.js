function InitView() {
    
    Ext.create('Jit.button.Button', {
        text: "下发",
        id: "btnSend_btn",
        renderTo: "btnSend",
        //disabled:true,
        //hide: true,
        //width: 50,
        handler: fnSend
        , margin: '0 0 0 0'
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    
    Ext.create('Jit.button.Button', {
        text: "确认下发",
        id: "btnSend2_btn",
        renderTo: "btnSend2",
        //width: 50,
        handler: fnSave
        , margin: '0 0 0 0'
        , jitIsHighlight: true
        , jitIsDefaultCSS: true
    });
    
    Ext.create('Jit.button.Button', {
        text: "返回",
        id: "btnBack",
        renderTo: "btnBack",
        //width: 50,
        handler: function(){
            get("pnl2").style.display = "none";
            get("pnl").style.display = "";
        }
        //, margin: '0 0 0 0'
    });
}