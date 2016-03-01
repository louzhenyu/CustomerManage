<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>会员管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/style.css?v=0.4" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/queryList.js?ver=0.1">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
            <!--个别信息查询-->
            <div class="queryTermArea" id="simpleQuery" >
                <div class="item">

                    <form>
                    </form>
                    <form id="seach">
                    <div class="commonSelectWrap">
                     <em class="tit">会员编号</em>
                    <div class="searchInput"><input type="text" name="VipCardCode"  placeholder="请输入卡号" value=""/> </div>
</div><!--commonSelectWrap-->
                     <div class="commonSelectWrap">
                      <em class="tit">会员姓名</em>
                     <div class="searchInput"><input type="text"  name="VipName" placeholder="请输入姓名/昵称" value=""/> </div>
 </div><!--commonSelectWrap-->
                      <div class="commonSelectWrap">
                       <em class="tit">手机号</em>
                      <div class="searchInput"><input type="text"  name="Phone" placeholder="请输入手机号" value=""/> </div>
  </div><!--commonSelectWrap-->
                    </form>

 <div class="moreQueryWrap">
                        <a href="javascript:;" class="commonBtn queryBtn select">查询</a>
                    </div>
                </div>



            </div>
            <div class="tableWrap"  id="tableWrap">
              <div class="optionBtn" id="opt">
                                         <div class="commonBtn icon icon_import w80 r"  id="inportvipmanageBtn">导入</div>
                                     </div>
                <div class="dataTable gridLoading" id="gridTable">
                   <!-- <div class="loading">
                        <span>
                            <img src="../static/images/loading.gif"></span>
                    </div>-->
                </div>
                <div id="pageContianer">
                    <div class="dataMessage">
                      没有符合条件的记录  </div>
                    <div id="kkpager">
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div style="display: none">
    <div id="win1" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true"  >
      		<div class="easyui-layout" data-options="fit:true" id="Div2">

      			<div data-options="region:'center'" style="padding:0px;">


			            <div class="qb_member">
                            <div id="step1" class="member step">
                                <div class="menber_title"><img src="images/lc_1.png" /></div>
                                <div class="menber_center">
                                    <div class="menber_centernr">
                                        <div class="menber_centernrt">
                                            <p>请按照数据模板的格式准备要导入的数据<a href="http://help.chainclouds.cn/?p=733">（如何导入？）</a></p>
                                            <p><a href="/File/ExcelTemplate/会员导入数据模板.xlsx">下载模板</a></p>
                                            <div class="attention"><span>注意事项：</span>
                                                1.模板中的表头名称不可更改、表头行不能删除。<br />
                                                2.项目顺序可以调整，不需要的项目可以删除。<br />
                                                3.表中的会员姓名、手机号为必填项目，必须保留。<br />
                                                4.导入文件请勿超过 1 MB。
                                            </div>
                                        </div>
                                        <div class="menber_centernrb" id="editLayer">
                                            选择需要导入的xlsx文件
                                            <p id="nofiletext" >未选择文件</p>
                                             <div class="CSVFilelist"></div>
                                            <input id="CSVFileurl" value="" type="hidden"  />
                                           <input type="file" class="uploadCSVFileBtn" />
                                        </div>
                                    </div>  
	                            </div>
                            </div>

                             <div id="step2"  class="member step" style="display:none">
                                <div class="menber_title"><img src="images/lc_2.png" /></div>
                                    <div class="menber_center">
                                        <div class="menber_centernr">
                                            <div class="memberloading">导入中...</div>
                                            <div class="attention"><span>提示：</span>
                                                1.导入过程中请勿关闭此页面；<br />
                                                2.数据导入结束后，可能下载错误报告，以便重新处理。
                                            </div>
        	                            </div>
		                            </div>
                            </div>


                            <div id="step3"  class="member step" style="display:none">
                                <div class="menber_title"><img src="images/lc_3.png" /></div>
                                <div class="menber_center">
                                    <div class="menber_centernr">
                                        <div class="succeed">导入完成<p>共<span id="inputTotalCount" class="inputCount"> 0</span> 条，成功导入<span  id="inputErrCount" class="red inputCount"> 0</span> 条</p></div>
                                        <div class="menber_centernrb1">
                	                        下载错误报告，查看失败原因
                                            <p><a id="error_report" href="javascipt:void(0)">error_report.xlsx<span class="commonBtn w100">选择文件</span></a></p>
                    
                                        </div>
        	                        </div>
		                        </div>
                            </div>


                            </div>
			
			  


      			</div>
      			<div class="btnWrap" id="btnWrap1" data-options="region:'south',border:false" style="height:80px;padding:5px 20px 0;">
      				<a id="startinport"  class="easyui-linkbutton commonBtn saveBtn" >开始导入</a>  
                      <a id="closebutton" style="display:none;" class="easyui-linkbutton commonBtn closeBtn close" >关闭</a>
      			</div>
      		</div>

      	</div>
        </div>

    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
