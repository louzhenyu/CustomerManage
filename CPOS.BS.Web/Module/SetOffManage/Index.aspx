<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>连锁掌柜-基础数据</title>
    <link href="css/index.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="allPage" id="section" data-js="js/index.js">
        <div class="indexMemberGoldore clearfix">
        	<p class="titBar">基础数据</p>
            <div class="totalArea">
            	<div class="wxTotalBox">
                	<p class="minTit">微信粉丝</p>
                    <div class="starBox">
                    	<div class="infoBox">
                        	<p class="tinyTit">总数</p>
                            <p class="countBox onlineFansCount"><em>0</em><span>人</span></p>
                        </div>
                        <em class="iconEqual"></em>
                        <div class="infoBox">
                        	<p class="tinyTit">已完成微信注册</p>
                            <p class="countBox onlineVipCount"><em>0</em><span>人</span></p>
                        </div>
                        <em class="iconAdd"></em>
                        <div class="infoBox">
                        	<p class="tinyTit">未完成微信注册</p>
                            <p class="countBox onlineOnlyFansCount"><em>0</em><span>人</span></p>
                        </div>
                    </div>
                </div>
                
                <div class="userTotalBox">
                	<p class="minTit">会员 <span>完成微信注册的会员和未完成微信注册的线下会员卡持有者</span></p>
                    <div class="starBox">
                    	<div class="infoBox">
                        	<p class="tinyTit">总数</p>
                            <p class="countBox vipCount"><em>0</em><span>人</span></p>
                        </div>
                        <em class="iconEqual"></em>
                        <div class="infoBox">
                        	<p class="tinyTit">已完成微信注册</p>
                            <p class="countBox onlineVipCount"><em>0</em><span>人</span></p>
                        </div>
                        <em class="iconAdd"></em>
                        <div class="infoBox">
                        	<p class="tinyTit">未完成会员注册</p>
                            <p class="countBox offlineVipCount"><em>0</em><span>人</span></p>
                        </div>
                    </div>
                </div>
            </div>
            
            
            <div class="day30Area">
            	<div class="itemBox">
                	<p class="minTit">近30天活跃会员人数</p>
                    <div class="infoBox">
                    	<p class="exp"></p>
                        <div class="dataBox">
                        	<p class="actualBox onlineVipCountFor30DayOrder"><em>0</em><span>人</span></p>
                            <p class="elevator onlineVipCountPerFor30DayOrder"><em>0</em>%</p>
                        </div>
                    </div>
                </div>
                
                <div class="itemBox">
                	<p class="minTit">近30天活跃会员占比</p>
                    <div class="infoBox">
                    	<p class="exp">占注册会员总数</p>
                        <div class="dataBox">
                        	<p class="actualBox onlineVipCountFor30DayOrderM2M"><em>0</em><span> %</span></p>
                            <p class="elevator onlineVipCountPerFor30DayOrderM2M"><em>0</em>%</p>
                        </div>
                    </div>
                </div>
                
                <div class="itemBox">
                	<p class="minTit">注册会员近30天销量贡献</p>
                    <div class="infoBox">
                    	<p class="exp"></p>
                        <div class="dataBox">
                        	<p class="actualBox onlineVipSalesFor30Day"><span>￥</span><em>0</em></p>
                            <p class="elevator onlineVipSalesFor30DayM2M"><em>0</em>%</p>
                        </div>
                    </div>
                </div>
                
                <div class="itemBox">
                	<p class="minTit">注册会员近30天销量贡献</p>
                    <div class="infoBox">
                    	<p class="exp">占注册会员总贡献</p>
                        <div class="dataBox">
                        	<p class="actualBox onlineVipSalesPerFor30Day"><em>0</em><span> %</span></p>
                            <p class="elevator onlineVipSalesPerFor30DayM2M"><em>0</em>%</p>
                        </div>
                    </div>
                </div>
                
            </div>
            
            
            <div class="operateArea">
            	<div class="itemBox">
                	<p class="minTit">集客</p>
                    <div class="picBox">
                    	<img src="images/icon-collect.png" />
                    </div>
                    <div class="textBox">
                    	<p>发布集客行动，激励店员和会员在店内、店外、微信广泛集客，扩展会员规模。</p>
                        <p>发动集客行动前，请通过手动导入会员数据，把已有线下会员导入到线上并激活，激励他们参与集客行动。</p>
                    </div>
                    <br />
                    <div class="btnBox">
                    	<a href="javascript:;" class="" id="inportvipmanageBtn">手动导入会员数据</a>
                    </div>
                    <div class="btnBox">
                    	<a href="Action.aspx" class="">发布集客行动</a>
                    </div>
                </div>
                
                <div class="itemBox">
                	<p class="minTit">权益</p>
                    <div class="picBox">
                    	<img src="images/icon-equity.png" />
                    </div>
                    <div class="textBox">
                    	<p>好的权益会让会员离不开你。</p>
                        <p>我们提供丰富的权益方案，请根据您所属的行业、产品、目标客户特点，选择合适的权益方案。您也可以发挥想象，自定义更有趣的会员权益。</p>
                    </div>
                    <div class="btnBox" style="padding-top:60px">
                    	<a href="/module/activateCard/querylist.aspx" class="">设置会员权益</a>
                    </div>
                </div>
                
                <div class="itemBox">
                	<p class="minTit">忠诚度</p>
                    <div class="picBox">
                    	<img src="images/icon-loyalty.png" />
                    </div>
                    <div class="textBox">
                    	<p>利用丰富的会员标签＋会员大数据帮您预知会员需求，实施精准的会员营销，提升会员忠诚度。</p>
                    </div>
                    <div class="btnBox" style="padding-top:115px">
                    	<a href="/module/newVipManage/querylist.aspx" class="">发动会员营销</a>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    
    
    
    
    <!-- 导入数据 -->
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
                                            <p  style="display:none;">请按照数据模板的格式准备要导入的数据<a href="http://help.chainclouds.cn/?p=733">（如何导入？）</a></p>
                                            <div class="attention">
                                            	<span>模板预览</span>
                                                <p><img src="images/tplTablePic.png"</p>
                                                <br /><br />
                                            	<span>注意事项：</span>
                                                <div class="textBox">
                                                    <p>1.模板中的表头名称不可更改、表头行不能删除。</p>
                                                    <p>2.项目顺序可以调整，不需要的项目可以删除。</p>
                                                    <p>3.表中的会员姓名、手机号为必填项目，必须保留。</p>
                                                    <p>4.导入文件请勿超过 1 MB。</p>
                                                </div>
                                            </div>
                                            <p class="btnWrapBox"><a class="downModelBtn" href="/File/ExcelTemplate/会员导入数据模板.xls">下载模板</a></p>
                                        </div>
                                        <div class="menber_centernrb" id="editLayer">
                                            选择需要导入的xls文件
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
                                            <p><a id="error_report" href="javascipt:void(0)">error_report.xlsx<span class="commonBtn w100">下载文件</span></a></p>
                    
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

</asp:Content>
