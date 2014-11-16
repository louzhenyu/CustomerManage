/**
*
*  MD5 (Message-Digest Algorithm)
*  http://www.webtoolkit.info/
*
**/
 
var MD5 = function (string) {
 
	function RotateLeft(lValue, iShiftBits) {
		return (lValue<<iShiftBits) | (lValue>>>(32-iShiftBits));
	}
 
	function AddUnsigned(lX,lY) {
		var lX4,lY4,lX8,lY8,lResult;
		lX8 = (lX & 0x80000000);
		lY8 = (lY & 0x80000000);
		lX4 = (lX & 0x40000000);
		lY4 = (lY & 0x40000000);
		lResult = (lX & 0x3FFFFFFF)+(lY & 0x3FFFFFFF);
		if (lX4 & lY4) {
			return (lResult ^ 0x80000000 ^ lX8 ^ lY8);
		}
		if (lX4 | lY4) {
			if (lResult & 0x40000000) {
				return (lResult ^ 0xC0000000 ^ lX8 ^ lY8);
			} else {
				return (lResult ^ 0x40000000 ^ lX8 ^ lY8);
			}
		} else {
			return (lResult ^ lX8 ^ lY8);
		}
 	}
 
 	function F(x,y,z) { return (x & y) | ((~x) & z); }
 	function G(x,y,z) { return (x & z) | (y & (~z)); }
 	function H(x,y,z) { return (x ^ y ^ z); }
	function I(x,y,z) { return (y ^ (x | (~z))); }
 
	function FF(a,b,c,d,x,s,ac) {
		a = AddUnsigned(a, AddUnsigned(AddUnsigned(F(b, c, d), x), ac));
		return AddUnsigned(RotateLeft(a, s), b);
	};
 
	function GG(a,b,c,d,x,s,ac) {
		a = AddUnsigned(a, AddUnsigned(AddUnsigned(G(b, c, d), x), ac));
		return AddUnsigned(RotateLeft(a, s), b);
	};
 
	function HH(a,b,c,d,x,s,ac) {
		a = AddUnsigned(a, AddUnsigned(AddUnsigned(H(b, c, d), x), ac));
		return AddUnsigned(RotateLeft(a, s), b);
	};
 
	function II(a,b,c,d,x,s,ac) {
		a = AddUnsigned(a, AddUnsigned(AddUnsigned(I(b, c, d), x), ac));
		return AddUnsigned(RotateLeft(a, s), b);
	};
 
	function ConvertToWordArray(string) {
		var lWordCount;
		var lMessageLength = string.length;
		var lNumberOfWords_temp1=lMessageLength + 8;
		var lNumberOfWords_temp2=(lNumberOfWords_temp1-(lNumberOfWords_temp1 % 64))/64;
		var lNumberOfWords = (lNumberOfWords_temp2+1)*16;
		var lWordArray=Array(lNumberOfWords-1);
		var lBytePosition = 0;
		var lByteCount = 0;
		while ( lByteCount < lMessageLength ) {
			lWordCount = (lByteCount-(lByteCount % 4))/4;
			lBytePosition = (lByteCount % 4)*8;
			lWordArray[lWordCount] = (lWordArray[lWordCount] | (string.charCodeAt(lByteCount)<<lBytePosition));
			lByteCount++;
		}
		lWordCount = (lByteCount-(lByteCount % 4))/4;
		lBytePosition = (lByteCount % 4)*8;
		lWordArray[lWordCount] = lWordArray[lWordCount] | (0x80<<lBytePosition);
		lWordArray[lNumberOfWords-2] = lMessageLength<<3;
		lWordArray[lNumberOfWords-1] = lMessageLength>>>29;
		return lWordArray;
	};
 
	function WordToHex(lValue) {
		var WordToHexValue="",WordToHexValue_temp="",lByte,lCount;
		for (lCount = 0;lCount<=3;lCount++) {
			lByte = (lValue>>>(lCount*8)) & 255;
			WordToHexValue_temp = "0" + lByte.toString(16);
			WordToHexValue = WordToHexValue + WordToHexValue_temp.substr(WordToHexValue_temp.length-2,2);
		}
		return WordToHexValue;
	};
 
	function Utf8Encode(string) {
		string = string.replace(/\r\n/g,"\n");
		var utftext = "";
 
		for (var n = 0; n < string.length; n++) {
 
			var c = string.charCodeAt(n);
 
			if (c < 128) {
				utftext += String.fromCharCode(c);
			}
			else if((c > 127) && (c < 2048)) {
				utftext += String.fromCharCode((c >> 6) | 192);
				utftext += String.fromCharCode((c & 63) | 128);
			}
			else {
				utftext += String.fromCharCode((c >> 12) | 224);
				utftext += String.fromCharCode(((c >> 6) & 63) | 128);
				utftext += String.fromCharCode((c & 63) | 128);
			}
 
		}
 
		return utftext;
	};
 
	var x=Array();
	var k,AA,BB,CC,DD,a,b,c,d;
	var S11=7, S12=12, S13=17, S14=22;
	var S21=5, S22=9 , S23=14, S24=20;
	var S31=4, S32=11, S33=16, S34=23;
	var S41=6, S42=10, S43=15, S44=21;
 
	string = Utf8Encode(string);
 
	x = ConvertToWordArray(string);
 
	a = 0x67452301; b = 0xEFCDAB89; c = 0x98BADCFE; d = 0x10325476;
 
	for (k=0;k<x.length;k+=16) {
		AA=a; BB=b; CC=c; DD=d;
		a=FF(a,b,c,d,x[k+0], S11,0xD76AA478);
		d=FF(d,a,b,c,x[k+1], S12,0xE8C7B756);
		c=FF(c,d,a,b,x[k+2], S13,0x242070DB);
		b=FF(b,c,d,a,x[k+3], S14,0xC1BDCEEE);
		a=FF(a,b,c,d,x[k+4], S11,0xF57C0FAF);
		d=FF(d,a,b,c,x[k+5], S12,0x4787C62A);
		c=FF(c,d,a,b,x[k+6], S13,0xA8304613);
		b=FF(b,c,d,a,x[k+7], S14,0xFD469501);
		a=FF(a,b,c,d,x[k+8], S11,0x698098D8);
		d=FF(d,a,b,c,x[k+9], S12,0x8B44F7AF);
		c=FF(c,d,a,b,x[k+10],S13,0xFFFF5BB1);
		b=FF(b,c,d,a,x[k+11],S14,0x895CD7BE);
		a=FF(a,b,c,d,x[k+12],S11,0x6B901122);
		d=FF(d,a,b,c,x[k+13],S12,0xFD987193);
		c=FF(c,d,a,b,x[k+14],S13,0xA679438E);
		b=FF(b,c,d,a,x[k+15],S14,0x49B40821);
		a=GG(a,b,c,d,x[k+1], S21,0xF61E2562);
		d=GG(d,a,b,c,x[k+6], S22,0xC040B340);
		c=GG(c,d,a,b,x[k+11],S23,0x265E5A51);
		b=GG(b,c,d,a,x[k+0], S24,0xE9B6C7AA);
		a=GG(a,b,c,d,x[k+5], S21,0xD62F105D);
		d=GG(d,a,b,c,x[k+10],S22,0x2441453);
		c=GG(c,d,a,b,x[k+15],S23,0xD8A1E681);
		b=GG(b,c,d,a,x[k+4], S24,0xE7D3FBC8);
		a=GG(a,b,c,d,x[k+9], S21,0x21E1CDE6);
		d=GG(d,a,b,c,x[k+14],S22,0xC33707D6);
		c=GG(c,d,a,b,x[k+3], S23,0xF4D50D87);
		b=GG(b,c,d,a,x[k+8], S24,0x455A14ED);
		a=GG(a,b,c,d,x[k+13],S21,0xA9E3E905);
		d=GG(d,a,b,c,x[k+2], S22,0xFCEFA3F8);
		c=GG(c,d,a,b,x[k+7], S23,0x676F02D9);
		b=GG(b,c,d,a,x[k+12],S24,0x8D2A4C8A);
		a=HH(a,b,c,d,x[k+5], S31,0xFFFA3942);
		d=HH(d,a,b,c,x[k+8], S32,0x8771F681);
		c=HH(c,d,a,b,x[k+11],S33,0x6D9D6122);
		b=HH(b,c,d,a,x[k+14],S34,0xFDE5380C);
		a=HH(a,b,c,d,x[k+1], S31,0xA4BEEA44);
		d=HH(d,a,b,c,x[k+4], S32,0x4BDECFA9);
		c=HH(c,d,a,b,x[k+7], S33,0xF6BB4B60);
		b=HH(b,c,d,a,x[k+10],S34,0xBEBFBC70);
		a=HH(a,b,c,d,x[k+13],S31,0x289B7EC6);
		d=HH(d,a,b,c,x[k+0], S32,0xEAA127FA);
		c=HH(c,d,a,b,x[k+3], S33,0xD4EF3085);
		b=HH(b,c,d,a,x[k+6], S34,0x4881D05);
		a=HH(a,b,c,d,x[k+9], S31,0xD9D4D039);
		d=HH(d,a,b,c,x[k+12],S32,0xE6DB99E5);
		c=HH(c,d,a,b,x[k+15],S33,0x1FA27CF8);
		b=HH(b,c,d,a,x[k+2], S34,0xC4AC5665);
		a=II(a,b,c,d,x[k+0], S41,0xF4292244);
		d=II(d,a,b,c,x[k+7], S42,0x432AFF97);
		c=II(c,d,a,b,x[k+14],S43,0xAB9423A7);
		b=II(b,c,d,a,x[k+5], S44,0xFC93A039);
		a=II(a,b,c,d,x[k+12],S41,0x655B59C3);
		d=II(d,a,b,c,x[k+3], S42,0x8F0CCC92);
		c=II(c,d,a,b,x[k+10],S43,0xFFEFF47D);
		b=II(b,c,d,a,x[k+1], S44,0x85845DD1);
		a=II(a,b,c,d,x[k+8], S41,0x6FA87E4F);
		d=II(d,a,b,c,x[k+15],S42,0xFE2CE6E0);
		c=II(c,d,a,b,x[k+6], S43,0xA3014314);
		b=II(b,c,d,a,x[k+13],S44,0x4E0811A1);
		a=II(a,b,c,d,x[k+4], S41,0xF7537E82);
		d=II(d,a,b,c,x[k+11],S42,0xBD3AF235);
		c=II(c,d,a,b,x[k+2], S43,0x2AD7D2BB);
		b=II(b,c,d,a,x[k+9], S44,0xEB86D391);
		a=AddUnsigned(a,AA);
		b=AddUnsigned(b,BB);
		c=AddUnsigned(c,CC);
		d=AddUnsigned(d,DD);
	}
 
	var temp = WordToHex(a)+WordToHex(b)+WordToHex(c)+WordToHex(d);
 
	return temp.toLowerCase();
};
//////////////////////////////////////////////////////////////////
//考虑到其他的客户用到shop所以不单独配置md5.js文件，直接在本文件添加  其他地方可以不用修改
//////////////////////////////////////////////////////////////////
Jit.AM.defindPage({
    name: 'GoodsDetail',
	initWithParam: function(param){
		// 根据config 是否显示配送方式
        this.express = param.express;
		if(param.express!=1){

			$('[data-express=hide]').hide();
		}
		
		if(param.invoice != 1){
			$('[data-invoice=hide]').hide();
		}

		if(param.FS1Label){

			$('#fs1Label').html(param.FS1Label);
		}
		if(param.FS2Label){

			$('#fs2Label').html(param.FS2Label);
		}
	},
	/**
	 * @author 沈马石
	 * @desc   优惠券积分模块 
	 */
	Coupon:{
		vars:{
			score:0,     //积分
			balance:0,    //余额
			couponMoney:0,   //优惠券抵用的金额
			scoreMoney:0,    //积分抵用的金额
			orderMoney:0,    //订单金额
			passwordFlag:0,  //是否设置支付密码  1为设置  0为未设置
			lockFlag:0,      //是否账户锁定   1为锁定  0为未锁定  
			skuIds:[{}]  //skuIds集合
		},
		//接口的信息
		urls:{
			//前缀
			prefix:"/ApplicationInterface/Vip/VipGateway.ashx",
			//优惠券
			getCouponAction:"GetVipCoupon",
			//积分
			getScoreAction:"GetVipIntegral",
			//检测支付密码
			checkPayPassAction:"CheckVipPayPassword",
			//设置支付密码
			setPayPassAction:"SetVipPayPassword",
			//获取会员信息
			getVipInfoAction:"GetVipInfo"
		},
		//获得优惠券数据  也可以用来获得积分
		getCouponData:function(obj){
			var me=this;
			//SkuIdAndQtyList   [{SkuId:"",Qty:""},{SkuId:"",Qty:""}]
			JitPage.ajax({
				url:me.urls.prefix,
				data:{
					"action":me.urls[obj.action],
					"SkuIdAndQtyList":obj.skuIds
				},
				success:function(data){
					 if (data.ResultCode == 0) {
                        //表示成功
                        if (obj.callback) {
                            obj.callback(data);
                        }
                    }
                    else {
                        JitPage.alert(data.Message);
                    }
					
				}
			});
		},
		//获得验证码
		getAuthCode:function(mobile){
			Jit.UI.Loading(true);
			var me=this;
			JitPage.ajax({
	            url: '/ApplicationInterface/Gateway.ashx',
	            data: {
	                'action': 'VIP.Login.GetAuthCode',
	                'Mobile': mobile,
	                'VipSource':3
	            },
	            success: function (data) {
	                Jit.UI.Loading(false);
	                if(data.ResultCode==310){
	                	 Jit.UI.Dialog({
			                'content': data.Message,
			                'type': 'Alert',
			                'CallBackOk': function () {
			                    Jit.UI.Dialog('CLOSE');
			                }
			            });
	                }else{
	                	//表示已经发送   1分钟后可以再次发送
	                	me.authCode=false;
	        			me.showTimer("#getCode");
	                }
	                //需要添加60秒倒计时代码
	                
	            }
	        });
		},
		//设置支付密码
		setPayPass:function(obj){
			Jit.UI.Loading(true);
			var me=this;
			JitPage.ajax({
				url:me.urls.prefix,
				data:{
					"action":me.urls.setPayPassAction, //支付密码action
					"Mobile":obj.mobile,  //手机号
					"AuthCode":obj.authCode,  //验证码
					"Password":obj.password, //密码
					"PasswordAgain":obj.passAgain  //再次的密码
				},
				success:function(data){
					Jit.UI.Loading(false);
					 if (data.ResultCode == 0) {
                        //表示成功
                        if (obj.callback) {
                            obj.callback(data);
                        }
                    }
                    else {
                        JitPage.alert(data.Message);
                    }
					
				}
			});
		},
		//检测支付密码
		checkPayPass:function(password,callback){
			Jit.UI.Loading(true);
			var me=this;
			JitPage.ajax({
				url:me.urls.prefix,
				data:{
					"action":me.urls.checkPayPassAction, //检测支付密码action
					"Password":password //密码
				},
				success:function(data){
					Jit.UI.Loading(false);
					 if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
                    else {
                        JitPage.alert(data.Message);
                    }
					
				}
			});
		},
		//获得会员信息
		getVipInfo:function(callback){
			var me=this;
			JitPage.ajax({
				url:me.urls.prefix,
				data:{
					"action":me.urls.getVipInfoAction //获得VIP action
				},
				success:function(data){
					 if (data.ResultCode == 0) {
                        //表示成功
                        if (callback) {
                            callback(data);
                        }
                    }
				}
			});
		},
		elems:{
			couponDiv:$("#coupon"),    //优惠券层
			scoreDiv:$("#score"),      //积分层
			balanceDiv:$("#balance"),     //余额
			money:$("#money"),         //实际支付金额
			mask:$("#mask"),           //mask
			showCoupon:$("#showCoupon"),  //优惠券弹层
			showVip:$("#showVip"),      //显示是否已经是vip会员提示注册
			showPay:$("#showPay"),      //显示支付密码层
			showSetPass:$("#showSetPass"),  //显示设置密码
			useCoupon:$("#coupon .titWrap"), //是否使用优惠券
			useScore:$("#score .titWrap"),   //是否使用积分
			useBalance:$("#balance .titWrap") //是否使用余额
			
		},
		//初始化信息
		init:function(){
			this.initEvent();
		},
		//获得积分余额判断可用余额是否可以点击
		loadBanlanceData:function(){
			var me=this;
			//获取积分余额
			this.getCouponData({
				action:"getScoreAction",
				skuIds:me.vars.skuIds,
				callback:function(data){
					debugger;
					//余额
					me.vars.balance=data.Data.VipEndAmount;
					//积分
					me.vars.score=data.Data.Integral;
					//积分金额
					me.vars.scoreMoney=data.Data.IntegralAmount;
					//设置积分显示的内容
					me.elems.scoreDiv.find(".optionBox>span").html(data.Data.IntegralDesc);
					//设置默认的积分的金额
					me.elems.scoreDiv.attr("data-ammount",data.Data.IntegralAmount);
					//设置余额
					me.elems.balanceDiv.find(".tit").html("使用账户余额"+data.Data.VipEndAmount+"元");
					//设置实际支付的金额
					me.elems.money.html("实际支付金额"+me.vars.orderMoney+"元");
				}
			});
		},
		//根据选择的优惠券还有积分进行余额计算
		changeBalance:function(){
			var elems=this.elems;
			var couponMoney=0,   //优惠券对应的金额
				scoreMoney=0,    //积分对应的金额
				balance=0;       //余额
			var orderMoney=0;
			//计算使用余额的金额
			//第一步判断是否选中了优惠券
			if(elems.useCoupon.hasClass("on")){//如果使用则去查找对应的金额
				//选中的优惠券
				var itemDom=elems.showCoupon.find(".on");
				couponMoney=itemDom.data("ammount");
			}
			//第二步判断是否使用了积分
			if(elems.useScore.hasClass("on")){//使用积分兑换余额
				scoreMoney=elems.scoreDiv.data("ammount");
			}
			//第三步根据订单金额减掉 优惠券的金额  减去积分的金额和余额进行比较     值小的则为要使用的余额
			var minusMoney=Math.subtr(this.vars.orderMoney,Math.add(couponMoney,scoreMoney));
				minusMoney=minusMoney>0?minusMoney:0;
			//获得小的金额
			balance=minusMoney>this.vars.balance?this.vars.balance:minusMoney;
			//更改页面的余额显示内容
			elems.balanceDiv.find(".tit").html("使用账户余额"+balance+"元");
			elems.balanceDiv.find(".tit").data("balance",balance);
			//获得要支付的实际金额
			//判断是否勾选了使用余额的操作
			var realMoney=0;
			if(elems.useBalance.hasClass("on")){
				realMoney=Math.subtr(minusMoney,balance);
			}else{
				realMoney=minusMoney;
			}
			realMoney=realMoney>0?realMoney:0;
			
			//设置实际支付的金额内容
			elems.money.html("实际支付金额"+realMoney+"元");
			elems.money.data("realMoney",realMoney);
		},
		//设置支付密码的验证合法性
		validateData:function(){
			var flag=true;
			var phone=$("#phone").val();
			if(phone.length==0){
				JitPage.alert("手机号不能为空!");
				flag=false;
				return flag;
			}
			if(phone.length!=11){
				JitPage.alert("手机号长度为11位!");
				flag=false;
				return flag;
			}
		    if(!/^[0-9]*$/.test(phone)){
		    	JitPage.alert("手机号只能为数字!");
		        return false;
		    }
		    
			var code=$("#code").val();
			if(code.length==0){
				JitPage.alert("验证码不能为空!");
				flag=false;
				return flag;
			}
			if(isNaN(parseInt(code))){
				JitPage.alert("验证码只能为数字!");
				flag=false;
				return flag;
			}
			var password=$("#pass").val();
			if(password.length==0){
				JitPage.alert("密码不能为空!");
				flag=false;
				return flag;
			}
			if(password.length<6&&password>0){
				JitPage.alert("密码长度最少为6位!");
				flag=false;
				return flag;
			}
			if(password.length>16){
				JitPage.alert("密码长度最多为16位!");
				flag=false;
				return flag;
			}
			var passagain=$("#passAgain").val();
			if(passagain.length==0){
				JitPage.alert("确认密码不能为空!");
				flag=false;
				return flag;
			}
			if(passagain.length<6&&passagain>0){
				JitPage.alert("确认密码长度最少为6位!");
				flag=false;
				return flag;
			}
			if(passagain.length>16){
				JitPage.alert("确认密码长度最多为16位!");
				flag=false;
				return flag;
			}
			if(passagain!=password){
				JitPage.alert("两次输入的密码不一致!");
				flag=false;
				return flag;
			}
			return true;
		},
		//倒计时显示
	    showTimer:function(id,flag){
	    	var count=60;
	    	var that=this;
	    	if(flag){//是否停止定时器
	    		$(id).html("发送验证码");
    			//表示已经发送   1分钟后可以再次发送
            	that.authCode=true;
	    		clearInterval(this.timerId);
	    	}else{
		    	this.timerId=setInterval(function(){
		    		if(count>0){
		    			--count;
		    			$(id).html("<font size='2'>"+count+"秒</font>后发送");
		    		}else{
		    			clearInterval(that.timerId);
		    			$(id).html("发送验证码");
		    			//表示已经发送   1分钟后可以再次发送
		            	that.authCode=true;
		    		}
		    	},1000);
		    }
	    },
		//优惠券相关事件
		initEvent:function(){
			var me=this;
			//选择优惠券事件
			this.elems.couponDiv.find(".titWrap").bind(JitPage.eventType,function(){
				var titWrap=me.elems.couponDiv.find(".titWrap");
				if(titWrap.hasClass("on")){//已经选中  则取消选中状态  更改余额的价格
					titWrap.removeClass("on");
					//设置选中的优惠券的名称
					$(me.elems.couponDiv.find(".optionBox>span")[0]).html("请选择优惠券");
					//将选中的优惠券设置不选中
					me.elems.showCoupon.find(".on").removeClass("on");
					me.changeBalance();
				}else{  //加载优惠券内容
					me.elems.mask.show();
					//加载过 则只显示就行
					if(me.elems.showCoupon.find(".item").length){
						me.elems.showCoupon.show();
					}else{  //获取优惠券
						me.elems.showCoupon.show();
						me.getCouponData({
							action:"getCouponAction",
							skuIds:me.vars.skuIds,
							callback:function(data){
								var tpl=$('#Tpl_couponList').html();
								var html="";
								if(data.Data.CouponList&&data.Data.CouponList){
									for(var i=0;i<data.Data.CouponList.length;i++){
										var item=data.Data.CouponList[i];
										if(item.EnableFlag!=1){//表示可用
											item.FlagClass="off";
										}else{
											item.FlagClass="";
										}
										html+= Mustache.render(tpl,item);	
									}
									me.elems.showCoupon.find(".contentWrap").html(html);
								}else{
									me.elems.showCoupon.find(".contentWrap").html("没有优惠券信息");
								}
							}
						});
					}
				}
				
			});
			//优惠券确定选择的事件
			this.elems.showCoupon.find(".okBtn").bind(JitPage.eventType,function(){
				//确定的时候把item上面为on的内容获得并填充
				var itemDom=me.elems.showCoupon.find(".on");
				if(itemDom.length){
					//设置选中的优惠券的名称
					$(me.elems.couponDiv.find(".optionBox>span")[0]).html(itemDom.data("desc"));
				}else{
					//设置选中的优惠券的名称
					$(me.elems.couponDiv.find(".optionBox>span")[0]).html("请选择优惠券");
				}
				me.elems.mask.hide();
				me.elems.showCoupon.hide();
				//计算余额
				me.changeBalance();
			});
			//每个优惠券选择的事件
			this.elems.showCoupon.delegate(".item",JitPage.eventType,function(){
				var $this=$(this);
				if($this.hasClass("off")){//表示优惠券不可用
					JitPage.alert("优惠券不可用!");
					return;
				}
				if(!$this.hasClass("on")){
					$this.addClass("on");
					$this.siblings().removeClass("on");
					me.elems.couponDiv.find(".titWrap").addClass("on");
				}else{
					$this.removeClass("on");
					me.elems.couponDiv.find(".titWrap").removeClass("on");
				}
				
			});
			//使用积分的世事件
			this.elems.useScore.bind(JitPage.eventType,function(){
				if(me.vars.scoreMoney<=0){//表示积分不能使用
					JitPage.alert("您暂时没有积分可用!");
				}else{
					//已经选中则进行取消
					if(me.elems.useScore.hasClass("on")){
						me.elems.useScore.removeClass("on");
					}else{
						me.elems.useScore.addClass("on");
					}
					me.changeBalance();
				}
				
				
			});
			//选中是否使用账户余额    选中的时候判断是否已经是会员    不是的话    则跳出领会员的层
			this.elems.balanceDiv.bind(JitPage.eventType,function(){
				//是后使用余额   有On则表示已经选中  则取消
				if(me.elems.useBalance.hasClass("on")){
					me.elems.useBalance.removeClass("on");
					me.changeBalance();
					return;
				}
				//判断是否有余额
				if(me.vars.balance>0){
					if(me.vars.status!=undefined){
						if(me.vars.status==1){//未领取会员卡
							me.elems.mask.show();
							me.elems.showVip.show();
						}else if(me.vars.status==2){//已经领取
							//判断账户是否激活
				       		if(me.vars.lockFlag==0){  //账户未锁定
				       			//弹出支付密码的层
				       			if(me.vars.passwordFlag==1){//已经设置支付密码
				       				me.elems.mask.show();
				       				me.elems.showPay.find("input").val("");
				       				me.elems.showPay.show();
				       			}else{//未设置支付密码   则弹出设置支付密码的层
				       				me.elems.mask.show();
				       				me.elems.showSetPass.show();
				       			}
				       		}else{
                                JitPage.alert("支付账户被冻结!");
				       		}
						}else if(me.vars.status==0){
                            JitPage.alert("你的会员状态已取消，请联系管理员！");
                        }
					}else{
						//获得会员信息
						me.getVipInfo(function(data){
							me.vars.status=data.Data.Status;
							if(data.Data.Status==1){//未领取会员卡
								me.elems.mask.show();
								me.elems.showVip.show();
							}else if(data.Data.Status==2){//已经领取
								//判断账户是否激活
					       		if(me.vars.lockFlag==0){  //账户未锁定
					       			//弹出支付密码的层
					       			if(me.vars.passwordFlag==1){//已经设置支付密码
					       				me.elems.mask.show();
					       				me.elems.showPay.find("input").val("");
					       				me.elems.showPay.show();
					       			}else{//未设置支付密码   则弹出设置支付密码的层
					       				me.elems.mask.show();
					       				me.elems.showSetPass.show();
					       			}
					       		}else{
					       			me.alert("支付账户被冻结!");
					       		}
							}
						});
					}
				}else{
					JitPage.alert("您的余额不足,不能使用");
				}
				
				
			});
			//隐藏会员领取的取消事件
			this.elems.showVip.find(".cancel").bind(JitPage.eventType,function(){
				me.elems.mask.hide();
				me.elems.showVip.hide();
			});
			//确认支付
			this.elems.showPay.find(".surePay").bind(JitPage.eventType,function(){
				var password=me.elems.showPay.find("input").val();
				if(password.length==0){
					JitPage.alert("支付密码不能为空");
					return;
				}
				if(password.length<6&&password>0){
					JitPage.alert("密码长度最少为6位!");
					return;
				}
				if(password.length>16){
					JitPage.alert("密码长度最多为16位!");
					return;
				}
				//检测支付密码
				me.checkPayPass(MD5(password),function(data){
					Jit.UI.Loading(false);
					me.elems.balanceDiv.find(".titWrap").addClass("on");
					me.elems.mask.hide();
					me.elems.showPay.hide();
					me.changeBalance();
					
					
				});
				
			});
			//忘记密码
			this.elems.showPay.find(".forget").bind(JitPage.eventType,function(){
				me.elems.showPay.hide();
				me.elems.showSetPass.show();
			});
			//设置密码的取消事件
			this.elems.showSetPass.find("#cancel").bind(JitPage.eventType,function(){
				me.elems.mask.hide();
				me.elems.showSetPass.hide();
				//停止倒计时
    			me.showTimer("#getCode",true); //第二个参数为是否停止定时器
			});
			//设置密码的取消事件
			this.elems.showSetPass.find("#sureSet").bind(JitPage.eventType,function(){
				//内容验证
				var flag=me.validateData();
				if(flag){//数据合法
					var phone=$("#phone").val();
					var passagain=$("#passAgain").val();
					var password=$("#pass").val();
					var code=$("#code").val();
					me.setPayPass({
						mobile:phone,
						password:MD5(password),
						passAgain:MD5(passagain),
						authCode:code,
						callback:function(data){
							Jit.UI.Loading(false);
							//设置支付密码状态成功
							me.vars.passwordFlag=1;
							me.elems.showSetPass.hide();
							me.elems.showPay.show();
							//me.elems.showSetPass.hide();
						}
					});
				}
				
			});
			//获取验证码
			this.elems.showSetPass.find("#getCode").bind(JitPage.eventType,function(){
				if(me.authCode==undefined||me.authCode){
					var phone=$("#phone").val();
					if(phone.length==0){
						JitPage.alert("手机号不能为空!");
						return;
					}
					if(phone.length!=11){
						JitPage.alert("手机号长度为11位!");
						return;
					}
				    if(!/^[0-9]*$/.test(phone)){
				    	JitPage.alert("手机号只能为数字!");
				        return ;
				    }
	            	me.getAuthCode(phone);
	            }
			});
		}
		
		
	},
    onPageLoad: function() {
        var me = this;
        this.orderInfo = null;

        //人人销售的店员ID
		var salesUserId=Jit.AM.getPageParam("_salesUserId_"),
        	channelId=Jit.AM.getPageParam("_channelId_"),  //渠道ID
        	recommendVip=Jit.AM.getPageParam("_recommendVip_");  //推荐会员
        if(salesUserId&&channelId){
            var appVersion=Jit.AM.getAppVersion();
            appVersion.AJAX_PARAMS="openId,customerId,userId,locale,ChannelID";
            Jit.AM.setAppVersion(appVersion);
            Jit.AM.setPageParam("_salesUserId_",salesUserId);
            Jit.AM.setPageParam("_channelId_",channelId);
            //公用参数
            var baseInfo=Jit.AM.getBaseAjaxParam();
            baseInfo.ChannelID=channelId;
            Jit.AM.setBaseAjaxParam(baseInfo);
        }else{   //没有传递过来则把数据清空掉
            Jit.AM.setPageParam("_salesUserId_",null);
            Jit.AM.setPageParam("_channelId_",null);
        }
        //=============================新接口获取订单详情=================
		me.ajax({
			url : '/ApplicationInterface/Gateway.ashx',
			data : {
				'action' : 'Order.Order.GetOrderDetail',
				'orderId' : me.getUrlParam('orderId')
			},
			success : function(data) { 
				if (data.IsSuccess) {
                    var order = data.Data.OrderListInfo,
                        tpl = $('#Tpl_goods_info').html(),
                        html = '',
                        totalprice = 0;

                    me.initPageData(order);
                    for (var i = 0; i < order.OrderDetailInfo.length; i++) {

                        //totalprice += order.orderDetailList[i].salesPrice;
						order.OrderDetailInfo[i]['image120'] = order.OrderDetailInfo[i].ImageInfo.length?Jit.UI.Image.getSize(order.OrderDetailInfo[i].ImageInfo[0].ImageUrl,'120'):"";
						var gg="";
						if(order.OrderDetailInfo[i].GG){
							for(var j=0;j<5;j++){
								var theName="PropDetailName"+(j+1);
								var theName2="PropName"+(j+1);
								if(order.OrderDetailInfo[i].GG[theName]&&order.OrderDetailInfo[i].GG[theName]!=""){
									gg+=order.OrderDetailInfo[i].GG[theName2]+"--"+order.OrderDetailInfo[i].GG[theName]+"<br/> ";
								}
							}
							
						}
						order.OrderDetailInfo[i].GG=gg;
                        html += Mustache.render(tpl, order.OrderDetailInfo[i]);
                    }
                    $('#goods_list').append(html);

                } else {

                    Jit.UI.Dialog({
                        'content': data.Message,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
			}
		});
        //获取配送方式
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getDeliveryList'
            },
            success: function(data) {
                if (data.code == 200) {
                    var adslist = data.content.deliveryList;
					var domList =[];
                    for (var i = 0; i < adslist.length; i++) {
                        domList.push('[psfs=fs' + adslist[i].deliveryId + ']');
                    }
                   	$(domList.join(",")).parent().show();
                }
            }
        });

        me.initEvent();
        //优惠券事件初始化
        me.Coupon.init();
        
        
    },
    //=============================新接口获取配送费      =================
    GetDeliveryAmount:function(order,callback){
        var me = this;
        //alert(order.TotalAmount+"------"+order.DeliveryID);
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'GetDeliveryAmount',
                "totalAmount":order.TotalAmount,
                "DeliveryId":order.DeliveryID
            },
            success: function(data) {
                //alert(JSON.stringify(data));
                if (data.Code == 200||data.code == 200) {
                    try{
                    	me.DeliveryAmount=data.Data.DeliveryAmount||0;  //将配送费存起来
                        $('[data-express=hide] a b').html((data.Data.DeliveryAmount||0)+"元");
                        if(data.Data.DeliveryStrategyDesc){
                        	$("#devilyRemark").html(data.Data.DeliveryStrategyDesc).show();
                        }else{
                        	$("#devilyRemark").hide();
                        }
                        if(callback){   //回调
                        	callback(data);
                        }
                    }catch (e){
                        alert(e);
                    }
                }
            }
        });
    },
    initPageData: function(data) {
        var me = this;
		me.orderInfo = data;
		//保存订单的金额
		me.Coupon.vars.orderMoney=data.TotalAmount;
		me.Coupon.elems.money.data("realMoney",data.TotalAmount);
		//设置实际支付的金额
		me.Coupon.elems.money.html("实际支付金额"+data.TotalAmount+"元");
        $('#totalprice').html("￥" + data.TotalAmount);

        $('#totalqty').html(data.TotalQty);
		//=======将skuIds获取出来============//
		var array=[];
		if(data.OrderDetailInfo&&data.OrderDetailInfo.length){
			for(var i=0;i<data.OrderDetailInfo.length;i++){
				var item=data.OrderDetailInfo[i];
				var obj={
					SkuId:item.SkuID,
					Qty:item.Qty
				};
				array.push(obj);
			}
			//赋值保存
			me.Coupon.vars.skuIds=array;
		}
		
		//获得配送费
		me.GetDeliveryAmount(me.orderInfo,function(deliveryMoney){
			var mmoney=0;
			debugger;
			if(me.orderInfo.OrderStatus=="-99"){   //状态为100的时候数据库已经有配送费
				mmoney=(data.TotalAmount+((deliveryMoney.Data.DeliveryAmount||0)-0));
			}else{
				mmoney=data.TotalAmount;
			}
			//保存订单的金额
			me.Coupon.vars.orderMoney=mmoney;
			me.Coupon.elems.money.data("realMoney",mmoney);
			//设置实际支付的金额
			me.Coupon.elems.money.html("实际支付金额"+mmoney+"元");
		});
		
		//去初始化优惠券的相关内容
		me.Coupon.loadBanlanceData();
		//========================================
		//获得会员信息
		me.Coupon.getVipInfo(function(data){
			me.Coupon.vars.status=data.Data.Status;
			//是否设置了支付密码
			me.Coupon.vars.passwordFlag=data.Data.PasswordFlag;
			//是否账户锁定
			me.Coupon.vars.lockFlag=data.Data.LockFlag;
		});
		//获得优惠券
		me.Coupon.getCouponData({
			action:"getCouponAction",
			skuIds:me.Coupon.vars.skuIds,
			callback:function(data){
				var tpl=$('#Tpl_couponList').html();
				var html="";
				if(data.Data.CouponList&&data.Data.CouponList){
					for(var i=0;i<data.Data.CouponList.length;i++){
						var item=data.Data.CouponList[i];
						if(item.EnableFlag!=1){//表示可用
							item.FlagClass="off";
						}else{
							item.FlagClass="";
						}
						html+= Mustache.render(tpl,item);	
					}
				}
				me.Coupon.elems.showCoupon.find(".contentWrap").append(html);
			}
		});
		//============================================================		
        // $('#payprice').html("￥" + data.totalAmount);
        if (data.DeliveryAddress) {
            data.deliveryId = parseInt(data.DeliveryID);
            if (data.deliveryId == 2) {
                $('#shopAddress').html(data.DeliveryAddress);
                var radioB = $('[value=radioB]');
                radioB.attr('checked', 'checked');
                radioB.addClass('on');
                $('#shopTitle').html(data.CarrierName);
                $('#shopTel').html(data.StoreTel);
                $('#shopAddress').parents('.addressInfo').show();
                me.GetOrderDeliveryTimeRange(data.StoreID,function(data){
					//me.DateTimeEvent(data);
                    
                    me.DateRange = data.DateRange;

                    me.initDateSelection();
                });
            } else if (data.deliveryId == 1) {   //拿的是receiverName   选择地址的名字
                $('#linkMan').html(data.ReceiverName);
                $('#linkTel').html(data.Mobile);
                $('#linkAddress').html(data.DeliveryAddress);
                var radioA = $('[value=radioA]');
                radioA.attr('checked', 'checked');
                radioA.addClass('on');           
                $("#addressInfo").show();
                //$('#linkAddress').parents('.addressInfo').show();

                // 获取配送费
                if(me.express==1){
                    me.GetDeliveryAmount(me.orderInfo,function(deliveryMoney){
                    	var mmoney=0;
						if(me.orderInfo.OrderStatus=="-99"){   //状态为100的时候数据库已经有配送费
							mmoney=(me.orderInfo.TotalAmount+((deliveryMoney.Data.DeliveryAmount||0)-0));
						}else{
							mmoney=me.orderInfo.TotalAmount;
						}
						//保存订单的金额
						me.Coupon.vars.orderMoney=mmoney;
						me.Coupon.elems.money.data("realMoney",mmoney);
						//设置实际支付的金额
						me.Coupon.elems.money.html("实际支付金额"+mmoney+"元");
                    });
                }
            }
            me.hasAddress = true;
        } else {
            //获取用户设置的默认地址
            var _data = {
                'action': 'getVipAddressList',
                'page': 1,
                'pageSize': 99
            };

            if (me.getUrlParam('orderId')) {

                _data.orderId = me.getUrlParam('orderId');
            }

            me.ajax({
                url: '/OnlineShopping/data/Data.aspx',
                data: _data,
                success: function(data) {
                    if (data.code == 200 && data.content.itemList) {
                        var list = data.content.itemList,
                            defaultAddress;
                        for (var i = 0; i < list.length; i++) {
                            if (list[i].isDefault == "1") {
                                defaultAddress = list[i];
                                break;
                            };

                        }
                        if (defaultAddress) {
                        	var address =defaultAddress.province+defaultAddress.cityName+defaultAddress.districtName+defaultAddress.address;
                            $('#linkMan').html(defaultAddress.linkMan);
                            $('#linkTel').html(defaultAddress.linkTel);
                            $('#linkAddress').html(address);
                            var radioA = $('[value=radioA]');
                            radioA.attr('checked', 'checked');
                            radioA.addClass('on');
                            $('#linkAddress').parents('div').show();
                            me.hasAddress = true;
                            //为订单设置默认地址
                            me.ajax({
                                url: '/OnlineShopping/data/Data.aspx',
                                data: {
                                    'action': 'setOrderAddress',
                                    'orderId': me.getUrlParam('orderId'),
                                    'linkMan': defaultAddress.linkMan,
                                    'linkTel': defaultAddress.linkTel,
                                    'address': address,
									'deliveryId' : 1
                                },
                                success: function(data) {
                                    //alert(JSON.stringify(data))
                                    if (data.code == 200) {
                                        // 获取配送费
                                        me.orderInfo.DeliveryID=1;
                                        if(me.express==1){
                                            me.GetDeliveryAmount(me.orderInfo,function(deliveryMoney){
                                            	var mmoney=0;
												if(me.orderInfo.OrderStatus=="-99"){   //状态为100的时候数据库已经有配送费
													mmoney=(me.orderInfo.TotalAmount+((deliveryMoney.Data.DeliveryAmount||0)-0));
												}else{
													mmoney=me.orderInfo.TotalAmount;
												}
												//保存订单的金额
												me.Coupon.vars.orderMoney=mmoney;
												me.Coupon.elems.money.data("realMoney",mmoney);
												//设置实际支付的金额
												me.Coupon.elems.money.html("实际支付金额"+mmoney+"元");
                                            });
                                        }
                                        // body
                                    }
                                }
                            });
                        }
                    }
                }
            });
            JitPage.calculateAmount();
        }
		//me.getTodayDate();
    },
    DateTimeEvent : function(options) {//日期事件
		var self = this, 
			opt = {};
		
		opt.datetime = {preset : 'datetime'};
		opt.date = {preset : 'date'};
		opt.time = {preset : 'time'};
		
		opt["default"] = {
			theme : 'android-ics light', //皮肤样式
			display : 'modal', //显示方式
			mode : 'scroller', //日期选择模式
			lang : 'zh',
			CallBack : function() {}
		};
		opt.limit = {
			minDate  : new Date(options.BeginDateTime), 
			maxDate  : new Date(options.EndDateTime),
			stepMinute :10,
			firstDay:1
		};
		$("#selectDate").mobiscroll().datetime($.extend(opt['datetime'], opt['default'],opt.limit));
		//$("#selectTime").mobiscroll().time($.extend(opt['time'], opt['default'],opt.limit));
	},
    initDateSelection:function(){

        var me = this,html=[];

        function initTimeWithSelection(idx){

            var timestr = '',html=[];

            for(var i in me.DateRange[idx]['Ranges']){

                timestr = me.DateRange[idx]['Ranges'][i]['Desc'];

                html.push('<option value="'+timestr+'">'+timestr+'</option>');
            }

            $('#selectTime').html(html.join(''));
        }


        for(var i in me.DateRange){

            html.push('<option value="'+me.DateRange[i]['Date']+'">'+me.DateRange[i]['Date']+'</option>');
        }

        $('#selectDate').html(html.join('')).on('change',function(){

            var selection = $(this);

            initTimeWithSelection(selection.get(0).selectedIndex);
        });

        initTimeWithSelection($('#selectDate').get(0).selectedIndex);
    },
    initEvent: function() {

        var me = this,
            $goodsWrap = $('.goods_wrap');

        $('.delivery_mode .select_address').bind('click', function(evt) {

            var psfs = $(evt.currentTarget).attr('psfs');

            if (psfs == 'fs1') {

                me.toPage('SelectAddress', '&orderId=' + me.getUrlParam('orderId'));

            } else if (psfs == 'fs2') {

                me.toPage('SelectStore', '&orderId=' + me.getUrlParam('orderId'));
            }
        });

        $goodsWrap.delegate('.btn-yes', 'click', function() {
            $('.ui-mask').hide();
            $('.delivery_mode_list').hide();
        });
    },

    submitOrder: function() {
        var me = this;
        if (!me.hasAddress) {
			me.alert("请填写详细配送地址！");
            return;
        }
		
		if(!me.orderInfo){
			me.alert("订单信息丢失");
			return;
		}
		if(me.orderInfo.DeliveryID==2){
			var shopMan = $.trim($("#shopMan").val()),
	        	shopTel = $.trim($("#shopTel").val()),
                shopDate = $('#selectDate').val()+' '+$('#selectTime').val();
	        	
        	
			if(!shopMan.length){
				me.alert("请输入提货人！");
				return;
			}
			if(!shopTel.length){
				me.alert("请选择提货人手机号码！");
				return;
			}else if(!Jit.valid.isPhoneNumber(shopTel)){
				me.alert("请输入正确的手机号码！");
				return;
			}
			if(!shopDate.length){
				me.alert("请选择提货日期！");
				return;
			}
			me.ajax({
				url:'/ApplicationInterface/Gateway.ashx',
				data:{
					'action':'Order.Delivery.UpdateOrderDeliveryInfo',
					'OrderID':Jit.AM.getUrlParam('orderId'),
					'DeliveryTypeID':me.orderInfo.DeliveryID,
					'StoreID':me.orderInfo.StoreID,
					'PickupUpDateRange':shopDate,
					'Mobile':shopTel,
					'ReceiverName':shopMan
				},
				success:function(data){
					if(data.IsSuccess){
						me.submit();
					}else{
						me.alert(data.Message);
					}
				}
			});
		}else{
			me.submit();
		}
        
    },
    submit:function(){
    	var me = this;
    	var elems=me.Coupon.elems;
    	//使用优惠券
    	var couponFlag=elems.useCoupon.hasClass("on")?1:0;
    	//优惠券ID
    	var couponId=elems.showCoupon.find(".on").data("id");
    	//是否使用积分
    	var integralFlag=elems.useScore.hasClass("on")?1:0;
    	//是否使用余额
    	var vipEndAmountFlag=elems.useBalance.hasClass("on")?1:0;
    	//余额
    	var vipEndAmount=elems.balanceDiv.find(".tit").data("balance");
    	me.ajax({
            //url: '/OnlineShopping/data/Data.aspx',
            url:"/ApplicationInterface/Vip/VipGateway.ashx",
            data: {
                'action': 'SetOrderStatus',
                'OrderId': me.getUrlParam('orderId'),
                'Status': '100',
                'CouponFlag':couponFlag,   //是否选用优惠券  1是 0 否
                'CouponId':couponId,       //优惠券ID
                'IntegralFlag':integralFlag,  //是否使用积分  1 是 0 否
                'VipEndAmountFlag':vipEndAmountFlag,  //是否使用余额  1是 0否
                'VipEndAmount':vipEndAmount,  //余额,
                'Remark':$("#remark").val()
            },
            success: function(data) {
                if (data.ResultCode ==0) {
                	//判断实际支付的金额是否是大于0   只要大于0 
			       	var elems=me.Coupon.elems;
			       	//实际金额
			       	var realMoney=elems.money.data("realMoney");
			       	//不需要选择其他的支付方式通过余额优惠券直接支付
			       	if(realMoney==0){ //跳转到支付描述界面
			       		me.toPage('PaySuccess','useBalance=true')	;
			       	}else{
	                    //已下单
	                    me.toPage('OrderPay', '&orderId=' + me.getUrlParam('orderId')+'&isGoodsPage=1&realMoney='+realMoney);
	                    TopMenuHandle.ReCartCount();
	                }
                }else{
                	me.alert(data.Message);
                }
            }
        });
    },
    checkPayType: function(obj) {

        $(".ckpay").each(function() {
            this.checked = false;
            $(this).removeAttr("checked", "");
        });
        obj.checked = true;
        $(obj).attr("checked", "checked");
    },
    calculateAmount: function() {

        var totalprice = $("#totalprice").text().replace('￥', '');

        //计算总的数字
        JitPage.ajax({
           url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'orderCouponSum',
                'orderId': JitPage.getUrlParam('orderId'),
                'loadFlag': 1
            },
            success: function(data) {

                if (data.code == 200) {
                    var data = data.content;
                    $("#payprice").text('￥' + parseInt(Math.subtr(totalprice , data.amount)));
                    $('#juan-count').text(data.count);
                    $('#juan-amount').text(data.amount);
                }
            },
            failure: function() {
                Jit.UI.Dialog({
					'content': '计算错误！',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
				return;
            }
        });
    },
    GetOrderDeliveryTimeRange:function(storeId,callback){
    	var self =this;
		self.ajax({
			url:'/ApplicationInterface/Gateway.ashx',
			type:'get',
			data:{
				'action':'Order.Delivery.GetOrderDeliveryTimeRange',
				'StoreID':storeId
			},
			beforeSend:function(){				
				Jit.UI.AjaxTips.Tips({show:false});
			},
			success:function(data){
				if(data.IsSuccess){
					if(callback){
						callback(data.Data);
					}
				}else{
					self.alert(data.Message);
				}
			},
			complete:function(){
			}
		});
    },
    alert :function(text,callback){
		Jit.UI.Dialog({
			type:"Alert",
			content:text,
			CallBackOk:function(data){
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
	}
});