var canvas = {'temp':null, 'draw':null}; 
var mouseDown = false;
function supportsCanvas() {
	return !!document.createElement('canvas').getContext;
}

function getEventCoords(ev) {
	var first, coords = {};
	
	if (ev.targetTouches!=undefined && ev.targetTouches.length == 1) { 
		first = event.targetTouches[0];
		coords.pageX = first.pageX;
		coords.pageY = first.pageY;
	} else {
		coords.pageX = ev.pageX;
		coords.pageY = ev.pageY;
	}
	var getOffset= $("#maincanvas").offset();
	if(isWinner == false && (coords.pageX-getOffset.left) > 40 && (coords.pageX-getOffset.left)< 90){
					isWinner = true; 
					ggj.Winner();
		}
	return coords;
}


function getLocalCoords(elem, coords) {
	var ox = 0, oy = 0;

	while (elem != null) {
		ox += elem.offsetLeft;
		oy += elem.offsetTop;
		elem = elem.offsetParent;
	}

	return { 'x': coords.pageX - ox, 'y': coords.pageY - oy };
}


function recompositeCanvases() {
	var main = $('#maincanvas').get(0);
	var tempctx = canvas.temp.getContext('2d');
	var mainctx = main.getContext('2d');
	canvas.temp.width = canvas.temp.width; 
	tempctx.drawImage(canvas.draw, 0, 0);
	tempctx.globalCompositeOperation = 'source-atop';
	tempctx.drawImage(image.back.img, 0, 0);
	mainctx.drawImage(image.front.img, 0, 0);
	tempctx.font="16px '微软雅黑'";
	tempctx.fillText(prize,15,50);
	mainctx.drawImage(canvas.temp, 0, 0);	
}

function scratchLine(can, x, y, fresh) {
	var ctx = can.getContext('2d');
	ctx.lineWidth = 18;
	ctx.lineCap = ctx.lineJoin = 'round';
	ctx.strokeStyle = '#ddd'; 
	ctx.globalAlpha= 1; 
	if (fresh) {
		ctx.beginPath();
		ctx.moveTo(x+0.01, y);
	}
	ctx.lineTo(x, y);
	ctx.stroke();
}


function setupCanvases() {
	var c = $('#maincanvas').get(0);
	c.width = 140;
	c.height = 80; 
	canvas.temp = document.createElement('canvas');
	canvas.draw = document.createElement('canvas');
	canvas.temp.width = canvas.draw.width = c.width;
	canvas.temp.height = canvas.draw.height = c.height;
	recompositeCanvases();
	function mousedown_handler(e) {
		$("#PcBox").css({"height":$(window).height(),"overflow":"hidden"});
		var local = getLocalCoords(c, getEventCoords(e));
		mouseDown = true;
		scratchLine(canvas.draw, local.x, local.y, true);
		recompositeCanvases();
		
		return false;
	};

	function mousemove_handler(e) {
$("#PcBox").css({"height":$(window).height(),"overflow":"hidden"});
		if (!mouseDown) { return true; }

		var local = getLocalCoords(c, getEventCoords(e));
		
		scratchLine(canvas.draw, local.x, local.y, false);
		recompositeCanvases();

		return false;
	};
	function mouseup_handler(e) {
		$("#PcBox").css({"height":"auto","overflow":"auto"});
		if (mouseDown) {
			mouseDown = false;
			return false;
		}

		return true;
	};

			document.getElementById('maincanvas').ontouchstart= function(event){
					
		    if (event && event.preventDefault) {
        		 event.preventDefault();  
     		}  
				
				mousedown_handler(event)};
			document.getElementById('maincanvas').ontouchmove= function(event){
					
		    if (event && event.preventDefault) {
        		 event.preventDefault();  
     		}  
				mousemove_handler(event)};
			document.getElementById('maincanvas').ontouchend= function(event){
					
		    if (event && event.preventDefault) {
        		 event.preventDefault();  
     		}  
				mouseup_handler()};
		
}



function loadImages() {
	var loadCount = 0;
	var loadTotal = 0;
	var loadingIndicator;
	
	function imageLoaded(e) {
		loadCount++;
		if (loadCount >= loadTotal) {
			setupCanvases();
		}
	}
	
	for (k in image) if (image.hasOwnProperty(k))
		loadTotal++;
	for (k in image) if (image.hasOwnProperty(k)) {
		image[k].img = document.createElement('img'); // image is global
		$(image[k].img).load(imageLoaded);
		image[k].img.src = image[k].url;
	}
}

function loadcanvas(){
	if (supportsCanvas()) {
		loadImages();
	}
}