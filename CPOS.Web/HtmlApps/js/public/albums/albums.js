Jit.AM.defindPage({
    onPageLoad: function () {
    	var that=this;
       // this.initEvent();
        this.initPage(function(){
        	that.imagesLoaded();  //进行瀑布流布局
        	that.imagesSwipe();
        });
    },
    imagesLoaded:function(){
    	//瀑布流
        $('#Gallery').imagesLoaded(function() {
			// Prepare layout options.
			var options = {
				autoResize : true, // This will auto-update the layout when the browser window is resized.
				container : $('#main'), // Optional, used for some extra CSS styling
				offset : 4, // Optional, the distance between grid items
				itemWidth : 150 // Optional, the width of a grid item
			};
	
			// Get a reference to your grid items.
			var handler = $('#Gallery li');
			// Call the layout function.
			handler.wookmark(options);
		});
    },
    imagesSwipe:function(){
    	//滑动图片
    	//var PhotoSwipe = window.Code.PhotoSwipe;
		//var options = {loop:false},
		//instance = PhotoSwipe.attach( document.querySelectorAll('#Gallery a'), options );
		 $('#Gallery a').photoSwipe();
    },
    initPage: function (callback) {
    	var albumId=this.getUrlParam("albumId");
		this.ajax({
			url:'/lj/Interface/AlbumData.aspx',
			data:{
				"action":"getPhoto",
				"albumId":albumId
			},
			success: function (data) {
				console.log(data);
                if(data.code=="200"){
                	if(data.content&&data.content.photoList){
                		var html=bd.template("albumsTpl",data);
                		$("#Gallery").html(html);
                		callback();
                	}
                }
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
			}
		});
    }
});