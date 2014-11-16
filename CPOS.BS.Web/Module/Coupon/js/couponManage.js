define(['jquery', 'tools', 'template', 'kkpager'], function () {
    Date.prototype.format = function (format) {
        var o =
        {
            "M+": this.getMonth() + 1, //month
            "d+": this.getDate(),    //day
            "h+": this.getHours(),   //hour
            "m+": this.getMinutes(), //minute
            "s+": this.getSeconds(), //second
            "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
            "S": this.getMilliseconds() //millisecond
        }
        if (/(y+)/.test(format))
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(format))
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        return format;
    }
    var page =
        {
            pageSize: 2,
            //默认控制条数
            currentPage: 0,
            //图文素材ID
            url: "/ApplicationInterface/Coupon/CouponEntry.ashx",
            //关联到的类别
            elems:
            {
                searchBtn: $("#searchBtn"),                //搜索按钮
//有效期注释 2014-9-26                 dateTime: $("#dateTime"),       //日期
//                beginDate: $("#date-begin"),                 //开始时间
//                endDate: $("#date-end"),                     //结束时间

                divCouponType: $("#couponType"),            //优惠券类型
                couponTypeText: $("#couponTypeText"),       //优惠券类型内容

                source: $("#source"),                        //渠道
                sourceText: $("#sourceText"),                //渠道内容
                useStatus: $("#useStatus"),                  //使用状态
                useStatusText: $("#useStatusText"),          //使用状态的内容
                cardStatus: $("#cardStatus"),                //卡状态
                cardStatusText: $("#cardStatusText"),        //卡内容
                tabQueryResult: $("#queryResult"),       //表格结果
                popTableResult: $("#popTableResult"),
                status: $("#status"),                    //状态
                //选择活动详情的下拉框
                uiMask: $("#ui-mask")
            },
            clearInput: function () {

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
            //初始化提现日期内容
            initTime: function () {
                var date = new Date();
                //当前日期
                var currentDate = date.format("yyyy-MM-dd");
                var list = [];
                list[0] = {
                    date: currentDate + "/" + currentDate,
                    dateName: "今天"
                };

                var startDate = new Date(); // 开始日期
                //昨天
                startDate.setDate(date.getDate() - 1);
                var yesterday = startDate.format("yyyy-MM-dd");
                list[1] = {
                    date: yesterday + "/" + yesterday,
                    dateName: "昨天"
                }

                var startDate2 = new Date(); // 开始日期
                var endDate2 = new Date(); // 结束日期
                var day = date.getDate(); // 取当日
                var week = date.getDay(); // 取当日是周几
                startDate2.setDate(day - week);
                endDate2.setDate(day + (6 - week));
                list[2] = {
                    date: startDate2.format("yyyy-MM-dd") + "/" + endDate2.format("yyyy-MM-dd"),
                    dateName: "本周"
                }

                var startDate3 = new Date(); // 开始日期
                var endDate3 = new Date(); // 结束日期
                var year = date.getFullYear();
                var month = date.getMonth() + 1;
                endDate3 = new Date(year + "-" + (month + 1) + "-01");
                startDate3.setDate(1);
                endDate3.setDate(0);
                list[3] = {
                    date: startDate3.format("yyyy-MM-dd") + "/" + endDate3.format("yyyy-MM-dd"),
                    dateName: "本月"
                }
                var html = bd.template("tpl_date", { list: list });
                this.elems.dateTime.html(html);
            },
            init: function () {
                var that = this;

//有效期注释 2014-9-26                 this.initTime();
                //初始化渠道

                this.initData();
                this.initTables();

//有效期注释 2014-9-26                 var picker = new Pikaday(
//                {
//                    field: that.elems.beginDate[0],
//                    format: "yyyy-MM-dd",
//                    firstDay: 1,
//                    minDate: new Date('2000-01-01'),
//                    maxDate: new Date('2020-12-31'),
//                    yearRange: [1900, 2050]
//                });
//                var picker2 = new Pikaday(
//                {
//                    field: that.elems.endDate[0],
//                    format: "yyyy-MM-dd",
//                    firstDay: 1,
//                    minDate: new Date('2000-01-01'),
//                    maxDate: new Date('2020-12-31'),
//                    yearRange: [1900, 2050]
//                });
                this.initEvent();
            },
            //初始化渠道
            initData: function () {
                var that = this;
                this.loadData.getData(this.initCouponType);
            },
            //初始化消费券类型
            initCouponType: function (data) {
                var list = data.data;
                var html = bd.template("tpl_couponType", { list: list });
                page.elems.divCouponType.html(html);
            },
            initTables: function () {
                var that = this;
                $("#loading").show();
                //渠道ID
                var channelId = this.elems.sourceText.data("status");
                this.loadData.args.ChannelID = channelId;
                //idfrom
                var idFrom = $("#idFrom").val();
                this.loadData.args.CardNoStart = idFrom;
                var idTo = $("#idTo").val();
                this.loadData.args.CardNoEnd = idTo;
                //使用状态
                var useStatus = that.elems.useStatusText.data("value");
                this.loadData.args.UseStatus = useStatus;
                //卡状态
                var cardStatus = that.elems.cardStatusText.data("status");
                this.loadData.args.CouponStatus = cardStatus;
                //自定义类型
                this.loadData.args.DateRange = 5;
                //金额
                var intoMoney = $("#intoMoney").val();
                this.loadData.args.Amount = intoMoney;
//有效期注释 2014-9-26                 //制卡日期
//                var date = $("#dateTimeText").data("day");

                //优惠券名称
                var couponName = $("#couponName").val();
                this.loadData.args.CouponName = couponName;

                //优惠券类型
                var couponTypeID = that.elems.couponTypeText.data("value");
                this.loadData.args.CouponTypeID = couponTypeID;

                //优惠券编号
                var couponCode = $("#couponCode").val();
                this.loadData.args.CouponCode = couponCode;

// 有效期注释 2014-9-26                //获取阶段值
//                if (date == 999) {

//                    this.loadData.args.BeginTime = this.elems.beginDate.val() ? this.elems.beginDate.val() : this.loadData.args.BeginDate;
//                    this.loadData.args.EndTime = this.elems.endDate.val() ? this.elems.endDate.val() : this.loadData.args.EndDate;
//                } else {
//                    this.loadData.args.BeginTime = date.split("/")[0];
//                    this.loadData.args.EndTime = date.split("/")[1];
//                }
                //初始化当前页为0
                this.loadData.args.PageIndex = 0;

                //请求结果
                this.loadData.manageCouponSearch(function (data) {
                    var list = data.Data.CouponList;
                    list = list ? list : [];

                    var html = bd.template("tpl_content", { list: list })
                    $("#content").html(html);
                    $("#loading").hide();
                    //$("#appendPage").append("<div id='kkpager'></div>");
                    var total = data.Data.TotalPage;
                    if (data.Data.TotalPage >= 1) {
                        that.selector = "#list1";
                        kkpager.generPageHtml({
                            pno: 1,
                            mode: 'click', //设置为click模式
                            //总页码  
                            total: total,
                            isShowTotalPage: true,
                            isShowTotalRecords: false,
                            //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                            //适用于不刷新页面，比如ajax
                            click: function (n) {
                                //这里可以做自已的处理
                                this.init({ total: total });
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
                    } else {
                        $("#content").html("<p style='text-align:center'>暂无数据</p>");
                    }

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
            //显示弹层
            showElements: function (selector) {
                this.elems.uiMask.show();
                $(selector).show();
            },
            hideElements: function (selector) {
                this.elems.uiMask.fadeIn(500);
                $(selector).fadeIn(500);
            },
            //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex = currentPage - 1;
                this.loadData.manageCouponSearch(function (data) {
                    var list = data.Data.CouponList;
                    list = list ? list : [];

                    var html = bd.template("tpl_content", { list: list })
                    $("#content").html(html);
                    $("#loading").hide();
                    //触发全选按钮事件
                    if ($("#selAll").hasClass("on")) {
                        $("#selAll").removeClass("on");
                        $("#selAll").trigger("click");
                    }
                });
            },
            initEvent: function () {
                //初始化事件集
                var that = this;

                //选择优惠券类型事件
                this.elems.divCouponType.delegate("span", "click", function () {
                    var $this = $(this);
                    that.elems.couponTypeText.html($this.html());
                    that.elems.couponTypeText.data("value", $this.data("value") ? $this.data("value") : "");
                });

                //流水号 关键字查询事件  按下enter
                $("#intoMoney").keydown(function (e) {
                    if (e.keyCode == 13) {
                        that.initTables();
                    }
                });
                //关键字查询事件
                this.elems.searchBtn.click(function () {
                    //删除分页
                    that.initTables();
                });
                //选择渠道
                this.elems.source.delegate("span", "click", function () {
                    var $this = $(this);

                    that.elems.sourceText.html($this.html());
                    that.elems.sourceText.data("status", $this.data("status"))
                });

                //选择使用状态
                this.elems.useStatus.delegate("span", "click", function () {
                    var $this = $(this);
                    that.elems.useStatusText.html($this.html());
                    that.elems.useStatusText.data("value", $this.data("value"))
                });
                //选中每行
                $("tbody").delegate(".toSel", "click", function (e) {
                    var $this = $(this);
                    if ($this.hasClass("on")) {
                        $this.removeClass("on");
                    } else {
                        $this.addClass("on");
                    }
                    that.stopBubble(e);
                });
                //全选
                $("#selAll").click(function (e) {
                    var $this = $(this);
                    if ($this.hasClass("on")) {
                        $this.removeClass("on");
                        $(".toSel").removeClass("on");
                    } else {
                        $this.addClass("on");
                        $(".toSel").addClass("on");
                    }

                });
                //选择卡状态
                this.elems.cardStatus.delegate("span", "click", function () {
                    var $this = $(this);
                    that.elems.cardStatusText.html($this.html());
                    that.elems.cardStatusText.data("status", $this.data("status"))
                });
                //选择提现时间事件
//有效期注释 2014-9-26                this.elems.dateTime.delegate("span", "click", function () {
//                    var $this = $(this);
//                    $("#dateTimeText").html($this.html());
//                    var day = $this.data("day");
//                    //设置日期
//                    $("#dateTimeText").data("day", day);
//                    //让选择日期的空间不能再选择
//                    if (day != "" && day == 999) {
//                        $("#timeBetwwen").show();
//                    } else {
//                        $("#timeBetwwen").hide();
//                        //时间段显示

//                    }
//                });
                //可以弹出单个设置的层
                this.elems.tabQueryResult.on("mouseover", ".last", function () {
                    that.elems.tabQueryResult.find(".set-layer").hide();
                    $(this).find(".set-layer").show();
                }).on("mouseout", ".last", function () {
                    $(this).find(".set-layer").hide();
                });
                //状态更新
                $("tbody").delegate(".set-layer span", "click", function (e) {
                    var $this = $(this);

                    //获得id
                    var itemId = $this.parent().parent().data("itemid");

                    var newStatus = $this.data("status");
                    var text = $this.data("text");
                    that.loadData.setCard.CardIds = [itemId];
                    that.loadData.setCard.IsDelete = newStatus;

                    //有on点击则进行取消
                    if (!$this.hasClass("on")) {
                        //更新卡状态
                        that.loadData.setCouponStates(function (data) {
                            //状态修改
                            $this.parent().parent().find("span").removeClass("on");
                            $this.addClass("on");
                            $this.parent().parent().parent().find("a").html(text);

                        });


                    }
                    that.stopBubble(e);

                });
                //关闭弹出层
                $(".hintClose").bind("click", function (e) {
                    that.elems.uiMask.slideUp();
                    $(this).parent().parent().parent().fadeOut();
                    that.stopBubble(e);
                });
                //选择状态事件
                this.elems.status.delegate("span", "click", function (e) {
                    var $this = $(this);
                    $("#statusText").html($this.html());
                    $("#statusText").data("status", $this.data("status"));
                    that.stopBubble(e);
                });
                //批量更新卡
                $("#btnUpdateCards").click(function (e) {
                    var cardIds = [];
                    $("tbody").find(".toSel").each(function (i, o) {
                        var $this = $(this);
                        if ($this.hasClass("on")) {
                            var cardId = $this.data("cardid");
                            cardIds.push(cardId);
                        }
                    });
                    if (cardIds.length == 0) {
                        alert("请至少选中一行内容再进行批量操作!!");
                    } else {
                        that.loadData.setCard.CardIds = cardIds;
                        $("#cardCount").html(cardIds.length);
                        //弹出更新层
                        that.showElements("#popUpdateDiv");
                    }
                    that.stopBubble(e);
                });
                //确定批量更新卡
                $("#sureUpdate").click(function (e) {
                    that.loadData.setCouponCode(function (data) {
                        //alert("更新成功!");
                        //重新请求一次数据
                        //alert(1);
                        that.initTables();
                        $(".hintClose").trigger("click");
                    });
                    that.stopBubble(e);
                });


                //批量选择卡状态
                $("#setCouponCode").delegate("span", "click", function (e) {
                    var $this = $(this);
                    $("#setCardStatusText").html($this.html());
                    $("#setCardStatusText").data("status", $this.data("status"));
                    that.stopBubble(e);
                });


                //更新优惠券编号 点击事件
                $(".updateCode").live("click", that.showUpdateCode);
            }
            , showUpdateCode: function () {
                $("#updateCouponCodeText").text($(this).data("couponcode"));
                page.loadData.args.CouponID = $(this).data("couponid");
                page.showElements("#popUpdateDiv");
            }
        };

    page.loadData =
    {
        args: {
            PageIndex: 0,
            PageSize: 10,
            UseStatus: "",
            CouponID: "",
            CouponTypeID: "",
            CouponName: "",
            CouponStatus: "",
            BeginTime: "",
            EndTime: "",
            CouponCode: ""
        },
        setCard: {
            CardIds: [],
            IsDelete: "",
            CouponCode: "-11"
        },
        //获取渠道
        getData: function (callback) {
            $.ajax({
                url: "../WEvents/Handler/EventsHandler.ashx?method=getCouponType",
                type: "post",
                success: function (data) {
                    data = JSON.parse(data);
                    if (data.success && callback) {
                        callback(data);
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得优惠券列表
        manageCouponSearch: function (callback) {

            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'ManageCouponPagedSearch',
                    'PageIndex': this.args.PageIndex,
                    'PageSize': this.args.PageSize,
                    'CouponUseStatus': this.args.UseStatus,
                    'CouponTypeID': this.args.CouponTypeID,
                    'CouponName': this.args.CouponName,
                    'CouponStatus': this.args.CouponStatus,
                    "BeginTime": this.args.BeginTime,
                    "EndTime": this.args.EndTime,
                    "CouponCode": this.args.CouponCode
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
        //更新
        setCouponCode: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'SetCouponCode',
                    'CouponID': this.args.CouponID,
                    'CouponCode': $("#updateCouponCodeText").val()
                },
                success: function (data) {
                    alert(data.Message)
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                }
            });
        },
        //更改优惠券状态
        setCouponStates: function (callback) {

            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'SetCouponStates',
                    'CouponIDs': this.setCard.CardIds,
                    'IsDelete': this.setCard.IsDelete,
                    'CouponCode': this.setCard.CouponCode,
                    'CouponID': '0'
                },
                success: function (data) {
                    //alert(data.Message)
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                }
            });
        },

        //根据提现ID获得入现列表
        getEntryList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'getCustomerOrderPayPage',
                    'PageSize': 10,
                    'PageIndex': this.args.PageIndex,
                    'WithdrawalId': this.args.WithdrawalId   //提现ID
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