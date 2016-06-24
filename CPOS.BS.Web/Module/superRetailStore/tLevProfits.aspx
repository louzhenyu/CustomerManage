<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>分润管理</title>
    <link href="<%=StaticUrl+"/module/superRetailStore/css/tLevProfits.css?v1.3"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/tLevProfits.js?v1.3">
        <!--内容界面-->
        <div class="profits">
            <!--第一列 分销页面标题头-->
            <div class="col baseTitle">
                <span class="font16">分销商品定价规则</span>
				<!--<div class="saleBtn floatright"></div>-->
            </div>
            <!--第二列 商品定价规则-->
            <div class="col bgGrey borderTrangle setPrice">
                <!--小三角-->
                <div class="triangle bgGrey"></div>
                <!--主要内容-->
                    <!--滑动色块-->
                <div class="slideimg floatleft">
                	<div class="priceContentzero floatleft">    <!--滑动默认-->
                        <div class="contentText bgDefault colorwhite">
                            <span>请在右边录入分销商品规则的比例</span>
                        </div>
                    </div>
                    
                   	<div id="slideimgBox" style="display:none">
                    <div class="priceContentone floatleft">    <!--滑动1-->
                        <div class="contentText bgLitOrange colorwhite">
                            <span>商品销售佣金比例</span>
                        </div>
                        <div class="contentText colorOrange"><span>20%</span></div>
                    </div>
                    <div class="priceContenttwo floatleft"><!--滑动2-->
                        <div class="contentText bgred colorwhite">
                            <span>分销商分润比例</span>
                             <!--小三角-->
                            <div class="tooltipDiv">
                                <div class="bgWhite TipText">
                                    <div style="margin-top:-8px"><span>商品销售</span></div>
                                    <div style="margin-top:-20px">
                                        <span>佣金比例</span>
                                        <span class="colorred" id="tiptext0ne"></span>
                                    </div>
                                </div>
                                <div class="LitTriange bgWhite"></div>
                            </div>
                        </div>
                        <div class="contentText colorred"><span>20%</span></div>
                    </div>
                    <div class="priceContentthree floatleft">    <!--滑动3-->
                        <div class="contentText bgDeepGreen colorwhite">
                            <span>商家利润比例</span>
                             <!--小三角-->
                            <div class="tooltipDiv">
                                <div class="bgWhite TipText">
                                    <div style="margin-top:-8px"><span>分销分润</span></div>
                                    <div style="margin-top:-20px">
                                        <span>比例</span>
                                        <span class="colorred" id="tiptextTwo"></span>
                                    </div>
                                </div>
                                <div class="LitTriange bgWhite"></div>
                            </div>
                        </div>
                        <div class="contentText colorGreen"><span>20%</span></div>
                    </div>
                    <div class="priceContentfour floatleft">    <!--滑动4-->
                        <div class="contentText bgblue colorwhite">
                            <span>分销商品成本比例</span>
                             <!--小三角-->
                            <div class="tooltipDiv">
                                <div class="bgWhite TipText">
                                    <div style="margin-top:-8px"><span>商家利润</span></div>
                                    <div style="margin-top:-20px">
                                        <span>比例</span>
                                        <span class="colorred" id="tiptextThree"></span>
                                    </div>
                                </div>
                                <div class="LitTriange bgWhite"></div>
                            </div>
                        </div>
                        <div class="contentText colorblue"><span>20%</span></div>
                    </div>
                    </div>
                    
                </div>
                <form></form>
                <form  id="loadData">
                    <!--输入框-->
                <div class="inputPrice floatleft">
                    <div class="systemSet" style="text-align:right">
                       <span style="margin-right:12px">分销商品成本比例：</span>
		               <input type="text" class="easyui-numberbox systemInput" name="Cost" value="" data-options="min:0.1,max:100,precision:1">
                       <span>%</span> 
                    </div>
                    <div class="systemSet" style="text-align:right">
                       <span style="margin-right:12px">商品销售佣金比例：</span>
		               <input type="text" class="easyui-numberbox systemInput" name="SkuCommission" data-options="min:0.1,max:99.9,precision:1">
                       <span>%</span> 
                    </div>
                    <div class="systemSet" style="text-align:right">
                       <span style="margin-right:12px">分销商分润比例：</span>
		               <input type="text" class="easyui-numberbox systemInput" name="DistributionProfit" data-options="min:0.1,max:99.9,precision:1">
                       <span>%</span> 
                    </div>
                    <div class="systemSet" style="text-align:right">
                       <span style="margin-right:12px">商家利润比例：</span>
		               <input type="text" class="easyui-numberbox systemInput" name="CustomerProfit" data-options="min:0.1,max:99.9,precision:1">
                       <span>%</span>
                    </div>
                    <a href="javascript:;" class="commonBtn queryBtn" >一键分销</a>
                </div>
                </form>
            </div>
            
            
            
            <!--第三列 分销商品三级利润-->
            <div id="setProfitArea" class="parent" style="display:none">
                <div class="profitTitle">
                    <span class="baseTitle">1-2-1 三级分销提成体系</span>
                </div>
                <div class="profitTitle bgGrey lineTwo">
                    <div class="floatleft">
                        <a href="javascript:;" id="enteringProtocolBtn" class="commonBtn commonLink">录入协议</a>
                    </div>
                    <div class="floatleft lineTwoText">
                        <span class="isEntering">未录入</span>
                        <span>可分配分销利润剩余</span>
                        <span id="residueProfit">%</span>
                    </div>
                    <div class="floatleft">
                        <a href="javascript:;" class="commonBtn commonLink saveProfit">保存</a>
                    </div>
                    <div class="floatleft" style="display:none">
                        <a href="javascript:;" class="commonBtn commonLink cancelProfit">取消</a>
                    </div>
                </div>
                <!--三级体系-->
                <div id="hierarchySystem">
                    <div class="systemOne">
                       <div class="systemSet">
                           <span style="margin-right:15px">分润提成</span>
                           <input type="text" id="lev1" class="easyui-numberbox systemInput" value="" data-options="min:0,max:100,precision:1">
                           <span>%</span> 
                        </div>
                        <div class="systemTip">
                            <span>一级分润可以从下线销售中分润</span>
                            <span id="lev1Num"></span>
                            <span>%</span>
                        </div>
                    </div>
                    <div class="systemTwo">
                        <div class="systemSet">
                           <span style="margin-right:15px">分润提成</span>
                           <input type="text" class="easyui-numberbox systemInput" value="" data-options="min:0,max:100,precision:1">
                           <span>%</span> 
                        </div>
                        <div class="systemTip">
                            <span>二级分润可以从下线销售中分润</span>
                            <span id="lev2Num"></span>
                            <span>%</span>
                        </div>
                        <div class="switchBtn" style="display:none;">
                            <span></span>
                        </div>
                    </div>
                    <div class="systemThree">
                        <div class="systemSet">
                           <span style="margin-right:15px">分润提成</span>
                           <input type="text" class="easyui-numberbox systemInput" value="" data-options="min:0,max:100,precision:1">
                           <span>%</span>
                        </div>
                        <div class="systemTip">
                            <span>三级分润可以从下线销售中分润</span>
                            <span id="lev3Num"></span>
                            <span>%</span>
                        </div>
                        <div class="switchBtn" data-flag='level'>
                            <span></span>
                        </div>
                    </div>
                    <div class="systemFour">
                        <span>商品销售佣金比例 20%</span>
                    </div>
                    <div class="systemFifve">
                        <span>分润分销比例 20%</span>
                    </div>
                    <div class="systemSix"></div>
               </div>     
            </div>
        </div>
    </div>
    
    
    
    <!--弹出-->
    <div style="display:none">
        <div id="winmessage" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="overflow:hidden">
                      <div class="protocolArea">
                            <div class="item nameBox">
                                <em>协议名称:</em>
                                <div class="fromBox"><input class="protocolName" maxlength="5" placeholder="" /></div>
                                <p class="numLimit"><span>0</span>/5</p>
                            </div>
                            <div class="item contBox">
                            	<em>协议内容:</em>
                                <div class="fromBox"><textarea class="protocolContent" placeholder=""></textarea></div>
                            </div>
                            <div class="item expBox">
                            	<em></em>
                                <div class="fromBox">
                                	<p>如果您还没有分销商协议，可以参考模板内容进行修改。<a href="/File/ExcelTemplate/分销协议-模板.txt" target="_blank">下载样板</a></p>
                                    <p style="padding-bottom:10px;">注：编辑时不能带有任何格式哦！</p>
                                    <p>免责申明：连锁掌柜和超级分销仅提供功能支持，分销品及制度设置为品牌用户自定义，与连锁掌柜和超级分销无关。</p>
                                    <p id="protocolHintBox"></p>
                                </div>
                            </div>
                      </div>
                </div>
                <div class="btnWrap messageBtn" id="btnWrap" data-options="region:'south'">
                    <a class="easyui-linkbutton commonBtn saveBtn">确定</a>
                    <a class="easyui-linkbutton commonBtn cancelBtn cancel" style="height:35px;line-height:35px;" href="javascript:void(0)" onclick="javascript:$('#winmessage').window('close')" >取消</a>
                </div>
            </div>
        </div>
    </div>
    
    
    
    <!--弹出确认框-->
    <div style="display:none">
        <div id="winmessage2" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="overflow:hidden">
                      <div class="affirmArea">
                      	<p class="lineText">您还有<span></span>%的可分销利润可进行分配，</p>
						<p>是否并入商家利润中？</p>
                      </div>
                </div>
                <div class="btnWrap messageBtn" id="btnWrap" data-options="region:'south'">
                    <a class="easyui-linkbutton commonBtn saveBtn">确定</a>
                    <a class="easyui-linkbutton commonBtn cancelBtn cancel" style="height:35px;line-height:35px;" href="javascript:void(0)" onclick="javascript:$('#winmessage2').window('close')" >取消</a>
                </div>
            </div>
        </div>
    </div>
    
    
    
    
    <!--弹出loading.gif层-->
    <div style="display:none">
        <div id="winmessage3" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,draggable:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="overflow:hidden">
                      <div class="loadingGifBox">
                      	<p><img src="images/loading.gif" /></p>
						<p class="textBox">即将为您生成能够快速扩展分销商群体的1-2-1三级分销体系。</p>
                      </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
