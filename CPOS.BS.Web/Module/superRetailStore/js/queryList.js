define(['jquery', 'template', 'tools','easyui', 'kkpager', 'artDialog'], function ($) {
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
            optionBtn:$('.optionBtn'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            click1:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116, // 下来框统一高度
        },
        detailDate:{},

        ValueCard:'',//储值卡号
        classify:'',
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        selectProductList:[],
        init: function () {
            var that=this;
            that.loadData.getClassify(function(data){
                debugger;
                data[0]["checked"]=true;
                that.classify=data;


            });
            that.initEvent();
            that.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            //

            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.getSuperRetailTraderItemList(function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);

                });
                $.util.stopBubble(e);

            }).delegate(".tipInfo","mouseenter",function(){
                $(this).parent().find(".tipInfoText").show(function(){
                    $(this).css({
                        width: "388px",
                        height: "407px",
                        opacity: 1
                    })
                });
            }).delegate(".tipInfo","mouseleave",function(){

                $(this).parent().find(".tipInfoText").stop().hide(100);
            }).delegate(".tipInfoText","mouseenter",function(){
                $(this).stop().show(function(){
                    $(this).css({
                        width: "388px",
                        height: "407px",
                        opacity: 1
                    })
                });
            }).delegate(".tipInfoText","mouseleave",function(){
                  $(this).stop().hide(100);
            });

            that.elems.optionBtn.delegate(".commonBtn","click",function(e){
                var type=$(this).data("flag");
                if(that.elems.tabel.datagrid("getData").total==0&&type!="add"){
                    $.messager.confirm("提示","您还没有选择商品,确定选择商品吗",function(r){
                        if(r){
                            that.elems.optionBtn.find(".submit").hide();
                            $('[ data-flag="begEdit"]').show();
                            that.elems.tabel.datagrid("rejectChanges");
                            that.selectProduct();
                        }
                    });
                    return false;
                }
                if(type=="batch"){
                    that.elems["isHide"]=false;
                    if(that.elems.tabel.datagrid("getChecked").length>0){
                        $(this).find(".panelTab").show();
                    }else{
                        alert("至少选择一个分销商品的规格");
                    }
                }
                if(type=="begEdit"){
                    that.elems.optionBtn.find(".submit").show();
                    $(this).hide();
                  var data=that.elems.tabel.datagrid("getData");
                    for(var i=0;i<data.total;i++){
                       that.elems.tabel.datagrid("beginEdit",i);
                    }
                }
                if(type=="cancel") {   //取消
                    that.elems.optionBtn.find(".submit").hide();
                    $('[ data-flag="begEdit"]').show();
                    that.elems.tabel.datagrid("rejectChanges");
                }
                if(type=="save") {   //保存


                  /*  for (var index = 0; index < gridData.rows.length; index++) {
                        if(!that.elems.tabel.datagrid('validateRow',index)){
                            isSubmit= false;
                            errorIndex=index;
                            break;
                        }
                    }*/
                   /* for (var index = 0; index < gridData.rows.length; index++) {
                        that.elems.tabel.datagrid('beginEdit',index);
                    }*/

                    if($("#girdForm").form("validate")) {
                        that.elems.tabel.datagrid('acceptChanges');
                        var gridData = that.elems.tabel.datagrid('getData'); //去缓存的数据
                        var  isSubmit=true, errorIndex=-1;
                        var fileds=[{name:"ItemList",value:gridData.rows}];
                         that.loadData.operation(fileds,"savePice",function(){
                            alert("操作成功");
                             that.loadPageData();
                         });
                        that.elems.optionBtn.find(".submit").hide();
                        $('[ data-flag="begEdit"]').show();
                    }
                }
                if(type=="add"){ //选择商品
                    that.elems.optionBtn.find(".submit").hide();
                    $('[ data-flag="begEdit"]').show();
                    that.elems.tabel.datagrid("rejectChanges");
                    that.selectProduct();
                }

            }).delegate(".panelTab","mouseleave",function(){
                $(this).hide();
            }).delegate(".panelTab p","click",function(){
                var type=$(this).data("optiontype"),
                    fileds=[{name:"ItemIdList",value:""}],

                 rows=that.elems.tabel.datagrid("getSelections");
                 var list=[];
                var isSubmit=false;
                for(var i=0;i<rows.length;i++){
                    var obj={};
                    if(rows[i]["DistributerStock"]==0||rows[i]["DistributerCostPrice"]==0){
                        isSubmit=true
                    }
                    obj["ItemId"]=rows[i]["ItemId"];
                    obj["SkuId"]=rows[i]["SkuId"];

                   list.push(obj);
                }
                fileds[0].value=list;
                switch (type){
                    case "audit" :
                        fileds.push({name:"Status",value:10}); // 上架
                        if(isSubmit){
                            alert("您有商品还没有配置分销设置哦！");
                            return false;
                        }
                        break;
                    case "soldOut" :
                        fileds.push({name:"Status",value:90}); // 下架
                        break;
                    case "del" :
                        fileds.push({name:"Status",value:0}); // 删除
                        break;
                }
               that.loadData.operation(fileds,type,function(){
                   alert("操作成功");
                   that.loadPageData();
               });

            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=30;
            $('#item_status').combobox({
                width:wd,
                height:H,
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{
                    "id":100,
                    "text":"全部",
                    "selected":true
                },{
                    "id":10,
                    "text":"已上架",

                },{
                    "id":90,
                    "text":"已下架"

                }]
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
                closable:true,
                onClose:function(){
                    that.loadData.getSuperRetailTraderItemList(function(data){
                        that.renderTable(data);
                    });
                    that.selectProductList=[];
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e){
                $("#seach").form("clear");
                $('#item_status').combobox("setValue",100);
                    var treeList=$("#unitTreeSelect").tree("options").data;
                if (treeList.length>0) {
                    var list=[];
                    for(var i=0;i<treeList.length;i++){
                        if(treeList[i].children&&treeList[i].children.length>0){
                             for(var j=0;j<treeList[i].children.length;j++){
                                 var node= treeList[i].children[j];
                                 list.push({ItemId:node.ItemId,SkuId:node.SkuId})
                             }
                        } else{
                            list.push({ItemId:treeList[i].ItemId,SkuId:treeList[i].SkuId})
                        }
                    }
                     var fields=[{name:"ItemIdList",value:list}];
                    that.loadData.operation(fields,"setSuper",function(data){

                        alert("操作成功");
                        $('#win').window('close');
                        that.loadPageData(e);

                    });
                }else{
                    $.messager.alert("提示","请选择你要操作的商品");
                }
            }).delegate(".searchBtn","click",function(){
                     that.searchSelectProduct();
            }).delegate(".explain .optBtn","click",function(){
               var nodeList = $("#productGrid").datagrid('getData').rows;
                var nodes=$("#unitTreeSelect").tree("getChecked");
                var execute=true;
                if(nodes.length>0){
                    if(nodes.length>0){
                        for (j = 0; j < that.selectProductList.length;) {
                            var isRomove = false;


                            for (i = 0; i < nodes.length; i++) {
                                if (nodes[i].ParentId == that.selectProductList[j].ItemId && that.selectProductList[j].ParentId == -99) {  //找到规格属于哪个商品;

                                    for(var k=0;k<that.selectProductList[j].children.length;k++){   //清楚选中的子节点。防止多个sku删除某个无效
                                        if (nodes[i].SkuId == that.selectProductList[j].children[k].SkuId && that.selectProductList[j].children[k].ItemId == nodes[i].ItemId) {  //找到规格属于哪个商品;
                                            that.selectProductList[j].children.splice(k, 1);
                                            break;
                                        }
                                    }
                                }
                                if (that.selectProductList[j].SkuId == nodes[i].SkuId && that.selectProductList[j].ItemId == nodes[i].ItemId) {
                                    isRomove = true;
                                    break;
                                }
                            }
                            if (isRomove) {
                                that.selectProductList.splice(j,1);
                                j=0;
                            }else{
                                j++
                            }
                        }
                    }
                }


                if(nodeList.length>0&&nodes.length>0){
                    for(var i=0; i<nodes.length; i++) {

                            for (var j = 0; j < nodeList.length; j++) {
                                if (nodeList[j].SkuId == nodes[i].SkuId&&nodes[i].ParentId!=-99) {
                                    $("#productGrid").datagrid("unselectRow", j);
                                    execute=false;
                                }
                                if (nodeList[j].ItemId == nodes[i].ItemId&&nodes[i].ParentId==-99) {
                                    $("#productGrid").datagrid("unselectRow", j);
                                    execute=false;
                                }
                            }

                    }

                }
              if( execute){   //如果当前查询列表没有包含  任何一个要被批量删除的数据。需要调用一次  getAllSkuList方法
                  that.getAllSkuList();
              }


            });
            /**************** -------------------弹出窗口初始化 end****************/
          that.elems.tabelWrap.delegate(".opt",'click',function(){
             // that.elems.tabel.datagrid("clearSelections");
                var type=$(this).data("oprtype"),
                    //index=$(this).data("index"),
                       dataRow=$(this).data("json"),
              fileds=[{name:"ItemIdList",value:""}];
             // rows=that.elems.tabel.datagrid("getSelections");


              fileds[0].value= [{ItemId:dataRow["ItemId"],SkuId:dataRow["SkuId"]}];

              switch (type){
                  case "audit" :
                      fileds.push({name:"Status",value:10}); // 上架
                      if(dataRow["DistributerStock"]==0||dataRow["DistributerCostPrice"]==0){
                          alert("您有商品还没有配置分销设置哦！");
                          return false;
                      }
                      break;
                  case "soldOut" :
                      fileds.push({name:"Status",value:90}); // 下架
                      break;
                  case "del" :
                      fileds.push({name:"Status",value:0}); // 删除
                      break;
              }
              that.loadData.operation(fileds,type,function(){
                  alert("操作成功");
                  that.loadPageData();
              });
          });

        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            debugger;
            var that=this;
            //查询每次都是从第一页开始
            that.loadData.args.PageIndex=1;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                filed.value=filed.value=="0"?"":filed.value;
                that.loadData.seach[filed.name]=filed.value;
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
            if(!data.Data.SuperRetailTraderItemList){

                data.Data.SuperRetailTraderItemList=[];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.SuperRetailTraderItemList,
                sortName : 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
              //  idField : 'Item_Id', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                frozenColumns:[[
                    {
                        field : 'ck',
                        title:'全选',
                        checkbox : true,
                        width:80
                    }
                ]],
                columns : [[
                    {field : 'ItemName',title : '商品名称',width:80,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=40;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'PropName',title : '商品规格',width:30,align:'center',resizable:false } ,
                    {field : 'SalesPrice',title : '商城售价(元)',width:30,resizable:false,align:'center'},
                    {field : 'DistributerStock',title : '分销库存',width:30,align:'left',resizable:false,
                        formatter:function(value,row,index){
                           if(isNaN(Number(value))){
                             return 0;
                           }else{
                               return value;
                           }
                        },editor: {
                        type: 'numberbox',
                        options: {
                            min: 0,
                            max:999999,
                            required:true,
                            precision: 0,
                            height: 31,
                            width: 52,
                            validType:'nonzero'
                        }
                    }
                    },
                    {field : 'DistributerCostPrice',title : '成本价（元）',width:30,align:'left',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(Number())){
                                return 0.00;
                            }else{
                                return Number(value).toFixed(2);
                            }
                        }, editor: {
                        type: 'numberbox',
                        options: {
                            min: 0,
                            max:999999,
                            required:true,
                            precision: 2,
                            height: 31,
                            width: 52,
                            validType:'nonzero'
                        }
                    }
                    },
                    {field : 'DistributePirce',title : '分销售价（元）',width:30,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return value;
                            }
                        }},
                    {field : 'CustomerProgit',title : '商家利润（元）',width:30,align:'center',resizable:false,
                        formatter:function(value,row,index){
                            if(isNaN(parseInt(value))){
                                return 0;
                            }else{
                                return value;
                            }
                        }},

                    {field : 'Status',title : '状态',width:20,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            debugger;
                            var staus;
                            switch (value){
                                case 10: staus="已上架";break;

                                case 90: staus= "已下架"; break;
                            }
                            return staus;
                        }
                    },

					{
                        field: 'ItemId', title: '操作', width:30, align: 'center', resizable: false,
                        formatter:function(value ,row,index){
                            var html='<div class="optBtnPanel">';

                            switch (row.Status){
                                case 90:  html+='<div class="fontC opt" data-json=\''+JSON.stringify(row)+'\' data-index="'+index+'" data-oprtype="audit">上架</div>';
                                    break;
                                case 10:  html+='<div class="fontC opt" data-json=\''+JSON.stringify(row)+'\' data-index="'+index+'" data-oprtype="soldOut">下架</div>';
                                    break;
                            }
                            html+='<div class="fontC opt" data-json=\''+JSON.stringify(row)+'\' data-index="'+index+'" data-oprtype="del">移除</div>';
                            return html+"</div>";
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
                    that.mergeCellsByField(that.elems.tabel, "ItemName");
                    that.elems.optionBtn.find(".submit").hide();
                    $('[ data-flag="begEdit"]').show();
                },
                onClickRow:function(rowindex,rowData){
                    /* debugger;
                     if(that.elems.click){
                     that.elems.click = true;
                     debugger;

                     //var mid = JITMethod.getUrlParam("mid");
                     var url = "commodityExit.aspx?Item_Id=" + rowData.Item_Id;
                         $.util.toNewUrlPath(url);
                     }*/

                },onClickCell:function(rowIndex, field, value){
                    if(field == "ck" || field == "Item_Id" || field =="customer_id") {    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                     that.elems.click=false;
                     }else{
                     that.elems.click=true;
                     }
                }

            });




        kkpager.generPageHtml({
            pagerid:"kkpager",
                pno: that.loadData.args.PageIndex,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPageCount,
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
        /*  mergeCellsByField()在DataGrid的onLoadSuccess中调用。
         * EasyUI DataGrid根据字段动态合并单元格
         * @param tableID 要合并table的id
         * @param colList 要合并的列,用逗号分隔(例如："name,department,office");*/
        //合并相同的值的属性行
        mergeCellsByField:function(table,colList){


            var ColArray = colList.split(",");
            var tTable =table ;
            var TableRowCnts=tTable.datagrid("getRows").length;
            var tmpA;
            var tmpB;
            var PerTxt = "";
            var CurTxt = "";
            var alertStr = "";
            //for (j=0;j<=ColArray.length-1 ;j++ )
            for (j=ColArray.length-1;j>=0 ;j-- )
            {
                //当循环至某新的列时，变量复位。
                PerTxt="";
                tmpA=1;
                tmpB=0;

                //从第一行（表头为第0行）开始循环，循环至行尾(溢出一位)
                for (i=0;i<=TableRowCnts ;i++ )
                {
                    if (i==TableRowCnts)
                    {
                        CurTxt="";
                    }
                    else
                    {
                        CurTxt=tTable.datagrid("getRows")[i][ColArray[j]];
                    }
                    if (PerTxt==CurTxt)
                    {
                        tmpA+=1;
                    }
                    else
                    {
                        tmpB+=tmpA;
                        tTable.datagrid('mergeCells',{
                            index:i-tmpA,
                            field:ColArray[j],
                            rowspan:tmpA,
                            colspan:null
                        });
                        tmpA=1;
                    }
                    PerTxt=CurTxt;
                }
            }
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.PageIndex = currentPage;
            that.loadData.getSuperRetailTraderItemList(function(data){
                that.renderTable(data);
            });
        },
        //选择商品
        selectProduct:function(data){
            var that=this;
            that.elems.optionType="selectUnit";
            var top=$(document).scrollTop()+60;
            var left=$(window).width() - 1140>0 ? ($(window).width() - 1140)*0.5:80;
            $('#win').window({title:"选择商品",width:1140,height:630,top:top,left:left});
            $('#panlconent').layout('remove','center');
            debugger;
            var html=bd.template('tpl_AddProductList');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            $("#win").window("open");
            that.loadData.win.PageIndex=1;
            $('#unitParentTree').tree({
                id: 'id',
                text: 'text',
                data:that.classify,
                onClick:function(node) {
                    that.searchSelectProduct();
                }
            });
            $('#unitTreeSelect').tree({
                id: 'id',
                text: 'text',
                checkbox:true,
                cascadeCheck:true,
                formatter:function(node){
                    debugger;
                    var value = node.text;
                    if (node.ParentId==-99){
                        var long=18;

                        if(value&&value.length>long){
                            value= '<div class="bg"  title="'+value+'">'+value.substring(0,long)+'...</div>'
                        }else{
                            value= '<div class="bg" >'+value+'</div>'
                        }
                    }else{
                        var obj=node; obj.ItemName="";
                        var nodeString=JSON.stringify(obj);

                        value+='<em class="delete" title="删除" data-target='+node.id+' data-json='+nodeString+'></em>';

                    }
                    return value;
                }, onLoadSuccess:function(){

                    $(".bg").parents(".tree-node").addClass("bg");
                    $("#unitTreeSelect").delegate(".delete","click", function (e) {
                        debugger;
                        var id = $(this).data("target");
                        var nodeList = [];
                        var isDel = true;
                        nodeList = $("#productGrid").datagrid('getData').rows;
                        var node = $(this).data("json");
                        // $("#unitTreeSelect").tree("remove",node.target);
                        if (nodeList.length > 0) {
                            for (var j = 0; j < nodeList.length; j++) {

                                if (nodeList[j].SkuId == id) {
                                    isDel = false;
                                    $("#productGrid").datagrid("uncheckRow", j);
                                }
                            }

                        }
                        if (isDel) {
                            var conunt = 0, itemIndex = -1;
                            for (var i = 0; i < that.selectProductList.length;) {
                                if (node.ParentId == that.selectProductList[i].ParentId) {   //统计有多少个SKU;
                                    conunt++;
                                }
                                if (node.ParentId == that.selectProductList[i].ItemId && that.selectProductList[i].ParentId == -99) {  //找到规格属于哪个商品;
                                    itemIndex = i
                                   for(var j=0;j<that.selectProductList[i].children.length;j++){   //清楚选中的子节点。防止多个sku删除某个无效
                                       if (node.SkuId == that.selectProductList[i].children[j].SkuId && that.selectProductList[i].children[j].ItemId == node.ItemId) {  //找到规格属于哪个商品;
                                           that.selectProductList[i].children.splice(j, 1);
                                         break;
                                       }
                                   }
                                }
                                if (node.SkuId == that.selectProductList[i].SkuId && that.selectProductList[i].ItemId == node.ItemId) {  //找到规格属于哪个商品;
                                    that.selectProductList.splice(i, 1);
                                    i = 0
                                } else {
                                    i++;
                                }

                            }
                            if (conunt == 1) {    //如果值选中了一个商品的一个规格，直接移除商品
                                that.selectProductList.splice(itemIndex, 1);
                            }
                            that.getAllSkuList();
                        }
                    });
                },
                data:[]

            });


            that.searchSelectProduct();
        },

        searchSelectProduct:function(){
            var fileds=[],that=this;
            that.isSubmit=false;
            fileds.push({name:"ItemName",value:$("#ItemName").val()});
            if($("#unitParentTree").tree("getSelected")) {
                fileds.push({name: "ItemCategoryId", value: $("#unitParentTree").tree("getSelected").id})
            }
            debugger;
            fileds.push({name: "PageIndex", value:that.loadData.win.PageIndex});
            fileds.push({name: "PageSize", value:that.loadData.win.PageSize});
            //jQuery easy datagrid  表格处理

            $.util.partialRefresh( $("#productGrid"));
            that.loadData.operation(fileds,"selectProduct",function(data){
               var list=[];
                if(data.Data&&data.Data.ItemList&&data.Data.ItemList.length>0){
                    list=that.formattedDataList(data.Data.ItemList);

                }
                var index=[];
                if(that.selectProductList.length>0&&list.length>0){
                        for(var i=0;i<list.length; i++) {
                            var isAdd = false;
                            for (var j = 0; j < that.selectProductList.length; j++) {
                                if (that.selectProductList[j].SkuId == list[i].SkuId&&that.selectProductList[j].ItemId == list[i].ItemId) {
                                    isAdd = true;
                                }
                            }
                            if(isAdd){
                                index.push(i);
                            }

                        }
                }
                $("#productGrid").datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, //多选false 单选true
                    height : 360, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    scrollbarSize:18,
                    //数据来源
                    data:list,
                    sortName : 'ItemId', //排序的列
                    /*sortOrder : 'desc', //倒序
                     remoteSort : true, // 服务器排序*/
                    //idField : 'ItemId', //主键字段
                    /*  pageNumber:1,*/
                    /* frozenColumns : [ [ {
                     field : 'brandLevelId',
                     checkbox : true
                     } //显示复选框
                     ] ],*/
                    frozenColumns:[[
                        {
                            field : 'ck',
                            checkbox : true,
                            width:80
                        }
                    ]],
                    columns : [[
                        {field : 'DisplayIndex',title : '序号',width:20,align:'center',resizable:false,
                            formatter:function(value ,row,index){

                                if(row.ParentId==-99){
                                    return value;
                                }else{
                                    return ''
                                }
                            }
                        },     {field : 'ItemCode',title : '商品编号',width:40,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=40;
                                if(row.ParentId==-99){
                                        return value
                                }else{
                                    return ''
                                }

                            }
                        },
                        {field : 'ItemName',title : '商品名称',width:80,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                var long=40;
                                if(row.ParentId==-99){
                                    if(value&&value.length>long){
                                        return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                                    }else{
                                        return '<div class="rowText">'+value+'</div>'
                                    }
                                }else{
                                    return ''
                                }

                            }
                        },
                        {field : 'PropName',title : '商品规格',width:30,align:'center',resizable:false,
                            formatter:function(value ,row,index){
                                var long=56;
                                if(row.ParentId!=-99){
                                    return value;
                                }else{
                                    return ''
                                }
                            }
                        } ,
                        {field : 'SalesPrice',title : '零售价(元)',width:30,resizable:false,align:'center',
                            formatter:function(value ,row,index){
                                if(row.ParentId!=-99||(row.ParentId==-99&&row.SkuId)){
                                    return value;
                                }else{
                                    return ''
                                }
                            }
                        }
                    ]],

                    onLoadSuccess : function(data) {
                        debugger;
                       $("#productGrid").datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                        if(data.rows.length>0) {
                            $(".dataMessage1").hide();
                        }else{
                            $(".dataMessage1").show();
                        }
                        that.isSubmit=true;
                        if(index.length>0){
                            $.each(index,function(obj,i){
                                $("#productGrid").datagrid('selectRow',i);
                            })
                        }

                    },
                    onSelectAll:function(rows){

                        var check= $(this).datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked;
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        that.getAllSkuList();
                    },
                    onUnselectAll:function(rows){
                        var check= $(this).datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked;
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        if(that.isSubmit) {
                            that.getAllSkuList(rows,false,true);
                        }
                    },
                    onSelect:function(rowindex,rowData){
                        var check= $(this).datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked;
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        if(that.elems.click1) {
                            that.getAllSkuList(rowData, true);
                        }
                    },
                    onUnselect:function(rowindex,rowData){
                        var check= $(this).datagrid("getPanel").find(".datagrid-header-check").find("input").get(0).checked;
                        if(check){
                            $(this).datagrid("getPanel").find(".datagrid-header-check").addClass("on");
                        } else{
                            $(this).datagrid("getPanel").find(".datagrid-header-check").removeClass("on");
                        }
                        if(that.elems.click1) {
                            that.getAllSkuList(rowData);
                        }


                    },
                    onClickRow:function(rowindex,rowData){
                        /* debugger;
                         if(that.elems.click){
                         that.elems.click = true;
                         debugger;

                         //var mid = JITMethod.getUrlParam("mid");
                         var url = "commodityExit.aspx?Item_Id=" + rowData.Item_Id;
                         $.util.toNewUrlPath(url);
                         }*/

                    },onClickCell:function(rowIndex, field, value){
                        if(field == "ck") {    //在每一列有操作 而点击行有跳转页面的操作  才使用该功能。 此处不注释 与注释都可以。
                            that.elems.click=true;
                        }
                    }

                });


                 setTimeout(function(){
                 $("#productGrid").parents(".datagrid-view").find(".datagrid-body").eq(1).css({width:"527px"})
                 },100);
                //分


                //$.extend
                kkpager.generPageHtml({
                    pagerid:"kkpager2",
                    pno:that.loadData.win.PageIndex,
                    mode: 'click', //设置为click模式
                    //总页码
                    total: data.Data.TotalPageCount,
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

                        that.loadMoreData1(n);
                    },
                    //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                    getHref: function (n) {
                        return '#';
                    }

                }, true);
            });


        },
        loadMoreData1:function(n){
            this.loadData.win.PageIndex =n;
           this.searchSelectProduct()
        },
        /*
        isSelect： 是选中操作还是取消操作
         isList: row 是一个obj还是一个数组对象
         row 操作行数据
        * */
        getAllSkuList:function(row,isSelect,isList) {
            var that = this;
            var data = $("#productGrid").datagrid("getData");
            var i = 0,j= 0;
             if(row&&!isList) {
                 var removeList=[] //被连带移除的的选中项；
                 if (row.ParentId == -99) {     //操作行是商品的处理
                     that.elems.click1 = false;
                     for (i = 0; i < data.total; i++) {
                         if (data.rows[i].ParentId == row.ItemId) {
                             if (isSelect) {
                                 $("#productGrid").datagrid("selectRow", i);
                             } else {
                                 $("#productGrid").datagrid("unselectRow", i);
                                  //当时移除的时候    that.selectProductList 缓存数据也需要移除
                                 removeList.push(data.rows[i]);

                             }
                         }
                     }
                     that.elems.click1 = true;
                 } else {  //点击规格的处理

                     that.elems.click1 = false;
                     var list = [];
                     var index = -1;
                     var skuCount = 0;
                     for (j = 0; j < data.total; j++) {
                         if (data.rows[j].ItemId == row.ParentId && data.rows[j].ParentId == -99) { //找到规格对应的商品
                             index = j;
                         }
                         if (data.rows[j].ParentId == row.ParentId) {
                             skuCount++;
                         }
                     }

                     var isSemiangle=false;  //是否显示半选状态
                     var dataAll = $("#productGrid").datagrid("getSelections");
                     for ( j = 0; j < dataAll.length; j++) {
                         if (dataAll[j].ParentId == row.ParentId) {
                             list.push(dataAll[j]);
                         }
                     }
                     if (isSelect) {  //如果是选中规格 操作要判断是否是全部选中

                         if (list.length == skuCount) {    //全部选中时候
                             $("#productGrid").datagrid("selectRow", index);

                         } else {
                             $("#productGrid").datagrid("unselectRow", index);
                             //当取消选中的时候    that.selectProductList 缓存数据也需要移除
                             if(skuCount>1){
                                 isSemiangle=true;
                             }
                             removeList.push(data.rows[index]);
                         }

                     } else {     //如果是取消选中规格 操作要判断是否是全部取消
                         $("#productGrid").datagrid("unselectRow", index);
                         removeList.push(data.rows[index]);
                         if(list.length>0){ //有一个选中的规格商品就半选状态
                             isSemiangle=true;
                         }
                     }
                     that.elems.click1 = true;
                     if(isSemiangle){
                         $('[datagrid-row-index="'+index+'"]').addClass("semiangle");
                     }else{
                         $('[datagrid-row-index="'+index+'"]').removeClass("semiangle");
                     }

                 }
                 if(removeList.length>0){
                     for (j = 0; j < that.selectProductList.length;) {
                         var isRomove = false;
                         for (i = 0; i < removeList.length; i++) {
                             if (that.selectProductList[j].SkuId == removeList[i].SkuId && that.selectProductList[j].ItemId == removeList[i].ItemId) {
                                 isRomove = true;
                                 break;
                             }
                         }

                         if (isRomove) {
                             that.selectProductList.splice(j,1);
                             console.log(111);
                             j=0;
                         }else{
                             j++
                         }
                     }
                 }
             }

            //生成选择的数据
            var allList=[],dataList= $("#productGrid").datagrid("getSelections");
            allList=dataList;
            for (j = 0; j < that.selectProductList.length; j++) {
                var isAdd = true;
                for (i = 0; i < dataList.length; i++) {
                    if (that.selectProductList[j].SkuId == dataList[i].SkuId && that.selectProductList[j].ItemId == dataList[i].ItemId) {
                        isAdd = false;
                    }
                }
                if (isAdd && row && that.selectProductList[j].SkuId == row.SkuId && that.selectProductList[j].ItemId == row.ItemId) {
                    isAdd = isSelect;
                }
                if (isAdd) {
                    allList.push(that.selectProductList[j]);
                }
            }
            if(isList) {     //批量取消操作时候
                     allList=that.selectProductList;
                     for (j = 0; j < allList.length;) {
                         var isDel = false;
                         for (i = 0; i < row.length; i++) {
                             if (allList[j].SkuId == row[i].SkuId && allList[j].ItemId == row[i].ItemId) {
                                 isDel = true;
                                 break;
                             }
                         }

                         if (isDel) {
                             allList.splice(j,1);
                             j=0;
                         }else{
                             j++
                         }
                     }


                 }

            debugger;
            var treeList =[];
            var skuList=[]; //保存sku对象
            if (allList && allList.length > 0) {
                $.each(allList, function () {
                    if(this.ParentId==-99){  //已选中的商品加入节点
                            this["id"] = this.ItemId;
                            this["text"] = this.ItemName;
                            treeList.push(this);
                    }else{
                        skuList.push(this);
                    }

                })
            }
          /*var selectAllSkuList=skuList;
           /!* if(treeList.length>0) {
                $.each(treeList, function (k) {        //给全选的商品添加子节点。
                    treeList[k]["children"] = [];
                    $.each(selectAllSkuList, function () {
                        var thisItem = this;
                        if (thisItem.ParentId != -99 && thisItem.ParentId == treeList[k].ItemId) {
                            skuList.splice($.inArray(thisItem,skuList),1); //清除已经找到父亲节点的sku；
                            treeList[k]["children"].push(thisItem);
                        }
                    });
                });
            }*!/*/
            if(skuList.length>0) {   //sku找父亲
                $.each(data.rows, function (index) {
                    var isAdd = false;
                    data.rows[index]["children"] = [];
                    $.each(skuList, function () {
                        var thisItem = this;
                        //是否增加节点
                        if (data.rows[index].ParentId == -99 && thisItem.ParentId == data.rows[index].ItemId) {  //找到的商品，就增加节点
                            isAdd = true;
                            thisItem["id"]=thisItem.SkuId;
                            thisItem["text"]=thisItem.PropName;
                            delete thisItem.children;
                            data.rows[index]["children"].push(thisItem);
                        }
                    });
                    $.each(treeList, function (k) {      //treeList 是否存； 如果存在就赋值
                        if(treeList[k].ParentId == -99 && treeList[k].ItemId == data.rows[index].ItemId){
                           /// treeList[k] =data.rows[index];
                            isAdd=false;
                        }
                    });
                    if (isAdd) {
                        data.rows[index]["id"]=  data.rows[index].ItemId;
                        data.rows[index]["text"]=  data.rows[index].ItemName;

                        treeList.push(data.rows[index]);
                    }
                });
                debugger;
            }

            $('#unitTreeSelect').tree({

                data:treeList

            });
            that.selectProductList=allList;
        },


        //格式化数据。
        formattedDataList:function(list){
            debugger;
            var dataList=[];
            var parentList=[],childrenList=[];
           if(list.length>0){
               $.each(list,function(){
                   if(this.ParentId==-99){
                       parentList.push(this);
                   } else{
                       childrenList.push(this);
                   }
               });
               if(parentList.length>0&&childrenList.length>0){
                   for(var i=0; i<parentList.length;i++){
                       var itemId=parentList[i].ItemId;
                       var nodeList=[]   ///找到规格数据
                        for(var j=0;j<childrenList.length;j++){
                            if(itemId==childrenList[j].ParentId){
                                  nodeList.push(childrenList[j]);
                            }
                        }
                       if(nodeList.length==1&&nodeList[0].PropName==""){
                           parentList[i].SalesPrice=nodeList[0].SalesPrice;
                           parentList[i].SkuId=nodeList[0].SkuId;
                           dataList.push(parentList[i])
                       }else{
                           dataList.push(parentList[i]);
                           if(nodeList.length>0){
                               $.each(nodeList,function(){
                                   dataList.push (this);
                               })
                           }
                       }



                   }


               }
           }
            return dataList;


        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex:1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:10
            },
            win:{
                PageIndex:1,
                PageSize:10
            },
            seach:{
                ItemName:null,
                Status:null,

            },
            opertionField:{},

            getSuperRetailTraderItemList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Gateway.ashx",
                      data:{
                          action:'SuperRetailTrader.Item.GetSuperRetailTraderItemList',
                          ItemName:this.seach.ItemName,
                          Status:this.seach.Status,
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
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/Gateway.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                $.each(pram, function (i, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                });

                switch(operationType){
                    case "audit" :  // 上架
                    case "soldOut":   //下架
                    case "del":  //移除
                        prams.data.action="SuperRetailTrader.Item.SetSuperRetailTraderItemStatus";
                        break;
                    case "selectProduct":prams.data.action="SuperRetailTrader.Item.GetItemList";  //选择商品的查询
                        break;
                    case "setSuper": prams.data.action="SuperRetailTrader.Item.SetSuperRetailTraderItem";break;  //设置商品
                    case "savePice": prams.data.action="SuperRetailTrader.Item.SetSuperRetailTraderItemInfo";break; //设置库存和分销价

                }
                if(operationType=="soldOut"||operationType=="del") {
                    var str="确认进行该操作吗？";
                    switch(operationType) {
                        case "del" :  // 删除
                             str="确定要移除分销商品吗？";
                            break;
                        case "soldOut":
                            str="确定要下架分销商品吗？";
                            break;
                            //下架
                    }
                    $.messager.confirm("提示",str,function(r){
                             if(r){
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

                    });

                } else{
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


        }

    };

    page.init();
});

