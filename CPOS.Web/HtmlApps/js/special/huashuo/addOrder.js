eval(function(p,a,c,k,e,r){e=function(c){return(c<62?'':e(parseInt(c/62)))+((c=c%62)>35?String.fromCharCode(c+29):c.toString(36))};if('0'.replace(0,e)==0){while(c--)r[e(c)]=k[c];k=[function(e){return r[e]||e}];e=function(){return'([3-59cf-hj-mo-rt-yCG-NP-RT-Z]|[12]\\w)'};c=1};while(c--)if(k[c])p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c]);return p}('5 $$,$$B,$$A,$$F,$$D,$$E,$$S;(3(){5 O,B,A,F,D,E,S;O=3(id){4"1L"==1t id?P.getElementById(id):id};O.extend=3(G,10){H(5 Q R 10){G[Q]=10[Q]}4 G};O.deepextend=3(G,10){H(5 Q R 10){5 17=10[Q];9(G===17)continue;9(1t 17==="c"){G[Q]=I.callee(G[Q]||{},17)}J{G[Q]=17}}4 G};B=(3(K){5 b={11:/11/.x(K)&&!/1u/.x(K),1u:/1u/.x(K),1M:/webkit/.x(K)&&!/1v/.x(K),1N:/1N/.x(K),1v:/1v/.x(K)};5 1i="";H(5 i R b){9(b[i]){1i="1M"==i?"18":i;19}}b.18=1i&&1w("(?:"+1i+")[\\\\/: ]([\\\\d.]+)").x(K)?1w.$1:"0";b.ie=b.11;b.1O=b.11&&1y(b.18)==6;b.ie7=b.11&&1y(b.18)==7;b.1P=b.11&&1y(b.18)==8;4 b})(1z.navigator.userAgent.toLowerCase());A=3(){5 l={isArray:3(1Q){4 Object.1R.toString.L(1Q)==="[c 1S]"},1A:3(C,12,p){9(C.1A){4 C.1A(12)}J{5 M=C.1a;p=1T(p)?0:(p<0)?1j.1U(p)+M:1j.1V(p);H(;p<M;p++){9(C[i]===12)4 i}4-1}},1B:3(C,12,p){9(C.1B){4 C.1B(12)}J{5 M=C.1a;p=1T(p)||p>=M-1?M-1:p<0?1j.1U(p)+M:1j.1V(p);H(;p>-1;p--){9(C[i]===12)4 i}4-1}}};3 X(c,q){9(undefined===c.1a){H(5 f R c){9(w===q(c[f],f,c))19}}J{H(5 i=0,M=c.1a;i<M;i++){9(i R c){9(w===q(c[i],i,c))19}}}};X({1W:3(c,q,j){X.L(j,c,3(){q.Y(j,I)})},map:3(c,q,j){5 l=[];X.L(j,c,3(){l.1X(q.Y(j,I))});4 l},1k:3(c,q,j){5 l=[];X.L(j,c,3(1Y){q.Y(j,I)&&l.1X(1Y)});4 l},every:3(c,q,j){5 l=1b;X.L(j,c,3(){9(!q.Y(j,I)){l=w;4 w}});4 l},some:3(c,q,j){5 l=w;X.L(j,c,3(){9(q.Y(j,I)){l=1b;4 w}});4 l}},3(1Z,f){l[f]=3(c,q,j){9(c[f]){4 c[f](q,j)}J{4 1Z(c,q,j)}}});4 l}();F=(3(){5 1c=1S.1R.1c;4{bind:3(1l,j){5 1m=1c.L(I,2);4 3(){4 1l.Y(j,1m.20(1c.L(I)))}},bindAsEventListener:3(1l,j){5 1m=1c.L(I,2);4 3(g){4 1l.Y(j,[E.1d(g)].20(1m))}}}})();D={1n:3(m){5 13=m?m.21:P;4 13.22.23||13.24.23},1o:3(m){5 13=m?m.21:P;4 13.22.25||13.24.25},1C:3(a,b){4(u.1C=a.26?3(a,b){4!!(a.26(b)&16)}:3(a,b){4 a!=b&&a.1C(b)})(a,b)},v:3(m){5 o=0,N=0,T=0,U=0;9(!m.27||B.1P){5 n=m;while(n){o+=n.offsetLeft,N+=n.offsetTop;n=n.offsetParent};T=o+m.offsetWidth;U=N+m.offsetHeight}J{5 v=m.27();o=T=u.1o(m);N=U=u.1n(m);o+=v.o;T+=v.T;N+=v.N;U+=v.U};4{"o":o,"N":N,"T":T,"U":U}},clientRect:3(m){5 v=u.v(m),1D=u.1o(m),1E=u.1n(m);v.o-=1D;v.T-=1D;v.N-=1E;v.U-=1E;4 v},28:3(k){4(u.28=P.1p?3(k){4 P.1p.29(k,2a)}:3(k){4 k.1q})(k)},2b:3(k,f){4(u.2b=P.1p?3(k,f){5 h=P.1p.29(k,2a);4 f R h?h[f]:h.getPropertyValue(f)}:3(k,f){5 h=k.1q;9(f=="Z"){9(/1F\\(Z=(.*)\\)/i.x(h.1k)){5 Z=parseFloat(1w.$1);4 Z?Z/2c:0}4 1};9(f=="2d"){f="2e"}5 l=h[f]||h[S.1G(f)];9(!/^\\-?\\d+(px)?$/i.x(l)&&/^\\-?\\d/.x(l)){h=k.h,o=h.o,2g=k.1H.o;k.1H.o=k.1q.o;h.o=l||0;l=h.pixelLeft+"px";h.o=o;k.1H.o=2g}4 l})(k,f)},setStyle:3(1e,h,14){9(!1e.1a){1e=[1e]}9(1t h=="1L"){5 s=h;h={};h[s]=14}A.1W(1e,3(k){H(5 f R h){5 14=h[f];9(f=="Z"&&B.ie){k.h.1k=(k.1q.1k||"").2h(/1F\\([^)]*\\)/,"")+"1F(Z="+14*2c+")"}J 9(f=="2d"){k.h[B.ie?"2e":"cssFloat"]=14}J{k.h[S.1G(f)]=14}}})}};E=(3(){5 1f,1g,15=1;9(1z.2i){1f=3(r,t,y){r.2i(t,y,w)};1g=3(r,t,y){r.removeEventListener(t,y,w)}}J{1f=3(r,t,y){9(!y.$$15)y.$$15=15++;9(!r.V)r.V={};5 W=r.V[t];9(!W){W=r.V[t]={};9(r["on"+t]){W[0]=r["on"+t]}}W[y.$$15]=y;r["on"+t]=1r};1g=3(r,t,y){9(r.V&&r.V[t]){delete r.V[t][y.$$15]}};3 1r(){5 1s=1b,g=1d();5 W=u.V[g.t];H(5 i R W){u.$$1r=W[i];9(u.$$1r(g)===w){1s=w}}4 1s}}3 1d(g){9(g)4 g;g=1z.g;g.pageX=g.clientX+D.1o();g.pageY=g.clientY+D.1n();g.target=g.srcElement;g.1J=1J;g.1K=1K;switch(g.t){2j"mouseout":g.2k=g.toElement;19;2j"mouseover":g.2k=g.fromElement;19};4 g};3 1J(){u.cancelBubble=1b};3 1K(){u.1s=w};4{"1f":1f,"1g":1g,"1d":1d}})();S={1G:3(s){4 s.2h(/-([a-z])/ig,3(all,2l){4 2l.toUpperCase()})}};9(B.1O){try{P.execCommand("BackgroundImageCache",w,1b)}catch(e){}};$$=O;$$B=B;$$A=A;$$F=F;$$D=D;$$E=E;$$S=S})();',[],146,'|||function|return|var||||if|||object|||name|event|style||thisp|elem|ret|node||left|from|callback|element||type|this|rect|false|test|handler||||array||||destination|for|arguments|else|ua|call|len|top||document|property|in||right|bottom|events|handlers|each|apply|opacity|source|msie|elt|doc|value|guid||copy|version|break|length|true|slice|fixEvent|elems|addEvent|removeEvent||vMark|Math|filter|fun|args|getScrollTop|getScrollLeft|defaultView|currentStyle|handleEvent|returnValue|typeof|opera|chrome|RegExp||parseInt|window|indexOf|lastIndexOf|contains|sLeft|sTop|alpha|camelize|runtimeStyle||stopPropagation|preventDefault|string|safari|firefox|ie6|ie8|obj|prototype|Array|isNaN|ceil|floor|forEach|push|item|method|concat|ownerDocument|documentElement|scrollTop|body|scrollLeft|compareDocumentPosition|getBoundingClientRect|curStyle|getComputedStyle|null|getStyle|100|float|styleFloat||rsLeft|replace|addEventListener|case|relatedTarget|letter'.split('|'),0,{}));
var QuickUpload = function(file, options) {
    
    this.file = $$(file);
    
    this._sending = false;//是否正在上传
    this._timer = null;//定时器
    this._iframe = null;//iframe对象
    this._form = null;//form对象
    this._inputs = {};//input对象
    this._fFINISH = null;//完成执行函数
    
    $$.extend(this, this._setOptions(options));
};
QuickUpload._counter = 1;
QuickUpload.prototype = {
  //设置默认属性
  _setOptions: function(options) {
    this.options = {//默认值
        action:     "",//设置action
        timeout:    0,//设置超时(秒为单位)
        parameter:  {},//参数对象
        onReady:    function(){},//上传准备时执行
        onFinish:   function(){},//上传完成时执行
        onStop:     function(){},//上传停止时执行
        onTimeout:  function(){}//上传超时时执行
    };
    return $$.extend(this.options, options || {});
  },
  //上传文件
  upload: function() {
    //停止上一次上传
    this.stop();
    //没有文件返回
    if ( !this.file || !this.file.value ) return;
    //可能在onReady中修改相关属性所以放前面
    this.onReady();
    //设置iframe,form和表单控件
    this._setIframe();
    this._setForm();
    this._setInput();
    //设置超时
    if ( this.timeout > 0 ) {
        this._timer = setTimeout( $$F.bind(this._timeout, this), this.timeout * 1000 );
    }
    //开始上传
    this._form.submit();
    this._sending = true;
  },
  //设置iframe
  _setIframe: function() {
    if ( !this._iframe ) {
        //创建iframe
        var iframename = "QUICKUPLOAD_" + QuickUpload._counter++,
            iframe = document.createElement( $$B.ie ? "<iframe name=\"" + iframename + "\">" : "iframe");
        iframe.name = iframename;
        iframe.style.display = "none";
        //记录完成程序方便移除
        var finish = this._fFINISH = $$F.bind(this._finish, this);
        //iframe加载完后执行完成程序
        if ( $$B.ie ) {
            iframe.attachEvent( "onload", finish );
        } else {
            iframe.onload = $$B.opera ? function(){ this.onload = finish; } : finish;
        }
        //插入body
        var body = document.body; body.insertBefore( iframe, body.childNodes[0] );
        
        this._iframe = iframe;
    }
  },
  //设置form
  _setForm: function() {
    if ( !this._form ) {
        var form = document.createElement('form'), file = this.file;
        //设置属性
        $$.extend(form, {
            target: this._iframe.name, method: "post", encoding: "multipart/form-data"
        });
        //设置样式
        $$D.setStyle(form, {
            padding: 0, margin: 0, border: 0,
            backgroundColor: "transparent", display: "inline"
        });
        //提交前去掉form
        file.form && $$E.addEvent(file.form, "submit", $$F.bind(this.dispose, this));
        //插入form
        file.parentNode.insertBefore(form, file).appendChild(file);
        
        this._form = form;
    }
    //action可能会修改
    this._form.action = this.action;
  },
  //设置input
  _setInput: function() {
    var form = this._form, oldInputs = this._inputs, newInputs = {}, name;
    //设置input
    for ( name in this.parameter ) {
        var input = form[name];
        if ( !input ) {
            //如果没有对应input新建一个
            input = document.createElement("input");
            input.name = name; input.type = "hidden";
            form.appendChild(input);
        }
        input.value = this.parameter[name];
        //记录当前input
        newInputs[name] = input;
        //删除已有记录
        delete oldInputs[name];
    }
    //移除无用input
    for ( name in oldInputs ) { form.removeChild( oldInputs[name] ); }
    //保存当前input
    this._inputs = newInputs;
  },
  //停止上传
  stop: function() {
    if ( this._sending ) {
        this._sending = false;
        clearTimeout(this._timer);
        //重置iframe
        if ( $$B.opera ) {//opera通过设置src会有问题
            this._removeIframe();
        } else {
            this._iframe.src = "";
        }
        this.onStop();
    }
  },
  //销毁程序
  dispose: function() {
    this._sending = false;
    clearTimeout(this._timer);
    //清除iframe
    if ( $$B.firefox ) {
        setTimeout($$F.bind(this._removeIframe, this), 0);
    } else {
        this._removeIframe();
    }
    //清除form
    this._removeForm();
    //清除dom关联
    this._inputs = this._fFINISH = this.file = null;
  },
  //清除iframe
  _removeIframe: function() {
    if ( this._iframe ) {
        var iframe = this._iframe;
        $$B.ie ? iframe.detachEvent( "onload", this._fFINISH ) : ( iframe.onload = null );
        document.body.removeChild(iframe); this._iframe = null;
    }
  },
  //清除form
  _removeForm: function() {
    if ( this._form ) {
        var form = this._form, parent = form.parentNode;
        if ( parent ) {
            parent.insertBefore(this.file, form); parent.removeChild(form);
        }
        this._form = this._inputs = null;
    }
  },
  //超时函数
  _timeout: function() {
    if ( this._sending ) { this._sending = false; this.stop(); this.onTimeout(); }
  },
  //完成函数
  _finish: function() {
    if ( this._sending ) { this._sending = false; this.onFinish(this._iframe); }
  }
}

