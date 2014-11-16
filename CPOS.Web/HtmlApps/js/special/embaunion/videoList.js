Jit.AM.defindPage({
	name: 'VideoList',
	hideMask: function() {
		$("#maskLayer").hide();
	},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.hideMask();
		Jit.log('页面进入' + this.name);
		this.loadPageData();
		this.initPageEvent();
	},
	initPageEvent: function() {
		var self = this;
		$("#section").delegate(".itemBtn", "tap", function() {
			var $this = $(this),
				type = $this.data("type"),
				videoId = $this.data("id");
			if ((type == 1 || type == 2 || type == 3) && videoId) {
				self.addNewsCount(type, videoId, function() {
					if ($this.hasClass("on")) {
						$this.html(parseInt($this.html()) - 1);
					} else {
						$this.html(parseInt($this.html()) + 1);
					}
					$this.toggleClass("on");
				});
			}
		}).delegate(".videoBox", "tap", function() {
			var $this = $(this),
				type = $this.data("type"),
				videoId = $this.data("id");
			if ((type == 1 || type == 2 || type == 3) && videoId) {
				self.addNewsCount(type, videoId);
			}
		}).delegate(".share", 'tap', function() {
			$('#share-mask').show();
			$('#share-mask-img').show().attr('class', 'pullDownState');
		});
		$('#share-mask').bind('click', function() {
			var that = $(this);
			$('#share-mask-img').attr('class', 'pullUpState').show();
			setTimeout(function() {
				$('#share-mask-img').hide(500);
				that.hide(1000);
			}, 500);
		})
	},
	//加载数据
	loadPageData: function() {
		var self = this;
		self.getVideoData();
	},
	addNewsCount: function(type, videoId, callback) {
		var self = this;
		self.ajax({
			url: '/Project/CEIBS/CEIBSHandler.ashx',
			data: {
				'action': 'addNewsCount',
				'newsType': 2,
				'countType': type,
				'id': videoId
			},
			beforeSend: function() {
				//Jit.UI.Masklayer.show();
			},
			complete: function() {
				//Jit.UI.Masklayer.hide();
			},
			success: function(data) {
				if (data.code == 200) {
					if (callback) {
						callback();
					}
				} else {
					self.alert(data.description);
				}
			}
		});
	},
	getVideoData: function() {
		var self = this;
		self.ajax({
			url: '/Project/CEIBS/CEIBSHandler.ashx',
			data: {
				'action': 'getAlbumList',
				'page': 1,
				'pageSize': 99
			},
			beforeSend: function() {
				//Jit.UI.Masklayer.show();
			},
			complete: function() {
				//Jit.UI.Masklayer.hide();
			},
			success: function(data) {
				if (data.code == 200) {
					$("#videoList").html(template.render('videoListTemp', {
						"list": data.content.videoList
					}));
				} else {
					self.alert(data.description);
				}
			}
		});
	},
	alert: function(text, callback) {
		Jit.UI.Dialog({
			type: "Alert",
			content: text,
			CallBackOk: function() {
				Jit.UI.Dialog("CLOSE");
				if (callback) {
					callback();
				}
			}
		});
	}
});