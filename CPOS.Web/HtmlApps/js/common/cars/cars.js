Jit.AM.defindPage({
    onPageLoad: function () {
        this.initPage();
    },
    initEvent:function(){
        var me=this;
    
    	
    },
    initPage:function(){
			var step=$(window).height();
			$("#items").css({height:step+"px!important"});
			var options = {
				itemNav: 'basic',
				//moveBy:step,     //步长
				scrollBy: 200,
				speed: 200,
				easing: 'easeOutQuart',
				scrollBar: null,
				dynamicHandle: 1,
				dragHandle: 1,
				clickBar: 0,
				mouseDragging: 1,
				touchDragging: 1,
				releaseSwing: 1
			};
			
			//$("#items").css({height:""});
			var frame = new Sly('#frame', options);
		
		
			// Initiate frame
			frame.init();
		
			// Reload on resize
			$(window).on('resize', function () {
				frame.reload();
			});
    	
    	
    	
    	/*
    	
			var $frame  = $('#smart');
			var items=$(".m-page");   //所有的要显示的层元素
			var $slidee = $frame.children('ul').eq(0);
			var $wrap   = $frame.parent();
			var step=$(document).height();
			var sly = new Sly($(".p-index"), {
				itemNav: 'basic',
				scrollSource: document,
				dragSource: document,
				moveBy:step,     //步长
				smart: 1,
				activateOn: 'click',
				mouseDragging: 1,
				touchDragging: 1,
				releaseSwing: 1,
				startAt: 3,
				scrollBar: "",
				scrollBy: 1,
				pagesBar: "",
				activatePageOn: 'click',
				speed: 300,
				elasticBounds: 1,
				easing: 'easeOutExpo',
				dragHandle: 1,
				dynamicHandle: 1,
				clickBar: 1,
				move:function(event){
					alert(1);
				},
				// Buttons
				forward: $wrap.find('.forward'),
				backward: $wrap.find('.backward'),
				//prev: $wrap.find('.prev'),
				//next: $wrap.find('.next'),
				prevPage: $wrap.find('.prevPage'),
				nextPage: $wrap.find('.nextPage')
			}, {
			    load: function () {},
			    move: [
			        function () {},
			        function () {}
			    ]
			}).init();
			// Call Sly on frame
			/*$(".p-index").sly({
				itemNav: 'basic',
				moveBy:step,     //步长
				smart: 1,
				activateOn: 'click',
				mouseDragging: 1,
				touchDragging: 1,
				releaseSwing: 1,
				startAt: 3,
				scrollBar: "",
				scrollBy: 1,
				pagesBar: "",
				activatePageOn: 'click',
				speed: 300,
				elasticBounds: 1,
				easing: 'easeOutExpo',
				dragHandle: 1,
				dynamicHandle: 1,
				clickBar: 1,
				move:function(event){
					alert(1);
				},
				// Buttons
				forward: $wrap.find('.forward'),
				backward: $wrap.find('.backward'),
				//prev: $wrap.find('.prev'),
				//next: $wrap.find('.next'),
				prevPage: $wrap.find('.prevPage'),
				nextPage: $wrap.find('.nextPage')
			});*/
    }
});
