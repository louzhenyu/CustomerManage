Jit.AM.defindPage({

    name: 'CourseList',
    elements: {
        broExplain: '',
        broMore: '',
        btInEmba: ''
    },
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.loadPageData();
        this.initPageEvent();
    },
    initPageEvent: function() {
        var self = this;



        // new iScroll('wrapper', { hScrollbar: false, vScrollbar: false });	

        $("#section").delegate(".courseArea", "tap", function() {
            var $this = $(this),
                typeId = $this.data("id");
            // if(typeId==1||typeId==2||typeId==3)
            if (typeId) {
                $this.addClass("going");
                setTimeout(function() {
                    $this.removeClass("going");
                    Jit.AM.toPage("CourseDetail", "&typeId=" + typeId);
                }, 500);
            }
            //跳转到指定列表页
            //       else if(typeId==4){
            // 	$this.addClass("going");
            // 	setTimeout(function(){
            // 		$this.removeClass("going");
            // 		Jit.AM.toPage("CourseHub","&typeId="+typeId);
            // 	},500);

            // }
            else

            {
                self.alert("typeId不合法！");
            }
        });


        $("#footer").delegate(".js_back", "tap", function() {
            Jit.AM.pageBack();
        }).delegate(".js_home", "tap", function() {
            //Jit.AM.toPage();
        }).delegate(".js_aboutme", "tap", function() {
            //Jit.AM.toPage();
        }).delegate(".js_more", "tap", function() {
            //Jit.AM.toPage();
        });
    },
    //加载数据
    loadPageData: function() {

    },
    alert: function(text, callback) {
        Jit.UI.Dialog({
            type: "Alert",
            content: text,
            CallBackOk: function() {
                Jit.UI.Dialog("CLOSE");
                if (callback) {
                    callback();
                }
            }
        });
    }

});