// JavaScript Document

var Coupon = {
	getCouponList: function () {
	    //S("userId", 'add73ef71c2c480c89b5a6941cb0dfc9');
	    //S("islogin", "1");
		var jsonarr = "";
		if (getParam("orderId"))
			jsonarr = { 'action': "orderCouponList", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "orderId": getParam("orderId")} }) };
		else {
			$(".close").hide();
			jsonarr = { 'action': "myCouponList", ReqContent: JSON.stringify({ "common": Base.All() }) };
		}
		//alert(Base.All().userId);
		//console.log(jsonarr);
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
					var tpl = _.template($('#showCouponList').html(), o.content);
					$("#couponList").html(tpl);
					$("#appOpacity").css({ "height": $(document).height() }).show();
					$("#couponPopup").show();
					$("#appOpacity").live('touchmove', function (e) {
					    e.stopPropagation();
					    e.event.preventDefault();
					}) 
				} else {
					alert(o.description);
				}
			}
		})
	}

	, clickCoupon: function (id) {
		var action = "";
		if ($("#" + id).attr('checked'))
			action = "selectCoupon";
		else
			action = "cancelCoupon";

		var jsonarr = { 'action': action, ReqContent: JSON.stringify({ "common": Base.All(), "special": { "orderId": getParam("orderId"), "couponId": $("#" + id).attr("value")} }) };
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
					//if checked
				    if ($("#" + id).attr('checked') && !(o.content.result == 1)) {
				        alert("该优惠券不可使用");
				        $("#" + id).attr('checked', false);
				    }
				    else {
				        var orderId = getParam("orderId") ? getParam("orderId") : "";
				        var totalAmount = $("#totalAmount").text();
				        Vip.getCouponTypeList(orderId, totalAmount, '1');
				    }
				}
				else {
					alert(o.description);
				}
			}
		})
	}

	, ClosePopup: function () {
	    $("#appOpacity,#couponPopup").hide();
	}
}