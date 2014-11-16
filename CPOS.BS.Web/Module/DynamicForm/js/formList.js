define(['jquery', 'template', 'tools', 'pagination'], function ($, temp) {
    template.openTag = '<$';
    template.closeTag = '$>';

    var page = {
        ele: {
            section: $("#section"),
            formList: $("#formList"),
            createFormLayer:$("#createFormLayer")
        },
        page: {
            pageIndex: 0,
            pageSize: 5
        },
        init: function () {
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.loadPageList();
        },
        initEvent: function () {
            this.ele.section.delegate(".listItem", "click", function () {
                var formId= $(this).attr("data-formid");
                if (formId) {
                    var myMid = JITMethod.getUrlParam("mid");
                    window.location.href = "formEdit.aspx?formId=" + formId + "&mid=" + myMid;
                }else{
                    alert("获取不到formId");
                }

            }).delegate("#createFormBtn", "click", function () {
                self.createFormLayer.show();
            }).delegate(".delBtn", "click", function (e) {
                $.util.stopBubble(e);
                var item = $(this).parents(".listItem");
                var formId= $(this).attr("data-formid");
                if(formId){
                    if(confirm("确认删除本条表单？")){
                        self.deleteForm(formId,function(){
                            alert("删除成功");
                            if(item.index()==0&&item.siblings().length==0){
                                self.page.pageIndex = self.page.pageIndex==0?self.page.pageIndex:self.page.pageIndex-1;
                                self.loadPageList();
                            }else{
                                item.remove();
                            }
                        });
                    }

                }else{
                    alert("获取不到formId");
                }
            });

            this.ele.createFormLayer.delegate(".jsSubmitBtn", "click", function () {
                var name = self.ele.createFormLayer.find(".jsFormName").val();
                self.createForm(name,function(){
                    self.page.pageIndex = 0;
                    self.loadPageList();
                    self.createFormLayer.hide();
                });
            }).delegate(".jsCancelBtn", "click", function () {
                self.createFormLayer.hide();
            })

        },
        createForm:function(name,callback){
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                type:"get",
                data: {
                    action: "DynamicVipFormCreate",
                    Name:name
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if(callback){
                            callback();
                        }else{
                            self.page.pageIndex = 0;
                            self.loadPageList();
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });

        },
        deleteForm:function(formId,callback){
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                type:"get",
                data: {
                    action: "DynamicVipFormDelete",
                    FormID:formId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if(callback){
                            callback(data.Data);
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageList: function (callback) {
            $.util.ajax({
                url: "/ApplicationInterface/DynamicVipForm/DynamicVipFormEntry.ashx",
                type:"get",
                data: {
                    action: "DynamicVipFormList",
                    PageIndex: this.page.pageIndex,
                    PageSize: this.page.pageSize
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            self.renderPageList(data.Data.FormList);

                            //debugger
                            // 分页处理 begin
                            var pageNumber = Math.ceil(data.Data.TotalCount / self.page.pageSize);
                            if (pageNumber > 1) {

                                var pageStr='   <div class="pagination">\
                                                    <a href="javascript:;" class="previous" data-action="previous">&lsaquo;</a>\
                                                    <input type="text" readonly="readonly" />\
                                                    <a href="javascript:;" class="next" data-action="next">&rsaquo;</a>\
                                                </div>';
                                self.ele.section.find('.pageWrap').html(pageStr).show();
                                self.ele.section.find('.pagination').jqPagination({
                                    current_page: self.page.pageIndex + 1,
                                    max_page: pageNumber,
                                    paged: function (page) {
                                        self.page.pageIndex = page - 1;
                                        self.loadPageList();
                                    }
                                });

                            } else {
                                self.ele.section.find('.pageWrap').empty().hide();
                            }
                            // 分页处理 end
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        createFormLayer:{
            show:function(){
                this.reset();
                self.ele.createFormLayer.show();
            },
            hide:function(){
                self.ele.createFormLayer.hide();
            },
            reset:function(){
                self.ele.createFormLayer.find(".jsFormName").val("");
            }
        },
        renderPageList: function (list) {
            var temp = $("#formListTemp").html();
            //if(this.page.pageIndex==0){
                this.ele.formList.html(this.render(temp,{list:list}));
            //}else{
            //    this.ele.formList.append(this.render(temp,{list:list}));
            //}
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});