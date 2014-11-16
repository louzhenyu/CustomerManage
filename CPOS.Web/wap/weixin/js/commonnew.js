// JavaScript Document
//var url = 'test.php'; /*所有异步请求的地址*/
var url = '../../publicMark'; 
var Timeout = null ;
var GET1 = new Array(), QueString ='',querystr = new Array(), savebtnScrolltop = 0;
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
	$(window).scroll(function(){
		var geiheight = $(window).height();
			var getscroll =parseInt($(document).scrollTop());
			(getscroll+geiheight > geiheight) ? $("#TopFun").show() : $("#TopFun").hide();
			
	})
	
	$("#TopFun").click(function(){
        $('html, body').animate({scrollTop:0},200)
		});
	if(IsPC()){
		$("#PcBox").css("width",480).show();

		$(".ClassTabJs").css("min-height",500)
		if(IsIEVersion() > 0 ){
			$(".WAppMenu li").eq(2).css("width","32%");
		}
	}else{
		$("#PcBox").show();
		if(navigator.userAgent.indexOf("safari") > 0){
			$(".WAppMenu li").eq(1).css("margin","0 2%");
		}
	}
})

var position_option = {
	                enableHighAccuracy: true,
	                maximumAge: 0,
               	 	timeout: 20000
	            	};

function getParam(){ 
	//获得html上的参数
		querystr = window.location.href.split("?")
		if(querystr[1]){
				var GET1s = querystr[1].split("&")
				for(i=0;i<GET1s.length;i++){
					tmp_arr = GET1s[i].split("=")
					key=tmp_arr[0];
					var ParamValue = tmp_arr[1];
					var odE = ParamValue.split("#");
					if(odE.length > 0 ){
						ParamValue = odE[0];
					}
					GET1[key] = ParamValue;
					
					if(tmp_arr[0]!="openId" && tmp_arr[0] !="page"){
					
							QueString += tmp_arr[0]+"="+ParamValue+"&"
						}
				}
				
				QueString = QueString.substring(0,QueString.length-1);
		}
		return querystr[1];
	}
