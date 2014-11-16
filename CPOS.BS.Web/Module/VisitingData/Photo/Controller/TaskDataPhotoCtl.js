var btncode = "search";
var page = 1;
var limit = 50;
var html = "";
var pResult="";
var _scrollTimer;
var _pageCountSum=0;
var __pVisitingTaskStepID = JITMethod.getUrlParam("VisitingTaskStepID");
var __pVisitingTaskID = JITMethod.getUrlParam("VisitingTaskID");
var __pIsInOutPic = JITMethod.getUrlParam("IsInOutPic");
var __pVisitingTaskName = unescape(JITMethod.getUrlParam("VisitingTaskName"));
var __pStepName = unescape(JITMethod.getUrlParam("StepName"))
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/TaskDataPhotoHandler.ashx?mid=" + __mid);

    Ext.getStore("taskStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetTaskList";
    Ext.getStore("taskStore").load({ callback: function () {
        Ext.getStore("taskStore").insert(0, {           
            "VisitingTaskName": "--请选择--"
        });
    } 
    });

   // Ext.getCmp("DateFrom").jitSetValue(new Date());
   // Ext.getCmp("DateTo").jitSetValue(new Date());
    loadImg({ page: 1, handler: function (value) {
        $('#container').html(value);
    }
    });

    //    $(window).bind('scroll', function () {
    //        clearTimeout(_scrollTimer);
    //        _scrollTimer = setTimeout(function () {
    //            if ($(document).scrollTop() >= ($(document).height() - $(window).height() - 100)) {

    ////                loadImg({ page: page + 1, handler: function () {
    ////                    page++;
    ////                    fnRender(html);                 
    ////                }
    ////                });
    //            }



    ////            if ($(document).scrollTop() >= $(window).height()) {
    ////                $(".gotop").show();
    ////            }
    ////            else {
    ////                $(".gotop").hide();
    ////            }

    //        }, 300);
    //    });
});

function fnBack() {
    document.location.href = '/Module/VisitingData/File/TaskDataFile.aspx?mid=' + __mid;
}

/*下一页数据*/
function fnNextPage() {
    page = page + 1;
    fnLoadImg({ page: page, handler: function (value) {
        fnRender(value);
    }
    });
}

/*导出功能*/
function fnExport() {
    if (_pageCountSum > 0) {
        Ext.MessageBox.confirm("提示信息", "当前共计" + _pageCountSum + "张照片,确定导出？", function (btn) {
            if (btn == 'yes') {
                var ClientStructureID = Ext.getCmp("ClientStructureID").jitGetValue();
                var ClientPositionID = Ext.getCmp("ClientPositionID").jitGetValue();
                //                var VisitingTaskID = Ext.getCmp("VisitingTaskID").getValue();
                //                var VisitingTaskStepID = Ext.getCmp("VisitingTaskStepID").getValue();
                var IsInOutPic = __pIsInOutPic;
                var VisitingTaskID = __pVisitingTaskID;
                var VisitingTaskStepID = __pVisitingTaskStepID;
                var DateFrom = Ext.getCmp("DateFrom").jitGetValue();
                var DateTo = Ext.getCmp("DateTo").jitGetValue();
                var ObjectName = escape(Ext.getCmp("txt_VisitingTaskUserName").jitGetValue());
                var POPName = escape(Ext.getCmp("txt_ClientStoreName").jitGetValue());
                var ClientUserName = escape(Ext.getCmp("txt_ClientUserName").jitGetValue());
                window.open("Handler/TaskDataPhotoHandler.ashx?mid=" + __mid + "&btncode=export&method=ExportVisitingTaskPhoto&r=" + Math.random() +
                 "&ClientStructureID=" + ClientStructureID + "&ClientPositionID=" + ClientPositionID + "&VisitingTaskID=" + VisitingTaskID + "&VisitingTaskStepID=" + VisitingTaskStepID
                 + "&DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&ObjectName=" + ObjectName + "&POPName=" + POPName + "&ClientUserName=" + ClientUserName + "&IsInOutPic=" + IsInOutPic);
            }
        });
    } else {
        Ext.Msg.alert("提示", "当前没有照片可导出");
    }
}

/*查询照片*/
function fnSearch() {
    var form = Ext.getCmp("searchPanel").getForm();
    if (!form.isValid()) {
        return false;
    }
    $("#td_load").html("图片加载中...");
    $('#container').html("");
    page = 1;

    loadImg({ page: 1,limit:limit, handler: function (value) {
        $('#container').html(value);        
     }
    });
}


/*
@page 页码 handler 分页完成回调函数
*/
function loadImg(args) {
    $.post("Handler/TaskDataPhotoHandler.ashx?mid=" + __mid + "&btncode=search&method=GetVisitingTaskPhoto&r=" + Math.random(),
                {
                    page: args.page,
                    ClientStructureID: Ext.getCmp("ClientStructureID").jitGetValue(),
                    ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue(),
                 //   VisitingTaskID: Ext.getCmp("VisitingTaskID").getValue(),
                    // VisitingTaskStepID: Ext.getCmp("VisitingTaskStepID").getValue(),
                    VisitingTaskID: __pVisitingTaskID,
                    VisitingTaskStepID: __pVisitingTaskStepID,
                    IsInOutPic: __pIsInOutPic,
                    DateFrom: Ext.getCmp("DateFrom").jitGetValue(),
                    DateTo: Ext.getCmp("DateTo").jitGetValue(),
                    ObjectName: escape(Ext.getCmp("txt_VisitingTaskUserName").jitGetValue()),
                    POPName: escape(Ext.getCmp("txt_ClientStoreName").jitGetValue()),
                    ClientUserName: escape(Ext.getCmp("txt_ClientUserName").jitGetValue())
                },
                function (result) {
                    if (result != "") {
                        html = '';
                        pResult = result
                        fnLoadImg(args);
                    }
                    else {
                        $("#td_load").html("没有更多图片");
                    }
                });
}

