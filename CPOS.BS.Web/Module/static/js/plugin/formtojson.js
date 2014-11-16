/*
* jQuery扩展插件
* 直接将form表单元素转化为json对象
* param act:action名
* author yehua.zhang
*/
(function ($) {
    $.fn.formtojson = function (act) {
        //默认取Form的action，如果没有从参数里面取。
        var _act = $.trim(this.attr("action"));
        var action = _act.length ? _act : act;
        //声明json对象
        var o = action ? { action: action} : {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }
})(jQuery);