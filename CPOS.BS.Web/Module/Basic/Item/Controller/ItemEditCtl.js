var K;
var htmlEditor, sList, itemCategoryList = [];
var z_sku_prop_cfg, sImageUrl, sImageTitle, sImageDescription;
var imageObject, frontImage;
var updateSKUIndex;//当前更新的第几行
var myUpdataSkuIndex = -1;
var methedType = "Add"; // Edit/Add 操作类型，默认add，当逻辑判断是修改会重新赋值。  add by donal 2014-9-29 17:38:25

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    z_sku_prop_cfg = Ext.create('jit.biz.SkuPropCfg', {});
    InitVE();
    InitStore();
    InitView();
    UploadImage();

    //添加显示
    //$("#btnAddDisplay").click(function () {
    //    alert("添加");
    //    $("#z_sku_tb").show();
    //});

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/ItemHandler.ashx?mid=");

    itemSkuData = [];
    var item_id = getUrlParam("item_id");

    //加载商品标签
    Ext.getStore("itemTagStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_treegrid_json";
    Ext.getStore("itemTagStore").proxy.extraParams = {
        itemID: item_id
    };
    Ext.getStore("itemTagStore").load();
    if (item_id == "null" || item_id == "") {
        fnjqueryload(null);
    }
    if (item_id != "null" && item_id != "") {

        methedType = "Edit"; //标识操作为修改  add by donal 2014-9-29 17:40:07

        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_item_by_id",//获取商品信息
            params: { item_id: item_id },
            method: 'POST',
            success: function (response) {
                var storeId = "itemEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;//从商品里获取的信息
            //    console.log(response.responseText);//读取商品信息，查看sku信息

                Ext.getCmp("txtItemCategory").jitSetValue([{ "id": d.Item_Category_Id, "text": d.Item_Category_Name}]);
                //设置值
                Ext.getCmp("txtItemCode").jitSetValue(getStr(d.Item_Code));
                Ext.getCmp("txtItemName").jitSetValue(getStr(d.Item_Name));
                Ext.getCmp("txtItemEnglish").jitSetValue(getStr(d.Item_Name_En));
                Ext.getCmp("txtItemNameShort").jitSetValue(getStr(d.Item_Name_Short));
                //Ext.getCmp("txtStatus").jitSetValue(getStr(d.Status));
                Ext.getCmp("txtPyzjm").jitSetValue(getStr(d.Pyzjm));
                Ext.getCmp("txtIfgifts").setDefaultValue(getStr(d.ifgifts));
                Ext.getCmp("txtIfoften").setDefaultValue(getStr(d.ifoften));
                Ext.getCmp("txtIfservice").setDefaultValue(getStr(d.ifservice));
                Ext.getCmp("txtIsGB").setDefaultValue(getStr(d.isGB));
                Ext.getCmp("txtDisplayIndex").jitSetValue(getStr(d.display_index));
                Ext.getCmp("txtRemark").jitSetValue(getStr(d.Item_Remark));
                //Ext.getCmp("txtImageUrl").jitSetValue(getStr(d.Image_Url));

                //Ext.getCmp("txtCreateUserName").setValue(getStr(d.Create_User_Name));
                //Ext.getCmp("txtCreateTime").setValue(getStr(d.Create_Time));
                //Ext.getCmp("txtModifyUserName").setValue(getStr(d.Modify_User_Name));
                //Ext.getCmp("txtModifyTime").setValue(getStr(d.Modify_Time));
             //   debugger;
                imageUrl = d.imageUrl;
                if (imageUrl != null && imageUrl != '') {
                    $("#txtRQcode").val(imageUrl);
                    var image = '<img id="imgView" alt="" src=\"' + imageUrl + '\"width="256px" height="256px">';
                    $("#image").html(image);
                    if (d.ReplyType == "1") {

                        $("#text").val(d.Text);
                        $("select").val('1');
                        //$("#imageContentMessage").removeClass("show").addClass("hide");
                        //$("#contentEditor").removeClass("hide").addClass("show");
                        $("#text-display").show();
                        $("#imageContentMessage").hide();
                        fnjqueryload(null);
                    }
                    else if (d.ReplyType == "3") {
                        $("select").val('3');
                        $("#text-display").hide();
                        $("#imageContentMessage").show();
                        var MaterialTextIds = [];
                        if (d.listMenutext != null) {
                            for (var i = 0; i < d.listMenutext.length; i++) {
                                MaterialTextIds.push({
                                    Author: "",
                                    DisplayIndex: d.listMenutext[i].DisplayIndex,
                                    ImageUrl: d.listMenutext[i].CoverImageUrl,
                                    OriginalUrl: d.listMenutext[i].OriginalUrl,
                                    TestId: d.listMenutext[i].TextId,
                                    Title: d.listMenutext[i].Title,
                                    Text: d.listMenutext[i].Text
                                });
                            }
                            fnjqueryload(MaterialTextIds);
                        }
                    }
                    else {
                        fnjqueryload(MaterialTextIds);
                    }
                }
                else {
                    fnjqueryload(null);
                }


                if (d.ItemCategoryMappingList != undefined && d.ItemCategoryMappingList != null) {

                    //初始化商品类别集合
                    itemCategoryList = [];
                    var txtItemCategory = "";
                    for (var i = 0; i < d.ItemCategoryMappingList.length; i++) {

                        var isFirstVisit = 0;
                        var isShow = "否";
                        if (d.ItemCategoryMappingList[i].IsFirstVisit == 1) {
                            isFirstVisit = 1;
                            isShow = "是";
                        }
                        if (d.ItemCategoryMappingList[i].ItemCategoryName != null) {
                            itemCategoryList.push({
                                "ItemTagID": d.ItemCategoryMappingList[i].ItemCategoryId,
                                "IsFirstVisit": isFirstVisit
                            });

                            txtItemCategory += d.ItemCategoryMappingList[i].ItemCategoryName + " ";   // + "(" + isShow + "),";
                        }
                    }

                    if (txtItemCategory.length > 0) {
                        txtItemCategory = txtItemCategory.substring(0, txtItemCategory.length - 1);
                    }
                    Ext.getCmp("txtCategoryMapping").jitSetValue(txtItemCategory);//设置值
                }

                // prop
                loadPropData(d.ItemPropList, "ITEM");

                // price
                fnLoadPrice();

                // sku
                itemSkuData = d.SkuList;//给itemSkuData赋值，
                buildSkuTable();

                // image
                fnLoadImage();

                // unit
                fnLoadUnit();

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        fnLoadPrice();
        myMask.hide();
    }

});

function UploadImage() {
    //上传图片
    //KE = KindEditor;
    //var uploadbutton = KE.uploadbutton({
    //    button: KE('#uploadImage')[0],
    //    //上传的文件类型
    //    fieldName: 'imgFile',
    //    url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx?dir=image',
    //    afterUpload: function (data) {
    //        if (data.error === 0) {
    //            alert('图片上传成功');

    //            //取原图地址
    //            var url = data.url;
    //            Ext.getCmp("txtImageUrl").setValue(getStr(url));
    //        } else {
    //            alert(data.message);
    //        }
    //    },
    //    afterError: function (str) {
    //        alert('自定义错误信息: ' + str);
    //    }
    //});
    //uploadbutton.fileBox.change(function (e) {
    //    uploadbutton.submit();
    //});

    //上传图片
    KE2 = KindEditor;
    var uploadbutton2 = KE2.uploadbutton({
        button: KE2('#uploadImage2')[0],//上传图片的按钮
        fieldName: 'file',//传过去的参数
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
        uploadbutton2.submit();//放文件地址的地方值一变，就提交
    });
}

fnLoadPrice = function () {
    var store = Ext.getStore("itemEditPriceStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_item_price_info_by_item_id&item_id=" +
            getUrlParam("item_id"),
        params: { start: 0, limit: 0 }
    });
}

//上传图片中加载图片集合
fnLoadImage = function () {
    var store = Ext.getStore("itemEditImageStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_item_image_info_by_item_id&item_id=" +
            getUrlParam("item_id"),
        params: { start: 0, limit: 0 }
    });
}

fnLoadUnit = function () {
    var store = Ext.getStore("itemEditUnitStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_item_unit_info_by_item_id&item_id=" +
            getUrlParam("item_id"),
        params: { start: 0, limit: 0 }
    });
}

function fnAddItemPrice(id, op, param) {
    var hdItemPrice_TypeListCtrl = Ext.getCmp("txtItemPrice_TypeList");
    var tbItemPrice_PriceCtrl = Ext.getCmp("txtItemPrice_Price");

    if (id == undefined || id == null) id = newGuid();
    var parentGrid = Ext.getCmp("gridPrice");
    var item = {};
    //item.index = z_selected_data.index;
    item.item_price_id = id;
    item.item_id = getUrlParam("item_id");
    item.item_price_type_id = hdItemPrice_TypeListCtrl.jitGetValue();
    item.item_price_type_name = hdItemPrice_TypeListCtrl.rawValue;
    item.item_price = tbItemPrice_PriceCtrl.jitGetValue();

    if (item.item_price_type_id == null || item.item_price_type_id == "") {
        alert("请选择价格类型");
        return;
    }
    if (item.item_price == null || item.item_price == "") {
        alert("请输入价格");
        return;
    }

    var index = parentGrid.store.find("item_price_id", item.item_price_id);
    if (index != -1) {
        alert("价格已存在");
        return;
    }
    for (var i = 0; i < parentGrid.store.data.items.length; i++) {
        if (parentGrid.store.data.items[i].data.item_price_type_id == item.item_price_type_id) {
            alert("价格已存在");
            return;
        }
    }

    if (item.item_price_id == "") item.item_price_id = newGuid();
    parentGrid.store.add(item);
    parentGrid.store.commitChanges();
}

function fnDeleteItemPrice(id) {
    var store = Ext.getStore("itemEditPriceStore");
    if (id == undefined || id == null || id.length <= 2) {
        showInfo("请选择商品价格");
        return;
    };

    var ids = id.split(',');
    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = store.find("item_price_id", ids[idObj].toString().trim());
            store.removeAt(index);
            store.commitChanges();
        }
    }
}

