Ext.define('Jit.ImageView', {
	extend: 'Ext.view.View',
	alias: 'widget.imageview',
	requires: ['Ext.data.Store'],
	tpl: [
                    '<tpl for=".">',
                        '<div class="thumb-wrap">',
                            '<div class="thumb"  style="width:45px;height:45px;margin:5 1 0 2;float:left">',
                                '<img src="{url}" alt="{name}" style="" width="40px" height="40px" onerror="javascript:this.src=\'/Framework/Javascript/Other/photoControl/photoImg/no_picture.jpg\'" />',
                            '</div>',
                        '</div>',
                    '</tpl>'
                ],
	itemSelector: 'div.thumb-wrap',
	cls: 'x-image-view',
	autoScroll: true,

	initComponent: function() {
		this.callParent();
	}
});