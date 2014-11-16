define(['jquery', 'tools','mobileScroll'], function () {
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

            this.DateTimeEvent();
        	this.initEvent();
        	this.loadData();
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
                    ReportAction: "ireport!studentStatAssessReport.action",
                    DynamicParam: "coachee=" + that.email + "$startTime=" + startDate + "$endTime=" + endDate
                },
                success: function (data) {
                    data = JSON.parse(data.Data.JsonResult);
                    if (data.error.err_code == 0) {
                        //处理data数据
                        var list = data.object.lst;
                        list = list ? list : [];
                        var arrayData = [];

                        for (var i = 0, len = list.length; i < len; i++) {
                            var item = list[i];
                            var obj = {};
                            obj.inviteid = item.id;
                            obj.name = item.coacher_name;
                            obj.email = item.coacher;
                            obj.date = item.time;
                            arrayData.push(obj);
                        }

                        //生成报表
                        that.generateCharts(arrayData);
                    } else {
                        alert(data.error.err_msg);
                    }
                }
            });
        },
        generateCharts: function (array) {
            var that = this;
            $("#content").html("");
            for (var i = 0, il = array.length; i < il; i++) {
                var item = array[i];
                var year = item.date.substr(0, 4);
                var month = item.date.substr(5, 2);
                var day = item.date.substr(8, 2);
                var coachDateString = year + "-" + month + "-" + day;
                var rowHtmlString = "<tr>" +
	                "<td>" + item.email + "</td>" +
	                "<td>" + item.name + "</td>" +
	                "<td>" + coachDateString + "</td>" +
	                "<td><a href=\"" + "coacheeDetail.html?CustomerId=" + that.customerId +
	                "&UserId=" + that.userId + "&token=" + that.token + "&email=" + that.email +
                    "&inviteId=" + item.inviteid +
	                "\">查看明细</a></td>" +
	                "</tr>";
                $("#content").append(rowHtmlString);
            }
        },
        DateTimeEvent: function () {
            //日期事件
            var self = this,
				currYear = (new Date()).getFullYear(),
				currMonth = (new Date()).getMonth(),
				opt = {};
            //opt.datetime = { preset : 'datetime', minDate: new Date(2014,3,25,15,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
            opt.datetime = { preset: 'datetime' };
            opt.date = {
                preset: 'date',
                minDate: new Date()
            };
            opt.time = {
                preset: 'time',
                minDate: new Date()
            };
            opt["default"] = {
                theme: 'android-ics light', //皮肤样式
                display: 'modal', //显示方式
                mode: 'scroller', //日期选择模式
                lang: 'en',
                dateFormat:'yy-mm-dd',
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
        initEvent: function () {
            var that = this;
            //生成蜘蛛网
            $("#search").click(function (e) {
                that.loadData();
            });
        }
    };

    page.init();
});


