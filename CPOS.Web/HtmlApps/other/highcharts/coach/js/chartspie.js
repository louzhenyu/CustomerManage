define(['jquery', 'tools','mobileScroll','echarts'/*'highcharts','highchartsMore','highchartsExporting'*/], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        url: "/ApplicationInterface/Project/PG/PGHandler.ashx",
        //url: "http://o2oapi.dev.aladingyidong.com/ApplicationInterface/Project/PG/PGHandler.ashx",
        init: function () {
            var that = this;
            var customerId = $.util.getUrlParam("customerId"),
        		token = $.util.getUrlParam("token"),
        		userId = $.util.getUrlParam("userId"),
        		email = $.util.getUrlParam("email");
            that.customerId = customerId;
            that.token = token;
            that.userId = userId;
            that.email = email;
            $("#section a").each(function (i) {
                $(this).attr("href", $(this).attr("href") + "&customerId=" + customerId);
            });
            this.DateTimeEvent();
            this.initEvent();
            //请求报表数据
            that.loadData();
        },
        currentTime:function(){ 
	        var now = new Date();
	        var year = now.getFullYear();       //年
	        var month = now.getMonth() + 1;     //月
	        var day = now.getDate();            //日
	       
	        var hh = now.getHours();            //时
	        var mm = now.getMinutes();          //分
	       
	        var clock = year + "-";
	       
	        if(month < 10)
	            clock += "0";
	       
	        clock += month + "-";
	       
	        if(day < 10)
	            clock += "0";
	           
	        clock += day + " ";
	       var time="";
	        if(hh < 10)
	            time += "0";
	           
	        time += hh + ":";
	        if (mm < 10) time += '0'; 
	        time += mm; 
	        return {date:clock,time:time}; 
	    },
        feedbackItem: function () {
            var items = [];
            items[0] = "Goal";
            items[1] = "Reality";
            items[2] = "Options";
            items[3] = "Demo Strategy";
            items[4] = "Demo PSF";
            items[5] = "Recognition & Feedback";
            items[6] = "Will";

            return items;
        },

        //获得ajax的请求数据
        loadData: function (callback) {
            var startDate = $(txtDate).val();
            var endDate = $(txtTime).val();
            var that = this;
            $.util.ajax({
                url: that.url,
                customerId: that.customerId,
                userId: that.userId,
                data: {
                    action: 'CommonRequestReportForm',
                    PGToken: that.token,
                    ReportAction: "ireport!teacherStatFeedbackReport.action",
                    DynamicParam: "email=" + that.email + "$startTime=" + startDate + "$endTime=" + endDate
                },
                success: function (data) {
                    data = JSON.parse(data.Data.JsonResult);
                    if (data.error.err_code == 0) {
                        //处理data数据
                        var item = data.object;
                        item = item ? item : {};
                        var datas = [];

                        var arr = [];
                        if (item.result_avg.length == 0) {
                            arr = [0, 0, 0, 0, 0, 0, 0];
                        } else {
                            arr = item.result_avg.split("|");
                        }

                        // 分值对象
                        var avgobj = {};
                        avgobj.value = arr;
                        avgobj.name = 'Average Score';

                        var dataItem = {};
                        dataItem.name = 'Coach - Average';
                        dataItem.type = 'radar';
                        dataItem.data = [];
                        dataItem.data.push(avgobj);

                        datas.push(dataItem);
                        //生成报表
                        that.generateCharts(datas);
                    } else {
                        alert(data.error.err_msg);
                    }

                }

            });
        },

        DateTimeEvent: function () {
			//日期事件
			var self = this, 
				currYear = (new Date()).getFullYear(),
				currMonth=(new Date()).getMonth(),
				opt = {};
			//opt.datetime = { preset : 'datetime', minDate: new Date(2014,3,25,15,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
			opt.datetime = {preset : 'datetime'};
			opt.date = {
				preset : 'date',
				minDate:new Date()
			};
			opt.time = {
				preset : 'time',
				minDate:new Date()
			};
			opt["default"] = {
			    theme: 'android-ics light', //皮肤样式
			    display: 'modal', //显示方式
			    mode: 'scroller', //日期选择模式
			    lang: 'en',
			    dateFormat: 'yy-mm-dd',
			    dateOrder: 'yyyymmdd',
			    startYear: 1990, //开始年份
			    endYear: (currYear + 30), //结束年份,
			    CallBack: function (a, b, c) {
			    }
			};
			var curDate = this.currentTime().date;
			var ttx = "";
			var theYear = new Date().getFullYear();
			if (currMonth < 7) {
			    theYear = theYear - 1;
			}
			ttx = theYear + "-07-01";
			$("#txtDate").val(ttx);
			$("#txtTime").val(curDate);
			$("#txtDate").mobiscroll().date(opt['default']);
			$("#txtTime").mobiscroll().date(opt['default']);
		},
		//初始化时间
		initEvent:function(){
			var that=this;
			//生成蜘蛛网
			$("#search").click(function (e) {
			    that.loadData();
			});
		},
		//生成图表
		generateCharts:function(object){
		    var option = {
		        title: {
		            text: 'Coach - Average',
		            subtext: ''
		        },
		        tooltip: {
		            trigger: 'axis'
		        },

		        legend: {
		            orient: 'vertical',
		            x: 'center',
		            y: 'bottom',
		            data: []
		        },
		        toolbox: {
			        show: false
			    },
			    polar: [
                    {
                        indicator: [
                            { text: 'Goal', max: 5 },
                            { text: 'Reality', max: 5 },
                            { text: 'Options', max: 5 },
                            { text: 'Demo Strategy', max: 5 },
                            { text: 'Demo PSF', max: 5 },
                            { text: 'Recognition & Feedback', max: 5 },
                            { text: 'Will', max: 5 }
                        ]
                    }
			    ],
			    calculable: true,
			    pane: {
			    	size: '80%'
			    },
			    series: object
			};
		    echarts.init($('#container')[0]).setOption(option);
		}
    };

    page.init();
});