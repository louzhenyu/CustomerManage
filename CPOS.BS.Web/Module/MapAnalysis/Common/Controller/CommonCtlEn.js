function ___changeLanguage(languageID) {
    //
//    document.getElementById("KPIRoot").text = "sdf" + languageID;
//    document.getElementById("KPIRoot").text = "sdf" + languageID; 
}

/*
当前的地图设置对象
*/

var CurrentMapSetting = {};
//是否显示底图
CurrentMapSetting.isShowBaseMap = { Value: true, GetText: function (item) {
    if (item.Value) {
        return 'Hide Base Map';
    } else {
        return 'Show Base Map';
    }
}
};
//是否显示数值
CurrentMapSetting.isShowValue = { Value: false, GetText: function (item) {
    if (item.Value) {
        return 'Hide Numerical';
    } else {
        return 'Show Numerical';
    }
}
};
//是否显示文本
CurrentMapSetting.isShowText = { Value: true, GetText: function (item) {
    if (item.Value) {
        return 'Hide Text';
    } else {
        return 'Show Text';
    }
}
};
/*
获得当前地图设置的文本
*/
function getMapSettingText(mapSettingKey) {
    switch (mapSettingKey) {
        case 'isShowBaseMap':
            return CurrentMapSetting.isShowBaseMap.GetText(CurrentMapSetting.isShowBaseMap);
        case 'isShowValue':
            return CurrentMapSetting.isShowValue.GetText(CurrentMapSetting.isShowValue);
        case 'isShowText':
            return CurrentMapSetting.isShowText.GetText(CurrentMapSetting.isShowText);
        default:
            return null;
    }
}
/*
显示/隐藏地图
*/
function changeIsShowBaseMap() {
    //更改地图设置
    var mapObject = window._map_getMapObject();
    CurrentMapSetting.isShowBaseMap.Value = !CurrentMapSetting.isShowBaseMap.Value;
    mapObject._map_product_BaseMapSwitch(CurrentMapSetting.isShowBaseMap.Value);
    //更改菜单文本
    var ctl_menuIsShowBaseMap = Ext.get('menuIsShowBaseMap');
    ctl_menuIsShowBaseMap.setHTML(CurrentMapSetting.isShowBaseMap.GetText(CurrentMapSetting.isShowBaseMap));
}
/*
显示/隐藏数值
*/
function changeIsShowValue() {
    if (CurrentKPI) {
        //更改地图设置
        var mapObject = window._map_getMapObject();
        CurrentMapSetting.isShowValue.Value = !CurrentMapSetting.isShowValue.Value;
        var mapSettingValue = 0;
        if (CurrentMapSetting.isShowValue.Value)
            mapSettingValue += 2;
        if (CurrentMapSetting.isShowText.Value)
            mapSettingValue += 1;
        if (mapSettingValue == 0)
            mapSettingValue = 4;
        mapObject._map_product_ShowKpi(mapSettingValue);
        //更改菜单文本
        var ctl_menuIsShowValue = Ext.get('menuIsShowValue');
        ctl_menuIsShowValue.setHTML(CurrentMapSetting.isShowValue.GetText(CurrentMapSetting.isShowValue));
    } else {
        alert("请首先选择KPI.");
    }
}
/*
显示/隐藏文本
*/
function changeIsShowText() {
    if (CurrentKPI) {
        //更改地图设置
        var mapObject = window._map_getMapObject();
        CurrentMapSetting.isShowText.Value = !CurrentMapSetting.isShowText.Value;
        var mapSettingValue = 0;
        if (CurrentMapSetting.isShowValue.Value)
            mapSettingValue += 2;
        if (CurrentMapSetting.isShowText.Value)
            mapSettingValue += 1;
        if (mapSettingValue == 0)
            mapSettingValue = 4;
        mapObject._map_product_ShowKpi(mapSettingValue);
        //更改菜单文本
        var ctl_menuIsShowText = Ext.get('menuIsShowText');
        ctl_menuIsShowText.setHTML(CurrentMapSetting.isShowText.GetText(CurrentMapSetting.isShowText));
    } else {
        alert("请首先选择KPI.");
    }
}
var CurrentKPI = {};
/*
更改KPI
*/
//最后渲染样式
var _lastRenderStyleCode;
//渲染样式是否改变
var _renderStyleChanged;
//筛选条件是否改变
var _filterChanged;
//地图通知提供的缓存
var _strMapFlashCachedDataes = null;
var _strMapFlashCachedThresholds = null;
var _cachedLayer = null; //缓存的层
var _cachedQueryLayer = null;
var _cachedQueryFilterLayer = null;
var _cachedQueryFilterVal = null; 
//上次的筛选条件 
var _lastFilters;

