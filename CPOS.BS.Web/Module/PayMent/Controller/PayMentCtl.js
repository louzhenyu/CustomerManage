Ext.onReady(function () {

    InitView();

    fnload();
});

function fnwapHiden() {
    $("#txtwapBank").hide();
    $("#txtwaptbBack").hide();
    $("#txtwapPublic").hide();
    $("#txtwapPrivate").hide();
    $(".wapclass").hide();
}
function fnwapShow() {
    $("#txtwapBank").show();
    $("#txtwaptbBack").show();
    $("#txtwapPublic").show();
    $("#txtwapPrivate").show();
    $(".wapclass").show();

}
function fnlineHiden() {

    $("#txtlineBank").hide();
    $("#txtlinePrivate").hide();
    $(".lineclass").hide();
}
function fnlineShow() {

    $("#txtlineBank").show();
    $("#txtlinePrivate").show();
    $(".lineclass").show();
}
function fnCupHiden() {

    $("#Div1").height(200);


    $("#spanOpenUpload")[0].style.display = "none";
    $("#spanOpenUpload1")[0].style.display = "none";

    $("#txtCupBackID").hide();
    $("#txtCupEnCryption").hide();
    $("#txtCupPassword").hide();
    $("#txtCupDecryption").hide();
    $("#txtCupDecryptionPassword").hide();
    $("#btnOpenUpload").hide();
    $("#btnOpenUpload1").hide();
    $(".CupClass").hide();
}
function fnCupShow() {

    $("#txtCupBackID").show();
    $("#txtCupEnCryption").show();
    $("#txtCupPassword").show();
    $("#txtCupDecryption").show();
    $("#txtCupDecryptionPassword").show();
    $("#btnOpenUpload").show();
    $("#btnOpenUpload1").show();
    $(".CupClass").show();

}
function fnwebCupHiden() {


    $("#Div2").height(200);
    $("#spanOpenUpload2")[0].style.display = "none";
    $("#spanOpenUpload3")[0].style.display = "none";
    $("#txtwebCupBackID").hide();
    $("#txtwebCupEnCryption").hide();
    $("#txtwebCupPassword").hide();
    $("#txtwebCupDecryption").hide();
    $("#txtwebCupDecryptionPassword").hide();
    $("#btnOpenUpload2").hide();
    $("#btnOpenUpload3").hide();
    $(".webCupClass").hide();
}
function fnwebCupShow() {
    $("#txtwebCupBackID").show();
    $("#txtwebCupEnCryption").show();
    $("#txtwebCupPassword").show();
    $("#txtwebCupDecryption").show();
    $("#txtwebCupDecryptionPassword").show();
    $("#btnOpenUpload2").show();
    $("#btnOpenUpload3").show();
    $(".webCupClass").show();
}
function fnMicroHiden() {

    $("#txtMicroLendtity").hide();
    $("#txtMricroPublic").hide();
    $("#txtMricroStoreLendtity").hide();
    $("#txtMricroStoreCompotence").hide();
    $("#txtMricroParsword").hide();
    $(".Microclass").hide();
}
function fnMicroShow() {
    $("#txtMicroLendtity").show();
    $("#txtMricroPublic").show();
    $("#txtMricroStoreLendtity").show();
    $("#txtMricroStoreCompotence").show();
    $("#txtMricroParsword").show();
    $(".Microclass").show();
}




var AddPayChannelData = {}; //数据集合
var AddPayChannelList = new Array();

function fnSave() {

    //保存支付WAP支付
    if (Ext.getCmp("rbwapCustom").getValue()) {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "3";
        var txtBank = Ext.getCmp("txtwapBank").getValue();
        if (txtBank == null || txtBank == "") {
            Ext.Msg.alert("提示", "支付宝WAP支付,账号不能为空");
            return;
        }
        WapData.Partner = txtBank;
        var txttbBack = Ext.getCmp("txtwaptbBack").getValue();
        //        if (txttbBack == null || txttbBack == "") {
        //            Ext.Msg.alert("提示", "支付宝WAP支付,卖家淘宝账号不能为空");
        //            return;
        //        }
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
    if (Ext.getCmp("rblineCustom").getValue()) {
        var pay = {};
        var WapData = {}; //WAP支付数据集合
        pay.PayType = "4";


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
        //        WapData.SellerAccountName = null;
        //        WapData.RSA_PublicKey = null;
        //        WapData.RSA_PrivateKey = null;
        pay.WapData = WapData;
        if (AddPayChannelList == null || AddPayChannelList.length == 0) {
            AddPayChannelList[0] = pay;
        }
        else {
            AddPayChannelList[AddPayChannelList.length] = pay
        }
    }

    //银联语音支付
    if (Ext.getCmp("rbCupCustom").getValue()) {
        var UnionPayData = {}; //银联网页支付
        var pay = {};
        pay.PayType = "2"


        var txtCupBackID = Ext.getCmp("txtCupBackID").getValue();
        if (txtCupBackID == null || txtCupBackID == "") {
            Ext.Msg.alert("提示", "银联语音支付,账号ID不能为空");
            return;
        }
        UnionPayData.MerchantID = txtCupBackID

        var txtCupEnCryption = Ext.getCmp("txtCupEnCryption").getValue();
        if (txtCupEnCryption == null || txtCupEnCryption == "") {
            Ext.Msg.alert("提示", "银联语音支付,加密证书不能为空");
            return;
        }
        UnionPayData.DecryptCertificateFilePath = txtCupEnCryption;

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
    if (Ext.getCmp("rbwebCupCustom").getValue()) {
        var UnionPayData = {}; //银联网页支付
        var pay = {};
        pay.PayType = "1"
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

    if (Ext.getCmp("rbMricroCupCustom").getValue()) {
        var pay = {};
        var WxPayData = {};
        pay.PayType = "5";
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
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/ApplicationInterface/PayChannel/PayChannelGateway.ashx?type=Product&action=SetPayChannel',
        params: {
            "req": Ext.encode(AddPayChannelData)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.ResultCode == "0") {
                showError("保存数据成功：");
                flag = false;
                AddPayChannelData = {}; //数据集合
                AddPayChannelList = new Array();
            } else {
                showSuccess("保存数据失败" + d.Message);
                flag = true;
                AddPayChannelData = {}; //数据集合
                AddPayChannelList = new Array();
            }
        },
        failure: function (result) {
            AddPayChannelData = {}; //数据集合
            AddPayChannelList = new Array();
            showError("保存数据失败：" + result.responseText);
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
    else if (!Ext.Array.contains(["mp3", "wma", "wav", "amr"],
        Ext.getCmp(id).getValue().split(".").pop())) {
        Ext.Msg.show({
            title: '提示',
            msg: "只能上传后缀名为 mp3, wma, wav, amr 的文件！",
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

                if (txtid == "txtwebCupEnCryption") {
                    //UnionPayData.DecryptCertificateFile = o.result.fileData;
                    // UnionPayData.DecryptCertificateFile = o.result.postedFile;
                }
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
function fnOpenUpload(name, tb) {
    var div = $("#" + name);
    if (div[0].style.display == "none") {
        div[0].style.display = "";
        $("#" + tb).height(300);
    }
    else {
        div[0].style.display = "none";
        $("#" + tb).height(200);
    }
}
function fnload() {
    $("#spanOpenUpload")[0].style.display = "none";
    $("#spanOpenUpload1")[0].style.display = "none";
    $("#spanOpenUpload2")[0].style.display = "none";
    $("#spanOpenUpload3")[0].style.display = "none";
}