function fnLoadImg(args) {
    var obj = eval(pResult);
    jdata = obj[0].topics;
    var photoLength = limit * page;
    if (jdata.length < photoLength) {
        photoLength = jdata.length;
    }
    var nextPageHtml = "";
    for (var i = (page - 1) * limit; i < photoLength; i++) {
        //var pics = __imgpath + __clientid + "/" + jdata[i].ClientUserID + "/" + jdata[i].PhotoUrl;
        var pics =  "/" + jdata[i].ClientUserID + "/" + jdata[i].PhotoUrl;
        if (pics != null && pics != undefined && pics.length > 0) {
            nextPageHtml += '<div class="cell" > <a href="' + pics + '" title="'
                                + jdata[i].POPName + "&"
                                + jdata[i].TaskName + "&"
                                + jdata[i].StepName + "&"
                                + jdata[i].PositionName + "&"
                                + jdata[i].ClientUserName + "&"
                                //+ JITMethod.getDateTime(jdata[i].PhotoDateTime) + "&"
                                + jdata[i].ObjectName
                                + '" rel="fancybox_group"><div class="celldiv"><img data-fancybox-group="thumb" alt="' + jdata[i].POPName + '" src="' + pics + '" /></div><p><div style="height:45px;overflow:hidden;  text-align:left;font-weight:bold;">' + jdata[i].POPName + '</div><div style="float:left;">' + jdata[i].PositionName + ':' + jdata[i].ClientUserName + '</div><div style="float:right;">' + jdata[i].PhotoName + '</div></p></a></div>';
        }
        else {
            nextPageHtml += '<div class="cell" > <a href="/Framework/Image/graphics/no_picture.jpg" title="'
                                + jdata[i].POPName + "&"
                                + jdata[i].TaskName + "&"
                                + jdata[i].StepName + "&"
                                + jdata[i].PositionName + "&"
                                + jdata[i].ClientUserName + "&"
                                //+ JITMethod.getDateTime(jdata[i].PhotoDateTime) + "&"
                                + jdata[i].ObjectName
                                + '" rel="fancybox_group"><div class="celldiv"><img alt="' + jdata[i].POPName + '" src="/Framework/Image/graphics/no_picture.jpg" /></div><p><div style="height:45px;overflow:hidden;text-align:left;font-weight:bold;">' + jdata[i].POPName + '</div><div style="float:left;">' + jdata[i].PositionName + ':' + jdata[i].ClientUserName + '</div><div style="float:right;">' + jdata[i].PhotoName + '</div></p></a></div>';
        }
    }
    //html += nextPageHtml;
    args.handler(nextPageHtml);
    setTimeout(function () { setImg(); }, 100);
    _pageCountSum = obj[0].totalCount;
    Ext.getCmp("lbl_Count").setText("拜访任务：" + __pVisitingTaskName + "；拜访步骤：" + __pStepName + "；当前照片 总数量:" + obj[0].totalCount + "张;显示数量:" +
                        $("a[rel=fancybox_group]").length
                        + "张");
    if (obj[0].totalCount <= page * limit) {

        $("#td_load").html("没有更多图片");
    } else {
        if (page > 20) {
            $("#td_load").html("最多显示1000张图片");
        } else {
            $("#td_load").html("<a href='javascript:fnNextPage()'>查看更多图片</a>");
        }
    }
}

/*加载图片*/
function fnRender(elements) {
    if (!$(elements).length) return;
    $(elements).each(function (i) {
        $(elements).eq(i).css('opacity', 0).appendTo($("#container")).fadeTo(600, 1);
    });
}

/*重置*/
function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
    Ext.getCmp("ClientStructureID").jitSetValue("");
    Ext.getCmp("ClientPositionID").jitSetValue(""); 
    Ext.getCmp("VisitingTaskID").jitSetValue("");
    Ext.getCmp("VisitingTaskStepID").jitSetValue("");
    Ext.getCmp("DateFrom").jitSetValue(new Date());
    Ext.getCmp("DateTo").jitSetValue(new Date());
}

/*拜访任务改变*/
function fnTaskChange() {
    Ext.getStore("taskStepStore").removeAll();
    Ext.getCmp("VisitingTaskStepID").setValue("");

    Ext.getStore("taskStepStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetStepByTID&tid=" + this.getValue();
    Ext.getStore("taskStepStore").load({ callback: function () {
        Ext.getStore("taskStepStore").insert(0, {          
            "StepName": "--请选择--"
        });
    }
    });
}

/*图片加载fancybox*/
function setImg() {
    if ($("a[rel=fancybox_group]").length == 0) {
        return;
    }

    $("a[rel=fancybox_group]").fancybox({
        'transitionIn': 'none',
        'transitionOut': 'none',
        'titlePosition': 'over', 
        autoScale: true,
        'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
            if (title != null && title != "" && title.length > 0) {
                return '<span id="fancybox-title-over" style="height:90px;width:710px;"> '
                + ' <span style="float:left;"><font style="font-weight:bold;"> ' + title.split('&')[0] + '</font><br/>' + title.split('&')[3] + ':' + title.split('&')[4] + '<br>' + title.split('&')[1] + '<br/>' + title.split('&')[6] + '</span>'
                + ' <span style="float:right;">' + (currentIndex + 1) + ' / ' + currentArray.length + '<br/>' + title.split('&')[5] + '<br>' + title.split('&')[2] + '</span>'
                  + '</span>';
            }
            return "";
        }
    });
}

