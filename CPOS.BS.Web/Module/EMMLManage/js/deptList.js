define(['jquery', 'template','tools','kkpager','artDialog'], function ($) {
    window.alert = function (content, autoHide) {
        var d = dialog({
            title: '提示',
            cancelValue: '关闭',
            skin: "facebook",
            content: content
        });
        page.d = d;
        d.showModal();
        if (autoHide) {
            setTimeout(function () {
                page.d.close();
            }, 2000);
        }
    }
    var page = {
        ele: {
            section: $("#section"),
            sureTable: $("#sureTable")
        },
        temp: {
            thead: {
                "2": $("#sureTheadTemp").html()
            },
            tbody: {
                "1": $("#sureTbodyTemp").html()
            }
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        //显示弹层
        showElements: function (selector) {
            this.ele.uiMask.show();
            $(selector).slideDown();
        },
        hideElements: function (selector) {

            this.ele.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        },
        init: function () {
            this.url = "/ApplicationInterface/Product/QiXinManage/QiXinManageHandler.ashx";
            this.customerId = "e703dbedadd943abacf864531decdac1"; //$.util.getUrlParam("customerId"); 
            this.userId = "02AA1B9C39E941F498B2D406D4EB32F8"; //$.util.getUrlParam("userId");   //"82B04CE0C05E4AFF9D2C51743B2E0A08";

            //this.eventId = "16856b2950892b62473798f3a88ee3e3";
            //this.status = this.ele.tabMenu.find(".tabItem.on").data("status");
            this.status = "1";
            this.tableMap = {
                "1": this.ele.sureTable
            };
            this.loadData();
            this.initEvent();
        },
        buildAjaxParams: function (param) {
            var _param = {
                url: "",
                type: "get",
                dataType: "json",
                data: null,
                beforeSend: function () {

                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            };

            $.extend(_param, param);


            var action = param.data.action,
                interfaceType = param.data.interfaceType || 'Product',
                _req = {
                    'CustomerID': (param.data.customerId ? param.data.customerId : null),
                    'UserID': param.data.userId ? param.data.userId : null,
                    'Parameters': param.data
                };

            delete param.data.customerId;
            delete param.data.userId;
            delete param.data.action;
            delete param.data.interfaceType;

            var _data = {
                'req': JSON.stringify(_req)
            };

            _param.data = _data;

            _param.url = _param.url + '?type=' + interfaceType + '&action=' + action;

            return _param;
        },
        ajax: function (param) {

            var _param = this.buildAjaxParams(param);

            $.ajax(_param);
        },
        loadData: function () {
            this.loadPageList();
        },
        initEvent: function () {
            var that = this;
        },
        sendMessageAction: function (str, callback) {
            var self = this;
            $.util.ajax({
                url: "/ApplicationInterface/NwEvents/NwEventsGateway.ashx",
                type: "get",
                customerId: "e703dbedadd943abacf864531decdac1",
                userId: "004852E9-7AA1-4C3F-97A3-361B8EA96464",      //实际用时去掉  接口示例有，没有会报错，实际用时去掉
                data: {
                    action: "NotifyEventSignUp",
                    SignUpIds: str
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        }
                    } else {
                        if (page.d) {
                            page.d.close();
                        }
                        alert(data.Message);
                    }
                }
            });
        },
        //加载更多
        loadMoreData: function (currentPage) {
            var that = this;
            this.page.pageIndex = currentPage - 1;
            this.getPageList(function (data) {
                var columns={};
                that.renderTable(data.Data,columns);
            });
        },
        loadPageList: function (callback) {
            var that = this;
            this.getPageList(function (data) {
                var columns = {"UnitCode":"科室编码","UnitName":"科室名称","Leader":"负责人","DeptDesc":"描述"};
                that.renderTable(data.Data.UnitList, columns);
                var pageNumber = data.Data.TotalPage;
                if (pageNumber > 1) {
                    $("#pageContianer").html("<div id='kkpager' style='text-align:center'></div>");
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
                            //让  tbody的内容变成加载中的图标
                            var table = that.tableMap[that.status];
                            var length = table.find("thead th").length;
                            table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                            that.loadMoreData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);
                } else {
                    $('#kkpager').remove();
                }
            });
        },
        //获取数据
        getPageList: function (callback) {
            this.ajax({
                url: this.url,
                type: "get",
                data: {
                    action: "GetDept",
                    type: "Product",
                    customerId: this.customerId,
                    userId: this.userId,
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        if (page.d) {
                            page.d.close();
                        }
                        alert(data.Message);
                    }
                }
            });
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
        renderTable: function (dataList, colNames) {
            var table = this.tableMap[this.status];
            var tempHead = this.temp.thead[this.status];
            var tempBody = this.temp.tbody[this.status];
            table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
            //var headerObj = data.DicColNames;
            //var bodyList = data.SignUpList;
            var headerObj = colNames;
            var bodyList = dataList;
            //对应列名的对象    //未和列名对应的对象
            var finalList = [], otherItems = [];
            for (var i = 0; i < bodyList.length; i++) {
                var obj = {}, obj2 = {}, item = bodyList[i];
                for (var key in headerObj) {
                    obj[key] = item[key];
                }
                //把没有这个key的 给取出来
                for (var key2 in item) {
                    if (!headerObj.hasOwnProperty(key2)) {
                        obj2[key2] = item[key2];
                    }
                }
                otherItems.push(obj2);
                finalList.push(obj);
            }

            table.find("tbody").html(self.render(tempBody, { list: { finalList: finalList, otherItems: otherItems } }));

            //this.ele.tabMenu.find(".unsureTable em").html(data.TotalCountUn);
            //this.ele.tabMenu.find(".sureTable em").html(data.TotalCountYet);
        },
        render: function (temp, data) {
            var render = bd.template(temp, data);
            return render;
        }
    };

    self = page;

    page.init();
});