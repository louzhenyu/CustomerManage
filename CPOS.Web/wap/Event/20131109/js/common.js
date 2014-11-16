
var url = '../../../WEvent/weixinData.aspx';

var Timeout = null;

function LinkAddCustomerId(LinkUrl) {

    if (LinkUrl.indexOf("customerId") <= -1) {
        if (LinkUrl.indexOf("?") > -1) {
            location.href = LinkUrl + "&customerId=" + getParam("customerId") +  "&storeId=" + getParam("storeId") + "&random=" + Math.floor(Math.random() * 10000000);
        } else {
            location.href =LinkUrl + "?customerId=" + getParam("customerId") +  "&storeId=" + getParam("storeId") + "&random=" + Math.floor(Math.random() * 10000000);
        }

    }
}
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
			params.complete = function(){
			
		
				if(jsjfalse == false){
					jsjfalse = true;
					getVersion();	
				}
				
			}
        }
        pre_ajax(params);
    }
})(jQuery);
var myScroll;
function loaded() {
	myScroll = new iScroll('SearchListCy', { useTransition:true });
}


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
function IsIEVersion() {

    var userAgentInfo = navigator.appVersion;
    if (userAgentInfo.indexOf("MSIE 6.0") > 0) {
        return 6;
    }
    if (userAgentInfo.indexOf("MSIE 7.0") > 0) {
        return 7;
    }
    if (userAgentInfo.indexOf("MSIE 8.0") > 0) {
        return 8;
    };
    if (userAgentInfo.indexOf("MSIE 9.0") > 0) {
        return 9;
    };
    return 0;
}

$(function() {
//    Init();
    if (IsPC()) {
        $("#PcBox").css("width", 320).show();
    } else {
        $("#PcBox").show();
    }

})

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



function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++){
        var n = Math.floor(Math.random()*16.0).toString(16);
        guid += n;
    }
    return guid;
};
function G(param){

	var customerId = getParam("customerId"), abc = [], returnData ="", ob= false; 
	if(customerId !=""){
		var BaseCookie = WeiXin.GetCookie("base"); 
		if(BaseCookie!=""){
			var san= JSON.parse(BaseCookie);
			if(san.length > 0 ){
				for(var i=0; i< san.length; i++){
					if(customerId == san[i].customerId){
						returnData = eval("san[i]."+param);
						ob = true;
					}
				}	
				if(ob== false){
					abc = san;
				}
			}
			
			if(ob == true){
				return 	returnData;
			}
		}
			var iAPi = {customerId:getParam("customerId"), openId:newGuid,userId:newGuid,islogin:"0"};
			abc.push(iAPi);
			var ist =  JSON.stringify(abc);
			WeiXin.SetCookie("base", ist, 24 * 30 * 360*120, '/');	
		
			return eval("iAPi."+param);
	}
   return ""; 
}
function S(param,value){
		var customerId = getParam("customerId"), abc = [], returnData ="", ob= false; 
	if(customerId !=""){
		var BaseCookie = WeiXin.GetCookie("base"); 
		if(BaseCookie!=""){
			var san= JSON.parse(BaseCookie);
			if(san.length > 0 ){
				for(var i=0; i< san.length; i++){
					if(customerId == san[i].customerId){
					//	alert(eval("san[i]."+param))
						eval("san[i]."+param+"='"+value+"'");
						ob = true;
					}
					abc.push({customerId:san[i].customerId, openId:san[i].openId,userId:san[i].userId,islogin:san[i].islogin});
				}	
			}
			
		}
			if(ob== false){
			var iAPi = {customerId:getParam("customerId"), openId:newGuid,userId:newGuid,islogin:"0"};
				eval("iAPi."+param+"='"+value+"'");
				abc.push(iAPi);
			}
			var ist =  JSON.stringify(abc);
			WeiXin.SetCookie("base", ist, 24 * 30 * 360*120, '/');	
		
			return 1;
	}
   return ""; 
}

var newGuid  =  newGuid();
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
var Win = {
    Loading: function(type, id) {
        if (Timeout) {
            clearTimeout(Timeout);
        }
        if (type == "CLOSE" && id) {
            $(id).find(".cLoading").remove();
            return;
        }
        if (id) {
            $(id).find(".cLoading").remove();
            $(id).append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');
            return;
        }
        $("#BottomMenu").hide();

        if (type == "CLOSE") {
            $(".loading").hide();

            return false;
        }
        var getWindowsWidth = $(window).width(),
        getWindowsHeight = $(window).height(),
        dst = $(document).scrollTop();
        $(".loading").css({
            left: (getWindowsWidth - $(".loading").width()) / 2,
            top: ((getWindowsHeight - $(".loading").height()) / 2) + dst
        });
        $(".loading").show();
    },
    W: function() {
        return $("#PcBox").width();
    },
    H: function() {
        return $("#PcBox").height();
    }
}


function FocusFun(o, txt, isFocus, ispassword) {
    if (isFocus == 1) {
        if (o.value == txt) {
            o.value = '';
            if (ispassword && ispassword == 1) {
                o.setAttribute('type', 'password');
            }
        }
    } else {
        if (o.value == '') {
            o.value = txt;
            if (ispassword && ispassword == 1) {
                o.setAttribute('type', 'text');
            }
        }
    }
}
function IsEmail(b) {
    var a = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (!a.exec(b)) {
        return false
    }
    return true
}


