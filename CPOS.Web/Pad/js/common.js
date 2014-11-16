


var Timeout = null;
function IsPC() {
    var userAgentInfo = navigator.userAgent;

    var Agents = new Array("Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod");
    var flag = true;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
        }
    }
    return flag;
}
function IsIEVersion() {

    var userAgentInfo = navigator.appVersion;
    if (userAgentInfo.indexOf("MSIE 6.0") > 0) {
        return 6;
    }
    if (userAgentInfo.indexOf("MSIE 7.0") > 0) {
        return 7;
    }
    if (userAgentInfo.indexOf("MSIE 8.0") > 0) {
        return 8;
    };
    if (userAgentInfo.indexOf("MSIE 9.0") > 0) {
        return 9;
    };
    return 0;
}



function getParam(para) {
    //获得html上的参数
    querystr = window.location.href.split("?");
    var iparam = "",
    tmp_arr = [];
    if (querystr[1]) {
        var GET1s = querystr[1].split("&");
        for (i = 0; i < GET1s.length; i++) {
            tmp_arr = GET1s[i].split("=");
            if (para == tmp_arr[0]) {
                iparam = tmp_arr[1];
            }
        }

    }
    return iparam;
}




function G(param) {

    var customerId = getParam("customerId"), abc = [], returnData = "", ob = false;
    if (customerId != "") {
        var BaseCookie = WeiXin.GetCookie("base");
        if (BaseCookie != "") {
            var san = JSON.parse(BaseCookie);
            if (san.length > 0) {
                for (var i = 0; i < san.length; i++) {
                    if (customerId == san[i].customerId) {
                        returnData = eval("san[i]." + param);
                        ob = true;
                    }
                }
                if (ob == false) {
                    abc = san;
                }
            }

            if (ob == true) {
                return returnData;
            }
        }
        var iAPi = { customerId: getParam("customerId"), openId: newGuid, userId: newGuid, islogin: "0" };
        abc.push(iAPi);
        var ist = JSON.stringify(abc);
        WeiXin.SetCookie("base", ist, 24 * 30 * 360 * 120, '/');

        return eval("iAPi." + param);
    }
    return "";
}


var Base = {
    //公共类的获得
    openId: function () {


        return getParam("openId");

    },
    userId: function () {

        return getParam("userId");

    },
    customerId: function () {
        var customerId = getParam("customerId");
        return customerId;
    },
    locale: function () {
        return 'zh';
    },
    All: function () {

        return {
            "locale": this.locale(),
            "userId": this.userId(),
            "openId": this.openId(),
            "customerId": this.customerId()
        }
    }
}
var Win = {
    Loading: function (type, id) {
        if (Timeout) {
            clearTimeout(Timeout);
        }
        if (type == "CLOSE" && id) {
            $(id).find(".cLoading").remove();
            return;
        }
        if (id) {
            $(id).find(".cLoading").remove();
            $(id).append('<div align=center style="padding:14px;" class="cLoading"><img width="30" height="30" src="images/382.gif"></div>');
            return;
        }
        $("#BottomMenu").hide();

        if (type == "CLOSE") {
            $(".loading").hide();

            return false;
        }
        var getWindowsWidth = $(window).width(),
        getWindowsHeight = $(window).height(),
        dst = $(document).scrollTop();
        $(".loading").css({
            left: (getWindowsWidth - $(".loading").width()) / 2,
            top: ((getWindowsHeight - $(".loading").height()) / 2) + dst
        });
        $(".loading").show();
    },
    W: function () {
        return $("#PcBox").width();
    },
    H: function () {
        return $("#PcBox").height();
    }
}
function GetOrder(type) {
    var status = type ? type : "1";
    var jsonarr = { 'action': "getOnlinePosOrder", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'storeId': getParam("storeId"), status: status} }) };
    $.ajax({
        type: 'get',
        url: "/OnlineShopping/data/Data.aspx",
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
                o.content.status = status;
                var tpl = _.template($("#OrderScript").html(), o.content)
                $("#ConnextID").append(tpl);

            } else {
                alert(o.description);
            }
        }
    })
}
function DelOrder(orderId) {

    var jsonarr = { 'action': "setOnlinePosOrderStatus", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'orderId': orderId} }) };
    $.ajax({
        type: 'get',
        url: "/OnlineShopping/data/Data.aspx",
        data: jsonarr,
        timeout: 90000,
        cache: false,
        beforeSend: function () {
            $("#Load_" + orderId).html('操作中');
        },
        dataType: 'json',
        success: function (o) {
            if (o.code == "200") {
                $("#Load_" + orderId).html('操作完成');
                setTimeout(function () { $("#order_" + orderId).remove() }, 300);
            } else {
                $("#Load_" + orderId).html('操作失败');
                setTimeout(function () { $("#Load_" + orderId).text("完成") }, 300);
            }
        }
    })
}

function SetUserMessageDataByWap(toVipType, text) {
    var jsonarr = { 'action': "setUserMessageDataWap", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'toVipType': toVipType, "text": text} }) };
    $.ajax({
        type: 'get',
        url: "/OnlineShopping/Data/Data.aspx",
        data: jsonarr,
        timeout: 90000,
        cache: false,
        beforeSend: function () {
           
        },
        dataType: 'json',
        success: function (o) {
            if (o.code == "200") {
                alert(o.content);
            } else {
                alert(o.content);
            }
        }
    })
}
