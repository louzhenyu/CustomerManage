var AppVersion = "1.4";
 var require = {
         urlArgs: "v="+AppVersion
	    };
var LoadfileArr = [["css","css/V1.css?v="+AppVersion,["type","text/css"],["rel","stylesheet"]],
["js","js/require.js?v="+AppVersion,["type","text/javascript"],["data-main","js/lib/app.js"],["async","true"]]];

LoadFile(LoadfileArr);
function  LoadFile(arr){
	 if(arr.length > 0 ){
		for(var i = 0; i< arr.length; i++){
			var srrf  = arr[i];
			if(arr[i][0] == "css"){
				var src = document.createElement("link");
					src.setAttribute("href",arr[i][1]);
				 	for(var c =2; c< srrf.length; c++){
							src.setAttribute(srrf[c][0],srrf[c][1]);
						}
					document.getElementsByTagName("head")[0].appendChild(src);	
			}
			if(arr[i][0] == "js"){
				var src = document.createElement("script");
					src.setAttribute("src",arr[i][1]);
				 	for(var c =2; c< srrf.length; c++){
							src.setAttribute(srrf[c][0],srrf[c][1]);
						}
					document.getElementsByTagName("head")[0].appendChild(src);	
			}			 
		}
	}	
}
