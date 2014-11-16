Jit.AM.defindPage({

    name: 'ApplyGuide',
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
            'EventID': '0d3e7d19e7a44fe9ff19cc28210e2681',
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