define(['jquery', 'template', 'tools', 'kkpager', 'artDialog'], function ($) {
    template.openTag = '<$';
    template.closeTag = '$>';
    var self = {
        ele: {
            appInfoBtn: $(".appInfoBtn"),
            spMicroNumsText: $("#microNumsText"),
            divMicroNums: $("#microNums"),
            inKeyword: $("#inKeyword"),
            queryBtn: $(".queryBtn"),
            pDelAll: $(".selectList"),
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
            this.tipHtml = $("#dvMicroNumer").html()//将弹层的html保存到变量中
            //移除页面中的tip，方便弹层的操作。
            $("#dvMicroNumer").remove();
        },
        loadData: function () {
            this.loadMicroNums();
            this.loadPageList();
        },
        initEvent: function () {
            this.ele.section.delegate(".checkBox", "click", function () {
                $(this).toggleClass("on");
            }).delegate(".option", "click", function (e) {
                var $this = $(this);
                if ($this.attr("data-val")) {
                    $this.parent().siblings().attr("data-val", $this.attr("data-val")).html(e.target.innerText);
                } else {
                    alert("该选项没有Id,无法获取值");
                }
            }).delegate(".delBtn", "click", function (e) {
                var $this = $(this);
                id = $this.data("id");
                if (confirm("确认要删除本条期刊！")) {
                    if (id) {
                        self.microIssueNperDelete(id);
                    } else {
                        alert("该选项没有id,无法获取值");
                    }
                }
            }).delegate(".editBtn", "click", function (e) {
                debugger;
                var $this = $(this);
                var id = $this.data("id");
                if (id) {
                    window.location.href = "/Module/College/Micro/NumberTypeMap.aspx?id=" + id;
                    //                    var params = {};
                    //                    params.id = id;
                    //                    params.title = "更新期刊";
                    //                    params.action = "MicroIssueNperUpdate";
                    //                    self.editModal(params);
                } else {
                    alert("该项没有id，无法获取值");
                }
            });
            //查询
            this.ele.queryBtn.click(function () {
                self.loadPageList();
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
                    self.microIssueNperDelete(delIdsArr.join(","));
                    self.d.close();
                    alert("批量删除执行成功！");
                    //重新加载数据
                    self.loadPageList();
                } else {
                    alert("至少选择一项删除");
                }
                self.stopBubble(e);
            });
            //创建期刊
            this.ele.appInfoBtn.click(function (e) {
                var params = {};
                params.title = "创建期刊";
                params.action = "MicroIssueNperAdd";
                self.editModal(params);
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
        },
        editModal: function (obj) {//编辑模板
            var d = dialog({
                title: obj.title,
                content: self.tipHtml,
                fixed: true
            });

            d.showModal();
            debugger;
            //编辑
            if (obj.id) {
                debugger;
                self.loadMicroNumDetail(obj.id);
            }


            $(".addBtn").click(function () {
                var num = $("#ipNum").val();
                var title = $("#ipTitle").val();
                var imgThum = $("#imgThum").attr("src");

                if (obj.action == "MicroIssueNperUpdate" && !obj.id) {
                    alert("网络异常,请刷新后重试！");
                    close();
                    return;
                }
                if (!num) {
                    alert("刊号不能为空！");
                    return;
                } if (!title) {
                    alert("标题不能为空！");
                    return;
                }
                //if (!imgThum) {//暂时先省略
                //    alert("");
                //    return;
                //}
                debugger;
                var params = {
                    action: obj.action,
                    MicroNumberID: obj.id,
                    MicroNumberName: title,
                    MicroNumber: num,
                    ConverPath: imgThum
                };
                self.microIssueNperAddOrUp(params);
                close();
            });
            $(".cancelBtn").click(function () {
                close();
            });
            var close = function () {
                d.close().remove();
            }
        },
        microIssueNperAddOrUp: function (obj) {//新增或更新
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "post",
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("保存成功！");
                        self.loadPageList();
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        microIssueNperDelete: function (id) {//删除
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "get",
                data: {
                    action: "MicroIssueNperDelete",
                    NumberIds: id
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
        loadMicroNumDetail: function (id) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "get",
                data: {
                    action: "MicroNumberDetail",
                    MicroNumberID: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        debugger;
                        $("#ipNum").val(data.Data.EclubMicroNumber.MicroNumber);
                        $("#ipTitle").val(data.Data.EclubMicroNumber.MicroNumberName);
                        $("#imgThum").attr("src", data.Data.EclubMicroNumber.CoverPath);
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
                        var list = [];
                        for (var i = 0; i < data.Data.NumberList.length; i++) {
                            list.push({ id: data.Data.NumberList[i].MicroNumberID, name: data.Data.NumberList[i].MicroNumber });
                        }
                        var temp = $("#selectTemp").html();
                        $("#microNumsSelect").html(self.render(temp, { list: list }));
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageList: function (n) {
            $("#footer").show();
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "post",
                data: {
                    action: "GetNumberList",
                    Number: $("#microNumsText").attr("data-val"),
                    Keyword: this.ele.inKeyword.val(),
                    //SortField: "",//排序暂未实现
                    //SortOrder: "",
                    PageIndex: n ? n : this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        self.renderPageList(data.Data.Page);
                        $("#footer").hide();
                    } else {
                        alert(data.Message);
                        this.ele.table.find("tbody").html("");
                        $("#footer").show().find("td").html("未检索到相关数据内容");
                        $("#footer").hide();
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
                                self.loadPageList(n - 1);
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
                //因此它支持W3C的stopPropagation()方法 
                e.stopPropagation();
            }
            else {
                //否则，我们需要使用IE的方式来取消事件冒泡 
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        },
        renderPageList: function (list) {
            var temp = $("#tableTemp").html();
            this.ele.table.find("tbody").html(this.render(temp, { list: list }));
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };
    self.init();
});