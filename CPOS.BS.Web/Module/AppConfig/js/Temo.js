function anonymous($data,$id) {
    var $helpers = this, $string = $helpers.$string, areaFlag = $data.areaFlag, i = $data.i, arrayList = $data.arrayList, idata = $data.idata, list = $data.list, $out = '';
    $out += '<div class="jsListItem commonSelectArea" data-type="rightEventTemp" data-key="';
    $out += $string(areaFlag);
    $out += '"  data-model="event">					<div class="noticeList">						<div class="list clearfix">							';
    for (var i = 0; i < arrayList.length; i++) {
        var idata = arrayList[i];
        $out += '								<div class="noticeArea ';
        if (i % 3 == 1) {
            $out += 'mlmr';
        }
        $out += '">									<div class="box">										';
        if (idata.typeId == 2) {
            $out += '											<h2 class="title clock">限时抢购</h2>											<div class="timeList" data-time="';
            $out += $string(idata.remainingSec);
            $out += '">												<em>00</em>:<em>00</em>:<em>00</em>											</div>										';
        } else if (idata.typeId == 1) {
            $out += '											<h2 class="title group">疯狂团购</h2>											<div class="timeList" data-time="';
            $out += $string(idata.remainingSec);
            $out += '">												<em>00</em>:<em>00</em>:<em>00</em>											</div>										';
        } else if (idata.typeId == 3) {
            $out += '											<h2 class="title clock">热销榜单</h2>										  <div class="end">你值得拥有</div>										';
        } else {
            $out += '											<h2 class="title clock">未知分类</h2>										';
        }
        $out += '										';
        if (i == 1) {
            $out += '										 <div class="center"> 												<img src="';
            $out += $string(list[i].imageUrl);
            $out += '">									      </div>										';
        else
            {
                $out += '									<img src="';
                $out += $string(list[i].imageUrl);
                $out += '">										';
            }
            $out += '									</div>									<div class="info">									</div>								</div>							';
        }
        $out += '						</div>					</div>					   <div class="handle">				      <div class="bg"></div> 	                  <span class="jsExitGroup"></span> 						<span class="jsRemoveGroup"></span>					</div>				</div>';
        return new String($out);
    }

}