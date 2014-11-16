define(['jquery', 'template', 'tools'], function () {
    var page = {
        ele: {
            section: $("#section")
        },
        page: {
            pageIndex: 0,
            pageSize: 10
        },
        init: function () {
            this.url = "/ApplicationInterface/Product/QiXinManage/QiXinManageHandler.ashx";
            this.customerId = "e703dbedadd943abacf864531decdac1"; //$.util.getUrlParam("customerId"); 
            this.userId = "02AA1B9C39E941F498B2D406D4EB32F8"; //$.util.getUrlParam("userId");   //"82B04CE0C05E4AFF9D2C51743B2E0A08";

            this.UserID = $.util.getUrlParam("UserID");
            if (this.UserID == undefined) {
                this.UserID = '';
                $("#DeptJobDiv").show();
            }

            this.txtUserCode = $("#txtUserCode");
            this.txtUserName = $("#txtUserName");
            this.txtUserNameEn = $("#txtUserNameEn");
            this.txtUserEmail = $("#txtUserEmail");
            this.txtUserTelephone = $("#txtUserTelephone");
            this.txtUserBirthday = $("#txtUserBirthday");
            this.txtUserCellphone = $("#txtUserCellphone");
            this.txtUserPassword = $("#txtUserPassword");
            this.ddlUserGender = $("#ddlUserGender");
            this.ddlLineManager = $("#ddlLineManager");
            this.ddlUserStatus = $("#ddlUserStatus");

            this.ddlRole = $("#ddlRole");
            this.ddlDept = $("#ddlDept");
            this.ddlJobFunc = $("#ddlJobFunc");
            
            this.grabed = false;
            if (!this.customerId) {
                alert("获取不到请求参数customerId！");
                return false;
            }
            if (!this.userId) {
                alert("获取不到请求参数userId！");
                return false;
            }

            //alert(this.userId);
            this.isSending = false;

            this.loadData();
            this.initEvent();
        },
        AddDept: function (dataObj, callback) {
            this.ajax({
                url: this.url,
                data: {
                    action: "AddDept",
                    interfaceType: "Product",
                    customerId: this.customerId,
                    userId: this.userId
                    , UserCode: this.txtUserCode.val()
                    , UserName: this.txtUserName.val()
                    , UserNameEn: this.txtUserNameEn.val()
                    , UserEmail: this.txtUserEmail.val()
                    , UserTelephone: this.txtUserTelephone.val()
                    , UserBirthday: this.txtUserBirthday.val()
                    , UserCellphone: this.txtUserCellphone.val()
                    , UserGender: this.ddlUserGender.val()
                    , UserStatus: this.ddlUserStatus.val()
                    , LineManagerID:this.ddlLineManager.val()
                    , RoleID: this.ddlRole.val()
                    , UnitID: this.ddlDept.val()
                    , JobFunctionID:this.ddlJobFunc.val()
                },
                beforeSend: function () {
                    self.isSending = true;
                    //$.native.loading.show();
                },
                success: function (data) {

                    self.grabed = true;
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data);
                        }
                    } else {
                        alert(data.Message);
                    }
                },
                complete: function () {
                    self.isSending = false;
                    //$.native.loading.hide();
                }
            })
        },
        ModifyDept: function (UserID, callback) {
            this.ajax({
                url: this.url,
                data: {
                    action: "ModifyUserPersonalInfo"
                    , interfaceType: "Product"
                    , customerId: this.customerId
                    , userId: this.userId
                    , UserID: this.UserID
                    , UserCode: this.txtUserCode.val()
                    , UserName: this.txtUserName.val()
                    , UserNameEn: this.txtUserNameEn.val()
                    , UserEmail: this.txtUserEmail.val()
                    , UserTelephone: this.txtUserTelephone.val()
                    , UserBirthday: this.txtUserBirthday.val()
                    , UserCellphone: this.txtUserCellphone.val()
                    , UserGender: this.ddlUserGender.val()
                    , LineManagerID:this.ddlLineManager.val()
                    , UserStatus: this.ddlUserStatus.val()
                },
                beforeSend: function () {
                    //self.isSending = true;
                    //$.native.loading.show();
                },
                success: function (data) {

                    if (data.IsSuccess) {
                        alert("提交成功");
                    } else {
                        alert("提交失败");
                    }
                },
                complete: function () {

                }
            });
        },
        buildAjaxParams: function (param) {
            var _param = {
                url: "",
                type: "get",
                dataType: "json",
                data: null,
                beforeSend: function () {

                },
                success: null,
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            };

            $.extend(_param, param);


            var action = param.data.action,
                interfaceType = param.data.interfaceType || 'Product',
                _req = {
                    'CustomerID': (param.data.customerId ? param.data.customerId : null),
                    'UserID': param.data.userId ? param.data.userId : null,
                    'Parameters': param.data
                };

            delete param.data.customerId;
            delete param.data.userId;
            delete param.data.action;
            delete param.data.interfaceType;

            var _data = {
                'req': JSON.stringify(_req)
            };

            _param.data = _data;

            _param.url = _param.url + '?type=' + interfaceType + '&action=' + action;

            return _param;
        },
        ajax: function (param) {

            var _param = this.buildAjaxParams(param);

            $.ajax(_param);
        },
        loadData: function () {
            if (this.UserID) {
                this.ajax({
                    url: this.url,
                    data: {
                        action: "GetUserList",
                        interfaceType: "Product",
                        customerId: this.customerId,
                        userId: this.userId,
                        UserID: this.UserID
                    },
                    beforeSend: function () {
                        //self.isSending = true;
                        //$.native.loading.show();
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (data.Data.UserList != null) {
                                var singleUser = data.Data.UserList[0];
                                self.txtUserCode.val(singleUser.UserCode);
                                self.txtUserName.val(singleUser.UserName);
                                self.txtUserNameEn.val(singleUser.UserNameEn);
                                self.txtUserEmail.val(singleUser.UserEmail);
                                self.txtUserBirthday.val(singleUser.UserBirthday);
                                self.txtUserTelephone.val(singleUser.UserTelephone);
                                self.txtUserCellphone.val(singleUser.UserCellphone);
                                self.txtUserPassword.val(singleUser.UserPassword);
                                self.ddlUserGender.val(singleUser.UserGender);
                                self.ddlLineManager.val(singleUser.LineManagerID);
                                self.ddlUserStatus.val(singleUser.UserStatus);
                            }
                        } else {
                            alert(data.Message);
                        }
                    },
                    error: function () {
                        alert("加载数据失败！")
                    },
                    complete: function () {
                        self.isSending = false;
                        $.native.loading.hide();
                    }
                });
            }
        },
        initEvent: function () {

            this.ele.section.delegate("#takeBtn", "click", function () {
                if (self.UserID != '' || self.UserID == '-1') {
                    self.ModifyDept(self.UserID, function (data) {
                        self.UserID = data.Data.UserID;
                        alert(data.Message);
                    });
                } else {
                    var UserCode = "u1001";
                    self.AddDept(UserCode, function (data) {
                        alert(data.Message);
                    });
                }
            }).delegate(".closeBtn", "click", function () {
            })
        }
    };

    self = page;

    page.init();

});