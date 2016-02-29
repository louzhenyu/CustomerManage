<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>新增问卷</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/QuestionnaireNews/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    <link href="<%=StaticUrl+"/module/QuestionnaireNews/css/QuestionnaireDetail.css?v=0.6"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="allPage" id="section" data-js="js/QuestionnaireDetail.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">
             <div style="display:none;">
                                <input id="QuestionnaireType" class="Questionnairedata"   data-idname="QuestionnaireType" value="1"   type="text" />
                                <input id="QuestionnaireID" class="Questionnairedata"   data-idname="QuestionnaireID" value=""   type="text" />
             </div>

           
            
            <!--第一步信息页面-->
            <div class="step step1">
                <div style="display:none;">
                                <input  class="Questionnairestepdata"   data-idname="step" value="1"   type="text" />
                                
                </div>
                <div class="queryTermArea navhead " id="simpleQuery" >
                  <div class="navcontent" >   
                   </div>
                </div>
                
            
                <div class="tableWrap Questionnairebody" id="tableWrap">
                    <div class="startimg">
                        <img  class="StartBG" src="/Module/QuestionnaireNews/images/StartBG.png" />
                        <img class="_BGImageSrc" src="" />
                        <span class="startbtn">开始</span>
                        <span class="regular">活动规则></span>
                    </div>
                    <div class="startpageContent">

                            <div class="commonSelectWrap">
                                <em class="tit">按钮名称：</em>
                                <label class="searchInput" style="width: 180px;">
                                    <input id="ButtonName" class="Questionnairedata" data-alerttext="按钮名称不能为空"  data-required="true" maxlength="10"  data-idname="ButtonName"   name="ButtonName" type="text"
                                        value="开始">
                                </label>
                            </div>
                            <div class="commonSelectWrap colorplan">
                                <em class="tit">按钮颜色：</em>
                                <span style="display:none;">
                                <input id="StartPageBtnBGColor"  class="Questionnairedata"  data-idname="StartPageBtnBGColor"   type="text" value="#fc9a01"/>
                                    <input id="StartPageBtnTextColor"  class="Questionnairedata"  data-idname="StartPageBtnTextColor"     type="text"  value="#fff"/>
                                 </span>
                                <div class="color_plan" data-type="1">
                               
                                </div>
                            </div>

                        <div  class="commonSelectWrap uploadItem"  data-flag=1  data-url="" data-showclass="_BGImageSrc" data-batid="BGImageSrc" >
                            <div style="display:none;">
                                <input class="imgvalue Questionnairedata"    data-alerttext="请上传背景图片！"  data-required="true"  data-idname="BGImageSrc" value=""   type="text" />
                            </div>
                        	<p class="tit">背景图片：</p>
                            <div class="infoBox">
                            	<a href="javascript:void(0)" data-uploadimgwidth=130  class="commonHandleBtn uploadImgBtn"></a>
                                <p class="exp">(建议尺寸:640*1008，大小为50k以内)</p>
                            </div>
                        </div>
                        <div  class="commonSelectWrap" style="margin-top: 35px;" >
                        	<p class="tit">活动规则：</p>
                            <div class="checkBox on rulebtn" data-flag="EnableIntegral" data-name="r1" >
                                <div style="display:none;">
                                    <input class="checkvalue Questionnairedata"    data-text="活动规则启用" data-idname="IsShowQRegular" value="1"  type="text" />
                                </div>
                                  <em></em>
                                  <span>启用</span>
                              </div>
                            <div class="infoBox ruleText" style="margin-top: 10px; display: block;">
                            	<textarea id="QRegular"  class="Questionnairedata"  placeholder="请输入"  data-alerttext="规则内容不能为空！" data-required="true"  data-idname="QRegular"   ></textarea>
                            </div>
                        </div>
                    </div>


                    
                </div>

                <div class="btnWrap">
            	    <a href="javascript:void(0)" class="startStepbtn   btnopt commonBtn" data-flag="#nav01">返回</a>
            	    <a href="javascript:void(0)" class="commonStepBtn nextStepBtn  btnopt  commonBtn" data-showstep="step2"  data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
                    </div>
            </div>

            <!--第二步信息页面-->
            <div class="step step2" style="display:none;">
                <div style="display:none;">
                                <input  class="Questionnairestepdata"   data-idname="step" value="2"   type="text" />
                                
                </div>
                <div class="queryTermArea navhead "  >
                   <div class="navcontent" >   
                   </div>
                </div>
                
            
                <div class="tableWrap Questionnairebody" >
                     
                    <div class="modeltype">
                        <div style="display:none;">
                                <input class="radiovalue Questionnairedata"   data-idname="ModelType" value="1"   type="text" />
                        </div>
                        <div class="model">
                        <img  class="modeltypeimg"  data-noselected="/Module/QuestionnaireNews/images/ModelType1.png" data-selected="/Module/QuestionnaireNews/images/ModelType1_on.png" src="/Module/QuestionnaireNews/images/ModelType1_on.png" />
                            <div class="radio on" data-value="1"><span><em></em>&nbsp;单题样式</span></div>
                            </div>
                        <div class="model">
                        <img  class="modeltypeimg"  data-noselected="/Module/QuestionnaireNews/images/ModelType2.png" data-selected="/Module/QuestionnaireNews/images/ModelType2_on.png" src="/Module/QuestionnaireNews/images/ModelType2.png" />
                            <div class="radio"  data-value="2"><span><em></em>&nbsp;瀑布样式</span></div>
                            </div>
                    </div>


                    
                </div>

                <div class="btnWrap">
            	    <a href="javascript:void(0)" class="commonStepBtn prevStepBtn btnopt commonBtn"  data-showstep="step1"  data-flag="#nav01">上一步</a>
            	    <a href="javascript:void(0)" class="commonStepBtn nextStepBtn btnopt commonBtn" data-showstep="step3" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
                    </div>
            </div>

            <!--第三步信息页面-->
            <div class="step step3" style="display:none;">
                <div style="display:none;">
                                <input  class="Questionnairestepdata"   data-idname="step" value="3"   type="text" />
                                
                </div>
                <div class="queryTermArea navhead "  >
                   <div class="navcontent" >   
                   </div>
                </div>
                
            
                   
                   <div class="lineTitle topDashed firstTile"><em></em> 基本信息</div>
                    <div class="linePanel">
                             <div class="exitDiv">
                                    
                                 <div class="commonSelectWrap widthMax">
                                        <em class="tit">问卷名称:</em>
                                        <div class="searchInput" style="width:400px"><input  class="Questionnairedata"  data-idname="QuestionnaireName"  class="easyui-validatebox validatebox-text"  data-alerttext="问卷名称不能为空!"  data-required="true"   placeholder="请输入" data-options="required:true" ></div>
                                   </div>
                             </div>  
              
                    </div>
                    <div class="lineTitle topDashed"><em></em> 问卷内容</div>
                     <div class="linePanel">
                                 <div class="exitDiv">
                                     <div class="questionlist">
                                         <div class="questiondel" style="display:none;">
                           
                                             </div>


                                     </div>

                                     <%-- 添加模块 --%>
                                     <div id="addItem" class="commonOptsBtn">
                                         <div class="icon">
                                         </div>
                                         <p>
                                             添加模块
                                         </p>

                                         <div class="listAdd">
                                             <div class="addBtn" data-createtype="tpl_Singletext" data-text="" data-type="1">
                                                 单行文字
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_MultiLinetext" data-text="" data-type="2">
                                                 多行文字
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_singleselection" data-text="" data-type="3">
                                                 单项选择
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_Multiselect" data-text="" data-type="4">
                                                 多项选择
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_dropdownlist" data-text=""  data-type="5">
                                                 下拉框
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_phone" data-text="手机号"  data-type="6">
                                                 手机号
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_addr" data-text="地址"  data-type="7">
                                                 地址
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_date" data-text="日期" data-type="8">
                                                 日期
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_imgsingleselection" data-text="" data-type="9">
                                                 图片单选
                                             </div>
                                             <div class="addBtn" data-createtype="tpl_imgMultiselect" data-text="" data-type="10">
                                                 图片多选
                                             </div>


                                         </div>
                                     </div>



                                 </div>  
                         
              
                        </div>



                <div class="btnWrap">
            	    <a href="javascript:void(0)" class="commonStepBtn prevStepBtn btnopt  commonBtn"  data-showstep="step2"  data-flag="#nav01">上一步</a>
            	    <a href="javascript:void(0)" class="commonStepBtn nextStepBtn  btnopt commonBtn" data-showstep="step4" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
                    </div>
            </div>


            <!--第四步信息页面-->
            <div class="step step4" style="display:none;">
                <div style="display:none;">
                                <input  class="Questionnairestepdata "   data-idname="step" value="4"   type="text" />
                                
                </div>
                <div class="queryTermArea navhead "  >
                   <div class="navcontent" >   
                   </div>
                </div>
                
            
                   
                   <div class="lineTitle topDashed firstTile"><em></em> 请对下面题目进行分值设置&nbsp;&nbsp;&nbsp;  <span class="tips"><span>Tips</span>填空题等题型不支持设置得分，此处不显示</span><div class="ScoreRange">预计总得分：<span class="ScoreRangevalue"  data-minvalue="0"  data-maxvalue="0">0-0</span></div></div>
                    
                     <div class="OptionScorelinePanel">
                          
                         
                         
                    </div>


                <div class="btnWrap">
            	    <a href="javascript:void(0)" class="commonStepBtn prevStepBtn btnopt  commonBtn "  data-showstep="step3"  data-flag="#nav01">上一步</a>
            	    <a href="javascript:void(0)" class="commonStepBtn nextStepBtn  btnopt commonBtn" data-showstep="step5" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
                    </div>

        
           </div>


            <!--第五步信息页面-->
            <div class="step step5" style="display:none;">
                <div style="display:none;">
                                <input  class="Questionnairestepdata"   data-idname="step" value="5"   type="text" />
                                
                </div>
                <div class="queryTermArea navhead "  >
                   <div class="navcontent" >   
                   </div>
                </div>
                
            
                   
                    
                 <div class="tableWrap" id="tableWraplist">
                    <div class="optionBtn" id="opt">
                	    <div class="commonBtn descbtn" id="addUserBtn"><span>+</span>新增描述</div>预计总得分：<span  class="ScoreRangevalue" data-minvalue="0"  data-maxvalue="0">0-0</span>
                    </div>
                       <div class="">
                   		    <table class="dataTable" id="gridTable">
                        	    <div  class="loading">
                                   <span><img src="../static/images/loading.gif"></span>
                                </div>
                            </table>
                       </div>
                        <div id="pageContianer">
                        <div class="dataMessage" >没有符合条件的查询记录</div>
                            <div id="kkpager" >
                            </div>
                        </div>
                    </div>    
                         



                <div class="btnWrap">
            	    <a href="javascript:void(0)" class="commonStepBtn prevStepBtn btnopt commonBtn"  data-showstep="step4"  data-flag="#nav01">上一步</a>
            	    <a href="javascript:void(0)" class="commonStepBtn nextStepBtn  btnopt commonBtn" data-showstep="step6" data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">下一步</a>
                    </div>
            </div>

        


            <!--第六步信息页面-->
            <div class="step step6">
                <div style="display:none;">
                                <input  class="Questionnairestepdata"   data-idname="step" value="6"   type="text" />
                                
                </div>
                <div class="queryTermArea navhead " id="Div10" >
                   <div class="navcontent" >   
                   </div>
                </div>
                
            
                <div class="tableWrap Questionnairebody" id="Div11">
                    <div class="Endimg">
                        <img  class="EndBG" src="/Module/QuestionnaireNews/images/EndPage.png" />
                        <img class="end_BGImageSrc" src="" />
                        <span class="endpagetext">答题已完成是否要分享</span>
                        <span class="Endbtn">分享</span>
                        
                    </div>
                    <div class="EndpageContent">
                            <div  class="commonSelectWrap uploadItem"  data-flag=1  data-url="" data-showclass="end_BGImageSrc" data-batid="BGImageSrc" >
                                <div style="display:none;">
                                    <input class="imgvalue Questionnairedata" data-text="结束背景图片路径" data-idname="QResultBGImg" data-realvalue=""  type="text" />
                                </div>
                        	    <p class="tit">背景图片：</p>
                                <div class="infoBox">
                            	    <a href="javascript:void(0)" data-uploadimgwidth=130  class="commonHandleBtn uploadImgBtn"></a>
                                    <p class="exp">(建议尺寸:640*1008，大小为50k以内)</p>
                                </div>
                            </div>
                        
                            
                            <div class="commonSelectWrap colorplan">
                                <em class="tit">按钮颜色：</em>
                                <span style="display:none;">
                                <input  class="Questionnairedata"  id="QResultBGColor" data-idname="QResultBGColor"    type="text" value="#ffcc00"/>
                                    <input  class="Questionnairedata"  id="QResultBtnTextColor" data-idname="QResultBtnTextColor"      type="text"  value="#fff"/>

                                 </span>
                                <div class="color_plan" data-type="2">
                               
                                    </div>
                            </div>

                        
                    </div>


                    
                </div>

                <div class="btnWrap">
            	    <a href="javascript:void(0)" class="commonStepBtn prevStepBtn  btnopt commonBtn" data-showstep="step5" >上一步</a>
            	    <a href="javascript:void(0)" class="commonStepBtn endStepBtn  btnopt commonBtn"  data-flag="#nav03" data-page="redPackage" style="margin-left:40px;">保存</a>
                    </div>
            </div>


            

           </div>
    </div>

    

    <%-- 导航序号 --%>
    <script id="tpl_navtitle" type="text/html">
        <#for(var i=0,idata;i<data.length; i++){idata=data[i];#>
             <div class="ordernumber">
                 <# if( idata.checked){ #>
                     <div class="blueline"></div>
                      <div class="NavOuterCircle"><div class="NavCircle"><#=(i+1) #></div></div>
                     <div class="blueline"></div>
                 
                     <div class="bluetext"><#=idata.name#></div>
                <#}else{#>
                 
                     <div class="grayline"></div>
                      <div class="NavOuterCircle GrayColor"><div class="GrayColor NavCircle "><#=(i+1) #></div></div>
                     <div class="grayline"></div>
                     <div ><#=idata.name#></div>
                 <#}#>
               
            </div>
        <#}#>
                
                                
         </script>

    <%-- 颜色板 --%>
    <script id="tpl_colorplan" type="text/html">
         <img class="writecolor" src="/Module/QuestionnaireNews/images/writecolor.png"  />
                                   <img class="blackcolor" src="/Module/QuestionnaireNews/images/blackcolor.png"  />
                                   <img class="allcolor" src="/Module/QuestionnaireNews/images/allcolor.png"  />
                                <div class="Colorplate" >
                                    <%-- 第1行 --%>
                                    <div style="background-color:#960001;color:#fff;">文字</div>
                                    <div style="background-color:#ff0101;color:#fff;">文字</div>
                                    <div style="background-color:#fc9a01;color:#fff;">文字</div>
                                    <div style="background-color:#fffe03;color:#EA8E39;">文字</div>
                                    <div style="background-color:#04fd01;color:#274F13;">文字</div>
                                    <div style="background-color:#01ffcd;color:#007765;">文字</div>
                                    <div style="background-color:#00ffff;color:#44828F;">文字</div>
                                    <div style="background-color:#0102fa;color:#fff;">文字</div>
                                    <div style="background-color:#9c00fc;color:#fff;">文字</div>
                                    <div style="background-color:#ff00fe;color:#fff;">文字</div>
                                    <%-- 第2行 --%>
                                    <div style="background-color:#e7b8b0;color:#C94320;">文字</div>
                                    <div style="background-color:#f4cccc;color:#E26563;">文字</div>
                                    <div style="background-color:#fde4d0;color:#EA8E39;">文字</div>
                                    <div style="background-color:#fff2cd;color:#EA8E39;">文字</div>
                                    <div style="background-color:#d8ead2;color:#69A84F;">文字</div>
                                    <div style="background-color:#c3f8f2;color:#007765;">文字</div>
                                    <div style="background-color:#d2e0e3;color:#44828F;">文字</div>
                                    <div style="background-color:#cfe2f3;color:#3E85C7;">文字</div>
                                    <div style="background-color:#dad2e9;color:#674FA7;">文字</div>
                                    <div style="background-color:#e7d3dc;color:#A54E79;">文字</div>
                                    <%-- 第3行 --%>
                                    <div style="background-color:#DD7E6A;color:#fff;">文字</div>
                                    <div style="background-color:#E99897;color:#660000;">文字</div>
                                    <div style="background-color:#FAC99E;color:#7B3C07;">文字</div>
                                    <div style="background-color:#FFE598;color:#7E6000;">文字</div>
                                    <div style="background-color:#B6D7A8;color:#274F13;">文字</div>
                                    <div style="background-color:#7DDED5;color:#007765;">文字</div>
                                    <div style="background-color:#A3C4CB;color:#0C353B;">文字</div>
                                    <div style="background-color:#9FC5E9;color:#073863;">文字</div>
                                    <div style="background-color:#B3A6D4;color:#20124D;">文字</div>
                                    <div style="background-color:#D7A5BE;color:#fff;">文字</div>
                                    <%-- 第4行 --%>
                                    <div style="background-color:#C94320;color:#fff;">文字</div>
                                    <div style="background-color:#E26563;color:#fff;">文字</div>
                                    <div style="background-color:#F5B172;color:#fff;">文字</div>
                                    <div style="background-color:#FDDA64;color:#fff;">文字</div>
                                    <div style="background-color:#92C47F;color:#fff;">文字</div>
                                    <div style="background-color:#39C6B5;color:#fff;">文字</div>
                                    <div style="background-color:#79A3AF;color:#fff;">文字</div>
                                    <div style="background-color:#6FA8DD;color:#fff;">文字</div>
                                    <div style="background-color:#8D7CC3;color:#fff;">文字</div>
                                    <div style="background-color:#BD7D9F;color:#fff;">文字</div>

                                    <%-- 第5行 --%>
                                    <div style="background-color:#A61C00;color:#fff;">文字</div>
                                    <div style="background-color:#CC0001;color:#fff;">文字</div>
                                    <div style="background-color:#EA8E39;color:#fff;">文字</div>
                                    <div style="background-color:#F1C332;color:#fff;">文字</div>
                                    <div style="background-color:#69A84F;color:#fff;">文字</div>
                                    <div style="background-color:#05A792;color:#fff;">文字</div>
                                    <div style="background-color:#44828F;color:#fff;">文字</div>
                                    <div style="background-color:#3E85C7;color:#fff;">文字</div>
                                    <div style="background-color:#674FA7;color:#fff;">文字</div>
                                    <div style="background-color:#A54E79;color:#fff;">文字</div>

                                    <%-- 第6行 --%>
                                    <div style="background-color:#5B0F01;color:#fff;">文字</div>
                                    <div style="background-color:#660000;color:#fff;">文字</div>
                                    <div style="background-color:#7B3C07;color:#fff;">文字</div>
                                    <div style="background-color:#7E6000;color:#fff;">文字</div>
                                    <div style="background-color:#274F13;color:#fff;">文字</div>
                                    <div style="background-color:#007764;color:#fff;">文字</div>
                                    <div style="background-color:#0C353B;color:#fff;">文字</div>
                                    <div style="background-color:#053863;color:#fff;">文字</div>
                                    <div style="background-color:#20124D;color:#fff;">文字</div>
                                    <div style="background-color:#000000;color:#fff;">文字</div>

                                    <%-- 第7行 --%>
                                    <div ><img style="margin: 0px;width: 31px;" src="/Module/QuestionnaireNews/images/slash.png" /></div>
                                    <div style="background-color:#FFFFFF;color:#000">文字</div>
                                    <div style="background-color:#E1E1E1;color:#000">文字</div>
                                    <div style="background-color:#C3C3C3;color:#000">文字</div>
                                    <div style="background-color:#A5A5A5;color:#000;">文字</div>
                                    <div style="background-color:#868789;color:#fff;">文字</div>
                                    <div style="background-color:#696969;color:#fff;">文字</div>
                                    <div style="background-color:#4C4A4B;color:#fff;">文字</div>
                                    <div style="background-color:#232323;color:#fff;">文字</div>
                                    <div style="background-color:#000000;color:#fff;">文字</div>
                                </div>
         </script>

    <%-- 单行文字 --%>
    <script id="tpl_Singletext" type="text/html">

        <div class="question  <# if(isedit){#> select <#} #> questionnew" draggable="true" >
            <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
            <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="1" value="1"  type="text" />
                        </div>
            <div id="editLayer" class="handleLayer"   <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">单行文字</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix  ">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Name" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                <div class="jsAreaTitle clearfix uploadItem">
                   
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">默认值:</div>
                    <div class="wrapInput">
                        <input data-text="默认值" data-idname="DefaultValue" class="easyui-validatebox  Questiondata DefaultValue" placeholder="请输入" id="DefaultValue" data-realvalue="<#=DefaultValue  #>"  maxlength="500" name="DefaultValue" type="text" value="<#=DefaultValue  #>">
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                          <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate IsValidateMinChar" data-text="最少填写" data-idname="IsValidateMinChar" data-realvalue="<#=IsValidateMinChar  #>"  value="<#=IsValidateMinChar  #>"  type="text" />
                            </div>
                            <em></em><span>最少填写&nbsp;</span><input  class="Questiondata Qnumberbox MinChar" data-options="min:0"  type="text" data-idname="MinChar" data-realvalue="<#=MinChar  #>"  value="<#=MinChar  #>" /><span>&nbsp;个字符</span></div>

                        <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Repeat" data-text="不能和已有数据重复" data-idname="NoRepeat" data-realvalue="<#=NoRepeat  #>"  value="<#=NoRepeat  #>"   type="text" />
                            </div>
                            <em></em><span>不能和已有数据重复</span></div>

                        <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate IsValidateMaxChar" data-text="最大填写" data-idname="IsValidateMaxChar" data-realvalue="<#=IsValidateMaxChar  #>"  value="<#=IsValidateMaxChar  #>"  type="text" />
                            </div>
                            <em></em><span>最多填写&nbsp;</span><input  class="Questiondata Qnumberbox MaxChar"  type="text" data-idname="MaxChar" data-realvalue="<#=MaxChar  #>" value="<#=MaxChar  #>"  /><span>&nbsp;个字符</span></div>

                        
                        </div>
                </div>


            </div>




            <div class="questiontitle">单行文字<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
                <input class="titletext" readonly="readonly"  type="text" value="<#=DefaultValue  #>" />
            </div>


        </div>
    </script>


    <%-- 多行文字 --%>
    <script id="tpl_MultiLinetext" type="text/html">

        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true" >
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
            <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="2" value="2"  type="text" />
                        </div>
            <div id="Div1" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">多行文字</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text1" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                <div class="jsAreaTitle clearfix uploadItem">
                   
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">默认值:</div>
                    <div class="wrapInput">
                        <input data-text="默认值" data-idname="DefaultValue" class="easyui-validatebox  Questiondata DefaultValue" placeholder="请输入" id="Text10" data-realvalue="<#=DefaultValue  #>"  maxlength="500" name="DefaultValue" type="text" value="<#=DefaultValue  #>">
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                         <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate IsValidateMinChar" data-text="最少填写" data-idname="IsValidateMinChar" data-realvalue="<#=IsValidateMinChar  #>"  value="<#=IsValidateMinChar  #>"  type="text" />
                            </div>
                            <em></em><span>最少填写&nbsp;</span><input  class="Questiondata Qnumberbox MinChar" data-options="min:0"  type="text" data-idname="MinChar" data-realvalue="<#=MinChar  #>"  value="<#=MinChar  #>" /><span>&nbsp;个字符</span></div>
                         <div class="checkBox"  ></div>
                        <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate IsValidateMaxChar" data-text="最大填写" data-idname="IsValidateMaxChar" data-realvalue="<#=IsValidateMaxChar  #>"  value="<#=IsValidateMaxChar  #>"  type="text" />
                            </div>
                            <em></em><span>最多填写&nbsp;</span><input  class="Questiondata Qnumberbox MaxChar"  type="text" data-idname="MaxChar" data-realvalue="<#=MaxChar  #>" value="<#=MaxChar  #>"  /><span>&nbsp;个字符</span></div>

                    </div>
                </div>


            </div>




            <div class="questiontitle">多行文字<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
               <textarea class="titletext" rows="2" cols="2" readonly="readonly"><#=DefaultValue  #></textarea>
            </div>


        </div>
    </script>


    <%-- 单项选择 --%>
    <script id="tpl_singleselection" type="text/html">

        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true" >
             <div class="optiondel" style="display:none;">
                           
             </div>

             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
            <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="3" value="3"  type="text" />
                        </div>
            <div id="Div3" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">单项选择</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text2" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                 <div class="jsAreaTitle clearfix uploadItem">
                    
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">选项:</div>
                    <div class="wrapInput">
                        <div class="optionlist">
                            <# if(Optionlist!=null&&Optionlist.length>0){ #>
                            <# for(var i=0;i<Optionlist.length;i++){#>
                       <div class="option">
                            <div style="display:none;">
                                <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                            </div>
                                <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="<#=Optionlist[i].OptionContent#>" value="<#=Optionlist[i].OptionContent#>"  placeholder="请输入" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn" <# if(i==0){#> style="display:none;" <#} #> >删除</span></div>
                            </div>
                                <#}}else{#>

                                 <div class="option">
                                <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="" value=""  placeholder="请输入" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn" style="display:none;">删除</span></div>
                            </div>
                                <#}#>
                        </div>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                      <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">单项选择<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
                <div class="radiolist">
                    
                    <# for(var i=0;i<Optionlist.length;i++){#>
                        <div class='radio'><em></em>&nbsp;<#=Optionlist[i].OptionContent#></div>
                     <#}#>


                </div>
            </div>


        </div>

    </script>


    <%-- 多项选择 --%>
    <script id="tpl_Multiselect" type="text/html">
        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true" >
            <div class="optiondel" style="display:none;">
                           
             </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
                    <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="4" value="4"  type="text" />
                        </div>
            <div id="Div2" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">多项选择</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text3" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                 <div class="jsAreaTitle clearfix uploadItem">
                    
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">选项:</div>
                    <div class="wrapInput">
                        <div class="optionlist">
                             <# if(Optionlist!=null&&Optionlist.length>0){ #>
                            <# for(var i=0;i<Optionlist.length;i++){#>
                       <div class="option">
                           <div style="display:none;">
                                <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                            </div>
                                <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="<#=Optionlist[i].OptionContent#>" value="<#=Optionlist[i].OptionContent#>"  placeholder="请输入" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn"  <# if(i==0){#> style="display:none;" <#} #>  >删除</span></div>
                            </div>
                                <#}}else{#>

                                 <div class="option">
                                <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="" value=""  placeholder="请输入" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn"  style="display:none;">删除</span></div>
                            </div>
                                <#}#>
                       </div>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                       <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">多项选择<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
                <div class="checkBoxlist">

                    <# for(var i=0;i<Optionlist.length;i++){#>
                        <div class='checkBox'><em></em>&nbsp;<#=Optionlist[i].OptionContent#></div>
                     <#}#>

                </div>
            </div>


        </div>

    </script>


    <%-- 下拉框 --%>
    <script id="tpl_dropdownlist" type="text/html">

        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true">
            <div class="optiondel" style="display:none;">
                           
             </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="5" value="5"  type="text" />
                        </div>
            <div id="Div4" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">下拉框</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text4" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                 <div class="jsAreaTitle clearfix uploadItem">
                    
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">选项:</div>
                    <div class="wrapInput">
                        <div class="optionlist">
                               
                                 <# if(Optionlist!=null&&Optionlist.length>0){ #>
                            <# for(var i=0;i<Optionlist.length;i++){#>
                       <div class="option">
                           <div style="display:none;">
                                <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                            </div>
                                <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="<#=Optionlist[i].OptionContent#>" value="<#=Optionlist[i].OptionContent#>"  placeholder="请输入" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn"  <# if(i==0){#> style="display:none;" <#} #>  >删除</span></div>
                            </div>
                                <#}}else{#>

                                 <div class="option">
                                 <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue=""  placeholder="请输入" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn" style="display:none;">删除</span></div>
                                   </div>
                                <#}#>

                       </div>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">下拉框<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
               
                <input type="text"  class="titletext Qcombobox dropdownlist " placeholder="请输入" style="margin-left: 0px; margin-right: 18px; width: 142px;">
            </div>


        </div>

    </script>

    <%-- 手机号 --%>
    <script id="tpl_phone" type="text/html">

        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true">
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="6" value="6"  type="text" />
                        </div>
            <div id="Div5" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">手机号</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text5" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                 <div class="jsAreaTitle clearfix uploadItem">
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                        <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Repeat" data-text="不能和已有数据重复" data-idname="NoRepeat" data-realvalue="<#=NoRepeat  #>"  value="<#=NoRepeat  #>"   type="text" />
                            </div>
                            <em></em><span>不能和已有数据重复</span></div>

                    </div>
                </div>


            </div>




            <div class="questiontitle">手机号<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
               
                <input type="text" class="titletext"  readonly="readonly"  placeholder="请输入" value=""  >
            </div>


        </div>

    </script>


    <%-- 地址 --%>
    <script id="tpl_addr" type="text/html">


        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true">
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="7" value="7"  type="text" />
                        </div>
            <div id="Div6" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">地址</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text6" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                 <div class="jsAreaTitle clearfix uploadItem">
                   
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">所在地:</div>
                    <div class="wrapInput">
                        <div class="checkBox <# if(IsShowProvince==1){#> on <#} #> addroption" data-name="allList" data-value="showName">
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate" data-text="省" data-idname="IsShowProvince" data-showclass="Province,.City,.County" data-realvalue="<#=IsShowProvince  #>" value="<#=IsShowProvince  #>"   type="text" />
                            </div>
                            <em></em><span>
                            <input type="text" class="Qcombobox"  placeholder="请输入" value="省" style="margin-left: 0px; margin-right: 18px; width: 142px;"></span></div>
                        <div class="checkBox <# if(IsShowCity==1){#> on <#} #> addroption" data-name="allList" data-value="showName">
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate" data-text="市" data-idname="IsShowCity"  data-showclass="_City" data-realvalue="<#=IsShowCity  #>"   value="<#=IsShowCity  #>"  type="text" />
                            </div>
                            <em  style="display:none;"></em><span>
                            <input type="text" class="Qcombobox"  placeholder="请输入" value="市" style="margin-left: 0px; margin-right: 18px; width: 142px;"></span></div>
                        <div class="checkBox <# if(IsShowCounty==1){#> on <#} #> addroption" data-name="allList" data-value="showName">
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate" data-text="区/县" data-idname="IsShowCounty"  data-showclass="_County" data-realvalue="<#=IsShowCounty  #>"   value="<#=IsShowCounty  #>"  type="text" />
                            </div>
                            <em  style="display:none;"></em><span>
                            <input type="text" class="Qcombobox"  placeholder="请输入" value="区/县" style="margin-left: 0px; margin-right: 18px; width: 142px;"></span></div>

                    </div>

                    <div class="tit">详细地址:</div>
                    <div class="wrapInput">
                        <textarea rows="2" cols="2"></textarea>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                          <div class="checkBox"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate  Repeat" data-text="不能和已有数据重复" data-idname="NoRepeat" data-realvalue="<#=NoRepeat  #>"  value="<#=NoRepeat  #>"   type="text" />
                            </div>
                            <em></em><span>不能和已有数据重复</span></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">地址<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #> >*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
                
                <div class="showaddress Province" <# if(IsShowProvince==0){#>  style="display:none;" <#} #>>
                <input type="text" class="Qcombobox "  placeholder="请输入" value="省" style="margin-left: 0px; margin-right: 18px; width: 104px;">
                    </div>
                <div class="showaddress City" <# if(IsShowProvince==0){#>  style="display:none;" <#} #>>
                <input type="text" class="Qcombobox "  placeholder="请输入" value="市" style="margin-left: 0px; margin-right: 18px; width: 104px;">
                    </div>
                <div class="showaddress County" <# if(IsShowProvince==0){#>  style="display:none;" <#} #>>
                <input type="text" class="Qcombobox"  placeholder="请输入" value="区/县" style="margin-left: 0px; margin-right: 18px; width: 104px;">
                    </div>
                <textarea rows="2" readonly="readonly" cols="2"></textarea>
            </div>


        </div>
    </script>


    <%-- 日期 --%>
    <script id="tpl_date" type="text/html">

        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true">
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
             <div style="display:none;">
                            <input class="Questiondata " data-text="题目类型" data-idname="QuestionidType" data-realvalue="8" value="8"  type="text" />
                        </div>
            <div id="Div7" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">日期</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text7" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                 <div class="jsAreaTitle clearfix uploadItem">
                    <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">日期:</div>
                    <div class="wrapInput">
                        <div style="width:160px">
                        <input data-text="默认值" data-idname="DefaultValue" class=" Questiondata DefaultValue Qdatebox"  data-realvalue="<#=DefaultValue  #>"  value="<#=DefaultValue  #>" type="text"/>
                        </div>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">

                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                        <div class="checkBox checkBoxwidth"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate IsValidateStartDate" data-text="起始日期" data-idname="IsValidateStartDate" data-realvalue="<#=IsValidateStartDate  #>" value="<#=IsValidateStartDate  #>"  type="text" />
                            </div>
                            <em></em><span>起始日期&nbsp;</span><input class="Questiondata Qdatebox StartDate" type="text" data-idname="StartDate" data-realvalue="<#=StartDate  #>" value="<#=StartDate  #>"  /></div>
                        <div class="checkBox checkBoxwidth"  >
                            <div style="display:none;">
                                <input class="Questiondata checkBoxValidate IsValidateEndDate" data-text="结束日期" data-idname="IsValidateEndDate" data-realvalue="<#=IsValidateEndDate  #>" value="<#=IsValidateEndDate  #>"  type="text" />
                            </div>
                            <em></em><span>结束日期&nbsp;</span><input class="Questiondata Qdatebox EndDate" type="text" data-idname="EndDate" data-realvalue="<#=EndDate  #>" value="<#=EndDate  #>" /></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">日期<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
               <input type="text" class="Qdatebox titletext" value="<#=DefaultValue  #>" />
            </div>


        </div>

    </script>


    <%-- 图片单选 --%>
    <script id="tpl_imgsingleselection" type="text/html">


        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true">
            <div class="optiondel" style="display:none;">
                           
             </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="9" value="9"  type="text" />
                        </div>
            <div id="Div8" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">图片单选</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                       <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text8" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                <div class="jsAreaTitle clearfix uploadItem">
                   <div class="tit">图片:</div>
                    <div class="wrapPic ">
                         <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">答案:</div>
                    <div class="wrapPics">
                         <div class="optionlist editimgoptions">
                                 <# for(var i=0;i<Optionlist.length;i++){#>
                                <div class="editimgoption">
                                        <div style="display:none;">
                                    <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                                </div>
                                    <div style="display:none;">
                                    <input class="optiondata" data-text="选项图片路径" data-idname="OptionPicSrc" data-realvalue="<#=Optionlist[i].OptionPicSrc #>" value="<#=Optionlist[i].OptionPicSrc #>"  type="text" />
                                </div>
                                <img src="<#=Optionlist[i].OptionPicSrc #>" /><div class="editimgoptioninput">
                                    <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="<#=Optionlist[i].OptionContent#>" value="<#=Optionlist[i].OptionContent#>"  placeholder="请输入" maxlength="500" type="text" /></div>
                                <div class="optionclose">
                                    <img src="/Module/QuestionnaireNews/images/hintClose.png" /></div>
                            </div>
                                <#}#>
                             </div>
                        <div class="addeditimgoption">
                            <img src="/Module/QuestionnaireNews/images/add.png" />添加
                        </div>
                        <input class="addeditimgoptionbtn" type="file" />
                        <div class="alertmessage">
                            <span class="imgmessage">建议尺寸200px*200px，50KB以内</span>
                            <span class="tips"><span>Tips</span>单次最多可上传5张</span>
                        </div>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">图片单选<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                 <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
                
                <div class="Imgoptionlist">

                     <# for(var i=0;i<Optionlist.length;i++){#>
                         <div class='Imgoption'><img src="<#=Optionlist[i].OptionPicSrc #>" /><div class='radio '><em></em>&nbsp;<#=Optionlist[i].OptionContent#></div></div>
                     <#}#>

                </div>

              
            </div>


        </div>
    </script>


    <%-- 图片多选 --%>
    <script id="tpl_imgMultiselect" type="text/html">

        <div class="question <# if(isedit){#> select <#} #> questionnew" draggable="true">
            <div class="optiondel" style="display:none;">
                           
             </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                        </div>
             <div style="display:none;">
                            <input class="Questiondata" data-text="题目类型" data-idname="QuestionidType" data-realvalue="10" value="10"  type="text" />
                        </div>
            <div id="Div9" class="handleLayer" <# if(!isedit){#> style="display:none" <#} #>>
                <div class="option">
                    <img class="arrows" src="images/leftJiantou.png">
                    <h2 class="title">图片多选</h2>
                    <div class="wrapBtn"><span class="jsCancelBtn">取消</span>								    <span class="jsSaveCategoryBtn">保存</span>							    </div>
                </div>


                <div class="Separateline"></div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">标题:</div>
                    <div class="wrapInput">
                        <input data-text="标题" data-idname="Name" data-realvalue="<#=Name  #>" class="easyui-validatebox Qtitle Questiondata" placeholder="请输入" id="Text9" maxlength="500" name="Name" type="text" value="<#=Name  #>">
                    </div>
                </div>

                <div class="jsAreaTitle clearfix uploadItem">
                     <div class="tit">图片:</div>
                    <div class="wrapPic ">
                        <div style="display:none;">
                             <input class="Questiondata" data-text="题目图片路径id" data-idname="QuestionPicID" data-realvalue="<#=QuestionPicID  #>"  type="text" />
                            <input class="Questiondata imgvalue" data-text="题目图片路径" data-idname="Src" data-realvalue="<#=Src  #>"  type="text" />
                        </div>
                        <div class="uploadimg ">
                            <img class="img" src="<#=Src  #>" />
                            <img src="/Module/QuestionnaireNews/images/uploadpic.png" />
                        </div>
                        <span class="uploadBtn uploadbtnleft"><em class="upTit">上传</em><input class="uploadImgBtn input" type="file" /></span>
                        <span class="imgmessage">建议尺寸640px*360px，50KB以内</span>
                    </div>
                </div>

                <div class="jsAreaTitle clearfix">
                    <div class="tit">答案:</div>
                    <div class="wrapPics">
                        <div class="optionlist editimgoptions">

                            <# for(var i=0;i<Optionlist.length;i++){#>
                                <div class="editimgoption">
                                    <div style="display:none;">
                                        <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                                    </div>
                                    <div style="display:none;">
                                    <input class="optiondata" data-text="选项图片路径" data-idname="OptionPicSrc" data-realvalue="<#=Optionlist[i].OptionPicSrc #>" value="<#=Optionlist[i].OptionPicSrc #>"  type="text" />
                                </div>
                                <img src="<#=Optionlist[i].OptionPicSrc #>" /><div class="editimgoptioninput">
                                    <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue="<#=Optionlist[i].OptionContent#>" value="<#=Optionlist[i].OptionContent#>"  placeholder="请输入" maxlength="500" type="text" /></div>
                                <div class="optionclose">
                                    <img src="/Module/QuestionnaireNews/images/hintClose.png" /></div>
                            </div>
                                <#}#>

                               
                        </div>
                        <div class="addeditimgoption">
                            <img src="/Module/QuestionnaireNews/images/add.png" />添加
                        </div>
                        <input class="addeditimgoptionbtn" type="file" />
                        <div class="alertmessage">
                            <span class="imgmessage">建议尺寸200px*200px，50KB以内</span>
                            <span class="tips"><span>Tips</span>单次最多可上传5张</span>
                        </div>
                    </div>
                </div>

                <div class="Separateline"></div>

                <div class="jsAreaTitle line">
                    <div class="tit">验证:</div>
                    <div class="wrapRadio">
                        <div class="checkBox"  >
                             <div style="display:none;">
                                <input class="Questiondata checkBoxValidate Required" data-text="必填" data-idname="IsRequired" data-realvalue="<#=IsRequired  #>" value="<#=IsRequired  #>"   type="text" />
                            </div>
                            <em></em><span>必填</span></div>
                    </div>
                </div>


            </div>




            <div class="questiontitle">图片多选<span><img class="btn_move" src="/Module/QuestionnaireNews/images/move.png" title="添加" /><img class="btn_enlarge" src="/Module/QuestionnaireNews/images/enlarge.png"  title="鼠标左键按住不放进行拖动" /><img class="btn_del" src="/Module/QuestionnaireNews/images/del.png" title="删除" /></span></div>
            <div class="questionbody">
                <div class="qtext"><span class="red" <# if(IsRequired==0){#> style="display:none;" <#} #>>*</span><span class="title"><#=Name  #></span><img src="<#=Src  #>"></div>
                
                <div class="Imgoptions">
                     <# for(var i=0;i<Optionlist.length;i++){#>
                         <div class='Imgoption'><img src="<#=Optionlist[i].OptionPicSrc #>" /><div class='checkBox '><em></em>&nbsp;<#=Optionlist[i].OptionContent#></div></div>
                     <#}#>
                </div>

            </div>


        </div>

    </script>

    
    <%-- 选项 --%>
    <script id="tpl_option" type="text/html">

        <div class="option">
            <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue=""  placeholder="请输入" value="" maxlength="500" type="text" /><div class="operationbtn"><span class="addbtn">添加</span><span class="delbtn">删除</span></div>
        </div>
    </script>

  
    <%-- 图片选项 --%>
    <script id="tpl_imgoption" type="text/html">
        <div class="editimgoption">
                <div style="display:none;">
                <input class="optiondata" data-text="选项图片路径" data-idname="OptionPicSrc" data-realvalue="<#=_url #>" value="<#=_url #>"  type="text" />
            </div>
            <img src="<#=_url #>" /><div class="editimgoptioninput">
                <input class="optiontext optiondata" data-idname="OptionContent" data-realvalue=""  placeholder="请输入" maxlength="500" type="text" value="" /></div>
            <div class="optionclose">
                <img src="/Module/QuestionnaireNews/images/hintClose.png" /></div>
        </div>
    </script>

     <%-- 设置得分单选选项 --%>
    <script id="tpl_SetScoreRadioOption" type="text/html">
        <div class="OptionScoreitem">
           <div class="OptionScoreContent">
                <div style="display:none;">
                                        <input class="Questiondata" data-text="选项标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                                    </div>
                             
                    <div class="titleoptionitem"><div class="title"><span>*</span><#= index #>、<#=Name#></div> <div class="titleEdit">单选题&nbsp;&nbsp;本题得分：<span class="Scorevaluetext"> 0-0</span></div></div>
                    

                     <# for(var i=0;i<Optionlist.length;i++){#>
                         <div class="optionitem optionitemdata" >
                              <div style="display:none;">
                                        <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                                    </div>
                             
                             <div class="radio on">
                                 <em></em>&nbsp;<#=Optionlist[i].OptionContent#></div> <div class="setoption"> <input type="text" class="optiondata Scorevalue" data-text="答对选项分值"  data-idname="YesOptionScore" data-realvalue="<#=Optionlist[i].YesOptionScore  #>" value="<#=Optionlist[i].YesOptionScore  #>"  />分</div></div>
                     <#}#>           
                </div>
            </div>
        </script>

     <%-- 设置得分多选选项 --%>
    <script id="tpl_SetScoreCheckBoxOption" type="text/html">
         <div class="OptionScoreitem">
                <div class="OptionScoreContent">
                      <div style="display:none;">
                                        <input class="Questiondata" data-text="选项标识" data-idname="Questionid" data-realvalue="<#=Questionid  #>" value="<#=Questionid  #>"  type="text" />
                                    </div>
                             
                    <div class="titleoptionitem"><div class="title"><span>*</span><#=index#>、<#=Name#></div> <div class="titleEdit">多选题&nbsp;&nbsp;本题得分：<span class="Scorevaluetext"> 0-0</span></div></div>
                    <div class="optionitem"><div class="setoption">计分方式: <input class="Questiondata Questioncombobox" data-idname="ScoreStyle" data-realvalue="" value="<#=ScoreStyle  #>" type="text" /></div></div>
                     <# for(var i=0;i<Optionlist.length;i++){#>
                         <div class="optionitem optionitemdata" >
                             <div style="display:none;">
                                        <input class="optiondata" data-text="选项标识" data-idname="OptionID" data-realvalue="<#=Optionlist[i].OptionID  #>" value="<#=Optionlist[i].OptionID  #>"  type="text" />
                                    </div>
                             
                             <div class="checkBox on">
                                 <em></em>&nbsp;<#=Optionlist[i].OptionContent#></div> <div class="setoption"><div class="RightValue checkBox<# if(Optionlist[i].IsRightValue==1){#> on <#} #>" <# if(ScoreStyle==1){#> style="display:none;" <#} #> >
                                      <div style="display:none;">
                                        <input class="optiondata checkBoxValidate" data-text="是否是正确值" data-idname="IsRightValue" data-realvalue="<#=Optionlist[i].IsRightValue  #>" value="<#=Optionlist[i].IsRightValue  #>"   type="text" />
                                    </div>
                                     <em></em>设为正确答案</div><span class="OptionScore" <# if(ScoreStyle==3){#> style="display:none;" <#} #>><input type="text" class="optiondata  Scorevalue" data-text="答对选项分值"  data-idname="YesOptionScore" data-realvalue="<#=Optionlist[i].YesOptionScore  #>" value="<#=Optionlist[i].YesOptionScore  #>"  />分</span> </div></div>
                     <#}#>    
                    <div class="optionitem allRight" <# if(ScoreStyle!=3){#> style="display:none;" <#} #>><div class="setoption">全部答对得 <input  class="Questiondata  allScorevalue" type="text" data-idname="MaxScore" data-realvalue="<#=MaxScore==null?0:MaxScore  #>" value="<#=MaxScore==null?0:MaxScore   #>"  />分</div></div>
                </div>
            </div>
                                
        </script>


     
