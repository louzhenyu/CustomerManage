Jit.AM.defindPage({

    name: 'ListGoods',
	
	page:{
		index:1,
		size:10
	},
	
	initWithParam: function(param){
		
		if(param['bannerDisplay'] == true){
		
			$('#goodsTitleUrl').show();

            $('#goodsTitleUrl').attr('href',param.toUrl);

            $('#goodsTitleUrl img').attr('src',param.imgUrl);

		}else{

            $('#goodsTitleUrl').hide();
        }

        if(param['hidePrice'] == 'true'){

        	this.hidePrice = true;
        }
	},
	
    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入ListGoods.....');
        
		this.isSending = false;
		this.noMore = false;
		this.goodsType = JitPage.getUrlParam('goodsType');
        this.goodsItemName = JitPage.getUrlParam("itemName")?decodeURIComponent(JitPage.getUrlParam("itemName")):undefined;
		
        this.initEvent();
        this.loadData();
    },
	initEvent:function(){
		var self = this;
		window.onscroll = function(){
			if(getScrollTop() + getWindowHeight() == getScrollHeight()){//console.log("you are in the bottom!");
				if(!self.noMore&&!self.isSending) {
                    self.page.index++;
                    self.loadData();
                }
            }
		};
	},
    loadData: function () {

        var self = this;
		
		var _param = {
                'action': 'getItemList',
                'isExchange': 0,
				'page':this.page.index,
				'pageSize':this.page.size
            };
		
		if(this.goodsType){
			_param.itemTypeId = this.goodsType;
		}
        debugger;
        if(this.goodsItemName){
            _param.itemName = this.goodsItemName;
        }
		
		if(!this.isSending){
			this.ajax({
	        	type:"GET",
	            url: '/OnlineShopping/data/Data.aspx',
	            data: _param,
	            beforeSend:function(){
            		Jit.UI.Loading(true);
	            	self.isSending = true;
	            },
	            success: function (data) {
	                self.renderPage(data.content);
	                if(data.content.itemList.length!=self.page.size){
	                	self.noMore = true;
	                }
	            },
	            complete:function(){
            		Jit.UI.Loading(false);
	            	self.isSending =false;
	            }
	        });
		}
        

    },
    renderPage: function (data) {

        var itemlists = data.itemList;
		
        var tpl = $('#Tpl_goods_list').html(), html = '';

        for (var i = 0; i < itemlists.length; i++) {
            var itemdata = itemlists[i];
            if (itemdata.price > 0) {
                itemdata.oldprice = "￥" + itemdata.price;
            } else {
                itemdata.oldprice = '';
            }
			if(this.hidePrice){
				itemdata.hidePrice = 'hide';
			}
			itemdata.imageUrl = Jit.UI.Image.getSize(itemdata.imageUrl,'240');
			itemdata.saled=Math.floor(Math.random() * (1000- 0) + 0);
            html += Mustache.render(tpl, itemdata);
        }
        //console.log("index:"+this.page.index)
		if(this.page.index==1){
			$('[tplpanel=goods_list]').html(html);
            if(itemlists.length==0){
                $('[tplpanel=goods_list]').after('<div style="width:320px;height:100px;text-align: center;position: absolute;left:50%;top:50%;margin:-50px 0 0 -160px;;">没有查找到您搜索的商品!</div>');
            }

		}else{
			$('[tplpanel=goods_list]').append(html).siblings().remove();;
		}
        
    }
});

//滚动条在Y轴上的滚动距离
function getScrollTop(){
　　var scrollTop = 0, bodyScrollTop = 0, documentScrollTop = 0;
　　if(document.body){
　　　　bodyScrollTop = document.body.scrollTop;
　　}
　　if(document.documentElement){
　　　　documentScrollTop = document.documentElement.scrollTop;
　　}
　　scrollTop = (bodyScrollTop - documentScrollTop > 0) ? bodyScrollTop : documentScrollTop;
　　return scrollTop;
}

//文档的总高度
function getScrollHeight(){
　　var scrollHeight = 0, bodyScrollHeight = 0, documentScrollHeight = 0;
　　if(document.body){
　　　　bodyScrollHeight = document.body.scrollHeight;
　　}
　　if(document.documentElement){
　　　　documentScrollHeight = document.documentElement.scrollHeight;
　　}
　　scrollHeight = (bodyScrollHeight - documentScrollHeight > 0) ? bodyScrollHeight : documentScrollHeight;
　　return scrollHeight;
}

//浏览器视口的高度
function getWindowHeight(){
　　var windowHeight = 0;
　　if(document.compatMode == "CSS1Compat"){
　　　　windowHeight = document.documentElement.clientHeight;
　　}else{
　　　　windowHeight = document.body.clientHeight;
　　}
　　return windowHeight;
}
