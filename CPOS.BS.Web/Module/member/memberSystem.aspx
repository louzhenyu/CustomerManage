<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员体系</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <link href="<%=StaticUrl+"/module/member/css/memberSystem.css?v=0.1"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/memberSystem.js">
                    <div class="title">
                                               <ul id="optPanel">
                                               <li data-flag="#nav01" class="on">会员体系</li>
                                               <li data-flag="#nav02">会员卡销售激励</li>
                                               <li data-flag="#nav03">积分使用规则</li>
                                               </ul>
                                        </div>
          <div class="navPanel"  id="nav01">
          <div id="lumpList" style="display: none">
           <div class="lumpList" data-level="1"  data-id="-100">
                               <div class="lump"  data-type="cardInfo" data-filed="VipCardType">

                                  <p class="top"> 会员卡</p>
                                   <div class="content">
                                    <div class="commonSelectWrap">
                                          <label class="searchInput">
                                             <input class="easyui-validatebox" data-options="required:true,validType:'maxLength[30]'" data-filed="VipCardTypeName" placeholder="请输入等级一名称" name="VipCardTypeName" type="text" value="">
                                           </label>
                                          </div>
                                          <div class="uploadPanel"  data-filed="PicUrl">
                                          <div class="upLoadBtn">

          </div><!--upLoadBtn-->
          <p>点击上传图片</p>
          </div> <!--uploadPanel-->
          <p class="text line22 l">* 图片建议尺寸580×290PX</p>

                 </div>  <!--content-->
          </div>  <!--lump-->
                               <div class="lump" data-type="upgrade" data-filed="VipCardUpgradeRule">
                                  <p class="top"> 升级条件 <em class="edit"></em></p>
                                                <div class="content">
                                                      <p class="text line36"><b>完成注册</b></p>
                                                        <p class="text line36"><b>仅短信验证</b>（适用于所有级别）</p>
                                                      </div>  <!--content-->
          </div>  <!--lump-->
                               <div class="lump" data-type="interests" data-filed="VipCardRule">
                                  <p class="top"> 基本权益 <em class="edit"></em></p>
                                       <div class="content">
                                                                           <div class="addBtnPanel" >
                                                                           <div class="btnPanel"><p>新增基本权益</p></div>
                                                   </div> <!--addBtnPanel-->
                                                     </div>  <!--content-->
          </div>  <!--lump-->
                               <div class="lump" data-type="card" data-filed="VipCardUpgradeRewardList">
                                  <p class="top"> 开卡礼 <em class="edit"></em></p>
                                        <div class="content">
                                                        <div class="addBtnPanel">
                                                        <div class="btnPanel"><p>新增开卡礼</p></div>
                                </div> <!--addBtnPanel-->
                                    </div>  <!--content-->
          </div>  <!--lump-->

          </div>     <!--lumpList-->
           </div> <!--lumpList-->
                     <div class="lumpList on"  style="display:none">
                     <div class="lump">

                        <p class="top"> 会员卡 <em class="edit"></em></p>
                        <div class="content">
                        <div class="addBtnPanel" data-opttype="level">
                        <div class="btnPanel"><p>新增级别</p></div>
</div> <!--addBtnPanel-->
  </div>  <!--content-->
</div>  <!--lump-->
                     <div class="lump">

                        <p class="top"> 升级条件 <em class="edit"></em></p>
                         <div class="content">
                        <p class="text line36"><b>完成注册</b></p>
                          <p class="text line36"><b>仅短信验证</b>（适用于所有级别）</p>
                        </div>  <!--content-->
</div>  <!--lump-->
                     <div class="lump">

                        <p class="top"> 基本权益 <em class="edit"></em></p>
                        <div class="content">
                       <p class="tag"><em class="icon"></em> <span>会员消费享受<i>9.5</i>折</span> </p>
                         <p class="text line18">充值升级：会员折扣仅限使用卡内余额时享受；</p>
                         <p class="text line18">购卡升级或消费升级：持卡就可享受会员折扣。</p>
                           <p class="tag"><em class="icon bili"></em> <span>消费返积分比例<i>1</i>%</span> </p>
                                                 <p class="text line18">如设置1%，则每消费1元用户可获得0.01积分</p>
                      </div>  <!--content-->
