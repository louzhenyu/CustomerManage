Jit.AM.defindPage({
    name: 'Perfect',
    onPageLoad: function () {
        var me = this;
        this.initEvent();
    },
    initEvent: function () {
        var me = this;
        $("#inviteCode").val(me.getUrlParam("code"));
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

        var res = Jit.AM.getBaseAjaxParam();
        if (Jit.AM.getBaseAjaxParam() == null || Jit.AM.getBaseAjaxParam().customerId == null) {
            //如果userId不为空，则表示缓存已有基础数据，如果无，则需要给值
            if (me.getUrlParam('customerId') != null && me.getUrlParam('customerId') != "") {
                Jit.AM.setBaseAjaxParam({ "customerId": me.getUrlParam('customerId'), "userId": "", "openId": "" });
            }
        }


        var self = this;
        self.ajax({
            url: '/DynamicInterface/data/Data.aspx',
            data: {
                "action": "getEventList", "page": 1, "pageSize": 1000, "EventTypeID": ""
            },
            success: function (data) {
                if (data && data.code == 200 && data.content.content) {
                    alert(1);
                }
            }
        });

        //用户不为空时 获取数据
        if (res.userId != null && res.userId != "") {
            $(".form_list").css("display", "none");
            Jit.UI.Dialog({
                'content': '您已经成功注册！',
                'type': 'Confirm',
                'LabelOk': '去团购',
                'LabelCancel': '去刮奖',
                'CallBackOk': function () {
                    me.toPage('Sign-up');
                },
                'CallBackCancel': function () {
                    me.toPage('Prize');
                }
            });
            return false;
        } else {
            $(".form_list").css("display", "block")
        }
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getProvinceList'
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                Jit.UI.Masklayer.hide();
                me.initProvince(data.content);
            }
        });
    },
    fnRegister: function () {

        debugger;
        var me = this;
        //手机正则验证
        var mobileRegular = /^(1(([0-9][0-9])|(47)|[8][012356789]))\d{8}$/;
        var emailRegular = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;

        var mobile = $('[jitval=mobile]').val();
        var code = $('[jitval=code]').val();
        var name = $('[jitval=name]').val();
        var province = $('[jitval=province]').val();
        var city = $('[jitval=city]').val();
        var school = $('[jitval=school]').val();
        var email = $('[jitval=email]').val();
        var check = $('[jitval=check]').val();
        var inviteCode = $('[jitval=inviteCode]').val();

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
        //验证码不能为空
//        if (code == null || code == "") {
//            Jit.UI.Dialog({
//                'content': '验证码不能为空！',
//                'type': 'Alert',
//                'CallBackOk': function () {
//                    Jit.UI.Dialog('CLOSE');
//                }
//            });
//            return false;
//        }
//        //验证码长度验证
//        if (code != null && code != "" && code.length > 6) {
//            Jit.UI.Dialog({
//                'content': '验证码过长！',
//                'type': 'Alert',
//                'CallBackOk': function () {
//                    Jit.UI.Dialog('CLOSE');
//                }
//            });
//            return false;
//        }
//        if (code != null && code != "" && code.length < 6) {
//            Jit.UI.Dialog({
//                'content': '验证码过短！',
//                'type': 'Alert',
//                'CallBackOk': function () {
//                    Jit.UI.Dialog('CLOSE');
//                }
//            });
//            return false;
//        }
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

        if (email != null && email != "") {
            if (!emailRegular.test(email)) {
                Jit.UI.Dialog({
                    'content': '邮箱格式不正确,请重新输入！',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return false;
            }
        }

        //发送请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'setVip',
                'phone': $('[jitval=mobile]').val(),
                'name': $('[jitval=name]').val(),
                'city': $('[jitval=city]').val(),
                'school': $('[jitval=school]').val(),
                'email': $('[jitval=email]').val(),
                'code': $('[jitval=code]').val(),
                'inviteCode': $('[jitval=inviteCode]').val(),
                'check': document.getElementById("check").checked == true ? "1" : "0"
            },
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function (data) {
                var res = data.content;
                Jit.UI.Masklayer.hide();
                if (data.code == 200) {

                    Jit.AM.setBaseAjaxParam({ "userId": data.content.vipID, "openId": data.content.vipId, "customerId": me.getUrlParam('customerId') })

                    if (me.getUrlParam("backpage") != null && me.getUrlParam("backpage") != "") {
                        me.toPage('Prize');
                        return false;
                    }
                    Jit.UI.Dialog({
                        'content': '操作成功!',
                        'type': 'Confirm',
                        'LabelOk': '去团购',
                        'LabelCancel': '去刮奖',
                        'CallBackOk': function () {
                            me.toPage('Perfect');
                        },
                        'CallBackCancel': function () {
                            me.toPage('Prize');
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
    },
    fnGetVeriCode: function () {
        var me = this;

        //手机正则验证
        var mobileRegular = /^(1(([35][0-9])|(47)|[8][012356789]))\d{8}$/;

        var phoneNumber = $('[jitval=mobile]').val();
        if (phoneNumber == "" || phoneNumber == null) {
            Jit.UI.Dialog({
                'content': '请输入手机号码！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        } else if (!phoneNumber.match(mobileRegular)) {
            Jit.UI.Dialog({
                'content': '手机格式不正确,请重新输入！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }

        me.countDown();

        var jsonarr = { 'action': "sendCode", 'mobile': phoneNumber };
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: jsonarr,
            success: function (data) {
                if (data && data.code == 200) {

                } else {
                    Jit.UI.Dialog({
                        'content': '验证码发送错误，请稍后重试！',
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                    return false;
                }
            },
            error: function (res, sta, error) {
                Jit.UI.Dialog({
                    'content': error,
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return false;
            }
        });


    },
    initProvince: function (data) {
        var me = this;
        var itemlists = data.provinceList;
        $('[jitval=province]').html('');
        $('[jitval=province]').html('<option value="">--请选择--</option>');
        $.each(itemlists, function (i, obj) {
            $("[jitval=province]").append("<option value='" + obj.Province + "'>" + obj.Province + "</option>");
        });
    },
    initCity: function () {
        var me = this;
        me.ajax({
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
    },
    initSchool: function () {
        var me = this;
        var select = document.getElementById("city");
        var index = select.selectedIndex;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getSchoolList',
                CityName: select.options[index].text
            },
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
    },
    countDown: function () {
        var me = this;
        var btncode = $('#timeSpan');
        $("#btnCode").attr("href", "javascript:;");
        $("#btnCode").addClass('unenable');
        var me = this;
        me.timeNum = 60;
        me.timer = setInterval(function () {
            if (me.timeNum > 0) {
                btncode.html('(' + me.timeNum + ')');
                me.timeNum--;
            } else {
                me.getCodeOnOff = true;
                btncode.html('');
                $("#btnCode").removeClass('unenable');
                $("#btnCode").attr("href", "javascript:JitPage.fnGetVeriCode();");
                clearTimeout(me.timer);
                me.timer = null;
            }
        }, 1000);
    }
});