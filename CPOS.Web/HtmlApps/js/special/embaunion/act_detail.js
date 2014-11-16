Jit.AM.defindPage({
    name: 'ActDetail',
    keyUserInfo: 'userinfos',
    activityInfo: '',
    enTickedId: '', //报名ID
    enTickedPrice: '',
    elements: {
        submitError: false,
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //初始化数据
    initLoad: function() {
        var self = this;


        self.elements.btSignUp = $('#btSignUp');
        self.elements.txtActTitle = $('#txtActTitle');
        self.elements.txtActISOC = $('#txtActISOC');
        self.elements.txtActStartTime = $('#txtActStartTime');
        self.elements.txtActContacts = $('#txtActContacts');
        self.elements.txtActAddress = $('#txtActAddress');
        self.elements.txtActNumber = $('#txtActNumber');
        self.elements.txtActExplain = $('#txtActExplain');
        self.elements.btActMore = $('#btActMore');
        self.elements.ticketList = $('#ticketList');
        self.elements.actBox = $('.submit_area');
        self.elements.actBoxInfo = $('#actboxinfo');
        self.elements.actBgShade = $('.actBgShade');
        self.elements.actBoxSubmit = $('#actBoxSubmit');
        self.elements.txtActivityUp = $('#txtActivityUp');
        self.elements.txtActPhone = $('#txtActPhone');
        // self.elements.ticketExplain = $('.ticketExplain');
        self.elements.scrollList = $('#actWrapper');
        self.elements.addressMap = $('#addressMap');


        //清除输入框底部导航
        FootNavHide(true);

        self.elements.actBgShade.height($('body').height());


        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getEventByEventID',
                'ActivityID': self.getUrlParam('newsId')
            },
            beforeSend: function() {
                UIBase.loading.show();
            },
            success: function(data) {
                UIBase.loading.hide();
                if (data && data.code == 200) {
                    self.setPageInfo(data.content.Activity);
                    if (self.activityInfo.TicketID) {
                        self.elements.btSignUp.addClass('on');
                        self.elements.btSignUp.val("您已报名");
                    };
                }
            }
        });

        // 加载报名字段信息
        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getUserDefinedByUserID',
                TypeID: '2'
            },
            success: function(data) {
                self.elements.actBoxInfo.empty();
                if (data && data.code == 200 && data.content.pageList) {
                    self.elements.actBoxInfo.html(GetlistHtml(data.content.pageList));
                    self.controlDatas = data.content.pageList;
                    self.LoadUserCache();

                    //修正样式
                    $('.commonBox .comline').last().addClass('curline');
                } else {
                    self.elements.actBoxInfo.html("很抱歉，没有加载到你的配置信息。");
                }
            }
        });



        // 加载广告列表
        self.ajax({
            url: '/Project/CEIBS/CEIBSHandler.ashx',
            data: {
                'action': 'getImagesList',
                'eventId': self.getUrlParam('newsId')
            },
            success: function(data) {
                if (data && data.code == 200 && data.content.imageList) {

                    self.elements.scrollList.show();
                    var dataHtmls = self.GetActAdListHtml(data.content.imageList)
                    self.elements.scrollList.find('.DetailImgIeo ul').html(dataHtmls.adHtml);
                    self.elements.scrollList.find('.indicator').html(dataHtmls.navHtml);
                    self.topAdListEvent();
                } else {
                    self.elements.scrollList.hide();
                }
            }
        });



    },
    LoadUserCache: function() { //加载用户缓存信息
        var self = this,
            cacheUserInfos = self.getParams(self.keyUserInfo);

        if (cacheUserInfos) {
            for (var i = 0; i < cacheUserInfos.length; i++) {
                var userInfo = cacheUserInfos[i],
                    element = $('#' + userInfo.ControlId);
                if (element.size()) {
                    if (!element.val()) {
                        element.val(userInfo.Value);
                    };
                };

            };
        };

    },
    setPageInfo: function(activityInfo) {
        var self = this;
        self.activityInfo = activityInfo;
        self.elements.txtActTitle.html(activityInfo.ActivityTitle);
        self.elements.txtActISOC.html(activityInfo.ActivityCompany);
        self.elements.txtActStartTime.html(activityInfo.ActivityTime);
        self.elements.txtActContacts.html(activityInfo.ActivityLinker);
        self.elements.txtActAddress.html(activityInfo.ActivityAddr);
        self.elements.txtActivityUp.html(activityInfo.ActivityUp);
        self.elements.txtActPhone.html(activityInfo.ActivityPhone);


        self.elements.txtActExplain.html(activityInfo.ActivityContent);

        if (activityInfo.Ticket && activityInfo.Ticket.length > 0) {
            self.elements.ticketList.html(self.GetTicketHtml(activityInfo.Ticket));
            self.TickedBindEvent();
        };

        if (activityInfo.Longitude && activityInfo.Latitude) {
            var baseInfo = Jit.AM.getBaseAjaxParam();
            var toMapUrl = "/HtmlApps/auth.html?pageName=Map&customerId=" + baseInfo.customerId + "&openId=test&lng=" + activityInfo.Longitude + "&lat=" + activityInfo.Latitude + "&addr=" + activityInfo.ActivityAddr + "&store=" + activityInfo.ActivityTitle;
            self.elements.addressMap.attr('href', toMapUrl);
        };



    }, //绑定事件
    initEvent: function() {
        var self = this;

        self.elements.btSignUp.bind('tap', function() {
            if (self.elements.btSignUp.hasClass('on')) {
                return false;
            };
            self.SignUp();
        });

        self.elements.btActMore.bind('tap', function() {
            self.elements.txtActExplain.toggleClass('moreshow');
        });

        $('.thclose,#actBoxCancel', self.elements.actBox).bind('tap', function() {
            self.elements.actBgShade.hide();
            self.elements.actBox.hide();
        });
        self.elements.actBoxSubmit.bind('tap', function() {
            self.submitEnroll();
        });



    },
    topAdListEvent: function() {
        var self = this;

        //重新設置大小
        ReSize();

        function ReSize() {
            self.elements.scrollList.find('.DetailImgIeo').css({
                width: (self.elements.scrollList.width()) * self.elements.scrollList.find('li').size()
            });

            self.elements.scrollList.find('li').css({
                width: (self.elements.scrollList.width())
            });
        }
        var myScroll = new iScroll('actWrapper', {
            snap: true,
            momentum: false,
            hScrollbar: false,
            vScroll: false,
            onScrollEnd: function() {
                document.querySelector('#indicator > dd.active').className = '';
                document.querySelector('#indicator > dd:nth-child(' + (this.currPageX + 1) + ')').className = 'active';
            }
        });

        $(window).resize(function() {
            ReSize();
            myScroll.refresh();
        });

    },
    tips: function(str, obj) {
        Jit.UI.Dialog({
            'content': str,
            'type': 'Alert',
            'CallBackOk': function() {
                Jit.UI.Dialog('CLOSE');
                if (obj) {
                    obj.focus();
                };
            }
        });

    },
    TickedBindEvent: function() {
        var self = this;
        self.elements.ticketList.find('.list').bind('tap', function() {

            //已经报名不做任何操作
            // if (self.elements.btSignUp.hasClass('on')) {
            //     return false;
            // };
            var el = $(this);

            if (!el.hasClass('not')) {
                self.elements.ticketList.find('.list').removeClass('on');
                el.addClass('on');
                // self.elements.ticketExplain.html(el.data('explain')).show();
            };



        });
        //提交报名
    },
    getTicketItem: function(tickedId) {
        var self = this,
            tickedInfo;

        for (var i = 0; i < self.activityInfo.Ticket.length; i++) {
            var item = self.activityInfo.Ticket[i];
            if (item.TicketID == tickedId) {
                tickedInfo = item;
                break;
            };
        };

        return tickedInfo;
    },
    submitEnroll: function() {
        var self = this;

        self.submitError = false;
        var controList = self.GetControlInfos(1);

        if (self.submitError || !controList) {
            return false;
        };
        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'addEventInfo',
                'Control': controList,
                'ActivityID': self.activityInfo.ActivityID,
                'TicketID': self.enTickedId,
                'TicketPrice': self.enTickedPrice
            },
            beforeSend: function() {
                UIBase.loading.show();
            },
            success: function(data) {
                UIBase.loading.hide();
                if (data && data.code == 200) {

                    self.elements.btSignUp.addClass('on');
                    self.elements.btSignUp.val("您已报名");
                    self.elements.actBox.hide();
                    self.elements.actBgShade.hide();

                    self.setParams(self.keyUserInfo, controList);

                    var curTitketItem = self.getTicketItem(self.enTickedId),
                        enrollTip = self.activityInfo.VipSum > self.activityInfo.TicketSum ? '此活动名额已满，您将作为此次活动的备选人员' : '您已报名成功，正在审核';


                    //未登录的用户自动登陆
                    if (data.content.VipID && !Validates.isLogin()) {
                        var baseInfo = Jit.AM.getBaseAjaxParam();
                        baseInfo.userId = data.content.VipID;
                        baseInfo.openId = data.content.VipID;
                        Jit.AM.setBaseAjaxParam(baseInfo);
                    };

                    if (curTitketItem.TicketPrice <= 0) {
                        Jit.UI.Dialog({
                            'content': enrollTip,
                            'type': 'Alert',
                            'CallBackOk': function() {
                                Jit.UI.Dialog('CLOSE');
                            }
                        });
                    } else {

                        if (self.activityInfo.VipSum > self.activityInfo.TicketSum) {

                            Jit.UI.Dialog({
                                'content': enrollTip,
                                'type': 'Alert',
                                'CallBackOk': function() {
                                    self.toPage('Pay', '&orderId=' + data.content.OrderID);
                                }
                            });

                        } else {

                            self.toPage('Pay', '&orderId=' + data.content.OrderID);
                        }
                    }
                } else {

                    return Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });

                }
            }
        });

    },

    SignUp: function() { //参加报名
        var self = this;
        self.elements.enTickedId = '';
        $('.list', self.elements.ticketList).each(function() {
            var el = $(this),
                elParent = el.parent();
            if (el.hasClass('on')) {
                self.enTickedId = elParent.data(KeyList.val);
                self.enTickedPrice = elParent.data('price');
            };
        });

        if (!self.enTickedId) {
            Jit.UI.Dialog({
                'content': '请选择票',
                'type': 'Alert',
                'CallBackOk': function() {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        };

        self.elements.actBgShade.show();
        self.elements.actBox.show();
        // UIBase.middleShow({obj:self.elements.actBox});

        $('html,body').scrollTop(20);
    },
    GetTicketHtml: function(datas) {

        var self = this,
            listHtml = new StringBuilder(),
            tip = "<span>售票已结束</span>"

        for (var i = 0; i < datas.length; i++) {
            var dataInfo = datas[i];
            listHtml.appendFormat("<div class=\"item\" data-val=\"{0}\" data-price=\"{1}\"  >", dataInfo.TicketID, dataInfo.TicketPrice);
            listHtml.appendFormat("<div class=\"list {0} {1} {3} clearfix\" data-explain=\"{2}\">", self.activityInfo.TicketID == dataInfo.TicketID ? 'on' : datas.length <= 1 ? 'on' : '', dataInfo.TicketMore <= 0 ? "not" : '', dataInfo.TicketRemark, dataInfo.TicketPrice ? 'price' : '');
            listHtml.appendFormat("<p class=\"wrap\"><span class=\"money\">{0}</span><span class=\"exp\">{1}</span></p>", dataInfo.TicketPrice ? "<font color=\"#ff3344\">￥" + dataInfo.TicketPrice + "</font>" : "免费", dataInfo.TicketName);
            listHtml.append("<b></b></div>");
            // listHtml.appendFormat("<div class=\"explain\" style=\"display:{0}\" >", self.activityInfo.TicketID == dataInfo.TicketID ? "block" : "none");
            // listHtml.appendFormat("{0} {1}<i></i>", dataInfo.TicketMore > 0 ? "" : tip, dataInfo.TicketRemark);
            // listHtml.append("</div>");
            listHtml.append("</div>");
        };

        return listHtml.toString();
    },
    GetActAdListHtml: function(datas) {



        var adHtml = new StringBuilder(),
            navHtml = new StringBuilder();
        for (var i = 0; i < datas.length; i++) {
            var dataInfo = datas[i];
            adHtml.appendFormat("<li ><img src=\"{0}\"></li>", dataInfo.imageURL);
            navHtml.appendFormat("<dd class=\"{0}\">{1}</dd>", i == 0 ? 'active' : '', (i + 1));
        }
        return {
            adHtml: adHtml.toString(),
            navHtml: navHtml.toString()
        };
    },


    GetControlItem: function(page, controId) {
        var self = this;
        for (var i = 0; i < self.controlDatas.length; i++) {

            if (parseInt(self.controlDatas[i].PageNum) == page) {
                for (var j = 0; j < self.controlDatas[i].Block.length; j++) {
                    var subBlock = self.controlDatas[i].Block[j];
                    for (var p = 0; p < subBlock.Control.length; p++) {
                        var controInfo = subBlock.Control[p];

                        if (controInfo.ControlID == controId) {
                            return controInfo;
                        };
                    };
                };
            };
        };
        return null;

    },
    GetControlInfos: function(pageNumber) {
        var self = this,
            curPages = $('#page' + pageNumber);
        var controList = [];

        $('input,select,textarea', curPages).each(function() {

            if (self.submitError) {
                return;
            };
            var item = $(this),
                controlInfo = {
                    ControlId: item.attr('id'),
                    ColumnName: '',
                    Value: ''
                },
                dataControlInfo = self.GetControlItem(pageNumber, controlInfo.ControlId);


            //获取信息
            switch (parseInt(dataControlInfo.ControlType)) {
                case 1:
                case 6:
                case 9:
                case 10:
                    controlInfo.ColumnName = dataControlInfo.ColumnName;
                    controlInfo.Value = item.val();
                    break;
            }



            //验证是否输入
            if (dataControlInfo.IsMustDo && !controlInfo.Value) {
                self.submitError = true;

                //获取信息
                switch (parseInt(dataControlInfo.ControlType)) {
                    case 1:
                    case 9:
                        self.tips("请输入" + dataControlInfo.ColumnDesc, item);
                        break;
                    case 6:
                        self.tips("请选择" + dataControlInfo.ColumnDesc, item);
                        break;
                }
                return false;

            }

            // 1：文本; 2: 整数；3：小数；4:日期；5：时间；6：邮件 ；7：电话；8：手机；9验证Url网址；10：验证身份

            //验证类型
            switch (parseInt(dataControlInfo.AuthType)) {
                case 1:

                    break;
                case 2:
                    if (controlInfo.Value && !Validates.isInteger(controlInfo.Value)) {
                        self.submitError = true;
                        self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
                        return false;
                    };
                    break;
                case 3:
                    if (controlInfo.Value && !Validates.isDecimal(controlInfo.Value)) {
                        self.submitError = true;
                        self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
                        return false;
                    };
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    if (controlInfo.Value && !Validates.isEmail(controlInfo.Value)) {
                        self.submitError = true;
                        self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
                        return false;
                    };
                    break;
                case 7:
                    if (controlInfo.Value && !Validates.isPhone(controlInfo.Value)) {
                        self.submitError = true;
                        self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
                        return false;
                    };
                    break;
                case 8:
                    if (controlInfo.Value && !Validates.isMobile(controlInfo.Value)) {
                        self.submitError = true;
                        self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
                        return false;
                    };
                    break;
                case 9:
                    break;
                case 10:
                    break;
            }
            controList.push(controlInfo);
        });

        return controList;

    }


});



//获取用户基础信息

function GetlistHtml(pages) {



    //ControlType      类型：1:文本；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；9:超文本,10:密码类型

    if (!pages.length) {
        return ""
    };

    var htmlList = new StringBuilder(),
        hasItem = "<i>*</i> ",
        selectChecked = "selected=selected",
        controlType = 0;

    for (var i = 0; i < pages.length; i++) {
        var pageItem = pages[i];
        // 页面标题
        htmlList.appendFormat("<div data-val=\"{0}\"  style=\"display:{1}\" name=\"pages\" id=\"page" + pageItem.PageNum + "\" > ", pageItem.PageNum, i > 0 ? "none" : "block");

        // if (pageItem.PageName) {
        //     htmlList.appendFormat("<div class=\"topTitle\" ><h3>{0}</h3></div>", pageItem.PageName);
        // };

        //块级元素
        for (var j = 0; j < pageItem.Block.length; j++) {
            var blockItem = pageItem.Block[j];
            if (!blockItem.Control) {
                break;
            };

            htmlList.append("<div class=\"commonBox\"  >");

            for (var p = 0; p < blockItem.Control.length; p++) {
                var controlItem = blockItem.Control[p];
                controlType = parseInt(controlItem.ControlType);

                //支持当前类型
                switch (controlType) {
                    case 1:
                        htmlList.append("<p class=\"commonList\">");
                        htmlList.appendFormat("<em class=\"commonTit\">{0}</em>", controlItem.ColumnDesc);
                        htmlList.appendFormat("<span class=\"wrapInput\">{2}<input id=\"{0}\" maxlength=\"128\"  type=\"{3}\" value=\"{1}\"  ></span>", controlItem.ControlID, controlItem.Value ? controlItem.Value : '', controlItem.IsMustDo ? hasItem : '：', GetInputType(controlItem.AuthType));
                        htmlList.append("</p>");
                        htmlList.append("<p class=\"comline\"></p>");
                        break;
                    case 6:
                        htmlList.append("<p class=\"commonList\">");
                        htmlList.appendFormat("<em class=\"commonTit\">{0}</em><span class=\"wrapInput\">{2}<select id=\"{1}\" >   ", controlItem.ColumnDesc, controlItem.ControlID, controlItem.IsMustDo ? hasItem : '：');

                        for (var v = 0; v < controlItem.Options.length; v++) {
                            var optionItem = controlItem.Options[v];

                            htmlList.appendFormat(" <option value=\"{0}\" {2}>{1}</option>", optionItem.OptionID, optionItem.OptionText, optionItem.IsSelected ? selectChecked : '');

                        };

                        htmlList.append("</select></span></p>");
                        htmlList.append("<p class=\"comline\"></p>");
                        break;

                    case 9:


                        htmlList.append("<div class=\"subtxt\">");
                        htmlList.appendFormat("<div class=\"commonTitle\">{0}{1}</div>", controlItem.ColumnDesc, controlItem.IsMustDo ? hasItem : '：');
                        htmlList.appendFormat("<div class=\"commonArea\"><textarea id=\"{0}\" maxlength=\"500\"  >{1}</textarea></div>", controlItem.ControlID, controlItem.Value ? controlItem.Value : '');
                        htmlList.append("</div>");
                        break;
                    case 10:
                        htmlList.append("<p class=\"commonList\">");
                        htmlList.appendFormat("<em class=\"commonTit\">{0}</em>", controlItem.ColumnDesc);
                        htmlList.appendFormat("<span class=\"wrapInput\">{2}<input id=\"{0}\" maxlength=\"128\"  type=\"password\" value=\"{1}\"  ></span>", controlItem.ControlID, controlItem.Value ? controlItem.Value : '', controlItem.IsMustDo ? hasItem : '：');
                        htmlList.append("</p>");


                        htmlList.append("<p class=\"comline\"></p>");
                        break;
                }

            };


            htmlList.append(" </div>");
        };


        htmlList.append("</div>");
        htmlList.append("</div>");
    };

    //通过验证类型获取文本类型
    function GetInputType(valType) {
        var inputType;
        switch (parseInt(valType)) {
            case 1:
            case 7:
            case 8:
                inputType = "text";
                break;
            case 2:
            case 3:
            case 10:
                inputType = "number";
                break;
            case 4:
                inputType = "date";
                break;
            case 5:
                inputType = "datetime ";
                break;
            case 6:
                inputType = "email";
                break;
            case 9:
                inputType = "url ";
                break;
            default:
                inputType = 'text';

        }

        return inputType;
    }



    return htmlList.toString();
}