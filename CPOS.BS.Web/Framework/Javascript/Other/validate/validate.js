var validateRegExp = {
    decmal:"^([+-]?)\\d*\\.\\d+$",    //浮点数
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
    realname:"^[A-Za-z\\u4e00-\\u9fa5]+$",  // 真实姓名
    companyname:"^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$",
    companyaddr:"^[A-Za-z0-9_()（）\\#\\-\\u4e00-\\u9fa5]+$",
    companysite:"^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&#=]*)?$"
};


//$("#username").validate(validatePrompt.username, validateFunction.username, true);


//插件
/*option 提示信息类
	callback 操作类
		def 默认值，如果为 true则是外部调用（例如提交按钮），如果不为true 则为控件的事件绑定
*/
(function($) {
    $.fn.validate = function(option, callback, def) {
        var ele = this;
        var id = ele.attr("id");//当前控件id
        var type = ele.attr("type");//当前控件type
        var rel = ele.attr("rel");//当前控件rel
        var _onFocus = $("#" + id + validateSettings.onFocus.container);//focus  msg控件
        var _succeed = $("#" + id + validateSettings.succeed.container);//success  msg控件
        var _isNull = $("#" + id + validateSettings.isNull.container);//非空判断  msg控件
        var _error = $("#" + id + validateSettings.error.container);//格式错误 msg控件
        if (def == true) {//外部调用
            var str = ele.val();
            var tag = ele.attr("sta");//获取需验证控件的状态
            if (str == "" || str == "-1") {
                validateSettings.isNull.run({
                    prompts: option,
                    element: ele,
                    isNullEle: _isNull,
                    succeedEle: _succeed
                }, option.isNull);
            } else if (tag == 1 || tag == 2) {//如果控件的状态 为 错误 或者成功，则不需要再验证
                return;
            } else {//如果控件未经过验证，则进行验证
                callback({
                    prompts: option,
                    element: ele,
                    value: str,
                    errorEle: _error,
                    succeedEle: _succeed
                });
            }
        } else {//控件自身绑定
            if (typeof def == "string") {
                str = ele.val();
                if (str == "") {
                    ele.val(def);
                    ele.css("color","gray");
                }
            }
            switch (type) {
                case "text"://文本框
                case "password"://密码
                    ele.bind("focus", function() {//绑定获取焦点事件，给出提示信息
                        var str = ele.val();
                        if (str == def) {
                            ele.val("");
                            ele.css("color","Black");
                        }
                        validateSettings.onFocus.run({
                            prompts: option,
                            element: ele,
                            value: str,
                            onFocusEle: _onFocus,
                            succeedEle: _succeed
                        }, option.onFocus);
                    })
                        .bind("blur", function() {//绑定失去焦点事件，非空验证、执个性化行验证事件
                            var str = ele.val();
                            if (str == "") {
                                ele.val(def);
                                if (typeof def == "string") {
                                    ele.css("color","gray");
                                }
                            }
                            if (validateRules.isNull(str)) {//如果文本框为空，进行非空提示
                                validateSettings.isNull.run({
                                    prompts: option,
                                    element: ele,
                                    value: str,
                                    isNullEle: _isNull,
                                    succeedEle: _succeed
                                }, "");
                            } else {//如果文本框不为空，执行个性化验证事件
                                callback({
                                    prompts: option,
                                    element: ele,
                                    value: str,
                                    errorEle: _error,
                                    isNullEle: _isNull,
                                    succeedEle: _succeed
                                });
                            }
                        });
                    break;
                default:
                    break;
            }
        }
    }
})(jQuery);

