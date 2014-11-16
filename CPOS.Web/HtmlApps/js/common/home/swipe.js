window.requestAFrame = (function () {
        return window.requestAnimationFrame ||
                window.webkitRequestAnimationFrame ||
                function (callback) {
                    return window.setTimeout(callback, 1000 / 60); // shoot for 60 fps
                };
    })();
window.cancelAFrame = (function () {
    return window.cancelAnimationFrame ||
            window.webkitCancelAnimationFrame ||
            function (id) {
                window.clearTimeout(id);
            };
})();


function pv_trans_get(o,t){
	var ystrans = o.style[css3Transform].split(')');
	for(var i=0;i<ystrans.length;i++){
		if(ystrans[i].match(t)){
			return $.trim(ystrans[i].replace(t,'').replace('(',''));
		}
	}
	return null;
}
function pv_trans_set(o,t,v,tm){
	var ystrans = o.style[css3Transform].split(')');
	var ntrm = '';
	var hsaset = false;
	for(var i=0;i<ystrans.length;i++){
		var ys = $.trim(ystrans[i]);
		if(ys!=''){
			if(ys.match(t)){
				ntrm += t+'('+v+') ';
				hsaset = true;
			}else{
				ntrm += ys+') ';
			}
		}		
	}
	if(!hsaset){
		ntrm += t+'('+v+') ';
	}
	if(tm){
		o.style[css3TransitionDuration] = tm+'ms';
		setTimeout(function(){
			o.style[css3TransitionDuration] = '0ms';
		},tm);
	}
	o.style[css3Transform] = ntrm;
	
}
function pv_transscrollY(jo,js,ld){
	var scy = -ld*jo.data('rellistdatabl');
	js[0].style[css3Transform] = 'translate(0px,'+scy+ 'px) translateZ(0)';
}
function pv_transmoveY(o,y){
	o.style[css3Transform] = 'translate(0px,'+y+ 'px) translateZ(0)';
}
function pv_transmoveX(o,x){
	o.style[css3Transform] = 'translate('+x+'px,0px) translateZ(0)';
}
function pv_transmoveXY(o,x,y){
	o.style[css3Transform] = 'translate('+x+'px,'+y+ 'px) translateZ(0)';
}
function pv_transmovetimeY(o,y,t,cb){
	window.istaping = true;
	o.style[css3TransitionDuration] = t+'ms';
	o.style[css3Transform] = 'translate(0px,'+y+ 'px) translateZ(0)';
	setTimeout(function(){
		o.style[css3TransitionDuration] = '0ms';
		$(o).data('rellistdata',y);
		if(cb){
			cb();
		}
		window.istaping = false;
	},t);
}
function pv_transmovetimeX(o,x,t,cb){
	window.istaping = true;
	o.style[css3TransitionDuration] = t+'ms';
	o.style[css3Transform] = 'translate('+x+ 'px,0px) translateZ(0)';
	setTimeout(function(){
		o.style[css3TransitionDuration] = '0ms';
		$(o).data('relswipedata',x);
		if(cb){
			cb();
		}
		window.istaping = false;
	},t);
}
function pv_transmovetimeXY(o,x,y,t,cb){
	o.style[css3TransitionDuration] = t+'ms';
	o.style[css3Transform] = 'translate('+x+'px,'+y+'px) translateZ(0)';
	setTimeout(function(){
		o.style[css3TransitionDuration] = '0ms';
		$(o).data('relswipedata',x);
		$(o).data('rellistdata',y);
		if(cb){
			cb();
		}
	},t);
}
function pv_transmonmentumX(o,v){
	if(window.touching){
		return;
	}
	var qs =parseInt(v/6);
	if(v<-3){
		v++;
	}else if(v>3){
		v--;
	}else{
		return;
	}
	var xcd = $(o).data('relswipedata')+qs;
	if(xcd<$(o).data('relswipedatamin')){
		xcd = $(o).data('relswipedatamin');
		pv_transmovetimeX(o,xcd,150);
		$(o).data('relswipedata',xcd);
	}else if(xcd>$(o).data('relswipedatamax')){
		xcd = $(o).data('relswipedatamax');
		pv_transmovetimeX(o,xcd,150);
		$(o).data('relswipedata',xcd);
	}else{
		o.style[css3Transform] = 'translate('+xcd+'px,0px) translateZ(0)';
		$(o).data('relswipedata',xcd);
		window.requestAFrame(function(time){
			pv_transmonmentumX(o,v);
		});
	}
}


