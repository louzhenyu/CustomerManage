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
            this.chartsData();
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
        //加载charts
        chartsData:function(){
            var that = this;
            // 获取集客效果数据
            that.loadData.getRSetoffHomeList(function(data){
                var list = data.result;
                if(list!=null){
                    var chartsData1 = [];
                    var chartsData2 = [];
                    for(var i=0;i<list.length;i++){
                        if(list[i].SetoffType=="会员集客"){
                            var listData = list[i];
                            var html="<ul><li>会员转化率:"+list[i].VipPer+"%</li></ul>";
                            $('.setOffVipModule').find('.chartsData').html(html);
                            chartsData1.push(listData.OnlyFansCount,listData.VipCount);
                        }else if(list[i].SetoffType=="员工集客"){
                            var listData = list[i];
                            var html="<ul><li>会员转化率:"+list[i].VipPer+"%</li></ul>";
                            $('.setOffStaffModule').find('.chartsData').html(html);
                            chartsData2.push(listData.OnlyFansCount,listData.VipCount);
                        }

                    }
                    if(chartsData1[0]!='0'||chartsData1[1]!='0'){
                        $('#vipCharts').highcharts({
                            chart: {
                                plotBackgroundColor:null,
                                plotBorderWidth: null,
                                plotShadow: false,
                                width:'500'
                            },
                            colors:
                                ['#f3bc47','#ed7d31','#5b9bd5']
                            ,
                            title: {
                                text: '新增集客',
                                style:{fontFamily:"Microsoft YaHei",
                                color:'#666',
                                fontSize:'18px'
                                }
                            },
                            legend:{
                                align:'right',
                                layout:'vertical',
                                reflow:'true',
                                verticalAlign:'bottom',
                                x: 0,
                                y: 0
                            },
                            credits: {
                                enabled: false
                            },
                            plotOptions: {
                                pie: {
                                    allowPointSelect: true,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: false,
                                        color: '#000000',
                                        connectorColor: '#000000',
                                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                    },
                                    showInLegend: false
                                }
                            },
                            series: [{
                                type: 'pie',
                                name: '总览',
                                data: [['粉丝(未注册)',chartsData1[0]],['注册会员',chartsData1[1]]
                                ]
                            }]
                        });
                        $('.setOffVipModule').find('.chartsData').show();
                        //$('#vipCharts').parents('.contents').children('.noContents').hide();
                        //return false;
                    }
                    if(chartsData2[0]!='0'||chartsData2[1]!='0'){
                        $('#staffCharts').highcharts({
                            chart: {
                                plotBackgroundColor:null,
                                plotBorderWidth: null,
                                width:'500',
                                plotShadow: false,
                            },
                            colors:
                                ['#f3bc47','#ed7d31','#5b9bd5']
                            ,
                            title: {
                                text: '新增集客',
                                style:{fontFamily:"Microsoft YaHei",
                                    color:'#666',
                                    fontSize:'18px'
                                }
                            },
                            legend:{
                                align:'right',
                                layout:'vertical',
                                reflow:'true',
                                verticalAlign:'bottom',
                                x: 0,
                                y: 0
                            },
                            credits: {
                                enabled: false
                            },
                            plotOptions: {
                                pie: {
                                    allowPointSelect: true,
                                    cursor: 'pointer',
                                    dataLabels: {
                                        enabled: false,
                                        color: '#000000',
                                        connectorColor: '#000000',
                                        format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                                    },
                                    showInLegend: false
                                }
                            },
                            series: [{
                                type: 'pie',
                                name: '总览',
                                data: [['粉丝(未注册)',chartsData2[0]],['注册会员',chartsData2[1]]
                                ]
                            }]
                        });
                        $('.setOffStaffModule').find('.chartsData').show();
                        //$('#staffCharts').parents('.contents').children('.noContents').hide();
                        //return false;
                    }


                }
            })

        },
        //加载商品列表
        initEvent: function () {
            var that = this;
            //点击添加工具添加活动
			//that
            that.elems.sectionPage.delegate(".addTools","click", function (e) {

                debugger;
                that.updateTool();
                var type = $(this).attr('data-type');
                var ii = $(this).parents('.blockModul').find('.toolData').children('.title').find('li.current').index();
                $('#winTool').attr('data-type',type);
                $('#winTool').find('#opt').show();
                $('#winTool').find("#setPosterName").val('');
                $('#winTool').find("#setPosterName").attr('data-id','');
                $('#winTool').find("#setOffBack").attr('src','');
                $(that.elems.operation.find("li").get(ii)).trigger("click");

            });
            that.elems.operation.delegate("li","click",function(e){
                that.elems.operation.find("li").removeClass("on");
                $(this).addClass('on');
                var type =$('#winTool').attr('data-type');
                var value=$(this).attr("data-field7").toString();
                //var html = "<div class='notice' style='text-align:center;'>暂无活动，请新增相关活动</div>";
                if(value=='900'){
                    $('#winTool').find('.datagrid').hide();
                    $('#winTool').find('.toolList').hide();
                    that.elems.dataNoticeList.hide();
                    $('#winTool').find('#setOfferPoster').show();
                }else if(value=='0'){
                    $.util.partialRefresh($('#gridTable1'));
                    $('#winTool').find('#setOfferPoster').hide();
                    that.loadToolData(0,type);
                    $('#gridTable2').parents('.datagrid').hide();
                    $('#winTool').find('.toolList').show();
                    $('#winTool').find('.toolList').children('.commonBtn').html('新建活动');
                }else if(value=='100'){
                    $.util.partialRefresh($('#gridTable2'));
                    $('#winTool').find('#setOfferPoster').hide();
                    that.loadToolData(1,type);
                     $('#gridTable1').parents('.datagrid').hide();
                    $('#winTool').find('.toolList').show();
                    $('#winTool').find('.toolList').children('.commonBtn').html('新建优惠券');
                }
            });

            $('#tableWrap').delegate('.reload','click',function(){
                var value=that.elems.operation.find("li[class='on']").attr("data-field7").toString();
                var SetoffType = $('#winTool').attr('data-type');
                if(value=='0'){
                    that.loadToolData(0,SetoffType);
                    // that.loadData.getCTWLEventList(function(data){
                    //     that.renderTable1(data,0);
                    // });
                }else if(value=='100'){
                    that.loadToolData(1,SetoffType);
                    // that.loadData.getCouponTypeList(function(data){
                    //     that.renderTable2(data,1);
                    // });
                }
            })

            $('#tableWrap').delegate('.commonBtn','click',function(){
                var value=that.elems.operation.find("li[class='on']").attr("data-field7").toString();
                if(value=='0'){
                    window.open( "/module/CreativityWarehouse/CreativeWarehouseView/QueryList.aspx");

                }else if(value=='100'){
                    window.open( "/module/couponManage/querylist.aspx");
                }
            })
            // 新增工具
            $('#winTool').delegate('.message','click',function(){
                 //活动type=0，优惠券type=100，集客海报type=900
                var SetoffType = $('#winTool').attr('data-type'),//会员集客=1；员工集客=2
                    // listAction = that.elems.tabelAction.datagrid("getChecked"),//获取创意仓库选中 list
                    // listCoupon = that.elems.tabelCoupon.datagrid("getChecked"),//获取优惠券选中 list
                    name = $('#winTool').find('#setPosterName').val(),
                    imgUrl = $('#winTool').find("#setOffBack").attr('src'),
                    setPosterId =$('#winTool').find('#setPosterName').attr('data-id'),//编辑海报时，已创建的海报才有id。
                    contentVip = $('.setOffVipModule').find('.contentsData'),
                    noContentVip = $('.setOffVipModule').find('.noContents'),
                    toolTitleVip = $('.setOffVipModule').find('.toolData').children('.title'),
                    contentStaff = $('.setOffStaffModule').find('.contentsData'),
                    noContentStaff = $('.setOffStaffModule').find('.noContents'),
                    toolTitleStaff = $('.setOffStaffModule').find('.toolData').children('.title'),
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
                var listPoster = [{"ImageUrl":imgUrl,"Name":name}];//创建集客海报list
                if(SetoffType=='1'){
                    if(listAction!=''&&isUniform==true){
                        that.fiterData(listAction,0,SetoffType,function(data){
                            if(data!=false) {
                                var listData = that.loadData.setOff.toolVipAction;
                                listAction = listData.concat(listAction);
                                that.loadData.setOff.toolVipAction = listAction;
                                var html = bd.template("tpl_action", {list: listAction});
                                $("#VipActionTool").html(html);
                                $('.setOffVipModule').find(".toolSetOff").html('已选择');
                            }else{
                                isUniform =false;
                                return false;
                            }
                        })
                    }
                    if(listCoupon!=''&&isUniform==true){
                        that.fiterData(listCoupon,100,SetoffType,function(data){
                            if(data!=false) {
                                var listData = that.loadData.setOff.toolVipCoupon;
                                listCoupon = listData.concat(listCoupon);
                                that.loadData.setOff.toolVipCoupon = listCoupon;
                                var html = bd.template("tpl_coupon", {list: listCoupon});
                                $("#VipCouponTool").html(html);
                                $('.setOffVipModule').find(".toolSetOff").html('已选择');
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
                            that.fiterData(listPoster,900,SetoffType,function(data){
                                if(data!=false) {
                                    var listData = that.loadData.setOff.toolVipPoster;
                                    listPoster = listData.concat(listPoster);
                                    that.loadData.setOff.toolVipPoster = listPoster;
                                    var html = bd.template("tpl_poster", {list: listPoster});
                                    $("#VipPosterTool").html(html);
                                    $('.setOffVipModule').find(".toolSetOff").html('已选择');
                                }else{
                                    isUniform =false;
                                    return false;
                                }
                            })
                        }

                    }
                    contentVip.show();
                    noContentVip.hide();
                    toolTitleVip.show();
                    if(value=='0'){
                        toolTitleVip.find('li').eq(0).trigger('click');
                    }else if(value=='100'){
                        toolTitleVip.find('li').eq(1).trigger('click');
                    }else if(value=='900'){
                        toolTitleVip.find('li').eq(2).trigger('click');
                    }

                }
                if(SetoffType=='2'){
                    if(listAction!=''&&isUniform==true){
                        that.fiterData(listAction,0,SetoffType,function(data){
                            if(data!=false) {
                                var listData = that.loadData.setOff.toolStaffAction;
                                listAction = listData.concat(listAction);
                                that.loadData.setOff.toolStaffAction = listAction;
                                var html = bd.template("tpl_action", {list: listAction});
                                $("#StaffActionTool").html(html);
                                $('.setOffStaffModule').find(".toolSetOff").html('已选择');
                            }else{
                                isUniform =false;
                                return false;
                            }
                        })
                    }
                    if(listCoupon!=''&&isUniform==true){
                        that.fiterData(listCoupon,100,SetoffType,function(data){
                            if(data!=false) {
                                var listData = that.loadData.setOff.toolStaffCoupon;
                                listCoupon = listData.concat(listCoupon);
                                that.loadData.setOff.toolStaffCoupon = listCoupon;
                                var html = bd.template("tpl_coupon", {list: listCoupon});
                                $("#StaffCouponTool").html(html);
                                $('.setOffStaffModule').find(".toolSetOff").html('已选择');
                            }else{
                                isUniform =false;
                                return false;
                            }
                        })
                    }
                    if(isData!=false&&isUniform==true){
                        if(setPosterId!=""){
                            that.loadData.setPoster.SetoffPosterID = setPosterId;
                            that.loadData.setPoster.Name = name;
                            that.loadData.setPoster.ImageUrl = imgUrl;
                            that.loadData.setoffPoster(function(data){
                                var id = data.SetoffPosterID;
                                var namePoster = data.Name;
                                var imgUrlPoster =data.ImageUrl;
                                var $tt = $('.setOffStaffModule').find('.contentsData').find("li[data-id='"+id+"']");
                                $tt.find('.name').html(namePoster);
                                $tt.attr('data-url',imgUrlPoster);
                            })
                        }else{
                            that.fiterData(listPoster,900,SetoffType,function(data){
                                if(data!=false) {
                                    var listData = that.loadData.setOff.toolStaffPoster;
                                    listPoster = listData.concat(listPoster);
                                    that.loadData.setOff.toolStaffPoster = listPoster;
                                    var html = bd.template("tpl_poster", {list: listPoster});
                                    $("#StaffPosterTool").html(html);
                                    $('.setOffStaffModule').find(".toolSetOff").html('已选择');
                                }else{
                                    isUniform =false;
                                    return false;
                                }
                            })
                        }

                    }
                    contentStaff.show();
                    noContentStaff.hide();
                    toolTitleStaff.show();
                    if(value=='0'){
                        toolTitleStaff.find('li').eq(0).trigger('click');
                    }else if(value=='100'){
                        toolTitleStaff.find('li').eq(1).trigger('click');
                    }else if(value=='900'){
                        toolTitleStaff.find('li').eq(2).trigger('click');
                    }

                }
                if(isUniform==true){
                    $('#winTool').window('close');
                    that.loadToolData(0,SetoffType);
                    that.loadToolData(1,SetoffType);
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
            //集客已选活动内容切换
            $('.toolData .title').delegate('li','click',function(){
                $(this).parents('.toolData').find('li').removeClass('current');
                $(this).addClass('current');
                var ii = $(this).index();
                var isStaff = $(this).parents('.Module').hasClass('setOffStaffModule');
                if(isStaff){
                    $('.setOffStaffModule').find('.contentsData').hide();
                    $('.setOffStaffModule').find('.contentsData').eq(ii).show();
                }else{
                    $('.setOffVipModule').find('.contentsData').hide();
                    $('.setOffVipModule').find('.contentsData').eq(ii).show();
                }
            });

            // 集客行动工具收起，展开
            $('.ModuleContent').delegate('.point','click',function(){
                var parModul = $(this).parents('.ModuleContent').children('.blockModul');
                var isHidden = parModul.css('display');
                if(isHidden=="none"){
                    $(parModul).slideDown(800);
                    $(this).children('span:first').html('收起');
                    $(this).children('span:last').removeClass('bottom');
                    $(this).children('span:last').addClass('top');
                }else{
                    $(parModul).slideUp(800);
                    $(this).children('span:first').html('展开');
                    $(this).children('span:last').removeClass('top');
                    $(this).children('span:last').addClass('bottom');
                }
            });

            //点击设置集客行动展开集客行动
            $('.charts').delegate('a','click',function(){
                var parModul = $(this).parents('.Module').find('.blockModul');
                var pointBtn = $(this).parents('.Module').find('.point');
                var isHidden = parModul.css('display');
                if(isHidden=="none"){
                    $(parModul).slideDown(800);
                    $(pointBtn).children('span:first').html('收起');
                    $(pointBtn).children('span:last').removeClass('bottom');
                    $(pointBtn).children('span:last').addClass('top');
                }
            });

            //集客行动禁用，启用
            $('.lockBack').delegate('.cirle','click',function(){
                var isCheck = $(this).parents('.lockBack').hasClass('on');
                var cirle = $(this).parents('.lockBack').children('.cirle');
                
                var parConents = $(this).parents('.contents');
                if(isCheck){
                   // $(parConents).find('#rewardRule').combobox({disabled:true});
                    $(this).parents('.lockBack').attr('data-eabled','90');
                    $(this).parents('.lockBack').removeClass('on');
                    $(cirle).animate({left:"1"});
                    $(parConents).find('input').attr('readonly',true);
                    var name =undefined;
                    //that.updateCanel(name,isCheck);
                    alert('确认发布后禁用生效');
                    $(parConents).find('.editArea').find("p[class='on']").addClass('disab');
                   // $(parConents).find('.editArea').find("p[class='on']").removeClass('on');
                    //$.messager.alert('提示','确认发布后生效');
                }else{
                   $(parConents).find('#rewardRule').combobox({disabled:false});
                    $(this).parents('.lockBack').attr('data-eabled','10');
                    $(this).parents('.lockBack').addClass('on');
                    $(cirle).animate({left:'20'});
                    $(parConents).find('input').removeAttr('readonly');
                    //$(parConents).find('.editArea').find("p[class='disab']").addClass('on');
                    $(parConents).find('.editArea').find("p").removeClass('disab');
                }
            })

            //集客行动奖励规则
            $('.editArea p').click(function(){
                var isChecked = $(this).parents('.contents').find('.lockBack').attr('data-eabled');
                if(isChecked =='10'){
                    $(this).parents('.editArea').find('p').removeClass('on');
                    $(this).addClass('on');
                }
            })

            //集客工具移除
            $('.toolData').delegate('.remove','click',function(){
                //var that = this;
                var par =$(this).parents('li');
                var name = $(par).find('.name').html();
                var ii =par.index();
                var type = par.attr('data-type');
                var setOffType = par.parents('.blockModul').find('.addTools').attr('data-type');
                $.messager.confirm('提示','确定移除吗？',function(r){
                    if(r){
                        //that.loadData.updateSetoffTool();
                        par.remove();
                        alert(""+name+"移除成功");
                        that.removeTool(type,setOffType,ii);
                    }
                })
            })
            //集客工具取消发布
            $('.toolData').delegate('.removeDlist','click',function(){
                var par =$(this).parents('li');
                var idData = [];
                par.each(function(i){
                    var id = $(par).attr('data-toolid');
                    idData.push(id);
                })
                var ii =par.index();
                var type = par.attr('data-type');//创意仓库活动=1;优惠券活动=2;集客海报=3;
                var name = par.find('.name').html();
                var setOffType = par.parents('.blockModul').find('.addTools').attr('data-type');//会员集客=1；员工海报=2；

                that.loadData.tag.toolDList =idData;
                that.updateCanel();
                $('#winCanel').find('.btnWrap').show();
                $('#winCanel').attr('data-name',name);
                $('#winCanel').attr('data-type',type);
                $('#winCanel').attr('data-index',ii);
                $('#winCanel').attr('data-setOffType',setOffType);
            })
            $('#winCanel').delegate('.message','click',function(){
                $('#winCanel').window('close');
                that.loadData.updateSetoffTool(function(data,IsSuccess){
                    if(IsSuccess){
                        var name = $('#winCanel').attr('data-name');
                        var type = $('#winCanel').attr('data-type');
                        var ii = $('#winCanel').attr('data-index');
                        var setOffType = $('#winCanel').attr('data-setOffType');
                        //par.remove();
                        alert(""+name+"已停止发布")
                        //that.updateCanel(name);
                        //$.messager.alert("提示",""+name+"取消成功");
                        that.removeTool(type,setOffType,ii);
                    }
                });
            })


            //集客海报编辑
            $(".toolData").delegate('.exitPoster',"click",function(){
                var par =$(this).parents('li');
                var id = $(par).attr('data-id');
                var url = $(par).attr('data-url');
                var name = $(par).find('.name').html();
                that.updateTool(id);
                var type = $(this).parents('.blockModul').find('.addTools').attr('data-type');
                $(that.elems.operation.find("li").get(2)).trigger("click");
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
                that.loadData.getSetOffAction(function(data){
                    if(data.GetSetOffActionInfoList==''){
                        $.messager.alert('提示','请先设置集客行动');
                    }else{
                        $('#winMessage').attr('data-type','2');
                        that.updateMessage();
                    }
                });
            });

            //确认发布
            $('#saveSetOff').bind('click',function(){
                var setOff=[];
                var setMessage=[];
				var tt={};
                var isSuccess = true;
				var SetoffRegAwardType = $('#rewardRule').combobox('getValue'),
                isStaffChange=true,
                    isVipChange=true;
				//var SetoffOrderTimers=$(item).find('.editArea').find("p[class='on']").attr('data-type');
				if(SetoffRegAwardType=='1'){
					var SetoffVipName ='现金';

                    var SetoffViptill ='元';
				}else{
					var SetoffVipName ='积分';

                    var SetoffViptill ='分';
				}
				tt.SetoffType="1";
				tt.SetoffRegAwardName="现金";
				tt.setoffRegPrize = $('.Module').eq(0).find('.setoffRegPrize').val();
				tt.setoffOrderPer = $('.Module').eq(0).find('.setoffOrderPer').val();
				tt.setoffOrderTimers =$('.Module').eq(0).find('.editArea').find(".on").attr('data-name');
				tt.activeLL = $('.Module').eq(0).find('.contentsData').find('li').length;
				tt.setoffVipName=SetoffVipName;
                tt.SetoffViptill =SetoffViptill;
				tt.setoffVipRegPrize = $('.Module').eq(1).find('.setoffRegPrize').val()||0;
				tt.setoffVipOrderPer = $('.Module').eq(1).find('.setoffOrderPer').val();
				tt.setoffOrderVipTimers =$('.Module').eq(1).find('.editArea').find(".on").attr('data-name');
				tt.activeVipLL = $('.Module').eq(1).find('.contentsData').find('li').length;
				setMessage.push(tt);
				var data =setMessage;

                $('.Module').each(function(i){
                    var item =$(this).find('.blockModul'),
                        inputs = item.find('input'),
                        onRewardRule =item.find('.editArea').find('.on').length,
                        onRewardType =item.find('.editArea').find('.on').attr('data-type'),
                        lockRule = item.find('.lockBack').attr('data-eabled'),
                        vipLock = that.loadData.reward.vipLock,
                        vipOrderTimers =that.loadData.reward.vipOrderTimers,
                        vipRegAwardType =that.loadData.reward.vipRegAwardType,
                        vipOrderPer =that.loadData.reward.vipOrderPer,
                        vipRegPrize =that.loadData.reward.vipRegPrize,
                        staffLock =that.loadData.reward.staffLock,
                        staffOrderTimers =that.loadData.reward.staffOrderTimers,
                        staffOrderPer =that.loadData.reward.staffOrderPer,
                        staffRegPrize =that.loadData.reward.staffRegPrize;
                    inputs.click(function(){
                        $(this).css('border','1px solid #ccc');
                    })
                    if(i==0){
                        if(lockRule=='10'){
                            if(onRewardRule=='0'){
                                $.messager.alert('提示','员工集客活动提成限制规则必须选择一种');
                                //$tt.css('border','1px solid red');
                                isSuccess=false;
                                return false;
                            }
                            if(parseInt(onRewardType)!=parseInt(staffOrderTimers)){

                                that.loadData.setOff.isStaffChange =false;
                            }
                            inputs.each(function(j){
                                var $tt = $(this);
                                if(j==0){

                                    if($tt.val()==""){
                                        $.messager.alert('提示','员工集客活动注册成功奖励不能为空');
                                        $tt.css('border','1px solid red');
                                        isSuccess=false;
                                        return false;
                                    }
                                    if($tt.val()!=staffRegPrize){

                                        that.loadData.setOff.isStaffChange =false;

                                    }

                                }
                                if(j==1){
                                    if($tt.val()==""){
                                        $.messager.alert('提示','员工集客活动销售成功奖励不能为空');
                                        $tt.css('border','1px solid red');
                                        isSuccess=false;
                                        return false;
                                    }
                                    if($tt.val()!=staffOrderPer){

                                        that.loadData.setOff.isStaffChange =false;
                                    }
                                }
                            })
                        }
                        if(lockRule!=staffLock){

                            that.loadData.setOff.isStaffChange=false;
                        }
                        if(isSuccess==false){
                            return false;
                        }
                    }if(i==1){

                        if(lockRule=='10'){
                            if(onRewardRule=='0'){
                                $.messager.alert('提示','会员集客活动提成限制规则必须选择一种');
                                //$tt.css('border','1px solid red');
                                isSuccess=false;
                                return false;
                            }
                            if(onRewardType!=vipOrderTimers){

                                that.loadData.setOff.isVipChange=false;
                            }
                            if(SetoffRegAwardType!=vipRegAwardType){

                                that.loadData.setOff.isVipChange=false;
                            }
                            inputs.each(function(j){
                                var $tt = $(this);

                                if(j==3){
                                    if($tt.val()==""){
                                        $.messager.alert('提示','会员集客活动注册成功奖励不能为空');
                                        $tt.css('border','1px solid red');
                                        isSuccess=false;
                                        return false;
                                    }
                                    if($tt.val()!=vipRegPrize){
                                        that.loadData.setOff.isVipChange =false;
                                    }

                                }
                                if(j==4){
                                    if($tt.val()==""){
                                        $.messager.alert('提示','会员集客活动销售成功奖励不能为空');
                                        $tt.css('border','1px solid red');
                                        isSuccess=false;
                                        return false;
                                    }
                                    if($tt.val()!=vipOrderPer){

                                        that.loadData.setOff.isVipChange =false;
                                    }
                                }
                            })
                        }
                        if(lockRule!=vipLock){

                            that.loadData.setOff.isVipChange=false;
                        }
                        if(isSuccess==false){
                            return false;
                        }
                    }
                })
                if(isSuccess!=false){
                    that.update(data);
                }
            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=140,H=30;
            $('#rewardRule').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":1,
                    "text":"现金"
                },{
                    "id":2,
                    "text":"积分",
                    "selected":true
                }]
            });
            $('#rewardRule').combobox({
                onSelect:function(){

                    var ii = $(this).combobox('getValue');
                    var hhml = $(this).parents('.editArea').find('.till');
                    var isChecked = $(this).parents('.contents').find('.lockBack').hasClass('on');

                    if(ii=='2'){
                        hhml.html('积分');
                    }else if(ii=='1'){
                        hhml.html('元');
                    }

            }});


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

            //设置集客行动
            $('#win').delegate(".message","click",function(e){
				var SetOff=[];
                var isData = $('#win').attr('data-isData');
                var isStaffChange = that.loadData.setOff.isStaffChange;//判断员工集客是否有改动，默认true，未改动
                var isVipChange = that.loadData.setOff.isVipChange;//判断会员集客是否有改动，默认true，未改动
				$('.Module').each(function(i){
                    var item = $(this);
                    var obj ={};
                    var setoffRegPrize = $(item).find('.setoffRegPrize');
                    var setoffOrderPer = $(item).find('.setoffOrderPer');
                    var onRules = $(item).find('.editArea').find("p[class='on']").attr('data-type');
                    var isEnabled = $(item).find('.lockBack').attr('data-eabled');
                    if(i==0){
                        obj.SetoffType='2';
                        obj.SetoffRegAwardType='1';
                    }
                    if(i==1){
                        obj.SetoffType='1';
                        obj.SetoffRegAwardType=$('#rewardRule').combobox('getValue');
                    }
                    obj.SetoffRegPrize=$(item).find('.setoffRegPrize').val()||0;
                    obj.SetoffOrderPer=$(item).find('.setoffOrderPer').val()||0;
                    obj.SetoffOrderTimers=$(item).find('.editArea').find("p[class='on']").attr('data-type');
                    obj.IsEnabled=$(item).find('.lockBack').attr('data-eabled');
                    var staffToolAction=[];
                    var staffToolCoupon=[];
                    var staffToolPoster=[];
                    var vipToolAction=[];
                    var vipToolCoupon=[];
                    var vipToolPoster=[];
                    var item1 =$(this).find('.toolData .contentsData').eq(0).find("li");
                    var item2 =$(this).find('.toolData .contentsData').eq(1).find('li');
                    var item3 =$(this).find('.toolData .contentsData').eq(2).find('li');
                    //会员集客，员工集客都有改动

                    if(isVipChange==false&&isStaffChange==false){
                        item1.each(function(j){
                            $tt = $(this);
                            var obj ={};
                            obj.ObjectId =$tt.attr('data-id');
                            obj.ToolType='CTW';
                            staffToolAction.push(obj);
                        })
                        item2.each(function(k){
                            $tt = $(this);
                            var obj ={};
                            obj.ObjectId =$tt.attr('data-id');
                            obj.ToolType='Coupon';
                            staffToolCoupon.push(obj);
                        })
                        item3.each(function(n){
                            $tt = $(this);
                            var obj ={};
                            obj.ImageUrl =$tt.attr('data-url');
                            obj.Name =$tt.find('.name').html();
                            staffToolPoster.push(obj);
                        })
                        obj.SetoffTools=staffToolAction.concat(staffToolCoupon);
                        obj.SetoffPosterList=staffToolPoster;
                        SetOff.push(obj);

                    }
                    //会员集客有改动，员工集客没有改动
                    else if(isVipChange==false&&isStaffChange==true){
                        if(i==0){
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
                            item3.each(function(n){
                                $tt = $(this);
                                var obj ={};
                                var dataNew =$tt.attr('data-new');
                                if(dataNew!='true'){
                                    obj.ImageUrl =$tt.attr('data-url');
                                    obj.Name =$tt.find('.name').html();
                                    staffToolPoster.push(obj);
                                }
                            })
                        }
                        if(i==1){
                            item1.each(function(j){
                                $tt = $(this);
                                var obj ={};
                                obj.ObjectId =$tt.attr('data-id');
                                obj.ToolType='CTW';
                                vipToolAction.push(obj);
                            })
                            item2.each(function(k){
                                $tt = $(this);
                                var obj ={};
                                obj.ObjectId =$tt.attr('data-id');
                                obj.ToolType='Coupon';
                                vipToolCoupon.push(obj);
                            })
                            item3.each(function(n){
                                $tt = $(this);
                                var obj ={};
                                obj.ImageUrl =$tt.attr('data-url');
                                obj.Name =$tt.find('.name').html();
                                vipToolPoster.push(obj);
                            })
                        }
                        obj.SetoffTools=staffToolAction.concat(staffToolCoupon,vipToolAction,vipToolCoupon);
                        obj.SetoffPosterList=staffToolPoster;
                        SetOff.push(obj);
                    }
                    //会员集客没有改动，员工集客有改动
                    else if(isVipChange==true&&isStaffChange==false){
                        if(i==0){
                            item1.each(function(j){
                                $tt = $(this);
                                var obj ={};
                                obj.ObjectId =$tt.attr('data-id');
                                obj.ToolType='CTW';
                                staffToolAction.push(obj);
                            })
                            item2.each(function(k){
                                $tt = $(this);
                                var obj ={};
                                obj.ObjectId =$tt.attr('data-id');
                                obj.ToolType='Coupon';
                                staffToolCoupon.push(obj);
                            })
                            item3.each(function(n){
                                $tt = $(this);
                                var obj ={};
                                obj.ImageUrl =$tt.attr('data-url');
                                obj.Name =$tt.find('.name').html();
                                staffToolPoster.push(obj);
                            })
                        }
                        if(i==1){
                            item1.each(function(j){
                                $tt = $(this);
                                var obj ={};
                                var dataNew = $tt.attr('data-new');
                                if(dataNew!='true'){
                                    obj.ObjectId =$tt.attr('data-id');
                                    obj.ToolType='CTW';
                                    vipToolAction.push(obj);
                                }

                            })
                            item2.each(function(k){
                                $tt = $(this);
                                var obj ={};
                                var dataNew = $tt.attr('data-new');
                                if(dataNew!='true'){
                                    obj.ObjectId =$tt.attr('data-id');
                                    obj.ToolType='Coupon';
                                    vipToolCoupon.push(obj);
                                }

                            })
                            item3.each(function(n){
                                $tt = $(this);
                                var obj ={};
                                var dataNew =$tt.attr('data-new')
                                if(dataNew!='true'){
                                    obj.ImageUrl =$tt.attr('data-url');
                                    obj.Name =$tt.find('.name').html();
                                    vipToolPoster.push(obj);
                                }
                            })
                        }
                        obj.SetoffTools=staffToolAction.concat(staffToolCoupon,vipToolAction,vipToolCoupon);
                        obj.SetoffPosterList=staffToolPoster;
                        SetOff.push(obj);

                    }
                    //会员集客，员工集客都没有改动
                    else if(isVipChange==true&&isStaffChange==true){
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
                        item3.each(function(n){
                            $tt = $(this);
                            var obj ={};
                            var dataNew =$tt.attr('data-new');
                            if(dataNew!='true'){
                                obj.ImageUrl =$tt.attr('data-url');
                                obj.Name =$tt.find('.name').html();
                                staffToolPoster.push(obj);
                            }
                        })
                        obj.SetoffTools=staffToolAction.concat(staffToolCoupon);
                        obj.SetoffPosterList=staffToolPoster;
                        SetOff.push(obj);
                    }
                })
                if(isData!=false){
                    that.loadData.setOff.SetOffActionInfo=SetOff;//发布集客行动
                    $('#win').window('close');
                    that.loadData.setOffAction(function(data,isSuccess){
                        if(isSuccess){
                            that.updateMessage();
                            $('#winMessage').attr('data-type','1');
                        }
                    });
                }else{
                    $.messager.alert('提示','请先设置集客行动');
                }


            });

            $('#win').delegate(".saveBtn","click",function(e){
                //if('')
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

            //集客发送通知复选框
			$('#winMessage').delegate(".checkBox","click",function(){
				var isChecked = $(this).hasClass('on');
				if(isChecked){
					$(this).removeClass('on');
				}else{
					$(this).addClass('on');
				}
			})

            $('.window').delegate(".panel-tool-close","click",function(e){
                alert('aa');
                var isTrue = $(this).parents('.window').find('#winMessage').length;
                console.log(isTrue);
                if(isTrue!='0'){
                    location.reload();
                }
            })
            //集客发送通知
			$('#winMessage').delegate(".message","click",function(e){
				var item = $('#winMessage').find('.on');
                if(item.length!='0'){
                    var noticeInfo =[];
                    var isSuccess =true;
                    var type = $('#winMessage').attr('data-type');
                    item.each(function(){
                        var obj ={};
                        var tt = $(this);
                        obj.NoticePlatformType = $(tt).parents('.editCheck').attr('data-type');
                        obj.SetoffEventID =$(tt).parents('.editCheck').attr('data-setoffeventid');
                        obj.Title = $(tt).parents('.editCheck').children('p').attr('data-name');
                        obj.Text = $(tt).parents('.contents').children('.editMessage').children('textarea').val();
                        if(obj.Text==""){
                            $.messager.alert('提示','所选通知内容不能为空');
                            isSuccess= false;
                        }else{
                            noticeInfo.push(obj);
                        }

                    })
                    that.loadData.setOff.Message = noticeInfo;
                    if(isSuccess){
                        that.loadData.sendNotice(function(data){
                            if(type=='1'){
                                $('#winMessage').window('close');
                                alert('集客行动及活动消息已发布');
                               $.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");
                            }else if(type=='2'){
                                $('#winMessage').window('close');
                                alert('活动消息已发布');
                                $.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");
                            }
                            else{
                                $('#winMessage').window('close');
                                alert('集客行动已发布');
                               $.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");
                            }

                        });
                    }
                }else{
                    $.messager.alert('提示','请选择至少一个');
                    //$('#winMessage').window('close');
                    //window.reload();
                    //alert('集客行动已发布');
                    //$.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");
                }
			})

            $('#winMessage').delegate(".saveBtn","click",function(e){
                var type = $('#winMessage').attr('data-type');
                if(type=='1'){
                    $('#winMessage').window('close');
                    alert('集客行动已发布');
                    $.util.toNewUrlPath( "/module/SetOffManage/Source.aspx");
                }else{
                    $('#winMessage').window('close');
                    location.reload()
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
        removeTool:function(type,setOffType,ii){
            var that=this;
            if(setOffType=='1'){
                if(type=='1'){
                    var list = that.loadData.setOff.toolVipAction;
                    list.splice(ii,1);
                    var html = bd.template("tpl_action", {list: list});
                    $("#VipActionTool").html(html);
                }
                else if(type=='2'){
                    var list = that.loadData.setOff.toolVipCoupon;
                    list.splice(ii,1);
                    var html = bd.template("tpl_coupon", {list: list});
                    $("#VipCouponTool").html(html);
                }
                else if(type=='3'){
                    var list = that.loadData.setOff.toolVipPoster;
                    list.splice(ii,1);
                    var html = bd.template("tpl_poster", {list: list});
                    $("#VipPosterTool").html(html);
                }
            }
            if(setOffType=='2'){

                if(type=='1'){

                    var list = that.loadData.setOff.toolStaffAction;
                    list.splice(ii,1);
                    var html = bd.template("tpl_action", {list: list});
                    $("#StaffActionTool").html(html);
                }
                else if(type=='2'){
                    var list = that.loadData.setOff.toolStaffCoupon;
                    list.splice(ii,1);
                    var html = bd.template("tpl_coupon", {list: list});
                    $("#StaffCouponTool").html(html);
                }
                else if(type=='3'){
                    var list = that.loadData.setOff.toolStaffPoster;
                    list.splice(ii,1);
                    var html = bd.template("tpl_poster", {list: list});
                    $("#StaffPosterTool").html(html);
                }
            }
        },
        updateTool:function(id){
            debugger;
            var that=this;
            if(id!=undefined){
                $('#winTool').window({title:"编辑集客海报",width:600,height:600,top:($(window).height()-600) * 0.5,
                    left:($(window).width() - 600) * 0.5});
            }else{
                $('#winTool').window({title:"选择工具",width:600,height:600,top:($(window).height()-600) * 0.5,
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
            that.loadData.getSetOffAction(function(data) {

               var list = data.GetSetOffActionInfoList;
               list=list?list:[];
               setMessageId=[];
               var vipId = list[1].SetoffEventID;
               var staffId = list[0].SetoffEventID;

               $('#winMessage .editCheck').eq(0).attr('data-SetoffEventID',staffId);
               $('#winMessage .editCheck').eq(1).attr('data-SetoffEventID',vipId);
           });

            $('#winMessage').window({title:"发送通知",width:550,height:400,top:($(window).height() - 400) * 0.5,
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

        update:function(data){
            debugger;
            var that=this;
            $('#win').window({title:"信息确认",width:550,height:380,top:($(window).height()-380) * 0.5,
                left:($(window).width() - 550) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
			var list = data;
			list=list?list:[];
            for(var i=0;i<list.length;i++){
                var rewardRule = list[i].setoffOrderTimers;
                var rewardVipRule = list[i].setoffOrderVipTimers;
                if(rewardRule==undefined&&rewardVipRule==undefined){
                    $('#win').attr('data-isData',false);
                }else{
                    $('#win').attr('data-isData',true);
                }
            }
            var html=bd.template('tpl_Info',{list:list});
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            $('#win').parents('.window').css('position','fixed');
        },
        //取消发布
        updateCanel:function(){
            debugger;
            var that=this;
            $('#winCanel').window({title:"提示",width:400,height:250,top:($(window).height()-250) * 0.5,
                left:($(window).width() - 400) * 0.5});

            // $('#winCanel').window({title:"提示",width:400,height:250,top:($(window).height()-250) * 0.5,
            //     left:($(window).width() - 400) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            // if(name!=undefined){
            //     var html = "<p style='text-align:center;font-size:14px;margin-top:60px;'>"+name+"已停止发布</p>";
            //     $('#winCanel').find('.btnWrap').hide();
            // }else if(name==undefined&&isCheck){
            //     var html = "<p style='text-align:center;font-size:14px;margin-top:60px;'>确认发布后生效</p>";
            //     $('#winCanel').find('.btnWrap').hide();
            // }else{
                var html = "<p style='text-align:center;font-size:14px;margin-top:40px;'>确定取消发布</p>";
                $('#winCanel').find('.btnWrap').show();
            // }
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
        loadToolData:function(ii,type){
            var that =this;//ii=0,活动列表；ii=1,优惠券列表
            if(type=="1"){
                var listAction = that.loadData.setOff.toolVipAction;
                var listCoupon = that.loadData.setOff.toolVipCoupon;
            }else{
                var listAction = that.loadData.setOff.toolStaffAction;
                var listCoupon = that.loadData.setOff.toolStaffCoupon;
            }
            if(ii==0){
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
            }else if(ii==1){
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
        fiterData:function(list,type,SetoffType,callback){
            var data= list;
            var staffToolAction=[];
            var staffToolCoupon=[];
            var staffToolPoster=[];
            var vipToolAction=[];
            var vipToolCoupon=[];
            var vipToolPoster=[];
            $('.Module').each(function(i){
                if(i=='0'){
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
                }else{
                    var item1 =$(this).find('.toolData .contentsData').eq(0).find('li');
                    var item2 =$(this).find('.toolData .contentsData').eq(1).find('li');
                    var item3 =$(this).find('.toolData .contentsData').eq(2).find('li');
                    item1.each(function(j){
                        $tt = $(this);
                        var obj ={};
                        obj.ObjectId =$tt.attr('data-id');
                        obj.Name =$tt.find('.name').html();
                        vipToolAction.push(obj);
                    })
                    item2.each(function(k){
                        $tt = $(this);
                        var obj ={};
                        obj.ObjectId =$tt.attr('data-id');
                        obj.Name =$tt.find('.name').html();
                        vipToolCoupon.push(obj);
                    })
                    item3.each(function(n){
                        $tt = $(this);
                        var obj ={};
                        obj.Name =$tt.find('.name').html();
                        vipToolPoster.push(obj);
                    })
                }
            })
            if(data!=null){
                var isContinued = true;
                if(type=='0'){
                    var idData =[];
                    for(var i=0;i<data.length;i++){
                        idData.push(data[i].CTWEventId);

                        if(SetoffType=='1'){
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
                            }
                        }else{
                            if(staffToolAction!=""){
                                var toolData = $('.setOffStaffModule').find('.toolData .contentsData').eq(0).find("li[data-id='"+idData[i]+"']");
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
                                }
                            }else{
                                callback(true);
                                isContinued = false;
                                return false;
                            }
                        }
                        if(isContinued == false){
                            return false;
                        }
                    }
                }
                if(type=='100'){
                    var idData =[];
                    for(var i=0;i<data.length;i++){
                        idData.push(data[i].CouponTypeID);
                        if(SetoffType=='1'){
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
                        }else{
                            if(staffToolCoupon!=""){
                                var toolData = $('.setOffStaffModule').find('.toolData .contentsData').eq(1).find("li[data-id='"+idData[i]+"']");
                                if(toolData.length!="0"){
                                    var name =$(toolData).find('.name').html();
                                    $.messager.alert('提示','你的会员优惠券列表已有'+name+'活动');
                                    // $('#winTool').find('.warnning').children('span').eq(0).html('您的会员优惠券列表中已有');
                                    // $('#winTool').find('.warnning').children('b').html(name);
                                    callback(false);
                                    return false;
                                }else{
                                    callback(true);
                                    isContinued = false;
                                }
                            }else{
                                callback(true);
                                isContinued = false;
                                return false;
                            }
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
                        if(SetoffType=='1'){
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
                        }else{
                            if(staffToolPoster!=""){
                                var toolData = $('.setOffStaffModule').find('.toolData .contentsData').eq(2).find("li[data-id='"+idData[i]+"']");
                                if(toolData.length!="0"){
                                    var name =$(toolData).find('.name').html();
                                    $.messager.alert('提示','你的会员活动列表已有'+name+'活动');
                                    // $('#winTool').find('.warnning').children('span').eq(0).html('您的会员集客海报列表中已有');
                                    // $('#winTool').find('.warnning').children('b').html(name);
                                    callback(false);
                                    return false;
                                }else{
                                    callback(true);
                                    isContinued = false;
                                }
                            }else{
                                callback(true);
                                isContinued = false;
                                return false;
                            }
                        }
                        if(isContinued == false){
                            return false;
                        }
                    }
                }

            }


        },

        //渲染tabel 1
        renderTable1: function (data,ii) {
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
                                str+="<li>"+row.StartDate+"-"+ row.EndDate+"</li>";
                                return str;
                            }
                        }
                    },
                    {field: 'OnfflineQRCodeId', title: '预览', width: 150, align: 'center', resizable: false
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
                    checkbox : true
                } //显示复选框
                ] ],
                columns : [[
                    {field : 'CouponTypeName',title : '优惠券名称',width:250,align:'left',resizable:false
                        ,formatter:function(value ,row,index) {
                        if (value) {
                            return value
                        }
                    }
                    },

                    {field : 'ValidityPeriod',title : '优惠券有效期',width:200,align:'left',resizable:false,
                        formatter:function(value,row,index){
                            if (value) {
                                if(row.BeginTimeDate!=''){
                                    var str = "<span>"+row.BeginTimeDate+"</span>至<span>"+row.EndTimeDate+"</span>"
                                    return str;
                                }else{
                                    var str = "<span>"+row.ValidityPeriod+"</span>"
                                    return str;
                                }
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

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            // 获取集客行动数据
            that.loadData.getSetOffAction(function(data){
                // 获取集客行动奖励数据
                if(data.GetSetOffActionInfoList.length!=0){
                    that.getAction(data);
                }else{
                    that.loadData.setOff.isData=false;
                }

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
        getAction:function(data){
            var that = this;
            var list = data.GetSetOffActionInfoList;
            //that.loatData.setOff.getActionList = list;
            for(var i =0;i<list.length;i++){
                var setoffType = list[i].SetoffType;//1=会员集客；2=员工集客
                var isDisabled = list[i].IincentiveRuleStatus;//90=禁用；10=启用

                var setoffOrderPer = list[i].SetoffOrderPer;//获取集客奖励提成比例
                var setoffOrderTimers = list[i].SetoffOrderTimers;//获取集客奖励提成限制(1.首单有效;0.单单有效)
                var setoffRegAwardType = list[i].SetoffRegAwardType;//获取集客奖励（1.现金；2.积分）
                var setoffRegPrize = list[i].SetoffRegPrize;//获取集客奖励内容
                var getSetoffTools = list[i].GetSetoffToolsInfoList//获取集客工具
                if(setoffOrderPer=="0"){
                    setoffOrderPer="";
                }
                if(setoffRegPrize=="0"){
                    setoffRegPrize="";
                }
                if(setoffType=='1'){
                    if(setoffOrderPer!=""&&setoffRegPrize!=""||getSetoffTools!=null){
                        $('.setOffVipModule').find('.point').children('span').eq(1).trigger('click');
                    }

                    that.loadData.setOff.setOffVipId = list[i].SetoffEventID;//获取会员集客行动id
                    if(setoffRegAwardType=='1'){
                        $('#rewardRule').parents('.editArea').find('.till').html('元');
                    }else{
                        $('#rewardRule').parents('.editArea').find('.till').html('积分');
                    }

                    if(isDisabled=='10'){
                        $('.setOffVipModule').find('.lockBack').children('.cirle').trigger('click');
                        $('.setOffVipModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('on');
                    }else{
                        $('#rewardRule').combobox('setValue', setoffRegAwardType);
                        $('#rewardRule').combobox({disabled:true});
                        $('.setOffVipModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('on');
                        $('.setOffVipModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('disab');
                    }
                    if(getSetoffTools==null||getSetoffTools==[]){
                        $('.setOffVipModule').find('.blockModul').find('.noContents').show();
                    }else{
                        $('.setOffVipModule').find(".toolSetOff").html('已发布');
                        that.setOffTools(setoffType,getSetoffTools);
                    }
                    if(setoffRegPrize==""&&setoffRegPrize==""){
                        $('.setOffVipModule').find('.rewardAction').html('新增激励方案')
                    }else{
                        $('.setOffVipModule').find('.rewardAction').html('使用中的激励方案')
                    }
                    $('#rewardRule').combobox('setValue', setoffRegAwardType);
                    $('.setOffVipModule').find('.setoffOrderPer').val(setoffOrderPer);
                    $('.setOffVipModule').find('.setoffRegPrize').val(setoffRegPrize);
                    that.loadData.reward.vipLock=isDisabled;
                    that.loadData.reward.vipOrderTimers=setoffOrderTimers;
                    that.loadData.reward.vipRegAwardType=setoffRegAwardType;
                    that.loadData.reward.vipOrderPer=setoffOrderPer;
                    that.loadData.reward.vipRegPrize=setoffRegPrize;
                }
                if(setoffType=='2'){
                    that.loadData.setOff.setOffStaffId = list[i].SetoffEventID;//获取集客员工行动id
                    if(setoffOrderPer!=""&&setoffRegPrize!=""||getSetoffTools!=null){
                        $('.setOffStaffModule').find('.point').children('span').eq(1).trigger('click');
                    }
                    if(isDisabled=="10"){
                        $('.setOffStaffModule').find('.lockBack').children('.cirle').trigger('click');
                        $('.setOffStaffModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('on');
                    }else{
                        $('.setOffStaffModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('on');
                        $('.setOffStaffModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('disab');
                    }
                    if(setoffRegPrize==""&&setoffRegPrize==""){
                        $('.setOffStaffModule').find('.rewardAction').html('新增激励方案')
                    }else{
                        $('.setOffStaffModule').find('.rewardAction').html('使用中的激励方案')
                    }
                    if(getSetoffTools==null||getSetoffTools==[]){
                        $('.setOffStaffModule').find('.blockModul').find('.noContents').show();
                    }else{
                        $('.setOffStaffModule').find(".toolSetOff").html('已发布');
                        that.setOffTools(setoffType,getSetoffTools);
                    }
                    $('.setOffStaffModule').find('.editArea').find("p[data-type='"+setoffOrderTimers+"']").addClass('on');
                    $('.setOffStaffModule').find('.setoffOrderPer').val(setoffOrderPer);
                    $('.setOffStaffModule').find('.setoffRegPrize').val(setoffRegPrize);
                    that.loadData.reward.staffLock=isDisabled;
                    that.loadData.reward.staffOrderTimers=setoffOrderTimers;
                    that.loadData.reward.staffOrderPer=setoffOrderPer;
                    that.loadData.reward.staffRegPrize=setoffRegPrize;
                }


            }
        },
        setOffTools:function(type,data){
            var that = this;
            if(type=='1'){
                var listData = data;
                var ToolsInfoList1 =[];
                var ToolsInfoList2 =[];
                var ToolsInfoList3 =[];
                for(var i=0;i<listData.length;i++){
                    var toolType =listData[i].ToolType;
                    if(toolType=='CTW'){
                        list =[];
                        var list = listData[i];
                        ToolsInfoList1.push(list);
                    }else if(toolType=='Coupon'){
                        var list = listData[i];
                        list=list?list:[];
                        ToolsInfoList2.push(list);
                    }else if(toolType=='SetoffPoster'){
                        var list = listData[i];
                        list=list?list:[];
                        ToolsInfoList3.push(list);
                    }
                }
                var noContent = $('.setOffVipModule').find('.noContents');
                var toolTitle = $('.setOffVipModule').find('.toolData').children('.title');
                var contentData =  $('.setOffVipModule').find('.contentsData');
                toolTitle.show();
                noContent.hide();
                contentData.hide();
                contentData.eq(0).show();
                that.loadData.setOff.toolVipAction = ToolsInfoList1;
                that.loadData.setOff.toolVipCoupon= ToolsInfoList2;
                that.loadData.setOff.toolVipPoster = ToolsInfoList3;
                var html1=bd.template("tpl_action",{list:ToolsInfoList1});
                var html2=bd.template("tpl_coupon",{list:ToolsInfoList2});
                var html3=bd.template("tpl_poster",{list:ToolsInfoList3});
                $("#VipActionTool").html(html1);
                $("#VipCouponTool").html(html2);
                $("#VipPosterTool").html(html3);

            }else{
                var listData = data;
                var ToolsInfoList1 =[];
                var ToolsInfoList2 =[];
                var ToolsInfoList3 =[];
                for(var i=0;i<listData.length;i++){
                    var toolType =listData[i].ToolType;
                    if(toolType=='CTW'){
                        list =[];
                        var list = listData[i];
                        ToolsInfoList1.push(list);
                    }else if(toolType=='Coupon'){
                        var list = listData[i];
                        list=list?list:[];
                        ToolsInfoList2.push(list);
                    }else if(toolType=='SetoffPoster'){
                        var list = listData[i];
                        list=list?list:[];
                        ToolsInfoList3.push(list);
                    }
                }
                var noContent = $('.setOffStaffModule').find('.noContents');
                var toolTitle = $('.setOffStaffModule').find('.toolData').children('.title');
                var contentData =  $('.setOffStaffModule').find('.contentsData');
                toolTitle.show();
                noContent.hide();
                contentData.hide();
                contentData.eq(0).show();
                that.loadData.setOff.toolStaffAction = ToolsInfoList1;
                that.loadData.setOff.toolStaffCoupon= ToolsInfoList2;
                that.loadData.setOff.toolStaffPoster = ToolsInfoList3;
                var html1=bd.template("tpl_action",{list:ToolsInfoList1});
                var html2=bd.template("tpl_coupon",{list:ToolsInfoList2});
                var html3=bd.template("tpl_poster",{list:ToolsInfoList3});
                $("#StaffActionTool").html(html1);
                $("#StaffCouponTool").html(html2);
                $("#StaffPosterTool").html(html3);

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
                SetOffActionInfo:[],
                setOffType:"",
                Message:[],
                getActionList:[],
                toolStaffAction:[],
                toolStaffCoupon:[],
                toolStaffPoster:[],
                toolVipAction:[],
                toolVipCoupon:[],
                toolVipPoster:[],
                isStaffChange:true,
                isVipChange:true,
                isData:true,
                setOffVipId:'',
                setOffStaffId:''
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
            //获取创意仓库活动列表
            getCTWLEventList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.GetCTWLEventList',
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
            //设置集客行动方案
            setOffAction: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.SetOffAction',
                        'SetOffActionList':this.setOff.SetOffActionInfo
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
			//获取集客行动方案
            getSetOffAction: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'VIP.VipGold.GetSetOffAction',
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
            //获取集客效果
            getRSetoffHomeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'Report.VipGoldReport.GetRSetoffHomeList'
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

