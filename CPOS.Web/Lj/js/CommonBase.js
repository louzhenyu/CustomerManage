// JavaScript Document
var deBug = "0";
var url = '';
if (deBug == "1") {
    url = 'test.htm';
    /*所有异步请求的地址*/
} else {
    url = 'data/Data.aspx';
}
var Timeout = null;

function LinkAddCustomerId(LinkUrl) {

    if (LinkUrl.indexOf("customerId") <= -1) {
        if (LinkUrl.indexOf("?") > -1) {
            location.href = LinkUrl + "&customerId=" + getParam("customerId") + "&random=" + Math.floor(Math.random() * 10000000);
        } else {
            location.href =LinkUrl + "?customerId=" + getParam("customerId")  + "&random=" + Math.floor(Math.random() * 10000000);
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
                data.plat = "pc";
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
var myScrollWine;

function loadImGWine() {
    myScrollWine = new iScroll('wrapper', {
        snap: true,
        momentum: false,
        hScrollbar: false,
        onScrollEnd: function() {
            document.querySelector('#indicator > dd.active').className = '';
            document.querySelector('#indicator > dd:nth-child(' + (this.currPageX + 1) + ')').className = 'active';
        }
    });
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
    Init();
    if (IsPC()) {
        $("#PcBox").css("width", 320).show();

    } else {
        $("#PcBox").show();
	/*	$("input[type=text],input[type=password],textarea").live("focusin",function(){
					$("#MenuBox").hide();
		})
			$("input[type=text],input[type=password],textarea").live("focusout",function(){
					$("#MenuBox").show();
		})*/
    }
		
})

var position_option = {
    enableHighAccuracy: true,
    maximumAge: 0,
    timeout: 20000
};

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
$(function() {
    $("a").live("click",
    function() {
        var getHref = $(this).attr("href");
        if (getHref.indexOf("customerId") <= -1 && getHref != 'javascript:void(0)' && getHref != 'javascript:;' && getHref.indexOf("tel:") <= -1 && getHref.indexOf("mailto:") <= -1) {
            if (getHref.indexOf("?") > -1) {
                location.href =getHref + "&customerId=" + getParam("customerId")   +"&random=" + Math.floor(Math.random() * 10000000);
            } else {
             location.href =getHref + "?customerId=" + getParam("customerId") +"&random=" + Math.floor(Math.random() * 10000000);
            }
			   return false;
        }
     
    })
})

$(function() {
    $(window).scroll(function() {

        var scrolltop = $(document).scrollTop(),
        DhAjax = $(document).height(),
        WHajax = $(window).height();
        if (loadMore != "undefined" && loadMore != "") {
            if ((scrolltop + WHajax + 90) > DhAjax && $("#" + loadMore[0]).length > 0 && $("#" + loadMore[0]).attr("isloading") == "0" && $("#" + loadMore[0]).attr("isnext") == "1") {
                eval(loadMore[1]);

            }
        }

    })

});
function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++){
        var n = Math.floor(Math.random()*16.0).toString(16);
        guid += n;
    }
    return guid;
};
function G(param){

	var customerId = getParam("customerId"), abc = [], returnData =null, ob= false; 
	if(customerId !=""){
		var BaseCookie = WeiXin.GetCookie("base"); 
		if(BaseCookie!=""){
			var san= JSON.parse(BaseCookie),adscc =[];
			if(san.length > 0 ){
				for(var i=0; i< san.length; i++){
					if(customerId == san[i].customerId){
						returnData = eval("san[i]."+param);
						
						if(typeof(san[i].skin)== "object"){
							
							ob = true;	
							adscc.push(san[i]);
						}
						
					}
				}	
				
				if(ob== false){
					abc = adscc;
				}
			}
			
			if(ob == true){
				return 	returnData;
			}
		}
			var iAPi = {customerId:getParam("customerId"), openId:newGuid,userId:newGuid,islogin:"0",skin:{unitName:'',isApp:'',firstPageImage:'',loginImage:''}};
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
						if(typeof(value) == "object"){
							eval("san[i]."+param+"="+JSON.stringify(value));
							}else{
							eval("san[i]."+param+"='"+value+"'");
						
						}
						ob = true;
					}
					abc.push({customerId:san[i].customerId, openId:san[i].openId,userId:san[i].userId,islogin:san[i].islogin,skin:san[i].skin});
				}	
			}
			
		}
			if(ob== false){
			var iAPi = {customerId:getParam("customerId"), openId:newGuid,userId:newGuid,islogin:"0",skin:{unitName:'',isApp:'',firstPageImage:'',loginImage:''}};
				eval("iAPi."+param+"='"+value+"'");
				abc.push(iAPi);
			}
			var ist =  JSON.stringify(abc);
			WeiXin.SetCookie("base", ist, 24 * 30 * 360*120, '/');	
		
			return 1;
	}
   return ""; 
}
function LoginExit(){
	WeiXin.SetCookie("base", "", 24 * 30 * 360*120, '/');	
	LinkAddCustomerId("login.html");
}
var newGuid  =  newGuid();
 var Base = {
    //公共类的获得
    openId: function() {
	
		//if(WeiXin.GetCookie("openId")  == "" || WeiXin.GetCookie("openId")  == null ){
			//    WeiXin.SetCookie("openId", newGuid, 24 * 30 * 360*60, '/');
		//	}
      //  var opid = WeiXin.GetCookie("openId");
        return G("openId");
        // return 'o8Y7Ejv3jR5fEkneCNu6N1_TIYIM';
    },
    userId: function() {
		//if(WeiXin.GetCookie("userId")  == ""|| WeiXin.GetCookie("userId")  == null  ){
			//    WeiXin.SetCookie("userId", newGuid, 24 * 30 * 360*60, '/');
		//}
     //   var userId = WeiXin.GetCookie("userId");
		 return G("userId");
      // return userId;
        //   return 'f7a5863a51334f1e8bd46ea3f0e3278f';
    },
    customerId: function() {
        var customerId = getParam("customerId");
        return customerId;
        //   return 'f7a5863a51334f1e8bd46ea3f0e3278f';
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

var AppSet = {
    Bg: function(id, Action) {
        $(".loading").hide();
        $(document).scrollTop(0);
        var divw = $("#" + id).width(),
        divh = $("#" + id).height();
        //	if(divh > $(document).height()){
        //$(document).height(divh+20);
        //	}
        $("#AppBgGray").css({
            "width": Win.W(),
            "height": $(document).height()
        }).show();
        var minheight = (($("#" + id).height() + 50) < $(document).height()) ? $(document).height() : $("#" + id).height() + 50;
        //	$("#CommonList").css({"min-height":minheight-60})
        $("#PcBox").css({
            "min-height": minheight
        })

        $("#" + id).fadeIn();

        if (Action && Action != "") {
            $("#EventAction").val(Action);
        }

    },
    IsLogin: function() {

        if ( G("islogin")== "1") {
            return 3;
        }
       
        return 0;
    },

    LoginSubmit: function(type) {
        var username = $.trim($("#username").val()),
        pwd = $.trim($("#pwd").val());
	if(username=="" || pwd==""){
				return false; 	
		}
		var  regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;
		 if( !regAee.test(username)){
			alert("请正确输入手机号码！");
			return false;  
			}
        var jsonarr = {
            'action': "setSignIn",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'phone': username,
                    'password': pwd
                }
            })
        };
        $.ajax({
            type: 'post',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                Win.Loading();
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");

                if (o.code == 200) {
					S("islogin", "1");
                   	 S("userId", o.content.userId);
					 S("openId", o.content.openId);
                 	 alert("登录成功");
               		if(getParam("back")!=""){
						LinkAddCustomerId(decodeURIComponent(getParam("back")));
					}else{
						LinkAddCustomerId("vipCenter.html");	
					}
                } else {
                    alert(o.description);
                  
                }
				  return false;
            }
        });
    },
	RegSubmit:function() {
        var username = $.trim($("#username1").val()),
        pwd = $.trim($("#pwd1").val());
		if(username=="" || pwd==""){
			return false; 	
		}
		 var regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;
		 if( !regAee.test(username)){
			alert("请正确输入手机号码！");
			return false;  
			}
        var jsonarr = {
            'action': "setSignUp",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'phone': username,
                    'password': pwd
                }
            })
        };
        $.ajax({
            type: 'post',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                Win.Loading();
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");

                if (o.code == 200) {
                    S("islogin", "1");
                    S("userId", o.content.userId);
                    S("openId", o.content.openId);
                    alert("注册成功");
                    if (getParam("back") != "") {
                        LinkAddCustomerId(decodeURIComponent(getParam("back")));
                    } else {
                        LinkAddCustomerId("vipCenter.html");
                    }
                } else {
                    alert(o.description);
                }
				  return false;
            }
        });
    },
	forget:function() {
        var username = $.trim($("#username").val());
       var  regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;
		 if( !regAee.test(username)){
			alert("请正确输入手机号码！");
			return false;  
			}
		if(username==""){
			return false; 	
		}
        var jsonarr = {
            'action': "setSMSPush",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'phone': username
                }
            })
        };
        $.ajax({
            type: 'post',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                Win.Loading();
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");

                if (o.code == 200) {
				
					 alert("发送成功");
            			$("#username").val("");
                } else {
                    alert(o.description);
                }
				  return false;
            }
        });
	},
	forgetpwd:function() {
        var username = $.trim($("#username").val());
        var pwd = $.trim($("#pwd").val());
		var pwd1 = $.trim($("#pwd1").val());
		var regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;
		 if( !regAee.test(username)){
			alert("请正确输入手机号码！");
			return false;  
			}
		if(username==""|| pwd=="" || pwd1==""){
			return false; 	
		}
		if(pwd!=pwd1){
			alert("两次密码输入不一样！")
			return false; 	
		}
        var jsonarr = {
            'action': "setVipPassword",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'phone': username,
					'password':pwd
                }
            })
        };
        $.ajax({
            type: 'post',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                Win.Loading();
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");

                if (o.code == 200) {
				
						 alert("修改成功");
						 location.href = document.URL;
            			
                } else {
                    alert(o.description);
                }
				  return false;
            }
        });
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

