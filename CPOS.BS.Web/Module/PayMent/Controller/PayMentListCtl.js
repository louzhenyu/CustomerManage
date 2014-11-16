Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();
    JITPage.HandlerUrl.setValue("Handler/PayMentHander.ashx?mid=" + __mid);
    fnSearch();
});

function fnSearch() {

    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=getPayMentTypePage";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("pageBar").moveFirst();
}
function fnSetView(id, code, IsSave) {
    switch (code) {
        case "AlipayOffline": //支付宝Offline面支付
            fnView("支付宝线下支付", id, code, IsSave);
            break;
        case "CupWap": //银联wap
            var win = Ext.create('jit.biz.Window', {
                jitSize: "large",
                height: 400,
                id: "PayMentEdit",
                title: "创建支付通道",
                url: "PayMentWap.aspx?mid=" + __mid + "&paymentId=" + id + "&payCode=" + code + "&IsSave=" + IsSave
            });
            win.show();
            break;
        case "AlipayWap": //支付宝wap
            fnView("支付宝wap支付", id, code, IsSave);
            break;
        case "CupVoice": //银联语音
            var win = Ext.create('jit.biz.Window', {
                jitSize: "large",
                height: 400,
                id: "PayMentEdit",
                title: "创建支付通道",
                url: "PayMentAlipay.aspx?mid=" + __mid + "&paymentId=" + id + "&payCode=" + code + "&IsSave=" + IsSave
            });
            win.show();
            break;
        case "WXJS": //微信JS支付
            fnView("微信支付", id, code, IsSave);
            break;
        default:

    }
}

function fnView(title, paymentId, code, IsSave) {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 400,
        id: "PayMentEdit",
        title: "创建支付通道",
        url: "PayMentEdit.aspx?mid=" + __mid + "&paymentId=" + paymentId + "&payCode=" + code + "&IsSave=" + IsSave
    });
    win.show();
}
var AddPayChannelData = {}; //数据集合
var AddPayChannelList = new Array();
var AddPayChannel = {}; //数据集合
function fnDefault(id, code, ChannelId) {

    var paymentId = id;
    var paymentCode = code;
    if (this.ChannelId == "" || this.ChannelId == null) {
        this.ChannelId = 0;
    }
    if (paymentCode == "AlipayWap") {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "3";
        pay.PaymentTypeId = paymentId;
        pay.ChannelId = this.ChannelId;

        pay.WapData = WapData;
        AddPayChannelList[0] = pay;
    }

    //保存支付宝线下支付
    if (paymentCode == "AlipayOffline") {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "4";
        pay.ChannelId = this.ChannelId;
        pay.PaymentTypeId = paymentId;

        pay.WapData = WapData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //银联语音支付
    if (paymentCode == "CupVoice") {
        var UnionPayData = {}; //银联语音支付
        var pay = {};
        pay.PayType = "2";
        pay.ChannelId = this.ChannelId;
        pay.PaymentTypeId = paymentId;

        pay.UnionPayData = UnionPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }

    }

    //银联网页支付
    if (paymentCode == "CupWap") {
        var UnionPayData = {}; //银联网页支付
        var pay = {};
        pay.PayType = "1"
        pay.ChannelId = this.ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.UnionPayData = UnionPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //微信支付
    if (paymentCode == "WXJS") {
        var pay = {};
        var WxPayData = {};
        pay.PayType = "5";
        pay.ChannelId = this.ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.WxPayData = WxPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //消费者自助
    //add by donal 2014-10-16 14:51:29
    if (paymentCode == "CustomerSelfPay") {
        var pay = {};
        pay.PaymentTypeId = paymentId;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }
        
    //货到付款
    //add by donal 2014-10-16 14:51:45
    if (paymentCode == "GetToPay") {
        var pay = {};
        pay.PaymentTypeId = paymentId;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    /*
        by donal 2014-10-16 14:55:21       
       查看后台接口代码，发现后台接口只需要PaymentTypeId作为数据保存。
       以前上面的 if 去case 可能是有其他业务逻辑，或者开发者自己没搞清楚。
       后面加【消费者自助，货到付款】直接删减参数
       以后可以直接把上面所以的代码删掉，只做参数PaymentTypeId
    */


    AddPayChannelData.AddPayChannelData = AddPayChannelList
    AddPayChannel.Parameters = AddPayChannelData;

    Ext.Msg.confirm("请确认", "是否启用阿拉丁支付？", function (button) {
        if (button == "yes") {
            url: JITPage.HandlerUrl.getValue() + "&method=SetDefaultPayChannel",
               Ext.Ajax.request({
                   method: 'POST',
                   sync: true,
                   async: false,
                   url: '/ApplicationInterface/PayChannel/PayChannelGateway.ashx?type=Product&action=SetDefaultPayChannel',
                   params: {
                       "req": Ext.encode(AddPayChannel)
                   },
                   method: 'POST',
                   success: function (result, request) {
                       var d = Ext.decode(result.responseText);
                       if (d.ResultCode == "0") {
                           showError("阿拉丁支付设置成功：");
                           AddPayChannelData = {}; //数据集合
                           AddPayChannelList = new Array();
                           AddPayChannel = {};
                           fnSearch();
                       } else {
                           showSuccess("阿拉丁支付设置失败" + d.Message);
                           AddPayChannelData = {}; //数据集合
                           AddPayChannelList = new Array();
                       }
                   },
                   failure: function (result) {
                       AddPayChannelData = {}; //数据集合
                       AddPayChannelList = new Array();
                       showError("阿拉丁支付设置失败：" + result.responseText);
                   }
               });
        }
    });

}

function fnDisble(PaymentTypeID) {
    Ext.Msg.confirm("请确认", "是否停用支付通道？", function (button) {
        if (button == "yes") {
            url: JITPage.HandlerUrl.getValue() + "&method=disablePayment",
               Ext.Ajax.request({
                   method: 'POST',
                   sync: true,
                   async: false,
                   url: 'Handler/PayMentHander.ashx?type=Product&method=disablePayment',
                   params: {
                       "paymentTypeId": PaymentTypeID
                   },
                   method: 'POST',
                   success: function (result, request) {
                       var d = Ext.decode(result.responseText);
                       if (d.ResultCode == "0") {
                           showError("支付通道停用成功：");
                           AddPayChannelData = {}; //数据集合
                           AddPayChannelList = new Array();
                           AddPayChannel = {};
                           fnSearch();
                       } else {
                           showSuccess("支付通道停用失败" + d.Message);
                           AddPayChannelData = {}; //数据集合
                           AddPayChannelList = new Array();
                       }
                   },
                   failure: function (result) {
                       AddPayChannelData = {}; //数据集合
                       AddPayChannelList = new Array();
                       showError("支付通道停用置失败：" + result.responseText);
                   }
               });
        }
    });

}
