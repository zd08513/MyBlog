Ext.define('Desktop.AccountingManage', {
    extend: 'Ext.ux.desktop.Module',

    id: 'accountingmanage',

    init: function () {
        this.launcher = {
            text: '财务管理',
            iconCls: 'accounting_manage'
        }
    },

    createWindow: function () {
        var desktop = this.app.getDesktop();
        var win = desktop.getWindow('accountingmanage');
        if (!win) {
            win = desktop.createWindow({
                id: 'accountingmanage',
                title: '财务管理',
                width: 600,
                height: 400,
                iconCls: 'accounting_manage',
                animCollapse: false,
                border: false,
                hideMode: 'offsets',

                layout: 'fit',
                items: [
                    {
                        html:'<iframe src="/home/index" width="100%" height="100%"></iframe>'
                    }
                ]
            });
        }
        return win;
    }
});