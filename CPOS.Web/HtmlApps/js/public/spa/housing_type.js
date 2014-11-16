//单位M
var EARTH_RADIUS = 6378137.0;
var PI = Math.PI;
Jit.AM.defindPage({
    name: 'HousingType',
    detail:"",   //要跳转的地址
    onPageLoad: function () {
        Jit.log('进入HousingType.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;
		var storeId=me.getParams('storeId');
		if(!storeId){
			storeId=me.getUrlParam("storeId");
		}
		this.storeId=storeId;
		$("#toGoDetail").bind("click",function(){
			Jit.AM.toPage('Introduce',me.detail);
		});
        /*页面异步数据请求_获取酒店详细信息*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId':me.storeId 
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                Jit.UI.Masklayer.hide();
                if (data.code == 200) {
                    me.loadStoreData(data.content);
                }
            }
        });
        /*页面异步数据请求_获取酒店中房间列表*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetailByStoreID',
                'storeId': me.storeId ,
                'BeginDate': me.getParams('InDate'),
                'EndDate': me.getParams('InDate'),
                'Page': 1,
                'PageSize': 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    me.loadPageData(data.content);
                } else {
                    /*Jit.UI.Dialog({
                        'content': '搜索门店信息错误',
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });*/
                    return false;
                }
            }
        });
    },
    loadStoreData: function (data) {
        var me = this;
        $('[jitval=storename]').html(data.storeName);
        $('[jitval=hotelType]').html(data.HotelType);
        $('[jitval=addr]').html(data.address);
        if (data.imageURL != null && data.imageURL != "") {
            $(".imgBox img").attr("src", data.imageURL);
        } else {
            if (data.imageList.length > 0) {
                $(".imgBox img").attr("src", data.imageList[0].imageURL);
            } else {
                $(".imgBox img").attr("src", "../../../images/public/hotel_default/noImg.png");
            }
        }
        if (data.imageList.length > 0) {
            $('[jitval=imgList]').html(data.imageList.length);
        } else {
            $(".imgBox p").hide();
            $('[jitval=imgList]').html("");
        }
        $("[jitval=InDate]").html("<b>预约日期:</b>" + me.getParams('InDate'));
        $("[jitval=OutDate]").html("<b>预约时间:</b>" + (me.getParams('appointmentTime')?me.getParams('appointmentTime'):"未写预约时间"));

        //$("[jitval=Date]").html("<b>" + dateVal.toString().replace('-', '') + "</b>");

        //计算距离
        if (data.latitude != null && data.latitude != "" && data.longitude != null && data.longitude != "") {
            var p = new Object();
            p.Lat = data.latitude;
            p.Lng = data.longitude;
            p.Lat2 = me.getUrlParam("lat");
            p.Lng2 = me.getUrlParam("lng");
            var Distance = this.getFlatternDistance(p.Lat, p.Lng, p.Lat2, p.Lng2);
            if (Distance != "" && Distance != null && p.Lat != 0 && p.Lat != "" && p.Lat != null) {
                $('[jitval=distance]').html("<i></i>距您" + (Distance / 1000).toFixed(2) + "公里");
                $("#mapUrl").attr("href", "javascript:Jit.AM.toPage('Map','&lat=" + p.Lat + "&lng=" + p.Lng + "&addr=" + data.address + "&store=" + data.storeName + "');");
            	me.detail="&lat=" + p.Lat + "&lng=" + p.Lng + "&addr=" + data.address + "&store=" + data.storeName+ "&storeId="+me.storeId;
            } else {
                $('[jitval=distance]').html('<i></i>未知距离');
            }
        } else {
            $('[jitval=distance]').html('<i></i>门店没有坐标');
        }
    },
    loadPageData: function (data) {
        var itemlists = data.StoreDetail;
        /*清空页面数据*/
        var tpl = $('#Tpl_house_list').html(), html = '';
        for (var i = 0; i < itemlists.length; i++) {
            //酒店是否满房
            if (itemlists[i].IsFull != null && itemlists[i].IsFull != "" && itemlists[i].IsFull == 1) {
                itemlists[i].ItemID = "javascript:;";
                itemlists[i].IsFull = "<img src=\"../../../images/public/hotel_default/full_room.png\" style=\"text-align: right;\">";
            } else {
                itemlists[i].IsFull = "";
                itemlists[i].ItemID = "javascript:JitPage.toHouseDetail('" + itemlists[i].ItemID + "')";
            }
            if (itemlists[i].ImageUrl == null || itemlists[i].ImageUrl == "") {
                itemlists[i].ImageUrl = "../../../images/public/hotel_default/noImg.png";
            }
            html += Mustache.render(tpl, itemlists[i]);
        }
        /*页面数据渲染*/
        $('[tplpanel=house_list]').html(html);
    },
    daysBetween: function (DateOne, DateTwo) {
        var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('-'));
        var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('-') + 1);
        var OneYear = DateOne.substring(0, DateOne.indexOf('-'));

        var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('-'));
        var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('-') + 1);
        var TwoYear = DateTwo.substring(0, DateTwo.indexOf('-'));

        var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 86400000);
        return cha;
    },
    toHouseDetail: function (itemId) {
        var me = this;
        Jit.AM.toPage('HouseDetail', '&storeId=' + me.storeId  + '&itemId=' + itemId);
    }
    , /**
     * approx distance between two points on earth ellipsoid
     * @param {Object} lat1
     * @param {Object} lng1
     * @param {Object} lat2
     * @param {Object} lng2
     */
    getFlatternDistance: function (lat1, lng1, lat2, lng2) {
        var f = this.getRad((parseFloat(lat1) + parseFloat(lat2)) / 2);
        var g = this.getRad((parseFloat(lat1) - parseFloat(lat2)) / 2);
        var l = this.getRad((parseFloat(lng1) - parseFloat(lng2)) / 2);

        var sg = Math.sin(g);
        var sl = Math.sin(l);
        var sf = Math.sin(f);

        var s, c, w, r, d, h1, h2;
        var a = EARTH_RADIUS;
        var fl = 1 / 298.257;

        sg = sg * sg;
        sl = sl * sl;
        sf = sf * sf;

        s = sg * (1 - sl) + (1 - sf) * sl;
        c = (1 - sg) * (1 - sl) + sf * sl;

        w = Math.atan(Math.sqrt(s / c));
        r = Math.sqrt(s * c) / w;
        d = 2 * w * a;
        h1 = (3 * r - 1) / 2 / c;
        h2 = (3 * r + 1) / 2 / s;

        return d * (1 + fl * (h1 * sf * (1 - sg) - h2 * (1 - sf) * sg));
    },
    getRad: function (d) {
        return d * PI / 180.0;
    }
});