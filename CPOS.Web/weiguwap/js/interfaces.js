
// 接口名称 Url：待定
// GetReservationData.asmx?type=ServiceItemsData
// --根据服务变量的名称定义请求类型
//
//-- 可选服务内容

var ServiceItemsData = {"code":"200","description":"操作成功","content":{"DataItems":[
{"ServiceItemId":"1","ItemTitle":"45分钟洗车服务"},
{"ServiceItemId":"2","ItemTitle":"15分钟洗车服务"},
{"ServiceItemId":"3","ItemTitle":"贴膜"},
{"ServiceItemId":"4","ItemTitle":"汽车美容"}
]}};

// -- 可选日期段,Status 0 空,1,满。

var reqData={"common":{"userId":"238869","codeId":"83u824c"},"special":{"ServiceItemId":"2"}}
var DateItemsData = {"code":"200","description":"操作成功","ServiceItemId":"2","ServiceTitle":"15分钟洗车服务","content":{"DataItems":[
{"DateItemId":"1","ItemTitle":"6月24 星期一","Status":"1"},
{"DateItemId":"2","ItemTitle":"6月25 星期二","Status":"0"},
{"DateItemId":"3","ItemTitle":"6月26 星期三","Status":"0"},
{"DateItemId":"4","ItemTitle":"6月27 星期四","Status":"0"},
{"DateItemId":"5","ItemTitle":"6月28 星期五","Status":"0"},
{"DateItemId":"6","ItemTitle":"6月29 星期六","Status":"0"},
{"DateItemId":"7","ItemTitle":"6月30 星期日","Status":"0"},
{"DateItemId":"8","ItemTitle":"7月1 星期一","Status":"0"},
{"DateItemId":"9","ItemTitle":"7月2 星期二","Status":"0"},
{"DateItemId":"10","ItemTitle":"7月3 星期三","Status":"0"},
{"DateItemId":"11","ItemTitle":"7月4 星期四","Status":"0"},
{"DateItemId":"13","ItemTitle":"7月5 星期五","Status":"0"},
{"DateItemId":"13","ItemTitle":"7月6 星期六","Status":"0"},
{"DateItemId":"14","ItemTitle":"7月7 星期日","Status":"0"},
{"DateItemId":"15","ItemTitle":"7月8 星期一","Status":"0"},
{"DateItemId":"16","ItemTitle":"7月9 星期二","Status":"0"},
{"DateItemId":"17","ItemTitle":"7月10 星期三","Status":"0"},
{"DateItemId":"18","ItemTitle":"7月11 星期四","Status":"0"},
{"DateItemId":"19","ItemTitle":"7月13 星期五","Status":"0"},
{"DateItemId":"20","ItemTitle":"7月13 星期六","Status":"0"}
]}};

// 可选小时段,Status 0 空,1,满。

var reqData1={"common":{"userId":"238869","codeId":"83u824c"},"special":{"ServiceItemId":"2","DateItemId":"1"}}
var HourItemsData = {"code":"200","description":"操作成功","ServiceItemId":"2","ServiceTitle":"15分钟洗车服务","DateItemId":"1","DateTitle":"6月24 星期一","content":{"DataItems":[
{"HourItemId":"1","ItemTitle":"10:00-11:30","Status":"1"},
{"HourItemId":"2","ItemTitle":"11:30-13:00","Status":"0"},
{"HourItemId":"3","ItemTitle":"13:00-14:30","Status":"0"},
{"HourItemId":"4","ItemTitle":"14:30-16:00","Status":"0"},
{"HourItemId":"5","ItemTitle":"16:00-18:00","Status":"0"}
]}};

//可选时间段,Status 0 空,1,满。

var reqData2={"common":{"userId":"238869","codeId":"83u824c"},"special":{"ServiceItemId":"2","DateItemId":"1","HourItemId":"1"}}
var TimeItemsData = {"code":"200","description":"操作成功","ServiceItemId":"2","ServiceTitle":"15分钟洗车服务","DateItemId":"1","DateTitle":"6月24 星期一","HourItemId":"1","HourTitle":"10:00-13:00","content":{"DataItems":[
{"TimeItemId":"1","ItemTitle":"10:00-10:15","Status":"1"},
{"TimeItemId":"2","ItemTitle":"10:15-10:30","Status":"0"},
{"TimeItemId":"3","ItemTitle":"10:30-10:45","Status":"0"},
{"TimeItemId":"4","ItemTitle":"10:45-11:00","Status":"0"},
{"TimeItemId":"5","ItemTitle":"11:00-11:15","Status":"0"},
{"TimeItemId":"6","ItemTitle":"11:15-11:30","Status":"0"}
]}};


// 预定列表,Status 0 未开始,1,进行中,2 已完成。

var reqData3={"common":{"userId":"238869","codeId":"83u824c"},"special":{"StatusId":"1"}}
var ReservationData = {"code":"200","description":"操作成功","UserId":"238869","UserCode":"83u824c","content":{"DataItems":[
{"ReservationId":"1","ServiceId":"1","Position":"工位1","ServiceTitle":"45分钟洗车服务","DateItemId":"1","DateTitle":"6月24 星期一","TimeItemId":"1","TimeTitle":"10:00-10:15","Status":"0"},
{"ReservationId":"2","ServiceId":"2","Position":"工位2","ServiceTitle":"15分钟洗车服务","DateItemId":"2","DateTitle":"6月25 星期二","TimeItemId":"1","TimeTitle":"10:00-10:15","Status":"1"},
{"ReservationId":"3","ServiceId":"3","Position":"工位3","ServiceTitle":"汽车覆膜服务","DateItemId":"3","DateTitle":"6月26 星期三","TimeItemId":"2","TimeTitle":"10:15-10:35","Status":"2"},
{"ReservationId":"4","ServiceId":"4","Position":"工位4","ServiceTitle":"汽车美容服务","DateItemId":"1","DateTitle":"6月24 星期一","TimeItemId":"1","TimeTitle":"10:00-10:15","Status":"0"}
]}};


var SubmitOrder ={"code":"200","description":"操作成功","content":{"vip":"123456","username":"王孟孟","bus":"沪A41474","balance":"1230","vipPrice":"80"}}
var LoginReturnData ={"code":"200","description":"操作成功","userId":"238869","codeId":"83u824c"}; 
var CacelReturnData = {"code":"200","description":"操作成功"}


