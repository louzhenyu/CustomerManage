var K;
var htmlEditor;
var up = true;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");

    get("spanOpenUpload").style.display = "none";

    var Id = getUrlParam("Id");
    if (Id != "null" && Id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_WMaterialVoice_by_id",
            params: { Id: Id },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtVoiceName").jitSetValue(getStr(d.VoiceName));
                Ext.getCmp("txtVoiceUrl").jitSetValue(getStr(d.VoiceUrl));
                Ext.getCmp("txtVoiceSize").jitSetValue(getStr(d.VoiceSize));
                Ext.getCmp("txtVoiceFormat").jitSetValue(getStr(d.VoiceFormat));
                //Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));

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
    CloseWin('WMaterialVoiceEdit');
}

function fnSave() {
    var flag;
    var item = {};
    item.VoiceId = getUrlParam("Id");
    item.ModelId = getUrlParam("ModelId");
    item.VoiceName = Ext.getCmp("txtVoiceName").jitGetValue();
    item.VoiceUrl = Ext.getCmp("txtVoiceUrl").jitGetValue();
    item.VoiceSize = Ext.getCmp("txtVoiceSize").jitGetValue();
    item.VoiceFormat = Ext.getCmp("txtVoiceFormat").jitGetValue();
    item.ApplicationId = getUrlParam("ApplicationId");

    if (item.VoiceName == null || item.VoiceName == "") {
        showError("请填写名称");
        return;
    }
    if (item.VoiceUrl == null || item.VoiceUrl == "") {
        showError("请填写链接地址");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WMaterialVoice_save&Id=' + item.VoiceId, 
        params: {
            "item": Ext.encode(item)
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
    CloseWin('WMaterialVoiceEdit');
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
	else if (!Ext.Array.contains(["mp3", "wma", "wav", "amr"], 
        Ext.getCmp("txt_Attach").getValue().split(".").pop())) {
		Ext.Msg.show({
			title: '提示',
			msg: "只能上传后缀名为 mp3, wma, wav, amr 的文件！",
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
                Ext.getCmp("txtVoiceUrl").setValue(o.result.file.url);
                Ext.getCmp("txtVoiceSize").setValue(o.result.file.size);
                Ext.getCmp("txtVoiceFormat").setValue(o.result.file.extension.substring(1));
				Ext.Msg.show({
					title: '提示',
					msg: "上传成功！",
					buttons: Ext.Msg.OK,
					icon: Ext.Msg.INFO
				});
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

fnOpenUpload = function() {
    if (get("spanOpenUpload").style.display == "none")
        get("spanOpenUpload").style.display = "";
    else
        get("spanOpenUpload").style.display = "none";
}