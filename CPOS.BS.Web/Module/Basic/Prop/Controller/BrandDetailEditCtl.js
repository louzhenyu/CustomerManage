var K;
var htmlEditor;
var uploadImage;
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    UploadImage();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/PropHandler.ashx?mid=");

    itemSkuData = [];
    var item_id = getUrlParam("item_id");
    if (item_id != "null" && item_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_BrandDetail_by_id",
            params: { item_id: item_id },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtBrandName").jitSetValue(getStr(d.BrandName));
                Ext.getCmp("txtBrandCode").jitSetValue(getStr(d.BrandCode));
                //Ext.getCmp("txtBrandEngName").jitSetValue(getStr(d.BrandEngName));
                // Ext.getCmp("txtImageUrl").jitSetValue(getStr(d.BrandLogoURL));
                Ext.getCmp("txtTel").jitSetValue(getStr(d.Tel));
                Ext.getCmp("txtDisplayIndex").jitSetValue(getStr(d.DisplayIndex));

                uploadImage = d.BrandLogoURL;
                if (uploadImage != null && uploadImage != "") {
                    var img = "<img width=\"179px\" height=\"100px\" src=\"" + uploadImage + "\">";
                    document.getElementById("image").innerHTML = img;
                }
                htmlEditor.html('');
                htmlEditor.insertHtml(getStr(d.BrandDesc));

                // image
                fnLoadImage();

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

function UploadImage() {
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

                //取原图地址
                var url = data.url;
                uploadImage = url;
                if (uploadImage != null && uploadImage != "") {
                    var img = "<img width=\"179px\" height=\"100px\" src=\"" + uploadImage + "\">";
                    document.getElementById("image").innerHTML = img;
                }
                //  Ext.getCmp("txtImageUrl").setValue(getStr(url));
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

    //上传图片
    KE2 = KindEditor;
    var uploadbutton2 = KE2.uploadbutton({
        button: KE2('#uploadImage2')[0],
        fieldName: 'file',
        url: '/Framework/Upload/UploadFile.ashx?method=image',
        afterUpload: function (data) {
            if (data.success) {
                alert('图片上传成功');
                var url = data.file.url;
                Ext.getCmp("txtImage_ImageUrl").setValue(getStr(url));
            } else {
                alert(data.msg);
            }
        },
        afterError: function (str) {
            alert('错误信息: ' + str);
        }
    });
    uploadbutton2.fileBox.change(function (e) {
        uploadbutton2.submit();
    });
}

//加载图片集合
fnLoadImage = function () {
    var store = Ext.getStore("itemEditImageStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_item_image_info_by_item_id&item_id=" +
            getUrlParam("item_id"),
        params: { start: 0, limit: 0 }
    });
}

//删除图片
function fnDeleteItemImage(id) {
    var store = Ext.getStore("itemEditImageStore");
    if (id == undefined || id == null || id.length <= 2) {
        showInfo("请选择上传图片");
        return;
    };

    var ids = id.split(',');
    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = store.find("ImageId", ids[idObj].toString().trim());
            store.removeAt(index);
            store.commitChanges();
        }
    }
}
//添加图片
function fnAddImageUrl() {
    var parentGrid = Ext.getCmp("gridImage");

    var item = {};
    item.ImageId = newGuid();
    item.ObjectId = getUrlParam("item_id");
    item.ImageURL = Ext.getCmp("txtImage_ImageUrl").jitGetValue();
    item.DisplayIndex = Ext.getCmp("txtImage_DisplayIndex").jitGetValue();

    if (item.ImageURL == null || item.ImageURL == "") {
        alert("请上传图片");
        return;
    }
    if (item.DisplayIndex == null || item.DisplayIndex == "") {
        alert("请输入排序");
        return;
    }

    var index = parentGrid.store.find("ImageId", item.ImageId);
    if (index != -1) {
        alert("图片已存在");
        return;
    }
    for (var i = 0; i < parentGrid.store.data.items.length; i++) {
        if (parentGrid.store.data.items[i].data.ImageId == item.ImageId) {
            alert("图片已存在");
            return;
        }
    }

    parentGrid.store.add(item);
    parentGrid.store.commitChanges();
}


function fnClose() {
    CloseWin('BrandDetailEdit');
}

function fnSave() {
    var flag;
    var _gridImage = Ext.getStore("itemEditImageStore");
    var item = {};

    item.BrandId = getUrlParam("item_id");
    item.BrandName = Ext.getCmp("txtBrandName").jitGetValue();
    item.BrandCode = Ext.getCmp("txtBrandCode").jitGetValue();
    //item.BrandEngName = Ext.getCmp("txtBrandEngName").jitGetValue();
    item.BrandLogoURL = uploadImage;  //Ext.getCmp("txtImageUrl").jitGetValue();
    item.Tel = Ext.getCmp("txtTel").jitGetValue();
    item.DisplayIndex = Ext.getCmp("txtDisplayIndex").jitGetValue();
    item.BrandDesc = htmlEditor.html();

    // image
    item.ItemImageList = [];
    if (_gridImage.data.map != null) {
        for (var tmpItem in _gridImage.data.map) {
            var objData = _gridImage.data.map[tmpItem].data;
            var objItem = {};
            objItem.ImageId = objData.ImageId;
            objItem.ObjectId = item.BrandId;
            objItem.ImageURL = objData.ImageURL;
            objItem.DisplayIndex = objData.DisplayIndex;
            item.ItemImageList.push(objItem);
        }
    }

    if (item.BrandName == null || item.BrandName == "") {
        showError("请填写名称");
        return;
    }
    if (item.BrandCode == null || item.BrandCode == "") {
        showError("请填写代码");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Prop/Handler/PropHandler.ashx?method=save_BrandDetail&item_id=' + item.BrandId,
        params: { "item": Ext.encode(item) },
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
    CloseWin('BrandDetailEdit');
}

go_Del = function (sender, type) {
    $(sender).parent().parent().remove();
    switch (type) {
        case "itemPrice":
            {
                //getTableShowOrHide("tbTablePrice","title1"); 
                deleteItemPrice($(sender).parent().parent().attr("item_price_type_id"));
            }
            break;
        case "itemSku":
            {
                //getTableShowOrHide("tbTableSku","title2");
                deleteSku(sender);
            }
            break;
        default: break;
    }
}

//获取价格列表
function getItemPrice() {
    return JSON.stringify(itemPriceData);
}
//获取Sku列表
function getItemSku() {
    return itemSkuData;
}
//获得属性列表
function getItemProp() {
    var item_prop_data = savePropData("ITEM");
    return item_prop_data;
}



