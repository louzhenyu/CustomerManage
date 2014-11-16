var K;
var htmlEditor;

var objTags;
var uploadImage;
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/NewsHandler.ashx?mid=");

    var NewsId = new String(JITMethod.getUrlParam("NewsId"));

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetNewsTags",
        params: {
            NewsId: NewsId
        },
        async: false,
        method: 'post',
        success: function (response) {
            console.log(response.responseText);
            objTags = eval(response.responseText);


        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    //var strTags = "[{ id: 'chk1', text: 'checkbox1' },{ id: 'chk2', text: 'checkbox2' }]";
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
                //alert('图片上传成功');
                //取返回值,注意后台设置的key,如果要取原值
                //取缩略图地址
                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
                Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(data.thumUrl));

                //取原图地址
                //var url = KE.formatUrl(data.url, 'absolute');
                // Ext.getCmp("txtImageUrl").setValue(getStr(data.url));
                var img = "<img width=\"179px\" height=\"100px\" src=\"" + data.url + "\">";
                document.getElementById("image").innerHTML = img;
                uploadImage = data.url;

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
    var d = new Date();
    var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate())
    Ext.getCmp("txtPublishTime").setValue(dateNow);

    Ext.getCmp("cmbIsDefault").setWidth(100);
    Ext.getCmp("cmbIsTop").setWidth(100);
    htmlEditor.html('');
    if (NewsId != "null" && NewsId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_news_by_id",
            params: {
                NewsId: NewsId
            },
            method: 'post',
            success: function (response) {
                var storeId = "newsEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;
                debugger;
                Ext.getCmp("txtNewsType").jitSetValue([{ "id": d.NewsType, "text": d.NewsTypeName}]); //.jitSetValue(d.NewsType);
                Ext.getCmp("txtNewsTitle").setValue(getStr(d.NewsTitle));
                Ext.getCmp("txtPublishTime").setValue(getStr(d.StrPublishTime));
                Ext.getCmp("txtNewsSubTitle").setValue(getStr(d.NewsSubTitle));
                Ext.getCmp("txtContent").setValue(getStr(d.Content));
                Ext.getCmp("txt_Intro").setValue(getStr(d.Intro));
                Ext.getCmp("txtContentUrl").setValue(getStr(d.ContentUrl));
                //Ext.getCmp("txtImageUrl").setValue(getStr(d.ImageUrl));
                if (d.ImageUrl != "" && d.ImageUrl != null) {
                    var img = "<img width=\"179px\" height=\"100px\" src=\"" + d.ImageUrl + "\">";
                    document.getElementById("image").innerHTML = img;
                    uploadImage = d.ImageUrl;
                }
                Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(d.ThumbnailImageUrl));
                Ext.getCmp("txtAPPId").setValue(getStr(d.APPId));

                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.DisplayIndex));
                Ext.getCmp("cmbIsDefault").jitSetValue(getStr(d.IsDefault));//设置是否“设为首页”值
                Ext.getCmp("cmbIsTop").jitSetValue(getStr(d.IsTop));//设置置顶
                Ext.getCmp("txtAuthor").setValue(getStr(d.Author));


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
    CloseWin('NewsEdit');
}

function fnSave() {
    var news = {};

    news.NewsId = getUrlParam("NewsId");//获取url参数
    news.NewsType = Ext.getCmp("txtNewsType").jitGetValue(); //Ext.getCmp("txtNewsType").getValue();
    news.NewsTitle = Ext.getCmp("txtNewsTitle").getValue();
    news.NewsSubTitle = Ext.getCmp("txtNewsSubTitle").getValue();
    news.PublishTime = Ext.getCmp("txtPublishTime").jitGetValueText();//时间类型
    news.DisplayIndex = Ext.getCmp("txtDisplayIndex").getValue();
    news.Content = Ext.getCmp("txtContent").getValue();
    news.Content = htmlEditor.html();  //编辑器取值
    news.Intro = Ext.getCmp("txt_Intro").jitGetValue();
    news.ContentUrl = Ext.getCmp("txtContentUrl").getValue();
    news.ImageUrl = uploadImage; //Ext.getCmp("txtImageUrl").getValue();
    news.ThumbnailImageUrl = Ext.getCmp("txtThumbnailImageUrl").getValue();
    news.APPId = Ext.getCmp("txtAPPId").getValue();


    news.IsDefault = Ext.getCmp("cmbIsDefault").jitGetValue();//获取设为首页
    news.IsTop = Ext.getCmp("cmbIsTop").jitGetValue();
    if(news.IsDefault == "")
    { news.IsDefault = null; }
    if(news.IsTop == "")
    { news.IsTop = null; }

    news.Author = Ext.getCmp("txtAuthor").getValue();

    if (news.Intro != null && news.Intro.length > 500) {
        showError("简介不能超过500字符");
        return;
    }

    if (news.NewsType == null || news.NewsType == "") {
        showError("必须选择资讯类型");
        return;
    }

    if (news.NewsTitle == null || news.NewsTitle == "") {
        showError("必须输入资讯标题");
        return;
    }

    if (news.PublishTime == null || news.PublishTime == "") {
        showError("必须选择发布时间");
        return;
    }

    if (news.Content == null || news.Content == "") {
        showError("必须输入资讯内容");
        return;
    }



    var vals = '', valString = '';
    for (var i = 0; i < objTags.length; i++) {
        var ctlID = objTags[i].id;
        if (Ext.getCmp(ctlID).checked == true) {
            var val = Ext.getCmp(ctlID).inputValue;
            vals += val + ",";
        }
    }
    valString = vals.substring(0, vals.length - 1);
    news.StrTags = valString;
    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/NewsHandler.ashx?method=news_save&NewsId=' + news.NewsId,
        params: {
            "news": Ext.encode(news)
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

