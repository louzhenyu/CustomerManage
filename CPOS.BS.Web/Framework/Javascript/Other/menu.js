$(document).ready(function () {
    /* limenu */
    $('.limenu').click(function () {
        if ($(this).parent().attr('class') == 'current') {
            $(this).parent().removeClass('current').children('a').removeClass('aCurrent').siblings('.menu').slideUp();
        } else {
            $(this).parent().addClass('current').children('a').addClass('aCurrent').siblings('.menu').slideDown()
            .end().end().siblings().removeClass('current').children('a').removeClass('aCurrent').siblings('.menu').slideUp();
        }
    });

    var mid = JITMethod.getUrlParam("mid");
    $("#" + mid).addClass("aCtn");
    $("#" + mid).parent().parent().show().siblings('.menu').show();
    $("#" + mid).parent().parent().parent().addClass("current");
    $("#" + mid).parent().parent().parent().find("a:eq(0)").addClass("aCurrent");
});