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

        //定义页面尺寸
        me.windowHeight = window.innerHeight;
        me.windowWidth = window.innerWidth;

        //获取门店区域属性信息
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getStoreArea'
            },
            success: function (data) {
                if (data.code == 200) {
                    me.loadPageData(data.content);
                }
            }
        });
        //获取日历中的入住 离店 时间
        var InDate = me.getParams("InDate");
        var OutDate = me.getParams("OutDate");
        //设置默认日期
        var currentDate = new Date();
		//设置默认开始日期
        var defaultYear = currentDate.getFullYear(), defaultMonth = currentDate.getMonth() + 1, defaultDay = currentDate.getDate();
        defaultMonth = (defaultMonth < 10) ? ("0" + defaultMonth.toString()) : defaultMonth;
        var currentTime=defaultYear + "-" + defaultMonth + "-" + defaultDay;
        
        //设置默认开始日期
        var year = currentDate.getFullYear(), month = currentDate.getMonth() + 1, data = currentDate.getDate();
        month = (month < 10) ? ("0" + month.toString()) : month;
        data=(data < 10) ? ("0" + data.toString()) : data;
        //当天日期
        var currentDay=year + "-" + (month<10?("0"+month):month) + "-" + (data<10?("0"+data):data);
        
        if (InDate && OutDate&&(InDate.replace(/\//g,"-"))>=currentDay) {
        	var currentTime=new Date().getTime();
        	var inDateTime=new Date(InDate).getTime();
        	//则把当前的日期清除
        	if(currentTime>OutDate){
        		$("#ddTime").text("请选择入住时间");
        	}else{
            	$("#ddTime").text(InDate.replace(/-/g, "/") + " 至 " + OutDate.replace(/-/g, "/"));
            }
        } else {
            var nextDate = new Date();
            nextDate.setDate(new Date().getDate() + 1);
            me.setParams("InDate", year + "/" + month + "/" + data);
            //设置默认结束日期
            year = nextDate.getFullYear(), month = nextDate.getMonth() + 1, data = nextDate.getDate();
            month = (month < 10) ? ("0" + month.toString()) : month;
            me.setParams("OutDate", year + "/" + month + "/" + data);

            $("#ddTime").text(me.getParams("InDate") + " 至 " + me.getParams("OutDate"));
        }
    },
    loadPageData: function (data) {
        var itemlists = data.cityList;
        //获取省份 城市 列表
        if (itemlists != null && itemlists.length > 0) {
            $("#selCity").html("");
            $("#selCity").append("<option value=''>--请选择--</option>");
            for (var i = 0; i < itemlists.length; i++) {
                if (itemlists[i].Province == itemlists[i].CityName) {
                    $("#selCity").append("<option value='" + itemlists[i].CityName + "'>" + itemlists[i].CityName + "</option>");
                } else {
                    if (itemlists[i].CityName != "") {
                        $("#selCity").append("<option value='" + itemlists[i].CityName + "'>" + itemlists[i].CityName + "</option>");
                    }
                }
            }
        }

        if (this.getParams("selCity") != "" && this.getParams("selCity") != null) {
            $("#selCity").val(this.getParams("selCity"));
            //默认城市选中
            JitPage.fnSelChange();
        }
    },
    fnSubmit: function () {
        var me = this;
        var city = $("#selCity").val();
        if (city == "") {
            Jit.UI.Dialog({
                'content': "请选择入住区域",
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;
        }
        var store = $("#selStore").val();
        var InDate = me.getParams("InDate");
        var OutDate = me.getParams("OutDate");
        if (store) {
            //如果已经选择店，直接跳转到店详细页
            me.toPage('H_GetPosition', "&storeId=" + store + '&city=' + city);
        } else {
            me.toPage('H_GetPosition', '&city=' + city);
        }
    },
    fnSelChange: function () {
        var me = this;
        me.setParams("selCity", $("#selCity").val());
        //城市下拉框改变事件
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                action: 'getStoreListByArea',
                city: $("#selCity").val(),
                page: 1,
                pageSize: 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    var itemlists = data.content.storeList;
                    $("#selStore").html("");
                    $("#selStore").append("<option value=''>--全部--</option>");
                    $.each(itemlists, function (i, obj) {
                        $("#selStore").append("<option value='" + obj.storeId + "'>" + obj.storeName + "</option>");
                    });

                    if (me.getParams("selStore") != "") {
                        $("#selStore").val(me.getParams("selStore"));
                    }
                }
            }
        });
    },
    fnSelStoreChange: function () {
        var me = this;
        me.setParams("selStore", $("#selStore").val());
    }
});