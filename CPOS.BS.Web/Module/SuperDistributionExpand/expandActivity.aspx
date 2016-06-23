<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>分销拓展行动</title>
    <link href="<%=StaticUrl+"/module/SuperDistributionExpand/css/expandActivity.css?v=0.5"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/expandActivity.js?ver=0.1" >
        <!-- 内容区域 -->
         <!-- 员工集客 -->
         <div class="Module">
            <div class="ModuleHead">
                <div class="panelDiv">
                   <p class="title">近30天拓展行动效果</p>
                </div>
            </div>
            <div class="ModuleContent day30Totle">
                <div class="day30Area">
                    <div class="itemTotle">
                        <div class="head">
                            <div class="title">
                                <img src="images/rightList.png">
                                <span>总计</span>
                            </div>
                        </div>
                        <div class="content" style="border-bottom:1px solid #ccc">
                            <p>近30天拓展工具分享次数</p>
                            <div class="record">
                                <p><em class='totle'>0</em><i>人</i></p><span><i class='top'></i><em>0</em>人</span>
                            </div>
                        </div>
                        <div class="content">
                            <p>近30天新增分销商</p>
                            <div class="record">
                                <p><em class='totle'>0</em><i>人</i></p><span><i class='top'></i><em>0</em>人</span>
                            </div>
                        </div>
                    </div>
                    <div class="item" id="weChat">
                        <div class="head">
                            <div class="title">
                                <span>微信图文</span>
                            </div>
                        </div>
                        <div class="content">
                            <p>已推送</p>
                            <div class="record">
                                <p><em class='totle'>0</em><i>人</i></p><span><i class='top'></i><em>0</em>人</span>
                            </div>
                        </div>
                    </div>
                    <div class="item" id="expandActivity">
                        <div class="head">
                            <div class="title">
                                <span>分销活动</span>
                            </div>
                        </div>
                        <div class="content">
                            <p>已推送</p>
                            <div class="record">
                                <p><em class='totle'>0</em><i>人</i></p><span><i class='top'></i><em>0</em>人</span>
                            </div>
                        </div>
                    </div>

                    <div class="item" id="coupon">
                        <div class="head">
                            <div class="title">
                                <span>优惠券</span>
                            </div>
                        </div>
                        <div class="content">
                            <p>已推送</p>
                            <div class="record">
                                <p><em class='totle'>0</em><i>人</i></p><span><i class='top'></i><em>0</em>人</span>
                            </div>
                        </div>
                    </div>

                    <div class="item" id="poster">
                        <div class="head">
                            <div class="title">
                                <span>招募海报</span>
                            </div>
                        </div>
                        <div class="content">
                            <p>已推送</p>
                             <div class="record">
                                 <p><em class='totle'>0</em><i>人</i></p><span><i class='top'></i><em>0</em>人</span>
                             </div>
                        </div>
                    </div>
                </div>
                <div class="noContents" style="display: none">
                    <p>
                        你还没有添加工具<a>请点击添加工具</a>，拓展工具可以更好的帮助分销商拓展下线哟！
                    </p>
                </div>
            </div>
            <div class="ModuleHead toolData">
                <div class="panelDiv">
                   <p class="title">拓展工具</p><span>未发布</span>
                </div>
            </div>
            <div class="ModuleContent">

                <div class="ModuleList">
                    <div class="title">
                        <ul>
                            <li class="current">图文素材</li>
                            <li>分销活动</li>
                            <li>优惠券</li>
                            <li>招募海报</li>
                        </ul>
                    </div>
                    <div class="addTools">
                        <div class="commonBtn">添加工具</div>
                    </div>
                </div>
                <div class="ModuleTool">
                    <div class="contentsTool">
                        <div class="toolExmaple">
                            <div class="title">
                                <p>将品牌和商品的优势编辑成为微信图文</p>
                                <p>作为拓展工具提供给分销商，帮助分销商丰富分享内容，用品牌的力量协助分销商拓展分销事业！</p>
                            </div>
                            <p>示例</p>
                            <p class="imgText">
                               <img src="images/imgText.png">
                            </p>
                        </div>
                        <div class="toolDatas imageTextTool"></div>
                    </div>
                    <div class="contentsTool">
                        <div class="toolExmaple">
                            <p>将品牌、产品和分销政策等内容设置在创意活动中，帮助分销商实施专业的互动营销活动，吸引客户参与。</p>
                            <p>示例</p>
                            <p class="imgText">
                               <img src="images/distribution.png">
                            </p>
                        </div>
                        <div class="toolDatas saleActivityTool"></div>
                    </div>
                    <div class="contentsTool">
                        <div class="toolExmaple">
                            <p>设置优惠券，用优惠帮助分销商推动商品的销售，让分销商也能够做促销！</p>
                            <p>示例</p>
                            <p class="imgText">
                               <img src="images/coupon.png">
                            </p>
                        </div>
                        <div class="toolDatas couponTool"></div>
                    </div>
                    <div class="contentsTool">
                        <div class="toolExmaple">
                            <p>帮助分销商设置招募海报，分销商分享后，快速增长分销下线！</p>
                            <p>示例</p>
                            <p class="imgText">
                               <img src="images/poster.png">
                            </p>
                        </div>
                        <div class="toolDatas posterTool"></div>
                    </div>
                </div>
            </div>


         </div>
          <div class="btnWrap" style="border-top:1px solid #ccc;margin:0;margin-top:-5px;">
                <a href="javascript:;" id="saveSetOff" class="commonBtn cancelBtn ">确认发布</a>
              <a href="javascript:;" id="saveMessage" class="commonBtn saveBtn">发送通知</a>

          </div>
    </div>

	<!-- 集客行动信息确认 -->
    <div style="display: none">
        <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true">
            <div class="easyui-layout" data-options="fit:true" >
                <div data-options="region:'center'" style="padding: 10px;" id="panlconent">
                    指定的模板添加内容
                </div>
                <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height: 80px;
                    text-align: center;border-top:1px solid #e1e7ea;margin:0;">
                     <a class="easyui-linkbutton commonBtn cancelBtn"href="javascript:void(0)" onclick="javascript:$('#win').window('close')">取消</a>
                     <a class="commonBtn saveBtn">确定</a>
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
                    text-align: center;border-top: 1px solid #ccc;">
                     <a class="easyui-linkbutton commonBtn cancelBtn"href="javascript:void(0)" onclick="javascript:$('#winMessage').window('close')">取消</a>
                     <a class="easyui-linkbutton commonBtn saveBtn">确认</a>
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
                        text-align: center;border-top:1px solid #e1e7ea;">
                         <a class="easyui-linkbutton commonBtn cancelBtn"href="javascript:void(0)" onclick="javascript:$('#winCanel').window('close')">取消</a>
                         <a class="easyui-linkbutton commonBtn saveBtn">确定</a>
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
                               <ul>
                                    <li class="on" data-Field7="200"><em > 图文素材 </em></li>
                                    <li data-Field7="0"><em>分销活动 </em></li>
                                    <li data-Field7="100"><em>优惠券 </em></li>
                                    <li data-Field7="900"><em>集客海报</em></li>
                               </ul>
                            </div>
                            <div class="toolList">
                                <div class="text">
                                    <p><img src="images/icon3.png"></p>
                                    <p><b class="instruction">将品牌和商品的优势编辑成为微信图文,作为拓展工具提供给分销商，帮助分销商丰富分享内容，用品牌的力量协助分销商拓展分销事业！</b><a class="example">查看示例</a></p>
                                </div>
                                <div class="commonBtn">新建图文素材</div>
                                <div class="reload"><em>刷新</em></div>
                            </div>
                            <div id="notice" style="text-align:center;display:none; ">暂无活动，请新增相关活动</div>
                            <div class="dataTable" id="gridTable1" style="display:none;">
                                <div  class="loading" style="padding-top: 50px;">
                                     <span><img src="../static/images/loading.gif"></span>
                                </div>
                            </div>
                            <div class="dataTable" id="gridTable2" style="display:none;">
                                <div  class="loading" style="padding-top: 50px;">
                                     <span><img src="../static/images/loading.gif"></span>
                                </div>
                            </div>
                            <div class="dataTable" id="gridTable3" style="display:none;">
                                <div  class="loading" style="padding-top: 50px;">
                                     <span><img src="../static/images/loading.gif"></span>
                                </div>
                            </div>
                            <div id="setOfferPoster">
                                <div class="editArea">
                                    <em>海报名称</em><input type="text" placeholder="国庆节集客海报" id="setPosterName" data-id="" class="easyui-validatebox" data-options="required:true">
                                </div>
                                <div class="editArea">
                                    <p>帮助分销商设置招募海报，分销商分享后，快速增长分销下线！</p>
                                </div>
                                <div class="upImageModul" id="editLayer">
                                    <div class="upImage uploadItem"  data-flag=16  data-url="" data-batid="BackGround">
                                        <img src="" id="setOffBack" style="width:194px;height:300px;">
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
                                    <div class="posterExmaple">
                                        <p>示例</p>
                                        <p class="imgText">
                                           <img src="images/poster.png">
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="
                        text-align: center; padding: 20px 0 0;margin:0;">
                         <a class="easyui-linkbutton commonBtn cancelBtn "href="javascript:void(0)" onclick="javascript:$('#winTool').window('close')">取消</a>
                         <a class="easyui-linkbutton commonBtn saveBtn">确定</a>
                    </div>


    </div>
    <!--图文列表-->
    <script id="tpl_imgText" type="text/html">
          <div class="tplImgtext">
            <ul>
                <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="Material"){#>
                    <li data-id="<#=item.ObjectId#>" data-new="true" data-type="0" data-toolid="<#=item.SetoffToolId#>">
                    <b><#=i+1#>.</b>
                    <img src="<#=item.ImageUrl#>" style="width:40px;height:40px;">
                    <label>
                        <p><span class="name"><#=item.Name#></span></p>
                        <p><span class="name"><#=item.Text#></span></p>
                    </label>

                    <span  class="removeDlist" data-id="<#=item.SetoffToolID#>">取消发布</span></li>
                <#}else{#>
                    <li data-id="<#=item.TextId#>" data-type="0">
                    <b><#=i+1#></b>
                    <img src="<#=item.CoverImageUrl#>" style="width:40px;height:40px;">
                    <label>
                        <p><span class="name"><#=item.Title#></span></p>
                        <p><#=item.Text#></p>
                    </label>
                    <span  class="remove" data-id="<#=item.TextId#>">移除</span></li>
                <#}#>
                 <#}#>
            </ul>
          </div>
    </script>
    <!--活动列表-->
    <script id="tpl_action" type="text/html">
          <div class="tplAction">
            <ul>
                <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="CTW"){#>
                    <li data-id="<#=item.ObjectId#>" data-new="true" data-type="1" data-toolid="<#=item.SetoffToolId#>">
                    <b><#=i+1#>.</b>
                    <label>
                        <p><span class="name"><#=item.Name#></span></p>
                        <p><span><em>开始时间 ：</em><em class="staffStart"><#=item.BeginData#></em></span><span><em>结束时间 ：</em><em class="StaffEnd"><#=item.EndData#></em></span> </p>
                    </label>
                    <span  class="removeDlist" data-id="<#=item.SetoffToolID#>">取消发布</span></li>
                <#}else{#>
                    <li data-id="<#=item.CTWEventId#>" data-type="1">
                    <b><#=i+1#>.</b>
                    <label>
                        <p><span class="name"><#=item.Name#></span></p>
                        <p><span><em>开始时间 ：</em><em class="staffStart"><#=item.StartDate#></em></span><span><em>结束时间 ：</em><em class="StaffEnd"><#=item.EndDate#></em></span> </p>
                    </label>
                    <span  class="remove" data-id="<#=item.CTWEventId#>">移除</span></li>
                <#}#>
                 <#}#>
            </ul>
          </div>
    </script>

    <!--优惠券列表-->
    <script id="tpl_coupon" type="text/html">
          <div class="tplCoupon">
            <ul>
            <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="Coupon"){#>
                    <li data-id="<#=item.ObjectId#>" data-new="true" data-type="2" data-toolid="<#=item.SetoffToolId#>">
                        <b><#=i+1#>.</b>
                        <label>
                            <p><span class="name"><#=item.Name#></span><span class="till">剩余<#=item.SurplusCount#>张</span> </p>
                            <#if(item.BeginData==""){#>
                                <p class="Time">领取后<#=item.ServiceLife#>天有效</p>
                            <#}else{#>
                                <p class="Time"><span><em>开始时间 ：</em><em class="staffStart"><#=item.BeginData#></em></span><span><em>结束时间 ：</em><em class="StaffEnd"><#=item.EndData#></em></span> </p>
                            <#}#>
                        </label>

                    <span class="removeDlist" data-id="<#=item.CouponTypeID#>">取消发布</span></li>

                <#}else{#>
                    <li data-id="<#=item.CouponTypeID#>" data-type="2">
                    <b><#=i+1#>.</b>
                    <label>
                        <p><span class="name"><#=item.CouponTypeName#></span><span class="till">剩余<#=item.SurplusQty#>张</span> </p>
                        <#if(item.BeginTimeDate==""){#>
                            <p class="Time">领取后<#=item.ServiceLife#>天有效</p>
                        <#}else{#>
                            <p class="Time"><span><em>开始时间 ：</em><em class="staffStart"><#=item.BeginTimeDate#></em></span><span><em>结束时间 ：</em><em class="StaffEnd"><#=item.EndTimeDate#></em></span> </p>
                        <#}#>

                    </label>
                <span class="remove" data-id="<#=item.CouponTypeID#>">移除</span></li>
                 <#}#>
                 <#}#>
            </ul>
          </div>
    </script>

    <!--集客海报列表-->
    <script id="tpl_poster" type="text/html">
          <div class="tplPoster">
            <ul>
                <#for(var i=0;i<list.length;i++){ var item=list[i];#>
                <#if(item.ToolType=="SetoffPoster"){#>
                    <li data-id="<#=item.ObjectId#>" data-url="<#=item.ImageUrl#>" data-new="true" data-type="3" data-toolid="<#=item.SetoffToolId#>">
                        <b><#=i+1#>.</b>
                        <img style="width:40px;height:40px;" src="<#=item.ImageUrl#>">
                        <label>
                            <p style="line-height: 50px;"><span class="name"><#=item.Name#></span></p>
                        </label>
                    <span class="removeDlist" data-id="<#=item.CouponTypeID#>">取消发布</span>
                    <span class="exitPoster">编辑</span>
                    </li>
                    <#}else{#>
                    <li data-id="<#=item.SetoffPosterID#>" data-url="<#=item.ImageUrl#>" data-type="3">
                        <b><#=i+1#>.</b>
                        <img style="width:40px;height:40px;" src="<#=item.ImageUrl#>">
                        <label>
                            <p style="line-height: 50px;"><span class="name"><#=item.Name#></span></p>
                        </label>
                        <span class="remove">删除</span>
                        </li>
                    </li>
                 <#}#>
                 <#}#>
            </ul>
          </div>
    </script>

    <!--信息确认 二级模板-->
    <script id="tpl_Info" type="text/html">
        <div class="contents">
            <p>确认发布拓展工具？</p>
            <p><span class="checkBox on"></span><b>发送通知</b></p>
        </div>
    </script>

    <!--短信通知-->
    <script id="tpl_Message" type="text/html">
        <div class="contents" style="height:100px;margin-left:20px;">
            <div class="editCheck" data-type="3">
                <span class="opt on checkBox"></span><p data-name="分销拓展工具通知">通知分销商-发送到微超级分销APP-通知</p>
            </div>
            <div class="editMessage">
                <textarea maxlength="50">分销拓展工具已经发布，快来使用可以更好帮助您拓展分销商哦！</textarea>
                <span><b>29</b>/50</span>
            </div>
        </div>
        <div class="contents" style="height:100px;margin-left:20px;">
            <div class="editCheck" data-type="2">
                <span class="opt on checkBox"></span><p data-name="分销拓展工具通知">通知员工-发送到连锁掌柜APP-总部消息</p>
            </div>
            <div class="editMessage">
                <textarea maxlength="50">分销拓展工具已经发布，快来使用可以更好帮助您拓展分销商哦！</textarea>
                <span><b>29</b>/50</span>
            </div>
        </div>
        <div class="contents" style="height:100px;margin-left:20px;">
            <div class="editCheck" data-type="1">
                <span class="opt on checkBox"></span><p data-name="分销拓展工具通知">通知会员-发送到微信会员中心-通知</p>
            </div>
            <div class="editMessage" maxlength="50">
                <textarea>分销拓展工具已经发布，快来使用可以更好帮助您拓展分销商哦！</textarea>
                <span><b>29</b>/50</span>
            </div>
        </div>
    </script>
</asp:Content>
