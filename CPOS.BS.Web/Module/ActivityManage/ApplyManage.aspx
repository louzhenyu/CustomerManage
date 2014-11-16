<%@ Page Title="报名管理" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <script id="loadEventSummary" language="javascript" type="text/javascript"></script>
    <title>报名管理</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="js/applyManage">
        <form action="EventSave" name="EventForm" id="eventForm" method="post">
            <div class="activityInfo-item">
                <div class="infoBox">
                    <div class="titWrap t-overflow">
                        <h3 class="tit" name="EventTitle"></h3>
                        <span class="shelf" name="EventStatusText"></span>
                        <div class="getBack">返回</div>
                    </div>
                    <div class="other">
                        <p class="exp">主办方：<label name="SponsorText"></label></p>
                        <p class="timeSlot" name="BeginTime"></p>
                        <span class="read">已读<strong id="readcount">0</strong></span>
                        <span class="enroll">报名<strong id="signupCount">0</strong></span>
                    </div>
                </div>

                    <%--<a href="javascript:;" class="commonBtn editNum">编辑</a>--%>
            </div>
        </form> 

        <div class="commonTitleBox">
            <%--<a href="javascript:history.go(-1);" class="getBack">返回</a>--%>
            <h2 class="commonTitle"><a href="javascript:history.go(-1);">活动管理</a> > 报名人员列表</h2>
        </div>

        <div class="mainContent">
            
            <div id="tabMenu" class="tableMenu" >
                <a href="javascript:;" class="tabItem unsureTable first on" data-table="unsureTable" data-status="1"><span>未确认名单(<em>-</em>)</span></a>
                <a href="javascript:;" class="tabItem sureTable" data-table="sureTable" data-status="10"><span>已确认名单(<em>-</em>)</span></a>
            </div>
            <div class="tableWrap">
                <div class="tablehandle">
<%--                    <div class="selectBox">
                        <span class="text">按最近时间升序</span>
                        <div class="selectList">
                            <p>按最近时间降序</p>
                            <p>按最近时间升序</p>
                        </div>
                    </div>--%>
                    <!--<h3 class="count">显示<span>11</span>条数据</h3>-->
                    <a href="javascript:;" id="sendMessageBtn" class="commonBtn importBtn" style="display:none;">发送通知</a>
                    <a href="javascript:;" id="importBtn" class="commonBtn importBtn">导入名单</a>
                    <%--<a href="javascript:;" class="commonBtn exportBtn">导出</a>--%>
                </div>
                    
                <!-- 未确认名单表格 -->
                <table id="unsureTable" class="dataTable" style="">
                    <thead>
                    </thead>
                    <tbody>
                        <tr >
                            <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="9" align="center"> <span><img src="../static/images/loading.gif"></span></td>
                        </tr> 
                    </tbody>
                </table>
                    
                <!-- 已确认名单表格 -->
                <table id="sureTable" class="dataTable" style="display:none;">
                    <thead>
                        
                    </thead>
                    <tbody>
                        <tr >
                            <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="9" align="center"> <span><img src="../static/images/loading.gif"></span></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="pageContianer">
                <div id="kkpager" style="text-align:center;"></div>
            </div>
        </div>

        <!-- 弹层，导入-->
        <div class="ui-pc-mask" id="ui-mask" style="display:none;"></div>
        <div id="importDiv" class="jui-dialog jui-dialog-import" style="display:none">
        <div class="jui-dialog-tit">
    	    <h2>导入</h2>
            <a href="javascript:;" class="jui-dialog-close hintClose"></a>
        </div>
        <div class="promptContent" style="display:block" id="step1">
    	    <div class="stepWrap">
        	    <span class="step01"></span>
                <span class="step02"><em></em></span>
            </div>
            <p class="exp">点击下载标准导入模板，并根据模板填写内容，如已完成此项可点击下一步</p>
            <a href="javascript:;" id="downloadTmpl" class="downTmplBtn"></a>
            <div class="btnWrap">
                <a href="javascript:;" id="nextStep" class="commonBtn">下一步</a>
            </div>
        </div>
    
        <div class="promptContent stepTwo" id="step2" style="display:none">
    	    <div class="stepWrap">
        	    <span class="step01"></span>
                <span class="step02 "><em></em></span>
            </div>
            <p class="exp">请导入标准格式，您可以点击返回按钮来下载模板</p>
            <div class="uploadArea">
        	    <a href="javascript:;" class="uploadBtn"></a>
                <input id="file_upload" name="file_upload" type="file" />

        	    <p class="uploadInput"><input id="uploadText" type="text" value=""></p>
            
            </div>
            <div class="btnWrap">
        	    <a href="javascript:;" id="backStep" class="commonBtn cancelBtn">返回</a>
                <a href="javascript:;" id="comitUpload" class="commonBtn">完成</a>
            </div>
        </div>
 
    </div>
    </div>

    <script id="unsureTheadTemp" type="text/html">
        <#for(var i in obj){#>
        <th><#=obj[i]#></th>
        <#}#>
        <th>操作</th>
    </script>
    <script id="unsureTbodyTemp" type="text/html">
        <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
        <tr>
            <#for(var e in idata){#>
            <td><#=idata[e]#></td>
            <#}#>
            <td><span data-id="<#=list.otherItems[i].SignUpID#>" class="signUpBtn joinBtn">确定参加</span></td>
        </tr>
        <#} #>
    </script>
    <script id="sureTheadTemp" type="text/html">
        <th class="checkBox"><em></em></th>
        <#for(var i in obj){#>
            <th><#=obj[i]#></th>
        <#}#>
    </script>
    <script id="sureTbodyTemp" type="text/html">
        <#for(var i=0,idata;i<list.finalList.length;i++){ idata=list.finalList[i]; #>
        <tr>
            <td class="checkBox" data-id="<#=list.otherItems[i].SignUpID #>"><em></em></td>
            <#for(var e in idata){#>
                <# var result=0;
                if(e=="SendStatus"){
                    if(idata[e]==0){
                        result="未发送";
                    }else{
                        result="已发送";
                    }
            
             }
             if(e=="IsPay"){
                if(idata[e]==0){
                    result="未支付";
                }else{
                    result="已支付";
                }
             }
        
            #>
            <td>
                <# if(e=="GroupName"){#>
                    <input type="text" class="group" data-id="<#=list.otherItems[i].SignUpID #>" value="<#=idata[e]#>">
                 <#}else{#>
                    <#=result?result:idata[e]#>
                <#}#>
           </td>
            <#}#>
        </tr>
        <#} #>
    </script>

</asp:Content>