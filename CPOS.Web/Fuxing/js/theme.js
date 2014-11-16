Jit.AM.defindPage({
    name: 'UserEdit',
    onPageLoad: function() {
        CheckSign();
        //当页面加载完成时触发


        this.initEvent();

        this.initData();

          $('.bot_nav dt:eq(0)').show();


    },
    initData: function() {


        //加载主题信息
        var me = this,
            keyVal = 'val',
            toEventId = Jit.AM.getUrlParam('toEventId'),
            isSignIn, //是否已经报名
            datas = {
                eventId: toEventId
            },
            curUserInfo;
        elements = {
            myJoin: $('.myjoin'),
            joinSubmit: $('joinSubmit'),
            bgShade: $('.bgshade'),
            submitArea: $('.submit_area'),
            submit: $('.submit_area .sub a:eq(0)'),
            cancel: $('.submit_area .sub a:eq(1)'),
            detailArea: $('#detailArea'),
            thClose: $('.submit_area .thclose ')
        };
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?action=getEventDetail',
            data: datas,
            success: function(data) {
                if (data.code == 200) {
                    elements.detailArea.html(SetDetailHtml(data.content));
                }
            }
        });


        function SetDetailHtml(detailInfo) {
            var strHtml = "";
            // strHtml += "<div> <img width=\"100%\" src=\"" + detailInfo.imageUrl + "\"> </div>";
            strHtml += "<div class=\"DetailAll\">";
            // strHtml += "<h2>" + detailInfo.title + "</h2>";
            // strHtml += "<div class=\"DetailLi\"> <h3><span>时间 </span> </h3> <p>" + detailInfo.timeStr + "</p> </div>";
            // strHtml += "<div class=\"DetailLi\"> <h3><span>地点 </span> </h3> <p>" + detailInfo.address + "</p> </div>";
            // strHtml += "<div class=\"DetailLi\"> <h3><span>活动发起 </span> </h3> <p>" + detailInfo.organizer + "</p> </div>";
            // strHtml += "<div class=\"DetailLi\"> <h3> <span>联系人 </span> </h3> <p>" + detailInfo.contact + "</p> </div>";
            strHtml += "<div class=\"DetailLi\">" + detailInfo.description + "</div>";
            strHtml += "</div>";
            return strHtml;
        }





        //验证用户是否已经报名
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?action=getUserSignInEvent',
            data: datas,
            success: function(data) {
                if (data.code) {
                    isSignIn = data.content.isSignIn;
                    if (isSignIn=="1") {
                     elements.myJoin.html('您已参加');
                     elements.myJoin.css('background-color','#8d8d8d');
                    }
                    LoadUserInfo();

                }

            }
        });


        elements.myJoin.bind('click', function() {
            if (isSignIn=="1") {
            return false;
               };
              elements.bgShade.show();
            elements.submitArea.show();
           $('html,body').scrollTop(20);
        });

        //加载当前用户
