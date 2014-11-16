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
        var searchBottom = $('#toSearch'), txtSearch = $('#txtSearch'), me = this;


        searchBottom.bind('click', function () {
            var datas = { keyword:$.trim(txtSearch.val()), page: '1', pageSize: '30', vipId: '' };
            if (!datas.keyword) {
                Tips({ msg: '请输入您要搜索的用户.' });
                return;
            }

            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=GetSearchVipStaff',
                data: datas,
                success: function (data) {
                    if (data.code == 200 && data.content.vipList) {
                        me.setParams('searchname', datas.keyword);
                        Jit.AM.toPage('UserList');

                    } else {
                        Tips({ msg: '在参会嘉宾中未查询到您输入的用户名称，请确认您输入是否有误，如需帮助，请联系现场工作人员.' });
                    }
                }
            });

        });


    }
});