//通用配置
/*
option 操作对象集合
str  提示信息
*/
var validateSettings = {
    onFocus: {
        state: null,
        container: "_error",
        style: "focus",
        run: function(option, str) {
            if (!validateRules.checkType(option.element)) {
                option.element.removeClass(validateSettings.INPUT_style2).addClass(validateSettings.INPUT_style1);
            }
            option.onFocusEle.removeClass().addClass(validateSettings.onFocus.style).html(str);
        }
    },
    isNull: {
        state: 0,
        container: "_error",
        style: "null",
        run: function(option, str) {
            option.element.attr("sta", 0);
            if (!validateRules.checkType(option.element)) {
                if (str != "") {
                    option.element.removeClass(validateSettings.INPUT_style1).addClass(validateSettings.INPUT_style2);
                } else {
                    option.element.removeClass(validateSettings.INPUT_style2).removeClass(validateSettings.INPUT_style1);
                }
            }
            option.succeedEle.removeClass(validateSettings.succeed.style);
            option.isNullEle.removeClass().addClass(validateSettings.isNull.style).html(str);
        }
    },
    error: {
        state: 1,
        container: "_error",
        style: "error",
        run: function(option, str) {
            option.element.attr("sta", 1);
            if (!validateRules.checkType(option.element)) {
                option.element.removeClass(validateSettings.INPUT_style1).addClass(validateSettings.INPUT_style2);
            }
            option.succeedEle.removeClass(validateSettings.succeed.style);
            option.errorEle.removeClass().addClass(validateSettings.error.style).html(str);
        }
    },
    succeed: {
        state: 2,
        container: "_succeed",
        style: "succeed",
        run: function(option) {
            option.element.attr("sta", 2);
            option.errorEle.empty();
            if (!validateRules.checkType(option.element)) {
                option.element.removeClass(validateSettings.INPUT_style1).removeClass(validateSettings.INPUT_style2);
            }
            option.succeedEle.addClass(validateSettings.succeed.style);
        }
    },
    INPUT_style1: "highlight1",
    INPUT_style2: "highlight2"
};


