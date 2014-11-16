define(['jquery', 'tools', 'template', , 'kkpager'], function () {
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
                keyword: $("#keyword"),                                 //查询关键字
                btnQueryVip: $("#queryVip"),                           //查询按钮
                popDiv: $("#popDiv"),                                   //弹出层
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
            },
            authCode: true,
            clearInput: function () {

            },
            init: function () {
                var that = this;
                this.initEvent();
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
            initTables: function () {
                var that = this;
                $("#loading").show();
                //初始化当前页为0
                this.loadData.args.PageIndex = 0;
                //请求结果
                this.loadData.getCardVip(function (data) {
                    var list = data.Data.VipList;
                    list = list ? list : [];

                    var html = bd.template("tpl_content", { list: list })
                    $("#content").html(html);
                    $("#loading").hide();
                    if (data.Data.TotalPage < 1) {
                        $("#content").html("<p style='text-align:center'>暂无数据</p>");
                    }
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

                });
            },
            //加载更多的资讯或者活动
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex = currentPage - 1;
                this.loadData.getCardVip(function (data) {
                    var list = data.Data.VipList;
                    list = list ? list : [];

                    var html = bd.template("tpl_content", { list: list })
                    $("#content").html(html);
                    $("#loading").hide();
                });
            },
            //倒计时显示
            showTimer: function (id) {
                var count = 60;
                var that = this;
                this.timerId = setInterval(function () {
                    if (count > 0) {
                        --count;
                        $(id).val(count + "秒后发送");
                        $(id).css({ "width": "80px", "background": "gray" });
                    } else {
                        clearInterval(that.timerId);
                        $(id).val("发送");
                        //表示已经发送   1分钟后可以再次发送
                        that.authCode = true;
                        $(id).css({ "background": "#5FAFE4", "width": "70px" });
                    }
                }, 1000);
            },
            initEvent: function () {
                //初始化事件集
                var that = this;
                //查询会员
                this.elems.btnQueryVip.click(function (e) {
                    that.initTables();
                    that.stopBubble(e);
                });
                $("tbody").delegate(".btn-pay", "click", function (e) {
                    that.vipInfo = $(this).parent().parent().data("item");
                    page.loadData.args.VipID = $(this).parent().parent().data("item").VIPID
                    page.showCouponList();
                    that.stopBubble(e);
                });
                $(".btnWriteOff").live("click", (function (e) {
                    $("#tips").fadeIn();
                    page.loadData.args.CouponID= $(this).parent().parent().data("item").CouponID;
                    page.loadData.args.Comment=$(this).parent().parent().find("#Comment").val();
                    //alert(page.loadData.args.Comment);
//                    debugger;

                }));

                //点击完成的事件  则关闭遮罩层 进行核销
                $("#complet").click(function () {
                    $("#tips").fadeOut();
                    that.clearInput();
                    
                    //进行核销                    
                    that.loadData.writeOffCoupon(function (data) {

                        page.showCouponList();
                    });

                    that.stopBubble(e);
                });

                //分发
                $("#btnSure").click(function (e) {
                    var vipId = that.vipInfo.VIPID;
                    that.loadData.args.VipID = vipId;

                    var code = $("#couponCode").val();
                    if (code.length == 0) {
                        alert("优惠券编号不能为空!");
                        return;
                    }

                    //确认分发
                    that.loadData.bindCoupon(function (data) {
                        if (data.IsSuccess) {
                            alert("分发成功!");
                            that.initTables();
                        }
                        $(".hintClose").trigger("click");
                    });

                    that.stopBubble(e);
                });
                //关闭弹出层
                $(".hintClose").bind("click", function () {
                    that.elems.uiMask.slideUp();
                    $(this).parent().parent().fadeOut();
                });
            },
            showCouponList: function () {
                page.loadData.getCouponList(function (data) {
                    var list = data.Data.CouponList;
                    list = list ? list : [];

                    var html = bd.template("tpl_couponList", { list: list })
                    $("#couponListContent").html(html);
                    $("#loading").hide();

                    page.showElements(page.elems.popDiv);
                });
            }
        };

    page.loadData =
    {
        args: {
            PageIndex: 0,
            PageSize: 10,
            VipID: "",
            CouponID: "",
            Comment:""
        },
        getCardVip: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/Card/CardEntry.ashx",
                type: "post",
                data:
                {
                    'action': 'GetCardVip',
                    'PageSize': this.args.PageSize,
                    'PageIndex': this.args.PageIndex,
                    'Criterion': $("#name").val(),   //手机号/身份证/会员名
                    'CouponCode':$("#couponCode").val()
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
        //载入会员优惠券
        getCouponList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetCouponList',
                    'VipID': this.args.VipID ,               //会员ID
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else
                        alert(data.Message);
                }
            });
        },
        //核销优惠券
        writeOffCoupon: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WriteOffCoupon',
                    'CouponID': this.args.CouponID,
                    'Comment':this.args.Comment
                },
                success: function (data) {
                    alert(data.Message);
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }                        
                }
            });
        },
    }
    //初始化
    page.init();
});