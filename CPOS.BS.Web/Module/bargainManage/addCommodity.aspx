<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>砍价列表信息</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <link href="<%=StaticUrl+"/module/bargainManage/css/addCoupon.css?v=0.4"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/addBargin.js?ver=0.3" >
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">

              <div class="panelDiv">
                 <p class="title">新建砍价</p>
              </div>
              <div class="moreQueryWrap" style="display:none;">
                 <a href="javascript:;" class="commonBtn queryBtn">查询</a>
              </div>
              <div class="clear"></div>
            </div>
            <!--砍价信息-->
            <div class="barginManageTitle" id="barginTitleInfo">
            </div>

            <!--添加砍价商品-->
            <div class="addBarginCommodity">
              <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">

                <div class="panelDiv">
                   <p class="title">添加砍价商品</p>
                </div>
              </div>
              <div class="optionBtn" id="addShopBtn">
                <div class="commonBtn r sales" data-flag="add" id="sales"> <img src="images/add.png"  > 添加商品
                </div>
              </div>
            </div>

            <form></form>
            <form id="addCoupon">
              <div class="table" id="tableWrap">
                   <div class="dataTable" id="gridTable">
                                            <div  class="loading" style="padding-top: 50px;">
                                                     <span>
                                                   <img src="../static/images/loading.gif"></span>
                                              </div>

