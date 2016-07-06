<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员卡详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/vipCardManage/css/detail.css?v=0.4"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="allPage" id="section" data-js="js/vipCardDetail.js?ver=0.3">
            <!-- 内容区域 -->
            <div class="contentArea_vipquery">
                <!--个别信息查询-->
                <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
                <div class="optBtn commonBtn" data-authority="Activation" data-optionType="10" data-show="0">激活</div>
                      <div class="optBtn commonBtn" data-authority="Upgrade" data-optionType="2" data-show="1">升级</div>
                      <div class="optBtn commonBtn" data-authority="ReportTheLoss" data-optionType="3"  data-show="1">挂失</div>
                      <div class="optBtn commonBtn" data-authority="Freeze" data-optionType="4"  data-show="1">冻结</div>

                      <div class="optBtn commonBtn" data-authority="Transform"  data-flag="卡转移" data-optionType="5"  data-show="4,1">转卡</div>
                      <div class="optBtn commonBtn" data-authority="RelieveLoss"  data-optionType="6"  data-show="4">解挂</div>
                      <div class="optBtn commonBtn" data-authority="RelieveFreeze"  data-optionType="7"  data-show="2">解冻</div>
                      <div class="optBtn commonBtn" data-authority="Awakening"  data-optionType="9"  data-show="5">唤醒</div>
                      <div class="commonBtn" data-authority="Cancel"  data-optionType="8"  data-show="5,2">作废</div>
                    <!--<h2 class="commonTitle">会员查询</h2>-->

                </div>
                  <form></form>
                  <form id="loadForm">
                   <div class="lineTitle">会员信息</div>
                   <div class="linePanel">
                   <div class="commonSelectWrap">
                    <em class="tit">会员卡号:</em>
                    <div class="searchInput"><input name="VipCardCode" disabled="disabled"/></div>
                   </div>  <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">会员姓名:</em>
                    <div class="searchInput"><input name="VipName" disabled="disabled"/></div>
                   </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">手机号:</em>
                    <div class="searchInput"><input name="Phone" disabled="disabled"/></div>
                   </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">生日:</em>
                    <div class="searchInput"><input name="Birthday" disabled="disabled"/></div>
                   </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">性别:</em>
                    <div class="searchInput"><input name="Gender" disabled="disabled"/></div>
                   </div><!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">当前积分:</em>
                    <div class="searchInput"><input name="Integration" disabled="disabled"/></div>
                   </div><!--commonSelectWrap-->

                   </div>  <!--linePanel-->


                   <div class="lineTitle">卡信息</div>
                   <div class="linePanel">
                   <div class="commonSelectWrap">
                    <em class="tit">卡内码:</em>
                    <div class="searchInput"><input name="VipCardISN" disabled="disabled"/></div>
                   </div>  <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">会员卡类型:</em>
                    <div class="searchInput"><input name="VipCardName" disabled="disabled"/></div>
                   </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">会员卡状态:</em>
                    <div class="searchInput" id="status" ><input class="fontC" name="VipCardStatusName" disabled="disabled"/></div>
                   </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">办卡日期:</em>
                    <div class="searchInput"><input name="MembershipTime" disabled="disabled"/></div>
                   </div> <!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">办卡门店:</em>
                    <div class="searchInput"><input name="MembershipUnitName" disabled="disabled"/></div>
                   </div><!--commonSelectWrap-->
                   <div class="commonSelectWrap">
                    <em class="tit">操作人:</em>
                    <div class="searchInput"><input name="CraeteUserName" disabled="disabled"/></div>
                   </div><!--commonSelectWrap-->
                   <div class="commonSelectWrap" style="display: none">
                    <em class="tit">售卡员工:</em>
                    <div class="searchInput"><input name="SalesUserName" disabled="disabled"/></div>
                   </div><!--commonSelectWrap-->

                   </div>  <!--linePanel-->

                   <div class="lineTitle">卡操作记录</div>
                   <div class="imgTable cursorDef">
                    <div class="VipCardStatusList"></div>
                     <div id="pageContianer">
                            <div class="dataMessage" >当前卡没有任何操作记录</div>
                     </div>
                    </div>
                    <div class="lineTitle" style="display: none">余额变动</div>
                    <div class="linePanel imgTable cursorDef" style="display: none" >
                       <div class="optPanel">
                                        <div class="commonSelectWrap">
                                         <em class="tit">累计充值:</em>
                                         <div class="searchInput" ><input name="TotalAmount" disabled="disabled"/></div>

                                        </div>
                                        <div class="commonSelectWrap">
                                                         <em class="tit">当前余额:</em>
                                                         <div class="searchInput"><input class="fontC" name="BalanceAmount" disabled="disabled"/></div>
                                        </div>
                                        <div class="commonBtn adjust" data-authority="Adjust" data-optionType="1">调整</div>
                       </div><!--optPanel-->



                    <div class="VipCardBalanceList"></div>
                     <div id="pageContianer1">
                            <div class="dataMessage" >当前卡没有任何余额变动记录</div>
                             <div id="kkpager" style="
                             text-align: center;"></div>
                     </div>
                    </div><!--linePanel-->
                  </form>


            </div>
        </div>
        <!---easy ui  弹框---->
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
       <!-- 状态变更-->
       <script id="tpl_setVipCard" type="text/html">
            <form id="optionform">
               <div class="commonSelectWrap">
                     <em class="tit"><b class="showHide" data-hide="3,4" >原</b>卡号：</em>
                    <div class="searchInput bonone">
                               <input name="VipCardCode" type="text" disabled="disabled"/>
                   </div>
               </div>


         <div class="commonSelectWrap showHide" data-show="2,5" style="float: none; width: 500px;">
                                <em class="tit">卡号：</em>
                               <div class="searchInput">
                                  <input type="text" name="NewCardCode"  class="easyui-validatebox" data-options="required:true"  />  <em class="hint">提示:可刷卡</em>
                              </div>
                          </div>
         <div class="commonSelectWrap">
                                <em class="tit">原因：</em>
                               <div class="searchInput bonone " style="width: 460px;">
                                     <input  id="ChangeReason" name="ChangeReason" class="easyui-combobox" data-options="validType:'selectIndex',editable:false,width:460,height:32,valueField: 'label',textField: 'value'"/>


                              </div>
                          </div>
        <!--         <div class="commonSelectWrap">
                                <em class="tit">服务类型：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-combobox" name="ServicesType" data-options="width:160,height:32" />
                              </div>
                          </div>
             <div class="commonSelectWrap">
                                <em class="tit">服务时长：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-numberbox" name="Duration" data-options="width:160,height:32,min:0,precision:2,max:48" />
                              </div>
                          </div>-->
              <div class="commonSelectWrap" style="height: 80px;">
                 <em class="tit">备注：</em>
                <div class="searchInput remark" >
                   <textarea type="text" name="Remark" class="easyui-validatebox" data-options="validType:'maxLength[50]'"> </textarea>
               </div>
           </div>
            <div class="commonSelectWrap showHide" data-show="3,">
            <em class="tit">图片上传：</em>
            <div class="searchInput bonone">
                     <div class="uploadBtn"></div>
                     <div class="imgPanel"><img /></div>
            </div>
            </div>


           </form>
       </script>
    <!--调整余额-->
    <script id="tpl_adjustIntegral" type="text/html">
    <form id="optionform">

               <div class="commonSelectWrap">
                     <em class="tit">当前余额：</em>
                    <div class="searchInput bonone">
                               <input name="BalanceAmount" type="text" disabled="disabled"/>
                   </div>
               </div>


         <div class="commonSelectWrap" style="float: none; width: 500px;">
                                <em class="tit">调整数量：</em>
                               <div class="searchInput bonone">
                                  <input type="text" name="BalanceMoney" class="easyui-numberbox"  data-options="precision:0,width:100,height:32,required:true,"  />
                              </div>
                          </div>
         <div class="commonSelectWrap">
                                <em class="tit">原因：</em>
                               <div class="searchInput bonone " style="width: 460px;">
                                     <input  name="ChangeReason" class="easyui-combobox" data-options="validType:'selectIndex',editable:false,width:460,height:32,
                                     		valueField: 'label',
                                     		textField: 'value',
                                     		data: [{
                                     			label: '营销活动',
                                     			value: '营销活动'
                                     		},

                                     		{
                                     			label: '客户充值',
                                     			value: '客户充值'
                                     		},{
                                     			label: '埋单错误',
                                     			value: '埋单错误'
                                     		},
                                     		{
                                     			label: '0',
                                     			value: '请选择',
                                     			selected:true
                                     		}
                                     		]" />


                              </div>
                          </div>
        <!--         <div class="commonSelectWrap">
                                <em class="tit">服务类型：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-combobox" name="ServicesType" data-options="width:160,height:32" />
                              </div>
                          </div>
             <div class="commonSelectWrap">
                                <em class="tit">服务时长：</em>
                               <div class="searchInput bonone">
                                  <input type="text" class="easyui-numberbox" name="Duration" data-options="width:160,height:32,min:0,precision:2,max:48" />
                              </div>
                          </div>-->
              <div class="commonSelectWrap">
                 <em class="tit">备注：</em>
                <div class="searchInput remark" >
                   <textarea type="text" name="Remark" class="easyui-validatebox" data-options="validType:'maxLength[50]'"> </textarea>
               </div>
                      <div class="commonSelectWrap showHide" data-show="3,">
                      <em class="tit">图片上传：</em>
                      <div class="searchInput bonone">
                               <div class="uploadBtn"></div>
                               <div class="imgPanel"><img /></div>
                      </div>
                      </div>

     </form>
    </script>
        <!--激活-->
        <script id="tpl_activate" type="text/html">
        <form id="optionform">

                   <div class="commonSelectWrap">
                         <em class="tit">会员卡类型：</em>
                        <div class="searchInput bonone">
                                   <input name="VipCardName" type="text" disabled="disabled"/>
                       </div>
                   </div>
                   <div class="commonSelectWrap">
                         <em class="tit">卡号：</em>
                        <div class="searchInput bonone">
                                   <input name="VipCardCode" type="text" disabled="disabled"/>
                       </div>
                   </div>
                   <div class="commonSelectWrap">
                         <em class="tit">会员姓名：</em>
                        <div class="searchInput bonone">
                                   <input name="VipName" type="text" disabled="disabled"/>
                       </div>
                   </div>
     </form>
    </script>
       <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
            defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
