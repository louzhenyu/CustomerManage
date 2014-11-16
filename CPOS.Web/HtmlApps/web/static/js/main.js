//当body标签添加cache属性时，查找version值作为版本号，如果没有version属性，则缓存版本十二小时
var appConfig = {
    cache:document.body.hasAttribute("cache"),
    version:document.body.hasAttribute("cache")&&document.body.hasAttribute("version")?document.body.getAttribute("version"):Math.floor(new Date().getTime()/(1000*60*60*12))
}
// 路径定义
require.config({
    baseUrl: '../../static/js',
    urlArgs:'bust='+ (appConfig.cache?appConfig.version:(new Date()).getTime()),
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
        'localStorage':{
            deps: ['tools'],
            exports: 'localStorage'
        }

    },
    paths: {
        // lib 
        jquery: 'lib/jquery.1.11',
        tools:'lib/tools-lib',
        // plugin
        touchslider: 'plugin/touchslider',
        highcharts: 'plugin/highcharts',
        drag: 'plugin/jquery.drag',
        template: 'lib/bdTemplate',
        json2: 'plugin/json2',
        datepicker: 'plugin/datepicker',
        md5: 'lib/MD5',
        kkpager: 'plugin/kkpager',
        localStorage:'plugin/localstorage'
    }
});

define(['jquery','tools','localStorage'], function($) {
    var pageJs=$("#section").data("js"),
        pageJsPrefix = '';
    if(pageJs.length){
        var arr=pageJs.split(" ");
        for(var i=0;i<arr.length;i++){
            arr[i]=pageJsPrefix+arr[i];
        }
        require([arr.join(",")],function(){});
    }

});

