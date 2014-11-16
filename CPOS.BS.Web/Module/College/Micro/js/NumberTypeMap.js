define(['jquery', 'bdTemplate', 'kkpager', 'tools', 'artDialog'], function ($) {
    var self = {
        ele: {
            lbNum: $("#lbNum"),
            lbTitle: $("#lbTitle"),
            lbTime: $("#lbTime"),
            imSrc_f: $("#imSrc_f"),
            numObj: $("#dvNumber"),
            tyObj: $("#dvType"),
            section: $("#section"),
            table: $("#table")
        },
        model: {
            typeModel: "",
            numModel: ""
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
            this.loadData();
            this.initEvent();
            this.initModel();
        },
        loadData: function () {
            var id = $.util.getUrlParam("id");
            this.loadNumberDetail(id, true);
            this.loadTypeList();
        },
        initEvent: function () {
            this.ele.section.delegate(".appInfoBtn", "click", function (e) {
                self.typeModel();
            }).delegate(".checkBox", "click", function () {
                $(this).toggleClass("on");
            }).delegate(".delBtn", "click", function (e) {
                var $this = $(this);
                id = $this.data("id");

                if (confirm("确认要删除本条板块！")) {
                    if (id) {
                        self.delTypeOper(id);
                    } else {
                        alert("该选项没有id,无法获取值");
                    }
                }
            }).delegate(".editIcon", "click", function () {
                var id = $(this).data("id");
                self.typeModel(id);
            }).delegate(".editNum", "click", function () {

                //获取Url键值
                var id = $.util.getUrlParam("id");
                self.numModel(id);
            }).delegate(".getBack", "click", function () {
                window.location.href = "/Module/College/Micro/NumberList.aspx";
            }).delegate(".cancelBtn", "click", function () {
                window.location.href = "/Module/College/Micro/NumberList.aspx";
            }).delegate(".savePage", "click", function () {
                debugger;
                //获取Url 键值
                var id = $.util.getUrlParam("id");
                if (!id) {
                    alert("网络异常请重试……");
                    window.location.href = "/Module/College/Micro/NumberList.aspx";
                    return;
                }
                var idsArr = [];
                $(".dataTable tr td.checkBox.on").each(function (i, t) {
                    idsArr.push(t.getAttribute("data-id"));
                });
                if (idsArr.length) {
                    self.saveCurrentPage(id, idsArr.join(","));
                } else {
                    alert("至少选择一项信息关联保存");
                }
            });
        },
        initModel: function () {
            self.model.typeModel = self.ele.tyObj.html();
            self.ele.tyObj.remove();

            self.model.numModel = self.ele.numObj.html();
            self.ele.numObj.remove();
        },
        typeModel: function (id) {//板块模板
            var _t = id ? "更新板块" : '创建板块';

            //自定义弹框
            var d = dialog({
                title: _t,
                content: self.model.typeModel,
                fixed: true
            });

            //弹框
            d.showModal();

            //加载类型
            self.loadMicroTypes()

            //获取详细信息：更新操作
            if (id) {
                self.loadTypeDetail(id);
            }

            //绑定模拟下拉框事件
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
            $(".addBtn").click(function () {
                debugger;
                var pType = $("#pre-typeList > span"); //父类型对象
                var name = $("#ipname"); //板块名称对象
                var des = $("#ipDesc"); //板块描述对象
                var thumUrl = $("#dvimgUrl"); //板块缩略图对象

                if (!name.val()) {
                    alert("板块名称不能为空！");
                    return;
                }
                if (!des.val()) {
                    alert("板块描述不能为空！");
                    return;
                }
                //                if (thumUrl) {//暂时省略
                //                    alert("");
                //                    return;
                //                }

                //请求参数
                var params = {};
                params.MicroTypeName = name.val();
                params.ParentID = pType.data("val");
                params.ParentLevel = pType.data("level");
                params.Intro = des.val();
                params.Sequence = 1000;

                if (id) {//更新
                    params.MicroTypeID = id;
                    params.action = "MicroIssueTypeUpdate";
                    self.addTypeOper(params);
                }
                else { //新增
                    params.action = "MicroIssueTypeAdd";
                    self.addTypeOper(params);
                }
                close();
            });

            //绑定取消按钮事件
            $(".cancelBtn").click(function () { close(); });

            //定义弹框关闭函数
            var close = function () {
                d.close().remove();
            }
        },
        numModel: function (id) {//期刊模板
            //自定义模板
            var d = dialog({
                title: "更新期刊",
                content: this.model.numModel,
                fixed: true
            });
            //弹框
            d.showModal();

            //判断是否更新
            if (id) {
                self.loadNumberDetail(id);
            }

            //保存事件
            $(".addBtn").click(function () {
                if (!id) {
                    alert("网络异常，请重试……！");
                    window.location.href = "/Module/College/Micro/NumberList.aspx";
                    return;
                }
                var num = $("#ipNum").val(); //刊号对象
                var title = $("#ipTitle").val(); //标题对象
                var imgThum = $("#imgThum").attr("src"); //缩略图对象

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

                //请求参数
                var params = {
                    action: "MicroIssueNperUpdate",
                    MicroNumberID: id,
                    MicroNumberName: title,
                    MicroNumber: num,
                    ConverPath: imgThum
                };
                //执行更新
                self.upNumberInfo(params);

                close();
            });
            //绑定取消按钮事件
            $(".cancelBtn").click(function () { close(); });

            //定义弹框关闭函数
            var close = function () {
                d.close().remove();
            }
        },
        saveCurrentPage: function (id, ids) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "post",
                data: {
                    action: "SetNumberTypeMapping",
                    NumberId: id,
                    TypeIds: ids
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("保存成功！");
                        self.loadTypeList();
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        addTypeOper: function (obj) {//新增板块
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "post",
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("保存成功！");
                        self.loadTypeList();
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        delTypeOper: function (id) {//删除板块
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "post",
                data: {
                    action: "MicroIssueTypeDelete",
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
        loadTypeDetail: function (id) {//加载板块详细
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "post",
                data: {
                    action: "MicroIssueTypeDetail",
                    MicroTypeID: id
                },
                success: function (data) {
                    debugger;
                    if (data.IsSuccess) {
                        var resJson = data.Data.EclubMicroType[0];
                        var pType = $("#pre-typeList > span");
                        pType.attr({ "data-level": resJson.TypeLevel, "data-val": resJson.ParentID });
                        if (resJson.ParentTypeName) {
                            pType.text(resJson.ParentTypeName);
                        }
                        $("#ipname").val(resJson.MicroTypeName);
                        $("#ipDesc").val(resJson.Intro);
                        $("#dvimgUrl").attr("src", resJson.IconPath);
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        upNumberInfo: function (obj) {//更新期刊信息
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "post",
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("保存成功！");

                        //动态更新版面展示
                        self.ele.lbNum.text(obj.MicroNumber);
                        self.ele.lbTitle.text(obj.MicroNumberName);
                        self.ele.imSrc_f.attr("src", obj.ConverPath);
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadMicroTypes: function () {//加载板块类型信息
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetMicroTypes"
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        var temp = $("#selType").html();
                        $("#pre-typeList").html(self.render(temp, { list: data.Data.MicroTypeList }));
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadNumberDetail: function (id, b) {//加载期刊详细            
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/MicroIssueGateway.ashx",
                type: "get",
                data: {
                    action: "MicroNumberDetail",
                    MicroNumberID: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (b) {
                            self.ele.lbNum.text(data.Data.EclubMicroNumber.MicroNumber);
                            self.ele.lbTitle.text(data.Data.EclubMicroNumber.MicroNumberName);
                            self.ele.imSrc_f.attr("src", data.Data.EclubMicroNumber.CoverPath);
                            self.ele.lbTime.text(data.Data.EclubMicroNumber.CreateTime.replace(/T\d{2}:\d{2}:\d{2}.\d{0,3}$/, ""));
                        } else {
                            $("#ipNum").val(data.Data.EclubMicroNumber.MicroNumber);
                            $("#ipTitle").val(data.Data.EclubMicroNumber.MicroNumberName);
                            $("#imgThum").attr("src", data.Data.EclubMicroNumber.CoverPath);
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadNumTypeMap: function (id) {
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    action: "GetNumberTypeMapping",
                    NumberId: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        //循环遍历
                        var res = data.Data.TypeIds.split(',');
                        $(".ckOper").each(function (k, v) {
                            $(res).each(function (i, o) {
                                var obj = $(v);
                                if (obj.data("id") === o) {
                                    obj.addClass("on");
                                } else {
                                    obj.removeClass("on");
                                }
                            });
                        });

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadTypeList: function (n) {//加载板块列表
            $("#footer").show();
            $.util.ajax({
                url: "/ApplicationInterface/NwNews/NwNewsGateway.ashx",
                type: "get",
                data: {
                    SortField: "",
                    SortOrder: 0,
                    PageIndex: n ? n : self.page.pageIndex,
                    PageSize: self.page.pageSize,
                    action: "GetMicroTypeListPage"
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        self.renderPageList(data.Data.MicroTypeList);

                        //初始化已选择事项
                        var id = $.util.getUrlParam("id");
                        if (id) {
                            self.loadNumTypeMap(id);
                        }
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
                                self.loadTypeList(n);
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
        renderPageList: function (list) {//渲染列表页
            var temp = $("#tableTemp").html();
            self.ele.table.find("tbody").html(self.render(temp, { list: list }));
        },
        render: function (temp, data) {//渲染引擎
            var render = bd.template(temp);
            return render(data || {});
        }
    };
    self.init();
});