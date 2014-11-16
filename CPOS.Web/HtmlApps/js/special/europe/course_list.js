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
    initPageEvent:function(){
    	
    },
    //加载数据
    loadPageData:function(){
    	
    }

});

