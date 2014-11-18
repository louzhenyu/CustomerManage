define(function($) {
    var temp = {
        pageList: '<%for(var i=0;i<list.length;i++){var idata = list[i];%>\
                        <tr>\
                            <td><a href="configAdd.aspx?id=<%=idata.PageId%>" class="editText" data-id="<%=idata.PageId%>">编辑</a></td>\
                            <td><%=idata.PageKey%></td>\
                            <td><%=idata.Title%></td>\
                            <td><%=idata.Version%></td>\
                            <td><%=idata.LastUpdateTime%></td>\
                          </tr>\
                    <%}%>',

        packageList: '<%for(var i=0;i<list.length;i++){var idata = list[i];%>\
                        <div class="setList jsListItem <%if(idata.select){%>on<%}%>" data-id="<%=idata.VocaVerMappingID%>">\
        	                <span class="checkBox"></span>\
                            <p class="text"><%=idata.VocationDesc%>---<%=idata.VersionDesc%></p>\
                        </div>\
                    <%}%>'
	};
	return temp;
});