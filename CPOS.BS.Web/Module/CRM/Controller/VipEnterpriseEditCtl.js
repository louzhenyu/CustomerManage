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

    var EnterpriseCustomerId = getUrlParam("ECId");
    var EnterpriseCustomerName = getUrlParam("ECName");
    if (EnterpriseCustomerId != "null" && EnterpriseCustomerId != "") {
        parent.Ext.getCmp("EEnterpriseCustomersEdit").setTitle("联系人");
        Ext.getCmp("txtEnterpriseCustomerId").setValue(EnterpriseCustomerName);
        get("hECCustomerId").value = EnterpriseCustomerId;
    }

    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_VipEnterprise_by_id",
            params: {
                id: id
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtName").setValue(getStr(d.VipName));
                Ext.getCmp("txtGender").setDefaultValue(getStr(d.Gender));
                Ext.getCmp("txtEnterpriseCustomerId").setValue(d.EnterpriseCustomerName);
                get("hECCustomerId").value = d.EnterpriseCustomerId;
                Ext.getCmp("txtDepartment").jitSetValue(d.Department);
                Ext.getCmp("txtPosition").jitSetValue(d.Position);
                Ext.getCmp("txtPhone").jitSetValue(d.Phone);
                Ext.getCmp("txtFax").jitSetValue(d.Fax);
                Ext.getCmp("txtEmail").jitSetValue(d.Email);
                Ext.getCmp("txtPDRoleId").jitSetValue(d.PDRoleId);
                Ext.getCmp("txtStatus").setDefaultValue(d.Status);
                Ext.getCmp("txtRemark").jitSetValue(d.PersonDesc);

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
    var win = getUrlParam("win");
    if (win == undefined || win == null || win == "") win = "VipEnterpriseEdit";
    CloseWin(win);
}

function fnSave() {
    var events = {};
    events.Vip = {};
    
    events.VipId = getUrlParam("id");
    events.EnterpriseCustomerId = get("hECCustomerId").value;
    events.Vip.VipName = Ext.getCmp("txtName").jitGetValue();
    events.Vip.Gender = Ext.getCmp("txtGender").jitGetValue();
    events.Department = Ext.getCmp("txtDepartment").jitGetValue();
    events.Position = Ext.getCmp("txtPosition").jitGetValue();
    events.Vip.Phone = Ext.getCmp("txtPhone").jitGetValue();
    events.Fax = Ext.getCmp("txtFax").jitGetValue();
    events.Vip.Email = Ext.getCmp("txtEmail").jitGetValue();
    events.Status = Ext.getCmp("txtStatus").jitGetValue();
    events.PDRoleId = Ext.getCmp("txtPDRoleId").jitGetValue();
    events.PersonDesc = Ext.getCmp("txtRemark").jitGetValue();

    if (events.Vip.VipName == null || events.Vip.VipName == "") {
        showError("必须输入姓名");
        return;
    }
    if (events.Vip.Gender == null || events.Vip.Gender == "") {
        showError("必须输入性别");
        return;
    }
    if (events.EnterpriseCustomerId == null || events.EnterpriseCustomerId == "") {
        showError("必须输入所属客户");
        return;
    }
    if (events.Department == null || events.Department == "") {
        showError("必须输入部门");
        return;
    }
    if (events.Position == null || events.Position == "") {
        showError("必须输入职务");
        return;
    }
    if (events.Vip.Phone == null || events.Vip.Phone == "") {
        showError("必须输入联系电话");
        return;
    }
    if (events.PDRoleId == null || events.PDRoleId == "") {
        showError("必须输入决策作用");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/CRMHandler.ashx?method=VipEnterprise_save&id=' + events.VipId,
        params: {
            "item": Ext.encode(events)
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


