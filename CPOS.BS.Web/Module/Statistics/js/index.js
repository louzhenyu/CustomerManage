define(['jquery','daterangepicker', 'template', 'tools', 'pagination'], function ($, temp) {
    template.openTag = '<$';
    template.closeTag = '$>';

    var page = {
        ele: {
            section: $("#section")
        },
        page: {
            pageIndex: 0,
            pageSize: 5
        },
        init: function () {
            this.editUrl = "";
            this.ajaxUrl = "http://stats.aladingyidong.com/index.php?module=API&method=Events.getCategory&format=JSON&idSite=1&period=day&date=today&segment=eventAction=@b0b946bd761c47898eaea4636cd45151&token_auth=696e09e73516673ff82da68f5c499317";
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            this.loadPageList();
        },
        initEvent: function () {
                $("#dataInput").daterangepicker({
                    arrows:true,
                    onChange:function(){
                        var $dataInput = $("#dataInput");//range
                        if($dataInput.length!=0){
                            self.editUrl = self.ajaxUrl;
                            self.ajaxUrl = self.getUrlChangeParam("date",$dataInput.val());
                            if($dataInput.val().indexOf(",")!=-1){
                                self.ajaxUrl = self.getUrlChangeParam("period","range");
                            }
                            self.loadPageList();
                        }
                    }
                });
        },
        getUrlChangeParam : function(key,value){
            var urlArr = this.editUrl.split("?"),paramArr=[],
                params = {};
            if(urlArr[1].length.length==0){
                return false;
            }

            var items = urlArr[1].split("&");
            for (i = 0; i < items.length; i++) {
                if(items[i].match(/=/g).length!=1){
                    paramArr.push(items[i]);
                }else{
                    itemarr = items[i].split("=");
                    params[itemarr[0]] = itemarr[1];
                }

            }

            params[key] = value.replace(/\s/g,'');
            for(var i in params){
                paramArr.push(i+"="+params[i]);
            }
            this.editUrl = urlArr[0]+"?"+paramArr.join("&");
            return this.editUrl;
        },
        loadPageList: function (callback) {
            $.ajax({
                url: this.ajaxUrl,
                type:"get",
                dataType: "jsonp",
                success: function (data) {
                    console.log(Object.prototype.toString.call(data) );
                    if (Object.prototype.toString.call(data)=="[object Array]") {
                        if (callback) {
                            callback(data);
                        } else {
                            var idata,iarr,index,keyName,tempObj={};
                            var nameArray=[],renderObj={};
                            //debugger;
                            for(var i=0;i<data.length;i++){
                                idata = data[i];
                                iarr = idata.label.split("---");
                                if(iarr.length!=2) continue;
                                switch(iarr[1]){
                                    case "浏览":
                                        keyName = "visit";
                                        break;
                                    case "转发到好友":
                                        keyName = "share";
                                        break;
                                    case "转发到朋友圈":
                                        keyName = "share";
                                        break;
                                    default :
                                        keyName = "";
                                }
                                if(keyName=="") continue;

                                index=$.inArray(iarr[0],nameArray);
                                if(index!=-1){
                                    renderObj[index].name=iarr[0];
                                    renderObj[index][keyName] = renderObj[index][keyName]?(renderObj[index][keyName]+idata.nb_events):idata.nb_events;
                                }else{
                                    nameArray.push(iarr[0]);
                                    renderObj[nameArray.length-1]={};
                                    renderObj[nameArray.length-1].name=iarr[0];
                                    renderObj[nameArray.length-1][keyName] =idata.nb_events;
                                }

                            }
                            //debugger;
                            self.renderPageList($.util.obj2list(renderObj));
                        }

                    }
                }
            });
        },
        renderPageList: function (list) {
            var temp = $("#tableTemp").html();
            $("#table tbody").html(this.render(temp,{list:list}));
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});