function changeKPI(kpiMenuItem) {
    var kpiID = kpiMenuItem.id;
    var kpi = findKPIBy(kpiID);
    $("#GetPoiPop").remove();
    //
    if (kpi != null) {
        //设置筛选选项卡的可用性
        var filterType = "";
        filterType = kpi.IsSupportTimeFilter + "," + kpi.IsSupportStoreFilter + "," + kpi.IsSupportSKUFilter;
        $("#TipIsClickValue").val(filterType);
        $("#IsSelectPop").val("1")

        var mapObject = window._map_getMapObject();
        //先删除当前的地图
        mapObject._map_product_DelKpiLayer();
        //

        var k = {};
        k.skpi_code = kpi.KPIID;
        k.skpi_label = kpi.KPIText;
        k.skpi_level = kpi.SupportedLevel;
        k.style_code = kpi.StyleCode;
        k.style_subcode = kpi.StyleSubCode;
        k.query_url = "";
        k.query_methed = "ReturnKPI";
        k.title = kpi.KPIText;
        k.legendname = kpi.KPILegendText;
        k.threshold_type = kpi.ThresholdType;
        k.point_threshold_Type = kpi.PointThresholdType;
         

        CurrentKPI = k;
        //清除筛选条件
        _lastFilters = null;
        filters = null;
        closeFilterResultWindow();
        resetAllFilter();
        _cachedLayer = new Array();
        _cachedLayer.push('1,0');
        _cachedLayer.push('2,0');
        _cachedLayer.push('3,0');
        _cachedLayer.push('4,0');
        //设置样式切换的值

        //设置方法 value="1,2,1,2,0"
        var styleCode = new Array();
        styleCode.push("0");
        styleCode.push("0");
        styleCode.push("0");
        styleCode.push("0");
        styleCode.push("0");
        styleCode.push("0");
        switch (k.style_code) {//SA:面图,SI:饼图,SB:气泡
            case "SA": //面图
            default:
                if (k.style_subcode == "1" || k.style_subcode == null)
                    styleCode[0] = "2";
                else
                    styleCode[0] = "1";
                if (k.style_subcode == "2")
                    styleCode[1] = "2";
                else
                    styleCode[1] = "1";
                if (k.style_subcode == "3")
                    styleCode[2] = "2";
                else
                    styleCode[2] = "1";
                styleCode[4] = "1";
                break;
            case "SI": //饼图
                styleCode[3] = "2";
                styleCode[5] = "1";
                break;
            case "SH": //饼图
                styleCode[3] = "1";
                styleCode[5] = "2";
                break;
            case "SB": //气泡  
                styleCode[0] = "1";
                styleCode[1] = "1";
                styleCode[2] = "1";
                styleCode[4] = "2";
                break; 
        }
        _currentStyle = styleCode[0] + "," + styleCode[1] + "," + styleCode[2] + "," + styleCode[3] + "," + styleCode[4] + "," + styleCode[5];
        _lastRenderStyleCode = _currentStyle;
        _renderStyleChanged = false ;
        _filterChanged = false;
        setStyleValue(_currentStyle);
        _strMapFlashCachedDataes = null;
        _strMapFlashCachedThresholds = null;
        mapObject._map_product_RenderKpi(k);

    } else {
        alert('找不到相应的KPI');
    }
}
/*
清空地图
*/
function clearMap() {
    var mapObject = window._map_getMapObject();
    //先删除当前的地图
    mapObject._map_product_DelKpiLayer();
    //应重置当前KPI，筛选条件等信息（ CurrentKPI，Filter）
    CurrentKPI = null;
    _queryLayer = null;
    _kpiCode = null;
    _queryFilterLayer = null;
    _queryFilterVal = null;
    //清空弹出窗口
    mapObject._map_product_HidePop();
    closeMapPointWin();

    resetAllFilter();
    //提示用户
    alert('Empty Map Successfully.');
}
/*
判断用户是否选择了查询条件
*/
function judgeHasFilter(){ 
    if (filters != null) {
        if (filters.BrandIDs != null && filters.BrandIDs.length>0) {
           return true;
        }
        if(filters.CategoryIDs!=null &&filters.CategoryIDs.length>0){
            return true;
        }
        if(filters.ChainID !=null &&filters.ChainID!=""){
            return true;
        }
        if(filters.ChannelID !=null &&filters.ChannelID.length>0){
            return true;
        }
        if(filters.SKUIDs !=null &&filters.SKUIDs.length>0){
            return true;
        }
        if(filters.IsCheckCustomizeDatePeriod){
            if (filters.StartDate != "" || filters.EndDate != "") {
                return true;
            }
        }else{
            if (filters.DatePeriod != 1) {
                return true;
            }
        }
    }
    //
    return false;
}
 
/*
试图显示筛选结果
*/
function tryShowFilterResult() {
    var hasFilter = judgeHasFilter();
    if (hasFilter) {
        $("#win_filter_result").css("display", "block");
    }
}
/*
关闭筛选结果
*/
function closeFilterResultWindow() {
    $("#win_filter_result").css("display", "none");
}

/*
执行样式切换
*/
var _isSwitchStyle = false;
function switchStyle() {
 
    _isSwitchStyle = true;
    getRenderStyleCode();
    //检测渲染样式是否改变了
    if (_currentStyle == _lastRenderStyleCode) {
        _renderStyleChanged = false;
    }
    else {
        _cachedLayer[0] = '1,0';
        _cachedLayer[1] = '2,0';
        _cachedLayer[2] = '3,0';
        _cachedLayer[3] = '4,0';
        _renderStyleChanged = true;
        _lastRenderStyleCode = _currentStyle;
    }
    doKPIFilter();
}
/*
执行KPI筛选
*/
var _isDoKPIFilter = false;
function doKPIFilter() {
    if (CurrentKPI) {
        _isDoKPIFilter = true;
        filters = fnGetFilters();
        //重置筛选结果
        resetFilterResult();
        //尝试显示筛选结果
        tryShowFilterResult();
        //记录最后筛选条件
        //检测筛选条件是否改变了
        if (Ext.JSON.encode(_lastFilters) == Ext.JSON.encode(filters)) {
            _filterChanged = false;
        }
        else {
            _filterChanged = true;
            _cachedLayer[0] = '1,0';
            _cachedLayer[1] = '2,0';
            _cachedLayer[2] = '3,0';
            _cachedLayer[3] = '4,0';
            _lastFilters = filters;
        }
        //渲染地图
        var mapObject = window._map_getMapObject();
        mapObject._map_product_ReRenderKpi(CurrentKPI);
        //关闭窗体
        $.fancybox.close();
    } else {
        alert("Please Select KPI First.");
    }
}

