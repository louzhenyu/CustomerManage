var id, btncode;
/*提示插件*/
Ext.QuickTips.init();
var pEventID;

Ext.onReady(function () {
    pEventID = getUrlParam("pEventID");
    InitVE();
    InitStore();
    InitEditView();
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=" + __mid);
    fnSearchColumn();
});

/*查询方法*/
function fnSearch() {
    var myVip_Info = new Array();
    myVip_Info.push({ ControlType: '1', ControlName: 'UserName', ControlValue: Ext.getCmp("txt_VipName").jitGetValue() });
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=PageGridDataByEventID&pEventID=" + pEventID;
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = { pSearch: Ext.JSON.encode(myVip_Info) };
    Ext.getCmp("pageBar").moveFirst();
}

function fnColumnUpdate() { }

/*查询方法*/
function fnSearchColumn() {
    var dataurl = JITPage.HandlerUrl.getValue() + "&method=PageGridDataByEventID&pEventID=" + pEventID;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=InitGridDataByEventID&pEventID=" + pEventID,
        params: {

        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            var GridColumnDefinds = new Array();
            GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: "操作", sortable: false, dataIndex: "SignUpID", renderer: fnColumnDelete });
            for (var i = 0; i < jdata.GridColumnDefinds.length; i++) {
                if (jdata.GridColumnDefinds[i].DataIndex.toLowerCase() == "vipname") {
                    jdata.GridColumnDefinds[i].DataIndex = "vipname";
                }
                switch (jdata.GridColumnDefinds[i].ColumnControlType) {
                    case 1:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                    case 2:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                    case 3:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                    case 4:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                    case 11:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex, renderer: renderPhoto });
                        break;
                    default:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                }
            }

            //获取数据定义
            var GridDataDefinds = new Array();
            GridDataDefinds.push({ name: "SignUpID", type: 'string' });
            for (var i = 0; i < jdata.GridDataDefinds.length; i++) {
                if (jdata.GridDataDefinds[i].DataIndex.toLowerCase() == "vipname") {
                    jdata.GridDataDefinds[i].DataIndex = "vipname";
                }
                switch (jdata.GridDataDefinds[i].DataType) {

                    case 1:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                    case 2:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                    case 3:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                    case 4:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                    default:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                }
            }
            //提示错误信息
            if (jdata.msg != null) {
                Ext.Msg.alert("提示", jdata.msg);
                return;
            }

            //获取模型
            Ext.define('vipModel', {
                extend: 'Ext.data.Model',
                fields: eval(GridDataDefinds)
            });


            //获取数据审核列表信息
            new Ext.create('Ext.data.Store', {
                pageSize: JITPage.PageSize.getValue(),
                model: "vipModel",
                id: "vipStore",
                proxy: {
                    type: 'ajax',
                    url: dataurl,
                    reader: {
                        type: 'json',
                        root: 'GridData',
                        totalProperty: "RowsCount"
                    },
                    extraParams: { pSearch: "" },
                    actionMethods: { read: 'POST' }
                }
            });

            //重新绑定store
            Ext.getCmp("gridlist").reconfigure(Ext.getStore("vipStore"), eval(GridColumnDefinds));
            var myVip_Info = new Array();
            // 重新加载数据
            var store = Ext.getCmp("gridlist").getStore();
            Ext.getCmp("pageBar").bind(store);
            store.proxy.url = dataurl;
            Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
            Ext.getCmp("pageBar").store.proxy.extraParams = { pSearch: Ext.JSON.encode(myVip_Info) };
            Ext.getCmp("pageBar").moveFirst();

        },
        failure: function () {

        }
    });
}


/*导出数据*/
function fnExportData() {
    var myVip_Info = new Array();
    myVip_Info.push({ ControlType: '1', ControlName: 'UserName', ControlValue: Ext.getCmp("txt_VipName").jitGetValue() });
    window.open("Handler/EventsHandler.ashx?method=ExportUserListByEventID&r=" + Math.random() + "&pSearch=" + Ext.JSON.encode(myVip_Info) + "&pEventID=" + pEventID);
}

/*渲染删除列*/
function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete('" + value + "')\">删除</a>";
}

/*删除方法*/
function fnDelete(value) {
    Ext.Msg.confirm("请确认", "是否要删除数据？", function (button) {
        if (button == "yes") {
            var handlerUrl = JITPage.HandlerUrl.getValue() + "&method=delete";
            Ext.Ajax.request({
                url: handlerUrl,
                params: {
                    id: value
                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '删除成功',
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: function () {
                                parent.Ext.getStore("eventsStore").load();
                                Ext.getStore("vipStore").load();
                            }
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
            });
        }
    });
}