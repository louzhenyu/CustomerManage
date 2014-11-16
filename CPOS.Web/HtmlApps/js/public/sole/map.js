Jit.AM.defindPage({
    name: 'Map',
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('进入Map.....');


            function test () {

                       var map = new BMap.Map("mapArea");
        map.centerAndZoom(new BMap.Point(121.441142, 31.228790), 11);
        var driving = new BMap.DrivingRoute(map, {
            renderOptions: {
                map: map,
                autoViewport: true
            }
        });
        map.addEventListener("click", function(e) {
            alert(e.point.lng + "," + e.point.lat);

        });

        var startPoint = new BMap.Point(121.303683, 31.221919),
            endPoint = new BMap.Point(121.441142, 31.228790);



        // driving.search("天安门", "百度大厦");
        driving.search(startPoint, endPoint);
            }


            setTimeout(test,200);

        // this.initEvent();


    },
    initEvent: function() {
        var me = this;


        var panamLng = Jit.AM.getUrlParam('lng'),
            paramLat = Jit.AM.getUrlParam('lat');
        // debugger;
        if (panamLng && paramLat) {
            lng = parseFloat(panamLng);
            lat = parseFloat(paramLat);
            InitMap(lng, lat);
        };

        function InitMap(lng, lat) {
            var map = new BMap.Map("mapArea"); // 创建地图实例    
            var point = new BMap.Point(lng, lat); // 创建点坐标    
            map.centerAndZoom(point, 11); // 初始化地图，设置中心点坐标和地图级别    
            // var m = new BMap.Icon("http://img1.40017.cn/touch/cn/hotel/detail/mapIcon.png", new BMap.Size(36, 45));
            //添加缩放控件  
            // map.addControl(new BMap.NavigationControl());
            // map.addControl(new BMap.ScaleControl());
            // map.addControl(new BMap.OverviewMapControl());

            //设置线路图
            var clientPositions = me.GetClientPosition();
            if (clientPositions) {
                var drivingRoute = new BMap.DrivingRoute(map, {
                    renderOptions: {
                        map: map,
                        autoViewport: true
                    }
                });
                var clientPoint = new BMap.Point(clientPositions.lng, clientPositions.lat);
                drivingRoute.search(clientPoint, point);
            };


            //具体地址poi
            // var marker = new BMap.Marker(point, {
            //     icon: m
            // }); 
            // // 创建标注    
            // map.addOverlay(marker); // 将标注添加到地图中  


            var addr = new BMap.InfoWindow("<p style='font-size:14px;'>" + decodeURI(me.getUrlParam("addr")) + "</p>");
            marker.addEventListener("click", function() {
                this.openInfoWindow(addr);
            });
            map.openInfoWindow(addr, point);



            $(window).on("resize", function() {
                map.centerAndZoom(point, 15);
            });
        }
    }, //获取客户地理位置
    GetClientPosition: function() {
        var positions = {
            lng: 121.303683,
            lat: 31.221919
        };


        return positions;
        if (typeof(navigator.geolocation) != undefined && navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function(datas) {
                positions.lng = coords.longitude;
                positions.lat = coords.latitude;
                return positions;
            }, function(error) {
                // switch (error.code) {
                //     case error.TIMEOUT:
                //         Jit.UI.Dialog({
                //             'content': '连接超时，请重试！',
                //             'type': 'Alert',
                //             'CallBackOk': function() {
                //                 Jit.UI.Dialog('CLOSE');
                //             }
                //         });
                //         break;
                //     case error.PERMISSION_DENIED:
                //         Jit.UI.Dialog({
                //             'content': '您拒绝了使用位置共享服务，查询已取消！',
                //             'type': 'Alert',
                //             'CallBackOk': function() {
                //                 Jit.UI.Dialog('CLOSE');
                //             }
                //         });
                //         break;
                //     case error.POSITION_UNAVAILABLE:
                //         Jit.UI.Dialog({
                //             'content': '亲爱的用户，非常抱歉，我们暂时无法为您所在的区域提供位置服务！',
                //             'type': 'Alert',
                //             'CallBackOk': function() {
                //                 Jit.UI.Dialog('CLOSE');
                //             }
                //         });
                //         break;
                //     case error.UNKNOWN_ERROR:
                //         Jit.UI.Dialog({
                //             'content': '一个未知的错误发生！',
                //             'type': 'Alert',
                //             'CallBackOk': function() {
                //                 Jit.UI.Dialog('CLOSE');
                //             }
                //         });
                //         break;
                // }


            });
        }

        return false;
    }


});