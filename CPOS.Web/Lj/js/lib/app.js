requirejs.config({
    //默认相对"js/lib"解析模块ID
    baseUrl: 'js/',
    //如果模块ID以"app"开始，会相对js/app目录解析，path config是相对 baseUrl 解析的，而且不用包含".js"，因为 path config 指向的可能是目录
    paths: {
        app: '../app'
    }
});
requirejs(['iscroll', 'jquery-1.8.3.min'],
function($) {
    requirejs(["CommonBase", "smart", "blocksit.min", 'jQueryRotate.2.2', 'jquery.easing.min'],
    function () {
        requirejs(['CommonV1', 'ShopVipCommon', 'LzljBusiness'],
            function(){
                requirejs(['ZN', 'lottery', 'register'],
                    function() {
                        if(G("skin.unitName")==null || G("skin.unitName")=="" || G("skin.loginImage")==""){
                            getConfig();
                        }
                        if (Von != "") {
                            eval(Von);
                        }
                    }
            )
            })
       });
});