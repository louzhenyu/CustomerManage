
var Welfare = {
    getItemList: function (isfirst, itemTypeId,o,cc) {
        
        if (getParam("openId") != "") {
            WeiXin.SetCookie("openId", getParam("openId"), 24 * 30 * 360, '/');
        }
        if (getParam("userId") != "") {
            WeiXin.SetCookie("userId", getParam("userId"), 24 * 30 * 360, '/');
        }
       if(itemTypeId!=null){
				WelfareAction.CloseSearch();
				$("#waterwall1,#waterwall2").html('');
				$(o).addClass("CurItemTypeClass").siblings().removeClass("CurItemTypeClass");
						
			}else{
				 itemTypeId = getParam("itemTypeId") ? getParam("itemTypeId") :"";
			}
			$("#WelfareNoConnext").hide();	
			
			


        if (isfirst == "1") {
            $("#WelfareConnext").attr("page", "1");
        }
		var  itemName = '';
		if(cc !=null){
			itemName =($.trim($("#SearchInputInnerInputss").val()) == "输入关键字快速搜索") ? "" : $.trim($("#SearchInputInnerInputss").val());
			}
        $("#WelfareConnext").attr("isloading", "1");
        var page = $("#WelfareConnext").attr("page");
        var jsonarr = { 'action': "getItemList", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'itemTypeId': itemTypeId, 'page': page, "pageSize": 12,"itemName":itemName} }) };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                if (isfirst == "1") {
                    Win.Loading();
                } else {
                    Win.Loading('', "#WelfareConnext");
                }

            },
            dataType: 'json',
            success: function (o) {
                if (deBug == "1") {
                    var o = getItemList;
                }
                $("#WelfareConnext").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#WelfareConnext");
                if (o.code == "200") {
                    $("#WelfareConnext").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
                    var ReturnHtml1 = HtmlTemplate.getItemList(o.content, 1);
                    var ReturnHtml2 = HtmlTemplate.getItemList(o.content, 2);

                    if (isfirst != "1") {
                        $("#waterwall1").append(ReturnHtml1);
                        $("#waterwall2").append(ReturnHtml2);
                    } else {
                        $("#waterwall1").html(ReturnHtml1);
                        $("#waterwall2").html(ReturnHtml2);
                    }
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                 Pop.Alert(o.description);	
                }
            }
        })
    },
    getItemTypeList: function () {
        var jsonarr = { 'action': "getItemTypeList", ReqContent: JSON.stringify({ "common": Base.All(), "special": {} }) };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {

                Win.Loading('', "#SearchListCy");


            },
            dataType: 'json',
            success: function (o) {
                if (deBug == "1") {
                    var o = getItemTypeList;
                }

                Win.Loading("CLOSE", "#SearchListCy");
                if (o.code == "200") {

                    var ReturnHtml = HtmlTemplate.getItemTypeList(o.content);

                    $("#SearchListCy ul").html(ReturnHtml);
                    loaded();
                    setTimeout(function () { myScroll.refresh(); }, 100)
                }
            }
        })
    },
    setVipDetail: function () {
        var phone = $("#phone").val(), email = $.trim($("#email").val()), address = $.trim($("#address").val()), vipName = $.trim($("#vipName").val());
        if (email != "" && !IsEmail(email)) {
            alert("请正确输入您的邮箱！");
            return false;
        }
        if (vipName == "" || phone == "" ) {
            alert("请输入必填项!");
            return false;
        }
        var jsonarr = { 'action': "setVipDetail", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "phone": phone, "address": address, "email": email, "vipName": vipName} }) };
        $.ajax({
            type: 'post',
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
                    var o = getItemTypeList;
                }

                Win.Loading("CLOSE");
                if (o.code == "200") {

                    alert(o.description);
            

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getVipDetail: function () {
      
        var jsonarr = { 'action': "getVipDetail", ReqContent: JSON.stringify({ "common": Base.All(), "special": {} }) };
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
                    var o = getVipDetail;
                }

                Win.Loading("CLOSE");
                if (o.code == "200") {

                     $("#phone").val(o.content.phone);
					 $("#email").val(o.content.email);
					 $("#address,#preferentialDesc").val(o.content.address);
					 $("#vipName").val(o.content.vipName);
					
            

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getItemDetail: function (type) {
        /*if(type=="1"){
        if(this.IsCanSumnitOrder() == false){
        return false; 
        }
        }*/

        var itemId = getParam("itemId") ? getParam("itemId") : "";
       
        var jsonarr = { 'action': "getItemDetail", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'itemId': itemId} }) };
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
                    var o = getItemDetail;
                }

                Win.Loading("CLOSE");

                if (o.code == "200") {
                    if (type != "1") {
                        var ReturnHtml = HtmlTemplate.getItemDetail(o.content);

                        $("#DetailBigBox").html(ReturnHtml);
                        if (o.content.imageList != null && o.content.imageList.length > 0) {
                            loadImGWine();
                            setTimeout(function () { myScrollWine.refresh(); }, 100)
                            $(".DetailImgIeo").css("width", o.content.imageList.length * $("#PcBox").width())
                        }
                        $(".Menu").css("left", ($(window).width() - Win.W()) / 2);
                    } else {
                        $("#getDDcTitle").text(o.content.itemName+" "+o.content.skuList[0].skuProp1);
                        $("#danjiadiv").text(o.content.skuList[0].salesPrice);
                        $("#zongjiadiv").text(o.content.skuList[0].salesPrice);
                        $("#skuId").val(o.content.skuList[0].skuId);
                        $("#stdPrice").val(o.content.skuList[0].price);
                        $(".ProvideCom").show();
						 $(".Menu").css("left", ($(window).width() - Win.W()) / 2);
                    }
                } else {
                    alert(o.description);
                }
            }
        })

    },
    getStoreListByItem: function (isfirst, longitude, latitude) {
        var itemId = getParam("itemId") ? getParam("itemId") : "";
        $("#AppShopList").attr("isloading", "1");

        if (itemId == "") {
            Win.Loading("CLOSE");
            alert("商品不存在");
            return false;
        }
        if (isfirst == "1") {
            $("#WelfareConnext").attr("page", "1");
            $("#longitude").val(longitude)
            $("#latitude").val(latitude)
        } else {
            var longitude = $("#longitude").val();
            var latitude = $("#latitude").val();
        }
        var page = $("#AppShopList").attr("page");
        var jsonarr = { 'action': "getStoreListByItem", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'itemId': itemId, 'page': page, "pageSize": 12, longitude: longitude, latitude: latitude} }) };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                if (isfirst == "1") {
                    Win.Loading();
                } else {
                    Win.Loading('', "#AppShopList");
                }

            },
            dataType: 'json',
            success: function (o) {
                if (deBug == "1") {
                    var o = getStoreListByItem;
                }
                $("#AppShopList").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#AppShopList");
                if (o.code == "200") {
                    $("#AppShopList").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);

                    if (isfirst == "2") {
                        var ReturnHtml = HtmlTemplate.getStoreListByItemSroder(o.content);
                        $("#AppShopList").attr("isnext", "0");
                    } else {

                        var ReturnHtml = HtmlTemplate.getStoreListByItem(o.content);

                    }
                    if (isfirst != "1") {
                        $("#AppShopList").append(ReturnHtml);

                    } else {
                        $("#AppShopList").html(ReturnHtml);

                    }
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getPosition: function () {
        Win.Loading();
        navigator.geolocation.getCurrentPosition(function (e) {
            Welfare.getPositionSuccess(e);
        }, function (e) {
            Welfare.getPositionSuccess(e, 0);

        }, position_option);
    },
    getPositionSuccess: function (position, p) {
        var Latitude = 0;
        var Longitude = 0;
        if (p != 0) {
            var Latitude = position.coords.latitude;
            var Longitude = position.coords.longitude;
        }
        Welfare.getStoreListByItem("1", Longitude, Latitude);

    },

    getStoreDetail: function () {


        var storeId = getParam("storeId") ? getParam("storeId") : "";
        if (storeId == "") {
            alert("门店不存在");
            return false;
        }
        var jsonarr = { 'action': "getStoreDetail", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'storeId': storeId} }) };
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
                    var o = getStoreDetail;
                }

                Win.Loading("CLOSE");

                if (o.code == "200") {

                    var ReturnHtml = HtmlTemplate.getStoreDetail(o.content);

                    $("#shopDetail").html(ReturnHtml);
                    if (o.content.imageList != null && o.content.imageList.length > 0) {
                        loadImGWine();
                        setTimeout(function () { myScrollWine.refresh(); }, 100)
                        $(".DetailImgIeo").css("width", o.content.imageList.length * $(".DetailSmallBox").width())
                    }
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                    alert(o.description);
                }
            }
        })

    },
    getBrandDetail: function () {
        var brandId = getParam("brandId") ? getParam("brandId") : "";
        if (brandId == "") {
            alert("该条数据不存在");
            return false;
        }
        var jsonarr = { 'action': "getBrandDetail", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'brandId': brandId} }) };
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
                    var o = getBrandDetail;
                }

                Win.Loading("CLOSE");

                if (o.code == "200") {

                    var ReturnHtml = HtmlTemplate.getBrandDetail(o.content);

                    $("#brandDetail").html(ReturnHtml);

                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    setDownloadItem: function () {
        var itemId = getParam("itemId") ? getParam("itemId") : "";
        if (itemId == "") {
            Win.Loading("CLOSE");
            alert("商品不存在");
            return false;
        }
        var jsonarr = { 'action': "setDownloadItem", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'itemId': itemId} }) };
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
                    var o = getBrandDetail;
                }

                Win.Loading("CLOSE");

                if (o.code == "200") {

                    alert(o.description);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    IsCanSumnitOrder: function (o, type) {
        /*var openId = (window.localStorage&& localStorage.getItem("openId")  && localStorage.getItem("openId") !='') ? localStorage.getItem("openId") : '';
        openId  = (openId == '') ? WeiXin.GetCookie("openId") : openId ;
        if(openId ==""){
        location.href = "noLogin.html"
        return false; 	
        }*/
        if (type == "1") {
			LinkAddCustomerId($(o).attr("shref"));
            //location.href = ;
        }
    },
    getUserInfo: function (type) {

        var urlc = '../../publicMark';
        var jsonarr = { 'action': "getUserInfo", ReqContent: JSON.stringify({ "common": Base.All(), "special": {} }) };
        $.ajax({
            type: 'get',
            url: urlc,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {

            },
            dataType: 'json',
            success: function (o) {
                if (deBug == "1") {
                    var o = getUserInfo;
                }

                Win.Loading("CLOSE");

                if (o.code == "200") {
                    if (type != "2") {
                        $("#username").val(o.content.username);
                        $("#mobile").val(o.content.mobile);
                        $("#email").val(o.content.email);
                        $("#userClass").val(o.content.userClass);
                    } else {
                        $("#username").text(o.content.username);
                        $("#mobile").text(o.content.mobile);
                        $("#email").text(o.content.email);
                        $("#userClass").text(o.content.userClass);
                    }
                    $("#user_id").val(o.content.userId);
                }
            }
        })
    },
    setOrderInfoCard: function () {
       var skuIdArr = [];
	   var i = 0 ; 
	   	$("div[id^=CartLi_]").each(function() {
            if($(this).find(".checkBoxTrue").length > 0){
					i++;
					skuIdArr.push({"skuId":$(this).attr("skuId"),"salesPrice":$("#danjia_"+$(this).attr("itemId")).text(),"qty":$("#shuliang_"+$(this).attr("itemId")).text()})
				}
        });
		if(i == 0 ){
				alert("至少选择一个商品");
				return; 
			}
        var jsonarr = { 'action': "setOrderInfo", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "skuId": '', "qty": '', "storeId": '', "salesPrice": '', "stdPrice": '', "totalAmount": $("#totalAmount").text(), "username": '', "mobile": '', "email": '', "remark": '', "deliveryId": '', "deliveryAddress": '', "deliveryTime": '',"orderDetailList":skuIdArr} }) };
        $.ajax({
            type: 'post',
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
                    var o = setOrderInfo;
                }

                Win.Loading("CLOSE");
                if (o.code == "200") {

                   // alert(o.description);
				  	 LinkAddCustomerId("submitOrder.html?orderId=" + o.content.orderId);
				 	//  location.href = &customerId="+getParam("customerId");
                   // setTimeout(function(){location.href = "sureOrder.html?orderId=" + o.content.orderId;},1500);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    setOrderInfo: function () {
        var mobile = $.trim($("#phone").val()), email = $.trim($("#email").val()), remark = $.trim($("#remark").val()),
		skuId = $("#skuId").val(),
		qty = $("#qty").text(),
		deliveryId = $("#deliveryId").val();

        var deliveryAddress = $.trim($("#preferentialDesc").val());
        if (deliveryId == "1") {
            if (deliveryAddress == "") {
                alert("请填写送货的地址！");
                return false;
            }
        }
        var idg = false, getThisStore = '';
        if ($("div[id^=StoreListc_]").length > 0) {
            $("div[id^=StoreListc_]").each(function () {
                if ($(this).find(".Detailicn").hasClass("HasSelectYY")) {
                    getThisStore = $(this).attr("storeid");
                    idg = true;
                    return idg;
                }

            });
        }
        var deliveryTime = $.trim($("#deliveryTime").val());
        if (deliveryId == "2") {
            if (idg == false) {
                alert("请选择提货的门店！");
                return false;
            }
            if (deliveryTime == "") {
                alert("请填写提货的时间！");
                return false;
            }

        }



        if (email != "" && !IsEmail(email)) {
            alert("请正确输入您的邮箱！");
            return false;
        }
        if (mobile == "") {
            alert("请输入必填项!");
            return false;
        }
        var jsonarr = { 'action': "setOrderInfo", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "skuId": skuId, "qty": qty, "storeId": getThisStore, "salesPrice": $("#danjiadiv").text(), "stdPrice": $("#stdPrice").val(), "totalAmount": $("#zongjiadiv").text(), "username": $("#vipName").val(), "mobile": mobile, "email": email, "remark": $("#remark").val(), "deliveryId": deliveryId, "deliveryAddress": deliveryAddress, "deliveryTime": deliveryTime} }) };
        $.ajax({
            type: 'post',
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
                    var o = setOrderInfo;
                }

                Win.Loading("CLOSE");
                if (o.code == "200") {

                   // alert(o.description);
				  	 LinkAddCustomerId("sureOrder.html?orderId=" + o.content.orderId);
				 	//  location.href = &customerId="+getParam("customerId");
                   // setTimeout(function(){location.href = "sureOrder.html?orderId=" + o.content.orderId;},1500);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getOrderPayment: function () {
        var orderId = getParam("orderId") ? getParam("orderId") : "";
        if (orderId == "") {
            alert("该订单不存在！");
            return false;
        }

        var jsonarr = { 'action': "getOrderInfo", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "orderId": orderId} }) };
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
                    var o = getOrderInfo;
                }

                Win.Loading("CLOSE");

                if (o.code == "200") {
                   
                    $("#prod_name").val(o.content.itemname);
                    $("#order_id").val(Base.customerId()+","+o.content.ordercode);
                    $("#prod_price").val(o.content.salesprice);
                    
                    $("#email").text(o.content.email);
                    $("#mobile").text(o.content.mobile);
                    $("#username").text(o.content.username);
                    
                    var host_server = "http://o2oapi.aladingyidong.com/";
                    $("#merchant_url").val(host_server + '/OnlineShopping/welfareDetail.html?itemId=' + o.content.itemId+"&customerId="+getParam("customerId"));
                //    var call_back_url = host_server + "/phone_controller?action=wx_online_payment_callback&type=wap&module=1&";
                  //  call_back_url += 'paymentAmount=' + o.content.totalamount
                    //+'&paymentTypeId='+o.content.paymentTypeId;
					//	+ '&paymentTypeId=1&local=zh&openId=' + Base.openId() + '&';
                    $("#call_back_url").val("http://o2oapi.aladingyidong.com/OnlineShopping/data/OnlinePayAfter.aspx");

                    $("#itemname").text(o.content.itemname);
                    $("#ordercode").text(o.content.ordercode);
					var totalcc = o.content.totalamount;
					totalcc = totalcc.substring(0,totalcc.indexOf(".")+3);
                    $("#totalamount").text(totalcc);
                    $("#remark").text(o.content.remark);
                    $("#deliveryname").text(o.content.deliveryname);
                    $(".deliveryaddress").text(o.content.deliveryaddress);
                    $(".deliverytime").text(o.content.deliverytime);
                   // if (o.content.deliveryid == "1") {
                        $("#Dea_" + o.content.deliveryid).show();
                        $("#dDea_" + o.content.deliveryid).show();
                    //}
 					$(".Menu").css("left", ($(window).width() - Win.W()) / 2);
                }
            }
        })
    },
	setBrowseHistory:function(){
		   var jsonarr = { 'action': "setBrowseHistory", ReqContent: JSON.stringify({ "common": Base.All(), "special": {"itemId":getParam("itemId")} }) };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
   
            },
            dataType: 'json',
            success: function (o) {
           		
            }
        })	
	}
}


