<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>新版首页管理</title>
        <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />

    <link rel="stylesheet" href="css/detail.css" />

     <script type="text/javascript">
window.onbeforeunload = function() {
var n = window.event.screenX - window.screenLeft;
var b = n > document.documentElement.scrollWidth - 20;
if (!(b && window.event.clientY < 0 || window.event.altKey)) {
window.event.returnValue = "未保存的数据可能会丢失!"; //这里可以放置你想做的操作代码
}
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section"  data-js="js/homePageDetail.js?ver=0.3" class="section m10">
        <form>
        </form>
        <form id="saveForm">
        <div class="savePanel">
            <div class="commonSelectWrap">
                <em class="tit">名称:</em>
                <div class="searchInput" style="width: 340px;">
                    <input type="text" name="Title"  id="searchType"  placeholder="请输入" value="" />
                </div>
            </div>
            <!--commonSelectWrap-->
            <div class="commonBtn saveHomPageBtn">
                保存</div>
        </div>
        </form>
        <div class="showContentWrap clearfix">
            <img src="images/phoneBg.png" width="363" height="276" class="topImg" />
            <div class="collectionList showLayer">
                <div class="backGround">
                    <div id="sortable">
                      <div class="loading">
                                            <span>
                                                <img src="../static/images/loading.gif"></span>
                        </div>
                       <!-- <div data-type="followInfo" class="ation">
                            <div class="jsListItem commonSelectArea" data-type="rightSearchTemp"
                                                               data-key="数据对象名称" data-model="followInfo">
                            <div class="follow">
                            <div class="backBg"></div>
                             <p>欢迎进入微信商城</p>
                             <div class="followBtn">立即关注</div>
                             </div>
                             <div class="handle">
                                <div class="bg">
                                     </div>
                                <span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>
                             </div>
                            </div>
                        </div>
                        <div data-type="search" class="action">
                            <div class="jsListItem commonSearchArea commonSelectArea" data-type="rightSearchTemp"
                                data-key="" data-model="Search">
                                <a href="javascript:Jit.AM.toPage('Category')" class="allClassify"></a>
                                <div class="commonSearchBox">
                                    <p class="searchBtn">
                                        <input id="searchBtn"  type="button" /></p>
                                    <p class="searchInput">
                                        <input id="searchContent" type="text" value="搜索店铺内的宝贝..." placeholder="搜索店铺内的宝贝..." /></p>
                                </div>
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                        </div>
                        &lt;!&ndash;search&ndash;&gt;
                        <div data-type="adList" class="action">
                            <div class="jsListItem jsTouchslider commonSelectArea" data-type="rightADTemp" data-key=""
                                data-model="ad">
                                <div class="touchslider touchslider-demo">
                                    <div class="touchslider-viewport">
                                        <div style="height: 100%; width: 100%;">
                                            <div class="touchslider-item">
                                                <img src="images/adList/01.png" /></div>
                                            <div class="touchslider-item">
                                                <img src="images/adList/01.png" /></div>
                                            <div class="touchslider-item">
                                                <img src="images/adList/01.png" /></div>
                                            <div class="touchslider-item">
                                                <img src="images/adList/01.png" /></div>
                                            <div class="touchslider-item">
                                                <img src="images/adList/01.png" /></div>
                                        </div>
                                    </div>
                                    <div class="dotWrap">
                                        <div class="dotContainer">
                                            <span class="touchslider-nav-item  touchslider-nav-item-current"></span><span class="touchslider-nav-item  ">
                                            </span><span class="touchslider-nav-item  "></span><span class="touchslider-nav-item  ">
                                            </span><span class="touchslider-nav-item "></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                        </div>
                        &lt;!&ndash;adList&ndash;&gt;
                        <div data-type="entranceList" class="action">
                            <p class="space">
                            </p>
                            <div class="jsListItem commonSelectArea" data-type="rightEntranceTemp" data-key=""
                                data-model="entrance">
                                <div class="navPicArea">
                                    <a href="javascript:;">
                                        <img src="images/classify/2_01.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_02.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_03.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_04.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_05.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_06.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_07.png" alt="" />
                                    </a><a href="javascript:;">
                                        <img src="images/classify/2_08.png" alt="" />
                                    </a>
                                </div>
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                   <span class="jsExitGroup"></span>
                                    <span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                        </div>
                        &lt;!&ndash;entranceList&ndash;&gt;
                        <div data-type="secondKill" class="action">
                            <p class="space">
                            </p>
                            <div class="jsListItem commonSelectArea" data-type="rightSecondKillTemp" data-key=""
                                data-model="secondKill">
                                <div class="secondKill">
                                    <div class="tit">
                                        <b>疯狂团购</b>
                                        <img src="images/time.png">
                                        <div class="timeList" data-time="86400">
                                            <em>00</em>:<em>00</em>:<em>00</em>
                                        </div>
                                    </div>
                                    <div style="display: none" class="commonIndexArea" type="3">
                                        <div class="leftBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/3_1.png"></a>
                                        </div>
                                        <div class="rightBox rightBoxModel2">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/3_2.png"></a> <a href="javascript:;">
                                                    <img src="images/itemlist/3_3.png"></a>
                                        </div>
                                    </div>
                                    <div style="display: none" class="commonIndexArea" type="1">
                                        <div class="allBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/1_1.png"></a>
                                        </div>
                                    </div>
                                    <div style="" class="commonIndexArea" type="2">
                                        <div class="leftBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/2_1.png"></a>
                                        </div>
                                        <div class="rightBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/2_2.png"></a>
                                        </div>
                                    </div>
                                    &lt;!&ndash;commonIndexArea&ndash;&gt;
                                </div>
                                &lt;!&ndash;.secondKill&ndash;&gt;
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span>
                                    <span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                            &lt;!&ndash;jsListItem&ndash;&gt;
                        </div>
                        &lt;!&ndash;#secondKill&ndash;&gt;
                        <div data-type="eventList" class="action">
                            <p class="space">
                            </p>
                            <div class="jsListItem commonSelectArea" data-type="rightEventTemp" data-key="" data-model="event">
                                <div class="noticeList">
                                    <div class="list clearfix">
                                        <div class="noticeArea">
                                            <div class="box">
                                                <h2 class="title clock">
                                                    限时抢购</h2>
                                                <div class="timeList" data-time="8400">
                                                    <em>00</em>:<em>00</em>:<em>00</em>
                                                </div>
                                                <img src="images/skill/1.png">
                                            </div>
                                            &lt;!&ndash;box&ndash;&gt;
                                            <div class="info">
                                            </div>
                                        </div>
                                        &lt;!&ndash;noticeArea&ndash;&gt;
                                        <div class="noticeArea">
                                            <div class="box">
                                                <h2 class="title clock">
                                                    疯狂团购</h2>
                                                <div class="timeList" data-time="8400">
                                                    <em>00</em>:<em>00</em>:<em>00</em>
                                                </div>
                                                <div class="center">
                                                    <img src="images/skill/2.png" width="104" height="120">
                                                </div>
                                            </div>
                                            &lt;!&ndash;box&ndash;&gt;
                                            <div class="info">
                                            </div>
                                        </div>
                                        &lt;!&ndash;noticeArea&ndash;&gt;
                                        <div class="noticeArea">
                                            <div class="box">
                                                <h2 class="title clock">
                                                    热销榜单</h2>
                                                <div class="end">
                                                    你值得拥有
                                                </div>
                                                <img src="images/skill/3.png" width="106" height="120">
                                            </div>
                                            &lt;!&ndash;box&ndash;&gt;
                                            <div class="info">
                                            </div>
                                        </div>
                                        &lt;!&ndash;noticeArea&ndash;&gt;
                                    </div>
                                </div>
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span>
                                    <span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                        </div>
                        &lt;!&ndash;eventList&ndash;&gt;
                        <div data-type="originalityList" class="action category">
                            <p class="space">
                            </p>
                            <div class="jsListItem commonSelectArea" data-type="rightSecondKillTemp" data-key=""
                                data-model="secondKill">
                                <div class="secondKill">
                                    <div class="tit">
                                        <b>创意组合</b>
                                    </div>
                                    <div style="" class="commonIndexArea" type="3">
                                        <div class="leftBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/3_1.png"></a>
                                        </div>
                                        <div class="rightBox rightBoxModel2">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/3_2.png"></a> <a href="javascript:;">
                                                    <img src="images/itemlist/3_3.png"></a>
                                        </div>
                                    </div>
                                    <div style="display: none" class="commonIndexArea" type="1">
                                        <div class="allBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/1_1.png"></a>
                                        </div>
                                    </div>
                                    <div style="display: none" class="commonIndexArea" type="2">
                                        <div class="leftBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/2_1.png"></a>
                                        </div>
                                        <div class="rightBox">
                                            <a href="javascript:;">
                                                <img src="images/itemlist/2_2.png"></a>
                                        </div>
                                    </div>
                                    &lt;!&ndash;commonIndexArea&ndash;&gt;
                                </div>
                                &lt;!&ndash;.secondKill&ndash;&gt;
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                            &lt;!&ndash;jsListItem&ndash;&gt;
                        </div>
                        <div data-type="publicList" class="action">
                            <div class="jsListItem commonSelectArea " data-type="" data-key="" data-model="">
                                <div class="imagePanel" type="1" style="display: none">
                                    <div class="item">
                                    <img src="images/leftJiantou.png">
                                        <img src="images/publicList/1_1.png">
                                        <div class="txtPanel">
                                            <p class="name">
                                                商品名称</p>
                                            <p>
                                                <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                            </p>
                                            <p class="sales">
                                                <b>销量:</b> <b>18285</b></p>
                                        </div>
                                    </div>
                                    &lt;!&ndash;item&ndash;&gt;
                                    <div class="item">
                                        <img src="images/publicList/1_2.png">
                                        <div class="txtPanel">
                                            <p class="name">
                                                商品名称</p>
                                            <p>
                                                <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                            </p>
                                            <p class="sales">
                                                <b>销量:</b> <b>18285</b></p>
                                        </div>
                                    </div>
                                    &lt;!&ndash;item&ndash;&gt;
                                </div>
                                &lt;!&ndash;imagePanel&ndash;&gt;
                                <div class="imagePanel" type="2">
                                    <div class="item">
                                        <div class="itemL">
                                            <img src="images/publicList/2_1.png" />
                                            <div class="txtPanel">
                                                <p class="name">
                                                    商品名称</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                        </div>
                                        <div class="itemR">
                                            <img src="images/publicList/2_2.png" />
                                            <div class="txtPanel">
                                                <p class="name">
                                                    商品名称</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                        </div>
                                        <div class="itemL">
                                            <img src="images/publicList/2_3.png" />
                                            <div class="txtPanel">
                                                <p class="name">
                                                    商品名称</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                        </div>
                                        <div class="itemR">
                                            <img src="images/publicList/2_4.png" />
                                            <div class="txtPanel">
                                                <p class="name">
                                                    商品名称</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                &lt;!&ndash;imagePanel&ndash;&gt;
                                <div class="imagePanel" type="3">
                                    <div class="item">
                                        <img src="images/publicList/1_1.png">
                                        <div class="txtPanel">
                                            <p class="name">
                                                商品名称</p>
                                            <p>
                                                <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                            </p>
                                            <p class="sales">
                                                <b>销量:</b> <b>18285</b></p>
                                        </div>
                                    </div>
                                    &lt;!&ndash;item&ndash;&gt;
                                    <div class="item">
                                        <div class="itemL">
                                            <img src="images/publicList/2_1.png" />
                                            <div class="txtPanel">
                                                <p class="name">
                                                    商品名称</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                        </div>
                                        <div class="itemR">
                                            <img src="images/publicList/2_2.png" />
                                            <div class="txtPanel">
                                                <p class="name">
                                                    商品名称</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                        </div>
                                    </div>
                                    &lt;!&ndash;item&ndash;&gt;
                                </div>
                                &lt;!&ndash;imagePanel&ndash;&gt;
                                <div class="imagePanel" typeof="4">
                                 <div class="item">
                                 <img src="images/publicList/4_1.png" class="imgL">
                                 <div class="rightPanel">
                                            <div class="txtPanel">
                                                <p class="name">商品名称商品名称商品名称...</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                </div> &lt;!&ndash;rightPanel&ndash;&gt;
</div>&lt;!&ndash;item&ndash;&gt;
                                 <div class="item">
                                 <img src="images/publicList/4_2.png" class="imgL">
                                 <div class="rightPanel">
                                            <div class="txtPanel">
                                                <p class="name">商品名称商品名称商品名称...</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                </div> &lt;!&ndash;rightPanel&ndash;&gt;
</div>&lt;!&ndash;item&ndash;&gt;
                                 <div class="item">
                                 <img src="images/publicList/4_3.png" class="imgL">
                                 <div class="rightPanel">
                                            <div class="txtPanel">
                                                <p class="name">商品名称商品名称商品名称...</p>
                                                <p>
                                                    <b class="Price">价格 </b><b class="original">原价</b> <b class="discount">折扣</b>
                                                </p>
                                                <p class="sales">
                                                    <b>销量:</b> <b>18285</b></p>
                                            </div>
                                </div> &lt;!&ndash;rightPanel&ndash;&gt;
</div>&lt;!&ndash;item&ndash;&gt;
                                </div>
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                            &lt;!&ndash;jsListItem&ndash;&gt;
                        </div>
                        <div data-type="navList" class="action">
                            <div class="jsListItem jsTouchslider commonSelectArea" data-type="rightNavigationTemp"
                                data-key="" data-model="nav">
                                <div class="navlist">
                                    <ul>
                                        <li><a>
                                            <img src="images/nav/1.png"><p>
                                                首页</p>
                                        </a></li>
                                        <li><a>
                                            <img src="images/nav/2.png"><p>
                                                搜索</p>
                                        </a></li>
                                        <li><a>
                                            <img src="images/nav/3.png"><p>
                                                购物车</p>
                                        </a></li>
                                        <li><a>
                                            <img src="images/nav/4.png"><p>
                                                我的</p>
                                        </a></li>
                                    </ul>
                                </div>
                                <div class="handle">
                                    <div class="bg">
                                    </div>
                                    <span class="jsExitGroup"></span><span class="jsRemoveGroup"></span>
                                </div>
                            </div>
                        </div>-->

                    </div>
                </div>
                <!--backGround-->
                <div class="zsy">
                </div>
            </div>
            <!--collectionList-->
            <div id="addItem" class="commonOptsBtn">
                <div class="icon">
                </div>
                <p>
                    添加模块</p>

                <div class="listAdd">
                <div class="addBtn" data-createtype="followInfo">
                        立即关注</div>
                    <div class="addBtn" data-createtype="Search">
                        搜索框</div>
                    <div class="addBtn" data-createtype="adList">
                        广告轮播</div>
                    <div class="addBtn" data-createtype="entranceList">
                        分类导航</div>
                    <div class="addBtn" data-createtype="secondKill" data-showtype="2">
                        限时抢购</div>
                    <div class="addBtn" data-createtype="secondKill" data-showtype="3">
                        热销榜单</div>
                    <div class="addBtn" data-createtype="secondKill" data-showtype="1">
                        疯狂团购</div>
                    <div class="addBtn" data-createype="originalityList">
                        创意组合</div>
                    <div class="addBtn" data-createtype="productList">
                        商品列表</div>
                    <div class="addBtn paddingBtn" data-createtype="eventList">
                                            限时抢购/疯狂团购/热销榜单</div>
                    <div class="addBtn" data-createtype="nav">
                        底部导航</div>

                </div>
            </div>
            <div id="editLayer" class="handleLayer" style="display: none;">
            </div>
            <!--editLayer-->
        </div>
        <!--showContentWrap-->



         <div id="materialTextList" class="popupLayer" style="display: none;">
                    <div class="wrapTitle clearfix">
                        <h2 class="title">
                            图文选择</h2>
                        <span class="closePopupLayer closeBtn"></span>
                    </div>
                    <div class="infoShow" id="material">
                        <div class="wrapTable">
                            <table class="gridtable">
                                <thead>
                                    <tr>
                                        <th>
                                            图文素材名称
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="layerMaterialTextList">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                          <div class="pageContianer">
                                            <div class="dataMessage" >没有符合条件的查询记录</div>
                                                <div id="kpText" class="pager" >
                                                </div>
                                            </div>
                    </div>
                </div>




        <div id="productPopupLayer" class="popupLayer" style="display: none;">
            <div class="wrapTitle clearfix">
                <h2 class="title">
                    产品选择</h2>
                <span class="closePopupLayer closeBtn"></span>
            </div>
            <div class="infoShow" id="infoShow">
                <div class="wrapSearch clearfix">
                    <p>
                        <select id="categorySelect">
                            <option>类别</option>
                        </select>
                    </p>
                    <p>
                        <input type="text" placeholder="商品名称">
                    </p>
                    <span class="searchBtn">查询</span>
                </div>
                <div class="wrapTable">
                    <table class="gridtable">
                        <thead>
                            <tr>
                                <th>
                                    商品类别
                                </th>
                                <th>
                                    商品名称
                                </th>
                            </tr>
                        </thead>
                        <tbody id="layerProductList">
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                  <div class="pageContianer">
                                    <div class="dataMessage" >没有符合条件的查询记录</div>
                                        <div class="pager" id="kkpager11" >
                                        </div>
                                    </div>
            </div>
        </div>
          <div id="productPopupLayerSkill" class="popupLayer" style="display: none;">
                    <div class="wrapTitle clearfix">
                        <h2 class="title">
                            产品选择</h2>
                        <span class="closePopupLayer closeBtn"></span>
                    </div>
                    <div class="infoShow" id="skillInfoShow">
                        <div class="wrapSearch clearfix">
                            <p>
                                <select id="categorySelect">
                                    <option>类别</option>
                                </select>
                            </p>
                            <p>
                                <input type="text" placeholder="商品名称">
                            </p>
                            <span class="searchBtn">查询</span>
                        </div>
                        <div class="wrapTable">
                            <table class="gridtable">
                                <thead>
                                    <tr>
                                        <th>
                                            商品类别
                                        </th>
                                        <th>
                                            商品名称
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="layerProductList">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                          <div class="pageContianer">
                                            <div class="dataMessage" >没有符合条件的查询记录</div>
                                                <div class="pager" id="kkpager11" >
                                                </div>
                                            </div>
                    </div>
                </div>
        <div id="categoryPopupLayer" class="popupLayer" style="display: none;">
            <div class="wrapTitle clearfix">
                <h2 class="title">
                    商品类型</h2>
                <span class="closePopupLayer closeBtn"></span>
            </div>
            <div class="infoShow">
                <div class="wrapSearch clearfix">
                    <p>
                        <input type="text" placeholder="类别名称">
                    </p>
                    <span class="searchBtn">查询</span>
                </div>
                <div class="wrapTable">
                    <table class="gridtable">
                        <thead>
                            <tr>
                                <th>
                                    商品类别
                                </th>
                            </tr>
                        </thead>
                        <tbody id="layerCategoryList">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
     <div class="pageContianer">
                                    <div class="dataMessage" >没有符合条件的查询记录</div>
                                        <div class="pager" id="kkpager12" >
                                        </div>
                                    </div>
            </div>
        </div>
        <div id="activityPopupLayer" class="popupLayer" style="display: none;">
                    <div class="wrapTitle clearfix">
                        <h2 class="title">
                            活动类型</h2>
                        <span class="closePopupLayer closeBtn"></span>
                    </div>
                    <div class="infoShow">

                        <div class="wrapTable">
                            <table class="gridtable">
                                <thead>
                                    <tr>
                                        <th>
                                            活动类型名称
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="layerActivityLayer">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
             <div class="pageContianer">
                                            <div class="dataMessage" >没有符合条件的查询记录</div>
                                                <div class="pager" id="kkpager14" >
                                                </div>
                                            </div>
                    </div>
                </div>

        <div id="commodityGroupLayer" class="popupLayer" style="display: none;">
                    <div class="wrapTitle clearfix">
                        <h2 class="title">
                            商品分组</h2>
                        <span class="closePopupLayer closeBtn"></span>
                    </div>
                    <div class="infoShow">
                        <div class="wrapSearch clearfix">
                            <p>
                                <input type="text" placeholder="分组名称">
                            </p>
                            <span class="searchBtn">查询</span>
                        </div>
                        <div class="wrapTable">
                            <table class="gridtable">
                                <thead>
                                    <tr>
                                        <th>
                                            商品分组
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="layerCommodityList">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
             <div class="pageContianer">
                                            <div class="dataMessage" >没有符合条件的查询记录</div>
                                                <div class="pager" id="kkpager13" >
                                                </div>
                                            </div>
                    </div>
                </div>



        <div class="ui-mask">
        </div>
    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/AppConfig/js/main"%>"></script>
</asp:Content>