$(function(){

	getParam();
	
	
	$(window).resize(function(){
		$("#EventDetailId,#YaoJiangDetailId,#HuDongDetailID,#UserSearchId,#GroupDetailId,#GroupPersonId,#GroupHuDongId").css("width","100%");
	})
	$(window).scroll(function(){
		
			var scrolltop = $(document).scrollTop(), DhAjax = $(document).height(), WHajax= $(window).height();
			
			if((scrolltop+WHajax+90) > DhAjax && $("#hiddenIsNext").length > 0 && $("#hiddenIsNext").attr("isLoading")=="0" && $("#hiddenIsNext").val() == "1"){
					Event.GetMoreWinner();
				}
			if((scrolltop+WHajax+90) > DhAjax && ($("#UserSearchResultID").css("display") =="block") && $("#UserSearchResultID").attr("isNext")=="1" && $("#UserSearchResultID").attr("loading") == "0"){
					User.searchResult();
				}
			if((scrolltop+WHajax+90) > DhAjax && $("#GroupHiddenIsNext").length > 0 && $("#GroupHiddenIsNext").attr("isLoading")=="0" && $("#GroupHiddenIsNext").val() == "1"){
					Group.GroupList();
				}
			if((scrolltop+WHajax+90) > DhAjax && $("#PersonGroupHiddenIsNext").length > 0 && $("#PersonGroupHiddenIsNext").attr("isLoading")=="0" && $("#PersonGroupHiddenIsNext").val() == "1" && $("#CommonGID").attr("paidtype")!=""){
					Group.GroupPerson($("#GroupDetailId").attr("groupId"),$("#CommonGID").attr("paidtype"));
				}
			if((scrolltop+WHajax+90) > DhAjax && $("#ComApplyPerson").length > 0 && $("#ComApplyPerson").attr("isLoading")=="0" && $("#CCisNext").val() == "1"){
					Event.EventApplyPerson();
				}
		})
	
	//获得hash已取得页面分流
	if($("#DLoading").length > 0 ){
	var appHash = location.hash;
	appHash = appHash.substring(1,appHash.length);
	
	var AppPageType = appHash.split("/"); 
	//if(appHash == ""){
		//	AppPageType=['Event','Detail'];	
		//}
	switch(AppPageType[0]){
			case "Event": 
					$(".WAppMenu li").removeClass("WAppMenuActive");
					if(AppPageType[1] == "Detail"){
							if(GET1['eventId'] && GET1['eventId']!=""){
										var eventUrlHash = new Array();
										eventUrlHash = GET1['eventId'].split("#");
										
										$("#IsReferLoad").val('Menu_EventDetail');
										Event.Detail(eventUrlHash[0]);
								}else {
										alert("EventID不存在！")
								}
						}else if(AppPageType[1] == "Prize"){
										var eventUrlHash = new Array();
										eventUrlHash = GET1['eventId'].split("#");
										$("#IsReferLoad").val('Menu_Prize')
										Event.PrizeDetail(eventUrlHash[0]);	
						}if(AppPageType[1] == "HuDong"){
								var eventUrlHash = new Array();
								eventUrlHash = GET1['eventId'].split("#");
								$("#IsReferLoad").val('Menu_HuDong')
								Event.HuDongDetail(eventUrlHash[0]);	
							}	
				break;
			case "User":
				$(".WAppMenu li").removeClass("WAppMenuActive");
				if(AppPageType[1] == "Search"){
					//var eventUrlHash = new Array();
					//eventUrlHash = GET1['eventId'].split("#");
					$("#IsReferLoad").val('Menu_UserSearch');
					User.ShowSearch()
				}
				if(AppPageType[1] == "Nearby"){
					//var eventUrlHash = new Array();
					//eventUrlHash = GET1['eventId'].split("#");
					$("#IsReferLoad").val('Menu_Nearby');
					User.NearBy()
				}
			break;
			case "Group":
				$("#CommonList,#CommonGID").css("min-height",$(document).height() - 80)
				if(AppPageType[1] == "List"){
					Group.GroupList("1");
					return;
				}
				$(".WAppMenu li").removeClass("WAppMenuActive");
				if(AppPageType[1] == "Detail"){
					if(GET1['groupId'] && GET1['groupId']!=""){
					var GroupUrlHash = new Array();
					GroupUrlHash = GET1['groupId'].split("#");
					$("#IsReferLoad").val('Menu_GroupDetail');
					Group.GroupDetail(GroupUrlHash[0])
					}else{
						alert("groupId不存在！")	
					}
				}
				if(AppPageType[1] == "Person"){
					if(GET1['groupId'] && GET1['groupId']!=""){
						var GroupUrlHash = new Array();
						GroupUrlHash = GET1['groupId'].split("#");
						$("#IsReferLoad").val('Menu_GroupPerson');
						Group.GroupPerson(GroupUrlHash[0],1,'1')
					}else{
						alert("groupId不存在！")	
					}
				}
				if(AppPageType[1] == "Person0"){
					if(GET1['groupId'] && GET1['groupId']!=""){
						var GroupUrlHash = new Array();
						GroupUrlHash = GET1['groupId'].split("#");
						$("#IsReferLoad").val('Menu_GroupPerson0');
						Group.GroupPerson(GroupUrlHash[0],0,'1')
					}else{
						alert("groupId不存在！")	
					}
				}
			break;
		}
	}
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
		Loading:function(type){
				if(Timeout){
					clearTimeout(Timeout);	
				}
				$("#BottomMenu").hide();
				$(".TipJp").css("opacity",0).hide();
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
		Tip:function(txt){
			$(".TipJp").html(txt);
			var getTipHeight = $(".TipJp").height();
			$(".TipJp").css("bottom",-getTipHeight);
			var Timeout = setTimeout(function(){$(".TipJp").show().animate({bottom:0,opacity:1},500);},1200);
				setTimeout(function(){$(".TipJp").animate({"opacity":0,"bottom":-getTipHeight},500);},4000,function(){$(this).hide()});
		},
		AniMateWidth:function(){
			return $("#CommonGID").width();	
		}
	}

var Event = { //活动
		Detail:function(eventId,Curid){
			//活动详情
			$("#FollowStyle").hide();
			if(!eventId){
					alert('活动不存在！')
					return;
				}
				$("#Menu_EventDetail").parent().addClass("WAppMenuActive");
			var jsonarr = {'action':"getEventDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":eventId}})};
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
				
					if(deBug == "1"){
						var o = DetailData;
					}
				
					$("#FollowStyle").show();
					Win.Loading("CLOSE");
					if(o.code == 200){
							
							$("#EventDetailId").attr("eventId", eventId);
							$("#EventDetailId").css("left",Win.AniMateWidth());
							if(window.localStorage){
							localStorage.setItem("isCheckByBind",o.content.isCheckByBind);
							localStorage.setItem("hasUserAccount",o.content.hasUserAccount);
							}
								WeiXin.SetCookie("isCheckByBind", o.content.isCheckByBind, 60*24*360*60,'/');
									WeiXin.SetCookie("hasUserAccount", o.content.hasUserAccount, 60*24*360*60,'/');
							
							var htmldata = Templates.EventDetailTemp(o.content);	
							$("#EventDetailId").show().html(htmldata);
							if(IsIEVersion()==0){
							$("#HeaderTitle").text("『"+o.content.timeStr+"』"+o.content.title);
							}
							$("#EventDetailId").animate({"left":0, "width":Win.AniMateWidth()},800,function(){	$("#IsMenuClick").val("1");});
									
							//if($("#CheckInSuccess").length > 0 ){
								//	var poffset = $("#CheckInSuccess").offset();
								//	savebtnScrolltop = poffset.top;
								
									if($(".CheckInSuccess").eq(0).attr("apptype") == $("#EventAction").val()){
										switch($(".CheckInSuccess").eq(0).attr("apptype")){
												case "Apply":
													AppSet.Bg('EventApply','getEventApplyQues');
												break;
												case "CheckIn":
													Event.AppCheckIn.getPosition();
												break;
												default:;
											}
									}
								//}
							
							if(o.content.hasPrize == "1"){
								Win.Tip('本活动诚邀校友企业赞助现场奖品，相关事宜联系活动联系人');
							}
						}else{
							alert(o.description);
						}
						
				}
			 });
			},
		CheckHasBind:function(openId, localLink,type){
				var jsonarr = {'action':"getAccountBind",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
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
					if(deBug == "1"){
						
						var o = Return_getAccountBind;
					}
						
						Win.Loading("CLOSE");	
					if(o.code == 200){
					//	if(o.content.isBindSucceed == "1"){
						if(type == "1" && o.content.hasBindWeixin =="1"){
									$("#BindUsername").html(o.content.userName);
									$('#AlumniConnextDD').hide();
									$('#AlumniConnextDD2').show();
									if(window.localStorage){
									localStorage.setItem("userName",o.content.userName);
									localStorage.setItem("userId",o.content.userId);
										localStorage.setItem("openId",((openId =="undefined") ? "" : openId));
									}
									WeiXin.SetCookie("userId", o.content.userId, 60*24*360*60,'/');
									WeiXin.SetCookie("openId", o.content.openId, 60*24*360*60,'/');
								
									
									return ;
									
							}else{
									WeiXin.SetCookie("openId", o.content.openId, 60*24*360,'/');
									if(window.localStorage){
										localStorage.setItem("openId",((openId =="undefined") ? "" : openId));
										localStorage.setItem("userName",'');
										localStorage.setItem("userId",'');
									}
									WeiXin.SetCookie("userId", '', 60*24*360*60,'/');
									
									$('#AlumniConnextDD2').hide();
									$('#AlumniConnextDD').show();
									return ; 
								}
						
						}else{
							alert(o.description);
					}
				}
			 });
		},
	ActionApply:function(){
			if(AppSet.IsLogin() > 0){
					AppSet.Bg('EventApply','getEventApplyQues');
				}else{
					AppSet.Bg('AlumniConnext1','Apply');	
			}
		},
	ShowApply:function(){
			var jsonarr = {'action':"getEventApplyQues",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":$("#EventDetailId").attr("eventId")}})};
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
					if(deBug == "1"){
					var o = ApplyData;
					}
					if(o.code == 200){
							Win.Loading("CLOSE");
							var htmldata = Templates.EventDetailApply(o.content);	
							$("#ApplyBmID").html(htmldata);	
							AppSet.Bg('EventApply');		
						}else{
							alert(o.description);
						}
				}
			 });	
		},
	ApplySubmit:function(){
			var username = $.trim($("#ApplyName").val());
			var moblie = $.trim($("#ApplyMobile").val()); 
			var  email = $.trim($("#ApplyEmail").val());
			var NewsAnswerData = {};
			if(username == ""){
				alert("请输入名字!");
				return; 
			}
			if(moblie == ""){
				alert("请输入手机号码!");
				return;
				}
			if(!IsEmail(email)){
				alert("请正确输入邮箱的格式！");
				return;
				}
	
			if(this.GetApplyAnswer("AllQuestionId")!=false){
				if($("#AllQuestionId").find("div[id^='QuestionAbc_']").length <=0){
				
						NewsAnswerData.questions =[];
				}else{
						var oAnswerData = this.GetApplyAnswer("AllQuestionId");
					
						 NewsAnswerData.questions = oAnswerData;
					}
				}else{
					return false;	
				}
			var jsonarr = {'action':"submitEventApply",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":$("#EventDetailId").attr("eventId"), userName:username, mobile:moblie, email:email, className:$("#ApplyClass").val(),questionnaireResult:NewsAnswerData}})};
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
					if(deBug == "1"){
						var o = ApplyData;
					}
					if(o.code == 200){
							AppSet.Close("EventApply");
							Win.Loading("CLOSE");
							alert("报名成功！");
							Event.Detail($("#EventDetailId").attr("eventId"));
						}else{
							alert(o.description);
					}
				}
			 });	
		},
	GetApplyAnswer:function(id){
		// id 问题外面包装的Id
		
			//先检查必选项是否填写数据
			var $saves1 = $("#"+id), lf = true;
			if($saves1.find("div[id^='QuestionAbc_']").length <=0){
					return true;
				}
		$saves1.find("div[id^='QuestionAbc_']").each(function(){
			if($(this).attr("isrequired")=="1"){
				var getVt = $(this).attr("value"), mk = false;
					$(this).find("[id^='qt_"+getVt+"']").each(function(){
						/*checkbox and radio start*/
							if($(this).attr("type") != "text"){
									if($(this).attr("checked") == true || $(this).attr("checked") == "checked"){
											mk = true;	
										}
							}else{
							
									var  gv = $.trim($(this).val());	
									if(gv == ""){
											lf = false;
										}else{
											mk= true;	
										}
							}
						/*checkbox and radio end*/
						})
					
						if(mk == false){	
								lf = false;
						}
			}
		})	
		
	if(lf == false){
			alert("请填写必填项！")
			return false; 
		}else{
			var dataArr = [], lfc = true; 
			
			$saves1.find("div[id^='QuestionAbc_']").each(function(i){
				var getVt = $(this).attr("value"), sObj = {};	
				sObj.questionId  = getVt;
			//	sObj.type  = $(this).attr("type");
				sObj.questionValue ='';
				//sObj.other ='';
				var answerid = '', other = '';
				$(this).find("[id^='qt_"+getVt+"']").each(function(){
						
						if($(this).attr("type") != "text"){
								if($(this).attr("checked") == true || $(this).attr("checked") == "checked"){
									answerid += $(this).val()+",";
									
								}
							}else{
								answerid = $.trim($(this).val())+',';
							}	
					})
				if(answerid!=""){
						answerid  = answerid.substring(0,answerid.length -1);
						sObj.questionValue = answerid;
					}
				
				dataArr.push(sObj);
			})	
	//	return 	JSON.stringify(dataArr);
			if(lfc== false ){
				return false; 
				}else{
				return dataArr;
				}
			}
		},
	AppCheckIn:{
			getPosition:function(type){
				if(type!="pritze"){
						if((window.localStorage && localStorage.isCheckByBind  == "1") || WeiXin.GetCookie("isCheckByBind")=="1" ){
								if((window.localStorage &&localStorage.hasUserAccount =="0")|| WeiXin.GetCookie("hasUserAccount")=="0" ){
						 	  		AppSet.Bg('AlumniConnext1','CheckIn');	
									return;
								}
							}
					}
			navigator.geolocation.getCurrentPosition(function(e){Event.AppCheckIn.getPositionSuccess(e,type)}, Event.AppCheckIn.getPositionError, position_option);
			
				},
			getPositionSuccess:function(position,type){
					var latitude = position.coords.latitude;
			        var longitude = position.coords.longitude;
					/*if(typeof position.address !== "undefined"){
               	 	var country = position.address.country;
               	 	var province = position.address.region;
                	var city = position.address.city;
	        		}*/
				if(type=="pritze"){
					var jsonarr = {'action':"submitEventPrize",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":$("#EventDetailId").attr("eventId"), longitude:longitude,latitude:latitude }})};
					}else{
					var jsonarr = {'action':"submitEventCheckin",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":$("#EventDetailId").attr("eventId"), longitude:longitude,latitude:latitude }})};
					}
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
							//test data
							
							if(deBug == "1"){
							if(type!="pritze"){
								var o = ApplyData;
							}else{
								var o = PriteListData;
								}
							}
							//test data
								Win.Loading("CLOSE");
							if(o.code == 200){
									AppSet.Close("EventApply");
								
								
									if(type=="pritze"){
												if(o.content.prizeStatus == 1){
													 alert("恭喜您，抽中了。请查看我的奖品！");
													 Event.PrizeDetail($("#EventDetailId").attr("eventid"),'');
													}else{
													 alert("很可惜，您这次没有中奖!");
												}
												
											}else{
													alert(o.description);
											if(o.description=="操作成功"){
													$(".CheckInSuccess").hide();
													$(".CheckInSuccessBtn").show();
											}
										}
								}else{
									alert(o.description);
							}
						}
					 });	
				},
			getPositionError:function(error){
					switch (error.code) {
	       					 case error.TIMEOUT:
							alert("连接超时，请重试");
								break;
							case error.PERMISSION_DENIED:
							alert("您拒绝了使用位置共享服务，无法操作");
							break;	
							case error.POSITION_UNAVAILABLE:
							alert("获取位置信息失败,无法操作");
							break;
				}
				}
	}, 
	PrizeDetail:function(eventId,type,Curid){
		$("#FollowStyle").hide();
			if(!type){type ='';}
			if(!eventId){
			var eventId = $("#EventDetailId").attr("eventId") ? $("#EventDetailId").attr("eventId") : ''; }
			if(eventId==""){
					alert('活动不存在！')
					return;
				}
				$("#Menu_Prize").parent().addClass("WAppMenuActive");
			var jsonarr = {'action':"getEventPrizeInfo",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":eventId,"type":type}})};
			var saveScrolltop = $(document).scrollTop();
			$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					if(type==""){
							Win.Loading();
					}else{
					saveScrolltop = $(document).scrollTop();
					$(".prizediv_jp").prepend('<div align=center style="padding:14px; "><img width="30" height="30" src="images/382.gif"></div>');	
						}
				},
				dataType : 'json',
				success:function(o){
					/*test data*/
					
					if(deBug == "1"){
					var randdata=[PriteListData,PriteListData1,PriteListData2];
					if(type!=""){
					for(var i=0; i<randdata.length; i++){
						
						if(type == i){
							var o = randdata[i];
						}
					}
					}else{
							var o = randdata[Math.floor(Math.random()*3)];	
						}
					}
					/*test end*/
					if(o.code == 200){
							Win.Loading("CLOSE");
							$("#EventDetailId").attr("eventId", eventId);
							if(type==""){
								$("#YaoJiangDetailId").css("left",Win.AniMateWidth());
								if(window.localStorage){
									localStorage.hasUserAccount = o.content.hasUserAccount; 
								}
									WeiXin.SetCookie("hasUserAccount",o.content.hasUserAccount,24*60*60*360,'/');
								var htmldata = Templates.EventPrizeTemp(o.content);	
								$("#YaoJiangDetailId").html(htmldata).show();
								$("#YaoJiangDetailId").animate({"left":0, "width":Win.AniMateWidth()},800,function(){$("#IsMenuClick").val("1");});
							}else{
									var htmldata = Templates.EventPrizeTemp(o.content,1);
								
									$(".prizediv_jp").html(htmldata).show();
										$(document).scrollTop(saveScrolltop)
								}
						}else{
							alert(o.description);
							}
					
				}
			 });
		},
		GetMoreWinner:function(){
			$("#loadMoreIcn").remove();
			$("#hiddenIsNext").attr("isLoading","1");
			var jsonarr = {'action':"getEventWinners",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":$("#EventDetailId").attr("eventid"),"type":2, timeStamp:$("#hiddenIsNext").attr("timestamp")}})};
			var saveScrolltop = $(document).scrollTop();
			$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					$(".prizediv_jp").append('<div align=center style="padding:14px;" id="loadMoreIcn"><img width="30" height="30" src="images/382.gif"></div>');	
					
				},
				dataType : 'json',
				success:function(o){
					/*test data*/
					if(deBug == "1"){
							var o = PriteListData2;
					}
					/*test end*/
					$("#loadMoreIcn").remove();
					$("#hiddenIsNext").attr("isLoading","0");
					if(o.code == 200){
						$("#hiddenIsNext").remove();
							var htmldata = Templates.EventPrizeTemp(o.content,1);
							$(".prizediv_jp").append(htmldata);
						}
				}
			 });	
			},
		HuDongDetail:function(eventId,Curid){
			
				if(!eventId){
					alert('活动不存在！')
					return;
				}
				$("#FollowStyle").hide();
				$("#Menu_HuDong").parent().addClass("WAppMenuActive");
			var jsonarr = {'action':"getEventDiscussx",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":eventId}})};
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
						//deBug = deBug.substring(0,1);
					//if(deBug == "1"){
					var o = HuDongData;
					//}
					if(o.code == 200){
							Win.Loading("CLOSE");
							$("#EventDetailId").attr("eventId", eventId);
							$("#HuDongDetailID").css("left",Win.AniMateWidth());
							if(window.localStorage){
					//		localStorage.hasUserAccount = o.content.hasUserAccount; 
							}
				//			WeiXin.SetCookie("hasUserAccount",o.content.hasUserAccount,24*60*60*360,'/');
							var htmldata = Templates.EventInteractiveTemp(o.content);	
							$("#HuDongDetailID").html(htmldata).show();
							$("#HuDongDetailID").animate({"left":0, "width":Win.AniMateWidth()},800,function(){$("#IsMenuClick").val("1");});
							
						}else{
							alert(o.description); 	
						}
						
				}
			 });
			},
		ActionEventApplyPerson:function(eventId,personNum){
			if(personNum == "0"){
				alert("该活动暂无报名人员！"); 
				return; 
			}
			location.href="EventApplyPerson.html?eventId="+eventId;
			
		},
		EventApplyPerson:function(){
			
			var eventId = GET1["eventId"] ?  GET1["eventId"] : "";
				if(eventId ==""){
					alert("活动Id不能为空");
					return; 	
				}
				$("#ComApplyPerson").attr("isLoading","1");
				var jsonarr = {'action':"getRegisteredMembers",ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":eventId,"page":$("#CCpage").val(), "page_size":12}})};
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
				//	var o = GroupPersonData;
						Win.Loading("CLOSE");
						$("#ComApplyPerson").attr("isLoading","0");
						$("#BackEventDetail").html('<a style="color:#fff;" href="base.html?eventId='+eventId+'#Event/Detail"><<返回</a>');
					if(o.code == 200){
							
							var htmldata = Templates.EventApplyPerson(o.content);	
							$("#CCisNext").val(o.content.isNext);
							if($("#CCpage").val() =="1"){
								$("#ComApplyPerson").html(htmldata).show();
							}else{
								$("#ComApplyPerson").append(htmldata);
								}
							$("#CCpage").val((parseInt($("#CCpage").val())+1));
						}else{
							alert(o.description); 	
						}
				}
		
			})
		},
		pay:function(){
			var eventId  = GET1['eventId'] ? GET1['eventId'] :"";
			if(eventId==""){
				alert("该活动数据出错！");
				return; 
			}
			if(Base.userId() ==""){
				alert("请登陆");
				AppSet.Bg('AlumniConnext1','');	
				return;	
			}
			location.href = '/ceibs/event?action=order_by_online_payment&u='+Base.userId()+'&i='+eventId+'&t=2';
		}

};

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
			
			if(Action == "getEventApplyQues"){
					Event.ShowApply();
				}
			if(Action == "CheckIn"){
					//return ;
				}
			if(Action == "getToJoinInfo"){
					Group.ShowToJoin();
				}
			if(Action && Action!=""){
					$("#EventAction").val(Action);
				}
			
		},
	IsLogin:function(){
			//if(window.localStorage &&  localStorage.openId && localStorage.openId !="" && localStorage.userId && localStorage.userId!="" ){
					// 返回2 说明有USEID 并且带有OPENId
					//return 2;
				//}
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
			//	var o = Return_login;
					
						Win.Loading("CLOSE","1");
						
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
								switch($("#EventAction").val()){
										case 'Apply': Event.Detail($("#EventDetailId").attr("eventid"));
										break;
										case 'CheckIn': Event.Detail($("#EventDetailId").attr("eventid"));
										break;
										case 'Prize': Event.PrizeDetail($("#EventDetailId").attr("eventid"));
										break;
										case 'User/Search': User.ShowSearch();
										break;
										case 'ToJoin': Group.GroupDetail($("#GroupDetailId").attr("groupId"));
										break;
										case 'GroupList':Group.GroupList();
										break;
										default : location.href = document.URL+LinkDesString;
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
function MenuAppTab(Curid,index,hash){
	
	if($("#IsReferLoad").val() == Curid){
			return ;
		}
	
	if($("#IsMenuClick").val() == "0"){
			return false; 
		}
	$("#IsMenuClick").val("0");
	$("#FollowStyle").hide();
	$("#IsReferLoad").val(Curid)
	$("#"+Curid).parent().siblings("li").removeClass("WAppMenuActive");
	$("#"+Curid).parent().addClass("WAppMenuActive");
	
	$(".ClassTabJs").each(function(index){
		if($(this).css("left") == "0px"){
					$(this).animate({"left":-Win.AniMateWidth()},500,function(){$(this).hide().html('');
					$("#FollowStyle").show();
						if(Curid == "Menu_EventDetail"){
							Event.Detail($("#EventDetailId").attr("eventid"),Curid);
						}
					
					if(Curid == "Menu_Prize"){
							Event.PrizeDetail($("#EventDetailId").attr("eventid"),'',Curid);
						}
					if(Curid == "Menu_HuDong"){
							Event.HuDongDetail($("#EventDetailId").attr("eventid"),Curid);
						}
					if(Curid == "Menu_UserSearch"){
							User.ShowSearch();
						}
					if(Curid == "Menu_Nearby"){
							User.NearBy();
						}
					if(Curid == "Menu_GroupPerson"){
							Group.GroupPerson($("#GroupDetailId").attr("groupId"),1,"1")
						}
					if(Curid == "Menu_GroupPerson0"){
							Group.GroupPerson($("#GroupDetailId").attr("groupId"),0,"1")
						}
					if(Curid == "Menu_GroupDetail"){
							Group.GroupDetail($("#GroupDetailId").attr("groupId"))
						}

					});	
			}	
	})

	
	
	location.hash = hash;
}
function PrizeTabFun(type){
	$("a[id^=prizeMenuApc_]").removeClass("cHover");
	$("#prizeMenuApc_"+type).addClass("cHover");
		Event.PrizeDetail($("#EventDetailId").attr("eventid"),type);
	}
function ShopDetailAjax(){
		var jsonarr = {'action':"getShopDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"serviceId":GET1['serviceId']}})};
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

					//	var o = ShopDetailData;
						Win.Loading("CLOSE");
						if(o.code == 200){
								var dataShop = o.content;
								$("#WeiXinDetailH2").html(dataShop.name);
								$("#WeiXinDetailImage").attr("src",dataShop.imageUrl);
								$("#price").html(dataShop.price);
								$(".priceNow").html(dataShop.priceNow);
								$("#padress").html(dataShop.address);
								$("#pphone").html(dataShop.telephone);
								$("#pdescription").html(dataShop.description);
								$("#shopDetail").show();
							}
					}
				})
}
function GoTopay(){
    location.href = "http://115.29.186.161/Trade.aspx?open_id=" + Base.openId() + "&prod_name=体验劵&prod_price=1";
}
function BtnFunction(id){
	//var $saveId = $("#"+id);
	//var poffset = $("#"+id).offset();
	var dScrollTop = $(document).scrollTop();
//	console.log(dScrollTop+"--"+poffset.top);
	//console.log(dScrollTop >= poffset.top-38);

	if(dScrollTop >= savebtnScrolltop -43){
		if(!$("#"+id).hasClass("btnStylePosition")){
				$("#"+id).addClass("btnStylePosition");
		}
		}else{
			$("#"+id).removeClass("btnStylePosition");
		}
}

