; (function ($) {
    var util = {};
    util.stopBubble=function (e) {
        if (e && e.stopPropagation) {
            //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        } else {
            //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;
        }
    };

    util.obj2list = function(obj){
        var list = [];
        for(var i in obj){
            list.push(obj[i]);
        }
        return list;
    };

    util.list2obj = function(list,key){
        var obj = {};
        for(var i=0;i<list.length;i++){
            var idata = list[i];
            obj[idata[key]] = idata;
        }
        return obj;
    };
    util.getUrlParam = function(key){
		var urlstr = window.location.href.split("?"),
            params = {};
        if (urlstr[1]) {
			
				var items = urlstr[1].split("&");
				
				for (i = 0; i < items.length; i++) {
				
					itemarr = items[i].split("=");
					
					params[itemarr[0]] = itemarr[1];
				}
			}
        return key?params[key]:params;
        
    }
    util.toUrlWithParam=function(toUrl,param){
		
		var value = "",itemarr = [],params;


		params = this.getUrlParam();
		
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
	}
    //构建ajax
    util.buildAjaxParams=function(param){
            
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
			
            if(_param.dataType=="jsonp"){
                var jsonCallback = "jsonCallback"+Math.floor(Math.random()*1000000000);
                window[jsonCallback] = function(data){
                    _param.success(data);
                    delete window[jsonCallback];
                }
            }
			var action = param.action,
				interfaceType = param.interfaceType||'Product',
				_req = {
					'Locale':null,
					'CustomerID':null,
					'UserID':null,
					'OpenID':null,
					'Token':null,
                    'JSONP':_param.dataType=="jsonp"?jsonCallback:null,
					'Parameters':param.data
				};

			delete param.action;

			var _data = {
				'req':JSON.stringify(_req)
			};
			
			_param.data = _data;
			_param.url = _param.url+'?type='+interfaceType+'&action='+action;
            //debugger;
			return _param;
	};
    //最新的ajax封装
	util.ajax=function(param){

			var _param;

			if(param.url.indexOf('Gateway.ashx')!=-1){

				_param = util.buildAjaxParams(param);
			}else{

				_param = util.buildAjaxParams(param);
			}
			
			$.ajax(_param);
		},
    $.util=util;
})(jQuery);