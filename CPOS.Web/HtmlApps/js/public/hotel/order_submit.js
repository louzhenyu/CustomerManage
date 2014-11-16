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


var __data = null;
Jit.AM.defindPage({
    name: 'OrderSubmit',
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
			skuIds:[{}],  //skuIds集合
			returnScore:0,//返还的积分
			returnMoney:0 //返还的现金
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
					"SkuIdAndQtyList":obj.skuIds,
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
					"action":me.urls.getVipInfoAction, //获得VIP action
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
					me.elems.money.html(me.vars.orderMoney);
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
			var minusMoney=this.vars.orderMoney-couponMoney-scoreMoney;
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
				realMoney=minusMoney-balance;
			}else{
				realMoney=minusMoney;
			}
			realMoney=realMoney>0?realMoney:0;
			
			//设置实际支付的金额内容
			elems.money.html(realMoney);
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
				}else{
					//加载过 则只显示就行
					if(me.elems.showCoupon.find(".item").length){
						me.elems.mask.show();
						me.elems.showCoupon.show();
					}else{  //获取优惠券
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
										me.elems.mask.show();
										me.elems.showCoupon.find(".contentWrap").append(html);
										me.elems.showCoupon.show();
									}
								}else{
									JitPage.alert("您还没有优惠券可以使用!");
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
					JitPage.alert("您的积分不足，不能使用");
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
				       			me.alert("支付账户被冻结!");
				       		}
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
    onPageLoad: function () {
        Jit.log('进入OrderSubmit.....');
        this.initEvent();
        this.Coupon.init();
    },
    initEvent: function () {
        var me = this;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getItemDetailForHotel',
                'storeId': me.getUrlParam('storeId'),  //店ID
                'itemId': me.getUrlParam('itemId'),    //商品标识
                'beginDate': me.getParams('InDate'), //开始日期
                'endDate': me.getParams('OutDate')  //结束日期x
            },
            success: function (data) {
            	if(data.code=="200"){
	                var returnData = data.content;
					 //debugger;
			        var num = $("#goods_number").val();
			        var allAomunt = 0;
			        //=======将skuIds获取出来============//
					var array=[];
			        if (num > 0 &&returnData.storeItemDailyStatus&&returnData.storeItemDailyStatus.length > 0) {
			            for (var i = 0; i < returnData.storeItemDailyStatus.length - 1; i++) {
			                if (returnData.storeItemDailyStatus[i].LowestPrice) {
			                    allAomunt += returnData.storeItemDailyStatus[i].LowestPrice * num;
			                }
			                var item=returnData.storeItemDailyStatus[i];
							var obj={
								SkuId:item.SkuID,
								Qty:1
							};
							array.push(obj);
			            }
			            //赋值保存
						me.Coupon.vars.skuIds=array;
			        }
					me.Coupon.elems.money.data("realMoney",allAomunt);
					//设置实际支付的金额
					me.Coupon.elems.money.html(allAomunt);
					//保存订单的金额
					me.Coupon.vars.orderMoney=allAomunt;
					//保存每个房间订购的返还的积分和现金
					me.Coupon.vars.returnScore=returnData.Forpoints?returnData.Forpoints:0;  //返还的积分
					me.Coupon.vars.returnMoney=returnData.GG?returnData.GG:0;         //返还的现金
					$("#returnScore").html(me.Coupon.vars.returnScore);
					$("#returnMoney").html(me.Coupon.vars.returnMoney);
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
					
					
					
					
					
					
					
	                $('[fvalue=storeName]').html("酒店：" + returnData.storeInfo.storeName); //酒店名
	                $('[fvalue=itemName]').html("房型：" + returnData.itemName); //房型
	                $('[fvalue=inDate]').html("入住时间：" + me.getParams('InDate')); //房型
	                $('[fvalue=outDate]').html("离店时间：" + me.getParams('OutDate')); //房型
	                $('[fvalue=remark]').html(returnData.remark); //房型
	
	                __data = returnData;
	                JitPage.fnCalcAomunt(returnData); //计算金额
	                JitPage.fnGetWeekData(returnData); //生成每日 数据
	
	                /*日期比较*/
	                var dateVal = "";
	                var date = new Date();
	                var pToDay = me.getParams('InDate').replace('/', '-');
	                var pCallDate = me.getParams('OutDate').replace('/', '-');
	                if (pToDay != null && pCallDate != null && pToDay != "" && pCallDate != "") {
	                    dateVal = me.daysBetween(pCallDate,pToDay);
	                }
	                $('#nightNum').html(dateVal.toString().replace('-', ''));
	            }else{
	            	JitPage.alert(data.description);
	            }
            }
        });

        $("#goBack").bind("click", function () {
            Jit.UI.Dialog({
                'content': '您的订单还未完成哦，确定要离开吗?',
                'type': 'Confirm',
                'LabelOk': '确定',
                'LabelCancel': '取消',
                'CallBackOk': function () {
                    Jit.AM.pageBack();
                },
                'CallBackCancel': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return;
        });
    },
    fnAdd: function () {
        var num = $("#goods_number").val();
        if (num > 0) {
            num++;
            $("#goods_number").val(num);
        } else {
            $("#goods_number").val(1);
        }
        JitPage.fnCalcAomunt(__data); //计算金额
    },
    fnSub: function () {
        var num = $("#goods_number").val();
        if (num > 1) {
            num--;
            $("#goods_number").val(num);
        } else {
            $("#goods_number").val(1);
        }
        JitPage.fnCalcAomunt(__data); //计算金额
    },
    fnCalcAomunt: function (data) {
        //debugger;
        var num = $("#goods_number").val();
        var allAomunt = 0;
        if (num > 0 &&data.storeItemDailyStatus&&data.storeItemDailyStatus.length > 0) {
            for (var i = 0; i < data.storeItemDailyStatus.length - 1; i++) {
                if (data.storeItemDailyStatus[i].LowestPrice) {
                    allAomunt += data.storeItemDailyStatus[i].LowestPrice * num;
                }
            }
        }
        $("[fvalue=num]").html("X" + num);
        $("[fvalue=number]").html(num);
        $("[fvalue=amount]").html(allAomunt?allAomunt:0);
        //返还的积分
        $("#returnScore").html(this.Coupon.vars.returnScore*num);
        //返还的现金
        $("#returnMoney").html(this.Coupon.vars.returnMoney*num);
        //每间房间的积分和金额兑换
		// var scores=num*this.Coupon.vars.score,
			// scoreMoney=num*this.Coupon.vars.scoreMoney;
		// if(scores==0){
			// //设置积分描述
			// this.Coupon.elems.scoreDiv.find(".optionBox>span").html("无可用积分");
		// }else{
			// //设置积分描述
			// this.Coupon.elems.scoreDiv.find(".optionBox>span").html("使用"+scores+"积分,抵用"+scoreMoney+"元");
			// this.Coupon.elems.scoreDiv.data("ammount",scoreMoney);
		// }
        //设置总共的实际支付的钱
        this.Coupon.vars.orderMoney=allAomunt;
        //修改实际支付的金额
        this.Coupon.changeBalance();
    },
    fnGetWeekData: function (data) {
        var me = this;
        var $tr = $('<tr></tr>');
        var num = $("#goods_number").val();
        if (num > 0 &&data.storeItemDailyStatus&& data.storeItemDailyStatus.length > 0) {
            var date = new Date(data.storeItemDailyStatus[0].StatusDate);  //时间
            var day = date.getDay();
            var alllength = 0;

            for (var i = 0; i < day; i++) {
                alllength++;
                $('<td class="tdline"></td>').html("<span></span>").appendTo($tr);
            }
            for (var j = 0; j < data.storeItemDailyStatus.length - 1; j++) {
                if (alllength % 7 == 0) {
                    $("#tblWeek").append($tr);
                    $tr = $('<tr></tr>');
                }
                alllength++;
                if (data.storeItemDailyStatus[j].LowestPrice > 0) {
                    $('<td class="tdline"></td>').html("<span class='on'><em>¥" + data.storeItemDailyStatus[j].LowestPrice + "</em><br /><em fvalue='num' >X" + num + "</em></span>").appendTo($tr);
                }
            }

            if (alllength % 7 > 0) {
                var residueNum = (parseInt(alllength / 7) + 1) * 7 - alllength;
                for (var j = 0; j < residueNum; j++) {
                    $('<td class="tdline"></td>').html("<span></span>").appendTo($tr);
                }
                $("#tblWeek").append($tr);
            } else {
                $("#tblWeek").append($tr);
            }



        }
    },
    daysBetween: function (DateOne, DateTwo) {
        var OneMonth = DateOne.substring(5, DateOne.lastIndexOf('-'));
        var OneDay = DateOne.substring(DateOne.length, DateOne.lastIndexOf('-') + 1);
        var OneYear = DateOne.substring(0, DateOne.indexOf('-'));

        var TwoMonth = DateTwo.substring(5, DateTwo.lastIndexOf('-'));
        var TwoDay = DateTwo.substring(DateTwo.length, DateTwo.lastIndexOf('-') + 1);
        var TwoYear = DateTwo.substring(0, DateTwo.indexOf('-'));

        var cha = ((Date.parse(OneMonth + '/' + OneDay + '/' + OneYear) - Date.parse(TwoMonth + '/' + TwoDay + '/' + TwoYear)) / 86400000);
        return cha;
    },
    fnSubmit: function () {
    	var me = this;
        var me = this;
        var intRegular = /^[0-9]*[1-9][0-9]*$/;
        var phoneRegular = /^(1(([0-9][0-9])|(47)|[8][012356789]))\d{8}$/;
        //组装订单数据
        var houseList = new Array();
        var num = $("#goods_number").val();
        var allAomunt = 0;
        var storeId = me.getUrlParam('storeId');  //店ID

        var txtPhone = $("#txtPhone").val();      //手机号
        var txtUserName = $("#txtUserName").val(); //用户名
        var remark = $("#remark").val()


        //入住人姓名格式：1.英文姓名格式为：Lastname/Firstname 2.姓名中不可含有称谓等词语，如：小姐、先生、太太、夫人等。
        if (!txtUserName) {
            Jit.UI.Dialog({
                'content': '入住人姓名不能为空！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        } else {
            if (!/^[A-Za-z .\u4e00-\u9fa5][A-Za-z .\u4e00-\u9fa5]{1,50}$/.test(txtUserName)) {
                Jit.UI.Dialog({
                    'content': '入住人姓名有误，请输入中文名或英文名',
                    'type': 'Alert',
                    'CallBackOk': function () {
                        Jit.UI.Dialog('CLOSE');
                    }
                });
                return false;
            } else {
                if (txtUserName.indexOf("先生") > -1 || txtUserName.indexOf("小姐") > -1 || txtUserName.indexOf("太太") > -1 || txtUserName.indexOf("夫人") > -1) {
                    Jit.UI.Dialog({
                        'content': '入住人姓名不可以是某先生或小姐.太太.夫人等!',
                        'type': 'Alert',
                        'CallBackOk': function () {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                    return false;
                }
            }
        }
        if (!txtPhone) {
            Jit.UI.Dialog({
                'content': '请填写手机号！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        if (!txtPhone.match(phoneRegular)) {
            Jit.UI.Dialog({
                'content': '联系电话格式不正确,请重新输入！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        if (!intRegular.test(num)) {
            Jit.UI.Dialog({
                'content': '房间数格式不正确,请重新输入！',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }

        if (num > 0) {
            var beginDate = endDate = new Date();
            if(__data.storeItemDailyStatus){
	            for (var i = 0; i < __data.storeItemDailyStatus.length - 1; i++) {
	                if (__data.storeItemDailyStatus[i].LowestPrice) {
	                    allAomunt += __data.storeItemDailyStatus[i].LowestPrice * num;
	                }
	            }
	            beginDate = JitPage.formatDate(new Date(__data.storeItemDailyStatus[0].StatusDate));
	            endDate = JitPage.formatDate(new Date(__data.storeItemDailyStatus[__data.storeItemDailyStatus.length - 1].StatusDate));
	
	            /*根据开始 结束日期 只传一条数据*/
	            houseList.push({ skuId: __data.storeItemDailyStatus[0].SkuID, salesPrice: __data.storeItemDailyStatus[0].LowestPrice, qty: num, beginDate: beginDate, endDate: endDate });
	
	            /* OLD 向数据库传多条数据
	            $.each(__data.storeItemDailyStatus, function (i, v) {
	            if (v.LowestPrice) {
	            allAomunt += v.LowestPrice * num;
	            var fDatetime = JitPage.formatDate(new Date(v.StatusDate));
	            houseList.push({ skuId: v.SkuID, salesPrice: v.LowestPrice, qty: num, beginDate: fDatetime, endDate: fDatetime });
	            }
	            });*/
	        }
        }
        if (txtUserName && txtPhone && houseList) {
            me.ajax({
                url: '/OnlineShopping/data/Data.aspx',
                data: {
                    'action': 'setOrderInfo',
                    'mobile': txtPhone,
                    'username': txtUserName,
                    'storeId': storeId,
                    'totalAmount': allAomunt,
                    'orderDetailList': houseList,
                    'remark': remark,
                    'reqBy': 1
                },
                beforeSend: function () {
                    Jit.UI.Masklayer.show();
                },
                success: function (data) {
                    Jit.UI.Masklayer.hide();
                    if (data && data.code == 200) {
                   		//根据返回的orderId进行提交数据
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
				                'OrderId': data.content.orderId,
				                'Status': '100',
				                'CouponFlag':couponFlag,   //是否选用优惠券  1是 0 否
				                'CouponId':couponId,       //优惠券ID
				                'IntegralFlag':integralFlag,  //是否使用积分  1 是 0 否
				                'VipEndAmountFlag':vipEndAmountFlag,  //是否使用余额  1是 0否
				                'VipEndAmount':vipEndAmount   //余额
				            },
				            success: function(data) {
				                
				            }
				        });
                        me.fnSendMsg(data.content.orderId);

                        Jit.UI.Dialog({
                            'content': '订单提交成功!',
                            'type': 'Confirm',
                            'LabelOk': '再逛逛',
                            'LabelCancel': '我的订单',
                            'CallBackOk': function () {
                                me.toPage('H_Reserve');
                            },
                            'CallBackCancel': function () {
                                me.toPage('H_OrderList');
                            }
                        });

                        me.setParams("selCity", null);
                        me.setParams("selStore", null);

                    } else {
                        Jit.UI.Dialog({
                            'content': data.description,
                            'type': 'Alert',
                            'CallBackOk': function () {
                                Jit.UI.Dialog('CLOSE');
                            }
                        });
                        return false;
                    }
                }
            });
        }
    },
    formatDate: function (day) {
        var Year = 0;
        var Month = 0;
        var Day = 0;
        var CurrentDate = "";
        Year = day.getFullYear(); //ie火狐下都可以 
        Month = day.getMonth() + 1;
        Day = day.getDate();
        CurrentDate += Year + "-";
        if (Month >= 10) {
            CurrentDate += Month + "-";
        }
        else {
            CurrentDate += "0" + Month + "-";
        }
        if (Day >= 10) {
            CurrentDate += Day;
        }
        else {
            CurrentDate += "0" + Day;
        }
        return CurrentDate;
    },
    fnSendMsg: function (data) {
        var me = this;
        var OrderId=data;
        //数据请求
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getOrderList',
                'orderId': data,
                'page': 1,
                'pageSize': 10000
            },
            success: function (data) {
                if (data.code == 200) {
                    if (data.content.orderList.length > 0) {

                        //                        me.ajax({
                        //                            url: '/CustomerService/Data.aspx',
                        //                            data: {
                        //                                'action': 'sendMessage',
                        //                                'isCS': '1',
                        //                                'csPipelineId': '2',
                        //                                'messageContent': '亲爱的' + $("#txtUserName").val() + '您好，感谢您选择花间堂！您的订单' + data.content.orderList[0].orderCode + '已收到，正在玩命确认中。您可以进入个人中心页面随时关注订单状态，如有疑问请致电4000 767 123。',
                        //                                'messageId': '',
                        //                                'serviceTypeId': null,
                        //                                'objectId': null,
                        //                                'sign': '花间堂',
                        //                                'mobileNo': $("#txtPhone").val()
                        //                            }
                        //                        });
                        me.ajax({
                            url: '/ApplicationInterface/Order/OrderGateway.ashx',
                            data: {
                                'action': 'SendWeixinMessage',
                                'OrderId': OrderId,
                            }
                        });

                        me.ajax({
                            url: '/OnlineShopping/data/Data.aspx',
                            data: {
                                'action': 'sendMail',
                                'type':'submit',
                                'UserName': $("#txtUserName").val(),
                                'Mobile': $("#txtPhone").val(),
                                'StoreName': __data.storeInfo.storeName,
                                'RoomName': __data.itemName,
                                'OrderNo': data.content.orderList[0].orderCode,
                                'OrderDate': me.getParams('InDate') + ' 至 ' + me.getParams('OutDate')
                            }
                        });
                    }
                }
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