
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载  
    JITPage.HandlerUrl.setValue("Handler/MapHandler.ashx?mid=");
    Ext.getCmp("btnSend_btn").hide();
    fnLoad();
});
fnLoad = function() {
    var id = getUrlParam("id");
    top.fnOrder(id);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetOrder",
        params: {
            id: id
        },
        method: 'post',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d != null) {
                if (d.Address != null && d.Address.length > 0) {
                    var add = d.Address
                    if (add.indexOf('+')) add = add.substring(0, add.indexOf('+'));
                    get("txtAddress").innerHTML = add;
                    get("txtAddress").title = d.Address;
                }
                if (d.CreateTime != null) {
                    var date = getDate(d.CreateTime);
                    get("txtTime").innerHTML = date.substring(0, date.length - 3);
                }
                if (d.Status == 4) {
                    Ext.getCmp("btnSend_btn").hide();
                } else {
                    //Ext.getCmp("btnSend_btn").show();
                }
                
                if (d.Qty != null && d.Qty > 0) {
                    get("txtOrderQty2").innerHTML = d.Qty + " 件";
                }
                if (d.Phone != null && d.Phone > 0) {
                    get("txtPhone").innerHTML = d.Phone + "";
                }
                
                var mapData2 = top.mapData2;
                for (var i = 0; i < mapData2.data.length; i++) {
                    if (id == mapData2.data[i].StoreID) {
                        if (mapData2.data[i].SendUserList != undefined && mapData2.data[i].SendUserList != null) {
                            //p = mapData2.data[i].SendUserList;
                            var userList = mapData2.data[i].SendUserList.split(',');
                            get("txtUser").innerHTML = userList.length + " 人";
                            //for (var j = 0; j < userList.length; j++) {
                            //    if (userList[j].length > 0) {
                            //        var p = '[{"StoreID":"'+ mapData2.data[i].StoreID +'"},{"StoreID":"' + userList[j] + '"}]';
                            //        map._map_CreateLine(newGuid(),p,"line",true,false,"1","end","3");
                            //    }
                            //}
                        }
                    }
                }

            }
        },
        failure: function (result) {
            alert("错误：" + result.responseText);
        }
    });
}
function fnSave() {
    var flag;
    var item = {};
    var userId = $('input[name="order"]:checked').val();
    if (userId == null || userId.length == 0) {
        alert("请选择人员");
        return;
    }
    var id = getUrlParam("id");
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: JITPage.HandlerUrl.getValue() + "&method=SendOrder", 
        params: {
            id: id,
            userId: userId
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("数据发送失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("数据发送成功");
                flag = true;
                get("pnl2").style.display = "none";
                get("pnl").style.display = "";
                fnLoad();
                top.fnSearch(id);
            }
        },
        failure: function (result) {
            showError("数据发送失败：" + result.responseText);
        }
    });
}

fnSend = function() {
    get("pnl").style.display = "none";
    get("pnl2").style.display = "";
    
    var id = getUrlParam("id");
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetOrder",
        params: {
            id: id
        },
        method: 'post',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d != null) {
                get("txtOrderNo").innerHTML = d.OrderCode;
                get("txtAddress2").innerHTML = d.Address;
                get("txtOrderQty").innerHTML = d.Qty + " 件";
                
            }
        },
        failure: function (result) {
            alert("错误：" + result.responseText);
        }
    });

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetOrderTable",
        params: {
            id: id
        },
        method: 'post',
        success: function (response) {
            var d = response.responseText;
            if (d != null) {
                get("txtList").innerHTML = d;
                
            }
        },
        failure: function (result) {
            alert("错误：" + result.responseText);
        }
    });

}
fnClose = function() {
    var map = top.window.frames["MapFlash"].index;
    var d = top.mapData2;
    var storeId = getUrlParam("id");
    var p = "";
    var mapData2 = top.mapData2;

    for (var i = 0; i < mapData2.data.length; i++) {
        if (storeId == mapData2.data[i].StoreID) {
            if (mapData2.data[i].SendUserList != undefined && mapData2.data[i].SendUserList != null) {
                p = mapData2.data[i].SendUserList;
                //var userList = mapData2.data[i].SendUserList.split(',');
                //for (var j = 0; j < userList.length; j++) {
                //    if (userList[j].length > 0) {
                //        var p = '[{"StoreID":"'+ mapData2.data[i].StoreID +'"},{"StoreID":"' + userList[j] + '"}]';
                //        map._map_CreateLine(newGuid(),p,"line",true,false,"1","end","3");
                //    }
                //}
            }
        }
    }
    map._map_ClearLines("");
    map._map_HidePopUp();
}