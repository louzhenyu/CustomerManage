<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta charset="UTF-8" />
    <title>提现管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="css/sendingManager.css?ver=0.7"/>

</asp:Content>
<asp:Content ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<body cache>
<div class="allPage" id="section" data-js="js/sendingManager.js">
    <!-- 内容区域 -->
    <div class="totalStatistic">
        <div class="totalContent">

            <div class="itemTotle">
                <div class="head">
                    <div class="title">
                        <img src="images/rightList.png">
                        <span>提现金额总计</span>
                    </div>
                </div>
                <div class="content" style="border-bottom:1px solid #ccc">
                    <div class="record">
                        <p><em class='totle'>0</em><i>元</i></p>
                    </div>
                </div>
                <div class="content">
                    <ul>
                        <li>
                            <p>会员</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>员工</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>分销商</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="itemTotle">
                <div class="head">
                    <div class="title">
                        <img src="images/rightList.png">
                        <span>可提现金额总计</span>
                    </div>
                </div>
                <div class="content" style="border-bottom:1px solid #ccc">
                    <div class="record">
                        <p><em class='totle'>0</em><i>元</i></p>
                    </div>
                </div>
                <div class="content">
                    <ul>
                        <li>
                            <p>会员</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>员工</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>分销商</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item">
                <div class="head">
                    <div class="title">
                        <img src="images/icon2.png">
                        <span>待批准提现</span>
                    </div>
                </div>
                <div class="content" style="border-bottom:1px solid #ccc">
                    <a class="viewPoint">看笔数</a>
                    <div class="record">
                        <p><em class='totle'>0</em><i>元</i></p>
                    </div>
                </div>
                <div class="content">
                    <ul>
                        <li>
                            <p>会员</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>员工</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>分销商</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="item">
                <div class="head">
                    <div class="title">
                        <img src="images/icon3.png">
                        <span>当年已完成提现</span>
                    </div>
                </div>
                <div class="content" style="border-bottom:1px solid #ccc">
                    <a class="viewPoint">看笔数</a>
                    <div class="record">
                        <p><em class='totle'>0</em><i>元</i></p>
                    </div>
                </div>
                <div class="content">
                    <ul>
                        <li>
                            <p>会员</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>员工</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                        <li>
                            <p>分销商</p>
                            <p><em class='totle'>0</em><i>元</i></p>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="packBtn top" data-type="1" style="color:#555;margin-bottom:20px;">收起</div>
    </div>

    <div class="contentArea_vipquery">
        <!--个别信息查询-->
        <div class="queryTermArea" id="simpleQuery" style="display:inline-block;width: 100%;border-top:1px dashed #ddd;border-bottom:1px dashed #ddd;" >
             <form></form>
            <form id="queryFrom"> <div class="item" style="width:990px;">
            <div class="commonSelectWrap">
                <em class="tit">提现单号：</em>
                <label class="searchInput"><input  name="WithdrawNo" type="text" value="" placeholder="请输入"/></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">姓名：</em>
                <label class="searchInput"><input   name="Name" type="text" value="" placeholder="请输入"/></label>
            </div>
            <div class="commonSelectWrap">
                <em class="tit">手机号：</em>
                <label class="searchInput "><input id="phone" name="Phone" class="easyui-validatebox" type="text" value="" data-options="validType:'mobile'" placeholder="请输入"/></label>
            </div>

             <div class="moreQueryWrap">
                  <a href="javascript:;" class="commonBtn queryBtn">查询</a>
              </div>

            <div class="contents" style="display: block">
                <div class="commonSelectWrap">
                    <em class="tit">类别：</em>
                    <div class="selectBox bordernone">
                      <input id="category" class="easyui-combobox" name="VipType" editable="false" />
                    </div>
                </div>


                <div class="commonSelectWrap">
                   <em class="tit">申请日期：</em>
                   <div class="searchInput bordernone whauto"  style="width: 202px; background: none;">
                       <div class="line">
                             <input id="ApplyStartDate"  name="ApplyStartDate" class="easyui-datebox"  data-options="width:120,height:32" /><span style="margin:0 5px;">至</span><input id="ApplyEndDate" name="ApplyEndDate" class="easyui-datebox" data-options="width:120,height:32" validType="compareDate[$('#ApplyStartDate').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/>
                       </div>
                    </div>
                 </div>

             </div>

            <div class="contents" style="display: block">
                <div class="commonSelectWrap">
                    <em class="tit">状态：</em>
                    <div class="selectBox bordernone">
                      <input id="cc" class="easyui-combobox" name="Status" editable="false" />
                    </div>
                </div>

                <div class="commonSelectWrap">
                   <em class="tit">完成日期：</em>
                   <div class="searchInput bordernone whauto"  style="width: 252px; background: none;">
                       <div class="line">
                             <input id="CompleteStartDate"  name="CompleteStartDate" class="easyui-datebox"  data-options="width:120,height:32" /><span style="margin:0 5px;">至</span><input id="CompleteEndDate" name="CompleteEndDate" class="easyui-datebox" data-options="width:120,height:32" validType="compareDate[$('#CompleteStartDate').datebox('getText'),'当前选择的时间必须晚于前面选择的时间']"/>
                       </div>
                    </div>
                 </div>
             </div>
            <div class="packBtn up" data-type="2">收起</div>


        </div>
     </form>
     </div>

        <!--表格操作按钮-->
        <div id="menuItems" class="optionBtn">
            <!--<span class="commonBtn _addVip">添加新会员</span>-->
           <!-- <span class="commonBtn exportBtn">打印</span>-->
            <span class="commonBtn affirmBtn w80 l" data-statusid=1>审核</span>
            <span class="commonBtn finishBtn w80 l" data-statusid=2>完成</span>

            <div class="commonBtn sales icon w80 icon_export r" data-flag="export">导出</div>

        </div>    
        <div class="tableWrap">
            <table class="dataTable"  id="dataTable">
                <div class="cursorDef">
                    <div class="dataTable" id="gridTable">
                        <div class="loading" style="padding-top:10px;">
                             <span>
                           <img src="../static/images/loading.gif"></span>
                        </div>
                    </div>
                </div>
            </table>
            <div id="pageContianer">
             <div class="dataMessage" >没有符合条件的查询记录</div>
                <div id="kkpager" ></div>
            </div>
        </div>
    </div>
