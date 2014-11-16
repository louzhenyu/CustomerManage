<%@ Page Title="活动详情" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master" AutoEventWireup="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link  href="../static/css/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <link  href="/module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Framework/Javascript/Other/kindeditor/themes/default/default.css?v=Math.random()" />
    <title>活动详情</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="js/activityManage">
        <div class="subMenu">
            <ul class="clearfix">
                <li class="modify on" data-href="">活动详情</li>
                <li class="apply" data-href="ApplyManage.aspx">报名管理</li>
                <li class="sign" data-href="SignupManage.aspx">签到管理</li>
            </ul>
        </div>
        <div class="commonTitleBox">
            <a href="javascript:history.go(-1);" class="getBack">返回</a>
            <h2 class="commonTitle">活动详情</h2>
        </div>
        <div class="mainContent">
            <div class="addInfoArea">
                <form action="EventSave" name="EventForm" id="eventForm" method="post">
                    <input id="eventID" name="EventID" type="text" value="" style="display: none;" />
                <div class="item-line">
                    <div class="commonSelectWrap">
                        <em class="tit"><span class="fontRed">*</span> 标题</em>
                        <p class="searchInput w-596">
                            <input id="eventTitle" name="EventTitle" type="text" value="" /></p>
                    </div>
                </div>
                <div class="item-line">
                    <div class="commonSelectWrap classInput">
                        <em class="tit"><span class="fontRed">*</span>活动分类</em>
                        <div id="eventTypeSelect" name="EventTypeID" class="selectBox">
                        </div>
                    </div>
                    <div class="commonSelectWrap authorInput">
                        <em class="tit">主办方</em>
                        <div id="sponsorSelect" name="Sponsor" class="selectBox">
                        </div>
                    </div>
                </div>
                <div class="item-line">
                    <div class="commonSelectWrap">
                        <em class="tit">开始时间：</em>
                        <p class="searchInput"><input type="text" id="beginTime" name="BeginTime" /></p>		

                        <em class="tit">结束时间：</em>
                        <p class="searchInput"><input type="text" id="endTime" name="EndTime" /></p>	
                    </div>
                </div>
                <div class="item-line desc" style="height: 170px;">
                    <div class="commonSelectWrap">
                        <em class="tit">描述</em>
                        <p class="textarea"><textarea id="description" name="Description" style="width:670px;"></textarea></p>
                    </div>
                </div>

                <div class="item-line">
                    <div class="commonSelectWrap">
                        <em class="tit">活动地点</em>
                        <p class="searchInput w-596">
                            <input id="address" name="Address" type="text" value="" />
                        </p>
                    </div>
                </div>

                <div class="item-line">
                    <div class="commonSelectWrap authorInput">
                        <em class="tit">人数限制</em>
                        <p class="searchInput">
                            <input id="personLimit" name="PersonLimit" type="text" value="" /></p>
                    </div>
                    <div class="commonSelectWrap authorInput">
                        <em class="tit">初始人数</em>
                        <p class="searchInput">
                            <input id="beginPersonCount" name="BeginPersonCount" type="text" value="" /></p>
                    </div>
                </div>

                <div class="item-line">
                    <div class="commonSelectWrap classInput">
                        <em class="tit">活动费用</em>
                        <p class="searchInput">
                            <input id="eventFee" name="EventFee" type="text" value="" /></p>
                    </div>
                </div>

                <div class="item-line">
                    <div class="commonSelectWrap classInput">
                        <em class="tit">报名字段</em>
                        <table id="dynamicFormTable" name="FieldList" class="dataTable checkboxGroup" style="display:inline-table;">
                            <thead>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="height: 150px; text-align: center; vertical-align: middle;" colspan="9" align="center"><span>
                                    <img src="../static/images/loading.gif"></span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="item-line">
                    <div class="commonSelectWrap classInput">
                        <em class="tit"></em>
                        <div class="addInfoArea"><p class="radioBox checkboxSingle" name="IsSignUpList"><em></em><span>允许显示报名人员</span></p></div>
                    </div>
                </div>

                <div class="btnWrap">
                    <a id="saveBtn" href="javascript:;" class="commonBtn">保存</a> <a id="cancelBtn" href="javascript:;"
                        class="commonBtn cancelBtn">取消</a>
                </div>
                </form>
            </div>
        </div>
    </div>
    <div id="maskDisplay" class="ui-pc-mask" style="display:none"></div>

    <script id="selectTemp" type="text/html">
        <span class="selected text"></span>
        <div class="selectList">
            <p class="option" data-val=""></p>
            <# for(var i=0,idata;i<list.length;i++){ idata=list[i] ;#>
                <p class="option" data-val="<#=idata.id#>"><#=idata.name#></p>
            <#}#>
        </div>
    </script>
    <script id="dynamicFormTableTemp" type="text/html">
    <#for(var i=0,idata; i < list.length; i++){ idata=list[i]; #>
    <tr>
        <td><#=idata.hierarchy#></td>
        <#for(var j=0; j < idata.data.length; j++){ #>
            <#if(j == 0 || j%3 != 0){#>
                <td><div class="addInfoArea"><p class="radioBox" name="PublicControlID" data-val="<#=idata.data[j].PublicControlID#>"><em></em><span><#=idata.data[j].ColumnDesc#></span></p></div></td>
                <!--补td-->                
                <#if(j == (idata.data.length - 1) && j%3 != 0){ 
                    var k = (j%3) + 1;
                    for(k; k < 3; k++){ #>
                        <td></td>
                <#  } 
                } #>
            <!--超过3个换行-->
            <#} else {#>
                </tr>
                <tr>
                    <td></td>
                    <td><div class="addInfoArea"><p class="radioBox" name="PublicControlID" data-val="<#=idata.data[j].PublicControlID#>"><em></em><span><#=idata.data[j].ColumnDesc#></span></p></div></td>
            <#} #>
        <#} #>
    </tr>
    <#} #>
    </script>

</asp:Content>
