(function() {

    var ImgV = {

        domList: null,

        init: function(domList) {

            var me = this;

            me.domList = [];

            me.winH = window.innerHeight;

            me.winW = window.innerWidth;

            console.log('me.winH '+ me.winH);

            me.panel = $('<div name="domView"></div>').appendTo('body').css({
                'top': 0,
                'left': 0,
                'width': me.winW + 'px',
                'height': me.winH + 'px',
                'over-flow': 'hidden',
                'position': 'fixed'
            });

            var newdom, viewitem;

            for (var i in domList) {

                newdom = $($(domList[i]).clone());

                viewitem = $('<div style="background-color:#333333; position:absolute;left:0;top:' + i * me.winH + 'px"></div>');
                var curWindow = $(window);
                viewitem.height(curWindow.height());
                newdom.appendTo(viewitem);

                viewitem.appendTo(me.panel);

                me.domList.push(viewitem);

                $(domList[i]).hide();
            }

            me.selection = 0;

            me.initEvent();
        },
        state: {
            touchStartY: null,
            touchEndY: null,
            press: false,
            pressTime: null
        },

        handleDom: null,

        initEvent: function() {

            var me = this;

            me.panel.bind('touchstart', this.touchStart);

            me.panel.bind('touchmove', this.touchMove);

            me.panel.bind('touchend', this.touchEnd);
        },

        Lock: false,

        LockAnima:false,

        scrollTo:function(index,tob){
            this.MoveDomToScene(ImgV.domList[index],tob||"top",tob?500:0);
        },
        MoveDomToScene: function(item, tob, time) {
            if (ImgV.Lock) {
                return;
            }

            ImgV.LockAnima = true;

            if(time=="undefined"){
                time=500;
            }
            $(item).css({
                'z-index': 3,
            });
            function animateCallback(){
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
            }
            if(time==0){
                $(item).css({top:0});
                animateCallback();
            }else{
                $(item).animate({
                    'top': 0,
                }, animateCallback,time);
            }

        },
        touchStart: function(evt) {

            if (ImgV.Lock || ImgV.LockAnima) {

                return;
            }

            ImgV.Lock = true;

            var me = ImgV;

            me.state.pressTime = (new Date()).getTime();
            //兼容jquery触摸事件
            var pageY=evt.touches?evt.touches[0].pageY:evt.originalEvent.touches[0].pageY;
            me.state.touchStartY = pageY;

            ImgV.stopEvent(evt);
        },
        touchMove: function(evt) {

            if (!ImgV.Lock || ImgV.LockAnima) {

                return;
            }
            var me = ImgV,
                selection;
            //兼容jquery触摸事件
            var pageY=evt.changedTouches?evt.changedTouches[0].pageY:evt.originalEvent.changedTouches[0].pageY;
            var pageY = pageY;

            selection = ImgV.selection;

            if (pageY > me.state.touchStartY) {
                //向下滑

                if (selection >= 1) {

                    ImgV.handleDom = ImgV.domList[selection - 1];

                    ImgV.handleDom.css({
                        'z-index': 3,
                        'top': -ImgV.winH + pageY - me.state.touchStartY + 'px'
                    });

                }

            } else {
                //console.log(pageY,me.state.touchStartY,selection<ImgV.domList.length);

                if (selection < ImgV.domList.length - 1) {

                    ImgV.handleDom = ImgV.domList[selection + 1];

                    ImgV.handleDom.css({
                        'z-index': 3,
                        'top': ImgV.winH - (me.state.touchStartY - pageY) + 'px'
                    });

                }

            }

            //console.log(evt.changedTouches[0].pageY);
        },
        touchEnd: function(evt) {

            var me = ImgV,
                nowtime = (new Date()).getTime();

            if (!ImgV.Lock || ImgV.LockAnima) {

                return;
            }

            ImgV.Lock = false;

            //var pageY = evt.changedTouches[0].pageY;
            //兼容jquery触摸事件
            var pageY=evt.changedTouches?evt.changedTouches[0].pageY:evt.originalEvent.changedTouches[0].pageY;
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

                    if (me.state.touchStartY > pageY && ImgV.selection < ImgV.domList.length - 1) {

                        ImgV.reBackDom();

                    } else if (me.state.touchStartY < pageY && ImgV.selection >= 1) {

                        ImgV.reBackDom();
                    }
                }
            } else if (nowtime - me.state.pressTime > 200) {

                if (me.state.touchStartY - pageY > 100 && ImgV.selection < ImgV.domList.length - 1) {
                    //向上滑
                    ImgV.MoveDomToScene(ImgV.handleDom, 'bottom');

                } else if (pageY - me.state.touchStartY > 100 && ImgV.selection >= 1) {
                    //向下滑
                    ImgV.MoveDomToScene(ImgV.handleDom, 'top');

                } else {

                    if (me.state.touchStartY > pageY && ImgV.selection < ImgV.domList.length - 1) {

                        ImgV.reBackDom();

                    } else if (me.state.touchStartY < pageY && ImgV.selection >= 1) {

                        ImgV.reBackDom();
                    }
                }
            }
            ImgV.stopEvent(evt);
        },
        reBackDom: function() {

            $(ImgV.handleDom).css({
                'top': -(ImgV.winH+60) + 'px',
                'z-index':1
            });
        },
        stopEvent: function(e) {

            var evt = e || window.event;

            evt.stopPropagation ? evt.stopPropagation() : (evt.cancelBubble = true);

            evt.preventDefault ? evt.preventDefault() : null;
        }
    }

    Jit.UI.DomView = ImgV;
})()