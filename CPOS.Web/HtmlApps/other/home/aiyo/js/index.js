define(['jquery', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        init: function () {
        	this.customerId=$.util.getUrlParam("customerId");
            this.initEvent();
        },
        initEvent:function(){
            var self=this;
            $("#section").delegate("#section a","click",function(){
                $(this).attr("href",$(this).attr("data-href")+"&customerId="+self.customerId);
            });
        }
    };

    page.init();
});