/*
结束筛选
*/
function exitKPIFilter() {
    if (!_isDoKPIFilter) {//win_filter_terminal//win_filter_product
        //还原筛选条件状态
        if (filters == null) {
            resetProductFilter();
            fnClearFilters();
        }
        else {
            resetProductFilter();
            //时间
            if (filters.IsCheckCustomizeDatePeriod)
                document.getElementById("chk_IsCustomizePeriod").click();
            else
                document.getElementById("rdoWeek").click();
            
            document.getElementById("drpPeriod").value = filters.DatePeriod;
            document.getElementById("dtStartDate").value = filters.StartDate;
            document.getElementById("dtEndDate").value = filters.EndDate;

            //渠道
         
            Ext.getCmp('cmbMultiSelect').jitSetValue(filters.ChannelID);
        
            //品类、品牌、产品
            if (filters.CategoryIDs != null) {
                for (i = 0; i < filters.CategoryIDs.length; i++) {
//                    alert(filters.CategoryIDs[i]);
                    //                    alert("p_chkCategory_" + filters.CategoryIDs[i]);.click()

                    document.getElementById("chkCategory_" + filters.CategoryIDs[i]).click();
//                    document.getElementById("chkCategory_" + filters.CategoryIDs[i]).checked = true;
                }
            }
            if (filters.BrandIDs != null) {
                for (i = 0; i < filters.BrandIDs.length; i++) {
                    document.getElementById("chkBrand_" + filters.BrandIDs[i]).click();
                }
            }
            if (filters.SKUIDs != null) {
                for (i = 0; i < filters.SKUIDs.length; i++) {
                    document.getElementById("chkSKU_" + filters.SKUIDs[i]).click();
                }
            }
        }
    }
    _isDoKPIFilter = false;
}

/*
结束样式切换
*/
function exitChangeStyle() {
    if (!_isSwitchStyle) {
        //还原样式
        setStyleValue(_currentStyle);
    }
    _isSwitchStyle = false;
}

