Jit.AM.defindPage({
	onPageLoad : function() {
		this.initPage();
	},
	initEvent : function() {
		var me = this;
	},
	initPage : function() {
		//playbox.init("playbox");

		//$(".navBox").fadeOut().fadeIn();
		//另外一个特效
		var first = alice.init();
		first.slide({
			elems : ".a_move",
			move : "left",
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
