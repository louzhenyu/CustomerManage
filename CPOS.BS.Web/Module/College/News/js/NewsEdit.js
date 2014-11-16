define(['jquery', 'datepicker', 'bdTemplate', 'tools', 'kindeditor', 'formtojson'], function ($, Pikaday) {
    var KE = window.KindEditor;

    var self = {
        ele: {
            section: $("#section"),
            newsTypeSelect: $("#newsTypeSelect"),
            microNumsSelect: $("#microNumsSelect"),
            microTypeSelect: $("#microTypeSelect"),
            title: $("#title"),
            description: $("#description"),
            content: $("#description"),
            author: $("#author"),
            IsRelMicro: $("#IsRelMicro"),
            PublishTime: $("#PublishTime")
        },
        init: function () {
            this.newsId = $.util.getUrlParam("newsId");

            this.loadData();
            this.initEvent();

            //初始化kindeditor
            window.editor = KE.create('#editor_id',
            {
                uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx',
                fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true
            });
            //初始化datepicker
            new Pikaday(
            {
                field: $("#PublishTime")[0],
                format: "yyyy-MM-dd",
                minDate: new Date('2000-01-01'),
                maxDate: new Date('2020-12-31'),
                yearRange: [1900, 2050]
            });
        },
        loadData: function () {
            this.loadNewsType();
            this.loadMicroNums();
            this.loadMicroTypes();
            //加载详细信息
            if (self.newsId) {
                $(".commonTitle").text("修改资讯");
                this.loadNewsDetail();
            }
        },
        initEvent: function () {
            this.ele.section.delegate(".radioBox", "click", function () {
                $(this).toggleClass("on");
                $(this).siblings(".commonSelectWrap").toggle(500);
            }).delegate(".option", "click", function (e) {
                var $this = $(this);
                var val = $this.data("val");
                if (val) {
                    $this.parent().siblings().attr("data-val", val).html($(e.target).text());
                } else {
                    alert("该选项没有id，无法获取值");
                }
            }).delegate("#saveBtn", "click", function () {
                var selType = $("#newsTypeSelect .selected").attr("data-val");
                var selNumber = $("#microNumsSelect .selected").attr("data-val");
                var selMicroType = $("#microTypeSelect .selected").attr("data-val");
                var data = $("#frm").formtojson(self.newsId ? "UpdateNewsInfo" : "AddNewsInfo");
                data.NewsId = self.newsId;
                data.NewsTypeId = selType;
                data.MicroNumberID = selNumber;
                data.MicroTypeID = selMicroType;
                data.Content = editor.html();

                if ($("#IsRelMicro").hasClass("on"))
                    data.IsRelMicro = true;
                else
                    data.IsRelMicro = false;

                if (data.NewsTypeId.length == 0) {
                    alert("请选择分类");
                    return false;
                }
                if (data.NewsTitle.length == 0) {
                    alert("标题不能为空");
                    return false;
                }
                if (data.Content.length == 0) {
                    alert("内容不能为空");
                    return false;
                }
                if (data.IsRelMicro) {
                    if (!data.MicroNumberID) {
                        alert("请选择刊号");
                        return false;
                    }
                    if (!data.MicroTypeID) {
                        alert("请选择版块");
                        return false;
                    }
                }

                self.saveAction(data);
            });
            //取消
            $("#cancelBtn").click(function () { history.back(); });
        },
        loadNewsType: function () {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetNewsTypes"
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        var temp = $("#selType").html();
                        self.ele.newsTypeSelect.html(self.render(temp, { list: data.Data.TypeList }));
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMicroNums: function () {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetMicroNums"
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        var temp = $("#selNumber").html();
                        self.ele.microNumsSelect.html(self.render(temp, { list: data.Data.NumberList }));
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMicroTypes: function () {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetMicroTypes"
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        var temp = $("#selMicroType").html();
                        self.ele.microTypeSelect.html(self.render(temp, { list: data.Data.MicroTypeList }));
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadNewsDetail: function () {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetNewsDetail",
                    NewsId: self.newsId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        //没有获取到详细信息
                        if (!data.Data || data.Data.NewsDetail.length == 0) {
                            alert("网络超时、请重试……");
                            location.href = "/Module/College/News/NewsList.aspx";
                        } else {
                            var detail = data.Data.NewsDetail[0];
                            //作者
                            $("#author").val(detail.Author);
                            //标题
                            $("#NewsTitle").val(detail.NewsTitle);
                            //描述
                            $("#Intro").val(detail.Intro);
                            //内容
                            editor.html(detail.Content)
                            //缩略图
                            //$(".uploadBtn").val(data.NewsDetail.ThumbnailImageUrl);
                            //加载分类
                            $("#newsTypeSelect .selected").attr("data-val", detail.NewsTypeId).text($("p.option[data-val='" + detail.NewsTypeId + "']").text());
                            //关联微刊
                            if (data.IsRelMicro) {
                                $("#IsRelMicro").addClass("on");
                                $("#microNumsSelect .selected").attr("data-val", detail.MicroNumberID).text($("p.option[data-val='" + detail.MicroNumberID + "']").text());
                                $("#microTypeSelect .selected").attr("data-val", detail.MicroTypeID).text($("p.option[data-val='" + detail.MicroTypeID + "']").text());
                                //关联展示
                                $(".commonSelectWrap").show();
                            }
                            //发布时间
                            $("#PublishTime").val(detail.PublishTime);
                        }
                    } else {
                        alert(data.Message);
                        location.href = "/Module/College/News/NewsList.aspx";
                    }
                }
            });
        },
        saveAction: function (obj) {
            var o = obj;
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "post",
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("保存成功！");
                        location.href = "NewsList.aspx";
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        render: function (temp, data) {
            var render = bd.template(temp);
            return render(data || {});
        }
    };
    self.init();
});