var User = {}
User = {
		ShowSearch:function(){
			if(AppSet.IsLogin() == 0){
					AppSet.Bg("AlumniConnext1","User/Search");
					$("#CloseHasBind").hide();
					return;
				}
				$(".ClassTabJs").hide(); 
				$("#SearchPage").val('0');
			$("#Menu_UserSearch").parent().addClass("WAppMenuActive");
			var jsonarr = {'action':"getCeibsIndustry",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
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
					$("#HeaderTitle").html("校友查询");
					//var o = industryListData;
						Win.Loading("CLOSE");
						if(o.code == "200"){
							$("#UserSearchId").css("left",Win.W());
							var htmldata = Templates.UserSearch(o.content);	
							$("#UserSearchId").html(htmldata).show();
							$("#UserSearchId").animate({"left":0, "width":Win.W()},800);
							}
					}
				})
		},
		searchResult:function(type){
			if(AppSet.IsLogin() == 0){
					AppSet.Bg("AlumniConnext1","User/Search");
					$("#CloseHasBind").hide();
					return;
				}
			var userName = $.trim($("#SearchUsername").val()), industryId = $.trim($("#SearchIndustry").val()),
			company = $.trim($("#SearchCompany").val()), address = $.trim($("#SearchAdress").val());
			if(userName=="姓名"){userName=""}
			if(company=="公司"){company=""}
			if(address=="地址"){address=""}
			if(userName == "" && industryId=="" && company =="" && address=="" ){
				alert("至少输入一项内容");
				return; 
				}
		
			var page = parseInt($("#SearchPage").val()) + 1;
			$("#IsReferLoad").val('') 
			var jsonarr = {'action':"queryUsers",ReqContent:JSON.stringify({"common":Base.All(),"special":{"userName":userName,"company":company,"address":address,"industryId":industryId,"page":page}})};
			$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						$("#UserSearchResultID").attr("loading","1");
						if(type =="1"){Win.Loading();}else{
						$("#UserSearchResultID").append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');	
						}
				},
				dataType : 'json',
				success:function(o){
				
				//var o = SearchResultData; 
							Win.Loading("CLOSE");
							$(".cLoading").remove();
						if(o.code == "200"){
							var userlength = o.content.users;
							if(type =="1"){
								$("#UserSearchResultID").css("left",Win.W());
							}
							$("#SearchPage").val(page);
							$("#UserSearchResultID").attr("isNext",o.content.inNext)
							if(userlength.length == 0 ){
									alert("没有搜索到数据，请重试！"); 
									return; 
								}
							
							$("#UserSearchResultID").attr("loading","0");
							var htmldata = Templates.UserSearchResult(o.content,type);	
							$("#Menu_UserSearch").parent().removeClass("WAppMenuActive");
								if(type =="1"){$(".ClassTabJs").hide(); $("#UserSearchResultID").html(htmldata).show();}else{
									$("#UserSearchResultID").append(htmldata);
									
								}
								$("#UserSearchResultID").animate({"left":0, "width":Win.W()},800);
							}else{
								alert(o.description); 		
							}
					}
				})
		},
		NearBy:function(){
			if(AppSet.IsLogin() == 0){
					AppSet.Bg("AlumniConnext1","User/Nearby");
					$("#CloseHasBind").hide();
					return;
				}
				this.GetLatLin();
			},
		GetLatLin:function(){
			 navigator.geolocation.getCurrentPosition(function(e){User.getPositionSuccess(e)}, Event.AppCheckIn.getPositionError, position_option);	
			},
		getPositionSuccess:function(position){
			var latitude = position.coords.latitude;
			var longitude = position.coords.longitude;
			$("#longitude").val(longitude);
			$("#latitude").val(latitude);
			this.RoadNearBy("1")
		//	return {latitude:latitude,longitude:longitude};
			},
		RoadNearBy:function(type){
			$("#Menu_Nearby").parent().addClass("WAppMenuActive");
			var jsonarr = {'action':"queryNearbyUsers",ReqContent:JSON.stringify({"common":Base.All(),"special":{"longitude":$("#longitude").val(),"latitude":$("#latitude").val(),"distanceScope":"0","timeScope":"0","sortType":"1"}})};
			$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					//	$("#UserNearbyId").attr("loading","1");
						if(type =="1"){Win.Loading();}else{
						$("#UserNearbyId").append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');	
						}
				},
				dataType : 'json',
				success:function(o){
					//var o = SearchNearbyData; 
							Win.Loading("CLOSE");
							$(".cLoading").remove();
						if(o.code == "200"){
							if(type =="1"){
								$("#UserNearbyId").css("left",Win.W());
							}
							var userlength = o.content.users;
						//	$("#UserNearbyId").attr("page",page);
						//	$("#UserNearbyId").attr("isNext",o.content.inNext)
							if(userlength.length == 0 ){
									$("#UserNearbyId").html("<div align=center>没有搜索到数据，请重试！</div>"); 
									return; 
								}
							
							//$("#UserNearbyId").attr("loading","0");
							var htmldata = Templates.UserNearbyResult(o.content,type);	
								
								if(type =="1"){$(".ClassTabJs").hide(); $("#UserNearbyId").html(htmldata).show();}else{
									$("#UserNearbyId").append(htmldata);
								}
							$("#UserNearbyId").animate({"left":0, "width":Win.W()},800);
							}
					}
				})
			}
}

