define(['jquery', 'template','artDialog'], function ($) {
    String.format = function () {
        var s = arguments[0];
        for (var i = 0; i < arguments.length - 1; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            s = s.replace(reg, arguments[i + 1]);
        }
        return s;
    }
    var app = {
        initEvent:initEvents,
        validate: validate,
        getItems: getItems,
        loadItems:loadItems,
        saveItems:saveItems,
        bindItems: bindItems,
        msgHolder: $('#section .tips'),
        pushInfo: '#VipName#,您本次消费#SalesAmount#元,返现金额#ReturnAmount#元，帐内余额#EndAmount#,<a href=\\"http://o2oapi.aladingyidong.com/HtmlApps/html/public/index/index_shop_app.html?customerId=#CustomerId#&openId=#OpenId#&userId=#UserId#&rid=9112\\">点击此处</a>使用返现.',
        clearWarning:clearWarning,
        url: '/Module/WXSalesPolicy/PolicySettings.aspx',
        //整数验证
        intExp: /\d{1,7}/, 
        //0-100的浮点数字验证
        cExp: /^0*$|^[0-9]?[0-9]?(\.[0-9]*)?$/,
        items: [],
        error:'',
        message: {
            //系数
            c: '返利比例为0%到100%之间的数值.',
            //起始值
            b: '起始值应为上一规则的结束值.',
            //结束值
            e: '结束值应大于起始值{0}', 
            //整数类型
            n: '输入值必须为整数类型',
            //结束值和下一个规则的起始值需相等
            nq: '起始值不等于上一规则的结束值.',
            //起始值和结束值的有效范围
            beyond:'值必须在0和9999999之间.',
            empty:'没有设置任何规则.',
            warning:'没有填入任何值，此操作将删除\n所有已存在规则,确认吗？'
        }
    };
    function saveItems() {
        var self = app;
        $.each(self.items, function (i, e) {
            e.Coefficient = e.Coefficient * 0.01;
            delete e.be;
            delete e.ee;
            delete e.ce;
        });
        var jdata = {
            action: 'save',
            data: JSON.stringify(self.items)
        };
        $.ajax({
            url: app.url,
            type: 'post',
            data: jdata,
            dataType:'json',
            success: function (data) {
                var result = data;
                if (result.IsSuccess == 0) {
                    self.msgHolder.text(result.Message);
                }
                else if (result.IsSuccess == 1) {
                    self.msgHolder.addClass('green').text(result.Message);
                }
            },
            error: function (err) {
                self.msgHolder.text(err);
            }
        });
    }
    function loadItems() {
        var self = app;
        var jdata = {action:'load'};
        $.ajax({
            url: app.url,
            type: 'get',
            data: jdata,
            dataType:'json',
            success: function (data) {
                app.bindItems(data);
            },
            error: function (err) {
                self.msgHolder.text(err);
            }
        });
    }
    function bindItems(items) {
        var list = items || [];
        var html = bd.template('tpl_setting', { list: list });
        $('#settings').html(html);
    }
    function getItems() {
        var self = app;
        app.items = [];
        var $divs = $('#settings div.noEmpty');
        //alert($divs.length); return;
        $.each($divs,function (i, item) {
            var inputs = $(item).find('input');
            
            var b = inputs.eq(0).val(),e=inputs.eq(1).val(),c=inputs.eq(2).val();
            var obj = {
                //左区间输入框
                be: inputs.eq(0), 
                //右区间输入框
                ee: inputs.eq(1), 
                //系数输入框
                ce:inputs.eq(2), 
                AmountBegin: b,
                RateId:$(item).data('rateid'),
                AmountEnd: e,
                PushInfo:app.pushInfo,
                //系数
                Coefficient:c  
            };
            app.items.push(obj);
        });
    }
    function clearWarning() {
        $('#settings input').removeClass('error');
    }
    function bindClickEvent() {
        var self = app;
        $('#section').delegate('.btnSave', 'click', function (e) {
            var rlt = app.validate();
            if (rlt.s == 0) {
                self.msgHolder.text(rlt.m);
                return;
            }
            if (rlt.s == 2) {
                dialog({
                    title: '警告',
                    content: rlt.m,
                    ok: function () {
                        self.saveItems();
                    },
                    okValue: '确认',
                    cancelValue: '取消',
                    cancelVal: '取消',
                    cancel: true
                }).showModal();
                return;
            }
            self.msgHolder.text('');
            self.saveItems();
        });
    }
    function bindClearEvent() {
        $('#settings').delegate('span.clear', 'click', function () {
            $(this).parent('div').find('input').val('');
        });
    }
    function bindInputEvent() {
        var settings = $('#settings');
        settings.delegate('input', 'blur', function () {
            var div = $(this).parent('div');
            var els = div.find('input');
            for (var i = 0; i < els.length; i++) {
                if ($(els[i]).val() != '') {
                    div.addClass('noEmpty').removeClass('emptyInput');
                    return;
                }
            }
            div.addClass('emptyInput').removeClass('noEmpty');
        }).delegate('input', 'focus', function () {
            var div=$(this).parent('div');
            div.add('noEmpty').removeClass('emptyInput');
        });
    }
    function initEvents() {
        bindInputEvent();
        bindClearEvent();
        bindClickEvent();
    }
    function validate() {
        var self = app;
        self.msgHolder.text('').removeClass('green');
        self.getItems();
        self.clearWarning();
        var rlt =
            {
                //是否成功
                s: 0,
                //错误消息
                m: 'error'
            };
        var len = self.items.length;
        if (len == 0) {
            rlt.s = 2;
            rlt.m = self.message.warning;
            return rlt;
        }
        for (var i = 0; i < len; i++) {
            var o = self.items[i];
            if (!self.intExp.test(o.AmountBegin)) {
                o.be.focus();
                o.be.addClass('error');
                rlt.s = 0;
                rlt.m = self.message.n;
                return rlt;
            }
            if (!self.intExp.test(o.AmountEnd)) {
                o.ee.focus();
                o.ee.addClass('error');
                rlt.s = 0;
                rlt.m = self.message.n
                return rlt;
            }
            if (!self.cExp.test(o.Coefficient)) {
                o.ce.focus();
                o.ce.addClass('error');
                rlt.s = 0;
                rlt.m = self.message.c;
                return rlt;
            }
            //第一个规则的左区间如果为空则默认是0
            var b = parseInt(o.AmountBegin == '' ? '0' : o.AmountBegin),
                e = parseInt(o.AmountEnd == '' ? '9999999':o.AmountEnd);
            if (b < 0 || b > 9999999) {
                o.be.focus().addClass('error');
                rlt.s = 0;
                rlt.m = self.message.beyond;
                return rlt;
            }
            if (e < 0 || e > 9999999) {
                o.ee.focus().addClass('error');
                rlt.s = 0;
                rlt.m = self.message.beyond;
                return rlt;
            }
            var c= parseFloat(o.Coefficient==''?'0':o.Coefficient);
            if (b >= e) {
                o.ee.focus();
                o.ee.addClass('error');
                rlt.s = 0;
                rlt.m = String.format(self.message.e, o.AmountBegin);
                return rlt;
            }
            if (i > 0) {
                if (b != parseInt(self.items[i - 1].AmountEnd)) {
                    o.be.focus();
                    o.be.addClass('error');
                    rlt.s = 0;
                    rlt.m = self.message.nq;
                    return rlt;
                }
            }
        }
        $('#settings input').removeClass('error');
        rlt.s = 1;
        rlt.m = '';
        return rlt;
    }
    $(function () {
        app.initEvent();
        app.loadItems();
    });
});