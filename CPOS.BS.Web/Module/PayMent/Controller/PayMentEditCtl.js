var ChannelId = "";
var url = "";
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    myMask.show();
    InitView();
    var paymentId = new String(JITMethod.getUrlParam("paymentId"));
    var paymentCode = new String(JITMethod.getUrlParam("payCode"));
    var IsSave = new String(JITMethod.getUrlParam("IsSave"));

    fnview();
    debugger;
    if (IsSave.toString() == "false") {
        $("#btnSave").hide();
    }
    JITPage.HandlerUrl.setValue("Handler/PayMentHander.ashx?mid=");
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=getMapingbyPayMentTypeId",
        params: {
            paymentTypeId: paymentId
        },
        method: 'post',
        success: function (response) {
            debugger;
            var d = Ext.decode(response.responseText);
            url = d.url;
            switch (paymentCode.toString()) {
                case "AlipayOffline": //支付宝Offline面支付
                    if (d.ChannelId == "" || d.ChannelId == null) {
                        d.ChannelId = 0;
                    }
                    ChannelId = d.ChannelId;
                    Ext.getCmp("txtlineBank").jitSetValue(d.PayAccountNumber);
                    Ext.getCmp("txtlinePrivate").jitSetValue(d.ApplyMD5Key);
                    break;
                case "CupWap": //银联wap
                    if (d.ChannelId == "" || d.ChannelId == null) {
                        d.ChannelId = 0;
                    }
                    ChannelId = d.ChannelId;
                    Ext.getCmp("txtwebCupBackID").jitSetValue(d.PayAccountNumber);
                    Ext.getCmp("txtwebCupEnCryption").jitSetValue(d.EncryptionCertificate);
                    Ext.getCmp("txtwebCupPassword").jitSetValue(d.EncryptionPwd);
                    Ext.getCmp("txtwebCupDecryption").jitSetValue(d.DecryptionCertificate);
                    Ext.getCmp("txtwebCupDecryptionPassword").jitSetValue(d.DecryptionPwd);
                    break;
                case "AlipayWap": //支付宝wap
                    if (d.ChannelId == "" || d.ChannelId == null) {
                        d.ChannelId = 0;
                    }
                    ChannelId = d.ChannelId;
                    Ext.getCmp("txtwapBank").jitSetValue(d.PayAccountNumber);
                    Ext.getCmp("txtwaptbBack").jitSetValue(d.SalesTBAccess);
                    Ext.getCmp("txtwapPublic").jitSetValue(d.PayAccounPublic);
                    Ext.getCmp("txtwapPrivate").jitSetValue(d.PayPrivate);

                    break;
                case "CupVoice": //银联语音
                    if (d.ChannelId == "" || d.ChannelId == null) {
                        d.ChannelId = 0;
                    }
                    ChannelId = d.ChannelId;
                    Ext.getCmp("txtCupBackID").jitSetValue(d.PayAccountNumber);
                    Ext.getCmp("txtCupEnCryption").jitSetValue(d.EncryptionCertificate);
                    Ext.getCmp("txtCupPassword").jitSetValue(d.EncryptionPwd);
                    Ext.getCmp("txtCupDecryption").jitSetValue(d.DecryptionCertificate);
                    Ext.getCmp("txtCupDecryptionPassword").jitSetValue(d.DecryptionPwd);
                    break;
                case "WXJS": //微信JS支付
                    if (d.ChannelId == "" || d.ChannelId == null) {
                        d.ChannelId = 0;
                    }
                    ChannelId = d.ChannelId;
                    Ext.getCmp("txtMicroLendtity").jitSetValue(d.AccountIdentity);
                    Ext.getCmp("txtMricroPublic").jitSetValue(d.PublicKey);
                    Ext.getCmp("txtMricroStoreLendtity").jitSetValue(d.TenPayIdentity);
                    Ext.getCmp("txtMricroStoreCompotence").jitSetValue(d.TenPayKey);
                    Ext.getCmp("txtMricroParsword").jitSetValue(d.PayEncryptedPwd);
                    break;
                default:
            }
            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
            myMask.hide();
        }
    });


});
function fnview() {
    var paymentId = new String(JITMethod.getUrlParam("paymentId"));
    var paymentCode = new String(JITMethod.getUrlParam("payCode"));
    switch (paymentCode.toString()) {
        case "AlipayOffline": //支付宝Offline面支付
            $("#AlipayOffline").show();
            break;
        case "CupWap": //银联wap
            $("#spanOpenUpload2")[0].style.display = "none";
            $("#spanOpenUpload3")[0].style.display = "none";
            break;
        case "AlipayWap": //支付宝wap
            $("#AlipayWap").show();
            break;
        case "CupVoice": //银联语音
            $("#spanOpenUpload")[0].style.display = "none";
            $("#spanOpenUpload1")[0].style.display = "none";
            break;
        case "WXJS": //微信JS支付
            $("#WXJS").show();
            break;
        default:

    }
}

