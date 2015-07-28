/*Jermyn 2013-04-01
POS小票
*/

var nextStatus;   //下级状态(jifeng.cao)
var order_id;     //收款订单(jifeng.cao)

var statusType = "";

Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/Inout3Handler.ashx?mid=" + __mid);
    debugger;
    //查询
    fnSearch();
    //  Ext.getCmp("txtSalesUnitId").addCls("txtDisable2");
 //   $("#txtSalesUnitId").addClass("txtDisable2");
    //如果有url参数有查询订单状态，就选中。 add by donal 2014-10-11 11:10:32
    var statusType = getUrlParam("statusType");
    if (statusType != "") {
        fnGridSearch(statusType);
    }

});

//创建订单，暂时未用
function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PosOrderEdit",
        title: "订单详细信息",
        url: "PosOrderEdit3.aspx?mid=" + __mid + "&op=new"
    });
    win.show();
}

//查询方法
function fnSearch() {
    var type = get("hType").value; //记录当前的栏目
    fnGridSearch(type);
}

//订单查询方法
function fnGridSearch(value) {


    //只有查询未选择门店时显示按钮
    if (value == "1234567890") {
        $("#btn_SetUnit").show();
    }
    else {
        $("#btn_SetUnit").hide();
    }


    get("hType").value = value;
    $('div[id^=tab]').each(function () {
        this.style.background = "#E6E4E1";
    });
    //    for (var i = 0; i < 7; i++) {
    //        get('tab' + i).style.background = "#E6E4E1";
    //    }
    get('tab' + value).style.background = "#F3F3F6";
    var storeId = "salesOutOrderStore1";  // grid的store
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=PosOrder_lj";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())//传递参数
        , sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        , Field7: value
    };

    Ext.getCmp("pageBar0").moveFirst();

    Ext.getStore(storeId).load({  //store装载
        params: {},
        callback: function (d) {
            fnLoadTotalCount();
        }
    });

}

//保存数据后刷新列表当前页
function fnGridReload() {

    var page = Ext.getCmp("pageBar0");
    var fromRecord = page.getPageData().fromRecord;
    var toRecord = page.getPageData().toRecord;

    //计算当前页记录条数
    var currentRecordCount = toRecord - fromRecord + 1;

    if (currentRecordCount == 1 && (page.store.currentPage - 1 > 0)) {
        //判断当前页只有一条记录且当前页不是第一页
        page.store.loadPage(page.store.currentPage - 1);
    } else {
        page.store.loadPage(page.store.currentPage);
    }
    //重新加载各状态订单数量
    fnLoadTotalCount();
}


