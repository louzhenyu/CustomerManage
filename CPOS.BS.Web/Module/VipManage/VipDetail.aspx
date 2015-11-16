<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员详情</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <body></body>--%>
    <div class="allPage" id="section" data-js="js/vipDetail">
        <!-- 内容区域 -->
        <div>
            <div class="vipDetailInfo">
             <!--   <div class="commonTitle">
                    <a class="before" href="javascript:history.back();">会员查询</a>
                    <img src="images/pointerTit.png" />
                    <span class="after">会员详情</span>
                </div>-->
                <div>
                    <div class="item">
                        <div class="itemBox">
                            <em class="tit">会员编号：</em>
                            <p class="itemText" id="vipCode">
                                --</p>
                        </div>
                        <div class="itemBox">
                            <em class="tit">会员姓名：</em>
                            <p class="itemText" id="vipName">
                                --</p>
                        </div>
                        <div class="itemBox" style="display: none ">
                            <em class="tit">微信昵称：</em>
                            <p class="itemText" id="vipWeixin">
                                --</p>
                        </div>
                           <div class="itemBox" >
                                                    <em class="tit">手机号：</em>
                                                    <p class="itemText" id="phone">
                                                        --</p>
                                                </div>
                    </div>
                    <div class="item">
                        <div class="itemBox"  style="display: none ">
                            <em class="tit">会员等级：</em>
                            <p class="itemText" id="vipLevel">
                                --</p>
                        </div>
                        <div class="itemBox"  style="display: none ">
                            <em class="tit">会籍店：</em>
                            <p class="itemText" id="vipUnit">
                                --</p>
                        </div>
                        <div class="itemBox"  >
                            <em class="tit">现有积分：</em>
                            <p class="itemText" id="vipPoint">
                                0积分</p>
                        </div>
                        <div class="itemBox"  >
                                                    <em class="tit">会员卡类型：</em>
                                                    <p class="itemText" id="VipCardTypeName">
                                                       </p>
                                                </div>
                    </div>

                    <div class="item line"   style="display: none ">
                        <div class="itemBox">
                            <em class="tit">余额：</em>
                            <p class="itemText" id="vipBalance">
                                0元</p>
                        </div>
                    </div>
                    <div class="tagItem"  style="display: none ">
                        <em class="tit">动态标签：</em>
                        <p id="labels" class="lab clearfix">
                        </p>
                    </div>
                </div>
            </div>
            <!--会员详情菜单-->
            <div class="subMenu">
                <ul class="clearfix">
                    <li data-id="nav01" class="nav01 on">会员信息</li>
                      <li data-id="nav05" class="nav05">会员卡</li>

                    <li data-id="nav03" class="nav03">积分</li>
                    <li data-id="nav04" class="nav04">优惠券</li>

                    <!--<li data-id="nav06" class="nav06">下线会员</li>-->
                    <li data-id="nav07" class="nav07">客服记录</li>
                  <!--  <li data-id="nav08" class="nav08">变更记录</li>-->
                    <li data-id="nav09" class="nav09">会员标签</li>
                     <li data-id="nav02" class="nav02">消费记录</li>
                </ul>
            </div>
            <div id="nav09" style="display: none;">
                <div class="panlnav09">
                    <p>
                        已添加标签</p>
                    <div class="tagbtnList" id="tagList">

                    </div>
                    <div id="selectTag" class="selectTag">
                        <p>
                            可选标签</p>
                        <div class="panltaglist">
                            <ul class="groupTag">
                                <li class="on">职业</li>
                                <li>年龄层</li>
                                <li>性别</li>
                            </ul>
                            <div class="fontC">
                                换一批</div>
                        </div>
                        <div class="groupTaglist" id="groupTagList">

                        </div>
                        <!--groupTaglist-->
                    </div>
                </div>
                <!--panlnav09-->
                <div class="btnList">
                    <div class="commonBtn" data-flag="save" data-authority="Save" data-type="groupsubmit">
                        保存</div>
                </div>
            </div>
            <!--idnav09-->
            <script type="text/html" id="tagTypeList">
                       <#for(var i=0;i<list.length;i++){var item=list[i];#>
                        <li  data-tagid="<#=item.TypeId#>" ><#=item.TypeName#></li>
                     <#}#>
            </script>
            <script type="text/html" id="groupTagBtn">
               <#for(var i=0;i<list.length;i++){var item=list[i];#>
                <div class="tagbtn" data-tagid="<#=item.TagsId#>"><#=item.TagsName#></div>
             <#}#>
              <div class="textbtn">
                <input type="text" id="TagsName" value=""> <div class="saveTagBtn tagbtn" data-flag="add">添加</div>
              <div>
            </script>
            <script type="text/html" id="tagListBtn">
           <#for(var i=0;i<list.length;i++){var item=list[i];#>
               <div class="tagbtn" data-tagid="<#=item.TagId#>" data-tagname="<#=item.TagName#>"> <#=item.TagName#> <em class="icon" style="display: none;"></em></div>
           <#}#>
            </script>
            <div id="nav07" style="display: none;">
                <div class="tableHandleBox">
                    <!-- <span class="commonBtn export">全部导出</span>-->
                    <span class="commonBtn addICon L"  data-authority="Add">新增客服记录</span>
                </div>
                <div  id="servicesLog" class="gridLoading">

                       <div  class="loading">
                                    <span>
                                        <img src="../static/images/loading.gif"></span>
                                </div>
                </div>
                  <div id="pageContianer">
                   <div class="dataMessage" >该会员没有客服记录</div>


                    </div>
            </div>
            <div id="nav08" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                时间
                            </th>
                            <th>
                                操作人
                            </th>
                            <th>
                                操作事项
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblLogs">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="4"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <script id="tpl_logs" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.logid#>"><em></em></td>
                        <td><#=item.createtime#></td>
                        <td><#=item.cu_name#></td>
                        <td><#=item.action#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--上线与下线-->
            <div id="nav06" style="display: none;">
                <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>
                <table class="dataTable" style="display: inline-table;">
                    <thead>
                        <tr class="title">
                            <th class="selectListBox">
                                选择<div class="minSelectBox">
                                    <em class="minArr"></em>
                                    <p>
                                        全选本页</p>
                                    <p>
                                        全选所有页</p>
                                    <p>
                                        取消选择</p>
                                </div>
                            </th>
                            <th>
                                会员编号
                            </th>
                            <th>
                                微信昵称
                            </th>
                            <th>
                                姓名
                            </th>
                            <th>
                                等级
                            </th>
                            <th>
                                积分
                            </th>
                            <th>
                                下线数
                            </th>
                            <th>
                                入会时间
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tblOnline">
                        <tr>
                            <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="7"
                                align="center">
                                <span>
                                    <img alt="loading..." src="../static/images/loading.gif" /></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <script id="tpl_online" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipId#>"><em></em></td>
                        <td><#=item.VipCode#></td>
                        <td><#=item.VipName#></td>
                        <td><#=item.VipRealName#></td>
                        <td><#=item.VipCardGradeName#></td>
                        <td><#=item.EndIntegral#></td>
                        <td><#=item.OfflineCount#></td>
                        <td><#=item.CreateTime#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <div id="nav01">
                <div class="baseInfoArea">
                    <%--<div class="commonSelectWrap">
            	    <em class="tit">姓名:</em>
                    <p class="searchInput"><input type="text" id="editVipRealName" value=""></p>
                </div>
                <div class="commonSelectWrap">
            	    <em class="tit">昵称:</em>
                    <p class="searchInput"><input type="text" id="editVipName" value=""></p>
                </div>
                <div class="commonSelectWrap">
            	    <em class="tit">手机号:</em>
                    <p class="searchInput"><input type="text" id="editPhone" value=""></p>
                </div>
                <div class="commonSelectWrap">
            	    <em class="tit">会籍店:</em>
                    <p class="searchInput"><input type="text" id="editStore" value=""></p>
                </div>--%>
                    <div class="promptContent">
                    </div>
                    <div class="btnWrap">
                        <a href="javascript:;"  data-authority="SaveVipInfo" class="commonBtn saveBtn">保存</a>
                    </div>
                </div>
                <script id="tpl_EditVipForm" type="text/html">
            <#var jsonColumns = JSON.parse(Data.JsonColumns);console.log(jsonColumns)#>
            <#var vipInfo = Data.VipInfo[0];console.log("vipInfo",vipInfo);#>
            <#var subRoot=jsonColumns.Root.SubRoot;subRoot=subRoot?subRoot:[];#>
                <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i];var theCount=0;var findResult="";var theCount2=0;var findResult=""#>
                        <#if(item.DisplayType==1||item.DisplayType==3 || item.DisplayType==4 || item.DisplayType==7 ||item.DisplayType==2){#>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <label class="searchInput">
                                <input <#=(item.IsRead==1)?"disabled":""#> data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo" type="text" value="<#=vipInfo[item.ColumnName]?vipInfo[item.ColumnName]:''#>"></label>
                            </div>
                        <#}#>
                        <#if(item.DisplayType==5){#>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <div class="selectBox">
                                    <#for(var kt=0,ktlength=item.Fn.length;kt<ktlength;kt++){ var ktitem=item.Fn[kt];#>
                                        <#if(ktitem.OptionID==vipInfo[item.ColumnName]){#>
                                            <#theCount2++;findResult2=ktitem;break;#>
                                        <#}#>
                                    <#}#>
                                    <#if(theCount2>0&&findResult2!=""){#>
                                        <span class="text" <#=(item.IsRead==1)?"disabled":""#> data-forminfo="<#=JSON.stringify(item)#>" name="editvipinfo"  optionid="<#=ktitem.OptionID#>"><#=ktitem.OptionValue#></span>
                                    <#}else{#>
                                        <span class="text" <#=(item.IsRead==1)?"disabled":""#> data-forminfo="<#=JSON.stringify(item)#>" name="editvipinfo"   optionid="">请选择</span>
                                    <#}#>

                                    <div class="selectList">
                                        <#if(item.Fn instanceof Array){#>
                                             <#if(item.Fn&&item.Fn.length){#>
                                                <#for(var j=0;j<item.Fn.length;j++){var sel=item.Fn[j];#>
                                                    <p data-optionid="<#=sel.OptionID#>"><#=sel.OptionValue#></p>
                                                <#}#>
                                            <#}#>
                                        <#}else{#>
                                            <p data-optionid="<#=item.Fn.OptionID#>"><#=item.Fn.OptionValue#></p>
                                        <#}#>
                                    </div>
                                </div>
                            </div>
                        <#}#>
                        <#if(item.DisplayType==205){ #>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <label class="searchInput">
                                <#for(var kk=0,length2=item.Fn[0].Tree.length;kk<length2;kk++){ var kitem=item.Fn[0].Tree[kk];#>
                                    <#if(kitem.UnitID==vipInfo[item.ColumnName]){ theCount++;findResult=kitem;#>

                                    <# break;}else{#>

                                    <#}#>
                                <#}#>
                                <#if(findResult!=""&&theCount>0){#>
                                    <input <#=(item.IsRead==1)?"disabled":""#>  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"   name="editvipinfo" unitId="<#=findResult.UnitID#>"  type="text" value="<#=findResult.UnitName#>">
                                <#}else{#>
                                    <input <#=(item.IsRead==1)?"disabled":""#>  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"   name="editvipinfo"   type="text" >
                                <#}#>
                                </label>
                                <ul id="ztree<#=Math.floor(Math.random()*9999999999+1)#>"  data-forminfo="<#=JSON.stringify(item.Fn[0].Tree)#>" class="ztree" style="display:none;position: absolute;left: 120px;background:#FFF;margin-top: 31px;width:173px;z-index:100;"></ul>
                            </div>
                        <#}#>
                        <#if(item.DisplayType==6){#>
                            <div class="commonSelectWrap">
                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                <label class="searchInput"><input class="datepicker "  <#=(item.IsRead==1)?"disabled":""#> data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo" type="text" value="<#=vipInfo[item.ColumnName]#>"></label>
                            </div>
                        <#}#>
                    <#}#>
                </script>
                                <script id="tpl_EditVipForm1" type="text/html">
                            <#var jsonColumns = JSON.parse(Data.JsonColumns);console.log(jsonColumns)#>
                            <#var vipInfo = Data.VipInfo[0];console.log("vipInfo",vipInfo);#>
                            <#var subRoot=jsonColumns.Root.SubRoot;subRoot=subRoot?subRoot:[];#>
                                <#for(var i=0,length=subRoot.length;i<length;i++){ var item=subRoot[i]; item.verify=""; var theCount=0;var findResult="";var theCount2=0;var findResult=""#>
                                        <#if(item.DisplayType==1||item.DisplayType==3 || item.DisplayType==4 || item.DisplayType==7 ||item.DisplayType==2){#>
                                            <div class="commonSelectWrap"  data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo">
                                                <em class="tit"><#=item.ColumnDesc#>:</em>
                                                <div class="searchInput">
                                               <#if(item.ColumnDesc.indexOf("手机")!=-1){item.verify="mobile"}#>
                                               <#if(item.ColumnDesc.indexOf("身份证")!=-1){item.verify="idCardNo"}#>
                                                <input  <#=(item.IsRead==1)?"disabled":""#> type="text" name="<#=item.ColumnName#>"  class="easyui-validatebox"  data-options="required:true,validType:'<#=item.verify#>'" value="<#=vipInfo[item.ColumnName]?vipInfo[item.ColumnName]:''#>">
                                                </div>
                                           </div>
                                        <#}#>
                                        <#if(item.DisplayType==5){#>
                                            <div class="commonSelectWrap">
                                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                                <div class="selectBox">
                                                    <#for(var kt=0,ktlength=item.Fn.length;kt<ktlength;kt++){ var ktitem=item.Fn[kt];#>
                                                        <#if(ktitem.OptionID==vipInfo[item.ColumnName]){#>
                                                            <#theCount2++;findResult2=ktitem;break;#>
                                                        <#}#>
                                                    <#}#>
                                                    <#if(theCount2>0&&findResult2!=""){#>
                                                        <span class="text" <#=(item.IsRead==1)?"disabled":""#> data-forminfo="<#=JSON.stringify(item)#>" name="editvipinfo"  optionid="<#=ktitem.OptionID#>"><#=ktitem.OptionValue#></span>
                                                    <#}else{#>
                                                        <span class="text" <#=(item.IsRead==1)?"disabled":""#> data-forminfo="<#=JSON.stringify(item)#>" name="editvipinfo"   optionid="">请选择</span>
                                                    <#}#>

                                                    <div class="selectList">
                                                        <#if(item.Fn instanceof Array){#>
                                                             <#if(item.Fn&&item.Fn.length){#>
                                                                <#for(var j=0;j<item.Fn.length;j++){var sel=item.Fn[j];#>
                                                                    <p data-optionid="<#=sel.OptionID#>"><#=sel.OptionValue#></p>
                                                                <#}#>
                                                            <#}#>
                                                        <#}else{#>
                                                            <p data-optionid="<#=item.Fn.OptionID#>"><#=item.Fn.OptionValue#></p>
                                                        <#}#>
                                                    </div>
                                                </div>
                                            </div>
                                        <#}#>
                                        <#if(item.DisplayType==205){ #>
                                            <div class="commonSelectWrap">
                                                <em class="tit"><#=item.ColumnDesc#>：</em>
                                                <label class="searchInput">
                                                <#for(var kk=0,length2=item.Fn[0].Tree.length;kk<length2;kk++){ var kitem=item.Fn[0].Tree[kk];#>
                                                    <#if(kitem.UnitID==vipInfo[item.ColumnName]){ theCount++;findResult=kitem;#>

                                                    <# break;}else{#>

                                                    <#}#>
                                                <#}#>
                                                <#if(findResult!=""&&theCount>0){#>
                                                    <input <#=(item.IsRead==1)?"disabled":""#>  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"   name="editvipinfo" unitId="<#=findResult.UnitID#>"  type="text" value="<#=findResult.UnitName#>">
                                                <#}else{#>
                                                    <input <#=(item.IsRead==1)?"disabled":""#>  data-forminfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"   name="editvipinfo"   type="text" >
                                                <#}#>
                                                </label>
                                                <ul id="ztree<#=Math.floor(Math.random()*9999999999+1)#>"  data-forminfo="<#=JSON.stringify(item.Fn[0].Tree)#>" class="ztree" style="display:none;position: absolute;left: 120px;background:#FFF;margin-top: 31px;width:173px;z-index:100;"></ul>
                                            </div>
                                        <#}#>
                                        <#if(item.DisplayType==6){#>
                                            <div class="commonSelectWrap"  data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo">
                                                <em class="tit"><#=item.ColumnDesc#>:</em>
                                                <div class="searchInput borderNone">
                                               <#if(1==1){item.verify="dateTime"}#>
                                                <input  <#=(item.IsRead==1)?"disabled":""#> type="text" name="<#=item.ColumnName#>"  class="easyui-datebox"  data-options=" width:160,height:32,required:true,validType:'<#=item.verify#>'" value="<#=vipInfo[item.ColumnName]?vipInfo[item.ColumnName]:''#>">
                                                </div>
                                           </div>
                                        <#}#>
                                    <#}#>
                                </script>
            </div>
            <!--积分明细-->
            <div id="nav03" style="display: none;">
            <div class="optPanel">
                 <div class="commonSelectWrap">
                  <em class="tit">累计积分:</em>
                  <div class="inputBox" id="CumulativeIntegral">

                  </div>

                 </div>
                 <div class="commonSelectWrap">
                                  <em class="tit">当前积分:</em>
                                  <div class="inputBox fontC" id="VipIntegral">

                                  </div>

                                 </div>
                 <div class="commonBtn adjust" data-authority="Adjust">调整</div>
            </div>
            <div class="lineTitle">
             积分变动记录
            </div>
                 <!--<div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>-->
                <div id="tblPoint" class="gridLoading">

                       <div  class="loading">
                                    <span>
                                        <img src="../static/images/loading.gif"></span>
                                </div>



                </div>
                 <div id="pageContianer">
                        <div class="dataMessage" >该会员没有积分的操作记录</div>

                   </div>
                <!--分页-->
                <div id="kkpager" style="text-align: center;">
                </div>
                <script id="tpl_point" type="text/html">
                <#for(var i=0;i<list.length; i++){ var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipIntegralId#>"><em></em></td>
                        <td><#=item.Date#></td>
                        <td><#=item.Integral#></td>
                        <td><#=item.VipIntegralSource#></td>
                        <td>
                            <#=item.Remark#>
                        </td>
                    </tr>
                <#}#>
                </script>
            </div>

            <!--帐内余额-->
            <div id="nav04" style="display: none;">
               <!-- <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>-->
                <div id="tblAmount" class="gridLoading">

                       <div  class="loading">
                                    <span>
                                        <img src="../static/images/loading.gif"></span>
                                </div>

                </div>
                <div id="pageContianer">
                  <div class="dataMessage" >该会员没有可使用的优惠券</div>


                 </div>
                <script id="tpl_amount" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.VipAmountId#>"><em></em></td>
                        <td><#=item.Date#></td>
                        <td><#=item.Amount#>元</td>
                        <td><#=item.VipAmountSource#></td>
                        <td><#=item.Remark#></td>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--消费卡-->
            <div id="nav05" style="display: none;">
               <!-- <div class="tableHandleBox">
                    <span class="commonBtn export">全部导出</span>
                </div>-->
                <div  id="tblConsumer" class="imageList" tyle="display: inline-table">

                </div>
                <script id="tpl_consumer" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                         <div class="line">
                             <a class="textA" href="/module/vipCardManage/vipCardDetail.aspx?VipCardID=<#=item.VipCardID#>&mid=<#=mid#>" >
                               <img src=" <#=item.ImageUrl#>" width="270" height="135">
                               <p >点击图片可查看该卡详细信息</p>
                         </a>
                         <div class="lineText">
                          <div class="commonSelectWrap">
                                  <em class="tit">卡号:</em>
                                  <div class="inputBox">
                                            <#=item.VipCardCode#>
                                  </div>

                                 </div>  <!--commonSelectWrap-->
                                <!-- 0未激活，1正常，2冻结，3失效，4挂失，5休眠)-->
                          <div class="commonSelectWrap">
                                  <em class="tit">会员卡状态:</em>
                                  <div class="inputBox fontC">
                                            <#=item.VipCardStatusName#>
                                  </div>

                                 </div>  <!--commonSelectWrap-->
                          <div class="commonSelectWrap">
                                  <em class="tit">办卡日期:</em>
                                  <div class="inputBox">
                                            <#=item.MembershipTime#>
                                  </div>

                                 </div>  <!--commonSelectWrap-->
                          <div class="commonSelectWrap">
                                  <em class="tit">卡类别:</em>
                                  <div class="inputBox">
                                            <#=item.VipCardName#>
                                  </div>

                                 </div>  <!--commonSelectWrap-->
                          <div class="commonSelectWrap">
                                  <em class="tit">办卡门店:</em>
                                  <div class="inputBox">
                                           <#=item.MembershipUnitName#>
                                  </div>

                                 </div>  <!--commonSelectWrap-->
                           <div class="commonSelectWrap">
                                   <em class="tit">当前余额:</em>
                                   <div class="inputBox">
                                            <#=item.BalanceAmount#>
                                   </div>

                                  </div>  <!--commonSelectWrap-->
                             <div class="commonSelectWrap">
                                                          <em class="tit">售卡员工:</em>
                                                          <div class="inputBox">
                                                                   <#=item.SalesUserName#>
                                                          </div>

                                                         </div>  <!--commonSelectWrap-->

                         </div> <!--lineText-->

                         </div>
                    </tr>
                <#}#>
                </script>
            </div>
            <!--交易记录-->
            <div id="nav02" style="display: none;">
                <!--<div class="tableHandleBox">
                    <%--<span class="commonBtn">添加新会员</span>--%>
                    <span class="commonBtn export">全部导出</span>
                </div>-->
                <div class="tableWrap">
                    <%--<div class="tablehandle">
                <div class="selectBox">
                    <span class="text">按最近时间升序</span>
                    <div class="selectList">
                        <p>按最近时间降序</p>
                        <p>按最近时间升序</p>
                    </div>
                </div>
                <div class="selectBox fl">
                    <span class="text">操作</span>
                    <div class="selectList">
                        <p>操作1</p>
                        <p>操作2</p>
                    </div>
                </div>
                
                <div class="selectBox filterIcon fl">
                    <span class="text">筛选</span>
                    <div class="selectList">
                        <p>筛选1</p>
                        <p>筛选2</p>
                    </div>
                </div>
                
            </div>--%>
                    <!-- 已确认名单表格 -->
                    <div id="content"  class="gridLoading">
                      <!--  <thead>
                            <tr class="title">
                                <th class="selectListBox">
                                    选择<div class="minSelectBox">
                                        <em class="minArr"></em>
                                        <p>
                                            全选本页</p>
                                        <p>
                                            全选所有页</p>
                                        <p>
                                            取消选择</p>
                                    </div>
                                </th>
                                <th>
                                    操作
                                </th>
                                <th>
                                    订单编号
                                </th>
                                <th>
                                    交易时间
                                </th>
                                <th>
                                    下单方式
                                </th>
                                <th>
                                    交易门店
                                </th>
                                <th>
                                    交易金额
                                </th>
                                <th>
                                    支付状态
                                </th>
                                <th>
                                    支付方式
                                </th>
                                <th>
                                    订单状态
                                </th>
                            </tr>
                        </thead>-->

                       <div  class="loading">
                                    <span>
                                        <img src="../static/images/loading.gif"></span>
                                </div>

                    </div>
                     <div id="pageContianer">
                                      <div class="dataMessage" >该会员没有任何消费记录</div>


                                     </div>
                    <!--分页-->
                </div>
            </div>
            <!--表格操作按钮-->
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
    </div>
    <!--百度模板渲染模板 数据部分-->
    <!--//新增客服界面-->
    <script id="tpl_addCustomer" type="text/html">
    <form id="optionform">
     <input name="ServicesLogID" type="text" style="display: none" title="编辑时候保存id">
              <!-- <div class="commonSelectWrap">
                     <em class="tit">服务时间：</em>
                    <div class="searchInput bonone">
                       <input type="text" class="easyui-datetimebox" name="ServicesTime" data-options="width:160,height:32,showSeconds:false" />
                   </div>
               </div>-->

           <div class="commonSelectWrap"style="width: 628px">
                 <em class="tit">服务方式：</em>
                <div class="searchInput" style="width: 500px; border: none">
                    <div class="radio L on" data-name="r1"><em></em> <span> 到店</span></div>
                    <div class="radio L" data-name="r1"><em></em> <span> 电话</span></div>
                    <div class="radio L" data-name="r1"><em></em> <span> 微信</span></div>
                    <div class="radio L out" data-name="r1"><em></em> <span> 其他</span></div>
                    <input type="text" class="easyui-validatebox L" name="ServicesMode"  style="border:1px solid #ccc;width:160px;height:32px "  />
               </div>
           </div>
         <div class="commonSelectWrap">
                                <em class="tit">服务门店：</em>
                               <div class="searchInput bonone">
                                  <input type="text" name="UnitName" readonly="readonly"  id="searchInputUnitName"  />
                              </div>
                          </div>
         <div class="commonSelectWrap">
                                <em class="tit">服务员工：</em>
                               <div class="searchInput bonone">
                                      <input type="text" id="userName" readonly="readonly" name="UserName" />
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
                 <em class="tit">服务内容：</em>
                <div class="searchInput remark" >
                 <textarea type="text" name="Content" class="easyui-validatebox" data-options="required:true,validType:'maxLength[50]'" placeholder="请输入不超过50个以内的字符"></textarea>

               </div>
           </div>


     </form>
    </script>

    <!--调整积分-->
 <script id="tpl_adjustIntegral" type="text/html">
    <form id="optionform">

               <div class="commonSelectWrap">
                     <em class="tit">当前积分：</em>
                    <div class="searchInput bonone">
                               <input name="VipIntegral" type="text" disabled="disabled"/>
                   </div>
               </div>


         <div class="commonSelectWrap" style="float: none; width: 500px;">
                                <em class="tit">调整数量：</em>
                               <div class="searchInput bonone">
                                  <input type="text" name="Qty" class="easyui-numberbox"  data-options="precision:0,width:100,height:32,required:true,"  />
                              </div>
                          </div>
         <div class="commonSelectWrap">
                                <em class="tit">原因：</em>
                               <div class="searchInput bonone " style="width: 460px;">
                                     <input  name="Reason" class="easyui-combobox" data-options="validType:'selectIndex',editable:false,width:460,height:32,
                                     		valueField: 'label',
                                     		textField: 'value',
                                     		data: [{
                                     			label: '处理投诉',
                                     			value: '处理投诉'
                                     		},{
                                     			label: '法定假日',
                                     			value: '法定假日'
                                     		},
                                     		{
                                     			label: '积分期限',
                                     			value: '积分期限'
                                     		},
                                     		{
                                     			label: '营销活动',
                                     			value: '营销活动'
                                     		}
                                          ,{
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
              <div class="commonSelectWrap" style="height:80px;">
                 <em class="tit">备注：</em>
                <div class="searchInput remark" >
                   <textarea type="text" name="remark" class="easyui-validatebox" data-options="validType:'maxLength[50]'" placeholder="请输入不超过50个以内的字符"></textarea>
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

    <script id="tpl_content" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){ var item=list[i]; #>
        <tr>
            <td class="checkBox"><em></em></td>
            <td class="seeIcon" data-orderid="<#=item.OrderId#>"></td>
            <td class="fontC"><#=item.OrderNo#></td>
            <td><#=item.CreateTime#></td>
            <td></td>
            <td><#=item.PayUnitName#></td>
            <td><#=item.PayAmount#></td>
            <td><#=item.PayStatus#></td>
            <td class="fontF"><#=item.PayType#></td>
            <td class="fontF"><#=item.OrderStatus#></td>
        </tr>
<#} #>
    </script>
    <!--会员标签-->
    <script id="tpl_vipTag" type="text/html">
<#for(var i=0,length=list.length;i<length;i++){  var item=list[i];#>
<span><#=item.TagName#></span>
<#}#>
    </script>
    <!--table没数据的提示-->
    <script id="tpl_noContent" type="text/html">
<tr >
    <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><#=tips#></span></td>
</tr>
    </script>



        <!--收款-->
         <script id="tpl_OrderPayMent" type="text/html">
            <form id="fromOption">


            <div class="optionclass">
               <div class="commonSelectWrap amount">
                             <em class="tit">订单金额:</em>
                                <div class=" searchInput borderNone" >
                                 <input id="Amount" class=" easyui-numberbox " data-options="width:160,height:32, min:0,precision:2,disabled:true" name="actual_amount" readonly="readonly" style="border:none"/>
                               </div>
                </div>

                <div class="commonSelectWrap PayMethod">
                                        <em class="tit">付款方式：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="PayMethod"  class="easyui-combobox" data-options="width:160,height:32,required:true"  name="PayMethod" />
                                       </div>
                </div>
                <div class="commonSelectWrap CheckResult">
                                        <em class="tit">审核不通过理由：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="CheckResult"  class="easyui-combobox" data-options="width:160,height:32,required:true"  name="CheckResult" />
                                       </div>
                </div>

                <div class="commonSelectWrap DeliverCompany">
                                        <em class="tit">配送商：</em>
                                        <div class="searchInput bodernone" >
                                               <input id="DeliverCompany"  class="easyui-combobox" data-options="width:160,height:32,required:true"  name="DeliverCompany" />
                                       </div>
                </div>
                  <div class="commonSelectWrap DeliverOrder">
                                             <em class="tit">配送单号:</em>
                                                <div class="searchInput" >
                                                 <input class="easyui-validatebox" data-options="width:160,height:32" name="DeliverOrder" style="border:none"/>
                                               </div>
                                </div>

               </div>
                      <div class="commonSelectWrap">
                       <div class="tit">备注 </div>
                       <div class="searchInput" style="width:457px; height: 100px;">
                          <textarea type="text" id="Remark" name="Remark" class="easyui-validatebox,required:true" data-options="validType:'maxLength[50]'" placeholder="请输入不超过50个以内的字符"></textarea>

                         </div>

                      </div>
                </form>
                </script>



    <script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true"
        data-main="js/main.js"></script>
</asp:Content>
