<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>触点活动</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <link href="<%=StaticUrl+"/module/TouchSpotActivityManagement/css/style.css?v=0.4"%>" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .textbox-addon-right {
            right: 9px !important;
            top: 1px;
        }

        .queryTermArea .commonSelectWrap {
            margin-right: 0px;
        }

        .commonSelectWrap .tit {
            width: 102px;
        }

        .moreQueryWrap .queryBtn {
            width: 72px;
            margin: 3px 70px 0 0;
        }

        .datagrid-btable .handle {
            height: 39px;
            line-height: 38px;
            cursor: pointer;
        }

        .datagrid-btable .delete {
            background: url(images/delete.png) no-repeat center center;
        }

        .datagrid-btable .running {
            background: url(images/running.png) no-repeat center center;
        }

        .datagrid-btable .pause {
            background: url(images/pause.png) no-repeat center center;
        }

        .datagrid-btable .down {
            background-repeat: no-repeat;
            background-position: center center;
        }
       

        #tableWrap2 .datagrid-row, #tableWrap2 .datagrid-header-row {
            height: 62px;
            line-height: 61px;
        }

        #tableWrap2 .datagrid-btable {
            width: 100%;
        }
        /*活动参与，弹出*/
        .jui-dialog .dataTable {
            border: none;
        }

            .jui-dialog .dataTable tr {
                height: 49px;
                line-height: 48px;
            }

            .jui-dialog .dataTable th {
                text-align: center;
                font-size: 14px;
                color: #4d4d4d;
            }

            .jui-dialog .dataTable td {
                font-size: 13px;
                color: #666;
            }

            .jui-dialog .dataTable .tableHead {
                border-bottom: 2px solid #07c8cf;
            }

        .jui-dialog-table {
            width: 1026px;
            height: 706px;
            position: fixed;
            top: 30px;
            margin-left: -513px;
        }

        .jui-dialog-tit {
            height: 55px;
        }

            .jui-dialog-tit h2 {
                font: 16px/55px "Microsoft YaHei";
                color: #66666d;
            }

     

        .jui-dialog .tit {
            width: 250px;
        }

        .jui-dialog .activityContent {
            padding: 10px 0 30px 0;
        }

        .jui-dialog .searchInput {
            width: 190px;
        }

        .jui-dialog .commonSelectWrap {
            float: none;
            display: block;
            margin: 20px 0 2px 0;
        }

        .jui-dialog .hint-exp {
            padding-top: 5px;
            text-indent: 30px;
            text-align: center;
            font-size: 14px;
            color: #999;
        }

        .jui-dialog .btnWrap {
            padding: 15px 0 0 0;
        }

       

        .jui-dialog .cancelBtn {
            border: none;
            background: #ccc;
            color: #fff;
        }

        .exportTable {
            height: 75px;
            background: #f4f8fa;
        }

            .exportTable .exportBtn {
                display: inline-block;
                width: 110px;
                height: 32px;
                line-height: 32px;
                margin: 22px 0 0 30px;
                text-align: center;
                font-size: 14px;
                border-radius: 3px;
                background: #0cc;
                color: #f4f8fa;
            }
        /*弹层，分页*/
        /*底部分页的重写*/
        #kkpager2 {
            clear: both;
            height: 30px;
            line-height: 30px;
            margin-top: 10px;
            color: #999999;
            font-size: 14px;
        }

            #kkpager2 a {
                padding: 4px 8px;
                margin: 10px 3px;
                font-size: 12px;
                color: #9d9d9d;
                text-decoration: none;
            }

            #kkpager2 span {
                font-size: 14px;
            }

                #kkpager2 span.disabled {
                    padding: 4px 8px;
                    margin: 10px 3px;
                    font-size: 12px;
                    border: 1px solid #DFDFDF;
                    background-color: #FFF;
                    color: #DFDFDF;
                }

                #kkpager2 span.curr {
                    padding: 4px 8px;
                    margin: 10px 3px;
                    font-size: 12px;
                    border: 1px solid #FF6600;
                    background-color: #FF6600;
                    color: #FFF;
                }

            #kkpager2 a:hover {
                background-color: #FFEEE5;
                border: 1px solid #FF6600;
            }

            #kkpager2 span.normalsize {
                font-size: 12px;
            }

        #kkpager2 {
            clear: both;
            height: 30px;
            line-height: 23px;
            color: #323232;
            font-size: 12px;
            text-align: right;
            padding-right: 65px;
        }

            #kkpager2 span.normalsize {
                color: #323232;
            }

            #kkpager2 span.disabled, #kkpager a {
                border: none;
                color: #969696;
            }

            #kkpager2 a:hover {
                border: none;
                background: none;
                color: #00cccc;
            }

            #kkpager2 span.curr {
                color: #00cccc;
                font-size: 14px;
                font-weight: 600;
                border: none;
                background: none;
            }


      

        

      

        #tableWrap .addBtn {
            display: block;
            width: 100%;
            height: 39px;
            background: url(images/icon-add.png) no-repeat center center;
            cursor: pointer;
        }

        #tableWrap .delBtn {
            display: block;
            width: 100%;
            height: 49px;
            background: url(images/delete.png) no-repeat center center;
            cursor: pointer;
        }

        /*添加活动*/
        .jui-dialog-addShare {
            width: 636px;
            height: auto;
            max-height:665px;
            position: fixed;
            top: 10px;
            margin-left: -318px;
        }

        .ruleSetTit {
            margin-bottom: 0px;
            margin: 21px;
            padding: 15px 0 0;
            font-size: 16px;
            font-family: "黑体";
            border-top: 1px solid #ccc;
            color: #66666d;
        }

        .jui-dialog-addShare .btnWrap {
            padding: 15px 0 15px 0;
        }

        .iconPlay, .iconPause, .editBtn, .running {
            display: inline-block;
            width: 30px;
            height: 90px;
        }

        .iconPlay {
            background: url(images/running.png) no-repeat center center;
        }

        .iconPause {
            background: url(images/pause.png) no-repeat center center;
        }

        .editBtn {
            background: url(images/edit.png) no-repeat center center;
        }

        .jui-dialog .datebox {
            border: 1px solid #ccc;
        }

        .dateInput {
            margin-left: 5px;
        }


         /*添加详情*/
        .jui-dialog-activityDetail {
            width: 636px;
            height: auto;
            max-height:665px;
            position: fixed;
            top: 5px;
            margin-left: -318px;
        }

      

        .jui-dialog-activityDetail .btnWrap {
            padding: 15px 0 15px 0;
        }

      

        /*追加奖品数量*/