function pv_transmonmentumY(o,v,thescroll){
	if(window.touching){
		return;
	}
	var qs =parseInt(v/6);
	if(v<-3){
		v++;
	}else if(v>3){
		v--;
	}else{
		if(thescroll){
			thescroll.animate({opacity:0}, 1000);
		}
		return;
	}
	var xcd = $(o).data('rellistdata')+qs;
	if(xcd<$(o).data('rellistdatamin')){
		xcd = $(o).data('rellistdatamin');
		o.style[css3Transform] = 'translate(0px,'+xcd+ 'px) translateZ(0)';
		$(o).data('rellistdata',xcd);
		if(thescroll){
			pv_transscrollY($(o),thescroll,xcd);
			thescroll.animate({opacity:0}, 1000);
		}
	}else if(xcd>$(o).data('rellistdatamax')){
		xcd = $(o).data('rellistdatamax');
		o.style[css3Transform] = 'translate(0px,'+xcd+ 'px) translateZ(0)';
		$(o).data('rellistdata',xcd);
		if(thescroll){
			pv_transscrollY($(o),thescroll,xcd);
			thescroll.animate({opacity:0}, 1000);
		}
	}else{
		o.style[css3Transform] = 'translate(0px,'+xcd+ 'px) translateZ(0)';
		if(thescroll){
			pv_transscrollY($(o),thescroll,xcd);
		}
		$(o).data('rellistdata',xcd);
		window.requestAFrame(function(time){
			pv_transmonmentumY(o,v,thescroll);
		});
	}
}

