var __model = null;     //单个实体修改
var __modelList = null; //集合批量修改
var __StatusDate = null; //日期
var __type = 1;
var chkWeek;
var __res = "";
Ext.onReady(function () {
    //加载需要的文件
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/StoreItemDailyStatusHandler.ashx?mid=" + __mid);
    fnLoadStoreCmb(); //加载下拉选项

    //加载终端
    Ext.getStore("unitStore").addListener({
        'load': function () {
            Ext.getCmp("cmbStoreID").jitSetValue(Ext.getStore("unitStore").first().data.unit_id);
        }
    });

    //加载房型
    Ext.getStore("houseTypeStore").addListener({
        'load': function () {
            if (Ext.getStore("houseTypeStore").first()) {
                Ext.getCmp("cmbHouseType").jitSetValue(Ext.getStore("houseTypeStore").first().data.sku_id);
            } else {
                Ext.getCmp("cmbHouseType").jitSetValue("");
            }
            if (__type == 1) {  //如果是第一次，则调用查询事件
                fnSearch();
                __type++;
            }
        }
    });

    $("#radio2-boxLabelEl").click(function () {
        Ext.getCmp("txtStockAmount").jitSetValue(100);
        $("#txtStockAmount").css("display", "block");
    });
    $("#radio1-boxLabelEl").click(function () {
        $("#txtStockAmount").css("display", "none");
    });
});

//加载终端下拉列表
function fnLoadStoreCmb() {
    var unitStore = Ext.getStore("unitStore");
    unitStore.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetStoreList";
    unitStore.load();
}

//加载房型
function fnLoadTypeCmb() {
    var storeID = Ext.getCmp("cmbStoreID").jitGetValue();
    var loadUrl = JITPage.HandlerUrl.getValue() + "&method=getHouseType";
    if (storeID) {
        loadUrl += "&storeID=" + storeID;
    }
    var store = Ext.getStore("houseTypeStore");
    store.proxy.url = loadUrl;
    store.load();
}

