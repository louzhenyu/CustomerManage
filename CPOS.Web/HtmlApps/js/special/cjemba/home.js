(function () {

    var ImgV = {

        domList: null,
        elements: {},
        selection:0,

        initElements: function () {
            var me = this;

            //头像缩略图容器
            me.elements.alumnusPicList = $('#alumnusPicList a');
            //头像缩略图子容器（滚动）
            me.elements.scrollList = $('#scrollList');
            //上箭头
            me.elements.topArrow = $('#top_arrow');
            //下箭头
            me.elements.bottomArrow = $('#bottom_arrow');
            //定位索引隐藏域
            me.elements.pos_index = $('#pos_index');
        },

        //显示/隐藏上下箭头
        toggleArrow: function () {
            var me = this;
//            console.log(me.selection);
            if (me.selection == me.domList.length - 1) {
                me.elements.topArrow.hide();
                me.elements.bottomArrow.hide();
            } else {
                me.elements.topArrow.hide();
                me.elements.bottomArrow.show();
            }
        },
        setUrlParam: function (key, value) {
            var itemarr = [],
                urlstr = window.location.search.substr(1);

            var new_param_arr = [];
            var new_param_str = '';
            if (urlstr) {

                var item = urlstr.split("&");

                for (i = 0; i < item.length; i++) {

                    itemarr = item[i].split("=");

                    if (key == itemarr[0]) {
                        new_param_str = key + '=' + value;
                    } else {
                        new_param_arr.push(item[i]);
                    }
                }
                if (!new_param_str) {
                    new_param_str = key + '=' + value;
                }
                new_param_arr.push(new_param_str);
            }
            return new_param_arr.join('&');
        },


        //分享
        share: function () {
            var me=this;
            var prefix=window.location.protocol + '//' + window.location.host+'/'+window.location.pathname;
            var selection=(me.selection==6)?6:this.selection;
            var search=me.setUrlParam('pos_index',me.selection);
//            console.log(me.setUrlParam('pos_index',me.getPositionIndex()));
//            console.log(window.location.protocol + '//' + window.location.host + '/HtmlApps/images/special/cjemba/banner01.png');


            if(selection==6){
                Jit.WX.shareFriends('慧聚长江 | 长江EMBA论坛近期安排', '诚邀各地商界精英拨冗出席，与长江教授、校友共同为中国经济发展建言献策。', prefix+'?'+search, window.location.protocol + '//' + window.location.host + '/HtmlApps/images/special/cjemba/share_activity.png');
            }else{
                Jit.WX.shareFriends('长江商学院EMBA：中国商界精英的共同选择', '长江EMBA秋季班热招中，如果您期望与最优秀精英共同学习，或在您身边有优秀而好学的高层管理人员，欢迎自荐或推荐。', prefix+'?'+search, window.location.protocol + '//' + window.location.host + '/HtmlApps/images/special/cjemba/share_cj.png');
            }

            //浏览统计 第一次加载和每次动画完成都会调用一次分享
            console.log(ImgV.selection);
            if(window._paq){
                var title = ImgV.domList[ImgV.selection].attr("data-title");
                document.title = title;
                title+="---浏览";
                var baseInfo = Jit.AM.getBaseAjaxParam();
                _paq.push(['trackEvent', title,baseInfo.customerId]);
            }
        },

        //重置页面动画
        resetPageAnimate:function(){
            //page4
            $('#num_40').removeClass('animate').css({
                'background-size':'40px 19.5px'
            });
            $('#num_06').removeClass('animate').css({
                'background-size':'18px 28px'
            });

            //page5
            $('#num_20').removeClass('animate').css({
                'background-size':'30px 11px'
            });
            $('#num_111').removeClass('animate').css({
                'background-size':'33px 11px'
            });
            $('#num_136').removeClass('animate').css({
                'background-size':'background-size:33px 10px;'
            });

            //page6
            $('#big_5000').removeClass('animate').css({
                'font-size':'80px'
            });

        },

        //页面动画
        pageAnimate:function(){
            ImgV.resetPageAnimate();
            switch (ImgV.selection){
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    $('#num_40').addClass('animate').bind('webkitAnimationEnd', function () {
                        $(this).css({
                            'background-size':'64px 39px'
                        });
                    })
                    $('#num_06').addClass('animate').bind('webkitAnimationEnd', function () {
                        $(this).css({
                            'background-size':'37px 56px'
                        });
                    })
                    break;
                case 4:
                    $('#num_20').addClass('animate').bind('webkitAnimationEnd', function () {
                        $(this).css({
                            'background-size':'59px 23px'
                        });
                    })
                    $('#num_111').addClass('animate').bind('webkitAnimationEnd', function () {
                        $(this).css({
                            'background-size':'66px 23px'
                        });
                    })
                    $('#num_136').addClass('animate').bind('webkitAnimationEnd', function () {
                        $(this).css({
                            'background-size':'66px 21px'
                        });
                    })
                    break;
                case 5:
                    $('#big_5000').addClass('animate').bind('webkitAnimationEnd', function () {
                        $(this).css({
                            'font-size':'110px'
                        });
                    })
                    break;
                case 6:
                    break;
            }
        },

        init: function (domList) {

            var me = this;
            me.initElements();
            me.domList = [];
            var curWindow = $(window);
            me.winH = curWindow.height();

            me.winW = curWindow.width();

//            console.log('me.winH ' + me.winH);
//            console.log('me.winW ' + me.winW);

            me.panel = $('#dom_view').css({
                /*'top': 0,
                 'left': 0,*/
                'width': me.winW + 'px',
                'height': me.winH + 'px',
                'overflow': 'hidden',
                'margin': '0 auto',
                'position': 'relative'
            });

            var newdom, viewitem;

            for (var i in domList) {

//                newdom = $($(domList[i]).clone());

//                viewitem = $('<div style="position:absolute;left:0;top:' + i * me.winH + 'px"></div>');
//                var curWindow = $(window);
//                viewitem.height(curWindow.height());
//                newdom.appendTo(viewitem);
//
//                viewitem.appendTo(me.panel);
//
                me.domList.push($(domList[i]));

                $(domList[i]).css({
                    width: 100 + '%',
                    height: me.winH,
                    top: i * me.winH,
                    left:0
                });
                //$(domList[i]).hide();
            }


            //me.selection = 0;
            me.selection=parseInt(Jit.AM.getUrlParam('pos_index'))||0;
            me.initDomPosition();
            //移除原有结构
//            $('#intoListArea').remove();

            me.toggleArrow();
            me.initEvent();
            me.share();
        },
        state: {
            touchStartY: null,
            touchStartX: null,
            touchEndY: null,
            touchEndX: null,
            press: false,
            pressTime: null
        },

        handleDom: null,

        initEvent: function () {

            var me = this;

            me.panel.bind('touchstart', this.touchStart);

            me.panel.bind('touchmove', this.touchMove);

            me.panel.bind('touchend', this.touchEnd);

            document.body.addEventListener('touchmove', function (e) {
                me.stopEvent(e);
            },false);

            /*$('.into_sub_text').bind('touchstart', function() {
             var el = $(this);
             if (el.hasClass('on')) {
             el.removeClass('off');


             }else{
             el.addClass('off');


             }
             });*/
//            me.scrollEvent();
        },
        scrollEvent: function () {
            self = this;
            // 绑定滚动事件
            var myScroll, isWidth = 0;
            //重新设置大小
            ReSize();

            function ReSize() {
                self.elements.alumnusPicList.each(function (i) {
                    var el = $(this);
                    isWidth += el.width() + 8;
                });
                isWidth += 18;
                self.elements.scrollList.css({
                    width: isWidth
                });
            }

            myScroll = new iScroll('alumnusPicList', {
                snap: true,
                momentum: false,
                hScrollbar: false,
                vScroll: false
            });
            $(window).resize(function () {
                ReSize();
                myScroll.refresh();
            });
        },

        Lock: false,

        LockAnima: false,

        scrollTo: function (index, tob) {
            this.MoveDomToScene(ImgV.domList[index], tob || "top", tob ? 500 : 0);
        },
        initDomPosition:function(){
            ImgV.domList[ImgV.selection].css({top: 0});
        },
        MoveDomToScene: function (item, tob, time) {
            if (ImgV.Lock) {
                return;
            }
            /*if(ImgV.LockAnima){//动画执行过程中
             time=0;
             //                ImgV.LockAnima = false;
             }else{
             ImgV.LockAnima = true;
             }*/

            ImgV.LockAnima = true;


            if (time == "undefined") {
                time = 500;
            }
            $(item).css({
                'z-index': 3
            });
            function animateCallback() {
                $(ImgV.domList[ImgV.selection]).css({
                    'z-index': 1
                });

                if (tob == 'top') {

                    ImgV.selection--;

                } else if (tob == 'bottom') {

                    ImgV.selection++;
                }

                $(item).css({
                    'z-index': 2
                });

                ImgV.LockAnima = false;
                ImgV.toggleArrow();
                ImgV.share();
                ImgV.pageAnimate();
            }

            if (time == 0) {
                $(item).css({top: 0});
                animateCallback();
            } else {
                $(item).animate({
                    'top': 0
                }, animateCallback, time);
            }

        },
        touchStart: function (evt) {
//            console.log(evt.target);
            if (ImgV.Lock || ImgV.LockAnima) {

                return;
            }

            ImgV.Lock = true;

            var me = ImgV;

            me.state.pressTime = (new Date()).getTime();
            //兼容jquery触摸事件
            var pageY = evt.touches ? evt.touches[0].pageY : evt.originalEvent.touches[0].pageY;
            var pageX = evt.touches ? evt.touches[0].pageX : evt.originalEvent.touches[0].pageX;
            me.state.touchStartY = pageY;
            me.state.touchStartX = pageX;
//            if(!evt.target.classList.contains('self-event'))
//                ImgV.stopEvent(evt);
        },
        touchMove: function (evt) {
//            console.log(evt.target);
            if (!ImgV.Lock || ImgV.LockAnima) {

                return;
            }
            var me = ImgV,
                selection;
            //兼容jquery触摸事件
            var pageY = evt.changedTouches ? evt.changedTouches[0].pageY : evt.originalEvent.changedTouches[0].pageY;
            var pageX = evt.changedTouches ? evt.changedTouches[0].pageX : evt.originalEvent.changedTouches[0].pageX;
            //var pageY = pageY;

            selection = ImgV.selection;
//            if(Math.absabs()me.state.touchStartY)
            if (Math.abs(me.state.touchStartY - pageY) - Math.abs(me.state.touchStartX - pageX) > 0) {//手势，垂直滑动
                if (pageY > me.state.touchStartY) {
                    //手指向下滑

                    if (selection >= 1) {

                        ImgV.handleDom = ImgV.domList[selection - 1];

                        ImgV.handleDom.css({
                            'z-index': 3,
                            'top': -ImgV.winH + pageY - me.state.touchStartY + 'px'
                        });

                    }

                } else {
                    //console.log(pageY,me.state.touchStartY,selection<ImgV.domList.length);
//                    console.log(selection);
//                    console.log(ImgV.domList.length - 1);
                    if (selection < ImgV.domList.length - 1) {

                        ImgV.handleDom = ImgV.domList[selection + 1];
//                        console.log(ImgV.domList);
//                        console.log(selection + 1);
//                        console.log(ImgV.domList[selection + 1]);

                        ImgV.handleDom.css({
                            'z-index': 3,
                            'top': ImgV.winH - (me.state.touchStartY - pageY) + 'px'
                        });

                    }

                }
            }


//            ImgV.stopEvent(evt);
            //console.log(evt.changedTouches[0].pageY);
        },
        touchEnd: function (evt) {
//            console.log(evt.target);
            var me = ImgV,
                nowtime = (new Date()).getTime();

            if (!ImgV.Lock || ImgV.LockAnima) {

                return;
            }

            ImgV.Lock = false;

            //var pageY = evt.changedTouches[0].pageY;
            //兼容jquery触摸事件
            var pageY = evt.changedTouches ? evt.changedTouches[0].pageY : evt.originalEvent.changedTouches[0].pageY;
            var pageX = evt.changedTouches ? evt.changedTouches[0].pageX : evt.originalEvent.changedTouches[0].pageX;
            if (Math.abs(me.state.touchStartY - pageY) - Math.abs(me.state.touchStartX - pageX) > 0) {//手势，垂直滑动


                if (nowtime - me.state.pressTime < 200) {
                    //快速滑动

                    if (me.state.touchStartY - pageY > 10 && ImgV.selection < ImgV.domList.length - 1) {
                        //向上滑
                        ImgV.handleDom = ImgV.domList[ImgV.selection + 1];

                        ImgV.MoveDomToScene(ImgV.handleDom, 'bottom');

                    } else if (pageY - me.state.touchStartY > 10 && ImgV.selection >= 1) {
                        //向下滑
                        ImgV.handleDom = ImgV.domList[ImgV.selection - 1];

                        ImgV.MoveDomToScene(ImgV.handleDom, 'top');

                    } else {

                        /*if (me.state.touchStartY > pageY && ImgV.selection < ImgV.domList.length - 1) {

                         ImgV.reBackDom(1);

                         } else if (me.state.touchStartY < pageY && ImgV.selection >= 1) {

                         ImgV.reBackDom(-1);
                         }*/
                        ImgV.reBackDom();
                    }
//                    ImgV.stopEvent(evt);
                } else if (nowtime - me.state.pressTime > 200) {

                    if (me.state.touchStartY - pageY > 100 && ImgV.selection < ImgV.domList.length - 1) {
                        //手指向上滑
                        ImgV.MoveDomToScene(ImgV.handleDom, 'bottom');

                    } else if (pageY - me.state.touchStartY > 100 && ImgV.selection >= 1) {
                        //手指向下滑
                        ImgV.MoveDomToScene(ImgV.handleDom, 'top');

                    } else {
                        ImgV.reBackDom();
                        /*if ((me.state.touchStartY > pageY && ImgV.selection < ImgV.domList.length - 1)||(me.state.touchStartY < pageY && ImgV.selection >= 1)) {       //上下屏收缩

                         ImgV.reBackDom();

                         }*/
                        /* else if (me.state.touchStartY < pageY && ImgV.selection >= 1) {   //上一屏收缩

                         ImgV.reBackDom();
                         }*/
                    }
                }
            } else {
                ImgV.reBackDom();
            }
//            ImgV.stopEvent(evt);
        },
        reBackDom: function (direction) {
            var prev_selection = ImgV.selection - 1;
            var next_selection = ImgV.selection + 1;
            /*if(direction==1){
             selection= ImgV.selection + 1;
             }else{
             selection= ImgV.selection - 1;
             }*/

            /*console.log(prev_selection);
             console.log(next_selection);
             console.log(ImgV.domList[prev_selection]);
             console.log(ImgV.domList[next_selection]);*/
            try {
                ImgV.domList[prev_selection].animate({/*-(ImgV.winH + 60)*/
                    'top': prev_selection * ImgV.winH + 'px',
//                'top': 0,
                    'z-index': 1
                }, 100);
                ImgV.domList[next_selection].animate({/*-(ImgV.winH + 60)*/
                    'top': next_selection * ImgV.winH + 'px',
//                'top': 0,
                    'z-index': 1
                }, 100);
            } catch (ex) {

            }
        },
        stopEvent: function (e) {

            var evt = e || window.event;

            evt.stopPropagation ? evt.stopPropagation() : (evt.cancelBubble = true);

            evt.preventDefault ? evt.preventDefault() : null;
        }
    }

    Jit.UI.DomView = ImgV;
})()

