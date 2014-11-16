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
        'touchslider': {
            deps: ['jquery'],
            exports: 'touchslider'
        },
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        },
        'tools': {
            deps: ['jquery'],
            exports: 'tools'
        }
    },
    paths: {
        //page
        homePageTemp: 'js/tempModel',

        // lib
        jquery: '/Module/static/js/lib/jquery-1.8.3.min',
        tools:'/Module/static/js/lib/tools-lib',
        // plugin
        touchslider: '/Module/static/js/plugin/touchslider',
        template: '/Module/static/js/plugin/template',
        kindeditor: '/Module/static/js/plugin/kindeditor',
        pagination: '/Module/static/js/plugin/jquery.jqpagination'
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


