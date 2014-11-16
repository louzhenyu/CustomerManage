(function(win, undefined) {
	var area = {};
	
	area.getProvince = function(callback) {
		Jit.AM.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getProvince'
            },
            success: function(data) {
            	if(data.code==200){
            		if(callback){
	                	callback(data.content);
	                }
            	}else{
            		win.alert(data.description);
            	}
            }
        });
	};
	area.getCityByProvince = function(province,callback) {
		Jit.AM.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getCityByProvince',
                'Province': province
            },
            success: function(data) {
                if(data.code==200){
            		if(callback){
	                	callback(data.content);
	                }
            	}else{
            		win.alert(data.description);
            	}
            }
        });
	};
	area.getDistrictByCity = function(city,callback) {
		Jit.AM.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getDistrictsByDistricID',
                'districtId': city
            },
            success: function(data) {
                if(data.code==200){
            		if(callback){
	                	callback(data.content);
	                }
            	}else{
            		win.alert(data.description);
            	}
            }
        });
	};
	win.alert = function(text,callback){
		Jit.UI.Dialog({
			type:"Alert",
			content:text,
			CallBackOk:function(data){
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
	};
	win.area = area;
})($);
