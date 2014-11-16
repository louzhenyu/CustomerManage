// JavaScript Document
var url = '/Lj/Interface/Data.aspx'; /*所有异步请求的地址*/
//var url = "test.php";
var Timeout = null ;
var page = 1, OrderCount = 0, pageCount = 0 , isloadComplete = true;
var myScroll,
	pullDownEl, pullDownOffset,
	pullUpEl, pullUpOffset,
	generatedCount = 0;

function loaded() {
	pullUpEl = document.getElementById('pullUp');	
	pullUpOffset = pullUpEl.offsetHeight;

	myScroll = new iScroll('ScollDd', {
		useTransition: true,
	
		onRefresh: function () {
			if (pullUpEl.className.match('loading')) {
				pullUpEl.className = '';
			}
		},
		onScrollMove: function () {
				pullUpEl.querySelector('img').style.display = "none"
			 if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
				pullUpEl.className = 'flip';
	
				this.maxScrollY = this.maxScrollY;
			} else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
				pullUpEl.className = '';

				this.maxScrollY = pullUpOffset;
			}
		},
		onScrollEnd: function () {
			if (pullUpEl.className.match('flip')) {
				pullUpEl.className = 'loading';
				pullUpEl.querySelector('.pullUpLabel').innerHTML = "向上拉加载下一页";
				if(isloadComplete == true){				
						VipCard.getOrders("0")
				}
			}
		}
	});
	
	setTimeout(function () { document.getElementById('ScollDd').style.left = '0'; }, 800);
}


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
		openId:function(){
			var openId =  (localStorage.getItem("openId")  && localStorage.getItem("openId") !='') ? localStorage.getItem("openId") : ''; 
				openId  = (openId == '') ? WeiXin.GetCookie("openId") : openId ;
				return openId;
			},
		weiXinId:function(){
			var weiXinId =  (localStorage.getItem("weiXinId")  && localStorage.getItem("weiXinId") !='') ? localStorage.getItem("weiXinId") : ''; 
				weiXinId  = (weiXinId == '') ? WeiXin.GetCookie("weiXinId") : weiXinId ;
				return weiXinId;
			},
		vipCode:function(){
			var weiXinId =  (localStorage.getItem("vipCode")  && localStorage.getItem("vipCode") !='') ? localStorage.getItem("vipCode") : ''; 
				weiXinId  = (weiXinId == '') ? WeiXin.GetCookie("vipCode") : weiXinId ;
				return weiXinId;
			},
		All:function(){
				return {"weiXinId":this.weiXinId(),"openId":this.openId()};
			}
	}
