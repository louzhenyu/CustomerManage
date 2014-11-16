var K;
var htmlEditor, htmlEditor2, htmlEditor3, htmlEditor4, htmlEditor5, htmlEditor6, htmlEditor7, htmlEditor8, htmlEditor9, htmlEditor10;

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
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");

    //设置默认发布时间为当天
    var d = new Date();
    var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    Ext.getCmp("txtStartDate").setValue(dateNow);
    Ext.getCmp("txtEndDate").setValue(dateNow);

    htmlEditor.html('');
    htmlEditor2.html('');
    htmlEditor3.html('');
    htmlEditor4.html('');
    htmlEditor5.html('');
    htmlEditor6.html('');
    htmlEditor7.html('');
    htmlEditor8.html('');
    htmlEditor9.html('');
    htmlEditor10.html('');

    
    document.getElementById("pnl2").style.display = "none";
    document.getElementById("pnl3").style.display = "none";
    document.getElementById("pnl4").style.display = "none";
    document.getElementById("pnl5").style.display = "none";
    document.getElementById("pnl6").style.display = "none";
    document.getElementById("pnl7").style.display = "none";
    document.getElementById("pnl8").style.display = "none";
    document.getElementById("pnl9").style.display = "none";
    document.getElementById("pnl10").style.display = "none";
    
    var ForumId = new String(JITMethod.getUrlParam("ForumId"));
    if (ForumId != "null" && ForumId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_events_by_id",
            params: {
                ForumId: ForumId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                //if (d.ParentForumId != null && d.ParentForumId.length > 0) {
                //    Ext.getCmp("txtParentEvent").jitSetValue([{ "id": d.ParentForumId, "text": d.ParentEventTitle}]);
                //}
                Ext.getCmp("txtCity").setValue(getStr(d.City));
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

                Ext.getCmp("txtForumType").jitSetValue(d.ForumTypeIds);
                Ext.getCmp("txtCourse").jitSetValue(d.CourseIds);

                Ext.getCmp("txtEmail").jitSetValue(getStr(d.Email));
                Ext.getCmp("txtEmailTitle").jitSetValue(getStr(d.EmailTitle));
                Ext.getCmp("txtImageUrl").jitSetValue(getStr(d.ImageUrl));
                Ext.getCmp("txtIsSignUp").setDefaultValue(getStr(d.IsSignUp));
                
                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.Desc));
                
                htmlEditor2.html('');
                htmlEditor2.insertHtml(getStr(d.Organizer));
                
                htmlEditor3.html('');
                htmlEditor3.insertHtml(getStr(d.Schedule));
                
                htmlEditor4.html('');
                htmlEditor4.insertHtml(getStr(d.Food));
                
                htmlEditor5.html('');
                htmlEditor5.insertHtml(getStr(d.Sponsor));
                
                htmlEditor6.html('');
                htmlEditor6.insertHtml(getStr(d.Roundtable));
                
                htmlEditor7.html('');
                htmlEditor7.insertHtml(getStr(d.Speakers));
                
                htmlEditor8.html('');
                htmlEditor8.insertHtml(getStr(d.PreviousForum));
                
                htmlEditor9.html('');
                htmlEditor9.insertHtml(getStr(d.ContactUs));
                
                htmlEditor10.html('');
                htmlEditor10.insertHtml(getStr(d.Register));

                myMask.hide();

                document.getElementById("pnl1").style.display = "";
                document.getElementById("pnl2").style.display = "none";
                document.getElementById("pnl3").style.display = "none";
                document.getElementById("pnl4").style.display = "none";
                document.getElementById("pnl5").style.display = "none";
                document.getElementById("pnl6").style.display = "none";
                document.getElementById("pnl7").style.display = "none";
                document.getElementById("pnl8").style.display = "none";
                document.getElementById("pnl9").style.display = "none";
                document.getElementById("pnl10").style.display = "none";
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

    events.ForumId = getUrlParam("ForumId");
    events.ForumTypeIds = Ext.getCmp("txtForumType").jitGetValue();
    events.CourseIds = Ext.getCmp("txtCourse").jitGetValue();

    events.City = Ext.getCmp("txtCity").getValue();
    events.Title = Ext.getCmp("txtTitle").getValue();

    events.StartTimeText = Ext.getCmp("txtStartDate").jitGetValueText();
    events.EndTimeText = Ext.getCmp("txtEndDate").jitGetValueText();
    
    events.StartTimePart = Ext.getCmp("txtStartTime").getValue();
    events.EndTimePart = Ext.getCmp("txtEndTime").getValue();

    events.StartTimeText += " " + events.StartTimePart + ":00";
    events.EndTimeText += " " + events.EndTimePart + ":00";
    
    events.Email = Ext.getCmp("txtEmail").getValue();
    events.EmailTitle = Ext.getCmp("txtEmailTitle").getValue();

    events.ImageUrl = Ext.getCmp("txtImageUrl").getValue();
    if (events.ImageUrl.length == 0) events.ImageUrl = null;
    events.IsSignUp = Ext.getCmp("txtIsSignUp").getValue();

    events.Desc = htmlEditor.html();
    events.Organizer = htmlEditor2.html();
    events.Schedule = htmlEditor3.html();
    events.Food = htmlEditor4.html();
    events.Sponsor = htmlEditor5.html();
    events.Roundtable = htmlEditor6.html();
    events.Speakers = htmlEditor7.html();
    events.PreviousForum = htmlEditor8.html();
    events.ContactUs = htmlEditor9.html();
    events.Register = htmlEditor10.html();

    if (events.Title == null || events.Title == "") {
        showError("必须输入标题");
        return;
    }

    if (events.City == null || events.City == "") {
        showError("必须输入城市");
        return;
    }
    if (events.Email == null || events.Email == "") {
        showError("必须输入报名邮件");
        return;
    }
    if (events.EmailTitle == null || events.EmailTitle == "") {
        showError("必须输入报名邮件抬头");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventsHandler.ashx?method=events_save&ForumId=' + events.ForumId,
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

fnChange = function(type) {
    document.getElementById("btn1").className = "z_tb2";
    document.getElementById("btn2").className = "z_tb2";
    document.getElementById("btn3").className = "z_tb2";
    document.getElementById("btn4").className = "z_tb2";
    document.getElementById("btn5").className = "z_tb2";
    document.getElementById("btn6").className = "z_tb2";
    document.getElementById("btn7").className = "z_tb2";
    document.getElementById("btn8").className = "z_tb2";
    document.getElementById("btn9").className = "z_tb2";
    document.getElementById("btn10").className = "z_tb2";
    
    document.getElementById("pnl1").style.display = "none";
    document.getElementById("pnl2").style.display = "none";
    document.getElementById("pnl3").style.display = "none";
    document.getElementById("pnl4").style.display = "none";
    document.getElementById("pnl5").style.display = "none";
    document.getElementById("pnl6").style.display = "none";
    document.getElementById("pnl7").style.display = "none";
    document.getElementById("pnl8").style.display = "none";
    document.getElementById("pnl9").style.display = "none";
    document.getElementById("pnl10").style.display = "none";

    document.getElementById("btn" + type).className = "z_tb1";
    document.getElementById("pnl" + type).style.display = "";
}

