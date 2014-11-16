var __data = null;
Jit.AM.defindPage({
    name: 'OrderDetail',
    onPageLoad: function () {
        Jit.log('进入OrderDetail.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemDetail',
                'storeId': me.getUrlParam('storeId'),  //店ID
                'itemId': me.getUrlParam('itemId'),    //商品标识
                'beginDate': me.getParams('InDate'), //开始日期
                'endDate': me.getParams('InDate')  //结束日期
            },
            success: function (data) {
                var returnData = data.content;

                $('[fvalue=storeName]').html(returnData.storeInfo.storeName); //酒店名
                $('[fvalue=itemName]').html(returnData.itemName); //房型
                me.setParams("storeName",returnData.itemName);    //设置门店名称
                $('[fvalue=inDate]').html(me.getParams('InDate')?me.getParams('InDate'):"未选择日期"); //房型
                $('[fvalue=outDate]').html((me.getParams('appointmentTime')?me.getParams('appointmentTime'):"未写预约时间")); //房型
                __data = returnData;
                JitPage.fnCalcAomunt(returnData); //计算金额
                JitPage.fnGetWeekData(returnData); //生成每日 数据
            }
        });
    },
    fnAdd: function () {
        var num = parseInt($("#goods_number").html());
        if (num > 0) {
            num++;
            $("#goods_number").html(num);
        } else {
            $("#goods_number").html(1);
        }
        JitPage.fnCalcAomunt(__data); //计算金额
    },
    fnSub: function () {
       	var num = parseInt($("#goods_number").html());
        if (num > 1) {
            num--;
            $("#goods_number").html(num);
        } else {
            $("#goods_number").html(1);
        }
        JitPage.fnCalcAomunt(__data); //计算金额
    },
    fnCalcAomunt: function (data) {
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
    },
    fnGetWeekData: function (data) {
        var $tr = $('<tr></tr>');
        var num = $("#goods_number").val();
        
        if (num > 0 &&data.storeItemDailyStatus&& data.storeItemDailyStatus.length > 0) {
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
    },
    fnSubmit: function () {
        
        var me = this;
        var intRegular = /^[0-9]*[1-9][0-9]*$/;
        var phoneRegular = /^(1(([35][0-9])|(47)|[8][012356789]))\d{8}$/;
        //组装订单数据
        var houseList = new Array();
        var num = parseInt($("#goods_number").html());
        var allAomunt = 0;
        var storeId = me.getUrlParam('storeId');  //店ID

        var txtPhone = $("#txtPhone").val();      //手机号
        var txtUserName = $("#txtUserName").val(); //用户名

        if (!txtUserName) {
            Jit.UI.Dialog({
                'content': '姓名不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        if (!txtPhone) {
            Jit.UI.Dialog({
                'content': '请填写手机号！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        if (!txtPhone.match(phoneRegular)) {
            Jit.UI.Dialog({
                'content': '联系电话格式不正确,请重新输入！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        if (!intRegular.test(num)) {
            Jit.UI.Dialog({
                'content': '人数格式不正确,请重新输入！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }

        if (num > 0) {
            var beginDate = endDate = new Date();
            if(__data.storeItemDailyStatus){
	            for (var i = 0; i < __data.storeItemDailyStatus.length; i++) {        
	                if (__data.storeItemDailyStatus[i].LowestPrice) {
	                    allAomunt += __data.storeItemDailyStatus[i].LowestPrice * num;
	                }
	            }
	        }
            beginDate = JitPage.formatDate(new Date(__data.storeItemDailyStatus[0].StatusDate));
            endDate = JitPage.formatDate(new Date(__data.storeItemDailyStatus[__data.storeItemDailyStatus.length - 1].StatusDate));
            var appointmentTime=me.getParams("appointmentTime");
            /*根据开始 结束日期 只传一条数据*/
            houseList.push({ skuId: __data.storeItemDailyStatus[0].SkuID, salesPrice: __data.storeItemDailyStatus[0].LowestPrice, qty: num, beginDate: beginDate, endDate: endDate,appointmentTime:appointmentTime});
        
        }
        if (txtUserName && txtPhone && houseList) {
            me.ajax({
                url: '/OnlineShopping/data/Data.aspx',
                data: {
                    'action': 'setOrderInfo',
                    'mobile': txtPhone,
                    'username': txtUserName,
                    'storeId': storeId,
                    'totalAmount': allAomunt,
                    'orderDetailList': houseList
                },
                beforeSend: function () {
                    Jit.UI.Masklayer.show();
                },
                success: function (data) {
                    Jit.UI.Masklayer.hide();
                    if (data && data.code == 200) {
                    	console.log(data);
                    	var orderId=data.content.orderId;
                       //预约成功后跳转到成功页面
                       Jit.AM.toPage('OrderSuccess',"&orderId="+orderId);
                    } else {
                        Jit.UI.Dialog({
                            'content': data.description,
                            'type': 'Alert',
                            'CallBackOk': function () {
                                Jit.UI.Dialog('CLOSE');
                            }
                        });
                        return false;
                    }
                }
            });
        }
    },
    formatDate: function (day) {
        var Year = 0;
        var Month = 0;
        var Day = 0;
        var CurrentDate = "";
        Year = day.getFullYear(); //ie火狐下都可以 
        Month = day.getMonth() + 1;
        Day = day.getDate();
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