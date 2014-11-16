define(['jquery', 'template', 'tools', 'kkpager'], function ($) {
    var page = {
        ele: {
            section: $("#section"),
            tabMenu: $("#tabMenu"),
            sureTable: $("#sureTable"),
            unsureTable: $("#unsureTable")
        },
        temp: {
            thead: {
                "1": $("#unsureTheadTemp").html(),
                "10": $("#sureTheadTemp").html()
            },
            tbody: {
                "1": $("#unsureTbodyTemp").html(),
                "10": $("#sureTbodyTemp").html()
            }
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
            this.eventId = "16856b2950892b62473798f3a88ee3e3";
            this.loadData.args.EventId=this.eventId;
            this.loadPageData();
            this.initEvent();
        },
        loadPageData: function () {
            this.loadPageList();
        },
        initEvent: function () {
            var that=this;
            $(".subMenu").delegate("li","click",function(e){
                var $t=$(this);
                $t.addClass("on").siblings().removeClass("on");
                setTimeout(function(){
                    var theUrl=$t.data("href");
		    if(theUrl!=""){
			location.href=theUrl;
		    }
                    
                },500);
                that.stopBubble(e);
            });
            //鼠标移上去效果
            $(".subMenu li").mouseover(function(e){
                var $t=$(this);
                if(!$t.hasClass("on")){
                    $t.addClass("on");
                    that.moveDom=$t;
                }
                 that.stopBubble(e);
            }).mouseout(function(e){
                if(that.moveDom&&that.moveDom.hasClass("on")){
                    that.moveDom.removeClass("on");
                }
                 that.stopBubble(e);
            });
        },
        loadPageList: function (callback) {
            var that=this;
            //获得签到列表
            this.loadData.getSignList(function(data){
                var list=data.Data.MappingList;
                list=list?list:[];
                //设置显示多少条数据
                $("#countNum").html(list.length);
                //已经签到总数
                $("#signCount").html(data.Data.TotalCount);
                var html=bd.template("tpl_content",{list:list})
                $("#content").html(html);
                if(data.Data.TotalPage>1){
                    kkpager.generPageHtml({
                        pno: 1,
                        mode: 'click', //设置为click模式
                        //总页码  
                        total: data.Data.TotalPage,
                        isShowTotalPage: false,
                        isShowTotalRecords: false,
                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                        //适用于不刷新页面，比如ajax
                        click: function (n) {
                            //这里可以做自已的处理
                            //...
                            //处理完后可以手动条用selectPage进行页码选中切换
                            this.selectPage(n);

                            that.loadMoreData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);
                }
            });
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex=currentPage-1;
            this.loadData.getSignList(function(data){
                var list=data.Data.MappingList;
                list=list?list:[];
                //设置显示多少条数据
                $("#countNum").html(list.length);
                var html=bd.template("tpl_content",{list:list})
                $("#content").html(html);
            });
        },
        renderTable: function (data) {
            var table = this.tableMap[this.status];
            var tempHead = this.temp.thead[this.status];
            var tempBody = this.temp.tbody[this.status];
            table.find("thead tr").html(self.render(tempHead, { obj: data.DicColNames }));
            table.find("tbody").html(self.render(tempBody, { list: data.SignUpList }));

            this.ele.tabMenu.find(".unsureTable em").html(data.TotalCountUn);
            this.ele.tabMenu.find(".sureTable em").html(data.TotalCountYet);
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
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
        loadData: {
            args:{
                PageIndex:0,
                PageSize:10
            },
            //获得签到列表
            getSignList:function(callback){
                $.util.ajax({
                    url: "/ApplicationInterface/NwEvents/NwEventsGateway.ashx",
                    type: "get",
                    customerId: "86a575e616044da3ac2c3ab492e44445",
                    userId: "004852E9-7AA1-4C3F-97A3-361B8EA96464",      //实际用时去掉  接口示例有，没有会报错，实际用时去掉
                    data: {
                        action: "GetEventUserMapping",
                        EventId: this.args.EventId,
                        PageIndex:this.args.PageIndex,
                        PageSize:this.args.PageSize
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            
        }

    };

    self = page;

    page.init();
});