//删除图片
function fnDeleteItemImage(id) {
    var store = Ext.getStore("itemEditImageStore");
    if (id == undefined || id == null || id.length <= 2) {
        showInfo("请选择上传图片");
        return;
    };

    var ids = id.split(',');//转换为数组
    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = store.find("ImageId", ids[idObj].toString().trim());//传两个参数，第一个是列名，第二个是值
            store.removeAt(index);//删除指定index的数据
            store.commitChanges();//保存更改
        }
    }
}

//修改图片
function fnUpdateItemImage(data) {
    var d = Ext.decode(unescape(data));//解码
    imageObject = d;
    Ext.getCmp("txtImage_ImageUrl").setValue(d.ImageURL);
    Ext.getCmp("txtImage_DisplayIndex").setValue(d.DisplayIndex);
    Ext.getCmp("txtImage_Title").setValue(d.Title);
    Ext.getCmp("txtImage_Description").setValue(d.Description);
}

//添加图片
function fnAddImageUrl() {
    var parentGrid = Ext.getCmp("gridImage");
    var imgFlag = "new";

    var item = {};

    if (imageObject)
        item = imageObject;

    if (!item.ImageId)
        item.ImageId = newGuid();
    else
        imgFlag = "update";

    item.ObjectId = getUrlParam("item_id");
    item.ImageURL = Ext.getCmp("txtImage_ImageUrl").jitGetValue();
    item.DisplayIndex = Ext.getCmp("txtImage_DisplayIndex").jitGetValue();
    item.Title = Ext.getCmp("txtImage_Title").jitGetValue();
    item.Description = Ext.getCmp("txtImage_Description").jitGetValue();

    if (item.DisplayIndex == "1")
        frontImage = item.ImageURL;//注：排序为1的图片将展示在首页


    if (item.ImageURL == null || item.ImageURL == "") {
        alert("请上传图片");
        return;
    }
    if (item.DisplayIndex == null || item.DisplayIndex == "") {
        alert("请输入排序");
        return;
    }

    //var index = parentGrid.store.find("ImageId", item.ImageId);
    //if (index != -1) {
    //    alert("图片已存在");
    //    return;
    //}

    //for (var i = 0; i < parentGrid.store.data.items.length; i++) {
    //if (parentGrid.store.data.items[i].data.ImageId == item.ImageId) {
    //    alert("图片已存在");
    //    return;
    //}
    //if (parentGrid.store.data.items[i].data.ImageURL == item.ImageURL) {
    //    imgFlag = false;
    //    continue;
    //}
    //}
    sImageUrl = item.ImageURL;
    sImageTitle = item.Title;
    sImageDescription = item.Description;
    var tmplist = getItemSku();
    if (tmplist.length > 1) {
        get("pnlSkuImage").style.display = "";
        var str = "<table class=\"z_is_tb\"><tr>";
        str += "<td class=\"z_is_td2\"></td>";
        if (z_sku_prop_cfg.sku_prop_1 == "1") {
            str += "<td class=\"z_is_td\">" + (z_sku_prop_cfg.prop_1_name == undefined ? "规格" : z_sku_prop_cfg.prop_1_name) + "</td>";
        }
        if (z_sku_prop_cfg.sku_prop_2 == "1") {
            str += "<td class=\"z_is_td\">" + z_sku_prop_cfg.prop_2_name + "</td>";
        }
        if (z_sku_prop_cfg.sku_prop_3 == "1") {
            str += "<td class=\"z_is_td\">" + z_sku_prop_cfg.prop_3_name + "</td>";
        }
        if (z_sku_prop_cfg.sku_prop_4 == "1") {
            str += "<td class=\"z_is_td\">" + z_sku_prop_cfg.prop_4_name + "</td>";
        }
        if (z_sku_prop_cfg.sku_prop_5 == "1") {
            str += "<td class=\"z_is_td\">" + z_sku_prop_cfg.prop_5_name + "</td>";
        }
        str += "</tr>";

        for (var i = 0; i < tmplist.length; i++) {
            str += "<tr>";
            var chkFlag = false;
            //for (var j = 0; j < sList.length; j++) {
            //    if (sList[j].ObjectId == tmplist[i].sku_id) {
            //        chkFlag = true;
            //        break;
            //    }
            //}
            if (chkFlag)
                str += "<td class=\"z_is_td2\"><input type=\"radio\" name=\"order\" value=\"" + tmplist[i].sku_id + "\" checked /></td>";
            else
                str += "<td class=\"z_is_td2\"><input type=\"radio\" name=\"order\" value=\"" + tmplist[i].sku_id + "\" /></td>";
            if (tmplist[i].prop_1_detail_id != null && tmplist[i].prop_1_detail_id.length > 0)
                str += "<td class=\"z_is_td\" >" + tmplist[i].prop_1_detail_name + "</td>";
            if (tmplist[i].prop_2_detail_id != null && tmplist[i].prop_2_detail_id.length > 0)
                str += "<td class=\"z_is_td\" >" + tmplist[i].prop_2_detail_name + "</td>";
            if (tmplist[i].prop_3_detail_id != null && tmplist[i].prop_3_detail_id.length > 0)
                str += "<td class=\"z_is_td\" >" + tmplist[i].prop_3_detail_name + "</td>";
            if (tmplist[i].prop_4_detail_id != null && tmplist[i].prop_4_detail_id.length > 0)
                str += "<td class=\"z_is_td\" >" + tmplist[i].prop_4_detail_name + "</td>";
            if (tmplist[i].prop_5_detail_id != null && tmplist[i].prop_5_detail_id.length > 0)
                str += "<td class=\"z_is_td\" >" + tmplist[i].prop_5_detail_name + "</td>";
            str += "</tr>";
        }
        str += "</table>";
        get("skuList").innerHTML = str;
    }

    if (imgFlag == "new") {
        parentGrid.store.add(item);
        parentGrid.store.commitChanges();

        imageObject = null;
        fnImageClear();
    }
    else if (imgFlag == "update") {
        var image = parentGrid.store.getAt(parentGrid.store.find("ImageId", item.ImageId, 0, true, false))
        image.set("ImageURL", item.ImageURL);
        image.set("DisplayIndex", item.DisplayIndex);
        image.set("Title", item.Title);
        image.set("Description", item.Description);
        parentGrid.store.commitChanges();

        imageObject = null;
        fnImageClear();
    }
}

function fnAddImageUrlClear() {
    Ext.MessageBox.confirm("提示", "是否确认清除？", function (btn) {
        if (btn == "yes") {
            fnImageClear();
        }
    });
}

function fnImageClear() {
    Ext.getCmp("txtImage_ImageUrl").setValue();
    Ext.getCmp("txtImage_DisplayIndex").setValue();
    Ext.getCmp("txtImage_Title").setValue();
    Ext.getCmp("txtImage_Description").setValue();
}

//添加sku
function fnAddItemSku(id, op, param) {
    //alert("添加");

    //  var a = updateSKUIndex;

    //sku列表的最大index
    var index = parseInt($("#tbTableSku tbody tr").last().attr("index") == null ? "0" : $("#tbTableSku tbody tr").last().attr("index")) + 1;

    //获取sku信息
    var skuData = getSkuInfo(index);
    for (var i = 0; i < skuData.sku_price_list.length; i++) {
        var lbl = $("div #" + skuData.sku_price_list[i].item_price_type_id).attr("price_type_name");
        if (skuData.sku_price_list[i].sku_price == "" || skuData.sku_price_list[i].sku_price == null) {   //必须填写一个价格
            Ext.Msg.alert("提示", lbl + "不能为空！");
            return;
        }
        else {
            if (isNaN(skuData.sku_price_list[i].sku_price)) {
                Ext.Msg.alert("提示", lbl + "只能位数字！");
                return;
            }
        }
    }
    addSkuPost(updateSKUIndex);//这是什么意思？添加或者修改
    updateSKUIndex = null;
    //保存成功关闭
    // Ext.get("z_sku_tb").hide();//虽然隐藏掉了，占的空间还在
    $("#z_sku_tb").hide();
    Ext.getCmp("btnAddDisplayInn").show();
    $("#tbTableSku").show();
}

function fnAddItemUnit(id, op, param) {
    var hdItemUnit_TypeListCtrl = Ext.getCmp("txtItemUnit_List");

    if (id == undefined || id == null) id = newGuid();
    var parentGrid = Ext.getCmp("gridUnit");
    var item = {};
    //item.index = z_selected_data.index;
    item.MappingId = id;
    item.ItemId = getUrlParam("item_id");
    item.UnitId = hdItemUnit_TypeListCtrl.jitGetValue();
    item.UnitName = hdItemUnit_TypeListCtrl.rawValue;

    if (item.UnitId == null || item.UnitId == "") {
        alert("请选择门店");
        return;
    }

    var index = parentGrid.store.find("MappingId", item.MappingId);
    if (index != -1) {
        alert("门店已存在");
        return;
    }
    for (var i = 0; i < parentGrid.store.data.items.length; i++) {
        if (parentGrid.store.data.items[i].data.UnitId == item.UnitId) {
            alert("门店已存在");
            return;
        }
    }

    if (item.MappingId == "") item.MappingId = newGuid();
    parentGrid.store.add(item);
    parentGrid.store.commitChanges();
}

function fnDeleteItemUnit(id) {
    var store = Ext.getStore("itemEditUnitStore");
    if (id == undefined || id == null || id.length <= 0) {
        showInfo("请选择门店");
        return;
    };

    var ids = id.split(',');
    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = store.find("MappingId", ids[idObj].toString().trim());
            store.removeAt(index);
            store.commitChanges();
        }
    }
}

function fnClose() {
    CloseWin('ItemEdit');
}


//提交标记，防止重复提交
var submitFlag = true;

