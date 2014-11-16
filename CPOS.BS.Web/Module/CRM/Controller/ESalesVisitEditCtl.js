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

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/CRMHandler.ashx?mid=");

    var id = new String(JITMethod.getUrlParam("SalesVisitId"));
    var SalesId = getUrlParam("SalesId");
    var SalesName = getUrlParam("SalesName");
    var EnterpriseCustomerId = getUrlParam("EnterpriseCustomerId");
    var EnterpriseCustomerName = getUrlParam("EnterpriseCustomerName");

    get("hSalesId").value = SalesId;
    Ext.getCmp("txtSalesName").jitSetValue(SalesName);
    get("hEnterpriseCustomerId").value = EnterpriseCustomerId;
    Ext.getCmp("txtEnterpriseCustomerId").jitSetValue(EnterpriseCustomerName);

    get("spanOpenUpload").style.display = "";

    if (id != "null" && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_ESalesVisit_by_id",
            params: {
                id: id
            },
            method: 'post',
            success: function (response) {
                //var d = Ext.decode(response.responseText).topics;
                //if (d != null) {
                //    //Ext.getCmp("txtSalesName").setValue(getStr(d.SalesName));
                //    //Ext.getCmp("txtEnterpriseCustomerId").setValue(getStr(d.EnterpriseCustomerName));
                //    //get("hECCustomerId").value = d.EnterpriseCustomerId;
                //    //Ext.getCmp("txtSalesProductId").jitSetValue(d.SalesProductId);
                //    //Ext.getCmp("txtEndDate").jitSetValue(d.EndDate);
                //    //Ext.getCmp("txtSalesPerson").jitSetValue(d.SalesPerson);
                //    //Ext.getCmp("txtSalesVipId").jitSetValue(d.SalesVipId);
                //    //Ext.getCmp("txtStageId").jitSetValue(d.StageId);
                //    //Ext.getCmp("txtPossibility").jitSetValue(d.Possibility);
                //    //Ext.getCmp("txtECSourceId").jitSetValue(d.ECSourceId);
                //    //Ext.getCmp("txtForecastAmount").jitSetValue(d.ForecastAmount);
                //    //Ext.getCmp("txtRemark").jitSetValue(d.Remark);

                //}
                //// image
                //fnLoadImage();

                //myMask.hide();
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
    CloseWin('ESalesVisitEdit');
}


//加载图片集合
fnLoadImage = function () {
    var store = Ext.getStore("itemEditImageStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=ObjectDownloads_query&ObjectId=" +
            getUrlParam("SalesVisitId"),
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
            var index = store.find("DownloadId", ids[idObj].toString().trim());
            store.removeAt(index);
            store.commitChanges();
        }
    }
}
//添加图片
function fnAddImageUrl() {
    var parentGrid = Ext.getCmp("gridImage");

    var item = {};
    item.DownloadId = newGuid();
    item.ObjectId = getUrlParam("SalesVisitId");
    item.DownloadUrl = get("hImage_DownloadUrl").value;
    item.DownloadName = Ext.getCmp("txtImage_DownloadName").jitGetValue();
    item.DisplayIndex = Ext.getCmp("txtImage_DisplayIndex").jitGetValue();

    if (item.DownloadUrl == null || item.DownloadUrl == "") {
        alert("请上传附件");
        return;
    }
    if (item.DownloadName == null || item.DownloadName == "") {
        alert("请输入附件名");
        return;
    }
    if (item.DisplayIndex == null || item.DisplayIndex == "") {
        alert("请输入排序");
        return;
    }

    var index = parentGrid.store.find("DownloadId", item.ImageId);
    if (index != -1) {
        alert("附件已存在");
        return;
    }
    for (var i = 0; i < parentGrid.store.data.items.length; i++) {
        if (parentGrid.store.data.items[i].data.DownloadId == item.DownloadId) {
            alert("附件已存在");
            return;
        }
    }

    parentGrid.store.add(item);
    parentGrid.store.commitChanges();
    get("hImage_DownloadUrl").value = "";
    get("txtImage_UploadStatus").innerHTML = "";
    Ext.getCmp("txtImage_DownloadName").jitSetValue("");
    Ext.getCmp("txtImage_DisplayIndex").jitSetValue("");
}

