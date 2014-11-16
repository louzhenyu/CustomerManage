Jit.AM.defindPage({

    name: 'CourseHub',
    onPageLoad: function() {
        
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.loadPageData();
        this.initPageEvent();
    }, 
    initPageEvent:function(){
       
        $("#section").delegate(".courseArea","tap",function(){
            var $this = $(this),typeId = $this.data("id");
            if(typeId==5||typeId==6||typeId==7||typeId==8){
                $this.addClass("going");
                setTimeout(function(){
                    Jit.AM.toPage("CourseDetail","&typeId="+typeId);
                },500);
            }else{
                //self.alert("typeId不合法！");
            }
        });
    },
    //加载数据
    loadPageData:function(){
        
    }

});

