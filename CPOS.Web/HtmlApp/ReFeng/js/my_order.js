Jit.AM.defindPage({

    name: 'my_order',

    onPageLoad: function () {

        //当页面加载完成时触发
        Jit.log('进入myorder.....');

        this.initEvent();
    },

    initEvent: function () {
        debugger;
        var me = this;

        me.windowHeight = window.innerHeight;

        me.windowWidth = window.innerWidth;

        me.tabUnpaidData();

    },
    tabUnpaidData: function () {
        $(".nav_myorder ul li").attr("class", "");
        $($(".nav_myorder ul li")[0]).attr("class", "cur");
        JitPage.ajax({
            url: '../../../OnlineShopping/data/Data.aspx',
            data: {
                action: "getOrderList",
                page: "1",
                pageSize: "15"
            },
            success: function (data) {
                //Jit.log(data);
                var itemlists = data.content.orderList;
                var orderlist = new Array();
                var itemlist = new Array();

                for (var i = 0; i < itemlists.length; i++) {
                    var itemhtml = "";
                    if (itemlists[i].orderDetailList != null && itemlists[i].orderDetailList != "") {
                        itemhtml = "";
                        for (var m = 0; m < itemlists[i].orderDetailList.length; m++) {
                            itemhtml += "<li class=\"clearfix\">";
                            itemhtml += "<div class=\"imgBox\"><img src=\"" + itemlists[i].orderDetailList[m].imageList[0].imageUrl + "\" width=\"70\" height=\"95\"></div>";
                            itemhtml += "<div class=\"infoBox\">";
                            itemhtml += "<h3>" + itemlists[i].orderDetailList[m].itemName + "</h3>";
                            itemhtml += "<div class=\"gray\">" + itemlists[i].orderDetailList[m].GG + "</div>";
                            itemhtml += "<div class=\"gray\">数量 " + itemlists[i].orderDetailList[m].qty + " </div>";
                            itemhtml += "<b>￥" + itemlists[i].orderDetailList[m].stdPrice + "</b>  </div>";
                            itemhtml += "</li>";
                        }
                    }
                    orderlist.push({
                        index: i,
                        orderId: itemlists[i].orderId,
                        createTime: itemlists[i].createTime.replace('-', '/').replace('-', '/'),
                        totalAmount: itemlists[i].totalAmount,
                        totalQty: itemlists[i].totalQty
                    });

                    itemlist.push({
                        index: i,
                        itemhtml: itemhtml
                    });
                }

                /*清空页面数据*/
                var tpl = $('#Tpl_list').html(), html = '';

                for (var i = 0; i < orderlist.length; i++) {
                    html += Mustache.render(tpl, orderlist[i]);
                }


                /*页面数据渲染*/
                $('[tplpanel=order_list]').html(html);

                for (var i = 0; i < itemlist.length; i++) {
                    Jit.log(itemlist[i].itemhtml);
                    $("#itemlist" + i).html(itemlist[i].itemhtml);
                }
            }
        });
    },
    tabDeilivery: function () {
        $(".nav_myorder li").attr("class", "");
        $($(".nav_myorder ul li")[1]).attr("class", "cur");
        $('[tplpanel=order_list]').html('<div class="order_mod order_goods_list">暂无订单</div>');
    },
    tabComplete: function () {
        $(".nav_myorder li").attr("class", "");
        $($(".nav_myorder ul li")[2]).attr("class", "cur");
        $('[tplpanel=order_list]').html('<div class="order_mod order_goods_list">暂无订单</div>');
    }
});