define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            listItem: $("#unitList"),             //所有的查询条件层dom
            Tooltip:$("#Tooltip"),
            listTable:$(".listTable .datagrid"), //
            productTable:$(".listTable .productGrid"),
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
        selectDataUnit:[],
        selectDataUnitList:[],
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

            that.elems.sectionPage.on("keyup", ".validDays .numberbox input", function () {
                $("#validDays").numberbox("setValue", $("#validDays").numberbox("getText"));

            });


            //点击查询按钮进行数据查询
            that.elems.simpleQueryDiv.delegate(".listBtn", "click", function () {
                debugger;
                var me = $(this);

                if (!me.hasClass("show")) {
                    that.elems.simpleQueryDiv.find(".listBtn").removeClass("show");
                    me.addClass("show");
                    debugger;
                    if (me.data("coupontype") == 1) {
                        $('#ParValue').numberbox({
                            max: 0,
                            disabled: true
                        });
                        $('#ParValue').siblings(".textbox.numberbox").css({"background": "#efefef"});
                        var classs = "." + me.data("hide");

                        $(classs).hide(0);
                    } else if (me.data("coupontype") == 0) {
                        $('#ParValue').numberbox({
                            max: null,
                            disabled: false
                        });
                        var classs = "." + me.data("show");
                        $(classs).show();

                        $('#ParValue').siblings(".textbox.numberbox").css({"background": "#fff"});
                    }
                }


            });
            if ($.util.getUrlParam("couponType") == 1) {
                that.elems.simpleQueryDiv.find(".listBtn").eq(1).trigger("click");
            } else {
                that.elems.simpleQueryDiv.find(".listBtn").eq(0).trigger("click");
            }

            that.elems.sectionPage.delegate(".radio", "click", function (e) {
                var me = $(this), name = me.data("name");
                me.toggleClass("on");
                if (name) {
                    var selector = "[data-name='{0}']".format(name);
                    $(selector).removeClass("on");
                    me.addClass("on");
                    $(selector).siblings().find(".easyui-numberbox").numberbox({
                        disabled: true
                        //required: false
                    });
                    $(selector).siblings().find(".easyui-datebox").datebox({
                        disabled: true
                        //required: false
                    });
                    if (me.data("validity") == "time") {
                        me.siblings().find(".easyui-datebox").datebox({
                            disabled: false,
                            required: true
                        });
                    } else if (me.data("validity") == "day") {
                        me.siblings().find(".easyui-numberbox").numberbox({
                            disabled: false,
                            required: true
                        });
                    }
                    var valueType = me.data("valuetype");
                    if (name == "unit") {
                        debugger;
                        if (valueType == "portion") {
                            me.siblings(".radioBtn").show();

                            if (that.selectDataUnitList.length > 0) {
                                $('[data-name="unit" ].listTable').show();
                            } else {
                                $('[data-name="unit"].listTable').hide();
                            }
                            that.selectUnit();
                        } else {
                            me.siblings(".radioBtn").hide();
                            $('[data-name="unit" ].listTable').hide();
                        }
                    }
                    if (name == "product") {
                        debugger;
                        if (valueType == "portion") {
                            that.selectProduct();
                            me.siblings(".radioBtn").show();
                        } else {

                            me.siblings(".radioBtn").hide();
                        }
                    }

                }
                $.util.stopBubble(e);
            }).delegate(".submitBtn", "click", function () { //新增优惠券

                if ($("#addCoupon").form("validate")) {
                    var fields = $("#addCoupon").serializeArray();

                    that.loadData.operation(fields, "addCoupon", function () {
                        if ($('#win1', window.parent.document).length > 0) {
                            window.parent.$('#win1').window('close');
                        } else {
                            alert("操作成功");
                           // var mid = JITMethod.getUrlParam("mid");
                            $.util.toNewUrlPath("queryList.aspx");
                        }

                    })
                }

            }).delegate(".radioBtn", "click", function () {
                if ($(this).data("name") == "product") {
                    that.selectProduct();
                } else {
                    that.selectUnit();

                }
            });

            that.elems.sectionPage.find("[data-validity].radio").eq(0).trigger("click");
            $(".listTable").delegate(".delete", "click", function (e) {
                debugger;
                var index = $(this).data("index"), id = $(this).data("id");
                // index=parseInt(index);
                that.elems.listTable.datagrid("deleteRow", index);

                var data = that.elems.listTable.datagrid("getData");
                that.elems.listTable.datagrid("loadData", data);
                if (data.rows.length == 0) {
                    that.elems.listTable.parents(".listTable").hide();
                }
                that.reloadUnitData([{id: id}]);
            });
            that.elems.sectionPage.delegate(".checkBox", "click", function (e) {
                var me = $(this);
                me.toggleClass("on");
                $.util.stopBubble(e);
                debugger;
                $('#addUnit').tooltip("hide");  //取消按钮
                var className = "." + me.data("toggleclass");
                if (me.hasClass("on") && me.data("flag") == "SuitableForStore") {
                    $(className).hide(0);
                } else if (me.data("flag") == "SuitableForStore") {
                    $(className).show(0);
                }
                if (me.hasClass("on") && me.data("flag") == "ConditionValue") {
                    $('#ConditionValue').numberbox({
                        min: 0,
                        max: null,
                        disabled: false
                    });
                    $('#ConditionValue').siblings(".textbox.numberbox").css({"background": "#fff"});
                } else if (me.data("flag") == "ConditionValue") {
                    $('#ConditionValue').numberbox({
                        /*  max: 0,*/
                        disabled: true
                    });
                    $('#ConditionValue').siblings(".textbox.numberbox").css({"background": "#efefef"});
                }
            });
            that.elems.sectionPage.find(".checkBox").trigger("click").trigger("click");
            /*  that.elems.Tooltip.delegate(".commonBtn","click",function(e){
             debugger;
             var  type= $(this).data("flag");
             var parms={};
             if(type=="sales"){
             var nodes=$("#Tooltip").find(".treeNode").tree('getChecked');



             }
             if(type=="cannel"){
             $('#addUnit').tooltip("hide");  //取消按钮
             }


             });*/
            /**************** -------------------初始化easyui 控件 start****************/
            $("#win").delegate(".optBtn", "click", function () {
                var nodeData = $('#unitTreeSelect').tree("getChecked");
                if (nodeData && nodeData.length > 0) {
                    for (var k = 0; k < nodeData.length; k++) {
                        var childen = nodeData.children;
                        if (!childen) {
                            var id = nodeData[k].id;

                            var nodeList = $("#unitGrid").datagrid('getData').rows;
                            var isDel = true;
                            if (nodeList.length > 0) {
                                for (var j = 0; j < nodeList.length; j++) {
                                    if (nodeList[j].Id == id) {
                                        isDel = false;
                                        $("#unitGrid").datagrid("uncheckRow", j);
                                    }
                                }

                            }
                            if (isDel) {
                                that.reloadUnitData([{id: id}]);
                            }
                        }


                    }


                }
            });

            //链接类型下拉框
            var wd = 160, H = 32;


            /*   $("#applicationType").combobox({
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

             }]
             });*/
            that.loadData.get_unit_tree(function (data) {
                debugger;
                that.loadData.getUitTree.node = data[0].id;
                /* that.loadData.get_unit_tree(function(datas) {
                 debugger;
                 data[0].children=datas;*/
                that.unitTree = data;
                $("#Tooltip").find(".treeNode").tree({
                    // animate:true
                    //lines: true,
                    checkbox: true,
                    valueField: 'id',
                    textField: 'text',

                    data: data

                });
                /*  })*/
            });
            $('#addUnit').tooltip({
                content: function () {
                    return $("#Tooltip");
                },
                showEvent: 'click',
                hideEvent: 'dblclick',
                onShow: function () {
                    var str = $("#applicationType").combobox("getValue");
                    if (str == 0) {
                        $(this).tooltip('tip').css({"width": "222"});
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
                    } else {
                        $(this).tooltip('tip').css({"display": "none"});
                        that.addNumber();
                    }
                },
                onHide: function () {
                    var nodes = $("#Tooltip").find(".treeNode").tree('getChecked');
                    $.each(nodes, function () {
                        var me = this;
                        $("#Tooltip").find(".treeNode").tree('uncheck', me.target);
                    });
                }
            });
            /**************** -------------------初始化easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            /* 属性名 属性值类型 描述 默认值
             title        string 窗口的标题文本。 New Window
             collapsible boolean 定义是否显示可折叠按钮。 true
             minimizable boolean 定义是否显示最小化按钮。 true
             maximizable boolean 定义是否显示最大化按钮。 true
             closable    boolean 定义是否显示关闭按钮。 true
             closed      boolean 定义是否可以关闭窗口。 false
             zIndex      number  窗口Z轴坐标。 9000
             draggable   boolean 定义是否能够拖拽窗口。 true
             resizable   boolean 定义是否能够改变窗口大小。 true
             shadow      boolean 如果设置为true，在窗体显示的时候显示阴影。 true
             inline      boolean 定义如何布局窗口，如果设置为true，窗口将显示在它的父容器中，否则将显示在所有元素的上面。 false
             modal       boolean 定义是否将窗体显示为模式化窗口。 true
             */


            $('#win').window({
                modal: true,
                shadow: false,
                collapsible: false,
                minimizable: false,
                maximizable: false,
                closed: true,
                closable: true,
                onBeforeOpen: function () {
                   // $("body").css({"overflowY": "hidden"});
                },
                onBeforeClose: function () {

                    $("body").css({"overflowY": "auto"});
                }
            });
            $('#panlconent').layout({
                fit: true
            });
            /* $("#win").delegate(".datagrid-header-check", "mouseDown", function (e) {
             $(this).toggleClass("on");
             return false;
             });*/
            $('#win').delegate(".saveBtn", "click", function (e) {
                that.selectDataUnitList = [];
                if (that.elems.optionType == "selectUnit") {
                    var nodeData = that.selectDataUnit;


                    if (nodeData && nodeData.length > 0) {
                        for (var k = 0; k < nodeData.length; k++) {
                            var children = nodeData[k].children;
                            if (children && children.length > 0) {
                                $.each(children, function (index, filed) {
                                    that.selectDataUnitList.push(children[index]);
                                });

                            }
                        }
                    }
                    that.elems.listTable.parents(".listTable").show();
                    var nodes = that.selectDataUnitList;
                    if (that.selectDataUnitList.length == 0) {
                        $.messager.alert("提示", '“已选择的门店中”为空请添加');
                        return false;
                    }
                    that.elems.listTable.datagrid({

                        data: nodes,
                        rownumbers: true,
                        singleSelect: true, //单选
                        height: 332, //高度
                        fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。                striped : true, //奇偶行颜色不同
                        collapsible: true,//可折叠
                        //数据来源
                        columns: [[
                            {field: 'text', title: '门店名称', width: 81, align: 'left', resizable: false,},
                            {
                                field: 'id', title: '删除', width: 20, align: 'left', resizable: false,
                                formatter: function (value, row, index) {
                                    return '<p class="fontC opt delete" data-id="' + row.id + '" data-index="' + index + '" data-oprtype="del"></p>';
                                }
                            }
                        ]]

                    })
                }

                $("#win").window("close");
            }).delegate(".searchBtn", "click", function () {
                if (that.elems.optionType == "selectUnit") {
                    that.renderTableUnit();
                }

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
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                    //var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+10);
                    //return d1<=date && date<=d2;
                    return d1 <= date;
                }
            });
            $('#expireDate').datebox().datebox('calendar').calendar({
                validator: function (date) {
                    var now = new Date();
                    var d1 = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 1);
                    //var d2 = new Date(now.getFullYear(), now.getMonth(), now.getDate()+10);
                    //return d1<=date && date<=d2;
                    return d1 <= date;
                }
            });

        },
        //选择门店
        selectUnit:function(data){
            var that=this;
            that.elems.optionType="selectUnit";
            var top=$(document).scrollTop()+0;
            var left=$(window).width() - 1140>0 ? ($(window).width() - 1140)*0.5:80;
            $('#win').window({title:"选择门店",width:1140,height:630,top:top,left:left});
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_setUnitList');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $("#win").window("open");
            //组织层级
            that.loadData.getUnitClassify(function(data) {
                if(!(data&&data.length>0)){
                    data=[];
                }
                debugger;
                $('#unitParentTree').tree({
                    id: 'id',
                    text: 'text',
                    data:data,
                    onClick:function(node) {
                        that.renderTableUnit();
                    }
                });
            });
            $('#unitTreeSelect').tree({
                id: 'id',
                text: 'text',
                checkbox:true,
                formatter:function(node){
                    debugger;
                    var s = node.text;
                    if (node.children){
                       // s += '&nbsp;<span style=\'color:blue\'>(' + node.children.length + ')</span>';
                    }else{
                         s+='<em class="delete" title="删除" data-target='+node.id+'></em>'
                    }
                    return s;
                },
                data:[]

            });
            $("#unitTreeSelect").delegate(".delete","click", function (e) {
                var id=$(this).data("target");

                var nodeList=[] ;
                var isDel=true;
                    var node= $('#unitParentTree').tree("getSelected");
                   nodeList = $("#unitGrid").datagrid('getData').rows;

                if(nodeList.length>0){
                    for(var j=0;j<nodeList.length;j++){
                       if(nodeList[j].Id==id){
                           isDel=false;
                           $("#unitGrid").datagrid("uncheckRow",j);
                       }
                    }

                }
                if(isDel) {
                    that.reloadUnitData([{id: id}]);
                }


            });
            that.reloadUnitData();
        },
        //刷新选中门店数据
        reloadUnitData:function(delList){
            var that=this;
           var   selectDataUnit=that.selectDataUnit;

            if (selectDataUnit.length > 0&&delList&&delList.length>0) {
                var dataUnitlist=[];
                for (var i = 0; i < selectDataUnit.length; i++) {
                    for(var j=0;j<delList.length;j++) {
                        var delNode=delList[j]
                        if (delNode.id !== selectDataUnit[i].id) {
                            var children=[];
                            if(selectDataUnit[i].children&&selectDataUnit[i].children.length>0){
                                for (var K = 0; K < selectDataUnit[i].children.length; K++) {
                                    if (delNode.id !== selectDataUnit[i].children[K].id) {
                                        children.push(selectDataUnit[i].children[K])
                                    }
                                }
                            }
                            if(children.length>0) {
                                var nodeNew = {
                                    id: selectDataUnit[i].id,
                                    text: selectDataUnit[i].text,
                                    "children": children
                                };

                                dataUnitlist.push(nodeNew);
                            }
                        }
                    }

                }
                that.selectDataUnit=dataUnitlist;
                selectDataUnit=dataUnitlist;
            }



            $('#unitTreeSelect').tree("loadData",selectDataUnit);

        },


        //缓存选中门店的数据
        cacheUnitData:function(){
            debugger;
            var that=this;
            var isAdd = true,nodeList=[];


              var node= $('#unitParentTree').tree("getSelected");
               nodeList=$("#unitGrid").datagrid('getChecked');
              var nodeParent={id:node.id,text:node.text,"children":[]};
            var nodeObjList=[];
              if(nodeList.length>0){
                  for(var j=0;j<nodeList.length;j++){
                      var obj={};
                      obj["id"]=nodeList[j].Id;
                      obj["text"]=nodeList[j].Name;
                      nodeObjList.push(obj)
                  }
                  nodeParent["children"]=nodeObjList;
              }
              var dataUnitlist=[],  selectDataUnit=that.selectDataUnit;

              if (selectDataUnit.length > 0) {
                  for (var i = 0; i < selectDataUnit.length; i++) {
                      if (nodeParent.id!== selectDataUnit[i].id) {
                          var nodeNew={id:selectDataUnit[i].id,text:selectDataUnit[i].text,"children":selectDataUnit[i].children};
                          dataUnitlist.push(nodeNew);
                      }

                  }
              }
              if(nodeObjList.length>0){
                  dataUnitlist.push(nodeParent)
              }

            that.selectDataUnit=dataUnitlist;
            $('#unitTreeSelect').tree("loadData",dataUnitlist);
        },
         //绑定门店数据
        renderTableUnit:function(){
                 var that=this;
           var node= $('#unitParentTree').tree("getSelected");
            if(node){
                that.loadData.unitSearch.Parent_Unit_ID=node.id;
            } else{
                $.messager.alert("提示","请选择一个门店上级组织");
                return;
            }
            that.loadData.unitSearch.unit_name=$("#unit_name").val();

            that.loadData.getUnitList(function(data){
                var isSubMit=false;
                  $("#unitGrid").datagrid({
                      method : 'post',
                      iconCls : 'icon-list', //图标
                      singleSelect : false, //多选false 单选true
                      height : 430, //高度
                      fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                      striped : true, //奇偶行颜色不同
                      collapsible : true,//可折叠
                      scrollbarSize:18,
                      //数据来源
                      data:data.topics,
                      sortName : 'Id', //排序的列
                      /*sortOrder : 'desc', //倒序
                       remoteSort : true, // 服务器排序*/
                      idField : 'Id', //主键字段
                      /* pageNumber:1,*/

                      columns : [[
                          {field : 'Name',title : '店名',width:60,align:'left',resizable:false,
                              formatter:function(value ,row,index){
                                  return value;
                              }
                          },
                   /*       {field : 'Contact',title : '联系人',width:70,align:'left',resizable:false,
                              formatter:function(value ,row,index){
                                  return value;
                              }
                          },
                          {field : 'Telephone',title : '电话',width:60,resizable:false,align:'left',resizable:false,formatter:function(value ,row,index){
                              return value;
                          }
                          },
                          {field : 'Parent_Unit_Name',title : '上级组织',width:80,align:'left',resizable:false,
                              formatter:function(value,row,index){
                                  return value
                              }
                          },
                          {field : 'StoreType',title : '类型',width:60,align:'left',resizable:false,formatter:function(value ,row,index){
                              var staus;
                              switch (value){
                                  case "DirectStore": staus="直营店";break;

                                  case "NapaStores": staus= "加盟店"; break;
                              }
                              return staus;
                          }
                          },
                          {field : 'Status',title : '状态',width:60,align:'left',resizable:false,
                              formatter:function(value ,row,index){
                                  var staus;
                                  switch (value){
                                      case "1": staus="正常";break;

                                      case "-1": staus= "停用"; break;
                                  }
                                  return staus;
                              }
                          },*/

                          {
                              field : 'ck',
                              width:20,
                              title:'全选',
                              align:'center',
                              checkbox : true
                          }


                      ]],
                    /*  frozenColumns:[[
                          {
                              field : 'ck',
                              width:70,
                              title:'全选',
                              align:'center',
                              checkbox : true
                          }
                      ]],*/
                      onLoadSuccess : function(data) {
                          debugger;

                          $("#unitGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                          isSubMit=true;
                      },

                      onCheck:function(){
                          var check= $("#unitGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                          if(check){
                              $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                          } else{
                              $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                          }
                          if(isSubMit) {
                              that.cacheUnitData();
                          }

                      } ,
                     onUncheck:function(){
                         var check= $("#unitGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                         if(check){
                             $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                         } else{
                             $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                         }
                         if(isSubMit) {
                             that.cacheUnitData();
                         }

                      } ,
                      onCheckAll:function(){
                          $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                          if(isSubMit) {
                              that.cacheUnitData();
                          }
                      } ,onUncheckAll:function(){
                          $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                          if(isSubMit) {
                              that.cacheUnitData();
                          }
                      }


                  });
            })

        },

        renderProductTable :function(){
            var that=this;
             var batId=$("#selectType").combobox("getValue");
            var node= $('#unitParentTree').tree("getSelected");
            if(node){
                that.loadData.productSearch.CategoryId=node.id;
            } else{
                if(batId==1) {
                    alert("请选择一个分类");

                }else{
                    alert("请选择一个分组");
                }
                return;
            }
            that.loadData.productSearch.ItemName=$("#ItemName").val();
            that.loadData.getProductList(function(data){
                debugger;
                var isSubMit=false;
                var prdouctList=[];
                if(data.Data&&data.Data.ItemSkuInfoList.length>0){
                    prdouctList= data.Data.ItemSkuInfoList;
                }
                $("#unitGrid").datagrid({
                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, //多选false 单选true
                    height : 430, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    scrollbarSize:18,
                    //数据来源
                    data:prdouctList,
                    sortName : 'Id', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    idField : 'Id', //主键字段
                    /* pageNumber:1,*/

                    columns : [[
                        {field : 'item_name',title : '商品名称',width:60,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },
                        {field : 'item_code',title : '商品名称',width:60,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                return value;
                            }
                        },

                        {
                            field : 'ck',
                            width:20,
                            title:'全选',
                            align:'center',
                            checkbox : true
                        }


                    ]],
                    /*  frozenColumns:[[
                     {
                     field : 'ck',
                     width:70,
                     title:'全选',
                     align:'center',
                     checkbox : true
                     }
                     ]],*/
                    onLoadSuccess : function(data) {
                        debugger;

                        $("#unitGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        isSubMit=true;
                    },

                    onCheck:function(){
                        var check= $("#unitGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        if(isSubMit) {
                            that.cacheUnitData();
                        }

                    } ,
                    onUncheck:function(){
                        var check= $("#unitGrid").datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        if(isSubMit) {
                            that.cacheUnitData();
                        }

                    } ,
                    onCheckAll:function(){
                        $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        if(isSubMit) {
                            that.cacheUnitData();
                        }
                    } ,onUncheckAll:function(){
                        $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        if(isSubMit) {
                            that.cacheUnitData();
                        }
                    }


                });
            })

        },
        selectProduct:function(data){
            var that=this;
            that.elems.optionType="selectUnit";
            var top=$(document).scrollTop()+60;
            var left=$(window).width() - 1140>0 ? ($(window).width() - 1140)*0.5:80;
            $('#win').window({title:"选择商品",width:1140,height:730,top:top,left:left});
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_setProduct');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $("#win").window("open");
            //组织层级
            that.loadData.getClassify(function(data) {
                if(!(data&&data.length>0)){
                    data=[];
                }
                debugger;
                $('#unitParentTree').tree({
                    id: 'id',
                    text: 'text',
                    data:data,
                    onClick:function(node) {
                        that.renderProductTable();
                    }
                });
            });
            $('#unitTreeSelect').tree({
                id: 'id',
                text: 'text',
                checkbox:true,
                formatter:function(node){
                    debugger;
                    var s = node.text;
                    if (node.children){
                        // s += '&nbsp;<span style=\'color:blue\'>(' + node.children.length + ')</span>';
                    }else{
                        s+='<em class="delete" title="删除" data-target='+node.id+'></em>'
                    }
                    return s;
                },
                data:[]

            });
            $("#unitTreeSelect").delegate(".delete","click", function (e) {
                var id=$(this).data("target");

                var nodeList=[]
                var isDel=true;
                var node= $('#unitParentTree').tree("getSelected");
                if(node){
                    nodeList= $('#unitParentTree').tree("getSelected");
                }
                if(nodeList.length>0){
                    for(var j=0;j<nodeList.length;j++){
                        if(nodeList[j].Id==id){
                            isDel=false;
                            $("#unitGrid").datagrid("uncheckRow",j);
                        }
                    }

                }
                if(isDel) {
                    that.reloadUnitData([{id: id}]);
                }


            });
            //that.reloadUnitData();
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

        //设置查询条件   取得动态的表单查询参数    如果值为0 传递空值
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
                limit:15,

            },
            tag:{
                VipId:"",
                orderID:''
            },
            unitSearch:{
                unit_name:'',//门店名称
                unit_status:'1',//int	否	状态（1:正常，０：失效）
                StoreType:'',//string	否	门店类型 直营店：DirectStore，加盟店：NapaStores
                Parent_Unit_ID:'',
                OnlyShop:'1' //int	是	只取门店（是：1,否：０）
            },
            productSearch :{
                ItemName:"",
                CategoryId:"",
                BatId:"1"
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
            getProductList:function(callback){
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                    data:{
                        action:"CreativityWarehouse.MarketingActivity.GetSkuInfoByCategoryOrName",
                        ItemName:this.productSearch.ItemName,
                        CategoryId:this.productSearch.CategoryId,
                        BatId:this.productSearch.BatId
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
            //获取门店等级
            getUnitClassify: function(callback){
                $.util.isLoading()
                $.util.ajax({
                    url: "/ApplicationInterface/Module/Basic/UnitAndType/UnitTypeTreeHandler.ashx",
                    data:{
                        hasShop:0
                    },
                    success: function(data){
                        if(data){
                            if(callback)
                                callback(data);
                        }
                        else{
                            alert("门店数据加载不成功");
                        }
                    }
                });
            },
            //只获取门店列表
            getUnitList: function (callback) {
                $.util.isLoading();
            debugger;
        $.util.oldAjax({
            url: "/module/basic/unit/Handler/UnitHandler.ashx",
            data:{
                action:'search_unit',
                page:1,
                //start:this.args.start,
                limit:100000,
                form:this.unitSearch
            },
            success: function (data) {
                if (data.topics) {
                    if (callback) {
                        callback(data);
                    }
                } else {
                    alert("加载门店列表数据不成功");
                }
            }
        });
    },
            //分类
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
                if($(".listBtn.show").data("coupontype")==1){  //是兑换券
                    prams.data["CouponCategory"]="GIFT";

                }else if($(".listBtn.show").data("coupontype")==0){  //是现金券
                    prams.data["CouponCategory"]="CASH";
                }
               /* var str= $("#applicationType").combobox("getValue");*/
                //适用门店(1=所有门店；2=部分门店/分销商;3=所有分销商)
                        if( $(".radio.on[data-name='unit']").data("valuetype")=="all"){
                            prams.data["SuitableForStore"]=1
                        }else{
                            prams.data["SuitableForStore"]=2;
                            prams.data.ObjectIDList=[];
                           var data= page.elems.listTable.datagrid("getData");
                           for(var i=0;i<data.rows.length; i++){

                                   prams.data.ObjectIDList.push({"ObjectID": data.rows[i].id});

                                  // prams.data.ObjectIDList.push({"ObjectID": data.rows[i].RetailTraderID});

                            }

                        }
                $.each(pram, function (index, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value;
                    }
                });
                if(prams.data["ParValue"]==="0.00"||prams.data["ParValue"]==="0"){
                   $.messager.alert("错误提示","优惠券面值必须大于零");
                    return false;
                }



                $.util.ajax({
                    url: prams.url,
                    data: prams.data,
                    beforeSend: function () {
                        $.util.isLoading();
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
            }


        },
		addGroupDialog:function(){
            var that=this;
            $('#win').window({title:"选择品类分组",width:550,height:480,top:50,left:($(window).width() - 550) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addProm');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
        },
		getGroupTree:function(roleId,defAppId){
			var that = this;
			$.util.oldAjax({
				url: "/Module/Basic/Role/Handler/RoleHandler.ashx",
				  data:{
					  action:'get_sys_menus_by_role_id',
					  role_id:roleId,
					  app_sys_id:defAppId
				  },
				  success: function (data) {
					if(data.totalCount) {
						var result = data.data;
						$("#selectGroupTreeBox").tree({
							//animate:true,
							//lines: true,
							checkbox: true,
							cascadeCheck: false,
							//valueField: 'id',
							//textField: 'text',
							data: result
						});
						
					}else{
						alert("加载数据不成功");
					}
				}
			});
		}
    };
    page.init();
});