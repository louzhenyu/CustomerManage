define(['tools', 'template', 'kkpager', 'artDialog', 'json2','easyui', 'ajaxform'], function () {
    var page =
        {
            notifyUrl: '',
            //关联到的类别
            elems:
            {
                uiMask: $(".jui-mask"),
                dataObj: {}
            },

            init: function () {
                var that = this;
                //var pageType = $.util.getUrlParam("pageType");
                //var pageName = decodeURIComponent($.util.getUrlParam("pageName"));

                that.queryPayMentList(function (data) {
                    that.notifyUrl = data.url;
                });
                that.initEvent();
				
				$('#WXJS_parnterkey').val(that.randomString(32));
                //that.pager();
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
            //显示遮罩层
            showMask: function (flag, type) {
                if (!!!flag) {
                    this.elems.uiMask.hide();
                    this.elems.chooseEventsDiv.hide();
                }
                else {
                    this.elems.uiMask.show();
                    //动态的填充弹出层里面的内容展示
                    this.loadPopUp(type);
                    this.elems.chooseEventsDiv.show();
                }
            },
            //显示弹层
            showElements: function (selector) {
                this.elems.uiMask.show();
                $(selector).slideDown(500);
                var tops=$(document).scrollTop()+70;
                $(".jui-dialog").css({"top":tops+"px"})
            },
            hideElements: function (selector) {
                this.elems.uiMask.fadeIn(500);
                $(selector).fadeIn(500);
            },
            alert: function (content) {
                var d = dialog({
                    fixed: true,
                    title: '提示',
                    content: content
                });
                d.showModal();
                setTimeout(function () {
                    d.close().remove();
                }, 3500);
            },
            pager: function () {
                var that = this;
                kkpager.generPageHtml({
                    pno: 1,
                    mode: 'click', //设置为click模式
                    //总页码  
                    total: 2,
                    isShowTotalPage: false,
                    isShowTotalRecords: false,
                    isGoPage: false,
                    //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                    //适用于不刷新页面，比如ajax
                    click: function (n) {
                        //这里可以做自已的处理
                        //...
                        //处理完后可以手动条用selectPage进行页码选中切换
                        this.selectPage(n);

                        //that.loadMoreData(n);
                    },
                    //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                    getHref: function (n) {
                        return '#';
                    }

                }, true);
            },
            loadMoreData: function (currentPage) {
                var that = this;
                this.loadData.args.PageIndex = currentPage - 1;
                this.loadData.getEventsList(function (data) {
                    var list = data.Data.PanicbuyingEventList;
                    list = list ? list : [];
                    $.each(list, function (i) {
                        list[i].pageStr = (that.pageStr ? that.pageStr : "支付");
                    });
                    var html = bd.template("tpl_content", { list: list })
                    $("#goodsList").html(html);
                });
            },
            initEvent: function () {
                //初始化事件集
                var that = this,
					$tr;
                //单选框
                $('.jui-dialog-payMent .radioBox').bind('click', function () {
                    var $this = $(this);
                    $('#jui-dialog-' + that.elems.dataObj.typecode + ' .radioBox').removeClass('on');
                    $this.addClass('on');
                    if ($this.data('radio') == 'disable') {
                        $this.parents('.payMentContent').find('input').attr('disabled', 'disabled').parent().css({ 'border': 'none' });
                        $this.parents('.payMentContent').find('.uploadFileBox').hide();
						
						$('.jui-dialog-payMent .checkBox').addClass('disable');
						
                    } else {
                        $this.parents('.payMentContent').find('input').attr('disabled', false).parent().css({ 'border': '1px solid #dedede' });
                        $this.parents('.payMentContent').find('.uploadFileBox').show();
						if (that.elems.dataObj.IsNativePay != 0) {
							that.elems.dataObj.action = "SetPayChannel" //启用商户支付
						}
						$('#WXJS_parnterkey').attr('disabled', 'disabled');
						
						$('.jui-dialog-payMent .checkBox').removeClass('disable');
						if($('#scanCodePay').hasClass('on')){
							$('#AlipayWap_appid').attr('disabled', false);
						}else{
							$('#AlipayWap_appid').attr('disabled', true);
						}
						$('#pagePay').addClass('on');
                    }
                    
                    //$("#text").disable = ture;
                });
                //复选框选择
                $('.jui-dialog-payMent').delegate('.checkBox', 'click', function () {
                    var $this = $(this),
						isDisable = $this.hasClass('disable'),
						isOn = $this.hasClass('on'),
						payWay = $this.data('value');
					if(isDisable){
						return false;
					}else{
						 if(isOn){
							$this.removeClass('on');
							if(payWay=='scanCodePay'){
								$('#AlipayWap_appid').attr('disabled',true);
							}
						}else{
							$this.addClass('on');
							if(payWay=='scanCodePay'){
								$('#AlipayWap_appid').attr('disabled',false);
							}
						}
					}
					
					
                });

                //编辑操作
                $('.tableWrap').delegate('.operateWrap', 'click', function (e) {
                    var $this = $(this);
                    $tr = $('.unstart', $this.parent());
                    that.elems.dataObj.typecode = $this.data('typecode');
                    that.elems.dataObj.typeid = $this.data('typeid');
                    that.elems.dataObj.channelid = $this.data('channelid');
                    that.elems.dataObj.userType = $this.data("usertype");
                    that.elems.dataObj.action = "SetDefaultPayChannel"//启用商户 和启用阿拉丁调用不同的方法。 默认SetDefaultPayChannel
                    that.showElements('#jui-dialog-' + that.elems.dataObj.typecode);
                    var index = 0;
                    var userType = eval('(' + $this.data("usertype") + ')');//IsOpen 是否启用 IsDefault 是否是默认（阿拉丁）是否是商户IsCustom
					that.elems.dataObj.IsNativePay = userType.IsNativePay;

                    if (userType.IsOpen) {
						index = 0
                        //if(userType.IsCustom){}
						$('#pagePay').addClass('on');
                    } else {
                        index = 1;
                    }
                    $('#jui-dialog-' + that.elems.dataObj.typecode + ' .radioBox').eq(index).trigger('click');
					that.getDefaultInfo(function(params){
						if(that.elems.dataObj.typecode == 'CCAlipayWap'){
							
						}else if(that.elems.dataObj.typecode == 'WXJS') {
							$("#WXJS_appid").val(params.AccountIdentity);//微信号APPID
							$("#WXJS_parnterid").val(params.TenPayIdentity);//微信支付商户号
							$("#WXJS_parnterkey").val(params.TenPayKey || that.randomString(32));//API秘钥
						}else if(that.elems.dataObj.typecode == 'AlipayWap') {//支付宝wap
                            $("#AlipayWap_id").val(params.PayAccountNumber);//合作者ID
							$("#AlipayWap_tbid").val(params.SalesTBAccess);//支付宝账号
							$("#AlipayWap_publicKey").val(params.PayAccounPublic);//支付宝公钥
							$("#AlipayWap_privateKey").val(params.PayPrivate);//私钥
							if(params.EncryptionCertificate){
								$("#AlipayWap_appid").val(params.EncryptionCertificate);//服务商应用APPID
								$('#scanCodePay').addClass('on');
							}
                        }
					});
					
                    that.stopBubble(e);
                });
                //关闭弹出层
                $(".jui-dialog-close").bind("click", function () {
                    that.elems.uiMask.slideUp();
                    $(this).parents('.jui-dialog').fadeOut();
                });
                $('.jui-dialog').delegate('.cancelBtn', 'click', function () {
                    that.elems.uiMask.slideUp();
                    $(this).parents('.jui-dialog').fadeOut();
                });


                //点击上传文件按钮
                $('.jui-dialog').delegate('.uploadFileBox', 'click', function () {
                    var $this = $(this),
						randomNum = Math.ceil(Math.random() * 1000);
                    $this.append('<form id="form' + randomNum + '" action="/Framework/Upload/UploadFile.ashx" method="post" enctype="multipart/form-data" ><input id="file' + randomNum + '" name="file' + randomNum + '" type="file" value=""/></form>').attr('class', 'uploadFileBox01');
                    $("#file" + randomNum).change(function () {
                        if ($(this).val()) {
                            $("#form" + randomNum).ajaxSubmit({
                                success: function (data) {
                                    var data = JSON.parse(data);
                                    if (data.success == true)
                                        $('input', $this.siblings('p')).val(data.file.url);
                                    else {
                                        alert(data.msg);
                                    }
                                }
                            });
                        }
                    });

                });


                //点击保存按钮
                $('.jui-dialog').delegate('.saveBtn', 'click', function () {
                    var $this = $(this),
						radioValue = $('.radioBox.on', $this.parents('.payMentContent')).data('value');

                    var paymentId = that.elems.dataObj.typeid;
                    var paymentCode = that.elems.dataObj.typecode;
                    var channelId = that.elems.dataObj.channelid;
                    if (channelId == "" || channelId == null) {
                        channelId = 0;
                    }

                    if (radioValue == 'unalading'){
                        that.disablePayment(function () {
                            alert('停用成功');
                            $tr.text('未启用').addClass('blue');
                            that.elems.uiMask.slideUp();
                            $this.parents('.jui-dialog').fadeOut();
                            that.queryPayMentList(function (data) {
                                that.notifyUrl = data.url;
                            });
                        });
                    } else {
                        var AddPayChannelList = new Array();
                        var pay = {};
                        var obj = {};
                        var WapData = {};
                        pay.PaymentTypeId = paymentId;
                        pay.ChannelId = channelId;
                        //判断是哪种支付方式
                        if (paymentCode == 'AlipayWap') {//支付宝wap

                            pay.PayType = "3";

                            if (radioValue == 'busine') {
                                if ($("#AlipayWap_id").val() == '') {
                                    that.alert('账号不能为空！');
                                    return;
                                }
								if ($("#scanCodePay").hasClass('on') && $("#AlipayWap_appid").val() == '') {
                                    that.alert('服务商应用APPID不能为空！');
                                    return;
                                }
                                if ($("#AlipayWap_tbid").val() == '') {
                                    that.alert('卖家淘宝账号不能为空！');
                                    return;
                                }
                                if ($("#AlipayWap_publicKey").val() == '') {
                                    that.alert('支付宝公钥不能为空！');
                                    return;
                                }
                                if ($("#AlipayWap_privateKey").val() == '') {
                                    that.alert('私钥不能为空！');
                                    return;
                                }

                                pay.NotifyUrl = that.notifyUrl;
                                WapData = {
                                    "Partner": $("#AlipayWap_id").val(),
									"SCAN_AppID": $("#AlipayWap_appid").val(),
                                    "SellerAccountName": $("#AlipayWap_tbid").val(),
                                    "RSA_PublicKey": $("#AlipayWap_publicKey").val(),
                                    "RSA_PrivateKey": $("#AlipayWap_privateKey").val(),
                                    "MD5Key": ""
                                };
                            }

                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;


                        } else if (paymentCode == 'AlipayOffline') {
                            pay.PayType = "4";

                            if (radioValue == 'busine') {
                                if ($("#AlipayOffline_id").val() == '') {
                                    that.alert('账号不能为空！');
                                    return;
                                }
                                if ($("#AlipayOffline_md5").val() == '') {
                                    that.alert('秘钥不能为空！');
                                    return;
                                }
                                pay.NotifyUrl = that.notifyUrl;
                                WapData = {
                                    "Partner": $("#AlipayOffline_id").val(),
                                    "MD5Key": $("#AlipayOffline_md5").val()
                                };
                            }

                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;

                        } else if (paymentCode == 'WXJS') {
                            //pay.PayType = "6";

                            if (radioValue == 'busine') {
                                if ($("#WXJS_appid").val() == '') {
                                    that.alert('微信号APPID不能为空！');
                                    return;
                                }
                                if ($("#WXJS_parnterid").val() == '') {
                                    that.alert('微信支付商户号不能为空！');
                                    return;
                                }
                                if ($("#WXJS_parnterkey").val() == '') {
                                    that.alert('API秘钥不能为空！');
                                    return;
                                }

                                pay.PayType = "6";
                                pay.NotifyUrl = that.notifyUrl;
                                var WxPayData = {
                                    "AppID": $("#WXJS_appid").val(),
                                    "Mch_ID": $("#WXJS_parnterid").val(),
                                    "SignKey": $("#WXJS_parnterkey").val(),
									"Trade_Type": "JSAPI"
                                };
                                pay.WxPayData = WxPayData;
                            }

                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;

                        } else if (paymentCode == 'CupWap') {//银联网页支付
                            pay.PayType = "1";

                            if (radioValue == 'busine') {
                                if ($("#CupWap_merchantid").val() == '') {
                                    that.alert('账号ID不能为空！');
                                    return;
                                }
                                if ($("#CupWap_certificatecilepath").val() == '') {
                                    that.alert('加密证书不能为空！');
                                    return;
                                }
                                if ($("#CupWap_certificatefilepassword").val() == '') {
                                    that.alert('加密密码不能为空！');
                                    return;
                                }
                                if ($("#CupWap_decryptcertificatefilepath").val() == '') {
                                    that.alert('解密证书不能为空！');
                                    return;
                                }
                                if ($("#CupWap_packetencryptkey").val() == '') {
                                    that.alert('解密密码不能为空！');
                                    return;
                                }

                                pay.NotifyUrl = that.notifyUrl;
                                var UnionPayData = {
                                    "MerchantID": $("#CupWap_merchantid").val(),
                                    "CertificateFilePath": $("#CupWap_certificatecilepath").val(),
                                    "CertificateFilePassword": $("#CupWap_certificatefilepassword").val(),
                                    "DecryptCertificateFilePath": $("#CupWap_decryptcertificatefilepath").val(),
                                    "PacketEncryptKey": $("#CupWap_packetencryptkey").val()
                                };
                                pay.UnionPayData = UnionPayData;
                            }

                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;

                        } else if (paymentCode == 'CupVoice') {
                            pay.PayType = "2";

                            if (radioValue == 'busine') {
                                if ($("#CupVoice_merchantid").val() == '') {
                                    that.alert('账号ID不能为空！');
                                    return;
                                }
                                if ($("#CupVoice_certificatecilepath").val() == '') {
                                    that.alert('加密证书不能为空！');
                                    return;
                                }
                                if ($("#CupVoice_certificatefilepassword").val() == '') {
                                    that.alert('加密密码不能为空！');
                                    return;
                                }
                                if ($("#CupVoice_decryptcertificatefilepath").val() == '') {
                                    that.alert('解密证书不能为空！');
                                    return;
                                }
                                if ($("#CupVoice_packetencryptkey").val() == '') {
                                    that.alert('解密密码不能为空！');
                                    return;
                                }

                                pay.NotifyUrl = that.notifyUrl;
                                var UnionPayData = {
                                    "MerchantID": $("#CupVoice_merchantid").val(),
                                    "CertificateFilePath": $("#CupVoice_certificatecilepath").val(),
                                    "CertificateFilePassword": $("#CupVoice_certificatefilepassword").val(),
                                    "DecryptCertificateFilePath": $("#CupVoice_decryptcertificatefilepath").val(),
                                    "PacketEncryptKey": $("#CupVoice_packetencryptkey").val()
                                };
                                pay.UnionPayData = UnionPayData;
                            }

                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;

                        } else if (paymentCode == 'CustomerSelfPay') {//
                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;

                        } else if (paymentCode == 'GetToPay') {
                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;
                        } else if(paymentCode == 'CCAlipayWap'){
							//待开发
							pay.PayType = "3";
							pay.NotifyUrl = that.notifyUrl;
                            pay.WapData = WapData;
                            AddPayChannelList[0] = pay;
						}

                        obj.Parameters = {
                            'AddPayChannelData': AddPayChannelList
                        };
                        obj = JSON.stringify(obj);
                        that.startPayment(obj, function () {
                            //location.reload();
                            alert('启用成功');
                            $('input', $this.parents('.jui-dialog')).val('');
                            $tr.text('已启用').removeClass('blue');
                            that.elems.uiMask.slideUp();
                            $this.parents('.jui-dialog').fadeOut();
                            that.queryPayMentList(function (data) {
                                that.notifyUrl = data.url;
                            });
                        });

                    }

                });

            },
            queryPayMentList: function (callback) {
                $.ajax({
                    url: '/Module/PayMent/Handler/PayMentHander.ashx?mid=' + __mid + "&method=getPayMentTypePage", //查询支付列表接口
                    type: "post",
                    data:
					{
					    'form': {
					        "Payment_Type_Name": "",
					        "Payment_Type_Code": ""
					    },
					    'page': 1
					},
                    success: function (data) {
                        var data = JSON.parse(data),
							list = data.topics;
                       /* var html = bd.template("tpl_payMentList", { list: list })
                        $("#payMentList").html(html);*/
                        $("#payMentList").datagrid({
                            method: 'post',
                            iconCls: 'icon-list', //图标
                            singleSelect: true, //单选
                            // height : 332, //高度
                            fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                            striped: true, //奇偶行颜色不同
                            collapsible: true,//可折叠
                            //数据来源
                            data: list,
                            /*sortName : 'MembershipTime', //排序的列*/
                            sortOrder: 'desc', //倒序
                            remoteSort: true, // 服务器排序
                            idField: 'OrderID', //主键字段

                            columns: [[

                                {field: 'PaymentTypeName', title: '支付方式', width: 196, align: 'left', resizable: false,
                                    formatter:function(value ,row,index){

                                        if(row.IsNativePay==0){
                                            return '平台'+value
                                        }else{
                                            return value
                                        }
                                    }
                                },
                                {field: 'IsOpen', title: '状态', width: 182, align: 'left', resizable: false,
                                    formatter:function(value ,row,index){

                                        if(value=='true'){
                                            return '<div class="unstart">已启用</div>'
                                        }else{
                                            return '<div class="unstart blue">未启用</div>'
                                        }
                                    }
                                },
                                {
                                    field: 'deliveryId', title: '编辑', width: 46, align: 'left', resizable: false,
                                    formatter: function (value, row, index) {
                                        debugger;
                                      var obj= "{'IsOpen':"+row.IsOpen+",'IsDefault':"+row.IsDefault+",'IsCustom':"+row.IsCustom+",'IsNativePay':"+row.IsNativePay+"}";
                                        return '<div class="operateWrap" title="编辑" data-usertype="'+obj+'" data-typecode="'+row.PaymentTypeCode+'" data-typeid="'+row.PaymentTypeID+'" data-channelid="'+row.ChannelId+'" > <span class="editIcon opt exit"></span> </td>';
                                    }
                                }
                            ]]
                        });
                        if (data.totalCount > 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //启用账号接口调用
            startPayment: function (params, callback) {
                var that = this;
                $.ajax({
                    url: '/ApplicationInterface/PayChannel/PayChannelGateway.ashx?type=Product&action=' + that.elems.dataObj.action,
                    type: "post",
                    data:
					{
					    'req': params
					},
                    success: function (data) {
                        var data = JSON.parse(data);
                        if (data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                                that.queryPayMentList();
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
            //停用账号接口调用
            disablePayment: function (callback) {
                var that = this;
                $.ajax({
                    url: '/Module/PayMent/Handler/PayMentHander.ashx?type=Product&method=disablePayment',
                    type: "post",
                    data:
					{
					    'paymentTypeId': that.elems.dataObj.typeid
					},
                    success: function (data) {
                        var data = JSON.parse(data);
                        if (data.ResultCode == 0) {
                            //表示成功
                            if (callback) {
                                callback(data);
                            }
                        } else {
                            alert(data.Message);
                        }
                    }
                });
            },
			randomString: function(len) {
			　　len = len || 32;
			　　var $chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';    /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/
			　　var maxPos = $chars.length;
			　　var pwd = '';
			　　for (i = 0; i < len; i++) {
			　　　　pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
			　　}
			　　return pwd;
			},
			getDefaultInfo: function(callback) {
				$('#dialog').hide();
                var that = this;
                $.ajax({
                    url: '/Module/PayMent/Handler/PayMentHander.ashx?type=Product&method=getMapingbyPayMentTypeId',
                    type: "post",
                    data:
					{
					    'paymentTypeId': that.elems.dataObj.typeid
					},
                    success: function (data) {
                        var data = JSON.parse(data);
                        //表示成功
						if (callback) {
							callback(data);
						}
                    }
                });
            },

        };

    page.init();
});