//验证规则
var validateRules = {
    isNull:function(str) {
        return (str == "" || typeof str != "string");
    },
    betweenLength:function(str, _min, _max) {
        return (str.length >= _min && str.length <= _max);
    },
    isUid:function(str) {
        return new RegExp(validateRegExp.username).test(str);
    },
    isPwd:function(str) {
        return new RegExp(validateRegExp.password).test(str);
    },
    isPwd2:function(str1, str2) {
        return (str1 == str2);
    },
    isEmail:function(str) {
        return new RegExp(validateRegExp.email).test(str);
    },
    isTel:function(str) {
        return new RegExp(validateRegExp.tel).test(str);
    },
    isMobile:function(str) {
        return new RegExp(validateRegExp.mobile).test(str);
    },
    checkType:function(element) {
        return (element.attr("type") == "checkbox" || element.attr("type") == "radio" || element.attr("rel") == "select");
    },
    isChinese:function(str) {
        return new RegExp(validateRegExp.chinese).test(str);
    },
    isRealName:function(str) {
        return new RegExp(validateRegExp.realname).test(str);
    },
    isDeptname:function(str) {
        return new RegExp(validateRegExp.deptname).test(str);
    },
    isCompanyname:function(str) {
        return new RegExp(validateRegExp.companyname).test(str);
    },
    isCompanyaddr:function(str) {
        return new RegExp(validateRegExp.companyaddr).test(str);
    },
    isCompanysite:function(str) {
        return new RegExp(validateRegExp.companysite).test(str);
    },
    isNum:function(str){
        return new RegExp(validateRegExp.intege).test(str);
    },
    isNum1:function(str){
        return new RegExp(validateRegExp.num1).test(str);
    },
    isDecimal1:function(str){
        return new RegExp(validateRegExp.decmal1).test(str);
    }
};
//验证文本
var validatePrompt = {
    username: {
        onFocus: usernameOnfocus,
        succeed: usernameSucceed,
        isNull: usernameIsNull,
        error: {
            beUsed: usernameErrorBeUsed,
            badLength: usernameErrorBadLength,
            badFormat: usernameErrorBadFormat,
            fullNumberName: usernameErrorFullnumber
        }
    },
    changeusername: {
        onFocus: usernameOnfocus,
        succeed: usernameSucceed,
        isNull: usernameIsNull,
        error: {
            beUsed: usernameErrorBeUsed,
            badLength: usernameErrorBadLength,
            badFormat: usernameErrorBadFormat,
            fullNumberName: usernameErrorFullnumber
        }
    },
    pwd: {
        onFocus: pwdOnFocus,
        succeed: pwdSucceed,
        isNull: pwdIsNull,
        error: {
            badLength: pwdErrorBadLength,
            badFormat: pwdErrorBadFormat
        }
    },
    pwd2: {
        onFocus: pwd2OnFocus,
        succeed: pwd2Succeed,
        isNull: pwd2IsNull,
        error: {
            badLength: pwd2ErrorBadLength,
            badFormat2: pwd2ErrorBadFormat2,
            badFormat1: pwd2ErrorBadFormat1
        }
    },
    mail: {
        onFocus: mailOnFocus,
        succeed: mailSucceed,
        isNull: mailIsNull,
        error: {
            beUsed: mailErrorBeUsed,
            badFormat: mailErrorBadFormat,
            badLength: mailErrorBadLength
        }
    },
    mail2: {
        onFocus: mailOnFocus,
        succeed: mailSucceed,
        isNull: mailIsNull,
        error: {
            beUsed: mailErrorBeUsed,
            badFormat: mailErrorBadFormat,
            badLength: mailErrorBadLength
        }
    },
    changemail: {
        onFocus: mailOnFocus,
        succeed: mailSucceed,
        isNull: mailIsNull,
        error: {
            beUsed: mailErrorBeUsed,
            badFormat: mailErrorBadFormat,
            badLength: mailErrorBadLength
        }
    },
    authcode: {
        onFocus: authcodeOnFocus,
        succeed: authcodeSucceed,
        isNull: authcodeIsNull,
        error: authcodeError
    },
    referrer: {
        onFocus: referrerOnFocus,
        succeed: referrerSucceed,
        isNull: referrerIsNull,
        error: referrerError
    },
    empty: {
        onFocus:emptyOnFocus,
        succeed:emptySucceed,
        isNull:emptyIsNull,
        error:emptyError
    },
    required: {
        onFocus:requiredOnFocus,
        succeed:requiredSucceed,
        isNull:requiredIsNull,
        error:requiredError
    },
    num: {
        onFocus:numOnFocus,
        succeed:numSucceed,
        isNull:numIsNull,
        error:numError
    },
    num1: {
        onFocus:num1OnFocus,
        succeed:num1Succeed,
        isNull:num1IsNull,
        error:num1Error
    },
    decimal1: {
        onFocus:decimal1OnFocus,
        succeed:decimal1Succeed,
        isNull:decimal1IsNull,
        error:decimal1Error
    },
    mobile: {
        onFocus:mobileOnFocus,
        succeed:mobileSucceed,
        isNull:mobileIsNull,
        error:mobileError
    }
};

