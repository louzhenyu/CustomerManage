var id = JITMethod.getUrlParam("id");
var btncode = JITMethod.getUrlParam("btncode");

var edit_cycledetailid = null;
var editload = false;

Ext.onReady(function () {
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //设置初始值
    Ext.getCmp("eidt_State").setValue(1);
    Ext.getStore("cycleDetailStore").addListener({
        'load': function () {
            if (Ext.getStore("cycleDetailStore").first() != undefined) {

                if (editload == false && edit_cycledetailid != null) {
                    //编辑时，第一次加载，加载数据库值
                    Ext.getCmp("eidt_CycleDetail").setValue(edit_cycledetailid.split(','));
                    editload = true;
                }
                else {
                    //加载完成后，或者添加时 设置默认值为第一项
                    Ext.getCmp("eidt_CycleDetail").setValue(Ext.getStore("cycleDetailStore").first().data.CycleDetailID);
                }
            }
        }
    });

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/RouteHandler.ashx?mid=" + __mid);

    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    //操作权限
    id = (id == null ? "" : id);

    Ext.getStore("cycleStore").proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetCycleList";
    Ext.getStore("cycleStore").load();

    if (id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetRouteByID",
            params: { id: id },
            method: 'post',
            success: function (response) {
                var jdata = eval(response.responseText);

                //加载form
                Ext.getStore("routeStore").add(jdata[0]);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("routeStore").first());

                Ext.getCmp("eidt_CycleDetail").setValue("");
                edit_cycledetailid = jdata[0].CycleDetailID;

                Ext.getCmp("ClientUserID").jitSetValue(jdata[0].ClientUserID);
                Ext.getCmp("TripMode").jitSetValue(jdata[0].TripMode)
                Ext.getCmp("StartDate").setValue(JITMethod.getDate(jdata[0].StartDate));
                Ext.getCmp("EndDate").setValue(JITMethod.getDate(jdata[0].EndDate));
                Ext.getCmp("POPType").jitSetValue(jdata[0].POPType);

                fnLoadTab({
                    id: jdata[0].RouteID,
                    POPType: jdata[0].POPType,
                    focus: JITMethod.getUrlParam("focus")
                });
                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取数据失败");
            }
        });
    }
    else {
        myMask.hide();
    }
});

function fnCycleChange() {
    Ext.getStore("cycleDetailStore").removeAll();
    Ext.getStore("cycleDetailStore").proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetCycleDetailByCID&cycleid=" + this.getValue();
    Ext.getStore("cycleDetailStore").load();
}
function fnSubmit() {
    var form = this.up('form').getForm();
    if (!form.isValid()) {
        return false;
    }

    if (Ext.getCmp("ClientUserID").jitGetValue() == undefined || Ext.getCmp("ClientUserID").jitGetValue() == "") {
        Ext.Msg.show({
            title: '提示',
            msg: '请选择人员',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.WARNING
        });
        return false;
    }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditRoute",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            ClientUserID: Ext.getCmp("ClientUserID").getValue(),
            TripMode: Ext.getCmp("TripMode").jitGetValue()
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        parent.tab2State = false;
                        parent.tab3State = false;
                        parent.document.getElementById("tab1").setAttribute("src", "RouteEdit.aspx" + "?mid=" + __mid + "&id=" + o.result.id + "&btncode=" + btncode);
                    }
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
    top.window.location = "Route.aspx?mid=" + __mid;
}

/*
@id 
@POPType
*/
function fnLoadTab(obj) {
    parent.tab2State = false;
    parent.tab3State = false;
    parent.Ext.getCmp("tabs").items.items[1].setDisabled(false);
    parent.Ext.getCmp("tabs").items.items[2].setDisabled(false);
    var parameters = "?mid=" + __mid + "&id=" + obj.id + "&btncode=" + btncode + "&r=" + Math.random();
    switch (parseInt(obj.POPType)) {
        case 1:
            //门店
            parent.document.getElementById("tab2").setAttribute("src", "RoutePOPMap_Store.aspx" + parameters);
            parent.document.getElementById("tab3").setAttribute("src", "RoutePOPList_Store.aspx" + parameters);
            break;
        case 2:
            //经销商
            parent.document.getElementById("tab2").setAttribute("src", "RoutePOPMap_Distributor.aspx" + parameters);
            parent.document.getElementById("tab3").setAttribute("src", "RoutePOPList_Distributor.aspx" + parameters);
            break;
    }
    if (obj.focus != null && obj.focus != 0) {
        parent.Ext.getCmp("tabs").setActiveTab(obj.focus);
        return;
    }
}