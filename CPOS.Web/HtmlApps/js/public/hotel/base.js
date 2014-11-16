try{
Zepto=Zepto?Zepto:jQuery;
}catch(EX){
Zepto=jQuery;
}
!function (a, b) { function c(a) { return a.replace(/([a-z])([A-Z])/, "$1-$2").toLowerCase() } function d(a) { return e ? e + a : a.toLowerCase() } var e, f, g, h, i, j, k, l, m, n, o = "", p = { Webkit: "webkit", Moz: "", O: "o" }, q = window.document, r = q.createElement("div"), s = /^((translate|rotate|scale)(X|Y|Z|3d)?|matrix(3d)?|perspective|skew(X|Y)?)$/i, t = {}; a.each(p, function (a, c) { return r.style[a + "TransitionProperty"] !== b ? (o = "-" + a.toLowerCase() + "-", e = c, !1) : void 0 }), f = o + "transform", t[g = o + "transition-property"] = t[h = o + "transition-duration"] = t[j = o + "transition-delay"] = t[i = o + "transition-timing-function"] = t[k = o + "animation-name"] = t[l = o + "animation-duration"] = t[n = o + "animation-delay"] = t[m = o + "animation-timing-function"] = "", a.fx = { off: e === b && r.style.transitionProperty === b, speeds: { _default: 400, fast: 200, slow: 600 }, cssPrefix: o, transitionEnd: d("TransitionEnd"), animationEnd: d("AnimationEnd") }, a.fn.animate = function (c, d, e, f, g) { return a.isFunction(d) && (f = d, e = b, d = b), a.isFunction(e) && (f = e, e = b), a.isPlainObject(d) && (e = d.easing, f = d.complete, g = d.delay, d = d.duration), d && (d = ("number" == typeof d ? d : a.fx.speeds[d] || a.fx.speeds._default) / 1e3), g && (g = parseFloat(g) / 1e3), this.anim(c, d, e, f, g) }, a.fn.anim = function (d, e, o, p, q) { var r, u, v, w = {}, x = "", y = this, z = a.fx.transitionEnd, A = !1; if (e === b && (e = a.fx.speeds._default / 1e3), q === b && (q = 0), a.fx.off && (e = 0), "string" == typeof d) w[k] = d, w[l] = e + "s", w[n] = q + "s", w[m] = o || "linear", z = a.fx.animationEnd; else { u = []; for (r in d) s.test(r) ? x += r + "(" + d[r] + ") " : (w[r] = d[r], u.push(c(r))); x && (w[f] = x, u.push(f)), e > 0 && "object" == typeof d && (w[g] = u.join(", "), w[h] = e + "s", w[j] = q + "s", w[i] = o || "linear") } return v = function (b) { if ("undefined" != typeof b) { if (b.target !== b.currentTarget) return; a(b.target).unbind(z, v) } else a(this).unbind(z, v); A = !0, a(this).css(t), p && p.call(this) }, e > 0 && (this.bind(z, v), setTimeout(function () { A || v.call(y) }, 1e3 * e + 25)), this.size() && this.get(0).clientLeft, this.css(w), 0 >= e && setTimeout(function () { y.each(function () { v.call(this) }) }, 0), this }, r = null } (Zepto);
//zepto动画fx模块

//公用导航
var TopMenuHandle = {
    BindScrollEvent: function () { //绑定滚动事件
        var navPane = $('.commonHeader'), curWindow = $(window);
        curScrollTop = curWindow.scrollTop(), eventTop = 1, stime = '';
        curWindow.scroll(function () {
            eventTop = parseInt(curWindow.scrollTop());
            if (eventTop <= 60) { //如果当前滚动条小于60像素不执行任何效果。
                navPane.css('top', 0);
                clearTimeout(stime);
                return false;
            };
            eventTop = curWindow.scrollTop();
            clearTimeout(stime);
            stime = setTimeout(function () {
                if (curScrollTop > eventTop) {
                    navPane.animate({
                        top: '0' //正常显示
                    }, 150);
                } else {
                    navPane.animate({
                        top: '-3.5em' //往上移动大小
                    }, 150);
                }
                curScrollTop = curWindow.scrollTop();
                eventTop = curWindow.scrollTop();
            }, 100); //滚动条停止100毫秒后触发事件
        });
    }
};