</div>  <!--lump-->
                     <div class="lump">
                        <p class="top"> 开卡礼 <em class="edit"></em></p>
                        <div class="content">
                       <p class="tag"><em class="icon yhq"></em> <span>买一送一</span> </p>
                         <p class="text line18">自领券后10天有效 <i>×4</i></p>
                          <p class="tag"><em class="icon yhq"></em> <span>买一送一</span> </p>
                                                  <p class="text line18">自领券后10天有效 <i>×4</i></p>

                                                        </div>  <!--content-->
</div>  <!--lump-->

</div>     <!--lumpList-->


                    <div class="lumpList">
                      <div class="lump"  id="addLump">
                         <p class="top"> 会员卡 </p>
                  <div class="content">
                                             <div class="addBtnPanel">
                                             <div class="btnPanel"><p>新增级别</p></div>
                     </div> <!--addBtnPanel-->
               </div>  <!--content-->
 </div>  <!--lump-->

 </div>     <!--lumpList-->
        </div> <!--navPanel-->
         <div class="navPanel" style="display: none" id="nav02">
             <div class="optList" id="optList">
             <div class="optBtn on"><p>员工分润设置</p></div> <div class="optBtn"><p>门店分润设置</p></div>
</div><!--optList-->
      <div class="nav02panel"   >
           <p class="mainTitle">会员卡分润设置</p>  <!--mainTitle-->
          <form></form>
           <form id="vipScheme">
           <div class="listScheme">

</div><!--listScheme-->
 </form>
</div><!--nav02panel-->
     <div class="nav02panel" style="display: none">
             <p class="mainTitle">会员卡分润设置</p>  <!--mainTitle-->
             <form></form>
                      <form id="unitScheme">
                       <div class="listScheme">

            </div><!--listScheme-->
            </form>
</div><!--nav02panel-->
     <div class="commonBtn icon icon_add">添加分润方案</div>
</div> <!-- navPanel-->
         <div class="navPanel" id="nav03"  style="display: none" >
         <form></form>
         <form id="optionFormSet">
         <div class="lineT">
              <div class="mainTitle">使用规则设置</div>
                                      <div class="rowRline">
                                             <div class="linetext"><em class="txt">积分抵扣：</em>每 <input type="text" value="" name="IntegralAmountPer" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"> 积分抵扣1元 </div>

                                  <p><em class="explain txt">0或空代表积分不可用于消费抵扣。</em></p>
                                      </div>
                                      <div class="rowRline">
                                             <div class="linetext"><em class="txt">使用条件：</em>账户积分累计满<input type="text" name="PointsRedeemLowestLimit" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"> 才可使用积分</div>
                                     <p><em class="explain txt">积分数未达到此限制不允许抵扣，0或空代表不限制。</em></p>
                                      </div>
                                      <div class="rowRline">
                                             <div class="linetext"><em class="txt">使用限制： </em>每单可使用积分上限 <input type="text" value="" name="PointsRedeemUpLimit" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"> %  </div>
                                       <p><em class="explain txt">按订单实付金额比例设置</em></p>
                                      </div>
                                      <div class="rowRline">
                                             <div class="linetext"><em class="txt">有效期：</em> 积分有效期<input type="text" name="PointsValidPeriod" value="" class="easyui-numberbox"  data-options="min:1,precision:0,width:100,height:30"> 年</div>
                                             <p><em class="explain txt">例如：积分有效期为2年，则客户2018年度（2018/1/1 - 2018/12/31），累计的所有积分，次年年底（2019/12/31）积分到期。</em></p>
                                      </div>
</div> <!--lineT-->
</form>
  </div> <!--navPanel-->
           <div class="bottomSubmit">
           <div class="commonBtn release"> 发布</div>

</div>  <!--bottomSubmit-->

        </div>  <!--#section-->



        <div style="height: 60px; width: 100%"></div>

       <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">

      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      			</div>
      		</div>

      	</div>
      </div>


        <!--设置开卡礼-->
         <script id="tql_addCouponList" type="text/html">
            <form id="optionForm">

   <div class="coupon">
    <div class="dataGridPanel">
    <p class="optListPanel"><b class="optDom">刷新</b><a class="commonBtn" target="_blank" href="/module/couponManage/addCoupon.aspx?skipType=merber">新建</a></p>
   <div class="panelContent">
    <div id="couponList"></div>
    <div id="dataMessage" class="dataMessage">没有可供选择的优惠券</div>
    </div> <!--panelContent-->

</div> <!--dataGridPanel-->
<div class="optBg"></div>
    <div class="couponList">
    <p class="optListPanel"> <b>已选择</b></p>
    <div class="panelContent"> <div id="couponListSelect">




