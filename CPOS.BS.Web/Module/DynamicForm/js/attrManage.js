define(['jquery', 'template', 'tools', 'pagination'], function ($, temp) {
    template.openTag = '<$';
    template.closeTag = '$>';

    var page = {
        ele: {
            section: $("#section"),
            basicList: $("#basicList"),
            menuList: $("#menuList"),
            propertyContainer: $("#propertyContainer"),
            editLayer: $("#editLayer"),
            addLayer: $("#addLayer")
        },

        init: function () {
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.DisplayTypeList = null;    // 右侧基本属性列表

            this.editInfo = { flag: false, mode: "add", modeName: "添加", id: "", name: "", optionList: [] };

            this.classMap = {
                "1": "iconName",
                "2": "iconNumber",
                "3": "iconMobile",
                "4": "iconEmail",
                "5": "iconMenu",
                "6": "iconDate",
                "7": "iconMobile",
                "other": "iconOther"
            };

            this.loadPageList();
            this.loadBasic();
        },
        initEvent: function () {
            this.optionStr = '<div class="item optionItem">\
                                    <i></i>\
                                    <label><input type="text" placeholder="请输入选项"></label>\
                                        <span class="minus"></span>\
                                        <span class="plus"></span>\
                                </div>';

            this.ele.section.delegate(".optionSection .plus", "click", function () {
                var optionsItem = $(this).parents(".optionItem");
                $(self.optionStr).insertAfter(optionsItem).find("input")[0].focus();
            }).delegate(".optionSection .minus", "click", function () {
                var optionsItem = $(this).parents(".optionItem");
                if (optionsItem.siblings().length >= 2) {
                    optionsItem.remove();
                } else {
                    alert("至少需要两个选项");
                }

            });

            this.ele.propertyContainer.delegate(".propertyItem", "hover", function () {
                $(this).toggleClass("on");
            }).delegate(".propertyItem", "click", function (e) {
                var $this = $(this),
                    type = $this.attr("data-type"),
                    name = $this.attr("data-name"),
                    id = $this.attr("data-property");

                self.editInfo.id = id;
                self.editInfo.name = name;
                self.editInfo.type = type;
                if (id && type == 5) {
                    self.getPropertyContentByID(id, function (data) {
                        if (data.OptionList && data.OptionList.length != 0) {
                            self.editInfo.flag = true;
                        } else {
                            self.editInfo.flag = false;
                        }
                        self.editInfo.mode = "edit";
                        self.editInfo.modeName = "编辑";
                        self.editInfo.optionList = data.OptionList || [];
                        self.ele.editLayer.html(self.render("editTemp", self.editInfo));
                        self.showTab(1);
                        self.ele.editLayer.find("input")[0].focus();
                    });
                } else {
                    self.editInfo.flag = false;
                    self.editInfo.mode = "edit";
                    self.editInfo.modeName = "编辑";
                    self.editInfo.optionList = [];
                    self.ele.editLayer.html(self.render("editTemp", self.editInfo));
                    self.showTab(1);
                    self.ele.editLayer.find("input")[0].focus();
                }
            });
            this.ele.editLayer.delegate(".editSubmitBtn", "click", function () {
                var title = self.ele.editLayer.find(".editTitle").val();
                var optionList = [];
                var saveObj = null;
                var flag = true;
                var props = $('#propertyList div.propertyItem');
                for (var i = props.length - 1; i--; i >= 0) {
                    var name = $(props[i]).data('name');
                    if (name == title) {
                        alert("已存在此属性名.");
                        return false;
                    }
                }
                if(title.length==0){
                    alert("属性不能为空！");
                    return false;
                }
                if (title.length > 50) {
                    alert('属性长度不能超过50.');
                    return false;
                }
                self.ele.editLayer.find(".optionSection .optionItem input").each(function (i, e) {
                    if (e.value.length != 0) {
                        optionList.push({ OptionText: e.value });
                    } else {
                        alert("选项" + (i + 1) + "内容不能为空！");
                        flag = false;
                        return false;
                    }
                });
                if (!flag) {
                    return false;
                }
                if (self.editInfo.type == 5 && optionList.length < 2) {
                    alert("下拉类型至少需要两个选项！");
                    return false;
                }
                if (self.editInfo.flag) {
                    if ((self.editInfo.type == 5 && self.editInfo.optionList.length != optionList.length) || self.editInfo.type != 5) {
                        saveObj = {
                            id: self.editInfo.id,
                            name: title,
                            type: self.editInfo.type,
                            optionList: optionList
                        };
                        self.saveProperty(saveObj);
                    } else {
                        alert("请完成选项添加");
                    }
                }

            });

            this.ele.addLayer.delegate("#basicList li", "click", function () {
                self.editInfo.flag = true;
                self.editInfo.mode = "add";
                self.editInfo.modeName = "添加";

                self.editInfo.id = "";
                self.editInfo.name = "";
                self.editInfo.type = $(this).attr("data-type"),

                self.ele.editLayer.html(self.render("editTemp", self.editInfo));
                self.showTab(1);
                self.ele.editLayer.find("input")[0].focus();
            });

            // tab事件
            this.ele.menuList.delegate(".tabHead", "click", function () {
                var index = $(this).index();
                if (index == 0) {
                    if (self.editInfo.flag) {
                        if (confirm("正在" + self.editInfo.modeName + "属性,确认放弃本次编辑？")) {
                            self.showTab(index);
                            self.editInfo.flag = false;
                        }
                    } else {
                        self.showTab(index);
                    }
                }
            });

        },
        showTab: function (index) {
            var $tab = this.ele.menuList.find(".tabHead").eq(index);
            $tab.addClass("on").siblings().removeClass("on");
            $("#" + $tab.data("layer")).show().siblings(".tabContainer").hide();
        },
        loadBasic: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                type: "get",
                data: {
                    action: "DynamicVipDisplayTypeList"
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        self.DisplayTypeList = data.Data.DisplayTypeList;
                        debugger;
                        if (callback) {
                            callback(data.Data);
                        } else {
                            self.ele.basicList.html(self.render("basicListTemp", { list: self.DisplayTypeList, classMap: self.classMap }));
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageList: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                type: "get",
                data: {
                    action: "DynamicVipPropertyList",
                    PageIndex: 0,
                    PageSize: 50
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            self.ele.propertyContainer.html(self.render("propertyTemp", data.Data));
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        getPropertyContentByID: function (id, callback) {
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                type: "get",
                data: {
                    action: "DynamicVipPropertyOptionList",
                    PropertyID: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        saveProperty: function (obj, callback) {
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                data: {
                    action: "DynamicVipPropertySave",
                    PropertyID: obj.id,
                    DisplayType: obj.type,
                    PropertyName: obj.name,
                    OptionList: obj.optionList
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback();
                        } else {
                            alert("保存成功！");
                            self.showTab(0);
                            self.editInfo.flag = false;
                            self.loadPageList();
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        render: function (tempId, data) {
            var render = template.compile($("#" + tempId).html());
            return render(data || {});
        }
    };

    var self = page;
    page.init();
});