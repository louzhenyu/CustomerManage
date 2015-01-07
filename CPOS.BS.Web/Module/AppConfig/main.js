// 路径定义
require.config({
    baseUrl: '',
    shim: {
        'touchslider': {
            exports: 'touchslider'
        },
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        }

    },
    paths: {
        //page
        temp: 'js/tempModel',
        productLayer: 'js/productLayer',
        // lib 
        jquery: 'lib/jquery-1.8.3.min',
        // plugin
        touchslider: 'plugin/touchslider',
        template: 'plugin/template',
        kindeditor: 'plugin/kindeditor',
        pagination: 'plugin/jquery.jqpagination'
    }
});

define(['jquery'], function ($) {
    var pageJs = $("#section").data("js"),
        pageJsPrefix = 'js/';
    if (pageJs.length) {
        var arr = pageJs.split(" ");
        for (var i = 0; i < arr.length; i++) {
            arr[i] = pageJsPrefix + arr[i];
        }
        require([arr.join(",")], function () {
        });
    }

});