var  Win={
		Loading:function(type){
				if(Timeout){
					clearTimeout(Timeout);	
				}
	
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



function IsEmail(b){var a=/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;if(!a.exec(b)){return false}return true}

var VipCard = {
		IsVipCard:function(isBp,curpage){
			var isVip = 0; 
			var openId = getParam("openId") ? getParam("openId") : Base.openId();
			var weiXinId = getParam("weiXinId") ? getParam("weiXinId") : Base.weiXinId();
			var jsonarr = {'action':"IsVipCard",ReqContent:JSON.stringify({"common":{"openId":openId,"weiXinId":weiXinId},"special":{}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:true,
				async : false,
				beforeSend:function(){
						Win.Loading();	
				},
				dataType : 'json',
				success:function(o){
					//var o= IsVipCard;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){
							localStorage.setItem("weiXinId",weiXinId);
							WeiXin.SetCookie("weiXinId", weiXinId, 60*24*360*60,'/');	
							localStorage.setItem("openId", openId);
							WeiXin.SetCookie("openId", openId, 60*24*360*60,'/');
							if(isBp == "1"){
									if(o.content.isGenerate == "1"){
										isVip = 1; 
									}else{
										if(curpage == "1"){
										$("#DingGou").animate({bottom:-50},400,function(){
												LZEvent.EventAnimate('#BackVipCardTip','#LZWineDetail',"WineDetail");	
											})
										
											
											}else{
											LZEvent.EventAnimate('#BackVipCardTip','#LZEventDiv',"EventDetail6");
										}
									}
								}else{
									if(o.content.isGenerate == "1"){
									
											VipCard.getVipInfo(o.content.isGenerate);
										}else{
											$("#SqVip").fadeIn();
											$("#HasVip").hide();
										}
								}
												
					}else{
						alert(o.description);	
					}
				}
				
			})
			return isVip;
		},
	setVipCard:function(){
			var jsonarr = {'action':"setVipCard",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
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
					//var o= IsVipCard;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){	
						VipCard.getVipInfo();
									
					}else{
						alert(o.description);	
					}
				}
				
			})
		},
	getVipInfo:function(de){
			var jsonarr = {'action':"getVipInfo",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
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
					//var o= getVipInfo;
					Win.Loading("CLOSE");
					
					if(o.code=="200"){	
									localStorage.setItem("vipCode",o.content.vipCode);
									WeiXin.SetCookie("vipCode", o.content.vipCode, 60*24*360*60,'/');
							$("#SpanBg3").animate({top:-140},260,function(){
									$(".SpanBg2,.SpanBg1,.HdSqVip").fadeOut(200);
								$("#SqVip").css({"width":270,"height":180})
								$("#VipCode,#VipCode1").text(o.content.vipCode);
								$("#VipCardJf").text(o.content.validIntegral);
								$("#VipCardDG").text(o.content.orderCount);
								$(this).animate({top:-20,left:0, width:270},300,function(){
												if(de!="1"){
												$(".ConnextVip").hide();
												}
												$("#HasVip").fadeIn(300);		
								});	
						});
									
					}else{
						alert(o.description);	
					}
				}
				
			})
	},
	getIntegrationExchange:function(o){
		if($(o).find(".icn3").hasClass("dddeTran")){
			$("#VipCardMenuTxtJf").slideUp(500)
			$(o).find(".icn3").removeClass("dddeTran").addClass("dddeTranhover");
			return; 
		}		
		
		$(o).find(".icn3").removeClass("dddeTranhover").addClass("dddeTran");
 
 
			var jsonarr = {'action':"getIntegrationExchange",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:url,
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					$("#VipCardMenuTxtJf").show();
					$("#VipCardMenuTxtJfInner").html('<div style="padding:10px; text-align:center;"><img width="30" height="30" src="images/382.gif"></div>');
				},
				dataType : 'json',
				success:function(o){
					//var o= getIntegrationExchange;
					Win.Loading("CLOSE");
					
					if(o.code=="200"){	
							var HtmlString = HtmlTemplates.getIntegrationExchange(o.content);
							$("#VipCardMenuTxtJfInner").html(HtmlString).hide().slideDown();	
					}else{
						alert(o.description);	
					}
				}
				
			})
	},
	SelectFun:function(id){
		var getValue = parseInt($("#SelectID_"+id).val()), defaultFf = parseInt($("#SelectID_"+id).attr("default"));
			var getJF = parseInt($("#Jfenoo").text()), SelectNum = $("#SelectID_"+id).attr("selectnum");
		$("#SelectID_"+id).attr("selectnum",getValue);
		var CountNum = getJF+(defaultFf* SelectNum)-(defaultFf*getValue);
		$("#Jfenoo").text(CountNum);
		$("select[id^=SelectID_]").each(function(){
			if($(this).attr("itemId") != id && $(this).attr("selectnum")=="0"){
					var getDerr = $(this).attr("default"),getNumdd =Math.floor(CountNum/ getDerr);
					var icString = '<option value="0">0</option>';
					for(var j=1; j<=getNumdd; j++){
							icString += '<option value="'+j+'">'+j+'</option>';	
						}
					$(this).html(icString);
				}
			if($(this).attr("itemId") != id && $(this).attr("selectnum")!="0"){
					var getSelectVal1 = $(this).val();
					var getDerr1 = $(this).attr("default");
					CountNum1  =  parseInt($("#Jfenoo").text())  + (getDerr1* getSelectVal1);
					var getNumdd1 =Math.floor(CountNum1/ getDerr1);
					
					var icString = '<option value="0">0</option>';
					
					for(var j=1; j<=getNumdd1; j++){
							if(getSelectVal1 == j ){
									icString += '<option value="'+j+'" selected="selected">'+j+'</option>';	
								}else{
								icString += '<option value="'+j+'">'+j+'</option>';	
							}
						}
					$(this).html(icString);
				}
		});
	},
	
	getOrders:function(isfirst,pagedd){
		var  pageSize = 5; 
				//	console.log(OrderCount+",,"+pageCount+",,"+page);
			
		if(isfirst == "1"){
				OrderCount =0;
				pageCount = 0;
				page = 1;
				$(document).scrollTop(0);
				if(pagedd != "Event"){
					
					Pop.AllStyle('',1,"我的订购")	;
				}
			}else{
			if(pageCount < page ){
					$("#pullUp").find("img").hide();
					$(".pullUpLabel").show().html("已经是最后一页！");
				
					return; 
				}
			
			}
				isloadComplete =false; 
				$("#pullUp").find("img").show();
			var jsonarr = {'action':"getOrders",ReqContent:JSON.stringify({"common":Base.All(),"special":{"page":page,"pageSize":pageSize}})};
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
					//var o= getOrders;	
					isloadComplete =true; 
					if(o.code=="200"){	
							page++; 
							var HtmlString = HtmlTemplates.getOrders(o.content,isfirst,pagedd);
						
						
						if(isfirst == "1"){
							if(pagedd == "Event"){
							
										$("#AppdLConnnext").html(HtmlString);	
								}else{
								 	 $("#AppBoxLConnnext").html(HtmlString);	
							}
							OrderCount = o.content.totalCount;
						
							pageCount = Math.ceil(OrderCount/pageSize)
							
							loaded();
							$("#pullUp").find("img").hide();
						}else{
						
							$("#UListId").append(HtmlString)
						}
							
							setTimeout(function(){myScroll.refresh();},500)
							
					}else{
						alert(o.description);	
					}
				}
				
			})
	
	},
	setExchanges:function(){
			var Items= [];
			var fal = false; 
			var c = 0 ; 
			$("select[id^=SelectID_]").each(function() {
                	if($(this).val() != "0"){
							fal = true; 
							 var ode = {};
							 ode.itemId = $(this).attr("itemid");
							 ode.count = $(this).val();
							 Items.push(ode);
						}
            });
			if(fal  == false ){
				alert("请选择兑换的商品！");
				return; 
				}
			var jsonarr = {'action':"setExchanges",ReqContent:JSON.stringify({"common":Base.All(),"special":{"Items":Items}})};
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
					//var o= IsVipCard;
					Win.Loading("CLOSE");
					alert(o.description);
					if(o.code=="200"){	
						location.href = document.URL;
					}else{
						
					}
				}
				
			})
	}
	
}
/*
* LZEvent 对象
*　 针对　 Event 的 使用的方法集
*/


