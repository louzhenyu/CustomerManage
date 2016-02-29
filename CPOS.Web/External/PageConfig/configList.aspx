<!DOCTYPE html>
<html>
<head>
    <title>页面模板上传页面</title>
    <link href="css/style3.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/pagination.css" rel="stylesheet" type="text/css" />
       <script type="text/javascript">
           
           fnShowLoading = function (msg) {
               var lblInit = document.getElementById("lblInit");
               var lblClose = document.getElementById("lblClose");
               lblInit.style.display = "";
               lblClose.style.display = "";
               lblInit.innerHTML = msg;// "正在初始化，可能需要几分钟...";
               return true;
           }
           fnHideLoading = function (msg) {
               if (msg == "") msg = "初始化完成";
               var lblInit = document.getElementById("lblInit");
               var lblClose = document.getElementById("lblClose");
               lblInit.innerHTML = msg;
               lblInit.style.display = "";
               lblClose.style.display = "";
           }
           fnClose = function (msg) {
               var lblInit = document.getElementById("lblInit");
               var lblClose = document.getElementById("lblClose");
               lblInit.style.display = "none";
               lblClose.style.display = "none";
           }
   </script> 
</head>
<body>
    <div id="section" class="page commonOutArea" data-js="/External/PageConfig/js/configList">
        <div class="commonTitleWrap">
             <h2>前端页面客户化配置列表</h2>
        </div>
    
        <div class="pageUserConfigArea">
           <%-- loading信息--%>
              <div id="lblInit" style="position:absolute; left:300px; top: 120px; padding:4px; padding-left:8px;background:#eee; border:1px solid #333; height:50px; width: 300px; line-height:50px; font-size:12px; display:none; "></div>
    <div id="lblClose" style="position:absolute; left:560px; font-size:12px; top: 125px; height:20px; width:30px; color:#333; cursor:pointer;display:none;" onclick="fnClose()">关闭</div>

            <div class="queryWrap clearfix">
                <span id="publicBtn" class="queryBtn">发布</span>
                <a href="configAdd.aspx" class="addBtn">添加</a>
            </div>
		    <div class="queryWrap clearfix">
        	    <span class="tit">页面KEY</span>
                <input id="keyInput" type="text" class="inputKey">
                <span class="tit">页面名</span>
                <input id="nameInput" type="text" class="inputName">
                <span id="searchBtn" class="queryBtn">查询</span>
            </div>
            <div class="queryListWrap">
        	    <table id="pageList" width="730" border="1">
                    <thead>
                        <tr>
                            <th>操作</th>
                            <th>Key</th>
                            <th>页面名</th>
                            <th>版本号</th>
                            <th>更新时间</th>
                        </tr>
                    </thead>
                    <tbody>
                      <%--<tr>
                        <td><a href="javascript:;" class="editText">编辑</a></td>
                        <td>6368536</td>
                        <td>微官网首页</td>
                        <td>1.1.0</td>
                        <td>2014-05-06</td>
                      </tr>
                      <tr class="lineBg">
                        <td><a href="javascript:;" class="editText">编辑</a></td>
                        <td>6368536</td>
                        <td>详情页面</td>
                        <td>1.1.0</td>
                        <td>2014-05-06</td>
                      </tr>--%>
                    </tbody>
                </table>
            </div>
            <div class="pageWrap" style="display:none;">
                <div class="pagination">
                    <a href="javascript:;" class="first" data-action="first">&laquo;</a>
                    <a href="javascript:;" class="previous" data-action="previous">&lsaquo;</a>
                    <input type="text" readonly="readonly" />
                    <a href="javascript:;" class="next" data-action="next">&rsaquo;</a>
                    <a href="javascript:;" class="last" data-action="last">&raquo;</a>
                </div>
            </div>
        </div>    
    </div>
    <script type="text/javascript" src="/External/static/js/lib/require.js"  defer  async="true" data-main="/External/static/js/main.js"></script>

</body>
</html>
