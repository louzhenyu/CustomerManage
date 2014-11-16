require.config({
	
	baseUrl: "../",
	
	paths: {
　　	'jquery':'lib/jquery-1.8.3.min',
		'zepto':'lib/zepto.min',
		'jit':'lib/jit-lib'
　　}
});

require(['zepto','../lib/jit-lib.js?version='+((new Date()).getTime())], function (){
	
	$ = Zepto;
	
	var version = Jit.version();
	
	var PageConfigs = Jit.AM.getAppPageConfig();
	
	if(!Jit.AM.getAppPageConfig()){
	
		var rst = $.ajax({
			url: "../config.js",
			async:false,
			cache:false
		});
		
		var vcfg = Jit.strToJson(rst.responseText);
		
		Jit.AM.setAppPageConfig(vcfg);
		
		PageConfigs = Jit.AM.getAppPageConfig();
	}
	
	
	var htmlname = $('title').attr('name');
	
	if(!htmlname){
	
		alert('Html 中页面名称未定义');
		
		return ;
	}
	
	var pageconfig = PageConfigs[htmlname];
	
	if(!pageconfig){
	
		alert('Config.js 中未找到对应的HTML');
		
		return ;
	}
	
	$('title').html(pageconfig.title);
	
	
	function loadFiles(list,type,callback){
		
		if(!$.isArray(list))
			return;
		
		var js_arr = [],
			type = ( type == 'plugin' ? '../plugin/':'../js/' );
		
		$.each(list,function(key,val){
			
			js_arr.push(type+val+'.js?version='+version);
			
		});
		
		require(js_arr,callback);
	}
	
	
	
　　if(pageconfig){
		
		var plugins = pageconfig.plugin;
		
		var plugins = pageconfig.plugin;
		
		if($.isArray(plugins) && plugins.length>0){
			
			loadFiles(plugins,'plugin',function(){
			
				var scripts = pageconfig.script;
				
				loadFiles(scripts,'script',main);
			});
			
		}else{
			
			var scripts = pageconfig.script;
				
			loadFiles(scripts,'script',main);
		}
	}
	
	function main(){
	
		Jit.AM.weiXinToolBar(false);

	    //Jit.AM.weiXinOptionMenu(false);
		
		if(Jit.AM.getUrlParam('openId') || Jit.AM.getUrlParam('customerId') || Jit.AM.getUrlParam('userId')){
		
			var cfg = Jit.AM.getAppConfig(),
				keys = cfg.AJAX_PARAMS.split(',');
			
			var param = {};
			
			for(var key in keys){
				
				hash = Jit.AM.getUrlParam(keys[key]);
				
				if(hash){
					
					param[keys[key]] = hash;
				}
				
			}
			
			Jit.AM.setBaseAjaxParam(param);
			
			/*
			Jit.AM.setBaseAjaxParam({
				'openId':Jit.AM.getUrlParam('openId'),
				'userId':Jit.AM.getUrlParam('userId'),
				'customerId':Jit.AM.getUrlParam('customerId'),
				'locale':'ch'
			});
			*/
		}else{
			
			/*测试使用
			
			Jit.AM.setBaseAjaxParam({
				'openId':'ofUHqjgSme_qaNQN0oohrM7kX_Ck',
				'userId':'add73ef71c2c480c89b5a6941cb0dfc9',
				'customerId':'679e8361b250487d8fdd07791dced25d',
				'locale':'ch'
			});
			*/
		}
		
		Jit.AM.onLoad();
	}
});

(function(global){
	
})(window)

