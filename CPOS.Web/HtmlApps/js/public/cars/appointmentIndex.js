Jit.AM.defindPage({
	name:"appointment",
	elements:{},
	onPageLoad : function() {
		this.initPage();
	},
	initPage : function() {
		this.alice(".menu a");
	},
	alice:function(ele){
		alice.init().slide({
			elems : ele,
			move : "up",
			duration : {
				"value" : "1000ms",
				"randomness" : "30%",
				"offset" : "100ms"
			},
			"overshoot" : "5"
		});
	}
});
