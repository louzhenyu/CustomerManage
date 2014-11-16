var K;
var htmlEditor;

var objTags;
var LEventsAlbumId;
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/LEventsAlbumHandler.ashx?mid=" + __mid);

    LEventsAlbumId = new String(JITMethod.getUrlParam("AlbumId"));

    fnLoadType();


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
                //取原图地址          
                Ext.getCmp("txtImageUrl").setValue(getStr(data.url));
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



    //设置默认发布时间为当天 
    if (LEventsAlbumId != "null" && LEventsAlbumId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetLEventsAlbumById",
            params: {
                LEventsAlbumId: LEventsAlbumId
            },
            method: 'post',
            success: function (response) {
                var storeId = "newsEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;
                Ext.getCmp("txtNewsTitle").jitSetValue(getStr(d.Title));
                Ext.getCmp("txtImageUrl").setValue(getStr(d.ImageUrl));
                Ext.getCmp("txt_Intro").setValue(getStr(d.Intro));
                Ext.getCmp("txtVideoUrl").setValue(getStr(d.Description));
                Ext.getCmp("txtSortOrder").setValue(getStr(d.SortOrder));
                Ext.getCmp("txtNewsType").jitSetValue(getStr(d.ModuleType));
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
    CloseWin('NewsEdit');
}

function fnLoadType() {
    Ext.getStore("lNewsTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetlNewsType";
    Ext.getStore("lNewsTypeStore").load();

}

function fnUpdateVideo() {
    var fileName = Ext.getCmp("file-pathID").getValue();
    if (fileName == "" || fileName == null) {
        Ext.Msg.show({
            title: "提示",
            msg: "请选择上传MP4文件",
            minWidth: 200,
            modal: true,
            icon: Ext.Msg.INFO,
            buttons: Ext.Msg.OK,
            fn: function () {
                return;
            }
        });
    }
    else {
        var filesuffix = fileName.substring(fileName.lastIndexOf("."));
        if (filesuffix.toLowerCase() == ".mp4") {
            var myMask_info = "loading...";
            var myMask = new Ext.LoadMask(Ext.getBody(), {
                msg: myMask_info
            });
            myMask.show();
            Ext.getCmp("fileForm").getForm().submit({
                method: 'POST',
                timeout: 360000,
                url: 'Handler/LEventsAlbumHandler.ashx?method=UploadVideo',
                params: {

            },
            success: function (fp, o) {
                try {
                    var d;
                    d = o.result;
                    if (d.success == false) {
                        showError("上传数据失败：" + d.msg);

                    } else {
                        showSuccess("上传成功");
                        Ext.getCmp("txtVideoUrl").jitSetValue(d.msg);
                    }
                    myMask.hide();
                } catch (e) {
                    showSuccess("上传失败,最大可上传100m视频文件");
                    myMask.hide();
                }
            },
            failure: function (fp, o) {
                showError("上传数据失败：" + o.result);
                myMask.hide();
            }
        });
    } else {
        Ext.Msg.show({
            title: "提示",
            msg: "请选择上传MP4文件",
            minWidth: 200,
            modal: true,
            icon: Ext.Msg.INFO,
            buttons: Ext.Msg.OK,
            fn: function () {
                return;
            }
        });
    }
}
}

function fnSave() {
    var news = {};
    news.LEventsAlbumId = LEventsAlbumId;
    news.ModuleType = Ext.getCmp("txtNewsType").jitGetValue();
    news.ModuleName = Ext.getCmp("txtNewsType").rawValue;
    news.Title = Ext.getCmp("txtNewsTitle").jitGetValue();
    news.ImageUrl = Ext.getCmp("txtImageUrl").jitGetValue();
    news.Intro = Ext.getCmp("txt_Intro").jitGetValue();
    news.Description = Ext.getCmp("txtVideoUrl").jitGetValue();
    news.SortOrder = Ext.getCmp("txtSortOrder").jitGetValue();

    if (news.Intro != null && news.Intro.length > 500) {
        showError("简介不能超过500字符");
        return;
    }

    if (news.Title == null || news.Title == "") {
        showError("必须输入视频标题");
        return;
    }
    if (news.ModuleType == null || news.ModuleType == "") {
        showError("必须输入视频类型");
        return;
    }
    var flag = true;
    Ext.getCmp("fileForm").getForm().submit({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/LEventsAlbumHandler.ashx?method=SaveLEventsAlbum&LEventsAlbumId=' + news.LEventsAlbumId,
        params: {
            "LEventsAlbum": Ext.encode(news)
        },
        success: function (fp, o) {
            var d = o.result;
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);

            } else {
                showSuccess("保存数据成功");
                fnClose();
                parent.fnSearch();

            }
        },
        failure: function (fp, o) {
            showError("保存数据失败：" + o.result);
        }
    });

}

