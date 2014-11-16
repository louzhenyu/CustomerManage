Jit.AM.defindPage({
    eventId:'',
    name: 'NewUser',
    onPageLoad: function () {
        //当页面加载完成时触发
        this.initEvent();
        this.initData();
    },
    initData: function () {
    	var self = this;
    	this.questionInfo = null;
    	this.answerList = [];
    	this.quesIndex = 0;
        this.eventId=this.getUrlParam('eventId');
        
		this.loadSurveyData(function(data){
			self.questionInfo = data;
		});
    },
    initEvent: function () {
    	var self =this;
    	$("#section").delegate(".beginBtn","touchend",function(){
    		if(!self.questionInfo){
    			self.loadSurveyData(function(data){
					self.questionInfo = data;
					self.nextQuestion();
				});
    		}else{
				self.nextQuestion();
    		}
    	}).delegate(".option","touchend",function(){
			var $option = $(this);
			$option.toggleClass("option-sel");
		}).delegate(".submit","touchend",function(){
			debugger;
			var $this = $(this);
			var answer = [];
			$this.siblings(".options").find(".option-sel").each(function(i,e){
				answer.push($(this).attr("data-id"));
			});
			
			if($this.attr("data-isrequired")==1){
				var minSelected = $this.attr("data-minSelected");
				var	maxSelected = $this.attr("data-maxSelected");
				
				if(answer.length<minSelected){
					self.alert("本题是必选题，至少选"+minSelected+"项");
					return false;
				}else if(answer.length>maxSelected){
					self.alert("本题是必选题，最多选"+maxSelected+"项");
					return false;
				}else{
					self.getAnswer($this.attr("data-index"),$this.attr("data-questionId"),answer.join(","));
					//下一题的index在数组中找不到时提交问卷
					if(self.quesIndex > self.questionInfo.questions.length-1){
						self.submitSurvey();
					}else{
						self.nextQuestion();
					}
				}
			}else{
				self.getAnswer($this.attr("data-index"),$this.attr("data-questionId"),answer.join(","));
				if(self.quesIndex > self.questionInfo.questions.length-1){
					self.submitSurvey();
				}else{
					self.nextQuestion();
				}
			}
			
		});
    },
    nextQuestion:function(){
    	var data= this.questionInfo.questions[this.quesIndex];
		data.quesIndex = this.quesIndex++;
		$("#section").html(template.render('quesTemp',data));
    },
    getAnswer:function(index,id,value){
    	this.answerList[index] = {"questionId":id,"questionValue":value};
    },
    submitSurvey:function(){
    	var self = this;
    	self.ajax({
    		url : '/OnlineShopping/data/Data.aspx',
			data : {
				'action' : 'submitEventApply',
				'eventId' : self.eventId,
				'userName':"william",
				'mobile':"13645516215",
				'email':"yingtianlee@foxmail.com",
				'questionnaireResult':{'questions':self.answerList}
			},
			success : function(data) {
				if (!data) {
					self.alert("服务器返回数据错误");
				} else {
					if (data.code == 200) {
						self.alert("提交成功，谢谢您的参与！");
					}else{
						self.alert(data.description);
					}
				}
			},
			error:function(e){
				alert(JSON.stringify(e));
			}
    	});
    },
    loadSurveyData:function(callback){
    	var self = this;
    	self.ajax({
    		url : '/OnlineShopping/data/Data.aspx',
			data : {
				'action' : 'getEventApplyQues',
				'eventId' : self.eventId
			},
			success : function(data) {
				if (!data) {
					self.alert("服务器返回数据错误");
				} else {
					if (data.code == 200) {
						if(!!data.content.questionCount&&data.content.questionCount<=0){
							self.alert("问题长度为零，无法开启问卷！");
						}else{
							if(callback){
								callback(data.content);
							}
						}
					}else{
						self.alert(data.description);
					}
				}
			},
			error:function(e){
				alert(JSON.stringify(e));
			}
    	});
    },
    alert:function(text,callback){
    	Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
    }
});