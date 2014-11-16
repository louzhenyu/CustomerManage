!function(a,b){function c(a){return a.replace(/([a-z])([A-Z])/,"$1-$2").toLowerCase()}function d(a){return e?e+a:a.toLowerCase()}var e,f,g,h,i,j,k,l,m,n,o="",p={Webkit:"webkit",Moz:"",O:"o"},q=window.document,r=q.createElement("div"),s=/^((translate|rotate|scale)(X|Y|Z|3d)?|matrix(3d)?|perspective|skew(X|Y)?)$/i,t={};a.each(p,function(a,c){return r.style[a+"TransitionProperty"]!==b?(o="-"+a.toLowerCase()+"-",e=c,!1):void 0}),f=o+"transform",t[g=o+"transition-property"]=t[h=o+"transition-duration"]=t[j=o+"transition-delay"]=t[i=o+"transition-timing-function"]=t[k=o+"animation-name"]=t[l=o+"animation-duration"]=t[n=o+"animation-delay"]=t[m=o+"animation-timing-function"]="",a.fx={off:e===b&&r.style.transitionProperty===b,speeds:{_default:400,fast:200,slow:600},cssPrefix:o,transitionEnd:d("TransitionEnd"),animationEnd:d("AnimationEnd")},a.fn.animate=function(c,d,e,f,g){return a.isFunction(d)&&(f=d,e=b,d=b),a.isFunction(e)&&(f=e,e=b),a.isPlainObject(d)&&(e=d.easing,f=d.complete,g=d.delay,d=d.duration),d&&(d=("number"==typeof d?d:a.fx.speeds[d]||a.fx.speeds._default)/1e3),g&&(g=parseFloat(g)/1e3),this.anim(c,d,e,f,g)},a.fn.anim=function(d,e,o,p,q){var r,u,v,w={},x="",y=this,z=a.fx.transitionEnd,A=!1;if(e===b&&(e=a.fx.speeds._default/1e3),q===b&&(q=0),a.fx.off&&(e=0),"string"==typeof d)w[k]=d,w[l]=e+"s",w[n]=q+"s",w[m]=o||"linear",z=a.fx.animationEnd;else{u=[];for(r in d)s.test(r)?x+=r+"("+d[r]+") ":(w[r]=d[r],u.push(c(r)));x&&(w[f]=x,u.push(f)),e>0&&"object"==typeof d&&(w[g]=u.join(", "),w[h]=e+"s",w[j]=q+"s",w[i]=o||"linear")}return v=function(b){if("undefined"!=typeof b){if(b.target!==b.currentTarget)return;a(b.target).unbind(z,v)}else a(this).unbind(z,v);A=!0,a(this).css(t),p&&p.call(this)},e>0&&(this.bind(z,v),setTimeout(function(){A||v.call(y)},1e3*e+25)),this.size()&&this.get(0).clientLeft,this.css(w),0>=e&&setTimeout(function(){y.each(function(){v.call(this)})},0),this},r=null}($);
//zepto动画fx模块
// !function(a){function i(a,b,c,d){return Math.abs(a-b)>=Math.abs(c-d)?a-b>0?"Left":"Right":c-d>0?"Up":"Down"}function j(){f=null,b.last&&(b.el.trigger("longTap"),b={})}function k(){f&&clearTimeout(f),f=null}function l(){c&&clearTimeout(c),d&&clearTimeout(d),e&&clearTimeout(e),f&&clearTimeout(f),c=d=e=f=null,b={}}function m(a){return("touch"==a.pointerType||a.pointerType==a.MSPOINTER_TYPE_TOUCH)&&a.isPrimary}function n(a,b){return a.type=="pointer"+b||a.type.toLowerCase()=="mspointer"+b}var c,d,e,f,h,b={},g=750;a(document).ready(function(){var o,p,s,t,q=0,r=0;"MSGesture"in window&&(h=new MSGesture,h.target=document.body),a(document).bind("MSGestureEnd",function(a){var c=a.velocityX>1?"Right":a.velocityX<-1?"Left":a.velocityY>1?"Down":a.velocityY<-1?"Up":null;c&&(b.el.trigger("swipe"),b.el.trigger("swipe"+c))}).on("touchstart MSPointerDown pointerdown",function(d){(!(t=n(d,"down"))||m(d))&&(s=t?d:d.touches[0],d.touches&&1===d.touches.length&&b.x2&&(b.x2=void 0,b.y2=void 0),o=Date.now(),p=o-(b.last||o),b.el=a("tagName"in s.target?s.target:s.target.parentNode),c&&clearTimeout(c),b.x1=s.pageX,b.y1=s.pageY,p>0&&250>=p&&(b.isDoubleTap=!0),b.last=o,f=setTimeout(j,g),h&&t&&h.addPointer(d.pointerId))}).on("touchmove MSPointerMove pointermove",function(a){(!(t=n(a,"move"))||m(a))&&(s=t?a:a.touches[0],k(),b.x2=s.pageX,b.y2=s.pageY,q+=Math.abs(b.x1-b.x2),r+=Math.abs(b.y1-b.y2))}).on("touchend MSPointerUp pointerup",function(f){(!(t=n(f,"up"))||m(f))&&(k(),b.x2&&Math.abs(b.x1-b.x2)>30||b.y2&&Math.abs(b.y1-b.y2)>30?e=setTimeout(function(){b.el.trigger("swipe"),b.el.trigger("swipe"+i(b.x1,b.x2,b.y1,b.y2)),b={}},0):"last"in b&&(30>q&&30>r?d=setTimeout(function(){var d=a.Event("tap");d.cancelTouch=l,b.el.trigger(d),b.isDoubleTap?(b.el&&b.el.trigger("doubleTap"),b={}):c=setTimeout(function(){c=null,b.el&&b.el.trigger("singleTap"),b={}},250)},0):b={}),q=r=0)}).on("touchcancel MSPointerCancel pointercancel",l),a(window).on("scroll",l)}),["swipe","swipeLeft","swipeRight","swipeUp","swipeDown","doubleTap","tap","singleTap","longTap"].forEach(function(b){a.fn[b]=function(a){return this.on(b,a)}})}(Zepto);
//zepto的touch模块
// ;(function($){var zepto=$.zepto,oldQsa=zepto.qsa,oldMatches=zepto.matches function visible(elem){elem=$(elem)return!!(elem.width()||elem.height())&&elem.css("display")!=="none"}var filters=$.expr[':']={visible:function(){if(visible(this))return this},hidden:function(){if(!visible(this))return this},selected:function(){if(this.selected)return this},checked:function(){if(this.checked)return this},parent:function(){return this.parentNode},first:function(idx){if(idx===0)return this},last:function(idx,nodes){if(idx===nodes.length-1)return this},eq:function(idx,_,value){if(idx===value)return this},contains:function(idx,_,text){if($(this).text().indexOf(text)>-1)return this},has:function(idx,_,sel){if(zepto.qsa(this,sel).length)return this}}var filterRe=new RegExp('(.*):(\\w+)(?:\\(([^)]+)\\))?$\\s*'),childRe=/^\s*>/,classTag='Zepto'+(+new Date())function process(sel,fn){sel=sel.replace(/=#\]/g,'="#"]')var filter,arg,match=filterRe.exec(sel)if(match&&match[2]in filters){filter=filters[match[2]],arg=match[3]sel=match[1]if(arg){var num=Number(arg)if(isNaN(num))arg=arg.replace(/^["']|["']$/g,'')else arg=num}}return fn(sel,filter,arg)}zepto.qsa=function(node,selector){return process(selector,function(sel,filter,arg){try{var taggedParent if(!sel&&filter)sel='*'else if(childRe.test(sel))taggedParent=$(node).addClass(classTag),sel='.'+classTag+' '+sel var nodes=oldQsa(node,sel)}catch(e){console.error('error performing selector: %o',selector)throw e}finally{if(taggedParent)taggedParent.removeClass(classTag)}return!filter?nodes:zepto.uniq($.map(nodes,function(n,i){return filter.call(n,i,nodes,arg)}))})}zepto.matches=function(node,selector){return process(selector,function(sel,filter,arg){return(!sel||oldMatches(node,sel))&&(!filter||filter.call(node,null,arg)===node)})}})(Zepto)
//zepto选择器模块