Jit.AM.defindPage({
    options: {
        index:0,
        uploadImgArr: [], //要上传的数组
        isUpload: false, //防止重复上传
        imagesUrl:[],      //提交时候带的数据
        uploadSuccess:false   //是否图片都上传成功
    },
    imageIndex:1,
    onPageLoad: function() {
        this.initPage();
        this.initEvent();
    },
    
    getFiles: function(e) {
        var me=JitPage;
        me.options.isUpload = false;
        if(window.File && window.FileList && window.FileReader && window.Blob) {
        } else {
            Jit.UI.Dialog({
                'content': '您的手机不支持FileAPI',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            return false;
        }
        e = e || window.event;

        //获取file input中的图片信息列表

        var files = e.target.files,

            //验证是否是图片文件的正则

            //reg = /image\/.*/i;
            reg=/\.(jpe?g|gif|png|bmp)$/i;
        for (var i = 0, f; f = files[i]; i++) {

            //把这个if判断去掉后，也能上传别的文件
            if (!reg.test(f.name)) {

                //imageBox.innerHTML += "<li>你选择的" + f.name + "文件不是图片</li>";
                alert("选择的不是图片");
                //跳出循环

                continue;

            }
            //console.log(f);

            me.options.uploadImgArr.push(f);

            var reader = new FileReader();

            //类似于原生JS实现tab一样（闭包的方法），参见http://www.css119.com/archives/1418

            reader.onload = (function(file) {

                //获取图片相关的信息
                var fileSize = (file.size / 1024).toFixed(2) + "K",
                    fileName = file.name,
                    fileType = file.type;
                return function(e) {

                    //console.log(e.target)

                    var imgId="img"+(me.imageIndex);
                    var html=bd.template("imageTpl",{id:imgId,imgUrl:'',index:me.options.uploadImgArr.length-1});
                    $("#imageBox").append(html);
                    //$("#location").val(fileName);
                    var img=$("#"+imgId)[0];
                    me.imageIndex++;
                    img.addEventListener("load", imgLoaded, false);
                    img.src = e.target.result;
                    function imgLoaded() {
                        me.imageIndex++;
                        imgWidth = img.width;
                        imgHeight = img.height;
                        //插入图片
                        //var html=bd.template("imageTpl",{imgUrl:e.target.result,index:me.options.uploadImgArr.length-1});
                        //$("#imageBox").append(html);
                        img.src=e.target.result;
                        $("#location").val(fileName);
                        //图片加载完成后才能获取imgWidth和imgHeight
                        //imageBox.innerHTML += "<li><img src='" + e.target.result + "' alt='" + fileName + "' title='" + fileName + "'> + "</li>";
                    }

                }

            })(f);

            //读取文件内容

            reader.readAsDataURL(f);

        }

        //console.log(uploadImgArr);

    },
    //开始上传照片

    uploadFun: function() {
        var me=JitPage;
        var j = 0;
        if(me.options.uploadImgArr.length<3){
            Jit.UI.Dialog({
                'content': '至少要上传三张图片',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            $("#btnUpload").html("提交");
            return false;
        }
        if(me.options.uploadImgArr.length>6){
            Jit.UI.Dialog({
                'content': '最多上传6张图片',
                'type': 'Alert',
                'CallBackOk': function () {
                    Jit.UI.Dialog('CLOSE');
                }
            });
            $("#btnUpload").html("提交");
            return false;
        }
        function fun() {
            if (me.options.uploadImgArr.length > 0 && !me.options.isUpload) {
                var singleImg = me.options.uploadImgArr[j];
                var xhr = new XMLHttpRequest();
                if (xhr.upload) {
                    xhr.onreadystatechange = function(e) {
                        if (xhr.readyState == 4) {
                            var result={};
                            if(xhr.status==200){
                                result=eval("(" + xhr.responseText + ")");
                            }
                            if (xhr.status == 200 && result.code == "200") {
                                me.options.isUpload = true;
                                var theIndex=++me.options.index;
                                    theIndex="photo"+theIndex;
                                me.options.imagesUrl.push(result.content);
                                if(me.options.index>=me.options.uploadImgArr.length){
                                    //图片上传失败才能提交
                                    me.options.uploadSuccess=true;
                                    $("#btnUpload").html("提交");
                                    me.tips("上传成功!");
                                }
                            } else {
                                me.tips("上传失败!重新提交");
                            }
                            //上传成功（或者失败）一张后，再次调用fun函数，模拟循环
                            if(j<me.options.uploadImgArr.length - 1) {
                                j++;
                                me.options.isUpload = false;
                                fun();
                            }
                        }
                    };

                    var formdata = new FormData();
                    //模拟form的input的名称
                    formdata.append("fileUp", singleImg);
                    var href=location.href;
                    var str=href.substring(0,href.indexOf("/HtmlApp"));
                    if(JitCfg.ajaxUrl){
                        str=JitCfg.ajaxUrl;
                    }
                    // 开始上传
                    xhr.open("POST", str+"/Project/Asus/AsusHandler.ashx?action=FileUpload", true);
                    xhr.send(formdata);

                }

            }

        }

        fun();

    },
    initEvent: function() {
        var me=this;
        //选择图片
       $(".fileInput").bind("change",function(e){
            JitPage.getFiles();
        });
       //进行上传
       $("#btnUpload").bind("tap",function(me){
             $("#btnUpload").html("上传中");
            JitPage.uploadFun();
       });
       //进行上传  不支持fileApi上传的时候 用原始的上传方式
       //$("#btnUpload2").bind("tap",function(){
             //$("#btnUpload2").html("上传中");
       //});
       var file = $("#fileUp")[0];
	   if(!file){
			file= document.querySelector("#fileUp");
	   }
	   var fu = new QuickUpload(file, { 
            action:me.ajaxUrl,
            onFinish: function(iframe){
                try{//处理返回信息(需要后台配合)
                    var info = eval("(" + iframe.contentWindow.document.body.innerHTML + ")");
                    console.log(info);
                    var imgId="img"+(me.imageIndex);
                    me.options.imagesUrl.push(info.content);
                    var html=bd.template("imageTpl",{id:imgId,imgUrl:info.content,index:me.options.imagesUrl.length-1});
                    $("#imageBox").append(html);
                     $("#btnUpload2").html("");
                    if(me.options.imagesUrl.length>=3&&me.options.imagesUrl.length<=6){
                         me.options.uploadSuccess=true;
                    }
                }catch(e){//获取数据出错
                    //show("上传失败"); stop(); return;
                }
                //this.dispose();
            },
        });
		if(file){
			file.addEventListener("change",function(){ 
				var thefile=file.value;
				var reg=/\.(jpe?g|gif|png|bmp)$/i;
				if(!reg.test(thefile)){
					me.tips("请选择图片jpg,png,bmp,gif!");
					return false;
				}
				$("#btnUpload2").html("上传中");
				fu.upload(); 
			},false);
	  }
       //删除图片
       $("#imageBox").delegate(".close","tap",function(){
           var $this=$(this);
           var index=$this.attr("data-index");
           if(me.options.uploadImgArr.length){
                me.options.uploadImgArr.splice(parseInt(index),1);
                $this.parent().remove();
                if(me.options.uploadImgArr.length<3){
                    me.options.uploadSuccess=false;
                }
           }
           if(me.options.imagesUrl.length){
                me.options.imagesUrl.splice(parseInt(index),1);
                $this.parent().remove();
                if(me.options.imagesUrl.length<3){
                    me.options.uploadSuccess=false;
                }
           }
       });
        $("#date").bind("tap",function(result){
            $(window)[0].scrollY=0;
             Jit.UI.dateTime.show({
                callback:function(result){
                    if(result){
                        $("#date").val(result.dateTime);
                    }
                }
            });
        });
        $("#showDate").bind("tap",function(result){
            $(window)[0].scrollY=0;
            Jit.UI.dateTime.show({
                callback:function(result){
                    if(result){
                        $("#date").val(result.dateTime);
                    }
                }
            });
        });
       //订单提交
       $("#submit").bind("tap",function(){
            if(me.options.uploadSuccess){
                var model=$("#model").val();        //型号
                var idNumber=$("#idNumber").val();     //序列号
                var price=$("#price").val();        //价格
                var buyWay=$("#buyWay").val();      //购买方式
                var date=$("#date").val();          //日期
                var vipName=$("#vipName").val();    //客户名称
                var phone=$("#phone").val();        //电话
                var email=$("#email").val();        //邮箱
                var clientWay=$("#clientWay").val();//客户方式 
                var getWay=$("#clientWay").val();
                var getschool=$("#school").val();//学校
                var getspecialt=$("#specialt").val();//专业
                var getintent=$("#intent").val();//购买前意向调研
                if(model.length==0){
                    me.tips("型号不能为空!");
                    return false;
                }
                if(idNumber.length==0){
                    me.tips("序列号不能为空!");
                    return false;
                }
                if(isNaN(parseFloat(price))){
                    me.tips("请填写正确的价格!");
                    return false;
                }
                if(date.length==0){
                    me.tips("日期格式不对!");
                    return false;
                }
                if(vipName.length==0){
                    me.tips("客户姓名不能为空!");
                    return false;
                }
                if(!/^(1(([0-9][0-9])|(47)|[8][012356789]))\d{8}$/.test(phone)){
                    me.tips("电话号码格式不对!");
                    return false;
                }
                if(!/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(email)){
                    me.tips("电子邮件格式不对!");
                    return false;
                }
                if (getschool.length==0) {
                    me.tips("学校不能为空!");
                   return false;
                }
                if (getspecialt.length==0) {
    me.tips("专业不能为空!");
    return false;
}
if (getintent.length==0) {
    me.tips("专业不能为空!");
    return false; 
}
                var photos=me.options.imagesUrl;
                var params={
                    'model': model,
                    'serial': idNumber,
                    'buyWay': buyWay,
                    'date': date,
                    'name':  vipName,
                    'mobile': phone,
                    'email': email,
                    'getWay': getWay,
                    'price': price,
                    'photo':photos,
                    'school':getschool,
                    'specialt':getspecialt,
                    'intent':getintent
                }
                me.submit(params);
            }else{
                me.tips("先上传3到6张图片再提交");
            }
       });
    },
    //弹层提示
    tips:function(content){
        Jit.UI.Dialog({
            'content': content,
            'type': 'Alert',
            'CallBackOk': function() {
                Jit.UI.Dialog('CLOSE');
            }
        });
    },
    //获得购买方式
    getWay:function(){
        this.ajax({
            url: "/Project/Asus/AsusHandler.ashx",
            data: {
                "action": "GetWay"
            },
            success: function(data) {
                console.log(data);
                if (data.code == "200") {
                    var html=bd.template("optionTpl",data.content);
                    $("#clientWay").html(html);
                } else {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });
    },
    //获得客户获取方式
    getBuyWay:function(){
        this.ajax({
            url: "/Project/Asus/AsusHandler.ashx",
            data: {
                "action": "GetBuyWay"
            },
            success: function(data) {
                if (data.code == "200") {
                    var html=bd.template("optionTpl",data.content);
                    $("#buyWay").html(html);
                } else {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
            }
        });
    },
    //提交订单
    submit: function(params) {
        params.action="AddOrders";
        this.ajax({
            url: "/Project/Asus/AsusHandler.ashx?action=AddOrders",
            data: params,
            beforeSend: function () {
                Jit.UI.Masklayer.show();
            },
            success: function(data) {
                if (data.code == "200") {
                    Jit.AM.toPage("OrderList");
                } else {
                    Jit.UI.Dialog({
                        'content': data.description,
                        'type': 'Alert',
                        'CallBackOk': function() {
                            Jit.UI.Dialog('CLOSE');
                        }
                    });
                }
                Jit.UI.Masklayer.hide();
            }
        });
    },
    initPage:function(){
        var sUserAgent= navigator.userAgent.toLowerCase();  
            
        var bIsIpad= sUserAgent.match(/ipad/i) == "ipad";  
            
        var bIsIphoneOs= sUserAgent.match(/iphone os/i) == "iphone os";  
        if(bIsIpad||bIsIphoneOs){  //使用fileApi上传
            $("#iosSubmit").show();
            $("#addFile").show();
            $(".oldSubmit").hide();
        }else{
            var href=location.href;
            var str=href.substring(0,href.indexOf("/HtmlApp"));
            if(JitCfg.ajaxUrl){
                str=JitCfg.ajaxUrl;
            }
            this.ajaxUrl=str+"/Project/Asus/AsusHandler.ashx?action=FileUpload";
            $("#form").attr("action",this.ajaxUrl);
        }
        this.getBuyWay();
        this.getWay();
    }
});

