
var DefaultPageSize = 15;
var DetailPageSize = 10;
var MaxPageSize = 10000;
var DefaultGridWidth = "100%"; // 620
var DefaultGridHeight = 367; //284 399
var DetailGridWidth = "100%"; // 620
var DetailGridHeight = 255; //284 399
var defaultCtrlWidth = 100;
var defaultCtrlHeight = 100;
var detailWinWidth = 840;
var detailWinHeight = 550;
var SearchPanelWidth = 820;
var SearchPanelHeight = 44;
var sp = ",";
var spMsg = "$";

var SearchPanelMoreBtnText = "高级查询";
var SearchPanelMoreBtnHideText = "隐藏高级查询";

//Ext.lib.Ajax.defaultPostHeader += ";charset=utf-8";

var timeoutMsg = "页面超时，请重新登录";
function SetLogoInfo() {
  $.ajax({
        method: 'GET',
        async: true,
        url: '/Module/CustomerBasicSetting/Handler/CustomerBasicSettingHander.ashx?mid=' + getUrlParam('mid') + '&method=GetCustomerList',
        success: function (response) {
            var data = JSON.parse(response);
            var logo = $('#img_logo');
            //  debugger;
            logo.attr('alt', data.data.loadInfo.customerName);
            logo.closest('a').attr('title', data.data.loadInfo.customerName);

            $('#unitName').html(data.data.loadInfo.customerName); //title是全称html是简写名;        

            var str = $("#lblLoginUserName").html();
            var UnitName = window.UnitShortName ? window.UnitShortName : window.UnitName;
            window.UserName = str;
            UnitName = UnitName.length > 8 ? UnitName.substring(0, 8) + "..." : UnitName
            $("#lblLoginUserName").html("<b>|</b>" + UnitName + "&nbsp;&nbsp;&nbsp;" + str).attr("title", window.UnitName + "(" + window.RoleName + ")");
            if (data.data.requset != null) {
                for (var i = 0; i < data.data.requset.length; i++) {
                    var code = data.data.requset[i].SettingCode;
                    if (code == 'WebLogo') {
                        var val = ''; //data.data.requset[i].SettingValue;
                        if (val == '') break;
                        logo.attr('src', val).css({ 'margin-top': 'auto', 'max-width': '179px', 'max-height': '50px' });
                        break;
                    }
                }
            }

            var menuZoom = function () {
                $("#commonNav").show();
                var countWidth = $(".commonHeader").outerWidth(true);  //总的的宽度

                var handleWrapWidth = $(".handleWrap").outerWidth(true); //右侧门店名字的宽度
                var logoWrapWidth = $(" .commonHeader .logoWrap").outerWidth(true);    //logo的宽度

                var autoWidth = countWidth - handleWrapWidth - logoWrapWidth;
                if ($("#commonNav li.dropDown li").length > 0) {
                    $("#commonNav .addul").append($("#commonNav li.dropDown li"));
                    $("#commonNav li.dropDown").remove();
                }



                var ulList = $("<ul></ul>");
                var liCountWidth = $("#commonNav").outerWidth(true) - $("#commonNav").width() + 36; //包含边框宽度
                liCountWidth += $("#commonNav li.dropDown").outerWidth(true) ? $("#commonNav li.dropDown").outerWidth("true") : 0
                $("#commonNav ul li").each(function () {
                    liCountWidth += $(this).outerWidth(true);
                    if (liCountWidth >= autoWidth) {
                        ulList.append($(this));
                    }
                });

                if (ulList.find("li").length > 0) {
                    $("#commonNav li.dropDown").remove();
                    $("#commonNav ul.clearfix").append("<li class='dropDown'></li>");
                    $("#commonNav li.dropDown").append(ulList);
                    $("#commonNav li.dropDown").find("ul").hide();

                }


            };
            menuZoom();


            $(window).resize(function () {
                menuZoom();
            });

        }
    });
}

