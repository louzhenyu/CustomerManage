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
            , txtDeptCode: $("#txtDeptCode")
            , txtDeptName: $("#txtDeptName")
            , txtLeader: $("#txtLeader")
            , txtDeptDesc: $("#txtDeptDesc")
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
            this.customerId = "3084cd58e4144cceb1faaac1c515f151"; // $.util.getUrlParam("customerId"); 
            this.userId = "fec9eccd2be748c590a57aee841f06dc"; //$.util.getUrlParam("userId");

            this.commId = $.util.getUrlParam("deptId");
            if (this.commId == undefined) {
                this.commId = '';
            }

            this.status = "1";
            this.tableMap = {
                "1": this.ele.sureTable
            };

            //表格排序缓存
            this.columnSort = "UnitName desc";

            //表格标题
            this.columns = {
                "UnitCode": "部门编码"
                , "UnitName": "部门名称"
                , "Leader": "负责人"
                , "DeptDesc": "描述"
            };

            this.EventObj = null;

            this.grabed = false;

            this.loadData();
            this.initEvent();

            this.tipHtml = $("#dvEditMessage").html()//将弹层的html保存到变量中
            //移除页面中的tip，方便弹层的操作。
            $("#dvEditMessage").remove();

        }
        , loadData: function () {
            this.loadPageList();
            //select下拉
            //this.loadSelectJobFunc();
        }
        , initEvent: function () {
            var that = this;

            //弹出添加层
            $("#appInfoBtn").click(function (e) {
                self.commId = '';
                self.ele.txtDeptCode.val('');
                self.ele.txtDeptName.val('');
                self.ele.txtLeader.val('');
                self.ele.txtDeptDesc.val('');

                var params = {};
                params.title = "添加职务";
                params.action = "AddDept";
                self.editModal(params);

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

            //组织，部门，人员管理切换
            $(".subMenu .clearfix").click(function (e) {
                var $this = $(e.target);
                var locationHref = "";
                if ($this.attr("class") == "nav01 on")
                    locationHref = "";
                else if ($this.attr("class") == "nav02 on")
                    locationHref = "JobFunc.aspx";
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
                var params = {};
                params.id = id;
                params.title = "编辑部门";
                params.action = "ModifyDept";
                self.editModal(params);

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
            }).delegate("#deptSelectBox.selectBox .selectList p", "click", function (e) {
                //查询面板-下拉
                var $this = $(e.target);
                $this.parent().siblings("span").attr("data-val", $this.attr("data-val"))
                    .html($this.html());
                //alert($this.html());
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
                    , UnitID: self.commId
                    , UnitCode: self.ele.txtDeptCode.val()
                    , UnitName: self.ele.txtDeptName.val()
                    , DeptDesc: self.ele.txtDeptDesc.val()
                    , Leader: self.ele.txtLeader.val()
                }
                if (data.UnitCode.length == 0) {
                    alert("部门编码不能为空");
                    return false;
                }
                if (data.UnitName.length == 0) {
                    alert("部门名称不能为空");
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
                self.delDept(self.EventObj);
                $(self.ele.idConfirm).hide();
                $(self.ele.confirmMask).css("display", "none");
                that.stopBubble(e);
            });
        }
        , editModal: function (obj) {//编辑模板
            var d = dialog({
                width: 500,
                title: obj.title,
                content: self.tipHtml,
                fixed: true
            });

            d.showModal();
            //debugger;
            //编辑
            if (obj.id) {
                //debugger;
                self.InitEditData(obj.id);
            }

            $(".addBtn").click(function () {

                var params = {
                    type: "Product"
                    , action: obj.action
                    , UnitID: obj.id
                    , UnitCode: $("#txtDeptCode").val()
                    , UnitName: $("#txtDeptName").val()
                    , DeptDesc: $("#DeptDesc").val()
                    , Leader: $("#txtLeader").val()
                };

                if (params.UnitCode.length == 0) {
                    alert("部门编码不能为空");
                    return;
                }
                if (params.UnitName.length == 0) {
                    alert("部门名称不能为空");
                    return;
                }
                self.saveAction(params);
                close();
            });
            $(".cancelBtn").click(function () {
                close();
            });
            var close = function () {
                d.close().remove();
            }
        }
        //删除
         , delDept: function (obj) {

             var dept = [];
             if ($(obj).attr("id") == "delSelect") {
                 var checkArr = $("#sureTable td.checkBox.on");
                 for (var i = 0; i < checkArr.length; i++) {
                     dept.push($(checkArr[i]).attr("data-id"));
                 }
             } else {
                 var checkId = $(obj).parent().siblings(".checkBox").attr("data-id");
                 dept.push(checkId);
             }

             //return;

             //if (!confirm("确定要删除部门吗？")) return;

             if (dept.length > 0) {
                 $.util.ajax({
                     url: self.url,
                     type: "get",
                     data: {
                         action: "DelDept",
                         UnitID: dept.join(','),
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
            $.util.ajax({
                url: self.url,
                data: obj,
                success: function (data) {
                    if (data.IsSuccess) {
                        self.commId = data.Data.UnitID;
                        alert("提交成功！", true);
                    } else {
                        alert(data.Message);
                    }
                }
            });
        }
     , InitEditData: function (id) {
         if (id) {
             $.util.ajax({
                 url: this.url,
                 data: {
                     action: "GetDept",
                     UnitID: id
                 },
                 beforeSend: function () {
                     //self.isSending = true;
                     //$.native.loading.show();
                 },
                 success: function (data) {
                     if (data.IsSuccess) {
                         if (data.Data.UnitList != null) {
                             var single = data.Data.UnitList[0];
                             self.ele.txtDeptCode.val(single.UnitCode);
                             self.ele.txtDeptName.val(single.UnitName);
                             self.ele.txtDeptDesc.val(single.DeptDesc);
                             self.ele.txtLeader.val(single.Leader);
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
     , loadSelectJobFunc: function () {
         this.ajax({
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
                     list.push({ id: "", name: "全部" });
                     var item;
                     for (var i = 0; i < data.Data.JobFuncList.length; i++) {
                         item = data.Data.JobFuncList[i];
                         list.push({ id: item.JobFunctionID, name: item.Name });
                     }
                     var temp = $("#selectTemp2").html();
                     //debugger;
                     self.ele.deptSelectBox.html(self.renderSelect(temp, { list: list, idprefix: "job-item" }));
                     //table.find("thead").html('<tr class="title"></tr>').find(".title").html(self.render(tempHead, { obj: colNames }));
                 } else {
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
                     action: "GetDept",
                     type: "Product",
                     customerId: this.customerId,
                     userId: this.userId,
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
             var bodyList = data.UnitList;
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