function photoSize(imgObject, oWidth, oHeight) {

    if (imgObject.width >= imgObject.height) {
        imgObject.height = oHeight;
        imgObject.width = (imgObject.width / oWidth) * imgObject.height;

        imgObject.style.marginLeft = -((imgObject.width - oWidth) / 2) + "px";
    } else {
        imgObject.width = oWidth;

        imgObject.height = imgObject.height / oHeight * imgObject.width;

    }

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
function openFollowTips(){
	var getDoH = $(document).height() > $(window).height() ? $(document).height() : $(window).height();
	var Istring="<div style='height:"+getDoH+"px; z-index:9' class='masksBox' onclick='CloseOpenTips(event)'></div><div style='position:absolute; right:5px; top:10px; z-index:10' id='tipso'  onclick='CloseOpenTips(event)'><img src='images/followtips.png' width='300px' /></div>"	
	$("body").append(Istring);
	$(document).scrollTop(0);
}
function CloseOpenTips(e){
	$(".masksBox,#tipso").remove();
	 e.stopPropagation();
    e.preventDefault();
}
function Init() {
    if ($("#MenuBox").length > 0) {
        var getRel = $("#MenuBox").attr("rel");
        var IMenu = '<ul><li><a onclick="backReload()" href="javascript:void(0)"';
        if (getRel == "1") {
            IMenu += " class='ImenuClass' "
        }
        IMenu += '><div class="smain1"><img src="images/nt1.png"></div>返回</a></li> <li><a href="index.html"><div class="smain1"><img src="images/nt2.png"></div> 首页</a></li><li><a href="share.html"';
        if (getRel == "3") {
            IMenu += " class='ImenuClass' "
        }
        IMenu += '><div class="smain1"><img src="images/nt3.png"></div>分享</a></li><li><a onclick="GotoLogin(\'vipCenter.html\')" href="javascript:void(0)"';
        if (getRel == "4") {
            IMenu += " class='ImenuClass' "
        }
        IMenu += '><div class="smain1"><img src="images/nt4.png"></div>我</a></li></ul>';
        $("#MenuBox").html(IMenu);
    }
}
function backReload(){
	if(getParam("back")!=""){
			LinkAddCustomerId(getParam("back")+".html");
			return false; 	
		}
	history.back();
//setTimeout(function(){location.href = document.URL},10)
}
function GotoLogin(linkurl){
	if(AppSet.IsLogin() > 0 ){
		LinkAddCustomerId(linkurl);	
	}else{
		LinkAddCustomerId("login.html");		
	}
}
function SelectList(o, e,skuId) {
    var $soo = $(o).siblings("img");
    var cCardNum = $("#CardNum").offset();

    var dSrc = $soo.offset();

    var getSelect = $(o).attr("isselect");
    if (getSelect == "0") {
        $(o).attr("isselect", "1");
        $(o).find("img").attr("src", "images/right1.png");
        $("body").append("<img src='" + $soo.attr("src") + "' width='" + $soo.width() + "' style='position:absolute;left:" + dSrc.left + "px; top:" + dSrc.top + "px' class='sssda'   >");
        $(".sssda").animate({
            left: cCardNum.left,
            top: cCardNum.top,
            width: 0,
            height: 0
        },
        800,
        function() {
            $(this).remove();
        });
        $("#CardNum").text(parseInt($("#CardNum").text()) + 1);
		Vip.setShoppingCart(skuId,"list")
    } else {
        $(o).attr("isselect", "0");
        $(o).find("img").attr("src", "images/right2.png");
        $("#CardNum").text(parseInt($("#CardNum").text()) - 1);
		Vip.DelSKuId(skuId,"list")
    }
    e.stopPropagation();
    e.preventDefault();
    return false;
}
function animateAll(startid,endId,e){
	var $ste = $(startid);
    var cCardNum = $(endId).offset();
    var dSrc = $(startid).offset();
   	var RandId = Math.floor(Math.random()*1000);
        $("body").append("<img style='position:absolute;left:" + dSrc.left + "px; top:" + dSrc.top + "px; width:"+$ste.width()+"px; height:"+$ste.height()+"px' id='RandId_"+RandId+"' class='AniClass' src='"+$ste.find("img").attr("src")+"' />");
        $("#RandId_"+RandId).animate({
            left: cCardNum.left,
            top: cCardNum.top,
            width: 0,
            height: 0
        },
        1200,
        function() {
            $(this).remove();
			$(endId).text(parseInt($(endId).text())+1);
        });
	e.stopPropagation();
			e.preventDefault();
     return false; 
}
function getVersion(){
		$.ajax({
			  url: "js/version.js",
			  dataType: "text",
			  loadd:"c",
			   cache:false,
			  success: function(result){
					WeiXin.SetCookie("v",result,60*60*24*60,"/");
					if(G("skin.unitName")!="" && WeiXin.GetCookie("v") !=result){
							getConfig();
					}
				}
			});
}

function getConfig(){
		 var jsonarr = {
            'action': "getCustomerDetail",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                   storeId:''
                }
            })
        };
		$.ajax({
			  url: url,
			  dataType: "json",
			  data:jsonarr,
			  cache:false,
			  success: function(result){
			      //WeiXin.SetCookie("v",result,60*60*24*60,"/");
			      if (result != null) {
			          var ObjBase = result.content;
			          var skin = {};
			          skin.firstPageImage = ObjBase.firstPageImage;
			          skin.unitName = ObjBase.unitName;
			          skin.isApp = ObjBase.isApp;
			          skin.loginImage = ObjBase.loginImage;
			          S("skin", skin);
			          document.title = ObjBase.unitName;
			          $("#firstPageImage").attr("src", ObjBase.firstPageImage);
			          $("#loginImage").attr("src", ObjBase.loginImage);
			          $("#AppdownLoadIndex").show();
			      }
				}
			});
}
function SelectGouWu(){
	$(document).scrollTop(0);
	var getDocumentHeight = $(document).height();	
	$("#appOpacity").css({"height":getDocumentHeight}).show();
	$("#appDetailBox").show();
	GetData.getSkuProp2List(getParam("itemId"),$("#prop2DetailId").val());
}
function selectItemList(propDetailId,o){
		$(o).siblings("dd").removeClass("DefaultDo");
		$(o).addClass("DefaultDo");
		$("#DetailSkuId").val($(o).attr("skuId"));
		GetData.getSkuProp2List(getParam("itemId"),propDetailId);
}
function ClickCloseDetail(){
	$("#appOpacity,#appDetailBox").hide();
}
function selectPop2ItemList(prop2DetailId,skuId,o){
	$(o).siblings("dd").removeClass("DefaultDo");
	$(o).addClass("DefaultDo");	
	$("#DetailSkuId").val($(o).attr("skuId"));
}