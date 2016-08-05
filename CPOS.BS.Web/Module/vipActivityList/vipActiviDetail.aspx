<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员活动设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <link href="<%=StaticUrl+"/module/vipActivityList/css/vipActiviDetail.css?v=0.1"%>" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/vipActiviDetail.js">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block;">
                <div class="panelList">
                    <div class="title">
                        <ul id="optPanel">
                            <li data-flag="#nav01" class="on one"><span><em>1</em> 基础信息</span></li>
                            <li data-flag="#nav02"><span><em>2</em>活动内容</span></li>
                            <li data-flag="#nav03" class="borderNone"><span><em>3</em>消息</span></li>
                        </ul>
                    </div>
                    <form>
                    </form>
                    <form id="nav0_1">
                    <div class="panelDiv" id="nav01" data-index="0">
                        <div class="line">
                          <div class="commonSelectWrap">
                               <em class="tit">活动类型：</em>

                                                            <div  class="bordernone searchInput whauto">
                                                                 <div class="radio"  data-name="ActivityType" data-value="1"><em></em><span>生日关怀</span></div>
                                                                   <div class="radio on" data-name="ActivityType" data-value="3"><em></em><span>充值满赠</span></div>
                                                                    <div class="radio"   data-name="ActivityType" data-value="2"><em></em><span>营销活动</span></div>
                                                            </div>
                                                        </div>
                                <div class="commonSelectWrap">
                                    <em class="tit">活动名称：</em>
                                    <label class="searchInput">
                                        <input class="easyui-validatebox"  data-options="required:true,validType:['stringCheck','maxLength[30]']" name="ActivityName"
                                            type="text" value="" placeholder="请输入">
                                    </label>
                                </div>
                                <div class="commonSelectWrap">
                                    <em class="tit">活动时间：</em>
                                    <div class="bordernone searchInput whauto">
                                        <input type="text" id="BeginDate" name="StartTime" class="easyui-datebox" data-options="required:true,width:120,height:30"
                                            placeholder="请选择" />
                                        <em class="text">至 </em>
                                        <input type="text" id="name" data-name="IsLongTime" name="EndTime" class="easyui-datebox"
                                            data-options="required:true,width:120,height:30" placeholder="请选择" validtype="compareEqualityDate[$('#BeginDate').datebox('getText')]" />
                                    </div>
                                    <div class="checkBox" data-filed="IsLongTime" data-name="datebox">
                                        <em></em><span>长期</span></div>
                                </div>

                                <div class="commonSelectWrap">
                                  <div class="tit">  目标人群： </div>
                                     <div class="checkBox" style="margin-left: 0" data-filed="IsAllCardType" data-type="all" data-name="IsAllCardType"> <em></em><span>全部会员</span></div>
                                   <div class="bordernone searchInput whauto" id="levelList">

                                     </div> <!--levelList-->
                                    <p id="vipCount" class="textExt">全部持卡人数：0人</p>
                                 </div>


                         </div> <!--line-->
                    </div><!--panelDiv-->
                    </form>
                    <!--奖品明细-->
                   <form></form>
                   <form id="nav0_2">
                    <div class="panelDiv" id="nav02" data-index="1">
                        <div class="tagPanel" style="display: block">
                                <div class="commonSelectWrap" data-name="IsPrepaid"  data-value="1" >

                                                      <em class="tit w100">活动种类：</em>
                                                        <div class="searchInput borderNone" style="width: 160px;">
                                                                <input data-text="活动种类" id="ProfitType" class="easyui-combobox" name="ProfitType" data-options="width:120,height:30"   type="text" value=""/>

                                                        </div> <!--searchInput-->
                                                         <p class="ext">梯度：充不同金额，获赠不同金额回馈。</p>
                                                         <div class="lineList" data-name="Step" style="display: none">
                                                                                                                        <div class="linetext" >
                                                                                                                                    <em class="txt">充值满</em>
                                                                                                                                     <input type="text" data-filed="RechargeAmount" name="RechargeAmount"  value="" class="easyui-numberbox"  data-options="min:0, required: true,precision:0,width:70,height:30"/>
                                                                                                                                        <em >赠</em>
                                                                                                                                         <input type="text" data-filed="GiftAmount" name="GiftAmount" value="" class="easyui-numberbox"  data-options="min:0, required: true,precision:0,width:70,height:30"/> 元
                                                                                                                                        <div class="radioBtn r" data-name="add"> &nbsp; &nbsp; &nbsp; &nbsp;</div>    <div class="radioBtn r" data-name="add">添加</div>
                                                                                                                                     </div><!-- linetext-->
                                                                                                                       </div><!--lineList-->
                                                                                                                       <div class="lineList" data-name="Superposition">
                                                                                                                         <div class="linetext" >
                                                                                                                                     <em class="txt">每充值满</em>
                                                                                                                                      <input type="text" data-filed="RechargeAmount" name="RechargeAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:70, required: true,height:30"/>
                                                                                                                                         <em >赠</em>
                                                                                                                                          <input type="text"  data-filed="GiftAmount" name="GiftAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:70, required: true,height:30"/> 元

                                                                                                                                      </div><!-- linetext-->
                                                                                                                        </div><!--lineList-->
                                                </div> <!--commonSelectWrap-->

                        </div>
                        <!--tagPanel-->
                        <div class="tagPanel">
                            <div class="checkBoxPanel">
                                <div class="inputDiv">

                                   <span>赠送礼券：</span>
                                    <div class="optPanel optSelect">
                                        <div class="commonBtn" data-prizestype="1" data-prizesid="">选择优惠券</div>
                                        <em class="hint">持卡人数：10,000 人</em>
                                    </div>
                                </div>
                                <!--inputDiv-->
                            </div>
                            <!--checkBoxPanel-->
                            <div class="tablePanel optSelect">
                                <div class="gridTable">

                                </div>
                            </div>
                            <!--tablePanl-->
                        </div>
                        <!--tagPanel-->
                    </div>
                   </form>
                    <!--panelDiv-->


                    <div class="panelDiv" id="nav03" data-index="2">
                      <form></form>
                       <form id="nav0_3">
                         <div class="lineTitle">
                                    <div class="checkBoxPanel">
                                         <div class="inputDiv">
                                                <div class="checkBox on" data-filed="WeChat" data-type="add"  data-name="optSelect" style="margin-top:8px;">
                                                 <em style="display: none"></em>  <span>微信设置</span></div>
                                            </div>
                                            <!--inputDiv-->
                                     </div>
                            </div>   <!--lineTitle-->
                           <div class="tagPanel borderNone">

                                                   <!--checkBoxPanel-->
                                     <div id="WeChat" class="optSelect" style="display: none" >
                                                             <div class="templatePanel " data-message="">
                                                                      <div class="panelSel">
                                                                                                 <div class="commonSelectWrap">
                                                                                                     <em class="tit">生日前</em>
                                                                                                     <div class="selectBox">
                                                                                                         <input class="easyui-combobox" data-name="day" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                                                                            valueField: 'value',
                                                                                            textField: 'label',
                                                                                            onSelect:onSelectDay,
                                                                                            data: [{
                                                                                                     label: '1天',
                                                                                                     value: '1'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '2天',
                                                                                                     value: '2'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '3天',
                                                                                                     value: '3'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '4天',
                                                                                                     value: '4'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '5天',
                                                                                                     value: '5'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '6天',
                                                                                                     value: '6'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '7天',
                                                                                                     value: '7'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '8天',
                                                                                                     value: '8'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '9天',
                                                                                                     value: '9'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '10天',
                                                                                                     value: '10'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '15天',
                                                                                                     value: '15'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '30天',
                                                                                                     value: '30'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '请选择',
                                                                                                     value: '0',
                                                                                                     selected:true
                                                                                                  }]" />
                                                                                                         <input class="easyui-combobox" data-name="hour" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                                                                            valueField: 'value',
                                                                                            textField: 'label',
                                                                                            onSelect:onSelectHour,
                                                                                            data: [
                                                                                            {label: '1时',value: '01'},{label: '2时',value: '02'},{label: '3时',value: '03'},{label: '4时',value: '04'},{label: '5时',value: '05'},{label: '6时',value: '06'},
                                                                                            {label: '7时',value: '07'},{label: '8时',value: '08'},{label: '9时',value: '09'},{label: '10时',value: '10'},{label: '11时',value: '11'},{label: '12时',value: '12'},
                                                                                            {label: '13时',value: '13'},{label: '14时',value: '14'},{label: '15时',value: '15'},{label: '16时',value: '16'},{label: '17时',value: '17'},{label: '18时',value: '18'},
                                                                                            {label: '19时',value: '19'},{label: '20时',value: '20'},{label: '21时',value: '21'},{label: '22时',value: '22'},{label: '23时',value: '23'},{label: '24时',value: '00'},
                                                                                            {label: '请选择',value: '0',selected:true}]" />
                                                                                                    <i class="cursorDiv">送券</i>
                                                                                                     </div>
                                                                                                     <!--selectBox-->
                                                                                                 </div>
                                                                                                 <!--commonSelectWrap-->

                                                                                                 <div class="commonBtn" style="display: none">
                                                                                                     选择模板</div>
                                                                                                 <div class="text">
                                                                                                     <textarea></textarea>
                                                                                                 </div>
                                                                                             </div>
                                                              </div>

                                                   <div class="textAddBtn" data-flag="WeChat" style="display: none">
                                                       添加</div>
                                       </div> <!--optSelect-->
                                               </div> <!--tagPanel-->
                        <div class="lineTitle">
                            <div class="checkBoxPanel">
                                                        <div class="inputDiv">
                                                            <div class="checkBox" data-filed="SMS" data-type="add"  data-name="optSelect"  style="margin-top:8px;">
                                                                <em></em><span>短信通知 （每次发送不能少于100个手机号）</span></div>
                                                        </div>
                                                        <!--inputDiv-->
                                                    </div>
                                                    <!--checkBoxPanel-->
                           </div> <!--lineTitle-->
                        <div class="tagPanel borderNone">


                            <div id="SMS" class="optSelect" style="display: none">
                                       <div class="templatePanel " data-message="">
                                                <div class="panelSel">
                                                                           <div class="commonSelectWrap">
                                                                               <em class="tit">生日前</em>
                                                                               <div class="selectBox">
                                                                                   <input class="easyui-combobox" data-name="day" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                                                      valueField: 'value',
                                                                      textField: 'label',
                                                                      onSelect:onSelectDay,
                                                                      data: [{
                                                                               label: '1天',
                                                                               value: '1'
                                                                            },
                                                                            {
                                                                               label: '2天',
                                                                               value: '2'
                                                                            },
                                                                            {
                                                                               label: '3天',
                                                                               value: '3'
                                                                            },

                                                                                                  {
                                                                                                     label: '4天',
                                                                                                     value: '4'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '5天',
                                                                                                     value: '5'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '6天',
                                                                                                     value: '6'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '7天',
                                                                                                     value: '7'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '8天',
                                                                                                     value: '8'
                                                                                                  },
                                                                                                  {
                                                                                                     label: '9天',
                                                                                                     value: '9'
                                                                                                  },
                                                                            {
                                                                               label: '10天',
                                                                               value: '10'
                                                                            },
                                                                            {
                                                                               label: '15天',
                                                                               value: '15'
                                                                            },
                                                                            {
                                                                               label: '30天',
                                                                               value: '30'
                                                                            },
                                                                            {
                                                                               label: '请选择',
                                                                               value: '0',
                                                                               selected:true
                                                                            }]" />
                                                                                   <input class="easyui-combobox" data-name="hour" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                                                      valueField: 'value',
                                                                      textField: 'label',
                                                                      onSelect:onSelectHour,
                                                                      data: [
                                                                      {label: '1时',value: '01'},{label: '2时',value: '02'},{label: '3时',value: '03'},{label: '4时',value: '04'},{label: '5时',value: '05'},{label: '6时',value: '06'},
                                                                      {label: '7时',value: '07'},{label: '8时',value: '08'},{label: '9时',value: '09'},{label: '10时',value: '10'},{label: '11时',value: '11'},{label: '12时',value: '12'},
                                                                      {label: '13时',value: '13'},{label: '14时',value: '14'},{label: '15时',value: '15'},{label: '16时',value: '16'},{label: '17时',value: '17'},{label: '18时',value: '18'},
                                                                      {label: '19时',value: '19'},{label: '20时',value: '20'},{label: '21时',value: '21'},{label: '22时',value: '22'},{label: '23时',value: '23'},{label: '24时',value: '00'},
                                                                      {label: '请选择',value: '0',selected:true}]" />
                                                                               </div>
                                                                               <!--selectBox-->
                                                                           </div>
                                                                           <!--commonSelectWrap-->
                                                                           <div class="commonBtn" style="display:none " >
                                                                               选择模板</div>
                                                                           <div class="text">
                                                                               <textarea></textarea>
                                                                           </div>
                                                                       </div>
                                        </div>
                            <div class="textAddBtn" data-flag="SMS"> 添加</div>
                            </div><!--SMS-->

                        </div><!--tagPanel-->

                    </form>
                    </div>

                    <!--panelDiv-->
                </div>

                <!--panelList-->
                <div class="zsy">
                </div>

                <div class="openPanelDiv">
                <div class="btnopt" data-falg="nav01">
                    <div class=" commonBtn bgWhite prevStepBtn" data-flag="#nav01" style="float:left;">
                        上一步</div>
                    <div class=" commonBtn nextStepBtn" id="submitBtn" data-submitindex="1" data-flag="#nav02" style="float:left;">
                        下一步</div>
                </div>
                </div>
            </div>
        </div>
        <div style="display: none">
            <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
                    <div data-options="region:'center'" >
                        指定的模板添加内容
                    </div>
                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                        text-align: center; padding: 5px 0 0; margin-top: 18px; border-bottom:none;">
                         <a class="easyui-linkbutton commonBtn cancelBtn"
                            href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                            <a class="easyui-linkbutton commonBtn saveBtn">确定</a>
                    </div>
                </div>
            </div>
        </div>

         <script id="add_lineListData" type="text/html">
         <#if(type=="Step"){#>
           <div class="linetext" data-id="<#=RechargeStrategyId#>">
                <em class="txt">充值满</em>
                <input type="text" data-filed="RechargeAmount" value="<#=RechargeAmount#>" name="RechargeAmount" class="easyui-numberbox"  data-options="min:0,precision:0,required:true,width:70,height:30"/>
                <em >赠</em>
                <input type="text" data-filed="GiftAmount" name="GiftAmount" value="<#=GiftAmount#>" class="easyui-numberbox"  data-options="min:0,precision:0,required:true,width:70,height:30"/> 元
              <div class="radioBtn r" data-name="del">删除</div>      <div class="radioBtn r" data-name="add">添加</div>
           </div><!-- linetext-->
          <#}else{#>
            <div class="linetext" data-id="<#=RechargeStrategyId#>">
                 <em class="txt">每充值满</em>
                 <input type="text" data-filed="RechargeAmount" name="RechargeAmount" value="<#=RechargeAmount#>"  value="" class="easyui-numberbox"  data-options="min:0,precision:0,required:true,width:70,height:30"/>
                 <em >赠</em>
                 <input type="text" data-filed="GiftAmount" name="GiftAmount" value="<#=GiftAmount#>" class="easyui-numberbox"  data-options="min:0,precision:0,required: true,width:70,height:30"/> 元
            </div><!-- linetext-->
          <#}#>

         </script>
        <!-- 优惠券-->
        <script id="tpl_addTicket" type="text/html">
           <div class="ticketList">
             <#for(var i=0;i<list.length;i++){ var item=list[i]; var className="borber"; if(i==list.length-1){className="borderNone";}#>
            <div class="ticket <#=className#>">
                <div class="checkBox" data-name="r1" data-id="<#=item.CouponTypeID#>" data-formInfo="<#=JSON.stringify(item)#>"><em></em><span> <#=item.CouponTypeName#></span></div>
            </div>
           <#}#>
        </script>
   <!--消息模板弹框-->
         <script id="tpl_addTemplate" type="text/html">
            <div class="ticketList">
              <#for(var i=0;i<list.length;i++){ var item=list[i]; var className="borber"; if(i==list.length-1){className="borderNone";}#>
             <div class="ticket <#=className#>">
                 <div class="radio" data-name="r1" data-id="<#=item.TemplateID#>" data-formInfo="<#=JSON.stringify(item)#>"><em></em><span> <#=item.Title#></span></div>
             </div>
            <#}#>
         </script>
   <!-- 消息模板编辑-->
        <script id="tpl_addMessage" type="text/html">
        <div class="templatePanel " data-message="<#=JSON.stringify(item)#>">
                                        <div class="panelSel">
                                               <#if(item.ActivityType==3&&item.MessageType=="WeChat"){#>
                                                  <div class="commonSelectWrap" style="display: none">
                                              <#}else{#>
                                                <div class="commonSelectWrap">
                                              <#}#>
                                                  <#if(item.ActivityType==1){#>
                                                 <em class="tit">生日前</em>
                                                  <#}else{#>
                                                   <em class="tit">活动前</em>
                                                  <#}#>
                                                     <div class="selectBox">
                                                    <input class="easyui-combobox" data-name="day" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                       valueField: 'value',
                                       textField: 'label',
                                       onSelect:onSelectDay,
                                       data: [
                                                    {
                                                       label: '请选择',
                                                       value: '0',
                                                       selected:true
                                                    } ,{
                                                label: '1天',
                                                value: '1'
                                             },
                                             {
                                                label: '2天',
                                                value: '2'
                                             },
                                             {
                                                label: '3天',
                                                value: '3'
                                             },
                                             {
                                                label: '4天',
                                                value: '4'
                                             },

                                             {
                                                label: '5天',
                                                value: '5'
                                             },

                                             {
                                                label: '6天',
                                                value: '6'
                                             },

                                             {
                                                label: '7天',
                                                value: '7'
                                             },

                                             {
                                                label: '8天',
                                                value: '8'
                                             },

                                             {
                                                label: '9天',
                                                value: '9'
                                             },
                                             {
                                                label: '10天',
                                                value: '10'
                                             },
                                             {
                                                label: '15天',
                                                value: '15'
                                             },
                                             {
                                                label: '30天',
                                                value: '30'
                                             }]" />
                                                    <input class="easyui-combobox" data-name="hour" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                       valueField: 'value',
                                       textField: 'label',
                                       onSelect:onSelectHour,
                                       data: [ {label: '请选择',value: '0',selected:true},
                                       {label: '1时',value: '1'},{label: '2时',value: '2'},{label: '3时',value: '3'},{label: '4时',value: '4'},{label: '5时',value: '5'},{label: '6时',value: '6'},
                                       {label: '7时',value: '7'},{label: '8时',value: '8'},{label: '9时',value: '9'},{label: '10时',value: '10'},{label: '11时',value: '11'},{label: '12时',value: '12'},
                                       {label: '13时',value: '13'},{label: '14时',value: '14'},{label: '15时',value: '15'},{label: '16时',value: '16'},{label: '17时',value: '17'},{label: '18时',value: '18'},
                                       {label: '19时',value: '19'},{label: '20时',value: '20'},{label: '21时',value: '21'},{label: '22时',value: '22'},{label: '23时',value: '23'},{label: '24时',value: '00'},
                                       ]" />
                                         <#if(item.MessageType!="SMS"){#>
                                                                              <i class="cursorDiv">送券</i>
                                       <#}#>
                                                </div>
                                                <!--selectBox-->
                                                  <#if(item.MessageType=="SMS"){#>
                                                <div class="deletebtn " >删除</div>
                                                <#}#>
                                            </div>

                                            <!--commonSelectWrap-->


                                            <div class="commonBtn" style="display: none">
                                                选择模板</div>
                                                <#if(item.MessageType=="WeChat"){#>
                                                 <p class="mgr22" >连锁掌柜会在优惠券或充值金额到帐后，推送微信模版消息通知您的会员。</p>
                                                 <#}else{#>
                                                     <div class="text">

                                                              <p data-text="充值满赠" data-activity="3">一大波福利向您飞来！即日起<input class="easyui-validatebox mar-l5"   data-options="required:true,validType:'maxLength[16]'" placeholder="请输入活动名">
                                                                                      ,更多狂欢尽在【<input class="easyui-validatebox"  data-options="required:true,validType:'maxLength[16]'" placeholder="请输入品牌名">】！</p>

                                                             <p data-text="营销活动" data-activity="2">你知道吗，我很想你，所以偷偷往你的账户里放了<input class="easyui-validatebox mar-l5"   data-options="required:true,validType:'maxLength[16]'" placeholder="优惠券名称"> ~</p>

                                                             <p data-text="营销活动" data-activity="2">  你若有空，能否来看看我呢【<input class="easyui-validatebox"  data-options="required:true,validType:'maxLength[16]'" placeholder="请输入你的品牌名">】！</p>

                                                             <p data-text="生日关怀" data-activity="1">长长的距离，长长的思念，远方的我一直在掂念着您，【 <input class="easyui-validatebox"  data-options="required:true,validType:'maxLength[16]'" placeholder="请输入你的品牌名">】 提前祝您生日快乐！
                                                             您的专属生日礼物已经发送至账户中，请到会员中心领取。</p>



                                                     </div> <!--text-->
                                            <#}#>
                                        </div>
                                        <!--panelDiv-->
                                    </div>


