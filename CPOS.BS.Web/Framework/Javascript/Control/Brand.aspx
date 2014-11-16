<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Brand.aspx.cs" Inherits="JIT.CPOS.BS.Web.Framework.Javascript.Brand" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/framework/css/reset.css" rel="stylesheet" type="text/css" />
    <link href="/framework/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/framework/css/webcontrol.css" rel="stylesheet" type="text/css" />
    <!-- javaScript -->
    <script type="text/javascript" src="/framework/javascript/Other/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/framework/javascript/Other/menu.js"></script>
    <!--[if IE 6]>
<script type="text/javascript" src="js/png.js"></script>
<script type="text/javascript">
	DD_belatedPNG.fix("*");
</script>
<![endif]-->
    <link href="/Lib/Javascript/Ext4.1.0/Resources/css/ext-all-gray.css" rel="stylesheet"
        type="text/css" />
    <link href="/Lib/Css/jit-all.css" rel="stylesheet" type="text/css" />
    <script src="/Lib/Javascript/Ext4.1.0/ext-all.js" type="text/javascript"></script>
    <script src="/Lib/Javascript/Jit/jit-all-dev.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Utility/CommonMethod.js" type="text/javascript"></script>
    <script src="/Framework/Javascript/Utility/JITPage.js" type="text/javascript"></script>
    <title>Brand控件测试</title>
    <script src="/Framework/javascript/Biz/Brand.js" type="text/javascript"></script>
       <style type="text/css">
        dt
        {
           color:#333333; 
           font-size:18px;
           font-family:宋体;
           font-weight:bolder;
           margin:10px;
        }
        .sample
        {
        	border-style:dashed;
        	border-width:1px;
        	margin:20px;
        	padding:20px;
        	width:800px;
        }
    </style>
</head>
<body>
  <div class="sample">
        <dl>
            <dt>说明</dt><dd>业务：品牌下拉树控件  对应数据表Brand ,xtype:'jitbizbrand', 对应js  “/Framework/javascript/Biz/Brand.js” 注：查看本示例，请先登录</dd>
            <dt>示例</dt>
            <dd>
                <div id="dvBiz1"></div>
                <div id="dvBiz2"></div>
            </dd>
            <dt>代码</dt>
            <dd>
                <pre>
            var initValues = new Array();
            initValues.push({ id: '3', text: '' });
            initValues.push({ id: '2', text: '' });
            var Channel1 = Ext.create("Jit.Biz.Channel", {
                id: 'cmbMultiSelect1'
                , fieldLabel: '渠道信息'
                , renderTo: 'dvBiz1'
                , margin: '10'
                , multiSelect: true                 //树是否为多选
                , rootText: '渠道'                  //树的根节点的文本
                , rootID: '-1'                      //树的根节点的值
                , isSelectLeafOnly: true             //只能选择叶子节点
                , initSelectedItems: initValues     //初始选中的项,该参数为一个数组，数组中的每个元素都包含id和text属性
            });
              主要说明：根据登录用户，自动选择该用户的所属客户，选择出渠道信息
               
                </pre>
            </dd>
        </dl>
    </div>   
    <script language="javascript" type="text/javascript">
        Ext.onReady(function () {
            var Channel1 = Ext.create("Jit.Biz.Brand", {
                id: 'cmbMultiSelect2'
                , fieldLabel: '品牌'         
                , renderTo: 'dvBiz1'
                , margin: '10'
            });
         });
    </script>
</body>
</html>
