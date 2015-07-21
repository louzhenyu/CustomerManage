define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','kindeditor'], function ($) {
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt'),              //表头分类查询
            optionBtn:$(".optionBtn"),       //操作按钮父类集合
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:150                           // 下来框统一高度
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
           // this.loadPageData();
            this.elems.dataMessage.show();
            var  data={Data:{UserList:[],isFirst:true }};
            this.renderTable(data)
            $(".info").css({ width: $("#tableWrap").width()-20 + "px", height: "300px" });
            this.elems.editor = KE.create('.info', {
                allowFileManager: true,
                fileManagerJson: "/Framework/Javascript/Other/kindeditor/asp.net/file_manager_json.ashx", //打开图片空间路径
                uploadJson: "/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?width=600" //上传图片
            });
        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getUserList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            }).delegate("#submitBtn","click",function(){
                var pram={};
               
                that.loadData.getUserListAll(function(data){
                pram.NewsUserList=data.Data.UserList;
                if(pram.NewsUserList.length>0) {
                    if(that.elems.editor.html()){
                        if ($('#meassage').form('validate')) {

                            var fields = $('#meassage').serializeArray(); //自动序列化表单元素为JSON对象

                            pram.InnerGroupNewsInfo = fields;
                            that.loadData.operation(pram, that.elems.optionType, function (data) {

                                alert("操作成功");


                                var mid = JITMethod.getUrlParam("mid");
                                location.href="/module/massTexting/internalMessage.aspx?mid=" + mid ;
                            });


                        }
                    } else{
                        $.messager.alert("提示","发送消息不可为空");
                    }


                }else{
                    $.messager.alert("提示","当前条件查询的员工不存在，请重新确认查询条件");
                }
                });
            });




            var wd = 160, H = 32;
            that.loadData.getUnitByUser(function(data){
                debugger;
                var treeData=that.listToTree('-99','ParentUnitID','UnitID',data.Data.UnitList);
                $('#UnitList').combotree({
                    width: wd,
                    height: H,
                    panelHeight: that.elems.panlH,
                    lines:true,
                    valueField: 'id',
                    textField: 'text',
                    data: treeData.Data
                });

            });

            that.loadData.GetDeptList(function(data){

                $('#DeptID').combobox({
                    width:wd,
                    height:H,
                    required: true,
                    panelHeight:that.elems.panlH,
                    valueField:'DeptID',
                    textField:'DeptName',
                    data:data.Data.DeptList
                });
            });

            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
           /* $('#win').window({
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

                    var fields = $('#payOrder').serializeArray(); //自动序列化表单元素为JSON对象

                    that.loadData.operation(fields,that.elems.optionType,function(data){

                        alert("操作成功");
                        that.loadPageData();

                    });
                }
            });
            *//**************** -------------------弹出窗口初始化 end****************//*

            *//**************** -------------------列表操作事件用例 start****************//*
            that.elems.tabelWrap.delegate(".fontC","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="payment") {
                    if(row.IsPaid!=1&&row.Status!=10&&row.Status!=11) {
                        that.payMent(row);
                    }
                }
                if(optType=="cancel"){
                    that.cancelOrder(row);
                }
            })*/
            /**************** -------------------列表操作事件用例 End****************/
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.start=0;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){

                if(filed.value!=""&&filed.name=="UnitID") {
                     that.loadData.seach[filed.name] = filed.value;
                 }else{
                    that.loadData.seach[filed.name] = filed.value;
                }
            });
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.UserList){

                data.Data.UserList=[]
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                /* height : 232,*/ //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.UserList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/

                /*  pageNumber:1,*/

                columns : [[



                    {field : 'user_name',title : '会员名称',width:120,align:'center',resizable:false

                    },
                    {field : 'Phone',title : '手机号',width:120,align:'center',resizable:false

                    },
                    {field : 'UnitName',title : '所属门店',width:120,align:'center',resizable:false}

                /*    {field : 'create_time',title : '下单时间',width:120,align:'left',resizable:false,
                     formatter:function(value ,row,index){
                     return new Date(value).format("yyyy-MM-dd hh:mm");
                     }
                     },*/
                    /*{field : 'Field9',title : '确认收货日期',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            if(value) {
                                return new Date(value).format("yyyy-MM-dd hh:mm");
                            }
                        }
                    },*/

                   /* {field : 'modify_time',title : '订单状态更新时间',width:120,align:'left',resizable:false,
                     formatter:function(value ,row,index){
                     return new Date(value).format("yyyy-MM-dd hh:mm");
                     }
                     }*/

                ]],

                onLoadSuccess : function(datas) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if(datas.rows.length>0) {
                        that.elems.dataMessage.hide();
                    }else{
                         if(!data.Data.isFirst){
                             that.elems.dataMessage.html("没有符合查询条件的记录")  ;
                         }
                        that.elems.dataMessage.show();
                    }
                },
                onClickRow:function(rowindex,rowData){
                    debugger;
                    if(that.elems.click){
                        that.elems.click = true;
                        debugger;
                        var mid = JITMethod.getUrlParam("mid");
                        // location.href = "orderDetail.aspx?orderId=" + rowData.order_id +"&mid=" + mid;
                    }

                },onClickCell:function(rowIndex, field, value){
                    if(field=="ck"){    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                        that.elems.click=false;
                    }else{
                        that.elems.click=true;
                    }
                }

            });



            //分页
            that.loadData.args.PageSizeAll=data.Data.TotalCount;
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPages,
                totalRecords: data.Data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function (n) {
                    //这里可以做自已的处理
                    //...
                    //处理完后可以手动条用selectPage进行页码选中切换
                    this.selectPage(n);
                    //让  tbody的内容变成加载中的图标
                    //var table = $('table.dataTable');//that.tableMap[that.status];
                    //var length = table.find("thead th").length;
                    //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                    that.loadMoreData(n);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);


            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getUserList(function(data){
                that.renderTable(data);
            });
        },


        //取消订单
        cancelOrder:function(data){
            var that=this;
            that.elems.optionType="cancel";
            $('#win').window({title:"取消订单",width:360,height:260});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderCancel');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            this.loadData.tag.orderID=data.OrderID;
            $('#win').window('open');
        },
        //rootParentIdValue      表示根节点的父类id 值是多少 如门店组织结构为-99
        //parentIdName    表示父类id的节点名称是什么    如门店组织结构的名称  ParentUnitID
        //nodeIdName    表示列表对象主键的名称  如门店是UnitID
        // listData 为二维的树形结构列表
        listToTree:function(rootParentIdValue,parentIdName,nodeIdName,ListData){
            if(ListData instanceof Array&&ListData.length>0&&ListData[i][parentIdName]){
                //rootList 根节点集合  nodeList 子节点集合
                var rootList=[], nodeList=[],allChildren=[];
                // 分离出 根节点和子节点集合
                $.each(ListData,function(index,node){
                    node.id=node[nodeIdName];node.text=node.UnitName;
                        if(node[parentIdName]==rootParentIdValue){
                            rootList.push(node);
                        }else{
                            nodeList.push(node);
                        }
                });
                //递归增加子节点 rootNodeList根节点集合， //子节点集合
                function childrenNodeAdd(rootNodeList,childrenList){
                    //debugger;

                   if(childrenList.length>0){   //遍历根节点集合在 子节点中查找其自身的子节点并绑定
                       $.each(rootNodeList,function(index,rootNode){ //遍历所有子节点
                           rootNodeList[index]["children"]=[];
                           $.each(childrenList,function(childrenIndex,childrenNode){
                               //debugger;
                               console.log(childrenIndex);
                                 if(parentIdName in childrenNode&&rootNode[nodeIdName]==childrenNode[parentIdName]){     //跟节点的id 等于子节点的父类id
                                     rootNodeList[index]["children"].push(childrenNode);
                                     allChildren.push(node);// 缓存所有已经添加过的对象

                                 }else{
                                    /// debugger;
                                     var node=childrenNode;
                                 }
                           });
                           var childrenNodeList=[]

                           $.each(childrenList,function(childrenIndex,childrenNode) {
                             var  isAdd=true
                               $.each(allChildren,function(index,node) {
                                      if(node==childrenNode){
                                           isAdd=false
                                      }

                               });
                              if(isAdd){
                                  childrenNodeList.push(childrenNode);
                              }
                           });

                           childrenNodeAdd(rootNodeList[index]["children"],childrenNodeList);
                       })
                   }
                }
                debugger;
               if(nodeList.length>0&&rootList.length>0){      //如果没有子节点集合
                   childrenNodeAdd(rootList,nodeList);
                   return {isTree:false,Data:rootList,message:"转化完成"}
               }else if(rootList.length>0){
                   return {isTree:false,Data:ListData,message:"没有对应的子节点集合"}
               }else{
                   return {isTree:false,Data:ListData,message:"没有对应的父类节点集合"}
               }

            }else{
                return {isTree:false,Data:ListData,message:"格式不正确，无法转换"}
            }
        },


        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 6,
                PageSizeAll:1000000000,
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
                UnitID:window.UnitID, //门店id
                PhoneList:""


            },
            opertionField:{},
             //根据门店和手机号获取员工信息
            getUserList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/InnerGoupNews.ashx",
                      data:{
                          action:'GetUserList',
                          UnitID:this.seach.UnitID,
                          PhoneList:this.seach.PhoneList,
                          PageIndex:this.args.PageIndex,
                          PageSize:this.args.PageSize
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
            getUserListAll: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/InnerGoupNews.ashx",
                    data:{
                        action:'GetUserList',
                        UnitID:this.seach.UnitID,
                        PhoneList:this.seach.PhoneList,
                        PageIndex:this.args.PageIndex,
                        PageSize:this.args.PageSizeAll
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

            //获取门店信息接口
            getUnitByUser: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/InnerGoupNews.ashx",
                    data:{
                        action:'GetUnitByUser'
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
           //获取内部消息分组
            GetDeptList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/InnerGoupNews.ashx",
                    data:{
                        action:'GetDeptList'
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
            } ,
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"SaveInnerGroupNews",InnerGroupNewsInfo:{},NewsUserList:[]}};
                prams.url="/ApplicationInterface/Vip/InnerGoupNews.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data.ItemInfoList=[];
                $.each( pram.InnerGroupNewsInfo,function(i,filed){
                    if(filed.value) {
                        prams.data.InnerGroupNewsInfo[filed.name] = filed.value;
                    }
                    if(filed.name=="Text"){
                        prams.data.InnerGroupNewsInfo[filed.name] =page.elems.editor.html();
                    }
                });
                $.each( pram.NewsUserList,function(i,filed){

                    prams.data.NewsUserList.push({UserID:filed.user_id})

                });




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

