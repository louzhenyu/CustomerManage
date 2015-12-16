// 路径定义
require.config({
    baseUrl: '/Module/static/js/',
    shim: {
        'touchslider': {
            exports: 'touchslider'
        },
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        },
        'highcharts': {
            deps: ['jquery'],
            exports: 'highcharts'
        },
        'tools': {
            deps: ['jquery'],
            exports: 'tools'
        },
        'artDialog': {
            deps: ['jquery'],
            exports: 'artDialog'
        }

    },
    paths: {
        //page
        homePageTemp: 'AppConfig/tempModel',
        formPageTemp: 'FormConfig/tempModel',
        customerTemp: 'CustomerPageConfig/tempModel',

        // lib 
        jquery: 'lib/jquery-1.8.3.min',
        easyui: '/Module/static/js/lib/jquery.easyui.min',
        langzh_CN: '/Module/static/js/lib/easyui-lang-zh_CN',
        tools:'lib/tools-lib',
		mustache:'lib/mustache',
        // plugin
        touchslider: 'plugin/touchslider',
        template: 'plugin/template',
        artDialog: 'plugin/artDialog',
        kindeditor: 'plugin/kindeditor',
        pagination: 'plugin/jquery.jqpagination',
        highcharts: 'plugin/highcharts'
    }
});

define(['jquery'], function($) {
    var pageJs=$("#section").data("js"),
        pageJsPrefix = '';
    if(pageJs&&pageJs.length){
        var arr=pageJs.split(" ");
        for(var i=0;i<arr.length;i++){
            arr[i]=pageJsPrefix+arr[i];
        }
        require([arr.join(",")],function(){});
    }

});

