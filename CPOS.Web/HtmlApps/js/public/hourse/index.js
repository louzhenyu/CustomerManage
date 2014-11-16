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
			move : "up",
			duration : {
				"value" : "1000ms",
				"randomness" : "30%",
				"offset" : "100ms"
			},
			"overshoot" : "5"
		});
		var logo = alice.init();
		alice.plugins.zoom({
			"elems": "#boder",
			"scale": {"from": "40%","to": "100%"},
			"shadow": "true",
			"move": "left",
			"duration": {"value": "2000ms","randomness": "30%"},
			"timing": "easeOutQuad","delay": {"value": "0ms","randomness": "20%"},
			"iteration": "1",
			"direction": "alternate",
			"playstate": "running"
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
