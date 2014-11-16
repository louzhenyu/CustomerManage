// JavaScript Document
var Lzlj = {
    url: "/lj/Interface/RegisterData.aspx",
    registerURL: '/lj/register.html?source=' + window.location.pathname,
    isRegistered: "",
    checkRegister: function() {
		//if(G("islogin") == "0"){
		//	LinkAddCustomerId("login.html");
		//	return false; 	
		//}
        //S("userId", "e42bdd1883e24dc9a650d812633f8c57");
        //console.log(G("userId"));
        if (document.cookie.indexOf("IsRegistered=") == -1) {
		    this.getIsRegisterRequired(this.getIsRegistered);
		}
		else
            isRegistered = WeiXin.GetCookie("IsRegistered");
    }
    , getIsRegisterRequired: function (callback) {
        var jsonarr = {
            'action': "getIsRegisterRequired",
            ReqContent: JSON.stringify({
                "common": Base.All(),
                "special": { pageName: window.location.pathname }
            })
        };
        $.ajax({
            type: 'get',
            url: this.url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                Win.Loading();
            },
            dataType: 'json',
            success: function (o) {
                Win.Loading("CLOSE");
                if (o.code == "200") {
                    if (o.content.IsRegisterRequired == "1") {
                        if(callback)
                            callback(this.redirectRegisterPage);
                    }
                } else {
                    alert(o.description);
                }
            }
        })
    },getUrlParam:function(key) {
            
            var value = "",itemarr = [],
                urlstr = window.location.href.split("?");
            
            if (urlstr[2]) {
            
                var item = urlstr[2].split("&");
                
                for (i = 0; i < item.length; i++) {
                
                    itemarr = item[i].split("=");
                    
                    if (key == itemarr[0]) {
                    
                        value = itemarr[1];
                    }
                }
            }
            
            return value;
        }
    , getIsRegistered: function (callback) {
        //S("openId", "oUcanjlz0pGMW57Xm50-uiCqkPIc");
        //S("userId", "8e64ec412d224c46a8ad80857c26e2eb");




        var userInfo= {
            "locale":Lzlj.getUrlParam('locale'),
            "userId":Lzlj.getUrlParam('userId'),
            "openId":Lzlj.getUrlParam('openId'),
            "customerId":Lzlj.getUrlParam('customerId')
            };

            if (!userInfo.openId||!userInfo.userId||!userInfo.customerId) {
              userInfo=Base.All();
             };
             if (!userInfo.locale||userInfo.locale=='null') {
userInfo.locale=Base.All().locale;


             };


            var jsonarr = {
                'action': "getIsRegistered",
                ReqContent: JSON.stringify({
                    "common": userInfo
                })
            };

        //alert(JSON.stringify(jsonarr));
        $.ajax({
            type: 'get',
            url: Lzlj.url,
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
                Win.Loading();
            },
            dataType: 'json',
            success: function (o) {
                Win.Loading("CLOSE");
                if (o.code == "200") {
                    if (o.content.IsRegistered == "2") {
                        WeiXin.SetCookie("IsRegistered", o.content.IsRegistered);
                        Lzlj.isRegistered = "2";
                    }

                    if (callback)
                        callback();
                } else {
                    alert(o.description);
                }
            }
        })
    }
    , redirectRegisterPage: function () {
        if (Lzlj.isRegistered != "2")
            LinkAddCustomerId(Lzlj.registerURL);
    }
}

$(document).ready(function () {
    Lzlj.checkRegister();
});