
// Window 业务控件
Ext.define('jit.biz.SkuPropCfg', {
    alias: 'widget.jitbizskupropcfg',
    constructor: function (args) {
        var z_sku_prop_cfg;
//        if (z_sku_prop_cfg != undefined && z_sku_prop_cfg != null)
//            return z_sku_prop_cfg;
        Ext.Ajax.request({
            method: 'GET',
            url: '/Framework/Javascript/Biz/Handler/SkuPropCfgHandler.ashx?method=sku_prop_cfg',
            params: { },
            sync: true,
            async : false,
            success: function(result, request) {
                var d =  Ext.decode(result.responseText);
                if (d.data != null) {
                    z_sku_prop_cfg = d.data;
                } else {
                    showInfo("页面超时，请重新登录");
                }
            },
            failure : function(result) {
                showInfo("错误：" + result.responseText);
            }
        });
        return z_sku_prop_cfg;
  }
})