var LZEvent = {
	getPosition:function(){
		
			Win.Loading();
		if(WeiXin.GetCookie("Longitude") && WeiXin.GetCookie("Longitude")!="" ){
			
			this.getEventPrizes(WeiXin.GetCookie("Longitude"),WeiXin.GetCookie("Latitude"));	
		}else{
			
			navigator.geolocation.getCurrentPosition(function(e){
					 LZEvent.getPositionSuccess(e); 
			}, function(e){
				LZEvent.getPositionError(e);
				
			}, position_option);
		}
		},
	getPositionSuccess:function(position){
			var Latitude = position.coords.latitude;
			 var Longitude = position.coords.longitude;
		
					this.getEventPrizes(Longitude,Latitude);	 
			 
		},
	getPositionError:function(error){
		
					switch (error.code) {
	       					case error.TIMEOUT:
							alert("连接超时，请重试");
							break;
							case error.PERMISSION_DENIED:
							alert("您拒绝了使用位置共享服务，部分功能无法操作");
							break;	
							case error.POSITION_UNAVAILABLE:
							alert("获取位置信息失败");
							break;
					}
				
					LZEvent.getEventPrizes('','');
				
	},
	getEventInfo : function(Longitude,Latitude){
		var getEventID = getParam("EventId") ? getParam("EventId") : "";
			if(getEventID ==""){
				alert("该活动不存在!")
				return;	
			}
		var jsonarr = {'action':'getEventInfo',ReqContent:JSON.stringify({"common":Base.All(),"special":{"EventId":getEventID,"Longitude":Longitude,"Latitude":Latitude}})};
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
					//var o= getEventInfo;
					Win.Loading("CLOSE");
					
					if(o.code=="200"){	
						$("#isHasApply").val(o.content.isHasApply);
						$("#isOverEvent").val(o.content.isOverEvent);
						$("#isSite").val(o.content.isSite);	
						$("#LZEventTitle").text(o.content.title);
						$("#LZEventUBox").fadeIn();	
						$("#CCity").val(o.content.cityId);
						$("#WineList").attr("href","wineList.html?EventId="+getEventID);
						var HtmlString = HtmlTemplates.getEventInfo(o.content);
						$("#LZWineDetailInner").html(HtmlString)
					}else{
						alert(o.description);	
					}
				}
				
			})	
	},
	getSkus : function(){
		var getEventID = getParam("EventId") ? getParam("EventId") : "";
		
		var jsonarr = {'action':'getSkus',ReqContent:JSON.stringify({"common":Base.All(),"special":{"EventId":getEventID}})};
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
					//var o= getSkus;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){	
						
						var HtmlString = HtmlTemplates.getSkus(o.content);
					
						$("#WineListId").html(HtmlString)
					}else{
						alert(o.description);	
					}
				}
				
			})	
	},
	getSkuDetail : function(){
		var getskuId= getParam("skuId") ? getParam("skuId") : "";
		
		var jsonarr = {'action':'getSkuDetail',ReqContent:JSON.stringify({"common":Base.All(),"special":{"skuId":getskuId}})};
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
					//var o= getSkuDetail;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){	
						
						var HtmlString = HtmlTemplates.getSkuDetail(o.content);
					
						$("#LZWineDetailInner").html(HtmlString);
						$("#RenGouJia").text(o.content.purchasePrice+" "+o.content.unit);
						$("#salesPrice").val(o.content.purchasePrice);
						$("#DingGou").show().animate({"bottom":0},400);
						$("#itemId").val(getParam("itemId"));
						if(o.content.imageList !=null && o.content.imageList.length > 0 ){
							loadImGWine()
						}
					}else{
						alert(o.description);	
					}
				}
				
			})	
	},
	EventDetail:function(){
		this.EventAnimate('#LZWineDetailInner','#LZEventUBox',"EventDetail");
		
		
		
		/*$("#LZWineDetailInner").css("left",$("#LZWineDetailInner").width());
		$("#LZEventUBox").animate({left:-($("#LZEventUBox").width())},500,function(){
				$(this).hide();
				$("#LZWineDetailInner").show().animate({"left":0},500);
		})*/
		
	},
	EventAnimate:function(id1,id2,Prev){

		if(Prev == "0"){
			$("#BackPrev").hide();
			$("#BackPrev").parent().css("padding-left","2%");	
		}else{
			$("#BackPrev").show();
			$("#BackPrev").parent().css("padding-left",30);	
		}
		//$(id1).css("left",$(id1).width());
		var oleft = $(id2).width(); 
		
		if($(id2).attr("defaultleft") == "0"){
				oleft  = -oleft; 
				
			}
		$(id2).animate({left:oleft},500,function(){
				$(this).hide();
				$(id1).show().css("opacity",0);
				var oleft1 = $(id1).width(), cLeft = 0 ; 
				
				if($(id1).attr("defaultleft") == "0"){
						oleft1  = -oleft1; 
					}
				
				$(id1).css("left", oleft1); 
				$(id1).animate({"left":0,"opacity":1},500);
				$("#backPrevPosition").val(Prev);
		})	
		$(document).scrollTop(0);
	},
	BackPre:function(){
			var getBack = $("#backPrevPosition").val();
		if(getBack == "1"){
			location.href = "vipEvent.html?EventId="+getParam("EventId");
		}
		if(getBack == "2"){
			location.href = "wineList.html?EventId="+getParam("EventId");
		}
		if(getBack == "EventDetail"){
				this.EventAnimate('#LZEventUBox','#LZWineDetailInner',"0");
		}
		if(getBack == "EventDetail2"){
				this.EventAnimate('#LZEventDiv','#LZApplyDiv',"0");
		}
		if(getBack == "EventDetail3"){
				this.EventAnimate('#LZEventUBox','#AppdLConnnext',"0");
		}
		
		if(getBack == "EventDetail4"){
				this.EventAnimate('#LZEventUBox','#Subscription',"0");
		}
		if(getBack == "EventDetail5"){
				this.EventAnimate('#LZEventDiv','#LZPrizes',"0");
		}
		if(getBack == "EventDetail6"){
				this.EventAnimate('#LZEventDiv','#BackVipCardTip',"0");
		}
	
		if(getBack == "WineDetail"){
				this.EventAnimate('#LZWineDetail','#BackVipCardTip',"2");
				if($("#DingGou").length > 0){
				$("#DingGou").animate({"bottom":0},500)
				}
		}
	},
	LZWineGOSale:function(){
			if(VipCard.IsVipCard("1","1") == 1){
			var HtmlString = HtmlTemplates.setOrderInfo();
				$(document).scrollTop(0);		
				Pop.AllStyle(HtmlString,1,"");
			}
		},
	LZWineSale:function(){
		var jsonarr = {'action':'getSkuProps',ReqContent:JSON.stringify({"common":Base.All(),"special":{"itemId":$("#itemId").val()}})};
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
					//var o= getSkuProps;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){	
						
						var HtmlString = HtmlTemplates.WineType(o.content);
							$("#CddTipID").html(HtmlString);
							$("#ApdpBoxL12,#AdLayer1").show();
								$(document).scrollTop(0);
					}else{
						alert(o.description);	
					}
				}
				
			})	
		
	},
	setOrderInfo:function(){
		var getUserName = $.trim($("#name").val()),	phone = $.trim($("#phone").val()),ADtextarea = $.trim($("#ADtextarea").val()),tableNumber = $.trim($("#tableNumber").val());
		if(getUserName ==""){
			alert("请输入姓名."); 
			return false; 
			}
		
		if(phone==""){
				alert("请输入手机."); 
				return false; 
			}
		if(tableNumber==""){
			alert("请输入桌号.");
			return false; 
		}
		if(ADtextarea.length > 15){
			alert("个性化信息最多只能输入15字");
			return false; 
		}
		if($("#skuId").val() == ""){
			alert("请选择酒的品类");
			return false;	
		}
		var getEventID = getParam("EventId") ? getParam("EventId") : "";
		var jsonarr = {'action':'setOrderInfo',ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":getEventID,"skuId":$("#skuId").val(),"userName":getUserName,"phone":phone,"individuationInfo":ADtextarea,"tableNumber":tableNumber,"salesPrice":$("#priceCID").val()}})};
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
					//var o= IsVipCard;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){
							Pop.Close();
							alert(o.description);	
						
					}else{
						alert(o.description);	
					}
				}
				
			})
	},
	ActionApply:function(){
		if(VipCard.IsVipCard("1") == 1){
		
			$("#ASVipCode").val(Base.vipCode());
			this.EventAnimate('#LZApplyDiv','#LZEventDiv',"EventDetail2");
		}
	},
	setEventSignUp:function(){
			var getUserName = $.trim($("#Cname").val()),	phone = $.trim($("#Cphone").val()),CCity = $.trim($("#CCity").val());
		if(getUserName ==""){
			alert("请输入姓名."); 
			return false; 
			}
		
		if(phone==""){
				alert("请输入手机."); 
				return false; 
			}
		if(CCity==""){
				alert("请输入所在城市."); 
				return false; 
			}
		
		
		var getEventID = getParam("EventId") ? getParam("EventId") : "";
		var jsonarr = {'action':'setEventSignUp',ReqContent:JSON.stringify({"common":Base.All(),"special":{"EventId":getEventID,"UserName":getUserName,"Phone":phone,"City":CCity }})};
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
					//var o= IsVipCard;
					Win.Loading("CLOSE");
			
					if(o.code=="200"){
							
							alert(o.description);	
						LZEvent.EventAnimate('#LZEventDiv','#LZApplyDiv',"0");
					}else{
						alert(o.description);	
					}
				}
				
			})
		
		},
	ActionGetOrders:function(){
		if(VipCard.IsVipCard("1") == 1){
		
			this.EventAnimate('#AppdLConnnext','#LZEventUBox',"EventDetail3");
			VipCard.getOrders("1","Event")
		}
	},
	ActionSubscription:function(){
		this.EventAnimate('#Subscription','#LZEventUBox',"EventDetail4");		
	},
	
	getEventPrizes:function(Longitude,Latitude){
			if(VipCard.IsVipCard("1") != 1){
				return false; 
			}
		var getEventID = getParam("EventId") ? getParam("EventId") : "";
		var jsonarr = {'action':'getEventPrizes',ReqContent:JSON.stringify({"common":Base.All(),"special":{"EventId":getEventID,"Longitude":Longitude,"Latitude":Latitude}})};
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
					//var o= getEventPrizes; 
					Win.Loading("CLOSE");
					if(WeiXin.GetCookie("Longitude")==""&& Longitude!=""){
						WeiXin.SetCookie("Longitude", Longitude, 3600,'/');
						WeiXin.SetCookie("Latitude", Latitude, 3600,'/');
					}
					if(o.code=="200"){
							$("#canvas").text('');
						if(o.content.isLottery =="0"){
							
							if(Longitude  == "" && WeiXin.GetCookie("Longitude")==""){
									alert("无法拿到您的位置，抽奖功能无法使用");
									o.content.winningDesc = "";
								}	
							//	if(WeiXin.GetCookie("Longitude")!=""){
										var Istring ="";
										var mdata = o.content.prizes;
										if(mdata.length > 0 ){
										var iDisplayIndex = [];
										for(var i =0 ; i< mdata.length; i++){
											iDisplayIndex.push(mdata[i].displayIndex);
										}
										
										iDisplayIndex.sort(function(a,b){return a>b?1:-1});
										var onewdata = [];
										for(var j =0; j<iDisplayIndex.length; j++){
											for(var i =0 ; i< mdata.length; i++){
											
												if(mdata[i].displayIndex ==iDisplayIndex[j]){
													onewdata.push(mdata[i]);	
												}	
											}
										}
								
										for(var i =0 ; i< mdata.length; i++){
												Istring += onewdata[i].prizeName+"："+onewdata[i].prizeDesc+"("+onewdata[i].countTotal+")<br />";
											}
										}else{
												Istring  += "暂无奖品！"	
										}
											$("#PrizebgListConext").html(Istring);
											LZEvent.EventAnimate('#LZPrizes','#LZEventDiv',"EventDetail5");	
										    if(WeiXin.GetCookie("Longitude")!=""){
												var container = document.getElementById('canvas');
												CanvasObj.init(container, 80, 30, '#ddd');
											}
											$("#JPName").text(o.content.winningDesc);
									/*}else{
										LZEvent.getPosition();		
									}*/
						}else{
							
							$("#JPName").text(o.content.winningDesc);
						
							LZEvent.EventAnimate('#LZPrizes','#LZEventDiv',"EventDetail5");	
						}
						
					}else{
						alert(o.description);	
					}
				}
				
			})	
	},
	Winner:function(){
			
		var getEventID = getParam("EventId") ? getParam("EventId") : "";
		var jsonarr = {'action':'setEventPrizes',ReqContent:JSON.stringify({"common":Base.All(),"special":{"EventId":getEventID }})};
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
					//var o= IsVipCard;
					
			
					if(o.code=="200"){
						
					}
				}
				
			})
	}
	
}

