Ext.define('ext.examples.desktop.app.WorkingManage', {
    extend: 'Ext.ux.desktop.Module',

    id: 'workingmanage',

    init: function () {
        this.launcher = {
            text: '工作管理',
            iconCls: 'working_manage_16'
        }
    },

    createWindow: function () {
        var desktop = this.app.getDesktop();
        var win = desktop.getWindow('accountingmanage');
        if (!win) {
            win = desktop.createWindow({
                id: 'workingmanage',
                title: '工作管理',
                width: '80%',
                height: '80%',
                iconCls: 'working_manage_16',
                animCollapse: false,
                border: false,
                hideMode: 'offsets',

                layout: 'fit',
                items: [
                    {
                        html: '<iframe src="/company/index" frameborder="0" width="100%" height="100%"></iframe>'
                    }
                ]
            });
        }
        return win;
    }
});