</script>


        <!--设置开卡礼-->
         <script id="tql_addCouponList" type="text/html">
            <form id="payOrder">

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

   <!-- 消息模板编辑-->
        <script id="tpl_addMessageExit" type="text/html">
        <div class="templatePanel " data-starttime="<#=item.SendTime#>"  data-message="<#=JSON.stringify(item)#>">
                                        <div class="panelSel">
                                            <#if(item.ActivityType==3&&item.MessageType=="WeChat"){#>
                                                  <div class="commonSelectWrap" style="display: none">
                                              <#}else{#>
                                                <div class="commonSelectWrap">
                                              <#}#>
                                                <#if(item.ActivityType==1){#>
                                                   <em class="tit">生日前</em>
                                                    <#}else{#>
                                                     <em class="tit">活动前</em>
                                                 <#}#>
                                                     <div class="selectBox">
                                                    <input class="easyui-combobox" data-name="day" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                       valueField: 'value',
                                       textField: 'label',
                                       value:'<#=item.day#>',
                                       onSelect:onSelectDay,
                                       data: [
                                             {
                                                label: '请选择',
                                                value: '0',
                                             },{
                                                label: '1天',
                                                value: '1'
                                             },
                                             {
                                                label: '2天',
                                                value: '2'
                                             },
                                             {
                                                label: '3天',
                                                value: '3'
                                             },
                                             {
                                                label: '4天',
                                                value: '4'
                                             },

                                             {
                                                label: '5天',
                                                value: '5'
                                             },

                                             {
                                                label: '6天',
                                                value: '6'
                                             },

                                             {
                                                label: '7天',
                                                value: '7'
                                             },

                                             {
                                                label: '8天',
                                                value: '8'
                                             },
                                             {
                                                label: '9天',
                                                value: '9'
                                             },
                                             {
                                                label: '10天',
                                                value: '10'
                                             },
                                             {
                                                label: '15天',
                                                value: '15'
                                             },
                                             {
                                                label: '30天',
                                                value: '30'
                                             }]" />
                                                    <input class="easyui-combobox" data-name="hour" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                       valueField: 'value',
                                       textField: 'label',
                                       value:'<#=item.hour#>',
                                       onSelect:onSelectHour,
                                       data: [{label: '请选择',value: '0'},
                                       {label: '1时',value: '01'},{label: '2时',value: '02'},{label: '3时',value: '03'},{label: '4时',value: '04'},{label: '5时',value: '05'},{label: '6时',value: '06'},
                                       {label: '7时',value: '07'},{label: '8时',value: '08'},{label: '9时',value: '09'},{label: '10时',value: '10'},{label: '11时',value: '11'},{label: '12时',value: '12'},
                                       {label: '13时',value: '13'},{label: '14时',value: '14'},{label: '15时',value: '15'},{label: '16时',value: '16'},{label: '17时',value: '17'},{label: '18时',value: '18'},
                                       {label: '19时',value: '19'},{label: '20时',value: '20'},{label: '21时',value: '21'},{label: '22时',value: '22'},{label: '23时',value: '23'},{label: '24时',value: '00'},
                                       ]" />
                                        <#if(item.MessageType!="SMS"){#>
                                      <i class="cursorDiv">送券</i>
                                       <#}#>
                                                </div>
                                                <!--selectBox-->
                                                  <#if(item.MessageType=="SMS"){#>
                                                                                                <div class="deletebtn " >删除</div>
                                                                                                <#}#>
                                            </div>
                                            <!--commonSelectWrap-->

                                            <div class="commonBtn" style="display:none" >
                                                选择模板</div>
                                                 <#if(item.MessageType=="WeChat"){#>
                                                 <p class="mgr22">连锁掌柜会在优惠券或充值金额到帐后，推送微信模版消息通知您的会员。</p>
                                                 <#}else{#>
                                                     <div class="text">

                                                              <p data-text="充值满赠" data-activity="3">一大波福利向您飞来！即日起<input class="easyui-validatebox mar-l5"   data-options="required:true,validType:'maxLength[16]'" placeholder="请输入活动名称" value="<#=item.activityName#>">
                                                                                      ,更多狂欢尽在【<input class="easyui-validatebox"  data-options="required:true,validType:'maxLength[16]'" placeholder="请输入品牌名称" value="<#=item.brandName#>">】！</p>

                                                             <p data-text="营销活动" data-activity="2">你知道吗，我很想你，所以偷偷往你的账户里放了<input class="easyui-validatebox mar-l5"   data-options="required:true,validType:'maxLength[16]'" placeholder="请输入活动名称" value="<#=item.activityName#>"> ~</p>

                                                             <p data-text="营销活动" data-activity="2">  你若有空，能否来看看我呢【<input class="easyui-validatebox"  data-options="required:true,validType:'maxLength[16]'" placeholder="请输入你的品牌名" value="<#=item.brandName#>">】！</p>

                                                             <p data-text="生日关怀" data-activity="1">长长的距离，长长的思念，远方的我一直在掂念着您，【 <input class="easyui-validatebox"  data-options="required:true,validType:'maxLength[16]'" placeholder="请输入你的品牌名" value="<#=item.brandName#>">】 提前祝您生日快乐！
                                                             您的专属生日礼物已经发送至账户中，请到会员中心领取。</p>



                                                     </div> <!--text-->
                                            <#}#>
                                        </div>
                                        <!--panelDiv-->
                                    </div>


