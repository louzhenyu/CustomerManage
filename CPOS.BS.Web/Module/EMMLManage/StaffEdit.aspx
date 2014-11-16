<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Module/EMMLManage/Qixin.Master" CodeBehind="StaffEdit.aspx.cs" Inherits="JIT.CPOS.BS.Web.Module.EMMLManage.StaffEdit" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="../styles/css/qixin/private.css" rel="stylesheet" />
    <section id="section" data-js="js/StaffEdit.js">
        <div class="commonTitleBox">
            <a href="javascript:history.go(-1);" class="getBack">返回</a>
            <h2 class="commonTitle">添加资讯</h2>
        </div>
        <div class="mainContent">
            <div class="addInfoArea">
                <form action="AddNewsInfo" name="frm" id="frm" method="post">
                    <div class="item-line">
                        <div class="commonSelectWrap classInput">
                            <em class="tit"><span class="fontRed">*</span> 分类</em>
                            <div id="newsTypeSelect" class="selectBox">
                            </div>
                        </div>
                        <div class="commonSelectWrap authorInput">
                            <em class="tit">作者</em>
                            <p class="searchInput">
                                <input id="Author" name="Author" type="text" value="" />
                            </p>
                        </div>
                    </div>
                    <div class="item-line">
                        <div class="commonSelectWrap">
                            <em class="tit"><span class="fontRed">*</span> 标题</em>
                            <p class="searchInput  w-596">
                                <input id="NewsTitle" name="NewsTitle" type="text" value="" />
                            </p>
                        </div>
                    </div>
                    <div class="item-line desc">
                        <div class="commonSelectWrap">
                            <em class="tit">描述</em>
                            <p class="searchInput w-596">
                                <input id="Text1" name="Author" type="text" value="" />
                            </p>
                        </div>
                    </div>
                    <div class="item-line theme richtext">
                        <div class="commonSelectWrap">
                            <em class="tit"><span class="fontRed">*</span> 内容</em>
                            <p class="searchInput w-596">
                                 <input id="Text2" name="Author" type="text" value="" />
                            </p>
                        </div>
                    </div>
                    <div class="item-line thumb">
                    </div>
                    <div class="item-line">
                        <div class="commonSelectWrap selectDateBox">
                            <span class="tit">发布时间</span>
                            <p>
                                <input id="PublishTime" name="PublishTime" type="text" value="" />
                            </p>
                        </div>
                    </div>
                    <div class="btnWrap">
                        <a id="saveBtn" href="javascript:;" class="commonBtn">保存</a> <a id="cancelBtn" href="javascript:;"
                            class="commonBtn cancelBtn">取消</a>
                    </div>
                </form>
            </div>
        </div>
    </section>
</asp:Content>
