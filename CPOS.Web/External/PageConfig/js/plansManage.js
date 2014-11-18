define(['jquery', 'template', 'tools', 'pagination'], function ($, temp) {
    var page = {
        ele: {
            section: $("#section")
        },
        temp: temp,
        page: {
            pageIndex: 0,
            pageSize: 15
        },
        init: function () {
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            //this.loadPageList();
            this.pageListInner=[{"PageId":"9d7a06f4-20b3-4d93-900a-0dde1b0ef834","Title":"微商城会员注册","PageKey":"Register","Version":"1.0","LastUpdateTime":"2014/7/22 15:57:28","MappingID":null},{"PageId":"a915c88b-d791-47b4-8407-e844c72a08b7","Title":"热销榜单列表","PageKey":"WellSellList","Version":"1.0","LastUpdateTime":"2014/7/22 15:36:26","MappingID":null},{"PageId":"58b75597-5735-4bb4-a274-181736c21317","Title":"微商城首页","PageKey":"IndexShop","Version":"1.0","LastUpdateTime":"2014/7/21 14:50:52","MappingID":null},{"PageId":"fcb5d384-5146-461e-aadf-a0ebce2b9cfc","Title":"微官网","PageKey":"$HomeIndex","Version":"1.0","LastUpdateTime":"2014/7/14 15:13:09","MappingID":null},{"PageId":"50f0d046-f29d-4086-987f-55fbc2ce574c","Title":"微商城首页-App版","PageKey":"IndexShopApp","Version":"1.0","LastUpdateTime":"2014/7/9 20:17:56","MappingID":null},{"PageId":"b4d95332-e098-4741-a6a8-da8dfaaf359f","Title":"资讯列表页-单个分类","PageKey":"NewsListSingle","Version":"1.0","LastUpdateTime":"2014/7/9 19:47:40","MappingID":null},{"PageId":"37f4929e-788e-4461-9c7f-b1ce8241375f","Title":"资讯列表页-全部分类","PageKey":"NewsList","Version":"1.0","LastUpdateTime":"2014/7/9 19:47:18","MappingID":null},{"PageId":"8ea205a1-02c4-449d-baa0-157202159b65","Title":"加盟服务与产品","PageKey":"JoinProduct","Version":"1.0","LastUpdateTime":"2014/7/9 17:09:28","MappingID":null},{"PageId":"cb7f8b1c-7a63-495d-9c06-bc14341db03d","Title":"我发的红包","PageKey":"RedPacketSend","Version":"1.0","LastUpdateTime":"2014/7/8 18:43:57","MappingID":null},{"PageId":"4c1de336-e995-4569-9ab0-0499033654ad","Title":"我领的红包","PageKey":"RedPacketGet","Version":"1.0","LastUpdateTime":"2014/7/8 18:39:42","MappingID":null},{"PageId":"b9f9d483-fe55-44e5-a824-aa092e420a0c","Title":"微官网3","PageKey":"$HomeIndex3","Version":"1.0","LastUpdateTime":"2014/7/7 14:31:38","MappingID":null},{"PageId":"1b2e00eb-45ed-484a-80b7-7f0995266afd","Title":"商品列表页面","PageKey":"GoodsList","Version":"1.0","LastUpdateTime":"2014/7/4 16:06:30","MappingID":null},{"PageId":"38fbc7e8-4557-4333-b91f-a63cc80e5055","Title":"红包-新","PageKey":"RedPacket","Version":"1.0","LastUpdateTime":"2014/7/4 11:58:41","MappingID":null},{"PageId":"baeb14c3-2bae-4a8c-bdae-09c694e908e7","Title":"微官网2[123]","PageKey":"$HomeIndex2","Version":"1.0","LastUpdateTime":"2014/7/3 17:20:09","MappingID":null},{"PageId":"63883c9e-e819-430f-b590-e3a211da9b47","Title":"微官网1","PageKey":"$HomeIndex1","Version":"1.0","LastUpdateTime":"2014/6/30 12:11:57","MappingID":null}];

            this.pageListOuter = [{"PageId":"0daa44da-7b6f-44a0-88ac-15e797b6710c","Title":"大转盘页面","PageKey":"BigWheel","Version":"1.0","LastUpdateTime":"2014/6/17 18:02:39","MappingID":null},{"PageId":"900209a3-791f-48dc-9d29-d9b8954a974b","Title":"刮刮卡","PageKey":"ScratchCard","Version":"1.0","LastUpdateTime":"2014/6/17 17:29:10","MappingID":null},{"PageId":"8487f970-4542-4916-8cc6-ea810dfb708e","Title":"商品确认页面","PageKey":"GoodsOrder","Version":"1.0","LastUpdateTime":"2014/6/13 20:53:26","MappingID":null},{"PageId":"0add5d05-361f-4e97-89aa-472bfed2ef89","Title":"我的优惠券列表页面","PageKey":"CouponDetail","Version":"1.0","LastUpdateTime":"2014/6/13 13:28:08","MappingID":null},{"PageId":"6e4f7b34-1cb8-47bb-93ac-498d96ee16a0","Title":"我的优惠券列表页面","PageKey":"CouponList","Version":"1.0","LastUpdateTime":"2014/6/13 13:27:40","MappingID":null},{"PageId":"16e47ae9-492c-4524-aada-834ec3e14675","Title":"会员卡页面","PageKey":"GetVipCard","Version":"1.0","LastUpdateTime":"2014/6/13 13:14:21","MappingID":null},{"PageId":"d3c11ec2-5c86-4d02-966d-2a73595c0cf3","Title":"我的余额","PageKey":"Balance","Version":"1.0","LastUpdateTime":"2014/6/13 11:48:22","MappingID":null},{"PageId":"ef80a344-52ed-452c-8179-4ea0fa3cf496","Title":"我的积分","PageKey":"MyScore","Version":"1.0","LastUpdateTime":"2014/6/13 10:03:03","MappingID":null},{"PageId":"b6f93061-5823-44f7-932b-fa98832de997","Title":"团购商品列表","PageKey":"GroupGoodsList","Version":"1.0","LastUpdateTime":"2014/6/12 9:50:16","MappingID":null},{"PageId":"d4e954f8-18e8-4244-be97-67b3a3d65676","Title":"前世今生","PageKey":"H_Introduce","Version":"1.0","LastUpdateTime":"2014/6/11 17:12:06","MappingID":null},{"PageId":"bc5deee1-8bd8-4600-810a-2d3729b3eb1d","Title":"消息中心","PageKey":"Message","Version":"1.0","LastUpdateTime":"2014/6/11 11:41:31","MappingID":null},{"PageId":"f029797c-e4f6-4ee0-8286-224505923b5f","Title":"酒店详情 - 热点","PageKey":"H_Hot11","Version":"1.0","LastUpdateTime":"2014/6/10 10:04:21","MappingID":null},{"PageId":"c13eb0a0-6043-4eb2-bb8d-52af4e66d088","Title":"查看照片","PageKey":"H_Photo","Version":"1.0","LastUpdateTime":"2014/6/9 18:22:02","MappingID":null},{"PageId":"ec4b2857-47f3-4d77-b013-28df13e20bbf","Title":"地图查看","PageKey":"H_Map","Version":"1.0","LastUpdateTime":"2014/6/9 18:16:59","MappingID":null},{"PageId":"5bc0c411-6320-4d7a-8c2c-4463a119885b","Title":"系统定位中...","PageKey":"H_GetPosition","Version":"1.0","LastUpdateTime":"2014/6/9 18:15:36","MappingID":null}];

            this.renderPageList();

            this.pageObjInner = $.util.list2obj(this.pageListInner,"PageId");
            this.pageObjOuter = $.util.list2obj(this.pageListOuter,"PageId");

        },
        initEvent: function () {
            this.ele.section.delegate("#searchBtn", "click", function () {
                self.loadPageList();
            }).delegate("#publicBtn", "click", function () {
                self.public();
            }).delegate("tbody>tr", "click", function () {
                $(this).toggleClass("on");
            }).delegate("#toRightBtn", "click", function () {
                if($("#innerContainer .on").length){
                    $("#innerContainer .on").each(function(i,e){
                        var pageId = $(e).attr("data-pageId");
                        self.pageObjOuter[pageId] =self.pageObjInner[pageId];
                        delete self.pageObjInner[pageId];
                    });
                    self.pageListInner = $.util.obj2list(self.pageObjInner);
                    self.pageListOuter = $.util.obj2list(self.pageObjOuter);
                    self.renderPageList();
                }
            }).delegate("#toLeftBtn", "click", function () {
                if($("#outerContainer .on").length){
                    $("#outerContainer .on").each(function(i,e){
                        var pageId = $(e).attr("data-pageId");
                        self.pageObjInner[pageId] =self.pageObjOuter[pageId];
                        delete self.pageObjOuter[pageId];
                    });
                    self.pageListInner = $.util.obj2list(self.pageObjInner);
                    self.pageListOuter = $.util.obj2list(self.pageObjOuter);
                    self.renderPageList();
                }
            });
        },
        public: function () {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.CreateCustomerConfig",
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("发布成功");
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageList: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.GetSysPageList",
                data: {
                    Key: this.ele.key.val(),
                    Name: this.ele.name.val(),
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                beforeSend: function () {

                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            self.renderPageList(data.Data.PageList);

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
                                            self.loadPageList();
                                        }
                                    });
                                }
                            } else {
                                self.ele.section.find('.pageWrap').hide();
                            }
                            // 分页处理 end
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        renderPageList: function () {

            $("#outerContainer").html(template.render("pageList",{list:this.pageListOuter}));
            $("#innerContainer").html(template.render("pageList",{list:this.pageListInner}));
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});