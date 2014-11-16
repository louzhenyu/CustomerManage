define(['jquery', 'template', 'dbTemplate', 'tools', 'kkpager', 'artDialog', 'datepicker'], function ($) {

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
            , deptSelectBox: $("#deptSelectBox")
            , txtJobFuncName: $("#txtJobFuncName")
            , txtJobFuncLevel: $("#txtJobFuncLevel")
            , txtJobFuncDesc: $("#txtJobFuncDesc")
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
            this.customerId = ""; // $.util.getUrlParam("customerId"); 
            this.userId = ""; //$.util.getUrlParam("userId");

            this.commId = $.util.getUrlParam("jobid")
            if (this.commId == undefined) {
                this.commId = '';
            }

            this.status = "1";
            this.tableMap = {
                "1": this.ele.sureTable
            };

            //表格排序缓存
            this.columnSort = "Name desc";

            //表格标题
            this.columns = {
                "Name": "职务名称"
                , "Description": "职务描述"
                //, "Status": "状态"
            };

            this.EventObj = null;

            this.grabed = false;

            this.loadData();
            this.initEvent();
        }
      , loadData: function () {
          this.loadPageList();
      }
    , initEvent: function () {
        var that = this;

        //弹出添加层
        $("#appInfoBtn").click(function (e) {
            self.commId = '';
            self.ele.txtJobFuncName.val('');
            self.ele.txtJobFuncDesc.val('');
            self.ele.txtJobFuncLevel.val('');

            $("#maskDisplay").css("display", "block");
            $("#dialogEditDiv").show();

            $(".jui-dialog-tit h2").html("添加职务");
            //self.ele.logOutBtn.hide();

            that.stopBubble(e);
        });

        //隐藏添加层
        this.ele.section.delegate("#cancelBtn", "click", function (e) {
            that.ele.uiMask.slideUp();
            $(this).parent().parent().parent().fadeOut();
            that.stopBubble(e);
        }).delegate("#dialogEditClose", "click", function (e) {
            that.ele.uiMask.slideUp();
            $(this).parent().parent().fadeOut();
            that.stopBubble(e);
        })

        //组织，职务，人员管理切换
        $(".subMenu .clearfix").click(function (e) {
            var $this = $(e.target);
            var locationHref = "";
            if ($this.attr("class") == "nav01 on")
                locationHref = "Dept.aspx";
            else if ($this.attr("class") == "nav02 on")
                locationHref = "";
            else if ($this.attr("class") == "nav03 on")
                locationHref = "Sample.aspx";

            if (locationHref != "")
                window.location.href = locationHref;

            that.stopBubble(e);
        });

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

        this.ele.section.delegate(".operateWrap .editIcon", "click", function (e) {

            //操作按钮
            var $this = $(e.target);
            var id = $this.parent().siblings(".checkBox").attr("data-id");
            self.commId = id;
            $(".jui-dialog-tit h2").html("编辑职务");
            $("#maskDisplay").css("display", "block");
            $("#dialogEditDiv").show();
            self.InitEditData();
            that.stopBubble(e);

        }).delegate(".operateWrap .delIcon,#delSelect", "click", function (e) {
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
        }).delegate(".imgSort", "click", function (e) {
            var $this = $(e.target);
            if ($this.attr("data-sort") == "asc") {
                $this.attr("data-sort", "desc");
            } else {
                $this.attr("data-sort", "asc");
            }
            self.columnSort = $this.attr("data-id") + " " + $this.attr("data-sort");
            self.loadPageList();
            that.stopBubble(e);
        }).delegate("#saveBtn", "click", function () {
            var data = {
                type: "Product"
                , JobFunctionID: self.commId
                , Name: self.ele.txtJobFuncName.val()
                , Description: self.ele.txtJobFuncDesc.val()
                , Level: self.ele.txtJobFuncLevel.val()//未实现
                , Status: "1"//默认1
            }
            if (data.Name.length == 0) {
                alert("职务名称不能为空");
                return false;
            }
            self.saveAction(data);
        });

        //confirm取消
        $("#delCancel").click(function (e) {
            $(self.ele.idConfirm).hide();
            $(self.ele.confirmMask).css("display", "none");
            that.stopBubble(e);
        });
        //confirm确认
        $("#delOk").click(function (e) {
            self.delJobFunc(self.EventObj);
            $(self.ele.idConfirm).hide();
            $(self.ele.confirmMask).css("display", "none");
            that.stopBubble(e);
        });
    }
        //删除
    , delJobFunc: function (obj) {

        var JobFunc = [];
        if ($(obj).attr("id") == "delSelect") {
            var checkArr = $("#sureTable td.checkBox.on");
            for (var i = 0; i < checkArr.length; i++) {
                JobFunc.push($(checkArr[i]).attr("data-id"));
            }
        } else {
            var checkId = $(obj).parent().siblings(".checkBox").attr("data-id");
            JobFunc.push(checkId);
        }
        //return;
        //if (!confirm("确定要删除职务吗？")) return;

        if (JobFunc.length > 0) {
            $.util.ajax({
                url: self.url,
                type: "get",
                data: {
                    action: "DelJobFunc",
                    JobFunctionID: JobFunc.join(",")
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("已删除！", true);
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
    , saveAction: function (obj) {
        if (self.commId == '' || self.commId == '-1') {
            obj.action = "AddJobFunc";
            $.util.ajax({
                url: self.url,
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        self.commId = data.Data.JobFunctionID;
                        alert("提交成功！", true);
                    } else {
                        alert(data.Message);
                    }
                }
            });
        } else {
            obj.action = "ModifyJobFunc";
            $.util.ajax({
                url: self.url,
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("提交成功！", true);
                        self.loadPageList();
                        $("#maskDisplay").css("display", "none");
                        $("#dialogStaffDiv").hide();
                    } else {
                        alert(data.Message);
                    }
                }
            });
        }
    }
    , InitEditData: function () {
        if (self.commId) {
            $.util.ajax({
                url: this.url,
                data: {
                    action: "GetJobFunc",
                    JobFunctionID: self.commId
                },
                beforeSend: function () {
                    //self.isSending = true;
                    //$.native.loading.show();
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (data.Data.JobFuncList != null) {
                            var single = data.Data.JobFuncList[0];
                            self.ele.txtJobFuncName.val(single.Name);
                            self.ele.txtJobFuncDesc.val(single.Description);
                            self.ele.txtJobFuncLevel.val("");
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
    , getPageList: function (callback) {
        $.util.ajax({
            url: this.url,
            type: "get",
            data: {
                action: "GetJobFunc",
                type: "Product",
                PageIndex: this.page.pageIndex,
                PageSize: this.page.pageSize
                , Keyword: $("#searchKeyword").val()
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
    , renderTable: function (data, colNames) {
        var table = this.tableMap[this.status];
        var tempHead = this.temp.thead[this.status];
        var tempBody = this.temp.tbody[this.status];
        table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
        //var headerObj = data.DicColNames;
        var bodyList = data.JobFuncList;
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
    }

    var self = page;

    page.init();
});