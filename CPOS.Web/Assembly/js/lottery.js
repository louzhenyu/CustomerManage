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
    url: {
        load: "js/data2.js",
        save: "js/data2.js?id={1}&"
    },
    initData: function(){
        var that = this;  //alert($(window).width()+"#"+$(document).width());
        $.ajax({
            type: "get",
            dataType: "json",
            url: that.url.load,
            data: "r=" + Math.random(),
            success: function(data){
                that.param = data[0].content;
                that.initPrizeParam();
            },
            error: function(XMLHttpRequest, textStatus, errorThrown){
                alert(errorThrown);
            }
        });
    },
    initPrizeParam: function(){
        var that = this, o = that.param;
        if (o.isLottery == "1") {
            that.canPlay = true;
            if (isInt(o.isWinning)) {
                o.prizeStatus = parseInt(o.isWinning);
                if (o.prizeStatus == 1) { //一等奖
                    o.prizeIndex = that.prizes1[getRandom(0, that.prizes1.length - 1)];
                }
                else 
                    if (o.prizeStatus == 2) { //二等奖
                        o.prizeIndex = that.prizes2[getRandom(0, that.prizes2.length - 1)];
                    }
                    else 
                        if (o.prizeStatus == 3) { //三等奖
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
            var o = o.prizeList;
            var s = '';
            for (var i = 0; i < o.length; i++) {
                s += '<li><img src="' + o[i].imageUrl + '"><h3><b>' + that.i18n_prize[i] + '</b> （' + o[i].countTotal + '名）</h3>';
                s += '<p><span class=prizename>'+o[i].prizeName+'</span><br>' + o[i].prizeDesc + '</p><div class=dot></div></li>';
            }
            $('#p_prizelist').html(s);
            that.initEvent();
        }
        else {
            if (!that.checkAccess()) {
                return;
            }
        }
    },
    saveLottery: function(p){
        var that = this;
        $.ajax({
            type: "get",
            dataType: "json",
            url: that.url.save,
            data: "r=" + Math.random(),
            success: function(data){
                
            },
            error: function(XMLHttpRequest, textStatus, errorThrown){
                alert(errorThrown);
            }
        });
    },
    checkAccess: function(){
        var that = this;
        if (!that.canPlay) {
            that.doAlert({
                action: 'show',
                title: "您已经没有抽奖机会了。",
                body: "  <a href='#' onclick=\"Lottery.doAlert({action:'hide'})\" class=qianlanse>确认</a>"
            });
            return false;
        }
        return true;
    },
    initEvent: function(){
        var that = this;
        if (!that.checkAccess()) {
            return;
        }
        $("#p_start").rotate({
            bind: {
                click: function(){
                    if (that.isRotating || !that.checkAccess()) {
                        return;
                    }
                    that.canPlay = false;
										that.isRotating=true;
                    $("#p_disk").rotate({
                        duration: 3000,
                        angle: 0,
                        animateTo: 1440 + that.param.prizeDegree,
                        easing: $.easing.easeOutSine,
                        callback: function(){
                            //document.title = ('中奖了！' + that.param.currentPrize);
                            var p = {
                                index: that.param.prizeIndex,
                                title: that.prizeTitles[that.param.prizeIndex]
                            };
                            that.saveLottery(p);
                            that.doAlert({
                                action: 'show',
                                title: p.title,
                                body: "  <a href='#' onclick=\"Lottery.doAlert({action:'hide'})\" class=lanse>确认</a>"
                            });
														that.isRotating=false;
                        }
                    });
                }
            }
        });
    },
    doAlert: function(o){
        if (o.action == 'show') {
            $('#p_alert_title').html(o.title);
            $('#p_alert_body').html(o.body);
            $("#p_alert").css("display", "block").css("left", (($(window).width()) / 2 - (parseInt($("#p_alert").width()) / 2)) + "px").css("top", (($(window).height()) / 2 - (parseInt($("#p_alert").height()) / 2)-50) + "px");
        }
        else 
            if (o.action == 'hide') {
                $("#p_alert").hide();
            }
    },
    doInit: function(){
        this.initData();
    }
};

$(document).ready(function(){
    Lottery.doInit();
});

