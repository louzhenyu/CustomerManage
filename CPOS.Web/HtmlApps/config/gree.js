{
	'Config' : {
		'Name':'微官网',
		'Shorthand':{
			'gr':'public/gree/'
		}
	},
	'Common': {
			'script': [''],
			'style': ['base/global'],
	},
	"GreeReverse":{
		'path': '%gr%appointment.html',
		'title': '预约安装',
		'plugin': ['jquery_plugin/mobiscroll.core-2.5.2'],
		'script': ['%gr%appointment'],
		'style': ['%gr%style','common/mobiscroll.core-2.5.2']
	},
	"SelectAir":{
		'path': '%gr%selectAir.html',
		'title': '选择空调',
		'plugin': [''],
		'script': ['%gr%selectAir'],
		'style': ['%gr%style']
	},
	"IsAppointment":{
		'path': '%gr%isAppointment.html',
		'title': '预约师傅',
		'plugin': [''],
		'script': ['%gr%isAppointment'],
		'style': ['%gr%style']
	},
	"SelectShifu":{
		'path': '%gr%selectShifu.html',
		'title': '选择师傅',
		'plugin': ['bdTemplate'],
		'script': ['%gr%selectShifu'],
		'style': ['%gr%style']
	},
	"Evaluate":{
		'path': '%gr%evaluate.html',
		'title': '发表评价',
		'plugin': [''],
		'script': ['%gr%evaluate'],
		'style': ['%gr%style']
	},
	"EvaluateSuccess":{
		'path': '%gr%evaluateSuccess.html',
		'title': '评价成功',
		'plugin': [''],
		'script': [''],
		'style': ['%gr%style']
	},
	"ShifuTask":{
		'path': '%gr%shifuTask.html',
		'title': '师傅任务',
		'plugin': [''],
		'script': ['%gr%shifuTask'],
		'style': ['%gr%style']
	},
}