var __model = null;     //单个实体修改
var __modelList = null; //集合批量修改
var __StatusDate = null; //日期
var __type = 1;
var chkWeek;
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
    store.proxy.url = loadUrl;//设置ajax请求的地址
    store.load();//进行请求
}

//画空白的单元格
function fnBuildEmptyTD() {
    $("#tblMonth").find('tr').remove(); //删除月份
    $("#tblWeek").find('tr').remove();  //删除日历
    var beginDate = new Date(Ext.getCmp("txtStartTime").jitGetValue());
    var endDate = new Date(Ext.getCmp("txtEndTime").jitGetValue());
    chkWeek = Ext.getCmp("cmbWeek").jitGetValue();//获取当前选中的日期

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
                    pDate += beginDate.getFullYear() + "-" + (beginDate.getMonth() + 1) + "-" + beginDate.getDate() + ",";
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
                        pDate += beginDate.getFullYear() + "-" + (beginDate.getMonth() + 1) + "-" + beginDate.getDate() + ",";
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

        var trHeight = 45;  //日历一行对应月单元格的高度， 在aspx页面 样式.house_table td

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

    var storeID = Ext.getCmp("cmbStoreID").jitGetValue();//门店
    var skuID = Ext.getCmp("cmbHouseType").jitGetValue();//房型

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
                            $("#" + tdID).html("<li onclick='fnGetSingle(\"" + dateArray[j].StoreItemDailyStatusID + "\") '>" + firstDate.getDate() + "<br><i>¥" + dateArray[j].LowestPrice + "</i></li>");
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
    fnShowDiv();//弹出创建框
    __model = null;//设为null
    __StatusDate = pStatusDate;
    var date = new Date(pStatusDate);
    $("#hdnTDID").val("td" + date.getFullYear() + (date.getMonth() + 1) + date.getDate());
    $("#pDate").html(date.getFullYear() + "/" + (date.getMonth() + 1) + "/" + date.getDate());

}

//获取单个的房价信息
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
            __model = eval("(" + result.responseText + ")");  //返回值，这里要新增加一个SourcePrice***
            $("#txtSourcePrice").val(__model.SourcePrice); //原价***，不能和现在的价格一样
            $("#txtNowPrice").val(__model.LowestPrice);    //现价
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

//批量价格修改
function fnBatchUpdate() {
    chkWeek = Ext.getCmp("cmbWeek").jitGetValue();//星期
    var price = Ext.getCmp("txtPrice").jitGetValue();//批量修改的价格
    var storeID = Ext.getCmp("cmbStoreID").jitGetValue();//门店
    var skuID = Ext.getCmp("cmbHouseType").jitGetValue();//房型
    var BatchSourcePrice = Ext.getCmp("txtBatchSourcePrice").jitGetValue();//批量修改的价格
    
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

    if (isNaN(price) || parseInt(price) <= 0) {
        Ext.Msg.show({
            title: '警告',
            msg: "价格必须为正整数！",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }
    if (isNaN(BatchSourcePrice) || parseInt(BatchSourcePrice) <= 0) {
        Ext.Msg.show({
            title: '警告',
            msg: "原价必须为正整数！",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }

  
    var form = Ext.getCmp('btn_panel_batchupdate').getForm();//这个是批量修改所在的formPanel，能够通过序列化的方式获取
    if (!form.isValid()) {  //主要是验证的
        return false;
    }

    Ext.Msg.confirm("请确认", "确认批量修改以下数据吗？", function (button) {

        var myMask = new Ext.LoadMask(document.body, { msg: '系统处理中...' });
        myMask.show();

        if (button == "yes") {
            Ext.Ajax.request({
                method: 'post',
                sync: true,
                url: JITPage.HandlerUrl.getValue() + "&method=BatchUpdate",//批量修改方法
                params: {
                    OperateType: "price",
                    LowestPrice: price,
                    SourcePrice:BatchSourcePrice,
                    StockAmount: 100,  //非满房，什么意思？
                    beginDate: Ext.getCmp("txtStartTime").jitGetValue(),
                    endDate: Ext.getCmp("txtEndTime").jitGetValue(),
                    storeID: storeID,
                    skuID: skuID,   //房型
                    date: chkWeek  //星期
                },
                success: function (result, request) {
                    if (result.status == 200) {
                        fnSearch();  //重新查询一下
                        Ext.Msg.show({
                            title: '提示',
                            msg: "操作成功",
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO
                        });
                    }
                    Ext.getCmp("txtPrice").jitSetValue("")
                    Ext.getCmp("txtBatchSourcePrice").jitSetValue("")
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
    $("#txtNowPrice").val(0);    //现价，这个是添加的情况的
    $(".house_choose").show();
}

//单个价格修改
function fnUpdate() {

    var price = $("#txtNowPrice").val();
    var mySourcePrice = $("#txtSourcePrice").val();
    
    if (!price || isNaN(price) || parseInt(price) <= 0) {
        Ext.Msg.show({
            title: '警告',
            msg: "价格必须为正整数！",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }
    //再加一个原价
    if (!mySourcePrice || isNaN(mySourcePrice) || parseInt(mySourcePrice) <= 0) {
        Ext.Msg.show({
            title: '警告',
            msg: "原价必须为正整数！",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return;
    }
    //新增
    if (__model == null) {
        Ext.Ajax.request({
            method: 'post',
            sync: true,
            url: JITPage.HandlerUrl.getValue() + "&method=Add",
            params: {
                OperateType: "price",
                content: Ext.JSON.encode(
            {
                LowestPrice: price,    //最低价为0
                SourcePrice:mySourcePrice,//新增加原价
                StockAmount: 100,   //空房
                StatusDate: __StatusDate,
                storeID: Ext.getCmp("cmbStoreID").jitGetValue(), //'5aafaf0a78644b1f90d091b8dbfcc5b3',
                skuID: Ext.getCmp("cmbHouseType").jitGetValue()   //Ext.getCmp("cmbHouseType").jitGetValue(),
            })
            },
            success: function (result, request) {
                var returnModel = eval("(" + result.responseText + ")");
                var firstDate = new Date(returnModel.StatusDate);
                var tdID = $("#hdnTDID").val();
                $("#" + tdID).html("<li onclick='fnGetSingle(\"" + returnModel.StoreItemDailyStatusID + "\") '>" + firstDate.getDate() + "<br><i>¥" + returnModel.LowestPrice + "</i></li>");

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
        __model.LowestPrice = price;
        __model.SourcePrice = mySourcePrice;
        //给__model添加上SourcePrice
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
                $("#" + tdID).html("<li onclick='fnGetSingle(\"" + returnModel.StoreItemDailyStatusID + "\") '>" + firstDate.getDate() + "<br><i>¥" + returnModel.LowestPrice + "</i></li>");
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