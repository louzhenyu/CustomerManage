Jit.AM.defindPage({

    name: 'ActivityListGoods',
    ele: {
        activityList: $("#activityList")
    },
    initWithParam: function(param) {

    },
    onPageLoad: function() {
        this.eventTypeId = Jit.AM.getUrlParam("eventTypeId");
        if(!this.eventTypeId){
            alert("地址栏获不到eventTypeId");
            return false;
        }
        this.loadData();
        this.initEvent();
    },
    loadData:function(){
        var self = this,
            paramList = {
                'action':'getPanicbuyingEventList',
                'eventTypeId': this.eventTypeId,
                'page': 1,
                'pageSize': 99
            };
        self.ajax({
            url: '/Interface/data/ItemData.aspx',
            data: paramList,
            beforeSend:function(){
                Jit.UI.AjaxTips.Loading(true);
            },
            success: function(data) {
                if (data.code == 200) {
                    if(data.content.length){
                        var html = template.render("goodsListTemp",{list:data.content});
                        self.ele.activityList.html(html);
                        setInterval(function(){
                            self.timeDown();
                        },1000);
                    }else{
                        self.ele.activityList.html('<div style="width:320px;height:100px;text-align: center;position: absolute;left:50%;top:50%;margin:-50px 0 0 -160px;;">活动正在筹备中，敬请期待！</div>');
                    }
                }else{
                    self.alert(data.description);
                }
            },
            complete:function(){
                Jit.UI.AjaxTips.Loading(false);
            }
        });
    },

    initEvent: function() {
        var self = this;
        this.ele.activityList.delegate(".snappedList-item","click",function(){
            var itemId = $(this).attr("data-itemid"),
                eventId = $(this).attr("data-eventid");
            if(self.eventTypeId==1||self.eventTypeId==2||self.eventTypeId==3){
                Jit.AM.toPage('GroupGoodsDetail','goodsId='+itemId+'&eventId='+eventId);
            }else{
                self.alert("eventType找不到");
            }

        });
    },
    timeDown: function () {
        var self = this;
        var domlist = $('[time-date]'), endtime, second;

        var _h, _m, _s;

        domlist.each(function (idx, dom) {

            endtime = $(dom).attr('time-date');

            second = parseInt(endtime);
            if(second==0){
                self.loadData();
            }
            _h = Math.floor(second / 3600);

            _m = Math.floor((second % 3600) / 60);

            _s = Math.floor(((second % 3600) % 60));

            //超过99小时全部显示99
            _m = _h > 99 ? 99 : _m;
            _s = _h > 99 ? 99 : _s;
            _h = _h > 99 ? 99 : _h;


            $(dom).attr('time-date', endtime - 1);
            //console.log(_h+' '+_m+' '+_s);

            $(dom).find('[tn=time-h-max]').html(Math.floor(_h / 10) > 9 ? 9 : Math.floor(_h / 10));

            $(dom).find('[tn=time-h-min]').html(Math.floor(_h % 10));

            $(dom).find('[tn=time-m-max]').html(Math.floor(_m / 10));

            $(dom).find('[tn=time-m-min]').html(Math.floor(_m % 10));

            $(dom).find('[tn=time-s-max]').html(Math.floor(_s / 10));

            $(dom).find('[tn=time-s-min]').html(Math.floor(_s % 10));
        });
    },
    getDay: function(_unixTime) { //获取剩余天数

        var unixTime = parseInt(_unixTime) * 1000; //转换成JS时间

        var endTime = new Date(unixTime),
            seconds = 1000,
            min = 60 * seconds,
            hour = 60 * min,
            days = 24 * hour,
            now = new Date().getTime(),
            dateDiff = endTime - now,
            tipe;
        if (dateDiff <= 0)
            tipe = 1;
        else {
            tipe = parseInt(dateDiff / days);
        }
        return tipe || 1;
    },

    alert:function(text,callback){
        Jit.UI.Dialog({
            type : "Alert",
            content : text,
            CallBackOk : function() {
                Jit.UI.Dialog("CLOSE");
                if(callback){
                    callback();
                }
            }
        });
    }


});