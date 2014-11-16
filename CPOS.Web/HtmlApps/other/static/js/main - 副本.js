//当body标签添加cache属性时，查找version值作为版本号，如果没有version属性，则缓存版本一小时
var appConfig = {
    cache:document.body.hasAttribute("cache"),
    version:document.body.hasAttribute("cache")&&document.body.hasAttribute("version")?document.body.getAttribute("version"):Math.floor(new Date().getTime()/(1000*60*60))
}

// 路径定义
require.config({
    baseUrl: '/HtmlApps/other/static/js/',
    urlArgs: "v=" + (appConfig.cache?appConfig.version:(new Date()).getTime()),
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
        'jsonp':{
        	deps: ['jquery'],
            exports: 'jsonp'
        }

    },
    paths: {

        // lib 
        jquery: 'lib/jquery-1.8.3.min',
        tools:'lib/tools-lib',
        // plugin
        jsonp:'plugin/jquery.jsonp',
		mustache:'plugin/mustache',
        touchslider: 'plugin/touchslider',
        template: 'plugin/template',
        kindeditor: 'plugin/kindeditor',
        pagination: 'plugin/jquery.jqpagination',
        highcharts: 'plugin/highcharts'
    }
});

define(['jquery'], function($) {
    var pageJs=$("#section").data("js")?$("#section").data("js"):"",
        pageJsPrefix = '';
    if(pageJs.length){
        var arr=pageJs.split(" ");
        for(var i=0;i<arr.length;i++){
            arr[i]=pageJsPrefix+arr[i];
        }
        require([arr.join(",")],function(){});
    }

});

