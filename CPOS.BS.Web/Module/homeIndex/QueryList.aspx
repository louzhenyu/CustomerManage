<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>微官网配置页面</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.1" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/alice.min.js"></script>
    <script type="text/javascript" src="js/iscroll.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
           <div class="main">
            <div class="mainMenuList">
            <ul>
            <li class="on lb" data-title="菜单动画版" ><span >菜单动画版</span></li>
            <li  data-title="底部菜单版"  ><span >底部菜单版</span></li>
            <li   data-title="左侧菜单版"  ><span >左侧菜单版</span></li>
            <li data-title="底部滑动版"  class="rb"><span >底部滑动版</span></li>

            </ul>
             </div><!--mainMenuList-->

            <div class="phonePanel">
            <div class="phone">
            <div id="pageTitle"></div>
            <div class="screen indexPage">
           <div data-key="$HomeIndex" class="page indexArea" style="width: 304px; height: 407px;">
           				<img id="backgroundImg" class="bgPic" src="">
           				<div id="navList" class="navBox">
           					<ul>
           						<li  class="logo a_move">
           							<div class="boder"><img id="logo" class="logo" src="" alt="logo"/>
           							</div>
           						</li>
           						<li class="show a_move">
           							<a href="javascript:Jit.AM.toPage('Introduce','&type=1')"> <span></span> <span></span> </a>
           						</li>
           						<li class="sales a_move">
           							<a href="javascript:Jit.AM.toPage('Introduce','&type=2')"> <span></span> <span></span> </a>
           						</li>
           						<li class="view a_move">
           							<a href="javascript:Jit.AM.toPage('Introduce','&type=1')"> <span ></span> <span></span> </a>
           						</li>
           						<li class="appointment a_move">
           							<a href="javascript:Jit.AM.toPage('Introduce','&type=2');"> <span></span> <span></span> </a>
           						</li>
           						<li class="store a_move">
           							<a href="javascript:Jit.AM.toPage('Introduce','&type=1')"> <span></span> <span></span> </a>
           						</li>
           					</ul>
           				</div>
           			</div>
           <div data-key="$HomeIndex1" class="page page1">
           <img id="theBg" src="" >
           			<div id="menu" class="menu">
           				<p class="list">
           					<a class="move1">生益印象</a>
           					<a class="move2" href="javascript:Jit.AM.toPage('NewList','&typeId=948172F6F17A42B6A9CC6E76664ABB75&title=项目推荐');">项目推荐</a>
           				</p>
           				<p class="list">
           					<!-- <a href="javascript:Jit.AM.toPage('NewList','&typeId=773F654A4CEF43698E698A1FCFA6983F&title=品牌活动');">品牌活动</a> -->
           					<a class="move3" href="javascript:Jit.AM.toPage('ReserveStore');">门店预约</a>
           					<a class="move4" href="javascript:Jit.AM.toPage('NewList','&typeId=7296AA49227F4B49801ED6E1035095FE&title=微杂志');">微杂志</a>
           				</p>
           			</div>
          </div>
           			<div data-key="$HomeIndex2" class="page page2" id="intoShow">
                    			<div id="homeWrapper1" class="bgImgWrap1">
                    				<div id="homeScroller">
                    					<ul class="clearfix">
                    						<li>
                    							<img src="#" />
                    						</li>
                    						<li>
                    							<img src="#" />
                    						</li>
                    						<li>
                    							<img src="#" />
                    						</li>
                    						<!--      <li>
                    						<img src="#" />
                    						</li> -->
                    					</ul>
                    				</div>
                    			</div>

                    			<div  class="indexNav2">
                    				<a href="javascript:Jit.AM.toPage('NewList')" class="follow">最受关注</a>
                    				<a href="javascript:Jit.AM.toPage('CourseList')" class="course">中欧课程</a>
                    				<a href="javascript:Jit.AM.toPage('ToEmba')" class="into">走进中欧</a>
                    				<a href="javascript:Jit.AM.toPage('Fete')" class="video">20周年庆</a>
                    			</div>
                    			<div class="indexMenu"></div>
                    			<div class="menuWrap">
                    				<a href="http://weibo.com/ceibs" class="sinaweibo"></a>
                    				<a href="javascript:Jit.AM.toPageWithParam('Map','&openId=test&lng=121.603301&lat=31.243023&storeId=a2814f74a5954478ae000a8879b2bd8c&addr=上海浦东红枫路699号&store=中欧国际商学院&showmore=1')" class="location"></a>
                    				<a  class="tel"  href="javascript:Jit.AM.toPage('Contact')"  ></a>
                    			</div>
                    		</div>
           <div data-key="$HomeIndex3" class="page page3">

           	<div id="homeWrapper" class="bgImgWrap">
           				<div id="homeScroller">
           					<ul class="clearfix">
           						<li>
           							<img />
           						</li>
           						<li>
           							<img />
           						</li>
           						<li>
           							<img />
           						</li>
           						<!--      <li>
           						<img src="#" />
           						</li> -->
           					</ul>
           				</div>
           			</div>
           	<div class="indexNav3" id="menuWrapper">
           				<div class="navList">
           					<ul id="list" class="clearfix">
           						<li><a href="javascript:;" class="logoIcon"><span class="tit">了解我们</span></a></li>
           						<li><a href="javascript:;" class="zxdtIcon"><span class="tit">最新动态</span></a></li>
           						<li><a href="javascript:;" class="hdzqIcon"><span class="tit">活动专区</span></a></li>
           						<li><a href="javascript:;" class="xkzsIcon"><span class="tit">新款展示</span></a></li>
           						<li><a href="javascript:;" class="logoIcon"><span class="tit">了解我们</span></a></li>
           						<li><a href="javascript:;" class="zxdtIcon"><span class="tit">最新动态</span></a></li>
           						<li><a href="javascript:;" class="hdzqIcon"><span class="tit">活动专区</span></a></li>
           						<li><a href="javascript:;" class="xkzsIcon"><span class="tit">新款展示</span></a></li>
           					</ul>
           				</div>
           			</div>
</div>   <!--page-->
</div> <!--screen-->

</div> <!--phone-->
            <div class="deploy" id="deploy">
              <div id="pageInfo"> </div>

              <div id="paramsList"></div>
               <div class="commonBtn subMitBtn saveBtn" id="submitBtn">保存</div>
               <div class="zsy"> </div>


</div>
</div><!--phonePanel-->

</div><!--main-->
        </div> <!--allPage-->
        <div class="ui-mask">
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
            <%-- 颜色板 --%>


       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>

