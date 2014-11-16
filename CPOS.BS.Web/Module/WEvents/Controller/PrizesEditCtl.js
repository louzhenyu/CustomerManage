var K;
var htmlEditor;
var K2;
var htmlEditor2;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();

    //上传图片
    KE = KindEditor;
    var uploadbutton = KE.uploadbutton({
        button: KE('#uploadImage')[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');
                ////取返回值,注意后台设置的key,如果要取原值
                ////取缩略图地址
                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
                //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(thumUrl));

                //取原图地址
                var url = data.url;
                Ext.getCmp("txtImageUrl").setValue(getStr(url));
            } else {
                alert(data.message);
            }
        },
        afterError: function (str) {
            alert('自定义错误信息: ' + str);
        }
    });
    uploadbutton.fileBox.change(function (e) {
        uploadbutton.submit();
    });

    //上传图片2
    KE2 = KindEditor;
    var uploadbutton2 = KE2.uploadbutton({
        button: KE2('#uploadLogo')[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');
                ////取返回值,注意后台设置的key,如果要取原值
                ////取缩略图地址
                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
                //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(thumUrl));

                //取原图地址
                var url = data.url;
                Ext.getCmp("txtLogoURL").setValue(getStr(url));
            } else {
                alert(data.message);
            }
        },
        afterError: function (str) {
            alert('自定义错误信息: ' + str);
        }
    });
    uploadbutton2.fileBox.change(function (e) {
        uploadbutton2.submit();
    });

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");
    fnCouponTypeLoad();
    //htmlEditor.html('');

    var PrizesID = new String(JITMethod.getUrlParam("PrizesID"));
    if (PrizesID != "null" && PrizesID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_prizes_by_id",
            params: {
                PrizesID: PrizesID
            },
            method: 'post',
            success: function (response) {
                var storeId = "eventsEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtPrizeName").setValue(getStr(d.PrizeName));
                Ext.getCmp("txtPrizeShortDesc").setValue(getStr(d.PrizeShortDesc));
                Ext.getCmp("txtPrizeDesc").setValue(getStr(d.PrizeDesc));
                Ext.getCmp("txtLogoURL").setValue(getStr(d.LogoURL));
                Ext.getCmp("txtImageUrl").setValue(getStr(d.ImageUrl));
                Ext.getCmp("txtContentText").setValue(getStr(d.ContentText));
                Ext.getCmp("txtContentUrl").setValue(getStr(d.ContentUrl));
                Ext.getCmp("txtPrice").setValue(getStr(d.Price == null ? 0 : d.Price));
                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.DisplayIndex));
                Ext.getCmp("txtCountTotal").setValue(getStr(d.CountTotal));
                Ext.getCmp("txtCountLeft").setValue(getStr(d.CountLeft));
                if (d.IsAutoPrizes=="1") {
                    Ext.getCmp("IsAutoPrizes").jitSetValue(true);
                }

                debugger;
                var type = d.PrizeTypeId;
                Ext.getCmp("txtPrizeType").jitSetValue(d.PrizeTypeId.toString());
                switch (type) {
                    case 2:
                        $("#lblCouponType").show();
                        $("#tCouponType").show();
                        Ext.getCmp("txtCouponType").jitSetValue(d.CouponTypeID);
                        break;
                    case 3:
                        $("#tPoint").show();
                        $("#lblPoint").show();
                        Ext.getCmp("txtPoint").jitSetValue(d.Point);
                        break;
                    case 4:
                        $("#lblMoney").show();
                        $("#tMoney").show();
                        Ext.getCmp("txtMoney").setValue(d.Price);
                        break;
                    default:

                }

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        myMask.hide();
    }
});

function fnClose() {
    CloseWin('PrizesEdit');
}

