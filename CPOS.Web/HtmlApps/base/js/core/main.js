(function () {

    var debugInfo = {
        'openId': 'oRBZQuNkyXc9C5GeAwYt3xTS4xQw',
        'userId': '7c4aeb1158984813aa22a15675a06751',
        'locale': 'ch'
    }

    var appcfg = Jit.AM.getAppVersion();

    Jit.WX.ToolBar(appcfg['APP_TOOL_BAR']);

    Jit.WX.OptionMenu(appcfg['APP_OPTION_MENU']);

    Jit.WX.initShare();

    if (appcfg['APP_DEBUG_PANEL']) {

        $('body').append('<div class="jit-debug-panel"></div>');
    }

    var version = ((appcfg.APP_CACHE === false || appcfg.APP_CACHE == 'False') ? ((new Date()).getTime()) : appcfg.APP_VERSION);

    Jit.log('APP_VERSION : ' + version);

    Jit.log('APP_NAME: ' + appcfg['APP_NAME']);

    var PageConfigs = Jit.AM.getAppPageConfig();

    var htmlname = $('title').attr('name');

    if (!htmlname) {

        alert('Html 中页面名称未定义');

        return;
    }

    var pageconfig = PageConfigs[htmlname];

    if (!pageconfig) {

        alert('Config.js 中未找到对应的HTML');

        return;
    }

    var urlparam = location.href.split('?')[1];

    if (urlparam) {

        Jit.AM.pageHistoryPush(htmlname + ':' + urlparam);

    } else {

        Jit.AM.pageHistoryPush(htmlname);
    }

    $('title').html(Jit.AM.getUrlParam('title') || pageconfig.title);

    var JsLoad = false, CssLoad = false;

    var styles = [], styl
    common = null;

    if (PageConfigs['Config'] && PageConfigs['Config']['Common']) {

        common = PageConfigs['Config']['Common'];
    }

    if (common && common['style']) {

        var commonstyles = common['style'];

        for (var i = 0; i < commonstyles.length; i++) {

            styl = commonstyles[i];

            styl = styl.replace(/%(\S*)%/, function (str) {

                return PageConfigs['Config']['Shorthand'][str.substring(1, str.length - 1)];
            })

            styles.push(JitCfg.baseUrl + 'css/' + styl + '.css?version=' + version);
        }
    }

    if (!!pageconfig['style']) {

        for (var i = 0; i < pageconfig.style.length; i++) {

            styl = pageconfig.style[i];

            styl = styl.replace(/%(\S*)%/, function (str) {

                return PageConfigs['Config']['Shorthand'][str.substring(1, str.length - 1)];
            })
            if (styl) {

                styles.push(JitCfg.baseUrl + 'css/' + styl + '.css?version=' + version);
            }
        }
    }

    function checkCssLoadComplete() {

        var cssTimer = setInterval(function () {

            var loadnum = 0 , cssList = $('link');
            if (!cssList || !cssList.size()) {
                clearInterval(cssTimer);

                CssLoad = true;

                checkFileHasLoad();
                return false;
            }
            ;


            cssList.each(function () {

                var me = this;
                /*
                 @EditBy:junsheng.cheng
                 link 标签如果不带 type='text/css' 会导致无法检测到css加载完成，所以该段代码先注释掉
                 if(me.type != 'text/css'){

                 return false;
                 }
                 */

                if (me.sheet && me.sheet.cssRules) {

                    loadnum++;
                }
            });

            if (loadnum >= styles.length) {

                clearInterval(cssTimer);

                CssLoad = true;

                checkFileHasLoad();
            }
        }, 500);
    }

    Jit.loadFiles(styles);

    checkCssLoadComplete();

    function loadJs(list, type, callback) {

        if (!$.isArray(list))
            return;

        var js_arr = [],
            type = ( type == 'plugin' ? JitCfg.baseUrl + 'plugin/' : JitCfg.baseUrl + 'js/' );

        $.each(list, function (key, val) {

            val = val.replace(/%(\S*)%/, function (str) {

                return PageConfigs['Config']['Shorthand'][str.substring(1, str.length - 1)];
            })

            if (val) {

                js_arr.push(type + val + '.js?version=' + version);
            }
        });

        require(js_arr, callback);
    }

    function loadPageScript(allscripts) {

        if ($.isArray(allscripts) && allscripts.length > 0) {

            loadJs(allscripts, 'script', function () {

                Jit.log('script files load complete');

                JsLoad = true;

                checkFileHasLoad();
            });

        } else {

            JsLoad = true;

            checkFileHasLoad();
        }
    }
    if (pageconfig) {

        var plugins = pageconfig.plugin ? pageconfig.plugin : [],
			allscripts = pageconfig.script ? pageconfig.script : [];

        if (common && common['script'] && common['script'].length > 0) {

            allscripts = common['script'].concat(allscripts);
        }

        if ($.isArray(plugins) && plugins.length > 0) {

            loadJs(plugins, 'plugin', function () {

                loadPageScript(allscripts);
            });

        } else {

            loadPageScript(allscripts);
        }
    }

    function checkFileHasLoad() {

        if (CssLoad && JsLoad) {

            main();
        }
    }

    var fileloadtimer = null, timeout = 12000;

    function loadstatus() {

        if (fileloadtimer) {

            var c = confirm('您的网络不给力，页面加载失败！是否继续等待？');

            if (c) {

                fileloadtimer = setTimeout(loadstatus, timeout);

            } else {

                clearTimeout(fileloadtimer);

                $('.wx_loading_inner').html('装载页面样式及脚本失败，请稍后重试！').css({
                    'width': '152px',
                    'padding': '12px'
                });
            }
        }
    }

    fileloadtimer = setTimeout(loadstatus, timeout);

    function main() {

        clearTimeout(fileloadtimer);

        var cfg = Jit.AM.getAppVersion();

        Jit.log('current run model :' + (cfg.APP_DEBUG ? 'Debug' : 'Release'));

        Jit.log('customer id : ' + Jit.AM.CUSTOMER_ID);

        if (Jit.AM.getUrlParam('APP_TYPE')) {

            cfg['APP_TYPE'] = Jit.AM.getUrlParam('APP_TYPE');
        }

        if (cfg.APP_DEBUG != 'False' && cfg.APP_DEBUG) {

            /* Debug模式 测试使用**/

            debugInfo.customerId = Jit.AM.CUSTOMER_ID;

            Jit.AM.setBaseAjaxParam(debugInfo);

        } else if (Jit.AM.getUrlParam('openId')) {

            var keys = cfg.AJAX_PARAMS.split(',');

            var param = {};

            for (var key in keys) {

                hash = Jit.AM.getUrlParam(keys[key]);

                if (hash) {

                    param[keys[key]] = hash;
                }

            }

            param.customerId = Jit.AM.CUSTOMER_ID;

            Jit.AM.setBaseAjaxParam(param);
        }

        var info = Jit.AM.getBaseAjaxParam();

        if (!cfg.APP_DEBUG && cfg['APP_TYPE'] && cfg['APP_TYPE'] == 'SERVICE') {
            //服务号

            Jit.log('公共帐号类型：服务号');

            var hasAuth = Jit.AM.getUrlParam('CheckOAuth');

            if ((!info.userId && hasAuth != 'unAttention')
                || (Jit.AM.getAppParam('Launch', 'CheckOAuth') == 'unAttention' && hasAuth != 'unAttention' && !Jit.AM.getUrlParam('openId'))) {

                var url = window.location.href.replace('http://', ''), surl = '',
                    customerId = Jit.AM.getUrlParam('customerId');

                var authType = Jit.AM.getUrlParam('authType');

                url = url.replace(/&+/gi, '%26');

                url = url.replace('customerId=' + customerId, '');

                surl = '/WXOAuth/AuthUniversal.aspx?&customerId=' + customerId;

                if (authType) {

                    surl = surl + '&scope=' + authType;
                }

                surl = surl + '&goUrl=' + url;

                window.location.href = surl;

                return;

            } else if (!info.userId && hasAuth == 'unAttention') {

                Jit.log('未关注用户，并且没有userId');

                Jit.AM.setAppParam('Launch', 'CheckOAuth', 'unAttention');

                if (!info.userId) {

                    var userId = Jit.AM.buildUserId();

                    Jit.AM.setBaseAjaxParam({
                        'customerId': Jit.AM.CUSTOMER_ID,
                        'userId': userId,
                        'openId': userId
                    });
                }

                JitCfg.CheckOAuth = 'unAttention';

            } else {

                Jit.AM.setAppParam('Launch', 'CheckOAuth', 'Attention');
            }

        } else if (!cfg.APP_DEBUG && !info.userId && cfg['APP_TYPE'] && (cfg['APP_TYPE'] == 'SUBSCIBE' || cfg['APP_TYPE'] == 'SUBSCIBE_NOINFO')) {
            //订阅号

            Jit.log('公共帐号类型：订阅号');

            if (cfg['APP_TYPE'] == 'SUBSCIBE_NOINFO') {

                Jit.AM.setBaseAjaxParam({
                    'customerId': Jit.AM.CUSTOMER_ID,
                    'userId': null,
                    'openId': null
                });

            } else {

                var userId = Jit.AM.buildUserId();

                Jit.AM.setBaseAjaxParam({
                    'customerId': Jit.AM.CUSTOMER_ID,
                    'userId': userId,
                    'openId': userId
                });
            }
        }

        delete debugInfo;

        info = Jit.AM.getBaseAjaxParam();

        Jit.log('user id : ' + info.userId);

        if (Jit.AM.onLoad) {

            Jit.AM.onLoad();

            Jit.log('Jit.AM.onLoad start');
        }
        if (Jit.AM.isPageNeedLog()) {

            if (Jit.AM.getUrlParam('isShare') == 'YES') {

                Jit.AM.logToServer('browserForward');
            } else {

                Jit.AM.logToServer('browser');
            }
        }

        Jit.UI.Nav.init();

        if (pageconfig.param && pageconfig.param.Navigation) {

            var items = $('#topNav li');

            var navcfg = pageconfig.param.Navigation;

            if (!navcfg) {

                return;
            }

            if (items.length != navcfg.count) {

                var htmls = '';

                for (var i = 0; i < navcfg.count; i++) {

                    htmls += "<li><a href=\"" + (navcfg.href[i] || '') + "\"></a></li>";
                }

                $('#topNav ul').html(htmls);

            } else {

                for (var i in navcfg.href) {

                    $(items.eq(i).find('a')).attr('href', (navcfg.href[i] || ''));
                }
            }
        }

        //页面对象JitPage 存在hideMask方法时，由页面本身控制MaskLayer的显示与隐藏
        try {
            if (typeof JitPage != "undefined") {

                if (!JitPage.hideMask) {

                    $('#masklayer').hide();
                }
            } else {

                $('#masklayer').hide();
            }
        } catch (e) {

            alert(e.message);
        }
    }
})()

