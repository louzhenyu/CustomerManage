define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            listItem: $("#unitList"),             //所有的查询条件层dom
            Tooltip:$("#Tooltip"),
            listTable:$(".listTable .easyui-datagrid"), //
            click:true,

            panlH:116                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            this.initEvent();


        },
        unitBtnEvent:function(){
            var that = this;
            that.elems.listItem.delegate(".icon", "click", function () {   //删除操作按钮操作

                var me = $(this);
                if (me.parent().hasClass('pro')) {
                    me.parents(".skuList").remove();
                    if (that.elems.sku.find(".skuList").length == 0) {
                        $("#dataState").fadeIn();
                    }
                }
                me.parent().remove();
            }).delegate(".unitBtn", "mouseenter", function () {  //停靠显示和隐藏删除按钮
                $(this).find(".icon").show(0)
            }).delegate(".unitBtn", "mouseleave", function () {
                $(this).find(".icon").hide(0)
            })
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

             that.elems.simpleQueryDiv.delegate(".listBtn","click",function(){
                 debugger;
                  var me=$(this);

                  if(!me.hasClass("show")){
                      that.elems.simpleQueryDiv.find(".listBtn").removeClass("show");
                       me.addClass("show");
                      debugger;
                      if(me.data("coupontype")==1){
                          $('#ParValue').numberbox({
                              max:0,
                              disabled:true
                          });
                          $('#ParValue').siblings(".textbox.numberbox").css({"background":"#efefef"});
                          var classs="."+me.data("hide");

                          $(classs).hide(0);
                      }else if(me.data("coupontype")==0){
                          $('#ParValue').numberbox({
                              max:null,
                              disabled:false
                          });
                          var classs="."+me.data("show");
                          $(classs).show();

                          $('#ParValue').siblings(".textbox.numberbox").css({"background":"#fff"});
                      }
                  }


             }) ;
            that.elems.simpleQueryDiv.find(".listBtn").eq(0).trigger("click");
            that.elems.sectionPage.delegate(".radio","click",function(e){
                var me= $(this), name= me.data("name");
                me.toggleClass("on");
                if(name){
                    var  selector="[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                    $(selector).siblings().find(".easyui-numberbox").numberbox({
                        disabled:true
                        //required: false
                    });
                    $(selector).siblings().find(".easyui-datebox").datebox({
                        disabled:true
                        //required: false
                    });
                    if(me.data("validity")=="time") {
                        me.siblings().find(".easyui-datebox").datebox({
                            disabled:false,
                            required: true
                        });
                    } else if(me.data("validity")=="day"){
                        me.siblings().find(".easyui-numberbox").numberbox({
                            disabled:false,
                            required: true
                        });
                    }
                }
                $.util.stopBubble(e);
            }).delegate(".submitBtn","click",function(){ //新增优惠券
                if($("#addCoupon").form("validate")){
                     var fields=$("#addCoupon").serializeArray();

                    that.loadData.operation(fields,"addCoupon",function(){
                          alert("操作成功");
                        var mid = JITMethod.getUrlParam("mid");
                        location.href = "queryList.aspx?mid=" + mid;
                    })
                }

            });

            that.elems.sectionPage.find("[data-validity].radio").eq(0).trigger("click");
            $(".listTable").delegate(".del","click",function(e){
                debugger;
                  var  index=$(this).data("index");
                   // index=parseInt(index);
                  that.elems.listTable.datagrid("deleteRow",index);

                var  data=that.elems.listTable.datagrid("getData");
                that.elems.listTable.datagrid("loadData",data);

            });
            that.elems.sectionPage.delegate(".checkBox","click",function(e){
                var me= $(this);
                me.toggleClass("on");
                $.util.stopBubble(e);
                debugger;
                $('#addUnit').tooltip("hide");  //取消按钮
                var  className="."+me.data("toggleclass");
                if(me.hasClass("on")&&me.data("flag")=="SuitableForStore"){
                   $(className).hide(0);
                }else if(me.data("flag")=="SuitableForStore") {
                    $(className).show(0);
                }
                if(me.hasClass("on")&&me.data("flag")=="ConditionValue") {
                    $('#ConditionValue').numberbox({
                        min: 0,
                        max:null,
                        disabled: false
                    });
                    $('#ConditionValue').siblings(".textbox.numberbox").css({"background":"#fff"});
                }else if(me.data("flag")=="ConditionValue"){
                    $('#ConditionValue').numberbox({
                      /*  max: 0,*/
                        disabled: true
                    });
                    $('#ConditionValue').siblings(".textbox.numberbox").css({"background":"#efefef"});
                }
            });
            that.elems.sectionPage.find(".checkBox").trigger("click").trigger("click");
            that.elems.Tooltip.delegate(".commonBtn","click",function(e){
                  debugger;
                var  type= $(this).data("flag");
                var parms={};
                if(type=="sales"){
                    var nodes=$("#Tooltip").find(".treeNode").tree('getChecked');

                    that.elems.listTable.datagrid({

                        data:nodes,
                        columns:[[
                            {field:'text',title:'',width:100,
                                formatter: function(value,row,index){
                                      return '<div class="texlist">'+value+' <img class="del" data-index="'+index+'" src="images/delicon.png"></div>'
                                }

                            }
                        ]]
                    })

                }
                if(type=="cannel"){
                    $('#addUnit').tooltip("hide");  //取消按钮
                }


            });
            /**************** -------------------初始化easyui 控件 start****************/
            var  wd=160,H=32;
            $("#applicationType").combobox({
                valueField: 'id',
                textField: 'text',
                onChange:function(newValue, oldValue){
                    if(newValue=="0"){
                        that.elems.listTable.datagrid({
                            title:"已选门店",
                            data:[]
                        })
                    }
                    if(newValue=="1"){
                        that.elems.listTable.datagrid({
                            title:"已选分销商",
                            data:[]
                        })
                    }
                },
                data:[{
                "id":0,
                    "text":"门店"

            },{
                "id":1,
                 "text":"分销商"

            } ]
            });
            that.loadData.get_unit_tree(function(data) {
                debugger;
                that.loadData.getUitTree.node=data[0].id;
                   /* that.loadData.get_unit_tree(function(datas) {
                        debugger;
                        data[0].children=datas;*/
                        that.unitTree=data;
                        $("#Tooltip").find(".treeNode").tree({
                            // animate:true
                            lines: true,
                            checkbox: true,
                            valueField: 'id',
                            textField: 'text',

                            data: data

                        });
                  /*  })*/
            });
            $('#addUnit').tooltip({
                content: function(){
                   return  $("#Tooltip");
                },
                showEvent: 'click',
                hideEvent:'dblclick',
                onShow: function(){
                    var str= $("#applicationType").combobox("getValue");
                    if(str==0) {
                        $(this).tooltip('tip').css({"width":"222" });
                        var nodes = $("#Tooltip").find(".treeNode").tree('getChecked');
                        /*$.each(nodes, function () {
                            var me = this;
                            $("#Tooltip").find(".treeNode").tree('uncheck', me.target);
                        });*/
                        var t = $(this);
                        t.tooltip('tip').unbind().bind('mouseenter', function () {
                            t.tooltip('show');
                        }).bind('mouseleave', function () {
                            t.tooltip('hide');
                        });
                    }else{
                        $(this).tooltip('tip').css({"display":"none"});
                       that.addNumber();
                    }
                },
                onHide:function(){
                    var nodes = $("#Tooltip").find(".treeNode").tree('getChecked');
                    $.each(nodes, function () {
                     var me = this;
                     $("#Tooltip").find(".treeNode").tree('uncheck', me.target);
                     });
                }
            });
            /**************** -------------------初始化easyui 控件  End****************/


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
            $("#win").delegate(".datagrid-header-check", "mousedown", function (e) {
                $(this).toggleClass("on");
                return false;
            });
            $('#win').delegate(".saveBtn","click",function(e){
                var nodes=$("#searchGrid").datagrid("getChecked");
                var data= that.elems.listTable.datagrid("getData");
                debugger;
                var  loadDatas={};
                if(!(nodes.length>0)){
                    $.messager.alert("提示" ,"请选择一个分销商");
                    return false;
                }

                if(data.rows.length>0){
                    loadDatas=data;
                   for(var i=0;i<nodes.length;i++){
                       var isAdd=true
                         for(var j=0;j<data.rows.length;j++){
                             if(data.rows[j].RetailTraderID==nodes[i].RetailTraderID) {
                                    isAdd=false;
                             }
                         }
                       if(isAdd) {
                           loadDatas.rows.push(nodes[i])
                       }
                    }

                }else{

                    loadDatas.rows= nodes;
                }

                if(loadDatas.rows.length>0){
                    that.elems.listTable.datagrid({

                        data:loadDatas,
                        columns:[[
                            {field:'RetailTraderName',title:'',width:100,
                                formatter: function(value,row,index){
                                    return '<div class="texlist">'+value+' <img class="del" data-index="'+index+'" src="images/delicon.png"></div>'
                                }

                            }
                        ]],onLoadSuccess:function(){
                            $("#win").window("close");
                        }
                    });
                }



            }).delegate(".searchBtn","click",function(){
                 var t = $('#unitTree').combotree('tree');	// 获取树对象
                var n = t.tree('getSelected');		// 获取选择的节点
                debugger;
                if(!n){
                    $.messager.alert("提示","必须选择一家门店");
                    return;
                }
                that.loadData.seach.UnitID="";
                if(n&&n.length>1 ){
                    that.loadData.seach.UnitID=[];
                    for(var i=0;i< n.length;i++){
                        that.loadData.seach.UnitID.push(n[i].id);
                    }
                } else if(n){
                    that.loadData.seach.UnitID= n.id;
                }
                //that.loadData.seach.UnitID=$('#unitTree').combobox('getValue');
                that.loadData.seach.RetailTraderName=$()
                var fields=$("#payOrder").serializeArray();
                $.each(fields,function(index,field){
                    that.loadData.seach[field.name]=field.value;
                });
                that.loadData.GetRetailTraders(function(data){
                   var Data=data.Data.RetailTraderList;
                   if(data.Data.RetailTraderList&&data.Data.RetailTraderList.length>0) {
                       $("#searchGrid").datagrid({
                           data: data.Data.RetailTraderList,
                           singleSelect:false,//多选
                           frozenColumns: [
                               [
                                   {
                                       field: 'ck',
                                       width: 70,
                                       title: '全选',
                                       align: 'center',
                                       checkbox: true
                                   }
                               ]
                           ],//显示复选框
                           columns: [
                               [
                                   {field: 'RetailTraderName',align:'left',title: '分销商名称',width:410}
                               ]
                           ]
                       })
                   }else{
                       $.messager.alert("提示","该门店无任何分销商");
                   }
               })


            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            /*that.elems.tabelWrap.delegate(".opt","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="add") {
                    if(row.IsPaid!=1&&row.Status!=10&&row.Status!=11) {
                        that.addNumber(row);
                    }
                }
                if(optType=="delete"){
                    $.messager.confirm("删除优惠券操作","确认要删除该条记录",function(r){
                             if(r){
                                 alert("执行删除")
                             }
                    })
                }
            })*/
            /**************** -------------------列表操作事件用例 End****************/

            that.unitBtnEvent();



            $('#startDate').datebox().datebox('calendar').calendar({
                validator: function(date){
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                   //var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+10);
                    //return d1<=date && date<=d2;
                    return d1<=date;
                }
            });
            $('#expireDate').datebox().datebox('calendar').calendar({
                validator: function(date){
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+1);
                    //var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+10);
                    //return d1<=date && date<=d2;
                    return d1<=date;
                }
            });

        },


        //收款
        addNumber:function(data){
            debugger;
            var that=this;
            that.elems.optionType="add";
            $('#win').window({title:"选择分销商",width:470,height:530,top:($("body").height() - 530),
                left:($(window).width() - 530) * 0.5});

            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_AddUnitList');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);

              $("#win").window("open");


                   $("#unitTree").combotree({
                       //panelWidth:300,
                       width:220,
                       //animate:true,
                       multiple:false,
                       valueField: 'id',
                       textField: 'text',
                       data:that.unitTree
                   });
            $("#unitTree").combotree("setText","选择分销商所属门店");

            $("#searchGrid").datagrid({
                data:[],
                height:300,
                singleSelect:false,//多选
                frozenColumns: [
                    [
                        {
                            field: 'ck',
                            width: 70,
                            title: '全选',
                            align: 'center',
                            checkbox: true
                        }
                    ]
                ],//显示复选框
                columns: [
                    [
                        {field: 'RetailTraderName',align:'left',title: '分销商名称',width:410}
                    ]
                ]
            })
        },

        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="0"?"":filed.value;
                that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });





        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
        },

        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 0,
                PageSize: 6,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:15
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                item_category_id:null,
                SalesPromotion_id:null,
                form:{
                    item_code:"",
                    item_name:"",
                    item_status:null,
                    item_can_redeem:null
                },
                RetailTraderName:"",
                UnitID:""
            },
            getUitTree:{
               node:""
            },
            opertionField:{},

            getCommodityList: function (callback) {
                $.util.oldAjax({
                    url: "/module/basic/Item/Handler/ItemHandler.ashx",
                      data:{
                          action:'search_item',
                          item_category_id:this.seach.item_category_id,
                          SalesPromotion_id:this.seach.SalesPromotion_id,
                          page:this.args.page,
                          start:this.args.start,
                          limit:this.args.limit,
                          form:{
                              item_code:"",
                              item_name:this.seach.form.item_name,
                              item_status:this.seach.form.item_status,
                              item_can_redeem:null
                          }
                      },
                      success: function (data) {
                          debugger;
                        if (data.topics) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },
            get_unit_tree: function (callback) {
                $.ajax({
                    url: "/Framework/Javascript/Biz/Handler/UnitSelectTreeHandler.ashx?method=get_unit_tree&parent_id=&_dc=1433225205961&node="+this.getUitTree.node+"&multiSelect=false&isSelectLeafOnly=false&isAddPleaseSelectItem=false&pleaseSelectText=--%E8%AF%B7%E9%80%89%E6%8B%A9--&pleaseSelectID=-2",
                    success: function (data) {
                        debugger;
                        var datas=JSON.parse(data);
                        if (datas&&datas.length>0) {
                            if (callback) {
                                callback(datas);
                            }

                        } else {
                            alert("加载数据不成功");
                        }
                    }
                });
            },

            GetRetailTraders: function (callback) {
                debugger;
                $.util.ajax({
                    url: "/ApplicationInterface/AllWin/RetailTrader.ashx",
                    data:{
                        action:"GetRetailTraders",
                        UnitID:this.seach.UnitID,
                        PageSize:1000000,
                        PageIndex:1,
                        RetailTraderName:this.seach.RetailTraderName
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },


            getClassify: function (callback) {
                $.util.oldAjax({
                    url: "/module/basic/ItemCategoryNew/Handler/ItemCategoryTreeHandler.ashx",
                    data:{
                        node:"root",
                        isAddPleaseSelectItem:true,
                        pleaseSelectText:"请选择",
                        pleaseSelectID:"0",
                        bat_id:this.args.bat_id,
                        Status:"1"


                    },
                    success: function (data) {
                        if (data) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("分类加载异常请联系管理员");
                        }
                    }
                });
            },
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"Marketing.Coupon.SetCoupon"}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data["UsableRange"]=$(".radio.on[data-usablerange]").data("usablerange");

                var str= $("#applicationType").combobox("getValue");
                //适用门店(1=所有门店；2=部分门店/分销商;3=所有分销商)
                        if( $(".checkBox.on[data-flag='SuitableForStore']").length>0){
                            if(str==0){
                                prams.data["SuitableForStore"]=1
                            }else if(str==1){
                                prams.data["SuitableForStore"]=3
                            }
                        }else{
                            prams.data["SuitableForStore"]=2;
                            prams.data.ObjectIDList=[];
                           var data= page.elems.listTable.datagrid("getData");
                           for(var i=0;i<data.rows.length; i++){
                               if(str==0) {
                                   prams.data.ObjectIDList.push({"ObjectID": data.rows[i].id});
                               }else{
                                   prams.data.ObjectIDList.push({"ObjectID": data.rows[i].RetailTraderID});
                               }
                            }

                        }
                $.each(pram, function (index, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value;
                    }
                })

                if(prams.data["ParValue"]==="0.00"||prams.data["ParValue"]==="0"){
                   $.messager.alert("错误提示","优惠券面值必须大于零");
                    return false;
                }



                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }


        }

    };
    page.init();
});

