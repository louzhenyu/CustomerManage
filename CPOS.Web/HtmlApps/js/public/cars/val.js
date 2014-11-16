!function (a, b) { function c(a) { return a.replace(/([a-z])([A-Z])/, "$1-$2").toLowerCase() } function d(a) { return e ? e + a : a.toLowerCase() } var e, f, g, h, i, j, k, l, m, n, o = "", p = { Webkit: "webkit", Moz: "", O: "o" }, q = window.document, r = q.createElement("div"), s = /^((translate|rotate|scale)(X|Y|Z|3d)?|matrix(3d)?|perspective|skew(X|Y)?)$/i, t = {}; a.each(p, function (a, c) { return r.style[a + "TransitionProperty"] !== b ? (o = "-" + a.toLowerCase() + "-", e = c, !1) : void 0 }), f = o + "transform", t[g = o + "transition-property"] = t[h = o + "transition-duration"] = t[j = o + "transition-delay"] = t[i = o + "transition-timing-function"] = t[k = o + "animation-name"] = t[l = o + "animation-duration"] = t[n = o + "animation-delay"] = t[m = o + "animation-timing-function"] = "", a.fx = { off: e === b && r.style.transitionProperty === b, speeds: { _default: 400, fast: 200, slow: 600 }, cssPrefix: o, transitionEnd: d("TransitionEnd"), animationEnd: d("AnimationEnd") }, a.fn.animate = function (c, d, e, f, g) { return a.isFunction(d) && (f = d, e = b, d = b), a.isFunction(e) && (f = e, e = b), a.isPlainObject(d) && (e = d.easing, f = d.complete, g = d.delay, d = d.duration), d && (d = ("number" == typeof d ? d : a.fx.speeds[d] || a.fx.speeds._default) / 1e3), g && (g = parseFloat(g) / 1e3), this.anim(c, d, e, f, g) }, a.fn.anim = function (d, e, o, p, q) { var r, u, v, w = {}, x = "", y = this, z = a.fx.transitionEnd, A = !1; if (e === b && (e = a.fx.speeds._default / 1e3), q === b && (q = 0), a.fx.off && (e = 0), "string" == typeof d) w[k] = d, w[l] = e + "s", w[n] = q + "s", w[m] = o || "linear", z = a.fx.animationEnd; else { u = []; for (r in d) s.test(r) ? x += r + "(" + d[r] + ") " : (w[r] = d[r], u.push(c(r))); x && (w[f] = x, u.push(f)), e > 0 && "object" == typeof d && (w[g] = u.join(", "), w[h] = e + "s", w[j] = q + "s", w[i] = o || "linear") } return v = function (b) { if ("undefined" != typeof b) { if (b.target !== b.currentTarget) return; a(b.target).unbind(z, v) } else a(this).unbind(z, v); A = !0, a(this).css(t), p && p.call(this) }, e > 0 && (this.bind(z, v), setTimeout(function () { A || v.call(y) }, 1e3 * e + 25)), this.size() && this.get(0).clientLeft, this.css(w), 0 >= e && setTimeout(function () { y.each(function () { v.call(this) }) }, 0), this }, r = null } (jQuery);
//zepto动画fx模块

//验证用户信息
var valKeyList = {
    userId: '_userId'
};

//汉字及字母长度一致获取字符长度
String.prototype.len = function() {
    var l = 0;
    var a = this.split("");
    for (var i = 0; i < a.length; i++) {
        if (a[i].charCodeAt(0) < 299) {
            l++;
        } else {
            l += 2;
        }
    }
    return l;
};

//汉字及字母长度一致截断字符
String.prototype.cut = function(len, substr) {

    if (this.len() <= len) return this;
    sl = substr.len();
    var sb = new StringBuilder();
    var l = 0;
    var a = this.split("");
    for (var i = 0; i < a.length; i++) {
        if (l >= len - sl) break;
        if (a[i].charCodeAt(0) < 299) {
            l++;
        } else {
            l += 2;
        }
        if (l > len) sb.append(" ");
        else sb.append(a[i]);
    }
    sb.append(substr);
    return sb.toString();
};

