Jit.AM.defindPage({
    name: 'evaluate',
    elems:{},
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initPage();
        this.initEvent();
        var param=this.pageParam;
    }, //加载数据
    //绑定事件
    initEvent: function() {
       var that=this;
      //点击星星事件
        $(".starBox").delegate("span", "click", function () {
            var $this = $(this);
            //把当前之前的所有的星都选中
            var index = $this.data("i");
            var $parent = $this.parent();
            if (index == 1 && $parent.find(".on").length == 1) {
                if ($this.hasClass("on")) {
                    $this.removeClass("on");
                } else {
                    $this.addClass("on");
                }
                return;
            }
            if (index >= 1) {
                for (var i = 1; i <= index; i++) {
                    $parent.find("[data-i='" + (i) + "']").addClass("on");
                }
            }
            //把之后的星都取消选中
            for (var j = index; j <= 5; j++) {
                $parent.find("[data-i='" + (j + 1) + "']").removeClass("on");
            }
        });
       //开始评价
       $("#commentBtn").bind(this.eventType,function(){
       		that.alert("评价成功!2秒自动关闭提示!");
       		setTimeout(function(){
       			Jit.UI.Dialog('CLOSE');
       			that.toPage("EvaluateSuccess");
       		},2000);
       });
    },
    initPage:function(){
    },
	alert:function(text,callback){
		Jit.UI.Dialog({
			'content' : text,
			'type' : 'Alert',
			'CallBackOk' : function() {
				Jit.UI.Dialog('CLOSE');
				if(callback){
					callback();
				}
			}
		});
	}
	
});
