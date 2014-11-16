define(['jquery', 'datepicker', 'bdTemplate', 'tools', 'kkpager', 'pagination', 'artDialog', 'mooarray'], function ($, Pikaday) {
    var self = {
        ele: {
            section: $("#section"),
            tab: $("#tabMenu"),
            table: $("#table")
        },
        page: {
            pageIndex: 0,
            pageSize: 2,
            pageTotal: 0
        },
        pageDialog: {
            pageIndex: 0,
            pageSize: 5
        },
        init: function () {
            //初始化参数
            this.typeId = $.util.getUrlParam("TypeId");
            this.numberId = $.util.getUrlParam("NumberId");

            //将弹层的html保存到变量中
            this.tipHtml = $("#dvNewsMicro").html()
            //移除页面中的tip，方便弹层的操作。
            $("#dvNewsMicro").remove();

            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.loadMicroInfo();
            this.loadMicroTypeMaps();
        },
        initEvent: function () {
            this.ele.section.delegate(".checkBox", "click", function () {
                $(this).toggleClass("on");
            }).delegate(".option", "click", function (e) {
                var $this = $(this);
                if ($this.attr("data-val")) {
                    $this.parent().siblings().attr("data-val", $this.attr("data-val")).html(e.target.innerText);
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
            })
            //全选反选
            $(".selectListBox p").click(function () {
                var val = $(this).data("val");
                if (val == "select")
                    self.ele.table.find("td.checkBox").addClass("on");
                else
                    self.ele.table.find("td.checkBox").removeClass("on");
            });
            //添加资讯按钮事件
            $(".appInfoBtn").click(function () {
                var d = dialog({
                    title: "设置关联资讯",
                    content: self.tipHtml,
                    fixed: true,
                    onclose: function () {
                        //弹层关闭后需要手工再次绑定分页控件
                        //因为kkpager是一个全局对象，list页的对象属性已经被改变。
                        self.bindPageClick();
                    }
                });
                d.showModal();

                //载入下拉框
                $.util.ajax({
                    url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                    type: "get",
                    data: {
                        action: "GetNewsTypes"
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            var temp = $("#selType").html();
                            $("#sel_newstype").html(self.render(temp, { list: data.Data.TypeList }));

                            //绑定下拉框选择事件
                            $("#sel_newstype .option").click(function (e) {
                                var $this = $(this);
                                var val = $this.data("val");
                                if (val) {
                                    $this.parent().siblings().attr("data-val", val).html($(e.target).text());
                                } else {
                                    alert("该选项没有id，无法获取值");
                                }
                            });
                        } else {
                            alert(data.Message);
                        }
                    }
                });
                //绑定日期
                new Pikaday(
                {
                    field: $("#sdate")[0],
                    format: "yyyy-MM-dd",
                    minDate: new Date('2000-01-01'),
                    maxDate: new Date('2020-12-31'),
                    yearRange: [1900, 2050]
                });
                new Pikaday(
                {
                    field: $("#edate")[0],
                    format: "yyyy-MM-dd",
                    minDate: new Date('2000-01-01'),
                    maxDate: new Date('2020-12-31'),
                    yearRange: [1900, 2050]
                });
                //绑定查询事件
                $(".queryBtn").click(function () {
                    self.loadDialogNews();
                });
                //选中
                $("#dig-body").delegate(".checkBox", "click", function () {
                    $(this).toggleClass("on");
                });
                //保存事件



                $(".cancelBtn").click(function () { close(); });
                var close = function () {
                    d.close().remove();
                }
            });
        },
        loadDialogNews: function () {
            this.loadNewsData(function (data) {
                //渲染Table
                self.renderDialogTable(data.Data.Page);
                //加载分页控件
                if (data.Data.PageCount > 0) {
                    $("#dig-pg-con").html("<div id='dig-pager' style='text-align:center'></div>");
                    kkpager.generPageHtml({
                        pagerid: "dig-pager",
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
                            $("#dig-body").html('<tr><td colspan="2" style="text-align:center;"><span><img src="../CommRes/images/loading.gif"></span></td></tr>');

                            this.selectPage(n);
                            self.loadMoreNewsData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function () {
                            return "#";
                        }
                    }, true);
                } else {
                    $('#dig-pager').remove();
                }
            });
        },
        //点击dialog的查询载入数据
        loadNewsData: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetNewsList",
                    NewsTypeId: $("#sel_newstype .selected").attr("data-val"),
                    Keyword: $("#keywords").val(),
                    StartDate: $("#sdate").val(),
                    EndDate: $("#edate").val(),
                    PageIndex: this.pageDialog.pageIndex,
                    PageSize: this.pageDialog.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMoreNewsData: function (currentPage) {
            this.pageDialog.pageIndex = currentPage - 1;
            this.loadNewsData(function (data) {
                self.renderDialogTable(data.Data.Page);
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
        loadMicroInfo: function () {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "get",
                data: {
                    action: "MicroNumberDetail",
                    MicroNumberID: self.numberId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        var detail = data.Data.EclubMicroNumber;
                        $("#micro_num").text(detail.MicroNumber);
                        $("#micro_title").text(detail.MicroNumberName);
                        $("#micro_time").text(detail.CreateTime.substring(0, 10));
                        //$("#micro_cover").attr("src",detail.CoverPath);
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMicroTypeMaps: function () {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "get",
                data: {
                    action: "GetNumberTypeSum",
                    NumberId: self.numberId
                },
                success: function (d) {
                    if (d.IsSuccess) {
                        var data = d.Data.Summarys;
                        var _s = "";
                        for (i in data) {
                            _s += "<a href='javascript:;' data-id='" + data[i].TypeId + "'><span>" + data[i].TypeName + "(<em>" + data[i].TypeCount + "</em>)</span></a>";
                        }
                        self.ele.tab.html(_s);
                        self.ele.tab.find("a").click(function () {
                            //绑定click事件
                            self.page.pageIndex = 0;
                            self.typeId = $(this).data("id");
                            self.loadMappingNews();

                            _select();
                        });

                        var _select = function () {
                            self.ele.tab.children("a").removeClass("on");
                            self.ele.tab.find("a[data-id='" + self.typeId + "']").addClass("on");
                        }

                        //第一次载入时根据TypeId判断哪一个tab被加载
                        //如果没有传递TypeId，默认第一个tab被激活
                        self.ele.tab.find("a[data-id='" + self.typeId + "']").click();
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMappingNews: function () {
            this.loadMappingData(function (data) {
                //渲染Table
                self.renderTable(data.Data.Page);
                //加载分页控件
                if (data.Data.PageCount > 0) {
                    $("#pageContianer").html("<div id='kkpager' style='text-align:center'></div>");
                    //记录tabs对应的pagecount
                    //否则dialog里面的分页会重写了tabs的pagecount属性
                    self.page.pageTotal = data.Data.PageCount;
                    //绑定分页
                    self.bindPageClick();
                } else {
                    $('#kkpager').remove();
                }
            });
        },
        //绑定分页事件
        bindPageClick: function () {
            kkpager.generPageHtml({
                pagerid: "kkpager",
                pno: self.page.pageIndex + 1,
                mode: 'click',
                total: self.page.pageTotal,
                isShowTotalPage: false,
                isShowTotalRecords: false,
                click: function (n) {
                    var length = self.ele.table.find("thead th").length;
                    self.ele.table.find("tbody").html('<tr><td colspan="' + (length + 1) + '" align="center"><span><img src="../CommRes/images/loading.gif"></span></td></tr>');

                    this.selectPage(n);
                    self.loadMoreMappingData(n);
                },
                getHref: function () {
                    return "#";
                }
            }, true);
        },
        loadMappingData: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetNewsMappList",
                    MicroNumberId: self.numberId,
                    MicroTypeId: self.typeId,
                    SortField: "", //暂未实现
                    SortOrder: "0",
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMoreMappingData: function (currentPage) {
            this.page.pageIndex = currentPage - 1;
            this.loadMappingData(function (data) {
                self.renderTable(data.Data.Page);
            });
        },
        //渲染tab选项卡list
        renderTable: function (list) {
            var temp = $("#tableTemp").html();
            this.ele.table.find("tbody").html(this.render(temp, { list: list }));
        },
        //渲染dialog的list
        renderDialogTable: function (list) {
            var temp = $("#dialogNews").html();
            $("#dig-body").html(this.render(temp, { list: list }));
        },
        render: function (temp, data) {
            var render = bd.template(temp);
            return render(data || {});
        }
    };
    self.init();
});