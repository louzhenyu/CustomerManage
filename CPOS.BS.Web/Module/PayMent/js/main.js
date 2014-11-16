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
        },
        'artDialog': {
            deps: ['jquery'],
            exports: 'artDialog'
        },
        'swfobject':{
            deps:['cookie'],
            exports:'swfobject'
        },
        'fullAvatarEditor': {
            deps:['swfobject'],
            exports: 'fullAvatarEditor'
        }
    },
    paths: {
        drag: '/Module/static/js/plugin/jquery.drag',
        artDialog: '/Module/static/js/plugin/artDialog',
        newJquery: '/Module/Withdraw/js/jquery',
        tools: '/Module/static/js/lib/tools-lib',
        template: '/Module/static/js/lib/bdTemplate',
        pagination: '/Module/static/js/plugin/jquery.jqpagination',
        json2: '/Module/static/js/plugin/json2',
        datatables: '/Module/static/js/plugin/jquery.dataTables.min',
        datepicker: '/Module/static/js/plugin/datepicker',
        md5: '/Module/static/js/lib/MD5',
        cookie:'/Module/static/js/plugin/jquery.cookie',
        kkpager: '/Module/static/js/plugin/kkpager',
        datetimePicker: '/Module/static/js/plugin/jquery.datetimepicker',
        swfobject: '/Framework/swfupload/scripts/swfobject',
        fullAvatarEditor: '/Framework/swfupload/scripts/fullAvatarEditor',
		ajaxform: '/Module/static/js/plugin/jquery.form'
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

