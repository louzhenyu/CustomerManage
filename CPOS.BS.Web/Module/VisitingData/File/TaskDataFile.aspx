<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/CPOS.Master" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<script src="Controller/TaskDataFileCtl.js" type="text/javascript"></script>
<script src="Model/TaskDataFileVM.js" type="text/javascript"></script>
<script src="Store/TaskDataFileVMStore.js" type="text/javascript"></script>
<script src="View/TaskDataFileView.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
var __imgpath='"<%=CurrentUserInfo.ImgPath %>"';
var __clientid='<%=CurrentUserInfo.ClientID %>';
</script>
<style type="text/css">
body,div{ margin:0; padding:0}
img{ border:0}
#container 
{
    text-align:center; 
}
#container .cell 
{
    
    border:1px solid #E3E3E3; 
    background:#F5F5F5; 
    margin:10px;
    *margin:9px;
    padding:5px;
    *padding:0px;
    float:left;
    width:202px;
    height:238px;

}
#container .celldiv
{ 
  background-color:White;
  height:120px;
  width:190px;
  text-align:center;
}
#container .celldiv img 
{   
 max-width:144;
 max-height:91; 
} 

#container p 
{
    line-height:20px;
    margin-top:5px;
    margin-left:5px;
    text-align:left;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div style="min-height:600px;width:1113px;height:auto" id="container">            
      <%--      <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>            
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>            
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>
            <div class="cell">
            <div class="celldiv"><img src="/Framework/Image/file.jpg" /></div>
            <p>任务1任务1任务1任务1任务1任务1任务1任务1任务1<Br />
            步骤1<Br />
            总数：1000</p></div>--%>
            
            
             </div>

</asp:Content>