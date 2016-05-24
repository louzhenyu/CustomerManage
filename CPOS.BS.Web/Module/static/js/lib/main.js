//当body标签添加cache属性时，查找version值作为版本号，如果没有version属性，则缓存版本十二小时
var appConfig = {
    cache: document.body.hasAttribute("cache"),
    version: document.body.hasAttribute("cache") && document.body.hasAttribute("version") ? document.body.getAttribute("version") : Math.floor(new Date().getTime() / (1000 * 60 * 60 * 12))
}

// 路径定义
require.config({
    urlArgs: "v=" + (appConfig.cache ? appConfig.version : (new Date()).getTime()),
    baseUrl:'/',   //window.staticUrl ? window.staticUrl:'http://static.uat.chainclouds.com'
    shim: {

        'piwik': {
            deps: ['jquery'],
            exports: 'piwik'
        },
        'piwik_BI': {
            deps: ['piwik'],
            exports: 'piwik_BI'
        },
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        },
        'tools': {
            deps: ['jquery'],
            exports: 'tools'
        },
        'optimize': {
            deps: ['tools'],
            exports: 'optimize'
        },
        'drag': {
            deps: ['jquery']
        },
        'artDialog': {
            deps: ['jquery'],
            exports: 'artDialog'
        },
        'swfobject': {
            deps: ['cookie'],
            exports: 'swfobject'
        },
        'fullAvatarEditor': {
            deps: ['swfobject'],
            exports: 'fullAvatarEditor'
        },
        'swffileupload': {
            deps: ['jquery'],
            exports: 'swffileupload'
        },
        zTree: {
            deps: ['jquery'],
            exports: 'zTree'
        },
        //jvveshowc shim config
        'jquery-jvve': {
            'deps': ['jquery'],
            'init': function(){
                window.jQjvve = jQuery.noConflict(true);
                return window.jQjvve;
            }
        },
        'easyui': {
            deps: ['jqueryui', 'jquery'],
            exports: 'easyui'
        },
        'jquery-event-move': {
            'deps': ['jquery']
        },
        'jquery-event-swipe': {
            'deps': ['jquery-event-move']
        },
        'jquery-slimScroll': {
            'deps': ['jquery']
        },
        'jquery-ui': {
            'deps': ['jquery-jvve']
        },
        'jquery-easy-ui': {
            'deps': ['jquery']
        }
    },
    paths: {

        drag: 'Module/static/js/plugin/jquery.drag',
        copy:'Module/static/js/plugin/jquery.zclip.min',
        tools: 'Module/static/js/lib/tools-lib',
        optimize: 'Module/static/js/lib/optimize',//模板也js处理
        template: 'Module/static/js/lib/bdTemplate',
        artTemplate: 'Module/static/js/plugin/template',
        customerTemp: 'homeIndex/js/tempModel',//微官网配置模板
        easyui: 'Module/static/js/lib/jquery.easyui.min',
        jqueryui: 'Module/static/js/lib/jquery-ui.min',
        langzh_CN: 'Module/static/js/lib/easyui-lang-zh_CN',
        validator: 'Module/static/js/lib/validator',
        jquery: 'Module/static/js/lib/jquery-1.8.3.min',
       // newJquery: 'Withdraw/js/jquery',  //jquery版本兼容问题，待定
        pagination: 'Module/static/js/plugin/jquery.jqpagination',
        json2: 'Module/static/js/plugin/json2',
        kindeditor: 'Framework/javascript/Other/kindeditor/kindeditor',
        artDialog: 'Module/static/js/plugin/artDialog',
        ajaxform: 'Module/static/js/plugin/jquery.form',
        datetimePicker: 'Module/static/js/plugin/jquery.datetimepicker',
        kkpager: 'Module/static/js/plugin/kkpager',
        highcharts: 'Module/static/js/plugin/highcharts',
		//lang: '/Framework/Javascript/Other/kindeditor/lang/zh_CN',
        bxslider: 'Module/static/js/plugin/jquery.bxslider',
        touchslider: 'Module/static/js/plugin/touchslider',
        nicescroll:'Module/static/js/plugin/jquery.nicescroll',

        'jquery-jvve': 'Module/static/js/vendor/jquery-2.2.0/jquery',
        'jquery-ui-jvve': 'Module/static/js/vendor/jquery-ui-1.11.4/jquery-ui-jvve',
        'caman': 'Module/static/js/vendor/CamanJS-4.1.2/caman',
        'jquery-event-move': 'Module/static/js/vendor/jquery-event-move-1.3.6/jquery.event.move',
        'jquery-event-swipe': 'Module/static/js/vendor/jquery-event-swipe-0.5/jquery.event.swipe',
        'jquery-slimScroll': 'Module/static/js/vendor/jquery-slimScroll-1.3.7/jquery.slimscroll',
        //统计代码
        piwik:'Module/static/js/Piwik/piwik',
        piwik_BI:'Module/static/js/Piwik/piwik_BI'
    }
});

define(['jquery', 'piwik', 'piwik_BI', 'optimize'], function () {
        var pageJs = $("#section").data("js"),
        pageJsPrefix = '';
        if (pageJs.length) {
            var arr = pageJs.split(" ");
            for (var i = 0; i < arr.length; i++) {
                arr[i] = pageJsPrefix + arr[i];
            }
            require([arr.join(",")], function () { });
        }


});

