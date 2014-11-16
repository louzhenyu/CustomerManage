define(['jquery', 'template', 'dbTemplate', 'tools', 'kkpager', 'artDialog', 'datepicker', 'swffileupload'], function ($) {

    template.openTag = '<#';
    template.closeTag = '#>';

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
            }, 1500);
        }
    }

    var page = {
        ele: {
            section: $("#section"),
            sureTable: $("#sureTable")
            , uiMask: $(".jui-mask")
            , deptSelect: $("#deptSelect")
            , jobFuncSelect: $("#jobFuncSelect")
            , lineManagerSelect: $("#lineManagerSelect")
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
            , sexIcon: $("#sexIcon")
            , txtLineManage: $("#txtLineManage")
            , deptSelectBox: $("#deptSelectBox")
            , idConfirm: $("#idConfirm")
            , confirmMask: $("#confirmMask")

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
            $("#step1").show();
            $("#step2").hide().find(".step02").removeClass("on");
        }
        , hideElements: function (selector) {

            this.ele.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        }
        , init: function () {
            this.url = "/ApplicationInterface/Product/QiXinManage/QiXinManageHandler.ashx";

            this.customerId = "";
            this.userId = "";

            this.currentUserId = $.util.getUrlParam("cUserID")
            if (this.currentUserId == undefined) {
                this.currentUserId = '';
            }

            this.eventId = "4C7346F6D091F806BF1A3DBA42BC10B4";
            //this.status = this.ele.tabMenu.find(".tabItem.on").data("status");

            this.status = "1";
            this.tableMap = {
                "1": this.ele.sureTable
            };

            //表格排序缓存
            this.columnSort = "UserStatus desc";

            //表格标题
            this.columns = {
                //"HeadImageUrl": "头像"
                "UserEmail": "邮箱"
                , "UserName": "姓名"
                // , "UserNameEn": "英文名"
                , "UserGender": "性别"
                , "UserTelephone": "手机"
                , "DeptName": "部门"
                , "JobFuncName": "职务"
                , "UserStatus": "状态"
            };

            //导入文件地址
            this.Path = "";
            //导出操作类型
            this.ExportType = "select";

            this.EventObj = null;

            this.grabed = false;

            this.loadData();
            this.initEvent();
        }
        , loadData: function () {
            this.loadPageList();
            //select下拉
            this.loadDept();
            this.loadSelectDept();
            this.loadJobFunc();
            this.loadUserSelect();
            //加载员工详细信息
            this.loadSingleStaffData();
            //渲染表头
            this.renderTableTitle(this.columns);
        }
        , initEvent: function () {
            var that = this;

            //组织，职务，人员管理切换
            $(".subMenu .clearfix").click(function (e) {
                var $this = $(e.target);
                var locationHref = "";
                if ($this.attr("class") == "nav01 on")
                    locationHref = "Dept.aspx";
                else if ($this.attr("class") == "nav02 on")
                    locationHref = "JobFunc.aspx";
                else if ($this.attr("class") == "nav03 on")
                    locationHref = "";

                if (locationHref != "")
                    window.location.href = locationHref;

                that.stopBubble(e);
            });

            //编辑跳转
            $("#sureTable").delegate("a", "click", function (e) {
                window.location.href = "userEdit.html?UserID=" + $(this).attr("data-id");
                that.stopBubble(e);
            });

            //弹出添加层
            $("#appInfoBtn").click(function (e) {
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

                //self.showElements("#dialogStaffDiv").show();

                $("#dialogStaffDiv").show();

                $(".jui-dialog-tit h2").html("添加员工");
                self.ele.logOutBtn.hide();

                that.stopBubble(e);
            });

            //隐藏添加层
            this.ele.section.delegate("#dialogStaffCancleBtn", "click", function (e) {
                that.ele.uiMask.slideUp();
                $(this).parent().parent().parent().parent().fadeOut();
                that.stopBubble(e);
            }).delegate("#dialogStaffClose", "click", function (e) {
                that.ele.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
                that.stopBubble(e);
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
            $("#importBtn").click(function (e) {
                that.showElements("#importDiv").show();
                that.stopBubble(e);
            });
            //下载模板
            $("#downloadTmpl").click(function (e) {
                //var url = "/ApplicationInterface/NwEvents/NwEventsGateway.ashx?type=Product&action=DownEnrollTpl" + '&req={"Parameters":{"EventId":"' + that.eventId + '",Status:' + that.status + '},' + '"random":' + Math.random() + '}';
                var url = "http://test.o2omarketing.cn:9200/File/QiXinManage/BatchUserTemplate.xls";
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

            this.ele.section.delegate(".listBox", "click", function (e) {
                var $this = $(e.target);
                if ($this.attr("data-val")) {
                    $this.parent().siblings(".textBox").attr("data-val", $this.attr("data-val")).html(e.target.innerText);
                    //切换男女icon
                    self.changeSexIcon($this.parent().parent(), $this.attr("data-val"));
                    //
                    self.changeLeader($this);

                } else {
                    alert("该选项没有id，无法获取值");
                }
            }).delegate("#saveBtn", "click", function () {
                var data = {
                    type: "Product"
                    , customerId: self.customerId
                    , userId: self.userId
                    , UserID: self.currentUserId
                    , UserCode: self.ele.txtUserCode.val()
                    , UserEmail: self.ele.txtUserEmail.val()
                    , UserName: self.ele.txtUserName.val()
                    , UserNameEn: self.ele.txtUserNameEn.val()
                    , UserGender: $(self.ele.ddlUserGender.find(".textBox")[0]).attr("data-val")
                    , UserBirthday: self.ele.txtUserBirthday.val()
                    , UserTelephone: self.ele.txtUserTelephone.val()
                    , UserCellphone: self.ele.txtUserCellphone.val()
                    , LineManagerID: $(self.ele.lineManagerSelect.find("input")).attr("data-val")
                    //, RoleID: $(self.ele.roleSelect.find(".textBox")[0]).attr("data-val")
                    , UnitID: $(self.ele.deptSelect.find(".textBox")[0]).attr("data-val")
                    , JobFunctionID: $(self.ele.jobFuncSelect.find(".textBox")[0]).attr("data-val")
                }

                if (data.UserEmail.length == 0 || !self.checkEmail(data.UserEmail)) {
                    alert("邮箱格式不正确");
                    return false;
                }
                if (data.UserCode.length == 0) {
                    alert("员工编码不能为空");
                    return false;
                }
                if (data.UserName.length == 0) {
                    alert("姓名不能为空");
                    return false;
                }
                if (data.UserID.length == 0) {
                    alert("部门不能为空");
                    return false;
                }
                if (data.JobFunctionID.length == 0) {
                    alert("职务不能为空");
                    return false;
                }
                if (data.UserTelephone.length == 0 || !self.checkMobile(data.UserTelephone)) {
                    alert("手机格式不正确");
                    return false;
                }
                if (data.LineManagerID.length == 0) {
                    alert("直属上级不能为空");
                    return false;
                }
                if (data.UserTelephone.length == 0 || !self.checkMobile(data.UserTelephone)) {
                    alert("手机格式不正确");
                    return false;
                }
                self.saveAction(data);
            }).delegate(".operateWrap .editIcon", "click", function (e) {

                $(".jui-dialog-tit h2").html("编辑员工");
                //self.ele.logOutBtn.show();

                //操作按钮
                var $this = $(e.target);
                var id = $this.parent().siblings(".checkBox").attr("data-id");

                self.currentUserId = id;
                $("#maskDisplay").css("display", "block");
                $("#dialogStaffDiv").show();

                self.loadSingleStaffData();
                //that.stopBubble(e);
            }).delegate(".operateWrap .logoutIcon,#logoutAll", "click", function (e) {
                self.EventObj = $(e.target);
                $(self.ele.confirmMask).css("display", "block");
                $(self.ele.idConfirm).show();
                that.stopBubble(e);
            }).delegate("#selectedAllCurPage", "click", function (e) {

                $("#sureTable tr td.checkBox").addClass("on");
                that.stopBubble(e);

            }).delegate("#selectedAllPage", "click", function (e) {

                $("#sureTable tr td.checkBox").addClass("on");
                that.stopBubble(e);

            }).delegate("#removeSelected", "click", function (e) {
                $("#sureTable tr td.checkBox").removeClass("on");
                that.stopBubble(e);

            }).delegate("#queryBtn", "click", function (e) {
                //查询按钮事件
                self.page.pageIndex = 0;
                self.loadPageList();
                that.stopBubble(e);
            }).delegate("#txtLineManage", "keydown", function () {
                self.loadUserSelect();
            }).delegate("#deptSelectBox.selectBox .selectList p", "click", function (e) {
                //查询面板-部门下拉
                var $this = $(e.target);
                $this.parent().siblings("span").attr("data-val", $this.attr("data-val"))
                    .html($this.html());
                //alert($this.html());
                that.stopBubble(e);
            }).delegate(".imgSort", "click", function (e) {
                var $this = $(e.target);
                if ($this.attr("data-sort") == "asc") {
                    //$this.attr("data-sort", "desc").attr("src", "images/selectIcon01.png");
                    $this.attr("data-sort", "desc").addClass("up");
                } else {
                    //$this.attr("data-sort", "asc").attr("src", "images/selectIcon02.png");
                    $this.attr("data-sort", "asc").removeClass("up");
                }
                self.columnSort = $this.attr("data-id") + " " + $this.attr("data-sort");

                self.loadPageList();
                that.stopBubble(e);
            }).delegate("#exportBtn", "click", function (e) {
                self.ExportStaff();
                that.stopBubble(e);
            }).delegate(".minSelectBox p", "click", function (e) {
                $this = $(e.target);
                if ($this.attr("id") == "selectedAllPage")
                    self.ExportType = "allPage";
                else if ($this.attr("id") == "selectedAllCurPage")
                    self.ExportType = "currentPage";
                else if ($this.attr("id") == "removeSelected")
                    self.ExportType = "noSelect";
                else self.ExportType = "select";
            });

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
                'uploader': "/ApplicationInterface/Product/QiXinManage/QiXinManageHandler.ashx?type=Product&action=UploadFile&req={}",
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

                    $("#uploadText").val(errorString);
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
                    //文件所在地址
                    self.Path = json.Data.Path;
                    //测试
                    //self.ImportUrl = "http://test.o2omarketing.cn:9200/File/QiXinManage/BatchUserTemplate.xls";
                    $("#exportStatus").html("正在执行导入……");
                    self.ExecImport();

                } //上传成功后的方法
            });

            //confirm取消
            $("#logOutCancel").click(function (e) {
                $(self.ele.idConfirm).hide();
                $(self.ele.confirmMask).css("display", "none");
                that.stopBubble(e);
            });
            //confirm确认
            $("#logOutOk").click(function (e) {
                self.logOutStaff(self.EventObj);
                $(self.ele.idConfirm).hide();
                $(self.ele.confirmMask).css("display", "none");
                that.stopBubble(e);
            })
        }
        //导出员工
         , ExportStaff: function () {

             var staff = [];
             var checkArr = $("#sureTable td.checkBox.on");
             for (var i = 0; i < checkArr.length; i++) {
                 staff.push($(checkArr[i]).attr("data-id"));
             }
             var url = self.url + "?type=Product&action=ExportStaff" + '&req={"Parameters":{"PageIndex":' + this.page.pageIndex + ',"PageSize":' + this.page.pageSize + ',"Keyword":"' + $("#searchKeyword").val() + '","UnitID":"' + self.ele.deptSelectBox.find("span").attr("data-val") + '","sort":"' + self.columnSort.toString() + '","ExportType":"' + self.ExportType + '","StaffIds":"' + staff.join(',') + '"},' + '"random":' + Math.random() + '}';
             location.href = url;
         }
        //执行导入
         , ExecImport: function () {
             $.util.ajax({
                 url: this.url
                  , type: "get"
                  , data: {
                      type: "Product"
                      , action: "BatchImportUserList"
                      , Path: self.Path
                  }
                  , success: function (data) {
                      if (data.IsSuccess) {
                          $("#exportStatus").html("导入成功");
                      } else {
                          $("#exportStatus").html(data.Message);
                      }
                  }
             });
         }
        //员工注销
            , logOutStaff: function (obj) {

                var staff = [];
                if ($(obj).attr("id") == "logoutAll") {
                    var checkArr = $("#sureTable td.checkBox.on");
                    for (var i = 0; i < checkArr.length; i++) {
                        staff.push($(checkArr[i]).attr("data-id"));
                    }
                } else {
                    var checkId = $(obj).parent().siblings(".checkBox").attr("data-id");
                    staff.push(checkId);
                }

                //if (!confirm("确定要注销员工吗？")) return;

                self.submitLogOut(staff);
            }
            , submitLogOut: function (data) {
                if (data.length > 0) {
                    $.util.ajax({
                        url: self.url,
                        type: "get",
                        customerId: self.customerId,
                        userId: self.userId,
                        data: {
                            action: "DisableUser",
                            UserID: data.join(",")
                        },
                        success: function (data) {
                            if (data.IsSuccess) {
                                alert("已注销！", true);
                                self.loadPageList();
                            } else {
                                if (page.d) {
                                    page.d.close();
                                }
                                alert(data.Message);
                            }
                        }
                    });
                }
            }
            , delStaff: function (str, callback) {
                var self = this;
                $.util.ajax({
                    url: self.url,
                    type: "get",
                    customerId: self.customerId,
                    userId: self.userId,
                    data: {
                        action: "DelUser",
                        UserID: str
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
            , loadDept: function () {
                $.util.ajax({
                    url: this.url
                    , type: "get"
                    , data: {
                        type: "Product"
                        , action: "GetDept"
                        , customerId: this.customerId
                        , userId: this.userId
                        , PageIndex: 0
                        , PageSize: 1000
                    }
                    , success: function (data) {
                        if (data.IsSuccess) {
                            var list = [];
                            for (var i = 0; i < data.Data.UnitList.length; i++) {
                                list.push({ id: data.Data.UnitList[i].UnitID, name: data.Data.UnitList[i].UnitName });
                            }
                            var temp = $("#selectTemp").html();
                            //debugger;
                            self.ele.deptSelect.html(self.renderSelect(temp, { list: list, idprefix: "dept-item" }));
                            //table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
            , loadSelectDept: function () {
                $.util.ajax({
                    url: this.url
                    , type: "get"
                    , data: {
                        type: "Product"
                        , action: "GetDept"
                        , customerId: this.customerId
                        , userId: this.userId
                        , PageIndex: 0
                        , PageSize: 1000
                    }
                    , success: function (data) {
                        if (data.IsSuccess) {
                            var list = [];
                            list.push({ id: "", name: "全部" });
                            for (var i = 0; i < data.Data.UnitList.length; i++) {
                                list.push({ id: data.Data.UnitList[i].UnitID, name: data.Data.UnitList[i].UnitName });
                            }
                            var temp = $("#selectTemp2").html();
                            //debugger;
                            self.ele.deptSelectBox.html(self.renderSelect(temp, { list: list, idprefix: "dept-item" }));
                            //table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
            , loadJobFunc: function () {
                $.util.ajax({
                    url: this.url
                    , type: "get"
                    , data: {
                        type: "Product"
                        , action: "GetJobFunc"
                        , customerId: this.customerId
                        , userId: this.userId
                        , PageIndex: 0
                        , PageSize: 1000
                    }
                    , success: function (data) {
                        if (data.IsSuccess) {
                            var list = [];
                            var item;
                            for (var i = 0; i < data.Data.JobFuncList.length; i++) {
                                item = data.Data.JobFuncList[i];
                                list.push({ id: item.JobFunctionID, name: item.Name });
                            }
                            var temp = $("#selectTemp").html();
                            //debugger;
                            self.ele.jobFuncSelect.html(self.renderSelect(temp, { list: list, idprefix: "job-item" }));
                            //table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
            , loadUserSelect: function () {
                $.util.ajax({
                    url: this.url
                    , type: "get"
                    , data: {
                        type: "Product"
                        , action: "GetUserDict"
                        , customerId: this.customerId
                        , userId: this.userId
                        , UserName: self.ele.txtLineManage.val()
                        , PageIndex: 0
                        , PageSize: 3000
                    }
                    , success: function (data) {
                        if (data.IsSuccess) {
                            var list = [];
                            var item;
                            for (var i = 0; i < data.Data.UserList.length; i++) {
                                item = data.Data.UserList[i];
                                list.push({ id: item.UserID, name: item.UserName });
                            }
                            var temp = $("#leaderSelectTemp").html();
                            //debugger;
                            self.ele.lineManagerSelect.find(".listBox").html(self.renderSelect(temp, { list: list, idprefix: "line-item" }));
                            //table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
            , loadSingleStaffData: function () {
                if (self.currentUserId) {
                    $.util.ajax({
                        url: this.url,
                        data: {
                            action: "GetUserList",
                            interfaceType: "Product",
                            customerId: this.customerId,
                            userId: this.userId,
                            UserID: self.currentUserId
                        },
                        beforeSend: function () {
                            //self.isSending = true;
                            //$.native.loading.show();
                        },
                        success: function (data) {
                            if (data.IsSuccess) {
                                if (data.Data.UserList != null) {
                                    var singleUser = data.Data.UserList[0];
                                    self.ele.txtUserCode.val(singleUser.UserCode);
                                    self.ele.txtUserName.val(singleUser.UserName);
                                    self.ele.txtUserNameEn.val(singleUser.UserNameEn);
                                    self.ele.txtUserEmail.val(singleUser.UserEmail);
                                    self.ele.txtUserBirthday.val(singleUser.UserBirthday);
                                    self.ele.txtUserTelephone.val(singleUser.UserTelephone);
                                    self.ele.txtUserCellphone.val(singleUser.UserCellphone);
                                    //select
                                    self.setSelectItem("gender-item", self.ele.ddlUserGender, singleUser.UserGender);
                                    //切换男女icon
                                    self.changeSexIcon(self.ele.ddlUserGender, singleUser.UserGender);
                                    self.setLeaderSelect("line-item", self.ele.lineManagerSelect, singleUser.LineManagerID);
                                    self.setSelect(self.ele.deptSelect, singleUser.DeptName);
                                    self.setSelect(self.ele.jobFuncSelect, singleUser.JobFuncName);
                                }
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
            }
            , changeLeader: function (obj, vl) {
                if (obj.parent().parent().attr("id") == "lineManagerSelect") {
                    obj.parent().siblings("input").attr("data-val", obj.attr("data-val")).val(obj.html());
                }
            }
            , changeSexIcon: function (obj, vl) {
                if (obj.attr("id") == "ddlUserGender") {
                    if (vl == 2)
                        $(self.ele.sexIcon).addClass("sexFemale");
                    else
                        $(self.ele.sexIcon).removeClass("sexFemale");
                }
            }
            , setSelectItem: function (prefix, obj, val) {
                var ob = self.ele.section.find("#" + prefix + "-" + val);
                obj.find(".textBox").attr("data-val", ob.attr("data-val")).html(ob.html());
            }
            , setLeaderSelect: function (prefix, obj, val) {
                var ob = self.ele.section.find("#" + prefix + "-" + val);
                obj.find("input").attr("data-val", ob.attr("data-val")).val(ob.html());
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
            , UploadAction: function (obj) {
                $.util.ajax({
                    url: self.url,
                    data: obj,
                    success: function (data) {
                        if (data.IsSuccess) {
                            alert("提交成功！");
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }
            , saveAction: function (obj) {
                if (self.currentUserId == '' || self.currentUserId == '-1') {
                    obj.action = "AddUser";
                    $.util.ajax({
                        url: self.url,
                        data: obj,
                        success: function (data) {
                            if (data.IsSuccess) {
                                //if (callback) {
                                //    callback();
                                //} else {
                                self.currentUserId = data.UserID;
                                alert("提交成功！");
                                //}
                            } else {
                                alert(data.Message);
                            }
                        }
                    });
                } else {
                    obj.action = "ModifyUserPersonalInfo";
                    $.util.ajax({
                        url: self.url,
                        data: obj,
                        success: function (data) {
                            if (data.IsSuccess) {
                                //if (callback) {
                                //    callback();
                                //} else {
                                alert("提交成功！", true);
                                self.loadPageList();

                                $("#maskDisplay").css("display", "none");
                                $("#dialogStaffDiv").hide();

                                //}
                            } else {
                                alert(data.Message);
                            }
                        }
                    });
                }
            }
        //加载更多
            , loadMoreData: function (currentPage) {
                var that = this;
                this.page.pageIndex = currentPage - 1;
                this.getPageList(function (data) {
                    that.renderTable(data.Data, that.columns);
                });
            }
            , loadPageList: function (callback) {
                var that = this;
                this.getPageList(function (data) {

                    that.renderTable(data.Data, that.columns);
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
            }
        //获取数据
            , getPageList: function (callback) {
                $.util.ajax({
                    url: this.url,
                    type: "get",
                    data: {
                        action: "GetUserList",
                        type: "Product",
                        customerId: this.customerId,
                        userId: this.userId,
                        PageIndex: this.page.pageIndex,
                        PageSize: this.page.pageSize
                        , Keyword: $("#searchKeyword").val()
                        , UnitID: self.ele.deptSelectBox.find("span").attr("data-val")
                        , JobFunctionID: ""
                        , sort: self.columnSort.toString()
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
            , renderTableTitle: function (colNames) {
                var table = this.tableMap[this.status];
                var tempHead = this.temp.thead[this.status];
                table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
            }
            , renderTable: function (data, colNames) {
                var table = this.tableMap[this.status];
                var tempHead = this.temp.thead[this.status];
                var tempBody = this.temp.tbody[this.status];
                //table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
                //var headerObj = data.DicColNames;
                var bodyList = data.UserList;
                var headerObj = colNames;
                //var bodyList = dataList;
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

                table.find("tbody").html(self.render(tempBody, { list: { finalList: finalList, otherItems: otherItems } }));

                //this.ele.tabMenu.find(".unsureTable em").html(data.TotalCountUn);
                //this.ele.tabMenu.find(".sureTable em").html(data.TotalCountYet);
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
            , checkMobile: function (str) {
                var re = /^1\d{10}$/
                if (re.test(str))
                    return true;
                else
                    return false;
            }
            , checkEmail: function (str) {
                var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
                if (re.test(str))
                    return true;
                else
                    return false;
            }
    }

    var self = page;

    page.init();
});