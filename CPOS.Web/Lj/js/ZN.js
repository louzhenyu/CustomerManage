var ZN = {
    url: "Interface/AlbumData.aspx",
    LoadHomePage: function () {
	    var jsonarr = { 'action': "getEventAlbum", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "eventId": getParam("eventId") } }) };

       var masklayer=$('#masklayer');

		$.ajax({
			type: "get",
			dataType: "json",
			url: ZN.url,
			data: jsonarr, //"r=" + Math.random(),
			beforeSend: function () {
				Win.Loading();
			},
			success: function (data) {
                masklayer.hide();
			    //data = data[0];
                //the first section
			    var firstSectionContent = _.template($("#FirstSectionContent").html(), data.content);
			    $("#FirstSection").html(firstSectionContent);

                //filter different albumtype
			    var photo = $.grep(data.content.albumList, function (value, key) {
			        return value.albumType == "1";
			    });

			    var video = $.grep(data.content.albumList, function (value, key) {
			        return value.albumType == "2";
			    });

                //the second section: photo
			    var secondSectionContent = [];
			    $.each(photo, function (index, value) {
			        secondSectionContent.push(_.template($("#SecondSectionContent").html(), value));
			    });

			    $("#SecondSection").html(secondSectionContent.join());

                //the third section: video
			    var thirdSectionContent = [];
			    $.each(video, function (index, value) {
			        thirdSectionContent.push(_.template($("#ThirdSectionContent").html(), value));
			    });

			    $("#ThirdSection").html(thirdSectionContent.join());
			    Win.Loading("CLOSE");
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
			    alert(errorThrown);
			    Win.Loading("CLOSE");
                masklayer.hide();
			}
		});
    }, 
    LoadPhotoPage: function () {
        var jsonarr = { 'action': "getPhoto", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "albumId": getParam("albumId") } }) };
        $.ajax({
            type: "get",
            dataType: "json",
            url:ZN.url,
            data: jsonarr, //"r=" + Math.random(),
            beforeSend: function () {
                Win.Loading();
            },
            success: function (data) {
                //data = data[1];
                var photo = $.map(data.content.photoList, function (value, index) {
                    //console.log(value, index);
                    return value.linkUrl;
                });
                
                var showPhotoListHtml = _.template($("#showPhotoList").html(), data.content);
                $("#container").html(showPhotoListHtml);
                
                var touchsliderListHtml = _.template($("#touchsliderList").html(), data.content);
                $("#touchsliderListContainer").html(touchsliderListHtml);


                ZN.PhotoPageBindEvent(photo);
                
                $(document).ready(function () { ZN.Block(); });
                Win.Loading("CLOSE");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            	
            	//@author kevin   临时测试使用
				 var photoList= [
			            { "photoId": "1", "title": "曾娜风采1", "readerCount": "12", "linkUrl": "http://bs.aladingyidong.com/Framework/Javascript/Other/kindeditor/attached/image/lzlj/zn_photo04.jpg", "displayIndex": "1" },
			            { "photoId": "2", "title": "曾娜风采2", "readerCount": "22", "linkUrl": "http://bs.aladingyidong.com/Framework/Javascript/Other/kindeditor/attached/image/lzlj/broswe_big_03.jpg", "displayIndex": "2" },
			            { "photoId": "3", "title": "曾娜风采3", "readerCount": "32", "linkUrl": "http://bs.aladingyidong.com/Framework/Javascript/Other/kindeditor/attached/image/lzlj/zn_photo05.jpg", "displayIndex": "3" },
			            { "photoId": "4", "title": "曾娜风采4", "readerCount": "11", "linkUrl": "http://bs.aladingyidong.com/Framework/Javascript/Other/kindeditor/attached/image/lzlj/photo03.jpg", "displayIndex": "4" },
			     ];
			     var data={
			         photoList:photoList
			     };
				 var photo = $.map(photoList, function (value, index) {
				    //console.log(value, index);
				    return value.linkUrl;
				});
				var showPhotoListHtml = _.template($("#showPhotoList").html(), data);
                $("#container").html(showPhotoListHtml);
                
                var touchsliderListHtml = _.template($("#touchsliderList").html(), data);
                $("#touchsliderListContainer").html(touchsliderListHtml);
                
                
				ZN.PhotoPageBindEvent(photo);
            	$(document).ready(function () { ZN.Block(); });
            	
                alert(errorThrown);
                Win.Loading("CLOSE");
            }
        });
    }, 
    LoadVideoPage: function () {
        var jsonarr = { 'action': "getVideo", ReqContent: JSON.stringify({ "common": Base.All(), "special": { "albumId": getParam("albumId") } }) };
        $.ajax({
            type: "get",
            dataType: "json",
            url: ZN.url,
            data: jsonarr, //"r=" + Math.random(),
            beforeSend: function () {
                Win.Loading();
            },
            success: function (data) {
                //data = data[1];
                $(".main_pad").html(data.content);
                Win.Loading("CLOSE");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
                Win.Loading("CLOSE");
            }
        });
    }, 
    HomePageBindEvent: function () {
        
    }, 
    Block: function () {
        var currentWidth = $(window).width();
        var size = ~~(currentWidth / 140);
        if(!arguments.callee.first){
            arguments.callee.first=true;
            setTimeout(function(){
                $('#container').BlocksIt({
                    numOfCol: size,
                    offsetX: 8,
                    offsetY: 8
                });
            },200);
        }else{
            $('#container').BlocksIt({
                numOfCol: size,
                offsetX: 8,
                offsetY: 8
            });
        }
        
            
    }, 
    PhotoPageBindEvent: function (photo) {
        require(["plugin/touchslider"],function(ts){
            $(".touchslider").touchSlider({
                container: this,
                duration: 350, // 动画速度
                delay: 3000, // 动画时间间隔
                margin: 5,
                mouseTouch: true,
                namespace: "mytouchslider",
                pagination: ".touchslider-nav-item",
                currentClass: "touchslider-nav-item-current", // current 样式指定
                viewport: ".touchslider-viewport",
                autoplay: false, // 自动播放
                change:function(index){
                    console.log(index);
                }
            });
        	var currentIndex=0; //当前图片的索引
        	var beforeY,afterY,curScroll;
        	$('.imgholder').live('touchstart',function(event){
        	    beforeY = event.originalEvent.changedTouches[0].clientY;
        	});
            $('.imgholder').live('touchend', function (event) {
                curScroll=$(document).scrollTop();
                afterY = event.originalEvent.changedTouches[0].clientY;
                if(beforeY == afterY){
                    currentIndex=$(event.target).attr("data-index");
           
                    var height=$(window).height();
                    $('.touchslider-viewport').css({
                        height:height,
                        overflow:"hidden"
                    });
                    $('.touchslider-item span').css({
                        height:height
                    });
                    $('.waterflow').hide();
                    $('.touchslider').show();
                    
                    $(".touchslider").data("mytouchslider").step(currentIndex);
                }
            });        	
            
            $('.touchslider-close').live('touchend',function(){
                $('.waterflow').show();
                $('.touchslider').hide();
                 ZN.Block();
                $("html,body").scrollTop(curScroll);

                //$("body").css({"overflow":"auto"});
            });
            $(".touchslider-autoplay").live("touchend",function(){
                $(".touchslider").data("mytouchslider").start();
            });
            $(".touchslider-stop").live("touchend",function(){
                $(".touchslider").data("mytouchslider").stop();
            });
            $(".touchslider-prev").live("touchend",function(){
                $(".touchslider").data("mytouchslider").prev(function (a, b, c) {
                    console.log("prev----");
                });
            });
            $(".touchslider-next").live("touchend",function(){
                 $(".touchslider").data("mytouchslider").next(function (a, b, c) {
                    console.log("next-----");
                });
            });
            
        });
        //window resize
        $(window).resize(function () { ZN.Block();});
    }
};