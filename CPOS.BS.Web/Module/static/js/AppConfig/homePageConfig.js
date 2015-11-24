define(['jquery', 'homePageTemp', 'touchslider', 'template', 'kindeditor', 'pagination'], function ($, temp) {
    //debugger;
    //上传图片
    KE = KindEditor;
    var page = {
        url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
        template: temp,
        ele: {
            section: $("#section"),
            adList: $("#adList"),
            eventList: $("#eventList"),
            categoryList: $("#categoryList"),
            editLayer: $("#editLayer"),
            addItem: $("#addItem"),
            productLayer: $("#productPopupLayer"),
            categoryLayer: $("#categoryPopupLayer"),
            mask: $(".ui-mask"),
            categorySelect: $("#categorySelect")
        },
        init: function () {
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.allData = null;                //保存所有左侧load下来的数据 A B Ｃ区
            this.eventTypeList=[{ key: 2, value: "限时抢购" }, { key: 1, value: "疯狂团购" }, { key: 3, value: "热销榜单"}];    //B区tab名
            this.currentEditData = null;        //编辑数据

            this.GetLevel1ItemCategory();       //加载弹层一集分类
            this.GetHomePageConfigInfo();       //获取左侧数据
        },
        stopBubble: function (e) {              //阻止冒泡方法
            if (e && e.stopPropagation) {
                //因此它支持W3C的stopPropagation()方法 
                e.stopPropagation();
            } else {
                //否则，我们需要使用IE的方式来取消事件冒泡 
                window.event.cancelBubble = true;
            }
        },
        initEvent: function () {
            //初始化事件集
            this.sectionEvent();
            this.editLayerEvent();          //编辑区域事件
            this.categoryLayerEvent();
            this.productLayerEvent();
        },
        categoryLayerEvent: function () {
            // 分类弹层 事件委托
            this.ele.categoryLayer.delegate(".searchBtn", "click", function () {
                self.categoryLayer.loadDate($(this).siblings().children().val());

            }).delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.mask.hide();

            }).delegate(".categoryItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.categoryLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.categoryLayer.hide();
                    $this.removeClass("on");
                }, 300);
            });
        },
        productLayerEvent: function () {
            // 产品选择事件委托
            this.ele.productLayer.delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.mask.hide();

            }).delegate(".searchBtn", "click", function () {
                self.productLayer.loadDate($(this).siblings().children("select").val(), $(this).siblings().children("input").val());

            }).delegate(".productItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.productLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.productLayer.hide();
                    $this.removeClass("on")
                }, 300);
            });
        },
        sectionEvent: function () {
            // 给section委托事件
            this.ele.section.delegate(".jsListItem", "hover", function (e) {
                ////////////////////////////////////////////    上移下移删除    //////////////////////////////////////////////

                var $this = $(this),
                siblingsLen = $this.parent().find(".jsListItem").length,
                index = $this.index();
                if (index == 0) {
                    $this.find(".jsTowardsUp").remove();
                } else if (index == siblingsLen - 1) {
                    $this.find(".jsTowardsDown").remove();
                }
                $(this).toggleClass("hover");

            }).delegate(".jsRemoveGroup", "click", function (e) {   // 删除
                self.ele.editLayer.empty().hide();
                self.ele.section.find(".jsListItem").removeClass("on");

                var $this = $(this),
                gid = $this.parent().data("groupid");

                if (confirm("确认删除该分组？")) {
                    if (gid) {
                        self.DeleteItemCategoryArea(gid, function () {
                            $this.parents(".jsListItem").remove();
                        });
                    } else {
                        $this.parents(".jsListItem").remove();
                    }
                }
                self.stopBubble(e);

            }).delegate(".jsTowardsUp", "click", function (e) {     //  上移
                self.ele.editLayer.empty().hide();
                self.ele.section.find(".jsListItem").removeClass("on");

                var $this = $(this),
                $jsListItem = $this.parents(".jsListItem").eq(0);

                var gidFrom = $jsListItem.data("groupid"),
                gidTo = $jsListItem.prev().data("groupid");
                // 如果不在最顶端
                if (gidTo) {
                    self.UpdateMHCategoryAreaByGroupId(gidFrom, gidTo, function () {
                        //重新拉数据
                        self.GetHomePageConfigInfo();
                    });
                }
                self.stopBubble(e);

            }).delegate(".jsTowardsDown", "click", function (e) {   //  下移
                self.ele.editLayer.empty().hide();
                self.ele.section.find(".jsListItem").removeClass("on");

                var $this = $(this),
                $jsListItem = $this.parents(".jsListItem").eq(0);

                var gidFrom = $jsListItem.data("groupid"),
                gidTo = $jsListItem.next().data("groupid");
                // 如果不在最顶端
                if (gidTo) {
                    self.UpdateMHCategoryAreaByGroupId(gidFrom, gidTo, function () {
                        //重新拉数据
                        self.GetHomePageConfigInfo();
                    });
                }
                self.stopBubble(e);

            }).delegate(".jsListItem", "click", function (e) {
                /////////////////////////////// 点击左侧listitem，显示右侧模板begin  ////////////////////////
                var $this = $(this),
                    key = $this.data("key"),
                    index = $this.data("index"),
                    type = $this.data("type"),
                    model = $this.data("model");
                //debugger;
                if (type && type.length) {
                    self.currentEditData = index==undefined?self.allData[key]:self.allData[key][index];
                    if (self.currentEditData) {
                        // 分发渲染
                        if (model == "ad") {
                            self.ele.editLayer.html(self.render(self.template.rightADTemp));
                            //附加后面的数据
                            self.ele.editLayer.append(self.render(self.template.adItemListTemp, { list: self.currentEditData }));
                            //debugger;
                            self.ele.editLayer.append('<div class="jsAddadItemTemp addLayerBtn"></div>');

                            // 注册上传按钮
                            self.ele.editLayer.find(".uploadImgBtn").each(function (i, e) {
                                self.addUploadImgEvent(e);
                            });

                        } else if (model == "event") {
                            var list = [];
                            for (var i = 0; i < self.currentEditData.length; i++) {
                                var obj = self.currentEditData[i];
                                obj.json = JSON.stringify(obj);
                                list.push(obj);
                            }
                            //debugger;
                            self.ele.editLayer.html(self.render(self.template.rightEventTemp, { list: list, eventTypeList: self.eventTypeList }));

                            var cuttentTab = self.ele.editLayer.find(".jsTabContainer .on");
                            var typeId = cuttentTab.data("typeid");
                            if (typeId) {
                                //数据筛选
                                self.goodsSelect.typeId = typeId;
                                self.goodsSelect.pageIndex = 0;
                                self.goodsSelect.currentItemId = cuttentTab.data("currentitemid");
                                self.goodsSelect.loadData();
                            }
                        } else if (model == "category") {
                            self.ele.editLayer.html(self.render(self.template.rightCategoryTemp));
                            //附加后面的数据
                            //debugger;
                            self.renderCategoryItemList(self.currentEditData);
                        }

                    } else {
                        // 添加空白模版
                        if (model == "ad") {
                            self.ele.editLayer.html(self.render(self.template.rightADTemp));
                            self.ele.editLayer.append('<div class="jsAddadItemTemp addLayerBtn"></div>');

                        } else if (model == "category") {
                            self.ele.editLayer.html(self.render(self.template.rightCategoryTemp));
                            //c区模板选择
                            self.ele.editLayer.append(self.render(self.template.categoryModelTemp));
                            var cuttentTab = self.ele.editLayer.find(".jsSectionCTabContainer .on");
                            var modelId = cuttentTab.data("model");
                            self.renderCategoryItemList({ length: modelId });

                        } else if (model == "event") {
                            self.ele.editLayer.html(self.render(self.template.rightEventTemp, { eventTypeList: self.eventTypeList }));

                            //数据筛选
                            self.goodsSelect.typeId = self.ele.editLayer.find(".on").data("typeid");
                            self.goodsSelect.pageIndex = 0;
                            self.goodsSelect.loadData();
                        }
                    }

                    self.ele.editLayer.show();

                    // 修改左侧选中状态
                    self.ele.section.find(".jsListItem").removeClass("on");
                    $this.addClass("on");
                } else {
                    self.ele.editLayer.hide();
                }

                /////////////////////////////// 点击左侧listitem，显示右侧模板end  ////////////////////////
            }).delegate("#addItem", "click", function (e) {
                var $this = $(this),
                type = $this.data("type");
                if (self.ele.categoryList.find(".jsListItemEmpty").length == 0) {
                    self.ele.categoryList.append(self.render(self.template.categoryEmptyModel));
                } else {
                    alert("请先编辑空白模板并保存");
                }
            });
        },
        editLayerEvent: function () {
            // 给右侧编辑层 委托事件
            this.ele.editLayer.delegate(".jsCancelBtn", "click", function (e) {
                // 关闭右侧编辑层
                self.ele.editLayer.empty().hide();
                self.ele.section.find(".jsListItem").removeClass("on");

            }).delegate(".jsTypeSelect", "change", function (e) {
                //选择select类型  产品、资讯、类型....
                var $this = $(this);
                $this.parent().siblings(".infoContainer").children(".jsNameInput").val("");
                $this.parents(".jsAreaItem").eq(0).data("objectid", "").data("typeid", this.value.substring(this.value.indexOf("-") + 1));
                if (this.value == "ad-2") {
                    $this.parent().siblings(".infoContainer").children(".jsNameInput").removeAttr("disabled");
                    $this.parent().siblings(".infoContainer").children(".jsChooseBtn").hide();
                } else {
                    $this.parent().siblings(".infoContainer").children(".jsNameInput").attr("disabled", "disabled");
                    $this.parent().siblings(".infoContainer").children(".jsChooseBtn").show();
                }
            }).delegate(".jsNameInput", "blur", function (e) {
                // A区广告选择咨询后 输入框输入URL地址
                var $this = $(this);
                var value = $this.parent().siblings(".typeContainer").children(".jsTypeSelect").val();
                $this.parents(".jsAreaItem").eq(0).data("objectid", "").data("typeid", value.substring(value.indexOf("-") + 1));

            }).delegate(".jsChooseBtn", "click", function (e) {
                //输入框右侧的选择按钮
                /////////////////////////////////////~~~~~~~~~~~category~~~~~~~~~~~~~~~~~/////////////////////////////
                var $this = $(this),
                type = $this.parent().siblings(".typeContainer").children().val();
                if (type == "cg-1") {
                    self.categoryLayer.pageIndex = 0;
                    self.categoryLayer.show(function (key, name) {
                        //注册回调
                        $this.parents(".jsAreaItem").eq(0).data("objectid", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.categoryLayer.loadDate();
                } else if (type == "cg-2" || type == "ad-3") {
                    self.productLayer.pageIndex = 0;
                    self.productLayer.show(function (key, name) {
                        //注册回调
                        $this.parents(".jsAreaItem").eq(0).data("objectid", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.productLayer.loadDate();
                }
            }).delegate(".jsSaveCategoryBtn", "click", function (e) {
                //保存C区信息
                self.saveCategory();
            }).delegate(".jsTab", "click", function () {
                //tab选择
                $(this).addClass("on").siblings().removeClass("on");

            }).delegate(".jsSectionCTabContainer .jsTab", "click", function () {
                //C区选择模板
                var modelId = $(this).data("model");
                if (modelId) {
                    self.renderCategoryItemList({ length: modelId });
                }

            }).delegate(".jsTabContainer .jsTab", "click", function () {
                //B区选择模板
                /////////////////////////////////////~~~~~~~~~~~event  B区~~~~~~~~~~~~~~~~~/////////////////////////////
                self.ele.editLayer.find(".jsGoodsList").html('<li style="text-align:center;">数据加载中...</li>');

                var typeId = $(this).data("typeid");
                if (typeId) {
                    //数据筛选
                    self.goodsSelect.typeId = typeId;
                    self.goodsSelect.pageIndex = 0;
                    self.goodsSelect.loadData();
                    self.goodsSelect.currentItemId = $(this).data("currentitemid");
                }

            }).delegate(".jsGoodsItem", "click", function () {
                //赋值给当前选中的dom tab元素
                //debugger;
                var currentTab = self.ele.editLayer.find(".jsTabContainer .on");
                var itemData = $(this).data();

                currentTab.attr("data-value", $(this).find("input").val());

                //修改itemId和eventId
                currentTab.data("currentitemid", itemData.itemid).data("eventid", itemData.eventid);


            }).delegate(".jsSaveEventBtn", "click", function () {
                // 保存活动
                //debugger;
                self.saveEvent();
            }).delegate(".jsAddadItemTemp", "click", function () {
                ////////////////////////////////////////begin~~~~~~A区广告  ad
                var $this = $(this);
                $this.before(self.render(self.template.adItemListTemp, { length: 1 }));
                self.addUploadImgEvent($this.prev().find(".uploadImgBtn")[0]);
            }).delegate(".jsDeladItemTemp", "click", function () {
                $(this).parents(".jsAreaItem ").eq(0).remove();
            }).delegate(".jsSaveADBtn", "click", function () {
                // 保存广告
                self.saveAD();
            });
        },
        saveAD: function () {
            var list = [];
            var flag = true;
            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["imageUrl"] = $this.data("imageurl");
                obj["adId"] = $this.data("adid") || "";
                obj["displayIndex"] = i + 1;

                if (obj.typeId == 2) {
                    obj["url"] = $this.find(".jsNameInput").val();
                } else {
                    obj["objectId"] = $this.data("objectid");
                }

                //debugger;

                if (!obj.imageUrl) {
                    flag = false;
                    alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                    return false;
                }
                if (obj.typeId) {
                    if (obj.typeId == 2) {
                        if (!obj.url) {
                            flag = false;
                            alert("第" + (i + 1) + "项展示广告不能为空，请填写资讯链接地址");
                            return false;
                        }
                    } else if (obj.typeId == 3) {
                        if (!obj.objectId) {
                            flag = false;
                            alert("第" + (i + 1) + "项展示广告不能为空，请选择展示的商品");
                            return false;
                        }
                    } else {
                        flag = false;
                        alert("第" + (i + 1) + "项选择的是未知类型");
                        return false;
                    }
                } else {
                    flag = false;
                    alert("第" + (i + 1) + "项展示广告不能为空，请选择展示的商品或填写资讯链接地址");
                    return false;
                }

                list.push(obj);
            });
            if (flag) {
                self.SaveAds(list);
            }
        },
        saveEvent: function () {
            debugger;
            var list = [];
            var flag = true;
            self.ele.editLayer.find(".jsTab").each(function (i, e) {
                var obj = {},
                    $this = $(e),
                    idata = $this.data().value;

                if (idata) {
                    //flag = false;
                    //alert("请为" + $this.html() + "选择商品");
                    //return false;
                    obj["itemId"] = $this.data("currentitemid") || idata.itemId;
                    obj["eventId"] = $this.data("eventid") || idata.eventId;
                    obj["isUrl"] = 1;
                    obj["displayIndex"] = $this.data("displayindex") || (i + 1);
                    if (self.currentEditData && self.currentEditData[i]) {
                        obj["itemAreaId"] = self.currentEditData[i].eventAreaItemId;
                    }
                    list.push(obj);
                }

            });
            //debugger;
            if (flag) {
                self.SaveEventItemArea(list);
            }
        },
        saveCategory: function () {
            var list = [];
            var model = {};
            var flag = true;
            if (self.currentEditData) {
                model.id = self.currentEditData.modelTypeId||3;
                model.name = self.currentEditData.modelTypeName || ("C区模板" + model.id);
            } else {
                var modelId = self.ele.editLayer.find(".jsSectionCTabContainer .on").data("model");
                if (!modelId) {
                    alert("获取不到模板信息");
                    return;
                } else {
                    model.id = modelId;
                    model.name = "C区模板" + modelId;
                }
            }

            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                //debugger;
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["objectId"] = $this.data("objectid");
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["displayIndex"] = $this.data("displayindex");
                    obj["groupId"] = self.currentEditData.groupId;
                } else {

                    obj["displayIndex"] = i + 1;
                    if (!obj.objectId) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示商品不能为空，请选择展示的商品或类型");
                        return false;
                    }
                    if (!obj.imageUrl) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                        return false;
                    }
                }
                list.push(obj);
            });
            if (flag) {
                self.SaveItemCategory(list, model);
            }
        },
        renderCategoryItemList: function (obj) {
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            self.ele.editLayer.append(self.render(self.template.categoryItemListTemp, obj));
            // 注册上传按钮
            self.ele.editLayer.find(".uploadImgBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },
        addUploadImgEvent: function (e) {
            self.uploadImg(e, function (ele, data) {
                //上传成功后回写数据
                $(ele).parent().siblings("p").html('<img src="' + data.thumUrl + '" />');
                $(ele).parents(".jsAreaItem").eq(0).data("imageurl", data.thumUrl);
            });
        },
        uploadImg: function (btn, callback) {
            var uploadbutton = KE.uploadbutton({
                width: "100%",
                button: btn,
                //上传的文件类型
                fieldName: 'imgFile',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=600',
                afterUpload: function (data) {
                    if (data.error === 0) {
                        if (callback) {
                            callback(btn, data);
                        }
                        //取返回值,注意后台设置的key,如果要取原值
                        //取缩略图地址
                        //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                        //取原图地址
                        //var url = KE.formatUrl(data.url, 'absolute');
                    } else {
                        alert(data.message);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },
        UpdateMHCategoryAreaByGroupId: function (gidfrom, gidto, callback) {
            this.ajax({
                url: self.url + "?method=UpdateMHCategoryAreaByGroupId",
                type: 'post',
                data: {
                    groupIDFrom: gidfrom,
                    groupIDTo: gidto
                },
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        alert(data.msg);
                    }
                }
            });
        },
        DeleteItemCategoryArea: function (gid, callback) {
            this.ajax({
                url: self.url + "?method=DeleteItemCategoryArea",
                type: 'post',
                data: {
                    groupID: gid
                },
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        alert(data.msg);
                    }
                }
            });
        },
        //2.1.9	获取活动商品列表
        GetItemAreaByEventTypeID: function (EventTypeID, pageIndex, pageSize, callback) {
            this.ajax({
                url: self.url,
                type: 'get',
                data: {
                    method: "GetItemAreaByEventTypeID",
                    EventTypeID: EventTypeID,
                    pageIndex: pageIndex,
                    pageSize: pageSize
                },
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        alert(data.msg);
                    }
                }
            });
        },
        // 获取一级分类列表
        GetLevel1ItemCategory: function () {
            this.ajax({
                url: self.url,
                type: 'get',
                data: {
                    method: "GetLevel1ItemCategory"
                },
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        self.ele.categorySelect[0].options[0] = new Option("请选择分类", "");
                        if (data.data.categoryList && data.data.categoryList.length) {
                            for (var i = 0; i < data.data.categoryList.length; i++) {
                                var idata = data.data.categoryList[i];
                                self.ele.categorySelect[0].options[i + 1] = new Option(idata.categoryName, idata.categoryId);
                            }
                        } else {
                            alert("一级分类列表为空！");
                        }

                    } else {
                        alert(data.msg);
                    }
                }
            });
        },
        // 获取左侧信息列表
        GetHomePageConfigInfo: function (callback) {
            this.ajax({
                url: self.url,
                type: 'get',
                data: {
                    method: "GetHomePageConfigInfo"
                },
                cache: false,
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        self.allData = data.data;
                        if (callback) {
                            callback(data);
                        } else {
                            if (data.data.adList) {
                                self.renderADList(data.data.adList);
                            }
                            if (data.data.eventList) {
                                self.renderEventList(data.data.eventList);
                            }
                            if (data.data.categoryList) {
                                self.renderCategoryList(data.data.categoryList);
                            }
                        }
                    } else {
                        alert(data.msg);
                    }
                }
            });
        },
        //    eventItemList	
        //		itemAreaId	Guid	活动商品ID(为空为新增，否则为Update)
        //		itemID		string	商品ID					
        //		eventID		Guid	活动ID
        //		isUrl		bool	是否需要链接
        //		displayIndex		int	排序排序
        SaveEventItemArea: function (list, callback) {
            //debugger;
            this.ajax({
                url: self.url + "?method=SaveEventItemArea",
                data: { eventItemList: JSON.stringify(list) },
                dataType: "json",
                type: "post",
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        } else {
                            alert("保存成功！");
                            self.ele.editLayer.empty().hide();
                            self.GetHomePageConfigInfo();
                        }
                    } else {
                        alert(data.msg);
                    }
                },
                error: function () {

                }
            });
        },
        //        categoryList		分类集合
        //            typeId			类型ID： 1=商品分类 2=商品
        //            categoryAreaId 商品类别区域ID
        //            objectId 		对象ID
        //            objectName		对象名称
        //            groupId			分组ID（1、2、3…….）
        //            displayIndex		序号（1、2、3）
        //            imageUrl		图片链接
        SaveItemCategory: function (list, model) {
            //debugger;
            this.ajax({
                url: self.url + "?method=SaveItemCategory",
                data: {
                    categoryList: JSON.stringify(list),
                    modelTypeId: model.id,
                    modelTypeName: model.name
                },
                dataType: "json",
                type: "post",
                success: function (data) {
                    if (data.success) {
                        alert("保存成功！");
                        self.ele.editLayer.empty().hide();
                        self.GetHomePageConfigInfo();
                    } else {
                        alert(data.msg);
                    }
                },
                error: function () {

                }
            });
        },
        //  adList
        //	    adAreaId		标识（为空新增，不为空更新）
        //      typeId			类型ID： 1=活动 2=资讯 3=商品 4=门店
        //      objectId 		对象ID
        //      displayIndex		序号
        //      imageUrl		图片链接

        SaveAds: function (list, callback) {
            //debugger;
            this.ajax({
                url: self.url + "?method=SaveAds",
                data: { adList: JSON.stringify(list) },
                dataType: "json",
                type: "post",
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        } else {
                            alert("保存成功！");
                            self.ele.editLayer.empty().hide();
                            self.GetHomePageConfigInfo();
                        }
                    } else {
                        alert(data.msg);
                    }
                },
                error: function (e) {

                }
            });
        },
        renderADList: function (list) {
            var list = list || [];
            if (list.length) {
                this.ele.adList.html(self.render(this.template.adModel, { list: list, key:"adList" }));
                //this.ele.adList.html(self.render(this.template.adModelStatic));
                $(".touchslider").touchSlider({
                    duration: 350, // the speed of the sliding animation in milliseconds
                    delay: 3000, // initial auto-scrolling delay for each loop
                    margin: 5, // borders size. The margin is set in pixels.
                    mouseTouch: true,
                    namespace: "touchslider",
                    pagination: ".touchslider-nav-item",
                    currentClass: "touchslider-nav-item-current", // class name for current pagination item.
                    autoplay: true, // whether to move from image to image automatically
                    viewport: ".touchslider-viewport"
                });
            } else {
                this.ele.adList.html(self.render(this.template.adModel));
            }

        },
        renderEventList: function (list) {
            var list = list || [];
            if (list.length) {
                this.ele.eventList.html(self.render(this.template.eventModel, { list: list, key:"eventList" }));
            } else {
                this.ele.eventList.html(self.render(this.template.eventEmptyModel));
            }

        },
        renderCategoryList: function (list) {
            var list = list || [];
            if (list.length) {
                this.ele.categoryList.html(self.render(this.template.categoryModel, { list: list,key:"categoryList" }));
            } else {
                //不需要添加空白项
                //this.ele.categoryList.html(self.render(this.template.categoryEmptyModel));
            }
        },
        goodsSelect: {
            pageIndex: 0,
            pageSize: 5,
            typeId: 1,
            currentItemId: undefined,
            loadData: function (callback) {
                self.GetItemAreaByEventTypeID(this.typeId, this.pageIndex, this.pageSize, function (data) {
                    var html = "";
                    if (data.data.totalCount) {
                        for (var i = 0; i < data.data.itemList.length; i++) {
                            var idata = data.data.itemList[i];
                            data.data.itemList[i].json = JSON.stringify(idata);
                        }
                        data.data.currentItemId = self.goodsSelect.currentItemId;
                        html = self.render(self.template.goods, data.data);
                        // 分页处理 begin
                        var pageNumber = Math.ceil(data.data.totalCount / self.productLayer.pageSize);

                        if (pageNumber > 1) {
                            if (self.goodsSelect.pageIndex == 0) {
                                //总页数大于一，且第一页时注册分页
                                self.ele.editLayer.find('.pageWrap').show();
                                self.ele.editLayer.find('.pagination').jqPagination({
                                    current_page: self.goodsSelect.pageIndex + 1,
                                    max_page: pageNumber,
                                    paged: function (page) {
                                        self.goodsSelect.pageIndex = page - 1;
                                        self.goodsSelect.loadDate();
                                    }
                                });
                            }
                        } else {
                            self.ele.editLayer.find('.pageWrap').hide();
                        }
                        // 分页处理 end

                    } else {
                        html = '<li style="text-align:center;">没有数据</li>';
                    }
                    self.ele.editLayer.find(".jsGoodsList").html(html);
                });
            }
        },
        productLayer: {
            show: function (callback) {
                self.ele.productLayer.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.ele.productLayer.hide();
                self.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            loadDate: function (id, text) {
                var categoryId = id || self.ele.productLayer.find("select").val();
                var itemName = itemName || self.ele.productLayer.find("input").val();
                self.ajax({
                    url: self.url,
                    type: 'get',
                    data: {
                        method: 'GetItemList',
                        categoryId: categoryId,
                        itemName: itemName,
                        pageIndex: this.pageIndex,
                        pageSize: this.pageSize
                    },
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            var html = "";
                            if (data.data.totalCount) {
                                html = self.render(self.template.product, data.data);
                                // 分页处理 begin
                                var pageNumber = Math.ceil(data.data.totalCount / self.productLayer.pageSize);

                                if (pageNumber > 1) {
                                    if (self.productLayer.pageIndex == 0) {
                                        //总页数大于一，且第一页时注册分页
                                        self.ele.productLayer.find('.pageWrap').show();
                                        self.ele.productLayer.find('.pagination').jqPagination({
                                            current_page: self.productLayer.pageIndex + 1,
                                            max_page: pageNumber,
                                            paged: function (page) {
                                                self.productLayer.pageIndex = page - 1;
                                                self.productLayer.loadDate();
                                            }
                                        });
                                    }
                                } else {
                                    self.ele.productLayer.find('.pageWrap').hide();
                                }
                                // 分页处理 end

                            } else {
                                html = self.render(self.template.product, { itemList: [] });
                            }
                            $("#layerProductList").html(html);
                        } else {
                            alert(data.msg);
                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
            }
        },
        categoryLayer: {
            show: function (callback) {
                self.ele.categoryLayer.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.ele.categoryLayer.hide();
                self.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            loadDate: function (text) {
                var categoryName = text || self.ele.categoryLayer.find("input").val();
                self.ajax({
                    url: self.url,
                    type: 'get',
                    data: {
                        method: 'GetItemCategory',
                        categoryName: categoryName,
                        pageIndex: this.pageIndex,
                        pageSize: this.pageSize
                    },
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            var html = "";

                            if (data.data.totalCount) {
                                html = self.render(self.template.category, data.data);
                                // 分页处理 begin
                                var pageNumber = Math.ceil(data.data.totalCount / self.categoryLayer.pageSize);
                                if (pageNumber > 1) {
                                    if (self.categoryLayer.pageIndex == 0) {
                                        //总页数大于一，且第一页时注册分页
                                        self.ele.categoryLayer.find('.pageWrap').show();
                                        self.ele.categoryLayer.find('.pagination').jqPagination({
                                            current_page: self.categoryLayer.pageIndex + 1,
                                            max_page: pageNumber,
                                            paged: function (page) {
                                                self.categoryLayer.pageIndex = page - 1;
                                                self.categoryLayer.loadDate();
                                            }
                                        });
                                    }
                                } else {
                                    self.ele.categoryLayer.find('.pageWrap').hide();
                                }
                                // 分页处理 end

                            } else {
                                html = self.render(self.template.product, { itemList: [] });
                            }
                            $("#layerCategoryList").html(html);
                        } else {
                            alert(data.msg);
                        }
                    },
                    error: function (data) {
                        alert(JSON.stringify(data));
                    }
                });
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
