// JavaScript Document

var QuestArr = [
	
	{
		title:"什么是业主委员会？",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;业主委员会，是指由物业管理区域内业主代表组成，代表业主的利益，向社会各方反映业主意愿和要求，并监督物业管理公司管理运作的一个民间性组织。"
	},
	
	{
		title:"联洋花园为什么要成立业委会？",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因为联洋花园未成立业主委员会,累计2000多万元的维修基金不能使用，同时每年损失的利息高达100多万元，多年来循环损失的利息不计其数，小区公共事务管理因缺少经费而存在缺失(如:小区安全管理重视不够、车辆停放困难重重、卫生清理不及时、电梯维修欠缺、小区绿化欠缺管理等等)， 加上小区设施设备由于时间长远，已经到了需要维护的重要时点，只有尽快成立业委会，才能计划开展启动小区设施设备的重大维护保养项目（水泵，电梯，弱电系统等等）。"
	},
	
	{
		title:"为什么联洋花园1、2、3期只能成立一个业委会？",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;《上海市住宅物业管理规定》第七条：住宅小区，包括分期建设或者两个以上单位共同开发建设的住宅小区，其设置的配套设施设备是共用的，应当划分为一个物业管理区域；由于联洋花园1、2、3期的配套设施设备是共用的，所以划分为一个物业管理区域。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;第十二条：业主大会由一个物业管理区域内的全体业主组成。所以，根据第七条和第十二条，联洋花园1、2、3期只能成立一个业委会。"
	},
	
	{
		title:"成立业委会的程序？",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;通常成立业委会需要经过以下九个程序：</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.提出申请。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.产生筹备组成员。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.筹备组人员公告。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.进行筹备工作。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.筹备工作公告。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.业主大会召开。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.备案。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.出备案证，刻制印章。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.委员会名单等公告。"
	},
	
	{
		title:"维修基金的使用规则",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;按照“上海市商品住宅维修基金管理办法”，简单说:业委会成立后建立维修基金总账和分账，公共部位维修基金是按户分摊，总账核算，个人住户按照一幢或者一个门号为单位核算使用，当余额不足原缴纳维修基金30%时，业委会要负责通知筹集全部或者一幢或门号业主再次缴纳维修基金。"
	},
	
	{
		title:"成立业委会的障碍",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.物业反对业委会的监督。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.政府机关的行政阻碍。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.法律法规的缺失。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.业主协调难度大。"
	},

	{
		title:"成立业委会的注意事项",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.筹备组成立业委会的所有实质性事项，业主大会确认筹备结果。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.业委会只有团结所有业主，才能选举出代表全体业主真正利益的代言人。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.开发商在房屋没有售完前也是业主，也有权参与筹备和投票，房地局规定单个业主投票权不能超过30%。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.会议形式尽量以书面征求意见，凝聚漠视业主的力量，避免操纵选举。"
	},
	
	{
		title:"业主大会召开和选举办法",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.业主大会应由过半数以上具有投票权的业主参加。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.由筹备小组或业委会进行资格确认。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.候选人名单应在业主大会召开前公示一周。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.由业主大会筹备小组或上届业主委员主持。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.业委会委员的选举一律采用实名投票的方法。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.所投的票数等于或者少于投票人数的有效。选票所选的人数等于或者少于规定应选代表人数的有效。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.全体业主超过1/2参加投票选举有效，选举结果当场宣布。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.业委员委员受全体业主直接监督。"
	},
	
	{
		title:"居民事务协调小组的权利和业委会区别",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目前的居民事务协调小组，是在业委会成立前，代表业主和物业，居委沟通的桥梁，在法律允许范围内工作，不能动用2500万的维修基金，不能和物业进行服务合同签署。其成员，可以是业委会筹备组成员，在业委会成立后，筹备组和协调小组自动解散。业委会筹备组，是业委会成立过程中，法律定义的一个组织，是为了业委会成立而产生的，职责是明确的。见关于业委会成立中筹备组职责部分。"
	},

	{
		title:"成立业委会的程序",
		description:"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.业主向区（县）房管局提出申请。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.产生筹备组成员。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.筹备组成员名单公告。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.进行筹备工作。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.筹备工作公告。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.召开业主大会。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7业委会向区（县）房管部门备案。</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.委员名单等公告。"
	}
]