//画空白的单元格
function fnBuildEmptyTD() {
    $("#tblMonth").find('tr').remove(); //删除月份
    $("#tblWeek").find('tr').remove();  //删除日历
    var beginDate = new Date(Ext.getCmp("txtStartTime").jitGetValue());
    var endDate = new Date(Ext.getCmp("txtEndTime").jitGetValue());
    chkWeek = Ext.getCmp("cmbWeek").jitGetValue();

    var $tr = $('<tr></tr>');        //创建行
    var trRows = 0;    //计算行数
    var day = beginDate.getDay();    //前面需要空格的单元格  
    var alllength = 0;
    for (var k = 0; k < day; k++) {
        alllength++;
        $('<td></td>').html("").appendTo($tr);
    }
    var arrayMonth = new Array();

    var beginMonth = new Date(beginDate.getFullYear() + "/" + (beginDate.getMonth() + 1) + "/1");
    var endMonth = new Date(endDate.getFullYear() + "/" + (endDate.getMonth() + 1) + "/1");
    for (; beginMonth <= endMonth; beginMonth.setMonth(beginMonth.getMonth() + 1)) {
        arrayMonth.push(beginMonth.getFullYear() + "-" + (beginMonth.getMonth() + 1));
    }

    beginDate = new Date(Ext.getCmp("txtStartTime").jitGetValue());  //开始时间
    arrayMonth = unique(arrayMonth); //抽取月数组
    for (var i = 0; i < arrayMonth.length; i++) {
        for (; beginDate <= endDate; beginDate.setDate(beginDate.getDate() + 1)) {
            if (chkWeek == null || chkWeek == "" || chkWeek.length == 0) {
                //循环当前月份的日期
                if (beginDate.getFullYear() + "-" + (beginDate.getMonth() + 1) == arrayMonth[i]) {
                    alllength++;
                    //添加空白单元格
                    $('<td id="td' + beginDate.getFullYear() + (beginDate.getMonth() + 1) + beginDate.getDate() + '" style="cursor:pointer;" onclick="fnAdd(\'' + formatDate(beginDate) + '\')" ></td>').html(beginDate.getDate()).appendTo($tr);
                    //循环日期
                    if (alllength % 7 == 0) {
                        $("#tblWeek").append($tr);  //如果多出7个单元格，则增加一行
                        $tr = $('<tr></tr>');
                    }
                } else {
                    break;
                }
            } else {
                if (chkWeek.indexOf(beginDate.getDay()) >= 0) {
                    //循环当前月份的日期
                    if (beginDate.getFullYear() + "-" + (beginDate.getMonth() + 1) == arrayMonth[i]) {
                        alllength++;
                        //添加空白单元格
                        $('<td id="td' + beginDate.getFullYear() + (beginDate.getMonth() + 1) + beginDate.getDate() + '" style="cursor:pointer;" onclick="fnAdd(\'' + formatDate(beginDate) + '\')" ></td>').html(beginDate.getDate()).appendTo($tr);
                        //循环日期
                        if (alllength % 7 == 0) {
                            $("#tblWeek").append($tr);  //如果多出7个单元格，则增加一行
                            $tr = $('<tr></tr>');
                        }
                    } else {
                        break;
                    }
                } else {
                    //循环当前月份的日期
                    if (beginDate.getFullYear() + "-" + (beginDate.getMonth() + 1) == arrayMonth[i]) {
                        alllength++;
                        //添加空白单元格
                        $('<td></td>').html(beginDate.getDate()).appendTo($tr);
                        //循环日期
                        if (alllength % 7 == 0) {
                            $("#tblWeek").append($tr);  //如果多出7个单元格，则增加一行
                            $tr = $('<tr></tr>');
                        }
                    } else {
                        break;
                    }
                }
            }
        }

        if (alllength % 7 > 0) {
            var residueNum = (parseInt(alllength / 7) + 1) * 7 - alllength;
            for (var a = 0; a < residueNum; a++) {
                $('<td></td>').html("").appendTo($tr);
            }
            $("#tblWeek").append($tr);
            trRows = (parseInt(alllength / 7) + 1);  //行数累加
        } else {
            $("#tblWeek").append($tr);
            trRows = parseInt(alllength / 7);  //行数累加
        }

        var trHeight = 61;  //日历一行对应月单元格的高度， 在aspx页面 样式.house_table td

        var $trmonth = $('<tr><td style="height:' + (trHeight * trRows) + 'px">' + arrayMonth[i].replace('-', '<br/>') + '月</td></tr>');        //创建月表格行

        $("#tblMonth").append($trmonth);

        alllength = 0;
        day = beginDate.getDay();    //前面需要空格的单元格
        trRows = 0; //行数
        $tr = $('<tr></tr>');
        for (var k = 0; k < day; k++) {
            alllength++;
            $('<td></td>').html("").appendTo($tr);
        }
    }
}
//查询方法
function fnSearch() {
    $("#tblMonth").find('tr').remove(); //删除月份
    $("#tblWeek").find('tr').remove();  //删除日历

    var storeID = Ext.getCmp("cmbStoreID").jitGetValue();
    var skuID = Ext.getCmp("cmbHouseType").jitGetValue();

    if (!storeID) {
        Ext.Msg.show({
            title: '警告',
            msg: '请选择"门店"',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }
    if (!skuID) {
        Ext.Msg.show({
            title: '警告',
            msg: '请选择"房型"',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }
    if (Ext.getCmp("cmbStoreID").jitGetValue() && Ext.getCmp("cmbHouseType").jitGetValue()) {
        var myMask = new Ext.LoadMask(document.body, { msg: '系统处理中...' });
        myMask.show();

        fnBuildEmptyTD();
        $("#h_housetype").html(Ext.getCmp("cmbHouseType").getRawValue()); //设置房型标题
        Ext.Ajax.request({
            method: 'GET',
            sync: true,
            url: JITPage.HandlerUrl.getValue() + "&method=GetList",
            params: {
                beginDate: Ext.getCmp("txtStartTime").jitGetValue(),
                endDate: Ext.getCmp("txtEndTime").jitGetValue(),
                storeID: Ext.getCmp("cmbStoreID").jitGetValue(), //'5aafaf0a78644b1f90d091b8dbfcc5b3',
                skuID: Ext.getCmp("cmbHouseType").jitGetValue(), //'22BA521E42C847B7A1B5541FD520649B',
                pageIndex: 1,
                pageSize: 10000
            },
            success: function (result, request) {

                var returnResult = Ext.decode(result.responseText);

                if (returnResult && returnResult.topics.length > 0) {
                    var dateArray = returnResult.topics;
                    __modelList = returnResult.topics;

                    for (var j = 0; j < dateArray.length; j++) {
                        var firstDate = new Date(dateArray[j].StatusDate);
                        var tdID = "td" + firstDate.getFullYear() + (firstDate.getMonth() + 1) + firstDate.getDate();
                        if (tdID) {
                            $("#" + tdID).removeAttr("onclick");
                            var houseStateHtml = "";
                            if (dateArray[j].StockAmount == null || parseInt(dateArray[j].StockAmount) == 0) {
                                houseStateHtml = "<i class=\"color01\">满房</i>";
                            } else {
                                houseStateHtml = "<i class=\"color02\">非满房</i>" + dateArray[j].StockAmount + "/" + dateArray[j].UsedAmount;
                            }

                            $("#" + tdID).html("<li onclick='fnGetSingle(\"" + dateArray[j].StoreItemDailyStatusID + "\") '>" + firstDate.getDate() + houseStateHtml + "</li>");
                        }
                    }
                }
                myMask.hide();
            },
            failure: function (result) {
                Ext.Msg.show({
                    title: '警告',
                    msg: "操作失败：" + result.responseText,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                myMask.hide();
            }
        });
    }
}

//创建单个
function fnAdd(pStatusDate) {
    fnShowDiv();
    __model = null;
    __StatusDate = pStatusDate;
    var date = new Date(pStatusDate);
    $("#hdnTDID").val("td" + date.getFullYear() + (date.getMonth() + 1) + date.getDate());
    $("#pDate").html(date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate());

}

//获取单个信息
function fnGetSingle(pDetailID) {
    fnShowDiv();
    Ext.Ajax.request({
        method: 'get',
        sync: true,
        url: JITPage.HandlerUrl.getValue() + "&method=GetById", //'Handler/StoreItemDailyStatusHandler.ashx?method=GetList',
        params: {
            id: pDetailID
        },
        success: function (result, request) {
            __model = eval("(" + result.responseText + ")");
            if (__model.StockAmount == null || parseInt(__model.StockAmount) == 0) {
                $("input:radio[value=0]").attr('checked', 'true');
                fnGetStock(0, 1);
            } else {
                $("input:radio[value=100]").attr('checked', 'true');
                fnGetStock(100, 0);
            }
            $("input[name=txt_StockAmount]").val(__model.StockAmount == null ? 100 : __model.StockAmount);
            $("input[name=txt_UserAmount]").val(__model.UsedAmount == null ? 0 : __model.UsedAmount);
            __res = $("input[name=txt_StockAmount]").val();
            var date = new Date(__model.StatusDate);
            $("#hdnTDID").val("td" + date.getFullYear() + (date.getMonth() + 1) + date.getDate());
            $("#pDate").html(date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate());
        },
        failure: function (result) {
            Ext.Msg.show({
                title: '警告',
                msg: "操作失败：" + result.responseText,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
        }
    });
}

function fnGetStock(val, type) {
    if (val == 100) {
        $("#stock").css("display", "block");
        $("input[name=txt_StockAmount]").val(100);
    } else if (type == 2) {
        $("#stock").css("display", "none");
        $("input[name=txt_StockAmount]").val(0);
    } else if (type == 1) {
        $("#stock").css("display", "none");
    }
}

//批量价格修改
function fnBatchUpdate() {
    //获取单选按钮值
    var stockAmount = null;
    chkWeek = Ext.getCmp("cmbWeek").jitGetValue();
    if (Ext.getCmp("radio1").checked) {
        stockAmount = 0;
    }

    if (Ext.getCmp("radio2").checked) {
        stockAmount = Ext.getCmp("txtStockAmount").jitGetValue();
    }

    var storeID = Ext.getCmp("cmbStoreID").jitGetValue();
    var skuID = Ext.getCmp("cmbHouseType").jitGetValue();

    if (isNaN(stockAmount)) {
        Ext.Msg.show({
            title: '警告',
            msg: '请选择房间状态',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }

    if (!storeID) {
        Ext.Msg.show({
            title: '警告',
            msg: '请选择"门店"',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }

    if (!skuID) {
        Ext.Msg.show({
            title: '警告',
            msg: '请选择"房型"',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }

    Ext.Msg.confirm("请确认", "确认批量修改以下数据吗？", function (button) {

        var myMask = new Ext.LoadMask(document.body, { msg: '系统处理中...' });
        myMask.show();

        if (button == "yes") {
            Ext.Ajax.request({
                method: 'post',
                sync: true,
                url: JITPage.HandlerUrl.getValue() + "&method=BatchUpdate",
                params: {
                    OperateType: "stockAmount",
                    LowestPrice: 0,
                    StockAmount: stockAmount,
                    beginDate: Ext.getCmp("txtStartTime").jitGetValue(),
                    endDate: Ext.getCmp("txtEndTime").jitGetValue(),
                    storeID: storeID,
                    skuID: skuID,
                    date: chkWeek
                },
                success: function (result, request) {
                    if (result.status == 200) {
                        fnSearch();
                        Ext.Msg.show({
                            title: '提示',
                            msg: "操作成功",
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO
                        });
                    }
                    myMask.hide();
                },
                failure: function (result) {
                    Ext.Msg.show({
                        title: '警告',
                        msg: "操作失败：" + result.responseText,
                        buttons: Ext.Msg.OK,
                        icon: Ext.Msg.INFO
                    });
                    myMask.hide();
                }
            });
        } else {
            myMask.hide();
        }
    });
}

//显示层
function fnShowDiv() {
    var height = $(window).scrollTop() + 200;
    $(".house_choose").css("left", "500px");
    $(".house_choose").css("top", height + "px");
    $("#txtSourcePrice").val(0); //原价
    $("#txtNowPrice").val(0);    //现价
    $(".house_choose").show();
}

//单个价格修改
function fnUpdate() {
    var stockAmount = $('input[name="txt_StockAmount"]').val();
    var useAmount = $('input[name="txt_UserAmount"]').val();
    //新增
    if (__model == null) {
        Ext.Ajax.request({
            method: 'post',
            sync: true,
            url: JITPage.HandlerUrl.getValue() + "&method=Add",
            params: {
                OperateType: "stockAmount",
                content: Ext.JSON.encode({
                    LowestPrice: 0,    //最低价为0
                    StockAmount: stockAmount,   //空房
                    UsedAmount: useAmount,
                    StatusDate: __StatusDate,
                    storeID: Ext.getCmp("cmbStoreID").jitGetValue(), //'5aafaf0a78644b1f90d091b8dbfcc5b3',
                    skuID: Ext.getCmp("cmbHouseType").jitGetValue()   //Ext.getCmp("cmbHouseType").jitGetValue(),
                })
            },
            success: function (result, request) {
                var returnModel = eval("(" + result.responseText + ")");
                var firstDate = new Date(returnModel.StatusDate);
                var tdID = $("#hdnTDID").val();
                var houseStateHtml = "";
                if (returnModel.StockAmount == null || parseInt(returnModel.StockAmount) == 0) {
                    houseStateHtml = "<i class=\"color01\">满房</i>";
                } else {
                    houseStateHtml = "<i class=\"color02\">非满房</i>" + returnModel.StockAmount + "/" + returnModel.UsedAmount;
                }

                $("#" + tdID).html("<li onclick='fnGetSingle(\"" + returnModel.StoreItemDailyStatusID + "\") '>" + firstDate.getDate() + houseStateHtml + "</li>");
            },
            failure: function (result) {
                Ext.Msg.show({
                    title: '警告',
                    msg: "操作失败：" + result.responseText,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            }
        });
    } else {
        __model.StockAmount = stockAmount;
        __model.UsedAmount = useAmount;
        //修改
        Ext.Ajax.request({
            method: 'post',
            sync: true,
            url: JITPage.HandlerUrl.getValue() + "&method=Update",
            params: {
                content: Ext.JSON.encode(__model)
            },
            success: function (result, request) {
                var returnModel = eval("(" + result.responseText + ")");
                var firstDate = new Date(returnModel.StatusDate);
                var tdID = $("#hdnTDID").val();

                if (returnModel.StockAmount == null || parseInt(returnModel.StockAmount) == 0) {
                    houseStateHtml = "<i class=\"color01\">满房</i>";
                } else {
                    houseStateHtml = "<i class=\"color02\">非满房</i>" + returnModel.StockAmount + "/" + returnModel.UsedAmount;
                }
                $("#" + tdID).html("<li onclick='fnGetSingle(\"" + returnModel.StoreItemDailyStatusID + "\") '>" + firstDate.getDate() + houseStateHtml + "</li>");
            },
            failure: function (result) {
                Ext.Msg.show({
                    title: '警告',
                    msg: "操作失败：" + result.responseText,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            }
        });
    }
    fnCloseDiv();
}

//隐藏弹出层
function fnCloseDiv() {
    $(".house_choose").hide();
}

//数组去重
function unique(pResult) {
    var res = [];
    var json = {};
    for (var i = 0; i < pResult.length; i++) {
        if (!json[pResult[i]]) {
            res.push(pResult[i]);
            json[pResult[i]] = 1;
        }
    }
    return res;
}

function formatDate(day) {
    var Year = 0;
    var Month = 0;
    var Day = 0;
    var CurrentDate = "";
    //初始化时间 
    Year = day.getFullYear(); //ie火狐下都可以 
    Month = day.getMonth() + 1;
    Day = day.getDate();

    CurrentDate += Year + "-";
    if (Month >= 10) {
        CurrentDate += Month + "-";
    }
    else {
        CurrentDate += "0" + Month + "-";
    }
    if (Day >= 10) {
        CurrentDate += Day;
    }
    else {
        CurrentDate += "0" + Day;
    }
    return CurrentDate;
}

function fnBatchAmount(type, val) {
    if (parseInt(type) == 100) {
        Ext.getCmp("txtStockAmount").show();
    } else {
        Ext.getCmp("txtStockAmount").hide();
    }
}