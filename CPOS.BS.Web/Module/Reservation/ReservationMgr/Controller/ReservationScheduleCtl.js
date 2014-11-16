Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //������Ҫ���ļ�
    InitVE();
    InitStore();
    InitView();

    //ҳ�����
    JITPage.HandlerUrl.setValue("Handler/ReservationHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnSearch() {
    Ext.getStore("reservationScheduleListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=reservation_return_schedule_list";
    Ext.getStore("reservationScheduleListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("reservationScheduleListStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("reservationScheduleListStore").load();
}


function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "SalesReturnOrderEdit",
        title: "����ԤԼ",
        url: "ReservationScheduleEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}
function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "SalesReturnOrderEdit",
        title: "�����˻�",
        url: "SalesReturnOrderEdit.aspx?order_id=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "order_id"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=order_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "order_id"
            })
        },
        handler: function () {
            Ext.getStore("salesReturnOrderStore").load();
        }
    });
}

function fnPass(id) {
    if (!confirm("ȷ�����?")) return;
    if (id == undefined || id == null) id = "";
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        url: 'Handler/OrderHandler.ashx?method=order_pass&order_id=' + id,
        params: {},
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                alert("����ʧ�ܣ�" + d.msg);
            } else {
                alert("��˳ɹ�");
                fnSearch();
            }
        },
        failure: function (result) {
            alert("����ʧ�ܣ�" + result.responseText);
        }
    });
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "74px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtOrderDate").hidden = false;
        Ext.getCmp("txtOrderDate").setVisible(true);
        Ext.getCmp("txtRequestDate").hidden = false;
        Ext.getCmp("txtRequestDate").setVisible(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtOrderDate").hidden = true;
        Ext.getCmp("txtOrderDate").setVisible(false);
        Ext.getCmp("txtRequestDate").hidden = true;
        Ext.getCmp("txtRequestDate").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}
