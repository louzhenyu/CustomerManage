var btncode = "search";

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();
    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/TaskDataFileHandler.ashx?mid=" + __mid);
    fnSearch();
});

/*查询照片*/
function fnSearch() {
    Ext.Ajax.request({
        url: "Handler/TaskDataFileHandler.ashx?mid=" + __mid + "&btncode=search&method=GetVisitingTaskStepSum",
        params: {},
        method: 'post',
        success: function (response) {
            try {
                var jdata = eval(response.responseText);
                var nextPageHtml = "";
                for (var i = 0; i < jdata[0].topics.length; i++) {
                    var p = jdata[0].topics[i];
                    nextPageHtml += ' <div class="cell"><a  href="/Module/VisitingData/photo/TaskDataphoto.aspx?mid='
                                        + __mid + '&VisitingTaskID='
                                        + p.VisitingTaskID + '&VisitingTaskStepID='
                                        + p.VisitingTaskStepID + '&IsInOutPic='
                                        + p.IsInOutPic + '&VisitingTaskName='
                                        + escape(p.VisitingTaskName) + '&StepName='
                                        + escape(p.StepName) + '" title="拜访任务：'
                                        + p.VisitingTaskName + '；拜访步骤：'
                                         + p.StepName + '；照片数量：'
                                          + p.PicSum 
                                        + '张"><div class="celldiv"><img src="/Framework/Image/file.jpg" /></div><p>拜访任务：'
                                        + p.VisitingTaskName + ''
                                        + '<br/>拜访步骤：' + p.StepName
                                        + '<br/>照片数量：' + p.PicSum + '张</p></a></div>';
                }
                document.getElementById("container").innerHTML = nextPageHtml;
            } catch (e) {
         
            }
        },
        failure: function () {
            //Ext.Msg.alert("提示", "验证数据失败");
        }
    });
}
