<%@ Page Title="" Language="C#" MasterPageFile="~/Framework/MasterPage/light.Master"
ValidateRequest="false" AutoEventWireup="true" Inherits="JIT.CPOS.BS.Web.PageBase.JITChildPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>帮助中心</title>
    <link rel="stylesheet" href="<%=StaticUrl+"/Module/helpCenter/css/style.css?v1.2"%>" />
    <style type="text/css">
       
        #contentArea {
        background-color:#fff;
        
        }

        .Tips {
            float: right;
            background-color: red;
            border-radius: 4px;
            width: 45px;
            font-size: 15px;
            line-height: 21px;
            color: #fff;
            text-align: center;
        }

        .classalert {
            height: 30px;
            display: inline-block;
            float: right;
        }

        .key {
            float: right;
            font-size: 15px;
            line-height: 21px;
            color: #f00;
            margin-left: 10px;
            font-weight: 700;
        }

        #contentArea {
            min-height: 500px;
            height: auto;
        }

        .class {
            padding: 10px 20px;
            height: 35px;
        }

        
        
        .moduletitle {
          display: inline-block;
          background-color: #1074bd;
          padding: 10px;
          font-size:14px;
          border-top-left-radius: 5px;
          border-top-right-radius: 5px;
          color: #fff;
        }
        .modulecontent {
            padding:10px;
        
            background-color:#f6f6f6;
        }

        .helpmodule {
          margin-top: 33px;
        
        }

        .helpmodule:nth-of-type(1) .moduletitle {
            background-color: #58bcc6;
        }

        .helpmodule:nth-of-type(2) .moduletitle {
            background-color: #e73a36;
        }

        .helpmodule:nth-of-type(3) .moduletitle {
            background-color: #eb6313;
        }

        .helpmodule:nth-of-type(4) .moduletitle {
            background-color: #71ba36;
        }

        .helpmodule:nth-of-type(5) .moduletitle {
            background-color: #00a0e8;
        }

        .helpmodule:nth-of-type(6) .moduletitle {
            background-color: #f8b62c;
        }

        .helpmodule:nth-of-type(7) .moduletitle {
            background-color: #1074bd;
        }

      

        .classify {
            float: left;
            margin-right: 5px;
            font-size: 13px;
            width: 80px;
            height: 18px;
        }

        .options {
            float: left;
        }

        .options span {
            width: 110px;
            display: inline-block;
            font-size: 18px;
            color: #22d3d3;
            margin: 0 10px;
        }

        .options span a {
            font-size: 13px;
            color: #307fc1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="helpmodule">
        <div class="moduletitle">云店O2O</div> <div class="classalert"><span class="key">按Ctrl+F搜索问题关键字</span><span class="Tips">Tips</span></div>

        <div class="modulecontent">
        <div class="class">
            <div class="classify">商城</div>
            <div class="options"><span><a target="_blank" href="#">配送方</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=776">配送方式</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=774">支付方式</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=772">商城装修</a></span></div>
        </div>
        <div class="class">
            <div class="classify">商品</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=761">商品列表</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=763">商品发布</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=765">商品品类设置</a></span><%--<span><a target="_blank" href="http://help.chainclouds.cn/?p=769">商品规格</a></span>--%><span><a target="_blank" href="http://help.chainclouds.cn/?p=767">商品营销分组</a></span></div>
        </div>
        <div class="class">
            <div class="classify">订单</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=836">订单管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=840">退换货管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=843">退款管理</a></span></div>
        </div>
        </div>
    </div>
   
     <div class="helpmodule">
        <div class="moduletitle">营销利器</div> 

        <div class="modulecontent">
             <div class="class">
            <div class="classify">创意仓库</div>
            <div class="options"><span><a target="_blank" href="#">发起活动</a></span><span><a target="_blank" href="#">我的活动</a></span></div>
        </div>
               <div class="class">
            <div class="classify">商城活动</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=812">抢购/秒杀</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=807">团购</a></span><span><a target="_blank" href="#">热销</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=767">商品营销分组</a></span></div>
        </div>
             <div class="class">
            <div class="classify">优惠券管理</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=1141">优惠券设置</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=814">优惠券查询</a></span></div>
        </div>
            <div class="class">
            <div class="classify">营销工具</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=822">生日关怀</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=1217">节日关怀</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=931">花样问卷</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=820">触点活动</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=818">互动游戏</a></span></div>
        </div>
          </div>
    </div>

     <div class="helpmodule">
        <div class="moduletitle">会员金矿</div> 

        <div class="modulecontent">
             <div class="class">
            <div class="classify">会员卡</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=874">卡类型管理</a></span><span><a target="_blank" href="#">制卡</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=719">假日设置</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=888">开卡</a></span></div>
        </div>
             <div class="class">
            <div class="classify">忠诚度</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=717">积分/返现设置</a></span></div>
        </div>
             <div class="class">
            <div class="classify">大数据</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=730">会员数据</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=727">标签设置</a></span><span><a target="_blank" href="#">会员分组</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=1217">节日关怀</a></span></div>
        </div>
          </div>
    </div>

     <div class="helpmodule">
        <div class="moduletitle">店长合伙</div> 

        <div class="modulecontent">

          </div>
    </div>

     <div class="helpmodule">
        <div class="moduletitle">员工激励</div> 

        <div class="modulecontent">
             <div class="class">
            <div class="classify">员工激励</div>
            <div class="options"><span><a target="_blank" href="#">集客奖励</a></span><span><a target="_blank" href="#">员工提现</a></span></div>
        </div>
          </div>
    </div>

     <div class="helpmodule">
        <div class="moduletitle">渠道创新</div> 

        <div class="modulecontent">
             <div class="class">
            <div class="classify">员工</div>
            <div class="options"><span><a target="_blank" href="#">员工小店</a></span><span><a target="_blank" href="#">员工提现</a></span></div>
        </div>
             <div class="class">
            <div class="classify">会员</div>
            <div class="options"><span><a target="_blank" href="#">会员小店设置</a></span><span><a target="_blank" href="#">员工提现管理</a></span></div>
        </div>
             <div class="class">
            <div class="classify">外部渠道</div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=740">分销奖励模板</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=743">分销商管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=745">分销商奖励</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=747">分销商提现</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=749">销售员奖励</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=751">销售员提现</a></span></div>
        </div>
          </div>
    </div>

     <div class="helpmodule">
        <div class="moduletitle">经营数据</div> 

        <div class="modulecontent">

          </div>
    </div>

    <div class="helpmodule">
        <div class="moduletitle">设置</div> 

        <div class="modulecontent">
             <div class="class">
            <div class="classify"></div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=1089">商户信息</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=753">组织架构</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=755">门店管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=757">角色设置</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=759">员工管理</a></span></div>
        </div>
          </div>
    </div>

    <div class="helpmodule">
        <div class="moduletitle">微信</div> 

        <div class="modulecontent">
             <div class="class">
            <div class="classify"></div>
            <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=785">微信公众号授权</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=787">微信菜单管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=789">微信关注回复</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=794">微信关键字回复</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=792">微信图文素材</a></span><span><a target="_blank" href="#">微官网</a></span></div>
        </div>
          </div>
    </div>

   <%-- <div class="class">
        <div class="classify">会员</div>
        <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=730">会员管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=888">开卡</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=874">卡类型管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=727">标签管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=717">积分返现</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=719">假日设置</a></span></div>
    </div>
    <div class="class">
        <div class="classify">云店</div>
        <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=772"">云店装修</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=774">支付方式</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=776">配送方式</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=807">团购</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=812">抢购/秒杀</a></span></div>
    </div>
    <div class="class">
        <div class="classify">营销</div>
        <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=814">优惠券查询</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=818">互动游戏</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=820">触点活动</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=822">生日营销</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=824">营销活动</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=826">消息模板</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=828">社会化销售</a></span></div>
    </div>
    <div class="class">
        <div class="classify">微信</div>
        <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=785">微信公众号授权</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=787">微信菜单管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=789">微信关注回复</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=794">微信关键字回复</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=792">微信图文素材</a></span></div>
    </div>
    <div class="class">
        <div class="classify">设置</div>
        <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=753">组织结构</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=755">门店</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=757">角色</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=759">员工</a></span></div>
    </div>
    <div class="class">
        <div class="classify">异业合作</div>
        <div class="options"><span><a target="_blank" href="http://help.chainclouds.cn/?p=740">奖励模板</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=743">分销商管理</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=745">分销商奖励</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=747">分销商提现</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=749">销售员奖励</a></span><span><a target="_blank" href="http://help.chainclouds.cn/?p=751">销售员提现</a></span></div>
    </div>--%>
    <SCRIPT type=text/javascript>
        $(function () {
            $(".help img").attr("src", "/Framework/Image/leftImgList/helpon.png")
        });
</SCRIPT>
</asp:Content>