var Pop = {
	AllStyle:function(connext,isMask,title){
		$(".AppBoxL").remove();
		var popString = '<div class="AppBoxL"><div class="AppBoxLTitle"><a href="javascript:void(0)" onclick="Pop.Close()" class="ClosePop"><span class=" incSkin icn3 deTran"></span></a>'+title+'</div><div class="AppBoxLConnnext" id="AppBoxLConnnext">'+connext+'</div></div>';
		$("body").append(popString);
		var getPopHeight = $(".AppBoxL").height();
		if(getPopHeight > Win.H()){
			$(".AppBoxL").css("top","20px");
		}else{
			$(".AppBoxL").css("top", "20px");	
		}
		$("#LZEventBase").css({"overflow":"hidden","height":(((getPopHeight-60) > $(window).height()) ? (getPopHeight-60) : $(window).height() - 50 )})
		if(isMask != "2"){
			$(".AppLayer").css({"height":(Win.DH() > getPopHeight)  ? Win.DH() :getPopHeight }).fadeIn(300);
		}
		$(".AppBoxL").animate({"opacity":1},500)
	},
	LoginPop:function(actionType,id){
		var HtmlString = HtmlTemplates.LoginTmp(actionType,id);	
		Pop.AllStyle(HtmlString)
	},
	Close:function(){
				$(".AppBoxL").remove();
				$(".AppLayer").hide();
				$("#LZEventBase").css({"overflow":"hidden","height":"auto"})
		}
}

