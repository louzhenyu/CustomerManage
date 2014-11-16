
var url = '../../../OnlineShopping/data/Data.aspx';

var Timeout = null;


(function($) {
    var pre_ajax = $.ajax;
	var jsjfalse = false; 
    $.ajax = function(params) {
        params || (params = {});
        if (params['dataType'] != "html" && typeof params['data'] != 'undefined') {
            if (params['data'] != null) {
                var data = params['data'];
             //   data.plat = "pc";
                params['data'] = data;
            }
		
        }
        pre_ajax(params);
    }
})(jQuery);


function IsPC() {
    var userAgentInfo = navigator.userAgent;

    var Agents = new Array("Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod");
    var flag = true;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
        }
    }
    return flag;
}




function getParam(para) {
    //获得html上的参数
    querystr = window.location.href.split("?");
	var iparam = "",
    tmp_arr = [];
    if (querystr[1]) {
        var GET1s = querystr[1].split("&");
		for (i = 0; i < GET1s.length; i++) {
            tmp_arr = GET1s[i].split("=");
			if (para == tmp_arr[0]) {
                iparam = tmp_arr[1];
            }
        }

    }
    return iparam;
}






 var Base = {
    //公共类的获得
    openId: function() {
	
	
		 return getParam("openId");

    },
    userId: function() {

		 return getParam("userId");

    },
    customerId: function() {
        var customerId = getParam("customerId");
        return customerId;
    },
    locale: function() {
        return 'zh';
    },
    All: function() {

        return {
            "locale": this.locale(),
            "userId": this.userId(),
            "openId": this.openId(),
            "customerId": this.customerId()
        }
    }
} 
function MarketEvent(){
			var ec = '07D2FA499B3A4DE88F3FFB234D4F5856';
			var jsonarr = {'action':"getMarketEventAnalysis",ReqContent:JSON.stringify({"common":Base.All(),"special":{'marketEventId':getParam("marketEventId"),"timestamp":$("#MarketBoxID").attr("time")}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
				
				},
				dataType : 'json',
				success:function(o){
					
		
					
					if(o.code == "200"){
						$("#SG1").text(o.content.storeCount);
						$("#SG2").text(o.content.responseStoreCount);
						$("#SG3").text(o.content.responseStoreRate);
						$("#SG4").text(o.content.personCount);
						$("#SG5").text(o.content.pesponsePersonCount);
						$("#SG6").text(o.content.responsePersonRate);
						$("#MarketBoxID").attr("time",o.content.timestamp);
						if($("div[id^=dperson_]").length ==0){
							var tpl = _.template($("#EventScript").html(),o.content)
							$("#ConnextID").html(tpl);
						}else{
							var hep =o.content.prizeList;
							if(hep.length > 0 ){
								for(var i= 0; i< hep.length; i++){
									var hep2 =hep[i].prizeWinnerList;
									var Istring = '';
									if(hep2.length > 0 ){
										for(var c=0; c<hep2.length; c++){
											Istring = '<span style="margin-left:20px;">'+hep2[c].vipName+'</span>';
											
										}	
									}
									$("#dperson_"+hep[i].id).prepend(Istring);		
								}	
							}
						}
						gofIframe();
					}else{
						//console.log(o.description);	
					}
				}
			})
		}
var odb = 0; 
function gofIframe(){
		var getw  = $("#dframe").width(), 	geth  = $("#dframe").height() + 50;
		if(odb == geth){
			return false; 
		}
	
		$("#iframeLocalhost").remove();
		var istring = '<iframe width="1" height="1" src="http://bs.aladingyidong.com/iframe.html?rand='+parseInt(Math.random()*100000)+'#'+geth+'" scrolling="no" frameborder="0" id="iframeLocalhost" name="frameborder"';
		istring += 'style="overflow:hidden; "></iframe>';
		$("body").append(istring)
	//	window.parent.fnChangeSize(getw,geth);

}