
define(['jquery','customerTemp','kindeditor', 'kkpager','artTemplate','tools'], function ($, temp) {

    /*RGB颜色转换为16进制*/
    String.prototype.colorHex = function(){
        var that = this;
        if(/^(rgb|RGB)/.test(that)){
            var aColor = that.replace(/(?:\(|\)|rgb|RGB)*/g,"").split(",");
            var strHex = "#";
            for(var i=0; i<aColor.length; i++){
                var hex = Number(aColor[i]).toString(16);
                if(hex === "0"){
                    hex += hex;
                }
                if(hex.length==1){
                    hex="0"+hex
                }
                strHex += hex;
            }
            if(strHex.length !== 7){
                strHex = that;
            }
            return strHex;
        }else if(reg.test(that)){
            var aNum = that.replace(/#/,"").split("");
            if(aNum.length === 6){
                return that;
            }else if(aNum.length === 3){
                var numHex = "#";
                for(var i=0; i<aNum.length; i+=1){
                    numHex += (aNum[i]+aNum[i]);
                }
                return numHex;
            }
        }else{
            return that;
        }
    };


//-------------------------------------------------

    /*16进制颜色转为RGB格式*/
    /*
    * isLong 为
    *
    */
    String.prototype.colorRgb = function(isShort){
        var reg = /^#([0-9a-fA-f]{3}|[0-9a-fA-f]{6})$/;
        var sColor = this.toLowerCase();
        if(sColor && reg.test(sColor)){
            if(sColor.length === 4){
                var sColorNew = "#";
                for(var i=1; i<4; i+=1){
                    sColorNew += sColor.slice(i,i+1).concat(sColor.slice(i,i+1));
                }
                sColor = sColorNew;
            }
            //处理六位的颜色值
            var sColorChange = [];
            for(var i=1; i<7; i+=2){

                sColorChange.push(parseInt("0x"+sColor.slice(i,i+2)));
            }
            if(isShort){
                return "rgb(" + sColorChange.join(",")+")";
            }
            return "rgba(" + sColorChange.join(",") + ",0.6)";
        }else{
            return sColor;
        }
    };
    //上传图片
        KE = KindEditor;
    var page = {
        template: temp,
        url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
        elems: {
            categorySelect: $("#categorySelect"),
            sectionPage:$("#section"),
            pageInfo: $("#pageInfo"),
            paramsList: $("#paramsList"),
            mask: $(".ui-mask"),
            mainMenuList:$(".mainMenuList"),
            categoryLayer: $("#categoryPopupLayer"), //选择商品分类弹框
            productLayer: $("#productPopupLayer"),   //选择商品弹框
            deploy:$("#deploy")
                               },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            var  self=this;
         /*   self.elems.sectionPage.find(".jsUploadBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });*/
            this.initEvent();
            this.categoryLayerEvent();
            this.productLayerEvent();
            this.loadPageData();
            this. GetLevel1ItemCategory();
            $(".panelMenu").height($(window).height());
            template.helper('$menuToC', function (number){
            switch (number){
                            case 0:  return "菜单一"; break;
                            case 1:  return "菜单二"; break;
                            case 2:  return "菜单三"; break;
                            case 3:  return "菜单四"; break;
                            case 4:  return "菜单五"; break;
                            case 5:  return "菜单六";  break;
                            case 6:  return "菜单七";  break;
                            case 7:  return "菜单八";  break;
                            case 8:  return "菜单九"; break;
                            case 9:  return "菜单十";    break;
                            case 10:  return "菜单十一";break;
                            case 11:  return "菜单十二";  break;
                            case 12:  return "菜单十三";  break;
                            case 13:  return "菜单十四"; break;
                        }
            });
            template.helper("$returnImgName",function(str){
                return str.match(/\/(\w+\.(?:png|jpg|gif|bmp))$/i)[1];
            })
        },
        initPage : function(key,pageParam) {
            try {
                //有用户保存的参数时 把用户设置的值置为默认值，没有的时候取json中的默认值，并设用户变量param对象保存数据
                var param  = JSON.parse(pageParam);
                $(".screen").find(".page").hide();
                $("[data-key='"+key+"']").show();
                if(key=="$HomeIndex") {

                    debugger;
                    var animateDirection = "";
                    //初始化页面内容
                    if (param) {
                        //设置title
                        if (param.title) {
                            document.title = param.title;
                        }
                        //设置Logo
                        if (param.logo) {
                            $("#logo").show();
                            $("#logo").attr("src", param.logo);
                        } else {
                            $("#logo").hide();
                        }
                        //背景图
                        //if (param.backgroundImage) {
                            $("#backgroundImg").attr("src", param.backgroundImage);
                        //}

                        //菜单项

                        if (typeof param.links == 'string') {
                            //兼容系统生成的config文件 参数为字符串类型
                            try {

                                param.links = eval('(' + param.links + ')');

                            } catch (e) {

                                param.links = [];
                            }
                        }

                        var navs = $("#navList li");
                        for (var i = 0, length = param.links.length; i < length; i++) {
                            var item = param.links[i];
                            if (item.isShow == false || item.isShow == 'false') {
                                $($(navs[i + 1])).get(0).style.visibility = "hidden";
                                continue;
                            } else{
                                $($(navs[i + 1])).get(0).style.visibility = "visible";
                            }
                            var doma = $($(navs[i + 1])).find("a");
                            if (param.littleImage && param.littleImage.imgUrl) {
                                var size = param.littleImage.size ? param.littleImage.size : "15px";
                                doma.css({
                                    "background": "url(" + param.littleImage.imgUrl + ") no-repeat 10px 18px",
                                    "background-size": size + " " + size
                                });
                            }

                            doma.attr("href", "javascript:void(0)");
                            //设置中文名称
                            $($(navs[i + 1]).find("span").get(0)).html(item.title);
                            $($(navs[i + 1]).find("span").get(1)).html(item.english);
                        }
                        //动画方向
                        animateDirection = param.animateDirection;
                        //增加百度直达功能
                        if (param.baiduEnter) {

                            var bdObj = param.baiduEnter[0];
                            if (bdObj.baiduScript) {
                                bdObj.baiduScript = bdObj.baiduScript.replace(/&quot;/g, '"').replace(/&nbsp;/g, " ");//.replace(/\//g,"\\\/");
                                $("body").append("<div style='width:100%;position:fixed;bottom:0px;height:35px;' id='baiduScript'></div>");

                                var iframe = document.createElement("iframe");
                                //iframe.setAttribute("src","test.html");
                                iframe.src = '';
                                iframe.id = 'bdIframe';
                                iframe.style.width = "100%";
                                iframe.style.height = "100%";
                                $("#baiduScript").append(iframe);
                                $("#bdIframe").contents()[0].write(bdObj.baiduScript);
                                $("#bdIframe").contents()[0].close();
                            }
                        }

                    }
                    if(!param["animation"]||param["animation"]=="执行") {
                        var first = alice.init();
                        var move = {};
                        if (animateDirection == "right") {
                            move = {
                                special: true,    //针对源代码进行修改的   为了兼容以前的代码
                                direction: animateDirection ? animateDirection : 'right',
                                start: -304,
                                end: 1
                            };
                        }
                        if (animateDirection == "down") {
                            move = {
                                special: true,
                                direction: animateDirection ? animateDirection : 'down',
                                start: -480,
                                end: 1
                            };
                        }
                        if (animateDirection == "up") {
                            move = "up";
                        }
                        if (animateDirection == "left") {
                            move = "left";
                        }
                        //debugger;
                        first.slide({
                            elems: ".a_move",
                            move: move,
                            duration: {
                                "value": "1000ms",
                                "randomness": "30%",
                                "offset": "100ms"
                            },
                            "overshoot": "5"
                        });
                        var logo = alice.init();
                        logo.slide({
                            "elems": ".boder",
                            "duration": {
                                "value": "1000ms",
                                "randomness": "20%"
                            },
                            "timing": "easeOutQuad",
                            "iteration": "1",
                            "direction": "normal",
                            "playstate": "running",
                            "move": "up",
                            "rotate": "-360%",
                            "fade": "in",
                            "scale": {
                                "from": "10%",
                                "to": "100%"
                            },
                            "shadow": "true",
                            "overshoot": 1,
                            "perspective": "none",
                            "perspectiveOrigin": {
                                x: "50%",
                                y: "50%"
                            },
                            "backfaceVisibility": "visible"
                        });
                    }
                }
                if(key=="$HomeIndex3"){
                    debugger
                    self.page3Key.initLoad(param);
                }
                if(key=="$HomeIndex2"){
                    debugger
                    self.page2Key.initLoad(param);

                }
                if(key=="$HomeIndex1") {
                    if(param){
                        //设置背景图
                        if(param.backgroundImg){
                            $("#theBg").show();
                            $("#theBg").attr("src",param.backgroundImg);
                        }else{
                            $("#theBg").hide();
                        }
                        //菜单项
                        if(param.links){
                            var navs=$("#menu a").hide();
                            var length=param.menuLength||param.links.length;
                            for(var i=0;i<length;i++){
                                var item=param.links[i];
                                var doma=$(navs[i]).show();
                                if(param.ColorSchemes&&param.ColorSchemes.bg){
                                    doma.css({
                                        "background":param.ColorSchemes.bg.colorRgb(),
                                        "color":param.ColorSchemes.color
                                        //"opacity":0.6
                                    });
                                }
                                item.toUrl="javascript:void(0)";
                                doma.html(item.title);
                                doma.attr("href",item.toUrl);

                            }
                        }
                    }
                    $(".menu a").addClass("tm");
                }
            } catch (error) {

            }

        },
        timelyLoadPage:function(notCarryAnimation){
            self.param={};
            self.param["animation"]=notCarryAnimation||true;
            $(".jsParamValue").each(function(){
                var key=$(this).data("key");

                if($(this).find(".menuPanel").length==0){ //不是菜单的保存
                    self.param[key]=$(this).data("value");
                }
                //多张背景图片处理
                if($(this).find(".imgOption").length>0){
                    var imgList=[]
                    $(this).find(".imgOption").each(function(){
                        imgList.push({"imgUrl":$(this).data("value")});
                    })
                    self.param[key]=imgList;
                }
                if($(this).find(".menuProperty").length>0){//菜单项保存
                    var links=[];

                    $(this).find(".menuProperty").each(function(){
                        var obj={};
                        $(this).find(".commonInputWrap").each(function(index){

                            if($(this).data("key")=="toUrl"){
                                var toUrlObj={};
                                toUrlObj=$(this).find(".jsAreaItem").data();
                                toUrlObj["id"]=toUrlObj["id"].toString();
                                toUrlObj["url"]=$(this).find(".jsAreaItem .jsNameInput").val();
                                toUrlObj["name"]=$(this).find(".jsAreaItem .jsNameInput").val();

                                obj[$(this).data("key")]=JSON.stringify(toUrlObj).replace(/,/g,"&&");
                            } else{
                                obj[$(this).data("key")]=$(this).data("value").toString();
                            }
                        });
                        links.push(obj);
                    });
                    self.param[key]=links;
                }
            });
            var key =$(".mainMenuList").find("li.on").data("key");
            self.initPage(key,JSON.stringify(self.param));
        },
        page2Key:{

        elements: {
            homeWrapper:$('#homeWrapper1'),
            indexNav : $('.indexNav2')
        },
        onPageLoad: function(param) {

            //当页面加载完成时触发

            this.initLoad(param);
            this.initEvent();


        }, //加载数据
        initLoad: function(param) {
            var self = this;
            self.onBgSize();
            window.onresize = self.onBgSize;

            if(param.links){
                var linksDom=$(".indexNav2 a");
                var length=param.links.length;
                for(var i=0;i<length;i++){
                    var item=param.links[i];
                    item.toUrl="javascript:void(0)";
                    var doma=$(linksDom[i]);

                    if(item.backgroundImg){
                        doma.css({
                            "background":"rgba(255,255,255,0.6) url("+item.backgroundImg+") no-repeat",
                            "background-size":"34px 30px",
                            "background-position":"center 15px"

                        });
                        if( param.LeftColorSchemes&&param.LeftColorSchemes.bg){
                            doma.css({
                                "background":param.LeftColorSchemes.bg.colorRgb()+" url("+item.backgroundImg+") no-repeat",
                                "background-size":"34px 30px",
                                "background-position":"center 15px",
                                "color": param.LeftColorSchemes.color
                            });
                        }
                    }else{
                        doma.css({
                            "background":"rgba(255,255,255,0.6) no-repeat",
                            "background-size":"34px 30px",
                            "background-position":"center 15px"

                        });

                        if( param.LeftColorSchemes&&param.LeftColorSchemes.bg){
                            doma.css({
                                "background":param.LeftColorSchemes.bg.colorRgb()+"no-repeat",
                                "background-size":"34px 30px",
                                "background-position":"center 15px",
                                "color": param.LeftColorSchemes.color
                            });
                        }
                    }
                    doma.html(item.title);
                    doma.attr("href",item.toUrl);
                }
            }
            if(param.menus){
                var menusDom=$(".menuWrap a");
                var length=param.menus.length;
                for(var i=0;i<length;i++){
                    var item=param.menus[i];
                    var doma=$(menusDom[i]);

                    if(item.backgroundImg){
                        doma.css({
                            "background":"url("+item.backgroundImg+") no-repeat",
                            "background-size":"100%"
                        });
                    }
                    if( param.bottomColorSchemes&&param.bottomColorSchemes.bg){
                        doma.css({
                            "background-color":param.bottomColorSchemes.bg,
                            "color": param.bottomColorSchemes.color
                        });
                    }
                    item.toUrl="javascript:void(0)";
                    doma.html(item.title);
                    doma.attr("href",item.toUrl);
                }
            }
            //设置背景
            var homeBg = $('.bgImgWrap1 img'),
                bgList =[
                    {
                        imgUrl:''
                    },
                    {
                        imgUrl:''
                    },
                    {
                        imgUrl:''
                    }
                ];
            //设置多个背景图
            if(param.backgroundImgArr){
                bgList=param.backgroundImgArr;
            }
            // index = Math.round(Math.random() * (bgList.length - 1));
            homeBg.each(function(i) {
                if(bgList[i].imgUrl){
                    $(this).show();

                    $(this).attr('src', bgList[i].imgUrl).css({"width":304});
                } else{
                    $(this).hide();
                }

            });

            var firstImg = homeBg.eq(0);


            self.navMenuEvent();

            self.onBgImgSlider();
            /*setTimeout(function(){
                $('.indexMenu').trigger("click");
            },1000);*/

        },
        navMenuEvent: function() { //导航动画效果

            var self = this,
                aList = self.elements.indexNav.find('a'),
                number = 0,moveItem=aList.first(),menuTime=200;
            // self.elements.indexNav.animate({
            // 	'left': 23,
            // }, 300);

            setTimeout(ShowMenuItem, menuTime);
            function ShowMenuItem() {
                moveItem.addClass('show');
                moveItem=moveItem.next();
                if (moveItem.size()) {
                    setTimeout(ShowMenuItem,menuTime+=80);
                };
            }

        },
        onBgSize: function() {
            var w =304,
                self = this;
            $("#intoShow").css('width', w);
            $('#homeWrapper1').find('li').css('width', w);
            // self.elements.sliderWrap.css({
            // 	'width': w,
            // 	'right': -w
            // });
            // $("#sliderWrap section").css('width', w);


        },
        //背景图片滑动
        onBgImgSlider: function() {
            var myScrollOne = new iScroll('homeWrapper1', {
                snap: "li",
                momentum: false,
                hScrollbar: false
            });
            clearInterval(this.elements.timeCar);
            this.elements.timeCar= setInterval(function() {
                i<$("#homeWrapper1 li").length?i++:i=0;
                var el="li:nth-child(" +i+")";
                myScrollOne. scrollToElement(el);
            },2000);
        },
        //滑动层
        onSlider: function() {
            myScroll2 = new iScroll('sliderWrap', {
                snap: 'section',
                momentum: false,
                hScrollbar: false,
                vScroll: false
            });
        },
        //绑定事件
        initEvent: function() {





            //拨号
            // $('#indexTelBtn').bind('click', function() {
            // 	// // $("#intoShow").hide();
            // 	// self.elements.sliderWrap.show();
            // 	// self.elements.sliderWrap.animate({
            // 	// 	right: 0
            // 	// }, 300);
            // 	// self.onSlider();
            // 	// self.elements.homeWrapper.hide();
            // 	var teWrapper = $('#te-Wrapper');

            // 	if (teWrapper.size()) {
            // 		teWrapper.show();

            // 	} else {
            // 		Jit.UI.DomView.init('.schoolArea');

            // 	}
            // });


        }
        // ,
        // CloseTeWrapper: function() {
        // 	var self = this;
        // 	$('#te-Wrapper').hide();
        // 	self.elements.homeWrapper.show();

        // }


        },
        page3Key:{
            elements:{
                homeWrapper: $('#homeWrapper'),
                indexNav :$('.indexNav3') ,
                timeCar:null,
            },
            initLoad: function(param) {
                this.onBgSize();
                window.onresize = this.onBgSize;
                if(param.links){
                    var length=param.menuLength||param.links.length,finalLength=length;

                    var html="";
                    var logoList=[
                        "images/images4/logoIcon.png",
                        "images/images4/zxdtIcon.png",
                        "images/images4/hdzqIcon.png",
                        "images/images4/xkzsIcon.png"
                    ];
                    for(var i=0;i<length;i++){

                        var item=param.links[i];
                        item.toUrl="javascript:void(0)";
                        if(!param.links[i].title){finalLength--;continue;}
                        var style="'";
                        html+="<li>";

                        var str="rgba(0,0,0,0.6)"
                        if(param.colorSchemes&&param.colorSchemes.bg) {
                            str=  param.colorSchemes.bg.colorRgb();
                        }
                        if(param.colorSchemes&&param.colorSchemes.bg) {
                            style+= "color:"+ param.colorSchemes.color+";";
                        }
                        if(item.backgroundImg){
                            style+="background:"+str+" url("+item.backgroundImg+") no-repeat left 32px;"+"background-size:74px 188px";
                        }else{
                            var imgUrl=Math.floor(Math.random(3)*3);
                            style+="background:"+str+" url("+logoList[imgUrl]+") no-repeat left 32px;"+"background-size:74px 188px";
                        }
                        style+="'";
                        html+="<a href="+item.toUrl+" style="+style+"><span class='tit'>"+item.title+"</span></a></li>";
                    }
                    //每个li是74宽度
                    $(".navList").css("width",finalLength*76);
                    $("#list").html(html);
                }
                //设置背景
                var homeBg = $('.bgImgWrap img'),
                    bgList =[
                        {
                            imgUrl:''
                        },
                        {
                            imgUrl:''
                        },
                        {
                            imgUrl:''
                        }
                    ];
                //设置多个背景图
                if(param.backgroundImgArr){
                    bgList=param.backgroundImgArr;
                }
                homeBg.each(function(i) {
                    if(bgList[i].imgUrl) {
                        $(this).attr('src', bgList[i].imgUrl).css({"width":304});
                        $(this).show();
                    }else{
                        $(this).hide();
                    }
                });
                this.onBgImgSlider();

                this.onMenuSlider();

            },
            onBgSize: function() {
                var w = 304;
                $('#homeWrapper').find('li').css('width', w);
                $('#homeWrapper').find('li').eq(0).addClass("tm2");
            },
            //背景图片滑动
            onBgImgSlider: function() {
                if($("#menuWrapper").find(".navList").siblings("div").length>0 ){
                    $("#menuWrapper").find(".navList").siblings("div").remove();
                }
                var myScroll = new iScroll('homeWrapper', {
                    snap: true,
                    momentum: false,
                    hScrollbar: false
                });
                //myScroll. scrollToElement("li:nth-child(0)",100);
                var i=0;
                clearInterval(this.elements.timeCar);
                this.elements.timeCar= setInterval(function() {
                    i<$("#homeWrapper li").length?i++:i=0;
                    var el="li:nth-child(" +i+")";
                    myScroll. scrollToElement(el);
                },2000);

            },
            //菜单可以左右滑动
            onMenuSlider:function() {

                    var myScroll = new iScroll('menuWrapper', {
                        snap: "li",
                        momentum: true,
                        hScroll: true
                    });
                    this.elements.indexNav.find("a").addClass("tm");
            }
        },
        categoryLayerEvent: function () {
            // 分类弹层 事件委托
            this.elems.categoryLayer.delegate(".searchBtn", "click", function () {
                self.categoryLayer.loadDate($(this).siblings().children().val());

            }).delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.elems.mask.hide();

            }) .delegate(".categoryItem", "click", function (e) {
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.categoryLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.categoryLayer.hide();
                    $this.removeClass("on");
                }, 300);
            });

        },

        productLayerEvent: function () {
            // 产品选择事件委托
            this.elems.productLayer.delegate(".closePopupLayer", "click", function (e) {
                $(this).parents(".popupLayer").hide();
                self.elems.mask.hide();

            }).delegate(".searchBtn", "click", function () {
                self.productLayer.loadDate($(this).siblings().children("select").val(), $(this).siblings().children("input").val());

            }).delegate(".productItem", "click", function (e) {
                debugger;
                var $this = $(this);
                $this.addClass("on").siblings().removeClass("on");
                self.productLayer.callback($this.data("id"), $this.data("name"));
                setTimeout(function () {
                    self.productLayer.hide();
                    $this.removeClass("on")
                }, 300);
            });

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.mainMenuList.delegate("li","click",function(e){
                $(this).parent().find("li").removeClass("on");
                $(this).addClass("on");
                that.loadData.args.pageKey=$(this).data("key") ;
                that.loadData.args.pageId=$(this).data("id") ;
                that.loadData.loadPageInfo();


            }).delegate("li","mouseenter",function(e){

                $(this).addClass("hoverDiv");
            }).delegate("li","mouseleave",function(e){
                $(this).removeClass("hoverDiv");
            });
            that.elems.sectionPage.delegate(".colorPanel","mouseenter",function(){
                $(this).stop().slideDown(600);
            }).delegate(".colorPanel img","click",function(e){
                $(this).parents(".colorPanel").slideUp(600);
                $.util.stopBubble(e)
            }).delegate(".colorPanel div","click",function(e){
                    $(this).parents(".colorPanel").slideUp(600);
                $(this).parents(".colorPanel").find(".on").remove();
                var obj={};

                    obj["bg"]=$(this).css("background-color").colorHex();
                    obj["color"]=$(this).css("color").colorHex();

                if(obj["bg"]) {
                    $(this).parents(".commonInputWrap").data("value", obj);
                    self.timelyLoadPage();
                }
                if($(this).find("img").length==0){
                    $(this).append("<div class='on'></div>")
                }

                }).delegate(".scheme span","click",function(){
                var obj={};

                if($(this).attr("class")=="one"){
                     obj["bg"]="#ffffff";
                     obj["color"]="#000000";
                }
                if($(this).attr("class")=="two"){
                    obj["color"]="#ffffff";
                    obj["bg"]="#000000";
                }
                if($(this).attr("class")=="all"){
                  $(this).parent().find(".colorPanel").slideDown(400)
                }  else{
                    $(this).parent().find(".colorPanel").slideUp(400);
                }
                   if(obj["bg"]) {
                       $(this).parents(".commonInputWrap").data("value", obj);
                       self.timelyLoadPage();
                   }

            }).delegate(".radio","click",function(e) {
                var name = $(this).data("name"), value = $(this).data("value");
                if (name) {
                    $('[data-name="' + name + '"]').removeClass("on");
                    $(this).addClass("on");
                }
                $(this).parents(".commonInputWrap").data("value", value);
                if ($(this).parents(".radioList").data("key") == "menuLength") {
                    var html = self.render(self.template.menuLi, {arrayLength: value});
                    $(".radio").parents(".line").siblings().find(".menuPanel .menuUl ul").html(html);
                    if (value > 5) {
                        $(".radio").parents(".line").siblings().find(".menuPanel .menuUl").css({"width": "374px"})
                    } else {
                        $(".radio").parents(".line").siblings().find(".menuPanel .menuUl").css({"width": "auto"})
                    }
                    self.timelyLoadPage();
                } else if ($(this).parents(".radioList").data("key") == "animateDirection") {
                    self.timelyLoadPage("执行");
                }
                else {
                    self.timelyLoadPage(true);
                }


            }).delegate(".bgImg .del","click",function(){
               $(this).parents(".commonInputWrap").data("value","");//删除背景图片和logo图片
                self.timelyLoadPage();
            }) .delegate(".imgDelBtn","click",function(){
                    $(this).siblings("span").html("");
                    $(this).parents(".imgOption").data("value","");//删除背景图片和logo图片
                    self.timelyLoadPage();
                }).delegate(".inputBox input","blur",function(){
                var value=$(this).val();
                $(this).parents(".commonInputWrap").data("value",value);
                if($(this).data("name")=="title"){
                    $("#pageTitle").html(value);
                }
                self.timelyLoadPage();
            }).delegate(".jsChooseBtn","click",function(e) {
                var $this = $(this),
                    type = $this.parent().siblings(".jsTypeSelect").val();

                if (type == "cg-1") {
                    //分类
                    self.categoryLayer.pageIndex = 0;
                    self.categoryLayer.show(function (key, name) {
                        //注册回调
                        debugger;
                        $this.parents(".jsAreaItem").eq(0).data("id", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.categoryLayer.loadDate();
                }
                else if (type == "cg-2") {
                    //产品
                    self.productLayer.pageIndex = 0;
                    self.productLayer.eventId = "";
                    self.productLayer.typeId = "";
                    self.productLayer.show(function (key, name) {
                        //注册回调
                        $this.parents(".jsAreaItem").eq(0).data("id", key).data("name", name).data("typeid", type.substring(type.indexOf("-") + 1));
                        $this.siblings("input").val(name);
                    });
                    //执行一次搜索
                    self.productLayer.loadDate();
                }
            }).delegate("#submitBtn", "click", function () {
                var node = [], nodeValue = [];
                //捕获客户标题和模板
                $(".jsNodeValue").each(function (i, e) {
                    node.push($(e).data("node"));
                    nodeValue.push($(e).data("value") ? $(e).data("value") : $(e).val());
                });
                //debugger;
                // 有底部参数时添加node和nodeValue
                self.param = {};
                var isSubmit = true
                $(".jsParamValue").each(function () {
                    var key = $(this).data("key");

                    if ($(this).find(".menuPanel").length == 0) { //不是菜单的保存
                        self.param[key] = $(this).data("value");
                    }
                    //多张背景图片处理
                    if ($(this).find(".imgOption").length > 0) {
                        var imgList = []
                        $(this).find(".imgOption").each(function () {
                            imgList.push({"imgUrl": $(this).data("value")});
                        })
                        self.param[key] = imgList;
                    }

                    if ($(this).find(".menuProperty").length > 0) {//菜单项保存
                        var links = [];

                        $(this).find(".menuProperty").each(function (index) {
                             index+=1
                            var obj = {}, menuName = $(this).parent().siblings().find("em.tit").html();
                            $(this).find(".commonInputWrap").each(function () {

                                if ($(this).data("key") == "toUrl") {
                                    var execute=!self.param["menuLength"];
                                      if(self.param["menuLength"]>=index){
                                          execute=true;
                                      }

                                    if(execute) {
                                        var toUrlObj = {};
                                        toUrlObj = $(this).find(".jsAreaItem").data();
                                        toUrlObj["id"] = toUrlObj["id"].toString();
                                        toUrlObj["url"] = $(this).find(".jsAreaItem .jsNameInput").val();
                                        toUrlObj["name"] = $(this).find(".jsAreaItem .jsNameInput").val();
                                        debugger;
                                        obj[$(this).data("key")] = JSON.stringify(toUrlObj).replace(/,/g, "&&");
                                        if (toUrlObj.typeid == "null") {
                                            alert(menuName + "的第" + index + "项菜单,请选择一个链接类型");
                                            isSubmit = false;

                                        } else {
                                            if (toUrlObj.typeid == "3") {
                                                if (!toUrlObj.url) {
                                                    alert(menuName + "的第" + index + "项菜单，请填写一个正确的网址");
                                                    isSubmit = false;

                                                }
                                            } else {
                                                if (!toUrlObj.name) {
                                                    alert(menuName + "的第" + index + "项菜单，请选择一个商品或者商品品类");
                                                    isSubmit = false;

                                                }
                                            }

                                        }
                                    }

                                } else {
                                    obj[$(this).data("key")] = $(this).data("value").toString();
                                }

                            });
                            if (isSubmit) {
                                links.push(obj);
                            } else {
                                return false;
                            }
                        });
                        self.param[key] = links;
                    }
                    return isSubmit;
                });
                if (isSubmit) {
                    if (self.param) {
                        debugger;
                        //取最新值
                        node.push(3);

                        nodeValue.push(JSON.stringify(self.param));
                    }
                    debugger;
                    if (node.length != 0) {
                        //debugger;
                        self.loadData.submit(node, nodeValue);
                    } else {
                        alert("没有可提交的数据");
                    }
                }
            }).delegate(".jsTypeSelect", "change", function (e,isSimulation) {
                //选择select类型  产品、资讯、类型....
                var $this = $(this);
                $this.siblings(".infoContainer").children(".jsChooseBtn").show();
                $this.siblings(".infoContainer").children(".jsNameInput").css({"width": "80%"});
                //$this.siblings(".delIcon").show();
                if (!isSimulation) {
                    $this.siblings(".infoContainer").children(".jsNameInput").val("");
                }
                if (this.value=="cg-3") {
                    $this.siblings(".infoContainer").show().children(".jsNameInput").css({"width":"100%"}).removeAttr("disabled");
                    $this.siblings(".infoContainer").children(".jsChooseBtn").hide().attr("disabled", "disabled").css({"opacity": 0.2});
                } else if (this.value=="cg-null") {
                    // 请选择
                    $this.siblings(".infoContainer").hide();
                    $this.siblings(".delIcon").hide();
                }else {
                    $this.siblings(".infoContainer").children(".jsNameInput").attr("disabled", "disabled");
                    $this.siblings(".infoContainer").show().children(".jsChooseBtn").removeAttr("disabled").css({"opacity": 1});
                }
                var type=$(this).val();
                $this.siblings(".jsAreaItem").eq(0).data("typeid", type.substring(type.indexOf("-") + 1));
            });
            that.elems.deploy.delegate(".menuPanel .menuUl li","click",function(e){
                var me=$(this),index=me.index();
                me.parent().find("li").removeClass("on");
                me.addClass("on");
                var tag=me.parents(".menuPanel").data("flag");
                $("[data-property='"+tag+"']").hide().eq(index).show()
            })

             /*page2Key 事件测试*/
            $('.indexMenu').bind('click', function() {
                var self = $(this),
                    subElement = $('.menuWrap');
                if (self.hasClass('on')) {
                    subElement.animate({
                        left: -160
                    }, 400);
                    self.removeClass('on');
                } else {
                    subElement.animate({
                        left: 65
                    }, 400);
                    self.addClass('on');
                }
            });

        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){

        },

        renderBottomJsonInfo: function (userDataString) {
            var jsonParam = this.jsonParam;
            if (jsonParam.length) {
                var obj = {}, customerPara = null;
                try {
                    //有用户保存的参数时 把用户设置的值置为默认值，没有的时候取json中的默认值，并设用户变量param对象保存数据
                    customerPara = JSON.parse(userDataString);

                } catch (error) {

                }
                //debugger;
                // 获取默认参数 （已录入，则修改defaultValue，未录取，取defaultValue）
                for (var i = 0; i < jsonParam.length; i++) {
                    var idata = jsonParam[i];
                    if (customerPara && customerPara[idata.Key] != undefined) {
                        obj[idata.Key] = customerPara[idata.Key];
                        jsonParam[i].defaultValue = customerPara[idata.Key];
                    } else {
                        obj[idata.Key] = idata.defaultValue;
                    }
                }
                //debugger;
                self.param = obj;   //全局页面参数

                for (var i = 0; i < jsonParam.length; i++) {
                    var idata = jsonParam[i];
                     debugger;
                    if (idata.valueType == "Array"||idata.valueType=="imgArray") {
                        //对valueType = Array特殊处理    对象数组
                        //{
                        //	"Name":"入口配置",
                        //	"Key":"links",
                        //	"SubKey":"title,english,toUrl",
                        //	"SubName":"入口名,入口英文名,入口链接",
                        //	"SubValueType":"string,string,string",
                        //	"SubDefaultValue":"入口,Entry,www.jitmarketing.cn",
                        //	"valueType":"Array",
                        //	"arrayLength":5,
                        //	"defaultValue":"",
                        //    "optionMap":{
                        //        "englist":{
                        //            "values":["up","down","left","right"],
                        //            "valuesText":["向上","向下","向左","向右"]
                        //        }
                        //    }
                        //}

                        //输出结果中的links

                        //'param':{
                        //	'title':'哎哟我操',
                        //	'logo':'../../../images/public/shengyuan_default/logo.jpg',
                        //	'links':[
                        //		{
                        //			'title':'门店查询',
                        //			'english':'STORE <br/>INFORMATION',
                        //			'toUrl':"javascript:Jit.AM.toPage('StoreList')"
                        //		},
                        //		{
                        //			'title':'最新活动',
                        //			'english':'NEWEST',
                        //			'toUrl':"javascript:Jit.AM.toPage('Activity')"/*全新代揽胜*/
                        //		},
                        //		{
                        //			'title':'影讯',
                        //			'english':'MOVIE <br/>INFORMATION',
                        //			'toUrl':"javascript:Jit.AM.toPage('Introduce','&type=1')"/*揽胜极光*/
                        //		},
                        //		{
                        //			'title':'热卖商品',
                        //			'english':'HOT SALE',
                        //			'toUrl':"javascript:Jit.AM.toPage('GoodsList')"/*揽胜极光*/
                        //		},
                        //		{
                        //			'title':'联系我们',
                        //			'english':'CONTACT US',
                        //			'toUrl':"javascript:Jit.AM.toPage('Introduce','&type=1')"/*揽胜极光*/
                        //		}
                        //	],   /*跳转地址   相对路径或者javascript模式*/
                        //	'backgroundImage':'../../../images/public/shengyuan_default/indexBg.jpg',
                        //	'littleImage':'',
                        //	'animateDirection':'up'   /*动画方向*/
                        //}

                        var html = '';
                        var menuKeyList = idata.SubKey.split(","),
                            menuNameList = idata.SubName.split(","),
                            menuTypeList = idata.SubValueType.split(","),
                            menuValueList = idata.SubDefaultValue.split(",");

                        var menuList = [],      //所有的参数
                            valueMap = {};       //所有的值

                        for (var j = 0; j < menuKeyList.length; j++) {
                            var menuObj = {
                                key: menuKeyList[j],
                                name: menuNameList[j],
                                type: menuTypeList[j],
                                value: menuValueList[j]
                            };
                            menuList.push(menuObj);


                            valueMap[menuObj.key] = menuValueList[j];

                        }

                        var tempList = [],      //输出的对象数组
                            defaultValue;       //默认值
                        if (idata.defaultValue) {
                            try {
                                defaultValue = JSON.parse(idata.defaultValue);
                            } catch (err) {
                                defaultValue = idata.defaultValue;
                            }
                        } else {
                            defaultValue = false;
                        }

                        for (var j = 0; j < idata.arrayLength; j++) {
                            if (defaultValue && defaultValue[j]) {
                                tempList.push(defaultValue[j]);
                            } else {
                                tempList.push(valueMap);
                            }

                            html += '<div data-property="'+idata.Key+'" style="display: block;" class="menuProperty">';
                            for (var k = 0; k < menuList.length; k++) {
                                var edata = menuList[k];
                                edata.defaultValue = tempList[j][edata.key]||edata.value;
                                if(edata.type=="selectUrl"){

                                    edata["jsonValue"]=edata.defaultValue;
                                    edata.defaultValue=JSON.parse(edata.defaultValue.replace(/&&/g,","));
                                }
                               /* if(edata.defaultValue=='true'){
                                    edata.defaultValue="是";
                                } else if(edata.defaultValue=='false'){
                                    edata.defaultValue="否";
                                }*/
                                edata["radioName"]=edata.key+j;
                                if (edata.type == "string") {
                                    edata.defaultValue = edata.defaultValue.replace(/\"/g, "&quot;").replace(/\'/g, "&quot;").replace(/ /g, "&nbsp;");
                                    html += self.render(self.template.paramJsonString, edata);
                                } else if (edata.type == "option") {
                                    edata.option = idata.optionMap[edata.key];
                                    html += self.render(self.template.paramJsonOption, edata);
                                } else if (edata.type == "image") {

                                    html += self.render(self.template.paramJsonImage, edata);
                                }else if (edata.type == "imageAdd") {

                                        html += self.render(self.template.paramJsonImageAdd, edata);
                                }else if (edata.type == "bool") {
                                    html += self.render(self.template.paramJsonBool, edata);
                                }else if(edata.type == "selectUrl"){
                                    debugger;
                                    html += self.render(self.template.paramJsonUrl, edata);
                                } else {
                                    html += '<div style="margin:5px 0;"><span style="display: inline-block;width: 80px;">' + edata.name + '</span><input class="jsTrigger" type="text" data-key="' + edata.key + '" value="' + tempList[j][edata.key] + '" /></div>';
                                }

                            }
                            html += '</div>'
                        }
                        //debugger;
                        jsonParam[i].defaultValue = tempList;
                        jsonParam[i].html = html;
                        //debugger;
                    } else if (idata.valueType == "ArraySimple") {
                        //对valueType = ArraySimple 特殊处理    对象数组
                        //{
                        //	"Name":"简单数组",
                        //	"Key":"links2",
                        //	"valueType":"ArraySimple",
                        //  "arrayName":["入口名","入口英文名","入口链接"],
                        //	"arrayKey":["title","direction","bgimage"], //当某个type中有option时必须要写，arrayKey到optionMap中招对应的select数据。
                        //	"arrayValueType":["string","option","image"],
                        //	"defaultValue":{title:"测试字符",direction:"left",bgimage:"../../images/bg.png"},
                        //  "optionMap":{       //key为序号
                        //      "dirction":{
                        //          "values":["up","down","left","right"],
                        //          "valuesText":["向上","向下","向左","向右"]
                        //      }
                        //  }
                        //}

                        var html = '<div  class="jsGroup" style="margin:20px 0;">';
                        for (var j = 0; j < idata.arrayValueType.length; j++) {
                            var edata = {
                                type: idata.arrayValueType[j],
                                name: idata.arrayName[j],
                                key: idata.arrayKey[j],
                                defaultValue: idata.defaultValue[0][idata.arrayKey[j]]

                            };
                            if (edata.type == "string") {
                                html += self.render(self.template.paramJsonString, edata);
                            } else if (edata.type == "option") {
                                edata.option = idata.optionMap[edata.key];
                                html += self.render(self.template.paramJsonOption, edata);
                            } else if (edata.type == "image") {
                                html += self.render(self.template.paramJsonImage, edata);
                            } else {
                                html += '<div style="margin:5px 0;"><span style="display: inline-block;width: 80px;">' + edata.name + '</span><input class="jsTrigger" type="text" data-key="' + edata.key + '" value="' + tempList[j][edata.key] + '" /></div>';
                            }
                        }
                        html += '</div>'
                        //debugger;
                        jsonParam[i].defaultValue = tempList;
                        jsonParam[i].html = html;


                    } else if (idata.valueType == "string"||idata.valueType == "color") {
                        debugger;
                        if (typeof idata.defaultValue == "object") {
                            jsonParam[i].defaultValue = JSON.stringify(idata.defaultValue);
                        }
                    }
                    self.param[idata.Key] = jsonParam[i].defaultValue;
                    //debugger;
                }
                debugger;
                self.elems.paramsList.html(self.render(self.template.paramsList, { list: jsonParam }));

                // 注册上传按钮
                self.elems.paramsList.find(".jsUploadBtn").each(function (i, e) {
                    self.addUploadImgEvent(e);
                });


                    self.elems.paramsList.find(".colorPanel").each(function(i,e){
                        debugger;
                      var color=$(this).parents(".jsParamValue").data("value").bg.colorRgb(true);
                        $(this).find(".on").remove();
                        $(this).find("div").each(function (){
                            debugger;
                             if($(this).css("background-color").colorHex()==color.colorHex()){
                                 $(this).append("<div class='on'></div>");
                             }
                        });

                    });



                self.elems.paramsList.find(".jsTypeSelect").trigger("change",true);
                $('[data-key="menuLength"].radioList').find(".radio.on").eq(0).trigger("click")
                if(self.elems.deploy.find(".menuPanel .menuUl").length>0){
                    self.elems.deploy.find(".menuPanel .menuUl").each(function(){
                        $(this).find("li").eq(0).trigger("click")
                    })
                }
            }
            if($(".mainMenuList").find("li.on").data("key")=="$HomeIndex2"){

                $('[data-key="links"]').find("li").addClass("menuTab")
                $('[data-key="links"]').find(".menuUl").addClass("menuTab")
                $('[data-key="links"]').find(".uploadBtn").addClass("menuTab")
            }
        },
        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var  that=this;
            that.loadData.loadPageList(function(data){
                if(data.Data&&data.Data.TotalPageCount>=4) {
                    if (data.Data.PageList && data.Data.PageList.length > 0) {
                       for(var i=0;i<data.Data.PageList.length; i++) {
                           var row = data.Data.PageList[i];
                           //绑定对象的微官网页面的key和页面id
                           that.elems.mainMenuList.find("li").each(function () {
                               if (row.Title.indexOf($(this).data("title")) != -1) {
                                   $(this).data("key", row.PageKey).data("id", row.MappingID)
                               }
                           })
                       }
                    }
                    that.elems.mainMenuList.find("li").eq(0).trigger("click");
                }else{
                    console.log("模板数据不够数")
                }
            });
        },
        // 获取一级分类列表
        GetLevel1ItemCategory: function () {
            this.ajax({
                url: "/Module/AppConfig/Handler/HomePageHandler.ashx",
                type: 'get',
                data: {
                    method: "GetLevel1ItemCategory"
                },
                dataType: "json",
                success: function (data) {
                    debugger;
                    if (data.success) {
                        self.elems.categorySelect[0].options[0] = new Option("请选择分类", "");
                        if (data.data.categoryList && data.data.categoryList.length) {
                            for (var i = 0; i < data.data.categoryList.length; i++) {
                                var idata = data.data.categoryList[i];
                                self.elems.categorySelect[0].options[i + 1] = new Option(idata.categoryName, idata.categoryId);
                            }
                        } else {

                        }

                    } else {
                        alert(data.msg);
                    }
                }
            });
        },
        renderMidPageInfo: function () {
            //中部页面信息    标题、页面模板
            this.elems.pageInfo.html(self.render(self.template.pageInfo, this.pageInfo));
        },
        addUploadImgEvent: function (e) {
            this.uploadImg(e, function (ele, data) {
                debugger;
                //上传成功后回写数据
               if( $(ele).parents(".jsParamValue").length>0){
                   $(ele).parents(".jsParamValue").eq(0).data("value",data.thumUrl);

               }
                //菜单图标的上传
                if( $(ele).parents(".linkParamValue").length>0){
                    $(ele).parents(".linkParamValue").eq(0).data("value",data.thumUrl);
                }
                if($(ele).parents(".bgImg").find(".imgList").length>0){
                    var isChange=true;
                    $(ele).parents(".bgImg").find(".imgList .imgOption").each(function(){
                            if(!$(this).data("value")){
                                $(this).data("value",data.thumUrl);
                                $(this).find("span").html(data.thumUrl.match(/\/(\w+\.(?:png|jpg|gif|bmp))$/i)[1])
                                isChange=false;
                                return false;
                            }
                    });
                    if(isChange){
                        alert("请先删除你要跟换的图片");
                    }

                }
                self.timelyLoadPage()
               // alert("上传成功，保存以后显示") ;
                /*if ($(ele).parent().siblings("p.jsParamValue").length) {
                    $(ele).parent().siblings("p.jsParamValue").html('<img src="' + data.thumUrl + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                    this.param[$(ele).parent().siblings("p.jsParamValue").data("key")] = data.thumUrl;
                }

                if ($(ele).parent().siblings(".jsTrigger").length) {
                    $(ele).parent().siblings(".jsTrigger").html('<img src="' + data.thumUrl + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                    $(ele).parent().siblings(".jsTrigger").attr("data-value", data.thumUrl);
                    //推送保存变量
                    this.grabParamValue($(ele).parents(".jsParamValue").eq(0));
                }*/

            });
        },
        uploadImg: function (btn, callback) {
            console.log("uploadImg");
            setTimeout(function () {
                var uploadBtn = KE.uploadbutton({
                    width: "100%",
                    button: btn,
                    //上传的文件类型
                    fieldName: 'imgFile',
                    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                    url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=536&height=300',
                    afterUpload: function (data) {
                        if (data.error === 0) {
                            if (callback) {
                                callback(btn, data);
                            }
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
                uploadBtn.fileBox.change(function (e) {
                    uploadBtn.submit();
                });
            }, 10);

        },

        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },
        loadData: {
            args: {
               menuCode:"statementList",
                pageKey:"",
                pageId:""
            },
            submit: function (node, nodeValue) {
                //debugger;
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: "WX.SysPage.SetCustomerPageSetting",
                        PageKey: this.args.pageKey,
                        MappingId: this.args.pageId,
                        Node: node,
                        NodeValue: nodeValue
                    },
                    success: function (data) {
                        debugger;
                        if (data.IsSuccess) {
                            alert("保存成功，应用以后生效");
                            //window.location.href = "pageList.aspx";
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            public: function () {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: "WX.SysPage.CreateCustomerConfig"
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            alert("发布成功");
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            loadPageList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: "WX.SysPage.GetCustomerPageList",
                        Key: "homeIndex",
                        Name: "",
                        PageIndex:0,
                        PageSize: 15
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            loadPageInfo: function (callback) {
                $.util.ajax({
                    url:"/ApplicationInterface/Gateway.ashx",
                    data: {
                        action: "WX.SysPage.GetCustomerPageSetting",
                        PageKey: this.args.pageKey
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            } else {
                                var baseInfo, pageJson, pageInfo, jsonParam;
                                try {
                                    baseInfo = data.Data.PageBaseInfo[0];
                                    pageJson = JSON.parse(data.Data.JsonValue);
                                      //重写用户配置，没有时取json中的默认值
                                    pageInfo = {
                                        list: pageJson.htmls,
                                        model: data.Data.PageHtmls || pageJson.defaultHtml,
                                        title: data.Data.PageTitle || pageJson.title
                                    }
                                    $("#pageTitle").show().html(pageInfo.title);
                                    jsonParam = pageJson.params
                                } catch (error) {
                                    alert(error);
                                    return false;
                                }


                                self.baseInfo = baseInfo;
                                self.pageJson = pageJson;
                                self.pageInfo = pageInfo;
                                self.jsonParam = jsonParam;
                               /* //
                                //顶部基础信息   页面名、版本、更新人、时间
                                self.renderTopBaseInfo(pageInfo.model);
*/

                                //中部页面信息    标题、页面模板
                                self.renderMidPageInfo();

                                var key =$(".mainMenuList").find("li.on").data("key");
                                self.initPage(key,data.Data.PagePara)
                                //底部附加参数   用户配置
                                self.renderBottomJsonInfo(data.Data.PagePara);

                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }

        },
        productLayer: {  // 所有选择商品的数据来源
            show: function (callback) {
                self.elems.productLayer.show();
                self.elems.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.elems.productLayer.hide();
                self.elems.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            /* typeId:this.typeId,//只有掌声秒杀模块的时候才用到 typeId 1 表示团购（疯狂团购） 2表示限时抢购（掌上秒杀）的e
             eventId:this.eventId,*/
            loadDate: function (id, text,type) {
                debugger;
                var categoryId = id || self.elems.productLayer.find("select").val();
                var itemName = itemName || self.elems.productLayer.find("input").val();
                $(".wrapSearch").show();
                if(type){
                    $(".wrapSearch").hide();
                    debugger;
                    self.GetItemAreaByEventID(this.typeId,this.eventId, this.pageIndex, this.pageSize,function(data){
                        var html = "";

                        if (data.data.totalCount) {
                            for (var i = 0; i < data.data.itemList.length; i++) {
                                var idata = data.data.itemList[i];
                                data.data.itemList[i].json = JSON.stringify(idata);
                            }
                            data.data.currentItemId = self.goodsSelect.currentItemId;
                            html = self.render(self.template.goodsKill, data.data);
                            // 分页处理 begin
                            var pageNumber = Math.ceil(data.data.totalCount / self.productLayer.pageSize);

                            if (pageNumber > 1) {
                                kkpager.generPageHtml({
                                    pno: self.productLayer.pageIndex?self.productLayer.pageIndex+1:1,
                                    mode: 'click', //设置为click模式
                                    pagerid:'kkpager11',
                                    total: pageNumber,//总页码
                                    totalRecords: data.data.totalCount,
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
                                        self.productLayer.pageIndex=n-1
                                        self.productLayer.loadDate();
                                    },
                                    //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                    getHref: function (n) {
                                        return '#';
                                    }

                                }, true);

                                self.elems.productLayer.find('.pageContianer').show();
                            } else {
                                self.elems.productLayer.find('.pageContianer').hide();
                            }
                            // 分页处理 end

                        } else {
                            html = '<li style="text-align:center;">没有数据</li>';
                        }
                        $("#skillInfoShow").html(html);


                    });
                }else {
                    self.ajax({
                        url: self.url,
                        type: 'get',
                        data: {
                            method: 'GetItemList',
                            categoryId: categoryId,
                            itemName: itemName,
                            pageIndex: this.pageIndex,
                            pageSize: this.pageSize
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var html = "";
                                if (data.data.totalCount) {
                                    html = self.render(self.template.product, data.data);
                                    // 分页处理 begin
                                    var pageNumber = Math.ceil(data.data.totalCount / self.productLayer.pageSize);

                                    if (pageNumber > 1) {
                                        kkpager.generPageHtml({
                                            pno: self.productLayer.pageIndex?self.productLayer.pageIndex+1:1,
                                            mode: 'click', //设置为click模式
                                            pagerid:'kkpager11',
                                            total: pageNumber,//总页码
                                            totalRecords: data.data.totalCount,
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
                                                self.productLayer.pageIndex=n-1
                                                self.productLayer.loadDate();
                                            },
                                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                            getHref: function (n) {
                                                return '#';
                                            }

                                        }, true);

                                        self.elems.productLayer.find('.pageContianer').show();
                                    } else {
                                        self.elems.productLayer.find('.pageContianer').hide();
                                    }
                                    // 分页处理 end

                                } else {
                                    html = self.render(self.template.product, { itemList: [] });
                                }
                                $("#layerProductList").html(html);
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }

            }
        },
        ajax: function (param) {
            var _param = {
                type: "post",
                dataType: "json",
                url: self.url,
                data: null,
                beforeSend: function () {
                },
                complete: function () {
                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(JSON.stringify(XMLHttpRequest));
                }
            };

            $.extend(_param, param);
            $.ajax(_param);
        },
        categoryLayer: {   //商品分类 //团购抢购秒杀 分组的数据来源
            show: function (callback) {
                self.elems.categoryLayer.show();
                self.elems.mask.show();
                if (callback) {
                    this.callback = callback;
                }
            },
            hide: function () {
                self.elems.categoryLayer.hide();
                self.elems.mask.hide();
            },
            pageIndex: 0,
            pageSize: 5,
            loadDate: function (text) {
                var categoryName = text || self.elems.categoryLayer.find("input").val();
                if(text=="killListType"){
                    self.ajax({
                        url: self.url,
                        type: 'get',
                        data: {
                            method: 'GetPanicbuyingEventList',
                            eventTypeId: self.categoryLayer.shopType,
                            pageIndex: this.pageIndex,
                            pageSize: this.pageSize
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var html = "";
                                debugger;
                                if (data.data.totalCount) {
                                    html = self.render(self.template.activity, data.data);
                                    // 分页处理 begin
                                    var pageNumber = Math.ceil(data.data.totalCount / self.categoryLayer.pageSize);
                                    if (pageNumber > 1) {
                                        self.elems.categoryLayer.find('.pageContianer').show();
                                        kkpager.generPageHtml({
                                            pno: self.categoryLayer.pageIndex ? self.categoryLayer.pageIndex + 1 : 1,
                                            mode: 'click', //设置为click模式
                                            pagerid: 'kkpager12',
                                            total: pageNumber,//总页码
                                            totalRecords: data.data.totalCount,
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
                                                self.categoryLayer.pageIndex = n - 1;
                                                self.categoryLayer.loadDate();
                                            },
                                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                            getHref: function (n) {
                                                return '#';
                                            }

                                        }, true);


                                    } else {
                                        self.elems.activityLayer.find('.pageContianer').hide();
                                    }
                                    // 分页处理 end

                                } else {
                                    self.elems.activityLayer.hide();
                                    self.elems.mask.hide();
                                    $.messager.confirm("提示","没有对应的活动分组，确认添加新的活动分组吗？",function(r){
                                        if(r){
                                            location.href = "/module/GroupBuy/GroupList.aspx?pageType="+self.categoryLayer.shopType+"&mid="+ $.util.getUrlParam("mid")
                                        }
                                    })
                                }
                                $("#layerActivityLayer").html(html);
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }else {
                    self.ajax({
                        url: self.url,
                        type: 'get',
                        data: {
                            method: 'GetItemCategory',
                            categoryName: categoryName,
                            pageIndex: this.pageIndex,
                            pageSize: this.pageSize
                        },
                        dataType: "json",
                        success: function (data) {
                            if (data.success) {
                                var html = "";
                                debugger;
                                if (data.data.totalCount) {
                                    html = self.render(self.template.category, data.data);
                                    // 分页处理 begin
                                    var pageNumber = Math.ceil(data.data.totalCount / self.categoryLayer.pageSize);
                                    if (pageNumber > 1) {
                                        self.elems.categoryLayer.find('.pageContianer').show();
                                        kkpager.generPageHtml({
                                            pno: self.categoryLayer.pageIndex ? self.categoryLayer.pageIndex + 1 : 1,
                                            mode: 'click', //设置为click模式
                                            pagerid: 'kkpager12',
                                            total: pageNumber,//总页码
                                            totalRecords: data.data.totalCount,
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
                                                self.categoryLayer.pageIndex = n - 1
                                                self.categoryLayer.loadDate();
                                            },
                                            //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                                            getHref: function (n) {
                                                return '#';
                                            }

                                        }, true);


                                    } else {
                                        self.elems.categoryLayer.find('.pageContianer').hide();
                                    }
                                    // 分页处理 end

                                } else {
                                    html = self.render(self.template.product, {itemList: []});
                                }
                                $("#layerCategoryList").html(html);
                            } else {
                                alert(data.msg);
                            }
                        },
                        error: function (data) {
                            alert(JSON.stringify(data));
                        }
                    });
                }
            }
        },
    };

    page.init();
    self=page;
});

