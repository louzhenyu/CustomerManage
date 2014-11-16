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


var Validates = {
    hasPageList: ['UnionSchool'], //需要验证的列表页面
    isLogin: function() { //验证用户是否登陆
        var me = Jit.AM,
            baseInfo = me.getBaseAjaxParam();
        return (baseInfo && baseInfo.userId) ? true : false;
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
        // var elTitle = $('title');
        // if (elTitle.size()) {
        //     var curPageTitleName = elTitle.attr('name');
        //     if (self.hasPageList.join('').indexOf(curPageTitleName) == -1) { //如果不是需要验证的页面不做验证
        //         return false;
        //     };
        // };
        var userId = localStorage.getItem(valKeyList.userId);
        if (userId) {
            baseInfo.userId = userId;
            Jit.AM.setBaseAjaxParam(baseInfo);
            // me.toPage('Home');
            return false;
        };
        // me.toPage('NewLogin');
        // me.ajax({
        //     url: '/dynamicinterface/data/data.aspx',
        //     data: {
        //         'action': 'getUserIDByOpenID'
        //     },
        //     success: function(data) {
        //         if (data && data.code == 200) {
        //             baseInfo.userId = data.content.userId;
        //             Jit.AM.setBaseAjaxParam(baseInfo);
        //             localStorage.setItem(valKeyList.userId, baseInfo.userId);
        //             // me.toPage('Home');
        //         } else {
        //             localStorage.setItem(valKeyList.userId, '');
        //             return false;
        //             me.toPage('NewLogin');
        //         }
        //     },
        //     error: function() {
        //            return false;
        //         me.toPage('NewLogin');
        //     }
        // });

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

    setTimeout(function() {
        Validates.Status();
    }, 100)


    MasklayerHandle.hide();

})