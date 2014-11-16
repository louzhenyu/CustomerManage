//当body标签添加cache属性时，查找version值作为版本号，如果没有version属性，则缓存版本十二小时
var appConfig = {
    cache:document.body.hasAttribute("cache"),
    version:document.body.hasAttribute("cache")&&document.body.hasAttribute("version")?document.body.getAttribute("version"):Math.floor(new Date().getTime()/(1000*60*60*12))
}

// 路径定义
require.config({
    baseUrl: '/External/static/js/',
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
        'mobileScroll': {
            deps: ['jquery'],
            exports: 'mobileScroll'
        },
        'highchartsMore':{
        	deps: ['highcharts'],
            exports: 'highchartsMore'
        },
        'highchartsExporting':{
        	deps: ['highcharts','highchartsMore'],
            exports: 'highchartsExporting'
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
        iscroll:'plugin/iscroll',
        // plugin
        jsonp:'plugin/jquery.jsonp',
		mustache:'plugin/mustache',
        touchslider: 'plugin/touchslider',
        template: 'plugin/template',
        bdTemplate:'plugin/bdTemplate',
        kindeditor: 'plugin/kindeditor',
        pagination: 'plugin/jquery.jqpagination',
        highcharts: 'plugin/highcharts',
        highchartsMore:'plugin/highcharts-more',
        highchartsExporting:'plugin/highcharts-exporting',
        mobileScroll:'plugin/mobiscroll.core-2.5.2'
    }
});



var section = document.getElementById("section"),pageJs,pageJsPrefix="";
pageJs = section.hasAttribute("data-js")?section.getAttribute("data-js"):"";

if(pageJs.length){
    var arr=pageJs.split(" ");
    for(var i=0;i<arr.length;i++){
        arr[i]=arr[i].indexOf(".js")==-1?(pageJsPrefix+arr[i]+".js"):pageJsPrefix+arr[i];
    }
    require([arr.join(",")],function(){});
}


