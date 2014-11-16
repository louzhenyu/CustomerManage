var CommonMethod = {
    /*
    获取Url中的QueryString
    @pParamName 参数名
    return      返回参数值,如果没有相应的参数则返回null
    */
    getUrlParam: function (pParamName) {
        var strUrl, index, strParas, paraItems;
        strUrl = window.location.href;
        index = strUrl.indexOf('?');
        strParas = strUrl.substring(index + 1);
        paraItems = strTemp.split('&');
        if (paraItems != null && paraItems.length > 0) {
            for (var i = 0; i < paraItems.length; i++) {
                var temp = paraItems[i];
                index = temp.indexOf('=');
                if (temp.substring(0, index) == pParamName) {
                    return temp.substring(index + 1).replace('#', '');
                }
            }
        }
        //
        return null;
    }
    /*
    解析照片对象,获取照片的Url
    @pBasePath  网站的根url,例如：http://report.jitmarketing.cn:9021/
    @pClientID  当前的客户ID
    @pUserID    拍摄照片的用户的用户ID
    @pPicJSON   照片对象的JSON.
    return      返回照片Url数组
    */
    , getPictures: function (pBasePath, pClientID, pUserID, pPicJSON) {
        var photoUrls = new Array();
        if (picJSON != null && picJSON != '') {
            try {
                var photoDatas = eval(picJSON);
                if (!(photoDatas.length != null && photoDatas.length > 0)) {
                    photoDatas = eval("[" + picJSON + "]");
                }
                for (var i = 0; i < photoDatas.length; i++) {
                    var url = '';
                    var photoData = photoDatas[i];
                    if (photoData.FileName != null && photoData.FileName != '') {
                        url = pBasePath + pClientID + "/" + pUserID + "/" + photoData.FileName;
                    } else {
                        url = "/Lib/Image/null_picture.jpg";
                    }
                    photoUrls.push(url);
                }
            } catch (e) {
                
            }
        }
        return photoUrls;
    }

    //绑定下拉列表
    , bindSelect: function (sel, data, idField, nameField) {
        var list = [];
        for (var i = 0; i < data.length; i++) {
            list.push({ id: eval("data[i]." + idField), name: eval("data[i]." + nameField) });
        }

        var temp = $("#selectTemp").html();
        sel.html(self.renderSelect(temp, { list: list, idprefix: "dept-item" }));
    }

    //下拉选项选中事件
    , selectedEvent: function (target) {
        $(target).delegate(".selectBox p", "click", function (e) {
            //获得当前元素jquery对象
            var $t = $(this);
            //获得选择内容的id
            var optionId = $t.data("optionid");
            //改变显示的内容  及设置id
            $t.parent().parent().find(".text").html($t.html());
            $t.parent().parent().find(".text").attr("optionid", optionId);
            $t.parent().hide();
            $t.stopBubble(e);
        }).delegate("[name=vipinfo]", "click", function (e) {  //针对tree进行的事件
            var $t = $(this);
            var forminfo = $t.data("forminfo");
            if (forminfo.DisplayType == 205) { //tree
                $t.parent().parent().find(".ztree").show();
            }
            that.stopBubble(e);
        });
    }
};