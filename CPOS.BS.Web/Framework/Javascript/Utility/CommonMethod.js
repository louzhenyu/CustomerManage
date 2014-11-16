//从URL获取参数
var JITMethod = {
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
//JIT.JS.Method.ajax = function (args) {
//    Ext.Ajax.request({
//        url: args.url,
//        params: args.params,
//        method: 'post',
//        success: function (response) {
//            args.success(response);
//        },
//        failure: function () {
//            if (args.failure != undefined && args.failure != "") {
//                args.failure();
//            }
//        }
//    });
//}