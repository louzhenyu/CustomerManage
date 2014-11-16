

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
                for (var i = 1; i < arguments.length; i++) {
                    v = v.replace("{" + (i - 1) + "}", arguments[i]);
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



//自定义提示框
function Tips(options) {
    var sets = {
        alt: '提示',
        msg: '',
        fs: '', //获得焦点对象
        fn: '' //回调函数
    };
    if (options) {
        $.extend(sets, options);
    }
    var curBody = $('body');
    var html =new StringBuilder();
    // html.appendFormat("<div class=\"popshade\" style=\"display:block;height:{0};width:{1};  position: fixed; \" id=\"tipShade\"></div>",curBody.height(),curBody.width());
    html.append("<div class=\"popshade\" style=\"display:block;height:100%;width:100%;  position: fixed; \" id=\"tipShade\"></div>");
    html.append("<div class=\"tip_box\">");
    html.appendFormat("<div>{0}</div>",sets.alt);
    html.appendFormat("<div>{0}</div>",sets.msg);
    html.append("<div><a href=\"javascript:;\" id=\"confirm\">确定</a></div>");
    html.append("</div>");

    if ($('.tip_box').size() == 0) {

        curBody.append(html.toString());
        curBody.find('#confirm').bind('click', function() {
            curBody.find('.tip_box').hide();
            curBody.find('#tipShade').hide();
            curBody.find('.tip_box').remove();
            curBody.find('#tipShade').remove();
            if (sets.fs) {
                sets.fs.focus();
            };
            if (sets.fn) {
                sets.fn();
            };

        });
    }

}


//自定义载入效果

var Tipload = {
    Show: function() {

        var loading = $('<div class=\'tipload\' id=\'tipload\'>');
        loading.html("正在加载，请稍等。");
        var curBody = $('body');
        curBody.append("<div class=\"tiploadshade\" style=\"display:block;height:" + curBody.height() + ";width:" + curBody.width() + ";  position: fixed; \" id=\"tipLoadShade\">");
        curBody.append(loading);

    },
    Hide: function() {
        $('#tipLoadShade,#tipload').remove();
    },
    AutoHide: function() {
        setTimeout(function() {
            $('#tipLoadShade,#tipload').remove();
        }, 20000);

    }



};



//自定义页面载入效果
var FxLoading = {
    Show: function() {
        //        var loading = $('<div>');
        //        loading.addClass('fxloading');
        //        loading.html("Loading");
        //        $('body').append(loading);
    },
    Hide: function() {
        $('.fxloading').remove();

    },
    AutoHide: function() {
        setTimeout(function() {
            $('.fxloading').remove();
        }, 3000);
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
        var botNav = $('.bot_nav');
        $(window).resize(function() {

            if ((win.sH - window.innerHeight > 100) && (win.sW == window.innerWidth)) {

                botNav.hide();
            } else {

                botNav.show();
            }
        });

    };

}

//认证Key
var authKey = "authval";

function CheckSign() {

    //测试代码，记得删除
    // FxLoading.Hide();
    // return false;


    // 签到或者注册查询
    var me = Jit.AM,
        curAuthVal = me.getPageParam(authKey);
    if (curAuthVal && curAuthVal == "1") {
        FxLoading.Hide();
        return false;
    };

    var datas = {
        eventId: (me.getBaseAjaxParam()).eventId
    };

    me.ajax({
        url: '/OnlineShopping/data/Data.aspx?Action=checkSign',
        data: datas,
        beforeSend: function() {
            FxLoading.Show();
        },
        success: function(data) {

            if (data.code == 200) {
                // if (data.content.isRegistered == "0" || data.content.isSigned == "0")
                if (data.content.isRegistered == "0") {
                    me.toPage('Val');
                } else {
                    FxLoading.Hide();
                    me.setPageParam(authKey, '1');
                }
            } else {
                me.toPage('Val');
            }
        }
    });

    FxLoading.AutoHide();

}


$(function() {

    //如果没有上一页隐藏上一页按钮
    if (typeof Jit != 'undefined' && !Jit.AM.hasHistory()) {
        $('.bot_nav dt:eq(0)').hide();
    };

    //如果有输入框按钮，输入时隐藏底部导航
    FootNavHide(true);
})


//延时隐藏微信菜单。
var weixinTime = '',
    exeCount = 0;

function WeixinHide() {
    if (typeof WeixinJSBridge != 'undefined') {

        // 隐藏右上角的选项菜单入口;  
        WeixinJSBridge.call('hideOptionMenu');
        // 隐藏底部菜单;  
        WeixinJSBridge.call('hideToolbar');
    };
}

weixinTime = window.setInterval(function() {
    WeixinHide();
    exeCount++;
    if (exeCount > 4) {
        window.clearInterval(weixinTime);
    };
}, 100);