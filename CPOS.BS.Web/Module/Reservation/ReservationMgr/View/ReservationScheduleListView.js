Ext.define('Reservation.view.ReservationScheduleResultView', {
    extend: 'Ext.panel.Panel'
    , config: {
        /*View所属的Controller*/
        controller: null
        /*数据源*/
        , studentStore: null
    }
    , initComponent: function () {
        //初始化设置
        this.id = '__ReservationScheduleResultView';
        this.border = true;
        this.bodyStyle = { background: '#DCE9FA' };
        this.layout = 'hbox';
        //多语言资源项
        var headTextOfFacultyName = this.controller.getResource('Demo_aspx_001');
        var headTextOfClassName = this.controller.getResource('Demo_aspx_002');
        var headTextOfStudentName = this.controller.getResource('Demo_aspx_004');
        var headTextOfEntryDate = this.controller.getResource('Demo_aspx_005');
        //组织表格的列信息
        var columns = [
            { text: headTextOfFacultyName, width: 65, dataIndex: 'FacultyName', align: 'left' }
            , { text: headTextOfClassName, width: 65, dataIndex: 'ClassName', align: 'left' }
            , { text: headTextOfStudentName, width: 65, dataIndex: 'StudentName', align: 'left' }
            , { text: headTextOfEntryDate, width: 65, dataIndex: 'EntryDate', align: 'left' }
        ];
        //设置子组件
        this.items = [
            { xtype: 'grid', height: 500, id: '__ReservationScheduleResultView_grdStudentList', columns: columns, store: this.studentStore,margin:'2' }
        ];
        //调用父类方法完成初始化
        this.callParent(arguments);
    }
    /*设置数据 - 学生表格*/
    , applyStudentStore: function (data) {
        var ctl_grdStudentList = Ext.getCmp('__ReservationScheduleResultView_grdStudentList');
        if (ctl_grdStudentList != null) {
            ctl_grdStudentList.store = data;
        }
    }
});

