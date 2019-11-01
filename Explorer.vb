Public Class Explorer
    Private gSqliteFile As New SqLiteFile()
    Private Sub Explorer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TreeViewDirectoryStructure.ImageList = ImageListIcons
        TreeViewDirectoryStructure.Nodes.Add("/", "/", ItemType.Database, ItemType.Database)
        Dim lError As String
        FolderStructure.applyFolderStructure(gSqliteFile, TreeViewDirectoryStructure.Nodes(0).Nodes, "/", ItemOrderBy.name, lError)
        If (lError <> "") Then
            MsgBox(lError)
            Exit Sub
        End If
        ListViewCurDir.SmallImageList = ImageListIcons
        ListViewCurDir.View = View.List
        ' ListViewCurDir.Alignment = ListViewAlignment.Default
        FolderStructure.applyFolderContents(gSqliteFile, ListViewCurDir, "/", ItemOrderBy.name, lError)
        If (lError <> "") Then
            MsgBox(lError)
            Exit Sub
        End If
    End Sub

    Private Function getCurrentPath() As String
        ' change windows slashes to unix, also remove the leading slash (if present)
        Return TreeViewDirectoryStructure.SelectedNode.FullPath.Replace("\", "/").Replace("//", "/")
        '    Dim lPath As String = ""
        '    Dim lNode As TreeNode = TreeViewDirectoryStructure.SelectedNode
        '    While Not lNode Is Nothing
        '        If (TreeViewDirectoryStructure.SelectedNode.Name = "/") Then
        '            Exit While
        '        Else
        '            lPath = "/" & TreeViewDirectoryStructure.SelectedNode.Name & lPath
        '        End If
        '        lNode = lNode.Parent
        '    End While
    End Function

    Private Sub partialRefresh()
        Dim lError As String = ""
        Dim lCurrentPath As String = getCurrentPath()
        'TreeViewDirectoryStructure.SelectedNode.Nodes.Clear() moved into function
        FolderStructure.applyFolderStructure(gSqliteFile, TreeViewDirectoryStructure.SelectedNode.Nodes, lCurrentPath, ItemOrderBy.name, lError)
        FolderStructure.applyFolderContents(gSqliteFile, ListViewCurDir, lCurrentPath, ItemOrderBy.name, lError)
        If (lError <> "") Then MsgBox(lError)
    End Sub

    Private Sub NewItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewItemToolStripMenuItem.Click
        NewItem.ShowDialog()
        If NewItem.DialogResult = Windows.Forms.DialogResult.OK Then
            Dim lType As FolderStructure.ItemType = Nothing
            For lItem As FolderStructure.ItemType = FolderStructure.ItemType.Folder To FolderStructure.ItemType.Document
                If (NewItem.CmbType.Text = lItem.ToString) Then
                    lType = lItem
                End If
            Next
            If lType = Nothing Then
                MsgBox("Invalid type selected")
            End If
            Dim lName As String = NewItem.TxtName.Text
            Dim lLocation As String = getCurrentPath()
            Dim lError As String = ""
            FolderStructure.createItem(gSqliteFile, lName, lLocation, lType, lError)
            If (lError <> "") Then
                MsgBox(lError)
                Exit Sub
            End If
            partialRefresh()
        End If
    End Sub

    Private Sub TreeViewDirectoryStructure_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewDirectoryStructure.AfterSelect
        Dim lLocation As String = getCurrentPath()
        Dim lError As String = ""
        FolderStructure.applyFolderContents(gSqliteFile, ListViewCurDir, lLocation, ItemOrderBy.name, lError)
        If (lError <> "") Then
            MsgBox(lError)
            Exit Sub
        End If
    End Sub

    Private Sub ListViewCurDir_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListViewCurDir.DoubleClick
        For Each lItem As ListViewItem In ListViewCurDir.SelectedItems
            If lItem.ImageIndex = ItemType.Folder Then
                For Each lNode As TreeNode In TreeViewDirectoryStructure.SelectedNode.Nodes
                    If lNode.Name = lItem.Text Then
                        TreeViewDirectoryStructure.SelectedNode = lNode
                        Exit Sub
                    End If
                Next
            ElseIf lItem.ImageIndex = ItemType.Document Then
                ' open the document
                DocumentEditor.Show()
            End If
        Next
    End Sub

End Class
