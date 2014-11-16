Jit.AM.defindPage({
    name: 'UserInfo',
    onPageLoad: function () {

       //当页面加载完成时触发
        CheckSign();
        
        this.initEvent();

        this.initData();
    },
    initData: function () {
        //初次加载用户信息
        var datas = { keyword: '', page: '1', pageSize: '30', vipId: this.getUrlParam('vipId') }, me = this;
        var elements = { UserHead: $('#userhead'), UserName: $('#username'), Phone: $('#phone'), Position: $('#position'), Email: $('#email'), SayName: $('#sayname'), Company: $('#company'), School: $('#school'), Class: $('#class'), Hobby: $('#hobby'), MyValue: $('#myValue'), NeedValue: $('#needValue') };

        //加载用户信息
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=getVipDetail',
            data: datas,
            success: function (data) {
                if (data.code == 200 && data.content) {
                    SetData(data.content);
                }
            }
        });

        //设置信息
        function SetData(userInfo) {
		
			if(!userInfo.imageUrl){
			
				userInfo.imageUrl = '../images/default_face.png';
			}
            elements.UserHead.attr('src', userInfo.imageUrl);
            elements.UserName.html(userInfo.vipRealName);
            elements.Phone.html(userInfo.phone);
            elements.Position.html(userInfo.position);
            elements.Email.html(userInfo.email);
            elements.SayName.html(userInfo.staffName);
            elements.Company.html(userInfo.company);
            //elements.DcodeImageUrl.attr('src', userInfo.DCodeImageUrl);
            elements.School.html(userInfo.school);
            elements.Class.html(userInfo.className);
            elements.Hobby.html(userInfo.hobby);
            elements.MyValue.html(userInfo.myValue);
            elements.NeedValue.html(userInfo.needValue);
        }


    },
    initEvent: function () {
        var sayUser = $('#sayUser'), popPane = $('.popshade').css({ width: $(document).width(), height: $(document).height() }), sendBottom = $('#sendSay'), cancelBottm = $('#cancelPop'), txtContent = $('#sendContent'), popBox = $('.popup');
        var datas = { toVipId: this.getUrlParam('vipId'), text: txtContent.val() }, me = this;

        //显示发送窗口
        sayUser.bind('click', function () {
            popPane.show();
            popBox.show();
        });

        //发送对话信息
        sendBottom.bind('click', function () {
            datas.text = txtContent.val();

            if (!datas.text) {
                Tips({ msg: '请输入您的对话内容.' });
                return false;
            } else if (datas.text.length >= 500) {
                Tips({ msg: '您输入的对话内容过长，请限定500字符内.' });
                return false;
            }

            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?Action=SetUserMessageData',
                data: datas,
                success: function (data) {
                    if (data.code == 200) {
                        Tips({ msg: '消息已经发送给到对方' });
                        popPane.hide();
                        popBox.hide();
                    }else{
                    Tips({msg:data.description});

                    }
                }
            });


        });

        //关闭对话框
        cancelBottm.bind('click', function () {
            popPane.hide();
            popBox.hide();
        });





    }
});