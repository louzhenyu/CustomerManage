<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>生日营销活动设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/birthDayDetail.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/birthDayDetail.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" style="display: inline-block;">
                <div class="panelList">
                    <div class="title">
                        <ul id="optPanel">
                            <li data-flag="#nav01" class="on one"><span><em>1</em> 基础信息</span></li>
                            <li data-flag="#nav02"><span><em>2</em>奖品</span></li>
                            <li data-flag="#nav03" class="borderNone"><span><em>3</em>消息</span></li>
                        </ul>
                    </div>
                    <form>
                    </form>
                    <form id="nav0_1">
                    <div class="panelDiv" id="nav01" data-index="0">
                        <div class="line">
                            <div class="linePanel">
                                <div class="commonSelectWrap">
                                    <em class="tit">活动名称：</em>
                                    <label class="searchInput" style="width: 352px;">
                                        <input class="easyui-validatebox" data-options="required:true,validType:'stringCheck'" name="ActivityName"
                                            type="text" value="" placeholder="请输入">
                                    </label>
                                </div>
                                <div class="commonSelectWrap">
                                    <em class="tit">活动时间：</em>
                                    <div class="selectBox" style="width: 380px;">
                                        <input type="text" id="BeginDate" name="StartTime" class="easyui-datebox" data-options="required:true,width:160,height:32"
                                            placeholder="请选择" />
                                        <em class="text">至 </em>
                                        <input type="text" id="name" data-name="IsLongTime" name="EndTime" class="easyui-datebox"
                                            data-options="required:true,width:160,height:32" placeholder="请选择" validtype="compareEqualityDate[$('#BeginDate').datebox('getText')]" />
                                    </div>
                                    <div class="checkBox position" data-filed="IsLongTime" data-name="datebox">
                                        <em></em><span>长期</span></div>
                                </div>
                            </div>
                        </div>
                        <div class="line borderNone">
                            <div style="width: 380px;">
                                <div class="commonSelectWrap">
                                    <div class="tit">
                                        <div class="radio on" data-name="vip">
                                            <em></em><span>会员卡类型：</span></div>
                                    </div>
                                    <div class="selectBox">
                                        <input type="text" class="easyui-combobox" id="vipCard" name="VipCardTypeID" data-options="width:160,height:32,validType:'selectIndex'">
                                    </div>
                                </div>
                                <!--                        <div class="commonSelectWrap" >
                                               <div class="tit"><div class="radio" data-name="vip"><em></em><span>会员分组：</span></div></div>
                                               <div class="selectBox">
                                                <input type="text" class="easyui-combobox"  name="CardDiscount" data-options="width:160,height:32">
                                               </div>

                                           </div>-->
                            </div>
                        </div>
                    </div>
                    <!--panelDiv-->
                    </form>
                    <!--奖品明细-->
                   <form></form>
                   <form id="nav0_2">
                    <div class="panelDiv" id="nav02" data-index="1">
                        <div class="tagPanel" style="display: none">
                            <div class="checkBoxPanel">
                                <div class="inputDiv">
                                    <div class="checkBox" data-filed="PointsMultiple" data-name="numberBox">
                                        <em></em><span>消费获赠</span></div>
                                    <input type="text" data-name="PointsMultiple" name="PointsMultiple" class="easyui-numberbox" data-options="min:1,precision:0,max:10000,width:120,height:32">倍积分
                                    <span class="hint">提示：多倍积分将以您的【积分返现配置】的设置为基础值。</span>
                                </div>
                                <!--inputDiv-->
                            </div>
                            <!--checkBoxPanel-->
                            <div class="tablePanl">
                                <div id="gridTable">
                                </div>
                            </div>
                        </div>
                        <!--tagPanel-->
                        <div class="tagPanel">
                            <div class="checkBoxPanel">
                                <div class="inputDiv">
                                    <div class="checkBox" data-name="optSelect">
                                        <em></em><span>赠送礼券：</span></div>
                                    <div class="optPanel optSelect">
                                        <div class="commonBtn" data-prizestype="1" data-prizesid="">选择券</div>
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
                            短信设置</div>
                        <div class="tagPanel borderNone">
                            <div class="checkBoxPanel">
                                <div class="inputDiv">
                                    <div class="checkBox" data-filed="SMS" data-type="add"  data-name="optSelect">
                                        <em></em><span>是否启用</span></div>
                                </div>
                                <!--inputDiv-->
                            </div>
                            <!--checkBoxPanel-->

                            <div id="SMS" class="optSelect">
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
                                                                               label: '5天',
                                                                               value: '5'
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
                            <div class="textAddBtn" data-flag="SMS"> +添加</div>
                            </div><!--SMS-->

                        </div><!--tagPanel-->
                        <div class="lineTitle">
                            微信设置</div>
                        <div class="tagPanel borderNone">
                            <div class="checkBoxPanel">
                                <div class="inputDiv">
                                    <div class="checkBox" data-filed="WeChat" data-type="add"  data-name="optSelect">
                                        <em></em><span>是否启用</span></div>
                                </div>
                                <!--inputDiv-->
                            </div>
                            <!--checkBoxPanel-->
              <div id="WeChat" class="optSelect">
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
                                                                              label: '5天',
                                                                              value: '5'
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
                                                                          <div class="commonBtn" style="display: none">
                                                                              选择模板</div>
                                                                          <div class="text">
                                                                              <textarea></textarea>
                                                                          </div>
                                                                      </div>
                                       </div>

                            <div class="textAddBtn" data-flag="WeChat">
                                +添加</div>
                </div>
                        </div>
                    </form>
                    </div>

                    <!--panelDiv-->
                </div>

                <!--panelList-->
                <div class="zsy">
                </div>
                <div class="btnopt" data-falg="nav01">
                    <div class=" commonBtn bgWhite" data-flag="#nav01">
                        上一步</div>
                    <div class=" commonBtn" id="submitBtn" data-submitindex="1" data-flag="#nav02">
                        下一步</div>
                </div>
            </div>
        </div>
        <div style="display: none">
            <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
                <div class="easyui-layout" data-options="fit:true" id="panlconent">
                    <div data-options="region:'center'" style="padding: 10px;" >
                        指定的模板添加内容
                    </div>
                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                        text-align: center; padding: 5px 0 0; margin-top: 18px; border-bottom:none;">
                        <a class="easyui-linkbutton commonBtn saveBtn">确定</a> <a class="easyui-linkbutton commonBtn cancelBtn"
                            href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                    </div>
                </div>
            </div>
        </div>
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
                                                label: '5天',
                                                value: '5'
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
                                       {label: '1时',value: '1'},{label: '2时',value: '2'},{label: '3时',value: '3'},{label: '4时',value: '4'},{label: '5时',value: '5'},{label: '6时',value: '6'},
                                       {label: '7时',value: '7'},{label: '8时',value: '8'},{label: '9时',value: '9'},{label: '10时',value: '10'},{label: '11时',value: '11'},{label: '12时',value: '12'},
                                       {label: '13时',value: '13'},{label: '14时',value: '14'},{label: '15时',value: '15'},{label: '16时',value: '16'},{label: '17时',value: '17'},{label: '18时',value: '18'},
                                       {label: '19时',value: '19'},{label: '20时',value: '20'},{label: '21时',value: '21'},{label: '22时',value: '22'},{label: '23时',value: '23'},{label: '24时',value: '00'},
                                       {label: '请选择',value: '0',selected:true}]" />
                                                </div>
                                                <!--selectBox-->
                                            </div>
                                            <!--commonSelectWrap-->
                                            <div class="commonBtn" style="display: none">
                                                选择模板</div>
                                            <div class="text">
                                                <textarea  class="easyui-validatebox" data-options="validType:'maxLength[50]'"><#=item.Content#></textarea>
                                            </div>
                                        </div>
                                        <!--panelDiv-->
                                    </div>


