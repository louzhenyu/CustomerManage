<!DOCTYPE html>
<html>
<head>
    <title>页面模板上传页面</title>
    <link href="css/style3.css" rel="stylesheet" type="text/css" />
    <link href="../static/css/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="section" class="page" data-js="/External/PageConfig/js/applyToCustomer">
	      
           <div class="scratchCardPopup" style="top:0;">
	            <div class="commonTitleWrap">
    	            <h2>选择套餐页</h2>
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
    </div>
    <script type="text/javascript" src="/External/static/js/lib/require.js"  defer  async="true" data-main="/External/static/js/main.js"></script>
   
    <div class="ui-mask"></div>
    <div id="packageLayer" class="scratchCardPopup" style="display:none;">
	    <div class="commonTitleWrap">
    	    <h2>应用到套餐</h2>
            <span class="cancelBtn">取消</span>
            <span class="saveBtn">确定</span>
        </div>
        
    </div>
</body>
</html>
       


