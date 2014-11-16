Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.HandlerUrl.setValue("Handler/EventListHandler.ashx?mid=" + __mid);

    var MarketEventId = getUrlParam("id");
    //MarketEventId = "41D86F55613649539C480E697ADA9EBB";
    //alert(MarketEventID)

    fnSearch(MarketEventId);
    //////////////////////////////////////////////////////////////////////// 动态生成的图表数据
    window.generateData = function (n, floor) {
        var data = [],
            p = (Math.random() * 11) + 1,
            i;

        floor = (!floor && floor !== 0) ? 20 : floor;

        for (i = 0; i < (n || 12); i++) {
            data.push({
                name: Ext.Date.monthNames[i % 12],
                data1: Math.floor(Math.max((Math.random() * 1000), floor)),
                data2: Math.floor(Math.max((Math.random() * 1000), floor)),
                data3: Math.floor(Math.max((Math.random() * 1000), floor)),
                data4: Math.floor(Math.max((Math.random() * 1000), floor)),
                data5: Math.floor(Math.max((Math.random() * 1000), floor)),
                data6: Math.floor(Math.max((Math.random() * 1000), floor)),
                data7: Math.floor(Math.max((Math.random() * 1000), floor)),
                data8: Math.floor(Math.max((Math.random() * 1000), floor)),
                data9: Math.floor(Math.max((Math.random() * 1000), floor))
            });
        }
        return data;
    };
    window.store1 = Ext.create('Ext.data.JsonStore', {
        fields: ['name', 'data1', 'data2', 'data3', 'data4', 'data5', 'data6', 'data7', 'data9', 'data9'],
        data: generateData()
    });
    //////////////////////////////////////////////////////////////////////////////////////////
    var chart = Ext.create('Ext.chart.Chart', {
        id: 'chartCmp',
        xtype: 'chart',
        style: 'background:#fff',
        animate: true,
        shadow: true,
        store: store1,
        axes: [{
            type: 'Numeric',
            position: 'left',
            fields: ['data1'],
            label: {
                renderer: Ext.util.Format.numberRenderer('0,0')
            },
            title: 'Number of Hits',
            grid: true,
            minimum: 0
        }, {
            type: 'Category',
            position: 'bottom',
            fields: ['name'],
            title: 'Month of the Year'
        }],
        series: [{
            type: 'column',
            axis: 'left',
            highlight: true,
            tips: {
                trackMouse: true,
                width: 140,
                height: 28,
                renderer: function (storeItem, item) {
                    this.setTitle(storeItem.get('name') + ': ' + storeItem.get('data1') + ' $');
                }
            },
            label: {
                display: 'insideEnd',
                'text-anchor': 'middle',
                field: 'data1',
                renderer: Ext.util.Format.numberRenderer('0'),
                orientation: 'vertical',
                color: '#333'
            },
            xField: 'name',
            yField: 'data1'
        }]
    });


    var win = Ext.create('Ext.form.Panel', {
        width: 800,
        height: 450,
        minHeight: 400,
        minWidth: 550,
        hidden: false,
        maximizable: true,
        title: '图标说明',
        renderTo: 'divPanel', 
        layout: 'fit',
        tbar: [{
        //text: 'Reload Data',
        handler: function () {
            store1.loadData(generateData());
            }
        }],
    items: chart
    });
    ///////////////////////////////////////////////////////////////////////////////////////////
});

function fnSearch(MarketEventId) {
    //debugger;
    Ext.getStore("eventAnalysisStore").proxy.url = "Handler/EventListHandler.ashx?mid=&method=eventAnalysis_query&eventId=" + MarketEventId;
    Ext.getStore("eventAnalysisStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventAnalysisStore").proxy.extraParams = {
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("eventAnalysisStore").load();
}

