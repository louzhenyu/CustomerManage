/*定义页面*/
Jit.AM.defindPage({
    name: 'Categroy',
    ele: {
        section: $("#section"),
        categoryMainList:$("#categoryMainList")
    },
    page:{
        pageIndex:0,
        pageSize:10,
        allPage:2
    },
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('进入'+this.name);

        this.isSending = false;
        this.loadData();
        this.initEvent();
    },
    loadData: function() {
        this.loadCategoryList();
    },
    initEvent: function() {
        var self = this;
        $("#section").delegate(".mainCategory .categateMainMenu","click",function(e){
            $(this).parent().siblings().children(".commonSubList").hide();
            $(this).siblings().toggle(500);
        });

//        $(window).bind("scroll",function(){
//            if(self.reachBottom(100)){
//                if(!self.isSending){
//                    self.page.pageIndex++;
//                    self.loadCategoryList();
//                }
//            }
//        });
    },

    reachBottom:function(vars) {
        var scrollTop = 0;
        var clientHeight = 0;
        var scrollHeight = 0;
        if (document.documentElement && document.documentElement.scrollTop) {
            scrollTop = document.documentElement.scrollTop;
        } else if (document.body) {
            scrollTop = document.body.scrollTop;
        }
        if (document.body.clientHeight && document.documentElement.clientHeight) {
            clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight: document.documentElement.clientHeight;
        } else {
            clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight: document.documentElement.clientHeight;
        }
        scrollHeight = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
        if (scrollTop + clientHeight>= scrollHeight-vars) {
            return true;
        } else {
            return false;
        }
    },
    loadCategoryList: function() {
        var self = this;
        self.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            type:'get',
            data: {
                'action': 'GetItemCategoryList'
            },
            beforeSend:function(){
                Jit.UI.Loading(true);
                self.isSending = true;
            },
            success: function(data) {
                if (data && data.IsSuccess&&data.Data.GetItemCategoryList.length) {
                    var htmlList = template.render('tplListItem',{list:data.Data.GetItemCategoryList});
                    self.ele.categoryMainList.html(htmlList);
                }else{
                    self.ele.categoryMainList.html('<div style="width:320px;height:100px;text-align: center;position: absolute;left:50%;top:50%;margin:-50px 0 0 -160px;;">没有查找到分类信息!</div>');
                }
            },
            complete:function(){
                self.isSending = false;
                Jit.UI.Loading(false);
            }
        });
    },
    alert:function(text,callback){
        Jit.UI.Dialog({
            type : "Alert",
            content : text,
            CallBackOk : function() {
                Jit.UI.Dialog("CLOSE");
                if(callback){
                    callback();
                }
            }
        });
    }
});