</div><!--couponListSelect-->
</div> <!--panelContent-->
</div> <!--couponList-->
</div>  <!--coupon-->
                </form>
                </script>

        <!--设置升级条件-->
         <script id="tpl_addUpgrade" type="text/html">
            <form id="optionForm">
   <div class="upgrade">
            <div class="line">
             <div class="col"><div class="radio" data-name="upgrade" data-filed="IsPurchaseUpgrade"><em></em> <span>购卡升级<i>（付费购买会员资格，付费金额不能做余额使用）</i></span></div></div>
              <div class="col" style="padding-left: 34px;"><div class="checkBox" data-filed="IsExtraMoney"><em></em> <span>可补差价</span></div></div>
            <div class="linetext">
            <em class="txt">售卡金额</em>
             <input type="text" name="Prices"  value="" class="easyui-numberbox" id="number1"  data-options="required:true,min:0,precision:0,width:70,height:30, validType: 'nonzero'"/>
                <em >元   或    使用</em>
                 <input type="text" name="ExchangeIntegral"  value="" class="easyui-numberbox"  id="number2" data-options="required:true,min:0,precision:0,width:70,height:30, validType: 'nonzero'"/> 	            积分
             </div><!-- linetext-->
</div> <!--line-->
  <div class="line">
              <div class="col"><div class="radio" data-name="upgrade" data-filed="IsRecharge"><em></em> <span>充值升级<i>（充值到会员卡获得会员资格，充值金额可消费）</i></span></div></div>
            <div class="linetext">
            <em class="txt w102" >单次充值满	</em>
             <input type="text" name="OnceRechargeAmount" value=""  class="easyui-numberbox"  data-options="required:true,min:0,precision:0,width:70,height:30, validType: 'nonzero'"/> 元
             </div><!-- linetext-->
</div> <!--line-->
  <div class="line">
             <div class="col"><div class="radio" data-name="upgrade" data-filed="IsBuyUpgrade"  data-type="radio"><em></em> <span>消费升级<i>（充值到会员卡获得会员资格，充值金额可消费）</i></span></div></div>
           <div class="col" style="padding-left: 34px;"><div class="checkBox" ><em></em> <span>累计消费满<input type="text" name="BuyAmount"  value="" class="easyui-numberbox"  data-options="required:true,min:0,precision:0,width:70,height:30, validType: 'nonzero'"/> 元</span></div></div>
           <div class="col" style="padding:5px 0 0 34px "><div class="checkBox"><em></em> <span>一次性消费满<input type="text" name="OnceBuyAmount" value="" class="easyui-numberbox"  data-options="required:true,min:0,precision:0,width:70,height:30, validType: 'nonzero'"/> 元</span></div></div>
</div> <!--line-->
</div><!--upgrade-->
                </form>
                </script>
         <!--设置基本权益-->
    <script id="tql_addInterests" type="text/html">
               <form id="optionForm"> <div class="lineT">
                                     <div class="mainTitle">
                                     <div class="checkBox" data-name="interests"><em></em> <span> 会员折扣</span></div>
</div>

                                    <div class="rowRline">
                                            <div class="linetext"><em>   会员消费享受</em> <input type="text"   data-filed="VipCardTypeName" name="CardDiscount" class="easyui-numberbox"  data-options="required:true,min:0,max:10,precision:1,width:70,height:30,validType: 'nonzero'" value="">折</div>
                                     </div>
                                     <div class="mainTitle"><div class="checkBox" data-name="interests" data-type="radio"><em></em> <span> 消费返积分</span></div></div>
                                    <div class="rowRline">
                                            <div class="linetext"><div class="radio" data-name="integral"><em></em> <span style="margin:0;"> 消费返积分比例	</span></div> <input type="text" name="PaidGivePercetPoints" class="easyui-numberbox"  data-options="min:0,max:100,precision:0,width:70,height:30,validType: 'nonzero'"  value=""> % <em class="explain">（如设置1%，则每消费1元用户可获得0.01积分）</em></div>
                                          <div class="linetext"><div class="radio" data-name="integral"><em></em> <span style="margin:0;"> 每消费</span></div> <input type="text" name="PaidGivePoints" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30,validType: 'nonzero'"  value="">     元返 1积分 <em class="explain">（如设置10元，则每消费10元用户可获得1积分）</em></div>
                                     </div>
                                     </div>
                                     </form>