//微商城公用类

var KeyList = {
  cartCount: 'cartCount',
  val:'val',
  userId:Jit.AM.getAppVersion().APP_CUSTOMERID+'_userId'
}, ClientSession = { //当前客户的session对象
    get: function(k) {
    return   Jit.AM.getAppSession(Jit.AM.getAppVersion(), k);
    },
    set: function(k, v) {
     return  Jit.AM.setAppSession(Jit.AM.getAppVersion(), k, v);
    }
  };
//拼接字符串，该方法效率要高于str+="str";
function StringBuilder() {
  this.strList = [];
  this.append = function(v) {
    if (v) {
      this.strList.push(v);
    };
  };
  this.appendFormat = function(v) {
    if (v) {
      if (arguments.length > 1) {
        for (var i = 1; i <arguments.length; i++) {
                var Rep = new RegExp("\\{" + (i - 1) + "\\}", "gi");
                v = v.replace(Rep, arguments[i]);
        };
      }
      this.strList.push(v);
    };
  };
  this.toString = function() {
    return this.strList.join('');
  };
}


//验证是否是手机浏览器
function IsMobileBrowser() {
  var hash = window.location.hash,
    result = false;
  if (!hash.match("fromapp")) {
    result = navigator.userAgent.match(/(iPhone|iPod|Android|ios)/i);
  }
  return result;
}