</script>
<!--优惠券添加-->
<script id="tpl_addCoupon"  type="text/html">
<#for(var i=0;i<list.length; i++){ var item=list[i],itemJSON=JSON.stringify(item),CouponName=item.CouponTypeName ? item.CouponTypeName : item.CouponName;#>
    <div class="lineCouponInfo" data-couponid="<#=item.CouponTypeID#>"  data-item="<#=itemJSON#>">
  <#if(CouponName.length>12){ #>
           <p  class="shou" title="<#=CouponName#>"><i><#=i+1+length#></i>、<#=CouponName.substring(0,12)#> ...     <b><#=item.ValidityPeriod#> </b></p>
      <# }else{ #>
           <p><i><#=i+1+length#></i>、<#=CouponName#>      <b><#=item.ValidityPeriod#> </b></p>
      <# }#>
    <p>每人赠送： <input type="text" name="CouponNum" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30"  value="<#=item['NumLimit']#>">张</p>
    <em class="del">删除</em>
    </div>

  <#}#>
</script>
    <script id="tpl_levelItem" type="text/html">
    <div class="checkBox"  data-name="IsAllCardType" data-id="<#=VipCardTypeID#>" data-recharge="<#=IsPrepaid#>"> <em></em><span><#=VipCardTypeName#></span></div>
</script>
<script id="add_lineList" type="text/html">
<#if(type=="Step"){#>
  <div class="linetext" >
       <em class="txt">充值满</em>
       <input type="text" data-filed="RechargeAmount" name="RechargeAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30"/>
       <em >赠</em>
       <input type="text" data-filed="GiftAmount" name="GiftAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30"/> 元
      <div class="radioBtn r" data-name="del">删除</div>       <div class="radioBtn r" data-name="add">添加</div>
  </div><!-- linetext-->
 <#}else{#>
   <div class="linetext" >
        <em class="txt">每充值满</em>
        <input type="text" data-filed="RechargeAmount" name="RechargeAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30"/>
        <em >赠</em>
        <input type="text" data-filed="GiftAmount" name="GiftAmount" value="" class="easyui-numberbox"  data-options="min:0,precision:0,width:70,height:30"/> 元
       <div class="radioBtn r" data-name="del">删除</div>
   </div><!-- linetext-->
 <#}#>

</script>
</asp:Content>
