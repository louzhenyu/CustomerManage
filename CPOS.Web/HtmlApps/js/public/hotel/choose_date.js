var clickCnt = 0;
Jit.AM.defindPage({
    name: 'ChooseDate',
    onPageLoad: function () {
        this.initEvent();
        this.initPage();
    },
    initEvent: function () {
        var me = this;

        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        me.date = new Date();
        me.thisYear = me.date.getFullYear();
        me.thisMonth = me.date.getMonth() + 1;
        me.thisDay = me.date.getDate();
        var inDate = me.getParams("InDate"), otDate = me.getParams("OutDate");
        me.InDate=inDate;me.OutDate=otDate;
    },
    //初始化状态
    initStatus:function(){
    	var me=this;
    	var inDate = me.getParams("InDate"), otDate = me.getParams("OutDate");
        if (inDate >= me.getDateWithFormat(new Date(), 'YYYY-MM-DD')&&otDate>=inDate) {
            $('[jitval=InDate]').html(me.getParams("InDate"));
            $('[jitval=OutDate]').html(me.getParams("OutDate"));

            $('[jitval=InWeek]').html(me.initWeek(new Date(me.getParams("InDate")).getDay()));
            $('[jitval=OutWeek]').html(me.initWeek(new Date(me.getParams("OutDate")).getDay()));
            $("[data-date='"+inDate+"']").removeClass("no_i_tag").addClass("selected-start cui_cld_daycrt");
            $("[data-date='"+otDate+"']").removeClass("no_i_tag").addClass("selected-end cui_cld_daycrt");
            $("[data-date='"+inDate+"']").find("i").html("入住");
            $("[data-date='"+otDate+"']").find("i").html("退房");
        } else {
            var today = new Date();
            var format1=me.getDateWithFormat(today, 'YYYY-MM-DD');
            var format2=me.getDateWithFormat(me.changeDate(today, 0, 0, 1), 'YYYY-MM-DD');
            $('[jitval=InDate]').html(format1);
            $('[jitval=OutDate]').html(format2);

            $('[jitval=InWeek]').html(me.initWeek(today.getDay()));
            $('[jitval=OutWeek]').html(me.initWeek(me.changeDate(today, 0, 0, 1).getDay()));
            $("[data-date='"+format1+"']").removeClass("no_i_tag").addClass("selected-start cui_cld_daycrt");
            $("[data-date='"+format1+"']").removeClass("no_i_tag").find("i").html("入住");
            $("[data-date='"+format2+"']").addClass("selected-end cui_cld_daycrt");
            $("[data-date='"+format2+"']").find("i").html("退房");
        }
        me.focusDates();
        //让入住和离店的日期默认选中
        //valid  cui_cld_day_havetxt selected-end cui_cld_daycrt
    },
    initPage: function () {
        var me = this, year, month, firstDate = new Date(me.thisYear + '/' + me.thisMonth + '/1');
        for (var i = 0; i <= 2; i++) {
            if (me.thisMonth == 12) {
                year = me.thisYear + 1;
                month = 1;
            } else {
                year = me.thisYear;
                month = me.thisMonth + i;
            }
            curDate = new Date(year + '/' + month + '/1');
            $($('.cui_cldmonth').get(i)).html(me.getDateWithFormat(curDate, 'YYYY年MM月'));
            me.buildMonthView($('[name=month_' + i + ']'), curDate);
        }
        me.initCheck();
        $('.valid').bind('click', function (evt) {
            var target = $(evt.currentTarget);
            me.selectDate(target);
        });
        me.dateType = 'In';
        me.initStatus();
    },
    setInDate: function () {
        this.dateType = 'In';
    },
    setOutDate: function () {
        this.dateType = 'Out';
    },
    selectDate: function (target) {
        var me = this, dom;
        
        if (me.dateType == 'In') {//第n次点击事件清除前一次选中的样式
            if (me.OutDate) {
                dom = $('.selected-start');
                $(dom.find('i')).html('');
                if (dom.attr('dayDis')) {
                    $(dom.find('i')).html(dom.attr('dayDis'));
                    if (dom.attr('dayDis') != "今天" && dom.attr('dayDis') != "明天" && dom.attr('dayDis') != "后天") {
                        dom.removeClass('selected-start').removeClass('cui_cld_daycrt').addClass("no_i_tag");
                    }
                    else {
                        dom.removeClass('selected-start').removeClass('cui_cld_daycrt').addClass("no_i_tag");
                    }
                } else {
                    dom.removeClass('selected-start').removeClass('cui_cld_day_havetxt cui_cld_daycrt').addClass("no_i_tag");
                }
                dom = $('.selected-end');
                $(dom.find('i')).html('');
                dom.each(function (i, em) {
                    if ($(em).attr('dayDis')) {
                        $($(em).find('i')).html($(em).attr('dayDis'));
                        if (dom.attr('dayDis') != "今天" && dom.attr('dayDis') != "明天" && dom.attr('dayDis') != "后天") {
                            $(em).removeClass('selected-end').removeClass('cui_cld_daycrt').addClass("no_i_tag");
                        }
                        else {
                            $(em).removeClass('selected-end').removeClass('cui_cld_daycrt');
                        }
                    } else {
                        $(em).removeClass('selected-end').removeClass('cui_cld_day_havetxt cui_cld_daycrt').addClass("no_i_tag");
                    }
                });
                dom = $('.cui_cld_day_havetxt');
                dom.removeClass('no_i_tag')
                me.focusDates();
            }
            dom = $('.selected-start');
            $(dom.find('i')).html('');
            if (dom.attr('dayDis')) {
                $(dom.find('i')).html(dom.attr('dayDis'));

                dom.removeClass('selected-start').removeClass('cui_cld_daycrt');

            } else {
                dom.removeClass('selected-start').removeClass('cui_cld_day_havetxt cui_cld_daycrt');
            }

            target.addClass('selected-start cui_cld_day_havetxt cui_cld_daycrt').removeClass("no_i_tag");
            $(target.find('i')).html('入住');
            me.InDate = target.attr('data-date');
            me.setParams('InDate', target.attr('data-date'));
            me.setOutDate();
            $('[jitval=InDate]').text(target.attr('data-date'));
            $('[jitval=InWeek]').text(me.initWeek(new Date(target.attr('data-date')).getDay()));

        } else if (me.dateType == 'Out') {
            if (me.InDate) {
                var od = target.attr('data-date');
                od = new Date(od.replace(/-/gi, '/'));
                id = new Date(me.InDate.replace(/-/gi, '/'));
                //如果状态已经是退房 选择的日期在退房之前 则返回
                if (od.getTime() <= id.getTime()) {
                    return;
                }
                target.removeClass("no_i_tag");
            }
            dom = $('.selected-end');
            $(dom.find('i')).html('');
            dom.each(function (i, em) {
                if ($(em).attr('dayDis')) {
                    $($(em).find('i')).html($(em).attr('dayDis'));

                    $(em).removeClass('selected-end').removeClass('cui_cld_daycrt').addClass('no_i_tag');

                } else {
                    $(em).removeClass('selected-end').removeClass('cui_cld_day_havetxt cui_cld_daycrt').addClass('no_i_tag');
                }
            });
            target.addClass('selected-end cui_cld_day_havetxt cui_cld_daycrt');
            $(target.find('i')).html('退房');
            me.focusDates();
            me.OutDate = target.attr('data-date');
            me.setParams('OutDate', target.attr('data-date'));
            me.setInDate();
            $('[jitval=OutDate]').text(target.attr('data-date'));
            $('[jitval=OutWeek]').text(me.initWeek(new Date(target.attr('data-date')).getDay()));


        }
    },
    focusDates: function () {
        var inDom = $('.selected-start'), outDom = $('.selected-end'), loop = false;
        if (inDom.length == 0 || outDom.length == 0) {
            return;
        }
        $('.valid').each(function (i, item) {
            if ($(item).hasClass('selected-start')) {
                loop = true;
            } else {
                if ($(item).hasClass('selected-end')) {
                    loop = false;
                    return;
                }
                if (loop) {
                    $(item).addClass('selected-end cui_cld_day_havetxt cui_cld_daycrt');
                }
            }
        });
    },
    /*
    * 绑定月份内容
    * panel 待绑定区域面板
    * date  绑定开始日期
    */
    buildMonthView: function (panel, date) {
        var me = this, buildMonth = date.getMonth();
        //本月第一天
        var thisMonthFirstDate = date, loopDate = thisMonthFirstDate, daystr;

        var nulldays = loopDate.getDay(), panel = $(panel), daydom, daynum, dayDis = ['今天', '明天', '后天'];
        //渲染当前天所在月之前的日期
        for (var i = 1; i <= nulldays; i++) {
            panel.append($('<li class="cui_cld_dayfuture invalid"></li>'));
        }
        do {
            daystr = me.getDateWithFormat(loopDate, 'YYYY-MM-DD');
            daynum = loopDate.getDate();

            if ((daynum < me.thisDay && loopDate.getMonth() == me.date.getMonth())) {
                daydom = $('<li name="date" data-date="' + daystr + '" class="cui_cld_daypass invalid"><em>&nbsp;' + daynum + '&nbsp;</em></li>');
            } else if ((daynum >= me.thisDay && loopDate.getMonth() == me.date.getMonth())) {
                var cday = daynum - me.thisDay;
                //绑定 今 明 后 数据
                if (cday <= 2) {
                    daydom = $('<li name="date" data-date="' + daystr + '" dayDis="' + dayDis[cday] + '" class="valid  cui_cld_day_havetxt"><em>' + daynum + '</em><i>' + dayDis[cday] + '</i></li>');
                } else {
                    daydom = $('<li name="date" data-date="' + daystr + '" class="valid no_i_tag"><em>' + daynum + '</em><i></i></li>');
                }
            } else if (loopDate.getMonth() > me.date.getMonth()) {
                daydom = $('<li name="date" data-date="' + daystr + '" class="valid no_i_tag"><em>' + daynum + '</em><i></i></li>');
            }
            panel.append(daydom);
            loopDate = me.changeDate(loopDate, 0, 0, 1);
        } while (loopDate.getMonth() == buildMonth)
    },
    //格式化日期 
    getDateWithFormat: function (date, format) {
        var me = this;
        format = format.replace('YYYY', date.getFullYear());
        format = format.replace('MM', (date.getMonth() + 1) < 10 ? "0" + (date.getMonth() + 1).toString() : date.getMonth() + 1);
        format = format.replace('DD', (date.getDate()) < 10 ? "0" + (date.getDate()).toString() : date.getDate());
        return format;
    },
    //改变日期 +1天 +1月 +1年
    changeDate: function (date, year, month, day) {
        if (!date) {
            date = new Date();
        }
        if (year) {
            date.setFullYear(date.getFullYear() + year);
        }
        if (month) {
            date.setMonth(date.getMonth() + month);
        }
        if (day) {
            date.setDate(date.getDate() + day);
        }
        return date;
    },
    initWeek: function (date) {
        var me = this, html = '';
        switch (date.toString()) {
            case "0":
                html = "周日";
                break;
            case "1":
                html = "周一";
                break;
            case "2":
                html = "周二";
                break;
            case "3":
                html = "周三";
                break;
            case "4":
                html = "周四";
                break;
            case "5":
                html = "周五";
                break;
            case "6":
                html = "周六";
                break;
        }
        return html;
    },
    initCheck: function () {
        var me = this, dom = null, date = null;

        dom = $("[name=date]");
        $("[name=date]").each(function (a, b) {
            date = $(b).attr("data-date");
            if (date == me.getParams("InDate")) {
                //$(dom[a].find('i')).html('入住');
            }
        });
    }
});