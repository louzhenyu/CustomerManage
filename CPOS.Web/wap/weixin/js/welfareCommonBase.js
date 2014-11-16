// JavaScript Document

var deBug = "0"; 
var url = '';
if(deBug == "1"){
	 url = 'test.htm'; /*所有异步请求的地址*/
}else{
	 url = '../../welfare'; 
}
var Timeout = null ;

var myScroll;
function loaded() {
	
	myScroll = new iScroll('SearchListCy', { useTransition:true });

}
(function ($) {
	var pre_ajax = $.ajax;
	$.ajax = function (params) {
		params || (params = {});
        if(params['dataType'] != "html" && typeof params['data'] != 'undefined'){
            if (params['data'] != null) {
                var data =params['data'];
                   data.plat = "pc";
                params['data'] = data;
            }
        }
        pre_ajax(params);
	}
})(jQuery);


var myScrollWine;

function loadImGWine() {
	myScrollWine = new iScroll('wrapper', {
		snap: true,
		momentum: false,
		hScrollbar: false,
		onScrollEnd: function () {
			document.querySelector('#indicator > dd.active').className = '';
			document.querySelector('#indicator > dd:nth-child(' + (this.currPageX+1) + ')').className = 'active';
		}
	 });
}

function IsPC()
{ 
var userAgentInfo = navigator.userAgent; 

var Agents = new Array("Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod");
var flag = true; 
for (var v = 0; v < Agents.length; v++) { 
	if (userAgentInfo.indexOf(Agents[v])  > 0) { flag = false; break; } 
} 
return flag; 
}
function IsIEVersion(){
	
	var userAgentInfo = navigator.appVersion; 
	if(userAgentInfo.indexOf("MSIE 6.0") > 0 ){
			return 6; 
		}
	if(userAgentInfo.indexOf("MSIE 7.0") > 0 ){
			return 7; 
		}
	if(userAgentInfo.indexOf("MSIE 8.0") > 0 ){
			return 8; 
		};
	if(userAgentInfo.indexOf("MSIE 9.0") > 0 ){
			return 9; 
		};
	return 0;
}

$(function(){
	
	if(IsPC()){
		$("#PcBox,.Menu").css("width",320).show();
		$(".Menu").css("left",($(window).width() -320) / 2)
	}else{
		$("#PcBox").show();
	
	}
	$(window).resize(function(){
	$(".Menu").css("left",($(window).width() -Win.W()) / 2);	
	})
})

var position_option = {
	                enableHighAccuracy: true,
	                maximumAge: 0,
               	 	timeout: 20000
	            	};

function getParam(para){ 
	//获得html上的参数
		querystr = window.location.href.split("?")
		var iparam = "",tmp_arr = [];
		if(querystr[1]){
				var GET1s = querystr[1].split("&")
				for(i=0;i<GET1s.length;i++){
					tmp_arr = GET1s[i].split("=")
					if(para == tmp_arr[0]){
						iparam  =  	tmp_arr[1];
					}
				}
		
		}
		return iparam;
	}
$(function(){
	$(window).scroll(function(){
		
			var scrolltop = $(document).scrollTop(), DhAjax = $(document).height(), WHajax= $(window).height();
			
			if((scrolltop+WHajax+90) > DhAjax && $("#WelfareConnext").length > 0 && $("#WelfareConnext").attr("isloading")=="0" && $("#WelfareConnext").attr("isnext") == "1"){
					Welfare.getItemList();
				}
			if((scrolltop+WHajax+90) > DhAjax && $("#AppShopList").length > 0 && $("#AppShopList").attr("isloading")=="0" && $("#AppShopList").attr("isnext") == "1"){
				Welfare.getStoreListByItem();
			}
		})
	

})
var Base = {
	//公共类的获得
		openId:function(){
				 var opid = (window.localStorage&& localStorage.getItem("openId")  && localStorage.getItem("openId") !='') ? localStorage.getItem("openId") : ''; 
				 opid  = (opid == '') ? WeiXin.GetCookie("openId") : opid ;
				 return opid;
			},
		userId:function(){
			var usId =  (window.localStorage && localStorage.getItem("userId")  && localStorage.getItem("userId") !='') ? localStorage.getItem("userId") : ''; 
				usId  = (usId == '') ? WeiXin.GetCookie("userId") : usId ;
				return usId;
			},
		locale:function(){
				return 'zh';	
			},
		All:function(){
				
				return {"locale":this.locale(),"userId":this.userId(),"openId":this.openId()};
			}
	}
