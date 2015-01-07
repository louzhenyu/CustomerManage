define(['jquery', 'template', 'tools', 'kkpager', 'artDialog','datetimePicker','tips','zTree'], function ($) {
    window.alert = function (content, autoHide) {
        var d = dialog({
            title: '提示',
            cancelValue: '关闭',
            skin: "black",
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
        elems: {
            sectionPage:$("#section"), 
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#content"),                   //表格body部分
            thead:$("#thead"),                    //表格head部分
            dialogLabel:$("#dialogLabel"),          //标签选择层
            menuItems:$("#menuItems"),             //针对表格的操作菜单
            resultCount: $("#resultCount"),          //所有匹配的结果
            vipSourceId:''
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            var that = this;
            this.elems.vipSourceId = JITMethod.getUrlParam("vip_source_id");
            this.loadPageData();
            //获得标签类型
            //获得标签列表
            that.loadData.getTagList(function(data){
                that.select.tagType=data.Data.VipTagTypeList;
                that.select.tagList=data.Data.VipTagList;     //存储起来以便复用
                //渲染tag
                that.renderTag(true);   //是渲染的第一行
            });
            this.initEvent();
        },
        //显示插件的提示内容
        showTipsDiv:function(){
            //插件提示
            $('._showTips').poshytip({
				className: 'tip-yellowsimple',
				showOn: 'mouseover',
				alignTo: 'target',
				alignX: 'right',
				alignY: 'center',
				offsetX: -10,
				//showTimeout: 100
			});
        },
        initEvent: function () {
            var that = this;
            //点击展示更多的查询条件 事件
            that.elems.simpleQueryDiv.delegate(".more", "click", function (e) {
                that.elems.allQueryDiv.slideDown();
                that.elems.simpleQueryDiv.css("border-bottom", "1px solid #fff");
                //更多查询条件
                $(".moreQueryWrap").hide();
                that.stopBubble(e);
            });
            //点击全部导出按钮 事件
            $('.tableHandleBox').delegate('.exportBtn', 'click', function (e) {
                that.loadData.exportVipList();
                that.stopBubble(e);
            });
            //点击收起隐藏高级查询条件
            that.elems.allQueryDiv.delegate(".slideUp", "click", function (e) {
                //让更多查询条件的层显示
                that.elems.allQueryDiv.slideUp();
                that.elems.simpleQueryDiv.css("border-bottom", "1px solid #fff;");
                 //更多查询条件
                $(".moreQueryWrap").show();
                that.stopBubble(e);
            });
           
            $('#addNewVip').delegate('.cancelBtn', 'click', function (e) {
                that.hideElements('#addNewVip');
                that.stopBubble(e);
            });
            //创建新会员
            $('#addNewVip').delegate('.saveBtn', 'click', function (e) {
                var result=that.setNewVipInfoCondition();
                debugger;
                if(result){
                    that.loadData.addVipInfo(function (data) {
                        dialog({
                            title: "继续添加会员提示",
                            content: '继续添加会员？',
                            ok: function () {
                                //this.title('3秒后自动关闭').time(3);
                                //清空传入添加会员接口的数据
                                that.loadData.args.NewVipInfoColumns = [];
                                that.emptyParams();
                                return true;
                            },
                            okValue: "确认",
                            cancelValue: '关闭',
                            cancelVal: '关闭',
                            //为true等价于function(){}
                            cancel: function () {
                                that.hideElements('#addNewVip');
                            }
                        }).showModal();
                    });
                }
                that.stopBubble(e);
            });
           //针对模拟的下拉框进行事件绑定
           $.util.selectEvent("#simpleQuery,#allQuery,#addNewVip,#dialogLabel");
           $("#simpleQuery,#allQuery,#addNewVip").delegate("[name=vipinfo],[name=newvipinfo]","click",function(e){  //针对tree进行的事件
                var $t=$(this);
                var forminfo=$t.data("forminfo");
                if(forminfo.DisplayType==205){ //tree
                    $t.parent().parent().find(".ztree").show();
                }
                that.stopBubble(e);
            }).delegate("._resetLables","click",function(e){  //重置高级查询条件
                $("#lables").hide()
                $("#showSelectLabel").show();
                that.loadData.args.SearchColumns=[];  //数据清空
                that.stopBubble(e);
            });
            $("#simpleQuery,#allQuery,#addNewVip").delegate(".ztree","mouseleave",function(e){   //树结构移开则隐藏
                $t=$(this);
                $t.hide();
                that.stopBubble(e);
            });
            //点击弹出选择label的层
            that.elems.allQueryDiv.delegate("#showSelectLabel","click",function(e){
                //标签层
                that.showElements("#dialogLabel");
                that.elems.dialogLabel.find(".promptContent").html();
                that.renderTag(true);  //设置为第一次渲染  清空操作
                that.elems.dialogLabel.find("._sureBtn").removeAttr("disabled").css('background','#fe7c24');
                that.stopBubble(e);
            });
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getVipList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table
                    that.renderTable(data);
                    kkpager.generPageHtml({
                        pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex:1,
                        mode: 'click', //设置为click模式
                        //总页码  
                        total: data.Data.TotalPageCount,
                        totalRecords: data.Data.TotalCount,
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
                });
                that.stopBubble(e);
            });


            /***
             *** 此处为比较复杂的逻辑部分
             *** 进行高级查询的标签选择
             ***/
             //添加新的标签查询条件
             that.elems.dialogLabel.delegate(".addIcon","click",function(e){
                   //渲染tag
                   if(that.loadData.args.VipSearchTags.length){
                        that.isAddOne=true;   //表示只添加一个
                   }
                   that.renderTag();
                   that.elems.dialogLabel.find(".selectValueBox").removeClass("on");
                   that.stopBubble(e);
             }).delegate(".minusIcon","click",function(e){   //对应的删除查询条件
                $(this).parent().parent().remove();
                that.stopBubble(e);
             });
             //全选当前页面的全选   
             that.elems.thead.delegate("._selCurPage","click",function(e){
                 that.select.isSelectAllPage=false;
                 that.elems.tabel.find(".checkBox").addClass("on");  //全选
                 that.stopBubble(e);
             }).delegate("._selAllPage","click",function(e){  //所有页面都选择     进行特殊处理   即使翻页也选中
                  that.select.isSelectAllPage=true;   //设置为把所有的选择都选中
                  that.elems.tabel.find(".checkBox").addClass("on");  //全选
                  that.stopBubble(e);
             }).delegate("._cancelSel","click",function(e){   //取消所有的选择
                  that.select.isSelectAllPage=false;  //设置不是所有页都选中
                  that.elems.tabel.find(".checkBox").removeClass("on");  //反选
                  that.stopBubble(e);
             });
             //查询会员详情页面事件
             that.elems.tabel.delegate(".seeIcon","click",function(e){
                var vipId=$(this).parent().data("id");
                var option={
                    domFlag:"data-flag",
                    attrs:["unitId","optionid"],    //属性也要保存  回来进行使用
                    pageIndex:that.loadData.args.PageIndex  //第几页
                };
               
                //页面跳转之前将数据保存
                $.util.setPageParam(option);
                var mid = JITMethod.getUrlParam("mid");
                location.href = "VipDetail.aspx?vipId=" + vipId + "&mid=" + mid;;
                 
                that.stopBubble(e);
             });
             //跳转到详情页
             that.elems.tabel.delegate("td>a","click",function(e){
                var $t=$(this);
                var option={
                    domFlag:"data-flag",
                    attrs:["unitId","optionid"],    //属性也要保存  回来进行使用
                    pageIndex:that.loadData.args.PageIndex  //第几页
                };
                //页面跳转之前将数据保存
                $.util.setPageParam(option);
                debugger;
                location.href=$t.data("href");

             });
             //添加会员按钮事件
             that.elems.menuItems.delegate("._addVip","click",function(e){
                 that.showElements("#addNewVip");
                 var loadedKey = 'loaded';
                 if ($('#addNewVip').attr(loadedKey))
                     return;
                //动态的会员表单
                that.loadData.getVipDyniform(function(data){
                    
                    //高级查询配置
                    var html = bd.template("tpl_addVipForm", data);
                    $("#addNewVip").find(".promptContent").html(html);
                    //显示日期
                    that.showDatepicker();
                    $('#addNewVip').attr(loadedKey, true);
                    //加载ztree
                    that.loadZTree();
                });
                that.stopBubble(e);
             });
             //全部导出会员事件
             that.elems.menuItems.delegate("._exportVip","click",function(e){
                that.loadData.exportVipList();
                that.stopBubble(e);
             });
             //删除会员按钮事件
             //that.elems.menuItems.delegate("._delVip","click",function(e){
             //   var selDoms=that.elems.tabel.find(".on");   //获得选中的dom对象
             //   if(selDoms.length==0){
             //       alert("至少选择一行再进行删除!!",true);
             //       return false;
             //   }
             //   var vipIds=[];
             //   selDoms.each(function(i,j){
             //       vipIds.push($(this).parent().data("id"));
             //   });
             //   //赋值用来作为接口请求参数
             //   that.loadData.args.VipIds=vipIds;
             //   dialog({
             //       title:"删除提示",
             //       content: '是否要删除选中的会员?删除之后将不能恢复!!',
             //       ok: function () {
    	     //           //this.title('3秒后自动关闭').time(3);
             //           //调用删除会员的接口
             //           that.loadData.deleteVips(function(data){
             //               alert("成功删除"+selDoms.length+"条数据!!",true);
             //               //触发查询事件   重新加载数据
             //               $(that.elems.sectionPage.find(".queryBtn")[0]).trigger("click");
             //           });
                        
             //           return true;
             //       },
             //       okValue:"确认",
             //       cancelValue: '关闭',
             //       cancelVal: '关闭',
             //       cancel: true //为true等价于function(){}
             //   }).showModal();
             //   that.stopBubble(e);
             //});
             ///////////////////////////////////////////////
             //标签选择的操作
             ///////////////////////////////////////////////
             //点击标签的输入框事件   让对应的标签列表显示
             that.elems.dialogLabel.delegate("input","click",function(e){
                var $t=$(this);
                if($t.attr("isShow")=="false"||!!!$t.attr("isShow")){
                    $t.attr("isShow",true);
                    $t.parent().addClass("on").parent().siblings().find(".selectValueBox").removeClass("on");
                }else{
                    $t.parent().removeClass("on");
                    $t.attr("isShow",false);
                }
                //插件内容提示
                that.showTipsDiv();
                that.stopBubble(e);
             }).delegate("li","click",function(e){  //选择不同的标签类别对应显示标签
                 var $t=$(this);
                 $t.addClass("on").siblings().removeClass("on");
                 //类别Id
                 var tagTypeId=$t.data("tagid");
                 var html=bd.template("tpl_tagList",{
                     tagList:that.select.tagList?that.select.tagList:[],
                    typeId:tagTypeId
                 });
                 $t.parent().parent().find(".selectValueInfo").html(html);
                 //插件内容提示
                 that.showTipsDiv();
                 that.stopBubble(e);
             }).delegate(".selectValueInfo>p","click",function(e){//选择具体的标签
                var $t=$(this);
                //标签具体的内容
                var tagInfo=$t.data("taginfo");
                //父节点
                var parent=$t.parent().parent().parent();
                //设置内容
                parent.find("input").attr("tagId",tagInfo.TagId).attr("tagName",tagInfo.TagName);
                //设置值
                parent.find("input").val(tagInfo.TagName).attr("title",tagInfo.TagDesc);
                //如果出现错误则显示红色边框   选择一个则让边框从红色恢复
                parent.css("border","1px solid #ccc");
                var sureBtn=that.elems.dialogLabel.find("._sureBtn");
                sureBtn.removeAttr("disabled");
                sureBtn.css('background','#fe7c24');
                sureBtn.css('cursor','pointer');
                //隐藏
                parent.removeClass("on");
                //插件内容提示
                that.showTipsDiv();
                that.stopBubble(e);
             }).delegate(".selectList>p","click",function(e){//高级条件筛选选择下拉框的事件
                var $t=$(this);
                $t.parent().parent().find(".text").html($t.text());
                if(e.target.className=="kuohaoItem"){
                    //验证括号是否匹配
                    that.validateParentheses();
                }
                $t.parent().hide();
                that.stopBubble(e);
             }).delegate("._sureBtn","click",function(e){   //高级查询条件的确定事件
                var $t=$(this);
                var condition="";
                if($t.attr("disabled")!="disabled"){  //正常保存数据
                    //将所有的内容拼接成条件
                    //要拼接的jqObj
                    var conditionDoms=that.elems.dialogLabel.find(".promptContent .tagWrap");
                    var array=[];
                    var canContinue=true;   //没有错误是否可以正常保存标签
                    //循环一条条数据
                    conditionDoms.each(function(){
                        var $cons=$(this).find("[name=condition]");  //所有的要保存的条件
                        var obj={};
                        $cons.each(function(i,j){                       
                            var current=$(this);
                            if(i==0){  //左边括号
                                obj.LeftBracket=$.trim(current.text());
                            }
                            if(i==1){  //包含 不包含
                                if(current.text()=="包含"){
                                    obj.EqualFlag="include";
                                }else{
                                    obj.EqualFlag="exclude";
                                }
                                obj.EqualFlagStr=current.text();
                                
                            }
                            if(i==2){  //tagId
                                obj.TagId=current.attr("tagId");
                                //标签名称
                                obj.TagName=current.attr("tagName");
                                var sureBtn=that.elems.dialogLabel.find("._sureBtn");
                                if(!!!obj.TagId){  //没有正常选择tag标签  则让他显示红色
                                    current.parent().css("border","1px solid red");
                                    sureBtn.attr("disabled","disabled");
                                    sureBtn.css('background','grey');
                                    sureBtn.css('cursor','default');
                                    canContinue=false;  //不能确定提交
                                }
                                
                            }
                            if(i==3){  //右边的括号
                                obj.RightBracket=$.trim(current.text());
                            }
                            if(i==4){  //并且或者

                                if(current.text()=="并且"){
                                    obj.AndOrStr="And";
                                }else{
                                    obj.AndOrStr="Or";
                                }
                                obj.AndOrString=current.text();
                            }
                            
                        });
                        array.push(obj);
                    });
                    if(canContinue){
                        //将数据存储起来
                        that.loadData.args.VipSearchTags=array;
                        //显示标签
                        that.showLables();
                        //触发关闭事件的层
                        $(".hintClose").trigger("click");
                   }
                }
                that.stopBubble(e);
             });
             //修改高级标签的事件
             that.elems.allQueryDiv.delegate(".modifyBtn","click",function(e){
                //触发弹层事件
                $("#showSelectLabel").trigger("click");
                //每次修改 将重置为false
                that.isAddOne=false;
                that.stopBubble(e);
             });

             //选中行操作  复选框选择事件
             that.elems.tabel.delegate("td","click",function(e){
                var $t=$(this);
                $t.toggleClass("on");
             });


            //关闭弹出层
            $(".hintClose").bind("click", function (e) {
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
                that.stopBubble(e);
            });
        },
        //把数据给拼接成高级标签
        showLables:function(){
            //将模板转换为html
            var html=bd.template("tpl_lables",{list:this.loadData.args.VipSearchTags});
            //点击查询标签   隐藏
            $("#showSelectLabel").hide();
            //标签内容显示
            $("#lables").html(html).show();
        },
        //验证高级查询条件的括号
        validateParentheses:function(){
            var stack = new Array();
            debugger;
            //获得所有的括号的jqObj
            var $parenthesesArr = this.elems.dialogLabel.find(".promptContent .kuohao");
            //确定按钮
            var sureBtn=this.elems.dialogLabel.find("._sureBtn");
            $parenthesesArr.css('color','black');
            //循环判断
            $parenthesesArr.each(function(){
                var $selectedValue = $(this).text();
                $selectedValue = jQuery.trim($selectedValue);
                if('' != $selectedValue)
                {
                    if(stack.length == 0)
                    {
                        stack.push($(this));
                    }
                    else
                    {
                        if('(' == $selectedValue)
                        {
                            stack.push($(this));
                        }
                        else
                        {
                            var $preObj = stack.pop();
                            if('(' != $preObj.text())
                            {
                                stack.push($preObj);
                                stack.push($(this));
                            }
                        }
                    }

                }
            });
            if(stack.length>0)
            {
                //不可以进行条件保存
                sureBtn.attr("disabled","disabled");
                sureBtn.css('background','grey');
                sureBtn.css('cursor','default');
            }
            else
            {
                sureBtn.removeAttr("disabled");
                sureBtn.css('background','#fe7c24');
                sureBtn.css('cursor','pointer');
            }
            for(var i=0;i<stack.length;i++)
            {
                stack[i].css('color','red');
            }

        },
        //继续添加会员时清空参数
        emptyParams: function() {
            var that = this;
            var inputDom = $('[name=newvipinfo]');
            inputDom.each(function (i, dom) {
                var $dom = $(dom);
                var json = $dom.data("forminfo");
                if (json.DisplayType == 5) {//表示的是下拉框  需要特殊处理
                    $dom.attr('optionid', '');
                    $dom.text('全部');
                }
                else if (json.DisplayType == 205) {   //树结构
                    $dom.val('');
                    $dom.attr('unitId', '');
                } else{  //数值类型
                    $dom.val('');
                }
            });
        },
        //添加新会员 取得添加会员动态表单参数
        setNewVipInfoCondition: function () {
            var that = this;
            var vipinfo = [];
            var inputDom = $('[name=newvipinfo]');
            var flag=true;
            inputDom.each(function (i, dom) {
                var $dom = $(dom);
                var dataText = $dom.attr("data-text");
                /*if($dom.val()==""){
        		    alert(dataText+"不能为空!");
        		    return false;
        	    }else{*/  //提交的时候可以进行非空验证
                var json = $dom.data("forminfo");
                var obj = {};
                var value;
                if (json.DisplayType == 5) {//表示的是下拉框  需要特殊处理
                    if ($dom.html() == "请选择") {
                        value = "";
                    } else {
                        value = $dom.attr("optionid");
                    }
                    obj.ColumnValue1 = value;
                }else if(json.DisplayType==205){   //树结构
                    if(json.IsMustDo==1){
                        if($dom.val()==""){
                            alert(dataText+"必须选择一项!");
                            return false;
                        }
                    }
                    obj.ColumnValue1=$dom.attr("unitId"); 
                }else if(json.DisplayType==2){  //数值类型
                    var valStr=$dom.val();
                    if(valStr!=""){
                         if(isNaN(valStr)){
                            alert(dataText+"只能输入数字!");
                            return false;
                        }
                    }else{
                        if(json.IsMustDo==1){
                            alert(dataText+"不能为空!");
                            return false;
                        }
                    }
                    obj.ColumnValue1 = $dom.val();
                }else if(json.DisplayType==6){  //生日
                    var valStr=$dom.val();
                    if(json.IsMustDo==1){
                        if(valStr==""){
                            alert(dataText+"不能为空!");
                            return false;
                        }
                    }
                    obj.ColumnValue1 = $dom.val();
                }else{
                    obj.ColumnValue1 = $dom.val();
                }
                obj.ColumnName = json.ColumnName;
                obj.ControlType = json.DisplayType;
                //搜索的条件
                vipinfo.push(obj);
                //}
            });
            if(vipinfo.length==inputDom.length){//数据正常
                 //将查询条件赋值
                that.loadData.args.NewVipInfoColumns = vipinfo;
                flag=true;
                return flag;
            }else{
                flag=false;
                return flag;
            }
        },
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            var vipinfo = [];
            var inputDom = $('[name=vipinfo]');
            var vipSourceFlag = 0;
            inputDom.each(function(i,dom){
        	    var $dom=$(dom);
        	    var dataText=$dom.attr("data-text");
        	    /*if($dom.val()==""){
        		    alert(dataText+"不能为空!");
        		    return false;
        	    }else{*/  //提交的时候可以进行非空验证
                var json=$dom.data("forminfo");
                var obj={};
                var value;
                if(json.DisplayType==5){//表示的是下拉框  需要特殊处理
                    if($dom.html()=="全部"){
                        value="";
                    }else{
                        value=$dom.attr("optionid");
                    }
                    obj.ColumnValue1=value;
                }else if(json.DisplayType==205){   //树结构
                    obj.ColumnValue1=$dom.attr("unitId");
                }else if(json.DisplayType==2 || json.DisplayType == 6){   //数值类型,日期类型
                    if ($dom.data('order') == 'left') {
                        obj.ColumnValue1 = $dom.val();
                    }
                    else {
                        obj.ColumnValue2 = $dom.val();
                    }
                }
                else {
                    obj.ColumnValue1=$dom.val();
                }
                obj.ColumnName=json.ColumnName;     
                obj.ControlType=json.DisplayType;
                //搜索的条件
                vipinfo.push(obj);
                if (obj.ColumnName.toLowerCase() == 'VipSourceId') {
                    vipSourceFlag = 1;
                }
            });
            if (!vipSourceFlag) {
                var vipSourceJson = {ColumnName:'VipSourceId',ColumnValue1:that.elems.vipSourceId,
                    ControlType:5};
                vipinfo.push(vipSourceJson);
            }
            //将查询条件赋值
            that.loadData.args.SearchColumns = vipinfo;
        },
        //显示日期
        showDatepicker:function(){
            //获取表单之后让datepicker初始化
            $('.datepicker').datetimepicker({
                allowBlank:true,  //失去焦点是否可以为空
                lang: "ch",
                format: 'Y-m-d',
                timepicker:false  //不显示小时和分钟
                //step: 5 //分钟步长
            });
        },
        //加载页面的数据请求
        loadPageData: function () {
            var that = this;
            //获得动态表单的配置
            that.loadData.getDyniformSettings(function (data) {
                var simpleHtml = bd.template("tpl_simpleDyniform", data);
                //高级查询配置
                var html = bd.template("tpl_dyniform", data);
                that.elems.simpleQueryDiv.find(".item").append(simpleHtml);
                that.elems.allQueryDiv.append(html);
                //日期显示
                that.showDatepicker();
                //加载ztree
                that.loadZTree();
                $.util.setDomValue({
                    domFlag:"data-flag",
                    trigger:[{             //触发的事件
                        obj:"#addNewVip",
                        eventType:"click"
                    }],
                    callback:function(result){
                        that.loadData.args.PageIndex=result.pageIndex;
                    }
                });
                //触发查询事件
                $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            });
        },
        //加载ztree
        loadZTree:function(){
            //获得所有的ztree集合
            $(".ztree").each(function(i,j){
                var $t=$(this);
                //所有的数据集合
                var forminfo=$t.data("forminfo"),id=$t.attr("id");
                var zNodes=[];
                for(var j=0,length=forminfo.length;j<length;j++){
                    var item=forminfo[j];
                    if(item.ParrentUnitID==-99){  //父节点
                        item.ParrentUnitID==0;
                    }
                    item.name=item.UnitName;
                    item.id=item.UnitID;
                    item.pId=item.ParrentUnitID;
                    zNodes.push(item);
                }
                var setting = {
                    isSimpleData : true,
                    view:{
                        treeNodeKey : "id",
                        treeNodeParentKey : "pId",
                        chkStyle: "radio",
                        enable: true,
                        radioType: "all",
                    },
                    callback:{
                        onClick:function(event, treeId, treeNode){
                            //点击的子节点
                            //if(!!!treeNode.children){
                                $t.parent().find("input").val(treeNode.name).attr("unitId",treeNode.UnitID);
                                $t.hide();
                            //}
                                
                        }
                    }
                };
                var zTreeObj = $.fn.zTree.init($("#"+id), setting, []) ;
                var treeNodes = zTreeObj.transformTozTreeNodes(zNodes);
                zTreeObj.addNodes(null, treeNodes);
            });
                
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getVipList(function(data){
                    that.renderTable(data);
            });
        },
        //渲染tag
        renderTag:function(isFirst){   //是否是第一行   第一行的tag  前后不显示括号
            var option={
                tagType:this.select.tagType,
                tagList:this.select.tagList,
                isFirst:isFirst?isFirst:false,
                VipSearchTags:this.loadData.args.VipSearchTags.length?this.loadData.args.VipSearchTags:[]   //用来修改的时候填充
            };
            //将VipSearchColumns删除
            if(this.isAddOne){
                option.VipSearchTags=[];
            }
            var html=bd.template("tpl_newItem",option);
            if(isFirst){
                this.elems.dialogLabel.find(".promptContent").html(html);
            }else{
                this.elems.dialogLabel.find(".promptContent").append(html);
            }
        },
        //渲染tabel
        renderTable: function (data) {
            var that = this;
            //获得列名
            var headerObj = data.Data.Columns;
            $("#thead").html('<tr class="title"></tr>').find(".title").html(bd.template("tpl_thead", { obj: headerObj }));
            //获得列表数据
            var bodyList = data.Data.VipTable;
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

            var myMid = JITMethod.getUrlParam("mid");
            $("#content").html(bd.template("tpl_content", { list: { finalList: finalList, otherItems: otherItems }, mid: myMid }));
            //设置所有匹配的记录数
            that.elems.resultCount.html(data.Data.TotalCount);
            if(that.select.isSelectAllPage){  //如果是选择所有页面的  全选   则触发选中checkbox事件
                that.elems.tabel.find(".checkBox").addClass("on");  //全选
            }
            if (data.Data.TotalPageCount > 1) {
                kkpager.generPageHtml(
                    {
                        pno: data.Data.PageIndex,
                        mode: 'click', //设置为click模式
                        //总页码  
                        total: data.Data.TotalPageCount,
                        totalRecords:data.Data.TotalCount,
                        isShowTotalPage: true,
                        isShowTotalRecords: true,
                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                        //适用于不刷新页面，比如ajax
                        click: function (n) {
                            //这里可以做自已的处理
                            //...
                            //处理完后可以手动条用selectPage进行页码选中切换
                            this.selectPage(n);
                            //点击下一页或者上一页 进行加载数据
                            that.loadMoreData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);

            }
        },
        //显示弹层
        showElements: function (selector) {
            this.elems.uiMask.show();
            $(selector).slideDown();
        },
        hideElements: function (selector) {

            this.elems.uiMask.fadeOut(500);
            $(selector).slideUp(500);
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
        loadData: {
            args: {
                PageIndex: 1,
                PageSize: 10,
                SearchColumns: [],    //查询的动态表单配置
                NewVipInfoColumns:[], //新增会员动态表单配置
                OrderBy:"CREATETIME",           //排序字段
                SortType: 'DESC',        //如果有提供OrderBy，SortType默认为'ASC'
                VipSearchTags:[],     //标签集合
                VipIds:[]           //要删除的会员ID集合
            },
            tag:{
                VipId:"",
            },
            addVipInfo: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: 'AddVip',
                        Columns: this.args.NewVipInfoColumns
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得查询动态表单配置
            getDyniformSettings: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipSearchPropList"
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    },
                    error: function (err,status) {
                        alert(err + status);
                    }
                });
            },
            //通过隐藏form下载文件
            exportVipList: function (callback) {
                var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipList';//&req=';
                var data = {
                    Parameters: {
                        SearchColumns: this.args.SearchColumns,
                        PageSize: this.args.PageSize,
                        PageIndex: this.args.PageIndex,
                        OrderBy: this.args.OrderBy,
                        SortType: this.args.SortType,
                        VipSearchTags: this.args.VipSearchTags
                    }
                };
                var dataLink = JSON.stringify(data);
                var form = $('<form>');
                form.attr('style', 'display:none;');
                form.attr('target', '');
                form.attr('method', 'post');
                form.attr('action', getUrl);
                var input1 = $('<input>');
                input1.attr('type', 'hidden');
                input1.attr('name', 'req');
                input1.attr('value', dataLink);
                $('body').append(form);
                form.append(input1);
                form.submit();
                form.remove();
            },
            //获得查询的会员数据
            getVipList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipInfoList",
                        SearchColumns:this.args.SearchColumns,
                        PageSize:this.args.PageSize,
                        PageIndex:this.args.PageIndex,
                        OrderBy:this.args.OrderBy,
                        SortType: this.args.SortType,
                        VipSearchTags:this.args.VipSearchTags
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得标签类型
            getTagType: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipTagTypeList",
                        VipId:this.tag.VipId
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得标签信息列表
            getTagList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipTagList",
                        VipId:this.tag.VipId
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取动态的注册表单
            getVipDyniform: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetCreateVipPropList"
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //删除会员信息
            //deleteVips: function (callback) {
            //    $.util.ajax({
            //        url: "/ApplicationInterface/Vip/VipGateway.ashx",
            //        data: {
            //            action: "DeleteVip",
            //            VipIds:this.args.VipIds
            //        },
            //        success: function (data) {
            //            if (data.IsSuccess && data.ResultCode == 0) {
            //                if (callback) {
            //                    callback(data);
            //                }

            //            } else {
            //                alert(data.Message);
            //            }
            //        }
            //    });
            //}
        }

    };
    page.init();
});