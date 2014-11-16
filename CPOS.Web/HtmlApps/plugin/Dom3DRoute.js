(function() {
	var ImgV = {
		domList: null,
		//沈马石  通过3D进行浏览
		view3D:{
			options3d:{},  //存储3d浏览的一些参数
			//浏览下一个视图
			_nextView:function(){
				if( this.options3d.hasPerspective ) {
					this.options3d.$teTransition.addClass('te-show');
					this.options3d.$teCover.addClass('te-hide');
					if( $.inArray( this.options3d.type, this.options3d.wPerspective ) !== -1 ) {
						this.options3d.$teWrapper.addClass('te-perspective');
					}
				}
				this._updateImages();
			},
			_updateImages:function(){
				var $back 	= this.options3d.$teTransition.find('div.te-back'),
					$front	= this.options3d.$teTransition.find('div.te-front');
				var currentImg=this.options3d.currentImg,
					imagesCount=this.options3d.imagesCount,
					last_img=0,
					$teImages=this.options3d.$teImages;
				if(this.options3d.direction=="up"){
					//初始化的时候默认为0   滑动到最底部应该切换到0  所以重置为-1
					if(this.options3d.currentImg==imagesCount-1){
						currentImg=-1;
					}
					( this.options3d.currentImg === imagesCount - 1 ) ? 
					( last_img = imagesCount - 1, currentImg = 0 ) : 
					( last_img = currentImg, ++currentImg );
				}else if(this.options3d.direction=="down"){
					( this.options3d.currentImg === 0 ) ? 
					( last_img = 0, currentImg = imagesCount-1 ) : 
					( last_img = currentImg, --currentImg );
				}
				console.log(imagesCount+"=="+currentImg);
				this.options3d.currentImg=currentImg;
				var $last_img 	= $teImages.eq( last_img ),
					$currentImg	= $teImages.eq( currentImg );
				
				// $front.empty().append('<img src="' + $last_img.attr('src') + '">');
				// $back.empty().append('<img src="' + $currentImg.attr('src') + '">');

				$front.empty().append($last_img);
				$back.empty().append($currentImg);

				
				// this.options3d.$teCover.find('img').attr( 'src', $currentImg.attr('src') );

					this.options3d.$teCover.html($currentImg);

			},
			_init:function(str){
				var parent=$(str);
				
				ImgV.domList = [];
				ImgV.winH = window.innerHeight;
				ImgV.winW = window.innerWidth;
				ImgV.panel = $('<div id="te-Wrapper"></div>').appendTo('body').css({
					'top': 0,
					'left': 0,
					'width': ImgV.winW + 'px',
					'height': ImgV.winH + 'px',
					'over-flow': 'hidden',
					'position': 'fixed'
				});
				var newdom, viewitem;
				var domList=parent.children();
				for (var i=0;i<domList.length;i++) {
					newdom = $($(domList[i]).clone());
					viewitem = $('<div class="te-images" style="background-color:#333333; position:absolute;left:0;top:' + i * ImgV.winH + 'px"></div>');
					var curWindow = $(window);
					viewitem.height(curWindow.height());
					newdom.appendTo(viewitem);
					viewitem.appendTo(ImgV.panel);
					ImgV.domList.push(viewitem);
					$(domList[i]).hide();
				}
				ImgV.selection = 0;
				var $teWrapper		= $("#te-Wrapper");
				$teWrapper.append('<div class="te-cover" style="width:100%;height:100%;">\
					</div>\
					<div class="te-transition">\
						<div class="te-cube-front te-cube-face te-front"></div>\
						<div class="te-cube-top te-cube-face te-back"></div>\
						<div class="te-cube-bottom te-cube-face te-back"></div>\
						<div class="te-cube-right te-cube-face te-back"></div>\
						<div class="te-cube-left te-cube-face te-back"></div>\
					</div>');

				var firstImage=$teWrapper.find('div.te-images>div')[0];
				
				
				$teWrapper.addClass("te-Wrapper");
				$teWrapper.css({overflow:"visible"});
				var $teCover		= $teWrapper.find('div.te-cover');
				$teCover.append($(firstImage).clone());
				var $teImages		= $teWrapper.find('div.te-images>div');
				var imagesCount		= $teImages.length;
				var currentImg		= 0;
				var $navNext		= $('#te-next');
				var $type			= $('#type');
				var type			= "te-cube2";
				var $teTransition	= $teWrapper.find('.te-transition');
				// requires perspective
				var wPerspective	= [ 'te-flip1', 'te-flip2', 'te-flip3', 'te-flip4', 
									'te-rotation1', 'te-rotation2', 'te-rotation3', 'te-rotation4', 'te-rotation5', 'te-rotation6',
									'te-multiflip1', 'te-multiflip2', 'te-multiflip3', 
									'te-cube1', 'te-cube2', 'te-cube3', 'te-cube4',
									'te-unfold1', 'te-unfold2'];
				var animated		= false;
				// check for support
				var hasPerspective	=true; //Modernizr.csstransforms3d;
				$teTransition.addClass( type );
				this.options3d={
					"$teWrapper":$teWrapper,
					"$teCover":$teCover,
					"$teImages":$teImages,
					"imagesCount":imagesCount,
					"currentImg":currentImg,
					"$navNext":$navNext,
					"hasPerspective":hasPerspective,
					"type":type,
					"$teTransition":$teTransition,
					"wPerspective":wPerspective,
					"$teWrapper":$teWrapper
				}
				var me=this;
				$navNext.on( 'click', function( event ) {
					
					if( hasPerspective && animated ){
						return false;
					}else{
						animated = true;	
						//下一个视图
						me._nextView();
						return false;
					}
				});
				if( hasPerspective ) {
					$teWrapper.on({
						'webkitAnimationStart' : function( event ) {
							$type.prop( 'disabled', true );
						},
						'webkitAnimationEnd'   : function( event ) {
							if( ( type === 'te-unfold1' && event.originalEvent.animationName !== 'unfold1_3Back' ) ||
								( type === 'te-unfold2' && event.originalEvent.animationName !== 'unfold2_3Back' ) )
								return false;
							$teCover.removeClass('te-hide');
							if( $.inArray( type, wPerspective ) !== -1 )
								$teWrapper.removeClass('te-perspective');
							$teTransition.removeClass('te-show');
							animated = false;
							$type.prop( 'disabled', false );
						}
					});
				}
				ImgV.initEvent();
			}
		},
		//基于小程的代码进行修改
		init: function(domList) {
			var me = this;
			//另外一种页面滑动效果
			if(typeof domList==="string"){
				this.viewType="3d";
				this.view3D._init(domList);
			}else if(typeof domList==="array"){
				me.domList = [];
				me.winH = window.innerHeight;
				me.winW = window.innerWidth;
				console.log('me.winH '+ me.winH);
				me.panel = $('<div name="domView"></div>').appendTo('body').css({
					'top': 0,
					'left': 0,
					'width': me.winW + 'px',
					'height': me.winH + 'px',
					'over-flow': 'hidden',
					'position': 'fixed'
				});
				var newdom, viewitem;
				for (var i in domList) {
					newdom = $($(domList[i]).clone());
					viewitem = $('<div style="background-color:#333333; position:absolute;left:0;top:' + i * me.winH + 'px"></div>');
					var curWindow = $(window);
					viewitem.height(curWindow.height());
					newdom.appendTo(viewitem);
					viewitem.appendTo(me.panel);
					me.domList.push(viewitem);
					$(domList[i]).hide();
				}
				me.selection = 0;
				me.initEvent();
			}
		},
		state: {
			touchStartY: null,
			touchEndY: null,
			press: false,
			pressTime: null
		},
		handleDom: null,
		initEvent: function() {
			var me = this;
			me.panel.bind('touchstart', this.touchStart);
			me.panel.bind('touchmove', this.touchMove);
			me.panel.bind('touchend', this.touchEnd);
			var curBody=$('body');
						curBody.bind('touchmove', this.stopEvent);


		},
		Lock: false,
		LockAnima:false,
		MoveDomToScene: function(item, tob, isMoved) {
			if (ImgV.Lock) {
				return;
			}
			ImgV.LockAnima = true;
			$(item).css({
				'z-index': 3,
			});
			$(item).animate({
				'top': 0,
			}, function() {
				$(ImgV.domList[ImgV.selection]).css({
					'z-index': 1
				});
				if (tob == 'top') {
					ImgV.selection--;
				} else if (tob == 'bottom') {
					ImgV.selection++;
				}
				$(item).css({
					'z-index': 2
				});
				ImgV.LockAnima = false;
			});
		},
		touchStart: function(evt) {
			if (ImgV.Lock || ImgV.LockAnima) {
				return;
			}
			ImgV.Lock = true;
			var me = ImgV;
			me.state.pressTime = (new Date()).getTime();
			//兼容jquery触摸事件
			var pageY=evt.touches?evt.touches[0].pageY:evt.originalEvent.touches[0].pageY;
			me.state.touchStartY = pageY;
			// ImgV.stopEvent(evt);
		},
		touchMove: function(evt) {
			if (!ImgV.Lock || ImgV.LockAnima) {
				return;
			}
			var me = ImgV,
				selection;
			//兼容jquery触摸事件
			var pageY=evt.changedTouches?evt.changedTouches[0].pageY:evt.originalEvent.changedTouches[0].pageY;
			var pageY = pageY;
			selection = ImgV.selection;
			if (pageY > me.state.touchStartY) {
				
				//向下滑
				if (selection >= 1) {
					ImgV.handleDom = ImgV.domList[selection - 1];
					ImgV.handleDom.css({
						'z-index': 3,
						'top': -ImgV.winH + pageY - me.state.touchStartY + 'px'
					});
				}
			} else {
				//console.log(pageY,me.state.touchStartY,selection<ImgV.domList.length);
				if (selection < ImgV.domList.length - 1) {
					ImgV.handleDom = ImgV.domList[selection + 1];
					ImgV.handleDom.css({
						'z-index': 3,
						'top': ImgV.winH - (me.state.touchStartY - pageY) + 'px'
					});
				}
			}
			//console.log(evt.changedTouches[0].pageY);
		},
		touchEnd: function(evt) {
			var me = ImgV,
				nowtime = (new Date()).getTime();
			if (!ImgV.Lock || ImgV.LockAnima) {
				return;
			}
			ImgV.Lock = false;
			//var pageY = evt.changedTouches[0].pageY;
			//兼容jquery触摸事件
			var pageY=evt.changedTouches?evt.changedTouches[0].pageY:evt.originalEvent.changedTouches[0].pageY;
			if (nowtime - me.state.pressTime < 200) {
				//快速滑动
				if (me.state.touchStartY - pageY > 10 ) {
					if(me.viewType=="3d"){
						me.view3D.options3d.type="te-cube2";
						me.view3D.options3d.direction="up";   //向下翻转
						me.view3D.options3d.$teTransition.removeClass().addClass('te-transition').addClass(me.view3D.options3d.type);
						me.view3D._nextView();
					}
					if(ImgV.selection <= ImgV.domList.length - 1){
						//向上滑
						ImgV.handleDom = ImgV.domList[ImgV.selection + 1];
						ImgV.MoveDomToScene(ImgV.handleDom, 'bottom');
					}
				} else if (pageY - me.state.touchStartY > 10) {
					if(me.viewType=="3d"){
						me.view3D.options3d.type="te-cube1";   //3d向下翻转
						me.view3D.options3d.direction="down";   //向下翻转
						me.view3D.options3d.$teTransition.removeClass().addClass('te-transition').addClass(me.view3D.options3d.type);
						me.view3D._nextView();
					}
					if(ImgV.selection >= 1){
						//向下滑
						ImgV.handleDom = ImgV.domList[ImgV.selection - 1];
						ImgV.MoveDomToScene(ImgV.handleDom, 'top');
					}
				} else {
					if (me.state.touchStartY > pageY && ImgV.selection < ImgV.domList.length - 1) {
						ImgV.reBackDom();
					} else if (me.state.touchStartY < pageY && ImgV.selection >= 1) {
						ImgV.reBackDom();
					}
				}
			} else if (nowtime - me.state.pressTime > 200) {
				if (me.state.touchStartY - pageY > 100 && ImgV.selection < ImgV.domList.length - 1) {
					//向上滑
					ImgV.MoveDomToScene(ImgV.handleDom, 'bottom');
				} else if (pageY - me.state.touchStartY > 100 && ImgV.selection >= 1) {
					//向下滑
					ImgV.MoveDomToScene(ImgV.handleDom, 'top');
				} else {
					if (me.state.touchStartY > pageY && ImgV.selection < ImgV.domList.length - 1) {
						ImgV.reBackDom();
					} else if (me.state.touchStartY < pageY && ImgV.selection >= 1) {
						ImgV.reBackDom();
					}
				}
			}
			// ImgV.stopEvent(evt);
		},
		reBackDom: function() {
			$(ImgV.handleDom).css({
				'top': -(ImgV.winH+60) + 'px',
				'z-index':1
			});
		},
		stopEvent: function(e) {
			var evt = e || window.event;
			evt.stopPropagation ? evt.stopPropagation() : (evt.cancelBubble = true);
			evt.preventDefault ? evt.preventDefault() : null;
		}
	};
	Jit.UI.DomView = ImgV;
})();