var ajaxPath = "/Module/Vip/VipClear/Handler/VipClearHandler.ashx";

function loadData() {

    Ext.Ajax.request({
        url: ajaxPath
                , params: { pPageSize: 3, pPageInex: 0, pVIPClearID: 1, method: 'SearchGroup' }
                , method: 'POST'
                , async: false
                , callback: function (options, success, response) {
                    var json = Ext.JSON.decode(response.responseText);
                    //                GridDataDefinds
                    //                GridColumnDefinds
                    //                VipCroupRows
                    //                VipCroupDatas

                    var headhtml = GetHead(json.GridColumnDefinds);
                    var shtml = GetDatas(json.GridColumnDefinds, json.VipDatas, json.VipCroupDatas);
                    document.getElementById("span_panel2").innerHTML = "<table class='jitgrid'>" + headhtml + shtml + "</talbe>";
                }
    });
    //获取数据
}

function GetDatas(GridColumnDefinds, VipDatas, VipCroupDatas) {
    var shtml = "";
 for (var i = 0; i < VipCroupDatas.length; i++) {
     var vipda = GetVipDatas(VipDatas, VipCroupDatas[i].DuplicateGroup);
     var html="";
    for (var ii = 0; ii < vipda.length; ii++) {
        html = html + '<tr ><td><input type="checkbox" name="checkbox" id="checkbox" /> </td>';
        for (var iii = 0; iii < GridColumnDefinds.length; iii++) {
            text = "";
        if (vipda[ii][GridColumnDefinds[iii].DataIndex]!=null)
            text = vipda[ii][GridColumnDefinds[iii].DataIndex];
        html = html + "<td>" + text + "</td>";
             }
             html = html + "</tr>";
         }
      
             html = html + '<tr ><td width="75px">结果数据：</td>';
             for (var iii = 0; iii < GridColumnDefinds.length; iii++) {
                 html = html + "<td></td>";
             }
             html = html + "</tr>";
        
         shtml = shtml + html + '<tr><td colspan="' + GridColumnDefinds.length + 1 + '"><input type="submit" name="button" id="button" value="合并数据" />  <input type="submit" name="button2" id="button2" value="不合并" /></td></tr><tr>';
     }
     shtml = shtml+'<tr><td colspan="' + GridColumnDefinds.length + 1 + '">分页</td></tr><tr>';
 return shtml;
}

function GetVipDatas(VipDatas, DuplicateGroup) {
    var arr = new Array();
    for (var i = 0; i < VipDatas.length; i++) {
        if (VipDatas[i].DuplicateGroup == DuplicateGroup) {
            arr.push(VipDatas[i]);
        }
    }
    return arr;

}
function GetHead(GridColumnDefinds)
{
    var headhtml = '<tr style="background-color: gray;"><td></td>'
    for(var i=0;i<GridColumnDefinds.length;i++){
     headhtml=headhtml+'<td >'+GridColumnDefinds[i].ColumnText+'</td>';
    }
    headhtml = headhtml + "</tr>";
    return headhtml;
}

function GetHtml()
{

}
Ext.onReady(function () {

    loadData();
});