var WelfareAction = {
		Search:function(){
					$(document).scrollTop(0)
				var getWinh = $(window).height();
				$("#WelfareSearch").css({"left":Win.W(),"height":getWinh}).show().animate({"left":"30%"},800,function(){
						$("#searchBgGray").css({"height":getWinh,"width":"30%"}).fadeIn(300);
					});
				$("#PcBox").css({"height":getWinh,"overflow":"hidden"})
				$("#AppCom").animate({"left":"-70%"},800);
				
				//$(".Menu>.TopMenu").animate({"left":"-70%"},800)
				Welfare.getItemTypeList();
				$("#SearchListCy").css("height",getWinh - 120);
		},
		CloseSearch:function(){
			//$(".Menu>.TopMenu").animate({"left":0},800)
			$("#AppCom").animate({"left":0},800);	
			$("#WelfareSearch").animate({"left":"100%"},800,function(){$(this).hide(); $("#PcBox").css({"height":"auto","overflow":"auto"})});
			$("#searchBgGray").hide();
		},
	EventAnimate:function(id1,id2,Prev){

		if(Prev == "0"){
			$("#BackPrev").hide();
			
		}else{
			$("#BackPrev").show();
			
		
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
				var Link = ''; 
				if(getBack =="welfareList"){
						Link = "welfareList.html"; 
				}
				if(getBack =="welfareDetail"){
						var itemId = getParam("itemId") ? getParam("itemId") :"";
						if(itemId!=""){
							Link = "welfareDetail.html?itemId="+itemId
						}else{
							Link = "welfareList.html"; 
							}
					}
				if(getBack =="shopDetail"){
					var itemId = getParam("itemId") ? getParam("itemId") :"";
						if(itemId!=""){
							Link = "shopList.html?itemId="+itemId
						}else{
							Link = "welfareList.html"; 
						}	
				}
				if(getBack =="brandDetail"){
							var itemId = getParam("itemId") ? getParam("itemId") :"";
						if(itemId!=""){
							Link = "welfareDetail.html?itemId="+itemId
						}else{
							Link = "welfareList.html"; 
						}	
					}
				if(getBack =="donationList"){
						Link = "donationList.html"; 
					}
				if(getBack =="donationInfo"){
					Link = "donationInfo.html"; 
					}
				
				if(getBack =="courseList"){
					Link = "courseList.html"; 
					}
				if(getBack =="courseDetail"){
							var itemId = getParam("itemId") ? getParam("itemId") :"";
						if(itemId!=""){
							Link = "courseDetail.html?itemId="+itemId
						}else{
							Link = "courseList.html"; 
						}	
				}
				if(getBack == "vipCenter"){
					Link = "vipCenter.html"; 
				}
			LinkAddCustomerId(Link)
			
		},
		PersonAnimate:function(s){
			var oindex = parseInt($("#GoUlId").attr("page"));
			var ttlength = $("#GoUlId>span").length;
		
				if(s == "2"){
				
							oindex++;
						
							$(".CLeft").show();	
							if(oindex==ttlength){oindex=0;}	
							this.advFun(oindex);
							$("#GoUlId").attr("page", oindex)
						
				}else{
		
							oindex--;
						
							if(oindex==0){$(".CLeft").hide();	}
							this.advFun(oindex);
							
							
							$("#GoUlId").attr("page", oindex)
				}
		},
		advFun: function(n){	
					$("#GoUlId>span").eq(n).show();	
					$("#GoUlId").stop(true,false).animate({"margin-left" : -iWinth*n},400);	
					
                 
        },
		Up:function(id){
			var getNum  = parseInt($("#qty_"+id).text());
			var  getPice = $("#danjia_"+id).text();
			var ccco  = getNum+1; 
		
			$("#qty_"+id).text(ccco);
			$("#shuliang_"+id).text(ccco);
			var oddd= accMul(getPice,ccco);
		
			$("#danp_"+id).val(oddd);
			var Total = 0; 	
			$("input[id^=danp_]").each(function() {
              	var getVal= $(this).val();
				 Total =Total+ Number(getVal);
            });
			$("#totalAmount").text(Total.toFixed(2))	
		},
		Down:function(id){
			var getNum  = parseInt($("#qty_"+id).text());;
			if(getNum=="1"){
				return ; 
			}
			var  getPice = $("#danjia_"+id).text();
			var ccco  = getNum-1; 
			$("#qty_"+id).text(ccco);
				$("#shuliang_"+id).text(ccco);
			var oddd= accMul(getPice,ccco);
			$("#danp_"+id).val(oddd)
			var Total =0; 	
			$("input[id^=danp_]").each(function() {
              	var getVal= $(this).val();
				
				 Total += Number(getVal);
            });
			
			$("#totalAmount").text(Total.toFixed(2))	
		},
	CDDown:function(index){
	
		if(!$("#SH_"+index).hasClass("HasSelectYY")){
			$("li[id^=Tetdd_]").hide();
			$("#Tetdd_"+index).show();
			$("span[id^=SH_]").removeClass("HasSelectYY").addClass("HasSelectNo");
			$("#SH_"+index).removeClass("HasSelectNo").addClass("HasSelectYY");
			if(index == 2 && $.trim($("#AppShopList").text()) == ""){
					Welfare.getStoreListByItem("2",0,0);
			}
			$("#deliveryId").val(index);
		}
	
	},
	CDDownPP:function(id){
			if(!$("#StoreListc_"+id).find(".Detailicn").hasClass("HasSelectYY")){
				$("div[id^=StoreListc_]").find(".Detailicn").removeClass("HasSelectYY").addClass("HasSelectNo");
				$("#StoreListc_"+id).find(".Detailicn").removeClass("HasSelectNo").addClass("HasSelectYY");
			}
	}
}
function accMul(arg1,arg2)
{
  var m=0,s1=arg1.toString(),s2=arg2.toString();
  try{m+=s1.split(".")[1].length}catch(e){}
  try{m+=s2.split(".")[1].length}catch(e){}
  return Number(s1.replace(".",""))*Number(s2.replace(".",""))/Math.pow(10,m)
}

var Donation = {
		DonationList:function(){
			
			var jsonarr = {'action':"getDonationList",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					
						var o = getDonationList;
					
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						var ReturnHtml = HtmlTemplate.getDonationList(o.content);
						$("#DonationListUl").html(ReturnHtml);
						
					}else{
						alert(o.description);	
					}
					$("#DonationListId").show();
					$(".Menu").css("left",($(window).width() -Win.W()) / 2);
				}
			})	
		},
		setOrderDonation:function(){
			var username=$.trim($("#username").val()),userClass=$.trim($("#userClass").val()),company=$.trim($("#company").val()),
		usertitle = $("#usertitle").val(),
		tel = $("#tel").text(),
		mobile = $("#mobile").val(),email = $("#email").val(),address = $("#address").val(),money = $("#money").val(),
		PleaseSelect = $("#PleaseSelect").text();
		var reg = /^(0|[1-9][0-9]*)$/;
			if(!reg.test(money)){
				alert("请正确输入捐赠的金额！");
				return false; 
			}
			if(PleaseSelect =="请选择"){
				alert("请选择投资用途！");
				return false; 
			}
		var jsonarr = {'action':"getDonationList",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					
						var o = getDonationList;
					
				
						Win.Loading("CLOSE"); 
					alert(o.description);
					if(o.code == "200"){
						LinkAddCustomerId('sureDonationOrder.html');;
						
					}else{
						alert(o.description);	
					}
					
				}
			})			
			
	},
	getDonationOrderPayment : function(){
			
		 var jsonarr = {'action':"getOrderInfo",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					Win.Loading(); 		
				},
				dataType : 'json',
				success:function(o){
					
						var o = getOrderInfo;
					
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						//value="<%=Constants.Server_Host %>phone_controller?action=wx_online_payment_callback&type=wap" 
						$("#prod_name").val(o.content.itemname);
						$("#order_id").val(o.content.orderid);
						$("#prod_price").val(o.content.salesprice);
						var host_server= "http://alumniapp.ceibs.edu:8080/ceibs";
						$("#merchant_url").val(host_server+'/wap/welfare/welfareDetail.html?itemId='+o.content.itemid+"&customerId="+getParam("customerId"));
						var call_back_url=host_server+"/phone_controller?action=wx_online_payment_callback&type=wap&module=1&";
						call_back_url+= 'paymentAmount='+o.content.totalamount
						//+'&paymentTypeId='+o.content.paymentTypeId;
						//+'&paymentTypeId=1&local=zh&openId='+Base.openId()+'&';
						$("#call_back_url").val(call_back_url);
						
						$("#itemname").text(o.content.itemname);
						$("#ordercode").text(o.content.ordercode);
						$("#totalamount").text(o.content.totalamount);
						$("#remark").text(o.content.remark);
						$("#deliveryname").text(o.content.deliveryname);
						$(".deliveryaddress").text(o.content.deliveryaddress);
						$(".deliverytime").text(o.content.deliverytime);
						if(o.content.deliveryid=="1"){
							$("#Dea_"+o.content.deliveryid).show();
							$("#dDea_"+o.content.deliveryid).show();
						}
						
					}
				}
			})

			
	}
}
var DonationAction ={
		slideDownTT:function(o){
			var getVl = $(o).attr("rel");
			if(getVl == "1"){
				$("#SelectMor").slideDown();
				$(o).attr("rel","0")	
			}else{
				$("#SelectMor").slideUp();	
				$(o).attr("rel","1")	
			}
		},
		GetDD:function(o){
			var getVl = $(o).attr("rel");
			if(getVl == "1"){
				$("#Tetddc").show();
					
			}else{
				$("#Tetddc").hide();	
					
			}	
			var getTVal = $(o).find(".GetDD").text();
			$("#PleaseSelect").text(getTVal);
			if($(o).find(".Detailicn").hasClass("HasSelectNo")){
				$(".sg1").removeClass("HasSelectYY").addClass("HasSelectNo");
				$(o).find(".Detailicn").removeClass("HasSelectNo").addClass("HasSelectYY");
			}
			
			$("#SelectMor").slideUp();
		},
		YQ:function(o){
			var getshowid = $(o).attr("showid");
			if($(o).hasClass("NoChecked")){
				$(o).removeClass("NoChecked").addClass("HasChecked");
				$(o).attr("rel","1");
				if(getshowid!=""){
						$("#"+getshowid).show()
				}	
			}else{
				$(o).removeClass("HasChecked").addClass("NoChecked");	
				$(o).attr("rel","0");
				if(getshowid!=""){
						$("#"+getshowid).hide()
				}	
			}
		}
}
var Course = {
		getItemList:function(isfirst, itemTypeId){
				
			if(itemTypeId && itemTypeId!=""){
				WelfareAction.CloseSearch();	
				$("#waterwall1,#waterwall2").html('');
							
			}else{
				 itemTypeId = getParam("itemTypeId") ? getParam("itemTypeId") :"";
			}
			
			
			if(isfirst == "1"){
				$("#CourseConnext").attr("page","1"); 
			}
			$("#CourseConnext").attr("isloading","1");
			var page  = $("#CourseConnext").attr("page");	
			var jsonarr = {'action':"getItemList",ReqContent:JSON.stringify({"common":Base.All(),"special":{'itemTypeId':itemTypeId, 'page':page, "pageSize":12}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					if(isfirst == "1"){
						Win.Loading();		
					}else{
						Win.Loading('',"#CourseConnext");	
					}
						
				},
				dataType : 'json',
				success:function(o){
			
						var o = getItemList;
				
					$("#CourseConnext").attr("isloading","0");
					if(isfirst == "1"){
						Win.Loading("CLOSE"); 
					}
					Win.Loading("CLOSE","#CourseConnext");
					if(o.code == "200"){
						$("#CourseConnext").attr("page",parseInt(page)+1).attr("isnext",o.content.isNext); 
						var ReturnHtml1 = HtmlTemplate.CourseGetItemList(o.content,1);
						var ReturnHtml2  = HtmlTemplate.CourseGetItemList(o.content,2);
						
						if(isfirst != "1"){
							$("#waterwall1").append(ReturnHtml1);
							$("#waterwall2").append(ReturnHtml2);
							}else{
							$("#waterwall1").html(ReturnHtml1);
							$("#waterwall2").html(ReturnHtml2);	
							}
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		getCourseDetail:function(){
			var itemId = getParam("itemId") ? getParam("itemId") :"";	
			if(itemId ==""){
					alert("商品不存在");
					return false; 
				}
				
			var jsonarr = {'action':"getCourseDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{'itemId':itemId}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					if(itemId=="1"){
						var o = getItemCourseDetail1;
					}else{
						var o = getItemCourseDetail2;
						}
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
					
						var ReturnHtml = HtmlTemplate.getCourseDetail(o.content);
					
						$("#CourseDetailDiv").html(ReturnHtml);
						if(o.content.imageList !=null && o.content.imageList.length > 0 ){
							loadImGWine();
							setTimeout(function(){myScrollWine.refresh();},100)
							$(".DetailImgIeo").css("width", o.content.imageList.length * $("#wrapper").width())
						}
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		getCoursePageDetail:function(){
			var itemId = getParam("itemId") ? getParam("itemId") :"";	
			if(itemId ==""){
					alert("商品不存在");
					return false; 
				}
				
			var jsonarr = {'action':"getCoursePageDetail",ReqContent:JSON.stringify({"common":Base.All(),"special":{'itemId':itemId}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					if(itemId=="1"){
						var o = getItemCourseDetail1;
					}else{
						var o = getItemCourseDetail2;
						}
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
					
						var ReturnHtml = HtmlTemplate.getCoursePageDetail(o.content);
					
						$("#CoursePageDetailDiv").html(ReturnHtml);
						
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
						
					}else{
						alert(o.description);	
					}
				}
			})
		},
		CourseSetOrder:function(){
			var username=$.trim($("#username").val()),userClass=$.trim($("#userClass").val()),company=$.trim($("#company").val()),
		usertitle = $("#usertitle").val(),
		tel = $("#tel").text(),
		mobile = $("#mobile").val(),email = $("#email").val(),address = $("#address").val(),
		PleaseSelect = $(".HasSelectYY").attr("rel");
		if(username=="" || userClass=="" || company=="" || mobile==""){
			alert("请输入必须字段"); 
			return false; 
			}

		var jsonarr = {'action':"getDonationList",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
						Win.Loading();			
				},
				dataType : 'json',
				success:function(o){
					
						var o = getDonationList;
					
				
						Win.Loading("CLOSE"); 
					alert(o.description);
					if(o.code == "200"){
						LinkAddCustomerId('sureCourseOrder.html');
						//location.href = "sureCourseOrder.html";
						
					}else{
						alert(o.description);	
					}
					
				}
			})	
		},
	getCourseOrderPayment:function(){
		var jsonarr = {'action':"getOrderInfo",ReqContent:JSON.stringify({"common":Base.All(),"special":{}})};
				$.ajax({
				type:'get',
				url:'test.htm',
				data:jsonarr,
				timeout:90000,
				cache:false,
				beforeSend:function(){
					Win.Loading(); 		
				},
				dataType : 'json',
				success:function(o){
					
						var o = getOrderInfo;
					
				
						Win.Loading("CLOSE"); 
					
					if(o.code == "200"){
						//value="<%=Constants.Server_Host %>phone_controller?action=wx_online_payment_callback&type=wap" 
						$("#prod_name").val(o.content.itemname);
						$("#order_id").val(o.content.orderid);
						$("#prod_price").val(o.content.salesprice);
						var host_server= "http://alumniapp.ceibs.edu:8080/ceibs";
						$("#merchant_url").val(host_server+'/wap/welfare/welfareDetail.html?itemId='+o.content.itemid);
						var call_back_url=host_server+"/phone_controller?action=wx_online_payment_callback&type=wap&module=1&";
						call_back_url+= 'paymentAmount='+o.content.totalamount
						//+'&paymentTypeId='+o.content.paymentTypeId;
						//+'&paymentTypeId=1&local=zh&openId='+Base.openId()+'&';
						$("#call_back_url").val(call_back_url);
						
						$("#itemname").text(o.content.itemname);
						$("#ordercode").text(o.content.ordercode);
						$("#totalamount").text(o.content.totalamount);
						$("#remark").text(o.content.remark);
						$("#deliveryname").text(o.content.deliveryname);
						$(".deliveryaddress").text(o.content.deliveryaddress);
						$(".deliverytime").text(o.content.deliverytime);
					
						
					}
				}
			})
	}
	
}