// 验证是否是手机号码
function IsMobileNumber(n){
return  /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/.test(n);

}




//公用导航
var TopMenuHandle = {
  BindScrollEvent: function() { //绑定滚动事件
    var navPane = $('#topNav'),
      curWindow = $(window);
    curScrollTop = curWindow.scrollTop(), eventTop = 1, stime = '';
    curWindow.scroll(function() {
      eventTop = parseInt(curWindow.scrollTop());
      if (eventTop <= 60) { //如果当前滚动条小于60像素不执行任何效果。
        navPane.css('top', 0);
        clearTimeout(stime);
        return false;
      };
      eventTop = curWindow.scrollTop();
      clearTimeout(stime);
      stime = setTimeout(function() {
        if (curScrollTop > eventTop) {
          navPane.animate({
            top: '0' //正常显示
          }, 150);
          // navPane.show();
        } else {
          // navPane.hide();
          navPane.animate({
            top: '-3.5em' //往上移动大小
          }, 150);
        }
        curScrollTop = curWindow.scrollTop();
        eventTop = curWindow.scrollTop();
      }, 100); //滚动条停止100毫秒后触发事件
    });

  }, //加载购物车数量，默认只加载一次缓存在SessionStorage中
  LoadCartCount: function() {
    var cartCount = ClientSession.get(KeyList.cartCount);
    menuCartCount = $('#menuCartCount');
    if (!menuCartCount.size()) {
      return false
    };
    if (cartCount && cartCount > 0) {
      menuCartCount.show();
      menuCartCount.html(cartCount);
      return false;
    };
    ClientSession.set(KeyList.cartCount, 0);
    this.ReCartCount();
  },
  SetCartCount: function(cartCount) {
    $('#menuCartCount').html(cartCount);
    ClientSession.set(KeyList.cartCount, cartCount);
  },
  ReCartCount: function() { //重新设置购物车数量
    var me = Jit.AM,
      menuCartCount = $('#menuCartCount');
    me.ajax({
      url: '/OnlineShopping/data/Data.aspx',
      data: {
        'action': 'getShoppingCartCount',
        'vipId':Jit.AM.getBaseAjaxParam().userId
      },
      success: function(data) {
        if (data.code == 200 && data.content.count && data.content.count > 0) {
          ClientSession.set(KeyList.cartCount, data.content.count);
          menuCartCount.html(data.content.count).show();
        } else {
          ClientSession.set(KeyList.cartCount, 0);
          menuCartCount.hide();
        }
      }
    });
  }
};



(function($, undefined) {

  //绑定导航事件
  TopMenuHandle.BindScrollEvent();
  //加载购物车数量
  TopMenuHandle.LoadCartCount();

})($)