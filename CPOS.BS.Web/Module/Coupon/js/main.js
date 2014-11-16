// 路径定义
require.config({
    urlArgs: "bust=" + (new Date()).getTime(),
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
        }
    },
    paths: {
        drag: '/Module/static/js/plugin/jquery.drag',
        jquery: '/module/static/js/lib/jquery-1.8.3.min',
        tools: '/Module/static/js/lib/tools-lib',
        template: '/Module/static/js/lib/bdTemplate',
        pagination: '/Module/static/js/plugin/jquery.jqpagination',
        json2: '/Module/static/js/plugin/json2',
        datatables: '/Module/static/js/plugin/jquery.dataTables.min',
        datepicker: '/Module/static/js/plugin/datepicker',
        md5: '/Module/static/js/lib/MD5',
        kindeditor: '/Framework/Javascript/Other/kindeditor/kindeditor',
        kkpager: '/Module/static/js/plugin/kkpager'
    }
});

define(['jquery'], function () {
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

