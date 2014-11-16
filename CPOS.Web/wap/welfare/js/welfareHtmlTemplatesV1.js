
var iWinth;
var HtmlTemplate = {


    CourseGetItemList: function (data, atype) {
        var Istring = '',
				getData = data.courseList;
        if (getData.length > 0) {
            for (var i = 0; i < getData.length; i++) {
                var iFalse = false, ies = '';
                if (i % 2 == 0 && atype == 1) {
                    iFalse = true;
                }
                if (i % 2 != 0 && atype == 2) {
                    iFalse = true;
                }
                if (iFalse == true) {
                    var itemName = getData[i].courseName;
                    itemName = (itemName.length > 14) ? itemName.substring(0, 14) + ".." : itemName;
                    if (IsIEVersion() == 0) {

                        if (getData[i].courseId == 'B852F5679F2C491E945AD4E1B6BEEBE6') {
                            Istring += '<a href="http://ceibs-eep.umaman.com/lesson/category-list/category-list" >';
                        }
                        else if (getData[i].courseId == '315BE960474D43C7B9222962389FA31A') {
                            Istring += '<a href="http://140327fg0127.umaman.com/html/m/" >';

                        } else if (getData[i].courseId == 'F84900D8325A4CC5916B811A854A25BB') {
                            Istring += '<a href="http://lightapp.cn/meeting/index/2188?channel=1" >';
                        } else if (getData[i].courseId == '8EBAACDB-3E4D-414F-A2C0-AF2A9BDD8BF1') {
                            Istring += '<a href="http://www.lightapp.cn/meeting/index/2187?channel=1" >';
                        }
                        else if (getData[i].courseId == 'A834B8BF6DEC4D1E824FE85986E6F2A8') {
                            Istring += '<a href="http://ceibs-eep.umaman.com/html/mobile/ngp/" >';
                        }
                        else if (getData[i].courseId == '6AE7C257B3EE489F8D0387D88E1716F1') {
                            Istring += '<a href="http://ceibs-eep.umaman.com/html/m/amp/" >';
                        } else if (getData[i].courseId == 'A783DE5DDE47487AA3DA06C1BA77DCCF') {
                            Istring += '<a href="http://www.lightapp.cn/meeting/index/759?channel=1" >';
                        } else if (getData[i].courseId == '943CDEC436174B5AA8C16E9286F9D167') {
                            Istring += '<a href="http://ceibs-eep.umaman.com/html/mobile/art/index.html" >';
                        } else if (getData[i].courseId == '315BE960474D43C7B9222962389FA314') {
                            Istring += '<a href="http://ceibs-eep.umaman.com/html/mobile/ime/" >';
                        } else if (getData[i].courseId == '5356975D-6A45-413F-A26A-458DCF8046DF') {
                            Istring += '<a href="http://www.lightapp.cn/meeting/index/2187?channel=1" >';
                        }
                        else if (getData[i].courseId == '4602BABF-69A0-4103-81D1-9E87E7DF609E') {
                            Istring += '<a href="http://www.lightapp.cn/meeting/index/2188?channel=1" >';
                        }
                        else if (getData[i].courseId == '0569C4A2-B4B3-4AA5-AE71-950933777AD4') {
                            Istring += '<a href="http://www.lightapp.cn/meeting/index/759?channel=1" >';
                        }
                        else {

                            Istring += '<a href="courseDetail.html?courseId=' + getData[i].courseId + '&courseTypeId=' + getParam("courseTypeId") + '">';
                        }

                    } else {
                        ies = " onclick='location.href=welfareDetail.html?courseId=" + getData[i].courseId + "&courseTypeId=" + getParam("courseTypeId") + "'";
                    }
                    Istring += '<li ' + ies + '><div class="WelfareConnextBoxLi"><img width="100%" src="' + getData[i].imageUrl + '"> </div><div class="WelfareConnextBoxLiTxt"><h2>' + itemName + '</h2></div></li>';
                    if (IsIEVersion() == 0) {
                        Istring += '</a>';
                    }
                }

            }
        }
        return Istring;

    },
    getCourseDetail: function (data) {
        var Istring = '<section class="DetailAll"><div class="DetailImg" id="wrapper"><div class="DetailImgIeo" id="scroller"><ul>',
				getDataimg = data.imageList, msc = '';


        if (getDataimg.length > 0) {
            for (var i = 0; i < getDataimg.length; i++) {
                Istring += '<li style="width:' + (Win.W() * 0.94) + 'px"><img src="' + getDataimg[i].imageUrl + '" width="' + (Win.W() * 0.94) + '" /></li>';
                if (i == 0) {
                    msc += '<dd class="active">' + (i + 1) + '</dd>';
                } else {
                    msc += '<dd>' + (i + 1) + '</dd>';
                }
            }
        }
        Istring += '</ul></div><div class="KzDGrayTOP"></div><div class="KzDGray"></div><dl id="indicator">';
        Istring += msc;
        Istring += '</dl></div>';
        Istring += '<div class="kecLi"><strong>课程简介</strong></div><div class="kecLi">' + data.courseSummary + '</div>';
        Istring += '<div class="kecLi"><strong>课程内容</strong><br>' + data.couseDesc + '</div><div class="kecLi" style="border-bottom:none;"><strong>授课师资</strong><br>' + data.couseCapital + '</div>';

        Istring += "</section>";

        //	Istring +=' <section class="DetailAll" style="margin-top:20px;"><div class="kecLi"><strong>课程费用</strong></div><div class="kecLi"<strong>￥'+data.courseFee+'</strong></div></section>';
        /*Istring +='<section class="DetailAll" style="margin-top:20px;"><div class="ApplyEventParent">  <h4>申请 <span id="Pnum" class="Pnum">1</span> 人</h4><div class="PaidPei1"><div  class="EventPersonAll"> <div class="EventPerson"><div class="EventPersonInner"><img width="75" height="75"  onload="photoSize(this,75,75)" src="http://alumniapp.ceibs.edu:8080/ceibs/FileUploadServlet/upfiles/qronghao.e08sh2_middle_1371363916136.jpg?x=1" style="margin-left: 0px;"></div><div class="EventPersonName">邱荣浩 Raymond QIU</div></div></div></section>';*/
        Istring += '<div class="CourseBtn"><ul><li><a href="courseOnline.html?courseId=' + data.courseId + '&emailId=' + data.courseId + '&courseTypeId=' + getParam("courseTypeId") + '" class="CourseBtnAA">索取申请表</a></li>'
        if (getParam("courseId") != "3") {
            Istring += '<li style="width:34%; "><a href="studentsComment.html?courseTypeId=' + getParam("courseTypeId") + '&courseId=' + data.courseId + '&back=1" class="CourseBtnAA">学员评价</a></li>';
        }
        Istring += '<li><a class="CourseBtnAA" style="margin-right:0" href="courseContact.html?courseId=' + data.courseId + '&courseTypeId=' + getParam("courseTypeId") + '">联系我们</a></li></ul></div>';
        var AppcourseName = data.courseName;
        AppcourseName = (AppcourseName.length > 10) ? (AppcourseName + "..") : AppcourseName;
        $("#Btoo").text(AppcourseName);
        return Istring;

    },
    getCoursePageDetail: function (data) {
        var Istring = '<section class="DetailAll"><div class="kecLi"><strong>' + data.courseName + '</strong><br />' + data.courseStartTime + '</div><div class="kecLi"><strong>开课介绍</strong><br>' + data.courseSummary + '</div> <div class="kecLi"><strong>课程内容</strong><br>' + data.couseDesc + '</div><div class="kecLi"><strong>授课师资</strong><br>' + data.couseCapital + '</div><div class="kecLi"><strong>联系我们：</strong><br>' + data.couseContact + '</div><div class="kecLi" style="border-bottom:none;">注:我院保留对课程信息（包括价格、日期、地点、师资、课程安排和其他细节等）进行调整的权利。</div></section>';
        return Istring;
    },
    getCourseContact: function (data) {
        var Istring = '<section class="DetailAll"><div class="kecLi"><strong>联系我们：</strong><br>' + data.couseContact + '</div></section>';
        return Istring;
    },
    newsList: function (data) {
        var Istring = "";
        var getData = data.newsList;
        if (getData != null && getData.length > 0) {
            for (var i = 0; i < getData.length; i++) {
                var getTimea = getData[i].time;

                var ies = '';
                if (i == 0) {
                    desc = "最新新闻：" + getTimea + " " + getData[i].title;
                }
                if (IsIEVersion() == 0) {
                    Istring += '<a href="newsDetail.html?newsId=' + getData[i].newsId + '&courseTypeId=' + getParam("courseTypeId") + '">';

                } else {
                    ies = " onclick='location.href=newsDetail.html?newsId=" + getData[i].newsId;
                }
                Istring += '<div class="DetailSmallBox" ' + ies + '><div class="DetailSmallBoxYW" style="padding:12px 2% 6px 2%;"><p class="newsListp1" style="font-size:12px; padding-top:5px;">' + getTimea + '</p><p class="newsListp2">' + getData[i].title + '</p><span class="blank1"></span> </div><span style="top:25px;" class="Jro"></span></div>';
                if (IsIEVersion() > 0) {
                    Istring += "</a>";
                }
            }
        }
        return Istring;
    },
    newsDetailData: function (data) {
        var Istring = '<div class="DetailSmallBox newsDetailBox"><div style="padding:12px 2% 6px 2%;" class="DetailSmallBoxYW"><p class="newsListp2" style="margin-left:0px; font-weight:bold;">' + data.title + '</p><span style="font-size:12px;">' + data.time + '</span><span class="blank1"></span></div><div class="DetailImg" id="wrapper"><div class="DetailImgIeo" id="scroller"><ul>',
				getDataimg = data.imageList, msc = '';
        if (getDataimg.length > 0) {
            for (var i = 0; i < getDataimg.length; i++) {
                Istring += '<li style="width:' + (Win.W() * 0.94) + 'px"><img src="' + getDataimg[i].imageURL + '" width="' + (Win.W() * 0.94) + '" /></li>';
                if (i == 0) {
                    msc += '<dd class="active">' + (i + 1) + '</dd>';
                    if (getDataimg[i].imageURL != "") {
                        imgUrl = getDataimg[i].imageURL;
                    }
                } else {
                    msc += '<dd>' + (i + 1) + '</dd>';
                }
            }

            Istring += '</ul></div><div class="KzDGrayTOP"></div><div class="KzDGray"></div><dl id="indicator">';
            Istring += msc;
            Istring += '</dl></div>';
        }
        Istring += '<div class="newsDetailDescription">' + data.description + '</div></div>';
        return Istring;
    },
    bbsListData: function (data) {

        var Istring = "";
        var getData = data.forumList;
        if (getData != null && getData.length > 0) {
            for (var i = 0; i < getData.length; i++) {
                if (i == 0) {
                    desc = "最新活动：" + getData[i].beginTime + "  " + getData[i].title;
                }
                var ies = '';
                if (IsIEVersion() == 0) {

                    Istring += '<a href="consultDetail.html?forumId=' + getData[i].forumId + '&forumTypeId=' + getParam("forumTypeId") + '">';
                } else {
                    ies = " onclick='location.href=consultDetail.html?forumId=" + getData[i].forumId;
                }
                Istring += '<div class="DetailSmallBoxYW DetailBBSList"><span class="spanDDD">■</span><h1>' + getData[i].title + '</h1><span class="BBSTime">' + getData[i].beginTime + '</span><span class="Jro" style="top:25px;"></span></div>';
                if (IsIEVersion() > 0) {
                    Istring += "</a>";
                }
            }
        }
        return Istring;
    },
    consultList: function (data) {
        var Istring = "";
        var getData = data.forumList;
        if (getData != null && getData.length > 0) {
            for (var i = 0; i < getData.length; i++) {
                if (i == 0) {
                    desc = getData[i].beginTime + "  " + getData[i].title;
                }
                var ies = '';
                if (IsIEVersion() == 0) {

                    Istring += '<a href="consultDetail.html?forumId=' + getData[i].forumId + '&forumTypeId=' + getParam("forumTypeId") + '">';
                } else {
                    ies = " onclick='location.href=consultDetail.html?forumId=" + getData[i].forumId + "&forumTypeId=" + getParam("forumTypeId");
                }
                Istring += '<div class="DetailSmallBoxYW DetailBBSList"><span class="spanDDD">■</span><h1>' + getData[i].title + '</h1><span class="BBSTime">' + getData[i].beginTime + '</span><span class="Jro" style="top:25px;"></span></div>';
                if (IsIEVersion() > 0) {
                    Istring += "</a>";
                }
            }
        }
        return Istring;
    },
    CeisNewList: function (data) {
        var Istring = "";
        var getData = data.zoNewsList;
        if (getData != null && getData.length > 0) {
            for (var i = 0; i < getData.length; i++) {

                var ies = '';


                Istring += '<a href="ceisDetail.html?textId=' + getData[i].textId + '&typeId=' + getParam("typeId") + '">';

                Istring += '<div class="DetailSmallBoxYW DetailBBSList"><span class="spanDDD">■</span><h1>' + getData[i].title + '</h1><span class="Jro" style="top:25px;"></span></div>';

                Istring += "</a>";

            }
        }
        return Istring;
    },
    SudentsComment: function (data) {
        var Istring = "";
        var getData = data.courseReflectionsList;

        if (getData != null && getData.length > 0) {
            for (var i = 0; i < getData.length; i++) {

                Istring += '<div class="DetailSmallBox DetailStudents"><div  class="DetailSmallBoxYW"><h5>' + getData[i].studentPost + '  ' + getData[i].studentName + '</h5>';
                //alert(getData[i].imageURL!=undefined)

                if (getData[i].imageURL != "" || getData[i].videoURL != "") {
                    Istring += '<div class="StudentsCommentImgDiv">';
                    if (getData[i].videoURL != "") {
                        Istring += '<video id="example_video_1" class="video-js vjs-default-skin" controls preload="none" width="100%" height="140" poster="' + getData[i].imageURL + '" data-setup="{}"><source src="' + getData[i].videoURL + '" type="video/mp4" /><track kind="captions" src="videojs/demo.captions.vtt" srclang="en" label="English"></track></video>';
                    } else {
                        Istring += '<img src="' + getData[i].imageURL + '" width="100%">';
                    }
                    Istring += '</div>';
                }
                Istring += '<p class="StudentsDescription">' + getData[i].content + '</p></div></div>';
            }
        } else {
            Istring += "<div align=center>没有数据</div>";
        }
        return Istring;
    }
}
var isfshare = false;
function loadTechnicalSupport(formWhere) {
    $("#AppendFooter").remove();
    var wd = IsPC() ? 320 : $(window).width();
    $("body").append("<div class='footerBox' style='width:" + wd + "px' id='AppendFooter'><div class='fenxiang' onclick='ShareSHow()'><div class='imghjd'><img src='images/follow.png' width='120' /></div></div><div class='downloadUrl'><a href='http://alumniapp.ceibs.edu:8080/ceibs/app/mobile/download.html'><img src='images/downloadapp.jpg' width='100%' /></a></div></div>");
    if (isfshare == false) {
        isfshare = true;

        $("body").append('<div style=" bottom: 0px; position:fixed; left:0; width:90%; z-index:999; text-align:center;  background-color: #333333;   opacity: 0.95; display:none;" class="share" id="shareBox"><table border="0" align="center" style="margin-left:auto; margin-right:auto"><tr><td><a onclick="TipsFollow(this)" href="javascript:void(0)"  style="margin-left:0"> <img src="images/sendpyicn.png" width="52"></a><a  href="javascript:void(0)"  onclick="TipsFollow(this)" ><img src="images/sendpyic.png" width="52"></a><a  href="javascript:void(0)" id="sina" > <img src="images/weiboc.png" width="52"></a><a href="javascript:void(0)" id="qqWeibo"><img src="images/qqweiboc.png" width="52"></a></td></tr></table><div class="Inputbtnpp" onClick="hideBox()" style=" background-color:#f8f8f8; color:#000; margin-top:10px;">取消</div></div><div class="AppOpacity" id="AppOpacity" onclick="hidecc()"></div><div class="AppTipsFollow" id="AppTipsFollow" onclick="hidecc()"><img src="images/tipFoo.png" width="100%"  /></div>')


    }
}
function hidecc() {
    $("#AppOpacity").hide();
    $("#AppTipsFollow").hide();
}
function TipsFollow(o) {
    $(document).scrollTop(0);
    var getDocH = $(document).height();
    $("#AppOpacity").css({ "height": getDocH }).show();
    $("#AppTipsFollow").show();
}
function ShareSHow() {
    $("#shareBox").show();


    $("#sina").click(function () { var sinaurl = "http://service.weibo.com/share/share.php?title=" + title + "&url=" + encodeURIComponent(document.URL); location.href = sinaurl });



    $("#qqWeibo").click(function () { var qqWeibo = 'http://share.v.t.qq.com/index.php?c=share&a=index&title=' + title + '&url=' + encodeURIComponent(document.URL); location.href = qqWeibo });

}
function hideBox() {
    $("#shareBox").hide();
}