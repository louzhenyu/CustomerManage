define(['jquery','template', 'tools','langzh_CN','easyui', 'artDialog','kkpager','kindeditor'], function ($) {


    //上传图片
    KE = KindEditor;



    var page = {
        elems: {
            optPanel:$("#optPanel"), //页面的顶部li
            submitBtn:$("#submitBtn"),   //提交和下一步按钮
            section:$("#section"),    //
            editLayer:$("#editLayer"), //图片上传
            simpleQuery:$("#simpleQuery"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),                //表格head部分
            SpecialDateList: [],         //弹出框
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            width:200,
            height:30,
            panlH:200,
            sku  :$("#sku"),
            loadAlert:null,

            allData:{} //页面所有存放对象基础数据
        },

        init: function () {



            this.initEvent();
            var that=this;
            that.elems.section.find(".checkBox").trigger("click").trigger("click");
           /* setTimeout(function(){
                that.elems.section.find(".checkBox").eq(0).trigger("click").trigger("click");
                that.elems.section.find(".checkBox").eq(1).trigger("click").trigger("click");
            },300)*/
			
		/*	$('#VipCardLevel').combobox({
				width:200,
				height:that.elems.height,
				panelHeight:115,
				valueField: 'VipCardLevel',
				textField: 'VipCardName',
				data:[
				{
					VipCardLevel : 1,
					VipCardName : '等级1'
				},
				{
					VipCardLevel : 2,
					VipCardName : '等级2'
				},
				{
					VipCardLevel : 3,
					VipCardName : '等级3'
				},
				{
					VipCardLevel : 4,
					VipCardName : '等级4'
				},
				{
					VipCardLevel : 5,
					VipCardName : '等级5'
				},
				{
					VipCardLevel : 6,
					VipCardName : '等级6'
				},
				{
					VipCardLevel : 7,
					VipCardName : '等级7'
				}]
			});*/
			
			
            this.loadDataPage();
        },
        //动态创建页面元素


        initEvent: function () {
            var that = this;
            that.elems.simpleQuery.find(".panelDiv").fadeOut(0).eq(0).fadeIn("slow");

            /**************** -------------------初始化easyui 控件 start****************/
            var wd=200,H=30;

           $("#nav02").delegate(".fontC","click",function(e){
               var rowIndex=$(this).data("index");
               var optType=$(this).data("oprtype");
               that.elems.tabel.datagrid('selectRow', rowIndex);
               var row = that.elems.tabel.datagrid('getSelected');
               that.loadData.args.CouponTypeID=row.CouponTypeID;

               if(optType=="delete"){


				   that.loadData.operation(row,optType,function(){
					   that.loadData.getSysVipCardTypeDetail(function(data){
						   alert("操作成功");

						   that.elems.SpecialDateList=data.Data.SpecialDateList
						   that.renderTable(that.elems.SpecialDateList)
					   });
				   })

               }


           });
            // 获取促销分组

            /**************** -------------------初始化easyui 控件  End****************/
            that.elems.optPanel.delegate("li","click",function(e){
                debugger;
                var me=$(this), flag=$(this).data("flag"),issubmit=true;



                    that.elems.submitBtn.data("flag", flag);
                    if (me.index() == that.elems.optPanel.find("li").length - 1) {
                        if ($('#nav0_1').form('validate')) {
                            if(flag!="#nav01") {
                                if (!that.loadData.args.imgSrc) {
                                    issubmit = false;
                                    alert("请上传一张卡类型图片");
                                }
                                if(issubmit) {
									//TODO:wlong
                                     var fields = $('#nav0_1').serializeArray(), //自动序列化表单元素为JSON对象
									 	 category = $('.vipTypeCard .radio.on').data('category'),
                                         isExtraMoney=$('[data-flag="isextramoney"]').hasClass("on") ? 1 : 0;
										 isPassword = $('#IsPassword').hasClass('on')?1:0;

                                    category=2;
                                    fields.push({name:'Category',value:category});
                                    fields.push({name:'IsPassword',value:isPassword});

									 fields.push({name:'IsExtraMoney',value:isExtraMoney});
									 //console.log(fields);
									 //return false;
                                     that.loadData.operation(fields, "CardType", function (data) {
                                         that.loadData.args.VipCardTypeID = data.Data.VipCardTypeID;

                                         $.util.toNewUrlPath("QueryList.aspx");
                                         alert("操作成功");

                                         //that.elems.submitBtn.html("完成");
                                         /*$(".bgWhite").show();
                                         $(".bgCcc").hide();
                                         that.elems.submitBtn.data("issubMit", true);  //用于#submitBtn时间绑定的区分
                                         that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                                         $(flag).fadeIn("slow");
                                         that.elems.optPanel.find("li").removeClass("on");
                                         me.addClass("on");
                                         that.renderTable(that.elems.SpecialDateList);  //绑定数据*/

                                     });
                               }
                            }

                        }

                    } else {

                        that.elems.submitBtn.html("下一步");
                        that.elems.submitBtn.data("issubMit", false);
                        $(".bgCcc").show();
                        $(".bgWhite").hide();
                        that.elems.simpleQuery.find(".panelDiv").fadeOut(0);
                        $(flag).fadeIn("slow");
                        that.elems.optPanel.find("li").removeClass("on");
                        $(this).addClass("on")

                    }



            });
            that.elems.section.delegate(".bgWhite","click",function(){
                that.elems.optPanel.find("li").eq(0).trigger("click");
                $(this).hide();
            }).delegate(".bgCcc","click",function(){
              //  var mid = JITMethod.getUrlParam("mid");
                location.href = "QueryList.aspx?mid=" + mid;
                $.util.toNewUrlPath("QueryList.aspx")
            });
            that.elems.section.delegate(".bgWhite","click",function(){
                that.elems.optPanel.find("li").eq(0).trigger("click");
                $(this).hide();
            });
            that.elems.section.delegate("#submitBtn","click",function(e){
                debugger;
                var index=0;
                if($(this).data("issubMit")){
                      //跳转会列表

                     /* var mid = JITMethod.getUrlParam("mid");
                         location.href = "queryList.aspx?&mid=" + mid;*/
                    $.util.toNewUrlPath("queryList.aspx")
                }else {
                    that.elems.simpleQuery.find(".panelDiv").each(function (e) {
                        debugger;
                        if (!($(this).is(":hidden"))) {
                            index = $(this).data("index") + 1;
                        }

                    });
                    $(this).siblings().find(".bgCcc").hide();
                    that.elems.optPanel.find("li").eq(index).trigger("click");
                }
                $.util.stopBubble(e);
            }).delegate(".checkBox","click",function(e){
                var me= $(this);
                me.toggleClass("on");
                debugger;

                if(!me.hasClass("on")) {
                    me.siblings().find(".easyui-numberbox").numberbox({
                        disabled: true,
                        required: false
                    });
                    me.siblings().find(".textbox.numberbox").addClass("bgColor");
                }else{

                    me.siblings().find(".easyui-numberbox").numberbox({
                        disabled:false,
                        required: true
                    });
                    me.siblings().find(".textbox.numberbox ").removeClass("bgColor");

                }
                $.util.stopBubble(e);
            }).delegate("#addDatetime","click",function(){
                   that.addDatetime()
            });
            
			$('.vipTypeCard .radio').on('click',function(){
				var me= $(this),
					$vipTypeCard = $('.vipTypeCard .radio'),
					val = me.data('category'),
					$autoUpdateBox = $('#autoUpdateBox'),//自动升级
					$returnAmountPerBox = $('#returnAmountPerBox'),//返现
					$chargeGiveBox = $('#chargeGiveBox'),//充值
					$paidGivePointsBox = $('#paidGivePointsBox'),//积分
					$cardDiscountBox = $('#cardDiscountBox'),//折扣
					$vipCardLevelBox = $('#vipCardLevelBox');//卡等级
				$vipTypeCard.removeClass('on');
				me.addClass('on');
				
				//$autoUpdateBox.show();
				$returnAmountPerBox.show();
				$chargeGiveBox.show();
				$paidGivePointsBox.show();
				$cardDiscountBox.show();
				$vipCardLevelBox.show();
				if(val==1 || val==2){//储值卡,消费卡
					$autoUpdateBox.hide();
					$returnAmountPerBox.hide();
					$paidGivePointsBox.hide();
					$cardDiscountBox.hide();
					$vipCardLevelBox.hide();
					if(val == 2){
						$chargeGiveBox.hide();//充值
					}
					
				}
			})


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
            $('#win').delegate(".saveBtn","click",function(e){

                if ($('#payOrder').form('validate')) {
					if($('.listBtn.show').length==0){
						$('.ruleTipText').show();
						setTimeout(function(){
							$('.ruleTipText').hide();
						},1000);
						return false;
					}
                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,"setDate",function(data){

                        that.loadData.getSysVipCardTypeDetail(function(data){
                            alert("操作成功");
                            $('#win').window('close');
                            that.elems.SpecialDateList=data.Data.SpecialDateList;
                           // that.renderTable(that.elems.SpecialDateList)
                        });
                    });
                }
            }).delegate(".listBtn","click",function(){
                debugger;
                var me=$(this);
                me.toggleClass("show");
            }) ;
            /**************** -------------------弹出窗口初始化 end****************/


            that.registerUploadImgBtn();

        },
      /* setVipCard:function(){
           var that=this;
           if ($('#nav0_1').form('validate')) {

               var fields = $('#nav0_1').serializeArray(); //自动序列化表单元素为JSON对象

               that.loadData.operation(fields,that.elems.optionType,function(data){


                   that.loadData.getSysVipCardTypeDetail(function(data){
                       alert("操作成功");
                       $('#win').window('close');
                       that.elems.SpecialDateList=data.Data.SpecialDateList
                   });

               });
           }
       },*/


         //特殊日期操作
        addDatetime:function(){
            var that=this;
            var top=$(document).scrollTop()+60;
            $('#win').window({title:"特殊日期",width:444,height:335,top:top,left:($(window).width()-444)/2 });
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addDatetime');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
            that.loadData.getHolidayList(function(data){
                data.Data.HolidayList.push({"HolidayId":0,"HolidayName":"请选择","selected":true});
                $("#Holiday").combobox({
                    valueField:'HolidayId',
                    textField:'HolidayName',
                    data:data.Data.HolidayList
                })
            });


        },

        //初始化加载的页面数据
        loadDataPage:function(){
            var that=this;
            that.loadData.args.VipCardTypeID = $.util.getUrlParam("VipCardTypeID");
               if(that.loadData.args.VipCardTypeID!=-1) {
                   that.loadData.getSysVipCardTypeDetail(function (data) {
					   //TODO:wlong
					   if(data.Data){//卡类型
					   		var value = data.Data.Category;
							if(value != null){
								$('.radio[data-category='+data.Data.Category+']').trigger("click");
								$('.vipTypeCard .radio').unbind('click');
							}
					   }
					   if(data.Data){
						   setTimeout(function(){
								$('#VipCardLevel').combobox('setValue',data.Data.VipCardLevel || 1);
						   },200);
					   }
					   //zhekou
					   if (data.Data && data.Data.IsPassword) {//密码
                           $('.checkBox[data-flag="IsPassword"]').trigger("click");
                       }
					   if (data.Data && data.Data.CardDiscount) {//折扣
						   debugger
                           $('.checkBox[data-flag="CardDiscount"]').trigger("click");
                       }
					   if (data.Data && data.Data.PaidGivePoints){//积分 消费多少反馈多少积分 PointsMultiple:积分倍数移除了
                           $('.checkBox[data-flag="PaidGivePoints"]').trigger("click")
                       }
					   //充值满多少送多少 如果值可以等于零需要改成data.Data.ChargeGive>=0
                       if (data.Data && data.Data.ChargeFull && data.Data.ChargeGive) {
						   $('.checkBox[data-flag="EnableRewardCash"]').trigger("click");
                       }
					   if (data.Data && data.Data.ReturnAmountPer){//返现比例
                           $('.checkBox[data-flag="ReturnAmountPer"]').trigger("click")
                       }
					   
					   //自动升级
					   if (data.Data && data.Data.UpgradeAmount) {//累计消费金额满
						   $('.checkBox[data-flag="UpgradeAmount"]').trigger("click");
                       }
					   if (data.Data && data.Data.UpgradePoint) {//累计获得积分满
						   $('.checkBox[data-flag="UpgradePoint"]').trigger("click");
                       }
					   if (data.Data && data.Data.UpgradeOnceAmount) {//单次消费金额满
						   $('.checkBox[data-flag="UpgradeOnceAmount"]').trigger("click");
                       }
                       if (data.Data && data.Data.IsExtraMoney) {//是否可补差价
                           $('.checkBox[data-flag="isextramoney"]').trigger("click");
                       }
					   
					   
					   
                       if (data.Data && data.Data.PicUrl) {//上传图片
                           that.loadData.args.imgSrc = data.Data.PicUrl;
                           that.elems.editLayer.find(".imgPanl").find("img").attr("src", data.Data.PicUrl);
                       }

					   
					   

                       $(".tooltip ").hide();
                       $('#nav0_1').form('load', data.Data)
                       that.elems.SpecialDateList = data.Data.SpecialDateList
                   });
               }
        },
        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data){

                data=[]
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
               /* height : 332,*/ //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data,
                //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'VipCardTypeID', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[
                    {field : 'HolidayName',title : '名称',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=52;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    } ,
                    {field : 'BeginDate',title : '开始日期',width:80,align:'center',resizable:false},
                    {field : 'EndDate',title : '结束日期',width:80,align:'center',resizable:false},
                    {field : 'NoAvailableDiscount',title : '折扣',width:51,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            if(value){
                                return ' '
                            }else{
                                return '<img src="images/cha.png" width="16" height="16">'
                            }
                        }
                    } ,
                    {field : 'NoAvailablePoints',title : '使用积分',width:52,align:'center',resizable:false,
                        formatter:function(value ,row,index){

                            if(value){
                                return ' '
                            }else{
                                return '<img src="images/cha.png" width="16" height="16">'
                            }
                        }
                    } ,
                    {field : 'NoRewardPoints',title : '回馈积分',width:53,align:'center',resizable:false,
                        formatter:function(value ,row,index){

                            if(value){
                                return ''
                            }else{
                                return '<img src="images/cha.png" width="16" height="16">'
                            }
                        }
                    } ,

                    {field : 'SpecialID',title : '删除',width:22,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC opt delete" style="margin:0;" data-index="'+index+'" data-oprtype="delete"></p>';
                        }
                    }
                ]],

                onLoadSuccess : function(data) {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0){
                        that.elems.dataMessage.hide()
                    }else{
                        that.elems.dataMessage.show();
                    }
                },


                onClickRow:function(rowindex,rowData){

                },onClickCell:function(rowIndex, field, value){

                }

            });




        },








        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            var self = this;
            // 注册上传按钮
            self.elems.editLayer.find(".uploadImgBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },
        //上传图片区域的各种事件绑定
        addUploadImgEvent: function (e) {
            var self = this;



            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                self.loadData.args.imgSrc=data.url;
                $(ele).parent().siblings(".imgPanl").find("img").attr("src",data.url);
            });

        },
        //上传图片
        uploadImg: function (btn, callback) {
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width:300,
                //上传的文件类型
                fieldName: 'imgFile',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=640',

                afterUpload: function (data) {
                    debugger;
                    if (data.error === 0) {
                        if (callback) {
                            callback(btn, data);
                        }
                        //取返回值,注意后台设置的key,如果要取原值
                        //取缩略图地址
                        //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                        //取原图地址
                        //var url = KE.formatUrl(data.url, 'absolute');
                    } else{
                        alert(data.msg);
                    }
                },
                afterError: function (str) {
                    alert('自定义错误信息: ' + str);
                }
            });
            debugger;
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },

        loadData: {
            args: {
               imgSrc:"" ,
                VipCardTypeID:-1
            },
            getClassifySeach:{
                Status:"1",
                node:"root",
                multiSelect:'',
                isAddPleaseSelectItem:'true',
                pleaseSelectText:"请选择",
                pleaseSelectID:"0",
                bat_id:"1" //1 是商品分类 0是促销分组

            },


              operation:function(pram,operationType,callback) {
                  debugger;
                  var prams = {data: {action: ""}};
                  prams.url = "/ApplicationInterface/Gateway.ashx";
                  //根据不同的操作 设置不懂请求路径和 方法  \
                  if(this.args.VipCardTypeID!=-1){
                      prams.data.VipCardTypeID = this.args.VipCardTypeID;
                  }



                  $.each(pram, function (i, field) {
                          if(field.value!=="") {
                              prams.data[field.name] = field.value; //提交的参数
                          }
                  });


                  switch (operationType){
                      case "CardType" :
                          prams.data.action = "VIP.SysVipCardType.SetSysVipCardType";
                          prams.data["PicUrl"]=this.args.imgSrc;
                          break;
                      case "setDate":
                          prams.data.action="VIP.SysVipCardType.SetSpecialDate";
                          $("#win").find(".listBtn").each(function(){
                              var me=$(this);
                                if(me.hasClass("show")) {
                                    prams.data[me.data("flag")] =0;
                                }else{
                                    prams.data[me.data("flag")] =1;
                                }
                          });


                          break;
                      case "delete" :
                          prams.data.action = "VIP.SysVipCardType.DelSpecialDate";
                          prams.data["SpecialID"]=pram.SpecialID;
                          break;
                  }

                  $.util.ajax({
                      url: prams.url,
                      data: prams.data,
                      success: function (data) {
                          if (data.IsSuccess && data.ResultCode == 0) {
                              if (callback) {
                                  callback(data);
                              }

                          } else {
                              $.messager.alert("提示",data.Message);
                          }
                      }
                  });
              },
            getSysVipCardTypeDetail:function(callback){
                $.util.ajax({
                    url: " /ApplicationInterface/Gateway.ashx",
                    data: {
                        action: 'VIP.SysVipCardType.GetSysVipCardTypeDetail',
                        VipCardTypeID:this.args.VipCardTypeID
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });

            },
            getHolidayList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:"Basic.Holiday.GetHolidayList",
                        PageSize:100000,
                        PageIndex:1

                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("加载异常请联系管理员");
                        }
                    }
                });
            }

        }

    };
    page.init();
});

