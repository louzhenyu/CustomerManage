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
    JITPage.HandlerUrl.setValue("Handler/ItemCategoryHandler.ashx?mid=");

    var ItemCategoryId = new String(JITMethod.getUrlParam("ItemCategoryId"));
    if (ItemCategoryId != "null" && ItemCategoryId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_item_category_by_id",
            params: {
                ItemCategoryId: ItemCategoryId
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtItemCategoryCode").setReadOnly(true);

                Ext.getCmp("txtItemCategoryCode").setValue(d.Item_Category_Code);
                Ext.getCmp("txtItemCategoryName").setValue(d.Item_Category_Name);
                Ext.getCmp("txtPyzjm").setValue(d.Pyzjm);
                Ext.getCmp("txtStatus").setDefaultValue(getStr(d.Status));

                if (d.Parent_Id != "-99") {
                    Ext.getCmp("txtParent").jitSetValue([{ "id": d.Parent_Id, "text": d.Parent_Name}]); 
                }
                Ext.getCmp("txtDisplayIndex").setValue(d.DisplayIndex);
                Ext.getCmp("txtImageUrl").setValue(d.ImageUrl);

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        var rootId, rootText
        rootId = Ext.getCmp("txtParent").getRootID();
        rootText = Ext.getCmp("txtParent").getRootText();

        Ext.getCmp("txtParent").jitSetValue([{ "id": rootId, "text": rootText }]);
        Ext.getCmp("txtItemCategoryCode").setReadOnly(false);
        myMask.hide();
    }
});

//关闭
function fnClose() {
    CloseWin('ItemCategoryEdit');
}

function fnSave() {
    var itemCategorys = {};

    itemCategorys.Item_Category_Id = getUrlParam("ItemCategoryId");
    itemCategorys.Item_Category_Code = Ext.getCmp("txtItemCategoryCode").getValue();
    itemCategorys.Item_Category_Name = Ext.getCmp("txtItemCategoryName").getValue();
    itemCategorys.Pyzjm = Ext.getCmp("txtPyzjm").getValue();
    itemCategorys.Status = Ext.getCmp("txtStatus").jitGetValue();
    itemCategorys.Parent_Id = Ext.getCmp("txtParent").jitGetValue();
    itemCategorys.DisplayIndex = Ext.getCmp("txtDisplayIndex").getValue();
    itemCategorys.ImageUrl = Ext.getCmp("txtImageUrl").getValue();

    //alert(itemCategorys.Parent_Id); return;

    //if (itemCategorys.Parent_Id == "") itemCategorys.Parent_Id = "-99";

    if (itemCategorys.Item_Category_Code == null || itemCategorys.Item_Category_Code == "") {
        showError("必须输入类型编码");
        return;
    }
    if (itemCategorys.Item_Category_Name == null || itemCategorys.Item_Category_Name == "") {
        showError("必须输入类型名称");
        return;
    }
    if (itemCategorys.Status == null || itemCategorys.Status == "") {
        showError("必须选择状态");
        return;
    }
    if (itemCategorys.Parent_Id == null || itemCategorys.Parent_Id == "") {
        showError("必须选择上级商品名称");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/ItemCategoryHandler.ashx?method=save_item_category&ItemCategoryId=' + itemCategorys.Item_Category_Id,
        params: {
            "itemCategorys": Ext.encode(itemCategorys)
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

