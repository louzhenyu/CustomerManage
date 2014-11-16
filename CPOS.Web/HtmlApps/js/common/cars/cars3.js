Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
    },
    initEvent:function(){
        var me=this;
    },
    initPage:function(){
    	//effect    random
		// fold
		// fade
		// slideInLeft
		// slideInRight
		// sliceDown
		// sliceDownRight
		// sliceDownLeft
		// sliceUp
		// sliceUpRight
		// sliceUpLeft
		// sliceUpDown
		// sliceUpDownLeft
		// sliceUpDownRight
		// boxRandom
		// boxRain
		// boxRainReverse
		// boxRainGrow
		// boxRainGrowReverse
		 $('#slider').nivoSlider({
		 	effect: 'boxRain,boxRainReverse,boxRainGrow,boxRainGrowReverse', // 过渡效果  
		    slices: 15, //effect为切片效果时的数量  
		    boxCols: 8, //effect为格子效果时的列  
		    boxRows: 4, //effect为格子效果时的行  
		    animSpeed: 500, //动画速度  
		    pauseTime: 3000, //图片切换速度  
		    startSlide: 0, //从第几张开始  
		    directionNav: true, //是否显示图片切换按钮(上/下页)  
		    directionNavHide: false, //是否鼠标经过才显示  
		    controlNav: true, // 显示序列导航  
		    controlNavThumbs: false, // 显示图片导航  
		    controlNavThumbsFromRel: false, // 使用img的rel属性作为缩略图地址  
		    //controlNavThumbsSearch: '.jpg', // 查找特定字符串(controlNavThumbs必须为true)  
		    //controlNavThumbsReplace: '_thumb.jpg', // 替换成这个字符(controlNavThumbs必须为true)  
		    //keyboardNav: true, // 键盘控制（左右箭头）PS:建议把代码中的keypress换成keydown,因为在Chrome下有兼容问题.  
		    pauseOnHover: true, // 鼠标经过时暂停播放  
		    manualAdvance: false, // 是否手动播放(false为自动播放幻灯片)  
		    captionOpacity: 0.1, // 字幕透明度  
		    prevText: 'Prev', 
		    nextText: 'Next', 
		    randomStart: false, //是否随机图片开始  
		    beforeChange: function(){}, //动画开始前触发  
		    afterChange: function(){}, //动画结束后触发  
		    slideshowEnd: function(){}, // 本轮循环结束触发  
		    lastSlide: function(){}, // 最后一张图片播放结束触发  
		    afterLoad: function(){} // 加载完毕时触发  
		 });
		 var wrap=$("#frame").parent();
		 var options = {
		 		horizontal: 1,
				itemNav: 'basic',
				//moveBy:step,     //步长
				smartAt:0,
				scrollBy: 200,
				speed: 200,
				easing: 'easeOutQuart',
				scrollBar: wrap.find(".scrollbar"),
				dynamicHandle: 1,
				dragHandle: 1,
				clickBar: 0,
				mouseDragging: 1,
				touchDragging: 1,
				releaseSwing: 1,
				startPaused:true
			};
			//$("#items").css({height:""});
			var frame=$("#frame");
			var fra = new Sly(frame, options);
			// Initiate frame
			fra.init();
			//playbox.init("playbox");
			
			$(".ul_list").fadeOut().fadeIn();
			//另外一个特效
			var first = alice.init();
			first.slide({
			    elems: "#first",
			    move: "up",
			    duration: {
			        "value": "1000ms",
			        "randomness": "30%",
			        "offset": "150ms"
			    }
			});
			var second = alice.init();
			second.slide({
			    elems: "#second",
			    move: "up",
			    duration: {
			        "value": "1000ms",
			        "randomness": "40%",
			        "offset": "150ms"
			    }
			});
			var third = alice.init();
			third.slide({
			    elems: "#third",
			    move: "up",
			    duration: {
			        "value": "1000ms",
			        "randomness": "50%",
			        "offset": "150ms"
			    }
			});
			
			var fourth = alice.init();
			fourth.slide({
			    elems: "#fourth",
			    move: "up",
			    duration: {
			        "value": "1000ms",
			        "randomness": "60%",
			        "offset": "150ms"
			    }
			});
			alice.init().slide({
			    elems: "#PathMenu",
			    move: "left",
			    overshoot:15,
			    randomness:15,
			    timing:'easeOutQuad',
			    duration: {
			        "value": "1000ms",
			        "randomness": "60%",
			        "offset": "150ms"
			    }
			});
			
			
		var btn = document.getElementById("plug-btn");
				btn.onclick = function(){
					var divs = document.getElementById("plug-phone").querySelectorAll("div");
					var className = className=this.checked?"on":"";
					for(i = 0;i<divs.length; i++){
						divs[i].className = className;
					}
					
				}
			
    }
});