</script>
            <!--设置门店-->
        <script id="tpl_setUnitList" type="text/html">
          <div class="optionPanel">
           <div class="contentDiv" style="width: 200px;">
           <p class="explain">选择门店上级组织</p>
           <div class="content">
           <div id="unitParentTree"></div>
  </div><!--content-->
  </div> <!--contentDiv-->
            <div class="contentDiv center">
              <div class="explain"><em>所属组织:</em><span id="unitSelectName"></span>
              <div class="commonBtn searchBtn"> 搜索</div>
                                     <div class="commonSelectWrap r">
                      				   <div class=" searchInput">
                      					 <input  name="unit_name" id="unit_name" type="text" value="" placeholder="请输入店名">
                      				   </div>
                                      </div>


               </div>
                        <div class="content"  >
                         <div id="unitGrid"></div>
                           <div id="UnitDataMessage" class="dataMessage" style="top: 100px;">没有可供选择的门店</div>
               </div><!--content-->
  </div> <!--contentDiv-->
             <div class="contentDiv" style="width: 330px;">
                        <div class="explain"><em>已选择门店</em> <a class="optBtn">批量删除</a></div>
                        <div class="content">
                        <div id="unitTreeSelect"></div>
               </div><!--content-->
  </div> <!--contentDiv-->
  </div>



  </script>
  <!--设置等级-->
        <script id="tpl_addLevel" type="text/html">
         <#if(objLength%2){#>

                           <div class="lumpList " data-level="<#=objLength#>"  data-id="<#=id#>">
         <#}else{#>
                           <div class="lumpList on" data-level="<#=objLength#>"  data-id="<#=id#>">
          <#}#>

                                               <div class="lump"  data-filed="VipCardType">

                                                  <p class="top"> 会员卡 <em class="edit"></em></p>
                                                   <div class="content">
                                                    <div class="commonSelectWrap">
                                                          <label class="searchInput">
                                                             <input data-filed="VipCardTypeName"  placeholder="请输入等级<#=index#>名称"  class="easyui-validatebox" data-options="required:true,validType:'maxLength[30]'" name="VipCardTypeName" type="text" value="">
                                                           </label>
                                                          </div>
                                                          <div class="uploadPanel"  data-filed="PicUrl" data-value="">
                                                          <div class="upLoadBtn">

                          </div><!--upLoadBtn-->
                          <p>点击上传图片</p>
                          </div> <!--uploadPanel-->
                          <p class="text line22 l">* 图片建议尺寸580×290PX</p>
                         <#if(objLength!=1){#>
                          <div class="text line36 l"><div class="checkBox" data-filed="IsPrepaid" ><em></em> <span>是否可充值</span></div></div>
                          <div class="text line36 l"><div class="checkBox" data-filed="IsOnlineSales" ><em></em> <span>是否在线销售</span></div>
                          <p class="text line18 l">（如勾选则用户可以线上购买）</p>
                          </div>
                          <#}#>
                                 </div>  <!--content-->
                                 <div class="releasePanel" style="display:none"><div class="releaseBtn">发布</div></div>
                          </div>  <!--lump-->
                                               <div class="lump" data-type="upgrade"  data-filed="VipCardUpgradeRule">
                                                  <p class="top"> 升级条件 <em class="edit"></em></p>
                                                   <div class="content">
                                                   <#if(objLength==1){#>
                                                            <p class="text line36"><b>完成注册</b></p>
                                                                <p class="text line36"><b>仅短信验证</b>（适用于所有级别）</p>
                                                       <#}else{#>
                                                        <div class="addBtnPanel" data-opttype="upgrade">
                                                                        <div class="btnPanel"><p>新增升级条件</p></div>
                                                       </div> <!--addBtnPanel-->
                                                 <#}#>
                                                  </div>  <!--content-->

                          </div>  <!--lump-->
                                               <div class="lump" data-type="interests"  data-filed="VipCardRule">
                                                  <p class="top"> 基本权益 <em class="edit"></em></p>
                                                       <div class="content">
                                                                                           <div class="addBtnPanel" >
                                                                                           <div class="btnPanel"><p>新增基本权益</p></div>
                                                                   </div> <!--addBtnPanel-->
                                                                     </div>  <!--content-->

                          </div>  <!--lump-->
                                               <div class="lump" data-type="card"  data-filed="VipCardUpgradeRewardList">
                                                  <p class="top"> 开卡礼 <em class="edit"></em></p>
                                                        <div class="content">
                                                                        <div class="addBtnPanel">
                                                                        <div class="btnPanel"><p>新增开卡礼</p></div>
                                                </div> <!--addBtnPanel-->
                                                    </div>  <!--content-->

                          </div>  <!--lump-->
                                      <#if(isDel){#>
                                          <div class="delete"></div>
                                      <#}#>
                          </div>     <!--lumpList-->
          </script>
          <!--设置每一个方块显示的内容-->
          <script id="tpl_setLumpHtml" type="text/html">
              <#if(type=="upgrade"){#>
                 <#if(data["IsPurchaseUpgrade"]==1){#>
                         <# if(data["ExchangeIntegral"]&&data["Prices"]){#>
                             <p class="tag"><em class="icon gk"></em> <span class="line22">购卡金额<i><#=data["Prices"]#></i>元或 <br>使用<i><#=data["ExchangeIntegral"]#></i>积分</span> </p>
                         <#}else if(data["Prices"]){ #>
                             <p class="tag"><em class="icon gk"></em> <span>购卡金额<i><#=data["Prices"]#></i>元</span> </p>
                         <#}else if(data["ExchangeIntegral"]){ #>
                             <p class="tag"><em class="icon gk"></em> <span>使用<i><#=data["ExchangeIntegral"]#></i>积分</span> </p>
                         <#}#>
                <#}#>
                  <#if(data["IsRecharge"]==1){#>
                  <p class="tag"><em class="icon cz"></em> <span>单次充值满<i><#=data["OnceRechargeAmount"]#></i>元</span> </p>
                 <#}#>
                 <#if(data["IsBuyUpgrade"]==1){#>
                         <# if(data["BuyAmount"]&&data["OnceBuyAmount"]){#>
                             <p class="tag"><em class="icon xf"></em> <span class="line22">累计消费<i><#=data["BuyAmount"]#></i>元或 <br>一次性消费<i><#=data["OnceBuyAmount"]#></i>元</span> </p>
                         <#}else if(data["BuyAmount"]){ #>
                             <p class="tag"><em class="icon xf"></em> <span>累计消费<i><#=data["BuyAmount"]#></i>元</span> </p>
                         <#}else if(data["OnceBuyAmount"]){ #>
                             <p class="tag"><em class="icon xf"></em> <span>一次性消费<i><#=data["OnceBuyAmount"]#></i>元</span> </p>
                         <#}#>
                <#}#>

              <#}else if(type=="interests"){#>
                 <#if(data["CardDiscount"]||data["PaidGivePercetPoints"]||data["PaidGivePoints"]){#>
                    <#if(data["CardDiscount"]){#>
                       <#if(data["CardDiscount"]==10){#>
                              <p class="tag"><em class="icon"></em> <span>会员消费不享受折扣</span> </p>
                       <#}else{#>
                          <p class="tag"><em class="icon"></em> <span>会员消费享受<i><#=data["CardDiscount"]#></i>折</span> </p>
                       <#}#>


                                                   <p class="text line18">充值升级：会员折扣仅限使用卡内余额时享受；</p>
                                                   <p class="text line18">购卡升级或消费升级：持卡就可享受会员折扣。</p>
                     <#}#>

                         <# if(data["PaidGivePercetPoints"]){#>
                            <p class="tag"><em class="icon bili"></em> <span>消费返积分比例<i><#=data["PaidGivePercetPoints"]#></i>%</span> </p>
                            <p class="text line18">如设置1%，则每消费1元用户可获得0.01积分</p>
                         <#}else if(data["PaidGivePoints"]){ #>
                            <p class="tag"><em class="icon bili"></em> <span>每消费<#=data["PaidGivePoints"]#></> 元返 1积分<i></span> </p>
                            <p class="text line18">如设置10元，则每消费10元用户可获得1积分</p>
                         <#}#>

                  <#}else{#>
                   <div class="addBtnPanel" >
                           <div class="btnPanel"><p>新增基本权益</p></div>
                     </div> <!--addBtnPanel-->

                    <#}#>
              <#}else if(type=="card"){#>
                 <#if(data.length>0){#>
                  <#for(var i=0;i<data.length; i++){ var item=data[i],CouponName=item.CouponTypeName ? item.CouponTypeName : item.CouponName;#>
                        <#if(CouponName.length>10){#>
                          <p class="tag"><em class="icon yhq"></em> <span class="shou" title="<#=CouponName#>"><#=CouponName.substring(0,10)+"..."#></span> </p>
                        <#}else{#>
                         <p class="tag"><em class="icon yhq"></em> <span title=""><#=CouponName#></span> </p>
                            <#}#>
                                                <p class="text line18"><#=item.ValidityPeriod#> <i>×<#=item['CouponNum']#></i></p>

                  <#}#>

                 <#}else{#>
                  <div class="addBtnPanel" >
                                             <div class="btnPanel"><p>新增开卡礼</p></div>
                                       </div> <!--addBtnPanel-->
                   <#}#>
             <#}else if(type=="cardInfo"){#>

              <#}#>
