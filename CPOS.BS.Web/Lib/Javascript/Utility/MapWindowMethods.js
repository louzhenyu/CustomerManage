var MapWindowMethods = { 
    //定义变量
    //创建地图对象
    var mapObject = Ext.create('Jit.window.Window', null);
    var currentLevel=1;
    var parentId=null;
    var drilledId =null;
//接收回调
    //初始化完毕
    mapObject._map_AfterInit = function () {
                                                _map_AfterInit();
                                                }
    //层跳转完毕
    mapObject._map_AfterGotoLayer = function (level,parent_id) {
                                                            AfterGotoLayer(level,parent_id);
                                                            }
    //呈现完毕
    mapObject._map_AfterRender = function (kpis,kpi_dataes) {
                                                AfterRender();
                                                }
    //钻取完毕
    mapObject._map_AfterDrill = function (from_level,to_level,drilled_id) {
                                                AfterDrill(from_level,to_level,drilled_id);
                                                }

    //地图方法
    //层跳转
    GotoLayer: function (）
    {
       mapObject._map_GotoLayer(1,null);       
    }
    //呈现地图
    Render:function()
    { 
        //KPI定义信息
        //第一个KPI为背景KPI，采用着色渲染
        var kpi1 ={
                        "Code":"9001"
                        ,"Text":"订单数"
                        ,"LegendText":"订单数（单位个）"
                        ,"DataIndex":"Sales_order_count"
                        ,”DataLabelIndex”:”Sales_order_count”  //可以与DataIndex指向同一个属性
                        ,"IsBackgroundKPI",true
                        ,"KPIStyle":2
                        ,"Thresholds":[
                        {"Type":1,"Start":0,"End":20}   //数组中第一个元素为最低档的阀值
                        ,{"Type":1,"Start":20,"End":40}
                        ,{"Type":1,"Start":40,"End":60}
                        ,{"Type":1,"Start":60,"End":80}
                        ,{"Type":1,"Start":80}	//最后一个元素为最高档的阀值
                        ]
                        };

        //第二个KPI为前景KPI，采用气泡渲染
        var kpi2 ={
                        "Code":"9002"
                        ,"Text":"订单额"
                        ,"LegendText":"订单额（单位万元）"
                        ,"DataIndex":"Sales_amount"
                        ,”DataLabelIndex”:”Sales_amount_text”
                        ,"IsBackgroundKPI",false
                        ,"KPIStyle":2
                        ,"Thresholds":[
                        {"Type":1,"Start":0,"End":100}   //数组中第一个元素为最低档的阀值
                        ,{"Type":1,"Start":100,"End":200}
                        ,{"Type":1,"Start":200,"End":300}
                        ,{"Type":1,"Start":300,"End":400}
                        ,{"Type":1,"Start":500}	//最后一个元素为最高档的阀值
                        ]
                        };

                        //KPI填充数据
                 var kpiData =[
                                {"BoundID":1,"Sales_order_count":100,"Sales_amount":50," Sales_amount_text":" ￥50万"}
                                ,{"BoundID":2,"Sales_order_count":45,"Sales_amount":27," Sales_amount_text":" ￥27万"}
                                ];
                 mapObject._map_Render([kpi1,kpi2],kpiData);
    }
    //更改地图设置
    ChangeMapSetting:function(IsShowBaseMap,IsShowLabel,IsShowKPIValue)
    {
        mapObject._map_ChangeMapSetting(IsShowBaseMap,IsShowLabel,IsShowKPIValue);
    }

    //回调处理
    //初始化完成
    AfterInit: function ()
    {
        var mapInfo=mapObject._map_GetCurrentLayerInfo();        
        alert(mapInfo.Level);
    }
    //层跳转完成
    AfterGotoLayer:function(level,parent_id)
    {
        currentLevel=level;
        parentId=parent_id;
    }
    //呈现完毕
    AfterRender:function(kpis,kpi_dataes)
    {
        alert("呈现完毕");
    }
    //钻取完毕
    AfterDrill:function(from_level,to_level,drilled_id)
    {
        currentLevel =to_level;
        drilledId =drilled_id;
    }
    //将地图导出成图片
    ExportMap:function()
    {
        mapObject._map_ExportMap();
    }
};