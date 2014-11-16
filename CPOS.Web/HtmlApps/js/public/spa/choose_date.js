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

        $('[jitval=InDate]').html(me.getParams("InDate"));
        $('[jitval=OutDate]').html(me.getParams("OutDate"));

        $('[jitval=InWeek]').html(me.initWeek(new Date(me.getParams("InDate")).getDay()));
        $('[jitval=OutWeek]').html(me.initWeek(new Date(me.getParams("OutDate")).getDay()));

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
        $('.valid').bind('click', function (evt) {
            var target = $(evt.currentTarget);
            //me.selectDate(target);
            var $this=$(this);
            if(!$this.hasClass(".cui_cld_daycrt")){  //是否有选中效果
            	//将当期点击的日期添加状态其他的取消状态
            	$(".valid").removeClass("cui_cld_daycrt");
            	$(this).addClass("cui_cld_daycrt");
            	me.setParams('InDate', $this.attr('data-date'));
            }
        });
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
                daydom = $('<li data-date="' + daystr + '" class="cui_cld_daypass invalid"><em>&nbsp;' + daynum + '&nbsp;</em></li>');
            } else if ((daynum >= me.thisDay && loopDate.getMonth() == me.date.getMonth())) {
                
                var cday = daynum - me.thisDay;
                //绑定 今 明 后 数据
                if (cday <= 2) {
                    daydom = $('<li data-date="' + daystr + '" dayDis="' + dayDis[cday] + '" class="valid cui_cld_day_havetxt"><em>' + daynum + '</em><i>' + dayDis[cday] + '</i></li>');
                } else {
                    daydom = $('<li data-date="' + daystr + '" class="valid"><em>' + daynum + '</em><i></i></li>');
                }
                me.initCheck(daystr);
            } else if (loopDate.getMonth() > me.date.getMonth()) {
                daydom = $('<li data-date="' + daystr + '" class="valid"><em>' + daynum + '</em><i></i></li>');
                me.initCheck(daystr);
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
    initCheck: function (date) {
        
        var me = this;
        if (me.getParams('InDate') && me.getParams('OutDate')) {
            if (date == me.getParams('InDate')) {
                
                var curDate = new Date(me.getParams('InDate')).getDate();
                for (var i = 0; i < $(".cui_cld_daybox").find('li').length; i++) {
                    if ($(".cui_cld_daybox").find('li')[i].attributes[0].value == me.getParams('InDate')) {
                        
                    }
                }
                $('.valid').attr('data-date');
                
            } else if (date == me.getParams('OutDate')) {

            }
        }
    }
});