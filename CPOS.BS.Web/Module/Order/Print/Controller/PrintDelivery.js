$(document).ready(function () {
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
    var orderId = getUrlParam('orderId');
    var jsonData = {orderId:orderId};
    $.ajax({
        type: 'get',
        dataType: 'json',
        url: '/Module/Order/InoutOrders/Handler/Inout3Handler.ashx?method=PrintDelivery',
        data: jsonData,
        success: function (result) {
            var $delivery = $('#delivery_body'); 
            if (result != null && result != undefined) {
                var html = bd.template('tpl_delivery', { entity: result });
                $delivery.html(html);
                //$delivery.find('tr').addClass('x-grid-row');
            }
            else {
                var noHtml = bd.template('tpl_noContent', { tips: '没有数据.' });
            }
            //alert(JSON.stringify(result));
        },
        error: function (err, status) {
            alert('error');
        }
    });
});