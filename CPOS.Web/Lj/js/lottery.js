function trim(s){
    if (!s || s == null || typeof(s) == "undefined") 
        return "";
    return ("" + s).replace(/^\s*|\s*$/g, "");
}

function isEmpty(s){
    return trim(s).length == 0
}

function isInt(s){
    if (isEmpty(s)) 
        return false;
    for (var i = 0; i < s.length; i++) {
        if ('1234567890'.indexOf(s.charAt(i)) < 0) {
            return false;
        }
    }
    return true;
}

function getRandom(min, max){
    return parseInt(Math.random() * (max - min + 1) + min);
}

var Lottery = {
    param: [],
    i18n_prize: ["一等奖", "二等奖", "三等奖"],
    prizes: [20, 70, 110, 160, 203, 245, 290, 335],
    prizes0: [0, 2, 4, 6],
    prizes1: [1],
    prizes2: [7],
    prizes3: [3, 5],
    prizeTitles: ["不要灰心", "一等奖", "谢谢参与", "三等奖", "要加油哦", "三等奖", "再接再厉", "二等奖"],
    canPlay: false,
    isRotating: false,
    result: "",
    url: "http://o2oapi.aladingyidong.com/Lj/Interface/PrizesData.aspx",
    initData: function () {
        //S("openId", "oUcanjhJDSgCPVYlsmq3XB7EGebQ");
        //S("userId", "dc6c4352c96849ef80b981f52a6c4b18");
        /*
		var jsonarr = { 'action': "getEventPrizesBySales", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": getParam("eventId")} }) };
        var jsonarr = {
			'action':'getEventPrizesBySales',
			'ReqContent':JSON.stringify({
				'common':{"locale":"zh","userId":"00193aeff94341a1a8f64e224c7c249c","openId":"oUcanjuyG9hSplcZ8mSTBg-0airY","customerId":"e703dbedadd943abacf864531decdac1"},
				'special':{'eventId':'BFC41A8BF8564B6DB76AE8A8E43557BA','applicationId':'386D08D106C849A9ACAA6E493D23E853'}
			})
		}
		*/
		var openId = getParam("openId");
		if (openId == null || openId == "") {
			openId = Base.All().openId;
		}
		var customerId = getParam("customerId");
		if (customerId == null || customerId == "") {
			customerId = Base.All().customerId;
		}
		var userId = getParam("userId");
		if (userId == null || userId == "") {
			userId = Base.All().userId;
		}
		var eventId = getParam("eventId")
		//alert(userId);
		if (openId == null || openId == undefined || openId.length == 0) {
			location.href = "http://o2oapi.aladingyidong.com/OnlineClothing/boot/index.html";
		}
		var jsonarr = { 'action': "getEventPrizesBySales", ReqContent: JSON.stringify({ "common": { customerId: customerId, openId: openId, userId: userId }, "special": { "Longitude": "0.0", "Latitude": "0.0", eventId: eventId} }) };
        var that = this;  //alert($(window).width()+"#"+$(document).width());
        $.ajax({
            type: "get",
            dataType: "json",
            url: '/OnlineShopping/data/data.aspx',
            data: jsonarr, //"r=" + Math.random(),
            beforeSend: function () {
                Win.Loading();
				$("#content").hide();
				$(".overlay").css({ 'display': 'block', 'opacity': '0.8' });
                $(".showbox").stop(true).animate({ 'margin-top': '100px', 'opacity': '1' }, 0);
            },
            success: function (data) {
                that.param = data.content;
                that.initPrizeParam();
				
				$("#content").show();
				$(".showbox").stop(true).animate({ 'margin-top': '50px', 'opacity': '0' }, 0);
                $(".overlay").css({ 'display': 'none', 'opacity': '0' });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    },
    initPrizeParam: function () {
        var that = this, o = that.param;
        this.result = o;
        var p = o.prizes;
        var s = '';
        for (var i = 0; i < p.length; i++) {
            s += '<li><img src="' + p[i].imageUrl + '"><h3><b>' + that.i18n_prize[i] + '</b> （' + p[i].countTotal + '名）</h3>';
            s += '<p><span class=prizename>' + p[i].prizeName + '</span><br>' + p[i].prizeDesc + '</p><div class=dot></div></li>';
        }
        $('#p_prizelist').html(s);
		
        if (o.isWinning == "1") {
            that.canPlay = true;
			$("#changes").html('1');
			$('#dibuword').html('');
            if (o.winningDesc) {
                o.prizeStatus = parseInt(o.winningValue);
                if (o.winningDesc == '一等奖') { //一等奖
                    o.prizeIndex = that.prizes1[getRandom(0, that.prizes1.length - 1)];
                }
                else
                    if (o.winningDesc == '二等奖') { //二等奖
                        o.prizeIndex = that.prizes2[getRandom(0, that.prizes2.length - 1)];
                    }
                    else
                        if (o.winningDesc == '三等奖') { //三等奖
                            o.prizeIndex = that.prizes3[getRandom(0, that.prizes3.length - 1)];
                        }
                        else {
                            o.prizeIndex = that.prizes0[getRandom(0, that.prizes0.length - 1)];
                        }
            }
            else {
                o.prizeIndex = getRandom(0, that.prizes0.length - 1);
            }
            o.prizeDegree = that.prizes[o.prizeIndex];
            that.initEvent();
        }
        else {
			$("#changes").html('0');
            $("#p_start").bind('click',function(){
				
				that.doAlert({
					action: 'show',
					title: "每天只有一次摇奖机会哦，请明天再试，或参与<a href='http://o2oapi.aladingyidong.com/lj/howto_recommend.html'>推荐有礼</a>领取更多惊喜"
					, body: "  <a href='javascript:void(0)' onclick=\"Lottery.doAlert({action:'hide'})\" class=qianlanse>确认</a>"
				});
			});
			$("#dibuword").html("每天只有一次摇奖机会哦，请明天再试，或参与“推荐有礼”领取更多惊喜");
        }
    },
    saveLottery: function (p) {
        var jsonarr = { 'action': "setEventPrizes", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": getParam("eventId")} }) };
		// var jsonarr = {
		// 	'action':'',
		// 	'ReqContent':{
		// 		'common':{"locale":"zh","userId":"00193aeff94341a1a8f64e224c7c249c","openId":"oUcanjuyG9hSplcZ8mSTBg-0airY","customerId":"e703dbedadd943abacf864531decdac1"},
		// 		'special':{'eventId':'BFC41A8BF8564B6DB76AE8A8E43557BA','applicationId':'386D08D106C849A9ACAA6E493D23E853'}
		// 	}
		// }
		$("#changes").html('0');
        var that = this;
        $.ajax({
            type: "get",
            dataType: "json",
            url: Lottery.url,
            data: jsonarr, //"r=" + Math.random(),
            success: function (data) {
                that.canPlay = false;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    },
    checkAccess: function () {
        var that = this;
        if (!that.canPlay) {
			$("#changes").html('0');
			$('#dibuword').html('每天只有一次摇奖机会哦，请明天再试，或参与“推荐有礼”领取更多惊喜');
            that.doAlert({
                action: 'show',
                title: "每天只有一次摇奖机会哦，请明天再试，或参与<a href='http://o2oapi.aladingyidong.com/lj/howto_recommend.html'>推荐有礼</a>领取更多惊喜"
                , body: "  <a href='javascript:void(0)' onclick=\"Lottery.doAlert({action:'hide'})\" class=qianlanse>确认</a>"
            });
            return false;
		}
        return true;
    },
    initEvent: function () {
        var that = this;
        if (!that.checkAccess()) {
            return;
        }
        $("#p_start").rotate({
            bind: {
                click: function () {
                    if (that.isRotating || !that.checkAccess()) {
                        return;
                    }
                    that.canPlay = false;
                    that.isRotating = true;
                    $("#p_disk").rotate({
                        duration: 3000,
                        angle: 0,
                        animateTo: 1440 + that.param.prizeDegree,
                        easing: $.easing.easeOutSine,
                        callback: function () {
                            that.saveLottery();
                            Lottery.fnBindRecommender();
                            that.doAlert({
                                action: 'show',
                                title: Lottery.result.resultMessage,
                                body: "  <a href='javascript:void(0)' onclick=\"Lottery.doAlert({action:'hide'})\" class=lanse>确认</a>"
                            });
                            that.isRotating = false;
							
							/*
                            var jsonarr = { 'action': "getEventPrizes", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": getParam("eventId")} }) };

                            $.ajax({
                                type: "get",
                                dataType: "json",
                                url: Lottery.url,
                                data: jsonarr,
                                success: function (data) {

                                    if (data.content && data.content.isLottery) {

                                        that.canPlay = true;
                                    }
                                }
                            });
							*/
                        }
                    });
                }
            }
        });
    },
    doAlert: function (o) {
        if (o.action == 'show') {
            $('#p_alert_title').html(o.title);
            $('#p_alert_body').html(o.body);
            $("#p_alert").css("display", "block").css("left", (($(window).width()) / 2 - (parseInt($("#p_alert").width()) / 2)) + "px").css("top", (($(window).height()) / 2 - (parseInt($("#p_alert").height()) / 2) - 50) + "px");
        }
        else
            if (o.action == 'hide') {
                //location.href = document.URL;
                $("#p_alert").hide();
            }
    },
    doInit: function () {
        if (window.location.pathname.toLowerCase().indexOf("guaguaorderlj1.html") > -1) {
            this.initData();

        }
    },
    fnBindRecommender: function () {
        var jsonarr = { 'action': "bindRecommender", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "recommender": WeiXin.GetCookie("recommenderOpenId")} }) };
        //alert(WeiXin.GetCookie("recommenderOpenId"));
        //console.log(jsonarr);
        $.ajax({
            type: 'post',
            url: Lottery.url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                //Win.Loading();
            },
            dataType: 'json',
            success: function (o) {
            }

        });
    }
};

$(document).ready(function () {
    Lottery.doInit();
});