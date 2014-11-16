Ext.define('Reservation.view.ReservationScheduleResultView', {
    extend: 'Ext.panel.Panel'
    , config: {
        /*View������Controller*/
        controller: null
        /*����Դ*/
        , studentStore: null
    }
    , initComponent: function () {
        //��ʼ������
        this.id = '__ReservationScheduleResultView';
        this.border = true;
        this.bodyStyle = { background: '#DCE9FA' };
        this.layout = 'hbox';
        //��������Դ��
        var headTextOfFacultyName = this.controller.getResource('Demo_aspx_001');
        var headTextOfClassName = this.controller.getResource('Demo_aspx_002');
        var headTextOfStudentName = this.controller.getResource('Demo_aspx_004');
        var headTextOfEntryDate = this.controller.getResource('Demo_aspx_005');
        //��֯��������Ϣ
        var columns = [
            { text: headTextOfFacultyName, width: 65, dataIndex: 'FacultyName', align: 'left' }
            , { text: headTextOfClassName, width: 65, dataIndex: 'ClassName', align: 'left' }
            , { text: headTextOfStudentName, width: 65, dataIndex: 'StudentName', align: 'left' }
            , { text: headTextOfEntryDate, width: 65, dataIndex: 'EntryDate', align: 'left' }
        ];
        //���������
        this.items = [
            { xtype: 'grid', height: 500, id: '__ReservationScheduleResultView_grdStudentList', columns: columns, store: this.studentStore,margin:'2' }
        ];
        //���ø��෽����ɳ�ʼ��
        this.callParent(arguments);
    }
    /*�������� - ѧ�����*/
    , applyStudentStore: function (data) {
        var ctl_grdStudentList = Ext.getCmp('__ReservationScheduleResultView_grdStudentList');
        if (ctl_grdStudentList != null) {
            ctl_grdStudentList.store = data;
        }
    }
});

