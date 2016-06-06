define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','highcharts','kindeditor'], function ($) {

    var page = {
        elems: {
            sectionPage: $("#section"),
            uiMask: $("#ui-mask"),
            click: true
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
            this.chartsData();
        },
        //加载页面数据请求。判断页面是否有数据
        loadPageData: function (e) {
            var that = this;
        },
        //加载charts
        chartsData: function () {
            var that = this;
        },
        //加载商品列表
        initEvent: function () {
            var that = this;
            //点击切换查看5天数据
            that.elems.sectionPage.delegate(".checkDay", "click", function (e) {
                //判断查看的天数
                //判断查看的对象
                //调用设置函数
                that.getSetOffDate(dateNum);
            })
        },
        getSuperRetailData: function (num,obj) {
        }
    };
    page.init();
});