function ImgError(o, w, h) {
    o.src = "images/default.png";
    o.width = w;
    o.height = h;
}
var Pop = {
    Maskspublic: function(maskBoolen, boxwidth, boxContext, isTopbox, clo) {

        //if(ttime){clearInterval(ttime)};
        var masksBox = "masksBox",
        ifram = "ifram",
        poupId = "poupId",
        conInsertId = "conInsertId",
        isneednum = "";

        var getZindex = "",
        StyleIndex = '';
        if (isTopbox == true) {
            var getBoxLength = $("div[id^=poupId]").length;
            if (getBoxLength > 0) {
                getZindex = parseInt($("div[id^=poupId]").eq(getBoxLength - 1).css("z-index"));
            }
            masksBox = "masksBox" + (getZindex + 1),
            ifram = "ifram" + (getZindex + 1),
            poupId = "poupId" + (getZindex + 1),
            conInsertId = "conInsertId" + (getZindex + 1);
            StyleIndex = (getZindex == "") ? "": "z-index:" + (getZindex + 2),
            StyleIndex1 = (getZindex == "") ? "": "z-index:" + (getZindex + 3);
        } else {
            this.Close("1");
        }
        var txtBase = new String();

        var getDocumentScrollHeight = $(document).scrollTop();
        boxwidth = ($(window).width() - 20) >= 300 ? 300 : $(window).width() - 20;
        if (maskBoolen == undefined || maskBoolen == false) {
            txtBase = "";
        } else {
            txtBase = "<div id='" + masksBox + "' class='masksBox' style='height:" + $(document).height() + "px; " + StyleIndex + "'></div>";
            if ($.browser.msie && ($.browser.version < "7.0")) {
                txtBase = "<div id='" + masksBox + "' class='masksBox' style=' height:" + $(document).height() + "px;" + StyleIndex + "'><iframe width='100%' class='ifram' height='" + $(document).height() + "' frameborder=0 id='" + ifram + "' style='" + StyleIndex + "'></iframe></div>";
            }
        }
        var tString = new String();

        if (boxContext && boxContext != "") {
            tString = boxContext;
        }

        txtBase += '<div style="width:' + boxwidth + 'px; display:none; ' + StyleIndex + '" class="poupId" id="' + poupId + '"><div class="pupdiv">';

        txtBase += '<div id="' + conInsertId + '">' + tString + '</div></div>';

        $("#PcBox").append(txtBase);
        var getPoupHeight = $("#" + poupId).height();

        var getPoupLeft = ($("#PcBox").width() - boxwidth) / 2
        if (clo == "1") {
            var getPoupTop = (($(window).height() / 2) - getPoupHeight / 2) > 0 ? ($(window).height() / 2) - (getPoupHeight / 2) : 0;
        } else {
            var getPoupTop = (getDocumentScrollHeight + ($(window).height() / 2) - getPoupHeight / 2) > 0 ? getDocumentScrollHeight + ($(window).height() / 2) - (getPoupHeight / 2) : 0;
        }

        // $("#"+poupId).css({"left":getPoupLeft,"top":getPoupTop});
        $("#" + poupId).show().css({
            "left": getPoupLeft,
            "top": $(window).height(),
            "opacity": 0
        });
        if (!$.browser.msie && $(document).height() < 6264) {

            var owebkit = "-webkit-radial-gradient(center " + (getPoupTop + 100) + "px,rgba(0,0,0,0.1),rgba(0,0,0,0.9) 80%)";
            if (navigator.appVersion.indexOf("WebKit") == -1) {
                owebkit = "-moz-radial-gradient(center " + (getPoupTop + 100) + "px,rgba(0,0,0,0.1),rgba(0,0,0,0.9) 80%)";
            }
            $("#" + masksBox).css({
                "background": owebkit
            });
        } else {
            $("#" + masksBox).css({
                "opacity": 0.2
            });
        }

        $("#" + poupId).animate({
            "top": getPoupTop,
            "opacity": 1
        },
        500);
        $("body").css({
            "overflow": "hidden"
        });
    },
    Close: function(type) {
        $("body").css({
            "overflow": "auto"
        });
        if (type == "1") {
            $("#masksBox,.poupId").remove();
        } else {
            $("#masksBox").fadeOut(500,
            function() {
                $(this).remove()
            });
            $(".poupId").animate({
                top: 0,
                "opacity": 0
            },
            400,
            function() {
                $(this).remove();
            });
        }
    },
    Alert: function(txt) {
        Pop.Maskspublic(true, "300", '<div class="ccTitle">提示</div><div class="ccTxt">' + txt + '</div><div class="ccBtnClose"><input type="button" onclick="Pop.Close()" value="确定" class="btnStylecc"></div>', false, "");

    }

}


function getVersion(){
		$.ajax({
			  url: "js/version.js",
			  dataType: "text",
			  loadd:"c",
			   cache:false,
			  success: function(result){
				WeiXin.SetCookie("eventv",result,60*60*24*60,"/");
				}
			});
}