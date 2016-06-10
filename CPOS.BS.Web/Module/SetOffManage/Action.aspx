<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>集客行动</title>
    <link href="<%=StaticUrl+"/module/SetOffManage/css/action.css?v=1.7"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/action.js?ver=0.1" >
        <!-- 内容区域 -->
         <!-- 员工集客 -->
         <div class="Module setOffStaffModule">
            <div class="ModuleHeader">
                <div class="panelDiv">
                   <p class="title">员工集客</p>
                </div>
            </div>
            <div class="ModuleContent">
                <div class="heads">
                    <div class="title">
                        <img src="images/icon1.png">
                        <span>效果</span>
                    </div>
                </div>
                <div class="contents">
                    <div class="charts" id="staffCharts">
                        <p>点击<a>设置集客行动</a>，开启您的集客行动。</p>
                    </div>
                    <div class="noContents" style="display: none;">
                    </div>
                    <div class="chartsData"></div>
                </div>
            </div>
            <!-- 员工集客行动方案 -->
            <div class="ModuleContent">
                <div class="heads">
                    <div class="title">
                        <img src="images/icon2.png">
                        <span>行动方案</span>
                    </div>
                    <div class="point">
                        <span>展开</span>
                         <span class="bottom"></span>
                    </div>
                </div>
                <div class="blockModul">
                    <!-- 员工集客行动方案激励 -->
                    <div class="contents" style="height:220px;">
                        <div class="panelDiv">
                           <p class="title">激励</p><span class="rewardAction">新增激励方案</span>
                           <div class="lock">
                               <span>禁用</span>
                               <div class="lockBack" data-eabled="90">
                                   <a></a>
                                   <a></a>
                                   <b class="cirle"></b>
                               </div>
                               <span>启用</span>
                            </div>
                        </div>
                        <form>
                            <div class="editArea"><label><span class="title">集客注册成功奖励</span>现金：<input type="text" class="setoffRegPrize" readonly="readonly">元</label></div>
                            <div class="editArea ml"><label><span class="title"></span>*集客完成注册即可获得奖励</label></div>
                            <div class="editArea"><label><span class="title">集客销售成功奖励</span><input type="text" class="setoffOrderPer" readonly="readonly">%</label></div>
                            <div class="editArea ml"><label><span class="title"></span>*集客后产生消费，按消费实付金额获得提成</label></div>
                            <div class="editArea">
                                <label><span class="title"></span>
                                    提成限制<p data-type="1" data-name='首单有效'>首单有效<em class="radio"></em></p>
                                    <p data-type="0" data-name='单单有效'>单单有效<em class="radio"></em></p>
                                </label>
                            </div>
                        </form>
                    </div>
                    <!-- 员工集客行动方案工具 -->
                    <div class="contents contentsTool" >
                        <div class="panelDiv">
                           <p class="title">工具</p><span class="toolSetOff">暂无工具，请添加工具</span>

                        </div>
                        <div class="toolData">
                            <div class="title">
                                <ul>
                                    <li class="current">活动</li>
                                    <li>优惠券</li>
                                    <li>集客海报</li>
                                </ul>
                            </div>
                            <div class="contentsData" id="StaffActionTool"></div>
                            <div class="contentsData" id="StaffCouponTool"></div>
                            <div class="contentsData" id="StaffPosterTool"></div>
                        </div>
                        <div class="noContents">
                            <p>
                               暂无工具，请添加工具。
                            </p>
                        </div>
                    </div>
                    <div class="footer">
                        <div class="commonBtn addTools" data-type="2">添加工具</div>
                     </div>
                 </div>
            </div>
         </div>
         <!-- 会员集客 -->
         <div class="Module setOffVipModule">
             <div class="ModuleHeader">
                 <div class="panelDiv">
                    <p class="title">会员集客</p>
                 </div>
             </div>
             <div class="ModuleContent">
                 <div class="heads">
                     <div class="title">
                         <img src="images/icon1.png">
                         <span>效果</span>
                     </div>
                 </div><div class="contents">
                     <div class="charts" id="vipCharts">
                        <p>
                             点击<a>设置集客行动</a>，开启您的集客行动。
                         </p>
                    </div>
                    <div class="noContents" style="display: none;"></div>
                     <div class="chartsData"></div>
                 </div>
             </div>
             <!-- 会员集客行动方案 -->
             <div class="ModuleContent">
                 <div class="heads">
                     <div class="title">
                         <img src="images/icon2.png">
                         <span>行动方案</span>
                     </div>
                     <div class="point">
                         <span>展开</span>
                         <span class="bottom"></span>
                     </div>
                 </div>
                 <div class="blockModul">
                     <!-- 会员集客行动方案激励-->
                    <div class="contents" style="height:220px;">
                        <div class="panelDiv">
                           <p class="title">激励</p><span class="rewardAction">新增激励方案</span>
                           <div class="lock">
                                <span>禁用</span>
                                <div class="lockBack" data-eabled="90">
                                    <a></a>
                                    <a></a>
                                    <b class="cirle left"></b>
                                </div>
                                <span>启用</span>
                           </div>
                        </div>
                        <form>
                            <div class="editArea"><label><span class="title">集客注册成功奖励</span>
                                <input id="rewardRule" type="text" data-text="奖励规则" data-flag="Status" class="easyui-combobox"  />
                                <input type="text"  readonly="readonly" class="setoffRegPrize"><span class="till">积分</span></label>
                            </div>
                            <div class="editArea ml"><label><span class="title"></span>*集客完成注册即可获得奖励</label></div>
                            <div class="editArea"><label><span class="title">集客销售成功奖励</span><input type="text" class="setoffOrderPer" readonly="readonly">%</label></div>
                            <div class="editArea ml"><label><span class="title"></span>*集客后产生消费，按消费实付金额获得提成</label></div>
                            <div class="editArea">
                                <label><span class="title"></span>
                                    提成限制<p data-type="1" data-name='首单有效'>首单有效<em class="radio"></em></p>
                                    <p data-type="0" data-name='单单有效'>单单有效<em class="radio"></em></p>
                                </label>
                            </div>
                        </form>
                    </div>
                    <!-- 会员集客行动方案工具 -->
                     <div class="contents contentsTool">
                        <div class="panelDiv">
                           <p class="title">工具</p><span class="toolSetOff">暂无工具，请添加工具</span>
                        </div>
                         <div class="toolData">
                              <div class="title">
                                    <ul>
                                        <li class="current">活动</li>
                                        <li>优惠券</li>
                                        <li>集客海报</li>
                                    </ul>
                              </div>
                              <div class="contentsData" id="VipActionTool"></div>
                              <div class="contentsData" id="VipCouponTool"></div>
                              <div class="contentsData" id="VipPosterTool"></div>
                         </div>
                         <div class="noContents">
                             <p>
                                暂无工具，请添加工具。
                             </p>
                         </div>
                     </div>

                     <div class="footer" >
                        <div class="commonBtn addTools" data-type="1">添加工具</div>
                     </div>
                 </div>
             </div>
          </div>
          <div class="btnWrap">
                <a href="javascript:;" id="saveSetOff" class="commonBtn saveBtn">确认发布</a>
              <a href="javascript:;" id="saveMessage" class="commonBtn message">发送通知</a>

          </div>
    </div>

	<!-- 集客行动信息确认 -->
    <div style="display: none">
        <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" id="panlconent">
                <div data-options="region:'center'" style="padding: 10px;">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                    text-align: center;margin:0;border-top:1px solid #e1e7ea;">
                     <a class="easyui-linkbutton commonBtn saveBtn"href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                     <a class="commonBtn message">发布集客行动</a>
                </div>
            </div>
        </div>
    </div>
	<!-- 集客发送通知 -->
    <div style="display: none">
        <div id="winMessage" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:false">
            <div class="easyui-layout" data-options="fit:true" id="panlConent">
                <div data-options="region:'center'" style="padding: 10px;">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                    text-align: center;border-top:1px solid #e1e7ea;">
                     <a class="easyui-linkbutton commonBtn saveBtn">取消</a>
                     <a class="easyui-linkbutton commonBtn message">确认</a>
                </div>
            </div>
        </div>
    </div>

    <!-- 集客发送通知 -->
        <div style="display: none">
            <div id="winCanel" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
                <div class="easyui-layout" data-options="fit:true" >
                    <div data-options="region:'center'" style="padding: 10px;" id="panlConents">
                        指定的模板添加内容
                    </div>
                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                        text-align: center;border-top:1px solid #ccc;">
                         <a class="easyui-linkbutton commonBtn saveBtn"href="javascript:void(0)" onclick="javascript:$('#winCanel').window('close')">取消</a>
                         <a class="easyui-linkbutton commonBtn message">确定</a>
                    </div>
                </div>
            </div>
        </div>

    <!-- 集客行动工具列表 -->
        <div style="display: none">
            <div id="winTool" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true,zIndex:22">

                    <div data-options="region:'center'" style="padding: 10px;">
                        <div class="tableWrap" id="tableWrap">
                            <div class="tableList" id="opt">
                               <ul><li class="on" data-Field7="0"><em > 活动 </em></li>
                                    <li data-Field7="100"><em>优惠券 </em></li>
                                    <li data-Field7="900"><em>集客海报</em></li>
                               </ul>
                            </div>
                            <div class="toolList">
                                <div class="commonBtn">新建优惠券</div>
                                <div class="reload"><em>刷新</em></div>
                            </div>
                            <div id="notice" style="text-align:center;display:none; ">暂无活动，请新增相关活动</div>
                            <div class="dataTable" id="gridTable1" style="display:none">
                                <div class="loading" style="padding-top: 0px;">
                                     <span>
                                   <img src="../static/images/loading.gif"></span>
                                </div>
                            </div>
                            <div class="dataTable" id="gridTable2" style="display:none">
                                <div class="loading" style="padding-top: 0px;">
                                     <span>
                                   <img src="../static/images/loading.gif"></span>
                                </div>
                            </div>
                            <div id="setOfferPoster" style="display:none;">
                                <div class="editArea">
                                    <em>海报名称</em><input type="text" placeholder="国庆节集客海报" id="setPosterName" data-id="" class="easyui-validatebox" data-options="required:true">
                                </div>
                                <div class="editArea">
                                    <p>图片建议尺寸:1008x640PX,大小不超过600k.</p>
                                </div>
                                <div class="upImageModul" id="editLayer">
                                    <div class="upImage uploadItem"  data-flag=16  data-url="" data-batid="BackGround">
                                        <img src="" id="setOffBack" style="width:194px;height:296px;">
                                        <div class="commonBtn ">
                                            <a>上传海报背景</a>
                                            <a href="javascript:;" class="commonHandleBtn uploadImgBtn"  data-alertinfo="背景图片上传成功！">上传海报背景</a>
                                        </div>
                                        <div class="qrCodeModul">
                                            <div class="title">扫描成为会员</div>
                                            <div class="imageContents">
                                                <img  src="images/qrord.png">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--<div id="pageContianer">-->
                                <!--<div class="dataMessage">暂无活动，请新增活动</div>-->
                            <!--</div>-->
                        </div>
                    </div>

                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="
                        text-align: center; padding: 20px 0 0;margin:0;">
                         <a class="easyui-linkbutton commonBtn saveBtn"href="javascript:void(0)" onclick="javascript:$('#winTool').window('close')">取消</a>
                         <a class="easyui-linkbutton commonBtn message">确定</a>
                    </div>


    </div>

    <!--活动列表-->
    <script id="tpl_action" type="text/html">
          <div class="tplAction">
            <ol>
                <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="CTW"){#>
                    <li data-id="<#=item.ObjectId#>" data-new="true" data-type="1" data-toolid="<#=item.SetoffToolID#>">
                    <label>
                        <p><b><#=i+1#>.</b><span class="name"><#=item.Name#></span></p>
                        <p><span><em>开始时间:</em><em class="staffStart"><#=item.BeginData#></em></span><span><em>结束时间:</em><em class="StaffEnd"><#=item.EndData#></em></span> </p>
                    </label>
                    <span  class="removeDlist" data-id="<#=item.SetoffToolID#>">取消发布</span></li>
                <#}else{#>
                    <li data-id="<#=item.CTWEventId#>" data-type="1">
                    <label>
                        <p><b><#=i+1#>.</b><span class="name"><#=item.Name#></span></p>
                        <p><span><em>开始时间</em><em class="staffStart"><#=item.StartDate#></em></span><span><em>结束时间</em><em class="StaffEnd"><#=item.EndDate#></em></span> </p>
                    </label>
                    <span  class="remove" data-id="<#=item.CTWEventId#>">移除</span></li>
                <#}#>
                 <#}#>
            </ol>
          </div>
    </script>

    <!--优惠券列表-->
    <script id="tpl_coupon" type="text/html">
          <div class="tplCoupon">
            <ol>
            <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="Coupon"){#>
                    <li data-id="<#=item.ObjectId#>" data-time="<#=item.ValidityPeriod#>" data-new="true" data-type="2" data-toolid="<#=item.SetoffToolID#>">
                        <label>
                            <p><b><#=i+1#>.</b><span class="name"><#=item.Name#></span><span class="till">剩余<#=item.SurplusCount#>张</span> </p>
                            <#if(item.BeginData==null){#>
                                <p class="Time">领取后<#=item.ServiceLife#>天有效</p>
                            <#}else{#>
                                <p class="Time"><#=item.BeginData#>至<#=item.EndData#></p>
                            <#}#>
                        </label>

                    <span class="removeDlist" data-id="<#=item.CouponTypeID#>">取消发布</span></li>

                <#}else{#>
                    <li data-id="<#=item.CouponTypeID#>" data-time="<#=item.ValidityPeriod#>" data-type="2">
                    <label>
                        <p><b><#=i+1#>.</b><span class="name"><#=item.CouponTypeName#></span><span class="till">剩余<#=item.SurplusQty#>张</span> </p>
                        <#if(item.BeginTimeDate==""){#>
                            <p class="Time">领取后<#=item.ServiceLife#>天有效</p>
                        <#}else{#>
                            <p class="Time"><#=item.BeginTimeDate#>至<#=item.EndTimeDate#></p>
                        <#}#>
                    </label>
                <span class="remove" data-id="<#=item.CouponTypeID#>">移除</span></li>
                 <#}#>
                 <#}#>
            </ol>
          </div>
    </script>

    <!--集客海报列表-->
    <script id="tpl_poster" type="text/html">
          <div class="tplPoster">
            <ol>
                <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="SetoffPoster"){#>
                    <li data-id="<#=item.ObjectId#>" data-url="<#=item.SetoffPosterUrl#>"  data-new="true" data-type="3" data-toolid="<#=item.SetoffToolID#>">
                        <label>
                            <p><b><#=i+1#>.</b><span class="name"><#=item.Name#></span></p>
                        </label>
                    <span class="removeDlist" data-id="<#=item.CouponTypeID#>">取消发布</span>
                    <span class="exitPoster">编辑</span>
                    </li>
                    <#}else{#>
                    <li data-id="<#=item.SetoffPosterID#>" data-url="<#=item.ImageUrl#>" data-type="3">
                        <label>
                            <p><b><#=i+1#>.</b><span class="name"><#=item.Name#></span></p>
                        </label>
                        <span class="remove">移除</span>
                        </li>
                    </li>
                 <#}#>
                 <#}#>
            </ol>
          </div>
    </script>

    <!--信息确认 二级模板-->
    <script id="tpl_Info" type="text/html">
	 <#for(var i=0;i<list.length;i++){ var item=list[i];#>
        <div class="setInfoMoudle">
            <div class="panelDiv">
               <p class="title">员工集客</p>
            </div>
           
            <div class="editArea">
                <em>集客注册成功奖励:</em><p><b><#=item.SetoffRegAwardName#></b><#=item.setoffRegPrize#>元</p>
            </div>
            <div class="editArea">
                <em>集客销售成功提成:</em><p><#=item.setoffOrderPer#>%</p>
            </div>
            <div class="editArea">
                <em>提成限制:</em><p><#=item.setoffOrderTimers#></p>
            </div>
            <div class="editArea">
                <em>目前共发布集客行动</em><p><#=item.activeLL#>个</p>
            </div>
        </div>
        <div class="setInfoMoudle">
            <div class="panelDiv">
               <p class="title">会员集客</p>
            </div>
            
            <div class="editArea">
                <em>集客注册成功奖励:</em><p><b><#=item.setoffVipName#></b><#=item.setoffVipRegPrize#><#=item.SetoffViptill#></p>
            </div>
            <div class="editArea">
                <em>集客销售成功提成:</em><p><#=item.setoffVipOrderPer#>%</p>
            </div>
            <div class="editArea">
                <em>提成限制:</em><p><#=item.setoffOrderVipTimers#></p>
            </div>
            <div class="editArea">
                <em>目前共发布集客行动:</em><p><#=item.activeVipLL#>个</p>
            </div>
            
        </div>
	<#}#>
    </script>

    <!--短信通知-->
    <script id="tpl_Message" type="text/html">
        <div class="contents" style="height:100px;margin-left:20px;">
            <div class="editCheck" data-type="2">
                <span class="opt checkBox on"></span><p data-name="集客通知">通知员工-发送到连锁掌柜APP-总部消息</p>
            </div>
            <div class="editMessage"><textarea>集客行动已发布，快来参加获取丰富福利</textarea></div>
        </div>
        <div class="contents" style="height:100px;margin-left:20px;">
            <div class="editCheck" data-type="1">
                <span class="opt checkBox on"></span><p data-name="集客通知">通知会员-发送到微信会员中心-通知</p>
            </div>
            <div class="editMessage"><textarea>集客行动已发布，快来参加获取丰富福利</textarea></div>
        </div>
     </script>
</asp:Content>