</div>
    <div style="display: none">
      <div id="win" class="easyui-window" data-options="modal:true,shadow:false,collapsible:false,minimizable:false,maximizable:false,closed:true,closable:true" >
      		<div class="easyui-layout" data-options="fit:true" id="panlconent">

      			<div data-options="region:'center'" style="padding:10px;">
      				指定的模板添加内容
      			</div>
      			<div class="btnWrap" id="btnWrap" data-options="region:'south',border:false" style="height:80px;text-align:center;padding:20px 0 0;border-top:1px solid #e1e7ea;">
      				<a class="easyui-linkbutton commonBtn saveBtn" >确定</a>
      				<a class="easyui-linkbutton commonBtn cancelBtn"  href="javascript:void(0)" onClick="javascript:$('#win').window('close')" >取消</a>
      			</div>
      		</div>

      	</div>
      </div>
      <!--弹层-->
      <script id="tpl_window" type="text/html">
        <div class="tplWindow">
            <div class="edits">
                <em>审核：</em>
                <span type="1">审核通过</span>
                <span type="2">审核不通过</span>
            </div>

            <div class="edits">
                <em>备注：</em>
                <textarea>账户余额不足，审核不通过</textarea>
            </div>
        </div>
      </script>
    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>" defer async data-main="<%=StaticUrl+"/module/sendingTogether/js/main.js"%>" ></script>
    </body>
</asp:Content>