/*
重置筛选结果
*/
function resetFilterResult() {
    //处理筛选条件
    var hasFilters = judgeHasFilter();
    if (hasFilters) {
        //组织筛选条件结果文本
        //时间
        var strDatePeriod = "";
        if (filters.IsCheckCustomizeDatePeriod) {
            strDatePeriod = "自定义(" + document.getElementById("dtStartDate").value + "~" + document.getElementById("dtEndDate").value + ")";
        } else {
            var dp = document.getElementById("drpPeriod").value;
            switch (dp) {
                case "1":
                    strDatePeriod = "年";
                    break;
                case "2":
                    strDatePeriod = "季度";
                    break;
                case "3":
                    strDatePeriod = "月";
                    break;
                case "4":
                    strDatePeriod = "周";
                    break;
            }
        }
        $("#dvFilterResultOfDatePeriod").html(strDatePeriod);
        //渠道
        var strChannel = "全部";
        var ctl_cmbMultiSelect = Ext.getCmp('cmbMultiSelect');
        if (ctl_cmbMultiSelect.allSelector) {
            if (ctl_cmbMultiSelect.allSelector.dom.className.indexOf("x-boundlist-selected") <= 0) {
                var strChannel = Ext.getCmp('cmbMultiSelect').getRawValue();
                if (strChannel != null && strChannel != "") {
                    strChannel = strChannel.replace(/ /g, "");
                    if (strChannel.length > 15) {
                        strChannel = strChannel.substring(0, 15) + "...";
                    }
                } 
            }
        }
        if (strChannel != null && strChannel != "") {
            $("#dvFilterResultOfChannel").html(strChannel);
        } else {
            $("#dvFilterResultOfChannel").html("全部");
        }
        //产品
        var strSKU = "";
        if (filters.CategoryIDs != null && filters.CategoryIDs.length > 0) {
            for (var i = 0; i < filters.CategoryIDs.length; i++) {
                var selectedCategoryID = filters.CategoryIDs[i];
                for (var j = 0; j < __categories.length; j++) {
                    if (__categories[j].CategoryID == selectedCategoryID) {
                        strSKU += __categories[j].CategoryName + ",";
                        break;
                    }
                }
            }
        }
        if (strSKU.length < 15) {
            if (filters.BrandIDs != null && filters.BrandIDs.length > 0) {
                for (var i = 0; i < filters.BrandIDs.length; i++) {
                    var selectedBrandID = filters.BrandIDs[i];
                    for (var j = 0; j < __brands.length; j++) {
                        if (__brands[j].BrandID == selectedBrandID) {
                            strSKU += __brands[j].BrandName + ",";
                            break;
                        }
                    }
                }
            }
        }
        if (strSKU.length < 15) {
            if (filters.SKUIDs != null && filters.SKUIDs.length > 0) {
                for (var i = 0; i < filters.SKUIDs.length; i++) {
                    var selectedSKUID = filters.SKUIDs[i];
                    for (var j = 0; j < __skus.length; j++) {
                        if (__skus[j].SKUID == selectedSKUID) {
                            strSKU += __skus[j].SKUName + ",";
                            break;
                        }
                    }
                }
            }
        }
        if (strSKU != null && strSKU != "") {
            strSKU = strSKU.substring(0, strSKU.length - 1);
            if (strSKU.length > 15) {
                strSKU = strSKU.substring(0, 15) + "...";
            }
            $("#dvFilterResultOfSKU").html(strSKU);
        } else {
            $("#dvFilterResultOfSKU").html("全部");
        }
        //
        $("#btnFilter").addClass("navItemsSelect");
    } else {
        $("#btnFilter").removeClass("navItemsSelect");
    }
}
/*
根据KPI id获取KPI对象
*/
function findKPIBy(kpiID) {
    if (kpiID) {
        if (mapKPIs != null) {
            for (var i = 0; i < mapKPIs.length; i++) {
                if (mapKPIs[i].KPIID == kpiID) {
                    return mapKPIs[i];
                }
            }
        }
    }
    //
    return null;
}
var CurrentPointDataes = null; //当前的点信息
/*
根据点ID获取点信息
*/
function findPointInfo(pointID) {
    if (CurrentPointDataes != undefined && CurrentPointDataes.Data != undefined) {
        for (var i = 0; i < CurrentPointDataes.Data.length; i++) {
            var p = CurrentPointDataes.Data[i];
            if (p.id == pointID) {
                return p;
            }
        }
    }
    return null;
}
//记录查询参数，用于导出
var _kpiCode;
var _queryLayer;
var _queryFilterLayer;
var _queryFilterVal;
var _strMapFlashDataes = "null";
var _strMapFlashThresholds = "null";
var timeOutPop = null;
var _doubleclicking = false;
var filters = null;
/*
获取KPI数据
*/
function _map_product_ReturnKpi_js(kpiCode, queryLayer, queryFilterLayer, queryFilterVal) {

    //    alert('queryLayer' + queryLayer + ',queryFilterLayer' + queryFilterLayer + ',queryFilterVal' + queryFilterVal);
    _kpiCode = kpiCode;
    _queryLayer = queryLayer;
    _queryFilterLayer = queryFilterLayer;
    _queryFilterVal = queryFilterVal;
    _doubleclicking = true;
    //获得Map对象
    var mapObject = window._map_getMapObject();
    //保存当前的状态
    if (CurrentKPI) {
        CurrentKPI.query_layer = queryLayer;
        CurrentKPI.query_filterlayer = queryFilterLayer;
        CurrentKPI.query_filterval = queryFilterVal;
    }
    //如果需要钻入到终端层且设置中的底图是隐藏的，则自动显示
    if (queryLayer == "4" && CurrentMapSetting.isShowBaseMap.Value == false) {
        changeIsShowBaseMap();
    }
    //
    var mask1 = new Ext.LoadMask(Ext.getBody(), {
        msg: '加载数据,请等待...'
    });


    //样式
    var request = {};
    request.Level = queryLayer;
    request.BoundID = queryFilterVal;
    request.KPIID = kpiCode;
    request.StyleCode = CurrentKPI.style_code;
    request.StyleSubCode = CurrentKPI.style_subcode;
    //用户选择的筛选条件
    if (filters != null) {
        request.DatePeriod = filters.DatePeriod;
        request.IsCheckCustomizeDatePeriod = filters.IsCheckCustomizeDatePeriod;
        request.StartDate = filters.StartDate;
        request.EndDate = filters.EndDate;
        request.ChannelID = filters.ChannelID;
        request.ChainID = filters.ChainID;
        request.BrandIDs = filters.BrandIDs;
        request.CategoryIDs = filters.CategoryIDs;
        request.SKUIDs = filters.SKUIDs;
    }

    if (_cachedQueryLayer == _queryLayer && _cachedQueryFilterLayer == _queryFilterLayer && _cachedQueryFilterVal == _queryFilterVal && _cachedLayer[_queryLayer - 1] == _queryLayer + ',1'  && _strMapFlashCachedDataes != null && _strMapFlashCachedThresholds != null) {//使用缓存数据
        _strMapFlashDataes = _strMapFlashCachedDataes;
        _strMapFlashThresholds = _strMapFlashCachedThresholds; 
        mapObject._map_product_RenderKpi_Back(_strMapFlashDataes, _strMapFlashThresholds);
    }
    else {
        //请求新数据

        Ext.Ajax.request({
            method: 'POST'
        , url: JITPage.HandlerUrl.getValue() + '&op=2'
        , timeout: 60000
        , params: Ext.JSON.encode(request)
        , callback: function (options, success, response) {
            //初始化KPI菜单
            if (success) {
                //判断是否框架层有错误
                var temp = eval("(" + response.responseText + ")");
                if (temp != null && temp.success != undefined && temp.success != true) {
                    alert(temp.msg);
                    if (_strMapFlashDataes != "null" || _strMapFlashThresholds != "null") {
                        mapObject._map_product_RenderKpi_Back(_strMapFlashDataes, _strMapFlashThresholds);
                    }
                    mask1.hide();
                    return;
                }
                //
                var strDataes = response.responseText;
                var dataes = eval("(" + strDataes + ")");
                //
                if (dataes != null) {
                    _strMapFlashDataes = Ext.JSON.encode(dataes.Data);
                    _strMapFlashThresholds = Ext.JSON.encode(dataes.Thresholds);
                    _cachedLayer[_queryLayer - 1] = _queryLayer + ',1';
                }
                mapObject._map_product_RenderKpi_Back(_strMapFlashDataes, _strMapFlashThresholds);
                if (dataes == null) {
                    alert("查无数据");
                    // 弹出“筛选"层

                    //$("#btnFilter").fancybox({ 'padding': 0 }).click();
                    if (timeOutPop) { clearTimeout(timeOutPop) };
                    timeOutPop = setTimeout(function () { $("#btnFilter").trigger(jQuery.Event("click")); }, 500);

                }

                //如果需要钻入到终端层且当前KPI为销量数据，则开启收银监控
                if (queryLayer == "4" && kpiCode.toString().toUpperCase() == "EC44E43C-78BD-44C8-9ECA-30D031E27E12") {
//                    startTime = new Date();
                    CurrentPointDataes = eval(dataes);
                    keepCPOSMonitoring = true;
                    startCPOSMonitoring();
                } else {
                    stopCPOSMonitoring();
                }
            } else {
                //                mapObject._map_product_RenderKpi_Back("", "");
                alert('获取数据失败.');
                if (_strMapFlashDataes != "null" || _strMapFlashThresholds != "null") {
                    mapObject._map_product_RenderKpi_Back(_strMapFlashDataes, _strMapFlashThresholds);
                }
            }
            //
            mask1.hide();
        }
        });
        mask1.show();
    }
}
/*
导出地图
*/
var CurrentExportedMapFileName = "";
var CurrentExportedType = 1;
function exportMap() {
    //关闭导出窗体
    $.fancybox.close();
    //获得Map对象
    var mapObject = window._map_getMapObject();
    //获得导出的方式
    var exportType = 1;
    if (document.getElementById('rdgExportMap').checked) {
        exportType = 1;
    } else if (document.getElementById('rdgExportData').checked) {
        exportType = 2;
    } else if (document.getElementById('rdgExportMapAndData').checked) {
        exportType = 3;
    }
    CurrentExportedType = exportType;
    switch (exportType) {
        case 1:
            {
                //导出地图到本地
                mapObject._map_ExportMap("1", "");
            }
            break;
        case 2:
            {
                if (_queryLayer == null || _kpiCode == null) {
                    alert("请先选择KPI!");
                    return;
                }
                //导出数据
                exportData2();
            }
            break;
        case 3:
            {
                if (_queryLayer == null || _kpiCode == null) {
                    alert("请先选择KPI!");
                    return;
                }
                //导出地图&数据
                CurrentExportedMapFileName = Ext.Date.now().toString() + ".jpg";
                mapObject._map_ExportMap("2", CurrentExportedMapFileName);
            }
            break;
    }
}
/*
仅导出数据
*/
function exportData2() {
    //    if (_queryLayer == null || _kpiCode == null) {
    //        alert("请先选择KPI!");
    //        return;
    //    }

    //
    var mask = new Ext.LoadMask(Ext.getBody(), {
        msg: '正在准备数据,请等待...'
    });
    mask.show();
    var request = {};
    request.Level = _queryLayer;
    request.BoundID = _queryFilterVal;
    request.KPIID = _kpiCode;
    request.StyleCode = CurrentKPI.style_code;
    request.StyleSubCode = CurrentKPI.style_subcode;
    //用户选择的筛选条件 
    if (filters != null) {
        request.DatePeriod = filters.DatePeriod;
        request.IsCheckCustomizeDatePeriod = filters.IsCheckCustomizeDatePeriod;
        request.StartDate = filters.StartDate;
        request.EndDate = filters.EndDate;
        request.ChannelID = filters.ChannelID;
        request.ChainID = filters.ChainID;
        request.BrandIDs = filters.BrandIDs;
        request.CategoryIDs = filters.CategoryIDs;
        request.SKUIDs = filters.SKUIDs;
    }
    //
    Ext.Ajax.request({
        method: 'POST'
        , url: JITPage.HandlerUrl.getValue() + '&op=4'
        , timeout: 60000
        , params: Ext.JSON.encode(request)
        , callback: function (options, success, response) {
            //初始化KPI菜单
            if (success) {
                var filePath = response.responseText;
                if (filePath != null) {
                    //准备导出
                    downloadKpiData(filePath);
                }
            } else {
                alert('查无数据' );
            }
            //
            mask.hide();
        }
    });
}

