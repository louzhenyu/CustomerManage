/**
 * Created by Administrator on 2016/4/20.
 */

define(function() {
    var salesPromotion = {
        eventType:"1",
        skillPanel: $('[ data-interaction="2"] .skillPanel'),
        init: function (data) {//团购 秒杀  热销的基础数据绘制。

        },
        initEvent: function () {
            debugger;
            this.skillPanel.delegate(".eventList .editIconBtn", "click", function () {


            });

        }

    };
    return salesPromotion;
});