var usernameOnfocus = "4-20位字符，可由中文、英文、数字及“_”、“-”组成",
    usernameIsNull = "请输入用户名", //
    usernameSucceed = ""
usernameErrorBeUsed = "该用户名已被使用，请使用其它用户名注册，如果您是&quot;{1}&quot;，请<a href='/signIn.aspx' class='flk13'>登录", 
    usernameErrorBadLength = "Your user ID must be between 4 and 20 characters long.", //用户名长度只能在4-20位字符之间
    usernameErrorBadFormat = "Your user ID must be letters, numbers, underscores, or dashes.", //用户名只能由中文、英文、数字及“_”、“-”组成
    usernameErrorFullnumber = "Your user ID can not use number only",

    pwdOnFocus = "User 6 to 16 characters. You may use letters, mumbers, underscores, or dashes.", //6-16位字符，可由英文、数字及“_”、“-”组成
    pwdSucceed = "",
    pwdIsNull = "Please enter your password.", //请输入密码
    pwdErrorBadLength = "Your password must be between 6 and 16 characters long.", //密码长度只能在6-16位字符之间
    pwdErrorBadFormat = "Your password must be letters, numbers, underscores, or dashes.", //密码只能由英文、数字及“_”、“-”组成

    pwd2OnFocus = "Please enter your password again.", //请再次输入密码
    pwd2Succeed = "",
    pwd2IsNull = "Please enter your password.", //请输入密码
    pwd2ErrorBadLength = "Your password must be between 6 and 16 characters long.", //密码长度只能在6-16位字符之间
    pwd2ErrorBadFormat2 = "Your password entries must match. Please verify your password again.", //两次输入密码不一致
    pwd2ErrorBadFormat1 = "Your password must be letters, numbers, underscores, or dashes.", //密码只能由英文、数字及“_”、“-”组成

    mailOnFocus = "请输入正确的邮箱/用户名", //请输入常用的邮箱，将用来找回密码、接收订单通知等
    mailSucceed = "",
    mailIsNull = "请输入正确的邮箱/用户名", //请输入邮箱
    mailErrorBeUsed = "This email was registered, please try another one or use this email to <a href=\"javascript:window.parent.location='/forgetPwd_Send.aspx'\" class='flk13'>reset password</a>.", //该邮箱已被使用，请更换其它邮箱，或使用该邮箱<a href=\"javascript:window.parent.location='/forgetPwd_Send.aspx'\" class='flk13'>找回密码</a>
    mailErrorBadLength = "Your email address must be less than 50 characters long.", //您填写的邮箱过长，邮件地址只能在50个字符以内
    mailErrorBadFormat = "This is not a valid email address.", //邮箱格式不正确

    authcodeOnFocus = "Please type the characters you see in this image. This code can be typed in all lowercase.", //请输入图片中的字符，不区分大小写
    authcodeSucceed = "",
    authcodeIsNull = "Please type the characters you see in this image.", //请输入验证码
    authcodeError = "Looks like you had trouble reading the security code. Please try to validate the security code again.", //验证码错误


    referrerOnFocus = "If you sign up and finish orders, your inviter maybe get a chance to be rewarded.", //如您注册并完成订单，推荐人有机会获得积分
    referrerSucceed = "",
    referrerIsNull = "",
    referrerError = "",

    emptyOnFocus = ""
emptySucceed = "",
    emptyIsNull = "",
    emptyError = "",

    optionChecking = "<span style='color:#999'>Verifying</span>", //检验中……
    mayNull = "Optional",


    requiredOnFocus = "",
    requiredSucceed = "",
    requiredIsNull = "required", //必填项
    requiredError = "",

    numOnFocus = "Please enter an integer,do not distinguish between positive and negative numbers.", //请输入整数，不区分正负数
    numSucceed = "",
    numIsNull = "Please enter an integer.", //请输入整数
    numError = "Incorrect input format.", //输入格式不正确

    num1OnFocus = "Please enter an positive integer.", //请输入正整数
    num1Succeed = "",
    num1IsNull = "Please enter an positive integer.", //请输入正整数
    num1Error = "Incorrect input format.", //输入格式不正确

    decimal1OnFocus = "Please enter an positive float.", //请输入正浮点数
    decimal1Succeed = "", //
    decimal1IsNull = "Please enter an positive float.", //请输入正浮点数
    decimal1Error = "Incorrect input format.", //输入格式不正确

    mobileOnFocus = "Please enter the phone number.", //请输入手机号码
    mobileSucceed = "", //
    mobileIsNull = "Please enter the phone number.", //请输入手机号码
    mobileError = "Incorrect phone number format."; //手机号码格式不正确