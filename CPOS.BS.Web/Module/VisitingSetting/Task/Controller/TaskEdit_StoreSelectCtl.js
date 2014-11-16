var id = JITMethod.getUrlParam("id");
var btncode = JITMethod.getUrlParam("btncode");
var urlparams = window.location.search;

var pageLanguage = new Object();

var pnlSearch; //查询pannel
var pnlWork; //操作pannel

var btnAdd; //增加
var gridStoreList; //终端数据表

Ext.onReady(function () {
    JITPage.HandlerUrl.setValue("/Module/VisitingSetting/Task/Handler/TaskHandler_Store.ashx?mid=" + __mid);

    InitView();

    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    if (id != null) {
        Ext.Ajax.request({
            url: "Handler/TaskHandler.ashx?mid=" + __mid
                        + "&btncode=" + btncode + "&method=GetTaskPOP_SearchConditions",
            params: {
                id: id
            },
            method: 'post',
            success: function (response) {
                if (response.responseText != "") {
                    var json = Ext.JSON.decode(response.responseText);
                    
                    if (json.IsAutoFill != "") {
                        if (json.IsAutoFill == "1") {
                            Ext.getCmp("ckAutoFill").setValue("true");

                            if (json.GroupCondition != "") {
                                pnlSearch.jitSetValue(Ext.JSON.decode(json.GroupCondition))
                                pnlSearch.btnMoreSearch.hide();

                                gridStoreList.searchConditions = json.GroupCondition;
                                gridStoreList.pagebar.moveFirst();
                            }
                        }
                        else {
                            Ext.getCmp("ckAutoFill").setValue("false");
                        }
                    }
                }
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取数据失败");
            }
        });
    }

    //by zhongbao.xiao 2013.5.24 用户返回拜访任务列表页
    $("#navi").click(function () {
        window.location = "task.aspx?mid=" + __mid;
    });
});


function fnSubmit() {
    var myMask_info = JITPage.Msg.SubmitData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    var v = gridStoreList.selModel.jitGetValue(); //获取选中行

    var l = new Array();//获取查询条件
    for (var i = 0; i < pnlSearch.showPannel.items.items.length; i++) {
        var c = pnlSearch.showPannel.items.items[i];
        if (c.jitGetValue() != null && c.jitGetValue() != "" && c.jitGetValue() != "0") {
            var o = new Object();
            o.ControlType = c.ControlType;
            o.ControlValue = c.jitGetValue();
            o.ControlName = c.ControlName;
            o.CorrelationValue = c.CorrelationValue;
            l.push(o);
        }
    }

    var btn = this;
    btn.setDisabled(true);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue()
                        + "&btncode=" + btncode + "&method=EditTaskPOP_Store",
        params: {
            id: id,
            sCondition: Ext.JSON.encode(l),
            isAutoFill: Ext.getCmp("ckAutoFill").getValue(),
            allSelectorStatus: v.allSelectorStatus,
            defaultList: v.defaultList.toString(),
            includeList: v.includeList.toString(),
            excludeList: v.excludeList.toString()
        },
        method: 'post',
        success: function (response) {
            window.location = 'TaskEdit_Step.aspx' + urlparams;
            //Ext.Msg.alert("提示", "操作成功");
            btn.setDisabled(false);

            //gridStoreList.pagebar.moveFirst();
            //fnSearch(id);
            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            btn.setDisabled(false);
            myMask.hide();
        }
    });
}

function fnAutoFillSet() {
    document.getElementById("tab1").setAttribute("src", "TaskEdit_StoreAutoFillSet.aspx" + window.location.search);
    Ext.getCmp("storeAutoFillSetWin").show();
}

function fnAotoFillSetChange() {
    if (this.getValue() == true) {
        gridStoreList.columns[0].hide();

        gridStoreList.selModel.jitClearValue();
        gridStoreList.selModel.allSelectorStatus = 2;
        gridStoreList.selModel.updateHeaderState();
        gridStoreList.selModel.deselectAll();
    }
    else {
        gridStoreList.columns[0].show();

        gridStoreList.selModel.jitClearValue();
        gridStoreList.selModel.allSelectorStatus = 2;
        gridStoreList.selModel.updateHeaderState();
        gridStoreList.selModel.deselectAll();
        
    }

    //gridStoreList.pagebar.moveFirst();
}