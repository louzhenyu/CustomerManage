Jit.AM.defindPage({
    name: 'AddUserInfo',
    elements: {
        btSubmitInfo: '',
        infoList: ''
    },
    controlDatas: '',
    submitError: false,
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
        self.elements.infoList = $('#infoList');
        self.elements.btSubmitInfo = $('#btSubmitInfo');


        //加载用户信息

        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getUserDefinedByUserID',
                TypeID: '1',
                'RoleID': 'CE1FF5BE7B6A4B0E8ACF5E2501B43D78'
            },
            beforeSend: function() {
                UIBase.loading.show();
            },
            success: function(data) {
                UIBase.loading.hide();

                self.elements.infoList.empty();
                if (data && data.code == 200 && data.content.pageList.length > 0) {
                    self.elements.infoList.html(GetlistHtml(data.content.pageList));
                    self.controlDatas = data.content.pageList;

                    //修正样式
                    $('.commonBox .comline').last().addClass('curline');
                } else {
                    self.elements.infoList.html("很抱歉，没有加载到你的配置信息。");
                }
            }
        });

    }, //绑定事件
    initEvent: function() {
        var self = this,
            submitPage = 0;

        //提交数据
        self.elements.btSubmitInfo.bind('click', function() {
            self.submitError = false; //初始化验证
            var pages = $('div[name=pages]');
            var curPage = pages.eq(submitPage),
                nextPage = curPage.next();
            var controList = self.GetControlInfos(curPage.data(KeyList.val));
            if (self.submitError) {
                return false;
            };

            self.ajax({
                url: '/dynamicinterface/data/data.aspx',
                data: {
                    'action': 'submitUserInfo',
                    Control: controList
                },
                // beforeSend: function() {
                // },
                success: function(data) {
                    if (data && data.code == 200) {
                        var baseInfo = self.getBaseInfo();
                        baseInfo.userId = data.content.userId;
                        Jit.AM.setBaseAjaxParam(baseInfo);
                        localStorage.setItem(valKeyList.userId, baseInfo.userId); //设置缓存

                        if (!nextPage.size()) {
                            self.toPage('Home');
                            return false;
                        } else {
                            submitPage++
                        }
                        curPage.hide();
                        nextPage.show();
                        if ((submitPage + 1) >= pages.length) {
                            self.elements.btSubmitInfo.html('提交');

                        };
                        $('html,body').scrollTop(0);
                    } else {
                        self.tips(data.description);
                    }
                }
            });



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

        if (pageItem.PageName) {
            htmlList.appendFormat("<div class=\"topTitle\" >{0}</div>", pageItem.PageName);
        };

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

    //1：文本; 2: 整数；3：小数；4:日期；5：时间； 6：邮件 ；7：电话；8：手机；9验证Url网址；10：验证身份证

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