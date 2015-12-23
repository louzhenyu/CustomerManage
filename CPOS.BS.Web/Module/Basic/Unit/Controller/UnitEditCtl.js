var t_unit_id = "";

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //上传图片
    UploadImage();
    $("#Txt_Map").attr("width", "350px")
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/UnitHandler.ashx?mid=");

    var unit_id = getUrlParam("unit_id");
    if (unit_id == null || unit_id == "") {
        fnjqueryload(null);
    }

    if (unit_id != "null" && unit_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_unit_by_id",
            params: { unit_id: unit_id },
            method: 'POST',
            success: function (response) {

                var storeId = "unitEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;

                if (d.Parent_Unit_Id != null) {
                    Ext.getCmp("txtParentUnit").jitSetValue([{ "id": d.Parent_Unit_Id, "text": d.Parent_Unit_Name}]);
                }

                Ext.getCmp("txtUnitName").jitSetValue(getStr(d.Name));
                Ext.getCmp("txtUnitCode").jitSetValue(getStr(d.Code));
                Ext.getCmp("txtUnitEnglish").jitSetValue(getStr(d.EnglishName));
                Ext.getCmp("txtUnitShortName").jitSetValue(getStr(d.ShortName));

                Ext.getCmp("txtUnitType").jitSetValue(getStr(d.TypeId));

                Ext.getCmp("txtCity").jitSetValue([{ "id": d.CityCode, "text": d.CityName}]);

                Ext.getCmp("txtPostcode").jitSetValue(getStr(d.Postcode));
                Ext.getCmp("txtContact").jitSetValue(getStr(d.Contact));
                Ext.getCmp("txtTelephone").jitSetValue(getStr(d.Telephone));
                Ext.getCmp("txtFax").jitSetValue(getStr(d.Fax));
                Ext.getCmp("txtEmail").jitSetValue(getStr(d.Email));
                Ext.getCmp("txtLongitude").jitSetValue(getStr(d.longitude + ',' + d.dimension));
                Ext.getCmp("txtAddress").jitSetValue(getStr(d.Address));
                Ext.getCmp("txtRemark").jitSetValue(getStr(d.Remark));

                Ext.getCmp("txtCreateUserName").setValue(getStr(d.Create_User_Name));
                Ext.getCmp("txtCreateTime").setValue(getStr(d.Create_Time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.Modify_User_Name));
                Ext.getCmp("txtModifyTime").setValue(getStr(d.Modify_Time));


                WXCodeImageUrl = d.WXCodeImageUrl;
                if (WXCodeImageUrl != null && WXCodeImageUrl != '') {
                    $("#txtRQcode").val(WXCodeImageUrl);
                    var image = '<img id="imgView" alt="" src=\"' + WXCodeImageUrl + '\"width="256px" height="256px">';
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
                        else {
                            fnjqueryload(null);
                        }
                    }
                    else {
                        fnjqueryload(null);
                    }
                }
                else {
                    fnjqueryload(null);
                }

                //                Ext.getCmp("txtUnitFlag").setValue(d.Flag); //配送商的价格 qianzhi 20130708

                //Ext.getCmp("txtWeiXinId").jitSetValue(getStr(d.weiXinId));
                //                Ext.getCmp("txtWeiXinId").fnLoad(function () {
                //                    Ext.getCmp("txtWeiXinId").jitSetValue(getStr(d.weiXinId));
                //                    Ext.getCmp("txtWXCode").setDefaultValue(getStr(d.QRCodeTypeId));
                //                    Ext.getCmp("txtWXCode2").jitSetValue(getStr(d.WXCode));
                //                });

                //                Ext.getCmp("txtWebserversURL").jitSetValue(getStr(d.webserversURL));
                //                Ext.getCmp("txtDimensionalCodeURL").jitSetValue(getStr(d.dimensionalCodeURL));
                Ext.getCmp("txtImageUrl").setValue(getStr(d.imageURL));
                //                Ext.getCmp("txtFtpImagerURL").setValue(getStr(d.ftpImagerURL));

                //                Ext.getCmp("txtUnitSort").jitSetValue(d.UnitSortIdList);
                document.getElementById("imgView").src = d.dimensionalCodeURL;
                //document.getElementById("wxDownload").href = d.dimensionalCodeURL;
                // prop
                loadPropData(d.PropertyList, "UNIT");

                // image
                fnLoadImage();

                // Brand
                fnLoadBrand();

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

fnLoadRole = function () {
    var store = Ext.getStore("unitEditRoleStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_unit_role_info_by_unit_id&unit_id=" +
            getUrlParam("unit_id"),
        params: { start: 0, limit: 0 }
    });
}

