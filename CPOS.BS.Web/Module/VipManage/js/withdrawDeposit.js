define(['jquery', 'template', 'tools', 'kkpager', 'artDialog','datetimePicker','tips','zTree'], function ($) {
    window.alert = function (content, autoHide) {
        var d = dialog({
            title: '提示',
            cancelValue: '关闭',
            skin: "black",
            content: content
        });
        page.d = d;
        d.showModal();
        if (autoHide) {
            setTimeout(function () {
                page.d.close();
            }, 2000);
        }
    }
    var page = {  
		params:{
			pageIndex: 0,
			withdrawNo:'',
			vipName:'',
			status:''
		},
        elems: {
            sectionPage:$("#section"), 
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#content"),                   //表格body部分
            thead:$("#thead"),                    //表格head部分
            dialogLabel:$("#dialogLabel"),          //标签选择层
            menuItems:$("#menuItems"),             //针对表格的操作菜单
            resultCount: $("#resultCount"),          //所有匹配的结果
            vipSourceId:''
        },
        select:{
            isSelectAllPage:false,                 //是否是选择所有页面
            tagType:[],                             //标签类型
            tagList:[]                              //标签列表
        },
        init: function () {
            var that = this;
			//显示表格信息
            that.loadPageData();
			//绑定事件
            that.initEvent();
        },
		//加载页面的数据请求
        loadPageData: function () {
            var that = this,
				$resultCount = $('#resultCount');
            var myMid = JITMethod.getUrlParam("mid");
			that.getTableInfo(function(data){
				if (data.Data.TotalPageCount > 1) {
					kkpager.generPageHtml(
						{
							pno: page.params.pageIndex,
							mode: 'click', //设置为click模式
							//总页码  
							total: data.Data.TotalPageCount,
							totalRecords:data.Data.TotalCount,
							isShowTotalPage: true,
							isShowTotalRecords: true,
							//点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
							//适用于不刷新页面，比如ajax
							click: function (n) {
								//这里可以做自已的处理
								//...
								//处理完后可以手动条用selectPage进行页码选中切换
								this.selectPage(n);
								//点击下一页或者上一页,进行加载数
								page.params.pageIndex = --n;
								that.getTableInfo();
							},
							//getHref是在click模式下链接算法，一般不需要配置，默认代码如下
							getHref: function (n) {
								return '#';
							}
	
						}, true);
	
				}
			});
			
        },
        initEvent: function () {
            var that = this;
			//绑定搜索按钮事件
			$('.queryBtn').on('click',function(){
				var text = $('.selectBox .text').html();
				
				page.params.withdrawNo = $('#oddNumber').val();
				page.params.vipName = $('#vipName').val();
				
				if(text == '请选择'){
					page.params.status = '';
				}else if(text == '待确认'){
					page.params.status = 0;
				}else if(text == '已确认'){
					page.params.status = 1;
				}else if(text == '已完成'){
					page.params.status = 2;
				}
				
				that.getTableInfo();
				
			});
			
			//绑定状态下拉框选择事件
			$('.selectList').delegate('p','click',function(){
				var $this = $(this),
					textValue = $this.text();
				$('.selectBox .text').html(textValue);
			})
			
			
			
			//绑定复选框事件
			$('#content').delegate('.checkBox','click',function(){
				var $this = $(this);
				if($this.hasClass('on')){
					$this.removeClass('on');
				}else{
					$this.addClass('on');
				}
			})
			
			//绑定确认按钮事件
			$('#menuItems .affirmBtn,#menuItems .finishBtn').on('click',function(){
				var $this = $(this),
					$checkBox = $('#content .checkBox'),
					$checkBoxOn = $('#content .checkBox.on'),
					leng = $checkBox.length,
					onLong = $checkBoxOn.length,
					statusId = $this.data('statusid'),
					applyId = '';
				if(!!leng){
					if(!!onLong){
						for(var i=0;i<onLong;i++){
							if($checkBoxOn.eq(i).hasClass('on')){
								applyId += $checkBoxOn.eq(i).data('id')+',';
							}
						}
						//提交接口
						that.submitAuditStatus(statusId,applyId);
					}else{
						window.alert('还没有选中提现的记录！',true);
					}
					
				}else{
					window.alert('还没有提现的记录！',true);
				}
				
			})
			
        },
		submitAuditStatus: function(statusId,applyId,callback){
			$.util.ajax({
				url: "/ApplicationInterface/Vip/WithdrawDepositGateway.ashx",
				data: {
					action: 'UpdateWDApply',
					ApplyID: applyId,
					Status: statusId
				},
				success: function (data) {
					if (data.ResultCode == 0) {
						location.reload();
						if(callback){
							callback(data);
						}
							
					}
					else {
						alert(data.Message);
					}
				}
			});
		},
		getTableInfo: function(callback){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Vip/WithdrawDepositGateway.ashx",
				data: {
					action: 'GetWithdrawDeposit',
					PageSize: 10,
					PageIndex: page.params.pageIndex,
					WithdrawNo:page.params.withdrawNo,
					VipName:page.params.vipName,
					Status:page.params.status
				},
				success: function (data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						var listData = data.Data.WithdrawDepositList,
							htmlStr = '';
						for(var i=0;i<listData.length;i++){
							htmlStr += bd.template("tpl_content", listData[i]);
						}
						$("#content").html(htmlStr);
						
						if(callback){
							callback(data);
						}

					}
					else {
						alert(data.Message);
					}
				}
			});
		},
        stopBubble: function (e) {
            if (e && e.stopPropagation) {
                //因此它支持W3C的stopPropagation()方法 
                e.stopPropagation();
            }
            else {
                //否则，我们需要使用IE的方式来取消事件冒泡 
                window.event.cancelBubble = true;
            }
            e.preventDefault();
        },
		//通过隐藏form下载文件,导出文件
		exportVipList: function () {
			var getUrl = '/ApplicationInterface/Vip/VipGateway.ashx?type=Product&action=ExportVipList';//&req=';
			var data = {
				Parameters: {
					SearchColumns: [],
					PageSize: 10,
					PageIndex: 1,
					OrderBy: 'CREATETIME',
					SortType: 'DESC',
					VipSearchTags:  []
				}
			};
			var dataLink = JSON.stringify(data);
			var form = $('<form>');
			form.attr('style', 'display:none;');
			form.attr('target', '');
			form.attr('method', 'post');
			form.attr('action', getUrl);
			var input1 = $('<input>');
			input1.attr('type', 'hidden');
			input1.attr('name', 'req');
			input1.attr('value', dataLink);
			$('body').append(form);
			form.append(input1);
			form.submit();
			form.remove();
		}

    };
    page.init();
});