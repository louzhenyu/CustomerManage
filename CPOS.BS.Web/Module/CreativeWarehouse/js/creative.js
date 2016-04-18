/*define(['jquery', 'tools','js/tempModel.js','template', 'kindeditor','kkpager', 'artDialog'], function ($,temp) {*/


define(['jquery',"jvveshow",'js/tempModel.js','jquery-jvve','kindeditor','bxslider','kkpager','easyui','copy','artTemplate','tools', 'artDialog'],
    function ($,jvveshow,temp,jQjvve) {
    //上传图片
    KE = KindEditor;
    var page = {
        template: temp,
        elems: {
            sectionPage:$("#section"),
            navigation:$(".contentArea_vipquery .navigation"),
            submitBtn:$("#submitBtn"),
            editor1:"",
            isEdit:false,//是否是编辑
            selectType:"",//查看是那一项的奖品
            setUpTheType:"",//获取该活动实现的目的类型。1关注，2是成交
            isSaveJvveShow:false, //风格是否保存过，如果保存，风格再次切换的时候有提示。
            isInitStyle:true,//是否需要实例化一个风格
            currentStyleId:"",//当前的风格实例化id
            isloadzclip:true,// 绘制复制按钮
            eventInfoType:"",//当前选中的风格实现的功能
            panlH:200,                          // 下来框统一高度
            ImgCode:""
        },
        TemplateThemeList:[],//详情风格列表数据
        loadDataInfo:{},
        prizeListHB:[], //奖品列表
        prizeListShare:[],//分享奖励
        prizeListWatch:[],//关注奖励
        prizeListReg:[],//注册奖励
        integralDataList:[], //积分数据缓存

        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            ToolList:[
                {
                     DrawMethodId:"",
                    "InteractionType": 2,
                    "DrawMethodName": "团购",
                    "DrawMethodCode": "TG"
                },
                /*{

                 "InteractionType": 1,
                 "DrawMethodName": "问卷",
                 "DrawMethodCode": "QN"
                 },*/
                {
                    DrawMethodId:"",
                    "InteractionType": 1,
                    "DrawMethodName": "红包",
                    "DrawMethodCode": "HB"
                },
                /*  {

                 "InteractionType": 2,
                 "DrawMethodName": "热销",
                 "DrawMethodCode": "RX"
                 },*/
                {
                     DrawMethodId:"",
                    "InteractionType": 2,
                    "DrawMethodName": "抢购",
                    "DrawMethodCode": "QG"
                },
                /* {
                 "InteractionType": 1,
                 "DrawMethodName": "大转盘",
                 "DrawMethodCode": "DZP"
                 }*/
            ],
            tagList:[]                              //标签列表
        },

       config: {
            signature: {
                "sign": "A2B0FF0FB91E848794FCA587B17C96B702BE8E00",
                "noncestr": "wMXYLU",
                "timestamp": "1459417663",
                "appid": "lkfb9p7jr6t8ba8k1r",
                "secret": "284cf0799a5c4d1da5a5e90f419b7c71",
                url: encodeURIComponent(window.location.origin+window.location.pathname)
            },
            links:{},
            templateId: '',
            worksId: '',//'570871d97ec6134b71ebcd89',
            mediaId: ''//'56f915bb7ec6133325a50e34'
        },





      //生成一个作品
        initWorks:function() {

            jvveshow
             .cloneWorks(self.config.templateId)//复制一个作品。
             .then(function (data) {
             console.info("实例化作品成功！", data);
             self.config.worksId=data.data;
             //self.config.worksId="56f8d9ff7ec6133325a50ca6"
             debugger;
             self.instantiateEditor()

             }, function (data) {
             console.error("实例化作品失败！", data);
             });
        },

    /**
     * 实例化编辑器
     * @return {Undefined}
     */
  instantiateEditor:  function(){

            jvveshow
                .initEditor({
                    wroksId: self.config.worksId,
                    links:self.config.links,
                    container: $('.example-wp')
                })

                .then(function (data) {
                    debugger
                    console.info('实例化编辑器成功！');
                    self.elems.editor1 = data;
                    self.elems.isInitStyle=false;//已经实例化过就不需要从新实例化了
                }, function (data) {
                    console.error('实例化编辑器失败！');
                });


    },
  //保存微秀场
        saveJevvSHow:function(callback){
            if(!self.elems.editor1) {console.info('实例化编辑器对象不存在'); return;}

            // 保存
            self.elems.editor1
                .save()
                .then(function(data){
                    self.elems.isSaveJvveShow=true;
                    if(callback){
                        callback(data);
                    }
                   // alert("保存成功")
                    console.info('作品“'+ self.elems.editor1.worksId +'”保存成功！');
                });

        },
        initJevvShow:function(isInit,drawMethodCode) {
            var filde = [{name: "url", value: encodeURIComponent(window.location.origin + window.location.pathname)},{name:"DrawMethodCode",value:drawMethodCode}] ;
            self.loadData.operation(filde, "jvveShow", function (data) {
                //data.Data.sign=data.Data.sign.toLocaleLowerCase();
                $.extend(self.config.signature, data.Data);
                if(data.Data.UrlInfo){
                    self.config.links[data.Data.UrlInfo.Name]=data.Data.UrlInfo.Url
                }

                debugger;
                jvveshow
                    .config(self.config.signature)
                    .then(function (data) {
                        debugger
                        console.info('签名验证成功！');
                        if(isInit) {
                          self.instantiateEditor();

                        }else{
                            self.initWorks();
                        }
                    }, function (data) {
                        debugger
                        console.error('签名验证失败！');
                    });
            });
        } ,
    init: function () {
        this.initEvent();
        debugger;
        var templateId= $.util.getUrlParam("TemplateId");
        var CTWEventId=$.util.getUrlParam("CTWEventId");
        if (CTWEventId) {
            self.loadData.args.CTWEventId = CTWEventId;
            self.elems.isEdit=true;
            self.elems.isInitStyle=false;//编辑状态首次加载不需要从新实例化风格。

        }
        ///this.releaseOpen({obj:1234});
        if(templateId){
            self.loadData.args.TemplateId=templateId;
            this.loadPageData();
        }

       /* $('.draggable').draggable({
            onDrag:function(e) {

                var d = e.data;
                if (d.left < 0) {
                    d.left = 0
                }
                if (d.top < 0) {
                    d.top = 0
                }
                if (d.left + $(d.target).outerWidth() > $(d.parent).width()) {
                    d.left = $(d.parent).width() - $(d.target).outerWidth();
                }
                if (d.top + $(d.target).outerHeight() > $(d.parent).height()) {
                    d.top = $(d.parent).height() - $(d.target).outerHeight();
                }

            }
        });*/




            //var templateId=$.util.getUrlParam("TemplateId");



        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },

        //绘制编辑元素方法
        initEdit:function(){
          $("[data-edit='true']").each(function(i,index){
              if($(this).is("img")){

              } else if($(this).is("div")){
                  $(this).css({"position":"relative"});
                  $(this).prepend("<em class='editIconBtn'></em>");

              }

          });
        },
        initEvent: function () {
            var that = this;
             that.initEdit();
            //上传按钮初始化
            that.elems.sectionPage.find(".jsUploadBtn").each(function (i, e) {
              that.addUploadImgEvent(e);

            });
            that.elems.sectionPage.delegate(".spreadPanel .tabDiv","click",function(){
                $(this).siblings(".tabDiv").removeClass("on");
                $(this).addClass("on");
                var tabName=$(this).data("showtab");
                $('[data-tabname="'+tabName+'"]').siblings("[data-tabname]").hide();
                $('[data-tabname="'+tabName+'"]').show();
                if(tabName=="tab03"){
                    $(".optionPrize").show()
                } else{
                    $(".optionPrize").hide()
                }


            }).delegate(".slide div","click",function(){    //主题设置事件页面
                   var me=$(this);

                var domIndex= me.parent().data("index");
                    me.parent().siblings(".slide").removeClass("on");
                $("[data-index='"+domIndex+"'].slide").addClass("on");


                    $('[data-interaction].btnItem').addClass("disable").removeClass("on");
                    var index = $(this).parent().data("index");
                    var list = that.TemplateThemeList[index].EventInteractionList;
                    if (list && list.length > 0) {
                        $.each(list, function (index, filed) {
                            var code = filed.InteractionType;
                            $('[data-interaction="' + code + '"].btnItem').removeClass("disable");
                        })
                    }
                    if ($('[data-interaction].btnItem').eq(0).hasClass("disable")) {
                        $('[data-interaction].btnItem').eq(1).trigger("click");
                    } else {
                        $('[data-interaction].btnItem').eq(0).trigger("click");
                    }



            }).delegate(".radioList .radio","click",function(){
                var me= $(this), name= me.data("name");

                var  selector="[data-name='{0}']".format(name);
                $(selector).removeClass("on");
                me.toggleClass("on");
                if($(this).data("input")){
                    $(selector).siblings().find(".easyui-numberbox").numberbox({
                        disabled:false
                        //required: false
                    });
                } else{
                    $(selector).siblings().find(".easyui-numberbox").numberbox({
                        disabled:true
                        //required: false
                    });
                }

            }).delegate(".radioList .radio .numberbox","click",function(e){
                $.util.stopBubble(e);
            });


            $(".HBList").delegate(".delete","click",function(e){    //红包奖励删除
                debugger;
                var  index=$(this).data("index"),id=$(this).data("id");
                // index=parseInt(index);
                $("#prize").datagrid("deleteRow",index);

                var  data=$("#prize").datagrid("getData");
                $("#prize").datagrid("loadData",data);
                if(data.rows.length==0){
                    $("#prize").parents(".HBList").hide();
                }

            });
            $(".expandList").delegate(".delete","click",function(e){    //推广奖励删除
                debugger;
                var  index=$(this).data("index"),id=$(this).data("id");
                // index=parseInt(index);
                $("#expandGrid").datagrid("deleteRow",index);

                var  data=$("#expandGrid").datagrid("getData");
                $("#expandGrid").datagrid("loadData",data);
                if(data.rows.length==0){
                    $("#expandGrid").parents(".expandList").hide();
                }
                switch(that.elems.selectType){
                    case"share":
                       that.prizeListShare=data.rows ;
                        break;
                    case"watch":
                      that.prizeListWatch=data.rows ;
                        break;
                    case"reg":
                        that.prizeListReg=data.rows  ;
                        break;
                }
            });

            that.elems.navigation.delegate("li","click",function(e){
                debugger;


                /*处理页面切换之间的提交后天逻辑*/

                var navLiOn=that.elems.navigation.find("li.on").data("showpanel"),  //当前停留的tab

                    panelName=$(this).data("showpanel");//当前点击的tab
                      var me=$(this);
                   if(navLiOn==panelName){ // 点击和停留的一样什么都不做
                       return false;
                   }


                var ThemeId="",drawMethodCode="";      var  isPerform=true;  //能继续执行 true  可以继续执行false;
                var eventInfo="";//当前选中的风格实现的方案对象，用于设置页面对象实例化赋值

                if(navLiOn=="nav01") {
                    var index = $("#slider").find(".slide.on").data("index");
                    if (!index) {
                        if (index != 0) {
                            alert("请选择一个风格");
                            return false;
                        }
                    }
                    var InteractionType = $(".btnItem.on").data("interaction");
                    if(!InteractionType){
                        $.messager.alert("提示","没有关联活动,请选择其他风格");
                        return false;
                    }

                    var list = that.TemplateThemeList[index].EventInteractionList;
                    if (that.TemplateThemeList[index]) {
                        self.config.templateId = that.TemplateThemeList[index].H5TemplateId; //作品id
                        ThemeId = that.TemplateThemeList[index].ThemeId;
                        drawMethodCode= that.TemplateThemeList[index].DrawMethodCode
                    }


                    if(list&&list.length>0){
                        $.each(list,function(index,filed){
                            if(InteractionType==filed.InteractionType){
                                drawMethodCode=filed. DrawMethodCode;
                                eventInfo = list[index];
                            }
                        })
                    }

                    if(self.elems.currentStyleId&&self.elems.currentStyleId!=ThemeId) {
                        isPerform = false;
                            $.messager.confirm("提示", "选择新的风格会影响已有的配置是否还要进行操作？", function (r) {
                                if (r) {
                                    isPerform=true;
                                    self.elems.isInitStyle = true;//改变风格需要从新实例化

                                } else {
                                    isPerform=false;
                                }
                                if(isPerform) {
                                    if (ThemeId && self.elems.isInitStyle) {      // isInitStyle是否需要从新实例化一个风格
                                        that.initJevvShow(false,drawMethodCode);
                                        that.loadSetPageData(eventInfo);
                                        that.setDefaultImg(eventInfo);
                                        self.elems.currentStyleId = ThemeId;
                                    }
                                    var navLi = that.elems.navigation.find("li");
                                    if (me.index() == navLi.length - 1) {
                                        that.elems.navigation.find("ul img").hide(0);
                                        that.elems.navigation.find(".hide").show(0);
                                        that.elems.submitBtn.html("保存预览");
                                        that.elems.submitBtn.data("issubMit", true);
                                    } else {
                                        that.elems.navigation.find("ul img").show(0);
                                        that.elems.navigation.find(".hide").hide(0);
                                        that.elems.submitBtn.html("下一步");
                                        that.elems.submitBtn.data("issubMit", false);
                                    }

                                    that.elems.navigation.find("li").removeClass("on");
                                    me.addClass("on");
                                    $("[data-panel].panelDiv").hide();
                                    $("[data-panel='" + panelName + "'].panelDiv").show();
                                    that.elems.submitBtn.data("flag", panelName);
                                    if (that.elems.navigation.find("li.on").index() == 0) {
                                        $(".prevStepBtn").hide();
                                        $(".btnopt").css({width: "200px"});
                                    } else {
                                        $(".prevStepBtn").show();
                                        $(".btnopt").css({width: "280px"});
                                    }


                                }
                            });
                    }
                }

                if(navLiOn=="nav03"&&panelName=="nav04"){
                     index = $("#slider").find(".slide.on").data("index");
                     list = that.TemplateThemeList[index].EventInteractionList;
                    var InteractionType = $(".btnItem.on").data("interaction");
                    if (that.TemplateThemeList[index]) {
                        if(list&&list.length>0){
                            $.each(list,function(index,filed){
                                if(InteractionType==filed.InteractionType){
                                    drawMethodCode=filed. DrawMethodCode;
                                    eventInfo = list[index];
                                }
                            })
                        }
                    }
                    isPerform=$("#setOptionForm").form("validate");
                    if(isPerform&&drawMethodCode=="HB"){
                        if(that.prizeListHB.length==0){
                            alert("请添加奖品");
                            isPerform=false;
                            return false;
                        }

                    }
                }

                   if(isPerform) {
                       if (ThemeId && self.elems.isInitStyle) {      // isInitStyle是否需要从新实例化一个风格
                           that.initJevvShow(false,drawMethodCode);
                           self.elems.currentStyleId = ThemeId;
                           that.loadSetPageData(eventInfo);
                           that.setDefaultImg(eventInfo);
                       }else if(InteractionType&&that.elems.eventInfoType!=InteractionType){ //选中风格实现功能被切换
                           that.loadSetPageData(eventInfo);
                           that.setDefaultImg(eventInfo);
                       }
                       var navLi = that.elems.navigation.find("li");
                       if (me.index() == navLi.length - 1) {
                           that.elems.navigation.find("ul img").hide(0);
                           that.elems.navigation.find(".hide").show(0);
                           that.elems.submitBtn.html("提交");
                           that.elems.submitBtn.data("issubMit", true);
                       } else {
                           that.elems.navigation.find("ul img").show(0);
                           that.elems.navigation.find(".hide").hide(0);
                           that.elems.submitBtn.html("下一步");
                           that.elems.submitBtn.data("issubMit", false);
                       }

                       that.elems.navigation.find("li").removeClass("on");
                       $(this).addClass("on");
                       $("[data-panel].panelDiv").hide();
                       $("[data-panel='" + panelName + "'].panelDiv").show();
                       that.elems.submitBtn.data("flag", panelName);
                       if (that.elems.navigation.find("li.on").index() == 0) {
                           $(".prevStepBtn").hide();
                           $(".btnopt").css({width: "200px"});
                       } else {
                           $(".prevStepBtn").show();
                           $(".btnopt").css({width: "280px"});
                       }


                   }


                if(panelName){ //编辑的时候；
                    self.renderTablePrize();
                }

            });



            that.elems.sectionPage.delegate(".prevStepBtn","click",function(e){
                var index=that.elems.navigation.find("li.on").index()-1;
                that.elems.navigation.find("li").eq(index).trigger("click");
            });
            that.elems.sectionPage.delegate("#submitBtn","click",function(e){
                debugger;
                var index=0;
                if($(this).data("issubMit")){

                  that.setCTWEvent();


                }else{
                    var index=that.elems.navigation.find("li.on").index()+1;
                    that.elems.navigation.find("li").eq(index).trigger("click");
                }
                $.util.stopBubble(e);
            }).delegate(".commonBtn","click",function(){
                var type=$(this).data("type");
                if(type=="prize"){
                    that.setPrize("setPrize");
                }
            });

            that.elems.sectionPage.delegate('[data-panel="nav01"] .btnItem',"click",function(){
                if($(".slide").hasClass("on")){
                    if($(this).hasClass("disable")) {
                        //风格未实现该功能（关注和成交）
                    }else{

                        $(this).siblings().removeClass("on");
                        $(this).addClass("on");
                        self.elems.setUpTheType = $(this).data("interaction");
                    }
                }else{
                     alert("请选择一个风格");
                }



            }).delegate('[data-panel="nav03"] .editIconBtn',"click",function(){
                debugger;
                    var imgMessage={size:$(this).parent().data("size"),imgUrl:$(this).parent().data("imgurl")};
                    var imgCode= $(this).parent().data("imgcode");//页面每个图片唯一的标识
                    imgMessage["imgDefault"]=$(this).parent().data("default");
                    that.imgEdit(imgMessage,imgCode);
                }).delegate('[data-panel="nav04"] .editIconBtn',"click",function(){
                var type=$(this).data("type");
                    switch (type){
                        case "imgText"://图文
                            var title=$('[data-tabname="tab01"]').find('[data-view="Title"]').html();
                            var summary=$('[data-tabname="tab01"]').find('[data-view="Summary"]').html();
                            var imgUrl= $('[data-tabname="tab01"]').find('img').attr("src");
                            var data={title:title,summary:summary,imgUrl:imgUrl}
                            that.imgTextEdit(data);
                            break;
                        case "shareInfo": //分享
                            var title=$('[data-tabname="tab02"] .share').find('[data-view="Title"]').html();
                            var summary=$('[data-tabname="tab02"] .share').find('[data-view="Summary"]').html();
                            var imgUrl= $('[data-tabname="tab02"] .share').find('img').attr("src");
                            var data={title:title,summary:summary,imgUrl:imgUrl}
                            that.shareEdit(data);
                            break
                        case "text":
                           var dom =$(this).siblings(".editText");
                            var data={text:""};
                             if(dom.is("input")){
                                   data.text=dom.val()
                             }else{
                                 data.text=dom.html();
                             }
                            that.textEdit(data);
                            break;
                    }
                    if(!type){
                        var imgMessage={size:$(this).parent().data("size"),imgUrl:$(this).parent().data("imgurl")};
                        var imgCode= $(this).parent().data("imgcode");//页面每个图片唯一的标识
                        imgMessage["imgDefault"]=$(this).parent().data("default");
                        that.imgEdit(imgMessage,imgCode);
                    }
            }).delegate('.checkBox',"click",function(){
                $(this).toggleClass("on");
                $(this).find(".commonBtn").toggle();
                var type=$(this).data("type")
                var selected='[data-type="'+type+'"]';
                if($(this).hasClass("on")){
                    $(".erweiMaPanel").find(selected).show();
                }else{
                    $(".erweiMaPanel").find(selected).hide();
                }

            }).delegate('.checkBox .commonBtn',"click",function(e){
                var type=$(this).parents(".checkBox").data("type"),option=$(this).data("option");
                if(option=="add"){
                    that.setPrize(type);
                }else if(option=="select"){
                    that.selectPrize(type);
                }


                $.util.stopBubble(e);
            });
            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=30;
          // that.renderTablePrize();

            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            /* 属性名 属性值类型 描述 默认值
             title        string 窗口的标题文本。 New Window
             collapsible boolean 定义是否显示可折叠按钮。 true
             minimizable boolean 定义是否显示最小化按钮。 true
             maximizable boolean 定义是否显示最大化按钮。 true
             closable    boolean 定义是否显示关闭按钮。 true
             closed      boolean 定义是否可以关闭窗口。 false
             zIndex      number  窗口Z轴坐标。 9000
             draggable   boolean 定义是否能够拖拽窗口。 true
             resizable   boolean 定义是否能够改变窗口大小。 true
             shadow      boolean 如果设置为true，在窗体显示的时候显示阴影。 true
             inline      boolean 定义如何布局窗口，如果设置为true，窗口将显示在它的父容器中，否则将显示在所有元素的上面。 false
             modal       boolean 定义是否将窗体显示为模式化窗口。 true
             */



            /**************** -------------------弹出窗口初始化 end****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onOpen:function(){
                    $("#win").find(".jsUploadBtn").each(function (i, e) {
                        that.addUploadImgEvent(e);

                    });
                    $("#win").window("hcenter");
                },onClose:function(){
                    $("body").eq(0).css("overflow-y","auto");
                }
            });
            $('#win1').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onOpen:function() {
                    $("#win1").window("hcenter");
                },
                onClose:function(){
                    if(that.elems.optionType1=="coupon"||that.elems.optionType1=="addCouponNumber"){
                        var type=$("#selectType").combobox("getValue");
                        $("#prizeListGrid").datagrid('acceptChanges');
                        var nodeList=$("#prizeListGrid").datagrid("getData").rows;
                        that.couponRenderList(type,nodeList);
                    }
                }
            });
            $('#winrelease').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onClose:function(){
                    $.util.toNewUrlPath("/module/CreativityWarehouse/MyActivity/QueryList.aspx");
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#winrelease').delegate(".saveBtn","click",function(e) {

                $.messager.confirm("直接发布","您的活动即将发布，发布后，您的活动将无法进行界面和奖励的修改。",function(r){
                    if(r){
                        var flied=[{name:"Status",value:"20"}];
                        $.util.isLoading();
                        that.loadData.operation(flied,"changeStatus",function(){
                             $("#winrelease").window("close")
                        })
                    }

                });

            });
            $('#win').delegate(".saveBtn","click",function(e) {
                           debugger;
                var objMsg={isAdd:true};
               if(that.elems.optionType=="imgEdit"){
                   var imgUrl=$("#win").find(".imgUploadPanel").find('[data-imgcode="EditImg"]img').attr("src");
                   var imgCode=that.elems.ImgCode,dom=$("[data-imgcode="+imgCode+"]");
                   dom.data("imgurl",imgUrl);
                   if(!dom.hasClass(".jsUploadBtn")) {
                       if(dom.data("type")=="bg"){
                           dom.css({"backgroundImage": 'url(' + imgUrl+ ')'});

                       }else{
                           dom.find("img").attr("src",imgUrl);
                       }

                   }
               }else if(that.elems.optionType=="setPrize"){    //红包奖励
                   objMsg= that.returnPrizeList(that.prizeListHB);
                   if(objMsg.isAdd){
                       that.prizeListHB =objMsg.list
                   }else{
                     if(objMsg.index!==""){
                         $.messager.alert("提示", objMsg.msg);
                     } else{
                         $.messager.alert("提示", objMsg.msg);
                     }
                   }

                       that.renderTablePrize();


               }else if(that.elems.optionType=="imgText") {
                   $('[data-tabname="tab01"]').find('[data-view="Title"]').html($("#title").val());
                   $('[data-tabname="tab01"]').find('[data-view="Summary"]').html($("#Summary").val());
                   var imgUrl=$("#imgShare").attr("src");
                    $('[data-tabname="tab01"]').find('img').attr("src",imgUrl);
               }else if(that.elems.optionType=="shareInfo"){
                   $('[data-tabname="tab02"] .share').find('[data-view="Title"]').html($("#title").val());
                   $('[data-tabname="tab02"] .share').find('[data-view="Summary"]').html($("#Summary").val());
                   var imgUrl=$("#imgShare").attr("src");
                    $('[data-tabname="tab02"] .share').find('img').attr("src",imgUrl);
               }else if(that.elems.optionType=="share"){  //分享的奖励
                   objMsg= that.returnPrizeList(that.prizeListShare);
                   if(objMsg.isAdd){
                       that.prizeListShare =objMsg.list
                   }else{
                       if(objMsg.index!==""){
                           $.messager.alert("提示", objMsg.msg);
                       } else{
                           $.messager.alert("提示", objMsg.msg);
                       }
                   }
               }else if(that.elems.optionType=="watch"){ // watch的奖励
                   objMsg= that.returnPrizeList(that.prizeListWatch);
                   if(objMsg.isAdd){
                       that.prizeListWatch =objMsg.list
                   }else{
                       if(objMsg.index!==""){
                           $.messager.alert("提示", objMsg.msg);
                       } else{
                           $.messager.alert("提示", objMsg.msg);
                       }
                   }
               }else if(that.elems.optionType=="reg"){   //注册的奖励
                   objMsg= that.returnPrizeList(that.prizeListReg);
                   if(objMsg.isAdd){
                       that.prizeListReg =objMsg.list
                   }else{
                       if(objMsg.index!==""){
                           $.messager.alert("提示", objMsg.msg);
                       } else{
                           $.messager.alert("提示", objMsg.msg);
                       }
                   }

               }else if(that.elems.optionType=="textInfo"){

                   if($("#win").find("input").validatebox("validate")){
                       var text= $("#win").find("input").val();
                      $(".erweiMaPanel").find('[data-view="PromptText"]').html(text);
                   } else{
                       objMsg.isAdd=false;
                   }

               }
                if(objMsg.isAdd) {
                    $('#win').window("close");
                }
            }).delegate(".icon_add","click",function(){
              var type=$("#selectType").combobox("getValue");
                if(type==2) { //积分
                    that.addIntegral();
                }else{
                    that.addCoupon(type);
                }

            }).delegate(".tableWap .opt","click",function(e){
                debugger;
                   var type=$(this).data("opttype");
                   var id=$(this).data("id");
                   var index=$(this).data("index");
                   $("#prizeListGrid").datagrid("selectRow",index);
                   switch(type){
                       case "addIntegral" :
                           that.addNumber("addIntegralNumber",{id:id});

                           break;
                       case "addCoupon" :
                           that.addNumber("addCouponNumber",{id:id});
                           break
                   }
                $.util.stopBubble(e) ;
            }).delegate(".lineText .textBtn","click",function(e){
              var imgUrl= $(this).data("imgurl");
                if(imgUrl){
                    $("#win").find(".imgPanel img").attr("src",imgUrl)
                }


            });

            $('#win1').delegate(".saveBtn","click",function() {
                var isColse=true;
                if ($("#win2OptionForm").form("validate")) {
                    var isSubmit=true;
                    var fileds=$("#win2OptionForm").serializeArray();
                    var obj={id:that.integralDataList.length};
                    if (that.elems.optionType1 == "integral") {

                        $.each(fileds,function(index,filed){
                          obj[filed.name]=filed.value;
                        });

                        for(var i=0;i<that.integralDataList.length;i++) {
                            if (that.integralDataList[i].integral == obj.integral) {
                                isSubmit=false;
                                break
                            }
                        }
                        if(isSubmit) {
                            that.integralDataList.push(obj);
                            that.integralRenderList();
                        }else{
                            $.messager.alert("提示","已经存在积分值为"+ obj.integral+"的项，请重新添加");
                            return false;
                        }

                    } else if(that.elems.optionType1 == "addIntegralNumber") {   //追加积分数量

                        $.each(fileds,function(index,filed){
                            obj[filed.name]=filed.value;
                        });
                       for(var i=0;i<that.integralDataList.length;i++){
                              if(that.integralDataList[i].id==obj.id){
                                  that.integralDataList[i]["number"]=parseInt(that.integralDataList[i]["number"])+parseInt(obj.number);
                              }
                       }

                        that.integralRenderList();

                    }else if(that.elems.optionType1 == "addCouponNumber"){


                        $.each(fileds,function(index,filed){
                            if(filed.name=="id"){
                                fileds[index].name="CouponTypeID"
                            }
                            if(filed.name=="number"){
                                fileds[index].name="IssuedQty"
                            }
                        });
                        isColse=false;
                        that.loadData.operation(fileds,"addCouponNumber",function(){
                            $('#win1').window("close");

                        });
                        var type=$("#selectType").combobox("getValue");
                        that.couponRenderList(type);
                    }else if(that.elems.optionType1 == "coupon"){  //因为iframe引用，添加券会触发，Iframe中的保存时间，子页面保存完成以后会关闭，父页面 win1
                         isColse=false;
                        $("#addCouponIframe").contents().find(".saveBtn").trigger("click");

                    }
                    if(isColse) {
                        $('#win1').window("close");
                    }
                }


            });

        },
        releaseOpen:function( data){
            var top=$(document).scrollTop()+60;
           $('#winrelease').window({
               title: "完成预览", width: 590, height: 590, top:top,
               left: ($(window).width() - 590) * 0.5
           });
         if(data) {
             $("#winrelease .OnlineQRCodeId .codeimg img").attr("src", data.OnlineQRCodeUrl);
             $("#winrelease .OnlineQRCodeId .downaddress").attr("href", data.OnlineQRCodeUrl);
             $("#winrelease .OnlineQRCodeId .addressinput").val(data.OnlineQRCodeUrl);
             $("#winrelease .OnfflineQRCodeId .codeimg img").attr("src", data.OfflineQRCodeUrl);
             $("#winrelease .OnfflineQRCodeId .downaddress").attr("href", data.OfflineQRCodeUrl);
             $("#winrelease .OnfflineQRCodeId .addressinput").val(data.OfflineQRCodeUrl);

             $('#winrelease').window('open');
             if (self.elems.isloadzclip) {
                 debugger
                 self.elems.isloadzclip = false;
                 //复制地址
                 $(".addrcopy").zclip({
                     path: "/Module/static/js/plugin/ZeroClipboard.swf",
                     copy: function () {
                         return $(this).parents(".address").find(".addressinput").val();
                     },
                     afterCopy: function () {/* 复制成功后的操作 */
                         $.messager.alert("提示", "复制成功！");
                     }
                 });
             }

         }

       },


        selectPrize:function(type){
            var that=this,nodeList=[];
            that.elems.selectType=type;
            switch(type){
                case"share":
                    nodeList=that.prizeListShare ;
                    break;
                case"watch":
                    nodeList=that.prizeListWatch ;
                    break;
                case"reg":
                    nodeList=that.prizeListReg ;
                    break;
            }

            if(nodeList.length>0){
                $("#expandGrid").parents(".taleWarp").show();
            }else{
                $("#expandGrid").parents(".taleWarp").hide();
            }
            $("#expandGrid").datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                 height : 270, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:nodeList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[
                    {field :"PrizeTypeId",title : '序号',width:60,resizable:false,align:'center',
                        formatter:function(value ,row,index){
                            return index+1;
                        }
                    },
                    {field : 'PrizeName',title : '奖品名称',width:90,align:'left',resizable:false},
                    {field : 'PrizeCount',title : '奖品数量',width:60,align:'center',resizable:false},
                    {field: 'CouponTypeID', title: '操作', width: 90, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var str = "<div class='operation'>";
                            /*  str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";*/
                            str += "<div data-index=" + index + " data-flag='delStyle' class='delete opt' title='删除'></div></div>";
                            return str
                        }
                    }
                ]]

            });

        },

        returnPrizeList:function(oldList){
            //奖品格式
            /* PrizeName	String	奖品名称
             PrizeTypeId	String	奖品类型Point,Coupon
             CouponTypeID	String	优惠券类型
             Point	Int	奖励积分数
             PrizeCount	Int	奖品数量*/
            var list=[],objMsg={index:"",msg:"",isAdd:true,list:[]};
            var type=$("#selectType").combobox("getValue");
            $("#prizeListGrid").datagrid('acceptChanges');
            var nodelist=$("#prizeListGrid").datagrid("getChecked");
            if(nodelist.length==0){
                objMsg.isAdd=false;
                objMsg.msg="请至少选择一项作为奖品添加"
            }
            if(type==2){//积分
                for (var i = 0; i < nodelist.length; i++) {
                    if(!nodelist[i].number){
                        objMsg.isAdd=false;
                        objMsg.index=i;
                        objMsg.msg="请填写第"+(objMsg.index+1)+"项的奖品数量";
                    }
                    var obj = {
                        PrizeName: nodelist[i].integral+"积分",
                        PrizeTypeId: "Point",
                        Point: nodelist[i].integral,
                        PrizeCount: nodelist[i].number ? nodelist[i].number : 0
                    };
                    list.push(obj)

                }
            }else {//优惠券

                for (var i = 0; i < nodelist.length; i++) {
                    if(!nodelist[i].PrizeCount){
                        objMsg.isAdd=false;
                        objMsg.index=i;
                        objMsg.msg="请填写第"+(objMsg.index+1)+"项的奖品数量";
                    }
                    var obj = {
                        PrizeName: nodelist[i].CouponTypeName,
                        PrizeTypeId: "Coupon",
                        CouponTypeID: nodelist[i].CouponTypeID,
                        PrizeCount: nodelist[i].PrizeCount ? nodelist[i].PrizeCount : 0
                    };
                    if(nodelist[i].SurplusQty<obj.PrizeCount){
                        objMsg.isAdd=false;
                        objMsg.index=i;
                        objMsg.msg="第"+(objMsg.index+1)+"项的已有券数量不足，请追加券数量";
                    }

                    list.push(obj)

                }
            }
            if(objMsg.isAdd) {
                if (oldList && oldList.length > 0) {   //相同的奖品添加覆盖原有的

                    for (var i = 0; i < oldList.length; i++) {
                        var isAdd = true;
                        for (var j = 0; j < list.length; j++) {
                            if (list[j].PrizeName == oldList[i].PrizeName) {
                                if (oldList[i].CouponTypeID && list[j].CouponTypeID == oldList[i].CouponTypeID) {  //优惠券
                                    list[j].PrizeCount = list[j].PrizeCount; //覆盖原有的 //parseInt(oldList[i].PrizeCount)+parseInt(list[j].PrizeCount); //在原有上面叠加
                                } else if (!oldList[i].CouponTypeID) {     //积分
                                    list[j].PrizeCount = list[j].PrizeCount; //覆盖原有的
                                }


                                isAdd = false;
                            }
                        }
                        if (isAdd) {
                            list.push(oldList[i]);
                        }

                    }
                }
            }else{
                var dataList=$("#prizeListGrid").datagrid("getData").rows
                for(var index=0;index<dataList.length;index++) {
                    $("#prizeListGrid").datagrid('beginEdit', index);
                }
                $("#win .tableWap").find(".textbox.numberbox").css({width:81});
            }
            objMsg.list=list;
            return objMsg;
        },
        //奖品列表
        renderTablePrize:function(){
            var that=this;
            debugger;
            if(that.prizeListHB.length>0){
                $("#prize").parents(".taleWarp").show();
            }else{
                $("#prize").parents(".taleWarp").hide();
            }
            $("#prize").datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                height : 270, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:that.prizeListHB,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[
                    {field :"PrizeTypeId",title : '序号',width:60,resizable:false,align:'center',
                        formatter:function(value ,row,index){
                            return index+1;
                        }
                    },
                    {field : 'PrizeName',title : '奖品名称',width:90,align:'left',resizable:false},
                    {field : 'PrizeCount',title : '奖品数量',width:60,align:'center',resizable:false},
                    {field: 'CouponTypeID', title: '操作', width: 90, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            var str = "<div class='operation'>";
                          /*  str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";*/
                            str += "<div data-index=" + index + " data-flag='delStyle' class='delete opt' title='删除'></div></div>";
                            return str
                        }
                    }
                ]]

            });
        },
        //风格表格加载
        loadThemeList:function(){
            this.loadData.getLEventThemeList(function(data){

                $("#nav02Table").datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, //多选
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.Data.LEventThemeList,
                    sortName : 'brandCode', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    idField : 'Item_Id', //主键字段
                    /*  pageNumber:1,*/
                    /* frozenColumns : [ [ {
                     field : 'brandLevelId',
                     checkbox : true
                     } //显示复选框
                     ] ],*/
                    columns : [[
                        {field : 'ThemeName',title : '风格名称',width:80,resizable:false,align:'left'},
                        {field : 'ImageUrl',title : '图片',width:70,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                  var html="";
                                if(value){
                                    html=' <img src="'+value+'" width="40" height="40"  />'
                                }

                                return html;
                            }

                        },

                        {field: 'TemplateId', title: '操作', width: 100, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                var str = "<div class='operation'>";
                                str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";
                                str += "<div data-index=" + index + " data-flag='delStyle' class='delete opt' title='删除'></div></div>";
                                return str
                            }
                        }
                    ]],



                });
            })

        },

        uploadImg: function (btn, callback) {
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width:"100%",
                //上传的文件类型
                fieldName: 'imgFile',
                isShow:true,
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image',
                afterUpload: function (data) {
                    if (data.error === 0) {
                        if (callback) {
                            debugger  ;
                            callback(btn, data);
                        }
                        $.util.isLoading(true);
                        //取返回值,注意后台设置的key,如果要取原值
                        //取缩略图地址
                        //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                        //取原图地址
                        //var url = KE.formatUrl(data.url, 'absolute');
                    } else {
                        alert(data.message);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            debugger;
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
                $.util.isLoading()
            });


        },
        addUploadImgEvent: function (e) {
            this.uploadImg(e, function (ele, data) {
                //上传成功后回写数据
                        //上传成功后回写数据
                        // debugger;
                        var imgCode = $(ele).data("imgcode");
                        var dom= $("#win").find("[data-imgcode="+imgCode+"]");
                        if(dom.is("img")){
                            dom.attr("src",data.url);
                        }
            });
        },


        //奖品设置
        setPrize:function(type,rowData){

            var that=this;
            debugger;
            that.elems.optionType=type;//"setPrize";
            var top=$(document).scrollTop()+0;
            $("body").eq(0).css("overflow-y","hidden");
            var left=$(window).width() - 1140>0 ? ($(window).width() - 1140)*0.5:80;
            $('#win').window({title:"选择奖品",width:$(window).width(),height:$(window).height(),top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=that.render(that.template.setPrize,rowData);
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent').layout('add',options);
            $('#win').window('open');
            var dom=$('#win');
            $("#selectType").combobox({
                valueField:'id',
                textField:'text',
                data:[{
                    "id":1,
                    "text":"兑换券"
                },{
                    "id":0,
                    "text":"代金券"

                },{
                    "id":2,
                    "text":"积分"
                },{
                    "id":-1,
                    "text":"选择奖品类型",
                    "selected":true
                }],

                onSelect:function(record){
                    var type=record.id;
                    if(type==-1){
                        dom.find(".showPanel").hide();
                        dom.find(".listName").hide();
                    }else{
                        dom.find(".showPanel").show();
                        dom.find(".listName").show();
                    }
                    dom.find(".tableWap");//承载表格的对象
                    var dataListName="";
                   switch(type){
                       case 2:  //积分
                           dataListName="已有积分列表";
                           that.integralRenderList();
                           break
                       case 1:   //兑换券
                           dataListName="已有兑换券列表";
                             that.couponRenderList(type);
                        break;
                       case 0:  //代金券
                           dataListName="已有代金券列表";
                           that.couponRenderList(type);
                           break

                   }
                    debugger;
                   // dom.find(".listName").html(dataListName);
                }
            });




        },


         addNumber:function(type,rowData){
             var that=this;
             debugger;
             that.elems.optionType1=type;//"setPrize";
             var title="";
             if(type=="addIntegralNumber"){
                  title="追加积分数量";
             }
             if(type=="addCouponNumber"){
                 title="追加券数量";

             }
             var top=$(document).scrollTop()+100;
             var left=$(window).width() - 420>0 ? ($(window).width() - 420)*0.5:80;
             $('#win1').window({title:title,width:420,height:260,top:top});
             //改变弹框内容，调用百度模板显示不同内容
             $('#panlconent1').layout('remove','center');
             var html=that.render(that.template.addNumber);
             var options = {
                 region: 'center',
                 content:html
             };

             $('#panlconent1').layout('add',options);
             if(rowData){
                 $("#win2OptionForm").form("load",rowData);
             }
             $('#win1').window('open');
             if(type=="addCouponNumber")  {
                 $("#addIntegralNumber").hide();
             }
         },

        //添加积分，弹框操作
        addIntegral:function(rowData){
            var that=this;
            debugger;
            that.elems.optionType1="integral";//"setPrize";
            var top=$(document).scrollTop()+100;
            var left=$(window).width() - 420>0 ? ($(window).width() - 420)*0.5:80;
            $('#win1').window({title:"添加积分",width:420,height:260,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent1').layout('remove','center');
            var html=that.render(that.template.addIntegral,rowData);
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent1').layout('add',options);
            $('#win1').window('open');


        },
         //缓存积分数据
        setIntegral:function(){},
       //积分数据绑定
        integralRenderList:function(){
            var that=this;
            $("#prizeListGrid").parents(".tableWap").show();
            if(that.integralDataList.length>0){
                $("#win").find(".listName").hide();
            }else{
                $("#win").find(".listName").show();
            }
           $("#win").find(".listName").html("你目前没有积分奖品，请点击新增");
           $("#prizeListGrid").datagrid({
                   method : 'post',
                   iconCls : 'icon-list', //图标
                   singleSelect : false, //多选
                   // height : 332, //高度
                   fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                   striped : true, //奇偶行颜色不同
                   collapsible : true,//可折叠
                   //数据来源
                   data:that.integralDataList,
                  /* sortName : 'brandCode', //排序的列
                   sortOrder : 'desc', //倒序
                    remoteSort : true, // 服务器排序
                   idField : 'Item_Id', //主键字段*/
                   /*  pageNumber:1,*/
               frozenColumns : [ [ {
                   field : 'CouponTypeID',
                   checkbox : true
               } //显示复选框
               ] ],
                   columns : [[
                       {field : 'integral',title : '积分',width:80,resizable:false,align:'left'},
                       {field : 'number',title : '已有数量',width:70,align:'center',resizable:false},

                       {field: 'id', title: '操作', width: 30, align: 'left', resizable: false,
                           formatter: function (value, row, index) {
                               var str = "<div class='operation'>";
                               str += "<div class='opt' data-opttype='addIntegral' data-index="+index+" data-id='"+row.id+"'>追加数量</div>";
                               str += "</div>";
                               return str
                           }
                       }
                   ]],
               onLoadSuccess : function(data) {
                   debugger;

                   $("#prizeListGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
               },

               onCheck:function(){
                   var check= $("#prizeListGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                   if(check){
                       $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                   } else{
                       $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                   }


               } ,
               onUncheck:function(){
                   var check= $("#prizeListGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                   if(check){
                       $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                   } else{
                       $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                   }

               } ,
               onCheckAll:function(){
                   $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");

               } ,onUncheckAll:function(){
                   $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");

               }






           });
        },

        //添加优惠券
        addCoupon:function(type){
            var that=this;
            debugger;
            that.elems.optionType1="coupon";//"setPrize";
            var top=$(document).scrollTop()+0;
            //var left=$(window).width() - 640>0 ? ($(window).width() - 640)*0.5:80;
            $('#win1').window({title:"添加优惠券",width:$(window).width(),height:$(window).height(),top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent1').layout('remove','center');
            var str='<iframe id="addCouponIframe" src="/module/couponManage/addCoupon.aspx?couponType"'+type+' width="100%" height="100%"></iframe>';
            var html=that.render(str);
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent1').layout('add',options);
            $('#win1').window('open');
            $("#addCouponIframe").hide();
            $.util.isLoading();
            $("#addCouponIframe").load(function() {
                $("#addCouponIframe").show(0);
                $.util.isLoading(true);
                $("#addCouponIframe").contents().find("#simpleQuery").hide();
                $("#addCouponIframe").contents().find("#commonNav").hide();
                $("#addCouponIframe").contents().find(".contentArea_vipquery").find(".submitBtn").hide();
                $("#addCouponIframe").contents().find(".commonHeader").hide();
                $("#addCouponIframe").contents().find(".leftsead").hide();
                $("#addCouponIframe").contents().find("#simpleQuery").find("[data-coupontype="+type+"].listBtn").trigger("click");
                $("#addCouponIframe").contents().find(".kf_qycn_com_cckf_icon").hide()

            });




        },

        //券数据绑定
        couponRenderList:function(type,selectList){
            var that=this,list=[];
            if(selectList&&selectList.length>0) {
                list =selectList
            }else{
                if (that.elems.optionType == "setPrize") {   //红包奖励
                    list = that.prizeListHB;
                } else if (that.elems.optionType == "share") {  //分享的奖励
                    list = that.prizeListShare
                } else if (that.elems.optionType == "watch") { // watch的奖励
                    list = that.prizeListWatch
                } else if (that.elems.optionType == "reg") {   //注册的奖励
                    list = that.prizeListReg
                }
            }
            $.util.isLoading();
            that.loadData.getCouponTypeList(function(data){
                var type0List=[],type1List=[];
              debugger;
            if(data.CouponTypeList.length>0){

                $.each(data.CouponTypeList,function(index,filed){
                      for(var i=0;i<list.length; i++){
                           if(list[i].CouponTypeID==filed.CouponTypeID){
                               filed.PrizeCount=list[i].PrizeCount;
                           }
                      }

                      if(filed.ParValue==0){

                          type1List.push(filed);
                      }else{
                          type0List.push(filed);
                      }
                });
            }else{
                $("#prizeListGrid").parents(".tableWap").hide();
            }
debugger;
                $("#prizeListGrid").parents(".tableWap").show();

             if(type==0) {
                 if(type0List.length>0){
                     $("#win").find(".listName").hide();
                 }else{
                     $("#win").find(".listName").show();
                 }
                 $("#win").find(".listName").html("你目前没有代金券奖品，请点击新增");
                 $("#prizeListGrid").datagrid({
                     method: 'post',
                     iconCls: 'icon-list', //图标
                     singleSelect: false, //多选
                     // height : 332, //高度
                     fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                     striped: true, //奇偶行颜色不同
                     collapsible: true,//可折叠
                     //数据来源
                     data: type0List,
                     /* sortName : 'brandCode', //排序的列
                      sortOrder : 'desc', //倒序
                      remoteSort : true, // 服务器排序
                      idField : 'Item_Id', //主键字段*/
                     /*  pageNumber:1,*/
                      frozenColumns : [ [ {
                      field : 'CouponTypeID',
                      checkbox : true
                      } //显示复选框
                      ] ],
                     columns: [[
                         {field: 'CouponTypeName', title: '代金券名称', width:100, resizable: false, align: 'left'},
                         {field: 'ParValue', title: '面值', width: 70, align: 'center', resizable: false},
                         {field: 'ValidityPeriod', title: '失效日期', width: 80, align: 'left', resizable: false},
                         {field: 'SurplusQty', title: '已有数量', width: 70, align: 'center', resizable: false},
                         {
                             field: 'PrizeCount', title: '奖品数量', width: 60, align: 'left', resizable: false,
                             editor: {
                             type: 'numberbox',
                             options: {
                                 min: 1,
                                 precision: 0,
                                 height: 31,
                                 width: 136
                             }
                         }
                         },
                         {
                             field: 'TemplateId', title: '操作', width: 30, align: 'left', resizable: false,
                             formatter: function (value, row, index) {
                                 var str = "<div class='operation'>";
                                 str += "<div class='opt' data-opttype='addCoupon'  data-index="+index+"  data-type="+type+" data-id="+row.CouponTypeID+">追加券数量</div>";
                                 str += "</div>";
                                 return str
                             }
                         }
                     ]],
                     onLoadSuccess : function(data) {
                         debugger;

                         $("#prizeListGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                     },

                     onCheck:function(){
                         var check= $("#prizeListGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                         if(check){
                             $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                         } else{
                             $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                         }


                     } ,
                     onUncheck:function(){
                         var check= $("#prizeListGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                         if(check){
                             $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                         } else{
                             $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                         }

                     } ,
                     onCheckAll:function(){
                         $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");

                     } ,onUncheckAll:function(){
                         $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");

                     }

                 });
                 for(var index=0;index<type0List.length;index++) {
                     if(type0List[index].PrizeCount){
                         $("#prizeListGrid").datagrid('selectRow', index);
                     }
                     $("#prizeListGrid").datagrid('beginEdit', index);
                    /* var ed = $('prizeListGrid').datagrid('getEditor', {index:index,field:'prizeNumber'});
                     $(ed.target).numberbox('setValue', type1List[index].SurplusQty);*/
                 }

             }
             if(type==1) {
                 if(type1List.length>0){
                     $("#win").find(".listName").hide();
                 }else{
                     $("#win").find(".listName").show();
                 }
                 $("#win").find(".listName").html("你目前没有兑换券奖品，请点击新增");
                    $("#prizeListGrid").datagrid({
                        method: 'post',
                        iconCls: 'icon-list', //图标
                        singleSelect: false, //多选
                        // height : 332, //高度
                        fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                        striped: true, //奇偶行颜色不同
                        collapsible: true,//可折叠
                        //数据来源
                        data: type1List,
                        /* sortName : 'brandCode', //排序的列
                         sortOrder : 'desc', //倒序
                         remoteSort : true, // 服务器排序
                         idField : 'Item_Id', //主键字段*/
                        /*  pageNumber:1,*/
                         frozenColumns : [ [ {
                         field : 'CouponTypeID',
                         checkbox : true
                         } //显示复选框
                         ] ],
                        columns: [[
                            {field: 'CouponTypeName', title: '兑换券名称', width: 100, resizable: false, align: 'left'},
                            {field: 'ValidityPeriod', title: '失效日期', width: 70, align: 'left', resizable: false},
                            {field: 'SurplusQty', title: '已有数量', width: 80, align: 'left', resizable: false},
                            {
                                field: 'PrizeCount', title: '奖品数量', width: 60, align: 'left', resizable: false,
                                formatter: function (value, row, index) {
                                    return row.SurplusQty
                                }, editor: {
                                type: 'numberbox',
                                options: {
                                    min: 1,
                                    precision: 0,
                                    height: 31,
                                    width: 80
                                }
                            }
                            },
                            {
                                field: 'TemplateId', title: '操作', width: 30, align: 'left', resizable: false,
                                formatter: function (value, row, index) {
                                    var str = "<div class='operation'>";
                                    str += "<div class='opt' data-opttype='addCoupon'  data-index="+index+"  data-type="+type+" data-id="+row.CouponTypeID+">追加券数量</div>";
                                    str += "</div>";
                                    return str
                                }
                            }
                        ]],
                        onLoadSuccess : function(data) {
                            debugger;

                            $("#prizeListGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        },

                        onCheck:function(){
                            var check= $("#prizeListGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                            if(check){
                                $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                            } else{
                                $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                            }


                        } ,
                        onUncheck:function(){
                            var check= $("#prizeListGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                            if(check){
                                $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                            } else{
                                $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                            }

                        } ,
                        onCheckAll:function(){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");

                        } ,onUncheckAll:function(){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");

                        }

                    });
                 for(var index=0;index<type1List.length;index++) {
                     if(type1List[index].PrizeCount){
                         $("#prizeListGrid").datagrid('selectRow', index);
                     }
                     $("#prizeListGrid").datagrid('beginEdit', index);
                     /*var ed = $('prizeListGrid').datagrid('getEditor', {index:index,field:'prizeNumber'});
                     $(ed.target).numberbox('setValue', type1List[index].SurplusQty);*/

                 }

                }
                setTimeout(function(){
                    $("#win .tableWap").find(".textbox.numberbox").css({width:81});
                },400)
            });

        },

        //加载页面的数据请求
        loadPageData: function (e) {
            var that=this;
            that.loadData.getTemplateDetail(function(data) {
                debugger;
                var ThemeList=data.Data.TemplateThemeList;
                self.TemplateThemeList=data.Data.TemplateThemeList;
                self.loadDataInfo=data.Data;
                 $("#tempName").val(data.Data.TemplateName);
                $("#slider").html("");
                var startSlide=0;
                for(var i=0;i<ThemeList.length;i++) {
                    var html = '<div class="slide" data-index="' + i + '"><div><img src="' + ThemeList[i].ImageURL + '" /><em></em><p>'+ThemeList[i].ThemeName+'</p></div</div>';
                    if (data.Data.CustomerCTWEventInfo && data.Data.CustomerCTWEventInfo.OriginalThemeId == ThemeList[i].ThemeId) {
                        startSlide=i;
                        html = '<div class="slide on" data-index="' + i + '"><div><img src="' + ThemeList[i].ImageURL + '" /><em></em><p>'+ThemeList[i].ThemeName+'</p><div></div>';
                    }

                    $("#slider").append(html);
                    //  $("#slider").append(html);
                }
                startSlide=startSlide>2 ? startSlide : startSlide=0;
               /* $("#slider").bxSlider({
                 slideWidth: 230,
                 infiniteLoop:false,
                 hideControlOnEnd:true,
                 responsive:false,
                 pager:false,
                 minSlides: 1,
                 maxSlides: 3,
                 moveSlides: 1,
                 startSlide: startSlide, //定位显示的页码
                 slideMargin: 9
                 });*/
                $("#slider").bxSlider({
                    pager:false, //分页定位不显示
                    hideControlOnEnd:true,
                    infiniteLoop:true,
                    responsive:true,
                    slideWidth: 230,
                    minSlides: 1,
                    maxSlides: 3,
                    moveSlides: 1,  //每次移动多少
                    startSlide:startSlide,
                });
                  $(".bx-viewport").css({"height":"auto"});

                setTimeout(function(){

                    if(ThemeList.length<3){
                        $("#slider").parents(".bx-wrapper").find(".bx-controls-direction a").hide()
                    } else{
                        $("#slider").parents(".bx-wrapper").find(".bx-controls-direction a").show()
                    }
                    $(".slide").css({"width":"230px"});
                },400);        //不可小于400
                that.setLoadInfo(data.Data);

                //console.info(ThemeList.length);



                that.elems.navigation.find("li").eq(0).trigger("click")
            });
        },

         //设置默认图片
        setDefaultImg:function(eventInfo,spreadList){
            if(eventInfo) {         //nav03的默认图片处理
                var code = eventInfo.DrawMethodCode, dom = $('[data-panel="nav03" ]'), selected = "", selectAll = '[data-interaction]';
                if (eventInfo.InteractionType == 1) { // 关注
                    selected = '[data-interaction="1"]';
                } else if (eventInfo.InteractionType == 2) {   //成交
                    selected = '[data-interaction="2"]';
                }
                /* DZP 大转盘
                 HB   红包
                 QN  问卷
                 QG  抢购/秒杀
                 TG  团购
                 RX  热销*/

                switch (code) {
                    case "DZP":
                        break;
                    case "HB":
                        var domList = dom.find(selected).find(".attention").find("[data-imgcode]");

                        if (eventInfo.GameEventImageList && eventInfo.GameEventImageList.length > 0) {
                            for (var i = 0; i < eventInfo.GameEventImageList.length; i++) {
                                var imgObj = eventInfo.GameEventImageList[i];
                                $.each(domList, function () {
                                    if ($(this).data("imgcode").indexOf(imgObj.BatId) != -1) {
                                        if ($(this).data("edit")) {
                                            $(this).data("default", imgObj.ImageURL)
                                        }
                                    }

                                })
                            }
                        } else {
                            console.info("红包图片数据异常")
                        }
                        break;
                    case "QN":
                        break;
                    case "QG":
                        break;
                    case "TG":
                        break;
                }
            }
            if(spreadList&&spreadList.length>0){     //nav04的默认图片处理
                dom=$('[data-panel="nav04"]');
                for(var i=0;i<spreadList.length;i++){

                    var spreadObj=spreadList[i]
                    switch(spreadObj.SpreadType.toLocaleLowerCase()){
                        case "focus":  //引导关注
                          var  domPanel=dom.find('.phoneWebDiv[ data-type="Focus"]');
                                domPanel.find('[data-imgcode="bgPhone"]').data("default",spreadObj.ImageURL);
                            break;
                    }

                }
            }
        },

        //设置页面的（nav03）赋值
      loadSetPageData:function(eventInfo){
          var code=eventInfo.DrawMethodCode,dom=$('[data-panel="nav03" ]'),selected="",selectAll='[data-interaction]';
          dom.find(selectAll).hide()
           if(eventInfo.InteractionType==1){ // 关注
               selected='[data-interaction="1"]';
          }else if(eventInfo.InteractionType==2){   //成交
               selected='[data-interaction="2"]';
           }
         /* DZP 大转盘
          HB   红包
          QN  问卷
          QG  抢购/秒杀
          TG  团购
          RX  热销*/

          switch(code){
              case "DZP": break;
              case "HB":
                var domList =dom.find(selected).find(".attention").find("[data-imgcode]");

                  if(eventInfo.GameEventImageList&&eventInfo.GameEventImageList.length>0){
                      for(var i=0;i<eventInfo.GameEventImageList.length;i++){
                          var imgObj= eventInfo.GameEventImageList[i];
                      $.each(domList,function(){
                          if($(this).data("imgcode").indexOf(imgObj.BatId)!=-1){
                               if($(this).is("div")){
                                     $(this).data("imgurl",imgObj.ImageURL)
                                   if($(this).data("type")=="bg"){
                                       $(this).css({"backgroundImage":'url('+imgObj.ImageURL+')' });
                                   }
                               }else if($(this).is("img")){
                                   if($(this).data("imgcode").indexOf("ReceiveImageUrl")==-1) {
                                       $(this).attr("src", imgObj.ImageURL);
                                   }

                               }
                          }

                      })
                      }
                  }else{
                      console.info("红包图片数据异常")
                  }
                  break;
              case "QN": break;
              case "QG": break;
              case "TG": break;
          }

          dom.find(selected).show()
      },
        //设置页面的（nav04）赋值
       loadGeneralizePageData:function(spreadSettingList,materialText){
           debugger;
           var dom=$('[data-panel="nav04"]');
           var spreadObj="",domPanel;
            if(spreadSettingList&&spreadSettingList.length>0){
                 for(var i=0;i<spreadSettingList.length;i++){
                         //var SpreadType=spreadSettingList[i].SpreadType;
                     spreadObj=spreadSettingList[i]
                     switch(spreadObj.SpreadType.toLocaleLowerCase()){
                         case  "reg":  //微信图文
                              domPanel=dom.find('.phoneWebDiv[ data-type="Reg"]');
                             if(domPanel.find('[data-view]').length>0) {
                                 $.each(domPanel.find('[data-view]'), function () {
                                     var fieldName=$(this).data("view");
                                       $(this).html(spreadObj[fieldName]);
                                 });
                             }
                             domPanel.find('img').attr("src",spreadObj.ImageURL);


                             break;
                         case "focus":  //引导关注
                              domPanel=dom.find('.phoneWebDiv[ data-type="Focus"]');
                             if(domPanel.find('[data-view]').length>0) {
                                 $.each(domPanel.find('[data-view]'), function () {
                                     var fieldName=$(this).data("view");
                                     $(this).html(spreadObj[fieldName]);
                                 });
                             }
                             if(spreadObj.LeadPageRegPromptText&&spreadObj.LeadPageRegPromptText!="无"){
                                $("#release").find('[data-type="reg"]').trigger("click");
                             }
                             if(spreadObj.LeadPageSharePromptText&&spreadObj.LeadPageSharePromptText!="无"){
                                 $("#release").find('[data-type="share"]').trigger("click");
                             }
                             if(spreadObj.PromptText&&spreadObj.PromptText!="无"){
                                 $("#release").find('[data-type="watch"]').trigger("click");
                             }

                             if(materialText){ //对象存在证明是编辑的
                                 domPanel.find('img.bgPhone').attr("src",spreadObj.BGImageUrl);
                                 domPanel.find('[data-imgcode="bgPhone"]').data("imgurl",spreadObj.BGImageUrl);
                                 domPanel.find('.erWeiMa img').attr("src",spreadObj.LeadPageQRCodeImageUrl);
                                 domPanel.find('.erWeiMa').data("imgurl",spreadObj.LeadPageQRCodeImageUrl)
                             }else{
                                 domPanel.find('[data-imgcode="bgPhone"]').data("imgurl",spreadObj.ImageURL)
                                 domPanel.find('img.bgPhone').attr("src",spreadObj.ImageURL);
                             }
                             if(self.loadDataInfo.CustomerCTWEventInfo&&self.loadDataInfo.CustomerCTWEventInfo.ContactEventList){
                                    var contactEventList=self.loadDataInfo.CustomerCTWEventInfo.ContactEventList;
                                    /* prizeListShare:[],//分享奖励
                                     prizeListWatch:[],//关注奖励
                                     prizeListReg:[],//注册奖励*/
                                   if(contactEventList.length>0){
                                       for(var i=0;i<contactEventList.length; i++){
                                           var code=contactEventList[i].ContactTypeCode;
                                           var list=[];
                                           if(contactEventList[i].ContactPrizeList&&contactEventList[i].ContactPrizeList.length>0){
                                               list=contactEventList[i].ContactPrizeList;
                                           }
                                           switch (code){
                                               case "Share":  //分享
                                                  self.prizeListShare=list;
                                                   break;
                                               case "Focus":  //关注
                                                   self.prizeListWatch=list;
                                                   break;
                                               case "Reg":    //注册
                                                   self.prizeListReg=list;
                                                   break;
                                           }
                                       }

                                   }

                             }
                             break;
                         case "share":  //分享
                             domPanel=dom.find('.phoneWebDiv[ data-type="Share"]').find(".share");
                             if(domPanel.find('[data-view]').length>0) {
                                 $.each(domPanel.find('[data-view]'), function () {
                                     var fieldName=$(this).data("view");
                                     $(this).html(spreadObj[fieldName]);
                                 });
                             }

                              if(materialText){
                                  domPanel.find('img').attr("src",spreadObj.BGImageUrl);
                              }else{
                                  domPanel.find('img').attr("src",spreadObj.ImageURL);
                              }
                             break;
                     }

                 }
            }


           if(materialText){
               spreadObj=materialText;
                domPanel=dom.find('.phoneWebDiv[ data-type="Reg"]');
               if(domPanel.find('[data-view]').length>0) {
                   $.each(domPanel.find('[data-view]'), function () {
                       var fieldName=$(this).data("view");
                       $(this).html(spreadObj[fieldName]);
                   });
               }
               domPanel.find('img').attr("src",spreadObj.CoverImageUrl);//注意图片路径大小写
           }

       },
        setLoadInfo:function(loadData){  //编辑赋值全部页面的值


            if(loadData.CustomerCTWEventInfo){
                $("#tempName").val(loadData.CustomerCTWEventInfo.name);
                self.config.worksId=loadData.CustomerCTWEventInfo.worksId ;
                self.elems.currentStyleId=loadData.CustomerCTWEventInfo.OriginalThemeId;
                var InteractionType=loadData.CustomerCTWEventInfo.InteractionType;
                self.elems.eventInfoType=InteractionType;
                $("[data-interaction].btnItem").removeClass("disable");
                $("[data-interaction="+InteractionType+"].btnItem").trigger("click");
                self.initJevvShow(true,loadData.CustomerCTWEventInfo.DrawMethodCode);
                debugger;
                var eventInfo= loadData.CustomerCTWEventInfo.EventInfo;
                if(InteractionType==1){  //关注


                    eventInfo["InteractionType"]=InteractionType;
                    $(".radioList").find("[data-value="+eventInfo.PersonCount+"].radio").trigger("click");
                    if(eventInfo.DrawMethodCode=="HB"){ //红包
                        if(eventInfo.PrizeList&&eventInfo.PrizeList.length>0){
                            self.prizeListHB=eventInfo.PrizeList;
                        }

                        eventInfo["GameEventImageList"]=eventInfo.ImageList
                    }


                    $("#setOptionForm").form('load',eventInfo);

                    /*$('#startDate').datebox('setValue',eventInfo.BeginTime);
                    $('#endDate').datebox('setValue',eventInfo.EndTime);*/
                } else if(InteractionType==2){

                }
                self.loadSetPageData(eventInfo);
                self.loadGeneralizePageData(loadData.CustomerCTWEventInfo.SpreadSettingList,loadData.CustomerCTWEventInfo.MaterialText);

            } else{
                self.loadGeneralizePageData(loadData.TemplateSpreadSettingList);
                self.setDefaultImg(null,loadData.TemplateSpreadSettingList)
            }

        },
        //获取活动列表
        getLEventInteractionList: function () {
            debugger;
            var that=this;
          this.loadData.getLEventInteractionList(function(data){
              $("#nav03Table").datagrid({

                  method : 'post',
                  iconCls : 'icon-list', //图标
                  singleSelect : false, //多选
                  // height : 332, //高度
                  fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                  striped : true, //奇偶行颜色不同
                  collapsible : true,//可折叠
                  //数据来源
                  data:data.Data.LEventInteractionList,
                  sortName : 'brandCode', //排序的列
                  /*sortOrder : 'desc', //倒序
                   remoteSort : true, // 服务器排序*/
                  idField : 'Item_Id', //主键字段
                  /*  pageNumber:1,*/
                  /* frozenColumns : [ [ {
                   field : 'brandLevelId',
                   checkbox : true
                   } //显示复选框
                   ] ],*/
                  frozenColumns:[[
                      {
                          field : 'ck',
                          title:'全选',
                          checkbox : true
                      }//显示复选框
                  ]],
                  columns : [[
                      {field : 'ThemeName',title : '风格名称',width:60,align:'left',resizable:false},
                      {field : 'InteractionType',title : '目标方案',width:60,align:'left',resizable:false,
                          formatter: function (value, row, index) {
                      var str=""
                              if(value==1){
                                  str="游戏";
                              }else if(value==2){
                                  str="促销"
                              }
                      return str
                  }
                      },

                      {field : 'DrawMethodName',title : '活动工具',width:60,align:'left',resizable:false},
                      {field : 'ActivityName',title : '活动名称',width:60,align:'left',resizable:false},
                      {field: 'InteractionId', title: '操作', width: 100, align: 'left', resizable: false,
                          formatter: function (value, row, index) {
                              var str = "<div class='operation'>";
                              str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";
                              str += "<div data-index=" + index + " data-flag='delActivity' class='delete opt' title='删除'></div></div>";
                              return str
                          }
                      }
                      ]]

              });





            //分页

            kkpager.generPageHtml({
                pno: page?page+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPageCount,
                totalRecords: data.totalCount,
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


            /*if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }*/
          });
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.start = (currentPage-1)*15;
            that.loadData.getCommodityList(function(data){
                that.renderTable(data);
            });
        },
        //图文编辑
        imgTextEdit:function(rowData){
            var that=this;
            that.elems.optionType="imgText";
            var top=$(document).scrollTop()+60;
            var left=($(window).width()-680)*0.5>0?($(window).width()-680)*0.5:80;
            var title="更改" ;

            $('#win').window({title:title,width:680,height:670,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=that.render(that.template.imgText,rowData);
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent').layout('add',options);
            $('#win').window('open');

        },
        //分享编辑
        shareEdit:function(rowData){
        var that=this;
        that.elems.optionType="shareInfo";
        var top=$(document).scrollTop()+60;
        var left=($(window).width()-680)*0.5>0?($(window).width()-680)*0.5:80;
        var title="更改" ;

        $('#win').window({title:title,width:600,height:670,top:top});
        //改变弹框内容，调用百度模板显示不同内容
        $('#panlconent').layout('remove','center');
        var html=that.render(that.template.shareInfo,rowData);
        var options = {
            region: 'center',
            content:html
        };

        $('#panlconent').layout('add',options);
        $('#win').window('open');

    },
        //分享编辑
        textEdit:function(rowData){
            var that=this;
            that.elems.optionType="textInfo";
            var top=$(document).scrollTop()+60;
            var left=($(window).width()-680)*0.5>0?($(window).width()-680)*0.5:80;
            var title="更改" ;

            $('#win').window({title:title,width:600,height:400,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=that.render(that.template.editText,rowData);
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent').layout('add',options);
            $('#win').window('open');

        },
        //图片编辑
        imgEdit:function(rowData,imgCode){
            var that=this;
            that.elems.optionType="imgEdit";
            that.elems.ImgCode=imgCode;
            var top=$(document).scrollTop()+60;
            var left=($(window).width()-760)*0.5>0?($(window).width()-760)*0.5:80;
            var title="更改图片" ;

            $('#win').window({title:title,width:670,height:600,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=that.render(that.template.imgEdit,rowData);
            var options = {
                region: 'center',
                content:html
            };

            $('#panlconent').layout('add',options);
            $('#win').window('open');

        },



         imgLoad:function(me,obj){
        if(me.is("img")){
            me.attr("src",obj.imageUrl)
        }else if(me.hasClass("jsUploadBtn")){
            me.data("imgObj",obj);
        } else if(!me.hasClass("btnText")){
            me.css({"backgroundImage":'url('+obj.imageUrl+')' });
        }
    },


        setCTWEvent:function(){

          var pram=[],that=this;

            var    CTWEventId=that.loadData.args.CTWEventId||that.loadDataInfo.CTWEventId;
            pram.push({name:"CTWEventId",value:CTWEventId});
            pram.push({name:"ActivityGroupId",value:that.loadDataInfo.ActivityGroupId});
          var  OriginalThemeId="",index=null ;
            index=$("#slider").find(".slide.on").data("index");
            OriginalThemeId=that.TemplateThemeList[index].ThemeId;   //选中风格的id
            var templateObj=that.TemplateThemeList[index];//选中风格对象
              //模板的基础数据
            pram.push({name:"TemplateImageURL",value:that.loadDataInfo.ImageURL});
            pram.push({name:"TemplateName",value:$("#tempName").val()});

            pram.push({name:"OriginalThemeId",value:OriginalThemeId});
            var InteractionType=$(".btnItem.on").data("interaction")
            pram.push({name:"InteractionType",value:InteractionType});
            var OriginalLeventId="",DrawMethodCode="",EventThemeInfo={}; // 原始游戏或促销活动Id,
            // DrawMethodCode  游戏或促销方式DZP(大转盘),HB(红包),QN(问卷),QG(抢购/秒杀),TG(团购),RX(热销)
            // EventThemeInfo   商户风格信息
           var list=that.TemplateThemeList[index].EventInteractionList;
           if(list&&list.length){
               $.each(list,function(index,filed){
                   if(filed.InteractionType==InteractionType){
                       OriginalLeventId=filed.LeventId;
                       DrawMethodCode=filed.DrawMethodCode;
                       //EventThemeInfo=filed;
                   }
               });

           }
            templateObj["worksId"]= self.elems.editor1.worksId;
            pram.push({name:"EventThemeInfo",value:templateObj});

            pram.push({name:"DrawMethodCode",value:DrawMethodCode});
            pram.push({name:"OriginalLeventId",value:OriginalLeventId});


            var  GameEventInfo={}; //游戏活动信息
            if(DrawMethodCode=="HB") {
                if($('#startDate').datebox('getValue').length==0||$('#startDate').datebox('getValue').length==0||that.prizeListHB.length==0) {
                    alert("设置未完成，请完成设置");
                    that.elems.navigation.find("li").eq(2).trigger("click");
                    return false;
                }

                GameEventInfo["BeginTime"] = $('#startDate').datebox('getValue');
                GameEventInfo["EndTime"] = $('#endDate').datebox('getValue');

                GameEventInfo["PersonCount"] = $('[data-name="HB"].radio.on').data("value");
                if (GameEventInfo["PersonCount"] == 1) {
                    //
                    GameEventInfo["LotteryNum"] = $("#frequency").numberbox("getValue");
                }
                var imgList = [];
                imgList.push({BatId: "BackGround", ImageURL: $('[data-imgcode="BackGroundImageUrl"]').data("imgurl")})
                imgList.push({BatId: "Logo", ImageURL: $('[data-imgcode="LogoImageUrl"]').data("imgurl")})
                imgList.push({BatId: "NotReceive", ImageURL: $('[data-imgcode="NotReceiveImageUr"]').data("imgurl")})
                imgList.push({BatId: "Receive", ImageURL: $('[data-imgcode="ReceiveImageUr"]').data("imgurl")})

                GameEventInfo["ImageList"] = imgList;
                GameEventInfo["PrizeList"] = that.prizeListHB;
            }

            pram.push({name:"GameEventInfo",value:GameEventInfo});

           /* Title	String	标题
            BGImageUrl	String	背景图片
            Summary	String	概要
            PromptText	String	提示文字
            LeadPageQRCodeImageUrl	String	引导页二维码图片
            LeadPageSharePromptText	String	引导页分享项提示
            LeadPageFocusPromptText	String	引导页关注提示*/
            var SpreadSettingList=[];//推广设置(分享，关注)
            var SpreadSetting={
                SpreadType:"Share",
                Title:$('[data-tabname="tab02"] .share').find('[data-view="Title"]').html(),
                Summary:$('[data-tabname="tab02"] .share').find('[data-view="Summary"]').html(),
                BGImageUrl:$('[data-tabname="tab02"] .share').find('img').attr("src"),

            }

            SpreadSettingList.push(SpreadSetting)

             SpreadSetting={
                SpreadType:"Focus",
                Title:"",
                Summary:"",
                BGImageUrl: $('[data-tabname="tab03"]').find('[data-imgcode="bgPhone"]').data("imgurl"),
                 PromptText:$('[data-tabname="tab03"]').find('[data-view="PromptText"]').html(),
                 LeadPageQRCodeImageUrl:$('[data-tabname="tab03"] .erWeiMa').find('img').attr("src"),   //引导页二维码图片
                 LeadPageSharePromptText:"分享有奖",  //引导页分享项提示
                /* LeadPageFocusPromptText:"关注有奖",   //引导页关注提示*/
                 LeadPageRegPromptText:"注册有奖"     //引导注册提示
            };

            var ContactPrizeList=[];//触点活动奖励
          if($("#release").find('[data-type="share"]').hasClass("on")){    //分享
              ContactPrizeList.push({ContactTypeCode:"Share",PrizeList:that.prizeListShare});
            }else{
              SpreadSetting.LeadPageSharePromptText="";
              ContactPrizeList.push({ContactTypeCode:"Share",PrizeList:[]});
          }
            if($("#release").find('[data-type="watch"]').hasClass("on")){//引导关注
                ContactPrizeList.push({ContactTypeCode:"Focus",PrizeList:that.prizeListWatch});
            }else{
                SpreadSetting.PromptText="";
                ContactPrizeList.push({ContactTypeCode:"Focus",PrizeList:[]});
            }
            if($("#release").find('[data-type="reg"]').hasClass("on")){  //注册
                ContactPrizeList.push({ContactTypeCode:"Reg",PrizeList:that.prizeListReg});
            }else{
                SpreadSetting.LeadPageSharePromptText="";
                ContactPrizeList.push({ContactTypeCode:"Reg",PrizeList:[]});
            }

            SpreadSettingList.push(SpreadSetting);
            pram.push({name:"SpreadSettingList",value:SpreadSettingList});

            pram.push({name:"ContactPrizeList",value:ContactPrizeList});
            /* Title	String	标题
             ImageUrl	String	图片URL
             OriginalUrl	String	原文连接
             Text	String	文本内容*/
            var  MaterialText={}   ;//图文素材
            if(self.loadDataInfo.CustomerCTWEventInfo){
                pram.push({name:"MappingId",value:self.loadDataInfo.CustomerCTWEventInfo.MappingId});
                pram.push({name:"OnlineQRCodeId",value:self.loadDataInfo.CustomerCTWEventInfo.OnlineQRCodeId});
                pram.push({name:"OfflineQRCodeId",value:self.loadDataInfo.CustomerCTWEventInfo.OfflineQRCodeId});

                if(self.loadDataInfo.CustomerCTWEventInfo.MaterialText) {
                    MaterialText["TextId"] = self.loadDataInfo.CustomerCTWEventInfo.MaterialText.TextId;
                }
            }


            var title=$('[data-tabname="tab01"]').find('[data-view="Title"]').html();
            var summary=$('[data-tabname="tab01"]').find('[data-view="Summary"]').html();
            var imgUrl= $('[data-tabname="tab01"]').find('img').attr("src");
            MaterialText["Title"]=title;
            MaterialText["ImageUrl"]=imgUrl;
            MaterialText["Text"]=summary;

            pram.push({name:'MaterialText',value:MaterialText});

           // 风格 设置的的id
            self.saveJevvSHow(function(){       //先保存已经更改的风格，提交保存后台。
                pram.push({name:"worksId",value:self.elems.editor1.worksId});
                $.util.isLoading();
                that.loadData.operation(pram,"add",function(data){
                    that.releaseOpen(data.Data);

                   /* if(data.Data&&data.Data.CTWEventId) {
                        that.loadData.args.CTWEventId = data.Data.CTWEventId
                    }*/

                });

            }) ;
            //pram.push({name:"worksId",value:self.elems.editor1.worksId});
            //$.util.isLoading();
            //that.loadData.operation(pram,"add",function(data){
            //    that.releaseOpen(data.Data);
            //});

        },
        loadData: {
            args: {
                TemplateId:"",
                InteractionId:"",//活动id
                ThemeId:"" ,  //风格id
                pageIndex:0,
                pageSize:10
            },

            getLEventTemplateByID: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:'GetLEventTemplateByID',
                        TemplateId:this.args.TemplateId

                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }
                    }
                });
            },
            getTemplateDetail: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:"CreativityWarehouse.MarketingActivity.GetTemplateDetail",
                        TemplateId:this.args.TemplateId,
                        CTWEventId:this.args.CTWEventId
                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }


                    }
                });
            },

            //获取活动
            getLEventInteractionList:function(callback){
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:'GetLEventInteractionList',
                        PageIndex:this.args.pageIndex,
                        PageSize:this.args.pageSize,
                        TemplateId:this.args.TemplateId

                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }
                    }
                });
            },
            //获取风格
            getLEventThemeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:'GetLEventThemeList',
                        PageIndex:0,
                        PageSize:10,
                        TemplateId:this.args.TemplateId

                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }
                    }
                });

            },

            //获取优惠券列表
            getCouponTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'Marketing.Coupon.GetCouponTypeList',
                        CouponTypeName:"",
                        ParValue:"",
                        PageIndex:1,
                        PageSize:1000000

                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                prams.data["TemplateId"]=this.args.TemplateId;
                var    CTWEventId=self.loadData.args.CTWEventId||self.loadDataInfo.CTWEventId;
                pram.push({name:"CTWEventId",value:CTWEventId});
                $.each(pram, function (index, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value;
                    }
                });
                switch(operationType){
                    case "jvveShow":  prams.data.action="CreativityWarehouse.MarketingActivity.GetLingKinSign"; break;//签名验证
                    case "add":prams.data.action="CreativityWarehouse.MarketingActivity.SetCTWEvent";break; //保存全部
                    case "addCouponNumber":prams.data.action="Marketing.Coupon.SetCoupon";  // 追加数量
                        break;
                    case "changeStatus":prams.data.action="CreativityWarehouse.MarketingActivity.ChangeCTWEventStatus";  // 追加数量
                        break;
                   /* case "add":prams.data.action="SetLEventTemplate"; break;  //设置主题
                    case  "addStyle":prams.data.action="SetLEventTheme";
                        prams.data["ThemeId"]=this.args.ThemeId;

                        break;//设置风格

                    case  "delStyle":prams.data.action="DeleteLEventTheme";break; //删除风格
                    case  "addActivity":prams.data.action="SetLEventInteraction";
                        prams.data["InteractionId"]=this.args.InteractionId;
                        break;//设置活动

                    case  "delActivity":prams.data.action="DeleteLEventInteraction";break;//删除 活动
                    case  "setImg":prams.data.action="SetObjectImages"; break;     //设置图片id
                    case "GetLEventList":prams.data.action="GetLEventDropDownList";break;   //获取活动工具
                    case "select":prams.data.action="GetMarketingList";  //获取活动
                        prams.data["PageIndex"]= 0;
                        prams.data["PageSize"]=1000000;
                        break;
                    case "setSpread":prams.data.action="CreateSpreadSetting";break;   //设置推广
                    case "UpdateSpread":prams.data.action="UpdateSpreadSetting";break;   //设置推广
                    case "getSpread":prams.data.action="GetSpreadSettingList";break;   //获取推广*/

                }


                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("提示",data.Message);
                        }
                    }
                });
            }


        },

        setInteractionType:{
          type:"1"|"2",



        },


    };
    var self=page;
    page.init();

});