//导出订单
function fnSearchExcel() {
    var type = get("hType").value;

    //确定是否导出当前数据
    Ext.MessageBox.confirm('提示信息', '确定导出当前数据?', function ex(btn) {
        if (btn == 'yes') {
            var salesUnitId = Ext.getCmp("txtSalesUnit").jitGetValue();
            //导出当前数据
            window.open(JITPage.HandlerUrl.getValue() + "&method=Export_lj&Field7=" + type + "&sales_unit_id=" + salesUnitId + "&param=" + Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
        }
    });

}

//设置送货门店
function fnSetUnit() {

    var records = Ext.getCmp("gridView").getSelectionModel().getSelection();

    if (records.length == 0) {
        Ext.Msg.show({ title: "提示", msg: "请选择要设置的订单!", buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
        return;
    }

    var orderList = "";

    for (var i = 0; i < records.length; i++) {
        if (i!=0) {
            orderList += ",";
        }
        orderList += "'"+ records[i].data.order_no+"'";
    }

    var win = new Ext.Window({
        width: 600,
        height: 400,
        title: "选择门店",
        id: "UnitWin",
        items: [{
            xtype: "panel",
            id:"UnitPanel",
            layout: "anchor",
            width: 600,
            height: 400,
            bodyStyle: 'padding:30px 20px 5px 5px',
            items: [{
                id: "jitbizunit",
                xtype: "jitbizunitselecttree",
                fieldLabel: "门店"
            }]
        }],
        buttons: [{
            text: "保存",
            handler: function () {
                var unitID = Ext.getCmp("jitbizunit").jitGetValue();
                if (unitID=="") {
                    Ext.Msg.show({ title: "提示", msg: "请选择门店!", buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                    return;
                }

                Ext.Ajax.request({
                    method: 'GET',
                    sync: true,
                    url: 'Handler/Inout3Handler.ashx?method=SetUnit',
                    params: {
                        unitID: unitID
                        , orderList: orderList
                    },
                    success: function (result, request) {
                        var d = Ext.decode(result.responseText);
                        if (!d.success) {
                            Ext.Msg.show({ title: "提示", msg: "保存失败!", buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                        } else {
                            Ext.getCmp("UnitPanel").hide();
                            Ext.Msg.show({ title: "提示", msg: "保存成功!", buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });

                            window.location.reload();
                        }
                    },
                    failure: function (result) {
                        Ext.Msg.show({ title: "提示", msg: "保存失败!", buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                    }
                });
            }
        },{
            text: "取消",
            handler: function () {
                win.hide();
            }
        }]
    });
    win.show();    
}


//付款状态
function fnPayStatus(val, p, r) {
    if (val == "1") {
        return "已付款";
    } else {
        return "未付款";
    }
}

    //查看订单详细数据
    function fnView(id, op, st, pay) {
        if (id == undefined || id == null) id = "";
        var win = Ext.create('jit.biz.Window', {
            jitSize: "large",
            height: 500,
            id: "PosOrderEdit",
            title: "订单详细信息",
            url: "PosOrderEdit3.aspx?order_id=" + id + "&op=" + op + "&st=" + st + "&pay=" + pay
        });
        win.show();

    }

    //查询订单状态下的数据量
    function fnLoadTotalCount(d) {
        Ext.Ajax.request({
            method: 'GET',
            sync: true,
            url: 'Handler/Inout3Handler.ashx?method=GetPosOrderTotalCount_lj',
            params: {
                form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
                , sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
                , Field7: '100'
            },
            success: function (result, request) {
                var d = Ext.decode(result.responseText);
                if (!d.success) {
                    //alert("操作失败：" + d.msg);
                } else {

                    get("tablist").innerHTML = d.data.StatusManagerListHTML;
                    get("txtNum0").innerHTML = d.data.StatusTotalCount;

                    var type = get("hType").value; //记录当前的栏目
                    get('tab' + type).style.background = "#F3F3F6";
                }
            },
            failure: function (result) {
                alert("操作失败：" + result.responseText);
            }
        });
    }

    //高级查询功能
    function fnMoreSearchView(type) {
        if (!Ext.getCmp("searchPanel").isExpand) {
            document.getElementById("view_Search").style.height = "110px";
            Ext.getCmp("searchPanel").isExpand = true;

            Ext.getCmp("txtOrderDate").hidden = false;
            Ext.getCmp("txtOrderDate").setVisible(true);

            Ext.getCmp("txtField9").hidden = false;
            Ext.getCmp("txtField9").setVisible(true);

            Ext.getCmp("txtDataFromID").hidden = false;
            Ext.getCmp("txtDataFromID").setVisible(true);

            Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
            Ext.getCmp("searchPanel").doLayout();
        } else {
            document.getElementById("view_Search").style.height = "44px";
            Ext.getCmp("searchPanel").isExpand = false;

            Ext.getCmp("txtOrderDate").hidden = true;
            Ext.getCmp("txtOrderDate").setVisible(false);

            Ext.getCmp("txtField9").hidden = true;
            Ext.getCmp("txtField9").setVisible(false);

            Ext.getCmp("txtDataFromID").hidden = true;
            Ext.getCmp("txtDataFromID").setVisible(false);

            Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
            Ext.getCmp("searchPanel").doLayout();
        }
    }

    //查看订单详细数据
    function fnHSView(id, op, st, pay) {
        if (id == undefined || id == null) id = "";
        var win = Ext.create('jit.biz.Window', {
            jitSize: "large",
            height: 550,
            id: "Asus_OrdersDetail",
            title: "订单详细信息",
            url: "/Project/Asus/Orders/OrdersEdit.aspx?order_id=" + id + "&op=" + op + "&st=" + st + "&pay=" + pay
        });
        win.show();
    }
    //订单操作---收款
    function fnOperationSubmit() {
        var form = Ext.getCmp('operationPanel').getForm();//收款信息,获取整个form的信息
        if (!form.isValid()) {
     
            return false;
        }
        //alert("下一状态"+nextStatus);
        form.submit({//表单提交
            url: JITPage.HandlerUrl.getValue() + "&method=UpdateStatus",
            waitTitle: JITPage.Msg.SubmitDataTitle,  //等待时的标题
            waitMsg: JITPage.Msg.SubmitData,
            params: {
                order_id: order_id, 
                nextStatus: nextStatus,      //下一个状态
                PicUrl: Ext.getCmp("PicUrl").getValue()
            },
            success: function (fp, o) {
                if (o.result.success) {
                    Ext.Msg.show({
                        title: '提示',
                        msg: o.result.msg,
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.INFO,
                        fn: function () {

                            fnGridReload();
                            Ext.getCmp("operationWin").hide();
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
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        });
    }

    function fnOperationCancel() {
        Ext.getCmp("operationWin").hide();
    }

    function fnbtn(status, orderID) {

        //下级状态值
        nextStatus = status;
        //收款订单
        order_id = orderID;

        Ext.getCmp("operationPanel").getForm().reset();
        Ext.getCmp("operationWin").show();//收款弹出框
    }