var Group ={
		GroupList:function(isfirst){

				if(AppSet.IsLogin() > 0){
						var keyWord = $.trim($("#SearchGroup").val());
						if(keyWord =="搜索协会关键字"){
							keyWord ="";	
						}
						var groupType=GET1['grouptype'] ? GET1['grouptype']:"1";
						if(groupType=="2"){
							$("#GroupTypeId").text('分会');
						}else{
							$("#GroupTypeId").text('协会与俱乐部');	
						}
						if(isfirst=="1"){
							$("#CommonList").attr("page","1");
						}
						$("#GroupHiddenIsNext").attr("isLoading","1");	
						var jsonarr = {'action':"getGroups",ReqContent:JSON.stringify({"common":Base.All(),"special":{"Keyword":keyWord,"queryType":"1","sortType":"1","pageSize":20,"page":$("#CommonList").attr("page"),"groupType":groupType}})};
					$.ajax({
							type:'get',
							url:url,
							data:jsonarr,
							timeout:90000,
							cache:false,
							beforeSend:function(){
								if(isfirst == "1"){
									Win.Loading();	
								}else{
									$("#CommonList").append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');		
								}
							},
							dataType : 'json',
							success:function(o){
							//	var o = GroupListData;
									$(".cLoading").remove();
									Win.Loading("CLOSE");
									$("#GroupHiddenIsNext").attr("isLoading","0");	
								if(o.code == 200){
									
									var htmldata = Templates.GroupListTemp(o.content);
									$("#CommonList").attr("page",(parseInt($("#CommonList").attr("page"))+1));
									$("#GroupHiddenIsNext").val(o.content.isNext);
										if(isfirst == "1"){
												$("#CommonList").html(htmldata);
											}else{
												$("#CommonList").append(htmldata);	
											}
										
									}else{
										alert(o.description);
									}
									
							}
						 });
					}else{
						//先登陆
						AppSet.Bg('AlumniConnext1','GroupList');	
				}
			},
		GotoGroupDetail:function(groupId,o){
			if(o){
				$(o).addClass("WAppGounpActive").siblings().removeClass("WAppGounpActive");
			}
			 location.href="groupDetail.html?groupId="+groupId+"#Group/Detail";	
						/* if(window.history && window.history.pushState){
							 				Win.Loading();	
											
							 				 urlc = "groupDetail.html?deBug=1&groupId="+groupId+"#Group/Detail";
							 				$.ajax({
												type:'get',
												url:urlc,
												data:{},
												timeout:90000,
												cache:false,
												beforeSend:function(){
												
												},
												success:function(o){
													
														 state = {  
															title: document.title,  
															url: document.URL,
															data: $("#CommonList").html()
														};  
                                                		window.history.pushState(state, document.title, urlc);
														$("body").html(o);
													}
												})
                                               
                                               
                                          }else{
                                       	
                     }*/
		},
		GroupDetail:function(groupId){
			//协会详情
			
			if(!groupId){
					alert('协会不存在！')
					return;
				}
				$("#FollowStyle").hide();	//http://alumniapp.ceibs.edu:8080/ceibs_test/publicMark?action=getGroupDetail&ReqContent={"common":{"locale":"zh","userId":"238869","openId":""},"special":{"groupId":"356"}}
				$("#CommonGID").attr("paidtype","");
			$("#Menu_GroupDetail").parent().addClass("WAppMenuActive");
			var jsonarr = {'action':"getGroupDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"groupId":groupId}})};
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
				//		var o = GroupDetailData;
					Win.Loading("CLOSE");
					if(o.code == 200){
							$("#GroupDetailId").attr("groupId", groupId);
							$("#GroupDetailId").css("left",Win.AniMateWidth());
							var titlename = o.content.groupName; 
							titlename = ( titlename.length > 9 ) ? (titlename.substring(0,9)+"...") : titlename;
							$("#GroupNameTitle").text(titlename);
							if(window.localStorage){
								localStorage.setItem("hasBindWeixin",o.content.hasBindWeixin);
								localStorage.setItem("userId",o.content.userId);
							}
							WeiXin.SetCookie("hasBindWeixin", o.content.hasBindWeixin, 60*24*360*60,'/');
							WeiXin.SetCookie("userId", o.content.userId, 60*24*360*60,'/');	
							var htmldata = Templates.GroupDetailTemp(o.content);	
							$("#GroupDetailId").html(htmldata).show();
							$("#FollowStyle").show();
							if(IsIEVersion()==0){
							$("#HeaderTitle").text(o.content.groupName);
							}
							$("#GroupDetailId").animate({"left":0, "width":Win.AniMateWidth()},800,function(){	$("#IsMenuClick").val(1)});
							
									if($("#CheckInSuccess").attr("apptype") == $("#EventAction").val()){
										switch($("#CheckInSuccess").attr("apptype")){
												case "ToJoin":
													AppSet.Bg('GroupToJoin','getToJoinInfo');
												break;
												
												default:;
											}
									}
							
							
						}else{
							alert(o.description);
						}
						
				}
			 });
			},
		GroupToJoin:function(){
		
			if(AppSet.IsLogin() > 0){
					AppSet.Bg('GroupToJoin','getToJoinInfo');
				}else{
					AppSet.Bg('AlumniConnext1','ToJoin');	
			}	
		},
		ShowToJoin:function(){
				var jsonarr = {'action':"getGroupApplyQues",ReqContent:JSON.stringify({"common":Base.All(),"special":{"groupId":$("#GroupDetailId").attr("groupId")}})};
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
					
					//var o = ApplyData1;
					Win.Loading("CLOSE");
					if(o.code == 200){
							
							var htmldata = Templates.EventDetailApply(o.content,"1");	
							$("#ApplyBmID").html(htmldata);	
									
						}else{
							alert(o.description);
						}
				}
			 });
			},
		ToJoinSubmit:function(){
					var username = $.trim($("#ApplyName").val());
			var moblie = $.trim($("#ApplyMobile").val()); 
			var  email = $.trim($("#ApplyEmail").val());
			var NewsAnswerData = {};
			if(username == ""){
				alert("请输入名字!");
				return; 
			}
			if(moblie == ""){
				alert("请输入手机号码!");
				return;
				}
			if(!IsEmail(email)){
				alert("请正确输入邮箱的格式！");
				return;
				}
	
			if(Event.GetApplyAnswer("AllQuestionId")!=false){
				if($("#AllQuestionId").find("div[id^='QuestionAbc_']").length <=0){
				
						NewsAnswerData.questions =[];
				}else{
						var oAnswerData = Event.GetApplyAnswer("AllQuestionId");
					
						 NewsAnswerData.questions = oAnswerData;
					}
				}else{
					return false;	
				}
			var jsonarr = {'action':"submitGroupApply",ReqContent:JSON.stringify({"common":Base.All(),"special":{"groupId":$("#GroupDetailId").attr("groupId"), userName:username, mobile:moblie, email:email, className:$("#ApplyClass").val(),questionnaireResult:NewsAnswerData}})};
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
			//	var o = ToJoinReturnData; 
					Win.Loading("CLOSE");
					if(o.code == 200){
							AppSet.Close("GroupToJoin");
				
							alert(o.description);
							Group.GroupDetail($("#GroupDetailId").attr("groupId"));
						}else{
							alert(o.description);
					}
				}
			 });
		},
		GroupPerson:function(groupId,paidtype,isfirst){
			
						if(paidtype ==1){
								var InnerHtmlId = 'GroupPersonId';
							}else{
								var InnerHtmlId = 'GroupHuDongId';
							}
						if(isfirst=="1"){
							$("#CommonGID").attr("page","1");
						}
						$("#CommonGID").attr("paidtype",paidtype);
						var $saveID = $("#"+InnerHtmlId);
						$("#FollowStyle").hide();
						if(paidtype ==1){
								$("#Menu_GroupPerson").parent().addClass("WAppMenuActive");
						}else{
							$("#Menu_GroupPerson0").parent().addClass("WAppMenuActive");
						}
						$("#PersonGroupHiddenIsNext").attr("isLoading","1");	
						var jsonarr = {'action':"getGroupMembers",ReqContent:JSON.stringify({"common":Base.All(),"special":{"groupId":groupId,"hasPaid":paidtype,"page_size":20,"page":$("#CommonGID").attr("page")}})};
					$.ajax({
							type:'get',
							url:url,
							data:jsonarr,
							timeout:90000,
							cache:false,
							beforeSend:function(){
								if(isfirst == "1"){
										Win.Loading();	
								}else{
									$saveID.append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');		
								}
							},
							dataType : 'json',
							success:function(o){
							//	var o = GroupPersonData;
									$(".cLoading").remove();
									$("#GroupDetailId").attr("groupId",groupId);
									$("#PersonGroupHiddenIsNext").attr("isLoading","0");
									if(isfirst == "1"){
										$saveID.css({"left":Win.AniMateWidth(), "width":Win.AniMateWidth()});
									}
								Win.Loading("CLOSE");
								if(o.code == 200){
										
										
									var htmldata = Templates.GroupPersonTemp(o.content);
									if(isfirst == "1"){
										$saveID.animate({"left":0, "width":Win.AniMateWidth()},800,function(){$("#IsMenuClick").val(1)});
									}
									$("#CommonGID").attr("page",(parseInt($("#CommonGID").attr("page"))+1));
									$("#PersonGroupHiddenIsNext").val(o.content.isNext);
										if(isfirst == "1"){
												$saveID.html(htmldata).show();
											}else{
												$saveID.append(htmldata).show();	
											}
										
									}else{
										alert(o.description);
									}
									
							}
						 });
		},
		getComingEventsForGroup:function(){
		
			var groupId= GET1['groupId'] ? GET1['groupId'] :"";
			if(groupId==""){
				alert("groupId不存在");
				return; 	
			}
			var getVd =  GET1['groupName'] ? decodeURIComponent(GET1['groupName']) :"";
			getVd = ( getVd.length> 12 ) ? (getVd.substring(0,12)+"...") : getVd;
			$("#GroupTypeId").html("<a href='groupDetail.html?groupId="+groupId+"#Group/Detail' class='backGroupDetail'><<"+getVd+"</a>")
			var jsonarr = {'action':"getComingEventsForGroup",ReqContent:JSON.stringify({"common":Base.All(),"special":{"groupId":groupId}})};
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
							//	var o = getComingEventsForGroup; 
									Win.Loading("CLOSE");
								if(o.code == 200){
									
									var htmldata = Templates.getComingEventsForGroup(o.content);
									$("#CommonList").html(htmldata);
								}
							}
				});
		},
		submitGroupQuit:function(){
				if(confirm("确定退出吗？")){
				var groupId= GET1['groupId'] ? GET1['groupId'] :"";
				var jsonarr = {'action':"submitGroupQuit",ReqContent:JSON.stringify({"common":Base.All(),"special":{"groupId":groupId}})};
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
							//	var o = getComingEventsForGroup; 
								alert(o.description);
								if(o.code == 200){
									Group.GroupDetail(groupId);
								}
							}
				});
		}
		}
}
function GroupPay(){
		if($("#GroupDetailId").attr("groupId")==""){
				alert("该协会数据出错！");
				return; 
			}
		if(Base.userId() ==""){
			alert("请登陆");
			AppSet.Bg('AlumniConnext1','');	
			return;	
		}
	
		var Murlc = "/ceibs/event?action=order_by_online_payment&t=1&u="+Base.userId()+"&i="+$("#GroupDetailId").attr("groupId")+"&rand="+Math.floor(Math.random()*1000000);
	
		location.href  = Murlc ; 
	}