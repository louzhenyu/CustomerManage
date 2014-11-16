window.JitCfg={
	ajaxUrl:"http://o2oapi.aladingyidong.com"
}
;(function ($) {
    function stopBubble(e) {
        if (e && e.stopPropagation) {
            e.stopPropagation();
        } else {
            window.event.cancelBubble = true;
        }
    };

    function obj2list(obj){
        var list = [];
        for(var i in obj){
            list.push(obj[i]);
        }
        return list;
    }

    function list2obj(list,key){
        var obj = {};
        for(var i=0;i<list.length;i++){
            var idata = list[i];
            obj[idata[key]] = idata;
        }
        return obj;
    }
    function getUrlParam(key){
		var urlstr = window.location.href.split("?"),
            params = {};
        if (urlstr[1]) {
			
				var items = urlstr[1].split("&");
				
				for (i = 0; i < items.length; i++) {
				
					itemarr = items[i].split("=");
					
					params[itemarr[0].toLocaleLowerCase()] = itemarr[1];
				}
			}
        return key?params[key.toLocaleLowerCase()]:params;
        
    }
    function toUrlWithParam(toUrl,param){
		
		var value = "",itemarr = [],params;


		params = getUrlParam();
		
		if(param){
			
			var temps = param.split("&"),tempparam;
		
			for(var i=0;i<temps.length;i++){
				
				tempparam = temps[i].split('=');
				
				params[tempparam[0]] = tempparam[1];
			}
		}
		
		
		var paramslist = [];
		
		for(var key in params){
			
			paramslist.push(key + '=' + params[key]);
		}
		location.href= toUrl + "?" + paramslist.join("&");
	};
    //����ajax
    function buildNewAjaxParams(param){
			var _param = {
				type: "post",
				dataType: "json",
				url: "",
				data: null,
				beforeSend: function () {
					
				},
				success: null,
				error: function (XMLHttpRequest, textStatus, errorThrown){
					
				}
			};
			
			$.extend(_param,param);
			
			var action = param.data.action,
				interfaceType = param.interfaceType||'Product',
				_req = {
					'Locale':null,
					'CustomerID':(param.customerId?param.customerId:$.util.getUrlParam("customerId")),
					'UserID':param.userId?param.userId:null,
					'OpenID':param.openId?param.openId:null,
					'Token':null,
					'Parameters':param.data,
                    'random':Math.random()
				};

			delete param.data.action;

			var _data = {
				'req':JSON.stringify(_req)
			};
			
			_param.data = _data;

			_param.url = _param.url+'?type='+interfaceType+'&action='+action;

			return _param;
	};
	function buildAjaxParams(param){
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
			var baseInfo ={};
			//通过浏览器地址栏把内容填充
			if((!baseInfo.customerId)&&$.util.getUrlParam("customerId")){
				baseInfo.customerId=$.util.getUrlParam("customerId");
			}
			if((!baseInfo.userId)&&$.util.getUrlParam("userId")){
				baseInfo.userId=$.util.getUrlParam("userId");
			}
			if((!baseInfo.openId)&&$.util.getUrlParam("openId")){
				baseInfo.openId=$.util.getUrlParam("openId");
			}
			var _data = {
				'action':param.data.action,
				'ReqContent':JSON.stringify({
					'common':(param.data.common?$.extend(baseInfo,param.data.common):baseInfo),
					'special':(param.data.special?param.data.special:param.data)
				})
			};
			
			_param.data = _data;
			
			return _param;
	};
    //���µ�ajax��װ
	function ajax(param){
		var action = param.data.action;
		var _param;window.global={};
		if(param.url.indexOf('Gateway.ashx')!=-1 || param['interfaceMode'] == 'V2.0'){
			_param = buildNewAjaxParams(param);
		}else{
			_param = buildAjaxParams(param);
		}
		_param.url = JitCfg.ajaxUrl + _param.url;
		_param.beforeSend = function(){
			if(param.beforeSend){
				param.beforeSend();
			}
			global.timer = new Date().getTime();
		};
		_param.complete = function(){
			if(param.complete){
				param.complete();
			}
			console.log(
				"请求地址："+_param.url
				+"\r\n"+
				"请求方法："+action
				+"\r\n"+
				"请求耗时："+(new Date().getTime()- global.timer)+"毫秒"+"\r\n"
			);
		};
		$.ajax(_param);
	};

	//滚动条在Y轴上的滚动距离
	function getScrollTop(){
	　　var scrollTop = 0, bodyScrollTop = 0, documentScrollTop = 0;
	　　if(document.body){
	　　　　bodyScrollTop = document.body.scrollTop;
	　　}
	　　if(document.documentElement){
	　　　　documentScrollTop = document.documentElement.scrollTop;
	　　}
	　　scrollTop = (bodyScrollTop - documentScrollTop > 0) ? bodyScrollTop : documentScrollTop;
	　　return scrollTop;
	}

	//文档的总高度
	function getScrollHeight(){
	　　var scrollHeight = 0, bodyScrollHeight = 0, documentScrollHeight = 0;
	　　if(document.body){
	　　　　bodyScrollHeight = document.body.scrollHeight;
	　　}
	　　if(document.documentElement){
	　　　　documentScrollHeight = document.documentElement.scrollHeight;
	　　}
	　　scrollHeight = (bodyScrollHeight - documentScrollHeight > 0) ? bodyScrollHeight : documentScrollHeight;
	　　return scrollHeight;
	}

	//浏览器视口的高度
	function getWindowHeight(){
	　　var windowHeight = 0;
	　　if(document.compatMode == "CSS1Compat"){
	　　　　windowHeight = document.documentElement.clientHeight;
	　　}else{
	　　　　windowHeight = document.body.clientHeight;
	　　}
	　　return windowHeight;
	}

	//平台、设备和操作系统     
	function getPlatForm(){
		if(/AppleWebKit.*Mobile/i.test(navigator.userAgent) || (/Android|webOS|iPhone|iPod|iPad|BlackBerry|MIDP|SymbianOS|NOKIA|SAMSUNG|LG|NEC|TCL|Alcatel|BIRD|DBTEL|Dopod|PHILIPS|HAIER|LENOVO|MOT-|Nokia|SonyEricsson|SIE-|Amoi|ZTE/.test(navigator.userAgent))){
			return "phone"; 
		}else{
			return "pc";
		}
		/*
	    var system ={    
	        win : false,    
	        mac : false,    
	        xll : false    
	    };    
	    //检测平台     
	    var p = navigator.platform;
	    system.win = p.indexOf("Win") == 0;
	    system.mac = p.indexOf("Mac") == 0;
	    system.x11 = (p == "X11") || (p.indexOf("Linux") == 0);
	    if(system.win||system.mac||system.x11){
	    	return "pc";
	    }else{
	    	return "phone";
	    }
	    */
	}
	
	function storage(){
		
		var args = arguments;
		
		if(args.length == 1){
			
			return localStorage.getItem(args[0]);
			
		}else if(args.length == 2){
			
			localStorage.setItem(args[0],args[1]);
		}
	}
	var UI = {
        AjaxTips:{
        	//显示ajax加载数据的时候   出现加载图标
        	Loading:function(flag){

        		if(flag||arguments.length==0){
        			//显示loading

        			UI.Loading(true);

        		}else{
        			//隐藏loading
        			
        			UI.Loading(false);
        		}
        	},
        	//加载数据
        	Tips:function(options){
        		var left="50%",
        			top="50%";
        		if(options.left){
        			left=options.left;
        		}
        		if(options.top){
        			top=options.top;
        		}
        		if(options.show){//显示tips
					if($("#ajax__tips").length>0){
        				$("#ajax__tips").remove();
        			}
        			var style="position:fixed;top:"+top+";  left:"+left+";  width:100px;  height:100px;margin-top:-50px;margin-left:-50px;text-align: center;line-height100px;";
        			var $div=$("<div id='ajax__tips' style='"+style+"'>"+(options.tips?options.tips:"暂无数据")+"</div>");
        			$("body").append($div);
	        		
        		}else{  //隐藏tips
					$("#ajax__tips").hide();
        		}
        	}
        }

	}
	var util = {};
    util.stopBubble = stopBubble;
    util.obj2list = obj2list;
    util.list2obj = list2obj;
    util.getPlatForm = getPlatForm;

    util.getScrollTop = getScrollTop;
    util.getScrollHeight = getScrollHeight;
    util.getWindowHeight =getWindowHeight;


    util.getUrlParam = getUrlParam;
    util.toUrlWithParam = toUrlWithParam;
    util.buildAjaxParams = buildAjaxParams;
    util.ajax = ajax;
    util.UI=UI;
    //localStorage
    util.storage=storage;
    $.util=util;

////////////////////////////////////////////////    native adatper    /////////////////////////////////////////
	

	function isNative(){
		//console.log(getPlatForm()=="pc");
		return getPlatForm()=="phone";
	}

	function showDownpull(){
		if(isNative()){
			//alert("native://loading/downpullstart");
			location.href = "native://loading/downpullstart";
		}
	}
	function hideDownpull(){
		if(isNative()){
			//alert("native://loading/downpullend");
			location.href = "native://loading/downpullend";
		}
	}

	function showUppull(){
		if(isNative()){
			//alert("native://loading/uppullstart");
			location.href = "native://loading/uppullstart";
		}
	}
	function hideUppull(){
		if(isNative()){
			//alert("native://loading/uppullend");
			location.href = "native://loading/uppullend";
		}
	}

	function showLoading(){
		if(isNative()){
				location.href = "native://loading/popstart";	
			
		}
	}
	function hideLoading(){
		if(isNative()){
				location.href = "native://loading/popend";
		}
	}

	function imageView(imagelist,curr){
		var obj={};
		obj.curr = curr||0;
		obj.list=imagelist;
		if(isNative()){
			location.href = "native://image/"+JSON.stringify(obj);
		}
	}
	function mapView(locationList,curr){
		var obj={};
		obj.curr = curr||0;
		obj.list=locationList;
		if(isNative()){
			location.href = "native://map/"+JSON.stringify(obj);
		}
	}
	function getLocation(callback){
		var callbackName = "callback"+Math.floor(Math.random()*100000000);
		window[callbackName] = function(data){
			if(callback){
				callback(data);
			}
			delete window[callbackName];
		}
		if(isNative()){
			location.href = "native://getlocation/"+callbackName;
		}
	}
	function favorite(type,id,flag,callback){
		var callbackName = "callback"+Math.floor(Math.random()*100000000);
		window[callbackName] = function(data){
			if(callback){
				callback(data);
			}
			delete window[callbackName];
		}
		if(isNative()){
			location.href = "native://favorite/"+type+"/"+id+"/"+flag+"/"+callbackName;
		}
	}

	function share(type ,id ){
		if(isNative()){
			location.href = "native://share/"+type+"/"+id;
		}
	}

	function noData(type ,id ){
		if(isNative()){
			location.href = "native://noData";
		}
	}

	var loading ={
		show:showLoading,
		hide:hideLoading
	};

	var downpull= {
		show:showDownpull,
		hide:hideDownpull
	};

	var uppull ={
		show:showUppull,
		hide:hideUppull
	};


	var native={};
	native.isNative=isNative;
	native.loading = loading;
	native.downpull = downpull;
	native.uppull = uppull;

	native.imageView = imageView;
	native.mapView = mapView;
	native.favorite = favorite;
	native.share = share;
	native.getLocation=getLocation;

	native.noData = noData;
	$.native = native;
////////////////////////////////////////////////    native adatper    /////////////////////////////////////////

    
})(jQuery);