<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/optimize.Master"
    AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <title>超级分销首页</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=StaticUrl+"/module/superRetailStore/css/firstPage.css?v=0.3"%>" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="allPage" id="section" data-js="js/firstPage.js">
        <!--内容界面-->
        <div class="firstpage">
            <!--图表-->
            <div class="col" style="display:none">
                <div class="floatleft rowTwo">
                    <!--总销量图表-->
                    <div class="marginCommon borderGrey">
                        <div class="chartTitle colorwhite bggreen">
                            <div class="floatleft rowTwo">
                                <div class="paddigcommon borRightyellow">
                                    <div class="chartTitleData">
                                        <span>总销量</span>
                                        <span class="font20">0</span>
                                        <span>元</span>
                                    </div>
                                </div>
                            </div>
                            <div class="floatleft rowTwo">
                                <div class="paddigcommon">
                                    <div class="chartTitleData ">
                                        <span class="">近7天新增销售额</span>
                                        <span class="font20">0</span>
                                        <span class="">元</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="chartBase" id="allSale"></div>
                    </div>
                </div>
                <div class="floatleft rowTwo">
                    <!--分销商图表-->
                    <div class="borderGrey">
                        <div class="chartTitle colorwhite bgred">
                            <div class="floatleft rowTwo">
                                <div class="paddigcommon borRightyellow">
                                    <div class="chartTitleData">
                                        <span>分销商总数</span>
                                        <span class="font20">0</span>
                                        <span>人</span>
                                    </div>
                                </div>
                            </div>
                            <div class="floatleft rowTwo">
                                <div class="paddigcommon">
                                    <div class="chartTitleData ">
                                        <span class="">近7天新增分销商</span>
                                        <span class="font20">0</span>
                                        <span class="">人</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="chartBase" id="allSales"></div>
                    </div>
                </div>
            </div>
            <!--方块数据-->
            <div class="col" style="display:none">
                <div class="floatleft rowThree">
                    <div class="marginCommon borderGrey">
                        <!--分销商品订单-->
                        <div class="squreBase">
                            <div class="allTitle colorgrey font16 bgGrey">
                                <div class="floatleft rowTwo" id="l"><span>分销商品订单</span></div>
                                <div class="floatleft rowTwo textRight font10" >
                                    <a href="javascript:;" class="checkDay colorLitblue">近7日</a>
                                    <a href="javascript:;" class="checkDay colorgrey">近30日</a>
                                </div>
                            </div>
                            <div class="currentData">
                                <div class="floatleft rowTwo">
                                    <div class="paddigcommon borRight">
                                        <div><span>总单量</span></div>
                                        <div class="dataDiv">
                                            <span class="font20 colorred">0</span>
                                            <span class="colorred">单</span>
                                            <img src="" />
                                            <span class="font14"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="floatleft rowTwo">
                                    <div class="paddigcommon">
                                        <div><span>人均销量</span></div>
                                        <div class="dataDiv">
                                            <span class="font20 colorred">0</span>
                                            <span class="colorred">元</span>
                                            <img src="" />
                                            <span class="font14"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="floatleft rowThree">
                    <div class="marginCommon borderGrey">
                        <div class="squreBase">
                             <!--活跃分销商-->
                            <div class="allTitle colorgrey font16 bgGrey">
                                <div class="floatleft rowTwo"><span>活跃分销商</span></div>
                                <div class="floatleft rowTwo textRight font10" >
                                    <a href="javascript:;" class="checkDay colorLitblue">近7日</a>
                                    <a href="javascript:;" class="checkDay colorgrey">近30日</a>
                                </div>
                            </div>
                            <div class="currentData">
                                <div class=" rowTwo" style="width:100%">
                                    <div class="paddigcommon">
                                        <div><span>分销商人数</span></div>
                                        <div class="dataDiv">
                                            <span class="font20 colorred">0</span>
                                            <span class="colorred">人</span>
                                            <img src="" />
                                            <span class="font14"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="floatleft rowThree">
                    <div class="borderGrey">
                        <div class="squreBase">
                             <!--分销拓展-->
                            <div class="allTitle colorgrey font16 bgGrey">
                                <div class="floatleft rowTwo"><span>分销拓展</span></div>
                                <div class="floatleft rowTwo textRight font10" >
                                    <a href="javascript:;" class="checkDay colorLitblue">近7日</a>
                                    <a href="javascript:;" class="checkDay colorgrey">近30日</a>
                                </div>
                            </div>
                            <div class="currentData">
                                <div class="floatleft rowTwo">
                                    <div class="paddigcommon borRight">
                                        <div><span>分享次数</span></div>
                                        <div class="dataDiv">
                                            <span class="font20 colorred">0</span>
                                            <span class="colorred">次</span>
                                            <img src="" />
                                            <span class="font14"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="floatleft rowTwo">
                                    <div class="paddigcommon">
                                        <div><span>新增分销商</span></div>
                                        <div class="dataDiv">
                                            <span class="font20 colorred">0</span>
                                            <span class="colorred">人</span>
                                            <img src="" />
                                            <span class="font14"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--无数据界面-->
            <div class="missDatecol" style="display:none">
                <div class="missDataDiv">
                    <img src="images/icon5.png" class="missDataImg"/>
                    <p>您还没有建立分销体系，您可以<em>↓</em></p>
                    <div class="missDataBtn">
                        <a href="/module/superRetailStore/tLevProfits.aspx" class="bgwhite colorblue commonBtn  borderBlue" >一键分销</a>
                    </div>
                </div>
            </div>
                <!--链接-->
            <div class="col">
                <div class="floatleft rowThree">
                    <div class="marginCommon borderGrey bgGrey">
                        <div class="linkBase">
                            <div class="font20 linkTitleDiv">
                                <div class="steps bgblue colorwhite floatleft">1</div>
                                <div class="floatleft linkTitle">分销</div>
                            </div>
                            <div class="bgyellow iconimgs" >
                                <img src="images/icon2.png" class="imgs" />
                            </div>
                            <div class="text">
                                <div>
                                    <p>选品后，一键生成智能分品体系。</p>
                                </div>
                                <div class="line2">
                                    <p>我们提供行业内公认最具拓展性的分销体系算法，为您快速拓展分销生态系统提供支持!</p>
                                </div>
                                <div class="linkBtn">
                                     <a href="/module/superRetailStore/tLevProfits.aspx" class="bgwhite colorblue commonBtn  borderBlue" id="oneHrefBtn">管理分销</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="floatleft rowThree">
                    <div class="marginCommon borderGrey bgGrey">
                        <div class="linkBase">
                            <div class="font20 linkTitleDiv">
                                <div class="steps bgblue colorwhite floatleft">2</div>
                                <div class="floatleft linkTitle">选品</div>
                            </div>
                            <div class="bgorange iconimgs" >
                                <img src="images/icon3.png" class="imgs" />
                            </div>
                            <div class="text">
                                <div>
                                    <p>为分销商添加可分销的商品。</p>
                                </div>
                                <div class="line2">
                                    <p>选择具有一定利润空间的商品，将为分销商提供更多提成空间，调动分销积极性!</p>
                                </div>
                                <div class="line2">
                                    <p>选品前，请确保您了解商品成本，以便核算出适当的提成比例。</p>
                                </div>
                                <div class="linkBtn">
                                     <a href="/module/superRetailStore/querylist.aspx" class="bgwhite colorblue commonBtn borderBlue " id="twoHrefBtn">管理商品</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="floatleft rowThree">
                    <div class="borderGrey bgGrey">
                        <div class="linkBase">
                            <div class="font20 linkTitleDiv">
                                <div class="steps bgblue colorwhite floatleft">3</div>
                                <div class="floatleft linkTitle">拓展</div>
                            </div>
                            <div class="bgred iconimgs" >
                                <img src="images/icon1.png" class="imgs" />
                            </div>
                            <div class="text">
                                <div>
                                    <p>支持分销商的事业拓展，为分销商提供拓展工具。</p>
                                </div>
                                <div class="line2">
                                    <p>使用拓展工具，经销商将能够分享更丰富的内容，并与潜在下线客户进行有趣的互动沟通!</p>
                                </div>
                                <div class="linkBtn">
                                    <a href="/module/SuperDistributionExpand/expandActivity.aspx" class="bgwhite colorblue commonBtn borderBlue" id="threeHrefBtn">管理拓展工具</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
