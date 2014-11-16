<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="css/reset-pc.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
        <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/artDialog.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
        <link href="../static/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
        <style>
            .commonTitle{height:16px;line-height:16px;margin:15px 0 5px 53px;padding-left:8px;border-left:4px solid #fe7c23;color:#666;}
                .commonSelectWrap .selectBox {
                margin-left:0px;
            }
             </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <body></body>--%>
<div class="allPage" id="section" data-js="js/vipDetail">
    <!-- 内容区域 -->
    <div class="contentArea" style="margin-left:10px;">
        <div class="vipDetailInfo">
            <div class="commonTitle">
            	<a class="before" href="javascript:history.back();">会员查询</a>
                <img src="images/pointerTit.png" />
                <span class="after">会员详情</span>
            </div>
            <div>
            	<div class="item">
                    <div class="itemBox">
                        <em class="tit">会员编号：</em>
                        <p class="itemText" id="vipCode">--</p>
                    </div>
                    <div class="itemBox">
                        <em class="tit">会员姓名：</em>
                        <p class="itemText" id="vipName">--</p>
                    </div>
                    <div class="itemBox">
                        <em class="tit">微信昵称：</em>
                        <p class="itemText" id="vipWeixin">--</p>
                    </div>
                </div>
                <div class="item">
                	<div class="itemBox">
                        <em class="tit">会员等级：</em>
                        <p class="itemText" id="vipLevel">--</p>
                    </div>
                    <div class="itemBox">
                        <em class="tit">会籍店：</em>
                        <p class="itemText" id="vipUnit">--</p>
                    </div>
                    <div class="itemBox">
                        <em class="tit">会员积分：</em>
                        <p class="itemText" id="vipPoint">0积分</p>
                    </div>
                </div>
                <div class="item line">
                	<div class="itemBox">
                        <em class="tit">余额：</em>
                        <p class="itemText" id="vipBalance">0元</p>
                    </div>
                </div>
                
                <div class="tagItem">
                    <em class="tit">动态标签：</em>
                    <p id="labels" class="lab clearfix">
                        
                    </p>
                </div>
            </div>
        </div>
        
        <!--会员详情菜单-->
        <div class="subMenu">
            <ul class="clearfix">
                <li data-id="nav01"  class="nav01 on">基本信息</li>
                <li data-id="nav02" class="nav02">交易记录</li>
                <li data-id="nav03" class="nav03">积分明细</li>
                <li data-id="nav04" class="nav04">帐内余额</li>
                <li data-id="nav05" class="nav05">消费卡</li>
                <li data-id="nav06" class="nav06">上线与下线</li>
                <li data-id="nav07" class="nav07">客服记录</li>
                <li data-id="nav08" class="nav08">变更记录</li>
            </ul>
        </div>
        <div id="nav08" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div> 
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>时间</th>
                        <th>操作人</th>
                        <th>操作事项</th>
                    </tr>
                </thead>
                <tbody id="tblLogs">
                    <tr>
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="4" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
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
        <div id="nav06" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div> 
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>会员编号</th>
                        <th>微信昵称</th>
                        <th>姓名</th>
                        <th>等级</th>
                        <th>积分</th>
                        <th>下线数</th>
                        <th>入会时间</th>
                    </tr>
                </thead>
                <tbody id="tblOnline">
                    <tr>
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="7" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
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
                 <div class="promptContent"></div>
                <div class="btnWrap"> <a href="javascript:;" class="commonBtn saveBtn">保存</a> </div>
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
                                <label class="searchInput"><input class="datepicker"  <#=(item.IsRead==1)?"disabled":""#> data-formInfo="<#=JSON.stringify(item)#>" data-text="<#=item.ColumnDesc#>"  name="editvipinfo" type="text" value="<#=vipInfo[item.ColumnName]#>"></label>
                            </div>
                        <#}#>
                    <#}#>
                </script>
        </div>
        <!--积分明细-->
        <div id="nav03" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div> 
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>时间</th>
                        <th>积分变更</th>
                        <th>变更类型</th>
                        <th>备注</th>
                    </tr>
                </thead>
                <tbody id="tblPoint">
                    <tr >
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
                    </tr>
                </tbody>
            </table>
            <!--分页-->
            <div id="kkpager" style="text-align:center;"></div>
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
        <div id="nav04" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div> 
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>时间</th>
                        <th>余额</th>
                        <th>变更类型</th>
                        <th>备注</th>
                    </tr>
                </thead>
                <tbody id="tblAmount">
                    <tr >
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
                    </tr>
                </tbody>
            </table>
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
        <div id="nav05" style="display:none;">
            <div class="tableHandleBox">
                <span class="commonBtn export">全部导出</span>
            </div> 
            <table class="dataTable" style="display:inline-table">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>卡类型</th>
                        <th>卡名称</th>
                        <th>领卡方式</th>
                        <th>备注</th>
                        <th>状态</th>
                    </tr>
                </thead>
                <tbody id="tblConsumer">
                    <tr >
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><img alt="loading..." src="../static/images/loading.gif" /></span></td>
                    </tr>
                </tbody>
            </table>
            <script id="tpl_consumer" type="text/html">
                <#for(var i=0;i<list.length;i++){var item=list[i];#>
                    <tr>
                        <td class="checkBox" data-id="<#=item.CouponId#>"><em></em></td>
                        <td><#=item.CouponType#></td>
                        <td><#=item.CouponName#></td>
                        <td><#=item.CollarCardMode#></td>
                        <td><#=item.Remark#></td>
                        <td><#=item.CouponStatus#></td>
                    </tr>
                <#}#>
            </script>
        </div>
        <!--交易记录-->
        <div id="nav02" style="display:none;">
            <div class="tableHandleBox">
                <%--<span class="commonBtn">添加新会员</span>--%>
                <span class="commonBtn export">全部导出</span>
            </div>    
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
            <table class="dataTable" style="display:inline-table;">
                <thead>
                    <tr class="title">
                        <th class="selectListBox">选择<div class="minSelectBox"><em class="minArr"></em><p>全选本页</p><p>全选所有页</p><p>取消选择</p></div></th>
                        <th>操作</th>
                        <th>订单编号</th>
                        <th>交易时间</th>
                        <th>下单方式</th>
                        <th>交易门店</th>
                        <th>交易金额</th>
                        <th>支付状态</th>
                        <th>支付方式</th>
                        <th>订单状态</th>
                    </tr>
                </thead>
                <tbody id="content">
                    <tr >
                        <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="10" align="center"> <span><img src="../static/images/loading.gif"></span></td>
                    </tr>
                    
                </tbody>
            </table>
            <!--分页-->
        </div>
        </div>
        <!--表格操作按钮-->
    </div>
</div>
<!--百度模板渲染模板 数据部分-->
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
<script type="text/javascript" src="/Module/static/js/lib/require.min.js" defer async="true" data-main="js/main.js"></script>
</asp:Content>
