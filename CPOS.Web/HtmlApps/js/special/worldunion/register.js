Jit.AM.defindPage({
    elems:{},
    onPageLoad: function () {
        var that=this;
        
		this.ajaxUrl = '/ApplicationInterface/Project/HuaAn/HuaAnHandler.ashx';
		this.DetailID = JitPage.getUrlParam("detailId");//||'2C97BF9A-D770-44EC-9ED1-9866545F7EDD';
		if(!this.DetailID){
			this.alert("未获取到detailId，请检查url");
			return false;
		}
        this.elems.registerForm = $("#registerForm");
        this.elems.registerFormArea = $("#registerFormArea");
        this.getRegisterForm();
    },
    

    //动态用户注册表单项
    PageFormData:null,
    //获取注册表单项
    getRegisterForm:function(){

        var me = this;

        if(me.PageFormData){
			
            me.buildRegisterForm(me.PageFormData);

            return;
        }

        me.ajax({
            url: '/ApplicationInterface/Gateway.ashx',
            data: {
                'action': 'VIP.Register.GetRegisterFormItems',
                'EventCode':'OnLine005'
            },
            success: function (data) {
                Jit.UI.Loading(false);

                if(data.ResultCode == 0 && data.Data.Pages[0]){

                    me.PageFormData = data.Data.Pages[0].Blocks[0].PropertyDefineInfos;

                    me.buildRegisterForm(me.PageFormData);
                }else{
                	me.showTips("未获得注册表单!");
                	return false;
                }
            }
        });
    },

    buildRegisterForm:function(items){

        var htmlstr = '';
		/*
        items = items.sort(function(A,B){
            if(A.DisplayIndex>B.A){
                return 1;
            }else{
                return -1;
            }
        });
		*/
		var isDate=false;
        for(var i in items){
			var item=items[i];
			//表示的是日期类型
			if(item.ControlInfo.ControlType==4){
				isDate=true;
			}
            htmlstr += template.render('tpl_block_item',items[i]);
        }

        this.elems.registerForm.html(htmlstr).parent().show();
        
    },
	//提示内容
    showTips:function(content){
    	Jit.UI.Dialog({
            'content':content+"",
            'type': 'Alert',
            'CallBackOk': function () {
                Jit.UI.Dialog('CLOSE');
            }
        });
    	return false;
    },
    //保存信息
    saveVipInfo:function(){
		
        var me = this;

        var vipinfo = [];
        var inputDom=$('[name=vipinfo]');
        inputDom.each(function(i,dom){
        	var $dom=$(dom);
        	var dataText=$dom.attr("data-text");
        	if($dom.val()==""){
        		me.showTips(dataText+"不能为空!");
        		return false;
        	}else{
        		vipinfo.push({
	                'ID':$dom.attr('wid'),
	                'IsMustDo':false,
	                'Value':$dom.val()
            	});
        	}
            
        });
		if(inputDom&&(inputDom.length==vipinfo.length)){
	        Jit.UI.Loading(true);
	
	        me.ajax({
	            url: '/ApplicationInterface/Gateway.ashx',
	            data: {
	                'action': 'VIP.Register.SetRegisterFormItems',
	                'ItemList':vipinfo,
	                'VipSource':3
	            },
	            success: function (data) {
	                Jit.UI.Loading(false);
	                if(data.IsSuccess){
	                	me.BuyFund();
	                }else{
	                	me.showTips(data.Message);
	                }
	            }
	        });
	   }
    },
    BuyFund:function(callback){
		var self = this;
		var toUrl="http://"+location.host+"/HtmlApps/html/public/shop/paysuccess.html?customerId="+self.getUrlParam('customerId');
		
		self.ajax({
            url: self.ajaxUrl,
			interfaceType:'Project',
			interfaceMode:'V2.0',
            data: {
                action: "BuyFund",
                HouseDetailID:self.DetailID,
                ToPageUrl:toUrl
            },
            success: function(data) {
                if(data.IsSuccess){
					if(callback){
						callback(data.Data);
					}else{
						var obj = data.Data.FormData;
						var form='<form action="'+data.Data.Url+'" method="post">';
						for(var i in obj){
							form+='<input type="hidden" name="'+i+'" value="'+obj[i]+'">';
						}
						form+='</form>';
						$(form).appendTo("html").submit();
					}
                }else{
                	self.showTips(data.Message);
                }                
            }
        });
	}
});