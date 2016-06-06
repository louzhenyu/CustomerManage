define(['jquery', 'template', 'tools', 'easyui','artDialog','kindeditor'], function ($) {
	KE = KindEditor;
    var page = {
        elems: {
            sectionPage: $("#section"),
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            thead: $("#thead"),                    //表格head部分
            operation: $('#operation'),              //弹出框操作部分
            dataMessage: $(".dataMessage"),
			editLayer: $("#editLayer"), //CSV文件上传
            click: true,
            panlH: 116
        },
		init: function () {
            var that = this;
            

            that.loadPageData();
			that.initEvent();
            //$("[name='VipCardCode']").focus();

            //导入会员初始化
            setTimeout(function () {
                that.inportStoreDialog();
            }, 500);

            that.registerUploadCSVFileBtn();
        },
        initEvent: function () {
            var that = this,
				$uiMask = $('.ui-mask'),
				$closeBtn = $('.closeBtn');
				
			//导入会员
            $('#inportvipmanageBtn').on('click', function () {
                $('.inputCount').text("0");
                $('#CSVFileurl').val("");
                that.elems.editLayer.find("#nofiletext").show();
                that.elems.editLayer.find(".CSVFilelist").empty();
                $('#startinport').show();
                $('#closebutton').hide();
                $('.step').hide();
                $('#step1').show();
                $('#win1').window('open');
            });
            //开始导入
            $('#win1').delegate(".saveBtn", "click", function (e) {
                var CSVFileurl = $('#CSVFileurl').val();
                if (CSVFileurl != "") {
                    $('#startinport').hide();
                    $('.step').hide();
                    $('#step2').show();
                    that.inportEvent();
                } else {
                    $.messager.alert("提示", '请选择文件！');
                }
            });
            //关闭
            $('#win1').delegate(".closeBtn", "click", function (e) {

                $('#win1').window('close');
            });
			
				
        },
        //加载页面的数据请求
        loadPageData: function (e) {
            var that = this;
			that.getVipGold(function(data){
				console.log(data);
				
				
				$('.onlineFansCount em').text(data.OnlineFansCount);	//Int	微信粉丝总数
				$('.onlineOnlyFansCount em').text(data.OnlineOnlyFansCount);	//Int	未完成微信注册数量
				$('.onlineVipCount em').text(data.OnlineVipCount);	//Int	已完成微信注册数量
				$('.vipCount em').text(data.VipCount);	//Int	会员总数
				$('.offlineVipCount em').text(data.OfflineVipCount);	//Int	未完成会员注册数量
				
				
				$('.onlineVipCountFor30DayOrder em').text(data.OnlineVipCountFor30DayOrder);	//Int	近30天会员活跃人数
				$('.onlineVipCountPerFor30DayOrder em').text(data.OnlineVipCountPerFor30DayOrder);
				
				
				$('.onlineVipCountFor30DayOrderM2M em').text(data.OnlineVipCountFor30DayOrderM2M);//String	近30天活跃会员占比{占注册会员总数}
				$('.onlineVipCountPerFor30DayOrderM2M em').text(data.OnlineVipCountPerFor30DayOrderM2M);//String	近30天活跃会员占比增减
				
				$('.onlineVipSalesFor30Day em').text(data.OnlineVipSalesFor30Day);//String	注册会员近30天销量贡献(额)
				$('.onlineVipSalesFor30DayM2M em').text(data.OnlineVipSalesFor30DayM2M);//String注册会员近30天销量贡献增比
				
				
				$('.onlineVipSalesPerFor30Day em').text(data.OnlineVipSalesPerFor30Day);//String	注册会员近30天销量贡献
				$('.onlineVipSalesPerFor30DayM2M em').text(data.OnlineVipSalesPerFor30DayM2M);//String	注册会员近30天销量贡献增比

			});
            //$.util.stopBubble(e);
        },
		getVipGold:function(callback){
			var that = this;
			$.util.ajax({
				url: "/ApplicationInterface/Gateway.ashx",
				data: {
					action: 'Report.VipGoldReport.GetVipGoldHomeList'
				},
				success: function (data) {
					if (data.IsSuccess && data.ResultCode == 0) {
						if (callback) {
							callback(data.Data);
						}
					} else {
						$.messager.alert("提示", data.Message);
					}
				}
			});
		},
		/*导入start*/
        //执行导入
        inportEvent: function () {
            var that = this;
            var mid = JITMethod.getUrlParam("mid");
            $.util.oldAjax({
                url: "/Module/Basic/VIP/Handler/VipHandler.ashx",
                data: {
                    "mid": $.util.getUrlParam('mid'),
                    "action": 'ImportVip',
                    filePath: '/' + $('#CSVFileurl').val()
                },
                success: function (data) {
                    $('.step').hide();
                    $('#step3').show();
                    $('#closebutton').show();

                    if (data.success) {
                        $('#inputTotalCount').text(data.data.TotalCount);
                        var success = 0;
                        success = data.data.TotalCount - data.data.ErrCount;
                        $('#inputErrCount').text(success < 0 ? 0 : success);
                        if (data.data.ErrCount > 0 || data.data.TotalCount == 0) {
                            $('#error_report').attr("href", data.data.Url);
                            $('.menber_centernrb1').show();
                        } else {
                            $('.menber_centernrb1').hide();
                        }
                    } else {
                        if (data.data != null) {
                            $('#error_report').attr("href", data.data.Url);
                            $('.menber_centernrb1').show();
                        } else {
                            $('.menber_centernrb1').hide();
                            $.messager.alert("导入错误提示", data.msg);
                        }
                    }
                    that.loadPageData();
                }
            });
        },
        //导入窗口第一步
        inportStoreDialog: function (data) {
            var that = this;
            $('#win1').window({ title: "导入会员", width: 740, height: 670, top: 15, left: ($(window).width() - 740) * 0.5 });
            // $('#win').window('open');
        },

        //CSV文件上传按钮绑定
        registerUploadCSVFileBtn: function () {
            var self = this;
            // 注册上传按钮
            self.elems.editLayer.find(".uploadCSVFileBtn").each(function (i, e) {
                self.addUploadCSVFileEvent(e);
            });
        },
        //上传CSV文件区域的各种事件绑定
        addUploadCSVFileEvent: function (e) {
            var self = this;
            var CSVFilelist = self.elems.editLayer.find(".CSVFilelist");


            //上传CSV文件并显示
            self.uploadCSVFile(e, function (ele, data) {
                CSVFilelist.empty();
                CSVFilelist.append('<a id="fileurl" href="' + data.file.url + '" >' + data.file.name + '</a>');
                $('#CSVFileurl').val(data.file.localurl);

            });

        },
        //上传CSV文件
        uploadCSVFile: function (btn, callback) {
            var self = this;
            var uploadbutton = KE.uploadbutton({
                button: btn,
                width: 100,
                //上传的文件类型
                fieldName: 'file',
                //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                url: '/Framework/Upload/UploadFile.ashx?method=file',
                afterUpload: function (data) {
                    debugger;
                    if (data.file.extension.toLocaleLowerCase() == ".xls" || data.file.extension.toLocaleLowerCase() == ".csv" || data.file.extension.toLocaleLowerCase() == ".xlsx") {
                        if (data.file.size < 1000001) {

                            if (data.success) {
                                self.elems.editLayer.find("#nofiletext").hide();
                                if (callback) {
                                    debugger;
                                    callback(btn, data);
                                }
                                //取返回值,注意后台设置的key,如果要取原值
                                //取缩略图地址
                                //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                                //取原图地址
                                //var url = KE.formatUrl(data.url, 'absolute');
                            } else {
                                $.messager.alert(data.msg);
                            }
                        } else {
                            $.messager.alert("提示", "请上传要小于1M的文件！");
                        }
                    } else {
                        $.messager.alert("提示", "上传的文件格式只能是.xls、.xlsx！");
                    }
                },
                afterError: function (str) {
                    $.messager.alert("提示", '自定义错误信息: ' + str);
                }
            });
            debugger;
            uploadbutton.fileBox.change(function (e) {
                uploadbutton.submit();
            });
        },
        /*导入end*/
		

		
		//数据格式转换
        NumberFormat: function (oldformatdata, callback){
            var isyuan = true;
            var result = 0;
            if (oldformatdata) {
                if (oldformatdata > 9999) {
                    
                    result = (oldformatdata / 10000).toFixed(2);
                    isyuan = false;
                } else {
                    result= oldformatdata.toFixed(0);
                }
            }
            if (callback) {
                callback(result, isyuan);
            }
        }
    };
    page.init();
});

