var K;
var htmlEditor;
var t_unit_id = "";

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

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WEventsHandler.ashx?mid=");

    //设置默认发布时间为当天
    var d = new Date();
    var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    Ext.getCmp("txtStartDate").setValue(dateNow);
    Ext.getCmp("txtEndDate").setValue(dateNow);

    htmlEditor.html('');

    Ext.getCmp("cmbIsDefault").setWidth(100);
    Ext.getCmp("cmbIsTop").setWidth(100);
    
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

                if (d.ParentEventID != null && d.ParentEventID.length > 0) {
                    Ext.getCmp("txtParentEvent").jitSetValue([{ "id": d.ParentEventID, "text": d.ParentEventTitle}]);
                }
                Ext.getCmp("txtCityId").setValue(getStr(d.CityID));
                Ext.getCmp("txtTitle").setValue(getStr(d.Title));

                var startTime = toDate(d.BeginTime);
                var endTime = toDate(d.EndTime);
                Ext.getCmp("txtStartDate").setValue(startTime);
                Ext.getCmp("txtEndDate").setValue(endTime);
                if (startTime != undefined && startTime != null&& typeof(startTime) != "string") {
                    Ext.getCmp("txtStartTime").setValue(
                        (getDateHour(startTime) < 10 ? "0" + getDateHour(startTime) : getDateHour(startTime)) + ":" + 
                        (startTime.getMinutes() < 10 ? "0" + startTime.getMinutes() : startTime.getMinutes()));
                }
                if (endTime != undefined && endTime != null && typeof(endTime) != "string") {
                    Ext.getCmp("txtEndTime").setValue(
                        (getDateHour(endTime) < 10 ? "0" + getDateHour(endTime) : getDateHour(endTime)) + ":" + 
                        (endTime.getMinutes() < 10 ? "0" + endTime.getMinutes() : endTime.getMinutes()));
                }

                Ext.getCmp("txtImageUrl").setValue(getStr(d.ImageURL));
                Ext.getCmp("txtUrl").setValue(getStr(d.URL));

                Ext.getCmp("cmbIsDefault").jitSetValue(getStr(d.IsDefault));
                Ext.getCmp("cmbIsTop").jitSetValue(getStr(d.IsTop));
                Ext.getCmp("txtOrganizer").setValue(getStr(d.Organizer));

                Ext.getCmp("txtAddress").setValue(getStr(d.Address));
                Ext.getCmp("txtContact").setValue(getStr(d.Content));
                Ext.getCmp("txtPhoneNumber").setValue(getStr(d.PhoneNumber));
                Ext.getCmp("txtEmail").setValue(getStr(d.Email));

                Ext.getCmp("txtWeiXinPublic").fnLoad(function(){
                    Ext.getCmp("txtWeiXinPublic").jitSetValue(getStr(d.WeiXinID));
                    Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                    Ext.getCmp("txtWXCode").setDefaultValue(getStr(d.QRCodeTypeId));
                    Ext.getCmp("txtWXCode2").jitSetValue(getStr(d.WXCode));
                });

                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.Description));
                
                //Ext.getCmp("txtCheckinRange").setDefaultValue(getStr(d.CheckinRange));
                //Ext.getCmp("txtCheckinType").setDefaultValue(getStr(d.CheckinPriv));

                Ext.getCmp("txtApplyQues").setDefaultValue(getStr(d.ApplyQuesID));
                Ext.getCmp("txtPollQues").setDefaultValue(getStr(d.PollQuesID));
                Ext.getCmp("txtWEventAdmin").setDefaultValue(getStr(d.EventManagerUserId));
                
                Ext.getCmp("txtLongitude").jitSetValue(getStr(d.Longitude));
                Ext.getCmp("txtLatitude").jitSetValue(getStr(d.Latitude));
                Ext.getCmp("txtEventStatus").setDefaultValue(getStr(d.EventStatus));
                Ext.getCmp("txtDisplayIndex").jitSetValue(getStr(d.DisplayIndex));
                Ext.getCmp("txtPersonCount").jitSetValue(getStr(d.PersonCount));
                //Jermyn20140102
