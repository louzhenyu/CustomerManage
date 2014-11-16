var id = JITMethod.getUrlParam("id");
var btncode = JITMethod.getUrlParam("btncode");
var urlparams = "";

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    InitVE();
    InitStore();
    InitView();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/TaskHandler.ashx?mid=" + __mid);
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    if (id != null && id != "") {

        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetTaskByID",
            params: { id: id },
            method: 'post',
            success: function (response) {

                var jdata = eval(response.responseText);

                //加载form
                Ext.getStore("taskStore").add(jdata[0]);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("taskStore").first());
                Ext.getCmp("txtStartDate").setValue(JITMethod.getDate(jdata[0].StartDate));
                Ext.getCmp("txtEndDate").setValue(JITMethod.getDate(jdata[0].EndDate));
                Ext.getCmp("ClientPositionID").jitSetValue(jdata[0].ClientPositionID);

                Ext.getCmp("POPType").jitSetValue(jdata[0].POPType);
                Ext.getCmp("StartGPSType").jitSetValue(jdata[0].StartGPSType);
                Ext.getCmp("EndGPSType").jitSetValue(jdata[0].EndGPSType);
                myMask.hide();

                urlparams = "?mid=" + __mid + "&id=" + id + "&btncode=" + btncode + "&poptype=" + jdata[0].POPType.toString();
                var popurl = "";
                switch (jdata[0].POPType.toString()) {
                    case "1":
                        popurl = 'TaskEdit_StoreSelect.aspx';
                        break;
                    case "2":
                        popurl = 'TaskEdit_DistributorSelect.aspx';
                        break;
                }
                document.getElementById("a1").setAttribute("href", popurl + urlparams);
                document.getElementById("a2").setAttribute("href", 'TaskEdit_Step.aspx' + urlparams);

            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        myMask.hide();

        document.getElementById("a1").setAttribute("onclick", "javascript:Ext.Msg.show({title: '提示',msg:'请先添加基本信息',buttons: Ext.Msg.OK,icon: Ext.Msg.WARNING});");
        document.getElementById("a2").setAttribute("onclick", "javascript:Ext.Msg.show({title: '提示',msg:'请先添加基本信息',buttons: Ext.Msg.OK,icon: Ext.Msg.WARNING});");
    }

    //by zhongbao.xiao 2013.5.24 用户返回拜访任务列表页
    $("#navi").click(function () {
        window.location = "task.aspx?mid=" + __mid;
    });
});

function fnSubmit() {
    var form = this.up('form').getForm();
    if (!form.isValid()) {
        return false;
    }
    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditTask",
        waitTitle:JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue(),
            StartGPSType: Ext.getCmp("StartGPSType").jitGetValue(),
            EndGPSType: Ext.getCmp("EndGPSType").jitGetValue()
        },
        success: function (fp, o) {
            if (o.result.success) {
//                Ext.Msg.show({
//                    title: '提示',
//                    msg: o.result.msg,
//                    buttons: Ext.Msg.OK,
//                    icon: Ext.Msg.INFO
//                });
                //window.location = "user.aspx";

                //1门店 2经销商
                urlparams = "?mid=" + __mid + "&id=" + o.result.id + "&btncode=" + btncode + "&poptype=" + Ext.getCmp("POPType").getValue().toString();
                switch (Ext.getCmp("POPType").getValue().toString()) {
                    case "1":
                        window.location = "TaskEdit_StoreSelect.aspx" + urlparams;
                        break;
                    case "2":
                        window.location = "TaskEdit_DistributorSelect.aspx" + urlparams;
                        break;
                }
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: "提交失败",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

function fnCancel() {
    window.location = "task.aspx?mid=" + __mid;
}