//微商城公用类

var KeyList = {
  cartCount: 'cartCount',
  val:'val',
  userId:Jit.AM.getAppVersion().APP_CUSTOMERID+'_userId'
}, ClientSession = { //当前客户的session对象
    get: function(k) {
    return   Jit.AM.getAppSession(Jit.AM.getAppVersion(), k);
    },
    set: function(k, v) {
     return  Jit.AM.setAppSession(Jit.AM.getAppVersion(), k, v);
    }
  };
//拼接字符串，该方法效率要高于str+="str";
function StringBuilder() {
  this.strList = [];
  this.append = function(v) {
    if (v) {
      this.strList.push(v);
    };
  };
  this.appendFormat = function(v) {
    if (v) {
      if (arguments.length > 1) {
        for (var i = 1; i <arguments.length; i++) {
                var Rep = new RegExp("\\{" + (i - 1) + "\\}", "gi");
                v = v.replace(Rep, arguments[i]);
        };
      }
      this.strList.push(v);
    };
  };
  this.toString = function() {
    return this.strList.join('');
  };
}


//验证是否是手机浏览器
function IsMobileBrowser() {
  var hash = window.location.hash,
    result = false;
  if (!hash.match("fromapp")) {
    result = navigator.userAgent.match(/(iPhone|iPod|Android|ios)/i);
  }
  return result;
}


// 验证是否是手机号码
function IsMobileNumber(n){
return  /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/.test(n);

}












