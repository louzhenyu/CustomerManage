var YangtzeHandle = {
    dataList: [], //期刊列表，首页进入时赋值
    dataInfo: '', //选择期刊 默认选择第一期
    keyDataList: 'keydataListv1',
    keyDataInfo: 'keydataInfov1',
    getDataList: function() {
        return JitPage.getParams(this.keyDataList);
    },
    getDataInfo: function() {
        return JitPage.getParams(this.keyDataInfo);
    },
    initShareHande: function() {
        var self = this;
        $('.praise').unbind();
        $('.praise').click(function() {
            var element = $(this),
                microID = element.parent().attr('id');
            self.addMicroStats(microID, 'PraiseCount', function(count) {
                Jit.UI.Dialog({
                    'content': "提交成功，感谢您的配合。",
                    'type': 'Alert',
                    'CallBackOk': function() {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                element.text(count);
            });
        });
        $('.share').unbind();
        $('.share').click(function() {
            var element = $(this),
                microID = element.parent().attr('id');
            $('.footTipShare').remove();
            var shareBox = $("<div class=\"footTipShare\" style=\"background: none repeat scroll 0px 0px rgba(0, 0, 0, 0.5); height: 100%; left: 0px; position: fixed; top: 0px; width: 100%; z-index: 200; display: block;\"><div><img src=\"../../../images/public/xiehuibao/tipFoo.png\"></div></div>");
            $('body').append(shareBox);
            shareBox.click(function() {
                $('.footTipShare').remove();
            });
            self.addMicroStats(microID, 'CollCount', function(count) {
                element.text(count);
            });
        });
    },
    addMicroStats: function(newsId, field, callback) {
        Jit.AM.ajax({
            url: '/ApplicationInterface/Product/Eclub/Module/MicroIssueHandler.ashx',
            interfaceMode: 'V2.0',
            data: {
                action: 'MicroNewsCollSet',
                NewsId: newsId,
                Field: field
            },
            success: function(result) {
                if (result && result.ResultCode == 0) {
                    callback(result.Data.Count || 0);
                };
            }
        });
    }
}

$(function() {
    //设置微信分享
    setTimeout(function() {
        var curDataInfo = YangtzeHandle.getDataInfo(),
            serial = 3,
            shareImage = "http://bs.aladingyidong.com//Framework/Javascript/Other/kindeditor/asp.net/../attached/image/20140812/20140812142510_2935.jpg";

        //正式9月20号之前的缓存 用于访问过有缓存的机子,测试服务器请删除 
        // var serCustomerId = "b0b946bd761c47898eaea4636cd45151";
        // var baseInfo = Jit.AM.getBaseAjaxParam(),
        //     urlCustomerId = Jit.AM.getUrlParam('customerId');
        // if (baseInfo && baseInfo.customerId != serCustomerId || urlCustomerId != serCustomerId) {
        //     localStorage.clear();
        //     alert('连接已被重置，请重新打开页面!');
        //     return false;
        // };

        if (curDataInfo && curDataInfo.MicroNumber) {
            serial = curDataInfo.MicroNumber;
            if (curDataInfo.ShareIcon) {
                shareImage = curDataInfo.ShareIcon;
            };
            WeiXinShare.title = "长江EMBA-V刊第" + serial + "期";
            WeiXinShare.imageUrl = shareImage;
            WeiXinShare.desc = curDataInfo.MicroNumberName;
            WeiXinShare.link = "http://" + location.host + "/HtmlApps/html/special/yangtze/home.html?customerId=b0b946bd761c47898eaea4636cd45151&toMicroNumberID=" + curDataInfo.MicroNumberID + "&version=" + (new Date().getTime());
        };

    }, 400);
})