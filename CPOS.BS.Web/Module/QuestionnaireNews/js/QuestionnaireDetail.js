define(['jquery', 'template', 'tools', "jqueryui", 'langzh_CN', 'easyui', 'kkpager', 'kindeditor'], function ($) {
    //上传图片
    KE = KindEditor;
    var page = { 
        elems: {
            sectionPage:$("#section"), 
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('#operation'),              //弹出框操作部分
            dataMessage: $(".dataMessage"),
            colorplan: $(".colorplan"),             //调色板
            click:true,
            panlH: 116,                        // 下来框统一高度
            domain: "",
            type: 1,  //问卷类型  1,问答2,投票3,测试4,报名
            QuestionnaireID:"",
            isEditOption: false,  //是否在编辑题目
            submitstate: false,  //添加描述提交状态
            isregisterdescribeImg: false,  //是否注册了描述上传图
            Timer: null,
            Timercount: 1
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            debugger;
            this.elems.type = $.util.getUrlParam("type");
            this.elems.QuestionnaireID = $.util.getUrlParam("QuestionnaireID");

            if (!this.elems.QuestionnaireID)
            {
                this.elems.QuestionnaireID = "";
            }

            if (this.elems.type!=null)
                $("#QuestionnaireType").val(this.elems.type);

            if (this.elems.QuestionnaireID != null)
                $("#QuestionnaireID").val(this.elems.QuestionnaireID);
           

            window.onbeforeunload = function ()
            {
                return "还有编辑的数据未保存是否要离开此页面？";
            }
           

            this.initEvent();
            this.loadPageData();


        },
        initEvent: function () {
            var that = this;

            


            //调色板事件start
            that.elems.colorplan.on("click", ".writecolor", function (e) {

                var type = $(this).parents(".color_plan").data("type");

                if (type == 1) {
                    $(".startbtn").attr("style", "background-color:#FFFFFF;color:#000;");
                    $(".regular").css("color", "#FFFFFF");
                    $("#StartPageBtnBGColor").val("#FFFFFF");
                    $("#StartPageBtnTextColor").val("#000");
                }

                if (type == 2)
                {
                    $(".Endbtn").attr("style", "background-color:#FFFFFF;color:#000;");
                    $("#QResultBGColor").val("#FFFFFF");
                    $("#QResultBtnTextColor").val("#000");
                }
            });

            that.elems.colorplan.on("click", ".blackcolor", function (e) {
                var type = $(this).parents(".color_plan").data("type");

                if (type == 1) {
                    $(".startbtn").attr("style", "background-color:#000000;color:#fff;");
                    $(".regular").css("color", "#000");
                    $("#StartPageBtnBGColor").val("#000");
                    $("#StartPageBtnTextColor").val("#fff");
                }

                if (type == 2) {
                    $(".Endbtn").attr("style", "background-color:#000000;color:#fff;");
                    $("#QResultBGColor").val("#000");
                    $("#QResultBtnTextColor").val("#fff");
                }
            });

            $("body").on("click", function () {
                $(".colorplan .Colorplate").each(function () {
                    if ($(this).css("display") != "none") {
                        $(this).hide();
                    }
                });
            });


            that.elems.colorplan.on("click", ".allcolor", function (e) {
                $(".colorplan .Colorplate").toggle();
                e.stopPropagation();
            });

            $(".colorplan").on("click", ".Colorplate div", function (e) {
                debugger;
                var type = $(this).parents(".color_plan").data("type");

                if (type == 1) {
                    $(".startbtn").attr("style", $(this).attr("style"));
                    $(".regular").css("color",  $(this).css("background-color"));
                    $("#StartPageBtnBGColor").val($(this).css("background-color"));
                    $("#StartPageBtnTextColor").val($(this).css("color"));
                }

                if (type == 2) {
                    $(".Endbtn").attr("style", $(this).attr("style"));
                    $("#QResultBGColor").val($(this).css("background-color"));
                    $("#QResultBtnTextColor").val($(this).css("color"));
                }
                $(".colorplan .Colorplate").hide();
            });

           

            //调色板事件end

            $("#ButtonName").keyup(function () {
                $(".startbtn").text($(this).val());
               
            });
            



            //点击新增按钮
            that.elems.sectionPage.delegate("#addUserBtn", "click", function (e) {
                debugger;
                var that = this;
                $('#win').window({
                    title: "选择类型", width: 580, height: 430, top: ($(window).height() - 430) * 0.5,
                    left: ($(window).width() - 580) * 0.5
                });

                $(".optionclass").show();

                $('#win').window('open')

            });


            that.registerUploadImgBtn();


            //点击类型图片
            $(".SelectType").delegate("img", "click", function (e) {
                debugger;
                $(".SelectType img").removeClass("selected");
                $(".SelectType img").each(function () {
                    $(this).attr("src", $(this).data('nochecked'));
                });
                $(this).addClass("selected");
                $(this).attr("src", $(this).data('checked'));

            });

            //开始设置页点击启用规则
            $(".startpageContent").delegate(".checkBox", "click", function (e) {
                var me = $(this)
                me.toggleClass("on");

                var checkvalue = $(this).find(".checkvalue");
                if (checkvalue.val()==1) {
                    checkvalue.val(0);
                    $("#QRegular").data("required", false);
                } else {
                    checkvalue.val(1);
                    $("#QRegular").data("required", true);

                }

                $(".startimg .regular").toggle();
            });


            //选择微信端模板
            $(".model").delegate(".radio,.modeltypeimg", "click", function () {
                var model = $(this).parents(".model");
                var radio = model.find(".radio");
                var modeltypeimg = model.find(".modeltypeimg");

                $(".model .radio").removeClass("on");

                radio.addClass("on");

                $(this).parents(".modeltype").find(".radiovalue").val(radio.data("value"));
                debugger;
                $(".modeltypeimg").each(function () {
                    $(this).attr("src", $(this).data("noselected"));
                });
                modeltypeimg.each(function () {
                    $(this).attr("src", $(this).data("selected"));
                });
            });


            //点击添加题目
            $(".listAdd").on("click", ".addBtn", function () {

                $(".questionnew").removeClass("questionnew");

                $(".questionlist .question").removeClass("select");
                $(".questionlist .question").find(".handleLayer").hide();
                    
                var datajosn = "{Questionid:'',DefaultValue: '',EndDate: '',IsRequired: '0',IsShowAddress: '0',IsShowCity: '1',IsShowCounty: '1',IsShowProvince: '1',IsValidateEndDate: '0',IsValidateMaxChar: '0',IsValidateMinChar: '0',IsValidateStartDate: '0',Isphone: '0',MaxChar: '0',MaxScore: '0',MinChar: '0',MinScore: '0',Name: '" + $(this).data("text") + "',NoRepeat: '0',Optionlist:[],QuestionPicID:'',Src:'',isedit:true,StartDate:'' }";
                
                var josn =$.util.decode(datajosn);

                $(".questionlist").append(bd.template($(this).data("createtype"), josn));

                $('body').scrollTop($('body')[0].scrollHeight);

                that.RegisterControl();


            });

            //点击题目编辑按钮
            $(".questionlist").on("click", ".questiontitle,.questionbody", function () {
               
                    $(".questionlist .question").removeClass("select");
                    $(".questionlist .question").find(".handleLayer").hide();
                    $(this).parents(".question").addClass("select");

                    $(this).parents(".question").find(".handleLayer .Questiondata,.handleLayer .optiondata").each(function () {
                        $(this).val($(this).data("realvalue"));
                    });

                    if ($(".questionlist .question").find(".Questiondata").hasClass("MinChar")) {
                        var minchar = $(this).find(".MinChar").eq(0);
                        var maxchar = $(this).find(".MaxChar").eq(0);
                        minchar.numberbox("setValue", $(this).data("realvalue"));
                        maxchar.numberbox("setValue", $(this).data("realvalue"));
                        
                    }


                    if ($(".questionlist .question").find(".Questiondata").hasClass("Qdatebox")) {
                        var StartDate = $(this).find(".StartDate").eq(0);
                        var EndDate = $(this).find(".EndDate").eq(0);
                        var DefaultValue = $(this).find(".DefaultValue").eq(0);
                        

                        StartDate.datebox("setValue", $(this).data("realvalue"));
                        EndDate.datebox("setValue", $(this).data("realvalue"));
                        DefaultValue.datebox("setValue", $(this).data("realvalue"));
                    }

                    $(this).parents(".question").find(".checkBoxValidate").each(function () {
                        if ($(this).data("realvalue") == 1) {
                            $(this).parents(".checkBox").addClass("on");
                        } else {
                            $(this).parents(".checkBox").removeClass("on");
                        }

                    });

                    $(this).parents(".question").find(".uploadimg .img").attr("src", $(this).parents(".question").find(".Questiondata.imgvalue").data("realvalue"));

                    $(this).parents(".question").find(".handleLayer").show();
               
            });

            //点击题目移动按钮
            $(".questionlist").on("click", ".combo", function (e) {
                e.stopPropagation();
            });

            //点击题目添加按钮
            $(".questionlist").on("click", ".btn_move", function (e) {
                e.stopPropagation();
                var QuestionidType = $(this).parents(".question").find(".Questiondata[data-idname='QuestionidType']").eq(0);
                $(".listAdd .addBtn[data-type=" + QuestionidType.val() + "]").click();

            });

            //点击题目移动按钮
            $(".questionlist").on("click", ".btn_enlarge", function (e) {
                e.stopPropagation();
            });


            //点击题目删除按钮
            $(".questionlist").on("click", ".btn_del", function (e) {
                e.stopPropagation();
                var self = $(this);
                $.messager.confirm('提示', '是否确定要删除，题目和题目下的选项数据都会删除且不可恢复！', function (r) {
                    if (r) {

                        var QuestionID = self.parents(".question").find(".Questiondata[data-idname='Questionid']").val();
                        if (QuestionID != null && QuestionID != "") {
                            self.parents(".questionlist").find(".questiondel").append("<input class='Questiondeldata' data-text='删除题目' data-idname='QuestionID' data-realvalue='" + QuestionID + "' value='" + QuestionID + "'  type='text' />");
                        }

                        self.parents(".question").remove();
                    }
                });
            });

            //题目编辑层取消按钮
            $(".questionlist").on("click", ".jsCancelBtn", function () {



                var question = $(this).parents(".question");

                question.find(".optiondelete").removeClass("optiondelete");
                

                    $(".questionlist .question").removeClass("select");

                    question.find(".handleLayer .Questiondata").each(function () {
                        $(this).val($(this).data("realvalue"));

                    });

                    $(".questionlist .question").find(".handleLayer").hide();
                    that.elems.isEditOption = false;
               
            });

            //题目编辑层保存按钮
            $(".questionlist").on("click", ".jsSaveCategoryBtn", function () {
                $(".questionlist .question").removeClass("select");
                
                var self = $(this);
                var question = $(this).parents(".question");

                var Name = question.find(".Qtitle").val();


                //验证start
                if ( Name == "") {
                    $.messager.alert("提示", "标题不能为空！");
                    return;
                }

                //题目标题不能重复start
                var Questiontextcount = 0;

                $(".question").find(".Qtitle").each(function () {
                    if ($(this).val() == Name) {
                        Questiontextcount++;
                    }
                });

                if (Questiontextcount > 1) {
                    $.messager.alert("提示", "题目标题不能重复！");

                    return;
                }
                //题目标题不能重复end

                //验证最少字符不能大于最大字符start
                if (question.find(".Questiondata").hasClass("MinChar"))
                {
                    var IsValidateMinChar = question.find(".IsValidateMinChar").eq(0);
                    var IsValidateMaxChar = question.find(".IsValidateMaxChar").eq(0);
                    if (IsValidateMinChar.val() == "1" && IsValidateMaxChar.val() == "1") {
                        var minchar = question.find(".MinChar").eq(0);
                        var maxchar = question.find(".MaxChar").eq(0);

                        if (minchar.val() == "" || maxchar == "") {
                            return;
                        }

                        minchar.numberbox("setValue", minchar.numberbox("getText"));
                        maxchar.numberbox("setValue", maxchar.numberbox("getText"));

                        if (Number(minchar.val()) > Number(maxchar.val())) {
                            $.messager.alert("提示", "最少字符不能大于最大字符！");
                            return;
                        }
                    }
                }
                //验证最少字符不能大于最大字符end

                //验证开始时间必须小于结束时间start
                if (question.find(".Questiondata").hasClass("Qdatebox")) {

                    var IsValidateStartDate = question.find(".IsValidateStartDate").eq(0);
                    var IsValidateEndDate = question.find(".IsValidateEndDate").eq(0);

                    if (IsValidateStartDate.val() == "1")
                    {
                        var StartDate = question.find(".StartDate").eq(0);
                        if (StartDate.datebox("getText") == "") {
                            $.messager.alert("提示", "起始日期未填写！");
                            return;
                        }
                    }

                    if (IsValidateEndDate.val() == "1") {
                        var EndDate = question.find(".EndDate").eq(0);
                        if (EndDate.datebox("getText") == "") {
                            $.messager.alert("提示", "结束日期未填写！");
                            return;
                        }
                    }

                    if (IsValidateStartDate.val() == "1" && IsValidateEndDate.val() == "1") {
                        var StartDate = question.find(".StartDate").eq(0);
                        var EndDate = question.find(".EndDate").eq(0);

                        if (new Date(StartDate.datebox("getText")) > new Date(EndDate.datebox("getText"))) {
                            $.messager.alert("提示", "开始时间必须小于结束时间！");
                            return;
                        }
                    }
                }
                //验证开始时间必须小于结束时间end

                //验证不允许默认值重复的start
                if (question.find(".Questiondata").hasClass("Repeat"))
                {
                    var count = 1;
                    var repeat = question.find(".Repeat").eq(0);
                    var DefaultValue = question.find(".DefaultValue").eq(0).val();
                    if (repeat.val() == "1")
                    {
                        count = 0;
                    }

                    $(".question").find(".DefaultValue[data-idname='DefaultValue']").each(function () {
                        if ($(this).val() == DefaultValue && DefaultValue != "")
                        {
                            if(repeat.val() == "1"||$(this).parents(".question").find(".Repeat").eq(0).val()=="1")
                            count++;
                        }
                    });
                   
                    if (count > 1) {
                        $.messager.alert("提示", "存在重复数据,请修改本题或与之重复题目的默认值！！");
                       
                        return;
                    } 
                }
                //验证不允许默认值重复的end


                //验证选项至少一个start
                if (question.find(".optionlist").hasClass("editimgoptions"))
                {
                    if (question.find(".editimgoption").not(".optiondelete").length < 1)
                    {
                        $.messager.alert("提示", "答案至少设置一个图片选项！");
                        return;
                    }
                }
                //验证选项至少一个end


                //验证选项标题start
                var tempvalidate = true;

                question.find(".option").not(".optiondelete").find(".optiontext").each(function () {
                    if (tempvalidate) {
                        if ($(this).val() == "") {
                            $.messager.alert("提示", "选项内容不能为空！");
                            tempvalidate = false;
                        }
                    }
                });
                question.find(".editimgoption").not(".optiondelete").find(".optiontext").each(function () {
                    if (tempvalidate) {
                        if ($(this).val() == "") {
                            $.messager.alert("提示", "答案选项内容不能为空！");
                            tempvalidate = false;
                        }
                    }
                });
                if (!tempvalidate) {
                    return;
                }
                //验证选项标题end

                //题目标题不能重复start
                var optiontextcount = 0;

                question.find(".option,.editimgoption").not(".optiondelete").find(".optiontext").each(function () {
                    
                        var optionselef = $(this).val();
                        question.find(".optiontext").each(function () {
                            if ($(this).val() == optionselef) {
                                optiontextcount++;
                            }
                        });
                        if (optiontextcount > 1)
                            return;
                        else
                            optiontextcount = 0;
                });


                if (optiontextcount > 1) {
                    $.messager.alert("提示", "题目选项标题不能重复！");

                    return;
                }
                //题目标题不能重复end

                //验证end
                

                //保存数据到realvalue  start
                question.find(".handleLayer .Questiondata,.handleLayer .optiondata").each(function () {

                    $(this).data("realvalue", $(this).val());

                });

                question.find(".handleLayer .Questiondata.Qdatebox").each(function () {

                    $(this).data("realvalue", $(this).datebox("getText"));

                });
                //保存数据到realvalue end

                //标题start
                question.find(".qtext .title").text(Name);
                //标题end

                //图片start
                var Src = question.find(".uploadimg .img").attr("src")
                question.find(".qtext img").attr("src", Src);
                //图片end

                //默认值start
                var DefaultValue = question.find(".DefaultValue").eq(0);
                
                question.find(".titletext").val(DefaultValue.data("realvalue"));
                if (question.find(".Qdatebox").hasClass("titletext")) {
                    question.find(".Qdatebox.titletext").datebox("setValue", DefaultValue.datebox("getText"));
                }
                //默认值end

                //必填start
                var Required = question.find(".Required").eq(0);
                if (Required.data("realvalue") == 1) {
                    question.find(".qtext .red").show();
                } else {
                    question.find(".qtext .red").hide();
                }
                //必填end
                
                //地址显示start
                question.find(".showaddress").hide();
                question.find(".addroption .checkBoxValidate").each(function () {
                    var _self = $(this);
                    if (_self.data("realvalue") == 1) {
                        question.find("." + _self.data("showclass")).show();
                    }

                    
                });
                //地址显示end


                question.find(".radiolist").html("");
                question.find(".checkBoxlist").html("");
                question.find(".Imgoptionlist").html("");
                question.find(".Imgoptions").html("");

                //保存删除选项
                question.find(".optionlist .option.optiondelete,.optionlist .editimgoption.optiondelete").each(function () {
                    var OptionID = $(this).find(".optiondata[data-idname='OptionID']").val();
                    if (OptionID!=null&&OptionID != "") {
                        $(this).parents(".question").find(".optiondel").append("<input class='optiondeldata' data-text='删除选项' data-idname='OptionID' data-realvalue='" + OptionID + "' value='" + OptionID + "'  type='text' />");
                    }
                    $(this).remove();

                });

                //保存单选选项
                question.find(".optionlist .option").each(function () {
                    question.find(".radiolist").append("<div class='radio'><em></em>&nbsp;" + $(this).find(".optiontext").val() + "</div>");

                });
                //保存多选选项
                question.find(".optionlist .option").each(function () {
                    question.find(".checkBoxlist").append("<div class='checkBox'><em></em>&nbsp;" + $(this).find(".optiontext").val() + "</div>");

                });

                //保存图片单选选项
                question.find(".optionlist .editimgoption").each(function () {
                    question.find(".Imgoptionlist").append("<div class='Imgoption'><img src='" + $(this).find("img").attr("src") + "' /><div class='radio '><em></em>&nbsp;" + $(this).find(".optiontext").val() + "</div></div>");

                });
                //保存图片多选选项
                question.find(".optionlist .editimgoption").each(function () {
                    question.find(".Imgoptions").append("<div class='Imgoption'><img src='" + $(this).find("img").attr("src") + "' /><div class='checkBox '><em></em>&nbsp;" + $(this).find(".optiontext").val() + "</div></div>");

                });

                //保存下拉列表选项
                var dropdownlistjosn = [];
                question.find(".optionlist .option").each(function () {
                    dropdownlistjosn.push({  "text": $(this).find(".optiontext").val() });
                });
                debugger;

                question.find(".Qcombobox").combobox({
                    valueField: 'text',
                    textField: 'text',
                    data: dropdownlistjosn
                });
                //保存下拉列表选项end


                $(".questionlist .question").find(".handleLayer").hide();

                question.data("isok",true);

                that.elems.isEditOption = false;
            });

            //移动到选项显示删除按钮
            $(".questionlist").on("mouseover", ".editimgoption", function () {
                var self = $(this);
                self.find(".optionclose").show();
            });

            //移出到选项隐藏删除按钮
            $(".questionlist").on("mouseout", ".editimgoption", function () {
                var self = $(this);
                self.find(".optionclose").hide();
            });

            //点击选项删除按钮
            $(".questionlist").on("click", ".optionclose", function () {
                var self = $(this);
                $.messager.confirm('提示', '是否确定要删除！', function (r) {
                    if (r) {

                       

                        self.parents(".editimgoption").addClass("optiondelete");
                    }
                });
            });

            //点击选项增加按钮
            $(".questionlist").on("click", ".addbtn", function () {
                $(this).parents(".optionlist").append(bd.template("tpl_option", ""));

            });
           

            //点击选项删除按钮
            $(".questionlist").on("click", ".delbtn", function () {
                var self = $(this);

                if ($(this).parents(".optionlist").find(".option").length < 2) {
                    $.messager.alert('提示', '不能删除全部选项！');
                } else {

                    $.messager.confirm('提示', '是否确定要删除！', function (r) {
                        if (r) {
                            
                            self.parents(".option").addClass("optiondelete");

                        }
                    });
                }
            });

            //点击图片选项上传图片按钮
            $(".questionlist").on("mousedown", ".wrapPics .ke-upload-file", function () {
                
                var len = $(this).parents(".wrapPics").find(".editimgoption").not(".optiondelete").length;

                if (len > 4) {
                    $.messager.alert('提示', '最多上传5张图片！');
                    e.stopPropagation();
                }
                
            });
            


            //题目多选选中事件
            $(".questionlist").on("click", ".handleLayer .checkBox em", function () {

                $(this).parents(".checkBox").toggleClass("on");
                if ($(this).parents(".checkBox").hasClass("on")) {
                    $(this).parents(".checkBox").find(".checkBoxValidate").val(1);
                } else {
                    $(this).parents(".checkBox").find(".checkBoxValidate").val(0);
                }
            });

            //设置得分多选选中事件
            $(".OptionScorelinePanel").on("click", ".setoption .checkBox em", function () {

                $(this).parents(".checkBox").toggleClass("on");
                if ($(this).parents(".checkBox").hasClass("on")) {
                    $(this).parents(".checkBox").find(".checkBoxValidate").val(1);
                } else {
                    $(this).parents(".checkBox").find(".checkBoxValidate").val(0);
                }
            });
            



            //拖动排序start
            $(".questionlist").sortable({ delay: 150});
            $(".questionlist").disableSelection();
            //拖动排序end



            //新增描述模块
            $('#addUserBtn').bind('click', function () {
               


                $("#RecoveryContent").show();
                $("#RecoveryImg").hide();

                if (!that.elems.isregisterdescribeImg) {
                    // 新增描述模块图片选项上传按钮
                    $(".describe").find(".uploadImgBtn").each(function (i, e) {
                        that.addUploadDescribeImgEvent(e);
                    });
                    that.elems.isregisterdescribeImg = true;
                }


                that.initDesc();

                $("#describe").form("load", { ScoreRecoveryInformationID: "", MinScore:"", MaxScore: "", RecoveryType: "", RecoveryContent: "" });

                $(".msimg img").attr("src", "");
              

                $('.jui-mask').show();
                $('.jui-dialog-describe').show();
            });

           
           

            $('.jui-dialog-close').bind('click', function () {
                $('.jui-mask').hide();
                $('.jui-dialog').hide();
            });
            $('.jui-dialog .cancelBtn').bind('click', function () {
                $('.jui-mask').hide();
                $('.jui-dialog').hide();
            });

            

            //返回事件
            $(".startStepbtn").bind("click", function () {
                if (!$.util.getUrlParam("QuestionnaireID") || $.util.getUrlParam("QuestionnaireID") == "") {
                    $.util.toNewUrlPath("/Module/QuestionnaireNews/queryList.aspx?isshow=ture&mid=" + $.util.getUrlParam("mid"));
                } else {
                    $.util.toNewUrlPath("/Module/QuestionnaireNews/queryList.aspx");
                }
            });

            $(".endStepBtn").bind("click", function () {
                window.onbeforeunload = function () {

                }
                $.util.toNewUrlPath( "/Module/QuestionnaireNews/queryList.aspx");
            });

            //上一步、下一步点击事件
            $(".commonStepBtn").bind("click", function () {
                var self = $(this);
                
                if ($(this).hasClass("nextStepBtn") || $(this).hasClass("endStepBtn"))
                {                 

                    var Questiondatasjson = "";

                    //验证
                    var isok = true;
                    $(this).parents(".step").find("input,textarea").each(function () {
                        if (isok) {
                            if ($(this).val() == "" && $(this).data("required")) {
                                isok = false;
                                $.messager.alert("提示",$(this).data("alerttext"));
                            }
                        }
                    });
                    if (!isok) {
                        return;
                    }
                    //验证end

                        if ($(this).parents(".step3").length > 0) {
                            var _isok = true;
                            var questionindex = 0;
                            var questiontext = "";
                            if ($(".question").length > 0) {
                                $(".question").each(function () {
                                    if (_isok) {
                                        questionindex++;
                                        if (!$(this).data("isok")) {
                                            _isok = false;
                                        }
                                    }

                                });

                                if (!_isok) {
                                    $.messager.alert("提示", "第" + questionindex + "道题还有数据没有填写完整，请填写完再进行提交！");
                                    return;
                                }

                                //题目列表start

                                //删除集合
                                var Questiondeldata_json = "";
                                $(".Questiondeldata").each(function () {

                                    Questiondeldata_json += "{" + $(this).data("idname") + ":'" + $(this).data("realvalue") + "'},";

                                });
                                Questiondeldata_json = "QuestionDelDatalist:[" + Questiondeldata_json + "]";
                                //删除集合end

                                $(".question").each(function () {

                                    //删除集合
                                    var optiondeldatasjson = "";
                                    $(this).find(".optiondeldata").each(function () {

                                        optiondeldatasjson += "{" + $(this).data("idname") + ":'" + $(this).data("realvalue") + "'},";

                                    });
                                    optiondeldatasjson = "OptionDelDatalist:[" + optiondeldatasjson + "]";
                                    //删除集合end




                                    var Questiondata_json = "";
                                    $(this).find(".Questiondata").each(function () {

                                        Questiondata_json += $(this).data("idname") + ":'" + $(this).data("realvalue") + "',";

                                    });

                                    //选项列表start

                                        
                                    var optiondatasjson = "";
                                    debugger;
                                    $(this).find(".optionlist .option,.optionlist .editimgoption").each(function () {
                                        var optiondata_json = "";
                                        $(this).find(".optiondata").each(function () {
                                            optiondata_json += $(this).data("idname") + ":'" + $(this).data("realvalue") + "',";

                                        });

                                        optiondata_json = "{" + optiondata_json + "},";
                                        optiondatasjson += optiondata_json;
                                    });

                                    optiondatasjson = "Optionlist:[" + optiondatasjson + "]";
                                    //选项列表end


                                    Questiondata_json = "{" + Questiondata_json  + optiondatasjson +","+optiondeldatasjson+ "},";
                                    Questiondatasjson += Questiondata_json;
                                });
                                Questiondatasjson = "Questiondatalist:[" + Questiondatasjson + "]," + Questiondeldata_json;
                                //题目列表end


                               
                            } else {
                                $.messager.alert("提示", "您的问卷还没设置题目，不能进行下一步操作！");
                                return;
                            }
                        }


                        if ($(this).parents(".step4").length > 0)
                        {
                            //题目列表start

                            $(".OptionScoreitem").each(function () {
                                var Questiondata_json = "";
                                $(this).find(".Questiondata").each(function () {
                                   
                                        Questiondata_json += $(this).data("idname") + ":'" + $(this).val() + "',";
                                  

                                });




                                //选项列表start
                                var optiondatasjson = "";

                                $(this).find(".optionitemdata").each(function () {
                                    var optiondata_json = "";
                                    $(this).find(".optiondata").each(function () {

                                        optiondata_json += $(this).data("idname") + ":'" + $(this).val() + "',";

                                    });

                                    optiondata_json = "{" + optiondata_json + "},";
                                    optiondatasjson += optiondata_json;
                                });

                                optiondatasjson = "Optionlist:[" + optiondatasjson + "]";
                                //选项列表end


                                Questiondata_json = "{" + Questiondata_json + optiondatasjson + "},";
                                Questiondatasjson += Questiondata_json;
                            });
                            Questiondatasjson = "Questiondatalist:[" + Questiondatasjson + "]";
                            //题目列表end

                        }

                        debugger;
                    //提交编辑问卷start

                    if (that.elems.QuestionnaireID != "" || (that.elems.QuestionnaireID != "" || $(this).parents(".step3").length > 0))
                    {

                        var Questionnairedatajson = "";
                        $(".Questionnairedata").each(function () {

                            Questionnairedatajson += $(this).data("idname") + ":'" + $(this).val() + "',";

                        });

                        Questionnairedatajson += "step:'" + self.parents(".step").find(".Questionnairestepdata").val() + "',";

                        if (Questionnairedatajson != "") {
                            Questionnairedatajson = "{" + Questionnairedatajson + "action:'Questionnaire.Questionnaire.SetQuestionnaireList'," + Questiondatasjson + "}";
                        } else {
                            Questionnairedatajson = "{action:'Questionnaire.Questionnaire.SetQuestionnaireList'," + Questiondatasjson + "}";
                        }

                        
                        that.saveQuestionnaire($.util.decode(Questionnairedatajson));
                    }
                    //提交编辑问卷end
                }

                $(".step").hide();
                var step = $(this).data("showstep")
                $("." + $(this).data("showstep")).show();

                if (step == "step3") {
                    that.getQuestionList(function (data) {

                        $(".questionlist").html('<div class="questiondel" style="display:none;"></div>');
                        for (var i = 0; i < data.QuestionnaireList.length; i++) {
                            var Questionnaire = data.QuestionnaireList[i];
                            Questionnaire.isedit = false;
                            $.util.isLoading()
                            $(".questionlist").append(bd.template($(".addBtn[data-type=" + Questionnaire.QuestionidType + "]").data("createtype"), Questionnaire));
                            $.util.isLoading(true)

                        }

                       
                        that.RegisterControl();
                        
                        //保存下拉列表选项
                        var dropdownlistjosn = [];
                        var dropdownlist = $(".questionlist").find(".dropdownlist");
                        dropdownlist.parents(".question").find(".optionlist .option").each(function () {
                            dropdownlistjosn.push({ "text": $(this).find(".optiontext").val() });
                        });
                        dropdownlist.combobox("loadData",dropdownlistjosn);


                        $(".questionlist").find(".question").each(function(){
                            $(this).data("isok", true);
                        });
                    });
                    
                }

                if (step == "step4") {
                    that.getQuestionList(function (data) {

                        $(".OptionScorelinePanel").html("");
                        var _index = 1;
                        $.util.isLoading();
                        for (var i = 0; i < data.QuestionnaireList.length; i++) {
                            var Questionnaire = data.QuestionnaireList[i];
                             if (Questionnaire.QuestionidType == 3 || Questionnaire.QuestionidType == 9) {
                                 Questionnaire.index = _index;
                                 _index++;
                                $(".OptionScorelinePanel").append(bd.template("tpl_SetScoreRadioOption", Questionnaire));
                             } else if (Questionnaire.QuestionidType == 4 || Questionnaire.QuestionidType == 10) {
                                Questionnaire.index = _index;
                                $(".OptionScorelinePanel").append(bd.template("tpl_SetScoreCheckBoxOption", Questionnaire));

                                //注册ui下拉框
                                $(".OptionScorelinePanel").find(".Questioncombobox").combobox({
                                    height: 30,
                                    width: 180,
                                    valueField: 'id',
                                    textField: 'text',
                                    data: [{
                                        "id": 1,
                                        "text": "选中选项获得选项分数"
                                    }, {
                                        "id": 2,
                                        "text": "答对几项的几分，答错不得分"
                                    }, {
                                        "id": 3,
                                        "text": "全部答对才得分"
                                    }],
                                    onSelect: function (param) {
                                        debugger;
                                        if (param) {
                                            if (param.id == 1) {
                                                $(this).parents(".OptionScoreitem").find('.RightValue').hide();
                                                $(this).parents(".OptionScoreitem").find(".OptionScore").show();
                                                $(this).parents(".OptionScoreitem").find('.allRight').hide();

                                            } else if (param.id == 2) {
                                                $(this).parents(".OptionScoreitem").find('.RightValue').show();
                                                $(this).parents(".OptionScoreitem").find(".OptionScore").show();
                                                $(this).parents(".OptionScoreitem").find('.allRight').hide();
                                            } else if (param.id == 3) {
                                                $(this).parents(".OptionScoreitem").find('.RightValue').show();
                                                $(this).parents(".OptionScoreitem").find(".OptionScore").hide();
                                                $(this).parents(".OptionScoreitem").find('.allRight').show();
                                            }
                                        }

                                        $(this).parents(".setoption").find(".Questioncombobox").val(param.id);
                                        that.getScorse();
                                    }
                                }).combobox("setValue", data.QuestionnaireList[i].ScoreStyle);

                               
                                

                                _index++;
                            }
                        }
                        $.util.isLoading(true);

                        $(".OptionScorelinePanel").on("change", ".optionitem .allScorevalue ,.optionitem .Scorevalue ", function () {
                            that.getScorse();
                            
                        });

                        that.getScorse();
                        
                    });

                }

                if (step == "step5") {
                    $("#RecoveryType").combobox({
                        valueField: 'id',
                        textField: 'text',
                        data: [{
                            "id": 1,
                            "text": "文字"
                        }, {
                            "id": 2,
                            "text": "图片"
                        }],
                        onSelect: function (param) {
                            if (param) {
                                if (param.id == 1) {
                                    $("#RecoveryContent").show();
                                    $("#RecoveryImg").hide();
                                } else if (param.id == 2) {
                                    $("#RecoveryContent").hide();
                                    $("#RecoveryImg").show();
                                } 
                            }
                        }
                    });
                    $('#RecoveryType').combobox('select', 1);

                    that.getScoreRecoveryInformationList(function (data) {
                        debugger;
                        that.renderTable(data);
                    });
                }



            });


          
            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal: true,
                shadow: false,
                collapsible: false,
                minimizable: false,
                maximizable: false,
                closed: true,
                closable: true
            });
            $('#panlconent').layout({
                fit: true
            });
            $('.jui-dialog-describe').delegate(".saveBtn", "click", function (e) {
                if (!that.elems.submitstate) {
                    that.elems.submitstate = true;
                    debugger;
                    if ($("#MinScore").combobox("getText") == "")
                    {
                        $.messager.alert("提示", "得分段开始分数未选择");

                        that.elems.submitstate = false;
                        return;
                    }

                    if ($("#MaxScore").combobox("getText") == "") {
                        $.messager.alert("提示", "得分段结束分数未选择");
                        that.elems.submitstate = false;
                        return;
                    }

                    if ($("#RecoveryType").combobox("getText") == "") {
                        $.messager.alert("提示", "描述方式未选择");
                        that.elems.submitstate = false;
                        return;
                    }

                    if ($("#RecoveryType").combobox("getText") == "文字") {
                        if ($("#RecoveryContentvalue").val() == "") {
                            $.messager.alert("提示", "描述内容不能为空");
                            that.elems.submitstate = false;
                            return;
                        }
                    } else {

                        if ($("#RecoveryImgvalue").val() == "") {
                            $.messager.alert("提示", "描述图片未上传");
                            that.elems.submitstate = false;
                            return;
                        }
                    }

                    var prams = { action: 'Questionnaire.ScoreRecoveryInformation.SetScoreRecovery' },
                        fields = $('#describe').serializeArray();
                    //console.log(fields);
                    for (var i = 0; i < fields.length; i++) {
                        var obj = fields[i];
                        prams[obj['name']] = obj['value'];



                    }
                    prams["QuestionnaireID"] = that.elems.QuestionnaireID;
                    
                    that.saveScoreRecovery(prams);
                   


                } else {

                    $.messager.alert('提示', '正在提交请稍后！');
                }

            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            $("#tableWraplist").delegate(".handle", "click", function (e) {
                debugger;
                var rowIndex = $(this).data("index");
                var optType = $(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if (optType == "delete") {
                    $.messager.confirm('删除', '您是否确定删除？', function (r) {
                        if (r) {
                            that.delScoreRecovery(row.ScoreRecoveryInformationID, function (data) {
                                $.messager.alert("提示", "操作成功！");
                            });
                            that.getScoreRecoveryInformationList(function (data) {
                                that.renderTable(data);
                            });
                        }
                    });

                }
                if (optType == "edit") {
                    
                    if (!that.elems.isregisterdescribeImg) {
                        // 新增描述模块图片选项上传按钮
                        $(".describe").find(".uploadImgBtn").each(function (i, e) {
                            that.addUploadDescribeImgEvent(e);
                        });
                        that.elems.isregisterdescribeImg = true;
                    }

                    $("#MaxScore").combobox("loadData", []);


                    that.initDesc(row.MinScore);

                    $("#describe").form("load", { ScoreRecoveryInformationID: row.ScoreRecoveryInformationID, MinScore: row.MinScore, MaxScore: row.MaxScore, RecoveryType: row.RecoveryType, RecoveryContent: row.RecoveryContent });

                    if (row.RecoveryType == 1) {
                        $(".msimg img").attr("src", "");
                        $("#RecoveryImgvalue").val("")
                        $("#RecoveryContent").show();
                        $("#RecoveryImg").hide();
                    } else if (row.RecoveryType == 2) {

                        $(".msimg img").attr("src", row.RecoveryImg);
                        $("#RecoveryImgvalue").val(row.RecoveryImg)
                        $("#RecoveryContent").hide();
                        $("#RecoveryImg").show();
                    }

                    $('.jui-mask').show();
                    $('.jui-dialog-describe').show();
                }

            });
            /**************** -------------------列表操作事件用例 End****************/

        },




        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;

            //加载调色板
            $(".color_plan").append(bd.template("tpl_colorplan", ""));

           

            debugger;
            //加载导航数据
            
            //导航数据
            if (that.elems.type == 3) {
                var data1 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: false, name: "选择微信端模板" }, { checked: false, name: "编辑问题" }, { checked: false, name: "设置得分" }, { checked: false, name: "得分描述" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step1 .navcontent").append(bd.template("tpl_navtitle", data1));

                var data2 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: false, name: "编辑问题" }, { checked: false, name: "设置得分" }, { checked: false, name: "得分描述" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step2 .navcontent").append(bd.template("tpl_navtitle", data2));

                var data3 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: true, name: "编辑问题" }, { checked: false, name: "设置得分" }, { checked: false, name: "得分描述" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step3 .navcontent").append(bd.template("tpl_navtitle", data3));

                var data4 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: true, name: "编辑问题" }, { checked: true, name: "设置得分" }, { checked: false, name: "得分描述" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step4 .navcontent").append(bd.template("tpl_navtitle", data4));

                var data5 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: true, name: "编辑问题" }, { checked: true, name: "设置得分" }, { checked: true, name: "得分描述" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step5 .navcontent").append(bd.template("tpl_navtitle", data5));

                var data6 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: true, name: "编辑问题" }, { checked: true, name: "设置得分" }, { checked: true, name: "得分描述" }, { checked: true, name: "设置微信端结果页" }] };
                $(".step6 .navcontent").append(bd.template("tpl_navtitle", data6));
            } else {

                var data1 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: false, name: "选择微信端模板" }, { checked: false, name: "编辑问题" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step1 .navcontent").append(bd.template("tpl_navtitle", data1));

                var data2 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: false, name: "编辑问题" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step2 .navcontent").append(bd.template("tpl_navtitle", data2));

                var data3 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: true, name: "编辑问题" }, { checked: false, name: "设置微信端结果页" }] };
                $(".step3 .navcontent").append(bd.template("tpl_navtitle", data3));

                var data4 = { "data": [{ checked: true, name: "设置微信端首页" }, { checked: true, name: "选择微信端模板" }, { checked: true, name: "编辑问题" }, { checked: true, name: "设置微信端结果页" }] };
                $(".step6 .navcontent").append(bd.template("tpl_navtitle", data4));


                $(".step3 .nextStepBtn").data("showstep", "step6");
                $(".step6 .prevStepBtn").data("showstep", "step3");

            }
            
            if (this.elems.QuestionnaireID != null&&that.elems.QuestionnaireID != "") {
                that.getQuestionnaire();
            }



            $.util.stopBubble(e)


        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if (!data.ScoreRecoveryInformationList) {

                data.ScoreRecoveryInformationList = [];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //多选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data: data.ScoreRecoveryInformationList,
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: 'QuestionnaireID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    {
                        field: 'MinScore', title: '得分段', width: 30, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var MinScore = row.MinScore;
                            var MaxScore = row.MaxScore;
                            return MinScore + "-" + MaxScore;
                        }
                    },
                    {
                        field: 'RecoveryType', title: '描述', width: 100, resizable: false, align: 'center',
                        formatter: function (value, row, index) {
                            if (value == 1) {
                                return row.RecoveryContent;
                            } else {
                                return "<img src=" + row.RecoveryImg + " style='Width:60px'>";
                            }
                        }
                    },
                   
                    {field : 'Status',title : '操作',width:30,align:'center',resizable:false,
                    formatter: function (value, row, index) {
                        debugger;
                            var status = row.Status;
                            var optstr = "";
                            optstr += '<p class="handle exit opt" data-index="' + index + '" data-oprtype="edit"></p>';
                            optstr += '<p class="handle delete opt" data-index="' + index + '" data-oprtype="delete"></p>';
                           

                            return optstr;
                        }
                    },


                ]],

                onLoadSuccess : function() {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.ScoreRecoveryInformationList.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.show();
                        $(".loading").hide();
                    }
                },
                onClickRow:function(rowindex,rowData){
                 
                },onClickCell:function(rowIndex, field, value){
                 
                }

            });

            //循环判断数据列表是否存在样式问题
            that.elems.Timer = setInterval(function () {
                that.elems.Timercount++;
                if (that.elems.Timercount > 20)
                {
                    clearInterval(that.elems.Timer);
                }
                if (($("#tableWraplist .datagrid-btable").height() - $("#tableWraplist .datagrid-body").height()) > 0)
                {
                    that.elems.tabel.datagrid("resize");
                    clearInterval(that.elems.Timer);
                }
            }, 500);

            debugger;
            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPageCount,
                totalRecords: data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function (n) {
                    //这里可以做自已的处理
                    //...
                    //处理完后可以手动条用selectPage进行页码选中切换
                    this.selectPage(n);
                    //让  tbody的内容变成加载中的图标
                    //var table = $('table.dataTable');//that.tableMap[that.status];
                    //var length = table.find("thead th").length;
                    //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                    that.loadMoreData(n);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);


           
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage-1;
           
        },
        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            debugger;
            var self = this;
            // 注册上传按钮
            $(".contentArea_vipquery").find(".uploadImgBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });

           

        },
        //上传图片区域的各种事件绑定
        addUploadoptionImgEvent: function (e) {
            var self = this;
            debugger;
            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                var result = data,
					thumUrl = result.thumUrl,//缩略图
					url = result.url;//原图
                var urldata = { _url: url }
                //点击图片选项上传图片按钮
                $(".questionlist").on("mousedown", ".wrapPics .ke-upload-file", function () {

                    var len = $(this).parents(".wrapPics").find(".editimgoption").not(".optiondelete").length;

                    if (len > 4) {
                        $.messager.alert('提示', '最多上传5张图片！');
                        e.stopPropagation();
                    }

                });
                var len = $(ele).parents(".wrapPics").find(".editimgoption").not(".optiondelete").length;
                if (len <5) {
                    $(ele).parents(".wrapPics").find(".optionlist").append(bd.template("tpl_imgoption", urldata));
                }
                
            });
        },
        //上传图片区域的各种事件绑定
        addUploadDescribeImgEvent: function (e) {
            var self = this;

            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                debugger;
                var $uploadItem = $(ele).parents('.uploadItem'),
                imgvalue = $uploadItem.find(".imgvalue"),
                msimg = $uploadItem.find(".msimg img"),
                url = data.url;//原图

                imgvalue.val(url);
                msimg.attr("src",url);

            });
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent: function (e) {
            var self = this;
				
            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                var $uploadItem = $(ele).parents('.uploadItem'),
					flag = $uploadItem.data('flag'),
                    showclass = $uploadItem.data('showclass'),
                    imgvalue = $uploadItem.find(".imgvalue"),
                    uploadimg = $uploadItem.find(".uploadimg .img"),
					result = data,
					thumUrl = result.thumUrl,//缩略图
					url = result.url;//原图
                debugger
                if (showclass)
                {
                    $("." + showclass).attr("src",url);
                }

                if (imgvalue)
                {
                    imgvalue.val(url);
                }


                if (uploadimg)
                {
                    uploadimg.attr("src", url);
                   
                }
                
                $uploadItem.data('url', url);
            });
        },
        //上传图片
        uploadImg: function (btn, callback) {
            var that = this;
            var _width = 130;
            var that = this,
               w = 640,
               h = 1008;

            if($(btn).data("uploadimgwidth"))
            {
                _width = $(btn).data("uploadimgwidth");
            }
            
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width: _width,
                //上传的文件类型
                fieldName: 'imgFile',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: that.elems.domain + '/Framework/Javascript/Other/kindeditor/asp.net/upload_thumbnails_json.ashx?dir=image&width=' + w + '&height=' + h,
                //&width='+w+'&height='+h+'&originalWidth='+w+'&originalHeight='+h
                afterUpload: function (data) {
                    if (data.error === 0) {
                        if (callback) {
                            callback(btn, data);
                        }
                    } else {
                        alert(data.message);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },
        //初始化描述模块
        initDesc: function (_minscore) {
            var MinScoredata = [];
            var minvalue = $(".ScoreRangevalue").eq(0).data("minvalue");
            var maxvalue = $(".ScoreRangevalue").eq(0).data("maxvalue");
            for (var i = minvalue; i < Number(maxvalue) + 1; i++) {
                MinScoredata.push({ "text": i });
            }
            debugger;

            var MaxScoredata = [];
            if (_minscore) {
                $("#MaxScore").combobox("setValue", "");
                for (var i = (_minscore + 1) ; i < Number(maxvalue) + 1; i++) {
                    MaxScoredata.push({ "text": i });
                }
            }


            $("#MaxScore").combobox({
                valueField: 'text',
                textField: 'text',
                panelHeight: 'auto',
                data: MaxScoredata,
                onShowPanel: function () {
                    var count = $("#MaxScore").combobox("getData").length;
                    if (count < 1)
                    {
                        $("#MaxScore").combobox("hidePanel");
                        $.messager.alert("提示", "请先选择开始得分！");
                    }
                    
                }
            });

            $("#MinScore").combobox({
                valueField: 'text',
                textField: 'text',
                panelHeight: 'auto',
                data: MinScoredata,
                onSelect: function (param) {
                    $("#MaxScore").combobox("setValue", "");
                    var _MaxScoredata = [];
                    for (var i = (param.text + 1) ; i < Number(maxvalue) + 1; i++) {
                        _MaxScoredata.push({ "text": i });
                    }

                    $("#MaxScore").combobox("loadData", _MaxScoredata);

                }

            });
        }
        ,
        //获取问卷内容
        getQuestionnaire: function () {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'Questionnaire.Questionnaire.GetQuestionnaire',
                    QuestionnaireID: that.elems.QuestionnaireID
                },
                beforeSend: function () {
                    $.util.isLoading()
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        debugger;

                        $(".Questionnairedata[data-idname=BGImageSrc]").val(result.BGImageSrc);
                        $(".Questionnairedata[data-idname=ButtonName]").val(result.ButtonName);
                        $(".Questionnairedata[data-idname=IsShowQRegular]").val(result.IsShowQRegular);
                        $(".Questionnairedata[data-idname=ModelType]").val(result.ModelType);
                        $(".Questionnairedata[data-idname=QRegular]").val(result.QRegular);
                        $(".Questionnairedata[data-idname=QResultBGColor]").val(result.QResultBGColor);
                        $(".Questionnairedata[data-idname=QResultBGImg]").val(result.QResultBGImg);
                        $(".Questionnairedata[data-idname=QResultBtnTextColor]").val(result.QResultBtnTextColor);
                        $(".Questionnairedata[data-idname=QResultImg]").val(result.QResultImg);
                        $(".Questionnairedata[data-idname=QResultTitle]").val(result.QResultTitle);
                        $(".Questionnairedata[data-idname=QuestionnaireID]").val(result.QuestionnaireID);
                        $(".Questionnairedata[data-idname=QuestionnaireName]").val(result.QuestionnaireName);
                        $(".Questionnairedata[data-idname=QuestionnaireType]").val(result.QuestionnaireType);
                        $(".Questionnairedata[data-idname=StartPageBtnBGColor]").val(result.StartPageBtnBGColor);
                        $(".Questionnairedata[data-idname=StartPageBtnTextColor]").val(result.StartPageBtnTextColor);

                        //初始化开始页按钮背景和字体颜色、规则颜色start
                        $(".startbtn").attr("style", "background-color:" + result.StartPageBtnBGColor + ";color:" + result.StartPageBtnTextColor + ";");

                        $(".startbtn").html(result.ButtonName);
                        $(".regular").attr("style", "color:" + result.StartPageBtnBGColor + ";");
                        $("#StartPageBtnBGColor").val(result.StartPageBtnBGColor);
                        $("#StartPageBtnTextColor").val(result.StartPageBtnTextColor);
                        //初始化开始页按钮背景和字体颜色、规则颜色end

                        //初始化规则start
                        if (result.IsShowQRegular == 1) {
                            $(".rulebtn").addClass("on");
                            $("#QRegular").data("required", true);
                            $(".regular").show();
                        } else {
                            $(".rulebtn").removeClass("on");
                            $("#QRegular").data("required", false);
                            $(".regular").hide();
                        }
                        //初始化规则end
                        //开始页背景图片初始化
                        $("._BGImageSrc").attr("src", result.BGImageSrc);

                        //结束页背景图片初始化
                        $(".end_BGImageSrc").attr("src", result.QResultBGImg);

                        //结束页按钮背景和字体颜色初始化
                        $(".Endbtn").attr("style", "background-color:" + result.QResultBGColor + ";color:" + result.QResultBtnTextColor + ";");
                        $("#QResultBGColor").val(result.QResultBGColor);
                        $("#QResultBtnTextColor").val(result.QResultBtnTextColor);

                        //模版选择初始化start
                        $(".model .radio").removeClass("on");
                        $(".model .radio").eq((result.ModelType - 1)).addClass("on");
                        $(".modeltypeimg").each(function () {
                            $(this).attr("src", $(this).data("noselected"));
                        });
                        $(".model .modeltypeimg").eq((result.ModelType - 1)).attr("src", $(".model .modeltypeimg").eq((result.ModelType - 1)).data("selected"));
                        //模版选择初始化end

                    } else {
                        debugger;
                        alert(data.Message);
                    }
                }
            });
        }
        ,//获取问卷题目内容
        getQuestionList: function (callback) {
                debugger;
                var that = this;

                if (that.elems.QuestionnaireID != null&&that.elems.QuestionnaireID != "") {
                    $.util.ajax({
                        url: "/ApplicationInterface/Gateway.ashx",
                        data: {
                            action: 'Questionnaire.Questionnaire.GetQuestionList',
                            QuestionnaireID: that.elems.QuestionnaireID
                        },
                        beforeSend: function () {
                            $.util.isLoading()
                        },
                        success: function (data) {
                            if (data.IsSuccess && data.ResultCode == 0) {
                                var result = data.Data;

                                if (callback) {
                                    callback(result);
                                }

                               

                            } else {
                                debugger;
                                alert(data.Message);
                            }
                        }
                    });
                }
            }
        ,
        //保存问卷内容
        saveQuestionnaire: function (params)
            {
                debugger;
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    async: false,
                    data: params,
                    beforeSend: function () {
                        debugger;
                        $.util.isLoading()

                    },
                    success: function (data) {
                        debugger;
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            if (that.elems.QuestionnaireID == "") {
                                that.elems.QuestionnaireID = result.QuestionnaireID;

                                $("#QuestionnaireID").val(that.elems.QuestionnaireID);
                            }
                            
                        } else {
                            debugger;
                            alert(data.Message);
                        }
                    }
                });
        },
        //保存问卷内容
        saveScoreRecovery: function (params) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: params,
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (result.TesultValue == 0) {
                            $('.jui-mask').hide();
                            $('.jui-dialog').hide();
                            that.getScoreRecoveryInformationList(function (data) {
                                that.renderTable(data);
                            });
                        }
                        if (result.TesultValue == 1) {
                            $.messager.alert("提示", "得分段不能重复！");
                        }

                    } else {
                        debugger;
                        alert(data.Message);
                        
                    }
                },
                complete: function ()
                {
                    that.elems.submitstate = false;
                }
            });
        },

        //获取问卷描述内容列表
        getScoreRecoveryInformationList: function (callback) {
                debugger;
                var that = this;
                var load = that.loadData;
                if (that.elems.QuestionnaireID != null&&that.elems.QuestionnaireID != "") {
                    $.util.ajax({
                        url: "/ApplicationInterface/Gateway.ashx",
                        data: {
                            action: 'Questionnaire.Questionnaire.GetScoreRecoveryInformationList',
                            QuestionnaireID: that.elems.QuestionnaireID,
                            PageSize: load.args.PageSize,
                            PageIndex: load.args.PageIndex
                        },
                        beforeSend: function () {
                            $.util.isLoading()

                        },
                        success: function (data) {
                            if (data.IsSuccess && data.ResultCode == 0) {
                                var result = data.Data;

                                if (callback) {
                                    callback(result);
                                }

                               

                            } else {
                                debugger;
                                alert(data.Message);
                            }
                        }
                    });
                }
        },
        delScoreRecovery: function (ScoreRecoveryInformationID, callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'Questionnaire.ScoreRecoveryInformation.DelScoreRecovery',
                    ScoreRecoveryInformationID: ScoreRecoveryInformationID
                },
                beforeSend: function () {
                    $.util.isLoading()

                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(data);
                        }

                    } else {
                        debugger;
                        alert(data.Message);
                    }
                }
            });
        },
        //获取描述分数段值
        getScorse: function () {
            var minScore = null;
            var maxScore = 0;

            $(".OptionScoreitem").each(function () {
                var minScore = null;
                var maxScore = 0;
                var ScoreStyle = $(this).find(".Questioncombobox").val();
                var IsRequired = $(this).find(".Questiondata").data("isrequired");

                if (ScoreStyle == null)
                {
                    $(this).find(".Scorevalue").each(function () {
                        if (minScore == null) {
                            minScore = Number($(this).val());
                        }
                        if (minScore > Number($(this).val())) {
                            minScore = Number($(this).val())
                        }
                        if (maxScore < Number($(this).val())) {
                            maxScore = Number($(this).val())
                        }
                    });
                }

                if (ScoreStyle == 1 || ScoreStyle == 2 || ScoreStyle) {
                    $(this).find(".Scorevalue").each(function () {
                        if (minScore == null)
                        {
                            minScore = Number($(this).val());
                        }
                        if (minScore > Number($(this).val()))
                        {
                            minScore = Number($(this).val())
                        }
                        maxScore += Number($(this).val());
                    });
                }
                if (ScoreStyle == 3) {
                    minScore = 0;
                    $(this).find(".allScorevalue").each(function () {
                        maxScore = Number($(this).val());
                    });
                }

                if (IsRequired == 0)
                {
                    minScore = 0;
                }

                $(this).find(".Scorevaluetext").data("minvalue", minScore);
                $(this).find(".Scorevaluetext").data("maxvalue", maxScore);
                $(this).find(".Scorevaluetext").html(minScore + "-" + maxScore);

            });


            var minScoresum = 0;
            var maxScoresum = 0;
            $(".Scorevaluetext").each(function () {
                minScoresum += Number($(this).data("minvalue"));
                maxScoresum += Number($(this).data("maxvalue"));
            });
            $(".ScoreRangevalue").data("minvalue", minScoresum);
            $(".ScoreRangevalue").data("maxvalue", maxScoresum);
            $(".ScoreRangevalue").html(minScoresum + "-" + maxScoresum);
                //.data("allminScore", 10);
            //$(this).data("allmaxScore", 10);
        },
        //注册ui控件
        RegisterControl: function () {
            var that = this;

            //拖动排序start
            $(".optionlist").sortable();
            $(".optionlist").disableSelection();

            //拖动排序end


            // 注册上传按钮
            $(".questionlist").find(".questionnew .uploadImgBtn").each(function (i, e) {
                that.addUploadImgEvent(e);
            });

            // 注册图片选项上传按钮
            $(".questionlist").find(".questionnew .addeditimgoptionbtn").each(function (i, e) {
                that.addUploadoptionImgEvent(e);
            });

            //注册ui数字输入框
            $(".questionlist").find(".questionnew .Qnumberbox").numberbox({
                min: 0,
                precision: 0,
                height: 30
            });

            

            //注册ui日期输入框
            $(".questionlist").find(".questionnew .handleLayer .Qdatebox").datebox({
                height: 30,
                width: 150
            });

            //注册ui日期输入框
            $(".questionlist").find(".questionnew .questionbody .Qdatebox").datebox({
                height: 30,
                width: 150,
                disabled: true
            });

            //注册ui下拉框
            $(".questionlist").find(".questionnew .Qcombobox").combobox({
                height: 30,
                width: 120,
                valueField: 'text',
                textField: 'text',
                editable:false
            });

            
           
        },
       loadData: {
            args: {
                PageIndex: 1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                QuestionnaireName:"",
                QuestionnaireType:0
            }
        }

    };
    page.init();
});

