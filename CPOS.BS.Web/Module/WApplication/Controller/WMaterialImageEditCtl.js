var K;
var htmlEditor;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
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
                //取返回值,注意后台设置的key,如果要取原值
                //取缩略图地址
                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
                //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(data.thumUrl));

                //取原图地址
                var url = data.url;
                //var url = KE.formatUrl(data.url, 'absolute');
                Ext.getCmp("txtImageUrl").setValue(getStr(data.url));
                Ext.getCmp("txtImageFormat").setValue(getStr(data.url.substring(data.url.lastIndexOf(".")+1)));
                var req = $.ajax({ type: "HEAD", url: url,
                    success: function () {
                        Ext.getCmp("txtImageSize").setValue(getStr(req.getResponseHeader("Content-Length")));
                    }
                });

                document.getElementById("imgView").src = data.url;
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

    //htmlEditor.html('');

    var Id = getUrlParam("Id");
    if (Id != "null" && Id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_WMaterialImage_by_id",
            params: { Id: Id },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtImageName").jitSetValue(getStr(d.ImageName));
                Ext.getCmp("txtImageUrl").jitSetValue(getStr(d.ImageUrl));
                Ext.getCmp("txtImageSize").jitSetValue(getStr(d.ImageSize));
                Ext.getCmp("txtImageFormat").jitSetValue(getStr(d.ImageFormat));
                //Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));

                document.getElementById("imgView").src = d.ImageUrl;

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        myMask.hide();
    }
    
});

function fnClose() {
    CloseWin('WMaterialImageEdit');
}

function fnSave() {
    var flag;
    var item = {};
    item.ImageId = getUrlParam("Id");
    item.ModelId = getUrlParam("ModelId");
    item.ImageName = Ext.getCmp("txtImageName").jitGetValue();
    item.ImageUrl = Ext.getCmp("txtImageUrl").jitGetValue();
    item.ImageSize = Ext.getCmp("txtImageSize").jitGetValue();
    item.ImageFormat = Ext.getCmp("txtImageFormat").jitGetValue();
    item.ApplicationId = getUrlParam("ApplicationId");

    if (item.ImageName == null || item.ImageName == "") {
        showError("请填写名称");
        return;
    }
    if (item.ImageUrl == null || item.ImageUrl == "") {
        showError("请填写链接地址");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WMaterialImage_save&Id=' + item.ImageId, 
        params: {
            "item": Ext.encode(item)
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
    if (flag) fnCloseWin();
}

function fnCloseWin() {
    CloseWin('WMaterialImageEdit');
}

