// JavaScript Document
var Vip = {
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
               

                Win.Loading("CLOSE");
                if (o.code == "200") {
            	//	var returnHtml = vipTemplates.vipCenter(o.content);
					//alert(document.getElementById('getVipDetail').innerHTML)
						var tpl = new jSmart(document.getElementById('getVipDetail').innerHTML,"utf-8");
						
						var returnHtml = tpl.fetch(o.content);
					//	returnHtml=returnHtml.replace('\r','').replace('\n','').replace('\t','');
						
					//	alert(returnHtml);
						$("#vipCenter").html(returnHtml);
						$(".Menu").css("left",($(window).width() -Win.W()) / 2);
                } else {
                    alert(o.description);
                }
            }
        })
    },
	IntegralList:function(isfirst){
        if (isfirst == "1") {
            $("#VipIntegralXGlist").attr("page", "1");
        }
		var  itemName = '';
	
        $("#WelfareConnext").attr("isloading", "1");
        var page = $("#VipIntegralXGlist").attr("page");
        var jsonarr = { 'action': "getItemList", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'itemTypeId': "", 'page': page, "pageSize": 12,"itemName":"","isExchange":"1"} }) };
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
                    Win.Loading('', "#VipIntegralXGlist");
                }

            },
            dataType: 'json',
            success: function (o) {
               
                $("#VipIntegralXGlist").attr("isloading", "0");
                if (isfirst == "1") {
                   	 Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#VipIntegralXGlist");
                if (o.code == "200") {
                    $("#VipIntegralXGlist").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
						
						var Listdata = o.content.itemList, context1 = {itemList:[]}, context2 = {itemList:[]};
						if(Listdata.length > 0 ){
							for(var i=0; i< Listdata.length; i++){
								if(i%2 ==0){
									context1.itemList.push(Listdata[i]);	
								}else{
									context2.itemList.push(Listdata[i]);
								}
							}
						}else{
							$("#VipIntegralXGlist").html("<div align='center' style='padding-top:20px;'>没有可兑换的商品</div>");		
						}
						var tpl1 = new jSmart(document.getElementById('ppList').innerHTML);
						
						var returnHtml1= tpl1.fetch(context1);
						var returnHtml2= tpl1.fetch(context2);


                    if (isfirst != "1") {
                        $("#waterwall1").append(returnHtml1);
                   	  	 $("#waterwall2").append(returnHtml2);
                    } else {
                        $("#waterwall1").html(returnHtml1);
                      	 $("#waterwall2").html(returnHtml2);
                    }
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                 alert(o.description);	
                }
            }
        })
	},
	getBrowseHistory:function(isfirst){
        if (isfirst == "1") {
            $("#vipGetBrowseHistory").attr("page", "1");
        }
		var  itemName = '';
	
        $("#vipGetBrowseHistory").attr("isloading", "1");
        var page = $("#vipGetBrowseHistory").attr("page");
	
        var jsonarr = { 'action': "getBrowseHistory", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': page, "pageSize": 12} }) };
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
                    Win.Loading('', "#vipGetBrowseHistory");
                }

            },
            dataType: 'json',
            success: function (o) {
               
                $("#vipGetBrowseHistory").attr("isloading", "0");
                if (isfirst == "1") {
                   	 Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#vipGetBrowseHistory");
                if (o.code == "200") {
                    $("#vipGetBrowseHistory").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
						
						var Listdata = o.content.itemList;
						if(Listdata.length > 0 ){
							
						}else{
							$("#vipGetBrowseHistory").html("<div align='center' style='padding-top:20px;'>还没浏览商品</div>");		
						}
						var tpl = new jSmart(document.getElementById('getBrowseHistory').innerHTML);
						
						var returnHtml= tpl.fetch(o.content);

                    if (isfirst != "1") {
                        $("#vipGetBrowseHistory").append(returnHtml);
                   	  	
                    } else {
                        $("#vipGetBrowseHistory").html(returnHtml);
                      	
                    }
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

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
							var tpl = new jSmart(document.getElementById('getOrderList').innerHTML);
						
						var returnHtml= tpl.fetch(o.content);

                    if (isfirst != "1") {
                        $("#OrderXX").append(returnHtml);
                   	  	
                    } else {
                        $("#OrderXX").html(returnHtml);
                      	
                    }
						}else{
							$("#OrderXX").html("<div align='center' style='padding-top:20px;'>没有数据</div>");		
						}
						
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                 alert(o.description);	
                }
            }
        })
	},
	getShoppingCart:function(isfirst){
		
		 if (isfirst == "1") {
            $("#VipCart").attr("page", "1");
        }
		var  itemName = '';
	
        $("#VipCart").attr("isloading", "1");
        var page = $("#VipCart").attr("page");
	
        var jsonarr = { 'action': "getShoppingCart", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': page, "pageSize": 12} }) };
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
                    Win.Loading('', "#VipCart");
                }

            },
            dataType: 'json',
            success: function (o) {
               
                $("#VipCart").attr("isloading", "0");
                if (isfirst == "1") {
                   	 Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#VipCart");
                if (o.code == "200") {
                    $("#VipCart").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
						
						var Listdata = o.content.itemList;
					
						if(Listdata.length > 0 ){
							var tpl = new jSmart(document.getElementById('getShoppingCart').innerHTML);
						
						var returnHtml= tpl.fetch(o.content);

                    if (isfirst != "1") {
                        $("#VipCart").append(returnHtml);
                   	  	
                    } else {
                        $("#VipCart").html(returnHtml);
                      	
                    }
						}else{
							$("#VipCart").html("<div align='center' style='padding:20px;'>没有数据</div>");		
						}
						
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                 alert(o.description);	
                }
            }
        })
	},
	ShopEditCheck:function(o,type){
		if(type == "all"){
			if($(o).hasClass("checkBoxTrue")){
				$(".checkBoxTrue").addClass("checkBoxFalse").removeClass("checkBoxTrue");	
				}else{
				$(".checkBoxFalse").addClass("checkBoxTrue").removeClass("checkBoxFalse");	
			}
		}else{
			if($(o).hasClass("checkBoxTrue")){
				$(o).addClass("checkBoxFalse").removeClass("checkBoxTrue");	
				$("#checkBoxALLcc").addClass("checkBoxFalse").removeClass("checkBoxTrue")
				}else{
				$(o).addClass("checkBoxTrue").removeClass("checkBoxFalse");	
			}	
		}
	},
	ShopEditPP:function(o){
			var getRel  = $(o).attr("rel");
			if(getRel == "0"){
				$(o).attr("rel","1");
				$(".cartTD4").addClass("cartTD4ADDClass");
				$(".cartTD3").hide();
					$(".cartTD5").show();
			}else{
				$(o).attr("rel","0");
				$(".cartTD4").removeClass("cartTD4ADDClass");
				$(".cartTD3").show();
					$(".cartTD5").hide();
			}
		},
	getItemExchangeList:function(isfirst){
		if (isfirst == "1") {
            $("#PPgetItemExchangeList").attr("page", "1");
        }
		var  itemName = '';
	
        $("#PPgetItemExchangeList").attr("isloading", "1");
        var page = $("#PPgetItemExchangeList").attr("page");
	
        var jsonarr = { 'action': "getItemExchangeList", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'page': page, "pageSize": 12} }) };
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
                    Win.Loading('', "#PPgetItemExchangeList");
                }

            },
            dataType: 'json',
            success: function (o) {
               
                $("#PPgetItemExchangeList").attr("isloading", "0");
                if (isfirst == "1") {
                   	 Win.Loading("CLOSE");
                }
                Win.Loading("CLOSE", "#PPgetItemExchangeList");
                if (o.code == "200") {
                    $("#PPgetItemExchangeList").attr("page", parseInt(page) + 1).attr("isnext", o.content.isNext);
						
						var Listdata = o.content.itemList;
					
						if(Listdata.length > 0 ){
							var tpl = new jSmart(document.getElementById('getItemExchangeList').innerHTML);
						
						var returnHtml= tpl.fetch(o.content);
		
							if (isfirst != "1") {
								$("#PPgetItemExchangeListccc").append(returnHtml);
								
							} else {
								$("#PPgetItemExchangeListccc").html(returnHtml);
								
							}
						}else{
							$("#PPgetItemExchangeList").html("<div align='center' style='padding:20px;'>没有数据</div>");		
						}
						
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                 alert(o.description);	
                }
            }
        })
		},
	DelSKuId:function(id){
		if(!confirm("确定删除吗？")){
				return false; 
			}
		 var jsonarr = { 'action': "setShoppingCart", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'skuId': id, "qty": 0} }) };
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
                  
					$("#CartLi_"+id).remove();
                } else {
                 alert(o.description);	
                }
            }
        })
	},	
	DelOrder:function(id){
		if(!confirm("确定删除吗？")){
				return false; 
			}
		 var jsonarr = { 'action': "setOrderStatus", ReqContent: JSON.stringify({ "common": Base.All(), "special": {'orderId': id, "status": 0} }) };
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
                  
					$("#OrderID_"+id).remove();
                } else {
                 alert(o.description);	
                }
            }
        })
	},
	    setShoppingCart: function(id,sd) {

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
						setTimeout(function() {
							LinkAddCustomerId("vipCart.html")
						},
						100)
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
							var tpl = new jSmart(document.getElementById('CheckOrderDetailcc').innerHTML);
						
						var returnHtml= tpl.fetch(o.content);

                    
                       	 $("#CheckOrderDetail").html(returnHtml);
                 
						}else{
							$("#CheckOrderDetail").html("<div align='center' style='padding:20px;'>没有数据</div>");		
						}
						
                    $(".Menu").css("left", ($(window).width() - Win.W()) / 2);

                } else {
                 alert(o.description);	
                }
            }
        })
	}
}