function UploadImage() {
    //图片路径
    KE = KindEditor;
    var uploadbutton = KE.uploadbutton({
        button: KE('#uploadImageUrl')[0],
        //上传的文件类型
        fieldName: 'imgFile',
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx?dir=image',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

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

    //二维码图片
    KE1 = KindEditor;
    var uploadbutton1 = KE1.uploadbutton({
        button: KE1('#uploadDimensionalCodeURL'),
        //上传的文件类型
        fieldName: 'imgFile',
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx?dir=image',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

                //取原图地址
                //var url = KE.formatUrl(data.url, 'absolute');
                //                Ext.getCmp("txtDimensionalCodeURL").setValue(getStr(data.url));
            } else {
                alert(data.message);
            }
        },
        afterError: function (str) {
            alert('自定义错误信息: ' + str);
        }
    });
    uploadbutton1.fileBox.change(function (e) {
        uploadbutton1.submit();
    });

    //上传图片
    KE2 = KindEditor;
    var uploadbutton2 = KE2.uploadbutton({
        button: KE2('#uploadImage2')[0],
        //上传的文件类型
        fieldName: 'imgFile',
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_json.ashx?dir=image',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

                //取原图地址
                var url = data.url;
                Ext.getCmp("txtImage_ImageUrl").setValue(getStr(url));
            } else {
                alert(data.message);
            }
        },
        afterError: function (str) {
            alert('自定义错误信息: ' + str);
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
        url: JITPage.HandlerUrl.getValue() + "&method=get_item_image_info_by_unit_id&unit_id=" +
            getUrlParam("unit_id"),
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
        if (ids[idObj] != null) {
            var index = store.find("ImageId", ids[idObj]);
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
    item.ObjectId = getUrlParam("unit_id");
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



//加载Brand集合
fnLoadBrand = function () {
    var store = Ext.getStore("itemEditBrandStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_item_brand_info_by_unit_id&unit_id=" +
            getUrlParam("unit_id"),
        params: { start: 0, limit: 0 }
    });
}
//删除图片
function fnDeleteItemBrand(id) {
    var store = Ext.getStore("itemEditBrandStore");
    if (id == undefined || id == null || id.length <= 2) {
        showInfo("请选择品牌");
        return;
    };

    var ids = id.split(',');
    for (var idObj in ids) {
        if (ids[idObj] != null) {
            var index = store.find("MappingId", ids[idObj]);
            store.removeAt(index);
            store.commitChanges();
        }
    }
}
//添加Brand
function fnAddBrand() {
    var parentGrid = Ext.getCmp("gridBrand");

    var item = {};
    item.MappingId = newGuid();
    item.StoreId = getUrlParam("unit_id");
    //    item.BrandId = Ext.getCmp("txtBrand_List").jitGetValue();
    //    item.BrandName = Ext.getCmp("txtBrand_List").rawValue;

    if (item.BrandId == null || item.BrandId == "") {
        alert("请选择品牌");
        return;
    }

    var index = parentGrid.store.find("BrandId", item.BrandId);
    if (index != -1) {
        alert("品牌已存在");
        return;
    }
    for (var i = 0; i < parentGrid.store.data.items.length; i++) {
        if (parentGrid.store.data.items[i].data.MappingId == item.MappingId) {
            alert("品牌已存在");
            return;
        }
    }

    parentGrid.store.add(item);
    parentGrid.store.commitChanges();
}


function fnClose() {
    CloseWin('UnitEdit');
}

function fnSave() {

    var unit_id = getUrlParam("unit_id");

    var flag;
    var grid = Ext.getStore("unitEditRoleStore");
    var _gridImage = Ext.getStore("itemEditImageStore");
    var _gridBrand = Ext.getStore("itemEditBrandStore");
    var unit = {};

    var hdParentUnitCtrl = Ext.getCmp("txtParentUnit");
    if (hdParentUnitCtrl != null && hdParentUnitCtrl != "") {
        if (hdParentUnitCtrl.jitGetValue() == unit_id) {
            showError("上级单位不能选择自身!");
            return;
        }
    }

    var tbUnitCodeCtrl = Ext.getCmp("txtUnitCode");
    var tbUnitNameCtrl = Ext.getCmp("txtUnitName");
    var tbUnitEnglishCtrl = Ext.getCmp("txtUnitEnglish");
    var tbUnitShortNameCtrl = Ext.getCmp("txtUnitShortName");
    var hdUnitTypeCtrl = Ext.getCmp("txtUnitType");
    var hdCityCtrl = Ext.getCmp("txtCity");
    var tbPostcodeCtrl = Ext.getCmp("txtPostcode");
    var tbContactCtrl = Ext.getCmp("txtContact");
    var tbTelephoneCtrl = Ext.getCmp("txtTelephone");
    var tbFaxCtrl = Ext.getCmp("txtFax");
    var tbEmailCtrl = Ext.getCmp("txtEmail");
    var tbLongitudeCtrl = Ext.getCmp("txtLongitude");
    var tbDimensionCtrl = Ext.getCmp("txtDimension");
    var tbAddressCtrl = Ext.getCmp("txtAddress");
    var tbRemarkCtrl = Ext.getCmp("txtRemark");

    var unit_id = getUrlParam("unit_id");
    if (unit_id == undefined || unit_id == null || unit_id.length == 0) unit_id = t_unit_id;
    if (unit_id == undefined || unit_id == null || unit_id.length == 0) unit_id = newGuid();
    unit.Id = unit_id;
    //unit.Parent_Unit_Id = hdParentUnitCtrl.selectedValues.length > 0 ? hdParentUnitCtrl.selectedValues[0].id : null; 
    //unit.CityId = hdCityCtrl.selectedValues.length > 0 ? hdCityCtrl.selectedValues[0].id : null;

    unit.Parent_Unit_Id = hdParentUnitCtrl.jitGetValue();
    unit.CityId = hdCityCtrl.jitGetValue();

    //    alert("ParentId: " + unit.Parent_Unit_Id);
    //    alert("CityId: " + unit.CityId);
    //    return;
    unit.Code = tbUnitCodeCtrl.jitGetValue();
    unit.Name = tbUnitNameCtrl.jitGetValue();
    unit.EnglishName = tbUnitEnglishCtrl.jitGetValue();
    unit.ShortName = tbUnitShortNameCtrl.jitGetValue();
    unit.TypeId = hdUnitTypeCtrl.jitGetValue();
    unit.Postcode = tbPostcodeCtrl.jitGetValue();
    unit.Contact = tbContactCtrl.jitGetValue();
    unit.Telephone = tbTelephoneCtrl.jitGetValue();
    unit.Fax = tbFaxCtrl.jitGetValue();
    unit.Email = tbEmailCtrl.jitGetValue();
    var mapPos = tbLongitudeCtrl.jitGetValue().split(',');
    if (mapPos != null && mapPos.length == 2) {
        unit.longitude = mapPos[0];
        unit.dimension = mapPos[1];
    }
    unit.Address = tbAddressCtrl.jitGetValue();
    unit.Remark = tbRemarkCtrl.jitGetValue();
    //    unit.Flag = Ext.getCmp("txtUnitFlag").getValue();   //配送商的价格 qianzhi 20130708
    //    unit.weiXinId = Ext.getCmp("txtWeiXinId").jitGetValue();
    //    unit.webserversURL = Ext.getCmp("txtWebserversURL").jitGetValue();
    //    unit.dimensionalCodeURL = Ext.getCmp("txtDimensionalCodeURL").jitGetValue();
    unit.imageURL = Ext.getCmp("txtImageUrl").getValue();
    //    unit.ftpImagerURL = Ext.getCmp("txtFtpImagerURL").getValue();

    //    unit.UnitSortIdList = Ext.getCmp("txtUnitSort").jitGetValue();

    // prop
    unit.PropertyList = savePropData("UNIT");


    // image
    unit.ItemImageList = [];
    if (_gridImage.data.map != null) {
        for (var tmpItem in _gridImage.data.map) {
            var objData = _gridImage.data.map[tmpItem].data;
            var objItem = {};
            objItem.ImageId = objData.ImageId;
            objItem.ObjectId = unit.Id;
            objItem.ImageURL = objData.ImageURL;
            objItem.DisplayIndex = objData.DisplayIndex;
            unit.ItemImageList.push(objItem);
        }
    }

    // Brand
    unit.ItemBrandList = [];
    if (_gridBrand.data.map != null) {
        for (var tmpItem in _gridBrand.data.map) {
            var objData = _gridBrand.data.map[tmpItem].data;
            var objItem = {};
            objItem.MappingId = objData.MappingId;
            objItem.StoreId = unit.Id;
            objItem.BrandId = objData.BrandId;
            unit.ItemBrandList.push(objItem);
        }
    }

    //        if (unit.Parent_Unit_Id == null || unit.Parent_Unit_Id == "") {
    //            showError("请选择上级单位");
    //            return;
    //        }

    //二维码 保存记录

    var txtRQcode = $("#txtRQcode").val();

    var selectVal = $("#Valueselect option:selected").val();
    unit.MaxWQRCod = MaxWQRCod;
    unit.WXCodeImageUrl = WXCodeImageUrl;
    if (selectVal == '1') {
        if (txtRQcode != "" && txtRQcode != null) {
            var text = $("#text").val();
            if (text == "" || text == null) {
                showError('文本编辑不能为空!');
                return;
            }
            else if (text.length >= 1000) {
                alert('文本编辑长度不能超过1000!');
                return;
            }
        }
        unit.text = text;
        unit.ReplyType = 1;
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
        unit.listMenutextMapping = materialTextIds;
        unit.ReplyType = "3";
    }

    if (unit.Name == null || unit.Name == "") {
        showError("请填写门店名称");
        return;
    }

    if (unit.TypeId == null || unit.TypeId == "") {
        showError("请选择类型");
        return;
    }
    if (unit.Code == null || unit.Code == "") {
        showError("请填写门店编码");
        return;
    }
    if (unit.CityId == null || unit.CityId == "") {
        showError("请选择城市");
        return;
    }
    if (unit.Address == null || unit.Address == "") {
        showError("请填写地址");
        return;
    }
    if (unit.Contact == null || unit.Contact == "") {
        showError("请填写联系人");
        return;
    }
    //if (unit.Telephone == null || unit.Telephone == "") {
    //    showError("请填写电话");
    //    return;
    //}
    //else {
    //    re = /^((\d{3,4}-)*\d{7,8}(-\d{3,4})*|13\d{9}|(400)*\d{7,8}(-\d{3,4})*|(400)*\d{7,8}(-\d{3,4})*)$/  //匹配电话正则 
    //    if (!re.test(unit.Telephone)) {
    //        Ext.Msg.alert("提示", "请按照提示输入正确的电话");
    //        return;
    //    }
    //}


    if (unit.PropertyList == null || unit.PropertyList == "") {
        return; //这里不需要提示信息，因为getPropInfo内部做了消息通知
    }
    if (unit.longitude == null || unit.longitude == "") {
        showError("请选择经纬度");
        return;
    }
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Unit/Handler/UnitHandler.ashx?method=unit_save&unit_id=' + unit_id,
        params: {
            "unit": Ext.encode(unit)


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
    CloseWin('UnitEdit');
}

function fnGetWXCode() {

    var UnitId = getUrlParam("unit_id");
    //    var WXCode = Ext.getCmp("txtWXCode").jitGetValue();
    // var WeiXinId = Ext.getCmp("txtWeiXinId").jitGetValue();
    if (UnitId == undefined || UnitId == null || UnitId.length == 0) {
        UnitId = newGuid();
        t_unit_id = UnitId;
    }
    if (WXCode == null || WXCode == "") {
        alert("固定二维码类型不能为空");
        return;
    }
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        async: false,
        url: '/Module/Basic/Unit/Handler/UnitHandler.ashx?method=SetUnitWXCode&WXCode=' + WXCode + '&WeiXinId=' + WeiXinId + '&UnitId=' + UnitId,
        //params: { "unit": Ext.encode(unit) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError(d.msg);
                flag = false;
            } else {
                //                Ext.getCmp("txtDimensionalCodeURL").jitSetValue(getStr(d.msg));
                document.getElementById("imgView").src = d.msg;
                //                Ext.getCmp("txtWXCode2").jitSetValue(getStr(d.data));
                //               document.getElementById("wxDownload").href = d.msg;
                //                alert(d.status);
                //                alert(d.msg);
                //showSuccess("生成二维码成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError(result.responseText);
        }
    });
}