</div>  </div>
                    <div id="pageContianer">
                    <div class="dataMessage" >没有符合条件的查询记录</div>
                    </div>
            </form>
            
        </div>
    </div>
    <div style="display: none">
        <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="padding: 10px;">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                    text-align: center; padding: 5px 0 0;">
                    <a class="easyui-linkbutton commonBtn saveBtn">确定</a> <a class="easyui-linkbutton commonBtn cancelBtn"
                        href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                </div>
            </div>
        </div>
    </div>
    <!-- 弹层，活动产品新规则 -->
    <div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
    <div id="addNewRules" class="jui-dialog jui-dialog-addRule" style="display:none;">
        <div class="jui-dialog-tit">
          <h2>商品编辑</h2>
            <a href="javascript:;" class="jui-dialog-close hintClose"></a>
        </div>
        <div id="goodsInfo" class="jui-dialog-addRule-info">
            <div>
              <div class="quotaBox">
                  <em>每人限购:</em>
                  <p class="inputBox"><input type="text" id="singleCount" value=""><em style="margin:0;float:none;">件</em></p>
                  <p style="clear:left;font-size:14px;color:#">0或空代表不启用限购</p>
              </div>
              <div class="quotaBox">
                  <em>可砍时间:</em>
                  <p class="inputBox"><input type="text" id="barginTime" value=""><em style="margin:0;float:none;">小时</em></p>
              </div>
              <p class="shopName"></p>
            </div>
        </div>
        <div id="rules" class="jui-dialog-addRule-edit">
          

        </div>
        <div class="btnWrap">
            <a href="javascript:;" id="saveRules" class="commonBtn saveBtn">提交</a>
            <a href="javascript:;" class="commonBtn cancelBtn hintClose">取消</a>
        </div>
    </div>
    <!--添加商品-->
    <div id="addNewGoods" class="jui-dialog jui-dialog-addShop" style="display:none;">
        <div class="jui-dialog-tit">
          <h2>添加商品</h2>
            <a href="javascript:;" class="jui-dialog-close hintClose"></a>
        </div>
        <div class="contentArea">
          <div class="searchWrap">
              <span class="tit">请选择分类</span>
                <div class="selectBox">
                  <em></em>
                  <p id="categoryText">全部</p>
                    <div id="category" class="dropList">
                      
                    </div>
                </div>
                <label class="inputBox"><input type="text" id="keyword" placeholder="商品名称"></label>
                <a href="javascript:;" id="searchBtn" class="commonBtn searchBtn" style="margin:0;">查询</a>
            </div>
            <div class="searchResult clearfix">
              <div class="title">
                  <h2 class="classify">商品分类</h2>
                    <h2 class="shopName">商品名</h2>
                </div>
                <div id="searchList" class="searchList">
                  
                </div>
                
            </div>
            <div id="kkpager1" style="text-align:right;"></div>
        </div>
    </div>
    <!--商品分类-->
    <script id="tpl_category" type="text/html">
        <span data-categoryid="">全部</span>
         <#for(var i=0;i<list.length;i++){ var item=list[i];#>
            <span data-categoryid="<#=item.CategoryId#>"><#=item.CategoryName#></span>
        <#}#>
    </script>
    <script id="tpl_goods_list" type="text/html">
        <ul>
         <#for(var i=0;i<list.length;i++){ var item=list[i];#>
            <li data-itemid="<#=item.ItemId#>" data-name="<#=item.ItemName#>">
                <p class="classify"><#=item.ItemCategoryName#></p>
                <p class="shopName t-overflow"><#=item.ItemName#></p>
            </li>
        <#}#>
        </ul>
    </script>
    <!--商品规格规则-->
    <script id="tpl_rule" type="text/html">
     <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <div class="item <#=item.IsSelected=="true"?"on":""#>" data-mappingid="<#=item.EventSkuInfo && item.EventSkuInfo.EventItemMappingID#>" data-skuid="<#=item.SkuID#>" data-skuMappid="<#=item.EventSkuInfo && item.EventSkuInfo.EventSKUMappingId#>" data-Disable="false">
            <div class="editArea">
              <label  class="checkBox" style="width:auto;"><span></span><#=item.SkuName#></label>
              <p class="shopName"></p>
            </div>
            <div class="editArea pl">
                
                <label><em>商品数量：</em><input type="text" disabled value="<#=item.EventSkuInfo &&item.EventSkuInfo.Qty#>" /></label>
                <label class="ml"><em>原价:</em><em><input disabled="disabled" value="<#=item.Price#>"/>元</em></label>
                <label class="base"><em>底价：</em><input disabled type="text" value="<#=item.EventSkuInfo &&item.EventSkuInfo.BasePrice#>" /><em>元</em></label>
            </div>
            <div class="editArea mtmb">
                <label><em>每次砍减价：</em><input type="text" disabled value="<#=item.EventSkuInfo &&item.EventSkuInfo.BargainStartPrice#>"/><em style="width:auto;padding:0 5px;">—</em><input type="text" disabled value="<#=item.EventSkuInfo &&item.EventSkuInfo.BargainEndPrice#>"/><em>元</em></label>
                
            </div>

        </div>
        <div class="clear"></div>
    <#}#>
    </script>
    <!--商品基本信息编辑-->
    <script id="tpl_goods_basic" type="text/html">
        <div class="title">
          <h3>活动名称:</h3><p><#=list.EventName#></p>
          <div class="optionBtn">
            <div class="commonBtn r sales" data-flag="exit">修改
            </div>
          </div> 
        </div>
        <div class="commonSelectWrap">
             <em class="tit">开始时间：</em>
             <p><#=list.BeginTime#></p>
         </div>

         <div class="commonSelectWrap">
             <em class="tit">结束时间：</em>
             <p><#=list.EndTime#></p>
         </div>
        
    </script>

    <!--商品基本信息编辑-->
    <div id="goodsBasic_exit"  class="jui-dialog jui-dialog-addGoogs" style="display:none;">
        
          <div class="jui-dialog-tit">
            <h2>修改活动名称</h2>
              <a href="javascript:;" class="jui-dialog-close hintClose"></a>
          </div>
          <div class="optionclass">
             
            <div class="title">活动名称:</div>
            <div class="borderNone" >
             <input id="campaignName" data-options="width:260,height:34,min:1,precision:0,max:10000" name="IssuedQty" style="width:260px" />
            </div>
          </div>
          <div class="optionclass">
            <div class="title">开始时间:</div>
            <div class="searchInput bordernone">
              <input id="campaignBegin"  name="order_date_begin" style="width:260px" />
            </div>
            
          </div>
          <div class="optionclass">
            <div class="title">结束时间:</div>
            <div class="searchInput bordernone">
              <input id="campaignEnd" name="order_date_end" style="width:260px"/>
            </div>
            
          </div>
          <div class="btnWrap">
              <a href="javascript:;" id="saveCampaign" class="commonBtn saveBtn">提交</a>
              <a href="javascript:;" class="commonBtn cancelBtn hintClose">取消</a>
          </div>
        
        
    </div>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
