Ext.require('Ext.chart.*');
Ext.require(['Ext.layout.container.Fit', 'Ext.window.MessageBox']);

Ext.onReady(function () {

    store1.loadData(z_topData);
    store2.loadData(z_topData2);

    var donut = 75,
    chart = Ext.create('Ext.chart.Chart', {
        xtype: 'chart',
        id: 'chartCmp',
        animate: true,
        store: store1,
        shadow: true,
        legend: {
            position: 'right'
        },
        insetPadding: 10,
        theme: 'Base:gradients',
        //getLegendColor: function(index) {
        //    var me = this, colorLength = 0;

        //    me.colorArrayStyle = ['#ff000', '#fff000', '#ffff00'];
        //    colorLength = me.colorArrayStyle.length;
        //    if (me.style && me.style.fill) {
        //        return me.style.fill;
        //    } else {
        //        return me.colorArrayStyle[index % colorLength];
        //    }
        //},
        series: [{
            type: 'pie',
            field: 'data1',
            //angleField: 'data1', //bind angle span to visits
            //lengthField: 'data1', //bind pie slice length to views
            showInLegend: true,
            donut: donut
            ,tips: {
                //trackMouse: true,
                //width: 10,
                //height: 28,
                renderer: function (storeItem, item) {
                    //calculate percentage.
                    var total = 0;
                    store1.each(function (rec) {
                        total += parseFloat(rec.get('data1'));
                    });
                    //this.setTitle(storeItem.get('name') + ': ' + Math.round(storeItem.get('data1') / total * 100) + '%');
                    //this.setTitle(Math.round(parseFloat(storeItem.get('data1')) / total * 100) + '%');
                }
            }
            ,highlight: {
                segment: {
                    margin: 20
                }
            }
            ,label: {
                field: 'name',
                display: 'rotate', //rotate
                contrast: true,
                font: '13px Arial'
                ,color: '#ffffff'
                ,styles: { fill: { color: '#ffffff' } }
                ,renderer: function (label) {
                    var index = chart.store.findExact('name', label);
                    return chart.store.getAt(index).data.data2;
                }
            }
        }]
    });

    var panel1 = Ext.create('widget.panel', {
        width: 280,
        height: 130,
        renderTo: "charts",
        layout: 'fit',
        items: chart
    });

    
    chart2 = Ext.create('Ext.chart.Chart', {
        xtype: 'chart',
        id: 'chartCmp2',
        animate: true,
        store: store2,
        shadow: true,
        legend: {
            position: 'right'
        },
        insetPadding: 10,
        theme: 'Base:gradients',
        //getLegendColor: function(index) {
        //    var me = this, colorLength = 0;

        //    me.colorArrayStyle = ['#ff000', '#fff000', '#ffff00'];
        //    colorLength = me.colorArrayStyle.length;
        //    if (me.style && me.style.fill) {
        //        return me.style.fill;
        //    } else {
        //        return me.colorArrayStyle[index % colorLength];
        //    }
        //},
        series: [{
            type: 'pie',
            field: 'data1',
            //angleField: 'data1', //bind angle span to visits
            //lengthField: 'data1', //bind pie slice length to views
            showInLegend: true,
            donut: donut
            ,tips: {
                //trackMouse: true,
                //width: 10,
                //height: 28,
                renderer: function (storeItem, item) {
                    //calculate percentage.
                    var total = 0;
                    store1.each(function (rec) {
                        total += parseFloat(rec.get('data1'));
                    });
                    //this.setTitle(storeItem.get('name') + ': ' + Math.round(storeItem.get('data1') / total * 100) + '%');
                    //this.setTitle(Math.round(parseFloat(storeItem.get('data1')) / total * 100) + '%');
                }
            }
            ,highlight: {
                segment: {
                    margin: 20
                }
            }
            ,label: {
                field: 'name',
                display: 'rotate', //rotate
                contrast: true,
                font: '13px Arial'
                ,color: '#ffffff'
                ,styles: { fill: { color: '#ffffff' } }
                ,renderer: function (label) {
                    var index = chart2.store.findExact('name', label);
                    return chart2.store.getAt(index).data.data2;
                }
            }
        }]
    });

    var panel2 = Ext.create('widget.panel', {
        width: 280,
        height: 130,
        renderTo: "charts2",
        layout: 'fit',
        items: chart2
    });
    
    document.getElementById("pnl1").style.display = "";
    document.getElementById("pnl2").style.display = "none";
});

fnChange = function(type) {
    if (type == 1) {
        document.getElementById("btn1").className = "z_btn1";
        document.getElementById("btn2").className = "z_btn2";
        document.getElementById("pnl1").style.display = "";
        document.getElementById("pnl2").style.display = "none";
    } else {
        document.getElementById("btn1").className = "z_btn2";
        document.getElementById("btn2").className = "z_btn1";
        document.getElementById("pnl1").style.display = "none";
        document.getElementById("pnl2").style.display = "";
    }
}