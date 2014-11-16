var page = {
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
	buildNewAjaxParams:function(param){

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
		
		var baseInfo = this.getBaseAjaxParam();
		
		var action = param.data.action,
			interfaceType = param.interfaceType||'Product',
			_req = {
				'Locale':baseInfo.locale,
				'CustomerID':baseInfo.customerId,
				'UserID':baseInfo.userId,
				'OpenID':baseInfo.openId,
				'Token':null,
				'Parameters':param.data
			};

		delete param.data.action;

		var _data = {
			'req':JSON.stringify(_req)
		};
		
		_param.data = _data;

		_param.url = _param.url+'?type='+interfaceType+'&action='+action;

		return _param;
	},
	ajax:function(param){

		var _param = this.buildNewAjaxParams(param);
		
		_param.url = JitCfg.ajaxUrl + _param.url;
		
		$.ajax(_param);
	},
	bandEvent:function(){
        $('#btn_and').bind('click', function () {
            $('#share-mask').show();
            $('#share-mask-img').show().attr('class', 'pullDownState');
        });
        $('#share-mask').bind('click', function () {
            var that = $(this);
            $('#share-mask-img').attr('class', 'pullUpState').show();
            setTimeout(function () { $('#share-mask-img').hide(500); that.hide(1000); }, 500);
        });
	},
	isWeixin:function(){  
		var ua = navigator.userAgent.toLowerCase();  
		if(ua.match(/MicroMessenger/i)=="micromessenger") {  
			return true;  
		} else {  
			return false;  
		}  
	},
	init:function(){
		
		var me = this;
		var callback = ("callback" + (new Date().getTime()));
		var data = {
			"UserID" : "",
			"Locale" : "",
			"BusinessZoneID" : "",
			"Token" : "",
			"CallBack" : callback,
			"Parameters" : {
				"SearchItem" : "",
				"PageIndex" : 0,
				"PageSize" : 3,
				"CustomerID":me.getUrlParam('customerId')
			}
		}

		$.ajax({
			url : 'http://121.199.42.125:5001/Gateway.ashx?action=GetAppList',
			dataType : "jsonp",
			type : "GET",
			jsonp : "CallBack",  //回调的名称
			data : {
				CallBack:callback,
				ReqContent : JSON.stringify(data)
			},
			jsonpCallback:callback,
			success : function(data) {
				
				if(data.ResultCode=="200"){
					
					var info = data.Data.AppList[0];
					
					$('#btn_ios').attr('href','itms-services://?action=download-manifest&url='+info.IOSUrl);
					
					$('#btn_and').attr('href',info.AndroidUrl);
					
					$('#img_ico').attr('src',info.LogoUrl);
					
					$('.tit').html(info.ClientName);

					$('#des').html(info.AppDescription);
					
					//$('#des').html(data.AppList[0].LogoUrl);
					
					if(me.isWeixin() && navigator.userAgent.match(/Android/i)){
						me.bandEvent();
					}
					
				}
			},
			error : function(XMLHttpRequest, textStatus, errorThrown) {
			}
		});
	}
}