// var ajax_url = "http://localhost:23130/ApplicationInterface/Gateway.ashx";
var AjaxHandle = {
	buildAjaxParams: function(param) {
		var _param = {
			type: "post",
			dataType: "json",
			url: "",
			data: null,
			beforeSend: function() {},
			success: null,
			error: function(XMLHttpRequest, textStatus, errorThrown) {}
		};
		$.extend(_param, param);
		var baseInfo = param.BaseInfo;
		var action = param.data.action,
			interfaceType = param.interfaceType || 'Product',
			_req = {
				'Locale': baseInfo.locale,
				'CustomerID': baseInfo.customerId,
				'UserID': baseInfo.userId,
				'OpenID': baseInfo.openId,
				'Token': null,
				'Parameters': param.data
			};
		delete param.data.action;
		var _data = {
			'req': JSON.stringify(_req)
		};
		_param.data = _data;
		_param.url = _param.url + '?type=' + interfaceType + '&action=' + action;
		return _param;
	},
	ajax: function(param) {
		var _param;
		_param = this.buildAjaxParams(param);
		$.ajax(_param);
	}
};
var Main = (function() {
	var g = this;
	this.init = function() {
		this.lodaData();
	}

	function getUrlParam(key) {
		var value = "",
			itemarr = [],
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
	};
	this.lodaData = function() {
		var baseInfo = {
			"openId": "",
			"CustomerID": "",
			"UserID": "",
			"locale": ""
		}
		urlUnitID = getUrlParam('UnitID');
		baseInfo.CustomerID = getUrlParam('customerId'), baseInfo.UserID = getUrlParam('userId');
		if (!baseInfo.CustomerID) {
			return false;
		};
		//获取统计信息。
		AjaxHandle.ajax({
			interfaceMode: 'V2.0',
			BaseInfo:baseInfo,
			url: '/ApplicationInterface/Gateway.ashx',
			data: {
				'action': 'VIP.VipListNum.GetVipListNum',
				'UnitID': urlUnitID
			},
			success: function(result) {
		
				if (result && result.ResultCode == 0 && result.Data.Items) {
					var p = [];
					for (var i in result.Data.Items) {
						p.push([result.Data.Items[i]["TypeName"]+"到店<br/> <em>"+result.Data.Items[i]["Proportion"] + "%</em>", result.Data.Items[i]["Proportion"]]);
					}
					g.initChart(p);
					$(".storeStatArea .conductNum strong").html(result.Data.VipNumAll);
					$(".storeStatArea .pastNum strong").html(result.Data.VipNumNow);
				}
			},
		});
		//获取会员信息。
		AjaxHandle.ajax({
			interfaceMode: 'V2.0',
			BaseInfo:baseInfo,
			url: '/ApplicationInterface/Gateway.ashx',
			data: {
				'action': 'VIP.VipList.GetVipList',
				'UnitID': urlUnitID
			},
			success: function(result) {
				if (result && result.ResultCode == 0 && result.Data.VipList) {
					var tpl = '<li>' + '<div class="item">' + '<img class="pic" src="{{VipPhoto}}">' + '<div class="info">' + '<span class="name">{{VipName}}</span>' + '<span class="time">{{VipTime}}</span>' + '<span class="exp">{{VipArea}}</span>' + '</div>' + '</div>' + '</li>',
					 str = "",
					 keys = ["VipPhoto", "VipName", "VipTime", "VipArea"],
					 curr;
					for (var i in result.Data.VipList) {
						curr = result.Data.VipList[i];
						tpl_r = tpl;
						for (var j = 0, l = keys.length; j < l; j++) {
							tpl_r = tpl_r.replace("{{" + keys[j] + "}}", curr[keys[j]]);
						}
						str += tpl_r;
					}
					$(".newsShopList ul").html(str);
				}
			},
		});
	};
	this.initChart = function(params) {
		var plot8 = $.jqplot('pie', [params], {
			grid: {
				drawBorder: false,
				drawGridlines: false,
				background: '#fff',
				shadow: false
			},
			axesDefaults: {},
			seriesDefaults: {
				renderer: $.jqplot.PieRenderer,
				rendererOptions: {
					showDataLabels: true
				}
			},
			legend: {
				show: true,
				rendererOptions: {
					numberRows: 1
				},
				location: 's'
			}
		});
	}
	return this;
})();
$(document).ready(function() {
	Main.init();
});