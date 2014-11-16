// JavaScript Document
var Vip = {
    getVipDetail: function() {
		if(G("islogin") == "0"){
			LinkAddCustomerId("login.html");
			return false; 	
		}
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
            beforeSend: function() {
                Win.Loading();
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");
                if (o.code == "200") {

                    var tpl = _.template($('#getVipDetail').html(), o.content);
                    $("#vipCenter").html(tpl);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    IntegralList: function(isfirst) {
        if (isfirst == "1") {
            $("#VipIntegralXGlist").attr("page", "1");
        }
        var itemName = '';

        $("#WelfareConnext").attr("isloading", "1");
        var page = $("#VipIntegralXGlist").attr("page");
        var jsonarr = {
            'action': "getItemList",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'itemTypeId': "",
                    'page': page,
                    "pageSize": 12,
                    "itemName": "",
                    "isExchange": "1"
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                if (isfirst == "1") {
                    Win.Loading();
                } else {
                    Win.Loading('', "#VipIntegralXGlist");
                }

            },
            dataType: 'json',
            success: function(o) {

                $("#VipIntegralXGlist").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#VipIntegralXGlist");
                if (o.code == "200") {
                    $("#VipIntegralXGlist").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);

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
                    } else {
                        $("#VipIntegralXGlist").html("<div align='center' style='padding-top:20px;'>没有可兑换的商品</div>");
                    }
                    var tpl1 = _.template($('#ppList').html(), context1);
                    var tpl2 = _.template($('#ppList').html(), context2);

                    if (isfirst != "1") {
                        $("#waterwall1").append(context1);
                        $("#waterwall2").append(context2);
                    } else {
                        $("#waterwall1").html(context1);
                        $("#waterwall2").html(context2);
                    }

                } else {
                    alert(o.description);
                }
            }
        })
    },
    getBrowseHistory: function(isfirst) {
        if (isfirst == "1") {
            $("#vipGetBrowseHistory").attr("page", "1");
        }
        var itemName = '';

        $("#vipGetBrowseHistory").attr("isloading", "1");
        var page = $("#vipGetBrowseHistory").attr("page");
        var jsonarr = {
            'action': "getBrowseHistory",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'page': page,
                    "pageSize": 12
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                if (isfirst == "1") {
                    Win.Loading();
                } else {
                    Win.Loading('', "#vipGetBrowseHistory");
                }

            },
            dataType: 'json',
            success: function(o) {
                $("#vipGetBrowseHistory").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#vipGetBrowseHistory");
                if (o.code == "200") {
                    $("#vipGetBrowseHistory").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);

                    var Listdata = o.content.itemList;
                    if (Listdata.length > 0) {} else {
                        $("#vipGetBrowseHistory").html("<div align='center' style='padding-top:20px;'>还没浏览商品</div>");
                    }
                    var tpl = _.template($('#getBrowseHistory').html(), o.content);
                    if (isfirst != "1") {
                        $("#vipGetBrowseHistory").append(tpl);
                    } else {
                        $("#vipGetBrowseHistory").html(tpl);
                    }

                } else {
                    alert(o.description);
                }
            }
        })
    },

    getShoppingCart: function(isfirst) {

        if (isfirst == "1") {
            $("#VipCart").attr("page", "1");
        }
        var itemName = '';
        $("#VipCart").attr("isloading", "1");
        var page = $("#VipCart").attr("page");
        var jsonarr = {
            'action': "getShoppingCart",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'page': page,
                    "pageSize": 12
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                if (isfirst == "1") {
                    Win.Loading();
                } else {
                    Win.Loading('', "#VipCart");
                }

            },
            dataType: 'json',
            success: function(o) {

                $("#VipCart").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#VipCart");
                if (o.code == "200") {
                    $("#VipCart").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
                    var Listdata = o.content.itemList;
                    if (Listdata.length > 0) {
                        var tpl = _.template($("#getShoppingCart").html(), o.content);
                        if (isfirst != "1") {
                            $("#VipCart").append(tpl);

                        } else {
                            $("#VipCart").html(tpl);
							slVipTip(o.content.totalAmount);
                        }
					
                    }else {
						var tpl = _.template($("#Noto").html())
                        $("#aboutVipDiv").html(tpl);
                    }
					$("#aboutVipDiv").show();

                } else {
                    alert(o.description);
                }
            }
        })
    },
    ShopEditCheck: function(o, type) {
        if (type == "all") {
            if ($(o).hasClass("checkBoxTrue")) {
                $(".checkBoxTrue").addClass("checkBoxFalse").removeClass("checkBoxTrue");
            } else {
                $(".checkBoxFalse").addClass("checkBoxTrue").removeClass("checkBoxFalse");
            }
        } else {
            if ($(o).hasClass("checkBoxTrue")) {
                $(o).addClass("checkBoxFalse").removeClass("checkBoxTrue");
                $("#checkBoxALLcc").addClass("checkBoxFalse").removeClass("checkBoxTrue")
            } else {
                $(o).addClass("checkBoxTrue").removeClass("checkBoxFalse");
            }
        }
    },
    ShopEditPP: function(o) {
        var getRel = $(o).attr("rel");
        if (getRel == "0") {
            $(o).attr("rel", "1");
            $(".cartTD4").addClass("cartTD4ADDClass");
            $(".cartTD3").hide();
            $(".cartTD5").show();
        } else {
            $(o).attr("rel", "0");
            $(".cartTD4").removeClass("cartTD4ADDClass");
            $(".cartTD3").show();
            $(".cartTD5").hide();
        }
    },
    getItemExchangeList: function(isfirst) {
        if (isfirst == "1") {
            $("#PPgetItemExchangeList").attr("page", "1");
        }
        var itemName = '';

        $("#PPgetItemExchangeList").attr("isloading", "1");
        var page = $("#PPgetItemExchangeList").attr("page");

        var jsonarr = {
            'action': "getItemExchangeList",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'page': page,
                    "pageSize": 12
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
                if (isfirst == "1") {
                    Win.Loading();
                } else {
                    Win.Loading('', "#PPgetItemExchangeList");
                }

            },
            dataType: 'json',
            success: function(o) {

                $("#PPgetItemExchangeList").attr("isloading", "0");
                if (isfirst == "1") {
                    Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#PPgetItemExchangeList");
                if (o.code == "200") {
                    $("#PPgetItemExchangeList").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);

                    var Listdata = o.content.itemList;

                    if (Listdata.length > 0) {
                        var tpl = _.template($('#getItemExchangeList').html(), o.content);

                        if (isfirst != "1") {
                            $("#PPgetItemExchangeListccc").append(tpl);

                        } else {
                            $("#PPgetItemExchangeListccc").html(tpl);

                        }
                    } else {
                        $("#PPgetItemExchangeListccc").html("<div align='center' style='padding:20px;'>没有数据</div>");
                    }

                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                    alert(o.description);
                }
            }
        })
    },
    DelSKuId: function(id,sd) {
		if(sd !="list"){
			if (!confirm("确定删除吗？")) {
				return false;
			}
		}
        var jsonarr = {
            'action': "setShoppingCart",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'skuId': id,
                    "qty": 0
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
				if(sd !="list"){
                Win.Loading();
				}
            },
            dataType: 'json',
            success: function(o) {
				if(sd !="list"){
              	  Win.Loading("CLOSE");
				}
                if (o.code == "200") {
					if(sd !="list"){
                  	  $("#CartLi_" + id).remove();
					  WelfareAction.DelJSuan();
					}
                } else {
                    alert(o.description);
                }
            }
        })
    },
    setShoppingCart: function(id,sd) {
		if(id == ""){
			var id = $("#DetailSkuId").val();
		}
        var jsonarr = {
            'action': "setShoppingCart",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'skuId': id,
                    "qty": 1
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
				if(sd!="list"){
               	 Win.Loading();
				}
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");

                if (o.code == "200") {
					if(sd!="list"){
						alert(o.description);
					//	$(sd).parent().html('<a href="javascript:void(0)" style="background:#999" class="DetailActionBtn">已加入购物车</a>');	
					ClickCloseDetail();
					}
                } else {
                    alert(o.description);
                }
            }
        })
    },
	Good: function(o,e,showid) {
		var isselect = $(o).attr("isselect");
		if(isselect == "1"){
			
			e.stopPropagation();
			e.preventDefault();
			return false; 	
		}
		
		
		$(o).html('<img src="images/zan.png">');
		$(o).attr("isselect","1");
		var a = animateAll(o,"#zan_"+showid,e);
		
        var jsonarr = {
            'action': "setEventsEntriesPraise",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'entriesId': showid
                }
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
            },
            dataType: 'json',
            success: function(o) {

                Win.Loading("CLOSE");
                if (o.code == "200") {
					
                } 
            }
        })
		e.stopPropagation();
		e.preventDefault();
		return false; 
    },
    ShareDetail: function(type) {
		 var storeId = getParam("storeId") ? getParam("storeId") : "";
      
        var jsonarr = {
            'action': "getStoreDetail",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {'storeId': storeId}
            })
        };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function() {
				if(type!="vipShowDetail"){
                	Win.Loading();
				}
            },
            dataType: 'json',
            success: function(o) {
				
                Win.Loading("CLOSE");
               
                if (o.code == "200") {
					if(type=="vipShowDetail"){
						$("#WeixinHao").html(o.content.tel);
						$("#AppDownLoadLink").attr("href",o.content.address);
						return false; 	
					}
					o.content.tableNum =getParam("tableNum");
					o.content.w = Win.W();
					
                    var returnHtml = _.template($("#ShareDetail").html(),o.content);
                    $("#DetailBigBox").html(returnHtml);
					if($("#wrapper").length > 0 ){
						
					
							
							loadImGWine();
							$(".DetailImgIeo").css("width", o.content.imageList.length * $("#PcBox").width())
					  			 setTimeout(function() {
											myScrollWine.refresh();
										},
										100);	
					}
                } else {
                    alert(o.description);
                }
            }
        })
    },
    DelOrder: function(id) {
        if (!confirm("确定删除吗？")) {
            return false;
        }
        var jsonarr = {
            'action': "setOrderStatus",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": {
                    'orderId': id,
                    "status": 0
                }
            })
        };
        $.ajax({
            type: 'get',
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

                if (o.code == "200") {
                    $("#OrderID_" + id).parent().remove();
                } else {
                    alert(o.description);
                }
            }
        })
    },
	getOrderDetail:function(orderId){
      
        var jsonarr = { 'action': "getOrderList", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': 1, "pageSize": 1,"status":'',"orderId":getParam("orderId")} }) };
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
                    var Listdata = o.content.orderList;
                    if (Listdata.length > 0) {
                        var tpl = _.template($("#getShoppingCart").html(), o.content.orderList[0]);
                        
                            $("#VipCart").html(tpl);
							$("#vipSureCart").show();
							slVipTip(o.content.orderList[0].totalAmount);
							var pes = (WeiXin.GetCookie("preferentialDesc")!="") ? WeiXin.GetCookie("preferentialDesc"):"";
							$("#preferentialDesc").val(pes);
                       		
                    } else {
                        $("#VipCart").html("<div align='center' style='padding:20px;'>没有数据</div>");
                    }
                  

               	 } else {
                 alert(o.description);	
                }
            }
        })
	},
	getOrderList:function(isfirst,status){
        if (isfirst == "1") {
            $("#OrderXX").attr("page", "1");
        }
		var  itemName = '';
	
        $("#OrderXX").attr("isloading", "1");
        var page = $("#OrderXX").attr("page");
	
        var jsonarr = { 'action': "getOrderList", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': page, "pageSize": 6,"status":status,"orderId":""} }) };
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
                    Win.Loading('', "#OrderXX");
                }

            },
            dataType: 'json',
            success: function (o) {
               
                $("#OrderXX").attr("isloading", "0");
                if (isfirst == "1") {
                   	 Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#OrderXX");
                if (o.code == "200") {
                    $("#OrderXX").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
						var Listdata = o.content.orderList;
						if(Listdata.length > 0 ){
					
						 var tpl = _.template($("#getOrderList").html(), o.content);

                    if (isfirst != "1") {
                        $("#OrderXX").append(tpl);
                   	  	
                    } else {
                        $("#OrderXX").html(tpl);
                      	
                    }
						}else{
							$("#OrderXX").html("<div align='center' style='padding-top:20px;'>没有数据</div>");		
						}
						
         

                } else {
                 alert(o.description);	
                }
            }
        })
	},
	getItemKeep:function(isfirst){
		 if (isfirst == "1") {
            $("#VipFav").attr("page", "1");
        }
		var  itemName = '';
	
        $("#VipFav").attr("isloading", "1");
        var page = $("#VipFav").attr("page");
	
        var jsonarr = { 'action': "getItemKeep", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': page, "pageSize": 12} }) };
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
                    Win.Loading('', "#VipFav");
                }

            },
            dataType: 'json',
            success: function (o) {
               
                $("#VipFav").attr("isloading", "0");
                if (isfirst == "1") {
                   	 Win.Loading("CLOSE");
					 $("#vipSureCart").show();
                }
                Win.Loading("CLOSE", "#VipFav");
                if (o.code == "200") {
                    $("#VipFav").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
						var Listdata = o.content.itemList;
						if(Listdata.length > 0 ){
					
						 var tpl = _.template($("#getItemKeep").html(), o.content);

                    if (isfirst != "1") {
                        $("#VipFav").append(tpl);
                   	  	
                    } else {
                        $("#VipFav").html(tpl);
                      	
                    }
						}else{
							$("#VipFav").html("<div align='center' style='padding-top:20px;'>没有数据</div>");		
						}
						
         

                } else {
                 alert(o.description);	
                }
            }
        })
			
	},
	CheckOrderDetail:function(){

        var jsonarr = { 'action': "getOrderList", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': 1, "pageSize": 12,status:"",orderId:getParam("orderId")} }) };
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
						var Listdata = o.content.orderList;
					
						if(Listdata.length > 0 ){
							var tpl = _.template($("#CheckOrderDetailcc").html(), o.content);
                       	 $("#CheckOrderDetail").html(tpl);
                 
						}else{
							$("#CheckOrderDetail").html("<div align='center' style='padding:20px;'>没有数据</div>");		
						}
						
             
                } else {
                 alert(o.description);	
                }
            }
        })
	},
	ShopList:function(isfirst,curPage){

        var jsonarr = { 'action': "getStoreListByItem", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': 1, "pageSize": 100,itemId:"",longitude:"0.0",latitude:"0.0"} }) };
        $.ajax({
            type: 'get',
            url: url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () { 
					if(curPage == "vipSureDetail"){
						 Win.Loading('',"AppShopList");
					}else{
                   	 	Win.Loading();
					}
            },
            dataType: 'json',
            success: function (o) {   
                   	 Win.Loading("CLOSE");
                
                if (o.code == "200") {
						var Listdata = o.content.storeList;
					
						if(Listdata.length > 0 ){
							 o.content.w = Win.W();
							  o.content.isfirst =isfirst;
							var tpl = _.template($("#shopList").html(), o.content);
							if(curPage == "vipSureDetail"){
								$("#AppShopList").html(tpl);
								return false; 
								}
                       		 $("#DetailBigBox").html(tpl);
                 			loadImGWine();
							$(".DetailImgIeo").css("width", o.content.imageList.length * $("#PcBox").width())
							   setTimeout(function() {
													myScrollWine.refresh();
												},
								100);
						}else{
							$("#DetailBigBox").html("<div align='center' style='padding:20px;'>没有数据</div>");		
						}
						
             
                } else {
                 alert(o.description);	
                }
            }
        })
	},
	setUpdateOrderDelivery:function(){
		if(getParam("orderId")==""){
			alert("订单不存在！");
			return false;
		}
        var mobile = $.trim($("#phone").val()), email = $.trim($("#email").val()), remark = $.trim($("#remark").val()),
	
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
		var vipName =  $("#vipName").val(); 
		if(vipName == ""){
			alert("请输入姓名");
            return false;
			}
        var jsonarr = { 'action': "setUpdateOrderDelivery", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "storeId": getThisStore,   "username": $("#vipName").val(), "mobile": mobile, "email": email, "remark": $("#remark").val(), "deliveryId": deliveryId, "deliveryAddress": deliveryAddress, "deliveryTime": deliveryTime,orderId:getParam("orderId")} }) };
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
					 WeiXin.SetCookie("username",vipName,1000*60*60*24*10,"/");
					 WeiXin.SetCookie("phone",mobile,1000*60*60*24*10,"/");
					 WeiXin.SetCookie("preferentialDesc",$("#preferentialDesc").val(),1000*60*60*24*10,"/");
					 WeiXin.SetCookie("email",email,1000*60*60*24*10,"/");
					  WeiXin.SetCookie("deliveryTime",deliveryTime,1000*60*60*24*10,"/");
					   WeiXin.SetCookie("deliveryAddress",deliveryAddress,1000*60*60*24*10,"/");
				  	 LinkAddCustomerId("sureOrder.html?orderId=" + getParam("orderId"));
					
                } else {
                    alert(o.description);
                }
            }
        })
 
		
	}
}

