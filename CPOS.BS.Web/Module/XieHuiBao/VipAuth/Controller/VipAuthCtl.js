var id, btncode, vipChecked = new Array(), K, htmlEditor, Checked;
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/VipAuthHandler.ashx?mid=" + __mid);

    $(".ke-edit-iframe").css("height", "200px");

    fnSearch();


    Ext.getCmp("gridlist").getStore().addListener({
        'load': function () {
            //fnRender();
        }
    });

});


//导出列表
function fnSearchExcel() {

    //确定是否导出当前数据
    Ext.MessageBox.confirm('提示信息', '确定导出当前数据?', function ex(btn) {
        if (btn == 'yes') {

            var myVip_Info = new Array();
            myVip_Info.push({ ControlType: '1', ControlName: 'VipName', ControlValue: Ext.getCmp("txtVipName").jitGetValue() });
            myVip_Info.push({ ControlType: '2', ControlName: 'Status', ControlValue: Ext.getCmp("cmbStatusType").jitGetValue() });

            //导出当前数据
            window.open(JITPage.HandlerUrl.getValue() + "&method=ExportData&pSearch=" + Ext.JSON.encode(myVip_Info));
        }
    });

}



/*查询方法*/
function fnSearch() {
    get('tab10').style.background = "#F3F3F6";
    var dataurl = JITPage.HandlerUrl.getValue() + "&method=PageGridData";

    Ext.Ajax.request({ ControlType: "", ControlName: "",
        url: JITPage.HandlerUrl.getValue() + "&method=InitGridData",
        params: {},
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);

            var GridColumnDefinds = new Array();
            GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Tips', text: "状态", sortable: false, dataIndex: "Status", width: 120 });
            for (var i = 0; i < jdata.GridColumnDefinds.length; i++) {
                switch (jdata.GridColumnDefinds[i].ColumnControlType) {
                    case 1:
                        if (jdata.GridColumnDefinds[i].DataIndex == "VipName") {
                            GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex, renderer: fnColumnUpdate });
                        } else {
                            GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'String', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        }
                        break;
                    case 2:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Int', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                    case 3:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Decimal', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
                        break;
                    case 4:
                        GridColumnDefinds.push({ xtype: 'jitcolumn', jitDataType: 'Date', text: jdata.GridColumnDefinds[i].ColumnText, sortable: false, dataIndex: jdata.GridColumnDefinds[i].DataIndex });
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
            for (var i = 0; i < jdata.GridDataDefinds.length; i++) {
                switch (jdata.GridDataDefinds[i].DataType) {
                    case 1:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                    case 2:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'int' });
                        break;
                    case 3:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'float' });
                        break;
                    case 4:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'datetime' });
                        break;
                    default:
                        GridDataDefinds.push({ name: jdata.GridDataDefinds[i].DataIndex, type: 'string' });
                        break;
                }
            }
            GridDataDefinds.push({ name: "VIPID", type: 'string' })
            GridDataDefinds.push({ name: "Status", type: 'string' });
            GridDataDefinds.push({ name: "Col14", type: 'string' });
            GridDataDefinds.push({ name: "Col15", type: 'string' });

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

            var myVip_Info = new Array();
            myVip_Info.push({ ControlType: '1', ControlName: 'VipName', ControlValue: Ext.getCmp("txtVipName").jitGetValue() });
            myVip_Info.push({ ControlType: '2', ControlName: 'Status', ControlValue: Ext.getCmp("cmbStatusType").jitGetValue() });


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
                    extraParams: { pSearch: Ext.JSON.encode(myVip_Info) },
                    actionMethods: { read: 'POST' }
                }
            });

            //重新绑定store
            Ext.getCmp("gridlist").reconfigure(Ext.getStore("vipStore"), eval(GridColumnDefinds));

            // 重新加载数据
            var store = Ext.getCmp("gridlist").getStore();
            Ext.getCmp("pageBar").bind(store);
            store.proxy.url = dataurl;
            Ext.getCmp("pageBar").store.proxy.extraParams = { pSearch: Ext.JSON.encode(myVip_Info) };

            store.load({
                params: {
                    limit: JITPage.PageSize.getValue(),
                    page: 1
                },
                callback: function () {
                    fnGetStatusCount();
                }
            });
        },
        failure: function () {

        }
    });
}