/*
导出图片及数据
*/
function exportData3() {
    //    if (_queryLayer == null || _kpiCode == null) {
    //        alert("请先选择KPI!");
    //        return;
    //    }
    //
    var mask = new Ext.LoadMask(Ext.getBody(), {
        msg: '正在准备数据,请等待...'
    });
    mask.show();
    var request = {};
    request.Level = _queryLayer;
    request.BoundID = _queryFilterVal;
    request.KPIID = _kpiCode;
    request.StyleCode = CurrentKPI.style_code;
    request.StyleSubCode = CurrentKPI.style_subcode;
    //用户选择的筛选条件 
    if (filters != null) {
        request.DatePeriod = filters.DatePeriod;
        request.IsCheckCustomizeDatePeriod = filters.IsCheckCustomizeDatePeriod;
        request.StartDate = filters.StartDate;
        request.EndDate = filters.EndDate;
        request.ChannelID = filters.ChannelID;
        request.ChainID = filters.ChainID;
        request.BrandIDs = filters.BrandIDs;
        request.CategoryIDs = filters.CategoryIDs;
        request.SKUIDs = filters.SKUIDs;
    }
    //
    Ext.Ajax.request({
        method: 'POST'
        , url: JITPage.HandlerUrl.getValue() + '&op=5&file=http://www.qudaoyun.cn:2453/UpLoad/' + CurrentExportedMapFileName
        , timeout: 60000
        , params: Ext.JSON.encode(request)
        , callback: function (options, success, response) {
            //初始化KPI菜单
            if (success) {
                var filePath = response.responseText;
                if (filePath != null) {
                    //准备导出
                    downloadKpiData(filePath);
                }
            } else {
                alert('查无数据');
//                alert('出错了:' + response.responseText);
            }
            //
            mask.hide();
        }
    });
}
/*
下载数据
*/
function downloadKpiData(filePath) {
    var submitForm = document.createElement("FORM");
    document.body.appendChild(submitForm);
    //    submitForm.action = "./LayoutResource/templates/导入模板.xlsx";
    submitForm.action = filePath;
    submitForm.submit();
}
/*
地图导出回调函数
*/
function _map_ExportMap_Result(result) {
    var isSuccess = false;
    if (result == "") {
        isSuccess = true;
    }
    else {
        alert('地图导出失败,错误信息:['+result+'].');
    }
    if (isSuccess && CurrentExportedType == 3) {
        //            var postUrl = JITPage.HandlerUrl.getValue() + "&op=5&file=" + CurrentExportedMapFileName;
        exportData3();
    }
}

/*
用于调试地图渲染异常的回调函数
*/
function _map_dada(obj1, obj2, obj3) {
    var temp1 = obj1;
    var temp2 = obj2;
    var temp3 = obj3;
}

/*
地图初始化完毕通知函数
*/
function _map_InitMap() {
    changeIsShowBaseMap();
}

/*
3.17	提示框回调
*/
function _map_ToolTip_Result(attributes, point) {
    alert("_map_ToolTip_Result");
}