var nameold,emailold,authcodeold;
var namestate = false,emailstate = false,authcodestate = false;
//回调函数
var validateFunction = {
    username: function(option) {
        var format = validateRules.isUid(option.value);
        var length = validateRules.betweenLength(option.value.replace(/[^\x00-\xff]/g, "**"), 4, 20);
        if (!length && format) {
            validateSettings.error.run(option, option.prompts.error.badLength);
        }
        else if (!length && !format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        }
        else if (length && !format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        }
        else {
            if (!namestate || nameold != option.value) {
                if (nameold != option.value) {
                    nameold = option.value;
                    option.errorEle.html(optionChecking);
                    $.get("/AjaxFile/AjaxService.aspx?action=CheckLoginName&str=" + escape(option.value) + "&r=" + Math.random(), function(date) {
                        date = eval(date);
                        if (date[0].success == 0) {
                            validateSettings.succeed.run(option);
                            namestate = true;
                        } else {
                            validateSettings.error.run(option, option.prompts.error.beUsed.replace("{1}", option.value));
                            namestate = false;
                        }
                    })
                }
                else {
                    validateSettings.error.run(option, option.prompts.error.beUsed.replace("{1}", option.value));
                    namestate = false;
                }
            }
            else {
                validateSettings.succeed.run(option);
            }
        }
    },
    changeusername: function(option) {
        var format = validateRules.isUid(option.value);
        var length = validateRules.betweenLength(option.value.replace(/[^\x00-\xff]/g, "**"), 4, 20);
        if (!length && format) {
            validateSettings.error.run(option, option.prompts.error.badLength);
        }
        else if (!length && !format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        }
        else if (length && !format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        }
        else {
            if (!namestate || nameold != option.value) {
                if (typeof nameold != "string")//获取旧的用户名
                {
                    var oldname = option.element.attr("id") + "_Old";
                    nameold = $("#" + oldname).val();
                }
                if (nameold != option.value) {

                    option.errorEle.html(optionChecking);
                    $.get("/AjaxFile/AjaxService.aspx?action=CheckLoginName&str=" + escape(option.value) + "&r=" + Math.random(), function(date) {
                        date = eval(date);
                        if (date[0].success == 0) {
                            validateSettings.succeed.run(option);
                            namestate = true;
                        } else {
                            validateSettings.error.run(option, option.prompts.error.beUsed);
                            namestate = false;
                        }
                    })
                }
                else {
                    //validateSettings.error.run(option, option.prompts.error.beUsed);
                    //namestate = false;
                    validateSettings.succeed.run(option);
                }
            }
            else {
                validateSettings.succeed.run(option);
            }
        }
    },
    pwd: function(option) {
        var str1 = option.value;
        var str2 = $("#" + option.element.attr("id") + "2").val();
        var format = validateRules.isPwd(option.value);
        var length = validateRules.betweenLength(option.value, 6, 16);
        if (!length && format) {
            validateSettings.error.run(option, option.prompts.error.badLength);
        }
        else if (!length && !format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        }
        else if (length && !format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        }
        else {
            validateSettings.succeed.run(option);
        }
        if (str2 == str1) {
            $("#" + option.element.attr("id") + "2").focus();
        }
    },
    pwd2: function(option) {
        var str1 = option.value;
        var pwd1Id = option.element.attr("id");
        var str2 = $("#" + pwd1Id.substring(0, pwd1Id.length - 1)).val();
        var length = validateRules.betweenLength(option.value, 6, 16);
        var format2 = validateRules.isPwd2(str1, str2);
        var format1 = validateRules.isPwd(str1);
        if (!length) {
            validateSettings.error.run(option, option.prompts.error.badLength);
        } else {
            if (!format1) {
                validateSettings.error.run(option, option.prompts.error.badFormat1);
            } else {
                if (!format2) {
                    validateSettings.error.run(option, option.prompts.error.badFormat2);
                }
                else {
                    validateSettings.succeed.run(option);
                }
            }
        }
    },
    mail: function(option) {
        var format = validateRules.isEmail(option.value);
        var format2 = validateRules.betweenLength(option.value, 0, 50);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        } else {
            if (!format2) {
                validateSettings.error.run(option, option.prompts.error.badLength);
            } else {
                if (!emailstate || emailold != option.value) {
                    if (emailold != option.value) {
                        emailold = option.value;
                        option.errorEle.html(optionChecking);
                        $.get("/AjaxFile/AjaxService.aspx?action=CheckUserEmail&str=" + escape(option.value) + "&r=" + Math.random(), function(date) {
                            date = eval(date);
                            if (date[0].success == 0) {
                                validateSettings.succeed.run(option);
                                emailstate = true;
                            } else {
                                validateSettings.error.run(option, option.prompts.error.beUsed);
                                emailstate = false;
                            }
                        })

                    }
                    else {
                        validateSettings.error.run(option, option.prompts.error.beUsed);
                        emailstate = false;
                    }
                }
                else {
                    validateSettings.succeed.run(option);
                }
            }
        }
    },
    mail2: function(option) {
        var format = validateRules.isEmail(option.value);
        var format2 = validateRules.betweenLength(option.value, 0, 50);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        } else {
            if (!format2) {
                validateSettings.error.run(option, option.prompts.error.badLength);
            }
            else {
                validateSettings.succeed.run(option);
            }
        }
    },
    changemail: function(option) {
        var format = validateRules.isEmail(option.value);
        var format2 = validateRules.betweenLength(option.value, 0, 50);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error.badFormat);
        } else {
            if (!format2) {
                validateSettings.error.run(option, option.prompts.error.badLength);
            } else {
                if (!emailstate || emailold != option.value) {
                    if (typeof emailold != "string")//获取旧的邮箱地址
                    {
                        var oldmail = option.element.attr("id") + "_Old";
                        emailold = $("#" + oldmail).val();
                    }
                    if (emailold != option.value) {
                        option.errorEle.html(optionChecking);
                        $.get("/AjaxFile/AjaxService.aspx?action=CheckUserEmail&str=" + escape(option.value) + "&r=" + Math.random(), function(date) {
                            date = eval(date);
                            if (date[0].success == 0) {
                                validateSettings.succeed.run(option);
                                emailstate = true;
                            } else {
                                validateSettings.error.run(option, option.prompts.error.beUsed);
                                emailstate = false;
                            }
                        })

                    }
                    else {
                        //相等，通过验证
                        //validateSettings.error.run(option, option.prompts.error.beUsed);
                        //emailstate = false;
                        validateSettings.succeed.run(option);
                    }
                }
                else {
                    validateSettings.succeed.run(option);
                }
            }
        }
    },
    referrer: function(option) {
        validateSettings.succeed.run(option);
    },
    authcode: function(option) {
        validateSettings.succeed.run(option);
        authcodestate = true;
    },
    required: function(option) {
        var bool = validateRules.isNull(option.value);
        if (bool) {
            validateSettings.isNull.run(option, option.prompts.isNull);
        } else {
            validateSettings.succeed.run(option);
        }
    },
    mobile: function(option) {
        var format = validateRules.isMobile(option.value);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error);
        }
        else {
            validateSettings.succeed.run(option);
        }

    },
    num: function(option) {
        var format = validateRules.isNum(option.value);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error);
        }
        else {
            validateSettings.succeed.run(option);
        }

    },
    num1: function(option) {
        var format = validateRules.isNum1(option.value);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error);
        }
        else {
            validateSettings.succeed.run(option);
        }

    },
    decimal1: function(option) {
        var format = validateRules.isDecimal1(option.value);
        if (!format) {
            validateSettings.error.run(option, option.prompts.error);
        }
        else {
            validateSettings.succeed.run(option);
        }
    },
    FORM_submit: function(elements) {
        var bool = true;
        var errorTopEle = new Number(0);
        for (var i = 0; i < elements.length; i++) {
            if ($(elements[i]).attr("sta") == 2) {
                bool = true;
            } else {
                if (bool) {
                    bool = false;
                    errorTopEle = i;
                }
                break;
            }
        }
        if (!bool) {
            $(elements[errorTopEle]).focus();
        }
        return bool;
    }
};

//更换验证码的方法
function ChangeCode()
{
    var url="/ImgesCode.aspx?n="+Math.random();
    $("#img_code").attr("src",url);
};

//去除页面验证码自动记忆
$(function() {
    if(document.getElementById("Tb_AuthCode"))
    {
        $("#Tb_AuthCode").attr("autocomplete","off");
    }
})