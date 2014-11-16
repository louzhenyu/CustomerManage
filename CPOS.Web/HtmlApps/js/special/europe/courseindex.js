Jit.AM.defindPage({

    name: 'CourseIndex',
    elements: {},
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;

                //设置微信分享
        WeiXinShare.title = "中欧EMBA课程";
        // WeiXinShare.imageUrl ="";
        WeiXinShare.desc ="融世界之博大，合本土之精深。2015级EMBA春季班招生中。";
    },
    initEvent: function() {
  
    }

});






// Jit.AM.defindPage({
//     name: 'MBAContact',
//     elements: {},
//     onPageLoad: function () {
//         //当页面加载完成时触发
//         Jit.log('页面进入' + this.name);
//         this.loadPageData();
//         this.initPageEvent();
//     },
//     initPageEvent: function () {
//         var self = this;
//         self.submitApply();
//     },
//     //加载数据
//     loadPageData: function () {
//         var self = this;
//         //姓名输入框
//         self.elements.name = $('#name');
//         //手机输入框
//         self.elements.cellphone = $('#cellphone');
//         //邮箱输入框
//         self.elements.email = $('#email');
//         //工作年限输入框
//         self.elements.year = $('#year');
//         //提交按钮
//         self.elements.submit_btn=$('#submit_btn');
//     },
//     //提交报名
//     submitApply: function () {
//         var self=this;
//         self.elements.submit_btn.bind(self.eventType, function () {
//             var name_value=$.trim(self.elements.name.val());
//             var cellphone_value=$.trim(self.elements.cellphone.val());
//             var email_value=$.trim(self.elements.email.val());
//             var year_value=$.trim(self.elements.year.val());
//             if(!name_value){
//                 self.alert('请输入姓名！');
//                 return false;
//             }
//             if(!cellphone_value){
//                 self.alert('请输入手机！');
//                 return false;
//             }
//             if(!email_value){
//                 self.alert('请输入邮箱！');
//                 return false;
//             }
//             if(!year_value){
//                 self.alert('请输入工作年限！');
//                 return false;
//             }
//             if(!Jit.valid.isPhoneNumber(cellphone_value)){
//                 self.alert('手机格式不正确！');
//                 return false;
//             }
//             if(!Jit.valid.IsEmail(email_value)){
//                 self.alert('邮箱格式不正确！');
//                 return false;
//             }
//             //报名
//             self.ajax({
//                 url: '',
//                 data: {
//                     'action': ''
//                 },
//                 success: function(data) {
//                     if (data&&(data.ResultCode == 0)) {
//                         self.alert('提交成功！',function(){
//                             location.reload();
//                         });
//                     } else {
//                         self.alert(data.Message);
//                     }
//                 }
//             });
//         });
//     },
//     alert: function (text, callback) {
//         Jit.UI.Dialog({
//             type: "Alert",
//             content: text,
//             CallBackOk: function () {
//                 Jit.UI.Dialog("CLOSE");
//                 if (callback) {
//                     callback();
//                 }
//             }
//         });
//     }
// });