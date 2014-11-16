Jit.AM.defindPage({
    name: 'Map',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入Map.....');
        this.initEvent();
    },
    elements: { goto: '', conmenu: '' },
    Dialog: function (cfg) {
        if (cfg == 'CLOSE') {
            var panel = $('.jit-ui-panel');
            if (panel) {
                (panel.parent()).remove();
            }
        } else {
            cfg.LabelOk = cfg.LabelOk ? cfg.LabelOk : '确定';
            cfg.LabelCancel = cfg.LabelOk ? cfg.LabelCancel : '取消';
            var panel, btnstr;
            if (cfg.type == 'Alert' || cfg.type == 'Confirm') {
                btnstr = (cfg.type == 'Alert') ? '<a id="jit_btn_ok" style="margin:0 auto">' + cfg.LabelOk + '</a>' : '<a id="jit_btn_cancel">' + cfg.LabelCancel + '</a><a id="jit_btn_ok">' + cfg.LabelOk + '</a>';
                panel = $('<div"><div class="jit-ui-panel"></div><div name="jitdialog" style="margin-top:120px" class="popup br-5">'
                          + '<p class="ac f14 white">' + cfg.content + '</p><div class="popup_btn">'
                          + btnstr + '</div></div></div>');
            } else if (cfg.type == 'Dialog') {
                panel = $('<div><div class="jit-ui-panel"></div><div style="margin-top:120px" class="popup br-5"><p class="ac f14 white">' + cfg.content + '</p></div></div>');
            }
            panel.css({
                'position': 'fixed',
                'left': '0',
                'right': '0',
                'top': '0',
                'bottom': '0',
                'z-index': '99'
            });
            panel.appendTo($('body'));
            (function (panel, cfg) {
                setTimeout(function () {
                    if (cfg.CallBackOk) {
                        $(panel.find('#jit_btn_ok')).bind('click', cfg.CallBackOk);
                    }
                    if (cfg.CallBackCancel) {
                        $(panel.find('#jit_btn_cancel')).bind('click', cfg.CallBackCancel);
                    } else {
                        $(panel.find('#jit_btn_cancel')).bind('click', function () { Jit.UI.Dialog('CLOSE'); });
                    }
                }, 16);
            })(panel, cfg);
        }
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        $('#mapArea').attr('latlng', Jit.AM.getUrlParam('lat') + ',' + Jit.AM.getUrlParam('lng'));

        me.initMap();
    },
    initMap: function () {
        var me = this;
		
        $(window).resize(function () {
            $('#mapArea').css('top', '3.0em');
        });
		
        if ($("#mapArea").attr('latlng')) {
            var latlng, lat, lng;

            latlng = $("#mapArea").attr('latlng').split(',');
            lng = parseFloat(latlng[0], 10);
            lat = parseFloat(latlng[1], 10);

            var name = decodeURI(me.getUrlParam("store"));
            var addr = decodeURI(me.getUrlParam("addr"));

            //实例化地图区域
            var map = new BMap.Map("mapArea");
            var point = new BMap.Point(lat, lng);
            map.centerAndZoom(point, 15);
            //自定义地图图标显示
            var myIcon = new BMap.Icon("../../../images/public/hotel_default/mapIcon.png", new BMap.Size(36, 45));
            map.addControl(new BMap.NavigationControl());
            map.addControl(new BMap.ScaleControl());

            //创建标注  
            var marker = new BMap.Marker(point, { icon: myIcon });
            var infoWindow = new BMap.InfoWindow("地址: " + addr, { title: name });
            marker.addEventListener("click", function (e) {
                this.openInfoWindow(infoWindow);
            });

            map.addOverlay(marker);
            map.openInfoWindow(infoWindow, point);

            $(window).on('resize', function () {
                map.centerAndZoom(point, 15);
            });

            me.elements.goto = $('#goto');
            me.elements.conmenu = $('#conmenu');

            me.elements.conmenu.show();
            me.elements.goto.bind('click', function () {//导航事件
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(function (datas) {
                        var positions = {
                            lng: 0,
                            lat: 0
                        };
                        positions.lng = datas.coords.longitude;
                        positions.lat = datas.coords.latitude;
                        var drivingRoute = new BMap.DrivingRoute(map, {
                            renderOptions: {
                                map: map,
                                autoViewport: true
                            }
                        });
                        var clientPoint = new BMap.Point(positions.lng, positions.lat);
                        drivingRoute.search(clientPoint, point);
                        map.closeInfoWindow(); //关闭打开的窗口信息
                        map.removeOverlay(marker); //关闭标注信息
                    }, function (error) {
                        switch (error.code) {
                            case error.TIMEOUT:
                                me.Dialog({
                                    'content': '连接超时，请重试！',
                                    'type': 'Alert',
                                    'CallBackOk': function () {
                                        me.Dialog('CLOSE');
                                    }
                                });
                                break;
                            case error.PERMISSION_DENIED:
                                me.Dialog({
                                    'content': '您拒绝了使用位置共享服务，查询已取消！',
                                    'type': 'Alert',
                                    'CallBackOk': function () {
                                        me.Dialog('CLOSE');
                                    }
                                });
                                break;
                            case error.POSITION_UNAVAILABLE:
                                me.Dialog({
                                    'content': '亲爱的用户，非常抱歉，我们暂时无法为您所在的区域提供位置服务！',
                                    'type': 'Alert',
                                    'CallBackOk': function () {
                                        me.Dialog('CLOSE');
                                    }
                                });
                                break;
                            case error.UNKNOWN_ERROR:
                                me.Dialog({
                                    'content': '一个未知的错误发生！',
                                    'type': 'Alert',
                                    'CallBackOk': function () {
                                        me.Dialog('CLOSE');
                                    }
                                });
                                break;
                        }
                    }, { enableHighAcuracy: true, timeout: 3000, maximumAge: 2000 });
                } else {
                    me.Dialog({
                        'content': '您的设备不支持定位功能！',
                        'type': 'Alert',
                        'CallBackOk': function () {
                            me.Dialog('CLOSE');
                        }
                    });
                    return false;
                }
            });
        }
    }
});