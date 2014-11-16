
// Window 业务控件
Ext.define('jit.biz.Window', {
    alias: 'widget.jitbizwindow',
    constructor: function (args) {
        var width = 0;
        var height = 0;
        var jitSize = args.jitSize.toString().toLowerCase();
        switch (jitSize) {
            case 'small':
                {
                    width = 300;
                    height = 150;
                }
                break;
            case 'big':
                {
                    width = 680;
                    height = 250;
                }
                break;
            case 'large':
                {
                    width = 900;
                    height = 400;
                }
                break;
        }
        return Ext.create('Jit.window.Window', {
            closeAction:'destroy',
            closable:true,

            jitSize: args.jitSize,
            id: args.id,
            title: args.title,
            height: args.height,
                
	        html:'<div style="width:'+width+'px;height:'+args.height+'px;-webkit-overflow-scrolling:touch; overflow: scroll;">  <iframe scrolling="'+ args.scrolling +'" style="margin-left:0px;margin-top:0px;border:0;' +
	            'border-style:solid;border-color:red;" width="100%" height="100%" ' + 
	            ' id="frmWin'+ args.id +'" src="' + args.url + '" name="'+ args.id +'" /></div>'

        });
  }
})