function fnSave() {
    var events = {};

    events.EventID = getUrlParam("EventID");
    events.PrizesID = getUrlParam("PrizesID");
    events.PrizeName = Ext.getCmp("txtPrizeName").getValue();
    events.PrizeShortDesc = Ext.getCmp("txtPrizeShortDesc").getValue();
    events.PrizeDesc = Ext.getCmp("txtPrizeDesc").getValue();
    events.LogoURL = Ext.getCmp("txtLogoURL").getValue();
    events.ImageUrl = Ext.getCmp("txtImageUrl").getValue();
    events.ContentText = Ext.getCmp("txtContentText").getValue();
    events.ContentUrl = Ext.getCmp("txtContentUrl").getValue();
    events.Price = Ext.getCmp("txtPrice").getValue();
    events.DisplayIndex = Ext.getCmp("txtDisplayIndex").getValue();
    events.CountTotal = Ext.getCmp("txtCountTotal").getValue();
    events.CountLeft = Ext.getCmp("txtCountLeft").getValue();
    var IsAutoPrizes = Ext.getCmp("IsAutoPrizes").value;
    if (IsAutoPrizes) {
        events.IsAutoPrizes = 1;
    }
    else {
        events.IsAutoPrizes = 0;
    }


    if (events.PrizeName == null || events.PrizeName == "") {
        showError("必须输入奖品名称");
        return;
    }
    if (events.CountTotal == null || events.CountTotal == "") {
        showError("必须输入总数量");
        return;
    }
    var type = Ext.getCmp("txtPrizeType").jitGetValue();
    if (type == null) {
        showError("必须选择类型");
        return;
    }
    if (type == "1") {
        events.PrizeTypeId = "1"
        events.Point = 0;
    }
    else if (type == "2") {
        var CouponType = Ext.getCmp("txtCouponType").jitGetValue();
        if (CouponType == null) {
            showError("必须选择优惠劵");
            return;
        }
        events.Point = 0;
        events.PrizeTypeId = "2";
        events.CouponTypeID = CouponType;
    }
    else if (type == "3") {
        var Point = Ext.getCmp("txtPoint").jitGetValue();
        if (Point == "" || Point == null || parseInt(Point) <= 0) {
            showError("积分不能为空且大于0");
            return;
        }
        events.PrizeTypeId = "3"
        events.Point = Point;
    }
    else if (type == "4") {
        var price = Ext.getCmp("txtMoney").getValue();
        if (price == "" || price == null || parseInt(price) <= 0) {
            showError("现金不能为空且大于0");
            return;
        }
        events.PrizeTypeId = "4"
        events.Price = price;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventsHandler.ashx?method=prizes_save&PrizesID=' + events.PrizesID,
        params: {
            "events": Ext.encode(events)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch(getUrlParam("EventID"));
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    if (flag) fnClose();
}

function fnselectPrizeType() {
    var type = Ext.getCmp("txtPrizeType").jitGetValue();
    switch (type) {
        case "1":
            $("#tPoint").hide();
            $("#lblPoint").hide();
            $("#lblCouponType").hide();
            $("#tCouponType").hide();
            $("#lblMoney").hide();
            $("#tMoney").hide();
            break;
        case "2": //优惠劵
            $("#tPoint").hide();
            $("#lblPoint").hide();
            $("#lblCouponType").show();
            $("#tCouponType").show();//优惠券显示 
            $("#lblMoney").hide();
            $("#tMoney").hide();
            break;
        case "3":
            $("#tPoint").show();
            $("#lblPoint").show();
            $("#lblCouponType").hide();
            $("#tCouponType").hide();
            $("#lblMoney").hide();
            $("#tMoney").hide();
            break;
        case "4":
            $("#tPoint").hide();
            $("#lblPoint").hide();
            $("#lblCouponType").hide();
            $("#tCouponType").hide();
            $("#lblMoney").show();
            $("#tMoney").show();
            break;
        default:

    }

}

function fnCouponTypeLoad() {
    debugger;
    Ext.getStore("CouponTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=getCouponType";
    Ext.getStore("CouponTypeStore").load();

}



