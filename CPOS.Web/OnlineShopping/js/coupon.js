// JavaScript Document
url: "data/data.aspx";
var Coupon = {
	getCouponList: function () {
		//WeiXin.SetCookie("userId", '70413b931e6840d8898cfd69c62d3eb6');
		var jsonarr = "";
		if (getParam("orderId"))
			jsonarr = { 'action': "orderCouponList", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "orderId": getParam("orderId")} }) };
		else {
			$(".close").hide();
			jsonarr = { 'action': "myCouponList", ReqContent: JSON.stringify({ "common": Base.All() }) };
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
				//Win.Loading("CLOSE");
				if (o.code == "200") {
					var tpl = new jSmart(document.getElementById('showCouponList').innerHTML, "utf-8");
					var returnHtml = tpl.fetch(o.content);
					$("#couponList").html(returnHtml);
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
				//Win.Loading("CLOSE");
				if (o.code == "200") {
					//if checked
					if ($("#" + id).attr('checked') && !(o.content.result == 1)) {
						alert("该优惠券不可使用");
						$("#" + id).attr('checked', false);
					}
				}
				else {
					alert(o.description);
				}
			}
		})
	}
}