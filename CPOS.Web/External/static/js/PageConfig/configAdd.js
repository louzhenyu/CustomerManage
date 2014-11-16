define(['jquery', 'configTemp', 'template', 'util', 'pagination'], function ($, temp) {
    var page = {
        ele: {
            section: $("#section"),
            author: $("#author"),
            version: $("#version"),
            pageJson: $("#pageJson"),

            packageLayer: $("#packageLayer"),
            packageList: $("#packageList"),
            mask: $(".ui-mask")

        },
        temp: temp,
        page: {
            pageIndex: 0,
            pageSize: 5
        },
        init: function () {
            this.pageId = $.util.getUrlParam("id");

            //debugger;
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            if (this.pageId) {
                this.loadPageInfo();
            }
        },
        initEvent: function () {
            this.ele.section.delegate("#submitBtn", "click", function () {
                var author, version, pageJson;
                author = self.ele.author.val();
                version = self.ele.version.val();
                pageJson = self.ele.pageJson.val();

                if (author == "") {
                    alert("请输入作者");
                    return false;
                }
                if (version == "") {
                    alert("请输入版本号");
                    return false;
                }
                if (pageJson == "") {
                    alert("请编辑pageJson");
                    return false;
                } else {
                    var json;
                    try {
                        json = JSON.parse(pageJson)
                    } catch (error) {
                        alert("json format error");
                        return false;
                    }
                }
                var obj = {};
                obj.Author = author;
                obj.Version = version;
                obj.PageJson = pageJson;
                if (self.pageId) {
                    obj.PageId = self.pageId;
                }

                self.submit(obj);
            }).delegate("#cancelBtn", "click", function () {
                window.history.go(-1);

            }).delegate("#applyBtn", "click", function () {
                if (self.pageId) {
                    self.packageLayer.loadDate();
                    self.packageLayer.show();
                } else {
                    alert("请先创建页面");
                }

            });

            this.ele.packageLayer.delegate(".saveBtn", "click", function () {
                var list = [];
                $("#packageList").find(".on").each(function (i, e) {
                    list.push($(e).data("id"));
                });
                if (!list.length) {
                    alert("至少应用到一个套餐");
                    return false;
                }

                var obj = {
                    PageId: self.pageId,
                    VocaVerMappingID: list
                };
                debugger;
                self.apply(obj, function () {
                    alert("已提交，返回列表页");
                    window.location = "configList.aspx";
                });

            }).delegate(".cancelBtn", "click", function () {
                self.packageLayer.hide();
            }).delegate(".jsListItem", "click", function () {
                $(this).toggleClass("on");
            });
        },
        apply: function (param, callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.SetModulePageMapping",
                data: param,
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            alert("提交成功!");
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },

        submit: function (param) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.SetSysPage",
                data: param,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("提交成功!");
                        window.location = "configList.aspx";

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageInfo: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.GetSysPageDetail",
                data: {
                    PageId: this.pageId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            var idata = data.Data.PageInfo[0];
                            self.ele.author.val(idata.Author);
                            self.ele.version.val(idata.Version);
                            self.ele.pageJson[0].innerHTML = idata.PageJson;
                            // 添加映射表
                            self.mappingList = [];
                            if (data.Data.ModulePageMappingInfo && data.Data.ModulePageMappingInfo.length) {
                                for (var i = 0; i < data.Data.ModulePageMappingInfo.length; i++) {
                                    self.mappingList.push(data.Data.ModulePageMappingInfo[i].VocaVerMappingID);
                                }
                            }

                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        packageLayer: {
            show: function (callback) {
                self.ele.packageLayer.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.ele.packageLayer.hide();
                self.mask.hide();
            },
            loadDate: function (callback) {

                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    action: "WX.SysPage.GetVocationVersionMappingList",
                    data: {
                        pageIndex: self.page.pageIndex,
                        pageSize: self.page.pageSize
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            } else {
                                var versionList = data.Data.VocationVersionList;
                                for (var i = 0, idata; i < versionList.length; i++) {
                                    idata = versionList[i];
                                    if (self.mappingList != 0 && $.inArray(idata.VocaVerMappingID, self.mappingList) != -1) {
                                        versionList[i].select = true;
                                    } else {
                                        versionList[i].select = false;
                                    }
                                }
                                self.ele.packageList.html(self.render(self.temp.packageList, { list: versionList }));

                                //debugger;
                                // 分页处理 begin
                                var pageNumber = Math.ceil(data.Data.TotalPageCount / self.page.pageSize);
                                if (pageNumber > 1) {
                                    if (self.page.pageIndex == 0) {
                                        //总页数大于一，且第一页时注册分页
                                        self.ele.packageLayer.find('.pageWrap').show();
                                        self.ele.packageLayer.find('.pagination').jqPagination({
                                            current_page: self.page.pageIndex + 1,
                                            max_page: pageNumber,
                                            paged: function (page) {
                                                self.page.pageIndex = page - 1;
                                                self.packageLayer.loadDate();
                                            }
                                        });
                                    }
                                } else {
                                    self.ele.packageLayer.find('.pageWrap').hide();
                                }
                                // 分页处理 end
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
        },
        mask: {
            show: function () {
                self.ele.mask.show();
            },
            hide: function () {
                self.ele.mask.hide();
            }
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});