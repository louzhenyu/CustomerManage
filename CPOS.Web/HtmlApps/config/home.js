{
	'Config' : {
		'Name':'微官网',
		'Shorthand':{
			'hm':'common/home/',
			'ph':'common/photos/',
			'cars':'common/cars/'
		}
	},
	"$HomeIndex1":{    
	'path' : 'common/home/homeIndex1.html',
	'title' : '微官网',
	'style' : ['common/home/homeIndex1'],
	'plugin':[''],
	'script':['common/home/index1'],
	'param':{
		'backgroundImg':"http://attach.bbs.miui.com/forum/201305/23/010922xj3gtagaitxizajg.jpg",
		'links':[{
			backgroundColor:"gray",
			title:"测试1",
			toUrl:"javascript:Jit.AM.toPage('NewDetail','&typeId=06E96E2B023741438F662FDB6DCE2E60&title=生益印象')"
		},
		{
			backgroundColor:"gray",
			title:"哎哟",
			toUrl:"javascript:Jit.AM.toPage('NewDetail','&typeId=06E96E2B023741438F662FDB6DCE2E60&title=生益印象')"
		},
		{
			backgroundColor:"yellow",
			title:"测试3",
			toUrl:"javascript:Jit.AM.toPage('NewDetail','&typeId=06E96E2B023741438F662FDB6DCE2E60&title=生益印象')"
		},
		{
			backgroundColor:"red",
			title:"草泥马",
			toUrl:"javascript:Jit.AM.toPage('NewDetail','&typeId=06E96E2B023741438F662FDB6DCE2E60&title=生益印象')"
		}
		
		]
	}
	},
	'$HomeIndex':{
		'path':'common/home/index.html',
		'title':'微官网',
		'plugin':['alice'],
		'script':['common/home/index'],
		'style':['base/global'],
		'param':{
			'title':'哎哟我操',
			'logo':'../../../images/public/shengyuan_default/logo.jpg',
			'links':[
				{
					'title':'门店查询',
					'english':'STORE <br/>INFORMATION',
					'toUrl':"javascript:Jit.AM.toPage('StoreList')"
				},
				{
					'title':'最新活动',
					'english':'NEWEST',
					'toUrl':"javascript:Jit.AM.toPage('Activity')"/*全新代揽胜*/
				},
				{
					'title':'影讯',
					'english':'MOVIE <br/>INFORMATION',
					'toUrl':"javascript:Jit.AM.toPage('Introduce','&type=1')"/*揽胜极光*/
				},
				{
					'title':'热卖商品',
					'english':'HOT SALE',
					'toUrl':"javascript:Jit.AM.toPage('GoodsList')"/*揽胜极光*/
				},
				{
					'title':'联系我们',
					'english':'CONTACT US',
					'toUrl':"javascript:Jit.AM.toPage('Introduce','&type=1')"/*揽胜极光*/
				}
			],   /*跳转地址   相对路径或者javascript模式*/
			'backgroundImage':'../../../images/public/shengyuan_default/indexBg.jpg',
			'littleImage':'',
			'animateDirection':'up'   /*动画方向*/
		}
	},
	'$HomeIndex2': {
		'path': 'common/home/homeIndex2.html',
		'title': '首页',
		'plugin': ['iscroll'],
		'script': ['common/home/index2'],
		'style': ['common/home/homeIndex2'],
		'param':{
			backgroundImgArr:[
				{
					imgUrl:'../../../images/special/europe/indexBG.jpg'
				},
				{imgUrl:'../../../images/special/europe/indexBg2.jpg'},
				{imgUrl:'../../../images/special/europe/indexBg1.jpg'}
			],
			links:[
				{
					title:"傻鸟吧",
					toUrl:"javascript:alert('傻子~')",
					backgroundImg:"../../../images/special/europe/nav004.png",
					backgroundColor:"yellow"
				},
				{
					title:"二货",
					toUrl:"javascript:alert('二货~')",
					backgroundColor:"red"
				},
				{
					title:"二狗蛋",
					toUrl:"javascript:alert('二狗蛋')",
					backgroundColor:"yellow"
				},{
					title:"蛋蛋的忧伤",
					toUrl:"javascript:alert('忧伤')",
					backgroundColor:"orange"
				}
			],
			menus:[
				{
					title:"傻鸟吧",
					toUrl:"javascript:alert('傻子~')",
					backgroundImg:"../../../images/special/europe/nav004.png",
					backgroundColor:"yellow"
				},
				{
					title:"傻鸟吧",
					toUrl:"javascript:alert('傻子~')",
					backgroundImg:"../../../images/special/europe/nav004.png",
					backgroundColor:"yellow"
				},
				{
					title:"傻鸟吧",
					toUrl:"javascript:alert('傻子~')",
					backgroundImg:"../../../images/special/europe/nav004.png",
					backgroundColor:"yellow"
				}
				
			]
		}
	},
	'$HomeIndex3': {
		'path': 'common/home/homeIndex4.html',
		'title': '微官网',
		'plugin': ['iscroll'],
		'script': ['common/home/index3'],
		'style': ['common/home/homeIndex3'],
		'param':{
			links:[
				{
					title:"傻鸟吧",
					toUrl:"javascript:alert('傻子~')",
					backgroundImg:"../../../images/common/home_default/images4/logoIcon.png",
					backgroundColor:"yellow"
				},
				{
					title:"二货",
					toUrl:"javascript:alert('二货~')",
					backgroundImg:"../../../images/common/home_default/images4/zxdtIcon.png",
					backgroundColor:"red"
				},
				{
					title:"二狗蛋",
					toUrl:"javascript:alert('二狗蛋')",
					backgroundImg:"../../../images/common/home_default/images4/hdzqIcon.png",
					backgroundColor:"yellow"
				},{
					title:"蛋蛋的忧伤",
					toUrl:"javascript:alert('忧伤')",
					backgroundImg:"../../../images/common/home_default/images4/xkzsIcon.png",
					backgroundColor:"orange"
				},
				{
					title:"二货",
					toUrl:"javascript:alert('二货~')",
					backgroundImg:"../../../images/common/home_default/images4/zxdtIcon.png",
					backgroundColor:"red"
				},
				{
					title:"二狗蛋",
					toUrl:"javascript:alert('二狗蛋')",
					backgroundImg:"../../../images/common/home_default/images4/hdzqIcon.png",
					backgroundColor:"yellow"
				},{
					title:"蛋蛋的忧伤",
					toUrl:"javascript:alert('忧伤')",
					backgroundImg:"../../../images/common/home_default/images4/xkzsIcon.png",
					backgroundColor:"orange"
				},
				{
					title:"二货",
					toUrl:"javascript:alert('二货~')",
					backgroundImg:"../../../images/common/home_default/images4/zxdtIcon.png",
					backgroundColor:"red"
				},
				{
					title:"二狗蛋",
					toUrl:"javascript:alert('二狗蛋')",
					backgroundImg:"../../../images/common/home_default/images4/hdzqIcon.png",
					backgroundColor:"yellow"
				},{
					title:"蛋蛋的忧伤",
					toUrl:"javascript:alert('忧伤')",
					backgroundImg:"../../../images/common/home_default/images4/xkzsIcon.png",
					backgroundColor:"orange"
				}
			]
		}
	},
	'Photos':{
		'path':'%ph%photos.html',
		'title':'微相册',
		'plugin':['alice'],
		'script':['%ph%common'],
		'style':['%ph%photo']
	},
	'Cars2':{
		'path':'%cars%cars2.html',
		'title':'微汽车',
		'plugin':[],
		'script':['%cars%jquery.easing.1.3','%cars%txt_scroll'
		,'%cars%yl3d','%cars%ylMap','%cars%1_picker','%cars%2_picker.date',
		'%cars%3_picker.time','%cars%4_legacy','%cars%9_slidepic'
		,'%cars%wxm-core176ed4','%cars%wxshare','%cars%cars2'],
		'style':['%cars%default','%cars%default.date','%cars%default.time','%cars%main2']
	},
	'Cars':{
		'path':'%cars%cars.html',
		'title':'微汽车',
		'plugin':['jquery_plugin/sly'],
		'script':['%cars%cars'],
		'style':['%cars%default','%cars%default.date','%cars%default.time','%cars%main']
	},
	'Cars3':{
		'path':'%cars%cars3.html',
		'title':'微汽车',
		'plugin':['alice','jquery_plugin/nivo.slider.min','jquery_plugin/sly','jquery_plugin/transform'],
		'script':['%cars%cars3'],
		'style':['%cars%common','%cars%home',
		'common/nivo-slider','common/nivo-themes/bar/bar','common/cars/jit-font-awesome'
		]
	}
}