</script>
<!--优惠券添加-->
<script id="tpl_addCoupon"  type="text/html">
<#for(var i=0;i<list.length; i++){ var item=list[i],itemJSON=JSON.stringify(item),CouponName=item.CouponTypeName ? item.CouponTypeName : item.CouponName;var CouponNum=item.CouponNum ? item.CouponNum :1; #>
    <div class="lineCouponInfo" data-couponid="<#=item.CouponTypeID#>"  data-item="<#=itemJSON#>">
    <#if(CouponName.length>12){ #>
         <p class="shou" title="<#=CouponName#>"><i><#=i+1+length#></i>、<#=CouponName.substring(0,12)#> ...     <b><#=item.ValidityPeriod#> </b></p>
    <# }else{ #>
         <p><i><#=i+1+length#></i>、<#=CouponName#>      <b><#=item.ValidityPeriod#> </b></p>
    <# }#>

    <p>赠送数量： <input type="text" name="CouponNum" class="easyui-numberbox"  data-options="min:1,precision:0,width:70,height:30"  value="<#=CouponNum#>">张</p>
    <em class="del">删除</em>
    </div>

  <#}#>
</script>

<script id="tpl_addSchemeList" type="text/html">
<#if(data){var unitList=JSON.stringify(data.RuleUnitInfoList),ruleList=JSON.stringify(data.VipCardReRechargeProfitRuleList);#>
 <#if(unitList=="null"){#>
                         <div class="scheme" data-type="<#=data.ProfitOwner#>" >
                 <#}else{#>
  <div class="scheme"  data-type="<#=data.ProfitOwner#>" data-cache="<#=unitList#>" data-unitlist="<#=unitList#>">
                 <#}#>

                   <div class="title">
                     <div class="commonSelectWrap l">
                           <em class="tit  w100">选择会员卡：</em>
                                  <label class="searchInput borderNone">
                            <input data-text="选择会员卡" data-filed="VipCardTypeID" class="easyui-combobox" name="VipCardTypeID" data-options="width:125,height:30"   type="text" >
                         </label>
                      </div> <!--commonSelectWrap-->
                       <p class="r del" data-filed="CardBuyToProfitRuleId" data-value="<#=data.CardBuyToProfitRuleId#>" > 删除分润方案</p>
                   </div> <!--title-->
                 <div class="commonSelectWrap">
                                                    <em class="tit w100">选择适用门店：</em>
                                                      <div class="searchInput bordernone whauto">
                                                            <div class="radio on" data-name="unit" data-filed="IsApplyAllUnits"  data-valuetype="all"><em></em><span class="w100">全部门店</span></div>
                                                            <div class="radio" data-name="unit" data-valuetype="portion"><em></em><span class="w100">部分门店</span></div>
                                                             <div class="radioBtn r" data-name="unit">查看及修改</div>
                                                      </div>
                                              </div> <!--commonSelectWrap-->
                 <div class="commonSelectWrap" data-name="RefillCondition" data-value="1" style="display: none">
                                                    <em class="tit w100">首次售卡分润：</em>
                                                      <div class="searchInput borderNone">
                                                              <input data-text="首次售卡分润" data-filed="FirstCardSalesProfitPct" class="easyui-numberbox" name="FirstCardSalesProfitPct" data-options="width:100,height:30,min:0,max:100,precision:1"   type="text" value="<#=data.FirstCardSalesProfitPct#>"/>  %
                                                      </div>
                                              </div> <!--commonSelectWrap-->
                                              <p data-name="RefillCondition" data-value="1" style="display: none" class="explain">首次分润：按照购卡/充值金额进行提成。 </p>
                 <div class="commonSelectWrap" data-name="RefillCondition" data-value="0" style="display: none">
                                                    <em class="tit w100">首次充值分润：</em>
                                                      <div class="searchInput borderNone">
                                                              <input data-text="首次充值分润" data-filed="FirstRechargeProfitPct" class="easyui-numberbox" name="FirstRechargeProfitPct" data-options="width:100,height:30,min:0,max:100,precision:1"   type="text" value="<#=data.FirstRechargeProfitPct#>"/>  %
                                                      </div>
                                              </div> <!--commonSelectWrap-->
                                              <p class="explain" data-name="RefillCondition" data-value="0" style="display: none">首次分润：按照购卡/充值金额进行提成。 </p>
                 <#if(ruleList=="null"){#>
                         <div class="commonSelectWrap teShu" style="display: none" data-name="IsPrepaid"  data-value="1" >
                 <#}else{#>
                    <div class="commonSelectWrap teShu" style="display: none" data-name="IsPrepaid" data-rulelist="<#=ruleList#>" data-value="1" >
                 <#}#>


                                                    <em class="tit w100">续费充值分润：</em>
                                                      <div class="searchInput borderNone">
                                                              <input data-text="续费充值分润" data-filed="ProfitType" class="easyui-combobox" name="ProfitType" data-options="width:120,height:30"   type="text" value=""/>

                                                      </div> <!--searchInput-->
                                                            <div class="lineList" data-name="Step" style="display: none">
                                                              <div class="linetext" >
                                                                          <em class="txt">充值满</em>
                                                                           <input type="text" data-filed="LimitAmount" name="LimitAmount"  value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
                                                                              <em >获得</em>
                                                                               <input type="text" data-filed="ProfitPct" name="ProfitPct" value="" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
                                                                             <div class="radioBtn r" data-name="del">删除</div>      <div class="radioBtn r" data-name="add">添加</div>
                                                                           </div><!-- linetext-->
                                                             </div><!--lineList-->


                                                             <div class="lineList" data-name="Superposition">
                                                               <div class="linetext" >
                                                                           <em class="txt">每充值满</em>
                                                                            <input type="text" data-filed="LimitAmount" name="LimitAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
                                                                               <em >获得</em>
                                                                                <input type="text"  data-filed="ProfitPct" name="ProfitPct" value="" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
                                                                              <div class="radioBtn r" data-name="del">删除</div>
                                                                            </div><!-- linetext-->
                                                              </div><!--lineList-->
                                              </div> <!--commonSelectWrap-->
                                              <p class="explain" style="display: none;position: absolute;" data-name="IsPrepaid"  data-value="1" >
                                              <i data-name="Superposition" >叠加分润：按照充值金额进行固定比例提成。</i><i data-name="Step" style="display: none">梯度分润：充不同固定金额，享受不同的提成比例。</i> </p>
 </div><!--scheme-->

