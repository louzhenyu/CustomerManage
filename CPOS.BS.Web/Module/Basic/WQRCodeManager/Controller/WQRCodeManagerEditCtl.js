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
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WQRCodeManagerHandler.ashx?mid="); 
    
    
    ////上传图片
    //KE = KindEditor;
    //var uploadbutton = KE.uploadbutton({
    //    button: KE('#uploadImage')[0],
    //    //上传的文件类型
    //    fieldName: 'imgFile',
    //    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
    //    url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=65&height=61',
    //    afterUpload: function (data) {
    //        if (data.error === 0) {
    //            alert('图片上传成功');
    //            //取返回值,注意后台设置的key,如果要取原值
    //            //取缩略图地址
    //            //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
    //            //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(data.thumUrl));

    //            //取原图地址
    //            //var url = KE.formatUrl(data.url, 'absolute');
    //            Ext.getCmp("txtImageUrl").setValue(getStr(data.url));
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

    
    var wQRCodeManager_id = new String(JITMethod.getUrlParam("wQRCodeManager_id"));
    if (wQRCodeManager_id != "null" && wQRCodeManager_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_wQRCodeManager_by_id",
            params: { wQRCodeManager_id: wQRCodeManager_id },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtQRCode").setValue(getStr(d.QRCode));
                //Ext.getCmp("txtQRCodeTypeId").setValue(getStr(d.QRCodeTypeId));
                //Ext.getCmp("txtApplicationId").setDefaultValue(getStr(d.ApplicationId));
                Ext.getCmp("txtApplicationId").fnLoad(function(){
                    Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));
                    Ext.getCmp("txtQRCodeTypeId").setDefaultValue(getStr(d.QRCodeTypeId));
                });
                Ext.getCmp("txtIsUse").setDefaultValue(getStr(d.IsUse));
                Ext.getCmp("txtRemark").setValue(getStr(d.Remark));
                Ext.getCmp("txtImageUrl").setValue(getStr(d.ImageUrl));
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
    CloseWin('WQRCodeManagerEdit');
}

function fnSave() {
    var wQRCodeManager = {};

    var wQRCodeManager_Id = getUrlParam("wQRCodeManager_id");
    wQRCodeManager.QRCode = Ext.getCmp("txtQRCode").getValue();
    wQRCodeManager.QRCodeTypeId = Ext.getCmp("txtQRCodeTypeId").getValue();
    wQRCodeManager.Remark = Ext.getCmp("txtRemark").getValue();
    wQRCodeManager.IsUse = Ext.getCmp("txtIsUse").getValue();
    wQRCodeManager.ApplicationId = Ext.getCmp("txtApplicationId").getValue();
    wQRCodeManager.ImageUrl = Ext.getCmp("txtImageUrl").getValue();
    
    if (wQRCodeManager.QRCode == null || wQRCodeManager.QRCode == "") {
        showError("请填写二维码号码");
        return;
    }
    if (wQRCodeManager.QRCodeTypeId == null || wQRCodeManager.QRCodeTypeId == "") {
        showError("请填写类型");
        return;
    }
    if (wQRCodeManager.IsUse == null || wQRCodeManager.IsUse == "") {
        showError("请填写使用状态");
        return;
    }
    if (wQRCodeManager.ApplicationId == null || wQRCodeManager.ApplicationId == "") {
        showError("请填写微信公众平台");
        return;
    }
        
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/WQRCodeManagerHandler.ashx?method=wQRCodeManager_save&wQRCodeManager_id=' + wQRCodeManager_Id,
        params: { "wQRCodeManager": Ext.encode(wQRCodeManager) },
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


function fnLoadWQRCodeManagers(app_sys_id, wQRCodeManager_id) {
    //alert(app_sys_id);return;
    get("treeWQRCodeManager").innerHTML = "";
    app_sys_id = getStr(app_sys_id);
    wQRCodeManager_id = getStr(wQRCodeManager_id);
    var wQRCodeManagers_data = { };
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/Basic/WQRCodeManager/Handler/WQRCodeManagerHandler.ashx?method=get_sys_wQRCodeManagers_by_wQRCodeManager_id&wQRCodeManager_id=' + wQRCodeManager_id + 
            '&app_sys_id=' + app_sys_id,
        params: { },
        sync: true,
        async : false,
        success: function(result, request) {
            var d =  Ext.decode(result.responseText);
            if (d.data != null) {
                wQRCodeManagers_data = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure : function() {
            showInfo("页面超时，请重新登录");
        }
    });

    var storeWQRCodeManagers = Ext.create('Ext.data.TreeStore', {
        root: {
            expanded: true,
            children: wQRCodeManagers_data
        },
        fields: [
            {name: 'id', type: 'string', mapping: 'WQRCodeManager_Id'},
            {name: 'text', type: 'string', mapping: 'WQRCodeManager_Name'},
            {name: 'checked', type: 'boolean', mapping: 'check_flag'},
            {name: 'expanded', type: 'boolean', mapping: 'expanded_flag'},
            {name: 'leaf', type: 'boolean', mapping: 'leaf_flag'},
            {name: 'cls', type: 'string', mapping: 'cls_flag'}
        ]
    });

    treeWQRCodeManager = Ext.create('Ext.tree.Panel', {
        store: storeWQRCodeManagers,
        rootVisible: false,
        useArrows: true,
        frame: false,
        title: '',
        renderTo: 'treeWQRCodeManager',
        width: 372,
        height: 268
    });
}

function fnGetWXCode() {

    var UnitId = getUrlParam("unit_id");
    var WXCode = Ext.getCmp("txtQRCode").getValue();
    var WeiXinId = Ext.getCmp("txtApplicationId").jitGetValue();
    //document.getElementById("CED14CFEFEBD446DAD5BF2D116CBB5FB").value = "3";
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        async: false,
        url: '/Module/Basic/WQRCodeManager/Handler/WQRCodeManagerHandler.ashx?method=SetUnitWXCode&WXCode=' + WXCode + '&WeiXinId=' + WeiXinId + '&UnitId=' + UnitId,
        //params: { "unit": Ext.encode(unit) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                //document.getElementById("CED14CFEFEBD446DAD5BF2D116CBB5FB").value = d.status;
                Ext.getCmp("txtImageUrl").jitSetValue(getStr(d.msg));
                document.getElementById("imgView").src = d.msg;
 //               document.getElementById("wxDownload").href = d.msg;
//                alert(d.status);
//                alert(d.msg);
                showSuccess("生成二维码成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

function savepic() {
    response.addHeader("Content-Disposition", "attachment; filename=" + response.encodeURL(downloadfile));  
    if (document.all.a1 == null) {
        objIframe = document.createElement("IFRAME");
        document.body.insertBefore(objIframe);
        objIframe.outerHTML = "<iframe name=a1 style='width:400px;hieght:300px' src=" + imageName.href + "></iframe>";
        re = setTimeout("savepic()", 1)
    }
    else {
        clearTimeout(re)
        pic = window.open(imageName.href, "a1")
        pic.document.execCommand("SaveAs")
        document.all.a1.removeNode(true)
    }
}


