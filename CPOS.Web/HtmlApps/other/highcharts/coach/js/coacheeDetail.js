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
                inviteId = $.util.getUrlParam("inviteId");
            debugger;
            that.customerId = customerId;
            that.token = token;
            that.userId = userId;
            that.email = email;

            this.loadData(inviteId);
        },
        //获得ajax的请求数据
        loadData: function (inviteId) {
            var that = this;
            $.util.ajax({
                url: that.url,
                customerId: that.customerId,
                userId: that.userId,
                data: {
                    action: 'CommonRequestReportForm',
                    PGToken: that.token,
                    ReportAction: "icoach!coachInviteDetail.action",
                    DynamicParam: "inviteId=" + inviteId
                },
                success: function (data) {
                    data = JSON.parse(data.Data.JsonResult);
                    if (data.error.err_code == 0) {
                        //处理data数据
                        var item = data.object;
                        var obj = {};
                        obj.inviteid = item.id;
                        obj.topic = item.topic;
                        obj.date = item.time;
                        obj.coacher = item.coacher;

                        //生成报表
                        that.generateCoachData(obj);
                    } else {
                        alert(data.error.err_msg);
                    }
                }
            });
            $.util.ajax({
                url: that.url,
                customerId: that.customerId,
                userId: that.userId,
                data: {
                    action: 'CommonRequestReportForm',
                    PGToken: that.token,
                    ReportAction: "icoach!coachAssesse.action",
                    DynamicParam: "inviteId=" + inviteId + "$type=teacher"
                },
                success: function (data) {
                    data = JSON.parse(data.Data.JsonResult);
                    if (data.error.err_code == 0) {
                        //处理data数据
                        var item = data.object;
                        var obj = {};
                        obj.inviteid = item.id;
                        obj.result = item.result;
                        obj.averageScore = item.avg_score;
                        obj.actionPlan = item.action_plan;

                        //生成报表
                        that.generateCoachStatData(obj);
                    } else {
                        alert(data.error.err_msg);
                    }
                }
            });
        },
        generateCoachData: function (object) {
            debugger;
            $("#topic").html("");
            $("#topic").append(object.topic);
            $("#coach").html("");
            $("#coach").append(object.coacher);
            $("#date").html("");
            $("#date").append(object.date);
        },
        generateCoachStatData: function (object) {
            debugger;
            var scores = object.result.split("|");

            $("#averagescore").html("");
            $("#averagescore").append(object.averageScore);
            $("#identify").html("");
            $("#identify").append(scores[0]);
            $("#align").html("");
            $("#align").append(scores[1]);
            $("#co-create").html("");
            $("#co-create").append(scores[2]);
            $("#close").html("");
            $("#close").append(scores[3]);
            $("#execute").html("");
            $("#execute").append(scores[4]);
            $("#actionplan").html("");
            $("#actionplan").append(object.actionPlan);

            if (scores.length > 0) {
                var st = this.strength(scores);
                var op = this.opportunity(scores);
                $("#strength").html("");
                $("#strength").append(st);
                $("#opportunity").html("");
                $("#opportunity").append(op);
            }

        },
        getAssessItemList: function() {
            var items = [];
            items[0] = "Identify joint Value Drivers";
            items[1] = "Align Objectives";
            items[2] = "Co-create and Sell the plan";
            items[3] = "Close the Sell";
            items[4] = "Excute Perfectly";
            return items;
        },
        maxNumber: function(array) {
            var max = array[0];
            for (var i = 1, len = array.length; i < len; i++) {
                if (array[i] > max) {
                    max = array[i];
                }
            }
            return max;
        },
        strength : function(array) {
            var strengthIndex = [];
            var max = this.maxNumber(array);
            for (var i = 0, len = array.length; i < len; i++) {
                if (array[i] == max) {
                    strengthIndex.push(i);
                }
            }

            var stiList = this.getAssessItemList();
            var sts = "";
            for (var j = 0, jl = strengthIndex.length; j < jl; j++) {
                var index = strengthIndex[j];
                sts = sts + stiList[index];
                sts = sts + ", ";
            }

            return sts;
        },
        minNumber: function(array) {
            var min = array[0];
            for (var i = 1, len = array.length; i < len; i++) {
                if (array[i] < min) {
                    min = array[i];
                }
            }
            return min;
        },
        opportunity : function(array) {
            var opportunityIndex = [];
            var min = this.minNumber(array);
            for (var i = 0, len = array.length; i < len; i++) {
                if (array[i] == min) {
                    opportunityIndex.push(i);
                }
            }
            
            var opiList = this.getAssessItemList();
            var ops = "";
            for (var j = 0, jl = opportunityIndex.length; j < jl; j++) {
                var index = opportunityIndex[j];
                ops = ops + opiList[index];
                ops = ops + ", ";
            }

            return ops;
        }
    };

    page.init();
});