Jit.AM.defindPage({
    name: 'Home',
    elements: {
        domView: ''
    },
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function () {
        //alert(1);
        var self = this;
        self.elements.domView = $('#dom_view');
        var curWindow = $(window);
        self.elements.domView.find('.showitem').css({
            'position':'absolute',
            'top':0,
            'left':0,
            'height':curWindow.height(),
            'width':curWindow.width()
        });
        self.percentValue();
    },
    //页面元素百分比换算
    percentValue:function(){
        var winH=$(window).height();
        $('div[data-height]').each(function(){
            var height=$(this).attr('data-height');
            //console.log(height);
            $(this).css({
                height:Math.ceil(height/100*winH)+'px'
            });
        });
    },

    //绑定事件
    initEvent: function () {
        var self = this;
        Jit.UI.DomView.init(['#subInto1', '#subInto2', '#subInto3', '#subInto4', '#subInto5', '#subInto6', '#subInto7', '#subInto8']);

        $('.onlineApplyBtn,.commonApplyBtn').bind('click', function () {
            window.location.href = "http://www.ckgsb.edu.cn/emba/pages/index/130";
            return false;
        });
        //独特优势幻灯
        $(".bxslider").bxSlider({
            controls: false,
            onSlideAfter: function ($slideElement, oldIndex, newIndex) {
                $('#subInto3 .title').hide().eq(newIndex).show();
                $('#subInto3 .exp').hide().eq(newIndex).show();
            }
        });

        /*//长江活动幻灯
         $("#scrollList").bxSlider({
         controls:false,
         pager:false,
         maxSlides: 3,
         slideWidth: 79,
         pager: false,
         moveSlides: 1,
         onSlideAfter: function ($slideElement, oldIndex, newIndex) {
         //alert(newIndex);
         $('.activityList a').removeClass('on');
         $('.activityList a').eq(newIndex).addClass('on');
         }
         });*/
        var mySwiper = $('.swiper-container').swiper({
            //Your options here:
            initialSlide:0,
            slidesPerView: 3,
            loop: true
        });

//        self.indexText();
    }
    //首页中间白色区域文字飞入效果
    /*,indexText: function () {
        $index_area_center_p = $('#index_area_center>p');
//        console.log($index_area_center_p);
        $index_area_center_p.eq(0).animate({
            left: 13.35 + '%'
        }, 1000, '', function () {
            $index_area_center_p.eq(1).animate({
                left: 44.84 + '%'
            }, 1000, '', function () {
            });
        });
        $index_area_center_p.eq(2).animate({
            left: 6.41 + '%'
        }, 1000, '', function () {
            $index_area_center_p.eq(3).animate({
                left: 30 + '%'
            }, 1000, '', function () {

            });
        });
    }*/
});