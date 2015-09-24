define(['newJquery', 'tools', 'template','kkpager','datetimePicker','artDialog','cookie','swfobject','fullAvatarEditor'], function () {
    window.alert=function(content){
        var d = dialog({
            fixed:true,
            title: '提示',
            content: content
        });
        d.showModal();
        setTimeout(function () {
            d.close().remove();
        }, 2000);
    }
    //
    var page =
    {
        pageSize: 2,
        //默认控制条数
        currentPage: 0,
        //图文素材ID
        url: "/ApplicationInterface/Events/EventsGateway.ashx",
        //关联到的类别
        elems:
        {
            searchBtn: $("#searchBtn"),                //搜索按钮
            keyword:$("#keyword"),
            //选择活动详情的下拉框
            uiMask: $("#ui-mask"),
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
            //团购  抢购 市场都是一套逻辑  故用pageType标识
            var pageType=$.util.getUrlParam("pageType");
            //活动id
            var eventId=$.util.getUrlParam("eventId");
            var pageName = decodeURIComponent($.util.getUrlParam("pageName"));
            this.eventId=eventId;
            var pageStr = "";
            if (pageName) {
                pageStr = pageName;
                document.title = pageStr;
            } else {
                var d = dialog({
                    fixed: true,
                    title: '提示:哎呀妈呀',
                    content: "未配置活动名称!"
                });
                d.showModal();
                document.title = pageStr + "管理";
            }
            that.pageType=pageType;
            that.pageStr=pageStr;
            $("#addText").html("添加新"+pageStr+"");
            $("#textName").html(pageStr+"商品管理");
            $(".groupSet").html(pageStr+"设置");
            $(".shopManage").html(pageStr+"商品管理");
            //要展示的层页面
            var showPage=$.util.getUrlParam("showPage");
            this.showPage=showPage;
            //把table的状态修改过来
            $(".groupNav").find("."+this.showPage).addClass("on").siblings().removeClass("on");
            $("[data-table='block']").hide();
            //要跳转的地址
            $("#toBrowser").attr("href","GroupList.aspx?pageType="+that.pageType+"&pageName="+pageName);

        },
        init: function () {
            var that=this;
            this.initObj();
            //初始化详细页面信息
            this.initDetail();
            this.initEventGoodsList();
            this.initEvent();
            //初始化日期控件
            $('#beginTime').datetimepicker({
                lang:"ch",
                format:'Y-m-d H:i',
                step:5 //分钟步长
            });
            $('#endTime').datetimepicker({
                lang:"ch",
                format:'Y-m-d H:i',
                step:5 //分钟步长
            });
        },
        initDetail:function(){
            var that=this;

            //请求结果
            this.loadData.getEventDetail(function(data){
                var detailInfo=data.Data.PanicbuyingEvent;
                var status=detailInfo.EventStatus;
                detailInfo.pageName=that.pageStr;
                detailInfo.pageType=that.pageType;
                if(detailInfo.EventStatus==10){
                    detailInfo.EventStatus="未上架";
                }
                if(detailInfo.EventStatus==20){
                    detailInfo.EventStatus="已上架";
                }
                if(detailInfo.EventStatus==30){
                    detailInfo.EventStatus="已结束";
                }
                var html=bd.template("tpl_detail",{item:detailInfo});
                $("#eventName").val(detailInfo.EventName);
                $("#beginTime").val(detailInfo.BeginTime);
                $("#endTime").val(detailInfo.EndTime);
                $("#curEventName").html(detailInfo.EventName);

                var str="";
                if(status==10){
                    str="未上架";
                }
                if(status==20){
                    str="已上架";
                }
                if(status==30){
                    str="已结束";
                }
                $("#statusText").html(str);
                $("#statusText").data("status",status);
                $("#detailInfo").html(html);
            });
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
            //初始化事件集
            var that = this;
            $("#status").mouseover(function(e){
                $(".dropList").stop().show(500);
            });
            $(".dropList").mouseout(function(e){
                $(".dropList").stop().hide(500);
                that.stopBubble(e);
            });
            //选择上架状态
            $(".dropList").delegate("span", "click", function (e) {
                var $this = $(this);
                $("#statusText").html($this.html());
                $("#statusText").data("status", $this.data("status"));
                $(".dropList").stop().hide(500);
                that.stopBubble(e);
            });
            //关闭弹出层
            $(".hintClose").bind("click",function(){
                that.elems.uiMask.slideUp();
                $(this).parent().parent().fadeOut();
            });
            //table切换事件
            $(".groupNav").delegate("li","click",function(e){
                var $this=$(this);
                //设置层都隐藏
                $("[data-table='block']").hide();
                //关联id让对应的ID层显示
                $("#"+$this.attr("class").split(" ")[0]).show(200);
                $this.addClass("on").siblings().removeClass("on");
                that.stopBubble(e);
            });
            //确定修改活动
            $("#btnSureUpdate").click(function(e){
                if($("#eventName").val().length==0){
                    alert("活动名称不能为空!");
                    return;
                }
                if($("#beginTime").val().length==0){
                    alert("活动开始时间不能为空!");
                    return;
                }
                if($("#endTime").val().length==0){
                    alert("活动结束时间不能为空!");
                    return;
                }
                that.loadData.args.EventName=$("#eventName").val();
                that.loadData.args.BeginTime=$("#beginTime").val();
                that.loadData.args.EndTime=$("#endTime").val();
                that.loadData.args.EventStatus=$("#statusText").data("status");
                //添加新团购
                that.loadData.updateEvent(function(data){
                    alert("活动设置修改成功!");
                });

                //跳转到商品管理界面
                that.stopBubble(e);
            });
            //上移  下移操作
            $("#goodsList").delegate(".moveUp","click",function(e){
                //当前点击对象
                var $t=$(this);
                debugger;
                //商品item对象
                var father=$t.parent().parent().parent();
                //获得位置
                var position=father.attr("data-position");
                if(!$t.hasClass("first")){
                    //当前的移动的商品id
                    page.moveItemId=father.data("itemid");
                    //上一个dom节点
                    var prevJquery=father.prev();
                    //上一个商品id
                    page.itemId=prevJquery.data("itemid");
                    var prevPosition=prevJquery.attr("data-position");
                    //上一个dom节点=1
                    prevJquery.attr("data-position",parseInt(prevPosition)+1);
                    father.attr("data-position",position-1);
                    if((position-1)==1){
                        //设置当前的添加为first不能再点击上移
                        $t.addClass("first");
                    }
                    //设置向下不能点击
                    if(prevJquery.attr("data-position")==$("#goodsList .groupShopManage-list-item").length){
                        prevJquery.find(".moveDown").addClass("last");
                    }
                    $t.parent().find(".moveDown").removeClass("last");
                    prevJquery.before(father);
                    //设置前一个为可以进行上移的操作
                    prevJquery.find(".moveUp").removeClass("first");
                    //数据提交
                    that.loadData.setEventItemDisplay(function(){});
                }
                that.stopBubble(e);
            }).delegate(".moveDown","click",function(e){   //商品下移
                var $t=$(this);
                debugger;
                var father=$t.parent().parent().parent();
                //获得位置
                var position=father.attr("data-position");
                if(!$t.hasClass("last")){
                    //当前的移动的商品id
                    page.moveItemId=father.data("itemid");
                    //下一个dom节点
                    var nextJquery=father.next();
                    //下一个商品id
                    page.itemId=nextJquery.data("itemid");
                    var nextPosition=nextJquery.attr("data-position");
                    nextJquery.attr("data-position",nextPosition-1);
                    father.attr("data-position",parseInt(position)+1);
                    if((nextJquery.attr("data-position")==1)){
                        nextJquery.find(".moveUp").addClass("first");
                    }
                    //删除要上移的最后的一个标识last
                    nextJquery.find(".moveDown").removeClass("last");
                    father.find(".moveUp").removeClass("first");
                    //下移的时候判断是否是最后一个   是的话 不能下移
                    if(nextPosition==$("#goodsList .groupShopManage-list-item").length){
                        $t.addClass("last");
                    }
                    nextJquery.after(father);
                    nextJquery.find(".moveDown").removeClass("first");
                    //数据提交
                    that.loadData.setEventItemDisplay(function(){});
                }
                that.stopBubble(e);
            }).delegate("._showMore","click",function(e){  //点击展开更多
                var $t=$(this);
                var pclass=$t.find("p");
                if(pclass.hasClass("showUp")){
                    $t.parent().find(".groupShopNorms").hide(500);
                    $t.find("p").removeClass("showUp").css("width","96px").html("下拉查看更多");
                }else{
                    $t.parent().find(".groupShopNorms").show(500);
                    $t.find("p").addClass("showUp").css("width","45px").html("收起");
                }
                that.stopBubble(e);
            });
            //点击添加商品
            $("#shopManage").delegate(".addShopBtn","click",function(e){
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
            //点击删除商品
            $("#goodsList").delegate(".delShopBtn","click",function(e){
                var $this=$(this);
                var d = dialog({
                    fixed:true,
                    title: '提示',
                    content: '是否要删除该商品?删除后不可恢复!',
                    okValue: '确定',
                    ok: function () {
                        this.title('提交中…');
                        var mappingId=$this.parent().parent().parent().data("eventmappingid");
                        that.loadData.args.EventMappingId=mappingId;
                        //删除商品
                        that.loadData.delEventGoods(function(data){
                            $this.parent().parent().parent().hide(500,function(){
                                $this.parent().parent().parent().remove();
                                return true;  //return false为不关闭层
                            });
                        });

                    },
                    cancelValue: '取消',
                    cancel: function () {}
                });
                d.showModal();
                that.stopBubble(e);
            });
            //点击空白区域让规格收起
            $("#goodsList").delegate(".groupShopManage-list-item","click",function(e){
                var $this=$(this);
                if(e.target.className=="groupShopManage-list-item"||e.target.className=="textInfo"){
                    var showHide=$this.find(".showHide");
                    if(showHide.is(":hidden")){
                        showHide.slideDown(300);
                    }else{
                        showHide.slideUp(300);
                    }
                }

                that.stopBubble(e);
            });
            //点击修改操作
            $("#goodsList").delegate(".modifyBtn","click",function(e){
                var $this=$(this);
                //让保存 修改按钮显示
                $this.parent().addClass("on");
                //让所有的input为可编辑
                var inputs=$this.parent().parent().find("input");
                inputs.each(function(i,j){
                    var $t=$(this);
                    if(i!=4){  //4下标为原价  不可以修改
                        $t.removeAttr("disabled");
                    }
                });
                //让修改按钮隐藏
                $this.hide();
                that.stopBubble(e);
            });
            //点击修改的取消操作
            $("#goodsList").delegate(".commonCancel","click",function(e){
                var $this=$(this);
                //让保存和取消隐藏
                $this.parent().parent().removeClass("on");
                //让所有的input为只读属性
                $this.parent().parent().parent().find("input").attr("disabled","disabled");
                //让修改按钮显示
                $this.parent().parent().find(".modifyBtn").show(500);
                that.stopBubble(e);
            });
            //点击修改的保存操作
            $("#goodsList").delegate(".commonSave","click",function(e){
                var $this=$(this);
                //让所有的input为只读属性
                var inputList=$this.parent().parent().parent().find("input");
                var arr=[],index=0,tips=["商品数量","已售基数","真实数量","当前库存","原价","活动价"];
                //顺序是 0下标为 商品数量  1为已售商品基数  2为真实销售数量  3为当前库存
                inputList.each(function(i,obj){
                    var othis=$(this);
                    var value=othis.val();
                    var ago=value+"";//如果出错则恢复
                    if(i<4&&isNaN(parseInt(value))){   //i>4为价格
                        alert(tips[i]+"只能为整数!");
                        othis.css({border:"1px solid red"});
                        othis.focus();
                    }else if(i>4&&isNaN(parseFloat(value))){
                        alert(tips[i]+"只能为整数或者小数!");
                        othis.css({border:"1px solid red"});
                        othis.focus();
                    }else{

                        othis.css({"border-color":"","border-width":1,"border-style":""});
                        if(i<4){
                            othis.val(parseInt(value));
                        }else{
                            othis.val(parseFloat(value));
                        }
                        arr.push(parseFloat(value));
                    }

                });
                //进行数据更新
                if(arr.length==6){
                    //参数赋值
                    that.loadData.sku.numArr=arr;
                    //sku的映射id
                    that.loadData.sku.MappingId=$this.parent().parent().parent().data("mappingid");
                    that.loadData.updateEventItemSku(function(data){
                        //让保存和取消隐藏
                        $this.parent().parent().removeClass("on");
                        inputList.attr("disabled","disabled");
                        //让修改按钮显示
                        $this.parent().parent().find(".modifyBtn").show(500);
                    });
                }

                that.stopBubble(e);
            });
            //点击添加规格的操作
            $("#goodsList").delegate(".addSizeBtn","click",function(e){
                var $this=$(this);
                var itemId=$this.parent().parent().parent().data("itemid");
                var mappingId=$this.parent().parent().parent().data("eventmappingid");
                //sku的映射id
                that.loadData.sku.MappingId=mappingId;
                that.loadData.sku.ItemId=itemId;
                //获得sku
                that.loadData.getItemSku(function(data){
                    var list=data.Data.SkuList;
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
                        $("#goodsInfo").find(".shopName").html(data.Data.ItemName).end().find(".inputBox input").val(data.Data.SinglePurchaseQty||0);
                        that.showElements("#addNewRules");
                    }
                });

                that.stopBubble(e);
            });
            //选中规格和取消规格
            $("#rules").delegate(".item","click",function(e){
                var $t=$(this);
                if(e.target.className=="checkBox"||e.target.nodeName=="SPAN"){
                    $t.toggleClass("on");
                }
                var inputs=$t.find("input");
                if($t.hasClass("on")){
                    inputs.each(function(i){
                        var $tt=$(this);
                        if(i!=2){
                            $tt.removeAttr("disabled");
                        }
                    });
                }else{
                    inputs.each(function(i){
                        var $tt=$(this);
                        $tt.attr("disabled","disabled");
                    })
                }
                that.stopBubble(e);
            });
            //保存规格
            $("#saveRules").click(function(e){
                //获得所有的选中的规格
				var isBool = true;
                var singlePurchaseQtyInput = $("#goodsInfo .inputBox input");
                var items=$("#rules").find(".on");
                var skuList=[];
                var tips=["商品数量","已售基数","活动价"];
//                if(singlePurchaseQtyInput.val()<=0){
//                    alert("每人限购数量不能为零");
//                    return;
//                }
                that.loadData.sku.SinglePurchaseQty = singlePurchaseQtyInput.val()||0;

                if(items.length==0){
                    alert("请至少选择一个规格后再提交!");
                    return;
                }
                items.each(function(i,o){
                    //每个item
                    var $t=$(this);
                    var mappingid=$t.data("mappingid");
                    var skuId=$t.data("skuid");
                    var obj={};
                    obj.MappingId=mappingid;
                    obj.SkuID=skuId;
                    var inputList=$t.find("input");
                    inputList.each(function(j,k){
                        var othis=$(this);
                        var value=othis.val();
                        var ago=value+"";//如果出错则恢复
                        if(isNaN(parseInt(value))){
                            alert(tips[j]+"只能为整数!");
                            othis.css({border:"1px solid red"});
                        }else{
                            othis.css({"border-color":"","border-width":1,"border-style":""});
                            othis.val(parseInt(value));
                            if(j==0){
                                obj.Qty=parseInt(value);
                            }
                            if(j==1){
                                obj.KeepQty=parseInt(value);
                            }
                            if(j==2){
                                obj.price = parseFloat(value);
                            }
                            if(j==3){
                                obj.SalesPrice=parseFloat(value);
                            }
                        }
                    });
					if(obj.Qty<obj.KeepQty){
						isBool = false;
						alert("已售数量不能大于商品数量");
						return false;
					}
                    skuList.push(obj);
                });
				if(!isBool){
					return false;
				}
                that.loadData.sku.SkuList=skuList;
                //添加规格
                //debugger;
                that.loadData.addEventItemSku(function(data){
                    //初始化详细页面头部信息
                    that.initDetail();
                    that.initEventGoodsList();
                    that.hideElements("#addNewRules");
                });
            });
            //点击删除规格的操作
            $("#goodsList").delegate(".delSizeBtn","click",function(e){
                var $this=$(this);
                var mappingId=$this.parent().parent().data("mappingid");
                //sku的映射id
                that.loadData.sku.MappingId=mappingId;
                //规格重新变动
                var skuPosition=$this.parent().parent().data("position");
                //规格的个数
                var skuLength=$this.parent().parent().parent().find(".groupShopNorms").length;
                that.loadData.delEventItemSku(function(data){
                    if(skuPosition==1){
                        //更改其他的规格顺序
                        var leftSku=$this.parent().parent().parent().find(".groupShopNorms");
                        //剩余的依次改变顺序
                        leftSku.each(function(i,o){
                            $(this).data("position",$(this).data("position")-1);
                            $(this).find(".position").html($(this).data("position")+": ");
                        });
                        $this.parent().parent().hide(500,function(){
                            $this.parent().parent().remove();
                        });//删除当前节点
                    }
                    //删除中间的只需要把position-1就可以
                    if(skuPosition>1&&skuPosition<skuLength){
                        //更改其他的规格顺序
                        var leftSku=$this.parent().parent().parent().find(".groupShopNorms");
                        //剩余的依次改变顺序
                        leftSku.each(function(i,o){
                            if(i!=0&&i>=skuPosition){
                                $(this).data("position",$(this).data("position")-1);
                                $(this).find(".position").html($(this).data("position")+": ");
                            }
                        });
                        $this.parent().parent().hide(500,function(){
                            $this.parent().parent().remove();
                        });//删除当前节点
                    }
                    //最后一个  则不用动只需要删除即可
                    if(skuPosition==skuLength){
                        $this.parent().parent().hide(500,function(){
                            $this.parent().parent().remove();
                        });
                    }

                });
                that.stopBubble(e);
            });
            /*********************************************/
            //商品添加功能  包括商品查询  分页处理       //
            /*********************************************/
            //分类选择事件
            $("#category").delegate("span","click",function(e){
                var $t=$(this);
                var categoryId=$t.data("categoryid"),categoryName=$t.html();
                $("#categoryText").data("categoryid",categoryId).html(categoryName);
                $t.parent().hide(500);
                that.stopBubble(e);
            });
            //弹出类别选择事件
            $("#addNewGoods .selectBox").mouseover(function(e){
                $("#category").stop().show(500);
                that.stopBubble(e);
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
                        pno: 1,
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
                //设置商品名称
                $("#goodsInfo .shopName").html(shopName);
                that.hideElements("#addNewGoods");
                setTimeout(function(){
                    //获得sku
                    that.loadData.getItemSku(function(data){
                        var list=data.Data.SkuList;
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
                            that.showElements("#addNewRules");
                        }
                    });

                },550);
                that.stopBubble(e);
            });
            /*********************************************/
            //上传图片
            /*********************************************/
            $("#goodsList").delegate(".uploadPicBtn","click",function(e){
                that.showElements("#uploadPic");
                that.upload={};

                that.upload.eventMappingId=$(this).closest('.groupShopManage-list-item').data("eventmappingid");
                that.upload.domView=$(this).closest(".groupShopManage-list-item").find(".picWrap img");
                that.stopBubble(e);
            });
            swfobject.addDomLoadEvent(function () {
                //------------------------------------------------------------------------------示例一
                var webcamAvailable = false;
                var sourcePic1Url = $.Cookie('swf1');
                var callback = function (json) {
                    var id = this.id;
                    switch (json.code) {
                        case 2:
                            //如果加载原图成功，说明进入了编辑面板，显示保存和取消按钮，隐藏拍照按钮
                            if (json.type == 0) {
                                if(id == "swf1")
                                {
                                    $('#webcamPanelButton').hide();
                                    $('#editorPanelButtons').show();
                                }
                            }
                            //否则会转到上传面板
                            else {
                                //隐藏所有按钮
                                if(id == "swf1")$('#editorPanelButtons,#webcamPanelButton').hide();
                            }
                            break;
                        case 3:
                            //如果摄像头已准备就绪且用户已允许使用，显示拍照按钮。
                            if (json.type == 0) {
                            }
                            else {
                                if(id == "swf1")
                                {
                                    webcamAvailable = false;
                                    $('#webcamPanelButton').hide();
                                }
                                //如果摄像头已准备就绪但用户已拒绝使用。
                                if (json.type == 1) {
                                    alert('用户拒绝使用摄像头!');
                                }
                                //如果摄像头已准备就绪但摄像头被占用。
                                else {
                                    alert('摄像头被占用!');
                                }
                            }
                            break;
                        case 4:
                            alert("您选择的原图片文件大小（" + json.content + "）超出了指定的值(2MB)。");
                            break;
                        case 5:
                            //如果上传成功
                            if (json.type == 0) {

                                var e = this;
                                var html = $('<div class="imgList"/>');
                                for(var i = 0; i < json.content.avatarUrls.length; i++)
                                {
                                    html.append('<dl><dt></dt><dd><img src="'+'http://'+location.host+'/Framework/swfupload/asp.net/csharp/'+ json.content.avatarUrls[i] + '" /></dd></dl>');
                                }
                                var button = [];
                                //如果上传了原图，给个修改按钮，感受视图初始化带来的用户体验度提升
                                if(json.content.sourceUrl)
                                {
                                    button.push({text : '修改头像', callback:function(){
                                        this.close();
                                        $.Cookie(id, json.content.sourceUrl);
                                        location.reload();
                                        //e.call('loadPic', json.content.sourceUrl);
                                    }});
                                }
                                else
                                {
                                    $.Cookie(id, null);
                                }
                                var allUrl="http://"+location.host+"/Framework/swfupload/asp.net/csharp/"+json.content.avatarUrls[0]
                                button.push({text : '关闭窗口'});
                                var d = dialog({
                                    title: '图片已成功保存至服务器',
                                    content: html,
                                    okValue: '确定',
                                    time:2,
                                    ok: function () {
                                        var tt=this;
                                        this.title('提交中…');
                                        var mappingId=that.upload.eventMappingId;
                                        that.loadData.goods.EventItemMappingId=mappingId;
                                        that.loadData.goods.ImageUrl=allUrl;
                                        //保存图片
                                        that.loadData.uploadImage(function(data){
                                            that.upload.domView.attr("src",allUrl);
                                            tt.close()
                                            $(".hintClose").trigger("click");
                                            return true;  //return false为不关闭层
                                        });
                                        return false;
                                    },
                                    cancelValue: '关闭',
                                    cancel: function () {
                                        tt.close()
                                        $(".hintClose").trigger("click");
                                        return true;
                                    }
                                });
                                d.showModal();
                            }
                            else {
                                alert(json.type);
                            }
                            break;
                    }
                };
                var swf1 = new fullAvatarEditor('uploadContainer', 400,600, {
                    id : 'swf1',
                    upload_url : '/Framework/swfupload/asp.net/csharp/Upload.aspx',
                    //upload_url:'/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx',
                    src_url : sourcePic1Url,			//默认加载的原图片的url
                    tab_visible : false,				//不显示选项卡，外部自定义
                    button_visible : true,				//不显示按钮，外部自定义
                    src_upload : 2,						//是否上传原图片的选项：2-显示复选框由用户选择，0-不上传，1-上传
                    checkbox_visible : true,			//不显示复选框，外部自定义
                    browse_box_align : 38,				//图片选择框的水平对齐方式。left：左对齐；center：居中对齐；right：右对齐；数值：相对于舞台的x坐标
                    webcam_box_align : 38,				//摄像头拍照框的水平对齐方式，如上。
                    avatar_sizes : '258*200',			//定义单个头像
                    avatar_sizes_desc :'258*200像素',	//头像尺寸的提示文本。
                    //头像简介
                    avatar_intro : '最终生成258*200'+"的图片",
                    avatar_tools_visible:true			//是否显示颜色调整工具
                }, callback);
            });


        }
    };

    page.loadData =
    {
        args:{
            PageIndex:0,
            PageSize:10
        },
        //商品规格
        sku:{
            numArr:[],  //顺序是 0下标为 商品数量  1为已售商品基数  2为真实销售数量  3为当前库存
            mappingId:"",
            eventId:"",
            ItemId:"",
            SinglePurchaseQty:"",
            SkuList:[]
        },
        goods:{
            pageSize:10,
            pageIndex:0,
            itemName:""
        },
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
        //商品上下移
        setEventItemDisplay: function (callback) {
            $.util.ajax({
                url: page.url,
                type: "post",
                data:
                {
                    'action': 'UpdateEventItemDisplayIndex',
                    'EventID':page.eventId,
                    'MoveItemID':page.moveItemId,   //移动的商品ID
                    "ItemID":page.itemId      //商品ID
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
    //初始化
    page.init();
});