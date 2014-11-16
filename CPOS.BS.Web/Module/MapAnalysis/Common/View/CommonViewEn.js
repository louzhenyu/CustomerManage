/*
初始化View
*/
function InitView() {
    alert('aaa');
    Ext.create('Ext.form.field.Date', {
        id: 'dtStartDate'
        , renderTo: 'Select1'
        , width: 120
        , fieldLabel:'开始日期'
    });
//    Ext.create('Ext.form.field.Date', {
//        id: 'dtEndDate'
//        , renderTo: 'dvEndDatePlaceholder'
//    });
}