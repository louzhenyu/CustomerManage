Jit.AM.defindPage({
    name: 'MyInfoPage',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发
        Jit.log('进入MyInfoPage.....');

        this.initEvent();

        this.initData();
    },
    initData: function () {


    },
    initEvent: function () {

        //拖拽事件

        var userList = $('.search_list ul'), btSearch = $('#btSearch'), txtSearch = $('#txtSearch');

        var datas = { keyword: this.getParams('searchname'), page: '1', pageSize: '30', vipId: '' }, me = this;

        ToLoadData(datas);
        function ToLoadData(params) {
            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=GetSearchVipStaff',
                data: params,
                success: function (data) {
                    if (data.code == 200 && data.content.vipList) {
                        var str = "";
                        userList.empty();
                        for (var i = 0; i < data.content.vipList.length; i++) {
                            str += GetItem(data.content.vipList[i]);
                        }
                        userList.append(str);
                        //如果有下一页
                        if (data.isNext) {
                            userList.append("<li id=\"toNext\">下一页</li>");
                            $('#toNext').bind('click', function () {
                                datas.page = datas.page++;
                                ToLoadData(datas);
                            });

                        }


                    } else {
                        Tips({ msg: '很抱歉搜索不到您的用户.' });
                    }
                }
            });

        }
        function GetItem(userInfo) {

            var str = "";
            str += "<li data-val=\"" + userInfo.vipId + "\" onclick=\"javascript:Jit.AM.toPage('UserInfo','&vipId=" + userInfo.vipId + "')\" >";
            str += "<div class=\"imgBox\"><img src=\"" + userInfo.HeadImageUrl + "\" width=\"45\" height=\"45\"></div>";
            str += "<div class=\"infoBox\">";
            str += "<h3>" + userInfo.staffName + "</h3>";
            str += "<p>" + userInfo.staffCompany + "</p>";
            str += "<p>" + userInfo.staffPost + "</p>";
            str += "</div>";
            str += "<div class=\"clearfix\"></div></li>";

            return str;
        }


    }
});