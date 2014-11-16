document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
	
	WeixinJSBridge.call('hideOptionMenu');
	
	WeixinJSBridge.call('hideToolbar');
	
});

require.config({
	
	baseUrl: "../../../",
	
	paths: {
　　	'jquery':'lib/jquery-1.8.3.min',
		'zepto':'lib/zepto.min',
		'jit':'lib/jit-lib'
　　}
});

(function(){
	
	function getUrlParam(key) {
			
		var value = "",itemarr = [],
			urlstr = window.location.href.split("?");
		
		if (urlstr[1]) {
		
			var item = urlstr[1].split("&");
			
			for (i = 0; i < item.length; i++) {
			
				itemarr = item[i].split("=");
				
				if (key == itemarr[0]) {
				
					value = itemarr[1];
				}
			}
		}
		
		return value;
	}
	
	function ajax(url){
	
		url = encodeURI(url);
		
		var xhr=new XMLHttpRequest();
		
		if(xhr!=null){
			
			xhr.open('GET',url+'?version='+(new Date()).getTime(),false);
			
			xhr.send(null);
			
			if(xhr.status==200 || xhr.status==0){
			
				return eval( '(' + xhr.responseText + ')' );
			}
		}
	}
	
	var isGloble = getUrlParam('isGloble'),
		CID = (isGloble?'_globle':getUrlParam('customerId'));
	
	var cfg = ajax('/HtmlApps/version/'+CID+'.js');
	
	
	var jslib = (cfg.APP_JSLIB?cfg.APP_JSLIB:'zepto'),
		version = (cfg.APP_CACHE?cfg.APP_VERSION:((new Date()).getTime()));
	
	require([jslib,'../../../lib/jit-lib.js?version'+version], function (){
		
		if(typeof Zepto != 'undefined'){
			
			$ = Zepto;
		}
		
		if(typeof jQuery != 'undefined'){
			
			$ = jQuery;
		}
		
		var oldcfg = Jit.AM.getAppVersion(),_needReLoad = false;
		
		if((oldcfg && oldcfg.APP_VERSION != cfg.APP_VERSION) || cfg.APP_CACHE === false){
			
			_needReLoad = true;
		}
		
		Jit.AM.setAppVersion(cfg);
		
		if(isGloble){
			
			Jit.AM.setAppParam('isGloble','true');
			
		}else{
			
			Jit.AM.setAppParam('isGloble','false');
		}
		
		Jit.AM.checkAppPageConfig(_needReLoad);
		
		require(['/HtmlApps/main.js?version'+version]);
	});
	
})()
