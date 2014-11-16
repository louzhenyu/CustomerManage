define(['jquery', 'bdTemplate', 'tools', 'kkpager', 'artDialog'], function ($) {

    var self = {
        ele: {
            pDelAll: $(".selectList"),
            section: $("#section"),
            table: $(".dataTable")
        }, page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
            this.loadData();
            this.initEvent();

            this.tipHtml = $("#opertip").html()//将弹层的html保存到变量中
            //移除页面中的tip，方便弹层的操作。
            $("#opertip").remove();
        },
        loadData: function () {
            this.loadNewsTypeList();
        },
        initEvent: function () {
            this.ele.section.delegate(".checkBox", "click", function () {
                $(this).toggleClass("on");
            }).delegate(".delBtn", "click", function (e) {
                var $this = $(this);
                id = $this.data("id");
                if (confirm("确认要删除本条资讯！")) {
                    if (id) {
                        self.delOper(id);
                    } else {
                        alert("该选项没有id,无法获取值");
                    }
                }
            }).delegate(".editIcon", "click", function () {
                var id = $(this).data("id");
                self.showDialog(id);
            });
            //批量删除
            this.ele.pDelAll.click(function (e) {
                var delIdsArr = [];
                $(".dataTable tr td.checkBox.on").each(function (i, t) {
                    delIdsArr.push(t.getAttribute("data-id"));
                });
                if (delIdsArr.length) {
                    alert("正在执行批量删除...请稍后..");
                    //执行批量删除
                    self.delOper(delIdsArr.join(","));
                    self.d.close();
                    alert("批量删除执行成功！");
                    //重新加载数据
                    self.loadNewsTypeList();
                } else {
                    alert("至少选择一项删除");
                }
                self.stopBubble(e);
            });
            //全选反选
            $(".selectListBox p").click(function () {
                var val = $(this).data("val");
                if (val == "select") {
                    self.ele.table.find("td.checkBox").addClass("on");
                } else {
                    self.ele.table.find("td.checkBox").removeClass("on");
                }
            });
            //添加分类
            $(".appInfoBtn").click(function () {
                self.showDialog();
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
                        var temp = $("#selType").html();
                        $("#pre-typeList").html(self.render(temp, { list: data.Data.TypeList }));
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        showDialog: function (id) {
            var _t = id ? "修改" : '添加';
            var d = dialog({
                title: _t + '分类',
                content: self.tipHtml,
                fixed: true
            });
            d.showModal();
            //为分类列表委托事件
            $("#pre-typeList").delegate("p", "click", function (e) {
                var $this = $(this);
                var val = $this.data("val");
                var level = $this.data("level");
                if (val) {
                    $this.parent().siblings().attr({ "data-val": val, "data-level": level }).html($(e.target).text());
                } else {
                    alert("该选项没有id，无法获取值");
                }
            });
            //对话框创建之后，载入分类列表
            this.loadNewsType();
            //如果是修改分类，载入数据
            if (id) {
                $.util.ajax({
                    url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                    type: "get",
                    data: {
                        action: "GetNewsTypesDetail",
                        TypeId: id
                    },
                    success: function (data) {
                        $("#pre-typeList .selected").attr("data-val", data.Data.ParentTypeId).attr("data-level", data.Data.TypeLevel).text(data.Data.ParentTypeName);
                        $("#typename").val(data.Data.NewsTypeName);
                    }
                });
            }
            $(".addBtn").click(function () {
                var _tname = $.trim($("#typename").val());
                if (_tname.length == 0) {
                    alert("请输入类型名称！");
                    return;
                }
                var _el = $("#pre-typeList .selected");
                var action = id ? "UpdateNewsTypes" : "AddNewsTypes";
                var data = { action: action, ParentId: _el.attr("data-val"), ParentLevel: _el.attr("data-level"), TypeName: _tname };
                if (id) data.TypeId = id;

                $.util.ajax({
                    url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                    type: "get",
                    data: data,
                    success: function (data) {
                        if (data.IsSuccess) {
                            alert("分类" + _t + "成功！");
                            self.loadNewsTypeList();
                        } else {
                            alert(data.Message);
                        }
                    }
                });
                close();
            });
            $(".cancelBtn").click(function () { close(); });

            var close = function () {
                d.close().remove();
            }
        },
        delOper: function (id) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "DelNewsType",
                    TypeIds: id
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
        loadNewsTypeList: function (n) {
            $("#footer").show();
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetTypeListPage",
                    SortField: "", //暂未实现
                    SortOrder: 0,
                    PageIndex: n ? n : self.page.pageIndex,
                    PageSize: self.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        self.renderPageList(data.Data.Page);
                        $("#footer").hide();
                    } else {
                        alert(data.Message);
                        this.ele.table.find("tbody").html("");
                        $("#footer").show().find("td").html("未检索到相关数据内容");
                    }
                    if (data.Data.PageCount > 0) {
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
                                this.selectPage(n);
                                self.loadNewsTypeList(n - 1);
                            },
                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                            getHref: function () {
                                return "#";
                            }
                        });
                    } else {
                        $('#kkpager').remove();
                    }
                }
            });
        },
        stopBubble: function (e) {
            if (e && e.stopPropagation) {

            } else {
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        },
        renderPageList: function (list) {
            var temp = $("#tableTemp").html();
            self.ele.table.find("tbody").html(self.render(temp, { list: list }));
        }
    , render: function (temp, data) {
        var render = bd.template(temp);
        return render(data || {});
    }
    };
    self.init();
});