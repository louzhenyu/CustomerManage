Jit.AM.defindPage({
    name: 'Map',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入Map.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        var panamLng = Jit.AM.getUrlParam('lng'), paramLat = Jit.AM.getUrlParam('lat');
        $('[jitval=storeName]').html(decodeURI(me.getUrlParam("store")));
        if (panamLng && paramLat) {
            lng = parseFloat(panamLng);
            lat = parseFloat(paramLat);
            InitMap(lng, lat);
        };
        function InitMap(lng, lat) {
            //创建地图实例
            var map = new BMap.Map("mapArea");
            //创建点坐标
            var point = new BMap.Point(lng, lat);
            //初始化地图，设置中心点坐标和地图级别
            map.centerAndZoom(point, 15);
            var m = new BMap.Icon("../../../images/public/hotel_default/mapIcon.png", new BMap.Size(36, 45));
            //添加缩放控件  
            map.addControl(new BMap.NavigationControl());
            map.addControl(new BMap.ScaleControl());
            //创建标注
            var marker = new BMap.Marker(point, {
                icon: m
            });
            //将标注添加到地图中
            map.addOverlay(marker);

            //显示地址
            var addr = new BMap.InfoWindow("<p style='white-space:normal !important;font-size:14px;'>" + decodeURI(me.getUrlParam("addr")) + "</p>");
            marker.addEventListener("click", function () {
                this.openInfoWindow(addr);
            });
            map.openInfoWindow(addr, point);
            $(window).on("resize", function () {
                map.centerAndZoom(point, 15);
            });
        }
    }
});