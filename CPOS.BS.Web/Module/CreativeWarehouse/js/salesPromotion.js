/**
 * Created by Administrator on 2016/4/20.
 */

define(function() {
    var salesPromotion = {
        eventType:"1",
        skillPanel: $('[ data-interaction="2"] .skillPanel'),
        init: function (data) {//�Ź� ��ɱ  �����Ļ������ݻ��ơ�

        },
        initEvent: function () {
            debugger;
            this.skillPanel.delegate(".eventList .editIconBtn", "click", function () {


            });

        }

    };
    return salesPromotion;
});