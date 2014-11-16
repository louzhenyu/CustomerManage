define(['jquery', 'datepicker', 'bdTemplate', 'tools', 'kkpager', 'pagination'], function ($, Pikaday) {
    var self = {
        ele: {
            keyword: $("#ipkw"),
            starTime: $("#starTime"),
            endTime: $("#endTime"),
            section: $("#section"),
            table: $("#table")
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
            this.loadData();
            this.initEvent();

            //初始化datepicker
            var sdate = new Pikaday({
                field: $("#starTime")[0],
                format: "yyyy-MM-dd",
                minDate: new Date('2000-01-01'),
                maxDate: new Date('2020-12-31'),
                yearRange: [1900, 2050]
            });
            var edate = new Pikaday(
            {
                field: $("#endTime")[0],
                format: "yyyy-MM-dd",
                minDate: new Date('2000-01-01'),
                maxDate: new Date('2020-12-31'),
                yearRange: [1900, 2050]
            });
        },
        loadData: function () {
            this.loadNewsType();
            this.loadPageList(self.page.pageIndex);
        },
        initEvent: function () {
            this.ele.section.delegate(".checkBox", "click", function () {
                $(this).toggleClass("on");
            }).delegate(".option", "click", function (e) {
                var $this = $(this);
                if ($this.attr("data-val")) {
                    $this.parent().siblings().attr("data-val", $this.attr("data-val")).html($(e.target).text());
                } else {
                    alert("该选项没有id，无法获取值");
                }
            }).delegate(".delBtn", "click", function (e) {
                var $this = $(this),
                    id = $this.data("id");
                if (confirm("确认要删除本条咨询！")) {
                    if (id) {
                        self.delNewsInfo(id);
                    } else {
                        alert("该选项没有id，无法获取值");
                    }
                }
            }).delegate(".editBtn", "click", function (e) {
                var $this = $(this);
                var id = $this.data("id");
                if (id) {
                    window.location.href = "/Module/College/News/NewsEdit.aspx?newsId=" + id;
                } else {
                    alert("该项没有id，无法获取值");
                }
            }).delegate(".queryBtn", "click", function (e) {
                self.loadPageList(self.page.pageIndex);
            });
            //全选反选
            $(".selectListBox p").click(function () {
                var val = $(this).data("val");
                if (val == "select")
                    self.ele.table.find("td.checkBox").addClass("on");
                else
                    self.ele.table.find("td.checkBox").removeClass("on");
            });
        },
        delNewsInfo: function (id) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "DelNewsInfo",
                    NewsIds: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        $("#" + id).remove();
                    } else {
                        alert(data.Message);
                    }
                }
            });
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
                        var list = [];
                        for (var i = 0; i < data.Data.TypeList.length; i++) {
                            list.push({ id: data.Data.TypeList[i].NewsTypeId, name: data.Data.TypeList[i].NewsTypeName });
                        }
                        var temp = $("#selectTemp").html();
                        $("#newsTypeSelect").html(self.render(temp, { list: list }));

                        //$.util.selectEvent(".type-con");
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageList: function (pgIndex) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetNewsList",
                    NewsTypeId: $("#stype").attr("data-val"),
                    Keyword: self.ele.keyword.val(),
                    StartDate: self.ele.starTime.val(),
                    EndDate: self.ele.endTime.val(),
                    SortField: "", //暂未实现
                    SortOrder: "0",
                    PageIndex: pgIndex ? pgIndex : this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        self.renderPageList(data.Data.Page);
                    } else {
                        alert(data.Message);
                        this.ele.table.find("tbody").html("");
                        $("#footer").show().find("td").html("未检索到相关数据内容");
                        $("#footer").hide();
                    }
                    if (data.Data.PageCount > 0) {
                        //避免重复render分页html
                        if (!kkpager.inited) {
                            kkpager.generPageHtml({
                                pno: 1,
                                mode: 'click', //设置为Click模式
                                total: data.Data.PageCount, //总页码
                                isShowTotalPage: false,
                                isShowTotalRecords: false,
                                click: function (n) {
                                    /*这里可以做自己的处理
                                    ...
                                    处理完后可以手动调用selectPage进行页码选中切换
                                    */
                                    var length = self.ele.table.find("thead th").length;
                                    self.ele.table.find("tbody").html('<tr><td colspan="' + (length + 1) + '" align="center"><span><img src="../CommRes/images/loading.gif"></span></td></tr>');

                                    this.selectPage(n);
                                    self.loadPageList(n - 1);
                                },
                                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                getHref: function () {
                                    return "#";
                                }
                            });
                        }
                    } else {
                        $('#kkpager').remove();
                    }
                }
            });
        },
        renderPageList: function (list) {
            var temp = $("#tableTemp").html();
            this.ele.table.find("tbody").html(this.render(temp, { list: list }));
        },
        render: function (temp, data) {
            var render = bd.template(temp);
            return render(data || {});
        }
    };
    self.init();
});