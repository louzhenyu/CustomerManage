

function fnSubmit() {
    var form = Ext.getCmp("parameterEditPanel").getForm();
    //字符-文本框验证 by zhongbao.xiao 2013.5.29
    if (Ext.getCmp("ControlType").jitGetValue() == 1) {

        if (Ext.getCmp("Num_MaxValue").jitGetValue() != null && Ext.getCmp("Num_MaxValue").jitGetValue() != "" && Ext.getCmp("Num_MinValue").jitGetValue() != null && Ext.getCmp("Num_MinValue").jitGetValue() != "") {
            if (Ext.getCmp("Num_MaxValue").jitGetValue() < Ext.getCmp("Num_MinValue").jitGetValue()) {
                Ext.Msg.show({
                    title: '提示',
                    msg: "最大字符长度不可小于最小字符长度",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                return false;
            }
        }

        if (Ext.getCmp("txt_DefaultValue").jitGetValue().length > Ext.getCmp("Num_MaxValue").jitGetValue()) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
        if (Ext.getCmp("txt_DefaultValue").jitGetValue().length < Ext.getCmp("Num_MinValue").jitGetValue()) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
    }
    //整数-文本框验证 by zhongbao.xiao 2013.5.29
    if (Ext.getCmp("ControlType").jitGetValue() == 2) {
        if (Ext.getCmp("Num_MaxValue").jitGetValue() != null && Ext.getCmp("Num_MaxValue").jitGetValue() != "" && Ext.getCmp("Num_MinValue").jitGetValue() != null && Ext.getCmp("Num_MinValue").jitGetValue() != "") {
            if (Ext.getCmp("Num_MaxValue").jitGetValue() < Ext.getCmp("Num_MinValue").jitGetValue()) {
                Ext.Msg.show({
                    title: '提示',
                    msg: "最大值不可小于最小值",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                return false;
            }
        }
        if (Ext.getCmp("Num_DefaultValue").jitGetValue() > Ext.getCmp("Num_MaxValue").jitGetValue()) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
        if (Ext.getCmp("Num_DefaultValue").jitGetValue() < Ext.getCmp("Num_MinValue").jitGetValue()) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
    }
    //小数-文本框验证 by zhongbao.xiao 2013.5.29
    if (Ext.getCmp("ControlType").jitGetValue() == 3) {
        if (Ext.getCmp("MaxValue").jitGetValue() != null && Ext.getCmp("MaxValue").jitGetValue() != "" && Ext.getCmp("MinValue").jitGetValue() != null && Ext.getCmp("MinValue").jitGetValue() != "") {
            if (Ext.getCmp("MaxValue").jitGetValue() < Ext.getCmp("MinValue").jitGetValue()) {
                Ext.Msg.show({
                    title: '提示',
                    msg: "最大值不可小于最小值",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                return false;
            }
        }
        if (parseFloat(Ext.getCmp("Del_DefaultValue").jitGetValue()) > parseFloat(Ext.getCmp("MaxValue").jitGetValue())) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
        if (parseFloat(Ext.getCmp("Del_DefaultValue").jitGetValue()) < parseFloat(Ext.getCmp("MinValue").jitGetValue())) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
    }
    //拍照验证 by zhongbao.xiao 2013.5.29
    if (Ext.getCmp("ControlType").jitGetValue() == 12) {
        if (Ext.getCmp("Num_MaxValue").jitGetValue() != null && Ext.getCmp("Num_MaxValue").jitGetValue() != "" && Ext.getCmp("Num_MinValue").jitGetValue() != null && Ext.getCmp("Num_MinValue").jitGetValue() != "") {
            if (Ext.getCmp("Num_MaxValue").jitGetValue() < Ext.getCmp("Num_MinValue").jitGetValue()) {
                Ext.Msg.show({
                    title: '提示',
                    msg: "最多张不可小于最少张",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                return false;
            }
        }
        if (Ext.getCmp("Num_DefaultValue").jitGetValue() > Ext.getCmp("Num_MaxValue").jitGetValue()) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
        if (Ext.getCmp("Num_DefaultValue").jitGetValue() < Ext.getCmp("Num_MinValue").jitGetValue()) {
            Ext.Msg.show({
                title: '提示',
                msg: "缺省值应在最大字符长度之内",
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO
            });
            return false;
        }
    }
    if (Ext.getCmp("ControlType").jitGetValue() == 10) {
        //2012-01-01 88:88:00
        if (Ext.getCmp("txt_DefaultValue").jitGetValue() != null && Ext.getCmp("txt_DefaultValue").jitGetValue() != "") {
            var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;
            var r = Ext.getCmp("txt_DefaultValue").jitGetValue().match(reg);
            if (r == null) {
                Ext.Msg.show({
                    title: '提示',
                    msg: "请输入日期时间（2012-01-01 12:12:00 ）",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
                return false;
            }
        }
    }

    if (!form.isValid()) {
        return false;
    }

    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=Edit",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            ControlName: Ext.getCmp("ControlName").getValue(),
            ParameterType: Ext.getCmp("ddl_ParameterType").jitGetValue(),
            ControlType: Ext.getCmp("ControlType").jitGetValue()
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });

                Ext.getCmp("parameterEditWin").hide();
                Ext.getCmp("pageBar").moveFirst();
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: JITPage.submitFailure
    });
}

function fnControlTypeChange() {
    Ext.getCmp("IsVerify").show();            //强制校验
    Ext.getCmp("txt_DefaultValue").show();     //缺省值
    Ext.getCmp("Date_DefaultValue").hide();   //时间缺省值
    Ext.getCmp("YN_DefaultValue").hide();     //勾选缺省值
    Ext.getCmp("Num_DefaultValue").hide();     //整数缺省值
    Ext.getCmp("Del_DefaultValue").hide();     //小数缺省值
    Ext.getCmp("Time_DefaultValue").hide();   //时间缺省值
    Ext.getCmp("TxtWeight").hide();           //权重值
    Ext.getCmp("MaxValue").hide();           //最大值
    Ext.getCmp("MinValue").hide();           //最小值
    Ext.getCmp("Num_MaxValue").hide();       //数值类型最大值
    Ext.getCmp("Num_MinValue").hide();       //数值类型最小值
    Ext.getCmp("Scale").hide();   //小数位
    Ext.getCmp("txtUnit").hide();  //后缀
    Ext.getCmp("columnControl").hide(); //多选值
    Ext.getCmp("Num_MaxValue").setFieldLabel("最大值");  //设置默认Label
    Ext.getCmp("Num_MinValue").setFieldLabel("最小值");  //设置默认Label
    switch (parseInt(this.getValue())) {
        case 1:
            //字符-文本框
            Ext.getCmp("Num_MaxValue").show();
            Ext.getCmp("Num_MinValue").show();
            Ext.getCmp("Num_MaxValue").setFieldLabel("最大字符长度");
            Ext.getCmp("Num_MinValue").setFieldLabel("最小字符长度");
            break;
        case 2:
            //整数-文本框
            Ext.getCmp("Num_MaxValue").show();
            Ext.getCmp("Num_MinValue").show();
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("Num_DefaultValue").show();
            Ext.getCmp("txtUnit").show();
            Ext.getCmp("TxtWeight").show();
            break;
        case 3:
            //小数-文本框
            Ext.getCmp("MaxValue").show();
            Ext.getCmp("MinValue").show();
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("Del_DefaultValue").show();
            Ext.getCmp("Scale").show();
            Ext.getCmp("txtUnit").show();
            Ext.getCmp("TxtWeight").show();
            break;
        case 5:
            //单选下拉框
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("columnControl").show();
            break;
        case 6:
            //多选下拉框         
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("columnControl").show();
            break;
        case 7:
            //勾选               
            Ext.getCmp("IsVerify").hide();
            Ext.getCmp("columnControl").hide();
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("YN_DefaultValue").show();
            break;
        case 8:
            //日期
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("Date_DefaultValue").show();
            break;
        case 9:
            //时间
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("Time_DefaultValue").show();
            break;
        case 10:
            //日期时间

            break;
        case 11:
            //定位
            Ext.getCmp("txt_DefaultValue").hide();
            break;
        case 12:
            //拍照
            Ext.getCmp("Num_MaxValue").show();
            Ext.getCmp("Num_MinValue").show();
            Ext.getCmp("Num_MaxValue").setFieldLabel("最多(张)");
            Ext.getCmp("Num_MinValue").setFieldLabel("最少(张)");
            Ext.getCmp("txt_DefaultValue").hide();
            Ext.getCmp("Num_DefaultValue").show();
            break;
        default:
            break;
    }
    // Ext.getCmp("txt_DefaultValue").show();
}
function fnYesNoChange() {
    Ext.getCmp("txt_DefaultValue").jitSetValue(Ext.getCmp("YN_DefaultValue").jitGetValue());
}

function fnDateChange() {
    Ext.getCmp("txt_DefaultValue").jitSetValue(Ext.getCmp("Date_DefaultValue").jitGetValue());
}

function fnNumChange() {
    Ext.getCmp("txt_DefaultValue").jitSetValue(Ext.getCmp("Num_DefaultValue").jitGetValue());
}

function fnDelChange() {
    Ext.getCmp("txt_DefaultValue").jitSetValue(Ext.getCmp("Del_DefaultValue").jitGetValue());
}

function fnTimeChange() {
    Ext.getCmp("txt_DefaultValue").jitSetValue(Ext.getCmp("Time_DefaultValue").getRawValue());
}
