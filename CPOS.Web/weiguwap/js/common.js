// JavaScript Document
var url = 'test.htm'; /*所有异步请求的地址*/
//var url = '../../publicMark'; 
var Timeout = null ;
var GET1s = new Array(), querystr = new Array();
function getParam(){ 
	//获得html上的参数
		querystr = window.location.href.split("?")
		if(querystr[1]){
				var GET1s = querystr[1].split("&")
				for(i=0;i<GET1s.length;i++){
					tmp_arr = [i].split("=")
					key=tmp_arr[0]
					GET1[key] = tmp_arr[1]
					
				}
	
		
		}
		return querystr[1];
	}

var Base = {
		userId:function(){
			var usId =  (localStorage.getItem("userId")  && localStorage.getItem("userId") !='') ? localStorage.getItem("userId") : ''; 
				usId  = (usId == '') ? WeiXin.GetCookie("userId") : usId ;
				return usId;
			},
		codeId:function(){
			var usId =  (localStorage.getItem("codeId")  && localStorage.getItem("codeId") !='') ? localStorage.getItem("codeId") : ''; 
				usId  = (usId == '') ? WeiXin.GetCookie("codeId") : usId ;
				return usId;
			},
		All:function(){				
				return {"codeId":this.codeId(),"userId":this.userId()};
			}
	}
var  Win={
		Loading:function(type){
				if(Timeout){
					clearTimeout(Timeout);	
				}
				$(".TipJp").css("opacity",0);
				if(type == "CLOSE"){
						$(".loading").hide();
						return false; 
					}
				 var getWindowsWidth = $(window).width(), getWindowsHeight = $(window).height(), dst = $(document).scrollTop();
					$(".loading").css({left: (getWindowsWidth - $(".loading").width())/ 2 , top: ((getWindowsHeight - $(".loading").height())/ 2)+dst }); 
					$(".loading").show();
			},
		W: function(){
			return $(window).width(); 
			},
		H:function(){
			return  $(window).height();
			},
		DH:function(){
			return $(document).height();
			},
		ReSizeWin:function(){
			$("#ReserveActionDiv").css({"height":this.H(),"overflow":"hidden"});
			var od = $(".Imgbox").offset();
			var hei = this.H()-od.top;
			$("#myScrol2,#myScrol1").css({"height":hei-10,"overflow":"hidden"})	
			$(".Imgbox,.SelectTimeBox").css({"height":hei,"overflow":"hidden"});
			
			
		}
	}



