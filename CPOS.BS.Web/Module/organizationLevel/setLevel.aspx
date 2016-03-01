<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <meta charset="UTF-8" />
  <title>设置组织层级</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <link href="<%=StaticUrl+"/module/organizationLevel/css/setLevel.css?v=0.4"%>" rel="stylesheet" type="text/css" />
  <style type="text/css">
a:hover{color:#fff;}
#contentArea{background:#f4f8fa;min-height:630px !important;}
#section{padding:30px 20px 0 20px;}
.contentArea-info{border-radius:5px 5px 0 0;}
.panelDiv{margin-top:0;}
.contentArea-info .title{border:1px solid #d8d8d8;}
.contentArea-info .title li{width:50%;height:55px;}
.contentArea-info .title .itemNav{display:inline-block;height:55px;margin:0 auto;}
.contentArea-info .title li em{float:left;width:33px;height:55px;}
.contentArea-info .title li span{float:left;height:55px;line-height:55px;margin-left:10px;}
.contentArea-info .title .one em{background:url(images/icon-end1.png) no-repeat center center;}
.contentArea-info .title .one.on em{background:url(images/icon-on1.png) no-repeat center center;}
.contentArea-info .title .two{border-right:none;}
.contentArea-info .title .two em{background:url(images/icon-end2.png) no-repeat center center;}
.contentArea-info .title .two.on em{background:url(images/icon-on2.png) no-repeat center center;}

.borderArea{border:1px solid #d8d8d8;border-top:none;background:#fff;}
.clearBorder .combo,.clearBorder .numberbox{
	border: none;
	background: none;
}
.textbox-invalid {
	border-color: #d0d5d8;
	background-color: #fff;
}
.textbox-addon-right {
	right: 9px !important;
	top: 1px;
}
.inlineBlockArea {
	display: inline-block;
	width: 100%;
	min-height:450px;
	padding: 50px 0;
}
.inlineBlockArea .commonSelectWrap{float:none;display:block;width:432px;margin:0 auto;}
.inlineBlockArea .levelBox{display:none;}
.inlineBlockArea .levelBox .commonSelectWrap{display:none;margin-top:18px;}
.inlineBlockArea .text-tip{width:432px;margin:0 auto;padding:5px 0 0 132px;font-size:15px;color:#f00;}

.titleItem {display:inline-block;width:100%;height:41px;line-height:40px;margin-bottom:80px;padding-left:30px;font-size:16px;border-bottom:1px solid #e0e0e0;background:#eaeff1;color:#333;}

.commonHandleBtn{display:block;width:132px;height:32px;line-height:32px;text-align:center;font-size:14px;border-radius:4px;background:#07c8cf;color:#fff;}
.addPrizeArea .commonHandleBtn{}

.commonStepBtn{display:inline-block;width:148px;height:43px;line-height:43px;text-align:center;font-size:15px;border-radius:22px;color:#fff;}

/*设置组织层级*/
.frameworkArea{height:490px;}
.frameworkArea .subsetArea{position:relative;float:left;width:265px;height:100%;border-right:1px solid #d8d8d8;}
.frameworkArea .addBtnBox{position:absolute;bottom:0;left:0;width:100%;height:72px;text-align:center;border-top:1px solid #d8d8d8;}
.frameworkArea .addSubBtn{display:inline-block;margin-top:20px;}
.frameworkArea .handleArea{margin-left:265px;}
.frameworkArea .levelContentBox{}
.frameworkArea a.disableBtn{background:#ccc;cursor:default;}

.levelContentBox .commonSelectWrap{float:none;display:block;width:300px;height:33px;margin:0px auto 10px;position:relative;}
.contentArea-info .commonSelectWrap .tit{font-size:16px;color:#999;}
.levelContentBox .text-tip{width:500px;line-height:26px;margin:0 auto;text-indent:2em;font-size:14px;color:#f00;}

.levelContentBox .handleBtn{width:230px;padding:55px 0 25px 0;margin:0 auto;text-align:center;}
.levelContentBox .handleBtn a{display:inline-block;width:80px;height:30px;line-height:30px;}
.levelContentBox .handleBtn a.disableBtn{background:#ccc;cursor:default;}
.levelContentBox .handleBtn .delBtn{margin-left:15px;}

.levelInfoBox{width:200px;margin:0 auto;}
.levelItem{margin-bottom:10px;line-height:32px;font-size:16px;font-family:"黑体";color:#999;}
.levelItem .tit{float:left;width:85px;text-align:left;}
.levelItem>p{width:150px;margin-left:85px;}
.levelItem>p input{width:100%;height:34px;border:1px solid #fff;border-radius:5px;color#999;}

.treeBox{padding:15px;height:418px;overflow:auto;}
.tree-title{height:34px;font-size:15px;font-family:"黑体";}
.tree-node{height:25px;}
/*奖品配置，弹出*/
.jui-dialog-redPackage{width:636px;height:470px;position:fixed;top:90px;margin-left:-318px;}
.jui-dialog-tit{height:55px;}
.jui-dialog-tit h2{font:16px/55px "Microsoft YaHei";color:#66666d;}
.jui-dialog-close{position:absolute;top:0px;right:18px;width:30px;height:54px;background:url(images/icon-close-btn.png) no-repeat center center;cursor:pointer;}
.jui-dialog .tit{width:250px;}
.jui-dialog .redPackageContent{padding:10px 0 30px 0;}
.jui-dialog .searchInput{width:190px;}
.jui-dialog .commonSelectWrap{float:none;display:block;margin:20px 0 2px 0;}
.jui-dialog .hint-exp{padding-top:5px;text-indent:30px;text-align:center;font-size:14px;color:#999;}
.jui-dialog .btnWrap{padding:30px 0 0 0;}
.jui-dialog .commonBtn{width:150px;height:45px;line-height:45px;}
.jui-dialog .cancelBtn{border:none;background:#ccc;color:#fff;}


.commonSelectWrap .searchInput{font-family:"黑体";font-size:16px;color:#666;}


</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="allPage" id="section" data-js="js/setLevel.js?ver=0.3"> 
    <!-- 内容区域 -->
    <div class="contentArea-info">
      <!--个别信息查询-->
      <div class="queryTermArea" id="simpleQuery" style="display: inline-block; width: 100%;">
        <div class="title">
          <ul id="optPanel">
            <li data-flag="#nav01" class="on one"><div class="itemNav"><em></em><span>设置组织层级</span></div></li>
            <li data-flag="#nav02" class="two"><div class="itemNav"><em></em><span>设置组织架构</span></div></li>
          </ul>
        </div>
        
        <!-- 设置组织层级 -->
        <div class="panelDiv" id="nav01" data-index="0">
        <form></form>
          <form id="nav0_1">
          	<div class="borderArea">
                <!--<p class="titleItem" style="border-top:none;">基本信息</p>-->
                <div class="inlineBlockArea">
					<div class="commonSelectWrap">
                        <em class="tit" style="width:126px">选择组织层级：</em>
                        <label class="searchInput clearBorder" style="width:300px;">
                          <input data-text="选择组织层级" class="easyui-combobox" id="levelNameSelect" data-flag="LevelCount" name="LevelCount" type="text" value="" data-options="required:true,width:300,height:32"  validType='selectIndex'>
                        </label>
                      </div>
                      <p class="text-tip">提示：组织层级保存后不可修改</p>
                      
                      <div class="levelBox">
                          <div class="commonSelectWrap">
                            <em class="tit" style="width:126px">第一层级名称：</em>
                            <label class="searchInput clearBorder" style="width:300px;border:none;">&nbsp;总部</label>
                          </div>
                          <div class="commonSelectWrap">
                            <em class="tit" style="width:126px">第二层级名称：</em>
                            <label class="searchInput clearBorder" style="width:300px">
                              <input data-text="第二层级名称" class="easyui-validatebox"  data-flag="districtName" id="districtName" name="type_name" type="text" data-options="required:true"  value="" placeholder="区域">
                            </label>
                          </div>
                          <div class="commonSelectWrap">
                            <em class="tit" style="width:126px">第三层级名称：</em>
                            <label class="searchInput clearBorder" style="width:300px">
                              <input data-text="第三层级名称" class="easyui-validatebox"  data-flag="pieceName" id="pieceName" name="type_name" type="text" data-options="required:true"  value="" placeholder="片区">
                            </label>
                          </div>
                          <div class="commonSelectWrap">
                            <em class="tit" style="width:126px">第四层级名称：</em>
                            <label class="searchInput clearBorder" style="width:300px">
                              <input data-text="第四层级名称" class="easyui-validatebox"  data-flag="companyName" id="companyName" name="type_name" type="text" data-options="required:true"  value="" placeholder="代理公司">
                            </label>
                          </div>
                          <div class="commonSelectWrap">
                            <em class="tit" style="width:126px">第五层级名称：</em>
                            <label class="searchInput clearBorder" style="width:300px;border:none;">&nbsp;门店</label>
                          </div>
                          
                      
                      </div>
                </div>
            </div>
            <div class="btnWrap">
            	<a href="javascript:;" class="commonStepBtn nextStepBtn" data-flag="#nav02">保存</a>
            </div>
          </form>
        </div>
        
        <!--设置组织架构-->
        <div class="panelDiv" id="nav02" data-index="1">
          <form id="nav0_2">
            <div class="borderArea">
                <div class="frameworkArea">
                	<div class="subsetArea">
                    	<div class="treeBox"></div>
                    	<div class="addBtnBox"><a class="addSubBtn commonBtn w100" href="javascript:;">增加子组织</a></div>
                    </div>
                    <div class="handleArea">
                    	<p class="titleItem">组织详情</p>
                        <div class="levelContentBox">
                        	<div class="levelInfoBox">
                                <div class="levelItem" id="parentName">
                                    <em class="tit">上级名称：</em>
                                    <p>无</p>
                                </div>
                                <div class="levelItem" id="nowName">
                                    <em class="tit">当前层级：</em>
                                    <p>总部</p>
                                </div>
                                <div class="levelItem hide" id="tissueName">
                                    <em class="tit">组织名称：</em>
                                    <p><input type="text" value="组织名称" disabled="disabled"/></p>
                                </div>
                                <div class="levelItem" id="tissueStatus">
                                    <em class="tit">组织状态：</em>
                                    <p>已生效</p>
                                </div>
                            </div>
                            
                            <div class="handleBtn">
                            	<a class="editBtn disableBtn commonBtn w80" href="javascript:;">修改</a>
                                <a class="saveBtn commonBtn w80" href="javascript:;" style="display:none;border-radius:4px;">保存</a>
                                <a class="delBtn disableBtn commonBtn w80" href="javascript:;">删除</a>
                            </div>
                            
                            <p class="text-tip">提示：拥有下级组织（包括门店）的组织单位需在完成下级组织的删除或迁出后，方可进行删除操作。</p>
                        </div>
                    </div>
                </div>
            </div>
            
          </form>
        </div>
        <!--奖品配置End-->

      </div>
    </div>
  </div>
  
 
<!-- 遮罩层 -->
<div class="jui-mask"></div>
<!--奖品数量追加，弹出-->
<div class="jui-dialog jui-dialog-prizeCountAdd" style="display:none">
	<div class="jui-dialog-tit">
    	<h2>奖品数量追加</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="prizeCountAddContent">
    	<div class="commonSelectWrap">
            <em class="tit" style="width:172px;">数量：</em>
            <label class="searchInput clearBorder" style="width:308px">
              <input data-text="奖品数量追加" class="easyui-numberbox" id="prizeCountAdd" data-options="required:true,width:308,height:32" data-flag="" name="" type="text" value="">
            </label>
        </div>
        <div class="btnWrap">
            <a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
	</div>
</div>

<!--数据部分-->
<script id="tpl_prizeList" type="text/html">
<#for(var i=0,idata;i<PrizeList.length;i++){ idata=PrizeList[i];#>
<tr data-eventid="<#=idata.EventId#>" data-prizesid="<#=idata.PrizesID#>" data-num="<#=idata.CountTotal#>">
	<td><#=idata.PrizeLevel#></td>
	<td><#=idata.PrizeName#></td>
	<td class="numBox"><#=idata.CountTotal#></td>
	<td><#=idata.IssuedQty#></td>
	<td><em class="addBtn"></em></td>
	<td><em class="delBtn"></em></td>
</tr>
<#}#>
</script>
  
<script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>" ></script>

</asp:Content>
