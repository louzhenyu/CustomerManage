<%@ Page Title="签到管理" Language="C#" MasterPageFile="~/Framework/MasterPage/College.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/module/styles/css/college/private.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/kkpager.css" rel="stylesheet" type="text/css" />
    <title>签到管理</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contentArea" id="section" data-js="js/signupManage">
        <div class="subMenu">
            <ul class="clearfix">
                <li class="modify" data-href="">活动详情</li>
                <li class="apply on" data-href="ApplyManage.aspx">报名管理</li>
                <li class="sign" data-href="SignupManage.aspx">签到管理</li>
            </ul>
        </div>
        <!-- 内容区域 -->
        <div class="contentArea">
            <!--菜单选中的内容 签到管理-->
            <div class="subMenuContentArea">
                <span class="commonBtn">微信签到</span>
                <p class="signCount">已签到<strong id="signCount">0</strong>人</p>
                <div class="tableWrap">
                    <div class="tablehandle">
                        <div class="selectBox">
                            <span class="text">按最近时间升序</span>
                            <div class="selectList">
                                <p>按最近时间降序</p>
                                <p>按最近时间升序</p>
                            </div>
                        </div>
                        <h3 class="count">显示<span id="countNum">0</span>条数据</h3>
                        <a href="javascript:;" class="exportBtn"></a>
                    </div>
                    
                    <!-- 签到管理 -->
                    <table class="dataTable" style="display:inline-table;">
                        <thead>
                            <tr class="title">
                                <th>姓名</th>
                                <th>手机号码</th>
                                <th>签到时间</th>
                            </tr>
                        </thead>
                        <tbody id="content">
                        	

                            <tr >
                                <td style="height: 150px;text-align: center;vertical-align: middle;" colspan="3" align="center"> <span><img src="../static/images/loading.gif"></span></td>
                            </tr>                        </tbody>
                    </table>
                    <div id="kkpager" style="text-align:center;"></div>
                </div>
            </div>
        
        
        </div>
    </div>

    <script id="tpl_content" type="text/html">
         <#for(var i=0;i<list.length;i++){ var item=list[i];var createTime=(item.CreateTime!=null)?item.CreateTime.replace("T"," "):"";#>
            <tr>
                <td><#=item.UserName#></td>
                <td class="fontF"><#=item.Mobile#></td>
                <td class="fontF"><#=createTime.substring(0,createTime.lastIndexOf("."))#></td>
            </tr>
        <#}#>
    </script>

</asp:Content>
