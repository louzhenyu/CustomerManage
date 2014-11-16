
var ajax_url = "http://localhost:23130/ApplicationInterface/Gateway.ashx";
var Ajax = {
	
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

			//var baseInfo = this.getBaseAjaxParam();
			var baseInfo = {"openId":"","customerId":"","userId":"","locale":""};
			
			var _data = {
				'action':param.data.action,
				'ReqContent':JSON.stringify({
					'common':(param.data.common?$.extend(baseInfo,param.data.common):baseInfo),
					'special':(param.data.special?param.data.special:param.data)
				})
			};
			
			_param.data = _data;
			http://localhost:23130/ApplicationInterface/Gateway.ashx
			return _param;
		},
		ajax:function(param){

			var _param;

			_param = this.buildAjaxParams(param);
			
			$.ajax(_param);
		}
};

var Main = (function(){

	var g = this;

	this.init = function(){
		this.initData();
	}
	this.initData = function(){

		//get VipListNum
		Ajax.ajax({
			url:ajax_url,
			data:{
				'action':"VIP.VipListNum.GetVipListNum"
			},
			success:function(data){
				if(data.code == 200){
					var p = [];
					//var p = [['Sony',7], ['Samsumg',13.3], ['LG',14.7], ['Vizio',5.2], ['Insignia', 1.2]]
					
					var items = data.content.Items;
					for(var i in items){
						//p.push([items[i]["TypeName"],items[i]["VipNum"]]);
						p.push([items[i]["TypeName"]+"到店<br/><em>"+items[i]["Proportion"]*100+"%</em>",items[i]["Proportion"]]);
					}
					g.initChart(p);
					
					$(".storeStatArea .conductNum strong").html(data.content.VipNumAll);
					$(".storeStatArea .pastNum strong").html(data.content.VipNumNow);
				}
				
			}
		});
		//get viplist
		Ajax.ajax({
			url:ajax_url,
			data:{
				'action':"VIP.VipList.GetVipList"
			},
			success:function(data){
				if(data.code == 200){
							
					var tpl ='<li>'
				            	+'<div class="item">'
				                    +'<img class="pic" src="{{VipPhoto}}">'
				                    +'<div class="info">'
				                        +'<span class="name">{{VipName}}</span>'
				                        +'<span class="time">{{VipTime}}</span>'
				                        +'<span class="exp">{{VipArea}}</span>'
				                    +'</div>'
				                +'</div>'
				               +'</li>';
			        var str ="";
			        var keys = ["VipPhoto","VipName","VipTime","VipArea"];;

			        var curr;
		            for(var i in data.content.VipList){
		            	curr = data.content.VipList[i];
		            	tpl_r = tpl;
		            	for(var j=0,l=keys.length;j<l;j++){
		            		tpl_r = tpl_r.replace("{{"+ keys[j] +"}}",curr[keys[j]]);
		            	}
		            	str += tpl_r; 
		            }
		            $(".newsShopList ul").html(str);
				}
			}
		});

	}
	this.initChart = function(params){
        
    	var plot8 = $.jqplot('pie', [params], {
	        grid: {
	            drawBorder: false, 
	            drawGridlines: false,
	            background: '#fff',
	            shadow:false
	        },
	        axesDefaults: {
	            
	        },
	        seriesDefaults:{
	            renderer:$.jqplot.PieRenderer,
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

$(document).ready(function(){ 
    Main.init();
});