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
        		email = $.util.getUrlParam("email"),
        		startDate = $.util.getUrlParam("startDate"),
        		endDate = $.util.getUrlParam("endDate"),
                coachee = $.util.getUrlParam("coachee");
            debugger;
            that.customerId = customerId;
            that.token = token;
            that.userId = userId;
            that.email = email;

            this.loadData(startDate, endDate, coachee);
        },
        //获得ajax的请求数据
        loadData: function (startDate, endDate, coachee) {
            var that = this;
            $.util.ajax({
                url: that.url,
                customerId: that.customerId,
                userId: that.userId,
                data: {
                    action: 'CommonRequestReportForm',
                    PGToken: that.token,
                    ReportAction: "ireport!teacherStatStudentReport.action",
                    DynamicParam: "email=" + that.email + "$coachee=" + coachee + "$startTime=" + startDate + "$endTime=" + endDate
                },
                success: function (data) {
                    data = JSON.parse(data.Data.JsonResult);
                    if (data.error.err_code == 0) {
                        //处理data数据
                        var list = data.list;
                        list = list ? list : [];
                        var arrayData = [];

                        for (var i = 0, len = list.length; i < len; i++) {
                            var item = list[i];
                            var obj = {};
                            obj.topic = item.topic;
                            obj.isFinished = item.is_finished;
                            obj.date = item.time;
                            obj.coachees = item.coachees;
                            obj.inviteid = item.id;
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
            $("#coachlist").html("");
            for (var i = 0, il = array.length; i < il; i++) {
                var item = array[i];
                var buttonClass;
                var buttonText;
                if (item.isFinished == 1) {
                    buttonClass = "finishBtn";
                    buttonText = "Finished";
                } else {
                    buttonClass = "startBtn";
                    buttonText = "Not Start";
                }
                var coacheeList = item.coachees.split("|");
                var coacheeString = "";
                for (var j = 0, jl = coacheeList.length; j < jl; j++) {
                    if (coacheeList[j].length > 0) {
                        coacheeString += coacheeList[j].toUpperCase().replace("@PG.COM", "");
                        coacheeString += ", ";
                    }
                }

                var year = item.date.substr(0, 4);
                var month = item.date.substr(5, 2);
                var day = item.date.substr(8, 2);

                var coachDateString = year + "-" + month + "-" + day;
                var rowHtmlString = "<li><a href=\"coachDetail.html?inviteId=" + item.inviteid +
                    "&UserId=" + that.userId + "&CustomerId=" + that.customerId +
                    "&token=" + that.token +
                    "\" class=\"" + buttonClass + "\">"+ buttonText +"</a>" +
                    "<div class=\"info\">" +
                	"<p class=\"clearfix\"><span class=\"title\">" + item.topic + "</span>" +
                    "<span class=\"date\">" + coachDateString + "</span></p>" +
                    "<p class=\"exp\">Coachee:" + coacheeString + "</p></div></li>";
                $("#coachlist").append(rowHtmlString);
            }
        }
    };

    page.init();
});