(function($){
    $.fn.extend({
        qtouch: function() {
            this.each(function(){
                if($(this).data('qt_bind')){
                	return;
                }
                $(this).on('touchstart',function(evt){
                	if(!window.qt_ee){
                		window.qt_ee = $(this);
                	}                	
                });
                $(this).data('qt_bind',true);                
            });
            return this;
        },
        qmove: function() {
            this.each(function(){
                if($(this).data('qm_bind')){
                	return;
                }
                $(this).on('touchstart',function(evt){
                	if(!window.qt_el){
                		window.qt_el = $(this);
                	}
                });
                $(this).data('qm_bind',true);
            });
            return this;
        },
        qtrans: function() {
            this.each(function(){
                if($(this).data('qs_bind')){
                	return;
                }                
                $(this).on('touchmove',function(evt){
                	var tcs = evt.originalEvent.touches;
                	if(tcs.length==2){
                		var ev1 = tcs[0];
                		var ev2 = tcs[1];
            			var xc = ev1.clientX-ev2.clientX;
            			var yc = ev1.clientY-ev2.clientY;
            			var cd = Math.sqrt(xc*xc+yc*yc);
                		if(!$(this).data('qtrrans')){
                			$(this).data('qtrrans',cd);
                			$(this).trigger('transstart');
                		}else{
                			var jbcz = $(this).data('qtrrans');
                			$(this).trigger('trans',cd/jbcz);
                		}
                	}
                }).on('touchend',function(evt){
                	$(this).data('qtrrans',null);
                	$(this).trigger('transend');
                });
                $(this).data('qs_bind',true);
            });
            return this;
        },
        qscroll: function(m) {
        	if(!m)m={};
            this.each(function(){
            	var mainjobj = $(this);
            	var mbody = mainjobj.children();
            	mainjobj.append('<div class="allscrollbar"></div>');
            	var thescroll = mainjobj.find('.allscrollbar');
            	
            	//mbody.css('position','relative');
            	var mindata = mainjobj.height()-mbody.height();
    			mbody.data('rellistdatamin',mindata);
    			if(mindata>0){
					mbody.height(mainjobj.height());
					mbody.data('rellistdatamin',0); 
				}
            	if(m.reload){
            		pv_transmoveY(mbody[0],mbody.data('rellistdatamax')); 
            		mbody.data('rellistdata',mbody.data('rellistdatamax'));
            		return; 
            	}    			
    			var pullDownEl = null;
    			if(m.downflush){
    				mbody.data('rellistdatamax',-65);
    				mbody.prepend('<div id="pullDown" rel="pullDown"><span class="pullDownIcon"></span><span class="pullDownLabel">下拉可以刷新...</span></div>');
    				pullDownEl = mbody.find('[rel="pullDown"]')[0];
    				mbody.data('rellistdata',-65);
    			}else{
    				mbody.data('rellistdatamax',0);
    				mbody.data('rellistdata',0);
    			}
    			pv_transmoveY(mbody[0],mbody.data('rellistdatamax'));
    			mainjobj.qmove().on('dragupstart dragdownstart',function(e){
    				var mh = mainjobj.height();
    				var sh = mbody.height();
    				var mindata = mh-sh;
    				if(mindata>=0){
    					mbody.height(mainjobj.height());
    					mbody.data('rellistdatamin',0);
    					mbody.data('rellistdatabl',0);
    					//thescroll.hide();
    				}else{
    					mbody.data('rellistdatamin',mindata);
    					mbody.data('rellistdatabl',mh/sh);
    					//设置scroll高度
    					thescroll.height(mbody.data('rellistdatabl')*mh).stop().css('opacity',0.8);
    				}
    				
    				window.rellistdata = mbody.data('rellistdata');
    				e.preventDefault();
    				return false;
    			}).on('dragup dragdown',function(e,msg){
    				
    				var xlcd = window.rellistdata+msg.dy;
    				
    				if(m.downflush){
    					if (xlcd > 30) {
    						if(pullDownEl.className!='flip'){
    							pullDownEl.className = 'flip';
    							pullDownEl.querySelector('.pullDownLabel').innerHTML = '松开进行刷新...';
    						}						
    					}else if(xlcd < 30 && xlcd > -95) {
    						if(pullDownEl.className!=''){
    							pullDownEl.className = '';
    							pullDownEl.querySelector('.pullDownLabel').innerHTML = '下拉可以刷新...';
    						}						
    					}
    				}
    				pv_transscrollY(mbody,thescroll,xlcd);
    				if(xlcd > mbody.data('rellistdatamax')){
    					var yd = xlcd - mbody.data('rellistdatamax');
    					var sxxd = parseInt(yd/4);
    					if(!sxxd){
    						sxxd = 0;
    					}
    					xlcd = mbody.data('rellistdatamax')+sxxd;
    				}else if(xlcd < mbody.data('rellistdatamin')){
    					var yd = mbody.data('rellistdatamin') - xlcd;
    					var sxxd = parseInt(yd/4);
    					if(!sxxd){
    						sxxd = 0;
    					}
    					xlcd = mbody.data('rellistdatamin')-sxxd;
    				}    				
    				pv_transmoveY(mbody[0],xlcd);
    				e.preventDefault();
    				return false;
    			}).on('dragupend dragdownend',function(e,msg){
    				var xlcd = window.rellistdata+msg.dy;
    				if(xlcd > mbody.data('rellistdatamax')){
    					var yd = xlcd - mbody.data('rellistdatamax');
    					var sxxd = parseInt(yd/4);
    					if(!sxxd){
    						sxxd = 0;
    					}
    					xlcd = mbody.data('rellistdatamax')+sxxd;
    				}else if(xlcd < mbody.data('rellistdatamin')){
    					var yd = mbody.data('rellistdatamin') - xlcd;
    					var sxxd = parseInt(yd/4);
    					if(!sxxd){
    						sxxd = 0;
    					}
    					xlcd = mbody.data('rellistdatamin')-sxxd;
    				}
    				mbody.data('rellistdata',xlcd);
    				e.preventDefault();
    				return false;
    			}).on('releaseupend releasedownend',function(e,msg){
    				if(m.downflush && pullDownEl.className.match('flip')){
    					pullDownEl.className = 'load';
    					pullDownEl.querySelector('.pullDownLabel').innerHTML = '信息加载中...';
    					mainjobj.trigger('wintoload');
						pv_transmovetimeY(mbody[0],0,150);
						mbody.data('rellistdata',0);
    					setTimeout(function(){
    						pullDownEl.className = 'loading';
    					},20);						
						return;
    				}else if(mbody.data('rellistdata')>mbody.data('rellistdatamax')){    						
						pv_transmovetimeY(mbody[0],mbody.data('rellistdatamax'),150);
						pv_transscrollY(mbody,thescroll,mbody.data('rellistdatamax'));
						mbody.data('rellistdata',mbody.data('rellistdatamax'));
					}else if(mbody.data('rellistdata')<mbody.data('rellistdatamin')){    						
						pv_transmovetimeY(mbody[0],mbody.data('rellistdatamin'),150);
						pv_transscrollY(mbody,thescroll,mbody.data('rellistdatamin'));
						mbody.data('rellistdata',mbody.data('rellistdatamin'));
					}
    				thescroll.animate({opacity:0}, 1000);
    				e.preventDefault();
    				return false;
    			}).on('swipeup swipedown',function(e,qd){
    				pv_transmonmentumY(mbody[0],parseInt(qd)*2,thescroll);
    				e.preventDefault();
    				return false;
    			});
            });
            return this;
        },
        qflow: function(m) {
        	if(!m)m={};
            this.each(function(){
            	var mainjobj = $(this);
            	var mbody = mainjobj.children();
    			mbody.data('relswipedatamin',mainjobj.width()-mbody.width()); 	
            	if(m.reload){
            		if(mbody.data('relswipedata')>mbody.data('relswipedatamax')){
            			pv_transmoveX(mbody[0],mbody.data('relswipedatamax'));
            		}            		
            		return;            		
            	}    			
            	mbody.data('relswipedatamax',0);
				mbody.data('relswipedata',0);
				pv_transmoveX(mbody[0],mbody.data('relswipedatamax'));
    			mainjobj.qmove().on('touch',function(e){
    				mbody[0].style[css3TransitionDuration] = '0ms';
    				e.preventDefault();
    				return false;
    			}).on('dragleftstart dragrightstart',function(e){
    				window.relswipedata = mbody.data('relswipedata');
    				e.preventDefault();
    				return false;
    			}).on('dragleft dragright',function(e,msg){
    				var xlcd = window.relswipedata+msg.dx;
    				pv_transmoveX(mbody[0],xlcd);   				
    				e.preventDefault();
    				return false;
    			}).on('dragleftend dragrightend',function(e,msg){
    				var realldata = window.relswipedata+msg.dx;
    				mbody.data('relswipedata',realldata);
    				e.preventDefault();
    				return false;
    			}).on('releaseleftend releaserightend',function(e,msg){
    				if(mbody.data('relswipedata')>mbody.data('relswipedatamax')){    						
						pv_transmovetimeX(mbody[0],mbody.data('relswipedatamax'),150);
						mbody.data('relswipedata',mbody.data('relswipedatamax'));
					}else if(mbody.data('relswipedata')<mbody.data('relswipedatamin')){
						pv_transmovetimeX(mbody[0],mbody.data('relswipedatamin'),150);
						mbody.data('relswipedata',mbody.data('relswipedatamin'));
					}
    				e.preventDefault();
    				return false;
    			}).on('swipeleft swiperight',function(e,qd){
    				pv_transmonmentumX(mbody[0],parseInt(qd)*2);
    				e.preventDefault();
    				return false;
    			});
            });
            return this;
        },
        qswipe: function(m) {
        	if(!m)m={};
            this.each(function(){
            	var mainjobj = $(this);
            	var mbody = mainjobj.children();
    			mbody.data('relswipedatamin',mainjobj.width()-mbody.width()); 	
            	if(m.reload){
            		if(mbody.data('relswipedata')>mbody.data('relswipedatamax')){
            			pv_transmoveX(mbody[0],mbody.data('relswipedatamax'));
            		}            		
            		return;            		
            	}
            	var autochange = $.noop;
            	var autocbk = null;
            	mbody.data('relswipedatamax',0);
				mbody.data('relswipedata',0);
            	if(m.stime){
            		autochange = function(fp){
            			if(autocbk){
            				clearInterval(autocbk);
            				autocbk = null;
            			}
            			if(fp){
            				autocbk = setInterval(function(){
            					window.relswipedata = mbody.data('relswipedata');
            					var xlcd = window.relswipedata;            					
                				if(xlcd <= (mbody.data('relswipedatamin')+20)){
                					xlcd = 0;
                					pv_transmovetimeX(mbody[0],xlcd,0);
                					mbody.data('relswipedata',xlcd);
                					mainjobj.trigger('dragok',Math.abs(xlcd/mainjobj.width()));
                				}else{
                					mbody[0].style[css3TransitionDuration] = '0ms';                						
                    				xlcd = xlcd-mainjobj.width();
                    				window.relswipedata = xlcd;
                    				pv_transmovetimeX(mbody[0],xlcd,500);
                    				mbody.data('relswipedata',xlcd);
                    				mainjobj.trigger('dragok',Math.abs(xlcd/mainjobj.width()));
                				}
                				
                			},m.stime);
            			}
            			
            		};
            	}
            	
				pv_transmoveX(mbody[0],mbody.data('relswipedatamax'));
    			mainjobj.qmove().on('touch',function(e){
    				autochange(false);
    				mbody[0].style[css3TransitionDuration] = '0ms';
    				e.preventDefault();
    				return false;
    			}).on('dragleftstart dragrightstart',function(e){
    				window.relswipedata = mbody.data('relswipedata');
    				e.preventDefault();
    				return false;
    			}).on('dragleft dragright',function(e,msg){
    				var xlcd = window.relswipedata+msg.dx;
    				if(xlcd > mbody.data('relswipedatamax')){
    					xlcd = mbody.data('relswipedatamax');
    				}else if(xlcd < mbody.data('relswipedatamin')){
    					xlcd = mbody.data('relswipedatamin');    					
    				}
    				pv_transmoveX(mbody[0],xlcd);   				
    				e.preventDefault();
    				return false;
    			}).on('dragleftend dragrightend',function(e,msg){
    				var realldata = window.relswipedata+msg.dx;
    				var ddwt = mainjobj.width();
    				var mmtime = 150;
    				if(realldata > mbody.data('relswipedatamax')){
    					realldata = mbody.data('relswipedatamax');
    				}else if(realldata < mbody.data('relswipedatamin')){
    					realldata = mbody.data('relswipedatamin');    					
    				}else{    					
    					var qus = Math.abs(realldata%ddwt);
    					if(e.type=='dragleftend'){
    						realldata = realldata - ddwt + qus;
    						mmtime = (ddwt-qus)*2; 
						}else{
							realldata = realldata + qus;
							mmtime = qus*2; 
						}    					
    				}
    				if(mmtime>300){
    					mmtime = 300;
    				}
    				pv_transmovetimeX(mbody[0],realldata,mmtime);
    				mbody.data('relswipedata',realldata);
    				if(realldata <= (mbody.data('relswipedatamin'))){
        				setTimeout(function(){	                					
        					realldata = 0;
            				pv_transmovetimeX(mbody[0],realldata,0);
            				mbody.data('relswipedata',realldata);
            						
        				},mmtime);
    				}
    				$(this).trigger('dragok',Math.abs(realldata/ddwt));
    				$(this).trigger(e.type.replace('end','ok'),Math.abs(realldata/ddwt));
    				e.preventDefault();
    				autochange(true);
    				return false;
    			});
    			autochange(true);
    			
            });
            return this;
        }
    });  
      
})(jQuery);


