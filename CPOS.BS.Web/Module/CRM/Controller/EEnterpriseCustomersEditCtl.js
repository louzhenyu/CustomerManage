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
    //            ////取返回值,注意后台设置的key,如果要取原值
    //            ////取缩略图地址
    //            //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');
    //            //Ext.getCmp("txtThumbnailImageUrl").setValue(getStr(thumUrl));

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

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/CRMHandler.ashx?mid=");

    Ext.getCmp("txtStatus").setDefaultValue("1");

    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_EEnterpriseCustomers_by_id",
            params: {
                id: id
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtName").setValue(getStr(d.EnterpriseCustomerName));
                Ext.getCmp("txtTypeId").setValue(getStr(d.TypeId));
                Ext.getCmp("txtIndustryId").jitSetValue(d.IndustryId);
                Ext.getCmp("txtAddress").jitSetValue(d.Address);
                Ext.getCmp("txtTel").jitSetValue(d.Tel);
                Ext.getCmp("txtECSourceId").jitSetValue(d.ECSourceId);
                Ext.getCmp("txtScaleId").jitSetValue(d.ScaleId);
                Ext.getCmp("txtStatus").setDefaultValue(d.Status);
                Ext.getCmp("txtRemark").jitSetValue(d.Remark);
                
                if (d.CityId != null && d.CityId.length > 0) {
                    Ext.getCmp("txtCity").jitSetValue([{ "id": d.CityId, "text": d.CityName}]);
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

function fnClose() {
    CloseWin('EEnterpriseCustomersEdit');
}

function fnSave() {
    var events = {};

    events.EnterpriseCustomerId = getUrlParam("id");
    events.EnterpriseCustomerName = Ext.getCmp("txtName").jitGetValue();
    events.TypeId = Ext.getCmp("txtTypeId").jitGetValue();
    events.IndustryId = Ext.getCmp("txtIndustryId").jitGetValue();
    events.CityId = Ext.getCmp("txtCity").jitGetValue();
    events.Address = Ext.getCmp("txtAddress").jitGetValue();
    events.Tel = Ext.getCmp("txtTel").jitGetValue();
    events.ECSourceId = Ext.getCmp("txtECSourceId").jitGetValue();
    events.ScaleId = Ext.getCmp("txtScaleId").jitGetValue();
    events.Status = Ext.getCmp("txtStatus").jitGetValue();
    events.Remark = Ext.getCmp("txtRemark").jitGetValue();

    if (events.EnterpriseCustomerName == null || events.EnterpriseCustomerName == "") {
        showError("必须输入名称");
        return;
    }
    if (events.TypeId == null || events.TypeId == "") {
        showError("必须输入客户类型");
        return;
    }
    if (events.IndustryId == null || events.IndustryId == "") {
        showError("必须输入所属行业");
        return;
    }
    if (events.CityId == null || events.CityId == "") {
        showError("必须输入所属省市");
        return;
    }
    //if (events.Tel == null || events.Tel == "") {
    //    showError("必须输入联系电话");
    //    return;
    //}
    if (events.ECSourceId == null || events.ECSourceId == "") {
        showError("必须输入来源");
        return;
    }
    if (events.Status == null || events.Status == "") {
        showError("必须输入状态");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/CRMHandler.ashx?method=EEnterpriseCustomers_save&id=' + events.EnterpriseCustomerId,
        params: {
            "item": Ext.encode(events)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                events.EnterpriseCustomerId = d.data;
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    //if (flag) fnClose();
    if (flag && getUrlParam("op") == "new") {
        location.href = "VipEnterpriseEdit.aspx?mid=&op=new&win=EEnterpriseCustomersEdit&ECId=" + events.EnterpriseCustomerId + 
            "&ECName=" + events.EnterpriseCustomerName;
    } else if (flag) {
        fnClose();
    }
}


