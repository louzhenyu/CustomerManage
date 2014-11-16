Jit.AM.defindPage({
	onPageLoad : function() {
		this.initPage();
	},
	initEvent : function() {
		var me = this;
	},
	initPage : function() {
		var param=this.pageParam;
		var animateDirection="";
		//初始化页面内容
		if(param){
			//设置title
			if(param.title){
				document.title=param.title;
			}
			//设置Logo
			if(param.logo){
				$("#logo").attr("src",param.logo);
			}else{
				$("#logo").hide();
			}
			//背景图
			if(param.backgroundImage){
				$("#backgroundImg").attr("src",param.backgroundImage);
			}
			
			//菜单项

			if(typeof param.links == 'string'){
				//兼容系统生成的config文件 参数为字符串类型
				try{

					param.links = eval('('+param.links+')');

				}catch(e){

					param.links = [];
				}
			}

			var navs=$("#navList li");
			for(var i=0,length=param.links.length;i<length;i++){
				var item=param.links[i];
				if(item.isShow==false||item.isShow=='false'){
					$($(navs[i+1])).get(0).style.visibility="hidden";
					continue;
				}
				var doma=$($(navs[i+1])).find("a");
				if(param.littleImage&&param.littleImage.imgUrl){
					var size=param.littleImage.size?param.littleImage.size:"15px";
					doma.css({
						"background": "url("+param.littleImage.imgUrl+") no-repeat 10px 18px",
						"background-size": size+" "+size
					});
				}
				
				doma.attr("href",item.toUrl);
				//设置中文名称
				$($(navs[i+1]).find("span").get(0)).html(item.title);
				$($(navs[i+1]).find("span").get(1)).html(item.english);
			}
			//动画方向
			animateDirection=param.animateDirection;
			//增加百度直达功能
			if(param.baiduEnter){
				debugger;
				var bdObj=param.baiduEnter[0];
				if(bdObj.baiduScript){
					bdObj.baiduScript=bdObj.baiduScript.replace(/&quot;/g,'"').replace(/&nbsp;/g, " ");//.replace(/\//g,"\\\/");
					$("body").append("<div style='width:100%;position:fixed;bottom:0px;height:35px;' id='baiduScript'></div>");
					
					var iframe = document.createElement("iframe");
					//iframe.setAttribute("src","test.html");
					iframe.src = '';
					iframe.id='bdIframe';
					iframe.style.width = "100%";
					iframe.style.height = "100%";
					$("#baiduScript").append(iframe);
					$("#bdIframe").contents()[0].write(bdObj.baiduScript);
					$("#bdIframe").contents()[0].close();
				}
			}

		}
		var first = alice.init();
		var move={};
		if(animateDirection=="right"){
			move={
				special:true,    //针对源代码进行修改的   为了兼容以前的代码	
				direction:animateDirection?animateDirection:'right',
				start:-(document.body.clientHeight),
				end:0
			};
		}
		if(animateDirection=="down"){
			move={
				special:true,	
				direction:animateDirection?animateDirection:'down',
				start:-(document.body.clientHeight),
				end:0
			};
		}
		if(animateDirection=="up"){
			move="up";
		}
		if(animateDirection=="left"){
			move="left";
		}
		//debugger;
		first.slide({
			elems : ".a_move",
			move : move,
			duration : {
				"value" : "1000ms",
				"randomness" : "30%",
				"offset" : "100ms"
			},
			"overshoot" : "5"
		});
		var logo = alice.init();
		logo.slide({
			"elems" : ".boder",
			"duration" : {
				"value" : "1000ms",
				"randomness" : "20%"
			},
			"timing" : "easeOutQuad",
			"iteration" : "1",
			"direction" : "normal",
			"playstate" : "running",
			"move" : "up",
			"rotate" : "-360%",
			"fade" : "in",
			"scale" : {
				"from" : "10%",
				"to" : "100%"
			},
			"shadow" : "true",
			"overshoot" : 1,
			"perspective" : "none",
			"perspectiveOrigin" : {
				x : "50%",
				y : "50%"
			},
			"backfaceVisibility" : "visible"
		});
		/*
		setTimeout(function() {
			alice.init().cheshire({
				"perspectiveOrigin" : "top",
				"elems" : ".boder",
				"rotate" : 20,
				"overshoot" : 0,
				"duration" : "1000ms",
				"timing" : "ease-in-out",
				"delay" : {
					"value" : "0ms",
					"randomness" : "0%"
				},
				"iteration" : "infinite",
				"direction" : "alternate",
				"playstate" : "running"
			});
		}, 3000);*/
	}
});