function LoadUserInfo(){
        var userDatas = {
            keyword: '',
            page: '1',
            pageSize: '30',
            vipId: me.getBaseInfo().userId
        };
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?Action=GetSearchVipStaff',
            data: userDatas,
            success: function(data) {
                if (data.code == 200 && data.content.vipList) {
                    curUserInfo = data.content.vipList[0];
                    SetQuestionHtml();
                }
            }
        });

}

        //获取我要参加的参数
        function SetQuestionHtml() {
            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?action=getEventApplyQues',
                data: datas,
                success: function(data) {
                    if (data.code == 200 && data.content.questions) {
                        elements.submitArea.find('.items').remove();
                        for (var i = 0; i < data.content.questions.length; i++) {
                            var questionInfo = data.content.questions[i];
                            var items = $(GetHtmlItem(questionInfo));
                            items.data(keyVal, questionInfo);
                            elements.submitArea.find('.sub').before(items);
                        }
                    elements.myJoin.show();
                    }else{
                    elements.myJoin.hide();
                    }

                    FxLoading.Hide();
                }
            });
        }
        FxLoading.AutoHide();

        //默认填写的用户信息
        function SetHtmlValue(qInfo) {
            var str = "";
            if (qInfo.cookieName) {
                var value = "";
                switch ($.trim(qInfo.cookieName)) {
                    case 'Company':
                        value = curUserInfo.staffCompany;
                        break;
                    case 'Username':
                        value = curUserInfo.staffName;
                        break;
                    case 'Post':
                        value = curUserInfo.staffPost;
                        break;
                        // case 'Contact':
                        // break;
                    case 'Email':
                        value = curUserInfo.email;
                        break;
                    case 'Phone':
                        value = curUserInfo.phone;
                        break;
                }

                if (qInfo.questionType == "1") {
                    str = "value=\"" + value + "\"";
                } else if (qInfo.questionType == "2") {
                    str = value;
                };

            };

            return str;
        }


        function GetHtmlItem(qInfo) {
            var str = "";
            switch (qInfo.questionType) {
                case '1':
                    str = "<dl class=\"items\" ><dt>" + qInfo.questionText + "" + (qInfo.isRequired == "1" ? "<span class=\"xingred\">*</span>" : "") + "</dt><dd><input type=\"text\" " + SetHtmlValue(qInfo) + "   id=\"" + qInfo.questionId + "\"></dd></dl>"; //单行文本 
                    break;
                case '2':
                    str = "<div class=\"items\" ><span>" + qInfo.questionText + " </span>" + (qInfo.isRequired == "1" ? "<span class=\"xingred\">*</span>" : "") + "" + (qInfo.isRequired ? "<span class=\"xingred\">*</span>" : "") + "  <br> <textarea id=\"" + qInfo.questionId + "\"  >" + SetHtmlValue(qInfo) + "</textarea></div>";
                    break; //文本域（多行文本）
                case '3':
                case '5':
                    str += "<div class=\"items\"><span>" + qInfo.questionText + " </span>" + (qInfo.isRequired == "1" ? "<span class=\"xingred\">*</span>" : "");

                    for (var i = 0; i < qInfo.options.length; i++) {
                        var option = qInfo.options[i];
                        str += "<br/><label><input type=\"radio\" name=\"" + qInfo.questionId + "\" id=\"" + qInfo.questionId + "\" value=\"" + option.optionText + "\" >&nbsp;&nbsp;" + option.optionText + "&nbsp;&nbsp;&nbsp;</label>";

                    };

                    str += "</div>";
                    break; //单选项
                case '4':
                    //复选框
                    str += "<div class=\"items\" ><span>" + qInfo.questionText + "</span>" + (qInfo.isRequired == "1" ? "<span class=\"xingred\">*</span>" : "");
                    for (var i = 0; i < qInfo.options.length; i++) {
                        var option = qInfo.options[i];
                        str += "<br/> <label><input type=\"checkbox\" name=\"" + qInfo.questionId + "\" id=\"" + qInfo.questionId + "\" value=\"" + option.optionText + "\" >&nbsp;&nbsp;" + option.optionText + "&nbsp;&nbsp;&nbsp;</label>";
                    };

                    str += "</div>";
                    break;

            }

            return str;
        }

        //提交报名
        elements.submit.bind('click',
            function() {

                datas = {
                    eventId: toEventId,
                    questions: [],
                    userName: curUserInfo.staffName,
                    mobile: curUserInfo.phone,
                    email: curUserInfo.email
                };

                var errors = {
                    obj: '',
                    tip: ''
                };
                elements.submitArea.find('.items').each(function() {
                    var self = $(this),
                        questionInfo = self.data(keyVal);
                    var questionItem = {
                        "questionId": questionInfo.questionId,
                        "isSaveOutEvent": questionInfo.needSaveCookie,
                        "cookieName": questionInfo.cookieName,
                        "questionValue": ""
                    };
                    switch (questionInfo.questionType) {
                        case '1':
                            questionItem.questionValue = self.find('input').val();
                            errors.obj = self.find('input');
                            break;
                        case '2':
                            questionItem.questionValue = self.find('textarea').val();
                            errors.obj = self.find('textarea');
                            break;
                        case '3':
                            questionItem.questionValue = self.find('input[type=radio]:checked').val();
                            errors.obj = self.find('input[type=radio]:checked');
                            break;
                        case '4':
                            var boxVal = "";
                            self.find('input[type=checkbox]:checked').each(function(i) {
                                var subSelf = $(this);
                                boxVal += subSelf.val();
                                if (i == 0) {
                                    errors.obj = subSelf;
                                };
                            });
                            questionItem.questionValue = boxVal;
                            break;
                        case '5':
                            questionItem.questionValue = self.find('input[type=radio]:checked').val();
                            errors.obj = self.find('input[type=radio]:checked');
                            break;
                    };

                    if (self.find('.xingred').size() && !questionItem.questionValue) {
                        errors.tip = (questionInfo.questionType == "1" || questionInfo.questionType == "2") ? '请输入' + questionInfo.questionText : '请选择' + questionInfo.questionText;
                        return false;
                    };
                    datas.questions.push(questionItem);
                });
                if (errors.tip) {
                    Tips({
                        msg: errors.tip,
                        fs:errors.obj
                    });
                    return false;
                };



                var params = {
                    'action': 'submitEventApply',
                    'Form': JSON.stringify({
                        'common': Jit.AM.getBaseAjaxParam(),
                        'special': datas,
                    })
                };



                $.post('/OnlineShopping/data/Data.aspx', params, function(data) {

                    if (data.code == 200) {
                        elements.submitArea.hide();
                        elements.bgShade.hide();
                        elements.myJoin.html('您已参加');
                        elements.myJoin.css('background-color','#8d8d8d');
                        isSignIn="1";
                    }
                    Tips({
                        msg: data.description
                    });

                }, 'json');



            });

        //报名窗口取消
        elements.cancel.bind('click',
            function() {
                elements.submitArea.hide();
                elements.bgShade.hide();
            });

        elements.thClose.bind('click', function() {
            elements.cancel.click();
        });

    },
    initEvent: function() {

    }
});