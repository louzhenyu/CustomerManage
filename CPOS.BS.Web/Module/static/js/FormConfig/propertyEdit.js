define(['jquery', 'formPageTemp', 'template', 'tools'], function ($, temp) {
    var page = {
        url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
        template: temp,
        ele: {
            section: $("#section"),
            addPPTBtn: $("#addPPTBtn"),
            extendPropertyLayer: $("#extendPropertyLayer"),
            extendPPTList: $("#extendPPTList"),
            advancedPPTList:$("#advancedPPTList"),
            mask: $(".ui-mask")
        },
        init: function () {
            this.tempPPTList = [];
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.getPropertyInfo(function (data) {
                
            });
        },
        initEvent: function () {
            //初始化事件
            this.ele.section.delegate(".addPPTBtn","click",function () {
                self.extendPropertyLayer.show(function () {

                });
            });
        },
        getPropertyInfo: function (callback) {
//            this.ajax({
//                url: self.url,
//                type: 'get',
//                data: {
//                    method: "GetLevel1ItemCategory"
//                },
//                dataType: "json",
//                success: function (data) {
//                    if (data.success) {
//                        self.ele.categorySelect[0].options[0] = new Option("请选择分类", "");
//                        if (data.data.categoryList && data.data.categoryList.length) {
//                            for (var i = 0; i < data.data.categoryList.length; i++) {
//                                var idata = data.data.categoryList[i];
//                                self.ele.categorySelect[0].options[i + 1] = new Option(idata.categoryName, idata.categoryId);
//                            }
//                        } else {
//                            alert("一级分类列表为空！");
//                        }

//                    } else {
//                        alert(data.msg);
//                    }
//                }
//            });
            var list = [{
	            MobileBunessDefinedID:1001,
	            ColumnName:"住房面积",
	            ControlType:1,
	            ColumnDesc:"住房面积",
	            CorrelationValue:"",
	            ListOrder:"1"
            },{
	            MobileBunessDefinedID:1001,
	            ColumnName:"是否商铺用户",
	            ControlType:1,
	            ColumnDesc:"是否商铺用户",
	            CorrelationValue:"",
	            ListOrder:"1"
            },{
	            MobileBunessDefinedID:1001,
	            ColumnName:"购房时间",
	            ControlType:1,
	            ColumnDesc:"购房时间",
	            CorrelationValue:"",
	            ListOrder:"1"
            }];
            if (callback) {
                callback(list);
            }
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
        extendPropertyLayer: {
            show: function (callback) {
                if (self.ele.extendPropertyLayer.length == 0) {
                    self.ele.extendPropertyLayer = $(self.template.extendPropertyLayer).appendTo(self.ele.section);
                    self.ele.extendPropertyLayer.delegate(".jsOKBtn", "click", function () {
                        if (callback) {
                            callback();
                        } else {
                            self.extendPropertyLayer.hide();
                        }
                    }).delegate(".jsCancelBtn,.closeBtn", "click", function () {
                        self.extendPropertyLayer.hide();
                    });
                }
                self.mask.show();
                self.ele.extendPropertyLayer.show();

            },
            hide: function () {
                self.ele.extendPropertyLayer.hide();
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