function fnSave() {

    var flag;
    var _gridPrice = Ext.getStore("itemEditPriceStore");
    var _gridImage = Ext.getStore("itemEditImageStore");//_gridImage是store
    var _gridUnit = Ext.getStore("itemEditUnitStore");
    var item = {};

    var hdItemCategoryCtrl = Ext.getCmp("txtItemCategory");//获取种类
    var tbItemCodeCtrl = Ext.getCmp("txtItemCode");
    var tbItemNameCtrl = Ext.getCmp("txtItemName");
    var tbItemEnglishCtrl = Ext.getCmp("txtItemEnglish");
    var tbItemNameShortCtrl = Ext.getCmp("txtItemNameShort");
    var tbPyzjmCtrl = Ext.getCmp("txtPyzjm");
    var hdIfgiftsCtrl = Ext.getCmp("txtIfgifts");
    var hdIfoftenCtrl = Ext.getCmp("txtIfoften");
    var hdIfserviceCtrl = Ext.getCmp("txtIfservice");
    var hdIsGBCtrl = Ext.getCmp("txtIsGB");
    var tbDisplayIndexCtrl = Ext.getCmp("txtDisplayIndex");
    var tbRemarkCtrl = Ext.getCmp("txtRemark");

    item.Item_Id = getUrlParam("item_id");//通过传过来的参数获取到商品Id
    item.Item_Category_Id = hdItemCategoryCtrl.jitGetValue();//类别的值
    item.Item_Code = tbItemCodeCtrl.jitGetValue();
    item.Item_Name = tbItemNameCtrl.jitGetValue();
    item.Item_Name_En = tbItemEnglishCtrl.jitGetValue();
    item.Item_Name_Short = tbItemNameShortCtrl.jitGetValue();
    item.Pyzjm = tbPyzjmCtrl.jitGetValue();
    item.Ifgifts = hdIfgiftsCtrl.jitGetValue();
    item.Ifoften = hdIfoftenCtrl.jitGetValue();
    item.Ifservice = hdIfserviceCtrl.jitGetValue();
    item.IsGB = hdIsGBCtrl.jitGetValue();
    item.Display_index = tbDisplayIndexCtrl.jitGetValue();
    item.Item_Remark = tbRemarkCtrl.jitGetValue();
    //item.Image_Url = Ext.getCmp("txtImageUrl").jitGetValue();
    item.Image_Url = frontImage;//注：排序为1的图片将展示在首页


    // prop
    item.ItemPropList = getItemProp();
    //    item.ItemCategoryIdList = Ext.getCmp("txtItemCategoryMapping").jitGetValue().split(",");
    item.ItemCategoryList = itemCategoryList;

    // price
    item.ItemPriceList = [];
    if (_gridPrice.data.map != null) {
        for (var tmpItem in _gridPrice.data.map) {
            var objData = _gridPrice.data.map[tmpItem].data;
            var objItem = {};
            objItem.item_price_id = objData.item_price_id;
            objItem.item_id = item.Item_Id;
            objItem.item_price_type_id = objData.item_price_type_id;
            objItem.item_price = objData.item_price;
            item.ItemPriceList.push(objItem);
        }
    }




    // image
    item.ItemImageList = [];
    if (_gridImage.data.map != null) {  //为什么取data.map呢
        for (var tmpItem in _gridImage.data.map) {//遍历每一行
            var objData = _gridImage.data.map[tmpItem].data;
            var objItem = {};
            objItem.ImageId = objData.ImageId;
            objItem.ObjectId = item.Item_Id;//商品ID
            objItem.ImageURL = objData.ImageURL;
            objItem.DisplayIndex = objData.DisplayIndex;
            objItem.Title = objData.Title;
            objItem.Description = objData.Description;
            item.ItemImageList.push(objItem);
        }
    }

    // unit
    item.ItemUnitList = [];
    if (_gridUnit.data.map != null) {
        for (var tmpItem in _gridUnit.data.map) {
            var objData = _gridUnit.data.map[tmpItem].data;
            var objItem = {};
            objItem.MappingId = objData.MappingId;
            objItem.ItemId = item.Item_Id;
            objItem.UnitId = objData.UnitId;
            item.ItemUnitList.push(objItem);
        }
    }

    // sku  保存时判断
    item.SkuList = getItemSku();
    if (SKUExists && (!item.SkuList || item.SkuList.length == 0)) {
        showError("请至少填写一个SKU");
        return;
    }
    item.SkuImageList = sList;

    if (item.Item_Category_Id == null || item.Item_Category_Id == "") {
        showError("请选择商品分类");
        return;
    }
    //if (item.Item_Code == null || item.Item_Code == "") {
    //    showError("请填写商品编码");
    //    return;
    //}
    if (item.Item_Name == null || item.Item_Name == "") {
        showError("请填写商品名称");
        return;
    }
    if (item.Ifgifts == null || item.Ifgifts == "") {
        showError("请选择是否赠品");
        return;
    }
    if (item.Ifoften == null || item.Ifoften == "") {
        showError("请选择是否常用商品");
        return;
    }
    if (item.Ifservice == null || item.Ifservice == "") {
        showError("请选择是否服务型商品");
        return;
    }
    if (item.IsGB == null || item.IsGB == "") {
        showError("请选择是否散货");
        return;
    }
    if (item.ItemPropList == null || item.ItemPropList == "") {
        return; //这里不需要提示信息，因为getPropInfo内部做了消息通知
    }


    var txtRQcode = $("#txtRQcode").val();
    //二维码 保存记录
    /* delete by donal 之前删除了保存二维码逻辑，现在注释相应判断，否则造成保存时错误判断，导致前台不能保存。

    var selectVal = $("#Valueselect option:selected").val();
    item.MaxWQRCod = MaxWQRCod;
    item.imageUrl = imageUrl;
    if (selectVal == '1') {
        if (txtRQcode != "" && txtRQcode != null) {
            var text = $("#text").val();
            if (text == "" || text == null) {
                alert('文本编辑不能为空!');
                return;
            }
            else if (text.length >= 1000) {
                alert('文本编辑长度不能超过1000!');
                return;
            }
        }
        item.text = text;
        item.ReplyType = 1;
    }
    else {
        var materialTextIds = [];
        var maxtrailDom = $("#imageContentMessage").find(".item");
        if (txtRQcode != "" && txtRQcode != null) {
            if (maxtrailDom.length <= 0) {
                Ext.Msg.alert("提示", "图文至少要添加一个图文!");
                return;
            }
        }
        maxtrailDom.each(function (i, k) {
            var obj = $(k).attr("data-obj");
            obj = JSON.parse(obj);
            materialTextIds.push({
                TextId: obj.TestId,
                DisplayIndex: (i + 1)
            });
        });
        item.listMenutextMapping = materialTextIds;
        item.ReplyType = "3";
    }
    */
    
    if (submitFlag == false) {
        //不能提交直接返回
        return;
    }
    submitFlag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Item/Handler/ItemHandler.ashx?method=save_item&item_id=' + item.Item_Id,
        params: {
            "item": Ext.encode(item),
            "Operation": methedType
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

    submitFlag = true;
    if (flag) fnCloseWin();
}


///操作单个sku  add by donal 2014-10-8 13:38:49
function fnSaveOne(Sku,Operation)
{
    if (methedType != "Edit") {
        return "";
    }
    
    var item = {};  
    var itemSkuData = [];
    itemSkuData.push(Sku);
    //通过传过来的参数获取到商品Id
    item.Item_Id = getUrlParam("item_id");
    item.SkuList = itemSkuData;

    var mask = new Ext.LoadMask(Ext.getBody(), {
        msg: '正在向服务器提交,请稍等...'
    });
    mask.show();

    var skuID = "";

    Ext.Ajax.request({
        method: 'POST',
        //sync: false,
        async: false,
        url: '/Module/Basic/Item/Handler/ItemHandler.ashx?method=save_sku&item_id=' + item.Item_Id,
        params: {
            "item": Ext.encode(item),
            "Operation":Operation
        },
        success: function (result, request) {
            mask.hide();

            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                Ext.Msg.alert('提示信息', "操作失败," + d.msg);
            } else {
                Ext.Msg.alert('提示信息', '操作成功！');
                skuID = d.msg;
            }
        },
        failure: function (result) {
            mask.hide();
            Ext.Msg.alert('提示信息', "操作失败," + result.responseText);
        }
    });

    return skuID;
}

function fnCloseWin() {
    CloseWin('ItemEdit');//关闭窗体
}

