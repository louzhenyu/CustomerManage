define(['jquery', 'template', 'tools', 'langzh_CN', 'easyui', 'artDialog', 'touchslider'], function ($) {
    var page = {
        elems: {
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        initEvent: function () {
            var that = this,
				$notShow = $('.nextNotShow span');
			$notShow.on('click',function(){
				if($notShow.hasClass('on')){
					$notShow.removeClass('on');
				}else{
					$notShow.addClass('on');
				}
			})
            


            $(".touchslider").touchSlider({
                container: this,
                duration: 350, // 动画速度
                delay: 3000, // 动画时间间隔
                margin: 5,
                mouseTouch: true,
                namespace: "touchslider",
                next: ".touchslider-next", // next 样式指定
                pagination: ".touchslider-nav-item",
                currentClass: "touchslider-nav-item-current", // current 样式指定
                prev: ".touchslider-prev", // prev 样式指定
                autoplay: true, // 自动播放
                viewport: ".touchslider-viewport"
            });



            //点击申请活动
            $(".InSeasonList").on("click", ".apply", function (e) {
                debugger;

                that.Apply($(this).data("themeid"), function (data) {

                    if (data.errCode == "1") {

                        $('#win').window({
                            title: "申请活动", width: 550, height: 200, top: ($(window).height() - 200) * 0.5,
                            left: ($(window).width() - 550) * 0.5
                        });
                        //改变弹框内容，调用百度模板显示不同内容
                        $('#panlconent').layout('remove', 'center');
                        var html = bd.template('tpl_Applyfail', data);
                        var options = {
                            region: 'center',
                            content: html
                        };
                        $('#panlconent').layout('add', options);
                        $('#win').window('open');

                    } else {

                        $('#win').window({
                            title: "申请活动", width: 550, height: 380, top: ($(window).height() - 380) * 0.5,
                            left: ($(window).width() - 550) * 0.5
                        });
                        //改变弹框内容，调用百度模板显示不同内容
                        $('#panlconent').layout('remove', 'center');
                        var html = bd.template('tpl_Apply');
                        var options = {
                            region: 'center',
                            content: html
                        };
                        $('#panlconent').layout('add', options);
                        $('#win').window('open');
                    }
                });

            });

            //展示二维码
            $(".InSeasonList").on("mouseover", ".Scan", function (e) {
                $(".qcode img").attr("src", $(this).data("src"));
                $(".qcode").show();

            });


            //展示二维码
            $(".InSeasonList").on("mouseout", ".Scan", function (e) {
                $(".qcode").hide();


            });


            $('#win').delegate(".saveBtn", "click", function (e) {
                $('#win').window('close');
            });
           
            
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                //filed.value=filed.value=="0"?"":filed.value;
                //that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });
        },
		
        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.InSeasonList(function (data) {

                $(".InSeasonList").append(bd.template("tpl_InSeasonList", data));


                //赋值导航start
                var navhtml="<span class='thisseasontouchslider-prev'></span>";
                for (i = 0; i < data.ThemeList.length; i++)
                {
                    if(i==0)
                        navhtml+=" <span class='thisseasontouchslider-nav-item thisseasontouchslider-nav-item-current'></span>";
                    else
                        navhtml+=" <span class='thisseasontouchslider-nav-item '></span>";
                }
                navhtml += "<span class='thisseasontouchslider-next'></span>";

                $(".thisseasontouchslider-nav").html(navhtml);
                //赋值导航end

                $(".touchsliderthisseason").touchSlider({
                    container: this,
                    duration: 350, // 动画速度
                    delay: 2000, // 动画时间间隔
                    margin: 5,
                    mouseTouch: true,
                    namespace: "touchslider",
                    next: ".thisseasontouchslider-next", // next 样式指定
                    pagination: ".thisseasontouchslider-nav-item",
                    currentClass: "thisseasontouchslider-nav-item-current", // current 样式指定
                    prev: ".thisseasontouchslider-prev", // prev 样式指定
                    autoplay: true, // 自动播放
                    viewport: ".thisseasontouchslider-viewport"
                });

            });

            that.NextSeasonList(function (data) {

                $(".NextSeasonList").append(bd.template("tpl_NextSeasonList", data));
            });

            that.GetAllThemeList(function (data) {

                $(".seasonlist_ul").append(bd.template("tpl_seasonlist", data));
            });
			that.quicklyDialog();
        },
		
		
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            
           
        },
        //封面信息
        EventTheme: function (callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.EventTheme'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                       
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },
        //申请
        Apply: function (themeid,callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.Apply',
                    ThemeId: themeid
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (result) {
                            if (callback) {
                                callback(result);
                            }
                        }

                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },
        //当季活动列表
        InSeasonList: function (callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.InSeasonList'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (result)
                        {
                            if (callback) {
                                callback(result);
                            }
                        }

                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },
        //下季活动列表
        NextSeasonList: function (callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.NextSeasonList'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        
                        if (result) {
                            if (callback) {
                                callback(result);
                            }
                        }

                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },
        //所有活动列表
        GetAllThemeList: function (callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.GetAllThemeList'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (result) {
                            if (callback) {
                                callback(result);
                            }
                        }

                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },
       
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:15
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                item_category_id:null,
                SalesPromotion_id:null,
                form:{
                    item_code:"",
                    item_name:"",
                    item_startTime:'',
                    item_endTime:''
                }
            }
			 
        },
		
		quicklyDialog: function(){
			var that=this,
				$notShow = $('.nextNotShow span'),
				cooksName = '';
			$('#winQuickly').window({title:"快速上手",width:600,height:422,top:($(window).height() - 422) * 0.5,left:($(window).width() - 600) * 0.5,
			onClose:function(){
				if($notShow.hasClass('on')){
					$.util.setCookie('chainclouds_management_system_creative', 'zmind');
				}
				//var mid = JITMethod.getUrlParam("mid"),PMenuID = JITMethod.getUrlParam("PMenuID");
				//location.href = "/module/newVipManage/querylist.aspx?mid=" +mid+"&PMenuID="+PMenuID;
			}
			});
			cooksName = $.util.getCookie('chainclouds_management_system_creative');
			if(!cooksName){
				$(document).ready(function() {
					setTimeout(function(){
						$('#winQuickly').window('open');
					},1000);
				});
			}else{
				$(document).ready(function() {
					$('#winQuickly').window('close');
				});
			}
			//改变弹框内容，调用百度模板显示不同内容
			/*$('#panlconent').layout('remove','center');
			var html=bd.template('tpl_addProm');
			var options = {
				region: 'center',
				content:html
			};
			$('#panlconent').layout('add',options);*/
		}

    };
    page.init();
});


