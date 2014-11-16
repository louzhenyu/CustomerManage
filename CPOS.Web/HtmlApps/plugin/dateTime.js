define(function(){
	var html='<div id="time-date" class="time-section">\
	<div class="header" id="date-header">\
		<nav>\
            <dl class="time-tool_bar clearfix">\
                <dd id="close" style="float: left;">\
                    <a href="javascript:;"></a>\
                </dd>\
                <dd id="sure" style="float: right;margin: 6px 15px 0 0;">\
                    <a  href="javascript:;">\
                        <img src="../../../images/public/hotel_default/X1.png" alt="确定"/>\
                    </a>\
                </dd>\
            </dl>\
        </nav>\
	</div>\
	<div style="background:#ccc">\
        <div class="btn_date">\
            <a href="javascript:JitPage.setInDate();" class="active">\
                <dl>\
                    <dt>选择时间</dt>\
                    <dd>\
                        <span id="currentDate">9月11日</span><span id="currentDay">星期三</span>\
                    </dd>\
                </dl>\
            </a>\
        </div>\
        <section class="cui_cldunit">\
            <h2 class="cui_cldmonth">\
                2014年1月\
            </h2>\
            <ul class="cui_cldweek">\
                <li>日</li>\
                <li>一</li>\
                <li>二</li>\
                <li>三</li>\
                <li>四</li>\
                <li>五</li>\
                <li>六</li>\
            </ul>\
            <ul class="cui_cld_daybox" name="month_3">\
            </ul>\
        </section>\
        <section class="cui_cldunit">\
            <h2 class="cui_cldmonth">\
                2014年1月\
            </h2>\
            <ul class="cui_cldweek">\
                <li>日</li>\
                <li>一</li>\
                <li>二</li>\
                <li>三</li>\
                <li>四</li>\
                <li>五</li>\
                <li>六</li>\
            </ul>\
            <ul class="cui_cld_daybox" name="month_4">\
            </ul>\
        </section>\
        <section class="cui_cldunit">\
            <h2 class="cui_cldmonth">\
                2014年1月\
            </h2>\
            <ul class="cui_cldweek">\
                <li>日</li>\
                <li>一</li>\
                <li>二</li>\
                <li>三</li>\
                <li>四</li>\
                <li>五</li>\
                <li>六</li>\
            </ul>\
            <ul class="cui_cld_daybox" name="month_0">\
            </ul>\
        </section>\
        <section class="cui_cldunit">\
            <h2 class="cui_cldmonth">\
                2014年1月\
            </h2>\
            <ul class="cui_cldweek">\
                <li>日</li>\
                <li>一</li>\
                <li>二</li>\
                <li>三</li>\
                <li>四</li>\
                <li>五</li>\
                <li>六</li>\
            </ul>\
            <ul class="cui_cld_daybox" name="month_1">\
            </ul>\
        </section>\
        <section class="cui_cldunit">\
            <h2 class="cui_cldmonth">\
                2014年2月\
            </h2>\
            <ul class="cui_cldweek">\
                <li>日</li>\
                <li>一</li>\
                <li>二</li>\
                <li>三</li>\
                <li>四</li>\
                <li>五</li>\
                <li>六</li>\
            </ul>\
            <ul class="cui_cld_daybox" name="month_2">\
            </ul>\
        </section>\
    </div>';
	var dateTime={
	    onPageLoad: function () {
	        this.initPage();
	    },
	    dateResult:{
	    	dateTime:"",   //日期2014-05-06
	    	day:""         //周几
	    },
	    currentObj:"",  //当前选中的对象
	    prevObj:"",
	    initPage: function () {
	        var me = dateTime;
	        me.date = new Date();
	        me.thisYear = me.date.getFullYear();
	        me.thisMonth = me.date.getMonth() + 1;
	        me.thisDay = me.date.getDate();
	        var year, month, firstDate = new Date(me.thisYear + '/' + me.thisMonth + '/1');
            // if (me.thisMonth == 2) {
            //     year = me.thisYear;
            //     month = me.thisMonth-1;
            // } else {
            //     year = me.thisYear;
            //     month = me.thisMonth-1;
            // }
            curDate = new Date(me.thisYear + '/' + ((me.thisMonth-2)==0?12:(me.thisMonth-2)) + '/1');
            $($('.cui_cldmonth').get(0)).html(me.getDateWithFormat(curDate, 'YYYY年MM月'));
            me.buildMonthView($('[name=month_' + 3 + ']'), curDate);

            curDate = new Date(me.thisYear + '/' + (me.thisMonth-1) + '/1');
            $($('.cui_cldmonth').get(1)).html(me.getDateWithFormat(curDate, 'YYYY年MM月'));
            me.buildMonthView($('[name=month_' + 4 + ']'), curDate);
            
	        for (var i = 0; i <= 2; i++) {
	            if (me.thisMonth == 12) {
	                year = me.thisYear + 1;
	                month = 1;
	            } else {
	                year = me.thisYear;
	                month = me.thisMonth + i;
	            }
	            curDate = new Date(year + '/' + month + '/1');
	            $($('.cui_cldmonth').get(i+2)).html(me.getDateWithFormat(curDate, 'YYYY年MM月'));
	            me.buildMonthView($('[name=month_' + i + ']'), curDate);
	        }
	        var $currentDate=$("#currentDate"),
	        	$currentDay=$("#currentDay");
	        $('.valid').bind('tap', function (evt) {
	            var target = $(evt.currentTarget);
	            //me.selectDate(target);
	            var $this=$(this);
	            if(me.prevObj){
	            	var text=me.prevObj.attr("data-text");
	            	if(text!=""){
	            		me.prevObj.find("i").html(text);
	            	}else{
						me.prevObj.find("i").html("");
	            	}
	            	me.prevObj.removeClass("cui_cld_daycrt");
	            }
	            me.currentObj=$this;
	            if(!me.flag){   //是否是第一次点击
	            	me.prevObj=me.currentObj;
	            	me.flag=true;
	            }else{
	            	me.prevObj=me.currentObj;
	            }
	            if(!$this.hasClass(".cui_cld_daycrt")){  //是否有选中效果
	            	//将当期点击的日期添加状态其他的取消状态
	            	$(".valid").removeClass("cui_cld_daycrt");
	            	$(this).addClass("cui_cld_daycrt");
	            	var dateStr=$this.attr('data-date');
				    $this.find("i").html(me.getDay(dateStr));
				    me.dateResult.dateTime=dateStr;
				    me.dateResult.day=me.getDay(dateStr);
				    $currentDate.html(dateStr);
				    $currentDay.html(me.dateResult.day);
				    //将数据回传
					me.callback(me.dateResult);
					setTimeout(function(){
						$("#time-date").remove();
					},300);
	            }

	        });
			$("#close").bind("tap",function(){
				$("#time-date").remove();
			});
			$("#sure").bind("tap",function(){
				//将数据回传
				me.callback(me.dateResult);
				setTimeout(function(){
					$("#time-date").remove();
				},500);
			});
				
	    },
	    //获得周几
	    getDay:function(dateStr){
			var arys1= new Array();      
		    arys1=dateStr.split('-');     //日期为输入日期，格式为 2013-3-10
		    var ssdate=new Date(arys1[0],parseInt(arys1[1]-1),arys1[2]);   
		    var theDay=ssdate.getDay();  //周几
		    return this.initWeek(theDay)
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
	        	if(me.subscribe){//是否为预约
	        		panel.append($('<li class="cui_cld_dayfuture invalid"></li>'));
	        	}else{
	        		panel.append($('<li class="valid"></li>'));
	        	}
	            
	        }
	        do {
	            daystr = me.getDateWithFormat(loopDate, 'YYYY-MM-DD');
	            daynum = loopDate.getDate();
	            if ((daynum < me.thisDay && loopDate.getMonth() == me.date.getMonth())) {
	            	if(me.subscribe){
	            		daydom = $('<li data-date="' + daystr + '" class="cui_cld_daypass invalid"><em>&nbsp;' + daynum + '&nbsp;</em></li>');
	            	}else{
	            		daydom = $('<li data-date="' + daystr + '" class="valid"><em>&nbsp;' + daynum + '&nbsp;</em></li>');
	            	}
	                
	            } else if ((daynum >= me.thisDay && loopDate.getMonth() == me.date.getMonth())) {
	                
	                var cday = daynum - me.thisDay;
	                //绑定 今 明 后 数据
	                if (cday <= 2) {
	                	if(dayDis[cday]=="今天"){
	                		$("#currentDate").html(daystr);
	                		$("#currentDay").html(me.getDay(daystr));
	                	}
	                    daydom = $('<li data-text="'+dayDis[cday]+'"  data-date="' + daystr + '" dayDis="' + dayDis[cday] + '" class="valid cui_cld_day_havetxt"><em>' + daynum + '</em><i>' + dayDis[cday] + '</i></li>');
	                } else {
	                    daydom = $('<li data-date="' + daystr + '" class="valid"><em>' + daynum + '</em><i></i></li>');
	                }
	                //me.initCheck(daystr);
	            } else if (loopDate.getMonth() > me.date.getMonth()) {
	                daydom = $('<li data-date="' + daystr + '" class="valid"><em>' + daynum + '</em><i></i></li>');
	                //me.initCheck(daystr);
	            }else{
	            	daydom = $('<li data-date="' + daystr + '" class="valid"><em>' + daynum + '</em><i></i></li>');
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
	    //显示
	    show:function(options){
	    	var me=this;
	    	$("body").append(html);
	    	this.onPageLoad();
	    	$("#date-header").hide();
	    	if(options){
	    		if(options.callback&&typeof options.callback=="function"){
		    		me.callback=options.callback;
		    	}
		    	//预约则会把之前的当前日期的前面时间设置为不可选   true为预约
		    	if(options.subscribe){
		    		me.subscribe=options.subscribe;
		    	}
	    	}
	    	
	    },
	    hide:function(){
	    	$("#time-date").remove();
	    },

	}

	Jit.UI.dateTime=dateTime;
});