function GOFocus(o,id){
	$("#"+id).focus();	
}
function Animi(o){
	
		$(o).siblings(".CddTip").css("background","#fff");	
		$(o).css("background-color","#f2d5b0");	
		$(".BBd").attr("checked",false);
		$(o).find(".BBd").attr("checked",true);
		

}
function sureSelecType(){
	var ofalse = false, oString = '',MString = '';
	$(".BBd").each(function() {
        if($(this).attr("checked") == true || $(this).attr("checked") == "checked"){
			ofalse = true; 	
			$("#skuId").val($(this).val());
			oString = $(this).attr("pli");
			MString = $(this).attr("price");
		}
    });
	if(ofalse == false ){
		alert("请选择一项酒的品类");
		return false; 	
	}

	$("#cTipDivName2").text(oString);
	$("#ApdpBoxL12,#AdLayer1").hide();
	$("#CddTipID").html('');
	$("#priceCID").val(MString);
}
var isWinner = false ; 
var CanvasObj ={
		createCanvas : function (parent, width, height){
			var canvas = {};
			canvas.node = document.createElement('canvas');
			canvas.context = canvas.node.getContext('2d');
			canvas.node.width = width || 80;
			canvas.node.height = height || 30;
			parent.innerHTML ='';
			parent.appendChild(canvas.node);
			return canvas;
		},
		init:function(container, width, height, fillColor){
			var canvas = this.createCanvas(container, width, height);
        var ctx = canvas.context;
        ctx.fillCircle = function(x, y, radius, fillColor) {
			
            this.fillStyle = fillColor;
            this.beginPath();
            this.moveTo(x, y);
            this.arc(x, y, radius, 0, Math.PI * 2, false);
            this.fill();
        };
        ctx.clearTo = function(fillColor) {
            ctx.fillStyle = fillColor;
            ctx.fillRect(0, 0, width, height);
        };
        ctx.clearTo(fillColor || "#ddd");
		
        canvas.node.ontouchmove = function(event) {
			
			$("#LZEventBase").css({"height":$(window).height(),"overflow":"hidden"});
			
			 if (event && event.preventDefault) {
        		 event.preventDefault();  
     		} 
			if (event.targetTouches.length == 1) {  
			var co = $("#canvas").offset();
			var touch = event.targetTouches[0];
            var x = touch.pageX - co.left;
            var y = touch.pageY - co.top;
		
            var radius = 7; // or whatever
            var fillColor = '#ff0000';
            ctx.globalCompositeOperation = 'destination-out';
			if(isWinner == false && x > 12 && x< 36){
					isWinner = true; 
					LZEvent.Winner();
			}
            ctx.fillCircle(x, y, radius, fillColor);	
			}
        };
       canvas.node.touchstart = function(event) {
		 
		    if (event && event.preventDefault) {
        		 event.preventDefault();  
     		}  
			
			$("#LZEventBase").css({"height":$(window).height(),"overflow":"hidden"});
			
			var co = $("#canvas").offset();
			if (event.targetTouches.length == 1) { 
				var touch = event.targetTouches[0];
				var x = touch.pageX - co.left;
				var y = touch.pageY - co.top;
			
				var radius = 7; // or whatever
				var fillColor = '#ff0000';
				  ctx.globalCompositeOperation = 'destination-out';
				if(isWinner == false && x > 12 && x< 36){
						isWinner = true; 
						LZEvent.Winner();
				}
				ctx.fillCircle(x, y, radius, fillColor);
			}
        };
        canvas.node.ontouchend = function(event) {
          
			$("#LZEventBase").css({"height":"auto","overflow":"auto"});
        };
				
		}
			
}





    