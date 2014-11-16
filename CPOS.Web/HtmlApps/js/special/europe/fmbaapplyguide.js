Jit.AM.defindPage({

    name: 'FmbaApplyGuide',
    elements: {},
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
        self.elements.btToSign = $('#toSign');


    },
    initEvent: function() {
        var self = this;
        ActivityBox.init({
            'EventID': '9842fca7bce7615de0a355380c5a51ae',
            callback: function(data, controList) {
                if (data.code == "200") {
                    ActivityBox.tips("报名成功");
                    ActivityBox.hide();
                } else {
                    ActivityBox.tips(data.description);
                }
            }
        });

        self.elements.btToSign.bind(self.eventType, function() {
            ActivityBox.show();
        });
    }

});