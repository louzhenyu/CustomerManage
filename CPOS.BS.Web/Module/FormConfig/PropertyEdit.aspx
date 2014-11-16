<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <a href="PropertyEdit.aspx">PropertyEdit.aspx</a>
    <title>属性管理</title>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" data-js="FormConfig/propertyEdit" class="section m10">
        
        <div class="showContentWrap clearfix">
            <div class="createdModifyArea">
	            <span class="digTit">动态属性管理</span>
            </div>

            <div class="createdModifyContentArea clearfix">
	            <div class="titleWrap">
    	            <h2 class="tit">在这里您可以新增会员动态扩展属性</h2>
                </div>

                <div class="showAttrWrap">
    	            <div class="commonBox">
        	            <div class="titWrap">
            	            <h2>会员基本属性</h2>
                        </div>
                        <div class="info clearfix">
            	            <div class="item">姓名</div>
                            <div class="item">手机号码</div>
            	            <div class="item">性别</div>
                        </div>
                    </div>
        
        
                    <div class="commonBox extendBox">
        	            <div class="titWrap">
            	            <h2>会员扩展属性</h2>
                        </div>
                        <div class="info clearfix">
                            <div id="extendPPTList">
                                <div class="item on1">住房面积
                	                <span>待确认</span>
                                </div>
                                <div class="item on2">是否商铺用户
                	                <span>处理中</span>
                                </div>
            	                <div class="item on3">购房时间
                	                <span>已使用</span>
                                </div>
                            </div>            	            
                            <div class="addPPTBtn item addBtn">添加</div>
                        </div>
                    </div>
        
        
                    <div class="commonBox extendBox">
        	            <div class="titWrap">
            	            <h2>会员高级制定属性</h2>
                        </div>
                        <div class="info clearfix">
            	            <div id="advancedPPTList">
                                <div class="item on1">住房面积
                	                <span>待确认</span>
                                </div>
                                <div class="item on2">是否商铺用户
                	                <span>处理中</span>
                                </div>
            	                <div class="item on3">购房时间
                	                <span>已使用</span>
                                </div>
                            </div>            	            
                            <div class="addPPTBtn item addBtn">添加</div>
                        </div>
                    </div>
                </div>
    
    
            </div>            
        </div>        
        <div class="ui-mask"></div>

    </div>
    <script type="text/javascript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/main"></script>
</asp:Content>
