var id, btncode;
var daysData=null;//用户周期详细信息历史记录

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/CycleHandler.ashx?mid=" + __mid);

    fnSearch();

    Ext.getCmp("CycleDay").addListener({
        'change': function () {
            if (parseInt(Ext.getCmp("CycleType").getValue()) == 3) {
                fnRenderDays(3, this.getValue());
            }
        }
    });
});

function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnCreate() {
    id = "";
    btncode = "create";
    daysData = null;
    
    Ext.getCmp("columnCycleDetail").removeAll();
    Ext.getCmp("cycleEditPanel").getForm().reset();

    Ext.getCmp("cycleEditWin").show();

    Ext.getCmp("CycleType").jitSetValue(3);
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
}

function fnDelete() {
    JITPage.delList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "CycleID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "CycleID" })
        },
        handler: function () {
            Ext.getStore("listStore").load();
        }
    });
}

function fnUpdate(pID,pBtnCode) {
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    id = pID;
    btncode = pBtnCode;
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetByID",
        params: { id: id },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);

            //days赋值
            daysData = jdata.detailEntity;

            //加载form
            Ext.getStore("editStore").removeAll();
            Ext.getStore("editStore").add(jdata);
            Ext.getCmp("cycleEditPanel").getForm().loadRecord(Ext.getStore("editStore").first());
            Ext.getCmp("StartDate").setValue(JITMethod.getDate(jdata.StartDate));
            Ext.getCmp("EndDate").setValue(JITMethod.getDate(jdata.EndDate));

            //fnRenderDays(jdata.CycleType, null);
            Ext.getCmp("CycleDay").setValue(jdata.CycleDay);

            Ext.getCmp("cycleEditWin").show();

            myMask.hide();
        },
        failure: function (response) {
            Ext.Msg.alert("提示", "获取数据失败");
            myMask.hide();
        }
    });
}

function fnColumnUpdate(value, p, r) {

    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.CycleID + "','update')\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.CycleID + "','search')\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
}
function fnColumnDelete(value, p, r) {
    if (r.index > 1) {
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
    }
    else {
        return "";
    }
}

function fnRenderType(value, p, r) {
    return (value == 1 ? "周" : value == 2 ? "月" : "自定义");
}

/*
动态显示天数
@type 周期类型
@dayofcycle 只有周期类型为自定义时使用
*/
function fnRenderDays(type, dayofcycle) {
    Ext.getCmp("columnCycleDetail").removeAll();

    var days = [];
    switch (parseInt(type)) {
        case 1:
            Ext.getCmp("CycleDay").setValue(7);
            Ext.getCmp("CycleDay").setDisabled(true);

            for (var i = 1; i <= 7; i++) {
                days.push({
                    xtype: "jitcheckbox",
                    boxLabel: fnGetWeekDayByNum(i),
                    name: "cycleDetail",
                    inputValue: fnGetDaysValue(i, daysData),
                    checked: !(fnGetDaysValue(i, daysData) == i),
                    width: 80
                });
            };
            
            break;
        case 2:
            Ext.getCmp("CycleDay").setValue(31);
            Ext.getCmp("CycleDay").setDisabled(true);

            for (var i = 1; i <= 31; i++) {
                days.push({
                    xtype: "jitcheckbox",
                    boxLabel: i + "日",
                    name: "cycleDetail",
                    inputValue: fnGetDaysValue(i, daysData),
                    checked: !(fnGetDaysValue(i, daysData) == i),
                    width: 80
                });
            };
            break;
        case 3:
            Ext.getCmp("CycleDay").setDisabled(false);

            for (var i = 1; i <= dayofcycle; i++) {
                days.push({
                    xtype: "jitcheckbox",
                    boxLabel: "第" + i + "天",
                    name: "cycleDetail",
                    inputValue: fnGetDaysValue(i, daysData),
                    checked: !(fnGetDaysValue(i, daysData) == i),
                    width: 80
                });
            };
            break;
    }
    Ext.getCmp("columnCycleDetail").add(days);
}

function fnGetDaysValue(i, data) {
    var res = i;
    if (data != null) {
        for (var j = 0; j < data.length; j++) {
            if (data[j].DayOfCycle == i) {
                res = data[j].CycleDetailID;
            }
        }
    }
    return res;
}

function fnCycleTypeChange() {
    switch (parseInt(this.getValue())) {
        case 3:
            Ext.getCmp("CycleDay").setValue(10);

            break;
        default:
            fnRenderDays(this.getValue(), null);
            break;
    }
    
}

function fnGetWeekDayByNum(num) {
    var res = "";
    switch (parseInt(num)) {
        case 1:
            res = "周一";
            break;
        case 2:
            res = "周二";
            break;
        case 3:
            res = "周三";
            break;
        case 4:
            res = "周四";
            break;
        case 5:
            res = "周五";
            break;
        case 6:
            res = "周六";
            break;
        case 7:
            res = "周日";
            break;
    }
    return res;
}

function fnSubmit() {
    var form = Ext.getCmp("cycleEditPanel").getForm();
    if (!form.isValid()) {
        return false;
    }
    if (form.getValues()["cycleDetail"] == undefined) {
        Ext.Msg.alert("提示", "请勾选执行日期");
        return false;
    }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=Edit",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            CycleDay: Ext.getCmp("CycleDay").getValue(),
            CycleType:Ext.getCmp("CycleType").jitGetValue()
        },
        success: function (fp, o) {

            Ext.getCmp("cycleEditWin").hide();
            Ext.getCmp("pageBar").moveFirst();
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
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
                msg: "编辑失败",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}

function fnCancel() {
    Ext.getCmp("cycleEditWin").hide();
}
