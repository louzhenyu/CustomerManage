define(['jquery', 'formPageTemp', 'template', 'tools'], function ($, temp) {
    var page = {
        url: "/ApplicationInterface/Gateway.ashx",
        template: temp,
        ele: {
            section: $("#section"),
            leftModifyWrap: $("#leftModifyWrap"),
            rightModifyWrap: $("#rightModifyWrap"),

            basicPPTList: $("#basicPPTList"),
            extendPPTList: $("#extendPPTList"),
            //advancedPPTList: $("#advancedPPTList"),
            tempPPTList: $("#tempPPTList"),
            mask: $(".ui-mask")
        },
        init: function () {
            if (!$.util.getUrlParam("fid")) {
                alert("获取不到表单ID");
                return false;
            } else {
                this.fid = $.util.getUrlParam("fid");
            }

            if (!$.util.getUrlParam("type")) {
                alert("获取不到表单类型");
                return false;
            } else {
                this.formType = $.util.getUrlParam("type");
            }



            this.tempPPTList = [];
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.loadCustomerPPT(function (data) {
                self.tempPPTList = data;
                self.renderCustomerPPT(data);
            });
            this.loadAllPPT(function (data) {
                for (var i = 0; i < data.BasicItems.length; i++) {
                    data.BasicItems[i].json = JSON.stringify(data.BasicItems[i]);
                }
                for (var i = 0; i < data.ExtendItems.length; i++) {
                    data.ExtendItems[i].json = JSON.stringify(data.ExtendItems[i]);
                }
                self.renderBasicPPT(data.BasicItems);
                self.renderExtendPPT(data.ExtendItems);
                //self.renderAdvancedPPT(data.advancedList);
            });
        },
        initEvent: function () {
            //左侧编辑 事件注册
            this.registerLeftWrapEvent();

            //右侧编辑 事件注册
            this.registerRightWrapEvent();

            //初始化事件集
        },
        loadCustomerPPT: function (callback) {
            //            this.ajax({
            //                url: self.url,
            //                type: 'get',
            //                data: {
            //                    action: "VIP.MobileModule.GetClientBunessDefined",
            //                    PageIndex: 0,
            //                    PageSize: 15,
            //                    Type: this.formType
            //                },
            //                dataType: "json",
            //                success: function (data) {
            //                    if (data.IsSuccess) {
            //                        if (callback) {
            //                            callback(list);
            //                        }
            //                    } else {
            //                        self.alert(data.Message);
            //                    }
            //                }
            //            });
            var list = [{
                MobileBunessDefinedID: 1001,
                ColumnName: "area",
                ControlType: 1,
                ColumnDesc: "住房面积",
                CorrelationValue: "",
                ListOrder: "1"
            }, {
                MobileBunessDefinedID: 1001,
                ColumnName: "user",
                ControlType: 1,
                ColumnDesc: "是否商铺用户",
                CorrelationValue: "",
                ListOrder: "1"
            }, {
                MobileBunessDefinedID: 1001,
                ColumnName: "time",
                ControlType: 1,
                ColumnDesc: "购房时间",
                CorrelationValue: "",
                ListOrder: "1"
            }];
            if (callback) {
                callback(list);
            }
        },
        loadAllPPT: function (callback) {
            this.ajax({
                url: self.url,
                type: 'get',
                data: {
                    action: "VIP.MobileModule.GetClientBunessDefined",
                    PageIndex: 0,
                    PageSize: 15,
                    Type: this.formType
                },
                dataType: "json",
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        }

                    } else {
                        self.alert(data.Message);
                    }
                }
            });
            //            var data = {};
            //            var list = [{
            //                MobileBunessDefinedID: 1001,
            //                ColumnName: "area",
            //                ControlType: 1,
            //                ColumnDesc: "住房面积",
            //                CorrelationValue: "",
            //                ListOrder: "1"
            //            }, {
            //                MobileBunessDefinedID: 1001,
            //                ColumnName: "user",
            //                ControlType: 1,
            //                ColumnDesc: "是否商铺用户",
            //                CorrelationValue: "",
            //                ListOrder: "1"
            //            }, {
            //                MobileBunessDefinedID: 1001,
            //                ColumnName: "time",
            //                ControlType: 1,
            //                ColumnDesc: "购房时间",
            //                CorrelationValue: "",
            //                ListOrder: "1"
            //            }];
            //            for (var i = 0; i < list.length; i++) {
            //                list[i].json = JSON.stringify(list[i]);
            //            }
            //            data.basicList = list;
            //            data.extendList = list;
            //            //data.advancedList = list;

            //            if (callback) {
            //                callback(data);
            //            }
        },
        processTempPPTList: function (obj) {
            var tempObj = $.util.list2obj(self.tempPPTList, "ColumnName");
            if (tempObj[obj.ColumnName]) {
                delete tempObj[obj.ColumnName];
            } else {
                tempObj[obj.ColumnName] = obj;
            }
            this.tempPPTList = $.util.obj2list(tempObj);
        },
        registerLeftWrapEvent: function () {
            this.ele.leftModifyWrap.delegate(".jsPropertyListItem ", "hover", function () {
                $(this).toggleClass("hover");

            }).delegate(".checkBox", "click", function () {
                $(this).parents(".jsPropertyListItem").toggleClass("on");

            }).delegate(".closeBtn", "click", function () {
                $li = $(this).parents(".jsPropertyListItem");
                self.processTempPPTList($li.data("json"));
                self.renderCustomerPPT();
            });
        },
        registerRightWrapEvent: function () {
            this.ele.rightModifyWrap.delegate(".jsPPTItem", "click", function () {
                debugger;
                var $this = $(this);
                var data = $this.data("json");
                var key = $this.data("key");
                if (data) {
                    $this.toggleClass("on");
                    self.processTempPPTList(data);
                    self.renderCustomerPPT();
                }

            });
        },
        renderCustomerPPT: function (list) {
            var list = list || this.tempPPTList;
            for (var i = 0; i < list.length; i++) {
                list[i].json = JSON.stringify(list[i]);
            }
            this.ele.tempPPTList.html(this.render(this.template.tempPPTItem, { list: list }));
        },
        renderBasicPPT: function (list) {
            this.ele.basicPPTList.html(this.render(this.template.pptItem, { list: list }));
        },
        renderExtendPPT: function (list) {
            this.ele.extendPPTList.html(this.render(this.template.pptItem, { list: list }));

        },
        //        renderAdvancedPPT: function (list) {
        //            this.ele.advancedPPTList.html(this.render(this.template.pptItem, { list: [] }));
        //        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },
        ajax: function (param) {
            var _param = {
                type: "post",
                dataType: "json",
                url: self.url,
                data: null,
                beforeSend: function () { },
                complete: function () { },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(JSON.stringify(XMLHttpRequest));
                }
            };

            $.extend(_param, param);
            $.util.ajax(_param);
        }
    },
    self = page;
    page.init();
});
