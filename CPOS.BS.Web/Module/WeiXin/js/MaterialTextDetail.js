define(['jquery', 'tools'], function () {
    var WeiXin = {
        shareInfo: {},
        OptionMenu: function (flag) {
            if (typeof WeixinJSBridge == 'object') {
                WeixinJSBridge.call(flag ? 'showOptionMenu' : 'hideOptionMenu');
            } else {
                document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
                    WeixinJSBridge.call(flag ? 'showOptionMenu' : 'hideOptionMenu');
                });
            }
        },
        ToolBar: function (flag) {
            if (typeof WeixinJSBridge == 'object') {
                WeixinJSBridge.call(flag ? 'showToolbar' : 'hideToolbar');
            } else {
                document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
                    WeixinJSBridge.call(flag ? 'showToolbar' : 'hideToolbar');
                });
            }
        },
        fnShare: function (o) {
            var map = {
                'Friends': {
                    'mKey': 'appmessage',
                    'invoke': 'sendAppMessage'
                }
            };
            function share(title, desc, link, imgUrl) {
                if (typeof WeixinJSBridge == 'object') {
                    WeixinJSBridge.on('menu:share:appmessage', function (argv) {

                        WeixinJSBridge.invoke('sendAppMessage', {
                            //"appid":appId,
                            "img_url": imgUrl || ('http://' + location.host + '/HtmlApps/images/common/jitico.jpg'),
                            //"img_width":"320",
                            //"img_height":"320",
                            "link": link,
                            "desc": desc,
                            "title": title
                        }, function (res) {
                            //分享成功
                            if (res.err_msg.indexOf('ok') != -1 || res.err_msg.indexOf('confirm') != -1) {
                            }
                        })
                    });
                    WeixinJSBridge.on('menu:share:timeline', function (argv) {
                        WeixinJSBridge.invoke('shareTimeline', {
                            //"appid":appId,
                            "img_url": imgUrl || ('http://' + location.host + '/HtmlApps/images/common/jitico.jpg'),
                            //"img_width":"320",
                            //"img_height":"320",
                            "link": link,
                            "desc": desc,
                            "title": title
                        }, function (res) {
                            //分享成功
                            if (res.err_msg.indexOf('ok') != -1 || res.err_msg.indexOf('confirm') != -1) {

                            }
                        })
                    });
                    return true;
                } else {
                    return false;
                }
            }
            var runCount = 20;
            function dofn(o, count) {
                return (function () {
                    if (share(o.title, o.desc, o.link, o.imgUrl) || count <= 0) {
                        clearInterval(window.WX_Share_timer);
                        window.WX_Share_timer = null;
                    }
                    count--;
                });
            };
            var hashdo = dofn(o, 20);
            if (window.WX_Share_timer) {
                clearInterval(window.WX_Share_timer);
            }
            window.WX_Share_timer = setInterval(hashdo, 300);
        },
        initShare: function () {

            var me = this;
            me.fnShare({
                'link': location.href,
                'title': document.title,
                'desc': "快来瞧瞧吧",
                'imgUrl': ""
            });
        },
        //分享到朋友圈
        shareTimeline: function (title, desc, link, imgUrl) {
            var me = this;
            if (arguments.length == 4) {
                me.fnShare({
                    'title': title,
                    'desc': desc,
                    'link': link,
                    'imgUrl': imgUrl
                });
            } else if (typeof arguments[0] == 'object') {
                me.fnShare(arguments[0]);
            }
        },
        //发送给好友
        shareFriends: function (title, desc, link, imgUrl) {
            var me = this;
            if (arguments.length == 4) {
                me.fnShare({
                    'title': title,
                    'desc': desc,
                    'link': link,
                    'imgUrl': imgUrl
                });
            } else if (typeof arguments[0] == 'object') {
                me.fnShare(arguments[0]);
            }
        },
        //添加关注
        addContact: function (name, callback) {
            if (typeof WeixinJSBridge == 'object') {
                WeixinJSBridge.invoke('addContact', { webtype: '1', username: name }, function (e) {
                    WeixinJSBridge.log(e.err_msg);
                    //e.err_msg:add_contact:added 已经添加
                    //e.err_msg:add_contact:cancel 取消添加
                    //e.err_msg:add_contact:ok 添加成功
                    if (e.err_msg == 'add_contact:added' || e.err_msg == 'add_contact:ok') {
                        //关注成功，或者已经关注过
                        callback(true);
                    }
                })
            }

        },
        /**
        * 调起微信Native的图片播放组件。
        * 这里必须对参数进行强检测，如果参数不合法，直接会导致微信客户端crash
        *
        * @param {String} curSrc 当前播放的图片地址
        * @param {Array} srcList 图片地址列表
        */
        imagePreview: function (curSrc, srcList) {
            if (!curSrc || !srcList || srcList.length == 0) {
                return;
            }
            if (typeof WeixinJSBridge == 'object') {
                WeixinJSBridge.invoke('imagePreview', {
                    'current': curSrc,
                    'urls': srcList
                });
            }
        }
    };
    var page =
        {
            option: {
                interfaceHost: "",
                textId: "",
                customerId: ""
            },
            init: function () {
                var that = this;
                var type = $.util.getUrlParam("type");
                var pageSize = $.util.getUrlParam("psize");

                var textId = "",
		        customerId = "",
		        interfaceHost = "";
                this.option.textId = $.util.getUrlParam("news_id");
                this.option.customerId = $.util.getUrlParam("customerId");
                this.option.interfaceHost = $.util.getUrlParam("interfacehost");
                var title = "", description = "", imgUrl = "";
                //获得详情
                this.getMaterialDetail(function (data) {
                    var list = data.Data.MaterialTextTitleList;
                    title = list.Title;
                    description = list.Author || $(list.Text).text().substring(0, 50);
                    var text = list.Text;
                    $("#title").html(title);
                    $("#content").append(text);
                    $('#content').find('img').each(function (i) {
                        var element = $(this);
                        var aWrap = $('<a>').attr({ "data-lightbox": 'imgbox' + i, 'href': element.attr('src') });
                        element.wrap(aWrap);
                    });
                    imgUrl = list.CoverImageUrl;
                    //获得图片地址
                    var imgs = $(text).find("img");
                    if ((!!!imgUrl)&&imgs.length > 0) {
                        imgUrl = $(imgs[0]).attr("src");
                    }
                    setTimeout(function () {
                        WeiXin.fnShare({
                            'link': location.href,
                            'title': title,
                            'desc': description,
                            'imgUrl': imgUrl
                        });
                    }, 1500);
                });
            },
            //获得详情
            getMaterialDetail: function (callback) {

                $.util.ajax({
                    url: this.option.interfaceHost + "ApplicationInterface/Gateway.ashx",
                    type: "post",
                    async: false,  //同步请求
                    data:
                    {
                        'action': 'WX.MaterialText.GetMaterialTextDetail',
                        'TextId': this.option.textId,
                        'CustomerId': this.option.customerId

                    },
                    success: function (data) {
                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            }
        }
    //初始化
    page.init();
});