Jit.AM.defindPage({
    /*定义页面名称 必须和config文件中设置的属性一直*/
    name: 'HousingType',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入HousingType.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        /*页面异步数据请求_获取酒店详细信息*/
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId': me.getUrlParam('storeId')
            },
            success: function (data) {
                me.loadStoreData(data.content);
            }
        });

        /*页面异步数据请求_获取酒店中房间列表*/
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemList',
                'storeId': me.getUrlParam('storeId'),
                'beginDate': me.getUrlParam("InDate"),
                'endDate': me.getUrlParam("OutDate")
            },
            success: function (data) {
                me.loadPageData(data.content);
            }
        });
    },
    loadStoreData: function (data) {
        //debugger;
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
                $(".imgBox img").attr("src", "../images/noImg.png");
            }
        }
        if (data.imageList.length > 0) {
            $('[jitval=imgList]').html(data.imageList.length);
        } else {
            $(".imgBox p").hide();
            $('[jitval=imgList]').html("");
        }
        $("[jitval=InDate]").html("<b>入住：</b>" + me.getUrlParam('InDate'));
        $("[jitval=OutDate]").html("<b>离店：</b>" + me.getUrlParam('OutDate'));

        /*日期比较*/
        var dateVal = "";
        var date = new Date();
        var pToDay = me.getUrlParam('InDate');
        var pCallDate = me.getUrlParam('OutDate');
        if (pToDay != null && pCallDate != null && pToDay != "" && pCallDate != "") {
            dateVal = me.daysBetween(pToDay, pCallDate);
        }
        $("[jitval=Date]").html("<b>" + dateVal.toString().replace('-', '') + "晚</b>");

    },
    loadPageData: function (data) {
        var itemlists = data.itemList;
        /*清空页面数据*/
        var tpl = $('#Tpl_house_list').html(), html = '';
        for (var i = 0; i < itemlists.length; i++) {
            if (itemlists[i].imageUrl == null || itemlists[i].imageUrl == "") {
                itemlists[i].imageUrl = "../images/nopic.png";
            }
            html += Mustache.render(tpl, itemlists[i]);
        }
        /*页面数据渲染*/
        $('[tplpanel=house_list]').html(html);
    },
    urlGoTo: function () {
        var me = this;
        me.toPage('Introduce', '&storeId=' + me.getUrlParam('storeId'));
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
    }, toHouseDetail: function (itemId) {
        var me = this;
        Jit.AM.toPage('HouseDetail', '&storeId=' + me.getUrlParam('storeId') + '&itemId=' + itemId + "&InDate=" + me.getUrlParam("InDate") + "&OutDate=" + me.getUrlParam("OutDate"));
    }
});