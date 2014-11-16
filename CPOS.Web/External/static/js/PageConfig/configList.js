define(['jquery', 'configTemp', 'template', 'util', 'pagination'], function ($, temp) {
    var page = {
        ele: {
            section: $("#section"),
            pageList: $("#pageList tbody"),
            key: $("#keyInput"),
            name: $("#nameInput")
        },
        temp: temp,
        page: {
            pageIndex: 0,
            pageSize: 15
        },
        init: function () {
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.loadPageList();
        },
        initEvent: function () {
            this.ele.section.delegate("#searchBtn", "click", function () {
                self.loadPageList();
            });
            this.ele.section.delegate("#publicBtn", "click", function () {
                self.public();
            });
        },
        public: function () {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.CreateCustomerConfig",
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("发布成功");
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageList: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.GetSysPageList",
                data: {
                    Key: this.ele.key.val(),
                    Name: this.ele.name.val(),
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                beforeSend: function () {

                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            self.renderPageList(data.Data.PageList);

                            //debugger;
                            // 分页处理 begin
                            var pageNumber = Math.ceil(data.Data.TotalPageCount / self.page.pageSize);
                            if (pageNumber > 1) {
                                if (self.page.pageIndex == 0) {
                                    //总页数大于一，且第一页时注册分页
                                    self.ele.section.find('.pageWrap').show();
                                    self.ele.section.find('.pagination').jqPagination({
                                        current_page: self.page.pageIndex + 1,
                                        max_page: pageNumber,
                                        paged: function (page) {
                                            self.page.pageIndex = page - 1;
                                            self.loadPageList();
                                        }
                                    });
                                }
                            } else {
                                self.ele.section.find('.pageWrap').hide();
                            }
                            // 分页处理 end
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        renderPageList: function (list) {
            this.ele.pageList.html(this.render(this.temp.pageList, { list: list }));
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});