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
                //var url = KE.formatUrl(data.url, 'absolute');
                Ext.getCmp("txtCoverImageUrl").setValue(getStr(data.url));
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

    htmlEditor.html('');

    var Id = getUrlParam("Id");
    if (Id != "null" && Id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_WMaterialText_by_id",
            params: { Id: Id },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtTitle").jitSetValue(getStr(d.Title));
                Ext.getCmp("txtAuthor").jitSetValue(getStr(d.Author));
                Ext.getCmp("txtCoverImageUrl").jitSetValue(getStr(d.CoverImageUrl));
                Ext.getCmp("txtOriginalUrl").jitSetValue(getStr(d.OriginalUrl));
                Ext.getCmp("txtDisplayIndex").jitSetValue(getStr(d.DisplayIndex));
                //Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));
                Ext.getCmp("txtTypeId").jitSetValue(getStr(d.TypeId));
    
                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.Text));

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
    CloseWin('WMaterialTextEdit');
}

function fnSave() {
    var flag;
    var item = {};
    item.TextId = getUrlParam("Id");
    item.ModelId = getUrlParam("ModelId");
    //item.ParentTextId = null;
    item.Title = Ext.getCmp("txtTitle").jitGetValue();
    item.Author = Ext.getCmp("txtAuthor").jitGetValue();
    item.CoverImageUrl = Ext.getCmp("txtCoverImageUrl").jitGetValue();
    item.Text = htmlEditor.html(); ;
    item.OriginalUrl = Ext.getCmp("txtOriginalUrl").jitGetValue();
    item.DisplayIndex = Ext.getCmp("txtDisplayIndex").jitGetValue();
    item.ApplicationId = getUrlParam("ApplicationId");
    item.TypeId = Ext.getCmp("txtTypeId").jitGetValue();

    if (item.Title == null || item.Title == "") {
        showError("请填写标题");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WMaterialText_save&Id=' + item.TextId, 
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
                parent.fnSearch(2);
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}

function fnCloseWin(url) {
    fnClose();
}

