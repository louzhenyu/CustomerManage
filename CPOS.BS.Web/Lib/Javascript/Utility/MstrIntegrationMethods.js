var MstrIntegrationMethods = {
    /*
    生成Ajax请求所需的Url
    @pParamName Mstr报表的PromptCode数组([MSTRPrompt].[PromptCode])
    @pParamValue Mstr报表的PromptAnswer值数组（数组成员为数组）
    @pDataRightHierachyLevel Mstr报表的数据权限控制层系级别
    @pDataRightHierachyValues Mstr报表的数据权限控制层系值（数组成员为数组）
    return      返回参数值,如果没有相应的参数则返回null
    */
    getAjaxRequestUrlString: function (pParamName, pParamValue, pDataRightHierachyLevel, pDataRightHierachyValues) {
        //一般提问回答
        var promptAnswer = new Array();
        for (var i = 0; i < pParamName.length; i++) {
            var foo = {};
            foo.PromptCode = pParamName[i];
            foo.QueryCondition = pParamValue[i];
            promptAnswer.push(foo);
        }
        //数据权限提问回答
        var dataRigthPromptAnswer = new Array();
        if (pDataRightHierachyLevel != null) {
            for (var i = 0; i < pDataRightHierachyLevel.length; i++) {
                var foo = {};
                foo.Level = pDataRightHierachyLevel[i];
                foo.Values = pDataRightHierachyValues[i];
                dataRigthPromptAnswer.push(foo);
            }
        }
        var partUrl = "&method=getmstrurl&PromptAnswers=" + Ext.JSON.encode(promptAnswer) + "&DataRigthPromptAnswers=" + Ext.JSON.encode(dataRigthPromptAnswer);
        return partUrl;
    }
};