/*定义页面*/
Jit.AM.defindPage({
    name: 'Reserve',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入Reserve.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        //alert(.getDate());


        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;
        var InDate = me.getParams("InDate");
        var OutDate = me.getParams("OutDate");
        if (InDate && OutDate) {
            $("#ddTime").text(InDate + "  " + OutDate);
        } else {
            var currentDate = new Date();
            var nextDate = new Date();
            nextDate.setDate(new Date().getDate() + 1);
            me.setParams("InDate", currentDate.getFullYear() + "-" + currentDate.getMonth() + 1 + "-" + currentDate.getDate());
            me.setParams("OutDate", nextDate.getFullYear() + "-" + nextDate.getMonth() + 1 + "-" + nextDate.getDate());

            $("#ddTime").text(me.getParams("InDate") + "   " + me.getParams("OutDate"));
        }
        //数据请求
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getCityList'
            },
            success: function (data) {
                me.loadPageData(data.content);
            }
        });
    },
    loadPageData: function (data) {

        var itemlists = data.cityList;
        //debugger;

        $.each(itemlists, function (i, obj) {
            $("#selCity").append("<option value='" + obj.city + "'>" + obj.city + "</option>");
        });
        //debugger;
        JitPage.fnSelChange(); //默认城市选中，改变终端下拉框
    },
    fnSubmit: function () {
        var me = this;
        var city = $("#selCity").val();
        var store = $("#selStore").val();
        var InDate = me.getParams("InDate");
        var OutDate = me.getParams("OutDate");
        if (store) { //如果已经选择店，直接跳转到店详细页
            me.toPage('HousingType', "&storeId=" + store + "&InDate=" + InDate + "&OutDate=" + OutDate);
        } else {
            me.toPage('StoreList', '&city=' + city + "&InDate=" + InDate + "&OutDate=" + OutDate);
        }
    },
    fnSelChange: function () {
        var me = this;
        //城市下拉框改变事件
        me.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreListByCity',
                city: $("#selCity").val(),
                page: 1,
                pageSize: 10000
            },
            success: function (data) {
                var itemlists = data.content.storeList;
                $("#selStore").html("");
                $("#selStore").append("<option value=''>请选择</option>");
                $.each(itemlists, function (i, obj) {
                    $("#selStore").append("<option value='" + obj.storeId + "'>" + obj.storeName + "</option>");
                });
            }
        });
    }
});