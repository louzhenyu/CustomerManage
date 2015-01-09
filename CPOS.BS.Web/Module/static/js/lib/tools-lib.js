; (function ($) {
    var util={};
    util.stopBubble=function (e) {
        if (e && e.stopPropagation) {
            //因此它支持W3C的stopPropagation()方法 
            e.stopPropagation();
        } else {
            //否则，我们需要使用IE的方式来取消事件冒泡 
            window.event.cancelBubble = true;
        }
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
    util.getUrlParam = function(key){
		var urlstr = window.location.href.split("?"),
            params = {};
        if (urlstr[1]) {
			
				var items = urlstr[1].split("&");
				
				for (i = 0; i < items.length; i++) {
				
					itemarr = items[i].split("=");
					
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
	}
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
			
			//var baseInfo = this.getBaseAjaxParam();
			
			var action = param.data.action,
				interfaceType = param.interfaceType||'Product',
				_req = {
					'Locale':null,
					'CustomerID':(param.customerId?param.customerId:null),
					'UserID':(param.userId?param.userId:null),
					'OpenID':null,
					'Token':null,
					'Parameters':param.data,
                    'random':Math.random()
				};

			delete param.data.action;

			var _data = {
				'req':JSON.stringify(_req)
			};
			
			_param.data = _data;

			_param.url = _param.url+'?type='+interfaceType+'&action='+action;

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
                    if(jitem.type=="INPUT"){  //input   //数据还原
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
        e.preventDefault();
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
            util.stopBubble(e);
        }).delegate(".selectList","mouseleave",function(e){    //鼠标从下拉内容移出的事件
            $(this).hide();
            that.stopBubble(e);
        });
    };
    $.util=util;
})(jQuery);