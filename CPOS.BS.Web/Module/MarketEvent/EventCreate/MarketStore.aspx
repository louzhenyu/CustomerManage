<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" 
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>活动管理</title>
    
    <script src="/Framework/javascript/Biz/OrderNo.js" type="text/javascript"></script>

    <script src="Controller/MarketStoreCtl.js" type="text/javascript"></script>
    <script src="Model/EventVM.js" type="text/javascript"></script>
    <script src="Store/EventVMStore.js" type="text/javascript"></script>
    <script src="View/MarketStoreView.js" type="text/javascript"></script>
    
    <meta name="google" value="notranslate" />
    <style type="text/css" media="screen"> 
        /*html, body  { height:100%; }*/
        /*body { margin:0; padding:0; overflow:auto; text-align:center; 
                background-color: #ffffff; }*/   
        object:focus { outline:none; }
        #flashContent { display:none; }
        #mapArea {width:800px;height:580px; border:1px solid #000000;top:66px;left:360px;z-index:99999;position:absolute;}
        #mapAreaClose {position:absolute;top:66px;left:1160px;z-index:99999; border:1px solid #000000; width:32px; 
                       background:#fff; padding:2px;height:26px; padding-left:3px; cursor:pointer; border-left:0px;
        }
    </style>   
        <script type="text/javascript" src="swfobject.js"></script>
        <script type="text/javascript">
            function obj2Str(obj) {
                switch (typeof (obj)) {
                    case 'object':
                        var ret = [];
                        if (obj instanceof Array) {
                            for (var i = 0, len = obj.length; i < len; i++) {
                                ret.push(obj2Str(obj[i]));
                            }
                            return '[' + ret.join(',') + ']';
                        }
                        else if (obj instanceof RegExp) {
                            return obj.toString();
                        }
                        else {
                            for (var a in obj) {
                                ret.push('"' + a + '"' + ':' + obj2Str(obj[a]));
                            }
                            return '{' + ret.join(',') + '}';
                        }
                    case 'function':
                        return 'function() {}';
                    case 'number':
                        return obj.toString();
                    case 'string':
                        return "\"" + obj.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g, function (a) { return ("\n" == a) ? "\\n" : ("\r" == a) ? "\\r" : ("\t" == a) ? "\\t" : ""; }) + "\"";
                    case 'boolean':
                        return obj.toString();
                    default:
                        return obj.toString();
                }
            }
        </script>
        <script type="text/javascript">
            var swfVersionStr = "11.1.0";
            var xiSwfUrlStr = "playerProductInstall.swf";
            var flashvars = {};
            var params = {};
            params.quality = "high";
			params.wmode='opaque';
            params.bgcolor = "#ffffff";
            params.allowscriptaccess = "sameDomain";
            params.allowfullscreen = "true";
            var attributes = {};
            attributes.id = "index";
            attributes.name = "index";
            attributes.align = "middle";
            swfobject.embedSWF(
                "index.swf", "flashContent", 
                "100%", "100%", 
                swfVersionStr, xiSwfUrlStr, 
                flashvars, params, attributes);
            swfobject.createCSS("#flashContent", "display:block;text-align:left;");
        </script>
        <script type="text/javascript">
            //中心点动态语句输出
            var lng = 121.472178;//平均值
            var lat = 31.233030; //平均值
            //所有门店（请用动态语句输出storesAll）
            var storesAll = '[' +
                    '{"StoreID":"1","LabelID":"1","Lng":"121.502091","Lat":"31.237371","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"陆家嘴店：上海市浦东新区陆家嘴世纪大道","PopInfoWidth":"200","PopInfoHeight":"50","PopInfo":[{"type":"1","title":"陆家嘴店","value":"上海市浦东新区陆家嘴世纪大道"}]},' +
                    '{"StoreID":"2","LabelID":"2","Lng":"121.521231","Lat":"31.240802","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"联洋店：上海市浦东新区浦东大道563号","PopInfoWidth":"200","PopInfoHeight":"50","PopInfo":[{"type":"1","title":"联洋店","value":"上海市浦东新区浦东大道563号"}]},' +
                    '{"StoreID":"3","LabelID":"3","Lng":"121.474922","Lat":"31.234821","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"南京东路店：上海市黄浦区南京东路1000号","PopInfoWidth":"200","PopInfoHeight":"50","PopInfo":[{"type":"1","title":"南京东路店","value":"上海市黄浦区南京东路1000号"}]},' +
                    '{"StoreID":"4","LabelID":"4","Lng":"121.475934","Lat":"31.232784","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"国贸店：上海市黄浦区西藏中路290号","PopInfoWidth":"500","PopInfoHeight":"400","PopInfo":[{"type":"1","title":"国贸店","value":"上海市黄浦区西藏中路290号"}]}' +
                    ']';
            storesAll = fnLoadMapStores();
            //所有门店转JSON对象
            var arrStores = eval("(" + storesAll + ")");        
            //"已选门店"数组
            var arrSelect = [];
            //打印已选门店
            function printStores() {
                var obj = document.getElementById("selectStores");
                obj.value = "";
                for (var i = 0; i < arrSelect.length; i++) {
                    //if (obj.value == "") obj.value = arrSelect[i];
                    //else obj.value += "," + arrSelect[i];
                    obj.value += "," + arrSelect[i];
                }
            }
            //加入"已选门店"数组
            function appendStores(storeId) {
                var flag = true;
                for (var i = 0; i < arrSelect.length; i++) {
                    if (arrSelect[i] == storeId) {
                        flag = false;
                    }
                }
                if (flag)
                    arrSelect.push(storeId);
            }
            //清空已选门店
            function removeStores(storeId) {
                for (var i = 0; i < arrSelect.length; i++)
                    arrSelect.splice(i, 1);
            }

            //地图初始事件
            function _map_InitMap() {
                _map_AddStores(); 
            }
            //地图加点事件            
             function _map_AddStores() {
                //var lng = 121; 
                //var lat = 31;
                 var ou = "";
                 var i = new Number(0);

                 for (i = 0; i < 1000; i++) {
                     ou += pp(i,
		                parseFloat(lng) + parseFloat(i / 10000) * Math.random(),
		                parseFloat(lat) + parseFloat(i / 10000) * Math.random()) + ",";
                 }

                 index._map_AddStores(storesAll, false);
                 index._map_MoveToStores();
             }
             //地图中心点
             function pp(id, lng, lat) {
                 return '{"StoreID": "' + id + '", "LabelID": "' + id + '", "Lng": "' + lng + '", "Lat": "' + lat + '", "Icon": "gs.png"}';
             }
             //地图点多边形选择事件
             function _map_Extent_Select(a) {
                 if (a.length > 0) {
                     arrSelect = []; //初始化已选门店
                     arrStores = eval("(" + storesAll + ")"); //初始化门店点
                     //数组循环
                     for (var i = 0; i < a.length; i++)
                         appendStores(a[i].StoreID);

                     printStores();

                     for (var j = 0; j < arrSelect.length; j++) {
                         for (var k = 0; k < arrStores.length; k++) {
                             if (arrStores[k].StoreID == arrSelect[j])
                                 arrStores[k].Icon = "os.png"; //替换图标
                         }
                     }
                     //原始点清除
                     index._map_RemoveStores("");
                     //点重新加入
                     index._map_AddStores(obj2Str(arrStores), false);
                 }
             }

             function _map_Graphic_OnDoubleClick(ob) {
                 //alert("双击")
              }
             function _map_Graphic_OnClick(ob) {
                 //alert("单击")
                 //appendStores(ob.StoreID);
                 //printStores();
              }
             //点移动事件
             function _map_Graphic_MoveEdit(ob) {
                 //alert("移动")
              }
             function _map_Graphic_OnRightClick(object, type) {
                 //alert("右键选择")
              }
             
             //导出事件
             function _map_ExportMap(b) {
                 alert(b.length)
             }
             function _map_Extent_Change(b)
             { }
            //聚焦到中心位置
             function _map_MoveToStores() {
                 index._map_MoveToStores();
             }
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section z_event">
     <div class="art-tit" style="background:#F6F6F6">
        <div class="z_event_step z_event_border" style="width:830px; border:0;">
            <div class="z_event_step_item pointer" onclick="fnGoto1()">
                <div class="z_event_step_item_icon z_event_step1"></div>
                <div class="z_event_step_item_text ">定义</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnPre()">
                <div class="z_event_step_item_icon z_event_step2"></div>
                <div class="z_event_step_item_text ">时间</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto3()">
                <div class="z_event_step_item_icon z_event_step3_h"></div>
                <div class="z_event_step_item_text ">门店</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnSave()">
                <div class="z_event_step_item_icon z_event_step4"></div>
                <div class="z_event_step_item_text ">人群</div>
            </div>
            <div class="z_event_next"></div>
            <div class="z_event_step_item pointer" onclick="fnGoto5()">
                <div class="z_event_step_item_icon z_event_step5"></div>
                <div class="z_event_step_item_text ">邀约</div>
            </div>
        </div>
    </div>
        <%--<div class="z_event_border" style="font-weight:bold; height:36px; line-height:36px; 
            padding-left:10px; border-top:0px;">定义11</div>--%>
        <div class="z_event_border" style="padding:0px;  padding-bottom:0px; height:460px;">
            
            <div style="width:100%; padding-left:0px; padding-right:0px;">
                <div style="width:100%;">
                    <div style="float:left; width:30%; background:;">
                        <div style="float:left; height:30px; width:100%; background:#f0f0f0; padding:4px;">1. 地图方式</div>
                        <div style="clear:both;padding:10px;">
                            <div style="float:left; margin-bottom:13px;">门店：</div>
                            <div id="btnSelectStore" style="float:left;"></div>
                        </div>
                        <div style="float:left; height:30px; width:100%; background:#f0f0f0; padding:4px;">2. 文件方式</div>
                        <div style="padding:10px;">
                            <div style="line-height:60px; height:60px;clear:both;">
                                文件模板：<a href="#"><font color="blue">下载</font></a>
                            </div>
                            <div style="height:100px;">
                                文件上传：
                                <div id="fileUpload" style="margin-top:10px;"></div>
                                <div id="btnUpload" style="margin-top:10px;"></div>
                            </div>
                        </div>
                    </div>
                    <div style="float:right; width:70%;">
                        <div id="gridStore"></div>
                    </div>
                </div>
            </div>

        </div>
        <div class="z_event_border" style="font-weight:bold; height:46px; line-height:46px; 
            padding-left:10px; border-top:0px; clear:both; background:rgb(241, 242, 245);">
            <div id="btnNext" style="float:right; margin-top:10px; margin-right:10px;"></div>
            <div id="btnPre" style="float:right; margin-top:10px;"></div>
            <div id="btnReset" style="float:right; margin-top:10px;"></div>
        </div>
    </div>

    <input id="selectStores" type="hidden" value="" />
    <div id="mapAreaClose" style="display:none;" onclick="fnMapAreaClose()">关闭</div>
     <div id="mapArea" style="display:none;">
        <div id="flashContent">
            <p>
                To view this page ensure that Adobe Flash Player version 
                11.1.0 or greater is installed. 
            </p>
            <script type="text/javascript"> 
                var pageHost = ((document.location.protocol == "https:") ? "https://" : "http://"); 
                document.write("<a href='http://www.adobe.com/go/getflashplayer'><img src='" 
                                + pageHost + "www.adobe.com/images/shared/download_buttons/get_flash_player.gif' alt='Get Adobe Flash player' /></a>" ); 
            </script> 
        </div>
        
        <noscript>
            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%" height="100%" id="index">
                <param name="movie" value="index.swf" />
                <param name="quality" value="high" />
                <param name="bgcolor" value="#ffffff" />
                <param name="allowScriptAccess" value="sameDomain" />
                <param name="allowFullScreen" value="true" />
                <!--[if !IE]>-->
                <object type="application/x-shockwave-flash" data="index.swf" width="100%" height="100%">
                    <param name="quality" value="high" />
                    <param name="bgcolor" value="#ffffff" />
                    <param name="allowScriptAccess" value="sameDomain" />
                    <param name="allowFullScreen" value="true" />
                <!--<![endif]-->
                <!--[if gte IE 6]>-->
                    <p> 
                        Either scripts and active content are not permitted to run or Adobe Flash Player version
                        11.1.0 or greater is not installed.
                    </p>
                <!--<![endif]-->
                    <a href="http://www.adobe.com/go/getflashplayer">
                        <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash Player" />
                    </a>
                <!--[if !IE]>-->
                </object>
                <!--<![endif]-->
            </object>
        </noscript>     

        </div>
</asp:Content>
