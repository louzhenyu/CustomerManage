/*Jermyn 2013-04-01
POS小票
*/

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

    //    //页面加载
    JITPage.HandlerUrl.setValue("Handler/InoutHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PosOrderEdit",
        title: "订单详细信息",
        url: "PosOrderEdit2.aspx?mid=" + __mid + "&op=new"
    });
    win.show();

}

function fnSearch() {
    //var type = "";
    //if (typeof(t) == "string") {
    //    type = t;
    //}
    var type = get("hType").value;
    switch (type) {
        case '1':
            fnSearch1();
            break;
        case '2':
            fnSearch2();
            break;
        case '3':
            fnSearch3();
            break;
        case '4':
            fnSearch4();
            break;
    }
}

function fnSearchExcel() {
    var type = get("hType").value;
    switch (type) {
        case '1':
            fnSearchExcel1();
            break;
        case '2':
            fnSearchExcel2();
            break;
    }
}



function fnSearchExcel1() {
    get("hType").value = "1";
    get('DivGridView1').style.display = "";
    get('DivGridView2').style.display = "none";
    get('DivGridView3').style.display = "none";
    get('DivGridView4').style.display = "none";
    Ext.getCmp("btnOp1").setVisible(true);
    Ext.getCmp("btnOp2").setVisible(false);
    Ext.getCmp("btnOp3").setVisible(false);
    get('tab1').style.background = "#F3F3F6";
    get('tab2').style.background = "#E6E4E1";
    get('tab3').style.background = "#E6E4E1";
    get('tab4').style.background = "#E6E4E1";

    var storeId = "salesOutOrderStore1";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=Export";
   // alert(Ext.getStore(storeId).proxy.url)
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        , sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        , Field7: '1'
    };
//    Ext.getStore(storeId).load({
//        params: {},
//        callback: function (d) {
//            //fnLoadTotalCount();
//        }
//    });

    //确定是否导出当前数据
    Ext.MessageBox.confirm('提示信息', '确定导出当前数据?', function ex(btn) {
        if (btn == 'yes') {
            var salesUnitId = Ext.getCmp("txtSalesUnit").jitGetValue();
            //导出当前数据
            window.open(JITPage.HandlerUrl.getValue() + "&method=Export&Field7=1&sales_unit_id=" + salesUnitId + "&param=" + Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
        }
    });

}

function fnSearchExcel2() {
    get("hType").value = "2";
    get('DivGridView1').style.display = "none";
    get('DivGridView2').style.display = "";
    get('DivGridView3').style.display = "none";
    get('DivGridView4').style.display = "none";
    Ext.getCmp("btnOp1").setVisible(false);
    Ext.getCmp("btnOp2").setVisible(true);
    Ext.getCmp("btnOp3").setVisible(false);
    get('tab1').style.background = "#E6E4E1";
    get('tab2').style.background = "#F3F3F6";
    get('tab3').style.background = "#E6E4E1";
    get('tab4').style.background = "#E6E4E1";

    var storeId = "salesOutOrderStore2";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=Export";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        , sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        , Field7: '2'
    };
    Ext.MessageBox.confirm('提示信息', '确定导出当前数据?', function ex(btn) {
        if (btn == 'yes') {
            var salesUnitId = Ext.getCmp("txtSalesUnit").jitGetValue();
            //导出当前数据
            window.open(JITPage.HandlerUrl.getValue() + "&method=Export&Field7=2&sales_unit_id=" + salesUnitId + "&param=" + Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
        }
    });
}

function fnSearch1() {
    get("hType").value = "1";
    get('DivGridView1').style.display = "";
    get('DivGridView2').style.display = "none";
    get('DivGridView3').style.display = "none";
    get('DivGridView4').style.display = "none";
    Ext.getCmp("btnOp1").setVisible(true);
    Ext.getCmp("btnOp2").setVisible(false);
    Ext.getCmp("btnOp3").setVisible(false);
    get('tab1').style.background = "#F3F3F6";
    get('tab2').style.background = "#E6E4E1";
    get('tab3').style.background = "#E6E4E1";
    get('tab4').style.background = "#E6E4E1";

    var storeId = "salesOutOrderStore1";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=PosOrder";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        ,sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        ,Field7: '1'
    };
    Ext.getStore(storeId).load({
        params:{},
        callback: function(d) {
            fnLoadTotalCount();
        }
    });
}

