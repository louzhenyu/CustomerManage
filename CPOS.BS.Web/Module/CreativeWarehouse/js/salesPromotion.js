/**
 * Created by Administrator on 2016/4/20.
 */

define(function($) {
   var salesPromotion={
        skillPanel:$('[ data-interaction="2"] .skillPanel'),
        init:function(data) {//�Ź� ��ɱ  �����Ļ������ݻ��ơ�

        },
       initEvent:function(){
          this.skillPanel.delegate(".eventList .ed")

       }


       };
    return salesPromotion;
});