define(['jquery', 'homePageTemp', 'touchslider', 'template', 'kindeditor', 'pagination', 'jqueryui'], function ($, temp, touchslider) {
    //console.dir(touchslider);
    //debugger;
    //上传图片
    KE = KindEditor;
    var page = {
        url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
        template: temp,
        sotrActionJson: {},
        ele: {
            section: $("#section"),
            adList: $("#adList"),
            entranceList: $("#entranceList"),
            eventList: $("#eventList"),
            categoryList: $("#categoryList"),
            editLayer: $("#editLayer"),
            addItem: $("#addItem"),
            productLayer: $("#productPopupLayer"),
            categoryLayer: $("#categoryPopupLayer"),
            mask: $(".ui-mask"),
            categorySelect: $("#categorySelect"),
            SearchDiv: $("#search"),
            secondKill:$("#secondKill"),
            sortableDiv: $("#sortable"),
            classAction: $("#sortable .action"),
            navigation:$("#navigation")

        },
        init: function () {
            this.loadData();
            this.initEvent();
            this.initSort();

        },
        initSort: function () {

            var self = this;
            self.ele.sortableDiv.sortable({
                revert: true,
                opacity: 0.8,
                axis: "y",
                start: function () {
                    var str = this;

                },
                drag: function () {

                },
                stop: function () {
                    self.sortAction();

                }
            });

            self.ele.classAction.disableSelection();

        },
        sortAction: function (isLoadInfo) {
            debugger

            self.sotrActionJson={}
            self.ele.classAction.each(function () {

                switch ($(this).attr("id")) {
                    case "search":
                        self.sotrActionJson.search =self.isAdd($(this))?-1:$(this).index();
                        break;  //搜索
                    case "adList":
                        self.sotrActionJson.adList = self.isAdd($(this))?-1:$(this).index();
                        break; //头部幻灯片
                    case "entranceList":
                        self.sotrActionJson.entranceList = self.isAdd($(this))?-1:$(this).index();
                        break; //分类导航
                    case "eventList":
                        self.sotrActionJson.eventList =self.isAdd($(this))?-1:$(this).index();
                        break; //团购活动
                    case "categoryList":
                        self.sotrActionJson.categoryList = self.isAdd($(this))?-1:$(this).index();
                        break;  //商品分类列表
                    case "secondKill":
                        self.sotrActionJson.secondKill =self.isAdd($(this))?-1:$(this).index();

                        break;
                    case "navigation":
                        self.sotrActionJson.navList =self.isAdd($(this))?-1:$(this).index();

                }
            });

            self.SaveActionStore(self.sotrActionJson,isLoadInfo);
        },
        returnIndex:function(obj){
            var index;
            switch (obj.attr("id")) {
                case "search":
                    index=self.sotrActionJson.search;
                    break;  //搜索
                case "adList":
                    index=self.sotrActionJson.adList;
                    break; //头部幻灯片
                case "entranceList":
                    index=self.sotrActionJson.entranceList;
                    break; //分类导航
                case "eventList":
                    index=self.sotrActionJson.eventList;
                    break; //团购活动
                case "categoryList":
                    index=self.sotrActionJson.categoryList;
                    break;  //商品分类列表
                case "secondKill":
                    index=self.sotrActionJson.secondKill;
                    break;
                case "navigation":
                    index= self.sotrActionJson.navList;
            }
            return index
        },
        loadData: function () {
            this.allData = null;                //保存所有左侧load下来的数据 A B Ｃ区
            this.eventTypeList = [
                { key: 2, value: "限时抢购" },
                { key: 1, value: "疯狂团购" },
                { key: 3, value: "热销榜单"}
            ];    //B区tab名
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
        isAdd:function(obj){//利用该方法来判断模型是否已经存在，存在不添加 return false，
           if(obj.find(".jsListItem").length>0){
                      return false;
           }else{
               return true;
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
                debugger;
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.productLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.productLayer.hide();
                    $this.removeClass("on")
                }, 300);
            }).delegate(".jsGoodsItem", "click", function (e) {
                debugger;
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.productLayer.callback($this.data("itemid"),$this.data("eventid"), $this.data("itemname"), $this.data("imageurl"));
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

            }).delegate(".radiobtn", "click", function (e) {
                var radioType = $(this).attr("name");
                //搜索样式的判断
                if (radioType == "SearchStyle") {
                    if ($(this).val() == "s1") {
                        $(this).parents().find(".wrapInput").show();
                        self.ele.SearchDiv.find(".allClassify").show();
                    } else {
                        $(this).parents().find(".wrapInput").hide();
                        self.ele.SearchDiv.find(".allClassify").hide();
                    }

                } else if (radioType == "entranceStyle") {//分类导航的样式判断
                    var obj = self.allData["categoryEntrance"];
                    if ($(this).val() == "s1") {
                        obj.listLength = 8;
                    } else {
                        obj.listLength = 4;
                    }
                    self.renderEntranceList(obj);
                    self.renderEntranceItemList(obj);
                    self.ele.titleDom = $("#entranceList .jsListItem");
                    $("#addTitle").trigger("click").trigger("click");

                } else if (radioType == "titleStyle") {
                    if ($(this).val() == "tl1") {
                        self.ele.titleDom.find(".titleTxt").removeClass("bg");
                    } else {
                        self.ele.titleDom.find(".titleTxt").addClass("bg");
                    }
                }
                else if (radioType == "navStyle") {
                    if ($(this).val() == "s2") {
                        self.ele.navigation.css({"top":"0","bottom":"auto"}) ;
                        self.ele.sortableDiv.css({"padding-top":"83px","padding-bottom":"0px"})
                    } else {
                        self.ele.navigation.css({"top":"auto","bottom":"0"}) ;
                        self.ele.sortableDiv.css({"padding-bottom":"63px","padding-top":"0px"});
                    }
                }


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
                self.sortAction();
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

            }).delegate(".jsListItem", "click", function (e,isfunction) {
                /////////////////////////////// 点击左侧listitem，显示右侧模板begin  isfunction 如果传递为TRUE 不显示右侧这是执行把事件当方法用 ////////////////////////
                debugger;
                self.ele.titleDom = $(this);
                var $this = $(this),
                    key = $this.data("key"),
                    index = $this.data("index"),
                    type = $this.data("type"),
                    model = $this.data("model");
                //debugger;
                if (type && type.length) {
                    self.currentEditData = index == undefined ? self.allData[key] : self.allData[key][index];
                    var isAddTtile = true ,index= 0,titleIndex=0;

                    debugger;
                    if (self.currentEditData) {

                        //模块样式
                        if(self.currentEditData.styleType=='s2'){
                            index=1;
                        }
                         //标题样式
                        if(self.currentEditData.titleStyle=='tl2'){
                            titleIndex=1;
                        }
                        // 分发渲染
                        if (model == "ad") {
                            isAddTtile = false;
                            self.ele.editLayer.html(self.render(self.template.rightADTemp));
                            //附加后面的数据
                            //debugger;
                            //如果启用幻灯片 就可以添加多张图片，如果不启用幻灯片就一张图片
                            if ($("#ADTempCheck").attr("checked")) {
                                self.ele.editLayer.append(self.render(self.template.adItemListTemp, { list: self.currentEditData }));

                            } else {
                                self.ele.editLayer.append(self.render(self.template.adItemListTemp, { list: self.currentEditData }));
                            }

                            self.ele.editLayer.append('<div class="jsAddadItemTemp addLayerBtn"></div>');

                            // 注册上传按钮
                            self.registerUploadImgBtn();

                        } else if (model == "event") {
                            debugger;
                            isAddTtile = false;
                            var list = [];
                            for (var i = 0; i < self.currentEditData.arrayList.length; i++) {
                                var obj = self.currentEditData.arrayList[i];
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


                        } else if (model == "entrance") {
                            self.ele.editLayer.html(self.render(self.template.rightEntranceTemp));
                            self.renderEntranceItemList(self.currentEditData);
                            self.ele.editLayer.find('input[name="entranceStyle"]').eq(index).trigger("click");

                        } else if (model == "Search") {
                            debugger
                            isAddTtile = false;
                            self.ele.editLayer.html(self.render(self.template.rightSearchTemp));
                            self.ele.editLayer.find(".radiobtn").eq(index).trigger("click");  //无数据时默认为第一个有数据时根据数据的不同改变eq（0）的值
                            self.registerUploadImgBtn();
                        } else if(model=="secondKill"){
                            isAddTtile = false;
                            self.ele.editLayer.html(self.render(self.template.rightSecondKillTemp));
                            self.renderSecondKillItemList(self.currentEditData);
                        }else if(model=="nav") {
                            debugger;
                            isAddTtile = false;
                            self.renderNavItemList(self.currentEditData);
                            self.ele.editLayer.find(".radiobtn").eq(index).trigger("click");
                        }

                    } else {
                        // 添加空白模版
                        if (model == "ad") {
                            isAddTtile = false;
                            self.ele.editLayer.html(self.render(self.template.rightADTemp));
                            self.ele.editLayer.append('<div class="jsAddadItemTemp addLayerBtn"></div>');

                        } else if (model == "event") {
                            isAddTtile = false;
                            self.ele.editLayer.html(self.render(self.template.rightEventTemp, { eventTypeList: self.eventTypeList }));

                            //数据筛选
                            self.goodsSelect.typeId = self.ele.editLayer.find(".on").data("typeid");
                            self.goodsSelect.pageIndex = 0;
                            self.goodsSelect.loadData();
                        } else if (model == "category") {
                            self.ele.editLayer.html(self.render(self.template.rightCategoryTemp));
                            //c区模板选择
                            self.ele.editLayer.append(self.render(self.template.categoryModelTemp));
                            var cuttentTab = self.ele.editLayer.find(".jsSectionCTabContainer .on");
                            var modelId = cuttentTab.data("model");
                            self.renderCategoryItemList({ length: modelId });

                        } else if (model == "entrance") {
                            self.ele.editLayer.html(self.render(self.template.rightEntranceTemp));
                            self.renderEntranceItemList();
                        } else if (model == "Search") {
                            isAddTtile = false;
                            self.ele.editLayer.html(self.render(self.template.rightSearchTemp));
                            self.ele.editLayer.find(".radiobtn").eq(0).trigger("click");  //无数据时默认为第一个有数据时根据数据的不同改变eq（0）的值
                            self.registerUploadImgBtn();
                        } else if(model=="secondKill"){
                            isAddTtile = false;

                            self.ele.editLayer.html(self.render(self.template.rightSecondKillTemp));

                            //c区模板选择
                            self.ele.editLayer.append(self.render(self.template.SecondKillModelTemp));
                            var cuttentTab = self.ele.editLayer.find(".jsSectionCTabContainer .on");
                            var modelId = cuttentTab.data("model");
                            self.renderSecondKillItemList({ length: modelId });
                        }  else if(model=="nav") {
                            isAddTtile = false;
                            self.renderNavItemList({ length: 4 });
                            self.ele.editLayer.find(".radiobtn").eq(0).trigger("click");
                        }
                    }
                    //加载的样式处理
                    self.ele.editLayer.find(".jsTypeSelect").trigger("change",true);
                   if(isfunction){
                       self.ele.editLayer.hide();
                   } else{
                       self.ele.editLayer.show();
                   }

                    //添加一个按钮
                    if (isAddTtile) {
                        debugger;
                        self.ele.editLayer.find(".clearfix").eq(0).after(self.render(self.template.titleBtnModel));
                        if(self.currentEditData.titleName&&self.currentEditData.titleName.length>0) {
                            $("#addTitle").trigger("click",self.currentEditData.titleName);
                            self.ele.editLayer.find('input[name="titleStyle"]').eq(titleIndex).trigger("click");
                        }
                    }
                    // 修改左侧选中状态
                    self.ele.section.find(".jsListItem").removeClass("on");
                    $this.addClass("on");
                } else {
                    self.ele.editLayer.hide();
                }

                /////////////////////////////// 点击左侧listitem，显示右侧模板end  ////////////////////////
            }).delegate("#addTitle", "click", function (e,obj) {
                  if(obj){ $(this).parent().find(".titleText").val(obj);}
                self.ele.titleDom.find(".titlePanl").remove();
                if ($("#addTitle").text() == "添加标题") {
                    $("#addTitle").text("删除标题");
                    var $this = $(this);
                    $(this).parent().find(".setTitle").show();

                    if (self.ele.titleDom) {
                        if($(this).parent().find(".titleText").val().length>0){
                            self.ele.titleDom.prepend(self.render(self.template.titleModel, {title: $(this).parent().find(".titleText").val()}));
                        }else{
                            self.ele.titleDom.prepend(self.render(self.template.titleModel, {title: "默认标题"}));
                        }
                    }

                } else {
                    $("#addTitle").text("添加标题");
                    $(this).parent().find(".setTitle").hide();
                    self.ele.titleDom.find(".titlePanl").remove();

                }
            }).delegate("#addItem", "click", function (event) {
                //触发事件的源对象获取
                var $this = $(this),
                    type = $this.data("type");
                   type=event.srcElement ? event.srcElement : event.target;
                type=type.dataset.type;
                switch (type) {
                    case "Search":
                   if(self.isAdd(self.ele.SearchDiv)){
                       self.renderSearchList();
                   }else{
                        alert("搜索框已存在");
                    }
                        break;  //搜索
                    case "nav":
                        if(self.isAdd(self.ele.navigation)){
                            self.renderNavigationModel();
                        }else{
                        alert("导航栏已存在");
                    }
                        break; //底部导航
                    case "adList":
                        if(self.isAdd(self.ele.adList)){
                            self.renderADList();
                        }else{
                            alert("幻灯片已存在");
                        }


                        break; //头部幻灯片
                    case "entranceList":
                        if(self.isAdd(self.ele.entranceList)){
                            self.renderEntranceList();
                        }else {
                            alert("分类导航已存在");
                        }

                        break; //分类导航
                    case "eventList":
                           self.renderEventList();
                        break; //团购活动
                    case "categoryList":
                        if (self.ele.categoryList.find(".jsListItemEmpty").length == 0) {
                            self.ele.categoryList.append(self.render(self.template.categoryEmptyModel));
                        } else {
                            alert("请先编辑空白模板并保存");
                        }
                        break;  //商品分类列表
                    case "secondKill":
                            self.renderSecondKillList();
                        break;


                }
                self.sortAction(true);
            });
        },
        editLayerEvent: function () {

            // 给右侧编辑层 委托事件
            this.ele.editLayer.delegate(".jsCancelBtn", "click", function (e) {
                // 关闭右侧编辑层
                self.ele.editLayer.empty().hide();
                self.ele.section.find(".jsListItem").removeClass("on");

            }).delegate(".delIcon", "click", function (e) {
                var $this = $(this),
                    select = $this.siblings("select")[0];
                $this.hide();
                select.selectedIndex = 0;
                $this.parent().siblings(".infoContainer").children(".jsNameInput").val("");
                $this.parents(".jsAreaItem").eq(0).data("objectid", "").data("typeid", select.value.substring(select.value.indexOf("-") + 1));

            }).delegate("#ADTempCheck", "click", function (e) {
                //如果启用幻灯 删除按钮可用，如果不启用删除按钮不可用。
                if ($("#ADTempCheck").attr("checked")) {
                    self.ele.editLayer.find(".delBtn").show();
                } else {
                    self.ele.editLayer.find(".delBtn").hide();
                }


            }).delegate(".jsTypeSelect", "change", function (e,isSimulation) {
                //选择select类型  产品、资讯、类型....
                var $this = $(this);

                $this.siblings(".delIcon").show();
                if(!isSimulation) {
                    $this.parent().siblings(".infoContainer").children(".jsNameInput").val("");
                }
                $this.parents(".jsAreaItem").eq(0).data("objectid", "").data("typeid", this.value.substring(this.value.indexOf("-") + 1));


                if (this.value == "ad-2"||this.value=="cg-3"||this.value == "et-3") {
                    $this.parent().siblings(".infoContainer").show().children(".jsNameInput").removeAttr("disabled");
                    $this.parent().siblings(".infoContainer").children(".jsChooseBtn").attr("disabled", "disabled").css({"opacity": 0.2});
                } else if (this.value == "et-8") {
                    // 全部分类
                    $this.parent().siblings(".infoContainer").hide();
                } else if (this.value == "et-null") {
                    // 请选择
                    $this.parent().siblings(".infoContainer").hide();
                    $this.siblings(".delIcon").hide();
                } else if (this.value == "logo-2") {
                    $this.parent().children(".seeahType").show();
                } else if (this.value == "logo-1") {
                    $this.parent().children(".seeahType").hide();
                }else if (this.value == "sk-1"||this.value == "sk-2") {
                    var text=$(".jsTypeSelect option:selected").text();
                     self.ele.secondKill.find(".tit").children("b").text(text);
                }//自定义链接
                else if(this.value == "cg-31"||this.value == "cg-32"||this.value == "cg-33"||this.value == "cg-34"){
                    $this.parent().siblings(".infoContainer").hide();
                }
                else {
                    $this.parent().siblings(".infoContainer").children(".jsNameInput").attr("disabled", "disabled");
                    $this.parent().siblings(".infoContainer").show().children(".jsChooseBtn").removeAttr("disabled").css({"opacity": 1});
                }
            }).delegate(".jsNameInput", "blur", function (e) {
                // A区广告选择咨询后 输入框输入URL地址
                var $this = $(this);
                var value = $this.parent().siblings(".typeContainer").children(".jsTypeSelect").val();
                if(value) {
                    $this.parents(".jsAreaItem").eq(0).data("objectid", "").data("typeid", value.substring(value.indexOf("-") + 1));
                }
            }).delegate(".jsChooseBtn", "click", function (e) {
                debugger
                //输入框右侧的选择按钮
                /////////////////////////////////////~~~~~~~~~~~category~~~~~~~~~~~~~~~~~/////////////////////////////
                var $this = $(this),
                    type = $this.parent().siblings(".typeContainer").children("select").val();
                    if(!type){
                        type=self.ele.editLayer.find(".jsAreaTitle").eq(0).find(".typeContainer").find("select").val();

                    }
                //显示删除按钮
                $this.parent().siblings(".typeContainer").children(".delIcon").show();
                if (type == "cg-1" || type == "et-1") {
                    //分类
                    self.categoryLayer.pageIndex = 0;
                    self.categoryLayer.show(function (key, name) {
                        //注册回调
                        debugger;
                        $this.parents(".jsAreaItem").eq(0).data("objectid", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.categoryLayer.loadDate();
                } else if (type == "sk-2" || type == "sk-1") {
                    //产品
                    self.productLayer.pageIndex = 0;
                    self.productLayer.show(function (itemId,eventId, name,imageUrl) {
                        //注册回调
                        debugger;
                        $this.parents(".jsAreaItem").eq(0).data("imageurl",imageUrl).data("itemid", itemId).data("eventid", eventId).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                        $this.parents(".jsAreaItem").find(".wrapPic img").attr("src",imageUrl)
                    });
                    //执行一次搜索
                    self.productLayer.typeId=type.substring(type.indexOf("-") + 1);
                    self.productLayer.loadDate(null,null,"skill");
                }
                else if (type == "cg-2" || type == "ad-3") {
                    //产品
                    self.productLayer.pageIndex = 0;
                    self.productLayer.show(function (key, name) {
                        //注册回调
                        $this.parents(".jsAreaItem").eq(0).data("objectid", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.productLayer.loadDate();
                }
            }).delegate(".jsSaveKillBtn","click",function() {
                self.saveSeconKill();
            }).delegate(".jsSaveNavBtn","click",function(){
                self.SaveNav();
            }).delegate(".jsSaveSearchBtn","click",function(){
              self.saveSearch();
            }).delegate(".jsSaveCategoryBtn", "click", function (e) {
                //保存C区信息
                self.saveCategory();
            }).delegate(".jsSaveEntranceBtn", "click", function () {
                //保存C8区信息
                self.saveEntrance();
            }).delegate(".jsTab", "click", function () {
                debugger;
                //tab选择
                $(this).addClass("on").siblings().removeClass("on");

            }).delegate(".jsSectionCTabContainer .jsTab", "click", function () {
                //C区选择模板
                var modelId = $(this).data("model");
                var parentModel=$(this).parent().data("model");
                if (modelId) {
                    if(parentModel=="kill"){
                        self.renderSecondKillItemList({ length: modelId })
                    } else {
                        self.renderCategoryItemList({ length: modelId });
                    }
                }

            }).delegate(".jsTabContainer .jsTab", "click", function () {
                debugger;
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
                debugger;
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
                /*if (!$("#ADTempCheck").attr("checked")) {
                    alert("启用幻灯片才可以添加");
                    return;
                }*/
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
                    obj["objectId"] = $this.data("objectid")==""?$this.attr("data-objectid"):$this.data("objectid");
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
            //debugger;
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
                model.id = self.currentEditData.modelTypeId;
                model.name = self.currentEditData.modelTypeName;
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
            model.styleType= self.ele.editLayer.find('input[name="entranceStyle"]:checked').val();
            var addTitle=self.ele.editLayer.find("#addTitle").text()=="添加标题"?false:true;
            if(addTitle){
                model.titleName=self.ele.editLayer.find(".titleText").val();
                model.titleStyle=self.ele.editLayer.find('input[name="titleStyle"]:checked').val();
            }
            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["objectId"] = $this.data("objectid");
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["displayIndex"] = $this.data("displayindex");
                    obj["groupId"] = self.currentEditData.groupId;
                }

                    obj["displayIndex"] = i + 1;
                    if (obj.typeId == 3) {
                        obj["url"] = $this.find(".jsNameInput").val();
                        if(!obj.url){
                            flag = false;
                            alert("第" + (i + 1) + "项展示不能为空，请自定义的url链接");
                            return false;
                        }
                    } else {
                        obj["objectId"] = $this.data("objectid")==""?$this.attr("data-objectid"):$this.data("objectid");
                        if (!obj.objectId) {
                            flag = false;
                            alert("第" + (i + 1) + "项展示商品不能为空，请选择展示的商品或类型");
                            return false;
                        }
                    }

                    if (!obj.imageUrl) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                        return false;
                    }

                list.push(obj);
            });
            if (flag) {
                self.SaveItemCategory(list, model);
            }
        },
        SaveNav:function(){
            var list = [];
            var model = {};
            var flag = true;
            if (self.currentEditData&&self.currentEditData.modelTypeId) {//首次加载currentEditData对象不为空。
                model.id = self.currentEditData.modelTypeId;
                model.name = self.currentEditData.modelTypeName;
            } else {

                    model.id = 4;
                    model.name = "C区模板4";
               if(!self.currentEditData) {self.currentEditData={ss:"11"}}
            }
            model.styleType= self.ele.editLayer.find('input[name="navStyle"]:checked').val();

            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["navName"]=  $this.find(".navName").val()
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                switch (obj.typeId){
                    case "3": obj["url"] = $this.find(".jsNameInput").val();  break;
                    case "31": obj["url"] = "IndexShopApp"; break;
                    case "32": obj["url"] ="Category";  break;
                    case "33": obj["url"] = "GoodsCart";  break;
                    case "34": obj["url"] = "MyOrder";  break;
                    default: obj["objectId"] = $this.data("objectid"); break;
                }
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["groupId"] = self.currentEditData.groupId;
                } else {


                    if (!obj.objectId&&!obj["url"]) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示商品不能为空，请选择展示的商品或类型");
                        return false;
                    }
                    if (!obj.imageUrl) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                        return false;
                    }
                    if (!obj.url) {
                        flag = false;
                        alert("第" + (i + 1) + "自定义链接不可为空");
                        return false;
                    }
                    if (!obj.navName) {
                        flag = false;
                        alert("第" + (i + 1) + "导航名称必填");
                        return false;
                    }
                }
                list.push(obj);
            });
            if (flag) {
                self.SaveItemNav(list, model);
            }


        },
        saveEntrance: function () {
            var list = [];
            var model = {};
            var flag = true;
            //debugger;
            if (self.currentEditData) {
                model.groupId = self.currentEditData.groupId;
                model.id = self.currentEditData.modelTypeId;
                model.name = self.currentEditData.modelTypeName;

            } else {
                model.id = 8;
                model.name = "C区模板8";
            }
            model.styleType= self.ele.editLayer.find('input[name="entranceStyle"]:checked').val();
            var addTitle=self.ele.editLayer.find("#addTitle").text()=="添加标题"?false:true;
            if(addTitle){
                model.titleName=self.ele.editLayer.find(".titleText").val();
                model.titleStyle=self.ele.editLayer.find('input[name="titleStyle"]:checked').val();
            }


            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                //debugger;
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["groupId"] = self.currentEditData.groupId;
                }
                if (obj.typeId == 3) {
                    obj["url"] = $this.find(".jsNameInput").val();
                } else {
                    obj["objectId"] = $this.data("objectid")==""?$this.attr("data-objectid"):$this.data("objectid");
                }
                if (!obj.imageUrl) {
                    flag = false;
                    alert("第" + (i + 1) + "项信息已编辑，图片不完善");
                    return false;
                }
                if (!obj["typeId"]) {
                    flag = false;
                    alert("第" + (i + 1) + "项信息已编辑，选项分类不完善");
                    return false;
                }else{
                    if(obj["typeId"]==3){
                        if(!obj["url"]){
                            flag = false;
                            alert("第" + (i + 1) + "项信息已编辑,url不完善");
                            return false;
                        }
                    }

                }



                if (obj.imageUrl && (obj.typeId == 1 && obj.objectId||obj.typeId == 3 || obj.typeId == 8)) {
                    obj["displayIndex"] = list.length;
                    list.push(obj);
                }

            });

            if (flag) {
                self.SaveItemCategory(list, model);
            }
        },
        saveSeconKill:function(){
            var list = [];
            var model = {};
            var flag = true;
            var type= self.ele.editLayer.find(".jsTypeSelect").val()
            var typeid=type.substring(type.indexOf("-") + 1);
            model.shopsType = typeid;
            model.areaFlag="secondKill";
            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] =typeid;
                obj["itemId"] = $this.data("itemid");
                obj["itemName"] = $this.data("name");
                obj["eventId"] =$this.data("eventid");
                obj["imageUrl"] = $this.data("imageurl");
                obj["isUrl"] = 1;
                if(self.currentEditData&&self.allData.secondKill.arrayList) {
                           obj["itemAreaId"]= self.allData.secondKill.arrayList[i].eventAreaItemId;
                }
                    obj["displayIndex"] = i + 1;

                    if (!obj.itemId) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示商品不能为空，请选择展示的商品或类型");
                        return false;
                    }
                    if (!obj.imageUrl) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                        return false;
                    }

                list.push(obj);
            });



            if(flag){
                self.saveItemSeconKill(list,model);
            }

        },
        saveSearch:function(){
            debugger;
             var obj={},flag=true;
            obj["styleType"] = self.ele.editLayer.find('input[name="SearchStyle"]:checked').val();
            var islogo=self.ele.editLayer.find('.jsTypeSelect').val()=="logo-1"?false:true;
            if(obj.styleType=="s1"){
                  if(islogo){
                      obj["show"]="logo";

                      self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                          //debugger;
                          obj["imageUrl"] = $(this).data("imageurl");

                              if (!obj.imageUrl) {
                                  flag = false;
                                  alert("logo图片不能为空，请选择展示图片");
                                  return false;
                              }
                      });

                  }else{
                      obj["show"]="type";
                  }
            }
            if (self.currentEditData) {
                obj["MHSearchAreaID"] = self.currentEditData.MHSearchAreaID;
            }
            if(flag){
                self.SaveItemSearch(obj);
            }
        },
        renderNavItemList:function(obj){
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            if(obj.itemList&&obj.itemList.length>0){
                obj.length= obj.itemList.length;
            }else{
                obj.length=4;
            }
            self.ele.editLayer.html(self.render(self.template.rightNavigationTemp,obj));
            // 注册上传按钮
            self.registerUploadImgBtn();
        } ,
        renderCategoryItemList: function (obj) {
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            self.ele.editLayer.append(self.render(self.template.categoryItemListTemp, obj));
          /*  for(var i=0;i<obj.length;i++){

                self.ele.titleDom = $("#categoryList .jsListItem");
                self.ele.titleDom.prepend(self.render(self.template.titleModel, {title: obj[i].titleName}));
                if (obj[i].titleStyle == "tl1") {
                    self.ele.titleDom.find(".titleTxt").removeClass("bg");
                } else {
                    self.ele.titleDom.find(".titleTxt").addClass("bg");
                }
            }*/
            // 注册上传按钮
            self.registerUploadImgBtn();
        },
        renderSecondKillItemList:function(obj){
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            if(obj.arrayList&&obj.arrayList.length>0){
                obj.length= obj.arrayList.length;
            }else{
                obj.length=3;
            }

            self.ele.editLayer.append(self.render(self.template.SencondKillListTemp, obj));
            // 注册上传按钮
            self.registerUploadImgBtn();
        } ,
        renderEntranceItemList: function (obj) {
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            self.ele.editLayer.append(self.render(self.template.entranceItemListTemp, obj || {}));
            // 注册上传按钮
            self.registerUploadImgBtn();
        },
        registerUploadImgBtn: function () {
            var self = this;
            // 注册上传按钮
            self.ele.editLayer.find(".uploadImgBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },
        addUploadImgEvent: function (e) {
            self.uploadImg(e, function (ele, data) {
                //上传成功后回写数据
                debugger;
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
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=640',
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

                        debugger
                        if (callback) {
                            callback(data);
                        } else {
                            if(data.data.sortActionJson){
                                self.sotrActionJson=JSON.parse(self.allData.sortActionJson);
                                var  items=self.ele.sortableDiv.find(".action").toArray().sort(function(a,b){
                                    return  self.returnIndex($(a))-self.returnIndex($(b));
                                });
                                $(items).appendTo(self.ele.sortableDiv);
                            }
                            if (data.data.adList&& self.sotrActionJson.adList!=-1) {
                                self.renderADList(data.data.adList);
                            }
                            if (data.data.categoryEntrance&& self.sotrActionJson.entranceList!=-1) {
                                self.renderEntranceList(data.data.categoryEntrance);
                            }
                            if (data.data.eventList&&data.data.eventList.arrayList&&self.sotrActionJson.eventList!=-1) {
                                self.renderEventList(data.data.eventList.arrayList);
                            }
                            if (data.data.categoryList&&self.sotrActionJson.categoryList!=-1) {
                                self.renderCategoryList(data.data.categoryList);
                            }
                            if(data.data.search&&self.sotrActionJson.search!=-1){
                                self.renderSearchList(data.data.search);
                            }
                            if(data.data.secondKill&&self.sotrActionJson.secondKill!=-1){
                                self.renderSecondKillList(data.data.secondKill);
                            }

                            if(data.data.navList&&self.sotrActionJson.navList!=-1){
                                self.renderNavigationModel(data.data.navList);
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
                    groupId: model.groupId,
                    categoryList: JSON.stringify(list),
                    modelTypeId: model.id,
                    modelTypeName: model.name,
                    styleType:model.styleType,
                    titleName: model.titleName,
                    titleStyle:model. titleStyle
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

        //  Search
        //	    MHSearchAreaID		标识（为空新增，不为空更新）
        //      styleType			搜索框样式 s1 样式1（显示一个图标） s2样式2 （不显示图标）
        //      show 		        显示类型  logo（自定图标） type（默认的分类图标）
        //      imageUrl		    图片链接
        SaveItemSearch: function (list, callback) {
            this.ajax({
                url: self.url + "?method=SaveSeach",
                data: { seach: JSON.stringify(list) },
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
        } ,
        //        navList		导航
        //            typeId			类型ID： 1=商品分类 2=商品
        //            categoryAreaId 商品类别区域ID
        //            objectId 		对象ID
        //            objectName		对象名称
        //            groupId			分组ID（1、2、3…….）
        //            displayIndex		序号（1、2、3）
        //            imageUrl		图片链接
        SaveItemNav: function (list, model) {
            //debugger;
            this.ajax({
                url: self.url + "?method=SaveItemCategory",
                data: {
                    groupId: model.groupId,
                    categoryList: JSON.stringify(list),
                    modelTypeId: model.id,
                    modelTypeName: model.name,
                    styleType:model.styleType

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
        SaveActionStore:function(list,isLoadInfo,callback){
            this.ajax({
                url: self.url + "?method=UpdateMobileHomeSort",
                data: { sortActionJson: JSON.stringify(list) },
                dataType: "json",
                type: "post",
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        } else {
                            //alert("排序成功！");
                            self.ele.editLayer.empty().hide();
                            if(!isLoadInfo) {
                                self.GetHomePageConfigInfo();
                            }
                        }
                    } else {
                        alert(data.msg);
                    }
                },
                error: function (e) {

                }
            });
        },

        saveItemSeconKill:function(list,model){

            this.ajax({
                url: self.url + "?method=SaveEventItemArea",
                data: {
                    eventItemList: JSON.stringify(list),
                    areaFlag: model.areaFlag
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
        /********************** 左侧模板添加start**********************/
        renderSearchList: function (obj) {

            if (obj) {
                obj.key = "search";  //key是调用数据的核心，如self.allData[key]
                this.ele.SearchDiv.html(self.render(this.template.SearchEmptyModel, obj));
                if(obj.styleType=="s2"){
                    self.ele.SearchDiv.find(".allClassify").hide();
                } else{
                    self.ele.SearchDiv.find(".allClassify").show();
                    if(obj.show=="logo"){
                        var url="url("+obj.imageUrl+")";
                        self.ele.SearchDiv.find(".allClassify").css("background-image",url)
                    }
                }
            } else {
                this.ele.SearchDiv.html(self.render(this.template.SearchEmptyModel, { key: "search" }));
            }
        },
        renderNavigationModel:function(obj){
            if (obj) {
                obj.key = "navList";  //key是调用数据的核心，如self.allData[key]
                this.ele.navigation.html(self.render(this.template.navigationModel, obj));
                if (obj.styleType == "s2") {
                    self.ele.navigation.css({"top":"0","bottom":"auto"}) ;
                    self.ele.sortableDiv.css({"padding-top":"83px","padding-bottom":"0px"})
                } else {
                    self.ele.navigation.css({"top":"auto","bottom":"0"}) ;
                    self.ele.sortableDiv.css({"padding-bottom":"63px","padding-top":"0px"});
                }
            } else {
                this.ele.navigation.html(self.render(this.template.navigationModel, { key: "navList" }));
            }
        } ,
        renderSecondKillList: function (obj) {
              debugger;
            if (obj) {
                obj.key = "secondKill";  //key是调用数据的核心，如self.allData[key]
                if(obj.arrayList){
                    for(var i=0;i<obj.arrayList.length;i++){
                        var discount=obj.arrayList[i].salesPrice/obj.arrayList[i].price;
                        obj.arrayList[i].discount=(discount*10).toFixed(1);
                    }
                 debugger;
                var second =obj.arrayList[0].remainingSec;
                obj._h = Math.floor(second/3600);

                obj._m = Math.floor((second%3600)/60);

                obj._s = Math.floor(((second%3600)%60));
                }
                this.ele.secondKill.html(self.render(this.template.secondKillModel, obj));
            } else {
                this.ele.secondKill.html(self.render(this.template.secondKillModel, { key: "secondKill" }));
            }


        },
        renderADList: function (list) {
            var list = list || [];
            if (list.length) {
                this.ele.adList.html(self.render(this.template.adModel, { list: list, key: "adList" }));
                //this.ele.adList.html(self.render(this.template.adModelStatic));
                if (list.length>1){
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
                }

            } else {
                this.ele.adList.html(self.render(this.template.adModel));
            }

        },
        renderEntranceList: function (obj) {
            debugger;
            if (obj) {
                obj.key = "categoryEntrance";
                if(!obj.listLength){
                    obj.listLength=obj.itemList.length;
                }
                this.ele.entranceList.html(self.render(this.template.entranceModel, obj));
                if (obj.titleName && obj.titleName.length > 0) {
                    self.ele.titleDom = $("#entranceList .jsListItem");
                    self.ele.titleDom.prepend(self.render(self.template.titleModel, {title: obj.titleName}));
                    if (obj.titleStyle == "tl1") {
                        self.ele.titleDom.find(".titleTxt").removeClass("bg");
                    } else {
                        self.ele.titleDom.find(".titleTxt").addClass("bg");
                    }
                }
            } else {
                this.ele.entranceList.html(self.render(this.template.entranceModel, { key: "categoryEntrance" }));
            }

        },
        renderEventList: function (list) {
            var list = list || [];
            if (list.length) {
                this.ele.eventList.html(self.render(this.template.eventModel, { list: list, key: "eventList" }));
            } else {
                this.ele.eventList.html(self.render(this.template.eventEmptyModel));
            }

        },
        renderCategoryList: function (list) {
            var list = list || [];
            if (list.length) {
                this.ele.categoryList.html(self.render(this.template.categoryModel, { list: list, key: "categoryList" }));
                this.ele.categoryList.find(".jsListItem").each(function(){
                    $(this).trigger("click",true);
                });
            } else {
                //不需要添加空白项
                //this.ele.categoryList.html(self.render(this.template.categoryEmptyModel));
            }

        },
        /********************** 右侧模板添加start**********************/

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
            typeId:this.type,//只有掌声秒杀模块的时候才用到 typeId 1 表示团购（疯狂团购） 2表示限时抢购（掌上秒杀）的
            loadDate: function (id, text,type) {
                var categoryId = id || self.ele.productLayer.find("select").val();
                var itemName = itemName || self.ele.productLayer.find("input").val();
                if(type){
                    self.GetItemAreaByEventTypeID(this.typeId, this.pageIndex, this.pageSize,function(data){
                        var html = "";
                        if (data.data.totalCount) {
                            for (var i = 0; i < data.data.itemList.length; i++) {
                                var idata = data.data.itemList[i];
                                data.data.itemList[i].json = JSON.stringify(idata);
                            }
                            data.data.currentItemId = self.goodsSelect.currentItemId;
                            html = self.render(self.template.goodsKill, data.data);
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
                        $("#infoShow").html(html);


                    });
                }else {
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
                                        var pageStr = '   <div class="pagination">\
                                                    <a href="javascript:;" class="previous" data-action="previous">&lsaquo;</a>\
                                                    <input type="text" readonly="readonly" />\
                                                    <a href="javascript:;" class="next" data-action="next">&rsaquo;</a>\
                                                </div>';


                                        self.ele.productLayer.find('.pageWrap').html(pageStr).show();
                                        self.ele.productLayer.find('.pagination').jqPagination({
                                            current_page: self.productLayer.pageIndex + 1,
                                            max_page: pageNumber,
                                            paged: function (page) {
                                                self.productLayer.pageIndex = page - 1;
                                                self.productLayer.loadDate();
                                            }
                                        });
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
                                    var pageStr = '   <div class="pagination">\
                                                    <a href="javascript:;" class="previous" data-action="previous">&lsaquo;</a>\
                                                    <input type="text" readonly="readonly" />\
                                                    <a href="javascript:;" class="next" data-action="next">&rsaquo;</a>\
                                                </div>';

                                    self.ele.categoryLayer.find('.pageWrap').html(pageStr).show();
                                    self.ele.categoryLayer.find('.pagination').jqPagination({
                                        current_page: self.categoryLayer.pageIndex + 1,
                                        max_page: pageNumber,
                                        paged: function (page) {
                                            self.categoryLayer.pageIndex = page - 1;
                                            self.categoryLayer.loadDate();
                                        }
                                    });

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
                beforeSend: function () {
                },
                complete: function () {
                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(JSON.stringify(XMLHttpRequest));
                }
            };

            $.extend(_param, param);
            $.ajax(_param);
        }
    };
    self = page;
    page.init();
});