</script>



   <!-- 消息模板编辑-->
        <script id="tpl_addMessageExit" type="text/html">
        <div class="templatePanel " data-starttime="<#=item.SendTime#>"  data-message="<#=JSON.stringify(item)#>">
                                        <div class="panelSel">
                                            <div class="commonSelectWrap">
                                                <em class="tit">生日前</em>
                                                <div class="selectBox">
                                                    <input class="easyui-combobox" data-name="day" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                       valueField: 'value',
                                       textField: 'label',
                                       value:'<#=item.day#>',
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
                                                label: '5天',
                                                value: '5'
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
                                             }]" />
                                                    <input class="easyui-combobox" data-name="hour" data-options="width:90,height:32,required:true,editable:false,validType:'selectIndex',
                                       valueField: 'value',
                                       textField: 'label',
                                       value:'<#=item.hour#>',
                                       onSelect:onSelectHour,
                                       data: [
                                       {label: '1时',value: '01'},{label: '2时',value: '02'},{label: '3时',value: '03'},{label: '4时',value: '04'},{label: '5时',value: '05'},{label: '6时',value: '06'},
                                       {label: '7时',value: '07'},{label: '8时',value: '08'},{label: '9时',value: '09'},{label: '10时',value: '10'},{label: '11时',value: '11'},{label: '12时',value: '12'},
                                       {label: '13时',value: '13'},{label: '14时',value: '14'},{label: '15时',value: '15'},{label: '16时',value: '16'},{label: '17时',value: '17'},{label: '18时',value: '18'},
                                       {label: '19时',value: '19'},{label: '20时',value: '20'},{label: '21时',value: '21'},{label: '22时',value: '22'},{label: '23时',value: '23'},{label: '24时',value: '00'},
                                       {label: '请选择',value: '0'}]" />
                                                </div>
                                                <!--selectBox-->
                                            </div>
                                            <!--commonSelectWrap-->
                                            <div class="commonBtn" style="display:none" >
                                                选择模板</div>
                                            <div class="text">
                                                <textarea class="easyui-validatebox" data-options="validType:'maxLength[50]'"><#=item.Content#></textarea>
                                            </div>
                                        </div>
                                        <!--panelDiv-->
                                    </div>


</script>

        <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
