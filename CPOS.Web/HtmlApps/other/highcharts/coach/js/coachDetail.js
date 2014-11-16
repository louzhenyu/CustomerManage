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
                        obj.averageScore = item.avg_score;

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
                    ReportAction: "icoach!allCoachFeedback.action",
                    DynamicParam: "inviteId=" + inviteId
                },
                success: function (data) {
                    data = JSON.parse(data.Data.JsonResult);
                    var commentList = [];
                    if (data.error.err_code == 0) {
                        //处理data数据
                        var list = data.object;
                        list = list ? list : [];
                        for (var i = 0, len = list.length; i < len; i++) {
                            var obj = {};
                            obj.like = list[i].islike;
                            obj.dislike = list[i].dislike;
                            obj.date = list[i].create_time;
                            commentList.push(obj);
                        }

                        //生成报表
                        that.generateCoachStatData(commentList);
                    } else {
                        alert(data.error.err_msg);
                    }
                }
            });
        },
        generateCoachData: function (object) {
            $("#topic").html("");
            $("#topic").append(object.topic);
            $("#coach").html("");
            $("#coach").append(object.coacher);
            $("#date").html("");
            $("#date").append(object.date);
            $("#averagescore").html("");
            $("#averagescore").append(object.averageScore);
        },
        generateCoachStatData: function (array) {
            debugger;
            $("#likeList").html("");
            $("#dislikeList").html("");
            for (var i = 0, len = array.length; i < len; i++) {
                var likeRowString = "<li><p class=\"item\"><span>" + array[i].like + 
                    "</span></p><p class=\"item line\"><span>" + array[i].date +
                    "</span></p></li>";
                $("#likeList").append(likeRowString);
                var dislikeRowString = "<li><p class=\"item\"><span>" + array[i].dislike +
                    "</span></p><p class=\"item line\"><span>" + array[i].date +
                    "</span></p></li>";
                $("#dislikeList").append(dislikeRowString);
            }
        },
    };

    page.init();
});


