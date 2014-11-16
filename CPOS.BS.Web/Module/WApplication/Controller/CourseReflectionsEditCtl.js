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
    
    htmlEditor.html('');

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
                var url = data.url;
                //var url = KE.formatUrl(data.url, 'absolute');
                Ext.getCmp("txtImageUrl").setValue(getStr(data.url));
//                Ext.getCmp("txtImageFormat").setValue(getStr(data.url.substring(data.url.lastIndexOf(".") + 1)));
//                var req = $.ajax({ type: "HEAD", url: url,
//                    success: function () {
//                        Ext.getCmp("txtImageSize").setValue(getStr(req.getResponseHeader("Content-Length")));
//                    }
//                });

//                document.getElementById("imgView").src = data.url;
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
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");

    ////设置默认发布时间为当天
    //var d = new Date();
    //var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    //Ext.getCmp("txtPublishTime").setValue(dateNow);

    var NewsId = new String(JITMethod.getUrlParam("ReflectionsId"));
    if (NewsId != "null" && NewsId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_Course_Reflections_by_id",
            params: {
                ReflectionsId: NewsId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtStudentName").jitSetValue(d.StudentName);
                Ext.getCmp("txtStudentPost").setValue(getStr(d.StudentPost));
                Ext.getCmp("txtVideoURL").setValue(getStr(d.VideoURL));

                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.Content));

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
    CloseWin('CourseReflectionsEdit');
}

function fnSave() {
    var news = {};
    
    news.ReflectionsId = getUrlParam("ReflectionsId");
    news.CourseId = getUrlParam("CourseId");
    news.StudentName = Ext.getCmp("txtStudentName").getValue();
    news.StudentPost = Ext.getCmp("txtStudentPost").getValue();
    news.Content = htmlEditor.html(); ;
    news.VideoURL = Ext.getCmp("txtVideoURL").getValue();
    news.ImageUrl = Ext.getCmp("txtImageUrl").jitGetValue();

    //if (news.NewsType == null || news.NewsType == "") {
    //    showError("必须选择新闻类型");
    //    return;
    //}

    //if (news.NewsTitle == null || news.NewsTitle == "") {
    //    showError("必须输入新闻标题");
    //    return;
    //}

    //if (news.PublishTime == null || news.PublishTime == "") {
    //    showError("必须选择发布时间");
    //    return;
    //}

    //if (news.Content == null || news.Content == "") {
    //    showError("必须输入新闻内容");
    //    return;
    //}

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/WApplicationHandler.ashx?method=Course_Reflections_save&ReflectionsId=' + news.ReflectionsId,
        params: {
            "news": Ext.encode(news),
            CourseId: getUrlParam("CourseId")
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

