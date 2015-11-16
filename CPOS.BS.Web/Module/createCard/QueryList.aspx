<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>开卡</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/createCard/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/queryList.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                        <div class="item">
                          <form></form>
                          <form id="loadForm">
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">卡号：</em>
                                                      <label class="selectBox" >
                                                          <input class="easyui-validatebox" name="VipCardCode" data-options="disabled:true">
                                                      </label>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">卡内码：</em>
                                                      <div class="selectBox">
                                                                <input class="easyui-validatebox" name="VipCardISN" data-options="disabled:true">
                                                      </div>
                                                  </div>


                                                  <div class="commonSelectWrap">
                                                      <em class="tit">开卡门店：</em>
                                                      <div class="selectBox">
                                                                 <input class="easyui-validatebox" name="MembershipUnit" data-options="disabled:true">
                                                      </div>
                                                  </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">卡类型：</em>
                                                      <div class="selectBox">

                                                     <input class="easyui-validatebox" name="VipCardName" data-options="disabled:true">

                                                      </div>
                                                  </div>
                                                   <div class="commonSelectWrap">
                                                       <em class="tit">卡状态：</em>
                                                       <div class="selectBox">

                                                      <input class="easyui-validatebox" name="VipCardStatusId" data-options="disabled:true">

                                                       </div>
                                                   </div>
                                                  <div class="commonSelectWrap">
                                                      <em class="tit">卡有效期：</em>
                                                      <div class="selectBox">

                                                     <!--  <input class="easyui-validatebox" name="BeginDate" style="width:43%" data-options="disabled:true"><span style="width: 14%;text-align: center; line-height: 32px; float: left;">至</span>-->
                                                        <input class="easyui-validatebox" name="EndDate" data-options="disabled:true">
                                                      </div>
                                                  </div>

                                                  </form>

                        </div>

                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>  <form id="createVipCard">
                <div class="submitPanel" >

                 <div class="l">
                 <div class="commonSelectWrap">
                 <em class="tit">会员姓名:</em>
                 <div class="searchInput"><input name="VipName"  class="easyui-validatebox" data-options="required:true"/> </div>
                 </div>

                  <div class="commonSelectWrap">
                                  <em class="tit">生日:</em>
                                  <div class="selectBox"><input name="Birthday"  id="birthday" class="easyui-datebox" data-options="required:true,value:'2015-01-01',width:260,height:32,validType:['dateCode']"/> </div>
                   </div>
                     <div class="commonSelectWrap">
                                                     <em class="tit">性别:</em>
                                                     <div class="selectBox">
                                                     <div class="radio" data-name="sex" data-sex="1"><em></em> <span>男</span></div>
                                                      <div class="radio" data-name="sex" data-sex="2"><em></em> <span>女</span></div>
                                                     </div>
                      </div>
                  <div class="commonSelectWrap">
                                  <em class="tit">身份证号:</em>
                                  <div class="searchInput"><input  name="IDCard" class="easyui-validatebox" data-options="width:260,height:32,validType:'idCardNo'"/> </div>
                   </div>
       <!--           <div class="commonSelectWrap Address " style="display: none">
                                  <em class="tit">所在地:</em>
                                  <div class="selectBox">
                            <input id="province" name="province" class="easyui-combobox " data-options="width:80,height:32,required:true" />
                            <input id="city" name="city" class="easyui-combobox" data-options="width:80,height:32,required:true" />
                            <input id="area" class="easyui-combobox" name="CityID" data-options="width:80,height:32,required:true" />
                                  </div>

                   </div>-->

                 </div><!-- class="l"-->
                  <div class="r">
                  <div class="commonSelectWrap">
                                  <em class="tit">手机号码</em>
                                  <div class="searchInput"><input name="Phone" class="easyui-validatebox" data-options="required:true,width:260,height:32,validType:'mobile'"/> </div>
                   </div>
                   <!--<div class="commonSelectWrap">
                                   <em class="tit">年龄段:</em>
                                   <div class="selectBox"><input name="TagsID" id="ageGroup"  class="easyui-combobox" data-options="width:260,height:32,editable:false"/> </div>
                    </div>-->
                  <div class="commonSelectWrap">
                                  <em class="tit">售卡员工:</em>
                                  <!-- id="vipName" -->
                                  <div class="searchInput"><input  name="SalesUserName" class="easyui-validatebox" data-options="width:260,height:32,required:true,validType:'maxLength[10]'"/> </div>
                   </div>
                <!--  <div class="commonSelectWrap" style="display: none">
                                  <em class="tit">E-mail:</em>
                                  <div class="searchInput"><input name="Email"  class="easyui-validatebox" data-options="width:260,height:32,validType:'email'"/> </div>
                   </div>-->
                   <!--<div class="commonSelectWrap " style="display: none">
                                   <em class="tit">详细地址:</em>
                                   <div class="searchInput"><input name="详细地址"  class="easyui-validatebox" data-options="width:260,height:32"/> </div>
                    </div>-->
                  <div class="commonSelectWrap">
                                  <em class="tit">售卡类型:</em>
                                  <div class="selectBox">
                                  <div class="checkBox" data-name="IsGift"><em></em><span>赠送</span></div>
                                 <!-- <div class="searchInput" style="width:170px; float: left;display: none">
                                   <input  class="easyui-validatebox" placeholder="请输入赠卡人姓名或工号"/>
                                  </div>-->
                                  </div>
                   </div>

                  </div> <!-- class="r"-->
                   <div class="zsy"> </div>

                </div>  <!--submitPanel-->
   </form>
                <div class="commonBtn optBtn">保存</div>
        </div>
        <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,collapsible:false,minimizable:false,maximizable:false,
      title:'开通会员卡',width:636,height:320,left:($(window).width()-636)/2,top:($(window).height()-320)/2" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding-top:62px;">
      			  <form id="payOrder">
                           <div class="commonSelectWrap">
                                 <em class="tit" style="width:167px">卡号：</em>
                                <div class="searchInput" style="width:306px">
                                   <input type="text" class="easyui-validatebox" name="VipCardISN" id="VipCardISN" data-options="required:true" placeholder="请刷卡/输入卡号" />
                               </div>
                           </div>

                           </form>
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:5px 0 0;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onclick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>
       <!-- 取消订单-->
       <script id="tpl_slotCardQuery" type="text/html">

       </script>
        <!--收款-->



       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