function fnSave() {
    var flag;
    var _gridImage = Ext.getStore("itemEditImageStore");
    var item = {};

    item.SalesVisitId = getUrlParam("SalesVisitId");
    //item.SalesVisitName = Ext.getCmp("txtSalesVisitName").jitGetValue();
    item.SalesId = getUrlParam("SalesId");
    item.VisitTypeId = Ext.getCmp("txtVisitTypeId").jitGetValue();
    item.VisitLog = Ext.getCmp("txtVisitLog").jitGetValue();
    item.StageId = Ext.getCmp("txtStageId").jitGetValue();
    item.ESalesVisitVipMappingIds = Ext.getCmp("txtSalesVisitVip").jitGetValue();

    // image
    item.ObjectDownloadsList = [];
    if (_gridImage.data.map != null) {
        for (var tmpItem in _gridImage.data.map) {
            var objData = _gridImage.data.map[tmpItem].data;
            var objItem = {};
            objItem.DownloadId = objData.DownloadId;
            objItem.ObjectId = item.Item_Id;
            objItem.DownloadUrl = objData.DownloadUrl;
            objItem.DownloadName = objData.DownloadName;
            objItem.DisplayIndex = objData.DisplayIndex;
            item.ObjectDownloadsList.push(objItem);
        }
    }

    if (item.VisitTypeId == null || item.VisitTypeId == "") {
        showError("请选择拜访方式");
        return;
    }
    if (item.ESalesVisitVipMappingIds == null || item.ESalesVisitVipMappingIds.length == 0) {
        showError("请填写拜访人群");
        return;
    }
    if (item.VisitLog == null || item.VisitLog == "") {
        showError("请填写拜访记录");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/CRM/Handler/CRMHandler.ashx?method=ESalesVisit_save&id=' + item.SalesVisitId,
        params: { "item": Ext.encode(item) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnLoad();

            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}


function fnUploadFile() {
	if (!Ext.getCmp("fileSpan").form.isValid()) {
		Ext.Msg.show({
			title: '提示',
			msg: "请先选择需要上传的文件!",
			buttons: Ext.Msg.OK,
			icon: Ext.Msg.WARNING
		});
		return false;
	}
	else {
		Ext.getCmp("fileSpan").getForm().submit({
			url: "/Framework/Upload/UploadFile.ashx",
			waitTitle: "请等待...",
			waitMsg: "上传中...",
			success: function (fp, o) {
				fnFileInput(Ext.getCmp("txt_Attach"));
                get("hImage_DownloadUrl").value = (o.result.file.url);
                //Ext.getCmp("txtVoiceSize").setValue(o.result.file.size);
                //Ext.getCmp("txtVoiceFormat").setValue(o.result.file.extension.substring(1));
				Ext.Msg.show({
					title: '提示',
					msg: "上传成功！",
					buttons: Ext.Msg.OK,
					icon: Ext.Msg.INFO
				});
                get("txtImage_UploadStatus").innerHTML = "上传成功";
			},
			failure: function (fp, o) {
				fnFileInput(Ext.getCmp("txt_Attach"));
				Ext.getCmp("txt_Attach").setValue('');
				Ext.Msg.show({
					title: '错误',
					msg: o.result.msg,
					buttons: Ext.Msg.OK,
					icon: Ext.Msg.ERROR
				});
			}
		})
	}
}

/*
*添加 修改 提交 后 统一将filefield控件替换为新的控件
*/
function fnFileInput(fileInput) {
	var me = fileInput,
        fileInput = me.isFileUpload() ? me.inputEl.dom : null, clone;
	if (fileInput) {
		clone = fileInput.cloneNode(true);
		try {
			//fileInput.parentNode.replaceChild(clone, fileInput);
			//因为parentNode有时候找不到 这里用jquery强制替换
			$("#txt_Attach-inputEl").replaceWith(clone);
		}
		catch (e) {
			$("#txt_Attach-inputEl").replaceWith(clone);
		}
		me.inputEl = Ext.get(clone);
	}
	return fileInput;
}

function fnCloseWin() {
    CloseWin('ESalesVisitEdit');
}

