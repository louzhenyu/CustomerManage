var JITPage = {
    HandlerUrl: {
        _handlerUrl: '',
        setValue: function (value) {
            this._handlerUrl = value;
        },
        getValue: function (args) {
            return this._handlerUrl;
        }
    },
    PageSize: {
        _pageSize: 15,
        setValue: function (value) {
            this._pageSize = value;
        },
        getValue: function () {
            return this._pageSize;
        }
    },
    deleteList: function (args) {
        if (args.ids.length > 0) {
            Ext.MessageBox.confirm('提示信息', '确认删除?', function deldbconfig(btn) {
                if (btn == 'yes') {
                    Ext.Ajax.request({
                        url: args.url,
                        params: args.params,
                        method: 'post',
                        callback: function (options, isSuccess, response) {
                            var result = eval("(" + response.responseText + ")");
                            if (isSuccess && result.success) {
                                args.handler();
                            } else {
                                if (result != null && result.msg != null)
                                    Ext.Msg.alert("提示", result.msg);
                                else
                                    Ext.Msg.alert("提示", "删除失败");
                            }
                        }
                        //                        success: function (response) {
                        //                            try {
                        //                                var jdata = eval(response.responseText);
                        //                                if (jdata[0].success) {
                        //                                    args.handler();
                        //                                }
                        //                                else {
                        //                                    Ext.Msg.alert("提示", "删除失败");
                        //                                }
                        //                            }
                        //                            catch (e) {
                        //                                Ext.Msg.alert("提示", "删除失败");
                        //                            }
                        //                        },
                        //                        failure: function () {
                        //                            Ext.Msg.alert("提示", "删除失败");
                        //                        }
                    });
                }
            });
        } else {
            Ext.Msg.alert("提示", "请选择需要删除的行");
        }
    },
    //此方法为2013.4.24新增方法 by tj
    delList: function (args) {
        if (args.ids.length > 0) {
            Ext.MessageBox.confirm('提示信息', '确认删除?', function deldbconfig(btn) {
                if (btn == 'yes') {
                    Ext.Ajax.request({
                        url: args.url,
                        params: args.params,
                        method: 'post',
                        success: function (response) {
                            try {
                                var jdata = Ext.JSON.decode(response.responseText);
                                //                                if (!JITPage.checkAjaxPermission(jdata)) {
                                //                                    return;
                                //                                }
                                if (jdata.success) {
                                    Ext.Msg.alert("提示", "删除成功");
                                    args.handler();
                                }
                                else {
                                    var resmsg = "删除失败";
                                    if (jdata.msg) {
                                        resmsg = jdata.msg;
                                    }
                                    Ext.Msg.alert("提示", resmsg);
                                }
                            }
                            catch (e) {
                                Ext.Msg.alert("提示", "删除失败");
                            }
                        },
                        failure: function () {
                            Ext.Msg.alert("提示", "删除失败");
                        }
                    });
                }
            });
        } else {
            Ext.Msg.alert("提示", "请选择需要删除的行");
        }
    },
     //通过返回数据检查操作权限
    checkAjaxPermission: function (jdata) {
        var hasPermission = false;
        if (jdata.success == undefined) {
            //非数据提交， 属于getbyid 或者其它获取数据，这里暂不提示
            hasPermission = true;
        }
        else {
            if (jdata.success) { //continue
                hasPermission = true;
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: jdata.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
                hasPermission = false;
            }
        }
        return hasPermission;
    },
    // 获取grid id集合
    getSelected: function (args) {
        var ids = new Array();
        if (args.gridView.getSelectionModel().hasSelection()) {
            Ext.each(args.gridView.getSelectionModel().getSelection(),
					function (item) {
					    ids.push(item.data[args.id]);
					});
        }
        return ids;
    },
    //通过id获取button code
    getCUCodeByID: function () {
    },
    //以下为页面UI相关
    Msg: {
        GetData: "系统处理中...", //获取数据
        SubmitDataTitle: " ", //提交数据-title
        SubmitData: "系统处理中..."//提交数据信息
    },
    Layout: {
        OperateWidth: 50//操作列宽度
    }
}