$(function () {

    var height=$(window).outerHeight()-$(".commonHeader").outerHeight();
    if($("#contentArea").outerHeight()<height ){
        $("#contentArea").css({"min-height":height+"px"})
    }
    if($("#contentArea").outerHeight()<$("#leftMenu").outerHeight()) {
        $("#contentArea").css({"min-height":$("#leftMenu").outerHeight()+"px"})
    }
    $(window).resize(function () {
        var height=$(window).outerHeight()-$(".commonHeader").outerHeight();
        if($("#contentArea").outerHeight()<height ){
            $("#contentArea").css({"min-height":height+"px"})
        }
        if($("#contentArea").outerHeight()<$("#leftMenu").outerHeight()) {
            $("#contentArea").css({"min-height":$("#leftMenu").outerHeight()+"px"})
        }
    });

    $('#section').resize(function() {
        if ($("#contentArea").outerHeight() < $("#section").outerHeight()) {
            $("#contentArea").css({"height": $("#section").outerHeight() + "px"})
        }
    });

    $("#commonNav").delegate(".dropDown",'click',function(){
        $(this).find("ul").stop().show();
    }).delegate(".dropDown",'mouseenter',function(){
        $(this).find("ul").stop().show();
    }).delegate(".dropDown",'mouseleave',function(){
        var me=  $(this);
        setTimeout(function(){
            me.find("ul").stop().hide();
        },300) //设置一个超时对象

    }).delegate(".dropDown ul",'mouseenter',function(){
        $(this).stop().show();
    }).delegate(".dropDown ul",'mouseleave',function(){
        $(this).stop().hide();
    });
    SetLogoInfo(); //头部导航菜单右侧部分赋值

    $("#section").delegate(".datagrid-header-check", "mousedown", function (e) {
        var dom = $(this);
        if (!dom.find("input").get(0).checked) {
            $(this).addClass("on");
        } else {
            $(this).removeClass("on");
        }
        return false;
    }).delegate(".datagrid-cell-check", "mousedown", function (e) {
        var dom = $(this);
        var nondes = dom.parents(".datagrid-body-inner").find(".datagrid-cell-check input");
        //验证是否是全选
        var isSeletAll=true;
        for (var i = 0; i < nondes.length; i++) {
            if (!nondes.get(i).checked&&nondes.get(i)!==dom.find("input").get(0)) { //排除当前的
                isSeletAll=false;

                break;
            }
        }
        if(isSeletAll) {    //其他都是选中状态
            if (dom.find("input").get(0).checked) { //如果当前的是选中装态。
                isSeletAll = false;
            }
        }

        if(isSeletAll){
          var allCheckBox=dom.parents(".datagrid-body").siblings(".datagrid-header").find(".datagrid-header-check").addClass("on")
            allCheckBox.find("input").get(0).checked=true;
        } else{
            var allCheckBox=dom.parents(".datagrid-body").siblings(".datagrid-header").find(".datagrid-header-check").removeClass("on")
            allCheckBox.find("input").get(0).checked=true;
        }



        return false;
    });
});

//{"order_no":"","vip_no":"","sales_unit_id":"","order_date_begin":"","order_date_end":"","data_from_id":null,"DeliveryId":null,"ModifyTime_begin":"","ModifyTime_end":""}
/* 未处理订单统计接口
function GetUnAuditCount() {
    Ext.Ajax.request({
        method: 'GET',
        //sync: false,
        async: true,
        url: '/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?method=GetPosOrderUnAuditTotalCount',
        params: {
            form: ""
            , sales_unit_id: ""
            , Field7: '100'
            , Count: UnAuditCount
        },
        //timeout: 2400000, //2400000
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                $("#unAuditCoutID").html("-");
            } else {
                $("#unAuditCoutID").html(d.data);
                UnAuditCount = d.data;
            }
            //GetUnAuditCount();
        },
        failure: function (result) {
            $("#unAuditCoutID").html("-");
            //GetUnAuditCount();
        }
    });
}*/
/*var UnAuditCount = 0;
GetUnAuditCount();
setInterval("GetUnAuditCount()",60000);*/

function goLoginPage() {
    location.href = "/GoSso.aspx";
}
function getUrlParam(key){
    var urlstr = window.location.href.split("?"),
        params = {};
    if (urlstr[1]) {

        var items = urlstr[1].split("&");

        for (var i = 0; i < items.length; i++) {

            var itemarr = items[i].split("=");

            params[itemarr[0]] = itemarr[1];
        }
    }
    return key?params[key]:params;

}

