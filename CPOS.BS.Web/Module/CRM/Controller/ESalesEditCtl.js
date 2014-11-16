var K;
var htmlEditor, SalesId;

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
    fnLoad();
});

function fnLoad() {
    var id = "";
    if (SalesId == undefined || SalesId== null || SalesId.length == 0 || SalesId == "null") {
        id = getUrlParam("SalesId");
        SalesId = id;
    }
    else {
        id = SalesId;
    }
    if (id != "null" && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_ESales_by_id",
            params: {
                id: id
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;
                if (d != null) {
                    Ext.getCmp("txtSalesName").setValue(getStr(d.SalesName));
                    Ext.getCmp("txtEnterpriseCustomerId").setValue(getStr(d.EnterpriseCustomerName));
                    get("hECCustomerId").value = d.EnterpriseCustomerId;
                    Ext.getCmp("txtSalesProductId").jitSetValue(d.SalesProductId);
                    Ext.getCmp("txtEndDate").jitSetValue(d.EndDate);
                    Ext.getCmp("txtSalesVipId").jitSetValue(d.SalesVipId);
                    Ext.getCmp("txtStageId").jitSetValue(d.StageId);
                    Ext.getCmp("txtPossibility").jitSetValue(d.Possibility);
                    Ext.getCmp("txtECSourceId").jitSetValue(d.ECSourceId);
                    Ext.getCmp("txtForecastAmount").jitSetValue(d.ForecastAmount);
                    Ext.getCmp("txtRemark").jitSetValue(d.Remark);

                }
                fnLoadVisit();

                //myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        //myMask.hide();
    }

}
function fnClose() {
    CloseWin('ESalesEdit');
}

function fnLoadVisit() {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=ESalesVisit_query",
        params: {
            SalesId: getUrlParam("SalesId"),
            page: 1,
            pageSize: 1000
        },
        method: 'post',
        success: function (response) {
            var list = Ext.decode(response.responseText).topics;

            var str = "";
            for (var i = 0; i < list.length; i++) {
                var time = getDate(list[i].CreateTime);
                if (time.length > 4) time = time.substring(0, time.length-3);
                var text = list[i].VisitLog;
                if (text.length > 40) text = text.substring(0, 40) + "...";
                str += "<div class=\"ywBoxList\">";
                if (text.length > 40) str += "<span class=\"HideIcn\" onclick=\"fnShowVisit(this)\"></span>";
                str += "<h2 class=\"ywBoxH2\">● "+ time +"</h2>";
                str += "<p class=\"ywBoxp\" title=\""+ list[i].VisitLog +"\">"+ text +"</p>";
                str += "<div class=\"ywBoxDiv\"><a href=\"#\" onclick=\"fnViewESalesVisitVip('" + list[i].SalesVisitId + "')\">显示全部联系人（"+ list[i].VipCount +"）</a>";
                
                if (list[i].ObjectCount > 0)
                str += "<a href=\"#\" style=\"margin-left:20px;\" onclick=\"fnViewObjectDownloads('" + list[i].SalesVisitId + "')\">查看全部附件（"+ list[i].ObjectCount +"）</a></div>";
                str += "</div>";
            }
            get("pnlVisitList").innerHTML = str;
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });

}

function fnShowVisit(obj) {
    if (obj.className == "HideIcn") {
        obj.className = "showIcn";
        obj.parentNode.childNodes[2].innerHTML = obj.parentNode.childNodes[2].title;
    } else {
        obj.className = "HideIcn";
        if (obj.parentNode.childNodes[2].title.length > 40)
            obj.parentNode.childNodes[2].innerHTML = obj.parentNode.childNodes[2].title.substring(0, 40) + "...";
        else
            obj.parentNode.childNodes[2].innerHTML = obj.parentNode.childNodes[2].title;
    }
}

function fnSave() {
    var events = {};
    
    events.SalesId = SalesId;
    if (SalesId== undefined || SalesId== null || SalesId.length == 0 || SalesId == "null")
        events.SalesId = getUrlParam("SalesId");
    events.EnterpriseCustomerId = get("hECCustomerId").value;
    events.SalesName = Ext.getCmp("txtSalesName").jitGetValue();
    events.SalesProductId = Ext.getCmp("txtSalesProductId").jitGetValue();
    events.EndDate = Ext.getCmp("txtEndDate").jitGetValue();
    events.SalesVipId = Ext.getCmp("txtSalesVipId").jitGetValue();
    events.StageId = Ext.getCmp("txtStageId").jitGetValue();
    events.Possibility = Ext.getCmp("txtPossibility").jitGetValue();
    events.ECSourceId = Ext.getCmp("txtECSourceId").jitGetValue();
    events.ForecastAmount = Ext.getCmp("txtForecastAmount").jitGetValue();
    events.Remark = Ext.getCmp("txtRemark").jitGetValue();

    if (events.SalesName == null || events.SalesName == "") {
        showError("必须输入销售线索名称");
        return;
    }
    if (events.SalesProductId == null || events.SalesProductId == "") {
        showError("必须输入销售产品");
        return;
    }
    if (events.EndDate == null || events.EndDate == "") {
        showError("必须输入结束日期");
        return;
    }
    if (events.EnterpriseCustomerId == null || events.EnterpriseCustomerId == "") {
        showError("必须输入所属客户");
        return;
    }
    if (events.StageId == null || events.StageId == "") {
        showError("必须输入阶段");
        return;
    }
    if (events.Possibility == null || events.Possibility == "") {
        showError("必须输入可能性");
        return;
    }
    if (events.ECSourceId == null || events.ECSourceId == "") {
        showError("必须输入业务机会来源");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/CRMHandler.ashx?method=ESales_save&id=' + events.SalesId,
        params: {
            "item": Ext.encode(events)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                SalesId = d.data;
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
}

fnCreateSalesVisit = function() {
    if (SalesId == undefined || SalesId == null || SalesId =="" || SalesId == "null") {
        alert("请先保存销售线索");
        return;
    }
    var SalesName = Ext.getCmp("txtSalesName").jitGetValue();
    var EnterpriseCustomerId = get("hECCustomerId").value;
    var EnterpriseCustomerName = Ext.getCmp("txtEnterpriseCustomerId").rawValue;
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "ESalesVisitEdit",
        title: "业务拜访",
        url: "ESalesVisitEdit.aspx?op=new" + "&SalesId=" + SalesId + "&SalesName=" + SalesName
         + "&EnterpriseCustomerId=" + EnterpriseCustomerId + "&EnterpriseCustomerName=" + EnterpriseCustomerName
    });
    win.show();
}
fnViewObjectDownloads = function(id) {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "ObjectDownloads",
        title: "附件",
        url: "ObjectDownloads.aspx?op=new" + "&ObjectId=" + id
    });
    win.show();
}
fnViewESalesVisitVip = function(id) {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "ESalesVisitVip",
        title: "拜访人",
        url: "ESalesVisitVip.aspx?op=new" + "&SalesVisitId=" + id
    });
    win.show();
}

