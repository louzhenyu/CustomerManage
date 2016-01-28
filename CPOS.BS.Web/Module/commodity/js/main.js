//当body标签添加cache属性时，查找version值作为版本号，如果没有version属性，则缓存版本十二小时
var appConfig = {
    cache: document.body.hasAttribute("cache"),
    version: document.body.hasAttribute("cache") && document.body.hasAttribute("version") ? document.body.getAttribute("version") : Math.floor(new Date().getTime() / (1000 * 60 * 60 * 12))
}

// 路径定义
require.config({
    urlArgs: "v=" + (appConfig.cache ? appConfig.version : (new Date()).getTime()),
    baseUrl:'',   //window.staticUrl ? window.staticUrl:'http://static.uat.chainclouds.com'
    shim: {
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        },
        'tools': {
            deps: ['jquery'],
            exports: 'tools'
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
        }
    },
    paths: {
        drag: '/Module/static/js/plugin/jquery.drag',
        tools: '/Module/static/js/lib/tools-lib',
        template: '/Module/static/js/lib/bdTemplate',
        artTemplate: '/Module/static/js/plugin/template',
        customerTemp: '/Module/homeIndex/js/tempModel',//微官网配置模板
        easyui: '/Module/static/js/lib/jquery.easyui.min',
        langzh_CN: '/Module/static/js/lib/easyui-lang-zh_CN',
        validator: '/Module/static/js/lib/validator',
        newJquery: '/Module/Withdraw/js/jquery',
        pagination: '/Module/static/js/plugin/jquery.jqpagination',
        json2: '/Module/static/js/plugin/json2',
        kindeditor1: '/Module/static/js/plugin/kindeditor',
        kindeditor: '/Framework/javascript/Other/kindeditor/kindeditor',
        artDialog: '/Module/static/js/plugin/artDialog',
        ajaxform: '/Module/static/js/plugin/jquery.form',
        datetimePicker: '/Module/static/js/plugin/jquery.datetimepicker',
        kkpager: '/Module/static/js/plugin/kkpager',
		lang: '/Framework/Javascript/Other/kindeditor/lang/zh_CN'

    }
});

define(['newJquery'], function () {
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

