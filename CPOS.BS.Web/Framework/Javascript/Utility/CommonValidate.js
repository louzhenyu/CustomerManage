var usernameold, emailold, authcodeold;
var usernamestate = false, emailstate = false, authcodestate = false;

Ext.onReady(function () {
    Ext.QuickTips.init();
    //控制提示信息显示在下面
    //    Ext.form.Field.prototype.msgTarget = "under";
    Ext.apply(Ext.form.field.VTypes, {
        password: function (val, field) {
            if (val == "") {
                return false;
            }
            if (field.compareField) {
                var pwd = field.up('form').down('#' + field.compareField);
                return (val == pwd.getValue());
            }
            return true;
        },
        passwordText: '密码不一致',

        mobile: function (val) {
            if (val == "") {
                return false;
            }
            var format = validateRules.isMobile(val);
            if (!format) {
                return false;
            }
            return true;
        },
        mobileText: '请输入正确的手机号',

        username: function (val, field) {
            if (val == "") {
                return false;
            }
            var unametext = "用户名重复";
            var format = validateRules.isUid(val);
            var length = validateRules.betweenLength(val.replace(/[^\x00-\xff]/g, "**"), 4, 20);
            if (!length && format) {
                this.usernameText = "用户名长度只能在4-20位字符之间";
                return false;
            }
            else if (!length && !format) {
                this.usernameText = "用户名只能由中文、英文、数字及“_”、“-”组成";
                return false;
            }
            else if (length && !format) {
                this.usernameText = "用户名只能由中文、英文、数字及“_”、“-”组成";
                return false;
            }
            else {
                if (!usernamestate || usernameold != val) {
                    if (usernameold != val) {
                        usernameold = val;
                        Ext.Ajax.request({
                            url: "/Handler/Common/ValidateHandler.ashx?type=CheckUserName",
                            params: { username: escape(val) }, // 获取表单输入的键值对
                            method: 'post',
                            async: false,
                            success: function (response) {
                                if (response.responseText != "") {
                                    var jdata = eval(response.responseText);
                                    if (jdata[0].success) {
                                        usernamestate = true;
                                    } else {
                                        usernamestate = false;
                                        unametext = "用户名重复";
                                    }
                                }
                            },
                            failure: function () {
                                usernamestate = false;
                                unametext = "用户名重复";
                            }
                        });
                    }
                }
            }
            this.usernameText = unametext;
            return usernamestate;
        },
        usernameText: "用户名格式不正确",
        number: function (val) {
            if (val == "") {
                return false;
            }
            var format = validateRules.isNum1(val); //大于等于0的整数
            if (!format) {
                return false;
            }
            return true;
        },
        numberText: '请输入整数',

        positiveInteger: function (val) {
            if (val == "") {
                return false;
            }
            var format = validateRules.isNum2(val); //大于0的整数
            if (!format) {
                return false;
            }
            return true;
        },
        positiveIntegerText: '请输入正整数',

        decimal: function (val) {
            if (val == "") {
                return false;
            }
            var format = validateRules.isDecimal1(val);
            if (!format) {
                return false;
            }
            return true;
        },
        decimalText: '请输入小数',

        enddate: function (val, field) {
            if (val == "") {
                return false;
            }
            if (field.begindateField) {
                var beginvalue = field.up('form').down('#' + field.begindateField).getValue();
                var begin = new Date(beginvalue);
                var end = new Date(field.getValue());
                return (begin <= end);
            }
            return true;
        },
        enddateText: '结束时间不能小于开始时间'
    });
});