/*记住翻页选中*/
function fnRender() {
    if (Ext.getCmp("gridlist").getStore() == undefined) {
        return;
    }
    //加载完执行的信息
    var data1 = Ext.getCmp("gridlist").getStore().data;
    //遍历当前store的信息
    for (var i = 0; i < data1.length; i++) {
        //获得当前行
        var record1 = Ext.getCmp("gridlist").getStore().getAt(i);
        //获得主键id 判断是否需要选中
        var id = record1.get("VIPID");
        var flag = false;
        if (notappointordersChecked.length > 0) {
            for (var r = 0; r < notappointordersChecked.length; r++) {
                var everySelectId = notappointordersChecked[r].VIPID;
                if (id == everySelectId) {
                    flag = true;
                    break;
                }
            }
        }

        //为true说明是需要选中的
        if (flag) {
            Ext.getCmp("gridlist").selModel.select(Ext.getCmp("gridlist").getStore().getById(id), true);
        }
    }
}

/*查询方法*/
function fnSearchVip(status) {

    get("hStatus").value = status;
    for (var i = 10; i < 14; i += 1) {
        get('tab' + i).style.background = "#E6E4E1";
    }
    if (parseInt(status) == 0)
        get('tab10').style.background = "#F3F3F6";
    else
        get('tab' + status).style.background = "#F3F3F6";


    var dataurl = JITPage.HandlerUrl.getValue() + "&method=PageGridData";

    var myVip_Info = new Array();
    myVip_Info.push({ ControlType: '1', ControlName: 'VipName', ControlValue: Ext.getCmp("txtVipName").jitGetValue() });
    if (status != 0) {
        myVip_Info.push({ ControlType: '2', ControlName: 'Status', ControlValue: status });
    }
    var store = Ext.getCmp("gridlist").getStore();
    Ext.getCmp("pageBar").bind(store);

    store.proxy.url = dataurl;
    Ext.getCmp("pageBar").store.proxy.extraParams = { pSearch: Ext.JSON.encode(myVip_Info) };

    store.load({
        params: {
            limit: JITPage.PageSize.getValue(),
            page: 1
        },
        callback: function () {
            Ext.getCmp("gridlist").setLoading().hide();
            fnGetStatusCount();
        }
    });
}

/*获取各个状态的值*/
function fnGetStatusCount() {

    var myVip_Info = new Array();
    myVip_Info.push({ ControlType: '1', ControlName: 'VipName', ControlValue: Ext.getCmp("txtVipName").jitGetValue() });
    myVip_Info.push({ ControlType: '2', ControlName: 'Status', ControlValue: Ext.getCmp("cmbStatusType").jitGetValue() });

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetVipStatusNum",
        params: { pSearch: Ext.JSON.encode(myVip_Info) },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            get("txtNum0").innerHTML = jdata.allCount;
            get("txtNum1").innerHTML = jdata.unapproveCount;
            get("txtNum2").innerHTML = jdata.approveCount;
            get("txtNum3").innerHTML = jdata.approveSuccessCount;
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
        }
    });
}

function fnColumnUpdate(value, p, r) {
    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.VIPID + "','update');\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.VIPID + "','search');\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
}

