Jit.AM.defindPage({

    name: 'GroupListGoods',
    elements: {
        groupGoodsList: ''
    },

    initWithParam: function(param) {

        $('#goodsTitleUrl').attr('href', param.toUrl);

        if (param['bannerDisplay'] == false) {

            $('#goodsTitleUrl').hide();
        }
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('进入ListGoods.....');
        this.InitPageInfo();
        this.initEvent();
    },
    InitPageInfo: function() {
        var self = this;
        self.elements.groupGoodsList = $('.grouponList');
    },

    initEvent: function() {

        var self = this,
            paramList = {
                'action':'getPanicbuyingItemList',
                'eventTypeId': 3,
                'page': 1,
                'pageSize': 99
            };

        Jit.UI.AjaxTips.Loading(true);

        self.ajax({
            url: '/Interface/data/ItemData.aspx',
            data: paramList,
            success: function(data) {
                if (data.code == 200 && data.content.itemList) {

                    Jit.UI.AjaxTips.Loading(false);

                    self.elements.groupGoodsList.html(self.GetGroupGoodsList(data.content.itemList));
                };

            }
        });
    },
    GetGroupGoodsList: function(datas) {

        var tpl = $('#tpl_item').html(), html = '';

        for (var i = 0; i < datas.length; i++) {

            var dataInfo = datas[i];

            dataInfo.overTime = dataInfo.deadlineTime;

            dataInfo.imageUrl = Jit.UI.Image.getSize(dataInfo.imageUrl,480);

            html += Mustache.render(tpl, dataInfo);
        };

        return html;
    },
    getDay: function(_unixTime) { //获取剩余天数

        var unixTime = parseInt(_unixTime) * 1000; //转换成JS时间

        var endTime = new Date(unixTime),
            seconds = 1000,
            min = 60 * seconds,
            hour = 60 * min,
            days = 24 * hour,
            now = new Date().getTime(),
            dateDiff = endTime - now,
            tipe;
        if (dateDiff <= 0)
            tipe = 1;
        else {
            tipe = parseInt(dateDiff / days);
        }
        return tipe || 1;
    }


});