//单位M
var EARTH_RADIUS = 6378137.0;
var PI = Math.PI;
Jit.AM.defindPage({
    name: 'HousingType',
    onPageLoad: function () {
        Jit.log('进入HousingType.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        /*页面异步数据请求_获取酒店详细信息*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId': me.getUrlParam('storeId')
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                Jit.UI.Masklayer.hide();
                if (data.code == 200) {
                    me.loadPicData(data.content);
                    me.loadStoreData(data.content);
                }
            }
        });

        /*页面异步数据请求_获取酒店中房间列表*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetailByStoreID',
                'storeId': me.getUrlParam('storeId'),
                'BeginDate': me.getParams('InDate'),
                'EndDate': me.getParams('OutDate'),
                'Page': 1,
                'PageSize': 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    me.loadPageData(data.content);
                }
            }
        });
    },
    loadPicData: function (data) {
    	var me=this;
        $('.describe').html(data.storeName + '<a style="color:white;" href=javascript:JitPage.toPage("H_IntroduceDetail","storeId='+me.getUrlParam('storeId')+'");>详情>></a>');
        var imglists = data.imageList;
        if (imglists && imglists.length > 0) {
            var imgHtml = barHtml = "";
            for (var i = 0; i < imglists.length; i++) {
                imgHtml += "<li><img src=\"" + imglists[i].imageURL + "\" ></li>";
                barHtml += "<em class=\"" + (i == 0 ? "on" : "") + "\"></em>";
            };
            $('#goodsImgs').html(imgHtml);
            if (imglists.length >= 1) {
                $('#goodsImgBar').html(barHtml);
                loaded();
            };
        }
        var myScroll;
        //拖拽事件
        function loaded() {
            var menuList = $('#goodsImgBar em'),
                goodsScroll = $('#goodsScroll');

            //重新設置大小
            ReSize();

            function ReSize() {
                goodsScroll.find('#goodsImgs').css({
                    width: goodsScroll.width() * goodsScroll.find('li').size()
                });
                goodsScroll.find('li').css({
                    width: goodsScroll.width()
                });
            }
			setTimeout(function(){
				
				//綁定滾動事件
            myScroll = new iScroll('goodsScroll', {
                snap: true,
                momentum: false,
                vScroll: false,
                hScrollbar: false,
                vScrollbar: false,
                onScrollEnd: function () {
                    if (this.currPageX > (menuList.size() - 1)) {
                        return false;
                    };
                    menuList.removeClass('on');
                    menuList.eq(this.currPageX).addClass('on');
                }
            });
            $("#goodsImgBar").delegate("em",'click', function () {
                myScroll.scrollToPage(menuList.index(this));
            });

            $(window).resize(function () {
                ReSize();
                myScroll.refresh();
            });
			},1000);
            
        }
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
                $("#imgs").attr("href", "javascript:Jit.AM.toPage('Photo','&storeId=" + me.getUrlParam('storeId') + "');");
            } else {
                $(".imgBox img").attr("src", "../../../images/public/hotel_default/noImg.png");
                $("#imgs").attr("href", "javascript:;");
            }
        }
        if (data.imageList.length > 0) {
            $("#imgs").attr("href", "javascript:Jit.AM.toPage('Photo','&storeId=" + me.getUrlParam('storeId') + "');");
            $('[jitval=imgList]').html(data.imageList.length);
        } else {
            $(".imgBox p").hide();
            $('[jitval=imgList]').html("");
            $("#imgs").attr("href", "javascript:;");
        }
        $("[jitval=InDate]").html("<b>入住:</b>" + me.getParams('InDate'));
        $("[jitval=OutDate]").html("<b>离店:</b>" + me.getParams('OutDate'));

        /*日期比较*/
        var dateVal = "";
        var date = new Date();
        var pToDay = me.getParams('InDate').replace('/', '-');
        var pCallDate = me.getParams('OutDate').replace('/', '-');
        if (pToDay != null && pCallDate != null && pToDay != "" && pCallDate != "") {
            dateVal = me.daysBetween(pToDay, pCallDate);
        }
        $("[jitval=Date]").html("<b>" + dateVal.toString().replace('-', '') + "晚</b>");

        //计算距离
        if (data.latitude != null && data.latitude != "" && data.longitude != null && data.longitude != "") {
            var p = new Object();
            p.Lat = data.latitude;
            p.Lng = data.longitude;
            p.Lat2 = me.getUrlParam("lat");
            p.Lng2 = me.getUrlParam("lng");

            var Distance;
            if (parseInt(me.getUrlParam("lat")) == 0 || parseInt(me.getUrlParam("lng")) == 0) {
                Distance = "";
            } else {
                Distance = this.getFlatternDistance(p.Lat, p.Lng, p.Lat2, p.Lng2);
            }
            if (Distance != "" && Distance != null && p.Lat != 0) {
                $('[jitval=distance]').html("<i></i>距您" + (Distance / 1000).toFixed(2) + "公里");
            } else {
                $('[jitval=distance]').html('<i></i>未知距离');
            }
            if (p.Lat != 0 && p.Lat != "" && p.Lat != null) {
                $("#mapUrl").attr("href", "../../../html/special/bollssom/map.html?customerId=" + Jit.appManage.CUSTOMER_ID + "&lat=" + data.latitude + "&lng=" + data.longitude + "&addr=" + data.address + "&store=" + data.storeName + "");
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
            var isfull = itemlists[i].IsFull;
            var price = itemlists[i].Price;
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
            if (itemlists[i].VipLevelName == null || itemlists[i].VipLevelName == "")
            {
                itemlists[i].VipLevelName = "微信";
            }
            //itemlist[i].VipLevelName = itemlist[i].VipLevelName || "微信";
            
            var xhtml = Mustache.render(tpl, itemlists[i]);
            if (isfull == 1) {
                xhtml = xhtml.replace('<li>', '<li class="fullStatus">');
            }
            if (price == null || price == 0 || price == "") {
                xhtml = xhtml.replace('市场价元/晚', '市场价0元/晚');
            }
            html += xhtml;

        }
        /*页面数据渲染*/
        $('[tplpanel=house_list]').html(html);
    },
    urlGoTo: function () {
        var me = this;
        me.toPage('H_HouseDetail', '&storeId=' + me.getUrlParam('storeId') + '&city=' + me.getUrlParam('city') + '&lat=' + me.getUrlParam('lat') + '&lng=' + me.getUrlParam('lng'));
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
        Jit.AM.toPage('H_HouseDetail', '&storeId=' + me.getUrlParam('storeId') + '&itemId=' + itemId + '&city=' + me.getUrlParam('city') + '&lat=' + me.getUrlParam('lat') + '&lng=' + me.getUrlParam('lng'));
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
    },
    urlGoToStore: function (url) {
        var me = this;
        me.toPage(url, '&city=' + me.getUrlParam('city') + '&lat=' + me.getUrlParam('lat') + '&lng=' + me.getUrlParam('lng'));
    }
});