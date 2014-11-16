var GetData = {
    getItemList: function (isfirst, itemTypeId, o, cc) {
        $("#ddRemove").remove();
        $("#WelfareNoConnext").attr("itemTypeId", itemTypeId);
        if (itemTypeId != null) {
            WelfareAction.CloseSearch();
            $("#waterwall1,#waterwall2").html('');
            $("#SearchInputInnerInputss").val('');

            if (cc != "cc") {
                $(".TgongFuli").text($(o).text());
                $(o).addClass("CurItemTypeClass").siblings().removeClass("CurItemTypeClass");
            }

        } else {
            itemTypeId = getParam("itemTypeId") != "" ? getParam("itemTypeId") : $("#WelfareNoConnext").attr("itemTypeId");

        }
        $("#WelfareNoConnext").hide();
        if (isfirst == "1") {
            $("#WelfareConnext").attr("page", "1");
        }
        var itemName = '';
        //   if (cc != null) {
        itemName = ($.trim($("#SearchInputInnerInputss").val()) == "输入关键字快速搜索") ? "" : $.trim($("#SearchInputInnerInputss").val());
        // }

        $("#WelfareConnext").attr("isloading", "1");
        var page = $("#WelfareConnext").attr("page");
        var jsonarr = {
            'action': "getItemList",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'itemTypeId': $("#WelfareNoConnext").attr("itemTypeId"),
                    'page': page,
                    "pageSize": 12,
                    "itemName": itemName
                }
            })
        };
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

                $("#WelfareConnext").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#WelfareConnext");
                if (o.code == "200") {
                    $("#WelfareConnext").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
                    var Listdata = o.content.itemList,
                    context1 = {
                        itemList: []
                    },
                    context2 = {
                        itemList: []
                    };
                    if (Listdata.length > 0) {
                        for (var i = 0; i < Listdata.length; i++) {
                            if (i % 2 == 0) {
                                context1.itemList.push(Listdata[i]);
                            } else {
                                context2.itemList.push(Listdata[i]);
                            }
                        }
                        var tpl1 = _.template($('#applist').html(), context1);
                        var tpl2 = _.template($('#applist').html(), context2);

                        if (isfirst != "1") {
                            $("#waterwall1").append(tpl1);
                            $("#waterwall2").append(tpl2);
                        } else {

                            $("#waterwall1").html(tpl1);
                            $("#waterwall2").html(tpl2);
                            $("#CardNum").text(o.content.shoppingCartCount);



                        }
                    } else {
                        $("#WelfareConnext").prepend("<div align='center' style='padding-top:45px;' id='ddRemove'>没有数据</div>");
                    }

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getVipShowList: function (isfirst, itemTypeId, o, cc) {
        $("#ddRemove").remove();

        if (itemTypeId != null) {
            WelfareAction.CloseSearch();
            $("#waterwall1,#waterwall2").html('');
            $(o).addClass("CurItemTypeClass").siblings().removeClass("CurItemTypeClass");

        } else {
            itemTypeId = getParam("itemTypeId") ? getParam("itemTypeId") : "";
        }
        $("#WelfareNoConnext").hide();
        if (isfirst == "1") {
            $("#WelfareConnext").attr("page", "1");
        }
        var itemName = '';
        if (cc != null) {
            itemName = ($.trim($("#SearchInputInnerInputss").val()) == "输入关键字快速搜索") ? "" : $.trim($("#SearchInputInnerInputss").val());
        }
        $("#WelfareConnext").attr("isloading", "1");
        var page = $("#WelfareConnext").attr("page");
        var jsonarr = {
            'action': "getVipShowList",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {

                    'page': page,
                    "pageSize": 12,
                    "experience": itemName,
                    "isTop": $("#isTop").val(),
                    "isMe": $("#isMe").val(),
                    "isNewest": $("#isNewest").val()
                }
            })
        };
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

                $("#WelfareConnext").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#WelfareConnext");
                if (o.code == "200") {
                    $("#WelfareConnext").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
                    var Listdata = o.content.vipShowList,
                    context1 = {
                        vipShowList: []
                    },
                    context2 = {
                        vipShowList: []
                    };
                    if (isfirst == "1") {
                        $("#waterwall1").html('');
                        $("#waterwall2").html('')
                    }
                    if (Listdata.length > 0) {
                        for (var i = 0; i < Listdata.length; i++) {
                            if (Listdata[i].experience.length > 16) {
                                Listdata[i].experience = Listdata[i].experience.substring(0, 16) + "...";
                            }
                            if (i % 2 == 0) {
                                context1.vipShowList.push(Listdata[i]);
                            } else {
                                context2.vipShowList.push(Listdata[i]);
                            }
                        }
                        var tpl1 = _.template($('#applist').html(), context1);
                        var tpl2 = _.template($('#applist').html(), context2);

                        if (isfirst != "1") {
                            $("#waterwall1").append(tpl1);
                            $("#waterwall2").append(tpl2);
                        } else {

                            $("#waterwall1").html(tpl1);
                            $("#waterwall2").html(tpl2);

                        }
                    } else {
                        $("#WelfareConnext").prepend("<div align='center' style='padding-top:45px;' id='ddRemove'>没有数据</div>");
                    }

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getItemTypeList: function () {
        var abd = $.trim($("#SearchListCy ul").html());
        if (abd != "") {
            return;
        }
        var jsonarr = {
            'action': "getItemTypeList",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {}
            })
        };
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

                Win.Loading("CLOSE", "#SearchListCy");
                if (o.code == "200") {
                    var oledata = o.content.itemTypeList,
                    returnHtml = '';
                    if (oledata.length > 0) {
                        var getoo = $(".TgongFuli").text();
                        for (var i = 0; i < oledata.length; i++) {
                            var icr = '';

                            if (getoo == oledata[i].itemTypeName) {
                                icr = 'class="CurItemTypeClass"';
                            }
                            returnHtml += '<li ' + icr + ' onclick="GetData.getItemList(\'1\',\'' + oledata[i].itemTypeId + '\',this)">' + oledata[i].itemTypeName + '</li>'
                        }
                    } else {
                        returnHtml = "<div align='center'>暂没有分类</div>";
                    }

                    $("#SearchListCy ul").html(returnHtml);
                    loaded();
                    setTimeout(function () {
                        myScroll.refresh();
                    },
                    100)
                }
            }
        })
    },
    setVipDetail: function () {
        var phone = $("#phone").val(),
        email = $.trim($("#email").val()),
        address = $.trim($("#address").val()),
        vipName = $.trim($("#vipName").val());
        if (email != "" && !IsEmail(email)) {
            alert("请正确输入您的邮箱！");
            return false;
        }
        if (vipName == "" || phone == "") {
            alert("请输入必填项!");
            return false;
        }
        var jsonarr = {
            'action': "setVipDetail",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    "phone": phone,
                    "address": address,
                    "email": email,
                    "vipName": vipName
                }
            })
        };
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

        var jsonarr = {
            'action': "getVipDetail",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {}
            })
        };
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


                if (o.code == "200") {
                    $("#email").val(WeiXin.GetCookie("email") != "" ? WeiXin.GetCookie("email") : o.content.email);
                    //     $("#address,#preferentialDesc").val(o.content.address);
                    $("#phone").val(WeiXin.GetCookie("phone") != "" ? WeiXin.GetCookie("phone") : o.content.phone);

                    var oiso = WeiXin.GetCookie("username") != "" ? WeiXin.GetCookie("username") : o.content.phone
                    $("#vipName").val(oiso);



                } else {
                    alert(o.description);
                }
            }
        })
    },
    getItemDetail: function (type) {

        var itemId = getParam("itemId") ? getParam("itemId") : "";
        if (itemId == "") {
            alert("商品不存在");
            return false;
        }
        var jsonarr = {
            'action': "getItemDetail",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'itemId': itemId
                }
            })
        };
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
                Win.Loading("CLOSE");

                if (o.code == "200") {
                    o.content.w = Win.W();
                    var ReturnC1 = _.template($('#DetailHideScript').html(), o.content);
                    $("body").append(ReturnC1);
                    var ReturnHtml = _.template($('#appDetail').html(), o.content);
                    $("#DetailBigBox").html(ReturnHtml);
                    if (o.content.imageList != null && o.content.imageList.length > 0) {
                        loadImGWine();
                        setTimeout(function () {
                            myScrollWine.refresh();
                        },
                        100);
                        $(".DetailImgIeo").css("width", o.content.imageList.length * $("#PcBox").width())
                    }


                } else {
                    alert(o.description);
                }
            }
        })

    },
    getSkuProp2List: function (itemId, propDetailId) {


        var jsonarr = {
            'action': "getSkuProp2List",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'itemId': itemId,
                    'propDetailId': propDetailId
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                $("#getSkuProp2List").html('');
                Win.Loading('', "#getSkuProp2List");
            },
            dataType: 'json',
            success: function (o) {
                Win.Loading('CLOSE', "#getSkuProp2List");

                if (o.code == "200") {
                    o.content.skuId = $("#DetailSkuId").val();
                    var ReturnC1 = _.template($('#DetailGetSkuProp2Script').html(), o.content);
                    $("#getSkuProp2List").html(ReturnC1);
                } else {
                    $("#getSkuProp2List").html("");
                }
            }
        })

    },
    getVipShowById: function (type) {

        var vipShowId = getParam("vipShowId") ? getParam("vipShowId") : "";
        if (vipShowId == "") {
            alert("数据不存在，可能已被删除");
            return false;
        }
        var jsonarr = {
            'action': "getVipShowById",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'vipShowId': vipShowId
                }
            })
        };
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
                Win.Loading("CLOSE");

                if (o.code == "200") {
                    o.content.w = Win.W();
                    o.content.appShareback = getParam("appShareback");
                    if (o.content.appShareback == "1") {
                        Vip.ShareDetail("vipShowDetail");
                    }
                    var ReturnHtml = _.template($('#appDetail').html(), o.content);
                    $("#DetailBigBox").html(ReturnHtml);
                    if (o.content.imageList != null && o.content.imageList.length > 0) {
                        loadImGWine();
                        setTimeout(function () {
                            myScrollWine.refresh();
                        },
                        100);
                        $(".DetailImgIeo").css("width", o.content.imageList.length * $("#PcBox").width())
                    }

                } else {
                    alert(o.description);
                }
            }
        })

    },
    setOrderInfoCard: function () {

        var skuIdArr = [];
        var i = 0;
        $("div[id^=CartLi_]").each(function () {
            if ($(this).find(".checkBoxTrue").length > 0) {
                i++;
                skuIdArr.push({
                    "skuId": $(this).attr("skuId"),
                    "salesPrice": $("#danjia_" + $(this).attr("itemId")).text(),
                    "qty": $("#shuliang_" + $(this).attr("itemId")).text()
                })
            }
        });
        if (i == 0) {
            alert("至少选择一个商品");
            return;
        }
        var jsonarr = {
            'action': "setOrderInfo",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    "skuId": '',
                    "qty": '',
                    "storeId": getParam("storeId"),
                    "salesPrice": '',
                    "stdPrice": '',
                    "totalAmount": $("#totalAmount").text(),
                    "tableNumber": getParam("tableNum"),
                    "username": '',
                    "mobile": '',
                    "email": '',
                    "remark": '1',
                    "deliveryId": '1',
                    "deliveryAddress": '',
                    "deliveryTime": '',
                    "orderDetailList": skuIdArr
                }
            })
        };
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


                Win.Loading("CLOSE");
                if (o.code == "200") {

                    // alert(o.description);
                    LinkAddCustomerId("vipSureCart.html?orderId=" + o.content.orderId);
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

        var jsonarr = {
            'action': "getOrderInfo",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    "orderId": orderId
                }
            })
        };
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

                Win.Loading("CLOSE");

                if (o.code == "200") {
                    $("#tableNum").text(getParam("tableNum"));
                    $("#prod_name").val(o.content.itemname);
                    $("#order_id").val(Base.customerId() + "," + o.content.ordercode);
                    $("#prod_price").val(o.content.salesprice);

                    $("#email").text(o.content.email);
                    $("#mobile").text(o.content.mobile);
                    $("#username").text(o.content.username);

                    var host_server = "http://o2oapi.aladingyidong.com/";
                    $("#merchant_url").val(host_server + '/OnlineAppClothing/detail.html?itemId=' + o.content.itemId + "&customerId=" + getParam("customerId"));

                    $("#call_back_url").val("http://o2oapi.aladingyidong.com/OnlineShopping/data/OnlinePayAfter.aspx");

                    $("#itemname").text(o.content.itemname);
                    $("#ordercode,#ordercode1").text(o.content.ordercode);
                    var totalcc = o.content.totalamount;
                    $("#totalqty").text(parseInt(o.content.totalqty));
                    totalcc = totalcc.substring(0, totalcc.indexOf(".") + 3);
                    $("#totalamount,#totalamount1").text(totalcc);
                    $("#remark").text(o.content.remark);
                    $("#deliveryname").text(o.content.deliveryname);
                    $(".deliveryaddress").text(o.content.deliveryaddress);
                    $(".deliverytime").text(o.content.deliverytime);

                    $("#Dea_" + o.content.deliveryid).show();
                    $("#dDea_" + o.content.deliveryid).show();

                }
            }
        })
    },
    CurOrderPayment: function () {

        var jsonarr = {
            'action': "setOrderPayment",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    "orderId": getParam("orderId"),
                    "paymentTypeId": "B4085585D16B496D9C2D576D8F03724C",
                    "paymentAmount": $("#totalamount1").text()
                }
            })
        };
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
                if (o.code == 200) {
                    LinkAddCustomerId("successWaitPay.html");
                } else {
                    alert(o.description);
                }
            }
        })

    },
    setBrowseHistory: function () {
        var jsonarr = { 'action': "setBrowseHistory", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "itemId": getParam("itemId")} }) };
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

            },
            dataType: 'json',
            success: function (o) {
                if (o.code == "200") {

                    $("#storeNanme").text(o.content.storeName);
                    $("#ListLocation").show();
                }
            }
        })

    },
    getDeliveryList: function () {
        var jsonarr = { 'action': "getDeliveryList", ReqContent: JSON.stringify({ "common": Base.All(), "special": {} }) };
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
                if (o.code == "200") {

                    var ReturnHtml = _.template($('#getDeliveryListId').html(), o.content);
                    $("#PeiSong").html(ReturnHtml);
                }
            }
        })
    }
	,
    ///////////////////////////////////////////////////////////////////////////////
    getClientUserInfo: function (openIdTmp, userIdTmp) {
        //alert(openIdTmp)
        //alert(G("userId"));
        var jsonarr = { 'action': "getClientUserInfo", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "vipId": userIdTmp, "openId": openIdTmp }
        })
        };
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
                if (o.code == "200") {
                    //alert("正确");
                    //                    var ReturnHtml = _.template($('#getDeliveryListId').html(), o.content);
                    //                    $("#PeiSong").html(ReturnHtml);
                } else {
                    alert(o.description);
                    return false;
                }
            }
        })
    }
	,
    ////////////////////////////////////////////////////////////////////////
    setItemKeep: function (itemId, keepStatus, wof) {
        var jsonarr = { 'action': "setItemKeep", ReqContent: JSON.stringify({ "common": Base.All(), "special": { itemId: itemId, keepStatus: keepStatus} }) };
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
                Win.Loading("CLOSE");
                if (o.code == "200") {
                    if (wof == "defav") {
                        $("#CartLi_" + itemId).remove();
                        return false;
                    }
                    if (keepStatus == "1") {
                        $('#fav').html('<img src="images/hasfav.png" width="100%" onclick="GetData.setItemKeep(\'' + itemId + '\',\'0\',this)" />')


                    } else {
                        $('#fav').html('<img src="images/fav.png" width="100%" onclick="GetData.setItemKeep(\'' + itemId + '\',\'1\',this)" />')
                    }
                }
            }
        })
    }
}
var ifalse = false; 
var WelfareAction = {
    Search: function (type) {
        $(document).scrollTop(0);
		 var getWinh = $(window).height();
        $("#WelfareSearch").css("height", getWinh - 45).show().animate({
            "left": 0
        },
        800,
        function() {
            $("#searchBgGray").css({
                "height": getWinh,
                "width": "50%",
                "left": "50%"
            }).show();
        });
        $(".TgongFuli").animate({
            "left": "50%"
        },
        800);
		 $("#PcBox").css({
            "height": getWinh,
            "overflow": "hidden"
        });
		if(type!="1"){
		GetData.getItemTypeList();
		}else{
			if(ifalse == false){
				ifalse = true;
					loaded();
                    setTimeout(function() {
                        myScroll.refresh();
                    },
                    100)	
			}
		}
        $("#SearchListCy").css("height", getWinh - 120);
    },
    CloseSearch: function() {
        $("#WelfareSearch").animate({
            "left": "-50%"
        },
        800,
        function() {
            $(this).hide();
            $("#PcBox").css({
                "height": "auto",
                "overflow": "auto"
            })
        });
        $(".TgongFuli").animate({
            "left": 0
        },
        800);
		$("#searchBgGray").hide();
    },
    EventAnimate: function(id1, id2, Prev) {

        if (Prev == "0") {
            $("#BackPrev").hide();

        } else {
            $("#BackPrev").show();

        }
        //$(id1).css("left",$(id1).width());
        var oleft = $(id2).width();

        if ($(id2).attr("defaultleft") == "0") {
            oleft = -oleft;

        }
        $(id2).animate({
            left: oleft
        },
        500,
        function() {
            $(this).hide();
            $(id1).show().css("opacity", 0);
            var oleft1 = $(id1).width(),
            cLeft = 0;

            if ($(id1).attr("defaultleft") == "0") {
                oleft1 = -oleft1;
            }

            $(id1).css("left", oleft1);
            $(id1).animate({
                "left": 0,
                "opacity": 1
            },
            500);
            $("#backPrevPosition").val(Prev);
        });
		 $(document).scrollTop(0);
    },

    Up: function(id) {
        var getNum = parseInt($("#qty_" + id).text());
        var getPice = $("#danjia_" + id).text();
        var ccco = getNum + 1;

        $("#qty_" + id).text(ccco);
        $("#shuliang_" + id).text(ccco);
        var oddd = accMul(getPice, ccco);

        $("#danp_" + id).val(oddd);
        var Total = 0;
        $("input[id^=danp_]").each(function() {
            var getVal = $(this).val();
            Total = Total + Number(getVal);
        });
        $("#totalAmount,#fenshu").text(Total.toFixed(2))
    },
    Down: function(id) {
        var getNum = parseInt($("#qty_" + id).text());;
        if (getNum == "1") {
            return;
        }
        var getPice = $("#danjia_" + id).text();
        var ccco = getNum - 1;
        $("#qty_" + id).text(ccco);
        $("#shuliang_" + id).text(ccco);
        var oddd = accMul(getPice, ccco);
        $("#danp_" + id).val(oddd);
		var Total = 0;
        $("input[id^=danp_]").each(function() {
            var getVal = $(this).val();

            Total += Number(getVal);
        });

        $("#totalAmount,#fenshu").text(Total.toFixed(2))
    },
    CDDown: function(index) {

        if (!$("#SH_" + index).hasClass("HasSelectYY")) {
            $("li[id^=Tetdd_]").hide();
            $("#Tetdd_" + index).show();
            $("span[id^=SH_]").removeClass("HasSelectYY").addClass("HasSelectNo");
            $("#SH_" + index).removeClass("HasSelectNo").addClass("HasSelectYY");
            if (index == 2 && $.trim($("#AppShopList").text()) == "") {
                Vip.ShopList("1","vipSureDetail");
            }
            $("#deliveryId").val(index);
        }

    },
    CDDownPP: function(id) {
        if (!$("#StoreListc_" + id).find(".Detailicn").hasClass("HasSelectYY")) {
            $("div[id^=StoreListc_]").find(".Detailicn").removeClass("HasSelectYY").addClass("HasSelectNo");
            $("#StoreListc_" + id).find(".Detailicn").removeClass("HasSelectNo").addClass("HasSelectYY");
        }
    },
    DelJSuan: function(id) {
		var Total = 0; 
		if($("input[id^=danp_]").length > 0 ){
      	  $("input[id^=danp_]").each(function() {
            var getVal = $(this).val();

            	Total += Number(getVal);
        });
		 $("#totalAmount,#fenshu").text(Total.toFixed(2))
		}else{
			var tpl = _.template($("#Noto").html());
			$("#aboutVipDiv").html(tpl);
		}
    }
}
function accMul(arg1, arg2) {
    var m = 0,
    s1 = arg1.toString(),
    s2 = arg2.toString();
    try {
        m += s1.split(".")[1].length
    } catch(e) {}
    try {
        m += s2.split(".")[1].length
    } catch(e) {}
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
}