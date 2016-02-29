; (function ($) {
    var util={};
    //日期时间的格式化
    Date.prototype.format =function(format)
    {
        var o = {
            "M+" : this.getMonth()+1, //month
            "d+" : this.getDate(), //day
            "h+" : this.getHours(), //hour
            "m+" : this.getMinutes(), //minute
            "s+" : this.getSeconds(), //second
            "q+" : Math.floor((this.getMonth()+3)/3), //quarter
            "S" : this.getMilliseconds() //millisecond
        }
        if(/(y+)/.test(format)) format=format.replace(RegExp.$1,
            (this.getFullYear()+"").substr(4- RegExp.$1.length));
        for(var k in o)if(new RegExp("("+ k +")").test(format))
            format = format.replace(RegExp.$1,
                    RegExp.$1.length==1? o[k] :
                    ("00"+ o[k]).substr((""+ o[k]).length));
        return format;
    };
    /*///两种调用方式
     var template1="我是{0}，今年{1}了";
     var template2="我是{name}，今年{age}了";
     var result1=template1.format("loogn",22);
     var result2=template2.format({name:"loogn",age:22});
     /////两个结果都是"我是loogn，今年22了"*/
    String.prototype.format = function(args) {
        var result = this;
        if (arguments.length > 0) {
            if (arguments.length == 1 && typeof (args) == "object") {
                for (var key in args) {
                    if(args[key]!=undefined){
                        var reg = new RegExp("({" + key + "})", "g");
                        result = result.replace(reg, args[key]);
                    }
                }
            }
            else {
                for (var i = 0; i < arguments.length; i++) {
                    if (arguments[i] != undefined) {
                        var reg = new RegExp("({[" + i + "]})", "g");
                        result = result.replace(reg, arguments[i]);
                    }
                }
            }
        }
        return result;
    };

    //日期比较根据不同的diffType 返回不同的 值， 相差多少天， 时 ，分 ，秒
    util.GetDateDiff=function(startTime, endTime, diffType) {
//作为除数的数字
        var divNum = 1;
        switch (diffType) {
            case "second":
                divNum = 1000;
                break;
            case "minute":
                divNum = 1000 * 60;
                break;
            case "hour":
                divNum = 1000 * 3600;
                break;
            case "day":
                divNum = 1000 * 3600 * 24;
                break;
            default:
                break;
        }
        return  parseInt((new Date(startTime)-new Date(endTime))/ parseInt(divNum));
    };


    util.obj2list = function(obj){
        var list = [];
        for(var i in obj){
            list.push(obj[i]);
        }
        return list;
    };

    util.list2obj = function(list,key){
        var obj = {};
        for(var i=0;i<list.length;i++){
            var idata = list[i];
            obj[idata[key]] = idata;
        }
        return obj;
    };
    util.obj2list = function(obj){
        var list = [];
        for(var i in obj){
            list.push(obj[i]);
        }
        return list;
    };
    util.getUrlParam=function(key){
		var urlstr = window.location.href.split("?"),
            params = {};
        if (urlstr[1]) {

				var items = urlstr[1].split("&");

				for (var i = 0; i < items.length; i++) {

					var itemarr = items[i].split("=");

					params[itemarr[0]] = itemarr[1];
				}
			}
        return key?params[key]:params;

    }
    util.toUrlWithParam=function(toUrl,param){

		var value = "",itemarr = [],params;


		params = this.getUrlParam();

		if(param){

			var temps = param.split("&"),tempparam;

			for(var i=0;i<temps.length;i++){

				tempparam = temps[i].split('=');

				params[tempparam[0]] = tempparam[1];
			}
		}


		var paramslist = [];

		for(var key in params){

			paramslist.push(key + '=' + params[key]);
		}
		location.href= toUrl + "?" + paramslist.join("&");
	} ;
    //构建ajax
    util.buildAjaxParams=function(param){
			var _param = {
				type: "post",
				dataType: "json",
				url: "",
				data: null,
				beforeSend: function () {

				},
				success: null,
				error: function (XMLHttpRequest, textStatus, errorThrown){

				}
			};

			$.extend(_param,param);
        _param.success= function (data) {
            if (!data.IsSuccess && data.ResultCode == 500) {
                $.messager.alert("提示", data.Message,"error",function() {
                    location.href = "/default.aspx?method=LogOut";
                });

            } else {
                param.success(data);
            }


        };
			//var baseInfo = this.getBaseAjaxParam();

			var action = param.data.action,
				interfaceType = param.interfaceType||'Product',
				_req = {
					'Locale':null,
					'CustomerID':(param.customerId?param.customerId:null),
					'UserID':(param.userId?param.userId:null),
					'OpenID':null,
					'Token':null,
                    'ChannelID':(param.ChannelID?param.ChannelID:null),
					'Parameters':param.data,
                    'random':Math.random()
				};


			delete param.data.action;

			var _data = {
				'req':JSON.stringify(_req)
			};

			_param.data = _data;
            if(param.data.oldInface){
                _param.url = _param.url;
            }else {
                _param.url = _param.url + '?type=' + interfaceType + '&action=' + action;
            }
			return _param;
	};
    //最新的ajax封装
	util.ajax=function(param){
			var _param;

			if(param.url.indexOf('Gateway.ashx')!=-1){

				_param = util.buildAjaxParams(param);
			}else{

				_param = util.buildAjaxParams(param);
			}
			//_param.url =  _param.url;

			$.ajax(_param);
	};
    /*
        保存页面的参数
        @option
        {
            domFlag:""   //页面元素要保存的标记
            attrs:"",    //要保存的属性
            pageSize
        ]

    */
   util.oldBuildAjaxParams=function(param){
       debugger;
       var _param = {
           type: "post",
           dataType: "json",
           url: "",
           data: null,
           beforeSend: function () {

           },
           success: null,
           error: function (XMLHttpRequest, textStatus, errorThrown){

           }
       };
       $.extend(_param,param);
       var method = param.data.action;
       delete param.data.action;

       //_param.data = JSON.stringify(param.data);
      if(param.data.form) {
         /* var _data = {
              'form': JSON.stringify(param.data.form)
          };*/
          param.data.form=JSON.stringify(param.data.form);
          _param.data = param.data;
      }
      if(param.isJSON){  //是否需要格式data 参数
          _param.data = JSON.stringify(param.data);
      }

        param.url = _param.url;
       _param.url = _param.url + '?&method=' + method;
       if(param.data.QueryStringData){

         var query=param.data.QueryStringData;
           var querystring=""
           $.each(query,function(name,val){
               querystring+="&{0}={1}".format(name,val);
           });
          // console.log(querystring);
           _param.url += querystring;
       }

       return _param;


    };
    util.oldAjax=function(param){
       debugger;
        var _param;

        if(param.url.indexOf('Handler.ashx')!=-1||param.url.indexOf('Hander.ashx')!=-1){

            _param = util.oldBuildAjaxParams(param);
        }
        //_param.url =  _param.url;

        $.ajax(_param);

    };

    util.toNewUrlPath=function(urlPath){

            var childMenuID =this.getUrlParam("mid");
            var parentMenu_Id = this.getUrlParam("PMenuID");
            var MMenuID = this.getUrlParam("MMenuID");
            var result = urlPath.indexOf("?");
            var newUrl = result != -1 ? (urlPath + "&mid=" + childMenuID + "&PMenuID=" + parentMenu_Id + "&MMenuID=" + MMenuID) : (urlPath + "?mid=" + childMenuID + "&PMenuID=" + parentMenu_Id + "&MMenuID=" + MMenuID);
            location.href = newUrl;


    };
    util.setPageParam=function(option){
        var array=[];
        $("["+option.domFlag+"]").each(function(i,j){
            var $t=$(this);
            var obj={};
                obj.attrs=[];
            if(j.tagName=="INPUT"){
                obj.type="INPUT";
                obj.value=$t.val();
            }else{
                obj.type=j.tagName;
                obj.value=$t.text();
            }
            //取出来该元素的属性标识
            if(option.attrs&&option.attrs instanceof Array&&option.attrs.length){
                for(var k=0,klength=option.attrs.length;k<klength;k++){
                    var attrObj={};
                    attrObj.attr=option.attrs[k];   //attr属性
                    attrObj.value=$t.attr(attrObj.attr);//对应的attr属性的value
                    obj.attrs.push(attrObj);
                }          
            }
            
            array.push(obj);
        });
        option.arr=array;
        location.hash="_saveData_="+encodeURIComponent(JSON.stringify(option));
    };
    
    /*
        @param option   //参数注释
        {
            domFlag:selector,      //jquery选择器  
            trigger:[{
                obj:jqueryObj,                  //要触发的事件操作
                eventType:"click"               //触发的事件类型
            }],
            callback:function(){                //回调函数
            }
        }
    */
    util.setDomValue=function(option){
        var sear=location.hash;
        sear=decodeURIComponent(sear);
        var result=sear.replace("#_saveData_=","");
        try{
            result=JSON.parse(result);
            //进行还原数据
            if(option.domFlag==result.domFlag){
                $("["+option.domFlag+"]").each(function(i,j){
                    var $t=$(this);
                    //dom的数据还原
                    var jitem=result.arr[i];               //每个dom
                    //判断是否是input
                    if(jitem.type==="INPUT"){  //input   //数据还原
                        $t.val(jitem.value);
                    }else{
                        $t.text(jitem.value||"");
                    }
                    //dom  属性还原
                    for(var atr=0,atrlen=jitem.attrs.length;atr<atrlen;atr++){
                        var attrItem=jitem.attrs[atr];  //每个属性
                        $t.attr(attrItem["attr"],attrItem["value"]);
                    }           
                });
                //事件触发
                if(option.trigger&&option.trigger instanceof Array){
                    for(var ii=0,iilen=option.trigger.length;ii<iilen;ii++){
                        var iitem=option.trigger[ii]; 
                        $(iitem["obj"]).trigger(iitem.eventType);  //进行事件触发
                    }
                }
                //回调函数
                if(option.callback&&typeof option.callback=="function"){
                    option.callback(result);
                }
                return true;
            }
        }catch(ex){
            return false;
        }
    };
    //组织默认事件
    util.stopBubble=function (e) {
        if (e && e.stopPropagation) {
            //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        }
        else {
            //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;
        }
        if(e && e.stopPropagation){e.preventDefault();}
    };
    //模拟的选择事件
    util.selectEvent=function(selector){
        //点击空白区域让指定的内容隐藏
        var that = this;
        $("body").bind("click",function(e){
            var target  = $(e.target);
            if(target.closest(".selectList").length == 0){
                $(".selectList").hide();
            }
           if(target.closest(".ztree").length == 0){
                $(".ztree").hide();
           } 
        });
        //模拟下拉框的点击事件
        $(selector).delegate(".selectBox span", "click", function (e) {
            //获得当前元素jquery对象
            var $t=$(this);
            var selList=$t.parent().find(".selectList");
            //判断下拉列表是否是显示状态
            if(selList.is(":hidden")){
                $(".selectBox") .find(".selectList").hide();
                selList.show();
				$t.parent().css("position","relative");
            }else{
                selList.hide();
				$t.parent().css("position","");
            }
            util.stopBubble(e);
			
        }).delegate(".selectBox p", "click", function (e) {  //下拉列表的点击事件

            //获得当前元素jquery对象
            var $t=$(this);
            //获得选择内容的id
            var optionId = $t.attr("optionid");
            //改变显示的内容  及设置id
            $t.parent().parent().find(".text").html($t.html());
            $t.parent().parent().find(".text").attr("optionid", optionId);

            //统一值属性命名   edit by Willie Yan
            var valId = $t.attr("data-val");
            $t.parent().parent().find(".text").attr("data-val", valId);

            $t.parent().hide();

        }).delegate(".selectList","mouseleave",function(e){    //鼠标从下拉内容移出的事件
            $(this).hide();
            util.stopBubble(e);
        }).delegate(".selectList","mouseenter",function(e){    //鼠标从下拉内容 移入事件
            $(this).show();
            clearTimeout(util.the_timeout);
            util.stopBubble(e);
        }).delegate(".selectBox span","mouseleave",function(e){    //鼠标从下拉控件输入框移出的事件
            var selList=$(this).parent().find(".selectList");
            util.the_timeout = setTimeout(function(){
                selList.hide();
            },1600);
            util.stopBubble(e);
        });


    };
    $.util=util;
})(jQuery);