function fnSearch2() {
    get("hType").value = "2";
    get('DivGridView1').style.display = "none";
    get('DivGridView2').style.display = "";
    get('DivGridView3').style.display = "none";
    get('DivGridView4').style.display = "none";
    Ext.getCmp("btnOp1").setVisible(false);
    Ext.getCmp("btnOp2").setVisible(true);
    Ext.getCmp("btnOp3").setVisible(false);
    get('tab1').style.background = "#E6E4E1";
    get('tab2').style.background = "#F3F3F6";
    get('tab3').style.background = "#E6E4E1";
    get('tab4').style.background = "#E6E4E1";

    var storeId = "salesOutOrderStore2";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=PosOrder";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        ,sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        ,Field7: '2'
    };
    Ext.getStore(storeId).load({
        params:{},
        callback: function(d) {
            fnLoadTotalCount();
        }
    });
}
function fnSearch3() {
    get("hType").value = "3";
    get('DivGridView1').style.display = "none";
    get('DivGridView2').style.display = "none";
    get('DivGridView3').style.display = "";
    get('DivGridView4').style.display = "none";
    Ext.getCmp("btnOp1").setVisible(false);
    Ext.getCmp("btnOp2").setVisible(false);
    Ext.getCmp("btnOp3").setVisible(true);
    get('tab1').style.background = "#E6E4E1";
    get('tab2').style.background = "#E6E4E1";
    get('tab3').style.background = "#F3F3F6";
    get('tab4').style.background = "#E6E4E1";

    var storeId = "salesOutOrderStore3";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=PosOrder";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        ,sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        ,Field7: '3'
    };
    Ext.getStore(storeId).load({
        params:{},
        callback: function(d) {
            fnLoadTotalCount();
        }
    });
}
function fnSearch4() {
    get("hType").value = "0";
    get('DivGridView1').style.display = "none";
    get('DivGridView2').style.display = "none";
    get('DivGridView3').style.display = "none";
    get('DivGridView4').style.display = "";
    Ext.getCmp("btnOp1").setVisible(false);
    Ext.getCmp("btnOp2").setVisible(false);
    Ext.getCmp("btnOp3").setVisible(false);
    get('tab1').style.background = "#E6E4E1";
    get('tab2').style.background = "#E6E4E1";
    get('tab3').style.background = "#E6E4E1";
    get('tab4').style.background = "#F3F3F6";

    var storeId = "salesOutOrderStore4";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=PosOrder";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        ,sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
        ,Field7: '0'
    };
    Ext.getStore(storeId).load({
        params:{},
        callback: function(d) {
            fnLoadTotalCount();
        }
    });
}

function fnView(id, op) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 500,
        id: "PosOrderEdit",
        title: "订单详细信息",
        url: "PosOrderEdit2.aspx?order_id=" + id + "&op=" + op
    });
    win.show();

}
function fnDelete(id) {
    //    debugger;
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "order_id" }),
        url: JITPage.HandlerUrl.getValue() + "&method=InoutOrderDelete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "order_id" })
        },
        handler: function () {
            Ext.getStore("salesOutOrderStore").load();
        }
    });
}
function fnPass(id) {
    if (!confirm("确认审核?")) return;
    if (id == undefined || id == null) id = "";
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        url: 'Handler/InoutHandler.ashx?method=InoutOrderPass&order_id=' + id,
        params: {},
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                alert("操作失败：" + d.msg);
            } else {
                alert("审核成功");
                fnSearch();
            }
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}