<!-- 遮罩层 -->
<div class="jui-mask"></div>
        <!--新增描述，弹出-->
<div class="jui-dialog jui-dialog-describe" style="display:none;" >
	<div class="jui-dialog-tit">
    	<h2>描述</h2>
        <span class="jui-dialog-close"></span>
    </div>
    <div class="describeContent">
        <form ></form>
    	<form id="describe" class="describe" >
            <div style="display:none;">
                <input class="easyui-validatebox ScoreRecoverydata" data-text="标识" name="ScoreRecoveryInformationID"  value=""  type="text" />
            </div>

        <div class="commonSelectWrap">
            <em class="tit">得分段：</em>
            <label class=" clearBorder">
              <input data-text="得分段" class="easyui-combobox ScoreRecoverydata " id="MinScore"  name="MinScore" data-options="editable:false,width:100,height:32" data-flag=""   type="text" value="">
                至
                <input data-text="得分段" class="easyui-combobox ScoreRecoverydata" id="MaxScore"   name="MaxScore"  data-options="editable:false,width:100,height:32" data-flag=""  type="text" value="">
            </label>
        </div>
        
        <div class="commonSelectWrap">
            <em class="tit">描述方式：</em>
            <label class=" clearBorder">
              <input data-text="描述方式" class="easyui-combobox ScoreRecoverydata" id="RecoveryType" name="RecoveryType"  data-options="editable:false,width:190,height:32" data-flag=""  type="text" value="">
            </label>
        </div>


        <div class="commonSelectWrap" id="RecoveryContent" style="display:none;">
            <em class="tit">描述内容：</em>
            <label class="searchInput clearBorder">
              <input data-text="描述内容" class="easyui-validatebox ScoreRecoverydata" id="RecoveryContentvalue" name="RecoveryContent" data-options="required:true,width:190,height:32" data-flag="" type="text" value="">
            </label>
        </div>
        
    
          

             <div  class="commonSelectWrap uploadItem"  id="RecoveryImg" >
                            
                <p class="tit">添加图片：</p>
                  <div style="display:none;">
                <input class="ScoreRecoverydata imgvalue" id="RecoveryImgvalue" data-text="图片路径" name="RecoveryImg"   type="text" />
            </div>
                <div class="infoBox">
                    <div style="display: inline-block;">
                    <div class="msimg"><img  src="/Module/QuestionnaireNews/images/zswj.png" /></div>
                        <a href="javascript:void(0)" data-uploadimgwidth=100  class="commonHandleBtn uploadImgBtn"></a>
                        </div>
                    <div class="exp">(建议尺寸550*450 大小为50K)</div>
                </div>
            </div>
        
        
            <div style="clear:both;"></div>
        </form>
        
        <div class="btnWrap">
        	<a href="javascript:;" class="commonBtn saveBtn">保存</a>
            <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
        </div>
    </div>
</div>

    
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/Module/QuestionnaireNews/js/main.js"%>"></script>
</asp:Content>
