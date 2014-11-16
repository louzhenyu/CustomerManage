<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" 
    Inherits="JIT.CPOS.BS.Web.Module.MapAnalysis.Common.Map" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="LayoutResource/css/tt.css" />
    <link rel="stylesheet" type="text/css" href="./LayoutResource/js/fancybox/jquery.fancybox-1.3.4.css"
        media="screen" />
    <link rel="stylesheet" href="LayoutResource/css/superfish.css" media="screen" />
    <%--标准样式--%>
<link href="/framework/css/reset.css" rel="stylesheet" type="text/css" />
<link href="/framework/css/style.css" rel="stylesheet" type="text/css" />
<link href="/framework/css/webcontrol.css" rel="stylesheet" type="text/css" />
<!-- javaScript -->
<script type="text/javascript" src="/framework/javascript/Other/jquery-1.9.0.min.js"></script>

<script type="text/javascript" src="/framework/javascript/Other/menu.js"></script>
<!--[if IE 6]>
<script type="text/javascript" src="js/png.js"></script>
<script type="text/javascript">
	DD_belatedPNG.fix("*");
</script>
<![endif]-->
<link href="/Lib/Javascript/Ext4.1.0/Resources/css/ext-all-gray.css" rel="stylesheet" type="text/css" />
<link href="/Lib/Css/jit-all.css" rel="stylesheet" type="text/css" />
<script src="/Lib/Javascript/Ext4.1.0/ext-all-dev.js" type="text/javascript"></script>
<script src="/Lib/Javascript/Ext4.1.0/locale/ext-lang-zh_CN.js" type="text/javascript"></script>  
<script src="/Lib/Javascript/Jit/jit-all-dev.js" type="text/javascript"></script>
<script src="/Framework/Javascript/Utility/CommonMethod.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Button.js" type="text/javascript"></script>
<script src="/Framework/Javascript/Utility/CommonValidate.js" type="text/javascript"></script>
<script src="/Framework/Javascript/Utility/JITPage.js" type="text/javascript"></script>
<script src="/Framework/Javascript/pub/JTIPagePannel.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Unit.js" type="text/javascript"></script>
<script src="/Framework/javascript/Biz/Warehouse.js" type="text/javascript"></script>

    <!--// Begin:日期控件样式表-->
    <!--// End:日期控件样式表-->
    <script type="text/javascript" src="./LayoutResource/js/plugin/jquery-1.4.3.min.js"></script>
    <script type="text/javascript" src="./LayoutResource/js/fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
    <script type="text/javascript" src="./LayoutResource/js/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <script src="./LayoutResource/js/superfish/hoverIntent.js" type="text/javascript"></script>
    <script src="./LayoutResource/js/superfish/superfish.js" type="text/javascript"></script>
    <%--    <!--// Begin:日期控件js文件-->
    <script type="text/javascript" src="./LayoutResource/js/date/jquery.date_input.js"></script>
    <script type="text/javascript" src="./LayoutResource/js/date/translations/jquery.date_input.zh_CN.js"></script>
    <!--// End:日期控件js文件-->--%>
    <!--// Begin:日期控件js文件-->
    <script language="javascript" type="text/javascript" src="./LayoutResource/js/My97DatePicker/my97datepicker/WdatePicker.js"></script>
    <!--// End:日期控件js文件-->
    <!--// Begin:日期控件绑定到<input class="date_input"> -->
    <%--    <script type="text/javascript">    $($.date_input.initialize);</script>--%>
    <!--// End:日期控件绑定到dom元素-->
    <script type="text/javascript">
        var __mid = 'cb3a8bca-05e9-4dd7-980e-0bcef534d9b3';
        $(document).ready(function () {
        
            //   var AllTip =true,false,true
            $("a#btnExport").fancybox({ 'showCloseButton': false, 'padding': 0,
                onComplete: function () {
                    if (_queryLayer == null || _kpiCode == null) {
                        $("#rdgExportData,#rdgExportMapAndData").attr("disabled", "disabled").css("opacity", 0.3);
                    } else {
                        $("#rdgExportData,#rdgExportMapAndData").removeAttr("disabled").css("opacity", 1);
                    }
                }
            });

            // 弹出“导入"层
            $("a#btnImport").fancybox({
                'showCloseButton': false,
                'padding': 0
            });

            // 弹出“筛选"层
            $("a#btnFilter").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    //                         if ($("#IsSelectPop").val() == "0")
                    if (_queryLayer == null || _kpiCode == null) {
                        alert("请先选择KPI!");
                        return false;
                    }
                },
                onComplete: function () {
                    TipIsClickFn("1");

                },
                onClosed: function () {
                    exitKPIFilter();
                }

            });

            //弹出 “样式切换” 层
            $("a#menuChangeStyle").fancybox({ 'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    //                         if ($("#IsSelectPop").val() == "0")
                    if (_queryLayer == null || _kpiCode == null) {
                        alert("请先选择KPI!");
                        return false;
                    }
                },
                onClosed: function () {
                    exitChangeStyle();
                }
            });


            // 切换至“筛选>时间标签"
            $("a#btnFilterTime").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnFilterTime").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }
                },

                onComplete: function () {
                    TipIsClickFn();

                },
                onClosed: function () {
                    exitKPIFilter();
                }
            });

            // 切换至“筛选:产品>时间标签"
            $("a#btnFilterTime2").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnFilterTime2").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }
                },

                onComplete: function () {
                    TipIsClickFn();

                },
                onClosed: function () {
                    exitKPIFilter();
                }
            });

            // 切换至“筛选>终端标签"
            $("a#btnFilterTerminal").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnFilterTerminal").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }

                },

                onComplete: function () {
                    TipIsClickFn();

                },
                onClosed: function () {
                    exitKPIFilter();
                }
            });

            // 切换至“筛选:产品>终端标签"
            $("a#btnFilterTerminal2").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnFilterTerminal2").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }
                },

                onComplete: function () {
                    TipIsClickFn();

                },
                onClosed: function () {
                    exitKPIFilter();
                }
            });

            // 切换至“筛选>产品标签"
            $("a#btnFilterProduct").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnFilterProduct").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }
                },

                onComplete: function () {
                    TipIsClickFn();

                },
                onClosed: function () {
                    exitKPIFilter();
                }
            });

            // 切换至“筛选:终端>产品标签"
            $("a#btnFilterProduct2").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnFilterProduct2").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }
                },

                onComplete: function () {
                    TipIsClickFn();

                },
                onClosed: function () {
                    exitKPIFilter();
                }
            });

            // 弹出“终端"层
            $("a#btnTerminal").fancybox({
                'showCloseButton': false,
                'padding': 0,
                onStart: function () {
                    var getISClick = $("a#btnTerminal").attr("isclick");

                    if (getISClick == "0") {
                        return false;
                    }
                },

                onComplete: function () {
                    TipIsClickFn();

                }
            });

            // 关闭弹出层
            $("a.winCaption_btnClose").die().live('click', function () {
                $.fancybox.close();
                $(".winTabs>li").find("a").attr("isclick", "1");
            })

            // 关闭弹出层
            $("a.cancel").die().live('click', function () {
                $.fancybox.close();
            })

            var example = $('#example').superfish({
            //add options here if required
        });

        /*$("#toolbar_items > li.level1 > a[class^=navItems]").hover(function (i) {
        console.log($(this).index());
        })*/

        $("#toolbar_items > li.level1 > a[class^=navItems]").each(function (i) {
            var index = i;
            // if (i > 0) return false;
            $(this).hover(function () {
                $(this).addClass('navSelected current' + i);
                if (i == 1) {
                    tryShowFilterResult();
                }
            }, function () {
                /*$(this).removeClass('navSelected current' + i);*/
                if (i == 1) {
                    closeFilterResultWindow();
                }
            })
        })

        $("#toolbar_items > li.level1").each(function (i) {
            $(this).hover(function () {
            }, function () {
                $(this).find("a.navItems" + i).removeClass('navSelected current' + i);
            })
        })


    })

    // “筛选“标签切换弹出层

    // 点击POi之后出现的内容页


    function TipISClick(date, Terminal, product) {
        var ForArr = [['date', 'Terminal', 'product'], [date, Terminal, product]];

        for (var i = 0; i < ForArr[1].length; i++) {
            if (ForArr[1][i] == false) {

                $("#fancybox-wrap .winTabs>li").eq(i).css("opacity", 0.3);
                $("#fancybox-wrap .winTabs>li").eq(i).find("a").attr("isclick", "0");
            }
        }

    }
    function TipIsClickFn(isfirst) {
        $("#grayWinContent").hide();
        var getTipValue = $("#TipIsClickValue").val();
        var getTipValueArr = getTipValue.split(",");
        $("#fancybox-wrap .winTabs>li").css({ "opacity": 1 })

        for (var i = 0; i < getTipValueArr.length; i++) {
            if (getTipValueArr[i] == "0") {
                if (isfirst == "1" && i == 0) {
                    $("#grayWinContent").css({ "opacity": 0.5, "background": "#000", "z-index": "999" }).show();
                }
                $("#fancybox-wrap .winTabs>li").eq(i).css({ "opacity": 0.3 });
                $("#fancybox-wrap .winTabs>li").eq(i).find("a").attr({ "isclick": "0", "cursor": "default" });
            }
        }


    }

    </script>
    <style type="text/css">
        .sf-menu ul ul
        {
            zoom: 1;
        }
        .sf-menu ul ul li
        {
            zoom: 1;
        }
        
        .icn_skin1
        {
            background-image: url(LayoutResource/images/clud_icon.png);
            background-repeat: no-repeat;
        }
        .SSconnextLi
        {
            border-bottom: 1px dashed #d8d8d8;
            min-height: 40px;
            position: relative;
        }
        .SSdate
        {
            position: absolute;
            left: 0;
            top: 10px;
            width: 61px;
            height: 20px;
            background: url(LayoutResource/images/c3.png) no-repeat left top;
        }
        .SSconnextLiTxt
        {
            margin-left: 90px;
            font-size: 16px;
            color: #9f9f9f;
            line-height: 20px;
            padding: 10px 0;
            font-weight: bold;
        }
        .SSChannel
        {
            position: absolute;
            left: 0;
            top: 10px;
            width: 61px;
            height: 20px;
            background: url(LayoutResource/images/c1.png) no-repeat left top;
        }
        .SSProduct
        {
            position: absolute;
            left: 0;
            top: 10px;
            width: 61px;
            height: 20px;
            background: url(LayoutResource/images/c2.png) no-repeat left top;
        }
        #toolbar_items li.level1 a.navItemsSelect1
        {
            background-position: 10px -30px;
        }
        #toolbar_items li.level1 a.navItemsSelect
        {
            background-position: 10px -310px;
        }
    </style>
    
    <script language="javascript" type="text/javascript">
        /*清空事件*/
        function fnClearFilters() {
            document.getElementById("rdoWeek").checked = true;
            document.getElementById("dtStartDate").value = "";
            document.getElementById("dtEndDate").value = "";
            document.getElementById("drpPeriod").value = "1";
            document.getElementById("dtStartDate").disabled = true;
            document.getElementById("dtEndDate").disabled = true;
            document.getElementById("drpPeriod").disabled = false;
            document.getElementById("<%=drpChain.ClientID %>").value = 0;
            document.getElementById("txt_CategoryName").value = "品类";
            document.getElementById("txt_BrandName").value = "品牌";
            document.getElementById("txt_SKUName").value = "产品";
            Ext.getCmp('cmbMultiSelect').jitSetValue(null);
            fnBrandSearch();
            fnCategorySearch();
            fnSKUSearch();
        }

        /*自定义数据周期 时间*/
        function fnCheckedPeriod() {
            var chk_IsCustomizePeriod = document.getElementById("chk_IsCustomizePeriod")
            if (!chk_IsCustomizePeriod.checked) {
                document.getElementById("dtStartDate").value = "";
                document.getElementById("dtEndDate").value = "";
                document.getElementById("drpPeriod").value = "1";
                document.getElementById("dtStartDate").disabled = true;
                document.getElementById("dtEndDate").disabled = true;
                document.getElementById("drpPeriod").disabled = false;
            } else {
                document.getElementById("drpPeriod").value = "";
                document.getElementById("dtStartDate").disabled = false;
                document.getElementById("dtEndDate").disabled = false;
                document.getElementById("drpPeriod").disabled = true;
            }
        }


        function fnOnFocus(type) {
            if (type == 1) {
                var BrandName = document.getElementById("txt_BrandName").value;
                if (BrandName == "品牌") {
                    document.getElementById("txt_BrandName").value = "";
                }
            } else if (type == 2) {
                var CategoryName = document.getElementById("txt_CategoryName").value;
                if (CategoryName == "品类") {
                    document.getElementById("txt_CategoryName").value = "";
                }
            } else if (type == 3) {
                var SKUName = document.getElementById("txt_SKUName").value;
                if (SKUName == "产品") {
                    document.getElementById("txt_SKUName").value = "";
                }
            }
        }

        function fnOnBlur(type) {
            if (type == 1) {
                var BrandName = document.getElementById("txt_BrandName").value;
                if (BrandName == "") {
                    document.getElementById("txt_BrandName").value = "品牌";
                }
            } else if (type == 2) {
                var CategoryName = document.getElementById("txt_CategoryName").value;
                if (CategoryName == "") {
                    document.getElementById("txt_CategoryName").value = "品类";
                }
            } else if (type == 3) {
                var SKUName = document.getElementById("txt_SKUName").value;
                if (SKUName == "") {
                    document.getElementById("txt_SKUName").value = "产品";
                }
            }
        }
        //是否正在复选处理
        var checkboxChecking = false;
        function fnCheckChange(type, id) {
            if (checkboxChecking)
                return;
            checkboxChecking = true;
            var pb = document.getElementById("p_chkBrand_" + id);
            var b = document.getElementById("chkBrand_" + id);
            var allItemCheckboxName = "chkBrandAll";
            var itemTypeName = "chkBrand";
            var itemIdPrefix = "chkBrandAll";
            if (type == 1) {
                pb = document.getElementById("p_chkBrand_" + id);
                b = document.getElementById("chkBrand_" + id);
                allItemCheckboxName = "chkBrandAll";
                itemTypeName = "chkBrand";
                itemIdPrefix = "p_chkBrand_";
            } else if (type == 2) {
                pb = document.getElementById("p_chkCategory_" + id);
                b = document.getElementById("chkCategory_" + id);
                allItemCheckboxName = "chkCategoryAll";
                itemTypeName = "chkCategory";
                itemIdPrefix = "p_chkCategory_";
            } else if (type == 3) {
                pb = document.getElementById("p_chkSKU_" + id);
                b = document.getElementById("chkSKU_" + id);
                allItemCheckboxName = "chkSKUAll";
                itemTypeName = "chkSKU";
                itemIdPrefix = "p_chkSKU_";
            }
            if (b.checked) {
                pb.className = 'pTextSelected';
            } else {
                pb.className = 'pText';
            }

            //处理全选框状态的联动
            var allItemsCheckbox = document.getElementById(allItemCheckboxName);
            var itemCheckBox = document.getElementsByName(itemTypeName);
            if (allItemsCheckbox.checked) {
                //取消全选
                if (!b.checked)
                    allItemsCheckbox.checked = false;
            }
            else {
                //添加全选
                var selectAll = true;
                if (itemCheckBox != null && itemCheckBox.length > 0) {
                    for (var i = 0; i < itemCheckBox.length; i++) {
                        var s = itemCheckBox[i];
                        var pb = document.getElementById(itemIdPrefix + s.value);
                        if (pb.style.display != 'none') {
                            if (!s.checked) {
                                selectAll = false;
                                break;
                            }
                        }
                    }
                }
                if (selectAll) {//全选
                    allItemsCheckbox.checked = true;
                }
            }
            //处理品牌、品类、产品的联动
            if (type == 1) {
                fnSetSkuItemByBrand();
            }
            else if (type == 2) {
                fnSetBrandItemByCategory();
            }


            checkboxChecking = false;
        }


        /*根据品牌联动品类*/
        function fnSetBrandItemByCategory() {

            var itemCategoryCheckBox = document.getElementsByName("chkCategory");
            var itemBrandCheckBox = document.getElementsByName("chkBrand");
            //如果未选择任何品牌，则忽略此条件
            var categorySelectNothing = true;
            for (var i = 0; i < itemCategoryCheckBox.length; i++) {
                if (itemCategoryCheckBox[i].checked) {
                    categorySelectNothing = false;
                    break;
                }
            }
            //如果未选择任何品类，则忽略此条件
            var brandSelectNothing = true;
            for (var i = 0; i < itemBrandCheckBox.length; i++) {
                if (itemBrandCheckBox[i].checked) {
                    brandSelectNothing = false;
                    break;
                }
            }
            if (categorySelectNothing && brandSelectNothing) {
                resetProductFilter();
                return;
            }
            //记住选中状态
            var selectedBrandItem = new Array();
            {//取消选择品牌 删除品类及产品选择(索引从大到小，因为集合itemCategoryCheckBox在删除项后会变动)
                for (var i = itemBrandCheckBox.length - 1; i >= 0; i--) {
                    //判断是否要删除此品类
                    var deleteBrand = true;
                    for (var j = 0; j < itemCategoryCheckBox.length; j++) {
                        if (!categorySelectNothing && !itemCategoryCheckBox[j].checked)//品牌是否选中,如果未选中，则处理下一个品牌
                            continue;
                        for (var k = 0; k < __skus.length; k++) {
                            var cagetoryMatch; //品牌条件是否匹配
                            if (categorySelectNothing) {
                                categoryMatch = true;
                            }
                            else {
                                categoryMatch = (__skus[k].CategoryID == itemCategoryCheckBox[j].value);
                            }
                            var brandMatch; //品类条件是否匹配
                            //                                if (categorySelectNothing) {
                            //                                    categoryMatch = true;
                            //                                }
                            //                                else {
                            brandMatch = (__skus[k].BrandID == itemBrandCheckBox[i].value);
                            //                                }
                            if (categoryMatch && brandMatch) {//品类与品牌的间接关联
                                //只要有任意产品存在当前品类与当前品牌的关联，则不用删除
                                deleteBrand = false;
                                break;
                            }
                        }
                        //只要有任意一个选中的品牌与当前品类相关，则不用删除
                        if (!deleteBrand)
                            break;
                    }
                    if (deleteBrand) {//删除无关联的品类
                        var brandItem = document.getElementById("p_chkBrand_" + itemBrandCheckBox[i].value);
                        brandItem.parentNode.removeChild(brandItem);
                    }
                    else {
                        if (itemBrandCheckBox[i].checked)
                            selectedBrandItem.push(itemBrandCheckBox[i]);
                    }
                }
            }

            //添加品类及产品选择

            var allBrandCheckBox = document.getElementById("chkCategoryAll");
            var brandHtml = document.getElementById("divBrand").innerHTML;
            for (var i = 0; i < __brands.length; i++) {
                var existsBrand = document.getElementById("chkBrand_" + __brands[i].BrandID);
                if (existsBrand != null)
                    continue;
                //判断是否要添加此品类
                var addBrand = false;
                for (var j = 0; j < itemCategoryCheckBox.length; j++) {
                    if (!categorySelectNothing && !itemCategoryCheckBox[j].checked)//品牌是否选中,如果未选中，则处理下一个品牌
                        continue;
                    for (var k = 0; k < __skus.length; k++) {
                        var categoryMatch; //品牌条件是否匹配
                        if (categorySelectNothing) {
                            categoryMatch = true;
                        }
                        else {
                            categoryMatch = (__skus[k].CategoryID == itemCategoryCheckBox[j].value);
                        }
                        var brandMatch; //品类条件是否匹配
                        //                            if (categorySelectNothing) {
                        //                                categoryMatch = true;
                        //                            }
                        //                            else {
                        brandMatch = (__skus[k].BrandID == __brands[i].BrandID);
                        //                            }
                        if (categoryMatch && brandMatch) {//品类与品牌的间接关联
                            //只要有任意产品存在当前品牌与当前品类的关联，则添加
                            addBrand = true;
                            break;
                        }
                    }
                    //只要有任意一个选中的品牌与当前品类相关，则不用删除
                    if (addBrand)
                        break;
                }
                if (addBrand) {//添加新关联的品类
                    var displayName = __brands[i].BrandName;
                    if (displayName.length > 7)
                        displayName = displayName.substring(0, 7);
                    brandHtml += "<p id=\"p_chkBrand_" + __brands[i].BrandID + "\" title=\"" + __brands[i].BrandName + "\"   class=\"pText\"><input id=\"chkBrand_" + __brands[i].BrandID + "\" onclick=\"fnCheckChange(1," + __brands[i].BrandID + ")\" name=\"chkBrand\" type=\"checkbox\" value=\"" + __brands[i].BrandID + "\" /><label for=\"chkBrand_" + __brands[i].BrandID + "\">" + displayName + "</label></p>\r\n";
                }
            }
            document.getElementById("divBrand").innerHTML = brandHtml;
            //记住选中状态
            for (var i = selectedBrandItem.length - 1; i >= 0; i--) {
                document.getElementById(selectedBrandItem[i].id).checked = true;
            }
            fnSetSkuItemByBrand();
        }

        /*根据品类联动产品*/
        function fnSetSkuItemByBrand() {
            var itemCategoryCheckBox = document.getElementsByName("chkCategory");
            var itemBrandCheckBox = document.getElementsByName("chkBrand");
            var itemSKUCheckBox = document.getElementsByName("chkSKU");
            //判断是否未选择品类（如果未选择，则筛选联动时忽略品牌条件）
            var categorySelectNothing = true;
            for (var i = 0; i < itemCategoryCheckBox.length; i++) {
                if (itemCategoryCheckBox[i].checked) {
                    categorySelectNothing = false;
                    break;
                }
            }
            //判断是否未选择品牌（如果未选择，则筛选联动时忽略品牌条件）
            var brandSelectNothing = true;
            for (var i = 0; i < itemBrandCheckBox.length; i++) {
                if (itemBrandCheckBox[i].checked) {
                    brandSelectNothing = false;
                    break;
                }
            }
            //记住选中状态
            var selectedSkuItem = new Array();

            {//删除产品选择(索引从大到小，因为集合在删除项后会变动)
                for (var i = itemSKUCheckBox.length - 1; i >= 0; i--) {
                    //判断是否要删除此产品
                    var deleteSKU = true;

                    for (var j = 0; j < itemCategoryCheckBox.length; j++) {
                        if (!categorySelectNothing && !itemCategoryCheckBox[j].checked)//品牌是否选中,如果未选中，则处理下一个品牌
                            continue;

                        for (var k = 0; k < itemBrandCheckBox.length; k++) {
                            if (!brandSelectNothing && !itemBrandCheckBox[k].checked)//品类是否选中,如果未选中，则处理下一个品类
                                continue;
                            //查找SKU
                            var currentSKUDetail;
                            for (var l = 0; l < __skus.length; l++) {
                                if (__skus[l].SKUID == itemSKUCheckBox[i].value) {
                                    currentSKUDetail = __skus[l];
                                    break;
                                }
                            }
                            if (currentSKUDetail == null)//未找到SKU详情
                                break;
                            var categoryMatch; //品牌条件是否匹配
                            if (categorySelectNothing) {
                                categoryMatch = true;
                            }
                            else {
                                categoryMatch = (currentSKUDetail.CategoryID == itemCategoryCheckBox[j].value);
                            }
                            var brandMatch; //品类条件是否匹配
                            if (brandSelectNothing) {
                                brandMatch = true;
                            }
                            else {
                                brandMatch = (currentSKUDetail.BrandID == itemBrandCheckBox[k].value);
                            }
                            if (categoryMatch && brandMatch) {//产品与品牌、品类的关联
                                //只要有任意产品存在当前品牌与当前品类的关联，则不用删除
                                deleteSKU = false;
                                break;
                            }
                        }
                        //只要有任意一个选中的品牌及品类与当前产品相关，则不用删除
                        if (!deleteSKU)
                            break;
                    }
                    if (deleteSKU) {//删除无关联的产品
                        var skuItem = document.getElementById("p_chkSKU_" + itemSKUCheckBox[i].value);
                        skuItem.parentNode.removeChild(skuItem);
                    }
                    else {
                        if (itemSKUCheckBox[i].checked)
                            selectedSkuItem.push(itemSKUCheckBox[i]);
                    }
                }
            }

            //添加产品选择
            var allSKUCheckBox = document.getElementById("chkSKUAll");
            var skuHtml = document.getElementById("divSKU").innerHTML;
            for (var i = 0; i < __skus.length; i++) {//所有SKU
                var existsSKU = document.getElementById("chkSKU_" + __skus[i].SKUID);
                if (existsSKU != null)//如果当前列表中已显示，则不用添加。
                    continue;
                //判断是否要添加此产品
                var addSKU = false;
                for (var j = 0; j < itemCategoryCheckBox.length; j++) {//当前选中的品牌
                    if (!categorySelectNothing && !itemCategoryCheckBox[j].checked)//品牌是否选中,如果未选中，则处理下一个品牌
                        continue;

                    for (var k = 0; k < itemBrandCheckBox.length; k++) {
                        if (!brandSelectNothing && !itemBrandCheckBox[k].checked)//品类是否选中,如果未选中，则处理下一个品类
                            continue;

                        var categoryMatch; //品牌条件是否匹配
                        if (categorySelectNothing) {
                            categoryMatch = true;
                        }
                        else {
                            categoryMatch = (__skus[i].CategoryID == itemCategoryCheckBox[j].value);
                        }
                        var brandMatch; //品类条件是否匹配
                        if (brandSelectNothing) {
                            brandMatch = true;
                        }
                        else {
                            brandMatch = (__skus[i].BrandID == itemBrandCheckBox[k].value);
                        }
                        if (categoryMatch && brandMatch) {//品牌与品类的间接关联
                            //只要有任意产品存在当前品牌与当前品类的关联，则添加
                            addSKU = true;
                            break;
                        }
                    }
                    //只要有任意一个选中的品牌与当前品类相关，则不用删除
                    if (addSKU)
                        break;
                }
                if (addSKU) {//添加新关联的品类
                    var displayName = __skus[i].SKUName;
                    if (displayName.length > 7)
                        displayName = displayName.substring(0, 7);
                    skuHtml += "<p id=\"p_chkSKU_" + __skus[i].SKUID + "\" title=\"" + __skus[i].SKUName + "\"   class=\"pText\"><input id=\"chkSKU_" + __skus[i].SKUID + "\" onclick=\"fnCheckChange(3," + __skus[i].SKUID + ")\" name=\"chkSKU\" type=\"checkbox\" value=\"" + __skus[i].SKUID + "\" /><label for=\"chkSKU_" + __skus[i].SKUID + "\">" + displayName + "</label></p>\r\n";
                }
            }
            document.getElementById("divSKU").innerHTML = skuHtml;

            //记住选中状态
            for (var i = selectedSkuItem.length - 1; i >= 0; i--) {
                document.getElementById(selectedSkuItem[i].id).checked = true;
            }

        }


        /*品牌查询事件*/
        function fnBrandSearch() {
            document.getElementById("chkBrandAll").checked = false;
            var BrandName = document.getElementById("txt_BrandName").value;
            if (BrandName == "品牌") {
                BrandName = "";
            }
            var brands = document.getElementsByName("chkBrand");
            if (brands != null && brands.length > 0) {
                for (var i = 0; i < brands.length; i++) {
                    var b = brands[i];
                    b.checked = false;
                    var pb = document.getElementById("p_chkBrand_" + b.value);
                    if (BrandName != null && BrandName != "") {
                        if (pb.title.indexOf(BrandName) > -1) {
                            pb.style.display = 'block';
                        }
                        else {
                            pb.style.display = 'none';
                        }
                    } else {
                        pb.style.display = 'block';
                    }
                    pb.className = 'pText';
                }
            }
        }

        //enter查询事件
        function onKeyDown(type) {
            var e = event;
            var keynum = 0;
            if (window.event) // IE
            {
                keynum = e.keyCode
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                keynum = e.which
            }
            if (keynum == 13) {
                if (type == 1) {
                    fnBrandSearch();
                } else if (type == 2) {
                    fnCategorySearch();
                } else if (type == 3) {
                    fnSKUSearch();
                }
            }
        }
        /*品类查询事件*/
        function fnCategorySearch() {
            document.getElementById("chkCategoryAll").checked = false;
            var CategoryName = document.getElementById("txt_CategoryName").value;
            if (CategoryName == "品类") {
                CategoryName = "";
            }
            var brands = document.getElementsByName("chkCategory");
            if (brands != null && brands.length > 0) {
                for (var i = 0; i < brands.length; i++) {
                    var b = brands[i];
                    b.checked = false;
                    var pb = document.getElementById("p_chkCategory_" + b.value);
                    if (CategoryName != null && CategoryName != "") {
                        if (pb.title.indexOf(CategoryName) > -1) {
                            pb.style.display = 'block';
                        } else {
                            pb.style.display = 'none';
                        }
                    } else {
                        pb.style.display = 'block';
                    }
                    pb.className = 'pText';
                }
            }
        }

        /*产品查询事件*/
        function fnSKUSearch() {
            document.getElementById("chkSKUAll").checked = false;
            var SKUName = document.getElementById("txt_SKUName").value;
            if (SKUName == "产品") {
                SKUName = "";
            }
            var brands = document.getElementsByName("chkSKU");

            if (brands != null && brands.length > 0) {
                for (var i = 0; i < brands.length; i++) {
                    var b = brands[i];
                    b.checked = false;
                    var pb = document.getElementById("p_chkSKU_" + b.value);
                    if (SKUName != null && SKUName != "") {
                        if (pb.title.indexOf(SKUName) > -1) {
                            pb.style.display = 'block';
                        } else {
                            pb.style.display = 'none';
                        }
                    } else {
                        pb.style.display = 'block';
                    }
                    pb.className = 'pText';
                }
            }
        }
        /*获取条件的方法*/
        function fnGetFilters() {
            //获取选择的品牌
            var BrandIDs = "";
            var brands = document.getElementsByName("chkBrand");
            if (brands != null && brands.length > 0) {
                for (var i = 0; i < brands.length; i++) {
                    var b = brands[i];
                    if (b.checked) {
                        BrandIDs = BrandIDs + b.value + ",";
                    }
                }
                if (BrandIDs.length > 0) {
                    BrandIDs = BrandIDs.substring(0, (BrandIDs.length - 1));
                }
            }
            //获取选择的品类
            var CategoryIDs = "";
            var categories = document.getElementsByName("chkCategory");
            if (categories != null && categories.length > 0) {
                for (var i = 0; i < categories.length; i++) {
                    var c = categories[i];
                    if (c.checked) {
                        CategoryIDs = CategoryIDs + c.value + ",";
                    }
                }
                if (CategoryIDs.length > 0) {
                    CategoryIDs = CategoryIDs.substring(0, (CategoryIDs.length - 1));
                }
            }

            //获取选择的产品
            var SKUIDs = "";
            var skues = document.getElementsByName("chkSKU");
            if (skues != null && skues.length > 0) {
                for (var i = 0; i < skues.length; i++) {
                    var s = skues[i];
                    if (s.checked) {
                        SKUIDs = SKUIDs + s.value + ",";
                    }
                }
                if (SKUIDs.length > 0) {
                    SKUIDs = SKUIDs.substring(0, (SKUIDs.length - 1));
                }
            }
            //返回的json对象
            var strJson = {
                "DatePeriod": document.getElementById("drpPeriod").value,
                "IsCheckCustomizeDatePeriod": document.getElementById("chk_IsCustomizePeriod").checked,
                "StartDate": document.getElementById("dtStartDate").value,
                "EndDate": document.getElementById("dtEndDate").value,
                "ChannelID": Ext.getCmp('cmbMultiSelect').jitGetValue(),
                "ChainID": document.getElementById("<%=drpChain.ClientID %>").value,
                "BrandIDs": BrandIDs.length > 0 ? BrandIDs.split(",") : null,
                "CategoryIDs": CategoryIDs.length > 0 ? CategoryIDs.split(",") : null,
                "SKUIDs": SKUIDs.length > 0 ? SKUIDs.split(",") : null
            };
            return eval(strJson);
        }
    </script>
    

    <script type="text/javascript">
        var objArray = new Array();
        objArray.push("1");
        objArray.push("1");
        objArray.push("1");
        objArray.push("1");
        objArray.push("1");
        objArray.push("1");
        //设置方法 value="1,2,1,2,0,0"
        function setStyleValue(value) {
            if (value != null && value != null) {
                var valueStr = value.split(",");
                for (var i = 0; i < valueStr.length; i++) {
                    if (i > 5) {
                        break;
                    }
                    objArray[i] = valueStr[i].toString();
                    if (objArray[i] == 2) {
                        document.getElementById("td_" + (i + 1)).innerHTML = "<img src='LayoutResource/images/style/m" + (i + 1).toString() + "2.png' />";
                    } else {
                        if (valueStr[i] == 1) {
                            document.getElementById("td_" + (i + 1)).innerHTML = "<a href='javascript:uptStyleValue(" + i + ")'><img src='LayoutResource/images/style/m" + (i + 1).toString() + "1.png' /></a>";
                        } else {
                            document.getElementById("td_" + (i + 1)).innerHTML = "<img src='LayoutResource/images/style/m" + (i + 1).toString() + "0.png' />";
                        }
                    }
                }
            }
        }
        //获取方法
        function getStyleValue() {
            var value = "";
            if (objArray != null && objArray.length > 0) {
                for (var i = 0; i < objArray.length; i++) {
                    value = value + "," + objArray[i];
                }
                value = value.substring(1);
            }
            return value;
        }
        //修改方法
        function uptStyleValue(n) {
            if (objArray != null && objArray.length == 6) {
                if (n < 6) {
                    for (var i = 0; i < 6; i++) {
                        if (objArray[i] != "0") {
                            objArray[i] = "1";
                            changeStyle(i, objArray[i]);
                        }
                    }
                    objArray[n] = "2";
                } else {
                    objArray[n] = "2";
                }
                changeStyle(n, "2");
            }
        }

        function changeStyle(i, value) {
            if (value == 2) {
                document.getElementById("td_" + (i + 1)).innerHTML = "<img src='LayoutResource/images/style/m" + (i + 1).toString() + "2.png' />";
            } else {
                if (value == 1) {
                    document.getElementById("td_" + (i + 1)).innerHTML = "<a href='javascript:uptStyleValue(" + i + ")'><img src='LayoutResource/images/style/m" + (i + 1).toString() + "1.png' /></a>";
                } else {
                    document.getElementById("td_" + (i + 1)).innerHTML = "<img src='LayoutResource/images/style/m" + (i + 1).toString() + "0.png' />";
                }
            }
        }
        //初始化设置   setStyleValue("1,2,0,2,0");
    </script>
    <script type="text/javascript">
        //    	    GetPoiData(400, 300, { contitle: '中欧移动商店', date: '2013-06-06', imageUrl: '', conlist: [{ "title": "1234", "description": "1233" }, { "title": "1234", "description": "1233"}] })


        $(function () {
            var wid = $(window).width() - 212;
            $("#MapBoxConnext").css("overflow", "hidden");
            $("#MapBoxConnext,#MapIframeId,.cloud_contain,.cloud_map").css({ "width": wid })

            $(window).resize(function () {
                if ($("#fullsreen").attr("rel") == "0") {
                    var wid = $(window).width() - 2;
                } else {
                    var wid = $(window).width() - 212;
                }

                //  $("#MapBoxConnext,.cloud_toolbar").css({ "width": wid })
            })
        })
        function ActionFullSreen(o) {
            var getrel = $(o).attr("rel");
            var getWinw = $(window).width();
            if (getrel == "1") {
                $(".aside").parent("td").hide();
                $("#MapBoxConnext,#MapIframeId,.cloud_contain,.cloud_map").css({ "width": getWinw - 2, "overflow": "hidden" });
                //    $("#MapBoxConnext, .cloud_contain, #MapIframeId").css({ "width": getWinw - 2, "overflow": "hidden" });
                $(".cloud_contain").css("margin-left", "0");

                $(".cloud_toolbar").css({ "width": getWinw - 2 });
                $(o).attr("rel", "0");
                $(o).text("退出全屏");
            } else {
                $(".aside").parent("td").show();
                $("#MapBoxConnext,#MapIframeId,.cloud_contain,.cloud_map").css({ "width": getWinw - 212, "overflow": "hidden" });
                //   $("#MapBoxConnext").css("overflow", "auto");
                var getWinw = $(window).width();
                $(".cloud_contain").css("margin-left", "3px");
                $(".cloud_toolbar").css("width", getWinw - 212);
                $(o).attr("rel", "1")
                $(o).text("全屏");
            }
        }		
    </script>
    <script language="javascript" type="text/javascript" src="/Module/MapAnalysis/Common/Controller/CommonCtl.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" value="1,1,1" id="TipIsClickValue" /><input type="hidden" value="0"
        id="IsSelectPop" />
    <div style="border-left: 1px solid #ccc;" id="MapBoxConnext">
        <div class="cloud_contain">
            <div class="cloud_toolbar" style="position: relative; z-index: 3">
                <ul class="sf-menu" id="toolbar_items">
                    <li class="level1"><a class="navItems0" href="#" style="padding: 0 1.5em 0 3em; background-position: 12px 11px;">
                        KPI</a>
                        <asp:Literal ID="ltMenuKPI" runat="server"></asp:Literal>
                    </li>
                    <li class="level1"><a class="navItems1 navItemsSelect1" id="btnFilter" href="#win_filter_time"
                        style="padding: 0 1.5em 0 3em;">筛选</a> </li>
                    <li class="level1"><a class="navItems2" id="btnExport" href="./LayoutResource/templates/export.html"
                        style="padding: 0 1.5em 0 3em; background-position: 10px -70px;">导出</a> </li>
                    <li class="level1" style="display: none"><a class="navItems3" id="AA" href="javascript:void(0)"
                        style="color: #ccc"><font color='#ccc'>导入</font></a>
                        <%-- <a class="navItems3" id="btnImport" href="./LayoutResource/templates/import_s1.html">导入</a>
                </li>

                <li class="level1" style="display:none">
                 <a class="navItems4" id="A1"  href="javascript:void(0)" style="color:#ccc"> <font color='#ccc'>分析</font></a>
                   <%-- <a class="navItems4" id="btnAnalysis" href="#">分析</a>--%>
                    </li>
                    <li class="level1"><a class="navItems4" id="btnSetting" href="#" style="padding: 0 1.5em 0 3em;
                        background-position: 10px -206px;">设置</a>
                        <ul class='level1-ul' style="padding-top: 5px">
                            <li><a href="#" id="menuIsShowBaseMap" onclick="changeIsShowBaseMap();">隐藏底图</a></li>
                            <li><a href="#" id="menuIsShowValue" onclick="changeIsShowValue();">显示数值</a></li>
                            <li><a href="#" id="menuIsShowText" onclick="changeIsShowText();">隐藏文本</a></li>
                            <li><a href="#" id="menuClearMap" onclick="clearMap();">清空地图</a></li>
                            <%--<li><a  href="javascript:void(0)" onclick="ActionFullSreen(this)" id="fullsreen" rel="1">全屏</a></li>--%>
                            <li><a id="menuChangeStyle" href="#win_filter_style">样式切换</a></li>
                        </ul>
                    </li>
                    <li class="level1"><a class="navItems5" id="fullsreen" onclick="ActionFullSreen(this)"
                        rel="1" href="#" style="padding: 0 1.5em 0 3em; background-position: 10px -255px;">
                        全屏</a> </li>
                </ul>
            </div>
            <div class="cloud_map" style="overflow: hidden; position: relative; z-index: 2;">
                <div style="position: absolute; z-index: 999; right: 20px; top: 20px; width: 405px;
                    display: none;" id="win_filter_result">
                    <div style="position: relative; width: 100%; height: 100%; z-index: 1001; box-shadow: 0px 0px 10px #bcbcbc;
                        border: 1px solid #d8d8d8">
                        <div class="winBoxer" style="background-color: #fff; overflow: hidden">
                            <div class="winCaption" style="height: 35px;">
                                <span style="width: 94px; height: 26px; position: absolute; left: 5px; top: 5px;
                                    background: url(LayoutResource/images/selectresult.png) no-repeat left top;">
                                </span><a href="###" class="winCaption_btnClose" onclick="javascript:closeFilterResultWindow();"
                                    style="top: 5px;">关闭</a>
                            </div>
                            <div class="SSconnext" style="width: 96%; margin-left: auto; margin-right: auto;">
                                <div class="SSconnextLi">
                                    <span class="SSdate"></span>
                                    <div class="SSconnextLiTxt" id="dvFilterResultOfDatePeriod">年
                                    </div>
                                </div>
                                <div class="SSconnextLi">
                                    <span class="SSChannel"></span>
                                    <div class="SSconnextLiTxt" id="dvFilterResultOfChannel">全部
                                    </div>
                                </div>
                                <div class="SSconnextLi" style="border: 0">
                                    <span class="SSProduct"></span>
                                    <div class="SSconnextLiTxt" id="dvFilterResultOfSKU">全部
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <iframe src="/Lib/MapAnalysis/MapAnalysisIndex.htm" id="MapIframeId" frameborder="0"
                    width="1130px" style="overflow: hidden; border: 0" scrolling="no" height="100%">
                </iframe>
            </div>
        </div>
    </div>
    <div>
    </div>
    <%--时间筛选--%>
    <div style="display: none; overflow: hidden">
        <div id="win_filter_time" class="winBoxer" style="width: 495px; height: 385px; overflow: hidden">
            <div class="winCaption">
                <span class="winCaption_icon tFilter"></span><a class="winCaption_btnClose" href="###">
                    关闭</a>
            </div>
            <ul class="winTabs">
                <li class="time selected0"></li>
                <li class="customer"><a href="#win_filter_terminal" id="btnFilterTerminal" isclick="1">
                </a></li>
                <li class="product"><a href="#win_filter_product" id="btnFilterProduct" isclick="1">
                </a></li>
            </ul>
            <div class="winContent contFilter" style="height: 275px; *height: 275px; position: relative;">
                <div style="position: absolute; left: 0; top: 0; height: 275px; *height: 275px; display: none;
                    width: 100%;" id="grayWinContent">
                </div>
                <ul style="height: 215px; *height: 155px; margin-left: 15px; padding-top: 50px;">
                    <li>
                        <input id="rdoWeek" type="radio" name="rdo_time" checked="checked" onclick="fnCheckedPeriod();" />
                        <label for="rdoWeek">
                            请选择 数据周期</label>
                        <select name="" id="drpPeriod">
                            <option value="1">年</option>
                            <option value="2">季度</option>
                            <option value="3">月</option>
                            <option value="4">周</option>
                        </select></li>
                    <li>
                        <input id="chk_IsCustomizePeriod" type="radio" name="rdo_time" onclick="fnCheckedPeriod();" />
                        <label for="chk_IsCustomizePeriod">
                            自定义</label>
                        <label for="">
                            开始日期</label>
                        <input type="text" id="dtStartDate" class="date_input" onclick="WdatePicker()" style="width: 100px;"
                            disabled="true" />
                        <label for="">
                            结束日期</label>
                        <input type="text" id="dtEndDate" class="date_input" onclick="WdatePicker()" style="width: 100px;"
                            disabled="true" /></li>
                    <li class="selDatetime" id="liStartEndDate" style="display: none;">
                        <%--<div id="dvPeriodPlaceholder"></div>--%>
                    </li>
                </ul>
                <div class="splitLine_1">
                </div>
                <div class="linkButton txtCenter">
                    <a class="btn_bottom ResetBtn" href="javascript:void(0)" onclick="resetAllFilter()"
                        style="width: 134px;">重置</a> <a class="btn_bottom ok" href="###" onclick="javascript:doKPIFilter();"
                            style="width: 134px;">确定</a>
                </div>
            </div>
            <div class="clearFix_12">
            </div>
        </div>
    </div>
    <%--终端筛选--%>
    <div style="display: none; height: 385px">
        <div id="win_filter_terminal" class="winBoxer" style="width: 495px; height: 385px;
            overflow: hidden">
            <div class="winCaption">
                <span class="winCaption_icon tFilter"></span><a class="winCaption_btnClose" href="###">
                    关闭</a>
            </div>
            <ul class="winTabs">
                <li class="time"><a href="#win_filter_time" id="btnFilterTime" isclick="1"></a></li>
                <li class="customer selected2"></li>
                <li class="product"><a href="#win_filter_product" id="btnFilterProduct2" isclick="1">
                </a></li>
            </ul>
            <div class="winContent contFilter" style="height: 275px">
                <ul style="height: 215px; *height: 155px; margin-left: 15px; padding-top: 50px;">
                    <li>
                        <%--<label for="">请选择终端类型</label>--%>
                        <div class="check" id="divChannel">
                            <asp:Literal ID="ltChannel" runat="server"></asp:Literal>
                        </div>
                        <%--
                       <asp:CheckBoxList ID="drpChannel" runat="server" Width="150px" >
                       </asp:CheckBoxList>--%>
                    </li>
                    <li class="selDatetime" style="display: none">
                        <label for="">
                            请选择连锁</label>
                        <asp:DropDownList ID="drpChain" runat="server" Width="150px">
                        </asp:DropDownList>
                    </li>
                </ul>
                <div class="splitLine_1">
                </div>
                <div class="linkButton txtCenter">
                    <a class="btn_bottom ResetBtn" href="javascript:void(0)" onclick="resetAllFilter()"
                        style="width: 134px;">重置</a> <a class="btn_bottom ok" href="###" onclick="javascript:doKPIFilter();"
                            style="width: 134px;">确定</a>
                </div>
            </div>
            <div class="clearFix_12">
            </div>
        </div>
    </div>
    <%--产品筛选--%>
    <div style="display: none; height: 385px">
        <div id="win_filter_product" class="winBoxer" style="width: 495px; height: 385px;
            overflow: hidden">
            <div class="winCaption">
                <span class="winCaption_icon tFilter"></span><a class="winCaption_btnClose" href="###">
                    关闭</a>
            </div>
            <ul class="winTabs">
                <li class="time"><a href="#win_filter_time" id="btnFilterTime2" isclick="1"></a>
                </li>
                <li class="customer"><a href="#win_filter_terminal" id="btnFilterTerminal2" isclick="1">
                </a></li>
                <li class="product selected1"></li>
            </ul>
            <div class="winContent contFilter">
                <ul class="terminal">
                    <li>
                        <div class="seekCondition">
                            <div class="input">
                                <input type="text" id="txt_CategoryName" value="品类" onfocus='fnOnFocus

