define(['newJquery', 'tools', 'template'], function () {
    var page =
        {
            url: "/ApplicationInterface/Card/CardEntry.ashx",
            elems:
            {
                inOrderSource: $("#sourceText"),       //渠道
                inOrderStatus: $("#statusText"),         //选择的渠道
                divStatus: $("#status"),                 //消费券
                divSource: $("#source"),                 //渠道
                btnMakeCard: $("#makeCard"),            //确定按钮
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
                btnExportOut: $("#exportOut"),           //导出
                btnComplet: $("#complet")                 //完成
            },
            clearInput: function () {
                $("#statusText").html("渠道");
                that.elems.inOrderSource.data("status", "");
                $("#intoMoney").val("");
                $("#otherMoney").val("");
                $("#nums").val("");
            },
            init: function () {
                var that = this;
                //初始化渠道
                this.initSource();
                //初始化消费券
                //this.initQuans();
                this.initEvent();
            },
            //初始化来源
            initSource: function () {
                var that = this;
                this.loadData.getSource(function (data) {
                    var list = data.Data.ChannelList;
                    var html = bd.template("tpl_source", { list: list })
                    that.elems.divSource.html(html);
                });

            },
            //初始化消费券
            initQuans: function () {
                var that = this;
                this.loadData.getQuans(function (data) {
                    var list = data.Data.listOptionStatus;
                    var html = bd.template("tpl_status", { list: list });
                    that.elems.divStatus.html(html);
                });

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
                //选择来源事件
                this.elems.divSource.delegate("span", "click", function () {
                    var $this = $(this);
                    that.elems.inOrderSource.html($this.html());
                    
                    that.elems.inOrderSource.data("status", $this.data("status"));
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

                //创建卡事件
                this.elems.btnMakeCard.click(function () {
                    //渠道
                    var source = that.elems.inOrderSource.data("status");
                    if (source <= 0) {
                        alert("请选择渠道!");
                        return;
                    }
                    //充值金额
                    var intoMoney = $("#intoMoney").val();
                    if (intoMoney.length == 0) {
                        alert("充入金额不能为空!");
                        return;
                    }
                    if (isNaN(parseInt(intoMoney))) {
                        alert("充入金额只能是数字!");
                        return;
                    }
                    //赠送金额
                    var otherMoney = $("#otherMoney").val();
                    if (isNaN(parseInt(otherMoney))) {
                        alert("赠送金额只能输入数字");
                        return;
                    }
                    //消费券
                    var quan = $("#statusText").data("status");
                    //制卡数量
                    var nums = $("#nums").val();
                    if (nums.length == 0) {
                        alert("制卡数量不能为空!");
                        return;
                    }
                    if (isNaN(parseInt(nums))) {
                        alert("卡数量只能输入数字!");
                        return;
                    }
                    //创建卡
                    that.loadData.makeCard(function(data){
                        $(".popup-mes").html("本次共生成新卡"+nums+"张");
                        that.elems.uiMask.fadeIn();
                        $("#tips").fadeIn();
                        that.BatchID=data.Data.BatchID;//批次号 用于导出卡号
                    });
                   
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
        getQuans: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetOptionsStatus',
                    'statusName': 'CustomerOrderPayStatus'
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
        makeCard: function (callback) {
            var that=this;
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'MakeCard',
                    'ChannelID':  page.elems.inOrderSource.data("status"),  //channelId
                    'Amount':$("#intoMoney").val(),//充值金额
                    'Bonus':$("#otherMoney").val(),
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
        },
        //获取渠道
        getSource: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'GetChannel'
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