//分享图片地址
var defaultShareImage = 'http://o2oapi.aladingyidong.com/HtmlApps/images/special/bollssom/alading.png';



//微信分享操作
var WeiXinShare = {
    appId: '',
    imageUrl: defaultShareImage, //分享图片地址
    link: document.URL, //分享地址
    title: document.title, //分享标题
    desc:"花间堂酒店预定", //分享内容，如果没有设置会取当前连接地址
    isComplate: false, //WeixinJSBridge对象是否已经完成加载
    init: function() {
        var self = this;
        if (typeof(WeixinJSBridge) == "undefined") {
            return false;
        };
        
        function shareEvent() {
            self.isComplate = true;
            WeixinJSBridge.on("menu:share:appmessage", function(s) {
  
            setTimeout(function(){
                WeixinJSBridge.invoke("sendAppMessage", {
                    appid: self.appId,
                    img_url: self.imageUrl,
                    img_width: "640",
                    img_height: "640",
                    link: self.link,
                    desc: self.desc || self.link,
                    title: self.title
                }, function(e) {});
                    },100);
            }),
            WeixinJSBridge.on("menu:share:timeline", function(e) {
                    
                WeixinJSBridge.invoke("shareTimeline", {
                    img_url: self.imageUrl,
                    img_width: "640",
                    img_height: "640",
                    link: self.link,
                    desc: self.desc || self.link,
                    title: self.title
                }, function(e) {});
            });
            WeixinJSBridge.on("menu:share:weibo", function(e) {
        
                WeixinJSBridge.invoke("shareWeibo", {
                    content: self.title,
                    url: self.link
                }, function(e) {});
            }), WeixinJSBridge.on("menu:share:facebook", function(e) {
                WeixinJSBridge.invoke("shareFB", {
                    img_url: self.imageUrl,
                    img_width: "640",
                    img_height: "640",
                    link: self.link,
                    desc: self.desc || self.link,
                    title: self.title
                }, function(e) {});
            }), WeixinJSBridge.on("menu:general:share", function(s) {
                
                s.generalShare({
                    appid: self.sid,
                    img_url: self.imageUrl,
                    img_width: "640",
                    img_height: "640",
                    link: self.link,
                    desc: self.desc || self.link,
                    title: self.title
                }, function(e) {});
            });
        }
        typeof WeixinJSBridge == "undefined" ?
            document.addEventListener ?
            document.addEventListener("WeixinJSBridgeReady", shareEvent, !1) :
            document.attachEvent && (document.attachEvent("WeixinJSBridgeReady", shareEvent),
                document.attachEvent("onWeixinJSBridgeReady", shareEvent)) :
            shareEvent();
    }
};

function htmlDecode(e) {
    return e.replace(/&#39;/g, "'").replace(/<br\s*(\/)?\s*>/g, "\n").replace(/&nbsp;/g, " ").replace(/&lt;/g, "<").replace(/&gt;/g, ">").replace(/&quot;/g, '"').replace(/&amp;/g, "&");
}






(function ($, undefined) {
    //绑定导航事件
    TopMenuHandle.BindScrollEvent();



//微信分享初始化,时间控制微信API是否正常加载
    /*
    WeiXinShare.isComplate = false;
    var weiXintimer = 0;
    weiXintimer = window.setInterval(function() {
        WeiXinShare.init();
        if (WeiXinShare.isComplate) {
            clearInterval(weiXintimer);
        };
    }, 30);
    */
})(Zepto)
