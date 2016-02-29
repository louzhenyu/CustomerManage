define(['jquery','tools','template', 'easyui'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
            $(".panelMenu").height($(window).height());
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".menuTitle","click",function(e){
                $(this).toggleClass("on");
                $(this).parent().find(".menuUl").stop().slideToggle("slow");

            });
            that.elems.sectionPage.delegate(".menuUl li","mouseleave",function(e){
                if(!$(this).find("p").hasClass("test")){$(this).find("p").removeClass("on");}
            });
            that.elems.sectionPage.delegate(".menuUl li","mouseenter",function(e){
                    if(!$(this).find("p").hasClass("test")){$(this).find("p").addClass("on");}
            });
            that.elems.sectionPage.delegate(".menuUl li","click",function(e) {
                $(this).parents(".menuUl").find("p").removeClass("on").removeClass("test");

                var data = $(this).find("p").data();
                var tab = $('#tabPanel').tabs('getTab', data["name"]);
                var name = $(this).find("p")[0].className;

                if (data['href']) {
                    $(".loading").show();
                    var url = window.statementUrl + data['href'] + "&fr_username=" + window.fsr_userName + "&fr_password=" + window.fsr_passWord + "&user_id=" + window.adminUserID;
                    if (tab) {
                        $('#tabPanel').tabs('select', data["name"])
                    } else {
                        $('#tabPanel').tabs('add', {
                            title: data["name"],
                            iconCls: name,
                            //href:"http://bs.chainclouds.cn/WebReport/ReportServer?reportlet=demo/chart/axis.cpt&op=fs_load&cmd=sso&fr_username=admin&fr_password=i15617945",
                            content: '<iframe width="' + ($("#section").width() - $(".panelMenu").width() - 10) + 'px" id="iframe" height="' + ($("#section").height() - 35) + 'px" src="' + url + '"></iframe>',
                            closable: true

                        });

                    }
                    $(".tabs").height(32);
                    //$("#content-container").remove();
                    $(".tabs-panels").height($("#section").height());
                    $(this).find("p").addClass("on").addClass("test");
                    if (!iframe.readyState || iframe.readyState == "complete") {
                        $(".loading").hide();
                    }
                } else{
                    $(this).toggleClass("show");
                    $(this).find("ul").slideToggle("slow");
                }

                $.util.stopBubble(e);
            });
            $('#tabPanel').tabs({
                width:$("#section").width()-$(".panelMenu").width()-10,
                height:$(window).height()-80 ,
                onSelect:function(title,index){
                    var target = this;
                    var panel=$(target).tabs('getTab',index);
                    $(this).find(".tabs li").find(".tabs-icon").removeClass("on");
                    $(this).find(".tabs li.tabs-selected").find(".tabs-icon").addClass("on");
                    $(this).find(".tabs li a.tabs-inner").css({height:'32px',"line-height":"33px"});
                    $(this).find(".tabs li.tabs-selected a.tabs-inner").css({height:'33px'});
                    $(".tabs").height(32);
                }

            });

        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){

        },

        //加载页面的数据请求
        loadPageData: function (e) {
            var that=this;
            this.loadData.getMenuList(function(data){
                 debugger;
                 var menuList=data.Data.currentMenu;
                if(menuList&&menuList.SubMenuList.length>0){
                    menuList=menuList.SubMenuList;
                }
                $("#menList").html("");
                $.each(menuList,function(index,menuObj) {
                    var menuDiv = $('<div class="menuBody"><div class="menuTitle"><img src="images/Cart.png" width="16" height="16">' + menuObj.Menu_Name + '</div></div>');
                    //var menuChildren=menuDiv.find(".menuBody");
                    var childrenList = "<ul class='menuUl'>";
                    var iconList = {"0": "a", "1": "b", "2": "c", "3": "d", "4": "e", "5": "f"};
                    var str = ""
                    for (var i = 0; i < menuObj.SubMenuList.length; i++) {
                        var children = menuObj.SubMenuList[i];
                        var iconIndex = i % 6;
                        var icon = iconList[iconIndex];

                        if (menuObj.SubMenuList[i].SubMenuList && menuObj.SubMenuList[i].SubMenuList.length > 0) {
                            str += '<li class="menuLi"><p  class="icon_Add icon" data-name="' + children.Menu_Name + '">' + children.Menu_Name+'</p>';
                           var html=""
                            for (var j = 0; j < menuObj.SubMenuList[i].SubMenuList.length; j++) {
                                children = menuObj.SubMenuList[i].SubMenuList[j];
                                iconIndex = j%6;
                                icon = iconList[iconIndex];
                                html += '<li><p data-href="' + children.Url_Path + '" class="icon_' + icon + ' icon on" data-name="' + children.Menu_Name + '">' + children.Menu_Name + '</p></li>';
                            }
                            str+= '<ul>'+html+'</ul></li>';
                        } else {
                            str += '<li class="menuLi"><p data-href="' + children.Url_Path + '" class="icon_' + icon + ' icon" data-name="' + children.Menu_Name + '">' + children.Menu_Name + '</p></li>';

                        }

                    }
                    childrenList += str
                    childrenList += "</ul>";
                    debugger;
                    menuDiv.append(childrenList);
                    $("#menList").append(menuDiv);
                    that.loadData.args.menuListString = "";
                })
               if($(".menuTitle").length>0){
                   $(".menuTitle").eq(0).trigger("click");
               }
            });
        },





        loadData: {
            args: {
               menuCode:"statementList",
                menuListString:""
            },
           getMenuList:function(callback){
               $.util.ajax({
                   url: "/ApplicationInterface/Vip/VipTags.ashx",
                   data:{
                       action: "GetMenusByPMenuCode",
                       menu_code:"jysj_jysj_jysj"
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
           }


        }

    };
    page.init();
});

