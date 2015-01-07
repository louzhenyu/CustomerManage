<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>提现记录</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <link href="<%=StaticUrl+"/module/VipManage/css/reset-pc.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/VipManage/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/kkpager.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/jquery.datetimepicker.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/artDialog.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/tip-yellowsimple/tip-yellowsimple.css"%>" rel="stylesheet" type="text/css" />
        <link href="<%=StaticUrl+"/module/static/css/zTreeStyle/zTreeStyle.css"%>" rel="stylesheet" type="text/css" />
        <%--<link href="../static/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />--%>
        <style type="text/css">
            .commonTitle{height:16px;line-height:16px;margin:15px 0 5px 53px;padding-left:8px;border-left:4px solid #fe7c23;color:#666;margin-bottom: 20px;}
            .commonSelectWrap .selectBox {
                margin-left:0px;
            }
            .commonSelectWrap input {
                border:1px solid #ccc;
            }
			
			
			.queryTermArea{min-height:65px;padding:16px 20px 0 0px;}
			.queryTermArea .queryBtn{float:right;height:33px;line-height:33px;}
			.queryTermArea .commonSelectWrap .tit{max-width:80px;}
			.queryTermArea .commonSelectWrap{margin-right:10px;}
			.commonSelectWrap .selectList{top:3px;border-top:2px solid #fe7c24;}
			.commonSelectWrap .selectList p:hover{color:#fe7c24;}
			
			.tableHandleBox{text-align:left;}
			.tableHandleBox .exportBtn{float:right;margin-right:10px;}
			.tableHandleBox .affirmBtn{float:left;}
			.tableHandleBox .finishBtn{}
			
        </style>
</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<body cache>
<div class="allPage" id="section" data-js="js/withdrawDeposit.js?ver=0.1">
    <!-- 内容区域 -->
    <div class="contentArea_vipquery">
        <!--个别信息查询-->
        <div class="queryTermArea" id="simpleQuery" style="display:inline-block;width: 100%; " >
            <a href="javascript:;" class="commonBtn queryBtn">查询</a>
            <div class="commonSelectWrap">
                <em class="tit">提现单号：</em>
                <label class="searchInput"><input id="oddNumber" type="text" value=""></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">会员：</em>
                <label class="searchInput"><input  id="vipName" type="text" value=""></label>
            </div>
            
            <div class="commonSelectWrap">
                <em class="tit">状态：</em>
                <div class="selectBox">
                    <span class="text">请选择</span>
                    <div class="selectList">
                    	<p data-statusid=''>请选择</p>
                        <p data-statusid=0>待确认</p>
                        <p data-statusid=1>已确认</p>
                        <p data-statusid=2>已完成</p>
                    </div>
                </div>
            </div>
                    
            
            
        </div>

        <!--表格操作按钮-->
        <div id="menuItems" class="tableHandleBox">
            <!--<span class="commonBtn _addVip">添加新会员</span>-->
            <span class="commonBtn exportBtn">打印</span>
            <span class="commonBtn affirmBtn" data-statusid=1>确认</span>
            <span class="commonBtn finishBtn" data-statusid=2>完成</span>
            
            <%--<span class="commonBtn _delVip">删除</span>--%>
        </div>    
        <div class="tableWrap">
            <div class="tablehandle">
                <h3 class="count">共查询到<span id="resultCount">--</span>条数据</h3>  
            </div>
            <table class="dataTable" style="display:inline-table;">
                <thead  id="thead">
                    <tr class="title">
                        <th>选择</th>
                        <th>提现单号</th>
                        <th>申请日期</th>
                        <th>会员</th>
                        <th>会员ID</th>
                        <th>提现金额（元）</th>
                        <th>状态</th>
                        <th>完成日期</th>
                    </tr>
                </thead>
                <tbody id="content">
                	<tr><td colspan="8"><img src="../static/images/loading.gif"></td></tr>
                    <!--<tr>
                        <td class="checkBox" data-id='1212'><em></em></td>
                        <td>1236666666</td>
                        <td>2014-01-01</td>
                        <td>丁丁</td>
                        <td>888888</td>
                        <td>100.00</td>
                        <td>待确认</td>
                        <td>2014-01-05</td>
                    </tr>-->
                </tbody>
            </table>
            <div id="pageContianer">
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>
    </div>
</div>
<!-- 弹层,遮罩 -->
<div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>


<!-- 数据表格 -->
<script id="tpl_content" type="text/html">
<tr>
    <td class="checkBox" data-id='<#=ApplyID#>'><em></em></td>
    <td><#=WithdrawNo#></td>
    <td><#=ApplyDate#></td>
    <td><#=VipName#></td>
    <td><#=VipID#></td>
    <td><#=Amount#></td>
    <td>
		<#if(Status == 0){#>
			待确认
		<#}else if(Status == 1){#>
			已确认
		<#}else if(Status == 2){#>
			已完成
		<#}#>
	</td>
	<td><#=CompleteDate#></td>
</tr>
</script>


<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async data-main="<%=StaticUrl+"/module/VipManage/js/main.js"%>" ></script>
    </body>
</asp:Content>