/*
3.18	弹出框回调
*/
function _map_Pop_Result(attributes, point) {
    //    alert("_map_Pop_Result");

    var mapObject = window._map_getMapObject();
    //    mapObject._map_product_LocateXY(200, 300);
    mapObject._map_product_LocateXY(attributes.x, attributes.y);
    //    var newPoint = findPointInfo(attributes.id.toLowerCase());
    //    if (newPoint == null)
    //        return;
    var pointInfo = {};
    pointInfo.contitle = attributes.title;
    var detailRow = new Array();
    switch (CurrentKPI.skpi_code.toUpperCase()) {
        case "1CF568D2-FCC3-4D65-9979-B942F50DF064": //终端数量(KPIData为渠道，与固定信息中的渠道重复，不用再添加)
            break;
        case "EC44E43C-78BD-44C8-9ECA-30D031E27E12": //销量数据
            var rowItem3 = {};
            rowItem3.title = "销量数据:";
            rowItem3.description = attributes.kpilabel;
            detailRow.push(rowItem3);
            break;
        case "CCC4CCF2-8D01-495C-A181-EEA21B9BB495": //分销率
            var rowItem4 = {};
            rowItem4.title = "是否分销:";
            rowItem4.description = attributes.kpilabel;
            detailRow.push(rowItem4);
            break;
        case "6A747AF4-1219-4514-8CBA-28847E6F7EA9": //月拜访率
            var rowItem5 = {};
            rowItem5.title = "月拜访率:";
            rowItem5.description = attributes.kpilabel;
            detailRow.push(rowItem5);
            break;
        case "B83657B2-D9E9-499D-9263-814B588D628A": //月有效拜访率
            var rowItem6 = {};
            rowItem6.title = "月有效拜访率:";
            rowItem6.description = attributes.kpilabel;
            detailRow.push(rowItem6);
            break;
    }
    //销量数据演示
    if (CurrentKPI.skpi_code.toUpperCase() == "EC44E43C-78BD-44C8-9ECA-30D031E27E12"
        && attributes.id == "BD3A6DE8-3C00-4EF6-B1C4-025D79BD1B8B".toLowerCase()
    ) {
        GetPoiData2(300, 400, pointInfo);
        return;
    }
    //固定信息

    Ext.Ajax.request({
        method: 'POST'
        , url: JITPage.HandlerUrl.getValue() + '&op=6&sid=' + attributes.id
        //        , params: Ext.JSON.encode(request)
        , callback: function (options, success, response) {
            //初始化KPI菜单
            if (success) {
                var responseInfo = response.responseText;
                var storeInfo = eval("(" + responseInfo + ")");
                if (storeInfo != null) {
                    var rowItem = {};
                    rowItem.title = "渠道类型:";
                    if (attributes.kpifilter1 != null)
                        rowItem.description = attributes.kpifilter1;
                    else
                        rowItem.description = "";
                    detailRow.push(rowItem);
                    var rowItem2 = {};
                    rowItem2.title = "地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;址:";
                    if (attributes.address != null)
                        rowItem2.description = attributes.address;
                    else
                        rowItem2.description = "";
                    detailRow.push(rowItem2);
                    var rowItem3 = {};
                    rowItem3.title = "联&nbsp;&nbsp;系&nbsp;人:";
                    if (storeInfo.Manager != null)
                        rowItem3.description = storeInfo.Manager;
                    else
                        rowItem3.description = "";
                    detailRow.push(rowItem3);
                    var rowItem4 = {};
                    rowItem4.title = "联系方式:";
                    if (storeInfo.Tel != null)
                        rowItem4.description = storeInfo.Tel;
                    else
                        rowItem4.description = "";
                    detailRow.push(rowItem4);
                    var rowItem5 = {};
                    rowItem5.title = "营业状态:";
                    if (storeInfo.Status == "1")
                        rowItem5.description = "正常";
                    else
                        rowItem5.description = "休业";
                    detailRow.push(rowItem5);

                    if (storeInfo.Col10 != null)
                        pointInfo.date = storeInfo.Col10;
                    else
                        pointInfo.date = "";
                    if (storeInfo.BannerPhoto != null)
                        pointInfo.imageUrl = storeInfo.BannerPhoto;
                    else
                        pointInfo.imageUrl = "";
                    //动态KPI数据
                    pointInfo.conlist = detailRow;

                    GetPoiData(300, 400, pointInfo);
                }

            } else {
                alert('出错了:' + response.responseText);
            }
            // 
        }
    });

    //    GetPoiData(newPoint.x, newPoint.y, { contitle: '中欧移动商店', date: '2013-06-06', imageUrl: '', conlist: [{ "title": "1234", "description": "1233" }, { "title": "1234", "description": "1233"}] });
    //    alert(attributes);
    //    alert(point);
    //    alert("_map_Pop_Result99");
}

/*
3.19	地图数据视图变更或是数据源变更时候通知js方法
*/
//var lastRenderMapDetail;
function _map_product_NoticeRenderData_js(mapDetail) {
    CurrentKPI.query_filtervalname = mapDetail.queryFilterValName;
    //此回调在一次钻取时会触发多次。(地图实际可能放大3层)    
    var mapObject = window._map_getMapObject();
    mapObject._map_product_HidePop();
    closeMapPointWin();
    if (_doubleclicking) {
        //用户双击的动作
        if (mapDetail.queryLayer == _queryLayer && mapDetail.queryFilterLayer == _queryFilterLayer && mapDetail.queryFilterVal == _queryFilterVal) {
            _doubleclicking = false;
        }
    }
    else {
        //滚轮动作
        if (mapDetail.queryLayer != _queryLayer || mapDetail.queryFilterLayer != _queryFilterLayer || mapDetail.queryFilterVal != _queryFilterVal) {
            //如果未切换样式且未改变筛选条件，则回退时，不重新取数据而直接用缓存数据。
            _strMapFlashCachedDataes = mapDetail.dataobj;
            _strMapFlashCachedThresholds = mapDetail.thresholdobj;

            _cachedQueryLayer = mapDetail.queryLayer;
            _cachedQueryFilterLayer = mapDetail.queryFilterLayer;
            _cachedQueryFilterVal = mapDetail.queryFilterVal;
            _queryLayer = mapDetail.queryLayer;
            _queryFilterLayer = mapDetail.queryFilterLayer;
            _queryFilterVal = mapDetail.queryFilterVal;
            CurrentKPI.query_layer = mapDetail.queryLayer;
            CurrentKPI.query_filterlayer = mapDetail.queryFilterLayer;
            CurrentKPI.query_filterval = mapDetail.queryFilterVal;
            //渲染地图
            mapObject._map_product_ReRenderKpi(CurrentKPI);
        }
    }
//        alert(mapDetail);

}


