// JavaScript Document

$(function(){
	/*var getWindowHeight = $(window).height()-42;	
	$("#odpage1").css("height",$("#aodiL").height()+30);
	//$("#scroller").css("height",getWindowHeight*$(".common").length);
	if(IsPC()){
		$(".common,.Menu,#wrapper,#wrapper li").css("width",320).show();	
		$(".common").addClass("oeir2");
	}else{
		$("#Common,.Menu").css("width","100%").show();	
	}*/
})
function IsPC() {
    var userAgentInfo = navigator.userAgent;

    var Agents = new Array("Android", "iPhone", "SymbianOS", "Windows Phone", "iPad", "iPod");
    var flag = true;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
        }
    }
    return flag;
}
function Contact(){
	var userName = $("#userName").val(),
		company = $("#company").val(),
		tel = $("#tel").val(),
		email = $("#email").val(),
		phone = $("#phone").val(),
		industry = $("#industry").val();
		if(phone ==""){
			alert("请输入手机号码");
			return false; 	
		}
	 var jsonarr = { 'action': "setContact", ReqContent: JSON.stringify({ "common":{}, "special": {
		 "userName":userName,
		 "company":company,
		 "tel": tel,
		 "email":email,
		 phone:phone,
		 industry:industry
		 } }) };
        $.ajax({
            type: 'post',
            url: "/OnlineShopping/data/Data.aspx",
            data: jsonarr,
            timeout: 90000,
            cache: false,
            beforeSend: function () {
            	
            },
            dataType: 'json',
            success: function (o) {
               

                  alert(o.description);
                
            }
        })	
		return false; 
}