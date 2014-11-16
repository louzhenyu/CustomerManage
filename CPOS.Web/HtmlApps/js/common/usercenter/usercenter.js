Jit.AM.defindPage({
    
    onPageLoad: function () {

        var me = this;

        Jit.UI.Loading();
        
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx',
            data: {
                'action': 'getVipDetail',
            },
            success: function (data) {
                
                Jit.UI.Loading(false);
                
                if(data.code==200){

                    $('[tplname=username]').html(data.content.vipName);
                }
            }
        });
    },
    initWithParam:function(param){
        
        if(param.cardImage){

            $('#customerImg').attr('src',param.cardImage);
        }
    },
    
});