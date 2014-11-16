Jit.AM.defindPage({
    name: 'Introduce',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('进入Introduce.....');
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;
        var paramLng = Jit.AM.getUrlParam('lng'), 
        	paramLat = Jit.AM.getUrlParam('lat'),
        	addr=Jit.AM.getUrlParam("addr"),
        	store=Jit.AM.getUrlParam("store");
        $("#toMap").attr("href", "javascript:Jit.AM.toPage('Map','&lat=" + paramLat + "&lng=" + paramLng + "&addr=" + decodeURIComponent(addr) + "&store=" + decodeURIComponent(store) + "');");
        $('[jitval=tel]').bind("click",function(){
        	var telphone=$('[jitval=tel]').html();
        	if(telphone){
        		location.href="tel:"+telphone;
        	}
        	
        });
        /*页面异步请求数据*/
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreDetail',
                'storeId': me.getUrlParam('storeId')
            },
            success: function (data) {
                me.loadStoreData(data.content);
            }
        });
    },
    loadStoreData: function (data) {
        $('[jitval=itemName]').html(data.storeName);
        $('[jitval=addr]').html(data.address);
        $('[jitval=tel]').html("Tel: "+data.tel);
        $('[jitval=fax]').html("Fax: " + (data.fax?data.fax:"暂无"));
        if (data.introduceContent != null && data.introduceContent != "") {
            $('[jitval=info]').html(data.introduceContent);
        } else {
            $('[jitval=info]').html('暂无内容');
        }
    },
    urlGoTo: function (url) {
        var me = this;
        me.toPage(url, '&storeId=' + me.getUrlParam('storeId') + "&InDate=" + me.getParams("InDate") + "&OutDate=" + me.getParams("OutDate"));
    }
});