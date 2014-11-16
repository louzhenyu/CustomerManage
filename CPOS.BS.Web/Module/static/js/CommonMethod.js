var CommonMethod = { 
    /*
    解析照片对象,获取照片的Url
    @pBasePath  网站的根url,例如：http://report.jitmarketing.cn:9021/
    @pClientID  当前的客户ID
    @pUserID    拍摄照片的用户的用户ID
    @pPicJSON   照片对象的JSON.
    return      返回照片Url数组
    */
    getPictures: function (pBasePath, pClientID, pUserID, pPicJSON) {
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
        if (sel && data && idField && nameField) {
            var list = [];
            for (var i = 0; i < data.length; i++) {
                list.push({ id: eval("data[i]." + idField), name: eval("data[i]." + nameField) });
            }

            var temp = $("#selectTemp").html();
            sel.html(self.renderSelect(temp, { list: list }));
        }
    }
    //下拉选项选中事件
    , selectedEvent: function (target) {
        var that = this;
        $(target).delegate(".selectBox span", "mouseover", function (e) {
            //获得当前元素jquery对象
            var $t = $(this);
            $t.parent().find(".selectList").show();
            that.stopBubble(e);
        }).delegate(".selectBox p", "click", function (e) {
            //获得当前元素jquery对象
            var $t = $(this);
            //获得选择内容的id
            var optionValue = $t.attr("data-val");
            //改变显示的内容  及设置id
            $t.parent().parent().find(".text").html($t.html());
            $t.parent().parent().find(".text").attr("data-val", optionValue);
            $t.parent().hide();
            that.stopBubble(e);
        }).delegate(".selectList", "mouseleave", function (e) {    //鼠标从下拉内容移出的事件
            $(this).hide();
            that.stopBubble(e);
        });
    }
    , stopBubble: function (e) {
        if (e && e.stopPropagation) {
            //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        }
        else {
            //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;
        }
        e.preventDefault();
    }
    , formToJson: function (formID)
    {
        var data = $("#" + formID).formtojson();

        //select
        $("#" + formID + " .selected").each(function (i, t) {
            if ($(t).attr("data-val") != "")
                data[$(t).parent().attr("name")] = $(t).attr("data-val");
        })

        //checkbox single
        $("#" + formID + " .checkboxSingle").each(function (i, t) {
            if ($(t).hasClass("on"))
                data[$(t).attr("name")] = "1";
            else
                data[$(t).attr("name")] = "0";
        })

        //checkbox group
        $("#" + formID + " .checkboxGroup").each(
            function (i, g) {
                //var group = {};
                var groupName = $(g).attr("name");
                //add group
                eval("data." + groupName + " = [];");

                $("#" + $(g).attr("id") + " .on").each(
                    function (i, t) {
                        if ($(t).attr("data-val") != "") {
                            var field = {};
                            field[$(t).attr("name")] = $(t).attr("data-val");
                            //add checkbox value
                            eval("data." + groupName + ".push(field)");
                        }
                })
        })

        return data;
    }
    , bindValue: function (formID, data)
    {
        for (var d in data) {
            var e = $("#" + formID + " [name = " + d + "]")[0];
            if (e) {
                var value = eval("data." + d);
                if (value && value != "") {
                    switch (e.tagName.toLowerCase()) {
                        case "input":
                        case "textarea":
                            $(e).val(value);
                            break;
                        default:
                            if ($(e).hasClass("selectBox"))
                            {
                                var selectedSpan = $($(e).children(".selected")[0]);
                                selectedSpan.attr("data-val", value);
                                $(e).children(".selectList").children(".option").each(function (i, d) {
                                    if ($(d).attr("data-val") == value) {
                                        selectedSpan.text($(d).text());
                                    }
                                });
                            }
                            else if ($(e).hasClass("checkboxSingle"))
                            {
                                if (value == "1")
                                    $(e).addClass("on");
                            }
                            else if ($(e).hasClass("checkboxGroup"))
                            {
                                for (var i = 0; i < value.length; i++) {
                                    $("#" + $(e).attr("id") + " [data-val = " + value[i].PublicControlID + "]").addClass("on");
                                }
                            }
                    }
                }
            }
        }
    }
};