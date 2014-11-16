requirejs.config({
    //默认相对"js/lib"解析模块ID
    baseUrl: 'js/',
    //如果模块ID以"app"开始，会相对js/app目录解析，path config是相对 baseUrl 解析的，而且不用包含".js"，因为 path config 指向的可能是目录
    paths: {
        app: '../app'
    }
});
requirejs(['iscroll', 'jquery1.6'],
function($) {
    requirejs(["CommonBase", "smart"],
    function() {
        requirejs(['CommonV1', 'ShopVipCommon'],
        function() {
            if (Von != "") {
                eval(Von);
            }

        })

    })
});