//自定义提示框
function Tips(options) {
    var sets = {
        alt: '提示',
        msg: '',
        fs: '' //获得焦点对象
    };
    if (options) {
        $.extend(sets, options);
    }
    var curBody = $('body');
    var html = "";
    html += "<div class=\"popshade\" style=\"display:block;height:" + curBody.height() + ";width:" + curBody.width() + ";  position: fixed; \" id=\"tipShade\"></div>";
    html += "<div class=\"tip_box\">";
    html += "<div>" + sets.alt + "</div>";
    html += "<div>" + sets.msg + "</div>";
    html += "<div><a href=\"javascript:;\" id=\"confirm\">确定</a></div>";
    html += "</div>";

    if ($('.tip_box').size() == 0) {

        curBody.append(html);
        curBody.find('#confirm').bind('click', function() {
            curBody.find('.tip_box').hide();
            curBody.find('#tipShade').hide();
            curBody.find('.tip_box').remove();
            curBody.find('#tipShade').remove();
            if (sets.fs) {
                sets.fs.focus();
            };

        });
    }



}

//自定义Loading
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

FxLoading.Hide();

//输入框获得焦点时隐藏底部导航
function FootNavHide(execute){

    var inputList = $('input[type=text]'),
        textareaList = $('textarea');

    if (inputList.size() || textareaList.size()||execute) {
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




function CheckSign() {

    // 签到或者注册查询
    if(Jit.AM.getPageParam('IsRegister') != '1'){
		
		Jit.AM.toPage('Val');
	}

}


$(function() {

    //如果没有上一页隐藏上一页按钮
    if (!Jit.AM.hasHistory()) {
        $('.bot_nav dt:eq(0)').hide();
    };

    //如果有输入框按钮，输入时隐藏底部导航
    FootNavHide(true);



})