(2)' onblur="fnOnBlur(2)" onkeydown="onKeyDown(2)" maxlength="15" />
                                <a class="iconSearcher" href="javascript:fnCategorySearch()">搜索</a>
                            </div>
                            <div class="check" id="divCategory">
                                <asp:Literal ID="ltCategoryList" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="seekCondition">
                            <div class="input">
                                <input type="text" id="txt_BrandName" value="品牌" onfocus='fnOnFocus(1)' onblur="fnOnBlur(1)"
                                    onkeydown="onKeyDown(1)" maxlength="15" />
                                <a class="iconSearcher" href="javascript:fnBrandSearch()">搜索</a>
                            </div>
                            <div class="check" id="divBrand">
                                <asp:Literal ID="ltBrandList" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="seekCondition">
                            <div class="input">
                                <input type="text" id="txt_SKUName" value="产品" onfocus='fnOnFocus(3)' onblur="fnOnBlur(3)"
                                    onkeydown="onKeyDown(3)" maxlength="15" />
                                <a class="iconSearcher" href="javascript:fnSKUSearch()">搜索</a>
                            </div>
                            <div class="check" id="divSKU">
                                <asp:Literal ID="ltSKUList" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </li>
                </ul>
                <div class="splitLine_1">
                </div>
                <div class="linkButton txtCenter">
                    <a class="btn_bottom ResetBtn" href="javascript:void(0)" onclick="resetAllFilter()"
                        style="width: 134px;">重置</a> <a class="btn_bottom ok" href="###" onclick="javascript:doKPIFilter();"
                            style="width: 134px;">确定</a>
                </div>
            </div>
            <div class="clearFix_12">
            </div>
        </div>
    </div>
    <%--样式切换--%>
    <div style="display: none; overflow: hidden">
        <div id="win_filter_style" class="winBoxer" style="width: 495px; height: 385px; overflow: hidden;">
            <div class="winCaption">
                <span class="winCaption_iconStyle tFilter"></span><a class="winCaption_btnClose"
                    href="###">关闭</a>
            </div>
            <div class="winContent contFilter">
                <div style="width: 475px; height: 265px; overflow: hidden; overflow-y: auto;">
                    <table class="tablestyle">
                        <tr>
                            <td colspan="3" style="text-align: left; padding-left: 15px">
                                面渲染
                            </td>
                        </tr>
                        <tr style="height: 100px; background-color: #f1f1f1">
                            <td id="td_1" style="width: 33%">
                                <a href="javascript:uptStyleValue(0)">
                                    <img src="LayoutResource/images/style/m11.png" /></a>
                            </td>
                            <td id="td_2" style="width: 33%">
                                <a href="javascript:uptStyleValue(1)">
                                    <img src="LayoutResource/images/style/m21.png" /></a>
                            </td>
                            <td id="td_3">
                                <a href="javascript:uptStyleValue(2)">
                                    <img src="LayoutResource/images/style/m31.png" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left; padding-left: 15px">
                                气泡渲染
                            </td>
                        </tr>
                        <tr style="height: 100px; background-color: #f1f1f1">
                            <td id="td_5">
                                <a href="javascript:uptStyleValue(4)">
                                    <img src="LayoutResource/images/style/m51.png" /></a>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left; padding-left: 15px">
                                饼面渲染
                            </td>
                        </tr>
                        <tr style="height: 100px; background-color: #f1f1f1">
                            <td id="td_4">
                                <a href="javascript:uptStyleValue(3)">
                                    <img src="LayoutResource/images/style/m41.png" /></a>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: left; padding-left: 15px">
                                柱渲染
                            </td>
                        </tr>
                        <tr style="height: 100px; background-color: #f1f1f1">
                            <td id="td_6">
                                <a href="javascript:uptStyleValue(5)">
                                    <img src="LayoutResource/images/style/m61.png" /></a>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="splitLine_1">
                </div>
                <div class="linkButton txtCenter">
                    <a class="btn_bottom cancel" href="###" style="width: 134px;">取消</a> <a class="btn_bottom ok"
                        href="###" onclick="javascript:switchStyle();" style="width: 134px;">
                        确定</a>
                </div>
            </div>
            <div class="clearFix_12">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