/*明细数据*/
function fnUpdate(mid, bcode) {
    id = mid;
    /*获取控件*/
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + bcode + "&method=EditView",
        params: {},
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);

            var strIsMustDo = '';
            var lshowitem = new Array();
            for (var i = 0; i < jdata.length; i++) {
                switch (jdata[i].ControlType) {
                    case 1: //文本
                        lshowitem.push({
                            xtype: 'jittextfield',
                            jitSize: 'big',
                            width: 300,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo
                        });
                        break;
                    case 2: //整型文本
                        lshowitem.push({
                            xtype: 'jitnumberfield',
                            jitSize: 'big',
                            width: 300,
                            allowDecimals: false,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo
                        });
                        break;
                    case 3: //数字文本
                        lshowitem.push({
                            xtype: 'jitnumberfield',
                            allowDecimals: true,
                            jitSize: 'big',
                            width: 300,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo
                        });
                        break;
                    case 4: //日期
                        lshowitem.push({
                            xtype: 'jitdatefield',
                            jitSize: 'big',
                            id: '__Time_' + jdata[i].ControlName,
                            width: 300,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo
                        });
                        break;
                    case 6: //自定义下拉
                        lshowitem.push({
                            xtype: 'jitbizoptions',
                            jitSize: 'big',
                            width: 300,
                            OptionName: jdata[i].CorrelationValue,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo

                        });
                        break;
                    case 9: //文本
                        lshowitem.push({
                            xtype: 'jittextfield',
                            jitSize: 'big',
                            width: 300,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo
                        });
                        break;
                    case 10: //文本
                        lshowitem.push({
                            xtype: 'jittextfield',
                            jitSize: 'big',
                            width: 300,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType,
                            CorrelationValue: jdata[i].CorrelationValue,
                            IsMustDo: jdata[i].IsMustDo
                        });
                        break;
                    case 11: //照片
                        lshowitem.push({
                            xtype: 'jitdisplayfield',
                            jitSize: 'big',
                            width: 300,
                            OptionName: jdata[i].CorrelationValue,
                            fieldLabel: strIsMustDo + jdata[i].fieldLabel,
                            alertLabel: jdata[i].fieldLabel,
                            ControlName: jdata[i].ControlName,
                            ControlType: jdata[i].ControlType

                        })
                        break;

                }
            }
            var showHeight = 34;
            var f = parseInt(jdata.length / 2) + jdata.length % 2;
            if (f > 15) f = 15;
            showHeight = showHeight * f;
            Ext.getCmp("showPanel").removeAll();
            Ext.getCmp("showPanel").add(lshowitem);
            Ext.getCmp("showPanel").setHeight(showHeight);

            Ext.getCmp("editWin").height = showHeight + 80;
            Ext.getCmp("editWin").width = 680;
            Ext.getCmp("editWin").bodyStyle = 'background:#F1F2F5;';
            Ext.getCmp("editWin").show();
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
        }
    });
    /*获取控件的值*/
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + bcode + "&method=EditViewData",
        params: {
            id: mid
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            for (var i = 0; i < jdata.length; i++) {
                var c = fnGetCol(jdata[i].ControlName, i);
                try {
                    if (jdata[i].ControlType == 11) {
                        if (jdata[i].ControlValue != null && jdata[i].ControlValue != "") {
                            c.jitSetValue("<a rel=\"fancybox_group\" href=\"" + jdata[i].ControlValue + "\" ><img src=\"/Lib/Image/image.png\" /></a>");
                        } else {
                            c.jitSetValue("<img src=\"/Lib/Image/noimage.png\" />");
                        }
                    } else {
                        c.jitSetValue(jdata[i].ControlValue);
                    }
                } catch (e) {
                    c.setValue(jdata[i].ControlValue);
                }
            }

            Ext.Ajax.request({
                url: JITPage.HandlerUrl.getValue() + "&btncode=" + bcode + "&method=GetVipStatusInfo",
                params: { id: mid },
                method: 'post',
                success: function (result) {
                    var info = Ext.JSON.decode(result.responseText);
                    //审核过后 不可再进行审核操作
                    if (info.status == 13 || info.status == 14) {
                        Ext.getCmp("btnApprove").hide();
                        Ext.getCmp("btnNoApprove").hide();
                    } else {
                        Ext.getCmp("btnApprove").show();
                        Ext.getCmp("btnNoApprove").show();
                    }
                }, failure: function () { }
            });

        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
        }
    });
}

/*获取详情的数据*/
function fnGetCol(pName, index) {
    for (var i = 0; i < Ext.getCmp("editWin").items.items[0].items.length; i++) {
        var o = Ext.getCmp("editWin").items.items[0].items.items[i];
        if (o.ControlName == pName) {
            return o;
        }
    }
}

/*审核通过*/
function fnApprove() {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=VipApprove",
        params: {
            id: id,
            status: '13'
        },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);
            if (jdata.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("editWin").hide();

                        fnSearch();
                    }
                });
            }

        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
        }
    });
}

function fnNoApprove() {
    Ext.getCmp("operationPanel").getForm().reset();
    Ext.getCmp("operationWin").show();
}

