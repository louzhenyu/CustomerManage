define(['jquery', 'template', 'bdTemplate', 'tools', 'kkpager', 'kindeditor', 'datetimePicker', 'formtojson', 'commonMethod'], function ($) {

    var KE = window.KindEditor;

    template.openTag = '<#';
    template.closeTag = '#>';

    /* window.alert = function (content, autoHide) {
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
             }, 1000);
         }
     }*/

    var page = {
        ele: {
            section: $("#section"),
            sureTable: $("#sureTable")
            , eventTypeSelect: $("#eventTypeSelect")
            , sponsorSelect: $("#sponsorSelect")
            , statusSelect: $("#statusSelect")
            , ddlUserGender: $("#ddlUserGender")
            , ddlIsGroupCreater: $("#ddlIsGroupCreater")
            , txtUserCode: $("#txtUserCode")
            , txtUserName: $("#txtUserName")
            , txtUserNameEn: $("#txtUserNameEn")
            , txtUserEmail: $("#txtUserEmail")
            , txtUserTelephone: $("#txtUserTelephone")
            , txtUserBirthday: $("#txtUserBirthday")
            , txtUserCellphone: $("#txtUserCellphone")
            , ddlUserGender: $("#ddlUserGender")
            , ddlLineManager: $("#ddlLineManager")
            , logOutBtn: $("#logOutBtn")

            //edit page
            , dynamicFormTable: $("#dynamicFormTable")

            , uiMask: $(".ui-pc-mask")
        }
        , temp: {
            thead: {
                "1": $("#sureTheadTemp").html()
            },
            tbody: {
                "1": $("#sureTbodyTemp").html()
            }
        }
        , page: {
            pageIndex: 0,
            pageSize: 8
        }
        //显示弹层
        , showElements: function (selector) {
            this.ele.uiMask.show();
            $(selector).slideDown();
        }
        , hideElements: function (selector) {

            this.ele.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        }
        , init: function () {
            this.url = "/ApplicationInterface/NwEvents/NwEventsGateway.ashx";
            this.status = "1";
            this.tableMap = {
                "1": this.ele.sureTable,
                "2": this.ele.dynamicFormTable,
            };

            //表格标题
            this.columns = {
                "EventTitle": "活动标题"
                , "Sponsor": "主办方"
                , "BeginTime": "活动开始时间"
                , "EventStatus": "活动状态"
            };

            this.grabed = false;
            //$("#txtUserBirthday").datepicker();

            //初始化日期控件
            $('#beginTime').datetimepicker({
                lang: "ch",
                format: 'Y-m-d',
                timepicker: false
            });
            $('#endTime').datetimepicker({
                lang: "ch",
                format: 'Y-m-d',
                timepicker: false
            });

            //初始化kindeditor
            window.editor = KE.create('#description',
            {
                uploadJson: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx',
                fileManagerJson: '/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true
            });

            this.loadData();
            this.initEvent();
        }
        , buildAjaxParams: function (param) {
            var _param = {
                url: "",
                type: "get",
                dataType: "json",
                data: null,
                beforeSend: function () {

                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            };

            $.extend(_param, param);


            var action = param.data.action,
                interfaceType = param.data.interfaceType || 'Product',
                _req = {
                    'CustomerID': (param.data.customerId ? param.data.customerId : null),
                    'UserID': param.data.userId ? param.data.userId : null,
                    'Parameters': param.data
                };

            delete param.data.customerId;
            delete param.data.userId;
            delete param.data.action;
            delete param.data.interfaceType;

            var _data = {
                'req': JSON.stringify(_req)
            };

            _param.data = _data;

            _param.url = _param.url + '?type=' + interfaceType + '&action=' + action;

            return _param;
        }
        , ajax: function (param) {

            var _param = this.buildAjaxParams(param);

            $.ajax(_param);
        }
        , loadData: function () {
            this.ele.uiMask.show();

            //select下拉
            if (self.ele.eventTypeSelect)
                self.loadEventType(function () {
                    if (self.ele.statusSelect)
                        self.loadStatus(function () {
                            if (self.ele.sponsorSelect)
                                self.loadSponsor(function () {
                                    if (self.ele.sureTable)
                                        self.loadPageList();

                                    if (self.ele.dynamicFormTable)
                                        self.loadDynamicForm(function () {
                                            //载入详细信息
                                            var eventID = $.util.getUrlParam("EventID");
                                            if (eventID)
                                                self.loadEvent(eventID);
                                        });
                                });
                        });
                });
        }
        , initEvent: function () {
            var that = this;
            //CommonMethod.selectedEvent($("#section"));
            $.util.selectEvent("#section");

            //编辑跳转
            $("#sureTable").delegate("a", "click", function (e) {
                window.location.href = "ActivityEdit.aspx?EventID=" + $(this).attr("data-id");
                that.stopBubble(e);
            });

            //弹出添加层
            $("#addBtn").click(function (e) {
                self.currentUserId = '';
                self.ele.txtUserBirthday.val('');
                self.ele.txtUserCode.val('');
                self.ele.txtUserName.val('');
                self.ele.txtUserNameEn.val('');
                self.ele.txtUserEmail.val('');
                self.ele.txtUserTelephone.val('');
                self.ele.txtUserCellphone.val('');

                $(self.ele.lineManagerSelect.find(".textBox")[0]).attr("data-val", "").html("");
                $(self.ele.deptSelect.find(".textBox")[0]).attr("data-val", "").html("");
                $(self.ele.jobFuncSelect.find(".textBox")[0]).attr("data-val", "").html("");

                $("#maskDisplay").css("display", "block");
                $("#dialogStaffDiv").show();

                $(".ui-dialog-tit h2").html("添加员工");
                self.ele.logOutBtn.hide();

                that.stopBubble(e);
            });

            //隐藏添加层
            this.ele.section.delegate("#dialogStaffCancleBtn", "click", function () {
                $("#maskDisplay").css("display", "none");
                $("#dialogStaffDiv").hide();
            }).delegate("#dialogStaffClose", "click", function () {
                $("#maskDisplay").css("display", "none");
                $("#dialogStaffDiv").hide();
            })

            //勾选
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

            //弹出导入层
            $("#importTask").click(function (e) {
                //that.showElements("#importDiv").show();
                $("#importDiv").show();
                //that.stopBubble(e);
            });
            //下载模板
            $("#downloadTmpl").click(function (e) {

                //var url = "/ApplicationInterface/NwEvents/NwEventsGateway.ashx?type=Product&action=DownEnrollTpl" + '&req={"Parameters":{"EventId":"' + that.eventId + '",Status:' + that.status + '},' + '"random":' + Math.random() + '}';
                var url = "http://test.o2omarketing.cn:9100/File/QiXinManage/BatchUserTemplate.xls";
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
                //that.ele.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
                that.stopBubble(e);
            });

            this.ele.section.delegate(".listBox", "click", function (e) {
                var $this = $(e.target);
                if ($this.attr("data-val")) {
                    $this.parent().siblings(".textBox").attr("data-val", $this.attr("data-val")).html(e.target.innerText);
                    //切换男女icon
                    if ($this.parent().parent().attr("id") == "ddlUserGender") {
                        var vl = $this.attr("data-val");
                        if (vl == 2)
                            $this.parent().parent().siblings("p").addClass("sexFemale");
                        else
                            $this.parent().parent().siblings("p").removeClass("sexFemale");
                    }

                } else {
                    alert("该选项没有id，无法获取值");
                }

            }).delegate("#saveBtn", "click", function () {
                self.saveAction();
            }).delegate(".operateWrap .editIcon", "click", function (e) {
                //操作按钮
                var $this = $(e.target);
                var id = $this.parent().siblings(".checkBox").attr("data-id");
                window.location.href = "ActivityEdit.aspx?EventID=" + id;
                that.stopBubble(e);
            })
             .delegate(".operateWrap .delIcon", "click", function (e) {
                 //操作按钮
                 var $this = $(e.target);
                 var id = $this.parent().siblings(".checkBox").attr("data-id");
                 //删除用户
                 if (confirm("确定删除吗？")) {
                     var signupIdArr = [];
                     signupIdArr.push(id);
                     if (signupIdArr.length > 0) {
                         self.delAction(id, function (data) {
                             alert("删除成功！", true);
                             self.loadPageList();
                         });
                     }
                 }
                 that.stopBubble(e);
             }).delegate("#selectedAllCurPage", "click", function () {

                 $("#sureTable tr td.checkBox").addClass("on");
                 that.stopBubble(e);

             }).delegate("#selectedAllPage", "click", function () {

                 $("#sureTable tr td.checkBox").addClass("on");
                 that.stopBubble(e);

             }).delegate("#removeSelected", "click", function () {
                 $("#sureTable tr td.checkBox").removeClass("on");
                 that.stopBubble(e);

             }).delegate("#queryBtn", "click", function (e) {
                 that.page.pageIndex = 0;
                 self.saveQueryData();
                 self.loadPageList();
                 that.stopBubble(e);
             }).delegate("#logOutBtn", "click", function () {

                 self.logOutStaff();
                 that.stopBubble(e);
             });

            //绑定checkbox点击事件
            $(".radioBox").live("click", function (e) {
                //debugger;
                $(this).toggleClass("on");
            });
        }
        , delAction: function (id, callback) {
            var self = this;
            $.util.ajax({
                url: self.url,
                type: "get",
                data: {
                    action: "EventSave",
                    EventEntity: {
                        EventID: id
                        , IsDelete: "1"
                    }
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
        }
        , loadEventType: function (callback) {
            this.ajax({
                url: this.url
                , type: "get"
                , data: {
                    action: "EventTypeList"
                }
                , success: function (data) {
                    if (data.IsSuccess) {
                        CommonMethod.bindSelect(self.ele.eventTypeSelect, data.Data.EventTypeList, "EventTypeID", "Title");
                    } else {
                        alert(data.Message);
                    }

                    if (callback)
                        callback();
                }
            });
        }
        , loadSponsor: function (callback) {
            this.ajax({
                url: this.url
                , type: "get"
                , data: {
                    action: "EventSponsorList"
                }
                , success: function (data) {
                    if (data.IsSuccess) {
                        CommonMethod.bindSelect(self.ele.sponsorSelect, data.Data.EventSponsorList, "OptionValue", "OptionText");
                    } else {
                        alert(data.Message);
                    }
                    if (callback)
                        callback();
                }
            });
        }
        , loadStatus: function (callback) {
            this.ajax({
                url: this.url
                , type: "get"
                , data: {
                    action: "EventStatusList"
                }
                , success: function (data) {
                    if (data.IsSuccess) {
                        CommonMethod.bindSelect(self.ele.statusSelect, data.Data.OptionList, "OptionValue", "OptionText");
                    } else {
                        alert(data.Message);
                    }

                    if (callback)
                        callback();
                }
            });
        }
        , loadDynamicForm: function (callback) {
            this.ajax({
                url: this.url,
                data: {
                    action: "DynamicFormLoad",
                },
                beforeSend: function () {
                    //self.isSending = true;
                    //$.native.loading.show();
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (data.Data.Event && data.Data.Event.FieldList.length > 0)
                        {
                            var fieldList = data.Data.Event.FieldList;
                            var category = [];
                            var field = [];
                            //group by Hierarchy
                            $.each(fieldList, function (i, d) {
                                var ff = { hierarchy: "", data: [] };
                                if ($.inArray(d.Hierarchy, category) == -1) {
                                    category.push(d.Hierarchy);
                                    ff.hierarchy = d.Hierarchy;
                                    ff.data.push(d);
                                    field.push(ff);
                                }
                                else {
                                    $.each(field, function (i, dd) {
                                        if (dd.hierarchy == d.Hierarchy)
                                            dd.data.push(d);
                                    })
                                }
                            });
                            self.renderDynamicForm(field);
                        }

                        if(callback)
                            callback();
                    } else {
                        alert(data.Message);
                    }
                },
                error: function () {
                    alert("加载数据失败！")
                },
                complete: function () {
                    //self.isSending = false;
                    //$.native.loading.hide();
                }
            });
        }
        , loadEvent: function (eventID) {
            this.ajax({
                url: this.url
                , type: "get"
                , data: {
                    action: "EventGet"
                    , EventID : eventID
                }
                , success: function (data) {
                    if (data.IsSuccess) {
                        console.log(data.Data.Event);
                        //filter field whose isused==1
                        if (data.Data.Event.FieldList) {
                            data.Data.Event.FieldList = $.grep(data.Data.Event.FieldList, function (d, i) {
                                return d.IsUsed == "1";
                            });
                        }
                        CommonMethod.bindValue("eventForm", data.Data.Event);
                    } else {
                        alert(data.Message);
                    }
                }
                , complete: function () {
                    //this.ele.uiMask.hide();
                }
            });
        }
        , setSelectItem: function (prefix, obj, val) {
           var ob = self.ele.section.find("#" + prefix + "-" + val);
           obj.find(".textBox").attr("data-val", ob.attr("data-val")).html(ob.html());
       }
        , setSelect: function (obj, val) {
            var objArr = obj.find(".listBox p");
            var ob = obj.find(".textbox");
            for (var i = 0, idata; i < objArr.length; i++) {
                idata = objArr[i]
                if ($(idata).html() == val) {
                    ob = $(idata);
                    break;
                }
            }
            obj.find(".textBox").attr("data-val", ob.attr("data-val")).html(ob.html());
        }
        , saveAction: function () {
            var data = CommonMethod.formToJson("eventForm");
            debugger;
            this.ajax({
                url: self.url,
                data: {
                    action: data.action
                    , EventEntity: data
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("保存成功！");
                    } else {
                        alert(data.Message);
                    }
                }
                , complete: function () {
                    this.ele.uiMask.hide();
                }
            });
        }
        //加载更多
        , loadMoreData: function (currentPage) {
            var that = this;
            this.page.pageIndex = currentPage - 1;
            self.saveQueryData();
            this.getPageList(function (data) {
                that.renderTable(data.Data, that.columns);
            });
        }
        , loadPageList: function (callback) {
            var that = this;
            self.bindQueryData();
            this.getPageList(function (data) {
                that.renderTable(data.Data, that.columns);
                var pageNumber = data.Data.TotalPage;
                if (pageNumber > 1) {
                    var pageNo = 1;
                    if (self.queryData.pageIndex)
                        pageNo = self.queryData.pageIndex + 1;
                    $("#pageContianer").html("<div id='kkpager' style='text-align:center'></div>");
                    kkpager.generPageHtml({
                        pno: pageNo,
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
        }
        //获取数据
        , getPageList: function (callback) {
            var data = {
                action: "EventList",
                EventTypeID: $("#eventTypeSelect .selected").attr("data-val"),
                Sponsor: $("#sponsorSelect .selected").attr("data-val"),
                EventStatus: $("#statusSelect .selected").attr("data-val"),
                EventTitle: $("#eventTitle").val(),
                BeginTime: $("#beginTime").val(),
                EndTime: $("#endTime").val(),
                PageIndex: this.page.pageIndex,
                PageSize: this.page.pageSize
            };

            this.ajax({
                url: this.url,
                type: "get",
                data: data,
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
        }
        , renderTable: function (data, colNames) {
            var table = this.tableMap[this.status];
            var tempHead = this.temp.thead[this.status];
            var tempBody = this.temp.tbody[this.status];
            table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
            //var headerObj = data.DicColNames;

            var bodyList = data.EventList;
            var headerObj = colNames;

            //对应列名的对象    //未和列名对应的对象
            var finalList = [], otherItems = [];
            if (bodyList) {
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
            }

            table.find("tbody").html(self.render(tempBody, { list: { finalList: finalList, otherItems: otherItems } }));
        }
        , render: function (temp, data) {
            var render = bd.template(temp, data);
            return render;
        }
        , renderSelect: function (temp, data) {
            //var render = bd.template(temp, data);
            //return render;
            var render = template.compile(temp);
            return render(data || {});
        }
        , stopBubble: function (e) {
            if (e && e.stopPropagation) {
                //因此它支持W3C的stopPropagation()方法 
                e.stopPropagation();
            }
            else {
                //否则，我们需要使用IE的方式来取消事件冒泡 
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        }
        , renderDynamicForm: function (data) {
            var table = this.tableMap["2"];
            var tempBody = $("#dynamicFormTableTemp").html();
            table.find("tbody").html(self.render(tempBody, { list: data }));
        }
        , queryData: {}
        , saveQueryData: function () {
            var queryData = CommonMethod.formToJson("queryForm");
            queryData.pageIndex = this.page.pageIndex;
            self.queryData = queryData;
            location.hash = "_saveData_=" + encodeURIComponent(JSON.stringify(queryData));
        }
        , bindQueryData: function () {
            var queryData = location.hash;

            if (queryData) {
                queryData = JSON.parse(decodeURIComponent(queryData.replace("#_saveData_=", "")));
                self.queryData = queryData;
                CommonMethod.bindValue("queryForm", queryData);

                if (this.page.pageIndex == 0)
                    this.page.pageIndex = queryData.pageIndex;
            }
        }
    };

    self = page;

    page.init();
});