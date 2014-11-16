define(['jquery', 'formPageTemp', 'template', 'tools'], function ($, temp) {
    var page = {
        url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
        template: temp,
        ele: {
            section: $("#section"),
            formList: $("#formList"),
            addFormBtn: $("#addFormBtn"),

            addFormLayer: $("#addFormLayer"),
            mask: $(".ui-mask")
        },
        init: function () {

            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.formList = [1, 2, 3];
        },
        initEvent: function () {
            this.ele.addFormBtn.click(function () {
                self.addFormLayer.show(function () {
                    $.util.toUrlWithParam("/module/FormConfig/FormEdit.aspx", "fid=1111&type=1");
                    self.addFormLayer.hide();
                    self.formList.push("item");
                    self.renderFormList();
                });
            });
            this.ele.formList.delegate(".delBtn", "click", function () {
                var $this = $(this);
                if ($this.data("count")) {
                    self.delFormLayer.data.used = true;
                    self.delFormLayer.data.textTit = "已有活动正在使用本表单，是否确认删除？";
                    self.delFormLayer.data.CancelText = "编辑";
                } else {
                    self.delFormLayer.data.used = false;
                    self.delFormLayer.data.textTit = "是否确认删除？";
                    self.delFormLayer.data.CancelText = "取消";
                }
                self.delFormLayer.show(function () {
                    self.delFormLayer.hide();
                }, function () {
                    self.delFormLayer.hide();
                });
            }).delegate(".renameBtn", "click", function () {
                var $this = $(this),
                    input = $this.parent().siblings(".titName").children("input"),
                    span = input.siblings("span");
                input.show().focus();
                span.hide();
            }).delegate(".titleInput", "blur", function () {
                var $this = $(this);
                $this.hide().siblings("span").html($this.val()).show();
            });

            //初始化事件集
            this.renderFormList();
        },
        renderFormList: function () {
            this.ele.formList.html(this.render(this.template.formList, { itemList: this.formList }));
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },
        mask: {
            show: function () {
                self.ele.mask.show();
            },
            hide: function () {
                self.ele.mask.hide();
            }
        },
        addFormLayer: {
            show: function (callback) {
                if (self.ele.addFormLayer.length == 0) {
                    self.ele.addFormLayer = $(self.template.addFormLayer).appendTo(self.ele.section);
                    self.ele.addFormLayer.delegate(".jsAddBtn", "click", function () {
                        if (callback) {
                            callback();
                        } else {
                            self.addFormLayer.hide();
                        }
                    }).delegate(".jsCancelBtn,.closeBtn", "click", function () {
                        self.addFormLayer.hide();
                    });
                }
                self.mask.show();
                self.ele.addFormLayer.show();

            },
            hide: function () {
                self.ele.addFormLayer.hide();
                self.mask.hide();
            }
        },
        delFormLayer: {
            data: {
                textTit: "是否确认删除？",
                OKText: "删除",
                OKClass: "jsDelBtn",
                CancelText: "取消",
                CancelClass: "jsCancelBtn",
                used: false
            },
            show: function (OKCallback, CancelCallback) {
                var that = this;
                if ($("#delFormLayer").length == 0) {
                    $(self.render(self.template.delFormLayer, this.data)).appendTo(self.ele.section);
                    debugger;
                    $("#delFormLayer").delegate("." + this.data.OKClass, "click", function () {
                        if (OKCallback) {
                            OKCallback();
                        } else {
                            self.delFormLayer.hide();
                        }
                    }).delegate("." + this.data.CancelClass, "click", function () {
                        if (that.data.used) {
                            if (CancelCallback) {
                                CancelCallback();
                            }
                        } else {
                            self.delFormLayer.hide();
                        }
                    }).delegate(".closeBtn", "click", function () {
                        self.delFormLayer.hide();
                    })
                }
                self.mask.show();
                $("#delFormLayer").show();
            },
            hide: function () {
                $("#delFormLayer").remove();
                self.mask.hide();
            }
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
            $.ajax(_param);
        }
    },
    self = page;
    page.init();
});
