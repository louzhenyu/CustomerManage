var K;
var htmlEditor;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    //InitStore();
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
                //取返回值,注意后台设置的key,如果要取原值
                //取缩略图地址
                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
                //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(data.thumUrl));

                //取原图地址
                //var url = KE.formatUrl(data.url, 'absolute');
                Ext.getCmp("txtImageUrl").setValue(getStr(data.url));
                get("imgPre").src = data.url;
                get("imgPre").style.display = "";
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
    JITPage.HandlerUrl.setValue("Handler/LEventsEntriesHandler.ashx?mid=");

    //设置默认发布时间为当天
    var d = new Date();
    var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    Ext.getCmp("txtWorkDate").setValue(dateNow);

    var EntriesId = new String(JITMethod.getUrlParam("EntriesId"));
    if (EntriesId != "null" && EntriesId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_LEventsEntries_by_id",
            params: {
                id: EntriesId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;
                if (d != null) {
                    Ext.getCmp("txtWorkTitle").jitSetValue(d.WorkTitle);
                    Ext.getCmp("txtWorkDate").setValue(getStr(d.WorkDate));
                    Ext.getCmp("txtCreative").setValue(getStr(d.Creative));
                    Ext.getCmp("txtPhone").setValue(getStr(d.Phone));
                    Ext.getCmp("txtCreativeAddress").setValue(getStr(d.CreativeAddress));
                    Ext.getCmp("txtDisplayIndex").setValue(getStr(d.DisplayIndex));
                    Ext.getCmp("txtImageUrl").setValue(getStr(d.WorkUrl));
                    if (d.WorkUrl != null) {
                        get("imgPre").src = getStr(d.WorkUrl);
                        get("imgPre").style.display = "";
                    }
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
    CloseWin('LEventsEntriesEdit');
}

function fnSave() {
    var news = {};
    news.EventId = "AC1DFF316EE44E72B11BB416A641E726";
    news.EntriesId = getUrlParam("EntriesId");
    news.WorkTitle = Ext.getCmp("txtWorkTitle").getValue();
    news.Creative = Ext.getCmp("txtCreative").getValue();
    news.WorkDate = Ext.getCmp("txtWorkDate").jitGetValueText();
    news.Phone = Ext.getCmp("txtPhone").getValue();
    news.CreativeAddress = Ext.getCmp("txtCreativeAddress").getValue();
    news.DisplayIndex = Ext.getCmp("txtDisplayIndex").getValue();
    news.WorkUrl = Ext.getCmp("txtImageUrl").getValue();
    //news.IsWorkDaren = 0;
    //news.IsMonthDaren = 0;

    if (news.WorkDate == null || news.WorkDate == "") {
        showError("必须输入作品日期");
        return;
    }

    if (news.WorkTitle == null || news.WorkTitle == "") {
        showError("必须输入作品名称");
        return;
    }
    if (news.Creative == null || news.Creative == "") {
        showError("必须输入作者名称");
        return;
    }

    if (news.WorkUrl == null || news.WorkUrl == "") {
        showError("必须输入作品图片");
        return;
    }


    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/LEventsEntriesHandler.ashx?method=LEventsEntries_save&id=' + news.EntriesId,
        params: {
            "item": Ext.encode(news)
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

