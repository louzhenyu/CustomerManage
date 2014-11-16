define(['jquery', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        init: function () {
        	var customerId=$.util.getUrlParam("customerId");
        	$("#section a").each(function(i){
        		$(this).attr("href",$(this).attr("href")+"&customerId="+customerId);
        	});
        }
    };

    page.init();
});