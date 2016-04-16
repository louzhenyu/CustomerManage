/**
 * Created by xianHu on 2015/12/28. 1015003209@qq.com
 */
define(['jquery','tools','template', 'kindeditor',"easyui"], function ($) {
    //console.dir(touchslider);
    //debugger;
    //上传图片
    KE = KindEditor;
    var page = {
        elem:{
            sectionPage:$("#section")
        },
        //初始化参数
        init: function () {
            self.loadPageData();
            self.initEvent();
        },
        //初始化事件
        initEvent:function(){
            self.elem.sectionPage.find(".jsUploadBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
            self.elem.sectionPage.delegate(".subMitButton","click",function(){
                if ($('#setForm').form('validate')) {
                    debugger;
                    var fields = $('#setForm').serializeArray(); //自动序列化表单元素为JSON对象
                    self.loadData.operation(fields,"",function(data){

                        alert("操作成功");
                        self.loadPageData();

                    });
                }

            });

        },
        // 初始化页面数据
        loadPageData:function(){

            self.loadData.GetBusinessBasisConfigInfo(function(data){

                if(data.Data.CustomerShortName){
                    //$("#unitName").html(data.Data.CustomerShortName);
                }else{
                    data.Data.CustomerShortName=data.Data.customer_name
                }
                $('#setForm').form('load',data.Data);
                $("#customerName").html(data.Data.customer_name);
                if (data.Data["WebLogo"] ) {
                    $(".logoWrap").css({ 'background-image': 'url("' + data.Data["WebLogo"]  + '")' });
                  }
                    $("[data-name].logo").each(function() {
                        var name = $(this).data("name");

                         if(data.Data[name]) {
                             $(this).find("img").attr("src", data.Data[name]);
                         }
                    })

            });
        },
        addUploadImgEvent: function (e) {
            this.uploadImg(e, function (ele, data) {
                //上传成功后回写数据
                if ($(ele).parent().parent().siblings("div.logo").length) {
                    $(ele).parent().parent().siblings("div.logo").html('<img src="' + data.thumUrl + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                }

            });
        },
        uploadImg: function (btn, callback) {
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

        loadData:{
            args: {

            },
            operation:function(pram,operationType,callback){
                debugger;
                var  isJson=true;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                if(pram.length){
                    $.each(pram,function(i,filed){
                        if(filed.value) {
                            prams.data[filed.name] = filed.value;
                        }

                    })

                }
                var  isSubmit=true;
             $("[data-name].logo").each(function(){
                 var name=$(this).data("name")
                 var url=$(this).find("img").attr("src");
                 if(url!="images/imgDefault.png"){
                     prams.data[name]=url;
                 }else {
                     if (name == "WebLogo") {
                         $.messager.alert("提示", "商户Logo为没有上传图片");
                         isSubmit = false;
                         return false;
                     }
                     /*if (name == "ShareImageUrl") {
                         $.messager.alert("提示", "分享图片没有上传图片")
                     }
                     if (name == "GuideQRCode") {
                         $.messager.alert("提示", "引导二维码为上传图片")
                     }*/

                 }

             });

                if(!isSubmit){
                    return false;
                }
               prams.data.action="Basic.Customer.SetBusinessBasisConfig";



                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    isJSON:isJson,
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("操作失败提示",data.Message);
                        }
                    },error: function(data) {
                        $.messager.alert("操作失败提示",data.Message);
                        console.log("日志:"+operationType+"请求接口异常");
                    }
                });
            },
            GetBusinessBasisConfigInfo:function(callback){
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{action:"Basic.Customer.GetBusinessBasisConfigInfo",
                        "CustomerId":window.clientID},
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("操作失败提示",data.Message);
                        }
                    },error: function(data) {
                        $.messager.alert("操作失败提示",data.Message);
                        console.log("日志:"+operationType+"请求接口异常");
                    }
                });
            }
        }

    };
    self=page;
    page.init();
}) ;
