define(['jquery', 'template', 'tools', 'kkpager', 'swffileupload', 'artDialog'], function ($) {
    window.alert = function (content, autoHide) {
        var d = dialog({
            title: '提示',
            cancelValue: '关闭',
            skin: "facebook",
            content: content
        });
        page.d = d;
        d.showModal();
        if (autoHide) {
            setTimeout(function () {
                page.d.close();
            }, 2000);
        }
    }
    var page = {
        ele: {
            section: $("#section"),
            tabMenu: $("#tabMenu"),
            sureTable: $("#sureTable"),
            unsureTable: $("#unsureTable"),
            uiMask: $("#ui-mask")
        },
        temp: {
            thead: {
                "1": $("#unsureTheadTemp").html(),
                "10": $("#sureTheadTemp").html()
            },
            tbody: {
                "1": $("#unsureTbodyTemp").html(),
                "10": $("#sureTbodyTemp").html()
            }
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        //显示弹层
        showElements: function (selector) {
            this.ele.uiMask.show();
            $(selector).slideDown();
            //Add By Alan 14-08-12
            $("#step1").show();
            $("#step2").hide().find(".step02").removeClass("on");
            that.stopBubble(e);
        },
        hideElements: function (selector) {

            this.ele.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        },
        init: function () {
            this.eventId = "16856b2950892b62473798f3a88ee3e3";
            this.status = this.ele.tabMenu.find(".tabItem.on").data("status");
            this.tableMap = {
                "10": this.ele.sureTable,
                "1": this.ele.unsureTable
            };
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.loadPageList();
        },
        initEvent: function () {
            var that = this;
            $('#file_upload').uploadify({
                'buttonText': '', // 设置按钮上显示的文本
                'buttonClass': "",
                'height': 39,     //flash高度
                'progressData': 'percentage',
                'formData': {
                    type: "Product",
                    action: "ImportEnrollData",
                    rr: Math.random(),
                    req: JSON.stringify({
                        Parameters: {
                            EventId: that.eventId,
                            Status: that.status,
                            Err: 123
                        }
                    })
                },
                method: "Post",
                'uploader': "/ApplicationInterface/NwEvents/NwEventsGateway.ashx?type=Product&action=ImportEnrollData",
                'swf': '/Framework/swfuploadfile/uploadify.swf',
                'cancelImage': '/Framework/swfuploadfile/uploadify-cancel.png',
                'auto': true, //是否自动上传
                'width': 490,
                onUploadStart: function (file) {
                    $("#file_upload").uploadify("settings", "formData", {
                        req: JSON.stringify({
                            Parameters: {
                                EventId: that.eventId,
                                Status: that.status
                            }
                        })
                    });
                },
                'onSelect': function (file) {
                    var that = this;

                    $(".uploadify-queue-item").css("width", 490).show();
                    $(".uploadify-button").show();
                    $(".uploadify-queue-item").css("width", 490);
                }, // 选择文件时触发的方法 
                'onUploadError': function (file, errorCode, errorMsg, errorString) {

                    //$("#uploadText").val(errorString);
                }, //上传出错后的方法
                'onUploadSuccess': function (file, data, response) {
                    var json = JSON.parse(data);
                    debugger;
                    if (json.IsSuccess && json.ResultCode == 0) {
                        $(".uploadify-queue-item").css("width", 40).hide();
                        $(".uploadify-button").hide();
                        $(".uploadify").hide();
                    }
                    //alert(file.name + "==" + data + "==" + response + "  上传成功！");
                    $("#uploadText").val(file.name + " 上传成功!");
                } //上传成功后的方法
            });
            //弹出导入层
            $("#importBtn").click(function (e) {
                that.showElements("#importDiv");
                that.stopBubble(e);
            });
            //下载模板
            $("#downloadTmpl").click(function (e) {

                var url = "/ApplicationInterface/NwEvents/NwEventsGateway.ashx?type=Product&action=DownEnrollTpl" + '&req={"Parameters":{"EventId":"' + that.eventId + '",Status:' + that.status + '},' + '"random":' + Math.random() + '}';
                setTimeout(function (e) {
                    $("#step1").hide();
                    $("#step2").show().find(".step02").addClass("on");

                }, 1000);

                location.href = url;
                that.stopBubble(e);

            });
            //完成上传
            $("#comitUpload").click(function (e) {
                //关闭层
                $(".hintClose").trigger("click");
                //刷新页面
                that.loadPageList();
                that.stopBubble(e);
            });
            //返回第一步
            $("#backStep").click(function (e) {
                $("#step1").show();
                $("#step2").hide().find(".step02").removeClass("on");
                that.stopBubble(e);
            });
            //下一步
            $("#nextStep").click(function (e) {
                $("#step1").hide();
                $("#step2").show().find(".step02").addClass("on");
                $(".uploadify-queue-item").css("width", 490).show();
                $(".uploadify-button").show();
                $(".uploadify").show();
                $("#uploadText").val("");
                $("#file_upload-queue").val("");
                that.stopBubble(e);
            });
            $(".subMenu").delegate("li", "click", function (e) {
                var $t = $(this);
                $t.addClass("on").siblings().removeClass("on");
                setTimeout(function () {
                    var theUrl = $t.data("href");
                    if (theUrl != "") {
                        location.href = theUrl;
                    }
                }, 500);
                that.stopBubble(e);
            });
            //鼠标移上去效果
            $(".subMenu li").mouseover(function (e) {
                var $t = $(this);
                if (!$t.hasClass("on")) {
                    $t.addClass("on");
                    that.moveDom = $t;
                }
                that.stopBubble(e);
            }).mouseout(function (e) {
                if (that.moveDom && that.moveDom.hasClass("on")) {
                    that.moveDom.removeClass("on");
                }
                that.stopBubble(e);
            });
            //关闭弹出层
            $(".hintClose").bind("click", function (e) {
                that.ele.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
                that.stopBubble(e);
            });
            //失去焦点进行设置
            $("#sureTable").delegate(".group", "blur", function (e) {
                var $t = $(this);
                var groupName = $t.val();
                if (groupName == "") {
                    return;
                }
                that.page.groupName = groupName;
                that.page.signUpId = $t.data("id");
                that.setGroupName(function (data) {
                    self.loadPageList();
                });
                that.stopBubble(e);
            });
            this.ele.section.delegate("#sendMessageBtn", "click", function (e) {

                var signupIdArr = [];
                $("#sureTable tr td.checkBox.on").each(function (i, t) {
                    signupIdArr.push(t.getAttribute("data-id"));
                });
                if (signupIdArr.length) {
                    alert("短信发送中...请稍后..");
                    self.sendMessageAction(signupIdArr.join(","), function (data) {
                        if (self.d) {
                            self.d.close();
                        }
                        alert("短信发送成功！", true);
                        self.loadPageList();
                    });
                } else {
                    alert("至少选择一项发送通知");
                }
                that.stopBubble(e);
            });

            this.ele.tabMenu.delegate(".tabItem", "click", function (e) {
                var $this = $(this);

                self.page.pageIndex = 0;

                $this.addClass("on").siblings().removeClass("on");
                self.status = $this.data("status");
                $(".dataTable").hide();
                $("#" + $this.data("table")).show();
                self.loadPageList();
                if ($this.data("table") == "sureTable") {
                    $("#sendMessageBtn").show();
                } else {
                    $("#sendMessageBtn").hide();
                }
                that.stopBubble(e);
            });

            this.ele.unsureTable.delegate(".signUpBtn", "click", function (e) {
                var $this = $(this);
                if ($this.attr('data-id')) {
                    self.signUpAction($this.attr('data-id'));
                } else {
                    alert("获取不到该条数据的signUpId");
                }
                that.stopBubble(e);
            });
            this.ele.sureTable.delegate("th.checkBox", "click", function (e) {
                var $this = $(this),
                    td = $this.parents(".dataTable").find("tr td.checkBox");
                $this.toggleClass("on");
                if ($this.hasClass("on")) {
                    td.addClass("on");
                } else {
                    td.removeClass("on");
                }
                that.stopBubble(e);
            }).delegate("td.checkBox", "click", function (e) {
                $(this).toggleClass("on");
                that.stopBubble(e);
            });
        },
        sendMessageAction: function (str, callback) {
            var self = this;
            $.util.ajax({
                url: "/ApplicationInterface/NwEvents/NwEventsGateway.ashx",
                type: "get",
                customerId: "86a575e616044da3ac2c3ab492e44445",
                userId: "004852E9-7AA1-4C3F-97A3-361B8EA96464",      //实际用时去掉  接口示例有，没有会报错，实际用时去掉
                data: {
                    action: "NotifyEventSignUp",
                    SignUpIds: str
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        }
                    } else {
                        if (page.d) {
                            page.d.close();
                        }
                        alert(data.Message);
                    }
                }
            });
        },
        signUpAction: function (id, callback) {
            $.util.ajax({
                url: "/ApplicationInterface/NwEvents/NwEventsGateway.ashx",
                type: "get",
                customerId: "86a575e616044da3ac2c3ab492e44445",
                userId: "004852E9-7AA1-4C3F-97A3-361B8EA96464",      //实际用时去掉  接口示例有，没有会报错，实际用时去掉
                data: {
                    action: "SetEventSignUp",
                    SignUpId: id
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            if (page.d) {
                                page.d.close();
                            }
                            alert("确认成功！", true);
                            self.loadPageList();
                        }

                    } else {
                        if (page.d) {
                            page.d.close();
                        }
                        alert(data.Message);
                    }
                }
            });
        },
        //加载更多
        loadMoreData: function (currentPage) {
            var that = this;
            this.page.pageIndex = currentPage - 1;
            this.getPageList(function (data) {
                that.renderTable(data.Data);
            });
        },
        loadPageList: function (callback) {
            var that = this;
            this.getPageList(function (data) {

                that.renderTable(data.Data);
                var pageNumber = data.Data.TotalPage;
                if (pageNumber > 1) {
                    $("#pageContianer").html("<div id='kkpager' style='text-align:center'></div>");
                    kkpager.generPageHtml({
                        pno: 1,
                        mode: 'click', //设置为click模式
                        //总页码  
                        total: data.Data.TotalPage,
                        isShowTotalPage: false,
                        isShowTotalRecords: false,
                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                        //适用于不刷新页面，比如ajax
                        click: function (n) {
                            //这里可以做自已的处理
                            //...
                            //处理完后可以手动条用selectPage进行页码选中切换
                            this.selectPage(n);
                            //让  tbody的内容变成加载中的图标
                            var table = that.tableMap[that.status];
                            var length = table.find("thead th").length;
                            table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                            that.loadMoreData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);
                } else {
                    $('#kkpager').remove();
                }
            });
        },
        //获取数据
        getPageList: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/NwEvents/NwEventsGateway.ashx",
                type: "get",
                data: {
                    action: "GetEventSignUp",
                    EventId: this.eventId,
                    Status: this.status,
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        if (page.d) {
                            page.d.close();
                        }
                        alert(data.Message);
                    }
                }
            });
        },
        //设置分组
        setGroupName: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/NwEvents/NwEventsGateway.ashx",
                type: "post",
                data: {
                    action: "SetSignUpGroup",
                    SignUpId: this.page.signUpId,
                    GroupName: this.page.groupName
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        if (page.d) {
                            page.d.close();
                        }
                        alert(data.Message);
                    }
                }
            });
        },
        stopBubble: function (e) {
            if (e && e.stopPropagation) {
                //因此它支持W3C的stopPropagation()方法 
                e.stopPropagation();
            }
            else {
                //否则，我们需要使用IE的方式来取消事件冒泡 
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        },
        renderTable: function (data) {
            var table = this.tableMap[this.status];
            var tempHead = this.temp.thead[this.status];
            var tempBody = this.temp.tbody[this.status];
            table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: data.DicColNames }));
            var headerObj = data.DicColNames;
            var bodyList = data.SignUpList;
            //对应列名的对象    //未和列名对应的对象
            var finalList = [], otherItems = [];
            for (var i = 0; i < bodyList.length; i++) {
                var obj = {}, obj2 = {}, item = bodyList[i];
                for (var key in headerObj) {
                    obj[key] = item[key];
                }
                //把没有这个key的 给取出来
                for (var key2 in item) {
                    if (!headerObj.hasOwnProperty(key2)) {
                        obj2[key2] = item[key2];
                    }
                }
                otherItems.push(obj2);
                finalList.push(obj);
            }

            table.find("tbody").html(self.render(tempBody, { list: { finalList: finalList, otherItems: otherItems} }));

            this.ele.tabMenu.find(".unsureTable em").html(data.TotalCountUn);
            this.ele.tabMenu.find(".sureTable em").html(data.TotalCountYet);
        },
        render: function (temp, data) {
            var render = bd.template(temp, data);
            return render;
        }
    };

    self = page;

    page.init();
});