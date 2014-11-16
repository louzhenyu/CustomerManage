Jit.AM.defindPage({

    name: 'IndexShop',
	
	initWithParam: function(param){
		
		if(param['showMiddleArea']=='false'){

			$('.noticeList').hide();
		}
	},
	
    onPageLoad: function () {
        this.loadData();
        this.initEvent();
    },
    initEvent:function(){
        $("body").delegate("#searchBtn",this.eventType,function(){
            Jit.AM.toPage('GoodsList','itemName='+ $.trim($("#searchContent").val()));
        });
    },
    loadData:function(){
        var me = this;

        Jit.UI.Loading(true);

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'AppConfig.HomePageConfig.HomePageConfig'
            },
            success: function (data) {

                Jit.UI.Loading(false);

                me.initImageScrollView(data.Data.AdAreaList);

                me.initEntrance(data.Data.CategoryEntrance);

                me.initMainEntry(data.Data.ItemEventAreaList);

                me.loadCategoryList(data.Data.CategoryGroupList);
            }
        });
    },
	initImageScrollView:function(datas){
		
		var me = this,htmls = [], bars = [], imglen = datas.length, href = '';
		
		for(var i in datas){
		
			if(datas[i]['ObjectTypeID'] == 2){

				href = datas[i]['Url'];

				if(datas[i]['Url'].indexOf('http://') == -1){

					href = 'http://'+href;
				}

			}else if(datas[i]['ObjectTypeID'] == 3){

				href = "javascript:Jit.AM.toPage('GoodsDetail','goodsId="+datas[i]['ObjectID']+"')";
				//href = "javascript:Jit.AM.toPage('GoodsList','goodsType="+datas[i]['ObjectID']+"')"
			}
			
			htmls.push('<li><a href="'+href+'"><img style="max-width:100%;max-height:100%;" src="'+datas[i]['ImageUrl']+'"></a></li>');
			//htmls.push('<li><a><img src="/HtmlApps/images/common/misspic.png"></a></li>');
			
			if(i==0){
				
				bars.push('<em class="on"></em>');
				
			}else{
			
				bars.push('<em></em>');
			}
		}
		
		$('.picList').html(htmls.join(''));
		
		$('.dot').html(bars.join(''));
		
		var goodsScroll = $('#PicScroll'),
            menuList = $('.dot em');
		
        //重新設置大小
        ReSize();
			
		function ReSize() {
			
			goodsScroll.find('.picList').css({
			
				width: (goodsScroll.width()) * goodsScroll.find('.picList li').size() + 'px'
			});

			goodsScroll.find('.picList li').css({
			
				width: (goodsScroll.width()) + 'px'
			});
		}
		
		me.ImgScroll = new iScroll('PicScroll', {
			snap: true,
			momentum: false,
			vScroll:false,
			hScrollbar: false,
			vScrollbar: false,
			onScrollEnd: function() {
				
				if (this.currPageX > (menuList.size() - 1)) {
				
					return false;
				};
				
                menuList.removeClass('on');
				
                menuList.eq(this.currPageX).addClass('on');
			}
		});
		
		$(window).resize(function(){
		
			ReSize();
			
			me.ImgScroll.refresh();
		});

		var scrollnum = 0;

		setInterval(function(){

			scrollnum++;

			me.ImgScroll.scrollToPage(scrollnum%imglen);

		},3000);
	},

    initEntrance:function(data){

        var me = this;

        var itemlists = data;


        if(!data||data.CategoryAreaList.length==0){
            $("#entranceList").remove();
            return false;
        }


        var tpl = $('#Tpl_entrance').html();

        var itemdata = me.buildItemData(itemlists);

        var html="";

        for (var i = 0; i < itemdata.length; i++) {
            html += Mustache.render(tpl, itemdata[i]);
        }

        $('#entranceList').html(html);

    },
	
	initMainEntry:function(datas){
	
		var me = this;
		
		if(datas.length == 0){
			//$('.list').parent().remove();
			return;
		}

		var tpl = '', html = '';
		
		for(var i in datas){
			
			if(datas[i].TypeID == 1){
				//团购
				tpl = $('#Tpl_group_shop').html();
				
			}else if(datas[i].TypeID == 2){
				//抢购
				tpl = $('#Tpl_rush_shop').html();
				
			}else if(datas[i].TypeID == 3){
				//热购
				tpl = $('#Tpl_hot_shop').html();
			}
			
			datas[i].DiscountRate =datas[i].DiscountRate;
			
			html += Mustache.render(tpl, datas[i]);
		}
		
		$('.list').html(html);
		
		$('.list .noticeArea ').eq(1).addClass('mlmr');
		
		if($('.list').find('[time-date]').length>0){
			
			setInterval(function(){
				
				me.timeDown();
				
			},1000);
		}
	},
	timeDown:function(){
		
		var domlist = $('[time-date]'),endtime,second,
			nowtime = new Date().getTime();
		
		var _h,_m,_s;
		
		domlist.each(function(idx,dom){
			
			endtime = $(dom).attr('time-date');
			
			endtime = endtime.replace(/-/g,'/');
			
			endtime = new Date(endtime).getTime();

			second = Math.abs(parseInt((endtime - nowtime)/1000));
			
			_h = Math.floor(second/3600);
			
			_m = Math.floor((second%3600)/60);
			
			_s = Math.floor(((second%3600)%60));

            //超过99小时全部显示99
            _m = _h > 99 ? 99 : _m;
            _s = _h > 99 ? 99 : _s;
            _h = _h > 99 ? 99 : _h;
			
			$(dom).find('[tn=time-h-max]').html( Math.floor(_h/10)>9?9:Math.floor(_h/10));
			
			$(dom).find('[tn=time-h-min]').html( Math.floor(_h%10) );
			
			$(dom).find('[tn=time-m-max]').html( Math.floor(_m/10) );
			
			$(dom).find('[tn=time-m-min]').html( Math.floor(_m%10) );
			
			$(dom).find('[tn=time-s-max]').html( Math.floor(_s/10) );
			
			$(dom).find('[tn=time-s-min]').html( Math.floor(_s%10) );
		});
	},
    loadCategoryList: function (data) {//加载c区模块
		
		var me = this;
		debugger;
        var itemlists = data;

        var html = '',tpl;

        for (var i = 0; i < itemlists.length; i++) {

        	if(itemlists[i]['ModelTypeId'] == 3){
        		//1+2模块
        		tpl = $('#Tpl_item_3').html();

        	}else if(itemlists[i]['ModelTypeId'] == 1){
        		//单张图片模块
        		tpl = $('#Tpl_item_1').html();
        	}else if(itemlists[i]['ModelTypeId']==2){
        		//1+1模式
        		tpl = $('#Tpl_item_2').html();
        	}
            
			var itemdata = me.buildItemData(itemlists[i]);
			
            html += Mustache.render(tpl, itemdata);
        }

        $('.content').append(html);
    },
	
	buildItemData:function(data){
		//单项排序
		var itemdata = data.CategoryAreaList.sort(function(A,B){

			if(A.DisplayIndex>B.DisplayIndex){

				return 1;

			}else{

				return -1;
			}

		});

		for(var i=0;i<itemdata.length;i++){

			if(itemdata[i].TypeID==1){
				//分类

				itemdata[i]['href'] = "javascript:Jit.AM.toPage('GoodsList','goodsType="+itemdata[i]['ObjectID']+"')";

			}else if(itemdata[i].TypeID==2){
				//商品

				itemdata[i]['href'] = "javascript:Jit.AM.toPage('GoodsDetail','goodsId="+itemdata[i]['ObjectID']+"')";
			}else if(itemdata[i].TypeID==8){
                // 全部分类
                itemdata[i]['href']="javascript:Jit.AM.toPage('Category')";
            }
		}

		return itemdata;
	}
});