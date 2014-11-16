var btncode = JITMethod.getUrlParam("btncode");
var id = JITMethod.getUrlParam("id");

Ext.onReady(function () {

    //加载需要的文件
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    InitVE();
    InitStore();
    InitView();
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/StepHandler.ashx?mid=" + __mid);
    Ext.getCmp("OnePage").jitSetValue(0);
    Ext.getCmp("MustDo").jitSetValue(0);
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    if (id != null && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetStepByID",
            params: { id: id },
            method: 'post',
            success: function (response) {
                //try {
                //alert(response.responseText);
                var jdata = eval(response.responseText);
                //加载form
                Ext.getStore("stepStore").add(jdata[0]);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("stepStore").first());
                Ext.getCmp("StepType").jitSetValue(jdata[0].StepType);
                Ext.getCmp("ObjectGroup").jitSetValue(jdata[0].ObjectGroup);
                myMask.hide();

                //tab设置
                var focus = null;
                if (JITMethod.getUrlParam("focus") != null && JITMethod.getUrlParam("focus") != "") {
                    focus = parseInt(JITMethod.getUrlParam("focus"));
                }
                fnLoadTab({
                    id: jdata[0].VisitingTaskStepID,
                    StepType: jdata[0].StepType,
                    focus: focus
                });

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

function fnSubmit() {
    var form = this.up('form').getForm();
    if (!form.isValid()) {
        return false;
    }

    if (Ext.getCmp("StepType").getValue().toString() == "7") {
        if (Ext.getCmp("ObjectGroup").getValue() == undefined || Ext.getCmp("ObjectGroup").getValue() == "") {
            Ext.Msg.alert("提示", "请选择对象分组");
            return false;
        }
    }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditStep",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            VisitingTaskID: JITMethod.getUrlParam("tid")
        },
        success: function (fp, o) {
            if (o.result.success) {
                //                Ext.Msg.show({
                //                    title: '提示',
                //                    msg: o.result.msg,
                //                    buttons: Ext.Msg.OK,
                //                    icon: Ext.Msg.INFO
                //                });

                var focus = 1;
                if (Ext.getCmp("StepType").getValue() == 5) {
                    focus = 2;
                }
                else if (Ext.getCmp("StepType").getValue() == 6 || Ext.getCmp("StepType").getValue() == 7) {
                    focus = 0;
                }
                parent.document.getElementById("tab1").setAttribute("src", "../Step/StepEdit.aspx" + "?mid=" + __mid + "&tid=" + JITMethod.getUrlParam("tid") + "&id=" + o.result.id + "&focus=" + focus + "&btncode=" + btncode);

                parent.Ext.getStore("stepStore").load();
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
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

function fnStepChange() {
    //alert(this.getValue());
    if (parseInt(this.getValue()) == 7) {
        Ext.getCmp("ObjectGroup").show();
    }
    else {
        Ext.getCmp("ObjectGroup").hide();
    }
}

/*
@id 
@StepType 
@focus
@ispageload
*/
function fnLoadTab(obj) {
    /*
    OptionValue	OptionText
    1	产品相关
    2	品牌相关
    3	品类相关
    4	人员考评
    5	自定义
    6	订单相关
    7   自定义/会员采集
    */
    parent.tab2State = false;
    parent.tab3State = false;
    var parameters = "?mid=" + __mid + "&tid=" + JITMethod.getUrlParam("tid") + "&id=" + obj.id + "&btncode=" + btncode + "&r=" + Math.random();
    parent.document.getElementById("tab3").setAttribute("src", "../Step/StepEdit_ParameterSelect.aspx" + parameters);
    switch (parseInt(obj.StepType)) {
        case 1:
            parent.document.getElementById("tab2").setAttribute("src", "../Step/StepEdit_SKUSelect.aspx" + parameters);

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(false);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(false);
            break;
        case 2:
            parent.document.getElementById("tab2").setAttribute("src", "../Step/StepEdit_BrandSelect.aspx" + parameters);

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(false);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(false);
            break;
        case 3:
            parent.document.getElementById("tab2").setAttribute("src", "../Step/StepEdit_CatagorySelect.aspx" + parameters);

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(false);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(false);
            break;
        case 4:
            parent.document.getElementById("tab2").setAttribute("src", "../Step/StepEdit_PositionSelect.aspx" + parameters);

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(false);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(false);
            break;
        case 5:

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(true);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(false);
            break;
        case 6:

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(true);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(true);
            break;

        case 7:
            //自定义对象

            parent.Ext.getCmp("tabs").items.items[1].setDisabled(true);
            parent.Ext.getCmp("tabs").items.items[2].setDisabled(true);
            break;
      
    }

    //编辑完成后
    if (obj.focus != null && obj.focus != 0) {
        parent.Ext.getCmp("tabs").setActiveTab(obj.focus);
        return;
    }
}
