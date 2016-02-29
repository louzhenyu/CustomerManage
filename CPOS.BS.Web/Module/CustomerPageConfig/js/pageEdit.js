define(['jquery', 'customerTemp', 'template', 'kindeditor', 'tools'], function ($, temp) {
    //上传图片
    KE = KindEditor;
    var page = {
        url: "/ApplicationInterface/Gateway.ashx",
        template: temp,
        ele: {
            section: $("#section"),
            baseInfo: $("#baseInfo"),
            pageInfo: $("#pageInfo"),
            paramsList: $("#paramsList"),
            mask: $(".ui-mask")
        },
        init: function () {
            this.pageKey = $.util.getUrlParam("key");
            this.pageId = $.util.getUrlParam("id");
            if (!this.pageKey) {
                alert("未获取到key");
                return false;
            }
            if (!this.pageId) {
                alert("未获取到id");
                return false;
            }
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            if (this.pageKey) {
                this.loadPageInfo();
            }
        },
        initEvent: function () {

            this.ele.section.delegate("#saveBtn", "click", function () {
                var node = [], nodeValue = [];
                //捕获客户标题和模板
                $(".jsNodeValue").each(function (i, e) {
                    node.push($(e).data("node"));
                    nodeValue.push($(e).data("value") ? $(e).data("value") : $(e).val());
                });
                //debugger;
                // 有底部参数时添加node和nodeValue
                if (self.param) {
                    //取最新值
                    node.push(3);

                    nodeValue.push(JSON.stringify(self.param));
                }
                if (node.length != 0) {
                    //debugger;
                    self.submit(node, nodeValue);
                } else {
                    alert("没有可提交的数据");
                }

            }).delegate(".jsPageMoudle", "click", function () {
                $(this).parents(".jsNodeValue").data("value", $(this).data("id"));
                $(this).addClass("on").siblings().removeClass("on");
                self.renderTopBaseInfo($(this).data("id"));

            }).delegate(".jsTrigger", "blur", function () {
                debugger;
                var $paramValue = $(this).parents(".jsParamValue");
                //推送保存变量
                self.grabParamValue($paramValue);
            }).delegate("input.jsParamValue,select.jsParamValue", "blur", function (e) {
                var $this = $(this);
                //修改用户变量
                self.param[$this.data("key")] = $this.val();
            });
        },
        grabParamValue: function (paramValueEle) {
            var list = [];
            paramValueEle.find(".jsGroup").each(function (i, e) {
                var obj = {};
                $(e).find("input.jsTrigger,select.jsTrigger,span.jsTrigger").each(function (index, ele) {
                    //特殊处理百度直通车
                    if ($(ele).data("key").toLowerCase() == "baiduscript") {
                        obj[$(ele).data("key")] = ele.value ? ele.value.replace(/\"/g, "&quot;").replace(/\'/g, "&quot;").replace(/ /g, "&nbsp;") : ele.getAttribute("data-value");
                    } else {
                        obj[$(ele).data("key")] = ele.value ? ele.value: ele.getAttribute("data-value");
                    }

                });
                list.push(obj);
            });
            self.param[paramValueEle.data("key")] = list;
        },
        renderFormList: function () {
            this.ele.formList.html(this.render(this.template.formList, { itemList: this.formList }));
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },
        submit: function (node, nodeValue) {
            //debugger;
            $.util.ajax({
                url: this.url,
                data: {
                    action: "WX.SysPage.SetCustomerPageSetting",
                    PageKey: this.pageKey,
                    MappingId: this.pageId,
                    Node: node,
                    NodeValue: nodeValue
                },
                success: function (data) {
                    debugger;
                    if (data.IsSuccess) {
                        alert("保存成功，即将跳往列表页");
                        window.location.href = "pageList.aspx";
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        renderTopBaseInfo: function (selectedId) {
            //debugger
            var data = this.baseInfo,
                selectedId = selectedId || this.pageJson.defaultHtml;

            var domain = this.baseInfo.DoMainUrl;
            domain = domain.substring(0, domain.length - 1);
            //debugger;
            var pageUrl = this.baseInfo.DoMainUrl;
            if(pageUrl.indexOf("HtmlApps")<0){
                pageUrl+="HtmlApps/html/";
            }
            for (var i = 0; i < this.pageJson.htmls.length; i++) {
                if (selectedId == this.pageJson.htmls[i].id) {
                    pageUrl += this.pageJson.htmls[i].path;
                    break;
                }
            }
            data.PageUrl = pageUrl + "?customerId=" + $("#customerId").val();
            //debugger;
            data.PageAuthUrl = domain + "/WXOAuth/AuthUniversal.aspx?customerId=" + $("#customerId").val() + "&pageName=" + this.pageJson.pageKey + "&goUrl=" + (domain.split("://").length == 2 ? domain.split("://")[1] : domain.split("://")[0]) + this.pageJson.urlTemplete;
            //顶部基础信息   页面名、版本、更新人、时间
            self.ele.baseInfo.html(self.render(self.template.baseInfo, data));

        },
        renderMidPageInfo: function () {
            //中部页面信息    标题、页面模板
            this.ele.pageInfo.html(self.render(self.template.pageInfo, this.pageInfo));
        },
        renderBottomJsonInfo: function (userDataString) {
            var jsonParam = this.jsonParam;
            if (jsonParam.length) {
                var obj = {}, customerPara = null;
                try {
                    //有用户保存的参数时 把用户设置的值置为默认值，没有的时候取json中的默认值，并设用户变量param对象保存数据
                    customerPara = JSON.parse(userDataString);

                } catch (error) {

                }
                //debugger;
                // 获取默认参数 （已录入，则修改defaultValue，未录取，取defaultValue）
                for (var i = 0; i < jsonParam.length; i++) {
                    var idata = jsonParam[i];
                    if (customerPara && customerPara[idata.Key] != undefined) {
                        obj[idata.Key] = customerPara[idata.Key];
                        jsonParam[i].defaultValue = customerPara[idata.Key];
                    } else {
                        obj[idata.Key] = idata.defaultValue;
                    }
                }
                //debugger;
                self.param = obj;   //全局页面参数

                for (var i = 0; i < jsonParam.length; i++) {
                    var idata = jsonParam[i];

                    if (idata.valueType == "Array") {
                        //对valueType = Array特殊处理    对象数组
                        //{
                        //	"Name":"入口配置",
                        //	"Key":"links",
                        //	"SubKey":"title,english,toUrl",
                        //	"SubName":"入口名,入口英文名,入口链接",
                        //	"SubValueType":"string,string,string",
                        //	"SubDefaultValue":"入口,Entry,www.jitmarketing.cn",
                        //	"valueType":"Array",
                        //	"arrayLength":5,
                        //	"defaultValue":"",
                        //    "optionMap":{
                        //        "englist":{
                        //            "values":["up","down","left","right"],
                        //            "valuesText":["向上","向下","向左","向右"]
                        //        }
                        //    }
                        //}

                        //输出结果中的links

                        //'param':{
                        //	'title':'哎哟我操',
                        //	'logo':'../../../images/public/shengyuan_default/logo.jpg',
                        //	'links':[
                        //		{
                        //			'title':'门店查询',
                        //			'english':'STORE <br/>INFORMATION',
                        //			'toUrl':"javascript:Jit.AM.toPage('StoreList')"
                        //		},
                        //		{
                        //			'title':'最新活动',
                        //			'english':'NEWEST',
                        //			'toUrl':"javascript:Jit.AM.toPage('Activity')"/*全新代揽胜*/
                        //		},
                        //		{
                        //			'title':'影讯',
                        //			'english':'MOVIE <br/>INFORMATION',
                        //			'toUrl':"javascript:Jit.AM.toPage('Introduce','&type=1')"/*揽胜极光*/
                        //		},
                        //		{
                        //			'title':'热卖商品',
                        //			'english':'HOT SALE',
                        //			'toUrl':"javascript:Jit.AM.toPage('GoodsList')"/*揽胜极光*/
                        //		},
                        //		{
                        //			'title':'联系我们',
                        //			'english':'CONTACT US',
                        //			'toUrl':"javascript:Jit.AM.toPage('Introduce','&type=1')"/*揽胜极光*/
                        //		}
                        //	],   /*跳转地址   相对路径或者javascript模式*/
                        //	'backgroundImage':'../../../images/public/shengyuan_default/indexBg.jpg',
                        //	'littleImage':'',
                        //	'animateDirection':'up'   /*动画方向*/
                        //}

                        var html = '';
                        var menuKeyList = idata.SubKey.split(","),
                            menuNameList = idata.SubName.split(","),
                            menuTypeList = idata.SubValueType.split(","),
                            menuValueList = idata.SubDefaultValue.split(",");

                        var menuList = [],      //所有的参数
                            valueMap = {};       //所有的值

                        for (var j = 0; j < menuKeyList.length; j++) {
                            var menuObj = {
                                key: menuKeyList[j],
                                name: menuNameList[j],
                                type: menuTypeList[j],
                                value: menuValueList[j]
                            };
                            menuList.push(menuObj);


                            valueMap[menuObj.key] = menuValueList[j];

                        }

                        var tempList = [],      //输出的对象数组
                            defaultValue;       //默认值
                        if (idata.defaultValue) {
                            try {
                                defaultValue = JSON.parse(idata.defaultValue);
                            } catch (err) {
                                defaultValue = idata.defaultValue;
                            }
                        } else {
                            defaultValue = false;
                        }

                        for (var j = 0; j < idata.arrayLength; j++) {
                            if (defaultValue && defaultValue[j]) {
                                tempList.push(defaultValue[j]);
                            } else {
                                tempList.push(valueMap);
                            }

                            html += '<div  class="jsGroup" style="margin:20px 0;">';
                            for (var k = 0; k < menuList.length; k++) {
                                var edata = menuList[k];
                                edata.defaultValue = tempList[j][edata.key] || edata.value;
                                if (edata.type == "string") {
                                    edata.defaultValue = edata.defaultValue.replace(/\"/g, "&quot;").replace(/\'/g, "&quot;").replace(/ /g, "&nbsp;");
                                    html += self.render(self.template.paramJsonString, edata);
                                } else if (edata.type == "option") {
                                    edata.option = idata.optionMap[edata.key];
                                    html += self.render(self.template.paramJsonOption, edata);
                                } else if (edata.type == "image") {

                                    html += self.render(self.template.paramJsonImage, edata);
                                } else {
                                    html += '<div style="margin:5px 0;"><span style="display: inline-block;width: 80px;">' + edata.name + '</span><input class="jsTrigger" type="text" data-key="' + edata.key + '" value="' + tempList[j][edata.key] + '" /></div>';
                                }

                            }
                            html += '</div>'
                        }
                        //debugger;
                        jsonParam[i].defaultValue = tempList;
                        jsonParam[i].html = html;
                        //debugger;
                    } else if (idata.valueType == "ArraySimple") {
                        //对valueType = ArraySimple 特殊处理    对象数组
                        //{
                        //	"Name":"简单数组",
                        //	"Key":"links2",
                        //	"valueType":"ArraySimple",
                        //  "arrayName":["入口名","入口英文名","入口链接"],
                        //	"arrayKey":["title","direction","bgimage"], //当某个type中有option时必须要写，arrayKey到optionMap中招对应的select数据。
                        //	"arrayValueType":["string","option","image"],
                        //	"defaultValue":{title:"测试字符",direction:"left",bgimage:"../../images/bg.png"},
                        //  "optionMap":{       //key为序号
                        //      "dirction":{
                        //          "values":["up","down","left","right"],
                        //          "valuesText":["向上","向下","向左","向右"]
                        //      }
                        //  }
                        //}

                        var html = '<div  class="jsGroup" style="margin:20px 0;">';
                        for (var j = 0; j < idata.arrayValueType.length; j++) {
                            var edata = {
                                type: idata.arrayValueType[j],
                                name: idata.arrayName[j],
                                key: idata.arrayKey[j],
                                defaultValue: idata.defaultValue[0][idata.arrayKey[j]]

                            };
                            if (edata.type == "string") {
                                html += self.render(self.template.paramJsonString, edata);
                            } else if (edata.type == "option") {
                                edata.option = idata.optionMap[edata.key];
                                html += self.render(self.template.paramJsonOption, edata);
                            } else if (edata.type == "image") {
                                html += self.render(self.template.paramJsonImage, edata);
                            } else {
                                html += '<div style="margin:5px 0;"><span style="display: inline-block;width: 80px;">' + edata.name + '</span><input class="jsTrigger" type="text" data-key="' + edata.key + '" value="' + tempList[j][edata.key] + '" /></div>';
                            }
                        }
                        html += '</div>'
                        //debugger;
                        jsonParam[i].defaultValue = tempList;
                        jsonParam[i].html = html;


                    } else if (idata.valueType == "string") {
                        if (typeof idata.defaultValue == "object") {
                            jsonParam[i].defaultValue = JSON.stringify(idata.defaultValue);
                        }
                    }
                    self.param[idata.Key] = jsonParam[i].defaultValue;
                    //debugger;
                }
                //debugger;
                self.ele.paramsList.html(self.render(self.template.paramsList, { list: jsonParam }));

                // 注册上传按钮
                self.ele.paramsList.find(".jsUploadBtn").each(function (i, e) {
                    self.addUploadImgEvent(e);
                });
            }
        },
        loadPageInfo: function (callback) {
            $.util.ajax({
                url: this.url,
                data: {
                    action: "WX.SysPage.GetCustomerPageSetting",
                    PageKey: this.pageKey
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            var baseInfo, pageJson, pageInfo, jsonParam;
                            try {
                                baseInfo = data.Data.PageBaseInfo[0];
                                pageJson = JSON.parse(data.Data.JsonValue);
                                //debugger;   //重写用户配置，没有时取json中的默认值
                                pageInfo = {
                                    list: pageJson.htmls,
                                    model: data.Data.PageHtmls || pageJson.defaultHtml,
                                    title: data.Data.PageTitle || pageJson.title
                                }
                                jsonParam = pageJson.params
                            } catch (error) {
                                alert(error);
                                return false;
                            }


                            self.baseInfo = baseInfo;
                            self.pageJson = pageJson;
                            self.pageInfo = pageInfo;
                            self.jsonParam = jsonParam;
                            //debugger;
                            //顶部基础信息   页面名、版本、更新人、时间
                            self.renderTopBaseInfo(pageInfo.model);


                            //中部页面信息    标题、页面模板
                            self.renderMidPageInfo();


                            //底部附加参数   用户配置
                            self.renderBottomJsonInfo(data.Data.PagePara);
                        }
                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        addUploadImgEvent: function (e) {
            self.uploadImg(e, function (ele, data) {
                //上传成功后回写数据
                if ($(ele).parent().siblings("p.jsParamValue").length) {
                    $(ele).parent().siblings("p.jsParamValue").html('<img src="' + data.thumUrl + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                    self.param[$(ele).parent().siblings("p.jsParamValue").data("key")] = data.thumUrl;
                }

                if ($(ele).parent().siblings(".jsTrigger").length) {
                    $(ele).parent().siblings(".jsTrigger").html('<img src="' + data.thumUrl + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                    $(ele).parent().siblings(".jsTrigger").attr("data-value", data.thumUrl);
                    //推送保存变量
                    self.grabParamValue($(ele).parents(".jsParamValue").eq(0));
                }
            });
        },
        uploadImg: function (btn, callback) {
            console.log("uploadImg");
            setTimeout(function () {
                var uploadBtn = KE.uploadbutton({
                    width: "100%",
                    button: btn,
                    //上传的文件类型
                    fieldName: 'imgFile',
                    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                    url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=536&height=300',
                    afterUpload: function (data) {
                        if (data.error === 0) {
                            if (callback) {
                                callback(btn, data);
                            }
                            //取返回值,注意后台设置的key,如果要取原值
                            //取缩略图地址
                            //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                            //取原图地址
                            //var url = KE.formatUrl(data.url, 'absolute');
                        } else {
                            alert(data.message);
                        }
                    },
                    afterError: function (str) {
                        alert('自定义错误信息: ' + str);
                    }
                });
                uploadBtn.fileBox.change(function (e) {
                    uploadBtn.submit();
                });
            }, 10);

        },
        mask: {
            show: function () {
                self.ele.mask.show();
            },
            hide: function () {
                self.ele.mask.hide();
            }
        },
        delFormLayer: {
            data: {
                textTit: "是否确认删除？",
                OKText: "删除",
                OKClass: "jsDelBtn",
                CancelText: "取消",
                CancelClass: "jsCancelBtn",
                used: false
            },
            show: function (OKCallback, CancelCallback) {
                var that = this;
                if ($("#delFormLayer").length == 0) {
                    $(self.render(self.template.delFormLayer, this.data)).appendTo(self.ele.section);
                    $("#delFormLayer").delegate("." + this.data.OKClass, "click", function () {
                        if (OKCallback) {
                            OKCallback();
                        } else {
                            self.delFormLayer.hide();
                        }
                    }).delegate("." + this.data.CancelClass, "click", function () {
                        if (that.data.used) {
                            if (CancelCallback) {
                                CancelCallback();
                            }
                        } else {
                            self.delFormLayer.hide();
                        }
                    }).delegate(".closeBtn", "click", function () {
                        self.delFormLayer.hide();
                    })
                }
                self.mask.show();
                $("#delFormLayer").show();
            },
            hide: function () {
                $("#delFormLayer").remove();
                self.mask.hide();
            }
        }
    },
    self = page;
    page.init();
});



