/*define(['jquery', 'tools','js/tempModel.js','template', 'kindeditor','kkpager', 'artDialog'], function ($,temp) {*/
define(['jquery','js/tempModel.js','kindeditor', 'kkpager','easyui','template','tools', 'artDialog'], function ($, temp) {
    //上传图片
    KE = KindEditor;
    var page = {
        template:temp,
        elems: {
            sectionPage:$("#section"),
            navigation:$(".contentArea_vipquery .navigation"),
            submitBtn:$("#submitBtn"),
            panlH:200                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            ToolList:[
                {
                     DrawMethodId:"",
                    "InteractionType": 2,
                    "DrawMethodName": "团购",
                    "DrawMethodCode": "TG"
                },
                /*{

                 "InteractionType": 1,
                 "DrawMethodName": "问卷",
                 "DrawMethodCode": "QN"
                 },*/
                {
                    DrawMethodId:"",
                    "InteractionType": 1,
                    "DrawMethodName": "红包",
                    "DrawMethodCode": "HB"
                },
                /*  {

                 "InteractionType": 2,
                 "DrawMethodName": "热销",
                 "DrawMethodCode": "RX"
                 },*/
                {
                     DrawMethodId:"",
                    "InteractionType": 2,
                    "DrawMethodName": "抢购",
                    "DrawMethodCode": "QG"
                },
                /* {
                 "InteractionType": 1,
                 "DrawMethodName": "大转盘",
                 "DrawMethodCode": "DZP"
                 }*/
            ],
            tagList:[]                              //标签列表
        },
        init: function () {
            var self=this;
            this.initEvent();

            var templateId=$.util.getUrlParam("TemplateId");
             if(templateId){
                 self.loadData.args.TemplateId=templateId;
                 this.loadPageData();
             }else{
                 self.loadData.getMarketingTypeList(function(data) {
                     if (data.Data && data.Data.SysMarketingGroupTypeDropDownList && data.Data.SysMarketingGroupTypeDropDownList.length > 0) {

                         data.Data.SysMarketingGroupTypeDropDownList.push({
                             "ActivityGroupId": "-1",
                             "Name": "请选择",
                             "selected": true
                         });
                         $('#activityGroup').combobox({
                             panelHeight: self.elems.panlH,
                             valueField: 'ActivityGroupId',
                             textField: 'Name',
                             data: data.Data.SysMarketingGroupTypeDropDownList
                         });
                     }
                 });
             }


        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        },
        initEvent: function () {
            var that = this;
            //上传按钮初始化
          that.elems.sectionPage.find(".jsUploadBtn").each(function (i, e) {
              that.addUploadImgEvent(e);

            });
            that.elems.sectionPage.delegate(".spreadPanel .tabDiv","click",function(){
                $(this).siblings(".tabDiv").removeClass("on");
                $(this).addClass("on");
                var tabName=$(this).data("showtab");
                $('[data-tabname="'+tabName+'"]').siblings("[data-tabname]").hide();
                $('[data-tabname="'+tabName+'"]').show();


            });
            $(".nav02Table").delegate(".opt","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                $("#nav02Table").datagrid('selectRow', rowIndex);
                var row =  $("#nav02Table").datagrid('getSelected');
                if(optType=="exit"){
                    that.addStyle(row);
                } else {
                    var fileds=[{name:"ThemeId",value:row.ThemeId}];
                    that.loadData.operation(fileds, optType, function (data) {
                      alert("操作成功")
                        that.loadThemeList();
                    });
                }


            }) ;
            $(".nav03Table").delegate(".opt","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                $("#nav03Table").datagrid('selectRow', rowIndex);
                var row =  $("#nav03Table").datagrid('getSelected');
                if(optType=="exit"){
                    that.addActivity(row);
                } else {
                    var fileds=[{name:"InteractionId",value:row.InteractionId}];
                    that.loadData.operation(fileds, optType, function (data) {
                        alert("操作成功");
                        that.getLEventInteractionList();
                    });
                }


            }) ;
            that.elems.navigation.delegate("li","click",function(e){
                /*处理页面切换之间的提交后天逻辑*/

                var navLiOn=that.elems.navigation.find("li.on").data("showpanel"),  //当前停留的tab

                    panelName=$(this).data("showpanel");//当前点击的tab
                      var me=$(this);


                if(navLiOn=="nav01"&&panelName!="nav01"){
                      if($("#creative").form("validate")){
                          debugger;
                        var fileds=$("#creative").serializeArray();
                          var issubMit = true, ImageIdList = [];//
                          $("#creative").find("[data-imgcode].jsUploadBtn").each(function () {
                              var dom = $(this), name = $(this).data("imgcode"), value = "", msg = $(this).data("msg");
                              var obj = $(this).data("imgObj");

                              if (obj && obj.ImageID) {
                                  ImageIdList.push(obj.ImageID);//多张的 图片处理 一张图片去第一项
                              } else {
                                  $.messager.alert("提示", msg);
                                  issubMit = false;
                                  return false;
                              }


                          });
                          fileds.push({name: "ImageId", value: ImageIdList[0]});
                          if(issubMit) {
                              that.loadData.operation(fileds, "add", function (data) {
                                  that.loadData.args.TemplateId = data.Data.TemplateId;
                                  var navLi = that.elems.navigation.find("li");
                                  if (me.index() == navLi.length - 1) {
                                      that.elems.navigation.find("ul img").hide(0);
                                      that.elems.navigation.find(".hide").show(0);
                                      that.elems.submitBtn.html("提交");
                                      that.elems.submitBtn.data("issubMit", true);
                                      that.getSpread();
                                  } else {
                                      that.elems.navigation.find("ul img").show(0);
                                      that.elems.navigation.find(".hide").hide(0);
                                      that.elems.submitBtn.html("下一步");
                                      that.elems.submitBtn.data("issubMit", false);
                                      if(panelName=="nav02"){
                                          that.loadThemeList();
                                      }
                                      if(panelName=="nav03"){
                                          that.getLEventInteractionList();
                                      }
                                  }
                                  that.elems.navigation.find("li").removeClass("on");
                                  me.addClass("on");
                                  $("[data-panel].panelDiv").hide();
                                  $("[data-panel='"+panelName+"'].panelDiv").show();
                                  that.elems.submitBtn.data("flag", panelName);

                              })
                          }

                      }
                } else{
                    var navLi=that.elems.navigation.find("li");
                    if(me.index()==navLi.length-1){
                        that.elems.navigation.find("ul img").hide(0);
                        that.elems.navigation.find(".hide").show(0);
                        that.elems.submitBtn.html("提交");
                        that.elems.submitBtn.data("issubMit", true);
                        that.getSpread();
                    } else{
                        that.elems.navigation.find("ul img").show(0);
                        that.elems.navigation.find(".hide").hide(0);
                        that.elems.submitBtn.html("下一步");
                        that.elems.submitBtn.data("issubMit", false);
                        if(panelName=="nav02"){
                            that.loadThemeList();
                        }
                        if(panelName=="nav03"){
                            that.getLEventInteractionList();
                        }
                    }
                    that.elems.navigation.find("li").removeClass("on");
                    $(this).addClass("on");
                    $("[data-panel].panelDiv").hide();
                    $("[data-panel='"+panelName+"'].panelDiv").show();
                    that.elems.submitBtn.data("flag", panelName);

                }



            });
            that.elems.navigation.find("li").eq(0).trigger("click");
            that.elems.sectionPage.delegate("#submitBtn","click",function(e){
                debugger;
                var index=0;
                if($(this).data("issubMit")){

                  that.setSpread();


                }else{
                        var index=that.elems.navigation.find("li.on").index()+1;
                    that.elems.navigation.find("li").eq(index).trigger("click");
                }
                $.util.stopBubble(e);
            });
            $("[data-panel].panelDiv").delegate(".commonBtn","click",function(){
                     var type=$(this).data("flag");
                  switch (type){
                      case "style":
                          that.addStyle();
                          break;
                      case "activity":
                          that.addActivity();
                          break;
                  }


            });

            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=30;
             that.loadData.operation("","GetLEventList",function(data){

                 $.each(that.select.ToolList,function(index,filed){

                     $.each(data.Data.LEventDrawMethodInfoList,function(i,filedV){

                         if(filed.DrawMethodCode==filedV.DrawMethodCode){
                             that.select.ToolList[index].DrawMethodId=data.Data.LEventDrawMethodInfoList[i].DrawMethodId;
                         }

                     })

                 })

             });


            /**************** -------------------弹出easyui 控件  End****************/


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
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true,
                onOpen:function(){
                    $("#win").find(".jsUploadBtn").each(function (i, e) {
                        that.addUploadImgEvent(e);

                    });
                }
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".saveBtn","click",function(e) {
                if ($('#optionForm').form('validate')) {

                    var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象
                    var issubMit = true, ImageIdList = [];//;
                    if (that.elems.optionType == "addStyle") {

                        $('#win').find("[data-imgcode].jsUploadBtn").each(function () {
                            var dom = $(this), name = $(this).data("imgcode"), value = "", msg = $(this).data("msg");
                            var obj = $(this).data("imgObj");

                            if (obj && obj.ImageID) {
                                ImageIdList.push(obj.ImageID);//多张的 图片处理 一张图片去第一项
                            } else {
                                $.messager.alert("提示", msg);
                                issubMit = false;
                                return false;
                            }


                        });
                        fields.push({name: "ImageId", value: ImageIdList[0]});
                       /* fields.push({name: "ImageList", value: ImageIdList}); 多张图片的处理*/
                    }
                    if (issubMit) {
                        that.loadData.operation(fields, that.elems.optionType, function (data) {

                            alert("操作成功")
                            $('#win').window('close');
                            if (that.elems.optionType == "addStyle") {
                                that.loadThemeList();
                            }else if(that.elems.optionType == "addActivity"){
                                that.getLEventInteractionList();
                            }

                        });
                    }
                }
            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
     /*       that.elems.tabelWrap.delegate(".fontC","click",function(e){
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
        //风格表格加载
        loadThemeList:function(){
            this.loadData.getLEventThemeList(function(data){

                $("#nav02Table").datagrid({

                    method : 'post',
                    iconCls : 'icon-list', //图标
                    singleSelect : false, //多选
                    // height : 332, //高度
                    fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                    striped : true, //奇偶行颜色不同
                    collapsible : true,//可折叠
                    //数据来源
                    data:data.Data.LEventThemeList,
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
                        {field : 'ThemeName',title : '风格名称',width:80,resizable:false,align:'left'},
                        {field : 'ImageUrl',title : '图片',width:70,align:'left',resizable:false,
                            formatter:function(value ,row,index){
                                  var html="";
                                if(value){
                                    html=' <img src="'+value+'" width="40" height="40"  />'
                                }

                                return html;
                            }

                        },

                        {field: 'TemplateId', title: '操作', width: 100, align: 'left', resizable: false,
                            formatter: function (value, row, index) {
                                var str = "<div class='operation'>";
                                str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";
                                str += "<div data-index=" + index + " data-flag='delStyle' class='delete opt' title='删除'></div></div>";
                                return str
                            }
                        }
                    ]],



                });
            })

        },

        uploadImg: function (btn, callback) {
            setTimeout(function () {
                var uploadBtn = KE.uploadbutton({
                    width: "100%",
                    button: btn,
                    //上传的文件类型
                    fieldName: 'imgFile',
                    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                    url: '/ApplicationInterface/kindeditor/asp.net/upload_homepage_json.ashx?dir=image',
                    afterUpload: function (data) {
                        if (data.error === 0) {
                            if (callback) {
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
                uploadBtn.fileBox.change(function (e) {
                    uploadBtn.submit();
                });
            }, 10);

        },
        addUploadImgEvent: function (e) {
            var that=this;
            this.uploadImg(e, function (ele, data) {
                //上传成功后回写数据

                        //上传成功后回写数据

                        debugger;
                        that.loadData.args["ImageUrl"] = data.url;
                        var imgCode = $(ele).data("imgcode"), BatId = $(ele).data("batid");
                        // $(ele).data("imgObj").html('<img src="' + data.url + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                        var fileds = [{name: "imageUrl", value: data.url}];

                        var obj = $(ele).data("imgObj");
                        if(obj) {
                            $.each(obj, function (name, value) {
                                if (name != "imageUrl") {
                                    fileds.push({name: name, value: value});
                                }

                            });
                        }
                        var dom= $("[data-imgcode="+imgCode+"]");
                        if(dom.is("img")){
                            $("[data-imgcode="+imgCode+"]").attr("src",data.url);
                        } else if(!dom.hasClass(".jsUploadBtn")){
                            $("[data-imgcode="+imgCode+"]").css({"backgroundImage":'url('+data.url+')' });
                        }


                        that.loadData.args.bat_id = BatId;
                        that.loadData.operation(fileds, "setImg", function (data) {
                            debugger;
                            if(!obj){obj={}}
                            $.each(fileds, function (index, filed) {
                                if (filed.name != "imageUrl") {
                                    obj[filed.name] = filed.value;
                                }
                            });
                            obj["ImageID"] = data.Data.ImageId;
                            $(ele).data("imgObj", obj);
                        });


            });
        },



        //加载页面的数据请求
        loadPageData: function (e) {
            var that=this
            that.loadData.getMarketingTypeList(function(data) {
                if(data.Data&&data.Data.SysMarketingGroupTypeDropDownList&&data.Data.SysMarketingGroupTypeDropDownList.length>0) {

                    data.Data.SysMarketingGroupTypeDropDownList.push( {
                        "ActivityGroupId":"-1",
                        "Name":"请选择",
                        "selected":true
                    });
                    $('#activityGroup').combobox({
                        panelHeight:that.elems.panlH,
                        valueField: 'ActivityGroupId',
                        textField: 'Name',
                        data: data.Data.SysMarketingGroupTypeDropDownList
                    });
                }

                that.loadData.getLEventTemplateByID(function(data){
                    debugger;
                       $("#creative").form("load",data.Data.LEventTemplateInfo);

                    $("#creative").find("img[data-imgcode]").attr("src",data.Data.LEventTemplateInfo.ImageUrl);
                    var obj={ImageID:data.Data.LEventTemplateInfo.ImageId,ImageURL:data.Data.LEventTemplateInfo.ImageUrl};
                    $("#creative").find('[data-imgcode].jsUploadBtn').data("imgObj",obj);

                });

            });
        },

        //获取活动列表
        getLEventInteractionList: function () {
            debugger;
            var that=this;
          this.loadData.getLEventInteractionList(function(data){
              $("#nav03Table").datagrid({

                  method : 'post',
                  iconCls : 'icon-list', //图标
                  singleSelect : false, //多选
                  // height : 332, //高度
                  fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                  striped : true, //奇偶行颜色不同
                  collapsible : true,//可折叠
                  //数据来源
                  data:data.Data.LEventInteractionList,
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
                  frozenColumns:[[
                      {
                          field : 'ck',
                          title:'全选',
                          checkbox : true
                      }//显示复选框
                  ]],
                  columns : [[
                      {field : 'ThemeName',title : '风格名称',width:60,align:'left',resizable:false},
                      {field : 'InteractionType',title : '目标方案',width:60,align:'left',resizable:false,
                          formatter: function (value, row, index) {
                      var str=""
                              if(value==1){
                                  str="游戏";
                              }else if(value==2){
                                  str="促销"
                              }
                      return str
                  }
                      },

                      {field : 'DrawMethodName',title : '活动工具',width:60,align:'left',resizable:false},
                      {field : 'ActivityName',title : '活动名称',width:60,align:'left',resizable:false},
                      {field: 'InteractionId', title: '操作', width: 100, align: 'left', resizable: false,
                          formatter: function (value, row, index) {
                              var str = "<div class='operation'>";
                              str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";
                              str += "<div data-index=" + index + " data-flag='delActivity' class='delete opt' title='删除'></div></div>";
                              return str
                          }
                      }
                      ]]

              });





            //分页

            kkpager.generPageHtml({
                pno: page?page+1:1,
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


            /*if((that.loadData.opertionField.displayIndex||that.loadData.opertionField.displayIndex==0)){  //点击的行索引的  如果不存在表示不是显示详情的修改。
                that.elems.tabel.find("tr").eq(that.loadData.opertionField.displayIndex).find("td").trigger("click",true);
                that.loadData.opertionField.displayIndex=null;
            }*/
          });
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.start = (currentPage-1)*15;
            that.loadData.getCommodityList(function(data){
                that.renderTable(data);
            });
        },


        //添加风格
        addStyle:function(rowData){
            var that=this;
            that.elems.optionType="addStyle";
            var top=$(document).scrollTop()+60;
            var title="添加风格" ;
            if(rowData) {
                title = "编辑风格";
                that.loadData.args.ThemeId=rowData.ThemeId;
            }
            $('#win').window({title:title,width:760,height:600,top:top,left:($(window).width()-760)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=that.render(that.template.addStyle);
            var options = {
                region: 'center',
                content:html
            };
            $.parser.parse();
            $('#panlconent').layout('add',options);
            $("#optionForm").form("load",rowData);


            $('#win').window('open');
            if(rowData) {
                var obj = {imageUrl: rowData.ImageUrl, ImageID: rowData.ImageId};//logo
                $('#win').find('[data-imgcode="imgDefined"]').each(function () {
                    that.imgLoad($(this), obj);
                })
            }
        },

         imgLoad:function(me,obj){
        if(me.is("img")){
            me.attr("src",obj.imageUrl)
        }else if(me.hasClass("jsUploadBtn")){
            me.data("imgObj",obj);
        } else if(!me.hasClass("btnText")){
            me.css({"backgroundImage":'url('+obj.imageUrl+')' });
        }
    },
       //设置推广
        setSpread:function() {
            var that=this;
            var SpreadSettingList=[];
            var isSubmit=true;
          $(".setSpread").find('[data-tabname]').each(function(){     //3大tab遍历
              var type=$(this).data("type"),msg=$(this).data("msg"),obj={"SpreadType":type,id:$(this).data("spreadId")};

              $(this).find('input[type="text"],textarea,.jsUploadBtn[data-imgcode]').each(function(){
                  var name=$(this).attr("name"),value=$(this).val();
                 if(name) {
                     if (value) {
                         obj[name] = value;
                     } else {
                         if (name == "Title") {
                             $.messager.alert("提示", "请输入" + msg + "的标题")
                         }
                         if (name == "PromptText") {
                             $.messager.alert("提示", "请输入" + msg + "的提示文字")
                         }
                         if (name == "Summary") {
                             $.messager.alert("提示", "请输入" + msg + "的摘要")
                         }
                         isSubmit = false;
                         return false;
                     }
                 }else if($(this).hasClass("jsUploadBtn")){ //图片处理
                     debugger;
                     var imgObj=$(this).data("imgObj"),imgMsg=$(this).data("msg");
                     if(imgObj){
                         $.extend(obj,imgObj);
                     } else{
                         $.messager.alert("提示", "请上传一张" + imgMsg);
                         isSubmit = false;
                         return false;
                     }

                 }

              });
              if(type) {
                  SpreadSettingList.push(obj);
              }
               return isSubmit;
          });

            if(isSubmit) {
                var fileds=[{name:"SpreadSettingList",value:SpreadSettingList}];
                var type="setSpread";
                if(SpreadSettingList[0].id){
                    type="UpdateSpread";
                }
                that.loadData.operation(fileds, "setSpread", function () {
                    //获取推广设置
                      that.getSpread();
                    $.messager.confirm("提示", "该活动已经设置完成，确认是否继续", function (r) {
                        if (r) {

                        } else {
                            $.util.toNewUrlPath("queryList.aspx");
                        }

                    });
                });
            }

        },
        getSpread:function(){
            this.loadData.operation("", "getSpread", function (data) {
                //获取推广设置
                var SpreadSettingList=data.Data.SpreadSettingList;
                $('[data-tabname]').each(function(){     //3大tab遍历
                    var type=$(this).data("type"),msg=$(this).data("msg"),obj={},me= $(this);
                    $.each(data.Data.SpreadSettingList,function(index,filed){
                           if(filed.SpreadType==type){
                               obj=filed;
                               me.data("spreadId",filed.id);
                           }
                    });

                    $(this).find('input[type="text"],textarea,[data-view],.jsUploadBtn[data-imgcode]').each(function(){
                        var name=$(this).attr("name"),value=$(this).val();
                        switch ($(this).data("view")){

                            case "Title": $(this).html(obj["Title"]) ;alert("11"); break;
                            case "PromptText": $(this).html(obj["PromptText"]) ; break;
                            case "Summary": $(this).html(obj["Summary"]) ; break;

                        }
                        if(name) {
                                if (name == "Title") {
                                       $(this).val(obj["Title"]);
                                }else
                                if (name == "PromptText") {
                                    $(this).val(obj["PromptText"]);
                                }else
                                if (name == "Summary") {
                                    $(this).val(obj["Summary"]);
                                }

                        }else if($(this).hasClass("jsUploadBtn")){ //图片处理
                            debugger;
                            var imgObj={ImageID:obj.ImageId,ImageURL:obj.ImageUrl};
                           $(this).data("imgObj",imgObj);
                            var imgCode=$(this).data("imgcode");
                           $("[data-imgcode="+imgCode+"]").each(function(){
                               var dom=$(this);
                               if(dom.is("img")){
                                $("[data-imgcode="+imgCode+"]").attr("src",obj.ImageUrl);
                            } else if(!dom.hasClass(".jsUploadBtn")) {
                                $("[data-imgcode=" + imgCode + "]").css({"backgroundImage": 'url(' + obj.ImageUrl + ')'});
                            }

                           });


                        }

                    });


                });


            });
        },


        //添加风格
        addActivity:function(rowData){
            debugger;
            var that=this;
            that.elems.optionType="addActivity";
            var top=$(document).scrollTop()+60;
            var title="添加活动" ;
            if(rowData) {
                title = "编辑活动";
                that.loadData.args.InteractionId=rowData.InteractionId;
            }

            $('#win').window({title:title,width:760,height:300,top:top,left:($(window).width()-760)*0.5});
            //改变弹框内容，调用百度模板显示不同内容
            $('#panlconent').layout('remove','center');
            var html=that.render(that.template.addActivity);
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            that.loadData.getLEventThemeList(function(data){
                if(data.Data&&data.Data.LEventThemeList) {
                   data.Data.LEventThemeList.push({ThemeName:"请选择",ThemeId:"-1",selected:true});
                    var list=data.Data.LEventThemeList;
                    $("#style").combobox({
                        panelHeight:that.elems.panlH,
                        valueField: 'ThemeId',
                        textField: 'ThemeName',
                        data: list
                    }) ;
                    if(rowData) {
                        $("#style").combobox("select", rowData.ThemeId);
                    }
                }
            });

            $('#win').window('open');
            $("#InteractionType").combobox({
                panelHeight:that.elems.panlH,
                valueField: 'id',
                textField: 'text',
                data:[{id:"1",text:"游戏"},{id:"2",text:"促销"},{id:"-1",text:"请选择",selected:true}],
                onSelect:function(record){
                     var list=[];
                    $.each(that.select.ToolList,function(i,filed){
                       if(record.id==filed.InteractionType){
                           list.push(filed);
                       }
                    });
                    list.push({DrawMethodId:"-1",DrawMethodName:"请选择",selected:true});
                    $("#DrawMethodId").combobox({
                        panelHeight:that.elems.panlH,
                        valueField: 'DrawMethodId',
                        textField: 'DrawMethodName',
                        data: list,
                        onSelect:function(record){
                            var fileds=[{name:"EventCode",value:record.DrawMethodCode}];

                            that.loadData.operation(fileds,"select",function(data){
                                var eventList=[];
                                if(data.Data.PanicbuyingEventList&&data.Data.PanicbuyingEventList.length>0){
                                    eventList=data.Data.PanicbuyingEventList
                                }
                                eventList.push({EventId:"-1",EventName:"请选择",selected:true});
                              $("#LeventId").combobox({
                                  panelHeight:that.elems.panlH,
                                  valueField: 'EventId',
                                  textField: 'EventName',
                                  data: eventList,
                              });
                                if(rowData) {
                                    $("#LeventId").combobox("select", rowData.LeventId);
                                }
                            });

                        }
                    });
                }
            }) ;
            if(rowData) {
                $("#InteractionType").combobox("select", rowData.InteractionType);
                $("#DrawMethodId").combobox("select", rowData.DrawMethodId);
            }

        },
        loadData: {
            args: {
                TemplateId:"",
                InteractionId:"",//活动id
                ThemeId:"" ,  //风格id
                pageIndex:0,
                pageSize:10
            },

            getLEventTemplateByID: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:'GetLEventTemplateByID',
                        TemplateId:this.args.TemplateId

                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }
                    }
                });
            },
            getMarketingTypeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:"GetSysMarketingGroupTypeList"
                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }


                    }
                });
            },

            //获取活动
            getLEventInteractionList:function(callback){
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:'GetLEventInteractionList',
                        PageIndex:this.args.pageIndex,
                        PageSize:this.args.pageSize,
                        TemplateId:this.args.TemplateId

                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }
                    }
                });
            },
            //获取风格
            getLEventThemeList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data:{
                        action:'GetLEventThemeList',
                        PageIndex:0,
                        PageSize:10,
                        TemplateId:this.args.TemplateId

                    },
                    success: function (data) {
                        if (data.IsSuccess&&data.ResultCode==0) {
                            if (callback)
                                callback(data);
                        }else{
                            alert(data.Message)
                        }
                    }
                });

            },

            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx";
                //根据不同的操作 设置不懂请求路径和 方法
                prams.data["TemplateId"]=this.args.TemplateId;

                $.each(pram, function (index, field) {
                    if(field.value!=="") {
                        prams.data[field.name] = field.value;
                    }
                });
                switch(operationType){
                    case "add":prams.data.action="SetLEventTemplate"; break;  //设置主题
                    case  "addStyle":prams.data.action="SetLEventTheme";
                        prams.data["ThemeId"]=this.args.ThemeId;

                        break;//设置风格

                    case  "delStyle":prams.data.action="DeleteLEventTheme";break; //删除风格
                    case  "addActivity":prams.data.action="SetLEventInteraction";
                        prams.data["InteractionId"]=this.args.InteractionId;
                        break;//设置活动

                    case  "delActivity":prams.data.action="DeleteLEventInteraction";break;//删除 活动
                    case  "setImg":prams.data.action="SetObjectImages"; break;     //设置图片id
                    case "GetLEventList":prams.data.action="GetLEventDropDownList";break;   //获取活动工具
                    case "select":prams.data.action="GetMarketingList";  //获取活动
                        prams.data["PageIndex"]= 0;
                        prams.data["PageSize"]=1000000;
                        break;
                    case "setSpread":prams.data.action="CreateSpreadSetting";break;   //设置推广
                    case "UpdateSpread":prams.data.action="UpdateSpreadSetting";break;   //设置推广
                    case "getSpread":prams.data.action="GetSpreadSettingList";break;   //获取推广

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
                            $.messager.alert("提示",data.Message);
                        }
                    }
                });
            }


        }

    };
    page.init();
});