var WXCodeImageUrl = "";
var MaxWQRCod = "";
function bindEvent() {
    $("#createCode").bind("click", function () {
        var txtRQcode = $("#txtRQcode").val();
        if (txtRQcode != "" && txtRQcode != null) {
            showError("二维码已生成,不能重复生成!");
            return;
        }
        var UnitId = getUrlParam("unit_id");
        Ext.Ajax.request({
            method: 'GET',
            sync: true,
            async: false,
            url: '/Module/Basic/Unit/Handler/UnitHandler.ashx?method=CretaeWxCode&unit_id='+UnitId,
            success: function (result, request) {

                var d = Ext.decode(result.responseText);
                if (d.success == true) {
                    WXCodeImageUrl = d.data.imageUrl;
                    $("#txtRQcode").val(WXCodeImageUrl);
                    var image = '<img id="imgView" alt="" src=\"' + WXCodeImageUrl + '\"width="256px" height="256px">';
                    $("#image").html(image);
                    MaxWQRCod = d.data.MaxWQRCod;

                } else {
                    Ext.Msg.alert("提示",d.msg);
                }
            },
            failure: function (result) {
                showError(result.responseText);
            }
        });

    });

    $("#downCode").bind("click", function () {
        
        var imageUrl = $("#txtRQcode").val();
        // window.open('/Module/Basic/Item/Handler/ItemHandler.ashx?method=download_qrcode&item_id=' + item_id + '&item_name=' + item_name);
        window.open('/Module/Basic/Unit/Handler/UnitHandler.ashx?method=UploadImagerUrl&imageUrl=' + imageUrl);
    
        //Ext.Ajax.request({
        //    method: 'GET',
        //    sync: true,
        //    async: false,
        //    url: '/Module/Basic/Unit/Handler/UnitHandler.ashx?method=UploadImagerUrl&imageUrl=' + imageUrl,
        //    success: function (result, request) {

        //        var d = Ext.decode(result.responseText);
        //        console.log(result.responseText);
        //        if (d.success == true) {
                    
        //        } else {

        //        }
        //    },
        //    failure: function (result) {
        //        showError(result.responseText);
        //    }
        //});
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


