var __data = null;
Jit.AM.defindPage({
    /*定义页面名称 必须和config文件中设置的属性一直*/
    name: 'OrderDetail',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入OrderDetail.....');
        this.initEvent();
    }, initEvent: function () {
        var me = this;
        //数据请求
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemDetail',
                'storeId': me.getUrlParam('storeId'),  //店ID
                'itemId': me.getUrlParam('itemId'),    //商品标识
                'beginDate': me.getUrlParam('InDate'), //开始日期
                'endDate': me.getUrlParam('OutDate')   //结束日期
            },
            success: function (data) {
                //debugger;
                var returnData = data.content;

                $('[fvalue=storeName]').html("酒店：" + returnData.storeInfo.storeName); //酒店名
                $('[fvalue=itemName]').html("房型：" + returnData.itemName); //房型
                $('[fvalue=inDate]').html("入住时间：" + me.getUrlParam('InDate')); //房型
                $('[fvalue=outDate]').html("离店时间：" + me.getUrlParam('OutDate')); //房型
                __data = returnData;
                JitPage.fnCalcAomunt(returnData); //计算金额
                JitPage.fnGetWeekData(returnData); //生成每日 数据
            }
        });
    }, fnAdd: function () {
        var num = $("#goods_number").val();
        if (num > 0) {
            num++;
            $("#goods_number").val(num);
        } else {
            $("#goods_number").val(1);
        }
        JitPage.fnCalcAomunt(__data); //计算金额
    }, fnSub: function () {
        var num = $("#goods_number").val();
        if (num > 1) {
            num--;
            $("#goods_number").val(num);
        } else {
            $("#goods_number").val(1);
        }
        JitPage.fnCalcAomunt(__data); //计算金额
    }, fnCalcAomunt: function (data) {
        //debugger;
        var num = $("#goods_number").val();
        var allAomunt = 0;
        if (num > 0) {
            $.each(data.storeItemDailyStatus, function (i, v) {
                if (v.LowestPrice) {
                    allAomunt += v.LowestPrice * num;
                }
            });
        }
        $("[fvalue=num]").html("X" + num);
        $("[fvalue=number]").html(num);
        $("[fvalue=amount]").html(allAomunt);
    }, fnGetWeekData: function (data) {
        var $tr = $('<tr></tr>');
        var num = $("#goods_number").val();
        //debugger;
        if (num > 0 && data.storeItemDailyStatus.length > 0) {
            var date = new Date(data.storeItemDailyStatus[0].StatusDate);  //时间
            var day = date.getDay();
            var alllength = 0;
            for (var i = 0; i < day; i++) {
                alllength++;
                $('<td class="tdline"></td>').html("").appendTo($tr);
            }

            $.each(data.storeItemDailyStatus, function (i, v) {
                if (alllength % 7 == 0) {
                    $("#tblWeek").append($tr);
                    $tr = $('<tr></tr>');
                }
                alllength++;
                if (v.LowestPrice > 0) {
                    $('<td class="tdline"></td>').html("<em>¥" + v.LowestPrice + "</em><br /><em fvalue='num' >X" + num + "</em>").appendTo($tr);
                }
            });

            if (alllength % 7 > 0) {
                var residueNum = (parseInt(alllength / 7) + 1) * 7 - alllength;
                for (var j = 0; j < residueNum; j++) {
                    $('<td class="tdline"></td>').html("").appendTo($tr);
                }
                $("#tblWeek").append($tr);
            } else {
                $("#tblWeek").append($tr);
            }



        }
    }
    , fnSubmit: function () {
        var me = this;
        //组装订单数据
        var houseList = new Array();
        var num = $("#goods_number").val();
        var allAomunt = 0;
        var storeId = me.getUrlParam('storeId');  //店ID

        var txtPhone = $("#txtPhone").val();      //手机号
        var txtUserName = $("#txtUserName").val(); //用户名

        if (!txtUserName) {
            alert("入住人姓名不能为空");
            return false;
        }
        if (!txtPhone) {
            alert("手机号不能为空");
            return false;
        }

        //        if (!txtPhone) {
        //            Jit.UI.Dialog({
        //                'content': '请填写手机号！',
        //                'type': 'Alert',
        //                'CallBackOk': function () {
        //                    Jit.UI.Dialog('CLOSE');
        //                }
        //            });
        //            return;
        //        }
        //        if (!txtUserName) {
        //            Jit.UI.Dialog({
        //                'content': '请填写入住人姓名！',
        //                'type': 'Alert',
        //                'CallBackOk': function () {
        //                    Jit.UI.Dialog('CLOSE');
        //                }
        //            });
        //            return;
        //        }

        if (num > 0) {
            $.each(__data.storeItemDailyStatus, function (i, v) {
                if (v.LowestPrice) {
                    allAomunt += v.LowestPrice * num;
                    var fDatetime = JitPage.formatDate(new Date(v.StatusDate));
                    houseList.push({ skuId: v.SkuID
                                    , salesPrice: v.LowestPrice
                                    , qty: num
                                    , beginDate: fDatetime
                                    , endDate: fDatetime
                    });
                }
            });
        }
        if (txtUserName && txtPhone && houseList) {
            me.ajax({
                url: '../../../OnlineShopping/data/Data.aspx',
                data: {
                    'action': 'setOrderInfo',
                    'mobile': txtPhone,
                    'username': txtUserName,
                    'storeId': storeId,
                    'totalAmount': allAomunt,
                    'orderDetailList': houseList
                },
                success: function (data) {
                    if (data && data.code == 200) {
                        alert("订单提交成功！");


                    } else {
                        alert(data.description);
                    }
                }
            });
        } else {

        }
    }, formatDate: function (day) {
        var Year = 0;
        var Month = 0;
        var Day = 0;
        var CurrentDate = "";
        //初始化时间 
        //Year= day.getYear();//有火狐下2008年显示108的bug 
        Year = day.getFullYear(); //ie火狐下都可以 
        Month = day.getMonth() + 1;
        Day = day.getDate();
        //Hour = day.getHours(); 
        // Minute = day.getMinutes(); 
        // Second = day.getSeconds(); 
        CurrentDate += Year + "-";
        if (Month >= 10) {
            CurrentDate += Month + "-";
        }
        else {
            CurrentDate += "0" + Month + "-";
        }
        if (Day >= 10) {
            CurrentDate += Day;
        }
        else {
            CurrentDate += "0" + Day;
        }
        return CurrentDate;
    }
});