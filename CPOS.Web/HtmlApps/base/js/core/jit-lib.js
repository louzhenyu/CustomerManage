
var JitCfg = {
    'baseUrl':'../../../',
    'ajaxUrl':'http://o2oapi2.test.aladingyidong.com',
    //http://o2oapi.aladingyidong.com
    'shareIco':'http://o2oapi.aladingyidong.com/HtmlApps/images/common/jitico.jpg',
    'statisticsCode':'<img src="http://dev.o2omarketing.cn:999/piwik.php?idsite=1&rec=1" style="border:0" alt="" />'
};
(function(global){

    function trim(str){

        return str.replace(/(\s)|(\r\n)|(\r)|(\n)/gi, "");
    }

    function strToJson(jsonStr){
        jsonStr = trim(jsonStr);

        return eval( '(' + jsonStr + ')' );
    }

    function getOffsetPos(obj){

        var _top=obj.offsetTop;

        var _left=obj.offsetLeft

        if(obj.offsetParent!=null){

            var opos = getOffsetPos(obj.offsetParent);

            _top += opos.top;

            _left += opos.left;
        }

        return {'top':_top,'left':_left};
    }

    /*
     #随机数函数
     parameter：
     (number)section 随机区间
     (number)start 随机起步值
     return:
     (int)
     @memberOf gc.fn
     * */
    function random(section,start){
        if(start != null){
            return Math.floor(Math.random()*section) + 1 + start;
        }else{
            return Math.floor(Math.random()*section) + 1;
        }
    }

    /**
     * 随机数函数（值>=start&&值<=end）
     * @param start 起始值
     * @param end 结束值
     * @returns {*}
     */
    function randomTo(start,end){
        var max=end-start+1;
        return Math.floor(Math.random()*max) + start;
    }

    function bind(func, scope){
        return function(){
            return func.apply(scope, arguments);
        }
    }

    function loadFiles(urls,callback){

        var _file,
            _Head = document.getElementsByTagName("HEAD").item(0);


        function createElement(tag){

            var element = document.createElement(tag == 'script' ? 'script' : 'link');

            if(tag == 'script'){

                element.setAttribute('type','text/javascript');

            }else{

                element.setAttribute('rel','stylesheet');

                element.setAttribute('type','text/css');
            }

            return element;
        }


        if(typeof urls == 'string'){

            urls = [urls];
        }

        var hasloadfilescount = 0,
            needloadfilescount = urls.length;

        for(var i in urls){

            if(urls[i].indexOf('.js')!=-1){

                _file = createElement('script');

                _Head.appendChild(_file);

                _file.src = urls[i];
            }

            if(urls[i].indexOf('.css')!=-1){

                _file = createElement('link');

                _Head.appendChild(_file);

                _file.href = urls[i];

            }

            if(typeof callback == 'function'){

                _file.onload = function(){

                    hasloadfilescount++;

                    if(hasloadfilescount>=needloadfilescount){

                        callback();
                    }
                };

                _file.onerror = function(){

                    hasloadfilescount++;

                    if(hasloadfilescount>=needloadfilescount){

                        callback();
                    }
                }
            }
        }

    }
    ///判定手机端还是pc端
    function deviceType() {

        var sUserAgent= navigator.userAgent.toLowerCase();

        var bIsIpad= sUserAgent.match(/ipad/i) == "ipad";

        var bIsIphoneOs= sUserAgent.match(/iphone os/i) == "iphone os";

        var bIsMidp= sUserAgent.match(/midp/i) == "midp";

        var bIsUc7= sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4";

        var bIsUc= sUserAgent.match(/ucweb/i) == "ucweb";

        var bIsAndroid= sUserAgent.match(/android/i) == "android";

        var bIsCE= sUserAgent.match(/windows ce/i) == "windows ce";

        var bIsWM= sUserAgent.match(/windows mobile/i) == "windows mobile";

        if (bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) {

            return 'mobile';

        } else {

            return 'pc';
        }

    }



    var logmsg = [],_dtype = deviceType();

    function log(str,type){

        var cfg = Jit.AM.getAppVersion();

        if(cfg.APP_DEBUG_PANEL){

            if(logmsg.length>=200){

                logmsg = logmsg.splice(1,logmsg.length-1);
            }

            logmsg.push('-> '+str);

            var logstr = '';

            for(var i=0;i<logmsg.length;i++){

                logstr += logmsg[i]+'<br>';
            }

            $('.jit-debug-panel').html(logstr);

        }else{

            console.log(str);
        }
    };


    function setCookie(name,value,expires,path){

        var expdate=new Date();

        expdate.setTime(expdate.getTime()+(expires*1000));

        document.cookie = name+"="+escape(value)
            + ";expires="+expdate.toGMTString()
            + ( path ? ";path=" + path : "" )
    }

    function getCookie(name){

        var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");

        if(arr=document.cookie.match(reg))

            return unescape(arr[2]);
        else
            return null;
    }

    function deleteCookie(name){

        var exp = new Date();

        exp.setTime(exp.getTime() - 1);

        var cval=this.GetCookie(name);

        if(cval!=null){

            document.cookie= name + "="+cval+";expires="+exp.toGMTString();
        }
    }

    var cookie = {

        set:function(key,val){

            if(!appManage.CUSTOMER_ID){
                alert('cookie 操作出错，需要customerId');
                return;
            }

            var appcookie = getCookie('jit_'+appManage.CUSTOMER_ID);

            try{

                if(appcookie){

                    appcookie = JSON.parse(appcookie);

                }else{

                    appcookie = {};
                }

            }catch(e){

                appcookie = {};
            }

            if(val!=null){

                appcookie[key] = val;

            }else{

                delete appcookie[key];
            }

            appcookie = JSON.stringify(appcookie);

            setCookie('jit_'+appManage.CUSTOMER_ID,appcookie,3600*24*7);
        },
        get:function(key){

            if(!appManage.CUSTOMER_ID){
                alert('cookie 操作出错，需要customerId');
                return;
            }

            var appcookie = getCookie('jit_'+appManage.CUSTOMER_ID);

            if(appcookie == '' || appcookie == null){

                return null;
            }

            appcookie = JSON.parse(appcookie);

            return (appcookie[key]==undefined?null:appcookie[key]);
        } ,
        del:function(key){

            if(!appManage.CUSTOMER_ID){
                alert('cookie 操作出错，需要customerId');
                return;
            }

            deleteCookie('jit_'+appManage.CUSTOMER_ID);


        }
    };


    ///HTML5 LocalStorage 本地存储获取修改和删除
    var store = function(){
        var locStorage = {

            set:function(key,val){

                if(key){

                    if(val){

                        localStorage.setItem(key,val);

                    }else{

                        localStorage.removeItem(key);
                    }

                }else{

                    log('set localStorage Error:key is null','error');
                }
            },

            get:function(key){

                return localStorage.getItem(key);
            }
        };
        var args = arguments;

        if(args.length == 1){

            return locStorage.get(args[0]);

        }else if(args.length == 2){

            locStorage.set(args[0],args[1]);
        }
    }

    /// Html5 SessionStorage 类似设置一个会话Cookie（即不设置过期时间，当关闭浏览器或是页面时，会话Cookie将被清除）
    var session = function(){
        var sesStorage = {

            set:function(key,val){

                if(key){

                    sessionStorage.setItem(key,val);

                }else{

                    log('set sessionStorage Error:key is null','error');
                }
            },

            get:function(key){

                return sessionStorage.getItem(key);
            }
        };
        var args = arguments;

        if(args.length == 1){

            return sesStorage.get(args[0]);

        }else if(args.length == 2){

            if(args[1] == null){

                sessionStorage.removeItem(args[0]);

            }else{

                sesStorage.set(args[0],args[1]);
            }
        }
    };


    var validator = {

        IsEmail : function(str){

            var reg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

            return reg.test(str)
        },

        isPhoneNumber : function (str){

            var regAee  = /^(13\d[1]{0,9}|14\d[1]{0,9}|15\d[1]{0,9}|18\d[1]{0,9})\d{8}$/;

            return regAee.test(str);
        }
    }

    var fn = {
        random : random,
        trim : trim,
        strToJson : strToJson,
        loadFiles : loadFiles,
        log : log,
        store : store,
        valid : validator,
        cookie : cookie,
        getPostion : getOffsetPos,
        session:session
    };


    /******************* #appManage#obj对象
     @@页面管模块
     appManage = {
			//设置应用的配置信息
			setAppVersion(配置信息)，

			//获取应用的配置信息
			getAppVersion(),

			//设置应用交互时ajax 携带的基本信息
			setBaseAjaxParam(json object),

			//获取应用交互时ajax 携带的基本信息
			getBaseAjaxParam(),

			//设置页面之间通信的数据信息
			setPageParam(key , value [object | string] ),

			//获取页面之间通信的数据信息
			getPageParam(key)
		}
     *******************/
    var appManage = {

        'APP_CODE':'',

        //获得url上的参数
        getUrlParam:function(key){

            var value = "",itemarr = [],
                urlstr = window.location.href.split("?");

            if (urlstr[1]) {

                var item = urlstr[1].split("&"),rst={};

                for (i = 0; i < item.length; i++) {

                    itemarr = item[i].split("=");

                    rst[itemarr[0]] = itemarr[1];

                }
            }else{

                return null;
            }

            if(key){

                return rst[key];

            }else{

                return rst;
            }
        },
        //随机生成一个唯一的ID
        buildUserId:function() {

            var guid = '';

            for (var i = 1; i <= 32; i++){

                var n = Math.floor(Math.random()*16.0).toString(16);

                guid += n;
            }

            return guid;
        },

        setAppVersion:function(cfg){
            //HTML5 LocalStorage 缓存一个Version配置
            var me = this;

            if(!me.CUSTOMER_ID){

                me.CUSTOMER_ID = me.getUrlParam('customerId');
            }

            if(!me.CUSTOMER_ID){

                log('Error: customer Id 丢失');

                return;
            }

            var ver = me.getAppVersion();

            if(ver){

                for(var key in cfg){

                    ver[key] = cfg[key];
                }

            }else{

                ver = cfg;
            }

            if(!me.APP_CODE){

                for(var key in ver){

                    if(key = 'APP_CODE'){

                        me.APP_CODE = cfg[key];

                        break;
                    }
                }
            }

            store(me.CUSTOMER_ID,JSON.stringify(ver));
        },
        //取出当前页面version;
        getAppVersion:function(){

            var me = this;

            if(!me.CUSTOMER_ID){

                me.CUSTOMER_ID = me.getUrlParam('customerId');
            }

            if(!me.CUSTOMER_ID){

                return null;
            }

            var rst = store(me.CUSTOMER_ID);

            if(rst){

                rst = eval('(' + rst + ')');

                return rst;

            }else{

                return null;
            }
        },

        /*
         setBaseAjaxParam设置ajax交互时的基本数据 (需要和config.js 中的AJAX_PARAMS 相匹配)

         @param : {
         'openId':'xxx',
         'userId':'xxx',
         'locale':'ch',
         'customerId':''
         }
         */
        setBaseAjaxParam:function(param,oncookie){

            var me = this , appcfg = me.getAppVersion();

            var ajaxKeys = appcfg['AJAX_PARAMS'].split(',');

            if(oncookie){

                for(var i in ajaxKeys){

                    param[ajaxKeys[i]] = param[ajaxKeys[i]]?param[ajaxKeys[i]]:null;
                }

                cookie.set('baseInfo',param);

            }else{

                for(var i in ajaxKeys){

                    store(this.APP_CODE+'_AJAX_PARAM_'+ajaxKeys[i] , param[ajaxKeys[i]]);
                }
            }
        },
        getBaseAjaxParam:function(){

            var me = this , appcfg = me.getAppVersion();

            var ajaxKeys = appcfg['AJAX_PARAMS'].split(',') , param = {};

            var baseInfo = cookie.get('baseInfo');

            if(baseInfo){

                return baseInfo;
            }

            for(var i in ajaxKeys){

                param[ajaxKeys[i]] = store(this.APP_CODE+'_AJAX_PARAM_'+ajaxKeys[i]);
            }

            return param;
        },
        //HTML5 LocalStorage 本地存储获取修改、添加
        setAppParam:function(type,key,val){

            var rkey = this.APP_CODE + '_' + type + '_' + key;

            if(val == null){

                store(rkey , null );

            }else if((typeof val) == 'object'){

                store(rkey , 'o_' + JSON.stringify(val) );

            }else{

                store(rkey , 's_' + val );
            }
        },

        getAppParam:function(type,key){

            var rkey = this.APP_CODE + '_' + type + '_' + key;

            var val = store(rkey);

            if(!val){

                return null;
            }

            var dtype = val.substr(0,1),

                rval = val.substring(2,val.length);

            if(dtype == 's'){

                return rval;

            }else if(dtype == 'o'){

                return eval('(' + rval + ')');
            }
        },
        //当前页面的  HTML5 Session    修改、添加
        setAppSession:function(type,key,val){

            var rkey = this.APP_CODE + '_' + type + '_' + key;

            if(val == null){

                session(rkey , null );

            }else if((typeof val) == 'object'){

                session(rkey , 'o_' + JSON.stringify(val) );

            }else{

                session(rkey , 's_' + val );
            }
        },

        getAppSession:function(type,key){

            var rkey = this.APP_CODE + '_' + type + '_' + key;

            var val = session(rkey);

            if(!val){

                return null;
            }

            var dtype = val.substr(0,1),

                rval = val.substring(2,val.length);

            if(dtype == 's'){

                return rval;

            }else if(dtype == 'o'){

                return eval('(' + rval + ')');
            }
        },

        //HTML5 LocalStorage type=PageParam 本地存储获取修改、添加
        setPageParam:function(key,val){

            this.setAppParam('PageParam',key,val);
        },
        getPageParam:function(key){

            return this.getAppParam('PageParam',key);
        },

        //当前页面的  HTML5 Session  type=PageParam  修改、添加
        setPageHashParam:function(key,val){

            this.setAppSession('PageParam',key,val);
        },
        getPageHashParam:function(key){

            return this.getAppSession('PageParam',key);
        },
        //将是否从新加载 config对应的   CUSTOMER_ID js文件
        checkAppPageConfig:function(_needReLoad){

            var cfg = this.getAppVersion();

            var pcfg = this.getAppPageConfig();

            if(!pcfg || _needReLoad){

                var isGloble = Jit.AM.getAppParam('isGloble');

                var _cfgname = (isGloble=='true'?'_globle':this.CUSTOMER_ID);

                var rst = $.ajax({
                    url: '../../../config/'+_cfgname+'.js',
                    async:false,
                    cache:false
                });

                var vcfg = Jit.strToJson(rst.responseText);

                Jit.AM.setAppPageConfig(vcfg);
            }
        },
        //HTML5 LocalStorage type=PageCfg 本地存储修改、添加
        setAppPageConfig:function(cfg){

            this.setAppParam('PageCfg','',cfg);
        },
        //HTML5 LocalStorage type=PageCfg 本地存储获取
        getAppPageConfig:function(){

            return this.getAppParam('PageCfg','');
        },
        //pageHistory 历史导航记录缓存 pageHistoryPush 增加
        pageHistoryPush:function(pagename){

            var history = this.getAppSession('PageHistory','');

            if(history){

                var list = history.split(',');

                if(list.length>=12){

                    list.splice(0,1);
                }

                list.push(pagename);

                this.setAppSession('PageHistory','',list.join(','));

            }else{

                this.setAppSession('PageHistory','',pagename);
            }
        },
        //pageHistory 历史导航记录缓存 pageHistorypop 删除
        pageHistoryPop:function(){

            var history = this.getAppSession('PageHistory','');

            if(history){

                var list = history.split(',');

                list.pop();

                this.setAppSession('PageHistory','',list.join(','));
            }
        },
        // pageHistory 历史导航记录缓存清空
        pageHistoryClear:function(){

            this.setAppSession('PageHistory','',null);
        },
        //pageHistory历史导航记录缓存
        hasHistory:function(){

            var history = this.getAppSession('PageHistory','');

            if(history){

                var list = history.split(',');

                if(list.length >= 2){

                    return true;
                }
            }

            return false;
        },
        //返回上一页
        pageBack:function(){

            var history = this.getAppSession('PageHistory','');

            if(history){

                var list = history.split(',');

                if(list.length >= 2){

                    log('返回上一页');

                    list.pop();

                    var tarpage = list.pop();

                    var pages = tarpage.split(':');

                    this.setAppSession('PageHistory','',list.join(','));

                    if(pages.length >1){

                        this.toPage(pages[0],pages[1]);

                    }else{

                        this.toPage(pages[0]);
                    }

                }
            }
        },
        /*
         跳转指定页面
         pagename   取出 config 中对应的customerId的  json对象中的页面key
         param 跳转页面要传递的参数
         */

        toPage:function(pagename,param){

            var pagecfg = this.getAppPageConfig(), //取出 config 中对应的customerId的js json对象

                page = pagecfg[pagename];// 获取页面中对应的详细参数

            var htmlpath = page.path.replace(/%(\S*)%/,function(str){

                return pagecfg['Config']['Shorthand'][str.substring(1,str.length-1)];
            });

            if(page){
                /*
                 if(param){

                 this.pageHistoryPush(pagename+':'+param);

                 }else{

                 this.pageHistoryPush(pagename);
                 }
                 */

                var cfg = Jit.AM.getAppVersion(),
                    version = (cfg.APP_CACHE?cfg.APP_VERSION:((new Date()).getTime()));

                location.href = JitCfg.baseUrl + 'html/'+htmlpath+'?customerId='+Jit.AM.CUSTOMER_ID+(param?('&'+param):'')+'&version='+version;
                console.log((param?('&'+param):'')+'&version='+version);
            }
        },
        //返回不走 NoAuthGoto认证的页面路径。pageName页面的key pageName是页面的可以
        getPageUrl:function(pageName,param){

            var url = location.host+'/WXOAuth/NoAuthGoto.aspx?'
                + 'customerId='+Jit.AM.CUSTOMER_ID
                + '&pageName='+pageName+'&'+param
                + 'Url=o2oapi.dev.aladingyidong.com/HtmlApps/html/_pageName_';
            return url;
        },
        //既不走  Auth认证也不走订阅号，
        toPageWithParam:function(pagename,param){

            var value = "",itemarr = [],params = {},
                urlstr = window.location.href.split("?");

            if (urlstr[1]) {

                var items = urlstr[1].split("&");

                for (i = 0; i < items.length; i++) {

                    itemarr = items[i].split("=");

                    params[itemarr[0]] = itemarr[1];
                }
            }
            if(param){

                var temps = param.split("&"),tempparam;

                for(var i in temps){

                    tempparam = temps[i].split('=');

                    params[tempparam[0]] = tempparam[1];
                }
            }

            delete params['customerId'];

            var paramslist = [];

            for(var key in params){

                paramslist.push(key + '=' + params[key]);
            }

            this.toPage(pagename,paramslist.join('&'));
        },
        //旧的ajax请求参数格式化方法
        buildAjaxParams:function(param){
            var _param = {
                type: "post",
                dataType: "json",
                url: "",
                data: null,
                beforeSend: function () {
                    //UI.Loading('SHOW');
                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown){
                    //UI.Loading("CLOSE");
                }
            };

            $.extend(_param,param);
            var baseInfo = this.getBaseAjaxParam();
            //通过浏览器地址栏把内容填充
            if((!baseInfo.customerId)&&this.getUrlParam("customerId")){
                baseInfo.customerId=this.getUrlParam("customerId");
            }
            if((!baseInfo.userId)&&this.getUrlParam("userId")){
                baseInfo.userId=this.getUrlParam("userId");
            }
            if((!baseInfo.openId)&&this.getUrlParam("openId")){
                baseInfo.openId=this.getUrlParam("openId");
            }
            if(!baseInfo.ChannelID){
                baseInfo.ChannelID="2";
            }

            var _data = {
                'action':param.data.action,
                'ReqContent':JSON.stringify({
                    'common':(param.data.common?$.extend(baseInfo,param.data.common):baseInfo),
                    'special':(param.data.special?param.data.special:param.data)
                })
            };

            _param.data = _data;

            return _param;
        },
        //新的ajax请求参数格式化方法
        buildNewAjaxParams:function(param){

            var _param = {
                type: "post",
                dataType: "json",
                url: "",
                data: null,
                beforeSend: function () {

                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown){

                }
            };

            $.extend(_param,param);
            var baseInfo = this.getBaseAjaxParam();
            //通过浏览器地址栏把内容填充
            if((!baseInfo.customerId)&&this.getUrlParam("customerId")){
                baseInfo.customerId=this.getUrlParam("customerId");
            }
            if((!baseInfo.userId)&&this.getUrlParam("userId")){
                baseInfo.userId=this.getUrlParam("userId");
            }
            if((!baseInfo.openId)&&this.getUrlParam("openId")){
                baseInfo.openId=this.getUrlParam("openId");
            }
            if(!baseInfo.ChannelID){
                baseInfo.ChannelID="2";
            }
            var action = param.data.action,
                interfaceType = param.interfaceType||'Product',
                _req = {
                    'Locale':baseInfo.locale,
                    'CustomerID':baseInfo.customerId,
                    'UserID':baseInfo.userId,
                    'OpenID':baseInfo.openId,
                    'ChannelID':baseInfo.ChannelID,
                    'Token':null,
                    'Parameters':param.data
                };

            delete param.data.action;

            var _data = {
                'req':JSON.stringify(_req)
            };

            _param.data = _data;

            _param.url = _param.url+'?type='+interfaceType+'&action='+action;

            return _param;
        },
        //ajax请求封装
        ajax:function(param){
            var action = param.data.action;

            var _param;
            if(param.url.indexOf('Gateway.ashx')!=-1 || param['interfaceMode'] == 'V2.0'){

                _param = this.buildNewAjaxParams(param);
            }
            else{

                _param = this.buildAjaxParams(param);
            }

            _param.url = JitCfg.ajaxUrl + _param.url;

            /**/


            /**/


            _param.beforeSend = function(){
                if(param.beforeSend){
                    param.beforeSend();
                }
                global.timer = new Date().getTime();
            };
            _param.complete = function(){
                if(param.complete){
                    param.complete();
                }

                console.log(
                    "\r\n"+
                        "页面名称："+Jit.AM.getAppPageConfig()[$("title").attr("name")].title+"|"+$("title").attr("name")
                        +"\r\n"+
                        "请求地址："+_param.url
                        +"\r\n"+
                        "请求方法："+action
                        +"\r\n"+
                        "请求耗时："+(new Date().getTime()- global.timer)+"毫秒"+"\r\n"
                );
            };


            $.ajax(_param);
        },

        //页面是否是存在version 中是否存在 LOG_PAGE
        isPageNeedLog:function(){

            var cfg = Jit.AM.getAppVersion(),
                htmlname = $('title').attr('name');

            if(htmlname && cfg['LOG_PAGE'] && cfg['LOG_PAGE'].indexOf(htmlname) != -1){

                return true;
            }

            return false;
        },
        logToServer:function(type){

            if(type == 'browser' || type == 'forward' || type == 'browserForward'){

                var _param = this.buildAjaxParams({
                    url: '/Module/BrowserRecord.ashx',
                    data: {
                        'action': type,
                        'webPage':$('title').attr('name')
                    },
                    success: function(){}
                });

                $.ajax(_param);
            }
        },
        openShareFunction:function(urlParams,onVisitCallBack){

            /*设置页面分享时的推荐链接*/

            var urls = [];

            urls.push(location.host+'/HtmlApps/Auth.html?pageName='+$('title').attr('name'));

            if(urlParams && urlParams.length>0){

                for(var i in urlParams){

                    var val = Jit.AM.getUrlParam(urlParams[i]);

                    if(val){

                        urls.push('&'+urlParams[i]+'='+val);
                    }
                }
            }


            urls.push('&customerId='+Jit.AM.CUSTOMER_ID);

            urls.push('&recommender=1&recommenderId='+Jit.AM.getBaseAjaxParam().userId);

            var url = urls.join('');

            Jit.WX.shareFriends("好友推荐",'',url,null);
            /*设置浏览分享页面时的行为逻辑*/

            if(Jit.AM.getUrlParam('recommender') == 1 && typeof onVisitCallBack == 'function'){

                onVisitCallBack(Jit.AM.getUrlParam('recommenderId'));
            }
        },
        //默认分享到微信的好友和朋友圈
        initShareEvent:function(shareInfo){
            Jit.WX.shareFriends(shareInfo);
            Jit.WX.shareTimeline(shareInfo);
        },

        //每个页面引用js必须运行的加载方法
        defindPage:function(page){

            window.scrollTo(0, 0);

            if(!page.initWithParam){

                page.initWithParam = function(){};
            }

            page.getBaseInfo = bind(this.getBaseAjaxParam,this);

            page.setParams = bind(this.setPageParam,this);
            //页面默认支持分享到微信和朋友圈
            page.initShareEvent=bind(this.initShareEvent,this);
            page.getParams = bind(this.getPageParam,this);

            page.setHashParam = bind(this.setPageHashParam,this);

            page.getHashParam = bind(this.getPageHashParam,this);

            page.getUrlParam = bind(this.getUrlParam,this);

            page.pageBack = bind(this.pageBack,this);

            page.toPage = bind(this.toPage,this);

            page.ajax = bind(this.ajax,this);

            page.buildAjaxParams = bind(this.buildAjaxParams,this);

            page.toPageWithParam = bind(this.toPageWithParam,this);

            page.openShareFunction = bind(this.openShareFunction,this);

            page.weiXinOptionMenu = bind(this.weiXinOptionMenu,this);

            page.weiXinToolBar = bind(this.weiXinToolBar,this);

            page._initShare = function(){

                var me = this;

                var param = this.pageParam;

                if(param && param['WX_TITLE']){

                    var shareInfo = {
                        'title':(param['WX_TITLE']||'好友推荐'),
                        'desc':(param['WX_DES']||'大奖等你抢！'),
                        'link':location.href,
                        'imgUrl':(param['WX_ICO'])
                    }

                    Jit.WX.shareFriends(shareInfo);
                }
            }

            Jit.AM.onLoad = function(){
                window._paq = window._paq || [];
                _paq.push(['trackPageView']);
                _paq.push(['enableLinkTracking']);
                (function() {
                    var u=(("https:" == document.location.protocol) ? "https" : "http") + "://dev.aladingyidong.com:999/";
                    _paq.push(['setTrackerUrl', u+'piwik.php']);
                    _paq.push(['setSiteId', 1]);
                    var d=document, g=d.createElement('script'), s=d.getElementsByTagName('script')[0]; g.type='text/javascript';
                    g.defer=true; g.async=true; g.src=u+'piwik.js'; s.parentNode.insertBefore(g,s);
                })();



                var pagecfg = Jit.AM.getAppPageConfig()[$('title').attr('name')];

                if(pagecfg && pagecfg['param']){
                    //沈马石添加
                    page.pageParam=pagecfg['param'];

                    page.initWithParam(pagecfg['param']);
                }

                page._initShare();

                //kevin  用来解决绑定事件的时候  是click还是tap事件
                page.eventType=Jit.deviceType()=="mobile"?"tap":"click";

                page.onPageLoad();
            }

            window.JitPage = page;
        },
        checkHasContact:function(){

            if(JitCfg.CheckOAuth == 'unAttention'){

                return false;
            }

            return true;
        },
        piwik:function(){

            $('body').append(JitCfg.statisticsCode);
        }
    }

    var WeiXin = {

        shareInfo:{},
        //显示或者隐藏网页右上角的按钮
        OptionMenu:function(flag){

            if(typeof WeixinJSBridge == 'object'){

                WeixinJSBridge.call(flag?'showOptionMenu':'hideOptionMenu');

            }else{

                document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {

                    WeixinJSBridge.call(flag?'showOptionMenu':'hideOptionMenu');

                });
            }
        },
        //显示或者隐藏 微信底部工具栏
        ToolBar:function(flag){

            if(typeof WeixinJSBridge == 'object'){

                WeixinJSBridge.call(flag?'showToolbar':'hideToolbar');

            }else{

                document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {

                    WeixinJSBridge.call(flag?'showToolbar':'hideToolbar');

                });
            }
        },

        fnShare : function(o){

            var map = {

                'Friends':{
                    'mKey':'appmessage',
                    'invoke':'sendAppMessage'
                }
            };

            function share(title,desc,link,imgUrl,isAuth){
                var customerId=Jit.AM.getUrlParam("customerId");
                if(!!!customerId){
                    customerId=Jit.AM.getBaseAjaxParam().customerId;
                }
                //是否需要高级auth认证
                if(isAuth){

                    link= location.host+'/WXOAuth/AuthUniversal.aspx?scope=snsapi_userinfo&customerId='+customerId+'&goUrl='+encodeURIComponent(link);
                }
                if(typeof WeixinJSBridge == 'object'){
                    WeixinJSBridge.on('menu:share:appmessage', function(argv){

                        WeixinJSBridge.invoke('sendAppMessage',{
                            //"appid":appId,
                            "img_url":imgUrl||('http://'+location.host+'/HtmlApps/images/common/jitico.jpg'),
                            //"img_width":"320",
                            //"img_height":"320",
                            "link":link,
                            "desc":desc,
                            "title":title
                        }, function(res) {
                            //分享成功
                            if(res.err_msg.indexOf('ok')!=-1||res.err_msg.indexOf('confirm')!=-1){
                                //转发给好友统计
                                if(window._paq){
                                    var baseInfo = appManage.getBaseAjaxParam();
                                    var title =document.title?document.title+'---转发到好友':'转发到好友';

                                    _paq.push(['trackEvent', title,baseInfo.customerId]);
                                }
                            }

                            if(res.err_msg.indexOf('ok')!=-1 && Jit.AM.isPageNeedLog()){

                                Jit.AM.logToServer('forward');
                            }
                        })
                    });

                    WeixinJSBridge.on('menu:share:timeline', function(argv){

                        WeixinJSBridge.invoke('shareTimeline',{
                            //"appid":appId,
                            "img_url":imgUrl||('http://'+location.host+'/HtmlApps/images/common/jitico.jpg'),
                            //"img_width":"320",
                            //"img_height":"320",
                            "link":link,
                            "desc":desc,
                            "title":title
                        }, function(res) {
                            //分享成功
                            if(res.err_msg.indexOf('ok')!=-1||res.err_msg.indexOf('confirm')!=-1){
                                //转发到朋友圈统计
                                if(window._paq){
                                    var baseInfo = appManage.getBaseAjaxParam();
                                    var title =document.title?document.title+'---转发到朋友圈':'转发到朋友圈';
                                    _paq.push(['trackEvent', title,baseInfo.customerId]);
                                }
                            }
                            if(res.err_msg.indexOf('ok')!=-1 && Jit.AM.isPageNeedLog()){

                                Jit.AM.logToServer('forward');
                            }
                        })
                    });

                    return true;

                }else{

                    return false;
                }
            }

            var runCount = 20;

            function dofn(o,count){

                return (function(){

                    if(share(o.title,o.desc,o.link,o.imgUrl,o.isAuth) || count<=0){

                        clearInterval(window.WX_Share_timer);

                        window.WX_Share_timer = null;
                    }

                    count--;
                });

            };

            var hashdo = dofn(o,20);

            if(window.WX_Share_timer){

                clearInterval(window.WX_Share_timer);
            }

            window.WX_Share_timer = setInterval(hashdo,300);
        },

        initShare : function(){

            var me = this,
                version = Jit.AM.getAppVersion();

            if(version['APP_WX_TITLE']){

                me.fnShare({
                    'link':location.href,
                    'title':version['APP_WX_TITLE'],
                    'desc':version['APP_WX_DES'],
                    'imgUrl':version['APP_WX_ICO'],
                    'isAuth':false  //默认不需要高级auth认证
                });
            }
        },
        //分享到朋友圈
        shareTimeline : function(title,desc,link,imgUrl,isAuth){

            var me = this;

            if(arguments.length==4){

                me.fnShare({
                    'title':title,
                    'desc':desc,
                    'link':link,
                    'imgUrl':imgUrl,
                    'isAuth':isAuth?isAuth:false    //是否需要高级auth认证  默认false
                });

            }else if(typeof arguments[0] == 'object'){

                me.fnShare(arguments[0]);
            }


        },

        //发送给好友
        shareFriends : function(title,desc,link,imgUrl,isAuth){

            var me = this;
            if(arguments.length==4){

                me.fnShare({
                    'title':title,
                    'desc':desc,
                    'link':link,
                    'imgUrl':imgUrl,
                    'isAuth':isAuth?isAuth:false    //是否需要高级auth认证  默认false
                });

            }else if(typeof arguments[0] == 'object'){

                me.fnShare(arguments[0]);
            }

        },
        //添加关注
        addContact : function(name,callback){

            if(typeof WeixinJSBridge == 'object'){

                WeixinJSBridge.invoke('addContact', {webtype: '1',username: name}, function(e) {

                    WeixinJSBridge.log(e.err_msg);
                    //e.err_msg:add_contact:added 已经添加
                    //e.err_msg:add_contact:cancel 取消添加
                    //e.err_msg:add_contact:ok 添加成功
                    if(e.err_msg == 'add_contact:added' || e.err_msg == 'add_contact:ok'){
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
        imagePreview:function (curSrc, srcList) {
            if (!curSrc || !srcList || srcList.length == 0) {
                return;
            }
            if(typeof WeixinJSBridge == 'object'){
                WeixinJSBridge.invoke('imagePreview', {
                    'current' : curSrc,
                    'urls' : srcList
                });
            }

        }
    }

    var UI = {
        //临时代码
        Dialog:function(cfg){

            if(cfg == 'CLOSE'){

                var panel = $('.jit-ui-panel');

                if(panel){

                    (panel.parent()).remove();
                }

            }else{

                cfg.LabelOk = cfg.LabelOk?cfg.LabelOk:'确定';

                cfg.LabelCancel = cfg.LabelOk?cfg.LabelCancel:'取消';

                var panel,btnstr;

                if(cfg.type == 'Alert' || cfg.type == 'Confirm'){

                    btnstr = (cfg.type == 'Alert')?'<a id="jit_btn_ok" style="margin:0 auto">'+cfg.LabelOk+'</a>':'<a id="jit_btn_cancel">'+cfg.LabelCancel+'</a><a id="jit_btn_ok">'+cfg.LabelOk+'</a>';

                    panel = $('<div id="dialog_div"><div class="jit-ui-panel"></div><div name="jitdialog" style="margin-top:120px" class="popup br-5">'
                        + '<p class="ac f14 white" id="dialog__content">'+cfg.content+'</p><div class="popup_btn">'
                        + btnstr + '</div></div></div>');

                }else if(cfg.type == 'Dialog'){
                    if(cfg.isAppend){  //追加内容
                        if($("#dialog__content").length){
                            $("#dialog__content").append("<br/>"+cfg.content);
                        }else{
                            panel = $('<div id="dialog_div"><div class="jit-ui-panel"></div><div style="margin-top:120px" class="popup br-5"><p class="ac f14 white" id="dialog__content">'+cfg.content+'</p></div></div>');
                        }
                    }else{
                        panel = $('<div id="dialog_div"><div class="jit-ui-panel"></div><div style="margin-top:120px" class="popup br-5"><p class="ac f14 white" id="dialog__content">'+cfg.content+'</p></div></div>');
                    }
                    if(cfg.times){
                        setTimeout(function(){
                            $("#dialog_div").hide();
                        },cfg.times);
                    }
                }

                if(panel){
                    panel.css({
                        'position':'fixed',
                        'left':'0',
                        'right':'0',
                        'top':'0',
                        'bottom':'0',
                        'z-index':'99999'
                    });
                    if($("#dialog_div").length){
                        $("#dialog_div").remove();
                    }
                    panel.appendTo($('body'));
                    (function(panel,cfg){

                        setTimeout(function(){

                            if(cfg.CallBackOk){

                                $(panel.find('#jit_btn_ok')).bind('click',cfg.CallBackOk);
                            }
                            if(cfg.CallBackCancel){

                                $(panel.find('#jit_btn_cancel')).bind('click',cfg.CallBackCancel);

                            }else{

                                $(panel.find('#jit_btn_cancel')).bind('click',function(){Jit.UI.Dialog('CLOSE');});
                            }
                        },16);

                    })(panel,cfg);
                }

                /*
                 var dialogdom =$('[name=jitdialog]');
                 dialogdom.css({
                 'left':(Jit.winSize.width-dialogdom.width())/2,
                 'top':(Jit.winSize.height-dialogdom.height())/2,
                 });
                 */


            }

        },
        Masklayer:{

            show:function(){

                if($('#masklayer').length<=0){

                    var mask = $('<div id="masklayer" style="position:fixed;background-color:#ECECEC;width:100%;height:100%;line-height:100%;z-index:9999;top:0;left:0;text-align:center"><img src="../../../images/common/loading.gif" style="margin:30px auto;" alt="" /></div>');

                    mask.appendTo('body');
                }

                $('#masklayer').css({'opacity':'0.6'}).show();
            },
            hide:function(){

                $('#masklayer').hide();
            }
        },
        Loading:function(display,msg){

            if(display||arguments.length==0){

                msg = msg || '正在加载...';

                var _html = '<div id="wxloading" class="wx_loading">'
                    + 	'<div class="wx_loading_inner">'
                    + 		'<i class="wx_loading_icon"></i>'
                    + 		'<span>'+ msg +'</span>'
                    +		'</div>'
                    +	'</div>'

                $('body').append(_html);

            }else{

                $('#wxloading').remove();
            }
        },
        AjaxTips:{
            //显示ajax加载数据的时候   出现加载图标
            Loading:function(flag){

                if(flag||arguments.length==0){
                    //显示loading

                    UI.Loading(true);

                }else{
                    //隐藏loading

                    UI.Loading(false);
                }
            },
            //加载数据
            Tips:function(options){
                var left="50%",
                    top="50%";
                if(options.left){
                    left=options.left;
                }
                if(options.top){
                    top=options.top;
                }
                if(options.show){//显示tips
                    if($("#ajax__tips").length>0){
                        $("#ajax__tips").remove();
                    }
                    var style="position:fixed;top:"+top+";  left:"+left+";  width:100px;  height:100px;margin-top:-50px;margin-left:-50px;text-align: center;line-height100px;";
                    var $div=$("<div id='ajax__tips' style='"+style+"'>"+(options.tips?options.tips:"暂无数据")+"</div>");
                    $("body").append($div);

                }else{  //隐藏tips
                    $("#ajax__tips").hide();
                }
            }
        },
        Image:{

            getSize:function(src,size){

                if(!src){

                    return '/HtmlApps/images/common/misspic.png';
                }

                var _src = src.replace(/(.png)|(.jpg)/,function(s){

                    return '_'+size+s;
                });

                return _src;
            }
        },
        showPicture:function(className){
            if (!className || className.length == 0	|| $("img."+className).length ==0) {
                return;
            }
            var imgList = [];

            for(var i=0,idata;i<$("img."+className).length;i++){
                idata = $("img."+className)[i];
                if(idata.src.length){
                    imgList.push(idata.src);
                }
            }
            $("body").delegate("img."+className,JitPage.eventType,function(){
                WeiXin.imagePreview(this.src,imgList);
            });

        },
        'Nav':{
            init:function(){

                var items = $('#topNav li');

                var cfg = Jit.AM.getAppPageConfig();

                var navcfg = null;

                if(cfg.Config && cfg.Config.Navigation){

                    navcfg = cfg.Config.Navigation;
                }

                if(!navcfg){

                    return;
                }

                if(items.length != navcfg.count){

                    var htmls = '';

                    for(var i=0;i<navcfg.count;i++){

                        htmls += "<li><a href=\""+(navcfg.href[i]||'')+"\"></a></li>";
                    }

                    $('#topNav ul').html(htmls);

                }else{

                    for(var i in navcfg.href){

                        $(items.eq(i).find('a')).attr('href',(navcfg.href[i]||''));
                    }
                }
            },
            setItemHref:function(idx,href){

                var items = $('#topNav li');

                $(items.get(idx)).attr('href',href);
            },
            showItemTips:function(idx,msg){

                var item = $('#topNav a').eq(idx);

                item.html('<span style="display: inline;">'+msg+'</span>');
            },
            displayItem:function(idx,display){

                if(display){

                    $('#topNav li').eq(idx).show();

                }else{

                    $('#topNav li').eq(idx).hide();
                }
            }
        }

    }





    global.Jit = (function(){
        var _jit = new Function();
        _jit.prototype = fn;
        _jit = new _jit();
        _jit.fn = fn;
        _jit.appManage = appManage;
        _jit.AM = appManage;
        _jit.WX = WeiXin;
        _jit.UI = UI;
        _jit.deviceType = deviceType;
        return _jit;
    })();


})(window,undefined);



(function(){
    /***************************      添加加减乘除操作，解决js浮点运算的bug  ---begin    ***************************/
        //除法函数，用来得到精确的除法结果
        //说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。
        //调用：accDiv(arg1,arg2)
        //返回值：arg1除以arg2的精确结果
    function accDiv(arg1, arg2) {
        var t1 = 0, t2 = 0, r1, r2;
        try {
            t1 = arg1.toString().split(".")[1].length;
        } catch(e) {
        }
        try {
            t2 = arg2.toString().split(".")[1].length;
        } catch(e) {
        }
        with (Math) {
            r1 = Number(arg1.toString().replace(".", ""));
            r2 = Number(arg2.toString().replace(".", ""));
            return (r1 / r2) * pow(10, t2 - t1);
        }
    }


    //乘法函数，用来得到精确的乘法结果
    //说明：javascript的乘法结果会有误差，在两个浮点数相乘的时候会比较明显。这个函数返回较为精确的乘法结果。
    //调用：accMul(arg1,arg2)
    //返回值：arg1乘以arg2的精确结果
    function accMul(arg1, arg2) {
        var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
        try {
            m += s1.split(".")[1].length;
        } catch(e) {
        }
        try {
            m += s2.split(".")[1].length;
        } catch(e) {
        }
        return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m);
    }


    //加法函数，用来得到精确的加法结果
    //说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
    //调用：accAdd(arg1,arg2)
    //返回值：arg1加上arg2的精确结果
    function accAdd(arg1, arg2) {
        var r1, r2, m;
        try {
            r1 = arg1.toString().split(".")[1].length;
        } catch(e) {
            r1 = 0;
        }
        try {
            r2 = arg2.toString().split(".")[1].length;
        } catch(e) {
            r2 = 0;
        }
        m = Math.pow(10, Math.max(r1, r2));
        return (arg1 * m + arg2 * m) / m;
    }


    //减法函数，用来得到精确的减法结果
    //说明：javascript的减法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的减法结果。
    //调用：accSubtr(arg1,arg2)
    //返回值：arg1减去arg2的精确结果
    function accSubtr(arg1, arg2) {
        var r1, r2, m, n;
        try {
            r1 = arg1.toString().split(".")[1].length;
        } catch(e) {
            r1 = 0;
        }
        try {
            r2 = arg2.toString().split(".")[1].length;
        } catch(e) {
            r2 = 0;
        }
        m = Math.pow(10, Math.max(r1, r2));
        //动态控制精度长度
        n = (r1 >= r2) ? r1 : r2;
        return ((arg1 * m - arg2 * m) / m).toFixed(n);
    }

    /*
     给基本类型添加原型方法添加不上。

     //给Number类型增加一个div方法，调用起来更加方便。
     Number.prototype.div = Number.prototype.divided = function(arg) {
     return accDiv(this, arg);
     };
     //给Number类型增加一个mul方法，调用起来更加方便。
     Number.prototype.mul =Number.prototype.multiplied = function(arg) {
     return accMul(arg, this);
     };
     //给Number类型增加一个add方法，调用起来更加方便。
     Number.prototype.add = function(arg) {
     return accAdd(arg, this);
     };
     //给Number类型增加一个subtr 方法，调用起来更加方便。
     Number.prototype.subtr = Number.prototype.subtract = function(arg) {
     return accSubtr(arg, this);
     };
     */
    Math.div = Math.divided = accDiv;
    Math.mul = Math.multiplied = accMul;
    Math.add = accAdd;
    Math.subtr = accSubtr;
    /***************************      添加加减乘除操作，解决js浮点运算的bug  ---end    ***************************/
})(window);

