
var JitCfg = {
	'isDebug':null,
};

(function(global){
	
	function trim(str){
	
		return (str?str.replace(/(\s)|(\r\n)|(\r)|(\n)/gi, ""):null);
	}
	
	function strToJson(jsonStr){
	
		jsonStr = trim(jsonStr);
		
		return eval( '(' + jsonStr + ')' );
	}
	
	/*
	 #随机数函数
	 parameter：
	 	(number)section 随机区间
	 	(number)start 随机起步值
	 return:
	 	(int)
	 @memberOf gc.fn
	 * */
	function random(section,start){
		if(start != null){
			return Math.floor(Math.random()*section) + 1 + start;
		}else{
			return Math.floor(Math.random()*section) + 1;
		}
	}
	
	function bind(func, scope){
		return function(){
			return func.apply(scope, arguments);
		}
	}
	
	function loadScript(url,callback){
	
		var _script = createElement('script');
		
		var _Head = document.getElementsByTagName("HEAD").item(0); 
		
		_Head.appendChild(_script);
		
		_script.src = url;
		
		if(typeof callback == 'function'){
		
			_script.onload = callback;
		}
	}
	
	function log(str,type){
	
		if(type == 'error'){
		
			console.error(str);
			
		}else{
		
			console.log(str);
		}
	}
	
	var cookie = {
		SetCookie:function(name,value,expires,path){var expdate=new Date();expdate.setTime(expdate.getTime()+(expires*1000));document.cookie=name+"="+escape(value)+"; expires="+expdate.toGMTString()+( ( path ) ? ";path=" + path : "" )}, 
		GetCookie:function(name){var arg=name+"=";var alen=arg.length;var clen=document.cookie.length;var i=0;while(i<clen){var j=i+alen;if(document.cookie.substring(i,j)==arg)return this.GetCookieVal(j);i=document.cookie.indexOf(" ",i)+1;if(i==0)break};return""},
		GetCookieVal:function(offset){var endstr=document.cookie.indexOf(";",offset);if(endstr==-1){endstr=document.cookie.length};return unescape(document.cookie.substring(offset,endstr))},
	};
	
	var locStorage = {
	
		set:function(key,val){
			
			if(key){
			
				localStorage.setItem(key,val);
				
			}else{
				
				log('set localStorage Error:key is null','error');
			}
		},
		
		get:function(key){
			
			return localStorage.getItem(key);
		}
	};
	
	var store = function(){
		
		var args = arguments;
		
		if(args.length == 1){
			
			return locStorage.get(args[0]);
			
		}else if(args.length == 2){
			
			if(args[1] == null){
				
				localStorage.removeItem(args[0]);
				
			}else{
				
				locStorage.set(args[0],args[1]);
			}
		}
	}
	
	
	var sesStorage = {
	
		set:function(key,val){
			
			if(key){
			
				sessionStorage.setItem(key,val);
				
			}else{
				
				log('set sessionStorage Error:key is null','error');
			}
		},
		
		get:function(key){
			
			return sessionStorage.getItem(key);
		}
	};
	
	var session = function(){
		
		var args = arguments;
		
		if(args.length == 1){
			
			return sesStorage.get(args[0]);
			
		}else if(args.length == 2){
			
			if(args[1] == null){
				
				sessionStorage.removeItem(args[0]);
				
			}else{
				
				sesStorage.set(args[0],args[1]);
			}
		}
	};
	
	
	var validator = {
	
		IsEmail : function(str){
			
			var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
			
			return reg.test(str)
		},
		
		isPhoneNumber : function (str){

			var regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;
			
			return regAee.test(str);
		}
	}

	
	function version(url){
		
		var rst = $.ajax({
			url: (url?url:"../version.js"),
			//dataType: (url?null:"json"),
			async:false,
			cache:false
		});
		
		var vcfg = strToJson(rst.responseText);
		
		JitCfg.isDebug = vcfg.APP_DEBUG;
		
		delete vcfg.APP_DEBUG;
		
		if(JitCfg.isDebug){
			
			vcfg['APP_NEEDRELOAD'] = 'YES';
			
			Jit.AM.setAppConfig(vcfg);
			
			return ((new Date()).getTime());
			
		}else{
		
			var cfg = Jit.AM.getAppConfig();
			
			if(!cfg || (cfg['APP_VERSION'] != vcfg['APP_VERSION']) ){
				
				vcfg['APP_NEEDRELOAD'] = 'YES';
				
			}else{
				
				vcfg['APP_NEEDRELOAD'] = 'NO';
			}
			
			console.log('vcfg',vcfg);
			
			Jit.AM.setAppConfig(vcfg);
			
			return vcfg['APP_VERSION'];
		}
	}
	
	var fn = {
		random : random,
		trim : trim,
		strToJson : strToJson,
		loadScript : loadScript,
		log : log,
		store : store,
		valid : validator,
		version : version,
		cookie : cookie
	};
	
	
	/*
		@@页面管模块
		
		appManage = {
		
			//设置应用的配置信息
			setAppConfig(配置信息)，
			
			//获取应用的配置信息
			getAppConfig(),
			
			//设置应用交互时ajax 携带的基本信息
			setBaseAjaxParam(json object),
			
			//获取应用交互时ajax 携带的基本信息
			getBaseAjaxParam(),
			
			//设置页面之间通信的数据信息
			setPageParam(key , value [object | string] ),
			
			//获取页面之间通信的数据信息
			getPageParam(key)
		}
	*/
	
	var appManage = {
	
		'APP_CODE':'',
		
		//获得url上的参数
		getUrlParam:function(key) {
			
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
		},
		
		setAppConfig:function(cfg){
			
			var tkey, code = null, me = this;
			
			if(!me.APP_CODE){
			
				for(var key in cfg){
					
					if(key = 'APP_CODE'){
						
						me.APP_CODE = cfg[key];
						
						break;
					}
				}
			}
			
			if(!me.APP_CODE){
			
				return ;
			}
			
			var keys = {},
				exitkeys = store(me.APP_CODE+'_APP_CFG_KEY');
			
			if(exitkeys){
				
				exitkeys = exitkeys.split(',');
				
				if(exitkeys.length>0){
					
					for(var i=0 ;i<exitkeys.length;i++){
						
						keys[exitkeys[i]] = true;
					}
				}
			}
			
			for(var key in cfg){
				
				tkey = me.APP_CODE + '_' + key;
				
				keys[tkey] = true;
				
				store(tkey,cfg[key]);
			}
			
			var newkeys = [];
			
			for(var key in keys){
				
				newkeys.push(key);
			}
			
			if(newkeys.length>0){
				
				store(me.APP_CODE+'_APP_CFG_KEY' , newkeys.join(','));
			}
		},
		
		getAppConfig:function(){
			
			var appKeys = store(this.APP_CODE+'_APP_CFG_KEY')
			
			if(!appKeys){
			
				return null;
			}
			
			appKeys = appKeys.split(',');
			
			var cfg = {} , key;
			
			for(var i in appKeys){
				
				key = appKeys[i];
				
				cfg[key.replace(this.APP_CODE+'_','')] = store(key);
			}
			
			return cfg;
		},
		
		
		/*
			设置ajax交互时的基本数据 (需要和config.js 中的AJAX_PARAMS 相匹配)
			
			@param : {
				'openId':'xxx',
				'userId':'xxx',
				'locale':'xxx',
				'customerId':''
			}
		*/
		setBaseAjaxParam:function(param){
			
			var ajaxKeys = store(this.APP_CODE+'_AJAX_PARAMS').split(',');
			
			for(var i in ajaxKeys){
				
				store(this.APP_CODE+'_AJAX_PARAM_'+ajaxKeys[i] , param[ajaxKeys[i]]);
			}
		},
		
		getBaseAjaxParam:function(){
			
			var ajaxKeys = store(this.APP_CODE+'_AJAX_PARAMS').split(',') , param = {};
			
			for(var i in ajaxKeys){
				
				param[ajaxKeys[i]] = store(this.APP_CODE+'_AJAX_PARAM_'+ajaxKeys[i]);
			}
			
			return param;
		},
		
		setAppParam:function(type,key,val){
			
			var rkey = this.APP_CODE + '_' + type + '_' + key;
			
			if(val == null){
			
				store(rkey , null );
				
			}else if((typeof val) == 'object'){
				
				store(rkey , 'o_' + JSON.stringify(val) );
				
			}else{
				
				store(rkey , 's_' + val );
			}
		},
		
		getAppParam:function(type,key){
			
			var rkey = this.APP_CODE + '_' + type + '_' + key;
			
			var val = store(rkey);
			
			if(!val){
				
				return null;
			}
			
			var dtype = val.substr(0,1),
			
				rval = val.substring(2,val.length);
			
			if(dtype == 's'){
				
				return rval;
				
			}else if(dtype == 'o'){
				
				return eval('(' + rval + ')');
			}
		},
		
		setAppSession:function(type,key,val){
			
			var rkey = this.APP_CODE + '_' + type + '_' + key;
			
			if(val == null){
			
				session(rkey , null );
				
			}else if((typeof val) == 'object'){
				
				session(rkey , 'o_' + JSON.stringify(val) );
				
			}else{
				
				session(rkey , 's_' + val );
			}
		},
		
		getAppSession:function(type,key){
			
			var rkey = this.APP_CODE + '_' + type + '_' + key;
			
			var val = session(rkey);
			
			if(!val){
				
				return null;
			}
			
			var dtype = val.substr(0,1),
			
				rval = val.substring(2,val.length);
			
			if(dtype == 's'){
				
				return rval;
				
			}else if(dtype == 'o'){
				
				return eval('(' + rval + ')');
			}
		},
		
		setPageParam:function(key,val){
			
			this.setAppParam('PageParam',key,val);
		},
		
		getPageParam:function(key){
			
			return this.getAppParam('PageParam',key);
		},
		
		setAppPageConfig:function(cfg){
			
			this.setAppParam('PageCfg','',cfg);
		},
		
		getAppPageConfig:function(){
		
			return this.getAppParam('PageCfg','');
		},
		
		pageHistoryPush:function(pagename){
			
			var history = this.getAppSession('PageHistory','');
			
			if(history){
				
				var list = history.split(',');
				
				if(list.length>=12){
					
					list.splice(0,1);
				}
				
				list.push(pagename);
				
				this.setAppSession('PageHistory','',list.join(','));
				
			}else{
				
				this.setAppSession('PageHistory','',pagename);
			}
		},
		
		pageHistoryPop:function(){
			
			var history = this.getAppSession('PageHistory','');
			
			if(history){
				
				var list = history.split(',');
				
				list.pop();
				
				this.setAppSession('PageHistory','',list.join(','));
			}
		},
		pageHistoryClear:function(){
			
			this.setAppSession('PageHistory','',null);
		},
		hasHistory:function(){
			
			var history = this.getAppSession('PageHistory','');
			
			if(history){
				
				var list = history.split(',');
				
				if(list.length >= 2){
					
					return true;
				}
			}
			
			return false;
		},
		pageBack:function(){
			
			var history = this.getAppSession('PageHistory','');
			
			if(history){
				
				var list = history.split(',');
				
				if(list.length >= 2){
					
					log('返回上一页');
					
					list.pop();
					
					var tarpage = list.pop();
					
					var pages = tarpage.split(':');
					
					this.setAppSession('PageHistory','',list.join(','));
					
					if(pages.length >1){
					
						this.toPage(pages[0],pages[1]);
						
					}else{
						
						this.toPage(pages[0]);
					}
					
				}
			}
		},
		
		toPage:function(pagename,param){
			
			var pagecfg = this.getAppPageConfig(),
			
			page = pagecfg[pagename];
			
			if(page){
				
				if(param){
				
					this.pageHistoryPush(pagename+':'+param);
					
				}else{
					
					this.pageHistoryPush(pagename);
				}
				
				
				var cfg = Jit.AM.getAppConfig();
				
				if(cfg['APP_NEEDRELOAD'] == 'YES'){
					
					location.href = page.path+'?version='+((new Date()).getTime())+(param?param:'');
					
				}else{
					
					location.href = page.path+(param?('?'+param):'');
				}
			}
		},
		buildAjaxParams:function(param){
			
			var _param = {
				type: "post",
				dataType: "json",
				url: "",
				data: null,
				beforeSend: function () {
					//UI.Loading('SHOW');
				},
				success: null,
				error: function (XMLHttpRequest, textStatus, errorThrown){
					//UI.Loading("CLOSE");
				}
			};
			
			$.extend(_param,param);
			
			var baseInfo = this.getBaseAjaxParam();
			
			var _data = {
				'action':param.data.action,
				'ReqContent':JSON.stringify({
					'common':(param.data.common?$.extend(baseInfo,param.data.common):baseInfo),
					'special':(param.data.special?param.data.special:param.data)
				})
			};
			
			_param.data = _data;
			
			return _param;
		},
		ajax:function(param){
			/*
			var _param = {
				type: "post",
				dataType: "json",
				url: "",
				data: null,
				beforeSend: function () {
					//UI.Loading('SHOW');
				},
				success: null,
				error: function (XMLHttpRequest, textStatus, errorThrown){
					//UI.Loading("CLOSE");
				}
			};
			
			$.extend(_param,param);
			
			var baseInfo = this.getBaseAjaxParam();
			
			var _data = {
				'action':param.data.action,
				'ReqContent':JSON.stringify({
					'common':baseInfo,
					'special':param.data
				})
			};
			
			_param.data = _data;
			*/
			
			var _param = this.buildAjaxParams(param);
			
			$.ajax(_param);
		},
		weiXinOptionMenu:function(flag){
		
			document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
			
				WeixinJSBridge.call(flag?'showOptionMenu':'hideOptionMenu');
				
			});
		},
		weiXinToolBar:function(flag){
		
			document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
			
				WeixinJSBridge.call(flag?'showToolbar':'hideToolbar');
				
			});
		},
		defindPage:function(page){
			
			window.scrollTo(0, 0);
			
			page.getBaseInfo = bind(this.getBaseAjaxParam,this);
			
			page.setParams = bind(this.setPageParam,this);
			
			page.getParams = bind(this.getPageParam,this);
			
			page.getUrlParam = bind(this.getUrlParam,this);
			
			page.pageBack = bind(this.pageBack,this);
			
			page.toPage = bind(this.toPage,this);
			
			page.ajax = bind(this.ajax,this);
			
			page.buildAjaxParams = bind(this.buildAjaxParams,this);
			
			page.weiXinOptionMenu = bind(this.weiXinOptionMenu,this);
			
			page.weiXinToolBar = bind(this.weiXinToolBar,this);
			
			Jit.AM.onLoad = function(){
				
				page.onPageLoad();
			}
			
			window.JitPage = page;
		}
	}
	
	

	var UI = {
		Dialog:function(cfg){
			
			if(cfg == 'CLOSE'){
				
				var panel = $('.jit-ui-panel');
				
				if(panel){
				
					(panel.parent()).remove();
				}
				
			}else{
				
				cfg.LabelOk = cfg.LabelOk?cfg.LabelOk:'确定';
				
				cfg.LabelCancel = cfg.LabelOk?cfg.LabelCancel:'取消';
				
				var panel,btnstr;
				
				if(cfg.type == 'Alert' || cfg.type == 'Confirm'){
				
					btnstr = (cfg.type == 'Alert')?'<a id="jit_btn_ok" style="margin:0 auto">'+cfg.LabelOk+'</a>':'<a id="jit_btn_cancel">'+cfg.LabelCancel+'</a><a id="jit_btn_ok">'+cfg.LabelOk+'</a>';
				
					panel = $('<div"><div class="jit-ui-panel"></div><div name="jitdialog" style="margin-top:120px" class="popup br-5">'
						  + '<p class="ac f14 white">'+cfg.content+'</p><div class="popup_btn">'
						  + btnstr + '</div></div></div>');
					
				}else if(cfg.type == 'Dialog'){
					
					panel = $('<div><div class="jit-ui-panel"></div><div style="margin-top:120px" class="popup br-5"><p class="ac f14 white">'+cfg.content+'</p></div></div>');
				}
				
				panel.css({
					'position':'fixed',
					'left':'0',
					'right':'0',
					'top':'0',
					'bottom':'0',
					'z-index':'99'
				});
				/*
				var dialogdom =$('[name=jitdialog]');
				dialogdom.css({
					'left':(Jit.winSize.width-dialogdom.width())/2,
					'top':(Jit.winSize.height-dialogdom.height())/2,
				});
				*/
				panel.appendTo($('body'));
				
				(function(panel,cfg){
				
					setTimeout(function(){
						
						if(cfg.CallBackOk){
							
							$(panel.find('#jit_btn_ok')).bind('click',cfg.CallBackOk);
						}
						if(cfg.CallBackCancel){
						
							$(panel.find('#jit_btn_cancel')).bind('click',cfg.CallBackCancel);
							
						}else{
						
							$(panel.find('#jit_btn_cancel')).bind('click',function(){Jit.UI.Dialog('CLOSE');});
						}
					},16);
					
				})(panel,cfg);
				
			}
			
		}
	}



	
	
	global.Jit = (function(){
		var _jit = new Function();
		_jit.prototype = fn;
		_jit = new _jit();
		_jit.fn = fn;
		_jit.appManage = appManage;
		_jit.AM = appManage;
		_jit.UI = UI;
		return _jit;
	})();
	
	
})(window,undefined)
