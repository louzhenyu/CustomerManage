Jit.AM.defindPage({
    name: 'Sign-up',
    onPageLoad: function () {
        this.initEvent();
    },
    initEvent: function () {
        var me = this;

        //延时隐藏微信菜单。
        var weixinTime = '', exeCount = 0;
        function WeixinHide() {
            if (typeof WeixinJSBridge != 'undefined') {
                // 隐藏右上角的选项菜单入口;  
                WeixinJSBridge.call('hideOptionMenu');
            };
        }
        weixinTime = window.setInterval(function () {
            WeixinHide();
            exeCount++;
            if (exeCount > 20) {
                window.clearInterval(weixinTime);
            };
        }, 100);

        /*
        大致流程
        1. 先绑定各级 select 元素的 change 事件 
        2. 加载当前数据后触发当前select的 change 事件 
        3. 触发当前select的 change事件时执行加载下级的数据
        加载当前数据->触发change事件-> 加载下级数据
        如此实现一层层传递加载
        */

        var province = city = school = "";
        var res = Jit.AM.getBaseAjaxParam();
        if (Jit.AM.getBaseAjaxParam() == null || Jit.AM.getBaseAjaxParam().customerId == null) {
            //如果userId不为空，则表示缓存已有基础数据，如果无，则需要给值
            if (me.getUrlParam('customerId') != null && me.getUrlParam('customerId') != "") {
                Jit.AM.setBaseAjaxParam({ "customerId": me.getUrlParam('customerId'), "userId": "", "openId": "" });
            }
        }

        //用户不为空时 获取数据
        if (res.userId != null && res.userId != "") {
            me.ajax({
                url: '/OnlineShopping/data/Data.aspx',
                data: {
                    'action': 'getWEventByUserID',
                    'vipId': res.userId
                },
                async: false,
                beforeSend: function () {
                    Jit.UI.Masklayer.show();
                },
                success: function (data) {
                    debugger;
                    if (data.code == 200) {
                        Jit.UI.Masklayer.hide();
                        if (data.content.vipList[0].Mapping == null || data.content.vipList[0].Mapping == "") {
                            $(".form_list").css("display", "block");
                            var vipList = data.content.vipList;
                            if (vipList != null && vipList.length > 0) {
                                $('#mobile').val(vipList[0].phone);
                                $('#name').val(vipList[0].name);
                                province = vipList[0].province;
                                city = vipList[0].city;
                                school = vipList[0].school;
                            }
                        } else {
                            $(".form_list").css("display", "none");
                            $(".success").css("display", "block");
                        }
                    }
                }
            });
        } else {
            $(".form_list").css("display", "block");
        }

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getProvinceList'
            },
            async: false,
            success: function (data) {
                var itemlists = data.content.provinceList;
                $('[jitval=province]').html('');
                $('[jitval=province]').html('<option value="">--请选择--</option>');
                $.each(itemlists, function (i, obj) {
                    $("[jitval=province]").append("<option value='" + obj.Province + "'>" + obj.Province + "</option>");
                });
            }
        });

        $('#province').val(province);

        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getCityByProvinceOfHS',
                Province: $("[jitval=province]").val()
            },
            async: false,
            success: function (data) {
                var itemlists = data.content.cityList;
                $('[jitval=city]').html('');
                $('[jitval=city]').html('<option value="">--请选择--</option>');
                $.each(itemlists, function (i, obj) {
                    $("[jitval=city]").append("<option value='" + obj.CityCode + "'>" + obj.CityName + "</option>");
                });
            }
        });

        $("#city option").each(function () {
            if ($(this).text() === '' + city + '') {
                $(this).attr('selected', 'selected');
            }
        });
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getSchoolList',
                CityName: city
            },
            async: false,
            success: function (data) {
                var itemlists = data.content.cityList;
                $('[jitval=school]').html('');
                $('[jitval=school]').html('<option value="">--请选择--</option>');
                $.each(itemlists, function (i, obj) {
                    $("[jitval=school]").append("<option value='" + obj.School + "'>" + obj.School + "</option>");
                });
                $('[jitval=school]').append('<option value="其他院校">其他院校</option>');
            }
        });

        $('#school').val(school);
        $('#province').bind('change', null, initCity);
        $('#city').bind('change', null, initSchool);

    },
    fnSubmit: function () {
        var me = this;
        //手机正则验证
        var mobileRegular = /^(1(([0-9][0-9])|(47)|[8][012356789]))\d{8}$/;
        var mobile = $('[jitval=mobile]').val();
        var name = $('[jitval=name]').val();
        var province = $('[jitval=province]').val();
        var city = $('[jitval=city]').val();
        var school = $('[jitval=school]').val();

        //手机不能为空
        if (mobile == "" || mobile == null) {
            Jit.UI.Dialog({
                'content': '手机不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        //手机格式验证
        if (!mobile.match(mobileRegular)) {
            Jit.UI.Dialog({
                'content': '手机格式不正确,请重新输入！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        //姓名不能为空
        if (name == null || name == "") {
            Jit.UI.Dialog({
                'content': '姓名不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        //省份不能为空
        if (province == null || province == "" || province == "--请选择--") {
            Jit.UI.Dialog({
                'content': '省份不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        //城市不能为空
        if (city == null || city == "" || city == "--请选择--") {
            Jit.UI.Dialog({
                'content': '城市不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        //学校不能为空
        if (school == null || school == "" || school == "--请选择--") {
            Jit.UI.Dialog({
                'content': '学校不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }

        //发送请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'joinGroupon',
                'phone': $('[jitval=mobile]').val(),
                'name': $('[jitval=name]').val(),
                'city': $('[jitval=city]').val(),
                'school': $('[jitval=school]').val()
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                var res = data.content;
                Jit.UI.Masklayer.hide();
                if (data.code == 200) {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                            me.toPage('Sign-up');
                        }
                    });
                } else {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                    return false;
                }
            }
        });
    }

});
function initCity() {
    var me = this;
    JitPage.ajax({
        url: '/OnlineShopping/data/Data.aspx',
        data: {
            'action': 'getCityByProvinceOfHS',
            Province: $("[jitval=province]").val()
        },
        success: function (data) {
            var itemlists = data.content.cityList;
            $('[jitval=city]').html('');
            $('[jitval=city]').html('<option value="">--请选择--</option>');
            $.each(itemlists, function (i, obj) {
                $("[jitval=city]").append("<option value='" + obj.CityCode + "'>" + obj.CityName + "</option>");
            });
        }
    });
}
function initSchool() {
    var select = document.getElementById("city");
    var index = select.selectedIndex;
    JitPage.ajax({
        url: '/OnlineShopping/data/Data.aspx',
        data: {
            'action': 'getSchoolList',
            CityName: select.options[index].text
        },
        success: function (data) {
            debugger;
            var itemlists = data.content.cityList;
            $('[jitval=school]').html('');
            $('[jitval=school]').html('<option value="">--请选择--</option>');
            $.each(itemlists, function (i, obj) {
                $("[jitval=school]").append("<option value='" + obj.School + "'>" + obj.School + "</option>");
            });
            $('[jitval=school]').append('<option value="其他院校">其他院校</option>');
        }
    });
}