/*审核不通过*/
function fnOperationSubmit() {
    var form = Ext.getCmp('operationPanel').getForm();
    if (Ext.getCmp("remark").jitGetValue() == null || Ext.getCmp("remark").jitGetValue() == "") {
        Ext.Msg.show({
            title: '警告',
            msg: "'备注'不能为空",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }

    if (!form.isValid()) {
        return false;
    }
    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&method=NoVipApprove",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            remarke: Ext.getCmp("remark").jitGetValue(),
            status: '14'
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("operationWin").hide();
                        Ext.getCmp("editWin").hide();
                        fnSearch();
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

/*列表页渲染图片*/
function renderPhoto(val, metaData, record, rowIndex, colIndex, store, view) {
    var photoValue = "";
    if (val != null && val != "") {
        try {
            return '<a rel="fancybox_group" href="' + val + '" ><img src="/Lib/Image/image.png" /></a>';
        } catch (e) {
            return "<img src='/Lib/Image/noimage.png' /> ";
        }
    }
    return "<img src='/Lib/Image/noimage.png' /> ";
}

function fnShowSendMessageWin() {

    if (vipChecked.length < 1) {
        Ext.Msg.show({
            title: '提示',
            msg: '请选择用户',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }

    var parameters = "?params=" + fnGetVipInfoFromArray(vipChecked) + "&r=" + Math.random();
    var src = "/Project/SendMessage/SendMessage.aspx" + parameters;

    Ext.getCmp("sendMessageWin").show();
    document.getElementById("framemessage").setAttribute("src", src);
}

/*@method 从array获取ID string 格式1,2,4,5*/
function fnGetVipInfoFromArray(checkArray) {
    var idList = "", nameList = "";
    /*获取选择的ID*/
    if (checkArray.length > 0) {
        for (var i = 0; i < checkArray.length; i++) {
            idList = idList + checkArray[i].Email + ",";
            nameList = nameList + checkArray[i].VipName + ",";
        }
        idList = idList.substring(0, idList.length - 1);
        nameList = nameList.substring(0, nameList.length - 1);
    }
    return idList + "|" + encodeURI(nameList);
}

/*数据提交*/
function fnSubmit() {
    var l = new Array();
    for (var i = 0; i < Ext.getCmp("editWin").items.items[0].items.length; i++) {
        var c = Ext.getCmp("editWin").items.items[0].items.items[i];
        if (c.jitGetValue(c) != null && c.jitGetValue(c) != "") {
            if (c.ControlType == 11) {
                debugger;
                var value = fnGetSearchValue(c.jitGetValue());
                var res = $(value).attr("href");
                var o = Object();
                o.ControlType = c.ControlType;
                o.ControlValue = res;
                o.ControlName = c.ControlName;
                o.CorrelationValue = c.CorrelationValue;
                l.push(o);
            }
            else {
                var o = Object();
                o.ControlType = c.ControlType;
                o.ControlValue = fnGetSearchValue(c.jitGetValue());
                o.ControlName = c.ControlName;
                o.CorrelationValue = c.CorrelationValue;
                l.push(o);
            }
        }
    }

    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: "系统提交中..." });
    myMask.show();
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue(),
        params: {
            method: 'update',
            pEditValue: Ext.JSON.encode(l),
            pKeyValue: id
        },
        method: 'POST',
        callback: function (options, success, response) {
            myMask.hide();
            if (success == true) {
                var jdata = Ext.JSON.decode(response.responseText);
                if (jdata.success) {
                    Ext.Msg.show({
                        title: "提示",
                        msg: (jdata.msg == "" ? "保存成功" : jdata.msg),
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.INFO,
                        fn: function () {
                            Ext.getCmp("editWin").hide();
                            Ext.getCmp("gridlist").getStore().load();
                        }
                    });
                }
                else {
                    Ext.Msg.show({
                        title: "错误",
                        msg: jdata.msg,
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.ERROR
                    });
                }
            }
            else {
                Ext.Msg.show({
                    title: "错误",
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        }
    });
}

function fnGetSearchValue(value) {
    if (Object.prototype.toString.call(value) == '[object Array]') {
        var a = '';
        for (var i = 0; i < value.length; i++) {
            if (i == 0) {
                a = value[i].toString();
            }
            else {
                a = a + ',' + value[i].toString();
            }
        }
        return a;
    }
    else {
        return value;
    }
}