function slVipTip(totalAmount){
			if(AppSet.IsLogin() > 0){
							$("#LoginTip").html('本次消费可累计积分<span style="font-size:16px; color:#e84d93" id="fenshu">'+Math.floor(totalAmount)+'</span>分');
						}else{
							
								var iurl = ReturnUrl(document.URL);
									
							$("#LoginTip").html('<a href="login.html?back='+iurl+'" class="jieSuanBtn" style="margin-right:10px; background:#a73856;width:100px;">登录/注册</a>本次消费可累计积分<span style="font-size:16px; color:#e84d93;" id="fenshu">'+Math.floor(totalAmount)+'</span>分，<br>快速加入会员，获取本次积分。');
						}
}
function ReturnUrl(URLO){
		var getDocumentUrl= URLO, iUrlArr = [], cUrl = [],iurl = '';
		if(getDocumentUrl.indexOf('?')>-1){
			iUrlArr = getDocumentUrl.split("?");
			if(iUrlArr[1].indexOf('&') > -1){
				cUrl =  iUrlArr[1].split("&");
				for(var i = 0; i< cUrl.length; i++){
					if((cUrl[i].indexOf('customerId=')<=-1) && (cUrl[i].indexOf("random=") <=-1) ){
					   iurl += cUrl[i]+"&";
						}
				}
				if(iurl != ""){
				  iurl = iurl.substring(0,iurl.length-1);
				}
			}
		if(iurl !=""){
			iurl = iUrlArr[0]+'?'+iurl;
			}else{
			iurl = iUrlArr[0];
		}
	}else{
	iurl = 	getDocumentUrl;
	}
	iurl = encodeURIComponent(iurl); 
	return iurl;
}