<#}else{ #>

   <div class="scheme"  data-type="<#=type#>">
                     <div class="title">
                       <div class="commonSelectWrap l">
                             <em class="tit  w100">选择会员卡：</em>
                                    <label class="searchInput borderNone">
                              <input data-text="选择会员卡" data-filed="VipCardTypeID" class="easyui-combobox" name="VipCardTypeID" data-options="width:125,height:30"   type="text" value="">
                           </label>
                        </div> <!--commonSelectWrap-->
                         <p class="r del" data-filed="CardBuyToProfitRuleId" data-value="" > 删除分润方案</p>
                     </div> <!--title-->
                   <div class="commonSelectWrap">
                                                      <em class="tit w100">选择适用门店：</em>
                                                        <div class="searchInput bordernone whauto">
                                                              <div class="radio on" data-name="unit" data-filed="IsApplyAllUnits"  data-valuetype="all"><em></em><span class="w100">全部门店</span></div>
                                                              <div class="radio" data-name="unit" data-valuetype="portion"><em></em><span class="w100">部分门店</span></div>
                                                               <div class="radioBtn r" data-name="unit">查看及修改</div>
                                                        </div>
                                                </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap" data-name="RefillCondition" data-value="1" style="display: none">
                                                      <em class="tit w100">首次售卡分润：</em>
                                                        <div class="searchInput borderNone">
                                                                <input data-text="首次售卡分润" data-filed="FirstCardSalesProfitPct" class="easyui-numberbox" name="FirstCardSalesProfitPct" data-options="width:100,height:30,min:0,max:100,precision:1"   type="text" value=""/>  %
                                                        </div>
                                                </div> <!--commonSelectWrap-->
                                                <p data-name="RefillCondition" data-value="1" style="display: none" class="explain">首次分润：按照购卡/充值金额进行提成。 </p>
                   <div class="commonSelectWrap" data-name="RefillCondition" data-value="0" style="display: none">
                                                      <em class="tit w100">首次充值分润：</em>
                                                        <div class="searchInput borderNone">
                                                                <input data-text="首次充值分润" data-filed="FirstRechargeProfitPct" class="easyui-numberbox" name="FirstRechargeProfitPct" data-options="width:100,height:30,min:0,max:100,precision:1"   type="text" value=""/>  %
                                                        </div>
                                                </div> <!--commonSelectWrap-->
                                                <p class="explain" data-name="RefillCondition" data-value="0" style="display: none">首次分润：按照购卡/充值金额进行提成。 </p>
                   <div class="commonSelectWrap teShu" style="display: none" data-name="IsPrepaid"  data-value="1" >

                                                      <em class="tit w100">续费充值分润：</em>
                                                        <div class="searchInput borderNone">
                                                                <input data-text="续费充值分润" data-filed="ProfitType" class="easyui-combobox" name="ProfitType" data-options="width:120,height:30"   type="text" value=""/>

                                                        </div> <!--searchInput-->
                                                              <div class="lineList" data-name="Step" style="display: none">
                                                                <div class="linetext" >
                                                                            <em class="txt">充值满</em>
                                                                             <input type="text" data-filed="LimitAmount" name="LimitAmount"  value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
                                                                                <em >获得</em>
                                                                                 <input type="text" data-filed="ProfitPct" name="ProfitPct" value="" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
                                                                               <div class="radioBtn r" data-name="del">删除</div>      <div class="radioBtn r" data-name="add">添加</div>
                                                                             </div><!-- linetext-->
                                                               </div><!--lineList-->
                                                               <div class="lineList" data-name="Superposition">
                                                                 <div class="linetext" >
                                                                             <em class="txt">每充值满</em>
                                                                              <input type="text" data-filed="LimitAmount" name="LimitAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
                                                                                 <em >获得</em>
                                                                                  <input type="text"  data-filed="ProfitPct" name="ProfitPct" value="" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
                                                                                <div class="radioBtn r" data-name="del">删除</div>
                                                                              </div><!-- linetext-->
                                                                </div><!--lineList-->
                                                </div> <!--commonSelectWrap-->
                                                <p class="explain" style="display: none;position: absolute;" data-name="IsPrepaid"  data-value="1" >
                                                 <i data-name="Superposition" >叠加分润：按照充值金额进行固定比例提成。</i> <i data-name="Step" style="display: none">梯度分润：充不同固定金额，享受不同的提成比例。</i>  </p>
   </div><!--scheme-->


