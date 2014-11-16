var K;
var htmlEditor;
var moduleId = "";
var moduleType = "";
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

    htmlEditor.html('');


    Ext.getStore("AlbumITypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=getAlbumType";
    Ext.getStore("AlbumITypeStore").load();


    var AlbumId = new String(JITMethod.getUrlParam("AlbumId"));
    if (AlbumId != "null" && AlbumId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_album_by_id",
            params: {
                AlbumId: AlbumId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                if (getStr(d.Type) == "2") {
                    document.getElementById('inputContent').style.display = "";
                }
                else {
                    document.getElementById('inputContent').style.display = "none";
                    document.getElementById('fpicture').style.display = "";
                    document.getElementById('tPicType').style.display = "";
                    Ext.getCmp("txtPicType").setValue(getStr(d.ModuleType));
                }

                Ext.getCmp("txtAlbumType").setDefaultValue(getStr(d.Type));
                Ext.getCmp("txtSortOrder").setValue(getStr(d.SortOrder));
                Ext.getCmp("txtTitle").setValue(getStr(d.Title));
                // Ext.getCmp("txtImageURL").setValue(getStr(d.ImageUrl));
                uploadImage = d.ImageUrl;
                if (d.ImageUrl != null && d.ImageUrl != "") {
                    var img = "<img width=\"179px\" height=\"100px\" src=\"" + d.ImageUrl + "\">";
                    document.getElementById("image").innerHTML = img;
                }
                Ext.getCmp("txtModuleName").setValue(getStr(d.ModuleName));

                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.Description));

                moduleId = getStr(d.ModuleId)
                moduleType = getStr(d.ModuleType)

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
                //alert('图片上传成功');
                //Ext.getCmp("txtImageURL").setValue(getStr(data.url));
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
    CloseWin('AlbumEdit');
}

function fnSetLink(id, type, name) {
    moduleId = id;
    moduleType = type;
    Ext.getCmp("txtModuleName").setValue(name);
}

function fnCreateLink() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 510,
        id: "AlbumLink",
        title: "绑定模块",
        url: "AlbumLink.aspx"
    });

    win.show();
}

function fnSave() {
    var album = {};

    album.AlbumId = getUrlParam("AlbumId");
    album.ModuleId = moduleId;
    album.ModuleType = Ext.getCmp("txtPicType").getValue(); // moduleType;
    album.ModuleName = Ext.getCmp("txtModuleName").getValue();
    album.Type = Ext.getCmp("txtAlbumType").getValue();
    album.ImageUrl = uploadImage;
    album.Title = Ext.getCmp("txtTitle").getValue();
    if (album.Title.length>50) {
        showError("相册标题长度不能超过50字符");
        return;
    }
    album.SortOrder = Ext.getCmp("txtSortOrder").getValue();
    album.Description = htmlEditor.html();

    if (album.Type == "1") {
        if (album.ModuleType == null || album.ModuleType == "") {
            showError("必须选择相片类型");
            return;
        }

    }
    if (album.Type == null || album.Type == "") {
        showError("必须选择相册类型");
        return;
    }
    if (album.Title == null || album.Title == "") {
        showError("必须输入相册标题");
        return;
    }
    if (album.ImageUrl == null || album.ImageUrl == "") {
        showError("必须选择封面图片");
        return;
    }
    if (album.ModuleId == null || album.ModuleId == "") {
        showError("必须选择模块");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/AlbumHandler.ashx?method=album_save&AlbumId=' + album.AlbumId,
        params: {
            "album": Ext.encode(album)
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

