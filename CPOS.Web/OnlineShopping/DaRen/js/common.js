// JavaScript Document

var deBug = "0"; 
var url = '';
if(deBug == "1"){
	 url = 'test.htm'; /*所有异步请求的地址*/
}else{
	 url = '../data/Data.aspx'; 
}
var Timeout = null ;



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
	$("#PcBox").css("width",320).show();
	
	}else{
		$("#PcBox").show();
	}
	if(getParam("customerId") != ""){
		WeiXin.SetCookie("customerId",getParam("customerId"),24*30*3600,'');
	}
	
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

var Base = {
    //公共类的获得
    openId: function () {
        var opid = (WeiXin.GetCookie("openId") == null) ? "" : WeiXin.GetCookie("openId");
        return opid;
        // return 'o8Y7Ejv3jR5fEkneCNu6N1_TIYIM';
    },
    userId: function () {
        var userId = (WeiXin.GetCookie("userId") == null) ? "" : WeiXin.GetCookie("userId");
        return userId;
        //   return 'f7a5863a51334f1e8bd46ea3f0e3278f';
    },
    signUpId: function () {
        var signUpId = (WeiXin.GetCookie("signUpId") == null) ? "" : WeiXin.GetCookie("signUpId");
        return signUpId;
        //   return 'f7a5863a51334f1e8bd46ea3f0e3278f';
    },
	 customerId: function () {
        var customerId = getParam("customerId") ? getParam("customerId") :((WeiXin.GetCookie("customerId") == null) ? "" : WeiXin.GetCookie("customerId"));
        return customerId;
       
    },
    locale: function () {
        return 'zh';
    },
    All: function () {

        return { "locale": this.locale(), "userId": this.userId(), "openId": this.openId(), "signUpId": this.signUpId(), "customerId":this.customerId() }
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
	Bg:function(id,actionType){
			$(".loading").hide();
			$(document).scrollTop(0);
			var divw = $("#"+id).width(), divh =  $("#"+id).height();

			$("#AppBgGray").css({"width":Win.W(),"height":$(document).height()}).show();
			var minheight = (($("#"+id).height()  + 50) < $(document).height()) ? $(document).height() : $("#"+id).height()  + 50;	

			$("#PcBox").css({"min-height":minheight})
			
			$("#"+id).fadeIn();	
			if(actionType !=""){
				$("#actionType").val(actionType)
			}
			
		},
	IsLogin:function(){
			if(WeiXin.GetCookie("signUpId") && WeiXin.GetCookie("signUpId")!=""  ){
					return 1;
			}
			return 0; 
		},
	Close:function(id){
			$("#AppBgGray").hide();
			$("#"+id).hide();
			
			
					
		},
	LoginSubmit:function(){
		var username = $.trim($("#username").val()), phone = $.trim($("#phone").val());
		if( username == ""){
				alert('请输入名称！');
				return false;
			}
		if(phone == "" ){
				alert('请输入手机号码!');
				return false; 
			}
		var jsonarr = {'action':"setEventSignUp",ReqContent:JSON.stringify({"common":Base.All(),"special":{'username':username, 'phone':phone}})};
				$.ajax({
				type:'get',
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
						var o = setEventSignUp;
					}
						Win.Loading("CLOSE");
						
					if(o.code == 200){
									
								AppSet.Close('EventApply');
							
								WeiXin.SetCookie("signUpId",o.content.signUpId,20*24*30*3600,'');
								WeiXin.SetCookie("phone",phone,20*24*30*3600,'');
								WeiXin.SetCookie("DaRenUserName",username,20*24*30*3600,'');
								var actiontype = $("#actionType").val();
							if(actiontype == "Comment"){
								DaRen.setEventsEntriesComment(getParam("entriesId"));
							}
							if(actiontype == "Good"){
								DaRen.setEventsEntriesPraise(getParam("entriesId"));
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
var DaRen = {
		getEventsEntriesList:function(){
			var getStrDate = 	getParam("strDate");
			var jsonarr = {'action':"getEventsEntriesList",ReqContent:JSON.stringify({"common":Base.All(),"special":{'strDate':getStrDate}})};
				$.ajax({
				type:'get',
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
						var o = getEventsEntriesList;
					}
					Win.Loading("CLOSE");
					if(o.code == "200"){
							var ReturnHtml = Tpl.getEventsEntriesList(o.content);
							$("#DaRenImgDiv").html(ReturnHtml);
							$("#StrDateO").text(o.content.strDate);
							$("#DaRenDivId").show();		
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });

		},
		getEventsEntriesCommentList:function(OnlyComment){
			if(OnlyComment !="1"){
				$("#DaRenPhotoDetail").hide();
			}else{
					$(".moreComment").remove();
				}
			var entriesId = getParam("entriesId"), page = $("#DaRenPhotoDetail").attr("page");
			var jsonarr = {'action':"getEventsEntriesCommentList",ReqContent:JSON.stringify({"common":Base.All(),"special":{'entriesId':entriesId,"page":page,"pageSize":10}})};
				$.ajax({
				type:'get',
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
						var o = getEventsEntriesCommentList;
					}
					Win.Loading("CLOSE");
					if(o.code == "200"){
							if(OnlyComment != "1"){
								var ReturnHtml = Tpl.getEventsEntriesCommentList(o.content,10,page);
								$("#DaRenPhotoDetail").html(ReturnHtml).show();
							}else{	
								var ReturnHtml = Tpl.MoreComment(o.content,10,page);
								$("#PhotoCommentList").append(ReturnHtml);
							}
						
							$("#DaRenPhotoDetail").attr("page",(parseInt(page)+1));
						
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });

		},
	setEventsEntriesComment:function(entriesId){
		
		var  content = $.trim($("#PhotoCommentTextareaId").val());
		if(content ==""){
			alert("请输入评论内容");
			return false; 
			}
		if(AppSet.IsLogin() != 1){
			AppSet.Bg("EventApply","Comment");
			return false; 	
		}
			var jsonarr = {'action':"setEventsEntriesComment",ReqContent:JSON.stringify({"common":Base.All(),"special":{'entriesId':entriesId,"content":content}})};
				$.ajax({
				type:'get',
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
						var o = getEventsEntriesCommentList;
					}
					Win.Loading("CLOSE");
					if(o.code == "200"){
							$("#NoCommentId").hide();
							//alert(o.description);
							$("#PhotoCommentTextareaId").val('')
							var Rand = Math.floor(Math.random() * 10);
								var getPhone = WeiXin.GetCookie("phone"); 
							
							var ccgetPhone = '';
							if(getPhone.length > 4){
						  		ccgetPhone = getPhone.substring(0,3)+"****"+getPhone.substring(7,getPhone.length);
							}
							var Istring = '<div class="PhotoCommentLi"><strong>'+WeiXin.GetCookie("DaRenUserName")+'</strong>('+ccgetPhone+')：'+content+'<p class="PhotoCommentLiP">'+Rand+'秒前</p></div>';
							$("#PhotoCommentList").prepend(Istring);
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });

	},
	setEventsEntriesPraise:function(entriesId){
		
		
		if(AppSet.IsLogin() != 1){
			AppSet.Bg("EventApply","Good");
			return false; 	
		}
			var jsonarr = {'action':"setEventsEntriesPraise",ReqContent:JSON.stringify({"common":Base.All(),"special":{'entriesId':entriesId}})};
				$.ajax({
				type:'get',
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
						var o = getEventsEntriesCommentList;
					}
					Win.Loading("CLOSE");
					if(o.code == "200"){
						
							alert(o.description);
							$(".PhotoCommentZan ").text((parseInt($(".PhotoCommentZan").text())+1));
						
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });

	},
		getEventsEntriesWinners:function(){
			
			var strDate = getParam("strDate");
			var jsonarr = {'action':"getEventsEntriesWinners",ReqContent:JSON.stringify({"common":Base.All(),"special":{'strDate':strDate}})};
				$.ajax({
				type:'get',
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
						var o = getEventsEntriesWinners;
					}
					Win.Loading("CLOSE");
					if(o.code == "200"){
						var workDarenList =o.content.workDarenList; 
						
						if(workDarenList.length > 0 ){
							var Istring = ''; 
							for(var i=0; i< workDarenList.length; i++){
									var GetzuopNum ='',username00 = ""; 
									username00 = workDarenList[i].userName; 
									if(workDarenList[i].prizeCount!=1){
											GetzuopNum = "("+workDarenList[i].prizeCount+"个作品)"
										username00  = (username00.length> 8) ? username00.substring(0,8)+'..':username00;
												
										}else{
											username00  = (username00.length> 12) ? username00.substring(0,12)+'..':username00;	
										}
									Istring += '<li style="overflow:hidden;">'+username00+GetzuopNum+'</li>';
							}
							$("#WinnerLoveShow").html(Istring)
						}else{
							$("#WinnerLoveShow").html("<div style='text-align:center; padding-top:10px;'>暂未公布</div")
							}
						var crowdDarentList =o.content.crowdDarentList; 
						if(crowdDarentList.length > 0 ){
							var Istring = ''; 
							for(var i=0; i< crowdDarentList.length; i++){
									Istring += '<div style="padding-top:10px; padding-left:10px; line-height:20px;"><strong>'+crowdDarentList[i].userName+'：</strong>'+crowdDarentList[i].content+'</div>';
							}
							$("#WinnerWeiGuan").html(Istring)
						}else{
							$("#WinnerWeiGuan").html("<div style='text-align:center; padding-top:10px;'>暂未公布</div>");
							}
						$("#WinnerDivId").show();
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });

		},
		getEventsEntriesMonthDaren:function(){
			var getStrDate = 	getParam("strDate");
			var jsonarr = {'action':"getEventsEntriesMonthDaren",ReqContent:JSON.stringify({"common":Base.All(),"special":{'strDate':getStrDate}})};
				$.ajax({
				type:'get',
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
						var o = getEventsEntriesList;
					}
					Win.Loading("CLOSE");
					if(o.code == "200"){
							var ReturnHtml = Tpl.getEventsEntriesMonthDaren(o.content);
							$("#DaRenImgDiv").html(ReturnHtml);
							
							$("#DaRenDivId").show();		
					 }else{
						 	alert(o.description); return false;
						 }
				}
			 });
		}
}