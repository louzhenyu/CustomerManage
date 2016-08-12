define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            tabelWrap:$('#tableWrap'),
            tabel:$("#gridTable"),
            click:true,
            panlH:116                           // 下来框统一高度
        },

        init: function () {
            this.initEvent();
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询

      /*      that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getClassify(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });*/


            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=160,H=32;




            $('.datebox').datebox({
                width:wd,
                height:H
            });
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

                if ($('#addProm').form('validate')) {
                      debugger;
                    var fields = $('#addProm').serializeArray(); //自动序列化表单元素为JSON对象
                    that.loadData.operation(fields,that.elems.optionType,function(data){

                        alert("操作成功");
                        $('#win').window('close');
                        that.loadPageData(e);

                    });
                }
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".fontC","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("oprtype");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="del") {
                    $.messager.confirm('促销分组删除', '该促销分组关联{0}件商品，确定删除吗？'.format(row.PromotionItemCount), function(r){
                        if (r){
                            that.loadData.operation(row,optType,function(data){
                                alert("操作成功");
                            });
                            that.loadPageData(e);
                        }
                    });

                }
                if(optType=="exit"){
                    that.elems.optionType="exit";
                    that.update();
                    $('#addProm').form('load',{Item_Category_Name:row.text,Item_Category_Id:row.id});
                }

            }).delegate(".commonBtn","click",function(e){
                var type=$(this).data("flag");
                that.elems.optionType=type;
                that.update()

            });
            /**************** -------------------列表操作事件用例 End****************/
        },

        update:function(){
            var top=$(document).scrollTop()+60;

            $('#win').window({title:"商品营销分组",width:400,height:230,top:top,left:($(window).width()-400)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addProm');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');
        },


        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){

        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.getClassify(function(data){
                //写死的数据
                //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                //渲染table
                that.renderTable(data);


            });
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data){

                data=[];
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
                data:data,
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField : 'id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/

                columns : [[

                    {field : 'text',title : '分组名称',width:220,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=12;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },


                    {field : 'PromotionItemCount',title : '商品数',width:100,align:'center',resizable:false} ,
                    {field : 'create_time',title : '创建时间',width:100,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            return new Date(value).format("yyyy-MM-dd");
                        }
                    },
                    {field : 'id',title : '编辑',width:30,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC opt exit" data-index="'+index+'" data-oprtype="exit"></p>';
                        }
                    },
                    {field : 'isParent',title : '删除',width:30,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC opt delete" data-index="'+index+'" data-oprtype="del"></p>';
                        }
                    }
                ]],

                onLoadSuccess : function() {
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题

                },
                onClickRow:function(rowindex,rowData){
                    /*  debugger;
                     if(that.elems.click){
                     that.elems.click = true;
                     debugger;
                     var index=rowindex, nodeData=rowData;
                     var mid = JITMethod.getUrlParam("mid");
                     location.href = "CarOrderDetail.aspx?OrderID=" + rowData.OrderID + "&vipId=" + rowData.VipID+"&UnitID="+rowData.UnitID + "&mid=" + mid;
                     }*/

                },onClickCell:function(rowIndex, field, value){
                    /*  if(field=="OrderID"){
                     that.elems.click=false;
                     }else{
                     that.elems.click=true;
                     }*/
                }

            });



            //分页
            data.Data={};
            data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
           if(data.Data.TotalPageCount&&data.Data.TotalPageCount>1) {
               kkpager.generPageHtml({
                   pno: that.loadData.args.start ? that.loadData.args.start + 1 : 1,
                   mode: 'click', //设置为click模式
                   //总页码
                   total: data.Data.TotalPageCount,
                   totalRecords: data.totalCount,
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

           }
            if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.start = currentPage-1;
            that.loadData.getClassify(function(data){
                that.renderTable(data);
            });
        },


        loadData: {
            args: {
                bat_id:"2",
                PageIndex: 0,
                PageSize: 6,
                Status:1,
                page:1,
                start:0,
                limit:15
            },
            opertionField:{},


            getClassify: function (callback) {
                $.util.oldAjax({
                    url: "/module/basic/ItemCategoryNew/Handler/ItemCategoryTreeHandler.ashx",
                    data:{
                          node:"root",
                       isAddPleaseSelectItem:true,
                        pleaseSelectText:"请选择",
                        pleaseSelectID:"0",
                        bat_id:this.args.bat_id,
                        Status:this.args.Status
                    },
                    success: function (data) {
                        if (data) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("加载异常请联系管理员");
                        }
                    }
                });
            },
            operation:function(pram,operationType,callback){
                debugger;
              var  isJson=true;
                var prams={data:{action:""}};
                prams.url="/module/basic/ItemCategoryNew/Handler/ItemCategoryHandler.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                  if(pram.length){
                      $.each(pram,function(i,filed){
                          if(filed.value) {
                              prams.data[filed.name] = filed.value;
                          }
                            if(filed.name=="Item_Category_Name"){
                                prams.data.Item_Category_Code=filed.value;
                            }

                      })

                  }

                switch(operationType){
                    case "del":prams.data.action="toggleStatus";  //上架
                        isJson=false;
                        prams.data.status=pram.Status==1?"-1":"1";
                        prams.data.id=pram.id;
                        break;
                    case "exit": //修改
                       // prams.data.Item_Category_Id=pram.id;
                        prams.data.Status=1;
                        prams.data.action="add";
                        break;
                    case "add":  //添加促销分组
                        prams.data.action="add";
                        prams.data.Status=1;
                        break;
                }

                prams.data.Parent_Id="-99";
                prams.data.bat_id="2";
                $.util.isLoading();
                $.util.oldAjax({
                    url: prams.url,
                    data:prams.data,
                    isJSON:isJson,
                    success: function (data) {
                        if (data.success) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            $.messager.alert("提示",data.msg);
                        }
                    }
                });
            }


        }

    };
    page.init();
});

