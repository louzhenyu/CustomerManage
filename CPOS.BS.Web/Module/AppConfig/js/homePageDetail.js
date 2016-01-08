define(['jquery', 'homePageTemp','tools', 'touchslider','kkpager', 'jqueryui','template', 'kindeditor','easyui'], function ($, temp, touchslider) {
    //console.dir(touchslider);
    //debugger;
    //上传图片
    KE = KindEditor;
    var page = {
        url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
        template: temp,
        sotrActionJson: {},
        ele: {
            homeId:$.util.getUrlParam("homeId"),//"A8215024-6B50-4A17-842E-4221638F1DE5", //$.util.getUrlParam("homeId"),
            type:$.util.getUrlParam("optionType"),//"Edit",//$.util.getUrlParam("optionType")
            section: $("#section"),
            editLayer: $("#editLayer"),
            addItem: $("#addItem"),
            notRepetition:true, //防止点击两次
            productLayer: $("#productPopupLayer"),   //选择商品弹框

            productPopupLayerSkill:$("#productPopupLayerSkill"),//掌声秒杀的商品弹框
            categoryLayer: $("#categoryPopupLayer"), //选择商品分类弹框
            materialTextList:$("#materialTextList"),//图文素材，
            activityLayer: $("#activityPopupLayer"), //选择商品活动类型弹框
            commodityGroupLayer:$("#commodityGroupLayer"),//选择商品分组
            mask: $(".ui-mask"),
            sortableDiv: $("#sortable"),
            categorySelect: $("#categorySelect"),
            adList: $("[data-type='adList']"),  //幻灯片播放
            followInfo: $("[data-type='followInfo']"),//关注
            entranceList: $("[data-type='categoryEntrance']"),//分类导航
            eventList: $("[data-type='eventList']"),//团购抢购营销,
            originalityList: $("[data-type='originalityList']"),//创意组合
            SearchDiv: $("[data-type='search']"), //搜索框
            secondKill:$("[data-type='secondKill']"),//抢购
            hotBuy:$("[data-type='hotBuy']"),//  抢购
            groupBuy:$("[data-type='groupBuy']"),//团购
            classAction: $("#sortable .action"), //排序元素
            navigation:$("[data-type='navList']"),//顶部导航
            productList:$("[data-type='productList']"), //商品列表
            productListObject:{},
        },
        CountDown: function(maxtime,Dom) {

            if (maxtime >= 0) {
                var hour = Math.floor(maxtime / 60 / 60);
                var minutes = Math.floor(maxtime / 60);
                var seconds = Math.floor(maxtime % 60);
               var html= "<em>"+hour+"</em>:<em>12</em>:<em>23</em>" + minutes + " 分 " + seconds + " 秒";
                Dom.html(html);

            }
            else {
                clearInterval(timer);
                Dom.html("活动已经结束");
            }
        },
        init: function () {
            var that=$(this);
          if(this.ele.type.indexOf("#")!=-1) {
              var sear=location.hash;
              sear=decodeURIComponent(sear);
              var result=sear.replace("#_saveData_=","");
              try {
                  result = JSON.parse(result);
                  if (result.homeId) {
                      self.ele.homeId = result.homeId;
                      self.ele.type = "Edit";
                      this.loadData();
                      this.initEvent();
                      this.initSort();
                  }else{
                      console.log("地址栏数据异常。"+ex);
                  }
              }catch(ex){
                     console.log("地址栏数据异常。"+ex);
              }
          } else{
              this.loadData();
              this.initEvent();
              this.initSort();
          }


        },
        initSort: function () {

            var self = this;
            self.ele.sortableDiv.sortable({
                revert: true,
                opacity: 0.8,
                cancel:'[data-type="navList"],[data-type="followInfo"]',
                axis: "y",
                'distance':20,
                start: function () {
                    var str = this;

                },
                drag: function () {

                },
                stop: function () {
                    debugger;
                   self.sortAction();

                }
            });

            self.ele.classAction.disableSelection();

        },
        sortAction: function (isLoadInfo) {
            debugger
            self.sotrActionJson=[];
            var displayIndexList=[];
            var  actiondom;
            $("#sortable .action").each(function () {
                if(!$(this).html()){
                    $(this).remove();
                }
                if(!isLoadInfo) {
                    if ($(this).find(".jsListItemEmpty").length > 0) {
                        actiondom = $(this);
                        $(this).remove();
                    }
                }
                $(this).find(".select").remove();

            });

               $("#sortable .action").each(function () {
                   var name = "未知模块", me = $(this), index = me.index(), groupId = me.find(".jsListItem ").data("id");

                   switch (me.data("type")) {
                       case "search":
                           name = "搜索";
                           break;  //搜索
                       case "followInfo":
                           name="立即关注";
                           break;
                       case "adList":
                           name = "幻灯片播放";
                           break; //头部幻灯片
                       case "categoryEntrance":
                           name = "分类导航";
                           break; //分类导航
                       case "secondKill":   //限时抢购
                           name = "限时抢购";
                           break;
                       case "hotBuy":
                           name = "热销榜单";
                           break;  //商品分类列表
                       case "groupBuy":
                           name = "疯狂团购";
                           break;
                       case "originalityList":
                           name = "创意组合";
                           break;
                       case "eventList":
                           name = "热销榜单/疯狂团购/限时抢购组合框";
                           break;
                       case "productList":
                           name = "商品列表";
                           break;
                       case "navList":

                           name = "菜单导航";
                           break;

                   }
                   if (groupId) {
                       displayIndexList.push({groupId: groupId, index: index});
                   }
                   self.sotrActionJson.push({type: me.data("type"), name: name})

               });
               //groupId,index,homeId
               // self.sotrActionJson=[{"type":"search","name":"查询"},{"type":"adList","name":"幻灯片播放"},{"type":"categoryEntrance","name":"分类导航"},{"type":"secondKill","name":"抢购"},{"type":"eventList","name":"抢购团购热销组合框"},{"type":"originalityList","name":"创意组合"},{"type":"productList","name":"商品列表"},{"type":"productList","name":"商品列表"},{"type":"navList","name":"菜单导航"}]
               //self.sotrActionJson=[{"type":"search","name":"搜索"},{"type":"adList","name":"幻灯片播放"},{"type":"categoryEntrance","name":"分类导航"},{"type":"secondKill","name":"限时抢购"},{"type":"eventList","name":"热销榜单/疯狂团购/限时抢购组合框"},{"type":"originalityList","name":"创意组合"},{"type":"productList","name":"商品列表"},{"type":"productList","name":"商品列表"},{"type":"originalityList","name":"创意组合"},{"type":"secondKill","name":"限时抢购"},{"type":"navList","name":"菜单导航"}]

               self.SaveActionStore(self.sotrActionJson, isLoadInfo,displayIndexList);
            if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                self.ele.sortableDiv.find('[data-type="navList"]').before(actiondom)

            } else{
                self.ele.sortableDiv.append(actiondom);
            }
              var ele = {
                adList: $("[data-type='adList']"),  //幻灯片播放
                entranceList: $("[data-type='categoryEntrance']"),//分类导航
                eventList: $("[data-type='eventList']"),//团购抢购营销,
                originalityList: $("[data-type='originalityList']"),//创意组合
                SearchDiv: $("[data-type='search']"), //搜索框
                secondKill: $("[data-type='secondKill']"),// 团购 抢购 营销单个
                hotBuy:$("[data-type='hotBuy']"),//  抢购
                groupBuy:$("[data-type='groupBuy']"),//团购
                navigation: $("[data-type='navList']"),//顶部导航
                followInfo: $("[data-type='followInfo']"),//关注
                productList: $("[data-type='productList']") //上班列表

            };
              $.extend(self.ele, ele);

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
                case "originalityList":
                    index=self.sotrActionJson.originalityList;
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



      /*      self.ele.editLayer.delegate(".search .checkBox","click",function(){
                $(this).toggleClass("on");
                if ($(this).hasClass("on")) {
                    $(this).parents().find(".wrapInput").show();
                    self.ele.SearchDiv.find(".allClassify").show();
                } else {
                    $(this).parents().find(".wrapInput").hide();
                    self.ele.SearchDiv.find(".allClassify").hide();
                }


            });*/
            //初始化事件集
            this.sectionEvent();
            this.editLayerEvent();          //编辑区域事件
            this.categoryLayerEvent();
            this.productLayerEvent();
            this.commodityGroupLayerEvent();
        },
        categoryLayerEvent: function () {
            // 分类弹层 事件委托
            this.ele.categoryLayer.delegate(".searchBtn", "click", function () {
                self.categoryLayer.loadDate($(this).siblings().children().val());

            }).delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.mask.hide();

            }) .delegate(".categoryItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.categoryLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.categoryLayer.hide();
                    $this.removeClass("on");
                }, 300);
            });
            this.ele.activityLayer.delegate(".categoryItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.categoryLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.categoryLayer.hideActivity();
                    $this.removeClass("on");
                }, 300);
            }).delegate(".closePopupLayer", "click", function (e) {
                    $(this).parents(".popupLayer").hide();
                    self.mask.hide();
            });
            this.ele.materialTextList.delegate(".categoryItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.materialTextList.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.materialTextList.hide();
                    $this.removeClass("on");
                }, 300);
            }).delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.mask.hide();
            });
        },
        commodityGroupLayerEvent: function () {
            // 分类弹层 事件委托
            this.ele.commodityGroupLayer.delegate(".searchBtn", "click", function () {
                self.commodityGroupLayer.loadDate($(this).siblings().children().val());

            }).delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.mask.hide();

            }).delegate(".categoryItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.commodityGroupLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.commodityGroupLayer.hide();
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
            })
            this.ele.productPopupLayerSkill.delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.mask.hide();

            }).delegate(".jsGoodsItem", "click", function (e) {
                debugger;
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.productLayer.callback($this.data("itemid"),$this.data("eventid"), $this.data("itemname"), $this.data("imageurl"));
                setTimeout(function () {
                    self.productLayer.skillHide();
                    $this.removeClass("on")
                }, 300);
            });
        },
        sectionEvent: function () {
            // 给section委托事件
            this.ele.section.delegate(".saveHomPageBtn","click",function(){
                var obj={}
                 obj["Title"]=$("#searchType").val();
                 if(obj["Title"].trim().length>0&&obj["Title"].trim().length<=30) {

                     self.SaveHomePage(obj);
                 } else if(obj["Title"].trim().length>30){
                     alert("名称输入长度不可超过30");
                 }else{
                      alert("名称不可能为空");
                }


            }).delegate(".jsListItem", "hover", function (e) {
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

            }).delegate(".checkBox","click",function(){

                $(this).toggleClass("on");
                var me=$(this);
               var type=me.data("name");
                switch (type){
                    case "allList" :
                        if(me.hasClass("on")){
                            switch (me.data("value")){
                                case "showSalesPrice":  self.ele.productListObject.showSalesPrice=1;   break; //销售价格
                                case "showSalesQty"  :  self.ele.productListObject.showSalesQty=1;   break;   //销量
                                case "showName" : self.ele.productListObject.showName=1; break;  //商品名称
                                case "showDiscount" :  self.ele.productListObject.showDiscount=1; break; //商品折扣
                                case  "showPrice" : self.ele.productListObject.showPrice=1; break;      //商品价格
                            }
                        }else{
                            switch (me.data("value")){
                                case "showSalesPrice":  self.ele.productListObject.showSalesPrice=0;   break; //销售价格
                                case "showSalesQty"  :  self.ele.productListObject.showSalesQty=0;   break;   //销量
                                case "showName" : self.ele.productListObject.showName=0; break;  //商品名称
                                case "showDiscount" :  self.ele.productListObject.showDiscount=0; break; //商品折扣
                                case  "showPrice" : self.ele.productListObject.showPrice=0; break;      //商品价格
                            }
                        }
                        debugger;
                        self.ele.titleDom.html(self.render(self.template.productListModel,{ object: self.ele.productListObject}));
                        //self.renderProductTemple(self.currentEditData);


                        break;
                    case "SearchStyle":
                        if ($(this).hasClass("on")) {
                            $(this).parents().find(".wrapInput").show();
                            self.ele.SearchDiv.find(".allClassify").show();
                        } else {
                            $(this).parents().find(".wrapInput").hide();
                            self.ele.SearchDiv.find(".allClassify").hide();
                        }
                        break;
                }


            }).delegate(".radio","click",function(e){
                debugger;
                var radioType =$(this).data("name");
                var value=$(this).data("value");
                if(radioType) {
                    var selector = "[data-name='{0}']".format(radioType);
                    $(selector).removeClass("on");
                    $(this).addClass("on");
                    switch (radioType) {
                        case "entranceStyle":   //分类导航的样式判断
                            var obj = self.allData["categoryEntrance"];
                            obj.listLength = value;
                            obj.isEdit=true;
                            self.renderEntranceList(obj);
                            self.renderEntranceItemList(obj);
                            self.ele.titleDom = $("#entranceList .jsListItem");
                            /*$("#addTitle").trigger("click").trigger("click");*/
                            break;
                        case "titleStyle":
                            debugger;
                            if (value == "left") {
                                self.ele.titleDom.parent().find(".titleTxt").removeClass("center");
                                self.ele.titleDom.parent().find(".titlePanel").removeClass("hide");
                            } else if (value == "center") {
                                self.ele.titleDom.parent().find(".titleTxt").addClass("center");
                                self.ele.titleDom.parent().find(".titlePanel").removeClass("hide");
                            } else if (value == "hide") {
                                self.ele.titleDom.parent().find(".titlePanel").addClass("hide");
                            }
                            var offset= self.ele.titleDom.parents(".action").offset();
                            if(self.ele.titleDom.parents(".action").find(".space").length>0) {
                                self.ele.editLayer.css({"top": (offset.top+10) + "px", "left": (offset.left + 360) + "px"});
                            } else{
                                self.ele.editLayer.css({"top": offset.top + "px", "left": (offset.left + 360) + "px"});
                            }

                            break;
                        case  "styleType":
                            self.ele.productListObject.styleType=value;
                            self.ele.titleDom.html(self.render(self.template.productListModel,{ object:self.ele.productListObject,key:"productList"}));
                            break
                    }
                }
            }).delegate(".jsRemoveGroup", "click", function (e) {   // 删除
                debugger;

                    self.ele.editLayer.empty().hide();
                    self.ele.section.find(".jsListItem").removeClass("on");


                    var $this = $(this),
                        data=$this.parents(".jsListItem").data(),
                        gid =data.id||data.groupid;

                    $.messager.confirm("提示","确认删除该模块？",function(r){
                         if(r) {
                             if (gid) {
                                 self.DeleteItemCategoryArea(gid, function () {
                                     $this.parents(".action").remove();
                                     self.stopBubble(e);
                                     self.sortAction(true);
                                 });
                             } else {
                                 $this.parents(".action").remove();
                                 self.stopBubble(e);
                                 self.sortAction();
                             }


                         }
                    });

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

            }).delegate(".jsExitGroup", "click", function (e,isfunction) {
                /////////////////////////////// 点击左侧listitem，显示右侧模板begin  isfunction 如果传递为TRUE 不显示右侧这是执行把事件当方法用 ////////////////////////
                debugger;
                self.currentEditData=null;
                self.ele.titleDom = $(this).parents(".action").find(".jsListItem");
                var $this = $(this).parents(".action").find(".jsListItem"),
                    key = $this.data("key"),
                    index = $this.data("index"),
                    type = $this.data("type"),
                    model = $this.data("model"),
                    defaultName=$(this).parents(".action").find(".titlePanel .span").html();
                var offset=$(this).parents(".action").offset();
                if($(this).parents(".action").find(".space").length>0) {
                    self.ele.editLayer.css({"top": (offset.top+10) + "px", "left": (offset.left + 360) + "px"});
                } else{
                    self.ele.editLayer.css({"top": offset.top + "px", "left": (offset.left + 360) + "px"});
                }
                //debugger;
                if (type && type.length) {
                    // 修改左侧选中状态
                    self.ele.section.find(".jsListItem").removeClass("on");
                    self.ele.section.find(".select").remove();
                    $(this).parents(".action").append("<div class='select'></div>");
                    $this.addClass("on");
                    debugger;
                    self.currentEditData = index == undefined ? self.allData[key] : self.allData[key][index];
                    var isAddTitle = true,titleIndex=0;

                       switch (key) {     //获取可以添加多个的模板的编辑数据
                           case "originalityList" :
                           case "productList":
                           case "secondKill":
                           case "groupBuy":
                           case "hotBuy":
                               self.currentEditData=null;
                               if (self.allData[key].length > 0) {
                                   for (var i = 0; i < self.allData[key].length; i++) {
                                       if ($this.data("displayindex") == self.allData[key][i].displayIndex) {
                                           self.currentEditData = self.allData[key][i];
                                           break;//介绍for循环
                                       }
                                   }

                               }

                               break;
                           case "":
                               break;

                       }

                    debugger;
                    if (self.currentEditData) {

                        //模块样式
                        if(self.currentEditData.styleType=='s2'){
                            index=1;
                        }

                        // 分发渲染
                        if (model == "ad") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightADTemp));
                            //附加后面的数据
                            //debugger;
                            //如果启用幻灯片 就可以添加多张图片，如果不启用幻灯片就一张图片
                            if ($("#ADTempCheck").attr("checked")) {
                                self.ele.editLayer.append(self.render(self.template.adItemListTemp, { list: self.currentEditData }));

                            } else {
                                self.ele.editLayer.append(self.render(self.template.adItemListTemp, { list: self.currentEditData }));
                            }

                            self.ele.editLayer.append('<div class="jsAddadItemTemp addLayerBtn"><p>提示：最多可添加5张图片</p></div>');

                            // 注册上传按钮
                            self.registerUploadImgBtn();

                        } else if (model == "event") {
                            debugger;
                            isAddTitle = false;
                            var list = [];
                            for (var i = 0; i < self.currentEditData[0].arrayList.length; i++) {
                                var obj = self.currentEditData[0].arrayList[i];
                                obj.json = JSON.stringify(obj);
                                list.push(obj);
                            }
                            //debugger;
                            self.ele.editLayer.html(self.render(self.template.rightEventTemp, { list: list, eventTypeList: self.eventTypeList }));

                            var cuttentTab = self.ele.editLayer.find(".jsTabContainer .on");
                            var typeId = cuttentTab.data("typeid");
                            self.goodsSelect.eventId=list[0].eventId;
                            if (list[0].eventId) {
                                //数据筛选
                                self.goodsSelect.typeId = typeId;
                                self.goodsSelect.pageIndex = 0;
                                self.goodsSelect.currentItemId = cuttentTab.data("currentitemid");
                                self.goodsSelect.loadData();
                            }
                        } else if (model == "category") {

                                self.ele.editLayer.html(self.render(self.template.rightCategoryTemp));
                                //c区模板选择
                                 //self.ele.editLayer.append(self.render(self.template.categoryModelTemp));
                          /*      //附加后面的数据
                            var cuttentTab = self.ele.editLayer.find(".jsSectionCTabContainer .jsTab").removeClass("on");

                             var seleect ='data-model='+self.currentEditData.modelId;
                            cuttentTab.find("["+select+"]").addClass("on");*/
                                //debugger;
                                self.renderCategoryItemList(self.currentEditData);


                        }else if (model == "followInfo") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightFollowInfoTemp,self.currentEditData));
                        } else if (model == "entrance") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightEntranceTemp));

                            var length =8;
                            if(self.currentEditData.itemList&&self.currentEditData.itemList.length) {
                                length = self.currentEditData.listLength || self.currentEditData.length;
                            }
                            self.currentEditData.listLength=length;
                            //self.renderEntranceItemList(self.currentEditData);
                            self.ele.editLayer.find("[data-value='" +length+ "']").trigger("click");


                        } else if (model == "Search") {
                            debugger
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightSearchTemp));
                             if(self.currentEditData.styleType=="s1"){
                                        if(!self.ele.editLayer.find(".checkBox").hasClass("on")){
                                            self.ele.editLayer.find(".checkBox").trigger("click");
                                        }
                             } else{
                                 if(self.ele.editLayer.find(".checkBox").hasClass("on")){
                                     self.ele.editLayer.find(".checkBox").trigger("click");
                                 }
                             }

                            self.registerUploadImgBtn();
                        } else if(model=="secondKill"){
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightSecondKillTemp,self.currentEditData));
                            self.renderSecondKillItemList(self.currentEditData);
                        }else if(model=="nav") {
                            debugger;
                            isAddTitle = false;
                            self.renderNavItemList(self.currentEditData);
                            self.ele.editLayer.find(".radiobtn").eq(index).trigger("click");
                        }else if(model=="product"){
                            self.renderProductTemple(self.currentEditData);
                             self.ele.productListObject=self.currentEditData;

                        }

                    } else {
                        // 添加空白模版
                        if (model == "ad") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightADTemp));
                            self.ele.editLayer.append('<div class="jsAddadItemTemp addLayerBtn"></div>');

                        } else if (model == "followInfo") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightFollowInfoTemp));
                        }
                        else if (model == "event") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightEventTemp, { eventTypeList: self.eventTypeList }));

                            //数据筛选
                          /*  self.goodsSelect.typeId = self.ele.editLayer.find(".on").data("typeid");
                            self.goodsSelect.pageIndex = 0;
                            self.goodsSelect.loadData();*/
                        } else if (model == "category") {
                             debugger;

                            self.ele.editLayer.html(self.render(self.template.rightCategoryTemp));
                            //c区模板选择
                            self.ele.editLayer.append(self.render(self.template.categoryModelTemp));
                            var cuttentTab = self.ele.editLayer.find(".jsSectionCTabContainer .on");
                            var modelId = cuttentTab.data("model");
                            debugger;
                            self.renderCategoryItemList({ length: modelId });

                        } else if (model == "entrance") {
                            self.ele.editLayer.html(self.render(self.template.rightEntranceTemp));
                            //self.renderEntranceItemList();
                            self.ele.editLayer.find("[data-value='8']").trigger("click");

                        } else if (model == "Search") {
                            isAddTitle = false;
                            self.ele.editLayer.html(self.render(self.template.rightSearchTemp));
                            self.ele.editLayer.find(".radiobtn").eq(0).trigger("click");  //无数据时默认为第一个有数据时根据数据的不同改变eq（0）的值
                            self.registerUploadImgBtn();
                        } else if(model=="secondKill"){
                            debugger;
                            isAddTitle = false;
                            /*{ key: 2, value: "限时抢购" },
                            { key: 1, value: "疯狂团购" },
                            { key: 3, value: "热销榜单"}*/
                            var name="限时抢购"
                            if($this.data().typeid==1){
                                 name="疯狂团购";
                            }else if($this.data().typeid==2){
                                name="限时抢购";
                            }else if($this.data().typeid==3){
                                name="热销榜单";
                            }

                             var object={shopType:$this.data().typeid,titleName:name};
                            self.ele.editLayer.html(self.render(self.template.rightSecondKillTemp,object));

                            //c区模板选择
                            //self.ele.editLayer.append(self.render(self.template.SecondKillModelTemp));
                            //var cuttentTab = self.ele.editLayer.find(".jsSectionCTabContainer .on");
                            var modelId =1// cuttentTab.data("model");
                            self.renderSecondKillItemList({ length: modelId,shopType:$this.data().typeid});
                        }  else if(model=="nav") {
                            isAddTitle = false;
                            self.renderNavItemList({ length: 4 });
                            self.ele.editLayer.find(".radiobtn").eq(0).trigger("click");
                        }else if(model=="product"){
                            debugger
                             self.ele.productListObject={
                                showDiscount:1, //显示折扣
                                showPrice:1, //原价
                                showName:1,   //商品名称
                                showSalesPrice:1,  //价格
                                showSalesQty:1 , //销量
                                styleType:"s1",
                                TitleName:"商品列表",
                                titleStyle:"left",
                                showCount:6
                            };
                            self.renderProductTemple(self.ele.productListObject);

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
                    if (isAddTitle) {
                        debugger;
                        self.ele.editLayer.find(".option").eq(0).after(self.render(self.template.titleBtnModel));
                        if(self.currentEditData&&self.currentEditData.titleName&&self.currentEditData.titleName.length>0) {
                            //右侧标题文字赋值
                            $("#titlePanel").find(".titleInput input").val(self.currentEditData.titleName);
                            //右侧标题文字样式选定
                           // self.ele.editLayer.find('input[name="titleStyle"]').eq(titleIndex).trigger("click");
                        } else{
                            defaultName=defaultName ? defaultName :"";
                            $("#titlePanel").find(".titleInput input").val(defaultName);
                        }
                    }
                    //标题样式
                    //self.currentEditData.titleStyle="left";
                    //self.currentEditData.titleStyle="center";
                    //self.currentEditData.titleStyle="hide";
                    if(self.currentEditData) {
                        $(".setTitle").find('[data-value='+self.currentEditData.titleStyle+']').trigger("click");
                    } else{
                        $(".setTitle").find('[data-value="left"]').trigger("click");
                    }


                } else {
                    self.ele.editLayer.hide();
                }

                /////////////////////////////// 点击左侧listitem，显示右侧模板end  ////////////////////////
            }).delegate("#addTitle", "click", function (e,obj) {
                debugger;
                  if(obj){ $(this).parent().find(".titleText").val(obj);}
                self.ele.titleDom.find(".titlePanel").remove();
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
                    self.ele.titleDom.find(".titlePanel").remove();

                }
            }).delegate("#addItem .addBtn", "click", function (event) {
                debugger;
                //触发事件的源对象获取
                $("#set").remove();

                var $this = $(this),
                    type = $this.data("createtype");
                if(!type){
                    type=event.srcElement ? event.srcElement : event.target;
                    type=type.dataset.createype;
                }
                var html="";


                switch (type) {
                    case "Search":
                   if(self.isAdd(self.ele.SearchDiv)){
                        self.renderSearchList();
                }else{
                    alert("搜索框已存在");
                }
                break;  //搜索
                    case "followInfo":
                        if(self.isAdd(self.ele.followInfo)){
                            self.renderFollowInfo();
                        }else{
                            alert("立即关注已经存在");
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
                        if(self.isAdd(self.ele.eventList)){
                            self.renderEventList();
                        }else {
                            alert("所要添加模块只能添加一个");
                        }

                        break; //团购活动
                    case "originalityList":
                        if (self.ele.originalityList.find(".jsListItemEmpty").length == 0) {
                            html=self.render(self.template.titleModel,{title:"创意组合"})+self.render(self.template.categoryEmptyModel);
                            if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                                self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='originalityList'>" + html + "</div>")

                            } else{
                                self.ele.sortableDiv.append("<div class='action' data-type='originalityList'>" + html + "</div>")
                            }

                            self.ele.originalityList=$("[data-type='originalityList']");


                        } else {
                            alert("请先编辑空白模板并保存");
                        }
                        break;  //创意组合
                    case "productList":
                        if (self.ele.originalityList.find(".jsListItemEmpty").length == 0) {

                                 self.ele.productListObject={
                                    showDiscount:1, //显示折扣
                                    showPrice:1, //原价
                                    showName:1,   //商品名称
                                    showSalesPrice:1,  //价格
                                    showSalesQty:1 , //销量
                                    styleType:"s1",
                                    TitleName:"商品列表",
                                    titleStyle:"left",
                                    ShowCount:6
                                };
                            html=self.render(self.template.titleModel,{title:"商品列表"})+self.render(self.template.productListModel,{ object:self.ele.productListObject,key: "null" });
                            debugger;
                            if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                                self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='productList'>" + html + "</div>");
                            }else{
                                self.ele.sortableDiv.append("<div class='action' data-type='productList'>" + html + "</div>");
                            }

                            self.ele.productList = $("[data-type='productList']");


                        } else {
                            alert("请先编辑空白模板并保存");
                        }
                        break;  //商品分类列表
                    case "secondKill":
                        if ( self.ele.sortableDiv.find(".jsListItemEmpty").length==0) {
                            var obj = {};
                            obj.key = "secondKill";  //key是调用数据的核心，如self.allData[key]
                            obj.second = 0;
                            obj.shopType = $this.data("showtype");
                            if (obj.shopType == 1) {
                                obj["titleName"] = "疯狂团购";
                                obj.key = "groupBuy";
                            } else if (obj.shopType == 2) {
                                obj["titleName"] = "限时抢购";
                                obj.key = "secondKill";

                            } else if (obj.shopType == 3) {
                                obj["titleName"] = "热销榜单";
                                obj.key = "hotBuy";
                            }


                            $(this).html();
                            obj.length=1;
                            html = '<p class="space"></p>' + self.render(self.template.secondKillModel, obj);
                            if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                                self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='" + obj.key + "'>" + html + "</div>");
                            }else{
                                  self.ele.sortableDiv.append("<div class='action' data-type='" + obj.key + "'>" + html + "</div>");
                            }

                            self.ele.secondKill = $("[data-type='secondKill']");// 团购 抢购 营销单个
                            self.ele.hotBuy = $("[data-type='hotBuy']");//  抢购
                            self.ele.groupBuy = $("[data-type='groupBuy']");//团购
                        }else{
                            alert("请先编辑空白模板并保存");
                        }


                }
               // self.sortAction(true);
            });
        },
        editLayerEvent: function () {

            // 给右侧编辑层 委托事件
            this.ele.editLayer.delegate(".jsCancelBtn", "click", function (e) {
                // 关闭右侧编辑层
                self.ele.editLayer.empty().hide();
                self.ele.section.find(".jsListItem").removeClass("on");
                self.ele.section.find(".select").remove();
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
                $this.parent().siblings(".infoContainer").children(".jsChooseBtn").show();
                $this.parent().siblings(".infoContainer").children(".jsNameInput").css({"width":"80%"});
                //$this.siblings(".delIcon").show();
                if(!isSimulation) {
                    $this.parent().siblings(".infoContainer").children(".jsNameInput").val("");
                }
                $this.parents(".jsAreaItem").eq(0).data("objectid", "").data("typeid", this.value.substring(this.value.indexOf("-") + 1));


                if (this.value == "ad-2"||this.value=="cg-3"||this.value == "et-3") {
                    $this.parent().siblings(".infoContainer").show().children(".jsNameInput").css({"width":"100%"}).removeAttr("disabled");
                    $this.parent().siblings(".infoContainer").children(".jsChooseBtn").hide().attr("disabled", "disabled").css({"opacity": 0.2});
                } else if (this.value == "et-8") {
                    // 全部分类
                    $this.parent().siblings(".infoContainer").hide();
                } else if (this.value == "et-null" || this.value=="cg-null") {
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
                else if(this.value == "cg-31"||this.value == "cg-99"||this.value == "cg-37"||this.value == "cg-32"||this.value == "cg-33"||this.value == "cg-34"){
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

            }).delegate(".jsChooseEventBtn","click",function(e){ //团购抢购热销单个，
                debugger;

                var $this=$(this);
                self.categoryLayer.pageIndex = 0;
                self.categoryLayer.shopType= $this.parents(".jsAreaTitle").data("typeid")||$this.parents(".jsAreaItem").data("typeid")  ;
                if(self.categoryLayer.shopType) {
                    self.categoryLayer.showActivity(function (key, name) {
                        //注册回调
                        debugger;
                        $this.parents(".jsAreaTitle").eq(0).data("eventid", key).data("name", name);
                        $this.parents(".jsAreaItem").eq(0).data("eventid", key).data("name", name);
                        $this.siblings("input").val(name);
                    });

                    //执行一次搜索
                    self.categoryLayer.loadDate("killListType");
                }
            }).delegate(".jsChooseEventListBtn","click",function(e){  //团购抢购热销组合框
                debugger;

                var $this=$(this);
                self.categoryLayer.pageIndex = 0;
                self.categoryLayer.shopType=self.ele.editLayer.find(".jsTabContainer .on").data("typeid");
                if(self.categoryLayer.shopType) {
                    self.categoryLayer.showActivity(function (key, name) {
                        //注册回调
                        debugger;

                        $this.parents(".jsAreaTitle").eq(0).data("eventid", key).data("name", name);
                        $this.siblings("input").val(name);
                        var currentTab = self.ele.editLayer.find(".jsTabContainer .on");

                        if(currentTab.data("eventid")!=key) {
                            self.ele.editLayer.find(".jsGoodsList").html('<li style="text-align:center;">数据加载中...</li>');
                            //数据筛选
                            self.goodsSelect.eventId = key;
                            self.goodsSelect.typeId = self.categoryLayer.shopType;
                            self.goodsSelect.pageIndex = 0;
                            self.goodsSelect.loadData();
                            currentTab.data("inputvalue", name).data("eventid", key);
                        }else{
                            currentTab.data("inputvalue", name).data("eventid", key);
                        }


                    });

                    //执行一次搜索
                    self.categoryLayer.loadDate("killListType");
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
                if(!type){
                    type=self.ele.editLayer.find(".jsAreaTitle").data("type");

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
                }else if(type == "cg-4" ) {
                    //分类
                    self.commodityGroupLayer.pageIndex = 0;
                    self.commodityGroupLayer.show(function (key, name) {
                        //注册回调
                        debugger;
                        $this.parents(".jsAreaItem").eq(0).data("objectid", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.commodityGroupLayer.loadDate();



                } else if (type == "sk-2" || type == "sk-1") {
                    self.productLayer.eventId="";
                    self.productLayer.typeId=self.ele.editLayer.find(".jsAreaTitle").data("typeid");
                    self.productLayer.eventId=self.ele.editLayer.find(".jsAreaTitle").data("eventid");
                    if(self.productLayer.eventId){
                    //产品
                    self.productLayer.pageIndex = 0;
                    self.productLayer.skillShow(function (itemId,eventId, name,imageUrl) {
                        //注册回调
                        debugger;
                        $this.parents(".jsAreaItem").eq(0).data("itemid", itemId).data("eventid", eventId).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                       /* $this.parents(".jsAreaItem").find(".wrapPic img").attr("src",imageUrl)*/
                    });
                    //执行一次搜索
                    debugger;

                    self.productLayer.loadDate(null,null,"skill");
                    }else{
                        alert("请选择一个活动分组");
                    }
                }
                else if (type == "cg-2" || type == "ad-3") {
                    //产品
                    self.productLayer.pageIndex = 0;
                    self.productLayer.eventId="";
                    self.productLayer.typeId="";
                    self.productLayer.show(function (key, name) {
                        //注册回调
                        $this.parents(".jsAreaItem").eq(0).data("objectid", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.productLayer.loadDate();
                }else if( type=="cg-35"){

                    self.materialTextList.pageIndex = 0;
                    self.materialTextList.show(function (key, name) {
                        debugger
                        //注册回调
                        $this.parents(".jsAreaItem").eq(0).data("id", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.materialTextList.loadDate();
                }
            }).delegate(".jsSaveKillBtn","click",function() {
                self.saveSeconKill();
            }).delegate(".jsSaveNavBtn","click",function(){
                self.SaveNav();
            }).delegate(".jsSaveSearchBtn","click",function(){
              self.saveSearch();
            }).delegate(".jsSaveFollowBtn","click",function(e){
              self.saveFollowInfo();
            }).delegate(".jsSaveCategoryBtn", "click", function (e) {
                //保存C区信息
                self.saveCategory();
            }).delegate(".jsSaveProductBtn", "click", function (e) {
                //保存C区信息
                self.saveProduct();
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
                    if(parentModel=="kill"){    //团购，抢购，热销 独立用的模板

                        var shopType=self.ele.editLayer.find(".jsAreaTitle").data("typeid");
                        self.renderSecondKillItemList({ length: modelId,shopType:shopType });
                    } else {
                        self.renderCategoryItemList({ length: modelId });
                    }
                }

            }).delegate(".jsTabContainer .jsTab", "click", function () {
                debugger;
                //B区选择模板
                /////////////////////////////////////~~~~~~~~~~~event  B区~~~~~~~~~~~~~~~~~/////////////////////////////
                self.ele.editLayer.find(".jsGoodsList").html("");
                var typeId = $(this).data("typeid");
                var currentTabData=self.ele.editLayer.find(".jsTabContainer .on").data();
                if(currentTabData.eventid) {
                    self.ele.editLayer.find(".jsAreaTitle").eq(0).data("eventid", currentTabData.eventid);
                    self.ele.editLayer.find(".jsAreaTitle .jsNameInput").val(currentTabData.inputvalue);
                }else{
                    self.ele.editLayer.find(".jsAreaTitle").eq(0).data("eventid","");
                    self.ele.editLayer.find(".jsAreaTitle .jsNameInput").val("");
                }

                self.goodsSelect.eventId=self.ele.editLayer.find(".jsAreaTitle").eq(0).data("eventid");


                if ( self.goodsSelect.eventId) {
                    self.ele.editLayer.find(".jsGoodsList").html('<li style="text-align:center;">数据加载中...</li>');
                   //数据筛选
                    self.goodsSelect.typeId = typeId;
                    self.goodsSelect.pageIndex = 0;
                    self.goodsSelect.currentItemId = $(this).data("currentitemid");
                    self.goodsSelect.loadData();

                }

            }).delegate(".jsGoodsItem", "click", function () {
                //赋值给当前选中的dom tab元素
                debugger;
                var currentTab = self.ele.editLayer.find(".jsTabContainer .on");
                var itemData = $(this).data();


                currentTab.data("value", $(this).find(".radio").data("value"));
                currentTab.data("inputvalue", self.ele.editLayer.find(".jsAreaTitle .jsNameInput").val());
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
              if($this.parent().find(".jsAreaItem").length<5){
                  $this.before(self.render(self.template.adItemListTemp, { length: 1 }));
                  self.addUploadImgEvent($this.prev().find(".uploadImgBtn")[0]);
               }



            }).delegate(".jsDeladItemTemp", "click", function () {
                var index=$(this).parents(".jsAreaItem ").index();
                $(this).parents(".jsAreaItem ").eq(0).remove();
            }).delegate(".jsSaveADBtn", "click", function () {
                // 保存广告
                self.saveAD();
            }).delegate(".jsAreaItem", "click", function () {
                    // 保存广告

                });
        },
        saveAD: function () {
            var list = [];
            var flag = true;
            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(this);
                obj["typeId"] = $this.data("typeid");
                obj["imageUrl"] = $this.data("imageurl");
                obj["adId"] = $this.data("adid") || "";
                obj["displayIndex"] = i + 1;

                //obj["objectId"] = $this.data("objectid")||$(this).attr("data-objectid");
                obj["objectName"] = $this.data("name");

                    switch (obj.typeId.toString()){
                        case "3": obj["url"] = $this.find(".jsNameInput").val();  break;//自定义链接
                        case "99": obj["url"] = "nothing";  break;  //无
                        default: obj["objectId"] = $this.data("objectid")||$(this).attr("data-objectid");
                            //obj["objectName"]= $this.find(".jsNameInput").val();
                            break;   //1商品类型  2商品 4商品分组
                    }



                //debugger;

                if (!obj.imageUrl) {
                    flag = false;
                    alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                    return false;
                }
                if (obj.typeId) {
                    if (obj.typeId == 3) {
                        if (!obj.url) {
                            flag = false;
                            alert("第" + (i + 1) + "项展示广告不能为空，请填写资讯链接地址");
                            return false;
                        }
                    } else if (obj.typeId == 1||obj.typeId ==2||obj.typeId ==4) {
                        if (!obj.objectId) {
                            flag = false;
                            alert("第" + (i + 1) + "项展示广告不能为空，请选择展示的分类、分组或商品");
                            return false;
                        }
                    }else if(obj.typeId=="null"){
                        flag = false;
                    alert("第" + (i + 1) + "项展示广告不能为空，请选择展示的商品或填写资讯链接地址");
                    return false;
                    }

                } else  {
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
            var model = {}
            model.modelTypeId = 8;
            model.modelTypeName = "eventList";
            model.areaFlag = "eventList";
            model.showStyle = '1';
            model.displayIndex = self.ele.titleDom.parents(".action").index();
            //组合框保存

            if (self.currentEditData && self.currentEditData.length > 0 && self.currentEditData[0].modelTypeId) {
                model.id = self.currentEditData[0].modelTypeId;
                model.name = self.currentEditData[0].modelTypeName;

            }
            if (self.currentEditData && self.currentEditData.length > 0 && self.currentEditData[0].groupId) {
                model["groupId"] = self.currentEditData[0].groupId;
                model["CategoryAreaGroupId"] = self.currentEditData[0].groupId;
            }
            self.ele.editLayer.find(".jsTab").each(function (i, e) {

                var obj = {},
                    $this = $(e),
                    idata = $this.data().value;

                if (idata) {
                    //flag = false;

                    //return false;
                    obj["itemId"] = $this.data("currentitemid") || idata.itemId;
                    obj["eventId"] = $this.data("eventid") || idata.eventId;
                    obj["isUrl"] = 1;
                    obj["displayIndex"] = $this.data("displayindex") || (i + 1);
                    if (idata && idata.eventAreaItemId) {

                        obj["itemAreaId"] = idata.eventAreaItemId;

                    } else if (self.currentEditData && self.currentEditData[0].arrayList && self.currentEditData[0].arrayList[i]) {
                        obj["itemAreaId"] = self.currentEditData[0].arrayList[i].eventAreaItemId;

                    }
                    list.push(obj);
                } else {
                    alert("请为" + $this.html() + "选择商品");
                    flag = false;
                    return false;
                }

            });
            //debugger;
            if (flag) {
                self.SaveEventItemArea(list, model);
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
                /*f (!modelId) {
                    alert("获取不到模板信息");
                    return;
                } else {*/
                    model.id = 3;
                    model.name = "创意组合" + modelId;
             /*   }*/
            }
            debugger;
            model.styleType=model.id;
            model.titleStyle=self.ele.editLayer.find('[data-name="titleStyle"].on').data("value");//标题样式；
            if(model.titleStyle!="hide"){
                model.titleName=self.ele.editLayer.find(".titleInput input").val();
               if(!model.titleName) {
                   alert("标题文字不可为空");
                   return;
               }
            }
            if (self.currentEditData) {
                //obj["groupId"] = self.currentEditData.CategoryAreaGroupId;
                model["CategoryAreaGroupId"] = self.currentEditData.CategoryAreaGroupId;
            }


            model.displayIndex=self.ele.titleDom.parents(".action").index();
            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["displayIndex"]=$(this).data("displayindex");
                obj["objectId"] = $this.data("objectid");
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                switch (obj.typeId){
                    case "3": obj["url"] = $this.find(".jsNameInput").val();  break; //自定义链接
                    default: obj["objectId"] = $this.data("objectid"); obj["objectName"]= $this.find(".jsNameInput").val(); break;  //1商品类型  2商品链接  4商品分组
                }
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["displayIndex"] = $this.data("displayindex");
                    obj["groupId"] =$this.data("groupid");
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
        saveProduct:function(){
            var list = [];
            var model = {};
            var flag = true;
            if (self.currentEditData) {
                model.id = self.currentEditData.modelTypeId;
                model.name = self.currentEditData.modelTypeName;
                model.groupId = self.currentEditData.CategoryAreaGroupId;
                model.categoryAreaGroupId = self.currentEditData.CategoryAreaGroupId;
            } else {
                var modelId = self.ele.editLayer.find(".jsSectionCTabContainer .on").data("model");
                /*f (!modelId) {
                 alert("获取不到模板信息");
                 return;
                 } else {*/
                model.id = 2;
                model.name = "商品列表" + modelId;
                /*   }*/
            }
            debugger;
            model.styleType=self.ele.editLayer.find('[data-name="styleType"].on').data("value"); //商品展现样式
            model.titleStyle=self.ele.editLayer.find('[data-name="titleStyle"].on').data("value");//标题样式；
            model.showCount=self.ele.editLayer.find('[data-name="showCount"].on').data("value");//标题样式
            if(model.titleStyle!="hide"){
                model.titleName=self.ele.editLayer.find(".titleInput input").val();
                if(!model.titleName) {
                    alert("标题文字不可为空");
                    return;
                }
            }
            self.ele.editLayer.find(".checkBox").each(function(){
                var me=$(this);
                model[me.data("value")]=me.hasClass("on") ? 1 : 0;
            });
            model.displayIndex=self.ele.titleDom.parents(".action").index();
            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["displayIndex"]=$(this).data("displayindex");
                obj["objectId"] = $this.data("objectid");
                obj["objectName"] = $this.data("name");
                switch (obj.typeId){
                    default: obj["objectId"] = $this.data("objectid"); break;  //1商品类型  2商品链接  4商品分组
                }
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["displayIndex"] = $this.data("displayindex");
                    obj["groupId"] = self.currentEditData.CategoryAreaGroupId;
                }

                obj["displayIndex"] = i + 1;

                    obj["objectId"] = $this.data("objectid")==""?$this.attr("data-objectid"):$this.data("objectid");
                    if (!obj.objectId) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示类型不能为空，请选择展示的类型");
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
                model.modelTypeId = self.currentEditData.modelTypeId;
                model.modelTypeName = self.currentEditData.modelTypeName;
                model.groupId = self.currentEditData.CategoryAreaGroupId;
                model.categoryAreaGroupId = self.currentEditData.CategoryAreaGroupId;
            } else {

                    model.modelTypeId = 4;
                    model.modelTypeName = "底部导航";
               if(!self.currentEditData) {self.currentEditData={ss:"11"}}
            }
            debugger;
            model.displayIndex=self.ele.titleDom.parents(".action").index();

            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["navName"]=  $this.find(".navName").val()
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                switch (obj.typeId.toString()){
                    case "3": obj["url"] = $this.find(".jsNameInput").val();  break;
                    case "31": obj["url"] = "IndexShopApp"; break;
                    case "32": obj["url"] ="Category";  break;
                    case "33": obj["url"] = "GoodsCart";  break;
                    case "34": obj["url"] = "MyOrder";  break;
                    case "37": obj["url"] = "GetVipCard";  break;
                    case "99": obj["url"] = "nothing";  break;
                    default: obj["objectId"] = $this.data("objectid")||$this.attr("data-objectid"); break;
                }
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["groupId"] = self.currentEditData.groupId;
                }
                   obj["displayIndex"]=i+1;
                 debugger;
                    if (obj.typeId==99) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示商品不能为空，请选择展示的商品或类型");
                        return false;
                    }else if (obj.typeId==1||obj.typeId==2) {
                        if(!obj["objectId"]) {
                            flag = false;
                            alert("第" + (i + 1) + "选择展示的商品不可为空");
                            return false;
                        }

                    }else{
                        if(!obj["url"]) {
                            flag = false;
                            alert("第" + (i + 1) + "自定义链接不可为空");
                            return false;
                        }

                    }
                   if (!obj.navName) {
                        flag = false;
                        alert("第" + (i + 1) + "导航名称必填");
                        return false;
                    }
                if (!obj.imageUrl) {
                    flag = false;
                    alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                    return false;
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
            if (self.currentEditData && self.currentEditData.modelTypeId) {
                model.CategoryAreaGroupId = self.currentEditData.CategoryAreaGroupId;
                model.groupId = self.currentEditData.CategoryAreaGroupId;
                model.id = self.currentEditData.modelTypeId;
                model.name = self.currentEditData.modelTypeName;

            } else {
                model.id = 1;
                model.name = "分类导航";
            }
            model.styleType= self.ele.editLayer.find('.on[data-name="entranceStyle"]').data("value");
          /*  var addTitle=self.ele.editLayer.find("#addTitle").text()=="添加标题"?false:true;
            if(addTitle){
                model.titleName=self.ele.editLayer.find(".titleText").val();
                model.titleStyle=self.ele.editLayer.find('input[name="titleStyle"]:checked').val();
            }*/


            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {    //
                //debugger;
                var obj = {}, $this = $(e);
                obj["typeId"] = $this.data("typeid");
                obj["objectName"] = $this.data("name");
                obj["imageUrl"] = $this.data("imageurl");
                if (self.currentEditData) {
                    obj["categoryAreaId"] = $this.data("categoryareaid");
                    obj["groupId"] = self.currentEditData.groupId;
                }
                switch (obj.typeId.toString()){
                    case "3": obj["url"] = $this.find(".jsNameInput").val();  break; //自定义链接
                    case "31": obj["url"] = "IndexShopApp"; break;  //首页
                    case "32": obj["url"] ="Category";  break;
                    case "33": obj["url"] = "GoodsCart";  break;
                    case "34": obj["url"] = "MyOrder";  break;
                    case "37": obj["url"] = "GetVipCard";  break;
                    case "99": obj["url"] = "nothing";  break;
                    default: obj["objectId"] = $this.data("objectid")||$this.attr("data-objectid"); break;  //1商品类型  2商品链接  4商品分组
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
                    }  else if (obj.typeId == 1||obj.typeId ==2||obj.typeId ==4) {
                        if (!obj.objectId) {
                            flag = false;
                            alert("第" + (i + 1) + "项展示导航不能为空，请选择展示的分类、分组或商品");
                            return false;
                        }
                    }else if(obj.typeId=="null"){
                        flag = false;
                        alert("第" + (i + 1) + "项展示广告不能为空，请选择展示的商品或填写资讯链接地址");
                        return false;
                    }

                }

                obj["displayIndex"] = i + 1;

              /*  if (obj.imageUrl && (obj.typeId == 1 && obj.objectId||obj.typeId == 3 || obj.typeId == 8)) {
                    obj["displayIndex"] = list.length;

                }*/
              /*  obj["displayIndex"] = list.length;*/
                list.push(obj);
            });

            if (flag) {
                self.SaveItemCategory(list, model);
            }
        },
        saveSeconKill:function(){
            var list = [];
            var model = {};
            var flag = true;
            //model.displayIndex=self.ele.titleDom.parents(".action").index();
            var typeId= self.ele.editLayer.find(".jsAreaTitle ").data("typeid");

            model.shopType = typeId;


            self.ele.editLayer.find(".jsAreaItem").each(function (i, e) {
                var obj = {}, $this = $(e);
                obj["typeId"] =$this.data("typeid");
                obj["itemId"] = $this.data("eventid"); ///
                obj["itemName"] = $this.data("name");
                obj["eventId"] =$this.data("eventid");
                obj["eventName"] =$this.data("eventname");
                obj["imageUrl"] = $this.data("imageurl");
                obj["isUrl"] = 1;
                obj["itemAreaId"]= $this.data("eventareaitemid");
                    obj["displayIndex"] = i + 1;

                    if (!obj.itemId) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示分组不能为空，请选择展示的类型");
                        return false;
                    }
                    if (!obj.imageUrl) {
                        flag = false;
                        alert("第" + (i + 1) + "项展示图片不能为空，请选择展示图片");
                        return false;
                    }

                list.push(obj);
            });

            model.ShowStyle= 1 ;
            if (self.currentEditData&&self.currentEditData.length>0) {
                model.ShowStyle= self.currentEditData.showStyle;
               // model.name = self.currentEditData.modelTypeName;
                model.groupId = self.currentEditData.groupId;
            }
            //5：团购6:热销，7：秒杀 8:组合活动
            if (model.shopType == 1) {
                model.modelTypeId=5;
                model.modelTypeName="groupBuy";
                model.areaFlag = "groupBuy";
            } else if (model.shopType == 2) {
                model.areaFlag = "secondKill";
                model.modelTypeId=7;
                model.modelTypeName="secondKill"
            } else if (model.shopType == 3) {
                model.areaFlag = "hotBuy";
                model.modelTypeId=6;
                model.modelTypeName="hotBuy"
            }
            model.displayIndex=self.ele.titleDom.parents(".action").index();
            if(flag){
                self.saveItemSeconKill(list,model);
            }

        },
        saveFollowInfo:function(){
            var obj={},flag=true;
           debugger;
           var data= self.ele.editLayer.find(".jsAreaItem").eq(0).data();
           var $this= self.ele.editLayer.find(".jsAreaItem").eq(0);
            if (self.currentEditData) {
                obj["FollowId"] = self.currentEditData.FollowId;
            }
            obj["typeId"]=data.typeid;
            obj["title"]=self.ele.editLayer.find("#titleName").val();
            switch (obj.typeId.toString()){
                case "3": obj["url"] = $this.find(".jsNameInput").val();  break; //自定义链接
                case "99": obj["url"] = "nothing";  break;
                default: obj["textId"] = $this.data("id");  obj["textTitle"]=$this.find(".jsNameInput").val(); break;  //1商品类型  2商品链接  4商品分组
            }
            if(data.typeid==35){
                 if( !obj["textId"]){
                      flag=false;
                     alert("选择的图文素材不能为空");
                 }
            }else{
                if( !obj["url"]){
                    flag=false;
                    alert("自定义链接不可为空");
                }
            }
            if(obj["title"]){
                if(obj["title"].length>16&&obj["title"].length<2) {
                    flag = false;
                    alert("请输入欢迎语的长度在2-16个");
                }
            }else{
                alert("欢迎语不可为空");
            }
            if(flag){
                self.savItemeFollowInfo(obj);
            }
        },
        saveSearch:function(){
            debugger;
             var obj={},flag=true;
            obj["styleType"] =self.ele.editLayer.find('.checkBox').hasClass("on") ? "s1" : "s2";


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
                obj.itemList.sort(
                    function(a, b)
                    {
                        if(a.displayIndex < b.displayIndex) return -1;
                        if(a.displayIndex > b.displayIndex) return 1;
                        return 0;
                    }
                );
            }else{
                obj.length=4;
            }

            console.log(JSON.stringify(obj.itemlist));
            self.ele.editLayer.html(self.render(self.template.rightNavigationTemp,obj));
            // 注册上传按钮
            self.registerUploadImgBtn();
        } ,
        renderCategoryItemList: function (obj) {
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            self.ele.editLayer.find(".hint").remove()
            if(obj.itemList){
                self.ele.editLayer.append(self.render(self.template.categoryItemListTemp,{itemList: obj.itemList}))
            }else{
                self.ele.editLayer.append(self.render(self.template.categoryItemListTemp,{length: obj.length}))
            }
          /*  for(var i=0;i<obj.length;i++){

                self.ele.titleDom = $("#originalityList .jsListItem");
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
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle,.hint").remove();
            if(obj.arrayList&&obj.arrayList.length>0){
                obj.length= obj.arrayList.length;
                obj.eventId=obj.arrayList[0].eventId;
                obj.eventName=obj.arrayList[0].eventName;
            }

            self.ele.editLayer.append(self.render(self.template.SencondKillListTemp, obj));
            // 注册上传按钮
            self.registerUploadImgBtn();
        } ,
        renderEntranceItemList: function (obj) {
            debugger;
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();
            self.ele.editLayer.append(self.render(self.template.entranceItemListTemp, obj||{} ));
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
                $(ele).parent().find(".upTit").html("重新上传");
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
        SaveHomePage:function(obj){
            var data={homeId: self.ele.homeId}

            $.extend(data,obj);
            this.ajax({
                url: self.url + "?method=SaveHomePage",
                type: 'post',
                data: data,
                success: function (data) {
                    if (data.success) {
                       alert("保存成功");
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
                    homeId: self.ele.homeId,
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
        GetItemAreaByEventID: function (EventTypeID, EventId ,pageIndex, pageSize, callback) {
            this.ajax({
                url: self.url,
                type: 'get',
                data: {
                    method: "GetItemAreaByEventID",
                    EventTypeID: EventTypeID,
                    EventId:EventId,
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
            $.util.oldAjax({
                url: self.url,
                data: {
                    action: "GetHomePageConfigInfo",
                    homeId: self.ele.homeId,
                    type:self.ele.type//"Edit" //$.util.getUrlParam("type");//Add 新增 Edit
                },
                success: function (data) {
                    if (data.success) {
                        debugger;
                        if(self.ele.type=="Add"){
                           self.ele.type="Edit";
                            location.hash="#Edit"
                        }
                        self.ele.homeId=data.data.homeId;   //new
                        var option={
                            homeId:data.data.homeId  //第几页
                        };
                        //页面跳转之前将数据保存
                        $.util.setPageParam(option);
                        self.allData = data.data;

                        debugger

                        if(data.data) {
                            $("#searchType").val(data.data.title);
                            debugger;
                            var json=data.data.sortActionJson||"[]"
                            self.sotrActionJson = JSON.parse(json);
                            if (data.data.sortActionJson&&self.sotrActionJson.length>0) {

                                var html = self.render(self.template.actionList, {list: self.sotrActionJson});
                                self.ele.sortableDiv.html(html);
                                var ele = {
                                    adList: $("[data-type='adList']"),  //幻灯片播放
                                    entranceList: $("[data-type='categoryEntrance']"),//分类导航
                                    eventList: $("[data-type='eventList']"),//团购抢购营销,
                                    originalityList: $("[data-type='originalityList']"),//创意组合
                                    SearchDiv: $("[data-type='search']"), //搜索框
                                    secondKill: $("[data-type='secondKill']"),// 团购 抢购 营销单个
                                    hotBuy:$("[data-type='hotBuy']"),//  抢购
                                    groupBuy:$("[data-type='groupBuy']"),//团购
                                    navigation: $("[data-type='navList']"),//顶部导航
                                    followInfo: $("[data-type='followInfo']"),//关注
                                    productList: $("[data-type='productList']") //上班列表

                                };

                                $.extend(self.ele, ele);
                                var isEmpty=true;

                                if (data.data.adList&&data.data.adList.length>0&&self.ele.adList.length>0) {   //广告轮播左侧
                                    isEmpty=false
                                    self.renderADList(data.data.adList);
                                } else{
                                    self.ele.adList.remove();
                                }

                                if (data.data.categoryEntrance&&data.data.categoryEntrance.CategoryAreaGroupId&&self.ele.entranceList.length>0) {    //分类导航左侧
                                    isEmpty=false
                                    self.renderEntranceList(data.data.categoryEntrance);
                                }else{
                                    self.ele.entranceList.remove();
                                }
                                if (data.data.eventList&&data.data.eventList.length>0&&self.ele.eventList.length>0) {  //团购抢购组合框。
                                   debugger;
                                    isEmpty=false
                                    self.renderEventList(data.data.eventList[0]);
                                } else{
                                    self.ele.eventList.remove();
                                }
                                if (data.data.originalityList&&data.data.originalityList.length>0&&self.ele.originalityList.length>0) {   //创意组合。
                                    isEmpty=false
                                    self.renderOriginalityList(data.data.originalityList);
                                } else{
                                    self.ele.originalityList.remove();
                                }

                                if(data.data.productList&&data.data.productList.length>0&&self.ele.productList.length>0){   //商品列表
                                    isEmpty=false
                                    self.renderProductList(data.data.productList);
                                } else{
                                    self.ele.productList.remove();
                                }

                                if (data.data.follow&&data.data.follow.FollowId&&self.ele.followInfo.length>0) {   //关注
                                    isEmpty=false
                                    self.renderFollowInfo(data.data.follow);
                                } else{
                                    self.ele.followInfo.remove();
                                }
                                if (data.data.search&&data.data.search.MHSearchAreaID&&self.ele.SearchDiv.length>0) { //搜索
                                    isEmpty=false
                                    self.renderSearchList(data.data.search);
                                }  else{
                                    self.ele.SearchDiv.remove();
                                }
                                if (data.data.secondKill&&data.data.secondKill.length>0&&self.ele.secondKill.length>0) {  //秒杀
                                    isEmpty=false
                                    self.renderSecondKillList(data.data.secondKill);
                                }  else{
                                    self.ele.secondKill.remove();
                                }
                                if (data.data.groupBuy&&data.data.groupBuy.length>0&&self.ele.groupBuy.length>0) {//团购
                                    isEmpty=false
                                    self.renderSecondKillList(data.data.groupBuy);
                                } else{
                                    self.ele.groupBuy.remove();
                                }
                                if (data.data.hotBuy&&data.data.hotBuy.length>0&&self.ele.hotBuy.length>0) {    //热销
                                    isEmpty=false
                                    self.renderSecondKillList(data.data.hotBuy);
                                } else{
                                    self.ele.hotBuy.remove();
                                }
                                if (data.data.navList&&data.data.navList.CategoryAreaGroupId&&self.ele.navigation.length>0) {//底部导航
                                    isEmpty=false
                                    self.renderNavigationModel(data.data.navList);
                                } else{
                                    self.ele.navigation.remove();
                                }
                                if(isEmpty) {
                                    self.ele.sortableDiv.html("<p id='set' style='width: 100%; text-align:center; font-size: 16px; color: #000; line-height: 36px'>该首页模板无任何模块</p>");
                                }
                                /****倒计时***/
                                if( $(".timeList").length>0) {
                                    $(".timeList").each(function () {
                                        var me = $(this);
                                        var maxTime = parseInt(me.data("time"));

                                        var timer = setInterval(function () {
                                            if (maxTime >= 0) {
                                                /*var hour = Math.floor(maxTime / 60/60);
                                                 var minutes = Math.floor(maxTime % (60*12));
                                                 var seconds = Math.floor(maxTime % 60);*/
                                                var d = Math.floor(maxTime / 60 / 60 / 24);
                                                var h = Math.floor(maxTime / 60 / 60);
                                                var m = Math.floor(maxTime / 60 % 60);
                                                var s = Math.floor(maxTime % 60);
                                                if(h<10){
                                                    h="0"+h
                                                }
                                                if(m<10){
                                                    m="0"+m
                                                }
                                                if(s<10){
                                                    s="0"+s
                                                }
                                                var html = "<em>" + h + "</em>:<em>" + m + "</em>:<em>" + s + "</em>";
                                                me.html(html);
                                                --maxTime;

                                            }
                                            else {
                                                clearInterval(timer);
                                                me.html("活动已经结束").addClass("end");
                                            }
                                        }, 1000);
                                    });
                                }
                            }else{
                                self.ele.sortableDiv.html("<p id='set' style='width: 100%; text-align:center; font-size: 16px; color: #000; line-height: 36px'>该首页模板无任何模块</p>");
                                var ele = {
                                    adList: $("[data-type='adList']"),  //幻灯片播放
                                    entranceList: $("[data-type='categoryEntrance']"),//分类导航
                                    eventList: $("[data-type='eventList']"),//团购抢购营销,
                                    originalityList: $("[data-type='originalityList']"),//创意组合
                                    SearchDiv: $("[data-type='search']"), //搜索框
                                    secondKill: $("[data-type='secondKill']"),// 团购 抢购 营销单个
                                    hotBuy:$("[data-type='hotBuy']"),//  抢购
                                    groupBuy:$("[data-type='groupBuy']"),//团购
                                    navigation: $("[data-type='navList']"),//顶部导航
                                    followInfo: $("[data-type='followInfo']"),//关注
                                    productList: $("[data-type='productList']") //上班列表

                                };
                                $.extend(self.ele, ele);
                            }
                            if (callback) {
                                callback(data);
                            }
                        }else{
                            //空白模板，无任何数据
                        }


                    }
                    else {
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
        SaveEventItemArea: function (list, model, callback) {
            if(self.ele.notRepetition) {
                self.ele.notRepetition=false;
                //debugger;
                var data = {
                    eventItemList: JSON.stringify(list),
                    homeId: self.ele.homeId,
                };
                $.extend(data, model);
                this.ajax({
                    url: self.url + "?method=SaveEventItemArea",
                    data: data,
                    dataType: "json",
                    type: "post",
                    success: function (data) {

                        if (data.success) {
                            if (callback) {
                                callback(data);
                            } else {
                                alert("保存成功！");
                                self.ele.editLayer.empty().hide();
                                self.sortAction(true)
                            }
                        } else {
                            alert(data.msg);
                        }
                        self.ele.notRepetition=true;
                    },
                    error: function () {
                        self.ele.notRepetition=true
                    }
                });
            }
        },
        //        originalityList		分类集合
        //            typeId			类型ID： 1=商品分类 2=商品
        //            categoryAreaId 商品类别区域ID
        //            objectId 		对象ID
        //            objectName		对象名称
        //            groupId			分组ID（1、2、3…….）
        //            displayIndex		序号（1、2、3）
        //            imageUrl		图片链接
        SaveItemCategory: function (list, model) {
            if(self.ele.notRepetition) {
                self.ele.notRepetition = false;
                debugger;
                var data = {
                    homeId: self.ele.homeId,
                    categoryList: JSON.stringify(list),
                    modelTypeId: model.id,
                    modelTypeName: model.name
                }
                delete  model.id;
                delete  model.name;
                $.extend(data, model);
                this.ajax({
                    url: self.url + "?method=SaveItemCategory",
                    data: data,
                    dataType: "json",
                    type: "post",
                    success: function (data) {
                        self.ele.notRepetition = true;
                        if (data.success) {
                            alert("保存成功！");
                            self.ele.editLayer.empty().hide();
                            self.sortAction(true);

                        } else {
                            alert(data.msg);
                        }
                    },
                    error: function () {

                    }
                });
            }
        },
        //  adList
        //	    adAreaId		标识（为空新增，不为空更新）
        //      typeId			类型ID： 1=活动 2=资讯 3=商品 4=门店
        //      objectId 		对象ID
        //      displayIndex		序号
        //      imageUrl		图片链接
        SaveAds: function (list, callback) {
            if(self.ele.notRepetition) {
                self.ele.notRepetition = false;
                //debugger;
                this.ajax({
                    url: self.url + "?method=SaveAds",
                    data: {adList: JSON.stringify(list), homeId: self.ele.homeId},
                    dataType: "json",
                    type: "post",
                    success: function (data) {
                        self.ele.notRepetition = true;
                        if (data.success) {
                            if (callback) {
                                callback(data);
                            } else {
                                alert("保存成功！");
                                self.ele.editLayer.empty().hide();
                                self.sortAction(true);
                            }
                        } else {
                            alert(data.msg);
                        }
                    },
                    error: function (e) {

                    }
                });
            }
        },

        //  foollow
        //	    FollowId		标识（为空新增，不为空更新）
        //      textId	string	是	图文信息id
        //      typeId 		       3
        //      imageUrl		    图片链接
        savItemeFollowInfo:function(obj,callback){
            if(self.ele.notRepetition) {
                self.ele.notRepetition=false;
                var data = {homeId: self.ele.homeId};
                $.extend(data, obj);
                this.ajax({
                    url: self.url + "?method=SaveFollowInfo",
                    data: data,
                    dataType: "json",
                    type: "post",
                    success: function (data) {

                        if (data.success) {
                            if (callback) {
                                callback(data);
                            } else {
                                alert("保存成功！");
                                self.ele.editLayer.empty().hide();
                                self.sortAction(true);
                            }
                        } else {
                            alert(data.msg);
                        }
                        self.ele.notRepetition=true;
                    },
                    error: function (e) {
                        self.ele.notRepetition=true
                    }
                });
            }
        },


        //  Search
        //	    MHSearchAreaID		标识（为空新增，不为空更新）
        //      styleType			搜索框样式 s1 样式1（显示一个图标） s2样式2 （不显示图标）
        //      show 		        显示类型  logo（自定图标） type（默认的分类图标）
        //      imageUrl		    图片链接
        SaveItemSearch: function (list, callback) {
            if(self.ele.notRepetition) {
                self.ele.notRepetition=false;
                this.ajax({
                    url: self.url + "?method=SaveSeach",
                    data: {seach: JSON.stringify(list), homeId: self.ele.homeId},
                    dataType: "json",
                    type: "post",
                    success: function (data) {

                        if (data.success) {
                            if (callback) {
                                callback(data);
                            } else {
                                alert("保存成功！");
                                self.ele.editLayer.empty().hide();
                                self.sortAction(true);
                            }
                        } else {
                            alert(data.msg);
                        }
                        self.ele.notRepetition=true;
                    },
                    error: function (e) {
                        self.ele.notRepetition=true
                    }
                });
            }
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
            if (self.ele.notRepetition) {
                self.ele.notRepetition = false;
                //debugger;
                var data = {homeId: self.ele.homeId, categoryList: JSON.stringify(list)};
                $.extend(data, model);
                this.ajax({
                    url: self.url + "?method=SaveItemCategory",
                    data: data,
                    dataType: "json",
                    type: "post",
                    success: function (data) {
                        if (data.success) {
                            alert("保存成功！");
                            self.ele.editLayer.empty().hide();
                            self.sortAction(true);
                        } else {
                            alert(data.msg);
                        }
                        self.ele.notRepetition = true;
                    },
                    error: function () {

                    }
                });
            }

        },
        SaveActionStore:function(list,isLoadInfo,displayIndexList,callback){
            this.ajax({
                url: self.url + "?method=UpdateMobileHomeSort",
                data: {
                    sortActionJson: JSON.stringify(list),
                    homeId: self.ele.homeId,
                    displayIndexList: JSON.stringify(displayIndexList)
                },
                dataType: "json",
                type: "post",
                success: function (data) {
                    if (data.success) {
                        if (callback) {
                            callback(data);
                        } else {
                            //alert("排序成功！");
                            self.ele.editLayer.empty().hide();
                            if(isLoadInfo) {
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
            if(self.ele.notRepetition) {
                self.ele.notRepetition = false;

                var data = {
                    eventItemList: JSON.stringify(list),
                    homeId: self.ele.homeId,
                };
                $.extend(data, model);
                this.ajax({
                    url: self.url + "?method=SaveEventItemArea",
                    data: data,
                    dataType: "json",
                    type: "post",
                    success: function (data) {

                        if (data.success) {
                            alert("保存成功！");
                            self.ele.editLayer.empty().hide();
                            self.sortAction(true)
                        } else {
                            alert(data.msg);
                        }
                        self.ele.notRepetition=true;
                    },
                    error: function () {
                        self.ele.notRepetition=true
                    }
                });
            }
        },
        /********************** 左侧模板添加start**********************/
        renderSearchList: function (obj) {
            if(this.ele.SearchDiv.length==0){
                if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                    self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='search'></div>")

                } else{
                    self.ele.sortableDiv.append("<div class='action' data-type='search'></div>")
                }
            }
            self.ele.SearchDiv=$("[data-type='search']")
            if (obj) {
                debugger;
                obj.key = "search";  //key是调用数据的核心，如self.allData[key]
                this.ele.SearchDiv.html(self.render(self.template.SearchEmptyModel, obj));
                if(obj.styleType=="s2"){
                    self.ele.SearchDiv.find(".allClassify").hide();
                } else{
                    self.ele.SearchDiv.find(".allClassify").show();
                }
            } else {
                this.ele.SearchDiv.html(self.render(self.template.SearchEmptyModel, { key: "search" }));
            }
        },
        renderFollowInfo: function (obj) {
            if(this.ele.followInfo.length==0){
                    self.ele.sortableDiv.prepend("<div class='action' data-type='followInfo'></div>")
            }
            self.ele.followInfo=$("[data-type='followInfo']");
            if (obj) {
                obj.key="follow";
                this.ele.followInfo.html(self.render(self.template.followInfoModel, obj));
            } else {
                this.ele.followInfo.html(self.render(self.template.followInfoModel, { key: "follow" }));
            }
        },







        renderNavigationModel:function(obj){
           debugger;
            if(this.ele.navigation.length==0){

                    self.ele.sortableDiv.append("<div class='action' data-type='navList'></div>")

            }
            self.ele.navigation=$("[data-type='navList']");

            if (obj) {
                obj.key = "navList";  //key是调用数据的核心，如self.allData[key]
                obj.itemList.sort(
                    function(a, b)
                    {
                        if(a.displayIndex < b.displayIndex) return -1;
                        if(a.displayIndex > b.displayIndex) return 1;
                        return 0;
                    }
                );

                this.ele.navigation.html(self.render(this.template.navigationModel, obj));
             /*   if (obj.styleType == "s2") {
                    self.ele.navigation.css({"top":"0","bottom":"auto"}) ;
                    self.ele.sortableDiv.css({"padding-top":"83px","padding-bottom":"0px"})
                } else {
                    self.ele.navigation.css({"top":"auto","bottom":"0"}) ;
                    self.ele.sortableDiv.css({"padding-bottom":"63px","padding-top":"0px"});
                }*/
            } else {
                this.ele.navigation.html(self.render(this.template.navigationModel, { key: "navList" }));
            }
        } ,
        renderSecondKillList: function (list) {
              debugger;
            var list = list || [];
            if (list.length) {
                for (var i = 0; i < list.length; i++) {
                    var obj=list[i];
                    if (obj) {
                        //key是调用数据的核心，如self.allData[key]
                        obj.second =0;
                        if (obj.arrayList) {
                            if (obj.arrayList[0].remainingSec) {
                                obj.second = obj.arrayList[0].remainingSec;
                            }


                        }
                        if (obj.shopType == 1) {
                            obj["titleName"] = "疯狂团购";
                            obj.key = "groupBuy";
                            self.ele.groupBuy.each(function () {
                                debugger;
                                var index = $(this).index();
                                if (obj.displayIndex == index) {
                                    $(this).html('<p class="space"></p>');

                                    $(this).append(self.render(self.template.secondKillModel, obj));
                                }
                            });
                        } else if (obj.shopType == 2) {
                            obj["titleName"] = "限时抢购";
                            obj.key = "secondKill";
                            self.ele.secondKill.each(function () {
                                debugger;
                                var index = $(this).index();

                                if (obj.displayIndex == index) {
                                    $(this).html('<p class="space"></p>');

                                    $(this).append(self.render(self.template.secondKillModel, obj));
                                }
                            });
                        } else if (obj.shopType == 3) {
                            obj["titleName"] = "热销榜单";
                            obj.key = "hotBuy";
                            self.ele.hotBuy.each(function () {
                                debugger;
                                var index = $(this).index();

                                if (obj.displayIndex == index) {
                                    $(this).html('<p class="space"></p>');

                                    $(this).append(self.render(self.template.secondKillModel, obj));
                                }
                            });
                        }





                    }

                }


            }



        },
        renderADList: function (list) {
            if(this.ele.adList.length==0){
                if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                    self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='adList'></div>")

                } else{
                    self.ele.sortableDiv.append("<div class='action' data-type='adList'></div>")
                }
            }
            self.ele.adList=$("[data-type='adList']");
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
                this.ele.adList.html(self.render(this.template.adModel,{ list: list, key: "adList" }));
            }

        },
        renderEntranceList: function (obj) {  //分类导航左侧绘制
            debugger;
            if(this.ele.entranceList.length==0){
                if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                    self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='categoryEntrance'></div>")

                } else{
                    self.ele.sortableDiv.append("<div class='action' data-type='categoryEntrance'></div>")
                }
            }
            this.ele.entranceList=$("[data-type='categoryEntrance']");//团购抢购营销,
            if (obj&&obj.CategoryAreaGroupId) {
                obj.key = "categoryEntrance";
                if(!obj.listLength){
                    obj.listLength=obj.itemList.length;
                }


                this.ele.entranceList.html(self.render(this.template.entranceModel, obj));
                if(obj.isEdit){
                    this.ele.entranceList.append("<div class='select'></div>");
                }
                /*if (obj.titleName && obj.titleName.length > 0) {
                    self.ele.titleDom = $("#entranceList .jsListItem");
                    self.ele.titleDom.prepend(self.render(self.template.titleModel, {title: obj.titleName}));
                    if (obj.titleStyle == "tl1") {
                        self.ele.titleDom.find(".titleTxt").removeClass("bg");
                    } else {
                        self.ele.titleDom.find(".titleTxt").addClass("bg");
                    }
                }*/
            } else {
                var object={"key":"categoryEntrance"}
                $.extend(object,obj);
                this.ele.entranceList.html(self.render(this.template.entranceModel,object));
            }

        },
        renderEventList: function (obj) {

            if(this.ele.eventList.length==0){
                if(self.ele.sortableDiv.find('[data-type="navList"]').length>0) {
                    self.ele.sortableDiv.find('[data-type="navList"]').before("<div class='action' data-type='eventList'></div>")

                } else{
                    self.ele.sortableDiv.append("<div class='action' data-type='eventList'></div>")
                }
            }
            this.ele.eventList=$("[data-type='eventList']");//团购抢购营销,


               if (obj) {
                   this.ele.eventList.html(self.render(this.template.eventModel, obj));
               } else {
                   this.ele.eventList.html(self.render(this.template.eventEmptyModel));
               }

        },
        renderOriginalityList: function (list) {
            debugger;
            var list = list || [];
            if (list.length) {
                self.ele.originalityList.each(function(){
                    debugger;
                    var index=$(this).index();
                    var idata=null;
                    for(var i=0;i<list.length;i++){
                       if( list[i].displayIndex==index){
                           idata= list[i];
                           break
                       }
                    }

                   if(idata){
                       $(this).html('<p class="space"></p>');
                       if(idata.titleName){
                          idata.title=idata.titleName
                          $(this).append(self.render(self.template.titleModel,idata));
                       }
                       $(this).append(self.render(self.template.originalityModel, { idata: idata, key: "originalityList" }));
                   }
                });


            } else {

               // this.ele.originalityList.html(self.render(this.template.categoryEmptyModel));
            }

        },
        renderProductList:function(list){
            debugger;
            var list = list || [];
            if (list.length) {
                self.ele.productList.each(function(){
                    debugger;
                    var index=$(this).index();
                    var object=null;
                    for(var i=0;i<list.length;i++){
                        if( list[i].displayIndex==index){
                            object= list[i];
                            break
                        }
                    }

                    if(object){
                        $(this).html('<p class="space"></p>');
                        if(object.titleName){
                            object.title=object.titleName;
                            $(this).append(self.render(self.template.titleModel,object));
                        }
                        //object.json=JSON.stringify(object);
                        $(this).append(self.render(self.template.productListModel, { object: object, key: "productList" }));
                    }
                });

            }
        },

        renderProductTemple:function(obj){  //编辑处理
            self.ele.editLayer.find(".jsAreaItem,.jsAreaTitle").remove();

            self.ele.editLayer.html(self.render(self.template.rightProductTemp,obj));
           //编辑数据针对 基础数据初始化
            self.ele.editLayer.find(".radio").each(function() {
                debugger;
                var name = $(this).data("name"),me= $(this);
                if(obj[name]==me.data("value")) {
                    console.log(obj);
                    me.trigger("click");
                }
                  /*  $.each(obj,function(fieldName,fieldValue) {
                        if (name.toLowerCase() == fieldName.toLowerCase() && fieldValue ==  me.data("value")) {
                            me.trigger("click");
                        }
                    });*/

            });
            /***高效。精确，耦合高****/
            self.ele.editLayer.find(".checkBox").each(function() {
                debugger;
                var me=$(this), name = me.data("value");
                me.removeClass("on");
                if(obj[name]==1) {
                    me.trigger("click");
                }

             /* 性能低。精确，耦合度低

              $.each(obj,function(fieldName,fieldValue){
                    if (name.toLowerCase()==fieldName.toLowerCase()&&fieldValue==0) {
                        if(me.hasClass("on")){
                            me.trigger("click");
                        }else{
                            me.addClass("on")
                        }
                    } else if(name.toLowerCase()==fieldName.toLowerCase()&&fieldValue==1){
                        if(!me.hasClass("on")){
                            me.trigger("click");
                        } else{
                            me.addClass("on")
                        }
                    }

                })*/

            });

        },
        /********************** 右侧模板添加end**********************/

        materialTextList: {  // 所有选择商品的数据来源
            show: function (callback) {
                self.ele.materialTextList.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.ele.materialTextList.hide();
                self.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            /* typeId:this.typeId,//只有掌声秒杀模块的时候才用到 typeId 1 表示团购（疯狂团购） 2表示限时抢购（掌上秒杀）的e
             eventId:this.eventId,*/
            loadDate: function () {
                /*debugger;
                var categoryId = id || self.ele.productLayer.find("select").val();
                var itemName = itemName || self.ele.productLayer.find("input").val();*/
                $.util.ajax({
                        url: "/ApplicationInterface/Gateway.ashx",

                        data: {
                            action:"WX.MaterialText.GetMaterialTextListNew",
                            PageIndex: this.pageIndex,
                            PageSize: this.pageSize
                        },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            debugger;
                            var html = "";
                            if (data.Data.TotalCount) {
                                html = self.render(self.template.materialTextList, data.Data);
                                // 分页处理 begin
                                var pageNumber = Math.ceil(data.Data.TotalCount / self.materialTextList.pageSize);

                                if (pageNumber > 1) {
                                    kkpager.generPageHtml({
                                        pno: self.materialTextList.pageIndex,
                                        mode: 'click', //设置为click模式
                                        pagerid:'kpText',
                                        total: pageNumber,//总页码
                                        totalRecords: data.Data.TotalCount,
                                        isShowTotalPage: true,
                                        isShowTotalRecords: true,
                                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                        //适用于不刷新页面，比如ajax
                                        click: function (n) {
                                            //这里可以做自已的处理
                                            //...
                                            //处理完后可以手动条用selectPage进行页码选中切换
                                            this.selectPage(n);
                                            //让  tbody的内容变成加载中的图标
                                            //var table = $('table.dataTable');//that.tableMap[that.status];
                                            //var length = table.find("thead th").length;
                                            //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                            self.materialTextList.pageIndex=n;
                                            self.materialTextList.loadDate();
                                        },
                                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                        getHref: function (n) {
                                            return '#';
                                        }

                                    }, true);

                                    self.ele.materialTextList.find('.pageContianer').hide();
                                } else {
                                    self.ele.materialTextList.find('.pageContianer').hide();
                                }
                                // 分页处理 end

                            } else {
                                html = self.render(self.template.product, { itemList: [] });
                            }
                            $("#layerMaterialTextList").html(html);

                        } else {
                            alert(data.Message);
                        }
                    },

                    });


            }
        },

        goodsSelect: {     //组合框的数据来源
            pageIndex: 0,
            pageSize: 5,
            typeId: 1,
            currentItemId: undefined,
            loadData: function (callback) {
                self.GetItemAreaByEventID(this.typeId, this.eventId, this.pageIndex, this.pageSize, function (data) {
                   debugger;
                    var html = "";
                    self.ele.editLayer.find('.pageWrap').hide();
                    if (data.data.totalCount) {
                        for (var i = 0; i < data.data.itemList.length; i++) {
                            var idata = data.data.itemList[i];
                            data.data.itemList[i].json = JSON.stringify(idata);
                        }
                        data.data.currentItemId = self.goodsSelect.currentItemId;
                        html = self.render(self.template.goods, data.data);
                        // 分页处理 begin
                        var pageNumber = Math.ceil(data.data.totalCount / self.goodsSelect.pageSize);

                        if (pageNumber > 1) {

                                kkpager.generPageHtml({
                                    pno: self.goodsSelect.pageIndex?self.goodsSelect.pageIndex+1:1,
                                    mode: 'click', //设置为click模式
                                    pagerid:'kPageList',
                                    total: pageNumber,//总页码
                                    totalRecords: data.data.totalCount,
                                    isShowTotalPage: true,
                                    isShowTotalRecords: true,
                                    //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                    //适用于不刷新页面，比如ajax
                                    click: function (n) {
                                        var currentTab = self.ele.editLayer.find(".jsTabContainer .on");
                                        self.goodsSelect.currentItemId=currentTab.data("currentitemid");
                                        //这里可以做自已的处理
                                        //...
                                        //处理完后可以手动条用selectPage进行页码选中切换
                                        this.selectPage(n);
                                        //让  tbody的内容变成加载中的图标
                                        //var table = $('table.dataTable');//that.tableMap[that.status];
                                        //var length = table.find("thead th").length;
                                        //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                        self.goodsSelect.pageIndex=n-1
                                        self.goodsSelect.loadData();
                                    },
                                    //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                    getHref: function (n) {
                                        return '#';
                                    }

                                }, true);
                            self.ele.editLayer.find('.pageWrap').show();
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
        productLayer: {  // 所有选择商品的数据来源
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
            skillShow: function (callback) {
                self.ele.productPopupLayerSkill.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            skillHide: function () {
                self.ele.productPopupLayerSkill.hide();
                self.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
           /* typeId:this.typeId,//只有掌声秒杀模块的时候才用到 typeId 1 表示团购（疯狂团购） 2表示限时抢购（掌上秒杀）的e
            eventId:this.eventId,*/
            loadDate: function (id, text,type) {
                debugger;
                var categoryId = id || self.ele.productLayer.find("select").val();
                var itemName = itemName || self.ele.productLayer.find("input").val();
                $(".wrapSearch").show();
                if(type){
                    $(".wrapSearch").hide();
                    debugger;
                    self.GetItemAreaByEventID(this.typeId,this.eventId, this.pageIndex, this.pageSize,function(data){
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
                                kkpager.generPageHtml({
                                    pno: self.productLayer.pageIndex?self.productLayer.pageIndex+1:1,
                                    mode: 'click', //设置为click模式
                                    pagerid:'kkpager11',
                                    total: pageNumber,//总页码
                                    totalRecords: data.data.totalCount,
                                    isShowTotalPage: true,
                                    isShowTotalRecords: true,
                                    //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                    //适用于不刷新页面，比如ajax
                                    click: function (n) {
                                        //这里可以做自已的处理
                                        //...
                                        //处理完后可以手动条用selectPage进行页码选中切换
                                        this.selectPage(n);
                                        //让  tbody的内容变成加载中的图标
                                        //var table = $('table.dataTable');//that.tableMap[that.status];
                                        //var length = table.find("thead th").length;
                                        //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                        self.productLayer.pageIndex=n-1
                                        self.productLayer.loadDate();
                                    },
                                    //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                    getHref: function (n) {
                                        return '#';
                                    }

                                }, true);

                                self.ele.productLayer.find('.pageContianer').hide();
                            } else {
                                self.ele.editLayer.find('.pageContianer').hide();
                            }
                            // 分页处理 end

                        } else {
                            html = '<li style="text-align:center;">没有数据</li>';
                        }
                        $("#skillInfoShow").html(html);


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
                                        kkpager.generPageHtml({
                                            pno: self.productLayer.pageIndex?self.productLayer.pageIndex+1:1,
                                            mode: 'click', //设置为click模式
                                            pagerid:'kkpager11',
                                            total: pageNumber,//总页码
                                            totalRecords: data.data.totalCount,
                                            isShowTotalPage: true,
                                            isShowTotalRecords: true,
                                            //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                            //适用于不刷新页面，比如ajax
                                            click: function (n) {
                                                //这里可以做自已的处理
                                                //...
                                                //处理完后可以手动条用selectPage进行页码选中切换
                                                this.selectPage(n);
                                                //让  tbody的内容变成加载中的图标
                                                //var table = $('table.dataTable');//that.tableMap[that.status];
                                                //var length = table.find("thead th").length;
                                                //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                                self.productLayer.pageIndex=n-1
                                                self.productLayer.loadDate();
                                            },
                                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                            getHref: function (n) {
                                                return '#';
                                            }

                                        }, true);

                                        self.ele.productLayer.find('.pageContianer').hide();
                                    } else {
                                        self.ele.editLayer.find('.pageContianer').hide();
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
        categoryLayer: {   //商品分类 //团购抢购秒杀 分组的数据来源
            show: function (callback) {
                self.ele.categoryLayer.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            showActivity : function (callback) {
                self.ele.activityLayer.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hideActivity: function () {
                self.ele.activityLayer.hide();
                self.mask.hide();
            },
            hide: function () {
                self.ele.categoryLayer.hide();
                self.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            loadDate: function (text) {
                var categoryName = text || self.ele.categoryLayer.find("input").val();
                if(text=="killListType"){
                    self.ajax({
                        url: self.url,
                        type: 'get',
                        data: {
                            method: 'GetPanicbuyingEventList',
                            eventTypeId: self.categoryLayer.shopType,
                            pageIndex: this.pageIndex,
                            pageSize: this.pageSize
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var html = "";
                                debugger;
                                if (data.data.totalCount) {
                                    html = self.render(self.template.activity, data.data);
                                    // 分页处理 begin
                                    var pageNumber = Math.ceil(data.data.totalCount / self.categoryLayer.pageSize);
                                    if (pageNumber > 1) {
                                        self.ele.categoryLayer.find('.pageContianer').show();
                                        kkpager.generPageHtml({
                                            pno: self.categoryLayer.pageIndex ? self.categoryLayer.pageIndex + 1 : 1,
                                            mode: 'click', //设置为click模式
                                            pagerid: 'kkpager12',
                                            total: pageNumber,//总页码
                                            totalRecords: data.data.totalCount,
                                            isShowTotalPage: true,
                                            isShowTotalRecords: true,
                                            //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                            //适用于不刷新页面，比如ajax
                                            click: function (n) {
                                                //这里可以做自已的处理
                                                //...
                                                //处理完后可以手动条用selectPage进行页码选中切换
                                                this.selectPage(n);
                                                //让  tbody的内容变成加载中的图标
                                                //var table = $('table.dataTable');//that.tableMap[that.status];
                                                //var length = table.find("thead th").length;
                                                //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                                self.categoryLayer.pageIndex = n - 1;
                                                self.categoryLayer.loadDate();
                                            },
                                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                            getHref: function (n) {
                                                return '#';
                                            }

                                        }, true);


                                    } else {
                                        self.ele.activityLayer.find('.pageContianer').hide();
                                    }
                                    // 分页处理 end

                                } else {
                                    self.ele.activityLayer.hide();
                                    self.mask.hide();
                                    $.messager.confirm("提示","没有对应的活动分组，确认添加新的活动分组吗？",function(r){
                                              if(r){
                                                  location.href = "/module/GroupBuy/GroupList.aspx?pageType="+self.categoryLayer.shopType+"&mid="+ $.util.getUrlParam("mid")
                                              }
                                    })
                                }
                                $("#layerActivityLayer").html(html);
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }else {
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
                                debugger;
                                if (data.data.totalCount) {
                                    html = self.render(self.template.category, data.data);
                                    // 分页处理 begin
                                    var pageNumber = Math.ceil(data.data.totalCount / self.categoryLayer.pageSize);
                                    if (pageNumber > 1) {
                                        self.ele.categoryLayer.find('.pageContianer').show();
                                        kkpager.generPageHtml({
                                            pno: self.categoryLayer.pageIndex ? self.categoryLayer.pageIndex + 1 : 1,
                                            mode: 'click', //设置为click模式
                                            pagerid: 'kkpager12',
                                            total: pageNumber,//总页码
                                            totalRecords: data.data.totalCount,
                                            isShowTotalPage: true,
                                            isShowTotalRecords: true,
                                            //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                            //适用于不刷新页面，比如ajax
                                            click: function (n) {
                                                //这里可以做自已的处理
                                                //...
                                                //处理完后可以手动条用selectPage进行页码选中切换
                                                this.selectPage(n);
                                                //让  tbody的内容变成加载中的图标
                                                //var table = $('table.dataTable');//that.tableMap[that.status];
                                                //var length = table.find("thead th").length;
                                                //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                                self.categoryLayer.pageIndex = n - 1
                                                self.categoryLayer.loadDate();
                                            },
                                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                            getHref: function (n) {
                                                return '#';
                                            }

                                        }, true);


                                    } else {
                                        self.ele.categoryLayer.find('.pageContianer').hide();
                                    }
                                    // 分页处理 end

                                } else {
                                    html = self.render(self.template.product, {itemList: []});
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
            }
        },

        commodityGroupLayer: { //商品分组
            show: function (callback) {
                self.ele.commodityGroupLayer.show();
                self.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.ele.commodityGroupLayer.hide();
                self.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            loadDate: function (text) {
                var groupName = text || self.ele.commodityGroupLayer.find("input").val();
                self.ajax({
                    url: self.url,
                    type: 'get',
                    data: {
                        method: 'GetItemGroup',
                        groupName: groupName,
                        pageIndex: this.pageIndex,
                        pageSize: this.pageSize
                    },
                    dataType: "json",
                    success: function (data) {
                        if (data.success) {
                            var html = "";
                            debugger;
                            if (data.data.totalCount) {
                                html = self.render(self.template.groupList, data.data);
                                // 分页处理 begin
                                var pageNumber = Math.ceil(data.data.totalCount / self.commodityGroupLayer.pageSize);
                                if (pageNumber > 1) {

                                    kkpager.generPageHtml({
                                        pno: self.commodityGroupLayer.pageIndex?self.commodityGroupLayer.pageIndex+1:1,
                                        mode: 'click', //设置为click模式
                                        pagerid:'kkpager13',
                                        total: pageNumber,//总页码
                                        totalRecords: data.data.totalCount,
                                        isShowTotalPage: true,
                                        isShowTotalRecords: true,
                                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                                        //适用于不刷新页面，比如ajax
                                        click: function (n) {
                                            //这里可以做自已的处理
                                            //...
                                            //处理完后可以手动条用selectPage进行页码选中切换
                                            this.selectPage(n);
                                            //让  tbody的内容变成加载中的图标
                                            //var table = $('table.dataTable');//that.tableMap[that.status];
                                            //var length = table.find("thead th").length;
                                            //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');
                                            self.commodityGroupLayer.pageIndex=n-1
                                            self.commodityGroupLayer.loadDate();
                                        },
                                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                        getHref: function (n) {
                                            return '#';
                                        }

                                    }, true);

                                    self.ele.categoryLayer.find('.pageContianer').show();

                                } else {
                                    self.ele.commodityGroupLayer.find('.pageContianer').hide();
                                }
                                // 分页处理 end

                            } else {
                                html = self.render(self.template.product, { itemList: [] });
                            }
                            $("#layerCommodityList").html(html);
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
