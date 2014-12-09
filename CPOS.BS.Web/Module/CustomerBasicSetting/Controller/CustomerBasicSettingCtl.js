var K;
var htmlEditor;
var KE
var KE1;
var kApp;
var kAppBack;
var imageurl;
var imagecfurl;
var forwardingMessageLogourl;
var uploadAppLogoImageUrl;
var uploadAppTopBackgroundImageUrl;
var kindeditorArray = new Array();
var isAld;
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();


    JITPage.HandlerUrl.setValue("Handler/CustomerBasicSettingHander.ashx?mid=" + __mid);
    fnIsAld();
    fnloadStore();
    fnView();
    fnEditView();

    $("#cmbCustomerType").css('margin-top', '10px');
    $("#txtCustomerMobile").css('margin-top', '10px');
    $("#label-1012").css('margin-left', '10px');
    $("#label-1018").css('margin-left', '10px');



    fnUploadImage();
    fnUploadCuImage();
    fnUploadForwardingImage();
    fnUploadAppLogoImage();
    fnUploadAppBackgroundLogoImage();
});

function fnIsAld() {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=IsAld",
        async: false,
        method: 'post',
        success: function (response) {
            var isald = response.responseText;
            if (isald == "1") {

                isAld = isald;
            }
            else {
                Ext.getCmp("App").setVisible(false);
                isAld = isald;
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

function fnEditView() {
    $("textarea[name='kindeditorcontent']").each(function (i, obj) {
        var editor = KindEditor.create(obj, {
            resizeType: 2,
            uploadJson: "/Framework/Javascript/Other/editor/EditorFileHandler.ashx?method=EditorFile&FileUrl=CustomerBasicSetting", //FileUrl自己定义文件夹，你的是CustomerBasicSetting
            allowFileManager: false
        });
        editor.html(obj.value);
        kindeditorArray.push(editor);
    });

}

function fnView() {

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetCustomerList",
        async: false,
        method: 'post',
        success: function (response) {
            var data = Ext.decode(response.responseText);
            Ext.getCmp("txtuserCode").jitSetValue(data.data.loadInfo.customerCode);
            Ext.getCmp("txtuserName").jitSetValue(data.data.loadInfo.customerName);
            Ext.getCmp("txtuserID").jitSetValue(data.data.loadInfo.customerID);
            if (data.data.requset != null) {
                for (var i = 0; i < data.data.requset.length; i++) {
                    var code = data.data.requset[i].SettingCode;
                    if (code == 'WebLogo' || code == 'VipCardLogo' || code == "ForwardingMessageLogo") {
                        if (code == "WebLogo" && data.data.requset[i].SettingValue != "") {

                            imageurl = data.data.requset[i].SettingValue;
                            $('#logopicture').attr('src', data.data.requset[i].SettingValue)
                        }
                        if (code == "VipCardLogo" && data.data.requset[i].SettingValue != "") {

                            imagecfurl = data.data.requset[i].SettingValue;
                            $('#vippicture').attr('src', data.data.requset[i].SettingValue)
                        }
                        if (code == "ForwardingMessageLogo" && data.data.requset[i].SettingValue != "") {
                            forwardingMessageLogourl = data.data.requset[i].SettingValue;
                            $('#ForwardingMessageLogopicture').attr('src', data.data.requset[i].SettingValue)
                        }
                    }
                    else if (code == 'CustomerType' || code == 'IsSearchAccessoriesStores' || code == 'IsAllAccessoriesStores') {
                        var cmp = Ext.getCmp("cmb" + data.data.requset[i].SettingCode);
                        if (cmp != undefined) {
                            cmp.jitSetValue(getStr(data.data.requset[i].SettingValue));
                        }
                    }
                    else {
                        if (code == "AboutUs" || code == "BrandStory" || code == "BrandRelated" || code == "MemberBenefits" || code == "WhatCommonPoints" || code == "GetPoints" || code == "SetSalesPoints") {

                            var cmp = Ext.getCmp("txt" + data.data.requset[i].SettingCode);
                            if (cmp != undefined && data.data.requset[i].SettingValue != "" && data.data.requset[i].SettingValue != null) {
                                cmp.jitSetValue(getStr(data.data.requset[i].SettingValue));

                            }
                        }
                        else {
                            var cmp = Ext.getCmp("txt" + data.data.requset[i].SettingCode);
                            if (cmp != undefined) {
                                cmp.jitSetValue(getStr(data.data.requset[i].SettingValue));
                            }
                        }
                    }
                    if (isAld.toString() == "1") {
                        if (code == "AppLogo") {
                            $('#AppLogo').attr('src', data.data.requset[i].SettingValue);
                            uploadAppLogoImageUrl = data.data.requset[i].SettingValue;
                        }
                        else if (code == "AppTopBackground") {
                            $('#AppTopBackground').attr('src', data.data.requset[i].SettingValue);
                            uploadAppTopBackgroundImageUrl = data.data.requset[i].SettingValue;
                        }
                    }

                }
            }

        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });

}

function fnSave() {
    if (kindeditorArray.length > 0) {//把editor的值给到对应的原始控件
        for (var i = 0; i < kindeditorArray.length; i++) {
            kindeditorArray[i].sync(); //同步
        }
    }

    var NewsTypeName = Ext.getCmp("txtAboutUs").jitGetValue();
    //if (NewsTypeName == null || NewsTypeName == "") {
    //    Ext.Msg.alert("提示", "请输入关于我们");
    //    return;
    //}
    var NewsTypeName = Ext.getCmp("txtBrandStory").jitGetValue();
    //if (NewsTypeName == null || NewsTypeName == "") {
    //    Ext.Msg.alert("提示", "请输入品牌故事");
    //    return;
    //}
    var NewsTypeName = Ext.getCmp("txtBrandRelated").jitGetValue();
    //if (NewsTypeName == null || NewsTypeName == "") {
    //    Ext.Msg.alert("提示", "请输入品牌相关");
    //    return;
    //}
    var IntegralAmountPer = Ext.getCmp("txtIntegralAmountPer").jitGetValue();
    if (IntegralAmountPer == null || IntegralAmountPer == "") {
        Ext.Msg.alert("提示", "请输入积分抵用金额的比率");
        return;
    }

    var SMSSign = Ext.getCmp("txtSMSSign").jitGetValue();
    if (SMSSign == null || SMSSign == "") {
        Ext.Msg.alert("提示", "请输入手机短信签名");
        return;
    }
    var ForwardingMessageTitle = Ext.getCmp("txtForwardingMessageTitle").jitGetValue();
    if (ForwardingMessageTitle == null || ForwardingMessageTitle == "") {
        Ext.Msg.alert("提示", "请输入转发消息默认摘要文字");
        return;
    }
    var WhatCommonPoints = Ext.getCmp("txtWhatCommonPoints").jitGetValue();
    if (WhatCommonPoints == null || WhatCommonPoints == "") {
        Ext.Msg.alert("提示", "请输入什么是通用积分");
        return;
    }
    var GetPoints = Ext.getCmp("txtGetPoints").jitGetValue();
    if (GetPoints == null || GetPoints == "") {
        Ext.Msg.alert("提示", "请输入如何获得积分");
        return;
    }
    var SetSalesPoints = Ext.getCmp("txtSetSalesPoints").jitGetValue();
    if (SetSalesPoints == null || SetSalesPoints == "") {
        Ext.Msg.alert("提示", "请输入如何消费积分");
        return;
    }

    if (forwardingMessageLogourl == null || forwardingMessageLogourl == "") {
        Ext.Msg.alert("提示", "请上传转发消息图标");
        return;

    }
    var NewsTypeName = Ext.getCmp("txtCustomerMobile").jitGetValue();
    if (NewsTypeName == null || NewsTypeName == "") {
        Ext.Msg.alert("提示", "请输入400电话");
        return;
    }
    else {
        re = /^((\d{3,4}-)*\d{7,8}(-\d{3,4})*|13\d{9}|(400)*\d{7,8}(-\d{3,4})*|(400)*\d{7,8}(-\d{3,4})*)$/  //匹配电话正则 
        if (!re.test(NewsTypeName)) {
            Ext.Msg.alert("提示", "请输入正确的客服电话");
            return;
        }
    }

    var NewsTypeName = Ext.getCmp("txtMemberBenefits").jitGetValue();
    if (NewsTypeName == null || NewsTypeName == "") {
        Ext.Msg.alert("提示", "请输入会员权益");
        return;
    }
    if (imageurl == null || imageurl == "") {
        Ext.Msg.alert("提示", "请上传客户Logo");
        return;
    }
    if (imagecfurl == null || imagecfurl == "") {
        Ext.Msg.alert("提示", "请上传会员图片");
        return;
    }
    var cmbCustomerType = Ext.getCmp("cmbCustomerType").jitGetValue();
    if (cmbCustomerType == null || cmbCustomerType == "") {
        Ext.Msg.alert("提示", "请选择客户分类");
        return;
    }
    if (isAld == "1") {
        if (uploadAppLogoImageUrl == null || uploadAppLogoImageUrl == "") {
            Ext.Msg.alert("提示", "请上传主页Logo");
            return;
        }
        if (uploadAppTopBackgroundImageUrl == null || uploadAppTopBackgroundImageUrl == "") {
            Ext.Msg.alert("提示", "请上传头部背景图片");
            return;
        }
    }
    var amountEnd = Ext.getCmp("txtAmountEnd").jitGetValue();
    if (amountEnd!=""&& isNaN(amountEnd))
    {
        Ext.Msg.alert("提示", "配送费的订单金额大于等于的值必须是数值类型");
        return;
    }
    var deliveryAmount = Ext.getCmp("txtDeliveryAmount").jitGetValue();
    if (deliveryAmount!="" && isNaN(deliveryAmount)) {
        Ext.Msg.alert("提示", "收取的配送费金额必须是数值类型");
        return;
    }

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=SaveustomerBasicrInfo",
        params: {
            form: Ext.JSON.encode(Ext.getCmp("cuserinfo").getValues()),
            form1: Ext.JSON.encode(Ext.getCmp("cuserinfocf").getValues()),
            form2: Ext.JSON.encode(Ext.getCmp("cuserserch").getValues()),
            form3: Ext.JSON.encode(Ext.getCmp("App").getValues()),
            imageurl: imageurl,
            imagecfurl: imagecfurl,
            forwardingMessageLogourl: forwardingMessageLogourl,
            aboutUs: Ext.getCmp("txtAboutUs").jitGetValue(),
            brandStory: Ext.getCmp("txtBrandStory").jitGetValue(),
            brandRelated: Ext.getCmp("txtBrandRelated").jitGetValue(),
            whatCommonPoints: Ext.getCmp("txtWhatCommonPoints").jitGetValue(),
            getPoints: Ext.getCmp("txtGetPoints").jitGetValue(),
            setSalesPoints: Ext.getCmp("txtSetSalesPoints").jitGetValue(),
            memberBenefits: Ext.getCmp("txtMemberBenefits").jitGetValue(),
            appLogo: uploadAppLogoImageUrl,
            appTopBackground: uploadAppTopBackgroundImageUrl
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            if (jdata.ResponseData.success) {
                var logo = $('#img_logo');
                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.ResponseData.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        myMask.hide()
                        logo.attr('src', imageurl).css({ 'margin-top': 'auto', 'max-width': '139px', 'max-height': '62px' });
                    }
                });
            }
            else {

                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            }
            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            myMask.hide();
        }


    });
}


