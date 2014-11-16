define(['jquery', 'zTree','tools', 'kkpager','datetimePicker','template'], function ($) {
    var app = {
        initialize: initialize,
        url: '/Module/WXSalesPolicy/WXSalesRebateList.aspx',
        gateway:'/ApplicationInterface/WXSalesRebate/Gateway.ashx',
        loadUnit: loadUnit,
        queryParameter:{PageIndex:1,PageSize:15,SearchColumns:[]},
        section: $('#section'),
        resultCount:$('#resultCount')
    };
    function initZTree(){
            //获得所有的ztree集合
            $(".ztree").each(function(i,j){
                var $t=$(this);
                //所有的数据集合
                var forminfo=$t.data("forminfo"),id=$t.attr("id");
                var zNodes=[];
                for(var j=0,length=forminfo.length;j<length;j++){
                    var item=forminfo[j];
                    if (item.ParentUnitID == -99) {  //父节点
                        item.ParentUnitID == 0;
                    }
                    item.name=item.UnitName;
                    item.id=item.UnitID;
                    item.pId = item.ParentUnitID;
                    zNodes.push(item);
                }
                var setting = {
                    isSimpleData : true,
                    view:{
                        treeNodeKey : "id",
                        treeNodeParentKey : "pId",
                        chkStyle: "radio",
                        enable: true,
                        radioType: "all",
                    },
                    callback:{
                        onClick:function(event, treeId, treeNode){
                            //点击的子节点
                            //if(!!!treeNode.children){
                            $t.parent().find("input").val(treeNode.name).data("unitId",treeNode.UnitID);
                            $t.hide();
                            //}
                                
                        }
                    }
                };
                var zTreeObj = $.fn.zTree.init($("#"+id), setting, []) ;
                var treeNodes = zTreeObj.transformTozTreeNodes(zNodes);
                zTreeObj.addNodes(null, treeNodes);
            });
                
    }
    //跳转到第几页
    function go2Page(n)
    {
        app.queryParameter.PageIndex = n;
        queryList();
    }
    //构建查询参数
    function buildQueryParameter() {
        app.queryParameter.PageIndex = 1;
        app.queryParameter.SearchColumns = [];
        var unit = $('#txtUnit').data('unitId');
        if (unit && unit != '') {
            var unitCol = { ColumnName: 'unitid', ColumnValue1: unit };
            app.queryParameter.SearchColumns.push(unitCol);
        }
        var txtOperator = $('#txtOperator').val();
        if ($.trim(txtOperator) != '') {
            var operatorCol = { ColumnName: 'operator', ColumnValue1: txtOperator };
            app.queryParameter.SearchColumns.push(operatorCol);
        }
        var begin = $('#dtBegin').val(), end = $('#dtEnd').val();
        if ($.trim(begin) != '' || $.trim(end)!='') {
            var timeCol = { ColumnName: 'time', ColumnValue1: $.trim(begin), ColumnValue2: $.trim(end) };
            app.queryParameter.SearchColumns.push(timeCol);
        }
        var amountBegin = $('#amountBegin').val(), amountEnd = $('#amountEnd').val();
        if ($.trim(amountBegin) || $.trim(amountEnd)) {
            var amountCol = { ColumnName: 'amount', ColumnValue1: amountBegin, ColumnValue2: amountEnd };
            app.queryParameter.SearchColumns.push(amountCol);
        }
    }
    //查询之前验证表单
    function validate() {
        $('label.searchInput').removeClass('error');
        $('.tips').text('');
        var rlt = {IsSuccess:1,Msg:''};
        var begin = $('#dtBegin'), end =$('#dtEnd');
        var amountBegin = $('#amountBegin'), amountEnd = $('#amountEnd');
        if (begin.val() != '' && end.val() != '') {
            var dtBegin = new Date(begin.val()), dtEnd = new Date(end.val());
            if (dtBegin > dtEnd) {
                begin.parent('.searchInput').addClass('error');
                rlt.IsSuccess = 0;
                rlt.Msg = '开始时间应小于结束时间.';
                return rlt;
            }
        }
        var exp = /^\d{1,7}$/;
        if (amountBegin.val() != '' && !exp.test(amountBegin.val())) {
            amountBegin.focus().parent('.searchInput').addClass('error');
            rlt.IsSuccess = 0;
            rlt.Msg = '请填入数字字符.';
            return rlt;
        }
        if (amountEnd.val() != '' && !exp.test(amountEnd.val())) {
            amountEnd.focus().parent('.searchInput').addClass('error');
            rlt.IsSuccess = 0;
            rlt.Msg = '请填入数字字符.';
            return rlt;
        }
        if (amountBegin.val() != '' && amountEnd.val() != '') {
            if(parseInt(amountBegin.val())>parseInt(amountEnd.val())){
                amountBegin.focus().parent('.searchInput').addClass('error');
                rlt.IsSuccess = 0;
                rlt.Msg = '开始交易金额应小于结束交易金额.';
                return rlt;
            }
        }
        return rlt;
    }
    //导出列表到Excel
    function exportList() {
        var getUrl = '/ApplicationInterface/WXSalesRebate/Gateway.ashx?type=Product&action=Export';//&req=';
        var data = {
            Parameters: {
                SearchColumns: app.queryParameter.SearchColumns,
                PageIndex: 1,
                PageSize:15
            }
        };
        var dataLink = JSON.stringify(data);
        var form = $('<form>');
        form.attr('style', 'display:none;');
        form.attr('target', '');
        form.attr('method', 'post');
        form.attr('action', getUrl);
        var input1 = $('<input>');
        input1.attr('type', 'hidden');
        input1.attr('name', 'req');
        input1.attr('value', dataLink);
        $('body').append(form);
        form.append(input1);
        form.submit();
        form.remove();
    }
    //查询列表
    function queryList() {
        $.util.ajax({
            url: app.gateway,
            type: 'post',
            dataType: 'json',
            data: {
                action: 'query',
                SearchColumns: app.queryParameter.SearchColumns,
                PageIndex: app.queryParameter.PageIndex,
                PageSize: app.queryParameter.PageSize
            },
            success: function (data) {
                data = data || [];
                var totalCount = data.length>0 ? data[0].TotalCount : 0;
                $('#content').html(bd.template('tpl_body', { list: data }));
                app.resultCount.html(totalCount);
                var totalPageCount = Math.ceil(totalCount * 1.0 / app.queryParameter.PageSize);
                //if (totalPageCount > 1) {
                    kkpager.generPageHtml(
                    {
                        pno: app.queryParameter.PageIndex,
                        mode: 'click', //设置为click模式
                        //总页码  
                        total: totalPageCount,
                        totalRecords: totalCount,
                        isShowTotalPage: true,
                        isShowTotalRecords: true,
                        //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                        //适用于不刷新页面，比如ajax
                        click: function (n) {
                            //这里可以做自已的处理
                            //...
                            //处理完后可以手动条用selectPage进行页码选中切换
                            this.selectPage(n);
                            //点击下一页或者上一页 进行加载数据
                            go2Page(n);
                        },
                        //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                        getHref: function (n) {
                            return '#';
                        }

                    }, true);
                //}
            },
            error: function (err,ex) {
                alert(ex);
            }
        });
    }
    //绑定查询按钮点击事件
    function initQueryClickEvent() {
        app.section.delegate('.queryBtn', 'click', function () {
            var rlt = validate();
            if (rlt.IsSuccess == 0) {
                $('.tips').text(rlt.Msg);
                return;
            }
            buildQueryParameter();
            queryList();
        });
    }
    function bindExportClick() {
        app.section.delegate('._export', 'click', function () {
            exportList();
        });
    }
    //初始化门店树控件事件
    function initTxtUnitEvent() {
        app.section.delegate('#txtUnit', 'click', function () {
            $('#ztreeUnit').show();
        }).delegate('#ztreeUnit', 'mouseleave', function () {
            $(this).hide();
        });
    }
    //初始化日期控件
    function initDatePicker() {
        $('.datepicker').datetimepicker({
            allowBlank: true,  //失去焦点是否可以为空
            lang: "ch",
            format: 'Y-m-d',
            timepicker: false  //不显示小时和分钟
            //step: 5 //分钟步长
        });
    }
    //加载门店
    function loadUnit() {
        var jdata = { action: "loadunit" };
        $.util.ajax({
            url: app.gateway,
            type: 'get',
            data: jdata,
            dataType: 'json',
            success: function (data) {
                $('.ztree').data('forminfo', data);
                initZTree();
            },
            error: function (err) {
            }
        });
    }
    //初始化
    function initialize() {
        initDatePicker();
        bindExportClick();
        initTxtUnitEvent();
        initQueryClickEvent();
    }
    $(function () {
        app.initialize();
        app.loadUnit();
    });
});