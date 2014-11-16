define(function() {
	console.log(2);
	var FCAPP = FCAPP || {};
	FCAPP.HOUSE = FCAPP.HOUSE || {};
	FCAPP.HOUSE.PICSHOW = {
		CONFIG : {},
		RUNTIME : {
			opacity : 0
		},
		init : function() {
			var R = PICSHOW.RUNTIME;
			if (!R.binded) {
				R.binded = true;
				PICSHOW.initElements(R);
				PICSHOW.bindEvents(R);
				R.template1 = FCAPP.Common.escTpl($('#template1').html());
				R.template2 = FCAPP.Common.escTpl($('#template2').html());
				R.template3 = FCAPP.Common.escTpl($('#template3').html());
				R.w = document.documentElement.clientWidth;
				R.h = document.documentElement.clientHeight;
			}
			PICSHOW.loadData();
			var id = '';
			if (window.gQuery && gQuery.id) {
				id = gQuery.id;
			}
			window.sTo = PICSHOW.scrollTo;
			FCAPP.Common.loadShareData(id);
			FCAPP.Common.hideToolbar();
		},
		initElements : function(R) {
			R.scroller = $('#scroller');
			R.scrollList = $('#scrollList');
			R.scrollTips = $('#scrollTips');
			R.scroller1 = $('#scroller1');
			R.scrollPic = $('#scrollPic');
			R.scrollPicLi = $('#scrollPic li');
			R.closeBtn = $('#photoClick');
			R.picName = $('#picName');
			R.popMask = $('#popMask');
			R.scrollWidth = [];
			R.scrollTitle = [];
			R.scrollPagesX = [];
			R.picSize = [];
			R.imgDom = [];
			R.picIdx = 0;
			R.thubIdx = 0;
			R.reduceSize = 0;
			R.loadedThub = {};
			R.view = 'thub';
		},
		bindEvents : function(R) {
			$(window).resize(PICSHOW.resizeLayout);
			R.closeBtn.click(PICSHOW.closeSlidePics);
		},
		loadData : function() {
			window.showPics = PICSHOW.showPics;
			var datafile = window.gQuery && gQuery.id ? gQuery.id + '.' : '', dt = new Date();
			datafile = datafile.replace(/[<>\'\"\/\\&#\?\s\r\n]+/gi, '');
			datafile += 'picshow.js?';
			debugger;
			this.showPics([{"title":"\u6ee8\u6d77\u666f\u89c2","ps1":[{"size":[150,150],"type":"title","title":"\u6ee8\u6d77\u666f\u89c2","subTitle":"Coastal Landscape"},{"size":[260,150],"type":"img","name":"\u6d77\u5cb8\u7ebf","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511358\/0"},{"size":[100,150],"type":"img","name":"\u6d77\u6c34\u666f\u89c2","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511371\/0"},{"size":[200,150],"type":"img","name":"\u7901\u77f3\u666f\u89c2","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511724\/0"}],"ps2":[{"size":[220,150],"type":"img","name":"\u7901\u77f3\u7fa4","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511385\/0"},{"size":[170,150],"type":"text","content":"\u5341\u516c\u91cc\u7ef5\u957f\u6d77\u5cb8\u7ebf\uff0c\u6e7e\u533a\u5185\u6ee8\u6d77\u65c5\u6e38\u8d44\u6e90\u4e30\u5bcc\uff0c\u6c34\u6e05\u6c99\u767d\u3001\u6d77\u5929\u4e00\u8272\uff0c\u5343\u5947\u767e\u602a\u3001\u5f62\u6001\u5404\u5f02\u7684\u7901\u77f3\u7fa4\u5f62\u6210\u4e86\u4e00\u4e2a\u65e0\u6cd5\u590d\u5236\u7684\u5929\u7136\u7901\u77f3\u516c\u56ed\u3002"},{"size":[200,150],"type":"img","name":"\u6d77\u5e95","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511733\/0"},{"size":[120,150],"type":"img","name":"\u6d77\u9e25","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511564\/0"}]},{"title":"\u56ed\u6797\u666f\u89c2","ps1":[{"size":[150,150],"type":"title","title":"\u56ed\u6797\u666f\u89c2","subTitle":"Garden Landscape"},{"size":[260,150],"type":"img","name":"\u56ed\u6797\u666f\u89c2","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511574\/0"},{"size":[100,150],"type":"img","name":"\u77012\u53f7\u7eff\u9053\u666f","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511742\/0"},{"size":[200,150],"type":"img","name":"\u77012\u53f7\u7eff\u9053\u666f","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511808\/0"}],"ps2":[{"size":[220,150],"type":"img","name":"\u6cf3\u6c60\u56ed\u6797","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511580\/0"},{"size":[170,150],"type":"text","content":"5.3\u4e07\u5e73\u65b9\u7c73\u8d85\u5927\u578b\u4e1c\u5357\u4e9a\u98ce\u60c5\u56ed\u6797\uff0c\u5145\u5206\u5c0a\u91cd\u6d77\u666f\u3001\u5c71\u5ce6\u3001\u7901\u5ca9\u3001\u6930\u6797\u7b49\u539f\u751f\u6001\u81ea\u7136\u666f\u89c2\uff0c\u6700\u5927\u5316\u7684\u6253\u9020\u51fa\u6700\u9187\u6b63\u7684\u6d77\u6e7e\u5ea6\u5047\u4eab\u53d7\u3002"},{"size":[200,150],"type":"img","name":"\u7eff\u9053\u4f11\u61a9\u7ad9","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511750\/0"},{"size":[120,150],"type":"img","name":"\u522b\u5885\u56ed\u533a","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511752\/0"}]},{"title":"\u6ee8\u6d77\u5a31\u4e50","ps1":[{"size":[150,150],"type":"title","title":"\u6ee8\u6d77\u5a31\u4e50","subTitle":"Coastal Recreation"},{"size":[260,150],"type":"img","name":"\u65e0\u8fb9\u6e38\u6cf3\u6c60","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511821\/0"},{"size":[100,150],"type":"img","name":"\u6469\u6258\u8247","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511586\/0"}],"ps2":[{"size":[220,150],"type":"img","name":"\u6c99\u6ee9\u6392\u7403","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511590\/0"},{"size":[170,150],"type":"text","content":"\u5927\u578b\u4e13\u9898\u4f53\u9a8c\u5427\u3001\u7279\u8272\u6ee8\u6d77\u670d\u52a1\u7b49\u65d7\u8230\u7ea7\u914d\u5957\u5373\u5c06\u542f\u52a8\uff0c\u4e0b\u73ed\u4e86\u56de\u5230\u5bb6\u4e2d\uff0c\u4e00\u5378\u5168\u8eab\u7684\u75b2\u60eb\uff0c\u6295\u8eab\u6ee8\u6d77\u4f53\u9a8c\u4e2d\u5fc3\uff0c\u8ba9\u60a8\u65f6\u65f6\u523b\u523b\u4eab\u6709\u5ea6\u5047\u7684\u4f53\u9a8c\u3002"},{"size":[120,150],"type":"img","name":"\u6c99\u6ee9\u8f66","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511762\/0"}]},{"title":"\u6ee8\u6d77\u9152\u5e97","ps1":[{"size":[150,150],"type":"title","title":"\u6ee8\u6d77\u9152\u5e97","subTitle":"Beach Hotel"},{"size":[260,150],"type":"img","name":"\u9152\u5e97\u5916\u7acb\u9762","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511598\/0"},{"size":[100,150],"type":"img","name":"\u9152\u5e97\u5427\u53f0","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511834\/0"},{"size":[200,150],"type":"img","name":"\u9152\u5e97\u5927\u5802","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511835\/0"}],"ps2":[{"size":[220,150],"type":"img","name":"\u8c6a\u534e\u5ba2\u623f","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511837\/0"},{"size":[170,150],"type":"text","content":"\u4e94\u661f\u7ea7\u6807\u51c6\u7684\u6ee8\u6d77\u9152\u5e97\uff0c\u62e5\u6709339\u95f4270\u5ea6\u5168\u6d77\u666f\u5ba2\u623f\uff0c\u662f\u6df1\u5733\u4e1c\u81f3\u597d\u7684\u6ee8\u6d77\u5ea6\u5047\u9152\u5e97\u3002\u56fd\u9645\u51e4\u51f0\u5bb4\u4f1a\u5385\u80fd\u5bb9\u7eb31500\u4f4d\u5609\u5bbe\uff0c\u4e2d\u897f\u9910\u5385\u3001\u6d77\u666f\u5065\u8eab\u5ba4\u3001\u6052\u6e29\u6cf3\u6c60\u7b49\u5eb7\u4f53\u5a31\u4e50\u8bbe\u65bd\u4e00\u5e94\u4ff1\u5168\uff0c\u8fd8\u6709\u4e1c\u4e9a\u6700\u5927\u7684\u6c34\u65cf\u7bb1\u3002"},{"size":[200,150],"type":"img","name":"\u4e1c\u5357\u4e9a\u6c34\u65cf\u9986","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511860\/0"},{"size":[120,150],"type":"img","name":"\u5065\u8eab\u623f","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511791\/0"}]},{"title":"\u4f11\u95f2\u751f\u6d3b","ps1":[{"size":[150,150],"type":"title","title":"\u4f11\u95f2\u751f\u6d3b","subTitle":"Leisure Life"},{"size":[260,150],"type":"img","name":"\u4f11\u95f2\u4e66\u5427","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511863\/0"},{"size":[100,150],"type":"img","name":"\u9152\u5427\u591c\u666f","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511796\/0"}],"ps2":[{"size":[220,150],"type":"img","name":"\u9152\u5427\u8857","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511797\/0"},{"size":[170,150],"type":"text","content":"\u4f18\u7f8e\u7684\u6d77\u5cb8\u6c99\u6ee9\u3001\u5927\u57ce\u914d\u5957\u548c\u4f11\u95f2\u5ea6\u5047\u751f\u6d3b\u65b9\u5f0f\u6811\u7acb\u4e86\u672a\u6765\u4e2d\u56fd\u6ee8\u6d77\u5ea6\u5047\u7684\u6807\u6746\uff0c\u6ee8\u6d77\u65c5\u6e38\u6f14\u53d8\u6210\u4e86\u96c6\u5c45\u4f4f\u3001\u4f11\u95f2\u3001\u5ea6\u5047\u4e3a\u4e00\u4f53\u7684\u6df1\u5ea6\u6ee8\u6d77\u751f\u6d3b\u3002"},{"size":[120,150],"type":"img","name":"\u4f11\u95f2\u533a","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc512027\/0"}]},{"title":"\u94f6\u6ee9\u524d\u666f","ps1":[{"size":[150,150],"type":"title","title":"\u94f6\u6ee9\u524d\u666f","subTitle":"Beach Landscape"},{"size":[260,150],"type":"img","name":"\u6c99\u6ee9\u7f8e\u5885","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc512029\/0"},{"size":[100,150],"type":"img","name":"\u6c99\u6ee9\u4e4b\u5fc3","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc511871\/0"}],"ps2":[{"size":[220,150],"type":"img","name":"\u7ea2\u82b1\u6c99\u6ee9","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc512031\/0"},{"size":[170,150],"type":"text","content":"\u5341\u91cc\u5929\u7136\u6d01\u51c0\u7684\u6c99\u6ee9\uff0c\u6d77\u6c34\u7eaf\u51c0\u800c\u900f\u660e\uff0c\u62e5\u6709\u6781\u9ad8\u7684\u80fd\u89c1\u5ea6\uff0c\u662f\u4e2d\u56fd\u5357\u65b9\u73af\u5883\u6700\u4f18\u7f8e\u3001\u6700\u751f\u6001\u7684\u6d77\u6ee9\u4e4b\u4e00\u3002\u793e\u533a\u5185\u5f15\u5165\u7eff\u9053\uff0c\u7901\u77f3\u516c\u56ed\uff0c\u7ea2\u6811\u516c\u56ed\u7b49\u539f\u751f\u6001\u65c5\u6e38\u8d44\u6e90\u3002"},{"size":[120,150],"type":"img","name":"\u6700\u7f8e\u94f6\u6ee9","img":"http:\/\/p.qpic.cn\/ecc_merchant\/0\/P_idc512034\/0"}]}]);
		},
		resizeLayout : function() {
			var R = PICSHOW.RUNTIME, p = R.picSize, pages = R.scrollPagesX, num = 0;
			R.w = document.documentElement.clientWidth;
			R.h = document.documentElement.clientHeight;
			R.widthTime = window.innerWidth > window.innerHeight && R.h < 350 ? 2 : 1;
			if (R.view == 'thub') {
				if (R.widthTime > 1) {
					R.scroller.addClass('photo_wide');
				} else {
					R.scroller.removeClass('photo_wide');
				}
				num = R.widthTime == 2 ? R.reduceSize : pages[pages.length - 1] - 10;
				R.scrollList.css({
					width : num + 'px'
				});
				if (R.scrollerScroll) {
					var t = R.thubIdx, el = $('#picshow' + t);
					PICSHOW.showSlideText(Math.max(t - 1, 0));
					R.scrollerScroll.refresh();
					R.isRunning = true;
					if (el.length) {
						try {
							R.scrollerScroll.scrollToElement(el[0], 40);
						} catch (e) {
						}
					}
					setTimeout(function() {
						delete R.isRunning;
					}, 50);
				} else {
					PICSHOW.thubScrollCB();
				}
			} else {
				var s1 = R.scroller1Scroll;
				R.scrollPicLi.addClass('noLoading');
				R.scrollPicLi.css({
					width : R.w + 'px'
				});
				R.scrollPic.css({
					width : p.length * R.w + 'px',
					height : R.h + 'px'
				});
				s1.refresh();
				setTimeout(function() {
					s1.scrollToPage(s1.currPageX, 0);
				}, 100);
				$('img[load="false"]').css({
					width : R.w + 'px',
					height : R.h + 'px'
				});
				for (var i = 0, il = R.imgDom.length; i < il; i++) {
					if (R.imgDom[i]) {
						PICSHOW.origImgLoad(R.imgDom[i]);
					}
				}
			}
		},
		slidePics : function(evt, idx) {
			evt = evt || window.event;
			var tar = evt.srcElement || evt.target, idx = !!idx ? idx : (tar && (tar.id || tar.idx) ? parseInt((tar.id || tar.idx).replace('thubImg', '')) : 0), idx = isNaN(idx) ? 0 : idx, R = PICSHOW.RUNTIME, img = $('#bImg' + idx), s = R.scroller1Scroll, p = R.picSize[idx];
			console.log(idx);
			R.view = 'big';
			R.scrollPic.show();
			R.scroller1.show();
			PICSHOW.resizeLayout();
			R.scrollPicLi.addClass('noLoading');
			R.popMask.show();
			s.refresh();
			if (img.length) {
				s.scrollToPage(idx);
				if (!p.loaded) {
					FCAPP.Common.loadImg(p.img, 'bImg' + idx, PICSHOW.origImgLoad);
				}
			}
			R.picName.html((idx + 1) + '/' + R.picSize.length + '  ' + p.name);
		},
		closeSlidePics : function() {
			var R = PICSHOW.RUNTIME, s = R.scrollerScroll, s1 = R.scroller1Scroll, p = R.picSize;
			R.scrollPic.hide();
			R.scroller1.hide();
			R.popMask.hide();
			R.view = 'thub';
			PICSHOW.resizeLayout();
			s.refresh();
			s.scrollToElement($('#thubImg' + p[s1.currPageX].idx)[0], 200);
		},
		showPics : function(data) {
			debugger;
			var R = PICSHOW.RUNTIME;
			var width = 0, totalWidth = 0;
			for (var i = 0, il = data.length; i < il; i++) {
				width = PICSHOW.calcWidth(data[i], i);
				R.scrollWidth.push(width);
				R.scrollTitle.push(data[i].title);
				totalWidth += width;
				R.scrollPagesX.push(totalWidth);
			}
			R.lastGroup = data[i - 1];
			PICSHOW.renderPics(data);
			PICSHOW.resizeLayout();
		},
		renderPics : function(data) {
			var R = PICSHOW.RUNTIME;
			PICSHOW.showSlideText(0);
			R.scrollList.html($.template(R.template1, {
				data : data
			}));
			R.scrollPic.html($.template(R.template3, {
				data : R.picSize,
				R : R
			}));
			R.scrollPicLi = $('#scrollPic li');
			setTimeout(function() {
				PICSHOW.initScroll('scroller', PICSHOW.thubScrollCB, false, true);
				FCAPP.Common.hideLoading();
				R.opacityInterval = setInterval(PICSHOW.showThubGroup, 50);
			}, 100);
			setTimeout(function() {
				R.scrollPic.css({
					width : R.w * R.picSize.length + 'px'
				});
				PICSHOW.initScroll('scroller1', PICSHOW.origScrollCB, true, false);
				R.picName.html(R.picSize[0].name);
				PICSHOW.loadThubImg(0);
				PICSHOW.loadThubImg(1);
			}, 500);
		},
		calcWidth : function(part, dataIdx) {
			var R = PICSHOW.RUNTIME, width = {}, titleIdx = -1, textIdx = -1, textLoc = 0, titleLoc = 0, str, len, cw, idx = -1;
			for (var i in part) {
				var data = part[i];
				if (!( data instanceof Array) || !('length' in data)) {
					continue;
				}
				width[i] = 0;
				for (var j = 0, jl = data.length; j < jl; j++) {
					if (data[j].type == 'img') {
						cw = Math.floor(data[j].size[0] * (150 / data[j].size[1]));
						idx = R.picIdx++;
						part[i][j].idx = idx;
						R.picSize[idx] = {
							name : data[j].name,
							img : data[j].img,
							idx : idx,
							group : dataIdx,
							w : data[j].size[0],
							h : data[j].size[1]
						};
					} else if (data[j].type == 'text') {
						str = data[j].content;
						len = str.length;
						cw = Math.ceil(len * 140 / 78) + 22;
						if (data[j].size) {
							cw = Math.floor(data[j].size[0] * (150 / data[j].size[1]));
						}
						data[j].width = cw - 10;
						textLoc = j;
					} else if (data[j].type == 'title') {
						str = data[j].title.replace(/[a-z0-9]+/gi, '');
						len = str.length + Math.ceil((data[j].title.length - str.length) / 2);
						cw = 150;
						data[j].width = cw;
					}
					cw += (j == 0 ? 2 : 10);
					width[i] += cw;
				}
			}
			cw = width.ps2 - width.ps1;
			if (cw > 0) {
				width.ps1 += cw;
				part.ps1[titleLoc].width += cw;
			} else {
				width.ps2 -= cw;
				part.ps2[textLoc].width -= cw;
			}
			width.ps2 += 24;
			width.ps1 += 24;
			R.reduceSize += width.ps1 + width.ps2 - 12;
			part.width = width.ps2;
			return part.width;
		},
		thubScrollCB : function() {
			if (!PICSHOW.RUNTIME.scrollerScroll || PICSHOW.RUNTIME.isRunning) {
				return;
			}
			var R = PICSHOW.RUNTIME, scroll = R.scrollerScroll, x = Math.abs(scroll.x), p = R.scrollPagesX, w = R.scrollWidth, tmp = 0;
			for (var il = p.length, i = il - 1; i > -1; i--) {
				tmp = (p[i] - w[i]) * R.widthTime - R.w / 2;
				if (x > tmp) {
					R.thubIdx = i + 1;
					PICSHOW.loadThubImg(i);
					PICSHOW.showSlideText(i);
					PICSHOW.loadThubImg(i + 1);
					break;
				}
			}
		},
		showThubGroup : function() {
			var R = PICSHOW.RUNTIME;
			if (R.opacity >= 1) {
				clearInterval(R.opacityInterval);
			} else {
				R.opacity += 0.05;
				R.scroller.css('opacity', R.opacity);
			}
		},
		showSlideText : function(i) {
			var R = PICSHOW.RUNTIME, t = R.scrollTitle, il = R.scrollPagesX.length, idx = 0, end = 0;
			if (i < 2) {
				idx = i;
				i = 0;
				end = i + 3;
			} else {
				idx = 1;
				if (il - i < 3) {
					end = il;
					if (i == il - 1) {
						idx = 2;
					}
					i = il - 3;
				} else {
					i -= 1;
					end = i + 3;
				}
			}
			var data = {
				data : t.slice(i, end),
				idx : idx,
				start : (i + 1)
			};
			R.scrollTips.html($.template(R.template2, data));
		},
		loadThubImg : function(idx) {
			var R = PICSHOW.RUNTIME, p = R.picSize;
			if (!R.loadedThub[idx] && idx < p.length) {
				R.loadedThub[idx] = true;
				for (var j = 0, jl = p.length; j < jl; j++) {
					if (p[j].group == idx) {
						FCAPP.Common.loadImg(p[j].img, 'thubImg' + p[j].idx, PICSHOW.thubImgLoad);
					}
				}
			}
		},
		thubImgLoad : function(img, i) {
			var R = PICSHOW.RUNTIME, idx = (img.idx || img.id).replace(/[^\d]+/g, '');
			img.height = 150;
			img.width = Math.floor(R.picSize[idx].w * (150 / R.picSize[idx].h));
			img.id = img.idx;
			img.onclick = PICSHOW.slidePics;
		},
		origScrollCB : function() {
			var R = PICSHOW.RUNTIME, scroll = R.scroller1Scroll, idx = scroll.currPageX, p = R.picSize[idx];
			$('#bLi' + idx).removeClass('noLoading');
			R.picName.html((idx + 1) + '/' + R.picSize.length + '  ' + p.name);
			if (!p.loaded) {
				FCAPP.Common.loadImg(p.img, 'bImg' + idx, PICSHOW.origImgLoad);
			}
		},
		origImgLoad : function(img) {
			if (!img) {
				return;
			}
			var R = PICSHOW.RUNTIME, idx = (img.idx || img.id).replace(/[^\d]+/g, ''), p = R.picSize[idx], cssText = '', mg = 0, sw = R.w - 10, sh = R.h, fw = 0, fh = 0;
			if ((p.h / p.w) < (sh / sw)) {
				fw = sw;
				fh = Math.ceil(p.h * sw / p.w);
				mg = Math.ceil((sh - fh) / 2);
				cssText = 'margin:' + mg + "px 0";
			} else {
				fh = sh;
				fw = Math.ceil(p.w * fh / p.h);
				mg = Math.ceil((sw - fw) / 2);
				cssText = 'margin:0 ' + mg + 'px 0 ' + mg + 'px';
			}
			img.id = 'bImg' + idx;
			if (!img.idx) {
				img.idx = img.id;
			} else {
				R.picSize[idx].loaded = true;
				R.imgDom[idx] = img;
			}
			img.width = fw;
			img.height = fh;
			img.style.cssText = cssText;
		},
		initScroll : function(id, cb, snap, momentum) {
			var R = PICSHOW.RUNTIME;
			R[id + 'Scroll'] = new iScroll(id, {
				zoom : false,
				snap : !!snap,
				momentum : !!momentum,
				hScrollbar : false,
				vScrollbar : false,
				fixScrollBar : false,
				hScroll : true,
				onScrollEnd : cb ||
				function() {
				}

			});
		},
		scrollTo : function(idx) {
			var R = PICSHOW.RUNTIME;
			try {
				R.scrollerScroll.scrollToElement($('#picshow' + idx)[0], 300);
			} catch (e) {
			}
		}
	};
	var PICSHOW = FCAPP.HOUSE.PICSHOW;
	window.PICSHOW = PICSHOW;
	$(document).ready(PICSHOW.init);
	return PICSHOW;
});
