<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">  
    <script src="/Framework/javascript/Biz/City.js" type="text/javascript"></script>
    <script src="Controller/MapCtl.js" type="text/javascript"></script>
    <script src="Model/MapVM.js" type="text/javascript"></script>
    <script src="Store/MapVMStore.js" type="text/javascript"></script>
    <script src="View/MapView.js" type="text/javascript"></script>
    <script language="javascript" src="http://webapi.amap.com/maps?v=1.2&key=4832ebc5e5c9801db987e11aebb1fdc7"></script>
    <script language="javascript">
        var mapObj;
        var marker = new Array();
        var windowsArr = new Array();
        //基本地图加载
        function mapInit() {

            mapObj = new AMap.Map("iCenter", {
                level: 13,
                center: new AMap.LngLat(121.44117, 31.22913)
            });
        }

        $(function() {
            mapInit();
        });
        function placeSearch() {
            if (document.getElementById("spSearchInputId").value == "") {
                return;
            }
            var MSearch;
            $(".getSearchTxtList").show();
            $("#result").html('<center>数据加载中</center>');
            mapObj.plugin(["AMap.PlaceSearch"], function () {
                MSearch = new AMap.PlaceSearch({ //构造地点查询类
                    city: "",
                    pageSize: 30
                });
                AMap.event.addListener(MSearch, "complete", keywordSearch_CallBack); //返回地点查询结果
                MSearch.search(document.getElementById("spSearchInputId").value); //关键字查询
            });
        }
        //添加marker&infowindow    
        function addmarker(i, d) {
            /* var lngX = d.location.getLng();
            var latY = d.location.getLat();
            var markerOption = {
            map: mapObj,
            icon: "http://api.amap.com/webapi/static/Images/" + (i + 1) + ".png",
            position: new AMap.LngLat(lngX, latY)
            };
            var mar = new AMap.Marker(markerOption);
            marker.push(new AMap.LngLat(lngX, latY));

            var infoWindow = new AMap.InfoWindow({
            content: "<h3><font color=\"#00a6ac\">&nbsp;&nbsp;" + (i + 1) + ". " + d.name + "</font></h3>" + TipContents(d.type, d.address, d.tel),
            size: new AMap.Size(300, 0),
            autoMove: true,
            offset: new AMap.Pixel(0, -30)
            });
            windowsArr.push(infoWindow);
            var aa = function (e) { infoWindow.open(mapObj, mar.getPosition()); };
            AMap.event.addListener(mar, "click", aa);*/
        }
        //回调函数
        function keywordSearch_CallBack(data) {
            var resultStr = "";
            var poiArr = data.poiList.pois;
            var resultCount = poiArr.length;
            if (resultCount == 0) {
                document.getElementById("result").innerHTML = "<center>搜索无结果</center>";
                return;
            }
            for (var i = 0; i < resultCount; i++) {
                var lngX = poiArr[i].location.getLng();
                var latY = poiArr[i].location.getLat();
                //   resultStr += "<div id='divid" + (i + 1) + "' onmouseover='openMarkerTipById1(" + i + ",this)' onmouseout='onmouseout_MarkerStyle(" + (i + 1) + ",this)' style=\"font-size: 12px;cursor:pointer;padding:0px 0 4px 2px; border-bottom:1px solid #C1FFC1;\"><table><tr><td><img src=\"http://api.amap.com/webapi/static/Images/" + (i + 1) + ".png\"></td>" + "<td><h3><font color=\"#00a6ac\">名称: " + poiArr[i].name + "</font></h3>";

                //  resultStr += TipContents(poiArr[i].type, poiArr[i].address, poiArr[i].tel) + "</td></tr></table></div>";
                if (i == 0) {
                    resultStr += "<li class='getSearchTxtListHoverLi' onclick='goMapProint(this," + lngX + "," + latY + ")'><img src='/Framework/Image/images/png_green.png' style='margin-right:10px; float:left;' />" + poiArr[i].name + "<br>地址：<span style='color:#666'>" + poiArr[i].address + "</span></li>";
                } else {
                    resultStr += "<li onclick='goMapProint(this," + lngX + "," + latY + ")'><img src='/Framework/Image/images/png_green.png' style='margin-right:10px; float:left; display:none;' />" + poiArr[i].name + "<br>地址：<span style='color:#666'>" + poiArr[i].address + "</span></li>";
                }

                //   addmarker(i, poiArr[i]);
            }
            //  mapObj.setFitView();
            document.getElementById("result").innerHTML = resultStr;
        }

        function goMapProint(o, lng, lat) {
            var px = '[{"StoreID":"9999","LabelID":"","Menu":"0","LabelContent":"","Lng":"' + lng + '","Lat":"' + lat + '","Icon":"g.png","IsAssigned":"false","IsEdit":"true","Tips":""}]';
            var a = window.frames["MapFlash"];
            a.index._map_RemoveStores('[{"StoreID":"9999"}]');
            a.index._map_AddStores(px, true);
            a.index._map_MoveToStoreByScale('9999', 13);
            //alert(lng + ".." + lat);
            $(o).find("img").show();
            $(o).addClass("getSearchTxtListHoverLi").siblings().removeClass("getSearchTxtListHoverLi");
            $(o).siblings().find("img").hide();
        }
        function _map_Graphic_OnClick(ob) {
            map = window.frames["MapFlash"].index;
            map._map_ClearLines("");
            //alert(ob.StoreID);
            //var d = Ext.decode(mapData2);
            for (var i = 0; i < mapData2.data.length; i++) {
                if (ob.StoreID == mapData2.data[i].StoreID) {
                    if (mapData2.data[i].SendUserList != undefined && mapData2.data[i].SendUserList != null) {
                        var userList = mapData2.data[i].SendUserList.split(',');
                        for (var j = 0; j < userList.length; j++) {
                            if (userList[j].length > 0) {
                                var p = '[{"StoreID":"'+ mapData2.data[i].StoreID +'"},{"StoreID":"' + userList[j] + '"}]';
                                map._map_CreateLine(newGuid(),p,"line",true,false,"1","end","3");
                            }
                        }
                    }
                }
            }
        }
    </script>
    <style type="text/css">
        .spSearch
        {
            position: absolute;
            right: 0;
            top: 0;
            width: 140px;
            box-shadow: 0 0 3px gray;
        }
        .spSearchDiv
        {
            background-color: #404853;
            height: 40px;
            position: relative;
            cursor: pointer;
        }
        .spSearchTitle
        {
            width: 117px;
            display: block;
            height: 32px;
            background: url(/Framework/Image/images/skin0909.png) no-repeat left top;
            position: absolute;
            top: 5px;
            left: 5px;
        }
        .spSearchJt, .spSearchJt2
        {
            width: 22px;
            display: block;
            height: 22px;
            background: url(/Framework/Image/images/skin0909.png) no-repeat -163px -5px;
            position: absolute;
            top: 10px;
            right: 5px;
        }
        .spSearchCt
        {
            width: 22px;
            display: block;
            height: 22px;
            background: url(/Framework/Image/images/skin0909.png) no-repeat -134px -5px;
            position: absolute;
            top: 10px;
            right: 5px;
            cursor: pointer;
        }
        .spSearchInputTxt
        {
            border: 1px solid #dbdcd6;
            padding: 10px;
            background-color: #fff;
            border-top: 0;
            clear: both;
            max-height: 580px;
            _height: 580px;
            overflow-y: auto;
            overflow: hidden;
        }
        .spSearchInputWarp
        {
            height: 36px;
            clear: both;
        }
        .spSearchInput
        {
            width: 215px;
            border: 1px solid #a0a0a0;
            height: 35px;
            position: relative;
            float: left;
            border-radius: 2px;
        }
        .spSearchInputStyle
        {
            width: 170px;
            height: 30px;
            padding: 3px 0 0 5px;
            font-size: 16px;
            border: 0;
            color: #333;
        }
        .closeSearchText
        {
            position: absolute;
            width: 25px;
            height: 25px;
            background: url(/Framework/Image/images/skin0909.png) no-repeat -283px 0;
            display: block;
            right: 4px;
            top: 3px;
            cursor: pointer;
        }
        .goSearchOO
        {
            border: 0;
            width: 54px;
            height: 36px;
            background: url(/Framework/Image/images/skin0909.png) no-repeat -213px -1px;
            float: left;
            margin-left: 8px;
            display: block;
            _display: inline;
            cursor: pointer;
        }
        .getSearchTxtList
        {
            border: 1px solid #cacaca;
            background-color: #f7f7f7;
            display: none;
        }
        .getSearchTxtList ul
        {
            padding: 3px;
        }
        
        .getSearchTxtList li
        {
            color: #000;
            font-size: 14px;
            line-height: 24px;
            padding: 6px 10px;
            border-bottom: 1px dashed #d2d2d2;
            cursor: pointer;
        }
        .getSearchTxtList li.getSearchTxtListHoverLi
        {
            background-color: #dadada;
        }
        span.spSearchJt2
        {
            -moz-transform: rotate(180deg);
            -moz-transform-origin: center 50%;
            -webkit-transform: rotate(180deg);
            -webkit-transform-origin: center 50%;
            -webkit-transition: -webkit-transform 0.2s ease-in;
            line-height: 26px;
            -moz-transition: -moz-transform 0.2s ease-in;
            background: url(/Framework/Image/images/skin0909.png) no-repeat -134px -5px\9;
        }
        span.spSearchJt
        {
            -moz-transform: rotate(0);
            -moz-transform-origin: center 50%;
            -webkit-transform: rotate(0);
            -webkit-transform-origin: center 50%;
            -webkit-transition: -webkit-transform 0.2s ease-in;
            line-height: 26px;
            -moz-transition: -moz-transform 0.2s ease-in;
        }
    </style>
    <script type="text/javascript">
        function KeyDownOO(o, e) {
            if ($(o).val() != "") {
                $(".closeSearchText").show();
            } else {
                $(".closeSearchText").hide();
            }
            e.stopPropagation();
            return false;
        }
        function ShowSearch(o) {
            var getrel = $(o).attr("rel");
            var getAnimate = $(o).attr("actionanimate");
            $(o).attr("actionanimate", "1");
            if (getAnimate == "0") {
                if (getrel == "0") {
                    $(o).parent().animate({ "width": 300 }, 800, function () {
                        $(o).attr("actionanimate", "0");

                        $("#spSearchInputTxt").slideDown();
                    });
                    $(o).attr("rel", "1");
                    $(o).find(".spSearchJt").removeClass("spSearchJt").addClass("spSearchJt2");
                } else {
                    $("#spSearchInputTxt").slideUp(800, function() { $(o).parent().animate({ "width": 140 }, 800, function() { $(o).attr("actionanimate", "0"); }); });

                    $(o).attr("rel", "0");
                    $(o).find(".spSearchJt2").removeClass("spSearchJt2").addClass("spSearchJt");
                }
            }

        }
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="m10 article">
            <div class="art-tit">
                <div class="view_Search" style="height:44px;">
                    <%--<span id='span_panel'></span>--%>
                    <div style="float:left; width:40px; margin:10px; font-weight:bold; text-align:center;">
                        <div id="txtType">任务</div>
                    </div>
                    <div id="btnSearch1" class="z_btn1" style="" onclick="fnSearchType(1)">待处理</div>
                    <div id="btnSearch2" class="z_btn2" style="" onclick="fnSearchType(2)">计划中</div>
                    <div id="btnSearch3" class="z_btn2" style="" onclick="fnSearchType(3)">已完成</div>

                    <div style="float:left; width:150px; margin:10px; font-weight:; text-align:center;">
                        <div id="txtAmount">总金额:</div>
                    </div>

                    <div style="float:right; width:70px; margin:10px; margin-right:20px;">
                        <div id="btnFullScreen" style=""></div>
                    </div>
                    <div style="float:right; width:60px; margin:10px; margin-top:10px;">
                        <div id="btnSearch" style=""></div>
                    </div>
                    <div style="float:right; width:180px; margin:10px; margin-top:12px;">
                        <div id="txtName" style=""></div>
                    </div>
                </div>
                <%--<div class="view_Search2">
                    <span id='span_panel2'></span>
                </div>--%>
                <a href="javascript:void(0)" onclick="openFullWall(this)" rel="1" id="hFullScreen"
                    style="display: none">全屏</a>
                <%--<div style="position: absolute;right: 0px; top: 120px;width: 500px" id="CCmapMenuDiv">
                    <div class="view_Button">
                        <span id='span_create'>图例：&nbsp;<img src="assets/images/jit/c3.png" />&nbsp;社区&nbsp;<img
                            src="assets/images/jit/s3.png" />&nbsp;校园&nbsp;<img src="assets/images/jit/o3.png" />&nbsp;办公楼&nbsp;<img
                                src="assets/images/jit/hs3.png" />&nbsp;发廊&nbsp;<img src="assets/images/jit/h3.png" />&nbsp;计免&nbsp;<img
                                    src="assets/images/jit/gw3.png" />&nbsp;西部计划&nbsp;</span>
                    </div>
                </div>--%>
            </div>
            <div class="DivGridView" id="DivGridView" style="border: 1px solid #A8B8C5; margin-right: 5px;
                padding: 5px; position: relative;">
                <%--<div class="spSearch">
                    <div class="spSearchDiv" onclick="ShowSearch(this)" rel="0" actionanimate="0">
                        <span class="spSearchTitle"></span><span class="spSearchJt"></span>
                    </div>
                    <div class="spSearchInputTxt" style="display: none;" id="spSearchInputTxt">
                        <span style="height: 5px; overflow: hidden; clear: both; display: block;"></span>
                        <div class="spSearchInputWarp">
                            <div class="spSearchInput">
                                <input type="text" class="spSearchInputStyle" id="spSearchInputId" value="" onkeydown="KeyDownOO(this,event); if(event.keyCode==13)placeSearch();" />
                                <em class="closeSearchText" style="display: none;" onclick="document.getElementById('spSearchInputId').value=''">
                                </em>
                            </div>
                            <input type="button" value="" class="goSearchOO" onclick="placeSearch()" />
                        </div>
                        <span style="height: 10px; overflow: hidden; clear: both; display: block;"></span>
                        <div class="getSearchTxtList" style="max-height: 510px; _height: 510px; overflow-y: auto;
                            overflow-x: hidden;">
                            <ul id="result">
                            </ul>
                        </div>
                    </div>
                </div>--%>
                <iframe src="index.html" id="MapFlash" width="100%" height="650px" marginheight="0"
                    scrolling="no" marginwidth="0" frameborder="0"></iframe>
            </div>
            <div class="cb">
            </div>
        </div>
    </div>
    <div id="iCenter" style="height: 60px; width: 60px; display: none;">
    </div>
</asp:Content>
