define(['jquery', '/External/PageConfig/js/tempModel.js', 'template', 'tools', 'pagination'], function ($, temp) {
    var page = {
        ele: {
            section: $("#section"),
            packageList: $("#packageList")
        },
        temp: temp,
        page: {
            pageIndex: 0,
            pageSize: 1000  //5  设置不分页
        },
        init: function () {
            this.customerId = $.util.getUrlParam("customerId");
            this.customerModuleList = null;
            if (!this.customerId) {
                alert("url获取不到参数customerId");
                return false;
            }
            //debugger;
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.getMappingList();
        },
        initEvent: function () {
            this.ele.section.delegate(".saveBtn", "click", function () {
                var list = [];
                $("#packageList").find(".on").each(function (i, e) {
                    list.push($(e).data("id"));
                });
                if (!list.length) {
                    alert("请选择一个套餐");
                    return false;
                }

                var obj = {
                    CustomerId: self.customerId,
                    VocaVerMappingID: list.join(",")
                };
                self.apply(obj);
            }).delegate(".cancelBtn", "click", function () {
                window.history.go(-1);
            }).delegate(".jsListItem", "click", function () {
                $(this).toggleClass("on");
                $(this).siblings().removeClass("on");
            });

        },
        getCustomerModuleMapping:function(callback){
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.GetCustomerModuleMapping",
                data: {
                    CustomerId:this.customerId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        var mappingList = data.Data.CustomerModuleMapping;
                        if(mappingList&&mappingList.length){
                            self.customerModuleList = mappingList;
                        }else{
                            self.customerModuleList = [];
                        }

                        if (callback) {
                            callback(data.Data);
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        getMappingList: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.GetVocationVersionMappingList",
                data: {
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    //debugger;
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            if(!self.customerModuleList){
                                self.getCustomerModuleMapping(function(){
                                    self.processRenderAndPager(data);
                                });
                            }else{
                                self.processRenderAndPager(data);
                            }

                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        processRenderAndPager:function(data){
            //debugger;
            var self = this;
            var versionList = data.Data.VocationVersionList;
            for(var i=0;i<self.customerModuleList.length;i++){

                for(var j = 0;j<versionList.length;j++){
                    var edata = versionList[j];
                    if(edata.VocaVerMappingID == self.customerModuleList[i]){
                        versionList[j].select =true;
                    }
                }
            }
            //debugger;
            self.ele.packageList.html(self.render(self.temp.packageList, { list: versionList }));

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
                            self.getMappingList();
                        }
                    });
                }
            } else {
                self.ele.section.find('.pageWrap').hide();
            }
            // 分页处理 end
        },
        apply: function (param, callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.SetCustomerModuleMapping",
                data: param,
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            alert("提交成功!");
                            window.history.go(-1);
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});