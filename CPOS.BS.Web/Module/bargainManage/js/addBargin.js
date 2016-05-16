define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','datetimePicker'], function ($) {
    var page = {
        url: "/ApplicationInterface/Events/EventsGateway.ashx",
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116,                       // 下来框统一高度
            searchBtn: $("#searchBtn"), 
            keyword:$("#keyword"),

        },
        detailDate:{},
        ValueCard: '',//储值卡号
        submitappcount: false,//是否正在提交追加表单
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        stopBubble: function (e) {
            if (e && e.stopPropagation) {
                //因此它支持W3C的stopPropagation()方法
                e.stopPropagation();
            }
            else {
                //否则，我们需要使用IE的方式来取消事件冒泡
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        },
        //获得页面分类和
        initObj:function(){
            var that=this;
            //活动id
            var eventId = $.util.getUrlParam("eventId");
            var commodityStatus = $.util.getUrlParam("CommodityStatus");
            that.eventId=eventId;
            that.commodityStatus=commodityStatus;
            var pageStr = "";
        },
        init: function () {
            var that=this;
            this.initObj();
            this.initEvent();
            this.loadPageData();


        },
        //初始化团购的商品列表
        initEventGoodsList:function(){
            var that=this;

            //请求结果
            this.loadData.getEventGoodsList(function(data){
                var list=data.Data.ItemList;
                list=(list&&list.length)?list:[];
                var html=bd.template("tpl_goodsItem",{list:list});
                $("#goodsList").html(html);
                //动画让其出现
                $("#"+that.showPage).show(1000);
            });
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
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.goods.pageIndex=currentPage-1;
            this.loadData.getQueryGoods(function(data){
                var list=data.Data.ItemInfoList;
                list=list?list:[];
                var html=bd.template("tpl_goods_list",{list:list})
                $("#searchList").html(html);
            });
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                
                that.loadData.getBargainItemList(function(data){
                    debugger;
					var list=data.Data;
					list=list?list:[];
					var html=bd.template("tpl_goods_basic",{list:list})
					$("#barginTitleInfo").html(html);
					//根据不同状态判断页面
					that.CommodityStatusInit();
                    that.renderTable(data);
                });
                $.util.stopBubble(e);

            });
            debugger;
           
            //点击添加商品
            $("#addShopBtn").delegate(".commonBtn","click",function(e){
                that.showElements("#addNewGoods");
                
                //获得分类
                that.loadData.getCategory(function(data){
                    //debugger;
                    var list=data.Data.ItemCategoryInfoList;
                    list=list?list:[];
                    var html=bd.template("tpl_category",{list:list})
                    $("#category").html(html);
                });
                //触发查询事件 获得所有的商品列表
                that.elems.searchBtn.trigger("click");
                that.stopBubble(e);
            });
            //商品基本信息编辑
            $("#barginTitleInfo").delegate(".commonBtn","click",function(e){
                that.showElements("#goodsBasic_exit");
                that.loadData.getBargainItemList(function(data){
                    var name = data.Data.EventName;
                    var start = data.Data.BeginTime;
                    var end = data.Data.EndTime;
					
                    $('#campaignName').val(name);
					$('#campaignBegin').val(start);
					$('#campaignEnd').val(end);
					if(page.commodityStatus=='2'){
						$('#goodsBasic_exit').find('input').attr('disabled',true);
						$('#campaignName').attr('disabled',false);
						$('#campaignBegin').css('background','#ccc');
						$('#campaignEnd').css('background','#ccc');
						
					}
					else if(page.commodityStatus=='3'){
						$('#goodsBasic_exit').find('input').attr('disabled',true);
						$('#campaignName').attr('disabled',false);
						$('#campaignBegin').css('background','#ccc');
						$('#campaignEnd').css('background','#ccc');
					}
                    
                })
            });
            //新增活动(编辑活动)
            $('#saveCampaign').bind('click',function(){
                var event_name = $('#campaignName').val();
                var event_start = $('#campaignBegin').val();
                var event_end = $('#campaignEnd').val();
                that.loadData.commodity.EventName = event_name;
                that.loadData.commodity.BeginTime = event_start;
                that.loadData.commodity.EndTime = event_end;
				if(event_start<event_end){
					that.loadData.setBargain(function(data){
						that.loadPageData();
						window.location.reload();//刷新当前页面.
					})
				}
				else{
					$.messager.alert('提示','活动开始时间不能大于结束时间'); 
				}
                //var event_id = data.Data.EventId;
                

            })
            //关闭弹出层
            $(".hintClose").bind("click",function(){
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
            });
            //搜索商品名称
            that.elems.searchBtn.click(function(e){
                var keyword=that.elems.keyword.val();
                that.loadData.goods.itemName=keyword;
                that.loadData.goods.pageIndex=0;
                that.loadData.goods.categoryId=$("#categoryText").data("categoryid");
                //查询商品
                that.loadData.getQueryGoods(function(data){
                    var list=data.Data.ItemInfoList;
                    list=list?list:[];
                    var html=bd.template("tpl_goods_list",{list:list})
                    $("#searchList").html(html);
                    kkpager.generPageHtml({
                        pagerid: 'kkpager1',
                        pno: that.loadData.args.PageIndex,
                        mode: 'click', //设置为click模式
                        //总页码
                        total: data.Data.TotalPage,
                        isShowTotalPage: false,
                        isShowTotalRecords: false,
                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                        //适用于不刷新页面，比如ajax
                        click: function (n) {
                            //这里可以做自已的处理
                            //...
                            //处理完后可以手动条用selectPage进行页码选中切换
                            this.selectPage(n);

                            that.loadMoreData(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);
                });
                that.stopBubble(e);
            });
            //选中一个商品则弹出新增规格的层
            $("#searchList").delegate("li","click",function(e){
                var $t=$(this);
                var itemId=$t.data("itemid"),shopName=$t.data("name");  //商品id 和商品名称
                that.loadData.sku.ItemId=itemId;
				that.loadData.args.EventItemMappingID="";
                //设置商品名称
                $("#goodsInfo .shopName").html(shopName);
				
                that.hideElements("#addNewGoods");
                setTimeout(function(){
                    //获得sku
                    that.loadData.getBargainDetails(function(data){
						var list = data.Data.SkuInfoList;
                        list=list?list:[];
                        if(list.length==0){
                            var d = dialog({
                                fixed:true,
                                title: '提示',
                                content: '该商品暂未配置规格，不能添加规格!'
                            });
                            d.showModal();
                            setTimeout(function () {
                                d.close().remove();
                            }, 3500);
                        }else{
                            var html=bd.template("tpl_rule",{list:list});
                            $("#rules").html(html);
							var inputs=$("#rules").find("input");
							inputs.each(function(i){
								var $tt=$(this).val();
								if($tt=='null'){
									$(this).val("");
								}	
							})
							$('#goodsInfo .quotaBox').find('input').removeClass('disab');
							$('#goodsInfo .quotaBox').find('input').attr('disabled',false);
							$('#goodsInfo .quotaBox').find('input').val('');
                            $("#rules .editArea .shopName").html(shopName);
                            $("#singleCount").css({border:"1px solid ccc"});
                            $("#barginTime").css({border:"1px solid ccc"});
                            that.showElements("#addNewRules");
                        }
                    });

                },550);
                that.stopBubble(e);
            });
            //选中规格和取消规格
            $("#rules").delegate(".item","click",function(e){
                var $t=$(this);
				var typeDisable = $t.attr('data-Disable');
				if(typeDisable =='false'){
					if(e.target.className=="checkBox"||e.target.nodeName=="SPAN"){
						$t.toggleClass("on");
					}
					var inputs=$t.find("input");
					
					if($t.hasClass("on")){
						
							inputs.each(function(i){
								var $tt=$(this);
								$tt.removeAttr("disabled");
								$t.find("input").eq(1).attr("disabled","disabled");
								$t.find("input").eq(2).css({'border':'1px solid #ccc','background':'fff'});
							});
						
						
					}else{
						
						inputs.each(function(i){
							var $tt=$(this);
							$tt.attr("disabled",true);
							$t.find("input").eq(2).css({'border':'1px solid #ccc','background':'none'});
							
						})
						
						
					}
				}	
				
                that.stopBubble(e);
            });
            //保存规格
            $("#saveRules").click(function(e){
                //获得所有的选中的规格
                var isBool = true,priceEorr={isSubMit:true,indexList:""};
                var singlePurchaseQtyInput = $("#singleCount");
                var BargaingingIntervalInput = $("#barginTime");
                var items=$("#rules").find(".on");
				var itemMappingId = $('#rules').attr('data_itemmappingid');
                var SkuInfoList=[];
                var exp =/^[0-9]*[1-9][0-9]*$/;//正整数正则
                var reg =/^\-[1-9][0-9]*$/;//负整数正则
                that.loadData.sku.SinglePurchaseQty = singlePurchaseQtyInput.val()||0;
                that.loadData.sku.BargaingingInterval = BargaingingIntervalInput.val();
				that.loadData.sku.mappingId=itemMappingId;
                $('#goodsInfo .quotaBox').find('input').click(function(){
                   $(this).css('border','1px solid #ccc');
                })
                if(items.length==0){
                    alert("请至少选择一个规格后再提交!");
                    return;
                }
                if(singlePurchaseQtyInput.val()!=""){
                    if(singlePurchaseQtyInput.val()!=0){
                        if(!exp.test(singlePurchaseQtyInput.val())){
                            if(reg.test(singlePurchaseQtyInput.val())){
                                $.messager.alert("提示","不能输入负数,请重新输入！")
                                othis.css({border:"1px solid red"});
                                isBool = false;
                                return fasle;
                            }else{
                                isBool = false
                                $.messager.alert("提示","砍价商品每人限购格式不对,请输入整数！");
                                singlePurchaseQtyInput.css({border:"1px solid red"});
                                return false;
                            }
                        }
                    }
                }
				if(BargaingingIntervalInput.val()!=""){

					if(!exp.test(BargaingingIntervalInput.val())){
                        if(BargaingingIntervalInput.val()=="0"){
                            isBool = false;
                            $.messager.alert("提示","砍价商品可砍时间不能为0!");
                            BargaingingIntervalInput.css({border:"1px solid red"});
                            return false;
                        }
                        else if(reg.test(BargaingingIntervalInput.val())){
                            $.messager.alert("提示","不能输入负数,请重新输入！")
                            othis.css({border:"1px solid red"});
                            isBool = false;
                            return fasle;
                        }else{
                            isBool = false
                            $.messager.alert("提示","砍价商品可砍时间格式不对,请输入整数！");
                            BargaingingIntervalInput.css({border:"1px solid red"});
                            return false;
                        }
					}
				}
				else{
					isBool = false;
					$.messager.alert("提示","砍价商品可砍时间不能为空!");
                    BargaingingIntervalInput.css({border:"1px solid red"});
					return false;
				}
				
                items.each(function(i,o){
                    //每个item
                    var $t=$(this);
                    var mappingid=$t.data("mappingid");
                    var skuMapid = $t.data("skumappid");
                    var skuId=$t.data("skuid");
                    var obj={};
                    obj.SkuID=skuId;
					obj.EventSKUMappingId=skuMapid;
                    var inputList=$t.find("input");

                    inputList.click(function(){
                        var othis=$(this);
                        othis.css('border','1px solid #ccc');
                    });
                    inputList.each(function(j,k){
                        var othis=$(this);
                        var value=othis.val();
                        var ago=value+"";//如果出错则恢复
						if(value!=""){
							if(isNaN(value)){
							   $.messager.alert("提示","只能输入数字")
								othis.css({border:"1px solid red"});
								isBool = false;
								return fasle;
							}
							else if(parseInt(value)=="0"){
								$.messager.alert("提示","数值不能为0")
								othis.css({border:"1px solid red"});
								isBool = false;
								return fasle;
							}
							else if(!exp.test(value)){
                                if(reg.test(value)){
                                    $.messager.alert("提示","不能输入负数,请重新输入！")
                                    othis.css({border:"1px solid red"});
                                    isBool = false;
                                    return fasle;
                                }else{
                                    $.messager.alert("提示","数值格式不对，请输入整数")
                                    othis.css({border:"1px solid red"});
                                    isBool = false;
                                    return fasle;
                                }
							}
							else{

								othis.css({"border-color":"","border-width":1,"border-style":""});
								if(j==0){
									obj.Qty=parseInt(value);
								}
								if(j==1){
									obj.Price=parseInt(value);
								}
								if(j==2){
									obj.BasePrice = parseFloat(value);
								}
								if(j==3){
									obj.BargainStartPrice=parseFloat(value);
								}
								if(j==4){
									obj.BargainEndPrice=parseFloat(value);
								}
							}
						}else{
							if(j==0){
								$.messager.alert("提示","商品数量不能为空");
							}
							if(j==2){
								$.messager.alert("提示","底价不能为空");
							}
							if(j==3){
								$.messager.alert("提示","砍减价不能为空");
							}
							if(j==4){
								$.messager.alert("提示","砍减价不能为空");
							}
							othis.css({border:"1px solid red"});
							isBool = false;
							return fasle;
						}

                    });
					if(obj.BargainStartPrice>obj.Price){
                        isBool = false;
                        $.messager.alert("提示","砍价可砍价格不能大于原价");
						inputList.eq(3).css({border:"1px solid red"});
                        return false;
                    }
                    if(obj.BargainStartPrice>obj.BargainEndPrice){
                        isBool = false;
                        $.messager.alert("提示","砍价区间后面不能小于前面");
						inputList.eq(4).css({border:"1px solid red"});
                        return false;
                    }
                    if(obj.Price<obj.BasePrice){
                        priceEorr.isSubMit=false;
						$.messager.alert("提示","底价不能大于原价");
						inputList.eq(2).css({border:"1px solid red"});
						 return false;
						
                    }
                    SkuInfoList.push(obj);
                });
                if(!isBool){
                  return false;
                }
                that.loadData.sku.SkuInfoList=SkuInfoList;
				debugger;

                if(priceEorr.isSubMit) {
                    that.loadData.setBargainDetails(function (data) {
                        //初始化详细页面头部信息
                        
                        that.loadMoreData2();
                        that.hideElements("#addNewRules");
                    });

                }
            });
            //分类选择事件
            $("#category").delegate("span","click",function(e){
                var $t=$(this);
                var categoryId=$t.data("categoryid"),categoryName=$t.html();
                $("#categoryText").data("categoryid",categoryId).html(categoryName);
                $t.parent().hide(500);
                that.stopBubble(e);
            });
            //获取时间插件
            $('#campaignBegin').datetimepicker({
                lang: "ch",
                format: 'Y-m-d H:i',
                step: 5 //分钟步长
            });
            $('#campaignEnd').datetimepicker({
                lang: "ch",
                format: 'Y-m-d H:i',
                step: 5 //分钟步长
            });

            //弹出类别选择事件
            $("#addNewGoods .selectBox").mouseover(function(e){
                $("#category").stop().show(500);
                that.stopBubble(e);
            });

			

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;


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
            $('#win').delegate(".saveBtn","click",function(e){

                if (!that.submitappcount) {
                    that.submitappcount = true;

                    if ($('#optionForm').form('validate')) {

                        var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象

                        that.loadData.operation(fields, that.elems.optionType, function (data) {

                            that.submitappcount = false;
                            $('#win').window('close');
                            alert("操作成功");

                            that.loadPageData(e);

                        });
                    }

                } else {

                    $.messager.alert('提示', '正在提交请稍后！');
                }

            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".opt","click",function(e){
                debugger;
                var rowIndex=$(this).data("index");
                var optType = $(this).data("flag");
                var couponTypeID = $(this).data("typeid");
                var CouponTypeName = $(this).data("typename");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                that.loadData.args.EventItemMappingID = row.EventItemMappingID;
				
                if(optType=="exit"){
                    var $t=$(this);
                    var itemId=$t.data("itemid"),shopName=$t.data("name");  //商品id 和商品名称
                    that.loadData.sku.ItemId=itemId;
                    setTimeout(function(){
                    //获得sku
                      that.loadData.getBargainDetails(function(data){
						  var SinglePurchaseQty = data.Data.SinglePurchaseQty;
						  var BargaingingInterval = data.Data.BargaingingInterval
                          var list=data.Data.SkuInfoList;
                          list=list?list:[];


                          if(list.length==0){
                              var d = dialog({
                                  fixed:true,
                                  title: '提示',
                                  content: '该商品暂未配置规格，不能添加规格!'
                              });
                              d.showModal();
                              setTimeout(function () {
                                  d.close().remove();
                              }, 3500);
                          }else{
                              
                              
                              var html=bd.template("tpl_rule",{list:list});
                              $("#rules").html(html);
							  var inputs=$("#rules").find("input");
							  
								inputs.each(function(i){
									var $tt=$(this).val();
									if($tt=="null"){
										$(this).val("");
									}
									
								})
							  $("#rules").attr('data_itemMappingId',row.EventItemMappingID);
							  $('#goodsInfo').find('.quotaBox').eq(1).find('input').val(BargaingingInterval);
							  $('#goodsInfo').find('.quotaBox').eq(0).find('input').val(SinglePurchaseQty);
							  $('#goodsInfo .quotaBox').find('input').addClass('disab');
							  $('#goodsInfo .quotaBox').find('input').attr('disabled',true);
                              var data_id = $('#rules').find('.item');
                              for(var i=0;i<data_id.length;i++){
                                  var rr = data_id.eq(i).attr('data-mappingid');
                                  if(rr!='null'){
                                    data_id.eq(i).addClass('on');
									data_id.eq(i).addClass('disab');
									data_id.eq(i).attr('data-Disable','true');
									data_id.eq(i).find('input').eq(0).attr('disabled',false);
									data_id.eq(i).find('input').eq(2).css('border','none');	
									data_id.eq(i).find('input').eq(3).attr('disabled',false);
									data_id.eq(i).find('input').eq(4).attr('disabled',false);
                                  }
                              }
                              $("#goodsInfo .shopName").html(shopName);
                              $("#rules .editArea .shopName").html(shopName);
                              $("#singleCount").css({border:"1px solid ccc"});
                              $("#barginTime").css({border:"1px solid ccc"});
                              that.showElements("#addNewRules");
                          }
                      });

                    },550);
                
                }

                if(optType=="delete"){
                    if (row.BeginTime&&row.EndTime) {
                        var Begindate = Date.parse(new Date(row.BeginTime).format("yyyy-MM-dd").replace(/-/g, "/"));
                        var Enddate = Date.parse(new Date(row.EndTime).format("yyyy-MM-dd").replace(/-/g, "/"));
                        var now = new Date();
                        // if (Begindate <= now && Enddate >= now) {
                        //     $.messager.alert("操作提示","优惠券在使用时间范围内不可删除");
                        //     return false;
                        // }
                    }
                    $.messager.confirm("提示","是否确认删除?",function(r){
                        if(r){
                            that.loadData.deleteBargainItem(function(){
                                alert("操作成功");
                                that.loadPageData()
                            })

                        }
                    })
                }
            })
            /**************** -------------------列表操作事件用例 End****************/
        },


        //收款
        addNumber:function(data){
            debugger;
            var that=this;
            that.elems.optionType="add";
            $('#win').window({title:"优惠券追加",width:430,height:230});

            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_AddNumber');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open')
        },
		//状态按钮
		CommodityStatusInit:function(){
			debugger;
			if(page.commodityStatus=="3"){
				$("#barginTitleInfo").undelegate(".commonBtn","click");
				$("#addShopBtn").undelegate(".commonBtn","click");
				$("#barginTitleInfo .commonBtn").css('background','#ccc');
				$("#addShopBtn .commonBtn").css('background','#ccc');
			}
			
		},
		
        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                that.loadData.seach[filed.name] = filed.value;
            });





        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.seach.PageIndex=1;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
            
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.ItemMappingInfoList){

                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.ItemMappingInfoList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[

                    {field : 'ItemName',title : '活动名称',width:300,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'SinglePurchaseQty',title : '每人限购',width:250,resizable:false,align:'left',
						formatter:function(value){
                            if (value =="0"){
								return value = "无限制";
                            }
							else{
								return value;
							}
                        }
					
					}
                    ,
                    {field : 'ItemId',title : '操作',width:100,align:'left',resizable:false,
                        formatter: function (value, row, index) {
							var str='';
							if(page.commodityStatus=="3"){
								str += "<div data-index=" + index + "  data-flag='exit2' data-TypeName='" + row.ItemName + "' data-TypeID='" + row.EventItemMappingID + "' data-name='"+ row.ItemName +"' data-ItemId='"+row.ItemId+"' class='exit2 opt' title='编辑'> </div>";
								str += "<div data-index=" + index + " data-flag='noDelete' class='noDelete opt' title='删除'></div></div>";
							}
							else{
								str += "<div data-index=" + index + "  data-flag='exit' data-TypeName='" + row.ItemName + "' data-TypeID='" + row.EventItemMappingID + "' data-name='"+ row.ItemName +"' data-ItemId='"+row.ItemId+"' class='exit opt' title='编辑'> </div>";
								str += "<div data-index=" + index + " data-flag='delete' class='delete opt' title='删除'></div></div>";
							}
                            
                            
                            
                            return str
                        }
                    }


                ]],

                onLoadSuccess : function(data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(data.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                        that.elems.dataMessage.show();
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

            
            //分页

            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData2: function () {
            var that = this;
            this.loadData.seach.PageIndex = 1;
            $(".datagrid-body").html('<div class="loading"><span><img src="../static/images/loading.gif"></span></div>');
            that.loadData.getBargainItemList(function(data){

                that.renderTable(data);
            });
        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex:1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                EventItemMappingID:''
            },
            seach:{
                CouponTypeName:"",
                ParValue:"",
                PageIndex:1,
                PageSize: 10,

            },
            //商品规格
            sku:{
                numArr:[],  //顺序是 0下标为 商品数量  1为已售商品基数  2为真实销售数量  3为当前库存
                mappingId:"",
                eventId:"",
                ItemId:"",
                SinglePurchaseQty:"",
                BargaingingInterval:"",
                SkuList:[],
                SkuInfoList:[]
            },
            goods:{
                pageSize:10,
                pageIndex:1,
                itemName:""
            },
            commodity:{
              EventName:"",
              BeginTime:"",
              EndTime:""
            },
            opertionField:{},
            //获取活动详情
            getEventDetail: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'GetPanicbuyingEventDetails',
                        'EventId':page.eventId
                    },
                    success: function (data) {
                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //新增商品(编辑商品)
            setBargain: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.SetBargain',
                        'EventId':page.eventId,
                        'EventName':this.commodity.EventName,
                        'BeginTime':this.commodity.BeginTime,
                        'EndTime':this.commodity.EndTime,
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
            //删除商品
            deleteBargainItem: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.DeleteBargainItem',
                        'EventItemMappingID':this.args.EventItemMappingID,
                        'EventId':page.eventId
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
            //商品规格创建(编辑)
            setBargainDetails:function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        'action':'WEvents.Bargain.SetBargainDetails',
                        'EventItemMappingID':this.args.EventItemMappingID,
                        'EventId':page.eventId,
                        'SinglePurchaseQty':this.sku.SinglePurchaseQty,
                        'BargaingingInterval':this.sku.BargaingingInterval,
                        'ItemId':this.sku.ItemId,
                        'EventSkuInfoList':this.sku.SkuInfoList                 
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
            //修改团购活动
            updateEvent: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'UpdatePanicbuyingEvent',
                        'EventName':this.args.EventName,
                        'EventTypeId':page.pageType,
                        'BeginTime':this.args.BeginTime,
                        'EndTime':this.args.EndTime,
                        'EventStatus':this.args.EventStatus,
                        'EventId':page.eventId
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取砍价活动与商品列表
            GetCouponTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:'Marketing.Coupon.GetCouponTypeList',
                        CouponTypeName:this.seach.CouponTypeName,
                        ParValue:this.seach.ParValue,
                        PageIndex:this.args.PageIndex,
                        PageSize:this.args.PageSize

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
            //获得活动商品列表
            getBargainItemList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    type: "post",
                    data:
                    {
                        'action': 'WEvents.Bargain.GetBargainItemList',
                        'EventId':page.eventId,
                    },
                    success: function (data) {
                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得活动商品列表
            getEventGoodsList: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'GetEventMerchandise',
                        'EventId':page.eventId
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获取活动商品的规格
            getBargainDetails: function (callback) {
                $.util.ajax({
                    url: '/ApplicationInterface/Gateway.ashx',
                    type: "post",
                    data:
                    {
                        'action': 'WEvents.Bargain.GetBargainDetails',
                        'EventItemMappingID':this.args.EventItemMappingID, //抢购活动商品关联Sku唯一标识
                        'ItemId':this.sku.ItemId
                    },
					beforeSend: function () {
		                $.util.isLoading()

		            },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //修改活动商品的规格
            updateEventItemSku: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'SaveEventItemSku',
                        'MappingId':this.sku.MappingId,  //抢购活动商品关联Sku唯一标识
                        'Qty':this.sku.numArr[0],             //商品数量
                        'KeepQty':this.sku.numArr[1],     //已售商品基数
                        'SoldQty':this.sku.numArr[2],     //真实销售数量
                        'InverTory':this.sku.numArr[3],  //库存
                        'SalesPrice':this.sku.numArr[5]  //活动价格
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //删除活动商品的规格
            delEventItemSku: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'RemoveEventItemSku',
                        'MappingId':this.sku.MappingId  //抢购活动商品关联Sku唯一标识
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //添加商品新规格
            addEventItemSku:function(callback){
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'AddEventItemSku',
                        'MappingId':this.sku.MappingId,  //抢购活动商品关联Sku唯一标识
                        'EventId':page.eventId,
                        'ItemID':this.sku.ItemId,
                        'SkuList':this.sku.SkuList,
                        'SinglePurchaseQty':this.sku.SinglePurchaseQty
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得商品规格
            getItemSku:function(callback){
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'GetItemSku',
                        'EventId':page.eventId,
                        'ItemId':this.sku.ItemId
                    },
                    success: function (data) {
                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //删除活动商品
            delEventGoods: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'RemoveEventItem',
                        'EventItemMappingId':this.args.EventMappingId  //抢购活动商品关联Sku唯一标识
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得一级分类
            getCategory: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'GetParentCategoryList'
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得商品列表通过分类
            getQueryGoods: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'GetItemList',
                        'ItemCategoryID':this.goods.categoryId,
                        'ItemName':this.goods.itemName,
                        'PageIndex':this.goods.pageIndex,
                        'PageSize':this.goods.pageSize
                    },
					beforeSend: function () {
		                $.util.isLoading()
		            },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //获得商品列表通过分类
            uploadImage: function (callback) {
                $.util.ajax({
                    url: page.url,
                    type: "post",
                    data:
                    {
                        'action': 'SaveImage',
                        'EventItemMappingId':this.goods.EventItemMappingId,
                        'ImageUrl':this.goods.ImageUrl
                    },
                    success: function (data) {

                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        }
                        else {
                            alert(data.Message);
                        }
                    }
                });
            }
        }

    };
    page.init();
});

