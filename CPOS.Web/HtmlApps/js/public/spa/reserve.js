/*定义页面*/
Jit.AM.defindPage({
	name : 'Reserve',
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入Reserve.....');
		this.initEvent();
	},

	initEvent : function() {
		var me = this;

		//定义页面尺寸
		me.windowHeight = window.innerHeight;
		me.windowWidth = window.innerWidth;

		//获取省份+城市组合名称
		me.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : {
				'action' : 'getProvinceCityName'
			},
			success : function(data) {
				if (data.code == 200) {
					me.loadPageData(data.content);
				} else {
					Jit.UI.Dialog({
						'content' : '获取城市列表有误',
						'type' : 'Alert',
						'CallBackOk' : function() {
							Jit.UI.Dialog('CLOSE');
						}
					});
					return false;
				}
			}
		});

		//获取日历中的预约时间
		var InDate = me.getParams("InDate");
		if (InDate) {
			//$("#ddTime").text(InDate.replace(/-/g, "/"));
			$("#selDate").html("").append("<option selected='selected'>"+InDate+"</option>");
		} else {

			//设置默认日期
			var currentDate = new Date();
			var nextDate = new Date();
			nextDate.setDate(new Date().getDate() + 1);

			//设置默认开始日期
			var year = currentDate.getFullYear(), month = currentDate.getMonth() + 1, data = currentDate.getDate();
			month = (month < 10) ? ("0" + month.toString()) : month;
			me.setParams("InDate", year + "/" + month + "/" + data);
			
		}
	},
	loadPageData : function(data) {
		var itemlists = data.cityList;
		//获取省份 城市 列表
		if (itemlists != null && itemlists.length > 0) {
			for (var i = 0; i < itemlists.length; i++) {
				if (itemlists[i].Province == itemlists[i].CityName) {
					$("#selCity").append("<option value='" + itemlists[i].CityName + "'>" + itemlists[i].Province + "</option>");
				} else {
					var province=itemlists[i].Province;
					$("#selCity").append("<option value='" + itemlists[i].CityName + "'>" + ((province?province:"")+ itemlists[i].CityName) + "</option>");
				}
			}
		}

		if (this.getParams("selCity") != "" && this.getParams("selCity") != null) {
			$("#selCity").val(this.getParams("selCity"));
			//默认城市选中
			JitPage.fnSelChange();
		}
	},
	fnSubmit : function() {
		var me = this;
		var city = $("#selCity").val();
		var store = $("#selStore").val();
		var InDate = me.getParams("InDate");
			InDate=InDate.replace("/","-");
		var selTime=$("#selTime").val();
		if(selTime!="--请选择--"){
			me.setParams("appointmentTime",selTime);  //预约时间
		}
		if (store&&store!="--请选择--") {
			me.setParams("storeId",store);
			//如果已经选择店，直接跳转到店详细页
			me.toPage('GetPosition', "&storeId=" + store);
		} else {
			me.setParams("InDate",InDate);
			me.setParams("OutDate",InDate);
			me.toPage('GetPosition', '&city=' + city);
		}
	},
	fnSelChange : function() {
		var me = this;

		me.setParams("selCity", $("#selCity").val());
		//城市下拉框改变事件
		me.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : {
				action : 'getStoreListByCity',
				city : $("#selCity").val(),
				page : 1,
				pageSize : 10000
			},
			success : function(data) {

				if (data.code == 200) {
					var itemlists = data.content.storeList;
					$("#selStore").html("");
					$("#selStore").append("<option value='--请选门店--'>请选门店</option>");
					$.each(itemlists, function(i, obj) {
						if(i==0){
							$("#selStore").append("<option selected='selected' value='" + obj.storeId + "'>" + obj.storeName + "</option>");
						}else{
							$("#selStore").append("<option value='" + obj.storeId + "'>" + obj.storeName + "</option>");
						}
					});

					if (me.getParams("selStore") !=null) {
						$("#selStore").val(me.getParams("selStore"));
					}
				}
			}
		});
	},
	fnSelStoreChange : function() {
		this.setParams("selStore", $("#selStore").val());
	}
}); 