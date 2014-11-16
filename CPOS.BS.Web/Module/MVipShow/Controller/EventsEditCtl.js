var K;
var htmlEditor;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();
    
    ////上传图片
    //KE = KindEditor;
    //var uploadbutton = KE.uploadbutton({ 
    //    button: KE('#uploadImage')[0],
    //    //上传的文件类型
    //    fieldName: 'imgFile',
    //    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
    //    url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
    //    afterUpload: function (data) {
    //        if (data.error === 0) {
    //            alert('图片上传成功');
    //            ////取返回值,注意后台设置的key,如果要取原值
    //            ////取缩略图地址
    //            //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
    //            //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(thumUrl));

    //            //取原图地址
    //            var url = data.url;
    //            Ext.getCmp("txtImageUrl").setValue(getStr(url));
    //        } else {
    //            alert(data.message);
    //        }
    //    },
    //    afterError: function (str) {
    //        alert('自定义错误信息: ' + str);
    //    }
    //});
    //uploadbutton.fileBox.change(function (e) {
    //    uploadbutton.submit();
    //});

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");

    ////设置默认发布时间为当天
    //var d = new Date();
    //var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    //Ext.getCmp("txtStartDate").setValue(dateNow);
    //Ext.getCmp("txtEndDate").setValue(dateNow);

    htmlEditor.html('');
    
    var EventID = new String(JITMethod.getUrlParam("EventID"));
    if (EventID != "null" && EventID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_events_by_id",
            params: {
                EventID: EventID
            },
            method: 'post',
            success: function (response) {
                var storeId = "eventsEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtVipName").setValue(getStr(d.VipName));
                Ext.getCmp("txtPraiseCount").setValue(getStr(d.PraiseCount));
                Ext.getCmp("txtCreateTime").setValue(getDate(d.CreateTime));
                Ext.getCmp("txtLotteryCode").setValue(getStr(d.LotteryCode));

                var passText = d.IsCheck == "1" ? "通过" : "未通过";
                Ext.getCmp("txtPass").setValue(passText);

                Ext.getCmp("txtItemCode").setValue(getStr(d.ItemName));
                get("hItemId").value = getStr(d.ItemId);
                
                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.Experience));

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
    CloseWin('EventsEdit');
}

function fnSave() {
    var events = {};

    events.VipShowId = getUrlParam("EventID");

    //events.Experience = Ext.getCmp("txtExperience").getValue();

    events.Experience = htmlEditor.html();
    events.ItemId = getStr(get("hItemId").value);
    
    //events.ApplyQuesID = Ext.getCmp("txtApplyQues").getValue();
    //events.PollQuesID = Ext.getCmp("txtPollQues").getValue();

    //if (events.EventType == null || events.EventType == "") {
    //    showError("必须选择类型");
    //    return;
    //}

    //if (events.Title == null || events.Title == "") {
    //    showError("必须输入标题");
    //    return;
    //}

    //if (events.CityId == null || events.CityId == "") {
    //    showError("必须选择城市");
    //    return;
    //}
    //if (events.Address == null || events.Address == "") {
    //    showError("必须输入地址");
    //    return;
    //}
    //if (events.Description == null || events.Description == "") {
    //    showError("必须输入描述");
    //    return;
    //}
    //if (events.CheckinRange == null || events.CheckinRange == "") {
    //    showError("必须选择签到范围");
    //    return;
    //}
    //if (events.CheckinPriv == null || events.CheckinPriv == "") {
    //    showError("必须选择签到类型");
    //    return;
    //}

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventsHandler.ashx?method=events_save&EventID=' + events.VipShowId,
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
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    if (flag) fnClose();
}

