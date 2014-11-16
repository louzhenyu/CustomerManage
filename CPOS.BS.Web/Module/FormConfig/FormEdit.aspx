<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>编辑表单</title>
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="section" data-js="FormConfig/formEdit" class="section m10">
        
        <div class="showContentWrap clearfix">
            <div class="createdModifyArea">
	            <span class="digTit">表单创建修改</span>
            </div>

            <div class="createdModifyContentArea clearfix">
	            <div class="titleWrap">
    	            <h2>会员卡领卡表单</h2>
                    <span class="cancelBtn">取消</span>
                    <span class="viewBtn">预览</span>
                    <span class="saveBtn">保存</span>
                </div>
    
                <div id="leftModifyWrap" class="leftModifyWrap">
    	            <div class="phoneArea on">
        	            <p>
                            <span class="tit">手机号码<span>*</span></span>
                            <input class="inputBox" type="text"/>
                        </p>
                        <p>
                            <span class="tit">验证码<span>*</span></span>
                            <input class="inputBox" type="text"/>
                        </p>
                    </div>
                    <div id="tempPPTList">
                    </div>
                    <%--<div class="jsPropertyListItem commonAttrArea">
        	            <p>
                            <span class="tit">姓名<span>*</span></span>
                            <input class="inputBox" type="text"/>
                            <span class="checkBox">必填</span>
                            <span class="closeBtn"></span>
                        </p>
                    </div>
        
                    <div class="jsPropertyListItem commonAttrArea">
        	            <p>
                            <span class="tit">性别<span>*</span></span>
                            <input class="inputBox" type="text"/>
                            <span class="checkBox">必填</span>
                            <span class="closeBtn"></span>
                        </p>
                    </div>--%>
    	
                    <div class="tipArea">请从右侧添加会员属性<%--，或在本页拖拽排序--%></div>
                </div>
    
    
                <div id="rightModifyWrap" class="rightModifyWrap">
    	            <div class="commonBox">
        	            <div class="titWrap">
            	            <h2>会员基本属性</h2>
                        </div>
                        <div id="basicPPTList" class="info clearfix">
            	            <%--<div class="list on">
                	            <span class="tit">姓名</span>
                	            <span class="icon"></span>
                            </div>
                
                            <div class="list">
                	            <span class="tit">手机号码</span>
                	            <span class="icon"></span>
                            </div>
                
            	            <div class="list on">
                	            <span class="tit">性别</span>
                	            <span class="icon"></span>
                            </div>--%>
                        </div>
                    </div>
        
        
                    <div class="commonBox">
        	            <div class="titWrap">
            	            <h2>会员扩展属性</h2>
                            <span class="help"></span>
                        </div>
                        <div id="extendPPTList" class="info clearfix">
            	            <%--<div class="list on">
                	            <span class="tit">从事IT行业时间（年）</span>
                	            <span class="icon"></span>
                            </div>
                
                            <div class="list">
                	            <span class="tit">手机号码</span>
                	            <span class="icon"></span>
                            </div>--%>
                        </div>
                    </div>
        
        
                    <%--<div class="commonBox">
        	            <div class="titWrap">
            	            <h2>会员高级制定属性</h2>
                            <span class="help"></span>
                        </div>
                        <div id="advancedPPTList" class="info clearfix">
            	            <div class="list not">
                	            暂无
                            </div>
                        </div>
                    </div>--%>
                </div>
    
    
            </div>            
        </div>        
        <div class="ui-mask"></div>

    </div>
    <script type="text/javascript" src="/Module/static/js/lib/require.js" defer async="true" data-main="/Module/static/js/main"></script>
</asp:Content>