function fnUploadImage() {

    //    //上传图片
    KE1 = KindEditor;
    var uploadbutton = KE1.uploadbutton({
        button: KE1("#uploadImageUrl")[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

                //取原图地址
                var url = data.url;
                imageurl = url;
                $('#logopicture').attr('src', url);


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

function fnUploadCuImage() {

    //    //上传图片
    KE = KindEditor;
    var uploadbutton = KE.uploadbutton({
        button: KE("#uploadCusImageUrl")[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

                var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                //取原图地址
                var url = data.url;
                imagecfurl = url;
                $('#vippicture').attr('src', url);


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

function fnUploadForwardingImage() {

    //    //上传图片
    KE = KindEditor;
    var uploadbutton = KE.uploadbutton({
        button: KE("#uploadForwardingImageUrl")[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

                var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                //取原图地址
                var url = data.url;
                forwardingMessageLogourl = url;
                $('#ForwardingMessageLogopicture').attr('src', url);


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

function fnUploadAppLogoImage() {
    //    //上传图片
    kApp = KindEditor;
    var uploadbutton = KE1.uploadbutton({
        button: KE1("#uploadAppLogoImageUrl")[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');
                //取原图地址
                var url = data.url;
                uploadAppLogoImageUrl = url;
                $('#AppLogo').attr('src', url);


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

function fnUploadAppBackgroundLogoImage() {
    //    //上传图片
    kAppBack = KindEditor;
    var uploadbutton = KE1.uploadbutton({
        button: KE1("#uploadAppTopBackgroundImageUrl")[0],
        //上传的文件类型
        fieldName: 'imgFile',
        //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
        url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
        afterUpload: function (data) {
            if (data.error === 0) {
                alert('图片上传成功');

                //取原图地址
                var url = data.url;
                uploadAppTopBackgroundImageUrl = url;
                $('#AppTopBackground').attr('src', url);


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

function fnloadStore() {

    Ext.getStore("customeTypeStore").removeAll();
    Ext.getStore("customeTypeStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetCousCustomerType";
    Ext.getStore("customeTypeStore").load();

}