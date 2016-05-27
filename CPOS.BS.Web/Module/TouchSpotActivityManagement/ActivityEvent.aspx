<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>触点活动</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/TouchSpotActivityManagement/css/ActivityEvent.css?v=0.5"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/TouchSpotActivityManagement/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="allPage" id="section" data-js="js/ActivityEvent.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">

            <!--个别信息查询-->
            <div class="optionBtn">
                <a href="javascript:;" class="commonBtn  icon w100  icon_add r" id="addShareBtn">新增活动</a>
            </div>
            <div class="tableWrap" id="tableWrap" style="display: inline-block; width: 100%;">
                <div class="dataTable" id="gridTable">

                          <div  class="loading">
                                   <span>
                                 <img src="../static/images/loading.gif"></span>
                            </div>

                </div>

                <div id="pageContianer">
                    <div class="dataMessage">没有符合条件的查询记录</div>
                    <div id="kkpager">
                    </div>
                </div>
            </div>
        </div>
    </div>



    <!-- 遮罩层 -->
    <div class="jui-mask" style="display: none;"></div>



    <!--添加活动，弹出-->
    <div class="jui-dialog jui-dialog-addShare" style="display: none">
        <div class="jui-dialog-tit">
            <h2>触点活动</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="redPackageContent">
            <form></form>
            <form id="addShareForm">
                
                <div class="commonSelectWrap">
                    <em class="tit">触点类型：</em>
                        <input id="ContactEventId" name="ContactEventId" value="" type="hidden" style="display: none" />
                        <input data-text="触点类型" class="easyui-combobox textbox combo" id="Activity_ContactTypeCode" data-options="required:true,width:190,height:32,invalidMessage:'必填',missingMessage:'必填'" missingMessage="必填" name="ContactTypeCode" type="text" value="" validtype='selectIndex'>
                    
                </div>

                <div class="commonSelectWrap">
                    <em class="tit">活动名称：</em>
                    <label class="searchInput clearBorder">
                       <input data-text="活动名称" class="easyui-validatebox" placeholder="请输入" id="Activity_ContactEventName" data-options="required:true,width:190,height:32,invalidMessage:'必填'" maxlength="40" name="ContactEventName" type="text" value="" validtype='length[1,60]'>
                   </label>
                </div>

                <div class="commonSelectWrap">
                    <em class="tit">活动时间：</em>
                    <label class="dateInput clearBorder">
                        <input id="startDate" name="BeginDate" class="easyui-datebox"  data-options="required:true,width:120,height:32" validType:'date' /><span>至</span>
                        <input id="expireDate" name="EndDate" class="easyui-datebox" data-options="required:true,width:120,height:32" validtype="compareEqualityDate[$('#startDate').datebox('getText'),'当前选择的开始时间必须晚于前面选择的结束时间']" />
                    </label>
                </div>
                 <div class="commonSelectWrap"  id="ActivitySelect"  style="display: none">
                    <em class="tit">活动选择：</em>
                        <input data-text="活动选择" class="easyui-combobox textbox combo" id="Activity_Select" data-options="width:190,height:32,invalidMessage:'必填',editable:false" name="ShareEventId" type="text" value="" validtype='selectIndex'>
                   
                </div>

                <div class="commonSelectWrap">
                    <em class="tit">奖品选择：</em>
                       <input data-text="奖品选择" class="easyui-combobox textbox combo" id="Activity_PrizeType" data-options="required:true,width:190,height:32,invalidMessage:'必填'" name="PrizeType" type="text" value="" validtype='selectIndex'>
                   
                </div>

                <div class="commonSelectWrap" >
                    <em class="tit">奖品数量：</em>
                      <input data-text="奖品数量" class="easyui-numberbox" min="0" max="1000000" placeholder="请输入" id="Activity_PrizeCount" data-options="required:true,width:190,height:32,invalidMessage:'奖品数量不能超过券的生成数量',missingMessage:'奖品数量不能超过券的生成数量'" name="PrizeCount" type="text" data-flag="" value="">
                    <span class="UnLimitedlayer" style="display:none;">
                        <input id="checkUnLimited" style="float: initial;" type="checkbox"   />无限制
                        <input id="UnLimited" type="hidden" name="UnLimited" value="0" />
                        </span>
                </div>

                <div class="commonSelectWrap" id="ActivityIntegral" style="display: none">
                    <em class="tit">积分：</em>
                        <input data-text="积分" class="easyui-numberbox" min="0" max="1000000" placeholder="请输入" id="Activity_Integral" data-options="width:190,height:32,invalidMessage:'必填',missingMessage:'积分必须为整数'" name="Integral" type="text" value="">
                   
                </div>

                <div class="commonSelectWrap" id="ActivityCouponType" style="display: none">
                    <em class="tit">优惠券：</em>
                       <input data-text="优惠券" class="easyui-combotree textbox combo" placeholder="请输入" id="Activity_CouponTypeID" data-options="height:32,multiple:true,invalidMessage:'优惠券数量为0时，触点活动状态自动更换为未启用。需要追加券后才可重新启用。',missingMessage:'优惠券数量为0时，触点活动状态自动更换为未启用。需要追加券后才可重新启用。'" name="CouponTypeID" type="text" value="" validtype='selectIndex'>
                    
                </div>

                <div class="commonSelectWrap" id="ActivityEvent" style="display: none">
                    <em class="tit">活动名称：</em>
                       <input data-text="活动名称" class="easyui-combobox textbox combo" id="Activity_EventId" data-options="width:190,height:32,invalidMessage:'必填'" name="EventId" type="text" value="" validtype='selectIndex'>
                    
                </div>

                <div class="commonSelectWrap" id="ActivityChanceCount" style="display: none">
                    <em class="tit">抽奖次数：</em>
                    
                        <input data-text="抽奖次数"  class="easyui-numberbox" min="0" max="100000" placeholder="请输入" id="Activity_ChanceCount" data-options="width:190,height:32,missingMessage:'抽奖次数必须为整数'" name="ChanceCount" type="text" value="">
                   
                </div>


                <div style="margin-bottom:0px;" class="ruleSetTit"></div>
                <div style="float: left;margin-left: 20px;">规则设置</div>
                <div class="commonSelectWrap" >
                    <em class="tit">奖励次数：</em>
                       <input data-text="奖励次数" class="easyui-combobox textbox combo" min="0" max="100000" placeholder="请输入" id="Activity_RewardNumber" data-options="required:true,width:190,height:32,invalidMessage:'必填'" name="RewardNumber" type="text" value="" validtype='selectIndex'>

                       
                    
                </div>
            </form>
            <div class="btnWrap">
                <a id="addActivity" href="javascript:;" class="commonBtn saveBtn ">保存</a>
                <span style="display:none;">
                <a id="cancelActivity"  href="javascript:;" class="commonBtn cancelBtn" style="margin-left: 16px;">取消</a>
                    </span>
            </div>
        </div>
    </div>


  

     <!--奖品数量追加，弹出-->
    <div class="jui-dialog jui-dialog-prizeCountAdd" style="display:none">
        <div class="jui-dialog-tit">
            <h2>奖品数量追加</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="prizeCountAddContent">
             <form id="prizeCount_Add">
                 <div class="commonSelectWrap">
                    <em class="tit" style="width:172px;">数量：</em>
                    <label class="searchInput clearBorder" style="width:308px">
                      <input data-text="奖品数量追加" class="easyui-numberbox" id="prizeCountAdd" min="0" data-options="required:true,width:308,height:32,invalidMessage:'奖品数量是大于0的整数'" data-flag="" name="prizeCount" type="text" value="">
                        
                    </label>
                </div>
                 </form>
            <div class="btnWrap">
                <a href="javascript:;" class="commonBtn saveBtn">保存</a>
                <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
            </div>
        </div>
    </div>
    

    <!--头部名称-->
    <script id="tpl_thead" type="text/html">
        <#for(var i in obj){#>
             <th><#=obj[i]#></th>
        <#}#>
    </script>


    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
