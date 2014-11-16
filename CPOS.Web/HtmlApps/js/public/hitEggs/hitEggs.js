Jit.AM.defindPage({

	name:'HitEggs',  //砸金蛋名称
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入HitEggs.....');
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
	},
	
	initEvent:function(){
		var self=this;
		//音乐初始化
		this.play("audio");
		$("#playbox").bind("click",function(){
			//音乐初始化
			self.play("audio");
		});
		//点击砸金蛋
		$("#shape").delegate(".plane","click",function(event){
			var $this=$(this);
			$this.find("span").addClass("on");
			$("#hit").addClass("on");
			//通过服务器端进行数据请求   判断是否中奖
			self.getResult();
			//self.count--;
			
		});
	},
	getResult:function(){  //获得中奖纪录
		var that=this;
		var datas = {
			'action':'getEventPrizes',
			'eventId':(that.getUrlParam('eventId')||'E5A304D716D14CD2B96560EBD2B6A29C')
		}
		this.ajax({
            url: '/Lj/Interface/PrizesData.aspx',  //大转盘接口
            data: datas,
            success: function (data) {
            	that.initPrizeParam(data);
        	}
       });
	},
	initPrizeParam:function(data){
		var o=data.content;
		var that=this;
		if (o.isWinning == "1") {
			var arg={};
            if (o.prizes&&o.prizes.length) {
                arg.type=o.prizes[0].prizeName;
                arg.desc=o.prizes[0].prizeDesc;
                 //中奖提示
                that.sucjg(arg);
            }
        }else{
        	//$(".color_golden").html("不限次数");
        	//this.count=0;  //设置抽奖次数为0
        	that.failJg();
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
	//中奖结果
	sucjg:function(arg){  //结果   中奖结果
		var d3 = new iDialog();
		d3.open({
			classList: "result",
			title:"",
			close:"",
			content:'<div class="header"><h5 style="color:#2f8ae5;font-size:16px;">恭喜您中奖了！</h6></div>\
				<table><tr>\
				<td style="width:75px;"><label>'+arg.type+'</label></td>\
				<td><img src="../../../images/public/hitEggs/4.png" /></td>\
				<td style="width:75px;"><label>'+arg.desc+'</label></td>\
				</tr></table>',
			btns:[
				{id:"", name:"领取奖品", onclick:"fn.call();", fn: function(self){
						$(".plane span").removeClass("on");
						$("#hit").removeClass("on");
						self.die();
					}
				},
				{id:"", name:"关闭", onclick:"fn.call();", fn: function(self){
						$(".plane span").removeClass("on");
						$("#hit").removeClass("on");
						self.die();
					},
				}]
		});
	},
	//未中奖提示
	failJg:function(arg){
		var dialog = new iDialog();
		dialog.open({
			classList: "success",
			title:"",
			close:"",
			content:'<div class="header"><h6>砸金蛋提示</h6></div>\
				<table><tr>\
				<td><img src="../../../images/public/hitEggs/7.png" /></td>\
				<td style="width:170px;"><label>非常抱歉!您没有中奖!<br/>请再接再厉哦!<br/>加油!!!</label></td>\
				</tr></table>',
			btns:[
				{id:"", name:"知道了", onclick:"fn.call();", fn: function(self){
						$(".plane span").removeClass("on");
						$("#hit").removeClass("on");
						self.die();
					}
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
				<td><img src="../../../images/public/hitEggs/7.png" /></td>\
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