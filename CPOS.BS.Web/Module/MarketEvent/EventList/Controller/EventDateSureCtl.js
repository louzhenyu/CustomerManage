Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require([
    'Ext.selection.CellModel',
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

Ext.onReady(function () {
    Ext.QuickTips.init();
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();
    fnSearch();
    var loadMarsk = new Ext.LoadMask('Eventconnext', {
        msg: "正在操作中，请稍候...",
        removeMask: true
    })
    Ext.get("ResetDataSure").on("click", function (e) {
        fnReset()
    })
    Ext.get("SubmitDataSure").on("click", function (e) {
        fnSubmit(loadMarsk)
    })
});


function fnSearch() {
    //debugger;
    Ext.getStore("EventDateSureStrone").proxy.url = "Handler/EventListHandler.ashx?mid=&method=waveBand_query&eventId=" + eventId + "&page=1";
    Ext.getStore("EventDateSureStrone").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("EventDateSureStrone").proxy.extraParams = {
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("EventDateSureStrone").load();

}

function fnReset() {
    var $savetr = $("#gridView .x-grid-table tr");
    $savetr.each(function (i) {
        $(this).find("td").eq(2).find("div").text('');
        $(this).find("td").eq(4).find("div").text('');
    })
    //Ext.getCmp("txtEndDate").setValue("");
}

function fnSubmit(loadMarsk) {

    var getJsonStringData = getSubmitData();
    var FdSubmit = {};
    if (getJsonStringData != false) {
       FdSubmit.MarketEventID = eventId;
       FdSubmit.MarketWaveBandInfoList = getJsonStringData;
        loadMarsk.show();

        Ext.Ajax.request({
            url: "Handler/EventListHandler.ashx?mid=&method=waveBand_save",
            params: { data: JSON.stringify(FdSubmit) },
            method: 'POST',
            success: function (response) {
                loadMarsk.hide();
                var eventRunData = eval("(" + response.responseText + ")");
                Ext.Msg.alert("提示", eventRunData.msg);
               

            },
            failure: function () {
                loadMarsk.hide();
                Ext.Msg.alert("提示", "保存数据失败");
            }
        });
        }

}
function getSubmitData() {
    var $savetr = $("#gridView .x-grid-table tr");
    if ($savetr.length > 1) {
        var saveFata = [];
        $savetr.each(function (i) {
            if (i != 0) {
                var ist = {};
                ist.FactBeginTime = $(this).find("td").eq(2).find("div").text();
                ist.FactEndTime = $(this).find("td").eq(4).find("div").text();
                ist.WaveBandID = $(this).find("td").eq(5).find("div").text();
                ist.MarketEventID = eventId;
                saveFata.push(ist);
            }
        })
         return saveFata; 
    }
    return false; 
}