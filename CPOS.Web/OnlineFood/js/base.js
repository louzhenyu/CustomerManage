var WeiXin = {
	SetCookie:function(name,value,expires,path){var expdate=new Date();expdate.setTime(expdate.getTime()+(expires*1000));document.cookie=name+"="+escape(value)+"; expires="+expdate.toGMTString()+
( ( path ) ? ";path=" + path : "" )}, 
	GetCookie:function(name){var arg=name+"=";var alen=arg.length;var clen=document.cookie.length;var i=0;while(i<clen){var j=i+alen;if(document.cookie.substring(i,j)==arg)return this.GetCookieVal(j);i=document.cookie.indexOf(" ",i)+1;if(i==0)break};return""},
	GetCookieVal:function(offset){var endstr=document.cookie.indexOf(";",offset);if(endstr==-1){endstr=document.cookie.length};return unescape(document.cookie.substring(offset,endstr))},
	IsEmail:function(strg){var patrn=/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;if(!patrn.exec(strg))return false;return true}
	}
document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {WeixinJSBridge.call('hideToolbar'); indexAbs()});
var AppVersion = WeiXin.GetCookie("v")?WeiXin.GetCookie("v"):"";
//var AppVersion =Math.random();
 var require = {
         urlArgs: "v="+AppVersion
	    };
var LoadfileArr = [["css","css/V1.css?v="+AppVersion,["type","text/css"],["rel","stylesheet"]],
["js","js/require.js?v="+AppVersion,["type","text/javascript"],["data-main","js/lib/app.js"],["async","true"]]];

LoadFile(LoadfileArr);
function  LoadFile(arr){
	 if(arr.length > 0 ){
		for(var i = 0; i< arr.length; i++){
			var srrf  = arr[i];
			if(arr[i][0] == "css"){
				var src = document.createElement("link");
					src.setAttribute("href",arr[i][1]);
				 	for(var c =2; c< srrf.length; c++){
							src.setAttribute(srrf[c][0],srrf[c][1]);
						}
					document.getElementsByTagName("head")[0].appendChild(src);	
			}
			if(arr[i][0] == "js"){
				var src = document.createElement("script");
					src.setAttribute("src",arr[i][1]);
				 	for(var c =2; c< srrf.length; c++){
							src.setAttribute(srrf[c][0],srrf[c][1]);
						}
					document.getElementsByTagName("head")[0].appendChild(src);	
			}			 
		}
	}	
}
var title = document.title,defaultimgUrl="";
var appId = '',imgUrl =defaultimgUrl, link = document.URL,fakeid = "",desc = desc || link;
function weiXinFollow(){

	if(typeof(WeixinJSBridge) != "object"){
		return false; 
		}
	WeixinJSBridge.on('menu:share:appmessage',function(argv) {
		WeixinJSBridge.invoke('sendAppMessage', {
			"appid": appId,
			"img_url": imgUrl,
			"img_width": "500",
			"img_height": "500",
			"link": link,
			"desc": desc,
			"title": title
		},
		function(res) {
			});
	});
	WeixinJSBridge.on('menu:share:timeline',function(argv) {
		WeixinJSBridge.invoke('shareTimeline', {
			"img_url": imgUrl,
			"img_width": "500",
			"img_height": "500",
			"link": link,
			"desc": desc,
			"title": title
		},
		function(res) {});
	});
	var weiboContent = '';
	WeixinJSBridge.on('menu:share:weibo',function(argv) {
		WeixinJSBridge.invoke('shareWeibo', {
			"content": title + link,
			"url": link
		},
		function(res) {
		});
	});
}
