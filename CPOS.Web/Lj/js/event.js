

var Event = {
    EventList: function () {

        var jsonarr = { 'method': "getEventApplyQues", ReqContent: JSON.stringify({ "common": Base.All(), "special": { 'eventId': getParam("eventId"), "status": getParam("dataFrom")} }) };
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
                    $("#imageURL").html('<img src="' + o.content.imageUrl + '" width="100%">');
                    var tpl = _.template($("#EventScript").html(), o.content)
                    $("#ConnextID").html(tpl);
                    

                } else {
                    alert(o.description);
                }
            }
        })
    },
    ClickCheckBox: function (o) {
        var getRel = $(o).find(".checkBox").attr("rel");
        if (getRel == "3") {
            return false;
        }

        if (getRel == "1") {
            $(o).find(".checkBox").attr("rel", "0");
            $(o).find(".checkBox").find("img").attr("src", "images/icn3.jpg");
            $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text()) - 1);
        } else {
            $(o).find(".checkBox").attr("rel", "1");
            $(o).find(".checkBox").find("img").attr("src", "images/icn2.jpg");
            $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text()) + 1);
        }
    },
    ClickRudioBox: function (o, id) {
        var getRel = $(o).find(".checkBox").attr("rel");
        if (getRel == "3") {
            return false;
        }
        var i = 0;
        $(".AUU_" + id).each(function () {
            if ($(this).attr("rel") == "1") {
                i++;
            }
            $(this).attr("rel", "0");
            $(this).find("img").attr("src", "images/icn9.jpg");
        });
        if (getRel == "1") {
            $(o).find(".checkBox").attr("rel", "0");
            $(o).find(".checkBox").find("img").attr("src", "images/icn9.jpg");
            // $("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text())-i);
        } else {
            $(o).find(".checkBox").attr("rel", "1");
            $(o).find(".checkBox").find("img").attr("src", "images/icn7.jpg");
            //$("#HasSelectNum,#HasSelectNum1").text(parseInt($("#HasSelectNum").text())+1-i);
        }
    },
    SfSelect: function () {
        var i = 0;
        var eventList = [];

        $("div[id^=QuestionAbc_]").each(function () {

            var getVt = $(this).attr("value"), sObj = {};
            sObj.questionId = getVt;
            sObj.isSaveOutEvent = $(this).attr("issaveoutevent");
            sObj.cookieName = $(this).attr("cookiename");

            sObj.questionValue = '';
            var answerid = '', other = '';
            $(this).find(".checkBox").each(function () {
                if ($(this).attr("rel") == "1") {
                    answerid += $(this).attr("eventId") + ",";
                    //eventList.push({"questionId":$(this).attr("eventId")});
                    i++;

                }
            });
            if (answerid != "") {
                answerid = answerid.substring(0, answerid.length - 1);
                sObj.questionValue = answerid;
            }
            eventList.push(sObj);
        });
        if (i == 0) {
            var oString = '';
            if ($(".Lang").attr("lang") == "zh") {
                oString = "Select at least one";
            } else {
                oString = "至少选择一项";
            }
            alert(oString);
            return false;
        }
        return eventList;
    },
    Dis: function () {
        var getWinw = $(window).width(), getDomH = $(document).height(), getWinH = $(window).height();
        $("#appbg").css("height", getDomH);
        $("#appbg").css("width", getWinw);
        var getDocumentSH = $(document).scrollTop();
        var t = getDocumentSH + ((getWinH - 280) / 2), l = (getWinw - 280) / 2;
        $("#appbgBox").css({ "left": l, "top": t });
        $("#appbg,#appbgBox").show();
    },
    SelectDiv: function () {
        if (this.SfSelect() != false) {
            this.SureSubmit();
        }
    },
    CacelBox: function () {
        $("#appbg,#appbgBox").hide();
    },
    SureSubmit: function () {

        var eventObj = this.SfSelect();
        if (eventObj == false) {
            return false;
        }
        var jsonarr = { Form: JSON.stringify({ "common": Base.All(), "special": { "eventId": getParam("eventId"), questions: eventObj, userName: '', mobile: "", email: ""} }) };
        $.ajax({
            type: 'post',
            url: url + "?method=submitEventApply",
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                Win.Loading();
            },
            dataType: 'json',
            success: function (o) {
                Win.Loading("CLOSE");
                alert(o.description);
                if (o.code == "200") {
                    $("#ConnextID").hide();
                    $("#ChessTip").show();
                }
            }
        })
    },
    Lang: function (o) {
        var getlang = $(o).attr("lang");

        if (getlang == "en") {
            $(o).text("中文");
            $(".zh").hide();
            $(".en").show();
            $(o).attr("lang", "zh");
        } else {
            $(o).text("English");
            $(".en").hide();
            $(".zh").show();
            $(o).attr("lang", "en");
        }
    }
}