var  Win={
		Loading:function(type,id){
				if(Timeout){
					clearTimeout(Timeout);	
				}
				if(type=="CLOSE"&& id ){
					$(id).find(".cLoading").remove();
					return; 
					}
				if(id){
					$(id).find(".cLoading").remove();
					$(id).append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');	
					return; 
				}
				$("#BottomMenu").hide();
				
				if(type == "CLOSE"){
						$(".loading").hide();
						$("#BottomMenu").show();
						return false; 
					}
				 var getWindowsWidth = $(window).width(), getWindowsHeight = $(window).height(), dst = $(document).scrollTop();
					$(".loading").css({left: (getWindowsWidth - $(".loading").width())/ 2 , top: ((getWindowsHeight - $(".loading").height())/ 2)+dst }); 
					$(".loading").show();
			},
		W: function(){
			return $("#PcBox").width(); 
			},
		H:function(){
			return  $("#PcBox").height();
			},
		
		AniMateWidth:function(){
			return $("#CommonGID").width();	
		}
	}



var AppSet = {
	Bg:function(id, Action){
			$(".loading").hide();
			$(document).scrollTop(0);
			var divw = $("#"+id).width(), divh =  $("#"+id).height();
		//	if(divh > $(document).height()){
					//$(document).height(divh+20);
			//	}
			
			$("#AppBgGray").css({"width":Win.W(),"height":$(document).height()}).show();
			var minheight = (($("#"+id).height()  + 50) < $(document).height()) ? $(document).height() : $("#"+id).height()  + 50;	
			//	$("#CommonList").css({"min-height":minheight-60})
			$("#PcBox").css({"min-height":minheight})
			
			$("#"+id).fadeIn();
			
			
			if(Action && Action!=""){
					$("#EventAction").val(Action);
				}
			
		},
	IsLogin:function(){
		
				if((window.localStorage && localStorage.getItem("hasBindWeixin")  && localStorage.getItem("hasBindWeixin") == "1") || WeiXin.GetCookie("hasBindWeixin") == "1"  ){
					return 3;
				}
			if((window.localStorage && localStorage.getItem("isBindSucceed")  && localStorage.getItem("isBindSucceed") == "1") || WeiXin.GetCookie("isBindSucceed") == "1"  ){
					return 2;
				}
			if((window.localStorage && localStorage.getItem("hasUserAccount")  && localStorage.getItem("hasUserAccount") == "1") || WeiXin.GetCookie("hasUserAccount") == "1"  ){
				// 返回2 说明有USEID 
					return 1;
			}
			return 0; 
		},
	Close:function(id,apptype){
			$("#AppBgGray").hide();
			$("#"+id).hide();
			$("#PcBox").css("min-height",0)
			$("#ApplyBmID").html('');
			if(apptype !="1"){
					$("#EventAction").val('');
				}		
		},
	LoginSubmit:function(type){
		var username = $.trim($("#username").val()), pwd = $.trim($("#pwd").val());
		if(username == "中欧校友账号" || username == ""){
				alert('请输入帐号！');
				return false;
			}
		if(pwd=="中欧校友密码" || pwd == "" ){
				alert('请输入密码!');
				return false; 
			}
		var jsonarr = {'action':"submitAccountBind",ReqContent:JSON.stringify({"common":Base.All(),"special":{'loginName':username, 'password':pwd}})};
				$.ajax({
				type:'post',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();	
				},
				dataType : 'json',
				success:function(o){
					if(deBug=="1"){
						var o = Return_login;
					}
						Win.Loading("CLOSE");
						
					if(o.code == 200){
									
								AppSet.Close('AlumniConnext1',"1");
							//	localStorage.userName = encodeURIComponent(o.content.userName);
								if(window.localStorage){
									localStorage.setItem("userId",o.content.userId);	
									localStorage.setItem("isBindSucceed",o.content.isBindSucceed);
								}
									WeiXin.SetCookie("userId",o.content.userId,24*30*360,'/');
									WeiXin.SetCookie("isBindSucceed",o.content.isBindSucceed,24*30*360,'/');
									if(type=='1'){
										var LinkDesString = '&bindbtn=1';
										}else{
										var LinkDesString = '';
										}
							
								
								
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });

	}
}
function FocusFun(o,txt,isFocus, ispassword){	
	if(isFocus == 1){
		if(o.value == txt){
				o.value = '';	
				if(ispassword && ispassword == 1){
					o.setAttribute('type','password');
				}
			}
	}else{
		if(o.value == ''){
				o.value = txt;
				if(ispassword && ispassword == 1){
				o.setAttribute('type','text');
				}
			}	
	}
}
function IsEmail(b){var a=/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;if(!a.exec(b)){return false}return true}


function photoSize(imgObject,oWidth,oHeight)
{
	
		if(imgObject.width >= imgObject.height){
					imgObject.height  = oHeight;
					imgObject.width = (imgObject.width/oWidth ) * imgObject.height;
					
					imgObject.style.marginLeft = -((imgObject.width  - oWidth)/2 ) +"px";
			}else{
					imgObject.width  = oWidth;
				
					imgObject.height = imgObject.height /oHeight * imgObject.width;
					
			}
			
}
function ImgError(o, w, h){
	o.src = "images/default.png"; 
	o.width = w; 
	o.height = h; 
}
var Pop={
	 Maskspublic:function(maskBoolen,boxwidth,boxContext,isTopbox,clo){
		
		//if(ttime){clearInterval(ttime)};
        var masksBox = "masksBox", ifram = "ifram", poupId = "poupId", conInsertId = "conInsertId",isneednum="";
        var getZindex ="", StyleIndex = '';
        if(isTopbox == true){
                var getBoxLength = $("div[id^=poupId]").length;
                        if(getBoxLength >0 ){
                                 getZindex = parseInt($("div[id^=poupId]").eq(getBoxLength-1).css("z-index"));
                        }
                         masksBox = "masksBox"+(getZindex+1), ifram = "ifram"+(getZindex+1), poupId = "poupId"+(getZindex+1), conInsertId = "conInsertId"+(getZindex+1);
                          StyleIndex = (getZindex=="")? "":"z-index:"+(getZindex+2), StyleIndex1 = (getZindex=="")? "":"z-index:"+(getZindex+3);
                }else{
                this.Close("1");	
        }
        var txtBase = new String();
        
        
    
        var getDocumentScrollHeight = $(document).scrollTop();
		 boxwidth = ($(window).width() - 20 ) >= 300 ? 300 :  $(window).width() - 20 ;
        if(maskBoolen==undefined || maskBoolen==false ){
           	 txtBase="";
        }else{ 
            txtBase = "<div id='"+masksBox+"' class='masksBox' style='height:" + $(document).height() + "px; "+StyleIndex+"'></div>";
            if ($.browser.msie && ($.browser.version < "7.0")) {
                txtBase = "<div id='"+masksBox+"' class='masksBox' style=' height:" + $(document).height() + "px;"+StyleIndex+"'><iframe width='100%' class='ifram' height='"+$(document).height()+"' frameborder=0 id='"+ifram+"' style='"+StyleIndex+"'></iframe></div>";		
            }
        }
	var tString = new String();
       
        if(boxContext&&boxContext!=""){
            tString = boxContext; 
        }
 
                txtBase +='<div style="width:'+boxwidth+'px; display:none; '+StyleIndex+'" class="poupId" id="'+poupId+'"><div class="pupdiv">';
                
                txtBase += '<div id="'+conInsertId+'">'+tString+'</div></div>';
             
		
        $("#PcBox").append(txtBase);
        var getPoupHeight = $("#"+poupId).height();
		
        var getPoupLeft =($("#PcBox").width() - boxwidth) / 2
        if(clo=="1"){
            var getPoupTop = (($(window).height()/2) - getPoupHeight/2)>0 ? ($(window).height()/2) - (getPoupHeight/2):0;
        }else{
            var getPoupTop = (getDocumentScrollHeight+($(window).height()/2) - getPoupHeight/2)>0 ? getDocumentScrollHeight + ($(window).height()/2) - (getPoupHeight/2):0;
        }
	
       // $("#"+poupId).css({"left":getPoupLeft,"top":getPoupTop});
			$("#"+poupId).show().css({"left":getPoupLeft,"top":$(window).height(),"opacity":0});
        if (!$.browser.msie && $(document).height()<6264){

			var owebkit ="-webkit-radial-gradient(center "+(getPoupTop+100)+"px,rgba(0,0,0,0.1),rgba(0,0,0,0.9) 80%)";
			if(navigator.appVersion.indexOf("WebKit")==-1){
					owebkit = "-moz-radial-gradient(center "+(getPoupTop+100)+"px,rgba(0,0,0,0.1),rgba(0,0,0,0.9) 80%)";
				}
            $("#"+masksBox).css({"background":owebkit}); 
        }else{
            $("#"+masksBox).css({"opacity":0.2});
        }
		
        $("#"+poupId).animate({"top":getPoupTop,"opacity":1},500);
		$("body").css({"overflow":"hidden"});
    },
	Close:function(type){
		$("body").css({"overflow":"auto"});
			if(type == "1"){
					$("#masksBox,.poupId").remove();
				}else{
			$("#masksBox").fadeOut(500,function(){$(this).remove()});
			$(".poupId").animate({top:0,"opacity":0},400,function(){$(this).remove();});	
				}
		},
	Alert:function(txt){
		Pop.Maskspublic(true,"300",'<div class="ccTitle">提示</div><div class="ccTxt">'+txt+'</div><div class="ccBtnClose"><input type="button" onclick="Pop.Close()" value="确定" class="btnStylecc"></div>',false,"");

		}
	
	}