$(function() {
    var page = {

        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();
            this.cancelOrder();
            debugger;
            $("#userName").val($("#lblLoginUserName",parent.document).attr("userName"))
        },
        initEvent:function(){
           var that=this;
        $('#winPwd').window({
            modal:true,
            shadow:false,
            collapsible:false,
            minimizable:false,
            maximizable:false,
            closed:true,
            closable:true,
            onClose:function(){
                $('#padIframe', parent.document).remove()
            }
        });
            $('#winPwd').delegate(".saveBtn","click",function(e){

                if ($('#payOrder').form('validate')) {

                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,"",function(data){

                        alert("操作成功");
                        $('#win').window('close');


                    });
                }
            });

    },

        cancelOrder:function(data){
            var that=this;
            var top=$(document).scrollTop()+80;

            $('#winPwd').window({title:"修改密码",width:380,left:($(window).width()-380)/2,height:340,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent1').layout('remove','center');
            var html=bd.template('tpl_OrderCancel');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent1').layout('add',options);
            $('#winPwd').window('open');

        },


        //设置查询条件   取得动态的表单查询参数

        loadData: {
            args: {

            },



            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"Basic.User.SetPassword"}};

                //根据不同的操作 设置不懂请求路径和 方法

                   prams.url="/ApplicationInterface/Gateway.ashx";
                   $.each(pram, function (index, filed) {

                       prams.data[filed.name] = filed.value;
                   });


                    $.util.ajax({
                        url: prams.url,
                        data: prams.data,
                        success: function (data) {
                            if (data.IsSuccess) {
                                if (callback) {
                                    callback(data);
                                }

                            } else {
                                $.messager.alert("操作失败提示", data.Message);
                            }
                        }, error: function (data) {
                            $.messager.alert("操作失败提示", data.Message);
                            console.log("日志:" + operationType + "请求接口异常");
                        }
                    });


            }


        }

    };
    page.init();
});

