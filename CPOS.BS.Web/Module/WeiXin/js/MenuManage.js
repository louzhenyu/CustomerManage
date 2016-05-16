define(['jquery', 'tools', 'template', 'kindeditor', 'lang', 'pagination', 'drag'], function () {
    //debugger;
    //上传图片
    KE = window.KindEditor;
    var page =
        {
            subMenuCount: 10,  //设置子菜单可有的数量
            saveType: "add", //保存菜单
            Name: "",  //图文名称
            TypeId: "",
            //默认控制条数
            currentPage: 0,
            //默认第一页
            pageParamJson: [],
            MenuId: "",
            //保存的MenuId
            //图文素材ID
            url: "/ApplicationInterface/Gateway.ashx",
            generateUrl: "",
            //图文关联后的url地址
            unionType: 0,
            count: 0,
            //加载请求的个数
            toSave:
            {
                //针对系统模块
                domObj: null,
                //要保存的dom对象的下拉框
                moduleType: 0,
                //模块类型
                urlTemplate: "",
                //要传递替换的参数
                detailDomObj: ""
                //弹出层选择后要填充的层

            },
            statusDomobj: null,  //该对象用来保存状态的dom节点
            //关联到的类别
            elems:
            {
                unionCategory: $("#unionCategory"),
                menuContent: $("#menuContent"),   //点击菜单的时候让文字显示
                publishMenu: $("#publishMenu"),  //发布菜单
                imageContentDiv: $("#imageContentMessage"),    //所有的图文关联父层
                imageContentItems: $("#imageContentItems"),
                addImageMessageDiv: $("#addImageMessage"),  //弹出图文列表
                menuArea: $("#menuArea"),
                //菜单区域
                menuBar: $("#menuArea .menuWrap"),
                //菜单条
                menuTitle: $("#menuTitle"),
                //菜单名称
                menuNo: $("#no"),
                //菜单序号
                isUse: $("#isUse"),
                //是否启用
                message: $("#message"),
                //消息层
                messageSelect: $("#message .selectBox"),
                //消息类型选择下拉
                toUploadImage: $("#toUploadImage"),
                //要上传的图片
                imageContentMessage: $("#imageContentMessage"),
                //图文消息层
                contentArea: $("#contentArea"),
                //右边区域
                saveBtn: $("#saveBtn"),
                //保存菜单
                delBtn: $("#delBtn"),
                //删除菜单
                lottoryWay: $("#lottoryWay"),

                //抽奖方式
                lottoryWayInput: $("#lottoryWay .inputBox"),
                //抽奖方式选择
                saveData: $("#btnSaveData"),
                //标题
                weixinAccount: $("#weixinAccount"),
                weixinList:$("#weixinList"),

                //微信号
                accountSelect: $("#weixinAccount .selectBox"),
                imageCategory: $("#imageCategory"),
                //图文分类
                imageCategorySelect: $("#imageCategory .selectBox"),
                //图文分类下拉框
                btnUpload: $("#uploadImage"),
                //图片上传
                imageView: $("#upImage"),
                //上传之后的展示
                category: $("#category"),
                //分类选择
                linkUrl: $("#contentUrl"),
                //链接地址
                urlAddress: $("#urlAddress"),
                //链接
                contentEditor: $("#contentEditor"),
                //详情内容编辑
                moduleName: $("#moduleName"),
                //模块名
                moduleSelect: $("#moduleName .selectBox"),
                events: $("#eventsType"),
                //活动
                newsType: $("#newsType"),
                //资讯分类
                newsTypeSelect: $("#newsType .selectBox"),
                //资讯类别
                newsDetail: $("#newsDetail"),
                //选择详情资讯
                newsDetailSelect: $("#newsDetail .inputBox"),
                //详情选择框
                shopsType: $("#shopsType"),
                //门店分类
                shopsSelect: $("#shopsType .selectBox"),
                //门店分类的下拉列表
                eventSelect: $("#eventsType .selectBox"),
                //活动的选择框
                eventDetail: $("#eventsDetail"),
                //活动详情
                eventDetailSelect: $("#eventsDetail .inputBox"),
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
                //遮罩层
                chooseEventsDiv: $("#chooseEvents")//选择活动层
            },
            popupOptions:
            {
                popupName: "",
                //弹出层   要输入的名称
                popupSelectName: "",
                //弹出层   下拉列表要显示的名称
                title: [],
                //弹出层    表格的title
                url: ""
                //弹出层的请求url   搜索的   分页的
            },
            //弹出类别后  确认之后的内容
            chooseOptions: {},
            //点击新增菜单 则把内容清空
            clearInput: function () {
                //设置菜单为空
                this.elems.menuTitle.val("");
                //序号
                this.elems.menuNo.val("");
                //文本设置为空
                $("#text").val("");
                //设置链接为空
                this.elems.urlAddress.val("");
                //是否启用
                this.elems.isUse.find("option[value='1']").attr("selected", "selected");
                //关联项
                this.elems.category.find("option[value='1']").attr("selected", "selected");
                //设置标识为elems的都隐藏
                this.hideElems();
                //链接
                this.elems.linkUrl.removeClass("hide").addClass("show");
            },
            init: function () {
                var that = this;
                var type = $.util.getUrlParam("type");
                //从地址栏获得每页显示的数据
                var pageSize = $.util.getUrlParam("psize");
                if (pageSize) {
                    this.pageSize = parseInt(pageSize);
                } else {
                    this.pageSize = 10;
                }
                //表示新增图文消息
                //if (type == "add") {
                //填充所有的数据
                this.fillContent();

                this.initEvent();
                this.events.init();
            },
            //填充选择的系统模块
            fillSysModule: function (menuInfo) {
                var that = this;

                var moduleType = 0;
                var obj = {};
                var pageParamJson = {};
                var config = {};
                //事件选择触发
                if (typeof menuInfo == 'number') {
                    moduleType = menuInfo;
                } else {
                    flag = true;
                    obj = menuInfo;
                    pageParamJson = obj.PageParamJson;
                    pageParamJson = JSON.parse(pageParamJson);
                    if (pageParamJson.length) {
                        config = pageParamJson[0];  //底部关联的内容
                    }
                    unionTypeId = menuInfo.UnionTypeId;
                }
                //表示的是系统功能模块
                that.unionType = 3;
                //该选择框是否已经加载过数据
                var isLoad = that.elems.moduleSelect.attr("data-load");
                //未加载过  加载模块
                if (isLoad == "false") {
                    that.loadData.getSystemModule(function (data) {

                        $.each(data.Data.SysModuleList,function(index,filed){
                            data.Data.SysModuleList[index].ModuleName=filed.ModuleName.trim();

                        })
                        var obj =
                        {
                            itemList:data.Data.SysModuleList
                        }

                        var html = bd.template("moduleTmpl", obj);
                        that.elems.moduleSelect.html(html);

                        var pageModule, pageCode;
                        //让模块选中
                        if (config.pageModule) {
                            pageModule = config.pageModule ? JSON.parse(config.pageModule) : {};
                            pageCode = pageModule.PageCode;
                            that.elems.moduleSelect.find("option[data-value='" + pageModule.PageID + "']").attr("selected", "selected");
                        }

                        var obj =
                            {
                                "EventList": 2,
                                //活动列表
                                "EventDetail": 3,
                                //活动详情
                                "EventLottory": 4,
                                //活动抽奖
                                "Recommend": 5,
                                //推荐有礼
                                "NewsList": 6,
                                //资讯列表
                                "NewsDetail": 7,
                                //资讯详情
                                "GoodsList": 8,
                                //商品列表
                                "ShopList": 9
                                //门店
                            }
                        var svalue = obj[pageCode] || 0;
                        debugger;
                        switch (svalue) {
                            case 0:
                                //默认请选择的时候
                                that.hideElems(that.elems.moduleName);
                                break;
                            case 1:
                                that.hideElems(that.elems.moduleName);
                                break;
                            case 2:
                                //表示的是活动列表

                                that.hideElems(that.elems.moduleName);
                                //获得活动类别
                                that.loadData.getEventType(function (data) {
                                    var obj =
                                        {
                                            itemList: data.Data.EventTypeList
                                        }
                                    //是否显示全部
                                    obj.showAll = false;
                                    var html = bd.template("eventTypeTmpl", obj);
                                    that.elems.eventSelect.html(html);
                                    //转换
                                    var pageType = JSON.parse(config.pageType);
                                    //让活动分类默认选中
                                    that.elems.eventSelect.find("option[data-value='" + pageType.EventTypeId + "']").attr("selected", "selected");

                                    //要选择的类别
                                    that.toSave.moduleType = 2;
                                    //活动列表
                                    //模板
                                    that.toSave.urlTemplate = pageModule.URLTemplate;
                                    that.toSave.domObj = that.elems.eventSelect;
                                });
                                that.elems.events.removeClass("hide").addClass("show");
                                break;
                            case 3:
                                //表示的是选择活动详情页
                                that.hideElems(that.elems.moduleName);
                                //活动详情
                                var pageDetail = JSON.parse(config.pageDetail);
                                //保存模板
                                that.toSave.urlTemplate = pageModule.URLTemplate;
                                that.toSave.moduleType = 3;
                                //要保存的详情dom对象
                                that.toSave.domObj = that.elems.eventDetail;
                                //默认名称显示
                                that.elems.eventDetailSelect.val(pageDetail.EventName);
                                //设置选择后的活动名称
                                that.elems.lottoryWay.find("input").val(pageDetail.EventName).attr("data-value",config.pageDetail);
                                //数据绑定
                                that.elems.eventDetailSelect.attr("data-value", config.pageDetail);
                                that.elems.eventDetail.removeClass("hide").addClass("show");
                                break;
                            case 4:
                                //资讯详情
                                var pageDetail = JSON.parse(config.pageDetail);
                                //保存模板
                                that.toSave.urlTemplate = pageModule.URLTemplate;
                                that.toSave.moduleType = 4;
                                //要保存的详情dom对象
                                that.toSave.domObj = that.elems.lottoryWayInput;
                                //默认名称显示
                                that.elems.lottoryWayInput.val(pageDetail.EventName);
                                //数据绑定
                                that.elems.lottoryWayInput.attr("data-value", config.pageDetail);
                                //活动抽奖
                                that.hideElems(that.elems.moduleName);
                                that.elems.lottoryWay.removeClass("hide").addClass("show");
                                break;
                            case 6:
                                //资讯列表是6
                                that.hideElems(that.elems.moduleName);
                                //获得资讯类别
                                that.loadData.getNewsTypeList(function (data) {
                                    var obj =
                                        {
                                            itemList: data.Data.NewsTypeList
                                        }
                                    //在模板里面设置是否显示全部的这个选项
                                    obj.showAll = false;
                                    var html = bd.template("NewsTypeTmpl", obj);
                                    that.elems.newsTypeSelect.html(html);
                                    //要选择的类别
                                    that.toSave.moduleType = 6;
                                    //转换
                                    var pageType = JSON.parse(config.pageType);
                                    //让资讯列表默认选中
                                    that.elems.newsTypeSelect.find("option[data-value='" + pageType.NewsTypeId + "']").attr("selected", "selected");
                                    //活动列表
                                    //模板
                                    that.toSave.urlTemplate = pageModule.URLTemplate;
                                    that.toSave.domObj = that.elems.newsTypeSelect;
                                    that.elems.newsType.removeClass("hide").addClass("show");
                                });

                                break;
                            case 7:
                                //资讯详情
                                var pageDetail = JSON.parse(config.pageDetail);
                                //资讯详情
                                that.hideElems(that.elems.moduleName);
                                //保存模板
                                that.toSave.urlTemplate = pageModule.URLTemplate;
                                that.toSave.moduleType = 7;
                                //要保存的详情dom对象
                                that.toSave.domObj = that.elems.newsDetailSelect;
                                that.elems.newsDetail.removeClass("hide").addClass("show");
                                //默认名称显示
                                that.elems.newsDetailSelect.val(pageDetail.NewsName);
                                //数据绑定
                                that.elems.newsDetailSelect.attr("data-value", config.pageDetail);
                                that.elems.newsDetailSelect.removeClass("hide").addClass("show");
                                break;
                            case 9:
                                that.hideElems(that.elems.moduleName);
                                that.elems.shopsType.removeClass("hide").addClass("show");
                                break;
                        }
                        //手动触发事件
                        that.elems.moduleSelect.trigger("change");
                    });
                }
                that.elems.moduleName.removeClass("hide").addClass("show");


            },
            //所有的数据请求默认加载数据    
            fillContent: function (applicationId) {

                var that = this;
                //填充微信账号
                this.loadData.getWeiXinAccount(function (data) {
                    debugger;
                    var obj =
                    {
                        itemList: data.Data.AccountList
                    }
                    var html = bd.template("accountTmpl", obj);
                    that.elems.accountSelect.html(html);
                    //把applicationId保存起来
                    if(!applicationId) {
                        that.applicationId = data.Data.AccountList[0].ApplicationId;
                    }else{
                        that.applicationId =applicationId;
                    }
                    //获得微信菜单列表
                    that.loadData.getMenuList(function (data) {
                        debugger;
                        var obj =
                        {
                            itemList: data.Data.MenuList
                        }
                        //obj.itemList = list;
                        var html = bd.template("menuTmpl", obj);
                        that.elems.menuArea.html(html);
                        //让当前的状态进行保存
                        if (that.statusDomobj) {
                            var menuJson = JSON.parse(that.statusDomobj.attr("data-menu"));
                            var menuId = menuJson.MenuId;
                            //显示当前的一级菜单
                            that.elems.menuContent.html(that.menuName ? that.menuName : menuJson.Name);
                            $("#" + menuId).addClass("on").siblings().removeClass("on");
                        } else {
                            //显示当前的一级菜单
                            that.elems.menuContent.html(data.Data.MenuList[0].Name);
                        }
                        that.elems.unionCategory.removeClass("hide").addClass("show");
                        that.elems.linkUrl.removeClass("hide").addClass("show");
                    });
                });

            },
            //显示遮罩层
            showMask: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.chooseEventsDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopUp(type);
                    this.elems.chooseEventsDiv.show();
                }
            },
            //加载更多的资讯或者活动
            loadMoreMaterial: function (currentPage) {
                var that = this;
                //设置当前页面
                page.pageIndex = currentPage - 1;
                this.loadData.getMaterialTextList("", function (data) {
                    var obj = {
                        pageSize: page.pageSize,
                        currentPage: currentPage,
                        allPage: data.Data.TotalPages,
                        showAdd: true,  //表示的一个标识
                        itemList: data.Data.MaterialTextList
                    }
                    var html = bd.template("addImageItemTmpl", obj);
                    that.elems.imageContentItems.html(html);
                });

            },
            //加载图文列表数据
            loadPopMatrialText: function () {
                var that = this;
                //加载图文类别
                this.loadData.getImageContentCategory(function (data) {

                    var obj = {
                        showAll: true,  //显示一个全部的选择项
                        itemList: data.Data.MaterialTextTypeList
                    }
                    var html = bd.template("optionTmpl", obj);
                    $("#imageCategory").html(html);
                });
                //获取图文列表
                this.loadData.getMaterialTextList("", function (data) {
                    var obj = {
                        pageSize: page.pageSize,
                        currentPage: 1,
                        allPage: data.Data.TotalPages,  //总页数
                        showAdd: true,  //表示的一个标识
                        itemList: data.Data.MaterialTextList
                    }
                    var html = bd.template("addImageItemTmpl", obj);
                    that.elems.imageContentItems.html(html);
                    //
                    that.events.initPagination(1, data.Data.TotalPages, function (page) {
                        that.loadMoreMaterial(page);
                    });
                });
            },
            //显示图文搜索的  进行获取
            showMatrialText: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.addImageMessageDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopMatrialText();
                    this.elems.addImageMessageDiv.show();
                }
            },
            stopBubble: function (e) {
                if (e && e.stopPropagation) {
                    //因此它支持W3C的stopPropagation()方法 
                    e.stopPropagation();
                }
                else {
                    //否则，我们需要使用IE的方式来取消事件冒泡 
                    window.event.cancelBubble = true;
                }
                e.preventDefault();
            },
            //所有活动相关
            events:
            {
                eventName: "",
                //活动名称
                eventTypeId: "",
                //活动类别id
                elems:
                {
                    eventsType: $("#pop_eventsType"),
                    //弹层的活动类别
                    eventName: $("#eventName"),
                    //活动名
                    btnSearchEvents: $("#searchEvents"),
                    //搜索活动按钮
                    btnCancel: $("#cancelBtn"),
                    //弹出层取消按钮
                    btnSave: $("#saveBtn")//弹出层保存按钮
                },
                init: function () {
                    //this.initPagination(1, 10);
                    this.initEvent();
                },
                initPagination: function (currentPage, allPage, callback) {
                    $('.pagination').remove();
                    var html = bd.template("pageTmpl", {});
                    page.elems.chooseEventsDiv.append(html);
                    page.elems.addImageMessageDiv.append(html);
                    $('.pagination').jqPagination({
                        link_string: '/?page={page_number}',
                        current_page: currentPage,
                        //设置当前页 默认为1
                        max_page: allPage,
                        //设置最大页 默认为1
                        page_string: '当前第{current_page}页,共{max_page}页',
                        paged: function (page) {
                            //回发事件。。。
                            if (callback) {
                                callback(page);
                            }
                        }
                    });
                },
                //搜索活动
                search: function () {
                    $.ajax({
                        url: page.url + "?method=DeleteItemCategoryArea",
                        type: "post",
                        data: {},
                        success: function (data) {
                            if (data.success) {
                                if (callback) {
                                    callback(data);
                                }
                            }
                            else {
                                alert(data.Message);
                            }
                        }
                    });
                },
                //加载更多的资讯或者活动
                loadMoreData: function (currentPage) {
                    var that = this;
                    //设置当前页面
                    page.currentPage = currentPage - 1;
                    //根据类型来判断是取的什么接口数据
                    switch (page.popupOptions.type) {
                        //chooseNews  获取资讯列表  chooseEvents获取活动抽奖列表                                                                                                                                                                                                                                                


                        case "chooseNews":
                            page.popupOptions.type = "chooseNews";
                            page.loadData.getNewsList(function (data) {
                                //资讯列表
                                page.popupOptions.itemList = data.Data.NewsList ? data.Data.NewsList : [];
                                var items = bd.template("itemTmpl", page.popupOptions);
                                $("#itemsTable").html(items);
                            });
                            break;
                        case "chooseEvents":
                            page.popupOptions.type = "chooseEvents";
                            page.loadData.getEventList(function (data) {
                                page.popupOptions.itemList = data.Data.EventList ? data.Data.EventList : [];
                                var items = bd.template("itemTmpl", page.popupOptions);
                                $("#itemsTable").html(items);
                            });
                            break;
                    }
                },
                //事件
                initEvent: function () {
                    var that = this;

                    //鼠标移动上去的效果
                    page.elems.chooseEventsDiv.delegate("tr", "mouseover", function (e) {
                        $(this).addClass("on");
                    }).delegate("tr", "mouseout", function () {
                        var $this = $(this);
                        if ($this.attr("choosed") != "true") {
                            $(this).removeClass("on");
                        }
                    });
                    //选中radio事件
                    page.elems.chooseEventsDiv.delegate("tr", "click", function () {
                        var $this = $(this);
                        $this.find("input[type=radio]").attr("checked", "checked");
                        //全部置为否
                        $this.siblings().removeClass("on").attr("choosed", "false");
                        //标记选中的
                        $this.addClass("on").attr("choosed", "true");
                    });
                    //开始搜索
                    page.elems.chooseEventsDiv.delegate("#searchEvents", "click", function (e) {
                        page.events.eventTypeId = "";
                        page.events.eventName = "";
                        page.currentPage = 0;
                        var current = page.events.elems;
                        var eventName = current.eventName.val();
                        //填充活动名
                        page.events.eventName = current.eventName.val();
                        var eventType = current.eventsType.val();
                        //json对象
                        var jsonType = null;
                        if (eventType) {
                            jsonType = JSON.parse(eventType);
                            if (page.popupOptions.type == "chooseNews") {
                                //设置newsTypeId
                                page.events.eventTypeId = jsonType.NewsTypeId;
                            }
                            else if (page.popupOptions.type == "chooseEvents") {
                                //设置EventsTypeId
                                page.events.eventTypeId = jsonType.EventTypeId;
                            }

                        }
                        //根据类型来判断是取的什么接口数据
                        switch (page.popupOptions.type) {
                            //chooseNews  获取资讯列表  chooseEvents获取活动抽奖列表                                                                                                                                                                                                                                                


                            case "chooseNews":
                                page.popupOptions.type = "chooseNews";
                                page.loadData.getNewsList(function (data) {
                                    //资讯列表
                                    page.popupOptions.itemList = data.Data.NewsList ? data.Data.NewsList : [];
                                    var items = bd.template("itemTmpl", page.popupOptions);
                                    $("#itemsTable").html(items);
                                    //获得总共页数
                                    that.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                        that.loadMoreData(currentPage);
                                        //加载更多
                                    });
                                });
                                break;
                            case "chooseEvents":
                                page.popupOptions.type = "chooseEvents";
                                page.loadData.getEventList(function (data) {
                                    page.popupOptions.itemList = data.Data.EventList ? data.Data.EventList : [];
                                    var items = bd.template("itemTmpl", page.popupOptions);
                                    $("#itemsTable").html(items);
                                    //获得总共页数
                                    that.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                        that.loadMoreData(currentPage);
                                        //加载更多
                                    });
                                });
                                break;

                        }

                        page.stopBubble(e);
                        return false;
                    });
                    //取消遮罩层
                    page.elems.chooseEventsDiv.delegate("#cancelBtn", "click", function (e) {
                        page.stopBubble(e);
                        page.showMask(false);

                        return false;
                    });
                    //保存数据
                    page.elems.chooseEventsDiv.delegate("#saveBtn", "click", function (e) {

                        //判断是否有选中
                        var jsonStr = $('#itemsTable input:radio[name="item"]:checked');
                        if (!jsonStr.length) {
                            alert("请选择一个" + page.popupOptions.tipsName + "!");
                            return;
                        }
                        jsonStr = jsonStr.val();
                        var jsonObj = JSON.parse(jsonStr);
                        //将内容保存
                        page.toSave.detailDomObj.attr("data-value", jsonStr);
                        if (page.popupOptions.type == "chooseNews") {
                            //让输入框的内容显示出选择的内容
                            page.toSave.detailDomObj.val(jsonObj.NewsName);
                        }
                        else if (page.popupOptions.type == "chooseEvents") {
                            //选择活动
                            page.toSave.detailDomObj.val(jsonObj.EventName);
                        }
                        page.showMask(false);
                        page.stopBubble(e);
                        return false;
                    });
                }
            },
            //根据类型弹出要展示的层内容
            loadPopUp: function (type) {
                var that = this;
                switch (type) {
                    case 1:
                        break;
                    case 2:
                        //活动抽奖、
                        this.popupOptions.popupName = "活动名称";
                        this.popupOptions.popupSelectName = "活动类别";
                        this.popupOptions.tipsName = "活动";
                        this.popupOptions.title = ["活动名", "活动类别", "活动状态", "抽奖方式"];
                        this.popupOptions.topTitle = "选择活动";
                        var html = bd.template("popDivTmpl", this.popupOptions);
                        this.elems.chooseEventsDiv.html(html);
                        //
                        this.popupOptions.type = "chooseEvents";
                        //获得活动类别
                        that.loadData.getEventType(function (data) {
                            var obj =
                    {
                        itemList: data.Data.EventTypeList
                    }
                            //用来在模板里面判断是否显示全部这一个选项
                            obj.showAll = true;
                            var itemsDom = bd.template("eventTypeTmpl", obj);
                            that.events.elems.eventsType.html(itemsDom);
                        });
                        //显示
                        //获取抽奖活动
                        this.loadData.getEventList(function (data) {
                            //资讯列表
                            that.popupOptions.itemList = data.Data.EventList;
                            var items = bd.template("itemTmpl", that.popupOptions);
                            $("#itemsTable").html(items);
                            that.currentPage = 0;
                            //设置默认页面为0
                            //获得总共页数
                            that.events.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                that.events.loadMoreData(currentPage);
                            });
                        });

                        break;
                    case 3:
                        //资讯详情    弹出资讯类别搜索
                        this.popupOptions.popupName = "资讯名称";
                        this.popupOptions.popupSelectName = "资讯分类";
                        this.popupOptions.tipsName = "资讯";
                        this.popupOptions.title = ["资讯名称", "资讯类别", "发布时间"];
                        this.popupOptions.topTitle = "选择资讯";
                        var html = bd.template("popDivTmpl", this.popupOptions);
                        this.elems.chooseEventsDiv.html(html);
                        //选择资讯的标识
                        this.popupOptions.type = "chooseNews";
                        //设置默认页面为0
                        that.currentPage = 0;
                        //获取资讯明细
                        this.loadData.getNewsList(function (data) {
                            //资讯列表
                            that.popupOptions.itemList = data.Data.NewsList;
                            var items = bd.template("itemTmpl", that.popupOptions);
                            $("#itemsTable").html(items);

                            //获得总共页数
                            that.events.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                that.events.loadMoreData(currentPage);
                            });
                        });
                        //获得资讯类别
                        this.loadData.getNewsTypeList(function (data) {
                            var obj =
                    {
                        itemList: data.Data.NewsTypeList
                    }
                            //用来在模板里面判断是否显示全部这一个选项
                            obj.showAll = true;
                            var itemsDom = bd.template("NewsTypeTmpl", obj);
                            that.events.elems.eventsType.html(itemsDom);
                        });
                        break;
                    case 4:
                        //资讯详情    弹出搜索资讯详情的




                        break;

                }
                this.events.elems =
        {
            eventsType: $("#pop_eventsType"),
            //弹层的活动类别
            eventName: $("#eventName"),
            //活动名
            btnSearchEvents: $("#searchEvents"),
            //搜索活动按钮
            btnCancel: $("#cancelBtn"),
            //弹出层取消按钮
            btnSave: $("#saveBtn")//弹出层保存按钮
        };
            },
            //根据类型让关联项的内容动态切换
            showElements: function (menuInfo) {
                debugger;
                var unionTypeId = 0, flag = false;
                //事件选择触发
                if (typeof menuInfo == 'number') {
                    unionTypeId = menuInfo;
                } else {
                    flag = true;
                    unionTypeId = menuInfo.UnionTypeId;
                }

                var that = this;
                switch (unionTypeId) {
                    case 0:
                        //默认请选择的时候
                        that.hideElems();
                        break;
                    case 1:
                        //表示的是链接
                        that.unionType = 1;
                        that.hideElems();
                        if (flag) {
                            //设置标识为elems的都隐藏
                            this.elems.urlAddress.val(menuInfo.MenuUrl);
                        } else {
                            this.elems.urlAddress.val("");
                        }
                        //链接
                        that.elems.linkUrl.removeClass("hide").addClass("show");
                        break;
                    case 2:
                        //表示的是回复消息
                        that.hideElems();
                        that.unionType = 2;
                        if (flag) {

                        } else {
                            //默认让文本选择项选中
                            that.elems.messageSelect.find("option[value='1']").attr("selected", "selected");
                        }
                        //控制文本类型默认显示
                        that.elems.contentEditor.removeClass("hide").addClass("show");
                        that.elems.message.removeClass("hide").addClass("show");


                        break;
                    case 3:
                        //表示的是系统功能模块
                        that.hideElems();
                        that.unionType = 3;
                        //该选择框是否已经加载过数据
                        var isLoad = that.elems.moduleSelect.attr("data-load");
                        that.fillSysModule(menuInfo);
                        that.elems.moduleName.removeClass("hide").addClass("show");
                        break;

                }
            },
            //根据消息类型   进行动态弹出
            showElementsByMessageType: function (menuInfo) {
                var that = this;
                var messageType = 0, flag = false;
                //事件选择触发
                if (typeof menuInfo == 'number') {
                    messageType = menuInfo;
                } else {
                    flag = true;
                    messageType = menuInfo.MessageType;
                }
                switch (messageType - 0) {
                    case 0:
                        break;
                    case 1:
                        //表示的文本消息类型
                        that.messageType = 1;
                        that.hideElems();
                        if (flag) {
                            //设置文本
                            $("#text").val(menuInfo.Text);
                        } else {
                            $("#text").val("");
                        }
                        //控制文本类型默认显示
                        that.elems.contentEditor.removeClass("hide").addClass("show");
                        break;
                    case 2:
                        //表示的是图片消息类型
                        that.hideElems();
                        that.messageType = 2;
                        if (flag) {
                            //设置图片
                            that.generateUrl = menuInfo.ImageUrl;
                            that.elems.imageView.html("<img src=" + that.generateUrl + ">");
                        }
                        //图片层显示
                        that.elems.toUploadImage.removeClass("hide").addClass("show");
                        break;
                    case 3:
                        //表示的是图文消息类型
                        that.hideElems();
                        that.messageType = 3;
                        //去获得图文列表
                        var obj = {
                            pageSize: page.pageSize,
                            currentPage: 1,
                            allPage: 1,
                            showAdd: false,  //此标识为true为本地dom否则为从服务器获取的数据
                            itemList: menuInfo.MaterialTextIds
                        }
                        var html = bd.template("addImageItemTmpl", obj);
                        that.elems.imageContentMessage.find(".list").html(html);
                        //表示已经选择的图文数量
                        $("#hasChoosed").html(obj.itemList ? obj.itemList.length : 0);
                        that.elems.imageContentMessage.removeClass("hide").addClass("show");
                        break;

                }
                if ((messageType - 0) > 0) {
                    that.elems.messageSelect.find("option[value='" + (menuInfo.MessageType - 0) + "']").attr("selected", "selected");
                    that.elems.message.removeClass("hide").addClass("show");
                }
            },
            //填充菜单详情
            fillMenuDetail: function (menuInfo) {
                //设置菜单为空
                this.elems.menuTitle.val(menuInfo.Name);
                //序号
                this.elems.menuNo.val(menuInfo.DisplayColumn);
                //是否启用
                this.elems.isUse.find("option[value='" + menuInfo.Status + "']").attr("selected", "selected");
                //表示点击的是一级菜单
                if (menuInfo.isClick) {
                    this.elems.category.parent().parent().removeClass("show").addClass("hide");
                    this.elems.linkUrl.removeClass("show").addClass("hide");
                    this.hideElems();
                } else {
                    this.elems.category.parent().parent().removeClass("hide").addClass("show");
                    //关联项
                    if (menuInfo.UnionTypeId == 0) { //默认赋值为1
                        menuInfo.UnionTypeId = 1;
                    }
                    this.elems.category.find("option[value='" + menuInfo.UnionTypeId + "']").attr("selected", "selected");
                    //根据关联展示内容
                    this.showElements(menuInfo);
                    //展示对应的消息类型
                    this.showElementsByMessageType(menuInfo);
                }

            },
            initEvent: function () {
                //初始化事件集
                var that = this;
                //拖拽排序
                that.elems.imageContentDiv.find(".list").sortable({ opacity: 0.7, cursor: 'move', update: function () { }
                });
                that.elems.weixinAccount.delegate(".selectBox","change",function(e){

                    var me=$(this);
                    if(me.val()) {
                        that.applicationId = me.val();

                        that.loadData.getMenuList(function (data) {
                            debugger;
                            var obj =
                            {
                                itemList: data.Data.MenuList
                            }
                            //obj.itemList = list;
                            var html = bd.template("menuTmpl", obj);
                            that.elems.menuArea.html(html);
                            //让当前的状态进行保存
                            if (that.statusDomobj) {
                                var menuJson = JSON.parse(that.statusDomobj.attr("data-menu"));
                                var menuId = menuJson.MenuId;
                                //显示当前的一级菜单
                                that.elems.menuContent.html(that.menuName ? that.menuName : menuJson.Name);
                                $("#" + menuId).addClass("on").siblings().removeClass("on");
                            } else {
                                //显示当前的一级菜单
                                that.elems.menuContent.html(data.Data.MenuList[0].Name);
                            }
                            that.elems.unionCategory.removeClass("hide").addClass("show");
                            that.elems.linkUrl.removeClass("hide").addClass("show");
                        });
                    }
                });
                //发布菜单
                this.elems.publishMenu.click(function () {
                    if (that.applicationId) {
                        that.loadData.publishMenu(function (data) {
                            alert("发布成功!!")
                        });
                    } else {
                        alert("没有ApplicationId");
                    }
                });
                //鼠标悬停的时候把内容展示出来
                this.elems.menuArea.delegate("span", "mouseover", function (e) {
                    var $this = $(this);
                    $this.addClass("on").find(".subMenuWrap").show();
                    $this.siblings().removeClass("on").find(".subMenuWrap").hide();
                    that.stopBubble(e);

                });
                //一级和二级菜单点击
                function clickEvent($this) {
                    var dataMenu = $this.attr("data-menu");
                    var dataMenuJson = JSON.parse(dataMenu);
                    var menuContent = "";
                    if (dataMenuJson.Level == 1) {
                        that.elems.delBtn.hide();
                        //填充菜单名称
                        menuContent = dataMenuJson.Name;
                    } else if (dataMenuJson.Level == 2) {
                        that.elems.delBtn.show();
                        var fatherDom = $this.parent().parent();
                        var dmenu = JSON.parse(fatherDom.attr("data-menu"));
                        menuContent = dmenu.Name + "--" + dataMenuJson.Name;
                    }
                    that.elems.menuContent.html(menuContent);
                    //菜单ID
                    var menuId = dataMenuJson.MenuId;
                    //获得详情并填充
                    that.loadData.getMenuDetail(menuId, function (data) {
                        var menuInfo = data.Data.MenuList[0];
                        if (dataMenuJson.Level == 1) {
                            //则表示该菜单为click类型
                            if (dataMenuJson.SubMenus && dataMenuJson.SubMenus.length > 0) {
                                menuInfo.isClick = true;
                            }
                        }
                        //填充菜单详情
                        that.fillMenuDetail(menuInfo);
                    });
                    //根据菜单ID获取数据填充
                    that.elems.saveData.attr("data-menu", dataMenu);
                    //用来删除
                    that.elems.delBtn.attr("data-menu", dataMenu);

                };
                //一级菜单点击事件
                this.elems.menuArea.delegate("span", "click", function (e) {
                    that.saveType = "edit";
                    var $this = $(this);
                    that.level = 1;
                    //以便保存或者删除后能够正常控制其状态
                    that.statusDomobj = $this;
                    clickEvent($this);
                    that.stopBubble(e);

                });
                //二级菜单点击事件
                this.elems.menuArea.delegate(".tempSubMenu", "click", function (e) {
                    that.saveType = "edit";
                    that.level = 2;
                    var $this = $(this);
                    //以便保存或者删除后能够正常控制其状态
                    that.statusDomobj = $this.parent().parent();
                    clickEvent($this);
                    that.stopBubble(e);

                });
                //点击添加菜单事件
                this.elems.menuArea.delegate(".addBtn", "click", function (e) {

                    var $this = $(this);
                    //获得焦点
                    that.elems.menuTitle.focus();
                    page.level = 2; //只要是新增level为2
                    page.saveType = "add";  //表示的要进行保存
                    //以便保存或者删除后能够正常控制其状态
                    that.statusDomobj = $this.parent().parent();
                    //二级菜单的父节ID
                    page.parentId = $this.attr("data-parentId");
                    that.elems.unionCategory.removeClass("hide").addClass("show");
                    //设置值
                    that.elems.saveData.attr("data-menu", $this.parent().attr("data-menu"));
                    that.elems.menuContent.html(JSON.parse(that.statusDomobj.attr("data-menu")).Name + "--添加子菜单");
                    //数据复原
                    that.clearInput();
                    that.stopBubble(e);
                });
                //点击获取图文的内容
                this.elems.imageContentDiv.delegate(".addBtn", "click", function (e) {
                    page.MaterialTypeId = "";
                    var $this = $(this);
                    that.showMatrialText(true);
                    that.stopBubble(e);
                });
                //保存图文事件
                this.elems.addImageMessageDiv.delegate(".saveBtn", "click", function (e) {
                    that.showMatrialText(false);
                    that.stopBubble(e);
                });
                //取消图文事件
                this.elems.addImageMessageDiv.delegate(".cancelBtn", "click", function (e) {
                    //再取消的时候把所有的删除
                    that.elems.imageContentDiv.find("[data-flag='add']").remove();
                    $("#hasChoosed").html(0);
                    that.showMatrialText(false);
                    that.stopBubble(e);
                });

                //查询图文事件
                this.elems.addImageMessageDiv.delegate(".queryBtn", "click", function (e) {
                    var eventName = $("#theTitle").val();
                    var eventType = that.elems.imageCategory.val();
                    page.MaterialTextName = eventName;  //图文名称
                    page.MaterialTypeId = eventType;    //图文typeId
                    page.pageIndex = 0;  //只要查询就从头查询
                    that.loadData.getMaterialTextList("", function (data) {
                        var obj = {
                            pageSize: page.pageSize,
                            currentPage: 1,
                            allPage: data.Data.TotalPages,
                            showAdd: true,  //表示的一个标识
                            itemList: data.Data.MaterialTextList
                        }
                        var html = bd.template("addImageItemTmpl", obj);
                        that.elems.imageContentItems.html(html);
                        that.events.initPagination(1, data.Data.TotalPages, function (page) {
                            that.loadMoreMaterial(page);
                        });
                    });
                    that.stopBubble(e);
                });
                //选择一个项则让他选中  同时在页面中展示出来
                this.elems.addImageMessageDiv.delegate(".item", "click", function (e) {
                    var $this = $(this);
                    var addId = $this.attr("data-id");
                    //已经有的图文数量
                    var hasLength = that.elems.imageContentDiv.find(".item").length;
                    if ($this.attr("isSelected") == "true") {  //表示已经选中则进行删除
                        $this.removeClass("on").attr("isSelected", "false");
                        $("#" + addId).remove();
                        hasLength = hasLength - 1;
                    } else {
                        if (hasLength >= 10) {
                            alert("图文素材最多选择10个!\r\n不能继续添加!");
                            return false;
                        }
                        $this.addClass("on").attr("isSelected", "true");
                        var clone = $this.clone();
                        var domObj = that.elems.imageContentDiv.find("[data-id=" + addId + "]");
                        if (domObj.length) {
                            domObj.remove();
                            hasLength = hasLength - 1;
                        }
                        //给克隆后的节点设置id
                        clone.attr("id", $this.attr("data-id"));
                        //将选中的内容添加到图文层
                        that.elems.imageContentDiv.find(".list").append(clone);
                        hasLength = hasLength + 1;

                    }
                    //表示已经选择的图文数量
                    $("#hasChoosed").html(hasLength);

                    that.stopBubble(e);
                });
                //已经选择的图文列表鼠标移动上去出现删除的按钮
                this.elems.imageContentDiv.find(".list").delegate(".item", "mouseover", function (e) {
                    var $this = $(this);
                    $this.addClass("hover");
                    that.stopBubble(e);

                }).delegate(".item", "mouseout", function (e) {
                    var $this = $(this);
                    $this.removeClass("hover");
                    that.stopBubble(e);
                });
                //删除图文消息    一种是删除dom 一种是删除数据库里面的
                this.elems.imageContentDiv.find(".list").delegate(".delBtn", "click", function (e) {
                    var $this = $(this);
                    //是否是已经存储在数据库的
                    var itemDom = $this.parent().parent();
                    itemDom.remove();
                    var length = (that.elems.imageContentDiv.find(".item").length);
                    //表示已经选择的图文数量
                    $("#hasChoosed").html(length);
                    that.stopBubble(e);
                })


                //删除菜单事件
                this.elems.delBtn.bind("click", function (e) {
                    var $this = $(this);

                    //获得menuId
                    var dataMenu = $this.attr("data-menu");
                    if (!dataMenu) {
                        alert("请先选择一个菜单再删除!");
                        return false;
                    }
                    var dataMenuJson = JSON.parse(dataMenu);
                    if (dataMenuJson && dataMenuJson.Level == 1) {
                        alert("一级菜单不能删除!");
                        return false;
                    }
                    var menuId = dataMenuJson.MenuId;
                    //根据二级菜单获得一级菜单
                    page.parentId = dataMenuJson.ParentId;
                    //删除完成之后重新load数据
                    that.loadData.delMenu(menuId, function () {
                        that.saveType = "add";  //删除完设置为add保存
                        //用来删除
                        that.elems.delBtn.attr("data-menu", "");
                        //that.fillContent();
                        that.elems.weixinList.trigger("change");
                        that.clearInput();
                        alert("菜单<" + dataMenuJson.Name + ">删除成功!");

                    });
                    that.stopBubble(e);
                });


                //上传图片
                that.uploadImage(that.elems.btnUpload[0], function (btn, data) {
                    //保存上传后的图片地址
                    that.generateUrl = data.thumUrl;
                    that.elems.imageView.html("<img src='" + that.generateUrl + "'>");
                });
                //关联到类别选择
                this.elems.category.bind("change", function (e) {
                    var $this = $(this);
                    var value = $this.val() - 0;
                    //根据选择展示内容
                    that.showElements(value);
                    that.stopBubble(e);
                });
                //根据消息类别
                this.elems.messageSelect.bind("change", function (e) {
                    var $this = $(this);
                    var value = $this.val() - 0;
                    that.showElementsByMessageType(value);
                    that.stopBubble(e);
                });

                //活动类别点击事件
                this.elems.eventSelect.bind("click", function () { });
                //保存数据
                this.elems.saveData.bind("click", function (e) {
                    var $this = $(this);
                    //验证下面的子菜单是否超过10个
                    var dataMenu = $this.attr("data-menu");
                    if (dataMenu) {
                        dataMenu = JSON.parse(dataMenu);
                        //父节点菜单
                        var fatherMenu = $("#" + dataMenu.MenuId);
                        //保存的是一级菜单  不进行子菜单数量验证
                        if (that.saveType == "add") {
                            //查找父节点下面的子菜单数量    里面有个添加的a 所以多1个
                            if (fatherMenu.find(".subMenuWrap>a").length >= (that.subMenuCount + 1)) {
                                alert("一级菜单<" + dataMenu.Name + ">最多只能有" + that.subMenuCount + "个子级菜单");
                                return false;
                            }
                        }
                    }
                    that.saveData($this);
                    that.stopBubble(e);
                });
                //保存菜单
                this.elems.saveBtn.bind("click", function (e) {
                    that.saveData();
                    that.stopBubble(e);
                });
                //活动模块选择
                //此处为最复杂的级联逻辑
                this.elems.moduleSelect.bind("change", function (e) {
                    debugger;
                    var $this = $(this);
                    var obj =
            {
                "EventList": 2,
                //活动列表
                "EventDetail": 3,
                //活动详情
                "EventLottory": 4,
                //活动抽奖
                "Recommend": 5,
                //推荐有礼
                "NewsList": 6,
                //资讯列表
                "NewsDetail": 7,
                //资讯详情
                "GoodsList": 8,
                //商品列表
                "ShopList": 9
                //门店
            }
                    var value = $this.val();
                    value = JSON.parse(value);
                    var pcode = value.PageCode;
                    var svalue = obj[pcode] || 0;
                    //根据选择的模块名称 判断urlTemplate是否需要构建PageParamJson   
                    // 类似于 /HtmlApps/html/_pageName_?eventType={eventType}
                    //则把eventType={eventType}  eventType取出来
                    if (value.URLTemplate) {
                        //获得所有的参数
                        var tmpArr = value.URLTemplate.match(/\{[\s\S]+?\}/g);
                        this.pageParamJson = [];
                        tmpArr = tmpArr ? tmpArr : [];
                        for (var i = 0; i < tmpArr.length; i++) {
                            var obj = {};
                            obj.key = tmpArr[i].replace("{", "").replace("}", "");
                            this.pageParamJson.push(obj);
                        }
                    }


                    switch (svalue) {
                        case 0:
                            //默认请选择的时候
                            that.hideElems(that.elems.moduleName);
                            //要选择的类别
                            that.toSave.moduleType = 0;
                            //模板
                            that.toSave.urlTemplate = value.URLTemplate;
                            that.toSave.domObj = that.elems.moduleSelect;
                            break;
                        case 1:
                            that.toSave.moduleType = 1;
                            //表示的是微商城
                            that.hideElems(that.elems.moduleName);
                            break;
                        case 2:
                            //表示的是活动列表

                            that.hideElems(that.elems.moduleName);
                            //获得活动类别
                            that.loadData.getEventType(function (data) {
                                var obj =
                        {
                            itemList: data.Data.EventTypeList
                        }
                                //是否显示全部
                                obj.showAll = false;
                                var html = bd.template("eventTypeTmpl", obj);
                                that.elems.eventSelect.html(html);
                                //要选择的类别
                                that.toSave.moduleType = 2;
                                //活动列表
                                //模板
                                that.toSave.urlTemplate = value.URLTemplate;
                                that.toSave.domObj = that.elems.eventSelect;
                            });
                            that.elems.events.removeClass("hide").addClass("show");
                            break;
                        case 3:
                        case 4:
                            that.toSave.moduleType = 4;
                            //保存模板
                            that.toSave.urlTemplate = value.URLTemplate;
                            //要保存的详情dom对象
                            that.toSave.domObj = that.elems.lottoryWayInput;
                            //活动抽奖
                            that.hideElems(that.elems.moduleName);
                            that.elems.lottoryWay.removeClass("hide").addClass("show");
                            break;
                        case 6:
                            //资讯列表是6
                            that.hideElems(that.elems.moduleName);
                            //获得资讯类别
                            that.loadData.getNewsTypeList(function (data) {
                                var obj =
                        {
                            itemList: data.Data.NewsTypeList
                        }
                                //在模板里面设置是否显示全部的这个选项
                                obj.showAll = false;
                                var html = bd.template("NewsTypeTmpl", obj);
                                that.elems.newsTypeSelect.html(html);
                                //要选择的类别
                                that.toSave.moduleType = 6;
                                //活动列表
                                //模板
                                that.toSave.urlTemplate = value.URLTemplate;
                                that.toSave.domObj = that.elems.newsTypeSelect;
                            });
                            that.elems.newsType.removeClass("hide").addClass("show");
                            break;
                        case 7:
                            //资讯详情
                            that.hideElems(that.elems.moduleName);
                            //保存模板
                            that.toSave.urlTemplate = value.URLTemplate;
                            that.toSave.moduleType = 7;
                            //要保存的详情dom对象
                            that.toSave.domObj = that.elems.newsDetailSelect;
                            that.elems.newsDetail.removeClass("hide").addClass("show");
                            break;
                        case 9:
                            that.hideElems(that.elems.moduleName);
                            that.elems.shopsType.removeClass("hide").addClass("show");
                            break;
                    }
                    that.stopBubble(e);
                });
                //选择活动详情的时候事件
                this.elems.eventDetailSelect.bind("click", function (e) {
                    that.showMask(true, 2);
                    //将点击的输入框保存起来
                    that.toSave.detailDomObj = $(this);
                    that.stopBubble(e);
                    //显示
                });
                //选择资讯详情的时候事件
                this.elems.newsDetailSelect.bind("click", function (e) {
                    //将点击的输入框保存起来
                    that.toSave.detailDomObj = $(this);
                    that.showMask(true, 3);
                    that.stopBubble(e);
                    //显示
                });
                //选择活动抽奖的时候弹出层   然后没有抽奖详情
                this.elems.lottoryWayInput.bind("click", function (e) {
                    //将点击的输入框保存起来
                    that.toSave.detailDomObj = $(this);
                    that.showMask(true, 2);
                    that.stopBubble(e);

                });
                //uimask 隐藏
                //this.elems.uiMask.bind("click", function () {
                //    that.showMask(false);
                //});
            },
            //隐藏元素
            hideElems: function (jqDom) {
                $('[name="elems"]').removeClass("show").addClass("hide");
                if (!!jqDom) {
                    jqDom.removeClass("hide").addClass("show");
                }
            },
            //保存数据
            saveData: function ($this) {
                var menuId = null, //菜单ID 标识
                title = null, //菜单名称
                imageUrl = null, //图片地址
                menuUrl = null, //菜单链接地址
                text = null, //文本内容
                displayColumn = null, //排序
                applicationId = null, //申请接口主标识
                level = null, //几级菜单
                typeId = null, //图文类别
                pageId = null, //模块id
                parentId = null, // 父节点
                moduleName = null, //模块名称
                status = null,  //是否启用
                materialTextIds = null, //图文消息列表数组
                moduleName = null,
                pageParamJson = {};
                var dataMenu = $this.attr("data-menu");
                //菜单名称
                title = this.elems.menuTitle.val();
                //状态
                status = this.elems.isUse.val();
                //是否编辑的是一级菜单
                var isEditLevelOne = false;
                //验证下面的子菜单是否超过10个
                if (dataMenu) {
                    dataMenu = JSON.parse(dataMenu);
                }
                if (dataMenu.Level == 1 && this.saveType != "add") { //一级菜单
                    //父节点菜单
                    var fatherMenu = $("#" + dataMenu.MenuId);
                    //查找父节点下面的子菜单数量    里面有个添加的a 所以多1个
                    if (fatherMenu.find(".subMenuWrap>a").length > 1) {
                        isEditLevelOne = true;
                    }
                }
                var objson = {};
                if (title.length == 0) {
                    alert("请填写标题");
                    return false;
                }
                displayColumn = this.elems.menuNo.val();
                if (displayColumn == "") {
                    alert("菜单序号不能为空!");
                    return false;
                }
                if (parseInt(displayColumn) >= 0) {
                    displayColumn = parseInt(displayColumn);
                } else {
                    alert("序号请输入数字!");
                    return false;
                }
                //验证菜单名称  一级菜单只能为4个
                if (page.level == 1) {
                    if (title.replace(/[^x00-xFF]/g, '**').length > 12) {
                        alert("一级菜单只能最多6个字!");
                        return false;
                    }
                }
                if (page.level == 2) {
                    if (title.replace(/[^x00-xFF]/g, '**').length > 16) {
                        alert("二级菜单只能最多8个字!");
                        return false;
                    }
                }
                var that = this;

                if (this.saveType != "add") {  //编辑的时候才进行判断
                    if (!dataMenu) {
                        alert("请先选择一个菜单再操作!");
                        return false;
                    }
                    if (!dataMenu) {
                        alert("请选择一个菜单修改后保存，或者新增菜单保存!");
                        return false;
                    }
                    objson = dataMenu;
                    //设置等级
                    page.level = objson.Level;
                    page.MenuId = objson.MenuId;
                    page.parentId = objson.ParentId || "";
                } else {  //添加菜单
                    page.MenuId = "";
                    page.level = 2;   //默认添加的时候level是2级菜单
                    //默认添加到第一个一级菜单下面
                    if (!page.parentId) {
                        var firstMenuObj = $(this.elems.menuArea.find("span")[0]).attr("data-menu");
                        var firstMenuId = JSON.parse(firstMenuObj).MenuId;
                        page.parentId = firstMenuId;
                    }

                }
                if (!isEditLevelOne) {
                    //页面替换参数JSON
                    var that = this;
                    //接口标识
                    applicationId = this.elems.accountSelect.val();
                    //图文类别id
                    typeId = this.elems.imageCategorySelect.val();
                    //图片地址
                    imageUrl = this.generateUrl;
                    this.unionType = this.elems.category.val();
                    //表示链接的时候进行验证
                    if (this.unionType == 1) {
                        var linkUrl = $("#urlAddress").val();
                        if (linkUrl.length == 0) {
                            alert("请输入链接地址");
                            return false;
                        }
                        if (linkUrl.indexOf("http") != 0) {
                            alert("请输入正确格式的链接地址 如http://***");
                            return false;
                        }
                        menuUrl = linkUrl;
                    }
                    var messageType = 0;
                    //标识的是回复消息
                    if (this.unionType == 2) {
                        messageType = this.elems.messageSelect.val();
                        messageType = messageType - 0;
                        //文本内容
                        text = $("#text").val();
                        switch (messageType) {
                            case 1:
                                if (text == "") {
                                    alert("文本内容不能为空!");
                                    return false;
                                }
                                if (text.length > 2048) {
                                    alert("文本的最大长度为2048");
                                    return false;
                                }
                                break;
                            case 2:
                                //图片
                                if (imageUrl == "") {
                                    alert("请上传封面图片");
                                    return false;
                                }
                                break;
                            case 3:
                                //图文消息
                                var maxtrailDom = that.elems.imageContentDiv.find(".item");
                                if (maxtrailDom.length <= 0) {
                                    alert("关键字关联的图文至少要添加一个图文!");
                                    return false;
                                }
                                materialTextIds = [];
                                maxtrailDom.each(function (i, k) {
                                    var obj = $(k).attr("data-obj");
                                    obj = JSON.parse(obj);
                                    materialTextIds.push({
                                        TestId: obj.TestId,
                                        DisplayIndex: (i + 1)
                                    });
                                });
                                break;

                        }


                    }
                    //系统模块
                    if (this.unionType == 3) {
                        //模块选择
                        var value = this.elems.moduleSelect.val();
                        value = JSON.parse(value);
                        pageId = value.PageID;
                        moduleName = value.ModuleName;
                        //根据模块进行获得下面联动的内容
                        switch (this.toSave.moduleType) {
                            case 0:
                                //系统默认的普通模块
                                //活动列表的模块
                                var domVal = this.toSave.domObj.val();
                                if (domVal != "") {
                                    //活动分类
                                    var pageTypeJson = JSON.parse(domVal);
                                    this.pageParamJson = [];
                                    var obj = {};
                                    //系统模块
                                    obj.pageModule = JSON.stringify(value);
                                    //具体关联
                                    obj.pageType = JSON.stringify(pageTypeJson);
                                    this.pageParamJson.push(obj);
                                }
                                break;
                            case 2:
                                //活动列表的模块
                                var domVal = this.toSave.domObj.val();
                                if (domVal != "") {
                                    //活动分类
                                    var eventTypeJson = JSON.parse(domVal);
                                    //获得所有的参数
                                    var tmpArr = this.toSave.urlTemplate.match(/\{[\s\S]+?\}/g);
                                    this.pageParamJson = [];
                                    var obj = {};
                                    obj.key = tmpArr[0].replace("{", "").replace("}", "");
                                    obj.value = eventTypeJson.EventTypeId;
                                    //系统模块
                                    obj.pageModule = JSON.stringify(value);
                                    //具体关联
                                    obj.pageType = JSON.stringify(eventTypeJson);
                                    this.pageParamJson.push(obj);
                                }
                                break;
                            case 6:
                                //资讯列表
                                var domVal = this.toSave.domObj.val();
                                if (domVal != "") {
                                    //资讯分类
                                    var eventTypeJson = JSON.parse(domVal);
                                    //获得所有的参数
                                    var tmpArr = this.toSave.urlTemplate.match(/\{[\s\S]+?\}/g);
                                    this.pageParamJson = [];
                                    var obj = {};
                                    obj.key = tmpArr[0].replace("{", "").replace("}", "");
                                    obj.value = eventTypeJson.NewsTypeId;
                                    //系统模块
                                    obj.pageModule = JSON.stringify(value);
                                    //具体关联
                                    obj.pageType = JSON.stringify(eventTypeJson);
                                    this.pageParamJson.push(obj);


                                }
                                break;
                            case 4:
                                if (this.toSave.domObj.val() == "") {
                                    alert("请选择一个活动!");
                                    return false;
                                }
                                //抽奖 活动详情
                                var domVal = this.toSave.domObj.attr("data-value");
                                if (domVal != "") {
                                    //活动详情
                                    var eventDetailJson = JSON.parse(domVal);
                                    //获得所有的参数
                                    var tmpArr = this.toSave.urlTemplate.match(/\{[\s\S]+?\}/g);
                                    this.pageParamJson = [];
                                    var obj = {};
                                    obj.key = tmpArr[0].replace("{", "").replace("}", "");
                                    obj.value = eventDetailJson.EventId;
                                    //系统模块
                                    obj.pageModule = JSON.stringify(value);
                                    //具体关联
                                    obj.pageType = JSON.stringify(eventTypeJson);
                                    //页面详情
                                    obj.pageDetail = JSON.stringify(eventDetailJson);
                                    this.pageParamJson.push(obj);
                                }
                                break;
                            case 7:
                                if (this.toSave.domObj.val() == "") {
                                    alert("请选择一个资讯!");
                                    return false;
                                }
                                //资讯详情
                                var domVal = this.toSave.domObj.attr("data-value");
                                if (domVal != "") {
                                    //资讯详情
                                    var newsDetailJson = JSON.parse(domVal);
                                    //获得所有的参数
                                    var tmpArr = this.toSave.urlTemplate.match(/\{[\s\S]+?\}/g);
                                    this.pageParamJson = [];
                                    var obj = {};
                                    obj.key = tmpArr[0].replace("{", "").replace("}", "");
                                    obj.value = newsDetailJson.NewsId;
                                    //系统模块
                                    obj.pageModule = JSON.stringify(value);
                                    //具体关联
                                    obj.pageType = JSON.stringify(eventTypeJson);
                                    //页面详情
                                    obj.pageDetail = JSON.stringify(newsDetailJson);
                                    this.pageParamJson.push(obj);
                                }
                                break;
                        }
                    }
                    //设置选择的类型
                    if (this.pageParamJson.length) {
                        this.pageParamJson[0].UnionTypeId = this.unionType;
                    }
                    else {
                        this.pageParamJson.push({});
                        this.pageParamJson[0].UnionTypeId = this.unionType;
                    }
                } else {
                    that.unionType = "0";
                }
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'WX.Menu.SetMenu',
                        'MenuId': page.MenuId,
                        'WeiXinId': objson.WeiXinId,
                        'Name': title,
                        'ParentId': page.parentId,
                        'Level': page.level,
                        'DisplayColumn': displayColumn,
                        'Status': parseInt(status),
                        'ApplicationId': applicationId,
                        'Text': text,
                        //文本内容
                        'MenuUrl': menuUrl,
                        //链接地址
                        'ImageUrl': imageUrl,
                        //图片地址
                        'MessageType': messageType,
                        'UnionTypeId': that.unionType || "",
                        'MaterialTextIds': materialTextIds,
                        'ModuleName': moduleName, //模块名称
                        'PageId': pageId,
                        'PageParamJson': JSON.stringify(that.pageParamJson ? that.pageParamJson : [{}])
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            that.saveType = "add";  //设置为不是添加的
                            page.MenuId = data.Data.MenuId;
                            alert("菜单<" + title + ">保存成功!");
                            //用来删除
                            that.elems.saveData.attr("data-menu", "");
                            //存储保存后的名称
                            if (page.level == 1) {
                                that.menuName = title;
                            }
                           //that.fillContent();
                            that.elems.weixinList.trigger("change");
                            that.clearInput();
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //上传图片
            uploadImage: function (btn, callback) {
                var that = this;
                var uploadbutton = KE.uploadbutton({
                    width: "100%",
                    button: btn,
                    //上传的文件类型
                    fieldName: 'imgFile',
                    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                    url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=536',
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
                        }
                        else {
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
            }

        };

    page.loadData =
    {
        //发布菜单
        publishMenu: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Menu.PublishMenu',
                    'ApplicationId': page.applicationId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得Menu详情
        getMenuDetail: function (menuId, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Menu.GetMenuDetail',
                    'MenuId': menuId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //删除微信菜单
        delMenu: function (menuId, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Menu.DeleteMenu',
                    'MenuId': menuId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得微信菜单
        getMenuList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "get",
                data:
                {
                    'action': 'WX.Menu.GetMenuList',
                    'ApplicationId': page.applicationId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得资讯明细
        getEventList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Event.GetEventList',
                    'EventTypeId': page.events.eventTypeId,
                    'EventName': page.events.eventName,

                    'BeginFlag': null,
                    //活动是否开始
                    'EndFlag': null,
                    //活动是否结束
                    'EventStatus': null,
                    //活动状态
                    'PageIndex': page.currentPage,
                    'PageSize': page.pageSize
                },
                success: function (data) {

                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得资讯明细
        getNewsList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.News.GetNewsList',
                    'NewsTypeId': page.events.eventTypeId,
                    'NewsName': page.events.eventName,
                    'PageSize': page.pageSize,
                    'PageIndex': page.currentPage
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //WX. Event.GetDrawMethodList
        //获取活动抽奖方式
        getDrawMethodList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Event.GetDrawMethodList',
                    'EventTypeId': page.events.eventTypeId,
                    //抽奖；类型ID
                    'EventName': page.events.eventName,
                    'PageSize': page.pageSize,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得资讯类别
        getNewsTypeList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.News.GetNewsTypeList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获取图文列表  点击图文列表的图文id的时候
        getMaterialTextList: function (id, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.MaterialText.GetMaterialTextList',
                    'MaterialTextId': id,
                    'Name': page.MaterialTextName,  //图文名称
                    'TypeId': page.MaterialTypeId,   //图文id
                    'PageSize': page.pageSize,
                    'PageIndex': page.pageIndex
                },
                success: function (data) {
                    console.log(data);
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得图文分类数据
        getImageContentCategory: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.MaterialText.GetMaterialTextTypeList',
                    'ApplicationId': page.applicationId,
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得所有的微信账号
        getWeiXinAccount: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Account.GetAccountList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得系统模块
        getSystemModule: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Module.GetSysModuleList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {

                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //根据活动列表选择所有的活动类别
        getEventType: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Event.GetEventTypeList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        }
    }
    //初始化
    page.init();
});