function fnPosOrderDeliveryUpdate(id, type, amount) {
    var text = "确认操作?";
    if (type == "2") {
        text = "该订单总计 " + amount + " 元，确认已经收讫吗？";
    }
    if (type == "3") {
        text = "确认直接给客户提货吗？";
    }

    if (!confirm(text)) return;
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        url: 'Handler/InoutHandler.ashx?method=PosOrderDeliveryUpdate&Field7=' + type,
        params: { ids: id },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                alert("操作失败：" + d.msg);
            } else {
                alert("操作成功");
                fnSearch(type);
            }
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}

function fnLoadTotalCount(d) {
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        url: 'Handler/InoutHandler.ashx?method=GetPosOrderTotalCount',
        params: { 
            form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
            ,sales_unit_id: Ext.getCmp("txtSalesUnit").jitGetValue()
            ,Field7: '1'
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                //alert("操作失败：" + d.msg);
            } else {
                get("txtNum1").innerHTML = d.data.num1;
                get("txtNum2").innerHTML = d.data.num2;
                get("txtNum3").innerHTML = d.data.num3;
                get("txtNum4").innerHTML = d.data.num4;
            }
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}


function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}

function fnColumnMustDo(value, p, r) {
    return (value == 1 ? "√" : "")
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "140px";
        Ext.getCmp("searchPanel").isExpand = true;

        Ext.getCmp("txtOrderDate").hidden = false;
        Ext.getCmp("txtOrderDate").setVisible(true);
        
        Ext.getCmp("txtField9").hidden = false;
        Ext.getCmp("txtField9").setVisible(true);

        Ext.getCmp("txtModifyTime").hidden = false;
        Ext.getCmp("txtModifyTime").setVisible(true);

//        Ext.getCmp("txtPosPayType").hidden = false;
//        Ext.getCmp("txtPosPayType").setVisible(true);

        Ext.getCmp("txtPosSendType").hidden = false;
        Ext.getCmp("txtPosSendType").setVisible(true);

        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;

        Ext.getCmp("txtOrderDate").hidden = true;
        Ext.getCmp("txtOrderDate").setVisible(false);
        
        Ext.getCmp("txtField9").hidden = true;
        Ext.getCmp("txtField9").setVisible(false);
        
        Ext.getCmp("txtModifyTime").hidden = true;
        Ext.getCmp("txtModifyTime").setVisible(false);

//        Ext.getCmp("txtPosPayType").hidden = true;
//        Ext.getCmp("txtPosPayType").setVisible(false);
        
        Ext.getCmp("txtPosSendType").hidden = true;
        Ext.getCmp("txtPosSendType").setVisible(false);

        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}

fnPosOrderDeliveryUpdateByBatch = function(type) {
    var ids = "";
    switch (type) {
        case '0': 
            var list = Ext.getCmp('gridView1').getSelectionModel().getSelection();
            for (var i = 0; i < list.length; i++) {
                ids += list[i].data.order_id + ",";
            }
            break;
        case '3': 
            var list = Ext.getCmp('gridView2').getSelectionModel().getSelection();
            for (var i = 0; i < list.length; i++) {
                ids += list[i].data.order_id + ",";
            }
            break;
        case '4': 
            var list = Ext.getCmp('gridView3').getSelectionModel().getSelection();
            for (var i = 0; i < list.length; i++) {
                ids += list[i].data.order_id + ",";
            }
            break;
    }
    
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        url: 'Handler/InoutHandler.ashx?method=PosOrderDeliveryUpdate&Field7=' + type,
        params: { ids: ids },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                alert("操作失败：" + d.msg);
            } else {
                alert("操作成功");
                fnSearch(type);
            }
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}

function fnPosOrderDeliveryView(id, op) {
    if (id == undefined || id == null) return;
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 500,
        id: "PosOrderDelivery",
        title: "配送单",
        url: "PosOrderDelivery.aspx?order_id=" + id
    });
    win.show();
}


function fnViewUnit(id, op) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 500,
        id: "PosOrder2Unit",
        title: "配送订单",
        url: "PosOrder2Unit.aspx?order_id=" + id + "&op=" + op
    });
    win.show();

}
