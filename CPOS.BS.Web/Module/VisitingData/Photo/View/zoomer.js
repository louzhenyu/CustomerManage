(function ($) {
    $.fn.Zoomer = function (b) {
        var c = $.extend({
            speedView: 200,
            speedRemove: 400,
            altAnim: false,
            speedTitle: 400,
            debug: false
        }, b);
        var d = $.extend(c, b);
        function e(s) {
            if (typeof console != "undefined"
					&& typeof console.debug != "undefined") {
                console.log(s)
            } else {
                alert(s)
            }
        }
        if (d.speedView == undefined || d.speedRemove == undefined
				|| d.altAnim == undefined || d.speedTitle == undefined) {
            e('speedView: ' + d.speedView);
            e('speedRemove: ' + d.speedRemove);
            e('altAnim: ' + d.altAnim);
            e('speedTitle: ' + d.speedTitle);
            return false
        }
        if (d.debug == undefined) {
            e('speedView: ' + d.speedView);
            e('speedRemove: ' + d.speedRemove);
            e('altAnim: ' + d.altAnim);
            e('speedTitle: ' + d.speedTitle);
            return false
        }

        if (typeof d.speedView != "undefined"
				|| typeof d.speedRemove != "undefined"
				|| typeof d.altAnim != "undefined"
				|| typeof d.speedTitle != "undefined") {
            if (d.debug == true) {
                e('speedView: ' + d.speedView);
                e('speedRemove: ' + d.speedRemove);
                e('altAnim: ' + d.altAnim);
                e('speedTitle: ' + d.speedTitle)
            }

            var divoldheight = $(this).height() + 10;
            var imgoldwidth = $(this).find('img').width();
            var imgoldheight = $(this).find('img').height();

            $(this).mouseover(function () {

                //移进
//                var cl;
//                try { clearTimeout(cl); } catch (e) { }
//                var cl = setTimeout(function () {
                    //保持父节点的宽高
                    $(this).css({
                        'z-index': '10',
                        filter: '',
                        height: divoldheight
                    });
                    //让子节点图片变大
                    $(this).find('img').css({
                        'z-index': '10',
                        position: "absolute"
                    });

                    $(this).find('img').addClass("hover").stop().animate({
                        marginTop: '-50px',
                        marginLeft: '-45px',
                        width: imgoldwidth + 100,
                        height: imgoldheight + 100,
                        //                    top: '50%',
                        //                    left: '50%',
                        //                    width: $(this).find('img').width() + 50+"px",
                        //                    height: $(this).find('img').height() + 50+"px",
                        padding: '20px'
                    }, d.speedView);
                    if (d.altAnim == true) {
                        var a = $(this).find("img").attr("alt");
                        if (a.length != 0) {
                            $(this).prepend('<div class="title" style="border:1px solid #5e5d5d;background:#ffffff;width:' + imgoldwidth + 'px;height:' + (parseInt(imgoldheight) - 5) + 'px;position: absolute;left:0;top:0;z-index:9;"><div style="color:black;padding-top:' + (parseInt(imgoldheight) + 62) + 'px">' + a + '</div></div>');

                            $('.title').animate({
                                marginTop: '-50px',
                                marginLeft: '-45px',
                                width: imgoldwidth + 100,
                                height: imgoldheight + 100
                            }, d.speedTitle);
                        }
                    }
//                }, 200);
            });
            $(this).mouseout(function () {
                //移出
                //alert($(this).height());
                $(this).css({
                    'z-index': '0',
                    filter: 'alpha(opacity=100)',
                    height: divoldheight
                });
                $(this).find('img').css({
                    position: "static"
                });
                $(this).find('img').removeClass("hover").stop().animate({
                    marginTop: '0',
                    marginLeft: '0',
                    top: '0',
                    left: '0',
                    width: imgoldwidth,
                    height: imgoldheight,
                    padding: '0'
                }, d.speedRemove);
                $(this).find('.title').remove();
            });
        }
    }
})(jQuery);