Jit.AM.defindPage({
    name: 'Propose',
    onPageLoad: function() {
        // CheckSign();
        //当页面加载完成时触发


        this.initEvent();

        this.initData();



    },
    initData: function() {


        //加载主题信息
        var me = this,
            keyVal = 'val',
            toEventId = Jit.AM.getUrlParam('eventId'),
            datas = {
                eventId: toEventId
            },
            curUserInfo;
        elements = {
            propseArea: $('.propose_list'),
            submit: $('.propose_list .sub a:eq(0)'),
        };


        //加载当前用户
        // function LoadUserInfo() {
        //     var userDatas = {
        //         keyword: '',
        //         page: '1',
        //         pageSize: '30',
        //         vipId: me.getBaseInfo().userId
        //     };
        //     me.ajax({
        //         url: '/OnlineShopping/data/Data.aspx?Action=GetSearchVipStaff',
        //         data: userDatas,
        //         success: function(data) {
        //             if (data.code == 200 && data.content.vipList) {
        //                 curUserInfo = data.content.vipList[0];
        //                 SetQuestionHtml();
        //             }
        //         }
        //     });

        // }


        // LoadUserInfo();
        SetQuestionHtml();

        //获取我要参加的参数
        function SetQuestionHtml() {
            me.ajax({
                url: '/OnlineShopping/data/Data.aspx?action=getEventApplyQues',
                data: datas,
                success: function(data) {
                    if (data.code == 200 && data.content.questions) {
                        elements.propseArea.find('.items').remove();
                        for (var i = 0; i < data.content.questions.length; i++) {
                            var questionInfo = data.content.questions[i];
                            var items = $(GetHtmlItem(questionInfo));
                            items.data(keyVal, questionInfo);
                            elements.propseArea.find('.sub').before(items);
                        }
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
                        value = curUserInfo.staffCompany||'';
                        break;
                    case 'Username':
                        value = curUserInfo.staffName||'';
                        break;
                    case 'Post':
                        value = curUserInfo.staffPost||'';
                        break;
                        // case 'Contact':
                        // break;
                    case 'Email':
                        value = curUserInfo.email||'';
                        break;
                    case 'Phone':
                        value = curUserInfo.phone||'';
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
                    str = "<input type=\"text\" " + SetHtmlValue(qInfo) + "   id=\"" + qInfo.questionId + "\">"; //单行文本 
                    break;
                case '2':
                    str = "<textarea maxlength=\"1000\" placeholder=\"请输入...\" id=\"" + qInfo.questionId + "\"  >" + SetHtmlValue(qInfo) + "</textarea>";
                    break; //文本域（多行文本）
                case '3':
                case '5':

                    for (var i = 0; i < qInfo.options.length; i++) {
                        var option = qInfo.options[i];
                        str += "<br/><label><input type=\"radio\" name=\"" + qInfo.questionId + "\" id=\"" + qInfo.questionId + "\" value=\"" + option.optionText + "\" >&nbsp;&nbsp;" + option.optionText + "&nbsp;&nbsp;&nbsp;</label>";

                    };

                    break; //单选项
                case '4':
                    //复选框
                    for (var i = 0; i < qInfo.options.length; i++) {
                        var option = qInfo.options[i];
                        str += "<br/> <label><input type=\"checkbox\" name=\"" + qInfo.questionId + "\" id=\"" + qInfo.questionId + "\" value=\"" + option.optionText + "\" >&nbsp;&nbsp;" + option.optionText + "&nbsp;&nbsp;&nbsp;</label>";
                    };
                    break;
            }

            var itemHtml = "";

            itemHtml += "<div class=\"item\"><dl><dt>" + qInfo.questionText + "" + (qInfo.isRequired == "1" ? "<span class=\"xingred\">*</span>" : "") + "</dt>";
            itemHtml += "<dd>" + str + "</dd></dl></div>";



            return itemHtml;
        }

        //提交建议
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
                elements.propseArea.find('.item').each(function() {
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
                        fs: errors.obj
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
                        elements.propseArea.find('input[type=text]').val('');
                        elements.propseArea.find('textarea').val('');
                    }
                    Tips({
                        msg: data.description
                    });

                }, 'json');



            });


    },
    initEvent: function() {

    }
});