//                alert(d.WXCode)
//                alert(d.WXCodeImageUrl)
                //Ext.getCmp("txtWXCode").setValue(getStr(d.WXCode));
                Ext.getCmp("txtDimensionalCodeURL").setValue(getStr(d.WXCodeImageUrl));
                document.getElementById("imgView").src = d.WXCodeImageUrl;

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

    events.EventID = getUrlParam("EventID"); 
    if (events.EventID == undefined || events.EventID == null || events.EventID.length == 0) events.EventID = t_unit_id;
    if (events.EventID == undefined || events.EventID == null || events.EventID.length == 0) events.EventID = newGuid();
    events.ParentEventID = Ext.getCmp("txtParentEvent").jitGetValue();
    events.IsSubEvent = 0;

    if (events.ParentEventID == "root" || events.ParentEventID == "-1") {
        events.ParentEventID = null;
    }
    if (events.ParentEventID != null && events.ParentEventID.length > 0) {
        events.IsSubEvent = 1;
    }

    events.CityId = Ext.getCmp("txtCityId").getValue();
    events.Title = Ext.getCmp("txtTitle").getValue();

    events.StartTimeText = Ext.getCmp("txtStartDate").jitGetValueText();
    events.EndTimeText = Ext.getCmp("txtEndDate").jitGetValueText();
    
    events.StartTimePart = Ext.getCmp("txtStartTime").getValue();
    events.EndTimePart = Ext.getCmp("txtEndTime").getValue();

    if (events.StartTimePart != "" && parseInt(events.StartTimePart.split(":")[0]) >= 24) {
        events.StartTimePart = "00:00";
        var date = new Date(events.StartTimeText);
        var date2 = new Date(events.StartTimeText);
        date2.setDate(date.getDate()+1);
        events.StartTimeText = getDateStr(date2);
    }
    if (events.EndTimePart != "" && parseInt(events.EndTimePart.split(":")[0]) >= 24) {
        events.EndTimePart = "00:00";
        var date = new Date(events.EndTimeText);
        var date2 = new Date(events.EndTimeText);
        date2.setDate(date.getDate()+1);
        events.EndTimeText = getDateStr(date2);
    }
    
    events.StartTimeText += " " + events.StartTimePart + ":00";
    events.EndTimeText += " " + events.EndTimePart + ":00";

    events.Content = Ext.getCmp("txtContact").getValue();
    events.PhoneNumber = Ext.getCmp("txtPhoneNumber").getValue();

    events.IsDefault = Ext.getCmp("cmbIsDefault").jitGetValue();
    events.IsTop = Ext.getCmp("cmbIsTop").jitGetValue();
    events.Organizer = Ext.getCmp("txtOrganizer").getValue();

    events.Address = Ext.getCmp("txtAddress").getValue();
    events.Email = Ext.getCmp("txtEmail").getValue();
    events.WeiXinID = Ext.getCmp("txtWeiXinPublic").jitGetValue();
    events.ModelId = Ext.getCmp("txtModelId").jitGetValue();

    events.ImageUrl = Ext.getCmp("txtImageUrl").getValue();
    if (events.ImageUrl.length == 0) events.ImageUrl = null;

    events.Description = htmlEditor.html();
    
    //events.CheckinRange = Ext.getCmp("txtCheckinRange").getValue();
    //events.CheckinPriv = Ext.getCmp("txtCheckinType").getValue();
    
    events.ApplyQuesID = Ext.getCmp("txtApplyQues").jitGetValue();
    events.PollQuesID = Ext.getCmp("txtPollQues").jitGetValue();
    events.EventManagerUserId = Ext.getCmp("txtWEventAdmin").jitGetValue();
    
    events.URL = Ext.getCmp("txtUrl").jitGetValue();
    events.Longitude = Ext.getCmp("txtLongitude").jitGetValue();
    events.Latitude = Ext.getCmp("txtLatitude").jitGetValue();
    events.EventStatus = Ext.getCmp("txtEventStatus").jitGetValue();
    events.DisplayIndex = Ext.getCmp("txtDisplayIndex").jitGetValue();
    events.PersonCount = Ext.getCmp("txtPersonCount").jitGetValue();
    events.WXCode = Ext.getCmp("txtWXCode").jitGetValue();
    events.WXCodeImageUrl = Ext.getCmp("txtDimensionalCodeURL").jitGetValue();
    //if (events.EventType == null || events.EventType == "") {
    //    showError("必须选择类型");
    //    return;
    //}

    if (events.Title == null || events.Title == "") {
        showError("必须输入标题");
        return;
    }

    if (events.CityId == null || events.CityId == "") {
        showError("必须选择城市");
        return;
    }
    if (events.Address == null || events.Address == "") {
        showError("必须输入地址");
        return;
    }
    if (events.Description == null || events.Description == "") {
        showError("必须输入描述");
        return;
    }
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
        url: 'Handler/WEventsHandler.ashx?method=events_save&EventID=' + events.EventID,
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

function fnGetWXCode() {

    var EventID = new String(JITMethod.getUrlParam("EventID")); ;
    var WXCode = Ext.getCmp("txtWXCode").jitGetValue();
    var WeiXinId = Ext.getCmp("txtWeiXinPublic").jitGetValue();
    if (EventID == undefined || EventID == null || EventID.length == 0) {
        EventID = newGuid();
        t_unit_id = EventID;
    }
//    alert(EventID);
//    alert(WXCode);
//    alert(WeiXinId);
    if (WXCode == null || WXCode == "") {
        alert("固定二维码类型不能为空");
        return;
    }
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        async: false,
        url: '/Module/WEventsData/Handler/WEventsHandler.ashx?method=SetEventWXCode&WXCode=' + WXCode + '&WeiXinId=' + WeiXinId + '&EventID=' + EventID,
        //params: { "unit": Ext.encode(unit) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                document.getElementById("txtWXCode").value = d.status;
                Ext.getCmp("txtDimensionalCodeURL").jitSetValue(getStr(d.msg));
                document.getElementById("imgView").src = d.msg;
                Ext.getCmp("txtWXCode2").jitSetValue(getStr(d.data));
                showSuccess("生成二维码成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}
