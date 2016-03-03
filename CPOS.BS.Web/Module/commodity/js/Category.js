define(['jquery', 'template', 'tools','langzh_CN','easyui', 'kkpager', 'artDialog','kindeditor'], function ($) {

    //上传图片
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage:$("#section"),
            tabelWrap:$('#tableWrap'),
            tabel:$("#gridTable"),
            tabel1:$("#gridTable1"),

            click:true,
            width:200,
            height:30,
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
            var  wd=200,H=30;

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

                if ($('#addFrom').form('validate')) {
                      debugger;
                    var fields = $('#addFrom').serializeArray(); //自动序列化表单元素为JSON对象
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
                debugger;
                var rowIndex=$(this).data("index");
                var optType=$(this).data("oprtype");
                that.elems.tabel.treegrid('select', rowIndex);
                var row = that.elems.tabel.treegrid('getSelected');
                 var rowParent=that.elems.tabel1.treegrid("getParent",row.id);
               //
               switch (optType){
                   case "shiftUp" :
                   case "shiftDown" :
                       that.sortUpdte(optType,rowParent,row);
                       break;
                   case "del" :
                       if(row.children&&row.children.length>0){
                           $.messager.alert("操作提示","请先删除子分类");
                           $(".messager-window").css({"top":"129px"});
                           return false;
                       }
                       $.messager.confirm('提示','您确定要删除该分类吗？',function(r){
                           if (r){
                               that.loadData.operation(row,optType,function(data){
                                   alert("操作成功");
                               });
                               that.loadPageData(e);

                           }
                       });
                       $(".messager-window").css({"top":"129px"});
                       break;
                   case "exit" :
                       that.elems.optionType="exit";
                       that.update(row);
                       $('#addFrom').form('load',{Item_Category_Name:row.text,Item_Category_Id:row.id,Parent_Id:row.ParentID});
                       break;
                   case "addChildren" :
                       that.elems.optionType="addChildren";
                       row.ImageUrl="";
                       that.update(row);
                       $('#addFrom').form('load',{Parent_Id:row.id});
                       break;
               }


            }).delegate(".commonBtn","click",function(e){  //新增
                var type=$(this).data("flag");
                that.elems.optionType=type;
                that.update()

            });
            /**************** -------------------列表操作事件用例 End****************/
        },

        //排序操作
        sortUpdte:function(type,rowParent,row){

            debugger;
            var data=row,that=this,objList=[];

           objList.push({Item_Category_Id:row.id,DisplayIndex:row.DisplayIndex,obj:row});
           objList.push({Item_Category_Id:"",DisplayIndex:""});
            //遍历循环取出当前等级小的。
            if(rowParent&&rowParent.children&&rowParent.children.length>1) {
                $.each(rowParent.children, function (index, rowData) {

                    if (row.id != rowData.id) {
                        if(type=="shiftUp"&&row.DisplayIndex>=rowData.DisplayIndex){ //上移动
                            objList[1].DisplayIndex=rowData.DisplayIndex;
                            objList[1].Item_Category_Id=rowData.id;
                            objList[1].obj=rowData;
                            row.DisplayIndex=rowData.DisplayIndex;
                        }
                        if(type=="shiftDown"&&row.DisplayIndex<=rowData.DisplayIndex){//下移动
                            objList[1].DisplayIndex=rowData.DisplayIndex;
                            objList[1].Item_Category_Id=rowData.id;
                            objList[1].obj=rowData;
                            row.DisplayIndex=rowData.DisplayIndex;
                        }

                    }

                });
                if(objList[1].DisplayIndex!=="") {
                    var displayIndex = objList[1].DisplayIndex;
                    objList[1].DisplayIndex = objList[0].DisplayIndex;
                    objList[0].DisplayIndex = displayIndex;
                    that.loadData.args.CategoryList=objList;
                    //缓存需要排序的两个对象，然后进行交换
                    that.loadData.CategorySort(function(){
                        that.loadPageData();
                    });
                } else{
                    alert("移动无效");
                }
            }else{
                alert(" 唯一子节点移动无效");
            }


        },

        update:function(data){
            debugger;
            var that=this;
            $('#win').window({title:"商品分类",width:550,height:380,top:($(window).height() - 380) * 0.5,
                left:($(window).width() - 550) * 0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=bd.template('tpl_addFrom');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $('#win').window('open');

            that.registerUploadImgBtn();

                $('#Category').combotree({
                    width: that.elems.width,
                    height: that.elems.height,
                    panelHeight: that.elems.panlH,
                    lines:true,
                    valueField: 'id',
                    textField: 'text',
                    data: that.elems.categoryList
                });

            if(data) {
                $("#editLayer").find(".imgPanl").html("<img src='"+data.ImageUrl+"'>");

            }
        },

        //图片上传按钮绑定
        registerUploadImgBtn: function () {
            var self = this;
            debugger;
            // 注册上传按钮
            $("#editLayer").find(".uploadImgBtn").each(function (i, e) {
                self.addUploadImgEvent(e);
            });
        },

        //上传图片区域的各种事件绑定
        addUploadImgEvent: function (e) {
            var self = this;
            //上传图片并显示
            self.uploadImg(e, function (ele, data) {
                $(ele).parent().siblings(".imgPanl").html('<img src="'+data.url+'">');
                //$(ele).parent().siblings(".imgPanl").find("img").attr("src",data.url);

            });

        },
        //上传图片
        uploadImg: function (btn, callback) {
            debugger;
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width:300,
                //上传的文件类型
                fieldName: 'imgFile',
                isShow:true,
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Javascript/Other/kindeditor/asp.net/upload_homepage_json.ashx?dir=image&width=600',
                afterUpload: function (data) {
                    if (data.error === 0) {
                        if (callback) {
                            debugger  ;
                            callback(btn, data);
                        }
                        //取返回值,注意后台设置的key,如果要取原值
                        //取缩略图地址
                        //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                        //取原图地址
                        //var url = KE.formatUrl(data.url, 'absolute');
                    } else {
                        alert(data.message);
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
                that.elems.categoryList=data;

            });
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            var list=[]
            if(data[0].children){
                list=data[0].children

            }
            //jQuery easy treegrid  表格处理
            that.elems.tabel.treegrid({
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:list,
                rownumbers:true,
                idField:'id',
                treeField:'text',
                columns : [[
                    {field : 'text',title : '分类名称',width:120,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=12;
                            var html='';
                            if(value&&value.length>long){
                                html= '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div> '
                            }else{
                                html= '<div class="rowText">'+value+'</div>'
                            }
                            html+='<div class="shift"><div class="fontC  shiftUp" data-index="'+row.id+'" data-oprtype="shiftUp">&#8593; </div>' +
                                '<div class="fontC  shiftDown" data-index="'+row.id+'" data-oprtype="shiftDown">&#8595; </div>'+
                                '</div>';
                            return html
                        }
                    },
                    {field : 'ImageUrl',title : '图片',width:70,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            var html=' <img src="images/商品.png" width="40" height="40"  />';
                            if(value) {
                                html = ' <img src="' + value + '" width="40" height="40"  />'
                            }
                            return html;
                        }

                    },
                    {field : 'create_time',title : '子类添加',width:81,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC opt btnAdd" data-index="'+row.id+'" data-oprtype="addChildren"></p>';
                        }
                    },
                    {field : 'id',title : '编辑',width:81,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC opt exit" data-index="'+row.id+'" data-oprtype="exit"></p>';
                        }
                    },
                    {field : 'isParent',title : '删除',width:81,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            return '<p class="fontC opt delete" data-index="'+row.id+'" data-oprtype="del"></p>';
                        }
                    }
                ]],

                onLoadSuccess : function() {
                    //that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题

                }


            });

            that.elems.tabel1.treegrid({
                singleSelect : true, //单选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data,
                rownumbers:true,
                idField:'id',
                treeField:'text'
            });

            //分页
            data.Data={};
            data.Data.TotalPageCount = data.totalCount%that.loadData.args.limit==0? data.totalCount/that.loadData.args.limit: data.totalCount/that.loadData.args.limit +1;
            /*kkpager.generPageHtml({
                pno: that.loadData.args.start?that.loadData.args.start+1:1,
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

            }, true);*/



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
                bat_id:"1",
                PageIndex: 0,
                PageSize: 6,
                Status:1,
                page:1,
                start:0,
                limit:15,
               CategoryList:[]
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
                        status:this.args.Status
                    },
                    success: function (data) {
                        if (data) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert("加载异常请联系管理员");
                            console.log("分类信息加载失败")
                        }
                    }
                });
            },
            CategorySort: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Module/Item/ItemNewHandler.ashx",
                    data:{
                        action:"CategorySort",
                        CategoryList:this.args.CategoryList
                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }
                        else {
                            alert(data.Message);
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
                  prams.data.ImageUrl=$("#editLayer").find(".imgPanl img").attr("src");
                prams.data.Status=1;
                switch(operationType){
                    case "del":prams.data.action="toggleStatus";  //删除
                        isJson=false;
                        prams.data.Status=pram.Status==1?"-1":"1";
                        prams.data.id=pram.id;
                        break;
                    case "exit": //修改
                       // prams.data.Item_Category_Id=pram.id;
                        prams.data.action="update";
                        break;
                    case "add":  //添加
                        prams.data.action="add";

                        break;
                    case "addChildren":
                        prams.data.action="add";
                        break;
                }


                prams.data.bat_id="1";
                if(prams.data.Item_Category_Id&&prams.data.Parent_Id&& prams.data.Parent_Id==prams.data.Item_Category_Id){
                    $.messager.alert("提示","上级分类不可以指定为自己");
                    $(".messager-window").css({"top":"211px"});
                    return false;
                }
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
                            $.messager.alert("操作失败提示",data.msg);
                            $(".messager-window").css({"top":"211px"});
                        }
                    },error: function(data) {
                        $.messager.alert("操作失败提示",data.msg);
                        console.log("日志:"+operationType+"请求接口异常");
                        $(".messager-window").css({"top":"211px"});
                    }
                });
            }


        }

    };
    page.init();
});

