Jit.AM.defindPage({
    name: 'GetPosition',
    onPageLoad: function () {
        Jit.log('进入GetPosition.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        Jit.UI.Masklayer.show();

        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;
        Jit.AM.setPageParam("getURL", window.location);
		debugger;
		
        /*获取设备经纬度*/
        me.initPosition();
    },
    initPosition: function () {
    	debugger;
        var me = this;
        try {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(me.PositionSuccess, me.PositionError, {
                    // 指示浏览器获取高精度的位置，默认为false
                    enableHighAcuracy: true,
                    // 指定获取地理位置的超时时间，默认不限时，单位为毫秒
                    timeout: 3000,
                    // 最长有效期，在重复获取地理位置时，此参数指定多久再次获取位置。
                    maximumAge: 2000
                });
            } else {
                Jit.UI.Dialog({
                    'content': '您的设备不支持定位功能！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return false;
            }
        } catch (e) {
            Jit.UI.Dialog({
                'content': e,
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
    },
    PositionSuccess: function (position) {
    	debugger;
        var coords = position.coords;
        if (Jit.AM.getUrlParam('storeId') != null && Jit.AM.getUrlParam('storeId') != "") {
            Jit.AM.toPage('HousingType', '&storeId=' + Jit.AM.getUrlParam('storeId') + '&lat=' + coords.latitude + '&lng=' + coords.longitude);
        } else {
            Jit.AM.toPage('StoreList', '&city=' + Jit.AM.getUrlParam('city') + '&lat=' + coords.latitude + '&lng=' + coords.longitude);
        }
    },
    PositionError: function (error) {
    	debugger;
        if (Jit.AM.getUrlParam('storeId') != null && Jit.AM.getUrlParam('storeId') != "") {
            Jit.AM.toPage('HousingType', 'storeId=' + Jit.AM.getUrlParam('storeId') + '&lat=0&lng=0');
            return false;
        } else {
            Jit.AM.toPage('StoreList', '&city=' + Jit.AM.getUrlParam('city') + '&lat=0&lng=0');
            return false;
        }
        switch (error.code) {
            case error.TIMEOUT:
                Jit.UI.Dialog({
                    'content': '连接超时，请重试！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                break;
            case error.PERMISSION_DENIED:
                Jit.UI.Dialog({
                    'content': '您拒绝了使用位置共享服务，查询已取消！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                break;
            case error.POSITION_UNAVAILABLE:
                Jit.UI.Dialog({
                    'content': '亲爱的用户，非常抱歉，我们暂时无法为您所在的区域提供位置服务！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                break;
            case error.UNKNOWN_ERROR:
                Jit.UI.Dialog({
                    'content': '一个未知的错误发生！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                break;
        }
    }
});