//会员列表
Jit.AM.defindPage({
    options:{
        pageIndex:1,
        pageSize:10,
        pageCount:1,
        param:"",   //根据关键字获得会员列表
        isAppend:false
    },
    onPageLoad: function () {
        this.initPage();
        this.initEvent();
    },
    initEvent:function(){
        var me=this;
    	//搜索事件
        $("body").delegate("#search","tap", function() {
            //搜索的内容
            var searchText=$("#searchText").val();
            if(me.options.param!=searchText){
                me.options.pageIndex=1;
                me.options.isAppend=false;
                me.options.param =searchText ;
                $("#list").html("");
                me.getMembers();
            }
        }).delegate("#addMember","tap",function(){ //添加会员
            Jit.AM.toPage("AddMember");
        }).delegate(".inputmark","focus",function(){ //修改备注
            var $this=$(this);
            if($this.val()=="暂无备注"){
                $this.val("");
            }
        }).delegate(".inputmark","blur",function(){ //修改备注
            var $this=$(this),
                remark=$this.val();
            if(remark==""){
                $this.val("暂无备注");
            }else{
                var vipId=$this.attr("data-vipId");
                me.updateMark(vipId,remark);
            }
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
        if(this.options.pageIndex<=this.options.pageCount){
            this.getMembers();
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
    tips:function(options){
        var opt={
            'content': options.content,
            'type': options.type,
            'times':2000,
            'CallBackOk': function () {
                Jit.UI.Dialog('CLOSE');
            }
        }
        if(!options.times){
            delete opt.times;
        }
        Jit.UI.Dialog(opt);
    },
    updateMark:function(vipId,remark){   //修改备注
        var me=this;
        this.ajax({
            url:"/Project/Asus/AsusHandler.ashx",
            data:{
                "action":"UpdateUserInfo",
                "vipID":vipId,
                "remark":remark
            },
            success:function(data){
                if(data.code=="200"){
                    me.tips({content:"修改备注成功!",type:"Dialog",times:2000});
                }else{
                    me.tips({content:data.description,type:"Dialog",times:2000});
                }
            }
        });
    },
    //获取会员列表
    getMembers:function(){
        Jit.UI.AjaxTips.Loading(true);
        var me=this;
        this.ajax({
            url:"/Project/Asus/AsusHandler.ashx",
            data:{
                "action":"GetUserList",
                 "param":me.options.param,
                 "pageSize":me.options.pageSize,
                 "pageIndex":me.options.pageIndex
            },
            success:function(data){
                if(data.code=="200"){
                    console.log(data);
                    //会员列表
                    if(data.content.vipList&&data.content.vipList.length){
                        //总页数
                        me.options.pageCount=data.content.pageCount||1;
                        var html=bd.template("listTpl",data.content);
                        if(me.options.isAppend){
                            $("#list").append(html);
                        }else{
                            $("#list").html(html);
                        }
                        Jit.UI.AjaxTips.Tips({show:false});
                    }else{
                        Jit.UI.AjaxTips.Tips({show:true,tips:"暂无会员数据"});
                    }
                }else{
                    Jit.UI.AjaxTips.Tips({show:true,tips:data.description});
                }
                Jit.UI.AjaxTips.Loading(false);
            }
        });
    },
    initPage: function () {
        var baseInfo=Jit.AM.getBaseAjaxParam();
        if(!baseInfo.userId){//已经登录
            Jit.AM.toPage("Login");
        }else{
            this.getMembers();
        }
    	
    }
});