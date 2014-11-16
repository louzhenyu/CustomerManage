Jit.AM.defindPage({

	name:'HitEggs',  //砸金蛋名称
	onPageLoad:function(){
		this.count=1;   //砸金蛋的次数
		this.info={};
		this.info.customerId=Jit.AM.getUrlParam("customerId");
		this.info.userId=Jit.AM.getUrlParam("userId");
		this.info.openId=Jit.AM.getUrlParam("openId");
		this.info.eventId=Jit.AM.getUrlParam("EventId");
		//当页面加载完成时触发
		Jit.log('进入Record.....');
		//重写alert
		window.alert = function(str){
			var d7 = new iDialog();
			d7.open({
				classList: "alert",
				title:"",
				close:"",
				content:str,
				btns:[
					{id:"", name:"确定", onclick:"fn.call();", fn: function(self){
							self.die();
						}
					}
				]
			});
		}
		this.initEvent();
		this.initData();
	},
	
	initEvent:function(){
		var customerId=Jit.AM.getUrlParam("customerId"),
		    userId=Jit.AM.getUrlParam("userId"),
		    openId=Jit.AM.getUrlParam("openId"),
		    eventId=Jit.AM.getUrlParam("EventId");
		var weixinData={
			customerId:customerId,
			userId:userId,
			openId:openId,
			eventId:eventId
		}
		var self=this;
		//音乐初始化
		this.play("audio");
		$("#playbox").bind("tap",function(){
			//音乐初始化
			self.play("audio");
		});
		//点击砸金蛋
		$("#shape").delegate(".plane","click",function(event){
			if(self.count>=1){
				var $this=$(this);
				$this.find("span").addClass("on");
				$("#hit").addClass("on");
				//通过服务器端进行数据请求   判断是否中奖
				self.getResult(weixinData);
				self.count--;
			}else{
				self.showTips();
			}
		});
	},
	getResult:function(obj){  //获得中奖纪录
		var that=this;
		var datas = {
			'action':'getEventPrizes',
			'eventId':(that.getUrlParam('eventId')||'E5A304D716D14CD2B96560EBD2B6A29C'),
			'recommender':localStorage.getItem('recommenderOpenId')
		}
		this.ajax({
            url: '/Lj/Interface/PrizesData.aspx',  //大转盘接口
            data: datas,
            success: function (data) {
            	
        	}
       });
	},
	initData:function(){   //初始化
		var that=this;
		var jsonarr = { 
			'action': "getEventPrizesBySales", 
			 ReqContent: JSON.stringify({ "common": { customerId: that.info.customerId, openId: that.info.openId, userId: that.info.userId },
			 							  "special": { "Longitude": "0.0", "Latitude": "0.0", eventId: that.info.eventId} }) 
	    };
	    try{
			this.ajax({
		        url: '/OnlineShopping/data/data.aspx',  //大转盘接口
		        data: jsonarr,
		        dataType:"json",
		        type:"get",
		        success: function (data) {
		        	alert(1);
		        	//alert(data.content.isWinning)
		            that.initPrizeParam(data);
		    	},
		    	error:function(arr){
		    		alert(JSON.stringify(arr));
		    		that.initPrizeParam({"content":{isWining:"0"}});
		    	}
		   });
       }catch(ex){
       	alert(ex.toString());
       }
	},
	initPrizeParam:function(data){
		var o=data.content;
		if (o.isWinning == "1") {
            if (o.winningDesc) {
                o.prizeStatus = parseInt(o.winningValue);
                if (o.winningDesc == '一等奖') { //一等奖
                    o.prizeIndex = that.prizes1[getRandom(0, that.prizes1.length - 1)];
                }else if (o.winningDesc == '二等奖') { //二等奖
                    o.prizeIndex = that.prizes2[getRandom(0, that.prizes2.length - 1)];
                }else if (o.winningDesc == '三等奖') { //三等奖
                    o.prizeIndex = that.prizes3[getRandom(0, that.prizes3.length - 1)];
                }else {
                    o.prizeIndex = that.prizes0[getRandom(0, that.prizes0.length - 1)];
                }
            }else {
                o.prizeIndex = getRandom(0, that.prizes0.length - 1);
            }
        }else{
        	$(".color_golden").html(0);
        	this.count=0;  //设置抽奖次数为0
        	this.showTips();
        }
	},
	//播放音乐
	play:function(idString){
		var domObj=$("#"+idString).get(0);
		if(domObj.paused){
			domObj.play();
			if(!$("#playbox").hasClass("on")){
				$("#playbox").addClass("on");
			}
		}else{
			domObj.pause();
			$("#playbox").removeClass("on");
		}
	},
	jg:function(arg){  //结果
		var d3 = new iDialog();
		d3.open({
			classList: "result",
			title:"",
			close:"",
			content:'<div class="header"><h5 style="color:#2f8ae5;font-size:16px;">恭喜您中奖了！您的运气实在是太好了！</h6></div>\
				<table><tr>\
				<td style="width:75px;"><label>'+arg.c_type+'</label></td>\
				<td><img src="'+arg.c_pic+'" /></td>\
				<td style="width:75px;"><label>'+arg.c_name+'</label></td>\
				</tr></table>',
			btns:[
				{id:"", name:"领取奖品", onclick:"fn.call();", fn: function(self){
						self.die();
						self.lq(arg);
					}
				},
				{id:"", name:"关闭", onclick:"fn.call();", fn: function(self){
						location.href = location.href + "&r="+Math.random();
						self.die();
					},
				}]
		});
	},
	showTips:function(arg){   //提示信息
		var d5 = new iDialog();
		d5.open({
			classList: "success",
			title:"",
			close:"",
			content:'<div class="header"><h6>砸金蛋提示</h6></div>\
				<table><tr>\
				<td><img src="http://stc.weimob.com/img/smashegg/7.png" /></td>\
				<td style="width:170px;"><label>您目前有 0 次砸金蛋的机会 <br/>每天只有一次机会哦,请明天再试</label></td>\
				</tr></table>',
			btns:[
				{id:"", name:"知道了", onclick:"fn.call();", fn: function(self){
						self.die();
					}
				}]
		});
	}
});