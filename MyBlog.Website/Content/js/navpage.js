//菜单-事件
function MainMenuClick(event, treeId, treeNode) {
    event.preventDefault()

    if (treeNode.isParent) {
        var zTree = $.fn.zTree.getZTreeObj(treeId)

        zTree.expandNode(treeNode, !treeNode.open, false, true, true)
        return
    }

    if (treeNode.target && treeNode.target == 'dialog')
        $(event.target).dialog({ id: treeNode.tabid, url: treeNode.url, title: treeNode.name })
    else
        $(event.target).navtab({ id: treeNode.tabid, url: treeNode.url, title: treeNode.name, fresh: treeNode.fresh, external: treeNode.external })
}