fnChangePwd = function () {
    location.href = "/Module/Basic/ChangePWD/ChangePWD.aspx?mid=1";
};
var  JITMethod = {
    getUrlParam: function (strname) {
        var hrefstr, pos, parastr, para, tempstr;
        hrefstr = window.location.href;
        pos = hrefstr.indexOf("?");
        parastr = hrefstr.substring(pos + 1);
        para = parastr.split("&");
        tempstr = "";
        for (i = 0; i < para.length; i++) {
            tempstr = para[i];
            pos = tempstr.indexOf("=");
            if (tempstr.substring(0, pos) == strname) {
                return tempstr.substring(pos + 1).replace("#", "");
            }
        }
        return null;
    },
    getDate : function (value) {
        if (value != null && value != "null" && value != "") {
            var one = value.indexOf("T");
            if (one > 0) {
                value = value.substring(0, one);
            } else {
                var num = value.indexOf(' ');
                if (num > 0) {
                    value = value.substring(0, num);
                }
            }
            return value;
        }
    },
    getDateTime: function (value) {
        if (value != null && value != "null" && value != "") {
            value = Ext.Date.parse(value, 'c');
            return Ext.Date.format(value, 'Y-m-d H:i:s');
        }
    },
    getDateTimeNotSS: function (value) {
        if (value != null && value != "null" && value != "") {
            value = Ext.Date.parse(value, 'c');
            return Ext.Date.format(value, 'Y-m-d H:i');
        }
    },
    getMonthDayHourMinute: function (value) {
        if (value != null && value != "null" && value != "") {
            value = Ext.Date.parse(value, 'c');
            return Ext.Date.format(value, 'm-d H:i');
        }
    },
//@basicPath clientid clientuserid picjson
    getPictures: function (basicPath, clientid, clientuserid, picJSON) {
        //alert(picJSON);
        //        var jdata = eval("[" + picJSON + "]");
        if (picJSON != null && picJSON != '') {
            try {
                var photoDatas = eval(picJSON);
                if (!(photoDatas.length != null && photoDatas.length > 0)) {
                    photoDatas = eval("[" + picJSON + "]");
                }

                var photoUrls = new Array();
                for (var i = 0; i < photoDatas.length; i++) {
                    var url = '';
                    var photoData = photoDatas[i];
                    if (photoData.FileName != null && photoData.FileName != '') {
                        url = basicPath + clientid + "/" + clientuserid + "/" + photoData.FileName;
                    } else {
                        url = "/Framework/Image/graphics/no_picture.jpg";
                    }
                    photoUrls.push(url);
                }
            } catch (e) {

            }
        }
        return photoUrls;
        //        var photoDatas = new Array();
        //        var photoUrls = '';
        //        if (photoDatas != null) {
        //            photoUrl = "http://report.jitmarketing.cn:9021/" + clientid + "/" + clientuserid + "/" + photoDatas.FileName;
        //        } else {
        //            photoUrl = "/Framework/Image/graphics/no_picture.jpg";
        //        }

        //        //var jdata = eval(picJSON);
        //        //        var pics = new Array();
        //        for (var i = 0; i < jdata.length; i++) {
        //            if (jdata[i] != null && jdata[i].FileName != null) {
        //                //                var da = jdata[i].Date.split(" ")[0].split('-');
        //                //                da = da[0].toString() + da[1].toString() + da[2].toString();
        //                pics.push("http://report.jitmarketing.cn:9021/" + clientid + "/" + clientuserid + jdata[i].FileName);
        //            }
        //            else {
        //                pics.push("/Framework/Image/graphics/no_picture.jpg");
        //            }
        //            //pics.push("http://www.jitmarketing.cn/mobile/picture/29848_1359344147030.jpg");
        //        }
        //        return photoUrl;
    },
    getDecimalValue: function (val) {
        if (val == null || val == "") {
            return "0.00";
        }
        var a = parseFloat(val).toFixed(2).toString();
        var fuhao = a.indexOf("-")
        if (fuhao > -1) {
            a = a.substring(1);
        }
        var b = "";
        if (a.length > 3) {
            var sum = a.lastIndexOf('.');
            if (sum < 1) {
                sum = a.length;
            }
            var c = a.substring(sum);
            var k = sum % 3;
            var j = parseInt(sum / 3);
            b = b + a.substring(0, k);
            for (var i = 0; i < j; i++) {
                var one = (k + 3 * i);
                var two = k + 3 * (i + 1);
                var d = ",";
                b = b + d + a.substring(one, two);
            }
            b = b + c;
        }
        else { b = a; }
        if (k == 0) {
            b = b.substring(1);
        }
        if (fuhao > -1) {
            b = "-" + b;
        }
        return b;
    },
    getIntValue: function (val) {
        if (val == null || val == "") {
            return "0";
        }
        var a = val.toString();
        var fuhao = a.indexOf("-")
        if (fuhao > -1) {
            a = a.substring(1);
        }
        var b = "";
        if (a.length > 3) {
            var sum = a.length;
            var c = a.substring(sum);
            var k = sum % 3;
            var j = parseInt(sum / 3);
            b = b + a.substring(0, k);
            for (var i = 0; i < j; i++) {
                var one = (k + 3 * i);
                var two = k + 3 * (i + 1);
                var d = ",";

                b = b + d + a.substring(one, two);
            }
            b = b + c;
        }
        else { b = a; }
        if (k == 0) {
            b = b.substring(1);
        }
        if (fuhao > -1) {
            b = "-" + b;
        }
        return b;
    },
    getBackValue: function (val) {
        if (val != null && val != "") {
            val = val.toString().replace(/,/g, "");
            return val;
        }
        return "0";
    }
}