var Validates = {
    hasPageList: ['UnionSchool'], //需要验证的列表页面
    isLogin: function() { //验证用户是否登陆
        var me = Jit.AM,
            baseInfo = me.getBaseAjaxParam();
        return (baseInfo && baseInfo.userId) ? true : false;
    },
    Customer:function(){//验证客户身份
        if (Jit.AM.getBaseAjaxParam() == null || Jit.AM.getBaseAjaxParam().customerId == null) { //如¨?果?userId不?为a空?，ê?则¨°表À¨ª示º?缓o存ä?已°?有®D基¨´础ä?数ºy据Y，ê?如¨?果?无T，ê?则¨°需¨¨要°a给?值¦Ì
            if (Jit.AM.getUrlParam('customerId') != null && Jit.AM.getUrlParam('customerId') != "") {
                Jit.AM.setBaseAjaxParam({
                    "customerId": Jit.AM.getUrlParam('customerId'),
                    "userId": "",
                    "openId": ""
                });
            }
        }

    },
    Status: function() { //验证当前用户浏览权限


        var me = Jit.AM,
            self = this,
            baseInfo = me.getBaseAjaxParam();
        if (!baseInfo.roleId || baseInfo.roleId == null) {
            baseInfo.roleId = 'CE1FF5BE7B6A4B0E8ACF5E2501B43D78'; //设置默认的角色ID
            Jit.AM.setBaseAjaxParam(baseInfo);
        };


        if (baseInfo && baseInfo.userId) {
            return false;
        };

        var userId = localStorage.getItem(valKeyList.userId);
        if (userId) {
            baseInfo.userId = userId;
            Jit.AM.setBaseAjaxParam(baseInfo);
            return false;
        };

    
    },
    isEmail: function(v) { //邮箱
        return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(v);
    },
    isPhone: function(v) { //电话
        return /^([0\+]\d{2,3}-?)?(0\d{2,3}-?)?(\d{7,8})([- ]+\d{1,6})?$/.test(v);
    },
    isMobile: function(v) { //手机
        return /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/.test(v);
    },
    isPostCode: function(v) { //邮政编码
        return /^\d{6}$/.test(v);
    },
    isIdCard: function(v) { //身份证
        //15位数身份证正则表达式
        var regShort = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
        //18位数身份证正则表达式
        var regLong = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/;
        if (str.match(regShort) == null && str.match(regLong) == null) {
            return false;
        } else {
            return true;
        }
        return regLong.test(v) || regShort.test(v);
    },
    isUrl: function(v) { //Url地址
        return /(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/.test(v);
    },
    isInteger: function(v) { //整数

        return /^[-]{0,1}[0-9]{1,}$/.test(v);
    },
    isDecimal: function(v) { //验证整数及浮点数
        if (isInteger(v)) return true;
        var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
        if (re.test(v)) {
            if (RegExp.$1 == 0 && RegExp.$2 == 0) return false;
            return true;
        } else {
            return false;
        }
    },
    isDate: function(date, fmt) { //验证日期格式
        if (fmt == null) fmt = "yyyyMMdd";
        var yIndex = fmt.indexOf("yyyy");
        if (yIndex == -1) return false;
        var year = date.substring(yIndex, yIndex + 4);
        var mIndex = fmt.indexOf("MM");
        if (mIndex == -1) return false;
        var month = date.substring(mIndex, mIndex + 2);
        var dIndex = fmt.indexOf("dd");
        if (dIndex == -1) return false;
        var day = date.substring(dIndex, dIndex + 2);
        if (!isNumber(year) || year > "2100" || year < "1900") return false;
        if (!isNumber(month) || month > "12" || month < "01") return false;
        if (day > getMaxDay(year, month) || day < "01") return false;

        function getMaxDay(year, month) {
            if (month == 4 || month == 6 || month == 9 || month == 11)
                return "30";
            if (month == 2)
                if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                    return "29";
                else
                    return "28";
            return "31";
        }

        return true;
    }
};

//输入框获得焦点时隐藏底部导航

function FootNavHide(execute) {
    var inputList = $('input[type=text]'),
        textareaList = $('textarea');

    if (inputList.size() || textareaList.size() || execute) {
        var win = {
            sH: window.innerHeight,
            sW: window.innerWidth
        }
        var botNav = $('.commonNav');
        $(window).resize(function() {

            if ((win.sH - window.innerHeight > 100) && (win.sW == window.innerWidth)) {

                botNav.hide();
            } else {

                botNav.show();
            }
        });

    };

}


//UI工具
var UIBase = {
    middleShow: function(_settings) { //局中显示
        var settings = {
            obj: '',
            offsetLeft: 0, //偏移位置
            offsetTop: 0 //偏移位置
        };

        if (typeof(_settings) == 'string') {
            settings.obj = $(_settings);

        } else {
            $.extend(settings, _settings);
        }
        settings.obj.css('opacity', 0).show();

        var cssList = {
            left: '50%',
            top: '50%',
            'position': 'fixed',
            'margin-left': -Math.abs(settings.obj.width() / 2) - settings.offsetLeft,
            'margin-top': -Math.abs(settings.obj.height() / 2) - settings.offsetTop,
            'opacity': 1
        };
        settings.obj.css(cssList);

    },//loading效果(基于CSS3动画)
    loading: {
        show: function() {
         $('body').append("<div id=\"wxloading\" class=\"wx_loading\"><div class=\"wx_loading_inner\"><i class=\"wx_loading_icon\"></i>正在加载...</div></div>");
        },
        hide: function() {
            $('#wxloading').remove();
        }

    }
    //自动隐藏loadding
},
    MasklayerHandle = {
        hide: function() {
            var element = $('#masklayer'),
                cssList = $('link');
            if (!cssList.size()) {
                element.remove();
                return false;
            } else {
                var mTime;
                mTime = setInterval(function() {
                    var number = 0;
                    cssList.each(function() {
                        var self = this;
                        if (self.type != 'text/css') {
                            return false;
                        };
                        if (self.sheet && self.sheet.cssRules) {
                            number++;
                        };

                    });
                    if (number >= cssList.length) {
                        element.remove();
                        clearInterval(mTime);
                    };

                }, 30);

            }


        }

    };




$(function() {

    Validates.Customer();
    Validates.Status();
    MasklayerHandle.hide();




})