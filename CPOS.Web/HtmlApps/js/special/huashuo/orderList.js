//订单列表
Jit.AM.defindPage({
    options:{
        pageIndex:1,
        pageSize:10,
        pageCount:1,
        param:"",   //根据关键字获得会员列表
        isAppend:false,
        isLoaded:true,  //ajax是否加载完毕
    },
    onPageLoad: function () {
        this.initPage();
        this.initEvent();
    },
    initEvent:function(){
        var me=this;
    	//添加订单事件
        $("body").delegate("#addOrder","tap",function(){
            Jit.AM.toPage("AddOrder");
        });
        $(window).scroll(function() {
            if (me.reachBottom(200)) {
                me.options.isAppend=true;
                me.scrollLoad();
            }
        });
    },
     //滚动加载更多
    scrollLoad : function() {
        //加载更多
        this.options.isAppend = true;
        this.options.pageIndex++;
        if(this.options.pageIndex<=this.options.pageCount&&this.options.isLoaded){
            this.options.isLoaded=false;
            this.getOrders();
        }
    },
    //是否到底了
    reachBottom : function(height) {
        height=height||0;
         var scrollTop = $(window).scrollTop();
    　　var scrollHeight = $(document).height();
    　　var windowHeight = $(window).height();
    　　if(scrollTop + windowHeight >=(scrollHeight-height)){
             return true;
    　　}
        return false;
    },
    //获取订单列表
    getOrders:function(){
        Jit.UI.AjaxTips.Loading(true);
        var me=this;
        this.ajax({
            url:"/Project/Asus/AsusHandler.ashx",
            data:{
                "action":"GetOrderList",
                "pageSize":me.options.pageSize,
                "pageIndex":me.options.pageIndex
            },
            success:function(data){
                me.options.isLoaded=true;
                if(data.code=="200"){
                    console.log(data);
                    //会员列表
                    if(data.content.orderList&&data.content.orderList.length){
                        var html=bd.template("listTpl",data.content);
                         //总页数
                        me.options.pageCount=data.content.pageCount||1;
                        if(me.options.isAppend){
                            $("#list").append(html);
                        }else{
                            $("#list").html(html);
                        }
                        Jit.UI.AjaxTips.Tips({show:false});
                    }else{
                         Jit.UI.AjaxTips.Loading(false);
                         Jit.UI.AjaxTips.Tips({show:true});
                    }
                }else{
                    Jit.UI.AjaxTips.Tips({show:true,tips:data.description});
                }
                Jit.UI.AjaxTips.Loading(false);
            }
        });
    },
    initPage: function () {
    	this.getOrders();
    }
});