go_Del = function (sender, type) {
    if (!confirm("确认要删除这条记录吗？"))
    {
        return;
    }
    //$(sender).parent().parent().remove();
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


function updateSKU(sender, type) {
    Ext.get("z_sku_tb").show();//展开上面的信息
    Ext.getCmp("btnAddDisplayInn").hide();
    $("#tbTableSku").hide();
    //console.log($(sender).parent().parent());
    //console.log($(sender).parent().parent()[0].rowIndex);
    var index = $(sender).parent().parent()[0].rowIndex - 1;//sender是td里的元素
    //console.log(itemSkuData[index]);

    //载入属性值
    for (var prop in itemSkuData[index]) {   //itemSkuData是存储sku对象数据的数组
        var reg = /^prop_+\d+_id$/i;
        if (reg.test(prop)) {
            //console.log(itemSkuData[0][prop]);
            var valueName = prop.replace("id", "detail_name");//把元素里的id替换为detail_name
            //if (itemSkuData[index][prop] != null)
            //    Ext.getCmp(itemSkuData[index][prop]).setValue(itemSkuData[index][valueName]);//这里要注意，上面的元素,这是原来的
            if (itemSkuData[index][prop] != null) {
              //  debugger;
                var at = Ext.getCmp(itemSkuData[index][prop])
                if (at != undefined) {
                    Ext.getCmp(itemSkuData[index][prop]).setValue(itemSkuData[index][valueName]);//这里要注意，上面的元素
                }
            }
        }
    }

    //载入价格值
    for (var i = 0; i < itemSkuData[index].sku_price_list.length; i++) {
        var price = itemSkuData[index].sku_price_list[i]
        var priceid = price.item_price_type_id;
        var sku_price = price.sku_price;

        Ext.getCmp(priceid).setValue(sku_price);
    }

    Ext.getCmp("txtBarcode").setValue(itemSkuData[index].barcode);
    updateSKUIndex = index;//索引,这里用了索引

    //提示一下保存成功,不必要，因为，这里还没有保存到数据库
    //  myUpdataSkuIndex=
  //  alert($(sender).parent().parent()[0].rowIndex);
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

fnCloseSku = function () {
    get("pnlSkuImage").style.display = "none";
}

fnSelectSku = function (id, url, title, desc) {
    var flag = false;
    if (sList != null) {
        for (var i = 0; i < sList.length; i++) {
            if (sList[i].ObjectId == id) {
                sList[i].ImageURL = url;
                flag = true;
            }
        }
    }
    // if (!flag) sList.push({ ObjectId: id, ImageURL: url, Title: title, Description: desc });
    get("pnlSkuImage").style.display = "none";
}

function fnColumnChecked(value, p, r) {
    //是否为根，为根节点的话不添加复选框
    if (r.data.ItemTagID.length > 0) {

        //是否选中，不选中的话禁用复选框
        if (r.data.checked) {

            //是否显示，显示的话选中复选框
            if (r.data.IsFirstVisit == 1) {
                return "<input id='" + r.data.ItemTagID + "' type='checkbox' checked>";
            } else {
                return "<input id='" + r.data.ItemTagID + "' type='checkbox'>";
            }

        } else {
            return "<input id='" + r.data.ItemTagID + "' type='checkbox' disabled='disabled'>";
        }

    } else {
        return "";
    }

}

function fnCancel() {
    Ext.getCmp("categorySelectWin").hide();
}

//商品分类保存
function fnSubmit() {

    //初始化商品类别集合
    itemCategoryList = [];
    var txtItemCategory = "";

    var nodes = Ext.getCmp('categoryGrid').getChecked();
    for (var i = 0; i < nodes.length; i++) {
        var isFirstVisit = 0;
        var isShow = "否";
        if ($("#" + nodes[i].data.ItemTagID).prop("checked")) {
            isFirstVisit = 1;
            isShow = "是";
        }
        itemCategoryList.push({
            "ItemTagID": nodes[i].data.ItemTagID,
            "IsFirstVisit": isFirstVisit
        });

        txtItemCategory += nodes[i].data.ItemTagName + " ";   // + "(" + isShow + "),";
    }
    if (txtItemCategory.length > 0) {
        txtItemCategory = txtItemCategory.substring(0, txtItemCategory.length - 1);
    }

    Ext.getCmp("txtCategoryMapping").jitSetValue(txtItemCategory);
    Ext.getCmp("categorySelectWin").hide();

}

function fnOptionCategory() {
    Ext.getCmp("categorySelectWin").show();
}

//设置节点切换状态
function NodeCheckChange(node, state) {
    SetChildNodeChecked(node, state);
    SetParentNodeCheckState(node, state);
}
//设置节点子级全部选中
function SetChildNodeChecked(node, state) {
    node.checked = state;
    node.eachChild(function (child) {
        child.set('checked', state);
        child.fireEvent('checkchange', child, state);
    });
}
//设置节点父节点选中状态
function SetParentNodeCheckState(node, state) {
    var parentNode = node.parentNode;
    if (parentNode != null) {
        if (parentNode.data.ItemTagID.length > 0) {

            if (state) {
                parentNode.set('checked', state);
            } else {

                var isCancel = true;
                parentNode.eachChild(function (child) {
                    if (child.data.checked) {
                        isCancel = false;
                    }
                });
                if (isCancel) {
                    parentNode.set('checked', state);
                }
            }
            this.SetParentNodeCheckState(parentNode, state);
        }
    }
}
//下载二维码
function fnDownloadQRCode() {
    var item_id = getUrlParam("item_id");
    var item_name = Ext.getCmp("txtItemName").getValue();
    //传了商品ID和商品名
    window.open('/Module/Basic/Item/Handler/ItemHandler.ashx?method=download_qrcode&item_id=' + item_id + '&item_name=' + item_name);

}
var imageUrl = "";
var MaxWQRCod = "";
function bindEvent() {
    $("#createCode").bind("click", function () {
        Ext.Ajax.request({
            method: 'GET',
            sync: true,
            async: false,
            url: '/Module/Basic/Item/Handler/ItemHandler.ashx?method=CretaeWxCode',
            success: function (result, request) {

                var d = Ext.decode(result.responseText);
               // debugger;
                if (d.success == true) {
                    imageUrl = d.data.imageUrl;
                    $("#txtRQcode").val(imageUrl);
                    var image = '<img id="imgView" alt="" src=\"' + imageUrl + '\"width="256px" height="256px">';
                    $("#image").html(image);
                    MaxWQRCod = d.data.MaxWQRCod;

                } else {

                }
            },
            failure: function (result) {
                showError(result.responseText);
            }
        });

    });

    $("#downCode").bind("click", function () {

        if (imageUrl == "" || imageUrl == null) {
            alert('请先生成二维码');
        }
        Ext.Ajax.request({
            method: 'GET',
            sync: true,
            async: false,
            url: '/Module/Basic/Unit/Handler/UnitHandler.ashx?method=UploadImagerUrl&imageUrl=' + imageUrl,
            success: function (result, request) {
                var d = Ext.decode(result.responseText);
                if (d.success == true) {

                } else {

                }
            },
            failure: function (result) {
                showError(result.responseText);
            }
        });
    });

    $("select").change(function () {
        if ($(this).val() == '1') {
            // alert('文本');
            $("#text-display").show();
            $("#imageContentMessage").hide();
        }
        else {
            // alert('图文');
            $("#text").val('');
            $("#text-display").hide();
            $("#imageContentMessage").show();

        }
    });
}

function fnjqueryload(listMenutext) {
  //  debugger;
    var page =
        {
            saveType: "add", //保存菜单
            Name: "",  //图文名称
            TypeId: "",
            //默认控制条数
            currentPage: 0,
            //默认第一页
            pageParamJson: [],
            MenuId: "",
            //保存的MenuId
            //图文素材ID
            url: "/ApplicationInterface/Gateway.ashx",
            generateUrl: "",
            //图文关联后的url地址
            unionType: 0,
            count: 0,
            keyword: {  //关键字搜索使用
                'keyword': "",
                'pageIndex': 0,
                'pageSize': 6
            },
            //加载请求的个数
            toSave:
            {
                //针对系统模块
                domObj: null,
                //要保存的dom对象的下拉框
                moduleType: 0,
                //模块类型
                urlTemplate: "",
                //要传递替换的参数
                detailDomObj: ""
                //弹出层选择后要填充的层

            },
            statusDomobj: null,  //该对象用来保存状态的dom节点
            //关联到的类别
            elems:
            {
                keys: $("#keys"),    //关键字
                keyNo: $("#keyNo"),   //关键字序号
                theKey: $("#theKey"),  //关键字
                keywordsTable: $("#keywordsTable"),
                keyQuery: $("#keyQuery"),   //关键字查询
                imageContentDiv: $("#imageContentMessage"),    //所有的图文关联父层
                imageContentItems: $("#imageContentItems"),
                addImageMessageDiv: $("#addImageMessage"),  //弹出图文列表
                menuArea: $("#menuArea"),
                //菜单区域
                menuBar: $("#menuArea .menuWrap"),
                //菜单条
                menuTitle: $("#menuTitle"),
                //菜单名称
                menuNo: $("#no"),
                //菜单序号
                isUse: $("#isUse"),
                //是否启用
                message: $("#message"),
                //消息层
                messageSelect: $("#message .selectBox"),
                //消息类型选择下拉
                toUploadImage: $("#toUploadImage"),
                //要上传的图片
                imageContentMessage: $("#imageContentMessage"),
                //图文消息层
                contentArea: $("#contentArea"),
                //右边区域
                saveBtn: $("#saveBtn"),
                //保存菜单
                delBtn: $("#delBtn"),
                //删除菜单
                lottoryWay: $("#lottoryWay"),

                //抽奖方式
                lottoryWayInput: $("#lottoryWay .inputBox"),
                //抽奖方式选择
                saveData: $("#btnSaveData"),
                //标题
                weixinAccount: $("#weixinAccount"),
                //微信号
                accountSelect: $("#weixinAccount .selectBox"),
                imageCategory: $("#imageCategory"),
                //图文分类
                imageCategorySelect: $("#imageCategory .selectBox"),
                //图文分类下拉框
                btnUpload: $("#uploadImage"),
                //图片上传
                imageView: $("#upImage"),
                //上传之后的展示
                category: $("#category"),
                //分类选择
                linkUrl: $("#contentUrl"),
                //链接地址
                urlAddress: $("#urlAddress"),
                //链接
                contentEditor: $("#contentEditor"),
                //详情内容编辑
                moduleName: $("#moduleName"),
                //模块名
                moduleSelect: $("#moduleName .selectBox"),
                events: $("#eventsType"),
                //活动
                newsType: $("#newsType"),
                //资讯分类
                newsTypeSelect: $("#newsType .selectBox"),
                //资讯类别
                newsDetail: $("#newsDetail"),
                //选择详情资讯
                newsDetailSelect: $("#newsDetail .inputBox"),
                //详情选择框
                shopsType: $("#shopsType"),
                //门店分类
                shopsSelect: $("#shopsType .selectBox"),
                //门店分类的下拉列表
                eventSelect: $("#eventsType .selectBox"),
                //活动的选择框
                eventDetail: $("#eventsDetail"),
                //活动详情
                eventDetailSelect: $("#eventsDetail .inputBox"),
                //选择活动详情的下拉框
                uiMask: $("#ui-mask"),
                //遮罩层
                chooseEventsDiv: $("#chooseEvents")//选择活动层
            },
            popupOptions:
            {
                popupName: "",
                //弹出层   要输入的名称
                popupSelectName: "",
                //弹出层   下拉列表要显示的名称
                title: [],
                //弹出层    表格的title
                url: ""
                //弹出层的请求url   搜索的   分页的
            },
            //弹出类别后  确认之后的内容
            chooseOptions: {},
            //点击新增菜单 则把内容清空
            clearInput: function () {
                //序号
                this.elems.keyNo.val("");
                //关键字
                this.elems.theKey.val("");
                //文本设置为空
                $("#text").val("");
                //设置类型为文本
                this.elems.messageSelect.find("option[value='1']").attr("selected", "selected");
                //设置标识为elems的都隐藏
                this.hideElems();
                this.elems.message.removeClass("hide").addClass("show");
                this.elems.contentEditor.removeClass("hide").addClass("show");
            },
            init: function () {

                var that = this;
                var type = $.util.getUrlParam("type");
                //从地址栏获得每页显示的数据
                var pageSize = $.util.getUrlParam("psize");
                if (pageSize) {
                    this.pageSize = pageSize;
                } else {
                    this.pageSize = 10;
                }
                //表示新增图文消息
                //if (type == "add") {
                //填充所有的数据
                this.fillContent();

                this.initEvent();
                this.events.init();
            },
            //填充选择的系统模块
            fillSysModule: function (menuInfo) {
                var that = this;

                var moduleType = 0;
                var obj = {};
                var pageParamJson = {};
                var config = {};
                //事件选择触发
                if (typeof menuInfo == 'number') {
                    moduleType = menuInfo;
                } else {
                    flag = true;
                    obj = menuInfo;
                    pageParamJson = obj.PageParamJson;
                    pageParamJson = JSON.parse(pageParamJson);
                    if (pageParamJson.length) {
                        config = pageParamJson[0];  //底部关联的内容
                    }
                    unionTypeId = menuInfo.UnionTypeId;
                }
                //表示的是系统功能模块
                that.unionType = 3;
                //该选择框是否已经加载过数据
                var isLoad = that.elems.moduleSelect.attr("data-load");
                //未加载过  加载模块
                if (isLoad == "false") {
                    that.loadData.getSystemModule(function (data) {
                        var obj =
                        {
                            itemList: data.Data.SysModuleList
                        }
                        var html = bd.template("moduleTmpl", obj);
                        that.elems.moduleSelect.html(html);
                        var pageModule, pageCode;
                        //让模块选中
                        if (config.pageModule) {
                            pageModule = config.pageModule ? JSON.parse(config.pageModule) : {};
                            pageCode = pageModule.PageCode;
                            that.elems.moduleSelect.find("option[data-value='" + pageModule.PageID + "']").attr("selected", "selected");
                        }

                        var obj =
                            {
                                "EventList": 2,
                                //活动列表
                                "EventDetail": 3,
                                //活动详情
                                "EventLottory": 4,
                                //活动抽奖
                                "Recommend": 5,
                                //推荐有礼
                                "NewsList": 6,
                                //资讯列表
                                "NewsDetail": 7,
                                //资讯详情
                                "GoodsList": 8,
                                //商品列表
                                "ShopList": 9
                                //门店
                            }
                        var svalue = obj[pageCode] || 0;

                        switch (svalue) {
                            case 0:
                                //默认请选择的时候
                                that.hideElems(that.elems.moduleName);
                                break;
                            case 1:
                                //表示的是微商城
                                that.hideElems(that.elems.moduleName);
                                break;
                            case 2:
                                //表示的是活动列表

                                that.hideElems(that.elems.moduleName);
                                //获得活动类别
                                that.loadData.getEventType(function (data) {
                                    var obj =
                                        {
                                            itemList: data.Data.EventTypeList
                                        }
                                    //是否显示全部
                                    obj.showAll = false;
                                    var html = bd.template("eventTypeTmpl", obj);
                                    that.elems.eventSelect.html(html);
                                    //转换
                                    var pageType = JSON.parse(config.pageType);
                                    //让活动分类默认选中
                                    that.elems.eventSelect.find("option[data-value='" + pageType.EventTypeId + "']").attr("selected", "selected");

                                    //要选择的类别
                                    that.toSave.moduleType = 2;
                                    //活动列表
                                    //模板
                                    that.toSave.urlTemplate = pageModule.URLTemplate;
                                    that.toSave.domObj = that.elems.eventSelect;
                                });
                                that.elems.events.removeClass("hide").addClass("show");
                                break;
                            case 3:
                                //表示的是选择活动详情页
                                that.hideElems(that.elems.moduleName);
                                that.elems.eventDetail.removeClass("hide").addClass("show");
                                break;
                            case 4:
                                //资讯详情
                                var pageDetail = JSON.parse(config.pageDetail);
                                //保存模板
                                that.toSave.urlTemplate = pageModule.URLTemplate;
                                that.toSave.moduleType = 4;
                                //要保存的详情dom对象
                                that.toSave.domObj = that.elems.lottoryWayInput;
                                //默认名称显示
                                that.elems.lottoryWayInput.val(pageDetail.EventName);
                                //数据绑定
                                that.elems.lottoryWayInput.attr("data-value", config.pageDetail);
                                //活动抽奖
                                that.hideElems(that.elems.moduleName);
                                that.elems.lottoryWay.removeClass("hide").addClass("show");
                                break;
                            case 6:
                                //资讯列表是6
                                that.hideElems(that.elems.moduleName);
                                //获得资讯类别
                                that.loadData.getNewsTypeList(function (data) {
                                    var obj =
                                        {
                                            itemList: data.Data.NewsTypeList
                                        }
                                    //在模板里面设置是否显示全部的这个选项
                                    obj.showAll = false;
                                    var html = bd.template("NewsTypeTmpl", obj);
                                    that.elems.newsTypeSelect.html(html);
                                    //要选择的类别
                                    that.toSave.moduleType = 6;
                                    //转换
                                    var pageType = JSON.parse(config.pageType);
                                    //让资讯列表默认选中
                                    that.elems.newsTypeSelect.find("option[data-value='" + pageType.NewsTypeId + "']").attr("selected", "selected");
                                    //活动列表
                                    //模板
                                    that.toSave.urlTemplate = pageModule.URLTemplate;
                                    that.toSave.domObj = that.elems.newsTypeSelect;
                                });
                                that.elems.newsType.removeClass("hide").addClass("show");
                                break;
                            case 7:
                                //资讯详情
                                var pageDetail = JSON.parse(config.pageDetail);
                                //资讯详情
                                that.hideElems(that.elems.moduleName);
                                //保存模板
                                that.toSave.urlTemplate = pageModule.URLTemplate;
                                that.toSave.moduleType = 7;
                                //要保存的详情dom对象
                                that.toSave.domObj = that.elems.newsDetailSelect;
                                that.elems.newsDetail.removeClass("hide").addClass("show");
                                //默认名称显示
                                that.elems.newsDetailSelect.val(pageDetail.NewsName);
                                //数据绑定
                                that.elems.newsDetailSelect.attr("data-value", config.pageDetail);
                                that.elems.newsDetailSelect.removeClass("hide").addClass("show");
                                break;
                            case 9:
                                that.hideElems(that.elems.moduleName);
                                that.elems.shopsType.removeClass("hide").addClass("show");
                                break;
                        }
                    });
                }
                that.elems.moduleName.removeClass("hide").addClass("show");


            },
            //所有的数据请求默认加载数据    
            fillContent: function (edit) {

                var that = this;
                //填充微信账号
                this.loadData.getWeiXinAccount(function (data) {

                    var obj =
                    {
                        itemList: data.Data.AccountList
                    }
                    var html = bd.template("accountTmpl", obj);
                    that.elems.accountSelect.html(html);
                    //把applicationId保存起来
                    that.applicationId = data.Data.AccountList[0].ApplicationId;
                    // var EventID = new String(JITMethod.getUrlParam("EventID"));
                    that.loadData.searchKeywords(function (data) {

                        //                        debugger;
                        //                        var obj = {
                        //                            itemList: data.Data.KeyWordList
                        //                        };

                        //                        that.ReplyId = data.Data.KeyWordList.ReplyId;
                        //var EventID = new String(JITMethod.getUrlParam("EventID"));
                        var itemlist = [];
                        itemlist.MaterialTextIds = listMenutext
                        itemlist.ReplyType = "3";


                        that.showElementsByMessageType(itemlist);
                    });

                });



            },
            //显示遮罩层
            showMask: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.chooseEventsDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopUp(type);
                    this.elems.chooseEventsDiv.show();
                }
            },
            //加载更多的资讯或者活动
            loadMoreMaterial: function (currentPage) {
                var that = this;
                //设置当前页面
                page.pageIndex = currentPage - 1;
                this.loadData.getMaterialTextList("", function (data) {
                    var obj = {
                        pageSize: page.pageSize,
                        currentPage: currentPage,
                        allPage: data.Data.TotalPages,
                        showAdd: true,  //表示的一个标识
                        itemList: data.Data.MaterialTextList
                    }
                    var html = bd.template("addImageItemTmpl", obj);
                    that.elems.imageContentItems.html(html);
                });

            },
            //加载图文列表数据
            loadPopMatrialText: function () {
                var that = this;
                //加载图文类别
                this.loadData.getImageContentCategory(function (data) {

                    var obj = {
                        showAll: true,  //显示一个全部的选择项
                        itemList: data.Data.MaterialTextTypeList
                    }
                    var html = bd.template("optionTmpl", obj);
                    $("#imageCategory").html(html);
                });
                //获取图文列表
                this.loadData.getMaterialTextList("", function (data) {
                    var obj = {
                        pageSize: page.pageSize,
                        currentPage: 1,
                        allPage: data.Data.TotalPages,  //总页数
                        showAdd: true,  //表示的一个标识
                        itemList: data.Data.MaterialTextList
                    }
                    var html = bd.template("addImageItemTmpl", obj);
                    that.elems.imageContentItems.html(html);
                    //
                    that.events.initPagination(1, data.Data.TotalPages, function (page) {
                        that.loadMoreMaterial(page);
                    }, that.elems.addImageMessageDiv);
                });
            },
            //显示图文搜索的  进行获取
            showMatrialText: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.addImageMessageDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopMatrialText();
                    this.elems.addImageMessageDiv.show();
                }
            },
            stopBubble: function (e) {
                if (e && e.stopPropagation) {
                    //因此它支持W3C的stopPropagation()方法 
                    e.stopPropagation();
                }
                else {
                    //否则，我们需要使用IE的方式来取消事件冒泡 
                    window.event.cancelBubble = true;
                }
                e.preventDefault();
            },
            //所有活动相关
            events:
            {
                eventName: "",
                //活动名称
                eventTypeId: "",
                //活动类别id
                elems:
                {
                    eventsType: $("#pop_eventsType"),
                    //弹层的活动类别
                    eventName: $("#eventName"),
                    //活动名
                    btnSearchEvents: $("#searchEvents"),
                    //搜索活动按钮
                    btnCancel: $("#cancelBtn"),
                    //弹出层取消按钮
                    btnSave: $("#saveBtn")//弹出层保存按钮
                },
                init: function () {
                    //this.initPagination(1, 10);
                    this.initEvent();
                },
                initPagination: function (currentPage, allPage, callback, elems) {
                    elems.find('.pagination').remove();
                    var html = bd.template("pageTmpl", {});
                    elems.append(html);
                    elems.find('.pagination').jqPagination({
                        link_string: '/?page={page_number}',
                        current_page: currentPage,
                        //设置当前页 默认为1
                        max_page: allPage,
                        //设置最大页 默认为1
                        page_string: '当前第{current_page}页,共{max_page}页',
                        paged: function (page) {
                            //回发事件。。。
                            if (callback) {
                                callback(page);
                            }
                        }
                    });
                },
                //事件
                initEvent: function () {

                    var that = this;
                    //鼠标移动上去的效果
                    page.elems.chooseEventsDiv.delegate("tr", "mouseover", function (e) {
                        $(this).addClass("on");
                    }).delegate("tr", "mouseout", function () {
                        var $this = $(this);
                        if ($this.attr("choosed") != "true") {
                            $(this).removeClass("on");
                        }
                    });
                    //选中radio事件
                    page.elems.chooseEventsDiv.delegate("tr", "click", function () {
                        var $this = $(this);
                        $this.find("input[type=radio]").attr("checked", "checked");
                        //全部置为否
                        $this.siblings().removeClass("on").attr("choosed", "false");
                        //标记选中的
                        $this.addClass("on").attr("choosed", "true");
                    });
                    //开始搜索
                    page.elems.chooseEventsDiv.delegate("#searchEvents", "click", function (e) {
                        page.currentPage = 0;
                        var current = page.events.elems;
                        var eventName = current.eventName.val();
                        //填充活动名
                        page.events.eventName = current.eventName.val();
                        var eventType = current.eventsType.val();
                        //json对象
                        var jsonType = null;
                        if (eventType) {
                            jsonType = JSON.parse(eventType);
                            if (page.popupOptions.type == "chooseNews") {
                                //设置newsTypeId
                                page.events.eventTypeId = jsonType.NewsTypeId;
                            }
                            else if (page.popupOptions.type == "chooseEvents") {
                                //设置EventsTypeId
                                page.events.eventTypeId = jsonType.EventTypeId;
                            }

                        }
                        //根据类型来判断是取的什么接口数据
                        switch (page.popupOptions.type) {
                            //chooseNews  获取资讯列表  chooseEvents获取活动抽奖列表                                                                                                                                                                                                                                                                                                                                                                                                            


                            case "chooseNews":
                                page.popupOptions.type = "chooseNews";
                                page.loadData.getNewsList(function (data) {
                                    //资讯列表
                                    page.popupOptions.itemList = data.Data.NewsList ? data.Data.NewsList : [];
                                    var items = bd.template("itemTmpl", page.popupOptions);
                                    $("#itemsTable").html(items);
                                    //获得总共页数
                                    that.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                        that.loadMoreData(currentPage);
                                        //加载更多
                                    });
                                });
                                break;
                            case "chooseEvents":
                                page.popupOptions.type = "chooseEvents";
                                page.loadData.getEventList(function (data) {
                                    page.popupOptions.itemList = data.Data.EventList ? data.Data.EventList : [];
                                    var items = bd.template("itemTmpl", page.popupOptions);
                                    $("#itemsTable").html(items);
                                    //获得总共页数
                                    that.initPagination(1, data.Data.TotalPages, function (currentPage) {
                                        that.loadMoreData(currentPage);
                                        //加载更多
                                    });
                                });
                                break;

                        }

                        page.stopBubble(e);
                        return false;
                    });
                    //取消遮罩层
                    page.elems.chooseEventsDiv.delegate("#cancelBtn", "click", function (e) {
                        page.stopBubble(e);
                        page.showMask(false);


                        return false;
                    });
                    //保存数据
                    page.elems.chooseEventsDiv.delegate("#saveBtn", "click", function (e) {

                        //判断是否有选中
                        var jsonStr = $('#itemsTable input:radio[name="item"]:checked');
                        if (!jsonStr.length) {
                            alert("请选择一个" + page.popupOptions.tipsName + "!");
                            return;
                        }
                        jsonStr = jsonStr.val();
                        var jsonObj = JSON.parse(jsonStr);
                        //将内容保存
                        page.toSave.detailDomObj.attr("data-value", jsonStr);
                        if (page.popupOptions.type == "chooseNews") {
                            //让输入框的内容显示出选择的内容
                            page.toSave.detailDomObj.val(jsonObj.NewsName);
                        }
                        else if (page.popupOptions.type == "chooseEvents") {
                            //选择活动
                            page.toSave.detailDomObj.val(jsonObj.EventName);
                        }
                        page.showMask(false);
                        page.stopBubble(e);
                        return false;
                    });
                }
            },
            //根据类型让关联项的内容动态切换
            showElements: function (menuInfo) {
                var unionTypeId = 0, flag = false;
                //事件选择触发
                if (typeof menuInfo == 'number') {
                    unionTypeId = menuInfo;
                } else {
                    flag = true;
                    unionTypeId = menuInfo.UnionTypeId;
                }

                var that = this;
                switch (unionTypeId) {
                    case 0:
                        //默认请选择的时候
                        that.hideElems();
                        break;
                    case 1:
                        //表示的是链接
                        that.unionType = 1;
                        that.hideElems();
                        if (flag) {
                            //设置标识为elems的都隐藏
                            this.elems.urlAddress.val(menuInfo.MenuUrl);
                        } else {
                            this.elems.urlAddress.val("");
                        }
                        //链接
                        that.elems.linkUrl.removeClass("hide").addClass("show");
                        break;
                    case 2:
                        //表示的是回复消息
                        that.hideElems();
                        that.unionType = 2;
                        if (flag) {

                        } else {
                            //默认让文本选择项选中
                            that.elems.messageSelect.find("option[value='1']").attr("selected", "selected");
                        }
                        //控制文本类型默认显示
                        that.elems.contentEditor.removeClass("hide").addClass("show");
                        that.elems.message.removeClass("hide").addClass("show");


                        break;
                    case 3:
                        //表示的是系统功能模块
                        that.hideElems();
                        that.unionType = 3;
                        //该选择框是否已经加载过数据
                        var isLoad = that.elems.moduleSelect.attr("data-load");
                        that.fillSysModule(menuInfo);
                        that.elems.moduleName.removeClass("hide").addClass("show");
                        break;

                }
            },
            //根据消息类型   进行动态弹出
            showElementsByMessageType: function (keyInfo) {

                var that = this;
                var messageType = 0, flag = false;
                //事件选择触发
                if (typeof keyInfo == 'number') {
                    messageType = keyInfo;
                } else {
                    flag = true;
                    messageType = keyInfo.ReplyType;
                }
                switch (messageType - 0) {
                    case 0:
                        break;
                    case 1:
                        //表示的文本消息类型
                        that.messageType = 1;
                        that.hideElems();
                        if (flag) {
                            //设置文本
                            $("#text").val(keyInfo.Text);
                        } else {
                            $("#text").val("");
                        }
                        //控制文本类型默认显示
                        that.elems.contentEditor.removeClass("hide").addClass("show");
                        break;
                    case 3:
                        //表示的是图文消息类型
                        that.hideElems();
                        that.messageType = 3;
                        //去获得图文列表
                        var obj = {
                            pageSize: page.pageSize,
                            currentPage: 1,
                            allPage: 1,
                            showAdd: false,  //此标识为true为本地dom否则为从服务器获取的数据
                            itemList: keyInfo.MaterialTextIds
                        }
                        var html = bd.template("addImageItemTmpl", obj);
                        that.elems.imageContentMessage.find(".list").html(html);
                        //表示已经选择的图文数量
                        $("#hasChoosed").html(obj.itemList ? obj.itemList.length : 0);
                        that.elems.imageContentMessage.removeClass("hide").addClass("show");
                        break;

                }
                if ((messageType - 0) > 0) {
                    that.elems.messageSelect.find("option[value='" + (messageType) + "']").attr("selected", "selected");
                    that.elems.message.removeClass("hide").addClass("show");
                }
            },

            //加载更多的资讯或者活动
            loadMoreKeys: function (currentPage) {
                var that = this;
                //设置当前页面
                page.keyword.pageIndex = currentPage - 1;
                this.loadData.searchKeywords(function (data) {
                    var obj = {
                        itemList: data.Data.SearchKeyList
                    }
                    var html = bd.template("keywordItemTmpl", obj);
                    that.elems.keywordsTable.html(html);
                });

            },
            //获得关键字
            getKeyWords: function () {
                var that = this;
                page.keyword.pageIndex = 0;
                //输入的关键字
                var text = that.elems.keys.val();
                page.keyword.keyword = text;
                that.loadData.searchKeywords(function (data) {
                    var obj = {
                        itemList: data.Data.SearchKeyList
                    };
                    var html = bd.template("keywordItemTmpl", obj);
                    that.elems.keywordsTable.html(html);
                    that.events.initPagination(1, data.Data.TotalPages, function (page) {
                        that.loadMoreKeys(page);
                    }, that.elems.menuArea);
                });
            },
            //填充关键字详情
            fillKeyWordDetail: function (keyInfo) {

                var that = this;
                //关键字
                that.elems.theKey.val(keyInfo.KeyWord);
                //序号
                that.elems.keyNo.val(keyInfo.DisplayIndex);
                if (keyInfo.DisplayIndex == 0) {
                    //设置只读
                    that.elems.keyNo.attr("readOnly", "readOnly");
                } else {
                    //设置只读
                    that.elems.keyNo.removeAttr("readOnly");
                }
                //展示对应的消息类型

                this.showElementsByMessageType(keyInfo);

            },
            initEvent: function () {
                //初始化事件集
                var that = this;
                //关键字查询事件  按下enter
                this.elems.keys.keydown(function (e) {
                    if (e.keyCode == 13) {
                        that.getKeyWords();
                    }
                });
                //关键字查询事件
                this.elems.keyQuery.click(function () {
                    that.getKeyWords();
                });
                //点击关键字显示详细内容
                this.elems.keywordsTable.delegate("tr", "click", function (e) {
                    var $this = $(this);
                    var jsonStr = $this.attr("data-keyword");
                    //以便删除
                    that.elems.delBtn.attr("data-keyword", jsonStr);
                    //以便更新
                    that.elems.saveData.attr("data-keyword", jsonStr);
                    var jsonObj = JSON.parse(jsonStr);
                    //关键字ID
                    var keywordId = jsonObj.ReplyId;
                    that.saveType = "edit";
                    that.ReplyId = keywordId;
                    //详情
                    that.loadData.getKeywordDetail(keywordId, function (data) {

                        var keyInfo = data.Data.KeyWordList;
                        //填充菜单详情

                        that.fillKeyWordDetail(keyInfo);
                    });
                    that.stopBubble(e);
                });






                //拖拽排序
                that.elems.imageContentDiv.find(".list").sortable({
                    opacity: 0.7, cursor: 'move', update: function () { }
                });

                //鼠标悬停的时候把内容展示出来
                this.elems.menuArea.delegate("span", "mouseover", function (e) {
                    var $this = $(this);
                    $this.addClass("on").find(".subMenuWrap").show();
                    $this.siblings().removeClass("on").find(".subMenuWrap").hide();

                });
                //点击添加菜单事件
                this.elems.menuArea.delegate(".addBtn", "click", function (e) {

                    var $this = $(this);
                    //获得焦点
                    that.elems.theKey.focus();
                    page.saveType = "add";  //表示的要进行保存
                    //数据复原
                    that.clearInput();
                    that.stopBubble(e);
                });
                //点击获取图文的内容
                this.elems.imageContentDiv.delegate(".addBtn", "click", function () {
                    //获取全部
                    page.MaterialTypeId = "";
                    var $this = $(this);
                    that.showMatrialText(true);
                });
                //保存图文事件
                this.elems.addImageMessageDiv.delegate(".saveBtn", "click", function () {
                    that.showMatrialText(false);
                });
                //取消图文事件
                this.elems.addImageMessageDiv.delegate(".cancelBtn", "click", function () {
                    //再取消的时候把所有的删除

                    that.elems.imageContentDiv.find("[data-flag='add']").remove();
                    $("#hasChoosed").html(0);
                    that.showMatrialText(false);
                });

                //查询图文事件
                this.elems.addImageMessageDiv.delegate(".queryBtn", "click", function () {
                    var eventName = $("#theTitle").val();
                    var eventType = that.elems.imageCategory.val();
                    page.MaterialTextName = eventName;  //图文名称
                    page.MaterialTypeId = eventType;    //图文typeId
                    page.pageIndex = 0;  //只要查询就从头查询
                    that.loadData.getMaterialTextList("", function (data) {
                        var obj = {
                            pageSize: page.pageSize,
                            currentPage: 1,
                            allPage: data.Data.TotalPages,
                            showAdd: true,  //表示的一个标识
                            itemList: data.Data.MaterialTextList
                        }
                        var html = bd.template("addImageItemTmpl", obj);
                        that.elems.imageContentItems.html(html);
                        that.events.initPagination(1, data.Data.TotalPages, function (page) {
                            that.loadMoreMaterial(page);
                        }, that.elems.addImageMessageDiv);
                    });

                });
                //选择一个项则让他选中  同时在页面中展示出来
                this.elems.addImageMessageDiv.delegate(".item", "click", function () {
                    var $this = $(this);
                    var addId = $this.attr("data-id");
                    //已经有的图文数量
                    var hasLength = that.elems.imageContentDiv.find(".item").length;
                    if ($this.attr("isSelected") == "true") {  //表示已经选中则进行删除
                        $this.removeClass("on").attr("isSelected", "false");
                        $("#" + addId).remove();
                        //表示已经选择的图文数量
                        hasLength = hasLength - 1;
                    } else {
                        if (hasLength >= 10) {
                            alert("图文素材最多选择10个!\r\n不能继续添加!");
                            return false;
                        }
                        $this.addClass("on").attr("isSelected", "true");
                        var clone = $this.clone();
                        var domObj = that.elems.imageContentDiv.find("[data-id=" + addId + "]");
                        if (domObj.length) {
                            domObj.remove();
                            hasLength = hasLength - 1;
                        }
                        //给克隆后的节点设置id
                        clone.attr("id", $this.attr("data-id"));
                        //将选中的内容添加到图文层
                        that.elems.imageContentDiv.find(".list").append(clone);
                        //表示已经选择的图文数量
                        hasLength = hasLength + 1;
                    }

                    $("#hasChoosed").html(hasLength);
                    //if (Ext.getCmp("txtIsShare").value == true) {
                    //    $("#tabInfo").height(1390 + hasLength * 80);
                    //}
                    //else {
                    //    $("#tabInfo").height(1340 + hasLength * 80);
                    //}


                });
                //已经选择的图文列表鼠标移动上去出现删除的按钮
                this.elems.imageContentDiv.find(".list").delegate(".item", "mouseover", function () {
                    var $this = $(this);
                    $this.addClass("hover");

                }).delegate(".item", "mouseout", function () {
                    var $this = $(this);
                    $this.removeClass("hover");
                });
                //删除图文消息    一种是删除dom 一种是删除数据库里面的
                this.elems.imageContentDiv.find(".list").delegate(".delBtn", "click", function () {
                    var $this = $(this);
                    //是否是已经存储在数据库的
                    var itemDom = $this.parent().parent();
                    itemDom.remove();
                    var length = (that.elems.imageContentDiv.find(".item").length);
                    //表示已经选择的图文数量
                    $("#hasChoosed").html(length);
                    //var height = length * 80 + 1200
                    //if (Ext.getCmp("txtIsShare").value == true) {
                    //    $("#tabInfo").height(1390 + length * 80);
                    //}
                    //else {
                    //    $("#tabInfo").height(1350 + length * 80);

                    //}
                })


                //删除关键字
                this.elems.delBtn.bind("click", function () {
                    var $this = $(this);

                    //获得ReplyId 关键字ID
                    var dataKeyword = $this.attr("data-keyword");
                    if (!dataKeyword) {
                        alert("请先选择一个关键字再删除!");
                        return false;
                    }
                    var dataKeywordJson = JSON.parse(dataKeyword);
                    var replyId = dataKeywordJson.ReplyId;
                    //删除完成之后重新load数据
                    that.loadData.delKeyWord(replyId, function (data) {
                        //用来删除
                        that.elems.delBtn.removeAttr("data-keyword");
                        that.fillContent();
                        that.clearInput();

                        alert("关键字<" + dataKeywordJson.KeyWord + ">删除成功!");

                    });
                });
                //关联到类别选择
                this.elems.category.bind("change", function (e) {
                    var $this = $(this);
                    var value = $this.val() - 0;
                    //根据选择展示内容
                    that.showElements(value);
                });
                //根据消息类别
                this.elems.messageSelect.bind("change", function (e) {
                    var $this = $(this);
                    var value = $this.val() - 0;
                    that.showElementsByMessageType(value);
                });

                //活动类别点击事件
                this.elems.eventSelect.bind("click", function () { });
                //保存数据
                this.elems.saveData.bind("click", function () {
                    that.saveData($(this));
                });
                //保存菜单
                this.elems.saveBtn.bind("click", function () {
                    that.saveData();
                });
                //活动模块选择
                //此处为最复杂的级联逻辑
                this.elems.moduleSelect.bind("change", function () {
                    var $this = $(this);
                    var obj =
            {
                "EventList": 2,
                //活动列表
                "EventDetail": 3,
                //活动详情
                "EventLottory": 4,
                //活动抽奖
                "Recommend": 5,
                //推荐有礼
                "NewsList": 6,
                //资讯列表
                "NewsDetail": 7,
                //资讯详情
                "GoodsList": 8,
                //商品列表
                "ShopList": 9
                //门店
            }
                    var value = $this.val();
                    value = JSON.parse(value);
                    var pcode = value.PageCode;
                    var svalue = obj[pcode] || 0;
                    //根据选择的模块名称 判断urlTemplate是否需要构建PageParamJson   
                    // 类似于 /HtmlApps/html/_pageName_?eventType={eventType}
                    //则把eventType={eventType}  eventType取出来
                    if (value.URLTemplate) {
                        //获得所有的参数
                        var tmpArr = value.URLTemplate.match(/\{[\s\S]+?\}/g);
                        this.pageParamJson = [];
                        tmpArr = tmpArr ? tmpArr : [];
                        for (var i = 0; i < tmpArr.length; i++) {
                            var obj = {};
                            obj.key = tmpArr[i].replace("{", "").replace("}");
                            this.pageParamJson.push(obj);
                        }
                    }


                    switch (svalue) {
                        case 0:
                            //默认请选择的时候
                            that.hideElems(that.elems.moduleName);
                            //要选择的类别
                            that.toSave.moduleType = 0;
                            //模板
                            that.toSave.urlTemplate = value.URLTemplate;
                            that.toSave.domObj = that.elems.moduleSelect;
                            break;
                        case 1:
                            that.toSave.moduleType = 1;
                            //表示的是微商城
                            that.hideElems(that.elems.moduleName);
                            break;
                        case 2:
                            //表示的是活动列表

                            that.hideElems(that.elems.moduleName);
                            //获得活动类别
                            that.loadData.getEventType(function (data) {
                                var obj =
                        {
                            itemList: data.Data.EventTypeList
                        }
                                //是否显示全部
                                obj.showAll = false;
                                var html = bd.template("eventTypeTmpl", obj);
                                that.elems.eventSelect.html(html);
                                //要选择的类别
                                that.toSave.moduleType = 2;
                                //活动列表
                                //模板
                                that.toSave.urlTemplate = value.URLTemplate;
                                that.toSave.domObj = that.elems.eventSelect;
                            });
                            that.elems.events.removeClass("hide").addClass("show");
                            break;
                        case 3:
                            that.toSave.moduleType = 3;
                            //表示的是选择活动详情页
                            that.hideElems(that.elems.moduleName);
                            that.elems.eventDetail.removeClass("hide").addClass("show");
                            break;
                        case 4:
                            that.toSave.moduleType = 4;
                            //保存模板
                            that.toSave.urlTemplate = value.URLTemplate;
                            //要保存的详情dom对象
                            that.toSave.domObj = that.elems.lottoryWayInput;
                            //活动抽奖
                            that.hideElems(that.elems.moduleName);
                            that.elems.lottoryWay.removeClass("hide").addClass("show");
                            break;
                        case 6:
                            //资讯列表是6
                            that.hideElems(that.elems.moduleName);
                            //获得资讯类别
                            that.loadData.getNewsTypeList(function (data) {
                                var obj =
                        {
                            itemList: data.Data.NewsTypeList
                        }
                                //在模板里面设置是否显示全部的这个选项
                                obj.showAll = false;
                                var html = bd.template("NewsTypeTmpl", obj);
                                that.elems.newsTypeSelect.html(html);
                                //要选择的类别
                                that.toSave.moduleType = 6;
                                //活动列表
                                //模板
                                that.toSave.urlTemplate = value.URLTemplate;
                                that.toSave.domObj = that.elems.newsTypeSelect;
                            });
                            that.elems.newsType.removeClass("hide").addClass("show");
                            break;
                        case 7:
                            //资讯详情
                            that.hideElems(that.elems.moduleName);
                            //保存模板
                            that.toSave.urlTemplate = value.URLTemplate;
                            that.toSave.moduleType = 7;
                            //要保存的详情dom对象
                            that.toSave.domObj = that.elems.newsDetailSelect;
                            that.elems.newsDetail.removeClass("hide").addClass("show");
                            break;
                        case 9:
                            that.hideElems(that.elems.moduleName);
                            that.elems.shopsType.removeClass("hide").addClass("show");
                            break;
                    }
                });
                //选择活动详情的时候事件
                this.elems.eventDetailSelect.bind("click", function () {
                    that.showMask(true, 1);
                    //显示
                });
                //选择资讯详情的时候事件
                this.elems.newsDetailSelect.bind("click", function (e) {
                    //将点击的输入框保存起来
                    that.toSave.detailDomObj = $(this);
                    that.showMask(true, 3);
                    //显示
                });
                //选择活动抽奖的时候弹出层   然后没有抽奖详情
                this.elems.lottoryWayInput.bind("click", function () {
                    //将点击的输入框保存起来
                    that.toSave.detailDomObj = $(this);
                    that.showMask(true, 2);

                });
                //uimask 隐藏
                //this.elems.uiMask.bind("click", function () {
                //    that.showMask(false);
                //});
            },
            //隐藏元素
            hideElems: function (jqDom) {
                $('[name="elems"]').removeClass("show").addClass("hide");
                if (!!jqDom) {
                    jqDom.removeClass("hide").addClass("show");
                }
            },
            //保存数据
            saveData: function ($this) {
                var replyId = null, //关键字id
                keyword = null, //关键字
                text = null, //文本内容
                displayIndex = null, //排序
                applicationId = null, //申请接口主标识
                keywordType = null,  //关键字回复类型
                materialTextIds = null; //图文消息列表数组
                var dataKeyword = $this.attr("data-keyword");
                //关键字
                keyword = this.elems.theKey.val();
                if (keyword.length == 0) {
                    alert("请填写关键字");
                    return false;
                }
                if (keyword.length > 20) {
                    alert("关键字最长为20个字符!");
                    return false;
                }
                var objson = {};
                if (this.saveType != "add") {  //编辑的时候才进行判断
                    if (!dataKeyword) {
                        alert("请先选择一个关键字再操作!");
                        return false;
                    }
                    if (!dataKeyword) {
                        alert("请选择一个关键字修改后保存，或者新增关键字保存!");
                        return false;
                    }
                    objson = JSON.parse(dataKeyword);
                    //设置关键字ID
                    page.ReplyId = objson.ReplyId;
                } else {  //添加菜单
                    page.ReplyId = "";
                }
                //页面替换参数JSON
                var that = this;
                //接口标识
                applicationId = this.elems.accountSelect.val();
                displayIndex = this.elems.keyNo.val();
                if (displayIndex == "") {
                    alert("关键字序号不能为空!");
                    return false;
                }
                if (parseInt(displayIndex) >= 0) {
                    displayIndex = parseInt(displayIndex);
                } else {
                    alert("序号请输入大于或等于0的整数!");
                    return false;
                }
                this.unionType = this.elems.category.val();
                var messageType = 0;
                //标识的是回复消息
                if (this.unionType == 2) {
                    messageType = this.elems.messageSelect.val();
                    messageType = messageType - 0;
                    //文本内容
                    text = $("#text").val();
                    switch (messageType) {
                        case 1:
                            if (text == "") {
                                alert("文本内容不能为空!");
                                return false;
                            }
                            if (text.length > 2048) {
                                alert("文本的最大长度为2048");
                                return false;
                            }
                            break;
                        case 3:
                            //图文消息
                            var maxtrailDom = that.elems.imageContentDiv.find(".item");
                            if (maxtrailDom.length <= 0) {
                                alert("关键字关联的图文至少要添加一个图文!");
                                return false;
                            }
                            materialTextIds = [];
                            maxtrailDom.each(function (i, k) {
                                var obj = $(k).attr("data-obj");
                                obj = JSON.parse(obj);
                                materialTextIds.push({
                                    TestId: obj.TestId,
                                    DisplayIndex: (i + 1)
                                });
                            });
                            break;

                    }


                }
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'WX.KeyWord.SetKeyWord',
                        'KeyWordList': {
                            'ReplyId': page.ReplyId || "",   //关键字ID
                            'KeyWord': keyword,
                            'BeLinkedType': 2,
                            'DisplayIndex': displayIndex,
                            'ApplicationId': applicationId,
                            'KeywordType': 1,  //关键字回复类型  关键字回复
                            'Text': text,
                            'ReplyType': messageType,  //消息类型
                            'MaterialTextIds': materialTextIds
                        }
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            that.saveType = "add";  //设置为默认添加
                            page.ReplyId = data.Data.ReplyId;
                            alert("关键字<" + keyword + ">保存成功!");
                            //用来删除
                            that.elems.saveData.removeAttr("data-keyword");
                            that.fillContent();
                            that.clearInput();
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            }


        };

    page.loadData =
    {

        //删除关键字
        delKeyWord: function (ReplyId, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.KeyWord.DeleteKeyWord',
                    'ReplyId': ReplyId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得关键字的详细内容
        getKeywordDetail: function (ReplyId, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.KeyWord.GetKeyWord',
                    'ReplyId': ReplyId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //删除微信菜单
        delMenu: function (menuId, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Menu.DeleteMenu',
                    'MenuId': menuId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //搜索关键字
        searchKeywords: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.KeyWord.SearchKeyWord',
                    'ApplicationId': '386D08D106C849A9ACAA6E493D23E853',
                    'KeyWord': page.keyword.keyword,
                    'PageIndex': page.keyword.pageIndex,
                    'PageSize': 2//page.keyword.pageSize
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得Menu详情
        getMenuDetail: function (menuId, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Menu.GetMenuDetail',
                    'MenuId': menuId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得微信菜单
        getMenuList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Menu.GetMenuList',
                    'ApplicationId': page.applicationId
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得资讯明细
        getEventList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Event.GetEventList',
                    'EventTypeId': page.events.eventTypeId,
                    'EventName': page.events.eventName,

                    'BeginFlag': null,
                    //活动是否开始
                    'EndFlag': null,
                    //活动是否结束
                    'EventStatus': null,
                    //活动状态
                    'PageIndex': page.currentPage,
                    'PageSize': page.pageSize
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得资讯明细
        getNewsList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.News.GetNewsList',
                    'NewsTypeId': page.events.eventTypeId,
                    'NewsName': page.events.eventName,
                    'PageSize': page.pageSize,
                    'PageIndex': page.currentPage
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //WX. Event.GetDrawMethodList
        //获取活动抽奖方式
        getDrawMethodList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Event.GetDrawMethodList',
                    'EventTypeId': page.events.eventTypeId,
                    //抽奖；类型ID
                    'EventName': page.events.eventName,
                    'PageSize': page.pageSize,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得资讯类别
        getNewsTypeList: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.News.GetNewsTypeList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获取图文列表  点击图文列表的图文id的时候
        getMaterialTextList: function (id, callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.MaterialText.GetMaterialTextList',
                    'MaterialTextId': id,
                    'Name': page.MaterialTextName,  //图文名称
                    'TypeId': page.MaterialTypeId,   //图文id
                    'PageSize': page.pageSize,
                    'PageIndex': page.pageIndex || 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得图文分类数据
        getImageContentCategory: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.MaterialText.GetMaterialTextTypeList',
                    'ApplicationId': page.applicationId,
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得所有的微信账号
        getWeiXinAccount: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Account.GetAccountList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {
                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //获得系统模块
        getSystemModule: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Module.GetSysModuleList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {

                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        },
        //根据活动列表选择所有的活动类别
        getEventType: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'WX.Event.GetEventTypeList',
                    'PageSize': 10000,
                    'PageIndex': 0
                },
                success: function (data) {

                    if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        alert(data.Message);
                    }
                }
            });
        }
    }
    //初始化
    page.init();

}