define(['jquery', 'tools', 'template', 'kindeditor'], function () {
    var KE = window.KindEditor;

    var page =
        {
            url: "/ApplicationInterface/Coupon/CouponEntry.ashx",
            elems:
            {
                couponTypeText: $("#couponTypeText"),
                inOrderStatus: $("#statusText"),         //选择的渠道
                divStatus: $("#status"),                 //消费券
                divCouponType: $("#couponType"),
                btnGenerateCoupon: $("#generateCoupon"),            //确定按钮
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
                btnExportOut: $("#exportOut"),           //导出
                btnComplet: $("#complet")                 //完成

                , beginTime: $("#beginTime"),                 //开始时间
                endTime: $("#endTime")                     //结束时间
            },
            clearInput: function () {
                $("#statusText").html("渠道");
                that.elems.inOrderSource.data("status", "");
                $("#couponName").val("");
                $("#otherMoney").val("");
                $("#nums").val("");
            },
            init: function () {
                var that = this;
                //初始化渠道
                this.initData();
                //初始化消费券
                //this.initQuans();
                this.initEvent();

                //初始化kindeditor
                window.editor = KE.create('#description',
                {
                    uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx',
                    fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                    allowFileManager: true
                });

            },
            //初始化数据
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

            initEvent: function () {
                //初始化事件集
                var that = this;
                //选择状态事件
                this.elems.divStatus.delegate("span", "click", function () {
                    var $this = $(this);
                    $("#statusText").html($this.html());
                    $("#statusText").data("status", $this.data("status"));
                });
                //选择优惠券类型事件
                this.elems.divCouponType.delegate("span", "click", function () {
                    var $this = $(this);
                    that.elems.couponTypeText.html($this.html());
                    that.elems.couponTypeText.data("value", $this.data("value") ? $this.data("value") : "");
                });
                //点击完成的事件  则关闭遮罩层
                that.elems.btnComplet.click(function () {
                    that.elems.uiMask.slideUp();
                    $("#tips").fadeOut();
                    that.clearInput();
                });
                //导出卡
                that.elems.btnExportOut.click(function(){
                    //导出卡
//                    that.loadData.exportCard(function(data){
//                    
//                    });
                    var url=that.url+"?type=Product&action=ExportCard"+'&req={"Parameters":{"BatchID":"batchid"},'+'"random":'+Math.random()+'}';
                    url=url.replace("batchid",that.BatchID);
                    location.href=url;
                });

                //生成优惠券事件
                this.elems.btnGenerateCoupon.click(function () {
                    //优惠券类型
                    var couponTypeID = that.elems.couponTypeText.data("value");
                    if (!couponTypeID || couponTypeID == "") {
                        alert("请选择优惠券类型!");
                        that.elems.couponTypeText.focus();
                        return;
                    }
                    //优惠券名称
                    var couponName = $("#couponName").val();
                    if (couponName.length == 0) {
                        alert("优惠券名称不能为空!");
                        $("#couponName").focus();
                        return;
                    }

                    //优惠券数量
                    var nums = $("#nums").val();
                    if (nums.length == 0) {
                        alert("优惠券数量不能为空!");
                        $("#nums").focus();
                        return;
                    }
                    if (isNaN(parseInt(nums))) {
                        alert("卡数量只能输入数字!");
                        return;
                    }

                    //创建卡
                    that.loadData.generateCoupon(function(data){
                        $(".popup-mes").html("本次共生成新卡"+nums+"张");
                        that.elems.uiMask.fadeIn();
                        $("#tips").fadeIn();
                        that.BatchID=data.Data.BatchID;//批次号 用于导出卡号
                    });
                });

                //初始化日期控件
                var picker = new Pikaday(
                {
                    field: that.elems.beginTime[0],
                    format: "yyyy-MM-dd",
                    firstDay: 1,
                    minDate: new Date('2000-01-01'),
                    maxDate: new Date('2020-12-31'),
                    yearRange: [1900, 2050]
                });
                var picker2 = new Pikaday(
                {
                    field: that.elems.endTime[0],
                    format: "yyyy-MM-dd",
                    firstDay: 1,
                    minDate: new Date('2000-01-01'),
                    maxDate: new Date('2020-12-31'),
                    yearRange: [1900, 2050]
                });
            }
        };

    page.loadData =
    {
        //请求参数
        args: {
            PageIndex: 0,
            PageSize: 2
        },
        //获取初始数据
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
                },
                failure: function (a, b, c, d) {
                    debugger;
                }
            });
        },
        //导出excel接口
        exportCard:function(){
            var that=this;
            
            $.util.ajax({
                url: page.url,
                type: "get",
                data:
                {
                    'action': 'ExportCard',
                    'BatchID':page.BatchID
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
        //制作卡接口
        generateCoupon: function (callback) {
            var that=this;
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GenerateCoupon',
                    'CouponTypeID': page.elems.couponTypeText.data("value"),
                    'CouponName': $("#couponName").val(),
                    'BeginTime': $("#beginTime").val(),
                    'EndTime': $("#endTime").val(),
                    'Description': editor.html(),   //$("#description").val(),
                    'Qty':$("#nums").val(),
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