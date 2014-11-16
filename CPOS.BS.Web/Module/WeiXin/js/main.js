//当body标签添加cache属性时，查找version值作为版本号，如果没有version属性，则缓存版本十二小时
var appConfig = {
    cache: document.body.hasAttribute("cache"),
    version: document.body.hasAttribute("cache") && document.body.hasAttribute("version") ? document.body.getAttribute("version") : Math.floor(new Date().getTime() / (1000 * 60 * 60 * 12))
}

// 路径定义
require.config({
    urlArgs: "v=" + (appConfig.cache ? appConfig.version : (new Date()).getTime()),
    baseUrl: '',
    shim: {
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        },
        'tools': {
            deps: ['jquery'],
            exports: 'tools'
        },
        'lang': {
            deps: ['kindeditor']
        },
        'drag': {
            deps: ['jquery']
        }
    },
    paths: {
        drag: '/Module/static/js/plugin/jquery.drag',
        jquery: '/Module/static/js/lib/jquery-1.8.3.min',
        tools: '/Module/static/js/lib/tools-lib',
        template: '/Module/static/js/lib/bdTemplate',
        kindeditor: '/Framework/Javascript/Other/kindeditor/kindeditor',
        lang: '/Framework/Javascript/Other/kindeditor/lang/zh_CN',
        pagination: '/Module/static/js/plugin/jquery.jqpagination',
        json2: '/Module/static/js/plugin/json2'
    }
});

define(['jquery'], function ($) {
    if (window.ActiveXObject) {//判断浏览器是否属于IE
        var browser = navigator.appName
        var b_version = navigator.appVersion
        var version = b_version.split(";");
        var trim_Version = version[1].replace(/[ ]/g, "");
        if (browser == "Microsoft Internet Explorer") {
            require(['json2'], function () {
                var pageJs = $("#section").data("js"),
                pageJsPrefix = '';
                if (pageJs.length) {
                    var arr = pageJs.split(" ");
                    for (var i = 0; i < arr.length; i++) {
                        arr[i] = pageJsPrefix + arr[i];
                    }
                    require([arr.join(",")], function () { });
                }
            })
        }
    } else {
        var pageJs = $("#section").data("js"),
        pageJsPrefix = '';
        if (pageJs.length) {
            var arr = pageJs.split(" ");
            for (var i = 0; i < arr.length; i++) {
                arr[i] = pageJsPrefix + arr[i];
            }
            require([arr.join(",")], function () { });
        }
    }


});

