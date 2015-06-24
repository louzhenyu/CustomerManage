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
        artDialog: '/Module/static/js/plugin/artDialog',
        newJquery: '/Module/Withdraw/js/jquery',
        easyui: '/Module/static/js/lib/jquery.easyui.min',
        tools: '/Module/static/js/lib/tools-lib',
        template: '/Module/static/js/lib/bdTemplate',
        pagination: '/Module/static/js/plugin/jquery.jqpagination',
        json2: '/Module/static/js/plugin/json2',
        datatables: '/Module/static/js/plugin/jquery.dataTables.min',
        datepicker: '/Module/static/js/plugin/datepicker',
        md5: '/Module/static/js/lib/MD5',
        cookie: '/Module/static/js/plugin/jquery.cookie',
        kkpager: '/Module/static/js/plugin/kkpager',
        datetimePicker: '/Module/static/js/plugin/jquery.datetimepicker',
        swfobject: '/Framework/swfupload/scripts/swfobject',
        fullAvatarEditor: '/Framework/swfupload/scripts/fullAvatarEditor',
        swffileupload: '/Framework/swfuploadfile/jquery.uploadify.min',
        zTree: '/Module/static/js/plugin/jquery.ztree.3.5',
        tips: '/Module/static/js/plugin/jquery.poshytip'
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

