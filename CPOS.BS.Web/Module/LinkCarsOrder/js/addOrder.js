/**
 * Created by Administrator on 2015/4/8.
 */
define([ 'jquery', 'template', 'tools', 'kkpager','langzh_CN','easyui'], function ($) {
    var page = {

        elems: {
            allPage:$(".allPage"),
            change:true,
            dayList: $(".daylist") //服务时间列表
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
            this.setCondition();
        },
        returnTimeList:function(strTime,endTime,list){
            strTime=new Date(strTime).setMinutes(new Date(strTime).getMinutes()+30 );
            if(new Date(strTime)-new Date(endTime)>0) {
                return false;
            }else{
                list.push(new Date(strTime).format("hh:mm"));

            }
            this.returnTimeList(strTime,endTime,list);
        },

        phoneBindData:function(value){
            var that=this;
            this.setCondition();
            this.loadData.getVipList(function(data){
                   debugger;
                var  viplist=data;
                if(viplist.Data.VipTable){
                    $('#Phone').combogrid({
                        value:value,
                        data:viplist.Data.VipTable


                    });
                }  else{
                    $('#Phone').combogrid({
                        value:value
                    });
                    that.loadData.args.VipId="";
                    that.loadData.args.CarID="";
                }

            });
        },

        initEvent: function () {

            var that=this;
            $('#Phone').combogrid({
                panelWidth:410,
                panelHeight:200,
                idField:'VIPID',
                textField:'Phone',
                loadMsg:'正在加载数据....',
                //mode: 'remote',//远程检索服务器
                width:136,
                height:27,
                required:true,
                validType:'mobile',
                columns:[[
                    {field:'VipRealName',title:'姓名',width:100},
                    {field:'Gender',title:"性别",width:80},
                    {field:'Status',title:'会员类别',width:100},
                    {field:'Phone',title:'手机号',width:120}

                ]],onClickRow:function(rowIndex, rowData){
                    //$("#select").form("load",rowData);
                    $(".tooltip-right").hide(0);
                    $("#VipRealName").val(rowData.VipRealName);
                    that.loadData.args.VipId=rowData.VIPID;
                    //获取优惠券
                    that.loadData.GetCouponList(function(data){
                        if(data.Data&&data.Data.CouponList){
                            $("#coupon").combogrid({
                                data:data.Data.CouponList,
                                valueField:'CouponID',
                                textField:'CouponDesc',
                                panelWidth:260,
                                columns:[[
                                    {field:'CouponCode',title:'优惠券编码',width:100},
                                    {field:'CouponDesc',title:'优惠券名称',width:100},
                                    {field:'ParValue',title:'金额',width:50},
                                ]], onSelect:function(index,record){
                                },
                                onLoadSuccess:function(){

                                }
                            });
                        }
                    });
                    //获取汽车列表
                    that.loadData.GetCarMainInfoList(function(data){


                        that.loadData.args.CarID="";

                        var carList=data.Data.CarMainInfoList;
                        $.each(carList, function(i, field){
                                 carList[i].carNo=carList[i].CarNoPinyin+ carList[i].CarNoNum
                        });
                         debugger;
                        $("#carNo").combogrid({
                            data: carList,
                            panelWidth:210,
                            panelHeight:200,
                           // mode: 'remote',
                            required:true,validType:'carNO',
                            width:102,
                            idField:'CarMainInfoID',
                            textField:'carNo',//'CarNoPinyin'+'CarNoNum',
                            columns:[[
                                {field:'CarModelsName',title:'品牌',width:100},
                                {field:'CarNumber',title:"车牌号",width:100},

                            ]],
                            onLoadSuccess: function () {
                                debugger;
                               $(this).combogrid("setValue", carList[0].carNo);

                            },onClickRow:function(rowIndex, rowData){
                                that.loadData.args.CarID=rowData.CarMainInfoID;
                            }
                        });
                    });



                },onChange:function(newValue, oldValue){
                       if(newValue.length>0){
                          // $(".tooltip-right").hide(0);
                       }
                },
                onLoadSuccess:function(){

                }

            });
            that.elems.dayList.delegate("li","click",function(e){
                var me=$(this);
                $(".daylist").find("li").removeClass("on");
                me.addClass("on");
                $(".panlList .panel").hide(
                    function(){
                        var id=me.data("panl");
                        $("#"+id).show();
                    }
                );


            });
            $(".select").delegate("input.phone","blur",function(e){
                console.log($('#Phone').combogrid('getValue'));
                    that.phoneBindData($('#Phone').combogrid('getValue'));
                $(".tooltip-right").hide(0);
            });
            $(".panel").delegate(".time","click",function(e) {
                var me=$(this);
                $(".panel").find(".time").removeClass("on")
                me.addClass("on");

            });
            that.elems.allPage.delegate(".checkBox","click",function(e){
                var $t=$(this);
                $t.toggleClass("on");
                that.stopBubble(e);
            });
            that.elems.allPage.delegate(".saveBtn","click",function(e){
                   debugger;
                    var submit=true;
                    var param=[],obj={};
                    if ($('#select').form('validate')) {
                        param = $('#select').serializeArray(); //自动序列化表单元素为JSON对象
                    } else{
                        var submit=false;
                    }

                  //预约时间
                     if($(".panel").find(".on").length>0){
                         obj.name="ReserveTime";
                         obj.value=$(".panel").find(".on").data("time");
                         console.log( obj.value);
                         //param.ReserveTime=$(".panel").find(".on").val();//预约时间
                         param.push({name:obj.name,value:obj.value});
                         debugger;
                     }else{
                         submit=false;
                         alert("您必须预约一个时间");
                     }
                    var gridData=$("#coupon").combogrid("grid").datagrid('getSelected');
                debugger;
                    if(gridData){
                        obj.name="CouponID"; //优惠券
                        obj.value=gridData.CouponID;
                        param.push({name:obj.name,value:obj.value});
                        obj.name="DeductionAmount"; //抵扣金额
                        obj.value= gridData?gridData.ParValue:"0";
                        param.push({name:obj.name,value:obj.value});
                    }else{
                        obj.name="DeductionAmount";  //抵扣金额
                        obj.value= gridData?gridData.ParValue:"0";
                        param.push({name:obj.name,value:obj.value});
                    }
                   if(that.loadData.args.CarID){   //车辆标识
                       obj.name="CarID";
                       obj.value= that.loadData.args.CarID;
                       param.push({name:obj.name,value:obj.value});
                   }
                if(that.loadData.args.VipID){//会员ID
                    obj.name="VipID";
                    obj.value= that.loadData.args.VipID;
                    param.push({name:obj.name,value:obj.value});
                }
                if($("#ServiceItem").hasClass("on")){
                    obj.name="ServiceItemID";
                    obj.value= "1";
                    param.push({name:obj.name,value:obj.value});
                }

                //
                obj.name="Amount";
                obj.value=$('#Amount').numberbox('getValue');
                param.push({name:obj.name,value:obj.value});
                //备注

                obj.name="Remark";
                obj.value=$("#WaiterRemark").val() ;
                param.push({name:obj.name,value:obj.value});
                debugger;
             if(submit) {
                 that.loadData.operation(param, "预约", function (data) {
                     alert("下单成功");
                 })
             }
                that.stopBubble(e);
            });



            //车牌号相关事件处理
            $("#CarNoProvince").click(function(){
                $('#win').window('open');
            }).keyup(function(){
                $(this).val("");
            }).keydown(function() {
                $(this).val("");
            });
           //区域赋值
            $("#listSpan").delegate("span",'click',function(){
                $('#win').window('close');
                $("#CarNoProvince").val($(this).data("falg"));
            });

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
        //设置查询条件   取得会员列表的查询参数，
        setCondition:function(){
             var that=this;
            that.loadData.args.SearchColumns=[];

           var pram= $('#select').serializeArray();
             var  Col20Vluae="###K" ;//车牌号
            $.each(pram, function(i, field) {
                var obj={};
                obj.ColumnName=field.name;
                obj.ControlType=1;
                obj.ColumnValue1=field.value;
                if(field.name=="Phone"){
                    obj.ControlType=3;
                }
                if(field.name=="CarNoProvince"||field.name=="CarNoNum"){
                    obj.ColumnName="Col20";
                    if(Col20Vluae==="###K"){
                        Col20Vluae=pram[i].value;
                    }else{
                        Col20Vluae+=pram[i].value;
                        obj.ColumnValue1=Col20Vluae;
                        that.loadData.args.SearchColumns.push(obj);
                    }
                }else{
                    that.loadData.args.SearchColumns.push(obj);
                }

            });
        },

        //加载页面的数据请求
        loadPageData: function () {
            var that=this;
            that.elems.dayList.find("li").eq(0).trigger("click");


            that.loadData.GetReserveInfo(function(data){

                var  flowNum=data.Data.FlowStationNums, fixedNum=data.Data.ReserveStationNums;  //流动工位 ，固定工位
                //var H=200/(flowNum+fixedNum)+"px";
                if(!(data.Data.BookedTime&&data.Data.BookedTime.length>0)){
                    return
                }
                var  list=data.Data.BookedTime;
                for(var i=0;i<list.length;i++){

                    that.drawPanel("#nav0"+(i+1),list[i]);
                }

            });




        },
        drawPanel:function(id,objList){

            var list= $.merge(objList.Daytime,objList.Night);
            for(var j=0;j<list.length;j++){
                var  html="<div class='item'>";
                html+="<div class='time' data-time='"+objList.Day+" "+list[j].time +"'>"+list[j].time+"</div>";
                html+="</div>";
                $(id).append(html);
            }

        },


        //清空下拉框选项
        clearCombox:function(obj){
            obj.combobox("loadData","");
            obj.combobox("clear");
        },


        //通过文本赋值combox
        setTextCombox:function(id,text,type){
            debugger;
            id="#"+id;
            if(type==1) {
                for (var i = 0; i < $(id).combobox("getData").length; i++) {
                    if ($(id).combobox("getData")[i].Province == text) {
                        $(id).combobox("setValue", $(id).combobox("getData")[i].CityID);
                        return $(id).combobox("getData")[i].CityID;
                    }

                }
            }
            if(type==2) {
                debugger;
                for (var i = 0; i < $(id).combobox("getData").length; i++) {
                    if ($(id).combobox("getData")[i].CityName == text) {
                        $(id).combobox("setValue", $(id).combobox("getData")[i].CityID);
                        return $(id).combobox("getData")[i].CityID;
                    }

                }
            }

        },
        loadData: {
            args: {
                VipId: "", //会员Id
                PageIndex: 1,
                PageSize: 10,
                SearchColumns: [],    //查询的动态表单配置
                NewVipInfoColumns:[], //新增会员动态表单配置
                OrderBy:"CREATETIME",           //排序字段
                SortType: 'DESC',        //如果有提供OrderBy，SortType默认为'ASC'
                VipSearchTags:[]     //标签集合
            },

            //获取优惠券
            GetCouponList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Services/ServiceGateway.ashx",
                    pathType:"public",
                    userId: this.args.VipId,
                    data: {
                        action: "GetCouponList",

                        PageSize:1000,
                        PageIndex:this.args.PageIndex,
                        Status:0  //指定优惠卷状态（0：未用，1：已使用，2：已过期，3：全部）
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
            //会员详细信息修改
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:"SetAppointment"}};
                prams.url="/ApplicationInterface/car/OrderGateway.ashx";



                //根据不同的操作 设置不懂请求路径和 方法
             /*
                switch(operationType){
                    case "addCar":prams.data.action="SetCarMainInfo";  //添加车辆信息
                        break;

                }*/
                var  Col20Vluae="###K" ;//车牌号
                $.each(pram, function(i, field) {
                    if (field.value !== "") {
                        prams.data[field.name] = field.value; //提交的参数
                    }
                    if (field.name == "CarNoProvince" || field.name == "CarNoNum") {

                        if (Col20Vluae === "###K") {
                            Col20Vluae = pram[i].value;
                        } else {
                            Col20Vluae += pram[i].value;
                            prams.data["CarNumber"] = Col20Vluae;
                        }
                    }
                });

                prams.data["UnitID"]=window.UnitID;
                debugger;
                $.util.ajax({
                    url: prams.url,
                    pathType:"public",
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
            },
            //获得工位预订情况
            GetReserveInfo: function (callback) {
                window.UnitID="005c99f55c26916fbd305bd69e8948fd";
                $.util.ajax({
                    url: "/ApplicationInterface/car/OrderGateway.ashx",
                    pathType:"public",
                    data: {
                        action: "GetReserveInfo",
                        UnitID:window.UnitID     //"005c99f55c26916fbd305bd69e8948fd"//
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
            // 车辆列表信息
            GetCarMainInfoList:function(callback){
                $.util.ajax({
                    url:"/ApplicationInterface/Car/CarMainInfoGateway.ashx",
                    pathType:"public",
                    data:{
                        action:"GetCarMainInfoList",
                        VIPID:this.args.VipId
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            // 车辆列表信息
            GetCarMainInfoList:function(callback){
                $.util.ajax({
                    url:"/ApplicationInterface/Car/CarMainInfoGateway.ashx",
                    pathType:"public",
                    data:{
                        action:"GetCarMainInfoList",
                        VIPID:this.args.VipId
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            // //获得查询的会员数据
            getVipList: function (callback) {
                $.util.ajax({
                    url: "/ApplicationInterface/Vip/VipGateway.ashx",
                    data: {
                        action: "GetVipInfoList",
                        SearchColumns:this.args.SearchColumns,
                        PageSize:this.args.PageSize,
                        PageIndex:this.args.PageIndex,
                        OrderBy:this.args.OrderBy,
                        SortType: this.args.SortType,
                        VipSearchTags:this.args.VipSearchTags
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




        }

    };
    page.init();
});