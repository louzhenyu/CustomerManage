<!DOCTYPE html>
<html>
<head>
    <title>页面模板上传页面</title>
    <link href="css/style3.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="section" class="page commonOutArea" data-js="/External/PageConfig/js/configAdd">
	    <div class="commonTitleWrap">
    	    <h2>页面模板上传页面</h2>
            <span id="submitBtn" class="submitBtn">提交</span>
            <span id="applyBtn" class="cancelBtn">应用到</span>
            <span id="cancelBtn" class="cancelBtn">返回</span>
        </div>
    
        <div class="moudleUploadPage">
            <div class="queryWrap clearfix">
        	    <span class="tit">作者</span>
                <input id="author" type="text" class="pageKey">
            </div>
            <div class="queryWrap clearfix">
        	    <span class="tit">版本</span>
                <input id="version" type="text" class="pageKey">
            </div>
            <textarea id="pageJson" class="moudleEditWrap"></textarea>
        </div>
    </div>
    <script type="text/javascript" src="/External/static/js/lib/require.js"  defer  async="true" data-main="/External/static/js/main.js"></script>
   
    <div class="ui-mask"></div>
    <div id="packageLayer" class="scratchCardPopup" style="display:none;">
	    <div class="commonTitleWrap">
    	    <h2>应用到套餐</h2>
            <span class="cancelBtn">取消</span>
            <span class="saveBtn">确定</span>
        </div>
        <div id="packageList" style="max-height:400px;overflow:scroll;overflow-x:hidden;">
    	    
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
</body>
</html>
       