var AddPayChannelData = {}; //数据集合
var AddPayChannelList = new Array();
var AddPayChannel = {}; //数据集合
function fnDefault(id, code, ChannelId) {
    var paymentId = id;
    var paymentCode = code;

    if (paymentCode == "AlipayWap") {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "3";
        pay.PaymentTypeId = paymentId;
        pay.ChannelId = this.ChannelId;

        pay.WapData = WapData;
        AddPayChannelList[0] = pay;
    }

    //保存支付宝线下支付
    if (paymentCode == "AlipayOffline") {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "4";
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;

        pay.WapData = WapData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //银联语音支付
    if (paymentCode == "CupVoice") {
        var UnionPayData = {}; //银联语音支付
        var pay = {};
        pay.PayType = "2";
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;

        pay.UnionPayData = UnionPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }

    }

    //银联网页支付
    if (paymentCode == "CupWap") {
        var UnionPayData = {}; //银联网页支付
        var pay = {};
        pay.PayType = "1"
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.UnionPayData = UnionPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //微信支付
    if (paymentCode == "WXJS") {
        var pay = {};
        var WxPayData = {};
        pay.PayType = "5";
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.WxPayData = WxPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }
}


function fnSave() {

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });


    var paymentId = new String(JITMethod.getUrlParam("paymentId")).toString();
    var paymentCode = new String(JITMethod.getUrlParam("payCode")).toString();
    //保存支付WAP支付
    if (paymentCode == "AlipayWap") {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "3";
        pay.PaymentTypeId = paymentId;
        //pay.ChannelId = ChannelId;
        pay.ChannelId = "0";
        pay.NotifyUrl = url;
        var txtBank = Ext.getCmp("txtwapBank").getValue();
        if (txtBank == null || txtBank == "") {
            Ext.Msg.alert("提示", "支付宝WAP支付,账号不能为空");
            return;
        }
        WapData.Partner = txtBank;
        var txttbBack = Ext.getCmp("txtwaptbBack").getValue();
        if (txttbBack == null || txttbBack == "") {
            Ext.Msg.alert("提示", "支付宝WAP支付,卖家淘宝账号不能为空");
            return;
        }
        WapData.SellerAccountName = txttbBack;
        var txtwapPublic = Ext.getCmp("txtwapPublic").getValue();
        if (txtwapPublic == null || txtwapPublic == "") {
            Ext.Msg.alert("提示", "支付宝WAP支付,支付宝公钥不能为空");
            return;
        }
        WapData.RSA_PublicKey = txtwapPublic;
        var txtwapPrivate = Ext.getCmp("txtwapPrivate").getValue();
        if (txtwapPrivate == null || txtwapPrivate == "") {
            Ext.Msg.alert("提示", "支付宝WAP支付,私钥不能为空");
            return;
        }
        WapData.RSA_PrivateKey = txtwapPrivate;
        WapData.MD5Key = "";
        pay.WapData = WapData;
        AddPayChannelList[0] = pay;
    }

    //保存支付宝线下支付
    if (paymentCode == "AlipayOffline") {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "4";
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.NotifyUrl = url;
        var txtlineBank = Ext.getCmp("txtlineBank").getValue();
        if (txtlineBank == null || txtlineBank == "") {
            Ext.Msg.alert("提示", "支付宝线下支付,账号不能为空");
            return;
        }
        WapData.Partner = txtlineBank;
        var txtlinePrivate = Ext.getCmp("txtlinePrivate").getValue();
        if (txtlinePrivate == null || txtlinePrivate == "") {
            Ext.Msg.alert("提示", "支付宝线下支付,秘钥不能为空");
            return;
        }
        WapData.MD5Key = txtlinePrivate;
        pay.WapData = WapData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //银联语音支付
    if (paymentCode == "CupVoice") {
        var UnionPayData = {}; //银联语音支付
        var pay = {};
        pay.PayType = "2";
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.NotifyUrl = url;
        var txtCupBackID = Ext.getCmp("txtCupBackID").getValue();
        if (txtCupBackID == null || txtCupBackID == "") {
            Ext.Msg.alert("提示", "银联语音支付,账号ID不能为空");
            return;
        }
        UnionPayData.MerchantID = txtCupBackID

        //之前的结构
        //        var txtCupEnCryption = Ext.getCmp("txtCupEnCryption").getValue();
        //        if (txtCupEnCryption == null || txtCupEnCryption == "") {
        //            Ext.Msg.alert("提示", "银联语音支付,加密证书不能为空");
        //            return;
        //        }

        //        UnionPayData.DecryptCertificateFilePath = txtCupEnCryption;

        //        var txtCupPassword = Ext.getCmp("txtCupPassword").getValue();
        //        if (txtCupPassword == null || txtCupPassword == "") {
        //            Ext.Msg.alert("提示", "银联语音支付,加密密码不能为空");
        //            return;
        //        }
        //        UnionPayData.CertificateFilePassword = txtCupPassword;

        //        var txtCupDecryption = Ext.getCmp("txtCupDecryption").getValue();
        //        if (txtCupDecryption == null || txtCupDecryption == "") {
        //            Ext.Msg.alert("提示", "银联语音支付,解密证书不能为空");
        //            return;
        //        }
        //        UnionPayData.DecryptCertificateFilePath = txtCupDecryption;

        //        var txtCupDecryptionPassword = Ext.getCmp("txtCupDecryptionPassword").getValue();
        //        if (txtCupDecryptionPassword == null || txtCupDecryptionPassword == "") {
        //            Ext.Msg.alert("提示", "银联语音支付,解密密码不能为空");
        //            return;
        //        }
        //UnionPayData.PacketEncryptKey = txtCupDecryptionPassword;


        //Add by changjian.tian
        var txtCupEnCryption = Ext.getCmp("txtCupEnCryption").getValue();
        if (txtCupEnCryption == null || txtCupEnCryption == "") {
            Ext.Msg.alert("提示", "银联语音支付,加密证书不能为空");
            return;
        }
        UnionPayData.CertificateFilePath = txtCupEnCryption;

        var txtCupPassword = Ext.getCmp("txtCupPassword").getValue();
        if (txtCupPassword == null || txtCupPassword == "") {
            Ext.Msg.alert("提示", "银联语音支付,加密密码不能为空");
            return;
        }
        UnionPayData.CertificateFilePassword = txtCupPassword;

        var txtCupDecryption = Ext.getCmp("txtCupDecryption").getValue();
        if (txtCupDecryption == null || txtCupDecryption == "") {
            Ext.Msg.alert("提示", "银联语音支付,解密证书不能为空");
            return;
        }
        UnionPayData.DecryptCertificateFilePath = txtCupDecryption;

        var txtCupDecryptionPassword = Ext.getCmp("txtCupDecryptionPassword").getValue();
        if (txtCupDecryptionPassword == null || txtCupDecryptionPassword == "") {
            Ext.Msg.alert("提示", "银联语音支付,解密密码不能为空");
            return;
        }

        UnionPayData.PacketEncryptKey = txtCupDecryptionPassword;
        pay.UnionPayData = UnionPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }

    }

    //银联网页支付
    if (paymentCode == "CupWap") {
        var UnionPayData = {}; //银联网页支付
        var pay = {};
        pay.PayType = "1"
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.NotifyUrl = url;
        var txtwebCupBackID = Ext.getCmp("txtwebCupBackID").getValue();
        if (txtwebCupBackID == null || txtwebCupBackID == "") {
            Ext.Msg.alert("提示", "银联网页支付,账号ID不能为空");
            return;
        }
        UnionPayData.MerchantID = txtwebCupBackID

        var txtwebCupEnCryption = Ext.getCmp("txtwebCupEnCryption").getValue();
        if (txtwebCupEnCryption == null || txtwebCupEnCryption == "") {
            Ext.Msg.alert("提示", "银联网页支付,加密证书不能为空");
            return;
        }
        UnionPayData.CertificateFilePath = txtwebCupEnCryption;

        var txtwebCupPassword = Ext.getCmp("txtwebCupPassword").getValue();
        if (txtwebCupPassword == null || txtwebCupPassword == "") {
            Ext.Msg.alert("提示", "银联网页支付,加密密码不能为空");
            return;
        }
        UnionPayData.CertificateFilePassword = txtwebCupPassword;

        var txtwebCupDecryption = Ext.getCmp("txtwebCupDecryption").getValue();
        if (txtwebCupDecryption == null || txtwebCupDecryption == "") {
            Ext.Msg.alert("提示", "银联网页支付,解密证书不能为空");
            return;
        }
        UnionPayData.DecryptCertificateFilePath = txtwebCupDecryption;
        var txtwebCupDecryptionPassword = Ext.getCmp("txtwebCupDecryptionPassword").getValue();
        if (txtwebCupDecryptionPassword == null || txtwebCupDecryptionPassword == "") {
            Ext.Msg.alert("提示", "银联网页支付,解密密码不能为空");
            return;
        }
        UnionPayData.PacketEncryptKey = txtwebCupDecryptionPassword
        pay.UnionPayData = UnionPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //微信支付
    if (paymentCode == "WXJS") {
        var pay = {};
        var WxPayData = {};
        pay.PayType = "6";
        pay.ChannelId = ChannelId;
        pay.PaymentTypeId = paymentId;
        pay.NotifyUrl = url;
        var txtMicroLendtity = Ext.getCmp("txtMicroLendtity").getValue();
        if (txtMicroLendtity == null || txtMicroLendtity == "") {
            Ext.Msg.alert("提示", "微信支付,身份标识不能为空");
            return;
        }
        WxPayData.AppID = txtMicroLendtity;
        var txtMricroStoreLendtity = Ext.getCmp("txtMricroStoreLendtity").getValue();
        if (txtMricroStoreLendtity == null || txtMricroStoreLendtity == "") {
            Ext.Msg.alert("提示", "微信支付,财付商户通身份标识别不能为空");
            return;

        }
        WxPayData.ParnterID = txtMricroStoreLendtity;
        var txtMricroStoreCompotence = Ext.getCmp("txtMricroStoreCompotence").getValue();
        if (txtMricroStoreCompotence == null || txtMricroStoreCompotence == "") {
            Ext.Msg.alert("提示", "微信支付,财付通商户权限秘钥不能为空");
            return;
        }
        WxPayData.ParnterKey = txtMricroStoreCompotence;
        WxPayData.AppSecret = Ext.getCmp("txtMricroPublic").getValue();
        if (WxPayData.AppSecret == null || WxPayData.AppSecret == "") {
            Ext.Msg.alert("提示", "微信支付,公共平台秘钥不能为空");
            return;
        }

        WxPayData.PaySignKey = Ext.getCmp("txtMricroParsword").getValue();
        pay.WxPayData = WxPayData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    AddPayChannelData.AddPayChannelData = AddPayChannelList
    AddPayChannel.Parameters = AddPayChannelData;

    myMask.show();
    Ext.Ajax.request({
        method: 'POST',
        //        sync: true,
        //        async: false,
        url: '/ApplicationInterface/PayChannel/PayChannelGateway.ashx?type=Product&action=SetPayChannel',
        params: {
            "req": Ext.encode(AddPayChannel)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.ResultCode == "0") {
                showSuccess("保存数据成功");
                flag = false;
                AddPayChannelData = {}; //数据集合
                AddPayChannelList = new Array();
                AddPayChannel = {};
                parent.fnSearch();
                CloseWin('PayMentEdit');
            } else {
                Ext.Msg.alert("提示", "保存数据失败" + d.Message);
                flag = true;
                AddPayChannelData = {}; //数据集合
                AddPayChannelList = new Array();
            }
            myMask.hide();
        },
        failure: function (result) {
            AddPayChannelData = {}; //数据集合
            AddPayChannelList = new Array();
            Ext.Msg.alert("提示", "保存数据失败：" + result.responseText);
            myMask.hide();
        }
    });
}