/*
切换KPI的样式
*/
var renderStyle = new Array();
var _currentStyle;
function getRenderStyleCode() {
    _currentStyle = getStyleValue();
    var newStyleCode = _currentStyle.split(","); 
    switch (CurrentKPI.style_code) {
        case 'SI': //饼、柱
        case 'SH': //饼、柱
            if (newStyleCode[3] == '2') {
                CurrentKPI.style_code = 'SI';
                CurrentKPI.style_subcode = '1';
            }
            else if (newStyleCode[5] == "2") {
                CurrentKPI.style_code = 'SH';
                CurrentKPI.style_subcode = '1';
            }

            break;
        default: //面、气泡
            if (newStyleCode[0] == '2') {
                CurrentKPI.style_code = 'SA';
                CurrentKPI.style_subcode = '1';
            }
            else if (newStyleCode[1] == "2") {
                CurrentKPI.style_code = 'SA';
                CurrentKPI.style_subcode = '2';
            }
            else if (newStyleCode[2] == "2") {
                CurrentKPI.style_code = 'SA';
                CurrentKPI.style_subcode = '3';
            }
            else if (newStyleCode[4] == "2") {
                CurrentKPI.style_code = 'SB';
                CurrentKPI.style_subcode = '1';
            }
            break;
    }
}

/*
Ext加载完毕
*/
Ext.onReady(function () {
    //
    JITPage.HandlerUrl.setValue("Handler/CommonHandler.ashx?mid=" + __mid + "&btnCode=search");
    //JITPage.HandlerUrl.setValue("Handler/CommonHandler.ashx?");

    //渠道选择控件
    var store = Ext.create('Ext.data.Store', {
        fields: ['ChannelID', 'ChannelName'],
        data: __channels
    });
    //创建多选下拉框
    Ext.create('Jit.form.field.ComboBox', {
        renderTo: 'divChannel'
                        , id: 'cmbMultiSelect'
                        , valueField: 'ChannelID'
                        , displayField: 'ChannelName'
                        , store: store
                        , width: 300
                        , labelWidth: 120
        //                        , labelStyle: 'font-family: "微软雅黑";font-size: 14px;'
                        , labelStyle: 'font-size: 16px;'
                        , multiSelect: true
                        , fieldLabel: 'Please Select Store Type'
                        , jitAllText: '全选'  //设置全选的文本内容，默认为All
    });
});
/*
品牌全选/全不选
*/
function brandSelectAll(allSelector) {
    if (allSelector) {
        var isSelected = allSelector.checked;
        var brands = document.getElementsByName("chkBrand");
        if (brands != null && brands.length > 0) {
            for (var i = 0; i < brands.length; i++) {
                var b = brands[i];
                var pb = document.getElementById("p_chkBrand_" + b.value);
                if (pb.style.display != 'none') {
                    b.checked = isSelected;
                    fnCheckChange(1, b.value);
                }
            }
        }
    }
}
/*
品类全选/全不选
*/
function categorySelectAll(allSelector) {
    if (allSelector) {
        var isSelected = allSelector.checked;
        var categories = document.getElementsByName("chkCategory");
        if (categories != null && categories.length > 0) {
            for (var i = 0; i < categories.length; i++) {
                var c = categories[i];
                var pb = document.getElementById("p_chkCategory_" + c.value);
                if (pb.style.display != 'none') {
                    c.checked = isSelected;
                    fnCheckChange(2, c.value);
                }
            }
        }
    }
}

/*
产品全选/全不选
*/
function skuSelectAll(allSelector) {
    if (allSelector) {
        var isSelected = allSelector.checked;
        var skues = document.getElementsByName("chkSKU");
        if (skues != null && skues.length > 0) {
            for (var i = 0; i < skues.length; i++) {
                var s = skues[i];
                var pb = document.getElementById("p_chkSKU_" + s.value);
                if (pb.style.display != 'none') {
                    s.checked = isSelected;
                    fnCheckChange(3, s.value);
                }
            }
        }
    }
}

