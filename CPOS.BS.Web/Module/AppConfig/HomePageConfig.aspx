<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>首页管理</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Framework/Javascript/Other/kindeditor/themes/default/default.css"%>" />
    <link rel="stylesheet" href="<%=StaticUrl+"/module/AppConfig/css/style.css"%>" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" data-js="js/homePageConfig" class="section m10">

        <div class="showContentWrap clearfix">
	        <div class="collectionlist showLayer">
                <div id="adList">
                    
                </div>
    	        <div id="entranceList">

    	        </div>
    	        
                <div id="eventList">
                    
                </div>
                
        
                <div id="categoryList">
                    
                </div>
                
        
                <div id="addItem" class="commonSelectArea">
        	        <div class="addLayerBtn"></div>
                </div>    
        
            </div>
    
    
    
            <div id="editLayer" class="handleLayer" style="display:none;">
    	        
            </div>
        </div>

        <div id="productPopupLayer" class="popupLayer" style="display:none;">
	        <div class="wrapTitle clearfix">
                <h2 class="title">产品选择</h2>
                <span class="closePopupLayer closeBtn">X</span>
            </div>
            <div class="infoShow">
    	        <div class="wrapSearch clearfix">
        	        <p>
            	        <select id="categorySelect">
                	        <option>类别</option>
                        </select>
                    </p>
                    <p>
            	        <input type="text" placeholder="商品名称">
                    </p>
                    <span class="searchBtn"></span>
                </div>
        
                <div class="wrapTable">
        	        <table class="gridtable">
                        <thead>
                            <tr>
                                <th>商品类别</th>
                                <th>商品名称</th>
                            </tr>
                        
                        </thead>
                        <tbody id="layerProductList">
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                            </tr>
                        </tbody>
                   </table>
                </div>
                <div class="pageWrap" style="display:none;">

                </div>
                    
            </div>
        </div>
        <div id="categoryPopupLayer" class="popupLayer" style="display:none;">
	        <div class="wrapTitle clearfix">
                <h2 class="title">商品类型</h2>
                <span class="closePopupLayer closeBtn">X</span>
            </div>
            <div class="infoShow">
    	        <div class="wrapSearch clearfix">
                    <p>
            	        <input type="text" placeholder="类别名称">
                    </p>
                    <span class="searchBtn"></span>
                </div>
        
                <div class="wrapTable">
        	        <table class="gridtable">
                        <thead>
                            <tr>
                                <th>商品类别</th>
                            </tr>
                        </thead>
                        <tbody id="layerCategoryList" >
                            <tr><td></td></tr>
                            <tr><td></td></tr>
                            <tr><td></td></tr>
                            <tr><td></td></tr>
                            <tr><td></td></tr>
                        </tbody>
                   </table>
                </div>
                <div class="pageWrap" style="display:none;">

                </div>
            </div>
        </div>
        <div class="ui-mask"></div>

    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/AppConfig/js/main"%>"></script>
</asp:Content>