var AppSet = {
	IsLogin:function(){

				if(localStorage.getItem("userId")&& localStorage.getItem("userId")!=""  &&  localStorage.getItem("codeId")!=""){
					return 1;
				}
				if(WeiXin.GetCookie("userId") && WeiXin.GetCookie("userId")!=null&& WeiXin.GetCookie("codeId")!=""){
					return 1; 
				}
			
			return 0; 
		},
	LoginSubmit:function(actiontype,id){
		var Vip = $.trim($("#Vip").val()), Phone = $.trim($("#Phone").val());
		if(Vip == "会员卡号" || Vip == ""){
				alert('请输入会员卡号！');
				return false;
			}
		if(Phone=="中欧校友密码" || Phone == "" ){
				alert('请输入手机号码!');
				return false; 
			}
		var jsonarr = {'action':"submitAccountBind",ReqContent:JSON.stringify({"common":Base.All(),"special":{'Vip':Vip, 'Phone':Phone}})};
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
					var o = LoginReturnData;
						Win.Loading("CLOSE","1");
						
					if(o.code == 200){
							localStorage.setItem("userId",o.UserId);
							WeiXin.SetCookie("userId", o.UserId, 60*24*360*60,'/');	
							localStorage.setItem("codeId",o.UserCode);
							WeiXin.SetCookie("codeId", o.UserCode, 60*24*360*60,'/');
							if(actiontype=="order"){
								Reserve.TimeLi(id,'',actiontype)	
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

var Reserve = {
		NowReserve:function(){
			$("#ReserveActionDiv").animate({"left":Win.W()},500);
			var jsonarr = {'action':"ServiceItemsData",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
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
					var o = ServiceItemsData;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
						var HtmlString = HtmlTemplates.ServiceItemsData(o.content,$("#ReSeverSelect_1").attr("cur-id"));
						$("#ReSeverInner_1>dl").html(HtmlString);
						$("#ReserveActionDiv").css("height",Win.H())
						$("#NowReserveDiv").animate({"left":-Win.W()},500,function(){$(this).hide();
						$("#ReserveActionDiv").show().animate({"left":0},500,function(){
							Reserve.FristReserve(1); 
							setTimeout(function(){myScroll.refresh();},20)
							
						});});	
												
					}else{
						alert(o.description);	
					}
				}
				
			})
		},
	ReturnDefault:function(type){
			
			var jsonarr = {'action':"ReservationData",ReqContent:JSON.stringify({"common":Base.All(),"special":{"StatusId":"1"}})};
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
						Win.Loading("CLOSE");
						var o = ReservationData;
						if(o.UserId && o.UserId!=""){
							localStorage.setItem("userId",o.UserId);
							WeiXin.SetCookie("userId", o.UserId, 60*24*360*60,'/');	
							localStorage.setItem("codeId",o.UserCode);
							WeiXin.SetCookie("codeId", o.UserCode, 60*24*360*60,'/');
							var HtmlString = HtmlTemplates.ReservationSData(o.content);
						}else{
							var HtmlString = HtmlTemplates.ReservationSData(o.content,0);	
						}
						
						$("#Reservation").html(HtmlString);
						if(type==1){
							$("#NowReserveDiv").show().animate({"left":0},500);
						
						}else{
						$("#ReserveActionDiv").animate({"left":Win.W()},500,function(){$(this).hide();
						$("#NowReserveDiv").show().animate({"left":0},500);
						});
						}
				}
				
			})
		},
	FristReserve:function(c){
		Win.ReSizeWin();

		if($("#ReSever_"+c).css("display")=="block"){
				$("#ReSever_"+c).hide();
				$(".Icn5").removeClass("animateCss");	
				$(".IcnJtou").hide();
				return;
			}
		if(c==2){
			if($("#ReSeverSelect_1").attr("cur-id") == ""){
				alert("请先选择服务类型");
				return;	
			}
			if(!this.SelectToTime($("#ReSeverSelect_1").attr("cur-id"))){
				return false;
			}
		}
		if(c==1){
			var getCurid = $("#ReSeverSelect_1").attr("cur-id");
			$("#ReSeverInner_1 dd").removeClass("ActiveDateDD");
			$("#ReSeverInner_1 dd").each(function() {
                if(getCurid == $(this).attr("service-data-id")){
						$(this).addClass("ActiveDateDD");
					}
            });	
		}
		
		if($("#ReSeverSelect_"+c).find(".IcnJtou").css("display")=="none"){
		$("#ReSeverSelect_"+c).find(".Icn5").addClass("animateCss");
		$("#ReSeverSelect_"+c).find(".IcnJtou").show();
		$("#ReSeverWarpInner_"+c).slideDown(600);
		$("#ReSever_"+c).show();
		for(var i=1; i<= 2; i++){
				if(i!=c){
					$("#ReSeverSelect_"+i).find(".Icn5").removeClass("animateCss");
					$("#ReSeverSelect_"+i).find(".IcnJtou").hide();
					$("#ReSeverWarpInner_"+i).hide();
					$("#ReSever_"+i).hide();
				}
			}
			var getGRay = $(".PsGray").offset();
			var getH = Win.H() - getGRay.top;
			
					 $(".PsGray").css("height",(getH > 0)? getH : 0);
				
			
		}
	},
	SelectToTime:function(ServiceItemId){
		var ofalse = false; 
			var jsonarr = {'action':"DateItemsData",ReqContent:JSON.stringify({"common":Base.All(),"special":{"ServiceItemId":ServiceItemId}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				async:false,
				beforeSend:function(){
						Win.Loading();	
				},
				dataType : 'json',
				success:function(o){
					
					var o = DateItemsData;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
					
						var HtmlString = HtmlTemplates.DateItemsData(o.content,$("#ReSeverSelect_2").attr("cur-id"));
						$("#ReSeverInner_2>dl").html(HtmlString);
						$("#ReserveActionDiv").css("height",Win.H())
					
							setTimeout(function(){ReserveType.refresh();},20)
						ofalse = true;
					}else{
						alert(o.description);	
					}
				}
				
			})
			return ofalse; 
	},
	HourItemsFun:function(ServiceItemId,DateItemId){
		var ofalse = false; 
			var jsonarr = {'action':"HourItemsData",ReqContent:JSON.stringify({"common":Base.All(),"special":{"ServiceItemId":ServiceItemId,"DateItemId":DateItemId}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				async:false,
				beforeSend:function(){
						Win.Loading();	
				},
				dataType : 'json',
				success:function(o){
					
					var o = HourItemsData;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
					
						var HtmlString = HtmlTemplates.HourItemsData(o.content);
						$("#myScrol1>ul").html(HtmlString);
						if(o.content.DataItems.length > 0){
							Reserve.AnimateFun('myScrol1',0)
						}
						$("#ReserveActionDiv").css("height",Win.H())
						ofalse = true;
						setTimeout(function(){myScrol1.refresh();},20)
					}else{
						alert(o.description);	
					}
				}
				
			})
			return ofalse; 
	},
	CloseSelect:function(){
		$(".IcnJtou").hide();
		$(".Icn5").removeClass("animateCss");
		$(".DateList").hide();	
	},
	AnimateFun:function(WrapId,index){
		if($("#"+WrapId).find("li").length > 0 && index < $("#"+WrapId).find("li").length){

		$("#"+WrapId).find("li").eq(index).animate({"opacity":1},400,function(){
				index++
				Reserve.AnimateFun(WrapId,index)	
			});
		}
	},
	TimeItemsFun:function(ServiceItemId,DateItemId,HourItemId){
	var ofalse = false; 
			var jsonarr = {'action':"TimeItemsData",ReqContent:JSON.stringify({"common":Base.All(),"special":{"ServiceItemId":ServiceItemId,"DateItemId":DateItemId,"HourItemId":HourItemId}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				async:false,
				beforeSend:function(){
						Win.Loading();	
				},
				dataType : 'json',
				success:function(o){
					
					var o = TimeItemsData;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
					
						var HtmlString = HtmlTemplates.TimeItemsData(o.content);
						$("#myScrol2>ul").html(HtmlString);
						if(o.content.DataItems.length > 0){
							Reserve.AnimateFun('myScrol2',0)
						}
						$("#ReserveActionDiv").css("height",Win.H());
						setTimeout(function(){myScrol2.refresh();},20)
						ofalse = true;
					}else{
						alert(o.description);	
					}
				}
				
			})
			return ofalse; 
	}, 
	ActionCanel:function(ReservationId){
		if(confirm("确定取消吗？\n 如果取消,我们将删除这条预定信息.")){
				var jsonarr = {'action':"CacelReturnData",ReqContent:JSON.stringify({"common":Base.All(),"special":{"ReservationId":ReservationId}})};
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
					
					var o = CacelReturnData;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
						alert(o.description);
						$("#HasReserveList_"+ReservationId).remove();
					}else{
						alert(o.description);	
					}
				}
				
			})
		}	
	},
	TimeLi:function(id,e,actionType){
		if(e && e!=""){
		 	e.stopPropagation();
		}
		if(AppSet.IsLogin() > 0 ){
			var ServiceItemId= $("#ReSeverSelect_1").attr("cur-id"),
			DateItemId = $("#ReSeverSelect_2").attr("cur-id"),
			HourItemId = $(".SelectDateActive").attr("hour-data-id"),
			TimeItemId  = $("#timeli_"+id).attr("time-data-id");
			var timeTxt =  $("#timeli_"+id).text();
			var jsonarr = {'action':"SubmitOrder",ReqContent:JSON.stringify({"common":Base.All(),"special":{"ServiceItemId":ServiceItemId,"DateItemId":DateItemId,"HourItemId":HourItemId,"TimeItemId":TimeItemId
			}})};
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
					
					var o = SubmitOrder;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
						var HtmlString = HtmlTemplates.SubmitOrder(o.content,timeTxt,id);
						Pop.AllStyle(HtmlString)
					}else{
						alert(o.description);	
					}
				}
				
			})	
		}else{
			Pop.LoginPop(actionType,id);
		}
	},
	CSubmitOrder:function(id,e){
		var ServiceItemId= $("#ReSeverSelect_1").attr("cur-id"),
			DateItemId = $("#ReSeverSelect_2").attr("cur-id"),
			HourItemId = $(".SelectDateActive").attr("hour-data-id"),
			TimeItemId  = $("#timeli_"+id).attr("time-data-id");
			var timeTxt =  $("#timeli_"+id).text();
			var jsonarr = {'action':"SubmitOrder",ReqContent:JSON.stringify({"common":Base.All(),"special":{"ServiceItemId":ServiceItemId,"DateItemId":DateItemId,"HourItemId":HourItemId,"TimeItemId":TimeItemId
			}})};
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
					
					var o = SubmitOrder;
					Win.Loading("CLOSE");
					if(o.code=="200"){	
					alert(o.description);	
					Pop.Close();
						Reserve.ReturnDefault();
					}else{
						alert(o.description);	
					}
				}
				
			})		
	}
}
var Pop = {
	AllStyle:function(connext,isMask){
		$(".PopDiv").remove();
		var popString = '<div class="PopDiv" ><div class="PopDivClose icn_skin" onclick="Pop.Close()"></div><div class="PopConnext">'+connext+'</div>';
		$("body").append(popString);
		var getPopHeight = $(".PopDiv").height();
		if(getPopHeight > Win.H()){
			$(".PopDiv").css("top","20px");
		}else{
			$(".PopDiv").css("top", (Win.H() - getPopHeight )/2);	
		}
		if(isMask != "2"){
			$(".opacityBg").css({"height":(Win.DH() > getPopHeight)  ? Win.DH() :getPopHeight }).fadeIn(300);
		}
		$(".PopDiv").animate({"opacity":1},500)
	},
	LoginPop:function(actionType,id){
		var HtmlString = HtmlTemplates.LoginTmp(actionType,id);	
		Pop.AllStyle(HtmlString)
	},
	Close:function(){
				$(".PopDiv").remove();
				$(".opacityBg").hide();
		}
}
var JSEvent = {
	 SerViceLi:function(o,e){
		
		 var serviceId = $(o).attr("service-data-id");
		var serviveTxt = $(o).text();
		$(o).addClass("ActiveDateDD").siblings("dd").removeClass("ActiveDateDD")
		$("#ReSeverSelect_1").attr("cur-id",serviceId);
		$("#ReSeverSelect_2>i").text("请选择时间");
		$("#ReSeverSelect_2").attr("cur-id",'');
		$("#myScrol1>ul,#myScrol2>ul").html('');
		$("#ReSeverSelect_1>i").text(serviveTxt);
		 e.stopPropagation();
		Reserve.FristReserve(2);
		
	 },
	DateLi:function(o,e){
		var dateId = $(o).attr("date-data-id");
		var dateTxt = $(o).text();
		 e.stopPropagation();
		if($(this).attr("status")=="1"){
			alert("抱歉，该日期已订满。")
			return;
			}
		$("#myScrol1>ul,#myScrol2>ul").html('');
		$(o).addClass("ActiveDateDD").siblings("dd").removeClass("ActiveDateDD")
		$("#ReSeverSelect_2").attr("cur-id",dateId);
		$("#ReSeverSelect_2>i").text(dateTxt);
		Reserve.CloseSelect();
		Reserve.HourItemsFun($("#ReSeverSelect_1").attr("cur-id"),$("#ReSeverSelect_2").attr("cur-id"));
	},
	HourLi:function(o,e){
		var hourId = $(o).attr("hour-data-id");
		;
		if($(o).attr("status")=="1"){
			alert("抱歉，该时间段已订满。")
			return;
			}
		$(o).siblings("li").removeClass("SelectDateActive").animate({"padding-left":12},200);
		$(o).addClass("SelectDateActive").animate({"padding-left":30},300);
		 e.stopPropagation()
		Reserve.CloseSelect();
		Reserve.TimeItemsFun($("#ReSeverSelect_1").attr("cur-id"),$("#ReSeverSelect_2").attr("cur-id"),$(o).attr("hour-data-id"))
	}
}
$(function(){
	
	$(window).resize(function(){
		Win.ReSizeWin();
	})
	
})