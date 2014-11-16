require.config({

    baseUrl: "../",

    paths: {
        'jquery': 'lib/jquery-1.8.3.min',
        'zepto': 'lib/zepto.min',
        'jit': 'lib/jit-lib'
    }
});

require(['zepto', '../lib/jit-lib.js?version=' + ((new Date()).getTime())], function () {

    $ = Zepto;

    var version = Jit.version();

    var PageConfigs = Jit.AM.getAppPageConfig();

    if (!Jit.AM.getAppPageConfig()) {

        var rst = $.ajax({
            url: "../config.js",
            async: false,
            cache: false
        });

        var vcfg = Jit.strToJson(rst.responseText);

        Jit.AM.setAppPageConfig(vcfg);

        PageConfigs = Jit.AM.getAppPageConfig();
    }


    var htmlname = $('title').attr('name');

    if (!htmlname) {

        alert('Html 中页面名称未定义');

        return;
    }

    var pageconfig = PageConfigs[htmlname];

    if (!pageconfig) {

        alert('Config.js 中未找到对应的HTML');

        return;
    }

    $('title').html(pageconfig.title);


    function loadFiles(list, type, callback) {

        if (!$.isArray(list))
            return;

        var js_arr = [],
			type = (type == 'plugin' ? '../plugin/' : '../js/');

        $.each(list, function (key, val) {

            js_arr.push(type + val + '.js?version=' + version);

        });

        require(js_arr, callback);
    }
    if (pageconfig) {

        var plugins = pageconfig.plugin;

        var plugins = pageconfig.plugin;

        if ($.isArray(plugins) && plugins.length > 0) {

            loadFiles(plugins, 'plugin', function () {

                var scripts = pageconfig.script;

                loadFiles(scripts, 'script', main);
            });

        } else {

            var scripts = pageconfig.script;

            loadFiles(scripts, 'script', main);
        }
    }

    function main() {

        Jit.AM.weiXinToolBar(false);

        if (Jit.AM.getUrlParam('openId')) {

            var cfg = Jit.AM.getAppConfig(),
				keys = cfg.AJAX_PARAMS.split(',');

            var param = {};

            for (var key in keys) {

                param[keys[key]] = Jit.AM.getUrlParam(keys[key]);
            }

            Jit.AM.setBaseAjaxParam(param);
        } else {
            /*;*/
            Jit.AM.setBaseAjaxParam({
                'openId': 'oUcanjuyG9hSplcZ8mSTBg-0airY',
                'userId': 'oUcanjuyG9hSplcZ8mSTBg-0airY',
                'customerId': 'c97fb511416d4370bfb1b87e62d54e0b',
                'locale': 'ch'
            })
        }

        Jit.AM.onLoad();
    }
});

(function (global) {

})(window)

