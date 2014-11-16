// JavaScript Document
//var url = 'test.php?method='; /*所有异步请求的地址*/
//var url = '../../publicMark'; 
var url = 'weixinData.aspx?method=';
var Timeout = null ;
var GET = new Array(), QueString ='',querystr = new Array();
var position_option = {
	                enableHighAccuracy: true,
	                maximumAge: 0,
               	 	timeout: 20000
	            	};

function getParam(){ 
	//获得html上的参数
		querystr = window.location.href.split("?")
		if(querystr[1]){
				var GETs = querystr[1].split("&")
				for(i=0;i<GETs.length;i++){
					tmp_arr = GETs[i].split("=")
					key=tmp_arr[0]
					GET[key] = tmp_arr[1]
					if(tmp_arr[0]!="openId" && tmp_arr[0] !="page"){
					
							QueString += tmp_arr[0]+"="+tmp_arr[1]+"&"
						}
				}
				deBug = GET["deBug"] ? GET["deBug"] : "0" ;
				QueString = QueString.substring(0,QueString.length-1);
		}
		return querystr[1];
	}
$(function(){
	var deBug = "0" ; 
	getParam();
	
	deBug = deBug.substring(0,1);
	
	$(window).resize(function(){
		$("#EventDetailId,#YaoJiangDetailId,#HuDongDetailID,#AppBgGray").css("width","100%");

	})
	$(window).scroll(function(){
		
			var scrolltop = $(document).scrollTop(), DhAjax = $(document).height(), WHajax= $(window).height();
			
			if((scrolltop+WHajax+90) > DhAjax && $("#hiddenIsNext").length > 0 && $("#hiddenIsNext").attr("isLoading")=="0" && $("#hiddenIsNext").val() == "1"){
					Event.GetMoreWinner();	
				}
		})
	
	//获得hash已取得页面分流
	if($("#DLoading").length > 0 ){
	var appHash = location.hash;
	appHash = appHash.substring(1,appHash.length);
	
	var AppPageType = appHash.split("/"); 
	
	switch(AppPageType[0]){
			case "Event": 
					$(".WAppMenu li").removeClass("WAppMenuActive");
					if(AppPageType[1] == "Detail"){
							if(GET['eventId'] && GET['eventId']!=""){
										var eventUrlHash = new Array();
										eventUrlHash = GET['eventId'].split("#");
										
										$("#IsReferLoad").val('Menu_EventDetail');
										Event.Detail(eventUrlHash[0]);
								}else {
										alert("EventID不存在！")
								}
						}else if(AppPageType[1] == "Prize"){
										var eventUrlHash = new Array();
										eventUrlHash = GET['eventId'].split("#");
										$("#IsReferLoad").val('Menu_Prize')
										Event.PrizeDetail(eventUrlHash[0]);	
						}if(AppPageType[1] == "HuDong"){
								var eventUrlHash = new Array();
								eventUrlHash = GET['eventId'].split("#");
								$("#IsReferLoad").val('Menu_HuDong')
								Event.HuDongDetail(eventUrlHash[0]);	
							}	
				break;
			case "Set": ;
			break;
			case "Down":;
			break;
		}
	}
})
var Base = {
	//公共类的获得
		openId:function(){
		    //return (localStorage.openId  && localStorage.openId !='') ? localStorage.openId : ''; 
		    return (GET['openId'] && GET['openId'] != '') ? GET['openId'] : ''; 
			},
		userId:function(){
				return (localStorage.userId  && localStorage.userId !='') ? localStorage.userId : ''; 
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
				$(".TipJp").css("opacity",0);
				if(type == "CLOSE"){
						$(".loading").hide();
						return false; 
					}
				 var getWindowsWidth = $(window).width(), getWindowsHeight = $(window).height();
					$(".loading").css({left: (getWindowsWidth - $(".loading").width())/ 2 , top: (getWindowsHeight - $(".loading").height())/ 2 }); 
					$(".loading").show();
			},
		W: function(){
			return $(window).width(); 
			},
		H:function(){
			return  $(window).height();
			},
		Tip:function(txt){
			$(".TipJp").html(txt);
			var getTipHeight = $(".TipJp").height();
			$(".TipJp").css("bottom",-getTipHeight);
			var Timeout = setTimeout(function(){$(".TipJp").animate({bottom:0,opacity:1},500);},1200);
				setTimeout(function(){$(".TipJp").animate({"opacity":0,"bottom":-getTipHeight},500);},4000);
		}
	}

	var Event = { //活动
	    Detail: function (eventId, Curid) {
	        //活动详情

	        if (!eventId) {
	            alert('活动不存在！')
	            return;
	        }
	        $("#Menu_EventDetail").parent().addClass("WAppMenuActive");
	        var jsonarr = { ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": eventId} }) };
	        $.ajax({
	            type: 'get',
	            url: url + "getEventDetail",
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                Win.Loading();
	            },
	            dataType: 'json',
	            success: function (o) {
	                deBug = deBug.substring(0, 1);
	                if (deBug == "1") {
	                    var o = DetailData;
	                }
	                if (o.code == 200) {
	                    Win.Loading("CLOSE");
	                    $("#EventDetailId").attr("eventId", eventId);

	                    $("#EventDetailId").css("left", Win.W());
	                    //localStorage.isCheckByBind= o.content.isCheckByBind;
	                    //localStorage.hasUserAccount = o.content.hasUserAccount; 
	                    var htmldata = Templates.EventDetailTemp(o.content);
	                    $("#EventDetailId").html(htmldata).show();
	                    $("#EventDetailId").animate({ "left": 0, "width": Win.W() }, 800);
	                    if (o.content.hasPrize == "1") {
	                        Win.Tip('本活动诚邀校友企业赞助现场奖品，相关事宜联系活动联系人');
	                    }
	                } else {
	                    alert(o.description);
	                }
	            }
	        });
	    },
	    CheckHasBind: function (openId, localLink, type) {
	        var jsonarr = { 'action': "getAccountBind", ReqContent: JSON.stringify({ "common": Base.All(), "special": {} }) };
	        $.ajax({
	            type: 'get',
	            url: url,
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                Win.Loading();
	            },
	            dataType: 'json',
	            success: function (o) {
	                if (deBug == "1") {

	                    var o = Return_getAccountBind;
	                }

	                Win.Loading("CLOSE");
	                if (o.code == 200) {
	                    //	if(o.content.isBindSucceed == "1"){
	                    if (type == "1" && o.content.hasBindWeixin == "1") {
	                        $("#BindUsername").html(o.content.userName);
	                        $('#AlumniConnextDD').hide();
	                        $('#AlumniConnextDD2').show();
	                        return;
	                    } else {
	                        $('#AlumniConnextDD2').hide();
	                        $('#AlumniConnextDD').show();
	                        return;
	                    }
	                    $(".SigininAppLoading").html('装载完成，跳转中...');
	                    alert(o.description);
	                    if (window.localStorage) {
	                        localStorage.userName = o.content.userName;
	                        localStorage.userId = o.content.userId;
	                        // 本地缓存App openId 
	                        localStorage.openId = openId;
	                    }

	                    if (localLink != "") {
	                        location.href = localLink;
	                    }
	                    //}else{
	                    //
	                    //}
	                } else {
	                    alert(o.description);
	                }
	            }
	        });
	    },
	    ActionApply: function () {
	        //if(AppSet.IsLogin() > 0){
	        AppSet.Bg('EventApply', 'getEventApplyQues');
	        //	}else{
	        //	AppSet.Bg('AlumniConnext1','Apply');	
	        //}
	    },
	    ShowApply: function (param) {
	        var eventId = $("#EventDetailId").attr("eventId") ? $("#EventDetailId").attr("eventId") : GET['eventId'];
	        var jsonarr = { ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": eventId} }) };
	        $.ajax({
	            type: 'get',
	            url: url + "getEventApplyQues",
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                Win.Loading();
	            },
	            dataType: 'json',
	            success: function (o) {
	                //					if(deBug == "1"){
	                //					var o = ApplyData;
	                //					}
	                if (o.Code == 200) {
	                    Win.Loading("CLOSE");
	                    var htmldata = Templates.EventDetailApply(o.content,param);
	                    $("#ApplyBmID").html(htmldata);
	                    if (param != "1") {
	                        var getEventID = $("#EventDetailId").attr("eventId");
	                        if (localStorage.getItem("Apply_" + getEventID) && localStorage.getItem("Apply_" + getEventID) != "") {
	                            $(".TipApply").html("您已成功报名！").show();
	                        }
	                    }
	                    AppSet.Bg('EventApply');

	                } else {
	                    alert(o.description);
	                }
	            }
	        });
	    },
	    ApplySubmit: function (param) {
	        //var username = $.trim($("#ApplyName").val());
	        //var moblie = $.trim($("#ApplyMobile").val()); 
	        //var  email = $.trim($("#ApplyEmail").val());
	        var NewsAnswerData = {};
	        /*	if(username == ""){
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
	        */
	        var oAnswerData = {};
	        if (this.GetApplyAnswer("AllQuestionId") != false) {
	            oAnswerData = this.GetApplyAnswer("AllQuestionId");
	            NewsAnswerData = oAnswerData;

	        } else {
	            return false;
	        }
	        var userName = "";
	        if (localStorage.getItem("userNameId") && localStorage.getItem("userNameId") != "") {
	            var userName = $.trim($("#qt_" + localStorage.getItem("userNameId")).val());
	        }
	        //	var jsonarr = {ReqContent:JSON.stringify({"common":Base.All(),"special":{"eventId":$("#EventDetailId").attr("eventId"), userName:username, mobile:moblie, email:email, className:$("#ApplyClass").val(),questionnaireResult:NewsAnswerData}})};
	        var jsonarr = { Form: JSON.stringify({ "common": Base.All(), "special": { "eventId": GET["eventId"], questions: NewsAnswerData, userName: userName, mobile: "", email: ""} }) };
	        $.ajax({
	            type: 'post',
	            url: url + "submitEventApply",
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                Win.Loading();
	            },
	            dataType: 'json',
	            success: function (o) {
	                if (deBug == "1") {
	                    var o = ApplyData;
	                }
	                if (o.Code == 200) {
	                    AppSet.Close("EventApply");
	                    Win.Loading("CLOSE");
	                    alert("报名成功！");
	                    if (param == "1") {
	                        location.href = document.URL;
	                    }
	                    /*设置本地存储*/

	                } else {
	                    alert(o.description);
	                }
	            }
	        });
	    },
	    GetApplyAnswer: function (id) {
	        // id 问题外面包装的Id

	        //先检查必选项是否填写数据
	        var $saves1 = $("#" + id), lf = true

	        $saves1.find("div[id^='QuestionAbc_']").each(function () {
	            if ($(this).attr("isrequired") == "1") {
	                var getVt = $(this).attr("value"), mk = false;
	                $(this).find("[id^='qt_" + getVt + "']").each(function () {
	                    /*checkbox and radio start*/
	                    if ($(this).attr("type") != "text") {
	                        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
	                            mk = true;
	                        }
	                    } else {

	                        var gv = $.trim($(this).val());
	                        if (gv == "") {
	                            lf = false;
	                        } else {
	                            mk = true;
	                        }
	                    }
	                    /*checkbox and radio end*/
	                })

	                if (mk == false) {
	                    lf = false;
	                }
	            }
	        })

	        if (lf == false) {
	            alert("请填写必填项！")
	            return false;
	        } else {
	            var dataArr = [], lfc = true;

	            $saves1.find("div[id^='QuestionAbc_']").each(function (i) {
	                var getVt = $(this).attr("value"), sObj = {};
	                sObj.questionId = getVt;
	                sObj.isSaveOutEvent = $(this).attr("issaveoutevent");
	                sObj.cookieName = $(this).attr("cookiename");
	                //	sObj.type  = $(this).attr("type");
	                sObj.questionValue = '';
	                //sObj.other ='';
	                var answerid = '', other = '';
	                $(this).find("[id^='qt_" + getVt + "']").each(function () {

	                    if ($(this).attr("type") != "text") {
	                        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
	                            answerid += $(this).val() + ",";

	                        }
	                    } else {
	                        answerid = $.trim($(this).val()) + ',';
	                    }
	                })
	                if (answerid != "") {
	                    answerid = answerid.substring(0, answerid.length - 1);
	                    sObj.questionValue = answerid;
	                }

	                dataArr.push(sObj);
	            })
	            //	return 	JSON.stringify(dataArr);
	            if (lfc == false) {
	                return false;
	            } else {
	                return dataArr;
	            }
	        }
	    },
	    AppCheckIn: {
	        getPosition: function (type) {
	            if (type != "pritze") {
	                if (localStorage.isCheckByBind == "1") {
	                    if (localStorage.hasUserAccount == "0") {
	                        AppSet.Bg('AlumniConnext1', 'CheckIn');
	                        return;
	                    }
	                }
	            }
	            navigator.geolocation.getCurrentPosition(function (e) { Event.AppCheckIn.getPositionSuccess(e, type) }, Event.AppCheckIn.getPositionError, position_option);

	        },
	        getPositionSuccess: function (position, type) {
	            var latitude = position.coords.latitude;
	            var longitude = position.coords.longitude;
	            /*if(typeof position.address !== "undefined"){
	            var country = position.address.country;
	            var province = position.address.region;
	            var city = position.address.city;
	            }*/
	            if (type == "pritze") {
	                var jsonarr = { 'action': "submitEventPrize", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": $("#EventDetailId").attr("eventId"), longitude: longitude, latitude: latitude} }) };
	            } else {
	                var jsonarr = { 'action': "submitEventCheckin", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": $("#EventDetailId").attr("eventId"), longitude: longitude, latitude: latitude} }) };
	            }
	            $.ajax({
	                type: 'get',
	                url: url,
	                data: jsonarr,
	                timeout: 90000,
	                cache: false,
	                beforeSend: function () {
	                    Win.Loading();
	                },
	                dataType: 'json',
	                success: function (o) {
	                    //test data
	                    deBug = deBug.substring(0, 1);
	                    if (deBug == "1") {
	                        if (type != "pritze") {
	                            var o = ApplyData;
	                        } else {
	                            var o = PriteListData;
	                        }
	                    }
	                    //test data
	                    Win.Loading("CLOSE");
	                    if (o.code == 200) {
	                        AppSet.Close("EventApply");


	                        if (type == "pritze") {
	                            if (o.content.prizeStatus == 1) {
	                                alert("恭喜您，抽中了。请查看我的奖品！");
	                                Event.PrizeDetail($("#EventDetailId").attr("eventid"), '');
	                            } else {
	                                alert("很可惜，您这次没有中奖!");
	                            }

	                        } else {
	                            alert(o.description);
	                            if (o.description == "操作成功") {
	                                $("#CheckInSuccess").hide();
	                                $("#CheckInSuccessBtn").show();
	                            }
	                        }
	                    } else {
	                        alert(o.description);
	                    }
	                }
	            });
	        },
	        getPositionError: function (error) {
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
	    PrizeDetail: function (eventId, type, Curid) {
	        if (!type) { type = ''; }
	        if (!eventId) {
	            var eventId = $("#EventDetailId").attr("eventId") ? $("#EventDetailId").attr("eventId") : '';
	        }
	        if (eventId == "") {
	            alert('活动不存在！')
	            return;
	        }
	        $("#Menu_Prize").parent().addClass("WAppMenuActive");
	        var jsonarr = { 'action': "getEventPrizeInfo", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": eventId, "type": type} }) };
	        var saveScrolltop = $(document).scrollTop();
	        $.ajax({
	            type: 'get',
	            url: url,
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                if (type == "") {
	                    Win.Loading();
	                } else {
	                    saveScrolltop = $(document).scrollTop();
	                    $(".prizediv_jp").prepend('<div align=center style="padding:14px; "><img width="30" height="30" src="images/382.gif"></div>');
	                }
	            },
	            dataType: 'json',
	            success: function (o) {
	                /*test data*/
	                if (deBug == "1") {
	                    var randdata = [PriteListData, PriteListData1, PriteListData2];
	                    if (type != "") {
	                        for (var i = 0; i < randdata.length; i++) {

	                            if (type == i) {
	                                var o = randdata[i];
	                            }
	                        }
	                    } else {
	                        var o = randdata[Math.floor(Math.random() * 3)];
	                    }
	                }
	                /*test end*/
	                if (o.code == 200) {
	                    Win.Loading("CLOSE");
	                    $("#EventDetailId").attr("eventId", eventId);
	                    if (type == "") {
	                        $("#YaoJiangDetailId").css("left", Win.W());
	                        localStorage.hasUserAccount = o.content.hasUserAccount;
	                        var htmldata = Templates.EventPrizeTemp(o.content);
	                        $("#YaoJiangDetailId").html(htmldata).show();
	                        $("#YaoJiangDetailId").animate({ "left": 0, "width": Win.W() }, 800);
	                    } else {
	                        var htmldata = Templates.EventPrizeTemp(o.content, 1);

	                        $(".prizediv_jp").html(htmldata).show();
	                        $(document).scrollTop(saveScrolltop)
	                    }
	                } else {
	                    alert(o.description);
	                }
	            }
	        });
	    },
	    GetMoreWinner: function () {
	        $("#loadMoreIcn").remove();
	        $("#hiddenIsNext").attr("isLoading", "1");
	        var jsonarr = { 'action': "getEventWinners", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": $("#EventDetailId").attr("eventid"), "type": 2, timeStamp: $("#hiddenIsNext").attr("timestamp")} }) };
	        var saveScrolltop = $(document).scrollTop();
	        $.ajax({
	            type: 'get',
	            url: url,
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                $(".prizediv_jp").append('<div align=center style="padding:14px;" id="loadMoreIcn"><img width="30" height="30" src="images/382.gif"></div>');

	            },
	            dataType: 'json',
	            success: function (o) {
	                /*test data*/
	                if (deBug == "1") {
	                    var o = PriteListData2;
	                }
	                /*test end*/
	                $("#loadMoreIcn").remove();
	                $("#hiddenIsNext").attr("isLoading", "0");
	                if (o.code == 200) {
	                    $("#hiddenIsNext").remove();
	                    var htmldata = Templates.EventPrizeTemp(o.content, 1);
	                    $(".prizediv_jp").append(htmldata);
	                }
	            }
	        });
	    },
	    HuDongDetail: function (eventId, Curid) {
	        if (!eventId) {
	            alert('活动不存在！')
	            return;
	        }
	        $("#Menu_HuDong").parent().addClass("WAppMenuActive");
	        var jsonarr = { 'action': "getEventDiscussx", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": eventId} }) };
	        $.ajax({
	            type: 'get',
	            url: url,
	            data: jsonarr,
	            timeout: 90000,
	            cache: false,
	            beforeSend: function () {
	                Win.Loading();
	            },
	            dataType: 'json',
	            success: function (o) {
	                //deBug = deBug.substring(0,1);
	                //if(deBug == "1"){
	                var o = HuDongData;
	                //}
	                if (o.code == 200) {
	                    Win.Loading("CLOSE");
	                    $("#EventDetailId").attr("eventId", eventId);
	                    $("#HuDongDetailID").css("left", Win.W());
	                    localStorage.hasUserAccount = o.content.hasUserAccount;
	                    var htmldata = Templates.EventInteractiveTemp(o.content);
	                    $("#HuDongDetailID").html(htmldata).show();
	                    $("#HuDongDetailID").animate({ "left": 0, "width": Win.W() }, 800);

	                } else {
	                    alert(o.description);
	                }
	            }
	        });
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
			$("#"+id).fadeIn();
			
			if(Action == "getEventApplyQues"){
					Event.ShowApply();
				}
			if(Action == "CheckIn"){
				return ;
				}
			if(Action == "Map"){
					$("#MapTitle").show();
					return ;	
				}
			if(Action && Action!=""){
					$("#EventAction").val(Action);
				}
			
		},
	IsLogin:function(){
			
			if(window.localStorage &&  localStorage.openId && localStorage.openId !="" && localStorage.hasUserAccount  && localStorage.hasUserAccount ==1   ){
				// 返回2 说明有USEID 并且带有OPENId
					return 2;
				}
			if(window.localStorage && localStorage.userId && localStorage.userId!=""){
				// 返回1 说明有USEID 
					return 1;
			}
			return 0; 
		},
	Close:function(id){
			$("#AppBgGray").hide();
			$("#"+id).hide();
			$("#ApplyBmID").html('');
			$("#MapTitle").hide();
		},
	LoginSubmit:function(){
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
					
					if(deBug =="1"){
						var o = Return_login;
					}
						Win.Loading("CLOSE");	
					if(o.code == 200){
								
								AppSet.Close('AlumniConnext1');
								localStorage.userName = encodeURIComponent(o.content.userName);
								localStorage.userId = o.content.userId;	
								
								switch($("#EventAction").val()){
										case 'Apply': Event.Detail($("#EventDetailId").attr("eventid"));
										break;
										case 'Prize': Event.PrizeDetail($("#EventDetailId").attr("eventid"));
										break;
										default : location.href = document.URL;
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
	
	$("#IsReferLoad").val(Curid)
	$("#"+Curid).parent().siblings("li").removeClass("WAppMenuActive");
	$("#"+Curid).parent().addClass("WAppMenuActive");
	$(".ClassTabJs").each(function(index){
		if($(this).css("left") == "0px"){
					$(this).animate({"left":-Win.W()},500,function(){$(this).hide().html('');
						if(Curid == "Menu_EventDetail"){
						Event.Detail($("#EventDetailId").attr("eventid"),Curid);
						}
					
					if(Curid == "Menu_Prize"){
							Event.PrizeDetail($("#EventDetailId").attr("eventid"),'',Curid);
						}
					if(Curid == "Menu_HuDong"){
							Event.HuDongDetail($("#EventDetailId").attr("eventid"),Curid);
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
		var jsonarr = {'action':"getShopDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{"serviceId":GET['serviceId']}})};
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

function LocalReturnData(DataObj,AloneCookie){
	if(AloneCookie && AloneCookie!=""){
			DataObj = $.parseJSON(DataObj);
			if(DataObj!=null){
				for(var i in DataObj){
						if(i == AloneCookie){
							return DataObj[i].replace(/\'/gi,"&#39").replace(/\"/gi,"&quot;");
							break;	
						}
					}
			}				
			return "";	
			
		}else{
		if(DataObj && DataObj!=null){
				return DataObj.replace(/\'/gi,"&#39").replace(/\"/gi,"&quot;");;
			}else{
				return "";	
			}
	}
}