var validateRegExp = {
    decmal: "^([+-]?)\\d*\\.\\d+$",    //浮点数
    decmal1: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$",    //正浮点数
    decmal2: "^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$",    //负浮点数
    decmal3: "^-?([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0)$",    //浮点数
    decmal4: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0$",    //非负浮点数（正浮点数 + 0）
    decmal5: "^(-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*))|0?.0+|0$",    //非正浮点数（负浮点数 + 0）
    intege: "^-?[1-9]\\d*$",    //整数
    intege1: "^[1-9]\\d*$",    //正整数
    intege2: "^-[1-9]\\d*$",    //负整数
    num: "^([+-]?)\\d*\\.?\\d+$",    //数字
    num1: "^[1-9]\\d*$",    //正数（正整数 不+ 0）
    num2: "^-[1-9]\\d*$",    //负数（负整数 不+ 0）
    ascii: "^[\\x00-\\xFF]+$",    //仅ACSII字符
    chinese: "^[\\u4e00-\\u9fa5]+$",    //仅中文
    color: "^[a-fA-F0-9]{6}$",    //颜色
    date: "^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$",    //日期
    email: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$",    //邮件
    idcard: "^[1-9]([0-9]{14}|[0-9]{17})$",    //身份证
    ip4: "^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$",    //ip地址
    letter: "^[A-Za-z]+$",    //字母
    letter_l: "^[a-z]+$",    //小写字母
    letter_u: "^[A-Z]+$",    //大写字母
    //mobile: "^0?(13|15|18)[0-9]{9}$",    //手机
    mobile: "^[0-9]\\d*$",    //手机
    notempty: "^\\S+$",    //非空
    password: "^[A-Za-z0-9_-]+$",    //密码
    picture: "(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$",    //图片
    qq: "^[1-9]*[1-9][0-9]*$",    //QQ号码
    rar: "(.*)\\.(rar|zip|7zip|tgz)$",    //压缩文件
    tel: "^[0-9\-()（）]{7,18}$",    //电话号码的函数(包括验证国内区号,国际区号,分机号)
    url: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$",    //url
    username: "^[A-Za-z0-9_\\-\\u4e00-\\u9fa5]+$",    //用户名
    deptname: "^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$",    //单位名
    zipcode: "^\\d{6}$",    //邮编
    realname: "^[A-Za-z\\u4e00-\\u9fa5]+$",  // 真实姓名
    companyname: "^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$",
    companyaddr: "^[A-Za-z0-9_()（）\\#\\-\\u4e00-\\u9fa5]+$",
    companysite: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&#=]*)?$"
};
//验证规则
var validateRules = {
    isNull: function (str) {
        return (str == "" || typeof str != "string");
    },
    betweenLength: function (str, _min, _max) {
        return (str.length >= _min && str.length <= _max);
    },
    isUid: function (str) {
        return new RegExp(validateRegExp.username).test(str);
    },
    isPwd: function (str) {
        return new RegExp(validateRegExp.password).test(str);
    },
    isPwd2: function (str1, str2) {
        return (str1 == str2);
    },
    isEmail: function (str) {
        return new RegExp(validateRegExp.email).test(str);
    },
    isTel: function (str) {
        return new RegExp(validateRegExp.tel).test(str);
    },
    isMobile: function (str) {
        return new RegExp(validateRegExp.mobile).test(str);
    },
    checkType: function (element) {
        return (element.attr("type") == "checkbox" || element.attr("type") == "radio" || element.attr("rel") == "select");
    },
    isChinese: function (str) {
        return new RegExp(validateRegExp.chinese).test(str);
    },
    isRealName: function (str) {
        return new RegExp(validateRegExp.realname).test(str);
    },
    isDeptname: function (str) {
        return new RegExp(validateRegExp.deptname).test(str);
    },
    isCompanyname: function (str) {
        return new RegExp(validateRegExp.companyname).test(str);
    },
    isCompanyaddr: function (str) {
        return new RegExp(validateRegExp.companyaddr).test(str);
    },
    isCompanysite: function (str) {
        return new RegExp(validateRegExp.companysite).test(str);
    },
    isNum: function (str) {
        return new RegExp(validateRegExp.intege).test(str);
    },
    isNum1: function (str) {
        return new RegExp(validateRegExp.num1).test(str);
    },
    isNum2: function (str) {//正整数
        return new RegExp(validateRegExp.intege1).test(str);
    },
    isDecimal1: function (str) {
        return new RegExp(validateRegExp.decmal1).test(str);
    }
};