function fnUploadFile(fileid, id, txtid) {
    if (!Ext.getCmp(fileid).form.isValid()) {
        Ext.Msg.show({
            title: '提示',
            msg: "请先选择需要上传的文件!",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.WARNING
        });
        return false;
    }
    else if (!Ext.Array.contains(["pfx", "cer"],
        Ext.getCmp(id).getValue().split(".").pop())) {
        Ext.Msg.show({
            title: '提示',
            msg: "只能上传后缀名为 pfx, cer 的文件！",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.WARNING
        });
        return false;
    }
    else {
        Ext.getCmp(fileid).getForm().submit({
            url: "/Framework/Upload/UploadFile.ashx",
            waitTitle: "请等待...",
            waitMsg: "上传中...",
            success: function (fp, o) {
                fnFileInput(Ext.getCmp(id));
                Ext.getCmp(txtid).setValue(o.result.file.url);
                Ext.Msg.show({
                    title: '提示',
                    msg: "上传成功！",
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });
            },
            failure: function (fp, o) {
                fnFileInput(Ext.getCmp(id));
                Ext.getCmp(id).setValue('');
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        })
    }
}
/*
*添加 修改 提交 后 统一将filefield控件替换为新的控件
*/
function fnFileInput(fileInput) {
    var me = fileInput,
        fileInput = me.isFileUpload() ? me.inputEl.dom : null, clone;
    if (fileInput) {
        clone = fileInput.cloneNode(true);
        try {
            //fileInput.parentNode.replaceChild(clone, fileInput);
            //因为parentNode有时候找不到 这里用jquery强制替换
            $("#txt_Attach-inputEl").replaceWith(clone);
        }
        catch (e) {
            $("#txt_Attach-inputEl").replaceWith(clone);
        }
        me.inputEl = Ext.get(clone);
    }
    return fileInput;
}
function fnClose() {

    CloseWin('PayMentEdit');
}