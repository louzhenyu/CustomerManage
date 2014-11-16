Jit.AM.defindPage({
	name: 'Vote',
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		Jit.WX.shareFriends('中欧深圳分会换届投票','各位中欧深圳校友：自深圳校友理事会换届提名工作启动以来，得到了广大校友的积极支持和参与，截至6月20日，共提名产生合资格候选人94名。自6月30日起将进入本次换届的投票选举阶段，依然期待广大深圳校友积极支持和参与。',window.location.href,window.location.protocol+'//'+window.location.host+'/HtmlApps/images/special/europe/shenz3.jpg');
		this.loadPageData();
		this.initPageEvent();
	},
	initPageEvent: function() {
		//,.,.,
		//Jit.WX.shareFriends('888','888',location.href,'/HtmlApps/images/special/europe/shenz3.jpg');
		var is_over=false;
		//console.log(is_over);
		if(is_over){//活动已经结束
			$('#vote_hint').show();
            $('#vote_form_submit').addClass('has_post').html('已结束');
		}
		if(Jit.AM.getPageParam('userId')){			
			//已经投票
			$('#vote_hint_apply').show();
			$('#vote_form_submit').addClass('has_post').html('您已投票');
			return false;
		}
		if(is_over){
			return false;
		}
		var self = this;
		var can_submit=true;
		var max_voter=30;
		//投票
		$('input[name="vote_name"]').on('change',function(){
			if(this.checked){
				$(this).parent().addClass('checked');
			}else{
				$(this).parent().removeClass('checked');
			}
		});
		//提交投票
		$('#vote_form_submit').on('click', function() {
			if(!can_submit)return false;
			var that = $(this);
			var grade=$.trim($('#grade').val());
			var name=$.trim($('#name').val());
			var cellphone=$.trim($('#cellphone').val());
			if(!grade){
				Jit.UI.Dialog({
					type:'Alert',
					content:'请输入您的班级！',					
					CallBackOk:function(){
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			if(!name){
				Jit.UI.Dialog({
					type:'Alert',
					content:'请输入您的姓名！',					
					CallBackOk:function(){
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			if(!cellphone){
				Jit.UI.Dialog({
					type:'Alert',
					content:'请输入您的手机号！',					
					CallBackOk:function(){
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			if(!Jit.valid.isPhoneNumber(cellphone)){
				Jit.UI.Dialog({
					type:'Alert',
					content:'您的手机号格式有误！',					
					CallBackOk:function(){
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			var vote_name_arr=[];
			$('input[name="vote_name"]:checked').each(function(){
				vote_name_arr.push($(this).val());
			});
			var vote_name_str=vote_name_arr.join(',');		
			//console.log(vote_name_arr);
			//console.log(vote_name_str);
			if(vote_name_arr.length<=0){//必选做出至少一个投票！
				Jit.UI.Dialog({
					type:'Alert',
					content:'必选做出至少一个投票！',					
					CallBackOk:function(){
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			if(vote_name_arr.length>max_voter){//最多投票人数
				Jit.UI.Dialog({
					type:'Alert',
					content:'至多做出'+max_voter+'个投票！',					
					CallBackOk:function(){
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			}
			Jit.AM.ajax({
				url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
				data: {
				    'action': 'submitVoteInfo',
				    //'VoteInfo':{
			    	'ObjectID':'70684073319bffd28a12ebc246c92b10',
			    	'ClassInfo':grade,
			    	'VipName':name,
			    	'Phone':cellphone,
			    	'VoteName':vote_name_str
				    //}
				},
				success: function(data) {
					if (data&&(data.ResultCode == 0)) {
						self.alert('投票成功！',function(){
							location.reload();
						});
						Jit.AM.setPageParam('userId',Jit.AM.getBaseAjaxParam().userId);
						//记录投票对象
						Jit.AM.setPageParam('voter',vote_name_str);
						can_submit=false;
					} else {
						//self.alert(data.Message,function(){
							//location.reload();
						//});
                        self.alert(data.Message);
						//console.log(data);
						//Jit.AM.setPageParam('userId',Jit.AM.getBaseAjaxParam().userId);
						//can_submit=false;
					}
				}
			});
		});
	},
	//加载数据
	loadPageData: function() {
		var self = this;
		self.getVoter();
	},
	getVoter: function() {
		$vote_list=$("#vote_list");
		var voter_list_str=Jit.AM.getPageParam('voter');
		//console.log(voter_list_str);
		var voter_list_arr=[];
		if(voter_list_str){
			voter_list_arr=voter_list_str.split(',');
		}
		//console.log(voter_list_arr);
		for(var i=0;i<voter.length;i++){
			//voter[i]['voter']=voter_list_arr;
			for(var j=0;j<voter_list_arr.length;j++){
				if(voter[i]['name']==voter_list_arr[j]){
					voter[i]['checked']=1;
					break;
				}
			}
            //索引
            voter[i]['index']=i+1;
			if ((i+1)%2==0) {
				$vote_list.append(template.render('vote_list_item_ml', {
								"list": voter[i]
							}));
			}else{
				$vote_list.append(template.render('vote_list_item', {
								"list": voter[i]
							}));
			}
		}
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