.jui-dialog-prizeCountAdd{width:636px;height:322px;position:fixed;top:20%;margin-left:-318px;}
.prizeCountAddContent .commonSelectWrap{margin:60px 0 45px 0;}
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="allPage" id="section" data-js="js/ActivityEvent.js?ver=0.3">
        <!-- 内容区域 -->
        <div class="contentArea_vipquery">

            <!--个别信息查询-->
            <div class="optionBtn">
                <a href="javascript:;" class="commonBtn  icon w100  icon_add r" id="addShareBtn">添加活动</a>
            </div>
            <div class="tableWrap" id="tableWrap" style="display: inline-block; width: 100%;">
                <table class="dataTable" id="gridTable">
                     <div class="dataTable" id="dataTable">
                          <div  class="loading">
                                   <span>
                                 <img src="../static/images/loading.gif"></span>
                            </div>
                    </div>
                </table>

                <div id="pageContianer">
                    <div class="dataMessage">没有符合条件的查询记录</div>
                    <div id="kkpager">
                    </div>
                </div>
            </div>
        </div>
    </div>



    <!-- 遮罩层 -->
    <div class="jui-mask" style="display: none;"></div>



    <!--添加活动，弹出-->
    <div class="jui-dialog jui-dialog-addShare" style="display: none">
        <div class="jui-dialog-tit">
            <h2>触点活动</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="redPackageContent">
            <form></form>
            <form id="addShareForm">
                
                <div class="commonSelectWrap">
                    <em class="tit">触点类型：</em>
                        <input id="ContactEventId" name="ContactEventId" value="" type="hidden" style="display: none" />
                        <input data-text="触点类型" class="easyui-combobox textbox combo" id="Activity_ContactTypeCode" data-options="required:true,width:190,height:32,invalidMessage:'必填',missingMessage:'必填'" missingMessage="必填" name="ContactTypeCode" type="text" value="" validtype='selectIndex'>
                    
                </div>

                <div class="commonSelectWrap">
                    <em class="tit">活动名称：</em>
                    <label class="searchInput clearBorder">
                       <input data-text="活动名称" class="easyui-validatebox" placeholder="请输入" id="Activity_ContactEventName" data-options="required:true,width:190,height:32,invalidMessage:'必填'" maxlength="40" name="ContactEventName" type="text" value="" validtype='length[1,60]'>
                   </label>
                </div>

                <div class="commonSelectWrap">
                    <em class="tit">活动时间：</em>
                    <label class="dateInput clearBorder">
                        <input id="startDate" name="BeginDate" class="easyui-datebox"  data-options="required:true,width:120,height:32" validType:'date' /><span>至</span>
                        <input id="expireDate" name="EndDate" class="easyui-datebox" data-options="required:true,width:120,height:32" validtype="compareEqualityDate[$('#startDate').datebox('getText'),'当前选择的开始时间必须晚于前面选择的结束时间']" />
                    </label>
                </div>
                 <div class="commonSelectWrap"  id="ActivitySelect"  style="display: none">
                    <em class="tit">活动选择：</em>
                        <input data-text="活动选择" class="easyui-combobox textbox combo" id="Activity_Select" data-options="width:190,height:32,invalidMessage:'必填',editable:false" name="ShareEventId" type="text" value="" validtype='selectIndex'>
                   
                </div>

                <div class="commonSelectWrap">
                    <em class="tit">奖品选择：</em>
                       <input data-text="奖品选择" class="easyui-combobox textbox combo" id="Activity_PrizeType" data-options="required:true,width:190,height:32,invalidMessage:'必填'" name="PrizeType" type="text" value="" validtype='selectIndex'>
                   
                </div>

                <div class="commonSelectWrap" >
                    <em class="tit">奖品数量：</em>
                      <input data-text="奖品数量" class="easyui-numberbox" min="0" max="1000000" placeholder="请输入" id="Activity_PrizeCount" data-options="required:true,width:190,height:32,invalidMessage:'奖品数量不能超过券的生成数量',missingMessage:'奖品数量不能超过券的生成数量'" name="PrizeCount" type="text" data-flag="" value="">
                    
                </div>

                <div class="commonSelectWrap" id="ActivityIntegral" style="display: none">
                    <em class="tit">积分：</em>
                        <input data-text="积分" class="easyui-numberbox" min="0" max="1000000" placeholder="请输入" id="Activity_Integral" data-options="width:190,height:32,invalidMessage:'必填',missingMessage:'积分必须为整数'" name="Integral" type="text" value="">
                   
                </div>

                <div class="commonSelectWrap" id="ActivityCouponType" style="display: none">
                    <em class="tit">优惠券：</em>
                       <input data-text="优惠券" class="easyui-combobox textbox combo" placeholder="请输入" id="Activity_CouponTypeID" data-options="height:32,invalidMessage:'优惠券数量为0时，触点活动状态自动更换为未启用。需要追加券后才可重新启用。',missingMessage:'优惠券数量为0时，触点活动状态自动更换为未启用。需要追加券后才可重新启用。'" name="CouponTypeID" type="text" value="" validtype='selectIndex'>
                    
                </div>

                <div class="commonSelectWrap" id="ActivityEvent" style="display: none">
                    <em class="tit">活动名称：</em>
                       <input data-text="活动名称" class="easyui-combobox textbox combo" id="Activity_EventId" data-options="width:190,height:32,invalidMessage:'必填'" name="EventId" type="text" value="" validtype='selectIndex'>
                    
                </div>

                <div class="commonSelectWrap" id="ActivityChanceCount" style="display: none">
                    <em class="tit">抽奖次数：</em>
                    
                        <input data-text="抽奖次数"  class="easyui-numberbox" min="0" max="100000" placeholder="请输入" id="Activity_ChanceCount" data-options="width:190,height:32,missingMessage:'抽奖次数必须为整数'" name="ChanceCount" type="text" value="">
                   
                </div>


                <div style="margin-bottom:0px;" class="ruleSetTit"></div>
                <div style="float: left;margin-left: 20px;">规则设置</div>
                <div class="commonSelectWrap" >
                    <em class="tit">奖励次数：</em>
                       <input data-text="奖励次数" class="easyui-combobox textbox combo" min="0" max="100000" placeholder="请输入" id="Activity_RewardNumber" data-options="required:true,width:190,height:32,invalidMessage:'必填'" name="RewardNumber" type="text" value="" validtype='selectIndex'>

                       
                    
                </div>
            </form>
            <div class="btnWrap">
                <a id="addActivity" href="javascript:;" class="commonBtn saveBtn ">保存</a>
                <span style="display:none;">
                <a id="cancelActivity"  href="javascript:;" class="commonBtn cancelBtn" style="margin-left: 16px;">取消</a>
                    </span>
            </div>
        </div>
    </div>


  

     <!--奖品数量追加，弹出-->
    <div class="jui-dialog jui-dialog-prizeCountAdd" style="display:none">
        <div class="jui-dialog-tit">
            <h2>奖品数量追加</h2>
            <span class="jui-dialog-close"></span>
        </div>
        <div class="prizeCountAddContent">
             <form id="prizeCount_Add">
                 <div class="commonSelectWrap">
                    <em class="tit" style="width:172px;">数量：</em>
                    <label class="searchInput clearBorder" style="width:308px">
                      <input data-text="奖品数量追加" class="easyui-numberbox" id="prizeCountAdd" min="0" data-options="required:true,width:308,height:32,invalidMessage:'奖品数量是大于0的整数'" data-flag="" name="prizeCount" type="text" value="">
                        
                    </label>
                </div>
                 </form>
            <div class="btnWrap">
                <a href="javascript:;" class="commonBtn saveBtn">保存</a>
                <a href="javascript:;" class="commonBtn cancelBtn" style="margin-left:16px;">取消</a>
            </div>
        </div>
    </div>
    

    <!--头部名称-->
    <script id="tpl_thead" type="text/html">
        <#for(var i in obj){#>
             <th><#=obj[i]#></th>
        <#}#>
    </script>


    <script type="text/javascript" src="<%=StaticUrl+"/Module/static/js/lib/require.min.js"%>"
        defer async="true" data-main="<%=StaticUrl+"/module/commodity/js/main.js"%>"></script>
</asp:Content>
