Ext.define('ext.examples.desktop.app.AccountingManage', {
    extend: 'Ext.ux.desktop.Module',

    id: 'accountingmanage',

    init: function () {
        this.launcher = {
            text: '财务管理',
            iconCls: 'accounting_manage_16'
        }
    },

    createWindow: function () {
        var desktop = this.app.getDesktop();
        var win = desktop.getWindow('accountingmanage');
        if (!win) {
            win = desktop.createWindow({
                id: 'accountingmanage',
                title: '财务管理',
                width: '80%',
                height: '80%',
                iconCls: 'accounting_manage_16',
                animCollapse: false,
                border: false,
                hideMode: 'offsets',

                layout: 'fit',
                items: [
                    {
                        html: '<iframe src="/salary/index" frameborder="0" width="100%" height="100%"></iframe>'
                    }
                ]
            });
        }
        return win;
    }
});