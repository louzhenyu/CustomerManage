Jit.AM.defindPage({

    name: 'AnniversaryaTemp',
    elements: {
        broExplain: '',
        broMore: '',
        btInEmba: ''
    },
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
     WeiXinShare.imageUrl="http://www.o2omarketing.cn:9004/HtmlApps/images/special/europe/sharelogo.jpg";


    },
    initEvent: function() {
       



    }

});

