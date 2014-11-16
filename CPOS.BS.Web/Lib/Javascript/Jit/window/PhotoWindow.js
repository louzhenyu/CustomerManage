
Ext.define('Jit.window.PhotoWindow', {
    alias: 'widget.jitphotowindow',

    constructor: function (args) {
        var me = this;
        var defaultConfig = {
            id: '__PhotoWindowID'   //panel 的id 默认为mapSelect          
            , renderTo: null //panel的renderTo          
            , photoTitle: '照片上传'
            , pClientID: 0
            , pClientUserID: 0
        }
        args = Ext.applyIf(args, defaultConfig);
        if (__clientid != null) {
            args.pClientID = __clientid;
        }
        //照片窗体控件
        //照片panel
        me.photoPanelImg = Ext.create('Ext.panel.Panel', {
            width: 490,
            height: 325,
            columnWidth: 1,
            html: '<div style="width:488px;height:325px; text-align:center;padding-top:5px"><img id="__img' + args.id + '" style="max-width:480px;max-height:280px" src="' + '/File/MobileDevices/Photo/' + args.pClientID + '/' + args.pClientUserID + '/' + args.value + '"></div>',
            layout: 'column',
            border: 0
        });

        if (Ext.getCmp(args.id) != null) {
            Ext.getCmp(args.id).destroy();
        }
        //上传图片的window
        var instance = Ext.create('Jit.window.Window', {
            id: args.id,
            title: args.photoTitle,
            items: [me.photoPanelImg],
            width: 500,
            height: 325,
            jitSize: "custom",
            constrain: true,
            modal: true
        });
    }
});