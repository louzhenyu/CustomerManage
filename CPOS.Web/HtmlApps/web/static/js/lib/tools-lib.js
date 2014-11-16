; (function ($) {
    var util = {};
    util.stopBubble=function (e) {
        if (e && e.stopPropagation) {
            //除IE之外的阻止默认事件
            e.stopPropagation();
        } else {
            //IE的阻止默认事件
            window.event.cancelBubble = true;
        }
    };
    //obj转化成list
    util.obj2list = function(obj){
        var list = [];
        for(var i in obj){
            list.push(obj[i]);
        }
        return list;
    };
    //list转换成obj
    util.list2obj = function(list,key){
        var obj = {};
        for(var i=0;i<list.length;i++){
            var idata = list[i];
            obj[idata[key]] = idata;
        }
        return obj;
    };
    //获得地址栏参数值
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
    //构建地址栏参数
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
    //构建基本参数
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
			
			//var baseInfo = this.getBaseAjaxParam();
			
			var action = param.data.action,
				interfaceType = param.interfaceType||'Product',
				_req = {
					'Locale':null,
					'CustomerID':(param.customerId?param.customerId:null),
					'UserID':null,
					'OpenID':null,
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
    //调用ajax
	util.ajax=function(param){

			var _param;

			if(param.url.indexOf('Gateway.ashx')!=-1){

				_param = util.buildAjaxParams(param);
			}else{

				_param = util.buildAjaxParams(param);
			}
			//_param.url =  _param.url;
			
			$.ajax(_param);
		},
    $.util=util;
})(jQuery);