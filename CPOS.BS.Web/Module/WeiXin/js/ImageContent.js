define(['jquery', 'tools', 'template', 'kindeditor', 'lang', 'pagination', 'drag'], function () {

    //debugger;
    //上传图片
    KE = window.KindEditor;
    var page =
    {
        pageSize: 10,   //默认控制条数
        currentPage: 0, //默认第一页
        pageParamJson: [],
        MaterialTextId: "",
        //图文素材ID
        url: "/ApplicationInterface/Gateway.ashx",
        generateUrl: "",
        //图文关联后的url地址
        unionType: 0,
        count: 0,    //加载请求的个数
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
        //关联到的类别
        elems:
        {
            cancelBtn: $("#cancelBtn"), //取消
            lottoryWay: $("#lottoryWay"),
            //抽奖方式
            lottoryWayInput: $("#lottoryWay .inputBox"),
            //抽奖方式选择
            saveData: $("#btnSaveData"),
            //保存数据
            imageTitle: $("#imageTitle"),
            //标题
            weixinAccount: $("#weixinAccount"),
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
			contentDigest: $('#contentDigest'),
			//摘要模块
			contentDigestText: $('#contentDigestText'),
			//摘要内容
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

        init: function () {
            var that = this;
            var type = $.util.getUrlParam("type");
            //表示新增图文消息
            if (type == "add") {
                //填充所有的数据
                this.fillContent();
            }
            //编辑图文消息
            if (type == "edit") {
                //图文ID
                var imageContentId = $.util.getUrlParam("imageContentId");
                //保存图文素材id
                this.MaterialTextId = imageContentId;
                this.fillContent(true);  //编辑
            }

            //初始化kindEditor
            //KE.ready(function (K) {
            window.editor = KE.create('#editor_id',
                {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    allowFileManager: true
                });
            //});

            this.initEvent();
            this.events.init();
        },
        //所有的数据请求
        loadData:
        {   //获得资讯明细
            getEventList: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'WX.Event.GetEventList',
                        'EventTypeId': page.events.eventTypeId,
                        'EventName': page.events.eventName,

                        'BeginFlag': null,     //活动是否开始
                        'EndFlag': null,       //活动是否结束
                        'EventStatus': null,    //活动状态
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
                        'EventTypeId': page.events.eventTypeId,   //抽奖；类型ID
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
                        'PageSize': 1,
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
            //获得图文分类数据
            getImageContentCategory: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'WX.MaterialText.GetMaterialTextTypeList',
                        'ApplicationId': page.ApplicationId,
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
        },
        //根据传递的参数判断是否是新增还是编辑模式
        fillContent: function (edit) {

            var that = this;
            if (edit) {//编辑模式 
                //填充微信账号
                this.loadData.getWeiXinAccount(function (data) {
                    var obj = {
                        itemList: data.Data.AccountList
                    }
                    var html = bd.template("accountTmpl", obj);

                    that.elems.accountSelect.html(html);
                    //ApplicationId
                    that.ApplicationId = data.Data.AccountList[0].ApplicationId;
                    //填充图文分类
                    that.loadData.getImageContentCategory(function (data) {
                        that.count++;
                        var obj2 =
                        {
                            itemList: data.Data.MaterialTextTypeList
                        }
                        var html2 = bd.template("optionTmpl", obj2);
                        that.elems.imageCategory.find(".selectBox").html(html2);
                        //设置前面两个请求是否加载完成
                        that.isLoad = true;
                        //获得数据并且把数据填充到页面上
                        that.loadData.getMaterialTextList(that.MaterialTextId, function (data) {
                            var itemList = data.Data.MaterialTextList;
                            if (itemList.length) {
                                var obj = itemList[0];
                                var pageParamJson = obj.PageParamJson;
                                pageParamJson = JSON.parse(pageParamJson);
                                var config = {};
                                if (pageParamJson && pageParamJson.length) {
                                    config = pageParamJson[0];  //底部关联的内容
                                }
                                //设置微信账号
                                that.elems.accountSelect.find("option[value='" + obj.ApplicationId + "']").attr("selected", true);
                                //设置标题
                                that.elems.imageTitle.val(obj.Title);
                                //设置分类
                                that.elems.imageCategorySelect.find("option[value='" + obj.TypeId + "']").attr("selected", true);
                                //设置图片
                                that.elems.imageView.html("<img src=" + obj.ImageUrl + ">");
                                //保存图片
                                that.generateUrl = obj.ImageUrl;
								
								//摘要内容
								that.elems.contentDigestText.val(obj.Abstract || '');
                                //表示的是链接
                                switch (config.UnionTypeId - 0) {
                                    case 1:
                                        that.unionType = 1;
                                        that.hideElems();
                                        that.elems.category.find("option[value='" + 1 + "']").attr("selected", "selected");
                                        that.elems.linkUrl.removeClass("hide").addClass("show");
                                        //设置链接
                                        that.elems.urlAddress.val(obj.OriginalUrl);
                                        break;
                                    case 2:
                                        //表示的是详细页
                                        that.hideElems();
                                        that.unionType = 2;
                                        that.elems.category.find("option[value='" + 2 + "']").attr("selected", "selected");
                                        that.elems.contentEditor.removeClass("hide").addClass("show");
										//that.elems.contentDigest.removeClass("hide").addClass("show");
                                        //设置详情
                                        window.editor.html(obj.Text);
                                        break;
                                    case 3:
                                        that.elems.category.find("option[value='" + 3 + "']").attr("selected", "selected");
                                        //表示的是系统功能模块
                                        that.hideElems();
                                        that.unionType = 3;
                                        //该选择框是否已经加载过数据
                                        var isLoad = that.elems.moduleSelect.attr("data-load");
                                        //未加载过  加载模块
                                        if (isLoad == "false") {
                                            that.loadData.getSystemModule(function (data) {
                                                var obj =
                                                {
                                                    itemList: data.Data.SysModuleList
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

                                                switch (svalue) {
                                                    case 0:
                                                        //默认请选择的时候
                                                        that.hideElems(that.elems.moduleName);
                                                        break;
                                                    case 1:
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
                                                        //数据绑定
                                                        that.toSave.domObj.attr("data-value", config.pageDetail);
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
                                                        });
                                                        that.elems.newsType.removeClass("hide").addClass("show");
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
                                                switch (pageCode) {


                                                }



                                            });
                                        }
                                        that.elems.moduleName.removeClass("hide").addClass("show");

                                }
                            }

                        });
                    });
                });
            } else {

                //填充微信账号
                this.loadData.getWeiXinAccount(function (data) {
                    that.count++;
                    var obj =
                {
                    itemList: data.Data.AccountList
                }
                    var html = bd.template("accountTmpl", obj);
                    that.elems.accountSelect.html(html);
                    //ApplicationId
                    that.ApplicationId = data.Data.AccountList[0].ApplicationId;
                    //填充图文分类,删除分类
					/*
                    that.loadData.getImageContentCategory(function (data) {
                        that.count++;
                        var obj =
                        {
                            itemList: data.Data.MaterialTextTypeList
                        }
                        var html = bd.template("optionTmpl", obj);
                        that.elems.imageCategory.find(".selectBox").html(html);
                    });
					*/
                });

            }

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
                switch (page.popupOptions.type) {  //chooseNews  获取资讯列表  chooseEvents获取活动抽奖列表
                    case "chooseNews":
                        page.popupOptions.type = "chooseNews";
                        page.loadData.getNewsList(function (data) {
                            //资讯列表
                            page.popupOptions.itemList = data.Data.NewsList ? data.Data.NewsList : [];
                            var items = bd.template("itemTmpl", page.popupOptions);
                            $("#itemsTable").html(items);
                            //获得总共页数
                            //that.initPagination(currentPage, data.Data.TotalPages);
                        });
                        break;
                    case "chooseEvents":
                        page.popupOptions.type = "chooseEvents";
                        page.loadData.getEventList(function (data) {
                            page.popupOptions.itemList = data.Data.EventList ? data.Data.EventList : [];
                            var items = bd.template("itemTmpl", page.popupOptions);
                            $("#itemsTable").html(items);
                            //获得总共页数
                            // that.initPagination(currentPage, data.Data.TotalPages);
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
                    page.currentPage = 0;
                    page.events.eventTypeId = "";
                    page.events.eventName = "";
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
                        } else if (page.popupOptions.type == "chooseEvents") {
                            //设置EventsTypeId
                            page.events.eventTypeId = jsonType.EventTypeId;
                        }

                    }
                    //根据类型来判断是取的什么接口数据
                    switch (page.popupOptions.type) {  //chooseNews  获取资讯列表  chooseEvents获取活动抽奖列表
                        case "chooseNews":
                            page.popupOptions.type = "chooseNews";
                            page.loadData.getNewsList(function (data) {
                                //资讯列表
                                page.popupOptions.itemList = data.Data.NewsList ? data.Data.NewsList : [];
                                var items = bd.template("itemTmpl", page.popupOptions);
                                $("#itemsTable").html(items);
                                //获得总共页数
                                that.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                    that.loadMoreData(currentPage); //加载更多
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
                                    that.loadMoreData(currentPage); //加载更多
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
                    } else if (page.popupOptions.type == "chooseEvents") { //选择活动
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
                    //活动详情    弹出搜索活动详情的

                    this.popupOptions.popupName = "活动名称";
                    this.popupOptions.popupSelectName = "活动类别";
                    //未选择的时候弹出提示的名称
                    this.popupOptions.tipsName = "活动";
                    this.popupOptions.title = ["活动名", "活动类别", "活动时间", "活动城市", "活动状态"];
                    this.popupOptions.topTitle = "选择活动";
                    var html = bd.template("popDivTmpl", this.popupOptions);
                    this.elems.chooseEventsDiv.html(html);
                    //获得总共页数
                    this.events.initPagination(1, 5);
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
                    this.currentPage = 0;
                    this.events.eventName = "";
                    this.events.eventTypeId = "";
                    //显示
                    //获取抽奖活动
                    this.loadData.getEventList(function (data) {
                        //资讯列表
                        that.popupOptions.itemList = data.Data.EventList;
                        var items = bd.template("itemTmpl", that.popupOptions);
                        $("#itemsTable").html(items);
                        that.currentPage = 0; //设置默认页面为0
                        //获得总共页数
                        that.events.initPagination(that.currentPage + 1, data.Data.TotalPages, function (currentPage) {
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
                    this.currentPage = 0;
                    this.events.eventName = "";
                    this.events.eventTypeId = "";
                    //获取资讯明细
                    this.loadData.getNewsList(function (data) {
                        //资讯列表
                        that.popupOptions.itemList = data.Data.NewsList;
                        var items = bd.template("itemTmpl", that.popupOptions);
                        $("#itemsTable").html(items);
                        that.currentPage = 0; //设置默认页面为0
                        //获得总共页数
                        that.events.initPagination(that.currentPage + 1, data.Data.TotalPages, function (currentPage) {
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
        initEvent: function () {
            //初始化事件集
            var that = this;
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
                switch (value) {
                    case 0:
                        //默认请选择的时候
                        that.hideElems();
                        break;
                    case 1:
                        //表示的是链接
                        that.unionType = 1;
                        that.hideElems();
                        that.elems.linkUrl.removeClass("hide").addClass("show");
                        break;
                    case 2:
                        //表示的是详细页
                        that.hideElems();
                        that.unionType = 2;
                        that.elems.contentEditor.removeClass("hide").addClass("show");
						//摘要显示
						//that.elems.contentDigest.removeClass("hide").addClass("show");
                        break;
                    case 3:
                        //表示的是系统功能模块
                        that.hideElems();
                        that.unionType = 3;
                        //该选择框是否已经加载过数据
                        var isLoad = that.elems.moduleSelect.attr("data-load");
                        //未加载过
                        if (isLoad == "false") {
                            that.loadData.getSystemModule(function (data) {
                                var obj =
                                {
                                    itemList: data.Data.SysModuleList
                                }
                                var html = bd.template("moduleTmpl", obj);
                                that.elems.moduleSelect.html(html);
                                //手动触发事件
                                that.elems.moduleSelect.trigger("change");
                            });
                        }
                        that.elems.moduleName.removeClass("hide").addClass("show");
                        break;

                }
            });
            //活动类别点击事件
            this.elems.eventSelect.bind("click", function () { });
            //保存数据
            this.elems.saveData.bind("click", function () {
                that.saveData();
            });
            //取消事件
            this.elems.cancelBtn.bind("click", function () {
                window.history.go(-1);
            });
            //活动模块选择
            //此处为最复杂的级联逻辑
            this.elems.moduleSelect.bind("change", function () {
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
                        obj.key = tmpArr[i].replace("{", "").replace("}");
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
            });
            //选择活动详情的时候事件
            this.elems.eventDetailSelect.bind("click", function () {
                //数据恢复
                that.currentPage = 0;
                that.events.eventName = "";
                that.events.eventTypeId = "";
                //that.showMask(true, 1);
                that.showMask(true, 2);
                //将点击的输入框保存起来
                that.toSave.detailDomObj = $(this);
                that.stopBubble(e);
                //显示
            });
            //选择资讯详情的时候事件
            this.elems.newsDetailSelect.bind("click", function (e) {
                //数据恢复
                that.currentPage = 0;
                that.events.eventName = "";
                that.events.eventTypeId = "";
                //将点击的输入框保存起来
                that.toSave.detailDomObj = $(this);
                that.showMask(true, 3);
                //显示
            });
            //选择活动抽奖的时候弹出层   然后没有抽奖详情
            this.elems.lottoryWayInput.bind("click", function () {
                //将点击的输入框保存起来
                that.toSave.detailDomObj = $(this);
                that.showMask(true, 2);

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
        saveData: function () {
            var textId = null, //图文素材id  标识
            title = null, //标题
            author = null, //作者
            imageUrl = null, //图片地址
            originalUrl = null, //原文连接
            text = null, //文本内容
			digestText = null, // 摘要文本内容
            displayIndex = null, //排序
            applicationId = null, //申请接口主标识
            typeId = null, //图文类别
            pageId = null, //模块id
            moduleName = null, //模块名称
            showCover = 0,      //是否显示封面图片
            pageParamJson = {};
            //页面替换参数JSON
            var that = this;
            //接口标识
            applicationId = this.elems.accountSelect.val();
            //标题
            title = this.elems.imageTitle.val();
            //图文类别id//删除分类
            //typeId = this.elems.imageCategorySelect.val();
            //图片地址
            imageUrl = this.generateUrl;
            this.unionType = this.elems.category.val();
            showCover = $("#showCover").val();
			digestText = that.elems.contentDigestText.val();//摘要文本内容
            if (title.length == 0) {
                alert("请填写标题");
                return false;
            }
            if (imageUrl == "") {
                alert("请上传封面图片");
                return false;
            }
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
                originalUrl = linkUrl;
            }
            //详情的时候
            if (this.unionType == 2) {
                //获得源码
                var html = window.editor.html();
                text = html;
                if (html.length == 0) {
                    alert("请输入详细内容");
                    return false;
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
                    case 0:  //系统默认的普通模块
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
                    case 3:
                    case 4:
                        if (this.toSave.domObj.find("input").val() == "") {
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
            } else {
                this.pageParamJson.push({});
                this.pageParamJson[0].UnionTypeId = this.unionType;
            }
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    action: "WX.MaterialText.SetMaterialText",
                    MaterialText:
                    {
                        UnionTypeId: that.unionType,
                        //关联到的id
                        TextId: that.MaterialTextId,
                        Title: title,
                        Author: "",
                        ImageUrl: imageUrl,
                        OriginalUrl: originalUrl,
                        Text: text,
                        ApplicationId: applicationId,
                        //TypeId: typeId,//删除分类
						Abstract: digestText,//摘要	
                        PageId: pageId,
                        //是否显示封面图片
                        IsTitlePageImage: showCover,
                        ModuleName: moduleName,
                        PageParamJson: JSON.stringify(that.pageParamJson ? that.pageParamJson : [{}])
                    }
                },
                success: function (data) {
                    //保存成功后把该图文的唯一ID给保存起来   如果再次提交则是提示修改成功
                    if (data.ResultCode == 0) {
                        if (that.MaterialTextId == "") {
                            alert("图文素材保存成功");
                            that.MaterialTextId = data.Data.MaterialTextId;
                            window.history.go(-1);
                        }
                        else {
                            alert("图文素材修改成功!");
                            window.history.go(-1);
                        }
                    } else {
                        alert(data.Message);
                    }
                },
                error: function () { }
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
    //初始化
    page.init();


});