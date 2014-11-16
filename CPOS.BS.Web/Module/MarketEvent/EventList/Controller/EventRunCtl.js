Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    var tpl = new Ext.Template(
        ' <div class="eventRunDetailContext"><b>活动代码</b>：{EventCode} （{StatusDesc}）</div>',
        '<div class="eventRunDetailContext"><b>预算总费用</b>：{BudgetTotal}</div>',
        '<div class="eventRunDetailContext"><b>品牌</b>：{BrandName}</div>',
        '<div class="eventRunDetailContext"><b>活动时间</b>：{BeginTime} 至 {EndTime}&nbsp;&nbsp;[<a href="#" class="pointer z_col_light_text">波段</a>]</div>',
        '<div class="eventRunDetailContext"><b>活动类型</b>：{EventType}</div>',
        '<div class="eventRunDetailContext"><b>参与门店</b>：{StoreCount}家&nbsp;&nbsp;[<a href="#" class="pointer z_col_light_text">门店</a>]</div>',
        '<div class="eventRunDetailContext"><b>活动方式</b>：{EventModeDesc}</div>',
        '<div class="eventRunDetailContext"><b>邀约人群</b>：{PersonCount} 人&nbsp;&nbsp;[<a href="#" class="pointer z_col_light_text">人群</a>]</div>',
        '<div class="eventRunDetailText"><b>活动描述</b>：<br />{EventDesc}</div>',
        '<div class="eventRunDetailText"><b>邀约内容</b>：<br />{TemplateContent}</div>'
    );
    var EventRunData;
    LoadRunData();
    function LoadRunData() {
        Ext.Ajax.request({
            url: "Handler/EventListHandler.ashx?method=eventInfo&eventId=" + eventId,
            success: function (request) {
                EventRunData = request.responseText;
                EventRunData = eval("(" + EventRunData + ")");
                EventRunDataObj = EventRunData.topics
                tpl.compile();
                tpl.overwrite(Ext.get("EventRunDetail"), EventRunDataObj);
                Ext.get("sendBtnAtt").show();
                if (EventRunDataObj.EventStatus == "2") {
                    $("#runSendGray").show();
                    $("#runSend").hide();
                } else {
                    $("#runSendGray").hide();
                    $("#runSend").show();
                }

            }

        })
    }
    Ext.Ajax.request({
        url: "Handler/EventListHandler.ashx?method=eventPerson_query&eventId=" + eventId,
        success: function (request) {
            var cEventRunData = request.responseText;

            cEventRunData = eval("(" + cEventRunData + ")");
            if (cEventRunData.success == true) {
                $("#SendPersonNum").text(cEventRunData.msg);
                $("#SendPersonNumGray").text(cEventRunData.msg);
            } else {
                $("#runSend").hide();
            }
        }

    })
    var loadMarsk = new Ext.LoadMask('EventRunDetail', {
        msg: "正在操作中，请稍候...",
        removeMask: true
    })
    //测试发送
    Ext.get("testSend").on("click", function (e) {
        loadMarsk.show();
        Ext.Ajax.request({
            url: "Handler/EventListHandler.ashx?method=testSend&eventId=" + eventId,
            success: function (request) {
                EventRunData = request.responseText;
                EventRunData = eval("(" + EventRunData + ")");
                loadMarsk.hide();
                if (EventRunData.success == true) {
                    Ext.MessageBox.show({
                        title: "提示",
                        msg: EventRunData.msg,
                        width: 250,
                        buttons: Ext.MessageBox.OK
                    })
                }
            }
        })
    })

    Ext.get("runSend").on("click", function (e) {
        loadMarsk.show();
        Ext.Ajax.request({
            url: "Handler/EventListHandler.ashx?method=runSend&eventId=" + eventId,
            success: function (request) {
                EventRunData = request.responseText;
                EventRunData = eval("(" + EventRunData + ")");
                loadMarsk.hide();
                if (EventRunData.success == true) {
                    Ext.MessageBox.show({
                        title: "提示",
                        msg: EventRunData.msg,
                        width: 250,
                        buttons: Ext.MessageBox.OK
                    })
                    LoadRunData();
                }
            }
        })
    })

});