/*
重置所有筛选条件
*/
function resetAllFilter() {
    resetProductFilter();
    fnClearFilters();
    resetFilterResult();
}
/*
重置 品牌、品类、筛选查询条件 
*/
function resetProductFilter() {
    document.getElementById("chkSKUAll").checked = false;
    document.getElementById("chkBrandAll").checked = false;
    document.getElementById("chkCategoryAll").checked = false;
    var itemSKUCheckBox = document.getElementsByName("chkSKU");
    for (var i = itemSKUCheckBox.length - 1; i >= 0; i--) {
        var skuItem = document.getElementById("p_chkSKU_" + itemSKUCheckBox[i].value);
        skuItem.parentNode.removeChild(skuItem);
    }
    var itemBrandCheckBox = document.getElementsByName("chkBrand");
    for (var i = itemBrandCheckBox.length - 1; i >= 0; i--) {
        var brandItem = document.getElementById("p_chkBrand_" + itemBrandCheckBox[i].value);
        brandItem.parentNode.removeChild(brandItem);
    }
    var itemCategoryCheckBox = document.getElementsByName("chkCategory");
    for (var i = itemCategoryCheckBox.length - 1; i >= 0; i--) {
        itemCategoryCheckBox[i].checked = false;
    }
    var brandHtml = document.getElementById("divBrand").innerHTML;
    for (var i = 0; i < __brands.length; i++) {

        var displayName = __brands[i].BrandName;
        if (displayName.length > 7)
            displayName = displayName.substring(0, 7);
        brandHtml += "<p id=\"p_chkBrand_" + __brands[i].BrandID + "\" title=\"" + __brands[i].BrandName + "\"   class=\"pText\"><input id=\"chkBrand_" + __brands[i].BrandID + "\" onclick=\"fnCheckChange(1," + __brands[i].BrandID + ")\" name=\"chkBrand\" type=\"checkbox\" value=\"" + __brands[i].BrandID + "\" /><label for=\"chkBrand_" + __brands[i].BrandID + "\">" + displayName + "</label></p>\r\n";
    }
    document.getElementById("divBrand").innerHTML = brandHtml;

    var skuHtml = document.getElementById("divSKU").innerHTML;
    for (var i = 0; i < __skus.length; i++) {//所有SKU 
        var displayName = __skus[i].SKUName;
        if (displayName.length > 7)
            displayName = displayName.substring(0, 7);
        skuHtml += "<p id=\"p_chkSKU_" + __skus[i].SKUID + "\" title=\"" + __skus[i].SKUName + "\"   class=\"pText\"><input id=\"chkSKU_" + __skus[i].SKUID + "\" onclick=\"fnCheckChange(3," + __skus[i].SKUID + ")\" name=\"chkSKU\" type=\"checkbox\" value=\"" + __skus[i].SKUID + "\" /><label for=\"chkSKU_" + __skus[i].SKUID + "\">" + displayName + "</label></p>\r\n";
    }
    document.getElementById("divSKU").innerHTML = skuHtml;

    //渠道选择控件 
    Ext.getCmp('cmbMultiSelect').jitSetValue(null);
}
var keepCPOSMonitoring = false;
/*
开始CPOS监控
*/
//var startTime = new Date();
function startCPOSMonitoring() {
    Ext.Ajax.request({
        method: 'POST'
        , url: JITPage.HandlerUrl.getValue() + '&op=3'
        , callback: function (options, success, response) {
            //处理结果
            if (success) {
                //
                var strResponse = response.responseText;
                //                alert(strResponse);
                var rst = eval("(" + strResponse + ")");
                //需要改变的点
                var mapObject = window._map_getMapObject();
                var point = findPointInfo("bd3a6de8-3c00-4ef6-b1c4-025d79bd1b8b".toLowerCase());
                if (point == null)
                    return;
                var changedPoint = Ext.clone(point);
//                var currentTime = new Date();
//                var dif = currentTime.getTime() - startTime.getTime();
//                var difSeconds = dif / (1000);
                //
                if (rst != null && rst.UnitList != null && rst.UnitList.length > 0) {
//                if (difSeconds < 30) {//需要改变
                    //                    changedPoint.styleimage = "assets/images/jit/SalesVolumes/1.png";
                    //                    changedPoint.styleimage = "assets/images/jit/dada.swf";
                    changedPoint.popurl = "http://o2oapi.aladingyidong.com/TerminalSalesInfo.aspx";
                    changedPoint.styleimage = "";
                    //
                    var dataes = new Array();
                    dataes.push(changedPoint);
                    var strDataes = Ext.JSON.encode(dataes);
                    //闪烁效果
                    var falshobj = '[{"duration":"1000","blurXFrom":"0","blurYFrom":"0","blurXTo":"20","blurYTo":"20","color":"0xFF0000","strength":"10"}]';
                    //
                    mapObject._map_product_ChangePoint(strDataes, falshobj);
                } else {
                    changedPoint.styleimage = "";
                    //                    changedPoint.styleimage = "assets/images/jit/SalesVolumes/1.png";
                    //重置点样式
                    var dataes = new Array();
                    dataes.push(changedPoint);
                    var strDataes = Ext.JSON.encode(dataes);
                    //
                    mapObject._map_product_ChangePoint(strDataes);
                }

            } else {
                alert("处理失败：" + response.responseText);
            }
            if (keepCPOSMonitoring) {
                setTimeout(startCPOSMonitoring, 1000 * 2);
            }
        }
    });
}
/*
停止CPOS监控
*/
function stopCPOSMonitoring() {
    keepCPOSMonitoring = false;
}

function GetPoiData(ax, by, Obj) {
    $("#GetPoiPop").remove();
    var getOffset = $(".cloud_map").offset();

    var x = $(".cloud_map").width() / 2, y = $(".cloud_map").height() / 2;

    var boxLeft = getOffset.left + x + 29,
		boxTop = 107 + y / 2;
    var iString = '';
    var _non = '';
    if (Obj.imageUrl == '') {
        _non = '_non';
    }
    iString += '<div class="winWraper" id="GetPoiPop" style="left: ' + boxLeft + 'px; top: ' + boxTop + 'px; z-index:999"><div style="width:492px; height:370px;" class="winBoxer"><div class="winCaption"><span class="winCaption_icon tShop"></span><a onclick="closeMapPointWin();" href="javascript:void(0)" class="btnCloseWin">关闭</a></div><div class="winContent contShare"><b class="h3"><span class="tH3">' + Obj.contitle + '</span> <span class="tDate">' + Obj.date + '</span></b><div class="mobile_shop' + _non + '">';
    if (Obj.imageUrl != "") {
        iString += '<img border="0" alt="" src="' + Obj.imageUrl + '" class="view">';
    } else {
        iString += '<img border="0" alt="" src="./LayoutResource/images/DefaultStoreImage.jpg" class="view">';
        //        iString += '<div class="addView"><a  href="###"><span class="hLine"></span><span class="vLine"></span><h2>添加终端图片</h2></a></div>';
    }
    iString += '<ul class="desc">';
    var getConnextObj = Obj.conlist;
    if (getConnextObj.length > 0) {
        for (var i = 0; i < getConnextObj.length; i++) {
            iString += '<li><label for="">' + getConnextObj[i].title + '</label>' + getConnextObj[i].description + '</li>';
        }
    }
    iString += '</ul></div></div><div class="clearFix_12"></div></div><div class="tip_leftArrow" style="top:141px;"></div></div>';
    $("body").append(iString);
}
function GetPoiData2(ax, by, Obj) {
    $("#GetPoiPop").remove();

    var getOffset = $(".cloud_map").offset();
    var x = $(".cloud_map").width() / 2, y = $(".cloud_map").height() / 2;

    var boxLeft = getOffset.left + x + 30,
		boxTop = 107 + y / 2;
    var iString = '';

    iString += '<div class="winWraper" id="GetPoiPop" style="left: ' + boxLeft + 'px; top: ' + boxTop + 'px; z-index:999; margin-top:-35px;"><div style="width:520px; height:450px;" class="winBoxer"><div class="winCaption"><span class="winCaption_icon tShop"></span><a onclick="closeMapPointWin();" href="javascript:void(0)" class="btnCloseWin">关闭</a></div><div class="winContent contShare">';


    iString += '<iframe src="http://o2oapi.aladingyidong.com/TerminalSalesInfo.aspx" scroll="no" frameborder="0" width="500" height="450"></iframe></div></div><div class="clearFix_12"></div><div class="tip_leftArrow" style="top:175px;"></div></div>';

    $("body").append(iString);
}

function closeMapPointWin() {
    $("#GetPoiPop").remove();
}