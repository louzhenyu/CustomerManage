define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','highcharts','kindeditor'], function ($) {

    //上传图片
    KE = KindEditor;

    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabelAction:$("#gridTable1"),                   //表格活动body部分
            tabelCoupon:$("#gridTable2"),                   //表格优惠券body部分
            tabelImgtext:$("#gridTable3"),                  //图文素材活body部分
            tabelWrap:$('#tableWrap'),
            editLayer: $("#editLayer"),           //图片上传
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            addToolsBtn:$('#addTools'),
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            dataNoticeList:$('#notice'),
            panlH:116                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard: '',//储值卡号
        submitappcount: false,//是否正在提交追加表单
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        //显示弹层
        showElements:function(selector){
            this.elems.uiMask.show();
            $(selector).slideDown();
        },
        hideElements:function(selector){
            this.elems.uiMask.fadeOut(500);
            $(selector).slideUp(500);
        },
        //加载商品列表
        initEvent: function () {
            var that = this;
            //点击添加工具添加活动
            //that
            $('.day30Totle').delegate('.noContents a','click',function(e){
                that.elems.sectionPage.find('.addTools').trigger("click");
            })

            that.elems.sectionPage.delegate(".addTools","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                //查询数据
                debugger;
                that.updateTool();
                var ii = $(this).parents('.ModuleList').children('.title').find('li.current').index();
                $(that.elems.operation.find("li").get(ii)).trigger("click");
                $('#winTool').find('#opt').show();
                $('#winTool').find("#setPosterName").val('');
                $('#winTool').find("#setPosterName").attr('data-id','');
                $('#winTool').find("#setOffBack").attr('src','');

            });
            that.elems.operation.delegate("li","click",function(e){
                that.elems.operation.find("li").removeClass("on");
                $(this).addClass('on');
                var value=$(this).attr("data-field7").toString();
                var html = "<div class='notice' style='text-align:center;'>暂无活动，请新增相关活动</div>";
                var html1 = "将品牌和商品的优势编辑成为微信图文，作为拓展工具提供给分销商，帮助分销商丰富分享内容，用品牌的力量协助分销商拓展分销事业！";
                var html2 = "将品牌、产品和分销政策等内容设置在创意活动中，帮助分销商实施专业的互动营销活动，吸引客户参与。";
                var html3 = "设置优惠券，用优惠帮助分销商推动商品的销售，让分销商也能够做促销！";
                if(value=='900'){
                    $('#winTool').find('.datagrid').hide();
                    $('#winTool').find('.toolList').hide();
                    that.elems.dataNoticeList.hide();
                    $('#winTool').find('#setOfferPoster').show();
                }else if(value=='200'){
                    $.util.partialRefresh($('#gridTable3'));
                    $('#winTool').find('#setOfferPoster').hide();
                    $('#gridTable1').parents('.datagrid').hide();
                    $('#gridTable2').parents('.datagrid').hide();
                    that.loadToolData(0);
                    $('#winTool').find('.toolList').show();
                    $('#winTool').find('.toolList').find('.instruction').html(html1);
                    $('#winTool').find('.toolList').children('.commonBtn').html('新建图文');
                }else if(value=='0'){
                    $.util.partialRefresh($('#gridTable1'));
                    $('#winTool').find('#setOfferPoster').hide();
                    $('#gridTable3').parents('.datagrid').hide();
                    $('#gridTable2').parents('.datagrid').hide();
                    that.loadToolData(1);
                    $('#winTool').find('.toolList').show();
                    $('#winTool').find('.toolList').find('.instruction').html(html2);
                    $('#winTool').find('.toolList').children('.commonBtn').html('新建活动');
                }else if(value=='100'){
                    $.util.partialRefresh($('#gridTable2'));
                    $('#winTool').find('#setOfferPoster').hide();
                    $('#gridTable1').parents('.datagrid').hide();
                    $('#gridTable3').parents('.datagrid').hide();
                    that.loadToolData(2);
                    $('#winTool').find('.toolList').show();
                    $('#winTool').find('.toolList').find('.instruction').html(html3);
                    $('#winTool').find('.toolList').children('.commonBtn').html('新建优惠券');
                }
            });

            $('#tableWrap').delegate('.reload','click',function(){
                var value=that.elems.operation.find("li[class='on']").attr("data-field7").toString();
                var SetoffType = $('#winTool').attr('data-type');
                if(value=='0'){
                    that.loadToolData(1);
                }else if(value=='100'){
                    that.loadToolData(2);
                }else if(value=='200'){
                    that.loadToolData(0);
                }
            })

            $('#tableWrap').delegate('.example','mouseover',function(){
                var value=that.elems.operation.find("li[class='on']").attr("data-field7").toString();
                if(value=='200'){
                    var url ="images/imgText.png";
                }else if(value=='0'){
                    var url ="images/distribution.png";
                }else if(value=='100'){
                    var url ="images/coupon.png";
                }
                $(this).tooltip({
                    position: 'bottom',
                    content: '<img style="color:#fff" src="'+url+'">',
                }).tooltip('show');
            })

            // $('#tableWrap .example')
            //
            $('#tableWrap').delegate('.commonBtn','click',function(){
                var value=that.elems.operation.find("li[class='on']").attr("data-field7").toString();
                if(value=='0'){
                    window.open( "/module/CreativityWarehouse/CreativeWarehouseView/QueryList.aspx");
                }else if(value=='100'){
                    window.open( "/module/couponManage/querylist.aspx");
                }else if(value=='200'){
                    window.open( "/Module/WMaterialText/WMaterialText.aspx");
                }
            })
            // 新增工具
            $('#winTool').delegate('.saveBtn','click',function(){
                //活动type=0，优惠券type=100，集客海报type=900
                var name = $('#winTool').find('#setPosterName').val(),
                    imgUrl = $('#winTool').find("#setOffBack").attr('src'),
                    setPosterId =$('#winTool').find('#setPosterName').attr('data-id'),//编辑海报时，已创建的海报才有id。
                    toolTitle = $('.ModuleList').children('.title'),
                    value = that.elems.operation.find("li[class='on']").attr('data-field7'),
                    isData=true,
                    isUniform = true;
                if(value=='900'){
                    if(name==""){
                        $.messager.alert('提示','海报名称为必填');
                        isData=false;
                        return false;
                    }else{
                        isData=true;
                    }
                    if(imgUrl==""){
                        $.messager.alert('提示','请上传一张图片');
                        isData=false;
                        return false;
                    }else{
                        isData=true;
                    }
                }else{
                    if(name==""){
                        isData=false;
                    }else{
                        isData=true;
                    }
                    if(imgUrl==""){
                        isData=false;
                    }else{
                        isData=true;
                    }
                }
                if($('#gridTable1').parents('.datagrid').length!='0'){
                    var listAction = that.elems.tabelAction.datagrid("getChecked");//获取创意仓库选中 list

                }else{
                    var listAction="";
                }
                if($('#gridTable2').parents('.datagrid').length!='0'){
                    var listCoupon = that.elems.tabelCoupon.datagrid("getChecked");//获取优惠券选中 list
                }else{
                    var listCoupon="";
                }
                if($('#gridTable3').parents('.datagrid').length!='0'){
                    var listImgtext = that.elems.tabelImgtext.datagrid("getChecked");//获取图文素材选中 list
                }else{
                    var listImgtext="";
                }
                var listPoster = [{"ImageUrl":imgUrl,"Name":name}];//创建集客海报list
                if(listImgtext!=''&&isUniform==true){
                    that.fiterData(listImgtext,0,function(data){
                        if(data!=false) {
                            var listData = that.loadData.setOff.toolImgtext;
                            listImgtext = listData.concat(listImgtext);
                            that.loadData.setOff.toolImgtext = listImgtext;
                            var html = bd.template("tpl_imgText", {list: listImgtext});
                            $(".imageTextTool").html(html);
                            $('.toolData').find('span').html('未发布');
                        }else{
                            isUniform =false;
                            return false;
                        }
                    })
                }
                if(listAction!=''&&isUniform==true){
                    that.fiterData(listAction,0,function(data){
                        if(data!=false) {
                            var listData = that.loadData.setOff.toolAction;
                            listAction = listData.concat(listAction);
                            that.loadData.setOff.toolAction = listAction;
                            var html = bd.template("tpl_action", {list: listAction});
                            $(".saleActivityTool").html(html);
                            $('.toolData').find('span').html('未发布');
                        }else{
                            isUniform =false;
                            return false;
                        }
                    })
                }
                if(listCoupon!=''&&isUniform==true){
                    that.fiterData(listCoupon,100,function(data){
                        if(data!=false) {
                            var listData = that.loadData.setOff.toolCoupon;
                            listCoupon = listData.concat(listCoupon);
                            that.loadData.setOff.toolCoupon = listCoupon;
                            var html = bd.template("tpl_coupon", {list: listCoupon});
                            $(".couponTool").html(html);
                            $('.toolData').find('span').html('未发布');
                        }else{
                            isUniform =false;
                            return false;
                        }
                    })
                }
                if(isData!=false&&isUniform==true){
                    if(setPosterId!=""){
                        var $tt = $('.setOffVipModule').find('.contentsData').find("li[data-id='"+setPosterId+"']");
                        $tt.find('.name').html(name);
                        $tt.attr('data-url',imgUrl);
                    }else{
                        that.fiterData(listPoster,900,function(data){
                            if(data!=false) {
                                var listData = that.loadData.setOff.toolPoster;
                                listPoster = listData.concat(listPoster);
                                that.loadData.setOff.toolPoster = listPoster;
                                var html = bd.template("tpl_poster", {list: listPoster});
                                $(".posterTool").html(html);
                                $('.toolData').find('span').html('未发布');
                            }else{
                                isUniform =false;
                                return false;
                            }
                        })
                    }
                }

                if(value=='0'){
                    toolTitle.find('li').eq(1).trigger('click');
                }else if(value=='100'){
                    toolTitle.find('li').eq(2).trigger('click');
                }else if(value=='200'){
                    toolTitle.find('li').eq(0).trigger('click');
                }else if(value=='900'){
                    toolTitle.find('li').eq(3).trigger('click');
                }
                if(isUniform==true){
                    $('#winTool').window('close');
                    that.loadToolData(0);
                    that.loadToolData(1);
                    that.loadToolData(2);
                }
            });

            // 活动列表二维码绑定
            $('#tableWrap').delegate('.screen','mouseover',function(e){
                debugger;
                var url =$(this).parents('li').attr('data-url');
                $(this).tooltip({
                    position: 'bottom',
                    content: '<img style="color:#fff;width:100px;height:100px"  src="'+url+'">'
                }).tooltip('show');

            })

            //分销活动已选活动内容切换
            $('.ModuleList .title').delegate('li','click',function(){
                $(this).parents('.ModuleList').find('li').removeClass('current');
                $(this).addClass('current');
                var ii = $(this).index(),
                    isData = $('.contentsTool').eq(ii).children('.toolDatas').find('li');
                $('.contentsTool').hide();
                $('.contentsTool').eq(ii).show();
                if(isData.length!="0"){
                    $('.contentsTool').eq(ii).children('.toolExmaple').hide();
                }else{
                    $('.contentsTool').eq(ii).children('.toolExmaple').show();
                }
            });
            //集客工具移除
            $('.toolDatas').delegate('.remove','click',function(){
                //var that = this;
                var par =$(this).parents('li');
                var ii =par.index();
                var type = par.attr('data-type');
                var name = par.find('.name').html();
                var cancelType = '1';//1==移除；2==取消发布
                that.updateCanel(type,cancelType);
                $('#winCanel').attr('data-name',name);
                $('#winCanel').attr('data-type',type);
                $('#winCanel').attr('data-index',ii);
                $('#winCanel').attr('data-cancelType','1');
                // $.messager.confirm('提示','确定移除吗？',function(r){
                //     if(r){
                //         //that.loadData.updateSetoffTool();
                //         par.remove();
                //         alert(""+name+"移除成功");
                //         that.removeTool(type,ii);
                //         $('.ModuleList .title').find('li').eq(type).trigger("click");
                //     }
                // })
            })
            //集客工具取消发布
            $('.toolDatas').delegate('.removeDlist','click',function(){
                var par =$(this).parents('li');
                var idData = [];
                par.each(function(i){
                    var id = $(par).attr('data-toolid');
                    idData.push(id);
                })
                var ii =par.index();
                var type = par.attr('data-type');//创意仓库活动=1;优惠券活动=2;集客海报=3;图文素材=4；
                var name = par.find('.name').html();
                var cancelType = '2';
                that.loadData.tag.toolDList =idData;
                that.updateCanel(type,cancelType);
                $('#winCanel').find('.btnWrap').show();
                $('#winCanel').attr('data-name',name);
                $('#winCanel').attr('data-type',type);
                $('#winCanel').attr('data-index',ii);
                $('#winCanel').attr('data-cancelType',cancelType);
            })
            $('#winCanel').delegate('.saveBtn','click',function(){
                $('#winCanel').window('close');
                var name = $('#winCanel').attr('data-name');
                var type = $('#winCanel').attr('data-type');
                var ii = $('#winCanel').attr('data-index');
                var cancelType = $('#winCanel').attr('data-cancelType');
                if(cancelType=='2'){
                    that.loadData.updateSetoffTool(function(data,IsSuccess){
                        if(IsSuccess){
                            alert(""+name+"已停止发布");
                        }
                    });
                }else{
                    if(type=='3'){
                        alert(""+name+"删除成功");
                    }else{
                        alert(""+name+"移除成功");
                    }


                }
                that.removeTool(type,ii);
                $('.ModuleList .title').find('li').eq(type).trigger("click");
            })


            //集客海报编辑
            $(".toolDatas").delegate('.exitPoster',"click",function(){
                var par =$(this).parents('li');
                var id = $(par).attr('data-id');
                var url = $(par).attr('data-url');
                var name = $(par).find('.name').html();
                that.updateTool(id);
                var type = $(this).parents('.blockModul').find('.addTools').attr('data-type');
                $(that.elems.operation.find("li").get(3)).trigger("click");
                $('#winTool').find('#setOffBack').attr('src',url);
                $('#winTool').find('#setPosterName').val(name);
                $('#winTool').attr('data-type',type);
                $('#winTool').find('#setPosterName').attr('data-id',id);
                $('#winTool').find('#opt').hide();
            });


            //关闭弹出层
            $(".hintClose").bind("click",function(){
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
            });

            //发送通知
            $('#saveMessage').bind('click',function(){
                that.updateMessage();
            });

            //确认发布
            $('#saveSetOff').bind('click',function(){
                var ilength = $('.contentsTool ul li').length;
                if(ilength!=0){
                    that.update();
                }else{
                    alert('请先添加工具');
                }

            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=140,H=30;
            // $('#rewardRule').combobox({
            //     width:wd,
            //     height:H,
            //     panelHeight:that.elems.panlH,
            //     valueField: 'id',
            //     textField: 'text',
            //     data:[{
            //         "id":1,
            //         "text":"现金"
            //     },{
            //         "id":2,
            //         "text":"积分"
            //     },{
            //         "id":0,
            //         "text":"选择奖励模式",
            //         "selected":true
            //
            //     }]
            // });



            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true
            });
            $('#panlconent').layout({
                fit:true
            });
            //发送通知复选框
            $('#win').delegate(".checkBox","click",function(){
                var isChecked = $(this).hasClass('on');
                if(isChecked){
                    $(this).removeClass('on');
                }else{
                    $(this).addClass('on');
                }

            })

            //设置集客行动
            $('#win').delegate(".saveBtn","click",function(e){
                var SetOff=[],
                     obj ={},
                     item1 = $('.saleActivityTool').find('li'),
                     item2 = $('.couponTool').find('li'),
                     item3 = $('.imageTextTool').find('li'),
                     item4 = $('.posterTool').find('li'),
                     staffToolAction =[],
                     staffToolCoupon =[],
                     staffToolImgtext =[],
                     staffToolPoster=[],
                     isCheck = $('#win').find('.on').length;
                    item1.each(function(j){
                        $tt = $(this);
                        var obj ={};
                        var dataNew = $tt.attr('data-new');
                        if(dataNew!='true'){
                            obj.ObjectId =$tt.attr('data-id');
                            obj.ToolType='CTW';
                            staffToolAction.push(obj);
                        }
                    })
                    item2.each(function(k){
                        $tt = $(this);
                        var obj ={};
                        var dataNew =$tt.attr('data-new');
                        if(dataNew!='true'){
                            obj.ObjectId =$tt.attr('data-id');
                            obj.ToolType='Coupon';
                            staffToolCoupon.push(obj);
                        }
                    })
                    item3.each(function(i){
                        $tt = $(this);
                        var obj ={};
                        var dataNew =$tt.attr('data-new');
                        if(dataNew!='true'){
                            obj.ObjectId =$tt.attr('data-id');
                            obj.ToolType='Material';
                            staffToolImgtext.push(obj);
                        }
                    })
                    item4.each(function(n){
                        $tt = $(this);
                        var obj ={};
                        var dataNew =$tt.attr('data-new');
                        if(dataNew!='true'){
                            obj.ImageUrl =$tt.attr('data-url');
                            obj.Name =$tt.find('.name').html();
                            staffToolPoster.push(obj);
                        }
                    })
                    SetOff=staffToolAction.concat(staffToolCoupon,staffToolImgtext);
                    that.loadData.setOff.commonList=SetOff;//发布集客行动
                    that.loadData.setOff.posterList=staffToolPoster;//发布集客行动
                    $('#win').window('close');
                    that.loadData.setExtend(function(data,isSuccess){
                        if(isSuccess){
                            if(isCheck=="1"){
                                alert('工具发布成功');
                                var isShow = $()
                                that.updateMessage();
                                $('#winMessage').attr('data-type','1');
                            }else{
                                $('#win').window('close');
                                alert('工具发布成功');
                                location.reload();
                            }
                        }
                    });



            });

            $('#winMessage').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:false
            });
            $('#panlConent').layout({
                fit:true
            });

            //发送通知复选框
            $('#winMessage').delegate(".checkBox","click",function(){
                var isChecked = $(this).hasClass('on');
                if(isChecked){
                    $(this).removeClass('on');
                }else{
                    $(this).addClass('on');
                }

            })

            $('#winMessage').delegate('.editMessage','click',function(){
                var $tt = $(this).children('textarea');
                var $span = $(this).children('span').children('b');
                $tt.keyup(function(){
                    var tt = $tt.val().length;
                    $span.html(tt);
                })
            })
            //发送通知
            $('#winMessage').delegate(".saveBtn","click",function(e){
                var item = $('#winMessage').find('.on');
                if(item.length!='0'){
                    var noticeInfo =[];
                    var isSuccess =true;
                    var type = $('#winMessage').attr('data-type');
                    item.each(function(i){
                        var obj ={};
                        var tt = $(this);
                        var txt = $(tt).parents('.contents').children('.editMessage').children('textarea');
                        $(txt).click(function(){
                            $(this).css('border','1px solid #ccc');
                        })
                        obj.NoticePlatformType = $(tt).parents('.editCheck').attr('data-type');
                        obj.SetoffEventID =$(tt).parents('.editCheck').attr('data-setoffeventid');
                        obj.Title = $(tt).parents('.editCheck').children('p').attr('data-name');
                        obj.Text = $(tt).parents('.contents').children('.editMessage').children('textarea').val();
                        if(obj.Text==""){
                            $.messager.alert('提示','所选通知内容不能为空');
                            isSuccess= false;
                            return false;
                        }
                        if(parseInt(txt.val().length)>50){
                            if(i=1){
                                $(txt).css('border','1px solid red');
                                $.messager.alert('提示','输入文字不能超过50字');
                                isSuccess= false;
                                return false;

                            }
                            if(i=2){
                                $(txt).css('border','1px solid red');
                                $.messager.alert('提示','输入文字不能超过50字');
                                isSuccess= false;
                                return false;
                            }
                            if(i=3){
                                $(txt).css('border','1px solid red');
                                $.messager.alert('提示','输入文字不能超过50字');
                                isSuccess= false;
                                return false;
                            }

                        }
                        else{
                            noticeInfo.push(obj);
                        }

                    })
                    that.loadData.setOff.Message = noticeInfo;
                    if(isSuccess){
                        that.loadData.sendNotice(function(data){
                            if(type=='1'){
                                $('#winMessage').window('close');
                                alert('拓展工具及发送通知已发布');
                                location.reload();
                                //$.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");

                            }
                            else{
                                $('#winMessage').window('close');
                                alert('发送通知已发布');
                                location.reload();
                                //$.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");
                                //wi//ndow.location.href="SetOffManage/source.aspx?";
                            }
                        });
                    }
                }else{
                    $.messager.alert('提示','请至少选择一种');
                }
            })

            $('#winMessage').delegate(".cancelBtn","click",function(e){
                var type = $('#winMessage').attr('data-type');
                if(type=='1'){
                    $('#winMessage').window('close');
                    alert('拓展工具及已发布');
                    location.reload();
                }else{
                    $('#winMessage').window('close');
                    location.reload();
                }
            })
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".opt","click",function(e){

            });
            //图片上传按钮绑定
            that.registerUploadImgBtn();

            /**************** -------------------列表操作事件用例 End****************/
        },

        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                that.loadData.seach[filed.name] = filed.value;
            });
            that.loadData.seach.Field7=that.elems.operation.find("li.on").data("field7");
        },
        //删除工具更新数据，重新遍历；
        removeTool:function(type,ii){
            var that=this;
            if(type=='1'){
                var list = that.loadData.setOff.toolAction;
                list.splice(ii,1);
                var html = bd.template("tpl_action", {list: list});
                $(".saleActivityTool").html(html);
            }
            else if(type=='2'){
                var list = that.loadData.setOff.toolCoupon;
                list.splice(ii,1);
                var html = bd.template("tpl_coupon", {list: list});
                $(".couponTool").html(html);
            }
            else if(type=='3'){
                var list = that.loadData.setOff.toolPoster;
                list.splice(ii,1);
                var html = bd.template("tpl_poster", {list: list});
                $(".posterTool").html(html);
            }
            else if(type=='0'){
                var list = that.loadData.setOff.toolImgtext;
                list.splice(ii,1);
                var html = bd.template("tpl_imgText", {list: list});
                $(".imageTextTool").html(html);
            }

        },
        updateTool:function(id){
            debugger;
            var that=this;
            if(id==undefined){
                $('#winTool').window({title:"选择工具",width:600,height:600,top:($(window).height()-600) * 0.5,
                    left:($(window).width() - 600) * 0.5});
            }else{
                $('#winTool').window({title:"编辑集客海报",width:600,height:600,top:($(window).height()-600) * 0.5,
                    left:($(window).width() - 600) * 0.5});
            }

            //改变弹框内容，调用百度模板显示不同内容
            $('.window-mask').hide();

            $('#winTool').window('open');
            $('#winTool').parents('.window').css('position','fixed');
        },

        updateMessage:function(){
            debugger;
            var that=this;

            $('#winMessage').window({title:"发送通知",width:550,height:540,top:($(window).height() - 540) * 0.5,
                left:($(window).width() - 550) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlConent').layout('remove','center');
            var html=bd.template('tpl_Message');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlConent').layout('add',options);
            $('#winMessage').window('open');
            $('#winMessage').parents('.window').css('position','fixed');
        },

        update:function(){
            debugger;
            var that=this;
            $('#win').window({title:"提示",width:370,height:235,top:($(window).height()-235) * 0.5,
                left:($(window).width() - 370) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            var html=bd.template('tpl_Info');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').html(html);
            $('#win').parents('.window').css('position','fixed');
            $('#win').window('open');
        },
        //取消发布
        updateCanel:function(type,cancelType){
            debugger;
            var that=this;
            $('#winCanel').window({title:"提示",width:400,height:250,top:($(window).height()-250) * 0.5,
                left:($(window).width() - 400) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            //创意仓库活动=1;优惠券活动=2;集客海报=3;图文素材=4；
            if(cancelType=='1'){
                if(type=='1'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定移除该分销活动？</p>";
                }else if(type=='2'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定移除该优惠券？</p>";
                }else if(type=='3'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定删除该集客海报？</p>";
                }else if(type=='0'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定移除该图文素材？</p>";
                }
            }else{
                if(type=='1'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定取消该分销活动？</p>";
                }else if(type=='2'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定取消该优惠券？</p>";
                }else if(type=='3'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定取消该集客海报？</p>";
                }else if(type=='0'){
                    var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>您确定取消该图文素材？</p>";
                }
            }


            var options = {
                region: 'center',
                content:html
            };
            $('#panlConents').html(html);
            $('.window-mask').hide();
            $('#winCanel').window('open');
            $('#winCanel').parents('.window').css('position','fixed');
        },
        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            var self = this;
            // 注册上传按钮
            self.elems.editLayer.find(".uploadImgBtn").each(function (i, e) {
                self.addSetOffImgEvent(e);
            });
        },
        //上传图片区域的各种事件绑定
        addSetOffImgEvent: function (e) {
            var self = this,
                $setOffBack = $('#setOffBack');
            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                var result = data,
                    thumUrl = result.thumbs,//缩略图
                    url = result.url;//原图
                $setOffBack.attr('src', url);
                debugger;
                $("#setOffBack").form("load", {ImageUrl:url});
            });
        },
        //上传图片
        uploadImg: function (btn, callback) {
            var _width = 130;
            var that = this,
                w = 640,
                h = 1008,
                flag = $(btn).parents('.uploadItem').data('flag');
            if(flag==3){
                w = 100;
                h = 100;
            }else if(flag==4){
                w = 536;
                h = 300;
            } else if (flag == "Cover" || flag == "Cover1") {
                _width = 80;
            }
            if ($(btn).parents('.uploadItem').data("flag") == 14)
            {
                _width = 88;
            }
            var	uploadbutton = KE.uploadbutton({
                button: btn,
                width: _width,
                //上传的文件类型
                fieldName: 'imgFile',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=536&height=300',
                //&width='+w+'&height='+h+'&originalWidth='+w+'&originalHeight='+h
                afterUpload: function(data){
                    if(data.error===0){
                        if(callback) {
                            callback(btn, data);
                            if ($(btn).data("alertinfo")) {
                                $.messager.alert('提示',$(btn).data("alertinfo"));
                            } else {
                                $.messager.alert('提示',"图片上传成功！");
                            }
                        }
                    }else{
                        alert(data.message);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            uploadbutton.fileBox.change(function(e){
                uploadbutton.submit();
            });
        },
        loadToolData:function(ii){
            var that =this;
            var listImgtext = that.loadData.setOff.toolImgtext;
            var listAction = that.loadData.setOff.toolAction;
            var listCoupon = that.loadData.setOff.toolCoupon;
            if(ii==0){
                that.loadData.getNewsList(function (data) {
                    var list2 = data.List;

                    for (var i = 0; i < listImgtext.length; i++) {
                        for (var j = 0; j < list2.length; j++) {
                            if (listImgtext[i].ObjectId == list2[j].TextId) {
                                list2.splice(j, 1);
                            } else if (listImgtext[i].TextId == list2[j].TextId) {
                                list2.splice(j, 1);
                            }
                        }
                    }
                    that.renderTable3(data, ii);
                });
            }else if(ii==1){
                that.loadData.getCTWLEventList(function(data){
                    var list2 = data.CTWLEventInfoList;
                    for(var i=0;i<listAction.length;i++){
                        for(var j=0;j<list2.length;j++){
                            if(listAction[i].ObjectId==list2[j].CTWEventId){
                                list2.splice(j,1);
                            }else if(listAction[i].CTWEventId==list2[j].CTWEventId){
                                list2.splice(j,1);
                            }
                        }
                    }
                    that.renderTable1(data,ii);
                });
            }else if(ii==2){
                that.loadData.getCouponTypeList(function(data){
                    var list2 = data.CouponTypeList;
                    for(var i=0;i<listCoupon.length;i++){
                        for(var j=0;j<list2.length;j++){
                            if(listCoupon[i].ObjectId==list2[j].CouponTypeID){
                                list2.splice(j,1);
                            }else if(listCoupon[i].CouponTypeID==list2[j].CouponTypeID){
                                list2.splice(j,1);
                            }
                        }
                    }
                    that.renderTable2(data,ii);
                });
            }
        },
        fiterData:function(list,type,callback){
            var data= list;
            var staffToolAction=[];
            var staffToolCoupon=[];
            var staffToolPoster=[];
            var vipToolAction=[];
            var vipToolCoupon=[];
            var vipToolPoster=[];

            var item1 =$(this).find('.toolData .contentsData').eq(0).find('li');
            var item2 =$(this).find('.toolData .contentsData').eq(1).find('li');
            var item3 =$(this).find('.toolData .contentsData').eq(2).find('li');
            item1.each(function(j){
                $tt = $(this);
                var obj ={};
                obj.ObjectId =$tt.attr('data-id');
                obj.Name =$tt.find('.name').html();
                staffToolAction.push(obj);
            })
            item2.each(function(k){
                $tt = $(this);
                var obj ={};
                obj.ObjectId =$tt.attr('data-id');
                obj.Name =$tt.find('.name').html();
                staffToolCoupon.push(obj);
            })
            item3.each(function(n){
                $tt = $(this);
                var obj ={};
                obj.Name =$tt.find('.name').html();
                obj.Name =$tt.find('.name').html();
                staffToolPoster.push(obj);
            })


            if(data!=null){
                var isContinued = true;
                if(type=='0'){
                    var idData =[];
                    for(var i=0;i<data.length;i++){
                        idData.push(data[i].CTWEventId);
                        if(vipToolAction!=""){
                            var toolData = $('.setOffVipModule').find('.toolData .contentsData').eq(0).find("li[data-id='"+idData[i]+"']");

                            if(toolData.length!="0"){
                                var name =$(toolData).find('.name').html();
                                $.messager.alert('提示','你的会员活动列表已有'+name+'活动');
                                // $('#winTool').find('.warnning').children('span').eq(0).html('您的会员活动列表中已有');
                                // $('#winTool').find('.warnning').children('b').html(name);
                                callback(false);
                                return false;
                            }else{
                                callback(true);
                                isContinued = false;
                                return true;
                            }
                        }else{
                            callback(true);
                            isContinued = false;
                            return false;
                        }if(isContinued == false){
                            return false;
                        }
                    }
                }
                if(type=='100'){
                    var idData =[];
                    for(var i=0;i<data.length;i++){
                        idData.push(data[i].CouponTypeID);

                        if(vipToolCoupon!=""){
                            var toolData = $('.setOffVipModule').find('.toolData .contentsData').eq(1).find("li[data-id='"+idData[i]+"']");
                            if(toolData.length!="0"){
                                var name =$(toolData).find('.name').html();
                                $.messager.alert('提示','你的员工优惠券列表已有'+name+'活动');
                                // $('#winTool').find('.warnning').children('span').eq(0).html('您的员工优惠券列表中已有');
                                // $('#winTool').find('.warnning').children('b').html(name);
                                callback(false);
                                return false;
                            }else{
                                callback(true);
                                isContinued = false;
                                return true;
                            }
                        }else{
                            callback(true);
                            isContinued = false;
                            return false;
                        }
                        if(isContinued == false){
                            return false;
                        }
                    }
                }
                if(type=='900'){
                    var idData =[];
                    for(var i=0;i<data.length;i++){
                        idData.push(data[i].SetoffPosterID);

                        if(vipToolPoster!=""){
                            var toolData = $('.setOffVipModule').find('.toolData .contentsData').eq(2).find("li[data-id='"+idData[i]+"']");
                            if(toolData.length!="0"){
                                var name =$(toolData).find('.name').html();
                                $.messager.alert('提示','你的员工集客海报列表已有'+name+'活动');
                                // $('#winTool').find('.warnning').children('span').eq(0).html('您的员工集客海报列表中已有');
                                // $('#winTool').find('.warnning').children('b').html(name);
                                callback(false);
                                return false;
                            }else{
                                callback(true);
                                isContinued = false;
                                return true;
                            }
                        }else{
                            callback(true);
                            isContinued = false;
                            return false;
                        }
                        if(isContinued == false){
                            return false;
                        }
                    }
                }

            }
        },

        //渲染tabel 1
        renderTable1: function (data) {
            debugger;
            var that=this;
            if(!data.CTWLEventInfoList){

                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabelAction.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                showHeader:false,
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.CTWLEventInfoList,
                sortName : 'brandCode',
                //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                frozenColumns : [ [ {
                    field : 'CTWEventId',
                    checkbox : true,
                    height:60
                } //显示复选框
                ] ],
                columns : [[
                    {field : 'Name',title : '活动名称',width:250,height:60,align:'left',resizable:false,sortable:true
                        ,formatter:function(value ,row,index) {
                        var str="";
                        if (value) {

                            var str ="<li>"+row.Name+"</li>";
                            str+="<li>"+row.StartDate+"至"+ row.EndDate+"</li>";
                            return str;
                        }
                    }
                    },
                    {field: 'OnfflineQRCodeId', title: '预览', width: 150,align: 'center', resizable: false
                        , formatter: function (value, row, index) {
                        var str="";
                        if (value) {
                            var str ="<li data-url='"+row.OnfflineQRCodeUrl+"' style='color:#00a0e8'><b class='screen'>预览</b></li>";

                            return str;
                        }
                    }
                    }
                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabelAction.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataNoticeList.hide();
                    }else{
                        $('#gridTable1').parents('.datagrid').hide();
                        that.elems.dataNoticeList.show();
                    }
                },
                onClickRow:function(rowindex,rowData){

                },onClickCell:function(rowIndex, field, value){
                    if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }
            });
        },

        //渲染tabel 2
        renderTable2: function (data,ii) {
            debugger;
            var that=this;
            if(!data.CouponTypeList){

                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabelCoupon.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                showHeader:false,
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.CouponTypeList,
                sortName : 'brandCode',
                //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                frozenColumns : [ [ {
                    field : 'CouponTypeID',
                    checkbox : true,
                    height:60
                } //显示复选框
                ] ],
                columns : [[
                    {field : 'CouponTypeName',title : '优惠券名称',width:250,height:60,align:'left',resizable:false
                        ,formatter:function(value ,row,index) {
                        if (value) {
                            return value
                        }
                    }
                    },

                    {field : 'ValidityPeriod',title : '优惠券有效期',width:150,height:60,align:'left',resizable:false,
                        formatter:function(value,row,index){
                            if (value) {
                                return value
                            }
                        }},
                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabelCoupon.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataNoticeList.hide();
                    }else{
                        $('#gridTable2').parents('.datagrid').hide();
                        that.elems.dataNoticeList.show();
                    }
                },
                onClickRow:function(rowindex,rowData){

                },onClickCell:function(rowIndex, field, value){
                    if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                },
            });

        },
        //渲染tabel 3
        renderTable3: function (data) {
            debugger;
            var that=this;
            if(!data.List){

                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabelImgtext.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                showHeader:false,
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.List,
                sortName : 'brandCode',
                //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                //idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                frozenColumns : [ [ {
                    field : 'TextId',
                    checkbox : true,
                    height:60
                } //显示复选框
                ] ],
                columns : [[
                    {field : 'CoverImageUrl',title : '图片',width:50,height:60,align:'left',resizable:false,sortable:true
                        ,formatter:function(value ,row,index) {
                        var str="";
                        if (value) {

                            var str ="<img style='width:40px;height:40px' src='"+row.CoverImageUrl+"'>";
                            return str;
                        }
                    }
                    },
                    {field: 'Title', title: '图文', width: 250,height:60,align: 'center', resizable: false
                        , formatter: function (value, row, index) {
                        var str="";
                        if (value) {
                            var str ="<li style='text-align:left;'>"+row.Title+"</li>";
                                str+= "<li style='text-align:left;'>"+row.Text+"</li>";
                            return str;
                        }
                    }
                    }
                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabelImgtext.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataNoticeList.hide();
                    }else{
                        $('#gridTable3').parents('.datagrid').hide();
                        that.elems.dataNoticeList.show();
                    }
                },
                onClickRow:function(rowindex,rowData){

                },onClickCell:function(rowIndex, field, value){
                    if(field=="addOpt"||field=="addOptdel"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }
            });
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            var isTools = true;//true显示图表，
            // 获取分销工具列表；
            that.loadData.getExtendList(function(data){
                // 获取集客行动奖励数据
                if(data.List!=null){
                    if(data.List.length!=0){
                        that.getAction(data);
                        isTools = true;
                    }
                    else{
                        isTools = false;
                    }
                }else{
                    isTools = false;
                }
                $('.ModuleList .title').find('li').eq(0).trigger('click');

            });
            // 获取分销效果
            that.loadData.getExtendStatistics(function(data){
                that.getStatistics(data,isTools);
            });
            $.util.stopBubble(e);
        },

        //加载更多的资讯或者活动
        loadMoreData1: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getCTWLEventList(function(data){
                that.renderTable1(data);
            });
        },
        loadMoreData2: function (currentPage) {
            var that = this;
            this.loadData.getCoupon.PageIndex = currentPage;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getCouponTypeList(function(data){
                that.renderTable2(data);
            });
        },
        //获取分销工具列表
        getAction:function(data){
            var that = this;
            var list = data.List;
            that.loadData.setOff.SetoffEventID = data.SetoffEventID;
            //that.loatData.setOff.getActionList = list;
            var ToolsInfoList1 =[];
            var ToolsInfoList2 =[];
            var ToolsInfoList3 =[];
            var ToolsInfoList4 =[];
            for(var j = 0;j<list.length;j++){
                var toolType = list[j].ToolType;
                if(toolType=='CTW'){
                    var listData = list[j];
                    listData=listData?listData:[];
                    ToolsInfoList1.push(listData);
                }else if(toolType=='Coupon'){
                    var listData = list[j];
                    listData=listData?listData:[];
                    ToolsInfoList2.push(listData);
                }else if(toolType=='SetoffPoster'){
                    var listData = list[j];
                    listData=listData?listData:[];
                    ToolsInfoList3.push(listData);
                }else if(toolType=='Material'){
                    var listData = list[j];
                    listData=listData?listData:[];
                    ToolsInfoList4.push(listData);
                }
            }
            if(list.length!=0){
                $('.toolData').find('span').html('已发布');
            }
            that.loadData.setOff.toolAction = ToolsInfoList1;
            that.loadData.setOff.toolCoupon= ToolsInfoList2;
            that.loadData.setOff.toolPoster = ToolsInfoList3;
            that.loadData.setOff.toolImgtext = ToolsInfoList4;
            var html1=bd.template("tpl_action",{list:ToolsInfoList1});
            var html2=bd.template("tpl_coupon",{list:ToolsInfoList2});
            var html3=bd.template("tpl_poster",{list:ToolsInfoList3});
            var html4=bd.template("tpl_imgText",{list:ToolsInfoList4});
            $(".saleActivityTool").html(html1);
            $(".couponTool").html(html2);
            $(".posterTool").html(html3);
            $('.imageTextTool').html(html4);
        },
        //获取分销效果
        getStatistics:function(data,isTools){
            var list = data.List;
            var html = "<div class='noContents'><p>你还没有添加工具<a>请点击添加工具</a>，拓展工具可以更好的帮助分销商拓展下线哟!</p></div>";
            for(var i=0;i<list.length;i++){
                if(isTools==true){
                    if(Number(list[i].LinkRelativeCount)>=0){
                        var html1 = "<p><em class='totle'>"+list[i].Count+"</em><i>人</i></p><span><i class='top'></i><em>"+list[i].LinkRelativeCount+"</em>人</span>";
                        var html2 = "<p><em class='totle'>"+list[i].Count+"</em><i>次</i></p><span><i class='top'></i><em>"+list[i].LinkRelativeCount+"</em>次</span>";
                    }else{
                        list[i].LinkRelativeCount = parseInt(list[i].LinkRelativeCount);
                        var html1 = "<p><em class='totle'>"+list[i].Count+"</em><i>人</i></p><span><i class='bottom'></i><em>"+-list[i].LinkRelativeCount+"</em>人</span>";
                        var html2 = "<p><em class='totle'>"+list[i].Count+"</em><i>次</i></p><span><i class='bottom'></i><em>"+-list[i].LinkRelativeCount+"</em>次</span>";
                    }
                    //近30天拓展工具推送次数
                    if(list[i].Type=='1'){
                        $('.day30Area .itemTotle').find('.content').eq(0).children('.record').html(html2);
                    }//近30天新增分销商
                    else if(list[i].Type=='4'){
                        $('.day30Area .itemTotle').find('.content').eq(1).children('.record').html(html1);
                    }//微信图文
                    else if(list[i].Type=='2'){
                        $('#weChat').find('.record').html(html1);
                    }//活动
                    else if(list[i].Type=='3'){
                        $('#expandActivity').find('.record').html(html1);
                    }//优惠券
                    else if(list[i].Type=='5'){
                        $('#coupon').find('.record').html(html1);
                    }//招募海报
                    else if(list[i].Type=='6'){
                        $('#poster').find('.record').html(html1);
                    }
                }else{
                    $('.day30Totle').html(html);
                }
            }
        },
        loadData: {
            args: {
                PageIndex:1,
                PageSize:999
            },
            tag:{
                VipId:"",
                orderID:'',
                toolDList:[]
            },
            setOff:{
                commonList:[],
                posterList:[],
                setOffType:"",
                Message:[],
                toolImgtext:[],
                toolAction:[],
                toolCoupon:[],
                toolPoster:[],
                isStaffChange:true,
                isVipChange:true,
                isData:true,
                SetoffEventID:''
            },
            reward:{
                vipLock:'',
                vipOrderTimers:'',
                vipRegAwardType:'',
                vipOrderPer:'',
                vipRegPrize:'',
                staffLock:'',
                staffOrderTimers:'',
                staffOrderPer:'',
                staffRegPrize:''
            },
            getCoupon:{
                PageIndex:1,
                PageSize: 999
            },
            goods:{
                EventId:"",
                EventName:"",
                BeginTime:"",
                EndTime:""
            },
            setPoster:{
                SetoffPosterID:"",
                Name:"",
                ImageUrl:""
            },
            opertionField:{},
            //获取图文素材活动列表
            getNewsList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.RTExtend.GetMaterialTextList',
                        // 'EventName':this.seach.EventName,
                        // 'EventStatus':this.seach.EventStatus,
                        // 'BeginTime':this.seach.BeginTime,
                        // 'EndTime':this.seach.EndTime,
                        'PageIndex':this.args.PageIndex,
                        'PageSize':this.args.PageSize
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取创意仓库活动列表
            getCTWLEventList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.GetCTWLEventList',
                        'type':'Product',
                        // 'EventName':this.seach.EventName,
                        // 'EventStatus':this.seach.EventStatus,
                        // 'BeginTime':this.seach.BeginTime,
                        // 'EndTime':this.seach.EndTime,
                        'PageIndex':this.args.PageIndex,
                        'PageSize':this.args.PageSize
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取优惠券活动列表
            getCouponTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'Marketing.Coupon.GetCouponTypeList',
                        'type':'Product',
                        // 'EventName':this.seach.EventName,
                        // 'EventStatus':this.seach.EventStatus,
                        // 'BeginTime':this.seach.BeginTime,
                        // 'EndTime':this.seach.EndTime,
                        'IsEffective':true,
                        'SurplusCount':1,
                        'PageIndex':this.getCoupon.PageIndex,
                        'PageSize':this.getCoupon.PageSize
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //设置分销工具
            setExtend: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.RTExtend.SetSetoffTools',
                        'CommonList':this.setOff.commonList,
                        'PosterList':this.setOff.posterList
                    },
                    beforeSend: function () {
                        $.util.isLoading()
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data,data.IsSuccess);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取拓展工具列表
            getExtendList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.RTExtend.GetSetoffToolList'
                    },
                    beforeSend: function () {
                        $.util.isLoading()
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取分销效果；
           getExtendStatistics: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'SuperRetailTrader.RTExtend.GetExtendStatistics'
                    },
                    beforeSend: function () {
                        $.util.isLoading()
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //设置集客集客海报
            setoffPoster: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.SetoffPoster',
                        'SetoffPosterID':this.setPoster.SetoffPosterID,
                        'Name':this.setPoster.Name,
                        'ImageUrl':this.setPoster.ImageUrl
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //取消发布集客工具
            updateSetoffTool: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.UpdateSetoffToolsStatus',
                        'SetoffToolIDList':this.tag.toolDList
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data,data.IsSuccess);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //发送通知
            sendNotice: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.SendNotice',
                        'NoticeInfoList':this.setOff.Message
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data.Data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
        }

    };
    page.init();
});

