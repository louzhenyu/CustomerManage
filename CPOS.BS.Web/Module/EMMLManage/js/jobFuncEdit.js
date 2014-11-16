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

            this.JobFunctionID = $.util.getUrlParam("JobFunctionID");

            this.txtName = $("#txtName");
            this.txtDescription = $("#txtDescription");
            this.ddlStatus = $("#ddlStatus");

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
        AddJobFunc: function (dataObj, callback) {
            this.ajax({
                url: this.url,
                data: {
                    action: "AddJobFunc",
                    interfaceType: "Product",
                    customerId: this.customerId,
                    userId: this.userId,
                    Name:this.txtName.val(),
                    Description: this.txtDescription.val(),
                    Status: this.ddlStatus.val()
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
        ModifyJobFunc: function (unitId, callback) {
            this.ajax({
                url: this.url,
                data: {
                    action: "ModifyJobFunc",
                    interfaceType: "Product",
                    customerId: this.customerId,
                    userId: this.userId,
                    JobFunctionID: this.JobFunctionID,
                    Name: this.txtName.val(),
                    Description: this.txtDescription.val(),
                    Status: this.ddlStatus.val()
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
            if (this.UnitID) {
                this.ajax({
                    url: this.url,
                    data: {
                        action: "GetDept",
                        interfaceType: "Product",
                        customerId: this.customerId,
                        userId: this.userId,
                        UnitID: this.UnitID
                    },
                    beforeSend: function () {
                        //self.isSending = true;
                        //$.native.loading.show();
                    },
                    success: function (data) {
                        if (data.IsSuccess) {
                            if (data.Data.UnitList != null) {
                                var singleUnit = data.Data.UnitList[0];
                                self.txtUnitCode.val(singleUnit.UnitCode);
                                self.txtUnitName.val(singleUnit.UnitName);
                                self.txtLeader.val(singleUnit.Leader);
                                self.txtDeptDesc.val(singleUnit.DeptDesc);
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
                if (this.UnitID != '') {
                    self.ModifyJobFunc(this.UnitID, function (data) {
                        alert(data.Message);
                    });
                } else {
                    var unitCode = "u1001";
                    self.AddJobFunc(unitCode, function (data) {
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