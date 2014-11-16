Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
    },
    //登录接口
    initPage:function(){
        var str=location.href;
        var vipName=str.substring(str.indexOf("vipName")+8,str.indexOf("&code"));
            vipName=decodeURIComponent(vipName);
            $("#vipName").html(vipName);
        var code=str.substring(str.indexOf("code")+5);
            $("#toReg").attr("href","javascript:Jit.AM.toPage('Perfect','&code="+code+"');");
        var baseInfo=this.getBaseInfo();
        if(baseInfo.userId){
            Jit.AM.toPage("Perfect");
        }
    }
});