<#}#>

</script>
<script id="add_lineList" type="text/html">
<#if(type=="Step"){#>
  <div class="linetext" >
       <em class="txt">充值满</em>
       <input type="text" data-filed="LimitAmount" name="LimitAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
       <em >获得</em>
       <input type="text" data-filed="ProfitPct" name="ProfitPct" value="" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
      <div class="radioBtn r" data-name="del">删除</div>   <div class="radioBtn r" data-name="add">添加</div>
  </div><!-- linetext-->
 <#}else{#>
   <div class="linetext" >
        <em class="txt">每充值满</em>
        <input type="text" data-filed="LimitAmount" name="LimitAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
        <em >获得</em>
        <input type="text" data-filed="ProfitPct" name="ProfitPct" value="" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
       <div class="radioBtn r" data-name="del">删除</div>
   </div><!-- linetext-->
 <#}#>

</script>

 <script id="add_lineListData" type="text/html">
 <#if(type=="Step"){#>
   <div class="linetext" data-id="<#=ReRechargeProfitRuleId#>">
        <em class="txt">充值满</em>
        <input type="text" data-filed="LimitAmount" value="<#=LimitAmount#>" name="LimitAmount" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
        <em >获得</em>
        <input type="text" data-filed="ProfitPct" name="ProfitPct" value="<#=ProfitPct#>" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
       <div class="radioBtn r" data-name="del">删除</div>        <div class="radioBtn r" data-name="add">添加</div>
   </div><!-- linetext-->
  <#}else{#>
    <div class="linetext" data-id="<#=ReRechargeProfitRuleId#>">
         <em class="txt">每充值满</em>
         <input type="text" data-filed="LimitAmount" name="LimitAmount" value="<#=LimitAmount#>"  value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:100,height:30"/>
         <em >获得</em>
         <input type="text" data-filed="ProfitPct" name="ProfitPct" value="<#=ProfitPct#>" class="easyui-numberbox"  data-options="min:0,precision:1,max:100,width:100,height:30"/> % 分润
        <div class="radioBtn r" data-name="del">删除</div>
    </div><!-- linetext-->
  <#}#>

 </script>
</asp:Content>
