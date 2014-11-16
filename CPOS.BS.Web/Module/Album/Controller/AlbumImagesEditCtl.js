var uploadImage;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });

    InitVE();
    InitStore();
    InitView();
    fnUploadImage();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/AlbumHandler.ashx?mid=");

    var PhotoId = new String(JITMethod.getUrlParam("PhotoId"));
    if (PhotoId != "null" && PhotoId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_album_image_by_id",
            params: {
                PhotoId: PhotoId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.SortOrder));
                Ext.getCmp("txtTitle").setValue(getStr(d.Title));
                //  Ext.getCmp("txtImageURL").setValue(getStr(d.LinkUrl));
                uploadImage = d.LinkUrl;
                if (d.LinkUrl != null && d.LinkUrl != "") {
                    var img = "<img width=\"179px\" height=\"100px\" src=\"" + d.LinkUrl + "\">";
                    document.getElementById("image").innerHTML = img;
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

function fnUploadImage() {
    //上传图片
    KE = KindEditor;
    var uploadbutton = KE.uploadbutton({
        button: KE('#uploadImage')[0],
        //上传的文件类型
        fieldName: 'imgFile',

        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx?dir=image',
        afterUpload: function (data) {
            if (data.error === 0) {
                //  alert('图片上传成功');
                //取原图地址
                //var url = KE.formatUrl(data.url, 'absolute');
                // Ext.getCmp("txtImageURL").setValue(getStr(data.url));
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
}

function fnClose() {
    CloseWin('AlbumImagesEdit');
}

function fnSave() {
    var image = {};

    image.PhotoId = getUrlParam("PhotoId");
    image.AlbumId = getUrlParam("AlbumId");
    image.Title = Ext.getCmp("txtTitle").getValue();
    image.LinkUrl = uploadImage; // Ext.getCmp("txtImageURL").getValue();
    image.SortOrder = Ext.getCmp("txtDisplayIndex").getValue();

    if (image.SortOrder == null || image.SortOrder == "") {
        showError("必须输入序号");
        return;
    }
    if (image.Title == null || image.Title == "") {
        showError("必须输入标题");
        return;
    }
    if (image.LinkUrl == null || image.LinkUrl == "") {
        showError("必须选择图片");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/AlbumHandler.ashx?method=album_image_save&PhotoId=' + image.PhotoId,
        params: {
            "image": Ext.encode(image)
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

