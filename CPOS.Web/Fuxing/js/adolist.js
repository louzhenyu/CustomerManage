Jit.AM.defindPage({
    name: 'AdoList',
    onPageLoad: function () {

       //当页面加载完成时触发
        CheckSign();

        this.initEvent();

        this.initData();
    },
    initData: function () {



    var mainPane,keyVal;
         mainPane = $('.present_list'),
        keyVal = 'val'
        ;
                var me=this;
  var datas =  { "modelId":me.getUrlParam('eventId'),'eventId':'1111'} ;

            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=GetMobileList',
                data: datas,
                success: function (data) {
                        if (data.code == '200' && data.content.mobileList) {
                var str = "";
                mainPane.empty();
                for (var i = 0; i < data.content.mobileList.length; i++) {
                    str += GetItem(data.content.mobileList[i]);
                }
                mainPane.append(str);


                //绑定点击事件
                mainPane.find('dl').bind('click', function () {
                    var self = $(this), val = self.data(keyVal);
                    location.href = val;

                });

            }
                }
            });

     
        function GetItem(mobileInfo) {
            var str = "";
            str += "<dl data-val=\"" + mobileInfo.url+ "\">";
            str += "<dt><img src=\"" + mobileInfo.imageUrl + "\" /></dt>";
            str += "<dd>";
            str += "<p>" + mobileInfo.title + "</p>";
            str += "<p>招标类别:" + mobileInfo.textType + "</p>";
            str += "<p>项目所在地:" + mobileInfo.city + "</p>";
            str += "</dd>";
            str += "</dl>";
            return str;
        }


    },
    initEvent: function () {


    }
});