function windowdragevent(win){
	$(win.document).on('touchstart',function(evt){
		var tcs = evt.originalEvent.touches;
		if(tcs.length>1){
			return;
		}
		win.touching = true;		
		if(win.qt_ee){
			win.qt_ee.trigger('touch');
			win.qt_tt = Date.now();
			win.qt_eest = setTimeout(function(){
        		if(win.qt_tt && Date.now()-win.qt_tt>900){
        			win.qt_ee.trigger('hold');
        			delete win.qt_eest;
        		}
 	    	},1000);
		}
		if(win.qt_el){
			var ev = tcs[0];
			win.qt_el.trigger('touch');			
			win.qt_px = ev.clientX;
			win.qt_py = ev.clientY;
			win.qt_st = Date.now();
		}
    });
	$(win.document).on('touchmove',function(evt){
		
		var tcs = evt.originalEvent.touches;
		var noprevent = false;
		if(tcs.length>1){
			evt.preventDefault();
			return false;
		}
		if(win.qt_ee){
        	if(win.qt_eest){
        		clearTimeout(win.qt_eest);
        		delete win.qt_eest;
        	}
        	delete win.qt_tt;
	    	delete win.qt_ee; 
		}
    	if(win.qt_el){
    		//当左右或上下相差30像素则启动drag
        	var ev = tcs[0];
        	var msg = {};
        	msg.dx = ev.clientX-win.qt_px;
        	msg.dy = ev.clientY-win.qt_py;
        	msg.px = ev.clientX;
        	msg.py = ev.clientY;
        	win.qt_msg = msg;
        	if(win.qt_dragfs){
        		win.qt_el.trigger('drag'+win.qt_dragfs,msg);
        		win.qt_el.trigger('drag',msg);
        	}else{
        		var dragms = null;
            	if(msg.dx > 16){
            		dragms = 'right';
            	}else if(msg.dx < -16){
            		dragms = 'left';
            	}
            	if(msg.dy < msg.dx && msg.dy<-16){
            		dragms = 'up';
            	}else if(msg.dy > msg.dx && msg.dy>16){
            		dragms = 'down';
            	}
            	if(dragms != null){
            		win.qt_px = ev.clientX;
            		win.qt_py = ev.clientY;
            		win.qt_dragfs = dragms;
            		win.qt_el.trigger('dragstart');
            		win.qt_el.trigger('drag'+dragms+'start');
            	}
        	}
        	if(win.qt_dragfs!='up' && win.qt_dragfs!='down'){
            	evt.preventDefault();
        		evt.stopPropagation();
        		return false;
        	}
    	}

    });	
	$(win.document).on('touchend',function(evt){
		win.touching = false;
		if(win.qt_ee){
	    	if(win.qt_eest){
	    		clearTimeout(win.qt_eest);
	    		delete win.qt_eest;
	    	}
	    	win.qt_ee.trigger('release');
	    	if((Date.now() - win.qt_tt) <500){
	    		win.qt_ee.trigger('tap');
	        }else{
	        	win.qt_ee.trigger('holdup');
	        }
	    	delete win.qt_tt;
	    	delete win.qt_ee;
		}
		
		if(win.qt_el){
			win.qt_el.trigger('release');
			if(win.qt_dragfs){
				var msg = win.qt_msg;
				win.qt_el.trigger('dragend',msg);
				win.qt_el.trigger('drag'+win.qt_dragfs+'end',msg);
		    	var sjjg = Date.now() - win.qt_st;	    	
		    	if(sjjg <300){
	        		var qd = 0;
	        		var dragms = null;
	        		if(win.qt_dragfs=='up' || win.qt_dragfs=='down'){
	        			qd = msg.dy*100/sjjg;
	        		}else{
	        			qd = msg.dx*100/sjjg;
	        		}
	        		win.qt_el.trigger('swipe'+win.qt_dragfs,qd);
	        		win.qt_el.trigger('swipe',qd);	    			
		        }else{
		        	win.qt_el.trigger('release'+win.qt_dragfs+'end',msg);
		        }
			}	    	
	    	//清空一些
	    	delete win.qt_px;
	    	delete win.qt_py;
	    	delete win.qt_st;
	    	delete win.qt_dragfs;
	    	delete win.qt_e;
	    	delete win.qt_msg;
	    	delete win.qt_el;
		}
    });
}
$(function(){
	
	$.isie = /msie/.test(navigator.userAgent.toLowerCase());
	window.css3Transform = $.isie ? 'msTtransform':'webkitTransform';
	window.css3TransitionDuration = $.isie ? 'msTransitionDuration':'webkitTransitionDuration';
	windowdragevent(window);
});
